Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_popup_score
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
            txtjobtitle.Text = Session("job_title").ToString
            lblJobType.Text = Session("user_position").ToString
            txtname.Text = Session("user_fullname").ToString
            txtdeptname.Text = Session("dept_name").ToString
            txtdatetime.Text = Date.Now
            '  isHasRow("ssip_manager_tab")
            bindDeptAll()

            If cid <> "" Then
                bindDeptSelect()
                bindForm()
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

    Sub isHasRow(ByVal table As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""

        Try
            sql = "SELECT * FROM " & table & " WHERE comment_id = " & cid
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count <= 0 Then
                sql = "INSERT INTO " & table & " (comment_id , ssip_id) VALUES( "
                sql &= "" & cid & " , "
                sql &= "" & id & ""
                sql &= ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & " : " & sql)
                End If
                conn.setDBCommit()
            End If

            ds.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
      

    End Sub

    Sub bindDeptAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            If cid <> "" Then
                sql = "SELECT * from user_dept WHERE dept_id NOT IN (SELECT dept_id FROM ssip_manager_relate_dept WHERE comment_id = " & cid & ") ORDER BY dept_name_en"

            Else
                sql = "SELECT * from user_dept ORDER BY dept_name_en"

            End If
             ds = conn.getDataSetForTransaction(sql, "t1")
            lblDeptAll.DataSource = ds
            lblDeptAll.DataBind()
            'lblDeptAll.Write(sql)

        Catch ex As Exception
            Response.Write(ex.Message & "bindHRRelateDept :: " & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDeptSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , dept_name AS dept_name_en from ssip_manager_relate_dept WHERE comment_id = " & cid & " ORDER BY dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            lblDeptSelect.DataSource = ds
            lblDeptSelect.DataBind()
            'lblDeptAll.Write(sql)

        Catch ex As Exception
            Response.Write(ex.Message & "bindHRRelateDept :: " & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindForm()
        Dim ds As New DataSet
        Dim sql As String

        Try

            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM ssip_manager_tab a INNER JOIN ssip_manager_comment b ON a.comment_id = b.comment_id WHERE a.comment_id = " & cid

            'sql &= " ORDER BY dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            txtcomment_detail.Value = ds.Tables("t1").Rows(0)("detail").ToString

            txtadd_subject.Text = ds.Tables("t1").Rows(0)("subject").ToString

            txtanswer1.SelectedValue = ds.Tables("t1").Rows(0)("q1_type").ToString() 
            txtmgr_reason1.Text = ds.Tables("t1").Rows(0)("q1_reason").ToString()

            txtanswer2.SelectedValue = ds.Tables("t1").Rows(0)("q2_type").ToString()
            txtmgr_reason2.Text = ds.Tables("t1").Rows(0)("q2_reason").ToString()

            txtanswer3.SelectedValue = ds.Tables("t1").Rows(0)("q3_type").ToString()
            txtmgr_reason3.Text = ds.Tables("t1").Rows(0)("q3_reason").ToString()

            txtanswer4.SelectedValue = ds.Tables("t1").Rows(0)("q4_type").ToString()
            txtmgr_reason4.Text = ds.Tables("t1").Rows(0)("q4_reason").ToString()

            txtanswer5.SelectedValue = ds.Tables("t1").Rows(0)("q5_type").ToString()
            txtmgr_reason5.Text = ds.Tables("t1").Rows(0)("q5_reason").ToString()

            txtanswer51.SelectedValue = ds.Tables("t1").Rows(0)("q6_type").ToString()
            txtmgr_reason51.Text = ds.Tables("t1").Rows(0)("q6_reason").ToString()


           

            txtbudget_num.Value = ds.Tables("t1").Rows(0)("q5_budget").ToString()

            For i As Integer = 1 To 6

                If ds.Tables(0).Rows(0)("chk_benefit" & i).ToString() = "1" Then
                    ' Response.Write(ds.Tables(0).Rows(0)("chk_suggest" & i).ToString())
                    CType(panelManager.FindControl("chk_benefit" & i), HtmlInputCheckBox).Checked = True
                Else
                    CType(panelManager.FindControl("chk_benefit" & i), HtmlInputCheckBox).Checked = False
                End If
            Next i

            txtbudget_num.Value = ds.Tables("t1").Rows(0)("budget_num").ToString()
            txtbenefit_num.Value = ds.Tables("t1").Rows(0)("benefit2_num").ToString()
            txtplan.Value = ds.Tables("t1").Rows(0)("plan_detail").ToString()
            txtother.Value = ds.Tables("t1").Rows(0)("other_detail").ToString()

            If ds.Tables("t1").Rows(0)("review_by_empcode").ToString() <> Session("emp_code").ToString Then
                ' panelManager.Enabled = False
                readonlyControl(panelManager)
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            If cid = "" Then
                pk = getPK("comment_id", "ssip_manager_comment", conn)
                sql = "INSERT INTO ssip_manager_comment (comment_id , ssip_id , subject , detail "
                sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
                sql &= ",create_date , create_date_ts"
                sql &= ") VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & id & "' ,"
                sql &= " '" & addslashes(txtadd_subject.Text) & "' ,"
                sql &= " '" & addslashes(txtcomment_detail.Value) & "' ,"
                sql &= " '" & Session("job_title").ToString & "' ,"
                sql &= " '" & Session("user_position").ToString & "' ,"
                sql &= " '" & Session("user_fullname").ToString & "' ,"
                sql &= " '" & Session("emp_code").ToString & "' ,"
                sql &= " '" & Session("dept_name").ToString & "' ,"
                sql &= " '" & Session("costcenter_id").ToString & "' ,"
                sql &= " GETDATE() ,"
                sql &= " '" & Date.Now.Ticks & "' "

                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)

                End If

                cid = pk
                isHasRow("ssip_manager_tab")
            Else
                sql = "UPDATE ssip_manager_comment SET "
                sql &= "  subject = '" & addslashes(txtadd_subject.Text) & "' "
                sql &= " , detail = '" & addslashes(txtcomment_detail.Value) & "' "
                sql &= " WHERE comment_id = " & cid

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)

                End If
            End If



            sql = "UPDATE ssip_manager_tab SET q1_type = '" & txtanswer1.SelectedValue & "' "
            sql &= ", q2_type = '" & txtanswer2.SelectedValue & "' "
            sql &= ", q3_type = '" & txtanswer3.SelectedValue & "' "
            sql &= ", q4_type = '" & txtanswer4.SelectedValue & "' "
            sql &= ", q5_type = '" & txtanswer5.SelectedValue & "' "
            sql &= ", q6_type = '" & txtanswer51.SelectedValue & "' "

            sql &= ", q1_reason = '" & addslashes(txtmgr_reason1.Text) & "' "
            sql &= ", q2_reason = '" & addslashes(txtmgr_reason2.Text) & "' "
            sql &= ", q3_reason = '" & addslashes(txtmgr_reason3.Text) & "' "
            sql &= ", q4_reason = '" & addslashes(txtmgr_reason4.Text) & "' "
            sql &= ", q5_reason = '" & addslashes(txtmgr_reason5.Text) & "' "
            sql &= ", q6_reason = '" & addslashes(txtmgr_reason51.Text) & "' "

            For i As Integer = 1 To 6
                If CType(panelManager.FindControl("chk_benefit" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= ", chk_benefit" & i & " = 1 "
                End If
            Next i

            sql &= " , budget_num = '" & txtbudget_num.Value & "' "
            sql &= " , benefit2_num = '" & txtbenefit_num.Value & "' "
            sql &= " , plan_detail = '" & txtplan.Value & "' "
            sql &= " , other_detail = '" & txtother.Value & "' "

            sql &= " WHERE comment_id = " & cid

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If


            sql = "DELETE FROM ssip_manager_relate_dept WHERE comment_id = " & cid
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If

            For i As Integer = 0 To lblDeptSelect.Items.Count - 1
                pk = getPK("mgr_relate_dept_id", "ssip_manager_relate_dept", conn)
                sql = "INSERT INTO ssip_manager_relate_dept (mgr_relate_dept_id , comment_id , ssip_id , dept_id , dept_name) VALUES("
                sql &= "" & pk & " ,"
                sql &= "" & cid & " ,"
                sql &= "" & id & " ,"
                sql &= "" & lblDeptSelect.Items(i).Value & " ,"
                sql &= "'" & addslashes(lblDeptSelect.Items(i).Text) & "' "
                sql &= ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & " : " & sql)
                End If
            Next i

            conn.setDBCommit()

            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; window.close();"
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try

    End Sub

    Protected Sub cmdAddDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddDept.Click
        While lblDeptAll.Items.Count > 0 AndAlso lblDeptAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblDeptAll.SelectedItem
            selectedItem.Selected = False
            lblDeptSelect.Items.Add(selectedItem)
            lblDeptAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveDept.Click
        While lblDeptSelect.Items.Count > 0 AndAlso lblDeptSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblDeptSelect.SelectedItem
            selectedItem.Selected = False
            lblDeptAll.Items.Add(selectedItem)
            lblDeptSelect.Items.Remove(selectedItem)
        End While
    End Sub
End Class
