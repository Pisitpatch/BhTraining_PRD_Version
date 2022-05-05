Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Data.SqlClient

Partial Class incident_preview_incident
    Inherits System.Web.UI.Page
    Protected formId As String = ""
    Protected irId As String = ""
    Protected dept_id As String = ""
    Protected mode As String = ""
    Private cl As Control
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected diag As String = ""
    Protected viewtype As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        irId = Request.QueryString("irId")
        mode = Request.QueryString("mode")
        dept_id = Request.QueryString("dept_id")
        formId = Request.QueryString("formId")
        viewtype = Request.QueryString("viewtype")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If


        bindDeptRelate()
        bindTQMCause()

        bindDepartment()
        bindForm()
        If CInt(formId) = 1 Then
            cl = LoadControl("rptFallControl.ascx")
            PlaceHolder1.Controls.Add(cl)
            bindFall()
        ElseIf CInt(formId) = 2 Then
            cl = LoadControl("rptMedControl.ascx")
            PlaceHolder1.Controls.Add(cl)
            bindMed()
        ElseIf CInt(formId) = 3 Then
            cl = LoadControl("rptPhebControl.ascx")
            PlaceHolder1.Controls.Add(cl)
            bindPheb()
        ElseIf CInt(formId) = 4 Then
            ' cl = LoadControl("AnesControl.ascx")
        ElseIf CInt(formId) = 5 Then
            ' cl = LoadControl("OtherControl.ascx")
        ElseIf CInt(formId) = 6 Then
            cl = LoadControl("rptPressureControl.ascx")
            PlaceHolder1.Controls.Add(cl)
            bindPressure()
        ElseIf CInt(formId) = 7 Then
            '  cl = LoadControl("DeleteScanControl.ascx")
            '
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

    Sub bindDeptRelate()
        Dim sql As String
        Dim ds As New DataSet
        Dim limit As String = ""
        Try
            sql = "SELECT * FROM ir_relate_dept WHERE ir_id = " & irId
            ds = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                lblDeptRelate.Text &= limit & ds.Tables("t1").Rows(i)("dept_name").ToString
            Next i

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindTQMCause()
        Dim sql As String
        Dim ds As New DataSet
        Dim limit As String = ""
        Try
            sql = "SELECT * FROM ir_tqm_cause_list WHERE ir_id = " & irId
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count > 0 Then
                lblIRCause.Text &= "<table  width='90%' cellspacing='0' style='border-collapse:collapse' ><tr><td style='border:solid 1px #000;font-weight:bold'>Cause of Incident</td><td style='border:solid 1px #000;font-weight:bold'>Description</td></tr>"

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblIRCause.Text &= "<tr>"
                    lblIRCause.Text &= "<td style='border:solid 1px #000' width='30%'>" & ds.Tables("t1").Rows(i)("cause_name").ToString & "</td>"
                    lblIRCause.Text &= "<td style='border:solid 1px #000' width='70%'>" & ds.Tables("t1").Rows(i)("cause_remark").ToString & "</td>"
                    lblIRCause.Text &= "</tr>"
                Next i
                lblIRCause.Text &= "</table>"
            End If
          

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            conn.setConnectionForTransaction()

            sql = "SELECT *  FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id"
            sql &= " LEFT OUTER JOIN ir_tqm_tab c ON a.ir_id = c.ir_id"
            sql &= " LEFT OUTER JOIN ir_topic_grand d ON c.grand_topic = d.grand_topic_id "
            sql &= " LEFT OUTER jOIN ir_topic e ON c.topic = e.ir_topic_id"
            sql &= " LEFT OUTER jOIN ir_topic_sub f ON c.subtopic1 = f.ir_subtopic_id"
            sql &= " LEFT OUTER jOIN ir_topic_sub2 g ON c.subtopic2 = g.ir_subtopic2_id"
            sql &= " LEFT OUTER jOIN ir_topic_sub3 h ON c.subtopic3 = h.ir_subtopic3_id"
            sql &= " LEFT OUTER jOIN ir_m_severity s ON c.severe_level_id = s.severe_id"
            sql &= " WHERE a.ir_id = " & irId

            ' sql = "SELECT  ir_id FROM ir_trans_list a  WHERE a.ir_id = 1 "

            ' Response.Write(sql)
            'Response.Write(66666)
            '  conn.startTransactionSQLServer()
            ds = conn.getDataSetForTransaction(sql, "t1")

            '  Return
            ' Response.Write(ds.Tables.Count)

            If ds.Tables.Count = 0 Then
                Response.Write(conn.errMessage)
                Return
            End If
            ' Return
            lblNo.Text = ds.Tables("t1").Rows(0)("ir_no").ToString
            '   Response.Write(3333)
            'lblNo0.Text = ds.Tables("t1").Rows(0)("ir_no").ToString)
            lblDetail.Text = ds.Tables("t1").Rows(0)("describe").ToString.Replace(vbCrLf, "<br/>").Replace("<", "&lt;").Replace(">", "&gt;")
            lblDept.Text = ds.Tables("t1").Rows(0)("division").ToString
            lblAge.Text = ds.Tables("t1").Rows(0)("age").ToString
            lblSex.Text = ds.Tables("t1").Rows(0)("sex").ToString
            lblHN.Text = ds.Tables("t1").Rows(0)("hn").ToString
            If ds.Tables("t1").Rows(0)("operation").ToString = "" Then
                lblprocedure.Text = "-"
            Else
                lblprocedure.Text = ds.Tables("t1").Rows(0)("operation").ToString
            End If

            lblroom.Text = ds.Tables("t1").Rows(0)("room").ToString
            lbldoctor_owner.Text = ds.Tables("t1").Rows(0)("physician").ToString
            ' lblpt_type.Text = ds.Tables("t1").Rows(0)("pt_type").ToString
            lblName.Text = ds.Tables("t1").Rows(0)("pt_title").ToString & " " & ds.Tables("t1").Rows(0)("pt_name").ToString
            diag = ds.Tables("t1").Rows(0)("diagnosis").ToString & " "
            lbldatetime_report.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString) & " "
            lblreport_date.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString) & " "
            lblreport_by.Text = ds.Tables("t1").Rows(0)("report_by").ToString & " "
            lblreport_tel.Text = ds.Tables("t1").Rows(0)("report_tel").ToString & " "
            If ds.Tables("t1").Rows(0)("datetime_assessment_ts").ToString() = "0" Or ds.Tables("t1").Rows(0)("datetime_assessment_ts").ToString() = "" Then
            Else
                '    lbldatetime_assessment.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("datetime_assessment_ts").ToString) & " "
            End If

            ' lbldoctor_name.Text = ds.Tables("t1").Rows(0)("doctor_name").ToString & " "
            lbllocation.Text = ds.Tables("t1").Rows(0)("location").ToString & " "
            lblservicetype.Text = ds.Tables("t1").Rows(0)("service_type").ToString & " "
            '  lblrecommend_detail.Text = ds.Tables("t1").Rows(0)("recommend_detail").ToString & " "
            ' lbldescribe_action.Text = ds.Tables("t1").Rows(0)("describe_action").ToString & " "
            ' lbldescribe_assessment.Text = ds.Tables("t1").Rows(0)("describe_assessment").ToString & " "
            lblinitial_action.Text = ds.Tables("t1").Rows(0)("initial_action").ToString & " "
            lblAction_Remark.Text = ds.Tables("t1").Rows(0)("action_result_remark").ToString & " "

            If ds.Tables("t1").Rows(0)("chk_physician").ToString = "1" Then
                lblpost_action.Text &= "แจ้งแพทย์เจ้าของไข้ "
            End If

            If ds.Tables("t1").Rows(0)("chk_family").ToString = "1" Then
                lblpost_action.Text &= "แจ้งญาติผู้ป่วย  "
            End If

            If ds.Tables("t1").Rows(0)("chk_document").ToString = "1" Then
                lblpost_action.Text &= "บันทึกรายงานใน File  "
            End If

            '  lblTreatment.Text = ""
            If ds.Tables("t1").Rows(0)("chk_xray").ToString = "1" Then
                '  lblTreatment.Text &= "X-Ray " & ds.Tables("t1").Rows(0)("xray_detail").ToString & " Result " & ds.Tables("t1").Rows(0)("xray_result").ToString & "<br/>"
            End If
            If ds.Tables("t1").Rows(0)("chk_lab").ToString = "1" Then
                '  lblTreatment.Text &= "Lab " & ds.Tables("t1").Rows(0)("lab_detail").ToString & " Result " & ds.Tables("t1").Rows(0)("lab_result").ToString & "<br/>"
            End If
            If ds.Tables("t1").Rows(0)("chk_other").ToString = "1" Then
                '  lblTreatment.Text &= "Other " & ds.Tables("t1").Rows(0)("other_detail").ToString & " Result " & ds.Tables("t1").Rows(0)("other_result").ToString & "<br/>"
            End If

            If ds.Tables("t1").Rows(0)("severe_id").ToString = "1" Then
                lblSevere.Text = "No apparent injury"
            ElseIf ds.Tables("t1").Rows(0)("severe_id").ToString = "2" Then
                lblSevere.Text = "Minor (needs observation)"
            ElseIf ds.Tables("t1").Rows(0)("severe_id").ToString = "3" Then
                lblSevere.Text = "Moderate (needs further treatment)"
            ElseIf ds.Tables("t1").Rows(0)("severe_id").ToString = "4" Then
                lblSevere.Text = "Severe (result in extended hospital stay or transfer to critical care)"
            ElseIf ds.Tables("t1").Rows(0)("severe_id").ToString = "5" Then
                lblSevere.Text = "Death"
            End If

            If ds.Tables("t1").Rows(0)("pt_type").ToString = "1" Then
                lblpttype.Text = "ผู้ป่วย"
            ElseIf ds.Tables("t1").Rows(0)("pt_type").ToString = "2" Then
                lblpttype.Text = "ผู้ญาติผู้ป่วย"
            ElseIf ds.Tables("t1").Rows(0)("pt_type").ToString = "3" Then
                lblpttype.Text = "พนักงาน"
            ElseIf ds.Tables("t1").Rows(0)("pt_type").ToString = "4" Then
                lblpttype.Text = "ผู้มาเยี่ยมไข้"
            ElseIf ds.Tables("t1").Rows(0)("pt_type").ToString = "5" Then
                lblpttype.Text = "อื่นๆ"
            Else
                lblpttype.Text = "-"
            End If

            If ds.Tables("t1").Rows(0)("customer_segment").ToString = "1" Then
                lblsegment.Text = "ไทย"
            ElseIf ds.Tables("t1").Rows(0)("customer_segment").ToString = "2" Then
                lblsegment.Text = "ต่างชาติ (ย้ายภูมิลำเนา)"
            ElseIf ds.Tables("t1").Rows(0)("customer_segment").ToString = "3" Then
                lblsegment.Text = "ต่างชาติ"
            Else
                lblsegment.Text = "-"
            End If


            If ds.Tables("t1").Rows(0)("severe_id").ToString = "1" Then
                lblEffect.Text = "No Effect"
            ElseIf ds.Tables("t1").Rows(0)("severe_id").ToString = "2" Then
                lblEffect.Text = "Effect to " & ds.Tables("t1").Rows(0)("severe_other_remark").ToString

            End If

            lblTopic.Text = ds.Tables("t1").Rows(0)("grand_topic_name").ToString
            lblTopic1.Text = ds.Tables("t1").Rows(0)("topic_name").ToString
            lblTopic2.Text = ds.Tables("t1").Rows(0)("subtopic_name").ToString
            lblTopic3.Text = ds.Tables("t1").Rows(0)("subtopic2_name_en").ToString
            lblTopic4.Text = ds.Tables("t1").Rows(0)("subtopic3_name_en").ToString
            lblTopicDetail.Text = ds.Tables("t1").Rows(0)("tqm_topic_detail").ToString
            lblAction.Text = ds.Tables("t1").Rows(0)("action_detail").ToString
            lblLevel.Text = ds.Tables("t1").Rows(0)("severe_name").ToString
            lblowner.Text = ds.Tables("t1").Rows(0)("tqm_case_owner").ToString
        Catch ex As Exception
            '  Response.Write(ex.StackTrace & sql)
            Response.Write(ex.Message & sql)
            Response.End()

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDepartment()
        Dim sql As String
        Dim ds As New DataSet
        Dim ds2 As New DataSet
        Dim str As String = ""
        Try
            lblDepartmentList.Text = ""
            sql = "SELECT * FROM ir_relate_dept WHERE ir_id = " & irId
            If viewtype = "" And dept_id <> "" Then
                sql &= " AND dept_id = " & dept_id
            ElseIf viewtype = "tqm" Then

            End If

            ds = conn.getDataSetForTransaction(sql, "t1")

            For s = 0 To ds.Tables("t1").Rows.Count - 1
                '   Response.Write(s)
                '  Response.Write(ds.Tables("t1").Rows(s)("dept_name").ToString)
                'If ds.Tables("t1").Rows.Count > 0 Then
                str &= "<strong>แผนก</strong> " & ds.Tables("t1").Rows(s)("dept_name").ToString & "<br/>"

                str &= "<strong><u>สรุปเหตุการณ์</u></strong> " & ds.Tables("t1").Rows(s)("investigation").ToString.Replace(vbCrLf, "<br/>") & "<br/>"
                str &= "<strong>สาเหตุ</strong> " & ds.Tables("t1").Rows(s)("cause_detail").ToString.Replace(vbCrLf, "<br/>") & "<br/>"

                str &= "<br/> <strong>วิธีแก้ไขปัญหาและป้องกันการเกิดเหตุซ้ำ</strong> <br/>"

                sql = "SELECT * FROM ir_dept_prevent_list WHERE relate_dept_id = " & ds.Tables("t1").Rows(s)("relate_dept_id").ToString
                sql &= " ORDER BY order_sort ASC"

                ds2 = conn.getDataSetForTransaction(sql, "t" & (s + 2))

                If ds2.Tables("t" & (s + 2)).Rows.Count > 0 Then
                    '  Response.Write(ds.Tables("t" & (s + 2)).Rows.Count)
                    str &= "<table  width='100%' cellspacing='0' style='border-collapse:collapse' ><tr><td style='border:solid 1px #000;font-weight:bold'>Corrective & Preventive Actions</td><td style='border:solid 1px #000;font-weight:bold'>Start</td><td style='border:solid 1px #000;font-weight:bold'>Completed</td><td style='border:solid 1px #000;font-weight:bold'>Responsible Person</td></tr>"
                    For i As Integer = 0 To ds2.Tables("t" & (s + 2)).Rows.Count - 1
                        str &= "<tr>"
                        str &= "<td style='border:solid 1px #000'>" & ds2.Tables("t" & (s + 2)).Rows(i)("action_detail").ToString.Replace(vbCrLf, "<br/>") & "&nbsp;</td>"
                        str &= "<td style='border:solid 1px #000'>" & ds2.Tables("t" & (s + 2)).Rows(i)("date_start").ToString.Replace(vbCrLf, "<br/>") & "&nbsp;</td>"
                        str &= "<td style='border:solid 1px #000'>" & ds2.Tables("t" & (s + 2)).Rows(i)("date_end").ToString.Replace(vbCrLf, "<br/>") & "&nbsp;</td>"
                        str &= "<td style='border:solid 1px #000'>" & ds2.Tables("t" & (s + 2)).Rows(i)("resp_person").ToString.Replace(vbCrLf, "<br/>") & "&nbsp;</td>"
                        str &= "</tr>"
                    Next i

                    str &= "</table>"
                End If
                str &= "<br/>"
                ' Response.Write("xxx")
                'End If
            Next s

            lblDepartmentList.Text = str
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
            ds2.Dispose()
        End Try
    End Sub


    Sub bindMed()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT  a.ir_id , 'IR' + cast(c.ir_no as varchar) AS 'IR NO' "
            sql &= " , c.pt_name , c.service_type AS patient_type  , c.describe AS 'Detail' , d.action_detail AS 'Corrective Action' "
            sql &= ", t1.grand_topic_name , t2.topic_name , t3.subtopic_name "
            sql &= ", case when chk_wrongtime = 1 then 'Wrong time' else '' end AS 'Wrong time'"
            sql &= ", case when chk_wrongroute= 1 then 'Wrong route' else '' end AS 'Wrong route'"
            sql &= ", case when chk_wrongdate= 1 then 'Wrong date' else '' end AS 'Wrong date'"
            sql &= ", case when chk_wrongrate= 1 then 'Wrong rate' else '' end AS 'Wrong rate'"
            sql &= ", case when chk_omission= 1 then 'Omission' else '' end AS 'Omission'"
            sql &= ", case when chk_wronglabel= 1 then 'Wrong label' else '' end AS 'Wrong label'"
            sql &= ", case when chk_wrongform= 1 then 'Wrong form' else '' end AS 'Wrong form'"
            sql &= ", case when chk_wrongdose= 1 then 'Wrong dose' else '' end AS 'Wrong dose'"
            sql &= ", case when chk_extradose= 1 then 'Extra dose' else '' end AS 'Extra dose'"
            sql &= ", case when chk_wrongiv= 1 then 'Wrong IV' else '' end AS 'Wrong IV'"
            sql &= ", case when chk_wrong_deteriorate= 1 then 'Wrong deteriorate' else '' end AS 'Wrong deteriorate'"
            sql &= ", case when chk_wrong_prep= 1 then 'Wrong drug preparation' else '' end AS 'Wrong drug preparation'"
            sql &= ", case when chk_wrong_drugerror= 1 then 'Unauthorized drug error' else '' end AS 'Unauthorized drug error'"
            sql &= ", case when chk_wrong_duplicate= 1 then 'Duplicate drug' else '' end AS 'Duplicate drug'"
            sql &= ", case when chk_wrong_formular= 1 then 'Wrong formula' else '' end AS 'Wrong formula'"
            sql &= ", case when chk_wrong_qty= 1 then 'Wrong quantity' else '' end AS 'wrong quantity'"
            sql &= ", case when chk_wrong_drug= 1 then 'Wrong drug' else '' end AS 'Wrong drug'"
            sql &= ", case when chk_wrong_pt= 1 then 'Wrong patient' else '' end AS 'Wrong patient'"
            sql &= ", case when chk_allergy= 1 then 'Known allergy information' else '' end AS 'known allergy information'"
            sql &= ", case when level_outcome = 1 then 'Level 0' when level_outcome = 2 then 'Level 1'"
            sql &= "when level_outcome = 3 then 'Level 2' when level_outcome = 4 then 'Level 3'"
            sql &= "when level_outcome = 5 then 'Level 4' when level_outcome = 6 then 'Level 5'"
            sql &= "when level_outcome = 7 then 'Level 6' when level_outcome = 1 then 'Level 7' end AS 'Level outcome'"
            ' sql &= ",drug_category"
            sql &= ",drug_group"
            sql &= ",'' AS drug_name"
            sql &= ",drug_name_right"
            sql &= ", case when chk_err_order =1 then 'Yes' else 'No' end AS 'Prescribing/ Ordering error'"
            sql &= ", case when chk_err_transcription  =1 then 'Yes' else 'No' end AS 'Transcription error'"
            sql &= ", case when chk_err_key  =1 then 'Yes' else 'No' end AS 'Key order error (order entry)'"
            sql &= ", case when chk_err_verify  =1 then 'Yes' else 'No' end AS 'Verification error'"
            sql &= ", case when chk_err_predis =1 then 'Yes' else 'No' end AS 'Pre-dispensing error'"
            sql &= ", case when chk_err_dispensing  =1 then 'Yes' else 'No' end AS 'Dispensing error'"
            sql &= ", case when chk_err_preadmin =1 then 'Yes' else 'No' end AS 'Pre-administration error'"
            sql &= ", case when chk_err_admin =1 then 'Yes' else 'No' end AS 'Administration error'"
            sql &= ", case when chk_err_monitor =1 then 'Yes' else 'No' end AS 'Monitoring error'"
            sql &= ", case when chk_order_type1 =1 then 'Yes' else 'No' end AS 'Standard orders'"
            sql &= ", case when chk_order_type2 =1 then 'Yes' else 'No' end AS 'Single orders'"
            sql &= ", case when chk_order_type3 =1 then 'Yes' else 'No' end AS 'verbal or Telelphone orders'"
            sql &= ", case when chk_order_type4 =1 then 'Yes' else 'No' end AS 'stat orders'"
            sql &= ", case when chk_order_type5 =1 then 'Yes' else 'No' end AS 'P.R.N orders'"
            sql &= ", case when chk_order_type6 =1 then 'Yes' else 'No' end AS 'Order for continues'"
            sql &= ", case when chk_order_type7 =1 then a.order_type else '' end AS 'Order for'"
            sql &= ",case when is_robot_product=1 then 'Yes' else 'No' end AS 'Robot product'"
            sql &= ",case when is_cpoe=1 then 'Yes' else 'No' end AS 'CPOE'"
            sql &= ",case when is_mar_error=1 then 'Yes' else 'No' end AS 'MAR error'"
            sql &= ",case when time_period = 1 then 'M Shift' when time_period = 2 then 'E Shift'"
            sql &= "when time_period = 3 then 'N Shift' end AS 'Time period'"
            sql &= ",period_hour AS 'Hour'"
            sql &= ",period_min AS 'Min'"
            sql &= ", '' AS Nursing "
            sql &= ", '' AS 'Nursing Period'  "
            sql &= ", '' AS Phamacist"
            sql &= ", '' AS 'Phamacist Period'  "
            sql &= ", '' AS APH"
            sql &= ", '' AS 'APH Period'  "
            sql &= ", '' AS Doctor"
            sql &= ", '' AS 'Doctor Period'  "

            sql &= ",case when chk_h1=1 then 'Yes' else 'No' end AS H1"
            sql &= ",case when chk_h2=1 then 'Yes' else 'No' end AS H2"
            sql &= ",case when chk_h3=1 then 'Yes' else 'No' end AS H3"
            sql &= ",case when chk_h4=1 then 'Yes' else 'No' end AS H4"
            sql &= ",case when chk_h5=1 then 'Yes' else 'No' end AS H5"
            sql &= ",case when chk_h6=1 then 'Yes' else 'No' end AS H6"
            sql &= ",case when chk_h7=1 then 'Yes' else 'No' end AS H7"
            sql &= ",case when chk_h8=1 then 'Yes' else 'No' end AS H8"
            sql &= ",case when chk_h9=1 then 'Yes' else 'No' end AS H9"
            sql &= ",case when chk_h10=1 then 'Yes' else 'No' end AS H10"
            sql &= ",case when chk_h11=1 then 'Yes' else 'No' end AS H11"
            sql &= ",h11_remark"
            sql &= ",case when chk_c1=1 then 'Yes' else 'No' end AS C1"
            sql &= ",case when chk_c2=1 then 'Yes' else 'No' end AS C2"
            sql &= ",case when chk_c3=1 then 'Yes' else 'No' end AS C3"
            sql &= ",case when chk_c4=1 then 'Yes' else 'No' end AS C4"
            sql &= ",case when chk_c5=1 then 'Yes' else 'No' end AS C5"
            sql &= ",case when chk_c6=1 then 'Yes' else 'No' end AS C6"
            sql &= ",case when chk_c7=1 then 'Yes' else 'No' end AS C7"
            sql &= ",case when chk_c8=1 then 'Yes' else 'No' end AS C8"
            sql &= ",c8_remark"
            sql &= ",case when chk_p1=1 then 'Yes' else 'No' end AS P1"
            sql &= ",case when chk_p2=1 then 'Yes' else 'No' end AS P2"
            sql &= ",case when chk_p3=1 then 'Yes' else 'No' end AS P3"
            sql &= ",case when chk_p4=1 then 'Yes' else 'No' end AS P4"
            sql &= ",case when chk_p5=1 then 'Yes' else 'No' end AS P5"
            sql &= ",case when chk_p6=1 then 'Yes' else 'No' end AS P6"
            sql &= ",case when chk_p7=1 then 'Yes' else 'No' end AS P7"
            sql &= ",case when chk_p8=1 then 'Yes' else 'No' end AS P8"
            sql &= ",case when chk_p9=1 then 'Yes' else 'No' end AS P9"
            sql &= ",case when chk_p10=1 then 'Yes' else 'No' end AS P10"
            sql &= ",case when chk_p11=1 then 'Yes' else 'No' end AS P11"
            sql &= ",case when chk_p12=1 then 'Yes' else 'No' end AS P12"
            sql &= ",case when chk_p13=1 then 'Yes' else 'No' end AS P13"
            sql &= ",case when chk_p14=1 then 'Yes' else 'No' end AS P14"
            sql &= ",case when chk_p15=1 then 'Yes' else 'No' end AS P15"
            sql &= ",case when chk_p16=1 then 'Yes' else 'No' end AS P16"
            sql &= ",case when chk_p17=1 then 'Yes' else 'No' end AS P17"
            sql &= ",case when chk_p18=1 then 'Yes' else 'No' end AS P18"
            sql &= ",case when chk_p19=1 then 'Yes' else 'No' end AS P19"
            sql &= ",case when chk_p20=1 then 'Yes' else 'No' end AS P20"
            sql &= ",case when chk_p21=1 then 'Yes' else 'No' end AS P21"
            sql &= ",p21_remark"
            sql &= ",case when chk_s1=1 then 'Yes' else 'No' end AS S1"
            sql &= ",case when chk_s2=1 then 'Yes' else 'No' end AS S2"
            sql &= ",case when chk_s3=1 then 'Yes' else 'No' end AS S3"
            sql &= ",case when chk_s4=1 then 'Yes' else 'No' end AS S4"
            sql &= ",case when chk_s5=1 then 'Yes' else 'No' end AS S5"
            sql &= ",case when chk_s6=1 then 'Yes' else 'No' end AS S6"
            sql &= ",case when chk_s7=1 then 'Yes' else 'No' end AS S7"
            sql &= ",case when chk_s8=1 then 'Yes' else 'No' end AS S8"
            sql &= ",case when chk_s9=1 then 'Yes' else 'No' end AS S9"
            sql &= ",s9_remark"
            sql &= ",case when chk_d1=1 then 'Yes' else 'No' end AS D1"
            sql &= ",case when chk_d2=1 then 'Yes' else 'No' end AS D2"
            sql &= ",case when chk_d3=1 then 'Yes' else 'No' end AS D3"
            sql &= ",case when chk_d4=1 then 'Yes' else 'No' end AS D4"
            sql &= ",case when chk_d5=1 then 'Yes' else 'No' end AS D5"
            sql &= ",case when chk_d6=1 then 'Yes' else 'No' end AS D6"
            sql &= ",d6_remark"
            sql &= ",case when chk_m1=1 then 'Yes' else 'No' end AS M1"
            sql &= ",case when chk_m2=1 then 'Yes' else 'No' end AS M2"
            sql &= ",m1_remark"
            sql &= ",m2_remark"

            sql &= " , 'No' AS 'Personal / Human Error' "
            sql &= " , 'No' AS 'Communication Error' "
            sql &= " , 'No' AS 'System Error' "
            sql &= " , 'No' AS 'Equipment Error' "
            sql &= " , 'No' AS 'Enviroment' "
            sql &= " , 'No' AS 'Poor Practice Habit' "
            sql &= " , 'No' AS 'Others' "
            sql &= " , 'No' AS 'Patient''s factor' "
            sql &= " , '' AS root_cause , c.hn  "
            sql &= " FROM ir_med_tab a INNER JOIN ir_trans_list b ON b.ir_id = a.ir_id INNER JOIN ir_detail_tab c ON b.ir_id = c.ir_id"
            sql &= " INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            sql &= " LEFT OUTER JOIN  ir_topic_grand t1 ON d.grand_topic = t1.grand_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic t2 ON d.topic = t2.ir_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub t3 ON d.subtopic1 = t3.ir_subtopic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub2 t4 ON d.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON d.subtopic3 = t5.ir_subtopic3_id "
            sql &= " WHERE ISNULL(b.is_cancel,0) = 0 And ISNULL(b.is_delete,0) = 0  And b.status_id > 2 AND b.form_id = 2 "
            sql &= " AND b.ir_id = " & irId
            '   Response.Write(sql)


            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            If ds.Tables("t1").Rows.Count > 0 Then
                CType(cl.FindControl("lblmedserioustype1"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong time").ToString & " "
                CType(cl.FindControl("lblmedserioustype1"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong route").ToString & " "
                CType(cl.FindControl("lblmedserioustype1"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong date").ToString & " "
                CType(cl.FindControl("lblmedserioustype1"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong rate").ToString & " "
                CType(cl.FindControl("lblmedserioustype1"), Label).Text &= ds.Tables("t1").Rows(0)("Omission").ToString & " "
                CType(cl.FindControl("lblmedserioustype1"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong label").ToString & " "
                CType(cl.FindControl("lblmedserioustype1"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong form").ToString & " "
                CType(cl.FindControl("lblmedserioustype2"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong dose").ToString & " "
                CType(cl.FindControl("lblmedserioustype2"), Label).Text &= ds.Tables("t1").Rows(0)("Extra dose").ToString & " "
                CType(cl.FindControl("lblmedserioustype2"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong IV").ToString & " "
                CType(cl.FindControl("lblmedserioustype2"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong deteriorate").ToString & " "
                CType(cl.FindControl("lblmedserioustype2"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong drug preparation").ToString & " "
                CType(cl.FindControl("lblmedserioustype2"), Label).Text &= ds.Tables("t1").Rows(0)("Unauthorized drug error").ToString & " "
                CType(cl.FindControl("lblmedserioustype2"), Label).Text &= ds.Tables("t1").Rows(0)("Duplicate drug").ToString & " "
                CType(cl.FindControl("lblmedserioustype2"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong formula").ToString & " "
                CType(cl.FindControl("lblmedserioustype2"), Label).Text &= ds.Tables("t1").Rows(0)("wrong quantity").ToString & " "
                CType(cl.FindControl("lblmedserioustype3"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong drug").ToString & " "
                CType(cl.FindControl("lblmedserioustype3"), Label).Text &= ds.Tables("t1").Rows(0)("Wrong patient").ToString & " "
                CType(cl.FindControl("lblmedserioustype3"), Label).Text &= ds.Tables("t1").Rows(0)("known allergy information").ToString & " "

                CType(cl.FindControl("lblmedlevelofseverity"), Label).Text = ds.Tables("t1").Rows(0)("Level outcome").ToString

                CType(cl.FindControl("lbldrugrightname"), Label).Text = ds.Tables("t1").Rows(0)("drug_name_right").ToString
                '  CType(cl.FindControl("lbldrugrightgroup"), Label).Text = ds.Tables("t1").Rows(0)("drug_wrong_name").ToString
                ' CType(cl.FindControl("lbldrugwrongname"), Label).Text = ds.Tables("t2").Rows(0)("drug_wrong_name").ToString
                '  CType(cl.FindControl("lbldrugwrongname"), Label).Text = ds.Tables("t2").Rows(0)("drug_wrong_name").ToString

            End If


            sql = "SELECT * FROM ir_med_tab_drug WHERE ir_id = " & irId
            ds = conn.getDataSetForTransaction(sql, "t2")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t2").Rows.Count > 0 Then
                CType(cl.FindControl("lbldrugwrongname"), Label).Text = ds.Tables("t2").Rows(0)("drug_wrong_name").ToString
                CType(cl.FindControl("lbldrugwronggroup"), Label).Text = ds.Tables("t2").Rows(0)("drug_group").ToString
                CType(cl.FindControl("lbldrugwronglasa"), Label).Text = ds.Tables("t2").Rows(0)("lasa_name").ToString
                CType(cl.FindControl("lbldrugwronghighalert"), Label).Text = ds.Tables("t2").Rows(0)("chk_alert").ToString
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindFall()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try

            sql &= "SELECT  a.ir_id , 'IR' + cast(c.ir_no as varchar) AS 'IR No.'"
            sql &= ", case when whofall_type = 1 then 'Patient' when whofall_type = 2 then 'Relative' "
            sql &= "when whofall_type = 3 then 'Visitor'  end AS 'Who falled and age'"
            sql &= ",whofall_age AS age"
            sql &= ",case when fall_type = 1 then 'Anticipated physiological' when  fall_type = 2 then 'Unanticipated physiological' "
            sql &= " when fall_type = 3 then 'Accidental' else '' end AS 'Type of fall' "
            sql &= " ,case when period_fall = 1 then 'M shift' when period_fall = 2 then 'E shift '"
            sql &= " when period_fall = 3 then 'N shift' else '' end AS 'Time of fall'"
            sql &= " ,period_hour AS 'hour'"
            sql &= " ,period_min AS 'min'"
            sql &= " ,case when activity_at_fall = 1 then 'Transferring into or out of bed' when activity_at_fall = 2 then 'Transferring in or out of chair'"
            sql &= " when activity_at_fall = 3 then 'Toileting' when activity_at_fall = 4 then 'Moving'"
            sql &= " when activity_at_fall = 5 then 'Walking' when activity_at_fall = 6 then 'Rehabilitation'"
            sql &= " when activity_at_fall = 7 then 'Sitting in a chair' when activity_at_fall = 8 then 'Sitting in a commode'"
            sql &= " when activity_at_fall = 9 then 'Sitting in a wheelchair' when activity_at_fall = 10 then 'Sitting in a stretcher'"
            sql &= " when activity_at_fall = 11 then 'Other' else '' end AS 'Activity at Time of fall'"
            sql &= " ,activity_remark"
            sql &= " ,case when a.location = 1 then 'Patient''s room' when a.location = 2 then 'Patient''s toilet'"
            sql &= " when a.location = 3 then 'Public toilet' when a.location = 4 then 'Public area '"
            sql &= " when a.location = 5 then 'Other' else '' end AS 'Location'"
            sql &= " ,location_remark"
            sql &= " ,case when is_renovation = 1 then 'Yes' else 'No' end AS 'Renovation area'"
            sql &= " ,case when is_has_assist = 1 then 'Yes' else 'No' end AS 'Having assistant during fall'"
            sql &= " ,case when vital_flag = 1 then 'Normal' when vital_flag =0 then 'Abnormal' else '' end AS 'Post Fall : Vital sign/Neuro sign'"
            sql &= " ,vital_remark"
            sql &= " ,case when is_examination = 1 then 'Yes' when is_examination = 0 then 'No' else '' end AS 'Examination conducted'"
            sql &= " ,exam_doctor"
            sql &= " ,date_exam"
            sql &= " ,date_exam_ts"
            sql &= " ,treatment_detail"
            sql &= " ,refuse_detail"

            sql &= ", case when chk_alzheimer = 1 or chk_sedative = 1 or chk_analgesic = 1  or chk_diuretic = 1 or chk_beta = 1  or chk_laxative = 1  or chk_antiepil= 1  "
            sql &= " or chk_narcotic = 1 or chk_benzo = 1 or chk_other1 = 1 "
            sql &= " then 'Yes' else 'No' end AS 'Medication last 24 hrs' "

            sql &= ", case when chk_pt1 = 1 or chk_pt2 = 1 or chk_pt3 = 1  or chk_pt4 = 1 or chk_pt5 = 1  or chk_pt5 = 1  or chk_pt6= 1  "
            sql &= " or chk_pt7 = 1 or chk_pt8 = 1 or chk_pt9 = 1 or chk_pt10 = 1  or chk_pt11 = 1  or chk_pt12 = 1 or chk_pt13 = 1 "
            sql &= " or chk_pt14 = 1 then 'Yes' else 'No' end AS 'Patients Factor'"

            sql &= " ,case when chk_alzheimer = 1 then 'Yes' else 'No' end AS 'Alzheimer''s drug'"
            sql &= " ,txt_alzheimer"
            sql &= " ,case when chk_sedative = 1 then 'Yes' else 'No' end AS 'Sedative / Hypnotics'"
            sql &= " ,txt_sedative"
            sql &= " ,case when chk_analgesic = 1 then 'Yes' else 'No' end AS 'Analgesic / muscle relaxant agents'"
            sql &= " ,txt_analgesic"
            sql &= " ,case when chk_diuretic = 1 then 'Yes' else 'No' end AS 'Diuretics'"
            sql &= " ,txt_diuretic"
            sql &= " ,case when chk_beta = 1 then 'Yes' else 'No' end AS 'Anti HT / beta-blocker'"
            sql &= " ,txt_beta"
            sql &= " ,case when chk_laxative = 1 then 'Yes' else 'No' end AS 'Laxatives'"
            sql &= " ,txt_laxative"
            sql &= " ,case when chk_antiepil = 1 then 'Yes' else 'No' end AS 'Antiepileptic'"
            sql &= " ,txt_antiepil"
            sql &= " ,case when chk_narcotic = 1 then 'Yes' else 'No' end AS 'Narcotic'"
            sql &= " ,txt_narcotic"
            sql &= " ,case when chk_benzo = 1 then 'Yes' else 'No' end AS 'Benzodiazepines'"
            sql &= "  ,txt_benzo"
            sql &= " ,case when chk_other1 = 1 then 'Yes' else 'No' end AS 'Other'"
            sql &= " ,txt_other1"

            sql &= " ,case when chk_pt1 = 1 then 'Yes' else 'No' end AS 'Mobility impairment'"
            sql &= " ,case when chk_pt2 = 1 then 'Yes' else 'No' end AS 'Poor eyesight'"
            sql &= " ,case when chk_pt3 = 1 then 'Yes' else 'No' end AS 'Altered elimination'"
            sql &= " ,case when chk_pt4 = 1 then 'Yes' else 'No' end AS 'Anemia'"
            sql &= " ,case when chk_pt5 = 1 then 'Yes' else 'No' end AS 'Old age > 60 yrs'"
            sql &= " ,case when chk_pt6 = 1 then 'Yes' else 'No' end AS 'Dizziness/ Vertigo'"
            sql &= " ,case when chk_pt7 = 1 then 'Yes' else 'No' end AS 'Neoplasm'"
            sql &= " ,case when chk_pt8 = 1 then 'Yes' else 'No' end AS 'Younger than 12 yrs'"
            sql &= " ,case when chk_pt9 = 1 then 'Yes' else 'No' end AS 'Implusive/ Noncompliance'"
            sql &= " ,case when chk_pt10 = 1 then 'Yes' else 'No' end AS 'CHF'"
            sql &= " ,case when chk_pt11 = 1 then 'Yes' else 'No' end AS 'Postural Hypotension'"
            sql &= " ,case when chk_pt12 = 1 then 'Yes' else 'No' end AS 'Not cooperation'"
            sql &= " ,case when chk_pt13 = 1 then 'Yes' else 'No' end AS 'CVA / Stroke'"
            sql &= " ,case when chk_pt14 = 1 then 'Yes' else 'No' end AS 'Other'"
            sql &= " ,pt_other_remark"

            sql &= " ,post_procedure AS 'Post operative / Procedure'"
            sql &= " ,case when type_anesthesia = 1 then 'GA' when type_anesthesia = 2 then 'SB'"
            sql &= " when type_anesthesia = 3 then 'EB' when type_anesthesia = 4 then 'IV'"
            sql &= " when type_anesthesia = 5 then 'LA' else '' end AS 'Type of Anesthesia'"

            sql &= ", case when chk_rn_care1 = 1 or chk_rn_care2 = 1 or chk_rn_care3 = 1  or chk_rn_care4 = 1 or chk_rn_care5 = 1  or chk_rn_care6 = 1  or chk_rn_care7= 1  "
            sql &= " or chk_rn_care8 = 1  "
            sql &= " then 'Yes' else 'No' end AS 'Nursing' "
            sql &= " , chk_rn_care1 "
            sql &= " ,case when chk_rn_care1 = 1 then 'Yes' else 'No' end AS 'Mobility assisting high risk patient during transferation'"
            sql &= " ,case when chk_rn_care2 = 1 then 'Yes' else 'No' end AS 'Provide assistance for toileting every time of nursing care intervention'"
            sql &= " ,case when chk_rn_care3 = 1 then 'Yes' else 'No' end AS 'Reviewed list of medication by pharmacist'"
            sql &= " ,case when chk_rn_care4 = 1 then 'Yes' else 'No' end AS 'Having assistance in toilet or sitting on commode'"
            sql &= " ,case when chk_rn_care5 = 1 then 'Yes' else 'No' end AS 'Provide toileting assistance every 2 hr or depends on patient''s condition'"
            sql &= " ,case when chk_rn_care6 = 1 then 'Yes' else 'No' end AS 'Position patient in easily observable area'"
            sql &= " ,case when chk_rn_care7 = 1 then 'Yes' else 'No' end AS 'Consider having sitter at all time'"
            sql &= " ,case when chk_rn_care8 = 1 then 'Yes' else 'No' end AS 'Other'"
            sql &= " ,rn_care_remark"

            sql &= ", case when chk_equip1 = 1 or chk_equip2 = 1 or chk_equip3 = 1  or chk_equip4 = 1 or chk_equip5 = 1  or chk_equip6 = 1  or chk_equip7= 1  "
            sql &= " or chk_equip8 = 1 or chk_equip9 = 1 or chk_equip10 = 1    "
            sql &= " then 'Yes' else 'No' end AS 'Equip' "

            sql &= "  ,case when chk_equip1 = 1 then 'Yes' else 'No' end AS 'Cane'"
            sql &= "  ,case when chk_equip2 = 1 then 'Yes' else 'No' end AS 'Crutch'"
            sql &= "  ,case when chk_equip3 = 1 then 'Yes' else 'No' end AS 'Consider bed alarm (Fall level 3 only)'"
            sql &= "  ,case when chk_equip4 = 1 then 'Yes' else 'No' end AS 'Furniture'"
            sql &= "  ,case when chk_equip5 = 1 then 'Yes' else 'No' end AS 'Use hospital slipper'"
            sql &= "   ,case when chk_equip6 = 1 then 'Yes' else 'No' end AS 'Walker'"
            sql &= "  ,case when chk_equip7 = 1 then 'Yes' else 'No' end AS 'Wheel of bed / Wheel chair has been locked'"
            sql &= "  ,case when chk_equip8 = 1 then 'Yes' else 'No' end AS 'Wheel chair / stretcher'"
            sql &= "  ,case when chk_equip9 = 1 then 'Yes' else 'No' end AS 'Using safety strapsor seat belt'"
            sql &= "  ,case when chk_equip10 = 1 then 'Yes' else 'No' end AS 'Selected suitable device'"

            sql &= ", case when chk_safe1 = 1 or chk_safe2 = 1 or chk_safe3 = 1  or chk_safe4 = 1 or chk_safe5 = 1  or chk_safe6 = 1  or chk_safe7= 1  "
            sql &= " or chk_safe8 = 1 or chk_safe9 = 1    "
            sql &= " then 'Yes' else 'No' end AS 'Safe' "
            sql &= "  ,case when chk_safe1 = 1 then 'Yes' else 'No' end AS '2 upper side rails up'"
            sql &= " ,case when chk_safe2 = 1 then 'Yes' else 'No' end AS 'Can reach to nurse call / Food tray'"
            sql &= " ,case when chk_safe3 = 1 then 'Yes' else 'No' end AS 'Decreasing risks : obstacles and clutter on bedside / floor'"
            sql &= " ,case when chk_safe4 = 1 then 'Yes' else 'No' end AS 'Suitable surface : dry floor'"
            sql &= " ,case when chk_safe5 = 1 then 'Yes' else 'No' end AS 'Alert Signage'"
            sql &= " ,case when chk_safe6 = 1 then 'Yes' else 'No' end AS 'Safety in kid zone'"
            sql &= " ,case when chk_safe7 = 1 then 'Yes' else 'No' end AS 'Light'"
            sql &= "  ,case when chk_safe8 = 1 then 'Yes' else 'No' end AS 'Low level of bed'"
            sql &= " ,case when chk_safe9 = 1 then 'Yes' else 'No' end AS 'Other'"
            sql &= " ,safe_remark"

            sql &= ", case when chk_inform1 = 1 or chk_inform2 = 1 or chk_inform3 = 1    "
            sql &= " then 'Yes' else 'No' end AS 'inform' "

            sql &= " ,case when chk_inform1 = 1 then 'Yes' else 'No' end AS 'Re-oriented confused patients'"
            sql &= " ,case when chk_inform2 = 1 then 'Yes' else 'No' end AS 'Educate / Instruct patient and relative about fall prevention'"
            sql &= " ,case when chk_inform3 = 1 then 'Yes' else 'No' end AS 'Orientating patient to bed area / ward facilities and not to get assistance'"
            sql &= " ,date_assess"
            sql &= " ,date_assess_ts"
            sql &= " ,assess_reason"

            sql &= ", case when chk_assess1 = 1 or chk_assess2 = 1 or chk_assess3 = 1 or chk_assess4 = 1 or chk_assess5 = 1    "
            sql &= " then 'Yes' else 'No' end AS 'assess' "

            sql &= " ,case when chk_assess1 = 1 then 'Yes' else 'No' end AS 'On admission to hospital'"
            sql &= " ,case when chk_assess2 = 1 then 'Yes' else 'No' end AS 'Post operative patient'"
            sql &= " ,case when chk_assess3 = 1 then 'Yes' else 'No' end AS 'On medicine effect / changed medicine orde'"
            sql &= " ,case when chk_assess4 = 1 then 'Yes' else 'No' end AS 'On OPD Nursing intervention'"
            sql &= " ,case when chk_assess5 = 1 then 'Yes' else 'No' end AS 'Other'"
            sql &= " ,assess_other_remark"
            sql &= " ,score_w1"
            sql &= " ,score_w2"
            sql &= " ,score_w3"
            sql &= " ,score_w4"
            sql &= " ,score_w5"
            sql &= " ,score_w6"
            sql &= " ,score_w7"
            sql &= "  ,score_w8"
            sql &= "  ,score_w9"
            sql &= " ,score_w10"
            sql &= "  ,score_w11"
            sql &= "  ,score_s1"
            sql &= "  ,score_s2"
            sql &= " ,score_s3"
            sql &= " ,score_s4"
            sql &= " ,score_s5"
            sql &= " ,score_s6"
            sql &= " ,score_s7"
            sql &= " ,score_s8"
            sql &= " ,score_s9"
            sql &= " ,score_s10"
            sql &= " ,score_s11"
            sql &= " ,ward_score AS 'Ward assessment score'"
            sql &= " ,manager_score AS 'Fall prevention specialist assessment/ Manager/ Supervisor/ Incharge score'"
            sql &= "  ,case when level_fall = 1 then  'Level 1' when level_fall = 2 then 'Level 2'"
            sql &= "  when level_fall = 3 then 'Level 3' when level_fall = 4 then 'Unknown' else '' end AS 'Risk level of fall'"
            sql &= "  ,case when severity_outcome = 1 then 'Level 0 (No any injuries)' when severity_outcome = 2 then 'Level 1'"
            sql &= "  when severity_outcome = 3 then 'Level 2' when severity_outcome = 4 then 'Level 3' "
            sql &= "   when severity_outcome = 4 then 'Level 4' else '' end AS 'Severity outcome'"
            sql &= " FROM ir_fall_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id"
            sql &= " INNER JOIN ir_detail_tab c ON a.ir_id = c.ir_id"
            sql &= " WHERE ISNULL(b.is_cancel,0) = 0 And ISNULL(b.is_delete,0) = 0  And b.status_id > 2 "
            sql &= " AND b.ir_id = " & irId
            '   Response.Write(sql)


            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count > 0 Then
                CType(cl.FindControl("lblwhofall"), Label).Text = ds.Tables("t1").Rows(0)("Who falled and age").ToString
                CType(cl.FindControl("lblfallage"), Label).Text = ds.Tables("t1").Rows(0)("age").ToString
                CType(cl.FindControl("lbltypeoffall"), Label).Text = ds.Tables("t1").Rows(0)("Type of fall").ToString
                CType(cl.FindControl("lbltimeoffall"), Label).Text = ds.Tables("t1").Rows(0)("Time of fall").ToString
                CType(cl.FindControl("lblactoffall"), Label).Text = ds.Tables("t1").Rows(0)("Activity at Time of fall").ToString
                CType(cl.FindControl("lblfalllocation"), Label).Text = ds.Tables("t1").Rows(0)("Location").ToString
                CType(cl.FindControl("lblasstfall"), Label).Text = ds.Tables("t1").Rows(0)("Having assistant during fall").ToString
                CType(cl.FindControl("lblpostfall"), Label).Text = ds.Tables("t1").Rows(0)("Post Fall : Vital sign/Neuro sign").ToString
                CType(cl.FindControl("lblfallexam"), Label).Text = ds.Tables("t1").Rows(0)("Examination conducted").ToString
                CType(cl.FindControl("lblfallexamby"), Label).Text = ds.Tables("t1").Rows(0)("exam_doctor").ToString
                CType(cl.FindControl("lblfallexamdate"), Label).Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_exam_ts").ToString)
                CType(cl.FindControl("lblfallexamtime"), Label).Text = ConvertTSTo(ds.Tables("t1").Rows(0)("date_exam_ts").ToString, "hour").PadLeft(2, "0") & ":" & ConvertTSTo(ds.Tables("t1").Rows(0)("date_exam_ts").ToString, "min").PadLeft(2, "0")
                CType(cl.FindControl("lblfalltx"), Label).Text = ds.Tables("t1").Rows(0)("treatment_detail").ToString
                CType(cl.FindControl("lblfallrisk1"), Label).Text = ds.Tables("t1").Rows(0)("Medication last 24 hrs").ToString
                CType(cl.FindControl("lblfallrisk2"), Label).Text = ds.Tables("t1").Rows(0)("Patients Factor").ToString
                CType(cl.FindControl("lblfallrisk3"), Label).Text = ds.Tables("t1").Rows(0)("Post operative / Procedure").ToString
                CType(cl.FindControl("lblfalltypeofanes"), Label).Text = ds.Tables("t1").Rows(0)("Type of Anesthesia").ToString
                CType(cl.FindControl("lblfallrisk4"), Label).Text = ds.Tables("t1").Rows(0)("Nursing").ToString

                CType(cl.FindControl("lblfallrisk5"), Label).Text = ds.Tables("t1").Rows(0)("Equip").ToString
                CType(cl.FindControl("lblfallrisk6"), Label).Text = ds.Tables("t1").Rows(0)("Safe").ToString
                CType(cl.FindControl("lblfallrisk7"), Label).Text = ds.Tables("t1").Rows(0)("inform").ToString
                CType(cl.FindControl("lblfallassesstime"), Label).Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_assess_ts").ToString) & " " & ConvertTSTo(ds.Tables("t1").Rows(0)("date_assess_ts").ToString, "hour").PadLeft(2, "0") & ":" & ConvertTSTo(ds.Tables("t1").Rows(0)("date_assess_ts").ToString, "min").PadLeft(2, "0")
                CType(cl.FindControl("lblfallassessreason"), Label).Text = ds.Tables("t1").Rows(0)("assess_reason").ToString
                CType(cl.FindControl("lblfallrisk8"), Label).Text = ds.Tables("t1").Rows(0)("assess").ToString

                CType(cl.FindControl("lblw1"), Label).Text = ds.Tables("t1").Rows(0)("score_w1").ToString
                CType(cl.FindControl("lblw2"), Label).Text = ds.Tables("t1").Rows(0)("score_w2").ToString
                CType(cl.FindControl("lblw3"), Label).Text = ds.Tables("t1").Rows(0)("score_w3").ToString
                CType(cl.FindControl("lblw4"), Label).Text = ds.Tables("t1").Rows(0)("score_w4").ToString
                CType(cl.FindControl("lblw5"), Label).Text = ds.Tables("t1").Rows(0)("score_w5").ToString
                CType(cl.FindControl("lblw6"), Label).Text = ds.Tables("t1").Rows(0)("score_w6").ToString
                CType(cl.FindControl("lblw7"), Label).Text = ds.Tables("t1").Rows(0)("score_w7").ToString
                CType(cl.FindControl("lblw8"), Label).Text = ds.Tables("t1").Rows(0)("score_w8").ToString
                CType(cl.FindControl("lblw9"), Label).Text = ds.Tables("t1").Rows(0)("score_w9").ToString
                CType(cl.FindControl("lblw10"), Label).Text = ds.Tables("t1").Rows(0)("score_w10").ToString
                CType(cl.FindControl("lblw11"), Label).Text = ds.Tables("t1").Rows(0)("score_w11").ToString

                CType(cl.FindControl("lblsp1"), Label).Text = ds.Tables("t1").Rows(0)("score_s1").ToString
                CType(cl.FindControl("lblsp2"), Label).Text = ds.Tables("t1").Rows(0)("score_s2").ToString
                CType(cl.FindControl("lblsp3"), Label).Text = ds.Tables("t1").Rows(0)("score_s3").ToString
                CType(cl.FindControl("lblsp4"), Label).Text = ds.Tables("t1").Rows(0)("score_s4").ToString
                CType(cl.FindControl("lblsp5"), Label).Text = ds.Tables("t1").Rows(0)("score_s5").ToString
                CType(cl.FindControl("lblsp6"), Label).Text = ds.Tables("t1").Rows(0)("score_s6").ToString
                CType(cl.FindControl("lblsp7"), Label).Text = ds.Tables("t1").Rows(0)("score_s7").ToString
                CType(cl.FindControl("lblsp8"), Label).Text = ds.Tables("t1").Rows(0)("score_s8").ToString
                CType(cl.FindControl("lblsp9"), Label).Text = ds.Tables("t1").Rows(0)("score_s9").ToString
                CType(cl.FindControl("lblsp10"), Label).Text = ds.Tables("t1").Rows(0)("score_s10").ToString
                CType(cl.FindControl("lblsp11"), Label).Text = ds.Tables("t1").Rows(0)("score_s11").ToString

                CType(cl.FindControl("lblfallrisklevel"), Label).Text = ds.Tables("t1").Rows(0)("Risk level of fall").ToString
                CType(cl.FindControl("lblfalloutcome"), Label).Text = ds.Tables("t1").Rows(0)("Severity outcome").ToString
            End If
         
            ' Response.Write(ds.Tables("t1").Rows(0)("Nursing").ToString)


            ' Response.Write(ds.Tables("t1").Rows(0)("Nursing").ToString)

            '  GridView1.DataSource = ds
            ' GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & " Fall ")
        End Try
    End Sub

    Sub bindPheb()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql &= "SELECT a.ir_id , 'IR' + cast(c.ir_no as varchar) AS 'IR NO.' ,case when infiltration_type = 1 then 'Stage 1' when infiltration_type = 2 then 'Stage 2'"
            sql &= " when infiltration_type = 3 then 'Stage 3' when infiltration_type = 4 then 'Stage 4' else ''"
            sql &= " end AS 'Infiltration'"
            sql &= " ,case when phlebitis_type = -1 then 'Stage 0  No symptom' when phlebitis_type = 1 then 'Stage 1  Erythema at access site with or without pain'"
            sql &= " when phlebitis_type = 2 then 'Stage 2  Pain at access site with erythema and/or edema' when phlebitis_type = 3 then 'Stage 3   Pain at access site with erythema, Streak formation, Palpable venous cord' "
            sql &= " when phlebitis_type = 4 then 'Stage 4 Pain at access site with erythema, Streak formation, Palpable venous cord >1 inch in length,  Purulent drainage' WHEN phlebitis_type = 5 THEN 'Stage 5'  else ''  end AS 'Phlebitis'"

            sql &= " ,case when chk_mechanical = 1 and chk_chemical=0 and chk_bacterial = 0  then 'Mechanical' "
            sql &= "  when chk_mechanical = 1 and chk_chemical=1 and chk_bacterial = 0  then 'Mechanical Chemical' "
            sql &= "  when chk_mechanical = 1 and chk_chemical=1 and chk_bacterial = 1  then 'Mechanical Chemical Bacterial' "
            sql &= "  when chk_mechanical = 0 and chk_chemical=1 and chk_bacterial = 0  then 'Chemical' "
            sql &= "  when chk_mechanical = 0 and chk_chemical=1 and chk_bacterial = 1  then 'Chemical Bacterial' "
            sql &= "  when chk_mechanical = 0 and chk_chemical=0 and chk_bacterial = 1  then 'Bacterial' "
            sql &= " end AS 'Type of Phlebitis' "

            sql &= " ,case when chk_mechanical = 1 then 'Yes' when chk_mechanical = 0 then 'No' else '' end AS 'Mechanical'"
            sql &= " ,case when chk_chemical = 1 then 'Yes' when chk_chemical = 0 then 'No' else '' end AS 'Chemical'"
            sql &= " ,case when chk_bacterial = 1 then 'Yes' when chk_bacterial = 0 then 'No' else '' end AS 'Bacterial'"

            sql &= " ,case when chk_jelco = 1 and chk_venflon=0 and chk_other_gauge = 0  then 'Jelco' "
            sql &= "  when chk_jelco = 1 and chk_venflon=1 and chk_other_gauge = 0  then 'Jelco venflon' "
            sql &= "  when chk_jelco = 1 and chk_venflon=1 and chk_other_gauge = 1  then 'Jelco venflon Other' "
            sql &= "  when chk_jelco = 0 and chk_venflon=1 and chk_other_gauge = 0  then 'venflon' "
            sql &= "  when chk_jelco = 0 and chk_venflon=1 and chk_other_gauge = 1  then 'venflon Other' "
            sql &= "  when chk_jelco = 0 and chk_venflon=0 and chk_other_gauge = 1  then 'Other' "
            sql &= " end AS 'Gauge' "
            sql &= " ,case when chk_jelco = 1 then 'Yes' when chk_jelco = 0 then 'No' else '' end AS 'Jelco'"
            sql &= " ,case when chk_venflon = 1 then 'Yes' when chk_venflon = 0 then 'No' else '' end AS 'venflon'"
            sql &= " ,case when chk_other_gauge = 1 then 'Yes' when chk_other_gauge = 0 then 'No' else '' end AS 'Other'"

            sql &= " ,jelco_remark"
            sql &= " ,venflon_remark"
            sql &= " ,other_gauge_remark"

            sql &= " ,case when chk_iv_dorsal = 1 and chk_iv_cep=0 and chk_iv_basi = 0  then 'Dorsal' "
            sql &= "  when chk_iv_dorsal = 1 and chk_iv_cep=1 and chk_iv_basi = 0  then 'Dorsal Cephalic' "
            sql &= "  when chk_iv_dorsal = 1 and chk_iv_cep=1 and chk_iv_basi = 1  then 'Dorsal Cephalic Basilic' "
            sql &= "  when chk_iv_dorsal = 0 and chk_iv_cep=1 and chk_iv_basi = 0  then 'Cephalic' "
            sql &= "  when chk_iv_dorsal = 0 and chk_iv_cep=1 and chk_iv_basi = 1  then 'Cephalic Basilic' "
            sql &= "  when chk_iv_dorsal = 0 and chk_iv_cep=0 and chk_iv_basi = 1  then 'Basilic' "
            sql &= " end AS 'IV site' "
            sql &= " ,case when chk_iv_dorsal = 1 then 'Yes' when chk_iv_dorsal = 0 then 'No' else '' end AS 'Dorsal metacarpal vein'"
            sql &= " ,case when chk_iv_cep = 1 then 'Yes' when chk_iv_cep = 0 then 'No' else '' end AS 'Cephalic vein'"
            sql &= " ,case when chk_iv_basi = 1 then 'Yes' when chk_iv_basi = 0 then 'No' else '' end AS 'Basilic vein'"
            sql &= " ,case when chk_iv_other = 1 then 'Yes' when chk_iv_other = 0 then 'No' else '' end AS 'Other' "
            sql &= " ,iv_other_remark"
            sql &= " ,fluid_comment as 'IV Fluid'"

            sql &= " , chk_conc , chk_chemo , chk_med , chk_anti , chk_ivmed_other "
            sql &= " ,case when chk_conc = 1 then 'Yes' when chk_conc = 0 then 'No' else '' end AS 'High Concentration' "
            sql &= " ,case when chk_chemo = 1 then 'Yes' when chk_chemo = 0 then 'No' else '' end AS 'On Chemotherapy' "
            sql &= " ,case when chk_med = 1 then 'Yes' when chk_med = 0 then 'No' else '' end AS 'Circulatory Medicine' "
            sql &= " ,case when chk_anti = 1 then 'Yes' when chk_anti = 0 then 'No' else '' end AS 'Antibiotic' "
            sql &= " ,case when chk_ivmed_other = 1 then 'Yes' when chk_ivmed_other = 0 then 'No' else '' end AS 'Other' "
            sql &= " ,ivmed_other_remark"

            sql &= ",case when duration = 1 then '< 24 hrs'  when duration = 2 then '24-48 hrs'"
            sql &= " when duration = 3 then '48-72 hrs'  when duration = 4 then '72-96 hrs' else '' end AS 'Duration of catherter in place'"
            sql &= " ,case when pain = 1 then 'No pain (pain score 0)' when pain = 2 then 'Mild (pain score 1-4)'"
            sql &= " when pain = 3 then 'Moderate (pain score 5-7)' when pain = 4 then 'Severe (pain score 7-10)'"
            sql &= " else '' end AS 'Pain'"
            sql &= ",case when redness = 1 then 'Mild' when redness = 2 then 'Moderate'"
            sql &= " when redness = 3 then 'Severe'  else '' end AS 'Redness'"
            sql &= ",case when erythema = 1 then 'Yes' when erythema = 2 then 'No' else '' end AS 'Erythema'"
            sql &= ",case when swelling = 1 then 'Yes' when swelling = 2 then 'No' else '' end AS 'Swelling'"
            sql &= ",case when induration = 1 then 'Yes' when induration = 2 then 'No' else '' end AS 'Induration'"
            sql &= " ,case when pvc = 1 then 'Yes' when pvc = 2 then 'No' else '' end AS 'Palpable venous cord'"
            sql &= ",case when fever = 1 then 'Yes' when fever = 2 then 'No' else '' end AS 'Fever'"
            sql &= ",case when inf_pain = 1 then 'No pain (pain score 0)' when inf_pain = 2 then 'Mild (pain score 1-4)'"
            sql &= " when inf_pain = 3 then 'Moderate (pain score 5-7)' when inf_pain = 4 then 'Severe (pain score 7-10)'"
            sql &= " else '' end AS 'infiltration Pain'"
            sql &= ",case when inf_edema = 1 then '2.5 cms in any direction' when inf_edema = 2 then '2.5 cms - 15 cms in any direction'"
            sql &= " when inf_edema = 3 then '15 cms in any direction' "
            sql &= " else '' end AS 'infiltration Edema'"
            sql &= ",case when med_history = 1 then 'Yes' when med_history = 2 then 'No' else '' end AS 'Medical History'"
            sql &= " ,case when chk_immune = 1 then 'Yes' when chk_immune = 0 then 'No' else '' end AS 'Immune suppression'"
            sql &= " ,case when chk_dm = 1 then 'Yes' when chk_dm = 0 then 'No' else '' end AS 'DM'"
            sql &= ",case when chk_obesity = 1 then 'Yes' when chk_obesity = 0 then 'No' else '' end AS 'Obesity'"
            sql &= ",case when chk_coagulo = 1 then 'Yes' when chk_coagulo = 0 then 'No' else '' end AS 'Coagulopathy'"
            sql &= " ,case when chk_cva = 1 then 'Yes' when chk_cva = 0 then 'No' else '' end AS 'CVA'"
            sql &= " ,case when chk_cancer = 1 then 'Yes' when chk_cancer = 0 then 'No' else '' end AS 'Cancer'"
            sql &= ",case when chk_mal = 1 then 'Yes' when chk_mal = 0 then 'No' else '' end AS 'Malnourished'"
            sql &= ",case when chk_bony = 1 then 'Yes' when chk_bony = 0 then 'No' else '' end AS 'Bony Trauma'"
            sql &= " ,case when chk_history_other = 1 then 'Yes' when chk_history_other = 0 then 'No' else '' end AS 'Other'"
            sql &= " ,history_other_remark"
            sql &= "  FROM ir_phlebitis_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id INNER JOIN ir_detail_tab c ON a.ir_id = c.ir_id"
            sql &= " WHERE ISNULL(b.is_cancel,0) = 0 And ISNULL(b.is_delete,0) = 0  "
            sql &= " AND b.ir_id = " & irId

            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count > 0 Then
                'Response.Write("xxxx " & ds.Tables("t1").Rows(0)("Phlebitis").ToStringsql)
                CType(cl.FindControl("lblinfiltration"), Label).Text = ds.Tables("t1").Rows(0)("Infiltration").ToString
                CType(cl.FindControl("lblinfiltrationsymptom1"), Label).Text = ds.Tables("t1").Rows(0)("infiltration Pain").ToString
                CType(cl.FindControl("lblinfiltrationsymptom2"), Label).Text = ds.Tables("t1").Rows(0)("infiltration Edema").ToString
                CType(cl.FindControl("lblphlebitis"), Label).Text = ds.Tables("t1").Rows(0)("Phlebitis").ToString
                CType(cl.FindControl("lblphlebitissymptom1"), Label).Text = ds.Tables("t1").Rows(0)("Pain").ToString
                CType(cl.FindControl("lblphlebitissymptom2"), Label).Text = ds.Tables("t1").Rows(0)("Redness").ToString
                CType(cl.FindControl("lblphlebitissymptom3"), Label).Text = ds.Tables("t1").Rows(0)("Erythema").ToString
                CType(cl.FindControl("lblphlebitissymptom4"), Label).Text = ds.Tables("t1").Rows(0)("Swelling").ToString
                CType(cl.FindControl("lblphlebitissymptom5"), Label).Text = ds.Tables("t1").Rows(0)("Induration").ToString
                CType(cl.FindControl("lblphlebitissymptom6"), Label).Text = ds.Tables("t1").Rows(0)("Palpable venous cord").ToString
                CType(cl.FindControl("lblphlebitissymptom7"), Label).Text = ds.Tables("t1").Rows(0)("Fever").ToString
                CType(cl.FindControl("lbltypeofphlebitis"), Label).Text = ds.Tables("t1").Rows(0)("Type of Phlebitis").ToString
                CType(cl.FindControl("lblguageno"), Label).Text = ds.Tables("t1").Rows(0)("Gauge").ToString
                CType(cl.FindControl("lblivsite"), Label).Text = ds.Tables("t1").Rows(0)("IV site").ToString
                CType(cl.FindControl("lblivfluid"), Label).Text = ds.Tables("t1").Rows(0)("IV Fluid").ToString

                If ds.Tables("t1").Rows(0)("chk_conc").ToString = "1" Then
                    CType(cl.FindControl("lblivmed"), Label).Text &= " High Concentration"
                End If
                If ds.Tables("t1").Rows(0)("chk_chemo").ToString = "1" Then
                    CType(cl.FindControl("lblivmed"), Label).Text &= " On Chemotherapy"
                End If
                If ds.Tables("t1").Rows(0)("chk_med").ToString = "1" Then
                    CType(cl.FindControl("lblivmed"), Label).Text &= " Circulatory Medicine"
                End If
                If ds.Tables("t1").Rows(0)("chk_anti").ToString = "1" Then
                    CType(cl.FindControl("lblivmed"), Label).Text &= " Antibiotic"
                End If
                If ds.Tables("t1").Rows(0)("chk_ivmed_other").ToString = "1" Then
                    CType(cl.FindControl("lblivmed"), Label).Text &= " Other"
                End If

                CType(cl.FindControl("lblcatherter"), Label).Text = ds.Tables("t1").Rows(0)("Duration of catherter in place").ToString
                CType(cl.FindControl("lblmedhx"), Label).Text = ds.Tables("t1").Rows(0)("Medical History").ToString

            End If
           

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindPressure()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql &= "SELECT  a.ir_id , 'IR' + cast(c.ir_no as varchar) AS 'IR NO.' "
            sql &= ", c.pt_name , c.service_type AS patient_type  , c.describe AS 'Detail' , d.action_detail AS 'Corrective Action'  "
            sql &= ", t1.grand_topic_name , t2.topic_name , t3.subtopic_name "

            sql &= " ,case when acquire_type = 1 then 'Hospital acquired' when acquire_type = 2 then 'Home acquired' "
            sql &= "when acquire_type = 3 then 'Other hospital acquired' end AS 'type of Pressure' "
            sql &= ",case when stage_type = 1 then 'Stage I' when stage_type = 2 then 'Stage II' "
            sql &= "when stage_type = 3 then 'Stage III' when stage_type =  4 then 'Stage IV' "
            sql &= " when stage_type = 5 then 'Unstageable' when stage_type =  6 then 'Suspected Deep Tissue Injury' end AS 'stage of Pressure ulcer'"
            sql &= ",case when  location_type = 1 then 'Coccyx' when  location_type = 2 then 'Buttock : ' + buttock_type "
            sql &= " when  location_type = 3 then '' when  location_type = 4 then '' end as 'location' "
            sql &= ",buttock_type as Buttock ,hip_type as Hip ,other_remark as other_location "
            sql &= ",case when admission_type = 1 then 'yes' when admission_type = 2 then 'no' + ' ' + scale_no_detail else '' end as 'On admission, was a skin inspection documented' "
            sql &= ",admission_detail as 'Braden scale was' "
            sql &= ",case when scale_type = 1 then 'yes' when scale_type = 2 then 'no' else '' end  as 'When was the first Braden scale lower or equal than 19, were there any preventive intervention implemented'"
            sql &= " ,case when chk_scale1 = 1 then 'yes'  when chk_scale1 = 0 then 'no' else '' end as 'Nursing Notes documented'"
            sql &= ",case when chk_scale2 = 1 then 'yes'  when chk_scale2 = 0 then 'no' else '' end as 'Education to Patient and relative'"
            sql &= " ,case when chk_scale3 = 1 then 'yes'  when chk_scale3 = 0 then 'no' else '' end as 'Provided toilet worksheet'"
            sql &= ",case when chk_scale4 = 1 then 'yes'  when chk_scale4 = 0 then 'no' else '' end as 'Provided airmattress'"
            sql &= ",case when chk_scale5 = 1 then 'yes'  when chk_scale5 = 0 then 'no' else '' end as 'Skin barrier'"
            sql &= " ,case when chk_scale6 = 1 then 'yes'  when chk_scale6 = 0 then 'no' else '' end as 'Skin care (Keep clean and dry)'"
            sql &= " ,case when chk_scale7 = 1 then 'yes'  when chk_scale7 = 0 then 'no' else '' end as 'Pressure redistribution device'"
            sql &= ",case when chk_scale8 = 1 then 'yes'  when chk_scale8 = 0 then 'no' else '' end as 'Repositioning'"
            sql &= " ,case when chk_scale9 = 1 then 'yes'  when chk_scale9 = 0 then 'no' else '' end as 'Nutrition support'"
            sql &= " ,case when chk_scale10 = 1 then 'yes'  when chk_scale10 = 0 then 'no' else '' end as 'Notify Wound care coordinator'"
            sql &= ",case when chk_scale11 = 1 then 'yes'  when chk_scale11 = 0 then 'no' else '' end as 'Other'"
            sql &= " ,scale_other_remark"
            sql &= ", risk_type "
            sql &= ",case when risk_type = 1 then 'Intrinsic factors'  when risk_type = 2 then 'Extrinsic factors'  else '' end as 'Risk factors'"
            sql &= " ,case when chk_risk1 = 1 then 'yes' else 'no' end as 'Age'"
            sql &= " ,case when chk_risk2 = 1 then 'yes' else 'no' end as 'Imobility'"
            sql &= ",case when chk_risk3 = 1 then 'yes' else 'no' end as 'Incontinence'"
            sql &= " ,case when chk_risk4 = 1 then 'yes' else 'no' end as 'Poor nutrition status'"
            sql &= ",case when chk_risk5 = 1 then 'yes' else 'no' end as 'Poor sensory perception'"
            sql &= ",case when chk_risk6 = 1 then 'yes' else 'no' end as 'Being assisted to change position'"
            sql &= ",case when chk_risk7 = 1 then 'yes' else 'no' end as 'Increase Temperature'"
            sql &= ",case when chk_risk8 = 1 then 'yes' else 'no' end as 'Limited activity'"
            sql &= ",case when chk_risk9 = 1 then 'yes' else 'no' end as 'Shear'"
            sql &= ",case when chk_risk10 = 1 then 'yes' else 'no' end as 'Friction'"
            sql &= ",case when chk_risk11 = 1 then 'yes' else 'no' end as 'Transferring'"
            sql &= " ,case when chk_risk12 = 1 then 'yes' else 'no' end as 'Tissue load from positioning' "
            sql &= ",case when device_type = 1 then 'yes'  when device_type = 2 then 'no'  else '' end as 'Was the use of device or appliance involved in the development or asvancement of the pressure ulcer'"
            sql &= ",case when chk_device1 = 1 then 'yes' else 'no' end as 'Anti-embolic device'"
            sql &= ",case when chk_device2 = 1 then 'yes' else 'no' end as 'Intraoperative positioning device'"
            sql &= ",case when chk_device3 = 1 then 'yes' else 'no' end as 'Orthopedic appliance'"
            sql &= ",case when chk_device4 = 1 then 'yes' else 'no' end as 'Oxygen delivery device'"
            sql &= ",case when chk_device5 = 1 then 'yes' else 'no' end as 'Tube'"
            sql &= ",case when chk_device51 = 1 then 'yes' else 'no' end as 'Endotracheal'"
            sql &= ",case when chk_device52 = 1 then 'yes' else 'no' end as 'Gastrostomy'"
            sql &= ",case when chk_device53 = 1 then 'yes' else 'no' end as 'Nasogastric'"
            sql &= ",case when chk_device54 = 1 then 'yes' else 'no' end as 'Tracheostomy'"
            sql &= ",case when chk_device55 = 1 then 'yes' else 'no' end as 'Indwelling urinary catheter'"
            sql &= ",case when chk_device56 = 1 then 'yes' else 'no' end as 'Other'"
            sql &= ",device_other_remark"
            sql &= ",case when sensory_type = 1 then 'Completely Limited' when sensory_type = 2 then 'Very Limited' "
            sql &= " when sensory_type = 3 then 'Slightly Limited'  when sensory_type = 4 then 'No Impairment'  else '' end  as 'Sensory Perception'"
            sql &= ",case when moisture_type = 1 then 'Constantly Moist' when moisture_type = 2 then 'Very Moist' "
            sql &= " when moisture_type = 3 then 'Occasionally Moist'  when moisture_type = 4 then 'Rarely Moist'  else '' end  as 'Moisture'"
            sql &= " ,case when activity_type = 1 then 'Bedfast' when activity_type = 2 then 'Chairfast' "
            sql &= "when activity_type = 3 then 'Walks Occasionally'  when activity_type = 4 then 'Walks Frequently'  else '' end  as 'Activity'"
            sql &= " ,case when activity_type = 1 then 'Completely Immobile' when activity_type = 2 then 'Very Limited' "
            sql &= "when activity_type = 3 then 'Sligtly Limited'  when activity_type = 4 then 'No Limitation'  else '' end  as 'Mobility'"
            sql &= ",case when nutrition_type = 1 then 'Very Poor' when nutrition_type = 2 then 'Probably Inadequate' "
            sql &= "when nutrition_type = 3 then 'Adequate'  when nutrition_type = 4 then 'Excellent'  else '' end  as 'Nutrition'"
            sql &= ",case when friction_type = 1 then 'Problem' when friction_type = 2 then 'Potential Problem' "
            sql &= "when friction_type = 3 then ' No Apparent Proble'   else '' end  as 'Friction & Shear'"
            sql &= " ,score"
            sql &= " FROM ir_pressure_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id INNER JOIN ir_detail_tab c ON a.ir_id = c.ir_id "
            sql &= "INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            sql &= "LEFT OUTER JOIN  ir_topic_grand t1 ON d.grand_topic = t1.grand_topic_id "
            sql &= "LEFT OUTER JOIN ir_topic t2 ON d.topic = t2.ir_topic_id "
            sql &= "LEFT OUTER JOIN ir_topic_sub t3 ON d.subtopic1 = t3.ir_subtopic_id "
            sql &= "LEFT OUTER JOIN ir_topic_sub2 t4 ON d.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON d.subtopic3 = t5.ir_subtopic3_id "
            sql &= " WHERE ISNULL(b.is_cancel,0) = 0 And ISNULL(b.is_delete,0) = 0  And b.status_id > 2 And b.form_id = 6 "
            sql &= " AND b.ir_id = " & irId
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count > 0 Then
                CType(cl.FindControl("lbltypeofpressure"), Label).Text = ds.Tables("t1").Rows(0)("type of Pressure").ToString
                CType(cl.FindControl("lblstageofpressure"), Label).Text = ds.Tables("t1").Rows(0)("stage of Pressure ulcer").ToString
                CType(cl.FindControl("lblpressurelocation"), Label).Text = ds.Tables("t1").Rows(0)("location").ToString
                CType(cl.FindControl("lblpressuredocument"), Label).Text = ds.Tables("t1").Rows(0)("On admission, was a skin inspection documented").ToString & " : " & ds.Tables("t1").Rows(0)("Braden scale was").ToString

                If ds.Tables("t1").Rows(0)("When was the first Braden scale lower or equal than 19, were there any preventive intervention implemented").ToString = "yes" Then
                    CType(cl.FindControl("lblpressureprevention1"), Label).Text = "yes"
                Else
                    CType(cl.FindControl("lblpressureprevention2"), Label).Text = "no"
                End If

                If ds.Tables("t1").Rows(0)("risk_type").ToString = "1" Then
                    CType(cl.FindControl("lblpressureintrinsic"), Label).Text = "yes"
                    CType(cl.FindControl("lblpressureextrinsic"), Label).Text = "no"
                Else
                    CType(cl.FindControl("lblpressureintrinsic"), Label).Text = "no"
                    CType(cl.FindControl("lblpressureextrinsic"), Label).Text = "yes"
                End If
               

                If ds.Tables("t1").Rows(0)("Was the use of device or appliance involved in the development or asvancement of the pressure ulcer").ToString = "yes" Then
                    CType(cl.FindControl("lblpressuredevice1"), Label).Text = "yes"
                Else
                    CType(cl.FindControl("lblpressuredevice1"), Label).Text = "no"
                End If
              
                CType(cl.FindControl("lblbradenscale1"), Label).Text = ds.Tables("t1").Rows(0)("Sensory Perception").ToString
                CType(cl.FindControl("lblbradenscale2"), Label).Text = ds.Tables("t1").Rows(0)("Moisture").ToString
                CType(cl.FindControl("lblbradenscale3"), Label).Text = ds.Tables("t1").Rows(0)("Activity").ToString
                CType(cl.FindControl("lblbradenscale4"), Label).Text = ds.Tables("t1").Rows(0)("Mobility").ToString
                CType(cl.FindControl("lblbradenscale5"), Label).Text = ds.Tables("t1").Rows(0)("Nutrition").ToString
                CType(cl.FindControl("lblbradenscale6"), Label).Text = ds.Tables("t1").Rows(0)("Friction & Shear").ToString

                CType(cl.FindControl("lblScore"), Label).Text = ds.Tables("t1").Rows(0)("score").ToString

            End If
            'Response.Write(sql)


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

End Class
