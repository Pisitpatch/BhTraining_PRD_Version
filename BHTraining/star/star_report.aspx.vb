Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Globalization

Partial Class star_star_report
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '  Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Request.QueryString("viewtype") <> "" Then
            viewtype = Request.QueryString("viewtype")
        Else

        End If

        Session("viewtype") = viewtype & ""



        If Page.IsPostBack Then

        Else ' First time load
         
            'bindReport1()
            ' bindReportDoctor()
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

    Sub bindReport1()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.star_id , CAST(ISNULL(b.star_no,'-') as varchar) AS star_no , complain_status_remark AS Name  , status_name "
            sql &= " , case when complain_status = 1 then 'ผู้ป่วย' when complain_status = 2 then 'ญาติผู้ป่วยหรือเพื่อน'  when complain_status = 3 then 'ผู้มาเยี่ยมไข้' "
            sql &= " when complain_status = 4 then 'พนักงาน'  when complain_status = 5 then 'แพทย์' else '-' end as nominee_type_name "
            sql &= ", d.srp_point , CONVERT(VARCHAR(10),b.datetime_complaint,103)  AS submit_date "
            sql &= "  ,case when award_date > 0 then CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int) , Cast('1900-01-01' As DATETIME)), 103) else '-' end  AS 'award_date' "
            '  sql &= " ,  CONVERT(varchar,DATEADD(d, Cast(((d.award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int), Cast('1900-01-01' As DATETIME)), 103) AS 'award_date'"
            sql &= " , case when feedback_from = 1 then 'แบบฟอร์มดาวเด่น' when feedback_from = 2 then 'แบบฟอร์ม CFB' when feedback_from = 3 then 'Email / Web site ของ ร.พ.' "
            sql &= " when feedback_from = 4 then 'อีเมล์ / แฟกซ์' when feedback_from = 5 then 'โทรศัพท์' when feedback_from = 6 then 'อื่นๆ' "
            sql &= " end  AS feedback , service_detail , country "

            sql &= "  FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id"
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " LEFT OUTER JOIN  star_hr_tab d ON a.star_id = d.star_id "
        

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND ISNULL(a.status_id,0) > 1 "
            sql &= " AND a.status_id > 1 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then

                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(d.award_date,0) > 0 "
                    sql &= " AND d.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY a.star_id DESC"
            ds = conn.getDataSet(sql, "table1")

            ' Response.Write(sql)
            ' Return

            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportDoctor()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select  CONVERT(VARCHAR(10),d.datetime_complaint,103) AS 'Submit Date'  "
            sql &= "  ,case when award_date > 0 then CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int) , Cast('1900-01-01' As DATETIME)), 103) else '-' end  AS 'award_date' "
            '  sql &= " ,  CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int), Cast('1900-01-01' As DATETIME)), 103) AS 'award_date'"
            sql &= ", d.star_no AS 'Star No.' , e.status_name AS 'Status' , d.complain_status_remark AS 'ผู้เสนอชื่อ'"
            sql &= " , case when complain_status = 1 then 'ผู้ป่วย' when complain_status = 2 then 'ญาติผู้ป่วยหรือเพื่อน'  when complain_status = 3 then 'ผู้มาเยี่ยมไข้' "
            sql &= " when complain_status = 4 then 'พนักงาน'  when complain_status = 5 then 'แพทย์' else '-' end as 'ประเภทผู้เสนอ' "
            sql &= " , c.doctor_name as 'ชื่อแพทย์ที่ถูกเสนอ' , f.specialty AS 'Specialty (สาขา)' , c.emp_code as 'Doctor Code' "
            sql &= "  , case when d.feedback_from = 1 then 'แบบฟอร์มดาวเด่น' when d.feedback_from = 2 then 'แบบฟอร์ม CFB' "
            sql &= " when d.feedback_from = 3 then 'Email / Web site ของ ร.พ.' when d.feedback_from = 4 then 'อีเมล์ / แฟกซ์' "
            sql &= " when d.feedback_from = 5 then 'โทรศัพท์' when d.feedback_from = 6 then 'อื่นๆ - ' + d.feedback_from_remark  "
            sql &= " end AS 'Nominate by' , ISNULL(b.srp_point,0) AS 'Staff Recognition Point' "
            sql &= " from star_trans_list a "
            sql &= " left outer join star_hr_tab b on a.star_id = b.star_id"
            sql &= " inner join star_relate_doctor c on a.star_id = c.star_id"
            sql &= " inner join star_detail_tab d on a.star_id = d.star_id"
            sql &= " inner join star_status_list e on a.status_id = e.status_id"
            sql &= " left outer join m_doctor f on c.emp_code = f.emp_no "
            sql &= " where(isnull(a.is_delete, 0) = 0 And isnull(a.is_cancel, 0) = 0) AND ISNULL(a.status_id,0) > 1 "

         
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then

                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND d.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(b.award_date,0) > 0 "
                    sql &= " AND b.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY a.star_id DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportEmployee()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select CONVERT(VARCHAR(10),d.datetime_complaint,103) AS 'Submit Date'  "
            sql &= "  ,case when award_date > 0 then CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int) , Cast('1900-01-01' As DATETIME)), 103) else '-' end  AS 'award_date' "
            '   sql &= " ,  CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int), Cast('1900-01-01' As DATETIME)), 103) AS 'award_date'"
            sql &= ", d.star_no AS 'Star No.' , e.status_name AS 'Status' , d.complain_status_remark AS 'ผู้เสนอชื่อ'"
            sql &= " , case when complain_status = 1 then 'ผู้ป่วย' when complain_status = 2 then 'ญาติผู้ป่วยหรือเพื่อน'  when complain_status = 3 then 'ผู้มาเยี่ยมไข้' "
            sql &= " when complain_status = 4 then 'พนักงาน'  when complain_status = 5 then 'แพทย์' else '-' end as 'ประเภทผู้เสนอ' "
            sql &= " , c.user_fullname as 'ชื่อพนักงานที่ถูกเสนอ' , c.emp_code as 'Empployee Code' , f.dept_name as 'Department' , ISNULL(b.srp_point,0) AS 'Staff Recognition Point' "
            sql &= " from star_trans_list a "
            sql &= " left outer join star_hr_tab b on a.star_id = b.star_id"
            sql &= " inner join star_relate_person c on a.star_id = c.star_id"
            sql &= " inner join star_detail_tab d on a.star_id = d.star_id"
            sql &= " inner join star_status_list e on a.status_id = e.status_id"
            sql &= " inner join user_profile f on c.emp_code = f.emp_code "
            sql &= " where(isnull(a.is_delete, 0) = 0 And isnull(a.is_cancel, 0) = 0) AND ISNULL(a.status_id,0) > 1 "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then

                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND d.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(b.award_date,0) > 0 "
                    sql &= " AND b.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY a.star_id DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select  CONVERT(VARCHAR(10),d.datetime_complaint,103) AS 'Submit Date'  "
            sql &= "  ,case when award_date > 0 then CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int) , Cast('1900-01-01' As DATETIME)), 103) else '-' end  AS 'award_date' "
            ' sql &= " ,  CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int), Cast('1900-01-01' As DATETIME)), 103) AS 'award_date'"
            sql &= ", d.star_no AS 'Star No.' , e.status_name AS 'Status' , d.complain_status_remark AS 'ผู้เสนอชื่อ'"
            sql &= " , case when complain_status = 1 then 'ผู้ป่วย' when complain_status = 2 then 'ญาติผู้ป่วยหรือเพื่อน'  when complain_status = 3 then 'ผู้มาเยี่ยมไข้' "
            sql &= " when complain_status = 4 then 'พนักงาน'  when complain_status = 5 then 'แพทย์' else '-' end as 'ประเภทผู้เสนอ' "
            sql &= " , c.costcenter_name as 'ชื่อทีมที่ถูกเสนอ' , c.costcenter_id as 'Cost Center' , ISNULL(b.srp_point,0) AS 'Staff Recognition Point' "
         

            sql &= " from star_trans_list a "
            sql &= " left outer join star_hr_tab b on a.star_id = b.star_id"
            sql &= " inner join star_relate_dept c on a.star_id = c.star_id"
            sql &= " inner join star_detail_tab d on a.star_id = d.star_id"
            sql &= " inner join star_status_list e on a.status_id = e.status_id"
            sql &= " where(isnull(a.is_delete, 0) = 0 And isnull(a.is_cancel, 0) = 0) AND ISNULL(a.status_id,0) > 1 "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND d.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(b.award_date,0) > 0 "
                    sql &= " AND b.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY a.star_id DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindWaitingTime()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select b.star_no as 'Star No.' "
            sql &= " , case when CONVERT(varchar, datetime_report, 103) = '01/01/1900' then '-' else CONVERT(varchar, datetime_report, 103) end   as 'วันที่เกิดเหตุการณ์' "
            sql &= " , case when CONVERT(varchar, datetime_complaint, 103) = '01/01/1900' then '-' else CONVERT(varchar, datetime_complaint, 103) end   as 'วันที่เขียนเสนอชื่อ' "

            sql &= " , CASE WHEN DATEDIFF ( dd ,datetime_report , datetime_complaint  ) < 0 THEN 0 ELSE DATEDIFF ( dd ,datetime_report , datetime_complaint  ) END AS 'ระยะเวลาเกิดเหตุการณ์ถึงเขียนเสนอชื่อ' "
            sql &= " , case when CONVERT(varchar, submit_date, 103) = '01/01/1900' then '-' else CONVERT(varchar, submit_date, 103) end as 'submit date'"
            sql &= " , CASE WHEN DATEDIFF ( dd ,datetime_complaint , submit_date  ) < 0  or CONVERT(varchar, datetime_complaint, 103) = '01/01/1900' THEN 0 ELSE DATEDIFF ( dd ,datetime_complaint , submit_date  ) END AS 'ระยะเวลาเขียนเสนอชื่อถึง submit' "
            sql &= " , case when CONVERT(varchar, award_date, 103) = '01/01/1900' then '-' else CONVERT(varchar, award_date, 103) end as 'award date'"
            sql &= " , CASE WHEN DATEDIFF ( dd ,submit_date , award_date  ) < 0  or CONVERT(varchar, submit_date, 103) = '01/01/1900' THEN 0 ELSE DATEDIFF ( dd ,submit_date , award_date  ) END AS 'ระยะเวลา submit ถึง award' "

            sql &= " , CASE WHEN DATEDIFF ( dd ,datetime_complaint , award_date  ) < 0  or CONVERT(varchar, datetime_complaint, 103) = '01/01/1900' THEN 0 ELSE DATEDIFF ( dd ,datetime_complaint , award_date  ) END AS 'ระยะเวลา เขียนเสนอชื่อ ถึง award' "

            sql &= " , case when CONVERT(varchar, close_date, 103) = '01/01/1900' then '-' else CONVERT(varchar, close_date, 103) end as 'close date'"

            sql &= " , CASE WHEN DATEDIFF ( dd ,submit_date , close_date  ) < 0  or CONVERT(varchar, datetime_complaint, 103) = '01/01/1900' THEN 0 ELSE DATEDIFF ( dd ,submit_date , close_date  ) END AS 'ระยะเวลา submit ถึง close' "

            sql &= " , a.report_by as 'ผู้ทำรายการ'"
            sql &= " from star_trans_list a inner join star_detail_tab b"
            sql &= " on a.star_id = b.star_id"
            sql &= " left outer join (SELECT star_id , MIN(log_time) AS award_date FROM star_status_log WHERE status_id = 7 GROUP BY star_id ) c ON a.star_id = c.star_id "
            sql &= " left outer join (SELECT star_id , MIN(log_time) AS close_date FROM star_status_log WHERE status_id = 5 GROUP BY star_id ) d ON a.star_id = d.star_id "
            sql &= " where a.status_id > 1 and isnull(a.is_delete,0) = 0 and isnull(a.is_cancel,0) = 0 AND ISNULL(a.status_id,0) > 1 "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY a.star_id DESC"
            ds = conn.getDataSet(sql, "table1")

            ' Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportPoint()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select "
            sql &= " c.user_fullname as 'ชื่อพนักงานที่ถูกเสนอ' , c.emp_code as 'Empployee Code' , f.dept_name as 'Department' , ISNULL(SUM(b.srp_point),0) AS 'Staff Recognition Point' "
            sql &= " from star_trans_list a "
            sql &= " left outer join star_hr_tab b on a.star_id = b.star_id"
            sql &= " inner join star_relate_person c on a.star_id = c.star_id"
            sql &= " inner join star_detail_tab d on a.star_id = d.star_id"
            sql &= " inner join star_status_list e on a.status_id = e.status_id"
            sql &= " inner join user_profile f on c.emp_code = f.emp_code "
            sql &= " where(isnull(a.is_delete, 0) = 0 And isnull(a.is_cancel, 0) = 0) AND ISNULL(a.status_id,0) = 7  "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND d.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If


            sql &= " GROUP BY c.emp_code , c.user_fullname , f.dept_name "
            sql &= " ORDER BY ISNULL(SUM(b.srp_point),0) DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportBIWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select"
            sql &= " star_no AS 'Star No.' , CONVERT(varchar, datetime_complaint, 103) as 'Submit date' "
            sql &= "  ,case when award_date > 0 then CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int) , Cast('1900-01-01' As DATETIME)), 103) else '-' end  AS 'award_date' "
            'sql &= " ,  CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int), Cast('1900-01-01' As DATETIME)), 103) AS 'award_date'"
            sql &= " , case when isnull(a1.person,'') <> '' then a1.person when isnull(a2.person,'') <> '' then a2.person "
            sql &= " when isnull(a3.person,'') <> '' then a3.person "
            sql &= " end AS 'Nominee'"
            sql &= " , case when isnull(a1.person,'') <> '' then 'Individual' when isnull(a2.person,'') <> '' then 'Doctor' "
            sql &= " when isnull(a3.person,'') <> '' then 'Team'"
            sql &= " end AS 'Nominee Type'"
            sql &= " , case when team_id > 0 then team_name else '-' end as 'Recognition Teams'"
           
            sql &= " , case when chk_newbi1 = 1 then 'Yes' Else 'No' end AS 'BI1'"
            sql &= " , case when chk_newbi2 = 1 then 'Yes' Else 'No' end AS 'BI2'"
            sql &= " , case when chk_newbi3 = 1 then 'Yes' Else 'No' end AS 'BI3'"
            sql &= " , case when chk_newbi4 = 1 then 'Yes' Else 'No' end AS 'BI4'"
            sql &= " , case when chk_newbi5 = 1 then 'Yes' Else 'No' end AS 'BI5'"
            sql &= " , case when chk_newbi6 = 1 then 'Yes' Else 'No' end AS 'BI6'"
            sql &= " , case when chk_newbi7 = 1 then 'Yes' Else 'No' end AS 'BI7'"
            sql &= " , case when chk_newbi8 = 1 then 'Yes' Else 'No' end AS 'BI8'"
            sql &= " , case when chk_newbi9 = 1 then 'Yes' Else 'No' end AS 'BI9'"
            sql &= " , case when chk_newbi10 = 1 then 'Yes' Else 'No' end AS 'BI10'"

            'sql &= "  ,case when chk_2015_1 = 1 then 'Yes' Else 'No' end AS 'CO'"
            'sql &= "  ,case when chk_2015_2 = 1 then 'Yes' Else 'No' end AS 'A'"
            'sql &= "  ,case when chk_2015_3 = 1 then 'Yes' Else 'No' end AS 'S'"
            'sql &= "  ,case when chk_2015_4 = 1 then 'Yes' Else 'No' end AS 'T'"

            sql &= " , case when c.recognition_id > 0 then c.recognition_name else '-' end AS 'Regonition'"
            sql &= " , c.srp_point AS 'Star Point'"
            sql &= " , d.status_name AS 'Status'"
            sql &= " from star_trans_list a "
            sql &= " inner join star_detail_tab b on a.star_id = b.star_id"
            sql &= " inner join star_hr_tab c on a.star_id = c.star_id"
            sql &= " inner join star_status_list d on a.status_id = d.status_id"
            sql &= " left outer join (select max(user_fullname) as person , star_id from star_relate_person "
            sql &= " group by star_id) a1 on a.star_id = a1.star_id"
            sql &= " left outer join (select max(doctor_name) as person , star_id from star_relate_doctor "
            sql &= " group by star_id) a2 on a.star_id = a2.star_id"
            sql &= " left outer join (select max(costcenter_name) as person , star_id from star_relate_dept "
            sql &= " group by star_id) a3 on a.star_id = a3.star_id"
            sql &= " where a.star_id > 1 and isnull(a.is_cancel,0) = 0 and isnull(a.is_delete,0) = 0 "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(c.award_date,0) > 0 "
                    sql &= " AND c.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY a.star_id DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportCoAST()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select"
            sql &= " star_no AS 'Star No.' , CONVERT(varchar, datetime_complaint, 103) as 'Submit date' "
            sql &= " ,case when award_date > 0 then CONVERT(varchar,DATEADD(d, Cast(((award_date - 599266080000000000) * POWER(10.00000000000,-7) / 60 / 60 / 24) As int) , Cast('1900-01-01' As DATETIME)), 103) else '-' end  AS 'award_date' "
            sql &= " , case when isnull(a1.person,'') <> '' then a1.person when isnull(a2.person,'') <> '' then a2.person "
            sql &= " when isnull(a3.person,'') <> '' then a3.person "
            sql &= " end AS 'Nominee'"
            sql &= " , case when isnull(a1.person,'') <> '' then 'Individual' when isnull(a2.person,'') <> '' then 'Doctor' "
            sql &= " when isnull(a3.person,'') <> '' then 'Team'"
            sql &= " end AS 'Nominee Type'"
            sql &= " , case when team_id > 0 then team_name else '-' end as 'Recognition Teams'"

            'sql &= " , case when chk_newbi1 = 1 then 'Yes' Else 'No' end AS 'BI1'"
            'sql &= " , case when chk_newbi2 = 1 then 'Yes' Else 'No' end AS 'BI2'"
            'sql &= " , case when chk_newbi3 = 1 then 'Yes' Else 'No' end AS 'BI3'"
            'sql &= " , case when chk_newbi4 = 1 then 'Yes' Else 'No' end AS 'BI4'"
            'sql &= " , case when chk_newbi5 = 1 then 'Yes' Else 'No' end AS 'BI5'"
            'sql &= " , case when chk_newbi6 = 1 then 'Yes' Else 'No' end AS 'BI6'"
            'sql &= " , case when chk_newbi7 = 1 then 'Yes' Else 'No' end AS 'BI7'"
            'sql &= " , case when chk_newbi8 = 1 then 'Yes' Else 'No' end AS 'BI8'"
            'sql &= " , case when chk_newbi9 = 1 then 'Yes' Else 'No' end AS 'BI9'"
            'sql &= " , case when chk_newbi10 = 1 then 'Yes' Else 'No' end AS 'BI10'"

            sql &= "  ,case when chk_2015_1 = 1 then 'Yes' Else 'No' end AS 'CO'"
            sql &= "  ,case when chk_2015_2 = 1 then 'Yes' Else 'No' end AS 'A'"
            sql &= "  ,case when chk_2015_3 = 1 then 'Yes' Else 'No' end AS 'S'"
            sql &= "  ,case when chk_2015_4 = 1 then 'Yes' Else 'No' end AS 'T'"

            sql &= " , case when c.recognition_id > 0 then c.recognition_name else '-' end AS 'Regonition'"
            sql &= " , c.srp_point AS 'Star Point'"
            sql &= " , d.status_name AS 'Status'"
            sql &= " from star_trans_list a "
            sql &= " inner join star_detail_tab b on a.star_id = b.star_id"
            sql &= " inner join star_hr_tab c on a.star_id = c.star_id"
            sql &= " inner join star_status_list d on a.status_id = d.status_id"
            sql &= " left outer join (select max(user_fullname) as person , star_id from star_relate_person "
            sql &= " group by star_id) a1 on a.star_id = a1.star_id"
            sql &= " left outer join (select max(doctor_name) as person , star_id from star_relate_doctor "
            sql &= " group by star_id) a2 on a.star_id = a2.star_id"
            sql &= " left outer join (select max(costcenter_name) as person , star_id from star_relate_dept "
            sql &= " group by star_id) a3 on a.star_id = a3.star_id"
            sql &= " where a.star_id > 1 and isnull(a.is_cancel,0) = 0 and isnull(a.is_delete,0) = 0 "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(c.award_date,0) > 0 "
                    sql &= " AND c.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY a.star_id DESC"
            ds = conn.getDataSet(sql, "table1")

            ' Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportStarType()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select SUM(n1.num_person) AS Employee ,  SUM(n2.num_team) AS Team	 ,  SUM(n3.num_doctor) AS Doctor  ,  SUM(n4.num_other) AS Other"
            sql &= " FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id"
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " INNER JOIN  star_hr_tab d ON a.star_id = d.star_id"
            sql &= " LEFT OUTER JOIN (SELECT star_id , count(*) as num_person FROM star_relate_person GROUP BY star_id ) n1 ON a.star_id =  n1.star_id"
            sql &= " LEFT OUTER JOIN (SELECT star_id , count(*) as num_team FROM star_relate_dept GROUP BY star_id ) n2 ON a.star_id =  n2.star_id"
            sql &= " LEFT OUTER JOIN (SELECT star_id , count(*) as num_doctor FROM star_relate_doctor GROUP BY star_id ) n3 ON a.star_id =  n3.star_id"
            sql &= " LEFT OUTER JOIN (SELECT star_id , count(*) as num_other FROM star_detail_tab  where isnull(custom_nominee,'') <> '' GROUP BY star_id ) n4 ON a.star_id =  n4.star_id"
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND ISNULL(a.status_id,0) > 1 "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(c.award_date,0) > 0 "
                    sql &= " AND c.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If


            ds = conn.getDataSet(sql, "table1")

            ' Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportSenderType()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select case when complain_status = 0 then 'ไม่ระบุ' when complain_status = 1 then 'ผู้ป่วย' when complain_status = 2 then 'ญาติผู้ป่วยหรือเพื่อน'"
            sql &= " when complain_status =3 then 'ผู้มาเยี่ยมไข้' when complain_status = 4 then 'พนักงาน' when complain_status = 5 then 'แพทย์'"
            sql &= " when complain_status = 7 then 'อื่นๆ'"
            sql &= " end AS 'ประเภทผู้เสนอ'  ,  COUNT(complain_status) AS 'จำนวน'"
            sql &= " FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id"
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " INNER JOIN  star_hr_tab d ON a.star_id = d.star_id"

            sql &= " WHERE ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And ISNULL(a.status_id, 0) > 1"



            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(c.award_date,0) > 0 "
                    sql &= " AND c.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " GROUP BY  complain_status"
            ds = conn.getDataSet(sql, "table1")

            ' Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportCoastPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select n1.Cost_Center , n1.Department"
            sql &= " ,SUM(ISNULL(chk_2015_1,0)) AS 'CO'"
            sql &= " , SUM(ISNULL(chk_2015_2,0)) AS 'A'"
            sql &= " , SUM(ISNULL(chk_2015_3,0)) AS 'S'"
            sql &= " , SUM(ISNULL(chk_2015_4,0)) AS 'T'"
            sql &= " FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id"
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " INNER JOIN  star_hr_tab d ON a.star_id = d.star_id"
            sql &= " INNER JOIN (SELECT aa.star_id , bb.Cost_Center , bb.Department FROM star_relate_person aa "
            sql &= " inner join temp_BHUser bb on aa.emp_code = bb.Employee_id "
            sql &= " ) n1 ON a.star_id =  n1.star_id"

            sql &= " WHERE ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And ISNULL(a.status_id, 0) > 1"
        

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(c.award_date,0) > 0 "
                    sql &= " AND c.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " group by n1.Cost_Center , n1.Department"
            sql &= " having   (SUM(ISNULL(chk_2015_1,0)) +  SUM(ISNULL(chk_2015_2,0)) +  SUM(ISNULL(chk_2015_3,0)) +  SUM(ISNULL(chk_2015_4,0))) > 0"
            ds = conn.getDataSet(sql, "table1")

            ' Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportCoastTeam()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select n1.Cost_Center , n1.Department"
            sql &= " ,SUM(ISNULL(chk_2015_1,0)) AS 'CO'"
            sql &= " , SUM(ISNULL(chk_2015_2,0)) AS 'A'"
            sql &= " , SUM(ISNULL(chk_2015_3,0)) AS 'S'"
            sql &= " , SUM(ISNULL(chk_2015_4,0)) AS 'T'"
            sql &= " FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id"
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " INNER JOIN  star_hr_tab d ON a.star_id = d.star_id"
            sql &= " INNER JOIN (SELECT aa.star_id , aa.costcenter_id AS Cost_Center , aa.costcenter_name AS Department FROM star_relate_dept aa  "

            sql &= " ) n1 ON a.star_id =  n1.star_id"

            sql &= " WHERE ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And ISNULL(a.status_id, 0) > 1"


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(c.award_date,0) > 0 "
                    sql &= " AND c.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " group by n1.Cost_Center , n1.Department"
            sql &= " having   (SUM(ISNULL(chk_2015_1,0)) +  SUM(ISNULL(chk_2015_2,0)) +  SUM(ISNULL(chk_2015_3,0)) +  SUM(ISNULL(chk_2015_4,0))) > 0"
            ds = conn.getDataSet(sql, "table1")

            ' Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReport13()
        Dim sql As String
        Dim ds As New DataSet

        Try
            '  sql = "SELECT quarter FROM ("
            sql = "SELECT 1 AS quarter ,  sum(d.srp_point) as point FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id "
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " INNER JOIN  star_hr_tab d ON a.star_id = d.star_id "
            sql &= " WHERE ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And ISNULL(a.status_id, 0) > 1"
            sql &= " AND d.srp_point IN (100,200)"
            sql &= " AND month(b.datetime_complaint) IN ( 1 , 2 , 3)"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then

                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(d.award_date,0) > 0 "
                    sql &= " AND d.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            sql &= " UNION "

            sql &= "SELECT 2 AS quarter ,  sum(d.srp_point) as point FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id "
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " INNER JOIN  star_hr_tab d ON a.star_id = d.star_id "
            sql &= " WHERE ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And ISNULL(a.status_id, 0) > 1"
            sql &= " AND d.srp_point IN (100,200)"
            sql &= " AND month(b.datetime_complaint) IN ( 4 , 5 , 6)"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then

                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(d.award_date,0) > 0 "
                    sql &= " AND d.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            sql &= " UNION "

            sql &= "SELECT 3 AS quarter ,  sum(d.srp_point) as point FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id "
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " INNER JOIN  star_hr_tab d ON a.star_id = d.star_id "
            sql &= " WHERE ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And ISNULL(a.status_id, 0) > 1"
            sql &= " AND d.srp_point IN (100,200)"
            sql &= " AND month(b.datetime_complaint) IN ( 7 , 8 , 9)"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then

                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(d.award_date,0) > 0 "
                    sql &= " AND d.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            sql &= " UNION "

            sql &= "SELECT 4 AS quarter ,  sum(d.srp_point) as point FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id "
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " INNER JOIN  star_hr_tab d ON a.star_id = d.star_id "
            sql &= " WHERE ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And ISNULL(a.status_id, 0) > 1"
            sql &= " AND d.srp_point IN (100,200)"
            sql &= " AND month(b.datetime_complaint) IN ( 10 , 11 , 12)"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then

                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND b.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(d.award_date,0) > 0 "
                    sql &= " AND d.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            ' sql &= ") GROUP BY quarter "
            '   sql &= " ORDER BY quarter"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            ' Return
            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReport14()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select   "
         
            sql &= " c.costcenter_name as 'ชื่อทีมที่ถูกเสนอ' , c.costcenter_id as 'Cost Center' , COUNT(c.costcenter_id) AS Amount "


            sql &= " from star_trans_list a "
            sql &= " left outer join star_hr_tab b on a.star_id = b.star_id"
            sql &= " inner join star_relate_dept c on a.star_id = c.star_id"
            sql &= " inner join star_detail_tab d on a.star_id = d.star_id"
            sql &= " inner join star_status_list e on a.status_id = e.status_id"
            sql &= " where(isnull(a.is_delete, 0) = 0 And isnull(a.is_cancel, 0) = 0) AND ISNULL(a.status_id,0) > 1 "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND d.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(b.award_date,0) > 0 "
                    sql &= " AND b.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " GROUP BY  c.costcenter_name , c.costcenter_id "
            sql &= " ORDER BY  COUNT(c.costcenter_id)  DESC"
            ds = conn.getDataSet(sql, "table1")

            ' Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReport15()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select   "
          
            sql &= "  c.user_fullname as 'ชื่อพนักงานที่ถูกเสนอ' , c.emp_code as 'Empployee Code' , f.dept_name as 'Department' ,COUNT(*) AS 'จำนวนใบคำชม' , n1.dept_num AS 'จำนวนพนักงานทั้งหมด' , REPLACE(CONVERT(VARCHAR,CONVERT(MONEY,round(COUNT(*)/cast(n1.dept_num  as decimal(8,2)) , 2) * 100 ),1), '.00','')  AS 'จำนวนใบคำชม/จำนวนพนักงานทั้งหมด*100'  "
            sql &= " from star_trans_list a "
            sql &= " left outer join star_hr_tab b on a.star_id = b.star_id"
            sql &= " inner join star_relate_person c on a.star_id = c.star_id"
            sql &= " inner join star_detail_tab d on a.star_id = d.star_id"
            sql &= " inner join star_status_list e on a.status_id = e.status_id"
            sql &= " inner join user_profile f on c.emp_code = f.emp_code "
            sql &= " INNER JOIN (SELECT Cost_Center , COUNT(*) AS dept_num FROM temp_BHUser WHERE Company = 'BH' GROUP BY Cost_Center) n1 ON n1.Cost_Center = f.dept_id "
            sql &= " where(isnull(a.is_delete, 0) = 0 And isnull(a.is_cancel, 0) = 0) AND ISNULL(a.status_id,0) > 1 "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then

                If txtdate_type.SelectedValue = "1" Then
                    sql &= " AND d.datetime_complaint BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND ISNULL(b.award_date,0) > 0 "
                    sql &= " AND b.award_date BETWEEN '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' AND '" & ConvertDateStringToTimeStamp(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " GROUP BY  c.user_fullname ,  c.emp_code , f.dept_name , n1.dept_num "
            sql &= " ORDER BY round(COUNT(*)/cast(n1.dept_num  as decimal(8,2)) , 2) * 100   DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            ' Return

            GridView2.DataSource = ds
            GridView2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Dim HyperLink1 As HyperLink = CType(e.Row.FindControl("HyperLink1"), HyperLink)
            Dim lblPk As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim lblName As Label = CType(e.Row.FindControl("lblName"), Label)
            Dim lblNomineeEmpCode As Label = CType(e.Row.FindControl("lblNomineeEmpCode"), Label)
            Dim lblNomineeDept As Label = CType(e.Row.FindControl("lblNomineeDept"), Label)
            Dim lblNomineeDeptID As Label = CType(e.Row.FindControl("lblNomineeDeptID"), Label)
            Dim sql As String
            Dim ds As New DataSet
            Dim img_active As String = ""

            Try

                sql = "SELECT a.* , ISNULL(b.Employee_id,0) AS is_working , ISNULL(b.Department,'-') AS Department , b.Cost_Center FROM star_relate_person a LEFT OUTER JOIN temp_BHUser b ON a.emp_code = b.Employee_id "
                sql &= " WHERE star_id = " & lblPk.Text
                ' Response.Write(sql)
                ds = conn.getDataSet(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                    If ds.Tables("t1").Rows(i)("is_working").ToString <> "0" Then
                        '  img_active = "<img src='../images/staff_active.png' title='Active Employee' /> "
                    Else
                        ' img_active = "<img src='../images/staff_terminate.png' title='Inactive Employee' /> "
                    End If

                    lblName.Text &= " <span style='color:black'>" & img_active & ds.Tables("t1").Rows(i)("user_fullname").ToString & "</span>" & vbCrLf
                    lblNomineeEmpCode.Text &= ds.Tables("t1").Rows(i)("emp_code").ToString & "" & vbCrLf
                    '  lblNomineeDept.Text &= ds.Tables("t1").Rows(i)("Cost_Center").ToString & "<br/>"
                    ' Response.Write(ds.Tables("1").Rows(i)("user_fullname").ToString & "<br/>")
                    lblNomineeDeptID.Text &= ds.Tables("t1").Rows(i)("Cost_Center").ToString & "" & vbCrLf
                Next i

                sql = "SELECT a.*  FROM star_relate_doctor a  "

                sql &= " WHERE star_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    '  img_active = "<img src='../images/special-offer.png' title='Doctor' /> "
                    lblName.Text &= " <span style='color:black'>" & img_active & ds.Tables("t1").Rows(i)("doctor_name").ToString & "</span>" & vbCrLf
                    lblNomineeEmpCode.Text &= ds.Tables("t1").Rows(i)("emp_code").ToString & "" & vbCrLf
                    ' Response.Write(ds.Tables("1").Rows(i)("user_fullname").ToString & "<br/>")
                    lblNomineeDeptID.Text &= "9000" & "" & vbCrLf
                Next i
                If lblName.Text = "" Then
                    lblName.Text = "-"
                End If


                sql = "SELECT a.*  FROM star_relate_dept a  "

                sql &= " WHERE star_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    '    img_active = "<img src='../images/153.png' title='Cost Center' /> "
                    lblNomineeDept.Text &= " <span style='color:black'>" & img_active & ds.Tables("t1").Rows(i)("costcenter_name").ToString & "</span>" & vbCrLf
                    lblNomineeDeptID.Text &= ds.Tables("t1").Rows(i)("costcenter_id").ToString & "" & vbCrLf
                    ' Response.Write(ds.Tables("1").Rows(i)("user_fullname").ToString & "<br/>")
                Next i
                If ds.Tables("t1").Rows.Count = 0 Then
                    lblNomineeDept.Text = "-"
                End If

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        'bindReport1()
        If txtreport.SelectedValue = "1" Then
            bindReport1()
            GridView1.Visible = True
            GridView2.Visible = False
        ElseIf txtreport.SelectedValue = "2" Then
            bindReportDoctor()
            GridView1.Visible = False
            GridView2.Visible = True
            '  GridView2.Columns(1).Visible = True
        ElseIf txtreport.SelectedValue = "3" Then
            bindReportEmployee()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "4" Then
            bindReportDept()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "5" Then
            bindWaitingTime()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "6" Then
            bindReportPoint()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "7" Then
            bindReportBIWay()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "8" Then
            bindReportCoAST()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "9" Then
            bindReportStarType()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "10" Then
            bindReportSenderType()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "11" Then
            bindReportCoastTeam()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "12" Then
            bindReportCoastPerson()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "13" Then
            bindReport13()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "14" Then
            bindReport14()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "15" Then
            bindReport15()
            GridView1.Visible = False
            GridView2.Visible = True
        End If

    End Sub

    Public Shared Sub Export(ByVal fileName As String, ByVal gv As GridView)


        HttpContext.Current.Response.Clear()

        'Export will take two parameter first one the name of Excel File, and second one for gridview to be exported

        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))

        ' HttpContext.Current.Response.ContentType = "application/octet-stream"
        HttpContext.Current.Response.ContentType = " application/vnd.ms-excel"

        HttpContext.Current.Response.Charset = "TIS-620"

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

    Protected Sub cmdExport_Click(sender As Object, e As System.EventArgs) Handles cmdExport.Click
        If txtreport.SelectedValue = "1" Then
            Export("star.xls", GridView1)
        ElseIf txtreport.SelectedValue = "2" Then
            Export("doctor.xls", GridView2)
        ElseIf txtreport.SelectedValue = "3" Then
            Export("person.xls", GridView2)
        ElseIf txtreport.SelectedValue = "4" Then
            Export("team.xls", GridView2)
        ElseIf txtreport.SelectedValue = "5" Then
            Export("waiting_time.xls", GridView2)
        ElseIf txtreport.SelectedValue = "6" Then
            Export("awarded.xls", GridView2)
        ElseIf txtreport.SelectedValue = "7" Then
            Export("bi_way.xls", GridView2)
        ElseIf txtreport.SelectedValue = "8" Then

            Export("coast.xls", GridView2)
        ElseIf txtreport.SelectedValue = "9" Then
            Export("report9.xls", GridView2)
        ElseIf txtreport.SelectedValue = "10" Then
            Export("report10.xls", GridView2)
        ElseIf txtreport.SelectedValue = "11" Then
            Export("report11.xls", GridView2)
        ElseIf txtreport.SelectedValue = "12" Then
            Export("report12.xls", GridView2)
        ElseIf txtreport.SelectedValue = "13" Then
            Export("report13.xls", GridView2)
        ElseIf txtreport.SelectedValue = "14" Then
            Export("report14.xls", GridView2)
        ElseIf txtreport.SelectedValue = "15" Then
            Export("report15.xls", GridView2)

        End If

    End Sub

  
    Protected Sub txtreport_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtreport.SelectedIndexChanged
        GridView1.Visible = False
        GridView2.Visible = False
    End Sub
End Class
