Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci2013_accessment
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected form_id As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        form_id = Request.QueryString("form_id")

        If IsPostBack Then

        Else ' First time load
            bindLeader()
            bindGrid()
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

    Sub bindLeader()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT emp_code , emp_name "

            sql &= " FROM jci13_assessment_list a "
            sql &= " WHERE ISNULL(is_assessment_delete,0) = 0 AND ISNULL(emp_name,'') <> ''  "
            sql &= " GROUP BY emp_code , emp_name ORDER BY emp_name"

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtleader.DataSource = ds
            txtleader.DataBind()

            txtleader.Items.Insert(0, New ListItem("All", ""))
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
            '   sql &= " , CASE WHEN is_form_active = 1 THEN 'Active' ELSE 'Inactive' END AS status_name "
            sql &= " FROM jci13_assessment_list a INNER JOIN jci13_form_list b ON a.form_id = b.form_id "
            sql &= " LEFT OUTER JOIN (SELECT COUNT(assessment_id) AS num , assessment_id  FROM jci13_assessment_me_list GROUP BY assessment_id) c ON a.assessment_id = c.assessment_id "
            sql &= " WHERE ISNULL(is_assessment_delete,0) = 0 AND a.form_id = " & form_id
            If txtleader.SelectedIndex > 0 Then
                sql &= " AND a.emp_code = '" & txtleader.SelectedValue & "' "
            End If
            sql &= " ORDER BY a.assessment_id DESC"
            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Response.Redirect("assessment_detail.aspx?mode=add&menu=3&form_id=" & form_id)
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "onDeleteGroup") Then
            Dim sql As String
            Dim errorMsg As String
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Try
                sql = "UPDATE jci13_assessment_list SET is_assessment_delete = 1 WHERE assessment_id = " & index
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                bindGrid()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub txtleader_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtleader.SelectedIndexChanged
        bindGrid()
    End Sub
End Class
