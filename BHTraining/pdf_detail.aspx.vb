Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci2013_pdf_detail
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id1 As String = ""
    Protected mode As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        id1 = Request.QueryString("id1")
        mode = Request.QueryString("mode")

        If Page.IsPostBack Then

        Else ' first time
            If mode = "edit" Then
                bindForm()
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

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Response.Redirect("pdf_list.aspx?menu=1")
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci13_pdf_list WHERE pdf_id = " & id1
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            txtdesc.Text = ds.Tables("t1").Rows(0)("pdf_name").ToString
            lblFile.Text = ds.Tables("t1").Rows(0)("pdf_name").ToString
            RadioButtonList1.SelectedValue = ds.Tables("t1").Rows(0)("is_pdf_active").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim filename As String()
        Dim ds As New DataSet

        Try

            If mode = "add" Then

                Try
                    sql = "SELECT ISNULL(MAX(pdf_id),0) + 1 AS pk FROM jci13_pdf_list"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try

                sql = "INSERT INTO jci13_pdf_list (pdf_id , pdf_name  , is_pdf_active , update_by , lastupdate_raw , lastupdate_ts) VALUES("
                sql &= "" & pk & " , "
                sql &= "'" & txtdesc.Text & "' , "
                sql &= "'" & RadioButtonList1.SelectedValue & "' , "
                sql &= "'" & Session("user_fullname").ToString & "' , "
                sql &= " GETDATE() , "
                sql &= "'" & Date.Now.Ticks & "'  "
                sql &= ")"

                id1 = pk
            Else

                sql = "UPDATE jci13_pdf_list SET pdf_name = '" & txtdesc.Text & "'  "
                sql &= " , is_pdf_active =  '" & RadioButtonList1.SelectedValue & "'  "
                sql &= " , update_by =  '" & Session("user_fullname").ToString() & "'  "
                sql &= " , lastupdate_raw =  GETDATE()  "
                sql &= " , lastupdate_ts =  '" & Date.Now.Ticks & "'  "
                sql &= " WHERE pdf_id = " & id1

            End If

            'Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            'Response.Write("pk = " & pk)
            If conn.errMessage <> "" Then
                Throw New Exception(" " & conn.errMessage & " " & sql)
            End If

            conn.setDBCommit()

            '   Response.Write(999)
            If Not IsNothing(FileUpload1.PostedFile) Then
                '  Response.Write(777)
                Dim strFileName = FileUpload1.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName <> "" Then
                    filename = strFileName.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    conn.startTransactionSQLServer()

                    sql = "UPDATE jci13_pdf_list SET pdf_path = '" & id1 & "." & extension & "'  "
                    sql &= " , pdf_size =  '" & FileUpload1.PostedFile.ContentLength & "'  "

                    sql &= " WHERE pdf_id = " & id1

                    errorMsg = conn.executeSQLForTransaction(sql)
                    'Response.Write("pk = " & pk)
                    If errorMsg <> "" Then
                        Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                    End If

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/jci/pdf/" & id1 & "." & extension))
                    Dim MyFile As New FileInfo(Server.MapPath("../share/jci/pdf/" & id1 & "." & extension))
                    If MyFile.Exists() Then
                        conn.setDBCommit()
                    Else
                        ' Throw New Exception("File Not Found")
                        ' MessageBox.Show("File not found.")
                        conn.setDBRollback()
                        Return
                    End If
                End If
                'Response.Write(11111111111111111)
            


            End If

            'bindForm()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        Finally

        End Try

        Response.Redirect("pdf_list.aspx?menu=1")
    End Sub
End Class
