Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci_popup_review_score
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String
    Protected emp_code As String
    Protected question_id As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("jci_emp_code")) Then
            ' Response.Redirect("login.aspx")
            Response.Write("Please re-login again")
            Response.End()
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        id = Request.QueryString("id")

        If IsPostBack Then

        Else ' Load first time
            bindForm()
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

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_trans_list WHERE trans_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            lblTitle.Text = ds.Tables("t1").Rows(0)("trans_q_name_th").ToString
            lblName.Text = ds.Tables("t1").Rows(0)("trans_create_by_name").ToString
            lblEmpCode.Text = ds.Tables("t1").Rows(0)("trans_create_by_emp_code").ToString
            lblDeptName.Text = ds.Tables("t1").Rows(0)("trans_dept_name").ToString
            lblJobTitle.Text = ds.Tables("t1").Rows(0)("trans_job_title").ToString

            If ds.Tables("t1").Rows(0)("review_score").ToString = "1" Then
                txtscore1.Checked = True
            ElseIf ds.Tables("t1").Rows(0)("review_score").ToString = "2" Then
                txtscore2.Checked = True
            ElseIf ds.Tables("t1").Rows(0)("review_score").ToString = "3" Then
                txtscore3.Checked = True
            End If

            emp_code = ds.Tables("t1").Rows(0)("trans_create_by_emp_code").ToString
            question_id = ds.Tables("t1").Rows(0)("question_id").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE jci_trans_list SET review_by_name = '" & Session("jci_user_fullname").ToString & "' "
            sql &= " , review_by_emp_code = " & Session("jci_emp_code").ToString
            sql &= " , review_date = GETDATE() "
            sql &= " , review_date_ts = " & Date.Now.Ticks

            If txtscore1.Checked = True Then
                sql &= " , review_score = " & 1
            ElseIf txtscore2.Checked = True Then
                sql &= " , review_score = " & 2
            ElseIf txtscore3.Checked = True Then
                sql &= " , review_score = " & 3
            End If

            sql &= " WHERE trans_id = " & id
            ' Response.Write(sql & "<hr />")
            ' Response.Write(txtscore1.Checked)
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; window.close();"
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        txtscore1.Checked = False
        txtscore2.Checked = False
        txtscore3.Checked = False
    End Sub
End Class
