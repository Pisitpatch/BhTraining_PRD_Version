Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_popup_idp_file
    Inherits System.Web.UI.Page

    Protected session_id As String
    Protected mode As String = ""
    Protected id As String
    Protected fid As String
    Protected empcode As String
    Protected viewtype As String = ""
    Protected flag As String = ""

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
        flag = Request.QueryString("flag")
        empcode = Request.QueryString("empcode")
        viewtype = Session("viewtype").ToString

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If IsPostBack Then

        Else ' First time
            bindFile()
            If flag = "ladder" Then
                checkStatus()
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
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub checkStatus()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_trans_list a INNER JOIN idp_function_tab b ON a.idp_id = b.idp_id "
            sql &= " WHERE 1 = 1 AND b.function_id = " & fid

            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(viewtype)
            If viewtype = "hr" Or viewtype = "educator" Then
                cmdDeleteFile.Enabled = True
                cmdSave.Enabled = True
                cmdUpload.Enabled = True
                FileUpload1.Enabled = True
            Else
                '  Response.Write("xxxx")
                If ds.Tables(0).Rows(0)("status_id").ToString = "" Or ds.Tables(0).Rows(0)("status_id").ToString = "1" Then
                    cmdDeleteFile.Enabled = True
                    cmdSave.Enabled = True
                    cmdUpload.Enabled = cmdSave.Enabled = True
                    FileUpload1.Enabled = True
                Else
                    cmdDeleteFile.Enabled = False
                    cmdSave.Enabled = False
                    cmdUpload.Enabled = False
                    FileUpload1.Enabled = False
                End If

            End If

            'If ds.Tables(0).Rows(0)("status_id").ToString <> "" And ds.Tables(0).Rows(0)("status_id").ToString = "1" Then
            '    If viewtype <> "hr" And viewtype <> "educator" Then
            '        cmdDeleteFile.Enabled = False
            '        cmdSave.Enabled = False
            '        cmdUpload.Enabled = False
            '        FileUpload1.Enabled = False
            '    Else
            '        cmdDeleteFile.Enabled = True
            '        cmdSave.Enabled = True
            '        cmdUpload.Enabled = cmdSave.Enabled = True
            '        FileUpload1.Enabled = True
            '    End If

            'Else
            '    If viewtype = "" Then
            '        If ds.Tables(0).Rows(0)("status_id").ToString = "" Or ds.Tables(0).Rows(0)("status_id").ToString = "1" Then
            '            cmdDeleteFile.Enabled = True
            '            cmdSave.Enabled = True
            '            cmdUpload.Enabled = True
            '            FileUpload1.Enabled = True
            '        Else
            '            cmdDeleteFile.Enabled = False
            '            cmdSave.Enabled = False
            '            cmdUpload.Enabled = False
            '            FileUpload1.Enabled = False
            '        End If
            '    Else
            '        cmdDeleteFile.Enabled = False
            '        cmdSave.Enabled = False
            '        cmdUpload.Enabled = False
            '        FileUpload1.Enabled = False
            '    End If


            'End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindFile()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM idp_file_list a INNER JOIN idp_function_tab b ON a.function_id = b.function_id "
            sql &= " INNER JOIN idp_trans_list c ON b.idp_id = c.idp_id "
            sql &= " WHERE 1 = 1 AND a.function_id = " & fid

            ' Response.Write(sql)

            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count = 0 Then
                cmdDeleteFile.Visible = False
                cmdSave.Visible = False
            Else
                cmdDeleteFile.Visible = True
                cmdSave.Visible = True
            End If

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


                sql = "INSERT INTO idp_file_list (file_id , function_id ,  file_name , file_path , file_size , create_date) VALUES("
                sql &= "" & pk & " , "
                sql &= "" & fid & " , "
                sql &= "'" & strFileName & "' , "
                sql &= "'" & empcode & "_" & pk & "." & extension & "' , "
                sql &= "'" & FileUpload1.PostedFile.ContentLength & "' , "
                sql &= " GETDATE()  "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                'Response.Write("pk = " & pk)
                If errorMsg <> "" Then
                    Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                End If

                FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/idp/attach_file/" & empcode & "_" & pk & "." & extension))

                If File.Exists(Server.MapPath("../share/idp/attach_file/" & empcode & "_" & pk & "." & extension)) Then
                    conn.setDBCommit()
                Else
                    conn.setDBRollback()
                    Return
                End If
            End If

            bindFile()

            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; "
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
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

                    lblFilePath = CType(GridFile.Rows(s).FindControl("lblFilePath"), Label)
                    File.Delete(Server.MapPath("../share/idp/attach_file/" & lblFilePath.Text))

                End If
                ' Response.Write("rrr")

            Next s


            conn.setDBCommit()
            bindFile()

            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; "
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub



    Protected Sub GridFile_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridFile.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

        End If
    End Sub

   
    Protected Sub GridFile_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridFile.SelectedIndexChanged

    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click

        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lblPK As Label
        Dim txtdate1 As TextBox
        Dim txtremark1 As TextBox

        Try
            i = GridFile.Rows.Count

            For s As Integer = 0 To i - 1
                lblPK = CType(GridFile.Rows(s).FindControl("lblPK"), Label)
                txtdate1 = CType(GridFile.Rows(s).FindControl("txtdate1"), TextBox)
                txtremark1 = CType(GridFile.Rows(s).FindControl("txtremark1"), TextBox)

                sql = "UPDATE idp_file_list SET date_action_ts = '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' "
                sql &= " , date_action_raw = '" & convertToSQLDatetime(txtdate1.Text) & "' "
                sql &= " , action_remark = '" & addslashes(txtremark1.Text) & "' "


                sql &= "  WHERE file_id = " & lblPK.Text
                '  Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                    Exit For
                End If
            Next s

            conn.setDBCommit()
            bindFile()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try

    End Sub

End Class
