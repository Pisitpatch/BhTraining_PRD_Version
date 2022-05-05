Imports System.Data
Imports System.IO
Imports ShareFunction
Partial Class idp_popup_editcomment
    Inherits System.Web.UI.Page

    Protected session_id As String
    Protected mode As String = ""
    Protected id As String
    Protected cid As String
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        session_id = Session("session_myid").ToString
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        cid = Request.QueryString("cid")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If IsPostBack Then

        Else ' First time
            bindCommentDetail()
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

    Sub bindCommentDetail()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_manager_comment WHERE comment_id = " & cid
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtcomment_detail.Value = ds.Tables("t1").Rows(0)("detail").ToString
            txtacknowedge_status.SelectedValue = ds.Tables("t1").Rows(0)("comment_status_id").ToString
            txtcomment.SelectedValue = ds.Tables("t1").Rows(0)("subject_id").ToString
            lblJobType.Text = ds.Tables("t1").Rows(0)("review_by_jobtype").ToString
            txtname.Value = ds.Tables("t1").Rows(0)("review_by_name").ToString
            txtjobtitle.Value = ds.Tables("t1").Rows(0)("review_by_jobtitle").ToString
            txtdeptname.Value = ds.Tables("t1").Rows(0)("review_by_dept_name").ToString
            txtdatetime.Value = ds.Tables("t1").Rows(0)("create_date").ToString
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub updateOnlyLog(ByVal status_id As String, Optional ByVal log_remark As String = "")
        Dim sql As String
        Dim errorMsg As String

        sql = "INSERT INTO idp_status_log (status_id , status_name , idp_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
        sql &= "'" & status_id & "' ,"
        sql &= "'" & "" & "' ,"
        sql &= "'" & id & "' ,"
        sql &= "GETDATE() ,"
        sql &= "'" & Date.Now.Ticks & "' ,"
        sql &= "'" & Session("user_fullname").ToString & "' ,"
        sql &= "'" & Session("user_position").ToString & "' ,"
        sql &= "'" & Session("dept_name").ToString & "' ,"
        sql &= "'" & log_remark & "' "
        sql &= ")"
        '  Response.Write(sql)
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If

    End Sub
 
    Protected Sub cmdSave1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave1.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            ' Response.Write(txtcomment_detail.Value)
            sql = "UPDATE idp_manager_comment SET comment_status_id = " & txtacknowedge_status.SelectedValue
            sql &= " , comment_status_name = '" & txtacknowedge_status.SelectedItem.Text & "' "
            sql &= " , subject_id = '" & txtcomment.SelectedValue & "' "
            sql &= " , subject = '" & txtcomment.SelectedItem.Text & "' "
            sql &= " , detail = '" & addslashes(txtcomment_detail.Value) & "' "

            sql &= " WHERE comment_id = " & cid
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            updateOnlyLog("0", txtacknowedge_status.SelectedItem.Text)
            conn.setDBCommit()

            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; window.close();"
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
