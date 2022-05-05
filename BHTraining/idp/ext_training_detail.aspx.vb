Imports System.Data

Imports System.IO
Imports ShareFunction
Imports System.Net.Mail
Imports QueryStringEncryption

Partial Class idp_ext_training_detail
    Inherits System.Web.UI.Page
    Protected session_id As String
    Protected mode As String = ""
    Protected id As String
    Protected idp_status As String = ""
    Protected new_idp_id As String
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected priceTotal As Decimal = 0
    Protected budgetTotal As Decimal = 0
    Protected priceTotal2 As Decimal = 0
    Protected budgetTotal2 As Decimal = 0
    Protected returnTotal As Decimal = 0
    Dim priv_list() As String
    Protected viewtype As String = ""
    Protected global_idp_no As String = ""
    Protected lang As String = "th"
    Protected req As String = "ext"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If
        '  Response.Write(Session("req").ToString)
        session_id = Session("session_myid").ToString
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        lang = Request.QueryString("lang")
        If lang = "" Then
            lang = "th"
        End If
        If id = "" Then
            id = 0
        End If

        If lang = "th" Then
            cmdThai.Enabled = False
        Else
            cmdEng.Enabled = False
            lblIDPTitle.Text = "Training Topic"
            lblIDPJoin.Text = "Attendance type"
            lblIDPMotivation.Text = "My motivation for developing the particular knowledge"
            lblIDPMotivate1.Text = "This training/development topic is specified in my IDP"
            lblIDPMotivate2.Text = "To fulfill the requirement in shifting my career ladder from level"
            lblIDPMotivate3.Text = "I have received feedback that I need to develop this skill from My supervisor or manager"
            lblIDPMotivate4.Text = "I have received feedback that I need to develop this skill from My personal skill assessment"
            lblIDPMotivate5.Text = "I want to prepare for a promotion or other career opportunity, and I know this skill will help me."
            lblIDPMotivate6.Text = "The organization has identified this as an essential skill."
            lblIDPMotivate7.Text = "Other"
            lblIDPResv1.Text = "Reservation made by staff"
            lblIDPResv2.Text = "Sent reservation form to Employee Training and Development Unit to make reservation within"
            lblIDPResv3.Text = "Employee Training and Development Unit make an online reservation within"
            lblIDPResvTopic.Text = "Reservation"
            lblIDPTrainHour.Text = "Training Hour"
            lblIDPDate.Text = "Training date"
            lblIDPPlace.Text = "Province/Country"
            lblIDPInstitute.Text = "Institute"
            lblIDPTrainType.Text = "Training type"
            lblIDPFacility.Text = "Meeting venue"
            lblIDPExpect.Text = "This development is expected to support which individual or departmental goal (can select > 1)"

            lblIDPCourse.Text = "Course Outline"
            lblIDPCourse2.Text = "Course Outline"
            lblIDPFile.Text = "File Attachment"
            lblIDPFile2.Text = "File Attachment"

            lblIDPTopic2.Text = "Participants"
            lblcontact.Text = "Contact Person"
            lblCategory.Text = "Category"

            lblIDPBudget.Text = "Request Budget from hospital"
            txtbudgetRequest.Items(0).Text = "Yes"
            txtbudgetRequest.Items(1).Text = "No"
            chk_expect1.Text = "support career ladder adjustment plan from level"
            lblexpect_level1_1.Text = "to level"
            chk_expect2.Text = "improve my performance appraisal rating"
            chk_expect3.Text = "apply new knowledge and skill to support departmental goal achievement in"
            chk_expect4.Text = "support hospital branding by exhibit work / research"
            chk_expect5.Text = "develop new business or service per departmental or hospital goals"
            chk_expect6.Text = "other reason"

            lblAttach1.Text = "** Please send the documents to Learning and Development if can’t attach."
            lblActionAttach1.Text = "Attach Certificate File  (Optional)"
        End If


        priv_list = Session("priv_list")

        viewtype = Session("viewtype").ToString
        ' Response.Write("xxxxxxxxxxxxxxxxxxxxxx viewtype = " & viewtype)
        If Request.QueryString("req") <> "" Then
            req = Request.QueryString("req")
            Session("req") = req
            ' Response.Write(1)
        Else
            req = Session("req").ToString
            'Response.Write(2)
        End If


        lblViewtype.Text = viewtype

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        cmdPreview.Attributes.Add("onclick", "window.open('preview_ext_training.aspx?id=" & id & "');return false;")

        If IsPostBack Then

        Else ' Load first time
            'Response.Write("xx" & viewtype)

            tab_goal.Visible = False
            cmdSaveGoal.Visible = False
            panel_reply.Enabled = False
            txtdate11.Attributes.Add("readonly", "readonly")
            txtdate2.Attributes.Add("readonly", "readonly")
            txtdate_register.Attributes.Add("readonly", "readonly")


            If viewtype = "" Then
                tab_trd.Visible = False
                email_alert.Visible = False
                tabMailLog.Visible = False
                '  tab_account_expense.Visible = False
                panel_actual_expense.Visible = False
                panel_update_expense.Enabled = False
                cmdUpdateStatus.Visible = False
                txtstatus.Enabled = False
            ElseIf viewtype = "dept" Then
                email_alert.Visible = False
                tabMailLog.Visible = False
                tab_trd.Visible = False
                'tab_account_expense.Visible = False
                panel_actual_expense.Visible = False
                panel_update_expense.Enabled = False
                txtstatus.Enabled = False
                cmdUpdateStatus.Visible = False
            ElseIf viewtype = "hr" Then
                email_alert.Visible = True
                tabMailLog.Visible = True
                tab_trd.Visible = True
                txtstatus.Enabled = True
                cmdUpdateStatus.Visible = True

                'panel_detail.Enabled = False
                ' readonlyControl(panel_detail)

                '  panel_expense.Enabled = False
                panel_actual_expense.Visible = False
                panel_update_expense.Enabled = False
                panel_reply.Enabled = True
            ElseIf viewtype = "budget" Then
                email_alert.Visible = False
                tabMailLog.Visible = False
                txtstatus.Enabled = False
                cmdUpdateStatus.Visible = False
                div_budget.Enabled = True
                tab_trd.Visible = False
            ElseIf viewtype = "expense" Then
                email_alert.Visible = False
                tabMailLog.Visible = False
                txtstatus.Enabled = False
                cmdUpdateStatus.Visible = False
                tab_trd.Visible = False
            End If

            If req = "int" And viewtype <> "hr" Then
                txtstatus.Enabled = False
                cmdUpdateStatus.Visible = False
            End If

            bindRequestType()
            bindExpenseTpye() ' ประเภทค่าใช้จ่าย
            bindExpense()

            If req = "ext" Then
                bindIDPCombo()
                bindCategoryCombo()
                bindExpectExternalCombo()



                lblHeader.Text = "External Training Request"
                tab_int_training.Visible = False
                tab_int_document.Visible = False
                tab_int_schedule.Visible = False
                tab_int_speaker.Visible = False

            Else
                cmdPreview.Visible = False
                lblHeader.Text = "Internal Training Request"
                tab_ext_training.Visible = False
                tab_goal.Visible = False
                bindComboIDPTemplate()
                bindEmployeeAll()
                bindEmployeeSelect()
                bindJobTypeAll()
                bindJobTypeSelect()
                bindJobTitleAll()
                bindJobTitleSelect()
                bindCCAll()
                bindCCSelect()
                bindMethodCombo()
                bindCategoryCombo()
                bindExpectCombo()
                bindFileTrainingDocInternal() ' Interanl Training Document
                bindInternalRelateIDP()
                bindSpeaker()
                bindSchedule()
                bindFileInternal()
                bindRoom()
            End If

            If mode = "edit" Then
                cmdSubmit1.Enabled = True
                cmdSubmit2.Enabled = True
                bindStatus()
                bindGridIDPLog()
                bindGridAlertLog()
                bindCommentList()
                bindDetail()
                bindBudgetMaster() ' Budget ของแต่ละแผนก ที่ลงไว้ตั้งแต่ต้นปี
                bindFile()
                bindGridInformationUpdate()


                If CInt(txtstatus.SelectedValue) > 1 Then
                    If viewtype <> "" Then
                        'cmdSaveGoal.Visible = False
                    Else
                        '   cmdSaveGoal.Visible = True
                        ' cmdSaveGoal.Visible = False
                    End If

                    If req = "int" Then ' Internal Training
                        ' ถ้าเป็น Internal Req  ให้ training แก้ได้
                        If viewtype = "hr" Then
                            cmdSaveDraft1.Text = "Save"
                            cmdSaveDraft2.Text = "Save"
                            cmdSaveDraft1.Visible = True
                            cmdSubmit1.Visible = False
                            cmdSaveDraft2.Visible = True
                            cmdSubmit2.Visible = False
                        End If

                        ' readonlyControl(panel_internal_detail)
                        If viewtype = "educator" Or viewtype = "hr" Then
                            panel_schedule_add.Visible = True
                            panel_speaker_add.Visible = True
                            If viewtype = "educator" Then
                                txtstatus.Enabled = False
                                cmdUpdateStatus.Visible = False
                            End If
                        End If

                        cmdSubmit1.Visible = False
                        cmdSubmit2.Visible = False

                    Else

                        cmdSaveDraft1.Visible = False
                        cmdSubmit1.Visible = False
                        cmdSaveDraft2.Visible = False
                        cmdSubmit2.Visible = False
                        readonlyControl(panel_internal_detail)
                        panel_schedule_add.Visible = False
                        panel_speaker_add.Visible = False
                        ' tab_goal.Visible = False
                    End If
                    readonlyControl(panel_detail)
                    cmdUpload.Attributes.Remove("disabled")
                    cmdDeleteFile.Attributes.Remove("disabled")
                    cmdUpload.Enabled = True
                    cmdDeleteFile.Enabled = True
                Else
                    cmdSaveGoal.Visible = False
                    tab_goal.Visible = False
                End If


                bindGoal()
                bindCommentList()
                getTotalApproveBudget() ' สรุปจำนวนเงินที่อนุมัติทั้งหมด
                bindExpense()
                bindExpense2()
                bindFileExpense() ' ไฟล์ของ Acutual expense

                Dim IDPApproveLevel As String = geIDPApproveLevel()


                If viewtype = "hr" Or viewtype = "" Then
                    panelAddComment.Visible = False
                    bindFileTRD()
                End If

                If CInt(getMyIPDLevel()) > CInt(IDPApproveLevel) Or CInt(getMyIPDLevel()) = 17 Then
                    lblMsg.Text = "Add/Edit Approval and Comment"
                    lblMsg.ForeColor = Drawing.Color.Green
                    If viewtype = "dept" Then
                        If CInt(IDPApproveLevel) = 17 And CInt(getMyIPDLevel()) = 23 Then  ' For IDP 4s
                            panelAddComment.Visible = False
                            lblMsg.Text = "View Only"
                            lblMsg.ForeColor = Drawing.Color.Red
                        Else
                            panelAddComment.Visible = True
                        End If

                    End If

                    If lblempcode.Text = Session("emp_code").ToString Then
                        panelAddComment.Visible = False
                    End If

                Else
                    lblMsg.Text = "View Only"
                    lblMsg.ForeColor = Drawing.Color.Red
                    panelAddComment.Visible = False
                End If

            ElseIf mode = "add" Then
                cmdSubmit1.Enabled = False
                cmdSubmit2.Enabled = False
                cmdPreview.Visible = False
                txtbudgetRequest.SelectedValue = 1
                cmdSaveGoal.Visible = False
                tab_trd.Visible = False
                tab_approve.Visible = False
                tab_update.Visible = False
                tab_account_expense.Visible = False
                tab_goal.Visible = False
                If viewtype = "" Then
                    lblempcode.Text = Session("emp_code").ToString
                    lblDept.Text = Session("dept_name").ToString
                    'lblDivision.Text = Session("job_title").ToString
                    lblrequest_NO.Text = ""
                    lblname.Text = Session("user_fullname").ToString
                    lbljobtitle.Text = Session("job_title").ToString
                    lblCostcenter.Text = Session("costcenter_id").ToString

                    bindBudgetMaster() ' Budget ของแต่ละแผนก ที่ลงไว้ตั้งแต่ต้นปี
                End If

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

  

    Function geIDPApproveLevel() As String
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = "0"
        Try
            sql = "SELECT * FROM user_role a INNER JOIN m_role b ON a.role_id = b.role_id WHERE a.role_id IN (12,13,14,15,16,17,23) AND a.emp_code IN (SELECT report_emp_code FROM idp_trans_list WHERE idp_id = " & id & ")"
            sql &= " ORDER BY a.role_id DESC"
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                result = ds.Tables("t1").Rows(0)("role_id").ToString
                lblEmpLevel.Text = ds.Tables("t1").Rows(0)("role_name").ToString
            Else
                lblEmpLevel.Text = "General Employee"
                result = "12"
            End If
        Catch ex As Exception
            ' Response.Write(ex.Message)
            result = "0"
        End Try
        Return result
    End Function

    Function getMyIPDLevel() As String ' Level ของผู้ login
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = "0"
        Try
            sql = "SELECT * FROM user_role a INNER JOIN m_role b ON a.role_id = b.role_id WHERE a.role_id IN (12,13,14,15,16,17,23) AND a.emp_code = " & Session("emp_code").ToString
            sql &= " ORDER BY a.role_id DESC"
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                result = ds.Tables("t1").Rows(0)("role_id").ToString
                lblYourLevel.Text = ds.Tables("t1").Rows(0)("role_name").ToString
            Else
                lblYourLevel.Text = "General Employee"
                ' result = "12"
            End If
        Catch ex As Exception
            result = "0"
        End Try
        Return result
    End Function

    Private Sub bindComboIDPTemplate()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sql = "SELECT *  FROM idp_template_master WHERE ISNULL(is_delete,0) = 0 AND is_active = 1 AND ISNULL(is_ladder_template,0) = 0 ORDER BY template_title"
            ds = conn.getDataSetForTransaction(sql, "table1")

            'Response.Write(sqlB.ToString)
            txtidp_relate.DataSource = ds
            txtidp_relate.DataBind()

            txtidp_relate.Items.Insert(0, New ListItem("-- Please Select --", ""))
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try


    End Sub
    Private Sub bindGridIDPLog()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * , ISNULL(b.status_name, a.log_remark) AS idp_status_name FROM idp_status_log a LEFT OUTER JOIN idp_status_list b ON a.status_id = b.idp_status_id WHERE a.idp_id = " & id)
            sqlB.Append(" ORDER BY log_time ASC")
            ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

            'Response.Write(sqlB.ToString)
            GridviewIDPLog.DataSource = ds
            GridviewIDPLog.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub

    Sub bindStatus()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM idp_status_list WHERE 1 = 1 "

            'sql &= " ORDER BY dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            txtstatus.DataSource = ds
            txtstatus.DataBind()

            txtstatus.Items.Insert(0, New ListItem("--All Status--", ""))

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindNewGoalCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_expect WHERE"
            txtnewgoal.DataSource = ds
            txtnewgoal.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Sub bindDetail()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_external_req a INNER JOIN idp_trans_list b ON a.idp_id = b.idp_id WHERE a.idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtcontact.Text = ds.Tables("t1").Rows(0)("contact_person").ToString
            lblrequest_NO.Text = ds.Tables("t1").Rows(0)("idp_no").ToString
            txtstatus.SelectedValue = ds.Tables("t1").Rows(0)("status_id").ToString
            txtrelate_idpno.SelectedValue = ds.Tables("t1").Rows(0)("relate_idp_no").ToString
            txttitle.Value = ds.Tables("t1").Rows(0)("ext_title").ToString
            txtcourse_detail.Value = ds.Tables("t1").Rows(0)("course_outline").ToString

            Try
                txtexpect_detail.SelectedValue = ds.Tables("t1").Rows(0)("expect_detail").ToString
            Catch ex As Exception

            End Try


            lblExpectLabel.Text = ds.Tables("t1").Rows(0)("expect_detail").ToString
            txtfacility.Value = ds.Tables("t1").Rows(0)("facility").ToString
            txtinstitution.Value = ds.Tables("t1").Rows(0)("institution").ToString
            txtplace.Text = ds.Tables("t1").Rows(0)("place").ToString

            txttype.SelectedValue = ds.Tables("t1").Rows(0)("ext_type_id").ToString
            txttraintype.SelectedValue = ds.Tables("t1").Rows(0)("training_type_id").ToString

            txtdate11.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_start_ts").ToString)
            txtdate2.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_end_ts").ToString)
            txthour1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_start_ts").ToString, "hour")
            txtmin1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_start_ts").ToString, "min")
            txthour2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_end_ts").ToString, "hour")
            txtmin2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_end_ts").ToString, "min")

            txtladder1.SelectedValue = ds.Tables("t1").Rows(0)("chk_attend2_ladder_before").ToString
            txtladder2.SelectedValue = ds.Tables("t1").Rows(0)("chk_attend2_ladder_after").ToString
            txtattend_remark.Value = ds.Tables("t1").Rows(0)("chk_attend7_remark").ToString

            For i As Integer = 1 To 7
                If ds.Tables("t1").Rows(0)("chk_attend" & i).ToString = "1" Then
                    CType(panel_detail.FindControl("chk_attend" & i), HtmlInputCheckBox).Checked = True
                End If
            Next i

            For i As Integer = 1 To 3
                If ds.Tables("t1").Rows(0)("register_type").ToString = i.ToString Then
                    CType(panel_detail.FindControl("txtr" & i), RadioButton).Checked = True
                End If
            Next i

            txtdate_register.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("register_type_reserve_date_ts").ToString)
            txtdate_register2.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("register_type_reserve_date_ts2").ToString)

            txttrd_note.Value = ds.Tables("t1").Rows(0)("trd_note").ToString

            lblempcode.Text = ds.Tables(0).Rows(0)("report_emp_code").ToString
            lblDept.Text = ds.Tables(0).Rows(0)("report_dept_name").ToString
            ' lblDivision.Text = ds.Tables(0).Rows(0)("report_jobtype").ToString ' replace by job_type

            lblname.Text = ds.Tables(0).Rows(0)("report_by").ToString
            lbljobtitle.Text = ds.Tables(0).Rows(0)("report_jobtitle").ToString
            lblCostcenter.Text = ds.Tables(0).Rows(0)("report_dept_id").ToString

            txtreply.Text = ds.Tables(0).Rows(0)("reply_note").ToString
            lblreply_by.Text = ds.Tables(0).Rows(0)("reply_by").ToString
            lblLastUpdate.Text = ds.Tables(0).Rows(0)("reply_date").ToString
            lblUpdateBy.Text = ds.Tables(0).Rows(0)("budget_update_by").ToString & " , " & ds.Tables(0).Rows(0)("budget_last_update").ToString
            lblUpdateby2.Text = ds.Tables(0).Rows(0)("expense_update_by").ToString & " , " & ds.Tables(0).Rows(0)("expense_last_update").ToString
            lblApproveName.Text = ds.Tables(0).Rows(0)("budget_update_by").ToString & " , " & ds.Tables(0).Rows(0)("budget_last_update").ToString

            txthour.Value = ds.Tables(0).Rows(0)("train_hour").ToString
            txtinstitution.Value = ds.Tables(0).Rows(0)("institution").ToString

            If ds.Tables(0).Rows(0)("is_budget_approve").ToString <> "" Then
                txtbudget_status.SelectedValue = ds.Tables(0).Rows(0)("is_budget_approve").ToString
                '   Response.Write(22)
                If txtbudget_status.SelectedValue = "1" Then ' Approve
                    ' div_budget.Enabled = False
                    panel_add_expense.Visible = False ' ถ้า Approve แล้ว key ขอเบิกเพิ่มไม่ได้

                    tab_account_expense.Visible = True
                Else
                    If viewtype = "dept" Then
                        panel_add_expense.Visible = False ' ห้ามเพิ่ม budget
                    Else
                        panel_add_expense.Visible = True
                    End If
                    tab_account_expense.Visible = False
                    panel_actual_expense.Visible = False
                End If
            Else '  ยังไม่ approve
                'Response.Write(0)
                If viewtype = "dept" Then
                    panel_add_expense.Visible = False ' ห้ามเพิ่ม budget
                Else
                    panel_add_expense.Visible = True
                End If

                tab_account_expense.Visible = False
                panel_actual_expense.Visible = False
            End If

          
            If viewtype = "" And (CInt(txtstatus.SelectedValue) <> "1") Then
                panel_add_expense.Visible = False ' ห้ามเพิ่ม budget
            End If

            txtbudget_remark.Text = ds.Tables(0).Rows(0)("budget_remark").ToString

            txtexpense_status.SelectedValue = ds.Tables(0).Rows(0)("is_expense_approve").ToString
            txtexpense_remark.Text = ds.Tables(0).Rows(0)("expense_remark").ToString

            txtint_topic.Value = ds.Tables(0).Rows(0)("internal_title").ToString
            txtint_outline.Value = ds.Tables(0).Rows(0)("internal_outline").ToString
            Try
                txtidp_relate.SelectedValue = ds.Tables(0).Rows(0)("internal_idp_program_id").ToString
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

            If ds.Tables(0).Rows(0)("internal_target_group_id").ToString = "1" Then
                txttarget1.Checked = True
            ElseIf ds.Tables(0).Rows(0)("internal_target_group_id").ToString = "2" Then
                txttarget2.Checked = True
            ElseIf ds.Tables(0).Rows(0)("internal_target_group_id").ToString = "3" Then
                txttarget3.Checked = True
            ElseIf ds.Tables(0).Rows(0)("internal_target_group_id").ToString = "4" Then
                txttarget4.Checked = True
            End If

            lblGoalTitle.Text = ds.Tables(0).Rows(0)("ext_title").ToString
            lblGoalFacility.Text = ds.Tables(0).Rows(0)("facility").ToString
            lblGoalTrainDate.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_start_ts").ToString)
            lblGoalType.Text = ds.Tables(0).Rows(0)("training_type_name").ToString

            If ds.Tables(0).Rows(0)("has_certificate").ToString = "1" Then
                chk_certificate.Checked = True
            Else
                chk_certificate.Checked = False
            End If
            txtcertificate_remark.Text = addslashes(ds.Tables(0).Rows(0)("certificate_detail").ToString)

            txtgoal_level.SelectedValue = ds.Tables(0).Rows(0)("goal_skill_level").ToString
            txtgoal_level_after.SelectedValue = ds.Tables(0).Rows(0)("goal_skill_level_aftertraining").ToString
            txtgoal_benefit.Value = ds.Tables(0).Rows(0)("goal_benefit").ToString
            txtgoal_problem.Text = ds.Tables(0).Rows(0)("goal_problem").ToString

            If ds.Tables(0).Rows(0)("chk_kpi1").ToString = "1" Then
                ch_kpi1.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_kpi2").ToString = "1" Then
                ch_kpi2.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_kpi3").ToString = "1" Then
                ch_kpi3.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_kpi4").ToString = "1" Then
                ch_kpi4.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_kpi5").ToString = "1" Then
                ch_kpi5.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_kpi6").ToString = "1" Then
                ch_kpi6.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_kpi7").ToString = "1" Then
                ch_kpi7.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_kpi8").ToString = "1" Then
                ch_kpi8.Checked = True
            End If

            txtgoal_ladder1.SelectedValue = ds.Tables(0).Rows(0)("chk_kpi1_ladder1").ToString
            txtgoal_ladder2.SelectedValue = ds.Tables(0).Rows(0)("chk_kpi1_ladder2").ToString
            '  txtgoal_kpi2.Text = ds.Tables(0).Rows(0)("chk_kpi2_remark").ToString
            txtgoal_kpi2_scope1.SelectedValue = ds.Tables(0).Rows(0)("chk_kpi2_scope1").ToString
            txtgoal_kpi2_scope2.SelectedValue = ds.Tables(0).Rows(0)("chk_kpi2_scope2").ToString
           
            txtgoal_kpi3.Text = ds.Tables(0).Rows(0)("chk_kpi3_remark").ToString
            txtgoal_kpi4.Text = ds.Tables(0).Rows(0)("chk_kpi4_remark").ToString
            txtgoal_kpi5.Text = ds.Tables(0).Rows(0)("chk_kpi5_remark").ToString
            txtgoal_kpi6.Text = ds.Tables(0).Rows(0)("chk_kpi6_remark").ToString
            txtgoal_kpi7.Text = ds.Tables(0).Rows(0)("chk_kpi7_remark").ToString
            txtgoal_kpi8.Text = ds.Tables(0).Rows(0)("chk_kpi8_remark").ToString

            Dim finish_date As Long = 0
            If ds.Tables("t1").Rows(0)("date_end_ts").ToString <> "" Then
                finish_date = CLng(ds.Tables("t1").Rows(0)("date_end_ts").ToString)
                If Date.Now.Ticks > finish_date And req = "ext" Then
                    tab_goal.Visible = True
                    If viewtype = "" Then
                        cmdSaveGoal.Visible = True
                    End If

                End If
            End If

            Try
                txtbudgetRequest.SelectedValue = ds.Tables(0).Rows(0)("is_budget_request").ToString
            Catch ex As Exception

            End Try


            If ds.Tables(0).Rows(0)("chk_ext_expect1").ToString = "1" Then
                chk_expect1.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_ext_expect2").ToString = "1" Then
                chk_expect2.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_ext_expect3").ToString = "1" Then
                chk_expect3.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_ext_expect4").ToString = "1" Then
                chk_expect4.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_ext_expect5").ToString = "1" Then
                chk_expect5.Checked = True
            End If

            If ds.Tables(0).Rows(0)("chk_ext_expect6").ToString = "1" Then
                chk_expect6.Checked = True
            End If

            Try
                txtnewgoal.SelectedValue = ds.Tables(0).Rows(0)("combo_ext_expect3_id").ToString
            Catch ex As Exception

            End Try

            Try
                txtexpect_level1.SelectedValue = ds.Tables(0).Rows(0)("combo_expect_level1").ToString
            Catch ex As Exception

            End Try

            Try
                txtexpect_level2.SelectedValue = ds.Tables(0).Rows(0)("combo_expect_level2").ToString
            Catch ex As Exception

            End Try

            txtexpect_goal_other.Text = ds.Tables(0).Rows(0)("chk_ext_expect6_text").ToString

            Try
                txtadd_extcat.SelectedValue = ds.Tables(0).Rows(0)("ext_category_id").ToString
            Catch ex As Exception

            End Try

            lblCerFile.Text = "<a target='_blank' href='../share/idp/attach_file/" & ds.Tables(0).Rows(0)("cer_file_path").ToString & "'>" & ds.Tables(0).Rows(0)("cer_file_name").ToString & "</a>"
        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCommentList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count = 0 And viewtype = "" Then
                tab_approve.Visible = False
            ElseIf viewtype = "dept" Then
                tab_approve.Visible = True
            End If

            GridComment.DataSource = ds
            GridComment.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindExpense()
        Dim sql As String
        Dim ds As New DataSet

        Try
            priceTotal = 0
            budgetTotal = 0

            sql = "SELECT * FROM idp_training_expense a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE a.accouting_type = 1  "
            sql &= " AND ISNULL(a.is_delete,0) = 0 "
            If mode = "add" Then
                sql &= " AND a.session_id = '" & Session.SessionID & "' "
            Else
                sql &= " AND a.idp_id = '" & id & "' "
            End If
            sql &= " ORDER BY a.order_sort "
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            If ds.Tables("t1").Rows.Count = 0 Then
                cmdSaveOrder.Visible = False
                cmdDelExpense.Visible = False
            Else
                cmdSaveOrder.Visible = True
                cmdDelExpense.Visible = True
            End If

            lblcurrency0.Text = txtcurrency.SelectedItem.Text

            GridExpense.DataSource = ds
            GridExpense.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindExpense2()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_expense  a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE  a.accouting_type = 2 "
            sql &= " AND a.idp_id = '" & id & "' "

            sql &= " ORDER BY a.order_sort "
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count = 0 Then
                '  cmdSaveOrder2.Visible = False
                ' cmdDelExpense.Visible = False
            End If

            GridExpense2.DataSource = ds
            GridExpense2.DataBind()
            lblcurrency.Text = txtcurrency2.SelectedItem.Text

            If CDbl(txtactual_expense.Text) > CDbl(lblApproveBudget.Text) Then
                txtactual_expense.ForeColor = Drawing.Color.Red
            Else
                txtactual_expense.ForeColor = Drawing.Color.Green
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub onDeleteComment(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "DELETE FROM idp_manager_comment WHERE comment_id = " & e.CommandArgument.ToString
            errorMsg = conn.executeSQLForTransaction(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If
            updateOnlyLog("0", "Delete Comment")
            conn.setDBCommit()

            bindCommentList()
            bindGridIDPLog()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try

    End Sub

    Sub isHasRow(ByVal table As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""

        sql = "SELECT * FROM " & table & " WHERE idp_id = " & id
        ' Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage)
        End If

        If ds.Tables("t1").Rows.Count <= 0 Then
            sql = "INSERT INTO " & table & " (idp_id) VALUES( "
            sql &= "" & id & ""
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If
        End If

    End Sub

    Protected Sub onSave(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            updateTransList(e.CommandArgument.ToString)

            If txtadd_expense_type.SelectedValue <> "" Then ' Automatic Save Expense
                addExpenseNoCommit()
            End If

            saveBudgetOrderNoCommit()

            isHasRow("idp_external_req")

            If mode = "add" Then
                updateExpense()
                updateFileInternalTraining()
                updateFileTraining()
                updateSchedule()
                updateSpeaker()
                updateIDPInternal()
            Else
                updateExpenseModeEdit()
            End If



            If req = "ext" Then
                updateDetail()
            ElseIf req = "int" Then
                updateDetailInternalRequest()
            End If
            

            conn.setDBCommit()


            If e.CommandArgument.ToString = "" Then

            ElseIf e.CommandArgument.ToString = "2" Then ' Submit
                loopMail(global_idp_no) ' Send mail to manager
                Response.Redirect("ext_training_list.aspx?req=" & req)
            Else
                Response.Redirect("ext_training_detail.aspx?mode=edit&id=" & id)
            End If


        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("onSave : " & ex.Message)
            'Response.End()
        End Try
    End Sub

    Sub loopMail(ByVal training_no As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim msgBody As String = ""
        Dim key As String = ""
        Try
            key = UserActivation.GetActivationLink("idp/ext_training_detail.aspx?mode=edit&id=" & id & "&req=" & req)
            msgBody = "<strong>" & txttitle.Value & "</strong><br/><br/>"
            msgBody &= "Dear Team, <br/><br/>" & vbCrLf
            If req = "ext" Then
                msgBody &= "The external training request is submitted for your approval. Please kindly open the following link for fast access.<br/>" & vbCrLf
            ElseIf req = "int" Then
                msgBody &= "The internal training request is submitted for your approval. Please kindly open the following link for fast access.<br/>" & vbCrLf
            End If

            msgBody &= "<a href='http://bhtraining/login.aspx?viewtype=dept&req=" & req & "&key=" & key & "'>External training request Online</a>"
            msgBody &= "<br/> Best regard, <br/> Training Group"
            msgBody &= "<br/>Phone 73029,72027,72023,73009"

            sql = "select emp_code from user_role where emp_code = " & Session("emp_code").ToString & " and role_id IN (14 , 15 ,16 ,17 )"
            ds = conn.getDataSetForTransaction(sql, "t0")
            ' ถ้าคนขอ เป็น mgr , dept. mrg , dd ,cc ไม่ส่งเมล์
            If ds.Tables("t0").Rows.Count = 0 Then
                ' ส่งเมล์ให้เฉพาะ Mgr
                sql = "SELECT * FROM user_profile a INNER JOIN user_access_costcenter_idp b ON a.emp_code = b.emp_code WHERE b.costcenter_id  = " & Session("dept_id").ToString
                sql &= " AND ISNULL(a.custom_user_email,'') <> ''  "
                sql &= "  AND a.emp_code in (select emp_code from user_role where role_id = 14 )"

                '  sql &= " AND a.emp_code NOT IN (select emp_code from user_role where emp_code = " & Session("emp_code").ToString & " and role_id IN (14 , 15 ,16 ,17 ))"
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    sendMail(ds.Tables("t1").Rows(i)("custom_user_email").ToString, "#" & training_no & " " & ds.Tables("t1").Rows(i)("user_fullname").ToString & " Please review external training request", msgBody)
                Next i
            End If

        Catch ex As Exception
            Response.Write("send mail :: " & ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub sendMail(ByVal email As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Try
            'ConfigurationManager.AppSettings("database").ToString()
            'oMsg.From = New MailAddress("traininggroup@bumrungrad.com")
            oMsg.From = New MailAddress("BumrungradPersonnelDevelopmentCenter@bumrungrad.com")
            oMsg.To.Add(New MailAddress(email))
            oMsg.IsBodyHtml = True
            ' oMsg.CC.Add(New MailAddress(email))
            oMsg.Subject = subject
            oMsg.Body = message
            Dim smtp As New SmtpClient("mail.bumrungrad.com")
            'Dim smtp As New SmtpClient("mail.powerpointproduct.com")
            ' SMTP Authenticate
            'smtp.Credentials = New System.Net.NetworkCredential("info@powerpointproduct.com", "natee")
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.Send(oMsg)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            oMsg = Nothing
        End Try


    End Sub

    Sub updateTransList(ByVal status_id As String, Optional ByVal log_remark As String = "")
        Dim sql As String
        Dim sql2 As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim new_idp_no As String = ""

        If mode = "add" Then
            Try
                sql = "SELECT ISNULL(MAX(idp_id),0) + 1 AS pk FROM idp_trans_list"
                ds = conn.getDataSetForTransaction(sql, "t1")
                pk = ds.Tables("t1").Rows(0)(0).ToString
                new_idp_id = pk
            Catch ex As Exception
                Response.Write(ex.Message)
                Response.Write(sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try


            sql = "INSERT INTO idp_trans_list (idp_id , idp_no , idp_runno , idp_yearno , date_submit , date_submit_ts , create_date  , status_id , report_dept_id , report_dept_name ,  report_by , report_emp_code , report_jobtitle , report_jobtype)"
            sql &= " VALUES("
            sql &= "" & pk & " ,"

            If status_id = "2" Then ' Submitted
                Dim y As String
                Dim runno As String

                ' sql2 = "SELECT * FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString & " ORDER BY idp_runno DESC , idp_yearno DESC "
                sql2 = "SELECT * FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString & " ORDER BY idp_yearno DESC , idp_runno DESC  "
                ds = conn.getDataSetForTransaction(sql2, "t1")
                'Response.Write(sql2)
                'Response.End()
                Try
                    y = ds.Tables("t1").Rows(0)("idp_yearno").ToString
                Catch ex As Exception
                    y = Date.Now.Year.ToString
                End Try

                If y <> Date.Now.Year.ToString Then
                    y = Date.Now.Year.ToString
                    runno = "1"
                Else
                    Try
                        runno = CInt(ds.Tables("t1").Rows(0)("idp_runno").ToString) + 1
                    Catch ex As Exception
                        runno = 1

                    End Try

                End If

                new_idp_no = y & "-" & Session("emp_code").ToString & "-" & runno.ToString.PadLeft(5, "0")
                global_idp_no = new_idp_no
                sql &= " '" & new_idp_no & "' ,"
                sql &= " '" & runno & "' ,"
                sql &= " '" & y & "' ,"
                sql &= " GETDATE() ,"
                sql &= "" & Date.Now.Ticks & " ,"
            Else
                sql &= " null ,"
                sql &= " null ,"
                sql &= " null ,"
                sql &= " null ,"
                sql &= " null ,"
            End If

            sql &= " GETDATE() ,"
            '  sql &= " GETDATE() ,"
            '  sql &= "" & Date.Now.Ticks & " ,"
            sql &= "" & status_id & " ,"
            sql &= "'" & Session("dept_id").ToString & "' ,"
            sql &= "'" & Session("dept_name").ToString & "' ,"
            sql &= "'" & Session("user_fullname").ToString & "' ,"
            sql &= "'" & Session("emp_code").ToString & "' ,"
            sql &= "'" & Session("job_title").ToString & "' ,"
            sql &= "'" & Session("user_position").ToString & "' "
            sql &= ")"
            ' Response.Write(sql)
            ' Response.End()
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            id = pk

            If status_id = 2 Then ' Mode =  add and then submit
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

                '  loopMail(new_idp_no) ' Send mail to manager

            End If
        Else
            If status_id <> "" And status_id <> "1" Then ' ถ้าไม่ใช่ save draft


                sql = "UPDATE idp_trans_list SET status_id = '" & status_id & "'"

                If status_id = "2" Then ' Submitted
                    Dim y As String
                    Dim runno As String

                    sql2 = "SELECT * FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString & " ORDER BY idp_yearno DESC , idp_runno DESC  "
                 
                    ds = conn.getDataSetForTransaction(sql2, "t1")
                  
                    Try
                        y = ds.Tables("t1").Rows(0)("idp_yearno").ToString
                    Catch ex As Exception
                        y = Date.Now.Year.ToString
                    End Try

                    If y <> Date.Now.Year.ToString Then
                        y = Date.Now.Year.ToString
                        runno = "1"
                    Else
                        Try
                            runno = CInt(ds.Tables("t1").Rows(0)("idp_runno").ToString) + 1
                        Catch ex As Exception
                            Response.Write(ex.Message)
                            runno = 1
                        End Try

                    End If

                    new_idp_no = y & "-" & Session("emp_code").ToString & "-" & runno.ToString.PadLeft(5, "0")
                    '  Response.Write(new_idp_no)
                    '   Response.End()
                    global_idp_no = new_idp_no
                    sql &= " , idp_no = '" & new_idp_no & "' "
                    sql &= " , idp_runno = '" & runno & "' "
                    sql &= " , idp_yearno = '" & y & "' "
                    sql &= " , date_submit =  GETDATE() "
                    sql &= " , date_submit_ts = " & Date.Now.Ticks & " "
                End If

                If status_id = "9" Then
                    sql &= " , date_close = GETDATE() "
                    sql &= " , date_close_ts = " & Date.Now.Ticks & " "
                End If
                sql &= " WHERE idp_id = " & id

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If

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

                If status_id = "2" Then
                    '  loopMail(new_idp_no) ' Send mail to manager
                End If
            End If

        End If
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

    Sub updateExpense()
        Dim sql As String
        Dim errorMsg As String


        sql = "UPDATE idp_training_expense SET session_id = null , idp_id = " & new_idp_id & "  WHERE session_id = '" & Session.SessionID & "'"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If
      
    End Sub

    Sub updateExpenseModeEdit()
        Dim sql As String
        Dim errorMsg As String


        sql = "UPDATE idp_training_expense SET session_id = null   WHERE idp_id = '" & id & "'"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If

    End Sub

    Sub updateFileTraining()
        Dim sql As String
        Dim errorMsg As String


        sql = "UPDATE idp_attachment SET session_id = null , ir_id = " & new_idp_id & "  WHERE session_id = '" & Session.SessionID & "'"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If

    End Sub

    Sub updateFileInternalTraining()
        Dim sql As String
        Dim errorMsg As String


        sql = "UPDATE idp_trainging_file SET session_id = null , idp_id = " & new_idp_id & "  WHERE session_id = '" & Session.SessionID & "'"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If
    End Sub

    Sub updateSchedule()
        Dim sql As String
        Dim errorMsg As String


        sql = "UPDATE idp_training_schedule SET session_id = null , idp_id = " & new_idp_id & "  WHERE session_id = '" & Session.SessionID & "'"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If

    End Sub

    Sub updateSpeaker()
        Dim sql As String
        Dim errorMsg As String


        sql = "UPDATE idp_training_speaker SET session_id = null , idp_id = " & new_idp_id & "  WHERE session_id = '" & Session.SessionID & "'"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If

    End Sub

    Sub updateIDPInternal()
        Dim sql As String
        Dim errorMsg As String


        sql = "UPDATE idp_training_relate_idp SET session_id = null , idp_id = " & new_idp_id & "  WHERE session_id = '" & Session.SessionID & "'"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If

    End Sub

    Sub updateDetail()
        Dim sql As String
        Dim errorMsg As String

        sql = "UPDATE idp_external_req SET last_update = GETDATE()"
        sql &= " , contact_person = '" & addslashes(txtcontact.Text) & "' "
        sql &= " , relate_idp_no = '" & addslashes(txtrelate_idpno.Text) & "' "
        sql &= " , ext_title = '" & addslashes(txttitle.Value) & "' "
        sql &= " , ext_type_id = '" & txttype.SelectedValue & "' "
        sql &= " , ext_type_name = '" & txttype.SelectedItem.Text & "' "
        sql &= " , training_type_id = '" & txttraintype.SelectedValue & "' "
        sql &= " , training_type_name = '" & txttraintype.SelectedItem.Text & "' "

        sql &= " , chk_attend2_ladder_before = '" & txtladder1.SelectedValue & "' "
        sql &= " , chk_attend2_ladder_after = '" & txtladder2.SelectedValue & "' "
        sql &= " , chk_attend7_remark = '" & addslashes(txtattend_remark.Value) & "' "

        For i As Integer = 1 To 7
            If CType(panel_detail.FindControl("chk_attend" & i), HtmlInputCheckBox).Checked = True Then
                sql &= ", chk_attend" & i & " = '" & 1 & "'"
            Else
                sql &= ", chk_attend" & i & " = '" & 0 & "'"
            End If
        Next i

        For i As Integer = 1 To 3
            If CType(panel_detail.FindControl("txtr" & i), RadioButton).Checked = True Then
                sql &= ", register_type = '" & i & "'"
                If i = 1 Then
                    sql &= ", register_type_name = 'Reservation made, Employee Training and Development Unit will provide cash for registration'"
                ElseIf i = 2 Then
                    sql &= ", register_type_name = 'Employee Training and Developement Department Unit will make reservation and provide cash for registration' "
                ElseIf i = 3 Then
                    sql &= ", register_type_name = 'Reservation made and prepaid by Employee Training and Development Department Unit on  ' "
                End If
            
            End If
        Next i

        sql &= " , register_type_reserve_date = '" & convertToSQLDatetime(txtdate_register.Text) & "' "
        sql &= " , register_type_reserve_date_ts = '" & ConvertDateStringToTimeStamp(txtdate_register.Text) & "' "

        sql &= " , register_type_reserve_date2 = '" & convertToSQLDatetime(txtdate_register2.Text) & "' "
        sql &= " , register_type_reserve_date_ts2 = '" & ConvertDateStringToTimeStamp(txtdate_register2.Text) & "' "

        sql &= " , course_outline = '" & addslashes(txtcourse_detail.Value) & "' "
        sql &= " , expect_detail = '" & addslashes(txtexpect_detail.SelectedValue) & "' "
        sql &= " , facility = '" & addslashes(txtfacility.Value) & "' "
        sql &= " , place = '" & addslashes(txtplace.Text) & "' "
        sql &= " , date_start = '" & convertToSQLDatetime(txtdate11.Text) & "' "
        sql &= " , date_start_ts = '" & ConvertDateStringToTimeStamp(txtdate11.Text, CInt(txthour1.SelectedValue), CInt(txtmin1.SelectedValue)) & "' "
        sql &= " , date_end = '" & convertToSQLDatetime(txtdate2.Text) & "' "
        sql &= " , date_end_ts = '" & ConvertDateStringToTimeStamp(txtdate2.Text, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & "' "
        sql &= " , institution = '" & addslashes(txtinstitution.Value) & "' "

        If chk_certificate.Checked = True Then
            sql &= " , has_certificate = 1 "
        End If
        sql &= " , certificate_detail = '" & addslashes(txtcertificate_remark.Text) & "' "

        sql &= " , goal_skill_level = '" & addslashes(txtgoal_level.SelectedValue) & "' "
        sql &= " , goal_skill_level_aftertraining = '" & addslashes(txtgoal_level_after.SelectedValue) & "' "
        sql &= " , goal_benefit = '" & addslashes(txtgoal_benefit.Value) & "' "
        sql &= " , goal_problem = '" & addslashes(txtgoal_problem.Text) & "' "
        If ch_kpi1.Checked = True Then
            sql &= " , chk_kpi1 = 1 "
        Else
            sql &= " , chk_kpi1 = 0 "
        End If

        If ch_kpi2.Checked = True Then
            sql &= " , chk_kpi2 = 1 "
        Else
            sql &= " , chk_kpi2 = 0 "
        End If

        If ch_kpi3.Checked = True Then
            sql &= " , chk_kpi3 = 1 "
        Else
            sql &= " , chk_kpi3 = 0 "
        End If

        If ch_kpi4.Checked = True Then
            sql &= " , chk_kpi4 = 1 "
        Else
            sql &= " , chk_kpi4 = 0 "
        End If

        sql &= " , chk_kpi1_ladder1 = '" & (txtgoal_ladder1.SelectedValue) & "' "
        sql &= " , chk_kpi1_ladder2 = '" & (txtgoal_ladder2.SelectedValue) & "' "

        sql &= " , chk_kpi2_remark = '" & addslashes(txtgoal_kpi2.Text) & "' "
        sql &= " , chk_kpi3_remark = '" & addslashes(txtgoal_kpi3.Text) & "' "
        sql &= " , chk_kpi4_remark = '" & addslashes(txtgoal_kpi4.Text) & "' "


        If txthour.Value <> "" Then
            Try
                sql &= " , train_hour = '" & CDbl(txthour.Value) & "' "
            Catch ex As Exception
                sql &= " , train_hour = null "
            End Try


        Else
            sql &= " , train_hour = null "
        End If

        sql &= " , is_budget_request = '" & txtbudgetRequest.SelectedValue & "' "

        If chk_expect1.Checked Then
            sql &= " , chk_ext_expect1 = 1 "
        Else
            sql &= " , chk_ext_expect1 = 0 "
        End If

        If chk_expect2.Checked Then
            sql &= " , chk_ext_expect2 = 1 "
        Else
            sql &= " , chk_ext_expect2 = 0 "
        End If

        If chk_expect3.Checked Then
            sql &= " , chk_ext_expect3 = 1 "
        Else
            sql &= " , chk_ext_expect3 = 0 "
        End If

        If chk_expect4.Checked Then
            sql &= " , chk_ext_expect4 = 1 "
        Else
            sql &= " , chk_ext_expect4 = 0 "
        End If

        If chk_expect5.Checked Then
            sql &= " , chk_ext_expect5 = 1 "
        Else
            sql &= " , chk_ext_expect5 = 0 "
        End If

        If chk_expect6.Checked Then
            sql &= " , chk_ext_expect6 = 1 "
        Else
            sql &= " , chk_ext_expect6 = 0 "
        End If

        sql &= " , combo_ext_expect3_id = '" & txtnewgoal.SelectedValue & "' "
        sql &= " , combo_ext_expect3_text = '" & txtnewgoal.SelectedItem.Text & "' "
        ' sql &= " , register_type = '" & new_idp_id & "' "
        sql &= " , combo_expect_level1 = '" & txtexpect_level1.SelectedValue & "' "
        sql &= " , combo_expect_level2 = '" & txtexpect_level2.SelectedValue & "' "
        sql &= " , chk_ext_expect6_text = '" & addslashes(txtexpect_goal_other.Text) & "' "

        sql &= " , ext_category_id = '" & txtadd_extcat.SelectedValue & "' "
        sql &= " , ext_category_name = '" & txtadd_extcat.SelectedItem.Text & "' "

        sql &= " WHERE idp_id = " & id



        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If
    End Sub


    Sub updateGoal()
        Dim sql As String
        Dim errorMsg As String

        Dim lblPkAction As Label
        Dim txtdate1 As TextBox
        Dim txtdate2 As TextBox
        Dim txtaction_status As DropDownList

        sql = ""
        For i As Integer = 0 To GridGoal.Rows.Count - 1
            lblPkAction = CType(GridGoal.Rows(i).FindControl("lblPkAction"), Label)
            txtdate1 = CType(GridGoal.Rows(i).FindControl("txtdate1"), TextBox)
            txtdate2 = CType(GridGoal.Rows(i).FindControl("txtdate2"), TextBox)
            txtaction_status = CType(GridGoal.Rows(i).FindControl("txtaction_status"), DropDownList)

            sql = "UPDATE idp_training_goal SET start_date_ts = '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' "
            sql &= " , start_date = '" & convertToSQLDatetime(txtdate1.Text) & "' "
            sql &= " , end_date_ts = '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' "
            sql &= " , end_date = '" & convertToSQLDatetime(txtdate1.Text) & "' "
            sql &= " , action_status = '" & txtaction_status.SelectedValue & "' "
            sql &= " , action_status_name = '" & txtaction_status.SelectedItem.Text & "' "
            sql &= " WHERE goal_id = " & lblPkAction.Text

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i


        sql = "UPDATE idp_external_req SET last_update = GETDATE()"

        If chk_certificate.Checked = True Then
            sql &= " , has_certificate = 1 "
        End If
        sql &= " , certificate_detail = '" & addslashes(txtcertificate_remark.Text) & "' "

        sql &= " , goal_skill_level = '" & addslashes(txtgoal_level.SelectedValue) & "' "
        sql &= " , goal_skill_level_aftertraining = '" & addslashes(txtgoal_level_after.SelectedValue) & "' "
        sql &= " , goal_benefit = '" & addslashes(txtgoal_benefit.Value) & "' "
        sql &= " , goal_problem = '" & addslashes(txtgoal_problem.Text) & "' "
        If ch_kpi1.Checked = True Then
            sql &= " , chk_kpi1 = 1 "
        Else
            sql &= " , chk_kpi1 = 0 "
        End If

        If ch_kpi2.Checked = True Then
            sql &= " , chk_kpi2 = 1 "
        Else
            sql &= " , chk_kpi2 = 0 "
        End If

        If ch_kpi3.Checked = True Then
            sql &= " , chk_kpi3 = 1 "
        Else
            sql &= " , chk_kpi3 = 0 "
        End If

        If ch_kpi4.Checked = True Then
            sql &= " , chk_kpi4 = 1 "
        Else
            sql &= " , chk_kpi4 = 0 "
        End If

        If ch_kpi5.Checked = True Then
            sql &= " , chk_kpi5 = 1 "
        Else
            sql &= " , chk_kpi5 = 0 "
        End If

        If ch_kpi6.Checked = True Then
            sql &= " , chk_kpi6 = 1 "
        Else
            sql &= " , chk_kpi6 = 0 "
        End If

        If ch_kpi7.Checked = True Then
            sql &= " , chk_kpi7 = 1 "
        Else
            sql &= " , chk_kpi7 = 0 "
        End If

        If ch_kpi8.Checked = True Then
            sql &= " , chk_kpi8 = 1 "
        Else
            sql &= " , chk_kpi8 = 0 "
        End If

        sql &= " , chk_kpi1_ladder1 = '" & (txtgoal_ladder1.SelectedValue) & "' "
        sql &= " , chk_kpi1_ladder2 = '" & (txtgoal_ladder2.SelectedValue) & "' "

        sql &= " , chk_kpi2_scope1 = '" & (txtgoal_kpi2_scope1.SelectedValue) & "' "
        sql &= " , chk_kpi2_scope2 = '" & (txtgoal_kpi2_scope2.SelectedValue) & "' "

        '    sql &= " , chk_kpi2_remark = '" & addslashes(txtgoal_kpi2.Text) & "' "
        sql &= " , chk_kpi3_remark = '" & addslashes(txtgoal_kpi3.Text) & "' "
        sql &= " , chk_kpi4_remark = '" & addslashes(txtgoal_kpi4.Text) & "' "
        sql &= " , chk_kpi5_remark = '" & addslashes(txtgoal_kpi5.Text) & "' "
        sql &= " , chk_kpi6_remark = '" & addslashes(txtgoal_kpi6.Text) & "' "
        sql &= " , chk_kpi7_remark = '" & addslashes(txtgoal_kpi7.Text) & "' "
        sql &= " , chk_kpi8_remark = '" & addslashes(txtgoal_kpi8.Text) & "' "

    
        ' sql &= " , register_type = '" & new_idp_id & "' "

        sql &= " WHERE idp_id = " & id

        'Response.Write(sql)

        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If
    End Sub


    Sub updateDetailInternalRequest()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""

        sql = "UPDATE idp_external_req SET last_update = GETDATE()"
        sql &= " , internal_title = '" & addslashes(txtint_topic.Value) & "' "
        sql &= " , internal_outline = '" & addslashes(txtint_outline.Value) & "' "
        sql &= " , request_type = 'int' "
        If txtidp_relate.SelectedIndex > 0 Then
            sql &= " , internal_idp_program_id = '" & txtidp_relate.SelectedValue & "' "
            sql &= " , internal_idp_program_name = '" & addslashes(txtidp_relate.SelectedItem.Text) & "' "
        End If

        If txttarget1.Checked = True Then
            sql &= " , internal_target_group_id = 1 , internal_target_group_name = 'employee' "
        ElseIf txttarget2.Checked = True Then
            sql &= " , internal_target_group_id = 2 , internal_target_group_name = 'cost center' "
        ElseIf txttarget3.Checked = True Then
            sql &= " , internal_target_group_id = 3 , internal_target_group_name = 'job type' "
        ElseIf txttarget4.Checked = True Then
            sql &= " , internal_target_group_id = 4 , internal_target_group_name = 'job title' "
        End If

        sql &= " WHERE idp_id = " & id

        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If



        Dim lblOutcome As TextBox
        Dim lblMeasure As TextBox
        Dim lblPK As Label
        For s As Integer = 0 To GridFunction.Rows.Count - 1
            lblPK = CType(GridFunction.Rows(s).FindControl("lblPK"), Label)
            lblOutcome = CType(GridFunction.Rows(s).FindControl("lblOutcome"), TextBox)
            lblMeasure = CType(GridFunction.Rows(s).FindControl("lblMeasure"), TextBox)

            sql = "UPDATE idp_training_relate_idp SET outcome = '" & addslashes(lblOutcome.Text) & "' "
            sql &= " , measure = '" & addslashes(lblMeasure.Text) & "' "

            sql &= "  WHERE relate_id = " & lblPK.Text
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
                Exit For
            End If
        Next s

        sql = "DELETE FROM idp_training_employee WHERE idp_id = " & id
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If

        For i As Integer = 0 To lblDivisionSelect.Items.Count - 1
            If lblDivisionSelect.Items(i).Value <> "" Then
                pk = getPK("training_emp_id", "idp_training_employee", conn)
                sql = "INSERT INTO idp_training_employee (training_emp_id , idp_id , emp_code , emp_name_th , emp_name_en) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & id & "' ,"
                sql &= " '" & lblDivisionSelect.Items(i).Value & "' ,"
                sql &= " '" & addslashes(lblDivisionSelect.Items(i).Text) & "' ,"
                sql &= " '" & addslashes(lblDivisionSelect.Items(i).Text) & "' "

                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If

        Next

        sql = "DELETE FROM idp_training_costcenter WHERE idp_id = " & id
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If

        For i As Integer = 0 To lblCCSelect.Items.Count - 1
            pk = getPK("training_cost_id", "idp_training_costcenter", conn)
            sql = "INSERT INTO idp_training_costcenter (training_cost_id , idp_id , costcenter_id) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & lblCCSelect.Items(i).Value & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        Next

        sql = "DELETE FROM idp_training_jobtype WHERE idp_id = " & id
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If
        'Response.Write(sql)

        For i As Integer = 0 To lblJobTypeSelect.Items.Count - 1
            pk = getPK("training_jobtype_id", "idp_training_jobtype", conn)
            sql = "INSERT INTO idp_training_jobtype (training_jobtype_id , idp_id , job_type_name_th , job_type_name_en) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & addslashes(lblJobTypeSelect.Items(i).Value) & "' ,"
            sql &= " '" & addslashes(lblJobTypeSelect.Items(i).Value) & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        Next

        sql = "DELETE FROM idp_training_jobtitle WHERE idp_id = " & id
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If

        For i As Integer = 0 To lblJobTitleSelect.Items.Count - 1
            pk = getPK("training_jobtitle_id", "idp_training_jobtitle", conn)
            sql = "INSERT INTO idp_training_jobtitle (training_jobtitle_id , idp_id , job_title_th , job_title_en) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & addslashes(lblJobTitleSelect.Items(i).Value) & "' ,"
            sql &= " '" & addslashes(lblJobTitleSelect.Items(i).Value) & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        Next
    End Sub

    Sub bindRequestType()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * , request_name_th + ' / ' + request_name_en AS request_name FROM idp_budget_request a WHERE request_type = 1 ORDER BY order_sort ASC"
          
            ' Response.Write(sql)

            ds = conn.getDataSetForTransaction(sql, "t1")

            txtreq_type.DataSource = ds.Tables("t1")
            txtreq_type.DataBind()

            sql = "SELECT * FROM idp_budget_request a WHERE request_type = 2"
            ds = conn.getDataSetForTransaction(sql, "t2")
            txtreq_type2.DataSource = ds.Tables("t2")
            txtreq_type2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Sub bindFile()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM idp_attachment a WHERE 1 = 1"
            If mode = "add" Then
                sql &= " AND a.session_id = '" & session_id & "'"
            Else
                sql &= " AND a.ir_id = " & id
            End If
            ' Response.Write(sql)

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridFile.DataSource = ds.Tables(0)
            GridFile.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Sub bindFileInternal()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM idp_attachment a WHERE 1 = 1"
            If mode = "add" Then
                sql &= " AND a.session_id = '" & session_id & "'"
            Else
                sql &= " AND a.ir_id = " & id
            End If
            ' Response.Write(sql)

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridFileInternal.DataSource = ds.Tables(0)
            GridFileInternal.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Sub bindFileTRD()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM idp_trainging_file a WHERE 1 = 1 AND ISNULL(file_training_type,1) = 1 "
        
            sql &= " AND a.idp_id = " & id

            '  Response.Write(sql)

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridFileTRD.DataSource = ds.Tables(0)
            GridFileTRD.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Sub bindFileTrainingDocInternal()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM idp_trainging_file a WHERE 1 = 1 AND ISNULL(file_training_type,1) = 3 "
            If mode = "add" Then
                sql &= " AND a.session_id = '" & Session.SessionID & "' "
            Else
                sql &= " AND a.idp_id = " & id
            End If


            '  Response.Write(sql)

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridTrainingDoc.DataSource = ds.Tables(0)
            GridTrainingDoc.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Sub bindFileExpense()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM idp_trainging_file a WHERE 1 = 1 AND ISNULL(file_training_type,1) = 2 "

            sql &= " AND a.idp_id = " & id

            '  Response.Write(sql)

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridFileExpense.DataSource = ds.Tables(0)
            GridFileExpense.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Protected Sub cmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim filename As String()
        Dim ds As New DataSet

        Try


            If Not IsNothing(FileUpload1.PostedFile) Then
                Dim strFileName = FileUpload1.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)
                Try
                    sql = "SELECT ISNULL(MAX(file_id),0) + 1 AS pk FROM idp_attachment"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO idp_attachment (file_id , ir_id ,  file_name , file_path , file_size , session_id) VALUES("
                sql &= "" & pk & " , "
                If id = "" Then
                    sql &= " null , "
                Else
                    sql &= "" & id & " , "
                End If

                sql &= "'" & addslashes(strFileName) & "' , "
                sql &= "'" & pk & "." & extension & "' , "
                sql &= "'" & FileUpload1.PostedFile.ContentLength & "' , "
                sql &= "'" & Session.SessionID & "'  "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                'Response.Write("pk = " & pk)
                If errorMsg <> "" Then
                    Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                End If

                FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/idp/attach_file/" & pk & "." & extension))

                conn.setDBCommit()
            End If

            bindFile()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Protected Sub cmdDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteFile.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim lblFilePath As Label

        i = GridFile.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridFile.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridFile.Rows(s).FindControl("chkSelect"), CheckBox)

                ' Response.Write(lbl.Text)
                If chk.Checked Then
                    sql = "DELETE FROM idp_attachment WHERE file_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                        'Exit For
                    End If
                End If
                ' Response.Write("rrr")

            Next s

            For s As Integer = 0 To i - 1
                chk = CType(GridFile.Rows(s).FindControl("chkSelect"), CheckBox)


                If chk.Checked Then
                    
                    lblFilePath = CType(GridFile.Rows(s).FindControl("lblFilePath"), Label)
                    File.Delete(Server.MapPath("../share/idp/attach_file/" & lblFilePath.Text))
                End If
            Next s

            conn.setDBCommit()
            bindFile()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

   
    Protected Sub GridComment_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridComment.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblStatusName As Label = CType(e.Row.FindControl("lblStatusName"), Label)
            Dim lblCommentStatusId As Label = CType(e.Row.FindControl("lblCommentStatusId"), Label)
            Dim lblEmpcode As Label = CType(e.Row.FindControl("lblEmpcode"), Label)
            Dim cmdDelComment As ImageButton = CType(e.Row.FindControl("cmdDelComment"), ImageButton)
            Dim cmdEditComment As ImageButton = CType(e.Row.FindControl("cmdEditComment"), ImageButton)

            cmdEditComment.Attributes.Add("onclick", "editDetail('" & lblPK.Text & "');return false;")

            If lblEmpcode.Text = Session("emp_code").ToString Then
                cmdDelComment.Visible = True
                cmdEditComment.Visible = True
            Else
                cmdDelComment.Visible = False
                cmdEditComment.Visible = False
            End If

            If lblCommentStatusId.Text = "1" Then
                lblStatusName.ForeColor = Drawing.Color.Green
                lblStatusName.Text = "<img src='../images/button_ok.png' id='img1' alt='approve' /> " & lblStatusName.Text
            ElseIf lblCommentStatusId.Text = "3" Then
                lblStatusName.Text = "<img src='../images/information.gif' id='img1' alt='approve' /> " & lblStatusName.Text
                lblStatusName.ForeColor = Drawing.Color.Red
            Else
                lblStatusName.Text = "<img src='../images/button_cancel.png' id='img1' alt='approve' /> " & lblStatusName.Text
                lblStatusName.ForeColor = Drawing.Color.Red
            End If

        End If
    End Sub

    Protected Sub GridComment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridComment.SelectedIndexChanged

    End Sub

    Protected Sub cmdAddExpense_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddExpense.Click
        addExpense()
    End Sub

    Sub addExpense()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try
            pk = getPK("expense_id", "idp_training_expense", conn)
            sql = "INSERT INTO idp_training_expense (expense_id , idp_id , accouting_type , expense_topic_id , expense_topic_name "
            sql &= ", expense_value , session_id , currency_type_id , currency_type_name , exchange_rate , is_sponsor , expense_remark , expense_request_type_id , expense_request_type_name , expense_payment_id , expense_payment_name , create_date , create_date_ts , create_by) VALUES( "
            sql &= " '" & pk & "' ,"
            If mode = "add" Then
                sql &= " null ,"
            Else
                sql &= " '" & id & "' ,"
            End If
            sql &= " 1 , "
            sql &= " '" & txtadd_expense_type.SelectedValue & "' ,"
            sql &= " '" & txtadd_expense_type.SelectedItem.Text & "' ,"
            sql &= " '" & txtadd_value.Text & "' ,"
            If mode = "edit" Then
                sql &= " null , "
            Else
                sql &= "'" & Session.SessionID & "' , "
            End If

            sql &= "'" & txtcurrency.SelectedValue & "' , "
            sql &= "'" & txtcurrency.SelectedItem.Text & "' , "
            If txtadd_exchange_rate.Text = "" Then
                sql &= "null , "
            Else
                sql &= "'" & txtadd_exchange_rate.Text & "' , "
            End If

            If txtadd_sponsor.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If
            sql &= "'" & addslashes(txtadd_expense_remark.Text) & "'  ,"

            sql &= "'" & txtreq_type.SelectedValue & "' , "
            sql &= "'" & txtreq_type.SelectedItem.Text & "' , "
            sql &= "'" & txtpayment_type.SelectedValue & "' , "
            sql &= "'" & txtpayment_type.SelectedItem.Text & "'  ,"

            sql &= " GETDATE() , "
            sql &= "'" & Date.Now.Ticks & "' , "
            sql &= "'" & Session("user_fullname").ToString & "'  "

            sql &= ")"
            'Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            txtadd_expense_type.SelectedIndex = 0
            txtadd_value.Text = "0"
            txtcurrency.SelectedIndex = 0
            txtadd_exchange_rate.Text = ""
            txtadd_expense_remark.Text = ""
            txtadd_sponsor.Checked = False
            bindExpense()
            bindBudgetMaster()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub addExpenseNoCommit()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        pk = getPK("expense_id", "idp_training_expense", conn)
        sql = "INSERT INTO idp_training_expense (expense_id , idp_id , expense_topic_id , expense_topic_name "
        sql &= ", expense_value , session_id , currency_type_id , currency_type_name , exchange_rate , is_sponsor , expense_remark , expense_request_type_id , expense_request_type_name , expense_payment_id , expense_payment_name , create_date , create_date_ts , create_by) VALUES( "
        sql &= " '" & pk & "' ,"
        If mode = "add" Then
            sql &= " null ,"
        Else
            sql &= " '" & id & "' ,"
        End If

        sql &= " '" & txtadd_expense_type.SelectedValue & "' ,"
        sql &= " '" & txtadd_expense_type.SelectedItem.Text & "' ,"
        sql &= " '" & txtadd_value.Text & "' ,"
        sql &= "'" & Session.SessionID & "' , "
        sql &= "'" & txtcurrency.SelectedValue & "' , "
        sql &= "'" & txtcurrency.SelectedItem.Text & "' , "
        If txtadd_exchange_rate.Text = "" Then
            sql &= "null , "
        Else
            sql &= "'" & txtadd_exchange_rate.Text & "' , "
        End If

        If txtadd_sponsor.Checked = True Then
            sql &= " 1 , "
        Else
            sql &= " 0 , "
        End If
        sql &= "'" & addslashes(txtadd_expense_remark.Text) & "' , "
        sql &= "'" & txtreq_type.SelectedValue & "' , "
        sql &= "'" & txtreq_type.SelectedItem.Text & "' , "
        sql &= "'" & txtpayment_type.SelectedValue & "' , "
        sql &= "'" & txtpayment_type.SelectedItem.Text & "'  ,"

        sql &= " GETDATE() , "
        sql &= "'" & Date.Now.Ticks & "' , "
        sql &= "'" & Session("user_fullname").ToString & "'  "
        sql &= ")"

        errorMsg = conn.executeSQLForTransaction(sql)

        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If


      
    End Sub

    Sub addExpense2()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try
            pk = getPK("expense_id", "idp_training_expense", conn)
            sql = "INSERT INTO idp_training_expense (expense_id , accouting_type , idp_id , expense_topic_id , expense_topic_name "
            sql &= ", expense_value  , currency_type_id , currency_type_name , exchange_rate ,  expense_remark , expense_request_type_id , expense_request_type_name , expense_payment_id , expense_payment_name , create_date , create_date_ts , create_by , acc_expense_by , acc_receive_by) VALUES( "
            sql &= " '" & pk & "' ,"
            sql &= " '" & 2 & "' ,"
            sql &= " '" & id & "' ,"


            sql &= " '" & txtadd_expense_type2.SelectedValue & "' ,"
            sql &= " '" & txtadd_expense_type2.SelectedItem.Text & "' ,"
            sql &= " '" & txtadd_value2.Text & "' ,"

            sql &= "'" & txtcurrency2.SelectedValue & "' , "
            sql &= "'" & txtcurrency2.SelectedItem.Text & "' , "
            If txtadd_exchange_rate2.Text = "" Then
                sql &= "null , "
            Else
                sql &= "'" & txtadd_exchange_rate2.Text & "' , "
            End If

          
            sql &= "'" & addslashes(txtadd_expense_remark2.Text) & "'  ,"

            sql &= "'" & txtreq_type2.SelectedValue & "' , "
            sql &= "'" & txtreq_type2.SelectedItem.Text & "' , "
            sql &= "'" & txtpayment_type2.SelectedValue & "' , "
            sql &= "'" & txtpayment_type2.SelectedItem.Text & "'  ,"

            sql &= " GETDATE() , "
            sql &= "'" & Date.Now.Ticks & "' , "
            sql &= "'" & Session("user_fullname").ToString & "'  ,"
            sql &= "'" & Session("user_fullname").ToString & "' , "
            sql &= "'" & addslashes(txtadd_receive.Text) & "'  "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            txtadd_expense_type2.SelectedIndex = 0
            txtadd_value2.Text = "0"
            txtcurrency2.SelectedIndex = 0
            txtadd_exchange_rate2.Text = ""
            txtadd_expense_remark2.Text = ""

            bindExpense2()


        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdDelExpense_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelExpense.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox

        Dim iDelete As Integer = 0
        i = GridExpense.Rows.Count

        Try
            conn.startTransactionSQLServer()

            For s As Integer = 0 To i - 1

                lbl = CType(GridExpense.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridExpense.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM idp_training_expense WHERE expense_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If

                    sql = "DELETE FROM idp_file_list WHERE expense_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If

                    iDelete += 1
                End If
            Next s

            conn.setDBCommit()

            bindExpense()
            If iDelete = i Then
                priceTotal = 0
                priceTotal2 = 0
                txttotal.Text = 0
                txtrequest_budget.Text = 0
            End If

            bindBudgetMaster()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdSaveOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveOrder.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim txtorder As TextBox

        i = GridExpense.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridExpense.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridExpense.Rows(s).FindControl("chk"), CheckBox)
                txtorder = CType(GridExpense.Rows(s).FindControl("txtorder"), TextBox)

                'If chk.Checked = True Then
                sql = "UPDATE  idp_training_expense SET order_sort = " & txtorder.Text & " WHERE expense_id = " & lbl.Text
                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
                'End If
            Next s

            conn.setDBCommit()

            bindExpense()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub saveBudgetOrderNoCommit()
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim txtorder As TextBox
        Dim order As Integer = 0

        i = GridExpense.Rows.Count

        For s As Integer = 0 To i - 1

            lbl = CType(GridExpense.Rows(s).FindControl("lblPK"), Label)
            chk = CType(GridExpense.Rows(s).FindControl("chk"), CheckBox)
            txtorder = CType(GridExpense.Rows(s).FindControl("txtorder"), TextBox)

            'If chk.Checked = True Then
            Try
                order = CInt(txtorder.Text)
            Catch ex As Exception
                order = 0
            End Try
            sql = "UPDATE  idp_training_expense SET order_sort = " & order & " WHERE expense_id = " & lbl.Text
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
                Exit For
            End If
            'End If
        Next s




    End Sub

    Protected Sub GridExpense_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridExpense.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblSponsor As Label = CType(e.Row.FindControl("lblSponsor"), Label)
            Dim lblExchange As Label = CType(e.Row.FindControl("lblExchange"), Label)
            Dim lblValue As Label = CType(e.Row.FindControl("lblValue"), Label)
            Dim lblcurtypeid As Label = CType(e.Row.FindControl("lblcurtypeid"), Label)
            Dim lblcurtype As Label = CType(e.Row.FindControl("lblcurtype"), Label)
            '  Dim chk_exchange As CheckBox = CType(e.Row.FindControl("chk_exchange"), CheckBox)
            Dim lblExpensetypeID As Label = CType(e.Row.FindControl("lblExpensetypeID"), Label)
            Dim lblReqBudget As Label = CType(e.Row.FindControl("lblReqBudget"), Label)
            Dim lblConvertToBaht As Label = CType(e.Row.FindControl("lblConvertToBaht"), Label)

            Dim sql As String
            Dim ds As New DataSet

            Try

                If lblReqBudget.Text = "1" Then ' is_request_budget = 1
                    e.Row.BackColor = Drawing.Color.LightYellow
                    If lblcurtypeid.Text = "1" Then ' Baht
                        'lblExchange.Text = "-"
                        budgetTotal += CDbl(lblValue.Text)
                    Else
                        If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                            lblExchange.Text = "1"
                        End If

                        budgetTotal += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))

                        ' lblExchange.Text = "1 Baht = " & lblExchange.Text & " " & lblcurtype.Text
                    End If
                End If

                If lblcurtypeid.Text = "1" Then ' Baht
                    lblExchange.Text = "-"
                    priceTotal += CDbl(lblValue.Text)
                Else
                    If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                        lblExchange.Text = "1"
                    End If

                    priceTotal += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))
                    lblConvertToBaht.Text = "<br/>" & FormatNumber((CDbl(lblValue.Text) * CDbl(lblExchange.Text)), 2) & " Baht"

                    lblExchange.Text = "1 Baht = " & lblExchange.Text & " " & lblcurtype.Text
                    ' lblExchange.Text &= "<br/>"

                End If

                txttotal.Text = FormatNumber(priceTotal)
                txtrequest_budget.Text = FormatNumber(budgetTotal)
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridExpense_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridExpense.SelectedIndexChanged

    End Sub

    Protected Sub GridviewIDPLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridviewIDPLog.PageIndexChanging
        GridviewIDPLog.PageIndex = e.NewPageIndex
        bindGridIDPLog()
    End Sub

    Protected Sub GridviewIDPLog_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridviewIDPLog.SelectedIndexChanged

    End Sub

    Protected Sub cmdUpdateStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateStatus.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE idp_trans_list SET status_id = " & txtstatus.SelectedValue & " WHERE idp_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            updateOnlyLog(txtstatus.SelectedValue, "HR process")

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        End Try

        Response.Redirect("ext_training_detail.aspx?mode=edit&id=" & id)
    End Sub

  

    Protected Sub cmdTRDUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTRDUpload.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim filename As String()
        Dim ds As New DataSet

        Try


            If Not IsNothing(FileUpload2.PostedFile) Then
                Dim strFileName = FileUpload2.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)
                Try
                    sql = "SELECT ISNULL(MAX(trainging_file_id),0) + 1 AS pk FROM idp_trainging_file"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                    Return
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO idp_trainging_file (trainging_file_id , idp_id , file_training_type ,  file_name , file_path , file_size ) VALUES("
                sql &= "" & pk & " , "
                sql &= "" & id & " , "
                sql &= "" & 1 & " , "
                sql &= "'" & strFileName & "' , "
                sql &= "'" & pk & "." & extension & "' , "
                sql &= "'" & FileUpload2.PostedFile.ContentLength & "' "

                sql &= ")"
                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                'Response.Write("pk = " & pk)
                If errorMsg <> "" Then
                    Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                End If

                FileUpload2.PostedFile.SaveAs(Server.MapPath("../share/idp/hr/" & pk & "." & extension))
                If File.Exists(Server.MapPath("../share/idp/hr/" & pk & "." & extension)) = False Then
                    'Response.Write("x")
                    conn.setDBRollback()
                    Return
                End If

                conn.setDBCommit()

                bindFileTRD()
            End If

            bindFileTRD()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Protected Sub cmdTRDDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTRDDelete.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim lblFilePath As Label

        i = GridFileTRD.Rows.Count

        Try

            For s As Integer = 0 To i - 1
                ' Response.Write(s)
                lbl = CType(GridFileTRD.Rows(s).FindControl("lblPK1"), Label)
                chk = CType(GridFileTRD.Rows(s).FindControl("chkSelect1"), CheckBox)

                ' Response.Write(lbl.Text)
                If chk.Checked Then
                    sql = "DELETE FROM idp_trainging_file WHERE trainging_file_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                        'Exit For
                    End If
                End If
                ' Response.Write("rrr")

            Next s

            For s As Integer = 0 To i - 1
                chk = CType(GridFileTRD.Rows(s).FindControl("chkSelect1"), CheckBox)
                If chk.Checked Then
                    lblFilePath = CType(GridFileTRD.Rows(s).FindControl("lblFilePath"), Label)
                    File.Delete(Server.MapPath("../share/idp/hr/" & lblFilePath.Text))
                End If
            Next s

            conn.setDBCommit()
            bindFileTRD()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

  

    Protected Sub cmdReply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReply.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE idp_external_req SET reply_note = '" & addslashes(txtreply.Text) & "' "
            sql &= " , trd_note = '" & addslashes(txttrd_note.Value) & "' "
            sql &= " , reply_date = GETDATE() "
            sql &= " , reply_by = '" & Session("user_fullname").ToString & "' "
            sql &= "  WHERE idp_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub bindGridInformationUpdate()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * FROM idp_information_update WHERE idp_id =  " & id)


            ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

            'Response.Write(sqlB.ToString)
            GridInformation.DataSource = ds
            GridInformation.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub

    Protected Sub cmdAddUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddUpdate.Click
        Dim sql As String = ""
        Dim errorMsg As String = ""
        Dim pk As String = ""

        Try
            pk = getPK("inform_id", "idp_information_update", conn)
            sql = "INSERT INTO idp_information_update (inform_id , idp_id , inform_type , inform_detail , inform_date , inform_date_ts , inform_by , inform_emp_code , inform_dept_name , inform_costcenter) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " 'idp' ,"
            sql &= " '" & addslashes(txtadd_update.Text) & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' ,"
            sql &= " '" & Session("user_fullname").ToString & " , " & Session("user_position").ToString & "' ,"
            sql &= " '" & Session("emp_code").ToString & "' ,"
            sql &= " '" & Session("dept_name").ToString & "' ,"
            sql &= " '" & Session("costcenter_id").ToString & "' "
            sql &= ")"
            'Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            txtadd_update.Text = ""
            bindGridInformationUpdate()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridInformation_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridInformation.RowDeleting
        Dim sql As String
        Dim errorMSg As String

        Try
            sql = "DELETE FROM idp_information_update WHERE inform_id = '" & GridInformation.DataKeys(e.RowIndex).Value & "'"
            errorMSg = conn.executeSQLForTransaction(sql)
            If errorMSg <> "" Then
                Throw New Exception(errorMSg)
            End If

            conn.setDBCommit()
            bindGridInformationUpdate()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridInformation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridInformation.SelectedIndexChanged

    End Sub

    Protected Sub cmdBudgetSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBudgetSave.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If txtbudget_status.SelectedValue = "" Then
                sql = "UPDATE idp_external_req SET is_budget_approve = null , budget_remark = '" & addslashes(txtbudget_remark.Text) & "' , budget_update_by = '" & Session("user_fullname").ToString & "' , budget_last_update = GETDATE() WHERE idp_id = " & id
            Else
                sql = "UPDATE idp_external_req SET is_budget_approve = '" & txtbudget_status.SelectedValue & "' , budget_remark = '" & addslashes(txtbudget_remark.Text) & "' , budget_update_by = '" & Session("user_fullname").ToString & "' , budget_last_update = GETDATE() WHERE idp_id = " & id
            End If

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            If txtbudget_status.SelectedValue = "1" Then ' Approve
                ' div_budget.Enabled = False
                panel_add_expense.Visible = False
                tab_account_expense.Visible = True

                updateExpense2NoCommit()
            Else
                panel_add_expense.Visible = True
                tab_account_expense.Visible = False
            End If
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(sql)
            Response.Write(ex.Message)
            Return
        End Try

        Response.Redirect("ext_training_detail.aspx?mode=edit&id=" & id)
    End Sub

    Sub updateExpense2NoCommit()
        Dim sql As String
        Dim errorMsg As String = ""
        Dim ds As New DataSet
        Dim pk As String = ""

        sql = "DELETE FROM idp_training_expense WHERE accouting_type = 2 AND idp_id = " & id
        errorMsg = conn.executeSQLForTransaction(sql)

        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If

        sql = "SELECT * FROM idp_training_expense a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE b.is_request_budget = 1 AND a.idp_id = " & id
        ' Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
            pk = getPK("expense_id", "idp_training_expense", conn)
            sql = "INSERT INTO idp_training_expense (expense_id , idp_id , expense_topic_id , expense_topic_name "
            sql &= ", expense_value ,  currency_type_id , currency_type_name , exchange_rate , is_sponsor , expense_remark , expense_request_type_id , expense_request_type_name , expense_payment_id , expense_payment_name , create_date , create_date_ts , create_by , acc_expense_by , accouting_type  ) VALUES( "
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            ' Response.Write(1)
            sql &= " '" & ds.Tables("t1").Rows(i)("expense_topic_id").ToString & "' ,"
            sql &= " '" & ds.Tables("t1").Rows(i)("expense_topic_name").ToString & "' ,"
            sql &= " '" & ds.Tables("t1").Rows(i)("expense_value").ToString & "' ,"
            'Response.Write(2)
            sql &= "'" & ds.Tables("t1").Rows(i)("currency_type_id").ToString & "' , "
            sql &= "'" & ds.Tables("t1").Rows(i)("currency_type_name").ToString & "' , "

            If ds.Tables("t1").Rows(i)("exchange_rate").ToString = "" Then
                sql &= "null , "
            Else
                sql &= "'" & ds.Tables("t1").Rows(i)("exchange_rate").ToString & "' , "
            End If

            sql &= "'" & ds.Tables("t1").Rows(i)("is_sponsor").ToString & "' , "
            sql &= "'" & ds.Tables("t1").Rows(i)("expense_remark").ToString & "'  ,"

            ' sql &= "'" & ds.Tables("t1").Rows(i)("expense_request_type_id").ToString & "' , "
            ' sql &= "'" & ds.Tables("t1").Rows(i)("expense_request_type_name").ToString & "' , "
            sql &= "'" & 5 & "' , "
            sql &= "'" & "ชำระเงิน" & "' , "

            sql &= "'" & ds.Tables("t1").Rows(i)("expense_payment_id").ToString & "' , "
            sql &= "'" & ds.Tables("t1").Rows(i)("expense_payment_name").ToString & "'  ,"
            '  Response.Write(3)
            sql &= " GETDATE() , "
            sql &= "'" & Date.Now.Ticks & "' , "
            sql &= "'" & ds.Tables("t1").Rows(i)("create_by").ToString & "'  ,"
            sql &= "'" & ds.Tables("t1").Rows(i)("create_by").ToString & "'  ,"
            sql &= " 2 "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        Next i


    End Sub

    Protected Sub cmdAddExpense2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddExpense2.Click
        addExpense2()
    End Sub

    Protected Sub GridExpense2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridExpense2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblExchange As Label = CType(e.Row.FindControl("lblExchange"), Label)
            Dim lblValue As Label = CType(e.Row.FindControl("lblValue"), Label)
            Dim lblcurtypeid As Label = CType(e.Row.FindControl("lblcurtypeid"), Label)
            Dim lblcurtype As Label = CType(e.Row.FindControl("lblcurtype"), Label)
            Dim lblExpensetypeID As Label = CType(e.Row.FindControl("lblExpensetypeID"), Label)
            Dim lblReqBudget As Label = CType(e.Row.FindControl("lblReqBudget"), Label)
            Dim lblConvertToBaht As Label = CType(e.Row.FindControl("lblConvertToBaht"), Label)
            Dim lblConvertToBaht2 As Label = CType(e.Row.FindControl("lblConvertToBaht2"), Label)

            Dim txttype As DropDownList = CType(e.Row.FindControl("txttype"), DropDownList)
            Dim txtpayment As DropDownList = CType(e.Row.FindControl("txtpayment"), DropDownList)
            Dim lbltype_id As Label = CType(e.Row.FindControl("lbltype_id"), Label)
            Dim lblpayment_id As Label = CType(e.Row.FindControl("lblpayment_id"), Label)

            Dim lblTopicID As Label = CType(e.Row.FindControl("lblTopicID"), Label)
            Dim lblApprove As Label = CType(e.Row.FindControl("lblApprove"), Label)
            Dim lblDelete As Label = CType(e.Row.FindControl("lblDelete"), Label)

            Dim textPersonContact As TextBox = CType(e.Row.FindControl("textPersonContact"), TextBox)
            Dim lblFileNum As Label = CType(e.Row.FindControl("lblFileNum"), Label)

            Dim lblMonney As Label = CType(e.Row.FindControl("lblMoney"), Label)
            Dim chkReceive As CheckBox = CType(e.Row.FindControl("chkReceive"), CheckBox)
            '  Dim chk_exchange As CheckBox = CType(e.Row.FindControl("chk_exchange"), CheckBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                If lblDelete.Text = "1" Then
                    e.Row.Font.Strikeout = True
                    txttype.Enabled = False
                    txtpayment.Enabled = False
                End If

                If lblMonney.Text = "1" Then
                    chkReceive.Checked = True
                Else
                    chkReceive.Checked = False
                End If

                txtpayment.SelectedValue = lblpayment_id.Text
                txttype.SelectedValue = lbltype_id.Text

                sql = "SELECT SUM(expense_value) FROM idp_training_expense a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE a.idp_id = " & id
                sql &= " AND b.is_request_budget = 1 AND a.accouting_type = 1 AND a.expense_topic_id = " & lblExpensetypeID.Text
                sql &= " GROUP BY a.expense_topic_id "
                ' Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    lblApprove.Text = FormatNumber(CDbl(ds.Tables("t1").Rows(0)(0).ToString), 2)
                End If

                sql = "SELECT * FROM idp_file_list WHERE expense_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t2")
                lblFileNum.Text = " (" & ds.Tables("t2").Rows.Count & ")"


                If textPersonContact.Text.Trim <> "" Then
                    For i As Integer = 0 To e.Row.Cells.Count - 1
                        e.Row.Cells(i).BackColor = Drawing.Color.GreenYellow
                    Next i

                End If

                If lblReqBudget.Text = "1" And lblDelete.Text <> "1" Then ' ชำระเงิน , เบิกคืน
                    e.Row.BackColor = Drawing.Color.LightYellow

                    If textPersonContact.Text.Trim <> "" Then
                        If lblcurtypeid.Text = "1" Then ' Baht
                            'lblExchange.Text = "-"
                            budgetTotal2 += CDbl(lblValue.Text)
                        Else
                            If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                                lblExchange.Text = "1"
                            End If


                            budgetTotal2 += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))

                            ' lblExchange.Text = "1 BAHT = " & lblExchange.Text & " " & lblcurtype.Text
                        End If
                    End If
                ElseIf lblReqBudget.Text = "0" And lblDelete.Text <> "1" Then ' รับคืน
                    If textPersonContact.Text.Trim <> "" Then
                        If lblcurtypeid.Text = "1" Then ' Baht
                            'lblExchange.Text = "-"
                            returnTotal += CDbl(lblValue.Text)
                        Else
                            If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                                lblExchange.Text = "1"
                            End If


                            returnTotal += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))

                            ' lblExchange.Text = "1 BAHT = " & lblExchange.Text & " " & lblcurtype.Text
                        End If
                    End If
                End If

                If lblcurtypeid.Text = "1" Then ' Baht
                    lblExchange.Text = "-"
                    priceTotal2 += CDbl(lblValue.Text)
                Else
                    If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                        lblExchange.Text = "1"
                    End If

                    lblConvertToBaht.Text = "<br/>" & FormatNumber((CDbl(lblValue.Text) * CDbl(lblExchange.Text)), 2) & " Baht"
                    lblConvertToBaht2.Text = "<br/>" & FormatNumber((CDbl(lblApprove.Text) * CDbl(lblExchange.Text)), 2) & " Baht"
                    priceTotal2 += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))

                    lblExchange.Text = "1 " & lblcurtype.Text & " = " & lblExchange.Text & " BAHT"
                    lblConvertToBaht.ToolTip = lblExchange.Text
                    lblConvertToBaht2.ToolTip = lblExchange.Text
                End If




                txtactual_expense.Text = FormatNumber(budgetTotal2)
                lblReturnBudget.Text = FormatNumber(returnTotal)
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridExpense2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridExpense2.RowDeleting

    End Sub

    Protected Sub GridExpense2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridExpense2.SelectedIndexChanged

    End Sub

    Protected Sub txtcurrency2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcurrency2.SelectedIndexChanged
        lblcurrency.Text = txtcurrency2.SelectedItem.Text
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridExpense2.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridExpense2.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridExpense2.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "UPDATE idp_training_expense SET is_delete = 1 WHERE expense_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If

                    'sql = "DELETE FROM idp_training_expense WHERE expense_id = " & lbl.Text

                    'errorMsg = conn.executeSQLForTransaction(sql)
                    'If errorMsg <> "" Then
                    '    Throw New Exception(errorMsg)
                    '    Exit For
                    'End If

                    'sql = "DELETE FROM idp_file_list WHERE expense_id = " & lbl.Text
                    'errorMsg = conn.executeSQLForTransaction(sql)
                    'If errorMsg <> "" Then
                    '    Throw New Exception(errorMsg)
                    '    Exit For
                    'End If
                End If
            Next s

            conn.setDBCommit()

            bindExpense2()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub getTotalApproveBudget()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT SUM(expense_value * case when currency_type_id <> 1 then exchange_rate else 1 end ) FROM idp_training_expense a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE a.idp_id = " & id
            sql &= " AND b.is_request_budget = 1 AND accouting_type = 1 "
            sql &= " GROUP BY a.idp_id "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                Try
                    lblApproveBudget.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0)(0).ToString), 2)
                Catch ex As Exception
                    lblApproveBudget.Text = 0
                End Try

            Else
                lblApproveBudget.Text = 0
            End If


        Catch ex As Exception

            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub txtcurrency_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcurrency.SelectedIndexChanged
        lblcurrency0.Text = txtcurrency.SelectedItem.Text
    End Sub

    Protected Sub cmdUpdateExpense_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateExpense.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chkSelect As CheckBox
        Dim textPersonContact As TextBox
        Dim txttype As DropDownList
        Dim txtpayment As DropDownList
        Dim txtremark As TextBox
        Dim chkReceive As CheckBox
        'Dim textPersonContact As TextBox

        i = GridExpense2.Rows.Count
        Dim pk As String = ""
        Try

            For s As Integer = 0 To i - 1
                chkSelect = CType(GridExpense2.Rows(s).FindControl("chk"), CheckBox)
                textPersonContact = CType(GridExpense2.Rows(s).FindControl("textPersonContact"), TextBox)
                txttype = CType(GridExpense2.Rows(s).FindControl("txttype"), DropDownList)
                txtpayment = CType(GridExpense2.Rows(s).FindControl("txtpayment"), DropDownList)
                lbl = CType(GridExpense2.Rows(s).FindControl("lblPk"), Label)
                txtremark = CType(GridExpense2.Rows(s).FindControl("txtremark"), TextBox)
                chkReceive = CType(GridExpense2.Rows(s).FindControl("chkReceive"), CheckBox)

                ' If chkSelect.Checked = True Then

                sql = "UPDATE idp_training_expense SET expense_request_type_id = " & txttype.SelectedValue
                sql &= " , expense_request_type_name = '" & txttype.SelectedItem.Text & "' "
                sql &= " , expense_payment_id = '" & txtpayment.SelectedValue & "' "
                sql &= " , expense_payment_name = '" & txtpayment.SelectedItem.Text & "' "
                sql &= " , acc_receive_by = '" & addslashes(textPersonContact.Text) & "' "
                sql &= " , expense_remark = '" & addslashes(txtremark.Text) & "' "
                If chkReceive.Checked Then
                    sql &= " , is_receive_money = 1 "
                Else
                    sql &= " , is_receive_money = 0 "
                End If

                sql &= " WHERE expense_id = " & lbl.Text
                'Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)

                End If

                If textPersonContact.Text <> "" Then
                    sql = "UPDATE idp_training_expense SET create_date = GETDATE() , create_date_ts = " & Date.Now.Ticks
                    sql &= " WHERE expense_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)

                    End If
                End If

                '  updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
                ' End If
            Next s

            ' Update Status
            If txtexpense_status.SelectedValue = "" Then
                sql = "UPDATE idp_external_req SET is_expense_approve = null , expense_remark = '" & addslashes(txtexpense_remark.Text) & "' , expense_update_by = '" & Session("user_fullname").ToString & "' , expense_last_update = GETDATE() WHERE idp_id = " & id
            Else
                sql = "UPDATE idp_external_req SET is_expense_approve = '" & txtexpense_status.SelectedValue & "' , expense_remark = '" & addslashes(txtexpense_remark.Text) & "' , expense_update_by = '" & Session("user_fullname").ToString & "' , expense_last_update = GETDATE() WHERE idp_id = " & id
            End If

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If


            conn.setDBCommit()
            '  bindDetail()
            bindExpense2()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub onCheckAll()

        Dim i As Integer


        Dim chk As CheckBox
        Dim h_chk As CheckBox
        h_chk = CType(GridExpense.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)
        i = GridExpense.Rows.Count

        Try

            For s As Integer = 0 To i - 1


                chk = CType(GridExpense.Rows(s).FindControl("chk"), CheckBox)


                If h_chk.Checked Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If


            Next s


        Catch ex As Exception

            Response.Write(ex.Message)
        End Try
    End Sub

    Sub onCheckAll2()

        Dim i As Integer


        Dim chk As CheckBox
        Dim h_chk As CheckBox
        h_chk = CType(GridExpense2.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)
        i = GridExpense2.Rows.Count

        Try

            For s As Integer = 0 To i - 1


                chk = CType(GridExpense2.Rows(s).FindControl("chk"), CheckBox)


                If h_chk.Checked Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If


            Next s


        Catch ex As Exception

            Response.Write(ex.Message)
        End Try
    End Sub

   

    Protected Sub cmdUploadFileExpense_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUploadFileExpense.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim filename As String()
        Dim ds As New DataSet

        Try


            If Not IsNothing(FileUpload3.PostedFile) Then
                Dim strFileName = FileUpload3.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)
                Try
                    sql = "SELECT ISNULL(MAX(trainging_file_id),0) + 1 AS pk FROM idp_trainging_file"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                    Return
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO idp_trainging_file (trainging_file_id , idp_id , file_training_type ,  file_name , file_path , file_size ) VALUES("
                sql &= "" & pk & " , "
                sql &= "" & id & " , "
                sql &= "" & 2 & " , "
                sql &= "'" & strFileName & "' , "
                sql &= "'" & pk & "." & extension & "' , "
                sql &= "'" & FileUpload3.PostedFile.ContentLength & "' "

                sql &= ")"
                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                'Response.Write("pk = " & pk)
                If errorMsg <> "" Then
                    Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                End If

                FileUpload3.PostedFile.SaveAs(Server.MapPath("../share/idp/hr/" & pk & "." & extension))
                If File.Exists(Server.MapPath("../share/idp/hr/" & pk & "." & extension)) = False Then
                    'Response.Write("x")
                    conn.setDBRollback()
                    Return
                End If

                conn.setDBCommit()

                bindFileExpense()
            End If

            '  bindFileTRD()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

   
    Protected Sub cmdDeleteFileExpense_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteFileExpense.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim lblFilePath As Label

        i = GridFileExpense.Rows.Count

        Try

            For s As Integer = 0 To i - 1
                ' Response.Write(s)
                lbl = CType(GridFileExpense.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridFileExpense.Rows(s).FindControl("chkSelect"), CheckBox)

                ' Response.Write(lbl.Text)
                If chk.Checked Then
                    sql = "DELETE FROM idp_trainging_file WHERE trainging_file_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                        'Exit For
                    End If
                End If
                ' Response.Write("rrr")

            Next s

            For s As Integer = 0 To i - 1
                chk = CType(GridFileExpense.Rows(s).FindControl("chkSelect"), CheckBox)
                If chk.Checked Then
                    lblFilePath = CType(GridFileExpense.Rows(s).FindControl("lblFilePath"), Label)
                    File.Delete(Server.MapPath("../share/idp/hr/" & lblFilePath.Text))
                End If
            Next s

            conn.setDBCommit()
            bindFileExpense()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindEmployeeAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM user_profile WHERE user_fullname IS NOT NULL AND emp_code NOT IN (SELECT emp_code FROM idp_training_employee WHERE idp_id = " & id & ") "
            If txtint_department.SelectedValue = "" And txtint_jobtitle.SelectedValue = "" And txtint_jobtype.SelectedValue = "" And txtfindemployee.Text = "" Then
                sql &= " AND 1 > 2 "
            End If

            If txtint_department.SelectedValue <> "" Then
                sql &= " AND dept_id = " & txtint_department.SelectedValue
            End If

            If txtint_jobtitle.SelectedValue <> "" Then
                sql &= " AND job_title LIKE '%" & addslashes(txtint_jobtitle.SelectedValue) & "' "
            End If

            If txtint_jobtype.SelectedValue <> "" Then
                sql &= " AND job_type LIKE '%" & addslashes(txtint_jobtype.SelectedValue) & "' "
            End If

            If txtfindemployee.Text <> "" Then
                sql &= " AND (user_fullname LIKE '%" & txtfindemployee.Text & "%' "
                sql &= " OR emp_code LIKE '%" & txtfindemployee.Text & "%') "
            End If

            sql &= " ORDER BY user_fullname "
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblDivisionAll.DataSource = ds
            lblDivisionAll.DataBind()

            ' txtadd_topic.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindEmployeeSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_employee a INNER JOIN user_profile b ON a.emp_code = b.emp_code WHERE 1 = 1 "

            sql &= " AND a.idp_id = " & id


            sql &= " ORDER BY a.emp_name_th "
            '   Response.Write(sql)
            '  Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblDivisionSelect.DataSource = ds
            lblDivisionSelect.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCCAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM user_dept WHERE 1 = 1 AND dept_id NOT IN (SELECT costcenter_id FROM idp_training_costcenter WHERE idp_id = " & id & ") "
          
            sql &= " ORDER BY dept_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblCCAll.DataSource = ds
            lblCCAll.DataBind()

            txtint_department.DataSource = ds
            txtint_department.DataBind()
            txtint_department.Items.Insert(0, New ListItem("-- Please Select", ""))
            ' txtadd_topic.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCCSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_costcenter a INNER JOIN user_dept b ON a.costcenter_id = b.dept_id WHERE 1 = 1 "
            sql &= " AND a.idp_id = " & id
            sql &= " ORDER BY b.dept_name_en "
            '   Response.Write(sql)
            '  Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblCCSelect.DataSource = ds
            lblCCSelect.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTypeAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_type FROM user_profile WHERE 1 = 1 AND job_type NOT IN (SELECT job_type_name_en FROM idp_training_jobtype WHERE idp_id = " & id & ")"
            If txtfind_jobtype.Text <> "" Then
                sql &= " AND LOWER(job_type) LIKE '%" & txtfind_jobtype.Text.ToLower & "%' "
            End If
            sql &= " GROUP BY job_type ORDER BY job_type"

            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblJobTypeAll.DataSource = ds
            lblJobTypeAll.DataBind()

            txtint_jobtype.DataSource = ds
            txtint_jobtype.DataBind()
            txtint_jobtype.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTypeSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_type_name_en AS job_type FROM idp_training_jobtype WHERE idp_id = " & id & "  ORDER BY job_type_name_en"
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblJobTypeSelect.DataSource = ds
            lblJobTypeSelect.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTitleAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_title FROM user_profile WHERE 1 = 1 AND  job_title NOT IN (SELECT job_title_en FROM idp_training_jobtitle WHERE idp_id = " & id & ")"
            If txtfind_jobtitle.Text <> "" Then
                sql &= " AND LOWER(job_title) LIKE '%" & txtfind_jobtitle.Text.ToLower & "%' "
            End If
            sql &= " GROUP BY job_title ORDER BY job_title"
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblJobTitleAll.DataSource = ds
            lblJobTitleAll.DataBind()

            txtint_jobtitle.DataSource = ds
            txtint_jobtitle.DataBind()
            txtint_jobtitle.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTitleSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_title_en AS job_title FROM idp_training_jobtitle  WHERE idp_id = " & id & "  ORDER BY job_title_en"
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblJobTitleSelect.DataSource = ds
            lblJobTitleSelect.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindMethodCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_method"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtadd_method1.DataSource = ds
            txtadd_method1.DataBind()

            txtadd_method1.Items.Insert(0, New ListItem("--Please Select--", "0"))


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            '  ds.Dispose()
        End Try
    End Sub

    Sub bindCategoryCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_category WHERE category_type = 'General'  "
            If viewtype = "" Then

                Try
                    If CInt(txtstatus.SelectedValue) > 1 Then

                    Else
                        sql &= " AND ISNULL(is_delete,0) = 0 "
                    End If
                Catch ex As Exception
                    sql &= " AND ISNULL(is_delete,0) = 0 AND 1 = 1 "
                End Try
            

            Else
                sql &= " AND ISNULL(is_delete,0) = 0 "
            End If

            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtadd_cat1.DataTextField = "category_name_" & lang
            txtadd_cat1.DataSource = ds
            txtadd_cat1.DataBind()

            txtadd_cat1.Items.Insert(0, New ListItem("--Please Select--", "0"))

            txtadd_extcat.DataTextField = "category_name_" & lang
            txtadd_extcat.DataSource = ds
            txtadd_extcat.DataBind()

            txtadd_extcat.Items.Insert(0, New ListItem("--Please Select--", "0"))

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTopicCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_topic WHERE ISNULL(is_delete,0) = 0 "
            If txtadd_cat1.SelectedValue <> "" Then
                sql &= " AND category_id = " & txtadd_cat1.SelectedValue
                If txtadd_cat1.SelectedValue = "4" Then ' Unit Manatory
                    ' sql &= " AND topic_id IN (SELECT topic_id FROM idp_m_topic_dept WHERE topic_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code") & ") )"
                    sql &= " AND owner_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code") & ") "
                End If
            Else
                sql &= " 1 > 2"
            End If
            sql &= " ORDER BY topic_name_th , topic_name_en "

            ds = conn.getDataSetForTransaction(sql, "t1")

            txtfind_topic.DataTextField = "topic_name_" & lang
            txtfind_topic.DataSource = ds
            txtfind_topic.DataBind()

            txtfind_topic.Items.Insert(0, New ListItem("--Please Select--", "0"))
            txtfind_topic.Items.Add(New ListItem("Other", "9999"))

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindRoom()
        Dim sql As String
        Dim ds As New DataSet

        Try
            'sql = "SELECT * FROM idp_m_room WHERE 1 = 1"

            'ds = conn.getDataSetForTransaction(sql, "t1")

            'txtsh_location.DataSource = ds
            'txtsh_location.DataBind()

            ' txtfind_expect.Items.Insert(0, New ListItem("--Please Select--", "0"))
            'txtfind_expect.Items.Add(New ListItem("Other", "9999"))

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindExpectCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' sql = "SELECT * FROM idp_m_expect WHERE 1 = 1 AND is_internal = 1 "

            sql = "SELECT * FROM idp_m_expect WHERE ISNULL(is_expect_delete,0) = 0 "

            If lang = "th" Then
                txtfind_expect.DataTextField = "expect_detail"
                sql &= " ORDER BY expect_detail"
            Else
                txtfind_expect.DataTextField = "expect_detail_en"
                sql &= " ORDER BY expect_detail_en"
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            txtfind_expect.DataSource = ds
            txtfind_expect.DataBind()

            txtfind_expect.Items.Insert(0, New ListItem("--Please Select--", "0"))
            ' txtfind_expect.Items.Add(New ListItem("Other", "9999"))

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindBudgetMaster()
        Dim sql As String
        Dim ds As New DataSet

        Try
            conn.startTransactionSQLServer()

            sql = "select d.dept_id , d.dept_name , isnull(max(d.budget_amount),0) as budget_amount , isnull(max(e.budget_request),0) as budget_request "
            sql &= "    ,  (isnull(max(d.budget_amount),0) - (isnull(max(e.budget_request),0) )) as balance"
            sql &= " from idp_trans_list b "
            sql &= " inner join idp_external_req c ON b.idp_id = c.idp_id and c.request_type = 'ext'"
            sql &= " left outer join idp_training_budget_list d on b.report_dept_id = d.dept_id"

            sql &= " left outer join (" ' request
            sql &= " select sum(case when currency_type_id = 1 then expense_value else (expense_value*exchange_rate) end) as budget_request , t2.report_dept_id from idp_training_expense t1 "
            sql &= " inner join idp_trans_list t2 on t1.idp_id = t2.idp_id"
            sql &= " inner join idp_external_req t3 on t2.idp_id = t3.idp_id and t3.request_type = 'ext' "
            sql &= " where accouting_type = 1 and year(t2.date_submit) = " & Date.Now.Year & " and ISNULL(t1.is_delete,0) = 0  and t2.status_id > 1  and t1.expense_request_type_id in (select request_id from idp_budget_request where is_request_budget = 1)"
            sql &= " group by t2.report_dept_id "
            sql &= " ) e on b.report_dept_id = e.report_dept_id"

            'sql &= " left outer join (" ' approve
            'sql &= " select sum(expense_value) as budget_approve , t2.report_dept_id from idp_training_expense t1 "
            'sql &= " inner join idp_trans_list t2 on t1.idp_id = t2.idp_id"
            ' sql &= " inner join idp_external_req t3 on t1.idp_id = t3.idp_id and t3.is_budget_approve = 1"
            'sql &= " where accouting_type = 1 and year(t2.date_submit) =  " & Date.Now.Year & " and ISNULL(t1.is_delete,0) = 0 group by t2.report_dept_id "
            'sql &= " ) f on b.report_dept_id = f.report_dept_id"

            sql &= " where ISNULL(b.is_cancel, 0) = 0 And ISNULL(b.is_delete, 0) = 0 And ISNULL(b.is_ladder, 0) = 0 and b.status_id > 1 and d.year_budget = " & Date.Now.Year
            sql &= " and d.dept_id = " & lblCostcenter.Text
            sql &= " group by d.dept_id , d.dept_name"

            ds = conn.getDataSetForTransaction(sql, "t1")

            If (ds.Tables("t1").Rows.Count > 0) Then
                If (CDbl(ds.Tables("t1").Rows(0)("balance").ToString()) < 0) Then
                    lblBudgetBalance.ForeColor = Drawing.Color.Red
                Else
                    lblBudgetBalance.ForeColor = Drawing.Color.Green
                End If
                lblBudgetBalance.Text = FormatNumber(ds.Tables("t1").Rows(0)("balance").ToString(), 2)
                lblBudgetApprove.Text = FormatNumber(ds.Tables("t1").Rows(0)("budget_amount").ToString(), 2)
                lblBudgetRequest.Text = FormatNumber(ds.Tables("t1").Rows(0)("budget_request").ToString(), 2)
            Else
                lblBudgetBalance.Text = 0
                lblBudgetApprove.Text = 0
                lblBudgetRequest.Text = 0
            End If

            lblBudgetYear.Text = Date.Now.Year
            conn.setDBCommit()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindExpectExternalCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_expect WHERE 1 = 1 AND is_external =  1 "

            ds = conn.getDataSetForTransaction(sql, "t1")
            If lang = "th" Then
                txtexpect_detail.DataValueField = "expect_detail"
                txtexpect_detail.DataTextField = "expect_detail"
            Else
                txtexpect_detail.DataValueField = "expect_detail_en"
                txtexpect_detail.DataTextField = "expect_detail_en"
            End If
            txtexpect_detail.DataSource = ds
            txtexpect_detail.DataBind()

            txtexpect_detail.Items.Insert(0, New ListItem("--Please Select--", ""))


            sql = "SELECT * FROM idp_m_expect WHERE ISNULL(is_expect_delete , 0) = 0  "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If lang = "th" Then
                txtnewgoal.DataValueField = "expect_id"
                txtnewgoal.DataTextField = "expect_detail"

                txtgoal_action.DataValueField = "expect_id"
                txtgoal_action.DataTextField = "expect_detail"
            Else
                txtnewgoal.DataValueField = "expect_id"
                txtnewgoal.DataTextField = "expect_detail_en"

                txtgoal_action.DataValueField = "expect_id"
                txtgoal_action.DataTextField = "expect_detail_en"
            End If
            txtnewgoal.DataSource = ds
            txtnewgoal.DataBind()

            txtnewgoal.Items.Insert(0, New ListItem("--Please Select--", ""))

            txtgoal_action.DataSource = ds
            txtgoal_action.DataBind()

            txtgoal_action.Items.Insert(0, New ListItem("--Please Select--", ""))

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindExpenseTpye() ' รายการค่าใช้จ่ายที่ต้องการเบิกจากบัญชี
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , expense_name_th + '/' + expense_name_en AS expense_name FROM idp_m_expense_type WHERE 1 = 1"
            If req = "ext" Then
                sql &= " AND category_code = 'ext'"
            Else
                sql &= " AND category_code = 'int'"
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            If lang = "th" Then
                txtadd_expense_type.DataTextField = "expense_name"
                txtadd_expense_type2.DataTextField = "expense_name"
            Else
                txtadd_expense_type.DataTextField = "expense_name_en"
                txtadd_expense_type2.DataTextField = "expense_name_en"
            End If
            txtadd_expense_type.DataSource = ds
            txtadd_expense_type2.DataSource = ds
            txtadd_expense_type.DataBind()


            txtadd_expense_type2.DataBind()

            txtadd_expense_type.Items.Insert(0, New ListItem("--Please Select--", ""))
            txtadd_expense_type2.Items.Insert(0, New ListItem("--Please Select--", ""))

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub txtadd_cat1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtadd_cat1.SelectedIndexChanged
        txtadd_topic1.Visible = False
        txtadd_expect1.Visible = False
        txtadd_topic1.Text = ""
        AutoCompleteExtender1.ContextKey = txtadd_cat1.SelectedValue
        bindTopicCombo()
    End Sub

    Protected Sub txtfind_topic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfind_topic.SelectedIndexChanged
        If txtfind_topic.SelectedValue = "9999" Then
            txtadd_topic1.Visible = True
            txtadd_topic1.Text = ""
        Else
            txtadd_topic1.Visible = False
            txtadd_topic1.Text = txtfind_topic.SelectedItem.Text
        End If

        If txtfind_topic.SelectedIndex = 0 Then
            txtadd_topic1.Text = ""
        End If
    End Sub

    Protected Sub txtfind_expect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfind_expect.SelectedIndexChanged
        If txtfind_expect.SelectedValue = "9999" Then
            txtadd_expect1.Visible = True
            txtadd_expect1.Text = ""
        Else
            txtadd_expect1.Visible = False
            txtadd_expect1.Text = txtfind_expect.SelectedItem.Text
        End If

        If txtfind_expect.SelectedIndex = 0 Then
            txtadd_expect1.Text = ""
        End If
    End Sub

    Protected Sub cmdSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        While lblDivisionAll.Items.Count > 0 AndAlso lblDivisionAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblDivisionAll.SelectedItem
            selectedItem.Selected = False
            lblDivisionSelect.Items.Add(selectedItem)
            lblDivisionAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        While lblDivisionSelect.Items.Count > 0 AndAlso lblDivisionSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblDivisionSelect.SelectedItem
            selectedItem.Selected = False
            lblDivisionAll.Items.Add(selectedItem)
            lblDivisionSelect.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdCCAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCCAdd.Click
        While lblCCAll.Items.Count > 0 AndAlso lblCCAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblCCAll.SelectedItem
            selectedItem.Selected = False
            lblCCSelect.Items.Add(selectedItem)
            lblCCAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub lblCCRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCCRemove.Click
        While lblCCSelect.Items.Count > 0 AndAlso lblCCSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblCCSelect.SelectedItem
            selectedItem.Selected = False
            lblCCAll.Items.Add(selectedItem)
            lblCCSelect.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdAddType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddType.Click
        While lblJobTypeAll.Items.Count > 0 AndAlso lblJobTypeAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblJobTypeAll.SelectedItem
            selectedItem.Selected = False
            lblJobTypeSelect.Items.Add(selectedItem)
            lblJobTypeAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveType.Click
        While lblJobTypeSelect.Items.Count > 0 AndAlso lblJobTypeSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblJobTypeSelect.SelectedItem
            selectedItem.Selected = False
            lblJobTypeAll.Items.Add(selectedItem)
            lblJobTypeSelect.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdAddTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTitle.Click
        While lblJobTitleAll.Items.Count > 0 AndAlso lblJobTitleAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblJobTitleAll.SelectedItem
            selectedItem.Selected = False
            lblJobTitleSelect.Items.Add(selectedItem)
            lblJobTitleAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveTitle.Click
        While lblJobTitleSelect.Items.Count > 0 AndAlso lblJobTitleSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblJobTitleSelect.SelectedItem
            selectedItem.Selected = False
            lblJobTitleAll.Items.Add(selectedItem)
            lblJobTitleSelect.Items.Remove(selectedItem)
        End While
    End Sub

    Sub bindInternalRelateIDP()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_relate_idp WHERE 1 = 1 "
            If mode = "add" Then
                sql &= " AND session_id = '" & session_id & "' "
            ElseIf mode = "edit" Then
                sql &= " AND idp_id = " & id
            End If
            sql &= " ORDER BY is_required DESC , order_sort ASC"
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridFunction.DataSource = ds
            GridFunction.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAddTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim new_order As String

        Try
            sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM idp_training_relate_idp WHERE"
            If mode = "add" Then
                sql &= " session_id = '" & session_id & "'"
            Else
                sql &= " idp_id = " & id
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If
            new_order = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("relate_id", "idp_training_relate_idp", conn)

            sql = "INSERT INTO idp_training_relate_idp ( idp_id , session_id , is_required , category_id , topic_id "
            sql &= ", category_name , topic_name , expect_detail , method_id , methodology , outcome , measure "
            sql &= " , remark , order_sort"
            sql &= ") VALUES("
            '  sql &= "'" & pk & "' ,"
            If mode = "add" Then
                sql &= " null ,"
                sql &= "'" & session_id & "' ,"
            Else
                sql &= "'" & id & "' ,"
                sql &= " null ,"
            End If
            sql &= "'" & 0 & "' ,"
            sql &= "'" & txtadd_cat1.SelectedValue & "' ,"
            sql &= "'" & txtfind_topic.SelectedValue & "' ,"
            sql &= "'" & txtadd_cat1.SelectedItem.Text & "' ,"
            sql &= "'" & addslashes(txtadd_topic1.Text) & "' ,"
            sql &= "'" & addslashes(txtadd_expect1.Text) & "' ,"
            sql &= "'" & txtadd_method1.SelectedValue & "' ,"
            sql &= "'" & txtadd_method1.SelectedItem.Text & "' ,"
            sql &= "'" & addslashes(txtadd_outcome.Text) & "' ,"
            sql &= "'" & addslashes(txtadd_measure.Text) & "', "
            sql &= "'" & addslashes(txtadd_remark.Text) & "' ,"
            sql &= "'" & new_order & "' "
            sql &= ")"
            '  txtadd_remark.Text = sql
            ' Return

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            ' Response.Write(sql)

            conn.setDBCommit()
            bindInternalRelateIDP()

            txtadd_cat1.SelectedIndex = 0
            txtfind_topic.SelectedIndex = 0
            txtadd_topic1.Text = ""
            txtfind_expect.SelectedIndex = 0
            txtadd_expect1.Text = ""
            txtadd_method1.SelectedIndex = 0
            txtadd_measure.Text = ""
            txtadd_outcome.Text = ""
            txtadd_remark.Text = ""

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridFunction.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridFunction.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridFunction.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM idp_training_relate_idp WHERE relate_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindInternalRelateIDP()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindSpeaker()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_speaker WHERE 1 = 1 "
            If mode = "add" Then
                sql &= " AND session_id = '" & session_id & "' "
            ElseIf mode = "edit" Then
                sql &= " AND idp_id = " & id
            End If
            sql &= " ORDER BY order_sort ASC"
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridSpeaker.DataSource = ds
            GridSpeaker.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAddSpeaker_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddSpeaker.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim new_order As String

        Try
            sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM idp_training_speaker WHERE"
            If mode = "add" Then
                sql &= " session_id = '" & session_id & "'"
            Else
                sql &= " idp_id = " & id
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If
            new_order = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("speaker_id", "idp_training_speaker", conn)

            sql = "INSERT INTO idp_training_speaker (speaker_id , idp_id , session_id , title , fname , lname "
            sql &= ", company , tax_id , remark , speaker_hour  "
            sql &= " , order_sort"
            sql &= ") VALUES("
            sql &= "'" & pk & "' ,"
            If mode = "add" Then
                sql &= " null ,"
                sql &= "'" & session_id & "' ,"
            Else
                sql &= "'" & id & "' ,"
                sql &= " null ,"
            End If
            sql &= "'" & addslashes(txtspk_title.Text) & "' ,"
            sql &= "'" & addslashes(txtspk_fname.Text) & "' ,"
            sql &= "'" & addslashes(txtspk_lname.Text) & "' ,"
            sql &= "'" & addslashes(txtspk_company.Text) & "' ,"
            sql &= "'" & addslashes(txtspk_tax.Text) & "' ,"
            sql &= "'" & addslashes(txtspk_remark.Text) & "' ,"
            sql &= "'" & txtadd_hour.Text & "' ,"
            sql &= "'" & new_order & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            ' Response.Write(sql)

            conn.setDBCommit()
            bindSpeaker()

            txtspk_title.Text = ""
            txtspk_fname.Text = ""

            txtspk_company.Text = ""
            txtspk_tax.Text = ""
            txtspk_remark.Text = ""

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub



    Sub bindSchedule()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_schedule WHERE ISNULL(is_delete , 0 ) = 0 "
            If mode = "add" Then
                sql &= " AND session_id = '" & session_id & "' "
            ElseIf mode = "edit" Then
                sql &= " AND idp_id = " & id
            End If
            sql &= " ORDER BY order_sort ASC"
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridSchedule.DataSource = ds
            GridSchedule.DataBind()

            If ds.Tables("t1").Rows.Count <= 0 Then
                cmdSaveSchedule.Visible = False
                cmdDelSchedule.Visible = False
            Else
                cmdSaveSchedule.Visible = True
                cmdDelSchedule.Visible = True
            End If
        Catch ex As Exception
            Response.Write("bindSchedule :: " & ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAddSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddSchedule.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim new_order As String

        Try
            sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM idp_training_schedule WHERE"
            If mode = "add" Then
                sql &= " session_id = '" & session_id & "'"
            Else
                sql &= " idp_id = " & id
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If
            new_order = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("schedule_id", "idp_training_schedule", conn)

            sql = "INSERT INTO idp_training_schedule (schedule_id , idp_id , session_id , schedule_start , schedule_start_ts , schedule_end "
            sql &= ", schedule_end_ts , schedule_type , location   "
            sql &= ", max_attendee , is_open , create_date , create_date_ts , create_by , order_sort "
            sql &= ") VALUES("
            sql &= "'" & pk & "' ,"
            If mode = "add" Then
                sql &= " null ,"
                sql &= "'" & session_id & "' ,"
            Else
                sql &= "'" & id & "' ,"
                sql &= " null ,"
            End If
            sql &= "'" & convertToSQLDatetime(txtsh_date1.Text, txthour_sh1.SelectedValue, txtmin_sh1.SelectedValue) & "' ,"
            sql &= "'" & ConvertDateStringToTimeStamp(txtsh_date1.Text, CInt(txthour_sh1.SelectedValue), CInt(txtmin_sh1.SelectedValue)) & "' ,"
            sql &= "'" & convertToSQLDatetime(txtsh_date2.Text, txthour_sh2.SelectedValue, txtmin_sh2.SelectedValue) & "' ,"
            sql &= "'" & ConvertDateStringToTimeStamp(txtsh_date2.Text, CInt(txthour_sh2.SelectedValue), CInt(txtmin_sh2.SelectedValue)) & "' ,"
            sql &= "'" & addslashes(txtsh_type.SelectedItem.Text) & "' ,"
            sql &= "'" & addslashes(txtsh_location.Text) & "' ,"
            sql &= "'" & txtadd_attendee.Text & "' ,"
            sql &= "'" & 0 & "' ,"
            sql &= " GETDATE() ,"
            sql &= "'" & Date.Now.Ticks & "' ,"
            sql &= "'" & Session("user_fullname").ToString & "' ,"
            sql &= "'" & new_order & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            ' Response.Write(sql)

            conn.setDBCommit()
            bindSchedule()

            'txtsh_date1.Text = ""
            'txtsh_date2.Text = ""

            'txtsh_location.SelectedIndex = 0
            'txtsh_type.SelectedIndex = 0
            'txthour_sh1.SelectedIndex = 0
            'txthour_sh2.SelectedIndex = 0
            'txtmin_sh1.SelectedIndex = 0
            'txtmin_sh2.SelectedIndex = 0
            'txtadd_attendee.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdFileInternal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileInternal.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim filename As String()
        Dim ds As New DataSet

        Try


            If Not IsNothing(FileUpload4.PostedFile) Then
                Dim strFileName = FileUpload4.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)
                Try
                    sql = "SELECT ISNULL(MAX(file_id),0) + 1 AS pk FROM idp_attachment"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO idp_attachment (file_id , ir_id ,  file_name , file_path , file_size , session_id) VALUES("
                sql &= "" & pk & " , "
                If id = "" Then
                    sql &= " null , "
                Else
                    sql &= "" & id & " , "
                End If

                sql &= "'" & strFileName & "' , "
                sql &= "'" & pk & "." & extension & "' , "
                sql &= "'" & FileUpload4.PostedFile.ContentLength & "' , "
                sql &= "'" & Session.SessionID & "'  "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                'Response.Write("pk = " & pk)
                If errorMsg <> "" Then
                    Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                End If

                FileUpload4.PostedFile.SaveAs(Server.MapPath("../share/idp/attach_file/" & pk & "." & extension))

                conn.setDBCommit()
            End If

            bindFileInternal()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim lblFilePath As Label

        i = GridFileInternal.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridFileInternal.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridFileInternal.Rows(s).FindControl("chkSelect"), CheckBox)

                ' Response.Write(lbl.Text)
                If chk.Checked Then
                    sql = "DELETE FROM idp_attachment WHERE file_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                        'Exit For
                    End If
                End If
                ' Response.Write("rrr")

            Next s

            For s As Integer = 0 To i - 1
                chk = CType(GridFileInternal.Rows(s).FindControl("chkSelect"), CheckBox)


                If chk.Checked Then
                    lblFilePath = CType(GridFileInternal.Rows(s).FindControl("lblFilePath"), Label)
                    File.Delete(Server.MapPath("../share/idp/attach_file/" & lblFilePath.Text))
                End If
            Next s

            conn.setDBCommit()
            bindFileInternal()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdDelSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelSchedule.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridSchedule.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridSchedule.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridSchedule.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "UPDATE idp_training_schedule SET is_delete = 1 WHERE schedule_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindSchedule()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdDelSpeaker_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelSpeaker.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridSpeaker.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridSpeaker.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridSpeaker.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM  idp_training_speaker  WHERE speaker_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindSpeaker()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdThai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdThai.Click
        Session("lang") = "th"
        Response.Redirect("ext_training_detail.aspx?mode=" & mode & "&id=" & id & "&lang=th&req=" & req)
    End Sub

    Protected Sub cmdEng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEng.Click
        Session("lang") = "en"
        Response.Redirect("ext_training_detail.aspx?mode=" & mode & "&id=" & id & "&lang=en&req=" & req)
    End Sub

    Protected Sub GridSchedule_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridSchedule.PageIndexChanging
        GridSchedule.PageIndex = e.NewPageIndex
        bindSchedule()
    End Sub

    Protected Sub GridSchedule_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridSchedule.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Dim txtroom As DropDownList = CType(e.Row.FindControl("txtroom"), DropDownList)
            Dim lblLocation As Label = CType(e.Row.FindControl("lblLocation"), Label)

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblAttendee As TextBox = CType(e.Row.FindControl("lblAttendee"), TextBox)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            Dim txtStatus As DropDownList = CType(e.Row.FindControl("txtStatus"), DropDownList)
            Dim cmdRegister As Button = CType(e.Row.FindControl("cmdRegister"), Button)
        
            Dim lblTimeStart As Label = CType(e.Row.FindControl("lblTimeStart"), Label)
            Dim lblTimeStart2 As Label = CType(e.Row.FindControl("lblTimeStart2"), Label)
            Dim txthour_ingrid_sh1 As DropDownList = CType(e.Row.FindControl("txthour_ingrid_sh1"), DropDownList)
            Dim txthour_ingrid_sh2 As DropDownList = CType(e.Row.FindControl("txthour_ingrid_sh2"), DropDownList)

            Dim lblMinStart As Label = CType(e.Row.FindControl("lblMinStart"), Label)
            Dim lblMinStart2 As Label = CType(e.Row.FindControl("lblMinStart2"), Label)
            Dim txtmin_ingrid_sh1 As DropDownList = CType(e.Row.FindControl("txtmin_ingrid_sh1"), DropDownList)
            Dim txtmin_ingrid_sh2 As DropDownList = CType(e.Row.FindControl("txtmin_ingrid_sh2"), DropDownList)

            Dim txttype As DropDownList = CType(e.Row.FindControl("txttype"), DropDownList)
            Dim lbltype As Label = CType(e.Row.FindControl("lbltype"), Label)
            Dim lblEvaluate As Label = CType(e.Row.FindControl("lblEvaluate"), Label)

            Dim chkOnline As CheckBox = CType(e.Row.FindControl("chkOnline"), CheckBox)
            Dim lblOnline As Label = CType(e.Row.FindControl("lblOnline"), Label)

            cmdRegister.Attributes.Add("onclick", " openPopupRegister('" & lblPK.Text & "');return false;")
            '  cmdRegister.Attributes.Add("style", "text-align:right")
            lblAttendee.Attributes.Add("onfocus", "this.select();")
            lblAttendee.Attributes.Add("style", "text-align:right")

            txthour_ingrid_sh1.SelectedValue = lblTimeStart.Text
            txtmin_ingrid_sh1.SelectedValue = lblMinStart.Text

            txthour_ingrid_sh2.SelectedValue = lblTimeStart2.Text
            txtmin_ingrid_sh2.SelectedValue = lblMinStart2.Text

            If lblStatus.Text <> "" Then
                txtStatus.SelectedValue = lblStatus.Text
            End If

            If lblOnline.Text = "1" Then
                chkOnline.Checked = True
            Else
                chkOnline.Checked = False
            End If

            txttype.SelectedValue = lbltype.Text

            Dim sql As String = ""
            Dim ds As New DataSet
            Dim ds2 As New DataSet
            Dim num As String = ""
            Try

                sql = "SELECT * FROM idp_training_registered WHERE schedule_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                cmdRegister.Text = "ผู้ลงทะเบียน (" & ds.Tables("t1").Rows.Count & "/" & lblAttendee.Text & ")"

             
                sql = "SELECT * FROM idp_evaluation_list WHERE schedule_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t2")
                If ds.Tables("t2").Rows.Count > 0 Then
                    lblEvaluate.Visible = True
                Else
                    lblEvaluate.Visible = False
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds2.Dispose()
            End Try

        End If
    End Sub

  

    Protected Sub GridSchedule_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridSchedule.SelectedIndexChanged

    End Sub

   
 
    Protected Sub cmdSaveSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveSchedule.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim txtstatus As DropDownList
        Dim lblAttendee As TextBox

        Dim lblDate1 As Label
        Dim lblDate2 As Label

        Dim h1 As DropDownList
        Dim h2 As DropDownList
        Dim m1 As DropDownList
        Dim m2 As DropDownList
        '  Dim txtroom As DropDownList

        Dim txttype As DropDownList
        Dim txtorder As TextBox
        Dim txtonline As CheckBox

        i = GridSchedule.Rows.Count
        Dim pk As String = ""
        Try

            For s As Integer = 0 To i - 1
                chk = CType(GridSchedule.Rows(s).FindControl("chk"), CheckBox)
                lblAttendee = CType(GridSchedule.Rows(s).FindControl("lblAttendee"), TextBox)
                txtstatus = CType(GridSchedule.Rows(s).FindControl("txtstatus"), DropDownList)
                lbl = CType(GridSchedule.Rows(s).FindControl("lblPk"), Label)

                lblDate1 = CType(GridSchedule.Rows(s).FindControl("lblDate1"), Label)
                lblDate2 = CType(GridSchedule.Rows(s).FindControl("lblDate2"), Label)
                h1 = CType(GridSchedule.Rows(s).FindControl("txthour_ingrid_sh1"), DropDownList)
                h2 = CType(GridSchedule.Rows(s).FindControl("txthour_ingrid_sh2"), DropDownList)
                m1 = CType(GridSchedule.Rows(s).FindControl("txtmin_ingrid_sh1"), DropDownList)
                m2 = CType(GridSchedule.Rows(s).FindControl("txtmin_ingrid_sh2"), DropDownList)
                txttype = CType(GridSchedule.Rows(s).FindControl("txttype"), DropDownList)
                '   txtroom = CType(GridSchedule.Rows(s).FindControl("txtroom"), DropDownList)
                txtorder = CType(GridSchedule.Rows(s).FindControl("txtorder"), TextBox)

                txtonline = CType(GridSchedule.Rows(s).FindControl("chkOnline"), CheckBox)
                ' If chkSelect.Checked = True Then

                sql = "UPDATE idp_training_schedule SET is_open = " & txtstatus.SelectedValue
                sql &= " , schedule_start = '" & convertToSQLDatetime(lblDate1.Text, h1.SelectedValue, m1.SelectedValue) & "' "
                sql &= " , schedule_start_ts = '" & ConvertDateStringToTimeStamp(lblDate1.Text, h1.SelectedValue, m1.SelectedValue) & "' "
                sql &= " , schedule_end = '" & convertToSQLDatetime(lblDate2.Text, h2.SelectedValue, m2.SelectedValue) & "' "
                sql &= " , schedule_end_ts = '" & ConvertDateStringToTimeStamp(lblDate2.Text, h2.SelectedValue, m2.SelectedValue) & "' "
                sql &= " , max_attendee = '" & lblAttendee.Text & "' "
                sql &= " , schedule_type = '" & txttype.SelectedItem.Text & "' "
                '   sql &= " , location = '" & txtroom.SelectedItem.Text & "' "
                sql &= " , order_sort = '" & txtorder.Text & "' "
                If txtonline.Checked Then
                    sql &= " , is_online = 1 "
                Else
                    sql &= " , is_online = 0 "
                End If

                sql &= " WHERE schedule_id = " & lbl.Text
                'Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)

                End If


                '  updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
                ' End If
            Next s

            conn.setDBCommit()
            bindSchedule()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("row databound " & ex.Message)
        End Try
    End Sub

    Sub bindIDPCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_trans_list WHERE idp_id NOT IN (SELECT idp_id FROM idp_external_req) AND status_id >= 2 AND report_emp_code = " & Session("emp_code").ToString

            ds = conn.getDataSetForTransaction(sql, "t1")
       
            txtrelate_idpno.DataSource = ds
            txtrelate_idpno.DataBind()

            txtrelate_idpno.Items.Insert(0, New ListItem("--Please Select--", ""))


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    
    Protected Sub cmdAddComment_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdAddComment.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try
            Dim login_max_authen As Integer = CInt(getMyIPDLevel())
            pk = getPK("comment_id", "idp_manager_comment", conn)
            sql = "INSERT INTO idp_manager_comment (comment_id , idp_id , comment_status_id , comment_status_name , subject_id , subject , detail "
            sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
            sql &= ",create_date , create_date_ts , review_by_role_id"
            sql &= ") VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & txtacknowedge_status.SelectedValue & "' ,"
            sql &= " '" & txtacknowedge_status.SelectedItem.Text & "' ,"
            sql &= " '" & txtcomment.SelectedValue & "' ,"
            sql &= " '" & txtcomment.SelectedItem.Text & "' ,"
            sql &= " '" & addslashes(txtcomment_detail.Value) & "' ,"
            sql &= " '" & Session("job_title").ToString & "' ,"
            sql &= " '" & Session("user_position").ToString & "' ,"
            sql &= " '" & Session("user_fullname").ToString & "' ,"
            sql &= " '" & Session("emp_code").ToString & "' ,"
            sql &= " '" & Session("dept_name").ToString & "' ,"
            sql &= " '" & Session("dept_id").ToString & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' ,"
            sql &= " '" & login_max_authen & "' "

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            updateOnlyLog("0", txtacknowedge_status.SelectedItem.Text)
            conn.setDBCommit()

            bindCommentList()
            bindGridIDPLog()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdSaveGoal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveGoal.Click

        Try
            updateGoal()
            conn.setDBCommit()

            bindDetail()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGoal()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_goal WHERE idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count = 0 Then
                cmdDeleteGoal.Visible = False
            Else
                cmdDeleteGoal.Visible = True
            End If

            GridGoal.DataSource = ds
            GridGoal.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdAddGoal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddGoal.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            pk = getPK("goal_id", "idp_training_goal", conn)
            sql = "INSERT INTO idp_training_goal (goal_id , idp_id , action_detail , person_detail , start_date , start_date_ts"
            sql &= " , end_date , end_date_ts , action_status , action_status_name , create_date , expect_goal_id , expect_goal_text ) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & addslashes(txtadd_action.Text) & "' ,"
            sql &= " '" & addslashes(txtadd_whom.Text) & "' ,"
            sql &= " '" & convertToSQLDatetime(txtadd_date1.Text) & "' ,"
            sql &= " '" & ConvertDateStringToTimeStamp(txtadd_date1.Text) & "' ,"
            sql &= " '" & convertToSQLDatetime(txtadd_date2.Text) & "' ,"
            sql &= " '" & ConvertDateStringToTimeStamp(txtadd_date2.Text) & "' ,"
            sql &= " '" & addslashes(txtadd_status.SelectedValue) & "' ,"
            sql &= " '" & addslashes(txtadd_status.SelectedItem.Text) & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & txtgoal_action.SelectedValue & "' ,"
            sql &= " '" & txtgoal_action.SelectedItem.Text & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            updateGoal()

            conn.setDBCommit()

            bindGoal()
            bindDetail()
            txtadd_action.Text = ""
            txtadd_whom.Text = ""
            txtadd_date1.Text = ""
            txtadd_date2.Text = ""
            txtadd_status.Text = ""


        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        End Try

        ' Response.Redirect("ext_training_detail.aspx?mode=edit&id=" & id & "&req=ext")
    End Sub

    Protected Sub cmdDeleteGoal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteGoal.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridGoal.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridGoal.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridGoal.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM idp_training_goal WHERE goal_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindGoal()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub getMailAndSMS()
        Dim sql As String
        Dim ds As New DataSet
        Dim emp_code() As String
        Dim email_list() As String
        Dim cc_list() As String
        Dim bcc_list() As String
        Dim n As Integer
        Dim title As String = ""
        Try

            email_list = txtidselect.Value.Split(",") ' create emailTo array
            cc_list = txtidCCselect.Value.Split(",") ' create emailCC array
            bcc_list = txtidBCCSelect.Value.Split(",") ' create emailCC array
            '  Response.Write("xxxx : " & txtidselect.Value)
            '  Response.Write("yyy : " & txtidCCselect.Value)
            'For i As Integer = 0 To UBound(email_list)

            '    sendMail(email_list(i), txtsubject.SelectedItem.Text, txtmessage.Value)

            '    If chk_sms.Checked = True Then
            '        sendSMS(ds.Tables(0).Rows(i)("custom_mobile").ToString())
            '    End If
            'Next i


            '  Dim thread As New Thread(New ParameterizedThreadStart(AddressOf sendMail2))
            Dim p1() As String = email_list
            Dim p2() As String = cc_list
            Dim p3 As String = ""
            Dim p4 As String = txtmessage.Value & "<br/>" & vbCrLf
            Dim p5 = ""

            If req = "ext" Then
                p3 = "[EXT #" & lblrequest_NO.Text & " " & lblname.Text & "]  : " & txtsubject.SelectedItem.Text
                title = "external"
            Else
                p3 = "[INT #" & lblrequest_NO.Text & " " & lblname.Text & "]  : " & txtsubject.SelectedItem.Text
                title = "internal"
            End If

            ' Dim parameters As Object() = New Object() {p1, p2, p3, p4, p5}

            'thread.Start(parameters)
            Dim host As String = ConfigurationManager.AppSettings("frontHost").ToString
            Dim key = UserActivation.GetActivationLink("idp/ext_training_detail.aspx?mode=edit&id=" & id & "&req=" & req)
            Dim msgbody As String = ""

            If txtsubject.SelectedValue = "102" Then ' To Accounting
                msgbody = "<strong>" & txttitle.Value & "</strong><br/><br/>"
                msgbody &= ""
                msgbody = "The " & title & " training request is submitted for your approval. Please kindly open the following link for fast access.<br/>" & vbCrLf
                msgbody &= "<a href='http://bhtraining/login.aspx?viewtype=budget&req=" & req & "&key=" & key & "'>External training request Online</a>"
                msgbody &= "<br/> Best regard, <br/> Learning & Development Department"
            ElseIf txtsubject.SelectedValue = "4" Then ' To department
                msgbody = "<strong>" & txttitle.Value & "</strong><br/><br/>"
                msgbody &= ""
                msgbody = "The " & title & " training request is submitted for your approval. Please kindly open the following link for fast access.<br/>" & vbCrLf
                msgbody &= "<a href='http://bhtraining/login.aspx?viewtype=dept&req=" & req & "&key=" & key & "'>External training request Online</a>"
                msgbody &= "<br/> Best regard, <br/> Learning & Development Department"
            ElseIf txtsubject.SelectedValue = "5" Then ' Internal
                msgbody = "<strong>" & txttitle.Value & "</strong><br/><br/>"
                msgbody &= ""
                msgbody = "The Internal training request is submitted for your approval. Please kindly open the following link for fast access.<br/>" & vbCrLf
                msgbody &= "<a href='http://bhtraining/login.aspx?viewtype=dept&req=" & req & "&key=" & key & "'>Internal training request Online</a>"
                msgbody &= "<br/> Best regard, <br/> Learning & Development Department"
            ElseIf txtsubject.SelectedValue = "103" Then ' To expense
                msgbody = "<strong>" & txttitle.Value & "</strong><br/><br/>"
                msgbody &= ""
                msgbody = "The " & title & " training request is submitted for your approval. Please kindly open the following link for fast access.<br/>" & vbCrLf
                msgbody &= "<a href='http://bhtraining/login.aspx?viewtype=expense&req=" & req & "&key=" & key & "'>External training request Online</a>"
                msgbody &= "<br/> Best regard, <br/> Learning & Development Department"
            End If
          
            sendMailWithCC_IDP(email_list, cc_list, bcc_list, p3, txtmessage.Value & msgbody, "", "ir")

        Catch ex As Exception
            Response.Write("Send mail :: " & ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Public Sub sendMailWithCC_IDP(ByVal toEmail() As String, ByVal ccEmail() As String, ByVal bccEmail() As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal mailType As String = "ir", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Dim smtp As New SmtpClient("mail.bumrungrad.com")

        Try

            'oMsg.From = New MailAddress("traininggroup@bumrungrad.com")
            oMsg.From = New MailAddress("BumrungradPersonnelDevelopmentCenter@bumrungrad.com")
            If chkHigh.Checked = True Then
                oMsg.Headers.Add("Disposition-Notification-To", "<suneeporn@bumrungrad.com>")
            End If


            'ConfigurationManager.AppSettings("database").ToString()

            For i As Integer = 0 To UBound(toEmail)
                If toEmail(i) <> "" And toEmail(i).Length > 5 Then
                    oMsg.To.Add(New MailAddress(toEmail(i).ToLower))
                End If
            Next

            For i As Integer = 0 To UBound(ccEmail)
                If ccEmail(i) Is Nothing Or ccEmail(i) = "" Then
                Else
                    If ccEmail(i) <> "" And ccEmail(i).Length > 5 Then
                        oMsg.CC.Add(New MailAddress(ccEmail(i).ToLower))
                    End If
                End If

            Next

            oMsg.Subject = subject
            oMsg.IsBodyHtml = True
            oMsg.Body = message
            If chkHigh.Checked = True Then
                oMsg.Priority = MailPriority.High
            End If

            'Dim smtp As New SmtpClient("mail.powerpointproduct.com")
            ' SMTP Authenticate
            'smtp.Credentials = New System.Net.NetworkCredential("info@powerpointproduct.com", "natee")
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.Send(oMsg)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            oMsg = Nothing
            smtp = Nothing
        End Try


    End Sub

    Protected Sub cmdSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendMail.Click

        Try
            updateOnlyLog("0", txtsubject.SelectedItem.Text)

            If chk_sms.Checked = True Then
                prepareSMS()
                insertAlertLog("SMS")
            End If

            If txtidselect.Value <> "" Then
                getMailAndSMS()
                insertAlertLog("mail")
            End If

            conn.setDBCommit()
            bindGridAlertLog()
            txtsubject.SelectedIndex = 0
            txtsend_sms.Text = ""
            txtmessage.Value = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
      

    End Sub

    Private Sub bindGridAlertLog()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * FROM idp_alert_log a  WHERE a.idp_id = " & id)
            sqlB.Append(" ORDER BY alert_date DESC")
            ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

            '  Response.Write(sqlB.ToString)
            GridAlertLog.DataSource = ds
            GridAlertLog.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub

    Sub insertAlertLog(ByVal cmd As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet

        pk = getPK("log_alert_id", "idp_alert_log", conn)
        sql = "INSERT INTO idp_alert_log (log_alert_id , idp_id , alert_date , alert_date_ts , alert_method , subject , send_to , cc_to , sms_to) VALUES( "
        sql &= "'" & pk & "' ,"
        sql &= "'" & id & "' ,"
        sql &= "GETDATE() ,"
        sql &= "'" & Date.Now.Ticks & "' ,"
        sql &= "'" & cmd & "' ,"
        sql &= "'" & txtsubject.SelectedItem.Text & "' ,"
        sql &= "'" & addslashes(txtto.Value) & "' ,"
        sql &= "'" & addslashes(txtcc.Value) & "' ,"
        sql &= "'" & addslashes(txtsend_sms.Text) & "' "
        sql &= ")"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & "" & sql)
        End If
    End Sub

    Sub prepareSMS()
        Dim username As String = ConfigurationManager.AppSettings("smsUsername")
        Dim password As String = ConfigurationManager.AppSettings("smsPassword")
        Dim sms_list() As String
        Dim dataPack As String
        Dim result As String
        sms_list = txtsend_idsms.Value.Split(",")

        For i As Integer = 0 To UBound(sms_list)
            If sms_list(i) <> "" And sms_list(i).Length > 5 Then
                dataPack = "RefNo=" & 101 & "&Sender=" & "026670001" & "&Msn=" & sms_list(i) & "&Msg=" & txtmessage.Value & "&MsgType=E&User=" & username & "&Password=" & password
                result = sendSMS(dataPack)
                ' Response.Write(result)
                'Response.Write(dataPack)
                ' Response.Write("sms " & sendSMS(dataPack))
            End If
        Next

        ' insertAlertLog("SMS")

    End Sub

    Protected Sub txtsubject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsubject.SelectedIndexChanged

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        bindEmployeeAll()

    

    End Sub

    Protected Sub cmdSearchJobType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchJobType.Click
        bindJobTypeAll()
    End Sub

    Protected Sub cmdSearchJobTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchJobTitle.Click
        bindJobTitleAll()
    End Sub

    Protected Sub cmdUploadInternalFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUploadInternalFile.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim filename As String()
        Dim ds As New DataSet

        Try


            If Not IsNothing(FileUpload5.PostedFile) Then
                Dim strFileName = FileUpload5.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)
                Try
                    sql = "SELECT ISNULL(MAX(trainging_file_id),0) + 1 AS pk FROM idp_trainging_file"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                    Return
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try

                ' 3 = Internal Training Document
                sql = "INSERT INTO idp_trainging_file (trainging_file_id , idp_id , file_training_type ,  file_name , file_path , file_size , file_remark , session_id ) VALUES("
                sql &= "" & pk & " , "
                If mode = "add" Then
                    sql &= " null , "
                Else
                    sql &= "" & id & " , "
                End If

                sql &= "" & 3 & " , "
                sql &= "'" & strFileName & "' , "
                sql &= "'" & pk & "." & extension & "' , "
                sql &= "'" & FileUpload5.PostedFile.ContentLength & "' ,"
                sql &= "'" & addslashes(txtfile_add_remark.Text) & "' ,"
                sql &= " '" & Session.SessionID & "' "
                sql &= ")"
                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                'Response.Write("pk = " & pk)
                If errorMsg <> "" Then
                    Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                End If

                FileUpload5.PostedFile.SaveAs(Server.MapPath("../share/idp/hr/" & pk & "." & extension))
                If File.Exists(Server.MapPath("../share/idp/hr/" & pk & "." & extension)) = False Then
                    'Response.Write("x")
                    conn.setDBRollback()
                    Return
                End If

                conn.setDBCommit()

                txtfile_add_remark.Text = ""
                bindFileTrainingDocInternal()
            End If

            bindFileTRD()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Protected Sub cmdDelFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelFile.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim lblFilePath As Label

        i = GridTrainingDoc.Rows.Count

        Try

            For s As Integer = 0 To i - 1
                ' Response.Write(s)
                lbl = CType(GridTrainingDoc.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridTrainingDoc.Rows(s).FindControl("chkSelect"), CheckBox)

                ' Response.Write(lbl.Text)
                If chk.Checked Then
                    sql = "DELETE FROM idp_trainging_file WHERE trainging_file_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                        'Exit For
                    End If
                End If
                ' Response.Write("rrr")

            Next s

            For s As Integer = 0 To i - 1
                chk = CType(GridTrainingDoc.Rows(s).FindControl("chkSelect"), CheckBox)
                If chk.Checked Then
                    lblFilePath = CType(GridTrainingDoc.Rows(s).FindControl("lblFilePath"), Label)
                    File.Delete(Server.MapPath("../share/idp/hr/" & lblFilePath.Text))
                End If
            Next s

            conn.setDBCommit()
            bindFileTrainingDocInternal()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdUploadCSV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUploadCSV.Click
        Dim strFileName As String = ""

        Try


            If Not IsNothing(FileUploadCSV.PostedFile) Then
                strFileName = FileUploadCSV.FileName

                If strFileName = "" Then
                    Return
                End If


                FileUploadCSV.PostedFile.SaveAs(Server.MapPath("../share/" & FileUploadCSV.FileName))


            End If


        Catch ex As Exception

            Response.Write(ex.Message)
            Return
        Finally

        End Try


        Dim strFilesName As String = strFileName
        Dim strPath As String = "../share/"
      

        Dim Sr As New StreamReader(Server.MapPath(strPath) & strFilesName)
        Dim sb As New System.Text.StringBuilder()
        Dim s As String
        While Not Sr.EndOfStream
            s = Sr.ReadLine()
            'cmd.CommandText = (("INSERT INTO MyTable Field1, Field2, Field3 VALUES(" + s.Split(","c)(1) & ", ") + s.Split(","c)(2) & ", ") + s.Split(","c)(3) & ")"
            lblDivisionSelect.Items.Add(New ListItem(s.Split(","c)(1), s.Split(","c)(0)))

            'cmd.ExecuteNonQuery()
        End While


      
    End Sub

    Protected Sub cmdUpdateIDPRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateIDPRef.Click
        Dim sql As String
        Dim errorMsg As String = ""
        Dim pk As String = ""

        Dim ds As New DataSet

        Try

            If mode = "edit" Then
                sql = "DELETE FROM idp_training_relate_idp WHERE idp_id =  " & id
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If


            pk = getPK("relate_id", "idp_training_relate_idp", conn)

            sql = "INSERT INTO idp_training_relate_idp ( idp_id , session_id , is_required , category_id , topic_id "
            sql &= ", category_name , topic_name , expect_id , expect_detail , method_id , methodology , outcome , measure "
            sql &= " , remark , order_sort)"
            sql &= " SELECT "
            '  sql &= "'" & pk & "' ,"
            If mode = "add" Then
                sql &= " null ,"
                sql &= "'" & session_id & "' ,"
            Else
                sql &= "'" & id & "' ,"
                sql &= " null ,"
            End If
            sql &= " template_is_require , template_category_id , template_topic_id , template_category_name , template_topic_name ,"
            sql &= " template_expect_id , template_expect_detail , template_methodogy_id , template_methodogy_name ,  '' , '' , '' , template_order_sort "
            sql &= "  FROM idp_template_detail WHERE template_id = " & txtidp_relate.SelectedValue

            'Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            If txtidp_relate.SelectedIndex > 0 And mode = "edit" Then
                sql = " UPDATE idp_external_req SET "
                sql &= "  internal_idp_program_id = '" & txtidp_relate.SelectedValue & "' "
                sql &= " , internal_idp_program_name = '" & addslashes(txtidp_relate.SelectedItem.Text) & "' "
                sql &= " WHERE idp_id = " & id
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If

            conn.setDBCommit()
            bindInternalRelateIDP()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub txtsh_date1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsh_date1.TextChanged
        txtsh_date2.Text = txtsh_date1.Text
    End Sub

    Protected Sub GridGoal_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridGoal.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblActionId As Label = CType(e.Row.FindControl("lblActionId"), Label)
            Dim txtaction_status As DropDownList = CType(e.Row.FindControl("txtaction_status"), DropDownList)

            Dim lblPkAction As Label = CType(e.Row.FindControl("lblPkAction"), Label)
            Dim txtdate1 As TextBox = CType(e.Row.FindControl("txtdate1"), TextBox)
            Dim txtdate2 As TextBox = CType(e.Row.FindControl("txtdate2"), TextBox)
            Try
                txtaction_status.SelectedValue = lblActionId.Text
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub GridGoal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridGoal.SelectedIndexChanged

    End Sub

    Protected Sub GridFunction_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridFunction.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblRequire As Label = CType(e.Row.FindControl("lblRequire"), Label)

            If lblRequire.Text = "0" Then
                lblRequire.Text = "E"
            Else
                lblRequire.Text = "R"
            End If

        End If
    End Sub

    Protected Sub GridFunction_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridFunction.SelectedIndexChanged

    End Sub

    Protected Sub cmdUploadCertifica_Click(sender As Object, e As System.EventArgs) Handles cmdUploadCertifica.Click
        Dim sql As String
        Dim errorMsg As String

        Dim filename As String()
        Dim newfilename As String = ""
        Dim ds As New DataSet

        Try

            If Not IsNothing(FileUpload6.PostedFile) Then
                Dim strFileName = FileUpload6.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)
                newfilename = Date.Now.Ticks
                sql = "UPDATE idp_external_req SET cer_file_path = '" & newfilename & "." & extension & "'  "
                sql &= " , cer_file_name = '" & addslashes(strFileName) & "' "
                sql &= " WHERE idp_id = " & id

                errorMsg = conn.executeSQLForTransaction(sql)
                'Response.Write("pk = " & pk)
                If errorMsg <> "" Then
                    Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                End If

                FileUpload6.PostedFile.SaveAs(Server.MapPath("../share/idp/attach_file/" & newfilename & "." & extension))

                conn.setDBCommit()
            End If

            bindDetail()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Protected Sub cmdDelCer_Click(sender As Object, e As System.EventArgs) Handles cmdDelCer.Click
        Dim sql As String
        Dim errorMsg As String

        Try

            sql = "UPDATE idp_external_req SET cer_file_path = ''  "
            sql &= " , cer_file_name = '' "
            sql &= " WHERE idp_id = " & id

            errorMsg = conn.executeSQLForTransaction(sql)
            'Response.Write("pk = " & pk)
            If errorMsg <> "" Then
                Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
            End If

            conn.setDBCommit()

            bindDetail()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub
End Class
