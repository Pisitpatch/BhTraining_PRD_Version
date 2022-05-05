Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_popup_training_file
    Inherits System.Web.UI.Page
    Protected session_id As String
    Protected mode As String = ""
    Protected id As String
    Protected fid As String
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        session_id = Session("session_myid").ToString
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        fid = Request.QueryString("fid")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If IsPostBack Then

        Else ' First time
            bindFile()
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindFile()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM idp_file_list a WHERE 1 = 1"
            sql &= " AND a.expense_id = " & fid

            ' Response.Write(sql)

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridFile.DataSource = ds.Tables(0)
            GridFile.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Protected Sub cmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim filename As String()
        Dim ds As New DataSet

        Try


            If Not IsNothing(FileUpload1.PostedFile) Then
                Dim strFileName = FileUpload1.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)
                Try
                    sql = "SELECT ISNULL(MAX(file_id),0) + 1 AS pk FROM idp_file_list"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO idp_file_list (file_id , expense_id ,  file_name , file_path , file_size , create_date) VALUES("
                sql &= "" & pk & " , "
                sql &= "" & fid & " , "
                sql &= "'" & strFileName & "' , "
                sql &= "'" & "ext_traning_" & Session("emp_code").ToString & "_" & pk & "." & extension & "' , "
                sql &= "'" & FileUpload1.PostedFile.ContentLength & "' , "
                sql &= " GETDATE()  "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                'Response.Write("pk = " & pk)
                If errorMsg <> "" Then
                    Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                End If

                FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/idp/ext_training/ext_traning_" & Session("emp_code").ToString & "_" & pk & "." & extension))

                If File.Exists(Server.MapPath("../share/idp/ext_training/ext_traning_" & Session("emp_code").ToString & "_" & pk & "." & extension)) Then
                    conn.setDBCommit()
                Else
                    conn.setDBRollback()
                    Return
                End If
            End If

            bindFile()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Protected Sub cmdDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteFile.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim lblFilePath As Label

        i = GridFile.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridFile.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridFile.Rows(s).FindControl("chkSelect"), CheckBox)

                ' Response.Write(lbl.Text)
                If chk.Checked Then
                    sql = "DELETE FROM idp_file_list WHERE file_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                        'Exit For
                    End If
                End If
                ' Response.Write("rrr")

            Next s

            For s As Integer = 0 To i - 1
                lblFilePath = CType(GridFile.Rows(s).FindControl("lblFilePath"), Label)
                File.Delete(Server.MapPath("../share/idp/ext_training/" & lblFilePath.Text))
            Next s

            conn.setDBCommit()
            bindFile()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub
End Class
