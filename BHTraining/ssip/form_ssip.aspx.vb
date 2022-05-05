Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Net.Mail
Imports System.Threading
Imports QueryStringEncryption
Imports System.Net

Partial Class ssip_form_ssip
    Inherits System.Web.UI.Page
    Protected formId As String = ""
    Protected id As String = ""
    Protected mode As String = ""
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected session_id As String
    Protected viewtype As String = ""

    Private new_ir_id As String = ""
    Private cl As Control

    Private relate_dept_id As String = ""
    Protected lang As String = "th"
    Protected global_ssip_no As String = ""
    Protected rewardArray() As Integer
    Protected totalHour As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        formId = Request.QueryString("formId")
        id = Request.QueryString("id")
        mode = Request.QueryString("mode")

        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

      

        viewtype = Request.QueryString("viewtype")

        ' rewardArray(0) = 1000

        If viewtype = "add" Then
            viewtype = ""
            Session("viewtype") = ""

        Else

            If IsNothing(Session("viewtype")) Then
                Session("viewtype") = ""

            End If

            If viewtype <> "" Then
                Session("viewtype") = viewtype
            Else
                viewtype = Session("viewtype").ToString
            End If


        End If
       
        '  Response.Write(viewtype)
        session_id = Session("session_myid").ToString


        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If viewtype = "" Or viewtype = "public" Or viewtype = "activity" Then
            tabSendMail.Visible = False
            tabMailLog.Visible = False
            tabDeptManager.Visible = False
            tabCommittee.Visible = False
            tabDeptManager.Visible = False
            ' tabDivision.Visible = False
            ' tabSupervisor.Visible = False
            '  tabManager.Visible = False

            panelDetail2.Visible = False
            panelHR2.Visible = False

            cmdRecv1.Visible = False
            cmdHRReview1.Visible = False
        End If

        If viewtype = "public" Then
            div_activity.Visible = False
        End If

        If Not Page.IsPostBack Then ' ถ้าเปิดมาครั้งแรก
            bindStatus()
            bindRelateDept()
            bindRelateBenefit()

            txtdate_status.Attributes.Add("readonly", "readonly")

            If mode = "edit" Then
                bindSSIPDetail()
                bindCategoryCombo()
                bindSelectDept()
                bindSelectPerson()
                bindSelectBenefit()
                bindSolutionList()
                bindBenefitList()
                bindFile()
                bindFileAttach() ' SSIP attachment
                bindGridSSIPLog()
                bindGridInformationUpdate()

                bindGridActivity()

                txtbenefit_old.Attributes.Add("style", "text-align:right")
                txtbenefit_old.Attributes.Add("onFocus", "this.select()")
                txtbenefit_new.Attributes.Add("style", "text-align:right")
                txtbenefit_new.Attributes.Add("onFocus", "this.select()")
                txtbenefit_factor.Attributes.Add("style", "text-align:right")
                txtbenefit_factor.Attributes.Add("onFocus", "this.select()")
                txtbenefit_final.Attributes.Add("style", "text-align:right")
                txtbenefit_final.Attributes.Add("onFocus", "this.select()")
                txtamount_reward.Attributes.Add("style", "text-align:right")
                txtamount_reward.Attributes.Add("onFocus", "this.select()")

                txtonthespot.Attributes.Add("style", "text-align:right;width: 180px")
                txtonthespot.Attributes.Add("onFocus", "this.select()")
                txtsrppoint.Attributes.Add("style", "text-align:right")
                txtsrppoint.Attributes.Add("onFocus", "this.select()")
                txtcash.Attributes.Add("style", "text-align:right;width: 180px")
                txtcash.Attributes.Add("onFocus", "this.select()")
                txtsrp_bonus.Attributes.Add("style", "text-align:right")
                txtsrp_bonus.Attributes.Add("onFocus", "this.select()")
                txtsrp_total_point.Attributes.Add("style", "text-align:right")
                txtsrp_total_point.Attributes.Add("onFocus", "this.select()")
                'Response.Write(viewtype)
                ' Manager
                'LinkButton1.Visible = False
                'Label5.Visible = False
                ' imgPanelManager.Visible = False
                ' Committee
                ' LinkButton2.Visible = False
                'Label7.Visible = False
                Image4.Visible = False

                If viewtype = "" Or viewtype = "public" Or viewtype = "activity" Then ' Drafted
                    panelAddComment.Visible = False
                    '   Response.Write(viewtype)
                    If txtstatus.SelectedValue <> "1" And txtstatus.SelectedValue <> "7" Then
                        'panelDetail.Enabled = False
                        readonlyControl(panelDetail)
                        cmdDraft1.Visible = False
                        cmdSubmit.Visible = False
                        cmdDraft2.Visible = False
                        cmdSubmit2.Visible = False
                        bindHRTab()
                        bindHRIMPLDept()
                    End If

                    If txtstatus.SelectedValue = "7" Then ' Return to staff
                        bindHRTab()
                        bindHRIMPLDept()
                    End If
                Else
                    bindGridAlertLog()

                    bindHRTab()
                    bindHRRelateDept()
                    bindHRIMPLDept()
                    bindCommentList()
                    bindCommitteeCommentList()

                    calReward() ' HR Tab
                    calScale()
                    '  calAmountText() ' HR Tab

                    panelDetail.Visible = False
                    panelDetail2.Visible = True

                    panelHR1.Visible = False
                    panelHR2.Visible = True

                    cmdDraft1.Visible = False
                    cmdSubmit.Visible = False
                    cmdDraft2.Visible = False
                    cmdSubmit2.Visible = False
                    cmdHRReview1.Visible = False
                    cmdRecv1.Visible = False
                End If

                If viewtype = "hr" Then

                    '  cmdUpdateStatus.Visible = True
                    bindStatus2()
                    txtstatus.Enabled = True
                    cmdHRReview1.Visible = True
                    cmdRecv1.Visible = False
                    panelAddComment.Visible = False
                    linkDept.Visible = False
                    linkCommittee.Visible = False
                Else
                    ' panelHR2.Enabled = False
                    readonlyControl(panelHR2)

                End If

                If viewtype = "sup" Or viewtype = "mgr" Then
                    cmdHRReview1.Visible = False
                    ' bindManagerTab()
                    bindDeptAll()

                    '  txtjobtitle.Value = Session("job_title").ToString
                    ' lblJobType.Text = Session("user_position").ToString
                    'txtname.Value = Session("user_fullname").ToString
                    'txtdeptname.Value = Session("dept_name").ToString
                    'txtdatetime.Value = Date.Now

                    panelAddCommiteeComment.Visible = False
                    panelAddComment.Visible = True
                    tabSendMail.Visible = False
                    tabMailLog.Visible = False

                    linkDept.Visible = False
                    linkCommittee.Visible = False
                    '  LinkButton1.Visible = True
                    '  Label5.Visible = True
                    ' imgPanelManager.Visible = True
                    txtadd_subject.Text = lblSSIPTopic.Text

                End If

                If viewtype = "com" Then
                    bindScore()

                    cmdHRReview1.Visible = False
                    panelAddCommiteeComment.Visible = True
                    panelAddComment.Visible = True
                    tabSendMail.Visible = False
                    tabMailLog.Visible = False

                    '  LinkButton2.Visible = True
                    ' Label7.Visible = True
                    Image4.Visible = True
                    linkDept.Visible = False
                    linkCommittee.Visible = False
                    'LinkButton1.Visible = True
                    'Label5.Visible = True
                    ' imgPanelManager.Visible = True
                Else
                    ' GridCommittee.Columns(0).ItemStyle.Width = 10
                End If

                If viewtype = "public" Then
                    cmdUpload.Visible = False
                    cmdDeleteFile.Visible = False
                    FileUpload0.Visible = False
                    readonlyControl(panelDetail)
                    panelDetail.Visible = False
                    panelDetail2.Visible = True
                    tabHR.Visible = False
                    tab_update.Visible = False
                    mytabber2.Visible = False
                End If

               

            ElseIf mode = "add" Then
                tab_update.Visible = False
                div_activity.Visible = False
                txtperson_select.Items.Add(New ListItem(Session("user_fullname").ToString, Session("emp_code")))
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

    Sub calScale()
        Dim sql As String
        Dim ds As New DataSet
        Try
            For i As Integer = 1 To 4
                sql = "SELECT ISNULL(COUNT(*),0) AS num FROM ssip_committee_tab a INNER JOIN ssip_manager_comment b ON a.comment_id = b.comment_id "
                sql &= " WHERE  a.intang_award_scale_id = " & i & " AND a.ssip_id = " & id
                '  Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    CType(panel_reward.FindControl("lblscale" & i), Label).Text &= " (" & ds.Tables("t1").Rows(0)(0).ToString & ")"
                End If

            Next i
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub calReward()
        Dim avg_score As Integer = getAverateScoreFromCommittee()
        ' Response.Write(avg_score)

        '  Dim targetId As Integer
        setRewardObjectColor("black")

        If avg_score >= 30 And avg_score <= 44 Then
            lblbenefit1.ForeColor = Drawing.Color.WhiteSmoke
            lblbenefit1.BackColor = Drawing.Color.SaddleBrown
            lblbenefit1.Font.Bold = True
        ElseIf avg_score >= 45 And avg_score <= 64 Then
            lblbenefit2.ForeColor = Drawing.Color.WhiteSmoke
            lblbenefit2.BackColor = Drawing.Color.SaddleBrown
            lblbenefit2.Font.Bold = True
        ElseIf avg_score >= 65 And avg_score <= 84 Then
            lblbenefit3.ForeColor = Drawing.Color.WhiteSmoke
            lblbenefit3.BackColor = Drawing.Color.SaddleBrown
            lblbenefit3.Font.Bold = True
        ElseIf avg_score >= 85 And avg_score <= 100 Then
            lblbenefit4.ForeColor = Drawing.Color.WhiteSmoke
            lblbenefit4.BackColor = Drawing.Color.SaddleBrown
            lblbenefit4.Font.Bold = True
        Else

        End If

        If txtreward_sum.SelectedValue = "1" And txtaward_scale.SelectedValue = "1" Then
            CType(panel_reward.FindControl("lblreward1"), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward1"), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward1"), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "2" And txtaward_scale.SelectedValue = "1" Then
            CType(panel_reward.FindControl("lblreward" & 2), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 2), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 2), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "3" And txtaward_scale.SelectedValue = "1" Then
            CType(panel_reward.FindControl("lblreward" & 3), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 3), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 3), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "4" And txtaward_scale.SelectedValue = "1" Then
            CType(panel_reward.FindControl("lblreward" & 4), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 4), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 4), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "1" And txtaward_scale.SelectedValue = "2" Then
            CType(panel_reward.FindControl("lblreward" & 5), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 5), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 5), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "2" And txtaward_scale.SelectedValue = "2" Then
            CType(panel_reward.FindControl("lblreward" & 6), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 6), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 6), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "3" And txtaward_scale.SelectedValue = "2" Then
            CType(panel_reward.FindControl("lblreward" & 7), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 7), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 7), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "4" And txtaward_scale.SelectedValue = "2" Then
            CType(panel_reward.FindControl("lblreward" & 8), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 8), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 8), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "1" And txtaward_scale.SelectedValue = "3" Then
            CType(panel_reward.FindControl("lblreward" & 9), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 9), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 9), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "2" And txtaward_scale.SelectedValue = "3" Then
            CType(panel_reward.FindControl("lblreward" & 10), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 10), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 10), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "3" And txtaward_scale.SelectedValue = "3" Then
            CType(panel_reward.FindControl("lblreward" & 11), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 11), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 11), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "4" And txtaward_scale.SelectedValue = "3" Then
            CType(panel_reward.FindControl("lblreward" & 12), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 12), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 12), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "1" And txtaward_scale.SelectedValue = "4" Then
            CType(panel_reward.FindControl("lblreward" & 13), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 13), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 13), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "2" And txtaward_scale.SelectedValue = "4" Then
            CType(panel_reward.FindControl("lblreward" & 14), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 14), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 14), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "3" And txtaward_scale.SelectedValue = "4" Then
            CType(panel_reward.FindControl("lblreward" & 15), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 15), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 15), Label).Font.Bold = True
        ElseIf txtreward_sum.SelectedValue = "4" And txtaward_scale.SelectedValue = "4" Then
            CType(panel_reward.FindControl("lblreward" & 16), Label).ForeColor = Drawing.Color.Red
            CType(panel_reward.FindControl("lblreward" & 16), Label).BackColor = Drawing.Color.Yellow
            CType(panel_reward.FindControl("lblreward" & 16), Label).Font.Bold = True
        End If

    End Sub

    Function getAverateScoreFromCommittee() As Integer
        Dim sql As String
        Dim ds As New DataSet
        Dim num As Integer = 0
        Dim score As Integer = 0
        Try
            sql = "SELECT ISNULL(score1 , 0) +  ISNULL(score2 , 0) +  ISNULL(score3 , 0) +  ISNULL(score4 , 0)  +  ISNULL(score5 , 0) AS sum_score   "
            sql &= " FROM ssip_manager_comment a INNER JOIN ssip_committee_tab b ON a.comment_id = b.comment_id WHERE a.ssip_id = " & id
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            num = ds.Tables("t1").Rows.Count

            If num <= 0 Then
                Return 0
            End If

            For i As Integer = 0 To num - 1
                score += Convert.ToInt32(ds.Tables("t1").Rows(i)("sum_score").ToString)
            Next

            Return CInt(score / num)
        Catch ex As Exception
            Response.Write(ex.Message)
            Return 0
        Finally
            ds.Dispose()
        End Try
    End Function

    Sub calAmountText()
        setAmountObjectColor("black")

        Try
            If txtbenefit_final.Text <> "" Then
                If Convert.ToDouble(txtbenefit_final.Text) > 0 And Convert.ToDouble(txtbenefit_final.Text) <= 200000 Then
                    lblamount1.ForeColor = Drawing.Color.Red
                    lblamount1.BackColor = Drawing.Color.Yellow
                    lblamount1.Font.Bold = True

                    If txtbenefit_final.Text <> "" Then
                        txtamount_reward.Text = Convert.ToDouble(txtbenefit_final.Text) * 0.1
                    End If
                ElseIf Convert.ToDouble(txtbenefit_final.Text) > 200000 And Convert.ToDouble(txtbenefit_final.Text) <= 1000000 Then
                    lblamount2.ForeColor = Drawing.Color.Red
                    lblamount2.BackColor = Drawing.Color.Yellow
                    lblamount2.Font.Bold = True

                    If txtbenefit_final.Text <> "" Then
                        txtamount_reward.Text = 20000 + (Convert.ToDouble(txtbenefit_final.Text) - 200000) * 0.03
                    End If

                ElseIf Convert.ToDouble(txtbenefit_final.Text) > 1000000 Then
                    lblamount3.ForeColor = Drawing.Color.Red
                    lblamount3.BackColor = Drawing.Color.Yellow
                    lblamount3.Font.Bold = True

                    If txtbenefit_final.Text <> "" Then
                        txtamount_reward.Text = 44000 + (Convert.ToDouble(txtbenefit_final.Text) - 1000000) * 0.01
                    End If
                Else
                    setAmountObjectColor("black")
                    txtamount_reward.Text = 0
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " calAmountText")
        End Try
        
    End Sub

    Sub calBenefit()
        If txtbenefit_old.Text <> "" And txtbenefit_new.Text <> "" And txtbenefit_factor.Text <> "" Then
            txtbenefit_final.Text = CDbl(txtbenefit_old.Text) - (CDbl(txtbenefit_new.Text) * CDbl(txtbenefit_factor.Text))
        End If
    End Sub

    Sub setAmountObjectColor(ByVal color As String)
        lblamount1.ForeColor = Drawing.Color.Black
        lblamount1.BackColor = Drawing.Color.Transparent
        lblamount1.Font.Bold = False

        lblamount2.ForeColor = Drawing.Color.Black
        lblamount2.BackColor = Drawing.Color.Transparent
        lblamount2.Font.Bold = False

        lblamount3.ForeColor = Drawing.Color.Black
        lblamount3.BackColor = Drawing.Color.Transparent
        lblamount3.Font.Bold = False
    End Sub

    Sub setRewardObjectColor(ByVal color As String)
        For i As Integer = 1 To 16
            If color = "black" Then
                CType(panel_reward.FindControl("lblreward" & i), Label).ForeColor = Drawing.Color.Black
                CType(panel_reward.FindControl("lblreward" & i), Label).Font.Bold = False
                CType(panel_reward.FindControl("lblreward" & i), Label).BackColor = Drawing.Color.Transparent

            End If

        Next i

        For i As Integer = 1 To 4
            If color = "black" Then
                CType(panel_reward.FindControl("lblbenefit" & i), Label).ForeColor = Drawing.Color.Black
                CType(panel_reward.FindControl("lblbenefit" & i), Label).Font.Bold = False
                CType(panel_reward.FindControl("lblbenefit" & i), Label).BackColor = Drawing.Color.Transparent
            End If

        Next i
    End Sub

    Function getNewSSIPNo() As String
        Dim sql As String
        Dim dsIR As New DataSet

        Dim new_ir_no As String = 0
        Try
            Dim yyyy As String = CStr(Date.Now.Year)
            sql = "SELECT * FROM ssip_trans_list WHERE ssip_no LIKE '" & yyyy & "%' ORDER BY ssip_no DESC"
            dsIR = conn.getDataSetForTransaction(sql, "t1")
            If dsIR.Tables(0).Rows.Count > 0 Then
                new_ir_no = CLng(dsIR.Tables(0).Rows(0)("ssip_no").ToString) + 1
            Else
                new_ir_no = yyyy & "00001"
            End If

            'Dim d As New Date(new_ir_no
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            dsIR.Dispose()

        End Try

        Return new_ir_no
    End Function

    Sub bindStatus()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM ssip_status_list"
            'sql &= " ORDER BY dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            txtstatus.DataSource = ds
            txtstatus.DataBind()

            txtstatus.Items.Insert(0, New ListItem("----", "1"))

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindStatus2()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM ssip_status_list WHERE status_id >= 1"
            'sql &= " ORDER BY dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            txtstatus.DataSource = ds
            txtstatus.DataBind()

            'txtstatus.Items.Insert(0, New ListItem("----", ""))

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindRelateBenefit()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from ssip_m_benefit "
            If mode = "edit" Then
                sql &= " WHERE master_benefit_id NOT IN (SELECT master_benefit_id FROM ssip_relate_benefit WHERE ssip_id = " & id & ")"
            End If

            sql &= " ORDER BY master_benefit_name "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtBenefit_all.DataSource = ds.Tables(0)
            txtBenefit_all.DataBind()

          
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindRelateDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from user_dept ORDER BY dept_name_en"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdept_all.DataSource = ds.Tables(0)
            txtdept_all.DataBind()

            If viewtype = "hr" Then
                txthr_alldept.DataSource = ds.Tables(0)
                txthr_alldept.DataBind()

                txthr_alldept2.DataSource = ds.Tables(0)
                txthr_alldept2.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from ssip_relate_dept WHERE ssip_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdept_select.DataSource = ds
            txtdept_select.DataBind()

            txtSSIPRelateDept.DataSource = ds
            txtSSIPRelateDept.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindRelatePerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from user_profile WHERE 1 = 1 "
            If txtdept_all.SelectedValue <> "" Then
                sql &= " AND costcenter_id = " & txtdept_all.SelectedValue
            End If
            sql &= " ORDER BY user_fullname"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtperson_all.DataSource = ds
            txtperson_all.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindHRRelateDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT costcenter_id , costcenter_name AS dept_name_en from ssip_hr_relate_dept WHERE is_impl_dept = 0 AND ssip_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txthr_selectdept.DataSource = ds
            txthr_selectdept.DataBind()
            'Response.Write(sql)

        Catch ex As Exception
            Response.Write(ex.Message & "bindHRRelateDept :: " & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindHRIMPLDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT costcenter_id , costcenter_name AS dept_name_en from ssip_hr_relate_dept WHERE is_impl_dept = 1 AND ssip_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txthr_impldept.DataSource = ds
            txthr_impldept.DataBind()

            txtdept_impl.DataSource = ds
            txtdept_impl.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & " bindHRIMPLDept : " & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from ssip_relate_person WHERE ssip_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtperson_select.DataSource = ds
            txtperson_select.DataBind()

            txtSSIPRelatePerson.DataSource = ds
            txtSSIPRelatePerson.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectBenefit()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from ssip_relate_benefit WHERE ssip_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtBenefit_select.DataSource = ds
            txtBenefit_select.DataBind()

            txtSSIPRelateBenefit.DataSource = ds
            txtSSIPRelateBenefit.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSolutionList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from ssip_solution_list WHERE 1= 1"
            If mode = "add" Then
                sql &= " AND session_id = '" & Session.SessionID & "' "
            Else
                sql &= " AND ssip_id = " & id
            End If

            sql &= " ORDER BY order_sort ASC"
            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(sql)
            GridSuggest.DataSource = ds
            GridSuggest.DataBind()

            GridSuggest2.DataSource = ds
            GridSuggest2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
            Return
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindBenefitList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from ssip_benefit_list WHERE 1= 1"
            If mode = "add" Then
                sql &= " AND session_id = '" & Session.SessionID & "' "
            Else
                sql &= " AND ssip_id = " & id
            End If

            sql &= " ORDER BY order_sort ASC"
            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(sql)
            GridBenefit.DataSource = ds
            GridBenefit.DataBind()

            GridBenefit2.DataSource = ds
            GridBenefit2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
            Return
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindFile()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from ssip_file_list WHERE 1= 1"
            If mode = "add" Then
                sql &= " AND session_id = '" & Session.SessionID & "' "
            Else
                sql &= " AND ssip_id = " & id
            End If

            sql &= " ORDER BY order_sort ASC"
            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(sql)
            GridFile.DataSource = ds
            GridFile.DataBind()

            GridFile2.DataSource = ds
            GridFile2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
            Return
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSSIPDetail()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from ssip_trans_list a INNER JOIN ssip_detail_tab b ON a.ssip_id = b.ssip_id  WHERE a.ssip_id = " & id
          
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtstatus.SelectedValue = ds.Tables("t1").Rows(0)("status_id").ToString
            txttopic.Text = ds.Tables("t1").Rows(0)("topic").ToString
            lblSSIPTopic.Text = ds.Tables("t1").Rows(0)("topic").ToString
            lblSSIPNo.Text = ds.Tables("t1").Rows(0)("ssip_no").ToString
            lblSSIPStatusName.Text = ds.Tables("t1").Rows(0)("ssip_status_name").ToString & " " & ConvertTSToDateString(ds.Tables("t1").Rows(0)("ssip_op_date_ts").ToString)
            lblSSIPFrom.Text = ds.Tables("t1").Rows(0)("ssip_source_type_name").ToString & " : " & ds.Tables("t1").Rows(0)("ssip_source").ToString


            txtsource.Value = ds.Tables("t1").Rows(0)("ssip_source").ToString
            txtsourcetype.SelectedValue = ds.Tables("t1").Rows(0)("ssip_source_type_id").ToString
           
            lblSSIPSuggest.Text = ""
            If ds.Tables("t1").Rows(0)("chk_suggest1").ToString = "1" Then
                lblSSIPSuggest.Text &= "เพิ่มความพึงพอใจให้แก่ลูกค้า (Improve customer satisfaction)" & "<br/>"
            End If

            If ds.Tables("t1").Rows(0)("chk_suggest2").ToString = "1" Then
                lblSSIPSuggest.Text &= "ประหยัดค่าใช้จ่ายได้เป็นอย่างมาก (Reduce costs)" & "<br/>"
            End If

            If ds.Tables("t1").Rows(0)("chk_suggest3").ToString = "1" Then
                lblSSIPSuggest.Text &= "เพิ่มรายได้หรือกำไรให้แก่องค์กร (Increase revenuse / Benefits)" & "<br/>"
            End If

            If ds.Tables("t1").Rows(0)("chk_suggest4").ToString = "1" Then
                lblSSIPSuggest.Text &= "เพิ่มประสิทธิภาพในการทำงาน (Increase productivity)" & "<br/>"
            End If

            If ds.Tables("t1").Rows(0)("chk_suggest5").ToString = "1" Then
                lblSSIPSuggest.Text &= "ปรับปรุงคุณภาพของสถานที่ทำงาน (Improve quality of workplace)" & "<br/>"
            End If

            txtdate_status.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("ssip_op_date_ts").ToString)

            For i As Integer = 1 To 5
                If ds.Tables(0).Rows(0)("ssip_status_id").ToString() = i.ToString Then
                    CType(panelDetail.FindControl("txts" & i), RadioButton).Checked = True
                End If
            Next i

            For i As Integer = 1 To 5

                If ds.Tables(0).Rows(0)("chk_suggest" & i).ToString() = "1" Then
                    ' Response.Write(ds.Tables(0).Rows(0)("chk_suggest" & i).ToString())
                    CType(panelDetail.FindControl("chk_suggest" & i), HtmlInputCheckBox).Checked = True
                Else
                    CType(panelDetail.FindControl("chk_suggest" & i), HtmlInputCheckBox).Checked = False
                End If
            Next i

            If ds.Tables("t1").Rows(0)("is_confirm").ToString = "1" Then
                chk_confirm.Checked = True
            Else
                chk_confirm.Checked = False
            End If

            lblSSIPEmpName.Text = ds.Tables("t1").Rows(0)("report_by").ToString
            lblSSIPempCode.Text = ds.Tables("t1").Rows(0)("report_emp_code").ToString
            '  lblSSIPjobTitle.Text = ds.Tables("t1").Rows(0)("ssip_op_date_ts").ToString
            lblSSIPDeptName.Text = ds.Tables("t1").Rows(0)("report_dept_name").ToString

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindHRTab()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_hr_tab a INNER JOIN ssip_trans_list b ON a.ssip_id = b.ssip_id WHERE a.ssip_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables(0).Rows(0)("is_public").ToString = "1" Then
                txtpublic.Checked = True
            End If

            Try
                txtssip_category.SelectedValue = ds.Tables(0).Rows(0)("cat_id").ToString
            Catch ex As Exception

            End Try

            lblResult.Text = ds.Tables(0).Rows(0)("result_title").ToString
            lblDetail.Text = ds.Tables(0).Rows(0)("result_detail").ToString
            lblCommittee.Text = ds.Tables(0).Rows(0)("consider_detail").ToString
            txtawarddate.Value = ConvertTSToDateString(ds.Tables(0).Rows(0)("date_award_ts").ToString)
            lblSumReward.Text = ""
            If ds.Tables(0).Rows(0)("coupon_num").ToString <> "0" Then
                lblSumReward.Text &= ds.Tables(0).Rows(0)("coupon_num").ToString & " On-The-Spot <br/>"
            End If
            If ds.Tables(0).Rows(0)("point_num").ToString <> "0" Then
                lblSumReward.Text &= ds.Tables(0).Rows(0)("point_num").ToString & " SRP point <br/>"
            End If
            If ds.Tables(0).Rows(0)("cash_num").ToString <> "0" Then
                lblSumReward.Text &= ds.Tables(0).Rows(0)("cash_num").ToString & " Baht <br/>"
            End If

            txtSSIPResult.SelectedValue = ds.Tables(0).Rows(0)("result_id").ToString
            txtdetail.Value = ds.Tables(0).Rows(0)("result_detail").ToString
            txtcommittee.Value = ds.Tables(0).Rows(0)("consider_detail").ToString
            txtedittopic.Value = ds.Tables(0).Rows(0)("innovation_subject").ToString
            txtonthespot.Value = ds.Tables(0).Rows(0)("coupon_num").ToString
            txtsrppoint.Text = ds.Tables(0).Rows(0)("point_num").ToString
            txtsrp_bonus.Text = ds.Tables(0).Rows(0)("point_bonus").ToString
            '  Response.Write(ds.Tables(0).Rows(0)("point_num").ToString)
            txtsrp_total_point.Text = ds.Tables(0).Rows(0)("point_total").ToString
            txtcash.Value = ds.Tables(0).Rows(0)("cash_num").ToString
            txtaward_date.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("date_award_ts").ToString)
            txtreceive_date.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("date_receive_ts").ToString)

            'Try
            '    For i As Integer = 1 To 5
            '        If CInt(ds.Tables(0).Rows(0)("reward_type_id").ToString) = i Then
            '            CType(panelHR2.FindControl("txtb" & i), RadioButton).Checked = True
            '        End If
            '    Next i
            'Catch ex As Exception

            'End Try

            'Try
            '    For i As Integer = 1 To 16
            '        If CInt(ds.Tables(0).Rows(0)("scale_type_id").ToString) = i Then
            '            CType(panelHR2.FindControl("txtsc" & i), RadioButton).Checked = True
            '        End If
            '    Next i
            'Catch ex As Exception

            'End Try


            'txtscale1.Text = ds.Tables(0).Rows(0)("benefit_num1").ToString
            'txtscale2.Text = ds.Tables(0).Rows(0)("benefit_num2").ToString
            'txtscale3.Text = ds.Tables(0).Rows(0)("benefit_num3").ToString

            txtreward_sum.SelectedValue = ds.Tables(0).Rows(0)("personal_reward_sum_id").ToString
            txtaward_scale.SelectedValue = ds.Tables(0).Rows(0)("intang_award_scale_id").ToString

            txtbenefit_old.Text = ds.Tables(0).Rows(0)("benefit_old").ToString
            txtbenefit_new.Text = ds.Tables(0).Rows(0)("benefit_new").ToString
            txtbenefit_factor.Text = ds.Tables(0).Rows(0)("benefit_factor").ToString
            txtbenefit_final.Text = ds.Tables(0).Rows(0)("benefit_final").ToString
            '  Response.Write(ds.Tables(0).Rows(0)("award_amount").ToString)
            If ds.Tables(0).Rows(0)("award_amount").ToString <> "" Then
                txtamount_reward.Text = CDbl(ds.Tables(0).Rows(0)("award_amount").ToString)
            End If

            txtkeyword.Value = ds.Tables(0).Rows(0)("subject_keyword").ToString
            txtdescription.Value = ds.Tables(0).Rows(0)("subject_description").ToString
            txtreward_remark.Text = ds.Tables(0).Rows(0)("reward_remark").ToString
           
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub isHasRow(ByVal table As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""

        sql = "SELECT * FROM " & table & " WHERE ssip_id = " & id
        ' Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage)
        End If

        If ds.Tables("t1").Rows.Count <= 0 Then
            sql = "INSERT INTO " & table & " (ssip_id) VALUES( "
            sql &= "" & id & ""
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If
        End If

        ds.Dispose()

    End Sub

    Sub updateSSIPNo()
        Dim sql As String
        Dim errorMsg As String
        Dim new_ssip_no As String = ""
        new_ssip_no = getNewSSIPNo()
        global_ssip_no = new_ssip_no
        sql = "UPDATE ssip_trans_list SET "

        '  sql &= " , review_status_id = " & review_status_id0
        sql &= "  ssip_no = '" & new_ssip_no & "'"

        sql &= " WHERE ssip_id = " & id

        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If
    End Sub

    Sub loopMail(ByVal training_no As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim msgBody As String = ""
        Dim key As String = ""

        Try
            Dim host As String = ConfigurationManager.AppSettings("frontHost").ToString
            key = UserActivation.GetActivationLink("ssip/form_ssip.aspx?mode=edit&id=" & id)
            msgBody = "Please find Staff Suggestion and Innovation. Please review on this idea, for fast access online review, please kindly open a link below <br/>" & vbCrLf
            msgBody &= "<a href='http://" & host & "/login.aspx?viewtype=dept&key=" & key & "'>Staff Suggestion and Innovation Online</a>"
            msgBody &= "<br/><br/> Best regard, <br/> Staff Suggestion and Innovation Team"
            msgBody &= "<br/>สอบถามข้อมูลเพิ่มเติมได้ที่ : คุณจิตติมา สุนทรกลัมพ์ ผู้ประสานงานโครงการข้อเสนอแนะและนวัตกรรม โทร. 72494"
            msgBody &= "<br/>For more information, please contact Khun Chittima Suntornklam, The coordinator of the Staff Suggestion & Innovation Committee on the extension number 72494"
            '  Response.Write(msgBody)
            sql = "SELECT * FROM user_profile a INNER JOIN user_access_costcenter_ssip b ON a.emp_code = b.emp_code WHERE b.costcenter_id  = " & Session("dept_id").ToString
            sql &= " AND ISNULL(a.custom_user_email,'') <> ''  AND a.emp_code in (select emp_code from user_role where role_id = 202) "
            ds = conn.getDataSetForTransaction(sql, "t1")
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                '  sendMail(ds.Tables("t1").Rows(i)("custom_user_email").ToString, "#" & global_ssip_no & " Please review SSIP", msgBody)
            Next i
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
            oMsg.From = New MailAddress("innovation@bumrungrad.com")
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

    Sub onSave(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            If e.CommandArgument.ToString <> "99" Then
                updateTransList(e.CommandArgument.ToString)
                If e.CommandArgument.ToString <> "1" Then
                    updateOnlyLog(e.CommandArgument)
                End If

                If e.CommandArgument.ToString = "2" Then ' Submit
                    updateSSIPNo()
                End If

                If txtadd_problem.Text <> "" And txtadd_suggest.Text <> "" Then
                    addTopicWithNoCommit()
                End If

                If txtadd_benefit.Text <> "" Then
                    addBenefitWithNoCommit()
                End If


            Else ' HR Save review = 99
                'updateTransList(txtstatus.SelectedValue) ' update status
                updateOnlyLog("0", "HR Update")
            End If

           

            isHasRow("ssip_detail_tab")
            updateSSIPetail("")
            isHasRow("ssip_hr_tab")

            updateRelateDept()
            updateRelatePerson()
            updateRelateBenefit()
            'Response.Write(5)
            updateSuggestList()
            updateBenefitList()
            updateFileList()

            If viewtype = "hr" Then
                updateSSIPStatus()
                updateHRTab()
                updateHRRelateDept()
                updateHRImplDept()
            End If

          

            conn.setDBCommit()

            If e.CommandArgument.ToString = "2" Then ' Submit
                loopMail(global_ssip_no)
            End If

           

            If e.CommandArgument.ToString = "2" Then ' Submit
                Response.Redirect("home.aspx")
            ElseIf e.CommandArgument.ToString = "99" And txtstatus.SelectedValue = "7" Then ' Return to staff
                Response.Redirect("home.aspx?viewtype=hr")
            Else
                Response.Redirect("form_ssip.aspx?mode=edit&id=" & id)
            End If


        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("onsave : " & ex.Message)
        End Try
    End Sub

    Sub updateRelateDept()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        If mode = "edit" Then
            sql = "DELETE FROM ssip_relate_dept WHERE ssip_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "DELETE FROM ssip_hr_relate_dept WHERE ssip_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


        For i As Integer = 0 To txtdept_select.Items.Count - 1
            ' User Relate Dept ssip_relate_dept
            pk = getPK("ssip_relate_dept_id", "ssip_relate_dept", conn)
            sql = "INSERT INTO ssip_relate_dept (ssip_relate_dept_id , ssip_id , costcenter_id , costcenter_name ) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & id & "' ,"
            sql &= "'" & txtdept_select.Items(i).Value & "' ,"
            sql &= "'" & txtdept_select.Items(i).Text & "' "

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            ' HR Relate Dept  ssip_hr_relate_dept
            pk = getPK("hr_relate_dept_id", "ssip_hr_relate_dept", conn)
            sql = "INSERT INTO ssip_hr_relate_dept (hr_relate_dept_id , ssip_id , costcenter_id , costcenter_name , is_impl_dept) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & id & "' ,"
            sql &= "'" & txtdept_select.Items(i).Value & "' ,"
            sql &= "'" & txtdept_select.Items(i).Text & "' ,"
            sql &= "'" & 0 & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

        Next i


    End Sub

    Sub updateRelatePerson()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        If mode = "edit" Then
            sql = "DELETE FROM ssip_relate_person WHERE ssip_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


        For i As Integer = 0 To txtperson_select.Items.Count - 1
            pk = getPK("ssip_relate_person_id", "ssip_relate_person", conn)
            sql = "INSERT INTO ssip_relate_person (ssip_relate_person_id , ssip_id , emp_code , user_fullname) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & id & "' ,"
            sql &= "'" & txtperson_select.Items(i).Value & "' ,"
            sql &= "'" & txtperson_select.Items(i).Text & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i


    End Sub

    Sub updateRelateBenefit()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        If mode = "edit" Then
            sql = "DELETE FROM ssip_relate_benefit WHERE ssip_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


        For i As Integer = 0 To txtBenefit_select.Items.Count - 1
            pk = getPK("relate_benefit_id", "ssip_relate_benefit", conn)
            sql = "INSERT INTO ssip_relate_benefit (relate_benefit_id , ssip_id , master_benefit_id , master_benefit_name) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & id & "' ,"
            sql &= "'" & txtBenefit_select.Items(i).Value & "' ,"
            sql &= "'" & txtBenefit_select.Items(i).Text & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i


    End Sub

    Sub updateHRRelateDept() ' HR ใส่แผนกที่เกี่ยวข้อง
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim pk2 As String = ""

        If mode = "edit" Then
            sql = "DELETE FROM ssip_hr_relate_dept WHERE is_impl_dept = 0 AND ssip_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


        For i As Integer = 0 To txthr_selectdept.Items.Count - 1
            pk = getPK("hr_relate_dept_id", "ssip_hr_relate_dept", conn)
            sql = "INSERT INTO ssip_hr_relate_dept (hr_relate_dept_id , ssip_id , costcenter_id , costcenter_name , is_impl_dept) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & id & "' ,"
            sql &= "'" & txthr_selectdept.Items(i).Value & "' ,"
            sql &= "'" & txthr_selectdept.Items(i).Text & "' ,"
            sql &= "'" & 0 & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

         
        Next i


    End Sub

    Sub updateHRImplDept()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        If mode = "edit" Then
            sql = "DELETE FROM ssip_hr_relate_dept WHERE is_impl_dept = 1 AND ssip_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


        For i As Integer = 0 To txthr_impldept.Items.Count - 1
            pk = getPK("hr_relate_dept_id", "ssip_hr_relate_dept", conn)
            sql = "INSERT INTO ssip_hr_relate_dept (hr_relate_dept_id , ssip_id , costcenter_id , costcenter_name , is_impl_dept) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & id & "' ,"
            sql &= "'" & txthr_impldept.Items(i).Value & "' ,"
            sql &= "'" & txthr_impldept.Items(i).Text & "' ,"
            sql &= "'" & 1 & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i


    End Sub

    Sub updateSuggestList()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        If mode = "add" Then
            sql = "UPDATE ssip_solution_list SET session_id = null , ssip_id = " & id & " WHERE session_id = '" & Session.SessionID & "'"
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


    End Sub

    Sub updateBenefitList()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        If mode = "add" Then
            sql = "UPDATE ssip_benefit_list SET session_id = null , ssip_id = " & id & " WHERE session_id = '" & Session.SessionID & "'"
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


    End Sub

    Sub updateFileList()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        If mode = "add" Then
            sql = "UPDATE ssip_file_list SET session_id = null , ssip_id = " & id & " WHERE session_id = '" & Session.SessionID & "'"
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


    End Sub

    Sub updateTransList(ByVal status_id As String, Optional ByVal review_status_id As Integer = 0)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim ds2 As New DataSet

        If mode = "add" Then
            Try
                sql = "SELECT ISNULL(MAX(ssip_id),0) + 1 AS pk FROM ssip_trans_list"
                ds = conn.getDataSetForTransaction(sql, "t1")
                pk = ds.Tables("t1").Rows(0)(0).ToString
                new_ir_id = pk
            Catch ex As Exception
                Response.Write(ex.Message & sql)
                Response.Write(sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try


            sql = "INSERT INTO ssip_trans_list (ssip_id , submit_date , submit_date_ts , status_id ,  report_by , report_emp_code , report_dept_id , report_dept_name ,  report_costcenter_id)"
            sql &= " VALUES("
            sql &= "" & pk & " ,"


            sql &= " GETDATE() ,"
            sql &= "" & Date.Now.Ticks & " ,"
            sql &= "" & status_id & " ,"
            sql &= "'" & Session("user_fullname").ToString & "' ,"
            sql &= "'" & Session("emp_code").ToString & "' ,"
            sql &= "'" & Session("dept_id").ToString & "' ,"
            sql &= "'" & Trim(Session("dept_name").ToString) & "' ,"
            sql &= "'" & Session("costcenter_id").ToString & "' "
            sql &= ")"
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            id = pk
        Else ' Edit mode

            sql = "UPDATE ssip_trans_list SET status_id = " & status_id
            '  sql &= " , review_status_id = " & review_status_id0
            'sql &= " , ssip_no = '" & new_ssip_no & "'"

            sql &= " WHERE ssip_id = " & id

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If
    End Sub

    Sub updateOnlyLog(ByVal status_id As String, Optional ByVal log_remark As String = "")
        Dim sql As String
        Dim errorMsg As String

        sql = "INSERT INTO ssip_status_log (status_id , status_name , ir_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
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

    Sub updateSSIPetail(ByVal status_id As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim new_ir_no As String = ""

        If status_id = "2" Then
            ' new_ir_no = getNewCFBNo()

        End If

        sql = "UPDATE ssip_detail_tab SET topic = '" & addslashes(txttopic.Text) & "'"

        sql &= " , ssip_source = '" & addslashes(txtsource.Value) & "'"

        sql &= " , ssip_source_type_id = '" & txtsourcetype.SelectedValue & "'"
        sql &= " , ssip_source_type_name = '" & txtsourcetype.SelectedItem.Text & "'"

        'sql &= " , hn = '" & txthn.Value & "'"
        sql &= " , ssip_op_date = '" & convertToSQLDatetime(txtdate_status.Text) & "'"
        sql &= " , ssip_op_date_ts = " & ConvertDateStringToTimeStamp(txtdate_status.Text) & ""

        For i As Integer = 1 To 4
            If CType(panelDetail.FindControl("txts" & i), RadioButton).Checked = True Then
                sql &= ", ssip_status_id = '" & i & "'"
                If i = 1 Then
                    sql &= " , ssip_status_name = 'ยังไม่ปฏิบัติ (No Experimental)' "
                ElseIf i = 2 Then
                    sql &= " , ssip_status_name = 'อยู่ระหว่างปฏิบัติการ (During experimental)' "
                ElseIf i = 3 Then
                    sql &= " , ssip_status_name = 'ปฏิบัติแล้ว (Already Operation) เมื่อ (Date)' "
                ElseIf i = 4 Then
                    sql &= " , ssip_status_name = 'ไม่ทราบ (Unknown)' "

                End If
            Else
                ' sql &= ", chk_suggest" & i & " = '" & 0 & "'"
            End If
        Next i

        For i As Integer = 1 To 5
            If CType(panelDetail.FindControl("chk_suggest" & i), HtmlInputCheckBox).Checked = True Then
                sql &= ", chk_suggest" & i & " = '" & 1 & "'"
            Else
                sql &= ", chk_suggest" & i & " = '" & 0 & "'"
            End If
        Next i

        If chk_confirm.Checked = True Then
            sql &= " , is_confirm = " & 1 & ""
        Else
            sql &= " , is_confirm = " & 0 & ""
        End If


        sql &= " WHERE ssip_id = " & id
        'Response.Write(sql)
        ' Response.End()
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If
    End Sub

   

    Sub updateHRTab()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim new_ir_no As String = ""

        Dim scales() As Integer = {1000, 2000, 3000, 4000, 2000, 3000, 4000, 5000, 3000, 4000, 5000, 6000, 4000, 5000, 6000, 10000}

        sql = "UPDATE ssip_hr_tab SET result_id = " & txtSSIPResult.SelectedValue
        If txtSSIPResult.SelectedValue = "0" Then
            sql &= " , result_title = '' "
        Else
            sql &= " , result_title = '" & addslashes(txtSSIPResult.SelectedItem.Text) & "'"
        End If

        sql &= " , cat_id = " & txtssip_category.SelectedValue & ""
        sql &= " , cat_name = '" & addslashes(txtssip_category.SelectedValue) & "' "

        sql &= " , result_detail = '" & addslashes(txtdetail.Value) & "'"
        sql &= " , consider_detail = '" & addslashes(txtcommittee.Value) & "'"
        If txtedittopic.Value.Trim = "" Then
            sql &= " , innovation_subject = '" & addslashes(lblSSIPTopic.Text) & "'"
        Else
            sql &= " , innovation_subject = '" & addslashes(txtedittopic.Value) & "'"
        End If


        sql &= " , coupon_num = '" & (txtonthespot.Value) & "'"
        sql &= " , point_num = '" & (txtsrppoint.Text) & "'"
        If txtsrp_bonus.Text <> "" Then
            sql &= " , point_bonus = '" & (txtsrp_bonus.Text) & "'"
        Else
            sql &= " , point_bonus = null "
        End If
        sql &= " , point_total = '" & (txtsrp_total_point.Text) & "'"
        sql &= " , cash_num = '" & (txtcash.Value) & "'"
        sql &= " , date_award = '" & convertToSQLDatetime(txtaward_date.Text) & "'"
        sql &= " , date_award_ts = '" & ConvertDateStringToTimeStamp(txtaward_date.Text) & "'"
        sql &= " , date_receive = '" & convertToSQLDatetime(txtreceive_date.Text) & "'"
        sql &= " , date_receive_ts = '" & ConvertDateStringToTimeStamp(txtreceive_date.Text) & "'"

        'For i As Integer = 1 To 5
        '    If CType(panelHR2.FindControl("txtb" & i), RadioButton).Checked = True Then
        '        sql &= ", reward_type_id = '" & i & "'"
        '    End If
        'Next i

        'For i As Integer = 1 To 16
        '    If CType(panelHR2.FindControl("txtsc" & i), RadioButton).Checked = True Then
        '        sql &= ", scale_type_id = '" & i & "'"
        '        sql &= " , scale_type_value = " & scales(i - 1) & ""
        '    End If
        'Next i

        'sql &= " , benefit_num1 = '" & (txtscale1.Text) & "'"
        'sql &= " , benefit_num2 = '" & (txtscale2.Text) & "'"
        'sql &= " , benefit_num3 = '" & (txtscale3.Text) & "'"

        sql &= " , personal_reward_sum_id = '" & (txtreward_sum.SelectedValue) & "'"
        If txtreward_sum.SelectedValue = "" Then
            sql &= " , personal_reward_sum_name = ''"
        Else
            sql &= " , personal_reward_sum_name = '" & (txtreward_sum.SelectedItem.Text) & "'"
        End If

        sql &= " , intang_award_scale_id = '" & (txtaward_scale.SelectedValue) & "'"
        If txtaward_scale.SelectedValue = "" Then
            sql &= " , intang_award_scale_name = ''"
        Else
            sql &= " , intang_award_scale_name = '" & (txtaward_scale.SelectedItem.Text) & "'"
        End If


        If txtbenefit_old.Text = "" Then
            sql &= " , benefit_old = null"
        Else
            sql &= " , benefit_old = '" & (txtbenefit_old.Text) & "'"
        End If

        If txtbenefit_new.Text = "" Then
            sql &= " , benefit_new = null"
        Else
            sql &= " , benefit_new = '" & (txtbenefit_new.Text) & "'"
        End If

        If txtbenefit_factor.Text = "" Then
            sql &= " , benefit_factor = null"
        Else
            sql &= " , benefit_factor = '" & (txtbenefit_factor.Text) & "'"
        End If

        If txtbenefit_final.Text = "" Then
            sql &= " , benefit_final = null"
        Else
            sql &= " , benefit_final = '" & (txtbenefit_final.Text) & "'"
        End If

        If txtamount_reward.Text = "" Then
            sql &= " , award_amount = null"
        Else
            sql &= " , award_amount = '" & (txtamount_reward.Text) & "'"
        End If

        sql &= " , subject_keyword = '" & addslashes(txtkeyword.Value) & "'"
        sql &= " , subject_description = '" & addslashes(txtdescription.Value) & "'"
        sql &= " , reward_remark = '" & addslashes(txtreward_remark.Text) & "'"


        sql &= " WHERE ssip_id = " & id

        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If
        ' Response.Write(sql)
        ' Throw New Exception("xxx")
        If txtpublic.Checked = True Then
            sql = "UPDATE ssip_trans_list SET is_public = 1 "
        Else
            sql = "UPDATE ssip_trans_list SET is_public = 0 "
        End If

        sql &= " WHERE ssip_id = " & id
     
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If
    End Sub

    Protected Sub txtdept_all_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdept_all.SelectedIndexChanged
        bindRelatePerson()

        ViewState("selIndex") = txtdept_all.SelectedIndex
        If ViewState("selIndex") IsNot Nothing Then
            txtdept_all.SelectedIndex = ViewState("selIndex")
        End If
    End Sub

    Protected Sub cmdAddRelateDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddRelateDept.Click
        While txtdept_all.Items.Count > 0 AndAlso txtdept_all.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtdept_all.SelectedItem
            selectedItem.Selected = False
            txtdept_select.Items.Add(selectedItem)
            ' txtdept_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveRelateDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveRelateDept.Click
        While txtdept_select.Items.Count > 0 AndAlso txtdept_select.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtdept_select.SelectedItem
            selectedItem.Selected = False
            ' txtdept_all.Items.Add(selectedItem)
            txtdept_select.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdAddRelatePerson_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddRelatePerson.Click
        While txtperson_all.Items.Count > 0 AndAlso txtperson_all.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtperson_all.SelectedItem
            selectedItem.Selected = False
            txtperson_select.Items.Add(selectedItem)
            txtperson_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveRelatePerson_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveRelatePerson.Click
        While txtperson_select.Items.Count > 0 AndAlso txtperson_select.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtperson_select.SelectedItem
            selectedItem.Selected = False
            txtperson_all.Items.Add(selectedItem)
            txtperson_select.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdAddTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim new_order As String

        Try
            sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ssip_solution_list WHERE"
            If mode = "add" Then
                sql &= " session_id = '" & session_id & "'"
            Else
                sql &= " ssip_id = " & id
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If
            new_order = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("solution_id", "ssip_solution_list", conn)
            sql = "INSERT INTO ssip_solution_list (solution_id , ssip_id , problem_detail , suggestion_detail , order_sort , session_id) VALUES("
            sql &= "'" & pk & "' ,"
            If mode = "add" Then
                sql &= " null ,"
            Else
                sql &= "'" & id & "' ,"
            End If

            sql &= "'" & addslashes(txtadd_problem.Text) & "' ,"
            sql &= "'" & addslashes(txtadd_suggest.Text) & "' ,"
            sql &= "'" & new_order & "' ,"

            If mode = "add" Then
                sql &= " '" & Session.SessionID & "' "
            Else
                sql &= " '" & "" & "' "
            End If
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
                '  Return
            End If

            conn.setDBCommit()
            txtadd_problem.Text = ""
            txtadd_suggest.Text = ""
            bindSolutionList()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub addTopicWithNoCommit()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim new_order As String


        sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ssip_solution_list WHERE"
        If mode = "add" Then
            sql &= " session_id = '" & session_id & "'"
        Else
            sql &= " ssip_id = " & id
        End If

        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage & sql)
        End If
        new_order = ds.Tables(0).Rows(0)(0).ToString

        pk = getPK("solution_id", "ssip_solution_list", conn)
        sql = "INSERT INTO ssip_solution_list (solution_id , ssip_id , problem_detail , suggestion_detail , order_sort , session_id) VALUES("
        sql &= "'" & pk & "' ,"
        If mode = "add" Then
            sql &= " null ,"
        Else
            sql &= "'" & id & "' ,"
        End If

        sql &= "'" & addslashes(txtadd_problem.Text) & "' ,"
        sql &= "'" & addslashes(txtadd_suggest.Text) & "' ,"
        sql &= "'" & new_order & "' ,"

        If mode = "add" Then
            sql &= " '" & Session.SessionID & "' "
        Else
            sql &= " '" & "" & "' "
        End If
        sql &= ")"

        errorMsg = conn.executeSQLForTransaction(sql)

        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
            '  Return
        End If

        txtadd_problem.Text = ""
        txtadd_suggest.Text = ""

        Try
            ds.Dispose()
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub cmdDelTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridSuggest.Rows.Count
        ' Response.Write(i)
        Try

            For s As Integer = 0 To i - 1
                ' Response.Write(s)
                lbl = CType(GridSuggest.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridSuggest.Rows(s).FindControl("chkSelect"), CheckBox)
                '   Response.Write(chk.Checked)

                If chk.Checked = True Then
                    sql = "DELETE FROM  ssip_solution_list WHERE solution_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
                '  Response.Write(sql)
             
            Next s

            conn.setDBCommit()
            bindSolutionList()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddBenefit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddBenefit.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim new_order As String = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ssip_benefit_list WHERE"
            If mode = "add" Then
                sql &= " session_id = '" & session_id & "'"
            Else
                sql &= " ssip_id = " & id
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If
            new_order = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("benefit_id", "ssip_benefit_list", conn)
            sql = "INSERT INTO ssip_benefit_list (benefit_id , ssip_id , benefit_detail , order_sort , session_id) VALUES("
            sql &= "'" & pk & "' ,"
            If mode = "add" Then
                sql &= " null ,"
            Else
                sql &= "'" & id & "' ,"
            End If

            sql &= "'" & addslashes(txtadd_benefit.Text) & "' ,"
            sql &= "'" & new_order & "' ,"
            If mode = "add" Then
                sql &= " '" & Session.SessionID & "' "
            Else
                sql &= " '" & "" & "' "
            End If
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
                '  Return
            End If

            conn.setDBCommit()
            txtadd_benefit.Text = ""
            bindBenefitList()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub addBenefitWithNoCommit()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim new_order As String = ""
        Dim ds As New DataSet


        sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ssip_benefit_list WHERE"
        If mode = "add" Then
            sql &= " session_id = '" & session_id & "'"
        Else
            sql &= " ssip_id = " & id
        End If

        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage & sql)
        End If
        new_order = ds.Tables(0).Rows(0)(0).ToString

        pk = getPK("benefit_id", "ssip_benefit_list", conn)
        sql = "INSERT INTO ssip_benefit_list (benefit_id , ssip_id , benefit_detail , order_sort , session_id) VALUES("
        sql &= "'" & pk & "' ,"
        If mode = "add" Then
            sql &= " null ,"
        Else
            sql &= "'" & id & "' ,"
        End If

        sql &= "'" & addslashes(txtadd_benefit.Text) & "' ,"
        sql &= "'" & new_order & "' ,"
        If mode = "add" Then
            sql &= " '" & Session.SessionID & "' "
        Else
            sql &= " '" & "" & "' "
        End If
        sql &= ")"

        errorMsg = conn.executeSQLForTransaction(sql)

        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
            '  Return
        End If
        txtadd_benefit.Text = ""
        ds.Dispose()

    End Sub

    Protected Sub cmdDelBenefit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelBenefit.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridBenefit.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridBenefit.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridBenefit.Rows(s).FindControl("chkSelect"), CheckBox)
                '  response.write(lbl.Text)

                If chk.Checked = True Then
                    sql = "DELETE FROM  ssip_benefit_list WHERE benefit_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
                ' Response.Write(sql)
               
            Next s

            conn.setDBCommit()
            bindBenefitList()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Button11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim filename As String()
        Dim ds As New DataSet

        Dim new_order As String = ""
        Try
            sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ssip_file_list WHERE"
            If mode = "add" Then
                sql &= " session_id = '" & session_id & "'"
            Else
                sql &= " ssip_id = " & id
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If
            new_order = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("file_id", "ssip_file_list", conn)

            sql = "INSERT INTO ssip_file_list (file_id , ssip_id , order_sort , session_id) VALUES("
            sql &= "" & pk & " , "
            If mode = "add" Then
                sql &= " null , "
            Else
                sql &= "" & id & " , "
            End If
            sql &= "" & new_order & " , "
            ' sql &= "" & formId & " , "
            '  sql &= "'" & strFileName & "' , "
            '  sql &= "'" & pk & "." & extension & "' , "
            '  sql &= "'" & FileUpload1.PostedFile.ContentLength & "' , "
            sql &= "'" & Session.SessionID & "'  "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            'Response.Write("pk = " & pk)
            If errorMsg <> "" Then
                Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
            End If

            If Not IsNothing(FileUpload1.PostedFile) Then
                Dim strFileName = FileUpload1.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName <> "" Then
                    filename = strFileName.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    sql = "UPDATE ssip_file_list SET file_name_before = '" & strFileName & "' "
                    sql &= " , file_path_before = '" & pk & "_before" & "." & extension & "'"
                    sql &= " WHERE file_id = " & pk
                    errorMsg = conn.executeSQLForTransaction(sql)
                    'Response.Write("pk = " & pk)
                    If errorMsg <> "" Then
                        Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                    End If
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/ssip/attach_file/" & pk & "_before." & extension))
                End If


            End If

            If Not IsNothing(FileUpload2.PostedFile) Then
                Dim strFileName = FileUpload2.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName <> "" Then
                    filename = strFileName.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    sql = "UPDATE ssip_file_list SET file_name_after = '" & strFileName & "' "
                    sql &= " , file_path_after = '" & pk & "_after" & "." & extension & "'"
                    sql &= " WHERE file_id = " & pk
                    errorMsg = conn.executeSQLForTransaction(sql)
                    'Response.Write("pk = " & pk)
                    If errorMsg <> "" Then
                        Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                    End If
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("../share/ssip/attach_file/" & pk & "_after." & extension))
                End If


            End If

            conn.setDBCommit()
            bindFile()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdDelFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelFile.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim lblFilePath1 As Label
        Dim lblFilePath2 As Label

        i = GridFile.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridFile.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridFile.Rows(s).FindControl("chkSelect"), CheckBox)
                '  response.write(lbl.Text)

                If chk.Checked = True Then
                    sql = "DELETE FROM  ssip_file_list WHERE file_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If

                    lblFilePath1 = CType(GridFile.Rows(s).FindControl("lblFilePath1"), Label)
                    If lblFilePath1.Text <> "" Then
                        File.Delete(Server.MapPath("../share/ssip/attach_file/" & lblFilePath1.Text))
                    End If
                    lblFilePath2 = CType(GridFile.Rows(s).FindControl("lblFilePath2"), Label)
                    If lblFilePath2.Text <> "" Then
                        File.Delete(Server.MapPath("../share/ssip/attach_file/" & lblFilePath2.Text))
                    End If
                End If
                ' Response.Write(sql)

            Next s

            conn.setDBCommit()
            bindFile()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdHRAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHRAdd.Click
        While txthr_alldept.Items.Count > 0 AndAlso txthr_alldept.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txthr_alldept.SelectedItem
            selectedItem.Selected = False
            txthr_selectdept.Items.Add(selectedItem)
            ' txtdept_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdHrRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHrRemove.Click
        While txthr_selectdept.Items.Count > 0 AndAlso txthr_selectdept.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txthr_selectdept.SelectedItem
            selectedItem.Selected = False
            ' txtdept_all.Items.Add(selectedItem)
            txthr_selectdept.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdImplAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdImplAdd.Click
        While txthr_alldept2.Items.Count > 0 AndAlso txthr_alldept2.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txthr_alldept2.SelectedItem
            selectedItem.Selected = False
            txthr_impldept.Items.Add(selectedItem)
            ' txtdept_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdImplRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdImplRemove.Click
        While txthr_impldept.Items.Count > 0 AndAlso txthr_impldept.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txthr_impldept.SelectedItem
            selectedItem.Selected = False
            ' txtdept_all.Items.Add(selectedItem)
            txthr_impldept.Items.Remove(selectedItem)
        End While
    End Sub

  

    Protected Sub cmdDraft1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDraft1.Click

    End Sub

    Protected Sub cmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim filename As String()
        Dim ds As New DataSet

        Try


            If Not IsNothing(FileUpload0.PostedFile) Then
                Dim strFileName = FileUpload0.FileName
                Dim extension As String
                Dim iCount As Integer = 0

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)
                Try
                    sql = "SELECT ISNULL(MAX(file_id),0) + 1 AS pk FROM ssip_attachment"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO ssip_attachment (file_id , ssip_id ,  file_name , file_path , file_size , session_id) VALUES("
                sql &= "" & pk & " , "
                If id = "" Then
                    sql &= " null , "
                Else
                    sql &= "" & id & " , "
                End If

                sql &= "'" & strFileName & "' , "
                sql &= "'" & pk & "." & extension & "' , "
                sql &= "'" & FileUpload0.PostedFile.ContentLength & "' , "
                sql &= "'" & Session.SessionID & "'  "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                'Response.Write("pk = " & pk)
                If errorMsg <> "" Then
                    Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                End If

                FileUpload0.PostedFile.SaveAs(Server.MapPath("../share/ssip/attach_file/" & pk & "." & extension))

                conn.setDBCommit()
            End If

            bindFileAttach()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Sub bindFileAttach()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM ssip_attachment a WHERE 1 = 1 "
            If mode = "add" Then
                sql &= " AND a.session_id = '" & session_id & "'"
            Else
                sql &= " AND a.ssip_id = " & id
            End If
            ' Response.Write(sql)

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridFileAttach.DataSource = ds.Tables(0)
            GridFileAttach.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Protected Sub cmdDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteFile.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim lblFilePath As Label

        i = GridFileAttach.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridFileAttach.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridFileAttach.Rows(s).FindControl("chkSelect"), CheckBox)

                '  response.write(lbl.Text)
                If chk.Checked Then
                    sql = "DELETE FROM ssip_attachment WHERE file_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        'Exit For
                    End If
                End If


            Next s

            For s As Integer = 0 To i - 1
                chk = CType(GridFileAttach.Rows(s).FindControl("chkSelect"), CheckBox)
                If chk.Checked Then
                    lblFilePath = CType(GridFileAttach.Rows(s).FindControl("lblFilePath"), Label)
                    File.Delete(Server.MapPath("../share/ssip/attach_file/" & lblFilePath.Text))
                End If
            Next s

            conn.setDBCommit()
            bindFileAttach()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindCategoryCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_m_category a WHERE ISNULL(a.is_delete,0) = 0 "
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtssip_category.DataSource = ds
            txtssip_category.DataBind()

            txtssip_category.Items.Insert(0, New ListItem("-- Please Select --", "0"))
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
            sql = "SELECT * FROM ssip_manager_comment a INNER JOIN ssip_manager_tab b ON a.comment_id = b.comment_id  WHERE a.ssip_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count = 0 And viewtype = "" Then
                '   tab_approve.Visible = False
            ElseIf viewtype = "dept" Then
                '    tab_approve.Visible = True
            End If

            lblNumSup.Text = " (" & ds.Tables("t1").Rows.Count & ")"
            GridComment.DataSource = ds
            GridComment.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCommitteeCommentList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT *  "
            sql &= " , CASE WHEN intang_award_scale_id = 1 THEN 'Improve Environment and Safety' "
            sql &= " WHEN intang_award_scale_id = 2 THEN 'Increase Efficiency' "
            sql &= " WHEN intang_award_scale_id = 3 THEN 'Customer Service' "
            sql &= " WHEN intang_award_scale_id = 4 THEN 'Healthcare' END AS 'scale_name' "
            sql &= " FROM ssip_manager_comment a INNER JOIN ssip_committee_tab b ON a.comment_id = b.comment_id "
            sql &= " WHERE b.ssip_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(sql)
            If ds.Tables("t1").Rows.Count = 0 And viewtype = "" Then
                '   tab_approve.Visible = False
            ElseIf viewtype = "dept" Then
                '    tab_approve.Visible = True
            End If

            lblNumCom.Text = " (" & ds.Tables("t1").Rows.Count & ")"
            GridCommittee.DataSource = ds
            GridCommittee.DataBind()

          

            txtscorename1.Attributes.Add("onchange", "$('#ctl00_ContentPlaceHolder1_txtscore1').val(this.value)")
            txtscorename2.Attributes.Add("onchange", "$('#ctl00_ContentPlaceHolder1_txtscore2').val(this.value)")
            txtscorename3.Attributes.Add("onchange", "$('#ctl00_ContentPlaceHolder1_txtscore3').val(this.value)")
            txtscorename4.Attributes.Add("onchange", "$('#ctl00_ContentPlaceHolder1_txtscore4').val(this.value)")
            txtscorename5.Attributes.Add("onchange", "$('#ctl00_ContentPlaceHolder1_txtscore5').val(this.value)")
        Catch ex As Exception
            Response.Write(ex.Message & "bindCommitteeCommentList" & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub onDeleteComment(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "DELETE FROM ssip_manager_comment WHERE comment_id = " & e.CommandArgument.ToString
            errorMsg = conn.executeSQLForTransaction(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            updateOnlyLog("0", "Delete Comment")
            conn.setDBCommit()

            bindCommentList()
            ' bindGridIDPLog()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try

    End Sub

    Sub onDeleteCommitteComment(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "DELETE FROM ssip_manager_comment WHERE comment_id = " & e.CommandArgument.ToString
            errorMsg = conn.executeSQLForTransaction(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            updateOnlyLog("0", "Delete Comment")
            conn.setDBCommit()

            bindCommitteeCommentList()

          
            panelAddCommiteeComment.Visible = True
            ' bindGridIDPLog()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Private Sub bindGridSSIPLog()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * , ISNULL(b.status_name, a.log_remark) AS ssip_status_name FROM ssip_status_log a LEFT OUTER JOIN ssip_status_list b ON a.status_id = b.status_id WHERE a.ir_id = " & id)
            sqlB.Append(" ORDER BY log_time ASC")
            ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

            ' Response.Write(sqlB.ToString)
            GridviewIDPLog.DataSource = ds
            GridviewIDPLog.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub

    Protected Sub GridviewIDPLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridviewIDPLog.PageIndexChanging
        GridviewIDPLog.PageIndex = e.NewPageIndex
        bindGridSSIPLog()
    End Sub

    Protected Sub GridviewIDPLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridviewIDPLog.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim startTS As String = ""
            Dim sql As String
            Dim ds As New DataSet
            Try
                Dim lblDuration As Label = CType(e.Row.FindControl("lblDuration"), Label)
                Dim lblDateTS As Label = CType(e.Row.FindControl("lblTS"), Label)

                sql = "SELECT * FROM ssip_status_log a INNER JOIN ssip_status_list b ON a.status_id = b.status_id WHERE a.status_id <> 1 AND a.ir_id = " & id
                sql &= " ORDER BY log_status_id ASC"
                ds = conn.getDataSetForTransaction(sql, "table1")
                startTS = ds.Tables("table1").Rows(0)("log_time_ts").ToString

                If startTS <> lblDateTS.Text Then
                    lblDuration.Text = MinuteDiff(startTS, lblDateTS.Text)
                Else
                    lblDuration.Text = 0
                End If

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()

            End Try
        End If
       
    End Sub

    Protected Sub GridviewIDPLog_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridviewIDPLog.SelectedIndexChanged

    End Sub

    



    Protected Sub GridComment_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridComment.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
         
            Dim lblEmpcode As Label = CType(e.Row.FindControl("lblEmpcode"), Label)
            Dim cmdDelComment As ImageButton = CType(e.Row.FindControl("cmdDelComment"), ImageButton)
            Dim cmdEditComment As ImageButton = CType(e.Row.FindControl("cmdEditComment"), ImageButton)
            Dim cmdMgrReview As ImageButton = CType(e.Row.FindControl("cmdMgrReview"), ImageButton)

            Dim lblDept As Label = CType(e.Row.FindControl("lblDept"), Label)
            Dim lblBenefit As Label = CType(e.Row.FindControl("lblBenefit"), Label)

            cmdEditComment.Attributes.Add("onclick", "editDetail('" & lblPK.Text & "');return false;")
            cmdMgrReview.Attributes.Add("onclick", "openReview('" & lblPK.Text & "');return false;")

            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ssip_manager_relate_dept WHERE comment_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblDept.Text &= ds.Tables("t1").Rows(i)("dept_name").ToString & "<br/>"
                Next i


                sql = "SELECT * FROM ssip_manager_tab WHERE comment_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t2")
                If ds.Tables("t2").Rows(0)("chk_benefit1").ToString = "1" Then
                    lblBenefit.Text &= "เพิ่มความพึงพอใจให้แก่ลูกค้า <br/>"
                End If
                If ds.Tables("t2").Rows(0)("chk_benefit2").ToString = "1" Then
                    lblBenefit.Text &= "ประหยัดค่าใช้จ่ายเป็นเงิน " & ds.Tables("t2").Rows(0)("q5_budget").ToString & " บาท<br/>"
                End If
                If ds.Tables("t2").Rows(0)("chk_benefit3").ToString = "1" Then
                    lblBenefit.Text &= "เพิ่มรายได้หรือกำไรให้แก่องค์กร <br/>"
                End If
                If ds.Tables("t2").Rows(0)("chk_benefit4").ToString = "1" Then
                    lblBenefit.Text &= "เพิ่มประสิทธิภาพในการทำงาน <br/>"
                End If
                If ds.Tables("t2").Rows(0)("chk_benefit5").ToString = "1" Then
                    lblBenefit.Text &= "ปรับปรุงคุณภาพของสถานที่ทำงาน <br/>"
                End If
                If ds.Tables("t2").Rows(0)("chk_benefit6").ToString = "1" Then
                    lblBenefit.Text &= "อื่นๆ  <br/>"
                End If
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try

            If lblEmpcode.Text = Session("emp_code").ToString Then
                cmdDelComment.Visible = True
                ' cmdEditComment.Visible = True
                cmdMgrReview.Visible = True
            Else
                cmdDelComment.Visible = False
                '  cmdEditComment.Visible = False
                cmdMgrReview.Visible = False
            End If

          

        End If
    End Sub

    Protected Sub GridComment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridComment.SelectedIndexChanged

    End Sub

    Protected Sub cmdOrderTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOrderTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim txtorder As TextBox

        i = GridSuggest.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridSuggest.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridSuggest.Rows(s).FindControl("chkSelect"), CheckBox)
                txtorder = CType(GridSuggest.Rows(s).FindControl("txtorder"), TextBox)

                ' If chk.Checked = True Then
                sql = "UPDATE  ssip_solution_list SET order_sort = " & txtorder.Text & " WHERE solution_id = " & lbl.Text
                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
                'End If
            Next s

            conn.setDBCommit()

            bindSolutionList()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdOrderBenefit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOrderBenefit.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox
        Dim txtorder As TextBox

        i = GridBenefit.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridBenefit.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridBenefit.Rows(s).FindControl("chkSelect"), CheckBox)
                txtorder = CType(GridBenefit.Rows(s).FindControl("txtorder"), TextBox)

                ' If chk.Checked = True Then
                sql = "UPDATE  ssip_benefit_list SET order_sort = " & txtorder.Text & " WHERE benefit_id = " & lbl.Text
                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
                'End If
            Next s

            conn.setDBCommit()

            bindSolutionList()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub updateSSIPStatus()
        Dim sql As String
        Dim errorMsg As String

        sql = "UPDATE ssip_trans_list SET status_id = " & txtstatus.SelectedValue & " WHERE ssip_id = " & id
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If

        updateOnlyLog(txtstatus.SelectedValue, "HR process")


    End Sub

    

    Sub isHasRow2(ByVal table As String, ByVal pk As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""


        sql = "SELECT * FROM " & table & " WHERE comment_id = " & pk
        ' Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage)
        End If

        If ds.Tables("t1").Rows.Count <= 0 Then
            sql = "INSERT INTO " & table & " (comment_id , ssip_id) VALUES( "
            sql &= "" & pk & " , "
            sql &= "" & id & ""
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If

        End If

        ds.Dispose()   

    End Sub

    Protected Sub GridCommittee_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCommittee.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim oGridView As GridView = DirectCast(sender, GridView)
            Dim oGridViewRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

            Dim oTableCell As New TableCell()

            '  oTableCell.ColumnSpan = 2
            oGridViewRow.Cells.Add(oTableCell)
         
            'Add Intangible
            oTableCell = New TableCell()
            oTableCell.HorizontalAlign = HorizontalAlign.Center
            oTableCell.Text = "<div align='center' style='width:100%;color:#cc3300'>การพิจารณาประโยชน์ที่จับต้องไม่ได้ (Intangible)</div>"

            ' oTableCell.ForeColor = Drawing.Color.SkyBlue
            oTableCell.ColumnSpan = 5

            oGridViewRow.Cells.Add(oTableCell)

            'Add Tangible
            oTableCell = New TableCell()
            oTableCell.HorizontalAlign = HorizontalAlign.Center
            oTableCell.Text = "<div align='center' style='width:100%;color:#3333cc'>การพิจารณาเรื่องประโยชน์ที่จับต้องได้ (Tangible)</div>"

            'oTableCell.ForeColor = Drawing.Color.SkyBlue
            oTableCell.ColumnSpan = 3
            oGridViewRow.Cells.Add(oTableCell)

            oTableCell = New TableCell()
            oTableCell.ColumnSpan = 3
            oGridViewRow.Cells.Add(oTableCell)

            oGridView.Controls(0).Controls.AddAt(0, oGridViewRow)
        End If
    End Sub

  

    Protected Sub GridCommittee_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCommittee.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)

            Dim lblEmpcode As Label = CType(e.Row.FindControl("lblEmpcode"), Label)
            Dim cmdDelComment As ImageButton = CType(e.Row.FindControl("cmdDelComment"), ImageButton)

            Dim cmdMgrReview As ImageButton = CType(e.Row.FindControl("cmdMgrReview"), ImageButton)
            Dim lbls1 As Label = CType(e.Row.FindControl("lbls1"), Label)
            Dim lbls2 As Label = CType(e.Row.FindControl("lbls2"), Label)
            Dim lbls3 As Label = CType(e.Row.FindControl("lbls3"), Label)
            Dim lbls4 As Label = CType(e.Row.FindControl("lbls4"), Label)
            Dim lbls5 As Label = CType(e.Row.FindControl("lbls5"), Label)

            cmdMgrReview.Attributes.Add("onclick", "openCommiteeReview('" & lblPK.Text & "');return false;")

            If lblEmpcode.Text = Session("emp_code").ToString Then
                If viewtype = "com" Then
                    cmdDelComment.Visible = True
                    cmdMgrReview.Visible = True
                Else
                    cmdDelComment.Visible = False
                    cmdMgrReview.Visible = False
                End If


            Else
                cmdDelComment.Visible = False
                cmdMgrReview.Visible = False
            End If

            If viewtype <> "hr" Then
                If lblEmpcode.Text <> Session("emp_code").ToString Then
                    lbls1.Text = "-"
                    lbls2.Text = "-"
                    lbls3.Text = "-"
                    lbls4.Text = "-"
                    lbls5.Text = "-"
                End If
            End If

        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim sql As String
            Dim ds As New DataSet
            Dim ds2 As New DataSet
            Try
                sql = "SELECT * , ISNULL(award_amount,0) AS amount FROM ssip_hr_tab WHERE ssip_id = " & id
                'Response.Write(sql)
                ds2 = conn.getDataSetForTransaction(sql, "t0")
              
                ' Response.Write(ds.Tables("t0").Rows(0)("personal_reward_sum_name").ToString)

                sql = "SELECT SUM(score1) AS s1 , SUM(score2) AS s2 , SUM(score3) AS s3 ,SUM(score4) AS s4 , SUM(score5) AS s5 "
                sql &= " , AVG(score1) AS a1 , AVG(score2) AS a2 , AVG(score3) AS a3 , AVG(score4) AS a4 , AVG(score5) AS a5"
                sql &= " FROM ssip_committee_tab a INNER JOIN ssip_manager_comment b ON a.comment_id = b.comment_id WHERE a.ssip_id = " & id
                sql &= " GROUP BY a.ssip_id"

                ds = conn.getDataSetForTransaction(sql, "t1")
                '  Response.Write(sql)
                If ds.Tables("t1").Rows.Count > 0 Then
                    e.Row.Cells(0).Text = "<table  ><tr><td>คะแนนรวม:</td></tr><tr><td>คะแนนเฉลี่ย:</td></tr>  </table>"
                    e.Row.Cells(0).Font.Bold = True

                    For i As Integer = 1 To 5
                        e.Row.Cells(i).Text = "<table  cellspacing='0' border='0'><tr><td>" & ds.Tables("t1").Rows(0)("s" & i).ToString & "</td></tr> <tr><td>" & ds.Tables("t1").Rows(0)("a" & i).ToString & "</td></tr> "
                        '  e.Row.Cells(i).Text &= "<tr><td>&nbsp;</td></tr>"

                        e.Row.Cells(i).Text &= "</table>"
                    Next i

                    'For i As Integer = 1 To 1
                    '    e.Row.Cells(i).Text &= "<table  cellspacing='0' border='0'>"
                    '    e.Row.Cells(i).Text &= "<tr><td>" & ds2.Tables("t0").Rows(0)("personal_reward_sum_name").ToString & "&nbsp;</td></tr>"
                    '    e.Row.Cells(i).Text &= "<tr><td>" & ds2.Tables("t0").Rows(0)("intang_award_scale_name").ToString & "&nbsp;</td></tr>"
                    '    e.Row.Cells(i).Text &= "<tr><td>-</td></tr>"
                    '    e.Row.Cells(i).Text &= "<tr><td>" & FormatNumber((ds2.Tables("t0").Rows(0)("amount").ToString), 2) & "</td></tr>"
                    '    e.Row.Cells(i).Text &= "</table>"
                    'Next
                    lblPersonSum.Text = ds2.Tables("t0").Rows(0)("personal_reward_sum_name").ToString
                    lblIntangAward.Text = ds2.Tables("t0").Rows(0)("intang_award_scale_name").ToString
                    If CDbl(ds2.Tables("t0").Rows(0)("amount").ToString) <= 200000 Then
                        lblTangAward.Text = "10% of estimated benefits"
                    ElseIf CDbl(ds2.Tables("t0").Rows(0)("amount").ToString) > 200000 And CDbl(ds2.Tables("t0").Rows(0)("amount").ToString) < 1000000 Then
                        lblTangAward.Text = "Baht 20,000 for the first Baht 200,000 +3% of estimated benefits over Baht 200,000"
                    ElseIf CDbl(ds2.Tables("t0").Rows(0)("amount").ToString) > 1000000 Then
                        lblTangAward.Text = "Baht 44,000 for the first Baht 1,000,000 +1% of estimated benefits more Baht 1,000,000"
                    End If

                    lblAwardAmount.Text = FormatNumber((CDbl(ds2.Tables("t0").Rows(0)("amount").ToString)), 2)
                End If
             
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
           
        End If
    End Sub

    Protected Sub GridCommittee_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridCommittee.SelectedIndexChanged

    End Sub

    Sub insertAlertLog(ByVal cmd As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet

        sql = "INSERT INTO ssip_alert_log (ssip_id , alert_date , alert_date_ts , alert_method , subject , send_to) VALUES( "
        sql &= "'" & id & "' ,"
        sql &= "GETDATE() ,"
        sql &= "'" & Date.Now.Ticks & "' ,"
        sql &= "'" & cmd & "' ,"
        sql &= "'" & txtsubject.SelectedItem.Text & "' ,"
        sql &= "'" & txtto.Value & "' "
        sql &= ")"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & "" & sql)
        End If
    End Sub

    Private Sub bindGridAlertLog()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * FROM ssip_alert_log a  WHERE a.ssip_id = " & id)
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

    Protected Sub cmdSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendMail.Click
        Try
            updateOnlyLog("0", txtsubject.SelectedItem.Text)

            insertAlertLog("Email")
            If chk_sms.Checked = True Then
                insertAlertLog("SMS")
            End If

            getMailAndSMS()

            conn.setDBCommit()

            bindGridAlertLog()
            txtto.Value = ""
            txtcc.Value = ""
            txtmessage.Value = ""
            txtsubject.SelectedIndex = 0

            bindGridSSIPLog()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Response.End()
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
            Dim p3 As String = "[SSIP #" & lblSSIPNo.Text & "]  : " & txttopic.Text
            Dim p4 As String = txtmessage.Value
            Dim p5 = ""

            ' Dim parameters As Object() = New Object() {p1, p2, p3, p4, p5}

            'thread.Start(parameters)
            Dim host As String = ConfigurationManager.AppSettings("frontHost").ToString
            Dim key = UserActivation.GetActivationLink("ssip/form_ssip.aspx?mode=edit&id=" & id)
            Dim msgbody As String = ""

            msgbody &= txtsubject.SelectedItem.Text & "<br/>" & vbCrLf
            msgbody &= "Please find Staff Suggestion and Innovation. Please review on this idea, for fast access online review, please kindly open a link below <br/>" & vbCrLf
            If txtsubject.SelectedValue = "4" Then
                msgbody &= vbCrLf & "<br/><a href='http://" & host & "/login.aspx?viewtype=sup&key=" & key & "'>Staff Suggestion and Innovation Online</a>"

            Else
                msgbody &= vbCrLf & "<br/><a href='http://" & host & "/login.aspx?viewtype=com&key=" & key & "'>Staff Suggestion and Innovation Online</a>"

            End If
           
            sendMailWithCC_SSIP(email_list, cc_list, bcc_list, p3, txtmessage.Value & msgbody, "", "ir")

        Catch ex As Exception
            Response.Write("Send mail :: " & ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAddUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddUpdate.Click
        Dim sql As String = ""
        Dim errorMsg As String = ""
        Dim pk As String = ""

        Try
            pk = getPK("inform_id", "ssip_information_update", conn)
            sql = "INSERT INTO ssip_information_update (inform_id , ssip_id , inform_type , inform_detail , inform_date , inform_date_ts , inform_by , inform_emp_code , inform_dept_name , inform_costcenter) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " 'ssip' ,"
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
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Private Sub bindGridInformationUpdate()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * FROM ssip_information_update WHERE ssip_id =  " & id)


            ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

            lblNumComment.Text = "(" & ds.Tables("table1").Rows.Count & ")"
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

    Protected Sub GridInformation_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridInformation.RowDeleting
        Dim sql As String
        Dim errorMSg As String

        Try
            sql = "DELETE FROM ssip_information_update WHERE inform_id = '" & GridInformation.DataKeys(e.RowIndex).Value & "'"
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

    Protected Sub cmdCommitteComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCommitteComment.Click
        saveCommitteeReview()
    End Sub

    Sub saveCommitteeReview()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String


        Try
         
            pk = getPK("comment_id", "ssip_manager_comment", conn)
            sql = "INSERT INTO ssip_manager_comment (comment_id , ssip_id , subject , detail "
            sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
            sql &= ",create_date , create_date_ts  "
            sql &= ") VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & "" & "' ,"
            sql &= " '" & "" & "' ,"
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

            isHasRow2("ssip_committee_tab", pk)

            sql = "UPDATE ssip_committee_tab SET q1_type = '" & txtcom_ans1.SelectedValue & "'"
            sql &= " , q1_reason = '" & addslashes(txtcom_reason1.Text) & "' "
            sql &= " WHERE comment_id = " & pk
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            sql = "UPDATE ssip_committee_tab SET q1_type = '" & txtcom_ans1.SelectedValue & "' "
            sql &= " , q1_reason = '" & addslashes(txtcom_reason1.Text) & "' "
            sql &= " , q2_type = '" & txtcom_ans2.SelectedValue & "' "
            sql &= " , q2_reason = '" & addslashes(txtcom_reason2.Text) & "' "
            If txtamount_old.Text = "" Then
                sql &= " , amount_old = null "
            Else
                sql &= " , amount_old = '" & (txtamount_old.Text) & "' "
            End If

            If txtamount_new.Text = "" Then
                sql &= " , amount_new = null "
            Else
                sql &= " , amount_new = '" & (txtamount_new.Text) & "' "
            End If

            If txtamount_cal.Text = "" Then
                sql &= " , amount_cal = null "
            Else
                sql &= " , amount_cal = '" & (txtamount_cal.Text) & "' "
            End If

            If txtamount_save.Text = "" Then
                sql &= " , amount_save = null "
            Else
                sql &= " , amount_save = '" & (txtamount_save.Text) & "' "
            End If


            sql &= " , benefit1_id = '" & txtscorename1.SelectedValue & "' "
            sql &= " , benefit1_name = '" & addslashes(txtscorename1.SelectedItem.Text) & "' "
            sql &= " , score1 = '" & txtscore1.Text & "' "

            sql &= " , benefit2_id = '" & txtscorename2.SelectedValue & "' "
            sql &= " , benefit2_name = '" & addslashes(txtscorename2.SelectedItem.Text) & "' "
            sql &= " , score2 = '" & txtscore2.Text & "' "

            sql &= " , benefit3_id = '" & txtscorename3.SelectedValue & "' "
            sql &= " , benefit3_name = '" & addslashes(txtscorename3.SelectedItem.Text) & "' "
            sql &= " , score3 = '" & txtscore3.Text & "' "

            sql &= " , benefit4_id = '" & txtscorename4.SelectedValue & "' "
            sql &= " , benefit4_name = '" & addslashes(txtscorename4.SelectedItem.Text) & "' "
            sql &= " , score4 = '" & txtscore4.Text & "' "

            sql &= " , benefit5_id = '" & txtscorename5.SelectedValue & "' "
            sql &= " , benefit5_name = '" & addslashes(txtscorename5.SelectedItem.Text) & "' "
            sql &= " , score5 = '" & txtscore5.Text & "' "

            sql &= " , total_score = '" & txtsum.Text & "' "
            sql &= " , result_text = '" & getResultText() & "' "
            sql &= " , last_update = GETDATE() "
            sql &= " , intang_award_scale_id  ='" & txtadd_award_scale.SelectedValue & "' "

            If txtadd_award_scale.SelectedValue = "" Then
                sql &= " , intang_award_scale_name = '-'  "
            Else
                sql &= " , intang_award_scale_name = '" & txtadd_award_scale.SelectedItem.Text & "'  "
            End If



            sql &= " WHERE comment_id = " & pk
            ' Response.Write(sql)

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If

            updateOnlyLog("0", "Review")
            conn.setDBCommit()

            txtadd_subject.Text = ""
            txtcomment_detail.Value = ""
            bindCommitteeCommentList()
            bindGridSSIPLog()


        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
            Return
        End Try

        Response.Redirect("form_ssip.aspx?mode=edit&id=" & id)
    End Sub

    Function getResultText() As String
        Dim result As String
        Dim total As Integer = 0

        Try
            total = CInt(txtsum.Text)
        Catch ex As Exception
            total = 0
            Response.Write(ex.Message)
        End Try

        If total >= 34 And total < 45 Then
            result = "Moderate"
        ElseIf total >= 45 And total < 65 Then
            result = "Substantial"
        ElseIf total >= 65 And total < 80 Then
            result = "High"
        ElseIf total > 80 Then
            result = "Exceptional"
        Else
            result = "Trophy"
        End If

        Return result
    End Function

    Sub bindScore()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM ssip_m_score WHERE score_type = 1"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtscorename1.DataSource = ds
            txtscorename1.DataBind()

            sql = "SELECT * FROM ssip_m_score WHERE score_type = 2"
            ds = conn.getDataSetForTransaction(sql, "t2")
            txtscorename2.DataSource = ds
            txtscorename2.DataBind()

            sql = "SELECT * FROM ssip_m_score WHERE score_type = 3"
            ds = conn.getDataSetForTransaction(sql, "t3")
            txtscorename3.DataSource = ds
            txtscorename3.DataBind()

            sql = "SELECT * FROM ssip_m_score WHERE score_type = 4"
            ds = conn.getDataSetForTransaction(sql, "t4")
            txtscorename4.DataSource = ds
            txtscorename4.DataBind()

            sql = "SELECT * FROM ssip_m_score WHERE score_type = 5"
            ds = conn.getDataSetForTransaction(sql, "t5")
            txtscorename5.DataSource = ds
            txtscorename5.DataBind()

            txtscorename1.Items.Insert(0, New ListItem("-- Please Select --", "0"))
            txtscorename2.Items.Insert(0, New ListItem("-- Please Select --", "0"))
            txtscorename3.Items.Insert(0, New ListItem("-- Please Select --", "0"))
            txtscorename4.Items.Insert(0, New ListItem("-- Please Select --", "0"))
            txtscorename5.Items.Insert(0, New ListItem("-- Please Select --", "0"))
        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub onSubmitScore(ByVal sc As DropDownList, ByVal txt As TextBox)
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_m_score WHERE score_id = " & sc.SelectedValue
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                ' txt.Text = ds.Tables("t1").Rows(0)("score_value").ToString
            Else
                ' txt.Text = 0
            End If
            ' Response.Write(sql)
            totalScore()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub totalScore()
        Dim p1 As Integer = 0
        Dim p2 As Integer = 0
        Dim p3 As Integer = 0
        Dim p4 As Integer = 0
        Dim p5 As Integer = 0

        Try
            p1 = Convert.ToInt32(txtscore1.Text)
        Catch ex As Exception
            p1 = 0
        End Try

        Try
            p2 = Convert.ToInt32(txtscore2.Text)
        Catch ex As Exception
            p2 = 0
        End Try

        Try
            p3 = Convert.ToInt32(txtscore3.Text)
        Catch ex As Exception
            p3 = 0
        End Try

        Try
            p4 = Convert.ToInt32(txtscore4.Text)
        Catch ex As Exception
            p4 = 0
        End Try

        Try
            p5 = Convert.ToInt32(txtscore5.Text)
        Catch ex As Exception
            p5 = 0
        End Try

        txtsum.Text = p1 + p2 + p3 + p4 + p5

    End Sub

    Protected Sub txtreward_sum_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtreward_sum.SelectedIndexChanged
        calReward()
    End Sub

    Protected Sub txtaward_scale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtaward_scale.SelectedIndexChanged
        calReward()
    End Sub

    Protected Sub txtamount_reward_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtamount_reward.TextChanged

        ' calAmountText()
    End Sub

    Protected Sub txtbenefit_factor_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbenefit_factor.TextChanged
        calBenefit()
        calAmountText()
    End Sub

    Protected Sub txtbenefit_new_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbenefit_new.TextChanged
        calBenefit()
        calAmountText()
    End Sub

    Protected Sub txtbenefit_old_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbenefit_old.TextChanged
        calBenefit()
        calAmountText()
    End Sub

    Sub calSRPBonus()
        If txtsrppoint.Text <> "" And txtsrp_bonus.Text <> "" Then
            Try
                txtsrp_total_point.Text = CDbl(txtsrppoint.Text) * CDbl(txtsrp_bonus.Text)
            Catch ex As Exception
                txtsrp_total_point.Text = 0
            End Try

        End If
    End Sub

    Protected Sub txtsrp_bonus_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsrp_bonus.TextChanged
        calSRPBonus()
    End Sub

    Protected Sub txtsrppoint_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsrppoint.TextChanged
        calSRPBonus()
    End Sub

    Protected Sub GridFile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridFile.SelectedIndexChanged

    End Sub

    Sub bindDeptAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
           
            sql = "SELECT * from user_dept ORDER BY dept_name_en"


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

    Protected Sub txtscorename1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename1.SelectedIndexChanged
        onSubmitScore(txtscorename1, txtscore1)
    End Sub

    Protected Sub txtscorename2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename2.SelectedIndexChanged
        onSubmitScore(txtscorename2, txtscore2)
    End Sub

    Protected Sub txtscorename3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename3.SelectedIndexChanged
        onSubmitScore(txtscorename3, txtscore3)
    End Sub

    Protected Sub txtscorename4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename4.SelectedIndexChanged
        onSubmitScore(txtscorename4, txtscore4)
    End Sub

    Protected Sub txtscorename5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename5.SelectedIndexChanged
        onSubmitScore(txtscorename5, txtscore5)
    End Sub

    Protected Sub cmdAddComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddComment.Click
        saveDeptReview()
    End Sub

    Sub saveDeptReview()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String


        Try
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

            updateOnlyLog("0", "Review")

            isHasRow2("ssip_manager_tab", pk)

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
                If CType(panelManagerReview.FindControl("chk_benefit" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= ", chk_benefit" & i & " = 1 "
                End If
            Next i

            sql &= " , budget_num = '" & txtbudget_num.Value & "' "
            sql &= " , benefit2_num = '" & txtbenefit_num.Value & "' "
            sql &= " , plan_detail = '" & txtplan.Value & "' "
            sql &= " , other_detail = '" & txtother.Value & "' "

            sql &= " WHERE comment_id = " & pk

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If


            sql = "DELETE FROM ssip_manager_relate_dept WHERE comment_id = " & pk
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If

            Dim pk2 As String = ""
            For i As Integer = 0 To lblDeptSelect.Items.Count - 1
                pk2 = getPK("mgr_relate_dept_id", "ssip_manager_relate_dept", conn)
                sql = "INSERT INTO ssip_manager_relate_dept (mgr_relate_dept_id , comment_id , ssip_id , dept_id , dept_name) VALUES("
                sql &= "" & pk2 & " ,"
                sql &= "" & pk & " ,"
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

            txtadd_subject.Text = ""
            txtcomment_detail.Value = ""
            bindCommentList()
            bindGridSSIPLog()

            '  cpe.Collapsed = True
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
            Return
        End Try

        Response.Redirect("form_ssip.aspx?mode=edit&id=" & id)
    End Sub
    Protected Sub txtbenefit_final_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbenefit_final.TextChanged
        calAmountText()
    End Sub

    Protected Sub cmdCalulate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalulate.Click
        calAmountText()
    End Sub

    Protected Sub cmdSaveReview2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveReview2.Click
        saveCommitteeReview()
    End Sub

    Protected Sub cmdSaveDeptReview2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveDeptReview2.Click
        saveDeptReview()
    End Sub

    Private Sub bindGridActivity()
        Dim ds As New DataSet
        Dim sql As String
        Try
            ' sql = "SELECT a.* , b.* , c.ir_status_name , d.* , g.form_name_en , a.date_report AS create_date FROM  ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id  "
            sql = "SELECT *  FROM ssip_activity_list a LEFT OUTER JOIN ssip_activity_person b ON a.record_id = b.record_id "
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 "
            sql &= " AND a.ssip_id = " & id


            'sql &= " ORDER BY a.ssip_id DESC"
            ds = conn.getDataSetForTransaction(sql, "table1")

            'Response.Write(sql)
            'Return
            GridActivity.DataSource = ds
            GridActivity.DataBind()

            lblPersonNum.Text = ds.Tables("table1").Rows.Count
            lblHourNum.Text = totalHour

            Try
                lblAverage.Text = totalHour / ds.Tables("table1").Rows.Count
            Catch ex As Exception
                lblAverage.Text = "0"
            End Try

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try

    End Sub

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("ssip_activity_detail.aspx?mode=add&ssip_id=" & id)
    End Sub

    Protected Sub GridActivity_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridActivity.RowCommand
        Dim sql As String
        Dim errorMsg As String = ""

        If (e.CommandName = "cancelCommand") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            ' Dim row As GridViewRow = GridView1.Rows(index)


            ' Add code here to add the item to the shopping cart.
            Try
                sql = "UPDATE ssip_activity_list SET is_delete = 1 WHERE record_id = " & index
                ' Response.Write(sql)
                conn.executeSQLForTransaction(sql)
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                conn.setDBCommit()
                bindGridActivity()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try

        ElseIf (e.CommandName = "cancelCommandByTQM") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            ' Dim row As GridViewRow = GridView1.Rows(index)


            ' Add code here to add the item to the shopping cart.
            Try
                sql = "UPDATE ssip_activity_list SET is_cancel = 1 WHERE record_id = " & index
                conn.executeSQLForTransaction(sql)
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                bindGridActivity()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End If
    End Sub

    Protected Sub GridActivity_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridActivity.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblHour As Label = CType(e.Row.FindControl("lblHour"), Label)
            Dim lblDateStart As Label = CType(e.Row.FindControl("lblDateStart"), Label)
            Dim lblDateEnd As Label = CType(e.Row.FindControl("lblDateEnd"), Label)

            Dim date_format1 As New Date(lblDateStart.Text)
            Dim date_format2 As New Date(lblDateEnd.Text)

            Dim diff_min As Long = DateDiff(DateInterval.Minute, date_format1, date_format2)

            lblHour.Text = diff_min / 60

            Try

                totalHour += CDbl(lblHour.Text)
            Catch ex As Exception

            End Try

        End If
    End Sub

    Protected Sub GridActivity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridActivity.SelectedIndexChanged

    End Sub

    Sub calCommitteeTang()
        Try
            txtamount_save.Text = CDbl(txtamount_old.Text) - CDbl(txtamount_new.Text)
        Catch ex As Exception
            txtamount_save.Text = ""
        End Try

    End Sub

    
    Protected Sub cmdCal11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCal11.Click
        calCommitteeTang()
    End Sub

    Protected Sub cmdAddRelateBenefit_Click(sender As Object, e As System.EventArgs) Handles cmdAddRelateBenefit.Click
        While txtBenefit_all.Items.Count > 0 AndAlso txtBenefit_all.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtBenefit_all.SelectedItem
            selectedItem.Selected = False
            txtBenefit_select.Items.Add(selectedItem)
            txtBenefit_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveRelateBenefit_Click(sender As Object, e As System.EventArgs) Handles cmdRemoveRelateBenefit.Click
        While txtBenefit_select.Items.Count > 0 AndAlso txtBenefit_select.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtBenefit_select.SelectedItem
            selectedItem.Selected = False
            txtBenefit_all.Items.Add(selectedItem)
            txtBenefit_select.Items.Remove(selectedItem)
        End While
    End Sub
End Class
