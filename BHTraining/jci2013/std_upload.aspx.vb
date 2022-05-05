Imports System.IO
Imports System.Data
Imports ShareFunction

Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

Partial Class jci2013_std_upload
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
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

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
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


            FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/jci/pdf/" & "std" & "." & extension))
            Dim MyFile As New FileInfo(Server.MapPath("../share/jci/pdf/" & "std" & "." & extension))
            If MyFile.Exists() Then
                '  Response.Write(Server.MapPath("../share/jci/pdf/" & "std" & "." & extension) & "<br/>")
                ReadExcelFileDOM(Server.MapPath("../share/jci/pdf/" & "std" & "." & extension))
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

                    'Value = XLGetCellValue(fileName, "Sheet1", "G" & i)
                    If (XLGetCellValue(fileName, "Sheet1", "G" & i) = "1.1") Then
                        Response.End()
                    End If
                    '  Response.Write(i & " : " & XLGetCellValue(fileName, "Sheet1", "G" & i) & "<br/>")
                    If i > 1 Then
                        addData(XLGetCellValue(fileName, "Sheet1", "A" & i), XLGetCellValue(fileName, "Sheet1", "B" & i), XLGetCellValue(fileName, "Sheet1", "C" & i), XLGetCellValue(fileName, "Sheet1", "D" & i), XLGetCellValue(fileName, "Sheet1", "E" & i), XLGetCellValue(fileName, "Sheet1", "F" & i), XLGetCellValue(fileName, "Sheet1", "G" & i), XLGetCellValue(fileName, "Sheet1", "H" & i), XLGetCellValue(fileName, "Sheet1", "I" & i), XLGetCellValue(fileName, "Sheet1", "J" & i), XLGetCellValue(fileName, "Sheet1", "K" & i))
                    End If




                    i += 1
                Next

                conn.setDBCommit()
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

        sql = "DELETE FROM jci13_std_list WHERE edition = '" & txtedition.Text & "' "
      

        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If

    End Sub

    Sub addData(type As String, edition As String, section_no As String, section_name As String, chapter As String, chapter_name As String, goal As String, std_no As String, std_detail As String, measure_element_no As String, measure_element_detail As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""

        pk = getPK("std_id", "jci13_std_list", conn)

        sql = "INSERT INTO jci13_std_list (std_id , type , edition , section_no , section_name , chapter , chapter_name , goal , std_no , std_detail , measure_element_no , measure_element_detail) VALUES("
        sql &= "'" & pk & "' , "
        sql &= "'" & type & "' , "
        sql &= "'" & edition & "' , "
        sql &= "'" & section_no & "' , "
        sql &= "'" & addslashes(section_name) & "' , "
        sql &= "'" & chapter & "' , "
        sql &= "'" & addslashes(chapter_name) & "' , "
        sql &= "'" & goal & "' , "
        sql &= "'" & std_no & "' , "
        sql &= "'" & addslashes(std_detail) & "' , "
        sql &= "'" & measure_element_no & "' , "
        sql &= "'" & addslashes(measure_element_detail) & "'  "
        sql &= ")"

        Response.Write(sql & "<br/>")
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If

    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Response.Redirect("std_list.aspx?menu=2")
    End Sub
End Class
