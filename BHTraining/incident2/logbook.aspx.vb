Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class incident_logbook
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected priv_list() As String
    Protected reporttype As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        priv_list = Session("priv_list")
        reporttype = Request.QueryString("reporttype")

        If findArrayValue(priv_list, "1001") = True Then
            '  panel_tqm.Visible = True
            '  panel_admin.Visible = True
        Else
            cmdSearch.Enabled = False
            cmdExport.Enabled = False
            txtselect.Enabled = False
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

    Sub bindDelete()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT  a.ir_id , 'IR' + cast(c.ir_no as varchar) AS 'IR NO.' "
            sql &= ", c.pt_name , c.service_type AS patient_type  , c.describe AS 'Detail' , d.action_detail AS 'Corrective Action'  "
            sql &= ", t1.grand_topic_name , t2.topic_name , t3.subtopic_name "
            sql &= ",ISNULL(is_doc_delete_name,'') as 'delete' "
            sql &= ",ISNULL(unit_err_type_name,'') as 'Unit of Error Document' "
            sql &= ",case when chk_typeofdoc1 = 1 then 'yes' when chk_typeofdoc1 = 0 then 'no' else '' end as 'Document Error' "
            sql &= ",case when chk_typeofdoc2 = 1 then 'yes' when chk_typeofdoc2 = 0 then 'no' else '' end as 'Scan Error' "
            sql &= ",case when chk_typeofdoc3 = 1 then 'yes' when chk_typeofdoc3 = 0 then 'no' else '' end as 'Registration Error' "
            sql &= ",case when chk_typeofdoc4 = 1 then 'yes' when chk_typeofdoc4 = 0 then 'no' else '' end as 'Correct Document' "
            sql &= ",case when chk_typeofdoc5 = 1 then 'yes' when chk_typeofdoc5 = 0 then 'no' else '' end as 'Print Error' "
            sql &= ",case when chk_typeofdoc6 = 1 then 'yes' when chk_typeofdoc6 = 0 then 'no' else '' end as 'Incomplete Document' "
            sql &= ",case when chk_typeofdoc7 = 1 then 'yes' when chk_typeofdoc7 = 0 then 'no' else '' end as 'Improper Document' "
            sql &= " ,case when chk_typeofdoc8 = 1 then 'yes' when chk_typeofdoc8 = 0 then 'no' else '' end as 'Unnecessary Document' "
            sql &= ",case when chk_typeofdoc9 = 1 then 'yes' when chk_typeofdoc9 = 0 then 'no' else '' end as 'Other' "
            sql &= ",case when chk_typeofptrec1 = 1 then 'yes' when chk_typeofptrec1 = 0 then 'no' else '' end as 'Patient Record Lost' "
            sql &= ",case when chk_typeofptrec2 = 1 then 'yes' when chk_typeofptrec2 = 0 then 'no' else '' end as 'Release Patient Record' "
            sql &= ",case when chk_typeofptrec3 = 1 then 'yes' when chk_typeofptrec3 = 0 then 'no' else '' end as 'Record Error' "
            sql &= ",case when chk_typeofptrec4 = 1 then 'yes' when chk_typeofptrec4 = 0 then 'no' else '' end as 'Other' "
            sql &= ",ISNULL(is_edoc_name,'') as 'Electronic Documentation' "
            sql &= ",ISNULL(is_doctype_name,'') as 'Type of Document' "
            sql &= ",case when is_opd_doc = 1 then 'yes'  else 'no' end as 'OPD Document' "
            sql &= ",opd_doc1 ,opd_doc2 ,opd_doc3 "
            sql &= " ,case when is_ipd_doc = 1 then 'yes'  else 'no' end as 'IPD Document' "
            sql &= ",ipd_doc1,ipd_doc2,ipd_doc3"
            sql &= " ,case when is_cardiac_doc = 1 then 'yes'  else 'no' end as 'Cardiac Investigation Form'"
            sql &= ",cardiac_doc1,cardiac_doc2,cardiac_doc3 "
            sql &= ",case when is_film_doc = 1 then 'yes'  else 'no' end as 'Film Document' "
            sql &= ",film_doc1,film_doc2,film_doc3 "
            sql &= " ,case when is_lab_doc = 1 then 'yes'  else 'no' end as 'Lab Document' "
            sql &= ",lab_doc1,lab_doc2,lab_doc3 "
            sql &= " ,case when is_other_doc = 1 then 'yes'  else 'no' end as 'Other Document' "
            sql &= ",other_doc1,other_doc2,other_doc3 "
            sql &= ",ISNULL(found_error_by_name,'') AS 'Found the error by' "
            sql &= ",found_error_remark "
            sql &= ",case when chk_scancause_h1 = 1 then 'yes'  else 'no' end as 'H1' "
            sql &= ",case when chk_scancause_h2 = 1 then 'yes'  else 'no' end as 'H2' "
            sql &= ",case when chk_scancause_h3 = 1 then 'yes'  else 'no' end as 'H3' "
            sql &= ",case when chk_scancause_h4 = 1 then 'yes'  else 'no' end as 'H4' "
            sql &= ",case when chk_scancause_h5 = 1 then 'yes'  else 'no' end as 'H5' "
            sql &= ",case when chk_scancause_h6 = 1 then 'yes'  else 'no' end as 'H6' "
            sql &= ",case when chk_scancause_h7 = 1 then 'yes'  else 'no' end as 'H7' "
            sql &= ",case when chk_scancause_h8 = 1 then 'yes'  else 'no' end as 'H8' "
            sql &= ",case when chk_scancause_h9 = 1 then 'yes'  else 'no' end as 'H9'"
            sql &= ",case when chk_scancause_h10 = 1 then 'yes'  else 'no' end as 'H10'"
            sql &= " ,case when chk_scancause_h11 = 1 then 'yes'  else 'no' end as 'H11'"
            sql &= " ,case when chk_scancause_h12 = 1 then 'yes'  else 'no' end as 'H12'"
            sql &= ",chk_scancause_h12_remark "
            sql &= ",case when chk_scancause_p1 = 1 then 'yes'  else 'no' end as 'P1' "
            sql &= " ,case when chk_scancause_p2 = 1 then 'yes'  else 'no' end as 'P2' "
            sql &= ",case when chk_scancause_p3 = 1 then 'yes'  else 'no' end as 'P3' "
            sql &= ",case when chk_scancause_p4 = 1 then 'yes'  else 'no' end as 'P4' "
            sql &= ",case when chk_scancause_p5 = 1 then 'yes'  else 'no' end as 'P5' "
            sql &= ",case when chk_scancause_p6 = 1 then 'yes'  else 'no' end as 'P6' "
            sql &= ",case when chk_scancause_p7 = 1 then 'yes'  else 'no' end as 'P7' "
            sql &= ",case when chk_scancause_p8 = 1 then 'yes'  else 'no' end as 'P8'"
            sql &= ",case when chk_scancause_p9 = 1 then 'yes'  else 'no' end as 'P9'"
            sql &= ",case when chk_scancause_p10 = 1 then 'yes'  else 'no' end as 'P10'"
            sql &= ",chk_scancause_p10_remark"
            sql &= ",case when chk_scancause_c1 = 1 then 'yes'  else 'no' end as 'C1'"
            sql &= " ,case when chk_scancause_c2 = 1 then 'yes'  else 'no' end as 'C2' "
            sql &= " ,case when chk_scancause_c3 = 1 then 'yes'  else 'no' end as 'C3' "
            sql &= ",case when chk_scancause_c4 = 1 then 'yes'  else 'no' end as 'C4' "
            sql &= ",case when chk_scancause_c5 = 1 then 'yes'  else 'no' end as 'C5' "
            sql &= " ,chk_scancause_c5_remark "
            sql &= ",case when chk_scancause_s1 = 1 then 'yes'  else 'no' end as 'S1' "
            sql &= ",case when chk_scancause_s2 = 1 then 'yes'  else 'no' end as 'S2' "
            sql &= ",case when chk_scancause_s3 = 1 then 'yes'  else 'no' end as 'S3' "
            sql &= " ,case when chk_scancause_s4 = 1 then 'yes'  else 'no' end as 'S4' "
            sql &= " ,case when chk_scancause_s5 = 1 then 'yes'  else 'no' end as 'S5' "
            sql &= ",case when chk_scancause_s6 = 1 then 'yes'  else 'no' end as 'S6' "
            sql &= ",chk_scancause_s6_remark"
            sql &= ",case when chk_scancause_m1 = 1 then 'yes'  else 'no' end as 'M1' "
            sql &= ",case when chk_scancause_m2 = 1 then 'yes'  else 'no' end as 'M2' "
            sql &= ",is_correct_name as 'Correct document in computer system' "
            sql &= " , tqm_report_type_name AS 'Report Type' , repeat_ir_no AS 'Repeat IR No.' "
            sql &= " , replace(dept1.root_cause,'&#x0D;',' , ') AS 'root cause from unit' "
            sql &= " , replace(deptplan.action_plan,'&#x0D;',' , ') AS 'action plan from unit' "

            sql &= " , replace(wrongdoc.wrong_document,'&#x0D;',' , ') AS 'wrong document name' "
            sql &= " , replace(correctdoc.correct_document,'&#x0D;',' , ') AS 'correct document name' "


            sql &= "FROM ir_delete_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id INNER JOIN ir_detail_tab c ON a.ir_id = c.ir_id "

            If reporttype = "sup" Then
                sql &= "LEFT OUTER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            Else
                sql &= "INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            End If

            sql &= "LEFT OUTER JOIN  ir_topic_grand t1 ON d.grand_topic = t1.grand_topic_id "
            sql &= "LEFT OUTER JOIN ir_topic t2 ON d.topic = t2.ir_topic_id "
            sql &= "LEFT OUTER JOIN ir_topic_sub t3 ON d.subtopic1 = t3.ir_subtopic_id "
            sql &= "LEFT OUTER JOIN ir_topic_sub2 t4 ON d.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON d.subtopic3 = t5.ir_subtopic3_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.cause_detail  "
            sql &= " FROM ir_relate_dept t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'root_cause'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) dept1 ON a.ir_id = dept1.ir_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.action_detail  "
            sql &= " FROM ir_dept_prevent_list t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'action_plan'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) deptplan ON a.ir_id = deptplan.ir_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.wrong_doc_name  "
            sql &= " FROM ir_delete_scan_detail t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'wrong_document'"
            sql &= " FROM ir_delete_scan_detail t11"
            sql &= " GROUP BY t11.ir_id ) wrongdoc ON a.ir_id = wrongdoc.ir_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.correct_doc_name  "
            sql &= " FROM ir_delete_scan_detail t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'correct_document'"
            sql &= " FROM ir_delete_scan_detail t11"
            sql &= " GROUP BY t11.ir_id ) correctdoc ON a.ir_id = correctdoc.ir_id "

            sql &= " WHERE ISNULL(b.is_delete,0) = 0 And ISNULL(b.is_cancel,0) = 0  And b.form_id = 7 "

            If reporttype = "sup" Then
                sql &= " AND b.status_id >= 2"
            Else
                sql &= " AND b.status_id > 2"
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            'Response.Write(sql)
            GridView1.DataSource = ds
            GridView1.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
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
            sql &= ",case when stage_type = 1 then 'Stage I' when stage_type = 2 then 'Stage I' "
            sql &= "when stage_type = 3 then 'Stage II' when stage_type =  4 then 'Stage IV' "
            sql &= " when stage_type = 5 then 'Unstageable' when stage_type =  6 then 'Suspected Deep Tissue Injury' end AS 'stage of Pressure ulcer'"
            sql &= ",case when  location_type = 1 then 'Coccyx' when  location_type = 2 then 'Buttock : ' + buttock_type "
            sql &= " when  location_type = 3 then '' when  location_type = 4 then '' end as 'location' "
            sql &= ",buttock_type as Buttock ,hip_type as Hip ,other_remark as other_location "
            sql &= ",case when admission_type = 1 then 'yes' when admission_type = 2 then 'no' + ' ' + scale_no_detail else '' end as 'On admission, was a skin inspection documented' "
            sql &= ",admission_detail as 'Braden scale was ' "
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
            sql &= " , tqm_report_type_name AS 'Report Type' , repeat_ir_no AS 'Repeat IR No.' "
            sql &= " , replace(dept1.root_cause,'&#x0D;',' , ') AS 'root cause from unit' "
            sql &= " , replace(deptplan.action_plan,'&#x0D;',' , ') AS 'action plan from unit' "
            sql &= " FROM ir_pressure_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id INNER JOIN ir_detail_tab c ON a.ir_id = c.ir_id "
            'sql &= "INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "

            If reporttype = "sup" Then
                sql &= " LEFT OUTER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            Else
                sql &= " INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            End If

            sql &= "LEFT OUTER JOIN  ir_topic_grand t1 ON d.grand_topic = t1.grand_topic_id "
            sql &= "LEFT OUTER JOIN ir_topic t2 ON d.topic = t2.ir_topic_id "
            sql &= "LEFT OUTER JOIN ir_topic_sub t3 ON d.subtopic1 = t3.ir_subtopic_id "
            sql &= "LEFT OUTER JOIN ir_topic_sub2 t4 ON d.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON d.subtopic3 = t5.ir_subtopic3_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.cause_detail  "
            sql &= " FROM ir_relate_dept t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'root_cause'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) dept1 ON a.ir_id = dept1.ir_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.action_detail  "
            sql &= " FROM ir_dept_prevent_list t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'action_plan'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) deptplan ON a.ir_id = deptplan.ir_id "

            sql &= " WHERE ISNULL(b.is_delete,0) = 0 And ISNULL(b.is_cancel,0) = 0 And b.form_id = 6 "

            If reporttype = "sup" Then
                sql &= " AND b.status_id >= 2"
            Else
                sql &= " AND b.status_id > 2"
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            'Response.Write(sql)
            GridView1.DataSource = ds
            GridView1.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindPheb()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql &= "SELECT a.ir_id , 'IR' + cast(c.ir_no as varchar) AS 'IR NO.' "
            sql &= " , c.pt_name , c.service_type AS patient_type  , c.describe AS 'Detail' , d.action_detail AS 'Corrective Action' "
            sql &= ", t1.grand_topic_name , t2.topic_name , t3.subtopic_name "
            sql &= ",case when infiltration_type = 1 then 'Stage 1' when infiltration_type = 2 then 'Stage 2'"
            sql &= " when infiltration_type = 3 then 'Stage 3' when infiltration_type = 4 then 'Stage 4' else ''"
            sql &= " end AS 'Infiltration'"
            sql &= " ,case when phlebitis_type = 1 then 'Stage 1' when phlebitis_type = 2 then 'Stage 2'"
            sql &= " when phlebitis_type = 3 then 'Stage 3' when phlebitis_type = 4 then 'Stage 4' "
            sql &= " when phlebitis_type = 5 then 'Stage 5' when phlebitis_type = -1 then 'Stage 0' else ''  end AS 'Phlebitis'"

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
            sql &= " , tqm_report_type_name AS 'Report Type' , repeat_ir_no AS 'Repeat IR No.' "
            sql &= " , replace(dept1.root_cause,'&#x0D;',' , ') AS 'root cause from unit' "
            sql &= " , replace(deptplan.action_plan,'&#x0D;',' , ') AS 'action plan from unit' "
            sql &= "  FROM ir_phlebitis_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id INNER JOIN ir_detail_tab c ON a.ir_id = c.ir_id"
            'sql &= " INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "

            If reporttype = "sup" Then
                sql &= " LEFT OUTER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            Else
                sql &= " INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            End If

            sql &= " LEFT OUTER JOIN  ir_topic_grand t1 ON d.grand_topic = t1.grand_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic t2 ON d.topic = t2.ir_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub t3 ON d.subtopic1 = t3.ir_subtopic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub2 t4 ON d.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON d.subtopic3 = t5.ir_subtopic3_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.cause_detail  "
            sql &= " FROM ir_relate_dept t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'root_cause'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) dept1 ON a.ir_id = dept1.ir_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.action_detail  "
            sql &= " FROM ir_dept_prevent_list t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'action_plan'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) deptplan ON a.ir_id = deptplan.ir_id "

            sql &= " WHERE ISNULL(b.is_delete,0) = 0 And ISNULL(b.is_cancel,0) = 0  AND b.form_id = 3 "

            If reporttype = "sup" Then
                sql &= " AND b.status_id >= 2"
            Else
                sql &= " AND b.status_id > 2"
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            ' Response.Write(sql)
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindFall()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try

            sql &= "SELECT  a.ir_id , 'IR' + cast(c.ir_no as varchar) AS 'IR No.'"
            sql &= " , c.pt_name , c.service_type AS patient_type  , c.describe AS 'Detail' , d.action_detail AS 'Corrective Action' "
            sql &= ", t1.grand_topic_name , t2.topic_name , t3.subtopic_name "
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
            sql &= " when activity_at_fall = 3 then 'Toileting' when activity_at_fall = 4 then 'Moving about in bed'"
            sql &= " when activity_at_fall = 5 then 'Walking' when activity_at_fall = 6 then 'Rehabilitation'"
            sql &= " when activity_at_fall = 7 then 'Sitting in a chair' when activity_at_fall = 8 then 'Sitting in a commode'"
            sql &= " when activity_at_fall = 9 then 'Sitting in a wheelchair' when activity_at_fall = 10 then 'Sitting in a stretcher'"
            sql &= " when activity_at_fall = 11 then 'Other' else '' end AS 'Activity at Time of fall'"
            sql &= " ,activity_remark"
            sql &= " ,case when a.location = 1 then 'Patient''s room' when a.location = 2 then 'Patient''s toilet'"
            sql &= " when a.location = 3 then 'Public toilet' when a.location = 4 then 'Public area '"
            sql &= " when a.location = 5 then 'Other' else '' end AS 'Location'"
            sql &= " ,location_remark"
            '  sql &= " ,case when is_renovation = 1 then 'Yes' else 'No' end AS 'Renovation area'"
            sql &= " ,case when is_has_assist = 1 then 'Yes' else 'No' end AS 'Having assistant during fall'"
            sql &= " ,case when vital_flag = 1 then 'Normal' when vital_flag =0 then 'Abnormal' else '' end AS 'Post Fall : Vital sign/Neuro sign'"
            sql &= " ,vital_remark"
            sql &= " ,case when is_examination = 1 then 'Yes' when is_examination = 0 then 'No' else '' end AS 'Examination conducted'"
            sql &= " ,exam_doctor"
            sql &= " ,ISNULL(date_exam,'') AS date_exam "
            ' sql &= " ,date_exam_ts"
            sql &= " ,treatment_detail"
            sql &= " ,refuse_detail"
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
            sql &= " ,case when chk_rn_care1 = 1 then 'Yes' else 'No' end AS 'Mobility assisting high risk patient during transferation'"
            sql &= " ,case when chk_rn_care2 = 1 then 'Yes' else 'No' end AS 'Provide assistance for toileting every time of nursing care intervention'"
            sql &= " ,case when chk_rn_care3 = 1 then 'Yes' else 'No' end AS 'Reviewed list of medication by pharmacist'"
            sql &= " ,case when chk_rn_care4 = 1 then 'Yes' else 'No' end AS 'Having assistance in toilet or sitting on commode'"
            sql &= " ,case when chk_rn_care5 = 1 then 'Yes' else 'No' end AS 'Provide toileting assistance every 2 hr or depends on patient''s condition'"
            sql &= " ,case when chk_rn_care6 = 1 then 'Yes' else 'No' end AS 'Position patient in easily observable area'"
            sql &= " ,case when chk_rn_care7 = 1 then 'Yes' else 'No' end AS 'Consider having sitter at all time'"
            sql &= " ,case when chk_rn_care8 = 1 then 'Yes' else 'No' end AS 'Other'"
            sql &= " ,rn_care_remark"
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
            sql &= "  ,case when chk_equip11 = 1 then 'Yes' else 'No' end AS 'Other'"
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
            sql &= " ,case when chk_inform1 = 1 then 'Yes' else 'No' end AS 'Re-oriented confused patients'"
            sql &= " ,case when chk_inform2 = 1 then 'Yes' else 'No' end AS 'Educate / Instruct patient and relative about fall prevention'"
            sql &= " ,case when chk_inform3 = 1 then 'Yes' else 'No' end AS 'Orientating patient to bed area / ward facilities and not to get assistance'"
            sql &= " ,ISNULL(date_assess,'') AS date_assess"
            ' sql &= " ,date_assess_ts"
            sql &= " ,assess_reason"
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
            sql &= "   when severity_outcome = 5 then 'Level 4'  else '' end AS 'Severity outcome'"
            sql &= " , nation_type AS Nationality , nation_remark"
            sql &= " , '' AS response_unit1 , '' AS response_unit2 , '' AS response_unit3 "
            sql &= " , nation_type AS 'Nationality'"
            sql &= " , tqm_report_type_name AS 'Report Type' , repeat_ir_no AS 'Repeat IR No.' "
            sql &= " , replace(dept1.root_cause,'&#x0D;',' , ') AS 'root cause from unit' "
            sql &= " , replace(deptplan.action_plan,'&#x0D;',' , ') AS 'action plan from unit' "
            sql &= " FROM ir_fall_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id"
            sql &= " INNER JOIN ir_detail_tab c ON a.ir_id = c.ir_id"

            If reporttype = "sup" Then
                sql &= " LEFT OUTER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            Else
                sql &= " INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            End If


            sql &= " LEFT OUTER JOIN  ir_topic_grand t1 ON d.grand_topic = t1.grand_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic t2 ON d.topic = t2.ir_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub t3 ON d.subtopic1 = t3.ir_subtopic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub2 t4 ON d.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON d.subtopic3 = t5.ir_subtopic3_id "
            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.cause_detail  "
            sql &= " FROM ir_relate_dept t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'root_cause'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) dept1 ON a.ir_id = dept1.ir_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.action_detail  "
            sql &= " FROM ir_dept_prevent_list t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'action_plan'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) deptplan ON a.ir_id = deptplan.ir_id "

            sql &= " WHERE ISNULL(b.is_delete,0) = 0 And ISNULL(b.is_cancel,0) = 0  AND b.form_id = 1 "

            If reporttype = "sup" Then
                sql &= " AND b.status_id >= 2"
            Else
                sql &= " AND b.status_id > 2"
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If


            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            ' Response.Write(sql)
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindMed()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT  a.ir_id , 'IR' + cast(c.ir_no as varchar) AS 'IR NO' "
            sql &= " , c.pt_name , c.service_type AS patient_type  , c.describe AS 'Detail' , d.action_detail AS 'Corrective Action' "
            sql &= ", t1.grand_topic_name AS 'Grand topic' , t2.topic_name AS 'Topic' , t3.subtopic_name  AS 'Subtopic1' "
            'sql &= ", case when chk_wrongtime = 1 then 'Yes' else 'No' end AS 'Wrong time'"
            'sql &= ", case when chk_wrongroute= 1 then 'Yes' else 'No' end AS 'Wrong route'"
            'sql &= ", case when chk_wrongdate= 1 then 'Yes' else 'No' end AS 'Wrong date'"
            'sql &= ", case when chk_wrongrate= 1 then 'Yes' else 'No' end AS 'Wrong rate'"
            'sql &= ", case when chk_omission= 1 then 'Yes' else 'No' end AS 'Omission'"
            'sql &= ", case when chk_wronglabel= 1 then 'Yes' else 'No' end AS 'Wrong label'"
            'sql &= ", case when chk_wrongform= 1 then 'Yes' else 'No' end AS 'Wrong form'"
            'sql &= ", case when chk_wrongdose= 1 then 'Yes' else 'No' end AS 'Wrong dose'"
            'sql &= ", case when chk_extradose= 1 then 'Yes' else 'No' end AS 'Extra dose'"
            'sql &= ", case when chk_wrongiv= 1 then 'Yes' else 'No' end AS 'Wrong IV'"
            'sql &= ", case when chk_wrong_deteriorate= 1 then 'Yes' else 'No' end AS 'Wrong deteriorate'"
            'sql &= ", case when chk_wrong_prep= 1 then 'Yes' else 'No' end AS 'Wrong drug preparation'"
            'sql &= ", case when chk_wrong_drugerror= 1 then 'Yes' else 'No' end AS 'Unauthorized drug error'"
            'sql &= ", case when chk_wrong_duplicate= 1 then 'Yes' else 'No' end AS 'Duplicate drug'"
            'sql &= ", case when chk_wrong_formular= 1 then 'Yes' else 'No' end AS 'Wrong formula'"
            'sql &= ", case when chk_wrong_qty= 1 then 'Yes' else 'No' end AS 'wrong quantity'"
            'sql &= ", case when chk_wrong_drug= 1 then 'Yes' else 'No' end AS 'Wrong drug'"
            'sql &= ", case when chk_wrong_pt= 1 then 'Yes' else 'No' end AS 'Wrong patient'"
            'sql &= ", case when chk_allergy= 1 then 'Yes' else 'No' end AS 'known allergy information'"
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
            sql &= " , '' AS response_unit1 , '' AS response_unit2 , '' AS response_unit3 "
            sql &= " , ISNULL(t4.subtopic2_name_th,'') AS 'subtopic2'  , ISNULL(t5.subtopic3_name_th,'') AS 'subtopic3' "
            sql &= ", '' AS 'High Alert' , '' AS 'Look Alike/Sound Alike', '' AS 'Floor stock', '' AS 'Smart/Infusion Pump' , '' AS 'BCMA'  "
            sql &= " , tqm_report_type_name AS 'Report Type' , repeat_ir_no AS 'Repeat IR No.' "
            sql &= " , MONTH(b.date_submit) AS 'Submit Month' , c.division "
            sql &= " , replace(dept1.root_cause,'&#x0D;',' , ') AS 'root cause from unit' "
            sql &= " , replace(deptplan.action_plan,'&#x0D;',' , ') AS 'action plan from unit' "
            sql &= " FROM ir_med_tab a INNER JOIN ir_trans_list b ON b.ir_id = a.ir_id INNER JOIN ir_detail_tab c ON b.ir_id = c.ir_id"
            ' sql &= " INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            If reporttype = "sup" Then
                sql &= " LEFT OUTER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            Else
                sql &= " INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            End If

            sql &= " LEFT OUTER JOIN  ir_topic_grand t1 ON d.grand_topic = t1.grand_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic t2 ON d.topic = t2.ir_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub t3 ON d.subtopic1 = t3.ir_subtopic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub2 t4 ON d.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON d.subtopic3 = t5.ir_subtopic3_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.cause_detail  "
            sql &= " FROM ir_relate_dept t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'root_cause'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) dept1 ON a.ir_id = dept1.ir_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.action_detail  "
            sql &= " FROM ir_dept_prevent_list t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'action_plan'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) deptplan ON a.ir_id = deptplan.ir_id "

            sql &= " WHERE ISNULL(b.is_delete,0) = 0 And ISNULL(b.is_cancel,0) = 0  AND b.form_id = 2 "

            If reporttype = "sup" Then
                sql &= " AND b.status_id >= 2"
            Else
                sql &= " AND b.status_id > 2"
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

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

    Sub bindMCO()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try

            sql &= "SELECT  a.ir_id , 'IR' + cast(c.ir_no as varchar) AS 'IR No.'"
            sql &= " , c.pt_name , c.service_type AS patient_type  , c.describe AS 'Detail' , d.action_detail AS 'Corrective Action' "
            sql &= ", t1.grand_topic_name , t2.topic_name , t3.subtopic_name "

            sql &= " , a.summary_detail , a.compensation , a.conclusion , a.remark_psm , a.is_legal , a.date_close"
            sql &= " , a.resp_person , a.psm_compensation , a.psm_dianosis , a.psm_recommendation , a.psm_remark "
            sql &= " , psm_status_name , a.psm_pt_satisfaction_name , a.psm_person "


            sql &= " FROM ir_psm_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id"
            sql &= " INNER JOIN ir_detail_tab c ON a.ir_id = c.ir_id"
            ' sql &= " INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "

            If reporttype = "sup" Then
                sql &= " LEFT OUTER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            Else
                sql &= " INNER JOIN ir_tqm_tab d ON a.ir_id = d.ir_id "
            End If

            sql &= " LEFT OUTER JOIN  ir_topic_grand t1 ON d.grand_topic = t1.grand_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic t2 ON d.topic = t2.ir_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub t3 ON d.subtopic1 = t3.ir_subtopic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub2 t4 ON d.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON d.subtopic3 = t5.ir_subtopic3_id "
            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.cause_detail  "
            sql &= " FROM ir_relate_dept t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'root_cause'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) dept1 ON a.ir_id = dept1.ir_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.action_detail  "
            sql &= " FROM ir_dept_prevent_list t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'action_plan'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) deptplan ON a.ir_id = deptplan.ir_id "

            sql &= " WHERE ISNULL(b.is_delete,0) = 0 And ISNULL(b.is_cancel,0) = 0   "

            If reporttype = "sup" Then
                sql &= " AND b.status_id >= 2"
            Else
                sql &= " AND b.status_id > 2"
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            sql &= " ORDER BY c.ir_no"

            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            ' Response.Write(sql)
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT  "
            sql &= " a.ir_id , "
            sql &= "   SUBSTRING(cast(ir_no as varchar),9,4) AS 'No', SUBSTRING(cast(ir_no as varchar),5,2) AS 'Month' , CONVERT(varchar, a.date_submit, 103)  AS 'Date of Received (dd/mm/yy)' ,  CONVERT(varchar, b.datetime_report, 103)  AS 'Date of Event (dd/mm/yy)' "
            If reporttype <> "sup" Then
                sql &= "  , (SELECT TOP 1 log_time FROM ir_status_log WHERE status_id = 3 AND ir_id = a.ir_id ORDER BY log_time ASC) AS 'Date of Reading (dd/mm/yy)'"
                sql &= ",case  when DATEDIFF(hour, b.datetime_report ,a.date_submit) <= 6 then 'Yes' ELSE 'No' end AS 'Response in 6 hr'"
                sql &= ",case  when DATEDIFF(hour, a.date_submit ,(SELECT TOP 1 log_time FROM ir_status_log WHERE status_id = 3 AND ir_id = a.ir_id ORDER BY log_time ASC)) <= 6 then 'Yes' ELSE 'No' end AS 'Read in 6 hr' "
            End If

            sql &= ", b.division AS 'Division'  , 'IR' + CAST(b.ir_no AS varchar) AS 'Incident No' "
            sql &= ", b.hn AS 'HN' , b.room AS 'Room No' , b.pt_name AS 'Name' "
            ' sql &= ", b.pt_name AS 'Last name'"
            sql &= ", ISNULL(case when b.customer_segment = 1 THEN 'Thai' when b.customer_segment = 2 THEN 'Expatriate' when b.customer_segment = 3 THEN 'International' end,'') AS 'market segment' "
            sql &= ", b.sex AS 'Gender' , b.age AS 'Age' "
            sql &= ", b.service_type AS 'Patient type' "

            If reporttype <> "sup" Then
                sql &= " , '' AS 'PT safety monitor' , ISNULL(c.log_safety_goal,'') AS 'Patient Safety Goals'  , d.severe_name AS 'Type risk'"
            End If

            sql &= ", ISNULL(case when b.clinical_type = 1 THEN 'Clinical' when b.clinical_type = 2 THEN 'Non-Clinical' END,'') AS 'IR type' "

            sql &= ", t1.grand_topic_name AS 'Grand topic' , ISNULL(t2.topic_name,'') AS 'Topic' , ISNULL(t3.subtopic_name,'') AS 'Subtopic1' , ISNULL(t4.subtopic2_name_th,'') AS 'subtopic2' "

            If reporttype <> "sup" Then
                sql &= ", ISNULL(c.tqm_topic_detail,'') AS 'Sub detail', c.log_lab_name AS 'Lab category' , c.log_asa_name AS 'ASA classification' , CAST(DATEPART (hour ,b.datetime_report) AS varchar) + ':' + CAST(DATEPART (MINUTE ,b.datetime_report) AS varchar)  AS 'Time' "
                sql &= ", '' AS 'Drug name' "
                sql &= ", ISNULL(case when med.time_period = 1 THEN 'M shift' when med.time_period  = 2 THEN 'E shift' when med.time_period  = 3 THEN 'N shift' END,'') AS 'Time of Med error' "
                sql &= ", ISNULL(case when fall.period_fall = 1 THEN 'M shift' when fall.period_fall = 2 THEN 'E shift' when fall.period_fall = 3 THEN 'N shift' END,'') AS 'Time of Fall' "
                sql &= ", ISNULL(case when fall.activity_at_fall = 1 THEN 'Transferring into or out of bed' when fall.activity_at_fall = 2 THEN 'Transferring in or out of chair' when fall.activity_at_fall = 3 THEN 'Toileting' when fall.activity_at_fall = 4 THEN 'Moving' when fall.activity_at_fall = 5 THEN 'Walking' "
                sql &= "  when fall.activity_at_fall = 6 THEN 'Rehabilitation' when fall.activity_at_fall = 7 THEN 'Sitting in a chair' when fall.activity_at_fall = 8 THEN 'Sitting in a commode' "
                sql &= "  when fall.activity_at_fall = 9 THEN 'Sitting in a wheelchair' when fall.activity_at_fall = 10 THEN 'Sitting in a stretcher' when fall.activity_at_fall = 11 THEN 'Other' END,'') AS 'Activity at Time of Fall' "
                sql &= " , ISNULL(case when fall.location = 1 THEN 'Patient''s room' when fall.location = 2 THEN 'Patient''s toilet' when fall.location = 3 THEN 'Public toilet' when fall.location = 4 THEN 'Public area' when fall.location = 5 THEN 'Other' END,'') AS 'Location of fall' "
                sql &= ", case when lasa.lasa_id = 1 or lasa.lasa_id = 2 then 'Yes' end AS 'LASA'"
                sql &= ", ISNULL(case when med.is_mar_error = 1 THEN 'Yes' when med.is_mar_error  = 0 THEN 'No' END,'') AS 'MAR error' "
                sql &= ", ISNULL(case when med.is_robot_product = 1 THEN 'Yes' when med.is_robot_product  = 0 THEN 'No' END,'') AS 'Robot' "
                sql &= ", ISNULL(case when med.is_cpoe = 1 THEN 'Yes' when med.is_cpoe  = 0 THEN 'No' END,'') AS 'CPOE' "
                sql &= ", ISNULL(case when med.chk_err_order = 1 THEN 'Yes' else 'No' END,'') AS 'P-Prescribing/ordering' "
                sql &= ", ISNULL(case when med.chk_err_transcription = 1 THEN 'Yes' else 'No'  END,'') AS 'T-Transcription' "
                sql &= ", ISNULL(case when med.chk_err_key = 1 THEN 'Yes'  else 'No'  END,'') AS 'K-Key order entry' "
                sql &= ", ISNULL(case when med.chk_err_verify = 1 THEN 'Yes'  else 'No'  END,'') AS 'V-verification' "
                sql &= ", ISNULL(case when med.chk_err_predis = 1 THEN 'Yes'  else 'No'  END,'') AS 'Pre-dispensing' "
                sql &= ", ISNULL(case when med.chk_err_dispensing = 1 THEN 'Yes'  else 'No'  END,'') AS 'D-Dispensing' "
                sql &= ", ISNULL(case when med.chk_err_preadmin = 1 THEN 'Yes'  else 'No'  END,'') AS 'pre-administration' "
                sql &= ", ISNULL(case when med.chk_err_admin = 1 THEN 'Yes'  else 'No'  END,'') AS 'A-administration' "
                sql &= ", ISNULL(case when med.chk_err_monitor = 1 THEN 'Yes'  else 'No'  END,'') AS 'M-Monitoring' "
            End If

            sql &= ", b.diagnosis AS 'Diagnosis' , b.operation AS 'Procedure' "
            sql &= ", b.describe AS 'Detail' , c.tqm_remark AS 'More Information' "

            If reporttype = "sup" Then
                sql &= " , b.physician AS 'Attending Physician' , b.initial_action AS 'initial action' , status.ir_status_name AS 'Status' "
            End If

            If reporttype <> "sup" Then
                ' ดึงจาก TQM Cause
                sql &= " , 'No' AS 'Personal / Human Error' "
                sql &= " , 'No' AS 'Communication Error' "
                sql &= " , 'No' AS 'System Error' "
                sql &= " , 'No' AS 'Equipment Error' "
                sql &= " , 'No' AS 'Enviroment' "
                sql &= " , 'No' AS 'Poor Practice Habit' "
                sql &= " , 'No' AS 'Others' "
                sql &= " , 'No' AS 'Patient''s factor' "



                sql &= ", case when fall.chk_alzheimer = 1 or fall.chk_sedative = 1 or fall.chk_analgesic = 1  or fall.chk_diuretic = 1 or fall.chk_beta = 1  or fall.chk_laxative = 1  or fall.chk_antiepil= 1  "
                sql &= " or fall.chk_narcotic = 1 or chk_benzo = 1 or fall.chk_other1 = 1 "
                sql &= " then 'Yes' else 'No' end AS 'Medication last 24 hrs' "

                sql &= ", case when chk_pt1 = 1 or chk_pt2 = 1 or chk_pt3 = 1  or chk_pt4 = 1 or chk_pt5 = 1  or chk_pt5 = 1  or chk_pt6= 1  "
                sql &= " or chk_pt7 = 1 or chk_pt8 = 1 or chk_pt9 = 1 or chk_pt10 = 1  or chk_pt11 = 1  or chk_pt12 = 1 or chk_pt13 = 1 "
                sql &= " or chk_pt14 = 1 then 'Yes' else 'No' end AS 'Patient''s Factor'"

                sql &= ", fall.post_procedure AS 'Post operative / Procedure' "
                sql &= ", '' AS ' root cause from TQM' , c.action_detail AS 'action plan from TQM ' , '' AS 'Response Unit 1' , '' AS 'Response Unit 2' "
                sql &= ", '' AS 'Response Unit 3', ISNULL(doctor.md_code,'') AS 'Physician Code' , ISNULL(doctor.doctor_name,'') AS 'Physician' , ISNULL(doctor.specialty_name,'') AS 'Specialty' , doctor.md_code "
                sql &= ", '' AS 'Sending (รอตอบกลับ)' "
                sql &= ", case when ISNULL(deptLog.ir_id,0) > 0 THEN 'Yes' else 'No'  END AS 'Sending' "
                sql &= ", ISNULL(c.tqm_case_owner,'') AS 'TQM Staff'  "
                sql &= ", case when ISNULL(deptLog.log_time_ts,0) > 0 then deptLog.log_time  end AS 'Sending Date (รอตอบกลับ)' "
                sql &= ", case when ISNULL(mcoLog.log_time_ts,0) > 0 then mcoLog.log_time  end AS ',Sending Date (case refer)' "
                sql &= ", case  when DATEDIFF(hour, a.date_submit ,deptLog.log_time) <= 24 then 'Yes' ELSE 'No' end AS 'Sending within 24 hr' "
                sql &= ", '' AS 'Follow up date (dd/mm/yy)' "
                sql &= ", case when ISNULL(deptReturn.log_time_ts,0) > 0 then deptReturn.log_time  end AS 'Date of Return (dd/mm/yy)' "
                sql &= ", DATEDIFF(day, deptLog.log_time , deptReturn.log_time ) AS 'Day' "
                sql &= ", case  when DATEDIFF(hour, deptLog.log_time , deptReturn.log_time ) <= 48 then 'Yes' ELSE 'No' end AS 'Return within 48 hr' "
                sql &= ", case  when DATEDIFF(hour, deptLog.log_time , deptReturn.log_time ) > 48 AND DATEDIFF(week, deptLog.log_time , deptReturn.log_time ) <= 1 then 'Yes' ELSE 'No' end AS 'Return within >48 hr- 1 wk' "
                sql &= ", case  when DATEDIFF(week, deptLog.log_time , deptReturn.log_time ) > 1 AND DATEDIFF(month, deptLog.log_time , deptReturn.log_time ) <= 1 then 'Yes' ELSE 'No' end AS 'Return within >1 wk - 1 month' "
                sql &= ", case  when DATEDIFF(month, deptLog.log_time , deptReturn.log_time ) >1 then 'Yes' ELSE 'No' end AS 'Return within >1 month' "
                sql &= ", '' AS 'Service Recovery' "
                sql &= ", case when ISNULL(relateDoc.ir_id,0) > 0 then 'Yes' else 'No' end AS 'Related to CFB' "
                ' sql &= ", '' AS 'convert to CFB', '' AS 'Related to FMS' "
                sql &= ", case when c.is_refer = 1 then 'Yes' else 'No' end AS 'Refer to MCO' "
                sql &= ", case when c.is_concern = 1 then 'Yes' else 'No' end AS 'Quality Concern' "
                sql &= ", case when a.status_id = 9 then 'Closed' else 'Open' end AS 'Status' "
                sql &= ", case when ISNULL(closeCase.log_time_ts,0) > 0 then closeCase.log_time  end AS 'Date of close (dd/mm/yy)' "
                sql &= ", case  when DATEDIFF(month, a.date_submit , closeCase.log_time ) <= 1 then 'Yes' when DATEDIFF(month, a.date_submit , closeCase.log_time )>1 then 'No' ELSE '' end AS 'Close with in 1 month' "
                sql &= " , c.tqm_report_type_name AS 'Report_type' , c.repeat_ir_no AS 'Repeat IR No.' , subtopic3_name_en AS 'Sub topic3' "
                sql &= " , CASE WHEN c.tqm_chk_write = 1 THEN 'Yes' ELSE 'No' END AS 'Write Off' , c.tqm_write_bath AS 'Write Off (THB)'"
                sql &= " , c.tqm_write_dept_name AS 'Write Off Dept. Unit' "
                sql &= " , CASE WHEN c.tqm_chk_refund = 1 THEN 'Yes' ELSE 'No' END AS 'Refund' , c.tqm_refund_bath AS 'Refund (THB)'"
                sql &= " , c.tqm_refund_dept_name AS 'Refund Dept. Unit' "
                sql &= " , CASE WHEN c.tqm_chk_remove = 1 THEN 'Yes' ELSE 'No' END AS 'Remove' , c.tqm_remove_bath AS 'Remove (THB)'"
                sql &= " , c.tqm_remove_dept_name AS 'Remove Dept. Unit' "
                sql &= " , b.location , replace(dept1.root_cause,'&#x0D;',' , ') AS 'Root Cause from unit' "
                sql &= " , replace(deptplan.action_plan,'&#x0D;',' , ') AS 'Action plan from unit' "
            End If

            '  sql &= " , tqm_report_type_name AS 'Report Type' , repeat_ir_no AS 'Repeat IR No.' "
            sql &= " FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id "

            If reporttype = "sup" Then
                sql &= " LEFT OUTER JOIN ir_tqm_tab c ON a.ir_id = c.ir_id "
            Else
                sql &= " INNER jOIN ir_tqm_tab c ON a.ir_id = c.ir_id "
            End If

            sql &= " LEFT OUTER JOIN ir_m_severity d ON c.severe_level_id = d.severe_id "
            sql &= " LEFT OUTER JOIN  ir_topic_grand t1 ON c.grand_topic = t1.grand_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic t2 ON c.topic = t2.ir_topic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub t3 ON c.subtopic1 = t3.ir_subtopic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub2 t4 ON c.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON c.subtopic3 = t5.ir_subtopic3_id "
            sql &= " LEFT OUTER JOIN ir_fall_tab fall ON a.ir_id = fall.ir_id "
            sql &= " LEFT OUTER JOIN ir_med_tab med ON a.ir_id = med.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT ir_id , MAX(doctor_name) AS doctor_name , MAX(md_code) AS md_code , MAX(a2.specialty) AS specialty_name "
            sql &= " FROM ir_doctor_defendant a1 INNER JOIN m_doctor a2 ON a1.md_code = a2.emp_no GROUP BY ir_id) doctor ON a.ir_id = doctor.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT ir_id , MIN(log_time) AS log_time ,  MIN(log_time_ts) AS log_time_ts FROM ir_status_log WHERE status_id = 4 GROUP BY ir_id  ) deptLog ON a.ir_id = deptLog.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT ir_id , MIN(log_time) AS log_time ,  MIN(log_time_ts) AS log_time_ts FROM ir_status_log WHERE status_id = 5 GROUP BY ir_id) mcoLog ON a.ir_id = mcoLog.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT ir_id , MAX(log_time) AS log_time ,  MAX(log_time_ts) AS log_time_ts FROM ir_status_log WHERE status_id = 7 GROUP BY ir_id) deptReturn ON a.ir_id = deptReturn.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT ir_id FROM ir_relate_document WHERE reference_type='ir' GROUP BY ir_id) relateDoc on a.ir_id = relateDoc.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT ir_id , MAX(log_time) AS log_time ,  MAX(log_time_ts) AS log_time_ts FROM ir_status_log WHERE status_id = 9 GROUP BY ir_id) closeCase ON a.ir_id = closeCase.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT Top 1 * FROM ir_med_tab_drug WHERE lasa_id IN (1 ,2 ) ) lasa ON a.ir_id = lasa.ir_id"
            sql &= " LEFT OUTER JOIN (SELECT TOP 1 * FROM ir_tqm_cause_list WHERE  cause_id = 1) cause1 ON a.ir_id = cause1.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT TOP 1 * FROM ir_tqm_cause_list WHERE  cause_id = 2) cause2 ON a.ir_id = cause2.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT TOP 1 * FROM ir_tqm_cause_list WHERE  cause_id = 3) cause3 ON a.ir_id = cause3.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT TOP 1 * FROM ir_tqm_cause_list WHERE  cause_id = 4) cause4 ON a.ir_id = cause4.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT TOP 1 * FROM ir_tqm_cause_list WHERE  cause_id = 5) cause5 ON a.ir_id = cause5.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT TOP 1 * FROM ir_tqm_cause_list WHERE  cause_id = 6) cause6 ON a.ir_id = cause6.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT TOP 1 * FROM ir_tqm_cause_list WHERE  cause_id = 7) cause7 ON a.ir_id = cause7.ir_id "
            sql &= " LEFT OUTER JOIN (SELECT TOP 1 * FROM ir_tqm_cause_list WHERE  cause_id = 8) cause8 ON a.ir_id = cause8.ir_id "
            '   sql &= " LEFT OUTER JOIN ir_relate_dept "
            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.cause_detail  "
            sql &= " FROM ir_relate_dept t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'root_cause'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) dept1 ON a.ir_id = dept1.ir_id "

            sql &= " LEFT OUTER JOIN ( "
            sql &= " SELECT "
            sql &= " t11.ir_id,"
            sql &= " STUFF(("
            sql &= " SELECT ', ' + t22.action_detail  "
            sql &= " FROM ir_dept_prevent_list t22"
            sql &= " WHERE t22.ir_id = t11.ir_id"
            sql &= " FOR XML PATH (''))"
            sql &= " ,1,2,'') AS 'action_plan'"
            sql &= " FROM ir_relate_dept t11"
            sql &= " GROUP BY t11.ir_id ) deptplan ON a.ir_id = deptplan.ir_id "

            sql &= " INNER JOIN ir_status_list status ON a.status_id = status.ir_status_id "

            sql &= " WHERE a.report_type = 'ir' AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0  "

            If reporttype = "sup" Then
                sql &= " AND a.status_id >= 2"
            Else
                sql &= " AND a.status_id > 2"
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            ' Response.Write(sql)
            GridView1.DataSource = ds
            GridView1.DataBind()

            '   GridView1.Columns(0).Visible = False
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
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


    Private Sub ExportData(ByVal _contentType As String, ByVal fileName As String)

        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = _contentType
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        frm.Attributes("runat") = "server"
        frm.Controls.Add(GridView1)
        GridView1.RenderControl(htw)

        Response.Write(sw.ToString())
        Response.[End]()
    End Sub
    Protected Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        Export("logbook.xls", GridView1)
        'ExportData("application/vnd.xls", "FileName.xls")
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If txtselect.SelectedValue = "1" Then
            bindGrid()
        ElseIf txtselect.SelectedValue = "2" Then
            bindMed()
        ElseIf txtselect.SelectedValue = "3" Then
            bindFall()
        ElseIf txtselect.SelectedValue = "4" Then
            bindPheb()
        ElseIf txtselect.SelectedValue = "5" Then
            bindDelete()
        ElseIf txtselect.SelectedValue = "6" Then
            bindPressure()
        ElseIf txtselect.SelectedValue = "7" Then
            bindMCO()
        End If

    End Sub

    Protected Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        txtdate1.Text = ""
        txtdate2.Text = ""
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If reporttype <> "sup" Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                '  Response.Write(e.Row.Cells(0).Text & "<br/>")
                Dim sql As String
                Dim ds As New DataSet
                Dim limit As String = ""

                If txtselect.SelectedValue = "1" Then
                    Try
                        sql = "SELECT * FROM (SELECT row_number () over (order by dept_unit_name) as num , * FROM ir_cfb_unit_defendant WHERE ir_id = " & e.Row.Cells(0).Text & ") t1 "
                        sql &= " WHERE num <=3 "
                        ds = conn.getDataSet(sql, "t1")
                        If conn.errMessage <> "" Then
                            Throw New Exception(conn.errMessage)
                        End If
                        ' If ds.Tables("t1").Rows.Count > 0 Then
                        'e.Row.Cells(65).Text = ds.Tables("t1").Rows(0)("dept_name").ToString
                        ' End If

                        ' Defendant Unit
                        For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                            ' e.Row.Cells(65 + i).Text = ds.Tables("t1").Rows(i)("dept_unit_name").ToString
                            e.Row.Cells(64 + i).Text = ds.Tables("t1").Rows(i)("dept_unit_name").ToString
                        Next i

                        sql = "SELECT * FROM (SELECT row_number () over (order by dept_name) as num , * FROM ir_relate_dept WHERE ir_id = " & e.Row.Cells(0).Text & ") t1 "
                        sql &= " WHERE num <=3 "
                        ds = conn.getDataSet(sql, "t2")
                        If conn.errMessage <> "" Then
                            Throw New Exception(conn.errMessage)
                        End If

                        For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1
                            ' e.Row.Cells(71).Text &= " " & ds.Tables("t2").Rows(i)("dept_name").ToString
                            e.Row.Cells(71).Text &= " " & ds.Tables("t2").Rows(i)("dept_name").ToString
                        Next i

                        sql = "SELECT * FROM ir_tqm_cause_list WHERE ir_id= " & e.Row.Cells(0).Text & " ORDER BY order_sort ASC"
                        ' Response.Write(sql)
                        ds = conn.getDataSet(sql, "t3")
                        If conn.errMessage <> "" Then
                            Throw New Exception(conn.errMessage)
                        End If
                        ' TQM Cause
                        For i As Integer = 0 To ds.Tables("t3").Rows.Count - 1
                            If i = 0 Then
                                limit = ""
                            Else
                                limit = ","
                            End If


                            Do While 1 = 1
                                If CInt(ds.Tables("t3").Rows(i)("cause_id").ToString()) = 1 Then
                                    e.Row.Cells(51).Text = "Yes"
                                    Exit Do
                                Else
                                    e.Row.Cells(51).Text = "No"
                                End If

                                If CInt(ds.Tables("t3").Rows(i)("cause_id").ToString()) = 2 Then
                                    e.Row.Cells(52).Text = "Yes"
                                    Exit Do
                                Else
                                    e.Row.Cells(52).Text = "No"
                                End If

                                If CInt(ds.Tables("t3").Rows(i)("cause_id").ToString()) = 3 Then
                                    e.Row.Cells(53).Text = "Yes"
                                    Exit Do
                                Else
                                    e.Row.Cells(53).Text = "No"
                                End If

                                If CInt(ds.Tables("t3").Rows(i)("cause_id").ToString()) = 4 Then
                                    e.Row.Cells(54).Text = "Yes"
                                    Exit Do
                                Else
                                    e.Row.Cells(54).Text = "No"
                                End If

                                If CInt(ds.Tables("t3").Rows(i)("cause_id").ToString()) = 5 Then
                                    e.Row.Cells(55).Text = "Yes"
                                    Exit Do
                                Else
                                    e.Row.Cells(55).Text = "No"
                                End If

                                If CInt(ds.Tables("t3").Rows(i)("cause_id").ToString()) = 6 Then
                                    e.Row.Cells(56).Text = "Yes"
                                    Exit Do
                                Else
                                    e.Row.Cells(56).Text = "No"
                                End If

                                If CInt(ds.Tables("t3").Rows(i)("cause_id").ToString()) = 7 Then
                                    e.Row.Cells(57).Text = "Yes"
                                    Exit Do
                                Else
                                    e.Row.Cells(57).Text = "No"
                                End If

                                If CInt(ds.Tables("t3").Rows(i)("cause_id").ToString()) = 8 Then
                                    e.Row.Cells(58).Text = "Yes"
                                    Exit Do
                                Else
                                    e.Row.Cells(58).Text = "No"
                                End If


                                Exit Do
                            Loop


                            e.Row.Cells(62).Text &= limit & ds.Tables("t3").Rows(i)("cause_remark").ToString
                        Next i


                        sql = "SELECT * FROM ir_med_tab_drug WHERE ir_id= " & e.Row.Cells(0).Text
                        ds = conn.getDataSet(sql, "t4")
                        If conn.errMessage <> "" Then
                            Throw New Exception(conn.errMessage)
                        End If
                        ' Drug name
                        For i As Integer = 0 To ds.Tables("t4").Rows.Count - 1
                            If i = 0 Then
                                limit = ""
                            Else
                                limit = ","
                            End If
                            e.Row.Cells(29).Text &= limit & ds.Tables("t4").Rows(i)("drug_wrong_name").ToString
                        Next i

                    Catch ex As Exception
                        Response.Write(ex.Message & sql)
                    Finally
                        ds.Dispose()
                    End Try
                ElseIf txtselect.SelectedValue = "2" Then ' Med Error

                    sql = "SELECT * FROM ir_tqm_cause_list WHERE ir_id= " & e.Row.Cells(0).Text & " ORDER BY order_sort ASC"
                    ' Response.Write(sql)
                    ds = conn.getDataSet(sql, "t0")
                    If conn.errMessage <> "" Then
                        Throw New Exception(conn.errMessage)
                    End If
                    ' TQM Cause
                    For i As Integer = 0 To ds.Tables("t0").Rows.Count - 1
                        If i = 0 Then
                            limit = ""
                        Else
                            limit = ","
                        End If


                        Do While 1 = 1
                            If CInt(ds.Tables("t0").Rows(i)("cause_id").ToString()) = 1 Then
                                e.Row.Cells(105).Text = "Yes"
                                Exit Do
                            Else
                                e.Row.Cells(105).Text = "No"
                            End If

                            If CInt(ds.Tables("t0").Rows(i)("cause_id").ToString()) = 2 Then
                                e.Row.Cells(106).Text = "Yes"
                                Exit Do
                            Else
                                e.Row.Cells(106).Text = "No"
                            End If

                            If CInt(ds.Tables("t0").Rows(i)("cause_id").ToString()) = 3 Then
                                e.Row.Cells(107).Text = "Yes"
                                Exit Do
                            Else
                                e.Row.Cells(107).Text = "No"
                            End If

                            If CInt(ds.Tables("t0").Rows(i)("cause_id").ToString()) = 4 Then
                                e.Row.Cells(108).Text = "Yes"
                                Exit Do
                            Else
                                e.Row.Cells(108).Text = "No"
                            End If

                            If CInt(ds.Tables("t0").Rows(i)("cause_id").ToString()) = 5 Then
                                e.Row.Cells(109).Text = "Yes"
                                Exit Do
                            Else
                                e.Row.Cells(109).Text = "No"
                            End If

                            If CInt(ds.Tables("t0").Rows(i)("cause_id").ToString()) = 6 Then
                                e.Row.Cells(110).Text = "Yes"
                                Exit Do
                            Else
                                e.Row.Cells(110).Text = "No"
                            End If

                            If CInt(ds.Tables("t0").Rows(i)("cause_id").ToString()) = 7 Then
                                e.Row.Cells(111).Text = "Yes"
                                Exit Do
                            Else
                                e.Row.Cells(111).Text = "No"
                            End If

                            If CInt(ds.Tables("t0").Rows(i)("cause_id").ToString()) = 8 Then
                                e.Row.Cells(112).Text = "Yes"
                                Exit Do
                            Else
                                e.Row.Cells(112).Text = "No"
                            End If


                            Exit Do
                        Loop

                        e.Row.Cells(115).Text &= limit & ds.Tables("t0").Rows(i)("cause_remark").ToString

                    Next i




                    sql = "SELECT * FROM ir_med_tab_drug WHERE  ir_id= " & e.Row.Cells(0).Text
                    ds = conn.getDataSet(sql, "t4")
                    If conn.errMessage <> "" Then
                        Throw New Exception(conn.errMessage)
                    End If

                    'e.Row.Cells(122).Text = ""
                    For i As Integer = 0 To ds.Tables("t4").Rows.Count - 1
                        If i = 0 Then
                            limit = ""
                        Else
                            limit = ","
                        End If
                        e.Row.Cells(10).Text &= limit & ds.Tables("t4").Rows(i)("drug_group").ToString
                        e.Row.Cells(11).Text &= limit & ds.Tables("t4").Rows(i)("drug_wrong_name").ToString

                        If ds.Tables("t4").Rows(i)("chk_alert").ToString = "Yes" Then
                            e.Row.Cells(122).Text = "Yes"
                        End If

                        If ds.Tables("t4").Rows(i)("lasa_name").ToString = "Look Alike" Or ds.Tables("t4").Rows(i)("lasa_name").ToString = "Sound Alike" Then
                            e.Row.Cells(123).Text = ds.Tables("t4").Rows(i)("lasa_name").ToString
                        End If

                        If ds.Tables("t4").Rows(i)("chk_floor").ToString = "Yes" Then
                            e.Row.Cells(124).Text = "Yes"
                        End If

                        If ds.Tables("t4").Rows(i)("chk_smartpump").ToString = "Yes" Then
                            e.Row.Cells(125).Text = "Yes"
                        End If

                        If ds.Tables("t4").Rows(i)("chk_bcma").ToString = "Yes" Then
                            e.Row.Cells(126).Text = "Yes"
                        End If
                    Next i

                    Dim iLoop As Integer = 0

                    For s As Integer = 1 To 4
                        sql = "SELECT * FROM ir_med_tab_period WHERE  ir_id= " & e.Row.Cells(0).Text
                        sql &= " AND job_type_id = " & s
                        If e.Row.Cells(0).Text = "3536" Then
                            '  Response.Write(sql & "<br/>")
                        End If

                        ds = conn.getDataSet(sql, "tt" & s)
                        If conn.errMessage <> "" Then
                            Throw New Exception(conn.errMessage)
                        End If

                        For i As Integer = 0 To ds.Tables("tt" & s).Rows.Count - 1
                            If i = 0 Then
                                limit = ""
                            Else
                                limit = ","
                            End If
                            '  Response.Write(35 + (s) & "<br/>")
                            e.Row.Cells(35 + (s - 1 + iLoop)).Text &= limit & ds.Tables("tt" & s).Rows(i)("job_type_name").ToString
                            e.Row.Cells(36 + (s - 1 + iLoop)).Text &= limit & ds.Tables("tt" & s).Rows(i)("period_name").ToString
                        Next i

                        iLoop += 1
                    Next

                    sql = "SELECT * FROM (SELECT row_number () over (order by dept_unit_name) as num , * FROM ir_cfb_unit_defendant WHERE ir_id = " & e.Row.Cells(0).Text & ") t1 "
                    sql &= " WHERE num <=3 "
                    ds = conn.getDataSet(sql, "t1")
                    If conn.errMessage <> "" Then
                        Throw New Exception(conn.errMessage)
                    End If
                    ' If ds.Tables("t1").Rows.Count > 0 Then
                    'e.Row.Cells(65).Text = ds.Tables("t1").Rows(0)("dept_name").ToString
                    ' End If

                    ' Defendant Unit
                    For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                        ' e.Row.Cells(65 + i).Text = ds.Tables("t1").Rows(i)("dept_unit_name").ToString
                        e.Row.Cells(117 + i).Text = ds.Tables("t1").Rows(i)("dept_unit_name").ToString
                    Next i

                ElseIf txtselect.SelectedValue = "3" Then ' Fall
                    sql = "SELECT * FROM (SELECT row_number () over (order by dept_unit_name) as num , * FROM ir_cfb_unit_defendant WHERE ir_id = " & e.Row.Cells(0).Text & ") t1 "
                    sql &= " WHERE num <=3 "
                    ds = conn.getDataSet(sql, "t1")
                    If conn.errMessage <> "" Then
                        Throw New Exception(conn.errMessage)
                    End If
                    ' If ds.Tables("t1").Rows.Count > 0 Then
                    'e.Row.Cells(65).Text = ds.Tables("t1").Rows(0)("dept_name").ToString
                    ' End If

                    ' Defendant Unit
                    For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                        ' e.Row.Cells(65 + i).Text = ds.Tables("t1").Rows(i)("dept_unit_name").ToString
                        e.Row.Cells(133 + i).Text = ds.Tables("t1").Rows(i)("dept_unit_name").ToString
                    Next i
                End If



            End If
        End If

   
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
