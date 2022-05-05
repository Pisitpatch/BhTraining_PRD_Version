Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class cfb_logbook
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected priv_list() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        priv_list = Session("priv_list")

        If findArrayValue(priv_list, "1002") = True Then
            '  panel_tqm.Visible = True
            '  panel_admin.Visible = True
        Else
            cmdSearch.Enabled = False
            cmdExport.Enabled = False

        End If

        If Not Page.IsPostBack Then
            ' bindGrid()
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


    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.ir_id , c.comment_id , SUBSTRING(cast(cfb_no as varchar),9,4) AS 'No', SUBSTRING(cast(cfb_no as varchar),5,2) AS 'Month'  "
            sql &= ", case  when b.feedback_from = 1 then 'Face to Face'  when b.feedback_from = 2 then 'Suggestion box' "
            sql &= "when b.feedback_from = 3 then 'Customer Comment Form' when b.feedback_from = 4 then 'Telephone' "
            sql &= "when b.feedback_from = 5 then 'E-mail' when b.feedback_from = 6 then 'Letter' "
            sql &= "when b.feedback_from = 7 then 'Fax' when b.feedback_from = 8 then 'Web site' "
            sql &= "when b.feedback_from = 9 then 'Other' "
            sql &= "when b.feedback_from = 10 then 'Survey' "
            sql &= "when b.feedback_from = 11 then 'Facebook' "
            sql &= "end AS 'Source_of_report'  "
            sql &= ", case when c.log_group_report_id = 1 then 'Direct' when c.log_group_report_id = 2 then 'Indirect' end  AS 'Group_of_report' , b.division AS 'Division' , '' AS Customer_Number "
            sql &= ", 'CFB'+cast(b.cfb_no as varchar) AS Online_Number , case when c.log_service1_id = 1 then 'Yes' when  c.log_service1_id = 2 then 'No' else '' end AS 'Service Recovery Criteria' , case when c.log_service2_id = 1 then 'Yes' when c.log_service2_id = 2 then 'No' when c.log_service2_id = 3 then 'N/A' else '' end  AS 'Service Recovery Success' "
            sql &= ", c.log_success_by AS 'Success by' , c.log_owner_name AS 'Owner unit' , c.log_success_within AS 'Success within' "
            sql &= ", c.log_new_pt_name AS 'New_pt' , '' AS 'Next_time' , b.datetime_report AS 'Date_of_event' , b.datetime_complaint AS 'Date_of_complaint' "
            sql &= ",case  when DATEDIFF(hour, b.datetime_report ,a.date_submit) <= 6 then 'Yes' ELSE 'No' end AS 'Report_in_6_hours' "
            sql &= ",(SELECT TOP 1 log_time FROM ir_status_log WHERE status_id = 3 AND ir_id = a.ir_id ORDER BY log_time ASC) AS 'Date_of_CFB_received(mm/dd/yyyy)'  "
            ' sql &= ", cast(DATEPART (hour ,a.date_submit) as varchar) + ':' + cast(DATEPART (MINUTE ,a.date_submit) as varchar) AS 'Time_record' "
            sql &= ", b.complain_detail AS 'Name' , b.hn AS HN , b.country AS 'Nationality'  "
            ' sql &= ", case when b.customer_segment = 2 then 'Yes' when b.customer_segment = 1 then 'No'  when b.customer_segment = 3 then 'No' end AS Expat "
            ' sql &= ", case when b.customer_segment = 2 then 'Expatriate' when b.customer_segment = 1 then 'Thai'  when b.customer_segment = 3 then 'International' end AS Customer_Segment "
            sql &= ", case when b.customer_segment = 3 then 'Yes' when b.customer_segment = 1 then 'No'  when b.customer_segment = 2 then 'No' end AS Expat "
            sql &= ", case when b.customer_segment = 3 then 'Expatriate' when b.customer_segment = 1 then 'Thai'  when b.customer_segment = 2 then 'International' end AS Customer_Segment "


            sql &= ", case when b.sex = 'Male' or b.sex = 'Female' then b.sex else 'Not specific' end AS 'Gender' , b.age AS 'Age'  , case when b.service_type = 'OPD' then 'OPD'  when b.service_type = 'IPD' then 'IPD' end  AS 'Customer_status'  "
            sql &= ", case when b.complain_status = 1 then 'Yes'  else 'No' end AS 'By_PT' "
            sql &= ", case  when b.complain_status = 1 then 'Patient'  when b.complain_status = 2 then 'Relatives' "
            sql &= "when b.complain_status = 3 then 'Visitors' when b.complain_status = 4 then 'Employee' "
            sql &= "when b.complain_status = 5 then 'Physician' when b.complain_status = 6 then 'Web Site' "
            sql &= "when b.complain_status = 7 then 'Other'  end AS 'Status of complainant' "
            sql &= ", b.complain_status_remark AS 'Complainant' , c.comment_type_name AS 'Comment_type' "
            sql &= ",case when b.cfb_customer_resp  = 1 then 'satified' when b.cfb_customer_resp = 2 then 'satified'  "
            sql &= "when b.cfb_customer_resp = 3 then 'dissatisfied' when b.cfb_customer_resp = 4 then 'dissatisfied' end "
            sql &= "AS 'Outcome' "
            sql &= ", case when c.tqm_concern = 1 then 'Clinic' when c.tqm_concern = 2 then 'Service'  end AS 'Involve' "
            sql &= " , b.cfb_tel_remark AS Telephone , b.cfb_email_remark AS Email_address "
            sql &= ",case when b.cfb_customer_resp  = 2 OR b.cfb_customer_resp = 4 then 'Yes' "
            sql &= "when b.cfb_customer_resp = 1 OR b.cfb_customer_resp = 3 then 'No' end  "
            sql &= "AS 'Need_response' "
            sql &= ",  case when c.chk_tqm_contact = 1 then 'Yes'  when c.chk_tqm_contact = 0 then 'No'  end AS Initial_Response  "
            sql &= ", case  when DATEDIFF(hour, a.date_submit ,deptLog.log_time) <= 24 then 'Yes' ELSE 'No' end AS 'Response_in_24' "
            sql &= ", c.tqm_response AS 'Response_by' , c.tqm_method AS 'Method' , c.tqm_duration AS duration "
            sql &= ", deptLog.log_time AS 'Date_of_response' "
            '  sql &= ", cast(DATEPART(HOUR,deptLog.log_time) as varchar) + ':' + cast(DATEPART(MINUTE,deptLog.log_time) as varchar) AS 'Time_of_response' "
            sql &= ", case when c.tqm_chk_feedback2 = 1 then 'Investigate' else 'FYI' end AS 'Sending_for' , unit.dept_unit_name AS 'Sending_to_unit' "

            'sql &= ", doctor.specialty_name AS 'Specialty' , '' AS 'Subspecialty' , doctor.md_code AS 'MD Code' , doctor.doctor_name AS 'Physician' "
            sql &= " , case when unit.dept_unit_id = 139 then doctor.specialty_name else '' end AS 'Specialty' "
            sql &= " , '' AS 'Subspecialty' "
            sql &= " , case when unit.dept_unit_id = 139 then doctor.md_code else '' end AS 'MD Code' "
            sql &= " , case when unit.dept_unit_id = 139 then doctor.doctor_name else '' end AS 'Physician' "

            ' sql &= ",'' AS 'Specialty' , '' AS 'Subspecialty' , '' AS 'MD Code' , '' AS 'Physician' "
            sql &= ", b.diagnosis AS 'Diagnosis' , b.operation AS 'Procedure'  , c.log_service_place AS 'Service_place' "
            sql &= ",  b.room AS 'Room_No'  "
            sql &= ", deptLog.log_time  AS 'Sending_date' ,  case  when DATEDIFF(hour, a.date_submit ,deptLog.log_time) <= 24 then 'Yes' ELSE 'No' end  AS 'Sending within_24 hr'  "
            sql &= ", t1.grand_topic_name AS 'Grand topic' , ISNULL(t2.topic_name,'') AS 'Topic' , ISNULL(t3.subtopic_name,'') AS 'Subtopic1' "
            sql &= ", t4.subtopic2_name_en AS 'Subdetails' , t5.subtopic3_name_en AS 'UnderSubdetails' , c.comment_detail AS 'Detail' "
            sql &= ", '' AS 'Root_cause' , c.log_factor AS 'Contributing_factors' , c.log_corrective_action AS 'Corrective_action' "
            sql &= ", case when ISNULL(deptReturn.log_time_ts,0) > 0 then deptReturn.log_time  end AS 'Date of return' "
            sql &= ", case  when DATEDIFF(hour, deptLog.log_time , deptReturn.log_time ) <= 24 then 'Yes' ELSE 'No' end  AS 'Return within 24 hours' "
            sql &= ", case  when DATEDIFF(day, deptLog.log_time , deptReturn.log_time ) <= 3 then 'Yes' ELSE 'No' end  AS 'Return within 3 days' "
            sql &= ", case  when DATEDIFF(day, deptLog.log_time , deptReturn.log_time ) <= 5 then 'Yes' ELSE 'No' end  AS 'Return within 5 days' "
            sql &= ", case  when DATEDIFF(day, deptLog.log_time , deptReturn.log_time ) > 5 AND DATEDIFF(month, deptLog.log_time , deptReturn.log_time ) <= 1 then 'Yes' ELSE 'No' end AS 'Return within > 5 days - 1 month'  "
            sql &= ", case  when DATEDIFF(month, deptLog.log_time , deptReturn.log_time ) > 1 then 'Yes' ELSE 'No' end AS 'Return within >1 month'  "
            sql &= ", case when ISNULL(c.followup_date,0) > 0 then c.followup_date end AS 'Follow_up'  "
            sql &= ", ss.ir_status_name AS 'Status' , c.tqm_owner AS 'CFB_Staff' "
            sql &= ", c.log_qc_name AS 'QC' , c.log_other_hosp_name AS 'Change_to_other_hospital' "
            sql &= ", case when c.tqm_clinic = 1 then 'Near miss (0)'  when c.tqm_clinic = 2 then 'No harm (1)' when c.tqm_clinic = 3 then 'Mild AE (2)' "
            sql &= "when c.tqm_clinic = 4 then 'Moderate AE (3)' when c.tqm_clinic = 5 then 'Serious AE (4)' when c.tqm_clinic = 6 then 'Sentinel Event (5)' end AS ' Risk Level' "
            sql &= ", c.log_customer_objective AS 'Customer_Objective' , c.log_resolution AS 'Resolution' "
            sql &= ", c.log_action_taken AS Action_Taken , case when c.log_outcome_id = 0 then 'NA' when c.log_outcome_id is null then 'NA' else c.log_outcome_name end  AS Final_outcome"
            sql &= ", case when ISNULL(c.tqm_ref_no,0) > 0 then 'Yes' else 'No' end AS 'Conver from IR' "
            sql &= ", case when c.tqm_ref_no = 0 then '-' else c.tqm_ref_no end  AS 'IR_Number' "
            sql &= ", case when c.tqm_report_type = 0 then 'New Report' when c.tqm_report_type = 1 then 'Additional Report' when c.tqm_report_type = 2 then 'Repeated Report' end AS 'Report type' "
            sql &= ", case when c.tqm_report_type = 1 or c.tqm_report_type = 2 then c.tqm_cfb_no end AS 'Relate CFB'"
            sql &= ", c.log_person AS 'Person' , c.log_area_code AS 'Area code'"
            sql &= ", c.tqm_topic_detail AS 'Topic_Detail' , c.tqm_remark AS 'CFB_Remark' "
            sql &= " , CASE WHEN c.tqm_chk_write = 1 THEN 'Yes' ELSE 'No' END AS 'Write Off' , c.tqm_write_bath AS 'Write Off (THB)'"
            sql &= " , c.tqm_write_dept_name AS 'Write Off Dept. Unit' "
            sql &= " , CASE WHEN c.tqm_chk_refund = 1 THEN 'Yes' ELSE 'No' END AS 'Refund' , c.tqm_refund_bath AS 'Refund (THB)'"
            sql &= " , c.tqm_refund_dept_name AS 'Refund Dept. Unit' "
            sql &= " , CASE WHEN c.tqm_chk_remove = 1 THEN 'Yes' ELSE 'No' END AS 'Remove' , c.tqm_remove_bath AS 'Remove (THB)'"
            sql &= " , c.tqm_remove_dept_name AS 'Remove Dept. Unit' "
            sql &= " , CASE WHEN c.tqm_chk_bi_way = 1 THEN 'Yes' WHEN c.tqm_chk_bi_way = 0 THEN 'No' ELSE '' END AS 'Related BI Way' "
            sql &= " , c.tqm_recognition_name AS 'Recognition Name' , b.feedback_topic_text AS 'Feedback Title', b.cfb_case_manager AS 'Case Manager' , c.related_standard AS 'Related Standard', CASE WHEN c.is_tqm_risk = 1 THEN 'Yes' ELSE 'No' END AS 'Refer to Risk Management', CASE WHEN c.is_tqm_imco = 1 THEN 'Yes' ELSE 'No' END AS 'Refer to MCO'"

            ' sql &= " , CASE WHEN c.tqm_clinic = 1 THEN 'Near miss (0)' WHEN c.tqm_clinic = 2 THEN 'No harm (1)' WHEN c.tqm_clinic = 3 THEN 'Mild AE (2)' WHEN c.tqm_clinic = 4 THEN 'Moderate AE (3)' WHEN c.tqm_clinic = 5 THEN 'Serious AE (4)' WHEN c.tqm_clinic = 6 THEN 'Sentinel Event (5)'  ELSE '-' END AS 'Refer to Risk Management', CASE WHEN c.is_tqm_imco = 1 THEN 'Yes' ELSE 'No' END AS 'Risk Level'"

            sql &= "from ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id " & vbCrLf
            sql &= "INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id " & vbCrLf
            sql &= "INNER JOIN ir_cfb_unit_defendant unit ON a.ir_id = unit.ir_id AND c.comment_id = unit.comment_id "
            sql &= "LEFT OUTER JOIN  ir_topic_grand t1 ON c.grand_topic = t1.grand_topic_id " & vbCrLf
            sql &= "LEFT OUTER JOIN ir_topic t2 ON c.topic = t2.ir_topic_id " & vbCrLf
            sql &= "LEFT OUTER JOIN ir_topic_sub t3 ON c.subtopic1 = t3.ir_subtopic_id " & vbCrLf
            sql &= "LEFT OUTER JOIN ir_topic_sub2 t4 ON c.subtopic2 = t4.ir_subtopic2_id " & vbCrLf
            sql &= "LEFT OUTER JOIN ir_topic_sub3 t5 ON c.subtopic3 = t5.ir_subtopic3_id " & vbCrLf
            sql &= "LEFT OUTER JOIN (SELECT ir_id , MIN(log_time) AS log_time ,  MIN(log_time_ts) AS log_time_ts FROM ir_status_log WHERE status_id = 4 GROUP BY ir_id  ) deptLog ON a.ir_id = deptLog.ir_id " & vbCrLf
            sql &= "LEFT OUTER JOIN (SELECT ir_id , MAX(log_time) AS log_time ,  MAX(log_time_ts) AS log_time_ts FROM ir_status_log WHERE status_id = 7 GROUP BY ir_id) deptReturn ON a.ir_id = deptReturn.ir_id " & vbCrLf

            sql &= "LEFT OUTER JOIN (SELECT a1.ir_id , a1.comment_id ,doctor_name , md_code , a2.specialty AS specialty_name , a1.dept_unit_id "
            sql &= "FROM ir_doctor_defendant a1 INNER JOIN m_doctor a2 ON a1.md_code = a2.emp_no ) doctor ON a.ir_id = doctor.ir_id AND c.comment_id = doctor.comment_id AND unit.dept_unit_id = doctor.dept_unit_id  "
            sql &= " "

            sql &= " INNER JOIN ir_status_list ss ON a.status_id = ss.ir_status_id " & vbCrLf
            sql &= "WHERE a.report_type = 'cfb' AND a.is_delete = 0 AND a.is_cancel = 0 AND c.tqm_report_type <> 2 AND a.status_id > 2 "

            sql &= ""
            sql &= ""
            sql &= ""
            sql &= ""
            sql &= ""
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If
            sql &= " ORDER BY b.cfb_no , c.order_sort"
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            '  Response.Write(sql)
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindGridMCO()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.ir_id , c.comment_id , SUBSTRING(cast(cfb_no as varchar),9,4) AS 'No', SUBSTRING(cast(cfb_no as varchar),5,2) AS 'Month'  "
            sql &= ", case  when b.feedback_from = 1 then 'Face to Face'  when b.feedback_from = 2 then 'Suggestion box' "
            sql &= "when b.feedback_from = 3 then 'Customer Comment Form' when b.feedback_from = 4 then 'Telephone' "
            sql &= "when b.feedback_from = 5 then 'E-mail' when b.feedback_from = 6 then 'Letter' "
            sql &= "when b.feedback_from = 7 then 'Fax' when b.feedback_from = 8 then 'Web site' "
            sql &= "when b.feedback_from = 9 then 'Other' "
            sql &= "end AS 'Source_of_report'  "
            sql &= ", case when c.log_group_report_id = 1 then 'Direct' when c.log_group_report_id = 2 then 'Indirect' end  AS 'Group_of_report' , b.division AS 'Division' , '' AS Customer_Number "
            sql &= ", 'CFB'+cast(b.cfb_no as varchar) AS Online_Number , case when c.log_service1_id = 1 then 'Yes' when  c.log_service1_id = 2 then 'No' else '' end AS 'Service Recovery Criteria' , case when c.log_service2_id = 1 then 'Yes' when c.log_service2_id = 2 then 'No' when c.log_service2_id = 3 then 'N/A' else '' end  AS 'Service Recovery Success' "
            sql &= ", c.log_success_by AS 'Success by' , c.log_owner_name AS 'Owner unit' , c.log_success_within AS 'Success within' "
            sql &= ", c.log_new_pt_name AS 'New_pt' , '' AS 'Next_time' , b.datetime_report AS 'Date_of_event' , b.datetime_complaint AS 'Date_of_complaint' "
            sql &= ",case  when DATEDIFF(hour, b.datetime_report ,a.date_submit) <= 6 then 'Yes' ELSE 'No' end AS 'Report_in_6_hours' "
            sql &= ",(SELECT TOP 1 log_time FROM ir_status_log WHERE status_id = 3 AND ir_id = a.ir_id ORDER BY log_time ASC) AS 'Date_of_TQM_received(mm/dd/yyyy)'  "
            ' sql &= ", cast(DATEPART (hour ,a.date_submit) as varchar) + ':' + cast(DATEPART (MINUTE ,a.date_submit) as varchar) AS 'Time_record' "
            sql &= ", b.complain_detail AS 'Name' , b.hn AS HN , b.country AS 'Nationality'  "
            ' sql &= ", case when b.customer_segment = 2 then 'Yes' when b.customer_segment = 1 then 'No'  when b.customer_segment = 3 then 'No' end AS Expat "
            ' sql &= ", case when b.customer_segment = 2 then 'Expatriate' when b.customer_segment = 1 then 'Thai'  when b.customer_segment = 3 then 'International' end AS Customer_Segment "
            sql &= ", case when b.customer_segment = 3 then 'Yes' when b.customer_segment = 1 then 'No'  when b.customer_segment = 2 then 'No' end AS Expat "
            sql &= ", case when b.customer_segment = 3 then 'Expatriate' when b.customer_segment = 1 then 'Thai'  when b.customer_segment = 2 then 'International' end AS Customer_Segment "


            sql &= ", case when b.sex = 'Male' or b.sex = 'Female' then b.sex else 'Not specific' end AS 'Gender' , b.age AS 'Age'  , case when b.service_type = 'OPD' then 'OPD'  when b.service_type = 'IPD' then 'IPD' end  AS 'Customer_status'  "
            sql &= ", case when b.complain_status = 1 then 'Yes'  else 'No' end AS 'By_PT' "
            sql &= ", case  when b.complain_status = 1 then 'Patient'  when b.complain_status = 2 then 'Relatives' "
            sql &= "when b.complain_status = 3 then 'Visitors' when b.complain_status = 4 then 'Employee' "
            sql &= "when b.complain_status = 5 then 'Physician' when b.complain_status = 6 then 'Web Site' "
            sql &= "when b.complain_status = 7 then 'Other'  end AS 'Status of complainant' "
            sql &= ", b.complain_status_remark AS 'Complainant' , c.comment_type_name AS 'Comment_type' "
            sql &= ",case when b.cfb_customer_resp  = 1 then 'satified' when b.cfb_customer_resp = 2 then 'satified'  "
            sql &= "when b.cfb_customer_resp = 3 then 'dissatisfied' when b.cfb_customer_resp = 4 then 'dissatisfied' end "
            sql &= "AS 'Outcome' "
            sql &= ", case when c.tqm_concern = 1 then 'Clinic' when c.tqm_concern = 2 then 'Service'  end AS 'Involve' "
            sql &= " , b.cfb_tel_remark AS Telephone , b.cfb_email_remark AS Email_address "
            sql &= ",case when b.cfb_customer_resp  = 2 OR b.cfb_customer_resp = 4 then 'Yes' "
            sql &= "when b.cfb_customer_resp = 1 OR b.cfb_customer_resp = 3 then 'No' end  "
            sql &= "AS 'Need_response' "
            sql &= ",  case when c.chk_tqm_contact = 1 then 'Yes'  when c.chk_tqm_contact = 0 then 'No'  end AS Initial_Response  "
            sql &= ", case  when DATEDIFF(hour, a.date_submit ,deptLog.log_time) <= 24 then 'Yes' ELSE 'No' end AS 'Response_in_24' "
            sql &= ", c.tqm_response AS 'Response_by' , c.tqm_method AS 'Method' , c.tqm_duration AS duration "
            sql &= ", deptLog.log_time AS 'Date_of_response' "
            '  sql &= ", cast(DATEPART(HOUR,deptLog.log_time) as varchar) + ':' + cast(DATEPART(MINUTE,deptLog.log_time) as varchar) AS 'Time_of_response' "
            sql &= ", case when c.tqm_chk_feedback2 = 1 then 'Investigate' else 'FYI' end AS 'Sending_for' , unit.dept_unit_name AS 'Sending_to_unit' "

            'sql &= ", doctor.specialty_name AS 'Specialty' , '' AS 'Subspecialty' , doctor.md_code AS 'MD Code' , doctor.doctor_name AS 'Physician' "
            sql &= " , case when unit.dept_unit_id = 139 then doctor.specialty_name else '' end AS 'Specialty' "
            sql &= " , '' AS 'Subspecialty' "
            sql &= " , case when unit.dept_unit_id = 139 then doctor.md_code else '' end AS 'MD Code' "
            sql &= " , case when unit.dept_unit_id = 139 then doctor.doctor_name else '' end AS 'Physician' "

            ' sql &= ",'' AS 'Specialty' , '' AS 'Subspecialty' , '' AS 'MD Code' , '' AS 'Physician' "
            sql &= ", b.diagnosis AS 'Diagnosis' , b.operation AS 'Procedure'  , c.log_service_place AS 'Service_place' "
            sql &= ",  b.room AS 'Room_No'  "
            sql &= ", deptLog.log_time  AS 'Sending_date' ,  case  when DATEDIFF(hour, a.date_submit ,deptLog.log_time) <= 24 then 'Yes' ELSE 'No' end  AS 'Sending within_24 hr'  "
            sql &= ", t1.grand_topic_name AS 'Grand topic' , ISNULL(t2.topic_name,'') AS 'Topic' , ISNULL(t3.subtopic_name,'') AS 'Subtopic1' "
            sql &= ", t4.subtopic2_name_en AS 'Subdetails' , t5.subtopic3_name_en AS 'UnderSubdetails' , c.comment_detail AS 'Detail' "
            sql &= ", '' AS 'Root_cause' , c.log_factor AS 'Contributing_factors' , c.log_corrective_action AS 'Corrective_action' "
            sql &= ", case when ISNULL(deptReturn.log_time_ts,0) > 0 then deptReturn.log_time  end AS 'Date of return' "
            sql &= ", case  when DATEDIFF(hour, deptLog.log_time , deptReturn.log_time ) <= 24 then 'Yes' ELSE 'No' end  AS 'Return within 24 hours' "
            sql &= ", case  when DATEDIFF(day, deptLog.log_time , deptReturn.log_time ) <= 3 then 'Yes' ELSE 'No' end  AS 'Return within 3 days' "
            sql &= ", case  when DATEDIFF(day, deptLog.log_time , deptReturn.log_time ) <= 5 then 'Yes' ELSE 'No' end  AS 'Return within 5 days' "
            sql &= ", case  when DATEDIFF(day, deptLog.log_time , deptReturn.log_time ) > 5 AND DATEDIFF(month, deptLog.log_time , deptReturn.log_time ) <= 1 then 'Yes' ELSE 'No' end AS 'Return within > 5 days - 1 month'  "
            sql &= ", case  when DATEDIFF(month, deptLog.log_time , deptReturn.log_time ) > 1 then 'Yes' ELSE 'No' end AS 'Return within >1 month'  "
            sql &= ", case when ISNULL(c.followup_date,0) > 0 then c.followup_date end AS 'Follow_up'  "
            sql &= ", ss.ir_status_name AS 'Status' , c.tqm_owner AS 'TQM_Staff' "
            sql &= ", c.log_qc_name AS 'QC' , c.log_other_hosp_name AS 'Change_to_other_hospital' "
            sql &= ", case when c.tqm_clinic = 1 then 'Near miss (0)'  when c.tqm_clinic = 2 then 'No harm (1)' when c.tqm_clinic = 3 then 'Mild AE (2)' "
            sql &= "when c.tqm_clinic = 4 then 'Moderate AE (3)' when c.tqm_clinic = 5 then 'Serious AE (4)' when c.tqm_clinic = 6 then 'Sentinel Event (5)' end AS ' Risk Level' "
            sql &= ", c.log_customer_objective AS 'Customer_Objective' , c.log_resolution AS 'Resolution' "
            sql &= ", c.log_action_taken AS Action_Taken , case when c.log_outcome_id = 0 then 'NA' when c.log_outcome_id is null then 'NA' else c.log_outcome_name end  AS Final_outcome"
            sql &= ", case when ISNULL(c.tqm_ref_no,0) > 0 then 'Yes' else 'No' end AS 'Conver from IR' "
            sql &= ", case when c.tqm_ref_no = 0 then '-' else c.tqm_ref_no end  AS 'IR_Number' "
            sql &= ", case when c.tqm_report_type = 0 then 'New Report' when c.tqm_report_type = 1 then 'Additional Report' when c.tqm_report_type = 2 then 'Repeated Report' end AS 'Report type' "
            sql &= ", case when c.tqm_report_type = 1 or c.tqm_report_type = 2 then c.tqm_cfb_no end AS 'Relate CFB'"
            sql &= ", c.log_person AS 'Person' , c.log_area_code AS 'Area code' "
            sql &= ", c.tqm_topic_detail AS 'Topic_Detail' , c.tqm_remark AS 'CFB_Remark' "
            sql &= " , CASE WHEN c.tqm_chk_write = 1 THEN 'Yes' ELSE 'No' END AS 'Write Off' , c.tqm_write_bath AS 'Write Off (THB)'"
            sql &= " , c.tqm_write_dept_name AS 'Write Off Dept. Unit' "
            sql &= " , CASE WHEN c.tqm_chk_refund = 1 THEN 'Yes' ELSE 'No' END AS 'Refund' , c.tqm_refund_bath AS 'Refund (THB)'"
            sql &= " , c.tqm_refund_dept_name AS 'Refund Dept. Unit' "
            sql &= " , CASE WHEN c.tqm_chk_remove = 1 THEN 'Yes' ELSE 'No' END AS 'Remove' , c.tqm_remove_bath AS 'Remove (THB)'"
            sql &= " , c.tqm_remove_dept_name AS 'Remove Dept. Unit' "
            sql &= " , CASE WHEN c.tqm_chk_bi_way = 1 THEN 'Yes' WHEN c.tqm_chk_bi_way = 0 THEN 'No' ELSE '' END AS 'Related BI Way' "
            sql &= " , c.tqm_recognition_name AS 'Recognition Name' , b.feedback_topic_text AS 'Feedback Title' "

            sql &= " , b.psm_compensation , b.psm_dianosis , b.psm_recommendation , b.psm_remark , b.psm_status_name , b.psm_pt_satisfaction_name , b.psm_person "

            sql &= "from ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id " & vbCrLf
            sql &= "INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id " & vbCrLf
            sql &= "INNER JOIN ir_cfb_unit_defendant unit ON a.ir_id = unit.ir_id AND c.comment_id = unit.comment_id "
            sql &= "LEFT OUTER JOIN  ir_topic_grand t1 ON c.grand_topic = t1.grand_topic_id " & vbCrLf
            sql &= "LEFT OUTER JOIN ir_topic t2 ON c.topic = t2.ir_topic_id " & vbCrLf
            sql &= "LEFT OUTER JOIN ir_topic_sub t3 ON c.subtopic1 = t3.ir_subtopic_id " & vbCrLf
            sql &= "LEFT OUTER JOIN ir_topic_sub2 t4 ON c.subtopic2 = t4.ir_subtopic2_id " & vbCrLf
            sql &= "LEFT OUTER JOIN ir_topic_sub3 t5 ON c.subtopic3 = t5.ir_subtopic3_id " & vbCrLf
            sql &= "LEFT OUTER JOIN (SELECT ir_id , MIN(log_time) AS log_time ,  MIN(log_time_ts) AS log_time_ts FROM ir_status_log WHERE status_id = 4 GROUP BY ir_id  ) deptLog ON a.ir_id = deptLog.ir_id " & vbCrLf
            sql &= "LEFT OUTER JOIN (SELECT ir_id , MAX(log_time) AS log_time ,  MAX(log_time_ts) AS log_time_ts FROM ir_status_log WHERE status_id = 7 GROUP BY ir_id) deptReturn ON a.ir_id = deptReturn.ir_id " & vbCrLf

            sql &= "LEFT OUTER JOIN (SELECT a1.ir_id , a1.comment_id ,doctor_name , md_code , a2.specialty AS specialty_name , a1.dept_unit_id "
            sql &= "FROM ir_doctor_defendant a1 INNER JOIN m_doctor a2 ON a1.md_code = a2.emp_no ) doctor ON a.ir_id = doctor.ir_id AND c.comment_id = doctor.comment_id AND unit.dept_unit_id = doctor.dept_unit_id  "
            sql &= " "

            sql &= " INNER JOIN ir_status_list ss ON a.status_id = ss.ir_status_id " & vbCrLf
            sql &= "WHERE a.report_type = 'cfb' AND a.is_delete = 0 AND a.is_cancel = 0 AND c.tqm_report_type <> 2 AND a.status_id > 2 "

            sql &= " AND (a.status_id IN (5,6,8) OR 5 IN (SELECT status_id FROM ir_status_log WHERE ir_id = a.ir_id) ) "

            sql &= " AND ISNULL(b.psm_status_id,0) > 0"
            sql &= ""
            sql &= ""
            sql &= ""
            sql &= ""
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If
            sql &= " ORDER BY b.cfb_no , c.order_sort"
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            '  Response.Write(sql)
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindDashboard()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT  "
            sql &= "  a.ir_id , "
            sql &= "    CONVERT(varchar, a.date_submit, 103)  AS 'Date of Received (dd/mm/yy)' ,  CONVERT(varchar, b.datetime_report, 103)  AS 'Date of Event (dd/mm/yy)' "
            sql &= ", 'CFB report' AS 'Type of Report' , c.comment_type_name  AS 'Comment Type'  ,  'CFB'+cast(b.cfb_no as varchar) AS 'Online Number' "

            sql &= ", ISNULL(case when b.customer_segment = 1 THEN 'Thai' when b.customer_segment = 2 THEN 'Expatriate' when b.customer_segment = 3 THEN 'International' end,'') AS 'Customer segment' "
            sql &= ", ISNULL(case when b.new_clinical_type = 1 THEN 'Clinical' when b.new_clinical_type = 2 THEN 'Non-Clinical' END,'') AS 'Clinical type' "
            sql &= ", CASE WHEN b.new_severe_id = 1 THEN 'No Harm' WHEN b.new_severe_id = 2 THEN 'Mild Adverse Event' WHEN b.new_severe_id = 3 THEN 'Moderate Adverse Event' WHEN b.new_severe_id = 4 THEN 'Serious Adverse Event' WHEN b.new_severe_id = 5 THEN 'Sentinel event' WHEN b.new_severe_id = 10 THEN 'Near Miss' ELSE '-' END  AS 'Risk level'"

            sql &= " , b.new_safe_goal AS 'Patient safety goal' , b.new_payor AS 'By Payor' ,b.new_issue AS 'Issue' , b.new_dept_relate_name AS 'Response unit' "
            sql &= " , b.location , c.comment_detail AS 'Detail' "



            sql &= "from ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id " & vbCrLf
            sql &= "INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id " & vbCrLf
            '  sql &= "INNER JOIN ir_cfb_unit_defendant unit ON a.ir_id = unit.ir_id AND c.comment_id = unit.comment_id "


            sql &= " INNER JOIN ir_status_list ss ON a.status_id = ss.ir_status_id " & vbCrLf
            '   sql &= "WHERE a.report_type = 'cfb' AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0  AND c.tqm_report_type <> 2 AND a.status_id > 2 "
            sql &= "WHERE a.report_type = 'cfb' AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0   AND a.status_id > 1 "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If
            '  sql &= " ORDER BY b.cfb_no , c.order_sort"
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            ' Response.Write(sql)
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If txtselect.SelectedIndex = 0 Then
            bindGrid()
        ElseIf txtselect.SelectedIndex = 1 Then
            bindGridMCO()
        Else
            bindDashboard()
        End If

      
    End Sub

    Protected Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        Export("logbook.xls", GridView1)
    End Sub

    Public Shared Sub Export(ByVal fileName As String, ByVal gv As GridView)


        HttpContext.Current.Response.Clear()

        System.Web.HttpContext.Current.Response.Buffer = True
        'System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8

        'Export will take two parameter first one the name of Excel File, and second one for gridview to be exported
        ' HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8
        'System.Web.HttpContext.Current.Response.Charset = ""

        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode
        HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())

        Using strWriter As New StringWriter()


            Using htmlWriter As New HtmlTextWriter(strWriter)


                ' Create a form to contain the grid

                Dim table As New Table()

                ' add the header row to the table

                If gv.HeaderRow IsNot Nothing Then


                    ExportControl(gv.HeaderRow)


                    table.Rows.Add(gv.HeaderRow)
                End If

                ' add each of the data rows to the table

                For Each row As GridViewRow In gv.Rows


                    ExportControl(row)


                    table.Rows.Add(row)
                Next

                ' add the footer row to the table

                If gv.FooterRow IsNot Nothing Then


                    ExportControl(gv.FooterRow)


                    table.Rows.Add(gv.FooterRow)
                End If

                ' render the table into the htmlwriter

                table.RenderControl(htmlWriter)

                ' render the htmlwriter into the response

                HttpContext.Current.Response.Write(strWriter.ToString())


                HttpContext.Current.Response.[End]()

            End Using
        End Using

    End Sub


    Private Shared Sub ExportControl(ByVal control As Control)


        For i As Integer = 0 To control.Controls.Count - 1


            Dim current As Control = control.Controls(i)

            If TypeOf current Is LinkButton Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, LinkButton).Text))

            ElseIf TypeOf current Is ImageButton Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, ImageButton).AlternateText))

            ElseIf TypeOf current Is HyperLink Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, HyperLink).Text))

            ElseIf TypeOf current Is DropDownList Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, DropDownList).SelectedItem.Text))

            ElseIf TypeOf current Is CheckBox Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(If(TryCast(current, CheckBox).Checked, "True", "False")))
            End If

            'Like that you may convert any control to literals

            If current.HasControls() Then



                ExportControl(current)

            End If
        Next

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            '  Response.Write(e.Row.Cells(0).Text & "<br/>")
            Dim sql As String
            Dim ds As New DataSet
            Dim limit As String = ""
            Try

                'sql = "SELECT * FROM  cfb_comment_list a INNER JOIN ir_cfb_unit_defendant b ON a.comment_id = b.comment_id "
                'sql &= " WHERE a.comment_id = " & e.Row.Cells(1).Text & " "

                'ds = conn.getDataSet(sql, "t2")
                'If conn.errMessage <> "" Then
                '    Throw New Exception(conn.errMessage)
                'End If

                'For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1
                '    If i = 0 Then
                '        limit = ""
                '    Else
                '        limit = ","
                '    End If

                '    '  e.Row.Cells(44).Text &= limit & ds.Tables("t2").Rows(i)("dept_unit_name").ToString
                'Next i

                ' If e.Row.Cells(44).Text = "MDCL" Then
                'sql = "SELECT * FROM ir_doctor_defendant a INNER JOIN m_doctor b ON a.md_code = b.emp_no WHERE a.comment_id = " & e.Row.Cells(1).Text & " "
                'ds = conn.getDataSet(sql, "t3")
                'If conn.errMessage <> "" Then
                '    Throw New Exception(conn.errMessage)
                'End If

                'If ds.Tables("t3").Rows.Count > 0 Then
                '    e.Row.Cells(48).Text &= ds.Tables("t3").Rows(0)("doctor_name_en").ToString
                '    e.Row.Cells(47).Text &= ds.Tables("t3").Rows(0)("md_code").ToString
                '    e.Row.Cells(45).Text &= ds.Tables("t3").Rows(0)("specialty").ToString
                'End If

                'End If


                sql = "SELECT * FROM cfb_tqm_cause_list WHERE comment_id = " & e.Row.Cells(1).Text
                ds = conn.getDataSet(sql, "t4")
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If
                ' TQM Cause
                For i As Integer = 0 To ds.Tables("t4").Rows.Count - 1
                    If i = 0 Then
                        limit = ""
                    Else
                        limit = ","
                    End If
                    If txtselect.SelectedIndex < 2 Then
                        e.Row.Cells(61).Text &= limit & ds.Tables("t4").Rows(i)("cause_name").ToString & " " & ds.Tables("t4").Rows(i)("cause_remark").ToString
                    End If

                Next i

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
