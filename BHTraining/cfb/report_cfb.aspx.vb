Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class cfb_report_cfb
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected cur_date As String = ""
    Protected clinic_level(6) As String
    Protected global_unit_num As String = ""
    Protected global_unit_name As String = ""

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

        clinic_level(0) = ""
        clinic_level(1) = "Near miss (0)"
        clinic_level(2) = "No harm (1)"
        clinic_level(3) = "Mild AE (2)"
        clinic_level(4) = "Moderate AE (3)"
        clinic_level(5) = "Serious AE (4)"
        clinic_level(6) = "Sentinel Event (5)"

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
        lblDateRelevant1.Text = txtdate1.SelectedItem.Text
        lblDateRelevant2.Text = txtdate2.SelectedItem.Text
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

    Sub bindDefendant()
        Dim sql As String
        Dim ds As New DataSet
        Dim login_date As Date
        Dim query_date As Date
        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)
        Try
            sql = "SELECT b.comment_type_name , a.dept_unit_name , ISNULL(COUNT(a.dept_unit_name),0) AS num FROM ir_cfb_unit_defendant a INNER JOIN cfb_comment_list b ON a.ir_id = b.ir_id AND a.comment_id = b.comment_id "
            sql &= " INNER JOIN ir_trans_list c ON b.ir_id = c.ir_id"
            sql &= " WHERE date_submit BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & query_date.Day & "' AND "
            sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            sql &= " AND c.status_id > 1 AND ISNULL(c.is_delete,0) = 0 AND ISNULL(c.is_cancel,0) = 0  "
            sql &= " GROUP BY b.comment_type_name , a.dept_unit_id , a.dept_unit_name ORDER BY COUNT(a.dept_unit_name) DESC "

            ds = conn.getDataSet(sql, "t1")

            Dim limit = ""
            global_unit_num = ""
            global_unit_name = ""
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 10 Then
                    Exit For
                End If

                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                global_unit_num &= limit & "['" & ds.Tables("t1").Rows(i)("dept_unit_name").ToString.Trim & "' , " & CInt(ds.Tables("t1").Rows(i)("num").ToString) & " ]"

                'global_unit_num &= limit & CInt(ds.Tables("t1").Rows(i)("num").ToString)
                '   global_unit_name &= limit & "'" & ds.Tables("t1").Rows(i)("dept_unit_name").ToString & "'"
            Next i
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Dim date1 As Date
        Dim date2 As Date
        Dim start_month As Array
        Dim last_month As Array
        Dim last_day_inmonth As Date

        ' Dim date_string1 As String
        '  Dim date_string2 As String
        Try
            start_month = txtdate1.SelectedValue.Split("|")
            date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

            last_month = txtdate2.SelectedValue.Split("|")
            date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

            last_day_inmonth = date2.AddMonths(1).AddDays(-1)
            date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


            sql = "SELECT e.doctor_name_en , e.specialty , COUNT(c.comment_id) AS num , e.emp_no "
            sql &= " FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id "
            sql &= " INNER JOIN ir_doctor_defendant d ON a.ir_id = d.ir_id AND c.comment_id = d.comment_id"
            sql &= " INNER JOIN m_doctor e ON d.md_code = e.emp_no "
            sql &= ""
            sql &= ""
            sql &= ""
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 2 "
            If txtcomment_type.SelectedValue <> "" Then
                sql &= " AND c.comment_type_id = " & txtcomment_type.SelectedValue
            End If
            sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "

            sql &= " GROUP BY e.doctor_name_en , e.specialty , e.emp_no"
            sql &= " ORDER BY e.doctor_name_en"
            ' Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")

            Gridview1.DataSource = ds
            Gridview1.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
            '  Response.Write(sql)
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

        ' Dim date_string1 As String
        '  Dim date_string2 As String
        Try
            start_month = txtdate1.SelectedValue.Split("|")
            date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

            last_month = txtdate2.SelectedValue.Split("|")
            date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

            last_day_inmonth = date2.AddMonths(1).AddDays(-1)
            date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


            sql = "SELECT COUNT(d.dept_unit_name) AS num , d.dept_unit_id , d.dept_unit_name FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id "
            sql &= " INNER JOIN ir_cfb_unit_defendant d ON a.ir_id = d.ir_id AND c.comment_id = d.comment_id "
            sql &= " INNER JOIN ir_status_list e ON a.status_id = e.ir_status_id"
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 2 "
            If txtcomment_type.SelectedValue <> "" Then
                sql &= " AND c.comment_type_id = " & txtcomment_type.SelectedValue
            End If
            sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "

            sql &= " GROUP BY d.dept_unit_id , d.dept_unit_name "
            sql &= " ORDER BY d.dept_unit_name"
            '  Response.Write(sql)
            ' Return
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

        ' Dim date_string1 As String
        '  Dim date_string2 As String
        Try
            start_month = txtdate1.SelectedValue.Split("|")
            date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

            last_month = txtdate2.SelectedValue.Split("|")
            date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

            last_day_inmonth = date2.AddMonths(1).AddDays(-1)
            date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


            sql = "SELECT COUNT(e.specialty) AS num ,  e.specialty FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id "
            sql &= " INNER JOIN ir_doctor_defendant d ON a.ir_id = d.ir_id AND c.comment_id = d.comment_id"
            sql &= " INNER JOIN m_doctor e ON d.md_code = e.emp_no"
            sql &= " INNER JOIN ir_status_list f ON a.status_id = f.ir_status_id"
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 2 AND e.specialty IS NOT NULL "
            If txtcomment_type.SelectedValue <> "" Then
                sql &= " AND c.comment_type_id = " & txtcomment_type.SelectedValue
            End If
            sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "

            sql &= " GROUP BY e.specialty "
            sql &= " ORDER BY e.specialty"
            'Response.Write(sql)
            'Return
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


            sql = "SELECT COUNT(d.dept_name) AS num , d.dept_id , d.dept_name FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id "
            sql &= " INNER JOIN cbf_relate_dept d ON c.comment_id = d.comment_id  "
            sql &= " INNER JOIN ir_status_list e ON a.status_id = e.ir_status_id"
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 2 "
            If txtcomment_type.SelectedValue <> "" Then
                sql &= " AND c.comment_type_id = " & txtcomment_type.SelectedValue
            End If
            sql &= " AND a.date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "

            sql &= " GROUP BY d.dept_id , d.dept_name "
            sql &= " ORDER BY d.dept_name"
            ' Response.Write(sql)
            '   Return
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
            Dim lblCFBNo As Label = CType(e.Row.FindControl("lblCFBNo"), Label)
            Dim lblTopic As Label = CType(e.Row.FindControl("lblTopic"), Label)
            Dim lblLevel As Label = CType(e.Row.FindControl("lblLevel"), Label)
            Dim lblMDCode As Label = CType(e.Row.FindControl("lblMDCode"), Label)
            Dim lblType As Label = CType(e.Row.FindControl("lblType"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Dim date1 As Date
            Dim date2 As Date
            Dim start_month As Array
            Dim last_month As Array
            Dim last_day_inmonth As Date

            Try
                lblCFBNo.Text = ""
                lblTopic.Text = ""
                lblLevel.Text = ""

                start_month = txtdate1.SelectedValue.Split("|")
                date1 = New Date(CInt(start_month(1)), CInt(start_month(0)), 1)

                last_month = txtdate2.SelectedValue.Split("|")
                date2 = New Date(CInt(last_month(1)), CInt(last_month(0)), 1)

                last_day_inmonth = date2.AddMonths(1).AddDays(-1)
                date2 = New Date(last_day_inmonth.Year, last_day_inmonth.Month, last_day_inmonth.Day)


                sql = "SELECT * FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id"
                sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id"
                sql &= " LEFT OUTER JOIN ir_topic_grand d ON d.grand_topic_id = c.grand_topic"
                ' sql &= " LEFT OUTER JOIN ir_m_severity e ON e.severe_id = c.severe_level_id"
                sql &= " WHERE a.ir_id IN (SELECT ir_id FROM ir_doctor_defendant WHERE md_code = " & lblMDCode.Text & " AND ir_id = a.ir_id AND comment_id = c.comment_id)"
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
                If txtcomment_type.SelectedValue <> "" Then
                    sql &= " AND c.comment_type_id = " & txtcomment_type.SelectedValue
                End If

                '  Response.Write(sql)
                ds = conn.getDataSet(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblCFBNo.Text &= "<a href='form_cfb.aspx?mode=edit&cfbId=" & ds.Tables("t1").Rows(i)("ir_id").ToString & "&cid=" & ds.Tables("t1").Rows(i)("comment_id").ToString & "'>CFB" & ds.Tables("t1").Rows(i)("cfb_no").ToString & "</a><br/>"
                    lblTopic.Text &= ds.Tables("t1").Rows(i)("grand_topic_name").ToString & "<br/>"
                    If ds.Tables("t1").Rows(i)("tqm_clinic").ToString <> "" Then
                        lblLevel.Text &= clinic_level(CInt(ds.Tables("t1").Rows(i)("tqm_clinic").ToString)) & "<br/>"
                    End If

                    lblType.Text &= ds.Tables("t1").Rows(i)("comment_type_name").ToString & "<br/>"
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
            Dim lblType As Label = CType(e.Row.FindControl("lblType"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Dim date1 As Date
            Dim date2 As Date
            Dim start_month As Array
            Dim last_month As Array
            Dim last_day_inmonth As Date

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


                sql = "SELECT * FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id"
                sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id"
                sql &= " LEFT OUTER JOIN ir_topic_grand d ON d.grand_topic_id = c.grand_topic"
                ' sql &= " LEFT OUTER JOIN ir_m_severity e ON e.severe_id = c.severe_level_id"
                sql &= " INNER JOIN ir_status_list f ON a.status_id = f.ir_status_id"
                sql &= " WHERE a.ir_id IN (SELECT ir_id FROM ir_cfb_unit_defendant WHERE dept_unit_id = " & lblDeptUnitID.Text & " AND comment_id = c.comment_id)"
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
                If txtcomment_type.SelectedValue <> "" Then
                    sql &= " AND c.comment_type_id = " & txtcomment_type.SelectedValue
                End If
                ds = conn.getDataSet(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblIRNo.Text &= "<a href='form_cfb.aspx?mode=edit&cfbId=" & ds.Tables("t1").Rows(i)("ir_id").ToString & "&cid=" & ds.Tables("t1").Rows(i)("comment_id").ToString & "'>CFB" & ds.Tables("t1").Rows(i)("cfb_no").ToString & "</a><br/>"
                    lblTopic.Text &= ds.Tables("t1").Rows(i)("grand_topic_name").ToString & "<br/>"
                    If ds.Tables("t1").Rows(i)("tqm_clinic").ToString <> "" Then
                        lblLevel.Text &= clinic_level(CInt(ds.Tables("t1").Rows(i)("tqm_clinic").ToString)) & "<br/>"
                    End If
                    lblStatus.Text &= ds.Tables("t1").Rows(i)("ir_status_name").ToString & "<br/>"
                    lblType.Text &= ds.Tables("t1").Rows(i)("comment_type_name").ToString & "<br/>"
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

    Protected Sub Gridview3_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview3.PageIndexChanging
        Gridview3.PageIndex = e.NewPageIndex
        bindGrid3()
    End Sub

    Protected Sub Gridview3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblIRNo As Label = CType(e.Row.FindControl("lblIRNo"), Label)
            Dim lblTopic As Label = CType(e.Row.FindControl("lblTopic"), Label)
            Dim lblLevel As Label = CType(e.Row.FindControl("lblLevel"), Label)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            Dim lblSpecialty As Label = CType(e.Row.FindControl("lblSpecialty"), Label)
            Dim lblType As Label = CType(e.Row.FindControl("lblType"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Dim date1 As Date
            Dim date2 As Date
            Dim start_month As Array
            Dim last_month As Array
            Dim last_day_inmonth As Date

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


                sql = "SELECT * FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id"
                sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id"
                sql &= " LEFT OUTER JOIN ir_topic_grand d ON d.grand_topic_id = c.grand_topic"
                ' sql &= " LEFT OUTER JOIN ir_m_severity e ON e.severe_id = c.severe_level_id"
                sql &= " INNER JOIN ir_status_list f ON a.status_id = f.ir_status_id"
                sql &= " WHERE a.ir_id IN (SELECT ir_id FROM ir_doctor_defendant xx INNER JOIN m_doctor yy ON xx.md_code = yy.emp_no WHERE yy.specialty = '" & lblSpecialty.Text & "' AND xx.comment_id = c.comment_id )"
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
                If txtcomment_type.SelectedValue <> "" Then
                    sql &= " AND c.comment_type_id = " & txtcomment_type.SelectedValue
                End If
                '  Response.Write(sql & "<hr/>")
                ds = conn.getDataSet(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblIRNo.Text &= "<a href='form_cfb.aspx?mode=edit&cfbId=" & ds.Tables("t1").Rows(i)("ir_id").ToString & "&cid=" & ds.Tables("t1").Rows(i)("comment_id").ToString & "'>CFB" & ds.Tables("t1").Rows(i)("cfb_no").ToString & "</a><br/>"
                    lblTopic.Text &= ds.Tables("t1").Rows(i)("grand_topic_name").ToString & "<br/>"
                    If ds.Tables("t1").Rows(i)("tqm_clinic").ToString <> "" Then
                        lblLevel.Text &= clinic_level(CInt(ds.Tables("t1").Rows(i)("tqm_clinic").ToString)) & "<br/>"
                    End If
                    lblStatus.Text &= ds.Tables("t1").Rows(i)("ir_status_name").ToString & "<br/>"
                    lblType.Text &= ds.Tables("t1").Rows(i)("comment_type_name").ToString & "<br/>"
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
            Export("cfb.xls", Gridview1)
        ElseIf txtselect_report.SelectedValue = "2" Then
            Export("unit.xls", Gridview2)
        ElseIf txtselect_report.SelectedValue = "3" Then
            Export("specialty.xls", Gridview3)
        ElseIf txtselect_report.SelectedValue = "4" Then
            Export("relevant.xls", Gridview4)
        End If
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click

    End Sub

    Protected Sub txtcomment_type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcomment_type.SelectedIndexChanged

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
            Dim lblType As Label = CType(e.Row.FindControl("lblType"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Dim date1 As Date
            Dim date2 As Date
            Dim start_month As Array
            Dim last_month As Array
            Dim last_day_inmonth As Date

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


                sql = "SELECT * FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id"
                sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id"
                sql &= " LEFT OUTER JOIN ir_topic_grand d ON d.grand_topic_id = c.grand_topic"
                ' sql &= " LEFT OUTER JOIN ir_m_severity e ON e.severe_id = c.severe_level_id"
                sql &= " INNER JOIN ir_status_list f ON a.status_id = f.ir_status_id"
                sql &= " WHERE c.comment_id IN (SELECT comment_id FROM cbf_relate_dept WHERE dept_id = " & lblDeptUnitID.Text & " AND comment_id = c.comment_id)"
                sql &= " AND date_submit BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
                If txtcomment_type.SelectedValue <> "" Then
                    sql &= " AND c.comment_type_id = " & txtcomment_type.SelectedValue
                End If
                ds = conn.getDataSet(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblIRNo.Text &= "<a href='form_cfb.aspx?mode=edit&cfbId=" & ds.Tables("t1").Rows(i)("ir_id").ToString & "&cid=" & ds.Tables("t1").Rows(i)("comment_id").ToString & "'>CFB" & ds.Tables("t1").Rows(i)("cfb_no").ToString & "</a><br/>"
                    lblTopic.Text &= ds.Tables("t1").Rows(i)("grand_topic_name").ToString & "<br/>"
                    If ds.Tables("t1").Rows(i)("tqm_clinic").ToString <> "" Then
                        lblLevel.Text &= clinic_level(CInt(ds.Tables("t1").Rows(i)("tqm_clinic").ToString)) & "<br/>"
                    End If
                    lblStatus.Text &= ds.Tables("t1").Rows(i)("ir_status_name").ToString & "<br/>"
                    lblType.Text &= ds.Tables("t1").Rows(i)("comment_type_name").ToString & "<br/>"
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
