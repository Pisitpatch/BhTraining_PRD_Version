Imports System.Data

Partial Class game_game_over_choice
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String = ""

    Protected gid As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("jci_emp_code")) Then
            Response.Redirect("multiple-choice-login.aspx")
            'Response.Write("Please re-login again")
            Response.End()
        End If

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If
        ' Response.End()
        tid = Request.QueryString("tid")

        gid = Request.QueryString("gid")
        If gid = "" Then
            Response.Write("Please select group")
            Return
        End If

        If IsPostBack Then

        Else ' First time load
            '  bindGridTest()
            bindScore()
            bindEvaluate()
        End If


        '  bindQuestion()
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

            '   dsQuestion.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindScore()
        Dim sql As String
        Dim ds As New DataSet
        Dim dsAll As New DataSet
        Dim totalScore As Integer = 0
        Try
            sql = "SELECT  zone_no FROM jci_trans_list a INNER JOIN jci_master_group b ON a.game_group_id = b.group_id "
            sql &= " WHERE game_group_id <> 22 AND trans_create_by_emp_code = " & Session("jci_emp_code").ToString & " GROUP BY zone_no ORDER BY zone_no "
            dsAll = conn.getDataSet(sql, "tGroup")
            ' Response.Write(sql)
            For i As Integer = 0 To dsAll.Tables("tGroup").Rows.Count - 1
                sql = "SELECT * FROM jci_trans_list a inner join jci_master_group b on a.game_group_id = b.group_id WHERE  b.zone_no = " & dsAll.Tables("tGroup").Rows(i)("zone_no").ToString
                sql &= " AND trans_create_by_emp_code = " & Session("jci_emp_code").ToString
                sql &= " AND is_correct_answer = 1 AND trans_order_sort = 1 "
                ' Response.Write(sql)
                ds = conn.getDataSet(sql, "tScore")
                ' Response.Write(111111111111111111)
                ' CType(panel1.FindControl("lblScore" & (i + 1)), Label).Text = ds.Tables("tScore").Rows.Count
                CType(panel1.FindControl("lblScore" & CInt(dsAll.Tables("tGroup").Rows(i)("zone_no").ToString())), Label).Text = ds.Tables("tScore").Rows.Count
                'lblScore1.Text = ds.Tables("tScore").Rows.Count
                totalScore += ds.Tables("tScore").Rows.Count
            Next i
            ' lblScore.Text = ds.Tables("tScore").Rows.Count & "/" & dsAll.Tables("tAll").Rows(0)(0).ToString
            lblSum.Text = totalScore
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            dsAll.Dispose()
        End Try
    End Sub

    Sub bindEvaluate()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_trans_list a INNER JOIN jci_master_group b ON a.game_group_id = b.group_id "
            sql &= " INNER JOIN jci_master_test c ON b.test_id = c.test_id AND c.category_id = 103"
            sql &= " WHERE trans_create_by_emp_code = " & Session("jci_emp_code").ToString
            ds = conn.getDataSet(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                lblEvaluate.Text = "Yes"
            Else
                lblEvaluate.Text = "No"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
