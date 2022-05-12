Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Net.Mail
Imports System.Threading
Imports System.Net
Imports QueryStringEncryption

Namespace incident

    Partial Class incident_form_incident
        Inherits System.Web.UI.Page

        Protected formId As String = ""
        Protected irId As String = ""
        Protected mode As String = ""
        Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Protected session_id As String
        Protected viewtype As String = ""

        Private new_ir_id As String = ""
        Private cl As Control

        Protected public_ir_id As String = ""
        Private relate_dept_id As String = ""
        Protected lang As String = "th"

        Protected costcenter_list() As String
        Protected is_lock As String = "0"
        Protected revision_no As String = ""
        Protected authorized_access_linkclick As Boolean

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            formId = Request.QueryString("formId")
            irId = Request.QueryString("irId")
            mode = Request.QueryString("mode")

            If IsNothing(Session("session_myid")) Then
                '  Response.Redirect("../login.aspx")
                Return
                'Response.End()
            End If

            costcenter_list = Session("costcenter_list")
            session_id = Session("session_myid").ToString
            authorized_access_linkclick = isAccessToLinkClick()

            If Session("lang") IsNot Nothing Then ' ถ้ามี session

                If IsPostBack Then
                    Session("lang") = txtlang.SelectedValue
                Else
                    ' Session("lang") = lang
                End If

                lang = Session("lang").ToString

                'Response.Write("Session = " & Session("lang").ToString)
                txtlang.SelectedValue = lang
                ' Response.Write("1" & Session("lang").ToString & "<br/>")
            Else

                Session("lang") = txtlang.SelectedValue
                ' Response.Write("2" & Session("lang").ToString & "<br/>")

            End If
            'txtlang.SelectedValue = lang
            ' lang = txtlang.SelectedValue
            ' Response.Write(Session("lang"))

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



                '  Response.Write(viewtype)

            End If

            ' Response.Write(HttpContext.Current.Session.SessionID & "<hr/>")
            If conn.setConnectionForTransaction Then

            Else
                Response.Write("Connection Error")
            End If

            If Page.IsPostBack Then

            Else ' First Time
                If viewtype = "" Or mode = "add" Then
                    tab_tqm.Visible = False
                    tab_dept.Visible = False
                    tab_psm.Visible = False
                    tab_update.Visible = False
                    incident_alert.Visible = False
                    incident_log.Visible = False
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
                    ' panel_department.Enabled = False

                    tab_tqm.Visible = True
                    'disableControl(panel_department)
                    'readonlyControl(panel_department)
                    readonlyControl(panel_psm_concern)

                    txtchangeForm.Visible = True
                    cmdChangeForm.Visible = True
                    lblForm.Visible = True
                ElseIf viewtype = "dept" Then
                    incident_alert.Visible = False
                    incident_log.Visible = False
                    tab_tqm.Visible = False
                    tab_psm.Visible = False
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

                    cmdTQMDraft1.Visible = False
                    cmdTQMDraft2.Visible = False
                    cmdDeptReturn1.Visible = False
                    cmdDeptReturn2.Visible = False

                    ' panel_ir_detail.Enabled = False
                    'panel_form.Enabled = False
                    'panel_additional.Enabled = False

                    'txtdept_tab.Enabled = False

                    'disableControl(panel_ir_detail)
                    'disableControl(panel_form)
                    'disableControl(panel_additional)

                    readonlyControl(panel_ir_detail)
                    readonlyControl(panel_form)
                    readonlyControl(panel_additional)


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
                    tab_tqm.Visible = True
                    '  readonlyControl(tab_tqm)
                    readonlyControl(panelTQM)
                    readonlyControl(panelTQMSub1)
                    ' tab_dept.Visible = False
                    tab_dept.Visible = True

                    incident_alert.Visible = False
                    incident_log.Visible = False
                ElseIf viewtype = "ha" Then
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

                    cmdDeptReturn1.Visible = False
                    cmdDeptReturn2.Visible = False

                    cmdTQMDraft1.Visible = False
                    cmdTQMDraft2.Visible = False

                    incident_alert.Visible = False
                    incident_log.Visible = False
                    'panel_ir_detail.Enabled = False
                    'panel_department.Enabled = False
                    'panel_additional.Enabled = False

                    'panelTQM.Enabled = False
                    'panel_psm.Enabled = False
                    readonlyControl(panel_ir_detail)
                    readonlyControl(panel_department)
                    readonlyControl(panel_additional)
                    readonlyControl(panelTQMSub1)
                    readonlyControl(panelTQM)
                    readonlyControl(panel_psm)
                    readonlyControl(panel_psm_concern)

                Else
                    Response.Write("Are you a hacker ??")
                    Response.End()
                End If

                ' ============================== '
                bindLanguage(lang)
                bindFormHeader()
                bindDept()
                bindGridDept()
                bindFile()
                bindStatus()
                bindHour()
                bindMinute()
                bindLocationCombo()
                bindResponseUnitCombo()

                If mode = "edit" Then

                    bindRevision()
                    bindGridIncidentLog()
                    bindGridAlertLog()
                    bindForm()
                    ' bindDeptTab()

                    bindGridIncidentHistory() ' Repeat IR
                    bindComboDept() ' tab department
                    bindComboGrandTopic() ' tab TQM grand topic
                    ' bindComboTQMDoctor() ' tab TQM doctor
                    bindGridInformationUpdate()
                    'bindDefendantDepartment_Grant()
                    ' bindDefendantDepartment_Select()

                    bindDefendantUnit_Grant()
                    bindDefendantUnit_Select()

                    bindDefendantUnitCombo()
                    bindDefendantUnit_TQM_Combo()

                    If viewtype = "tqm" Or viewtype = "ha" Or viewtype = "psm" Then
                        bindTQMTab() ' tab TQM
                        bindTQMDoctor()
                        bindTQMGridCause()
                        bindTQMPreventiveAction() ' Add date 19/7/2013

                        bindInfoDepartment_Select()
                        bindInfoDepartment_Grant()
                        bindGridRelateDocument("ir")
                        bindGridRelateDocument("cfb")

                        If viewtype = "tqm" Then
                            div_convert.Visible = True
                        End If

                    End If



                    If txtstatus.SelectedValue = "2" Then ' ถ้ายังไม่รับเรื่อง
                        cmdDraft.Visible = False
                        cmdDraft2.Visible = False
                        cmdTQMDraft1.Visible = False
                        cmdTQMDraft2.Visible = False
                        cmdTQMClose1.Visible = False
                        cmdTQMClose2.Visible = False
                        tab_dept.Visible = False
                        tab_psm.Visible = False
                        tab_tqm.Visible = False

                        cmdTQMView2.Text = "รับเรื่อง (Receive case)"
                        cmdTQMView1.Text = "รับเรื่อง (Receive case)"
                    End If

                    If txtstatus.SelectedValue = "9" Then ' ถ้าปิด Case แล้ว
                        '  txtirno.Visible = False
                        Panel1.Visible = False
                        Panel2.Visible = False

                        '  panel_ir_detail.Enabled = False
                        ' panel_form.Enabled = False
                        ' panel_additional.Enabled = False

                        readonlyControl(panel_ir_detail)
                        readonlyControl(panel_form)
                        readonlyControl(panel_additional)

                        If viewtype = "tqm" Then

                            cmdReopen2.Visible = True

                        End If
                    End If

                    If txtstatus.SelectedValue <> "" Then
                        If viewtype = "" And CInt(txtstatus.SelectedValue) > 1 Then
                            '  panel_ir_detail.Enabled = False
                            ' panel_form.Enabled = False
                            ' panel_additional.Enabled = False
                            readonlyControl(panel_ir_detail)
                            readonlyControl(panel_form)
                            readonlyControl(panel_additional)
                        End If

                        If viewtype <> "tqm" And CInt(txtstatus.SelectedValue) > 1 Then
                            GridDept.Columns(3).Visible = False
                            'FileUpload1.Visible = False
                            'cmdUpload.Visible = False
                            cmdDeleteFile.Visible = False
                        End If
                    End If
                    

                    If viewtype = "dept" Then
                        bindDeptTab()
                        bindDeptPreventiveAction()
                        bindDeptGridCause()
                        If CInt(txtstatus.SelectedValue) = 7 Or CInt(txtstatus.SelectedValue) = 9 Then
                            cmdTQMDraft1.Enabled = False
                            cmdTQMDraft2.Enabled = False
                            cmdDeptReturn1.Enabled = False
                            cmdDeptReturn2.Enabled = False
                            'panel_department.Enabled = False
                            readonlyControl(panel_department)
                        End If

                        If txtdept_tab.Items.Count < 2 Then ' ไม่มีรายการ unit defendant
                            ' Response.Write("12xxxxx " & txtstatus.SelectedValue)

                            tab_dept.Visible = False
                            cmdDeptReturn1.Visible = False
                            cmdDeptReturn2.Visible = False
                            cmdTQMDraft1.Visible = False
                            cmdTQMDraft2.Visible = False
                        Else
                            ' Response.Write("xxxxx " & txtstatus.SelectedValue)
                            '   If txtdept_tab.SelectedValue <> Session("dept_id").ToString Or (txtstatus.SelectedValue = "2" Or txtstatus.SelectedValue = "3") Then
                            If Not findArrayValue(costcenter_list, Session("costcenter_id").ToString) Or (txtstatus.SelectedValue = "2" Or txtstatus.SelectedValue = "3") Then
                                '   Response.Write("xxxxx " & Session("costcenter_id").ToString)
                                tab_dept.Visible = False
                                cmdDeptReturn1.Visible = False
                                cmdDeptReturn2.Visible = False
                                cmdTQMDraft1.Visible = False
                                cmdTQMDraft2.Visible = False
                                panel_department.Enabled = False
                                ' ElseIf txtdept_tab.SelectedValue = Session("dept_id").ToString And (txtstatus.SelectedValue = "5" Or txtstatus.SelectedValue = "8") Then
                            ElseIf findArrayValue(costcenter_list, Session("costcenter_id").ToString) And (txtstatus.SelectedValue = "5" Or txtstatus.SelectedValue = "8") Then
                                ' ถ้าเรื่องอยู่ที่ IMCO ด้วย
                                'cmdDeptReturn1.Visible = False
                                ' cmdDeptReturn2.Visible = False
                                ' cmdTQMDraft1.Visible = False
                                'cmdTQMDraft2.Visible = False
                                'panel_department.Enabled = False
                                ' Response.Write("yyyyyy " & txtstatus.SelectedValue)
                                If Not isHasStatusInLog("4") Then ' ถ้าไม่เคยส่งให้ dept investigate
                                    cmdDeptReturn1.Visible = False
                                    cmdDeptReturn2.Visible = False
                                    cmdTQMDraft1.Visible = False
                                    cmdTQMDraft2.Visible = False
                                    'panel_department.Enabled = False
                                    readonlyControl(panel_department)
                                    ' Response.Write("zzzzz " & txtstatus.SelectedValue)
                                    tab_dept.Visible = False
                                    'tab_dept.Visible = True
                                Else
                                    cmdDeptReturn1.Visible = True
                                    cmdDeptReturn2.Visible = True
                                    cmdTQMDraft1.Visible = True
                                    cmdTQMDraft2.Visible = True
                                    panel_department.Enabled = True
                                    tab_dept.Visible = True
                                End If

                            ElseIf txtstatus.SelectedValue = "4" Then
                                cmdDeptReturn1.Visible = True
                                cmdDeptReturn2.Visible = True
                                cmdTQMDraft1.Visible = True
                                cmdTQMDraft2.Visible = True
                                ' panel_department.Enabled = True
                            Else
                                '  panel_department.Enabled = False
                            End If
                        End If

                    End If

                    If viewtype = "tqm" Then
                        '   panel_psm.Enabled = False
                        readonlyControl(panel_psm)
                        readonlyControl(panel_psm_concern)
                    End If

                    If viewtype = "psm" Or viewtype = "tqm" Or viewtype = "ha" Then
                        bindPSMTab()
                        '  bindFileMCO()

                        bindFileMCO()
                        bindPSMConcern()
                        bindMCOCategory()
                        bindDeptPSM()

                      
                    End If

                    If viewtype = "psm" Then
                        cmdMCO.Visible = False
                       

                        If CInt(txtstatus.SelectedValue) = 8 Or CInt(txtstatus.SelectedValue) = 9 Then
                            ' panel_psm.Enabled = False
                            'disableControl(panel_psm)
                            readonlyControl(panel_psm)
                            readonlyControl(panel_psm_concern)
                            cmdPSMReturn1.Visible = False
                            cmdPSMReturn2.Visible = False
                            cmdTQMDraft1.Visible = False
                            cmdTQMDraft2.Visible = False
                        ElseIf txtstatus.SelectedValue = "5" Then
                            '  cmdMCO.Visible = True
                        End If

                        ' panel_ir_detail.Enabled = False
                        ' panel_form.Enabled = False
                        ' panel_additional.Enabled = False
                        readonlyControl(panel_ir_detail)
                        ' readonlyControl(panel_form)
                        panel_form.Enabled = False
                        readonlyControl(panel_additional)

                        'panel_department.Enabled = False
                        readonlyControl(panel_department)
                     

                        '  Response.Write(11111111)
                    End If
                ElseIf mode = "add" Then
                    txtdivision.Text = Session("dept_name").ToString
                    cmdPrint.Visible = False
                    cmdSubmit.Enabled = False
                    cmdSubmit2.Enabled = False
                End If
                End If


                If CInt(formId) = 1 Then
                    cl = LoadControl("FallControl.ascx")
                ElseIf CInt(formId) = 2 Then
                    cl = LoadControl("MedControl.ascx")
               
                ElseIf CInt(formId) = 3 Then
                    cl = LoadControl("PhlebControl.ascx")
                ElseIf CInt(formId) = 4 Then
                    cl = LoadControl("AnesControl.ascx")
                ElseIf CInt(formId) = 5 Then
                    cl = LoadControl("OtherControl.ascx")

                    tabOccurrence.Visible = False
                ElseIf CInt(formId) = 6 Then
                    cl = LoadControl("PressureControl.ascx")
            ElseIf CInt(formId) = 7 Then
                cl = LoadControl("DeleteScanControl.ascx")

                End If


                'CType(cl, incident_FallControl).year = 7
                PlaceHolder1.Controls.Add(cl)

          

            If mode = "edit" Then
                tab_update.Visible = True
                ' If Not IsPostBack Then ' first time
                If CInt(formId) = 1 Then
                    bindFall()
                ElseIf CInt(formId) = 2 Then

                    bindMed()
                    bindMedPeriod()
                    bindDrugWrongName()
                ElseIf CInt(formId) = 3 Then
                    bindPhelbitis()
                ElseIf CInt(formId) = 4 Then
                    bindAnes()
                ElseIf CInt(formId) = 5 Then
                ElseIf CInt(formId) = 6 Then
                    bindPressure()
                ElseIf CInt(formId) = 7 Then
                    bindDeleteTab()
                End If

                'End If
            Else
                tab_update.Visible = False
            End If

            If CInt(formId) = 2 Then
                Dim levelMsg As String = ""
                For com As Integer = 0 To CType(cl.FindControl("txtmed_level"), DropDownList).Items.Count - 1
                    Select Case com
                        Case 0 : levelMsg = "an error occurred but the error did not reach the patient"
                        Case 1 : levelMsg = "an error occurred that reached the patient but did not casuse harm"
                        Case 2 : levelMsg = "an error occurred that reached the patient and required to mornitoring to confirm thait it result in no harm to the patient and/or required intervention to preclude harm"
                        Case 3 : levelMsg = "an error occured that may have contributed to resulted in temporary harm to the patient and required initial or prolonged hospitalization"
                        Case 4 : levelMsg = "an error occurred that may have contributed to or resulted in permanent patient harm"
                        Case 5 : levelMsg = "an error occurred that may have contributed to or resulted in near-death event (e.g anaphylaxis,Cardiac arrest)"
                        Case 6 : levelMsg = "an error occurred that may hanve contributed to or resulted in the patuent's death"
                    End Select
                    CType(cl.FindControl("txtmed_level"), DropDownList).Items(com).Attributes.Add("title", levelMsg)
                Next

                CType(cl.FindControl("txtmed_level"), DropDownList).Attributes.Add("mouserover", "this.title=this.option[this.selectedIndex].title")
            End If

            txtadd_cfbno.Attributes.Add("onkeyup", "data_change(this)")
            txtadd_irno.Attributes.Add("onkeyup", "data_change(this)")

            ' cmdPrintTQM.Attributes.Add("onclick", "window.open('preview_tqm_tab.aspx?irId=" & irId & "')")
            ' Dim t As HtmlInputText = CType(cl.FindControl("txtyear"), HtmlInputText)
                't.Value = 34
            ' tab_tqm.Visible = True
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

        Function isHasStatusInLog(ByVal status_id As String) As Boolean
            Dim sql As String
            Dim ds As New DataSet
            Try
                sql = "SELECT * FROM ir_status_log WHERE ir_id = " & irId
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
                sql = "SELECT * FROM m_language WHERE module_code = 'IR' AND object_id <> 'N/A'"
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    Try
                        CType(panel_ir_detail.FindControl(ds.Tables(0).Rows(i)("object_id").ToString), Label).Text = ds.Tables(0).Rows(i)("name_" & lang).ToString
                    Catch ex As Exception
                        Throw New Exception("ERROR : " & ds.Tables(0).Rows(i)("object_id").ToString)
                    End Try

                Next i

                sql = "SELECT * FROM m_language WHERE module_code = 'IR_RADIO' ORDER BY object_id , lang_id"
                ds = conn.getDataSetForTransaction(sql, "tCombo")
                Dim oName As String = ""
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    oName = ds.Tables(0).Rows(i)("object_id").ToString
                    Try

                        CType(panel_ir_detail.FindControl(ds.Tables(0).Rows(i)("object_id").ToString), RadioButtonList).Items(ds.Tables(0).Rows(i)("order_sort")).Text = ds.Tables(0).Rows(i)("name_" & lang).ToString
                    Catch ex As Exception

                        Try
                            CType(panel_ir_detail.FindControl(ds.Tables(0).Rows(i)("object_id").ToString), DropDownList).Items(ds.Tables(0).Rows(i)("order_sort")).Text = ds.Tables(0).Rows(i)("name_" & lang).ToString

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
        End Sub

        Sub bindFormHeader()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_form_master a "
                sql &= " WHERE a.form_id = " & formId
                ds = conn.getDataSetForTransaction(sql, "t1")

                lblHeader.Text = ds.Tables("t1").Rows(0)("form_name_en").ToString

            Catch ex As Exception

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
                sqlB.Append("SELECT * FROM ir_status_log a LEFT OUTER JOIN ir_status_list b ON a.status_id = b.ir_status_id WHERE a.ir_id = " & irId)
                sqlB.Append(" ORDER BY log_time ASC")
                ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

                '  Response.Write(sqlB.ToString)
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
                sqlB.Append("SELECT * FROM ir_alert_log a  WHERE a.ir_id = " & irId)
                sqlB.Append(" ORDER BY alert_date DESC")
               
                ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

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

        Sub checkLockTransaction()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM cfb_detail_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id WHERE a.ir_id = " & irId
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

        Sub bindForm()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id"
                sql &= " WHERE a.ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")

                txtstatus.SelectedValue = ds.Tables("t1").Rows(0)("status_id").ToString
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

                If viewtype = "" Or viewtype = "tqm" Then
                    lblReport.Text = ds.Tables("t1").Rows(0)("report_by").ToString & " , " & ds.Tables("t1").Rows(0)("date_report").ToString
                    txtcontact.Text = ds.Tables("t1").Rows(0)("report_tel").ToString
                Else
                    lblReport.Text = "-"
                    txtcontact.Visible = False
                End If
               
                txtirno.Text = "" & ds.Tables("t1").Rows(0)("ir_no").ToString
                txtdivision.Text = ds.Tables("t1").Rows(0)("division").ToString
                txtdate_op.Value = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_operation_ts").ToString)
                txtdate_report.Value = ConvertTSToDateString(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString)
                

                txthour.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString, "hour")
                txtmin.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString, "min")
                txthour2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_assessment_ts").ToString, "hour")
                txtmin2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_assessment_ts").ToString, "min")
                txtdate_assessment.Value = ConvertTSToDateString(ds.Tables("t1").Rows(0)("datetime_assessment_ts").ToString)
                txtptage.Value = ds.Tables("t1").Rows(0)("age").ToString
                txtpt_monthday.Value = ds.Tables("t1").Rows(0)("month_day_num").ToString
                txtpt_selectmonthday.SelectedValue = ds.Tables("t1").Rows(0)("month_day_text").ToString
                txttitle.SelectedValue = ds.Tables("t1").Rows(0)("pt_title").ToString
                txtptname.Value = ds.Tables("t1").Rows(0)("pt_name").ToString
                txtptsex.SelectedValue = ds.Tables("t1").Rows(0)("sex").ToString
                txtpthn.Text = ds.Tables("t1").Rows(0)("hn").ToString
                txtroom.Text = ds.Tables("t1").Rows(0)("room").ToString
                Try
                    txtservicetype.SelectedValue = ds.Tables("t1").Rows(0)("service_type").ToString
                Catch ex As Exception

                End Try

                Try
                    txtsegment.SelectedValue = ds.Tables("t1").Rows(0)("customer_segment").ToString
                Catch ex As Exception

                End Try

                Try
                    txtclinical.SelectedValue = ds.Tables("t1").Rows(0)("clinical_type").ToString
                Catch ex As Exception

                End Try

                Try
                    txtpttype.SelectedValue = ds.Tables("t1").Rows(0)("pt_type").ToString
                Catch ex As Exception

                End Try


                txtstatusother.Value = ds.Tables("t1").Rows(0)("pt_type_remark").ToString
                txtdiagnosis.Value = ds.Tables("t1").Rows(0)("diagnosis").ToString
                txtoperation.Value = ds.Tables("t1").Rows(0)("operation").ToString
                txtatt_doctor.Text = ds.Tables("t1").Rows(0)("physician").ToString
                If ds.Tables("t1").Rows(0)("flag_serious").ToString = "1" Then
                    txtserious.Checked = True
                End If

                txtlocation.Text = ds.Tables("t1").Rows(0)("location").ToString
                Try
                    txtlocation_combo.SelectedValue = ds.Tables("t1").Rows(0)("location").ToString
                    ' txtlocation_combo.Items.FindByValue(ds.Tables("t1").Rows(0)("location").ToString).Selected = True
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try

                txtoccurrence.Value = ds.Tables("t1").Rows(0)("describe").ToString
                If ds.Tables("t1").Rows(0)("chk_physician").ToString = "1" Then
                    txtnoti1.Checked = True
                End If
                If ds.Tables("t1").Rows(0)("chk_family").ToString = "1" Then
                    txtnoti2.Checked = True
                End If
                If ds.Tables("t1").Rows(0)("chk_document").ToString = "1" Then
                    txtnoti3.Checked = True
                End If
                txtdoctor.Text = ds.Tables("t1").Rows(0)("doctor_name").ToString
                Try
                    txtdoctype.SelectedValue = ds.Tables("t1").Rows(0)("dotor_type").ToString
                Catch ex As Exception

                End Try

                txtassessment.Value = ds.Tables("t1").Rows(0)("describe_assessment").ToString

                Try
                    txtsevere.SelectedValue = ds.Tables("t1").Rows(0)("severe_id").ToString
                Catch ex As Exception

                End Try

                Try
                    txteffect.SelectedValue = ds.Tables("t1").Rows(0)("severe_other_id").ToString
                Catch ex As Exception

                End Try


                txteffect_detail.Value = ds.Tables("t1").Rows(0)("severe_other_remark").ToString

                If ds.Tables("t1").Rows(0)("chk_xray").ToString = "1" Then
                    txtxray.Checked = True
                End If
                If ds.Tables("t1").Rows(0)("chk_lab").ToString = "1" Then
                    txtlab.Checked = True
                End If
                If ds.Tables("t1").Rows(0)("chk_other").ToString = "1" Then
                    txtother.Checked = True
                End If

                txtxray_detail.Value = ds.Tables("t1").Rows(0)("xray_detail").ToString
                txtlab_detail.Value = ds.Tables("t1").Rows(0)("lab_detail").ToString
                txtother_detail.Value = ds.Tables("t1").Rows(0)("other_detail").ToString
                txtxray_result.Value = ds.Tables("t1").Rows(0)("xray_result").ToString
                txtlab_result.Value = ds.Tables("t1").Rows(0)("lab_result").ToString
                txtother_result.Value = ds.Tables("t1").Rows(0)("other_result").ToString

                txtrecommend.Value = ds.Tables("t1").Rows(0)("recommend_detail").ToString
                txtaction.Value = ds.Tables("t1").Rows(0)("describe_action").ToString

                txtinitial.Value = ds.Tables("t1").Rows(0)("initial_action").ToString

                Try
                    txtresult_action.SelectedValue = ds.Tables("t1").Rows(0)("action_result_id").ToString
                Catch ex As Exception

                End Try

                txtactionremark.Value = ds.Tables("t1").Rows(0)("action_result_remark").ToString
                txtcountry.Text = ds.Tables("t1").Rows(0)("country").ToString
                ' Response.Write(ds.Tables("t1").Rows(0)("date_report_ts").ToString)

                txtresponse_unit.SelectedValue = ds.Tables("t1").Rows(0)("new_dept_relate_name").ToString
                txtsafegoal.Text = ds.Tables("t1").Rows(0)("new_safe_goal").ToString
                txtissue.Text = ds.Tables("t1").Rows(0)("new_issue").ToString
                txtpayor.Text = ds.Tables("t1").Rows(0)("new_payor").ToString


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

                If viewtype = "tqm" And ds.Tables(0).Rows(0)("is_move_to_cfb").ToString = "1" Then
                    is_lock = "1"
                End If

            Catch ex As Exception
                Response.Write(ex.Message & " : " & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindFile()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_attachment a WHERE a.form_id = " & formId
                If mode = "add" Then
                    sql &= " AND a.session_id = '" & session_id & "'"
                Else
                    sql &= " AND a.ir_id = " & irId
                End If
                ' Response.Write(sql)

                ds = conn.getDataSetForTransaction(sql, "t1")

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

        Sub bindFileMCO()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_mco_attachment a WHERE 1 = 1 "
                If mode = "add" Then
                    sql &= " AND a.session_id = '" & session_id & "'"
                Else
                    sql &= " AND a.ir_id = " & irId
                End If
                ' Response.Write(sql)

                ds = conn.getDataSetForTransaction(sql, "t1")

                GridFileMCO.DataSource = ds.Tables(0)
                GridFileMCO.DataBind()

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
                sql = "SELECT * FROM ir_topic_grand a WHERE 1 = 1 "
                sql &= " AND topic_type = 'ir'"
               
                ds = conn.getDataSetForTransaction(sql, "t1")
                txtgrandtopic.DataSource = ds
                txtgrandtopic.DataBind()
                '  Response.Write(sql)
                txtgrandtopic.Items.Insert(0, New ListItem(" -- Please select -- ", "0"))

            Catch ex As Exception
                Response.Write("bindComboDept :: " & ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindComboNormalTopic()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_topic a WHERE grand_topic_id = " & txtgrandtopic.SelectedValue


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

        Sub bindComboDept()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_relate_dept a WHERE 1 = 1 AND  ISNULL(is_dept_delete,0) = 0 "
                If mode = "edit" Then
                    sql &= " AND a.ir_id = " & irId
                End If
                If viewtype = "dept" Then
                    'sql &= " AND a.dept_id = " & Session("dept_id").ToString
                    sql &= " AND a.dept_id IN (SELECT costcenter_id FROM user_access_costcenter WHERE emp_code = " & Session("emp_code").ToString & " ) "
                End If
                ds = conn.getDataSetForTransaction(sql, "t1")
                txtdept_tab.DataSource = ds
                txtdept_tab.DataBind()

                txtdept_tab.Items.Insert(0, New ListItem(" -- Please select -- ", "0"))

                If viewtype = "dept" Then
                    txtdept_tab.SelectedValue = Session("dept_id").ToString
                End If

                If ds.Tables("t1").Rows.Count = 0 Then
                    cmdAddPrevent.Enabled = False
                    cmdDeptAddCause.Enabled = False
                End If

                'Response.Write(sql)
            Catch ex As Exception
                Response.Write("bindComboDept :: " & ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindGridDept()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * , CASE WHEN is_investigate = 1 THEN 'Investigated' ELSE 'N/A' END AS status FROM ir_relate_dept a WHERE 1 = 1 AND ISNULL(is_dept_delete,0) = 0 "
                If mode = "add" Then
                    sql &= " AND ir_id is null AND a.session_id = '" & session_id & "'"
                Else
                    sql &= " AND a.ir_id = " & irId
                End If
                'Response.Write(sql)

                ds = conn.getDataSetForTransaction(sql, "t1")

                GridDept.DataSource = ds.Tables(0)
                GridDept.DataBind()

                If (ds.Tables("t1").Rows.Count = 0) Then
                    ' cmdConvert.Enabled = False
                Else
                    '   cmdConvert.Enabled = True

                End If

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
                sql = "SELECT * FROM ir_status_list"
                'sql &= " ORDER BY dept_name"
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                txtstatus.DataSource = ds
                txtstatus.DataBind()

                txtstatus.Items.Insert(0, New ListItem("----", ""))

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
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

        Sub bindDept() ' Combo box
            Dim ds As New DataSet
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "SELECT * FROM user_dept ORDER BY dept_name_en ASC"
                'sql &= " ORDER BY dept_name"
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                txtadd_dept.DataSource = ds
                txtadd_dept.DataBind()

                txtadd_dept.Items.Insert(0, New ListItem("--Please select--", ""))

            Catch ex As Exception
                Response.Write(ex.Message)
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

            ' txthour.Items.Insert(0, New ListItem("hh", ""))
            ' txthour2.Items.Insert(0, New ListItem("hh", ""))
        End Sub

        Private Sub bindMinute()
            Dim i As Integer = 0
            Dim i_str As String = ""
            For i = 0 To 59
                i_str = i.ToString
                txtmin.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
                txtmin2.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
               
            Next

            ' txtmin.Items.Insert(0, New ListItem("mm", ""))
            'txtmin2.Items.Insert(0, New ListItem("mm", ""))
            'txtmin2.Items.Insert(0, New ListItem("-", "0"))
            'txtmin2.SelectedIndex = 0

        End Sub

    

        Private Sub bindGridRepeatHistory() ' CFB Repeat
            Dim ds As New DataSet
            Dim sql As String
            Dim sqlB As New StringBuilder

            Try
                sqlB.Append("SELECT a.hn ,a.datetime_report ,b.comment_type_name , c.dept_name , a.cfb_no , a.ir_id FROM cfb_detail_tab a INNER JOIN cfb_comment_list b ON a.ir_id = b.ir_id ")
                sqlB.Append(" LEFT OUTER JOIN cbf_relate_dept c ON b.comment_id = c.comment_id WHERE 1 = 1 ")
                If txtpthn.Text = "" Then
                    sqlB.Append(" AND  1 > 2 ")
                Else
                    sqlB.Append(" AND a.hn = '" & txtpthn.Text & "'")
                End If

                sqlB.Append(" GROUP BY a.hn ,a.datetime_report ,b.comment_type_name,a.datetime_report_ts, c.dept_name, a.cfb_no , a.ir_id ")
                sqlB.Append(" ORDER BY a.datetime_report_ts DESC")
                ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

                'Response.Write(sqlB.ToString)


                If ds.Tables(0).Rows.Count <= 0 Then
                    incidentWarn.InnerHtml &= "There is no customer feedback for this patient."
                Else
                    If isAccessToLinkClick() Then
                        incidentWarn.InnerHtml &= "This patient has customer feedback on " & ds.Tables(0).Rows(0)("datetime_report").ToString & " (<a href='../cfb/form_cfb.aspx?mode=edit&cfbId=" & ds.Tables(0).Rows(0)("ir_id").ToString & "'>CFB NO. " & ds.Tables(0).Rows(0)("cfb_no").ToString & "</a>) "
                    Else
                        incidentWarn.InnerHtml &= "This patient has customer feedback on " & ds.Tables(0).Rows(0)("datetime_report").ToString & " (<asp:Label" & ds.Tables(0).Rows(0)("ir_id").ToString & "'>CFB NO. " & ds.Tables(0).Rows(0)("cfb_no").ToString & "</asp:Label>) "
                    End If
                End If

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
                sqlB.Append("SELECT  b.hn ,b.datetime_report ,c.form_name_en , d.severe_name , b.datetime_report_ts , b.ir_no , a.ir_id , a.form_id FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id ")
                sqlB.Append(" INNER JOIN ir_form_master c ON c.form_id = a.form_id")
                sqlB.Append(" LEFT OUTER JOIN ir_tqm_tab tqm ON tqm.ir_id = a.ir_id")
                sqlB.Append(" LEFT OUTER JOIN ir_m_severity d ON d.severe_id = tqm.severe_level_id WHERE 1 = 1 ")
                If txtpthn.Text = "" Then
                    sqlB.Append("  AND 1 > 2 ")
                Else
                    sqlB.Append("  AND b.hn = '" & txtpthn.Text & "' AND a.status_id > 1 AND a.ir_id <> " & irId)
                End If

                sqlB.Append(" GROUP BY b.hn ,b.datetime_report ,c.form_name_en , d.severe_name ,  b.datetime_report_ts , b.ir_no , a.ir_id , a.form_id")
                sqlB.Append(" ORDER BY b.datetime_report_ts DESC")
                ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

                'Response.Write(sqlB.ToString)
                GridRepeatIncident.DataSource = ds
                GridRepeatIncident.DataBind()

                If ds.Tables(0).Rows.Count <= 0 Then
                    incidentWarn.InnerHtml = "There is no repeated incident for this patient.<br/>"
                Else
                    incidentWarn.InnerHtml = "This patient has " & ds.Tables(0).Rows.Count & " repeated incident, last incident was reported on " & ds.Tables(0).Rows(0)("datetime_report").ToString & ". <br/>"
                End If

                bindGridRepeatHistory()

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try

        End Sub

        Private Sub bindGridInformationUpdate()
            Dim ds As New DataSet
            Dim sql As String
            Dim sqlB As New StringBuilder

            Try
                sqlB.Append("SELECT * FROM ir_information_update WHERE ir_id =  " & irId)
               
              
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
                        sql = "SELECT ISNULL(MAX(file_id),0) + 1 AS pk FROM ir_attachment"
                        ds = conn.getDataSetForTransaction(sql, "t1")
                        pk = ds.Tables("t1").Rows(0)(0).ToString
                    Catch ex As Exception
                        Response.Write(ex.Message)
                        Response.Write(sql)
                    Finally
                        ds.Dispose()
                        ds = Nothing
                    End Try


                    sql = "INSERT INTO ir_attachment (file_id , ir_id , form_id , file_name , file_path , file_size , session_id) VALUES("
                    sql &= "" & pk & " , "
                    If irId = "" Then
                        sql &= " null , "
                    Else
                        sql &= "" & irId & " , "
                    End If
                    sql &= "" & formId & " , "
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

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/incident/attach_file/" & pk & "." & extension))

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

                    '  response.write(lbl.Text)
                    If chk.Checked Then
                        sql = "DELETE FROM ir_attachment WHERE file_id = " & lbl.Text
                        errorMsg = conn.executeSQLForTransaction(sql)
                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg)
                            'Exit For
                        End If
                    End If

                   
                Next s

                For s As Integer = 0 To i - 1
                    chk = CType(GridFile.Rows(s).FindControl("chkSelect"), CheckBox)
                    If chk.Checked Then
                        lblFilePath = CType(GridFile.Rows(s).FindControl("lblFilePath"), Label)
                        File.Delete(Server.MapPath("../share/incident/attach_file/" & lblFilePath.Text))
                    End If

                Next s

                conn.setDBCommit()
                bindFile()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
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

        Protected Sub onSave(ByVal sender As Object, ByVal e As CommandEventArgs)
            Try
              
                updateTransList(e.CommandArgument.ToString)

                If viewtype = "" Or viewtype = "tqm" Then
                    'Response.Write("r")
                    updateIncidentDetail(e.CommandArgument.ToString)
                    updateAttachFile() ' รายการไฟล์ attach
                    updateDepartment() ' รายการ dept ใน incident detail
                    'Response.Write("y")
                    If CInt(formId) = 1 Then
                        isHasRow("ir_fall_tab")
                        updateFall()
                    ElseIf CInt(formId) = 2 Then
                        isHasRow("ir_med_tab")
                        updateMed()
                        updateMedPeriod()
                        updateMedDrugWrongName()
                    ElseIf CInt(formId) = 3 Then
                        isHasRow("ir_phlebitis_tab")
                        updatePhelbitis()
                    ElseIf CInt(formId) = 4 Then
                        isHasRow("ir_anes_tab")
                        updateAnes()
                    ElseIf CInt(formId) = 5 Then
                        'updateOther("ir_phlebitis_tab")
                    ElseIf CInt(formId) = 6 Then
                        isHasRow("ir_pressure_tab")
                        updatePressure()
                    ElseIf CInt(formId) = 7 Then
                        isHasRow("ir_delete_tab")
                        updateDeleteTab()
                        updateDeleteScan()
                    End If

                    If viewtype = "tqm" Then

                        isHasRow("ir_tqm_tab")
                        'Response.Write("h")
                        If mode = "edit" Then
                            updateTQMTab()
                            updateInfoDepartment()

                            updateDefendantUnit()

                            updateDeptTab() ' ir_relate_dept
                        End If
                        'Response.Write("m")
                    End If
                End If
                'Response.Write("t")
                If viewtype = "dept" Then

                    Dim txt_addprevent As HtmlTextArea
                    txt_addprevent = CType(GridViewPrevent.FooterRow.FindControl("txt_addprevent"), HtmlTextArea)
                    'Response.Write("cccc")
                    If txt_addprevent.Value <> "" Then

                        addPreventNoCommit()

                    End If
                    ' Response.Write("y")
                    'saveOrderDeptNoCommit()
                    ' Response.Write("t")
                    updateDeptTab() ' ir_relate_dept
                    '  Response.Write("z")
                End If
                'Response.Write("p")
                If viewtype = "psm" Then
                    isHasRow("ir_psm_tab")
                    updatePSMTab()
                End If
                ' Response.Write("x")


                If e.CommandArgument.ToString = "3" And txtstatus.SelectedValue = "2" Then ' รับเรื่องครั้งแรก
                    updateCaseOwner()
                    'conn.setDBCommit()
                End If

                conn.setDBCommit()



                ' Response.Redirect("form_incident.aspx?mode=edit&irId=" & irId & "&formId=" & formId)
                'If mode = "add" Then
                '    Response.Redirect("form_incident.aspx?mode=edit&irId=" & irId & "&formId=" & formId)
                'End If
                'bindFile()
                'bindGridDept()
                'bindForm()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write("onSave : " & ex.Message)
                Return
                'Response.End()
            End Try

            Response.Redirect("form_incident.aspx?mode=edit&irId=" & irId & "&formId=" & formId)
        End Sub

        Sub updateStatusList(ByVal status_column As String, ByVal status_id As String)
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""

            sql = "UPDATE ir_trans_list SET " & status_column & " = '" & status_id & "'"
            ' sql &= " , age = " & txtptage.Value & ""

            sql &= " WHERE ir_id = " & irId

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        End Sub

        Sub updateTransList(ByVal status_id As String, Optional ByVal log_remark As String = "")
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet
           
            If mode = "add" Then
                Try
                    sql = "SELECT ISNULL(MAX(ir_id),0) + 1 AS pk FROM ir_trans_list"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                    new_ir_id = pk
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO ir_trans_list (ir_id , form_id ,  date_report , date_submit , date_submit_ts , status_id , report_type , report_by , report_emp_code )"
                sql &= " VALUES("
                sql &= "" & pk & " ,"
                sql &= "" & formId & " ,"

                sql &= " GETDATE() ,"
                If status_id = "2" Then
                    sql &= " GETDATE() ,"
                    sql &= " " & Date.Now.Ticks & " ,"
                Else
                    sql &= " null ,"
                    sql &= " null ,"
                End If

                sql &= "" & status_id & " ,"
                sql &= " 'ir' ,"
                sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"
                sql &= "'" & Session("emp_code").ToString & "' "

                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                irId = pk
                updateOnlyLog(status_id, log_remark)
            Else
                If status_id <> "" And status_id <> "1" Then ' ถ้าไม่ใช่ save draft

                    sql = "UPDATE ir_trans_list SET is_change_to_draft = 0 WHERE ir_id = " & irId
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If

                    If status_id = "7" Then ' Dept Investigated, click Save and return to TQM
                        sql = "UPDATE ir_relate_dept SET is_investigate = 1 WHERE ir_id = " & irId & " "
                        '   sql &= " AND dept_id IN (SELECT costcenter_id FROM user_access_costcenter WHERE emp_code = " & Session("emp_code").ToString & ")"
                        sql &= " AND dept_id = " & txtdept_tab.SelectedValue

                        ' Response.Write(sql)
                        errorMsg = conn.executeSQLForTransaction(sql)
                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg)
                        End If

                    End If

                    If viewtype = "dept" Then
                        sql = "SELECT * FROM ir_relate_dept WHERE ir_id = " & irId & " AND is_investigate = 1 AND ISNULL(is_dept_delete,0) = 0 "
                        ds = conn.getDataSetForTransaction(sql, "tDept")



                        If GridDept.Rows.Count = ds.Tables("tDept").Rows.Count Then
                            'Response.Write("xxxxgrid = " & GridDept.Rows.Count)
                            updateTranListLog(status_id, log_remark)

                        End If

                        updateOnlyLog(status_id, log_remark)

                        If status_id = "7" Then ' Dept Investigated
                            getMailBackToTQM()
                        End If

                        ds.Clear() : ds.Dispose()

                    Else ' ถ้าไม่ใช่ manager
                        ' If txtstatus.SelectedValue = "2" Or status_id = "2" Or status_id = "9" Then ' ถ้าเป็น Submitted to TQM จะเป็น status เป็น TQM Receive เสมอ
                        ' updateTranListLog(status_id, log_remark)
                        ' updateOnlyLog(status_id, log_remark)
                        'Else ' ถ้าเป็น Status อื่น จะเป็น Log อย่างเดียว
                        'updateOnlyLog(status_id, log_remark)

                        If status_id = "2" Or status_id = "4" Or status_id = "5" Or status_id = "7" Or status_id = "8" Or status_id = "9" Or status_id = "91" Then
                            updateTranListLog(status_id, log_remark)
                            updateOnlyLog(status_id, log_remark)
                        ElseIf status_id = "3" And txtstatus.SelectedValue = "2" Then ' รับเรื่องครั้งแรก
                            'updateTranListLog(status_id, log_remark)
                            'updateOnlyLog(status_id, log_remark)

                            'isHasRow("ir_tqm_tab")
                          
                            ' updateRevision()
                            'saveHTMLForPDF(revision_no)
                        Else


                            '  updateRevision()
                            ' updateOnlyLog(status_id, "Revision : " & revision_no)
                            ' saveHTMLForPDF(revision_no)
                        End If

                      
                    End If

                Else ' ถ้าเป็น  status = Draft

                   
                End If

            End If


        End Sub

        Sub updateCaseOwner()
            Dim sql As String
            Dim errorMsg As String

            sql = "UPDATE ir_tqm_tab SET tqm_case_owner = '" & addslashes(Session("user_fullname").ToString) & "'"
            '  sql &= " , report_tel = '" & txtcontact.Text & "' "
            sql &= " WHERE ir_id = " & irId

            '  Response.Write(sql)
            ' Response.End()
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
        End Sub

        Sub updateTranListLog(ByVal status_id As String, Optional ByVal log_remark As String = "")
            Dim sql As String
            Dim errorMsg As String

            sql = "UPDATE ir_trans_list SET status_id = '" & status_id & "'"
            '  sql &= " , report_tel = '" & txtcontact.Text & "' "
            If status_id = "9" Then
                sql &= " , date_close = GETDATE() "
                sql &= " , date_close_ts = " & Date.Now.Ticks & " "
            End If

            If (status_id = "2") And txtstatus.SelectedValue = "1" Then
                sql &= " , date_submit = GETDATE() "
                sql &= " , date_submit_ts = " & Date.Now.Ticks & " "
            End If

            sql &= " WHERE ir_id = " & irId

            'Response.Write(sql)
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
            sql &= "'" & irId & "' ,"
            sql &= "GETDATE() ,"
            sql &= "'" & Date.Now.Ticks & "' ,"
            sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"
            sql &= "'" & addslashes(Session("user_position").ToString) & "' ,"
            sql &= "'" & addslashes(Session("dept_name").ToString) & "' ,"
            sql &= "'" & log_remark & "' "
            sql &= ")"
            'Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sql)
            End If
        End Sub

        Sub updateStatusNeedInvestigate()
            Dim sql As String = ""
            Dim errorMsg As String


            sql = "UPDATE ir_relate_dept SET is_investigate = 0 WHERE ir_id = " & irId
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


            sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ir_revision WHERE ir_id = " & irId
            ds = conn.getDataSetForTransaction(sql, "t1")
            order_sort = ds.Tables(0).Rows(0)(0).ToString

            Dim irno As String
            If public_ir_id <> "" Then
                irno = "" & public_ir_id & "_" & Date.Now.Year & Date.Now.Month.ToString.PadLeft(2, "0") & Date.Now.Day.ToString.PadLeft(2, "0") & "_revision_" & order_sort
            Else
                irno = "" & txtirno.Text & "_" & Date.Now.Year & Date.Now.Month.ToString.PadLeft(2, "0") & Date.Now.Day.ToString.PadLeft(2, "0") & "_revision_" & order_sort
            End If

            revision_no = irno

            pk = getPK("revision_id", "ir_revision", conn)
            sql = "INSERT INTO ir_revision (revision_id , ir_id , ir_no, revision_by_name , revision_by_empno , revision_date , revision_date_ts , file_name , order_sort) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & irId & "' ,"
            sql &= " '" & txtirno.Text.Replace("IR", "") & "' ,"
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

            ' saveHTMLForPDF(irno)
        End Sub

        Sub bindRevision()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_revision WHERE ir_id = " & irId
                sql &= " ORDER BY revision_id "
                ds = conn.getDataSetForTransaction(sql, "t1")

                GridRevision.DataSource = ds
                GridRevision.DataBind()

                lblRevisionNum.Text = "(" & ds.Tables(0).Rows.Count & ")"
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub updateIncidentDetail(ByVal status_id As String)
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet
            Dim new_ir_no As String = ""

            If status_id = "2" Then
                new_ir_no = getNewIRNo()
                public_ir_id = new_ir_no
            End If

            If mode = "add" Then

                sql = "INSERT INTO ir_detail_tab (ir_id , ir_no , division , dept_id ,report_tel, pt_title , pt_name , age , month_day_num , month_day_text , sex , hn"
                sql &= ",room , service_type , customer_segment , clinical_type , pt_type , pt_type_remark , diagnosis , operation , date_operation , date_operation_ts"
                sql &= ",physician , datetime_report , datetime_report_ts , flag_serious , location , describe , chk_physician"
                sql &= ",chk_family , chk_document , doctor_name , dotor_type , datetime_assessment , datetime_assessment_ts , describe_assessment"
                sql &= ",xray_detail , lab_detail , other_detail , chk_xray , chk_lab , chk_other , xray_result"
                sql &= ",lab_result , other_result , recommend_detail , describe_action , severe_id , severe_other_id , severe_other_remark"
                sql &= ",initial_action , action_result_id , action_result_remark , country "
                sql &= " , new_dept_relate_name , new_safe_goal  , new_payor , new_issue"
                sql &= ") VALUES("
                sql &= "" & new_ir_id & " ,"
                sql &= "'" & new_ir_no & "' ,"
                sql &= "'" & addslashes(txtdivision.Text) & "' ,"
                sql &= "'" & Session("dept_id").ToString & "' ,"
                sql &= "'" & addslashes(txtcontact.Text) & "' ,"
                sql &= "'" & txttitle.SelectedValue & "' ,"
                sql &= "'" & addslashes(txtptname.Value) & "' ,"
                sql &= "'" & txtptage.Value & "' ,"
                sql &= "'" & txtpt_monthday.Value & "' ,"
                sql &= "'" & txtpt_selectmonthday.SelectedValue & "' ,"
                sql &= "'" & txtptsex.SelectedValue & "' ,"
                sql &= "'" & addslashes(txtpthn.Text) & "' ,"
                sql &= "'" & addslashes(txtroom.Text) & "' ,"
                sql &= "'" & txtservicetype.SelectedValue & "' ,"
                sql &= "'" & txtsegment.SelectedValue & "' ,"
                sql &= "'" & txtclinical.SelectedValue & "' ,"
                sql &= "'" & txtpttype.SelectedValue & "' ,"
                sql &= "'" & addslashes(txtstatusother.Value) & "' ,"
                sql &= "'" & addslashes(txtdiagnosis.Value) & "' ,"
                sql &= "'" & addslashes(txtoperation.Value) & "' ,"
                sql &= "'" & convertToSQLDatetime(txtdate_op.Value) & "' ,"
                sql &= "'" & ConvertDateStringToTimeStamp(txtdate_op.Value) & "' ,"
                sql &= "'" & addslashes(txtatt_doctor.Text) & "' ,"
                sql &= "'" & convertToSQLDatetime(txtdate_report.Value, txthour.SelectedValue.PadLeft(2, "0"), txtmin.SelectedValue.PadLeft(2, "0")) & "' ,"
                sql &= "'" & ConvertDateStringToTimeStamp(txtdate_report.Value, CInt(txthour.SelectedValue), CInt(txtmin.SelectedValue)) & "' ,"
                If txtserious.Checked = True Then
                    sql &= "  1 ,"
                Else
                    sql &= "  0 ,"
                End If
                sql &= "'" & addslashes(txtlocation.Text) & "' ,"
                'sql &= "'" & addslashes(txtlocation_combo.Text) & "' ,"
                sql &= "'" & addslashes(txtoccurrence.Value) & "' ,"
                If txtnoti1.Checked = True Then
                    sql &= " 1 ,"
                Else
                    sql &= " 0 ,"
                End If
                If txtnoti2.Checked = True Then
                    sql &= "  1 ,"
                Else
                    sql &= "  0 ,"
                End If
                If txtnoti3.Checked = True Then
                    sql &= "  1 ,"
                Else
                    sql &= "  0 ,"
                End If
                sql &= "'" & addslashes(txtdoctor.Text) & "' ,"
                sql &= "'" & txtdoctype.SelectedValue & "' ,"
                sql &= "'" & convertToSQLDatetime(txtdate_assessment.Value, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "' ,"
                sql &= "'" & ConvertDateStringToTimeStamp(txtdate_assessment.Value, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & "' ,"
                sql &= "'" & addslashes(txtassessment.Value) & "' ,"
                sql &= "'" & addslashes(txtxray_detail.Value) & "' ,"
                sql &= "'" & addslashes(txtlab_detail.Value) & "' ,"
                sql &= "'" & addslashes(txtother_detail.Value) & "' ,"
                If txtxray.Checked = True Then
                    sql &= "  1 ,"
                Else
                    sql &= "  0 ,"
                End If
                If txtlab.Checked = True Then
                    sql &= "  1 ,"
                Else
                    sql &= "  0 ,"
                End If
                If txtother.Checked = True Then
                    sql &= "  1 ,"
                Else
                    sql &= "  0 ,"
                End If

                sql &= "'" & addslashes(txtxray_result.Value) & "' ,"
                sql &= "'" & addslashes(txtlab_result.Value) & "' ,"
                sql &= "'" & addslashes(txtother_result.Value) & "' ,"
                sql &= "'" & addslashes(txtrecommend.Value) & "' ,"
                sql &= "'" & addslashes(txtaction.Value) & "' ,"
                sql &= "'" & addslashes(txtsevere.SelectedValue) & "' ,"
                sql &= "'" & txteffect.SelectedValue & "' ,"
                sql &= "'" & addslashes(txteffect_detail.Value) & "' ,"
                sql &= "'" & addslashes(txtinitial.Value) & "' ,"
                sql &= "'" & addslashes(txtresult_action.SelectedValue) & "' ,"
                sql &= "'" & addslashes(txtactionremark.Value) & "' , "
                sql &= "'" & addslashes(txtcountry.Text) & "' , "

                sql &= "'" & txtresponse_unit.SelectedValue.ToString & "' , "
                sql &= "'" & addslashes(txtsafegoal.Text) & "' , "
                sql &= "'" & addslashes(txtpayor.Text) & "' , "
                sql &= "'" & addslashes(txtissue.Text) & "'  "

                sql &= ")"

                'Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & " " & sql)
                End If

                irId = new_ir_id
            Else ' Mode = edit
                sql = "UPDATE ir_detail_tab SET pt_name = '" & addslashes(txtptname.Value) & "'"
                sql &= " , pt_title = '" & txttitle.SelectedValue & "'"
                sql &= " , age = '" & txtptage.Value & "'"
                sql &= " , month_day_num = '" & txtpt_monthday.Value & "'"
                sql &= " , month_day_text = '" & txtpt_selectmonthday.SelectedItem.Text & "'"
                sql &= " , sex = '" & txtptsex.SelectedValue & "'"
                sql &= " , hn = '" & addslashes(txtpthn.Text) & "'"
                sql &= " , room = '" & addslashes(txtroom.Text) & "'"
                sql &= " , service_type = '" & txtservicetype.SelectedValue & "'"
                sql &= " , customer_segment = '" & txtsegment.SelectedValue & "'"
                sql &= " , clinical_type = '" & txtclinical.SelectedValue & "'"
                sql &= " , pt_type = '" & txtpttype.SelectedValue & "'"
                sql &= " , pt_type_remark = '" & addslashes(txtstatusother.Value) & "'"
                If status_id = "2" Then
                    If txtirno.Text = "" Or txtirno.Text = "0" Then
                        sql &= " , ir_no = '" & new_ir_no & "'"
                    End If

                End If
                sql &= " , division = '" & addslashes(txtdivision.Text) & "'"
                If mode = "add" Then
                    sql &= " , dept_id = '" & Session("dept_id").ToString & "'"
                End If
                sql &= " , report_tel = '" & addslashes(txtcontact.Text) & "'"
                sql &= " , diagnosis = '" & addslashes(txtdiagnosis.Value) & "'"
                sql &= " , operation = '" & addslashes(txtoperation.Value) & "'"
                sql &= " , date_operation = '" & convertToSQLDatetime(txtdate_op.Value) & "'"
                sql &= " , date_operation_ts = '" & ConvertDateStringToTimeStamp(txtdate_op.Value) & "'"
                sql &= " , physician = '" & addslashes(txtatt_doctor.Text) & "'"
                sql &= " , datetime_report = '" & convertToSQLDatetime(txtdate_report.Value, txthour.SelectedValue.PadLeft(2, "0"), txtmin.SelectedValue.PadLeft(2, "0")) & "'"
                sql &= " , datetime_report_ts = '" & ConvertDateStringToTimeStamp(txtdate_report.Value, CInt(txthour.SelectedValue), CInt(txtmin.SelectedValue)) & "'"
                If txtserious.Checked = True Then
                    sql &= " , flag_serious = 1 "
                Else
                    sql &= " , flag_serious = 0 "
                End If
                sql &= " , location = '" & addslashes(txtlocation.Text) & "'"
                'sql &= " , location = '" & addslashes(txtlocation_combo.Text) & "'"
                sql &= " , describe = '" & addslashes(txtoccurrence.Value) & "'"
                If txtnoti1.Checked = True Then
                    sql &= " , chk_physician = 1 "
                Else
                    sql &= " , chk_physician = 0 "
                End If
                If txtnoti2.Checked = True Then
                    sql &= " , chk_family = 1 "
                Else
                    sql &= " , chk_family = 0 "
                End If
                If txtnoti3.Checked = True Then
                    sql &= " , chk_document = 1 "
                Else
                    sql &= " , chk_document = 0 "
                End If
                sql &= " , doctor_name = '" & addslashes(txtdoctor.Text) & "'"
                sql &= " , dotor_type = '" & txtdoctype.SelectedValue & "'"
                sql &= " , datetime_assessment = '" & convertToSQLDatetime(txtdate_assessment.Value, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "'"
                sql &= " , datetime_assessment_ts = " & ConvertDateStringToTimeStamp(txtdate_assessment.Value, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & ""
                sql &= " , describe_assessment = '" & addslashes(txtassessment.Value) & "'"
                If txtxray.Checked = True Then
                    sql &= ", chk_xray =  1 "
                Else
                    sql &= ", chk_xray = 0 "
                End If
                If txtlab.Checked = True Then
                    sql &= ", chk_lab = 1 "
                Else
                    sql &= ", chk_lab = 0 "
                End If
                If txtother.Checked = True Then
                    sql &= ", chk_other = 1 "
                Else
                    sql &= ", chk_other = 0 "
                End If

                sql &= " , xray_detail = '" & addslashes(txtxray_detail.Value) & "'"
                sql &= " , lab_detail = '" & addslashes(txtlab_detail.Value) & "'"
                sql &= " , other_detail = '" & addslashes(txtother_detail.Value) & "'"
                sql &= " , xray_result = '" & addslashes(txtxray_result.Value) & "'"
                sql &= " , lab_result = '" & addslashes(txtlab_result.Value) & "'"
                sql &= " , other_result = '" & addslashes(txtother_result.Value) & "'"
                sql &= " , recommend_detail = '" & addslashes(txtrecommend.Value) & "'"
                sql &= " , describe_action = '" & addslashes(txtaction.Value) & "'"
                sql &= " , severe_id = '" & txtsevere.SelectedValue & "'"
                sql &= " , severe_other_id = '" & addslashes(txteffect.SelectedValue) & "'"
                sql &= " , severe_other_remark = '" & addslashes(txteffect_detail.Value) & "'"
                sql &= " , initial_action = '" & addslashes(txtinitial.Value) & "'"
                sql &= " , action_result_id = '" & txtresult_action.SelectedValue & "'"
                sql &= " , action_result_remark = '" & addslashes(txtactionremark.Value) & "'"
                sql &= " , country = '" & txtcountry.Text & "'"

                sql &= " , new_dept_relate_name = '" & txtresponse_unit.SelectedValue.ToString & "'"
                sql &= " , new_safe_goal = '" & addslashes(txtsafegoal.Text) & "'"
                sql &= " , new_payor = '" & txtpayor.Text & "'"
                sql &= " , new_issue = '" & txtissue.Text & "'"

                sql &= " WHERE ir_id = " & irId
                'Response.Write(sql)
                ' Response.End()
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & ":" & sql)
                End If
                End If
        End Sub

        Sub updateAttachFile()
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            If mode = "add" Then


                sql = "UPDATE ir_attachment SET ir_id = " & irId & " , session_id = '' WHERE session_id = '" & Session("session_myid").ToString & "'"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & "" & sql)
                End If
            Else
                sql = "UPDATE ir_attachment SET  session_id = '' WHERE ir_id = '" & irId & "'"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & "" & sql)
                End If
            End If
        End Sub

        Sub updateDepartment()
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            If mode = "add" Then


                sql = "UPDATE ir_relate_dept SET ir_id = " & irId & " , session_id = null WHERE session_id = '" & Session("session_myid").ToString & "'"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & "" & sql)
                End If


            End If
        End Sub

        Sub isHasRow(ByVal table As String)
            Dim sql As String
            Dim ds As New DataSet
            Dim errorMsg As String = ""

            sql = "SELECT * FROM " & table & " WHERE ir_id = " & irId
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count <= 0 Then
                sql = "INSERT INTO " & table & " (ir_id) VALUES( "
                sql &= "" & irId & ""
                sql &= ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & " : " & sql)
                End If
            End If

        End Sub

        Sub bindFall()
            Dim sql As String
            Dim ds As New DataSet

            Try
                isHasRow("ir_fall_tab")

                sql = "SELECT * FROM ir_fall_tab WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")
                Dim whotype As String = "1"
                Dim whoyear As String = ""
                If ds.Tables("t1").Rows(0)("whofall_type").ToString = "1" Then
                    CType(cl.FindControl("txtwho1"), RadioButton).Checked = True
                    ' CType(cl.FindControl("txtyear1"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("whofall_age").ToString
                ElseIf ds.Tables("t1").Rows(0)("whofall_type").ToString = "2" Then
                    CType(cl.FindControl("txtwho2"), RadioButton).Checked = True
                    ' CType(cl.FindControl("txtyear2"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("whofall_age").ToString
                ElseIf ds.Tables("t1").Rows(0)("whofall_type").ToString = "3" Then
                    CType(cl.FindControl("txtwho3"), RadioButton).Checked = True

                End If

                CType(cl.FindControl("txtyear3"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("whofall_age").ToString

                Try
                    CType(cl.FindControl("txtfalltype"), RadioButtonList).SelectedValue = ds.Tables("t1").Rows(0)("fall_type").ToString
                Catch ex As Exception

                End Try

                CType(cl.FindControl("txtfall_period"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("period_fall").ToString
                Try
                    CType(cl.FindControl("txtfall_hour1"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("period_hour").ToString
                    CType(cl.FindControl("txtfall_min1"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("period_min").ToString
                Catch ex As Exception

                End Try


                CType(cl.FindControl("txtactivity_fall"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("activity_at_fall").ToString
                CType(cl.FindControl("txtfall_remark"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("activity_remark").ToString
                CType(cl.FindControl("txtlocation_fall"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("location").ToString
                CType(cl.FindControl("txtlocation_remark"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("location_remark").ToString

                CType(cl.FindControl("txtdate_exam"), HtmlInputText).Value = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_exam_ts").ToString)
                Try
                    CType(cl.FindControl("txtfall_hour2"), DropDownList).SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_exam_ts").ToString, "hour")
                    CType(cl.FindControl("txtfall_min2"), DropDownList).SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_exam_ts").ToString, "min")
                Catch ex As Exception
                    'Response.Write("hour : " & ex.Message)
                End Try


                CType(cl.FindControl("txtexam_doctor"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("exam_doctor").ToString

                ' If ds.Tables("t1").Rows(0)("is_renovation").ToString = "1" Then
                'CType(cl.FindControl("txtreno_yes"), RadioButton).Checked = True
                ' ElseIf ds.Tables("t1").Rows(0)("is_renovation").ToString = "0" Then
                ' CType(cl.FindControl("txtreno_no"), RadioButton).Checked = True
                ' End If

                If ds.Tables("t1").Rows(0)("is_has_assist").ToString = "1" Then
                    CType(cl.FindControl("txtassist_yes"), RadioButton).Checked = True
                ElseIf ds.Tables("t1").Rows(0)("is_has_assist").ToString = "0" Then
                    CType(cl.FindControl("txtassist_no"), RadioButton).Checked = True
                End If

                If ds.Tables("t1").Rows(0)("vital_flag").ToString = "1" Then
                    CType(cl.FindControl("txtvital_normal"), RadioButton).Checked = True
                ElseIf ds.Tables("t1").Rows(0)("vital_flag").ToString = "0" Then
                    CType(cl.FindControl("txtvital_abnormal"), RadioButton).Checked = True
                End If

                CType(cl.FindControl("txtvital_remark"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("vital_remark").ToString

                If ds.Tables("t1").Rows(0)("is_examination").ToString = "1" Then
                    CType(cl.FindControl("txtexam_yes"), RadioButton).Checked = True
                ElseIf ds.Tables("t1").Rows(0)("is_examination").ToString = "0" Then
                    CType(cl.FindControl("txtexam_no"), RadioButton).Checked = True
                End If

                CType(cl.FindControl("txttreatment"), HtmlTextArea).Value = ds.Tables("t1").Rows(0)("treatment_detail").ToString
                CType(cl.FindControl("txtrefuse"), HtmlTextArea).Value = ds.Tables("t1").Rows(0)("refuse_detail").ToString


                If ds.Tables("t1").Rows(0)("chk_alzheimer").ToString = "1" Then
                    CType(cl.FindControl("chk_alzheimer"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_alzheimer"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_alzheimer").ToString

                If ds.Tables("t1").Rows(0)("chk_sedative").ToString = "1" Then
                    CType(cl.FindControl("chk_sedative"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_sedative"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_sedative").ToString

                If ds.Tables("t1").Rows(0)("chk_analgesic").ToString = "1" Then
                    CType(cl.FindControl("chk_analgesic"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_analgesic"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_analgesic").ToString

                If ds.Tables("t1").Rows(0)("chk_diuretic").ToString = "1" Then
                    CType(cl.FindControl("chk_diuretic"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_diuretic"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_diuretic").ToString

                If ds.Tables("t1").Rows(0)("chk_beta").ToString = "1" Then
                    CType(cl.FindControl("chk_beta"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_beta"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_beta").ToString

                If ds.Tables("t1").Rows(0)("chk_laxative").ToString = "1" Then
                    CType(cl.FindControl("chk_laxative"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_laxative"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_laxative").ToString

                If ds.Tables("t1").Rows(0)("chk_antiepil").ToString = "1" Then
                    CType(cl.FindControl("chk_antiepil"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_antiepil"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_antiepil").ToString

                If ds.Tables("t1").Rows(0)("chk_narcotic").ToString = "1" Then
                    CType(cl.FindControl("chk_narcotic"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_narcotic"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_narcotic").ToString

                If ds.Tables("t1").Rows(0)("chk_benzo").ToString = "1" Then
                    CType(cl.FindControl("chk_benzo"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_benzo"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_benzo").ToString

                If ds.Tables("t1").Rows(0)("chk_other1").ToString = "1" Then
                    CType(cl.FindControl("chk_other1"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txt_other1"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("txt_other1").ToString

                For i As Integer = 1 To 14
                    If ds.Tables("t1").Rows(0)("chk_pt" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_pt" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_pt" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i

                CType(cl.FindControl("txtptother"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("pt_other_remark").ToString
                CType(cl.FindControl("txtprocedure"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("post_procedure").ToString

                Try
                    CType(cl.FindControl("txttype_anes"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("type_anesthesia").ToString
                Catch ex As Exception

                End Try


                For i As Integer = 1 To 8
                    If ds.Tables("t1").Rows(0)("chk_rn_care" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_rn_care" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_rn_care" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txtrnremark"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("rn_care_remark").ToString

                For i As Integer = 1 To 11
                    If ds.Tables("t1").Rows(0)("chk_equip" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_equip" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_equip" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txtequip_remark"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("equip_remark").ToString

                For i As Integer = 1 To 9
                    If ds.Tables("t1").Rows(0)("chk_safe" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_safe" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_safe" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txtsafe_remark"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("safe_remark").ToString

                For i As Integer = 1 To 3
                    If ds.Tables("t1").Rows(0)("chk_inform" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_inform" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_inform" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                'Response.Write(ds.Tables("t1").Rows(0)("date_assess_ts").ToString)
                CType(cl.FindControl("txtdate_assess"), TextBox).Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_assess_ts").ToString)

                Try
                    CType(cl.FindControl("txtfall_hour3"), DropDownList).SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_assess_ts").ToString, "hour")
                    CType(cl.FindControl("txtfall_min3"), DropDownList).SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_assess_ts").ToString, "min")
                Catch ex As Exception
                    ' Response.Write("hour3 : " & ex.Message)
                End Try

                CType(cl.FindControl("txtreason"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("assess_reason").ToString

                For i As Integer = 1 To 5
                    If ds.Tables("t1").Rows(0)("chk_assess" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_assess" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_assess" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txtassess_other"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("assess_other_remark").ToString

                For i As Integer = 1 To 14
                    Try
                        CType(cl.FindControl("txtw" & i), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("score_w" & i).ToString
                    Catch ex As Exception

                    End Try

                Next i

                For i As Integer = 1 To 14
                    Try
                        CType(cl.FindControl("txts" & i), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("score_s" & i).ToString
                    Catch ex As Exception

                    End Try

                Next i

                Try
                    CType(cl.FindControl("txtward_sc"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("ward_score").ToString
                Catch ex As Exception

                End Try

                Try
                    CType(cl.FindControl("txtmanager_sc"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("manager_score").ToString
                Catch ex As Exception

                End Try



                Try
                    CType(cl.FindControl("txtlevelfall"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("level_fall").ToString
                Catch ex As Exception

                End Try

                Try
                    CType(cl.FindControl("txtsecure"), DropDownList).SelectedValue = ds.Tables("t1").Rows(0)("severity_outcome").ToString
                Catch ex As Exception

                End Try

                CType(cl.FindControl("txtfall_nation"), RadioButtonList).SelectedValue = ds.Tables("t1").Rows(0)("nation_type").ToString
                CType(cl.FindControl("txtnation_remark"), TextBox).Text = ds.Tables("t1").Rows(0)("nation_remark").ToString

                If ds.Tables("t1").Rows(0)("chk_factor_other").ToString = "1" Then
                    CType(cl.FindControl("chk_factor_other"), HtmlInputCheckBox).Checked = True
                End If
                CType(cl.FindControl("txtfactor_other"), HtmlInputText).Value = ds.Tables("t1").Rows(0)("factor_remark").ToString

            Catch ex As Exception
                Response.Write(ex.Message & " : " & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub updateFall()

            Dim sqlB As New StringBuilder
            Dim errorMsg As String
            'Dim t As RadioButton = CType(cl.FindControl("txtwho1"), RadioButton)
            'Response.Write(t.Checked)
            Dim whotype As String = "1"
            Dim whoyear As String = ""
            Dim timefall As String = ""
            Dim periodfall As String = ""
            If CType(cl.FindControl("txtwho1"), RadioButton).Checked = True Then
                whotype = "1"
                ' whoyear = CType(cl.FindControl("txtyear1"), HtmlInputText).Value
            ElseIf CType(cl.FindControl("txtwho2"), RadioButton).Checked = True Then
                whotype = "2"
                ' whoyear = CType(cl.FindControl("txtyear2"), HtmlInputText).Value
            ElseIf CType(cl.FindControl("txtwho3"), RadioButton).Checked = True Then
                whotype = "3"

            End If

            whoyear = CType(cl.FindControl("txtyear3"), HtmlInputText).Value

            'If CType(cl.FindControl("txttimefall1"), RadioButton).Checked = True Then
            '    periodfall = "1"
            'ElseIf CType(cl.FindControl("txttimefall2"), RadioButton).Checked = True Then
            '    periodfall = "2"
            'ElseIf CType(cl.FindControl("txttimefall3"), RadioButton).Checked = True Then
            '    periodfall = "3"
            'End If

            periodfall = CType(cl.FindControl("txtfall_period"), DropDownList).SelectedValue

            sqlB.Append("UPDATE ir_fall_tab SET whofall_type = " & whotype & "")
            sqlB.Append(", whofall_age = '" & whoyear & "'")
            sqlB.Append(", fall_type = '" & CType(cl.FindControl("txtfalltype"), RadioButtonList).SelectedValue & "'")
            sqlB.Append(", period_fall = '" & periodfall & "'")
            sqlB.Append(", period_hour = '" & CType(cl.FindControl("txtfall_hour1"), DropDownList).SelectedValue & "'")
            sqlB.Append(", period_min = '" & CType(cl.FindControl("txtfall_min1"), DropDownList).SelectedValue & "'")
            'sql &= ", time_fall = '" & 0 & "'"
            sqlB.Append(", activity_at_fall = '" & CType(cl.FindControl("txtactivity_fall"), DropDownList).SelectedValue & "'")
            sqlB.Append(", activity_remark = '" & addslashes(CType(cl.FindControl("txtfall_remark"), HtmlInputText).Value) & "'")
            sqlB.Append(", location = '" & CType(cl.FindControl("txtlocation_fall"), DropDownList).SelectedValue & "'")
            sqlB.Append(", location_remark = '" & addslashes(CType(cl.FindControl("txtlocation_remark"), HtmlInputText).Value) & "'")
            '   If CType(cl.FindControl("txtreno_yes"), RadioButton).Checked = True Then
            'sqlB.Append(", is_renovation = '" & 1 & "'")
            'Else
            'sqlB.Append(", is_renovation = '" & 0 & "'")
            'End If

            If CType(cl.FindControl("txtassist_yes"), RadioButton).Checked = True Then
                sqlB.Append(", is_has_assist = '" & 1 & "'")
            Else
                sqlB.Append(", is_has_assist = '" & 0 & "'")
            End If

            If CType(cl.FindControl("txtvital_normal"), RadioButton).Checked = True Then
                sqlB.Append(", vital_flag = '" & 1 & "'")
            Else
                sqlB.Append(", vital_flag = '" & 0 & "'")
            End If


            sqlB.Append(", vital_remark = '" & addslashes(CType(cl.FindControl("txtvital_remark"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("txtexam_yes"), RadioButton).Checked = True Then
                sqlB.Append(", is_examination = '" & 1 & "'")
            ElseIf CType(cl.FindControl("txtexam_no"), RadioButton).Checked = True Then
                sqlB.Append(", is_examination = '" & 0 & "'")
            End If


            sqlB.Append(", exam_doctor = '" & addslashes(CType(cl.FindControl("txtexam_doctor"), HtmlInputText).Value) & "'")

            sqlB.Append(", date_exam = '" & convertToSQLDatetime(CType(cl.FindControl("txtdate_exam"), HtmlInputText).Value) & "'")
            Try
                If CType(cl.FindControl("txtdate_exam"), HtmlInputText).Value <> "" Then
                    sqlB.Append(", date_exam_ts = '" & ConvertDateStringToTimeStamp(CType(cl.FindControl("txtdate_exam"), HtmlInputText).Value, CType(cl.FindControl("txtfall_hour2"), DropDownList).SelectedValue, CType(cl.FindControl("txtfall_min2"), DropDownList).SelectedValue) & "'")
                Else
                    sqlB.Append(", date_exam_ts = null")
                End If
            Catch ex As Exception
                sqlB.Append(" , date_exam_ts = null ")
            End Try

            sqlB.Append(", treatment_detail = '" & addslashes(CType(cl.FindControl("txttreatment"), HtmlTextArea).Value) & "'")
            sqlB.Append(", refuse_detail = '" & addslashes(CType(cl.FindControl("txtrefuse"), HtmlTextArea).Value) & "'")
            If CType(cl.FindControl("chk_alzheimer"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_alzheimer = '" & 1 & "'")
            Else
                sqlB.Append(", chk_alzheimer = '" & 0 & "'")
            End If
            sqlB.Append(", txt_alzheimer = '" & addslashes(CType(cl.FindControl("txt_alzheimer"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("chk_sedative"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_sedative = '" & 1 & "'")
            Else
                sqlB.Append(", chk_sedative = '" & 0 & "'")
            End If
            sqlB.Append(", txt_sedative = '" & addslashes(CType(cl.FindControl("txt_sedative"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("chk_analgesic"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_analgesic = '" & 1 & "'")
            Else
                sqlB.Append(", chk_analgesic = '" & 0 & "'")
            End If
            sqlB.Append(", txt_analgesic = '" & addslashes(CType(cl.FindControl("txt_analgesic"), HtmlInputText).Value) & "'")


            If CType(cl.FindControl("chk_diuretic"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_diuretic = '" & 1 & "'")
            Else
                sqlB.Append(", chk_diuretic = '" & 0 & "'")
            End If
            sqlB.Append(", txt_diuretic = '" & addslashes(CType(cl.FindControl("txt_diuretic"), HtmlInputText).Value) & "'")


            If CType(cl.FindControl("chk_beta"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_beta = '" & 1 & "'")
            Else
                sqlB.Append(", chk_beta = '" & 0 & "'")
            End If
            sqlB.Append(", txt_beta = '" & addslashes(CType(cl.FindControl("txt_beta"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("chk_laxative"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_laxative = '" & 1 & "'")
            Else
                sqlB.Append(", chk_laxative = '" & 0 & "'")
            End If
            sqlB.Append(", txt_laxative = '" & addslashes(CType(cl.FindControl("txt_laxative"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("chk_antiepil"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_antiepil = '" & 1 & "'")
            Else
                sqlB.Append(", chk_antiepil = '" & 0 & "'")
            End If
            sqlB.Append(", txt_antiepil = '" & addslashes(CType(cl.FindControl("txt_antiepil"), HtmlInputText).Value) & "'")


            If CType(cl.FindControl("chk_narcotic"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_narcotic = '" & 1 & "'")
            Else
                sqlB.Append(", chk_narcotic = '" & 0 & "'")
            End If
            sqlB.Append(", txt_narcotic = '" & addslashes(CType(cl.FindControl("txt_narcotic"), HtmlInputText).Value) & "'")


            If CType(cl.FindControl("chk_benzo"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_benzo = '" & 1 & "'")
            Else
                sqlB.Append(", chk_benzo = '" & 0 & "'")
            End If
            sqlB.Append(", txt_benzo = '" & addslashes(CType(cl.FindControl("txt_benzo"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("chk_other1"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_other1 = '" & 1 & "'")
            Else
                sqlB.Append(", chk_other1 = '" & 0 & "'")
            End If
            sqlB.Append(", txt_other1 = '" & addslashes(CType(cl.FindControl("txt_other1"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 8
                If CType(cl.FindControl("chk_rn_care" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_rn_care" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_rn_care" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", pt_other_remark = '" & addslashes(CType(cl.FindControl("txtptother"), HtmlInputText).Value) & "'")
            sqlB.Append(", post_procedure = '" & addslashes(CType(cl.FindControl("txtprocedure"), HtmlInputText).Value) & "'")
            sqlB.Append(", type_anesthesia = '" & CType(cl.FindControl("txttype_anes"), DropDownList).SelectedValue & "'")

            For i As Integer = 1 To 14
                If CType(cl.FindControl("chk_pt" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_pt" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_pt" & i & " = '" & 0 & "'")
                End If
            Next i

            sqlB.Append(", rn_care_remark = '" & addslashes(CType(cl.FindControl("txtrnremark"), HtmlInputText).Value) & "'")


            For i As Integer = 1 To 11
                If CType(cl.FindControl("chk_equip" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_equip" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_equip" & i & " = '" & 0 & "'")
                End If
            Next i

            sqlB.Append(", equip_remark = '" & addslashes(CType(cl.FindControl("txtequip_remark"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 9
                If CType(cl.FindControl("chk_safe" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_safe" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_safe" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", safe_remark = '" & addslashes(CType(cl.FindControl("txtsafe_remark"), HtmlInputText).Value) & "'")


            For i As Integer = 1 To 3
                If CType(cl.FindControl("chk_inform" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_inform" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_inform" & i & " = '" & 0 & "'")
                End If
            Next i

            If CType(cl.FindControl("txtdate_assess"), TextBox).Text = "" Then
                sqlB.Append(", date_assess = null ")
            Else
                sqlB.Append(", date_assess = '" & convertToSQLDatetime(CType(cl.FindControl("txtdate_assess"), TextBox).Text) & "'")
            End If

            'sqlB.Append(", date_assess_ts = '" & ConvertDateStringToTimeStamp(CType(cl.FindControl("txtdate_assess"), TextBox).Text) & "'")

            Try
                If CType(cl.FindControl("txtdate_assess"), TextBox).Text <> "" Then
                    sqlB.Append(", date_assess_ts = '" & ConvertDateStringToTimeStamp(CType(cl.FindControl("txtdate_assess"), TextBox).Text, CType(cl.FindControl("txtfall_hour3"), DropDownList).SelectedValue, CType(cl.FindControl("txtfall_min3"), DropDownList).SelectedValue) & "'")
                Else
                    sqlB.Append(", date_assess_ts = null ")
                End If
            Catch ex As Exception
                sqlB.Append(", date_assess_ts = null ")
                ' Response.Write(sqlB.ToString)
                'Response.Write(ex.Message)
                'Response.End()
                '  sqlB.Append(" , date_assess_ts = null ")
            End Try


            sqlB.Append(", assess_reason = '" & addslashes(CType(cl.FindControl("txtreason"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 5
                If CType(cl.FindControl("chk_assess" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_assess" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_assess" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", assess_other_remark = '" & addslashes(CType(cl.FindControl("txtassess_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 14
                sqlB.Append(", score_w" & i & " = '" & CType(cl.FindControl("txtw" & i), DropDownList).SelectedValue & "'")
            Next i

            For i As Integer = 1 To 14
                sqlB.Append(", score_s" & i & " = '" & CType(cl.FindControl("txts" & i), DropDownList).SelectedValue & "'")
            Next i

            sqlB.Append(", ward_score = '" & CType(cl.FindControl("txtward_sc"), DropDownList).SelectedValue & "'")
            Response.Write(22)
            sqlB.Append(", manager_score  = '" & CType(cl.FindControl("txtmanager_sc"), DropDownList).SelectedValue & "'")
            Response.Write(33)
            sqlB.Append(", level_fall = '" & CType(cl.FindControl("txtlevelfall"), DropDownList).SelectedValue & "'")
            Response.Write(55)
            sqlB.Append(", severity_outcome = '" & CType(cl.FindControl("txtsecure"), DropDownList).SelectedValue & "'")
            Response.Write(66)
            sqlB.Append(", nation_type = '" & CType(cl.FindControl("txtfall_nation"), RadioButtonList).SelectedValue & "'")
            sqlB.Append(", nation_remark = '" & addslashes(CType(cl.FindControl("txtnation_remark"), TextBox).Text) & "'")
            If CType(cl.FindControl("chk_factor_other"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_factor_other = '" & 1 & "'")
            Else
                sqlB.Append(", chk_factor_other = '" & 0 & "'")
            End If
            sqlB.Append(", factor_remark = '" & addslashes(CType(cl.FindControl("txtfactor_other"), HtmlInputText).Value) & "'")


            sqlB.Append(" WHERE ir_id = " & irId)

            errorMsg = conn.executeSQLForTransaction(sqlB.ToString)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sqlB.ToString)
            End If
        End Sub

        Sub bindDrugWrongName()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_med_tab_drug WHERE 1 = 1 "
                If mode = "add" Then
                    sql &= " AND session_id = '" & Session.SessionID & "'"
                Else
                    sql &= " AND ir_id = " & irId
                End If
                ds = conn.getDataSetForTransaction(sql, "t1")

              
                CType(cl.FindControl("GridDrugWrongName"), GridView).DataSource = ds
                CType(cl.FindControl("GridDrugWrongName"), GridView).DataBind()

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindMedPeriod()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_med_tab_period WHERE 1 = 1 "
                If mode = "add" Then
                    sql &= " AND session_id = '" & Session.SessionID & "'"
                Else
                    sql &= " AND ir_id = " & irId
                End If
                ds = conn.getDataSetForTransaction(sql, "t1")
                CType(cl.FindControl("GridMedPeriod"), GridView).DataSource = ds
                CType(cl.FindControl("GridMedPeriod"), GridView).DataBind()

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindMed()
            Dim sql As String
            Dim ds As New DataSet

            Try
                isHasRow("ir_med_tab")

                sql = "SELECT * FROM ir_med_tab WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")
                If ds.Tables(0).Rows(0)("chk_wrongtime").ToString = "1" Then
                    CType(cl.FindControl("chk_wrongtime"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrongtime"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrongroute").ToString = "1" Then
                    CType(cl.FindControl("chk_wrongroute"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrongroute"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrongdate").ToString = "1" Then
                    CType(cl.FindControl("chk_wrongdate"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrongdate"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrongrate").ToString = "1" Then
                    CType(cl.FindControl("chk_wrongrate"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrongrate"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_omission").ToString = "1" Then
                    CType(cl.FindControl("chk_omission"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_omission"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wronglabel").ToString = "1" Then
                    CType(cl.FindControl("chk_wronglabel"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wronglabel"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrongform").ToString = "1" Then
                    CType(cl.FindControl("chk_wrongform"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrongform"), HtmlInputCheckBox).Checked = False
                End If


                If ds.Tables(0).Rows(0)("chk_wrongbrand").ToString = "1" Then
                    CType(cl.FindControl("chk_wrongbrand"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrongbrand"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrongdose").ToString = "1" Then
                    CType(cl.FindControl("chk_wrongdose"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrongdose"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_extradose").ToString = "1" Then
                    CType(cl.FindControl("chk_extradose"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_extradose"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrongiv").ToString = "1" Then
                    CType(cl.FindControl("chk_wrongiv"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrongiv"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrong_deteriorate").ToString = "1" Then
                    CType(cl.FindControl("chk_wrong_deteriorate"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrong_deteriorate"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrong_prep").ToString = "1" Then
                    CType(cl.FindControl("chk_wrong_prep"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrong_prep"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrong_drugerror").ToString = "1" Then
                    CType(cl.FindControl("chk_wrong_drugerror"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrong_drugerror"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrong_duplicate").ToString = "1" Then
                    CType(cl.FindControl("chk_wrong_duplicate"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrong_duplicate"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrong_formular").ToString = "1" Then
                    CType(cl.FindControl("chk_wrong_formular"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrong_formular"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrong_qty").ToString = "1" Then
                    CType(cl.FindControl("chk_wrong_qty"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrong_qty"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrong_drug").ToString = "1" Then
                    CType(cl.FindControl("chk_wrong_drug"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrong_drug"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_wrong_pt").ToString = "1" Then
                    CType(cl.FindControl("chk_wrong_pt"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_wrong_pt"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_allergy").ToString = "1" Then
                    CType(cl.FindControl("chk_allergy"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_allergy"), HtmlInputCheckBox).Checked = False
                End If

                'For Each _listItem In CType(cl.FindControl("txtmed_level"), DropDownList).Items
                '    _listItem.attributes.add("title", _listItem.text)
                'Next

                Try
                    CType(cl.FindControl("txtmed_level"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("level_outcome").ToString
                Catch ex As Exception

                End Try

                Try
                    CType(cl.FindControl("txtmed_category"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("drug_category").ToString
                Catch ex As Exception

                End Try

                '  CType(cl.FindControl("txtdruggroup"), HtmlInputText).Value = ds.Tables(0).Rows(0)("drug_group").ToString
                '   CType(cl.FindControl("txtdrugname"), HtmlInputText).Value = ds.Tables(0).Rows(0)("drug_name").ToString
                CType(cl.FindControl("txtdrugname_right"), HtmlInputText).Value = ds.Tables(0).Rows(0)("drug_name_right").ToString

                If ds.Tables(0).Rows(0)("chk_err_order").ToString = "1" Then
                    CType(cl.FindControl("chk_err_order"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_err_order"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_err_transcription").ToString = "1" Then
                    CType(cl.FindControl("chk_err_transcription"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_err_transcription"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_err_key").ToString = "1" Then
                    CType(cl.FindControl("chk_err_key"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_err_key"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_err_verify").ToString = "1" Then
                    CType(cl.FindControl("chk_err_verify"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_err_verify"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_err_predis").ToString = "1" Then
                    CType(cl.FindControl("chk_err_predis"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_err_predis"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_err_dispensing").ToString = "1" Then
                    CType(cl.FindControl("chk_err_dispensing"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_err_dispensing"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_err_preadmin").ToString = "1" Then
                    CType(cl.FindControl("chk_err_preadmin"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_err_preadmin"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_err_admin").ToString = "1" Then
                    CType(cl.FindControl("chk_err_admin"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_err_admin"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_err_monitor").ToString = "1" Then
                    CType(cl.FindControl("chk_err_monitor"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_err_monitor"), HtmlInputCheckBox).Checked = False
                End If

                For i As Integer = 1 To 7
                    If ds.Tables("t1").Rows(0)("chk_order_type" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_order_type" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_order_type" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i

                Try
                    CType(cl.FindControl("txtorder_type"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("order_type").ToString
                Catch ex As Exception

                End Try



                If ds.Tables(0).Rows(0)("is_robot_product").ToString = "1" Then
                    CType(cl.FindControl("txtrobot1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_robot_product").ToString = "0" Then
                    CType(cl.FindControl("txtrobot2"), RadioButton).Checked = True
                End If

                If ds.Tables(0).Rows(0)("is_cpoe").ToString = "1" Then
                    CType(cl.FindControl("txtcpoe1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_cpoe").ToString = "0" Then
                    CType(cl.FindControl("txtcpoe2"), RadioButton).Checked = True
                End If

                If ds.Tables(0).Rows(0)("is_mar_error").ToString = "1" Then
                    CType(cl.FindControl("txtmar1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_mar_error").ToString = "0" Then
                    CType(cl.FindControl("txtmar2"), RadioButton).Checked = True
                End If


                Try
                    CType(cl.FindControl("txtmed_period"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("time_period").ToString
                    
                Catch ex As Exception

                End Try

                Try
                    CType(cl.FindControl("txtmed_hour1"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("period_hour").ToString

                Catch ex As Exception

                End Try

                Try
                    CType(cl.FindControl("txtmed_min1"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("period_min").ToString
                Catch ex As Exception

                End Try
              

                'If ds.Tables(0).Rows(0)("chk_rn").ToString = "1" Then
                '    CType(cl.FindControl("chk_rn"), HtmlInputCheckBox).Checked = True
                'Else
                '    CType(cl.FindControl("chk_rn"), HtmlInputCheckBox).Checked = False
                'End If

                'If ds.Tables(0).Rows(0)("chk_ph").ToString = "1" Then
                '    CType(cl.FindControl("chk_ph"), HtmlInputCheckBox).Checked = True
                'Else
                '    CType(cl.FindControl("chk_ph"), HtmlInputCheckBox).Checked = False
                'End If

                'If ds.Tables(0).Rows(0)("chk_aph").ToString = "1" Then
                '    CType(cl.FindControl("chk_aph"), HtmlInputCheckBox).Checked = True
                'Else
                '    CType(cl.FindControl("chk_aph"), HtmlInputCheckBox).Checked = False
                'End If

                'CType(cl.FindControl("txtrn_exp"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("rn_exp").ToString
                'CType(cl.FindControl("txtph_exp"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("ph_exp").ToString
                'CType(cl.FindControl("txtaph_exp"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("aph_exp").ToString

                For i As Integer = 1 To 11
                    If ds.Tables("t1").Rows(0)("chk_h" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_h" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_h" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txth11_remark"), HtmlInputText).Value = ds.Tables(0).Rows(0)("h11_remark").ToString

                For i As Integer = 1 To 8
                    If ds.Tables("t1").Rows(0)("chk_c" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_c" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_c" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txtc8_remark"), HtmlInputText).Value = ds.Tables(0).Rows(0)("c8_remark").ToString

                For i As Integer = 1 To 21
                    If ds.Tables("t1").Rows(0)("chk_p" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_p" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_p" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txtp21_remark"), HtmlInputText).Value = ds.Tables(0).Rows(0)("p21_remark").ToString

                For i As Integer = 1 To 9
                    If ds.Tables("t1").Rows(0)("chk_s" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_s" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_s" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txts9_remark"), HtmlInputText).Value = ds.Tables(0).Rows(0)("s9_remark").ToString

                For i As Integer = 1 To 6
                    If ds.Tables("t1").Rows(0)("chk_d" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_d" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_d" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txtd6_remark"), HtmlInputText).Value = ds.Tables(0).Rows(0)("d6_remark").ToString

                For i As Integer = 1 To 2
                    If ds.Tables("t1").Rows(0)("chk_m" & i).ToString = "1" Then
                        CType(cl.FindControl("chk_m" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_m" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("txtm1_remark"), HtmlInputText).Value = ds.Tables(0).Rows(0)("m1_remark").ToString
                CType(cl.FindControl("txtm2_remark"), HtmlInputText).Value = ds.Tables(0).Rows(0)("m2_remark").ToString

                If ds.Tables("t1").Rows(0)("is_floor_stock").ToString = "1" Then
                    CType(cl.FindControl("txtfloor1"), RadioButton).Checked = True
                ElseIf ds.Tables("t1").Rows(0)("is_floor_stock").ToString = "0" Then
                    CType(cl.FindControl("txtfloor2"), RadioButton).Checked = True
                End If
             

            Catch ex As Exception
                Response.Write(ex.Message & " : " & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub updateDeleteScan()
            Dim sql As String
            Dim errorMsg As String

            If mode = "add" Then
                sql = "UPDATE ir_delete_scan_detail SET ir_id = " & new_ir_id & " , session_id = null WHERE session_id = '" & Session.SessionID & "'"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If

        End Sub

        Sub updateMedPeriod()
            Dim sql As String
            Dim errorMsg As String

            If mode = "add" Then
                sql = "UPDATE ir_med_tab_period SET ir_id = " & new_ir_id & " , session_id = null WHERE session_id = '" & Session.SessionID & "'"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If

        End Sub

        Sub updateMedDrugWrongName()
            Dim sql As String
            Dim errorMsg As String

            If mode = "add" Then
                sql = "UPDATE ir_med_tab_drug SET ir_id = " & new_ir_id & " , session_id = null WHERE session_id = '" & Session.SessionID & "'"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If

        End Sub

        Sub updateMed()

            Dim sqlB As New StringBuilder
            Dim errorMsg As String
            Dim is_robot_product As String = "null"
            Dim is_cpoe As String = ""
            Dim is_mar_error As String = ""

        

            If CType(cl.FindControl("txtrobot1"), RadioButton).Checked = True Then
                is_robot_product = "1"
            ElseIf CType(cl.FindControl("txtrobot2"), RadioButton).Checked = True Then
                is_robot_product = "0"
            End If

            'If CType(cl.FindControl("txtcpoe1"), RadioButton).Checked = True Then
            '    is_cpoe = "1"
            'ElseIf CType(cl.FindControl("txtcpoe2"), RadioButton).Checked = True Then
            '    is_cpoe = "0"
            'End If

            'If CType(cl.FindControl("txtmar1"), RadioButton).Checked = True Then
            '    is_mar_error = "1"
            'ElseIf CType(cl.FindControl("txtmar2"), RadioButton).Checked = True Then
            '    is_mar_error = "0"
            'End If

            sqlB.Append("UPDATE ir_med_tab SET is_robot_product = " & is_robot_product & "")

            If CType(cl.FindControl("txtcpoe1"), RadioButton).Checked = True Then
                sqlB.Append(", is_cpoe = 1 ")
            ElseIf CType(cl.FindControl("txtcpoe2"), RadioButton).Checked = True Then
                sqlB.Append(", is_cpoe = 0 ")
            End If

            ' sqlB.Append(", is_cpoe = '" & is_cpoe & "'")
            'sqlB.Append(", is_mar_error = '" & is_mar_error & "'")
            If CType(cl.FindControl("txtmar1"), RadioButton).Checked = True Then
                sqlB.Append(", is_mar_error =1 ")
            ElseIf CType(cl.FindControl("txtmar2"), RadioButton).Checked = True Then
                sqlB.Append(", is_mar_error =0 ")
            End If

            If CType(cl.FindControl("chk_wrongtime"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrongtime = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrongtime = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrongroute"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrongroute = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrongroute = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrongdate"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrongdate = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrongdate = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrongrate"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrongrate = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrongrate = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_omission"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_omission = '" & 1 & "'")
            Else
                sqlB.Append(", chk_omission = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wronglabel"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wronglabel = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wronglabel = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrongform"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrongform = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrongform = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrongbrand"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrongbrand = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrongbrand = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrongdose"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrongdose = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrongdose = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_extradose"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_extradose = '" & 1 & "'")
            Else
                sqlB.Append(", chk_extradose = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrongiv"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrongiv = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrongiv = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrong_deteriorate"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrong_deteriorate = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrong_deteriorate = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrong_prep"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrong_prep = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrong_prep = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrong_drugerror"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrong_drugerror = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrong_drugerror = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrong_duplicate"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrong_duplicate = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrong_duplicate = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrong_formular"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrong_formular = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrong_formular = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrong_qty"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrong_qty = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrong_qty = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrong_drug"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrong_drug = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrong_drug = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_wrong_pt"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_wrong_pt = '" & 1 & "'")
            Else
                sqlB.Append(", chk_wrong_pt = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_allergy"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_allergy = '" & 1 & "'")
            Else
                sqlB.Append(", chk_allergy = '" & 0 & "'")
            End If

            sqlB.Append(", level_outcome = '" & CType(cl.FindControl("txtmed_level"), DropDownList).SelectedValue & "'")
            sqlB.Append(", drug_category = '" & addslashes(CType(cl.FindControl("txtmed_category"), DropDownList).SelectedValue) & "'")
            ' sqlB.Append(", drug_group = '" & addslashes(CType(cl.FindControl("txtdruggroup"), HtmlInputText).Value) & "'")
            '  sqlB.Append(", drug_name = '" & addslashes(CType(cl.FindControl("txtdrugname"), HtmlInputText).Value) & "'")
            sqlB.Append(", drug_name_right = '" & addslashes(CType(cl.FindControl("txtdrugname_right"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("chk_err_order"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_err_order = '" & 1 & "'")
            Else
                sqlB.Append(", chk_err_order = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_err_transcription"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_err_transcription = '" & 1 & "'")
            Else
                sqlB.Append(", chk_err_transcription = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_err_key"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_err_key = '" & 1 & "'")
            Else
                sqlB.Append(", chk_err_key = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_err_verify"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_err_verify = '" & 1 & "'")
            Else
                sqlB.Append(", chk_err_verify = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_err_predis"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_err_predis = '" & 1 & "'")
            Else
                sqlB.Append(", chk_err_predis = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_err_dispensing"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_err_dispensing = '" & 1 & "'")
            Else
                sqlB.Append(", chk_err_dispensing = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_err_preadmin"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_err_preadmin = '" & 1 & "'")
            Else
                sqlB.Append(", chk_err_preadmin = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_err_admin"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_err_admin = '" & 1 & "'")
            Else
                sqlB.Append(", chk_err_admin = '" & 0 & "'")
            End If
            If CType(cl.FindControl("chk_err_monitor"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_err_monitor = '" & 1 & "'")
            Else
                sqlB.Append(", chk_err_monitor = '" & 0 & "'")
            End If

            For i As Integer = 1 To 7
                If CType(cl.FindControl("chk_order_type" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_order_type" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_order_type" & i & " = '" & 0 & "'")
                End If
            Next i

         

            sqlB.Append(", order_type = '" & CType(cl.FindControl("txtorder_type"), DropDownList).SelectedValue & "'")

            sqlB.Append(", time_period = '" & CType(cl.FindControl("txtmed_period"), DropDownList).SelectedValue & "'")
            sqlB.Append(", period_hour = '" & CType(cl.FindControl("txtmed_hour1"), DropDownList).SelectedValue & "'")
            sqlB.Append(", period_min = '" & CType(cl.FindControl("txtmed_min1"), DropDownList).SelectedValue & "'")

            'sqlB.Append(", rn_exp = '" & CType(cl.FindControl("txtrn_exp"), DropDownList).SelectedValue & "'")
            'sqlB.Append(", ph_exp = '" & CType(cl.FindControl("txtph_exp"), DropDownList).SelectedValue & "'")
            'sqlB.Append(", aph_exp = '" & CType(cl.FindControl("txtaph_exp"), DropDownList).SelectedValue & "'")
            'If CType(cl.FindControl("chk_rn"), HtmlInputCheckBox).Checked = True Then
            '    sqlB.Append(", chk_rn = '" & 1 & "'")
            'Else
            '    sqlB.Append(", chk_rn = '" & 0 & "'")
            'End If
            'If CType(cl.FindControl("chk_ph"), HtmlInputCheckBox).Checked = True Then
            '    sqlB.Append(", chk_ph = '" & 1 & "'")
            'Else
            '    sqlB.Append(", chk_ph = '" & 0 & "'")
            'End If
            'If CType(cl.FindControl("chk_aph"), HtmlInputCheckBox).Checked = True Then
            '    sqlB.Append(", chk_aph = '" & 1 & "'")
            'Else
            '    sqlB.Append(", chk_aph = '" & 0 & "'")
            'End If

            For i As Integer = 1 To 11
                If CType(cl.FindControl("chk_h" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_h" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_h" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", h11_remark = '" & addslashes(CType(cl.FindControl("txth11_remark"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 8
                If CType(cl.FindControl("chk_c" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_c" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_c" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", c8_remark = '" & addslashes(CType(cl.FindControl("txtc8_remark"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 21
                If CType(cl.FindControl("chk_p" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_p" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_p" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", p21_remark = '" & addslashes(CType(cl.FindControl("txtp21_remark"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 9
                If CType(cl.FindControl("chk_s" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_s" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_s" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", s9_remark = '" & addslashes(CType(cl.FindControl("txts9_remark"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 6
                If CType(cl.FindControl("chk_d" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_d" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_d" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", d6_remark = '" & addslashes(CType(cl.FindControl("txtd6_remark"), HtmlInputText).Value) & "'")


            For i As Integer = 1 To 2
                If CType(cl.FindControl("chk_m" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_m" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_m" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", m1_remark = '" & addslashes(CType(cl.FindControl("txtm1_remark"), HtmlInputText).Value) & "'")
            sqlB.Append(", m2_remark = '" & addslashes(CType(cl.FindControl("txtm2_remark"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("txtfloor1"), RadioButton).Checked = True Then
                sqlB.Append(", is_floor_stock = 1")
            ElseIf CType(cl.FindControl("txtfloor2"), RadioButton).Checked = True Then
                sqlB.Append(", is_floor_stock = 0")
            End If

            sqlB.Append(" WHERE ir_id = " & irId)
            'Response.Write(sqlB.ToString)
            errorMsg = conn.executeSQLForTransaction(sqlB.ToString)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sqlB.ToString)
            End If
        End Sub

        Sub bindPhelbitis()
            Dim sql As String
            Dim ds As New DataSet

            Try
                isHasRow("ir_phlebitis_tab")

                CType(cl.FindControl("txtph_s5"), RadioButton).Visible = True
                CType(cl.FindControl("lblStage5"), Label).Visible = True

                sql = "SELECT * FROM ir_phlebitis_tab WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")
                If ds.Tables(0).Rows(0)("infiltration_type").ToString = "1" Then
                    CType(cl.FindControl("txtinf_s1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("infiltration_type").ToString = "2" Then
                    CType(cl.FindControl("txtinf_s2"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("infiltration_type").ToString = "3" Then
                    CType(cl.FindControl("txtinf_s3"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("infiltration_type").ToString = "4" Then
                    CType(cl.FindControl("txtinf_s4"), RadioButton).Checked = True
                End If

                If ds.Tables(0).Rows(0)("phlebitis_type").ToString = "1" Then
                    CType(cl.FindControl("txtph_s1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("phlebitis_type").ToString = "2" Then
                    CType(cl.FindControl("txtph_s2"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("phlebitis_type").ToString = "3" Then
                    CType(cl.FindControl("txtph_s3"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("phlebitis_type").ToString = "4" Then
                    CType(cl.FindControl("txtph_s4"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("phlebitis_type").ToString = "5" Then
                    CType(cl.FindControl("txtph_s5"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("phlebitis_type").ToString = "-1" Then
                    CType(cl.FindControl("txtph_s0"), RadioButton).Checked = True
                End If

                If ds.Tables(0).Rows(0)("chk_mechanical").ToString = "1" Then
                    CType(cl.FindControl("chk_mechanical"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_mechanical"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_chemical").ToString = "1" Then
                    CType(cl.FindControl("chk_chemical"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_chemical"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_bacterial").ToString = "1" Then
                    CType(cl.FindControl("chk_bacterial"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_bacterial"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_jelco").ToString = "1" Then
                    CType(cl.FindControl("chk_jelco"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_jelco"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_venflon").ToString = "1" Then
                    CType(cl.FindControl("chk_venflon"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_venflon"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_other_gauge").ToString = "1" Then
                    CType(cl.FindControl("chk_other_gauge"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_other_gauge"), HtmlInputCheckBox).Checked = False
                End If

                CType(cl.FindControl("txtjelco"), HtmlInputText).Value = ds.Tables(0).Rows(0)("jelco_remark").ToString
                CType(cl.FindControl("txtvenflon"), HtmlInputText).Value = ds.Tables(0).Rows(0)("venflon_remark").ToString
                CType(cl.FindControl("txtothergauge"), HtmlInputText).Value = ds.Tables(0).Rows(0)("other_gauge_remark").ToString

                If ds.Tables(0).Rows(0)("chk_iv_dorsal").ToString = "1" Then
                    CType(cl.FindControl("chk_iv_dorsal"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_iv_dorsal"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_iv_cep").ToString = "1" Then
                    CType(cl.FindControl("chk_iv_cep"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_iv_cep"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_iv_basi").ToString = "1" Then
                    CType(cl.FindControl("chk_iv_basi"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_iv_basi"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_iv_other").ToString = "1" Then
                    CType(cl.FindControl("chk_iv_other"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_iv_other"), HtmlInputCheckBox).Checked = False
                End If
                CType(cl.FindControl("txtivother"), TextBox).Text = ds.Tables(0).Rows(0)("iv_other_remark").ToString

                If ds.Tables(0).Rows(0)("chk_conc").ToString = "1" Then
                    CType(cl.FindControl("chk_conc"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_conc"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_chemo").ToString = "1" Then
                    CType(cl.FindControl("chk_chemo"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_chemo"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_med").ToString = "1" Then
                    CType(cl.FindControl("chk_med"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_med"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_anti").ToString = "1" Then
                    CType(cl.FindControl("chk_anti"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_anti"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_ivmed_other").ToString = "1" Then
                    CType(cl.FindControl("chk_ivmed_other"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_ivmed_other"), HtmlInputCheckBox).Checked = False
                End If

                CType(cl.FindControl("txtfluid"), HtmlInputText).Value = ds.Tables(0).Rows(0)("fluid_comment").ToString
                CType(cl.FindControl("txt_ivmed_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("ivmed_other_remark").ToString

                If ds.Tables(0).Rows(0)("duration").ToString = "1" Then
                    CType(cl.FindControl("txtduration1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("duration").ToString = "2" Then
                    CType(cl.FindControl("txtduration2"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("duration").ToString = "3" Then
                    CType(cl.FindControl("txtduration3"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("duration").ToString = "4" Then
                    CType(cl.FindControl("txtduration4"), RadioButton).Checked = True
                End If

                CType(cl.FindControl("txtpain"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("pain").ToString
                CType(cl.FindControl("txtredness"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("redness").ToString
                CType(cl.FindControl("txterythema"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("erythema").ToString
                CType(cl.FindControl("txtswelling"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("swelling").ToString
                CType(cl.FindControl("txtinduration"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("induration").ToString
                CType(cl.FindControl("txtpvc"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("pvc").ToString
                CType(cl.FindControl("txtfever"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("fever").ToString
                CType(cl.FindControl("txtinf_pain"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("inf_pain").ToString
                CType(cl.FindControl("txtinf_edema"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("inf_edema").ToString
                CType(cl.FindControl("txtmed_history"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("med_history").ToString

                If ds.Tables(0).Rows(0)("chk_immune").ToString = "1" Then
                    CType(cl.FindControl("chk_immune"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_immune"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_dm").ToString = "1" Then
                    CType(cl.FindControl("chk_dm"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_dm"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_obesity").ToString = "1" Then
                    CType(cl.FindControl("chk_obesity"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_obesity"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_coagulo").ToString = "1" Then
                    CType(cl.FindControl("chk_coagulo"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_coagulo"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_cva").ToString = "1" Then
                    CType(cl.FindControl("chk_cva"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_cva"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_cancer").ToString = "1" Then
                    CType(cl.FindControl("chk_cancer"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_cancer"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_mal").ToString = "1" Then
                    CType(cl.FindControl("chk_mal"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_mal"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_bony").ToString = "1" Then
                    CType(cl.FindControl("chk_bony"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_bony"), HtmlInputCheckBox).Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_history_other").ToString = "1" Then
                    CType(cl.FindControl("chk_history_other"), HtmlInputCheckBox).Checked = True
                Else
                    CType(cl.FindControl("chk_history_other"), HtmlInputCheckBox).Checked = False
                End If

                CType(cl.FindControl("txthistoryother"), HtmlInputText).Value = ds.Tables(0).Rows(0)("history_other_remark").ToString
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub updatePhelbitis()
            Dim sqlB As New StringBuilder
            Dim errorMsg As String

            sqlB.Append("UPDATE ir_phlebitis_tab SET last_update = GETDATE() ")

            If CType(cl.FindControl("txtinf_s1"), RadioButton).Checked = True Then
                sqlB.Append(", infiltration_type = 1")
            ElseIf CType(cl.FindControl("txtinf_s2"), RadioButton).Checked = True Then
                sqlB.Append(", infiltration_type = 2")
            ElseIf CType(cl.FindControl("txtinf_s3"), RadioButton).Checked = True Then
                sqlB.Append(", infiltration_type = 3")
            ElseIf CType(cl.FindControl("txtinf_s4"), RadioButton).Checked = True Then
                sqlB.Append(", infiltration_type = 4")
            ElseIf CType(cl.FindControl("txtinf_s5"), RadioButton).Checked = True Then
                sqlB.Append(", infiltration_type = 0")
            End If

            If CType(cl.FindControl("txtph_s1"), RadioButton).Checked = True Then
                sqlB.Append(", phlebitis_type = 1")
            ElseIf CType(cl.FindControl("txtph_s2"), RadioButton).Checked = True Then
                sqlB.Append(", phlebitis_type = 2")
            ElseIf CType(cl.FindControl("txtph_s3"), RadioButton).Checked = True Then
                sqlB.Append(", phlebitis_type = 3")
            ElseIf CType(cl.FindControl("txtph_s4"), RadioButton).Checked = True Then
                sqlB.Append(", phlebitis_type = 4")
            ElseIf CType(cl.FindControl("txtph_s5"), RadioButton).Checked = True Then
                sqlB.Append(", phlebitis_type = 5")
            ElseIf CType(cl.FindControl("txtph_s6"), RadioButton).Checked = True Then ' none select
                sqlB.Append(", phlebitis_type = 0")
            ElseIf CType(cl.FindControl("txtph_s0"), RadioButton).Checked = True Then ' stage 0
                sqlB.Append(", phlebitis_type = -1")
            End If

            If CType(cl.FindControl("chk_mechanical"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_mechanical = '" & 1 & "'")
            Else
                sqlB.Append(", chk_mechanical = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_chemical"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_chemical = '" & 1 & "'")
            Else
                sqlB.Append(", chk_chemical = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_bacterial"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_bacterial = '" & 1 & "'")
            Else
                sqlB.Append(", chk_bacterial = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_jelco"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_jelco = '" & 1 & "'")
            Else
                sqlB.Append(", chk_jelco = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_venflon"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_venflon = '" & 1 & "'")
            Else
                sqlB.Append(", chk_venflon = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_other_gauge"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_other_gauge = '" & 1 & "'")
            Else
                sqlB.Append(", chk_other_gauge = '" & 0 & "'")
            End If

            sqlB.Append(", jelco_remark = '" & addslashes(CType(cl.FindControl("txtjelco"), HtmlInputText).Value) & "'")
            sqlB.Append(", venflon_remark = '" & addslashes(CType(cl.FindControl("txtvenflon"), HtmlInputText).Value) & "'")
            sqlB.Append(", other_gauge_remark = '" & addslashes(CType(cl.FindControl("txtothergauge"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("chk_iv_dorsal"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_iv_dorsal = '" & 1 & "'")
            Else
                sqlB.Append(", chk_iv_dorsal = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_iv_cep"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_iv_cep = '" & 1 & "'")
            Else
                sqlB.Append(", chk_iv_cep = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_iv_basi"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_iv_basi = '" & 1 & "'")
            Else
                sqlB.Append(", chk_iv_basi = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_iv_other"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_iv_other = '" & 1 & "'")
            Else
                sqlB.Append(", chk_iv_other = '" & 0 & "'")
            End If
            sqlB.Append(", iv_other_remark = '" & addslashes(CType(cl.FindControl("txtivother"), TextBox).Text) & "'")

            If CType(cl.FindControl("chk_conc"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_conc = '" & 1 & "'")
            Else
                sqlB.Append(", chk_conc = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_chemo"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_chemo = '" & 1 & "'")
            Else
                sqlB.Append(", chk_chemo = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_med"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_med = '" & 1 & "'")
            Else
                sqlB.Append(", chk_med = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_anti"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_anti = '" & 1 & "'")
            Else
                sqlB.Append(", chk_anti = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_ivmed_other"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_ivmed_other = '" & 1 & "'")
            Else
                sqlB.Append(", chk_ivmed_other = '" & 0 & "'")
            End If

            sqlB.Append(", fluid_comment = '" & addslashes(CType(cl.FindControl("txtfluid"), HtmlInputText).Value) & "'")
            sqlB.Append(", ivmed_other_remark = '" & addslashes(CType(cl.FindControl("txt_ivmed_other"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("txtduration1"), RadioButton).Checked = True Then
                sqlB.Append(", duration = 1")
            ElseIf CType(cl.FindControl("txtduration2"), RadioButton).Checked = True Then
                sqlB.Append(", duration = 2")
            ElseIf CType(cl.FindControl("txtduration3"), RadioButton).Checked = True Then
                sqlB.Append(", duration = 3")
            ElseIf CType(cl.FindControl("txtduration4"), RadioButton).Checked = True Then
                sqlB.Append(", duration = 4")
            End If

            sqlB.Append(", pain = '" & CType(cl.FindControl("txtpain"), DropDownList).SelectedValue & "'")
            sqlB.Append(", redness = '" & CType(cl.FindControl("txtredness"), DropDownList).SelectedValue & "'")
            sqlB.Append(", erythema = '" & CType(cl.FindControl("txterythema"), DropDownList).SelectedValue & "'")
            sqlB.Append(", swelling = '" & CType(cl.FindControl("txtswelling"), DropDownList).SelectedValue & "'")
            sqlB.Append(", induration = '" & CType(cl.FindControl("txtinduration"), DropDownList).SelectedValue & "'")
            sqlB.Append(", pvc = '" & CType(cl.FindControl("txtpvc"), DropDownList).SelectedValue & "'")
            sqlB.Append(", fever = '" & CType(cl.FindControl("txtfever"), DropDownList).SelectedValue & "'")
            sqlB.Append(", inf_pain = '" & CType(cl.FindControl("txtinf_pain"), DropDownList).SelectedValue & "'")
            sqlB.Append(", inf_edema = '" & CType(cl.FindControl("txtinf_edema"), DropDownList).SelectedValue & "'")
            sqlB.Append(", med_history = '" & CType(cl.FindControl("txtmed_history"), DropDownList).SelectedValue & "'")

            If CType(cl.FindControl("chk_immune"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_immune = '" & 1 & "'")
            Else
                sqlB.Append(", chk_immune = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_dm"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_dm = '" & 1 & "'")
            Else
                sqlB.Append(", chk_dm = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_obesity"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_obesity = '" & 1 & "'")
            Else
                sqlB.Append(", chk_obesity = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_coagulo"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_coagulo = '" & 1 & "'")
            Else
                sqlB.Append(", chk_coagulo = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_cva"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_cva = '" & 1 & "'")
            Else
                sqlB.Append(", chk_cva = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_cancer"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_cancer = '" & 1 & "'")
            Else
                sqlB.Append(", chk_cancer = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_mal"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_mal = '" & 1 & "'")
            Else
                sqlB.Append(", chk_mal = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_bony"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_bony = '" & 1 & "'")
            Else
                sqlB.Append(", chk_bony = '" & 0 & "'")
            End If

            If CType(cl.FindControl("chk_history_other"), HtmlInputCheckBox).Checked = True Then
                sqlB.Append(", chk_history_other = '" & 1 & "'")
            Else
                sqlB.Append(", chk_history_other = '" & 0 & "'")
            End If

            sqlB.Append(", history_other_remark = '" & addslashes(CType(cl.FindControl("txthistoryother"), HtmlInputText).Value) & "'")
            sqlB.Append(" WHERE ir_id = " & irId)
            errorMsg = conn.executeSQLForTransaction(sqlB.ToString)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sqlB.ToString)
            End If
        End Sub

        Sub bindAnes()
            Dim sql As String
            Dim ds As New DataSet

            Try
                isHasRow("ir_anes_tab")

                sql = "SELECT * FROM ir_anes_tab WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")

                For i As Integer = 1 To 6
                    If ds.Tables(0).Rows(0)("chk_topic1" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic1" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic1" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i

                For i As Integer = 1 To 3
                    If ds.Tables(0).Rows(0)("chk_topic2" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic2" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic2" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i

                CType(cl.FindControl("topic2_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic2_other").ToString()

                For i As Integer = 1 To 3
                    If ds.Tables(0).Rows(0)("chk_topic3" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic3" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic3" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("topic3_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic3_other").ToString()

                For i As Integer = 1 To 7
                    If ds.Tables(0).Rows(0)("chk_topic4" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic4" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic4" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("topic4_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic4_other").ToString()

                For i As Integer = 1 To 6
                    If ds.Tables(0).Rows(0)("chk_topic5" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic5" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic5" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("topic5_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic5_other").ToString()

                For i As Integer = 1 To 7
                    If ds.Tables(0).Rows(0)("chk_topic6" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic6" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic6" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("topic6_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic6_other").ToString()

                For i As Integer = 1 To 8
                    If ds.Tables(0).Rows(0)("chk_topic7" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic7" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic7" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("topic7_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic7_other").ToString()

                For i As Integer = 1 To 8
                    If ds.Tables(0).Rows(0)("chk_topic8" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic8" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic8" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("topic8_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic8_other").ToString()

                For i As Integer = 1 To 3
                    If ds.Tables(0).Rows(0)("chk_topic9" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic9" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic9" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("topic9_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic9_other").ToString()


                For i As Integer = 1 To 3
                    If ds.Tables(0).Rows(0)("chk_topic10" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic10" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic10" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("topic10_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic10_other").ToString()

                For i As Integer = 1 To 7
                    If ds.Tables(0).Rows(0)("chk_topic11" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic11" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic11" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i

                For i As Integer = 1 To 5
                    If ds.Tables(0).Rows(0)("chk_topic12" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic12" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic12" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i

                For i As Integer = 1 To 14
                    If ds.Tables(0).Rows(0)("chk_topic13" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic13" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic13" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i
                CType(cl.FindControl("topic13_other1"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic13_other1").ToString()
                CType(cl.FindControl("topic13_other2"), HtmlInputText).Value = ds.Tables(0).Rows(0)("topic13_other2").ToString()

                For i As Integer = 1 To 4
                    If ds.Tables(0).Rows(0)("chk_topic14" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic14" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic14" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i

                For i As Integer = 1 To 3
                    If ds.Tables(0).Rows(0)("chk_topic15" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic15" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic15" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i

                For i As Integer = 1 To 3
                    If ds.Tables(0).Rows(0)("chk_topic16" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_topic16" & i), HtmlInputCheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_topic16" & i), HtmlInputCheckBox).Checked = False
                    End If
                Next i

                For i As Integer = 1 To 6
                    CType(cl.FindControl("comment" & i), HtmlTextArea).Value = ds.Tables(0).Rows(0)("comment" & i).ToString()
                Next i

            Catch ex As Exception
            Finally
            End Try
        End Sub

        Sub updateAnes()
            Dim sqlB As New StringBuilder
            Dim errorMsg As String

            sqlB.Append("UPDATE ir_anes_tab SET last_update = GETDATE() ")
            For i As Integer = 1 To 6
                If CType(cl.FindControl("chk_topic1" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic1" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic1" & i & " = '" & 0 & "'")
                End If
            Next i

            For i As Integer = 1 To 3
                If CType(cl.FindControl("chk_topic2" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic2" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic2" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic2_other = '" & addslashes(CType(cl.FindControl("topic2_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 3
                If CType(cl.FindControl("chk_topic3" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic3" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic3" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic3_other = '" & addslashes(CType(cl.FindControl("topic3_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 7
                If CType(cl.FindControl("chk_topic4" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic4" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic4" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic4_other = '" & addslashes(CType(cl.FindControl("topic4_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 6
                If CType(cl.FindControl("chk_topic5" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic5" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic5" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic5_other = '" & addslashes(CType(cl.FindControl("topic5_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 7
                If CType(cl.FindControl("chk_topic6" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic6" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic6" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic6_other = '" & addslashes(CType(cl.FindControl("topic6_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 8
                If CType(cl.FindControl("chk_topic7" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic7" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic7" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic7_other = '" & addslashes(CType(cl.FindControl("topic7_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 8
                If CType(cl.FindControl("chk_topic8" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic8" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic8" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic8_other = '" & addslashes(CType(cl.FindControl("topic8_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 3
                If CType(cl.FindControl("chk_topic9" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic9" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic9" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic9_other = '" & addslashes(CType(cl.FindControl("topic9_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 3
                If CType(cl.FindControl("chk_topic10" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic10" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic10" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic10_other = '" & addslashes(CType(cl.FindControl("topic10_other"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 7
                If CType(cl.FindControl("chk_topic11" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic11" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic11" & i & " = '" & 0 & "'")
                End If
            Next i

            For i As Integer = 1 To 5
                If CType(cl.FindControl("chk_topic12" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic12" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic12" & i & " = '" & 0 & "'")
                End If
            Next i

            For i As Integer = 1 To 14
                If CType(cl.FindControl("chk_topic13" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic13" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic13" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", topic13_other1 = '" & addslashes(CType(cl.FindControl("topic13_other1"), HtmlInputText).Value) & "'")
            sqlB.Append(", topic13_other2 = '" & addslashes(CType(cl.FindControl("topic13_other2"), HtmlInputText).Value) & "'")

            For i As Integer = 1 To 4
                If CType(cl.FindControl("chk_topic14" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic14" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic14" & i & " = '" & 0 & "'")
                End If
            Next i

            For i As Integer = 1 To 3
                If CType(cl.FindControl("chk_topic15" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic15" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic15" & i & " = '" & 0 & "'")
                End If
            Next i

            For i As Integer = 1 To 3
                If CType(cl.FindControl("chk_topic16" & i), HtmlInputCheckBox).Checked = True Then
                    sqlB.Append(", chk_topic16" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_topic16" & i & " = '" & 0 & "'")
                End If
            Next i

            For i As Integer = 1 To 6
                sqlB.Append(", comment" & i & " = '" & addslashes(CType(cl.FindControl("comment" & i), HtmlTextArea).Value) & "'")
            Next i

            sqlB.Append(" WHERE ir_id = " & irId)

            errorMsg = conn.executeSQLForTransaction(sqlB.ToString)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sqlB.ToString)
            End If
        End Sub

        Sub bindPressure()
            Dim sql As String
            Dim ds As New DataSet

            Try
                isHasRow("ir_pressure_tab")

                sql = "SELECT * FROM ir_pressure_tab WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")

                If ds.Tables(0).Rows(0)("acquire_type").ToString() = "1" Then
                    CType(cl.FindControl("is_acquire1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("acquire_type").ToString() = "2" Then
                    CType(cl.FindControl("is_acquire2"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("acquire_type").ToString() = "3" Then
                    CType(cl.FindControl("is_acquire3"), RadioButton).Checked = True
                End If

                If ds.Tables(0).Rows(0)("stage_type").ToString() = "1" Then
                    CType(cl.FindControl("is_stage1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("stage_type").ToString() = "2" Then
                    CType(cl.FindControl("is_stage2"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("stage_type").ToString() = "3" Then
                    CType(cl.FindControl("is_stage3"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("stage_type").ToString() = "4" Then
                    CType(cl.FindControl("is_stage4"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("stage_type").ToString() = "5" Then
                    CType(cl.FindControl("is_stage5"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("stage_type").ToString() = "6" Then
                    CType(cl.FindControl("is_stage6"), RadioButton).Checked = True
                End If

                If ds.Tables(0).Rows(0)("location_type").ToString() = "1" Then
                    CType(cl.FindControl("is_location1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("location_type").ToString() = "2" Then
                    CType(cl.FindControl("is_location2"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("location_type").ToString() = "3" Then
                    CType(cl.FindControl("is_location3"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("location_type").ToString() = "4" Then
                    CType(cl.FindControl("is_location4"), RadioButton).Checked = True
                End If

                CType(cl.FindControl("txtbuttock_type"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("buttock_type").ToString()
                CType(cl.FindControl("txthip_type"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("hip_type").ToString()
                CType(cl.FindControl("txtother_remark"), HtmlInputText).Value = ds.Tables(0).Rows(0)("other_remark").ToString()

                If ds.Tables(0).Rows(0)("admission_type").ToString() = "1" Then
                    CType(cl.FindControl("is_admission1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("admission_type").ToString() = "2" Then
                    CType(cl.FindControl("is_admission2"), RadioButton).Checked = True
                End If

                CType(cl.FindControl("txtadmission"), TextBox).Text = ds.Tables(0).Rows(0)("admission_detail").ToString()


                If ds.Tables(0).Rows(0)("scale_type").ToString() = "1" Then
                    CType(cl.FindControl("is_scale1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("scale_type").ToString() = "2" Then
                    CType(cl.FindControl("is_scale2"), RadioButton).Checked = True
                End If

                For i As Integer = 1 To 11
                    If ds.Tables(0).Rows(0)("chk_scale" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_scale" & i), CheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_scale" & i), CheckBox).Checked = False
                    End If
                Next i

                CType(cl.FindControl("txtscale_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("scale_other_remark").ToString()
                CType(cl.FindControl("txtscale_detail"), HtmlTextArea).Value = ds.Tables(0).Rows(0)("scale_no_detail").ToString()

                If ds.Tables(0).Rows(0)("risk_type").ToString() = "1" Then
                    CType(cl.FindControl("is_risk1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("risk_type").ToString() = "2" Then
                    CType(cl.FindControl("is_risk2"), RadioButton).Checked = True
                End If

                For i As Integer = 1 To 12
                    If ds.Tables(0).Rows(0)("chk_risk" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_risk" & i), CheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_risk" & i), CheckBox).Checked = False
                    End If
                Next i


                If ds.Tables(0).Rows(0)("device_type").ToString() = "1" Then
                    CType(cl.FindControl("is_device1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("device_type").ToString() = "2" Then
                    CType(cl.FindControl("is_device1"), RadioButton).Checked = True
                End If

                For i As Integer = 1 To 5
                    If ds.Tables(0).Rows(0)("chk_device" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_device" & i), CheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_device" & i), CheckBox).Checked = False
                    End If
                Next i

                For i As Integer = 1 To 6
                    If ds.Tables(0).Rows(0)("chk_device5" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_device5" & i), CheckBox).Checked = True
                    Else
                        CType(cl.FindControl("chk_device5" & i), CheckBox).Checked = False
                    End If
                Next i

                CType(cl.FindControl("txtdevice_other_remark"), HtmlInputText).Value = ds.Tables(0).Rows(0)("device_other_remark").ToString()
                CType(cl.FindControl("txtsensory"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("sensory_type").ToString()
                CType(cl.FindControl("txtmoisture"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("moisture_type").ToString()
                CType(cl.FindControl("txtactivity"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("activity_type").ToString()
                CType(cl.FindControl("txtmobility"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("mobility_type").ToString()
                CType(cl.FindControl("txtnutrition"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("nutrition_type").ToString()
                CType(cl.FindControl("txtfriction"), DropDownList).SelectedValue = ds.Tables(0).Rows(0)("friction_type").ToString()
                CType(cl.FindControl("txtscore"), HtmlInputText).Value = ds.Tables(0).Rows(0)("score").ToString()

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub updatePressure()
            Dim sqlB As New StringBuilder
            Dim errorMsg As String

            sqlB.Append("UPDATE ir_pressure_tab SET last_update = GETDATE() ")
            If CType(cl.FindControl("is_acquire1"), RadioButton).Checked = True Then
                sqlB.Append(", acquire_type = 1 ")
            ElseIf CType(cl.FindControl("is_acquire2"), RadioButton).Checked = True Then
                sqlB.Append(", acquire_type = 2 ")
            ElseIf CType(cl.FindControl("is_acquire3"), RadioButton).Checked = True Then
                sqlB.Append(", acquire_type = 3 ")
            End If

            If CType(cl.FindControl("is_stage1"), RadioButton).Checked = True Then
                sqlB.Append(", stage_type = 1 ")
            ElseIf CType(cl.FindControl("is_stage2"), RadioButton).Checked = True Then
                sqlB.Append(", stage_type = 2 ")
            ElseIf CType(cl.FindControl("is_stage3"), RadioButton).Checked = True Then
                sqlB.Append(", stage_type = 3 ")
            ElseIf CType(cl.FindControl("is_stage4"), RadioButton).Checked = True Then
                sqlB.Append(", stage_type = 4 ")
            ElseIf CType(cl.FindControl("is_stage5"), RadioButton).Checked = True Then
                sqlB.Append(", stage_type = 5 ")
            ElseIf CType(cl.FindControl("is_stage6"), RadioButton).Checked = True Then
                sqlB.Append(", stage_type = 6 ")
            End If

            If CType(cl.FindControl("is_location1"), RadioButton).Checked = True Then
                sqlB.Append(", location_type = 1 ")
            ElseIf CType(cl.FindControl("is_location2"), RadioButton).Checked = True Then
                sqlB.Append(", location_type = 2 ")
            ElseIf CType(cl.FindControl("is_location3"), RadioButton).Checked = True Then
                sqlB.Append(", location_type = 3 ")
            ElseIf CType(cl.FindControl("is_location4"), RadioButton).Checked = True Then
                sqlB.Append(", location_type = 4 ")
            End If

            sqlB.Append(", buttock_type = '" & CType(cl.FindControl("txtbuttock_type"), DropDownList).SelectedValue & "'")
            sqlB.Append(", hip_type = '" & CType(cl.FindControl("txthip_type"), DropDownList).SelectedValue & "'")
            sqlB.Append(", other_remark = '" & addslashes(CType(cl.FindControl("txtother_remark"), HtmlInputText).Value) & "'")

            If CType(cl.FindControl("is_admission1"), RadioButton).Checked = True Then
                sqlB.Append(", admission_type = 1 ")
            ElseIf CType(cl.FindControl("is_admission1"), RadioButton).Checked = True Then
                sqlB.Append(", admission_type = 2 ")
            End If
            sqlB.Append(", admission_detail = '" & addslashes(CType(cl.FindControl("txtadmission"), TextBox).Text) & "'")

            If CType(cl.FindControl("is_scale1"), RadioButton).Checked = True Then
                sqlB.Append(", scale_type = 1 ")
            ElseIf CType(cl.FindControl("is_scale2"), RadioButton).Checked = True Then
                sqlB.Append(", scale_type = 2 ")
            End If

            For i As Integer = 1 To 11
                If CType(cl.FindControl("chk_scale" & i), CheckBox).Checked = True Then
                    sqlB.Append(", chk_scale" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_scale" & i & " = '" & 0 & "'")
                End If
            Next i
            sqlB.Append(", scale_other_remark = '" & addslashes(CType(cl.FindControl("txtscale_other"), HtmlInputText).Value) & "'")
            sqlB.Append(", scale_no_detail = '" & addslashes(CType(cl.FindControl("txtscale_detail"), HtmlTextArea).Value) & "'")

            If CType(cl.FindControl("is_risk1"), RadioButton).Checked = True Then
                sqlB.Append(", risk_type = 1 ")
            ElseIf CType(cl.FindControl("is_risk2"), RadioButton).Checked = True Then
                sqlB.Append(", risk_type = 2 ")
            End If

            For i As Integer = 1 To 12
                If CType(cl.FindControl("chk_risk" & i), CheckBox).Checked = True Then
                    sqlB.Append(", chk_risk" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_risk" & i & " = '" & 0 & "'")
                End If
            Next i

            If CType(cl.FindControl("is_device1"), RadioButton).Checked = True Then
                sqlB.Append(", device_type = 1 ")
            ElseIf CType(cl.FindControl("is_device2"), RadioButton).Checked = True Then
                sqlB.Append(", device_type = 2 ")
            End If

            For i As Integer = 1 To 5
                If CType(cl.FindControl("chk_device" & i), CheckBox).Checked = True Then
                    sqlB.Append(", chk_device" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_device" & i & " = '" & 0 & "'")
                End If
            Next i

            For i As Integer = 1 To 6
                If CType(cl.FindControl("chk_device5" & i), CheckBox).Checked = True Then
                    sqlB.Append(", chk_device5" & i & " = '" & 1 & "'")
                Else
                    sqlB.Append(", chk_device5" & i & " = '" & 0 & "'")
                End If
            Next i

            sqlB.Append(", device_other_remark = '" & addslashes(CType(cl.FindControl("txtdevice_other_remark"), HtmlInputText).Value) & "'")

            sqlB.Append(", sensory_type = '" & CType(cl.FindControl("txtsensory"), DropDownList).SelectedValue & "'")
            sqlB.Append(", moisture_type = '" & CType(cl.FindControl("txtmoisture"), DropDownList).SelectedValue & "'")
            sqlB.Append(", activity_type = '" & CType(cl.FindControl("txtactivity"), DropDownList).SelectedValue & "'")
            sqlB.Append(", mobility_type = '" & CType(cl.FindControl("txtmobility"), DropDownList).SelectedValue & "'")
            sqlB.Append(", nutrition_type = '" & CType(cl.FindControl("txtnutrition"), DropDownList).SelectedValue & "'")
            sqlB.Append(", friction_type = '" & CType(cl.FindControl("txtfriction"), DropDownList).SelectedValue & "'")

            sqlB.Append(", score = '" & CType(cl.FindControl("txtscore"), HtmlInputText).Value & "'")

            sqlB.Append(" WHERE ir_id = " & irId)
            errorMsg = conn.executeSQLForTransaction(sqlB.ToString)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sqlB.ToString)
            End If
        End Sub

        Sub bindDeleteTab()
            Dim sql As String
            Dim ds As New DataSet


            Try
                isHasRow("ir_delete_tab")

                sql = "SELECT * FROM ir_delete_tab WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")

                If ds.Tables(0).Rows(0)("is_doc_delete_id").ToString() = "1" Then
                    CType(cl.FindControl("txtscandel1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_doc_delete_id").ToString() = "0" Then
                    CType(cl.FindControl("txtscandel2"), RadioButton).Checked = True
                End If

                If ds.Tables(0).Rows(0)("unit_err_type_id").ToString() = "1" Then
                    CType(cl.FindControl("txtscanunit1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("unit_err_type_id").ToString() = "2" Then
                    CType(cl.FindControl("txtscanunit2"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("unit_err_type_id").ToString() = "3" Then
                    CType(cl.FindControl("txtscanunit3"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("unit_err_type_id").ToString() = "4" Then
                    CType(cl.FindControl("txtscanunit4"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("unit_err_type_id").ToString() = "5" Then
                    CType(cl.FindControl("txtscanunit5"), RadioButton).Checked = True
                End If

                For i As Integer = 1 To 9
                    If ds.Tables(0).Rows(0)("chk_typeofdoc" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_typeofdoc" & i), HtmlInputCheckBox).Checked = True
                    End If
                Next i

                For i As Integer = 1 To 4
                    If ds.Tables(0).Rows(0)("chk_typeofptrec" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_typeofptrec" & i), HtmlInputCheckBox).Checked = True
                    End If
                Next i

                If ds.Tables(0).Rows(0)("is_edoc_id").ToString() = "1" Then
                    CType(cl.FindControl("txtscanedoc1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_edoc_id").ToString() = "0" Then
                    CType(cl.FindControl("txtscanedoc2"), RadioButton).Checked = True
                End If
                '  Response.Write(22222)
                If ds.Tables(0).Rows(0)("is_doctype_id").ToString() = "1" Then
                    CType(cl.FindControl("txtbarcode1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_doctype_id").ToString() = "0" Then
                    CType(cl.FindControl("txtbarcode2"), RadioButton).Checked = True
                End If
                '  Response.Write(111111111111111)
                If ds.Tables(0).Rows(0)("found_error_by_id").ToString() = "1" Then
                    CType(cl.FindControl("txtscanerrorby1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("found_error_by_id").ToString() = "2" Then
                    CType(cl.FindControl("txtscanerrorby2"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("found_error_by_id").ToString() = "3" Then
                    CType(cl.FindControl("txtscanerrorby3"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("found_error_by_id").ToString() = "4" Then
                    CType(cl.FindControl("txtscanerrorby4"), RadioButton).Checked = True
                End If

                CType(cl.FindControl("txtscanerrorby_other"), HtmlInputText).Value = ds.Tables(0).Rows(0)("found_error_remark").ToString()

                For i As Integer = 1 To 12
                    If ds.Tables(0).Rows(0)("chk_scancause_h" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_scancause_h" & i), HtmlInputCheckBox).Checked = True
                    End If
                Next i

                CType(cl.FindControl("txtscancause_hother"), HtmlInputText).Value = ds.Tables(0).Rows(0)("chk_scancause_h12_remark").ToString()

                For i As Integer = 1 To 10
                    If ds.Tables(0).Rows(0)("chk_scancause_p" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_scancause_p" & i), HtmlInputCheckBox).Checked = True
                    End If
                Next i

                CType(cl.FindControl("txtscancause_pother"), HtmlInputText).Value = ds.Tables(0).Rows(0)("chk_scancause_p10_remark").ToString()

                For i As Integer = 1 To 5
                    If ds.Tables(0).Rows(0)("chk_scancause_c" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_scancause_c" & i), HtmlInputCheckBox).Checked = True
                    End If
                Next i
                CType(cl.FindControl("txtscancause_cother"), HtmlInputText).Value = ds.Tables(0).Rows(0)("chk_scancause_c5_remark").ToString()

                For i As Integer = 1 To 6
                    If ds.Tables(0).Rows(0)("chk_scancause_s" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_scancause_s" & i), HtmlInputCheckBox).Checked = True
                    End If
                Next i
                CType(cl.FindControl("txtscancause_sother"), HtmlInputText).Value = ds.Tables(0).Rows(0)("chk_scancause_s6_remark").ToString()

                For i As Integer = 1 To 2
                    If ds.Tables(0).Rows(0)("chk_scancause_m" & i).ToString() = "1" Then
                        CType(cl.FindControl("chk_scancause_m" & i), HtmlInputCheckBox).Checked = True
                    End If
                Next i
                CType(cl.FindControl("txtscancausem1"), HtmlInputText).Value = ds.Tables(0).Rows(0)("m1_remark").ToString()
                CType(cl.FindControl("txtscancausem2"), HtmlInputText).Value = ds.Tables(0).Rows(0)("m2_remark").ToString()

                If ds.Tables(0).Rows(0)("is_correct_id").ToString() = "1" Then
                    CType(cl.FindControl("txtscancorrect1"), RadioButton).Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_correct_id").ToString() = "0" Then
                    CType(cl.FindControl("txtscancorrect2"), RadioButton).Checked = True
                End If

                If ds.Tables(0).Rows(0)("is_opd_doc").ToString() = "1" Then
                    CType(cl.FindControl("chk_opddoc"), HtmlInputCheckBox).Checked = True
                End If

                If ds.Tables(0).Rows(0)("is_ipd_doc").ToString() = "1" Then
                    CType(cl.FindControl("chk_ipddoc"), HtmlInputCheckBox).Checked = True
                End If

                If ds.Tables(0).Rows(0)("is_cardiac_doc").ToString() = "1" Then
                    CType(cl.FindControl("chk_cardiacdoc"), HtmlInputCheckBox).Checked = True
                End If

                If ds.Tables(0).Rows(0)("is_film_doc").ToString() = "1" Then
                    CType(cl.FindControl("chk_filmdoc"), HtmlInputCheckBox).Checked = True
                End If

                If ds.Tables(0).Rows(0)("is_lab_doc").ToString() = "1" Then
                    CType(cl.FindControl("chk_labdoc"), HtmlInputCheckBox).Checked = True
                End If

                If ds.Tables(0).Rows(0)("is_other_doc").ToString() = "1" Then
                    CType(cl.FindControl("chk_otherdoc"), HtmlInputCheckBox).Checked = True
                End If

                For i As Integer = 1 To 3
                    CType(cl.FindControl("chk_opddoc_n" & i), HtmlInputText).Value = ds.Tables(0).Rows(0)("opd_doc" & i).ToString()
                Next i

                For i As Integer = 1 To 3
                    CType(cl.FindControl("chk_ipddoc_n" & i), HtmlInputText).Value = ds.Tables(0).Rows(0)("ipd_doc" & i).ToString()
                Next i

                For i As Integer = 1 To 3
                    CType(cl.FindControl("chk_cardiacdoc_n" & i), HtmlInputText).Value = ds.Tables(0).Rows(0)("cardiac_doc" & i).ToString()
                Next i

                For i As Integer = 1 To 3
                    CType(cl.FindControl("chk_filmdoc_n" & i), HtmlInputText).Value = ds.Tables(0).Rows(0)("film_doc" & i).ToString()
                Next i

                For i As Integer = 1 To 3
                    CType(cl.FindControl("chk_labdoc_n" & i), HtmlInputText).Value = ds.Tables(0).Rows(0)("lab_doc" & i).ToString()
                Next i

                For i As Integer = 1 To 3
                    CType(cl.FindControl("chk_otherdoc_n" & i), HtmlInputText).Value = ds.Tables(0).Rows(0)("other_doc" & i).ToString()
                Next i

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            End Try
        End Sub

        Sub updateDeleteTab()
            Dim sql As String
            Dim errorMsg As String

            sql = "UPDATE ir_delete_tab SET last_update = GETDATE()"
            If CType(cl.FindControl("txtscandel1"), RadioButton).Checked = True Then
                sql &= " , is_doc_delete_id = 1 "
                sql &= " , is_doc_delete_name = 'Yes' "
            ElseIf CType(cl.FindControl("txtscandel2"), RadioButton).Checked = True Then
                sql &= " , is_doc_delete_id = 0 "
                sql &= " , is_doc_delete_name = 'No' "
            End If
            ' Response.Write(CType(cl.FindControl("txtscandel1"), RadioButton).Checked & "<hr/>")
            If CType(cl.FindControl("txtscanunit1"), RadioButton).Checked = True Then
                sql &= " , unit_err_type_id = 1 "
                sql &= " , unit_err_type_name = 'NRS OPD' "
            ElseIf CType(cl.FindControl("txtscanunit2"), RadioButton).Checked = True Then
                sql &= " , unit_err_type_id = 2 "
                sql &= " , unit_err_type_name = 'NRS IPD' "
            ElseIf CType(cl.FindControl("txtscanunit3"), RadioButton).Checked = True Then
                sql &= " , unit_err_type_id = 3 "
                sql &= " , unit_err_type_name = 'Check-up' "
            ElseIf CType(cl.FindControl("txtscanunit4"), RadioButton).Checked = True Then
                sql &= " , unit_err_type_id = 4 "
                sql &= " , unit_err_type_name = 'MDCL' "
            ElseIf CType(cl.FindControl("txtscanunit5"), RadioButton).Checked = True Then
                sql &= " , unit_err_type_id = 5 "
                sql &= " , unit_err_type_name = 'Others' "
            End If

            For i As Integer = 1 To 9
                If CType(cl.FindControl("chk_typeofdoc" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= ", chk_typeofdoc" & i & " = '" & 1 & "'"
                Else
                    sql &= ", chk_typeofdoc" & i & " = '" & 0 & "'"
                End If
            Next i

            For i As Integer = 1 To 4
                If CType(cl.FindControl("chk_typeofptrec" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= ", chk_typeofptrec" & i & " = '" & 1 & "'"
                Else
                    sql &= ", chk_typeofptrec" & i & " = '" & 0 & "'"
                End If
            Next i

            If CType(cl.FindControl("txtscanedoc1"), RadioButton).Checked = True Then
                sql &= " , is_edoc_id = 1 "
                sql &= " , is_edoc_name = 'Yes' "
            ElseIf CType(cl.FindControl("txtscanedoc2"), RadioButton).Checked = True Then
                sql &= " , is_edoc_id = 0 "
                sql &= " , is_edoc_name = 'No' "
            End If

            If CType(cl.FindControl("txtbarcode1"), RadioButton).Checked = True Then
                sql &= " , is_doctype_id = 1 "
                sql &= " , is_doctype_name = 'Yes' "
            ElseIf CType(cl.FindControl("txtbarcode2"), RadioButton).Checked = True Then
                sql &= " , is_doctype_id = 0 "
                sql &= " , is_doctype_name = 'No' "
            End If
            '   Response.Write(3)

            If CType(cl.FindControl("txtscanerrorby1"), RadioButton).Checked = True Then
                sql &= " , found_error_by_id = 1 "
                sql &= " , found_error_by_name = 'Patient' "
            ElseIf CType(cl.FindControl("txtscanerrorby2"), RadioButton).Checked = True Then
                sql &= " , found_error_by_id = 2 "
                sql &= " , found_error_by_name = 'Staff' "
            ElseIf CType(cl.FindControl("txtscanerrorby3"), RadioButton).Checked = True Then
                sql &= " , found_error_by_id = 3 "
                sql &= " , found_error_by_name = 'Physician' "
            ElseIf CType(cl.FindControl("txtscanerrorby4"), RadioButton).Checked = True Then
                sql &= " , found_error_by_id = 4 "
                sql &= " , found_error_by_name = 'Others' "
          
            End If

            sql &= " , found_error_remark = '" & addslashes(CType(cl.FindControl("txtscanerrorby_other"), HtmlInputText).Value) & "'"

            For i As Integer = 1 To 12
                If CType(cl.FindControl("chk_scancause_h" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= ", chk_scancause_h" & i & " = '" & 1 & "'"
                Else
                    sql &= ", chk_scancause_h" & i & " = '" & 0 & "'"
                End If
            Next i
            sql &= " , chk_scancause_h12_remark = '" & addslashes(CType(cl.FindControl("txtscancause_hother"), HtmlInputText).Value) & "'"

            For i As Integer = 1 To 10
                If CType(cl.FindControl("chk_scancause_p" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= ", chk_scancause_p" & i & " = '" & 1 & "'"
                Else
                    sql &= ", chk_scancause_p" & i & " = '" & 0 & "'"
                End If
            Next i
            sql &= " , chk_scancause_p10_remark = '" & addslashes(CType(cl.FindControl("txtscancause_pother"), HtmlInputText).Value) & "'"

            For i As Integer = 1 To 5
                If CType(cl.FindControl("chk_scancause_c" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= ", chk_scancause_c" & i & " = '" & 1 & "'"
                Else
                    sql &= ", chk_scancause_c" & i & " = '" & 0 & "'"
                End If
            Next i
            sql &= " , chk_scancause_c5_remark = '" & addslashes(CType(cl.FindControl("txtscancause_cother"), HtmlInputText).Value) & "'"

            For i As Integer = 1 To 6
                If CType(cl.FindControl("chk_scancause_s" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= ", chk_scancause_s" & i & " = '" & 1 & "'"
                Else
                    sql &= ", chk_scancause_s" & i & " = '" & 0 & "'"
                End If
            Next i
            sql &= " , chk_scancause_s6_remark = '" & addslashes(CType(cl.FindControl("txtscancause_sother"), HtmlInputText).Value) & "'"

            For i As Integer = 1 To 2
                If CType(cl.FindControl("chk_scancause_m" & i), HtmlInputCheckBox).Checked = True Then
                    sql &= ", chk_scancause_m" & i & " = '" & 1 & "'"
                Else
                    sql &= ", chk_scancause_m" & i & " = '" & 0 & "'"
                End If
            Next i
            sql &= " , m1_remark = '" & addslashes(CType(cl.FindControl("txtscancausem1"), HtmlInputText).Value) & "'"
            sql &= " , m2_remark = '" & addslashes(CType(cl.FindControl("txtscancausem2"), HtmlInputText).Value) & "'"

            If CType(cl.FindControl("txtscancorrect1"), RadioButton).Checked = True Then
                sql &= " , is_correct_id = 1 "
                sql &= " , is_correct_name = 'Yes' "
            ElseIf CType(cl.FindControl("txtscancorrect2"), RadioButton).Checked = True Then
                sql &= " , is_correct_id = 0 "
                sql &= " , is_correct_name = 'No' "
            End If

            If CType(cl.FindControl("chk_opddoc"), HtmlInputCheckBox).Checked = True Then
                sql &= " , is_opd_doc = 1 "
            Else
                sql &= " , is_opd_doc = 0 "
            End If

            If CType(cl.FindControl("chk_ipddoc"), HtmlInputCheckBox).Checked = True Then
                sql &= " , is_ipd_doc = 1 "
            Else
                sql &= " , is_ipd_doc = 0 "
            End If

            If CType(cl.FindControl("chk_cardiacdoc"), HtmlInputCheckBox).Checked = True Then
                sql &= " , is_cardiac_doc = 1 "
            Else
                sql &= " , is_cardiac_doc = 0 "
            End If

            If CType(cl.FindControl("chk_filmdoc"), HtmlInputCheckBox).Checked = True Then
                sql &= " , is_film_doc = 1 "
            Else
                sql &= " , is_film_doc = 0 "
            End If

            If CType(cl.FindControl("chk_labdoc"), HtmlInputCheckBox).Checked = True Then
                sql &= " , is_lab_doc = 1 "
            Else
                sql &= " , is_lab_doc = 0 "
            End If

            If CType(cl.FindControl("chk_otherdoc"), HtmlInputCheckBox).Checked = True Then
                sql &= " , is_other_doc = 1 "
            Else
                sql &= " , is_other_doc = 0 "
            End If

            For i As Integer = 1 To 3
                sql &= ", opd_doc" & i & " = '" & addslashes(CType(cl.FindControl("chk_opddoc_n" & i), HtmlInputText).Value) & "'"
            Next i

            For i As Integer = 1 To 3
                sql &= ", ipd_doc" & i & " = '" & addslashes(CType(cl.FindControl("chk_ipddoc_n" & i), HtmlInputText).Value) & "'"
            Next i

            For i As Integer = 1 To 3
                sql &= ", cardiac_doc" & i & " = '" & addslashes(CType(cl.FindControl("chk_cardiacdoc_n" & i), HtmlInputText).Value) & "'"
            Next i

            For i As Integer = 1 To 3
                sql &= ", film_doc" & i & " = '" & addslashes(CType(cl.FindControl("chk_filmdoc_n" & i), HtmlInputText).Value) & "'"
            Next i

            For i As Integer = 1 To 3
                sql &= ", lab_doc" & i & " = '" & addslashes(CType(cl.FindControl("chk_labdoc_n" & i), HtmlInputText).Value) & "'"
            Next i

            For i As Integer = 1 To 3
                sql &= ", other_doc" & i & " = '" & addslashes(CType(cl.FindControl("chk_otherdoc_n" & i), HtmlInputText).Value) & "'"
            Next i

            sql &= "  WHERE ir_id = " & irId
            '  Response.Write(sql)
            '  Response.End()
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sql)
            End If
        End Sub

        Sub updateOther()

        End Sub

        Sub updateTQMTab() ' สำหรับหน้า TQM Tab
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet
            ' Response.Write(10)
            sql = "UPDATE ir_tqm_tab SET grand_topic = '" & txtgrandtopic.SelectedValue & "' "
            ' sql &= " , grand_topic = '" & txtgrandtopic.SelectedValue & "' "
            sql &= " , topic = '" & txtnormaltopic.SelectedValue & "' "
            sql &= " , subtopic1 = '" & txtsubtopic1.SelectedValue & "' "
            sql &= " , subtopic2 = '" & txtsubtopic2.SelectedValue & "' "
            sql &= " , subtopic3 = '" & txtsubtopic3.SelectedValue & "' "
            sql &= " , tqm_topic_detail = '" & addslashes(txttqm_detail.Text) & "' "

            sql &= " , tqm_case_owner = '" & addslashes(txtcase_owner.Text) & "' "
            ' sql &= " , physician_id = '" & txttqm_doctor.SelectedValue & "' "
            ' sql &= " , physician_name = '" & txttqm_doctor.SelectedItem.Text & "' "
            '  sql &= " , incident_detail = '" & txtcause_detail.Value & "' "
            sql &= " , tqm_remark = '" & addslashes(txttqm_remark.Value) & "' "
            sql &= " , action_detail = '" & addslashes(txtaction_tqm_detail.Value) & "' "
            '  Response.Write(11)
            If txtsevere_level1.Checked = True Then
                sql &= ", severe_level_id = '" & 1 & "'"
            ElseIf txtsevere_level2.Checked = True Then
                sql &= ", severe_level_id = '" & 2 & "'"
            ElseIf txtsevere_level3.Checked = True Then
                sql &= ", severe_level_id = '" & 3 & "'"
            ElseIf txtsevere_level4.Checked = True Then
                sql &= ", severe_level_id = '" & 4 & "'"
            ElseIf txtsevere_level5.Checked = True Then
                sql &= ", severe_level_id = '" & 5 & "'"
            ElseIf txtsevere_level6.Checked = True Then
                sql &= ", severe_level_id = '" & 6 & "'"
            ElseIf txtsevere_level7.Checked = True Then
                sql &= ", severe_level_id = '" & 7 & "'"
            ElseIf txtsevere_level8.Checked = True Then
                sql &= ", severe_level_id = null "
            End If

            '   Response.Write(12)

            If txttqmconcern1.Checked = True Then
                sql &= ", is_concern = '" & 1 & "'"
            Else
                sql &= ", is_concern = '" & 0 & "'"
            End If

            If txttqmrefer1.Checked = True Then
                sql &= ", is_refer = '" & 1 & "'"
            Else
                sql &= ", is_refer = '" & 0 & "'"
            End If
            If txtir_cfb.Text = "" Then
                sql &= " , relate_cfb_no = null "
            Else
                sql &= " , relate_cfb_no = '" & txtir_cfb.Text & "' "
            End If

            If txtrelate_ir.Text = "" Then
                sql &= " , relate_ir_no = null "
            Else
                sql &= " , relate_ir_no = '" & txtrelate_ir.Text & "' "
            End If

            sql &= " , log_safety_goal = '" & addslashes(txtlog_safety.SelectedValue) & "' "
            sql &= " , log_safety_goal2 = '" & addslashes(txtlog_safety2.SelectedValue) & "' "
            sql &= " , log_lab_id = '" & txtlog_lab.SelectedValue & "' "
            If txtlog_lab.SelectedValue = "" Then
                sql &= " , log_lab_name = '' "
            Else
                sql &= " , log_lab_name = '" & addslashes(txtlog_lab.SelectedItem.Text) & "' "
            End If

            If txtlog_asa.SelectedValue = "" Then
                sql &= " , log_asa_name = '' "
            Else
                sql &= " , log_asa_name = '" & addslashes(txtlog_asa.SelectedItem.Text) & "' "
            End If
            sql &= " , log_asa_id = '" & txtlog_asa.SelectedValue & "' "
            sql &= " , repeat_ir_no = '" & txtrepeatIR.Text & "' "
            sql &= " , tqm_report_type = '" & txtreporttype.SelectedValue & "' "
            sql &= " , tqm_report_type_name = '" & txtreporttype.SelectedItem.Text & "' "

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

            sql &= " , chk_follow_id = '" & txtfollow.SelectedValue & "' "
            sql &= " , chk_follow_text = '" & txtfollow.SelectedItem.Text & "' "
            sql &= " , ir_related_standard = '" & addslashes(txtrelated_standard.Text) & "' "

            sql &= " WHERE ir_id = " & irId

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If

        End Sub

        Sub bindTQMTab()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_tqm_tab a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id INNER JOIN ir_trans_list c ON a.ir_id = c.ir_id WHERE a.ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    'txttqm_doctor.SelectedValue = ds.Tables("t1").Rows(0)("physician_id").ToString
                    txtgrandtopic.SelectedValue = ds.Tables("t1").Rows(0)("grand_topic").ToString

                    bindComboNormalTopic()
                    txtnormaltopic.SelectedValue = ds.Tables("t1").Rows(0)("topic").ToString
                    bindComboSubTopic("")
                    ' bindComboSubTopic("2")
                    txtsubtopic1.SelectedValue = ds.Tables("t1").Rows(0)("subtopic1").ToString
                    bindComboSubTopic2("")
                    txtsubtopic2.SelectedValue = ds.Tables("t1").Rows(0)("subtopic2").ToString
                    bindComboSubTopic3("")
                    txtsubtopic3.SelectedValue = ds.Tables("t1").Rows(0)("subtopic3").ToString

                    txttqm_detail.Text = ds.Tables("t1").Rows(0)("tqm_topic_detail").ToString
                    txtcase_owner.Text = ds.Tables("t1").Rows(0)("tqm_case_owner").ToString
                    ' txtcause.SelectedValue = ds.Tables("t1").Rows(0)("ir_type_no").ToString
                    '  txtcause_detail.Value = ds.Tables("t1").Rows(0)("incident_detail").ToString
                    txttqm_remark.Value = ds.Tables("t1").Rows(0)("tqm_remark").ToString
                    txtaction_tqm_detail.Value = ds.Tables("t1").Rows(0)("action_detail").ToString

                    If ds.Tables("t1").Rows(0)("severe_level_id").ToString = "1" Then
                        txtsevere_level1.Checked = True
                    ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "2" Then
                        txtsevere_level2.Checked = True
                    ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "3" Then
                        txtsevere_level3.Checked = True
                    ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "4" Then
                        txtsevere_level4.Checked = True
                    ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "5" Then
                        txtsevere_level5.Checked = True
                    ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "6" Then
                        txtsevere_level6.Checked = True
                    ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "7" Then
                        txtsevere_level7.Checked = True
                    End If


                    If ds.Tables("t1").Rows(0)("is_concern").ToString = "1" Then
                        txttqmconcern1.Checked = True
                    Else
                        txttqmconcern2.Checked = True
                    End If

                    If ds.Tables("t1").Rows(0)("is_refer").ToString = "1" Then
                        txttqmrefer1.Checked = True
                    Else
                        txttqmrefer2.Checked = True
                    End If

                    txtir_cfb.Text = ds.Tables("t1").Rows(0)("relate_cfb_no").ToString
                    txtrelate_ir.Text = ds.Tables("t1").Rows(0)("relate_ir_no").ToString

                    txtlog_safety.SelectedValue = ds.Tables("t1").Rows(0)("log_safety_goal").ToString
                    txtlog_safety2.SelectedValue = ds.Tables("t1").Rows(0)("log_safety_goal2").ToString
                    txtlog_lab.SelectedValue = ds.Tables("t1").Rows(0)("log_lab_id").ToString
                    txtlog_asa.SelectedValue = ds.Tables("t1").Rows(0)("log_asa_id").ToString
                    txtrepeatIR.Text = ds.Tables("t1").Rows(0)("repeat_ir_no").ToString
                    Try
                        txtreporttype.SelectedValue = ds.Tables("t1").Rows(0)("tqm_report_type").ToString
                    Catch ex As Exception

                    End Try

                    If ds.Tables("t1").Rows(0)("tqm_chk_write").ToString = "1" Then
                        chk_write.Checked = True
                    End If

                    If ds.Tables("t1").Rows(0)("tqm_chk_remove").ToString = "1" Then
                        chk_remove.Checked = True
                    End If

                    If ds.Tables("t1").Rows(0)("tqm_chk_refund").ToString = "1" Then
                        chk_refund.Checked = True
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

                    lblTQMContact.Text = ds.Tables("t1").Rows(0)("report_tel").ToString
                    lblTQMReportby.Text = ds.Tables("t1").Rows(0)("report_by").ToString & " , " & ds.Tables("t1").Rows(0)("date_report").ToString

                    Try
                        txtfollow.SelectedValue = ds.Tables("t1").Rows(0)("chk_follow_id").ToString
                    Catch ex As Exception

                    End Try
                    txtrelated_standard.Text = ds.Tables("t1").Rows(0)("ir_related_standard").ToString


                End If

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try

        End Sub

        Sub bindDeptTab()
            Dim sql As String
            Dim ds As New DataSet

            Try
                If txtdept_tab.SelectedValue <> "" Then


                    sql = "SELECT * FROM ir_relate_dept WHERE ISNULL(is_dept_delete,0) = 0 AND  ir_id = " & irId & " AND dept_id = " & txtdept_tab.SelectedValue
                    ' Response.Write(sql)
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    If ds.Tables("t1").Rows.Count > 0 Then
                        txtrelateid.Text = ds.Tables("t1").Rows(0)("relate_dept_id").ToString
                        txtdept_invest.Value = ds.Tables("t1").Rows(0)("investigation").ToString
                        txtdept_cause.Value = ds.Tables("t1").Rows(0)("cause_detail").ToString
                        Try
                            txtfms_damage.SelectedIndex = CInt(ds.Tables("t1").Rows(0)("fms_damage_type_id").ToString)
                            txtfms_work.SelectedIndex = CInt(ds.Tables("t1").Rows(0)("fms_work_id").ToString)
                        Catch ex As Exception

                        End Try

                        cmdDeptAddCause.Enabled = True
                        cmdAddPrevent.Enabled = True
                    Else
                        cmdDeptAddCause.Enabled = False
                        cmdAddPrevent.Enabled = False
                    End If
                Else
                    cmdDeptAddCause.Enabled = False
                    cmdAddPrevent.Enabled = False
                End If
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try

        End Sub

        Sub bindDeptPreventiveAction()
            Dim sql As String
            Dim ds As New DataSet

            Try
                If txtdept_tab.SelectedIndex = 0 Then
                    GridViewPrevent.DataSource = Nothing
                    GridViewPrevent.DataBind()
                    Return
                End If

                If txtrelateid.Text <> "" Then
                    sql = "SELECT * FROM ir_dept_prevent_list WHERE relate_dept_id = " & txtrelateid.Text
                    sql &= " ORDER BY order_sort ASC"
                    ds = conn.getDataSetForTransaction(sql, "t1")


                    If ds.Tables(0).Rows.Count = 0 Then

                        ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
                        GridViewPrevent.DataSource = ds
                        GridViewPrevent.DataBind()

                        Dim columnCount As Integer = GridViewPrevent.Rows(0).Cells.Count
                        GridViewPrevent.Rows(0).Cells.Clear()
                        GridViewPrevent.Rows(0).Cells.Add(New TableCell)
                        GridViewPrevent.Rows(0).Cells(0).ColumnSpan = columnCount
                        GridViewPrevent.Rows(0).Cells(0).Text = "No Records Found."
                    Else
                        GridViewPrevent.DataSource = ds
                        GridViewPrevent.DataBind()
                    End If
                Else

                    ' txtrelateid.Text = -1

                End If

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindTQMPreventiveAction()
            Dim sql As String
            Dim ds As New DataSet

            Try
                If irId <> "" Then
                    sql = "SELECT * FROM ir_tqm_prevent_list a LEFT OUTER JOIN user_dept b ON a.dept_id = b.dept_id WHERE ir_id = " & irId
                    sql &= " ORDER BY order_sort ASC"
                    ds = conn.getDataSetForTransaction(sql, "t1")
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

        Sub updateDeptTab() ' สำหรับหน้า Department Tab
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            sql = "UPDATE ir_relate_dept SET investigation = '" & addslashes(txtdept_invest.Value) & "' "
            sql &= " , cause_detail = '" & addslashes(txtdept_cause.Value) & "' "

            sql &= " , fms_damage_type_id = '" & txtfms_damage.SelectedIndex & "' "
            sql &= " , fms_damage_type_name = '" & txtfms_damage.Text & "' "
            sql &= " , fms_work_id = '" & txtfms_work.SelectedIndex & "' "
            sql &= " , fms_work_name = '" & txtfms_work.Text & "' "

            sql &= " , update_by = '" & addslashes(Session("user_fullname").ToString) & "' "
            sql &= " , update_date = GETDATE() "
            sql &= " , update_date_ts = '" & Date.Now.Ticks & "' "
            If mode = "edit" Then
                sql &= " WHERE ir_id = " & irId & " AND dept_id = " & txtdept_tab.SelectedValue
            ElseIf mode = "add" Then
                '  sql &= " , ir_id = " & new_ir_id
                ' sql &= " WHERE session_id = '" & session_id & "'"
            End If

            '   Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If

        End Sub

        Sub bindPSMTab() ' Tab PSM
            Dim ds As New DataSet
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "SELECT * FROM ir_psm_tab WHERE  ir_id = " & irId
                'sql &= " ORDER BY dept_name"
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtpsm_compensation.Value = ds.Tables(0).Rows(0)("psm_compensation").ToString
                    txtpsm_diagnonsis.Text = ds.Tables(0).Rows(0)("psm_dianosis").ToString
                    txtpsm_recommend.Value = ds.Tables(0).Rows(0)("psm_recommendation").ToString
                    txtpsm_remark.Value = ds.Tables(0).Rows(0)("psm_remark").ToString
                    txtpsm_date_expect.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("date_close_ts").ToString)
                    txtpsm_status.SelectedValue = ds.Tables(0).Rows(0)("psm_status_id").ToString

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
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub updatePSMTab() ' สำหรับหน้า PSM
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            sql = "UPDATE ir_psm_tab SET psm_compensation = '" & txtpsm_compensation.Value & "' "
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
            sql &= " WHERE ir_id = " & irId
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If

        End Sub


        Sub insertAlertLog(ByVal cmd As String)
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            sql = "INSERT INTO ir_alert_log (ir_id , alert_date , alert_date_ts , alert_method , subject , send_to , cc_to , bcc_to) VALUES( "
            sql &= "'" & irId & "' ,"
            sql &= "GETDATE() ,"
            sql &= "'" & Date.Now.Ticks & "' ,"
            sql &= "'" & cmd & "' ,"
            sql &= "'" & txtsubject.SelectedItem.Text & "' ,"
            If cmd = "SMS" Then
                sql &= "'" & addslashes(txtsend_sms.Text) & "' ,"
            Else
                sql &= "'" & txtto.Value & "', "
            End If
            sql &= "'" & txtcc.Value & "' ,"
            sql &= "'" & txtidBCCSelect.Value & "' "

            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If
        End Sub

        Protected Sub cmdSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendMail.Click
            Try
                If txtsubject.SelectedValue = "4" Then ' department

                    updateTranListLog("4") 'เปลี่ยน Status เป็น Need Investigate
                    updateOnlyLog("4", txtsubject.SelectedItem.Text)
                    updateStatusNeedInvestigate()
                ElseIf txtsubject.SelectedValue = "5" Then ' PSM

                    updateTranListLog("5")
                    updateOnlyLog("5", txtsubject.SelectedItem.Text)
                Else
                    updateOnlyLog("0", txtsubject.SelectedItem.Text)
                End If

                insertAlertLog("Email")
                If chk_sms.Checked = True Then
                    prepareSMS()
                    insertAlertLog("SMS")
                End If

                ' conn.setDBCommit()
                getMailAndSMS()

                conn.setDBCommit()

                bindGridAlertLog()
                txtto.Value = ""
                txtcc.Value = ""

                txtmessage.Value = ""
                txtsubject.SelectedIndex = 0
                txtsend_sms.Text = ""
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
                Response.End()
            End Try

            '   Response.Redirect("form_incident.aspx?mode=edit&irId=" & irId & "&formId=" & formId)
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
                    'Response.Write("sms " & sendSMS(dataPack))
                End If
            Next

            ' insertAlertLog("SMS")
         
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
                Dim p3 As String = "[IR #" & txtirno.Text & "]  HN" & txtpthn.Text & " " & txtptname.Value & " : " & txtsubject.SelectedItem.Text
                Dim p4 As String = txtmessage.Value
                Dim p5 = ""

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

                key = UserActivation.GetActivationLink("incident/form_incident.aspx?mode=edit&irid=" & irId & "&req=" & req & "&formid=" & formId)
                msgbody &= "<a href='http://bhtraining/login.aspx?viewtype=" & emailviewtype & "&req=" & req & "&key=" & key & "'>" & "http://bhtraining/login.aspx?viewtype=" & emailviewtype & "&req=" & req & "&key=" & key & " </a>"

                txtmessage.Value = txtmessage.Value.Replace("http://bhportal", msgbody)

                'Response.Write(txtmessage.Value.Replace(vbCrLf, "<br/>"))
                'Response.End()

                sendMailWithCC1(email_list, cc_list, bcc_list, p3, txtmessage.Value.Replace(vbCrLf, "<br/>"), "", "ir")

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

                email_list = "TQMIncident@bumrungrad.com".Split(",") ' create emailTo array
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
                Dim p4 As String = "Investigation : " & vbCrLf & txtdept_invest.Value & vbCrLf & vbCrLf & txtdept_cause.Value & vbCrLf & vbCrLf
                Dim p5 = ""


                p3 = "[IR #" & txtirno.Text & "] " & addslashes(Session("user_fullname").ToString) & ", " & addslashes(Session("dept_name").ToString) & " was investigated case, " & Date.Now.ToString

                ' Dim parameters As Object() = New Object() {p1, p2, p3, p4, p5}

                'thread.Start(parameters)

                sendMailWithCC(email_list, cc_list, bcc_list, p3, p4, "", "ir")

            Catch ex As Exception
                Response.Write("Send mail :: " & ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub cmdAddDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddDept.Click
            Dim sql As String
            Dim pk As String
            Dim errorMsg As String
            Try
                pk = getPK("relate_dept_id", "ir_relate_dept", conn)
                If mode = "add" Then
                    sql = "INSERT INTO ir_relate_dept (relate_dept_id ,  dept_name , dept_id , session_id) VALUES("
                    sql &= "'" & pk & "',"

                    sql &= "'" & txtadd_dept.SelectedItem.Text & "',"
                    sql &= "'" & txtadd_dept.SelectedValue & "',"
                    sql &= "'" & session_id & "' "
                    sql &= ")"
                Else
                    sql = "INSERT INTO ir_relate_dept (relate_dept_id , ir_id , dept_name , dept_id , session_id , create_by , create_date , create_date_ts) VALUES("
                    sql &= "'" & pk & "',"
                    sql &= "'" & irId & "',"
                    sql &= "'" & txtadd_dept.SelectedItem.Text & "',"
                    sql &= "'" & txtadd_dept.SelectedValue & "',"
                    sql &= "'" & session_id & "' ,"
                    sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"
                    sql &= " GETDATE() ,"
                    sql &= "'" & Date.Now.Ticks & "' "
                    sql &= ")"
                End If

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                bindGridDept()
                bindComboDept()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridDept_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridDept.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                If mode = "add" Then
                    sql = "DELETE FROM ir_relate_dept WHERE relate_dept_id = " & GridDept.DataKeys(e.RowIndex).Value & ""
                Else
                    sql = "UPDATE ir_relate_dept SET is_dept_delete = 1 , delete_by = '" & addslashes(Session("user_fullname").ToString) & "' "
                    sql &= " , delete_date_raw = GETDATE() "
                    sql &= " , delete_date_ts = " & Date.Now.Ticks
                    sql &= " WHERE relate_dept_id = " & GridDept.DataKeys(e.RowIndex).Value & ""
                End If

                'Response.Write(sql)
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Response.Write(result)
                End If

                If mode = "edit" Then
                    updateOnlyLog(0, "Delete Revelant Unit : " & GridDept.DataKeys(e.RowIndex).Value)
                End If


                conn.setDBCommit()

                bindGridDept()
                If mode = "edit" Then
                    bindComboDept()
                    bindDeptTab()
                    bindGridIncidentLog()
                End If
               
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try

        End Sub

        Protected Sub txtdept_tab_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdept_tab.SelectedIndexChanged
            bindDeptTab()
            bindDeptPreventiveAction()
            bindDeptGridCause()
        End Sub

        Protected Sub cmdAddPrevent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddPrevent.Click
            Dim sql As String
            Dim result As String
            Dim ds As New DataSet

            Dim txt_addprevent As HtmlTextArea
            Dim txt_addperson As HtmlInputText
            Dim txtdate_prevent1 As TextBox
            Dim txtdate_prevent2 As TextBox
            Try
                txt_addprevent = CType(GridViewPrevent.FooterRow.FindControl("txt_addprevent"), HtmlTextArea)
                txt_addperson = CType(GridViewPrevent.FooterRow.FindControl("txt_addperson"), HtmlInputText)
                txtdate_prevent1 = CType(GridViewPrevent.FooterRow.FindControl("txtdate_prevent1"), TextBox)
                txtdate_prevent2 = CType(GridViewPrevent.FooterRow.FindControl("txtdate_prevent2"), TextBox)

                sql = "SELECT ISNULL(MAX(ORDER_SORT),0) + 1 FROM ir_dept_prevent_list WHERE relate_dept_id = " & txtrelateid.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                Dim order As String
                order = ds.Tables("t1").Rows(0)(0).ToString

                sql = "INSERT INTO ir_dept_prevent_list (relate_dept_id , action_detail , resp_person , dept_id , ir_id , order_sort ,date_start , date_start_ts , date_end , date_end_ts)"
                sql &= " VALUES("
                sql &= "'" & txtrelateid.Text & "' ,"
                sql &= "'" & addslashes(txt_addprevent.Value) & "' ,"
                sql &= "'" & addslashes(txt_addperson.Value) & "' ,"
                sql &= "'" & txtdept_tab.SelectedValue & "' ,"
                sql &= "'" & irId & "' ,"
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

        Sub addPreventNoCommit()
            Dim sql As String
            Dim result As String
            Dim ds As New DataSet

            Dim txt_addprevent As HtmlTextArea
            Dim txt_addperson As HtmlInputText
            Dim txtdate_prevent1 As TextBox
            Dim txtdate_prevent2 As TextBox

            txt_addprevent = CType(GridViewPrevent.FooterRow.FindControl("txt_addprevent"), HtmlTextArea)
            txt_addperson = CType(GridViewPrevent.FooterRow.FindControl("txt_addperson"), HtmlInputText)
            txtdate_prevent1 = CType(GridViewPrevent.FooterRow.FindControl("txtdate_prevent1"), TextBox)
            txtdate_prevent2 = CType(GridViewPrevent.FooterRow.FindControl("txtdate_prevent2"), TextBox)

            sql = "SELECT ISNULL(MAX(ORDER_SORT),0) + 1 FROM ir_dept_prevent_list WHERE relate_dept_id = " & txtrelateid.Text
            ds = conn.getDataSetForTransaction(sql, "t1")
            Dim order As String
            order = ds.Tables("t1").Rows(0)(0).ToString

            sql = "INSERT INTO ir_dept_prevent_list (relate_dept_id , action_detail , resp_person , dept_id , ir_id , order_sort ,date_start , date_start_ts , date_end , date_end_ts)"
            sql &= " VALUES("
            sql &= "'" & txtrelateid.Text & "' ,"
            sql &= "'" & addslashes(txt_addprevent.Value) & "' ,"
            sql &= "'" & addslashes(txt_addperson.Value) & "' ,"
            sql &= "'" & txtdept_tab.SelectedValue & "' ,"
            sql &= "'" & irId & "' ,"
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


        End Sub

        Protected Sub GridDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridDept.SelectedIndexChanged

        End Sub

        Protected Sub GridViewPrevent_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewPrevent.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM ir_dept_prevent_list WHERE prevent_dept_id = " & GridViewPrevent.DataKeys(e.RowIndex).Value & ""
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

        Protected Sub GridviewIncidentLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridviewIncidentLog.PageIndexChanging
            GridviewIncidentLog.PageIndex = e.NewPageIndex
            bindGridIncidentLog()
        End Sub

        Protected Sub txtgrandtopic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtgrandtopic.SelectedIndexChanged
            bindComboNormalTopic()
        End Sub

        Protected Sub txtnormaltopic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtnormaltopic.SelectedIndexChanged
            bindComboSubTopic("1")
            'bindComboSubTopic("2")
        End Sub



        Protected Sub txtlang_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtlang.SelectedIndexChanged
            ' Response.Write(" bindLanguage :" & Session("lang").ToString & "<br/>")
            ' Response.Write(txtlang.SelectedValue)
            bindLanguage(txtlang.SelectedValue)
            ' Session("lang") = txtlang.SelectedValue
            'Session("lang") = txtlang.SelectedValue
            'Response.Write(" bindLanguage :" & Session("lang").ToString & "<br/>")
            ' Response.Write(txtlang.SelectedValue)
        End Sub

        Protected Sub cmdAddCause_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddCause.Click
            txtdept_cause.Value &= txtadd_cause.SelectedItem.Text & vbCrLf
        End Sub

        Protected Sub cmdSaveOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveOrder.Click
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

                    sql = "UPDATE ir_dept_prevent_list SET order_sort = '" & txtorder.Text & "' WHERE prevent_dept_id = " & lbl.Text

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

        Sub saveOrderDeptNoCommit()
            Dim sql As String
            Dim errorMsg As String
            Dim i As Integer

            Dim lbl As Label
            Dim txtorder As TextBox

            i = GridViewPrevent.Rows.Count

            If i = 0 Then
                Return
            End If

            For s As Integer = 0 To i - 1

                lbl = CType(GridViewPrevent.Rows(s).FindControl("lblPK"), Label)
                txtorder = CType(GridViewPrevent.Rows(s).FindControl("txtorder"), TextBox)

                sql = "UPDATE ir_dept_prevent_list SET order_sort = '" & txtorder.Text & "' WHERE prevent_dept_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
            Next s

        End Sub
       
        Protected Sub txtsubtopic1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsubtopic1.SelectedIndexChanged
            bindComboSubTopic2("")
        End Sub

        Protected Sub txtsubtopic2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsubtopic2.SelectedIndexChanged
            bindComboSubTopic3("")
        End Sub

        Sub bindTQMDoctor()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_doctor_defendant a INNER JOIN m_doctor b ON a.md_code = b.emp_no WHERE a.ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")

                GridViewTQMDoctor.DataSource = ds
                GridViewTQMDoctor.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindTQMGridCause()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_tqm_cause_list WHERE ir_id = " & irId
                sql &= " ORDER BY order_sort"
                ds = conn.getDataSetForTransaction(sql, "t1")

                GridTQMCause.DataSource = ds
                GridTQMCause.DataBind()
               
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindDeptGridCause()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                If txtrelateid.Text = "" Then
                    'txtrelateid.Text = -1
                    Return
                End If

                If txtdept_tab.SelectedIndex = 0 Then
                    GridDeptCause.DataSource = Nothing
                    GridDeptCause.DataBind()
                    Return
                End If

                sql = "SELECT * FROM ir_dept_cause_list WHERE relate_dept_id = " & txtrelateid.Text
                sql &= " ORDER BY order_sort"
                ds = conn.getDataSetForTransaction(sql, "t1")
                ' Response.Write(sql)
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

        Protected Sub cmdTQMAddDoctor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTQMAddDoctor.Click
            Dim sql As String
            Dim errorMsg As String

            Try
                sql = "INSERT INTO ir_doctor_defendant (ir_id , doctor_name , md_code , monitor_flag) VALUES("
                sql &= "" & irId & " , "
                sql &= "'" & txttqm_finddoctor.Text & "' , "
                sql &= "'" & txtmdcode.Text & "'  ,"
                sql &= "'" & txtmonitor.SelectedValue & "'  "
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

        Protected Sub cmdReopen2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReopen2.Click
            Response.Redirect("reopen_ir.aspx?irId=" & irId & "&formId=" & formId)
        End Sub

        Protected Sub GridviewIncidentLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridviewIncidentLog.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblReportby As Label = CType(e.Row.FindControl("lblReportby"), Label)
                Dim lblStatusID As Label = CType(e.Row.FindControl("lblStatusID"), Label)
                Dim lblLog As Label = CType(e.Row.FindControl("lblLog"), Label)
                Dim sql As String
                Dim ds As New DataSet
                Try
        
                    If lblStatusID.Text = "2" Then
                        lblReportby.Text = "Reported by"
                    ElseIf lblStatusID.Text = "3" Then
                        lblReportby.Text = "Received by"
                    ElseIf lblStatusID.Text = "4" Then
                        '   lblReportby.Text = "N/A"
                        lblReportby.Text = "N/A : " & lblLog.Text
                    ElseIf lblStatusID.Text = "7" Then
                        lblReportby.Text = "Investigated by"
                    ElseIf lblStatusID.Text = "9" Then
                        lblReportby.Text = "Closed by"
                    ElseIf lblStatusID.Text = "91" Then
                        lblReportby.Text = "Re-open"
                    Else
                        lblReportby.Text = lblLog.Text
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

        Sub bindDefendantUnitCombo()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM m_dept_unit"
                sql &= " ORDER BY dept_unit_name ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")
                '   Response.Write(sql)

                txtadd_cause_defendant.DataSource = ds
                txtadd_cause_defendant.DataBind()
                txtadd_cause_defendant.Items.Insert(0, New ListItem("", 0))
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub
        Protected Sub cmdTQMAddCause_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTQMAddCause.Click
            Dim sql As String
            Dim errorMsg As String
            Dim new_order_sort As String = "1"
            Dim ds As New DataSet

            Try
                sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ir_tqm_cause_list WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t0")
                new_order_sort = ds.Tables("t0").Rows(0)(0).ToString

                sql = "INSERT INTO ir_tqm_cause_list (ir_id , cause_id , cause_name , cause_remark , ir_type  , dept_unit_name , dept_unit_id , order_sort) VALUES("
                sql &= "" & irId & " ,"
                sql &= "'" & txtcause.SelectedValue & "' ,"
                sql &= "'" & txtcause.SelectedItem.Text & "' ,"
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
                txtcause.SelectedIndex = 0
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
            Dim ds As New DataSet
            Try
                sql = "DELETE FROM ir_tqm_cause_list WHERE tqm_cause_id = " & GridTQMCause.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    'Response.Write(result)
                    Throw New Exception(result)
                End If

                sql = "SELECT * FROM ir_tqm_cause_list WHERE ir_id = " & irId & " ORDER BY order_sort"
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    sql = "UPDATE ir_tqm_cause_list SET order_sort = " & i + 1 & " WHERE tqm_cause_id = " & ds.Tables("t1").Rows(i)("tqm_cause_id").ToString
                    result = conn.executeSQLForTransaction(sql)
                    If result <> "" Then

                        Throw New Exception(result)
                    End If
                Next i

                conn.setDBCommit()
                bindTQMGridCause()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

   
        Protected Sub GridTQMCause_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridTQMCause.SelectedIndexChanged

        End Sub

        Protected Sub GridViewPrevent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewPrevent.SelectedIndexChanged

        End Sub

        Protected Sub txtsubject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsubject.SelectedIndexChanged
            If txtsubject.SelectedValue <> "" Then
                Dim template As String = ""
                template &= "Dear ," & vbCrLf & vbCrLf

                If txtsubject.SelectedValue = "4" Then
                    template &= "Please find incident report. Please investigate on this matter, and kindly send it back to us." & vbCrLf & vbCrLf
                ElseIf txtsubject.SelectedValue = "101" Then
                    template &= "Please find incident report for your information. If you have any feedback on this matter, please do not hesitate to let us know. " & vbCrLf & vbCrLf
                End If

                'template &= "To access new online incident and customer feedback report, please open BH Portal (http://bhportal) and select Incident Report or Customer Feedback Report under Operation Support Application menu. For more information please contact TQM department." & vbCrLf & vbCrLf
                template &= "To access new online incident and customer feedback report, please open this link below. " & vbCrLf & " (http://bhportal)" & vbCrLf & "For more information please contact IR&CFB department." & vbCrLf & vbCrLf


                template &= "Best Regards, " & vbCrLf
             

                txtmessage.Value = template
            Else
                txtmessage.Value = ""
            End If
        End Sub

        Protected Sub cmdChangeForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdChangeForm.Click
            Dim sql As String
            Dim errorMsg As String
            Try
                If txtchangeForm.SelectedValue = "" Then
                    Return
                End If

                sql = "UPDATE ir_trans_list SET form_id = " & txtchangeForm.SelectedValue
                sql &= " WHERE ir_id =" & irId
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

            Response.Redirect("form_incident.aspx?mode=edit&irId=" & irId & "&formId=" & txtchangeForm.SelectedValue)

        End Sub

        Protected Sub cmdPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
            Dim strURL As String = "http://" & ConfigurationManager.AppSettings("frontHost").ToString & "/incident/preview_incident.aspx?irId=" & irId & "&formId=" & formId & "&dept_id=" & Session("dept_id").ToString & "&viewtype=" & viewtype

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
                Dim writer As StreamWriter = New StreamWriter(Server.MapPath("../share/") & "test.html", False)
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

            Response.Redirect("../pdf/irprint.php")
            'cmdPrint.Attributes.Add("onClick", "window.open('../pdf/irprint.php')")
        End Sub

        Sub saveHTMLForPDF(ByVal irno As String)
            Dim host As String = ""

            host = ConfigurationManager.AppSettings("frontHost").ToString
            ' Dim strURL As String = "http://" & host & "/incident/preview_incident.aspx?irId=" & irId & "&dept_id=" & Session("dept_id").ToString

            Dim strURL As String = "http://" & ConfigurationManager.AppSettings("frontHost").ToString & "/incident/preview_incident.aspx?irId=" & irId & "&formId=" & formId & "&dept_id=" & Session("dept_id").ToString & "&viewtype=" & viewtype

            '   Response.Write(strURL)

            Dim uri As New Uri(strURL)

            'Create the request object

            Dim req As HttpWebRequest = DirectCast(WebRequest.Create(uri), HttpWebRequest)
            req.UserAgent = "Get Content"
            Dim resp As WebResponse = req.GetResponse()
            Dim stream As Stream = resp.GetResponseStream()
            Dim sr As New StreamReader(stream)
            Dim html As String = sr.ReadToEnd()


            'Response.Write(html)
            ' Response.End()
            'Return
            Try
                'Response.Write(Server.MapPath("../share/incident/revision/") & rev_no & ".html")
                'File.Create(Server.MapPath("../share/incident/revision/") & rev_no & ".html")

                Dim writer As StreamWriter = New StreamWriter(Server.MapPath("../share/incident/revision/") & revision_no & ".html", False)
                writer.WriteLine(html)
                writer.Close()
                '   writer1.Close()
                'fp = File.CreateText(Server.MapPath("../share/") & "test.html")
                'fp.WriteLine(html)

                'fp.Close()
                Dim objRequest As HttpWebRequest = DirectCast(WebRequest.Create("http://" & host & "/pdf/irrevision.php?f=" & revision_no), HttpWebRequest)
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


        End Sub

        Sub bindInfoDepartment_Select()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM user_dept ORDER BY dept_name_en ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")

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
                sql = "SELECT * , dept_name AS dept_name_en FROM ir_dept_inform_update WHERE ir_id = " & irId
                sql &= " ORDER BY dept_name ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")

                txtinfo_dept2.DataSource = ds
                txtinfo_dept2.DataBind()

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

            sql = "DELETE FROM ir_dept_inform_update WHERE ir_id = " & irId

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If
            '  Response.Write("tttttt")
            For i As Integer = 0 To txtinfo_dept2.Items.Count - 1

                pk = getPK("info_id", "ir_dept_inform_update", conn)
                sql = "INSERT INTO ir_dept_inform_update (info_id , ir_id , dept_id , costcenter_id , dept_name , costcenter_name) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & irId & "' ,"
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

        Protected Sub cmdUploadFileMCO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUploadFileMCO.Click
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
                    If irId = "" Then
                        sql &= " null , "
                    Else
                        sql &= "" & irId & " , "
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

                    FileUpload2.PostedFile.SaveAs(Server.MapPath("../share/mco/ir/" & pk & "." & extension))

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
                    File.Delete(Server.MapPath("../share/mco/ir/" & lblFilePath.Text))
                Next s

                conn.setDBCommit()
                bindFileMCO()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub cmdAddUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddUpdate.Click
            Dim sql As String = ""
            Dim errorMsg As String = ""
            Dim pk As String = ""

            Try
                pk = getPK("inform_id", "ir_information_update", conn)
                sql = "INSERT INTO ir_information_update (inform_id , ir_id , inform_type , inform_detail , inform_date , inform_date_ts , inform_by , inform_emp_code , inform_dept_name , inform_costcenter) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & irId & "' ,"
                sql &= " 'ir' ,"
                sql &= " '" & addslashes(txtadd_update.Text) & "' ,"
                sql &= " GETDATE() ,"
                sql &= " '" & Date.Now.Ticks & "' ,"
                sql &= " '" & addslashes(Session("user_fullname").ToString) & " , " & addslashes(Session("user_position").ToString) & "' ,"
                sql &= " '" & Session("emp_code").ToString & "' ,"
                sql &= " '" & addslashes(Session("dept_name").ToString) & "' ,"
                sql &= " '" & Session("costcenter_id").ToString & "' "
                sql &= ")"
                '  Response.Write(sql)
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

        Sub bindPSMConcern()
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM ir_psm_concern_list a WHERE 1 = 1 "
                sql &= " AND a.ir_id = " & irId

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

                txtpsm_category.DataSource = ds.Tables(0)
                txtpsm_category.DataBind()

                txtpsm_category.Items.Insert(0, New ListItem("-- Please select --", ""))
                '  txtpsm_subcategory.Items.Insert(0, New ListItem("-- Please select --", ""))
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

                txtpsm_subcategory.DataSource = ds.Tables(0)
                txtpsm_subcategory.DataBind()

                txtpsm_subcategory.Items.Insert(0, New ListItem("-- Please select --", ""))

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
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

        Sub bindDeptPSM() ' Combo box
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

        Protected Sub txtpsm_category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpsm_category.SelectedIndexChanged
            bindMCOSubCategory()
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
                sql &= " '" & irId & "' ,"
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
                    sql &= " '" & irId & "' ,"
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
                    sql &= " '" & irId & "' ,"
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

        Protected Sub cmdPSMAddDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMAddDoc.Click
            txtpsm_list_doctor.Items.Add(New ListItem(txtpsm_add_doctor.Text & " " & txtpsm_add_special.Value, txtpsm_add_doctor.Text & " " & txtpsm_add_special.Value))

            txtpsm_add_doctor.Text = ""
            txtpsm_add_special.Value = ""
        End Sub

        Protected Sub cmdPSMDelDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMDelDoc.Click
            If txtpsm_list_doctor.SelectedIndex = -1 Then
                Return
            End If

            txtpsm_list_doctor.Items.RemoveAt(txtpsm_list_doctor.SelectedIndex)
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
                ' Response.Write(sql)
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

        Sub bindDefendantUnit_TQM_Combo()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                'sql = "SELECT * FROM user_dept ORDER BY dept_name_en ASC"
                sql = "SELECT * FROM m_dept_unit a "
                sql &= " ORDER BY a.dept_unit_name"
                ' sql &= " ORDER BY a.dept_name_en ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")
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


        Sub bindDefendantUnit_Select()
            Dim sql As String = ""
            Dim ds As New DataSet

            Try
                'sql = "SELECT * FROM user_dept ORDER BY dept_name_en ASC"
                sql = "SELECT * FROM m_dept_unit a WHERE dept_unit_id NOT IN (SELECT dept_unit_id FROM ir_cfb_unit_defendant WHERE ir_id = " & irId & " ) "
                sql &= " ORDER BY a.dept_unit_name"
                ' sql &= " ORDER BY a.dept_name_en ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")
                ' Response.Write(sql)
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
                sql = "SELECT * FROM ir_cfb_unit_defendant a WHERE 1 = 1 AND a.ir_id = " & irId
                sql &= " ORDER BY a.dept_unit_name"
                ds = conn.getDataSetForTransaction(sql, "t1")
                ' Response.Write(sql)
                txtunit_defandent_select.DataSource = ds
                txtunit_defandent_select.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message & ":" & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

    

        Sub updateDefendantUnit() ' Defendant unit
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim ds As New DataSet

            sql = "DELETE FROM ir_cfb_unit_defendant WHERE ir_id = " & irId
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If

            For i As Integer = 0 To txtunit_defandent_select.Items.Count - 1

                pk = getPK("unit_defendant_id", "ir_cfb_unit_defendant", conn)

                sql = "INSERT INTO ir_cfb_unit_defendant (unit_defendant_id , ir_id , dept_unit_id , dept_unit_name) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & irId & "' ,"
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

        Sub bindGridRelateDocument(Optional ByVal docType As String = "ir")
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_relate_document WHERE reference_type = '" & docType & "' AND ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")

                If docType = "ir" Then
                    GridRelateIR.DataSource = ds
                    GridRelateIR.DataBind()
                ElseIf docType = "cfb" Then
                    GridRelateCFB.DataSource = ds
                    GridRelateCFB.DataBind()
                End If
               

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub cmdAddCFBNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddCFBNo.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String
            Try
                pk = getPK("ir_relate_document_id", "ir_relate_document", conn)
                sql = "INSERT INTO ir_relate_document (ir_relate_document_id , ir_id , reference_no , reference_type) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & irId & "' ,"
                sql &= " '" & txtadd_cfbno.Text & "' ,"
                sql &= " 'cfb' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                txtadd_cfbno.Text = ""
                bindGridRelateDocument("cfb")
            Catch ex As Exception
                txtadd_cfbno.Text = ex.Message & sql
                ' Response.Write(ex.Message)
                conn.setDBRollback()
            End Try
        End Sub

        Protected Sub GridRelateCFB_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridRelateCFB.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM ir_relate_document WHERE ir_relate_document_id = " & GridRelateCFB.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Response.Write(result)
                End If

                conn.setDBCommit()
                bindGridRelateDocument("cfb")
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub cmdAddIRNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddIRNO.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String
            Try
                pk = getPK("ir_relate_document_id", "ir_relate_document", conn)
                sql = "INSERT INTO ir_relate_document (ir_relate_document_id , ir_id , reference_no , reference_type) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & irId & "' ,"
                sql &= " '" & txtadd_irno.Text & "' ,"
                sql &= " 'ir' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                txtadd_irno.Text = ""
                bindGridRelateDocument("ir")
            Catch ex As Exception
                txtadd_cfbno.Text = ex.Message & sql
                ' Response.Write(ex.Message)
                conn.setDBRollback()
            End Try
        End Sub

        Protected Sub GridRelateCFB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridRelateCFB.SelectedIndexChanged

        End Sub

        Protected Sub GridRelateIR_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridRelateIR.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM ir_relate_document WHERE ir_relate_document_id = " & GridRelateIR.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Response.Write(result)
                End If
                ' txtadd_irno.Text = sql
                conn.setDBCommit()
                bindGridRelateDocument("ir")
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridRelateIR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridRelateIR.SelectedIndexChanged

        End Sub

        Protected Sub txtadd_cause_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtadd_cause.SelectedIndexChanged
            If txtadd_cause.SelectedValue <> "" Then
                txtdept_cause.Value &= txtadd_cause.SelectedItem.Text & vbCrLf
            End If
        End Sub

        Protected Sub cmdRestore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRestore.Click
            Dim sql As String
            Dim errorMsg As String
            Try
                sql = "UPDATE ir_trans_list SET is_cancel = 0 WHERE ir_id = " & irId
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

            Response.Redirect("form_incident.aspx?mode=edit&irId=" & irId & "&formId=" & formId)
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

                    sql = "UPDATE ir_tqm_cause_list SET order_sort = '" & txtorder.Text & "' WHERE tqm_cause_id = " & lbl.Text

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

        Protected Sub cmdPrintTQM_Click(sender As Object, e As System.EventArgs) Handles cmdPrintTQM.Click
            Dim strURL As String = "http://" & ConfigurationManager.AppSettings("frontHost").ToString & "/incident/preview_tqm_tab.aspx?irId=" & irId & "&formId=" & formId & "&dept_id=" & Session("dept_id").ToString & "&viewtype=" & viewtype

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
                Dim writer As StreamWriter = New StreamWriter(Server.MapPath("../share/") & "test2.html", False)
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

            Response.Redirect("../pdf/irprintTQM.php")
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

        Protected Sub cmdConvert_Click(sender As Object, e As EventArgs) Handles cmdConvert.Click
            Dim sql As String
            Dim errMsg As String
            Dim ds As New DataSet
            Dim ds2 As New DataSet
            Dim cfb_no As String = ""
            Dim new_order_sort As String = ""
            Dim report_by As String = ""
            Dim pk As String = ""
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

                sql = "UPDATE ir_trans_list SET report_type = 'cfb'  , status_id = " & status & " , date_submit = GETDATE() , date_submit_ts = " & Date.Now.Ticks & " , is_move_to_cfb = 1 , is_move_to_ir = 0 WHERE ir_id = " & irId
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg)
                End If

                If txtconvertstatus.SelectedIndex = 0 Then ' ถ้าเลือก Current Status
                  
                Else ' Draft Status
                    sql = "UPDATE ir_trans_list SET date_submit = null  , date_submit_ts = 0  "
                    sql &= " , report_by = '" & Session("user_fullname").ToString & "' "
                    sql &= " ,report_emp_code = '" & Session("emp_code").ToString & "' "
                    sql &= " WHERE ir_id = " & irId

                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If
                End If
                'Response.Write(sql)
                isHasRow("cfb_detail_tab")

                sql = "SELECT * FROM cfb_detail_tab WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (ds.Tables("t1").Rows(0)("cfb_no").ToString = "" Or ds.Tables("t1").Rows(0)("cfb_no").ToString = "0") Then
                    cfb_no = getNewCFBNo()
                Else
                    cfb_no = ds.Tables("t1").Rows(0)("cfb_no").ToString
                End If

                If status = 1 Then
                    cfb_no = "0"
                End If

                'sql = "UPDATE ir_detail_tab SET ir_no = '0'  WHERE ir_id = " & irId
                'errMsg = conn.executeSQLForTransaction(sql)
                'If errMsg <> "" Then
                '    Throw New Exception(errMsg)
                'End If

                sql = "SELECT * FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id WHERE a.ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")
                report_by = ds.Tables("t1").Rows(0)("report_by").ToString

                sql = "UPDATE cfb_detail_tab SET hn = '" & txtpthn.Text & "'"

                sql &= " , cfb_no = '" & cfb_no & "'"

                sql &= " , division = '" & txtdivision.Text & "'"
                sql &= " , report_tel = '" & addslashes(txtcontact.Text) & "'"
                sql &= " , report_tel2 = '" & addslashes(lblReport.Text) & "'"
                sql &= " , diagnosis = '" & addslashes(txtdiagnosis.Value) & "'"
                sql &= " , operation = '" & addslashes(txtoperation.Value) & "'"
                sql &= " , location = '" & addslashes(txtlocation.Text) & "'"
                ' sql &= " , location = '" & addslashes(txtlocation_combo.Text) & "'"
                sql &= " , room = '" & addslashes(txtroom.Text) & "'"
                sql &= " , datetime_complaint = '" & convertToSQLDatetime(txtdate_report.Value) & "'"
                sql &= " , datetime_complaint_ts = " & ConvertDateStringToTimeStamp(txtdate_report.Value) & ""

                sql &= " , dept_id_report = '" & ds.Tables("t1").Rows(0)("dept_id").ToString & "'"
                'sql &= " , hn = '" & txthn.Value & "'"
                sql &= " , datetime_report = '" & convertToSQLDatetime(txtdate_report.Value, txthour.SelectedValue.PadLeft(2, "0"), txtmin.SelectedValue.PadLeft(2, "0")) & "'"
                sql &= " , datetime_report_ts = " & ConvertDateStringToTimeStamp(txtdate_report.Value, CInt(txthour.SelectedValue), CInt(txtmin.SelectedValue)) & ""
                sql &= " , complain_detail = '" & addslashes(txtptname.Value) & "'"
                sql &= " , service_type = '" & txtservicetype.SelectedValue & "'"
                sql &= " , pt_title = '" & txttitle.SelectedValue & "'"
                sql &= " , age = '" & txtptage.Value & "'"
                sql &= " , sex = '" & txtptsex.SelectedValue & "'"
                sql &= " , customer_segment = '" & txtsegment.SelectedValue & "'"
                'sql &= " , country = '" & txtcountry.Text & "'"
                'sql &= " , cfb_dept_id = '" & txtdept.SelectedValue & "'"
                ' sql &= " , cfb_dept_name = '" & txtdept.SelectedItem.Text & "'"
                'sql &= " , complain_status = '" & txtcomplain_status.SelectedValue & "'"
                'sql &= " , feedback_from = '" & addslashes(txtfeedback_from.SelectedValue) & "'"
                'sql &= " , complain_status_remark = '" & addslashes(txtcomplain_remark.Value) & "'"
                'sql &= " , feedback_from_remark = '" & addslashes(txtfeedback_remark.Value) & "'"

                'sql &= " , part_customer = '" & addslashes(txtpart_customer.Value) & "'"
                'sql &= " , part_hospital = '" & addslashes(txtpart_hospital.Value) & "'"
                'sql &= " , part_employee = '" & addslashes(txtpart_employee.Value) & "'"

                'If txtcom1.Checked = True Then
                '    sql &= " , cfb_is_complain =  1 "
                'ElseIf txtcom2.Checked = True Then
                '    sql &= " , cfb_is_complain =  0 "
                'Else
                '    sql &= " , cfb_is_complain = null"
                'End If

                'sql &= " , cfb_customer_resp = '" & txtcustomer.SelectedValue & "'"
                'sql &= " , cfb_customer_resp_remark = '" & addslashes(txtcus_detail.Value) & "'"

                'If chk_tel.Checked = True Then
                '    sql &= " , cfb_chk_tel = 1 "
                'Else
                '    sql &= " , cfb_chk_tel = 0 "
                'End If

                'If chk_email.Checked = True Then
                '    sql &= " , cfb_chk_email = 1 "
                'Else
                '    sql &= " , cfb_chk_email = 0 "
                'End If

                'If chk_othter.Checked = True Then
                '    sql &= " , cfb_chk_other = 1 "
                'Else
                '    sql &= " , cfb_chk_other = 0 "
                'End If

                'sql &= " , cfb_tel_remark = '" & addslashes(txttel.Value) & "'"
                'sql &= " , cfb_email_remark = '" & addslashes(txtemail.Value) & "'"
                'sql &= " , cfb_other_remark = '" & addslashes(txtother.Value) & "'"

                sql &= " WHERE ir_id = " & irId
                'Response.Write(status_id)
                ' Response.End()
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg & ":" & sql)
                End If

                sql = "SELECT * FROM ir_relate_dept WHERE isnull(is_dept_delete,0) = 0 AND ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "tDept")

                If ds.Tables("tDept").Rows.Count = 0 Then
                    sql = "INSERT INTO cfb_comment_list (ir_id , comment_type_id , comment_type_name , comment_detail , comment_solution , lastupdate_by , lastupdate_time , order_sort) VALUES("
                    sql &= "'" & irId & "' ,"
                    sql &= "'3' ,"
                    sql &= "'Complaint' ,"
                    sql &= "'" & addslashes(txtoccurrence.Value) & "' ,"
                    sql &= "'" & addslashes(txtinitial.Value) & "' ,"
                    sql &= "'" & report_by & "' , "
                    sql &= " GETDATE() , "
                    sql &= "'" & 0 & "' "
                    sql &= ")"


                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If
                End If

                For i As Integer = 0 To ds.Tables("tDept").Rows.Count - 1

                    'sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM cfb_comment_list WHERE "
                    'sql &= " ir_id = " & irId

                    '  ds = conn.getDataSetForTransaction(sql, "t1")
                    ' new_order_sort = ds.Tables(0).Rows(0)(0).ToString

                    sql = "INSERT INTO cfb_comment_list (ir_id , comment_type_id , comment_type_name , comment_detail , comment_solution , lastupdate_by , lastupdate_time , order_sort) VALUES("
                    sql &= "'" & irId & "' ,"
                    sql &= "'3' ,"
                    sql &= "'Complaint' ,"
                    sql &= "'" & addslashes(txtoccurrence.Value) & "' ,"
                    sql &= "'" & addslashes(txtinitial.Value) & "' ,"
                    sql &= "'" & report_by & "' , "
                    sql &= " GETDATE() , "
                    sql &= "'" & 0 & "' "
                    sql &= ")"


                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If

                    sql = "SELECT MAX(comment_id) FROM cfb_comment_list"
                    ds2 = conn.getDataSetForTransaction(sql, "tMaxID")

                    pk = getPK("cfb_relate_dept_id", "cbf_relate_dept", conn)
                    sql = "INSERT INTO cbf_relate_dept (cfb_relate_dept_id , comment_id ,  dept_id , dept_name , session_id) VALUES("
                    sql &= "'" & pk & "',"
                    sql &= "'" & ds2.Tables("tMaxID").Rows(0)(0).ToString & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_id").ToString & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_name").ToString & "',"
                    sql &= "'" & Session.SessionID & "' "
                    sql &= ")"

                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If

                Next i

                Dim filename As String()
                Dim extension As String
                Dim iCount As Integer = 0

                sql = "SELECT * FROM ir_attachment WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "tFile")

                For iFile As Integer = 0 To ds.Tables("tFile").Rows.Count - 1

                    pk = getPK("file_id", "cfb_attachment", conn)

                    filename = ds.Tables("tFile").Rows(iFile)("file_name").ToString.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    sql = "INSERT INTO cfb_attachment (file_id , ir_id ,  file_name , file_path , file_size , session_id) VALUES( "
                    sql &= "" & pk & " , "

                    sql &= "" & irId & " , "
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
                        File.Delete(Server.MapPath("../share/cfb/attach_file/" & pk & "." & extension))
                    Catch ex As Exception

                    End Try

                    File.Copy(Server.MapPath("../share/incident/attach_file/" & ds.Tables("tFile").Rows(iFile)("file_path").ToString), Server.MapPath("../share/cfb/attach_file/" & pk & "." & extension))

                Next iFile

                updateOnlyLog("0", "Convert from Incident : " & txtirno.Text)

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
                sql = "UPDATE ir_trans_list SET form_id = " & txtchangeFormCopy.SelectedValue & " , status_id = 1 , date_submit = null , date_submit_ts = 0 , is_change_to_draft = 1  WHERE ir_id = " & irId
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg)
                End If
                'Response.Write(sql)


                'sql = "UPDATE ir_detail_tab SET ir_no = '0'  WHERE ir_id = " & irId
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
            Dim pk1 As String

            Try
                ' ======================= UPDATE ir_trans_list =========================
                Try
                    sql = "SELECT ISNULL(MAX(ir_id),0) + 1 AS pk FROM ir_trans_list"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    cfbPk = ds.Tables("t1").Rows(0)(0).ToString
                    ' new_ir_id = pk
                Catch ex As Exception
                    Response.Write(ex.Message & sql)
                    Response.Write(sql)
                    Return
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO ir_trans_list (ir_id , form_id , date_report, date_submit , date_submit_ts , status_id , report_type , report_by ,  report_emp_code)"
                sql &= " VALUES("
                sql &= "" & cfbPk & " ,"
                sql &= "" & txtchangeFormCopy.SelectedValue & " , "
                sql &= " GETDATE() ,"
                sql &= " null ,"
                sql &= " null ,"
                sql &= "1 ,"
                sql &= " 'ir' ,"
                sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"

                sql &= "'" & Session("emp_code").ToString & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
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
                'Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & ":" & sql)
                End If

                ' ======================= UPDATE IR Detail =========================
                sql = "INSERT INTO ir_detail_tab (ir_id,ir_sub_id,ir_no,division,dept_id,costcenter_id,report_tel,service_type,pt_title,pt_name,age,month_day_num,month_day_text,sex,hn,room,customer_segment,pt_type,pt_type_remark,clinical_type,diagnosis,operation,date_operation,date_operation_ts,physician,datetime_report,datetime_report_ts,flag_serious,location,describe,chk_physician,chk_family,chk_document,doctor_name,dotor_type,datetime_assessment,datetime_assessment_ts,describe_assessment,xray_detail,lab_detail,other_detail,chk_xray,chk_lab,chk_other,xray_result,lab_result,other_result,recommend_detail,describe_action,severe_id,severe_other_id,severe_other_remark,initial_action,action_result_id,action_result_remark)"
                sql &= "SELECT " & cfbPk & " ,ir_sub_id , 0 , '" & Session("dept_name").ToString & "' , '" & Session("dept_id").ToString & "' , " & Session("dept_id").ToString & " ,report_tel,service_type,pt_title,pt_name,age,month_day_num,month_day_text,sex,hn,room,customer_segment,pt_type,pt_type_remark,clinical_type,diagnosis,operation,date_operation,date_operation_ts,physician,datetime_report,datetime_report_ts,flag_serious,location,describe,chk_physician,chk_family,chk_document,doctor_name,dotor_type,datetime_assessment,datetime_assessment_ts,describe_assessment,xray_detail,lab_detail,other_detail,chk_xray,chk_lab,chk_other,xray_result,lab_result,other_result,recommend_detail,describe_action,severe_id,severe_other_id,severe_other_remark,initial_action,action_result_id,action_result_remark FROM ir_detail_tab WHERE ir_id = " & irId

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If

                ' ======================= UPDATE IR Relate Dept =========================

                sql = "SELECT * FROM ir_relate_dept WHERE ISNULL(is_dept_delete,0) = 0 AND ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "tDept")

                For i As Integer = 0 To ds.Tables("tDept").Rows.Count - 1

                    'sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM cfb_comment_list WHERE "
                    'sql &= " ir_id = " & irId

                    '  ds = conn.getDataSetForTransaction(sql, "t1")
                    ' new_order_sort = ds.Tables(0).Rows(0)(0).ToString

                    pk1 = getPK("relate_dept_id", "ir_relate_dept", conn)
                    sql = "INSERT INTO ir_relate_dept (relate_dept_id , ir_id , dept_name , dept_id , session_id , create_by , create_date , create_date_ts) VALUES("
                    sql &= "'" & pk1 & "',"
                    sql &= "'" & cfbPk & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_name").ToString & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_id").ToString & "',"
                    sql &= "'" & Session.SessionID & "' ,"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("create_by").ToString & "' ,"
                    sql &= " GETDATE() ,"
                    sql &= "'" & Date.Now.Ticks & "' "
                    sql &= ")"


                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If

               
                Next i

               
                'isHasRow("cfb_detail_tab")
                ' updateCFBDetail(e.CommandArgument.ToString)
                'updateAttachFile() ' รายการไฟล์ attach

                ' ======================= UPDATE CFB File Attachment =========================
                Dim filename As String()
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filePK As String = ""
                sql = "SELECT * FROM ir_attachment WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "tFile")

                For iFile As Integer = 0 To ds.Tables("tFile").Rows.Count - 1

                    filePK = getPK("file_id", "ir_attachment", conn)

                    filename = ds.Tables("tFile").Rows(iFile)("file_name").ToString.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    sql = "INSERT INTO ir_attachment (file_id  , ir_id ,  file_name , file_path , file_size , session_id) VALUES( "
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



                    File.Copy(Server.MapPath("../share/incident/attach_file/" & ds.Tables("tFile").Rows(iFile)("file_path").ToString), Server.MapPath("../share/incident/attach_file/" & filePK & "." & extension))

                Next iFile

                irId = cfbPk

                If CInt(txtchangeFormCopy.SelectedValue) = 1 And CInt(formId) = 1 Then
                    isHasRow("ir_fall_tab")
                    updateFall()
                ElseIf CInt(txtchangeFormCopy.SelectedValue) = 2 And CInt(formId) = 2 Then
                    isHasRow("ir_med_tab")
                    updateMed()
                    updateMedPeriod()
                    updateMedDrugWrongName()
                ElseIf CInt(txtchangeFormCopy.SelectedValue) = 3 And CInt(formId) = 3 Then
                    isHasRow("ir_phlebitis_tab")
                    updatePhelbitis()
                ElseIf CInt(txtchangeFormCopy.SelectedValue) = 4 And CInt(formId) = 4 Then
                    isHasRow("ir_anes_tab")
                    updateAnes()
                ElseIf CInt(txtchangeFormCopy.SelectedValue) = 5 And CInt(formId) = 5 Then
                    'updateOther("ir_phlebitis_tab")
                ElseIf CInt(txtchangeFormCopy.SelectedValue) = 6 And CInt(formId) = 6 Then
                    isHasRow("ir_pressure_tab")
                    updatePressure()
                ElseIf CInt(txtchangeFormCopy.SelectedValue) = 7 And CInt(formId) = 7 Then
                    isHasRow("ir_delete_tab")
                    updateDeleteTab()
                End If

                conn.setDBCommit()



            Catch ex As Exception
                conn.setDBRollback()
                Response.Write("onSave : " & ex.Message & sql)
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
                    sql &= " WHERE ir_id = " & irId
                Else
                    sql = "UPDATE ir_trans_list SET is_lock = 0 , lock_by = '" & addslashes(Session("user_fullname")) & "' "
                    sql &= " , lock_date_raw = null , lock_empcode =  " & Session("emp_code").ToString
                    sql &= " WHERE ir_id = " & irId
                End If

                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()

                bindForm()
                ' lblLockMsg.Text = "Locked by " & addslashes(Session("user_fullname")).ToString

            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
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
                If txtrelateid.Text = "" Then
                    Return
                End If

                txtcause_dept = CType(GridDeptCause.FooterRow.FindControl("txtcause_dept"), DropDownList)
                txttqm_addremark_dept = CType(GridDeptCause.FooterRow.FindControl("txttqm_addremark_dept"), TextBox)

                sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ir_dept_cause_list WHERE relate_dept_id = " & txtrelateid.Text
                ds = conn.getDataSetForTransaction(sql, "t0")
                new_order_sort = ds.Tables("t0").Rows(0)(0).ToString

                pk = getPK("dept_cause_id", "ir_dept_cause_list", conn)

                sql = "INSERT INTO ir_dept_cause_list (dept_cause_id , relate_dept_id , ir_id , cause_id , cause_name , cause_remark , ir_type , order_sort) VALUES("
                sql &= "" & pk & " ,"
                sql &= "'" & txtrelateid.Text & "' ,"
                sql &= "" & irId & " ,"
                sql &= "'" & txtcause_dept.SelectedValue & "' ,"
                sql &= "'" & txtcause_dept.SelectedItem.Text & "' ,"
                sql &= "'" & addslashes(txttqm_addremark_dept.Text) & "' ,"
                sql &= "'ir' ,"
                sql &= "'" & new_order_sort & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                '### New Code add data to log book

                sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM ir_tqm_cause_list WHERE ir_id = " & irId
                ds = conn.GetDataSetForTransaction(sql, "t0")
                new_order_sort = ds.Tables("t0").Rows(0)(0).ToString

                'sql = "INSERT INTO ir_tqm_cause_list (ir_id , cause_id , cause_name , cause_remark , ir_type  , dept_unit_name , dept_unit_id , order_sort) VALUES("
                sql = "INSERT INTO ir_tqm_cause_list (ir_id , cause_id , cause_name , cause_remark , ir_type  , order_sort) VALUES("
                sql &= "" & irId & " ,"
                sql &= "'" & txtcause_dept.SelectedValue & "' ,"
                sql &= "'" & txtcause_dept.SelectedItem.Text & "' ,"
                sql &= "'" & addslashes(txttqm_addremark_dept.Text) & "' ,"
                sql &= "'ir' ,"
                'sql &= "'" & addslashes(txtadd_cause_defendant.SelectedItem.Text) & "' ,"
                'sql &= "'" & txtadd_cause_defendant.SelectedValue & "' ,"
                sql &= "'" & new_order_sort & "' "
                sql &= ")"

                errorMsg = conn.ExecuteSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                '### 
                conn.SetDBCommit()
                ' txttqm_addremark_dept.Text = ""
                'txtcause_dept.SelectedIndex = 0

                bindDeptGridCause()
                bindTQMGridCause()
            Catch ex As Exception
                WriteLog("Error ::" + ex.Message)
                conn.setDBRollback()
                Response.Write(ex.Message)
                Throw ex
            Finally
                ds.Dispose()
            End Try
        End Sub

        Private Sub WriteLog(inString As String)
            Dim fileName As String = "D:\Logs\BhTraing_Log.txt"
            If (File.Exists(fileName) = False) Then
                File.Open(fileName, FileMode.Append)
            End If

            File.WriteAllText(fileName, "[" + DateTime.Now.ToString() + "]" + inString)

        End Sub
        Protected Sub GridDeptCause_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridDeptCause.RowDeleting
            Dim sql As String
            Dim result As String
            Dim ds As New DataSet
            Try
                sql = "DELETE FROM ir_dept_cause_list WHERE dept_cause_id = " & GridDeptCause.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    'Response.Write(result)
                    Throw New Exception(result)
                End If

                sql = "SELECT * FROM ir_dept_cause_list WHERE relate_dept_id = " & txtrelateid.Text & " ORDER BY order_sort"
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    sql = "UPDATE ir_dept_cause_list SET order_sort = " & i + 1 & " WHERE dept_cause_id = " & ds.Tables("t1").Rows(i)("dept_cause_id").ToString
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

        Protected Sub cmdAddPreventTQM_Click(sender As Object, e As EventArgs) Handles cmdAddPreventTQM.Click
            Dim sql As String
            Dim result As String
            Dim ds As New DataSet
            Dim pk As String = ""
            Try
                sql = "SELECT ISNULL(MAX(ORDER_SORT),0) + 1 FROM ir_tqm_prevent_list WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(Sql, "t1")
                Dim order As String
                order = ds.Tables("t1").Rows(0)(0).ToString

                pk = getPK("prevent_tqm_id", "ir_tqm_prevent_list", conn)

                sql = "INSERT INTO ir_tqm_prevent_list (prevent_tqm_id  , action_detail , resp_person , ir_id , order_sort ,date_start , date_start_ts , date_end , date_end_ts)"
                Sql &= " VALUES("
                sql &= "'" & pk & "' ,"
                sql &= "'" & addslashes(txt_addprevent_tqm.Value) & "' ,"
                sql &= "'" & addslashes(txt_addperson_tqm.Value) & "' ,"

                sql &= "'" & irId & "' ,"
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
                result = conn.executeSQLForTransaction(Sql)

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
                Response.Write(ex.Message & " : " & Sql)
            End Try
        End Sub

        Protected Sub gridTQMActionPlan_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gridTQMActionPlan.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM ir_tqm_prevent_list WHERE prevent_tqm_id = " & gridTQMActionPlan.DataKeys(e.RowIndex).Value & ""
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

                sql = "SELECT * FROM ir_dept_prevent_list WHERE ir_id = " & irId
                sql &= " ORDER BY dept_id "
                ds = conn.getDataSetForTransaction(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                    sql = "SELECT ISNULL(MAX(ORDER_SORT),0) + 1 FROM ir_tqm_prevent_list WHERE ir_id = " & irId
                    ds2 = conn.getDataSetForTransaction(sql, "t1")
                    Dim order As String
                    order = ds2.Tables("t1").Rows(0)(0).ToString

                    pk = getPK("prevent_tqm_id", "ir_tqm_prevent_list", conn)

                    sql = "INSERT INTO ir_tqm_prevent_list (prevent_tqm_id  , action_detail , resp_person , ir_id , order_sort ,date_start , date_start_ts , date_end , date_end_ts , dept_id)"
                    sql &= " VALUES("
                    sql &= "'" & pk & "' ,"
                    sql &= "'" & addslashes(ds.Tables("t1").Rows(i)("action_detail").ToString) & "' ,"
                    sql &= "'" & addslashes(ds.Tables("t1").Rows(i)("resp_person").ToString) & "' ,"

                    sql &= "'" & irId & "' ,"
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

        Protected Sub cmdCopyToCFB_Click(sender As Object, e As EventArgs) Handles cmdCopyToCFB.Click
            Dim sql As String
            Dim errMsg As String
            Dim ds As New DataSet
            Dim ds2 As New DataSet
            Dim cfb_no As String = ""
            Dim new_order_sort As String = ""
            Dim report_by As String = ""
            Dim pk As String = ""
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
                sql &= " 'cfb' ,"
                sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"
                sql &= "'" & Session("emp_code").ToString & "' "
                sql &= ")"

                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg & sql)
                End If
                'Response.Write(sql)
                ' isHasRow("cfb_detail_tab")
                sql = "SELECT * FROM cfb_detail_tab WHERE ir_id = " & new_ir_id
                ' Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                If ds.Tables("t1").Rows.Count <= 0 Then
                    sql = "INSERT INTO cfb_detail_tab (ir_id) VALUES( "
                    sql &= "" & new_ir_id & ""
                    sql &= ")"
                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg & " : " & sql)
                    End If
                End If


                sql = "SELECT * FROM cfb_detail_tab WHERE ir_id = " & new_ir_id
                ds = conn.getDataSetForTransaction(sql, "t1")
                If (ds.Tables("t1").Rows(0)("cfb_no").ToString = "" Or ds.Tables("t1").Rows(0)("cfb_no").ToString = "0") Then
                    cfb_no = getNewCFBNo()
                Else
                    cfb_no = ds.Tables("t1").Rows(0)("cfb_no").ToString
                End If

                If status = 1 Then
                    cfb_no = "0"
                End If

                sql = "SELECT * FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id WHERE a.ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "t1")
                report_by = ds.Tables("t1").Rows(0)("report_by").ToString

                If txtconvertstatus.SelectedIndex = 0 Then ' ถ้าเลือก Current Status
                    sql = "UPDATE ir_trans_list SET date_submit = '" & ds.Tables("t1").Rows(0)("date_submit").ToString & "' "
                    sql &= " ,  date_submit_ts = '" & ds.Tables("t1").Rows(0)("date_submit_ts").ToString & "' "
                    sql &= " ,  report_by = '" & ds.Tables("t1").Rows(0)("report_by").ToString & "' "
                    sql &= " ,  report_emp_code = '" & ds.Tables("t1").Rows(0)("report_emp_code").ToString & "' "
                    sql &= " WHERE ir_id = " & new_ir_id

                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If
                Else ' Draft Status
                    sql = "UPDATE ir_trans_list SET "
                    sql &= " report_by = '" & Session("user_fullname").ToString & "' "
                    sql &= " ,report_emp_code = '" & Session("emp_code").ToString & "' "
                    sql &= " WHERE ir_id = " & new_ir_id

                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If
                End If



                sql = "UPDATE cfb_detail_tab SET hn = '" & txtpthn.Text & "'"

                sql &= " , cfb_no = '" & cfb_no & "'"

                sql &= " , division = '" & txtdivision.Text & "'"
                sql &= " , report_tel = '" & addslashes(txtcontact.Text) & "'"
                sql &= " , report_tel2 = '" & addslashes(lblReport.Text) & "'"
                sql &= " , diagnosis = '" & addslashes(txtdiagnosis.Value) & "'"
                sql &= " , operation = '" & addslashes(txtoperation.Value) & "'"
                sql &= " , location = '" & addslashes(txtlocation.Text) & "'"
                'sql &= " , location = '" & addslashes(txtlocation_combo.Text) & "'"
                sql &= " , room = '" & addslashes(txtroom.Text) & "'"
                sql &= " , datetime_complaint = '" & convertToSQLDatetime(txtdate_report.Value) & "'"
                sql &= " , datetime_complaint_ts = " & ConvertDateStringToTimeStamp(txtdate_report.Value) & ""

                sql &= " , dept_id_report = '" & ds.Tables("t1").Rows(0)("dept_id").ToString & "'"
                'sql &= " , hn = '" & txthn.Value & "'"
                sql &= " , datetime_report = '" & convertToSQLDatetime(txtdate_report.Value, txthour.SelectedValue.PadLeft(2, "0"), txtmin.SelectedValue.PadLeft(2, "0")) & "'"
                sql &= " , datetime_report_ts = " & ConvertDateStringToTimeStamp(txtdate_report.Value, CInt(txthour.SelectedValue), CInt(txtmin.SelectedValue)) & ""
                sql &= " , complain_detail = '" & addslashes(txtptname.Value) & "'"
                sql &= " , service_type = '" & txtservicetype.SelectedValue & "'"
                sql &= " , pt_title = '" & txttitle.SelectedValue & "'"
                sql &= " , age = '" & txtptage.Value & "'"
                sql &= " , sex = '" & txtptsex.SelectedValue & "'"
                sql &= " , customer_segment = '" & txtsegment.SelectedValue & "'"
                'sql &= " , country = '" & txtcountry.Text & "'"
                'sql &= " , cfb_dept_id = '" & txtdept.SelectedValue & "'"
                ' sql &= " , cfb_dept_name = '" & txtdept.SelectedItem.Text & "'"
                'sql &= " , complain_status = '" & txtcomplain_status.SelectedValue & "'"
                'sql &= " , feedback_from = '" & addslashes(txtfeedback_from.SelectedValue) & "'"
                'sql &= " , complain_status_remark = '" & addslashes(txtcomplain_remark.Value) & "'"
                'sql &= " , feedback_from_remark = '" & addslashes(txtfeedback_remark.Value) & "'"

                'sql &= " , part_customer = '" & addslashes(txtpart_customer.Value) & "'"
                'sql &= " , part_hospital = '" & addslashes(txtpart_hospital.Value) & "'"
                'sql &= " , part_employee = '" & addslashes(txtpart_employee.Value) & "'"

                'If txtcom1.Checked = True Then
                '    sql &= " , cfb_is_complain =  1 "
                'ElseIf txtcom2.Checked = True Then
                '    sql &= " , cfb_is_complain =  0 "
                'Else
                '    sql &= " , cfb_is_complain = null"
                'End If

                'sql &= " , cfb_customer_resp = '" & txtcustomer.SelectedValue & "'"
                'sql &= " , cfb_customer_resp_remark = '" & addslashes(txtcus_detail.Value) & "'"

                'If chk_tel.Checked = True Then
                '    sql &= " , cfb_chk_tel = 1 "
                'Else
                '    sql &= " , cfb_chk_tel = 0 "
                'End If

                'If chk_email.Checked = True Then
                '    sql &= " , cfb_chk_email = 1 "
                'Else
                '    sql &= " , cfb_chk_email = 0 "
                'End If

                'If chk_othter.Checked = True Then
                '    sql &= " , cfb_chk_other = 1 "
                'Else
                '    sql &= " , cfb_chk_other = 0 "
                'End If

                'sql &= " , cfb_tel_remark = '" & addslashes(txttel.Value) & "'"
                'sql &= " , cfb_email_remark = '" & addslashes(txtemail.Value) & "'"
                'sql &= " , cfb_other_remark = '" & addslashes(txtother.Value) & "'"

                sql &= " WHERE ir_id = " & new_ir_id
                'Response.Write(status_id)
                ' Response.End()
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg & ":" & sql)
                End If

                sql = "SELECT * FROM ir_relate_dept WHERE isnull(is_dept_delete,0) = 0 AND ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "tDept")

                If ds.Tables("tDept").Rows.Count = 0 Then
                    sql = "INSERT INTO cfb_comment_list (ir_id , comment_type_id , comment_type_name , comment_detail , comment_solution , lastupdate_by , lastupdate_time , order_sort) VALUES("
                    sql &= "'" & new_ir_id & "' ,"
                    sql &= "'3' ,"
                    sql &= "'Complaint' ,"
                    sql &= "'" & addslashes(txtoccurrence.Value) & "' ,"
                    sql &= "'" & addslashes(txtinitial.Value) & "' ,"
                    sql &= "'" & report_by & "' , "
                    sql &= " GETDATE() , "
                    sql &= "'" & 0 & "' "
                    sql &= ")"


                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If
                End If

                For i As Integer = 0 To ds.Tables("tDept").Rows.Count - 1

                    'sql = "SELECT ISNULL(MAX(order_sort),0) + 1 FROM cfb_comment_list WHERE "
                    'sql &= " ir_id = " & irId

                    '  ds = conn.getDataSetForTransaction(sql, "t1")
                    ' new_order_sort = ds.Tables(0).Rows(0)(0).ToString

                    sql = "INSERT INTO cfb_comment_list (ir_id , comment_type_id , comment_type_name , comment_detail , comment_solution , lastupdate_by , lastupdate_time , order_sort) VALUES("
                    sql &= "'" & new_ir_id & "' ,"
                    sql &= "'3' ,"
                    sql &= "'Complaint' ,"
                    sql &= "'" & addslashes(txtoccurrence.Value) & "' ,"
                    sql &= "'" & addslashes(txtinitial.Value) & "' ,"
                    sql &= "'" & report_by & "' , "
                    sql &= " GETDATE() , "
                    sql &= "'" & 0 & "' "
                    sql &= ")"


                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If

                    sql = "SELECT MAX(comment_id) FROM cfb_comment_list"
                    ds2 = conn.getDataSetForTransaction(sql, "tMaxID")

                    pk = getPK("cfb_relate_dept_id", "cbf_relate_dept", conn)
                    sql = "INSERT INTO cbf_relate_dept (cfb_relate_dept_id , comment_id ,  dept_id , dept_name , session_id) VALUES("
                    sql &= "'" & pk & "',"
                    sql &= "'" & ds2.Tables("tMaxID").Rows(0)(0).ToString & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_id").ToString & "',"
                    sql &= "'" & ds.Tables("tDept").Rows(i)("dept_name").ToString & "',"
                    sql &= "'" & Session.SessionID & "' "
                    sql &= ")"

                    errMsg = conn.executeSQLForTransaction(sql)
                    If errMsg <> "" Then
                        Throw New Exception(errMsg)
                    End If

                Next i

                Dim filename As String()
                Dim extension As String
                Dim iCount As Integer = 0

                sql = "SELECT * FROM ir_attachment WHERE ir_id = " & irId
                ds = conn.getDataSetForTransaction(sql, "tFile")

                For iFile As Integer = 0 To ds.Tables("tFile").Rows.Count - 1

                    pk = getPK("file_id", "cfb_attachment", conn)

                    filename = ds.Tables("tFile").Rows(iFile)("file_name").ToString.Split(".")
                    iCount = UBound(filename)
                    extension = filename(iCount)

                    sql = "INSERT INTO cfb_attachment (file_id , ir_id ,  file_name , file_path , file_size , session_id) VALUES( "
                    sql &= "" & pk & " , "

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
                        File.Delete(Server.MapPath("../share/cfb/attach_file/" & pk & "." & extension))
                    Catch ex As Exception

                    End Try

                    File.Copy(Server.MapPath("../share/incident/attach_file/" & ds.Tables("tFile").Rows(iFile)("file_path").ToString), Server.MapPath("../share/cfb/attach_file/" & pk & "." & extension))

                Next iFile

                ' updateOnlyLog("0", "Copy from Incident : " & txtirno.Text)

                sql = "INSERT INTO ir_status_log (status_id , status_name , ir_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
                sql &= "'" & 0 & "' ,"
                sql &= "'" & "" & "' ,"
                sql &= "'" & new_ir_id & "' ,"
                sql &= "GETDATE() ,"
                sql &= "'" & Date.Now.Ticks & "' ,"
                sql &= "'" & addslashes(Session("user_fullname").ToString) & "' ,"
                sql &= "'" & addslashes(Session("user_position").ToString) & "' ,"
                sql &= "'" & addslashes(Session("dept_name").ToString) & "' ,"
                sql &= "'" & "Copy from Incident : " & txtirno.Text & "' "
                sql &= ")"
                'Response.Write(sql)
                errMsg = conn.executeSQLForTransaction(sql)
                If errMsg <> "" Then
                    Throw New Exception(errMsg & ":" & sql)
                End If

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

        Protected Sub txtcomboowner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtcomboowner.SelectedIndexChanged
            txtcase_owner.Text = txtcomboowner.SelectedValue
        End Sub

        Protected Sub cmdTQMView2_Click(sender As Object, e As EventArgs) Handles cmdTQMView2.Click


            Try
                updateTranListLog("3", "")
                updateRevision()
                If txtstatus.SelectedValue = "2" And txtstatus.SelectedValue = "2" Then ' รับเรื่องครั้งแรก
                    updateOnlyLog("3", "")
                Else
                    updateOnlyLog("0", "Save Revision : " & revision_no)
                End If

                conn.setDBCommit()

                saveHTMLForPDF(irId)

                Response.Redirect("form_incident.aspx?mode=edit&irId=" & irId & "&formId=" & formId)
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
                    updateOnlyLog("3", "")
                Else
                    updateOnlyLog("0", "Save Revision : " & revision_no)
                End If

                conn.setDBCommit()

                saveHTMLForPDF(irId)

                Response.Redirect("form_incident.aspx?mode=edit&irId=" & irId & "&formId=" & formId)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
        Protected Sub txtcontact_TextChanged(sender As Object, e As EventArgs) Handles txtcontact.TextChanged

        End Sub

    End Class

End Namespace
