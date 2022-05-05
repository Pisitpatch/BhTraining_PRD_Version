Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Globalization

Partial Class incident_incident_physician_defendant
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected cur_date As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        Session("viewtype") = "ha"

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' First time load
            If Year(Date.Now) > 2500 Then
                cur_date = (Month(Date.Now)) & "|" & (Year(Date.Now) - 543)
            Else
                cur_date = (Month(Date.Now)) & "|" & Year(Date.Now)
            End If

            ' Response.Write(cur_date)
            bindDateRange()
           
            Try
                txtdate1.SelectedValue = cur_date
                txtdate2.SelectedValue = cur_date
            Catch ex As Exception
                Response.Write(cur_date)
            End Try

            bindDay()
            bindWeek()

        End If

        If txtselect_report.SelectedValue = "1" Then
            Panel_Doctor_Defendant.Visible = True
            Panel_Unit.Visible = False
            panel_specialty.Visible = False
            panel_relevant.Visible = False
            bindGrid()
        ElseIf txtselect_report.SelectedValue = "2" Then
            Panel_Doctor_Defendant.Visible = False
            Panel_Unit.Visible = True
            panel_specialty.Visible = False
            panel_relevant.Visible = False
            bindGrid2()
        ElseIf txtselect_report.SelectedValue = "3" Then
            Panel_Doctor_Defendant.Visible = False
            Panel_Unit.Visible = False
            panel_specialty.Visible = True
            panel_relevant.Visible = False
            bindGrid3()
        ElseIf txtselect_report.SelectedValue = "4" Then
            Panel_Doctor_Defendant.Visible = False
            Panel_Unit.Visible = False
            panel_specialty.Visible = False
            panel_relevant.Visible = True
            bindGrid4()
        Else
            Panel_Doctor_Defendant.Visible = False
            Panel_Unit.Visible = False
            panel_specialty.Visible = False
            panel_relevant.Visible = False
        End If

        lblDate1.Text = txtdate1.SelectedItem.Text
        lblDate2.Text = txtdate2.SelectedItem.Text
        lblUnitDate1.Text = txtdate1.SelectedItem.Text
        lblUnitDate2.Text = txtdate2.SelectedItem.Text
        lblSpecialDate1.Text = txtdate1.SelectedItem.Text
        lblSpecialDate2.Text = txtdate2.SelectedItem.Text
        lblRelevantDate1.Text = txtdate1.SelectedItem.Text
        lblRelevantDate2.Text = txtdate2.SelectedItem.Text
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

    Sub bindDateRange()
        Dim sql As String
        Dim ds As New DataSet
        Dim myvalue As String
        Dim mytext As String
        Try
            sql = "SELECT  MONTH(date_submit) AS m , YEAR(date_submit) AS y FROM ir_trans_list WHERE ISNULL(is_delete,0) = 0 AND ISNULL(is_cancel,0) = 0 AND ISNULL(date_submit,0) > 0 "
            sql &= " GROUP BY MONTH(date_submit) , YEAR(date_submit)"
            sql &= " ORDER BY MONTH(date_submit) , YEAR(date_submit) ASC"
            ds = conn.getDataSet(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                myvalue = ds.Tables("t1").Rows(i)("m").ToString & "|" & ds.Tables("t1").Rows(i)("y").ToString
                mytext = getMonthName(CInt(ds.Tables("t1").Rows(i)("m").ToString)) & " " & ds.Tables("t1").Rows(i)("y").ToString
                txtdate1.Items.Add(New ListItem(mytext, myvalue))
                txtdate2.Items.Add(New ListItem(mytext, myvalue))
            Next i

         

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindWeek()
        Dim date1 As Date
        Dim last_day_inmonth As Date
        Dim start_month As Array
        Dim date1_str As String

        Dim sql As String
        Dim ds As New DataSet
        Try
            start_month = txtdate1.SelectedValue.Split("|")
            date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

            last_day_inmonth = date1.AddMonths(1).AddDays(-1)
            date1 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)
            date1_str = date1.Year & "-" & date1.Month & "-" & date1.Day
            'Response.Write(GetWeekRows(CInt(start_month(1)), CInt(start_month(0))))
            sql = "SELECT DATEDIFF(week, DATEADD(MONTH, DATEDIFF(MONTH, 0, '" & date1_str & "'), 0),'" & date1_str & "') +1"
            ds = conn.getDataSet(sql, "t1")

            txtweek.Items.Clear()
            txtweek.Items.Add(New ListItem("All", ""))
            For i As Integer = 1 To CInt(ds.Tables("t1").Rows(0)(0).ToString)
                txtweek.Items.Add(New ListItem("wk " & i.ToString, i.ToString))
            Next
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Clear()
        End Try

    End Sub

    Sub bindDay()
        Dim date1 As Date
        Dim first_day_inmonth As Date
        Dim last_day_inmonth As Date
        Dim start_month As Array
        Dim date1_str As String

        Dim firstday_inweek As Date
        Dim lastday_inweek As Date
        Dim loop_date As Date

        Dim sql As String
        Dim ds As New DataSet
        '  Dim dinfo As New DateTimeFormatInfo()
        Try
            start_month = txtdate1.SelectedValue.Split("|")
            date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)
            first_day_inmonth = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

            last_day_inmonth = date1.AddMonths(1).AddDays(-1)
            date1 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)

            txtday.Items.Clear()
            txtday.Items.Add(New ListItem("All", ""))
            If txtweek.SelectedValue = "" Then
                txtday.Visible = True
                For i As Integer = 1 To date1.Day
                    txtday.Items.Add(New ListItem(i.ToString & " , " & WeekdayName(Weekday(first_day_inmonth), False, Microsoft.VisualBasic.FirstDayOfWeek.Sunday), i.ToString))
                    first_day_inmonth = DateAdd(DateInterval.Day, 1, first_day_inmonth)

                Next i
            Else ' ถ้าเลือก week
                ' firstday_inweek = DateAdd("d", 0 - first_day_inmonth.DayOfWeek, first_day_inmonth)
                'lastday_inweek = DateAdd("d", 6 - first_day_inmonth.DayOfWeek, first_day_inmonth)
                'txtday.Visible = False
                loop_date = first_day_inmonth
                For i As Integer = 1 To date1.Day

                    ' first_day_inmonth = DateAdd(DateInterval.Day, 1, first_day_inmonth)

                    '   If DateDiff(DateInterval.Weekday, DateAdd(DateInterval.Month, DateDiff(DateInterval.Month, Date.MinValue, first_day_inmonth), 0), first_day_inmonth) + 1 = 1 Then

                    'txtday.Items.Add(New ListItem(i.ToString & " , " & WeekdayName(Weekday(first_day_inmonth), False, Microsoft.VisualBasic.FirstDayOfWeek.Sunday), i.ToString))
                    'End If
                    ' Response.Write(DatePart(DateInterval.WeekOfYear, loop_date, Microsoft.VisualBasic.FirstDayOfWeek.Monday))
                    '  Response.Write(loop_date)
                    '  Response.Write(":")
                    ' Response.Write(DatePart(DateInterval.WeekOfYear, first_day_inmonth, Microsoft.VisualBasic.FirstDayOfWeek.Monday) + (CInt(txtweek.SelectedValue) - 1))
                   ' Response.Write("<hr/>")


                    If (DatePart(DateInterval.WeekOfYear, loop_date, Microsoft.VisualBasic.FirstDayOfWeek.Sunday) = DatePart(DateInterval.WeekOfYear, first_day_inmonth, Microsoft.VisualBasic.FirstDayOfWeek.Sunday) + (CInt(txtweek.SelectedValue) - 1)) Then
                        txtday.Items.Add(New ListItem(i.ToString & " , " & WeekdayName(Weekday(loop_date), False, Microsoft.VisualBasic.FirstDayOfWeek.Sunday), i.ToString))
                    End If

                    loop_date = DateAdd(DateInterval.Day, 1, loop_date)
                Next i
            End If

            '  Response.Write(DatePart(DateInterval.WeekOfYear, date1, Microsoft.VisualBasic.FirstDayOfWeek.Monday))
            ' Response.Write(Weekday(Date.Now, Microsoft.VisualBasic.FirstDayOfWeek.Monday))
            ' Response.Write(DateDiff(DateInterval.WeekOfYear, DateAdd(DateInterval.Month, DateDiff(DateInterval.Month, Date.MinValue, Date.Now), 0), Date.Now) + 1)
            ' Dim c As Date = DateAdd(DateInterval.Month, DateDiff(DateInterval.Month, 0, Date.Now), Date.Now)
            '   Response.Write(DateDiff(DateInterval.Month, Date.MinValue, Date.Now))

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Clear()
        End Try

    End Sub

   

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Dim date1 As Date
        Dim date2 As Date
        Dim start_month As Array
        Dim last_month As Array
        Dim last_day_inmonth As Date

        Dim cur_date As Date
        ' Dim date_string1 As String
        '  Dim date_string2 As String
        Try
            start_month = txtdate1.SelectedValue.Split("|")
            date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

            last_month = txtdate2.SelectedValue.Split("|")
            date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

            last_day_inmonth = date2.AddMonths(1).AddDays(-1)
            date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


            sql = "SELECT COUNT(c.md_code) AS num , d.emp_no  , d.specialty  , d.doctor_name_en FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN ir_doctor_defendant c ON a.ir_id = c.ir_id"
            sql &= " INNER JOIN m_doctor d ON c.md_code = d.emp_no"
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 2 "

            ' If txtweek.SelectedValue = "" And txtday.SelectedValue = "" Then
            sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
            ' End If

            If txtweek.Visible = True And txtweek.SelectedValue <> "" Then
                sql &= " AND (DATEDIFF(week, DATEADD(MONTH, DATEDIFF(MONTH, 0, date_submit), 0),date_submit) +1) =  " & txtweek.SelectedValue
            End If

            If txtday.Visible = True And txtday.SelectedValue <> "" Then
                cur_date = New Date(CInt(start_month(1)), CInt(start_month(0)), txtday.SelectedValue)
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(cur_date.Ticks) & "' AND '" & ConvertTSToSQLDateTime(cur_date.Ticks, "23", "59") & "' "
            End If

            sql &= " GROUP BY d.emp_no , d.specialty , d.doctor_name_en"
            sql &= " ORDER BY d.doctor_name_en"
            '   Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")

            Gridview1.DataSource = ds
            Gridview1.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
            'Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindGrid2() ' Unit Defendant
        Dim sql As String
        Dim ds As New DataSet
        Dim date1 As Date
        Dim date2 As Date

        Dim start_month As Array
        Dim last_month As Array
        Dim last_day_inmonth As Date
        Dim cur_date As Date
        ' Dim date_string1 As String
        '  Dim date_string2 As String
        Try
            start_month = txtdate1.SelectedValue.Split("|")
            date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

            last_month = txtdate2.SelectedValue.Split("|")
            date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

            last_day_inmonth = date2.AddMonths(1).AddDays(-1)
            date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


            sql = "SELECT COUNT(c.dept_unit_name) AS num , c.dept_unit_id , c.dept_unit_name   FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN ir_cfb_unit_defendant c ON a.ir_id = c.ir_id"
            sql &= " INNER JOIN ir_status_list d ON a.status_id = d.ir_status_id"
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "

            sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
            If txtweek.Visible = True And txtweek.SelectedValue <> "" Then
                sql &= " AND (DATEDIFF(week, DATEADD(MONTH, DATEDIFF(MONTH, 0, date_submit), 0),date_submit) +1) =  " & txtweek.SelectedValue
            End If
            If txtday.Visible = True And txtday.SelectedValue <> "" Then
                cur_date = New Date(CInt(start_month(1)), CInt(start_month(0)), txtday.SelectedValue)
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(cur_date.Ticks) & "' AND '" & ConvertTSToSQLDateTime(cur_date.Ticks, "23", "59") & "' "
            End If

            sql &= " GROUP BY c.dept_unit_id , c.dept_unit_name "
            sql &= " ORDER BY  c.dept_unit_name"
            '  Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")

            Gridview2.DataSource = ds
            Gridview2.DataBind()

            lblNum2.Text = ds.Tables("t1").Rows.Count
            '  Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindGrid3() ' Specialty
        Dim sql As String
        Dim ds As New DataSet
        Dim date1 As Date
        Dim date2 As Date

        Dim start_month As Array
        Dim last_month As Array
        Dim last_day_inmonth As Date
        Dim cur_date As Date
        ' Dim date_string1 As String
        '  Dim date_string2 As String
        Try
            start_month = txtdate1.SelectedValue.Split("|")
            date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

            last_month = txtdate2.SelectedValue.Split("|")
            date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

            last_day_inmonth = date2.AddMonths(1).AddDays(-1)
            date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


            sql = "SELECT COUNT(d.specialty) AS num ,  d.specialty FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN ir_doctor_defendant c ON a.ir_id = c.ir_id"
            sql &= " INNER JOIN m_doctor d ON c.md_code = d.emp_no"
            sql &= " INNER JOIN ir_status_list e ON a.status_id = e.ir_status_id"
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND d.specialty IS NOT NULL "

            sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
            If txtweek.Visible = True And txtweek.SelectedValue <> "" Then
                sql &= " AND (DATEDIFF(week, DATEADD(MONTH, DATEDIFF(MONTH, 0, date_submit), 0),date_submit) +1) =  " & txtweek.SelectedValue
            End If
            If txtday.Visible = True And txtday.SelectedValue <> "" Then
                cur_date = New Date(CInt(start_month(1)), CInt(start_month(0)), txtday.SelectedValue)
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(cur_date.Ticks) & "' AND '" & ConvertTSToSQLDateTime(cur_date.Ticks, "23", "59") & "' "
            End If

            sql &= " GROUP BY d.specialty "
            sql &= " ORDER BY d.specialty"
            '  Response.Write(sql)
            '   Return
            ds = conn.getDataSet(sql, "t1")

            Gridview3.DataSource = ds
            Gridview3.DataBind()

            lblNum3.Text = ds.Tables("t1").Rows.Count
            '  Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindGrid4() ' Relevant unit
        Dim sql As String
        Dim ds As New DataSet
        Dim date1 As Date
        Dim date2 As Date

        Dim start_month As Array
        Dim last_month As Array
        Dim last_day_inmonth As Date
        Dim cur_date As Date
        ' Dim date_string1 As String
        '  Dim date_string2 As String
        Try
            start_month = txtdate1.SelectedValue.Split("|")
            date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

            last_month = txtdate2.SelectedValue.Split("|")
            date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

            last_day_inmonth = date2.AddMonths(1).AddDays(-1)
            date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


            sql = "SELECT COUNT(c.dept_name) AS num , c.dept_id , c.dept_name FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN ir_relate_dept c ON a.ir_id = c.ir_id"
            sql &= " INNER JOIN ir_status_list d ON a.status_id = d.ir_status_id"
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "

            sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
            If txtweek.Visible = True And txtweek.SelectedValue <> "" Then
                sql &= " AND (DATEDIFF(week, DATEADD(MONTH, DATEDIFF(MONTH, 0, date_submit), 0),date_submit) +1) =  " & txtweek.SelectedValue
            End If
            If txtday.Visible = True And txtday.SelectedValue <> "" Then
                cur_date = New Date(CInt(start_month(1)), CInt(start_month(0)), txtday.SelectedValue)
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(cur_date.Ticks) & "' AND '" & ConvertTSToSQLDateTime(cur_date.Ticks, "23", "59") & "' "
            End If

            sql &= " GROUP BY c.dept_id , c.dept_name "
            sql &= " ORDER BY  c.dept_name"
            ' Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")

            Gridview4.DataSource = ds
            Gridview4.DataBind()

            lblnum4.Text = ds.Tables("t1").Rows.Count
            '  Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub
    Protected Sub Gridview1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview1.PageIndexChanging
        Gridview1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub Gridview1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblIRNo As Label = CType(e.Row.FindControl("lblIRNo"), Label)
            Dim lblTopic As Label = CType(e.Row.FindControl("lblTopic"), Label)
            Dim lblLevel As Label = CType(e.Row.FindControl("lblLevel"), Label)
            Dim lblMDCode As Label = CType(e.Row.FindControl("lblMDCode"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Dim date1 As Date
            Dim date2 As Date
            Dim start_month As Array
            Dim last_month As Array
            Dim last_day_inmonth As Date
            Dim cur_date As Date
            Try
                lblIRNo.Text = ""
                lblTopic.Text = ""
                lblLevel.Text = ""

                start_month = txtdate1.SelectedValue.Split("|")
                date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

                last_month = txtdate2.SelectedValue.Split("|")
                date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

                last_day_inmonth = date2.AddMonths(1).AddDays(-1)
                date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


                sql = "SELECT * FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id"
                sql &= " INNER JOIN ir_tqm_tab c ON a.ir_id = c.ir_id"
                sql &= " LEFT OUTER JOIN ir_topic_grand d ON d.grand_topic_id = c.grand_topic"
                sql &= " LEFT OUTER JOIN ir_m_severity e ON e.severe_id = c.severe_level_id"
                sql &= " WHERE a.ir_id IN (SELECT ir_id FROM ir_doctor_defendant WHERE md_code = " & lblMDCode.Text & ")"
                sql &= " AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 2"
                ' If txtweek.SelectedValue = "" And txtday.SelectedValue = "" Then
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
                '  End If
                If txtweek.Visible = True And txtweek.SelectedValue <> "" Then
                    sql &= " AND (DATEDIFF(week, DATEADD(MONTH, DATEDIFF(MONTH, 0, date_submit), 0),date_submit) +1) =  " & txtweek.SelectedValue
                End If

                If txtday.Visible = True And txtday.SelectedValue <> "" Then
                    cur_date = New Date(CInt(start_month(1)), CInt(start_month(0)), txtday.SelectedValue)
                    sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(cur_date.Ticks) & "' AND '" & ConvertTSToSQLDateTime(cur_date.Ticks, "23", "59") & "' "
                End If

                ds = conn.getDataSet(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblIRNo.Text &= "<a href='form_incident.aspx?mode=edit&irId=" & ds.Tables("t1").Rows(i)("ir_id").ToString & "&formId=" & ds.Tables("t1").Rows(i)("form_id").ToString & "'>IR" & ds.Tables("t1").Rows(i)("ir_no").ToString & "</a><br/>"
                    lblTopic.Text &= ds.Tables("t1").Rows(i)("grand_topic_name").ToString & "<br/>"
                    lblLevel.Text &= ds.Tables("t1").Rows(i)("severe_name").ToString & "<br/>"
                Next i
            Catch ex As Exception
                Response.Write(ex.Message & sql)

            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub Gridview1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview1.SelectedIndexChanged

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        'bindWeek()

        If txtselect_report.SelectedValue = "1" Then
            bindGrid()
        ElseIf txtselect_report.SelectedValue = "2" Then
            bindGrid2()
        ElseIf txtselect_report.SelectedValue = "3" Then
            bindGrid3()
        ElseIf txtselect_report.SelectedValue = "4" Then
            bindGrid4()
        End If



    End Sub

    Public Shared Sub Export(ByVal fileName As String, ByVal gv As GridView)


        HttpContext.Current.Response.Clear()

        'Export will take two parameter first one the name of Excel File, and second one for gridview to be exported

        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))

        HttpContext.Current.Response.ContentType = "application/octet-stream"

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

    Protected Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        If txtselect_report.SelectedValue = "1" Then
            Export("incident.xls", Gridview1)
        ElseIf txtselect_report.SelectedValue = "2" Then
            Export("unit.xls", Gridview2)
        ElseIf txtselect_report.SelectedValue = "3" Then
            Export("specialty.xls", Gridview3)
        ElseIf txtselect_report.SelectedValue = "4" Then
            Export("relevant.xls", Gridview4)
        End If

    End Sub

    Protected Sub Gridview2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview2.PageIndexChanging
        Gridview2.PageIndex = e.NewPageIndex
        bindGrid2()
    End Sub

    Protected Sub Gridview2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblIRNo As Label = CType(e.Row.FindControl("lblIRNo"), Label)
            Dim lblTopic As Label = CType(e.Row.FindControl("lblTopic"), Label)
            Dim lblLevel As Label = CType(e.Row.FindControl("lblLevel"), Label)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            Dim lblDeptUnitID As Label = CType(e.Row.FindControl("lblDeptUnitID"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Dim date1 As Date
            Dim date2 As Date
            Dim start_month As Array
            Dim last_month As Array
            Dim last_day_inmonth As Date
            Dim cur_date As Date
            Try
                lblIRNo.Text = ""
                lblTopic.Text = ""
                lblLevel.Text = ""

                start_month = txtdate1.SelectedValue.Split("|")
                date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

                last_month = txtdate2.SelectedValue.Split("|")
                date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

                last_day_inmonth = date2.AddMonths(1).AddDays(-1)
                date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


                sql = "SELECT * FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id"
                sql &= " INNER JOIN ir_tqm_tab c ON a.ir_id = c.ir_id"
                sql &= " LEFT OUTER JOIN ir_topic_grand d ON d.grand_topic_id = c.grand_topic"
                sql &= " LEFT OUTER JOIN ir_m_severity e ON e.severe_id = c.severe_level_id"
                sql &= " INNER JOIN ir_status_list f ON a.status_id = f.ir_status_id"
                sql &= " WHERE a.ir_id IN (SELECT ir_id FROM ir_cfb_unit_defendant WHERE dept_unit_id = " & lblDeptUnitID.Text & ")"
                sql &= " AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 2"
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "

                If txtweek.Visible = True And txtweek.SelectedValue <> "" Then
                    sql &= " AND (DATEDIFF(week, DATEADD(MONTH, DATEDIFF(MONTH, 0, date_submit), 0),date_submit) +1) =  " & txtweek.SelectedValue
                End If

                If txtday.Visible = True And txtday.SelectedValue <> "" Then
                    cur_date = New Date(CInt(start_month(1)), CInt(start_month(0)), txtday.SelectedValue)
                    sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(cur_date.Ticks) & "' AND '" & ConvertTSToSQLDateTime(cur_date.Ticks, "23", "59") & "' "
                End If

                ds = conn.getDataSet(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblIRNo.Text &= "<a href='form_incident.aspx?mode=edit&irId=" & ds.Tables("t1").Rows(i)("ir_id").ToString & "&formId=" & ds.Tables("t1").Rows(i)("form_id").ToString & "'>IR" & ds.Tables("t1").Rows(i)("ir_no").ToString & "</a><br/>"
                    lblTopic.Text &= ds.Tables("t1").Rows(i)("grand_topic_name").ToString & "<br/>"
                    lblLevel.Text &= ds.Tables("t1").Rows(i)("severe_name").ToString & "<br/>"
                    lblStatus.Text &= ds.Tables("t1").Rows(i)("ir_status_name").ToString & "<br/>"
                Next i
            Catch ex As Exception
                Response.Write(ex.Message & sql)

            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub Gridview2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview2.SelectedIndexChanged

    End Sub

    Protected Sub Gridview3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblIRNo As Label = CType(e.Row.FindControl("lblIRNo"), Label)
            Dim lblTopic As Label = CType(e.Row.FindControl("lblTopic"), Label)
            Dim lblLevel As Label = CType(e.Row.FindControl("lblLevel"), Label)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            Dim lblSpecialty As Label = CType(e.Row.FindControl("lblSpecialty"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Dim date1 As Date
            Dim date2 As Date
            Dim start_month As Array
            Dim last_month As Array
            Dim last_day_inmonth As Date
            Dim cur_date As Date

            Try
                lblIRNo.Text = ""
                lblTopic.Text = ""
                lblLevel.Text = ""

                start_month = txtdate1.SelectedValue.Split("|")
                date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

                last_month = txtdate2.SelectedValue.Split("|")
                date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

                last_day_inmonth = date2.AddMonths(1).AddDays(-1)
                date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


                sql = "SELECT * FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id"
                sql &= " INNER JOIN ir_tqm_tab c ON a.ir_id = c.ir_id"
                sql &= " LEFT OUTER JOIN ir_topic_grand d ON d.grand_topic_id = c.grand_topic"
                sql &= " LEFT OUTER JOIN ir_m_severity e ON e.severe_id = c.severe_level_id"
                sql &= " INNER JOIN ir_status_list f ON a.status_id = f.ir_status_id"
                sql &= " WHERE a.ir_id IN (SELECT ir_id FROM ir_doctor_defendant xx INNER JOIN m_doctor yy ON xx.md_code = yy.emp_no WHERE yy.specialty = '" & lblSpecialty.Text & "' )"
                sql &= " AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 2"
                ' If txtweek.SelectedValue = "" And txtday.SelectedValue = "" Then
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
                'End If

                If txtweek.Visible = True And txtweek.SelectedValue <> "" Then
                    sql &= " AND (DATEDIFF(week, DATEADD(MONTH, DATEDIFF(MONTH, 0, date_submit), 0),date_submit) +1) =  " & txtweek.SelectedValue
                End If

                If txtday.Visible = True And txtday.SelectedValue <> "" Then
                    cur_date = New Date(CInt(start_month(1)), CInt(start_month(0)), txtday.SelectedValue)
                    sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(cur_date.Ticks) & "' AND '" & ConvertTSToSQLDateTime(cur_date.Ticks, "23", "59") & "' "
                End If

                ds = conn.getDataSet(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblIRNo.Text &= "<a href='form_incident.aspx?mode=edit&irId=" & ds.Tables("t1").Rows(i)("ir_id").ToString & "&formId=" & ds.Tables("t1").Rows(i)("form_id").ToString & "'>IR" & ds.Tables("t1").Rows(i)("ir_no").ToString & "</a><br/>"
                    lblTopic.Text &= ds.Tables("t1").Rows(i)("grand_topic_name").ToString & "<br/>"
                    lblLevel.Text &= ds.Tables("t1").Rows(i)("severe_name").ToString & "<br/>"
                    lblStatus.Text &= ds.Tables("t1").Rows(i)("ir_status_name").ToString & "<br/>"
                Next i
            Catch ex As Exception
                Response.Write(ex.Message & sql)

            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub Gridview3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview3.SelectedIndexChanged

    End Sub

    Protected Sub txtdate1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdate1.SelectedIndexChanged
        bindWeek()
        bindDay()
        txtdate2.SelectedValue = txtdate1.SelectedValue
        txtweek.Visible = True
    End Sub

    Protected Sub txtdate2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdate2.SelectedIndexChanged
        If txtdate2.SelectedValue <> txtdate1.SelectedValue Then
            txtweek.Visible = False
        Else
            txtweek.Visible = True
        End If
    End Sub

    Protected Sub txtweek_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtweek.SelectedIndexChanged
        bindDay()
    End Sub

    Protected Sub txtselect_report_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtselect_report.SelectedIndexChanged

    End Sub

    Protected Sub Gridview4_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview4.PageIndexChanging
        Gridview4.PageIndex = e.NewPageIndex
        bindGrid4()
    End Sub

    Protected Sub Gridview4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview4.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblIRNo As Label = CType(e.Row.FindControl("lblIRNo"), Label)
            Dim lblTopic As Label = CType(e.Row.FindControl("lblTopic"), Label)
            Dim lblLevel As Label = CType(e.Row.FindControl("lblLevel"), Label)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            Dim lblDeptUnitID As Label = CType(e.Row.FindControl("lblDeptUnitID"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Dim date1 As Date
            Dim date2 As Date
            Dim start_month As Array
            Dim last_month As Array
            Dim last_day_inmonth As Date
            Dim cur_date As Date
            Try
                lblIRNo.Text = ""
                lblTopic.Text = ""
                lblLevel.Text = ""

                start_month = txtdate1.SelectedValue.Split("|")
                date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

                last_month = txtdate2.SelectedValue.Split("|")
                date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

                last_day_inmonth = date2.AddMonths(1).AddDays(-1)
                date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


                sql = "SELECT * FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id"
                sql &= " INNER JOIN ir_tqm_tab c ON a.ir_id = c.ir_id"
                sql &= " LEFT OUTER JOIN ir_topic_grand d ON d.grand_topic_id = c.grand_topic"
                sql &= " LEFT OUTER JOIN ir_m_severity e ON e.severe_id = c.severe_level_id"
                sql &= " INNER JOIN ir_status_list f ON a.status_id = f.ir_status_id"
                sql &= " WHERE a.ir_id IN (SELECT ir_id FROM ir_relate_dept WHERE dept_id = " & lblDeptUnitID.Text & ")"
                sql &= " AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 2"
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "

                If txtweek.Visible = True And txtweek.SelectedValue <> "" Then
                    sql &= " AND (DATEDIFF(week, DATEADD(MONTH, DATEDIFF(MONTH, 0, date_submit), 0),date_submit) +1) =  " & txtweek.SelectedValue
                End If

                If txtday.Visible = True And txtday.SelectedValue <> "" Then
                    cur_date = New Date(CInt(start_month(1)), CInt(start_month(0)), txtday.SelectedValue)
                    sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(cur_date.Ticks) & "' AND '" & ConvertTSToSQLDateTime(cur_date.Ticks, "23", "59") & "' "
                End If
                ' Response.Write(sql)
                ds = conn.getDataSet(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblIRNo.Text &= "<a href='form_incident.aspx?mode=edit&irId=" & ds.Tables("t1").Rows(i)("ir_id").ToString & "&formId=" & ds.Tables("t1").Rows(i)("form_id").ToString & "'>IR" & ds.Tables("t1").Rows(i)("ir_no").ToString & "</a><br/>"
                    lblTopic.Text &= ds.Tables("t1").Rows(i)("grand_topic_name").ToString & "<br/>"
                    lblLevel.Text &= ds.Tables("t1").Rows(i)("severe_name").ToString & "<br/>"
                    lblStatus.Text &= ds.Tables("t1").Rows(i)("ir_status_name").ToString & "<br/>"
                Next i
            Catch ex As Exception
                Response.Write(ex.Message & sql)

            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub Gridview4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview4.SelectedIndexChanged

    End Sub
End Class
