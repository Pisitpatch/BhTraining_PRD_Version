Imports System.Data
Imports ShareFunction
Partial Class srp_preview_memo
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            '  Response.Redirect("../login.aspx")
            '  Response.End()
        End If

        id = Request.QueryString("id")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Page.IsPostBack Then
        Else ' First time load
            bindForm()
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

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select b.quater_no , b.year_no , a.mgr_dept_name AS 'Cost Center' , a.mgr_name "
            sql &= " , a.staff_amount , a.calculate_percent , a.x_month , a.x_jobtype , a.quota_total "
            sql &= " , b.project_name , b.issue_qty  , b.card_id_start , b.card_id_end , b.quater_issue_id "
            sql &= " , c.First_Name_EN , c.Last_Name_EN , c.Department , c.JobType "
            sql &= " from srp_m_calculate_quota a inner join srp_m_quarter_issue b on a.mgr_emp_code = b.mgr_emp_code "
            sql &= " and a.quater_no = b.quater_no and a.year_no = b.year_no "
            sql &= " inner join temp_BHUser c ON a.mgr_emp_code = c.Employee_id "
            sql &= " where b.quater_issue_id =  " & id
            ' sql &= " and a.mrg_emp_code IN (SELECT Employee_id FROM temp_BHUser) "
            '  sql &= " AND cast(b.quater_no as varchar) + '/' + cast(b.year_no as varchar) = '" & txtfind_quarter2.SelectedValue & "'"
            sql &= " ORDER BY  b.card_id_start , b.card_id_end  "
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count > 0 Then
                'lblName.Text = ds.Tables("t1").Rows(0)("mgr_name").ToString
                lblYearThai.Text = (CInt(ds.Tables("t1").Rows(0)("year_no").ToString) + 543)
                lblYearEN.Text = ds.Tables("t1").Rows(0)("year_no").ToString

                If (ds.Tables("t1").Rows(0)("First_Name_EN").ToString = "") And (ds.Tables("t1").Rows(0)("Last_Name_EN").ToString = "") Then
                    lblName.Text = ds.Tables("t1").Rows(0)("First_Name_TH").ToString & " " & ds.Tables("t1").Rows(0)("Last_Name_TH").ToString
                Else
                    lblName.Text = ds.Tables("t1").Rows(0)("First_Name_EN").ToString & " " & ds.Tables("t1").Rows(0)("Last_Name_EN").ToString
                End If

                lblQuarter_th.Text = ds.Tables("t1").Rows(0)("quater_no").ToString & " ประจำปี " & (CInt(ds.Tables("t1").Rows(0)("year_no").ToString) + 543)
                lblQuarter_en.Text = ds.Tables("t1").Rows(0)("quater_no").ToString & "/" & ds.Tables("t1").Rows(0)("year_no").ToString
                lblQuarter_th1.Text = ds.Tables("t1").Rows(0)("quater_no").ToString
                lblQuarter_th2.Text = ds.Tables("t1").Rows(0)("quater_no").ToString
                lblQuarter_en1.Text = ds.Tables("t1").Rows(0)("quater_no").ToString
                lblQuarter_en2.Text = ds.Tables("t1").Rows(0)("quater_no").ToString & "/" & ds.Tables("t1").Rows(0)("year_no").ToString
                lblQuarter_en3.Text = ds.Tables("t1").Rows(0)("quater_no").ToString
                lblDateTH.Text = Day(Date.Now) & "/" & Month(Date.Now) & "/" & (Date.Now.Year + 543)
                lblDateEN.Text = ConvertTSToDateString(Date.Now.Ticks)
                lblNum1.Text = ds.Tables("t1").Rows(0)("staff_amount").ToString
                lblNum2.Text = ds.Tables("t1").Rows(0)("staff_amount").ToString
                lblPercent1.Text = ds.Tables("t1").Rows(0)("x_month").ToString
                lblPercent2.Text = ds.Tables("t1").Rows(0)("x_month").ToString
                lblBonus1.Text = ds.Tables("t1").Rows(0)("x_jobtype").ToString
                lblBonus2.Text = ds.Tables("t1").Rows(0)("x_jobtype").ToString
                lblQuota1.Text = ds.Tables("t1").Rows(0)("issue_qty").ToString
                lblQuota2.Text = ds.Tables("t1").Rows(0)("issue_qty").ToString
                lblRef.Text() = Date.Now.Year & ":" & ds.Tables("t1").Rows(0)("quater_issue_id").ToString.PadLeft(5, "0")

                lblDeptName.Text = ds.Tables("t1").Rows(0)("JobType").ToString & ", " & ds.Tables("t1").Rows(0)("Department").ToString
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

End Class
