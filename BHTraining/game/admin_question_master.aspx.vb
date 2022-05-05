Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class game_admin_question_master
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
            sql &= " , a.num_question , a.group_remark , a.zone_no "
            sql &= " FROM jci_master_group a LEFT OUTER JOIN jci_master_question b ON a.group_id = b.group_id AND ISNULL(b.is_delete,0) = 0 "
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND a.test_id = " & tid
            sql &= " GROUP BY a.group_id , a.group_name_th , a.group_fix_ip , a.create_date_ts , a.create_date , a.create_by_emp_name , a.num_question ,  a.group_remark , a.zone_no"
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

    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblTarget As Label = CType(e.Row.FindControl("lblTarget"), Label)
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblIP As Label = CType(e.Row.FindControl("lblIP"), Label)

            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM jci_main_target WHERE group_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblTarget.Text &= "- " & ds.Tables("t1").Rows(i)("target_name").ToString & "<br/>"
                Next i

                sql = "SELECT * FROM jci_ip_target WHERE group_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblIP.Text &= "- " & ds.Tables("t1").Rows(i)("ip_address").ToString & "<br/>"
                Next i
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
