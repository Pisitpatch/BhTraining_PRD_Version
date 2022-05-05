Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class incident_view_fall
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Private irId As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        irId = Request.QueryString("irId")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
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
            sql &= " ,case when a.location = 1 then 'Patient''s room' when a.location = 1 then 'Patient''s toilet'"
            sql &= " when a.location = 1 then 'Public toilet' when a.location = 1 then 'Public area '"
            sql &= " when a.location = 1 then 'Other' else '' end AS 'Location'"
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
            sql &= " ,date_assess"
            sql &= " ,date_assess_ts"
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
            sql &= "   when severity_outcome = 4 then 'Level 4' else '' end AS 'Severity outcome'"
            sql &= " FROM ir_fall_tab a INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id"
            sql &= " INNER JOIN ir_detail_tab c ON a.ir_id = c.ir_id"
            sql &= " WHERE b.is_delete = 0 And b.is_cancel = 0 And b.status_id > 2 "

            lblwhofall.Text = ds.Tables("t1").Rows(0)("Who falled and age").ToString


            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            ' Response.Write(sql)
            '  GridView1.DataSource = ds
            ' GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
