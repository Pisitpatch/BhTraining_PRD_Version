Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci_admin_group
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String
    ' Protected mode As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        tid = Request.QueryString("tid")


        If IsPostBack Then

        Else ' First time load

            bindGridGroup()
            bindPathWay()
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

    Sub bindGridGroup()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.group_id , a.group_name_th , a.group_fix_ip , a.create_date_ts , a.create_date , a.create_by_emp_name , COUNT(b.group_id) AS num "
            sql &= " FROM jci_master_group a LEFT OUTER JOIN jci_master_question b ON a.group_id = b.group_id "
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND a.test_id = " & tid
            sql &= " GROUP BY a.group_id , a.group_name_th , a.group_fix_ip , a.create_date_ts , a.create_date , a.create_by_emp_name  "
            sql &= " ORDER BY a.group_id ASC"
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

    Sub bindPathWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_test WHERE test_id = " & tid
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblPathWay.Text = " > " & ds.Tables("t1").Rows(0)("test_name_th").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("admin_group_edit.aspx?mode=add&tid=" & tid)
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        If (e.CommandName = "onDeleteGroup") Then
            Dim sql As String
            Dim errorMsg As String
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Try
                sql = "UPDATE jci_master_group SET is_delete = 1 WHERE group_id = " & index
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                bindGridGroup()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
