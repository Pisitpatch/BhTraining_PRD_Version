Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Net.Mail
Imports QueryStringEncryption

Partial Class idp_idp_detail
    Inherits System.Web.UI.Page

    Protected mode As String = ""
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected lang As String = "th"
    Protected session_id As String
    Protected viewtype As String = ""
    Protected id As String
    Protected flag As String
    Protected new_idp_id As String
    Protected idp_status As String = ""
    Protected dsMethod As New DataSet
    Protected login_max_authen As Integer = 0
    Dim priv_list() As String

    Protected btn_file As String = "File"
    Protected total_require_score As Integer = 0
    Protected total_elective_score As Integer = 0

    Protected is_has_remark As Boolean = True
    Protected formtype As String = "" ' adjust or maintain

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        flag = Request.QueryString("flag")
        If flag = "ladder" Then
            Me.MasterPageFile = "IDP2_MasterPage.master"
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        session_id = Session("session_myid").ToString
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        lang = Request.QueryString("lang")
        flag = Request.QueryString("flag")

        If lang = "" Then
            lang = "th"
        End If

        If lang = "th" Then
            cmdThai.Enabled = False
            btn_file = "ไฟล์แนบ"
        Else
            cmdEng.Enabled = False
            btn_file = "File"
        End If

        viewtype = Session("viewtype").ToString


        priv_list = Session("priv_list")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If


        bindDsMethod() ' Global dsMethod

        If flag = "ladder" Then
            If txtform.SelectedValue = "" Then
                '  panel_addTopic.Visible = False
            Else
                '  panel_addTopic.Visible = True
            End If
        End If

        cmdPreview.Attributes.Add("onclick", "window.open('preview_ladder.aspx?id=" & id & "');return false;")

        If IsPostBack Then

        Else ' Load first time
            tab_ladder.Visible = False
            tab_competency.Visible = False
            bindYear()

            If flag = "ladder" Then
                '  txtyear.Visible = False
                ' lblyear.Visible = False
                lblTab1.Text = "Nursing Clinical Ladder"
                cmdSubmit1.Text = "Apply Ladder"
                panel_ladder_score.Visible = True
                lblHeader.Text = "Nursing Clinical Ladder"
                lblLadderNotify.visible = True

                GridFunction.Columns(5).Visible = False ' Expect
                GridFunction.Columns(6).Visible = False ' Method

                ' GridFunction.Columns(11).ItemStyle.Width = "150px"
                GridFunction.Columns(11).Visible = False ' Start date
                GridFunction.Columns(12).Visible = False ' Finish date
                GridFunction.Columns(13).Visible = False ' Status

                If lang = "th" Then
                    GridFunction.Columns(14).HeaderText = "วันที่ทำได้ / หมายเหตุ"
                Else
                    GridFunction.Columns(14).HeaderText = "Date / Remark"
                End If

                lblRemark.Text = "วันที่ทำได้ / หมายเหตุ"
            Else
                lblLadderNotify.visible = False
                txtmonth.Visible = False
                panel_idp_expectoutcome.Visible = True
                panel_ladder_score.Visible = False
                lblFormName.Visible = False
                txtform.Visible = False
            End If



            bindStatus()

            If findArrayValue(priv_list, "17") = True Then
                txtstatus.Enabled = True
                cmdUpdateStatus.Visible = True
            Else

                If findArrayValue(priv_list, "22") = True Then 'Nursing Clinical Ladder Coordinator
                    txtstatus.Enabled = True
                    cmdUpdateStatus.Visible = True
                Else
                    txtstatus.Enabled = False
                    cmdUpdateStatus.Visible = False
                End If

            End If

            If viewtype = "" Or viewtype = "dept" Then
                'tabManager1.Visible = False
                'tabManager2.Visible = False
                ' panelManager1.Enabled = False
                cmdUpdateStatus.Visible = False
                txtstatus.Enabled = False
            End If


            bindMethodCombo()



            bindCategoryCombo()
            bindExpectCombo()

            bindLadderTemplateCombo()

            If mode = "edit" Then
                'Response.Write("xrrr=" & viewtype)
                If viewtype = "" Then
                    ' Response.Write("x=" & viewtype)
                    If flag <> "ladder" Then
                        '   Response.Write("falg=" & flag)
                        getRequireIDP()
                        getRequireIDPForCostcenter()
                        getRequireIDPForJobType()
                        getRequireIDPForJobTitle()
                        conn.setDBCommit()
                    End If
                End If

                bindIDPDetail()
                bindGridFunction("1") ' Functional
                bindGridIDPLog() ' Status Log
                bindScaleForm()
                bindGridInformationUpdate()
                bindCommonSubjectCombo()

                If flag = "ladder" Then

                    ' txtyear.Enabled = False
                    ' txtmonth.Enabled = False

                    If viewtype = "hr" Or viewtype = "educator" Then
                        cmdPreview.Visible = True
                    End If
                    Try
                        If CInt(lblEmpScoreRequire.Text) >= CInt(lblScoreRequire.Text) And is_has_remark = True Then
                            cmdSubmit1.Enabled = True

                            If CInt(lblEmpScoreElective.Text) >= CInt(lblScoreElective.Text) And is_has_remark = True Then
                                cmdSubmit1.Enabled = True
                            Else
                                cmdSubmit1.Enabled = False
                            End If

                        Else
                            cmdSubmit1.Enabled = False


                        End If


                    Catch ex As Exception
                        Response.Write(ex.Message)
                        cmdSubmit1.Enabled = False
                    End Try

                End If


                txtform.Enabled = False

                If CInt(txtstatusid.Value) > 1 Then ' Submitted , Apply
                    'panelFunction.Enabled = False


                    ' readonlyControl(panelFunction)

                    ' readonlyGrid(GridFunction)
                    cmdSaveDraft1.Visible = False
                    cmdSubmit1.Visible = False
                    ' cmdAddTopic.Visible = False
                    'cmdDelete.Visible = False
                    txtyear.Enabled = False
                    txtmonth.Enabled = False

                    If flag = "ladder" Then
                        cmdSaveOrder.Visible = False
                        cmdAddTopic.Visible = False
                        cmdDelete.Visible = False
                        panel_addTopic.Visible = False


                    Else
                        cmdAddTopic.Visible = True
                        cmdDelete.Visible = True
                    End If


                    If viewtype = "" And flag <> "ladder" Then
                        ' cmdSaveOrder.Attributes.Remove("disabled")
                        cmdSaveOrder.Visible = True
                    End If

                Else ' Draft
                    If viewtype = "educator" Or viewtype = "dept" Then
                        ' cmdSaveDraft1.Enabled = False
                        cmdUpdateStatus.Visible = False
                        txtstatus.Enabled = False

                        cmdSaveDraft1.Visible = False
                        txtform.Visible = False
                        lblFormName.Visible = False
                        cmdSubmit1.Visible = False
                    ElseIf viewtype = "hr" Then
                        txtyear.Enabled = False
                        txtmonth.Enabled = False
                        cmdSaveDraft1.Visible = False
                        txtform.Visible = False
                        lblFormName.Visible = False
                        cmdSubmit1.Visible = False
                    End If
                End If
                '  Response.Write(viewtype)
                If viewtype = "hr" Then
                    txtyear.Enabled = True
                    txtmonth.Enabled = True
                End If

                bindEducatorList()
                bindCommentList()

                txtjobtitle.Value = addslashes(("job_title").ToString)
                lblJobType.Text = Session("user_position").ToString
                txtname.Value = Session("user_fullname").ToString
                txtdeptname.Value = Session("dept_name").ToString
                txtdatetime.Value = Date.Now

                Dim IDPApproveLevel As String = geIDPApproveLevel()

                'Response.Write(IDPApproveLevel)
                'If viewtype <> "" And (findArrayValue(priv_list, "13") = True Or findArrayValue(priv_list, "14") = True Or findArrayValue(priv_list, "15") = True Or findArrayValue(priv_list, "16") = True) Then

                '    If IDPApproveLevel = "12" And (findArrayValue(priv_list, "13") = True Or findArrayValue(priv_list, "14") = True Or findArrayValue(priv_list, "15") = True Or findArrayValue(priv_list, "16") = True) Then
                '        panelAddComment.Visible = True
                '    ElseIf IDPApproveLevel = "13" And (findArrayValue(priv_list, "14") = True Or findArrayValue(priv_list, "15") = True Or findArrayValue(priv_list, "16") = True) Then
                '        panelAddComment.Visible = True
                '    ElseIf IDPApproveLevel = "14" And (findArrayValue(priv_list, "15") = True Or findArrayValue(priv_list, "16") = True) Then
                '        panelAddComment.Visible = True
                '    ElseIf IDPApproveLevel = "15" And (findArrayValue(priv_list, "16") = True) Then
                '        panelAddComment.Visible = True
                '    Else
                '        panelAddComment.Visible = False
                '    End If

                'Else
                '    panelAddComment.Visible = False
                'End If
                If flag = "ladder" And txtstatus.SelectedValue > 1 Then
                    tab_ladder.Visible = True
                    tab_competency.Visible = True
                End If

                If viewtype = "hr" Then
                    cmdDelete.Visible = False
                    panelAddComment.Visible = False
                    panel_addTopic.Visible = False

                    email_alert.Visible = True
                    tabMailLog.Visible = True
                End If

                If viewtype = "dept" Or viewtype = "educator" Then
                    cmdDelete.Visible = False
                    panel_addTopic.Visible = False
                    panel_scale.Enabled = True

                End If

                If viewtype = "educator" Then
                    panel_educator.Enabled = True
                Else
                    panel_educator.Enabled = False
                End If

                login_max_authen = CInt(getMyIPDLevel())
                If login_max_authen > CInt(IDPApproveLevel) And viewtype = "dept" Then
                    lblMsg.Text = "Add/Edit Approval and Comment"
                    lblMsg.ForeColor = Drawing.Color.Green
                    panelAddComment.Visible = True
                Else
                    lblMsg.Text = "View Only"
                    lblMsg.ForeColor = Drawing.Color.Red
                    panelAddComment.Visible = False
                End If

                If flag = "ladder" Then ' ตรวจสอบวันที่สามารถ apply ได้
                    checkExpireDate()
                End If

            ElseIf mode = "add" Then
                lblempcode.Text = Session("emp_code").ToString
                lblDept.Text = Session("dept_name").ToString
                lblDivision.Text = addslashes(Session("job_title").ToString)
                lblIDP_NO.Text = ""
                lblname.Text = Session("user_fullname").ToString
                lbljobtitle.Text = Session("user_position").ToString
                lblCostcenter.Text = Session("costcenter_id").ToString

                txtyear.SelectedValue = Date.Now.Year
                ' If True = isNotHasRequireIDP() Then
                Try
                    tab_approve.Visible = False
                    clearGridFunction()
                    If flag <> "ladder" Then

                        checkDupplicateIDP()

                        getRequireIDP()

                        getRequireIDPForCostcenter()

                        getRequireIDPForJobType()

                        getRequireIDPForJobTitle()

                        conn.setDBCommit()
                    Else
                        ' checkDupplicateLadderForm()
                        ' bindYearLadder()
                    End If



                Catch ex As Exception
                    conn.setDBRollback()
                    Response.Write(ex.Message)
                End Try

                bindGridFunction("1")
                'End If

            End If

            If flag <> "ladder" Then
                GridFunction.Columns(7).Visible = False
                GridFunction.Columns(8).Visible = False
                GridFunction.Columns(9).Visible = False
                GridFunction.Columns(10).Visible = False
            End If


        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing
            dsMethod.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindYear()
        Dim iYear = 0

        If mode <> "add" Then
            iYear = 2
        End If

        For i As Integer = Date.Now.Year - iYear To Date.Now.Year + 2
            txtyear.Items.Add(i)
        Next i
    End Sub

    Sub checkDupplicateLadderForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString
            sql &= " AND is_ladder = 1 AND ISNULL(is_delete,0) = 0 AND ISNULL(is_cancel ,0) = 0 "
            sql &= " AND plan_month =  " & txtmonth.SelectedValue
            sql &= " AND plan_year = " & txtyear.SelectedValue
            If txtform.SelectedIndex = 0 Then
                cmdSaveDraft1.Enabled = False
                cmdSubmit1.Enabled = False
            Else
                cmdSaveDraft1.Enabled = True
                sql &= " AND ladder_template_id = " & txtform.SelectedValue
            End If

            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                cmdSaveDraft1.Enabled = False
            End If
        Catch ex As Exception
            cmdSaveDraft1.Enabled = False
            cmdSubmit1.Enabled = False
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub checkDupplicateIDP()
        Dim sql As String
        Dim ds As New DataSet
        Dim status_table As String = "idp_status_list"
        Dim year_array As String()
        Try
            'sql = "SELECT * FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString
            'sql &= " AND plan_year = "

            If flag = "ladder" Then
                status_table = "idp_status_ladder"
            End If
            sql = "SELECT plan_year FROM idp_trans_list a INNER JOIN " & status_table & " b"
            sql &= " ON a.status_id = b.idp_status_id "
            sql &= " INNER JOIN user_profile c ON a.report_emp_code = c.emp_code "

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "
            sql &= " AND a.idp_id NOT IN (SELECT idp_id FROM idp_external_req)"
            sql &= " AND ISNULL(a.is_ladder,0) = 0 AND a.status_id > 1 "
            sql &= " AND a.report_emp_code = " & Session("emp_code").ToString
            sql &= " GROUP BY plan_year ORDER BY plan_year"
            'sql &= " AND a.plan_year = " & Date.Now.Year
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")


            txtyear.Items.Clear()

            ReDim year_array(ds.Tables("t1").Rows.Count)

            For s As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                year_array(s) = CInt(ds.Tables("t1").Rows(s)(0).ToString)


            Next s

            Dim irow As Integer = 0
            For i As Integer = Date.Now.Year To Date.Now.Year + 3

                If Not findArrayValue(year_array, i) Then
                    txtyear.Items.Add(i)
                End If

                irow += 1
            Next i

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindYearLadder()
        Dim sql As String
        Dim ds As New DataSet

        Try
            txtyear.Items.Clear()

            sql = "SELECT plan_year , plan_month  FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString
            sql &= " AND is_ladder = 1 "
            sql &= " AND plan_month IN (4,10) "
            sql &= " WHERE 1 = 1 "
            sql &= " GROUP BY plan_year , plan_month  "
            '  Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count = 0 Then
                For i As Integer = Date.Now.Year - 2 To Date.Now.Year + 2
                    txtyear.Items.Add(i)
                Next i
            Else
                For i As Integer = Date.Now.Year - 2 To Date.Now.Year + 2
                    For s As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                        If ds.Tables("t1").Rows(s)("plan_year").ToString <> i.ToString Then
                            txtyear.Items.Add(i)
                        End If
                    Next s


                Next i

                txtmonth.Items.Clear()
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub clearGridFunction()
        Dim sql As String
        Dim errorMsg As String


        sql = "DELETE FROM idp_function_tab WHERE session_id = '" & session_id & "' "
        '  Response.Write(sql)
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If


    End Sub

    Function geIDPApproveLevel() As String ' Level ของพนักงานที่ทำ IDP
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = "0"
        Try
            sql = "SELECT * FROM user_role a INNER JOIN m_role b ON a.role_id = b.role_id WHERE a.role_id IN (12,13,14,15,16) AND a.emp_code IN (SELECT report_emp_code FROM idp_trans_list WHERE idp_id = " & id & ")"
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
            result = "0"
        End Try
        Return result
    End Function

    Function getMyIPDLevel() As String ' Level ของผู้ login
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = "0"
        Try
            sql = "SELECT * FROM user_role a INNER JOIN m_role b ON a.role_id = b.role_id WHERE a.role_id IN (12,13,14,15,16,17) AND a.emp_code = " & Session("emp_code").ToString
            sql &= " ORDER BY a.role_id DESC"
            ' Response.Write(sql)
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

    Function isNotHasRequireIDP() As Boolean
        Dim sql As String
        Dim ds As New DataSet

        Dim result As Boolean = False
        Try
            sql = "SELECT * FROM idp_function_tab WHERE session_id = '" & session_id & "' AND is_required = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables(0).Rows.Count > 0 Then
                result = False
            Else
                result = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try

        Return result
    End Function

    Sub getRequireIDP()
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Dim pk As String

        sql = "DELETE FROM idp_function_tab WHERE session_id = '" & Session.SessionID & "' "
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If

        sql = "SELECT b.template_topic_id , b.template_category_id  , b.template_expect_id , b.template_expect_detail "
        sql &= " , MAX( b.template_order_sort) AS template_order_sort , MAX(b.template_is_require) AS template_is_require "
        sql &= " ,  b.template_category_name , b.template_topic_name "
        sql &= " , MAX(b.template_start_date_ts) AS template_start_date_ts ,  MAX(b.template_start_date) AS template_start_date"
        sql &= " , MAX(b.template_complete_date_ts) AS template_complete_date_ts ,  MAX(b.template_complete_date) AS template_complete_date"
        sql &= " , a.template_title , b.template_methodogy_id , b.template_methodogy_name "
        sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
        sql &= " INNER JOIN idp_template_employee c ON b.template_id  = c.template_id "
        sql &= " WHERE a.is_active = 1 AND b.template_topic_id > 0 AND ISNULL(a.is_delete,0) = 0 AND b.template_is_require = 1  "
        sql &= " AND c.emp_code = '" & Session("emp_code").ToString & "' "

        sql &= " AND b.template_topic_id NOT IN (SELECT topic_id FROM idp_function_tab WHERE 1 = 1 "
        If mode = "add" Then
            sql &= " AND session_id = '" & Session.SessionID & "' "
        Else
            sql &= " AND idp_id = " & id
        End If

        sql &= ")"
        sql &= " GROUP BY b.template_topic_id , b.template_category_id ,  b.template_expect_id , b.template_expect_detail , b.template_category_name , b.template_topic_name  , a.template_title , b.template_methodogy_id , b.template_methodogy_name "
        sql &= " ORDER BY MAX(b.template_is_require) DESC , MAX(b.template_order_sort) ASC"

        ' Response.Write(sql)
        ' 'sql = "SELECT * FROM idp_m_require WHERE is_active = 1"
        ds = conn.getDataSetForTransaction(sql, "t1")

        'Response.Write(sql & "<br/>")
        ' Response.Write(ds.Tables("t1").Rows.Count & "<hr/>")
        For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
            pk = getPK("function_id", "idp_function_tab", conn)
            sql = "INSERT INTO idp_function_tab (function_id , session_id , template_title , ipd_type_id , is_required , category_id , topic_id "
            sql &= ", category_name , topic_name , expect_id , expect_detail , method_id , methodology , date_start , date_complete "
            sql &= " , date_start_ts , date_complete_ts , topic_status , order_sort"
            sql &= ") VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & session_id & "' ,"
            sql &= "'" & addslashes(ds.Tables(0).Rows(i)("template_title").ToString) & "' ,"
            sql &= "'" & 1 & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_is_require").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_category_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_topic_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_category_name").ToString & "' ,"
            sql &= "'" & addslashes(ds.Tables(0).Rows(i)("template_topic_name").ToString) & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_expect_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_expect_detail").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_id").ToString & "' ," ' methodogy
            sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_name").ToString & "' ," ' methodogy

            If ds.Tables(0).Rows(i)("template_start_date_ts").ToString = "0" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_start_date_ts").ToString) & "' ,"
            End If

            If ds.Tables(0).Rows(i)("template_complete_date_ts").ToString = "0" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_complete_date_ts").ToString) & "' ,"
            End If

            sql &= "'" & ds.Tables(0).Rows(i)("template_start_date_ts").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_complete_date_ts").ToString & "' ,"

            sql &= " null ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_order_sort").ToString & "' "
            sql &= ")"

            'Response.Write(sql & "<br/>")
            ' Response.End()
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i

        If mode = "edit" Then
            sql = "UPDATE idp_function_tab SET is_new_idp = 1 , session_id = null , idp_id = " & id & " WHERE session_id = '" & Session.SessionID & "'"
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        End If

    End Sub

    Sub getRequireIDPForCostcenter()
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Dim pk As String

        sql = "SELECT b.template_topic_id , b.template_category_id ,  b.template_expect_id , b.template_expect_detail "
        sql &= " , MAX( b.template_order_sort) AS template_order_sort , MAX(b.template_is_require) AS template_is_require "
        sql &= " ,  b.template_category_name , b.template_topic_name "
        sql &= " , MAX(b.template_start_date_ts) AS template_start_date_ts ,  MAX(b.template_start_date) AS template_start_date"
        sql &= " , MAX(b.template_complete_date_ts) AS template_complete_date_ts ,  MAX(b.template_complete_date) AS template_complete_date"
        sql &= " , a.template_title , b.template_methodogy_id , b.template_methodogy_name "
        sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
        sql &= " INNER JOIN idp_template_costcenter c ON b.template_id  = c.template_id "
        sql &= " WHERE a.is_active = 1 and b.template_topic_id > 0  AND ISNULL(a.is_delete,0) = 0 AND b.template_is_require = 1  "
        sql &= " AND c.costcenter_id = '" & Session("dept_id").ToString & "' "

        sql &= " AND b.template_topic_id NOT IN (SELECT topic_id FROM idp_function_tab WHERE 1 = 1 "
        If mode = "add" Then
            sql &= " AND session_id = '" & Session.SessionID & "' "
        Else
            sql &= " AND idp_id = " & id
        End If

        sql &= ")"
        sql &= " GROUP BY b.template_topic_id , b.template_category_id ,  b.template_expect_id , b.template_expect_detail , b.template_category_name , b.template_topic_name , a.template_title , b.template_methodogy_id , b.template_methodogy_name "
        sql &= " ORDER BY MAX(b.template_is_require) DESC , MAX(b.template_order_sort) ASC"
        'Response.Write(sql)
        'sql = "SELECT * FROM idp_m_require WHERE is_active = 1"
        ds = conn.getDataSetForTransaction(sql, "t1")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            pk = getPK("function_id", "idp_function_tab", conn)
            sql = "INSERT INTO idp_function_tab (function_id , session_id  , template_title , ipd_type_id , is_required , category_id , topic_id "
            sql &= ", category_name , topic_name , expect_id , expect_detail , method_id , methodology , date_start , date_complete "
            sql &= " , date_start_ts , date_complete_ts , topic_status , order_sort"
            sql &= ") VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & session_id & "' ,"
            sql &= "'" & addslashes(ds.Tables(0).Rows(i)("template_title").ToString) & "' ,"
            sql &= "'" & 1 & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_is_require").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_category_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_topic_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_category_name").ToString & "' ,"
            sql &= "'" & addslashes(ds.Tables(0).Rows(i)("template_topic_name").ToString) & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_expect_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_expect_detail").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_id").ToString & "' ," ' methodogy
            sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_name").ToString & "' ," ' methodogy

            If ds.Tables(0).Rows(i)("template_start_date_ts").ToString = "0" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_start_date_ts").ToString) & "' ,"
            End If

            If ds.Tables(0).Rows(i)("template_complete_date_ts").ToString = "0" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_complete_date_ts").ToString) & "' ,"
            End If

            sql &= "'" & ds.Tables(0).Rows(i)("template_start_date_ts").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_complete_date_ts").ToString & "' ,"
            sql &= " null ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_order_sort").ToString & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i

        If mode = "edit" Then
            sql = "UPDATE idp_function_tab SET is_new_idp = 1 , session_id = null , idp_id = " & id & " WHERE session_id = '" & Session.SessionID & "'"
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        End If
    End Sub

    Sub getRequireIDPForJobType()
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Dim pk As String

        sql = "SELECT b.template_topic_id , b.template_category_id  ,b.template_expect_id, b.template_expect_detail "
        sql &= " , MAX( b.template_order_sort) AS template_order_sort , MAX(b.template_is_require) AS template_is_require "
        sql &= " ,  b.template_category_name , b.template_topic_name "
        sql &= " , MAX(b.template_start_date_ts) AS template_start_date_ts ,  MAX(b.template_start_date) AS template_start_date"
        sql &= " , MAX(b.template_complete_date_ts) AS template_complete_date_ts ,  MAX(b.template_complete_date) AS template_complete_date"
        sql &= " , a.template_title , b.template_methodogy_id , b.template_methodogy_name "
        sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
        sql &= " INNER JOIN idp_template_jobtype c ON b.template_id  = c.template_id "
        sql &= " WHERE a.is_active = 1 and b.template_topic_id > 0  AND ISNULL(a.is_delete,0) = 0 AND b.template_is_require = 1  "

        sql &= " AND c.job_type_name_en = '" & addslashes(Session("user_position").ToString) & "' "
        sql &= " AND b.template_topic_id NOT IN (SELECT topic_id FROM idp_function_tab WHERE 1 = 1 "
        If mode = "add" Then
            sql &= " AND session_id = '" & Session.SessionID & "' "
        Else
            sql &= " AND idp_id = " & id
        End If

        sql &= ")"
        sql &= " GROUP BY b.template_topic_id , b.template_category_id ,  b.template_expect_id , b.template_expect_detail , b.template_category_name , b.template_topic_name , a.template_title , b.template_methodogy_id , b.template_methodogy_name "
        sql &= " ORDER BY MAX(b.template_is_require) DESC , MAX(b.template_order_sort) ASC"
        'Response.Write(sql)
        'sql = "SELECT * FROM idp_m_require WHERE is_active = 1"
        ds = conn.getDataSetForTransaction(sql, "t1")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            pk = getPK("function_id", "idp_function_tab", conn)
            sql = "INSERT INTO idp_function_tab (function_id , session_id , template_title , ipd_type_id , is_required , category_id , topic_id "
            sql &= ", category_name , topic_name , expect_detail , method_id  , methodology , date_start , date_complete "
            sql &= " , date_start_ts , date_complete_ts , topic_status , order_sort"
            sql &= ") VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & session_id & "' ,"
            sql &= "'" & addslashes(ds.Tables(0).Rows(i)("template_title").ToString) & "' ,"
            sql &= "'" & 1 & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_is_require").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_category_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_topic_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_category_name").ToString & "' ,"
            sql &= "'" & addslashes(ds.Tables(0).Rows(i)("template_topic_name").ToString) & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_expect_detail").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_id").ToString & "' ," ' methodogy
            sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_name").ToString & "' ," ' methodogy

            If ds.Tables(0).Rows(i)("template_start_date_ts").ToString = "0" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_start_date_ts").ToString) & "' ,"
            End If

            If ds.Tables(0).Rows(i)("template_complete_date_ts").ToString = "0" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_complete_date_ts").ToString) & "' ,"
            End If

            sql &= "'" & ds.Tables(0).Rows(i)("template_start_date_ts").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_complete_date_ts").ToString & "' ,"
            sql &= " null ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_order_sort").ToString & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i

        If mode = "edit" Then
            sql = "UPDATE idp_function_tab SET is_new_idp = 1 , session_id = null , idp_id = " & id & " WHERE session_id = '" & Session.SessionID & "'"
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        End If
    End Sub

    Sub getRequireIDPForJobTitle()
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Dim pk As String

        sql = "SELECT b.template_topic_id , b.template_category_id ,  b.template_expect_id , b.template_expect_detail "
        sql &= " , MAX( b.template_order_sort) AS template_order_sort , MAX(b.template_is_require) AS template_is_require "
        sql &= " ,  b.template_category_name , b.template_topic_name "
        sql &= " , MAX(b.template_start_date_ts) AS template_start_date_ts ,  MAX(b.template_start_date) AS template_start_date"
        sql &= " , MAX(b.template_complete_date_ts) AS template_complete_date_ts ,  MAX(b.template_complete_date) AS template_complete_date"
        sql &= " , a.template_title , b.template_methodogy_id , b.template_methodogy_name "
        sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
        sql &= " INNER JOIN idp_template_jobtitle c ON b.template_id  = c.template_id "
        sql &= " WHERE a.is_active = 1 and b.template_topic_id > 0 AND ISNULL(a.is_delete,0) = 0 AND b.template_is_require = 1  "
        sql &= " AND c.job_title_en = '" & addslashes(Session("job_title").ToString) & "' "

        sql &= " AND b.template_topic_id NOT IN (SELECT topic_id FROM idp_function_tab WHERE 1 = 1 "
        If mode = "add" Then
            sql &= " AND session_id = '" & Session.SessionID & "' "
        Else
            sql &= " AND idp_id = " & id
        End If

        sql &= ")"
        sql &= " GROUP BY b.template_topic_id , b.template_category_id ,  b.template_expect_id , b.template_expect_detail , b.template_category_name , b.template_topic_name , a.template_title  , b.template_methodogy_id , b.template_methodogy_name "
        sql &= " ORDER BY MAX(b.template_is_require) DESC , MAX(b.template_order_sort) ASC"
        'Response.Write(sql)
        'sql = "SELECT * FROM idp_m_require WHERE is_active = 1"
        ds = conn.getDataSetForTransaction(sql, "t1")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            pk = getPK("function_id", "idp_function_tab", conn)
            sql = "INSERT INTO idp_function_tab (function_id , session_id , template_title , ipd_type_id , is_required , category_id , topic_id "
            sql &= ", category_name , topic_name , expect_detail , method_id , methodology , date_start , date_complete "
            sql &= " , date_start_ts , date_complete_ts , topic_status , order_sort"
            sql &= ") VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & session_id & "' ,"
            sql &= "'" & addslashes(ds.Tables(0).Rows(i)("template_title").ToString) & "' ,"
            sql &= "'" & 1 & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_is_require").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_category_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_topic_id").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_category_name").ToString & "' ,"
            sql &= "'" & addslashes(ds.Tables(0).Rows(i)("template_topic_name").ToString) & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_expect_detail").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_id").ToString & "' ," ' methodogy
            sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_name").ToString & "' ," ' methodogy

            If ds.Tables(0).Rows(i)("template_start_date_ts").ToString = "0" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_start_date_ts").ToString) & "' ,"
            End If

            If ds.Tables(0).Rows(i)("template_complete_date_ts").ToString = "0" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_complete_date_ts").ToString) & "' ,"
            End If

            sql &= "'" & ds.Tables(0).Rows(i)("template_start_date_ts").ToString & "' ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_complete_date_ts").ToString & "' ,"
            sql &= " null ,"
            sql &= "'" & ds.Tables(0).Rows(i)("template_order_sort").ToString & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i

        If mode = "edit" Then
            sql = "UPDATE idp_function_tab SET is_new_idp = 1 , session_id = null , idp_id = " & id & " WHERE session_id = '" & Session.SessionID & "'"
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        End If
    End Sub

    Sub bindStatus()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            If flag = "ladder" Then
                sql = "SELECT * FROM idp_status_ladder"
            Else
                sql = "SELECT * FROM idp_status_list"
            End If
            'sql &= " ORDER BY dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            txtstatus.DataSource = ds
            txtstatus.DataBind()

            txtstatus.Items.Insert(0, New ListItem("--", ""))

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindIDPDetail()
        Dim sql As String
        Dim ds As New DataSet
        Dim status_table As String = "idp_status_list"
        Dim h_date As String()

        Try
            If flag = "ladder" Then
                status_table = "idp_status_ladder"
            End If
            sql = "SELECT * , b.status_name AS idp_status_name FROM idp_trans_list a INNER JOIN " & status_table & " b ON a.status_id = b.idp_status_id "
            sql &= " INNER JOIN user_profile c ON a.report_emp_code = c.emp_code "
            sql &= " WHERE a.idp_id = " & id
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            lblStatus.Text = "[" & ds.Tables(0).Rows(0)("idp_status_name").ToString & "]"
            txtstatusid.Value = ds.Tables(0).Rows(0)("status_id").ToString
            txtstatus.SelectedValue = ds.Tables(0).Rows(0)("status_id").ToString

            lblIDP_NO.Text = ds.Tables(0).Rows(0)("idp_no").ToString

            If ds.Tables(0).Rows(0)("hire_date").ToString <> "" Then
                h_date = ds.Tables(0).Rows(0)("hire_date").ToString.Split(" ")
                lblHireDate.Text = h_date(0)
            End If

            lblempcode.Text = ds.Tables(0).Rows(0)("report_emp_code").ToString
            lblDept.Text = ds.Tables(0).Rows(0)("report_dept_name").ToString
            lblDivision.Text = ds.Tables(0).Rows(0)("report_jobtype").ToString ' replace by job_type

            lblname.Text = ds.Tables(0).Rows(0)("report_by").ToString
            lbljobtitle.Text = ds.Tables(0).Rows(0)("report_jobtitle").ToString
            lblCostcenter.Text = ds.Tables(0).Rows(0)("report_dept_id").ToString

            txtyear.SelectedValue = ds.Tables(0).Rows(0)("plan_year").ToString

            If flag = "ladder" Then

                Try
                    txtmonth.SelectedValue = ds.Tables(0).Rows(0)("plan_month").ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try


                lblScoreName.Text = ds.Tables(0).Rows(0)("ladder_template_name").ToString
                txtform.SelectedValue = ds.Tables(0).Rows(0)("ladder_template_id").ToString
                Try
                    txteducator_status.SelectedValue = ds.Tables(0).Rows(0)("educator_status_id").ToString
                Catch ex As Exception

                End Try

                ' txteducator_subject.SelectedValue = ds.Tables(0).Rows(0)("educator_subject_id").ToString
                ' txteducator_detail.Value = ds.Tables(0).Rows(0)("educator_comment_detail").ToString

                lblScoreRequire.Text = ds.Tables(0).Rows(0)("score_template_require").ToString
                lblScoreElective.Text = ds.Tables(0).Rows(0)("score_template_elective").ToString


            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub checkExpireDate()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_template_master WHERE template_id = " & txtform.SelectedValue
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows(0)("ladder_apply_date_ts").ToString() <> "" And ds.Tables("t1").Rows(0)("ladder_apply_date_ts").ToString() <> "0" Then
                '  Response.Write(Date.Now.Ticks & "<Br/>")
                '  Response.Write(CLng(ds.Tables("t1").Rows(0)("ladder_apply_date_ts").ToString()) & "<Br/>")
                If CLng(ds.Tables("t1").Rows(0)("ladder_apply_date_ts").ToString()) < Date.Now.Ticks Then
                    cmdSubmit1.Enabled = False
                    ' Response.Write("yes")
                Else
                    ' Response.Write("nu")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridFunction(ByVal idp_type As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim flag As String
        Try
            If lang = "th" Then
                flag = "_th"
            Else
                flag = ""
            End If
            sql = "SELECT * , b.method_name" & flag & " AS methodLang , d.expect_detail as expect_th , d.expect_detail_en as expect_en   "
            sql &= " , a.expect_detail AS expect_custom "
            sql &= " FROM idp_function_tab a "
            sql &= " LEFT OUTER JOIN idp_m_method b ON a.method_id = b.method_id "
            sql &= " LEFT OUTER JOIN idp_m_topic c ON a.topic_id = c.topic_id  "
            sql &= " LEFT OUTER JOIN idp_m_expect d ON a.expect_id = d.expect_id AND ISNULL(d.is_expect_delete,0) = 0 "
            sql &= " INNER  JOIN idp_m_category e ON a.category_id = e.category_id "
            sql &= "  WHERE  ISNULL(c.is_delete,0) = 0 AND ipd_type_id = " & idp_type
            If mode = "add" Then
                sql &= " AND a.session_id = '" & session_id & "' "
            ElseIf mode = "edit" Then
                sql &= " AND a.idp_id = " & id
            End If
            sql &= " ORDER BY is_required DESC , order_sort ASC"
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            If lang = "th" Then
                GridFunction.Columns(1).HeaderText = "ลำดับ"
                GridFunction.Columns(3).HeaderText = "หมวดหมู่"
                GridFunction.Columns(4).HeaderText = "หัวข้อ"
                GridFunction.Columns(5).HeaderText = "สิ่งที่จะได้รับ"
                GridFunction.Columns(6).HeaderText = "วิธีการ"
                GridFunction.Columns(11).HeaderText = "เริ่มต้น"
                GridFunction.Columns(12).HeaderText = "สิ้นสุด"
                GridFunction.Columns(13).HeaderText = "สถานะ"
                If flag = "ladder" Then
                    If lang = "th" Then
                        GridFunction.Columns(14).HeaderText = "วันที่ทำได้ / หมายเหตุ"
                    Else
                        GridFunction.Columns(14).HeaderText = "Date / Remark"
                    End If

                Else
                    GridFunction.Columns(14).HeaderText = "หมายเหตุ"
                End If

            Else
                'GridFunction.Columns(3).HeaderText = "Categories"
            End If
            ' Response.Write("xxx")
            GridFunction.DataSource = ds
            GridFunction.DataBind()



            If ds.Tables("t1").Rows.Count <= 0 Then
                'cmdSaveDraft1.Enabled = False
                'cmdSubmit1.Enabled = False
                'cmdDelete.Enabled = False
            Else
                cmdSaveDraft1.Enabled = True
                cmdSubmit1.Enabled = True
                cmdDelete.Enabled = True
            End If

            If mode = "add" Then
                cmdSubmit1.Enabled = False
            Else
                '  cmdSubmit1.Enabled = True
            End If

            'If flag = "ladder" Then
            '    If is_has_remark = False Then ' ถ้าไม่ใส่ remark
            '        cmdSubmit1.Enabled = False
            '    End If
            'End If

        Catch ex As Exception
            Response.Write("bindGridFunction : " & ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindMethodCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_method ORDER BY method_name"
            If lang = "th" Then
                sql &= "_th"
            End If
            ds = conn.getDataSetForTransaction(sql, "t1")

            If lang = "th" Then
                txtadd_method1.DataTextField = "method_name_th"
                txtadd_method1.Items.Insert(0, New ListItem("--กรุณาเลือก--", "0"))
            Else
                txtadd_method1.DataTextField = "method_name"
                txtadd_method1.Items.Insert(0, New ListItem("--Please Select--", "0"))
            End If
            txtadd_method1.DataSource = ds
            txtadd_method1.DataBind()

            If lang = "th" Then
                txtadd_method1.Items.Insert(0, New ListItem("--กรุณาเลือก--", "0"))
            Else
                txtadd_method1.Items.Insert(0, New ListItem("--Please Select--", "0"))
            End If




        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            '  ds.Dispose()
        End Try
    End Sub

    Sub bindDsMethod()
        Dim sql As String
        Try
            Sql = "SELECT * FROM idp_m_method"
            dsMethod = conn.getDataSetForTransaction(sql, "t1")

        Catch ex As Exception
            Response.Write(ex.Message & Sql)
        Finally
            '  ds.Dispose()
        End Try
    End Sub

    Sub bindLadderTemplateCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_template_master WHERE is_ladder_template = 1 AND is_active = 1 AND ISNULL(is_delete,0) = 0 "

            ds = conn.getDataSetForTransaction(sql, "t1")

            txtform.DataSource = ds
            txtform.DataBind()


            txtform.Items.Insert(0, New ListItem("--Please Select--", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCommonSubjectCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_common_subject WHERE 1 = 1 "
            If flag = "ladder" Then
                sql &= " AND subject_type = 'ladder' "
            Else
                '  sql &= " AND category_type = 'General' "
            End If
            ds = conn.getDataSetForTransaction(sql, "t1")
            'txteducator_subject.DataTextField = "category_name_" & lang
            txteducator_subject.DataSource = ds
            txteducator_subject.DataBind()
            txteducator_subject.Items.Insert(0, New ListItem("-", "0"))

            txtcomment.DataSource = ds
            txtcomment.DataBind()
            txtcomment.Items.Insert(0, New ListItem("-", "0"))

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCategoryCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_category WHERE ISNULL(is_delete , 0 ) = 0 "
            If flag = "ladder" Then
                sql &= " AND category_type = 'Nursing' "
            Else
                sql &= " AND category_type = 'General' "
            End If
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtadd_cat1.DataTextField = "category_name_" & lang
            txtadd_cat1.DataSource = ds
            txtadd_cat1.DataBind()

            ' txtadd_cat1.Items.Insert(0, New ListItem("--Please Select--", "0"))
            If lang = "th" Then
                txtadd_cat1.Items.Insert(0, New ListItem("--กรุณาเลือก--", "0"))
            Else
                txtadd_cat1.Items.Insert(0, New ListItem("--Please Select--", "0"))
            End If


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
            If flag <> "ladder" Then
                sql = "SELECT b.template_category_id , b.template_topic_id AS topic_id , e.master_topic_name_th AS  topic_name_th , e.master_topic_name_en AS  topic_name_en "
                sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
                sql &= " INNER JOIN idp_template_employee c ON b.template_id  = c.template_id "

                sql &= " INNER JOIN idp_m_topic d ON b.template_topic_id = d.topic_id "
                sql &= " INNER jOIN idp_m_master_topic e ON d.master_topic_id = e.master_topic_id AND ISNULL(e.is_delete,0) = 0 AND e.is_active = 1 "

                sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND ISNULL(b.template_is_require,0) = 0  "
                sql &= " AND c.emp_code = '" & Session("emp_code").ToString & "' AND b.template_category_id = " & txtadd_cat1.SelectedValue

                sql &= " UNION "
                sql &= "SELECT b.template_category_id ,b.template_topic_id AS topic_id,  e.master_topic_name_th AS  topic_name_th , e.master_topic_name_en AS  topic_name_en  "
                sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
                sql &= " INNER JOIN idp_template_costcenter c ON b.template_id  = c.template_id "

                sql &= " INNER JOIN idp_m_topic d ON b.template_topic_id = d.topic_id "
                sql &= " INNER jOIN idp_m_master_topic e ON d.master_topic_id = e.master_topic_id AND ISNULL(e.is_delete,0) = 0 AND e.is_active = 1 "

                sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND  ISNULL(b.template_is_require,0) = 0  "
                sql &= " AND c.costcenter_id = '" & Session("dept_id").ToString & "' AND b.template_category_id = " & txtadd_cat1.SelectedValue

                sql &= " UNION "
                sql &= "SELECT b.template_category_id , b.template_topic_id AS topic_id, e.master_topic_name_th AS  topic_name_th , e.master_topic_name_en AS  topic_name_en  "
                sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
                sql &= " INNER JOIN idp_template_jobtitle c ON b.template_id  = c.template_id "

                sql &= " INNER JOIN idp_m_topic d ON b.template_topic_id = d.topic_id "
                sql &= " INNER jOIN idp_m_master_topic e ON d.master_topic_id = e.master_topic_id AND ISNULL(e.is_delete,0) = 0 AND e.is_active = 1 "

                sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND  ISNULL(b.template_is_require,0) = 0  "
                sql &= " AND c.job_title_en = '" & addslashes(Session("job_title").ToString) & "' AND b.template_category_id = " & txtadd_cat1.SelectedValue

                sql &= " UNION "
                sql &= "SELECT b.template_category_id , b.template_topic_id AS topic_id,  e.master_topic_name_th AS  topic_name_th , e.master_topic_name_en AS  topic_name_en  "
                sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
                sql &= " INNER JOIN idp_template_jobtype c ON b.template_id  = c.template_id "

                sql &= " INNER JOIN idp_m_topic d ON b.template_topic_id = d.topic_id "
                sql &= " INNER jOIN idp_m_master_topic e ON d.master_topic_id = e.master_topic_id AND ISNULL(e.is_delete,0) = 0 AND e.is_active = 1 "

                sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND  ISNULL(b.template_is_require,0) = 0 "
                sql &= " AND c.job_type_name_en = '" & addslashes(Session("user_position").ToString) & "' AND b.template_category_id = " & txtadd_cat1.SelectedValue
            Else ' Ladder
                sql = "SELECT b.template_category_id , b.template_topic_id AS topic_id, d.master_topic_name_th AS  topic_name_th , d.master_topic_name_en AS  topic_name_en  "
                sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
                sql &= " INNER JOIN idp_m_topic c ON b.template_topic_id = c.topic_id "
                sql &= " INNER jOIN idp_m_master_topic d ON c.master_topic_id = d.master_topic_id "
                sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND  ISNULL(b.template_is_require,0) = 0 "
                sql &= " AND a.is_ladder_template = 1 AND a.template_id =  '" & txtform.SelectedValue & "'"
                sql &= " AND b.template_category_id = " & txtadd_cat1.SelectedValue
                sql &= " AND template_topic_id NOT IN (SELECT topic_id FROM idp_function_tab WHERE 1 =1 "
                If mode = "add" Then
                    sql &= " AND session_id = '" & Session.SessionID & "' "
                Else
                    sql &= " AND idp_id = " & id
                End If
                sql &= ""
                sql &= ")"
            End If


            ' TextBox1.Text = sql
            ' Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtfind_topic.DataTextField = "topic_name_" & lang
            txtfind_topic.DataSource = ds
            txtfind_topic.DataBind()

            ' txtfind_topic.Items.Insert(0, New ListItem("--Please Select--", "0"))

            If lang = "th" Then

                txtfind_topic.Items.Insert(0, New ListItem("--กรุณาเลือก--", "0"))

                If flag <> "ladder" Then
                    txtfind_topic.Items.Add(New ListItem("อื่นๆ", "9999"))
                End If
            Else

                txtfind_topic.Items.Insert(0, New ListItem("--Please Select--", "0"))

                If flag <> "ladder" Then
                    txtfind_topic.Items.Add(New ListItem("Other", "9999"))
                End If
            End If





        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindElectiveDetail()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT b.template_category_id , b.template_topic_id AS topic_id , b.template_topic_name AS  topic_name_th , b.template_topic_name AS  topic_name_en ,template_expect_id , template_expect_detail , template_methodogy_id , template_start_date_ts , template_complete_date_ts "
            sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
            sql &= " INNER JOIN idp_template_employee c ON b.template_id  = c.template_id "
            sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND ISNULL(b.template_is_require,0) = 0  "
            sql &= " AND c.emp_code = '" & Session("emp_code").ToString & "' AND b.template_category_id = " & txtadd_cat1.SelectedValue
            sql &= " AND b.template_topic_id = " & txtfind_topic.SelectedValue

            sql &= " UNION "
            sql &= "SELECT b.template_category_id ,b.template_topic_id AS topic_id, b.template_topic_name AS  topic_name_th , b.template_topic_name AS  topic_name_en ,template_expect_id, template_expect_detail , template_methodogy_id , template_start_date_ts , template_complete_date_ts  "
            sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
            sql &= " INNER JOIN idp_template_costcenter c ON b.template_id  = c.template_id "
            sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND  ISNULL(b.template_is_require,0) = 0  "
            sql &= " AND c.costcenter_id = '" & Session("dept_id").ToString & "' AND b.template_category_id = " & txtadd_cat1.SelectedValue
            sql &= " AND b.template_topic_id = " & txtfind_topic.SelectedValue

            sql &= " UNION "
            sql &= "SELECT b.template_category_id , b.template_topic_id AS topic_id, b.template_topic_name AS  topic_name_th , b.template_topic_name AS  topic_name_en ,template_expect_id, template_expect_detail , template_methodogy_id , template_start_date_ts , template_complete_date_ts "
            sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
            sql &= " INNER JOIN idp_template_jobtitle c ON b.template_id  = c.template_id "
            sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND  ISNULL(b.template_is_require,0) = 0  "
            sql &= " AND c.job_title_en = '" & addslashes(Session("job_title").ToString) & "' AND b.template_category_id = " & txtadd_cat1.SelectedValue
            sql &= " AND b.template_topic_id = " & txtfind_topic.SelectedValue

            sql &= " UNION "
            sql &= "SELECT b.template_category_id , b.template_topic_id AS topic_id, b.template_topic_name AS  topic_name_th , b.template_topic_name AS  topic_name_en ,template_expect_id, template_expect_detail , template_methodogy_id , template_start_date_ts , template_complete_date_ts "
            sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
            sql &= " INNER JOIN idp_template_jobtype c ON b.template_id  = c.template_id "
            sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND  ISNULL(b.template_is_require,0) = 0 "
            sql &= " AND c.job_type_name_en = '" & addslashes(Session("user_position").ToString) & "' AND b.template_category_id = " & txtadd_cat1.SelectedValue
            sql &= " AND b.template_topic_id = " & txtfind_topic.SelectedValue
            'TextBox1.Text = sql

            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                '  txtfind_expect.Items.FindByText(ds.Tables("t1").Rows(0)("template_expect_detail").ToString).Selected = True
                Try
                    txtfind_expect.SelectedValue = ds.Tables("t1").Rows(0)("template_expect_id").ToString
                    txtadd_expect1.Text = txtfind_expect.SelectedItem.Text
                Catch ex As Exception

                End Try

                txtadd_method1.SelectedValue = ds.Tables("t1").Rows(0)("template_methodogy_id").ToString
                txtadd_datestart1.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("template_start_date_ts").ToString)
                txtadd_dateend1.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("template_complete_date_ts").ToString)
            End If
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
            sql = "SELECT * FROM idp_m_expect WHERE ISNULL(is_expect_delete,0) = 0 "

            ds = conn.getDataSetForTransaction(sql, "t1")
            If lang = "th" Then
                txtfind_expect.DataTextField = "expect_detail"
            Else
                txtfind_expect.DataTextField = "expect_detail_en"
            End If
            txtfind_expect.DataSource = ds
            txtfind_expect.DataBind()

            ' txtfind_expect.Items.Insert(0, New ListItem("--Please Select--", "0"))
            If lang = "th" Then
                '  txtfind_expect.Items.Insert(0, New ListItem("--กรุณาเลือก--", "0"))
                '  txtfind_expect.Items.Add(New ListItem("อื่นๆ", "9999"))
            Else
                '  txtfind_expect.Items.Insert(0, New ListItem("--Please Select--", "0"))
                '  txtfind_expect.Items.Add(New ListItem("Other", "9999"))
            End If



        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Private Sub bindGridIDPLog()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder
        Dim status_table As String = ""
        Try
            If flag = "" Then
                status_table = "idp_status_list"
            Else
                status_table = "idp_status_ladder"
            End If

            sqlB.Append("SELECT * , ISNULL(b.status_name, a.log_remark) AS idp_status_name FROM idp_status_log a LEFT OUTER JOIN " & status_table & " b ON a.status_id = b.idp_status_id WHERE a.idp_id = " & id)
            sqlB.Append(" ORDER BY log_time ASC")
            ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

            'Response.Write(sqlB.ToString)
            ' Return
            GridviewIDPLog.DataSource = ds
            GridviewIDPLog.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub

    Protected Sub onSave(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            If txtadd_cat1.SelectedValue <> "" And txtadd_topic1.Text <> "" And txtadd_expect1.Text <> "" Then
                addTopicNoCommit("1")
            End If

            updateTransList(e.CommandArgument.ToString)
            updateFunctionPersonalTab()
            updateIDP()
            conn.setDBCommit()

            If e.CommandArgument.ToString = "" Then

            ElseIf e.CommandArgument.ToString = "2" Then ' Submit and send mail to alert sup/manager
                loopMail(lblIDP_NO.Text)
                Response.Redirect("idp_detail.aspx?mode=edit&id=" & id & "&flag=" & flag)
            Else
                Response.Redirect("idp_detail.aspx?mode=edit&id=" & id & "&flag=" & flag)
            End If


        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("onSave : " & ex.Message)
            'Response.End()
        End Try
    End Sub

    Sub updateTemplateLadder()
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet
        Dim pk As String
        Dim score_require As String = "0"
        Dim score_elective As String = "0"

        Try
            If txtform.SelectedValue <> "" Then
                sql = "DELETE FROM idp_function_tab WHERE 1 = 1 "
                If mode = "add" Then
                    sql &= " AND session_id = '" & session_id & "' "
                Else
                    sql &= " AND idp_id = " & id
                End If
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If

                sql = "SELECT * FROM idp_template_detail a INNER JOIN idp_template_master b ON a.template_id = b.template_id "
                sql &= " INNER JOIN idp_m_topic c ON a.template_topic_id = c.topic_id "
                sql &= " WHERE a.template_id = " & txtform.SelectedValue
                sql &= " AND a.template_is_require = 1 "
                'Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    score_require = ds.Tables("t1").Rows(i)("ladder_require_score").ToString
                    score_elective = ds.Tables("t1").Rows(i)("ladder_elective_score").ToString
                    lblScoreRequire.Text = ds.Tables("t1").Rows(i)("ladder_require_score").ToString
                    lblScoreElective.Text = ds.Tables("t1").Rows(i)("ladder_elective_score").ToString

                    pk = getPK("function_id", "idp_function_tab", conn)
                    sql = "INSERT INTO idp_function_tab (function_id , session_id  , template_title , ipd_type_id , is_required , category_id , topic_id "
                    sql &= ", category_name , topic_name , topic_name_en , expect_detail , method_id , methodology , nursing_score , nursing_limit , date_start , date_complete "
                    sql &= " , date_start_ts , date_complete_ts , topic_status , order_sort"
                    sql &= ") VALUES("
                    sql &= "'" & pk & "' ,"
                    sql &= "'" & session_id & "' ,"
                    sql &= "'" & addslashes(txtform.SelectedItem.Text) & "' ,"
                    sql &= "'" & 1 & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("template_is_require").ToString & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("template_category_id").ToString & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("template_topic_id").ToString & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("template_category_name").ToString & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("topic_name_th").ToString & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("topic_name_en").ToString & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("template_expect_detail").ToString & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_id").ToString & "' ," ' methodogy
                    sql &= "'" & ds.Tables(0).Rows(i)("template_methodogy_name").ToString & "' ," ' methodogy
                    sql &= "'" & ds.Tables(0).Rows(i)("template_score").ToString & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("template_limit").ToString & "' ,"

                    If ds.Tables(0).Rows(i)("template_start_date_ts").ToString = "0" Then
                        sql &= " null ,"
                    Else
                        sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_start_date_ts").ToString) & "' ,"
                    End If

                    If ds.Tables(0).Rows(i)("template_complete_date_ts").ToString = "0" Then
                        sql &= " null ,"
                    Else
                        sql &= "'" & ConvertTSToSQLDateTime(ds.Tables(0).Rows(i)("template_complete_date_ts").ToString) & "' ,"
                    End If

                    sql &= "'" & ds.Tables(0).Rows(i)("template_start_date_ts").ToString & "' ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("template_complete_date_ts").ToString & "' ,"
                    sql &= " null ,"
                    sql &= "'" & ds.Tables(0).Rows(i)("template_order_sort").ToString & "' "
                    sql &= ")"
                    'Response.Write(sql)
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                    End If
                Next i



                If mode = "edit" Then

                    sql = "UPDATE idp_function_tab SET idp_id = " & id & " , session_id = null WHERE session_id = '" & session_id & "' "
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If

                    sql = "UPDATE idp_trans_list SET ladder_template_id = " & txtform.SelectedValue
                    sql &= " , ladder_template_name = '" & txtform.SelectedItem.Text & "' "
                    sql &= " , score_template_require = '" & score_require & "' "
                    sql &= " , score_template_elective = '" & score_elective & "' "

                    sql &= " WHERE idp_id = " & id
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If


                End If

                conn.setDBCommit()
            End If

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub updateTransList(ByVal status_id As String, Optional ByVal log_remark As String = "")
        Dim sql As String
        Dim sql2 As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim ds2 As New DataSet
        Dim new_idp_no As String

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


            sql = "INSERT INTO idp_trans_list (idp_id , idp_no , idp_runno , idp_monthno , idp_yearno , plan_year , plan_month , create_date , date_submit , date_submit_ts , status_id , report_dept_id , report_dept_name ,  report_by , report_emp_code , report_jobtitle , report_jobtype)"
            sql &= " VALUES("
            sql &= "" & pk & " ,"

            If status_id = "2" Then ' Submitted
                Dim m As String = Date.Now.Month
                Dim y As String
                Dim runno As String

                sql2 = "SELECT * FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString & " ORDER BY idp_runno DESC , idp_yearno DESC "
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
                        runno = 1
                    End Try

                End If

                If flag = "ladder" Then
                    new_idp_no = y & "-" & m.PadLeft(2, "0") & "-" & runno.ToString.PadLeft(5, "0")
                Else
                    new_idp_no = y & "-" & Session("emp_code").ToString & "-" & runno.ToString.PadLeft(5, "0")
                End If


                sql &= " '" & new_idp_no & "' ,"
                sql &= " '" & runno & "' ,"
                sql &= " '" & m & "' ,"
                sql &= " '" & y & "' ,"
            Else
                sql &= " null ,"
                sql &= " null ,"
                sql &= " null ,"
                sql &= " null ,"
            End If
            sql &= txtyear.SelectedValue & " ," ' Plan year
            If flag = "ladder" Then
                sql &= txtmonth.SelectedValue & " ," ' Plan month
            Else
                sql &= " null ," ' Plan month
            End If

            sql &= " GETDATE() ,"
            sql &= " GETDATE() ,"
            sql &= "" & Date.Now.Ticks & " ,"
            sql &= "" & status_id & " ,"
            sql &= "'" & Session("dept_id").ToString & "' ,"
            sql &= "'" & Session("dept_name").ToString & "' ,"
            sql &= "'" & Session("user_fullname").ToString & "' ,"
            sql &= "'" & Session("emp_code").ToString & "' ,"
            sql &= "'" & addslashes(Session("job_title").ToString) & "' ,"
            sql &= "'" & addslashes(Session("user_position").ToString) & "' " ' jobtype
            sql &= ")"
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            If flag = "ladder" Then
                sql = "SELECT * FROM idp_template_detail a INNER JOIN idp_template_master b ON a.template_id = b.template_id WHERE a.template_id = " & txtform.SelectedValue
                '  Response.Write(sql)
                ds2 = conn.getDataSetForTransaction(sql, "t100")
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If
                ' Response.Write("X" & ds2.Tables("t00").Rows(0)("ladder_require_score").ToString)
                ' Return
                sql = "UPDATE idp_trans_list SET ladder_template_id = " & txtform.SelectedValue
                sql &= " , ladder_template_name = '" & txtform.SelectedItem.Text & "' "
                sql &= " , score_template_require = '" & ds2.Tables("t100").Rows(0)("ladder_require_score").ToString & "' "
                sql &= " , score_template_elective = '" & ds2.Tables("t100").Rows(0)("ladder_elective_score").ToString & "' "
                sql &= " , is_ladder = 1 "
                sql &= " WHERE idp_id = " & pk
                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If

            id = pk
        Else
            If status_id <> "" Then ' ถ้าไม่ใช่ save draft


                sql = "UPDATE idp_trans_list SET status_id = '" & status_id & "'"
                sql &= " , plan_year = " & txtyear.SelectedValue
                If flag = "ladder" Then
                    sql &= " , plan_month = " & txtmonth.SelectedValue
                End If



                If status_id = "2" Then ' Submitted
                    Dim y As String
                    Dim runno As String

                    If flag <> "ladder" Then
                        sql2 = "SELECT * FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString & " ORDER BY idp_runno DESC , idp_yearno DESC "
                    Else
                        sql2 = "SELECT * FROM idp_trans_list WHERE idp_monthno = " & Date.Now.Month & " AND idp_yearno = " & Date.Now.Year & " ORDER BY idp_yearno DESC , idp_monthno DESC , idp_runno DESC "
                    End If

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
                            runno = 1
                        End Try

                    End If

                    If flag = "ladder" Then
                        new_idp_no = y & "-" & Date.Now.Month.ToString.PadLeft(2, "0") & "-" & runno.ToString.PadLeft(5, "0")
                    Else
                        new_idp_no = y & "-" & Session("emp_code").ToString & "-" & runno.ToString.PadLeft(5, "0")
                    End If
                    '  new_idp_no = y & "-" & Session("emp_code").ToString & "-" & runno.ToString.PadLeft(5, "0")

                    sql &= " , idp_no = '" & new_idp_no & "' "
                    sql &= " , idp_monthno = '" & Date.Now.Month.ToString & "' "
                    sql &= " , idp_runno = '" & runno & "' "
                    sql &= " , idp_yearno = '" & y & "' "

                    sql &= ", date_submit = GETDATE() "
                    sql &= ", date_submit_ts =  " & Date.Now.Ticks

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

    Sub updateFunctionPersonalTab()
        Dim sql As String
        Dim errorMsg As String

        sql = "UPDATE idp_function_tab SET last_update = GETDATE()"
        If mode = "add" Then
            sql &= " , idp_id = " & new_idp_id
            sql &= " , session_id = null "
            'id = new_idp_id
        ElseIf mode = "edit" Then
            sql &= " , session_id = null "
        End If


        If mode = "add" Then
            sql &= " WHERE session_id = '" & session_id & "'"
        Else
            sql &= " WHERE idp_id = " & id
        End If


        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If
    End Sub

    Function getIDPNo() As String
        Dim sql As String
        Dim ds As New DataSet
        Dim y As String
        Dim runno As String
        Try
            sql = "SELECT * FROM idp_trans_list ORDER idp_runno DESC , idp_yearno DESC "
            ds = conn.getDataSetForTransaction(sql, "t1")
            y = ds.Tables("t1").Rows(0)("idp_yearno").ToString
            If y <> Date.Now.Year.ToString Then
                y = Date.Now.Year.ToString
                runno = "1"
            Else
                runno = CInt(ds.Tables("t1").Rows(0)("idp_runno").ToString) + 1
            End If
        Catch ex As Exception

        End Try
        Return ""
    End Function

    Protected Sub cmdAddTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTopic.Click
        addTopic("1") ' functional
        txtadd_cat1.SelectedIndex = 0
        txtadd_topic1.Text = ""
        txtadd_expect1.Text = ""
        txtadd_method1.SelectedIndex = 0
        txtadd_datestart1.Text = ""
        txtadd_dateend1.Text = ""
        txtadd_status1.SelectedIndex = 0

        bindTopicCombo()
        txtfind_expect.SelectedIndex = 0
    End Sub



    Sub addTopic(ByVal no As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim pk_f As String
        Dim ds As New DataSet
        Dim new_order As String

        Try
            sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM idp_function_tab WHERE"
            If mode = "add" Then
                sql &= " session_id = '" & session_id & "'"
            Else
                sql &= " idp_id = " & id
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If

            'Response.Write(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                new_order = ds.Tables(0).Rows(0)(0).ToString
            Else
                new_order = "1"
            End If


            sql = "SELECT b.template_category_id , b.template_topic_id AS topic_id, d.master_topic_name_th AS  topic_name_th , d.master_topic_name_en AS  topic_name_en  "
            sql &= " , a.template_title "
            sql &= " FROM idp_template_master a INNER JOIN idp_template_detail b ON a.template_id = b.template_id "
            sql &= " INNER JOIN idp_m_topic c ON b.template_topic_id = c.topic_id "
            sql &= " INNER jOIN idp_m_master_topic d ON c.master_topic_id = d.master_topic_id "
            sql &= " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 AND  ISNULL(b.template_is_require,0) = 0 "
            ' sql &= " and ISNULL(d.is_delete,0) = 0 "
            If flag = "ladder" Then
                sql &= " AND a.is_ladder_template = 1 AND a.template_id =  '" & txtform.SelectedValue & "'"

            End If
            sql &= " AND b.template_topic_id = " & CType(PlaceHolder1.FindControl("txtfind_topic"), DropDownList).SelectedValue
            '  txtadd_remark.Text = sql
            ds = conn.getDataSetForTransaction(sql, "tEn")
            Dim topic_name_en = ""
            Dim topic_name_th = ""

            If ds.Tables("tEn").Rows.Count > 0 Then
                topic_name_en = ds.Tables("tEn").Rows(0)("topic_name_en").ToString
                topic_name_th = ds.Tables("tEn").Rows(0)("topic_name_th").ToString
            Else
                topic_name_en = txtadd_topic1.Text
                topic_name_th = txtadd_topic1.Text
            End If
            ' Response.Write(sql)

            pk = getPK("function_id", "idp_function_tab", conn)

            If mode = "add" Then


                sql = "INSERT INTO idp_function_tab (function_id , session_id ,ipd_type_id , is_required , category_id , topic_id "
                sql &= ", category_name , topic_name , topic_name_en , expect_id ,  expect_detail , method_id , methodology , date_start , date_complete "
                sql &= " , date_start_ts , date_complete_ts , topic_status_id , topic_status , order_sort , function_remark , template_title  "
                sql &= ") VALUES("
                sql &= "'" & pk & "' ,"
                sql &= "'" & session_id & "' ,"
                sql &= "'" & no & "' ,"
                sql &= "'" & 0 & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_cat" & no), DropDownList).SelectedValue & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtfind_topic"), DropDownList).SelectedValue & "' ,"
                sql &= "'" & addslashes(CType(PlaceHolder1.FindControl("txtadd_cat" & no), DropDownList).SelectedItem.Text) & "' ,"
                '  sql &= "'" & addslashes(CType(PlaceHolder1.FindControl("txtadd_topic" & no), TextBox).Text) & "' ,"
                sql &= " '" & addslashes(topic_name_th) & "' ,"
                sql &= " '" & addslashes(topic_name_en) & "' ,"

                sql &= " '" & txtfind_expect.SelectedValue & "' ,"
                sql &= "'" & addslashes(CType(PlaceHolder1.FindControl("txtadd_expect" & no), TextBox).Text) & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_method" & no), DropDownList).SelectedValue & "' ,"
                sql &= "'" & addslashes(CType(PlaceHolder1.FindControl("txtadd_method" & no), DropDownList).SelectedItem.Text) & "' ,"

                If CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & convertToSQLDatetime(CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text) & "' ,"
                End If

                If CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & convertToSQLDatetime(CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text) & "' ,"
                End If

                If CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & ConvertDateStringToTimeStamp(CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text) & "' ,"
                End If

                If CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & ConvertDateStringToTimeStamp(CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text) & "' ,"
                End If
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_status" & no), DropDownList).SelectedValue & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_status" & no), DropDownList).SelectedItem.Text & "' ,"
                sql &= "'" & new_order & "' , "
                sql &= "'" & addslashes(txtadd_remark.Text) & "' , "
                Try
                    sql &= "'" & addslashes(ds.Tables("tEn").Rows(0)("template_title").ToString) & "' "
                Catch ex As Exception
                    sql &= "'" & "-" & "' "
                End Try

                sql &= ")"
                ' TextBox1.Text = sql
                ' Response.Write(111111)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If


            ElseIf mode = "edit" Then


                '   Response.Write(txtadd_method1.SelectedValue & "<hr/>")
                sql = "INSERT INTO idp_function_tab (function_id , idp_id , ipd_type_id , is_required , category_id , topic_id "
                sql &= ", category_name , topic_name , topic_name_en , expect_id , expect_detail , method_id , methodology , date_start , date_complete "
                sql &= " , date_start_ts , date_complete_ts ,topic_status_id , topic_status , order_sort , function_remark , template_title "
                sql &= ") VALUES("
                sql &= "'" & pk & "' ,"
                sql &= "'" & id & "' ,"
                sql &= "'" & no & "' ,"
                sql &= "'" & 0 & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_cat" & no), DropDownList).SelectedValue & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtfind_topic"), DropDownList).SelectedValue & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_cat" & no), DropDownList).SelectedItem.Text & "' ,"
                ' sql &= "'" & addslashes(CType(PlaceHolder1.FindControl("txtadd_topic" & no), TextBox).Text) & "' ,"
                sql &= " '" & addslashes(topic_name_th) & "' ,"
                sql &= " '" & addslashes(topic_name_en) & "' ,"
                sql &= " '" & txtfind_expect.SelectedValue & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_expect" & no), TextBox).Text & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_method" & no), DropDownList).SelectedValue & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_method" & no), DropDownList).SelectedItem.Text & "' ,"
                If CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & convertToSQLDatetime(CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text) & "' ,"
                End If

                If CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & convertToSQLDatetime(CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text) & "' ,"
                End If

                If CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & ConvertDateStringToTimeStamp(CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text) & "' ,"
                End If

                If CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & ConvertDateStringToTimeStamp(CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text) & "' ,"
                End If


                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_status" & no), DropDownList).SelectedValue & "' ,"
                sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_status" & no), DropDownList).SelectedItem.Text & "' ,"

                sql &= "'" & new_order & "' ,"

                sql &= "'" & addslashes(txtadd_remark.Text) & "' , "
                'sql &= "'" & addslashes(ds.Tables("tEn").Rows(0)("template_title").ToString) & "' "
                Try
                    sql &= "'" & addslashes(ds.Tables("tEn").Rows(0)("template_title").ToString) & "' "
                Catch ex As Exception
                    sql &= "'" & "-" & "' "
                End Try
                sql &= ")"


                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                If txtstatus.SelectedValue > 1 Then ' Not draft
                    sql = "UPDATE idp_function_tab SET is_new_idp = 1 WHERE function_id = " & pk
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If

            End If ' end mode edit

            If flag = "ladder" And txtfind_topic.SelectedValue <> "9999" Then

                sql = "SELECT * FROM idp_template_detail a  "
                sql &= "WHERE a.template_topic_id = " & txtfind_topic.SelectedValue & " AND a.template_category_id = " & txtadd_cat1.SelectedValue
                sql &= " AND a.template_id = " & txtform.SelectedValue

                ds = conn.getDataSetForTransaction(sql, "t100")
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                sql = "UPDATE idp_function_tab SET nursing_score = " & ds.Tables("t100").Rows(0)("template_score").ToString
                sql &= " , nursing_limit = " & ds.Tables("t100").Rows(0)("template_limit").ToString
                sql &= " WHERE function_id = " & pk
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
                '   TextBox1.Text = sql
            End If

            conn.setDBCommit()

            bindGridFunction(no)
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub addTopicNoCommit(ByVal no As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim new_order As String

        sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM idp_function_tab WHERE"
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

        pk = getPK("function_id", "idp_function_tab", conn)
        If mode = "add" Then
            sql = "INSERT INTO idp_function_tab (function_id , session_id ,ipd_type_id , is_required , category_id , topic_id "
            sql &= ", category_name , topic_name , expect_detail , method_id , methodology , date_start , date_complete "
            sql &= " , date_start_ts , date_complete_ts , topic_status_id , topic_status , order_sort"
            sql &= ") VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & session_id & "' ,"
            sql &= "'" & no & "' ,"
            sql &= "'" & 0 & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_cat" & no), DropDownList).SelectedValue & "' ,"
            sql &= "'" & 0 & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_cat" & no), DropDownList).SelectedItem.Text & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_topic" & no), TextBox).Text & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_expect" & no), TextBox).Text & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_method" & no), DropDownList).SelectedValue & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_method" & no), DropDownList).SelectedItem.Text & "' ,"

            If CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text = "" Then
                sql &= " null ,"
            Else
                sql &= "'" & convertToSQLDatetime(CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text) & "' ,"
            End If

            If CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text = "" Then
                sql &= " null ,"
            Else
                sql &= "'" & convertToSQLDatetime(CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text) & "' ,"
            End If

            If CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text = "" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertDateStringToTimeStamp(CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text) & "' ,"
            End If

            If CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text = "" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertDateStringToTimeStamp(CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text) & "' ,"
            End If
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_status" & no), DropDownList).SelectedValue & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_status" & no), DropDownList).SelectedItem.Text & "' ,"
            sql &= "'" & new_order & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        ElseIf mode = "edit" Then
            '   Response.Write(txtadd_method1.SelectedValue & "<hr/>")
            sql = "INSERT INTO idp_function_tab (function_id , idp_id , ipd_type_id , is_required , category_id , topic_id "
            sql &= ", category_name , topic_name , expect_detail , method_id , methodology , date_start , date_complete "
            sql &= " , date_start_ts , date_complete_ts ,topic_status_id , topic_status , order_sort"
            sql &= ") VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & id & "' ,"
            sql &= "'" & no & "' ,"
            sql &= "'" & 0 & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_cat" & no), DropDownList).SelectedValue & "' ,"
            sql &= "'" & 0 & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_cat" & no), DropDownList).SelectedItem.Text & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_topic" & no), TextBox).Text & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_expect" & no), TextBox).Text & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_method" & no), DropDownList).SelectedValue & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_method" & no), DropDownList).SelectedItem.Text & "' ,"
            If CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text = "" Then
                sql &= " null ,"
            Else
                sql &= "'" & convertToSQLDatetime(CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text) & "' ,"
            End If

            If CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text = "" Then
                sql &= " null ,"
            Else
                sql &= "'" & convertToSQLDatetime(CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text) & "' ,"
            End If

            If CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text = "" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertDateStringToTimeStamp(CType(PlaceHolder1.FindControl("txtadd_datestart" & no), TextBox).Text) & "' ,"
            End If

            If CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text = "" Then
                sql &= " null ,"
            Else
                sql &= "'" & ConvertDateStringToTimeStamp(CType(PlaceHolder1.FindControl("txtadd_dateend" & no), TextBox).Text) & "' ,"
            End If
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_status" & no), DropDownList).SelectedValue & "' ,"
            sql &= "'" & CType(PlaceHolder1.FindControl("txtadd_status" & no), DropDownList).SelectedItem.Text & "' ,"
            sql &= "'" & new_order & "' "
            sql &= ")"

            '   Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

        End If

        If flag = "ladder" And txtfind_topic.SelectedValue <> "9999" Then

            sql = "SELECT * FROM idp_template_detail a INNER JOIN idp_m_topic b ON a.template_topic_id = b.topic_id "
            sql &= "WHERE a.template_topic_id = " & txtfind_topic.SelectedValue & " AND a.template_category_id = " & txtadd_cat1.SelectedValue
            ds = conn.getDataSetForTransaction(sql, "t100")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If
            sql = "UPDATE idp_function_tab SET nursing_score = " & ds.Tables("t100").Rows(0)("nursing_score").ToString
            sql &= " , nursing_limit = " & ds.Tables("t100").Rows(0)("template_limit").ToString
            sql &= " WHERE function_id = " & pk
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            ' TextBox1.Text = sql
        End If
    End Sub

    Protected Sub GridFunction_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridFunction.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            If flag = "ladder" Then
                e.Row.Cells(11).Text = "วันที่ทำได้"
            End If

        End If
    End Sub



    Protected Sub GridFunction_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridFunction.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPk As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim lblFileList As Label = CType(e.Row.FindControl("lblFileList"), Label)
            Dim txtcat_id As Label = CType(e.Row.FindControl("txtcat_id"), Label)
            Dim lblRequire As Label = CType(e.Row.FindControl("lblRequire"), Label)
            Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)

            Dim lblCategoryEN As Label = CType(e.Row.FindControl("lblCategoryEN"), Label)
            Dim txtmethod_grid As DropDownList = CType(e.Row.FindControl("txtmethod_grid"), DropDownList)
            Dim lblMethod As Label = CType(e.Row.FindControl("lblMethod"), Label)
            Dim lblMethodId As Label = CType(e.Row.FindControl("lblMethodId"), Label)
            Dim txtStatusInGrid As DropDownList = CType(e.Row.FindControl("txtStatusInGrid"), DropDownList)
            Dim lblStatusId As Label = CType(e.Row.FindControl("lblStatusId"), Label)

            Dim lblProgram As Label = CType(e.Row.FindControl("lblProgram"), Label)

            Dim lblDelete As Label = CType(e.Row.FindControl("lblDelete"), Label)
            Dim lblNewIDP As Label = CType(e.Row.FindControl("lblNewIDP"), Label)

            ' Dim lblH1 As Label = CType(e.Row.FindControl("lblH1"), Label)
            'Dim lblH2 As Label = CType(e.Row.FindControl("lblH2"), Label)
            Dim lblScore As Label = CType(e.Row.FindControl("lblScore"), Label)
            Dim lblArchieve As Label = CType(e.Row.FindControl("lblArchieve"), Label)
            Dim txtTime As TextBox = CType(e.Row.FindControl("txtTime"), TextBox)
            Dim lblLimit As Label = CType(e.Row.FindControl("lblLimit"), Label)

            Dim cmdFile As Button = CType(e.Row.FindControl("cmdFile"), Button)
            Dim txtremark1 As TextBox = CType(e.Row.FindControl("txtremark1"), TextBox)
            Dim lblComment_th As Label = CType(e.Row.FindControl("lblComment_th"), Label)
            Dim lblComment_en As Label = CType(e.Row.FindControl("lblComment_en"), Label)

            Dim lblTopicNameEn As Label = CType(e.Row.FindControl("lblTopicNameEn"), Label)
            Dim lblTopicName As Label = CType(e.Row.FindControl("lblTopicName"), Label)
            Dim sql As String
            Dim ds As New DataSet

            Try
                lblArchieve.Text = "0"

                If lblDelete.Text = "1" Then
                    e.Row.Font.Strikeout = True
                    e.Row.ForeColor = Drawing.Color.Red
                End If

                If lblNewIDP.Text = "1" Then
                    e.Row.Font.Bold = True
                End If

                If flag <> "ladder" Then

                Else ' this is ladder ถ้าเป็น ladder
                    lblCategoryEN.Visible = False
                    If lang = "th" Then
                        lblComment_th.Visible = True
                        lblComment_en.Visible = False
                    Else
                        lblComment_en.Visible = True
                        lblComment_th.Visible = False
                        lblTopicName.Text = lblTopicNameEn.Text
                    End If

                    lblComment_th.Text = lblComment_th.Text.Replace(vbCrLf, "<br/>")
                    lblComment_en.Text = lblComment_en.Text.Replace(vbCrLf, "<br/>")

                    Try
                        If txtTime.Text = "" Then
                            txtTime.Text = "0"
                        End If
                        If txtTime.Text <> "" Then
                            If CInt(txtTime.Text) > CInt(lblLimit.Text) Then
                                lblArchieve.Text = CInt(lblScore.Text) * CInt(lblLimit.Text)
                            Else
                                lblArchieve.Text = CInt(lblScore.Text) * CInt(txtTime.Text)
                            End If

                        End If
                    Catch ex As Exception
                        lblArchieve.Text = "0"
                    End Try

                    If lblArchieve.Text = "" Then
                        lblArchieve.Text = "0"
                    End If

                    If txtremark1.Text = "" Then
                        is_has_remark = False
                    End If

                End If

                If txtstatus.SelectedValue <> "" Or txtstatus.SelectedValue <> "1" Then
                    ' txtmethod_grid.Enabled = False
                End If
                '  Response.Write("xxx : ")
                If lblProgram.Text <> "" Then
                    If flag <> "ladder" Then
                        lblProgram.Text = "<strong>Program :</strong> " & lblProgram.Text
                    Else ' ถ้าเป็น ladder ไม่ต้องแสดงชื่อ
                        lblProgram.Visible = False
                    End If

                End If

                If lblRequire.Text = "0" Then
                    lblRequire.Text = "E"
                    total_elective_score += CInt(lblArchieve.Text)
                Else
                    lblRequire.Text = "R"
                    total_require_score += CInt(lblArchieve.Text)
                    e.Row.BackColor = Drawing.Color.LightBlue
                    chk.Visible = False
                End If

                ' Response.Write(total_require_score & "<br/>")
                If flag = "ladder" Then
                    lblEmpScoreRequire.Text = total_require_score
                    lblEmpScoreElective.Text = total_elective_score
                End If

                If lang = "th" Then
                    txtmethod_grid.DataTextField = "method_name_th"
                Else
                    txtmethod_grid.DataTextField = "method_name"
                End If
                txtmethod_grid.DataSource = dsMethod
                txtmethod_grid.DataBind()

                Try
                    txtmethod_grid.SelectedValue = lblMethodId.Text
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try

                Try
                    txtStatusInGrid.SelectedValue = lblStatusId.Text
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try

                Try
                    If txtStatusInGrid.SelectedValue = "4" And txtstatus.SelectedValue > 1 Then
                        txtStatusInGrid.Enabled = False
                    End If
                Catch ex As Exception

                End Try

                'Response.Write("xx : ")
                txtStatusInGrid.Attributes.Add("onchange", "return onchangeStatus(this)")

                cmdFile.Attributes.Add("onclick", " openDetail('" & lblPk.Text & "','" & lblempcode.Text & "');return false;")
                sql = "SELECT * FROM idp_file_list WHERE function_id = " & lblPk.Text
                ds = conn.getDataSetForTransaction(sql, "tFile")
                Dim i As Integer = 0
                For i = 0 To ds.Tables("tFile").Rows.Count - 1
                    lblFileList.Text &= " <a href='../share/idp/attach_file/" & ds.Tables("tFile").Rows(i)("file_path").ToString & "' target='_blank'>- " & ds.Tables("tFile").Rows(i)("file_name").ToString & "</a><br/>"
                Next i
                cmdFile.Text = "Detail (" & i & ") "

            Catch ex As Exception
                Response.Write("row bound : " & ex.Message)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridFunction_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridFunction.SelectedIndexChanged

    End Sub

    Protected Sub cmdNewIDP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNewIDP.Click
        Response.Redirect("idp_detail.aspx?mode=add")
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
                    If txtstatus.SelectedValue <> "" Then
                        If CInt(txtstatus.SelectedValue) > 1 Then
                            sql = "UPDATE  idp_function_tab SET is_delete = 1 WHERE function_id = " & lbl.Text
                        ElseIf txtstatus.SelectedValue = "" Or txtstatus.SelectedValue = "1" Then
                            sql = "DELETE FROM idp_function_tab WHERE function_id = " & lbl.Text
                        End If
                    Else ' add or draft
                        sql = "DELETE FROM idp_function_tab WHERE function_id = " & lbl.Text
                    End If




                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindGridFunction("1")

            If flag = "ladder" Then
                Response.Redirect("idp_detail.aspx?mode=edit&id=" & id & "&flag=ladder")
            End If
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
        Dim txtorder As TextBox
        Dim txtmethod_grid As DropDownList
        Dim txtStatusInGrid As DropDownList
        Dim txtTime As TextBox
        Dim txtremark1 As TextBox

        i = GridFunction.Rows.Count

        Try
            If txtadd_cat1.SelectedValue <> "" And txtadd_topic1.Text <> "" And txtadd_expect1.Text <> "" Then
                addTopicNoCommit("1")
            End If

            For s As Integer = 0 To i - 1

                txtmethod_grid = CType(GridFunction.Rows(s).FindControl("txtmethod_grid"), DropDownList)
                lbl = CType(GridFunction.Rows(s).FindControl("lblPK"), Label)
                txtorder = CType(GridFunction.Rows(s).FindControl("txtorder"), TextBox)
                txtStatusInGrid = CType(GridFunction.Rows(s).FindControl("txtStatusInGrid"), DropDownList)
                txtTime = CType(GridFunction.Rows(s).FindControl("txtTime"), TextBox)
                txtremark1 = CType(GridFunction.Rows(s).FindControl("txtremark1"), TextBox)

                sql = "UPDATE idp_function_tab SET order_sort = '" & txtorder.Text & "' "
                sql &= " , method_id = " & txtmethod_grid.SelectedValue & " "
                sql &= " , methodology = '" & txtmethod_grid.SelectedItem.Text & "' "
                sql &= " , topic_status_id = " & txtStatusInGrid.SelectedValue & " "
                sql &= " , topic_status = '" & txtStatusInGrid.SelectedItem.Text & "' "
                sql &= " , nursing_time = '" & txtTime.Text & "' "
                sql &= " , function_remark = '" & addslashes(txtremark1.Text) & "' "

                If CType(GridFunction.Rows(s).FindControl("lblDate1"), TextBox).Text = "" Then
                    sql &= " , date_start = null "
                Else
                    sql &= " , date_start = '" & convertToSQLDatetime(CType(GridFunction.Rows(s).FindControl("lblDate1"), TextBox).Text) & "' "
                End If

                If CType(GridFunction.Rows(s).FindControl("lblDate2"), TextBox).Text = "" Then
                    sql &= " , date_complete = null "
                Else
                    sql &= " , date_complete = '" & convertToSQLDatetime(CType(GridFunction.Rows(s).FindControl("lblDate2"), TextBox).Text) & "' "
                End If

                If CType(GridFunction.Rows(s).FindControl("lblDate1"), TextBox).Text = "" Then
                    sql &= " , date_start_ts = null "
                Else
                    sql &= " , date_start_ts = '" & ConvertDateStringToTimeStamp(CType(GridFunction.Rows(s).FindControl("lblDate1"), TextBox).Text) & "' "
                End If

                If CType(GridFunction.Rows(s).FindControl("lblDate2"), TextBox).Text = "" Then
                    sql &= " , date_complete_ts = null "
                Else
                    sql &= " , date_complete_ts = '" & ConvertDateStringToTimeStamp(CType(GridFunction.Rows(s).FindControl("lblDate2"), TextBox).Text) & "' "
                End If

                sql &= "  WHERE function_id = " & lbl.Text
                '  Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                    Exit For
                End If
            Next s

            updateOnlyLog("0", "Updated IDP")

            conn.setDBCommit()

            bindGridFunction("1")
            txtadd_cat1.SelectedIndex = 0
            txtadd_topic1.Text = ""
            txtadd_expect1.Text = ""
            txtadd_method1.SelectedIndex = 0
            txtadd_datestart1.Text = ""
            txtadd_dateend1.Text = ""
            txtadd_status1.SelectedIndex = 0

            bindTopicCombo()
            bindGridIDPLog()

            txtfind_expect.SelectedIndex = 0
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        End Try

        '   Response.Redirect("idp_detail.aspx?mode=edit&id=" & id)
    End Sub

    Sub updateIDP()
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim txtorder As TextBox
        Dim txtmethod_grid As DropDownList
        Dim txtStatusInGrid As DropDownList
        Dim txtTime As TextBox
        Dim txtremark1 As TextBox

        i = GridFunction.Rows.Count


        'If txtadd_cat1.SelectedValue <> "" And txtadd_topic1.Text <> "" And txtadd_expect1.Text <> "" Then
        'addTopicNoCommit("1")
        'End If

        For s As Integer = 0 To i - 1

            txtmethod_grid = CType(GridFunction.Rows(s).FindControl("txtmethod_grid"), DropDownList)
            lbl = CType(GridFunction.Rows(s).FindControl("lblPK"), Label)
            txtorder = CType(GridFunction.Rows(s).FindControl("txtorder"), TextBox)
            txtStatusInGrid = CType(GridFunction.Rows(s).FindControl("txtStatusInGrid"), DropDownList)
            txtTime = CType(GridFunction.Rows(s).FindControl("txtTime"), TextBox)
            txtremark1 = CType(GridFunction.Rows(s).FindControl("txtremark1"), TextBox)

            sql = "UPDATE idp_function_tab SET order_sort = '" & txtorder.Text & "' "
            '  sql &= " , method_id = " & txtmethod_grid.SelectedValue & " "
            ' sql &= " , methodology = '" & txtmethod_grid.SelectedItem.Text & "' "
            sql &= " , topic_status_id = " & txtStatusInGrid.SelectedValue & " "
            sql &= " , topic_status = '" & txtStatusInGrid.SelectedItem.Text & "' "
            sql &= " , nursing_time = '" & txtTime.Text & "' "
            sql &= " , function_remark = '" & addslashes(txtremark1.Text) & "' "

            If CType(GridFunction.Rows(s).FindControl("lblDate1"), TextBox).Text = "" Then
                sql &= " , date_start = null "
            Else
                sql &= " , date_start = '" & convertToSQLDatetime(CType(GridFunction.Rows(s).FindControl("lblDate1"), TextBox).Text) & "' "
            End If

            If CType(GridFunction.Rows(s).FindControl("lblDate2"), TextBox).Text = "" Then
                sql &= " , date_complete = null "
            Else
                sql &= " , date_complete = '" & convertToSQLDatetime(CType(GridFunction.Rows(s).FindControl("lblDate2"), TextBox).Text) & "' "
            End If

            If CType(GridFunction.Rows(s).FindControl("lblDate1"), TextBox).Text = "" Then
                sql &= " , date_start_ts = null "
            Else
                sql &= " , date_start_ts = '" & ConvertDateStringToTimeStamp(CType(GridFunction.Rows(s).FindControl("lblDate1"), TextBox).Text) & "' "
            End If

            If CType(GridFunction.Rows(s).FindControl("lblDate2"), TextBox).Text = "" Then
                sql &= " , date_complete_ts = null "
            Else
                sql &= " , date_complete_ts = '" & ConvertDateStringToTimeStamp(CType(GridFunction.Rows(s).FindControl("lblDate2"), TextBox).Text) & "' "
            End If

            sql &= "  WHERE function_id = " & lbl.Text
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
                Exit For
            End If
        Next s


        txtadd_cat1.SelectedIndex = 0
        txtadd_topic1.Text = ""
        txtadd_expect1.Text = ""
        txtadd_method1.SelectedIndex = 0
        txtadd_datestart1.Text = ""
        txtadd_dateend1.Text = ""
        txtadd_status1.SelectedIndex = 0
        txtfind_expect.SelectedIndex = 0

    End Sub

    Protected Sub onAddTopic(ByVal sender As Object, ByVal e As CommandEventArgs)

    End Sub


    Protected Sub txtadd_cat1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtadd_cat1.SelectedIndexChanged
        txtadd_topic1.Visible = False
        txtadd_expect1.Visible = False
        txtadd_topic1.Text = ""
        ' AutoCompleteExtender1.ContextKey = txtadd_cat1.SelectedValue
        bindTopicCombo()
    End Sub

    Sub bindEducatorList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & id
            sql &= " AND ISNULL(is_educator,0) = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count = 0 And viewtype = "" Then
                tab_approve.Visible = False
            ElseIf viewtype = "dept" Then
                tab_approve.Visible = True
            End If

            GridEducator.DataSource = ds
            GridEducator.DataBind()
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
            sql &= " AND ISNULL(is_educator,0) = 0 "
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


    Sub onDeleteEducatorComment(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "DELETE FROM idp_manager_comment WHERE comment_id = " & e.CommandArgument.ToString
            errorMsg = conn.executeSQLForTransaction(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            updateOnlyLog("0", "Delete Educator Comment")
            conn.setDBCommit()

            bindEducatorList()
            bindGridIDPLog()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
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

            If lblCommentStatusId.Text = "1" Then ' Approve
                lblStatusName.ForeColor = Drawing.Color.Green
                lblStatusName.Text = "<img src='../images/button_ok.png' id='img1' alt='approve' /> " & lblStatusName.Text
            ElseIf lblCommentStatusId.Text = "2" Then ' Reject
                lblStatusName.Text = "<img src='../images/button_cancel.png' id='img1' alt='approve' /> " & lblStatusName.Text
                lblStatusName.ForeColor = Drawing.Color.Red
            Else ' N/A
                lblStatusName.Text = "" & lblStatusName.Text
                lblStatusName.ForeColor = Drawing.Color.Red
            End If

        End If
    End Sub

    Protected Sub GridComment_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridComment.RowEditing
        GridComment.EditIndex = e.NewEditIndex
        bindCommentList()
    End Sub

    Protected Sub GridComment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridComment.SelectedIndexChanged

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

            If flag = "ladder" And viewtype = "hr" Then
                sql = "UPDATE idp_trans_list SET plan_year = " & txtyear.SelectedValue
                sql &= "  , plan_month = " & txtmonth.SelectedValue & "  WHERE idp_id = " & id
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If
            '  Response.Write(flag)
            ' Response.Write(sql)
            '  Response.End()
            updateOnlyLog(txtstatus.SelectedValue, "HR process")

            conn.setDBCommit()

            bindIDPDetail()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        End Try

        Response.Redirect("idp_detail.aspx?mode=edit&id=" & id & "&flag=" & flag)
    End Sub




    Protected Sub GridviewIDPLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridviewIDPLog.PageIndexChanging
        GridviewIDPLog.PageIndex = e.NewPageIndex
        bindGridIDPLog()
    End Sub



    Protected Sub GridviewIDPLog_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridviewIDPLog.SelectedIndexChanged

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



    Protected Sub txtfind_topic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfind_topic.SelectedIndexChanged
        If txtfind_topic.SelectedValue = "9999" Then
            txtadd_topic1.Visible = True
            txtadd_topic1.Text = ""
        Else
            txtadd_topic1.Visible = False
            txtadd_topic1.Text = txtfind_topic.SelectedItem.Text
            If flag <> "ladder" Then
                bindElectiveDetail()
            End If

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

        'txtadd_expect1.Text = txtfind_expect.SelectedItem.Text
    End Sub

    Protected Sub cmdThai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdThai.Click
        Session("lang") = "th"
        Response.Redirect("idp_detail.aspx?mode=" & mode & "&id=" & id & "&lang=th&flag=" & flag)
    End Sub

    Protected Sub cmdEng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEng.Click
        Session("lang") = "en"
        Response.Redirect("idp_detail.aspx?mode=" & mode & "&id=" & id & "&lang=en&flag=" & flag)
    End Sub

    Sub loopMail(ByVal training_no As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim msgBody As String = ""
        Dim key As String = ""
        Dim host As String = ""
        Dim header As String = ""

        host = ConfigurationManager.AppSettings("frontHost").ToString

        Try
            If flag = "ladder" Then
                header = "The Nursing Clinical Ladder"
            Else
                header = "The Individual Development Plan (IDP)"
            End If
            key = UserActivation.GetActivationLink("idp/idp_detail.aspx?mode=edit&id=" & id & "&flag=" & flag)
            msgBody = "<strong>" & header & "</strong><br/><br/>"
            msgBody &= ""
            msgBody &= header & " is submitted for your approval. Please kindly open the following link for fast access.<br/>" & vbCrLf
            msgBody &= "<a href='http://" & host & "/login.aspx?viewtype=dept&key=" & key & "'>" & header & " Online</a>"

            If flag = "ladder" Then
                msgBody &= "<br/> Best regard, <br/> Nusing Clinial Ladder co ordinator"
            Else
                msgBody &= "<br/> Best regard, <br/> Training & Development Department"
            End If


            sql = "select emp_code from user_role where emp_code = " & Session("emp_code").ToString & " and role_id IN (13 , 14 , 15 ,16 ,17 )"
            ds = conn.getDataSetForTransaction(sql, "t0")
            ' ถ้าคนขอ เป็น mgr , dept. mrg , dd ,cc ไม่ส่งเมล์
            If ds.Tables("t0").Rows.Count = 0 Then
                ' ส่งเมล์ให้เฉพาะ Sup , Mgr
                sql = "SELECT * FROM user_profile a INNER JOIN user_access_costcenter_idp b ON a.emp_code = b.emp_code WHERE b.costcenter_id  = " & Session("dept_id").ToString
                sql &= " AND ISNULL(a.custom_user_email,'') <> ''  "
                sql &= "  AND a.emp_code in (select emp_code from user_role where role_id IN (13,14) )"

                '  sql &= " AND a.emp_code NOT IN (select emp_code from user_role where emp_code = " & Session("emp_code").ToString & " and role_id IN (14 , 15 ,16 ,17 ))"
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                    sendMail(ds.Tables("t1").Rows(i)("custom_user_email").ToString, "#" & training_no & " " & ds.Tables("t1").Rows(i)("user_fullname").ToString & " Please review  " & header, msgBody)

                    If flag = "ladder" Then
                        sendMail("renu@bumrungrad.com", "#" & training_no & " " & ds.Tables("t1").Rows(i)("user_fullname").ToString & " Please review " & header, msgBody)
                    End If

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
            If flag = "ladder" Then
                oMsg.From = New MailAddress("renu@bumrungrad.com")
            Else
                oMsg.From = New MailAddress("BumrungradPersonnelDevelopmentCenter@bumrungrad.com")
            End If

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


    Protected Sub cmdAddComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddComment.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim ds As New DataSet
        login_max_authen = CInt(getMyIPDLevel())

        Try
            sql = "SELECT * FROM idp_manager_comment WHERE review_by_empcode = " & Session("emp_code").ToString & " AND idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count = 0 Then
                pk = getPK("comment_id", "idp_manager_comment", conn)
                sql = "INSERT INTO idp_manager_comment (comment_id , idp_id , comment_status_id , comment_status_name , subject_id , subject , detail "
                sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
                sql &= ",create_date , create_date_ts , review_by_role_id , is_educator"
                sql &= ") VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & id & "' ,"
                sql &= " '" & txtacknowedge_status.SelectedValue & "' ,"
                sql &= " '" & txtacknowedge_status.SelectedItem.Text & "' ,"
                sql &= " '" & txtcomment.SelectedValue & "' ,"
                sql &= " '" & txtcomment.SelectedItem.Text & "' ,"
                sql &= " '" & addslashes(txtcomment_detail.Value) & "' ,"
                sql &= " '" & addslashes(Session("job_title").ToString) & "' ,"
                sql &= " '" & addslashes(Session("user_position").ToString) & "' ,"
                sql &= " '" & addslashes(Session("user_fullname").ToString) & "' ,"
                sql &= " '" & Session("emp_code").ToString & "' ,"
                sql &= " '" & Session("dept_name").ToString & "' ,"
                sql &= " '" & Session("dept_id").ToString & "' ,"
                sql &= " GETDATE() ,"
                sql &= " '" & Date.Now.Ticks & "' ,"
                sql &= " '" & login_max_authen & "' ,"
                sql &= " 0 "
                sql &= ")"
            Else
                sql = "UPDATE idp_manager_comment SET comment_status_id = " & txtacknowedge_status.SelectedValue
                sql &= " , comment_status_name = '" & txtacknowedge_status.SelectedItem.Text & "' "
                sql &= " , subject_id = '" & txtcomment.SelectedValue & "' "
                sql &= " , subject = '" & txtcomment.SelectedItem.Text & "' "
                sql &= " , detail = '" & addslashes(txtcomment_detail.Value) & "' "
                sql &= " WHERE  review_by_empcode = " & Session("emp_code").ToString & "  AND idp_id = " & id

            End If

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            updateOnlyLog("0", txtacknowedge_status.SelectedItem.Text)
            conn.setDBCommit()

            txtcomment.SelectedIndex = 0
            txtcomment_detail.Value = ""
            bindCommentList()
            bindGridIDPLog()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub



    Protected Sub txtform_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtform.SelectedIndexChanged
        If txtform.SelectedValue = 36 Or txtform.SelectedValue = 37 Or txtform.SelectedValue = 38 Or txtform.SelectedValue = 39 Then
            ' Maintain Ladder
            'txtmonth.Items.Clear()
            ' txtmonth.Items.Insert(0, New ListItem("October", 10))

            If txtmonth.Items.Count = 2 Then
                txtmonth.Items.RemoveAt(0)
            End If
        Else
            ' Adjust Ladder
            'txtmonth.Items.Clear()
            If txtmonth.Items.Count = 1 Then
                txtmonth.Items.Insert(0, New ListItem("April", 4))
            End If

            ' txtmonth.Items.Insert(1, New ListItem("Oct", 10))
        End If

        updateTemplateLadder()
        bindGridFunction("1")

        checkDupplicateLadderForm()
    End Sub

    Private Function AutoCompleteExtender2() As Object
        Throw New NotImplementedException
    End Function

    Protected Sub cmdSaveEducatorReview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveEducatorReview.Click
        'Dim sql As String
        'Dim errorMsg As String

        'Try
        '    sql = "UPDATE idp_trans_list SET educator_status_id = " & txteducator_status.SelectedValue
        '    sql &= ", educator_status_name = '" & txteducator_status.SelectedItem.Text & "' "
        '    sql &= ", educator_subject_id = " & txteducator_subject.SelectedValue
        '    sql &= ", educator_subject_name = '" & txteducator_subject.SelectedItem.Text & "' "
        '    sql &= ", educator_comment_detail = '" & addslashes(txteducator_detail.Value) & "' "
        '    sql &= ", educator_review_by = '" & Session("user_fullname").ToString & "' "
        '    sql &= ", educator_review_date_ts = " & Date.Now.Ticks
        '    sql &= ", educator_review_date_raw = GETDATE() "
        '    sql &= " WHERE idp_id = " & id

        '    errorMsg = conn.executeSQLForTransaction(sql)
        '    If errorMsg <> "" Then
        '        Throw New Exception(errorMsg)
        '    End If
        '    conn.setDBCommit()
        'Catch ex As Exception
        '    conn.setDBRollback()
        '    Response.Write(ex.Message)
        '    Return
        'End Try

        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim ds As New DataSet
        login_max_authen = CInt(getMyIPDLevel())

        Try
            sql = "SELECT * FROM idp_manager_comment WHERE is_educator = 1 AND review_by_empcode = " & Session("emp_code").ToString & " AND idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count = 0 Then
                pk = getPK("comment_id", "idp_manager_comment", conn)
                sql = "INSERT INTO idp_manager_comment (comment_id , idp_id , comment_status_id , comment_status_name , subject_id , subject , detail "
                sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
                sql &= ",create_date , create_date_ts , review_by_role_id , is_educator "
                sql &= ") VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & id & "' ,"
                sql &= " '" & txteducator_status.SelectedValue & "' ,"
                sql &= " '" & txteducator_status.SelectedItem.Text & "' ,"
                sql &= " '" & txteducator_subject.SelectedValue & "' ,"
                sql &= " '" & txteducator_subject.SelectedItem.Text & "' ,"
                sql &= " '" & addslashes(txteducator_detail.Value) & "' ,"
                sql &= " '" & addslashes(Session("job_title").ToString) & "' ,"
                sql &= " '" & addslashes(Session("user_position").ToString) & "' ,"
                sql &= " '" & addslashes(Session("user_fullname").ToString) & "' ,"
                sql &= " '" & Session("emp_code").ToString & "' ,"
                sql &= " '" & Session("dept_name").ToString & "' ,"
                sql &= " '" & Session("costcenter_id").ToString & "' ,"
                sql &= " GETDATE() ,"
                sql &= " '" & Date.Now.Ticks & "' ,"
                sql &= " '" & login_max_authen & "' ,"
                sql &= " 1  "
                sql &= ")"
            Else
                sql = "UPDATE idp_manager_comment SET comment_status_id = " & txteducator_status.SelectedValue
                sql &= " , comment_status_name = '" & txteducator_status.SelectedItem.Text & "' "
                sql &= " , subject_id = '" & txteducator_subject.SelectedValue & "' "
                sql &= " , subject = '" & txteducator_subject.SelectedItem.Text & "' "
                sql &= " , detail = '" & addslashes(txteducator_detail.Value) & "' "
                sql &= " WHERE is_educator = 1 AND review_by_empcode = " & Session("emp_code").ToString & "  AND idp_id = " & id
            End If


            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            updateOnlyLog("0", txteducator_status.SelectedItem.Text)
            conn.setDBCommit()

            txteducator_subject.SelectedIndex = 0
            txteducator_status.SelectedIndex = 0
            txteducator_detail.Value = ""
            bindEducatorList()
            bindGridIDPLog()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try

    End Sub

    Protected Sub GridEducator_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridEducator.RowDataBound
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
                cmdEditComment.Visible = False
            Else
                cmdDelComment.Visible = False
                cmdEditComment.Visible = False
            End If

            If lblCommentStatusId.Text = "1" Then ' Approve
                lblStatusName.ForeColor = Drawing.Color.Green
                lblStatusName.Text = "<img src='../images/button_ok.png' id='img1' alt='approve' /> " & lblStatusName.Text
            ElseIf lblCommentStatusId.Text = "2" Then ' Reject
                lblStatusName.Text = "<img src='../images/button_cancel.png' id='img1' alt='approve' /> " & lblStatusName.Text
                lblStatusName.ForeColor = Drawing.Color.Red
            Else ' N/A
                lblStatusName.Text = "" & lblStatusName.Text
                lblStatusName.ForeColor = Drawing.Color.Red
            End If

        End If
    End Sub

    Protected Sub GridEducator_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEducator.SelectedIndexChanged

    End Sub

    Sub bindScaleForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_trans_list WHERE idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtscale1.Text = ds.Tables("t1").Rows(0)("ladder_scale1").ToString
            txtscale2.Text = ds.Tables("t1").Rows(0)("ladder_scale2").ToString
            txtscale3.Text = ds.Tables("t1").Rows(0)("ladder_scale3").ToString
            txtscale4.Text = ds.Tables("t1").Rows(0)("ladder_scale4").ToString

            txttotal_scale.Text = ds.Tables("t1").Rows(0)("ladder_total_scale").ToString
            txtfullscore.Text = ds.Tables("t1").Rows(0)("ladder_full_score").ToString
            txtfullscore_percent.Text = ds.Tables("t1").Rows(0)("ladder_full_score_percent").ToString

            txtstatus_scale.SelectedValue = ds.Tables("t1").Rows(0)("ladder_scale_status_id").ToString
            txtscale_detail.Text = ds.Tables("t1").Rows(0)("ladder_scale_comment").ToString

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdUpdateScale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateScale.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE idp_trans_list SET ladder_scale1 = '" & txtscale1.Text & "' "
            sql &= ", ladder_scale2 = '" & txtscale2.Text & "' "
            sql &= ", ladder_scale3 = '" & txtscale3.Text & "' "
            sql &= ", ladder_scale4 = '" & txtscale4.Text & "' "
            sql &= ", ladder_total_scale = '" & txttotal_scale.Text & "' "
            sql &= ", ladder_full_score = '" & txtfullscore.Text & "' "
            sql &= ", ladder_full_score_percent = '" & txtfullscore_percent.Text & "' "
            sql &= ", ladder_scale_status_id = '" & txtstatus_scale.SelectedValue & "' "
            sql &= ", ladder_scale_status_name = '" & txtstatus_scale.SelectedItem.Text & "' "
            sql &= ", ladder_scale_comment = '" & addslashes(txtscale_detail.Text) & "' "

            sql &= ", ladder_scale_update_by = '" & Session("user_fullname").ToString & "' "
            sql &= ", ladder_scale_date_ts = " & Date.Now.Ticks
            sql &= ", ladder_scale_date_raw = GETDATE() "
            sql &= " WHERE idp_id = " & id
            '   Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            bindScaleForm()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        End Try
    End Sub

    Protected Sub cmdCal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCal.Click
        Dim result1 As Double = 0
        Dim result2 As Double = 0
        Dim result3 As Double = 0
        Dim result4 As Double = 0
        Dim full_score As Double = 0

        Try
            If txtscale1.Text <> "" Then
                result1 = CDbl(txtscale1.Text)
            End If
        Catch ex As Exception
            result1 = 0
        End Try

        Try
            If txtscale2.Text <> "" Then
                result2 = CDbl(txtscale2.Text)
            End If
        Catch ex As Exception
            result2 = 0
        End Try

        Try
            If txtscale3.Text <> "" Then
                result3 = CDbl(txtscale3.Text)
            End If
        Catch ex As Exception
            result3 = 0
        End Try

        Try
            If txtscale4.Text <> "" Then
                result4 = CDbl(txtscale4.Text)
            End If
        Catch ex As Exception
            result4 = 0
        End Try

        Try
            If txtfullscore.Text <> "" Then
                full_score = CDbl(txtfullscore.Text)
            End If
        Catch ex As Exception
            full_score = 1
        End Try

        txttotal_scale.Text = result1 + result2 + result3 + result4

        Try
            If txtfullscore.Text = "" Then
                Throw New Exception("")
            End If
            txtfullscore_percent.Text = FormatNumber(((result1 + result2 + result3 + result4) / full_score) * 100, 1)
        Catch ex As Exception
            txtfullscore_percent.Text = 0
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
            Dim p3 As String = ""
            Dim p4 As String = txtmessage.Value & "<br/>" & vbCrLf
            Dim p5 = ""

            If flag = "ladder" Then
                p3 = "[#" & lblIDP_NO.Text & " " & lblname.Text & "]  : " & "Please review Nursing Clinical Ladder"
            Else
                p3 = "[#" & lblIDP_NO.Text & " " & lblname.Text & "]  : " & "Please review IDP"
            End If

            ' Dim parameters As Object() = New Object() {p1, p2, p3, p4, p5}

            'thread.Start(parameters)
            Dim host As String = ConfigurationManager.AppSettings("frontHost").ToString
            Dim key = UserActivation.GetActivationLink("idp/idp_detail.aspx?mode=edit&id=" & id)
            Dim msgbody As String = ""
            Dim header As String = ""

            If flag = "ladder" Then
                header = "The Nursing Clinical Ladder"
            Else
                header = "The Individual Development Plan (IDP)"
            End If

            If txtsubject.SelectedValue = "4" Then ' To Accounting
                msgbody = "<strong>" & header & "</strong><br/><br/>"
                msgbody &= ""
                msgbody = header & " is submitted for your approval. Please kindly open the following link for fast access.<br/>" & vbCrLf
                msgbody &= "<a href='http://bhtraining/login.aspx?viewtype=dept&key=" & key & "'>The Individual Development Plan </a>"
                If flag = "ladder" Then
                    msgbody &= "<br/> Best regard, <br/> Nusing Clinial Ladder co ordinator"
                Else
                    msgbody &= "<br/> Best regard, <br/> Learning & Development Division"
                End If

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
        ' renu@bumrungrad.com
        'sukanyarat@bumrungrad.com
        Try
            If flag = "ladder" Then
                oMsg.From = New MailAddress("renu@bumrungrad.com")
                If chkHigh.Checked = True Then
                    oMsg.Headers.Add("Disposition-Notification-To", "<renu@bumrungrad.com>")
                End If
            Else
                ' oMsg.From = New MailAddress("traininggroup@bumrungrad.com")
                oMsg.From = New MailAddress("BumrungradPersonnelDevelopmentCenter@bumrungrad.com")
                If chkHigh.Checked = True Then
                    oMsg.Headers.Add("Disposition-Notification-To", "<suneeporn@bumrungrad.com>")
                End If
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
            txtsubject.Text = ""
            txtsend_sms.Text = ""
            txtmessage.Value = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub txtmonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmonth.SelectedIndexChanged
        'If flag = "ladder" Then
        '    updateTemplateLadder()
        '    bindGridFunction("1")

        '    checkDupplicateLadderForm()
        'End If
    End Sub

    Protected Sub txtyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtyear.SelectedIndexChanged
        'If flag = "ladder" Then
        '    updateTemplateLadder()
        '    bindGridFunction("1")

        '    checkDupplicateLadderForm()
        'End If
    End Sub
End Class
