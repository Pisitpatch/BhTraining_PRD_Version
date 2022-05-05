Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Globalization
Partial Class idp_idp_report
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected priv_list() As String

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
        priv_list = Session("priv_list")


        If Page.IsPostBack Then

        Else ' First time load
            bindYear()
            ' bindCategory()
            ' bindMethod()
            bindDept()
            bindGrid2()
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

    Sub bindYear()
        For i As Integer = Date.Now.Year - 2 To Date.Now.Year + 2
            txtyear.Items.Add(i)
        Next i

        txtyear.SelectedValue = Date.Now.Year
    End Sub

    Sub bindDept()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM user_dept WHERE 1 = 1 "
            If viewtype = "dept" Then
                sql &= " AND dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            sql &= " ORDER BY dept_name_en"
            'sql = "SELECT report_dept_id AS dept_id , cast(report_dept_id as varchar) + ' : ' + report_dept_name AS dept_name_en FROM idp_trans_list "
            ' sql &= " GROUP BY report_dept_id , report_dept_name ORDER BY report_dept_name"

            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txtdept.DataSource = reader
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("--All Department--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

  

   

    Sub bindGrid2()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ("
            sql &= " select c.Cost_Center ,  c.Department , ISNULL(MAX(tComplete.num1),0) AS 'topic_complete' , ISNULL(MAX(tAll.numAll),0) AS 'topic_all'"
            sql &= " , CASE WHEN ISNULL(MAX(tAll.numAll),0) > 0 THEN ISNULL(MAX(tComplete.num1),0)*100 / (ISNULL(MAX(tAll.numAll),0) + ISNULL(MAX(tComplete.num1),0)) ELSE 0 END AS 'percent'"
            sql &= " , ISNULL(MAX(tEmp1.emp_num),0) AS emp1 , MAX(tEmp2.emp_all) AS emp_fulltime , MAX(tEmp3.emp_all1) AS emp_fte "
            sql &= " , ISNULL(MAX(tEmpNotSubmit.notsubmit),0) AS emp_notsubmit  "

            sql &= " , CASE WHEN ISNULL(MAX(tEmp2.emp_all),0) > 0 THEN ISNULL(MAX(tEmp1.emp_num),0)*100 / (ISNULL(MAX(tEmp3.emp_all1),0) ) ELSE 0 END AS 'percentSubmit'"

            sql &= " from idp_trans_list a inner join idp_function_tab b on a.idp_id = b.idp_id"
            sql &= " INNER JOIN temp_BHUser c ON a.report_emp_code = c.Employee_id AND c.Company = 'BH' "

            sql &= " left outer join (select COUNT(a1.idp_id) AS num1 , a3.Cost_Center from idp_function_tab a1 "
            sql &= " inner join idp_trans_list a2 on a1.idp_id = a2.idp_id and ISNULL(a1.is_delete,0) = 0 and ISNULL(a2.is_delete,0) = 0 and ISNULL(a2.is_cancel,0) = 0 AND ISNULL(is_ladder,0) = 0 "
            sql &= " inner join temp_BHUser a3 ON  a2.report_emp_code = a3.Employee_id AND a3.Company = 'BH' "
            sql &= " where(topic_status_id = 1 And a2.status_id > 1)"
            If txtyear.SelectedValue <> "" Then
                sql &= " AND a2.plan_year = " & txtyear.SelectedValue
            End If
            ' sql &= " and a2.plan_year = 2012"
            sql &= " group by  a3.Cost_Center ) tComplete on c.Cost_Center = tComplete.Cost_Center"

            sql &= " left outer join (select COUNT(a1.idp_id) AS numAll , a3.Cost_Center from idp_function_tab a1 "
            sql &= " inner join idp_trans_list a2 on a1.idp_id = a2.idp_id and ISNULL(a1.is_delete,0) = 0 and ISNULL(a2.is_delete,0) = 0 and ISNULL(a2.is_cancel,0) = 0 AND ISNULL(is_ladder,0) = 0  "
            sql &= " inner join temp_BHUser a3 ON  a2.report_emp_code = a3.Employee_id AND a3.Company = 'BH' "
            sql &= " where(topic_status_id NOT IN (1,4) And a2.status_id > 1)"
            If txtyear.SelectedValue <> "" Then
                sql &= " AND a2.plan_year = " & txtyear.SelectedValue
            End If
            sql &= " group by a3.Cost_Center ) tAll on c.Cost_Center = tAll.Cost_Center"

            sql &= " left outer join (select b.Cost_Center , COUNT(distinct report_emp_code) AS emp_num from idp_trans_list a "
            sql &= " INNER JOIN temp_BHUser b ON a.report_emp_code = b.Employee_id AND b.Company = 'BH' "
            sql &= " INNER JOIN idp_function_tab c ON a.idp_id = c.idp_id "
            sql &= " where ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And a.status_id > 1 And ISNULL(a.report_dept_id, 0) > 0 AND ISNULL(is_ladder,0) = 0"
            'sql &= " and a.plan_year = 2012"
            If txtyear.SelectedValue <> "" Then
                sql &= " AND a.plan_year = " & txtyear.SelectedValue
            End If
            sql &= " group by b.Cost_Center , b.Department"
            sql &= " ) tEmp1 on c.Cost_Center = tEmp1.Cost_Center"

            sql &= " left outer join (select Cost_Center , COUNT(*) AS emp_all from temp_BHUser a "
            sql &= " WHERE EmployeeType = 'Full Time' AND a.Company = 'BH'"
            sql &= " GROUP BY Cost_Center ) tEmp2 on c.Cost_Center = tEmp2.Cost_Center"

            sql &= " left outer join (select Cost_Center , COUNT(*) AS emp_all1 from temp_BHUser a "
            sql &= " WHERE 1 =1 AND a.Company = 'BH' "
            sql &= " GROUP BY Cost_Center ) tEmp3 on c.Cost_Center = tEmp3.Cost_Center"
            ' NOt submit IDP
            sql &= " left outer join ( select Cost_Center , COUNT( Employee_id) AS 'notsubmit' from temp_BHUser "
            sql &= " where Company = 'BH' AND Employee_id not in"
            sql &= " ("
            sql &= " select cc.Employee_id from idp_trans_list aa inner join idp_function_tab bb on aa.idp_id = bb.idp_id"
            sql &= " INNER JOIN temp_BHUser cc ON aa.report_emp_code = cc.Employee_id AND cc.Company = 'BH' "
            sql &= " where ISNULL(aa.is_delete, 0) = 0 And ISNULL(aa.is_cancel, 0) = 0 And ISNULL(aa.status_id,0)  IN (2,3,4,5,6,7) And ISNULL(aa.report_dept_id, 0) > 0 "
            If txtyear.SelectedValue <> "" Then
                sql &= " AND aa.plan_year = " & txtyear.SelectedValue
            End If
            sql &= " AND ISNULL(is_ladder,0) = 0"
            sql &= "  "
            sql &= " ) GROUP BY Cost_Center "
            sql &= " ) tEmpNotSubmit ON c.Cost_Center = tEmpNotSubmit.Cost_Center "

            sql &= " where(ISNULL(a.is_cancel, 0) = 0 And ISNULL(a.is_delete, 0) = 0 And a.status_id > 1 And ISNULL(a.report_dept_id, 0) > 0)"
          
            sql &= " group by c.Cost_Center ,  c.Department"

            'sql &= " union"

            'sql &= " select dept_id , dept_name_en , 0 ,0,0,0,0 ,0, 0 from user_dept "
            'sql &= " where dept_id not in (Select report_dept_id from idp_trans_list aa inner join idp_function_tab bb on aa.idp_id = bb.idp_id"
            'sql &= " where ISNULL(aa.is_delete,0) = 0  and ISNULL(aa.is_cancel,0) = 0 and aa.status_id > 1 and ISNULL(aa.report_dept_id,0) > 0)"

            sql &= " ) aaa WHERE 1 = 1 "
            If viewtype = "dept" Then
                If findArrayValue(priv_list, "24") = True Then ' Traing

                Else
                    sql &= " AND Cost_Center IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
                End If

            End If
            If txtdept.SelectedValue <> "" Then

                sql &= " AND Cost_Center = " & txtdept.SelectedValue
            End If
            sql &= " ORDER BY Department"
            ' Response.Write(txtdept.SelectedValue)
            'Response.Write(sql)

            '  sql &= " group by  e.category_name_en ,  CASE WHEN ISNULL(b.topic_name,'') = '' THEN d.topic_name_th ELSE b.topic_name END , b.category_id , a.plan_year "
            ' sql &= " order by 5 desc "
            ' Response.Write(sql)
            ' Return
            ds = conn.getDataSet(sql, "t1")

            GridView2.DataSource = ds
            GridView2.DataBind()
            ' Response.Write(ds.Tables("t1").Rows.Count)
            ' Response.Write(GridView2.Rows.Count)
            '  lblNum.Text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        bindGrid2()
    End Sub
End Class
