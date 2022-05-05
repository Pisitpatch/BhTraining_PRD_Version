Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class game_admin_answer_edit
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String
    Protected gid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("jci_emp_code")) Then
            Response.Redirect("login.aspx")
            'Response.Write("Please re-login again")
            Response.End()
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        tid = Request.QueryString("tid")
        gid = Request.QueryString("gid")

        If IsPostBack Then

        Else ' First time load
            bindGrid()
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.question_id , is_active , question_detail_th , question_detail_en , create_by_emp_name , create_date , ISNULL((b.num_ans),0) AS num , correct  "
            sql &= "  , ISNULL(num4,0) AS 'answer4' , ISNULL(num3,0) AS 'answer3' , ISNULL(num2,0) AS 'answer2' , ISNULL(num1,0) AS 'answer1' "
            sql &= " , a.jci_code"
            sql &= " FROM jci_master_question a "
            'sql &= " LEFT OUTER JOIN jci_master_answer b ON a.question_id = b.question_id AND ISNULL(b.is_answer_delete,0) = 0 "
            sql &= " LEFT OUTER JOIN "
            sql &= " ( SELECT question_id , COUNT(question_id) AS num_ans , ISNULL(SUM(is_correct),0) AS correct FROM jci_master_answer WHERE ISNULL(is_answer_delete,0) = 0  GROUP BY question_id) b "
            sql &= " ON a.question_id = b.question_id "

            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(*) AS num1 , question_id  FROM jci_trans_list WHERE trans_order_sort = 1 AND is_correct_answer = 1 GROUP BY trans_order_sort , question_id  ) bb"
            sql &= " ON a.question_id = bb.question_id"
            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(*) AS num2 , question_id  FROM jci_trans_list WHERE trans_order_sort = 2 AND is_correct_answer = 1 GROUP BY trans_order_sort , question_id  ) c"
            sql &= " ON a.question_id = c.question_id"
            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(*) AS num3 , question_id  FROM jci_trans_list WHERE trans_order_sort = 3 AND is_correct_answer = 1 GROUP BY trans_order_sort , question_id  ) d"
            sql &= " ON a.question_id = d.question_id"
            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(*) AS num4 , question_id  FROM jci_trans_list WHERE trans_order_sort >= 4 AND is_correct_answer = 1 GROUP BY trans_order_sort , question_id  ) e"
            sql &= " ON a.question_id = e.question_id"

            sql &= " WHERE ISNULL(is_delete,0) = 0 AND group_id = " & gid
            '  sql &= " GROUP BY a.question_id , a.is_active , a.question_detail_th , a.create_by_emp_name , a.create_date"
            sql &= " ORDER BY a.question_id ASC"
            ' Response.Write(sql)
            ' Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindPathWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id WHERE a.group_id = " & gid
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblPathWay.Text = " > <a href='admin_question_master.aspx?tid=" & tid & "'>" & ds.Tables("t1").Rows(0)("test_name_th").ToString & "</a> > " & ds.Tables("t1").Rows(0)("group_name_th").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("admin_question_edit.aspx?mode=add&tid=" & tid & "&gid=" & gid)
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "onDeleteTest") Then
            Dim sql As String
            Dim errorMsg As String
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Try
                sql = "UPDATE jci_master_question SET is_delete = 1 WHERE question_id = " & index
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

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
            Dim lblActive As Label = CType(e.Row.FindControl("lblActive"), Label)
            Dim lblCorrect As Label = CType(e.Row.FindControl("lblCorrect"), Label)

            Dim lblQ1 As Label = CType(e.Row.FindControl("lblQ1"), Label)
            Dim lblQ2 As Label = CType(e.Row.FindControl("lblQ2"), Label)

            If lblActive.Text = "1" Then
                chkSelect.Checked = True
            Else
                chkSelect.Checked = False
            End If

            If lblCorrect.Text = "1" Then
            Else
                lblQ1.ForeColor = Drawing.Color.Red
                lblQ2.ForeColor = Drawing.Color.Red
                '  Response.Write(1111)
            End If
        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Sub onChangeActive()
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chkSelect As CheckBox

        i = GridView1.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)
                chkSelect = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)

                If chkSelect.Checked = True Then
                    sql = "UPDATE jci_master_question SET is_active = 1 WHERE question_id = " & lbl.Text
                Else
                    sql = "UPDATE jci_master_question SET is_active = 0 WHERE question_id = " & lbl.Text
                End If


                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
            Next s

            conn.setDBCommit()

            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE jci_master_question SET is_active = 1 WHERE group_id = " & gid
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub
End Class
