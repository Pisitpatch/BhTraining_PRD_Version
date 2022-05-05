Imports System.Data
Partial Class game_home
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected loginMsg As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If
        ' Response.Write(Session.Count)
        If Not Page.IsPostBack Then
            Session.Abandon()
            ' Session.Clear()
            ' Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))

            bindGroup()
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


    Sub authenWithDatabase()
        Dim sql As String
        Dim ds As New DataSet

        Session("session_myid") = Session.SessionID

        Try
            If txtusername.Text = "" Then
                Throw New Exception("Please enter your Employee ID")
            End If
            sql = "SELECT a.* , ISNULL(b.emp_code,0) AS authen FROM user_profile a LEFT OUTER JOIN jci_user_authen b ON a.emp_code = b.emp_code "
            sql &= " WHERE a.emp_code = '" & txtusername.Text & "'"
            ds = conn.getDataSet(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then



                Session("jci_emp_code") = txtusername.Text
                Session("jci_user_fullname") = ds.Tables("t1").Rows(0)("user_fullname").ToString
                Session("jci_dept_name") = ds.Tables("t1").Rows(0)("dept_name").ToString
                Session("jci_dept_id") = ds.Tables("t1").Rows(0)("dept_id").ToString
                Session("jci_job_title") = ds.Tables("t1").Rows(0)("job_title").ToString
                Session("jci_job_type") = ds.Tables("t1").Rows(0)("job_type").ToString

                Session("jci_emp_code_admin") = ds.Tables("t1").Rows(0)("authen").ToString
            Else
                lblError.Text = "Not found Employee ID"
                Return
            End If

            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
            Session.Abandon()
            Session.RemoveAll()
            'Response.End()
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

        If lblError.Text = "" Then
            ' Response.Redirect("quiz.html")
            bindGroup()

            If txtgroup.Items.Count > 0 Then
                setRandomQuestion()
                If txtusername.Text = "0" Then
                    sql = "DELETE FROM jci_trans_list WHERE trans_create_by_emp_code = 0 AND game_group_id = " & txtgroup.SelectedValue
                    conn.executeSQL(sql)
                End If
                Response.Redirect("game_actionword1.aspx?gid=" & txtgroup.SelectedValue & "&lang=" & txtlang.SelectedValue)
            Else

                ' lblError.Text = "In this zone, Question Set is not found for Employee ID " & txtusername.Text
                lblError.Text = loginMsg
            End If

        
        End If
    End Sub

    Sub validateTargetGroup()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT *"
        Catch ex As Exception

        End Try
    End Sub

    Sub setRandomQuestion()
        Dim sql As String
        Dim ds As New DataSet
        Dim num As String = ""
        Dim q_pk As String = ""
        Dim limit As String = ""
        Dim time_amount As String = "0"
        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id WHERE a.group_id = " & txtgroup.SelectedValue
            ds = conn.getDataSetForTransaction(sql, "t1")

            num = ds.Tables("t1").Rows(0)("num_question").ToString
            time_amount = ds.Tables("t1").Rows(0)("time_amount_sec").ToString
            Session("time_amount") = time_amount

            sql = "select top " & num & " * from jci_master_question where question_id in "
            sql &= " (select top 60 percent question_id from jci_master_question where isnull(is_delete,0) = 0 and isnull(is_active,0) = 1 and group_id = " & txtgroup.SelectedValue & "  order by newid())"

            ' Response.Write(sql)
            ' Response.End()
            ds = conn.getDataSetForTransaction(sql, "t2")
            For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                q_pk &= limit & ds.Tables("t2").Rows(i)("question_id").ToString
            Next i

            Session("question_index") = "(" & q_pk & ")"
            'Response.Write("<br/>" & q_pk)
            'Response.End()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGroup()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id AND b.is_game = 1 AND ISNULL(a.is_delete,0) = 0 AND category_id = 101 "
            sql &= " WHERE 1 = 1 "
            ' sql &= " AND (ISNULL(group_fix_ip,'') = '' OR group_fix_ip = '" & HttpContext.Current.Request.UserHostAddress & "' )"
            sql &= " AND a.group_id IN (SELECT group_id FROM jci_ip_target WHERE ip_address = '" & HttpContext.Current.Request.UserHostAddress & "')"

            If Not IsNothing(Session("jci_job_title")) Then
                If Session("jci_emp_code").ToString = "0" Then
                    Session("jci_job_title") = "Pharmacist"
                End If

                ' Validate Target group by job title
                sql &= " AND a.group_id IN (SELECT x1.group_id FROM jci_main_target x1 INNER JOIN jci_mapping_target x2 on x1.target_id = x2.target_id WHERE RTRIM(LTRIM(x2.job_title)) = '" & Session("jci_job_title").ToString & "' ) "
                '  Response.Write(sql)
            End If

            If Not IsNothing(Session("jci_job_title")) Then
                If Session("jci_emp_code").ToString = "0" Then

                Else
                    ' ห้ามสอบซ้ำชุดเดิม
                    sql &= " AND a.group_id NOT IN (SELECT x1.game_group_id FROM jci_trans_list x1  WHERE trans_create_by_emp_code = " & Session("jci_emp_code").ToString & "  AND ISNULL(game_group_id,0) > 0 )"

                End If
               
            End If

                ' Validate Zone
                '  sql &= " AND a.zone_no IN (SELECT zone_no FROM jci_mapping_zone x1 INNER JOIN jci_mapping_target x2 ON x1.target_id = x2.target_id WHERE RTRIM(LTRIM(x2.job_title)) = '" & Session("jci_job_title") & "' ) "

                '  Return
                'Response.Write(sql)
                'sql &= " ORDER BY group_name_th "

                ds = conn.getDataSet(sql, "t1")
                txtgroup.DataSource = ds
                txtgroup.DataBind()

                Do While True
                    If Not IsNothing(Session("jci_job_title")) Then
                        sql = "SELECT x1.group_id FROM jci_main_target x1 INNER JOIN jci_mapping_target x2 on x1.target_id = x2.target_id "
                        ' sql &= " INNER JOIN "
                        sql &= " WHERE RTRIM(LTRIM(x2.job_title)) = '" & Session("jci_job_title").ToString & "' "
                        sql &= " AND  x1.group_id IN (SELECT group_id FROM jci_ip_target WHERE ip_address = '" & HttpContext.Current.Request.UserHostAddress & "')"
                        ' Response.Write(sql)
                        ds = conn.getDataSet(sql, "t1")
                        If ds.Tables("t1").Rows.Count = 0 Then
                            loginMsg = "You don't have to answer this quiz"
                            Exit Do
                        End If
                    End If

                    If Not IsNothing(Session("jci_emp_code")) Then

                        sql = "SELECT x1.game_group_id FROM jci_trans_list x1  WHERE trans_create_by_emp_code = " & Session("jci_emp_code").ToString & ""
                        ' Response.Write(sql)
                        'Response.End()
                        ds = conn.getDataSet(sql, "t1")
                        If ds.Tables("t1").Rows.Count > 0 Then
                            loginMsg = "You've completed this quiz"

                        End If

                    End If

                    Exit Do
                Loop

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdLogin1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdLogin1.Click
        authenWithDatabase()
    End Sub
End Class
