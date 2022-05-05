Imports System.Data
Imports System.IO
Imports ShareFunction
Partial Class ssip_popup_editcomment
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
            If cid <> "" Then
                bindCommentDetail()
            End If

        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
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
            sql = "SELECT * FROM ssip_manager_comment WHERE comment_id = " & cid
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtcomment_detail.Value = ds.Tables("t1").Rows(0)("detail").ToString

            txtadd_subject.Text = ds.Tables("t1").Rows(0)("subject").ToString
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

    Protected Sub cmdSave1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave1.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            ' Response.Write(txtcomment_detail.Value)
            sql = "UPDATE ssip_manager_comment SET "
           
            sql &= "  subject = '" & addslashes(txtadd_subject.Text) & "' "
            sql &= " , detail = '" & addslashes(txtcomment_detail.Value) & "' "

            sql &= " WHERE comment_id = " & cid
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            ' updateOnlyLog("0", txtacknowedge_status.SelectedItem.Text)
            conn.setDBCommit()

            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; window.close();"
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
