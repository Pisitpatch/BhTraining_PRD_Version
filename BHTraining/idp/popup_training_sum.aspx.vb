Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_popup_training_sum
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Session("viewtype").ToString

        id = Request.QueryString("id")
        name = Request.QueryString("name")
        method = Request.QueryString("method")
        category = Request.QueryString("category")
        jobtype = Request.QueryString("jobtype")
        planyear = Request.QueryString("planyear")
        employee = Request.QueryString("employee")
        dept = Request.QueryString("dept")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' First time load
            lblTopic.Text &= "<strong>Topic :</strong> " & name & "<br/>"
            lblTopic.Text &= "<strong>Year :</strong> " & planyear & "<br/>"
            lblTopic.Text &= "<strong>Job Type :</strong> " & jobtype & "<br/>"
            bindMethod()
            bindCategory()
            bindGrid()
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

    Sub bindMethod()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_method WHERE method_id = " & method
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            lblTopic.Text &= "<strong>Methodology :</strong> " & ds.Tables("t1").Rows(0)("method_name").ToString & "<br/>"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindCategory()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_category WHERE category_id = " & category
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            lblTopic.Text &= "<strong>Category :</strong> " & ds.Tables("t1").Rows(0)("category_name_en").ToString & "<br/>"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT b.idp_no , a.topic_name , b.report_by , b.report_emp_code , b.report_dept_name , b.date_submit FROM idp_function_tab a INNER JOIN idp_trans_list b ON a.idp_id = b.idp_id  "

            sql &= " left outer join idp_m_topic c ON a.topic_id = c.topic_id  AND ISNULL(c.is_delete,0) = 0  "

            sql &= " WHERE 1 =1 AND ISNULL(b.is_delete,0) = 0 AND ISNULL(b.is_cancel,0) = 0 AND b.status_id > 1 and ISNULL(is_ladder,0) = 0"
            If id = "9999" Or id = "" Or id = "0" Then
                sql &= " AND RTRIM(LTRIM(LOWER(a.topic_name))) = '" & name.Trim.ToLower & "' "
            Else
                sql &= " AND (a.topic_id = " & id & " OR RTRIM(LTRIM(LOWER(a.topic_name))) = '" & name.Trim.ToLower & "') "
            End If

            sql &= " AND a.method_id = " & method
            sql &= " AND b.report_jobtype = '" & jobtype.Trim & "' "
            sql &= " AND a.category_id = " & category
            sql &= " AND ISNULL(a.is_delete,0) = 0 "
            sql &= " AND b.plan_year =  " & planyear
            If employee <> "" Then
                sql &= " AND (b.report_by LIKE '%" & employee & "%' OR  b.report_emp_code LIKE '%" & employee & "%') "
            End If
            If dept <> "" Then
                sql &= " AND b.report_dept_id = " & dept
            End If

            If viewtype = "dept" Then
                sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            ' sql &= " GROUP BY b.idp_no , a.topic_name , b.report_by , b.report_dept_name "
            ' Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            Gridview1.DataSource = ds
            Gridview1.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
