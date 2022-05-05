Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Partial Class idp_popup_idp_topic_list
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""
    Protected name As String = ""
    Protected method As String
    Protected category As String
    Protected jobtype As String
    Protected planyear As String
    Protected employee As String
    Protected dept As String
    Protected viewtype As String = ""
    Protected status As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Request.QueryString("viewtype")

        id = Request.QueryString("id")
        name = Request.QueryString("name")
        method = Request.QueryString("method")
        category = Request.QueryString("category")
        jobtype = Request.QueryString("jobtype")
        planyear = Request.QueryString("planyear")
        employee = Request.QueryString("employee")
        dept = Request.QueryString("dept")
        status = Request.QueryString("status")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' First time load
            lblTopic.Text &= "<strong>Cost Center :</strong> " & dept & "<br/>"
            lblTopic.Text &= "<strong>Year :</strong> " & planyear & "<br/>"
            ' lblTopic.Text &= "<strong>Job Type :</strong> " & jobtype & "<br/>"
            If viewtype = "" Then
                bindGrid()
            ElseIf viewtype = "emp" Then
                bindGridEmployee()
            ElseIf viewtype = "notsubmit" Then
                bindGridEmployeeNotSubmit()
            End If

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
            sql = "SELECT c.Employee_id AS 'Employee Code' , report_by AS 'Employee Name' , c.Department AS 'Department' , methodology AS 'Methodology' , topic_name AS 'IDP Topic' , topic_status AS 'Status' , last_update "
            sql &= " FROM idp_trans_list a INNER JOIN idp_function_tab b ON a.idp_id = b.idp_id "
            sql &= " INNER JOIN temp_BHUser c ON a.report_emp_code = c.Employee_id AND c.Company = 'BH' "
            sql &= " WHERE ISNULL(b.is_delete,0) = 0 AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 1 AND ISNULL(is_ladder,0) = 0 "
            If status <> "" Then
                sql &= " AND b.topic_status_id = " & status
            Else
                sql &= " AND b.topic_status_id NOT IN (1,4) "
            End If
            sql &= " AND c.Cost_Center = " & dept
            sql &= " AND plan_year = " & planyear
            sql &= " ORDER BY topic_name "
            ds = conn.getDataSet(sql, "t1")

            ' Response.Write(sql)
            Gridview1.DataSource = ds
            Gridview1.DataBind()

            lblNum.Text = Gridview1.Rows.Count
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridEmployee()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = " select b.Cost_Center AS 'Cost Center' , MAX(a.report_by) AS 'Employee Name' , a.report_emp_code AS 'Employee Code' , MAX(report_jobtitle) AS 'Job Title' , CONVERT(VARCHAR(10),MAX(a.date_submit),103)  AS 'Submit Date' from idp_trans_list a "
            sql &= " INNER JOIN temp_BHUser b ON a.report_emp_code = b.Employee_id AND b.Company = 'BH' "
            sql &= " INNER JOIN idp_function_tab c ON a.idp_id = c.idp_id "
            sql &= " where ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And a.status_id > 1 And ISNULL(a.report_dept_id, 0) > 0 AND ISNULL(is_ladder,0) = 0"
            'sql &= " and a.plan_year = 2012"
            sql &= " AND  b.Cost_Center = " & dept
            sql &= " AND a.plan_year = " & planyear
            sql &= " GROUP BY  b.Cost_Center , a.report_emp_code "
            sql &= " ORDER BY a.report_emp_code "
            ' Response.Write(sql)
            'Return
            ' sql &= " group by a.report_dept_id , a.report_dept_name"
            ds = conn.getDataSet(sql, "t1")


            Gridview1.DataSource = ds
            Gridview1.DataBind()

            lblNum.Text = Gridview1.Rows.Count
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridEmployeeNotSubmit()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select Employee_id , Job_Title , First_Name_TH , Last_Name_TH , JobType , EmployeeType , Last_updated from temp_BHUser "
            sql &= " where Company = 'BH' AND Employee_id not in"
            sql &= " ("
            sql &= " select report_emp_code from idp_trans_list a inner join idp_function_tab b on a.idp_id = b.idp_id "
            sql &= " INNER JOIN temp_BHUser c ON a.report_emp_code = c.Employee_id AND c.Company = 'BH' "
            sql &= " where ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0 And ISNULL(a.status_id,0)  IN (2,3,4,5,6,7) And ISNULL(a.report_dept_id, 0) > 0 "
            sql &= " AND ISNULL(is_ladder,0) = 0"
            sql &= " and c.Cost_Center = " & dept
            sql &= " AND a.plan_year = " & planyear
            sql &= " )"
            sql &= " AND Cost_Center = " & dept
            'Response.Write(sql)
            'Return
            ' sql &= " group by a.report_dept_id , a.report_dept_name"
            ds = conn.getDataSet(sql, "t1")


            Gridview1.DataSource = ds
            Gridview1.DataBind()

            lblNum.Text = Gridview1.Rows.Count
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
