Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Globalization

Partial Class idp_training_report
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected data1 As String = ""
    Protected data2 As String = ""
    Protected data2_name As String = ""

    Protected data3 As String = ""

    Protected data4_1 As String = ""
    Protected data4_2 As String = ""
    Protected data4_name As String = ""

    Protected data_external_budget As String = ""
    Protected data_internal_budget As String = ""

    Protected limit_record As String = ""

    Protected totalExpense As Double = 0
    Protected totalRequest As Double = 0
    Protected totalApprove As Double = 0
    Protected totalBuget As Double = 0
    Protected totalHour As Double = 0

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
            bindComboSelectReport()
            bindJobTypeAll()
            bindJobTitleAll()
            bindCCAll()

            bindInternalCombo()
            '  bindReport1()
            ' bindReport2()
        End If



       

        If viewtype = "hr" Then
            limit_record = " TOP 5 "
        End If

        If viewtype = "dept" Then
            GridviewActionList.Visible = True
            bindGridDeptIndividualList()
            bindGridActionList()
            panel_search_dept.Visible = True
            '  GridviewInternalSummary.Visible = False
            '  internal_summary.Visible = False
        Else
            bindGridIndividualList()
            GridviewActionList.Visible = False
        End If

        If viewtype = "" Then
            txtdept.Enabled = False
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

    Sub bindComboSelectReport()
        txtselect_report.Items.Clear()
        txtselect_report.Items.Add(New ListItem("------ Please Select ------", ""))
        If viewtype <> "" Then
            txtselect_report.Items.Add(New ListItem("- External Training Summary", "1"))


            If viewtype = "hr" Then
                txtselect_report.Items.Add(New ListItem("- External Training Summary by Category ", "2"))
                txtselect_report.Items.Add(New ListItem("- External Training Summary by Expect Outcome ", "8"))
                txtselect_report.Items.Add(New ListItem("- External Budget Training Summary", "13"))
            End If

            txtselect_report.Items.Add(New ListItem("- Action After Training Summary Report", "3"))

            If viewtype = "hr" Then
                txtselect_report.Items.Add(New ListItem("- Action After Training Detail Report", "10"))
                txtselect_report.Items.Add(New ListItem("- Internal Training Summary by Request", "11"))
                txtselect_report.Items.Add(New ListItem("- Internal Training Summary by Category", "12"))
            End If

            txtselect_report.Items.Add(New ListItem("- Internal Training Summary by %target staff attended", "7"))
            If viewtype = "dept" Then
                txtselect_report.Items.Add(New ListItem("- External Training Record by Individual Staff", "5"))
                txtselect_report.Items.Add(New ListItem("- Internal Training Summary by Category", "12"))
            End If

         

            If viewtype = "dept" Then
                txtselect_report.Items.Add(New ListItem("- Internal Training Record by Individual Staff", "6"))
            End If

            If viewtype = "hr" Then
                txtselect_report.Items.Add(New ListItem("- Internal Training Summary by Expected Outcome", "9"))
                txtselect_report.Items.Add(New ListItem("- Export to Excel External Training Budget", "101"))
                txtselect_report.Items.Add(New ListItem("- Export to Excel External Training Expense", "102"))
                txtselect_report.Items.Add(New ListItem("- Export to Excel Internal Training Budget", "103"))
                txtselect_report.Items.Add(New ListItem("- Export to Excel Internal Training Expense", "104"))

            End If

        Else
            txtselect_report.Items.Add(New ListItem("- External Training Summary by Request type", "3"))
            txtselect_report.Items.Add(New ListItem("- Training Record by Individual Staff", "4"))
        End If



    End Sub

    Sub bindInternalCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_trans_list a INNER JOIN idp_external_req b ON a.idp_id = b.idp_id "
            sql &= " INNER JOIN user_profile c ON a.report_emp_code = c.emp_code "
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id in (5,6,7) AND b.request_type = 'int' "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                ' sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
                sql &= " AND a.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If

            If viewtype = "" Then
                sql &= " AND a.report_emp_code IN (SELECT emp_code FROM  idp_training_registered )"
            End If

            If viewtype = "dept" Then
                sql &= " AND c.dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
            End If

            If txtdept.SelectedIndex > 0 Then
                sql &= " AND c.dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY internal_title"
            'Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")
            txtinternal_topic.DataSource = ds
            txtinternal_topic.DataBind()

            txtinternal_topic.Items.Insert(0, New ListItem("-- Please Select --", ""))
            txtinternal_topic.SelectedIndex = 0

            txtinternal_topic_person.DataSource = ds
            txtinternal_topic_person.DataBind()

            txtinternal_topic_person.Items.Insert(0, New ListItem("-- Please Select --", ""))
            txtinternal_topic_person.SelectedIndex = 0
            If viewtype = "" Then
                txtinternal_topic_person.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindReport1(Optional flag As String = "")
        Dim sql As String
        Dim sql_pre As String = ""
        Dim sql_grid As String = ""
        Dim ds As New DataSet
        Dim ds2 As New DataSet

        totalExpense = 0
        totalRequest = 0
        totalApprove = 0
        totalBuget = 0
        totalHour = 0
        Try

            sql = "SELECT b.report_dept_id , b.report_dept_name , COUNT(distinct total_all.total_request) AS total_request"
            sql &= " ,  COUNT(distinct total1) as num , (Select Count(*) From idp_trans_list aa inner join idp_external_req bb on aa.idp_id = bb.idp_id WHERE status_id IN(5,6,7) and ISNULL(is_delete,0) = 0 and ISNULL(is_cancel,0) = 0 and bb.request_type = 'ext' and report_dept_id is not null "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND bb.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(bb.date_start) = " & Date.Now.Year
            End If

            If viewtype = "dept" Then
                sql &= " AND aa.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            sql &= " ) as total"

            sql &= " , ISNULL(MAX(t2.budget),0)  as budget , ISNULL(MAX(t3.expense),0) as expense , ISNULL(MAX(hour1),0) as train_hour  "

            sql &= " FROM idp_external_req a INNER JOIN idp_trans_list b on a.idp_id = b.idp_id "
            ' sql &= " LEFT OUTER JOIN (select * from idp_training_expense where accouting_type = 1 and ISNULL(is_delete,0) = 0) c ON a.idp_id = c.idp_id "
            ' sql &= " LEFT OUTER JOIN (select * from idp_training_expense where accouting_type = 2 and ISNULL(is_delete,0) = 0) d ON a.idp_id = d.idp_id "

            sql &= " INNER JOIN user_profile up ON b.report_emp_code = up.emp_code "

            sql &= " LEFT OUTER JOIN (select (a1.idp_id) as total1 "
            sql &= " FROM idp_external_req a1 INNER JOIN idp_trans_list b1 on a1.idp_id = b1.idp_id "
            sql &= " WHERE b1.status_id IN (5,6,7) and ISNULL(b1.is_delete,0) = 0 and ISNULL(b1.is_cancel,0) = 0 and b1.report_dept_id is not null "
            sql &= " ) total ON a.idp_id = total.total1 "

            sql &= " LEFT OUTER JOIN (select a1.idp_id as total_request "
            sql &= " FROM idp_external_req a1 INNER JOIN idp_trans_list b1 on a1.idp_id = b1.idp_id "
            sql &= " WHERE b1.status_id > 1 and ISNULL(b1.is_delete,0) = 0 and ISNULL(b1.is_cancel,0) = 0 and b1.report_dept_id is not null "
            sql &= " ) total_all ON a.idp_id = total_all.total_request "

            sql &= " LEFT OUTER JOIN (SELECT SUM(train_hour) as hour1 , s2.report_dept_id FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            sql &= " GROUP BY s2.report_dept_id) "
            sql &= " t1 on b.report_dept_id = t1.report_dept_id "

            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget , s2.report_dept_id FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            sql &= " GROUP BY s2.report_dept_id) "
            sql &= " t2 on b.report_dept_id = t2.report_dept_id "

            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense , s2.report_dept_id FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2 and isnull(s3.acc_receive_by,'') <> '' "
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0  "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            sql &= " GROUP BY s2.report_dept_id) "
            sql &= " t3 on b.report_dept_id = t3.report_dept_id "


            sql &= " WHERE (b.status_id > 1 And ISNULL(b.is_delete, 0) = 0 And ISNULL(b.is_cancel, 0) = 0 And b.report_dept_id Is Not null AND a.request_type = 'ext') "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(a.date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedValue <> "" Then
                ' sql &= "  AND up.dept_id = " & txtdept.SelectedValue
                sql &= "  AND b.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtjobtitle.SelectedValue <> "" Then
                '  sql &= " AND b.report_jobtitle LIKE '%" & txtjobtitle.SelectedValue & "%' "
            End If

            If txtjobtype.SelectedValue <> "" Then
                ' sql &= " AND b.report_jobtype LIKE '%" & txtjobtype.SelectedValue & "%' "
            End If

            If viewtype = "dept" Then
                ' sql &= " AND up.dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
                sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            sql &= " GROUP BY b.report_dept_id , b.report_dept_name "
            sql_pre = sql
            sql_grid = sql
            sql &= " ORDER BY  ISNULL(MAX(t3.expense),0) DESC , b.report_dept_name "
            ' Response.Write(sql)

            sql_grid &= " ORDER BY b.report_dept_name ASC , ISNULL(MAX(t3.expense),0) DESC  "

            ds2 = conn.getDataSet(sql, "t1") ' For expense graph

            ds = conn.getDataSet(sql_grid, "tGrid")

            If flag = "excel" Then
                Response.Clear()
                Response.Buffer = True
                Response.ClearContent()
                Response.Charset = "Windows-874"
                Response.ContentEncoding = System.Text.Encoding.UTF8
                Me.EnableViewState = False
                Response.AddHeader("content-disposition", "attachment;filename=ExternalTraining.xls")
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.ms-excel"
                Dim sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)
                Dim gv As New GridView()
                gv.DataSource = ds.Tables(0)
                gv.DataBind()
                gv.RenderControl(hw)
                Response.Output.Write(sw)
                Response.Flush()
                Response.[End]()
                Return
            End If

            lblNum.Text = ds.Tables("tGrid").Rows.Count
            Gridview1.DataSource = ds
            Gridview1.DataBind()

            If txtdate1.Text = "" Then
                lblDate1.Text = Date.Now.Year
                lblDate2.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblDate1.Text = txtdate1.Text
                lblDate2.Text = txtdate2.Text
            End If


            Dim limit = ""
            data1 = ""
            For i As Integer = 0 To ds2.Tables("t1").Rows.Count - 1
                If i = 5 Then
                    Exit For
                End If

                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If

                If CInt(ds2.Tables("t1").Rows(i)("expense").ToString) > 0 Then

                    data1 &= limit & "['" & ds2.Tables("t1").Rows(i)("report_dept_name").ToString.Trim & " (" & FormatNumber(CInt(ds2.Tables("t1").Rows(i)("expense").ToString), 0) & ")' , " & CInt(ds2.Tables("t1").Rows(i)("expense").ToString) & " ]"

                End If

            Next i

            If viewtype = "hr" Then
                Dim expense_other As Integer = 0
                Dim temp_expense As Integer = 0
                For i As Integer = 5 To ds2.Tables("t1").Rows.Count - 1
                    Try
                        temp_expense = CInt(ds2.Tables("t1").Rows(i)("expense").ToString)
                    Catch ex As Exception
                        temp_expense = 0
                    End Try
                    expense_other += temp_expense
                Next i

                data1 &= ",['Other Department'," & expense_other & "]"
            End If


            sql_pre &= " ORDER BY COUNT(distinct total1) DESC "
            ds = conn.getDataSet(sql_pre, "t2")
            data2 = ""
            data2_name = ""
            For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1
                If i = 5 Then
                    Exit For
                End If


                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                'data2 &= limit & ds.Tables("t2").Rows(i)("num").ToString
                'data2_name &= limit & "'" & ds.Tables("t2").Rows(i)("report_dept_name").ToString.Trim & "'"
                data2 &= limit & "['" & ds.Tables("t2").Rows(i)("report_dept_name").ToString.Trim & " ( " & CInt(ds.Tables("t2").Rows(i)("num").ToString) & " )' , " & CInt(ds.Tables("t2").Rows(i)("num").ToString) & " ]"

            Next i

            If viewtype = "hr" Then
                Dim other_num As Integer = 0
                Dim temp_num As Integer = 0
                For i As Integer = 5 To ds.Tables("t2").Rows.Count - 1
                    Try
                        temp_num = CInt(ds.Tables("t2").Rows(i)("num").ToString)
                    Catch ex As Exception
                        temp_num = 0
                    End Try
                    other_num += temp_num
                Next i

                data2 &= ",['Other Department'," & other_num & "]"
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReport2()
        Dim sql As String
        Dim ds As New DataSet
        Dim total As Double = 0
        Try
            'sql = "SELECT COUNT(*) from  idp_external_req aa INNER JOIN idp_trans_list bb on aa.idp_id = bb.idp_id WHERE ISNULL(bb.is_delete,0) = 0 AND ISNULL(bb.is_cancel,0) = 0 AND bb.status_id > 1 "
            'If txtdate1.Text <> "" And txtdate2.Text <> "" Then
            '    sql &= " AND date_submit_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            'End If
            'ds = conn.getDataSet(sql, "t0")
            'If ds.Tables("t0").Rows.Count > 0 Then
            '    total = CDbl(ds.Tables("t0").Rows(0)(0).ToString)
            'End If

            'If total = 0 Then
            '    total = 1
            'End If

            'Response.Write(total)

            For i As Integer = 1 To 7
                sql = "SELECT COUNT(chk_attend" & i & ") from  idp_external_req aa INNER JOIN idp_trans_list bb on aa.idp_id = bb.idp_id WHERE ISNULL(bb.is_delete,0) = 0 AND ISNULL(bb.is_cancel,0) = 0 AND bb.status_id > 1 AND  ISNULL(chk_attend" & i & ",0) = 1 "
                If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                    sql &= " AND date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
                Else
                    sql &= " AND YEAR(aa.date_start) = " & Date.Now.Year
                End If
                If viewtype = "dept" Then
                    sql &= " AND bb.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
                End If

                If txtdept.SelectedValue <> "" Then
                    sql &= "  AND bb.report_dept_id = " & txtdept.SelectedValue
                End If
                '    Response.Write(sql & "<br/>")
                ds = conn.getDataSet(sql, "t" & i)
                If ds.Tables("t" & i).Rows.Count > 0 Then
                    '   Response.Write(total)
                    total += CDbl(ds.Tables("t" & i).Rows(0)(0).ToString)
                    '  CType(Panel_Motivation.FindControl("lblp" & i), Label).Text = FormatNumber((CDbl(ds.Tables("t" & i).Rows(0)(0).ToString) / total) * 100, 1) & "%"
                Else
                    total += 0
                    '  CType(Panel_Motivation.FindControl("lblp" & i), Label).Text = "-"
                End If
            Next i

            Dim limit = ""
            data3 = ""
            For i As Integer = 1 To 7
                sql = "SELECT COUNT(chk_attend" & i & ") from  idp_external_req aa INNER JOIN idp_trans_list bb on aa.idp_id = bb.idp_id WHERE ISNULL(bb.is_delete,0) = 0 AND ISNULL(bb.is_cancel,0) = 0 AND bb.status_id > 1 AND  ISNULL(chk_attend" & i & ",0) = 1 "
                If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                    sql &= " AND date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
                Else
                    sql &= " AND YEAR(aa.date_start) = " & Date.Now.Year
                End If
                If viewtype = "dept" Then
                    sql &= " AND bb.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
                End If

                If txtdept.SelectedValue <> "" Then
                    sql &= "  AND bb.report_dept_id = " & txtdept.SelectedValue
                End If
                ' Response.Write(sql & "<br/>")
                ds = conn.getDataSet(sql, "t" & i)
                If ds.Tables("t" & i).Rows.Count > 0 Then
                    '   Response.Write(total)
                    If i = 1 Then
                        limit = ""
                    Else
                        limit = ","
                    End If
                    data3 &= limit & "['" & "M" & (i) & "' , " & CInt(ds.Tables("t" & i).Rows(0)(0).ToString) & " ]"
                    CType(Panel_Motivation.FindControl("lblp" & i), Label).Text = FormatNumber((CDbl(ds.Tables("t" & i).Rows(0)(0).ToString) / total) * 100, 1) & "%"
                    CType(Panel_Motivation.FindControl("lbln" & i), Label).Text = ds.Tables("t" & i).Rows(0)(0).ToString

                Else

                    CType(Panel_Motivation.FindControl("lblp" & i), Label).Text = "-"
                End If
            Next i

            If txtdate1.Text = "" Then
                lblDate3.Text = Date.Now.Year
                lblDate4.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblDate3.Text = txtdate1.Text
                lblDate4.Text = txtdate2.Text
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReport3()
        Dim sql As String
        Dim ds As New DataSet
        Dim limit As String = ""
        Try

            sql = " select    b.report_dept_id , b.report_dept_name  , COUNT(approve) as approve , COUNT(complete) as complete , COUNT(total1) as total , COUNT(action_complete) as action1 "
            sql &= " FROM idp_external_req a INNER JOIN idp_trans_list b on a.idp_id = b.idp_id "

            sql &= " LEFT OUTER JOIN (select aa.idp_id as approve"
            sql &= " FROM idp_external_req aa INNER JOIN idp_trans_list bb on aa.idp_id = bb.idp_id "
            sql &= " WHERE bb.status_id IN (5,6,7) and ISNULL(bb.is_delete,0) = 0 and ISNULL(bb.is_cancel,0) = 0  "
            sql &= " AND bb.report_dept_id is not null) app on b.idp_id = app.approve "

            sql &= " LEFT OUTER JOIN (select aaa.idp_id as complete "
            sql &= " FROM idp_external_req aaa INNER JOIN idp_trans_list bbb on aaa.idp_id = bbb.idp_id "
            sql &= " WHERE  getdate() > aaa.date_end  and bbb.status_id IN (5,6,7) "
            sql &= " and ISNULL(bbb.is_delete,0) = 0 and ISNULL(bbb.is_cancel,0) = 0 and bbb.report_dept_id is not null "
            sql &= " ) finish on a.idp_id = finish.complete "

            sql &= " LEFT OUTER JOIN (select a1.idp_id as total1 "
            sql &= " FROM idp_external_req a1 INNER JOIN idp_trans_list b1 on a1.idp_id = b1.idp_id "
            sql &= " WHERE b1.status_id > 1 and ISNULL(b1.is_delete,0) = 0 and ISNULL(b1.is_cancel,0) = 0 and b1.report_dept_id is not null "
            sql &= " ) total ON a.idp_id = total.total1 "

            sql &= " LEFT OUTER JOIN (select aaa.idp_id as action_complete "
            sql &= " FROM idp_external_req aaa INNER JOIN idp_trans_list bbb on aaa.idp_id = bbb.idp_id "
            sql &= " WHERE  getdate() > aaa.date_end  and bbb.status_id IN (5,6,7) "
            sql &= " and ISNULL(bbb.is_delete,0) = 0 and ISNULL(bbb.is_cancel,0) = 0 and bbb.report_dept_id is not null and ISNULL(aaa.goal_skill_level,'') <> '' and ISNULL(aaa.goal_skill_level_aftertraining,'') <> ''"
            sql &= " ) action1 on a.idp_id = action1.action_complete  "

            sql &= " WHERE(b.status_id > 1 And ISNULL(b.is_delete, 0) = 0 And ISNULL(b.is_cancel, 0) = 0 And b.report_dept_id Is Not null) "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                If viewtype <> "" Then
                    sql &= " AND YEAR(a.date_start) = " & Date.Now.Year
                End If

            End If
            If viewtype = "" Then
                sql &= " AND b.report_emp_code = " & Session("emp_code").ToString
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= "  AND b.report_dept_id = " & txtdept.SelectedValue
            End If

            If viewtype = "dept" Then
                sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            sql &= " GROUP BY b.report_dept_id , b.report_dept_name "
            sql &= " HAVING ISNULL(COUNT(complete),0) > 0 "
            sql &= " ORDER BY   COUNT(action_complete) ASC "

            'Response.Write(sql)


            ds = conn.getDataSet(sql, "t1")
            lblNum.Text = ds.Tables("t1").Rows.Count
            Gridview3.DataSource = ds
            Gridview3.DataBind()
            data4_1 = ""
            data4_2 = ""
            data4_name = ""
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 5 Then
                    Exit For
                End If
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                data4_1 &= limit & ds.Tables("t1").Rows(i)("complete").ToString
                data4_2 &= limit & ds.Tables("t1").Rows(i)("action1").ToString
                data4_name &= limit & "'" & ds.Tables("t1").Rows(i)("report_dept_id").ToString.Trim & "'"
            Next i

            If txtdate1.Text = "" Then
                lblDate5.Text = Date.Now.Year
                lblDate6.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblDate5.Text = txtdate1.Text
                lblDate6.Text = txtdate2.Text
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportExternalSummaryByCategory()
        Dim sql As String
        Dim ds As New DataSet

        Try

            sql = "SELECT a.report_dept_id AS 'Cost Center' , a.report_dept_name AS 'Department Name' , category_name_en AS 'Training category' , COUNT(a.report_dept_id) AS 'Total Request' "
            sql &= ", ISNULL(MAX(c.total_approve),0) AS 'Total Approve' ,  CONVERT(VARCHAR(15), CAST(ISNULL(MAX(tBudget.budget),0) AS MONEY), 1) AS 'Budget Approved'"
            sql &= " , CONVERT(VARCHAR(15), CAST(ISNULL(MAX(t3.expense),0) AS MONEY), 1) AS 'Actual Expense'"
            sql &= " , SUM(ISNULL(b.train_hour,0)) AS 'Training Hour (Total Request)'"
            sql &= " FROM idp_trans_list a INNER JOIN idp_external_req b ON a.idp_id = b.idp_id"

            sql &= " LEFT OUTER JOIN idp_m_category d ON b.ext_category_id = d.category_id "

            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT a1.report_dept_id ,  a2.ext_category_id , COUNT(a1.report_dept_id) AS total_approve FROM idp_trans_list a1 "
            sql &= " INNER JOIN idp_external_req a2 ON a1.idp_id = a2.idp_id "
            sql &= " AND a1.status_id IN (5,6,7) "
            sql &= " WHERE ISNULL(a1.is_delete,0) = 0 and ISNULL(a1.is_cancel,0) = 0 and a2.request_type = 'ext'"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a2.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(a2.date_start) = " & Date.Now.Year
            End If
            sql &= " GROUP BY a1.report_dept_id , a2.ext_category_id)  c ON a.report_dept_id = c.report_dept_id AND b.ext_category_id = c.ext_category_id "

            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget , s2.report_dept_id , s1.ext_category_id FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0  and s1.is_budget_approve = 1 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            sql &= " GROUP BY s2.report_dept_id , s1.ext_category_id) "
            sql &= " tBudget on a.report_dept_id = tBudget.report_dept_id AND b.ext_category_id = tBudget.ext_category_id "


            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense , s2.report_dept_id , s1.ext_category_id FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2 and isnull(s3.acc_receive_by,'') <> '' "
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 and ISNULL(s3.is_delete,0) = 0   "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            sql &= " GROUP BY s2.report_dept_id , s1.ext_category_id) "
            sql &= " t3 on a.report_dept_id = t3.report_dept_id AND b.ext_category_id = t3.ext_category_id "

            sql &= " WHERE a.status_id > 1 and ISNULL(a.is_delete,0) = 0 and ISNULL(a.is_cancel,0) = 0 and b.request_type = 'ext'"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " GROUP BY a.report_dept_id , a.report_dept_name , d.category_name_en "
            sql &= " ORDER BY a.report_dept_name "
            ' sql &= " GROUP BY a.internal_title ,  d.dept_name "

            'Response.Write(sql)
            'Return
            ds = conn.getDataSet(sql, "t1")
            ' lblIntDate1.Text = txtdate1.Text
            ' lblIntDate2.Text = txtdate2.Text
            lblNumInternal1.Text = ds.Tables("t1").Rows.Count

            GridViewDynamic.DataSource = ds
            GridViewDynamic.DataBind()

            If txtdate1.Text = "" Then
                lblDynamicDate1.Text = Date.Now.Year
                lblDynamicDate2.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblDynamicDate1.Text = txtdate1.Text
                lblDynamicDate2.Text = txtdate2.Text
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportInternalSummaryByRequest()
        Dim sql As String
        Dim ds As New DataSet

        Try

            sql = "SELECT a.report_dept_id AS 'Cost Center' , a.report_dept_name AS 'Department Name', COUNT(a.report_dept_id) AS 'Total Request' "
            sql &= ", ISNULL(MAX(c.total_approve),0) AS 'Total Approve' ,  CONVERT(VARCHAR(15), CAST(ISNULL(MAX(tBudget.budget),0) AS MONEY), 1) AS 'Budget Approved'"
            sql &= " , CONVERT(VARCHAR(15), CAST(ISNULL(MAX(t3.expense),0) AS MONEY), 1) AS 'Actual expense'"
            sql &= " FROM idp_trans_list a INNER JOIN idp_external_req b ON a.idp_id = b.idp_id"

            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT a1.report_dept_id , COUNT(a1.report_dept_id) AS total_approve FROM idp_trans_list a1 "
            sql &= " INNER JOIN idp_external_req a2 ON a1.idp_id = a2.idp_id "
            sql &= " AND a1.status_id IN (5,6,7) "
            sql &= " WHERE ISNULL(a1.is_delete,0) = 0 and ISNULL(a1.is_cancel,0) = 0 and a2.request_type = 'int'"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
                sql &= " AND a1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "
            End If
            sql &= " GROUP BY a1.report_dept_id) c ON a.report_dept_id = c.report_dept_id "

            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget , s2.report_dept_id FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'int' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 and s1.is_budget_approve = 1 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "
            End If
            sql &= " GROUP BY s2.report_dept_id) "
            sql &= " tBudget on a.report_dept_id = tBudget.report_dept_id "


            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense , s2.report_dept_id FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2 and isnull(s3.acc_receive_by,'') <> '' "
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'int' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 and ISNULL(s3.is_delete,0) = 0   "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "
            End If
            sql &= " GROUP BY s2.report_dept_id) "
            sql &= " t3 on a.report_dept_id = t3.report_dept_id "

            sql &= " WHERE status_id > 1 and ISNULL(is_delete,0) = 0 and ISNULL(is_cancel,0) = 0 and b.request_type = 'int'"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
                sql &= " AND a.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "
            End If

            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " GROUP BY a.report_dept_id , a.report_dept_name"
            ' sql &= " GROUP BY a.internal_title ,  d.dept_name "

            'Response.Write(sql)
            'Return
            ds = conn.getDataSet(sql, "t1")
            'lblIntDate1.Text = txtdate1.Text
            ' lblIntDate2.Text = txtdate2.Text
            lblNumInternal1.Text = ds.Tables("t1").Rows.Count

            GridViewDynamic.DataSource = ds
            GridViewDynamic.DataBind()



        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportInternalSummaryByCategory()
        Dim sql As String
        Dim ds As New DataSet

        Try

            sql = "SELECT a.report_dept_id AS 'Cost Center' , a.report_dept_name AS 'Department Name'"
            sql &= " , e.category_name_en AS 'Training category' , ISNULL(MAX(c.total_approve),0) AS 'Total Approve' "
            sql &= " , CONVERT(VARCHAR(15), CAST(ISNULL(MAX(tBudget.budget),0) AS MONEY), 1) AS 'Budget Approved' "
            sql &= " , CONVERT(VARCHAR(15), CAST(ISNULL(MAX(t3.expense),0) AS MONEY), 1) AS 'Actual expense' "
            sql &= " FROM idp_trans_list a INNER JOIN idp_external_req b ON a.idp_id = b.idp_id"

            sql &= " INNER JOIN idp_training_relate_idp d ON a.idp_id = d.idp_id"
            sql &= " INNER JOIN idp_m_category e ON d.category_id = e.category_id"

            sql &= " LEFT OUTER JOIN ( SELECT a3.category_id , a1.report_dept_id , COUNT(a3.category_id) AS total_approve "
            sql &= " FROM idp_trans_list a1 INNER JOIN idp_external_req a2 ON a1.idp_id = a2.idp_id AND a1.status_id IN (5,6,7)"
            sql &= " INNER JOIN idp_training_relate_idp a3 ON a1.idp_id = a3.idp_id"
            sql &= " WHERE ISNULL(a1.is_delete,0) = 0 and ISNULL(a1.is_cancel,0) = 0 and a2.request_type = 'int' "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
                sql &= " AND a1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "
            End If
            If viewtype = "dept" Then
                sql &= " AND a1.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If

            sql &= " GROUP BY a3.category_id , a1.report_dept_id) c ON d.category_id = c.category_id "
            sql &= " and a.report_dept_id = c.report_dept_id"


            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) "
            sql &= " else 1 end)) as budget , s4.category_id , s2.report_dept_id FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 on s1.idp_id = s2.idp_id "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1 "
            sql &= " inner join idp_training_relate_idp s4 on s1.idp_id = s4.idp_id"
            sql &= " where isnull(s2.is_cancel,0) = 0 AND s1.request_type = 'int' and s2.status_id IN (5,6,7) "
            sql &= " and ISNULL(s2.is_delete,0) = 0 and s4.category_id > 0 and s1.is_budget_approve = 1"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "
            End If
            If viewtype = "dept" Then
                sql &= " AND s2.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If

            sql &= " GROUP BY s4.category_id , s2.report_dept_id) tBudget on d.category_id = "
            sql &= " tBudget.category_id and a.report_dept_id = tBudget.report_dept_id"

            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 "
            sql &= " then isnull(exchange_rate,1) else 1 end)) as expense , s4.category_id , s2.report_dept_id FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 on s1.idp_id = s2.idp_id left outer join idp_training_expense s3 "
            sql &= " on s1.idp_id = s3.idp_id and accouting_type = 2 and isnull(s3.acc_receive_by,'') <> '' "
            sql &= " inner join idp_training_relate_idp s4 on s1.idp_id = s4.idp_id"
            sql &= " where isnull(s2.is_cancel,0) = 0 AND s1.request_type = 'int' and s2.status_id IN (5,6,7) "
            sql &= " and ISNULL(s2.is_delete,0) = 0 and s4.category_id > 0 and ISNULL(s3.is_delete,0) = 0 "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "
            End If
            If viewtype = "dept" Then
                sql &= " AND s2.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If

            sql &= " GROUP BY s4.category_id , s2.report_dept_id) t3 on d.category_id = "
            sql &= " t3.category_id and a.report_dept_id = t3.report_dept_id"

            sql &= " WHERE a.status_id > 1 And ISNULL(a.is_delete, 0) = 0 And ISNULL(a.is_cancel, 0) = 0"
            sql &= " and b.request_type = 'int' AND d.category_id > 0"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
                sql &= " AND a.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "
            End If

            If viewtype = "dept" Then
                sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
           
            End If

            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " GROUP BY a.report_dept_id , a.report_dept_name ,d.category_id , e.category_name_en"
            ' sql &= " GROUP BY a.internal_title ,  d.dept_name "

            'Response.Write(sql)
            'Return
            ds = conn.getDataSet(sql, "t1")
            ' lblIntDate1.Text = txtdate1.Text
            ' lblIntDate2.Text = txtdate2.Text
            lblNumInternal1.Text = ds.Tables("t1").Rows.Count

            GridViewDynamic.DataSource = ds
            GridViewDynamic.DataBind()

            If txtdate1.Text = "" Then
                lblDynamicDate1.Text = Date.Now.Year
                lblDynamicDate2.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblDynamicDate1.Text = txtdate1.Text
                lblDynamicDate2.Text = txtdate2.Text
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportInternalSummary()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select ISNULL(max(b.total_require),0) as total_require , ISNULL(max(d.total1),0) as num_employee  , min(a.schedule_start_ts) as start_date , max(a.schedule_end_ts) as end_date"
            sql &= " , e.internal_title , e.idp_id , ISNULL(MAX(t2.budget),0) as budget , ISNULL(f.target_num,0) AS target_num  from idp_training_schedule  a   "

            sql &= " inner join (SELECT  s4.idp_id , COUNT(  s4.idp_id ) as total_require "
            sql &= " FROM (select a1.idp_id , a1.emp_code from idp_training_registered a1 inner join idp_training_schedule a2 on a1.schedule_id = a2.schedule_id where a1.is_register = 1  group by a1.idp_id , a1.emp_code  ) s1 "
            sql &= " inner join user_profile s2 on s1.emp_code = s2.emp_code  "
            ' sql &= " inner join idp_training_schedule s3 on s1.schedule_id = s3.schedule_id  "
            sql &= " inner join (select emp_code , idp_id from idp_training_employee group by emp_code,idp_id) s4 on s1.emp_code = s4.emp_code and s1.idp_id = s4.idp_id "
        
            sql &= " group by s4.idp_id "
            sql &= "  ) b on a.idp_id = b.idp_id  "

            sql &= "inner join idp_trans_list c on a.idp_id = c.idp_id and c.status_id > 1 "
            If viewtype = "dept" Then
                sql &= " and c.idp_id in (select a1.idp_id from idp_training_employee a1 inner join user_profile a2 on a1.emp_code = a2.emp_code where a2.dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") ) "
            End If
            sql &= " inner join idp_external_req e on c.idp_id = e.idp_id "

            sql &= "left outer join (SELECT COUNT(s1.schedule_id) as total1 , s3.idp_id    "
            sql &= " FROM idp_training_registered s1 "
            sql &= " inner join user_profile s2 on s1.emp_code = s2.emp_code "
            sql &= " inner join idp_training_schedule s3 on s1.schedule_id = s3.schedule_id "
            sql &= " WHERE s1.is_register = 1 GROUP BY s3.idp_id) d on c.idp_id = d.idp_id  "

            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget ,  s1.idp_id FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            'sql &= "  inner join idp_training_schedule s4 on s1.idp_id = s4.idp_id "
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'int' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "
            End If
            sql &= " GROUP BY s1.idp_id) "
            sql &= " t2 on a.idp_id = t2.idp_id "


            sql &= " left outer join (select count(emp_code) as target_num  , idp_id "
            sql &= "from idp_training_employee where 1 = 1 group by idp_id) f on c.idp_id = f.idp_id"

            sql &= " WHERE (c.status_id IN (5,6,7) And ISNULL(c.is_delete, 0) = 0 And ISNULL(c.is_cancel, 0) = 0 And c.report_dept_id Is Not null AND e.request_type = 'int') "


            If viewtype = "dept" Then ' Manager
                If txtdept.SelectedValue <> "" Then ' Filter Department
                    '   sql &= " AND  b.dept_id = " & txtdept.SelectedValue
                Else ' See all granted department
                    ' sql &= " AND b.dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
                End If

            End If

            If txtdept.SelectedIndex > 0 Then
                sql &= " AND  c.report_dept_id = " & txtdept.SelectedValue
            End If

          

            If txtinternal_topic.SelectedValue <> "" Then
                sql &= " AND (e.internal_title) LIKE '%" & txtinternal_topic.SelectedValue & "%' "
            Else
                'Response.Write(111111111111111111)
                ' Return
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                ' sql &= " AND a.schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
                sql &= " AND c.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                sql &= " AND c.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE YEAR(schedule_start) = " & Date.Now.Year & " AND ISNULL(is_delete,0) = 0 ) "

            End If

          

            sql &= " group by e.internal_title , e.idp_id , f.target_num   "
            sql &= " ORDER BY e.internal_title  "
            ' sql &= " GROUP BY a.internal_title ,  d.dept_name "

            ' Response.Write(sql)
            'Return
            ds = conn.getDataSet(sql, "t1")
           
            lblNumInternal1.Text = ds.Tables("t1").Rows.Count
            Gridview4.DataSource = ds
            Gridview4.DataBind()

            If txtdate1.Text = "" And txtdate2.Text = "" Then
                lblIntDate1.Text = Date.Now.Year
                lblIntDate2.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblIntDate1.Text = txtdate1.Text
                lblIntDate2.Text = txtdate2.Text
            End If
     

        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportInternalHistory(Optional flag As String = "")
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select  "
            If flag = "" Then
                sql &= " * , b.training_hour as work_hour "
                sql &= " , case when b.is_evaluate = 1 then 'Yes' else 'No' end as evaluate "
                sql &= " , f.schedule_end , f.schedule_start "
                sql &= " , case when isnull(attendance_type_name,'')='' then 'ผู้เข้าอบรม/ Trainee' else attendance_type_name end AS attendance_type_name1 "
            Else
                sql &= " schedule_type AS 'Training Type' , c.emp_code AS 'Employee Code' , emp_name AS 'Employee Name' , c.dept_name AS 'Department' "
                sql &= " , internal_title AS 'Course Topic' , CONVERT(VARCHAR(10),a.schedule_start,103) AS 'Start Training Date' , CONVERT(VARCHAR(10),a.schedule_end,103) AS 'Finish Training Date'  "
                sql &= " , case when isnull(register_time,0) > 0 then 'Yes' else 'No' end AS 'Class Attended' "
                sql &= " , case when isnull(attendance_type_name,'')='' then 'ผู้เข้าอบรม/ Trainee' else attendance_type_name end AS 'Attendance Type' ,  b.training_hour  AS  'Training Hour' "
                sql &= " , case when b.is_evaluate = 1 then 'Yes' else 'No' end as evaluate "
            End If

            sql &= "  from idp_training_schedule  a   "
            sql &= " INNER JOIN idp_training_registered b ON a.schedule_id = b.schedule_id AND b.is_register = 1 "
            sql &= " INNER JOIN user_profile c ON b.emp_code = c.emp_code "

            sql &= " inner join idp_trans_list d on a.idp_id = d.idp_id and d.status_id > 1 "
            sql &= " inner join idp_external_req e on d.idp_id = e.idp_id "

            sql &= " inner join (select max(schedule_end) as schedule_end ,  MIN(schedule_start) as schedule_start , idp_id "
            sql &= "from idp_training_schedule where ISNULL(schedule_start,'') <> '' group by idp_id) f on d.idp_id = f.idp_id"

            sql &= " WHERE (d.status_id > 1 And ISNULL(d.is_delete, 0) = 0 And ISNULL(d.is_cancel, 0) = 0 And d.report_dept_id Is Not null AND e.request_type = 'int') "

            sql &= " AND b.is_register = 1 " ' ลงทะเบียนเข้าเรียนแล้ว

            If viewtype = "dept" Then
                sql &= " AND c.dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
                'sql &= " AND b.dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If

            If viewtype = "" Then
                sql &= " AND b.emp_code = " & Session("emp_code").ToString
            End If

            If txtinternal_topic_person.SelectedValue <> "" Then
                sql &= " AND (e.internal_title) = '" & txtinternal_topic_person.SelectedValue & "' "
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                If viewtype <> "" Then
                    sql &= " AND YEAR(a.schedule_start) = " & Date.Now.Year
                End If

            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND c.dept_id = " & txtdept.SelectedValue
                'sql &= " AND b.dept_id = " & txtdept.SelectedValue
            End If

            If txtempname.Text <> "" Then
                sql &= " AND (b.emp_name LIKE '%" & txtempname.Text & "%' OR b.emp_code LIKE '%" & txtempname.Text & "%')"
            End If

            If txtjobtitle.SelectedValue <> "" Then
                sql &= " AND b.job_title LIKE '%" & txtjobtitle.Text & "%' "
            End If

            If txtjobtype.SelectedValue <> "" Then
                sql &= " AND b.job_title LIKE '%" & txtjobtype.Text & "%' "
            End If

            'sql &= " ORDER BY b.dept_name ,  e.internal_title "
            ' sql &= " GROUP BY a.internal_title ,  d.dept_name "

            'Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")
         

            If flag = "excel" Then

                Response.Clear()
                Response.Buffer = True
                Response.ClearContent()
                Response.Charset = "Windows-874"
                Response.ContentEncoding = System.Text.Encoding.UTF8
                Me.EnableViewState = False
                Response.AddHeader("content-disposition", "attachment;filename=InternalTraining.xls")
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.ms-excel"
                Dim sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)
                Dim gv As New GridView()
                gv.DataSource = ds.Tables(0)
                gv.DataBind()
                gv.RenderControl(hw)
                Response.Output.Write(sw)
                Response.Flush()
                Response.[End]()

            Else
                ' lblIntDate1.Text = txtdate1.Text
                '  lblIntDate2.Text = txtdate2.Text
                lblDateInternal1.Text = txtdate1.Text
                lblDateInternal2.Text = txtdate2.Text

               

                If txtdate1.Text = "" And txtdate2.Text = "" Then
                    lblDateInternal1.Text = Date.Now.Year
                    lblDateInternal2.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
                Else
                    lblDateInternal1.Text = txtdate1.Text
                    lblDateInternal2.Text = txtdate2.Text
                End If

                ' lblNumInternal1.Text = ds.Tables("t1").Rows.Count
                lblInternalNum.Text = FormatNumber(ds.Tables("t1").Rows.Count, 0)
                Gridview5.DataSource = ds
                Gridview5.DataBind()
            End If
            'txtempname.Text = ""
            'txtempname.Enabled = False
            'txtjobtitle.SelectedIndex = 0
            'txtjobtitle.Enabled = False
            'txtjobtype.SelectedIndex = 0
            'txtjobtype.Enabled = False
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindGridActionList()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql &= "select * FROM idp_external_req aaa  "
            sql &= " INNER JOIN idp_trans_list bbb on aaa.idp_id = bbb.idp_id WHERE getdate() >aaa.date_end "
            sql &= "  and bbb.status_id IN (5,6,7) and ISNULL(bbb.is_delete,0) = 0 and ISNULL(bbb.is_cancel,0) = 0 "
            sql &= " and bbb.report_dept_id is not null and ISNULL(aaa.goal_skill_level,'') = '' "
            sql &= " and ISNULL(aaa.goal_skill_level_aftertraining,'') = '' "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND bbb.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtempname.Text <> "" Then
                sql &= " AND (bbb.report_by LIKE '%" & txtempname.Text & "%' OR bbb.report_emp_code LIKE '%" & txtempname.Text & "%')"
            End If

            If txtjobtitle.SelectedValue <> "" Then
                sql &= " AND bbb.report_jobtitle LIKE '%" & txtjobtitle.Text & "%' "
            End If

            If txtjobtype.SelectedValue <> "" Then
                sql &= " AND bbb.report_jobtype LIKE '%" & txtjobtype.Text & "%' "
            End If


            If viewtype = "dept" Then
                sql &= " AND bbb.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            ds = conn.getDataSet(sql, "t1")
            ' Response.Write(sql)
            GridviewActionList.DataSource = ds
            GridviewActionList.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindGridIndividualList(Optional flag As String = "")
        Dim sql As String = ""
        Dim ds As New DataSet
        Dim date_sql As String = ""
        Try
            If flag = "excel" Then
                date_sql = "date_start  , date_end "
            Else
                date_sql = "date_start_ts , date_end_ts "
            End If
            sql = " select a.idp_id , a.request_type , idp_no , e.status_name , ext_title , report_by , " & date_sql & " , ISNULL(train_hour,0) AS train_hour , ISNULL(SUM(expense_value),0) AS expense  "
            sql &= " , case when ISNULL(goal_skill_level,'') <> '' then 'Complete' else 'Waiting' end AS action_complete "
            sql &= " FROM idp_external_req a INNER JOIN idp_trans_list b on a.idp_id = b.idp_id AND a.request_type = 'ext' "

            sql &= " LEFT OUTER JOIN idp_training_expense c ON a.idp_id = c.idp_id AND c.accouting_type = 2 AND c.is_delete = 0 AND ISNULL(c.acc_receive_by,'') <> '' "
            ' sql &= " LEFT OUTER JOIN idp_budget_request d ON c.expense_request_type_id = d.request_id AND d.is_request_budget = 0 "
            sql &= " INNER JOIN idp_status_list e ON b.status_id = e.idp_status_id "
            'sql &= " LEFT OUTER JOIN (select aa.idp_id as approve"
            'sql &= " FROM idp_external_req aa INNER JOIN idp_trans_list bb on aa.idp_id = bb.idp_id "
            'sql &= " WHERE bb.status_id IN (5,6) and ISNULL(bb.is_delete,0) = 0 and ISNULL(bb.is_cancel,0) = 0  "
            'sql &= " AND bb.report_dept_id is not null) app on b.idp_id = app.approve "

            'sql &= " LEFT OUTER JOIN (select aaa.idp_id as complete "
            'sql &= " FROM idp_external_req aaa INNER JOIN idp_trans_list bbb on aaa.idp_id = bbb.idp_id "
            'sql &= " WHERE  getdate() > aaa.date_end  and bbb.status_id IN (5,6) "
            'sql &= " and ISNULL(bbb.is_delete,0) = 0 and ISNULL(bbb.is_cancel,0) = 0 and bbb.report_dept_id is not null "
            'sql &= " ) finish on a.idp_id = finish.complete "

            'sql &= " LEFT OUTER JOIN (select a1.idp_id as total1 "
            'sql &= " FROM idp_external_req a1 INNER JOIN idp_trans_list b1 on a1.idp_id = b1.idp_id "
            'sql &= " WHERE b1.status_id > 1 and ISNULL(b1.is_delete,0) = 0 and ISNULL(b1.is_cancel,0) = 0 and b1.report_dept_id is not null "
            'sql &= " ) total ON a.idp_id = total.total1 "

            'sql &= " LEFT OUTER JOIN (select aaa.idp_id as action_complete "
            'sql &= " FROM idp_external_req aaa INNER JOIN idp_trans_list bbb on aaa.idp_id = bbb.idp_id "
            'sql &= " WHERE  getdate() > aaa.date_end  and bbb.status_id IN (5,6) "
            'sql &= " and ISNULL(bbb.is_delete,0) = 0 and ISNULL(bbb.is_cancel,0) = 0 and bbb.report_dept_id is not null and ISNULL(aaa.goal_skill_level,'') <> '' and ISNULL(aaa.goal_skill_level_aftertraining,'') <> ''"
            'sql &= " ) action1 on a.idp_id = action1.action_complete  "

            sql &= " WHERE(b.status_id > 1 And ISNULL(b.is_delete, 0) = 0 And ISNULL(b.is_cancel, 0) = 0 And b.report_dept_id Is Not null) AND status_id IN (5,6,7)  "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If

            If txtempname.Text <> "" Then
                sql &= " AND (b.report_by LIKE '%" & txtempname.Text & "%' OR b.report_emp_code LIKE '%" & txtempname.Text & "%')"
            End If

            If viewtype = "dept" Then
                sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            ' sql &= " GROUP BY b.report_dept_id , b.report_dept_name "
            'sql &= " ORDER BY  COUNT(total1) DESC "
            If viewtype = "" Then
                sql &= " AND b.report_emp_code = " & Session("emp_code").ToString
            End If

            sql &= " GROUP BY  a.idp_id , a.request_type , idp_no , e.status_name , ext_title , report_by , " & date_sql & " , train_hour "
            sql &= " , case when ISNULL(goal_skill_level,'') <> '' then 'Complete' else 'Waiting' end "
            ' Response.Write(sql)

            ds = conn.getDataSet(sql, "t1")

            If flag = "excel" Then
                ' Response.Write(sql)
                ' Return
                Response.Clear()
                Response.Buffer = True
                Response.ClearContent()
                Response.Charset = "Windows-874"
                Response.ContentEncoding = System.Text.Encoding.UTF8
                Me.EnableViewState = False
                Response.AddHeader("content-disposition", "attachment;filename=ExternalTraining1.xls")
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.ms-excel"
                Dim sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)
                Dim gv As New GridView()
                gv.DataSource = ds.Tables(0)
                gv.DataBind()
                gv.RenderControl(hw)
                Response.Output.Write(sw)
                Response.Flush()
                Response.[End]()
                Return
            End If

            lblExternalNum.Text = ds.Tables("t1").Rows.Count

            GridIndividual.DataSource = ds
            GridIndividual.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridDeptIndividualList(Optional flag As String = "")
        Dim sql As String = ""
        Dim ds As New DataSet
        Dim date_sql As String = ""

        Try
            If flag = "excel" Then
                date_sql = "date_start  , date_end "
            Else
                date_sql = "date_start_ts , date_end_ts "
            End If
            sql = " select a.idp_id , a.request_type , idp_no , e.status_name , ext_title , report_emp_code , report_by , " & date_sql & " , ISNULL(train_hour,0) AS train_hour , ISNULL(SUM(expense_value),0) AS expense  "
            sql &= " , case when ISNULL(goal_skill_level,'') <> '' then 'Complete' else 'Waiting' end AS action_complete "
            sql &= " FROM idp_external_req a INNER JOIN idp_trans_list b on a.idp_id = b.idp_id "

            sql &= " LEFT OUTER JOIN idp_training_expense c ON a.idp_id = c.idp_id AND c.accouting_type = 2 AND c.is_delete = 0 AND ISNULL(c.acc_receive_by,'') <> ''"
            '  sql &= " LEFT OUTER JOIN idp_budget_request d ON c.expense_request_type_id = d.request_id AND d.is_request_budget = 0 "
            sql &= " INNER JOIN idp_status_list e ON b.status_id = e.idp_status_id "

            sql &= " INNER JOIN user_profile f ON b.report_emp_code = f.emp_code "

            sql &= " WHERE(b.status_id > 1 And ISNULL(b.is_delete, 0) = 0 And ISNULL(b.is_cancel, 0) = 0 And b.report_dept_id Is Not null) AND status_id IN (5,6,7) "
            sql &= " AND a.request_type = 'ext' "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(date_start) = " & Date.Now.Year
            End If



            If txtdept.SelectedValue <> "" Then
                'sql &= " AND b.report_dept_id = " & txtdept.SelectedValue
                sql &= " AND f.dept_id = " & txtdept.SelectedValue
            End If

            If txtempname.Text <> "" Then
                sql &= " AND (b.report_by LIKE '%" & txtempname.Text & "%' OR b.report_emp_code LIKE '%" & txtempname.Text & "%')"
            End If

            If txtjobtitle.SelectedValue <> "" Then
                sql &= " AND b.report_jobtitle LIKE '%" & txtjobtitle.Text & "%' "
            End If

            If txtjobtype.SelectedValue <> "" Then
                sql &= " AND b.report_jobtype LIKE '%" & txtjobtype.Text & "%' "
            End If

            If viewtype = "dept" Then
                'sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
                sql &= " AND f.dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            ' sql &= " GROUP BY b.report_dept_id , b.report_dept_name "
            'sql &= " ORDER BY  COUNT(total1) DESC "
            If viewtype = "" Then
                sql &= " AND b.report_emp_code = " & Session("emp_code").ToString
            End If

            sql &= " GROUP BY  a.idp_id , a.request_type , idp_no , e.status_name , ext_title , report_emp_code , report_by , " & date_sql & " , train_hour "
            sql &= " , case when ISNULL(goal_skill_level,'') <> '' then 'Complete' else 'Waiting' end "
            sql &= " ORDER BY ext_title ASC "
            ' Response.Write(sql)
            ' Response.End()
            ds = conn.getDataSet(sql, "t1")

            If flag = "excel" Then
                ' Response.Write(sql)
                ' Return
                Response.Clear()
                Response.Buffer = True
                Response.ClearContent()
                Response.Charset = "Windows-874"
                Response.ContentEncoding = System.Text.Encoding.UTF8
                Me.EnableViewState = False
                Response.AddHeader("content-disposition", "attachment;filename=ExternalTrainingDept.xls")
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/vnd.ms-excel"
                Dim sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)
                Dim gv As New GridView()
                gv.DataSource = ds.Tables(0)
                gv.DataBind()
                gv.RenderControl(hw)
                Response.Output.Write(sw)
                Response.Flush()
                Response.[End]()
                Return
            End If

            GridDeptIndividual.DataSource = ds
            GridDeptIndividual.DataBind()

            If txtdate1.Text = "" And txtdate2.Text = "" Then
                lblDateExtHistory1.Text = Date.Now.Year
                lblDateExtHistory2.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblDateExtHistory1.Text = txtdate1.Text
                lblDateExtHistory2.Text = txtdate2.Text
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridExpectAndExpense()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try

            sql = " select f.expect_detail , f.expect_detail_en , ISNULL(MAX(t2.budget),0) AS budget ,  ISNULL(MAX(t3.expense),0) AS expense ,  COUNT(f.expect_detail) AS num "

            sql &= " , (Select Count(*) From idp_trans_list aa inner join idp_external_req bb on aa.idp_id = bb.idp_id WHERE status_id IN(5,6,7) and ISNULL(is_delete,0) = 0 and ISNULL(is_cancel,0) = 0 and bb.request_type = 'ext' and report_dept_id is not null "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND bb.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(bb.date_start) = " & Date.Now.Year
            End If
            sql &= " AND (bb.expect_detail IN (SELECT expect_detail FROM idp_m_expect) OR bb.expect_detail IN (SELECT expect_detail_en FROM idp_m_expect) ) "
            sql &= " ) AS total"

            sql &= " FROM idp_external_req a INNER JOIN idp_trans_list b on a.idp_id = b.idp_id "

            ' sql &= " LEFT OUTER JOIN idp_training_expense c ON a.idp_id = c.idp_id AND c.accouting_type = 1 AND ISNULL(c.is_delete,0) = 0 "
            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget , s1.expect_detail , COUNT(s1.expect_detail) AS num FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            sql &= " and (s1.expect_detail IN (SELECT expect_detail FROM idp_m_expect) OR s1.expect_detail IN (SELECT expect_detail_en FROM idp_m_expect) ) "
            sql &= " GROUP BY s1.expect_detail) "
            sql &= " t2 on a.expect_detail = t2.expect_detail "

            sql &= " LEFT OUTER JOIN (SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense , s1.expect_detail FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2 AND ISNULL(acc_receive_by,0) <> '' "
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            sql &= " GROUP BY s1.expect_detail) "
            sql &= " t3 on a.expect_detail = t3.expect_detail "


            sql &= " INNER JOIN idp_status_list e ON b.status_id = e.idp_status_id "
            sql &= " INNER JOIN idp_m_expect f ON RTRIM(LTRIM(a.expect_detail)) = RTRIM(LTRIM(f.expect_detail)) OR RTRIM(LTRIM(a.expect_detail)) = RTRIM(LTRIM(f.expect_detail_en)) "
            sql &= " WHERE(b.status_id > 1 And ISNULL(b.is_delete, 0) = 0 And ISNULL(b.is_cancel, 0) = 0 And b.report_dept_id Is Not null) AND status_id IN (5,6,7) "
            sql &= " AND a.request_type = 'ext' "
            sql &= " AND (t2.expect_detail IN (SELECT expect_detail FROM idp_m_expect) OR a.expect_detail IN (SELECT expect_detail_en FROM idp_m_expect) )  "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(date_start) = " & Date.Now.Year
            End If



            If txtdept.SelectedValue <> "" Then
                sql &= " AND b.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtempname.Text <> "" Then
                sql &= " AND (b.report_by LIKE '%" & txtempname.Text & "%' OR b.report_emp_code LIKE '%" & txtempname.Text & "%')"
            End If

            If txtjobtitle.SelectedValue <> "" Then
                sql &= " AND b.report_jobtitle LIKE '%" & txtjobtitle.Text & "%' "
            End If

            If txtjobtype.SelectedValue <> "" Then
                sql &= " AND b.report_jobtype LIKE '%" & txtjobtype.Text & "%' "
            End If

            If viewtype = "dept" Then
                sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            ' sql &= " GROUP BY b.report_dept_id , b.report_dept_name "
            'sql &= " ORDER BY  COUNT(total1) DESC "
            If viewtype = "" Then
                sql &= " AND b.report_emp_code = " & Session("emp_code").ToString
            End If

            sql &= " GROUP BY  f.expect_detail , f.expect_detail_en "
            sql &= " ORDER BY  ISNULL(MAX(t2.budget),0) DESC "
            ' Response.Write(sql)
            ' Return
            ds = conn.getDataSet(sql, "t1")

            GridExpectOutcome.DataSource = ds
            GridExpectOutcome.DataBind()

            Dim limit = ""
            data_external_budget = ""
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 5 Then
                    '   Exit For
                End If

                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If

                If CInt(ds.Tables("t1").Rows(i)("budget").ToString) > 0 Then

                    data_external_budget &= limit & "['" & ds.Tables("t1").Rows(i)("expect_detail").ToString.Trim & " (" & FormatNumber(CInt(ds.Tables("t1").Rows(i)("budget").ToString), 0) & ")' , " & CInt(ds.Tables("t1").Rows(i)("budget").ToString) & " ]"

                End If

            Next i


            If txtdate1.Text = "" Then
                lblExpectDate1.Text = Date.Now.Year
                lblExpectDate2.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblExpectDate1.Text = txtdate1.Text
                lblExpectDate2.Text = txtdate2.Text
            End If


         
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridExpectAndExpenseNew()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try

            sql = "SELECT Expect AS 'Expect Outcome'  , CONVERT(varchar, CAST(Budget AS money), 1) AS 'Budget (Baht)' , CONVERT(varchar, CAST(expense AS money), 1)  AS 'Expense (Baht)' , num1 AS 'Total Request'"
            sql &= " FROM ("
            sql &= " select COUNT(*) as num1 , 'อบรมตามแผนการปรับระดับความก้าวหน้า' AS Expect , MAX(budget) AS Budget , MAX(expense) AS expense  from idp_trans_list a "
            sql &= " inner join idp_external_req b ON a.idp_id = b.idp_id and b.request_type = 'ext'"
            sql &= " and chk_ext_expect1 = 1 "
            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where(chk_ext_expect1 = 1 And ISNULL(s3.is_delete, 0) = 0)"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " group by chk_ext_expect1"
            sql &= " ) c ON b.chk_ext_expect1 = 1"

            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2"
            sql &= " where chk_ext_expect1 = 1 and ISNULL(s3.is_delete,0) = 0  AND ISNULL(acc_receive_by,0) <> ''"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " group by chk_ext_expect1"
            sql &= " ) d ON b.chk_ext_expect1 = 1"

            sql &= " where isnull(a.is_cancel,0) = 0 and isnull(a.is_delete,0) = 0 and isnull(a.is_ladder,0) = 0 and a.status_id IN (5,6,7)"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " group by chk_ext_expect1"

            sql &= " union "
            sql &= " select COUNT(*) as num1 , 'เพื่อให้ผลประเมินการทำงานส่วนบุคคลดีขึ้น' AS Expect , MAX(budget) AS Budget , MAX(expense) AS expense from idp_trans_list a "
            sql &= " inner join idp_external_req b ON a.idp_id = b.idp_id and b.request_type = 'ext'"
            sql &= " and chk_ext_expect2 = 1 "
            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where(chk_ext_expect2 = 1 And ISNULL(s3.is_delete, 0) = 0)"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect2"
            sql &= " ) c ON b.chk_ext_expect2 = 1"

            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2"
            sql &= " where chk_ext_expect2 = 1 and ISNULL(s3.is_delete,0) = 0  AND ISNULL(acc_receive_by,0) <> ''"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect2"
            sql &= " ) d ON b.chk_ext_expect2 = 1"

            sql &= " where isnull(a.is_cancel,0) = 0 and isnull(a.is_delete,0) = 0 and isnull(a.is_ladder,0) = 0 and a.status_id IN (5,6,7)"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect2"

            sql &= " union"

            sql &= " select COUNT(*) as num1 , 'เกิดความรู้และทักษะใหม่มาใช้ปฏิบัติงานเพื่อสนับสนุนเป้าหมายของแผนก' AS Expect , MAX(budget) AS Budget , MAX(expense) AS expense from idp_trans_list a "
            sql &= " inner join idp_external_req b ON a.idp_id = b.idp_id and b.request_type = 'ext'"
            sql &= " and chk_ext_expect3 = 1 "
            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where(chk_ext_expect3 = 1 And ISNULL(s3.is_delete, 0) = 0)"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect3"
            sql &= " ) c ON b.chk_ext_expect3 = 1"

            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2"
            sql &= " where chk_ext_expect3 = 1 and ISNULL(s3.is_delete,0) = 0  AND ISNULL(acc_receive_by,0) <> ''"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect3"
            sql &= " ) d ON b.chk_ext_expect3 = 1"

            sql &= " where isnull(a.is_cancel,0) = 0 and isnull(a.is_delete,0) = 0 and isnull(a.is_ladder,0) = 0 and a.status_id IN (5,6,7)"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect3"

            sql &= "  union"
            sql &= " select COUNT(*) as num1 , 'สร้างชื่อเสียงให้โรงพยาบาลโดยการเผยแพร่ผลงาน / งานวิจัย' AS Expect , MAX(budget) AS Budget , MAX(expense) AS expense from idp_trans_list a "
            sql &= " inner join idp_external_req b ON a.idp_id = b.idp_id and b.request_type = 'ext'"
            sql &= " and chk_ext_expect4 = 1 "
            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where(chk_ext_expect4 = 1 And ISNULL(s3.is_delete, 0) = 0)"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect4"
            sql &= " ) c ON b.chk_ext_expect4 = 1"

            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2"
            sql &= " where chk_ext_expect4 = 1 and ISNULL(s3.is_delete,0) = 0  AND ISNULL(acc_receive_by,0) <> ''"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect4"
            sql &= " ) d ON b.chk_ext_expect4 = 1"

            sql &= " where isnull(a.is_cancel,0) = 0 and isnull(a.is_delete,0) = 0 and isnull(a.is_ladder,0) = 0 and a.status_id IN (5,6,7)"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect4"

            sql &= " union"

            sql &= " select COUNT(*) as num1 , 'ทำให้เกิดบริการหรืองานใหม่ตามเป้าหมายของหน่วยงานหรือองค์กร' AS Expect , MAX(budget) AS Budget , MAX(expense) AS expense from idp_trans_list a "
            sql &= " inner join idp_external_req b ON a.idp_id = b.idp_id and b.request_type = 'ext'"
            sql &= " and chk_ext_expect5 = 1 "
            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where(chk_ext_expect5 = 1 And ISNULL(s3.is_delete, 0) = 0)"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect5"
            sql &= " ) c ON b.chk_ext_expect5 = 1"

            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2"
            sql &= " where chk_ext_expect5 = 1 and ISNULL(s3.is_delete,0) = 0  AND ISNULL(acc_receive_by,0) <> ''"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect5"
            sql &= " ) d ON b.chk_ext_expect5 = 1"

            sql &= " where isnull(a.is_cancel,0) = 0 and isnull(a.is_delete,0) = 0 and isnull(a.is_ladder,0) = 0 and a.status_id IN (5,6,7)"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect5"

            sql &= " union"
            sql &= " select COUNT(*) as num1 , 'อื่นๆ ' AS Expect , MAX(budget) , MAX(expense) AS expense from idp_trans_list a "
            sql &= " inner join idp_external_req b ON a.idp_id = b.idp_id and b.request_type = 'ext'"
            sql &= " and chk_ext_expect6 = 1 "
            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as budget "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 1"
            sql &= " where(chk_ext_expect6 = 1 And ISNULL(s3.is_delete, 0) = 0)"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect6"
            sql &= " ) c ON b.chk_ext_expect6 = 1"

            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT SUM(expense_value * (case when currency_type_id > 1 then isnull(exchange_rate,1) else 1 end)) as expense "
            sql &= " FROM idp_external_req s1 "
            sql &= " inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join idp_training_expense s3 on s1.idp_id = s3.idp_id and accouting_type = 2"
            sql &= " where chk_ext_expect6 = 1 and ISNULL(s3.is_delete,0) = 0  AND ISNULL(acc_receive_by,0) <> ''"
            sql &= " and isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'ext' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect6"
            sql &= " ) d ON b.chk_ext_expect6 = 1"

            sql &= " where isnull(a.is_cancel,0) = 0 and isnull(a.is_delete,0) = 0 and isnull(a.is_ladder,0) = 0 and a.status_id IN (5,6,7)"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " group by chk_ext_expect6"
            sql &= " ) aaa ORDER BY Budget DESC"


            ' Response.Write(sql)
            ' Return
            ds = conn.getDataSet(sql, "t1")

            GridViewDynamicExternalExpect.DataSource = ds
            GridViewDynamicExternalExpect.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridExpectAndExpenseInternal()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try

            sql = " select t2.expect_detail , ISNULL(MAX(t2.budget),0) AS budget ,  ISNULL(MAX(t3.expense),0) AS expense  , ISNULL(MAX(t2.num),0) AS num "
            sql &= " , (Select Count(*) From idp_trans_list aa inner join idp_external_req bb on aa.idp_id = bb.idp_id  inner join idp_training_relate_idp cc on aa.idp_id = cc.idp_id WHERE status_id IN(5,6,7) and ISNULL(is_delete,0) = 0 and ISNULL(is_cancel,0) = 0 and bb.request_type = 'int' and report_dept_id is not null "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                'sql &= " AND bb.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
                sql &= " AND aa.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(bb.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND report_dept_id = " & txtdept.SelectedValue
            End If
            '  sql 
            sql &= "  AND (cc.expect_detail IN (SELECT expect_detail FROM idp_m_expect WHERE ISNULL(is_expect_delete,0) = 0 ) OR cc.expect_detail IN (SELECT expect_detail_en FROM idp_m_expect WHERE ISNULL(is_expect_delete,0) = 0 ) ) "
            sql &= " ) AS total"
            sql &= " FROM idp_external_req a INNER JOIN idp_trans_list b on a.idp_id = b.idp_id "

            sql &= " INNER JOIN idp_training_relate_idp aa ON a.idp_id = aa.idp_id "


            sql &= " LEFT OUTER JOIN (SELECT max(budget) as budget , s4.expect_detail , COUNT(s4.expect_detail) AS num FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join "
            sql &= "   (select idp_id , SUM(expense_value * (case when currency_type_id > 1 "
            sql &= " then isnull(exchange_rate,1) else 1 end)) as budget  from  idp_training_expense"
            sql &= " where accouting_type = 1 group by idp_id )"
            sql &= "  s3 on s1.idp_id = s3.idp_id "
            sql &= "  inner join idp_training_relate_idp s4 on s1.idp_id = s4.idp_id "
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'int' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                ' sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " GROUP BY s4.expect_detail) "
            sql &= " t2 on aa.expect_detail = t2.expect_detail "

            sql &= " LEFT OUTER JOIN (SELECT max(expense) as expense , s4.expect_detail FROM idp_external_req s1 inner join idp_trans_list s2 "
            sql &= " on s1.idp_id = s2.idp_id  "
            sql &= " left outer join ( "
            sql &= " select idp_id , SUM(expense_value * (case when currency_type_id > 1 "
            sql &= " then isnull(exchange_rate,1) else 1 end)) as expense  from  idp_training_expense"
            sql &= "  where accouting_type = 2 AND ISNULL(acc_receive_by,'') <> '' group by idp_id  "
            sql &= ")"
            sql &= " s3 on s1.idp_id = s3.idp_id  "
            sql &= "  inner join idp_training_relate_idp s4 on s1.idp_id = s4.idp_id "
            sql &= " where  isnull(s2.is_cancel,0) = 0  AND s1.request_type = 'int' and s2.status_id IN (5,6,7) and ISNULL(s2.is_delete,0) = 0 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                ' sql &= " AND s1.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
                sql &= " AND s1.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(s1.date_start) = " & Date.Now.Year
            End If
            If txtdept.SelectedIndex > 0 Then
                sql &= " AND s2.report_dept_id = " & txtdept.SelectedValue
            End If
            sql &= " GROUP BY s4.expect_detail) "
            sql &= " t3 on aa.expect_detail = t3.expect_detail "

            sql &= " INNER JOIN idp_status_list e ON b.status_id = e.idp_status_id "

            '   sql &= " INNER JOIN idp_m_expect f ON RTRIM(LTRIM(a.expect_detail)) = RTRIM(LTRIM(f.expect_detail)) OR RTRIM(LTRIM(a.expect_detail)) = RTRIM(LTRIM(f.expect_detail_en)) "
            sql &= " WHERE(b.status_id > 1 And ISNULL(b.is_delete, 0) = 0 And ISNULL(b.is_cancel, 0) = 0 And b.report_dept_id Is Not null) AND status_id IN (5,6,7) "

            sql &= " AND a.request_type = 'int'   "

            sql &= " AND t2.expect_detail IN (SELECT expect_detail FROM idp_m_expect WHERE ISNULL(is_expect_delete,0) = 0) OR aa.expect_detail IN (SELECT expect_detail_en FROM idp_m_expect WHERE ISNULL(is_expect_delete,0) = 0)  "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                ' sql &= " AND date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
                sql &= " AND a.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                ' sql &= " AND YEAR(date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedIndex > 0 Then
                sql &= " AND b.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtempname.Text <> "" Then
                sql &= " AND (b.report_by LIKE '%" & txtempname.Text & "%' OR b.report_emp_code LIKE '%" & txtempname.Text & "%')"
            End If

            If txtjobtitle.SelectedValue <> "" Then
                sql &= " AND b.report_jobtitle LIKE '%" & txtjobtitle.Text & "%' "
            End If

            If txtjobtype.SelectedValue <> "" Then
                sql &= " AND b.report_jobtype LIKE '%" & txtjobtype.Text & "%' "
            End If

            If viewtype = "dept" Then
                sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            ' sql &= " GROUP BY b.report_dept_id , b.report_dept_name "
            'sql &= " ORDER BY  COUNT(total1) DESC "
            If viewtype = "" Then
                sql &= " AND b.report_emp_code = " & Session("emp_code").ToString
            End If

            sql &= " GROUP BY  t2.expect_detail  "
            sql &= " ORDER BY ISNULL(MAX(t2.budget),0) DESC "
            '; Response.Write(sql)
            'Return
            ds = conn.getDataSet(sql, "t1")

            GridExpectOutcomeInternal.DataSource = ds
            GridExpectOutcomeInternal.DataBind()

            Dim limit = ""
            data_internal_budget = ""
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 5 Then
                    '   Exit For
                End If

                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If

                If CInt(ds.Tables("t1").Rows(i)("budget").ToString) > 0 Then

                    data_internal_budget &= limit & "['" & ds.Tables("t1").Rows(i)("expect_detail").ToString.Trim & " (" & FormatNumber(CInt(ds.Tables("t1").Rows(i)("budget").ToString), 0) & ")' , " & CInt(ds.Tables("t1").Rows(i)("budget").ToString) & " ]"

                End If

            Next i

            If txtdate1.Text = "" Then
                lblInternalDate1.Text = Date.Now.Year
                lblInternalDate2.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblInternalDate1.Text = txtdate1.Text
                lblInternalDate2.Text = txtdate2.Text
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridActionDetail()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try

            sql = " select aa.action_detail , ISNULL(COUNT(action_detail),0) AS num "
            sql &= " , ( SELECT ISNULL(COUNT(*),0)  FROM idp_training_goal s1 INNER JOIN idp_external_req s2 ON s1.idp_id = s2.idp_id  "
            sql &= " INNER JOIN idp_trans_list s3 ON s1.idp_id = s3.idp_id WHERE s3.status_id > 1 AND ISNULL(s3.is_delete,0) = 0 AND ISNULL(s3.is_cancel, 0) = 0 And s3.report_dept_id Is Not null AND s3.status_id IN (5,6,7) AND ISNULL(s1.action_detail,'') <> '' "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND s2.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(s2.date_start) = " & Date.Now.Year
            End If
            sql &= " ) AS total"
            sql &= " FROM idp_external_req a INNER JOIN idp_trans_list b on a.idp_id = b.idp_id "

            sql &= " INNER JOIN idp_status_list e ON b.status_id = e.idp_status_id "
            sql &= " INNER JOIN idp_training_goal aa ON a.idp_id = aa.idp_id "

            sql &= " WHERE(b.status_id > 1 And ISNULL(b.is_delete, 0) = 0 And ISNULL(b.is_cancel, 0) = 0 And b.report_dept_id Is Not null) AND status_id IN (5,6,7) "
            sql &= " AND a.request_type = 'ext' AND ISNULL(aa.action_detail,'') <> '' "
            'sql &= " AND aa.expect_detail IN (SELECT expect_detail FROM idp_m_expect) OR a.expect_detail IN (SELECT expect_detail_en FROM idp_m_expect)  "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND b.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtempname.Text <> "" Then
                sql &= " AND (b.report_by LIKE '%" & txtempname.Text & "%' OR b.report_emp_code LIKE '%" & txtempname.Text & "%')"
            End If

            If txtjobtitle.SelectedValue <> "" Then
                sql &= " AND b.report_jobtitle LIKE '%" & txtjobtitle.Text & "%' "
            End If

            If txtjobtype.SelectedValue <> "" Then
                sql &= " AND b.report_jobtype LIKE '%" & txtjobtype.Text & "%' "
            End If

            If viewtype = "dept" Then
                sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM  user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            ' sql &= " GROUP BY b.report_dept_id , b.report_dept_name "
            'sql &= " ORDER BY  COUNT(total1) DESC "
            If viewtype = "" Then
                sql &= " AND b.report_emp_code = " & Session("emp_code").ToString
            End If

            sql &= " GROUP BY  aa.action_detail  "
            sql &= " ORDER BY ISNULL(COUNT(action_detail),0) DESC "
            'Response.Write(sql)
            'Return

            ds = conn.getDataSet(sql, "t1")

            GridActionDetail.DataSource = ds
            GridActionDetail.DataBind()

            If txtdate1.Text = "" Then
                lblActionDate1.Text = Date.Now.Year
                lblActionDate2.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            Else
                lblActionDate1.Text = txtdate1.Text
                lblActionDate2.Text = txtdate2.Text
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCCAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM user_dept WHERE 1 = 1  "
            sql &= " AND dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            sql &= " ORDER BY dept_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtdept.DataSource = ds
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTypeAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_type FROM user_profile WHERE 1 = 1 "
            ' If txtfind_jobtype.Text <> "" Then
            'sql &= " AND LOWER(job_type) LIKE '%" & txtfind_jobtype.Text.ToLower & "%' "
            'End If
            sql &= " GROUP BY job_type ORDER BY job_type"

            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtjobtype.DataSource = ds
            txtjobtype.DataBind()

            txtjobtype.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTitleAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_title FROM user_profile WHERE 1 = 1 "
            ' If txtfind_jobtype.Text <> "" Then
            'sql &= " AND LOWER(job_type) LIKE '%" & txtfind_jobtype.Text.ToLower & "%' "
            'End If
            sql &= " GROUP BY job_title ORDER BY job_title"

            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtjobtitle.DataSource = ds
            txtjobtitle.DataBind()

            txtjobtitle.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub Gridview1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview1.PageIndexChanging
        Gridview1.PageIndex = e.NewPageIndex
        bindReport1()
    End Sub

    Protected Sub Gridview1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblNum As Label = CType(e.Row.FindControl("lblNum"), Label)
            Dim lblTotal As Label = CType(e.Row.FindControl("lblTotal"), Label)
            Dim lblTotalRequest As Label = CType(e.Row.FindControl("lblTotalRequest"), Label)

            Dim txtexpense As Label = CType(e.Row.FindControl("txtexpense"), Label)
            Dim lblReq As Label = CType(e.Row.FindControl("lblReq"), Label)

            Dim lblBudget As Label = CType(e.Row.FindControl("lblBudget"), Label)
            Dim lblHour As Label = CType(e.Row.FindControl("lblHour"), Label)
            ' Dim lblTotal As Label = CType(e.Row.FindControl("lblTotal"), Label)
            Try
                lblNum.Text = FormatNumber((CDbl(lblNum.Text) / CDbl(lblTotal.Text)) * 100, 1) & "%"

                totalExpense += CDbl(txtexpense.Text)
                totalRequest += CDbl(lblTotalRequest.Text)
                totalBuget += CDbl(lblBudget.Text)
                totalHour += CDbl(lblHour.Text)
                totalApprove += CDbl(lblReq.Text)
            Catch ex As Exception
                lblNum.Text = "-"
            End Try
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(1).Text = totalRequest
            e.Row.Cells(2).Text = totalApprove
            e.Row.Cells(3).Text = FormatNumber(totalBuget)
            e.Row.Cells(4).Text = FormatNumber(totalExpense)
            e.Row.Cells(5).Text = FormatNumber(totalHour, 1)
        End If
    End Sub

    Protected Sub Gridview1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview1.SelectedIndexChanged

    End Sub

    Protected Sub txtselect_report_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtselect_report.SelectedIndexChanged
        txtempname.Text = ""
        txtjobtitle.SelectedIndex = 0
        txtjobtype.SelectedIndex = 0
        txtdept.SelectedIndex = 0

        txtinternal_topic.SelectedIndex = 0
        txtinternal_topic_person.SelectedIndex = 0

        search_date.Visible = True

        If txtselect_report.SelectedValue = "1" Then
            Panel_Report1.Visible = True
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = False

            bindReport1()

            If viewtype = "dept" Then
                panel_search_dept.Visible = False
            End If
        ElseIf txtselect_report.SelectedValue = "2" Then
            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = True
            'bindReport2()
            bindReportExternalSummaryByCategory()

            If viewtype = "dept" Then
                panel_search_dept.Visible = False
            End If
        ElseIf txtselect_report.SelectedValue = "3" Then ' Action After Training
            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = True
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = False
            bindReport3()
        ElseIf txtselect_report.SelectedValue = "4" Then ' Individual report Ext and Int
            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = True
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False

            panel_internal_history.Visible = True
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = False
            bindGridIndividualList() ' External

            bindReportInternalHistory() ' Internal

        ElseIf txtselect_report.SelectedValue = "5" Then

            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = True
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = False
            bindGridDeptIndividualList()

        ElseIf txtselect_report.SelectedValue = "7" Then 'Internal Training Summary by Department

            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = True
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = False
            bindInternalCombo()
            bindReportInternalSummary()
            If viewtype = "dept" Then
                panel_search_dept.Visible = False
            End If
        ElseIf txtselect_report.SelectedValue = "6" Then 'Internal Training History

            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = True
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = False
            bindReportInternalHistory()
        ElseIf txtselect_report.SelectedValue = "8" Then 'Expect outcome External

            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = True
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = False
            bindGridExpectAndExpense()
            bindGridExpectAndExpenseNew()
        ElseIf txtselect_report.SelectedValue = "9" Then 'Internal outcome External

            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = True
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = False
            bindGridExpectAndExpenseInternal()
        ElseIf txtselect_report.SelectedValue = "10" Then 'External Action Detail

            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = True
            panelDynamic.Visible = False
            bindGridActionDetail()
        ElseIf txtselect_report.SelectedValue = "11" Then 'Internal Training Summary by request
            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = True
            bindReportInternalSummaryByRequest()
        ElseIf txtselect_report.SelectedValue = "12" Then 'Internal Training Summary by category
            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = True
            bindReportInternalSummaryByCategory()
        ElseIf txtselect_report.SelectedValue = "13" Then 'Internal Training Summary by category
            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
            panelDynamic.Visible = True
            search_date.Visible = False
            bindReportExternalBudgetSummary()
        Else
            Panel_Report1.Visible = False
            Panel_Motivation.Visible = False
            panel_request.Visible = False
            panel_individual.Visible = False
            panel_dept_individual.Visible = False
            panel_internal_summary.Visible = False
            panel_internal_history.Visible = False
            panel_external_expect.Visible = False
            panel_internal_expect.Visible = False
            panel_external_actiondetail.Visible = False
        End If
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

        search_date.Visible = True


        bindReport1()
        'bindReport2()
        bindReport3()
        bindInternalCombo()
        ' bindInternalCombo()
        bindReportInternalSummary()

        bindReportInternalHistory()
        bindGridExpectAndExpense()
        bindGridExpectAndExpenseNew()
        bindGridExpectAndExpenseInternal()
        bindGridActionDetail()



        If txtselect_report.SelectedValue = "11" Then
            bindReportInternalSummaryByRequest()
        ElseIf txtselect_report.SelectedValue = "2" Then
            bindReportExternalSummaryByCategory()
        ElseIf txtselect_report.SelectedValue = "12" Then
            bindReportInternalSummaryByCategory()

        ElseIf txtselect_report.SelectedValue = "13" Then
            panelDynamic.Visible = True
            search_date.Visible = False
            bindReportExternalBudgetSummary()
        End If



        If txtselect_report.SelectedValue = "101" Then
            exportExcelExternalBudget()
        ElseIf txtselect_report.SelectedValue = "102" Then
            exportExcelExternalExpense()
        ElseIf txtselect_report.SelectedValue = "103" Then
            exportExcelInternalBudget()
        ElseIf txtselect_report.SelectedValue = "104" Then
            exportExcelInternalExpense()
        End If
    End Sub

    Protected Sub Gridview3_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview3.PageIndexChanging
        Gridview3.PageIndex = e.NewPageIndex
        bindReport3()
    End Sub

    Protected Sub Gridview3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblActionComplete As Label = CType(e.Row.FindControl("lblActionComplete"), Label)
            Dim lblFinish As Label = CType(e.Row.FindControl("lblFinish"), Label)

            Dim lblActionPercent As Label = CType(e.Row.FindControl("lblActionPercent"), Label)


            Try
                If CDbl(lblFinish.Text) > 0 Then
                    lblActionPercent.Text = FormatNumber((CDbl(lblActionComplete.Text) / CDbl(lblFinish.Text)) * 100, 1) & "%"
                End If



            Catch ex As Exception
                lblActionPercent.Text = "-"
            End Try
        End If
    End Sub

    Protected Sub Gridview3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview3.SelectedIndexChanged

    End Sub

    Protected Sub GridviewActionList_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridviewActionList.PageIndexChanging
        GridviewActionList.PageIndex = e.NewPageIndex
        bindGridActionList()
    End Sub

    Protected Sub GridviewActionList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridviewActionList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblTAT As Label = CType(e.Row.FindControl("lblTAT"), Label)
            Dim lblDateTS As Label = CType(e.Row.FindControl("lblDateTS"), Label)

            If CLng(lblDateTS.Text) > 0 Then
                lblTAT.Text = MinuteDiff(lblDateTS.Text, Date.Now.Ticks.ToString)
                '  lblCloseDate.Visible = False
                ' Response.Write(CLng(lblDateTS.Text) & "<br/>")
            Else
                lblTAT.Text = ""
            End If
        End If
    End Sub

    Protected Sub GridviewActionList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridviewActionList.SelectedIndexChanged

    End Sub

    Protected Sub GridIndividual_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridIndividual.PageIndexChanging
        GridIndividual.PageIndex = e.NewPageIndex
        bindGridIndividualList()
    End Sub

    Protected Sub GridIndividual_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridIndividual.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblAction As Label = CType(e.Row.FindControl("lblAction"), Label)
        
            Try
                If lblAction.Text = "Complete" Then
                    lblAction.Text = "<span style='color:green'>Complete</span>"
                Else
                    lblAction.Text = "<span style='color:red'>Waiting</span>"
                End If

            Catch ex As Exception
                'lblActionPercent.Text = "-"
            End Try
        End If
    End Sub

    Protected Sub GridIndividual_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridIndividual.SelectedIndexChanged

    End Sub

    Protected Sub GridDeptIndividual_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridDeptIndividual.PageIndexChanging
        GridDeptIndividual.PageIndex = e.NewPageIndex
        bindGridDeptIndividualList()
    End Sub

    Protected Sub GridDeptIndividual_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridDeptIndividual.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblAction As Label = CType(e.Row.FindControl("lblAction"), Label)

            Try
                If lblAction.Text = "Complete" Then
                    lblAction.Text = "<span style='color:green'>Complete</span>"
                Else
                    lblAction.Text = "<span style='color:red'>Waiting</span>"
                End If

            Catch ex As Exception
                'lblActionPercent.Text = "-"
            End Try
        End If
    End Sub

    Protected Sub GridDeptIndividual_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridDeptIndividual.SelectedIndexChanged

    End Sub

    Protected Sub Gridview5_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview5.PageIndexChanging
        Gridview5.PageIndex = e.NewPageIndex
        bindReportInternalHistory()
    End Sub

    Protected Sub Gridview5_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview5.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblHour As Label = CType(e.Row.FindControl("lblHour"), Label)
            Dim lblStart As Label = CType(e.Row.FindControl("lblStart"), Label)
            Dim lblEnd As Label = CType(e.Row.FindControl("lblEnd"), Label)
            Dim lblEvaluate As Label = CType(e.Row.FindControl("lblEvaluate"), Label)
            Dim lblDateAttend As Label = CType(e.Row.FindControl("lblDateAttend"), Label)
            Dim lblRegisterTime As Label = CType(e.Row.FindControl("lblRegisterTime"), Label)
            Dim lblIsRegister As Label = CType(e.Row.FindControl("lblIsRegister"), Label)
            Dim register_time As Long = 0

            Dim date_format1 As New Date(lblStart.Text)
            Dim date_format2 As New Date(lblEnd.Text)

            Dim diff_min As Long = DateDiff(DateInterval.Hour, date_format1, date_format2)

            ' lblHour.Text = diff_min

            If lblEvaluate.Text = "Yes" Then
                lblEvaluate.ForeColor = Drawing.Color.Green
            Else
                lblEvaluate.ForeColor = Drawing.Color.Red
            End If

            If Long.TryParse(lblRegisterTime.Text, register_time) Then
                If register_time > 0 Then
                    lblDateAttend.Text = ConvertTSToDateString(lblRegisterTime.Text) & " " & ConvertTSTo(lblRegisterTime.Text, "hour") & ":" & ConvertTSTo(lblRegisterTime.Text, "min").PadLeft(2, "0")
                Else
                    lblDateAttend.Text = "-"
                End If
            End If

            If lblIsRegister.Text = "1" Then
                lblDateAttend.Text = "Yes - " & lblDateAttend.Text
            Else
                lblDateAttend.Text = "<span style='color:red'>Pending</span>"
            End If
        End If
    End Sub

    Protected Sub Gridview5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview5.SelectedIndexChanged

    End Sub

    Protected Sub Gridview4_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview4.PageIndexChanging
        Gridview4.PageIndex = e.NewPageIndex
        bindReportInternalSummary()
    End Sub

    Protected Sub Gridview4_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview4.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblDateStart As Label = CType(e.Row.FindControl("lblDateStart"), Label)
            Dim lblDateEnd As Label = CType(e.Row.FindControl("lblDateEnd"), Label)
            Dim lblID As Label = CType(e.Row.FindControl("lblID"), Label)

            Dim lblTargetNum As Label = CType(e.Row.FindControl("lblTargetNum"), Label)
            Dim lblTargetAttend As Label = CType(e.Row.FindControl("lblTargetAttend"), Label)
            Dim lblPercent As Label = CType(e.Row.FindControl("lblPercent"), Label)

            Dim targetNum As Integer = 0
            Dim targetAttend As Integer = 0

            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT MIN(schedule_start_ts) AS start_date , MAX(schedule_end_ts) AS end_date FROM idp_training_schedule WHERE idp_id = " & lblID.Text
                sql &= " AND schedule_start_ts > 0  GROUP BY idp_id"
                ds = conn.getDataSetForTransaction(sql, "t1")
                lblDateStart.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("start_date").ToString)
                lblDateEnd.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("end_date").ToString)

                Integer.TryParse(lblTargetNum.Text, targetNum)
                If targetNum > 0 Then
                    Integer.TryParse(lblTargetAttend.Text, targetAttend)
                    If targetAttend > targetNum Then
                        lblPercent.Text = "100 %"
                    Else
                        lblPercent.Text = FormatNumber((targetAttend / targetNum) * 100, 0) & " %"
                    End If

                Else
                    lblPercent.Text = "-"
                End If
            Catch ex As Exception
                Response.Write(ex.Message & sql)

            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub Gridview4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview4.SelectedIndexChanged

    End Sub

    Protected Sub txtinternal_topic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtinternal_topic.SelectedIndexChanged

    End Sub

    Protected Sub txtinternal_topic_person_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtinternal_topic_person.SelectedIndexChanged
        bindReportInternalHistory()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As System.EventArgs) Handles cmdClear.Click
        txtselect_report.SelectedIndex = 0
        txtdate1.Text = ""
        txtdate2.Text = ""
        txtdept.SelectedIndex = 0
        txtempname.Text = ""
        txtjobtitle.SelectedIndex = 0
        txtjobtype.SelectedIndex = 0

        Panel_Report1.Visible = False
        Panel_Motivation.Visible = False
        panel_request.Visible = False
        panel_individual.Visible = False
        panel_dept_individual.Visible = False
        panel_internal_summary.Visible = False
        panel_internal_history.Visible = False
        panel_external_actiondetail.Visible = False
        panel_internal_expect.Visible = False

    End Sub

    Protected Sub GridActionDetail_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridActionDetail.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblNum As Label = CType(e.Row.FindControl("lblNum"), Label)
            Dim lblTotal As Label = CType(e.Row.FindControl("lblTotal"), Label)
            Dim lblPercent As Label = CType(e.Row.FindControl("lblPercent"), Label)

            Try
                lblPercent.Text = FormatNumber((CDbl(lblNum.Text) / CDbl(lblTotal.Text)) * 100)
            Catch ex As Exception
                lblPercent.Text = "-"
            End Try
        End If
    End Sub

    Protected Sub GridActionDetail_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridActionDetail.SelectedIndexChanged

    End Sub

    Public Sub ExportToSpreadsheet(table As DataTable, name As String)
        Dim context As HttpContext = HttpContext.Current
        context.Response.Clear()

        For Each column As DataColumn In table.Columns
            context.Response.Write(column.ColumnName + ";")
        Next

        context.Response.Write(Environment.NewLine)

        For Each row As DataRow In table.Rows
            For i As Integer = 0 To table.Columns.Count - 1
                context.Response.Write(row(i).ToString().Replace(";", String.Empty) + ";")
            Next
            context.Response.Write(Environment.NewLine)
        Next

        context.Response.ContentType = "text/csv"
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + ".xls")
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode
        HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())
        context.Response.[End]()
    End Sub


    Sub exportExcelExternalBudget()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select d.category_name_en AS category,  ext_title , ext_type_name , training_type_name , expect_detail , facility , institution , place , chk_ext_expect1 , chk_ext_expect2, chk_ext_expect3 , chk_ext_expect4 , chk_ext_expect5 , chk_ext_expect6 "
            sql &= " ,date_start , date_end , train_hour , a.report_by , a.report_dept_id , a.report_dept_name , a.report_emp_code , a.report_jobtype , a.report_jobtitle "
            sql &= " , c.expense_topic_name , expense_value , currency_type_name , exchange_rate , case when is_sponsor = 1 then 'yes' else 'no' end as sponsor , c.expense_remark"
            sql &= " , expense_request_type_name , accouting_type , c.create_by as 'budget request by' , b.budget_update_by AS 'approve by' , b.budget_remark "

            sql &= " from idp_trans_list a inner join idp_external_req b on a.idp_id = b.idp_id"
            sql &= " inner join idp_training_expense c on a.idp_id = c.idp_id"
            sql &= " LEFT OUTER JOIN idp_m_category d ON b.ext_category_id = d.category_id "
            sql &= " where ISNULL(a.is_delete,0) = 0 and ISNULL(a.is_cancel,0) = 0 and b.request_type = 'ext'"
            sql &= " and c.accouting_type = 1 and ISNULL(c.is_delete,0) = 0 and a.status_id in (5,6,7)"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= "  AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            ' ExportToSpreadsheet(ds.Tables("t1"), "test")

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.Charset = "Windows-874"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Me.EnableViewState = False
            Response.AddHeader("content-disposition", "attachment;filename=Budget.xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            Dim gv As New GridView()
            gv.DataSource = ds.Tables(0)
            gv.DataBind()
            gv.RenderControl(hw)
            Response.Output.Write(sw)
            Response.Flush()
            Response.[End]()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub exportExcelExternalExpense()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select d.category_name_en AS category,  ext_title , ext_type_name , training_type_name , expect_detail , facility , institution , place , chk_ext_expect1 , chk_ext_expect2, chk_ext_expect3 , chk_ext_expect4 , chk_ext_expect5 , chk_ext_expect6"
            sql &= " ,date_start , date_end , train_hour , a.report_by , a.report_dept_id , a.report_dept_name , a.report_emp_code , a.report_jobtype , a.report_jobtitle"
            sql &= " , c.expense_topic_name , expense_value , currency_type_name , exchange_rate ,  case when is_sponsor = 1 then 'yes' else 'no' end as sponsor , c.expense_remark"
            sql &= " , expense_request_type_name , accouting_type , c.create_by as 'expense request by' , c.acc_receive_by AS 'receive by' "

            sql &= " from idp_trans_list a inner join idp_external_req b on a.idp_id = b.idp_id"
            sql &= " inner join idp_training_expense c on a.idp_id = c.idp_id"
            sql &= " LEFT OUTER JOIN idp_m_category d ON b.ext_category_id = d.category_id "
            sql &= " where ISNULL(a.is_delete,0) = 0 and ISNULL(a.is_cancel,0) = 0 and b.request_type = 'ext'"
            sql &= " and c.accouting_type = 2 and ISNULL(c.is_delete,0) = 0 and a.status_id in (5,6,7) AND ISNULL(acc_receive_by,'') <> ''"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= "  AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            ' ExportToSpreadsheet(ds.Tables("t1"), "test")

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.Charset = "Windows-874"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Me.EnableViewState = False
            Response.AddHeader("content-disposition", "attachment;filename=ActualExpense.xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            Dim gv As New GridView()
            gv.DataSource = ds.Tables(0)
            gv.DataBind()
            gv.RenderControl(hw)
            Response.Output.Write(sw)
            Response.Flush()
            Response.[End]()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub exportExcelInternalBudget()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select CONVERT(VARCHAR(10),a.date_submit,103) AS 'Submit Date' ,  CONVERT(VARCHAR(10),schedule_start,103) AS schedule_start , CONVERT(VARCHAR(10),schedule_end,103) AS schedule_end , d.category_name_en AS 'Category' ,  b.internal_title "
            sql &= " , a.report_by , a.report_dept_id , a.report_dept_name , a.report_emp_code , a.report_jobtype"
            sql &= " , c.expense_topic_name , expense_value , currency_type_name , exchange_rate , case when is_sponsor = 1 then 'yes' else 'no' end as sponsor , c.expense_remark"
            sql &= " , expense_request_type_name , accouting_type , c.create_by as 'budget request by' , b.budget_update_by AS 'approve by' , b.budget_remark "
            sql &= " from idp_trans_list a inner join idp_external_req b on a.idp_id = b.idp_id"
            sql &= " inner join idp_training_expense c on a.idp_id = c.idp_id"
            ' sql &= " LEFT OUTER JOIN idp_m_category d ON b.ext_category_id = d.category_id "
            sql &= " LEFT OUTER JOIN (SELECT a1.idp_id , MAX(a2.category_name_en) AS category_name_en FROM idp_training_relate_idp a1 INNER JOIN idp_m_category a2 ON a1.category_id = a2.category_id GROUP BY a1.idp_id) d ON d.idp_id = a.idp_id "

            sql &= " LEFT OUTER JOIN (SELECT a1.idp_id , MIN(a1.schedule_start) AS schedule_start , MAX(a1.schedule_end) AS schedule_end FROM idp_training_schedule a1  GROUP BY a1.idp_id) e ON e.idp_id = a.idp_id "

            sql &= " where ISNULL(a.is_delete,0) = 0 and ISNULL(a.is_cancel,0) = 0 and b.request_type = 'int'"
            sql &= " and c.accouting_type = 1 and ISNULL(c.is_delete,0) = 0  and a.status_id in (5,6,7)"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
                ' sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            Else
                '  sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= "  AND a.report_dept_id = " & txtdept.SelectedValue
            End If
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            ' ExportToSpreadsheet(ds.Tables("t1"), "test")

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.Charset = "Windows-874"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Me.EnableViewState = False
            Response.AddHeader("content-disposition", "attachment;filename=Budget2.xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            Dim gv As New GridView()
            gv.DataSource = ds.Tables(0)
            gv.DataBind()
            gv.RenderControl(hw)
            Response.Output.Write(sw)
            Response.Flush()
            Response.[End]()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub exportExcelInternalExpense()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select CONVERT(VARCHAR(10),a.date_submit,103) AS 'Submit Date' ,  CONVERT(VARCHAR(10),schedule_start,103) AS schedule_start , CONVERT(VARCHAR(10),schedule_end,103) AS schedule_end , d.category_name_en AS 'Category' ,  b.internal_title "
            sql &= " , a.report_by , a.report_dept_id , a.report_dept_name , a.report_emp_code , a.report_jobtype"
            sql &= " , c.expense_topic_name , expense_value , currency_type_name , exchange_rate , case when is_sponsor = 1 then 'yes' else 'no' end as sponsor , c.expense_remark"
            sql &= " , expense_request_type_name , accouting_type , c.create_by as 'budget request by' , b.budget_update_by AS 'approve by' , b.budget_remark  , c.acc_receive_by AS 'receive by' "
            sql &= " from idp_trans_list a inner join idp_external_req b on a.idp_id = b.idp_id"
            sql &= " inner join idp_training_expense c on a.idp_id = c.idp_id"
            sql &= " LEFT OUTER JOIN (SELECT a1.idp_id , MAX(a2.category_name_en) AS category_name_en FROM idp_training_relate_idp a1 INNER JOIN idp_m_category a2 ON a1.category_id = a2.category_id GROUP BY a1.idp_id) d ON d.idp_id = a.idp_id "

            sql &= " LEFT OUTER JOIN (SELECT a1.idp_id , MIN(a1.schedule_start) AS schedule_start , MAX(a1.schedule_end) AS schedule_end FROM idp_training_schedule a1  GROUP BY a1.idp_id) e ON e.idp_id = a.idp_id "

            sql &= " where ISNULL(a.is_delete,0) = 0 and ISNULL(a.is_cancel,0) = 0 and b.request_type = 'int'"
            sql &= " and c.accouting_type = 2 and ISNULL(c.is_delete,0) = 0   and a.status_id in (5,6,7)  AND ISNULL(acc_receive_by,'') <> ''"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                '  sql &= " AND b.date_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
                sql &= " AND a.idp_id IN (SELECT idp_id FROM idp_training_schedule WHERE schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59) & " AND ISNULL(is_delete,0) = 0 ) "
            Else
                '  sql &= " AND YEAR(b.date_start) = " & Date.Now.Year
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= "  AND a.report_dept_id = " & txtdept.SelectedValue
            End If
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            ' ExportToSpreadsheet(ds.Tables("t1"), "test")

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.Charset = "Windows-874"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Me.EnableViewState = False
            Response.AddHeader("content-disposition", "attachment;filename=Expense2.xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            Dim gv As New GridView()
            gv.DataSource = ds.Tables(0)
            gv.DataBind()
            gv.RenderControl(hw)
            Response.Output.Write(sw)
            Response.Flush()
            Response.[End]()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReportExternalBudgetSummary()
        Dim sql As String
        Dim ds As New DataSet

        Try

            sql = "select d.dept_id as 'Cost center' , d.dept_name as 'Department Name' , CONVERT(varchar, CAST(isnull(max(d.budget_amount),0) AS money), 1)  as 'Budget Allocated' , CONVERT(varchar, CAST(isnull(max(e.budget_request),0) AS money), 1)  as 'Total Budget requested'  "
            sql &= "    , CONVERT(varchar, CAST((isnull(max(d.budget_amount),0) - (isnull(max(e.budget_request),0) )) AS money), 1)  as 'Budget Balance' "
            sql &= " from idp_trans_list b "
            sql &= " inner join idp_external_req c ON b.idp_id = c.idp_id and c.request_type = 'ext'"
            sql &= " left outer join idp_training_budget_list d on b.report_dept_id = d.dept_id"

            sql &= " left outer join (" ' request
            sql &= " select sum(case when currency_type_id = 1 then expense_value else (expense_value*exchange_rate) end) as budget_request , t2.report_dept_id from idp_training_expense t1 "
            sql &= " inner join idp_trans_list t2 on t1.idp_id = t2.idp_id"
            sql &= " inner join idp_external_req t3 on t2.idp_id = t3.idp_id and t3.request_type = 'ext' "
            sql &= " where accouting_type = 1 and year(t2.date_submit) = " & Date.Now.Year & " and ISNULL(t1.is_delete,0) = 0  and t2.status_id > 1  and t1.expense_request_type_id in (select request_id from idp_budget_request where is_request_budget = 1)"
            sql &= " group by t2.report_dept_id "
            sql &= " ) e on b.report_dept_id = e.report_dept_id"

           

            sql &= " where ISNULL(b.is_cancel, 0) = 0 And ISNULL(b.is_delete, 0) = 0 And ISNULL(b.is_ladder, 0) = 0 and b.status_id > 1 and d.year_budget = " & Date.Now.Year
            ' sql &= " and d.dept_id = " & lblCostcenter.Text
            If txtdept.SelectedIndex > 0 Then
                sql &= " and d.dept_id = " & txtdept.SelectedValue.ToString()
            End If

            sql &= " group by d.dept_id , d.dept_name"


            '   Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            'lblIntDate1.Text = txtdate1.Text
            ' lblIntDate2.Text = txtdate2.Text
            lblNumInternal1.Text = ds.Tables("t1").Rows.Count

            GridViewDynamic.DataSource = ds
            GridViewDynamic.DataBind()



        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdInterSummarySearch_Click(sender As Object, e As System.EventArgs) Handles cmdInterSummarySearch.Click
        bindReportInternalSummary()
    End Sub



    Protected Sub cmdExcelInternal1_Click(sender As Object, e As System.EventArgs) Handles cmdExcelInternal1.Click
        bindReportInternalHistory("excel")
    End Sub

    Protected Sub cmdExternalExcel_Click(sender As Object, e As EventArgs) Handles cmdExternalExcel.Click
        bindReport1("excel")
    End Sub

    Protected Sub cmdExtPersonExcel_Click(sender As Object, e As EventArgs) Handles cmdExtPersonExcel.Click
        bindGridIndividualList("excel")
    End Sub

    Protected Sub cmdExtPersonExcel0_Click(sender As Object, e As EventArgs) Handles cmdExtPersonExcel0.Click
        bindGridDeptIndividualList("excel")
    End Sub
End Class
