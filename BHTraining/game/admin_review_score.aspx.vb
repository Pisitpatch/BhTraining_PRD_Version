Imports System.Data
Imports ShareFunction
Imports System.IO

Partial Class game_admin_review_score
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected gid As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        gid = Request.QueryString("gid")


        If IsPostBack Then

        Else ' First time load
            bindDepartment()
            bindGroup()
            If gid <> "" Then
                txtgroup.SelectedValue = gid
            End If
            bindGrid()
            ' bindGridGroup()
            'bindPathWay()
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
            sql = "SELECT f.zone_no AS 'Zone No.' , f.group_name_th AS 'Group Name' ,  question_detail_th AS Question , ISNULL(num1,0) AS '1 Time(s)' , ISNULL(num2,0) AS '2 Time(s)'  , ISNULL(num3,0) AS '3 Time(s)'  , ISNULL(num4,0) AS '4 Time(s)'  , ISNULL(num_employee,0) AS 'Total Staff' FROM jci_master_question a"
            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(*) AS num1 , question_id  FROM jci_trans_list WHERE trans_order_sort = 1 AND game_group_id  <> 22"
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            sql &= " GROUP BY trans_order_sort , question_id  ) b"
            sql &= " ON a.question_id = b.question_id"
            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(*) AS num2 , question_id  FROM jci_trans_list WHERE trans_order_sort = 2 AND game_group_id  <> 22 "
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            sql &= " GROUP BY trans_order_sort , question_id  ) c"
            sql &= " ON a.question_id = c.question_id"
            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(*) AS num3 , question_id  FROM jci_trans_list WHERE trans_order_sort = 3 AND game_group_id  <> 22 "
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            sql &= " GROUP BY trans_order_sort , question_id  ) d"
            sql &= " ON a.question_id = d.question_id"
            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(*) AS num4 , question_id  FROM jci_trans_list WHERE trans_order_sort >= 4 AND game_group_id  <> 22 "
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            sql &= " GROUP BY trans_order_sort , question_id  ) e"
            sql &= " ON a.question_id = e.question_id"

            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(DISTINCT trans_create_by_emp_code) AS num_employee , question_id FROM jci_trans_list WHERE game_group_id  <> 22  "
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            sql &= " GROUP BY question_id   ) aa1 "
            sql &= " ON aa1.question_id = a.question_id"

            sql &= " INNER JOIN jci_master_group f ON a.group_id = f.group_id AND f.group_id <> 22 "
            sql &= " INNER JOIN jci_master_test g ON f.test_id = g.test_id AND g.is_game = 1"
            sql &= " WHERE 1 = 1 AND ISNULL(a.is_delete,0) = 0 "
            If txtgroup.SelectedIndex > 0 Then
                sql &= " AND f.group_id = " & txtgroup.SelectedValue
            End If
          
            sql &= " ORDER BY f.zone_no , a.group_id  , a.question_id"
            '  sql &= "  where a.group_id = 1"
            '  sql &= " ORDER BY "
            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()

            ' Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid2()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT d.zone_no , c.question_detail_th AS Question , a.answer_id AS 'Answer ID' , answer_detail_th AS 'Answer Detail' , CASE WHEN is_correct = 1 THEN 'Correct' Else 'Wrong' END AS 'Type of Answer' , ISNULL(ans_num,0) AS 'No. of Times Selected'  FROM jci_master_answer a "
            sql &= " LEFT OUTER JOIN "
            sql &= " (SELECT COUNT(*) AS ans_num , game_answer_id  FROM jci_trans_list WHERE ISNULL(game_answer_id,0) > 0  AND game_group_id  <> 22 "
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            sql &= " GROUP BY game_answer_id   ) b"
            sql &= " ON a.answer_id = b.game_answer_id"
            sql &= " INNER JOIN jci_master_question c ON a.question_id = c.question_id"
            sql &= " INNER JOIN jci_master_group d ON c.group_id = d.group_id "
            sql &= " WHERE(ISNULL(ans_num, 0) > 0)"
            If txtgroup.SelectedIndex > 0 Then
                sql &= " AND c.group_id = " & txtgroup.SelectedValue
            End If
            sql &= " ORDER BY d.zone_no , c.question_id , ISNULL(ans_num,0) DESC"
            ' Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid3()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT Day(a.trans_create_date) AS Day ,  Month(a.trans_create_date) As Month , Year(a.trans_create_date) As Year , "
            sql &= "   b.emp_code AS 'Employee Code' ,  b.user_fullname AS 'Employee Name' , b.costcentre_name_e AS 'Department', f.target_name AS 'Target Group' , t6.TotalZone AS 'Total Score' "
            sql &= " , ISNULL(t1.Zone1,'-') AS Zone1 , ISNULL(t2.Zone2,'-') AS Zone2, ISNULL(t3.Zone3,'-') AS Zone3 , ISNULL(t4.Zone4,'-') AS Zone4 "
            sql &= " , CASE WHEN ISNULL(t5.Evaluate,0) = 0 THEN 'No' Else 'Yes' END AS Evaluate "
            sql &= " FROM jci_trans_list a INNER JOIN user_profile b ON a.trans_create_by_emp_code = b.emp_code"
            sql &= " INNER JOIN jci_master_group c ON a.game_group_id = c.group_id "
            sql &= " INNER JOIN jci_master_test d ON c.test_id = d.test_id AND d.is_game = 1 AND ISNULL(d.is_delete,0) = 0 AND d.category_id IN (101,102)"
            sql &= " INNER JOIN jci_mapping_target e ON b.job_title = e.job_title "
            sql &= " INNER JOIN jci_master_target f ON e.target_id = f.target_id "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Zone1 , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id <> 22 "
            sql &= " WHERE is_correct_answer = 1 and  trans_order_sort = 1 AND zone_no = 1 GROUP BY trans_create_by_emp_code) t1 ON t1.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Zone2 , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id <> 22 "
            sql &= " WHERE is_correct_answer = 1 and  trans_order_sort = 1 AND zone_no = 2 GROUP BY trans_create_by_emp_code) t2 ON t2.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Zone3 , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id <> 22 "
            sql &= " WHERE is_correct_answer = 1 and  trans_order_sort = 1 AND zone_no = 3 GROUP BY trans_create_by_emp_code) t3 ON t3.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Zone4 , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id <> 22 "
            sql &= " WHERE is_correct_answer = 1 and  trans_order_sort = 1 AND zone_no = 4 GROUP BY trans_create_by_emp_code) t4 ON t4.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Evaluate , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id = 22 "
            sql &= " WHERE 1 = 1 GROUP BY trans_create_by_emp_code) t5 ON t5.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS TotalZone , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id <> 22 "
            sql &= " WHERE is_correct_answer = 1 and  trans_order_sort = 1 GROUP BY trans_create_by_emp_code) t6 ON t6.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " WHERE (is_correct_answer = 1 And trans_order_sort = 1 and game_group_id <> 22 )"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                '  sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If

            If txtgroup.SelectedIndex > 0 Then
                ' sql &= " AND c.group_id = " & txtgroup.SelectedValue
            End If
            If txtdept.SelectedIndex > 0 Then
                ' sql &= " AND a.trans_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " GROUP BY b.emp_code,  b.user_fullname , b.costcentre_name_e , f.target_name ,  Day(a.trans_create_date) ,  Month(a.trans_create_date) , Year(a.trans_create_date) "
            sql &= " ,t1.Zone1 , t2.Zone2 , t3.Zone3 , t4.Zone4 , t5.Evaluate , t6.TotalZone"
            sql &= " ORDER BY Day(a.trans_create_date) ,  Month(a.trans_create_date) , COUNT(*) DESC"
            ' Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid4()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.trans_dept_id AS 'Cost Center' , b.dept_name_en AS 'Department'  , COUNT( a.trans_create_by_emp_code) AS 'Employee Attended' , ISNULL(MAX(c.total),0) AS 'Total Full Time' , MAX(d.GrandTotal) AS 'Grand Total' "
            ' sql &= " , (COUNT( a.trans_create_by_emp_code) / MAX(c.total)) * 100 AS 'Percent %'"
            sql &= " FROM (SELECT trans_create_by_emp_code , trans_dept_id FROM jci_trans_list aa1 INNER JOIN jci_master_group aa2 ON aa1.game_group_id = aa2.group_id  "
            sql &= " INNER jOIN jci_master_test aa3 ON aa2.test_id = aa3.test_id AND aa3.is_game = 1 AND ISNULL(aa3.is_delete,0) = 0 AND aa3.category_id IN (101,102)"
            sql &= " WHERE 1 = 1 AND ISNULL(game_answer_id,0) > 0 "
            If txtgroup.SelectedIndex > 0 Then
                sql &= " AND game_group_id = " & txtgroup.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            sql &= " GROUP BY trans_create_by_emp_code , trans_dept_id) a"
            sql &= " INNER JOIN user_dept b ON a.trans_dept_id = b.dept_id"
            sql &= " LEFT OUTER JOIN (SELECT Cost_Center , COUNT(*) AS 'total' FROM temp_BHUser WHERE EmployeeType = 'Full Time' GROUP BY Cost_Center ) c ON b.dept_id = c.Cost_Center"
            sql &= " LEFT OUTER JOIN (SELECT Cost_Center , COUNT(*) AS 'GrandTotal' FROM temp_BHUser  GROUP BY Cost_Center ) d ON b.dept_id = d.Cost_Center"
            sql &= " WHERE 1=1 "
          
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.trans_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " GROUP BY a.trans_dept_id , b.dept_name_en "
            sql &= " ORDER BY  b.dept_name_en "

            ' Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid5()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT zone_no AS 'Zone' , group_name_th AS 'Group Name' , b.dept_name AS 'Department' , num_question AS 'Full Score' , AVG(a.Score) AS 'Average Score' "
            sql &= " FROM (SELECT trans_create_by_emp_code , trans_dept_id , game_group_id , COUNT(*) AS Score FROM jci_trans_list "
            sql &= " WHERE is_correct_answer = 1 And trans_order_sort = 1 "
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            sql &= " GROUP BY trans_create_by_emp_code , trans_dept_id , game_group_id ) a "
            sql &= "  INNER JOIN user_profile b ON a.trans_create_by_emp_code = b.emp_code"
            sql &= " INNER JOIN jci_master_group c ON a.game_group_id = c.group_id AND ISNULL(c.is_delete,0) = 0 AND c.group_id <> 22 "
            sql &= " INNER JOIN jci_master_test d ON c.test_id = d.test_id AND d.is_game = 1 AND ISNULL(d.is_delete,0) = 0 AND d.category_id IN (101,102)"
            sql &= " WHERE 1 = 1"
            If txtgroup.SelectedIndex > 0 Then
                sql &= " AND c.group_id = " & txtgroup.SelectedValue
            End If
          
            sql &= " GROUP BY zone_no , b.dept_name , group_name_th , num_question  "
            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid6()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT b.dept_name AS 'Department' ,  CASE when g.is_professional = 1 then 'Professional' else 'Non-Professional' end AS 'Target Group'   , AVG(a.Score) AS 'Average Score' FROM  "
            sql &= " (SELECT trans_create_by_emp_code , trans_dept_id ,  COUNT(*) AS Score FROM jci_trans_list aa1 "
            sql &= " INNER JOIN jci_master_group aa2 ON aa1.game_group_id = aa2.group_id AND ISNULL(aa2.is_delete,0) = 0"
            sql &= " AND aa2.group_id <> 22 INNER JOIN jci_master_test aa3 ON aa2.test_id = aa3.test_id AND aa3.is_game = 1"
            sql &= " AND ISNULL(aa3.is_delete,0) = 0 AND aa3.category_id IN (101,102) "
            sql &= " WHERE is_correct_answer = 1 And trans_order_sort = 1 "
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtgroup.SelectedIndex > 0 Then
                'sql &= " AND aa2.group_id = " & txtgroup.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND aa1.trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            sql &= " GROUP BY trans_create_by_emp_code , trans_dept_id  ) a INNER JOIN user_profile b ON a.trans_create_by_emp_code = b.emp_code "

            '  sql &= " INNER JOIN jci_master_group c ON a.game_group_id = c.group_id AND ISNULL(c.is_delete,0) = 0"
            ' sql &= " AND c.group_id <> 22 INNER JOIN jci_master_test d ON c.test_id = d.test_id AND d.is_game = 1"
            'sql &= " AND ISNULL(d.is_delete,0) = 0 AND d.category_id IN (101,102) "
            sql &= " INNER JOIN jci_mapping_target f ON b.job_title = f.job_title INNER JOIN jci_master_target g"
            sql &= " ON f.target_id = g.target_id WHERE 1 = 1 "

          
            sql &= " GROUP BY b.dept_name ,  g.is_professional "
            sql &= " ORDER BY  b.dept_name , g.is_professional  "
            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()

            ' Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid7()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select job_title AS 'Job Title' , COUNT(*) as 'Amount' from ("

            sql &= " select b.job_title ,  b.emp_code from jci_trans_list a inner join user_profile b on a.trans_create_by_emp_code = b.emp_code "
            sql &= " where 1 = 1 "
            If txtgroup.SelectedIndex > 0 Then
                sql &= " AND a.game_group_id = " & txtgroup.SelectedValue
            End If
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.trans_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by b.job_title , b.emp_code"
            sql &= " ) a1"
            sql &= " where 1 = 1"
         
            sql &= " group by job_title "
            ' Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()

            ' Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
    Sub bindGridEvaluate()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select question_detail_th AS 'Evaluation Topic', answer_detail_th AS 'Result' , COUNT(*) AS 'Quantity' from jci_trans_list a INNER JOIN jci_master_answer b ON a.game_answer_id = b.answer_id AND  game_group_id = 22"
            sql &= " INNER JOIN jci_master_question c ON a.question_id = c.question_id"
            sql &= " WHERE 1=1 "
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtgroup.SelectedIndex > 0 Then
                sql &= " AND a.game_group_id = " & txtgroup.SelectedValue
            End If
            sql &= " group by question_detail_th , answer_detail_th "
            sql &= " order by question_detail_th "

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()

            ' Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid9()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT "
            sql &= "  b.emp_code AS 'Employee Code' ,  b.user_fullname AS 'Employee Name' , b.costcentre_name_e AS 'Department', f.target_name AS 'Target Group' , COUNT(*) AS 'Total Score' "
            sql &= " , ISNULL(t1.Zone1,'-') AS Zone1 , ISNULL(t2.Zone2,'-') AS Zone2, ISNULL(t3.Zone3,'-') AS Zone3 , ISNULL(t4.Zone4,'-') AS Zone4 "
            sql &= " , CASE WHEN ISNULL(t5.Evaluate,0) = 0 THEN 'No' Else 'Yes' END AS Evaluate "
            sql &= " FROM jci_trans_list a INNER JOIN user_profile b ON a.trans_create_by_emp_code = b.emp_code"
            sql &= " INNER JOIN jci_master_group c ON a.game_group_id = c.group_id "
            sql &= " INNER JOIN jci_master_test d ON c.test_id = d.test_id AND d.is_game = 1 AND ISNULL(d.is_delete,0) = 0 AND d.category_id IN (101,102)"
            sql &= " INNER JOIN jci_mapping_target e ON b.job_title = e.job_title "
            sql &= " INNER JOIN jci_master_target f ON e.target_id = f.target_id "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Zone1 , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id <> 22 "
            sql &= " WHERE is_correct_answer = 1 and  trans_order_sort = 1 AND zone_no = 1 GROUP BY trans_create_by_emp_code) t1 ON t1.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Zone2 , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id <> 22 "
            sql &= " WHERE is_correct_answer = 1 and  trans_order_sort = 1 AND zone_no = 2 GROUP BY trans_create_by_emp_code) t2 ON t2.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Zone3 , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id <> 22 "
            sql &= " WHERE is_correct_answer = 1 and  trans_order_sort = 1 AND zone_no = 3 GROUP BY trans_create_by_emp_code) t3 ON t3.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Zone4 , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id <> 22 "
            sql &= " WHERE is_correct_answer = 1 and  trans_order_sort = 1 AND zone_no = 4 GROUP BY trans_create_by_emp_code) t4 ON t4.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS Evaluate , trans_create_by_emp_code FROM jci_trans_list aa "
            sql &= " INNER JOIN jci_master_group bb ON aa.game_group_id = bb.group_id AND bb.group_id = 22 "
            sql &= " WHERE 1 = 1 GROUP BY trans_create_by_emp_code) t5 ON t5.trans_create_by_emp_code = a.trans_create_by_emp_code "

            sql &= " WHERE (is_correct_answer = 1 And trans_order_sort = 1 and game_group_id <> 22 )"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                '  sql &= " AND trans_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If

            If txtgroup.SelectedIndex > 0 Then
                ' sql &= " AND c.group_id = " & txtgroup.SelectedValue
            End If
            If txtdept.SelectedIndex > 0 Then
                ' sql &= " AND a.trans_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " GROUP BY b.emp_code , b.user_fullname , b.costcentre_name_e , f.target_name  "
            sql &= " ,t1.Zone1 , t2.Zone2 , t3.Zone3 , t4.Zone4 , t5.Evaluate"
            sql &= " ORDER BY  COUNT(*) DESC"
            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGroup()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id AND b.is_game = 1 WHERE ISNULL(a.is_delete,0) = 0 AND a.group_id <> 22 "
            sql &= " ORDER BY a.zone_no "
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtgroup.DataSource = ds
            txtgroup.DataBind()

            txtgroup.Items.Insert(0, New ListItem("All Group", ""))
            ' Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDepartment()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM user_dept  "
            sql &= " ORDER BY dept_name_en "
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdept.DataSource = ds
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("All Department", ""))
            ' Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        If txtreport.SelectedValue = "1" Then
            bindGrid()
        ElseIf txtreport.SelectedValue = "2" Then
            bindGrid2()
        ElseIf txtreport.SelectedValue = "3" Then
            bindGrid3()
        ElseIf txtreport.SelectedValue = "4" Then
            bindGrid4()
        ElseIf txtreport.SelectedValue = "5" Then
            bindGrid5()
        ElseIf txtreport.SelectedValue = "6" Then
            bindGrid6()
        ElseIf txtreport.SelectedValue = "7" Then
            bindGrid7()
        ElseIf txtreport.SelectedValue = "8" Then
            bindGridEvaluate()
        ElseIf txtreport.SelectedValue = "9" Then
            bindGrid9()
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


    Protected Sub txtreport_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtreport.SelectedIndexChanged

    End Sub

    Protected Sub cmdExport_Click(sender As Object, e As System.EventArgs) Handles cmdExport.Click
        Export("jci_report.xls", GridView2)
    End Sub
End Class
