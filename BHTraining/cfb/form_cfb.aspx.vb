Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Threading
Imports System.Net.Mail
Imports System.Net
Imports QueryStringEncryption

Namespace cfb
    Partial Class form_cfb
        Inherits System.Web.UI.Page
        Protected mode As String = ""
        Protected cfbId As String = "0"
        Private new_ir_id As String = ""
        Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Protected viewtype As String = ""
        Protected session_id As String = ""
        Protected lang As String = "th"
        Protected public_ir_id As String = ""
        Protected cid As String = "" ' comment_id
        Protected is_lock As String = "0"
        Protected revision_no As String = ""
        Protected costcenter_list() As String
        Protected authorized_access_linkclick As Boolean

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            mode = Request.QueryString("mode")
            cfbId = Request.QueryString("cfbId")
            cid = Request.QueryString("cid")

            If cfbId = "" Then
                cfbId = "0"
            End If

            If IsNothing(Session("session_myid")) Then
                Response.Redirect("../login.aspx")
                Response.End()
            End If

            session_id = Session("session_myid").ToString
            costcenter_list = Session("costcenter_list")
            authorized_access_linkclick = isAccessToLinkClick()

            If Session("lang") IsNot Nothing Then ' ถ้ามี session

                If IsPostBack Then
                    Session("lang") = txtlang.SelectedValue
                Else

                End If

                lang = Session("lang").ToString


                txtlang.SelectedValue = lang

            Else

                Session("lang") = txtlang.SelectedValue
                ' Response.Write("2" & Session("lang").ToString & "<br/>")

            End If

            If conn.setConnectionForTransaction Then

            Else
                Response.Write("Connection Error")
            End If

            If IsNothing(Session("viewtype")) Then
                Response.Write("There is no viewtype session")
                Response.End()
            Else
                If Request.QueryString("viewtype") = "add" Then
                    Session("viewtype") = ""
                    viewtype = ""
                Else
                    viewtype = Session("viewtype").ToString
                End If

                ' hide investigate tab
                tabInvestigate.Visible = False

                If Session("flag") = "update" Then
                    cmdTQMDraft1.Visible = False
                    cmdTQMDraft2.Visible = False
                    cmdDeptReturn1.Visible = False
                    cmdDeptReturn2.Visible = False
                    panel_department.Enabled = False
                End If
            End If

            'cmdPrintForm.Attributes.Add("onClick", "window.open('../pdf/cfbprint.php')")

            If mode = "add" Then
                tab_update.Visible = False
                cmdSubmit.Enabled = False
                cmdSubmit2.Enabled = False
                cmdPrintForm.Enabled = False
            Else
                tab_update.Visible = True
                cmdSubmit.Enabled = False
                cmdSubmit2.Enabled = False
                If txtstatus.SelectedValue <> "" Then
                    cmdPrintForm.Enabled = True
                Else
                    cmdPrintForm.Enabled = True
                End If

            End If

            If Page.IsPostBack Then

            Else ' load first time


                If viewtype = "" Or mode = "add" Then
                    'txtdivision.ReadOnly = True

                    txttab_alert.Visible = False
                    txttab_alert_log.Visible = False
                    tabManager.Visible = False
                    tabPSM.Visible = False
                    tabTQM.Visible = False

                    cmdTQMView1.Visible = False
                    cmdTQMView2.Visible = False
                    cmdTQMClose1.Visible = False
                    cmdTQMClose2.Visible = False
                    cmdTQMDraft1.Visible = False
                    cmdTQMDraft2.Visible = False
                    cmdDeptReturn1.Visible = False
                    cmdDeptReturn2.Visible = False
                    cmdPSMReturn1.Visible = False
                    cmdPSMReturn2.Visible = False


                ElseIf viewtype = "tqm" Then

                    cmdDraft.Visible = False
                    cmdDraft2.Visible = False
                    cmdSubmit.Visible = False
                    cmdSubmit2.Visible = False
                    cmdDeptReturn1.Visible = False
                    cmdDeptReturn2.Visible = False
                    cmdPSMReturn1.Visible = False
                    cmdPSMReturn2.Visible = False
                    'panel_department.Enabled = False
                    ' readonlyControl(panel_department)
                    readonlyControl(panel_psm)
                    ' panel_psm.Enabled = False
                ElseIf viewtype = "dept" Then

                    cmdDraft.Visible = False
                    cmdDraft2.Visible = False
                    cmdSubmit.Visible = False
                    cmdSubmit2.Visible = False

                    cmdTQMClose1.Visible = False
                    cmdTQMClose2.Visible = False
                    cmdTQMView1.Visible = False
                    cmdTQMView2.Visible = False
                    cmdPSMReturn1.Visible = False
                    cmdPSMReturn2.Visible = False

                    cmdTQMDraft1.Enabled = False
                    cmdTQMDraft2.Enabled = False
                    cmdDeptReturn1.Enabled = False
                    cmdDeptReturn2.Enabled = False

                    ' panel_cfb_detail.Enabled = False
                    panel_cfb_investigate.Enabled = False
                    readonlyControl(panel_cfb_investigate)
                    tabTQM.Visible = False
                    tabPSM.Visible = False

                    txttab_alert.Visible = False
                    txttab_alert_log.Visible = False
                ElseIf viewtype = "psm" Then
                    cmdDraft.Visible = False
                    cmdDraft2.Visible = False
                    cmdSubmit.Visible = False
                    cmdSubmit2.Visible = False

                    cmdTQMClose1.Visible = False
                    cmdTQMClose2.Visible = False
                    cmdTQMView1.Visible = False
                    cmdTQMView2.Visible = False

                    cmdDeptReturn1.Visible = False
                    cmdDeptReturn2.Visible = False

                    'panel_cfb_detail.Enabled = False
                    'panel_cfb_investigate.Enabled = False
                    readonlyControl(panel_cfb_detail)
                    readonlyControl(panel_cfb_investigate)

                    tabTQM.Visible = True
                    tabManager.Visible = True
                    txttab_alert.Visible = False
                    txttab_alert_log.Visible = False
                ElseIf viewtype = "ha" Then
                    cmdDraft.Visible = False
                    cmdDraft2.Visible = False
                    cmdSubmit.Visible = False
                    cmdSubmit2.Visible = False

                    cmdTQMDraft1.Visible = False
                    cmdTQMDraft2.Visible = False

                    cmdTQMClose1.Visible = False
                    cmdTQMClose2.Visible = False
                    cmdTQMView1.Visible = False
                    cmdTQMView2.Visible = False

                    cmdDeptReturn1.Visible = False
                    cmdDeptReturn2.Visible = False
                    cmdPSMReturn1.Visible = False
                    cmdPSMReturn2.Visible = False

                    'panelTQM.Enabled = False
                    readonlyControl(panelTQM)
                    '  readonlyControl(panelTQM2, True)
                    'panel_psm.Enabled = False
                    readonlyControl(panel_psm)
                    txttab_alert.Visible = False
                    txttab_alert_log.Visible = False
                Else
                    Response.Write("Are you a hacker ??")
                    Response.End()
                End If
                ' =============================================================

                bindLanguage(lang)

                bindStatus()
                bindHour()
                bindMinute()
                bindFile()
                ' bindNational()
                bindComboGrandTopic()
                bindLocationCombo()
                bindResponseUnitCombo()

                If mode = "edit" Then
                    '  Response.Write(11111 & txtstatus.SelectedValue)

                    bindRevision()
                    bindGridIncidentLog()
                    bindGridAlertLog()

                    bindCFBDetail() ' Bind TQM , Dept , PSM and other

                    bindGridRepeatHistory() ' Repeat CFB

                    bindGridComment()
                    bindRelateDept_Combo()
                    bindTQMCase()
                    bindDefendantUnitCombo()
                    bindDefendantUnit_TQM_Combo()

                    If cid <> "" Then
                        txttqmcase.SelectedValue = cid
                    End If

                    bindGridInformationUpdate()

                    If txttqmcase.SelectedValue <> "" Then

                        bindTQMTab(txttqmcase.SelectedValue)
                        bindTQMGridCause()
                    End If

                    If txtstatus.SelectedValue = "2" Then ' ถ้ายังไม่รับเรื่อง
                        cmdDraft.Visible = False
                        cmdDraft2.Visible = False
                        cmdTQMDraft1.Visible = False
                        cmdTQMDraft2.Visible = False
                        cmdTQMClose1.Visible = False
                        cmdTQMClose2.Visible = False
                        tabManager.Visible = False
                        tabTQM.Visible = False
                        tabPSM.Visible = False

                        cmdTQMView2.Text = "รับเรื่อง (Receive case)"
                        cmdTQMView1.Text = "รับเรื่อง (Receive case)"
                    End If

                    If txtstatus.SelectedValue = "9" Then
                        '  txtirno.Visible = False
                        Panel1.Visible = False
                        Panel2.Visible = False

                        If viewtype = "tqm" Then

                            cmdReopen2.Visible = True

                        End If
                    End If

                    If txtstatus.SelectedValue <> "1" Then
                        cmdSubmit.Enabled = False
                        cmdSubmit2.Enabled = False
                    Else
                        cmdSubmit.Enabled = True
                        cmdSubmit2.Enabled = True
                    End If


                    If viewtype = "" Then
                        If txtstatus.SelectedValue <> "1" Then
                            ' panel_cfb_detail.Enabled = False
                            ' panel_cfb_investigate.Enabled = False

                            '  disableControl(panel_cfb_detail)
                            '  disableControl(panel_cfb_investigate)

                            readonlyControl(panel_cfb_detail)
                            'readonlyControl()
                        End If
                    End If

                    If viewtype = "tqm" Or viewtype = "ha" Or viewtype = "psm" Then
                        If txtdeptcase.Items.Count > 0 Then
                            bindDeptTab()
                            bindDeptPreventiveAction()


                        End If

                        bindInfoDepartment_Select()
                        bindInfoDepartment_Grant()

                        bindDefendantUnit_Grant()
                        bindDefendantUnit_Select()

                        bindTQMPreventiveAction()

                        If viewtype = "tqm" Then
                            div_convert.Visible = True
                        End If

                        'Return
                        bindTQMDoctor()
                    End If

                  

                    If viewtype = "dept" Then
                        If txtdeptcase.Items.Count <= 0 Then
                            tabManager.Visible = False
                        Else
                            bindDeptTab()
                            bindDeptPreventiveAction()
                            bindDeptGridCause()
                            '   Response.End()
                            ' If txtdepttab_combo.SelectedValue <> Session("dept_id").ToString Then ' ถ้าไม่ใช่แผนกของตัวเองโดยตรง
                            '  Response.Write(costcenter_list)
                            If Not findArrayValue(costcenter_list, txtdepttab_combo.SelectedValue) Then ' ถ้าไม่ใช่แผนกของตัวเองโดยตรง
                                cmdTQMDraft2.Enabled = False
                                cmdTQMDraft1.Enabled = False
                                cmdDeptReturn1.Enabled = False
                                cmdDeptReturn2.Enabled = False
                            Else
                                If CInt(txtstatus.SelectedValue) = 4 And lblDeptStatusID.Text = "0" Then
                                    cmdTQMDraft1.Enabled = True
                                    cmdTQMDraft2.Enabled = True
                                    cmdDeptReturn1.Enabled = True
                                    cmdDeptReturn2.Enabled = True
                                ElseIf CInt(txtstatus.SelectedValue) = 4 And lblDeptStatusID.Text = "1" Then
                                    cmdTQMDraft2.Enabled = False
                                    cmdTQMDraft1.Enabled = False
                                    cmdDeptReturn1.Enabled = False
                                    cmdDeptReturn2.Enabled = False
                                End If
                            End If
                            'Response.Write(txtdepttab_combo.SelectedValue)
                            'Response.Write(Session("dept_id").ToString)
                            If CInt(txtstatus.SelectedValue) = 7 Or CInt(txtstatus.SelectedValue) = 9 Then
                                cmdTQMDraft1.Enabled = False
                                cmdTQMDraft2.Enabled = False
                                cmdDeptReturn1.Enabled = False
                                cmdDeptReturn2.Enabled = False


                                panel_department.Enabled = False
                                disableControl(panel_department)

                            End If


                            'panel_cfb_detail.Enabled = False
                            'disableControl(panel_cfb_detail)


                        End If

                        readonlyControl(panel_cfb_detail)

                    End If

                    If viewtype = "psm" Or viewtype = "tqm" Or viewtype = "ha" Then
                        bindFileMCO()
                        bindPSMConcern()
                        bindMCOCategory()
                        bindDept()
                    End If

                    If viewtype = "psm" Then

                        If CInt(txtstatus.SelectedValue) = 8 Or CInt(txtstatus.SelectedValue) = 9 Then
                            'panel_psm.Enabled = False

                            readonlyControl(panel_psm)
                            cmdPSMReturn1.Visible = False
                            cmdPSMReturn2.Visible = False
                            cmdTQMDraft1.Visible = False
                            cmdTQMDraft2.Visible = False
                        End If

                        If txtdeptcase.Items.Count > 0 Then
                            bindDeptTab()
                            bindDeptPreventiveAction()

                        End If

                        'panel_cfb_detail.Enabled = False
                        panel_cfb_investigate.Enabled = False
                        'panel_department.Enabled = False
                        readonlyControl(panel_cfb_detail)
                        readonlyControl(panel_department)

                    End If
                ElseIf mode = "add" Then
                    txtdivision.Text = Session("dept_name").ToString
                End If
            End If

         
            '   Response.Write(tabPSM.Visible)
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

        Sub bindLocationCombo() ' Combo box
            Dim ds As New DataSet
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "Select location_name AS location FROM m_location_new WHERE  1 = 1 "
                sql &= " GROUP BY location_name ORDER BY location_name ASC"
                'sql &= " ORDER BY dept_name"
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                txtlocation_combo.DataSource = ds
                txtlocation_combo.DataBind()

                txtlocation_combo.Items.Insert(0, New ListItem("-", ""))

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindResponseUnitCombo() ' Combo box
            Dim ds As New DataSet
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "Select location_name AS location FROM m_location_new WHERE  1 = 1 "
                sql &= " GROUP BY location_name ORDER BY location_name ASC"
                'sql &= " ORDER BY dept_name"
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                txtresponse_unit.DataSource = ds
                txtresponse_unit.DataBind()

                txtresponse_unit.Items.Insert(0, New ListItem("-", ""))

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Function isHasStatusInLog(ByVal status_id As String) As Boolean
            Dim sql As String
            Dim ds As New DataSet
            Try
                sql = "SELECT * FROM ir_status_log WHERE ir_id = " & cfbId
                sql &= " AND status_id = " & status_id
                ds = conn.getDataSetForTransaction(sql, "t1")
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try

            Return False
        End Function

        Sub bindLanguage(ByVal lang As String)
            Dim sql As String
            Dim ds As New DataSet
            Try
                sql = "SELECT * FROM m_language WHERE module_code = 'CFB' AND object_id <> 'N/A'"
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    Try
                        CType(panel_cfb_detail.FindControl(ds.Tables(0).Rows(i)("object_id").ToString), Label).Text = ds.Tables(0).Rows(i)("name_" & lang).ToString
                    Catch ex As Exception
                        Throw New Exception("ERROR : " & ds.Tables(0).Rows(i)("object_id").ToString & " : " & ex.Message)
                    End Try

                Next i

                sql = "SELECT * FROM m_language WHERE module_code = 'IR_RADIO_C' ORDER BY object_id , lang_id"
                ds = conn.getDataSetForTransaction(sql, "tCombo")
                Dim oName As String = ""
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    oName = ds.Tables(0).Rows(i)("object_id").ToString
                    
                    Try

                        CType(panel_cfb_detail.FindControl(ds.Tables(0).Rows(i)("object_id").ToString), RadioButtonList).Items(ds.Tables(0).Rows(i)("order_sort")).Text = ds.Tables(0).Rows(i)("name_" & lang).ToString
                    Catch ex As Exception
                        ' Throw New Exception("ERROR Radio : " & ds.Tables(0).Rows(i)("object_id").ToString)
                        Try
                          
                            CType(panel_cfb_detail.FindControl(ds.Tables(0).Rows(i)("object_id").ToString), DropDownList).Items(ds.Tables(0).Rows(i)("order_sort")).Text = ds.Tables(0).Rows(i)("name_" & lang).ToString
                            '  Response.Write(ds.Tables(0).Rows(i)("name_" & lang).ToString)
                        Catch ex1 As Exception
                            Throw New Exception("ERROR Dropdown : " & ds.Tables(0).Rows(i)("object_id").ToString & " " & ex1.Message)
                        End Try

                    End Try

                Next i

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try


            'txtfeedback_from.Items.RemoveAt(3)

        End Sub

        Sub bindDefendantUnit_TQM_Combo()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                'sql = "SELECT * FROM user_dept ORDER BY dept_name_en ASC"
                sql = "SELECT * FROM m_dept_unit a "
                sql &= " ORDER BY a.dept_unit_name"
                ' sql &= " ORDER BY a.dept_name_en ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                ' Response.Write(sql)
                txtwrite_dept.DataSource = ds
                txtwrite_dept.DataBind()

                txtrefund_dept.DataSource = ds
                txtrefund_dept.DataBind()

                txtremove_dept.DataSource = ds
                txtremove_dept.DataBind()

                txtwrite_dept.Items.Insert(0, New ListItem("-", ""))
                txtrefund_dept.Items.Insert(0, New ListItem("-", ""))
                txtremove_dept.Items.Insert(0, New ListItem("-", ""))

            Catch ex As Exception
                Response.Write(ex.Message & ": x" & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Private Sub bindHour()

            Dim i As Integer = 0
            Dim i_str As String = ""
            For i = 0 To 23
                i_str = i.ToString
                txthour.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
                txthour2.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
            Next

            'txthour.Items.Insert(0, New ListItem("hh", "0"))
            ' txthour2.Items.Insert(0, New ListItem("hh", "0"))
        End Sub

        Private Sub bindMinute()
            Dim i As Integer = 0
            Dim i_str As String = ""
            For i = 0 To 59
                i_str = i.ToString
                txtmin.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
                txtmin2.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
            Next

            ' txtmin.Items.Insert(0, New ListItem("mm", "0"))
            ' txtmin2.Items.Insert(0, New ListItem("mm", "0"))
            'txtmin2.Items.Insert(0, New ListItem("-", "0"))
            'txtmin2.SelectedIndex = 0


        End Sub

        Sub bindTQMGridCause()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM cfb_tqm_cause_list WHERE comment_id = " & txttqmcase.SelectedValue
                sql &= " ORDER BY order_sort"
                ds = conn.getDataSetForTransaction(sql, "t1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                GridTQMCause.DataSource = ds
                GridTQMCause.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Private Sub bindGridRepeatHistory()
            Dim ds As New DataSet
            Dim sql As String
            Dim sqlB As New StringBuilder

            Try
                sqlB.Append("SELECT a.hn ,a.datetime_report ,b.comment_type_name , c.dept_name , a.cfb_no , a.ir_id FROM cfb_detail_tab a LEFT OUTER JOIN cfb_comment_list b ON a.ir_id = b.ir_id ")
                sqlB.Append(" INNER JOIN ir_trans_list d ON a.ir_id = d.ir_id ")
                sqlB.Append(" LEFT OUTER JOIN cbf_relate_dept c ON b.comment_id = c.comment_id")
                sqlB.Append(" WHERE 1 = 1  AND ISNULL(d.is_delete,0) = 0 AND ISNULL(d.is_cancel,0) = 0 AND d.status_id > 1 ")
                If txthn.Text = "" Then
                    sqlB.Append(" AND  1 > 2 ")
                Else
                    sqlB.Append(" AND a.hn = '" & txthn.Text & "' AND a.ir_id <> " & cfbId)
                End If

                sqlB.Append(" GROUP BY a.hn ,a.datetime_report ,b.comment_type_name,a.datetime_report_ts, c.dept_name , a.cfb_no , a.ir_id")
                sqlB.Append(" ORDER BY a.datetime_report_ts DESC")
                ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sqlB.ToString)
                End If

                'Response.Write(sqlB.ToString)
                GridHistory.DataSource = ds
                GridHistory.DataBind()

                If ds.Tables(0).Rows.Count <= 0 Then
                    incidentWarn.InnerHtml = "There is no repeated customer feedback for this patient.<br/>"
                Else
                    incidentWarn.InnerHtml = "This patient has " & ds.Tables(0).Rows.Count & " repeated customer feedback, last customer feedback was reported on " & ds.Tables(0).Rows(0)("datetime_report").ToString & ". <br/>"
                End If

                bindGridIncidentHistory()
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try

        End Sub

        Private Sub bindGridIncidentHistory()
            Dim ds As New DataSet
            Dim sql As String
            Dim sqlB As New StringBuilder

            Try
                sqlB.Append("SELECT  b.hn ,b.datetime_report ,c.form_name_en  , b.datetime_report_ts , b.ir_no , a.ir_id , a.form_id FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id ")
                sqlB.Append(" INNER JOIN ir_form_master c ON c.form_id = a.form_id")
                ' sqlB.Append(" INNER JOIN ir_m_severity d ON d.severe_id = b.severe_id")
                sqlB.Append(" WHERE 1 = 1  AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 1 ")
                If txthn.Text = "" Then
                    sqlB.Append(" AND  1 > 2 ")
                Else
                    sqlB.Append(" AND b.hn = '" & txthn.Text & "'")
                End If

                sqlB.Append(" GROUP BY b.hn ,b.datetime_report ,c.form_name_en  ,  b.datetime_report_ts , b.ir_no , a.ir_id , a.form_id")
                sqlB.Append(" ORDER BY b.datetime_report_ts DESC")
                ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

                If ds.Tables(0).Rows.Count <= 0 Then
                    incidentWarn.InnerHtml &= "There is no incident for this patient."
                Else
                    If isAccessToLinkClick() Then
                        incidentWarn.InnerHtml &= "This patient has incident report on " & ds.Tables(0).Rows(0)("datetime_report").ToString & " (<a href='../incident/form_incident.aspx?mode=edit&irId=" & ds.Tables(0).Rows(0)("ir_id").ToString & "&formId=" & ds.Tables(0).Rows(0)("form_id").ToString & "' style='color:red;text-decoration:underline' target='_blank'>IR NO. " & ds.Tables(0).Rows(0)("ir_no").ToString & "</a>) "
                    Else
                        incidentWarn.InnerHtml &= "This patient has incident report on " & ds.Tables(0).Rows(0)("datetime_report").ToString & " (<asp:Label" & ds.Tables(0).Rows(0)("ir_id").ToString & "&formId=" & ds.Tables(0).Rows(0)("form_id").ToString & "' style='color:red;text-decoration:underline' target='_blank'>IR NO. " & ds.Tables(0).Rows(0)("ir_no").ToString & "</asp:Label>) "
                    End If
                End If


            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try

        End Sub

        Private Sub bindGridIncidentLog()
            Dim ds As New DataSet
            Dim sql As String
            Dim sqlB As New StringBuilder

            Try
                sqlB.Append("SELECT * FROM ir_status_log a LEFT OUTER JOIN ir_status_list b ON a.status_id = b.ir_status_id WHERE  a.ir_id = " & cfbId)
                sqlB.Append(" ORDER BY log_time ASC")
                ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sqlB.ToString)
                End If

                'Response.Write(sqlB.ToString)
                GridviewIncidentLog.DataSource = ds
                GridviewIncidentLog.DataBind()

                lblReviewLogNum.Text = "(" & ds.Tables(0).Rows.Count & ")"
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try

        End Sub

        Private Sub bindGridAlertLog()
            Dim ds As New DataSet
            Dim sql As String
            Dim sqlB As New StringBuilder

            Try
                sqlB.Append("SELECT * FROM ir_alert_log a  WHERE a.ir_id = " & cfbId)
                sqlB.Append(" ORDER BY alert_date DESC")
                ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sqlB.ToString)
                End If
                '  Response.Write(sqlB.ToString)
                GridAlertLog.DataSource = ds
                GridAlertLog.DataBind()

                lblAlertLogNum.Text = "(" & ds.Tables(0).Rows.Count & ")"
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try

        End Sub

        Sub bindComboGrandTopic()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_topic_grand a WHERE topic_type = 'cfb' "

                ds = conn.getDataSetForTransaction(sql, "t1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                txtgrandtopic.DataSource = ds
                txtgrandtopic.DataBind()
                '  Response.Write(sql)
                txtgrandtopic.Items.Insert(0, New ListItem(" -- Please select -- ", "0"))

            Catch ex As Exception
                Response.Write("bindComboGrandTopic :: " & ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindComboNormalTopic()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_topic a WHERE 1 = 1 "
                If txtgrandtopic.SelectedValue <> "0" Then
                    sql &= " AND grand_topic_id = " & txtgrandtopic.SelectedValue
                Else
                    sql &= " AND 1 > 2 "
                End If

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                ds = conn.getDataSetForTransaction(sql, "t1")
                txtnormaltopic.DataSource = ds
                txtnormaltopic.DataBind()
                ' Response.Write(sql)
                txtnormaltopic.Items.Insert(0, New ListItem(" -- Please select -- ", "0"))

            Catch ex As Exception
                Response.Write("bindComboNormalTopic :: " & ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindComboSubTopic(ByVal subtopicno As String)
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_topic_sub a WHERE ir_topic_id = " & txtnormaltopic.SelectedValue
                'sql &= " AND subtopic_no = " & subtopicno
                ' Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                txtsubtopic1.DataSource = ds
                txtsubtopic1.DataBind()
                ' Response.Write(sql)
                txtsubtopic1.Items.Insert(0, New ListItem(" -- Please select -- ", "0"))

            Catch ex As Exception
                Response.Write("bindComboNormalTopic :: " & ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindComboSubTopic2(ByVal subtopicno As String)
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_topic_sub2 a WHERE ir_subtopic_id = " & txtsubtopic1.SelectedValue
                'sql &= " AND subtopic_no = " & subtopicno
                ' Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                txtsubtopic2.DataSource = ds
                txtsubtopic2.DataBind()
                ' Response.Write(sql)
                txtsubtopic2.Items.Insert(0, New ListItem(" -- Please select -- ", "0"))

            Catch ex As Exception
                Response.Write("bindComboNormalTopic :: " & ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindComboSubTopic3(ByVal subtopicno As String)
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_topic_sub3 a WHERE ir_subtopic2_id = " & txtsubtopic2.SelectedValue
                'sql &= " AND subtopic_no = " & subtopicno
                ' Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                txtsubtopic3.DataSource = ds
                txtsubtopic3.DataBind()
                ' Response.Write(sql)
                txtsubtopic3.Items.Insert(0, New ListItem(" -- Please select -- ", "0"))

            Catch ex As Exception
                Response.Write("bindComboNormalTopic :: " & ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindDeptPreventiveAction()
            Dim sql As String
            Dim ds As New DataSet

            Try
                If txtdeptcase.SelectedValue <> "" Then
                    sql = "SELECT * FROM cfb_dept_prevent_list WHERE cfb_relate_dept_id = " & txtdeptcase.SelectedValue
                    sql &= " ORDER BY order_sort ASC"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    If ds.Tables("t1").Rows.Count > 0 Then
                        GridViewPrevent.DataSource = ds
                        GridViewPrevent.DataBind()
                    Else
                        GridViewPrevent.Columns.Clear()
                        GridViewPrevent.DataBind()
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message & sql)
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
                sql = "SELECT * FROM ir_status_list"
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

        Sub bindGridComment()
            Dim ds As New DataSet
            Dim sql As String

            Try

                sql = "SELECT * FROM cfb_comment_list WHERE 1 = 1 "
                If mode = "edit" Then
                    sql &= " AND ir_id = " & cfbId
                ElseIf mode = "add" Then
                    sql &= " AND session_id = '" & session_id & "'"
                End If
                sql &= " ORDER BY order_sort ASC"

                ds = conn.getDataSetForTransaction(sql, "t1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                'Response.Write(sql)
                GridView1.DataSource = ds
                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    lblCommentCount.Text = GridView1.Rows.Count
                Else
                    lblCommentCount.Text = ""
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindRevision()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_revision WHERE ir_id = " & cfbId
                sql &= " ORDER BY revision_id "
                ds = conn.getDataSetForTransaction(sql, "t1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                GridRevision.DataSource = ds
                GridRevision.DataBind()

                lblRevisionNum.Text = "(" & ds.Tables(0).Rows.Count & ")"
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub onSave(ByVal sender As Object, ByVal e As CommandEventArgs)
            Try

                If mode = "edit" Then
                    checkLockTransaction()

                    If is_lock = "1" Then
                        bindCFBDetail()
                        Return
                    End If
                End If
               

                updateTransList(e.CommandArgument.ToString)
                updateTypeOfComment() ' Update ตาราง Type of comment

                If viewtype = "" Or viewtype = "tqm" Then
                    isHasRow("cfb_detail_tab")
                    updateCFBDetail(e.CommandArgument.ToString)
                    updateAttachFile() ' รายการไฟล์ attach

                    If viewtype = "tqm" Then

                        updateTQMTab()
                        updateInfoDepartment()
                        '  updateDefendantDepartment()
                        updateDefendantUnit()
                        updateDeptTab()
                    End If
                ElseIf viewtype = "dept" Then
                    updateDeptTab()
                ElseIf viewtype = "psm" Then
                    updatePSMTab()
                End If

                conn.setDBCommit()

                If e.CommandArgument.ToString = "" Or e.CommandArgument.ToString = "1" Then
                    Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & cfbId)
                Else
                    Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & cfbId)

                End If

            Catch ex As Exception
                conn.setDBRollback()
                Response.Write("onSave : " & ex.Message)
            Finally
            End Try

            
        End Sub

        Sub updateDefendantUnit() ' Defendant unit
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            sql = "DELETE FROM ir_cfb_unit_defendant WHERE ir_id = " & cfbId
            sql &= " AND comment_id = " & txttqmcase.SelectedValue
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If

            For i As Integer = 0 To txtunit_defandent_select.Items.Count - 1

                pk = getPK("unit_defendant_id", "ir_cfb_unit_defendant", conn)

                sql = "INSERT INTO ir_cfb_unit_defendant (unit_defendant_id , ir_id , comment_id , dept_unit_id , dept_unit_name) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & cfbId & "' ,"
                sql &= " '" & txttqmcase.SelectedValue & "' ,"
                sql &= " '" & txtunit_defandent_select.Items(i).Value & "' ,"
                sql &= " '" & txtunit_defandent_select.Items(i).Text & "' "
                sql &= ")"
                ' Response.Write(sql)
                'Response.End()
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & "" & sql)
                End If
            Next i

        End Sub

        Sub updateTypeOfComment()
            Dim sql As String
            Dim errorMsg As String = ""
            If mode = "add" Then
                sql = "UPDATE cfb_comment_list SET ir_id = " & new_ir_id & ""
                sql &= " , session_id = ''"
                sql &= " WHERE session_id = '" & session_id & "'"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
            End If
        End Sub

        Sub updateTransList(ByVal status_id As String, Optional ByVal log_remark As String = "")
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet
            Dim ds2 As New DataSet

            If mode = "add" Then
                Try
                    sql = "SELECT ISNULL(MAX(ir_id),0) + 1 AS pk FROM ir_trans_list"
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


                sql = "INSERT INTO ir_trans_list (ir_id , date_report, date_submit , date_submit_ts , status_id , report_type , report_by ,  report_emp_code)"
                sql &= " VALUES("
                sql &= "" & pk & " ,"


                sql &= " GETDATE() ,"
                If status_id = "2" Then
                    sql &= " GETDATE() ,"
                    sql &= " " & Date.Now.Ticks & " ,"
                Else
                    sql &= " null ,"
                    sql &= " null ,"
                End If
                sql &= "" & status_id & " ,"
                sql &= " 'cfb' ,"
                sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"

                sql &= "'" & Session("emp_code").ToString & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If

                cfbId = pk

                updateOnlyLog(status_id, log_remark)
            Else
                If status_id <> "" And status_id <> "1" Then ' ถ้าไม่ใช่ save draft

                    sql = "UPDATE ir_trans_list SET is_change_to_draft = 0 WHERE ir_id = " & cfbId
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If

                    If status_id = "7" Then ' Dept Investigated
                        sql = "UPDATE cbf_relate_dept SET is_investigate = 1 WHERE cfb_relate_dept_id = " & txtdeptcase.SelectedValue
                        errorMsg = conn.executeSQLForTransaction(sql)
                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg)
                        End If

                    End If

                    If viewtype = "dept" Then
                        sql = "SELECT * FROM cbf_relate_dept a INNER JOIN cfb_comment_list b ON a.comment_id = b.comment_id "
                        sql &= " WHERE  b.ir_id = " & cfbId & " AND a.is_investigate = 1 "
                        ds = conn.getDataSetForTransaction(sql, "tDept1")

                        sql = "SELECT * FROM cbf_relate_dept a INNER JOIN cfb_comment_list b ON a.comment_id = b.comment_id "
                        sql &= " WHERE  b.ir_id = " & cfbId & " "
                        ds2 = conn.getDataSetForTransaction(sql, "tDept2")

                        If ds.Tables("tDept1").Rows.Count = ds2.Tables("tDept2").Rows.Count Then
                            updateTranListLog(status_id, log_remark)

                        End If

                        updateOnlyLog(status_id, log_remark)

                        ds.Clear() : ds.Dispose()

                        If status_id = "7" Then
                            getMailBackToTQM()
                        End If

                    Else ' ถ้าไม่ใช่ manager


                        If status_id = "2" Or status_id = "4" Or status_id = "5" Or status_id = "7" Or status_id = "8" Or status_id = "9" Or status_id = "91" Then
                            updateTranListLog(status_id, log_remark)
                            updateOnlyLog(status_id, log_remark)
                        ElseIf status_id = "3" And txtstatus.SelectedValue = "2" Then ' รับเรื่องครั้งแรก
                            updateCaseOwner()
                            updateTranListLog(status_id, log_remark)
                            updateOnlyLog(status_id, log_remark)

                            updateRevision()
                            saveHTMLForPDF(revision_no)
                        Else
                            ' updateRevision()
                            'updateOnlyLog(status_id, "Revision : " & revision_no)
                            'saveHTMLForPDF(revision_no)
                        End If

                    End If
                End If

            End If
        End Sub

        Sub updateTranListLog(ByVal status_id As String, Optional ByVal log_remark As String = "")
            Dim sql As String
            Dim errorMsg As String

            sql = "UPDATE ir_trans_list SET status_id = '" & status_id & "'"
            If status_id = "9" Then
                sql &= " , date_close = GETDATE() "
                sql &= " , date_close_ts = " & Date.Now.Ticks & " "
            End If
            If (status_id = "2") And txtstatus.SelectedValue = "1" Then
                sql &= " , date_submit = GETDATE()"
                sql &= " , date_submit_ts = " & Date.Now.Ticks & " "
            End If
            sql &= " WHERE ir_id = " & cfbId
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If


        End Sub

        Sub updateOnlyLog(ByVal status_id As String, Optional ByVal log_remark As String = "")
            Dim sql As String
            Dim errorMsg As String

            sql = "INSERT INTO ir_status_log (status_id , status_name , ir_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
            sql &= "'" & status_id & "' ,"
            sql &= "'" & "" & "' ,"
            sql &= "'" & cfbId & "' ,"
            sql &= "GETDATE() ,"
            sql &= "'" & Date.Now.Ticks & "' ,"
            sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"
            sql &= "'" & addslashes(Session("user_position").ToString) & "' ,"
            sql &= "'" & addslashes(Session("dept_name").ToString) & "' ,"
            sql &= "'" & log_remark & "' "
            sql &= ")"
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sql)
            End If
        End Sub

        Sub updateStatusNeedInvestigate()
            Dim sql As String = ""
            Dim errorMsg As String


            sql = "UPDATE cbf_relate_dept SET is_investigate = 0 WHERE comment_id "
            sql &= " IN (SELECT comment_id FROM cfb_comment_list WHERE ir_id = " & cfbId & ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

        End Sub

        Sub updateRevision()
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String
            Dim order_sort As String
            Dim ds As New DataSet

            sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ir_revision WHERE ir_id = " & cfbId
            ds = conn.getDataSetForTransaction(sql, "t1")
            order_sort = ds.Tables(0).Rows(0)(0).ToString

            Dim irno As String
            If public_ir_id <> "" Then
                irno = "CFB" & public_ir_id & "_" & Date.Now.Year & Date.Now.Month.ToString.PadLeft(2, "0") & Date.Now.Day.ToString.PadLeft(2, "0") & "_revision_" & order_sort
            Else
                irno = "CFB" & txtcfbid.Text & "_" & Date.Now.Year & Date.Now.Month.ToString.PadLeft(2, "0") & Date.Now.Day.ToString.PadLeft(2, "0") & "_revision_" & order_sort
            End If

            revision_no = irno

            pk = getPK("revision_id", "ir_revision", conn)
            sql = "INSERT INTO ir_revision (revision_id , ir_id , ir_no, revision_by_name , revision_by_empno , revision_date , revision_date_ts , file_name , order_sort) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & cfbId & "' ,"
            sql &= " '" & txtcfbid.Text.Replace("IR", "") & "' ,"
            sql &= " '" & addslashes(Session("user_fullname").ToString) & "' ,"
            sql &= " '" & Session("emp_code").ToString & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' ,"
            sql &= " '" & irno & "' ,"
            sql &= " '" & order_sort & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            saveHTMLForPDF(irno)
        End Sub

        Sub saveHTMLForPDF(ByVal irno As String)
            Dim host As String = ""
            host = ConfigurationManager.AppSettings("frontHost").ToString

            Dim strURL As String = "http://" & host & "/cfb/preview_cfb.aspx?irId=" & cfbId & "&dept_id=" & Session("dept_id").ToString



            Dim uri As New Uri(strURL)

            'Create the request object

            Dim req As HttpWebRequest = DirectCast(WebRequest.Create(uri), HttpWebRequest)
            req.UserAgent = "Get Content"
            Dim resp As WebResponse = req.GetResponse()
            Dim stream As Stream = resp.GetResponseStream()
            Dim sr As New StreamReader(stream)
            Dim html As String = sr.ReadToEnd()



            Try
                'Response.Write(Server.MapPath("../share/incident/revision/") & rev_no & ".html")
                'File.Create(Server.MapPath("../share/incident/revision/") & rev_no & ".html")

                Dim writer As StreamWriter = New StreamWriter(Server.MapPath("../share/cfb/revision/") & revision_no & ".html", False)
                writer.WriteLine(html)
                writer.Close()
                '   writer1.Close()
                'fp = File.CreateText(Server.MapPath("../share/") & "test.html")
                'fp.WriteLine(html)
                'fp.Close()

                Dim objRequest As HttpWebRequest = DirectCast(WebRequest.Create("http://" & host & "/pdf/cfbrevision.php?f=" & revision_no), HttpWebRequest)
                objRequest.UserAgent = "Get Content"
                Dim resp2 As WebResponse = objRequest.GetResponse()
                Dim stream2 As Stream = resp2.GetResponseStream()
                Dim sr2 As New StreamReader(stream2)
                Dim html2 As String = sr.ReadToEnd()

                If html2 <> "" Then
                    Throw New Exception("xxx " & html2)
                End If

            Catch err As Exception
                Response.Write(err.Message)
                Response.End()
                Return
            Finally

            End Try

            '  Response.End()
        End Sub

        Sub updateCaseOwner()
            Dim sql As String = ""
            Dim errorMsg As String


            sql = "UPDATE cfb_comment_list SET tqm_owner = '" & addslashes(Session("user_fullname").ToString) & "' WHERE ir_id = " & cfbId
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            ' Response.Write(sql)
            '  Response.End()

        End Sub

        Sub updateAttachFile()
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            If mode = "add" Then


                sql = "UPDATE cfb_attachment SET ir_id = " & cfbId & " , session_id = '' WHERE session_id = '" & Session("session_myid").ToString & "'"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & "" & sql)
                End If

            Else
                sql = "UPDATE cfb_attachment SET  session_id = '' WHERE ir_id = '" & cfbId & "'"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & "" & sql)
                End If
            End If
        End Sub

        Sub updateCFBDetail(ByVal status_id As String)
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim new_ir_no As String = ""

            If status_id = "2" Then
                Thread.Sleep(2000)
                new_ir_no = getNewCFBNo()
                If new_ir_no = "" Then
                    Throw New Exception("CFB NO. Error ")
                End If
            End If

            sql = "UPDATE cfb_detail_tab SET hn = '" & txthn.Text & "'"
            If status_id = "2" Then

                If txtcfbid.Text = "" Or txtcfbid.Text = "0" Then
                    sql &= " , cfb_no = '" & new_ir_no & "'"
                End If

            End If
            sql &= " , division = '" & txtdivision.Text & "'"
            sql &= " , report_tel = '" & addslashes(txtcontact.Text) & "'"
            sql &= " , report_tel2 = '" & addslashes(txtcfb_tel.Text) & "'"
            sql &= " , diagnosis = '" & addslashes(txtdiagnosis.Value) & "'"
            sql &= " , operation = '" & addslashes(txtoperation.Value) & "'"
            sql &= " , location = '" & addslashes(txtlocation.Text) & "'"
            sql &= " , room = '" & addslashes(txtroom.Text) & "'"
            sql &= " , datetime_complaint = '" & convertToSQLDatetime(txtdate_complaint.Text) & "'"
            sql &= " , datetime_complaint_ts = " & ConvertDateStringToTimeStamp(txtdate_complaint.Text) & ""

            If mode = "add" Then
                sql &= " , dept_id_report = '" & Session("dept_id").ToString & "'"
            End If
            'sql &= " , hn = '" & txthn.Value & "'"
            sql &= " , datetime_report = '" & convertToSQLDatetime(txtdate_report.Text, txthour.SelectedValue.PadLeft(2, "0"), txtmin.SelectedValue.PadLeft(2, "0")) & "'"
            sql &= " , datetime_report_ts = " & ConvertDateStringToTimeStamp(txtdate_report.Text, CInt(txthour.SelectedValue), CInt(txtmin.SelectedValue)) & ""
            sql &= " , complain_detail = '" & addslashes(txtcomplain.Value) & "'"
            sql &= " , service_type = '" & txtservicetype.SelectedValue & "'"
            sql &= " , pt_title = '" & txttitle.SelectedValue & "'"
            sql &= " , age = '" & txtptage.Value & "'"
            sql &= " , sex = '" & txtptsex.SelectedValue & "'"
            sql &= " , customer_segment = '" & txtsegment.SelectedValue & "'"
            sql &= " , country = '" & txtcountry.Text & "'"
            'sql &= " , cfb_dept_id = '" & txtdept.SelectedValue & "'"
            ' sql &= " , cfb_dept_name = '" & txtdept.SelectedItem.Text & "'"
            sql &= " , complain_status = '" & txtcomplain_status.SelectedValue & "'"
            sql &= " , feedback_from = '" & addslashes(txtfeedback_from.SelectedValue) & "'"

            sql &= " , feedback_topic_id = '" & addslashes(txtfeedback_othertopic.SelectedValue) & "'"
            sql &= " , feedback_topic_text = '" & addslashes(txtfeedback_othertopic.SelectedItem.Text) & "'"
            sql &= " , cfb_case_manager= '" & addslashes(txtcase_manager.Value) & "'"

            sql &= " , complain_status_remark = '" & addslashes(txtcomplain_remark.Value) & "'"
            sql &= " , feedback_from_remark = '" & addslashes(txtfeedback_remark.Value) & "'"

            sql &= " , part_customer = '" & addslashes(txtpart_customer.Value) & "'"
            sql &= " , part_hospital = '" & addslashes(txtpart_hospital.Value) & "'"
            sql &= " , part_employee = '" & addslashes(txtpart_employee.Value) & "'"

            If txtcom1.Checked = True Then
                sql &= " , cfb_is_complain =  1 "
            ElseIf txtcom2.Checked = True Then
                sql &= " , cfb_is_complain =  0 "
            Else
                sql &= " , cfb_is_complain = null"
            End If

            sql &= " , cfb_customer_resp = '" & txtcustomer.SelectedValue & "'"
            sql &= " , cfb_customer_resp_remark = '" & addslashes(txtcus_detail.Value) & "'"

            If chk_tel.Checked = True Then
                sql &= " , cfb_chk_tel = 1 "
            Else
                sql &= " , cfb_chk_tel = 0 "
            End If

            If chk_email.Checked = True Then
                sql &= " , cfb_chk_email = 1 "
            Else
                sql &= " , cfb_chk_email = 0 "
            End If

            If chk_othter.Checked = True Then
                sql &= " , cfb_chk_other = 1 "
            Else
                sql &= " , cfb_chk_other = 0 "
            End If

            sql &= " , cfb_tel_remark = '" & addslashes(txttel.Value) & "'"
            sql &= " , cfb_email_remark = '" & addslashes(txtemail.Value) & "'"
            sql &= " , cfb_other_remark = '" & addslashes(txtother.Value) & "'"

            sql &= " , new_dept_relate_name = '" & txtresponse_unit.SelectedValue.ToString & "'"
            sql &= " , new_safe_goal = '" & addslashes(txtsafegoal.Text) & "'"
            sql &= " , new_payor = '" & txtpayor.Text & "'"
            sql &= " , new_issue = '" & txtissue.Text & "'"
            sql &= " , new_severe_id = '" & txtsevere.SelectedValue & "'"
            sql &= " , new_clinical_type = '" & txtclinical.SelectedValue & "'"

            sql &= " WHERE ir_id = " & cfbId
            'Response.Write(status_id)
            ' Response.End()
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sql)
            End If
        End Sub

        Sub checkLockTransaction()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM cfb_detail_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id WHERE a.ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "t1")
                If ds.Tables(0).Rows.Count > 0 Then

                    If CInt(ds.Tables(0).Rows(0)("lock_empcode").ToString) = CInt(Session("emp_code").ToString) Then
                        is_lock = "0"
                    Else
                        is_lock = "1"
                    End If
                    ' is_lock = ds.Tables("t1").Rows(0)("is_lock").ToString
                Else
                    is_lock = "0"
                End If
            Catch ex As Exception
                is_lock = "0"
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindCFBDetail()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM cfb_detail_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id WHERE a.ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "t1")

                txtstatus.SelectedValue = ds.Tables(0).Rows(0)("status_id").ToString
                If txtstatus.SelectedValue <> "1" Then
                    cmdDraft.Enabled = False
                    cmdDraft2.Enabled = False
                    cmdSubmit.Enabled = False
                    cmdSubmit2.Enabled = False
                End If

                If ds.Tables("t1").Rows(0)("is_cancel").ToString = "1" Then
                    cmdTQMClose1.Visible = False
                    cmdTQMClose2.Visible = False
                    cmdTQMView1.Visible = False
                    cmdTQMView2.Visible = False
                    cmdTQMDraft1.Visible = False
                    cmdTQMDraft2.Visible = False
                    cmdRestore.Visible = True
                End If

                txtcfbid.Text = ds.Tables(0).Rows(0)("cfb_no").ToString
                txthn.Text = ds.Tables(0).Rows(0)("hn").ToString
                txtdivision.Text = ds.Tables(0).Rows(0)("division").ToString
                lblDeptID.Text = ds.Tables(0).Rows(0)("dept_id_report").ToString
                txtcontact.Text = ds.Tables(0).Rows(0)("report_tel").ToString
                txtcfb_tel.Text = ds.Tables(0).Rows(0)("report_tel2").ToString
                lblReport.Text = ds.Tables(0).Rows(0)("report_by").ToString

                txtdiagnosis.Value = ds.Tables(0).Rows(0)("diagnosis").ToString
                txtoperation.Value = ds.Tables(0).Rows(0)("operation").ToString
                txtlocation.Text = ds.Tables(0).Rows(0)("location").ToString
                Try
                    txtlocation_combo.SelectedValue = ds.Tables("t1").Rows(0)("location").ToString
                    ' txtlocation_combo.Items.FindByValue(ds.Tables("t1").Rows(0)("location").ToString).Selected = True
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try
                txtroom.Text = ds.Tables(0).Rows(0)("room").ToString
                txtdate_complaint.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("datetime_complaint_ts").ToString)

                txtptage.Value = ds.Tables(0).Rows(0)("age").ToString
                txtptsex.SelectedValue = ds.Tables(0).Rows(0)("sex").ToString
                txttitle.SelectedValue = ds.Tables(0).Rows(0)("pt_title").ToString
                txtsegment.SelectedValue = ds.Tables(0).Rows(0)("customer_segment").ToString
                txtservicetype.SelectedValue = ds.Tables(0).Rows(0)("service_type").ToString

                txtdate_report.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("datetime_report_ts").ToString)

              

                txthour.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString, "hour")
                txtmin.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString, "min")
                txtcomplain.Value = ds.Tables(0).Rows(0)("complain_detail").ToString
                txtcountry.Text = ds.Tables(0).Rows(0)("country").ToString
                ' txtdept.SelectedValue = ds.Tables(0).Rows(0)("cfb_dept_id").ToString
                txtcomplain_status.SelectedValue = ds.Tables(0).Rows(0)("complain_status").ToString
                txtfeedback_from.SelectedValue = ds.Tables(0).Rows(0)("feedback_from").ToString
                txtcase_manager.Value = ds.Tables(0).Rows(0)("cfb_case_manager").ToString

                Try
                    txtfeedback_othertopic.SelectedValue = ds.Tables(0).Rows(0)("feedback_topic_id").ToString
                Catch ex As Exception

                End Try

                txtcomplain_remark.Value = ds.Tables(0).Rows(0)("complain_status_remark").ToString
                txtfeedback_remark.Value = ds.Tables(0).Rows(0)("feedback_from_remark").ToString
                txtpart_customer.Value = ds.Tables(0).Rows(0)("part_customer").ToString
                txtpart_hospital.Value = ds.Tables(0).Rows(0)("part_hospital").ToString
                txtpart_employee.Value = ds.Tables(0).Rows(0)("part_employee").ToString

                If ds.Tables(0).Rows(0)("cfb_is_complain").ToString = "1" Then
                    txtcom1.Checked = True
                ElseIf ds.Tables(0).Rows(0)("cfb_is_complain").ToString = "0" Then
                    txtcom2.Checked = True
                End If

                txtcustomer.SelectedValue = ds.Tables(0).Rows(0)("cfb_customer_resp").ToString
                txtcus_detail.Value = ds.Tables(0).Rows(0)("cfb_customer_resp_remark").ToString

                If ds.Tables(0).Rows(0)("cfb_chk_tel").ToString = "1" Then
                   chk_tel.Checked = True
                End If

                If ds.Tables(0).Rows(0)("cfb_chk_email").ToString = "1" Then
                    chk_email.Checked = True
                End If

                If ds.Tables(0).Rows(0)("cfb_chk_other").ToString = "1" Then
                    chk_othter.Checked = True
                End If

                txttel.Value = ds.Tables(0).Rows(0)("cfb_tel_remark").ToString
                txtemail.Value = ds.Tables(0).Rows(0)("cfb_email_remark").ToString
                txtother.Value = ds.Tables(0).Rows(0)("cfb_other_remark").ToString

                txtresponse_unit.SelectedValue = ds.Tables("t1").Rows(0)("new_dept_relate_name").ToString
                txtsafegoal.Text = ds.Tables("t1").Rows(0)("new_safe_goal").ToString
                txtissue.Text = ds.Tables("t1").Rows(0)("new_issue").ToString
                txtpayor.Text = ds.Tables("t1").Rows(0)("new_payor").ToString
                Try
                    txtsevere.SelectedValue = ds.Tables("t1").Rows(0)("new_severe_id").ToString
                Catch ex As Exception

                End Try

                Try
                    txtclinical.SelectedValue = ds.Tables("t1").Rows(0)("new_clinical_type").ToString
                Catch ex As Exception

                End Try

                If ds.Tables(0).Rows(0)("is_lock").ToString = "1" Then
                    txtlock.SelectedIndex = 0
                    lblLockMsg.Text = "Locked by : " & ds.Tables(0).Rows(0)("lock_by").ToString & ", Date : " & ds.Tables(0).Rows(0)("lock_date_raw").ToString

                    Try
                        If CInt(ds.Tables(0).Rows(0)("lock_empcode").ToString) = CInt(Session("emp_code").ToString) Then
                            is_lock = "0"
                        Else
                            is_lock = "1"
                        End If
                    Catch ex As Exception
                        is_lock = "1"
                    End Try

                
                    'cmdSubmit2.Enabled = False
                    'cmdSubmit.Enabled = False
                    'cmdTQMDraft1.Enabled = False
                    'cmdTQMDraft2.Enabled = False
                    'cmdDraft.Enabled = False
                    'cmdDraft2.Enabled = False
                    'cmdTQMView1.Enabled = False
                    'cmdTQMView2.Enabled = False
                Else
                    is_lock = "0"
                    txtlock.SelectedIndex = 1
                    lblLockMsg.Text = ""
                End If

                If ds.Tables(0).Rows(0)("is_lock").ToString = "1" Then
                    lblHeader.Text = "Locked by " & ds.Tables(0).Rows(0)("lock_by").ToString
                Else
                    lblHeader.Text = ""
                End If

                If viewtype = "tqm" And ds.Tables(0).Rows(0)("is_change_to_draft").ToString = "1" Then
                    is_lock = "1"
                End If

                If viewtype = "tqm" And ds.Tables(0).Rows(0)("is_move_to_ir").ToString = "1" Then
                    is_lock = "1"
                End If

                If viewtype = "psm" Or viewtype = "tqm" Then
                    ' Response.Write(22222 & ds.Tables(0).Rows(0)("psm_status_id").ToString)
                    txtpsm_compensation.Value = ds.Tables(0).Rows(0)("psm_compensation").ToString
                    txtpsm_diagnonsis.Text = ds.Tables(0).Rows(0)("psm_dianosis").ToString
                    txtpsm_recommend.Value = ds.Tables(0).Rows(0)("psm_recommendation").ToString
                    txtpsm_remark.Value = ds.Tables(0).Rows(0)("psm_remark").ToString
                    txtpsm_date_expect.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("date_close_ts").ToString)

                    Try
                        txtpsm_status.SelectedValue = ds.Tables(0).Rows(0)("psm_status_id").ToString
                    Catch ex As Exception

                    End Try


                    If ds.Tables(0).Rows(0)("psm_is_legal").ToString = "1" Then
                        chk_psm_refer.Checked = True
                    Else
                        chk_psm_refer.Checked = False
                    End If

                    For i As Integer = 1 To 6
                        If ds.Tables(0).Rows(0)("psm_chk_resolution" & i).ToString = "1" Then
                            CType(panel_psm.FindControl("chk_reso" & i), HtmlInputCheckBox).Checked = True
                        Else
                            CType(panel_psm.FindControl("chk_reso" & i), HtmlInputCheckBox).Checked = False
                        End If
                    Next i

                    For i As Integer = 1 To 4
                        If ds.Tables(0).Rows(0)("psm_pt_satisfaction").ToString = i.ToString Then
                            CType(panel_psm.FindControl("txtpsm_pt" & i), RadioButton).Checked = True
                        Else
                            CType(panel_psm.FindControl("txtpsm_pt" & i), RadioButton).Checked = False
                        End If
                    Next i

                    txtpsm_response.Value = ds.Tables(0).Rows(0)("psm_person").ToString
                End If

            Catch ex As Exception
                Response.Write(ex.Message & ":" & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindNational()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM m_national"
                sql &= " ORDER BY national_name ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")
                '   Response.Write(sql)
                '  txtcountry.DataSource = ds
                ' txtcountry.DataBind()

              
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindDefendantUnitCombo()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM m_dept_unit"
                sql &= " ORDER BY dept_unit_name ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                '   Response.Write(sql)
                txtadd_doctor_defentdant_unit.DataSource = ds
                txtadd_doctor_defentdant_unit.DataBind()

                txtadd_doctor_defentdant_unit.SelectedValue = 139

                txtadd_cause_defendant.DataSource = ds
                txtadd_cause_defendant.DataBind()
                txtadd_cause_defendant.Items.Insert(0, New ListItem("", 0))
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub


        Sub bindRelateDept_Combo()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT a.dept_id , a.dept_name FROM cbf_relate_dept a INNER JOIN cfb_comment_list b ON a.comment_id = b.comment_id "
                sql &= " WHERE b.ir_id =  " & cfbId
                sql &= " GROUP BY a.dept_id , a.dept_name"
                ' Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                txtdepttab_combo.DataSource = ds
                txtdepttab_combo.DataBind()

                ' txtdepttab_combo.Items.Insert(0, New ListItem("-- Please Select --", ""))

                If txtdepttab_combo.Items.Count > 0 Then
                    'txtdepttab_combo.SelectedValue = Session("dept_id").ToString
                    bindDeptCase()
                    '  txtdepttab_combo.Enabled = False
                End If
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindDeptCase()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT a.cfb_relate_dept_id , '#' + CAST(b.order_sort AS varchar) + ' ' + b.comment_type_name AS order_sort   FROM cbf_relate_dept a INNER JOIN cfb_comment_list b ON a.comment_id = b.comment_id "
                sql &= " WHERE a.dept_id =  '" & txtdepttab_combo.SelectedValue & "'"
                sql &= " AND b.ir_id = " & cfbId
                sql &= " ORDER BY b.order_sort ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                txtdeptcase.DataSource = ds
                txtdeptcase.DataBind()

                If ds.Tables(0).Rows.Count > 0 Then
                    ' txtdepttab_comment_type.Text = ds.Tables(0).Rows(0)("comment_type_name").ToString
                End If
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindTQMCase()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM cfb_comment_list WHERE ir_id = " & cfbId

                sql &= " ORDER BY order_sort"
                ds = conn.getDataSetForTransaction(sql, "t1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                txttqmcase.DataSource = ds
                txttqmcase.DataBind()

                'txtdeptcase.DataSource = ds
                ' txtdeptcase.DataBind()

                If ds.Tables(0).Rows.Count > 0 Then
                    txttqm_complaintype.Text = ds.Tables(0).Rows(0)("comment_type_name").ToString
                    '    txtdepttab_comment_type.Text = ds.Tables(0).Rows(0)("comment_type_name").ToString
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindTQMTab(ByVal comment_id As String)
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM cfb_comment_list WHERE comment_id = " & comment_id
                'Response.Write(sql)

                ds = conn.getDataSetForTransaction(sql, "t1")

                '  Response.Write(1)
                If viewtype = "tqm" Or viewtype = "ha" Then ' Bind TQM Tab

                    lblTQMCommentDetail.Text = ds.Tables(0).Rows(0)("comment_detail").ToString.Replace(vbCrLf, "<br/>")
                    txttqm_owner.Text = ds.Tables(0).Rows(0)("tqm_owner").ToString
                    txttqm_complaintype.Text = ds.Tables(0).Rows(0)("comment_type_name").ToString
                    ' Response.Write("xx : " & ds.Tables(0).Rows(0)("grand_topic").ToString & " ||")
                    'If ds.Tables(0).Rows(0)("grand_topic").ToString <> "0" Then
                    'Response.Write(2)

                    txtrelated_standard.Text = ds.Tables(0).Rows(0)("related_standard").ToString

                    Try
                        txtgrandtopic.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("grand_topic").ToString)
                    Catch ex As Exception

                    End Try

                    'End If

                    bindComboNormalTopic()
                    Try
                        txtnormaltopic.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("topic").ToString)
                    Catch ex As Exception

                    End Try

                    bindComboSubTopic("")

                    Try
                        txtsubtopic1.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("subtopic1").ToString)
                    Catch ex As Exception

                    End Try

                    bindComboSubTopic2("")

                    Try
                        txtsubtopic2.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("subtopic2").ToString)
                    Catch ex As Exception

                    End Try

                    bindComboSubTopic3("")

                    Try
                        txtsubtopic3.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("subtopic3").ToString)
                    Catch ex As Exception

                    End Try

                    txttqmcfb_detail.Text = ds.Tables(0).Rows(0)("tqm_topic_detail").ToString()

                    If ds.Tables(0).Rows(0)("tqm_chk_feedback1").ToString = "1" Then
                        chk_feedback1.Checked = True
                    Else
                        chk_feedback1.Checked = False
                    End If
                    If ds.Tables(0).Rows(0)("tqm_chk_feedback2").ToString = "1" Then
                        chk_feedback2.Checked = True
                    Else
                        chk_feedback2.Checked = False
                    End If
                    txtfeedback_within.SelectedValue = ds.Tables(0).Rows(0)("tqm_feedback_within").ToString

                    If ds.Tables(0).Rows(0)("chk_tqm_contact").ToString = "1" Then
                        chk_tqm_contact.Checked = True
                    Else
                        chk_tqm_contact.Checked = False
                    End If
                    If ds.Tables(0).Rows(0)("chk_tqm_other").ToString = "1" Then
                        chk_tqmother.Checked = True
                    Else
                        chk_tqmother.Checked = False
                    End If
                    txtmedthod.SelectedValue = ds.Tables(0).Rows(0)("tqm_method").ToString
                    txttqm_date.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("tqm_date_ts").ToString)
                    Try
                        txthour2.SelectedValue = ConvertTSTo(ds.Tables(0).Rows(0)("tqm_date_ts").ToString, "hour")
                        ' Response.Write(ConvertTSTo(ds.Tables(0).Rows(0)("tqm_date_ts").ToString, "hour"))
                        txtmin2.SelectedValue = ConvertTSTo(ds.Tables(0).Rows(0)("tqm_date_ts").ToString, "min")
                    Catch ex As Exception
                        txthour2.SelectedValue = "0"
                        ' Response.Write(ConvertTSTo(ds.Tables(0).Rows(0)("tqm_date_ts").ToString, "hour"))
                        txtmin2.SelectedValue = "0"
                    End Try

                    txtduration.Value = ds.Tables(0).Rows(0)("tqm_duration").ToString
                    txt_response.Text = ds.Tables(0).Rows(0)("tqm_response").ToString
                    txttqm_detail.Value = ds.Tables(0).Rows(0)("tqm_detail").ToString
                    txttqm_customer.SelectedValue = ds.Tables(0).Rows(0)("tqm_customer").ToString
                    txttqm_customer_detail.Value = ds.Tables(0).Rows(0)("tqm_customer_detail").ToString

                    txtreporttype.SelectedValue = ds.Tables(0).Rows(0)("tqm_report_type").ToString
                    txttqm_cfbno.Text = ds.Tables(0).Rows(0)("tqm_cfb_no").ToString

                    txttqm_concern.SelectedValue = ds.Tables(0).Rows(0)("tqm_concern").ToString
                    txttqm_clinic.SelectedValue = ds.Tables(0).Rows(0)("tqm_clinic").ToString
                    txttqm_service.SelectedValue = ds.Tables(0).Rows(0)("tqm_service").ToString

                    Try
                        If CInt(ds.Tables(0).Rows(0)("is_tqm_risk").ToString) = 1 Then
                            ' Response.Write("xxx")
                            txtrisk1.Checked = True
                            txtrisk2.Checked = False
                        ElseIf CInt(ds.Tables(0).Rows(0)("is_tqm_risk").ToString) = 0 Then
                            '  Response.Write("zzz")
                            txtrisk1.Checked = False
                            txtrisk2.Checked = True
                        Else
                            ' Response.Write("yyyy")
                            txtrisk1.Checked = False
                            txtrisk2.Checked = False
                        End If

                        If CInt(ds.Tables(0).Rows(0)("is_tqm_imco").ToString) = 1 Then
                            txtimco1.Checked = True
                            txtimco2.Checked = False
                        ElseIf CInt(ds.Tables(0).Rows(0)("is_tqm_imco").ToString) = 0 Then
                            txtimco1.Checked = False
                            txtimco2.Checked = True
                        Else
                            txtimco1.Checked = False
                            txtimco2.Checked = False
                        End If
                    Catch ex As Exception

                    End Try
                
                    '  Response.Write(34)
                    '
                    'txtrisk1.Checked = True
                

                    txttqm_remark.Value = ds.Tables(0).Rows(0)("tqm_remark").ToString
                    txtrefno.Text = ds.Tables(0).Rows(0)("tqm_ref_no").ToString

                    If ds.Tables(0).Rows(0)("log_group_report_id").ToString = "1" Then
                        txtrepgr1.Checked = True
                        txtrepgr2.Checked = False
                    ElseIf ds.Tables(0).Rows(0)("log_group_report_id").ToString = "2" Then
                        txtrepgr1.Checked = False
                        txtrepgr2.Checked = True
                    End If
                  
                    If ds.Tables(0).Rows(0)("log_service1_id").ToString = "1" Then
                        txtsrcritia1.Checked = True
                        txtsrcritia2.Checked = False
                    ElseIf ds.Tables(0).Rows(0)("log_service1_id").ToString = "2" Then
                        txtsrcritia1.Checked = False
                        txtsrcritia2.Checked = True
                    End If

                    If ds.Tables(0).Rows(0)("log_service2_id").ToString = "1" Then
                        txtsrsuccess1.Checked = True
                        txtsrsuccess2.Checked = False
                        txtsrsuccess3.Checked = False
                    ElseIf ds.Tables(0).Rows(0)("log_service2_id").ToString = "2" Then
                        txtsrsuccess1.Checked = False
                        txtsrsuccess2.Checked = True
                        txtsrsuccess3.Checked = False
                    ElseIf ds.Tables(0).Rows(0)("log_service2_id").ToString = "3" Then
                        txtsrsuccess1.Checked = False
                        txtsrsuccess2.Checked = False
                        txtsrsuccess3.Checked = True
                    End If

                    txtlog_successby.SelectedValue = ds.Tables(0).Rows(0)("log_success_by").ToString

                    If ds.Tables(0).Rows(0)("log_owner_id").ToString = "1" Then
                        txtowner1.Checked = True
                        txtowner2.Checked = False
                        txtowner3.Checked = False
                    ElseIf ds.Tables(0).Rows(0)("log_owner_id").ToString = "2" Then
                        txtowner1.Checked = False
                        txtowner2.Checked = True
                        txtowner3.Checked = False
                    ElseIf ds.Tables(0).Rows(0)("log_owner_id").ToString = "3" Then
                        txtowner1.Checked = False
                        txtowner2.Checked = False
                        txtowner3.Checked = True
                    End If

                    successwithin.Value = ds.Tables(0).Rows(0)("log_success_within").ToString

                    If ds.Tables(0).Rows(0)("log_new_pt_id").ToString = "1" Then
                        txtnewpt1.Checked = True
                        txtnewpt2.Checked = False
                    ElseIf ds.Tables(0).Rows(0)("log_new_pt_id").ToString = "2" Then
                        txtnewpt1.Checked = False
                        txtnewpt2.Checked = True
                    End If

                    serviceplace.Value = ds.Tables(0).Rows(0)("log_service_place").ToString

                    If ds.Tables(0).Rows(0)("log_other_hosp_id").ToString = "1" Then
                        txtnewhosp1.Checked = True
                        txtnewhosp2.Checked = False
                    ElseIf ds.Tables(0).Rows(0)("log_other_hosp_id").ToString = "2" Then
                        txtnewhosp1.Checked = False
                        txtnewhosp2.Checked = True
                    End If

                    newhospname.Value = ds.Tables(0).Rows(0)("log_refer_hosp_name").ToString
                    txtlog_customer_object.SelectedValue = ds.Tables(0).Rows(0)("log_customer_objective").ToString
                    txtlog_resolution.SelectedValue = ds.Tables(0).Rows(0)("log_resolution").ToString
                    txtlog_action.SelectedValue = ds.Tables(0).Rows(0)("log_action_taken").ToString

                    If ds.Tables(0).Rows(0)("log_qc_id").ToString = "1" Then
                        txtqc1.Checked = True
                        txtqc2.Checked = False
                    ElseIf ds.Tables(0).Rows(0)("log_qc_id").ToString = "2" Then
                        txtqc1.Checked = False
                        txtqc2.Checked = True
                    End If
                    txtoutcome.SelectedValue = ds.Tables(0).Rows(0)("log_outcome_id").ToString

                    txttqm_factor.Value = ds.Tables(0).Rows(0)("log_factor").ToString
                    txttqm_corrective.Value = ds.Tables(0).Rows(0)("log_corrective_action").ToString

                    If ds.Tables(0).Rows(0)("is_followup_case").ToString = "1" Then
                        txttqm_followup.Checked = True
                    Else
                        txttqm_followup.Checked = False
                    End If
                    txttqm_follow_remark.Text = ds.Tables(0).Rows(0)("followup_remark").ToString
                    txttqm_follow_date.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("followup_date_ts").ToString)
                    ' sql &= " , log_person =  '" & txtlog_person.SelectedValue & "'"
                    ' sql &= " , log_area_code =  '" & txtlog_areacode.SelectedValue & "'"
                    txtlog_person.SelectedValue = ds.Tables(0).Rows(0)("log_person").ToString
                    txtlog_areacode.SelectedValue = ds.Tables(0).Rows(0)("log_area_code").ToString

                    If ds.Tables("t1").Rows(0)("tqm_chk_write").ToString = "1" Then
                        chk_write.Checked = True
                    Else
                        chk_write.Checked = False
                    End If

                    If ds.Tables("t1").Rows(0)("tqm_chk_remove").ToString = "1" Then
                        chk_remove.Checked = True
                    Else
                        chk_remove.Checked = False
                    End If

                    If ds.Tables("t1").Rows(0)("tqm_chk_refund").ToString = "1" Then
                        chk_refund.Checked = True
                    Else
                        chk_refund.Checked = False
                    End If

                    txtwrite_bath.Text = ds.Tables("t1").Rows(0)("tqm_write_bath").ToString
                    txtremove_bath.Text = ds.Tables("t1").Rows(0)("tqm_remove_bath").ToString
                    txtrefund_bath.Text = ds.Tables("t1").Rows(0)("tqm_refund_bath").ToString

                    Try
                        ' Response.Write(ds.Tables("t1").Rows(0)("tqm_write_dept_id").ToString)
                        '  If ds.Tables("t1").Rows(0)("tqm_write_dept_id").ToString <> "" Then
                        txtwrite_dept.SelectedValue = ds.Tables("t1").Rows(0)("tqm_write_dept_id").ToString
                        ' End If
                        '  txtwrite_dept.SelectedValue = ds.Tables("t1").Rows(0)("tqm_write_dept_id").ToString
                    Catch ex As Exception

                    End Try

                    Try
                        txtrefund_dept.SelectedValue = ds.Tables("t1").Rows(0)("tqm_refund_dept_id").ToString
                    Catch ex As Exception

                    End Try

                    Try
                        txtremove_dept.SelectedValue = ds.Tables("t1").Rows(0)("tqm_remove_dept_id").ToString
                    Catch ex As Exception

                    End Try

                    If ds.Tables("t1").Rows(0)("tqm_chk_bi_way").ToString = "1" Then
                        txtbiway1.Checked = True
                    Else
                        txtbiway1.Checked = False
                    End If

                    Try
                        txtbiway.SelectedValue = ds.Tables("t1").Rows(0)("tqm_recognition_id").ToString
                    Catch ex As Exception

                    End Try

                    ' sql &= " , tqm_recognition_id = '" & txtbiway.SelectedValue & "' "
                    '  sql &= " , tqm_recognition_name = '" & txtbiway.SelectedItem.Text & "' "

                    sql &= " , related_standard = '" & addslashes(txtrelated_standard.Text) & "'"

                End If

            Catch ex As Exception
                Response.Write("bindTQMTab : " & ex.Message & ":" & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub updateTQMTab()
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""

            If txttqmcase.Items.Count = 0 Then
                Return
            End If

            sql = "UPDATE cfb_comment_list SET session_id = '' "
            If txtstatus.SelectedValue <> "2" Then
                sql &= " , tqm_owner = '" & txttqm_owner.Text & "'"
            End If
            If chk_feedback1.Checked = True Then
                sql &= " , tqm_chk_feedback1 = 1 "
            Else
                sql &= " , tqm_chk_feedback1 = 0 "
            End If
            If chk_feedback2.Checked = True Then
                sql &= " , tqm_chk_feedback2 = 1 "
            Else
                sql &= " , tqm_chk_feedback2 = 0 "
            End If

            sql &= " , tqm_feedback_within = '" & txtfeedback_within.SelectedValue & "'"
            sql &= " , grand_topic = '" & txtgrandtopic.SelectedValue & "'"
            sql &= " , topic = '" & txtnormaltopic.SelectedValue & "'"
            sql &= " , subtopic1 = '" & txtsubtopic1.SelectedValue & "'"
            sql &= " , subtopic2 = '" & txtsubtopic2.SelectedValue & "'"
            sql &= " , subtopic3 = '" & txtsubtopic3.SelectedValue & "'"
            sql &= " , tqm_topic_detail = '" & txttqmcfb_detail.Text & "'"

            If chk_tqm_contact.Checked = True Then
                sql &= " , chk_tqm_contact = 1 "
            Else
                sql &= " , chk_tqm_contact = 0 "
            End If
            If chk_tqmother.Checked = True Then
                sql &= " , chk_tqm_other = 1 "
            Else
                sql &= " , chk_tqm_other = 0 "
            End If

            sql &= " , tqm_method = '" & txtmedthod.SelectedValue & "'"
            sql &= " , tqm_date = '" & convertToSQLDatetime(txttqm_date.Text, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "'"
            sql &= " , tqm_date_ts = '" & ConvertDateStringToTimeStamp(txttqm_date.Text, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & "'"
            sql &= " , tqm_duration = '" & txtduration.Value & "'"

            sql &= " , tqm_detail = '" & addslashes(txttqm_detail.Value) & "'"
            sql &= " , tqm_customer = '" & txttqm_customer.SelectedValue & "'"
            sql &= " , tqm_customer_detail = '" & addslashes(txttqm_customer_detail.Value) & "'"
          
            sql &= " , tqm_report_type = '" & txtreporttype.SelectedValue & "'"
            sql &= " , tqm_cfb_no = '" & txttqm_cfbno.Text & "'"

            sql &= " , tqm_concern = '" & txttqm_concern.SelectedValue & "'"
            sql &= " , tqm_clinic = '" & txttqm_clinic.SelectedValue & "'"
            sql &= " , tqm_service = '" & txttqm_service.SelectedValue & "'"
            If txtrisk1.Checked = True Then
                sql &= " , is_tqm_risk = 1 "
            Else
                sql &= " , is_tqm_risk = 0 "
            End If
            If txtimco1.Checked = True Then
                sql &= " , is_tqm_imco = 1 "
            Else
                sql &= " , is_tqm_imco = 0 "
            End If

            sql &= " , tqm_remark = '" & txttqm_remark.Value & "'"
            sql &= " , tqm_ref_no = '" & txtrefno.Text & "'"

            If txtrepgr1.Checked = True Then
                sql &= " , log_group_report_id = 1 "
                sql &= " , log_group_report_name =  'Direct' "
            ElseIf txtrepgr2.Checked = True Then
                sql &= " , log_group_report_id = 2 "
                sql &= " , log_group_report_name =  'Indirect' "
            End If

            If txtsrcritia1.Checked = True Then
                sql &= " , log_service1_id = 1 "
                sql &= " , log_service1_name =  'Yes' "
            ElseIf txtsrcritia2.Checked = True Then
                sql &= " , log_service1_id = 2 "
                sql &= " , log_service1_name =  'No' "
            End If

            If txtsrsuccess1.Checked = True Then
                sql &= " , log_service2_id = 1 "
                sql &= " , log_service2_name =  'Yes' "
            ElseIf txtsrsuccess2.Checked = True Then
                sql &= " , log_service2_id = 2 "
                sql &= " , log_service2_name =  'No' "
            ElseIf txtsrsuccess3.Checked = True Then
                sql &= " , log_service2_id = 3 "
                sql &= " , log_service2_name =  'N/A' "
            End If

            sql &= " , log_success_by =  '" & txtlog_successby.SelectedValue & "'"
            If txtowner1.Checked = True Then
                sql &= " , log_owner_id = 1 "
                sql &= " , log_owner_name =  'Yes' "
            ElseIf txtowner2.Checked = True Then
                sql &= " , log_owner_id = 2 "
                sql &= " , log_owner_name =  'No' "
            ElseIf txtowner3.Checked = True Then
                sql &= " , log_owner_id = 3 "
                sql &= " , log_owner_name =  'N/A' "
            End If

            sql &= " , log_success_within =  '" & addslashes(successwithin.Value) & "'"

            If txtnewpt1.Checked = True Then
                sql &= " , log_new_pt_id = 1 "
                sql &= " , log_new_pt_name =  'Yes' "
            ElseIf txtnewpt2.Checked = True Then
                sql &= " , log_new_pt_id = 2 "
                sql &= " , log_new_pt_name =  'No' "
            End If

            sql &= " , log_service_place =  '" & addslashes(serviceplace.Value) & "'"

            If txtnewhosp1.Checked = True Then
                sql &= " , log_other_hosp_id = 1 "
                sql &= " , log_other_hosp_name =  'Yes' "
            ElseIf txtnewhosp2.Checked = True Then
                sql &= " , log_other_hosp_id = 2 "
                sql &= " , log_other_hosp_name =  'No' "
            End If

            sql &= " , log_refer_hosp_name =  '" & addslashes(newhospname.Value) & "'"
            sql &= " , log_customer_objective =  '" & addslashes(txtlog_customer_object.SelectedValue) & "'"
            sql &= " , log_resolution =  '" & addslashes(txtlog_resolution.SelectedValue) & "'"
            sql &= " , log_action_taken =  '" & addslashes(txtlog_action.SelectedValue) & "'"

            sql &= " , log_outcome_id =  '" & txtoutcome.SelectedValue & "'"
            sql &= " , log_outcome_name =  '" & addslashes(txtoutcome.SelectedItem.Text) & "'"

            If txtqc1.Checked = True Then
                sql &= " , log_qc_id = 1 "
                sql &= " , log_qc_name =  'Yes' "
            ElseIf txtqc2.Checked = True Then
                sql &= " , log_qc_id = 2 "
                sql &= " , log_qc_name =  'No' "
            End If

            sql &= " , log_factor =  '" & addslashes(txttqm_factor.Value) & "'"
            sql &= " , log_corrective_action =  '" & addslashes(txttqm_corrective.Value) & "'"

            If txttqm_followup.Checked = True Then
                sql &= " , is_followup_case = 1 "
            Else
                sql &= " , is_followup_case = 0 "
            End If

            sql &= " , followup_remark =  '" & addslashes(txttqm_follow_remark.Text) & "'"
            sql &= " , followup_date_ts =  '" & ConvertDateStringToTimeStamp(txttqm_follow_date.Text) & "'"
            sql &= " , followup_date =  '" & convertToSQLDatetime(txttqm_follow_date.Text) & "'"

            sql &= " , log_person =  '" & txtlog_person.SelectedValue & "'"
            sql &= " , log_area_code =  '" & txtlog_areacode.SelectedValue & "'"

            If chk_write.Checked = True Then
                sql &= " , tqm_chk_write = 1 "
            Else
                sql &= " , tqm_chk_write = 0 "
            End If

            If chk_remove.Checked = True Then
                sql &= " , tqm_chk_remove = 1 "
            Else
                sql &= " , tqm_chk_remove = 0 "
            End If

            If chk_refund.Checked = True Then
                sql &= " , tqm_chk_refund = 1 "
            Else
                sql &= " , tqm_chk_refund = 0 "
            End If

            ' sql &= " , tqm_chk_remove = '" & txtreporttype.SelectedItem.Text & "' "
            sql &= " , tqm_write_bath = '" & txtwrite_bath.Text & "' "
            sql &= " , tqm_remove_bath = '" & txtremove_bath.Text & "' "
            sql &= " , tqm_refund_bath = '" & txtrefund_bath.Text & "' "

            sql &= " , tqm_write_dept_id = '" & txtwrite_dept.SelectedValue & "' "
            sql &= " , tqm_refund_dept_id = '" & txtrefund_dept.SelectedValue & "' "
            sql &= " , tqm_remove_dept_id = '" & txtremove_dept.SelectedValue & "' "
            sql &= " , tqm_write_dept_name = '" & addslashes(txtwrite_dept.SelectedItem.Text) & "' "
            sql &= " , tqm_refund_dept_name = '" & addslashes(txtrefund_dept.SelectedItem.Text) & "' "
            sql &= " , tqm_remove_dept_name = '" & addslashes(txtremove_dept.SelectedItem.Text) & "' "

            If txtbiway1.Checked = True Then
                sql &= " , tqm_chk_bi_way = 1 "
            Else
                sql &= " , tqm_chk_bi_way = 0 "
            End If

            sql &= " , tqm_recognition_id = '" & txtbiway.SelectedValue & "' "
            sql &= " , tqm_recognition_name = '" & txtbiway.SelectedItem.Text & "' "

            sql &= " WHERE comment_id = " & txttqmcase.SelectedValue

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sql)
            End If


            sql = "UPDATE cfb_comment_list SET "
            If chk_tqm_contact.Checked = True Then
                sql &= " chk_tqm_contact = 1 "
            Else
                sql &= " chk_tqm_contact = 0 "
            End If
            If chk_tqmother.Checked = True Then
                sql &= " , chk_tqm_other = 1 "
            Else
                sql &= " , chk_tqm_other = 0 "
            End If

            sql &= " , tqm_method = '" & txtmedthod.SelectedValue & "'"
            sql &= " , tqm_date = '" & convertToSQLDatetime(txttqm_date.Text, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "'"
            sql &= " , tqm_date_ts = '" & ConvertDateStringToTimeStamp(txttqm_date.Text, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & "'"
            sql &= " , tqm_duration = '" & txtduration.Value & "'"
            sql &= " , tqm_response = '" & addslashes(txt_response.Text) & "'"
            sql &= " WHERE ir_id = " & cfbId

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sql)
            End If
        End Sub

        Sub updateDeptTab()
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""

            If txtdeptcase.SelectedValue = "" Then
                Return
            End If

            sql = "UPDATE cbf_relate_dept SET dept_last_update = GETDATE() "
            If dept_chk_feedback1.Checked = True Then
                sql &= " , dept_chk_feedback1 = 1 "
            Else
                sql &= " , dept_chk_feedback1 = 0 "
            End If

            If dept_chk_feedback2.Checked = True Then
                sql &= " , dept_chk_feedback2 = 1 "
            Else
                sql &= " , dept_chk_feedback2 = 0 "
            End If

            sql &= " , dept_feedback_within =  " & dept_feedback_within.SelectedValue

            sql &= " , dept_investigation = '" & addslashes(txtinvestigation.Value) & "'"
            sql &= " , dept_cause = '" & addslashes(txtcause.Value) & "'"
            sql &= " , dept_corrective = '" & addslashes(txtcorrective.Value) & "'"

            sql &= " , dept_result = '" & addslashes(txtresult.SelectedValue) & "'"
            sql &= " , dept_result_detail = '" & addslashes(txtresult_detail.Value) & "'"

            sql &= " , dept_priority = '" & addslashes(txtdept_priority.SelectedValue) & "'"

            If viewtype = "dept" Then
                sql &= " , investigate_by = '" & addslashes(Session("user_fullname").ToString) & "'"
                sql &= " , investigate_date_ts = '" & Date.Now.Ticks & "'"
                sql &= " , investigate_date = GETDATE() "
            End If
           


            sql &= " WHERE cfb_relate_dept_id = " & txtdeptcase.SelectedValue

            'Response.Write(sql)
            '  Response.End()
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then

                Throw New Exception(errorMsg & ":" & sql)
            End If

          

        End Sub

        Sub bindDept() ' Combo box
            Dim ds As New DataSet
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "SELECT * FROM user_dept ORDER BY dept_name_en"
                'sql &= " ORDER BY dept_name"
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                txtpsm_add_dept.DataSource = ds
                txtpsm_add_dept.DataBind()

                txtpsm_add_dept.Items.Insert(0, New ListItem("--Please select--", ""))

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindDeptGridCause()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
              
                GridDeptCause.DataSource = Nothing
                GridDeptCause.DataBind()

                sql = "SELECT * FROM cfb_dept_cause_list WHERE cfb_relate_dept_id = " & txtdeptcase.SelectedValue

                '  sql &= " ORDER BY order_sort"
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                If ds.Tables(0).Rows.Count = 0 Then


                    ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
                    GridDeptCause.DataSource = ds
                    GridDeptCause.DataBind()

                    Dim columnCount As Integer = GridDeptCause.Rows(0).Cells.Count
                    GridDeptCause.Rows(0).Cells.Clear()
                    GridDeptCause.Rows(0).Cells.Add(New TableCell)
                    GridDeptCause.Rows(0).Cells(0).ColumnSpan = columnCount
                    GridDeptCause.Rows(0).Cells(0).Text = "No Records Found."
                Else
                    GridDeptCause.DataSource = ds
                    GridDeptCause.DataBind()
                End If



            Catch ex As Exception
                Response.Write("GridDeptCause : " & ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindDeptTab()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM cfb_comment_list a INNER JOIN cbf_relate_dept b ON a.comment_id = b.comment_id  "
                sql &= "LEFT OUTER JOIN  ir_topic_grand t1 ON a.grand_topic = t1.grand_topic_id "
                sql &= "LEFT OUTER JOIN ir_topic t2 ON a.topic = t2.ir_topic_id "
                sql &= "LEFT OUTER JOIN ir_topic_sub t3 ON a.subtopic1 = t3.ir_subtopic_id "
                sql &= "LEFT OUTER JOIN ir_topic_sub2 t4 ON a.subtopic2 = t4.ir_subtopic2_id "
                sql &= "LEFT OUTER JOIN ir_topic_sub3 t5 ON a.subtopic3 = t5.ir_subtopic3_id "
                sql &= " WHERE 1 = 1 AND b.cfb_relate_dept_id = " & txtdeptcase.SelectedValue

                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                lblDeptCommentDetail.Text = ds.Tables(0).Rows(0)("comment_detail").ToString.Replace(vbCrLf, "<br/>")

                If ds.Tables(0).Rows(0)("dept_chk_feedback1").ToString = "1" Then
                    dept_chk_feedback1.Checked = True
                Else
                    dept_chk_feedback1.Checked = False
                End If

                If ds.Tables(0).Rows(0)("dept_chk_feedback2").ToString = "1" Then
                    dept_chk_feedback2.Checked = True
                Else
                    dept_chk_feedback2.Checked = False
                End If

                dept_feedback_within.SelectedValue = ds.Tables(0).Rows(0)("dept_feedback_within").ToString


                txtinvestigation.Value = ds.Tables(0).Rows(0)("dept_investigation").ToString
                txtcause.Value = ds.Tables(0).Rows(0)("dept_cause").ToString
                txtcorrective.Value = ds.Tables(0).Rows(0)("dept_corrective").ToString
                txtresult.SelectedValue = ds.Tables(0).Rows(0)("dept_result").ToString

                txtresult_detail.Value = ds.Tables(0).Rows(0)("dept_result_detail").ToString
                lblGrandTopic.Text = ds.Tables(0).Rows(0)("grand_topic_name").ToString
                lblTopic.Text = ds.Tables(0).Rows(0)("topic_name").ToString
                lblSubtopic1.Text = ds.Tables(0).Rows(0)("subtopic_name").ToString
                lblSubtopic2.Text = ds.Tables(0).Rows(0)("subtopic2_name_en").ToString
                lblSubtopic3.Text = ds.Tables(0).Rows(0)("subtopic3_name_en").ToString

                '   Response.Write(txtdeptcase.SelectedValue)
                If ds.Tables(0).Rows(0)("is_investigate").ToString = "1" Then
                    lblDeptStatus.Text = "Investigated by " & ds.Tables(0).Rows(0)("investigate_by").ToString & " " & ds.Tables(0).Rows(0)("investigate_date").ToString()
                    lblDeptStatus.ForeColor = Drawing.Color.Green
                    lblDeptStatusID.Text = ds.Tables(0).Rows(0)("is_investigate").ToString
                Else
                    If txtstatus.SelectedValue = 4 Then
                        lblDeptStatus.Text = "Waiting for investigation"
                        lblDeptStatus.ForeColor = Drawing.Color.Red
                        lblDeptStatusID.Text = "0"
                    End If
                End If

                If viewtype = "dept" Then

                    '  If txtdepttab_combo.SelectedValue <> Session("dept_id").ToString Or ds.Tables(0).Rows(0)("is_investigate").ToString() = "1" Then

                    If findArrayValue(costcenter_list, txtdepttab_combo.SelectedValue) = False Or ds.Tables(0).Rows(0)("is_investigate").ToString() = "1" Then
                        cmdTQMDraft2.Enabled = False
                        cmdTQMDraft1.Enabled = False
                        cmdDeptReturn1.Enabled = False
                        cmdDeptReturn2.Enabled = False

                        cmdAddPrevent.Enabled = False
                        cmdPreventOrder.Enabled = False
                    Else
                        cmdTQMDraft2.Enabled = True
                        cmdTQMDraft1.Enabled = True
                        cmdDeptReturn1.Enabled = True
                        cmdDeptReturn2.Enabled = True

                        cmdAddPrevent.Enabled = True
                        cmdPreventOrder.Enabled = True
                    End If
                End If

                Try
                    If ds.Tables(0).Rows(0)("dept_priority").ToString <> "" Then
                        txtdept_priority.SelectedValue = ds.Tables(0).Rows(0)("dept_priority").ToString
                    Else
                        txtdept_priority.SelectedIndex = 0
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message & "BindDeptTab")
                End Try
              

            Catch ex As Exception
                Response.Write(ex.Message & ":" & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindInfoDepartment_Select()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM user_dept ORDER BY dept_name_en ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")
                ' Response.Write(sql)
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                txtinfo_dept1.DataSource = ds
                txtinfo_dept1.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message & ":" & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindInfoDepartment_Grant()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * , dept_name AS dept_name_en FROM ir_dept_inform_update WHERE ir_id = " & cfbId
                sql &= " ORDER BY dept_name ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                txtinfo_dept2.DataSource = ds
                txtinfo_dept2.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message & ":" & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindDefendantUnit_Select()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                'sql = "SELECT * FROM user_dept ORDER BY dept_name_en ASC"
                sql = "SELECT * FROM m_dept_unit a WHERE dept_unit_id NOT IN (SELECT dept_unit_id FROM ir_cfb_unit_defendant WHERE comment_id = " & txttqmcase.SelectedValue & " AND ir_id = " & cfbId & " ) "
                sql &= " ORDER BY a.dept_unit_name"
                ' sql &= " ORDER BY a.dept_name_en ASC"
                ' Response.Write(sql)
                'Return
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                txtunit_defandent_all.DataSource = ds
                txtunit_defandent_all.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message & ":" & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindDefendantUnit_Grant()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try

                sql = "SELECT * FROM ir_cfb_unit_defendant a WHERE 1 = 1 AND a.ir_id = " & cfbId
                sql &= " AND a.comment_id = " & txttqmcase.SelectedValue
                sql &= " ORDER BY a.dept_unit_name"
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                ' Response.Write(sql)
                ' Response.Write(sql)
                'Return
                txtunit_defandent_select.DataSource = ds
                txtunit_defandent_select.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message & ":" & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub updateInfoDepartment() ' Information Upate
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            sql = "DELETE FROM ir_dept_inform_update WHERE ir_id = " & cfbId
      
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If
            '  Response.Write("tttttt")
            For i As Integer = 0 To txtinfo_dept2.Items.Count - 1

                pk = getPK("info_id", "ir_dept_inform_update", conn)
                sql = "INSERT INTO ir_dept_inform_update (info_id , ir_id , dept_id , costcenter_id , dept_name , costcenter_name) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & cfbId & "' ,"
                sql &= " '" & txtinfo_dept2.Items(i).Value & "' ,"
                '  Response.Write("111")
                sql &= " '" & txtinfo_dept2.Items(i).Value & "' ,"
                '  Response.Write("222")
                sql &= " '" & txtinfo_dept2.Items(i).Text & "' ,"
                '  Response.Write("333")
                sql &= " '" & txtinfo_dept2.Items(i).Text & "' "
                sql &= ")"
                ' Response.Write(sql)
                'Response.End()
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & "" & sql)
                End If
            Next i

        End Sub

     

        Sub updatePSMTab() ' สำหรับหน้า PSM
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            sql = "UPDATE cfb_detail_tab SET psm_compensation = '" & txtpsm_compensation.Value & "' "
            sql &= " , psm_dianosis = '" & addslashes(txtpsm_diagnonsis.Text) & "' "
            '  sql &= " , recommend_psm = '" & addslashes(txtpsm_recomm.Value) & "' "
            '  sql &= " , conclusion =  '" & addslashes(txtpsm_conclusion.Value) & "' "
            '  sql &= " , remark_psm = '" & addslashes(txtpsm_remark.Value) & "' "

            sql &= " , date_close = '" & convertToSQLDatetime(txtpsm_date_expect.Text) & "' "
            sql &= " , date_close_ts = '" & ConvertDateStringToTimeStamp(txtpsm_date_expect.Text) & "' "
            ' sql &= " , resp_person = '" & addslashes(txtpsm_person.Value) & "' "
            For i As Integer = 1 To 6
                If CType(panel_psm.FindControl("chk_reso" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= " , psm_chk_resolution" & i & " =  '1' "
                Else
                    sql &= " , psm_chk_resolution" & i & " =  '0' "
                End If
            Next i

            For i As Integer = 1 To 4
                If CType(panel_psm.FindControl("txtpsm_pt" & i), RadioButton).Checked = True Then
                    sql &= " , psm_pt_satisfaction = " & i
                    sql &= " , psm_pt_satisfaction_name = '" & CType(panel_psm.FindControl("txtpsm_pt" & i), RadioButton).Text & "' "

                End If
            Next i

            If chk_psm_refer.Checked = True Then
                sql &= " , psm_is_legal = 1 "
            Else
                sql &= " , psm_is_legal = 0 "
            End If

            sql &= " , psm_recommendation = '" & addslashes(txtpsm_recommend.Value) & "' "
            sql &= " , psm_remark = '" & addslashes(txtpsm_remark.Value) & "' "
            sql &= " , psm_status_id = '" & txtpsm_status.SelectedValue & "' "
            sql &= " , psm_status_name = '" & txtpsm_status.SelectedItem.Text & "' "
            sql &= " , psm_person = '" & addslashes(txtpsm_response.Value) & "' "
            sql &= " WHERE ir_id = " & cfbId
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If

        End Sub

        Sub isHasRow(ByVal table As String)
            Dim sql As String
            Dim ds As New DataSet
            Dim errorMsg As String = ""

            sql = "SELECT * FROM " & table & " WHERE ir_id = " & cfbId
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count <= 0 Then
                sql = "INSERT INTO " & table & " (ir_id) VALUES( "
                sql &= "" & cfbId & ""
                sql &= ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & " : " & sql)
                End If
            End If

        End Sub

        Protected Sub cmdAddTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTopic.Click
            Dim sql As String
            Dim errorMsg As String
            Dim ds As New DataSet
            Dim new_order_sort As String
            Try

                If mode = "edit" Then
                    checkLockTransaction()
                    If is_lock = "1" Then
                        bindCFBDetail()
                        Return
                    End If
                End If


                sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM cfb_comment_list WHERE "
                If mode = "add" Then
                    sql &= " session_id = '" & session_id & "' "
                Else
                    sql &= " ir_id = " & cfbId
                End If

                ds = conn.getDataSetForTransaction(sql, "t1")
                new_order_sort = ds.Tables(0).Rows(0)(0).ToString

                If mode = "add" Then
                    sql = "INSERT INTO cfb_comment_list ( comment_type_id , comment_type_name , comment_detail , comment_solution , order_sort , lastupdate_by , lastupdate_time , session_id ) VALUES("

                    sql &= "'" & txtcomment_type.SelectedValue & "' ,"
                    sql &= "'" & txtcomment_type.SelectedItem.Text & "' ,"
                    sql &= "'" & addslashes(txtadd_detail.Value) & "' ,"
                    sql &= "'" & addslashes(txtadd_solution.Value) & "' , "
                    sql &= "'" & new_order_sort & "' , "
                    sql &= "'" & addslashes(Session("user_fullname").ToString) & "' , "
                    sql &= " GETDATE() , "
                    sql &= "'" & session_id & "' "
                    sql &= ")"
                Else
                    sql = "INSERT INTO cfb_comment_list (ir_id , comment_type_id , comment_type_name , comment_detail , comment_solution , lastupdate_by , lastupdate_time , order_sort) VALUES("
                    sql &= "'" & cfbId & "' ,"
                    sql &= "'" & txtcomment_type.SelectedValue & "' ,"
                    sql &= "'" & txtcomment_type.SelectedItem.Text & "' ,"
                    sql &= "'" & addslashes(txtadd_detail.Value) & "' ,"
                    sql &= "'" & addslashes(txtadd_solution.Value) & "' ,"
                    sql &= "'" & addslashes(Session("user_fullname").ToString) & "' , "
                    sql &= " GETDATE() , "
                    sql &= "'" & new_order_sort & "' "
                    sql &= ")"
                End If


                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                If mode = "add" Then

                    ' Try
                    updateTransList("1")
                    updateTypeOfComment()
                    isHasRow("cfb_detail_tab")
                    updateCFBDetail("")
                    updateAttachFile() ' รายการไฟล์ attach

                    ' conn.setDBCommit()
                    'Catch ex As Exception
                    ' conn.setDBRollback()
                    ' Response.Write(ex.Message)
                    ' Return
                    ' End Try

                    'Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & cfbId)
                End If

                conn.setDBCommit()


                bindGridComment()
                If mode = "edit" Then
                    bindRelateDept_Combo()
                    bindTQMCase()
                End If

                txtadd_detail.Value = ""
                txtadd_solution.Value = ""
                ' bindTQMCase()
                'bindDeptCase()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
                Return
            Finally
                ds.Dispose()
            End Try

            If mode = "add" Then
                Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & cfbId)
            End If
        End Sub

        Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblDeptList As Label = CType(e.Row.FindControl("lblDeptList"), Label)
                Dim lblCommentTypeId As Label = CType(e.Row.FindControl("lblCommentTypeId"), Label)
                Dim cmdSend As Button = CType(e.Row.FindControl("cmdSend"), Button)

                Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
                Dim LinkButton2 As LinkButton = CType(e.Row.FindControl("LinkButton2"), LinkButton)

                Dim sql As String
                '  Dim ds As New DataSet
                Dim ds2 As New DataSet
                Try

                    If lblCommentTypeId.Text = "1" Then
                        cmdSend.Visible = True
                        cmdSend.Attributes.Add("onclick", "window.open('popup_star.aspx?id=" & lblPK.Text & "', 'popupFile2', 'alwaysRaised,scrollbars =yes,width=800,height=750');return false;")
                    Else
                        cmdSend.Visible = False
                    End If

                    sql = "SELECT * FROM cbf_relate_dept WHERE comment_id = " & lblPK.Text

                    ds2 = conn.getDataSetForTransaction(sql, "t1")
                    For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1

                        If i > 0 Then
                            ' lblDeptList.Text &= "<br/>"
                        End If
                        lblDeptList.Text &= "<br/> - " & ds2.Tables(0).Rows(i)("dept_name").ToString
                    Next i


                    If txtstatus.SelectedValue = "" Or txtstatus.SelectedValue = "1" Or viewtype = "tqm" Then
                        LinkButton2.Visible = True
                    Else
                        LinkButton2.Visible = False
                    End If
                    'Response.Write(lblDeptList.Text)

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    '  ds.Dispose()
                    ds2.Dispose()
                End Try

            End If
        End Sub

        Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                If mode = "edit" Then
                    checkLockTransaction()
                    If is_lock = "1" Then
                        bindCFBDetail()
                        Return
                    End If
                End If

                sql = "DELETE FROM cfb_comment_list WHERE comment_id = " & GridView1.DataKeys(e.RowIndex).Value
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Throw New Exception(result)
                End If
                conn.setDBCommit()
                bindGridComment()
                If mode = "edit" Then
                    bindRelateDept_Combo()
                    bindTQMCase()
                End If
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
           
        End Sub


        Sub insertAlertLog(ByVal cmd As String)
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            sql = "INSERT INTO ir_alert_log (ir_id , alert_date , alert_date_ts , alert_method , subject , send_to , cc_to , bcc_to) VALUES( "
            sql &= "'" & cfbId & "' ,"
            sql &= "GETDATE() ,"
            sql &= "'" & Date.Now.Ticks & "' ,"
            sql &= "'" & cmd & "' ,"
            sql &= "'" & txtsubject.SelectedItem.Text & "' ,"
            sql &= "'" & txtto.Value & "' ,"
            sql &= "'" & txtcc.Value & "' ,"
            sql &= "'" & txtidBCCSelect.Value & "' "
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If
        End Sub


        Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        End Sub

        Protected Sub cmdSaveOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveOrder.Click
            Dim sql As String
            Dim errorMsg As String
            Dim i As Integer

            Dim lbl As Label
            Dim txtorder As TextBox

            If mode = "edit" Then
                checkLockTransaction()
                If is_lock = "1" Then
                    bindCFBDetail()
                    Return
                End If
            End If

            i = GridView1.Rows.Count

            Try

                For s As Integer = 0 To i - 1

                    lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)
                    txtorder = CType(GridView1.Rows(s).FindControl("txtorder"), TextBox)

                    sql = "UPDATE cfb_comment_list SET order_sort = '" & txtorder.Text & "' WHERE comment_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                Next s

                conn.setDBCommit()

                bindGridComment()

                txtadd_detail.Value = ""
                txtadd_solution.Value = ""
                bindTQMCase()
                bindDeptCase()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub txtgrandtopic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtgrandtopic.SelectedIndexChanged
            bindComboNormalTopic()
        End Sub

        Protected Sub txtnormaltopic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtnormaltopic.SelectedIndexChanged
            bindComboSubTopic("")

        End Sub

        Protected Sub txttqmcase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttqmcase.SelectedIndexChanged
            bindTQMTab(txttqmcase.SelectedValue)
            bindTQMGridCause()

            'bindDefendantDepartment_Select()
            bindDefendantUnit_Select()
            bindDefendantUnit_Grant()
            bindTQMDoctor()
        End Sub

        Protected Sub cmdSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendMail.Click
            Try
                If txtsubject.SelectedValue = "4" Then ' department 
                    ' updateTransList("4")
                    updateTranListLog("4") 'เปลี่ยน Status เป็น Need Investigate
                    updateOnlyLog("4")
                    updateStatusNeedInvestigate()
                ElseIf txtsubject.SelectedValue = "5" Then ' PSM
                    ' updateTransList("5")
                    updateTranListLog("5")
                    updateOnlyLog("5")
                End If

                insertAlertLog("Email")
                If chk_sms.Checked = True Then
                    insertAlertLog("SMS")
                End If
                ' conn.setDBCommit()
                getMailAndSMS()
                conn.setDBCommit()

            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
                Response.End()
            End Try

            Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & cfbId)
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
                ' Response.Write("xxxx : " & txtidselect.Value)
                'For i As Integer = 0 To UBound(email_list)

                '    sendMail(email_list(i), txtsubject.SelectedItem.Text, txtmessage.Value)

                '    If chk_sms.Checked = True Then
                '        sendSMS(ds.Tables(0).Rows(i)("custom_mobile").ToString())
                '    End If
                'Next i

                'Dim thread As New Thread(New ParameterizedThreadStart(AddressOf sendMail2))
                Dim p1() As String = email_list
                Dim p2() As String = cc_list
                Dim p3 As String
                Dim p4 As String = txtmessage.Value
                Dim p5 = ""

                If txtsubject.SelectedValue = "5" Then ' PSM
                    p3 = "[CFB #" & txtcfbid.Text & "] HN" & txthn.Text & " : " & txtcomplain.Value & " "
                Else
                    p3 = "[CFB #" & txtcfbid.Text & "] HN" & txthn.Text & " : " & txtcomplain.Value & " " & txtsubject.SelectedItem.Text
                End If
                ' Dim parameters As Object() = New Object() {p1, p2, p3, p4, p5}

                'thread.Start(parameters)

                ' ==== Add new June 2013 ====
                Dim key As String = ""
                Dim req As String = ""
                Dim msgbody As String = ""
                Dim emailviewtype As String = "ha"

                If txtsubject.SelectedValue = "4" Then
                    emailviewtype = "dept"
                ElseIf txtsubject.SelectedValue = "5" Then
                    emailviewtype = "psm"
                Else
                    emailviewtype = "ha"
                End If

                key = UserActivation.GetActivationLink("cfb/form_cfb.aspx?mode=edit&cfbid=" & cfbId & "&req=" & req)
                msgbody &= "<a href='http://bhtraining/login.aspx?viewtype=" & emailviewtype & "&req=" & req & "&key=" & key & "'>" & "http://bhtraining/login.aspx?viewtype=" & emailviewtype & "&req=" & req & "&key=" & key & " </a>"

                txtmessage.Value = txtmessage.Value.Replace("http://bhportal", msgbody)

                ' Response.Write(txtmessage.Value.Replace(vbCrLf, "<br/>"))
                'Response.End()
                sendMailWithCC1(email_list, cc_list, bcc_list, p3, txtmessage.Value.Replace(vbCrLf, "<br/>"), "", "cfb")

            Catch ex As Exception
                Response.Write("Send mail :: " & ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub sendMailWithCC1(ByVal toEmail() As String, ByVal ccEmail() As String, ByVal bccEmail() As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal mailType As String = "ir", Optional ByVal username As String = "", Optional ByVal password As String = "")
            Dim oMsg As New MailMessage()
            Dim smtp As New SmtpClient("mail.bumrungrad.com")

            Try
                If mailType = "ir" Then
                    oMsg.From = New MailAddress("TQMIncident@bumrungrad.com")
                    oMsg.Headers.Add("Disposition-Notification-To", "<TQMIncident@bumrungrad.com>")
                Else
                    oMsg.From = New MailAddress("CFB@bumrungrad.com")
                    oMsg.Headers.Add("Disposition-Notification-To", "<CFB@bumrungrad.com>")
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

                Try
                    For i As Integer = 0 To UBound(bccEmail)
                        If bccEmail(i) Is Nothing Or bccEmail(i) = "" Then
                        Else
                            If bccEmail(i) <> "" And bccEmail(i).Length > 5 Then
                                oMsg.Bcc.Add(New MailAddress(bccEmail(i).ToLower))
                            End If
                        End If

                    Next
                Catch ex As Exception

                End Try

                If chk_priority.Checked Then
                    oMsg.Priority = MailPriority.High
                End If

                oMsg.Subject = subject
                oMsg.IsBodyHtml = True
                oMsg.Body = message


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

        Sub getMailBackToTQM()
            Dim sql As String
            Dim ds As New DataSet
            Dim emp_code() As String
            Dim email_list() As String
            Dim cc_list() As String
            Dim bcc_list() As String
            Dim n As Integer
            Try

                email_list = "CFB@bumrungrad.com".Split(",") ' create emailTo array
                cc_list = "".Split(",") ' create emailCC array
                bcc_list = "".Split(",") ' create emailCC array
                ' Response.Write("xxxx : " & txtidselect.Value)
                'For i As Integer = 0 To UBound(email_list)

                '    sendMail(email_list(i), txtsubject.SelectedItem.Text, txtmessage.Value)

                '    If chk_sms.Checked = True Then
                '        sendSMS(ds.Tables(0).Rows(i)("custom_mobile").ToString())
                '    End If
                'Next i

                'Dim thread As New Thread(New ParameterizedThreadStart(AddressOf sendMail2))
                Dim p1() As String = email_list
                Dim p2() As String = cc_list
                Dim p3 As String
                Dim p4 As String = "Investigation : " & vbCrLf & txtinvestigation.Value & vbCrLf & vbCrLf & txtcause.Value & vbCrLf & vbCrLf & txtcorrective.Value
                Dim p5 = ""

               
                p3 = "[CFB #" & txtcfbid.Text & "] " & addslashes(Session("user_fullname").ToString) & ", " & Session("dept_name").ToString & " was investigated case, " & Date.Now.ToString

                ' Dim parameters As Object() = New Object() {p1, p2, p3, p4, p5}

                'thread.Start(parameters)

                sendMailWithCC(email_list, cc_list, bcc_list, p3, p4, "", "cfb")

            Catch ex As Exception
                Response.Write("Send mail :: " & ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub txtdepttab_combo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdepttab_combo.SelectedIndexChanged
            bindDeptCase()
            bindDeptTab()
            bindDeptPreventiveAction()
            bindDeptGridCause()
        End Sub

        Protected Sub txtdeptcase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdeptcase.SelectedIndexChanged
            bindDeptTab()
            bindDeptPreventiveAction()
        End Sub

        Protected Sub GridviewIncidentLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridviewIncidentLog.PageIndexChanging
            GridviewIncidentLog.PageIndex = e.NewPageIndex
            bindGridIncidentLog()
        End Sub

        Protected Sub GridviewIncidentLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridviewIncidentLog.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblReportby As Label = CType(e.Row.FindControl("lblReportby"), Label)
                Dim lblStatusID As Label = CType(e.Row.FindControl("lblStatusID"), Label)

                Dim sql As String
                Dim ds As New DataSet
                Try

                    If lblStatusID.Text = "2" Then
                        lblReportby.Text = "Submitted by"
                    ElseIf lblStatusID.Text = "3" Then
                        lblReportby.Text = "Received by"
                    ElseIf lblStatusID.Text = "4" Then
                        lblReportby.Text = "N/A"
                    ElseIf lblStatusID.Text = "7" Then
                        lblReportby.Text = "Investigated by"
                    ElseIf lblStatusID.Text = "9" Then
                        lblReportby.Text = "Closed by"
                    ElseIf lblStatusID.Text = "91" Then
                        lblReportby.Text = "Re-open"
                    ElseIf lblStatusID.Text = "0" Then

                    ElseIf lblStatusID.Text = "1" Then
                        lblReportby.Text = "Drafted"
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    ds.Dispose()

                End Try

            End If
        End Sub

        Protected Sub GridviewIncidentLog_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridviewIncidentLog.SelectedIndexChanged

        End Sub

        Function getNewCFBNo() As String
            Dim sql As String
            Dim dsIR As New DataSet

            Dim new_ir_no As String = 0
            Dim run_no As Long

            Try
                Dim yyyymmdd As String = CStr(Date.Now.Year) & Date.Now.Month.ToString.PadLeft(2, "0") ' & Date.Now.Day.ToString.PadLeft(2, "0")
                sql = "SELECT cfb_no , RIGHT(ISNULL(cfb_no,''),4) AS running FROM cfb_detail_tab WHERE ISNULL(cfb_no,0) <> '' AND cfb_no LIKE '" & yyyymmdd & "%' ORDER BY cfb_no DESC"
                dsIR = conn.getDataSetForTransaction(sql, "t1")
                If dsIR.Tables(0).Rows.Count > 0 Then
                    run_no = CLng(Trim(dsIR.Tables(0).Rows(0)("running").ToString)) + 1
                    new_ir_no = CLng(yyyymmdd & Date.Now.Day.ToString.PadLeft(2, "0")) & run_no.ToString.PadLeft(4, "0")
                Else
                    new_ir_no = yyyymmdd & Date.Now.Day.ToString.PadLeft(2, "0") & "0001"
                End If

                'Dim d As New Date(new_ir_no
            Catch ex As Exception
                Response.Write(ex.Message)
                new_ir_no = ""
            Finally
                dsIR.Dispose()

            End Try

            Return new_ir_no
        End Function

       
        Protected Sub GridHistory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridHistory.SelectedIndexChanged

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
                        sql = "SELECT ISNULL(MAX(file_id),0) + 1 AS pk FROM cfb_attachment"
                        ds = conn.getDataSetForTransaction(sql, "t1")
                        pk = ds.Tables("t1").Rows(0)(0).ToString
                    Catch ex As Exception
                        Response.Write(ex.Message)
                        Response.Write(sql)
                    Finally
                        ds.Dispose()
                        ds = Nothing
                    End Try


                    sql = "INSERT INTO cfb_attachment (file_id , ir_id ,  file_name , file_path , file_size , session_id) VALUES("
                    sql &= "" & pk & " , "
                    If cfbId = "" Then
                        sql &= " null , "
                    Else
                        sql &= "" & cfbId & " , "
                    End If

                    sql &= "'" & strFileName & "' , "
                    sql &= "'" & pk & "." & extension & "' , "
                    sql &= "'" & FileUpload1.PostedFile.ContentLength & "' , "
                    sql &= "'" & Session.SessionID & "'  "
                    sql &= ")"

                    errorMsg = conn.executeSQLForTransaction(sql)
                    'Response.Write("pk = " & pk)
                    If errorMsg <> "" Then
                        Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                    End If

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/cfb/attach_file/" & pk & "." & extension))
                    Dim MyFile As New FileInfo(Server.MapPath("../share/cfb/attach_file/" & pk & "." & extension))
                    If MyFile.Exists() Then

                    Else
                        ' Throw New Exception("File Not Found")
                        ' MessageBox.Show("File not found.")
                        conn.setDBRollback()
                        Return
                    End If

                    conn.setDBCommit()
                End If

                bindFile()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            Finally

            End Try
        End Sub

        Sub bindFile()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM cfb_attachment a WHERE 1 = 1"
                If mode = "add" Then
                    sql &= " AND a.session_id = '" & session_id & "'"
                Else
                    sql &= " AND a.ir_id = " & cfbId
                End If
                ' Response.Write(sql)

                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                GridFile.DataSource = ds.Tables(0)
                GridFile.DataBind()

                lblAttachNum.Text = "(" & ds.Tables(0).Rows.Count & ")"
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Protected Sub txtlang_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtlang.SelectedIndexChanged
            ' Session("lang") = txtlang.SelectedValue
            bindLanguage(txtlang.SelectedValue)
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
                        sql = "DELETE FROM cfb_attachment WHERE file_id = " & lbl.Text

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
                        File.Delete(Server.MapPath("../share/cfb/attach_file/" & lblFilePath.Text))
                    End If

                Next s

                conn.setDBCommit()
                bindFile()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            End Try
        End Sub

        Protected Sub txtsubtopic1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsubtopic1.SelectedIndexChanged
            bindComboSubTopic2("")
        End Sub

        Protected Sub txtsubtopic2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsubtopic2.SelectedIndexChanged
            bindComboSubTopic3("")
        End Sub

        Protected Sub cmdReopen2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReopen2.Click
            Response.Redirect("reopen_cfb.aspx?irId=" & cfbId)
        End Sub

        Protected Sub cmdTQMAddCause_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTQMAddCause.Click
            Dim sql As String
            Dim errorMsg As String
            Dim ds As New DataSet
            Dim new_order_sort As String = ""
            Try
                If txttqmcase.SelectedValue = "" Or txttqmcase.Items.Count <= 0 Then
                    Return
                End If

                sql = "SELECT ISNULL(MAX(order_sort) , 0) + 1 FROM cfb_tqm_cause_list WHERE comment_id = " & txttqmcase.SelectedValue & " AND ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "t1")
                new_order_sort = ds.Tables("t1").Rows(0)(0).ToString

                sql = "INSERT INTO cfb_tqm_cause_list (comment_id , ir_id , cause_id , cause_name , cause_remark , ir_type , dept_unit_name , dept_unit_id , order_sort) VALUES("
                sql &= "" & txttqmcase.SelectedValue & " ,"
                sql &= "" & cfbId & " ,"
                sql &= "'" & txtcfb_cause.SelectedValue & "' ,"
                sql &= "'" & txtcfb_cause.SelectedItem.Text & "' ,"
                sql &= "'" & addslashes(txttqm_addremark.Text) & "' ,"
                sql &= "'ir' ,"
                sql &= "'" & addslashes(txtadd_cause_defendant.SelectedItem.Text) & "' ,"
                sql &= "'" & txtadd_cause_defendant.SelectedValue & "' ,"
                sql &= "'" & new_order_sort & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()

                txttqm_addremark.Text = ""
                txtcfb_cause.SelectedIndex = 0
                txtadd_cause_defendant.SelectedIndex = 0
                bindTQMGridCause()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub GridTQMCause_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridTQMCause.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM cfb_tqm_cause_list WHERE tqm_cause_id = " & GridTQMCause.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Response.Write(result)
                End If

                conn.setDBCommit()
                bindTQMGridCause()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridTQMCause_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridTQMCause.SelectedIndexChanged

        End Sub

        Protected Sub cmdAddPrevent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddPrevent.Click
            Dim sql As String
            Dim result As String
            Dim ds As New DataSet
            Dim pk As String = ""
            Try
                sql = "SELECT ISNULL(MAX(ORDER_SORT),0) + 1 FROM cfb_dept_prevent_list WHERE cfb_relate_dept_id = " & txtdeptcase.SelectedValue
                ds = conn.getDataSetForTransaction(sql, "t1")
                Dim order As String
                order = ds.Tables("t1").Rows(0)(0).ToString

                pk = getPK("prevent_dept_id", "cfb_dept_prevent_list", conn)

                sql = "INSERT INTO cfb_dept_prevent_list (prevent_dept_id , cfb_relate_dept_id , action_detail , resp_person ,   order_sort ,date_start , date_start_ts , date_end , date_end_ts)"
                sql &= " VALUES("
                sql &= "'" & pk & "' ,"
                sql &= "'" & txtdeptcase.SelectedValue & "' ,"
                sql &= "'" & addslashes(txt_addprevent.Value) & "' ,"
                sql &= "'" & addslashes(txt_addperson.Value) & "' ,"
                '  sql &= "'" & txtdepttab_combo.SelectedValue & "' ,"
                ' sql &= "'" & cfbId & "' ,"
                sql &= "'" & order & "' ,"
                If txtdate_prevent1.Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & convertToSQLDatetime(txtdate_prevent1.Text) & "' ,"
                End If

                sql &= "'" & ConvertDateStringToTimeStamp(txtdate_prevent1.Text) & "' ,"
                If txtdate_prevent2.Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & convertToSQLDatetime(txtdate_prevent2.Text) & "' ,"
                End If

                sql &= "'" & ConvertDateStringToTimeStamp(txtdate_prevent2.Text) & "' "


                sql &= ")"
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Throw New Exception(result)
                End If


                conn.setDBCommit()
                bindDeptPreventiveAction()
                txt_addprevent.Value = ""
                txtdate_prevent1.Text = ""
                txtdate_prevent2.Text = ""
                txt_addperson.Value = ""
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & " : " & sql)
            End Try
        End Sub

       
     
        Protected Sub cmdPreventOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPreventOrder.Click
            Dim sql As String
            Dim errorMsg As String
            Dim i As Integer

            Dim lbl As Label
            Dim txtorder As TextBox


            i = GridViewPrevent.Rows.Count

            Try

                For s As Integer = 0 To i - 1

                    lbl = CType(GridViewPrevent.Rows(s).FindControl("lblPK"), Label)
                    txtorder = CType(GridViewPrevent.Rows(s).FindControl("txtorder"), TextBox)

                    sql = "UPDATE cfb_dept_prevent_list SET order_sort = '" & txtorder.Text & "' WHERE prevent_dept_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                Next s

                conn.setDBCommit()
                bindDeptPreventiveAction()

            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridViewPrevent_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewPrevent.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM cfb_dept_prevent_list WHERE prevent_dept_id = " & GridViewPrevent.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Response.Write(result)
                End If

                conn.setDBCommit()
                bindDeptPreventiveAction()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridViewPrevent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewPrevent.SelectedIndexChanged
         
        End Sub

        Protected Sub txtsubject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsubject.SelectedIndexChanged
            If txtsubject.SelectedValue <> "" Then
                Dim template As String = ""
                template &= "Dear ," & vbCrLf & vbCrLf

                If txtsubject.SelectedValue = "4" Then
                    template &= "Please find customer feedback report. Please investigate on this matter, and kindly send it back to us." & vbCrLf & vbCrLf
                ElseIf txtsubject.SelectedValue = "101" Then
                    template &= "Please find customer feedback report for your information. If you have any feedback on this matter, please do not hesitate to let us know. " & vbCrLf & vbCrLf
                End If

                'template &= "To access new online incident and customer feedback report, please open BH Portal (http://bhportal) and select Incident Report or Customer Feedback Report under Operation Support Application menu. For more information please contact TQM department." & vbCrLf & vbCrLf

                template &= "To access new online incident and customer feedback report, please open this link below. " & vbCrLf & " (http://bhportal)" & vbCrLf & "For more information please contact IR&CFB department." & vbCrLf & vbCrLf

                template &= "Best Regards, " & vbCrLf
              

                txtmessage.Value = template
            Else
                txtmessage.Value = ""
            End If
        End Sub

        Protected Sub cmdPrintForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrintForm.Click
            Dim strURL As String = "http://" & ConfigurationManager.AppSettings("frontHost").ToString & "/cfb/preview_cfb.aspx?irId=" & cfbId

            Dim uri As New Uri(strURL)

            'Create the request object

            Dim req As HttpWebRequest = DirectCast(WebRequest.Create(uri), HttpWebRequest)
            req.UserAgent = "Get Content"
            Dim resp As WebResponse = req.GetResponse()
            Dim stream As Stream = resp.GetResponseStream()
            Dim sr As New StreamReader(stream)
            Dim html As String = sr.ReadToEnd()

            Dim fp As StreamWriter

            Try
                Dim writer As StreamWriter = New StreamWriter(Server.MapPath("../share/") & "cfb.html", False)
                writer.WriteLine(html)
                writer.Close()
                'fp = File.CreateText(Server.MapPath("../share/") & "test.html")
                'fp.WriteLine(html)

                'fp.Close()
            Catch err As Exception
                Response.Write(err.Message)
                Return
            Finally

            End Try

            Response.Redirect("../pdf/cfbprint.php")
            ' cmdPrintForm.Attributes.Add("onClick", "window.open('../pdf/cfbprint.php')")
        End Sub

        Protected Sub cmdHRAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHRAdd.Click
            While txtinfo_dept1.Items.Count > 0 AndAlso txtinfo_dept1.SelectedItem IsNot Nothing
                Dim selectedItem As ListItem = txtinfo_dept1.SelectedItem
                selectedItem.Selected = False
                txtinfo_dept2.Items.Add(selectedItem)
                txtinfo_dept1.Items.Remove(selectedItem)
            End While
        End Sub

        Protected Sub cmdHrRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHrRemove.Click
            While txtinfo_dept2.Items.Count > 0 AndAlso txtinfo_dept2.SelectedItem IsNot Nothing
                Dim selectedItem As ListItem = txtinfo_dept2.SelectedItem
                selectedItem.Selected = False
                txtinfo_dept1.Items.Add(selectedItem)
                txtinfo_dept2.Items.Remove(selectedItem)
            End While
        End Sub

        Private Sub bindGridInformationUpdate()
            Dim ds As New DataSet
            Dim sql As String
            Dim sqlB As New StringBuilder

            Try
                sqlB.Append("SELECT * FROM ir_information_update WHERE ir_id =  " & cfbId)

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sqlB.ToString)
                End If
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

        Public Function getPK(ByVal column As String, ByVal table As String, ByVal conn As DBUtil) As String
            Dim sql As String
            Dim result As String = ""
            Dim ds As New DataSet
            ' Dim conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
            Try

                sql = "SELECT ISNULL(MAX(" & column & "),0) + 1 AS pk FROM " & table

                ds = conn.getDataSetForTransaction(sql, "t1")
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If
                result = ds.Tables("t1").Rows(0)("pk").ToString
            Catch ex As Exception

                result = ex.Message & " (" & sql & ")"
            Finally
                ds.Clear()
                ds = Nothing
                ' conn.closeSql()
            End Try

            Return result
        End Function

        Protected Sub cmdAddUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddUpdate.Click
            Dim sql As String = ""
            Dim errorMsg As String = ""
            Dim pk As String = ""

            Try
                pk = getPK("inform_id", "ir_information_update", conn)
                sql = "INSERT INTO ir_information_update (inform_id , ir_id , inform_type , inform_detail , inform_date , inform_date_ts , inform_by , inform_emp_code , inform_dept_name , inform_costcenter) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & cfbId & "' ,"
                sql &= " 'cfb' ,"
                sql &= " '" & addslashes(txtadd_update.Text) & "' ,"
                sql &= " GETDATE() ,"
                sql &= " '" & Date.Now.Ticks & "' ,"
                sql &= " '" & Session("user_fullname").ToString & " , " & Session("user_position").ToString & "' ,"
                sql &= " '" & Session("emp_code").ToString & "' ,"
                sql &= " '" & Session("dept_name").ToString & "' ,"
                sql &= " '" & Session("costcenter_id").ToString & "' "
                sql &= ")"
                ' Response.Write(sql)
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

        Sub bindFileMCO()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_mco_attachment a WHERE 1 = 1 "
                If mode = "add" Then
                    sql &= " AND a.session_id = '" & session_id & "'"
                Else
                    sql &= " AND a.ir_id = " & cfbId
                End If
                ' Response.Write(sql)

                ds = conn.getDataSetForTransaction(sql, "t1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If
                GridFileMCO.DataSource = ds.Tables(0)
                GridFileMCO.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Protected Sub cmdUploadFileMCO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUploadFileMCO.Click
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
                        sql = "SELECT ISNULL(MAX(file_id),0) + 1 AS pk FROM ir_mco_attachment"
                        ds = conn.getDataSetForTransaction(sql, "t1")
                        pk = ds.Tables("t1").Rows(0)(0).ToString
                    Catch ex As Exception
                        Response.Write(ex.Message)
                        Response.Write(sql)
                    Finally
                        ds.Dispose()
                        ds = Nothing
                    End Try


                    sql = "INSERT INTO ir_mco_attachment (file_id , ir_id ,  file_name , file_path , file_size , session_id) VALUES("
                    sql &= "" & pk & " , "
                    If cfbId = "" Then
                        sql &= " null , "
                    Else
                        sql &= "" & cfbId & " , "
                    End If

                    sql &= "'" & strFileName & "' , "
                    sql &= "'" & pk & "." & extension & "' , "
                    sql &= "'" & FileUpload1.PostedFile.ContentLength & "' , "
                    sql &= "'" & Session.SessionID & "'  "
                    sql &= ")"

                    errorMsg = conn.executeSQLForTransaction(sql)
                    'Response.Write("pk = " & pk)
                    If errorMsg <> "" Then
                        Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
                    End If

                    FileUpload3.PostedFile.SaveAs(Server.MapPath("../share/mco/cfb/" & pk & "." & extension))

                    conn.setDBCommit()
                End If

                bindFileMCO()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            Finally

            End Try
        End Sub

        Protected Sub cmdDeleteFileMCO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteFileMCO.Click
            Dim sql As String
            Dim errorMsg As String
            Dim i As Integer

            Dim lbl As Label
            Dim chk As CheckBox
            Dim lblFilePath As Label

            i = GridFileMCO.Rows.Count

            Try

                For s As Integer = 0 To i - 1

                    lbl = CType(GridFileMCO.Rows(s).FindControl("lblPKMCO"), Label)
                    chk = CType(GridFileMCO.Rows(s).FindControl("chkSelectMCO"), CheckBox)

                    '  response.write(lbl.Text)
                    If chk.Checked Then
                        sql = "DELETE FROM ir_mco_attachment WHERE file_id = " & lbl.Text
                        errorMsg = conn.executeSQLForTransaction(sql)
                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg)
                            'Exit For
                        End If
                    End If


                Next s

                For s As Integer = 0 To i - 1
                    lblFilePath = CType(GridFileMCO.Rows(s).FindControl("lblFilePathMCO"), Label)
                    File.Delete(Server.MapPath("../share/mco/cfb/" & lblFilePath.Text))
                Next s

                conn.setDBCommit()
                bindFileMCO()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub cmdPSMAddConcern_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMAddConcern.Click
            panel_psm_concern.Visible = True
            cmdPSMAddConcern.Visible = False
            clearValueInPanel()
        End Sub

        Protected Sub cmdCancelConcern_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelConcern.Click
            panel_psm_concern.Visible = False
            cmdPSMAddConcern.Visible = True
            clearValueInPanel()
        End Sub

        Protected Sub cmdSaveConcenrn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveConcenrn.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String
            Dim pk2 As String
            Dim pk3 As String

            Try
                pk = getPK("concern_id", "ir_psm_concern_list", conn)
                sql = "INSERT INTO ir_psm_concern_list (concern_id , ir_id , concern_detail , topic_id , topic_name , subtopic_id , subtopic_name , std_care_type , std_care_type_name  ) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & cfbId & "' ,"
                sql &= " '" & addslashes(txtpsm_concern.Value) & "' , "
                sql &= " '" & txtpsm_category.SelectedValue & "' ,"
                sql &= " '" & txtpsm_category.SelectedItem.Text & "' ,"
                If txtpsm_subcategory.Items.Count = 0 Then
                    sql &= " '' ,"
                    sql &= " '' ,"
                Else
                    sql &= " '" & txtpsm_subcategory.SelectedValue & "' ,"
                    sql &= " '" & txtpsm_subcategory.SelectedItem.Text & "' ,"
                End If

                If txtpsm_std1.Checked = True Then
                    sql &= " 1  ,"
                    sql &= " 'Yes' "
                ElseIf txtpsm_std2.Checked = True Then
                    sql &= " 2  ,"
                    sql &= " 'No' "
                ElseIf txtpsm_std3.Checked = True Then
                    sql &= " 3  ,"
                    sql &= " 'Borderline' "
                Else
                    sql &= " 0  ,"
                    sql &= " '' "
                End If

              

                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If


                For i As Integer = 0 To txtpsm_list_doctor.Items.Count - 1
                    pk2 = getPK("concern_doctor_id", "ir_psm_concern_doctor", conn)
                    sql = "INSERT INTO ir_psm_concern_doctor (concern_doctor_id , concern_id , ir_id , concern_doctor) VALUES("
                    sql &= " '" & pk2 & "' ,"
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & cfbId & "' ,"
                    sql &= " '" & txtpsm_list_doctor.Items(i).Text & "' "
                    sql &= ")"
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                Next i

                For i As Integer = 0 To txtpsm_list_dept.Items.Count - 1
                    pk3 = getPK("concern_dept_id", "ir_psm_concern_dept", conn)
                    sql = "INSERT INTO ir_psm_concern_dept (concern_dept_id , concern_id , ir_id , concern_dept_name , costcenter_id) VALUES("
                    sql &= " '" & pk3 & "' ,"
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & cfbId & "' ,"
                    sql &= " '" & txtpsm_list_dept.Items(i).Text & "' ,"
                    sql &= " '" & txtpsm_list_dept.Items(i).Value & "' "
                    sql &= ")"
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                Next i


                conn.setDBCommit()
                bindPSMConcern()
                panel_psm_concern.Visible = False
                cmdPSMAddConcern.Visible = True
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            End Try
        End Sub

        Sub bindPSMConcern()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_psm_concern_list a WHERE 1 = 1 "
                sql &= " AND a.ir_id = " & cfbId

                ' Response.Write(sql)

                ds = conn.getDataSetForTransaction(sql, "t1")

                GridConcern.DataSource = ds.Tables(0)
                GridConcern.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindMCOCategory()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_psm_category a WHERE 1 = 1 ORDER BY psm_category_name "
              

                ds = conn.getDataSetForTransaction(sql, "t1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                txtpsm_category.DataSource = ds.Tables(0)
                txtpsm_category.DataBind()

                txtpsm_category.Items.Insert(0, New ListItem("-- --", ""))
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindMCOSubCategory()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_psm_subcategory a WHERE 1 = 1 "
                If txtpsm_category.SelectedValue <> "" Then
                    sql &= " AND psm_category_id = " & txtpsm_category.SelectedValue
                End If


                ds = conn.getDataSetForTransaction(sql, "t1")

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                txtpsm_subcategory.DataSource = ds.Tables(0)
                txtpsm_subcategory.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Protected Sub txtpsm_category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpsm_category.SelectedIndexChanged
            bindMCOSubCategory()
        End Sub

        Protected Sub GridConcern_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridConcern.RowDeleting
            Dim sql As String
            Dim errorMSg As String

            Try
                sql = "DELETE FROM ir_psm_concern_list WHERE concern_id = '" & GridConcern.DataKeys(e.RowIndex).Value & "'"
                errorMSg = conn.executeSQLForTransaction(sql)
                If errorMSg <> "" Then
                    Throw New Exception(errorMSg)
                End If

                conn.setDBCommit()
                bindPSMConcern()

            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
           

        End Sub

        Protected Sub GridConcern_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridConcern.SelectedIndexChanged

        End Sub

        Protected Sub cmdPSMAddDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMAddDoc.Click
            txtpsm_list_doctor.Items.Add(New ListItem(txtpsm_add_doctor.Text & " " & txtpsm_add_special.Value, txtpsm_add_doctor.Text & " " & txtpsm_add_special.Value))

            txtpsm_add_doctor.Text = ""
            txtpsm_add_special.Value = ""
        End Sub

        Sub clearValueInPanel()
            For Each c As Control In panel_psm_concern.Controls

                If TypeOf c Is HtmlTextArea Then
                    DirectCast(c, HtmlTextArea).Value = ""
                End If


                If TypeOf c Is HtmlInputText Then
                    DirectCast(c, HtmlInputText).Value = ""
                End If

                If TypeOf c Is DropDownList Then
                    DirectCast(c, DropDownList).SelectedIndex = -1

                End If

                If TypeOf c Is ListBox Then
                    DirectCast(c, ListBox).Items.Clear()
                End If
            Next

            txtpsm_subcategory.Items.Clear()
        End Sub

        Protected Sub cmdPSMAddDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMAddDept.Click
            txtpsm_list_dept.Items.Add(New ListItem(txtpsm_add_dept.SelectedItem.Text, txtpsm_add_dept.SelectedValue))
            txtpsm_add_dept.SelectedIndex = 0
        End Sub

        Protected Sub cmdPSMRemoveDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMRemoveDept.Click

            If txtpsm_list_dept.SelectedIndex = -1 Then
                Return
            End If

            txtpsm_list_dept.Items.RemoveAt(txtpsm_list_dept.SelectedIndex)
        End Sub

        Protected Sub cmdPSMDelDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMDelDoc.Click
            If txtpsm_list_doctor.SelectedIndex = -1 Then
                Return
            End If

            txtpsm_list_doctor.Items.RemoveAt(txtpsm_list_doctor.SelectedIndex)
        End Sub

      
        Protected Sub cmdAddPSMPerson_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddPSMPerson.Click
            txtpsm_response.Value &= txtpsm_add_person.Text & vbCrLf
            txtpsm_add_person.Text = ""
        End Sub

        Protected Sub GridInformation_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridInformation.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim LinkDelete As LinkButton = CType(e.Row.FindControl("LinkDelete"), LinkButton)
                Dim lblEmpNo As Label = CType(e.Row.FindControl("lblEmpNo"), Label)

                Dim sql As String
                Dim ds As New DataSet
                Try

                    If lblEmpNo.Text = Session("emp_code").ToString Then
                        LinkDelete.Visible = True
                    Else
                        LinkDelete.Visible = False
                    End If


                Catch ex As Exception
                    Response.Write(ex.Message & sql)
                Finally
                    ds.Dispose()

                End Try

            End If
        End Sub

        Protected Sub GridInformation_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridInformation.RowDeleting
            Dim sql As String
            Dim errorMSg As String

            Try
                sql = "DELETE FROM ir_information_update WHERE inform_id = '" & GridInformation.DataKeys(e.RowIndex).Value & "'"
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

       

        Protected Sub cmdPrintInvest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrintInvest.Click
            Dim strURL As String = "http://" & ConfigurationManager.AppSettings("frontHost").ToString & "/cfb/preview_investigate.aspx?irId=" & cfbId & "&dept_id=" & txtdepttab_combo.SelectedValue & "&comment_id=" & txtdeptcase.SelectedValue
            ' Response.Write(strURL)
            ' Return
            Dim uri As New Uri(strURL)

            'Create the request object

            Dim req As HttpWebRequest = DirectCast(WebRequest.Create(uri), HttpWebRequest)
            req.UserAgent = "Get Content"
            Dim resp As WebResponse = req.GetResponse()
            Dim stream As Stream = resp.GetResponseStream()
            Dim sr As New StreamReader(stream)
            Dim html As String = sr.ReadToEnd()

            Dim fp As StreamWriter

            Try
                Dim writer As StreamWriter = New StreamWriter(Server.MapPath("../share/") & "cfb_invest.html", False)
                writer.WriteLine(html)
                writer.Close()
                'fp = File.CreateText(Server.MapPath("../share/") & "test.html")
                'fp.WriteLine(html)

                'fp.Close()
            Catch err As Exception
                Response.Write(err.Message)
                Return
            Finally

            End Try

            Response.Redirect("../pdf/cfbprint_investigate.php")
            'cmdPrintForm.Attributes.Add("onClick", "window.open('../pdf/cfbprint.php')")
        End Sub

        Protected Sub cmdUnitAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUnitAdd.Click
            While txtunit_defandent_all.Items.Count > 0 AndAlso txtunit_defandent_all.SelectedItem IsNot Nothing
                Dim selectedItem As ListItem = txtunit_defandent_all.SelectedItem
                selectedItem.Selected = False
                txtunit_defandent_select.Items.Add(selectedItem)
                txtunit_defandent_all.Items.Remove(selectedItem)
            End While
        End Sub

        Protected Sub cmdUnitRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUnitRemove.Click
            While txtunit_defandent_select.Items.Count > 0 AndAlso txtunit_defandent_select.SelectedItem IsNot Nothing
                Dim selectedItem As ListItem = txtunit_defandent_select.SelectedItem
                selectedItem.Selected = False
                txtunit_defandent_all.Items.Add(selectedItem)
                txtunit_defandent_select.Items.Remove(selectedItem)
            End While
        End Sub

        Protected Sub cmdTQMAddDoctor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTQMAddDoctor.Click
            Dim sql As String
            Dim errorMsg As String

            Try
                sql = "INSERT INTO ir_doctor_defendant (ir_id , comment_id , doctor_name , md_code , dept_unit_id , dept_unit_name) VALUES("
                sql &= "" & cfbId & " , "
                sql &= "" & txttqmcase.SelectedValue & " , "
                sql &= "'" & txttqm_finddoctor.Text & "' , "
                sql &= "'" & txtmdcode.Text & "' , "
                sql &= "'" & txtadd_doctor_defentdant_unit.SelectedValue & "' , "
                sql &= "'" & txtadd_doctor_defentdant_unit.SelectedItem.Text & "'  "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()

                txttqm_finddoctor.Text = ""
                txtmdcode.Text = ""
                bindTQMDoctor()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub bindTQMDoctor()
            Dim sql As String
            Dim ds As New DataSet

            If txttqmcase.Items.Count = 0 Then
                Return
            End If

            Try
                sql = "SELECT * FROM ir_doctor_defendant WHERE ir_id = " & cfbId
                sql &= " AND comment_id = " & txttqmcase.SelectedValue

                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage & sql)
                End If

                ds = conn.getDataSetForTransaction(sql, "t1")
                ' Response.Write(sql)
                GridViewTQMDoctor.DataSource = ds
                GridViewTQMDoctor.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub GridViewTQMDoctor_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewTQMDoctor.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM ir_doctor_defendant WHERE defendant_id = " & GridViewTQMDoctor.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Response.Write(result)
                End If

                conn.setDBCommit()
                bindTQMDoctor()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridViewTQMDoctor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewTQMDoctor.SelectedIndexChanged

        End Sub

        Protected Sub cmdRestore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRestore.Click
            Dim sql As String
            Dim errorMsg As String
            Try
                sql = "UPDATE ir_trans_list SET is_cancel = 0 WHERE ir_id = " & cfbId
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
                conn.setDBCommit()

            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
                Return
            End Try

            Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & cfbId)
        End Sub

        Protected Sub cmdTQMOrderCause_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTQMOrderCause.Click
            Dim sql As String
            Dim errorMsg As String
            Dim i As Integer

            Dim lbl As Label
            Dim txtorder As TextBox


            i = GridTQMCause.Rows.Count

            Try

                For s As Integer = 0 To i - 1

                    lbl = CType(GridTQMCause.Rows(s).FindControl("lblPK"), Label)
                    txtorder = CType(GridTQMCause.Rows(s).FindControl("txtorder"), TextBox)

                    sql = "UPDATE cfb_tqm_cause_list SET order_sort = '" & txtorder.Text & "' WHERE tqm_cause_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                Next s

                conn.setDBCommit()
                bindTQMGridCause()

            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub onGotoStar(ByVal sender As Object, ByVal e As CommandEventArgs)

        End Sub

        Function getNewIRNo() As String
            Dim sql As String
            Dim dsIR As New DataSet

            Dim new_ir_no As String = 0
            Dim run_no As Long
            Try
                Dim yyyymmdd As String = CStr(Date.Now.Year) & Date.Now.Month.ToString.PadLeft(2, "0") ' & Date.Now.Day.ToString.PadLeft(2, "0")
                sql = "SELECT ir_no , RIGHT(ir_no,4) AS running FROM ir_detail_tab WHERE ir_no LIKE '" & yyyymmdd & "%' ORDER BY ir_no DESC"
                dsIR = conn.getDataSetForTransaction(sql, "t1")
                If dsIR.Tables(0).Rows.Count > 0 Then
                    run_no = CLng(Trim(dsIR.Tables(0).Rows(0)("running").ToString)) + 1
                    new_ir_no = CLng(yyyymmdd & Date.Now.Day.ToString.PadLeft(2, "0")) & run_no.ToString.PadLeft(4, "0")
                Else
                    new_ir_no = yyyymmdd & Date.Now.Day.ToString.PadLeft(2, "0") & "0001"
                End If

                'Dim d As New Date(new_ir_no
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                dsIR.Dispose()

            End Try

            Return new_ir_no
        End Function

        Protected Sub cmdConvert_Click(sender As Object, e As EventArgs) Handles cmdConvert.Click
            Dim sql As String
            Dim errMsg As String
            Dim ds As New DataSet
            Dim ir_no As String = ""
            Dim new_order_sort As String = ""
            Dim report_by As String = ""
            Dim pk As String = ""
            Dim ir_detail As String = ""
            Dim status As String = ""

            Try
                If txtconvertstatus.SelectedIndex = 0 Then
                    Try
                        status = txtstatus.SelectedValue
                    Catch ex As Exception
                        status = 1
                    End Try

                Else
                    status = 1
                End If

                sql = "UPDATE ir_trans_list SET report_type = 'ir'  , status_id = " & status & " , date_submit = GETDATE() , date_submit_ts = " & Date.Now.Ticks & " , form_id = 5 , is_move_to_cfb = 0 , is_move_to_ir = 1 WHERE ir_id = " & cfbId
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg)
                End If
                'Response.Write(sql)
                isHasRow("ir_detail_tab")

             
                If txtconvertstatus.SelectedIndex = 1 Then
                    ir_no = "0"
                Else
                    sql = "SELECT * FROM ir_detail_tab WHERE ir_id = " & cfbId
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    If (ds.Tables("t1").Rows(0)("ir_no").ToString = "") Or (ds.Tables("t1").Rows(0)("ir_no").ToString = "0") Then
                        ir_no = getNewIRNo()

                    Else
                        ir_no = ds.Tables("t1").Rows(0)("ir_no").ToString
                    End If
                End If

                'sql = "UPDATE cfb_detail_tab SET cfb_no = '0'  WHERE ir_id = " & cfbId
                'errMsg = conn.executeSQLForTransaction(sql)
                'If errMsg <> "" Then
                '    Throw New Exception(errMsg)
                'End If

                sql = "SELECT * FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id WHERE a.ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "t1")
                report_by = ds.Tables("t1").Rows(0)("report_by").ToString

                sql = "UPDATE ir_detail_tab SET pt_name = '" & addslashes(txtcomplain.Value) & "'"
                sql &= " , pt_title = '" & txttitle.SelectedValue & "'"
                sql &= " , age = '" & txtptage.Value & "'"
                ' sql &= " , month_day_num = '" & txtpt_monthday.Value & "'"
                ' sql &= " , month_day_text = '" & txtpt_selectmonthday.SelectedItem.Text & "'"
                sql &= " , sex = '" & txtptsex.SelectedValue & "'"
                sql &= " , hn = '" & addslashes(txthn.Text) & "'"
                sql &= " , room = '" & addslashes(txtroom.Text) & "'"
                sql &= " , service_type = '" & txtservicetype.SelectedValue & "'"
                sql &= " , customer_segment = '" & txtsegment.SelectedValue & "'"
                ' sql &= " , clinical_type = '" & txtclinical.SelectedValue & "'"
                'sql &= " , pt_type = '" & txtpttype.SelectedValue & "'"
                'sql &= " , pt_type_remark = '" & addslashes(txtstatusother.Value) & "'"

                sql &= " , ir_no = '" & ir_no & "'"
                sql &= " , division = '" & addslashes(txtdivision.Text) & "'"

                sql &= " , dept_id = '" & ds.Tables("t1").Rows(0)("dept_id_report").ToString & "'"

                sql &= " , report_tel = '" & addslashes(txtcontact.Text) & "'"
                sql &= " , diagnosis = '" & addslashes(txtdiagnosis.Value) & "'"
                sql &= " , operation = '" & addslashes(txtoperation.Value) & "'"
                'sql &= " , date_operation = '" & convertToSQLDatetime(txtdate_op.Value) & "'"
                'sql &= " , date_operation_ts = '" & ConvertDateStringToTimeStamp(txtdate_op.Value) & "'"
                'sql &= " , physician = '" & addslashes(txtatt_doctor.Text) & "'"
                sql &= " , datetime_report = '" & convertToSQLDatetime(txtdate_report.Text, txthour.SelectedValue.PadLeft(2, "0"), txtmin.SelectedValue.PadLeft(2, "0")) & "'"
                sql &= " , datetime_report_ts = '" & ConvertDateStringToTimeStamp(txtdate_report.Text, CInt(txthour.SelectedValue), CInt(txtmin.SelectedValue)) & "'"
                'If txtserious.Checked = True Then
                '    sql &= " , flag_serious = 1 "
                'Else
                '    sql &= " , flag_serious = 0 "
                'End If
                sql &= " , location = '" & addslashes(txtlocation.Text) & "'"
                sql &= " , describe = '" & addslashes(ds.Tables("t1").Rows(0)("comment_detail").ToString) & "'"
                'If txtnoti1.Checked = True Then
                '    sql &= " , chk_physician = 1 "
                'Else
                '    sql &= " , chk_physician = 0 "
                'End If
                'If txtnoti2.Checked = True Then
                '    sql &= " , chk_family = 1 "
                'Else
                '    sql &= " , chk_family = 0 "
                'End If
                'If txtnoti3.Checked = True Then
                '    sql &= " , chk_document = 1 "
                'Else
                '    sql &= " , chk_document = 0 "
                'End If
                'sql &= " , doctor_name = '" & addslashes(txtdoctor.Text) & "'"
                'sql &= " , dotor_type = '" & txtdoctype.SelectedValue & "'"
                'sql &= " , datetime_assessment = '" & convertToSQLDatetime(txtdate_assessment.Value, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "'"
                'sql &= " , datetime_assessment_ts = " & ConvertDateStringToTimeStamp(txtdate_assessment.Value, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & ""
                'sql &= " , describe_assessment = '" & addslashes(txtassessment.Value) & "'"
                'If txtxray.Checked = True Then
                '    sql &= ", chk_xray =  1 "
                'Else
                '    sql &= ", chk_xray = 0 "
                'End If
                'If txtlab.Checked = True Then
                '    sql &= ", chk_lab = 1 "
                'Else
                '    sql &= ", chk_lab = 0 "
                'End If
                'If txtother.Checked = True Then
                '    sql &= ", chk_other = 1 "
                'Else
                '    sql &= ", chk_other = 0 "
                'End If

                'sql &= " , xray_detail = '" & addslashes(txtxray_detail.Value) & "'"
                'sql &= " , lab_detail = '" & addslashes(txtlab_detail.Value) & "'"
                'sql &= " , other_detail = '" & addslashes(txtother_detail.Value) & "'"
                'sql &= " , xray_result = '" & addslashes(txtxray_result.Value) & "'"
                'sql &= " , lab_result = '" & addslashes(txtlab_result.Value) & "'"
                'sql &= " , other_result = '" & addslashes(txtother_result.Value) & "'"
                'sql &= " , recommend_detail = '" & addslashes(txtrecommend.Value) & "'"
                'sql &= " , describe_action = '" & addslashes(txtaction.Value) & "'"
                'sql &= " , severe_id = '" & txtsevere.SelectedValue & "'"
                'sql &= " , severe_other_id = '" & addslashes(txteffect.SelectedValue) & "'"
                'sql &= " , severe_other_remark = '" & addslashes(txteffect_detail.Value) & "'"
                sql &= " , initial_action = '" & addslashes(ds.Tables("t1").Rows(0)("comment_solution").ToString) & "'"
                'sql &= " , action_result_id = '" & txtresult_action.SelectedValue & "'"
                'sql &= " , action_result_remark = '" & addslashes(txtactionremark.Value) & "'"

                sql &= " WHERE ir_id = " & cfbId
                ' Response.Write(sql)
                ' Response.End()
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg & ":" & sql)
                End If

                sql = "SELECT * FROM cfb_comment_list a INNER JOIN cbf_relate_dept b ON a.comment_id = b.comment_id WHERE  a.ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "tDept")
                For i As Integer = 0 To ds.Tables("tDept").Rows.Count - 1

                    'sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM cfb_comment_list WHERE "
                    'sql &= " ir_id = " & irId

                    '  ds = conn.getDataSetForTransaction(sql, "t1")
                    ' new_order_sort = ds.Tables(0).Rows(0)(0).ToString

                    pk = getPK("relate_dept_id", "ir_relate_dept", conn)
                    sql = "INSERT INTO ir_relate_dept (relate_dept_id , ir_id , dept_name , dept_id , session_id , create_by , create_date , create_date_ts) VALUES("
                    sql &= "'" & pk & "',"
                    sql &= "'" & cfbId & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_name").ToString & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_id").ToString & "',"
                    sql &= "'" & Session.SessionID & "' ,"
                    sql &= "'" & report_by & "' ,"
                    sql &= " GETDATE() ,"
                    sql &= "'" & Date.Now.Ticks & "' "
                    sql &= ")"

                    ir_detail &= ds.Tables("tDept").Rows(i)("comment_detail").ToString


                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If

                Next i

                Dim filename As String()
                Dim extension As String
                Dim iCount As Integer = 0

                sql = "SELECT * FROM cfb_attachment WHERE ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "tFile")

                For iFile As Integer = 0 To ds.Tables("tFile").Rows.Count - 1

                    pk = getPK("file_id", "ir_attachment", conn)

                    filename = ds.Tables("tFile").Rows(iFile)("file_name").ToString.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    sql = "INSERT INTO ir_attachment (file_id , form_id , ir_id ,  file_name , file_path , file_size , session_id) VALUES( "
                    sql &= "" & pk & " , "
                    sql &= " 5 ,"
                    sql &= "" & cfbId & " , "
                    sql &= "'" & ds.Tables("tFile").Rows(iFile)("file_name").ToString & "' , "
                    sql &= "'" & pk & "." & extension & "' , "
                    sql &= "'" & ds.Tables("tFile").Rows(iFile)("file_size").ToString & "' , "
                    sql &= "'" & Session.SessionID & "'  "
                    sql &= ")"
                    ' Response.Write(sql)
                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If

                    Try
                        File.Delete(Server.MapPath("../share/incident/attach_file/" & pk & "." & extension))
                    Catch ex As Exception

                    End Try

                    File.Copy(Server.MapPath("../share/cfb/attach_file/" & ds.Tables("tFile").Rows(iFile)("file_path").ToString), Server.MapPath("../share/incident/attach_file/" & pk & "." & extension))

                Next iFile




                updateOnlyLog("0", "Convert from CFB : " & txtcfbid.Text)

                conn.setDBCommit()
            Catch ex As Exception
                Response.Write(ex.Message & sql)
                conn.setDBRollback()
                Return
            Finally
                ds.Dispose()
            End Try

            Response.Redirect("home.aspx?viewtype=tqm")
        End Sub

        Protected Sub cmdChangeToDraft_Click(sender As Object, e As EventArgs) Handles cmdChangeToDraft.Click
            Dim sql As String
            Dim errMsg As String
            Dim ds As New DataSet
            Dim ir_no As String = ""
            Dim new_order_sort As String = ""
            Dim report_by As String = ""
            Dim pk As String = ""
            Dim ir_detail As String = ""

            Try
                sql = "UPDATE ir_trans_list SET  status_id = 1 , date_submit = null , date_submit_ts = 0 , is_change_to_draft = 1   WHERE ir_id = " & cfbId
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg)
                End If
                'Response.Write(sql)
              

                'sql = "UPDATE cfb_detail_tab SET cfb_no = '0'  WHERE ir_id = " & cfbId
                'errMsg = conn.executeSQLForTransaction(sql)
                'If errMsg <> "" Then
                '    Throw New Exception(errMsg)
                'End If

                updateOnlyLog("0", "Change status to Draft")

                conn.setDBCommit()
            Catch ex As Exception
                Response.Write(ex.Message & sql)
                conn.setDBRollback()
                Return
            Finally
                ds.Dispose()
            End Try

            Response.Redirect("home.aspx?viewtype=tqm")
        End Sub

        Protected Sub cmdCopy_Click(sender As Object, e As EventArgs) Handles cmdCopy.Click
            Dim cfbPk As String = ""
            Dim sql As String = ""
            Dim ds As New DataSet
            Dim ds2 As New DataSet
            Dim errorMsg As String = ""

            Try
                ' ======================= UPDATE ir_trans_list =========================
                Try
                    sql = "SELECT ISNULL(MAX(ir_id),0) + 1 AS pk FROM ir_trans_list"
                    ds = conn.getDataSetForTransaction(Sql, "t1")
                    cfbPk = ds.Tables("t1").Rows(0)(0).ToString
                    ' new_ir_id = pk
                Catch ex As Exception
                    Response.Write(ex.Message & Sql)
                    Response.Write(sql)
                    Return
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO ir_trans_list (ir_id , date_report, date_submit , date_submit_ts , status_id , report_type , report_by ,  report_emp_code)"
                sql &= " VALUES("
                sql &= "" & cfbPk & " ,"
                sql &= " GETDATE() ,"
                sql &= " null ,"
                sql &= " null ,"
                sql &= "1 ,"
                sql &= " 'cfb' ,"
                sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"

                sql &= "'" & Session("emp_code").ToString & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(Sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & Sql)
                End If


                sql = "INSERT INTO ir_status_log (status_id , status_name , ir_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
                sql &= "'" & 1 & "' ,"
                sql &= "'" & "" & "' ,"
                sql &= "'" & cfbPk & "' ,"
                sql &= "GETDATE() ,"
                sql &= "'" & Date.Now.Ticks & "' ,"
                sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"
                sql &= "'" & addslashes(Session("user_position").ToString) & "' ,"
                sql &= "'" & addslashes(Session("dept_name").ToString) & "' ,"
                sql &= "'" & "Draft" & "' "
                sql &= ")"
                '  Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & ":" & sql)
                End If

                ' ======================= UPDATE CFB Comment =========================

                sql = "SELECT * FROM cfb_comment_list WHERE ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "tDept")

                For i As Integer = 0 To ds.Tables("tDept").Rows.Count - 1

                    'sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM cfb_comment_list WHERE "
                    'sql &= " ir_id = " & irId

                    '  ds = conn.getDataSetForTransaction(sql, "t1")
                    ' new_order_sort = ds.Tables(0).Rows(0)(0).ToString

                    sql = "INSERT INTO cfb_comment_list (ir_id , comment_type_id , comment_type_name , comment_detail , comment_solution , lastupdate_by , lastupdate_time , order_sort) VALUES("
                    sql &= "'" & cfbPk & "' ,"
                    sql &= " '" & ds.Tables("tDept").Rows(i)("comment_type_id").ToString & "'  ,"
                    sql &= " '" & ds.Tables("tDept").Rows(i)("comment_type_name").ToString & "' ,"
                    sql &= " '" & addslashes(ds.Tables("tDept").Rows(i)("comment_detail").ToString) & "' ,"
                    sql &= " '" & addslashes(ds.Tables("tDept").Rows(i)("comment_solution").ToString) & "' ,"
                    sql &= " '" & ds.Tables("tDept").Rows(i)("lastupdate_by").ToString & "' ,"
                    sql &= " GETDATE() , "
                    sql &= "'" & 0 & "' "
                    sql &= ")"


                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If

                    sql = "SELECT MAX(comment_id) FROM cfb_comment_list"
                    ds2 = conn.getDataSetForTransaction(sql, "tMaxID")

                    Dim deptPK As String
                    deptPK = getPK("cfb_relate_dept_id", "cbf_relate_dept", conn)
                    sql = "INSERT INTO cbf_relate_dept (cfb_relate_dept_id , comment_id ,  dept_id , dept_name ) "
                    sql &= " SELECT " & deptPK & " ," & ds2.Tables("tMaxID").Rows(0)(0).ToString & " , dept_id , dept_name"
                    sql &= " FROM cbf_relate_dept WHERE comment_id = " & ds.Tables("tDept").Rows(i)("comment_id").ToString
                    'Response.Write(sql)
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If

                Next i

                ' ======================= UPDATE CFB Detail =========================
                sql = "INSERT INTO cfb_detail_tab (ir_id ,cfb_no ,running_no ,division ,dept_id_report ,report_tel,report_tel2,costcenter_id ,service_type ,pt_title,pt_name ,age ,sex,hn,diagnosis ,operation ,location ,room,datetime_complaint ,datetime_complaint_ts ,customer_segment,datetime_report,datetime_report_ts,complain_detail ,country,xxx_cfb_dept_id,xxx_cfb_dept_name,complain_status,feedback_from,complain_status_remark,feedback_from_remark,part_customer ,part_hospital,part_employee ,std_care ,pt_resolution ,compensation ,concern,recommend_psm ,conclusion ,remark_psm,is_legal,date_close,date_close_ts,resp_person,psm_compensation,psm_dianosis,psm_recommendation,psm_remark ,psm_status_id,psm_status_name ,psm_is_legal ,psm_chk_resolution1,psm_chk_resolution2 ,psm_chk_resolution3,psm_chk_resolution4 ,psm_chk_resolution5,psm_chk_resolution6,psm_pt_satisfaction ,psm_pt_satisfaction_name ,psm_person ,cfb_is_complain,cfb_customer_resp,cfb_customer_resp_remark,cfb_chk_tel,cfb_chk_email,cfb_chk_other ,cfb_tel_remark ,cfb_email_remark,cfb_other_remark)"
                sql &= "SELECT " & cfbPk & " , 0 ,0 ,division ,dept_id_report ,report_tel,report_tel2,costcenter_id ,service_type ,pt_title,pt_name ,age ,sex,hn,diagnosis ,operation ,location ,room,datetime_complaint ,datetime_complaint_ts ,customer_segment,datetime_report,datetime_report_ts,complain_detail ,country,xxx_cfb_dept_id,xxx_cfb_dept_name,complain_status,feedback_from,complain_status_remark,feedback_from_remark,part_customer ,part_hospital,part_employee ,std_care ,pt_resolution ,compensation ,concern,recommend_psm ,conclusion ,remark_psm,is_legal,date_close,date_close_ts,resp_person,psm_compensation,psm_dianosis,psm_recommendation,psm_remark ,psm_status_id,psm_status_name ,psm_is_legal ,psm_chk_resolution1,psm_chk_resolution2 ,psm_chk_resolution3,psm_chk_resolution4 ,psm_chk_resolution5,psm_chk_resolution6,psm_pt_satisfaction ,psm_pt_satisfaction_name ,psm_person ,cfb_is_complain,cfb_customer_resp,cfb_customer_resp_remark,cfb_chk_tel,cfb_chk_email,cfb_chk_other ,cfb_tel_remark ,cfb_email_remark,cfb_other_remark FROM cfb_detail_tab WHERE ir_id = " & cfbId

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
                'isHasRow("cfb_detail_tab")
                ' updateCFBDetail(e.CommandArgument.ToString)
                'updateAttachFile() ' รายการไฟล์ attach

                ' ======================= UPDATE CFB File Attachment =========================
                Dim filename As String()
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filePK As String = ""
                sql = "SELECT * FROM cfb_attachment WHERE ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "tFile")

                For iFile As Integer = 0 To ds.Tables("tFile").Rows.Count - 1

                    filePK = getPK("file_id", "cfb_attachment", conn)

                    filename = ds.Tables("tFile").Rows(iFile)("file_name").ToString.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    sql = "INSERT INTO cfb_attachment (file_id  , ir_id ,  file_name , file_path , file_size , session_id) VALUES( "
                    sql &= "" & filePK & " , "

                    sql &= "" & cfbPk & " , "
                    sql &= "'" & ds.Tables("tFile").Rows(iFile)("file_name").ToString & "' , "
                    sql &= "'" & filePK & "." & extension & "' , "
                    sql &= "'" & ds.Tables("tFile").Rows(iFile)("file_size").ToString & "' , "
                    sql &= "'" & Session.SessionID & "'  "
                    sql &= ")"
                    ' Response.Write(sql)
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If

                    Try
                        File.Copy(Server.MapPath("../share/cfb/attach_file/" & ds.Tables("tFile").Rows(iFile)("file_path").ToString), Server.MapPath("../share/cfb/attach_file/" & filePK & "." & extension))
                    Catch ex As Exception

                    End Try



                Next iFile

                conn.setDBCommit()



            Catch ex As Exception
                conn.setDBRollback()
                Response.Write("onSave : " & ex.Message)
                Return
            Finally
            End Try

            Response.Redirect("home.aspx?viewtype=tqm")
        End Sub

      

        Protected Sub txtlock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtlock.SelectedIndexChanged
            Dim sql As String
            Dim errorMsg As String

            Try
                If txtlock.SelectedIndex = 0 Then
                    sql = "UPDATE ir_trans_list SET is_lock = 1 , lock_by = '" & addslashes(Session("user_fullname")).ToString & "' "
                    sql &= " , lock_date_raw = GETDATE() , lock_empcode =  " & Session("emp_code").ToString
                    sql &= " WHERE ir_id = " & cfbId
                Else
                    sql = "UPDATE ir_trans_list SET is_lock = 0 , lock_by = '" & addslashes(Session("user_fullname")) & "' "
                    sql &= " , lock_date_raw = null , lock_empcode =  " & Session("emp_code").ToString
                    sql &= " WHERE ir_id = " & cfbId
                End If
              
                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()

                bindCFBDetail()
                ' lblLockMsg.Text = "Locked by " & addslashes(Session("user_fullname")).ToString

            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            End Try
        End Sub

        Protected Sub txtcomboowner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtcomboowner.SelectedIndexChanged
            txttqm_owner.Text = txtcomboowner.SelectedValue
        End Sub

        Protected Sub cmdConvertAsCopy_Click(sender As Object, e As EventArgs) Handles cmdConvertAsCopy.Click
            Dim sql As String
            Dim errMsg As String
            Dim ds As New DataSet
            Dim ir_no As String = ""
            Dim new_order_sort As String = ""
            Dim report_by As String = ""
            Dim pk As String = ""
            Dim ir_detail As String = ""
            Dim status As String = ""
            Dim new_ir_id As String = ""

            Try
                If txtconvertstatus.SelectedIndex = 0 Then
                    Try
                        status = txtstatus.SelectedValue
                    Catch ex As Exception
                        status = 1
                    End Try

                Else
                    status = 1
                End If


                Try
                    sql = "SELECT ISNULL(MAX(ir_id),0) + 1 AS pk FROM ir_trans_list"
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

                sql = "INSERT INTO ir_trans_list (ir_id , date_report , form_id , status_id , report_type , report_by ,  report_emp_code)"
                sql &= " VALUES("
                sql &= "" & new_ir_id & " ,"
                sql &= " GETDATE() ,"
                sql &= " 5 , "
                sql &= "" & status & " ,"
                sql &= " 'ir' ,"
                sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"
                sql &= "'" & Session("emp_code").ToString & "' "
                sql &= ")"

                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg & sql)
                End If

                'sql = "UPDATE ir_trans_list SET report_type = 'ir'  , status_id = " & status & " , date_submit = GETDATE() , date_submit_ts = " & Date.Now.Ticks & " , form_id = 5 WHERE ir_id = " & cfbId
                'errMsg = conn.executeSQLForTransaction(sql)
                'If errMsg <> "" Then
                '    Throw New Exception(errMsg)
                'End If
                'Response.Write(sql)
                ' isHasRow("ir_detail_tab")
                sql = "SELECT * FROM ir_detail_tab WHERE ir_id = " & new_ir_id
                ds = conn.getDataSetForTransaction(sql, "t1")
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                If ds.Tables("t1").Rows.Count <= 0 Then
                    sql = "INSERT INTO ir_detail_tab (ir_id) VALUES( "
                    sql &= "" & new_ir_id & ""
                    sql &= ")"
                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If
                End If

                If txtconvertstatus.SelectedIndex = 1 Then ' ถ้าเลือก Current Status
                    ir_no = "0"
                Else
                    sql = "SELECT * FROM ir_detail_tab WHERE ir_id = " & new_ir_id
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    If (ds.Tables("t1").Rows(0)("ir_no").ToString = "") Or (ds.Tables("t1").Rows(0)("ir_no").ToString = "0") Then
                        ir_no = getNewIRNo()

                    Else
                        ir_no = ds.Tables("t1").Rows(0)("ir_no").ToString
                    End If
                End If

             

                sql = "SELECT * , CONVERT(VARCHAR(30), date_submit, 121) AS date_submit_format FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id WHERE a.ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "t1")
                report_by = ds.Tables("t1").Rows(0)("report_by").ToString

                If txtconvertstatus.SelectedIndex = 0 Then ' ถ้าเลือก Current Status
                    sql = "UPDATE ir_trans_list SET date_submit = '" & ds.Tables("t1").Rows(0)("date_submit_format").ToString & "' "
                    sql &= " ,  date_submit_ts = '" & ds.Tables("t1").Rows(0)("date_submit_ts").ToString & "' "
                    sql &= " ,  report_by = '" & ds.Tables("t1").Rows(0)("report_by").ToString & "' "
                    sql &= " ,  report_emp_code = '" & ds.Tables("t1").Rows(0)("report_emp_code").ToString & "' "
                    sql &= " WHERE ir_id = " & new_ir_id
                    '  Response.Write(sql)
                    '  Response.End()
                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If
                Else
                    sql = "UPDATE ir_trans_list SET "
                    sql &= " report_by = '" & ds.Tables("t1").Rows(0)("report_by").ToString & "' "
                    sql &= " ,report_emp_code = '" & ds.Tables("t1").Rows(0)("report_emp_code").ToString & "' "
                    sql &= " WHERE ir_id = " & new_ir_id
                    ' Response.Write(sql)
                    'Response.End()
                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If
                End If


                sql = "INSERT INTO ir_status_log (status_id , status_name , ir_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
                sql &= " 0  ,"
                sql &= "'" & "" & "' ,"
                sql &= "'" & new_ir_id & "' ,"
                sql &= "GETDATE() ,"
                sql &= "'" & Date.Now.Ticks & "' ,"
                sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"
                sql &= "'" & addslashes(Session("user_position").ToString) & "' ,"
                sql &= "'" & addslashes(Session("dept_name").ToString) & "' ,"
                sql &= "'Convert from CFB' "
                sql &= ")"
                'Response.Write(sql)
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg & ":" & sql)
                End If

                sql = "UPDATE ir_detail_tab SET pt_name = '" & addslashes(txtcomplain.Value) & "'"
                sql &= " , pt_title = '" & txttitle.SelectedValue & "'"
                sql &= " , age = '" & txtptage.Value & "'"
                ' sql &= " , month_day_num = '" & txtpt_monthday.Value & "'"
                ' sql &= " , month_day_text = '" & txtpt_selectmonthday.SelectedItem.Text & "'"
                sql &= " , sex = '" & txtptsex.SelectedValue & "'"
                sql &= " , hn = '" & addslashes(txthn.Text) & "'"
                sql &= " , room = '" & addslashes(txtroom.Text) & "'"
                sql &= " , service_type = '" & txtservicetype.SelectedValue & "'"
                sql &= " , customer_segment = '" & txtsegment.SelectedValue & "'"
                ' sql &= " , clinical_type = '" & txtclinical.SelectedValue & "'"
                'sql &= " , pt_type = '" & txtpttype.SelectedValue & "'"
                'sql &= " , pt_type_remark = '" & addslashes(txtstatusother.Value) & "'"

                sql &= " , ir_no = '" & ir_no & "'"
                sql &= " , division = '" & addslashes(txtdivision.Text) & "'"

                sql &= " , dept_id = '" & ds.Tables("t1").Rows(0)("dept_id_report").ToString & "'"

                sql &= " , report_tel = '" & addslashes(txtcontact.Text) & "'"
                sql &= " , diagnosis = '" & addslashes(txtdiagnosis.Value) & "'"
                sql &= " , operation = '" & addslashes(txtoperation.Value) & "'"
                'sql &= " , date_operation = '" & convertToSQLDatetime(txtdate_op.Value) & "'"
                'sql &= " , date_operation_ts = '" & ConvertDateStringToTimeStamp(txtdate_op.Value) & "'"
                'sql &= " , physician = '" & addslashes(txtatt_doctor.Text) & "'"
                sql &= " , datetime_report = '" & convertToSQLDatetime(txtdate_report.Text, txthour.SelectedValue.PadLeft(2, "0"), txtmin.SelectedValue.PadLeft(2, "0")) & "'"
                sql &= " , datetime_report_ts = '" & ConvertDateStringToTimeStamp(txtdate_report.Text, CInt(txthour.SelectedValue), CInt(txtmin.SelectedValue)) & "'"
                'If txtserious.Checked = True Then
                '    sql &= " , flag_serious = 1 "
                'Else
                '    sql &= " , flag_serious = 0 "
                'End If
                sql &= " , location = '" & addslashes(txtlocation.Text) & "'"
                sql &= " , describe = '" & addslashes(ds.Tables("t1").Rows(0)("comment_detail").ToString) & "'"
                'If txtnoti1.Checked = True Then
                '    sql &= " , chk_physician = 1 "
                'Else
                '    sql &= " , chk_physician = 0 "
                'End If
                'If txtnoti2.Checked = True Then
                '    sql &= " , chk_family = 1 "
                'Else
                '    sql &= " , chk_family = 0 "
                'End If
                'If txtnoti3.Checked = True Then
                '    sql &= " , chk_document = 1 "
                'Else
                '    sql &= " , chk_document = 0 "
                'End If
                'sql &= " , doctor_name = '" & addslashes(txtdoctor.Text) & "'"
                'sql &= " , dotor_type = '" & txtdoctype.SelectedValue & "'"
                'sql &= " , datetime_assessment = '" & convertToSQLDatetime(txtdate_assessment.Value, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "'"
                'sql &= " , datetime_assessment_ts = " & ConvertDateStringToTimeStamp(txtdate_assessment.Value, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & ""
                'sql &= " , describe_assessment = '" & addslashes(txtassessment.Value) & "'"
                'If txtxray.Checked = True Then
                '    sql &= ", chk_xray =  1 "
                'Else
                '    sql &= ", chk_xray = 0 "
                'End If
                'If txtlab.Checked = True Then
                '    sql &= ", chk_lab = 1 "
                'Else
                '    sql &= ", chk_lab = 0 "
                'End If
                'If txtother.Checked = True Then
                '    sql &= ", chk_other = 1 "
                'Else
                '    sql &= ", chk_other = 0 "
                'End If

                'sql &= " , xray_detail = '" & addslashes(txtxray_detail.Value) & "'"
                'sql &= " , lab_detail = '" & addslashes(txtlab_detail.Value) & "'"
                'sql &= " , other_detail = '" & addslashes(txtother_detail.Value) & "'"
                'sql &= " , xray_result = '" & addslashes(txtxray_result.Value) & "'"
                'sql &= " , lab_result = '" & addslashes(txtlab_result.Value) & "'"
                'sql &= " , other_result = '" & addslashes(txtother_result.Value) & "'"
                'sql &= " , recommend_detail = '" & addslashes(txtrecommend.Value) & "'"
                'sql &= " , describe_action = '" & addslashes(txtaction.Value) & "'"
                'sql &= " , severe_id = '" & txtsevere.SelectedValue & "'"
                'sql &= " , severe_other_id = '" & addslashes(txteffect.SelectedValue) & "'"
                'sql &= " , severe_other_remark = '" & addslashes(txteffect_detail.Value) & "'"
                sql &= " , initial_action = '" & addslashes(ds.Tables("t1").Rows(0)("comment_solution").ToString) & "'"
                'sql &= " , action_result_id = '" & txtresult_action.SelectedValue & "'"
                'sql &= " , action_result_remark = '" & addslashes(txtactionremark.Value) & "'"

                sql &= " WHERE ir_id = " & new_ir_id
                ' Response.Write(sql)
                ' Response.End()
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg & ":" & sql)
                End If

                sql = "SELECT * FROM cfb_comment_list a INNER JOIN cbf_relate_dept b ON a.comment_id = b.comment_id WHERE  a.ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "tDept")
                For i As Integer = 0 To ds.Tables("tDept").Rows.Count - 1

                    'sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM cfb_comment_list WHERE "
                    'sql &= " ir_id = " & irId

                    '  ds = conn.getDataSetForTransaction(sql, "t1")
                    ' new_order_sort = ds.Tables(0).Rows(0)(0).ToString

                    pk = getPK("relate_dept_id", "ir_relate_dept", conn)
                    sql = "INSERT INTO ir_relate_dept (relate_dept_id , ir_id , dept_name , dept_id , session_id , create_by , create_date , create_date_ts) VALUES("
                    sql &= "'" & pk & "',"
                    sql &= "'" & new_ir_id & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_name").ToString & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_id").ToString & "',"
                    sql &= "'" & Session.SessionID & "' ,"
                    sql &= "'" & report_by & "' ,"
                    sql &= " GETDATE() ,"
                    sql &= "'" & Date.Now.Ticks & "' "
                    sql &= ")"

                    ir_detail &= ds.Tables("tDept").Rows(i)("comment_detail").ToString


                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If

                Next i

                Dim filename As String()
                Dim extension As String
                Dim iCount As Integer = 0

                sql = "SELECT * FROM cfb_attachment WHERE ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "tFile")

                For iFile As Integer = 0 To ds.Tables("tFile").Rows.Count - 1

                    pk = getPK("file_id", "ir_attachment", conn)

                    filename = ds.Tables("tFile").Rows(iFile)("file_name").ToString.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    sql = "INSERT INTO ir_attachment (file_id , form_id , ir_id ,  file_name , file_path , file_size , session_id) VALUES( "
                    sql &= "" & pk & " , "
                    sql &= " 5 ,"
                    sql &= "" & new_ir_id & " , "
                    sql &= "'" & ds.Tables("tFile").Rows(iFile)("file_name").ToString & "' , "
                    sql &= "'" & pk & "." & extension & "' , "
                    sql &= "'" & ds.Tables("tFile").Rows(iFile)("file_size").ToString & "' , "
                    sql &= "'" & Session.SessionID & "'  "
                    sql &= ")"
                    ' Response.Write(sql)
                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If

                    Try
                        File.Delete(Server.MapPath("../share/incident/attach_file/" & pk & "." & extension))
                    Catch ex As Exception

                    End Try

                    Try
                        File.Copy(Server.MapPath("../share/cfb/attach_file/" & ds.Tables("tFile").Rows(iFile)("file_path").ToString), Server.MapPath("../share/incident/attach_file/" & pk & "." & extension))
                    Catch ex As Exception

                    End Try


                Next iFile

                updateOnlyLog("0", "Convert from CFB : " & txtcfbid.Text)

                conn.setDBCommit()
            Catch ex As Exception
                Response.Write(ex.Message & sql)
                conn.setDBRollback()
                Return
            Finally
                ds.Dispose()
            End Try

            Response.Redirect("home.aspx?viewtype=tqm")
        End Sub

        Protected Sub cmdTQMView2_Click(sender As Object, e As EventArgs) Handles cmdTQMView2.Click
            Try
                updateTranListLog("3", "")
                updateRevision()


                If txtstatus.SelectedValue = "2" And txtstatus.SelectedValue = "2" Then ' รับเรื่องครั้งแรก
                    updateCaseOwner()
                    updateOnlyLog("3", "")
                Else
                    updateOnlyLog("0", "Save Revision : " & revision_no)
                End If

                conn.setDBCommit()

                saveHTMLForPDF(cfbId)

                Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & cfbId)

                'bindRevision()
                'bindGridIncidentLog()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End Sub

        Protected Sub cmdTQMView1_Click(sender As Object, e As EventArgs) Handles cmdTQMView1.Click
            Try
                updateTranListLog("3", "")
                updateRevision()


                If txtstatus.SelectedValue = "2" And txtstatus.SelectedValue = "2" Then ' รับเรื่องครั้งแรก
                    updateCaseOwner()
                    updateOnlyLog("3", "")
                Else
                    updateOnlyLog("0", "Save Revision : " & revision_no)
                End If

                conn.setDBCommit()

                saveHTMLForPDF(cfbId)

                Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & cfbId)

                'bindRevision()
                'bindGridIncidentLog()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub cmdDeptAddCause_Click(sender As Object, e As EventArgs) Handles cmdDeptAddCause.Click
            Dim sql As String
            Dim errorMsg As String
            Dim new_order_sort As String = "1"
            Dim ds As New DataSet
            Dim pk As String
            Dim txtcause_dept As DropDownList
            Dim txttqm_addremark_dept As TextBox

            Try
               

                txtcause_dept = CType(GridDeptCause.FooterRow.FindControl("txtcause_dept"), DropDownList)
                txttqm_addremark_dept = CType(GridDeptCause.FooterRow.FindControl("txttqm_addremark_dept"), TextBox)

                sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM cfb_dept_cause_list WHERE cfb_relate_dept_id = " & txtdeptcase.SelectedValue
                ds = conn.getDataSetForTransaction(sql, "t0")
                new_order_sort = ds.Tables("t0").Rows(0)(0).ToString

                pk = getPK("dept_cause_id", "cfb_dept_cause_list", conn)

                sql = "INSERT INTO cfb_dept_cause_list (dept_cause_id , cfb_relate_dept_id , ir_id , cause_id , cause_name , cause_remark , ir_type , order_sort) VALUES("
                sql &= "" & pk & " ,"
                sql &= "'" & txtdeptcase.SelectedValue & "' ,"
                sql &= "" & cfbId & " ,"
                sql &= "'" & txtcause_dept.SelectedValue & "' ,"
                sql &= "'" & txtcause_dept.SelectedItem.Text & "' ,"
                sql &= "'" & addslashes(txttqm_addremark_dept.Text) & "' ,"
                sql &= "'cfb' ,"
                sql &= "'" & new_order_sort & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If

                conn.setDBCommit()

                ' txttqm_addremark_dept.Text = ""
                'txtcause_dept.SelectedIndex = 0

                bindDeptGridCause()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub GridDeptCause_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridDeptCause.RowDeleting
            Dim sql As String
            Dim result As String
            Dim ds As New DataSet
            Try
                sql = "DELETE FROM cfb_dept_cause_list WHERE dept_cause_id = " & GridDeptCause.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    'Response.Write(result)
                    Throw New Exception(result)
                End If

                sql = "SELECT * FROM cfb_dept_cause_list WHERE cfb_relate_dept_id = " & txtdeptcase.SelectedValue & " ORDER BY order_sort"
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    sql = "UPDATE cfb_dept_cause_list SET order_sort = " & i + 1 & " WHERE dept_cause_id = " & ds.Tables("t1").Rows(i)("dept_cause_id").ToString
                    result = conn.executeSQLForTransaction(sql)
                    If result <> "" Then

                        Throw New Exception(result)
                    End If
                Next i

                conn.setDBCommit()
                bindDeptGridCause()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub GridDeptCause_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridDeptCause.SelectedIndexChanged

        End Sub

        Sub bindTQMPreventiveAction()
            Dim sql As String
            Dim ds As New DataSet

            Try
                If cfbId <> "" Then
                    sql = "SELECT * FROM cfb_tqm_prevent_list a LEFT OUTER JOIN user_dept b ON a.dept_id = b.dept_id WHERE ir_id = " & cfbId
                    sql &= " ORDER BY order_sort ASC"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    ' Response.Write(sql)
                    If ds.Tables("t1").Rows.Count > 0 Then
                        gridTQMActionPlan.DataSource = ds
                        gridTQMActionPlan.DataBind()
                    Else
                        gridTQMActionPlan.Columns.Clear()
                        gridTQMActionPlan.DataBind()
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Protected Sub cmdAddPreventTQM_Click(sender As Object, e As EventArgs) Handles cmdAddPreventTQM.Click
            Dim sql As String
            Dim result As String
            Dim ds As New DataSet
            Dim pk As String = ""
            Try
                sql = "SELECT ISNULL(MAX(ORDER_SORT),0) + 1 FROM cfb_tqm_prevent_list WHERE ir_id = " & cfbId
                ds = conn.getDataSetForTransaction(sql, "t1")
                Dim order As String
                order = ds.Tables("t1").Rows(0)(0).ToString

                pk = getPK("prevent_tqm_id", "cfb_tqm_prevent_list", conn)

                sql = "INSERT INTO cfb_tqm_prevent_list (prevent_tqm_id  , action_detail , resp_person , ir_id , order_sort ,date_start , date_start_ts , date_end , date_end_ts)"
                sql &= " VALUES("
                sql &= "'" & pk & "' ,"
                sql &= "'" & addslashes(txt_addprevent_tqm.Value) & "' ,"
                sql &= "'" & addslashes(txt_addperson_tqm.Value) & "' ,"

                sql &= "'" & cfbId & "' ,"
                sql &= "'" & order & "' ,"
                If txtadd_datetqm1.Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & convertToSQLDatetime(txtadd_datetqm1.Text) & "' ,"
                End If

                sql &= "'" & ConvertDateStringToTimeStamp(txtadd_datetqm1.Text) & "' ,"
                If txtadd_datetqm2.Text = "" Then
                    sql &= " null ,"
                Else
                    sql &= "'" & convertToSQLDatetime(txtadd_datetqm2.Text) & "' ,"
                End If

                sql &= "'" & ConvertDateStringToTimeStamp(txtadd_datetqm2.Text) & "' "

                sql &= ")"
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Throw New Exception(result)
                End If

                conn.setDBCommit()
                bindTQMPreventiveAction()
                txt_addprevent_tqm.Value = ""
                txtadd_datetqm1.Text = ""
                txtadd_datetqm2.Text = ""
                txt_addperson_tqm.Value = ""
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & " : " & sql)
            End Try
        End Sub

        Protected Sub gridTQMActionPlan_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gridTQMActionPlan.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM cfb_tqm_prevent_list WHERE prevent_tqm_id = " & gridTQMActionPlan.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Response.Write(result)
                End If

                conn.setDBCommit()
                bindTQMPreventiveAction()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub gridTQMActionPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gridTQMActionPlan.SelectedIndexChanged

        End Sub

        Protected Sub cmdAddPreventFromDept_Click(sender As Object, e As EventArgs) Handles cmdAddPreventFromDept.Click
            Dim sql As String
            Dim result As String
            Dim ds As New DataSet
            Dim ds2 As New DataSet
            Dim pk As String = ""
            Try

                sql = "SELECT * FROM cfb_dept_prevent_list a inner join cbf_relate_dept b"
                sql &= " on a.cfb_relate_dept_id = b.cfb_relate_dept_id"
                sql &= " inner join cfb_comment_list c on b.comment_id = c.comment_id"
                sql &= " WHERE c.ir_id = " & cfbId
                'sql &= " ORDER BY dept_id "
                ds = conn.getDataSetForTransaction(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                    sql = "SELECT ISNULL(MAX(ORDER_SORT),0) + 1 FROM cfb_tqm_prevent_list WHERE ir_id = " & cfbId
                    ds2 = conn.getDataSetForTransaction(sql, "t1")
                    Dim order As String
                    order = ds2.Tables("t1").Rows(0)(0).ToString

                    pk = getPK("prevent_tqm_id", "cfb_tqm_prevent_list", conn)

                    sql = "INSERT INTO cfb_tqm_prevent_list (prevent_tqm_id  , action_detail , resp_person , ir_id , order_sort ,date_start , date_start_ts , date_end , date_end_ts , dept_id)"
                    sql &= " VALUES("
                    sql &= "'" & pk & "' ,"
                    sql &= "'" & addslashes(ds.Tables("t1").Rows(i)("action_detail").ToString) & "' ,"
                    sql &= "'" & addslashes(ds.Tables("t1").Rows(i)("resp_person").ToString) & "' ,"

                    sql &= "'" & cfbId & "' ,"
                    sql &= "'" & order & "' ,"

                    If ds.Tables("t1").Rows(i)("date_start_ts").ToString = "" Or ds.Tables("t1").Rows(i)("date_start_ts").ToString = "0" Then
                        sql &= " null ,"
                    Else
                        sql &= "'" & ConvertTSToSQLDateTime(ds.Tables("t1").Rows(i)("date_start_ts").ToString) & "' ,"
                    End If

                    sql &= "'" & ds.Tables("t1").Rows(i)("date_start_ts").ToString & "' ,"

                    If ds.Tables("t1").Rows(i)("date_end_ts").ToString = "" Or ds.Tables("t1").Rows(i)("date_end_ts").ToString = "0" Then
                        sql &= " null ,"
                    Else
                        sql &= "'" & ConvertTSToSQLDateTime(ds.Tables("t1").Rows(i)("date_end_ts").ToString) & "' ,"
                    End If

                    sql &= "'" & ds.Tables("t1").Rows(i)("date_end_ts").ToString & "' , "
                    sql &= "'" & ds.Tables("t1").Rows(i)("dept_id").ToString & "'  "
                    sql &= ")"
                    result = conn.executeSQLForTransaction(sql)

                    If result <> "" Then
                        Throw New Exception(result)
                    End If
                Next i



                conn.setDBCommit()
                bindTQMPreventiveAction()
                txt_addprevent_tqm.Value = ""
                txtadd_datetqm1.Text = ""
                txtadd_datetqm2.Text = ""
                txt_addperson_tqm.Value = ""
            Catch ex As Exception
                conn.setDBRollback()
                '  Response.Write(ex.Message & " : " & sql)
                txt_addprevent_tqm.Value = sql
            Finally
                ds.Dispose()
                ds2.Dispose()
            End Try
        End Sub
    End Class

End Namespace
