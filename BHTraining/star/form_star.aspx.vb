Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Net.Mail
Imports System.Threading
Imports QueryStringEncryption
Imports System.Net


Partial Class star_form_star
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected session_id As String
    Protected viewtype As String = ""
    Protected flag As String = "" ' สำหรัย committee เพิ่มรายการเอง
    Protected mode As String = ""
    Protected new_star_id As String = ""
    Protected id As String = ""

    Protected num_endorse_com As Integer = 0
    Protected num_not_endorse_com As Integer = 0


    ' Private Const SCRIPT_DOFOCUS As String = "window.setTimeout('DoFocus()', 1);" & vbCr & vbLf & "    function DoFocus()" & vbCr & vbLf & "    {" & vbCr & vbLf & "        try {" & vbCr & vbLf & "            document.getElementById('REQUEST_LASTFOCUS').focus();" & vbCr & vbLf & "        } catch (ex) {}" & vbCr & vbLf & "    }"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        If Not IsPostBack Then
            '  HookOnFocus(TryCast(Me.Page, Control))
        End If

        '  Page.ClientScript.RegisterStartupScript(GetType(MyPage), "ScriptDoFocus", SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS", Request("__LASTFOCUS")), True)
        ' Page.ClientScript.RegisterStartupScript(GetType(star_form_star), "ScriptDoFocus", SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS", Request("__LASTFOCUS")), True)

        session_id = Session.SessionID
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        flag = Request.QueryString("flag")
        '  Response.Write("xxx" & Session("viewtype"))
        If Session("viewtype") Is Nothing Then
            Session("viewtype") = viewtype
        Else
            viewtype = Session("viewtype").ToString
        End If


        If mode = "add" Then
            Session("viewtype") = ""
            viewtype = ""
            'txtdate_report.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            'txtdate_complaint.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        cmdPreview.Attributes.Add("onclick", "window.open('preview_star.aspx?id=" & id & "');return false;")

        If Not Page.IsPostBack Then ' ถ้าเปิดมาครั้งแรก
            bindHour()
            bindMinute()
            bindFileAttach() ' file attach

            bindAllDoctor()
            bindAllDept()
            bindAllPerson()
            bindStatus()

            bindNation()
            bindRecognitionCombo()
            'bindRelateDept()

            If mode = "edit" Then
                cmdPreview.Visible = True
                If viewtype = "hr" Then
                    lblHeader.Text = " (Coordinator)"
                ElseIf viewtype = "sup" Then
                    lblHeader.Text = " (Chair of Star Committee)"
                ElseIf viewtype = "com" Then
                    lblHeader.Text = " (Star Committee)"
                ElseIf viewtype = "nominee" Then
                    lblHeader.Text = " (My Star Record)"
                End If

                tab_update.Visible = True ' Information Update

                bindGridStarLog() ' History log
                bindDetail()
                bindSelectDept()
                bindSelectPerson()
                bindSelectDoctor()

                bindFileAttach()
                bindGridInformationUpdate()

                txtjobtitle.Value = Session("job_title").ToString
                lblJobType.Text = Session("user_position").ToString
                txtname.Value = Session("user_fullname").ToString
                txtdeptname.Value = Session("dept_name").ToString
                txtdatetime.Value = Date.Now

                If CInt(txtstatus.SelectedValue) > 1 Then

                    If viewtype = "" Then
                        readonlyControl(panelDetail)
                    End If

                    cmdDraft1.Visible = False
                    cmdDraft2.Visible = False

                    cmdSubmit.Visible = False
                    cmdSubmit2.Visible = False

                    cmdCopy1.Visible = False
                    cmdCopy2.Visible = False
                Else
                    cmdCopy1.Visible = True
                    cmdCopy2.Visible = True
                End If

                If viewtype = "hr" Or viewtype = "sup" Then ' Star Coordinator
                    '  isHasRow("star_hr_tab")
                    If txtstatus.SelectedValue = "7" Then
                        chkSendMailAward.Visible = True
                    End If


                    bindGridAlertLog()
                    bindNoteCommbo()
                    bindAdmireCombo()
                    bindTopicMain()


                    bindHrTab()
                    bindGridAdmireHR()
                    bindGridDetailHR()
                    bindGridCoASTHR()
                    ' 
                    ' bindAdmireCombo()
                    bindCommitteeList() ' Committee comment
                    bindCommentList() ' Manager comment

                    txtstatus.Enabled = True
                    cmdHRReview1.Visible = True
                    tab_coordinator.Visible = True
                    tab_commitee.Visible = True
                    panel_add_committee.Visible = True
                    tab_manager.Visible = True
                    tabSendMail.Visible = True
                    tabMailLog.Visible = True
                End If

                If viewtype = "com" Then
                    bindNoteCommbo()
                    bindAdmireCombo()
                    bindCommitteeList() ' Committee comment
                    bindCommentList() ' Manager comment

                    bindTopicMain()
                    bindHrTab()

                    bindCommitteeList() ' Committee comment
                    bindCommentList() ' Manager comment

                    tab_coordinator.Visible = True
                    tab_commitee.Visible = True
                    tab_manager.Visible = True

                    If txtstatus.SelectedValue = "1" Then
                        tab_coordinator.Visible = False
                        tab_manager.Visible = False
                    End If
                    readonlyControl(panelDetail)
                    readonlyControl(panel_hr)
                    '  panel_hr.Enabled = False
                    panel_add_committee.Visible = True
                End If

                If viewtype = "sup" Then
                    'bindNoteCommbo()
                    'bindAdmireCombo()
                    'bindHrTab()
                    'bindCommitteeList() ' Committee comment
                    'bindCommentList() ' Manager comment
                    tab_coordinator.Visible = True
                    tab_commitee.Visible = True
                    tab_manager.Visible = True
                    ' readonlyControl(panelDetail)
                    'readonlyControl(panel_hr)
                    ' panel_hr.Enabled = False
                    panelAddComment.Visible = True
                End If

                If viewtype = "update" Then
                    bindGridAlertLog()
                    bindAdmireCombo()
                    bindHrTab()

                    bindCommitteeList() ' Committee comment
                    bindCommentList() ' Manager comment

                    txtstatus.Enabled = False
                    cmdHRReview1.Visible = False
                    tab_coordinator.Visible = True
                    tab_commitee.Visible = True
                    tab_manager.Visible = True
                    readonlyControl(panelDetail)
                    readonlyControl(panel_hr)

                    cmdDeleteFile.Visible = False
                    FileUpload0.Visible = False
                    cmdUpload.Visible = False
                End If

                If viewtype = "nominee" Or viewtype = "readonly" Then
                    readonlyControl(panelDetail)
                    cmdDeleteFile.Visible = False
                    FileUpload0.Visible = False
                    cmdUpload.Visible = False
                End If

            Else
                txtnominee_type.SelectedValue = 1
                cmdSubmit.Enabled = False
                cmdSubmit2.Enabled = False
            End If

        Else
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

    Private Sub HookOnFocus(CurrentControl As Control)
        'checks if control is one of TextBox, DropDownList, ListBox or Button

        If (TypeOf CurrentControl Is TextBox) OrElse (TypeOf CurrentControl Is DropDownList) OrElse (TypeOf CurrentControl Is ListBox) OrElse (TypeOf CurrentControl Is Button) Then
            'adds a script which saves active control on receiving focus 

            'in the hidden field __LASTFOCUS.

            TryCast(CurrentControl, WebControl).Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id} catch(e)}")

            If CurrentControl.HasControls() Then
                'if yes do them all recursively

                For Each CurrentChildControl As Control In CurrentControl.Controls
                    HookOnFocus(CurrentChildControl)
                Next

            End If
        End If
    End Sub

    Private Sub bindGridAlertLog()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * FROM star_alert_log a  WHERE a.star_id = " & id)
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

    Sub bindRecognitionCombo()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM star_m_recognition  "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtstar_recog.DataSource = ds
            txtstar_recog.DataBind()

            txtstar_recog.Items.Insert(0, New ListItem("-- Please Select --", ""))


            txthr_type.DataSource = ds
            txthr_type.DataBind()

            txthr_type.Items.Insert(0, New ListItem("-- Please Select --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindNation()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM dbo.m_national ORDER BY national_name "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtcountry.DataSource = ds
            txtcountry.DataBind()

            txtcountry.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTopicMain()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM star_m_topic_main ORDER BY main_topic_name_th "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txttopic_main.DataSource = ds
            txttopic_main.DataBind()

            txttopic_main.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTopicSub()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM star_m_topic_detail WHERE 1 = 1 "
            If txttopic_main.SelectedValue <> "" Then
                sql &= " AND main_topic_id = " & txttopic_main.SelectedValue
            Else
                sql &= " AND 1 > 2 "
            End If
            sql &= " ORDER BY subtopic_name_th "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txttopic_sub.DataSource = ds
            txttopic_sub.DataBind()

            txttopic_sub.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindNoteCommbo()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM star_m_note ORDER BY note_th "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtadd_detail_combo.DataSource = ds
            txtadd_detail_combo.DataBind()

            txtadd_detail_combo.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))

            txthr_detail_combo.DataSource = ds
            txthr_detail_combo.DataBind()

            txthr_detail_combo.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Private Sub bindAdmireCombo()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * FROM star_m_admire a  ")
            sqlB.Append(" ORDER BY admire_topic ASC")
            ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

            '  Response.Write(sqlB.ToString)
            txtevent_admire.DataSource = ds
            txtevent_admire.DataBind()

            txtevent_admire.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))

            txtsentence_admire.DataSource = ds
            txtsentence_admire.DataBind()

            txtsentence_admire.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))

            ' Committee Tab
            txtadd_event_admire.DataSource = ds
            txtadd_event_admire.DataBind()

            txtadd_event_admire.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))

            txtadd_sentence_admire.DataSource = ds
            txtadd_sentence_admire.DataBind()

            txtadd_sentence_admire.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))

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
            sql = "SELECT * FROM star_status_list"
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

    Private Sub bindHour()

        Dim i As Integer = 0
        Dim i_str As String = ""
        For i = 0 To 23
            i_str = i.ToString
            txthour.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
            ' txthour2.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
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
            ' txtmin2.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
        Next

        ' txtmin.Items.Insert(0, New ListItem("mm", "0"))
        ' txtmin2.Items.Insert(0, New ListItem("mm", "0"))
        'txtmin2.Items.Insert(0, New ListItem("-", "0"))
        'txtmin2.SelectedIndex = 0


    End Sub

    Sub isHasRow(ByVal table As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""

        sql = "SELECT * FROM " & table & " WHERE star_id = " & id
        'Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage)
        End If

        If ds.Tables("t1").Rows.Count <= 0 Then
            sql = "INSERT INTO " & table & " (star_id) VALUES( "
            sql &= "" & id & ""
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If
            '   Response.Write(sql)
        End If

        ds.Dispose()

    End Sub

    Sub loopMail(ByVal star_no As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim msgBody As String = ""
        Dim key As String = ""
        Dim name() As String
        Dim name_Str As String = ""

        Try
            Dim host As String = ConfigurationManager.AppSettings("frontHost").ToString
            key = UserActivation.GetActivationLink("star/form_star.aspx?mode=edit&id=" & id)
          
            '  Response.Write(msgBody)
            ' ส่งเมล์ให้เฉพาะ Mgr
            sql = "SELECT * FROM user_profile a INNER JOIN user_access_costcenter_idp b ON a.emp_code = b.emp_code WHERE "
            sql &= " ( b.costcenter_id IN (SELECT costcenter_id FROM star_relate_dept WHERE star_id = " & id & " ) OR "
            sql &= "  b.costcenter_id IN (SELECT y.dept_id FROM star_relate_person x INNER JOIN user_profile y ON x.emp_code = y.emp_code WHERE x.star_id = " & id & " ) )"
            sql &= " AND ISNULL(a.custom_user_email,'') <> ''  "
            sql &= "  AND a.emp_code in (select emp_code from user_role where role_id IN (13,14,15,16) )"

            sql &= " AND a.emp_code NOT IN (60616 , 52829 , 108274 , 118208) "
            'Response.Write(sql)
            'Response.End()
            ds = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                msgBody = "  Dear Khun " & ds.Tables("t1").Rows(i)("user_fullname").ToString & "<br/><br/>" & vbCrLf
                'Response.Write(11111)
                If txtperson_select.Items.Count > 0 Then

                    txtperson_select.SelectedIndex = 0

                    Try

                        name = txtperson_select.SelectedItem.ToString.Split(",")
                        name_Str = name(0)
                    Catch ex As Exception
                        name_Str = txtperson_select.SelectedItem.ToString
                    End Try
                    msgBody &= "We are delighted to inform you that Khun " & name_Str
                End If

                If txtdept_select.Items.Count > 0 Then
                    txtdept_select.SelectedIndex = 0
                    msgBody &= "We are delighted to inform you that " & txtdept_select.SelectedItem.ToString
                End If

                If txtdoctor_select.Items.Count > 0 Then
                    txtdoctor_select.SelectedIndex = 0
                    msgBody &= "We are delighted to inform you that " & txtdoctor_select.SelectedItem.ToString
                End If

                msgBody &= " has been awarded with " & txtsrp.SelectedValue & " points for  his/her recognition for "
                msgBody &= " the Star of Bumrungrad for the month of " & MonthName(Date.Now.Month) & " " & Date.Now.Year & ".We are absolutely sure that you are very proud of "
                If txtperson_select.Items.Count > 0 Then
                    txtperson_select.SelectedIndex = 0
                    Try
                        name = txtperson_select.SelectedItem.ToString.Split(",")
                        name_Str = name(0)
                    Catch ex As Exception
                        name_Str = txtperson_select.SelectedItem.ToString
                    End Try
                    msgBody &= "Khun " & name_Str
                End If

                If txtdept_select.Items.Count > 0 Then
                    txtdept_select.SelectedIndex = 0
                    msgBody &= "" & txtdept_select.SelectedItem.Text
                End If

                If txtdoctor_select.Items.Count > 0 Then
                    txtdoctor_select.SelectedIndex = 0
                    msgBody &= "" & txtdoctor_select.SelectedItem.Text
                End If
                msgBody &= " for his/her contribution to the hospital’s mission to provide excellent service at all times." & vbCrLf
              

                msgBody &= "<br/><br/>Thank you,<br/>"
                msgBody &= "With Best Regards,<br/><br/>"
                msgBody &= "Please kindly open a link below<br/>" & vbCrLf
                msgBody &= vbCrLf & "<br/><a href='http://" & host & "/login.aspx?viewtype=update&key=" & key & "'>Star of Bumrungrad Online</a> "
                msgBody &= "<br/><br/>Best Regards,<br/><br/>"

                'Response.Write(msgBody)
                sendMail(ds.Tables("t1").Rows(i)("custom_user_email").ToString, "#" & star_no & " : Please review star of bumrungrad", msgBody)

                insertLoopMailLog("Email", "#" & star_no & " : Please review star of bumrungrad", ds.Tables("t1").Rows(i)("custom_user_email").ToString, "")
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
            oMsg.From = New MailAddress("starbi@bumrungrad.com")
            oMsg.Headers.Add("Disposition-Notification-To", "<Star of Bumrungrad>")
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

              

                If e.CommandArgument.ToString = "11" Then ' Copy as draft
                    copyDraft()
                    ' updateSSIPNo()
                Else

                    updateTransList(e.CommandArgument.ToString)

                    If e.CommandArgument.ToString <> "1" Then
                        updateOnlyLog(e.CommandArgument)
                    End If

                End If

            

            Else ' HR Save review = 99
                '  Response.Write(1)
                isHasRow("star_hr_tab")
                '  Response.Write(2)
                updateHRTab()
                '  Response.Write(3)
                updateTransList(txtstatus.SelectedValue) ' update status
                'updateOnlyLog("0", "Coornidator Update")
                updateOnlyLog(txtstatus.SelectedValue)

                If txtstatus.SelectedValue = "7" Then

                    'Response.Write(11111)
                    If chkSendMailAward.Checked Then
                        ' Response.Write(22222)
                        loopMail(lblStarNo.Text) ' ถ้า status เป็น award ส่งเมล์ให้ mrg ที่เกี่ยวข้อง
                        bindGridAlertLog()
                    End If

                End If
            End If



            isHasRow("star_detail_tab")
            updateAttachFile()
            updateDetail(e.CommandArgument.ToString)
            '  Response.Write(1)
            updateRelateDept()
            ' Response.Write(2)
            updateRelatePerson()
            ' Response.Write(3)
            updateRelateDoctor()

            'Response.Write("yyy")

            conn.setDBCommit()

            If e.CommandArgument.ToString = "2" Then ' Submit
                ' loopMail(global_ssip_no)
            End If



            If e.CommandArgument.ToString = "2" Then ' Submit
                Response.Redirect("home.aspx")
            ElseIf e.CommandArgument.ToString = "99" And txtstatus.SelectedValue = "7" Then ' Return to staff
                bindGridStarLog()
                ' Response.Redirect("home.aspx?viewtype=hr")
                Response.Redirect("form_star.aspx?mode=edit&id=" & id)
            ElseIf e.CommandArgument.ToString = "11" Then ' copy draft
                Response.Redirect("home.aspx")
            Else
                If flag = "com" Then
                    Session("viewtype") = "com"
                    'Response.Write("1111111")
                    Response.Redirect("form_star.aspx?mode=edit&id=" & id & "&viewtype=com")
                Else
                    Response.Redirect("form_star.aspx?mode=edit&id=" & id)
                End If

            End If


        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("onsave : " & ex.Message)
        End Try
    End Sub

    Sub updateTransList(ByVal status_id As String, Optional ByVal review_status_id As Integer = 0)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim ds2 As New DataSet

        If mode = "add" Then
            Try
                sql = "SELECT ISNULL(MAX(star_id),0) + 1 AS pk FROM star_trans_list"
                ds = conn.getDataSetForTransaction(sql, "t1")
                pk = ds.Tables("t1").Rows(0)(0).ToString
                new_star_id = pk
            Catch ex As Exception
                Response.Write(ex.Message & sql)
                Response.Write(sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try


            sql = "INSERT INTO star_trans_list (star_id , submit_date , submit_date_ts , status_id ,  report_by , report_emp_code , report_dept_id , report_dept_name ,  report_costcenter_id)"
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
        Else ' Edit mode , update status only

            sql = "UPDATE star_trans_list SET status_id = " & status_id
            '  sql &= " , review_status_id = " & review_status_id0
            'sql &= " , ssip_no = '" & new_ssip_no & "'"

            sql &= " WHERE star_id = " & id

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If
    End Sub

    Sub updateOnlyLog(ByVal status_id As String, Optional ByVal log_remark As String = "")
        Dim sql As String
        Dim errorMsg As String

        sql = "INSERT INTO star_status_log (status_id , status_name , star_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
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

    Sub copyDraft()
        Dim sql As String
        Dim errorMsg As String = ""
        Dim ds As New DataSet
        Dim pk As String = ""
        Dim pkFile As String = ""

        Try
            sql = "SELECT ISNULL(MAX(star_id),0) + 1 AS pk FROM star_trans_list"
            ds = conn.getDataSetForTransaction(sql, "t1")
            pk = ds.Tables("t1").Rows(0)(0).ToString
            '  new_star_id = pk
        Catch ex As Exception
            Response.Write(ex.Message & sql)
            Response.Write(sql)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try


        sql = "INSERT INTO star_trans_list (star_id , submit_date , submit_date_ts , status_id ,  report_by , report_emp_code , report_dept_id , report_dept_name ,  report_costcenter_id)"
        sql &= " VALUES("
        sql &= "" & pk & " ,"


        sql &= " GETDATE() ,"
        sql &= "" & Date.Now.Ticks & " ,"
        sql &= "" & 1 & " ,"
        sql &= "'" & Session("user_fullname").ToString & "' ,"
        sql &= "'" & Session("emp_code").ToString & "' ,"
        sql &= "'" & Session("dept_id").ToString & "' ,"
        sql &= "'" & Trim(Session("dept_name").ToString) & "' ,"
        sql &= "'" & Session("costcenter_id").ToString & "' "
        sql &= ")"
        ' Response.Write(sql)
        '  Response.End()
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If

        Dim strFileName = ""
        Dim filename() As String
        Dim extension As String
        Dim iCount As Integer = 0

        sql = "SELECT * FROM star_attachment WHERE star_id = " & id
        ds = conn.getDataSetForTransaction(sql, "tFile")
        For i As Integer = 0 To ds.Tables("tFile").Rows.Count - 1

            strFileName = ds.Tables("tFile").Rows(i)("file_path").ToString
            filename = strFileName.Split(".")
            iCount = UBound(filename)
            extension = filename(iCount)

            sql = "SELECT ISNULL(MAX(file_id),0) + 1 AS pk FROM star_attachment"
            ds = conn.getDataSetForTransaction(sql, "tPKFile")
            pkFile = ds.Tables("tPKFile").Rows(0)(0).ToString

            sql = "INSERT INTO star_attachment (file_id , star_id , file_name , file_path , file_size)"
            sql &= "SELECT " & pkFile & " , " & pk & " , file_name , '" & pkFile & "." & extension & "' , file_size FROM star_attachment WHERE star_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            File.Copy(Server.MapPath("../share/star/attach_file/" & strFileName), Server.MapPath("../share/star/attach_file/" & pkFile & "." & extension))

        Next i

        id = pk
        isHasRow("star_detail_tab")
        updateDetail("1")

    End Sub

    Sub updateAttachFile()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet

        If mode = "add" Then


            sql = "UPDATE star_attachment SET star_id = " & id & " , session_id = '' WHERE session_id = '" & Session("session_myid").ToString & "'"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If

        Else
            sql = "UPDATE star_attachment SET  session_id = '' WHERE star_id = '" & id & "'"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & "" & sql)
            End If
        End If
    End Sub

    Private Sub bindGridStarLog()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * , ISNULL(b.status_name, a.log_remark) AS star_status_name FROM star_status_log a LEFT OUTER JOIN star_status_list b ON a.status_id = b.status_id WHERE a.star_id = " & id)
            sqlB.Append(" ORDER BY log_time ASC")
            ds = conn.getDataSetForTransaction(sqlB.ToString, "table1")

            lblLogNum.Text = " (" & ds.Tables("table1").Rows.Count & ")"
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

    Function getNewCFBNo() As String
        Dim sql As String
        Dim dsIR As New DataSet

        Dim new_star_no As String = 0
        Dim run_no As Long

        Try
            Dim yyyymmdd As String = CStr(Date.Now.Year) & Date.Now.Month.ToString.PadLeft(2, "0") ' & Date.Now.Day.ToString.PadLeft(2, "0")
            sql = "SELECT star_no , RIGHT(star_no,4) AS running FROM star_detail_tab WHERE star_no LIKE '" & yyyymmdd & "%' ORDER BY star_no DESC"
            dsIR = conn.getDataSetForTransaction(sql, "t1")
            If dsIR.Tables(0).Rows.Count > 0 Then
                run_no = CLng(Trim(dsIR.Tables(0).Rows(0)("running").ToString)) + 1
                new_star_no = CLng(yyyymmdd & Date.Now.Day.ToString.PadLeft(2, "0")) & run_no.ToString.PadLeft(4, "0")
            Else
                new_star_no = yyyymmdd & Date.Now.Day.ToString.PadLeft(2, "0") & "0001"
            End If

            'Dim d As New Date(new_ir_no
        Catch ex As Exception
            Response.Write(ex.Message)
            new_star_no = ""
        Finally
            dsIR.Dispose()

        End Try

        Return new_star_no
    End Function

    Sub bindDetail()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_detail_tab a INNER JOIN star_trans_list b ON a.star_id = b.star_id WHERE a.star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtstatus.SelectedValue = ds.Tables(0).Rows(0)("status_id").ToString
            '  Response.Write(ds.Tables(0).Rows(0)("status_id").ToString)

            lblStarNo.Text = ds.Tables(0).Rows(0)("star_no").ToString
            ' txtcfbid.Text = ds.Tables(0).Rows(0)("cfb_no").ToString
            txthn.Text = ds.Tables(0).Rows(0)("hn").ToString

            txtlocation.Text = ds.Tables(0).Rows(0)("location").ToString
            txtroom.Text = ds.Tables(0).Rows(0)("room").ToString
            txtdate_complaint.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("datetime_complaint_ts").ToString)

            txtptage.Value = ds.Tables(0).Rows(0)("age").ToString
            txtptsex.SelectedValue = ds.Tables(0).Rows(0)("sex").ToString
            txttitle_new.SelectedValue = ds.Tables(0).Rows(0)("pt_title").ToString
            txtsegment.SelectedValue = ds.Tables(0).Rows(0)("customer_segment").ToString
            txtservicetype.SelectedValue = ds.Tables(0).Rows(0)("service_type").ToString

            txtdate_report.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("datetime_report_ts").ToString)
            txthour.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString, "hour")
            txtmin.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString, "min")
            txtptname.Value = ds.Tables(0).Rows(0)("complain_detail").ToString
            txtcountry.SelectedValue = ds.Tables(0).Rows(0)("country").ToString
            ' txtdept.SelectedValue = ds.Tables(0).Rows(0)("cfb_dept_id").ToString
            txtcomplain_status.SelectedValue = ds.Tables(0).Rows(0)("complain_status").ToString
            txtfeedback_from.SelectedValue = ds.Tables(0).Rows(0)("feedback_from").ToString
            txtcomplain_remark.Value = ds.Tables(0).Rows(0)("complain_status_remark").ToString
            txtfeedback_remark.Value = ds.Tables(0).Rows(0)("feedback_from_remark").ToString
     

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

            txtstar_detail.Text = ds.Tables(0).Rows(0)("service_detail").ToString
            lblCommitteeDetail.Text = ds.Tables(0).Rows(0)("service_detail").ToString
            lblHRDetail.Text = ds.Tables(0).Rows(0)("service_detail").ToString

            txtCFBNo.Text = ds.Tables(0).Rows(0)("cfbno_relate").ToString

            txtnominee_type.SelectedValue = ds.Tables(0).Rows(0)("nominee_type_id").ToString
            txtcustom_name.Text = ds.Tables(0).Rows(0)("custom_nominee").ToString


            If txtcomplain_status.SelectedValue = "1" Then
                div_hn_remark.Visible = True
                div_profile.Visible = True
            Else
                div_hn_remark.Visible = True
                div_profile.Visible = False
            End If

            If txtfeedback_from.SelectedValue = "6" Then ' Other / อื่นๆ
                div_source_other.Visible = True
                div_cfb.Visible = False
            ElseIf txtfeedback_from.SelectedValue = "2" Then ' form CFB
                div_source_other.Visible = False
                div_cfb.Visible = True
            Else
                div_source_other.Visible = False
                div_cfb.Visible = False
            End If

            If txtcustomer.SelectedValue = "3" Or txtcustomer.SelectedValue = "4" Then
                div_response_remark.Visible = True
            Else
                div_response_remark.Visible = False
            End If

            If txtcustomer.SelectedValue = "2" Or txtcustomer.SelectedValue = "4" Then ' ติดกลับกลับโดยวิธี
                div_response_method.Visible = True
                lblCFBcomment19.Visible = True
            Else
                div_response_method.Visible = False
                lblCFBcomment19.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & ":" & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub updateDetail(ByVal status_id As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim new_star_no As String = ""

        If status_id = "2" Then
            new_star_no = getNewCFBNo()
            If new_star_no = "" Then
                Throw New Exception("Star NO. Error ")
            End If
        End If

        sql = "UPDATE star_detail_tab SET hn = '" & txthn.Text & "'"
        If status_id = "2" Then
            sql &= " , star_no = '" & new_star_no & "'"
        End If


        sql &= " , location = '" & addslashes(txtlocation.Text) & "'"
        sql &= " , room = '" & addslashes(txtroom.Text) & "'"
        sql &= " , datetime_complaint = '" & convertToSQLDatetime(txtdate_complaint.Text) & "'"
        sql &= " , datetime_complaint_ts = " & ConvertDateStringToTimeStamp(txtdate_complaint.Text) & ""

      
        '  sql &= " , hn = '" & txthn.Value & "'"
        sql &= " , datetime_report = '" & convertToSQLDatetime(txtdate_report.Text, txthour.SelectedValue.PadLeft(2, "0"), txtmin.SelectedValue.PadLeft(2, "0")) & "'"
        sql &= " , datetime_report_ts = " & ConvertDateStringToTimeStamp(txtdate_report.Text, CInt(txthour.SelectedValue), CInt(txtmin.SelectedValue)) & ""
        'Response.Write("ttt")
        sql &= " , complain_detail = '" & addslashes(txtptname.Value) & "'"
        sql &= " , service_type = '" & txtservicetype.SelectedValue & "'"
        sql &= " , pt_title = '" & txttitle_new.SelectedValue & "'"

        sql &= " , age = '" & txtptage.Value & "'"
        sql &= " , sex = '" & txtptsex.SelectedValue & "'"
        sql &= " , customer_segment = '" & txtsegment.SelectedValue & "'"
        sql &= " , country = '" & txtcountry.SelectedValue & "'"
        'sql &= " , cfb_dept_id = '" & txtdept.SelectedValue & "'"
        ' sql &= " , cfb_dept_name = '" & txtdept.SelectedItem.Text & "'"
        sql &= " , complain_status = '" & txtcomplain_status.SelectedValue & "'"
        sql &= " , feedback_from = '" & addslashes(txtfeedback_from.SelectedValue) & "'"
        sql &= " , complain_status_remark = '" & addslashes(txtcomplain_remark.Value) & "'"
        sql &= " , feedback_from_remark = '" & addslashes(txtfeedback_remark.Value) & "'"


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

        sql &= " , service_detail = '" & addslashes(txtstar_detail.Text) & "'"
        sql &= " , cfbno_relate = '" & addslashes(txtCFBNo.Text) & "'"
        sql &= " , nominee_type_id = '" & txtnominee_type.SelectedValue & "'"
        sql &= " , nominee_type_name = '" & txtnominee_type.SelectedItem.Text & "'"
        sql &= " , custom_nominee = '" & addslashes(txtcustom_name.Text) & "'"
        sql &= " WHERE star_id = " & id
        ' Response.Write(sql)

        'Response.End()
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If
    End Sub

    Sub bindHrTab()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_hr_tab WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count > 0 Then
                txthr_recog_type.SelectedValue = ds.Tables("t1").Rows(0)("team_id").ToString
                txtevent_admire.SelectedValue = ds.Tables("t1").Rows(0)("event_id").ToString
                txtsentence_admire.SelectedValue = ds.Tables("t1").Rows(0)("sentence_id").ToString
                txthr_detail_combo.SelectedValue = ds.Tables("t1").Rows(0)("detail_id").ToString
                If ds.Tables("t1").Rows(0)("chk_clear").ToString = "1" Then
                    chk_communicate.Checked = True
                Else
                    chk_communicate.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_care").ToString = "1" Then
                    chk_relative.Checked = True
                Else
                    chk_relative.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_smart").ToString = "1" Then
                    chk_talent.Checked = True
                Else
                    chk_talent.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_quality").ToString = "1" Then
                    chk_quality.Checked = True
                Else
                    chk_quality.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi1").ToString = "1" Then
                    chkbi1.Checked = True
                Else
                    chkbi1.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi2").ToString = "1" Then
                    chkbi2.Checked = True
                Else
                    chkbi2.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi3").ToString = "1" Then
                    chkbi3.Checked = True
                Else
                    chkbi3.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi4").ToString = "1" Then
                    chkbi4.Checked = True
                Else
                    chkbi4.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi5").ToString = "1" Then
                    chkbi5.Checked = True
                Else
                    chkbi5.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi6").ToString = "1" Then
                    chkbi6.Checked = True
                Else
                    chkbi6.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi7").ToString = "1" Then
                    chkbi7.Checked = True
                Else
                    chkbi7.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi8").ToString = "1" Then
                    chkbi8.Checked = True
                Else
                    chkbi8.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi9").ToString = "1" Then
                    chkbi9.Checked = True
                Else
                    chkbi9.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_newbi10").ToString = "1" Then
                    chkbi10.Checked = True
                Else
                    chkbi10.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_2015_1").ToString = "1" Then
                    chk2015_1.Checked = True
                Else
                    chk2015_1.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_2015_2").ToString = "1" Then
                    chk2015_2.Checked = True
                Else
                    chk2015_2.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_2015_3").ToString = "1" Then
                    chk2015_3.Checked = True
                Else
                    chk2015_3.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_2015_4").ToString = "1" Then
                    chk2015_4.Checked = True
                Else
                    chk2015_4.Checked = False
                End If


                txthr_recog.SelectedValue = ds.Tables("t1").Rows(0)("recognition_id").ToString
                txthr_type.SelectedValue = ds.Tables("t1").Rows(0)("recognition_type_id").ToString
                txthr_commit.SelectedValue = ds.Tables("t1").Rows(0)("committee_id").ToString

                txtstar_comment.Value = ds.Tables("t1").Rows(0)("comment").ToString
                txtsrp.SelectedValue = ds.Tables("t1").Rows(0)("srp_point").ToString
                txtcash.Text = ds.Tables("t1").Rows(0)("cash_num").ToString
                txtdate_award.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("award_date").ToString)
                txtdate_receive.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("receive_date").ToString)
                txtstar_remark.Text = ds.Tables("t1").Rows(0)("award_remark").ToString

                Try
                    txttopic_main.SelectedValue = ds.Tables("t1").Rows(0)("topic_id").ToString
                Catch ex As Exception

                End Try

                bindTopicSub()

                Try
                    txttopic_sub.SelectedValue = ds.Tables("t1").Rows(0)("subtopic_id").ToString
                Catch ex As Exception

                End Try

          
            End If

           
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub updateHRTab()
        Dim sql As String
        Dim errorMsg As String

        If chkbi1.Checked = True Or chkbi5.Checked = True Or chkbi9.Checked = True Or chkbi10.Checked = True Then
            chk2015_1.Checked = True
        End If

        If chkbi4.Checked = True Then
            chk2015_2.Checked = True
        End If

        If chkbi3.Checked = True Or chkbi6.Checked = True Then
            chk2015_3.Checked = True
        End If

        If chkbi2.Checked = True Or chkbi7.Checked = True Or chkbi8.Checked = True Then
            chk2015_4.Checked = True
        End If

        sql = "UPDATE star_hr_tab SET team_id = '" & txthr_recog_type.SelectedValue & "' "
      
        sql &= " , team_name = '" & txthr_recog_type.SelectedItem.Text & "' "
        sql &= " , event_id = '" & txtevent_admire.SelectedValue & "' "
        sql &= " , event_name = '" & txtevent_admire.SelectedItem.Text & "' "
        sql &= " , sentence_id = '" & txtsentence_admire.SelectedValue & "' "
        sql &= " , sentence_name = '" & txtsentence_admire.SelectedItem.Text & "' "
        sql &= " , detail_id = '" & txthr_detail_combo.SelectedValue & "' "
        sql &= " , detail_name = '" & txthr_detail_combo.SelectedItem.Text & "' "

        If chk_communicate.Checked = True Then
            sql &= " , chk_clear = 1 "
        Else
            sql &= " , chk_clear = 0 "
        End If
        If chk_relative.Checked = True Then
            sql &= " , chk_care = 1 "
        Else
            sql &= " , chk_care = 0 "
        End If
        If chk_talent.Checked = True Then
            sql &= " , chk_smart = 1 "
        Else
            sql &= " , chk_smart = 0 "
        End If
        If chk_quality.Checked = True Then
            sql &= " , chk_quality = 1 "
        Else
            sql &= " , chk_quality = 0 "
        End If

        If chkbi1.Checked = True Then
            sql &= " , chk_newbi1 = 1  "
        Else
            sql &= " , chk_newbi1 = 0  "
        End If

        If chkbi2.Checked = True Then
            sql &= " , chk_newbi2 = 1  "
        Else
            sql &= " , chk_newbi2 = 0  "
        End If

        If chkbi3.Checked = True Then
            sql &= " , chk_newbi3 = 1  "
        Else
            sql &= " , chk_newbi3 = 0  "
        End If

        If chkbi4.Checked = True Then
            sql &= " , chk_newbi4 = 1  "
        Else
            sql &= " , chk_newbi4 = 0  "
        End If

        If chkbi5.Checked = True Then
            sql &= " , chk_newbi5 = 1  "
        Else
            sql &= " , chk_newbi5 = 0  "
        End If

        If chkbi6.Checked = True Then
            sql &= " , chk_newbi6 = 1  "
        Else
            sql &= " , chk_newbi6 = 0  "
        End If

        If chkbi7.Checked = True Then
            sql &= " , chk_newbi7 = 1  "
        Else
            sql &= " , chk_newbi7 = 0  "
        End If

        If chkbi8.Checked = True Then
            sql &= " , chk_newbi8 = 1  "
        Else
            sql &= " , chk_newbi8 = 0  "
        End If

        If chkbi9.Checked = True Then
            sql &= " , chk_newbi9 = 1  "
        Else
            sql &= " , chk_newbi9 = 0  "
        End If

        If chkbi10.Checked = True Then
            sql &= " , chk_newbi10 = 1  "
        Else
            sql &= " , chk_newbi10 =  0  "
        End If

        If chk2015_1.Checked = True Then
            sql &= " , chk_2015_1 = 1  "
        Else
            sql &= " , chk_2015_1 =  0  "
        End If

        If chk2015_2.Checked = True Then
            sql &= " , chk_2015_2 = 1  "
        Else
            sql &= " , chk_2015_2 =  0  "
        End If

        If chk2015_3.Checked = True Then
            sql &= " , chk_2015_3 = 1  "
        Else
            sql &= " , chk_2015_3 =  0  "
        End If

        If chk2015_4.Checked = True Then
            sql &= " , chk_2015_4 = 1  "
        Else
            sql &= " , chk_2015_4 =  0  "
        End If


        sql &= " , recognition_id = '" & txthr_recog.SelectedValue & "' "
        sql &= " , recognition_name = '" & txthr_recog.SelectedItem.Text & "' "
        sql &= " , recognition_type_id = '" & txthr_type.SelectedValue & "' "
        sql &= " , recognition_type_name = '" & txthr_type.SelectedItem.Text & "' "
        sql &= " , committee_id = '" & txthr_commit.SelectedValue & "' "
        sql &= " , committee_name = '" & txthr_commit.SelectedItem.Text & "' "
        sql &= " , comment = '" & addslashes(txtstar_comment.Value) & "' "
        sql &= " , srp_point = '" & txtsrp.SelectedValue & "' "
        sql &= " , cash_num = '" & txtcash.Text & "' "
        sql &= " , award_date = '" & ConvertDateStringToTimeStamp(txtdate_award.Text) & "' "
        sql &= " , receive_date = '" & ConvertDateStringToTimeStamp(txtdate_receive.Text) & "' "
        sql &= " , award_remark = '" & addslashes(txtstar_remark.Text) & "' "
        If txttopic_main.SelectedIndex > 0 Then '
            sql &= " , topic_id = '" & txttopic_main.SelectedValue & "' "
            sql &= " , topic_name = '" & addslashes(txttopic_main.SelectedItem.Text) & "' "
            sql &= " , subtopic_id = '" & txttopic_sub.SelectedValue & "' "
            sql &= " , subtopic_name = '" & addslashes(txttopic_sub.SelectedItem.Text) & "' "
        End If
     
        ' Response.Write("32")
        sql &= " WHERE star_id = " & id
        'Response.Write(sql)
        'Response.End()
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If
        
    End Sub

    Sub onDeleteComment(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "DELETE FROM star_manager_comment WHERE comment_id = " & e.CommandArgument.ToString
            ' errorMsg = conn.executeSQLForTransaction(sql)
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
        Dim sql As String = ""
        Dim errorMsg As String

        Try
            sql = "DELETE FROM star_committee_comment WHERE comment_id = " & e.CommandArgument.ToString
           
            errorMsg = conn.executeSQLForTransaction(sql)
            ' errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            updateOnlyLog("0", "Delete Comment")
            conn.setDBCommit()

            bindCommitteeList()


            ' panelAddCommiteeComment.Visible = True
            bindGridStarLog()
        Catch ex As Exception
            conn.setDBRollback()
            txtadd_comment.Value = ex.Message & sql
            'Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindGridAdmireHR()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select   b.admire_topic AS Topic , count(b.admire_id) as No from star_committee_comment a "
            sql &= " inner join star_m_admire b on a.event_id = b.admire_id"
            sql &= " where a.star_id = " & id
            sql &= " group by a.star_id , a.event_id , b.admire_topic"
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            gridAdmireHR.DataSource = ds.Tables(0)
            gridAdmireHR.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridDetailHR()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select   b.note_en AS Topic , count(b.note_id) as No from star_committee_comment a "
            sql &= " inner join star_m_note b on a.detail_id = b.note_id"
            sql &= " where a.star_id = " & id
            sql &= " group by a.star_id , a.detail_id , b.note_en"
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            gridDetailHR.DataSource = ds.Tables(0)
            gridDetailHR.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridCoASTHR()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select"
            sql &= "  SUM(ISNULL(chk_2015_1,0)) AS 'CO'"
            sql &= "  , SUM(ISNULL(chk_2015_2,0)) AS 'A'"
            sql &= "  , SUM(ISNULL(chk_2015_3,0)) AS 'S'"
            sql &= "  , SUM(ISNULL(chk_2015_4,0)) AS 'T'"
            sql &= " from star_committee_comment a "
            sql &= " WHERE a.star_id =  " & id
            sql &= " GROUP BY a.star_id"

            ds = conn.getDataSetForTransaction(sql, "t1")

            '  Response.Write(sql)
            ' Return

            gridCoastHR.DataSource = ds.Tables(0)
            gridCoastHR.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindAllDoctor()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from m_doctor WHERE ISNULL(doctor_name_en,'') <> ''"

            If txtfind_doctor.Text <> "" Then
                sql &= " AND (doctor_name_en LIKE '%" & txtfind_doctor.Text & "%' OR doctor_name_th LIKE '%" & txtfind_doctor.Text & "%' OR emp_no LIKE '%" & txtfind_doctor.Text & "%' ) "
            Else
                sql &= " AND 1 > 2 "
            End If

            sql &= "  ORDER BY doctor_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdoctor_all.DataSource = ds.Tables(0)
            txtdoctor_all.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectDoctor()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , doctor_name AS doctor_name_en , emp_code as emp_no from star_relate_doctor WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdoctor_select.DataSource = ds
            txtdoctor_select.DataBind()

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblNominee.Text &= ds.Tables("t1").Rows(i)("doctor_name").ToString & "<br/>"
            Next i
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindAllDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from user_dept ORDER BY dept_name_en"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdept_all.DataSource = ds.Tables(0)
            txtdept_all.DataBind()


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
            sql = "SELECT * from star_relate_dept WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdept_select.DataSource = ds
            txtdept_select.DataBind()

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblNominee.Text &= ds.Tables("t1").Rows(i)("costcenter_name").ToString & "<br/>"
            Next i

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindAllPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT emp_code , (user_fullname + ', ' + dept_name) AS user_fullname  from user_profile WHERE emp_code IN (SELECT Employee_id FROM temp_BHUser) "
            If txtdept_all.SelectedValue <> "" Then
                ' sql &= " AND costcenter_id = " & txtdept_all.SelectedValue
            End If

            If txtfind_name.Text <> "" Then
                sql &= " AND (user_fullname LIKE '%" & txtfind_name.Text & "%' OR emp_code LIKE '%" & txtfind_name.Text & "%' OR user_fullname_local LIKE '%" & txtfind_name.Text & "%' )"
            Else
                sql &= " AND 1 > 2 "
            End If

            sql &= " ORDER BY user_fullname"
            '  Response.Write("Xxx" & txtfind_name.Text)
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtperson_all.DataSource = ds
            txtperson_all.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from star_relate_person WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtperson_select.DataSource = ds
            txtperson_select.DataBind()

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblNominee.Text &= ds.Tables("t1").Rows(i)("user_fullname").ToString & "<br/>"
            Next i

        Catch ex As Exception
            Response.Write(ex.Message & sql)
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

    Protected Sub cmdAddRelateDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddRelateDept.Click
        If txtdept_select.Items.Count >= 1 Then
            Return
        End If
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
        If txtperson_select.Items.Count >= 1 Then
            Return
        End If

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

    Protected Sub txtdept_all_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdept_all.SelectedIndexChanged
        'bindRelatePerson()

        'ViewState("selIndex") = txtdept_all.SelectedIndex
        ' Session("dept_index") = txtdept_all.SelectedValue
        'If ViewState("selIndex") IsNot Nothing Then
        '   txtdept_all.SelectedIndex = ViewState("selIndex")
        'End If

        ' txtdept_all.SelectedValue = Session("dept_index")
    End Sub


    Sub updateRelateDept()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        If mode = "edit" Then
            sql = "DELETE FROM star_relate_dept WHERE star_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

         
        End If


        For i As Integer = 0 To txtdept_select.Items.Count - 1
            ' User Relate Dept ssip_relate_dept
            pk = getPK("star_relate_dept_id", "star_relate_dept", conn)
            sql = "INSERT INTO star_relate_dept (star_relate_dept_id , star_id , costcenter_id , costcenter_name ) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & id & "' ,"
            sql &= "'" & txtdept_select.Items(i).Value & "' ,"
            sql &= "'" & txtdept_select.Items(i).Text & "' "

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
            sql = "DELETE FROM star_relate_person WHERE star_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


        For i As Integer = 0 To txtperson_select.Items.Count - 1
            pk = getPK("star_relate_person_id", "star_relate_person", conn)
            sql = "INSERT INTO star_relate_person (star_relate_person_id , star_id , emp_code , user_fullname) VALUES("
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

    Sub updateRelateDoctor()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        If mode = "edit" Then
            sql = "DELETE FROM star_relate_doctor WHERE star_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        End If


        For i As Integer = 0 To txtdoctor_select.Items.Count - 1
            pk = getPK("star_relate_doctor_id", "star_relate_doctor", conn)
            sql = "INSERT INTO star_relate_doctor (star_relate_doctor_id , star_id , emp_code , doctor_name) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & id & "' ,"
            sql &= "'" & txtdoctor_select.Items(i).Value & "' ,"
            sql &= "'" & txtdoctor_select.Items(i).Text & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i


    End Sub

    Private Sub bindGridInformationUpdate()
        Dim ds As New DataSet
        Dim sql As String
        Dim sqlB As New StringBuilder

        Try
            sqlB.Append("SELECT * FROM star_information_update WHERE star_id =  " & id)


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

    Protected Sub cmdAddUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddUpdate.Click
        Dim sql As String = ""
        Dim errorMsg As String = ""
        Dim pk As String = ""

        Try
            pk = getPK("inform_id", "ssip_information_update", conn)
            sql = "INSERT INTO star_information_update (inform_id , star_id , inform_type , inform_detail , inform_date , inform_date_ts , inform_by , inform_emp_code , inform_dept_name , inform_costcenter) VALUES("
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

    Sub bindFileAttach()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM star_attachment a WHERE 1 = 1 "
            If mode = "add" Then
                sql &= " AND a.session_id = '" & session_id & "'"
            Else
                sql &= " AND a.star_id = " & id
            End If
            'Response.Write(sql)

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
                    sql = "SELECT ISNULL(MAX(file_id),0) + 1 AS pk FROM star_attachment"
                    ds = conn.getDataSetForTransaction(sql, "t1")
                    pk = ds.Tables("t1").Rows(0)(0).ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write(sql)
                Finally
                    ds.Dispose()
                    ds = Nothing
                End Try


                sql = "INSERT INTO star_attachment (file_id , star_id ,  file_name , file_path , file_size , session_id) VALUES("
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

                FileUpload0.PostedFile.SaveAs(Server.MapPath("../share/star/attach_file/" & pk & "." & extension))

                conn.setDBCommit()
            End If

            bindFileAttach()
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

        i = GridFileAttach.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridFileAttach.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridFileAttach.Rows(s).FindControl("chkSelect"), CheckBox)

                '  response.write(lbl.Text)
                If chk.Checked Then
                    sql = "DELETE FROM star_attachment WHERE file_id = " & lbl.Text
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
                    File.Delete(Server.MapPath("../share/star/attach_file/" & lblFilePath.Text))
                End If
            Next s

            conn.setDBCommit()
            bindFileAttach()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddRelateDoctor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddRelateDoctor.Click
        If txtdoctor_select.Items.Count >= 1 Then
            Return
        End If
        While txtdoctor_all.Items.Count > 0 AndAlso txtdoctor_all.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtdoctor_all.SelectedItem
            selectedItem.Selected = False
            txtdoctor_select.Items.Add(selectedItem)
            ' txtdept_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveRelateDoctor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveRelateDoctor.Click
        While txtdoctor_select.Items.Count > 0 AndAlso txtdoctor_select.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtdoctor_select.SelectedItem
            selectedItem.Selected = False
            txtdoctor_all.Items.Add(selectedItem)
            txtdoctor_select.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub GridviewIDPLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridviewIDPLog.PageIndexChanging
        GridviewIDPLog.PageIndex = e.NewPageIndex
        bindGridStarLog()
    End Sub

    Protected Sub GridviewIDPLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridviewIDPLog.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim startTS As String = ""
            Dim sql As String
            Dim ds As New DataSet
            Try
                Dim lblDuration As Label = CType(e.Row.FindControl("lblDuration"), Label)
                Dim lblDateTS As Label = CType(e.Row.FindControl("lblTS"), Label)

                sql = "SELECT * FROM star_status_log a INNER JOIN star_status_list b ON a.status_id = b.status_id WHERE a.status_id <> 1 AND a.star_id = " & id
                sql &= " ORDER BY log_status_id ASC"
                ds = conn.getDataSetForTransaction(sql, "table1")
                startTS = ds.Tables("table1").Rows(0)("log_time_ts").ToString

                If startTS <> lblDateTS.Text Then
                    lblDuration.Text = MinuteDiff(startTS, lblDateTS.Text)
                Else
                    lblDuration.Text = 0
                End If

            Catch ex As Exception
                ' Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()

            End Try
        End If
    End Sub

    Protected Sub GridviewIDPLog_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridviewIDPLog.SelectedIndexChanged

    End Sub

    Sub bindCommentList() ' manager comment
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_manager_comment WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")


            lblManagerNum.Text = "(" & ds.Tables("t1").Rows.Count & ")"
            GridManagerComment.DataSource = ds
            GridManagerComment.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAddComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddComment.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        ' login_max_authen = CInt(getMyIPDLevel())

        Try
            pk = getPK("comment_id", "star_manager_comment", conn)
            sql = "INSERT INTO star_manager_comment (comment_id , star_id , comment_status_id , comment_status_name , subject_id , subject , detail "
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
            sql &= " '" & Session("costcenter_id").ToString & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' ,"
            sql &= " '" & 0 & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            updateOnlyLog("0", txtacknowedge_status.SelectedItem.Text)
            conn.setDBCommit()

            txtcomment.SelectedIndex = 0
            txtcomment_detail.Value = ""
            bindCommentList()
            bindGridStarLog()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub GridInformation_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridInformation.RowDeleting
        Dim sql As String
        Dim errorMSg As String

        Try
            sql = "DELETE FROM star_information_update WHERE inform_id = '" & GridInformation.DataKeys(e.RowIndex).Value & "'"
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

    Protected Sub cmdFindName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFindName.Click
        'Response.Write(txtfind_name.Text)
        bindAllPerson()

    End Sub

    Protected Sub cmdFindDoctor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFindDoctor.Click
        bindAllDoctor()
    End Sub

    Sub bindCommitteeList() ' committee comment
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_committee_comment WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")


            lblNumCommittee.Text = "(" & ds.Tables("t1").Rows.Count & ")"
            GridComment.DataSource = ds
            GridComment.DataBind()

            If ds.Tables("t1").Rows.Count <= 0 Then
                lblSumEndorse1.Text = 0
                lblSumNotEndorse1.Text = 0

                lblSumEndorse2.Text = 0
                lblSumNotEndorse2.Text = 0
            End If

            bindCommitteeSummaryAward()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCommitteeSummaryAward() ' committee comment award
        Dim sql As String
        Dim ds As New DataSet

        Try
            lblEndorseDetail.Text = ""

            sql = "SELECT recognition_award , COUNT(recognition_award) AS num FROM star_committee_comment "

            sql &= " WHERE recognition_id = 1 AND star_id = " & id
            sql &= " GROUP BY recognition_award ORDER BY recognition_award"
            ds = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblEndorseDetail.Text &= " " & ds.Tables("t1").Rows(i)("num").ToString & " - " & ds.Tables("t1").Rows(i)("recognition_award").ToString & "<br/>"

                lblEndorseDetail2.Text = lblEndorseDetail.Text
            Next i
           


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSaveCommittee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveCommittee.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try

            If chk_com1.Checked = True Or chk_com5.Checked = True Or chk_com9.Checked = True Or chk_com10.Checked = True Then
                chk2015_com1.Checked = True
            End If

            If chk_com4.Checked = True Then
                chk2015_com2.Checked = True
            End If

            If chk_com3.Checked = True Or chk_com6.Checked = True Then
                chk2015_com3.Checked = True
            End If

            If chk_com2.Checked = True Or chk_com7.Checked = True Or chk_com8.Checked = True Then
                chk2015_com4.Checked = True
            End If

            pk = getPK("comment_id", "star_committee_comment", conn)

            sql = "INSERT INTO star_committee_comment (comment_id , star_id , event_id , event_name , sentence_id , sentence_name , detail_id "
            sql &= ",detail_name , chk_clear , chk_care , chk_smart , chk_quality , chk_newbi1 , chk_newbi2 , chk_newbi3 , chk_newbi4 , chk_newbi5 , chk_newbi6 , chk_newbi7 , chk_newbi8 , chk_newbi9 , chk_newbi10 , recognition_id , recognition_name "
            sql &= " , recognition_type_id , recognition_type_name , recognition_award , committee_comment "
            sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
            sql &= ",create_date , create_date_ts , committee_id , committee_name "
            sql &= ",chk_2015_1 , chk_2015_2 , chk_2015_3 , chk_2015_4 "
            sql &= ") VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & txtadd_event_admire.SelectedValue & "' ,"
            sql &= " '" & txtadd_event_admire.SelectedItem.Text & "' ,"
            sql &= " '" & txtadd_sentence_admire.SelectedValue & "' ,"
            sql &= " '" & txtadd_sentence_admire.SelectedItem.Text & "' ,"
            sql &= " '" & txtadd_detail_combo.SelectedValue & "' ,"
            sql &= " '" & txtadd_detail_combo.SelectedItem.Text & "' ,"
            If chk_add_clear.Checked = True Then
                sql &= " 1 ,"
            Else
                sql &= " 0 ,"
            End If

            If chk_add_care.Checked = True Then
                sql &= " 1 ,"
            Else
                sql &= " 0 ,"
            End If

            If chk_add_smart.Checked = True Then
                sql &= " 1 ,"
            Else
                sql &= " 0 ,"
            End If

            If chk_add_quality.Checked = True Then
                sql &= " 1 ,"
            Else
                sql &= " 0 ,"
            End If

            If chk_com1.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_com2.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_com3.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_com4.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_com5.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_com6.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_com7.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_com8.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_com9.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_com10.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            sql &= " '" & txtstar_conclusion.SelectedValue & "' ,"
            sql &= " '" & txtstar_conclusion.SelectedItem.Text & "' ,"
            sql &= " '" & txtstar_recog.SelectedValue & "' ,"
            sql &= " '" & txtstar_recog.SelectedItem.Text & "' ,"
            sql &= " '" & txtadd_award.SelectedValue & "' ,"
            sql &= " '" & addslashes(txtadd_comment.Value) & "' ,"

            sql &= " '" & Session("job_title").ToString & "' ,"
            sql &= " '" & Session("user_position").ToString & "' ,"
            sql &= " '" & Session("user_fullname").ToString & "' ,"
            sql &= " '" & Session("emp_code").ToString & "' ,"
            sql &= " '" & Session("dept_name").ToString & "' ,"
            sql &= " '" & Session("costcenter_id").ToString & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' , "
            sql &= " '" & txtconsider.SelectedValue & "' , "
            sql &= " '" & txtconsider.SelectedItem.Text & "' , "

            If chk2015_com1.Checked = True Then
                sql &= " 1 ,"
            Else
                sql &= " 0 ,"
            End If

            If chk2015_com2.Checked = True Then
                sql &= " 1 ,"
            Else
                sql &= " 0 ,"
            End If

            If chk2015_com3.Checked = True Then
                sql &= " 1 ,"
            Else
                sql &= " 0 ,"
            End If

            If chk2015_com4.Checked = True Then
                sql &= " 1 "
            Else
                sql &= " 0 "
            End If

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            updateOnlyLog("0", "Star Committee Review")
            conn.setDBCommit()


            txtadd_event_admire.SelectedIndex = 0
            txtadd_detail_combo.SelectedIndex = 0
            txtadd_award.SelectedIndex = 0
            txtstar_conclusion.SelectedIndex = 0
            txtstar_recog.SelectedIndex = 0
            txtadd_comment.Value = ""
            txtconsider.SelectedIndex = 0
            chk_com1.Checked = False
            chk_com2.Checked = False
            chk_com3.Checked = False
            chk_com4.Checked = False
            chk_com5.Checked = False
            chk_com6.Checked = False
            chk_com7.Checked = False
            chk_com8.Checked = False
            chk_com9.Checked = False
            chk_com10.Checked = False

            chk2015_com1.Checked = False
            chk2015_com2.Checked = False
            chk2015_com3.Checked = False
            chk2015_com4.Checked = False

            lblComitteeComment.Text = ""

            bindCommitteeList()
            bindGridStarLog()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub GridComment_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridComment.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblEmpcode As Label = CType(e.Row.FindControl("lblEmpcode"), Label)
            'Dim cmdDelComment As ImageButton = CType(e.Row.FindControl("cmdDelComment"), ImageButton)
            Dim cmdDelComment As Button = CType(e.Row.FindControl("cmdDelComment"), Button)
            Dim lblEndorseID As Label = CType(e.Row.FindControl("lblEndorseID"), Label)
            Dim lblAward As Label = CType(e.Row.FindControl("lblAward"), Label)

            Dim lblCare As Label = CType(e.Row.FindControl("lblCare"), Label)
            Dim lblChk1 As Label = CType(e.Row.FindControl("lblChk1"), Label)
            Dim lblChk2 As Label = CType(e.Row.FindControl("lblChk2"), Label)
            Dim lblChk3 As Label = CType(e.Row.FindControl("lblChk3"), Label)
            Dim lblChk4 As Label = CType(e.Row.FindControl("lblChk4"), Label)

            Dim lblbi1 As Label = CType(e.Row.FindControl("lblbi1"), Label)
            Dim lblbi2 As Label = CType(e.Row.FindControl("lblbi2"), Label)
            Dim lblbi3 As Label = CType(e.Row.FindControl("lblbi3"), Label)
            Dim lblbi4 As Label = CType(e.Row.FindControl("lblbi4"), Label)
            Dim lblbi5 As Label = CType(e.Row.FindControl("lblbi5"), Label)
            Dim lblbi6 As Label = CType(e.Row.FindControl("lblbi6"), Label)
            Dim lblbi7 As Label = CType(e.Row.FindControl("lblbi7"), Label)
            Dim lblbi8 As Label = CType(e.Row.FindControl("lblbi8"), Label)
            Dim lblbi9 As Label = CType(e.Row.FindControl("lblbi9"), Label)
            Dim lblbi10 As Label = CType(e.Row.FindControl("lblbi10"), Label)

            Dim lbl2015_1 As Label = CType(e.Row.FindControl("lbl2015_1"), Label)
            Dim lbl2015_2 As Label = CType(e.Row.FindControl("lbl2015_2"), Label)
            Dim lbl2015_3 As Label = CType(e.Row.FindControl("lbl2015_3"), Label)
            Dim lbl2015_4 As Label = CType(e.Row.FindControl("lbl2015_4"), Label)

            Dim lblCommitteeComment1 As Label = CType(e.Row.FindControl("lblCommitteeComment1"), Label)
            Dim lblPostName As Label = CType(e.Row.FindControl("lblPostName"), Label)

            Dim sql As String
            Dim ds As New DataSet

            Try
                'If lblChk1.Text = "1" Then
                '    lblCare.Text &= "- ความสามารถในการสื่อสาร <br/>"
                'End If

                'If lblChk2.Text = "1" Then
                '    lblCare.Text &= "- สัมพันธไมตรีแบบไทย <br/>"
                'End If

                'If lblChk3.Text = "1" Then
                '    lblCare.Text &= "- ความเป็นเลิศทางวิชาการ <br/>"
                'End If

                'If lblChk4.Text = "1" Then
                '    lblCare.Text &= "- คุณภาพงานบริการ <br/>"
                'End If

                If lbl2015_1.Text = "1" Then
                    lblCare.Text &= "<strong>- Compassionate Caring บริการด้วยความเอื้ออาทร</strong> <br/>"
                End If

                If lblbi1.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- มุ่งมั่นที่จะให้บริการที่เกินความคาดหวังของลูกค้า <br/>"
                End If

                If lblbi5.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- ให้การบริการแบบไทย แก่ผู้ป่วยทุกชาติ ภาษา และวัฒนธรรมอย่างเท่าเทียมกัน <br/>"
                End If

                If lblbi9.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- เป็นองค์กรที่ยึดมั่นในความรับผิดชอบต่อสังคม <br/>"
                End If

                If lblbi10.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- ให้ความสำคัญต่อการอนุรักษ์สิ่งแวดล้อมและทรัพยากรธรรมชาติ <br/>"
                End If

                If lbl2015_2.Text = "1" Then
                    lblCare.Text &= "<strong>- Adaptability, Learning, and Innovation มีความพร้อมในการปรับเปลี่ยน เรียนรู้ สร้างสรรค์นวัตกรรมใหม่ๆ</strong> <br/>"
                End If

                If lblbi4.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- มุ่งมั่นที่ก้าวสู่ความเป็นเลิศทางวิชาชีพและนวัตกรรมทางการแพทย์อย่างต่อเนื่อง <br/>"
                End If


                If lbl2015_3.Text = "1" Then
                    lblCare.Text &= "<strong>- Safety, Quality with Measurable Results ยึดมั่นเรื่องความปลอดภัย คุณภาพมีผลลัพธ์ที่วัดผลได้</strong> <br/>"
                End If

                If lblbi3.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- ปรับปรุงคุณภาพและความปลอดภัยของทุกสิ่งที่เราปฏิบัติอย่างต่อเนื่อง <br/>"
                End If

                If lblbi6.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- มุ่งมั่นที่จะพัฒนาการบริการทุกขั้นตอนให้ได้มาตรฐานระดับโลก <br/>"
                End If

                If lbl2015_4.Text = "1" Then
                    lblCare.Text &= "<strong>- Teamwork and Integrity ทำงานเป็นทีมและยึดมั่นหลักคุณธรรม</strong> <br/>"
                End If

                If lblbi2.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- มุ่งมั่นในการพัฒนาด้านการศึกษา การเรียนการสอน การเพิ่มศักยภาพและระบบสวัสดิการที่ดีต่อบุคลากรโรงพยาบาล <br/>"
                End If


                If lblbi7.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- ปฏิบัติต่อผู้ป่วยด้วยความซื่อสัตย์ มีความไว้วางใจซึ่งกันและกัน และคงไว้ซึ่งจริยธรรม <br/>"
                End If

                If lblbi8.Text = "1" Then
                    lblCare.Text &= "&nbsp;&nbsp;- ทำงานร่วมกันเป็นทีมและแลกเปลี่ยนสิ่งที่เรียนรู้แก่กันและกัน <br/>"
                End If

             

                If lblEndorseID.Text = "1" Then
                    num_endorse_com += 1
                ElseIf lblEndorseID.Text = "2" Then
                    num_not_endorse_com += 1
                End If

                lblSumEndorse1.Text = num_endorse_com
                lblSumNotEndorse1.Text = num_not_endorse_com

                lblSumEndorse2.Text = num_endorse_com
                lblSumNotEndorse2.Text = num_not_endorse_com

                lblComitteeComment.Text &= lblPostName.Text & " : " & lblCommitteeComment1.Text & "<br/>"

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try

            If lblEmpcode.Text = Session("emp_code").ToString Then
                cmdDelComment.Visible = True
                ' cmdEditComment.Visible = True

            Else
                cmdDelComment.Visible = False
            End If

            If viewtype = "update" Then
                cmdDelComment.Visible = False
            End If
        End If
    End Sub

    Protected Sub GridComment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridComment.SelectedIndexChanged

    End Sub

    Protected Sub GridManagerComment_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridManagerComment.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)

            Dim lblEmpcode As Label = CType(e.Row.FindControl("lblEmpcode"), Label)
            Dim cmdDelComment As ImageButton = CType(e.Row.FindControl("cmdDelComment"), ImageButton)


            Dim sql As String = ""
            Dim ds As New DataSet

          

            If lblEmpcode.Text = Session("emp_code").ToString Then
                cmdDelComment.Visible = True
                ' cmdEditComment.Visible = True
                '  cmdMgrReview.Visible = True
            Else
                cmdDelComment.Visible = False
                '  cmdEditComment.Visible = False
                ' cmdMgrReview.Visible = False
            End If

            If viewtype = "update" Then
                cmdDelComment.Visible = False
            End If


        End If
    End Sub

    Protected Sub GridManagerComment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridManagerComment.SelectedIndexChanged

    End Sub

    Sub insertLoopMailLog(ByVal cmd As String, subject As String, send_to As String, cc_to As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet

        sql = "INSERT INTO star_alert_log (star_id , alert_date , alert_date_ts , alert_method , subject , send_to , cc_to) VALUES( "
        sql &= "'" & id & "' ,"
        sql &= "GETDATE() ,"
        sql &= "'" & Date.Now.Ticks & "' ,"
        sql &= "'" & cmd & "' ,"
        sql &= "'" & subject & "' ,"
        sql &= "'" & send_to & "' ,"
        sql &= "'" & cc_to & "' "
        sql &= ")"
        ' Response.Write(sql)
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

        sql = "INSERT INTO star_alert_log (star_id , alert_date , alert_date_ts , alert_method , subject , send_to , cc_to) VALUES( "
        sql &= "'" & id & "' ,"
        sql &= "GETDATE() ,"
        sql &= "'" & Date.Now.Ticks & "' ,"
        sql &= "'" & cmd & "' ,"
        sql &= "'" & txtsubject.SelectedItem.Text & "' ,"
        sql &= "'" & txtto.Value & "' ,"
        sql &= "'" & txtcc.Value & "' "
        sql &= ")"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & "" & sql)
        End If
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
            Dim p3 As String = "[Star" & "" & "]  : " & txtsubject.SelectedItem.Text
            Dim p4 As String = txtmessage.Value
            Dim p5 = ""

            ' Dim parameters As Object() = New Object() {p1, p2, p3, p4, p5}

            'thread.Start(parameters)
            Dim host As String = ConfigurationManager.AppSettings("frontHost").ToString
            Dim key = UserActivation.GetActivationLink("star/form_star.aspx?mode=edit&id=" & id)
            Dim msgbody As String = ""

            'msgbody &= txtsubject.SelectedItem.Text & "<br/>" & vbCrLf

            If txtsubject.SelectedValue = "4" Or txtsubject.SelectedValue = "7" Then ' manager
                msgbody = "Dear All,<br/>" & vbCrLf
                msgbody &= "Please consider, review and propose for the Star of the month  of (" & getMonthName(Date.Now.Month) & ") " & Date.Now.Year & ", in the link below. <br/>" & vbCrLf
                msgbody &= vbCrLf & "<br/><a href='http://" & host & "/login.aspx?viewtype=sup&key=" & key & "'>Star of Bumrungrad Online</a>"

                msgbody &= "<br/><br/>Best Regards,<br/><br/>"
                msgbody &= "Janjira Pimwong<br/>"
                msgbody &= "Star Of Bumrungrad Committee"
            ElseIf txtsubject.SelectedValue = "5" Then ' committee
                msgbody = "Dear Chairman,<br/>" & vbCrLf
                msgbody &= "Please approve  the Star of the month of " & getMonthName(Date.Now.Month) & " " & Date.Now.Year & ", in the link below. <br/>" & vbCrLf
                msgbody &= vbCrLf & "<br/><a href='http://" & host & "/login.aspx?viewtype=com&key=" & key & "'>Star of Bumrungrad Online</a>"
                msgbody &= "<br/><br/>Best Regards,<br/><br/>"
                msgbody &= "Janjira Pimwong<br/>"
                msgbody &= "Star Of Bumrungrad Committee"
            ElseIf txtsubject.SelectedValue = "6" Then ' update
                ' msgbody &= "Dear ,<br/>" & vbCrLf
                
                msgbody = "<br/>" & txtmessage.Value.Replace(vbCrLf, "<br/>") & "<br/>"
                msgbody &= "<br/>Thank you,<br/>"
                msgbody &= "With Best Regards,<br/>"
                msgbody &= "Please kindly open a link below<br/>" & vbCrLf
                msgbody &= vbCrLf & "<br/><a href='http://" & host & "/login.aspx?viewtype=update&key=" & key & "'>Star of Bumrungrad Online</a> "
                msgbody &= "<br/><br/>Best Regards,<br/><br/>"

                msgbody &= "Star Of Bumrungrad Committee"
            End If

            msgbody &= "<br/>"

            'Response.Write(msgbody)
            sendMailWithCC_Star(email_list, cc_list, bcc_list, p3, msgbody, "", "ir")

        Catch ex As Exception
            Response.Write("Send mail :: " & ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub sendMailWithCC_Star(ByVal toEmail() As String, ByVal ccEmail() As String, ByVal bccEmail() As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal mailType As String = "ir", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Dim smtp As New SmtpClient("mail.bumrungrad.com")

        Try

            oMsg.From = New MailAddress("starbi@bumrungrad.com")
            oMsg.Headers.Add("Disposition-Notification-To", "<Star of Bumrungrad>")

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

            bindGridStarLog()
            ' bindGridAlertLog()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Response.End()
        End Try
    End Sub

    Protected Sub txtsamename_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtsamename.CheckedChanged
        
    End Sub

    Protected Sub txtadd_event_admire_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtadd_event_admire.SelectedIndexChanged
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_m_admire WHERE admire_id = " & txtadd_event_admire.SelectedValue
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows(0)("is_clear").ToString = "1" Then
                chk_add_clear.Checked = True
                chk2015_com1.Checked = True
                ' chk_add_clear.Checked = True
            Else
                chk_add_clear.Checked = False
                chk2015_com1.Checked = False
            End If

            If ds.Tables("t1").Rows(0)("is_care").ToString = "1" Then
                chk_add_care.Checked = True
                chk2015_com2.Checked = True
            Else
                chk_add_care.Checked = False
                chk2015_com2.Checked = False
            End If

            If ds.Tables("t1").Rows(0)("is_smart").ToString = "1" Then
                chk_add_smart.Checked = True
                chk2015_com3.Checked = True
            Else
                chk_add_smart.Checked = False
                chk2015_com3.Checked = False
            End If

            If ds.Tables("t1").Rows(0)("is_quality").ToString = "1" Then
                chk_add_quality.Checked = True
                chk2015_com4.Checked = True
            Else
                chk_add_quality.Checked = False
                chk2015_com4.Checked = False
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub txtadd_detail_combo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtadd_detail_combo.SelectedIndexChanged
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_m_note WHERE note_id = " & txtadd_detail_combo.SelectedValue
            ds = conn.getDataSetForTransaction(sql, "t1")

            Try
                txtstar_conclusion.SelectedValue = ds.Tables("t1").Rows(0)("endrose_id").ToString
            Catch ex As Exception

            End Try

            Try
                txtstar_recog.SelectedValue = ds.Tables("t1").Rows(0)("recognition_id").ToString
            Catch ex As Exception

            End Try

            Try
                txtadd_award.SelectedValue = ds.Tables("t1").Rows(0)("recognition_award").ToString
            Catch ex As Exception

            End Try

        Catch ex As Exception
            ' Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub txttopic_main_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txttopic_main.SelectedIndexChanged
        bindTopicSub()
    End Sub

    Protected Sub txtcomplain_status_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtcomplain_status.SelectedIndexChanged
        If txtcomplain_status.SelectedValue = "1" Then
            div_hn_remark.Visible = True
            div_profile.Visible = True
        Else
            div_hn_remark.Visible = True
            div_profile.Visible = False
        End If
    End Sub

    Protected Sub txtfeedback_from_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtfeedback_from.SelectedIndexChanged
        If txtfeedback_from.SelectedValue = "6" Then ' Other / อื่นๆ
            div_source_other.Visible = True
            div_cfb.Visible = False
        ElseIf txtfeedback_from.SelectedValue = "2" Then ' form CFB
            div_source_other.Visible = False
            div_cfb.Visible = True
        Else
            div_source_other.Visible = False
            div_cfb.Visible = False
        End If
    End Sub

    Protected Sub txtcustomer_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtcustomer.SelectedIndexChanged
        If txtcustomer.SelectedValue = "3" Or txtcustomer.SelectedValue = "4" Then
            div_response_remark.Visible = True
        Else
            div_response_remark.Visible = False
        End If

        If txtcustomer.SelectedValue = "2" Or txtcustomer.SelectedValue = "4" Then ' ติดกลับกลับโดยวิธี
            div_response_method.Visible = True
            lblCFBcomment19.Visible = True
        Else
            div_response_method.Visible = False
            lblCFBcomment19.Visible = False
        End If
    End Sub

    Protected Sub txtevent_admire_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtevent_admire.SelectedIndexChanged
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_m_admire WHERE admire_id = " & txtevent_admire.SelectedValue
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows(0)("is_clear").ToString = "1" Then
                chk_communicate.Checked = True
                ' chk_add_clear.Checked = True
                chk2015_1.Checked = True
            Else
                chk_communicate.Checked = False
                chk2015_1.Checked = False
            End If

            If ds.Tables("t1").Rows(0)("is_care").ToString = "1" Then
                chk_relative.Checked = True
                chk2015_2.Checked = True
            Else
                chk_relative.Checked = False
                chk2015_2.Checked = False
            End If

            If ds.Tables("t1").Rows(0)("is_smart").ToString = "1" Then
                chk_talent.Checked = True
                chk2015_3.Checked = True
            Else
                chk_talent.Checked = False
                chk2015_3.Checked = False
            End If

            If ds.Tables("t1").Rows(0)("is_quality").ToString = "1" Then
                chk_quality.Checked = True
                chk2015_4.Checked = True
            Else
                chk_quality.Checked = False
                chk2015_4.Checked = False
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub txthr_detail_combo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txthr_detail_combo.SelectedIndexChanged
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_m_note WHERE note_id = " & txthr_detail_combo.SelectedValue
            ds = conn.getDataSetForTransaction(sql, "t1")

            Try
                txthr_recog.SelectedValue = ds.Tables("t1").Rows(0)("endrose_id").ToString
            Catch ex As Exception

            End Try

            Try
                txthr_type.SelectedValue = ds.Tables("t1").Rows(0)("recognition_id").ToString
            Catch ex As Exception

            End Try

            Try
                txtsrp.SelectedValue = ds.Tables("t1").Rows(0)("recognition_award").ToString
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub txtperson_all_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtperson_all.SelectedIndexChanged
        'txtperson_all.ToolTip = txtperson_all.SelectedItem.Text
    End Sub

    Protected Sub txtsubject_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtsubject.SelectedIndexChanged
        Dim msgBody As String = ""
        Dim name() As String
        Dim name_Str As String = ""

        If txtsubject.SelectedValue = 6 Then ' update & acknowledge
            ' txtmessage.Value &= "Dear _____________" & vbCrLf
            ' txtmessage.Value &= "We are delighted to inform you that _______(name)_____________" & vbCrLf
            ' txtmessage.Value &= "has been awarded with ____ points for  _(her/his­­­­­­­­­­­­­­­­)_ recognition for the Star of Bumrungrad for the month of ______(" & getMonthName(Date.Now.Month) & ") " & Date.Now.Year & "."
            ' txtmessage.Value &= "We are absolutely sure that you are very proud of ____Khun_____________for __his/her__ contribution to the hospital’s mission to "
            ' txtmessage.Value &= "provide excellent service at all times."

            msgBody = "Dear All" & "" & vbCrLf & vbCrLf
            'Response.Write(11111)
            If txtperson_select.Items.Count > 0 Then

                txtperson_select.SelectedIndex = 0

                Try

                    name = txtperson_select.SelectedItem.ToString.Split(",")
                    name_Str = name(0)
                Catch ex As Exception
                    name_Str = txtperson_select.SelectedItem.ToString
                End Try
                msgBody &= "We are delighted to inform you that Khun " & name_Str
            End If

            If txtdept_select.Items.Count > 0 Then
                txtdept_select.SelectedIndex = 0
                msgBody &= "We are delighted to inform you that " & txtdept_select.SelectedItem.ToString
            End If

            If txtdoctor_select.Items.Count > 0 Then
                txtdoctor_select.SelectedIndex = 0
                msgBody &= "We are delighted to inform you that " & txtdoctor_select.SelectedItem.ToString
            End If

            msgBody &= " has been awarded with " & txtsrp.SelectedValue & " points for  his/her recognition for "
            msgBody &= " the Star of Bumrungrad for the month of " & MonthName(Date.Now.Month) & " " & Date.Now.Year & ".We are absolutely sure that you are very proud of "
            If txtperson_select.Items.Count > 0 Then
                txtperson_select.SelectedIndex = 0
                Try
                    name = txtperson_select.SelectedItem.ToString.Split(",")
                    name_Str = name(0)
                Catch ex As Exception
                    name_Str = txtperson_select.SelectedItem.ToString
                End Try
                msgBody &= "Khun " & name_Str
            End If

            If txtdept_select.Items.Count > 0 Then
                txtdept_select.SelectedIndex = 0
                msgBody &= "" & txtdept_select.SelectedItem.Text
            End If

            If txtdoctor_select.Items.Count > 0 Then
                txtdoctor_select.SelectedIndex = 0
                msgBody &= "" & txtdoctor_select.SelectedItem.Text
            End If
            msgBody &= " for his/her contribution to the hospital’s mission to provide excellent service at all times." & vbCrLf

            txtmessage.Value = msgBody
            '  msgBody &= "<br/><br/>Thank you,<br/>"
            ' msgBody &= "With Best Regards,<br/><br/>"
            ' msgBody &= "Please kindly open a link below<br/>" & vbCrLf
            ' msgBody &= vbCrLf & "<br/><a href='http://" & host & "/login.aspx?viewtype=update&key=" & key & "'>Star of Bumrungrad Online</a> "
            ' msgBody &= "<br/><br/>Best Regards,<br/><br/>"
        Else
            txtmessage.Value = ""
        End If
    End Sub
End Class
