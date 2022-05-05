Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class star_star_news_edit
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = "id"
    Protected lang As String = "th"
    Protected mode As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Page.IsPostBack Then
        Else ' First time load
            If mode = "edit" Then
                bindDetail()
            End If
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)(
        End Try
    End Sub

    Sub bindDetail()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_news WHERE ISNULL(is_delete,0) = 0 AND new_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txttopic.Text = ds.Tables("t1").Rows(0)("title_th").ToString
            txtdetail.Text = ds.Tables("t1").Rows(0)("detail_th").ToString
            txtdate.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("new_date_ts").ToString)
            txtstatus.SelectedValue = ds.Tables("t1").Rows(0)("is_active").ToString
            lblFileName.Text = "<a href='../share/star/attach_file/" & ds.Tables("t1").Rows(0)("file_path").ToString & "' target='_blank'>" & ds.Tables("t1").Rows(0)("file_name").ToString & "</a>"
            lblFilePath.Text = ds.Tables("t1").Rows(0)("file_path").ToString

            If lblFilePath.Text <> "" Then
                linkDeleteFile.Visible = True
            Else
                linkDeleteFile.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim filename As String()
        Try

            If mode = "add" Then
                pk = getPK("new_id", "star_news", conn)
                sql = "INSERT INTO star_news (new_id , title_th , detail_th , new_date , new_date_ts , create_by , create_date , is_active , is_delete ) VALUES("
                sql &= "'" & pk & "' ,"
                sql &= "'" & addslashes(txttopic.Text) & "' ,"
                sql &= "'" & addslashes(txtdetail.Text) & "' ,"
                sql &= "'" & convertToSQLDatetime(txtdate.Text) & "' ,"
                sql &= "'" & ConvertDateStringToTimeStamp(txtdate.Text) & "' ,"
                sql &= "'" & Session("user_fullname").ToString & "' ,"
                sql &= " GETDATE() ,"
                sql &= "'" & txtstatus.SelectedValue & "' ,"
                sql &= " 0 "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                id = pk

            End If

            If mode = "edit" Then
                sql = "UPDATE star_news SET title_th ='" & addslashes(txttopic.Text) & "'"
                sql &= ", detail_th =  '" & addslashes(txtdetail.Text) & "'"
                sql &= ", new_date =  '" & convertToSQLDatetime(txtdate.Text) & "'"
                sql &= ", new_date_ts =  '" & ConvertDateStringToTimeStamp(txtdate.Text) & "'"
                sql &= ", create_by =  '" & Session("user_fullname").ToString & "'"
                sql &= ", create_date =  GETDATE() "
                sql &= ", is_active =  '" & txtstatus.SelectedValue & "'"
                sql &= " WHERE new_id = " & id

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If


            End If

            If Not IsNothing(FileUpload0.PostedFile) Then

                Dim strFileName = FileUpload0.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim new_filename As String = Date.Now.Ticks

                If strFileName <> "" Then
                    filename = strFileName.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    sql = "UPDATE star_news SET file_name ='" & addslashes(strFileName) & "'"
                    sql &= ", file_path =  '" & new_filename & "." & extension & "'"
                    sql &= ", file_size =  '" & FileUpload0.PostedFile.ContentLength & "'"

                    sql &= " WHERE new_id = " & id

                    errorMsg = conn.executeSQLForTransaction(sql)
                    'Response.Write("pk = " & pk)
                    If errorMsg <> "" Then
                        Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                    End If

                    FileUpload0.PostedFile.SaveAs(Server.MapPath("../share/star/attach_file/" & new_filename & "." & extension))
                End If

            End If

            conn.setDBCommit()

            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; window.close();"
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub linkDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkDeleteFile.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE star_news SET file_name = null , file_path = null , file_size = null WHERE new_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            'Response.Write("pk = " & pk)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If


            File.Delete(Server.MapPath("../share/star/attach_file/" & lblFilePath.Text))
            conn.setDBCommit()

            linkDeleteFile.Visible = False

            bindDetail()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
