Imports System.IO
Imports System.Data
Imports ShareFunction

Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

Partial Class jci2013_form_detail
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""
    Protected mode As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")

        If IsPostBack Then

        Else ' First time load


            If mode = "edit" Then
                cmdAddME.Visible = True
                cmdDelME.Visible = True
                GridView1.Visible = True
                FileUpload1.Visible = True
                cmdUpload.Visible = True
                bindForm()
                bindGrid()
            End If


        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_form_list a "
            sql &= " WHERE form_id =  " & id

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtname.Text = ds.Tables("t1").Rows(0)("form_name").ToString
            RadioButtonList1.SelectedValue = ds.Tables("t1").Rows(0)("is_form_active").ToString
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_std_select a "
            sql &= " WHERE form_id =  " & id

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView1.DataSource = ds
            GridView1.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAddME_Click(sender As Object, e As EventArgs) Handles cmdAddME.Click
        Response.Redirect("form_search_me.aspx?menu=3&id=" & id & "&mode=" & mode)
    End Sub

    Protected Sub cmdDelME_Click(sender As Object, e As EventArgs) Handles cmdDelME.Click
        Dim sql As String = ""
        Dim errorMsg As String = ""
        Dim pk As String = ""
        Dim chk As CheckBox
        Dim lblPK As Label
        Try
            For i As Integer = 0 To GridView1.Rows.Count - 1

                chk = CType(GridView1.Rows(i).FindControl("chk"), CheckBox)
                lblPK = CType(GridView1.Rows(i).FindControl("lblPK"), Label)

                If chk.Checked Then
                    'Response.Write(i)


                    sql = "DELETE FROM jci13_std_select WHERE me_id = " & lblPK.Text

                    'Response.Write(sql)
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If
            Next i

            conn.setDBCommit()

            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Response.Redirect("form_list.aspx?menu=3")
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            If mode = "add" Then
                pk = getPK("form_id", "jci13_form_list", conn)
                sql = "INSERT INTO jci13_form_list (form_id , form_name , is_form_active , lastupdate_raw , lastupdate_ts) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & addslashes(txtname.Text) & "' ,"
                sql &= " '" & RadioButtonList1.SelectedValue & "' ,"
                sql &= " GETDATE() , "
                sql &= " '" & Date.Now.Ticks & "' "
                sql &= ")"
                id = pk

            Else
                sql = "UPDATE jci13_form_list SET form_name = '" & txtname.Text & "' "
                sql &= " , is_form_active = '" & RadioButtonList1.SelectedValue & "' "
                sql &= " , lastupdate_raw = GETDATE() "
                sql &= " , lastupdate_ts = '" & Date.Now.Ticks & "' "
                sql &= " WHERE form_id = " & id
            End If
          

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        End Try

        Response.Redirect("form_detail.aspx?id=" & id & "&menu=3&mode=edit")
    End Sub

    Protected Sub cmdUpload_Click(sender As Object, e As EventArgs) Handles cmdUpload.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim filename As String()
        Dim ds As New DataSet

        If Not IsNothing(FileUpload1.PostedFile) Then

            Dim strFileName = FileUpload1.FileName
            Dim extension As String
            Dim iCount As Integer = 0

            If strFileName = "" Then
                Return
            End If
            'Response.Write(11111111111111111)
            filename = strFileName.Split(".")
            iCount = UBound(filename)
            extension = filename(iCount)


            FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/jci/pdf/" & "me" & "." & extension))
            Dim MyFile As New FileInfo(Server.MapPath("../share/jci/pdf/" & "me" & "." & extension))
            If MyFile.Exists() Then
                ' Response.Write(Server.MapPath("../share/jci/pdf/" & "me" & "." & extension) & "<br/>")
                ReadExcelFileDOM(Server.MapPath("../share/jci/pdf/" & "me" & "." & extension))
                ' Dim value As String
                'value = XLGetCellValue(Server.MapPath("../share/jci/pdf/" & "std" & "." & extension), "Sheet1", "A3")
                'Response.Write(value)
                ' conn.setDBCommit()
            Else
                ' Throw New Exception("File Not Found")
                ' MessageBox.Show("File not found.")
                'conn.setDBRollback()
                Return
            End If


        End If
    End Sub

    Public Function XLGetCellValue(ByVal fileName As String, ByVal sheetName As String, ByVal addressName As String) As String
        Dim value As String = Nothing

        Using document As SpreadsheetDocument = SpreadsheetDocument.Open(fileName, False)

            Dim wbPart As WorkbookPart
            wbPart = document.WorkbookPart

            ' Find the sheet with the supplied name, and then use that 
            ' Sheet object to retrieve a reference to the appropriate 
            ' worksheet.
            Dim theSheet As Sheet
            theSheet = wbPart.Workbook.Descendants(Of Sheet)().Where(Function(s) s.Name = sheetName).FirstOrDefault()

            If theSheet Is Nothing Then
                Throw New ArgumentException("sheetName")
            End If

            ' Retrieve a reference to the worksheet part, and then use its 
            ' Worksheet property to get a reference to the cell whose address 
            ' matches the address you supplied:
            Dim wsPart As WorksheetPart = CType(wbPart.GetPartById(theSheet.Id), WorksheetPart)
            Dim theCell As Cell
            theCell = wsPart.Worksheet.Descendants(Of Cell).Where(Function(c) c.CellReference = addressName).FirstOrDefault

            ' If the cell does not exist, return an empty string.
            If theCell IsNot Nothing Then
                value = theCell.InnerText
                Dim myvalue As Double
                Double.TryParse(value, myvalue)
                If myvalue > 0 Then
                    ' Response.Write(FormatNumber(myvalue, 3))
                    value = myvalue
                End If
                ' If the cell represents an numeric value, you are done. 
                ' For dates, this code returns the serialized value that 
                ' represents the date. The code handles strings and Booleans
                ' individually. For shared strings, the code looks up the 
                ' corresponding value in the shared string table. For Booleans, 
                ' the code converts the value into the words TRUE or FALSE.
                If theCell.DataType IsNot Nothing Then
                    Select Case theCell.DataType.Value
                        Case CellValues.SharedString
                            ' For shared strings, look up the value in the shared 
                            ' strings table.
                            Dim stringTable = wbPart.GetPartsOfType(Of SharedStringTablePart).FirstOrDefault()
                            ' If the shared string table is missing, something is wrong.
                            ' Return the index that you found in the cell.
                            ' Otherwise, look up the correct text in the table.
                            If stringTable IsNot Nothing Then
                                value = stringTable.SharedStringTable.ElementAt(Integer.Parse(value)).InnerText

                                ' Response.Write(0)
                            End If
                        Case CellValues.Boolean
                            Select Case value
                                Case "0"
                                    value = "FALSE"
                                Case Else
                                    value = "TRUE"
                            End Select
                    End Select
                End If
            End If
        End Using
        Return value
    End Function

    Public Sub ReadExcelFileDOM(ByVal fileName As String)
        Dim i As Integer = 1
        'Dim value As String

        Using spreadsheetDocument As SpreadsheetDocument = spreadsheetDocument.Open(fileName, False)
            Dim workbookPart As WorkbookPart = spreadsheetDocument.WorkbookPart
            Dim worksheetPart As WorksheetPart = workbookPart.WorksheetParts.First()
            Dim sheetData As SheetData = worksheetPart.Worksheet.Elements(Of SheetData)().First()
            Dim text As String = ""

            deleteData()
            Try
                For Each r As Row In sheetData.Elements(Of Row)()
                    ' For Each c As Cell In r.Elements(Of Cell)()
                    ' text = c.CellValue.Text
                    'text = c.CellValue.InnerText

                    'Console.Write(text & " ")


                    'Next
                    '   Response.Write(i & "<br/>")

                    'value = XLGetCellValue(fileName, "Sheet1", "C" & i)
                    '  Response.Write(i & " : " & fileName, "Sheet1", "A" & i & "<br/>")
                    ' Response.End()
                    If i > 1 Then
                        If XLGetCellValue(fileName, "Sheet1", "A" & i) = "" Then
                            Exit For
                        End If
                        addData(XLGetCellValue(fileName, "Sheet1", "A" & i), XLGetCellValue(fileName, "Sheet1", "B" & i), XLGetCellValue(fileName, "Sheet1", "C" & i), XLGetCellValue(fileName, "Sheet1", "D" & i), XLGetCellValue(fileName, "Sheet1", "E" & i), XLGetCellValue(fileName, "Sheet1", "F" & i), XLGetCellValue(fileName, "Sheet1", "G" & i), XLGetCellValue(fileName, "Sheet1", "H" & i), XLGetCellValue(fileName, "Sheet1", "I" & i), XLGetCellValue(fileName, "Sheet1", "J" & i), XLGetCellValue(fileName, "Sheet1", "K" & i))
                    End If




                    i += 1
                Next

                conn.setDBCommit()

                bindGrid()
            Catch ex As Exception
                Response.Write(ex.Message)
                conn.setDBRollback()
            End Try

            '  Console.WriteLine()
            '  Console.ReadKey()

        End Using
    End Sub

    Sub deleteData()
        Dim sql As String
        Dim errorMsg As String

        sql = "DELETE FROM jci13_std_select WHERE form_id = " & id


        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If

    End Sub

    Sub addData(type As String, edition As String, section_no As String, chapter As String, chapter_name As String, std_no As String, me_no As String, remark As String, order As String, criteria As String, method As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim ds As New DataSet

        pk = getPK("me_id", "jci13_std_select", conn)

        sql = "INSERT INTO jci13_std_select (me_id , form_id , type , edition , section_no , chapter , chapter_name , std_no , measure_element_no , remark , order_sort , criteria , method  "
        sql &= " , section_name , std_detail , goal , measure_element_detail "
        sql &= ")"
        sql &= " SELECT "
        sql &= "'" & pk & "' , "
        sql &= "'" & id & "' , "
        sql &= "'" & type & "' , "
        sql &= "'" & edition & "' , "
        sql &= "'" & section_no & "' , "
        sql &= "'" & chapter & "' , "
        sql &= "'" & chapter_name & "' , "
        sql &= "'" & std_no & "' , "
        sql &= "'" & me_no & "' , "
        sql &= "'" & remark & "' , "
        sql &= "'" & order & "' , "
        sql &= "'" & addslashes(criteria) & "' , "
        sql &= "'" & method & "' , "

        sql &= " section_name , std_detail , goal , measure_element_detail FROM jci13_std_list WHERE 1 = 1"
        sql &= " AND edition = '" & edition & "' "
        sql &= " AND section_no = '" & section_no & "' "
        sql &= " AND chapter = '" & chapter & "' "
        sql &= " AND std_no = '" & std_no & "' "
        sql &= " AND measure_element_no = '" & me_no & "' "

        Response.Write(sql & "<br/>")

        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If

    End Sub
End Class
