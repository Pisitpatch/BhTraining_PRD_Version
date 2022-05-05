Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_report_ssip
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected cur_date As String = ""
    Protected irow As Integer = 0
    Protected global_dept_name As String = ""
    Protected viewtype As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        viewtype = Request.QueryString("viewtype")

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
            bindGridSubmitDepartment()
            panel_submit.Visible = True
            panel_award.Visible = False
            panel_implement.Visible = False
            panel_logbook.Visible = False
        ElseIf txtselect_report.SelectedValue = "2" Then
            bindGridAwardDepartment()
            panel_submit.Visible = False
            panel_award.Visible = True
            panel_implement.Visible = False
            panel_logbook.Visible = False
        ElseIf txtselect_report.SelectedValue = "3" Then
            bindGridImplementDepartment()
            panel_submit.Visible = False
            panel_award.Visible = False
            panel_implement.Visible = True
            panel_logbook.Visible = False
        ElseIf txtselect_report.SelectedValue = "4" Then
            bindGridLogbook()
            panel_submit.Visible = False
            panel_award.Visible = False
            panel_implement.Visible = False
            panel_logbook.Visible = True
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

    Sub bindDateRange()
        Dim sql As String
        Dim ds As New DataSet
        Dim myvalue As String
        Dim mytext As String
        Try
            sql = "SELECT  MONTH(submit_date) AS m , YEAR(submit_date) AS y FROM ssip_trans_list WHERE ISNULL(is_delete,0) = 0 AND ISNULL(is_cancel,0) = 0 AND ISNULL(submit_date,0) > 0 "
            sql &= " GROUP BY MONTH(submit_date) , YEAR(submit_date)"
            sql &= " ORDER BY MONTH(submit_date) , YEAR(submit_date) ASC"
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

    Sub bindGridSubmitDepartment()
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


            sql = "SELECT * "
            sql &= " FROM ssip_trans_list a INNER JOIN ssip_detail_tab b ON a.ssip_id = b.ssip_id "
            sql &= " INNER JOIN ssip_status_list c ON a.status_id = c.status_id "
            sql &= " INNER JOIN ssip_hr_tab d ON a.ssip_id = d.ssip_id "
         
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 1 "
         
            sql &= " AND submit_date BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
            If viewtype = "dept" Then
                sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_ssip WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If

            '  sql &= " GROUP BY e.doctor_name_en , e.specialty , e.emp_no"
            sql &= " ORDER BY a.report_dept_name ASC"
            ' Response.Write(sql)
            ' Return
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

    Sub bindGridAwardDepartment()
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


            sql = "SELECT * "
            sql &= " FROM ssip_trans_list a INNER JOIN ssip_detail_tab b ON a.ssip_id = b.ssip_id "
            sql &= " INNER JOIN ssip_status_list c ON a.status_id = c.status_id "
            sql &= " INNER JOIN ssip_hr_tab d ON a.ssip_id = d.ssip_id "

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id = 8 "

            sql &= " AND submit_date BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
            If viewtype = "dept" Then
                sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_ssip WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            '  sql &= " GROUP BY e.doctor_name_en , e.specialty , e.emp_no"
            sql &= " ORDER BY a.report_dept_name ASC"
            ' Response.Write(sql)
            ' Return
            ds = conn.getDataSet(sql, "t1")

            Gridview2.DataSource = ds
            Gridview2.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
            '  Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridImplementDepartment()
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


            sql = "SELECT * "
            sql &= " FROM ssip_trans_list a INNER JOIN ssip_detail_tab b ON a.ssip_id = b.ssip_id "
            sql &= " INNER JOIN ssip_status_list c ON a.status_id = c.status_id "
            sql &= " INNER JOIN ssip_hr_tab d ON a.ssip_id = d.ssip_id "
            sql &= " INNER JOIN ssip_hr_relate_dept e ON a.ssip_id = e.ssip_id AND is_impl_dept = 1 "

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 1 "

            sql &= " AND submit_date BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
            If viewtype = "dept" Then
                sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_ssip WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            '  sql &= " GROUP BY e.doctor_name_en , e.specialty , e.emp_no"
            sql &= " ORDER BY a.report_dept_name ASC"
            ' Response.Write(sql)
            ' Return
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

    Sub bindGridLogbook()
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


            sql = "SELECT * "
            sql &= " FROM ssip_trans_list a INNER JOIN ssip_detail_tab b ON a.ssip_id = b.ssip_id "
            sql &= " INNER JOIN ssip_status_list c ON a.status_id = c.status_id "
            sql &= " INNER JOIN ssip_hr_tab d ON a.ssip_id = d.ssip_id "
            ' sql &= " INNER JOIN ssip_hr_relate_dept e ON a.ssip_id = e.ssip_id AND is_impl_dept = 1 "

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 1 "

            sql &= " AND submit_date BETWEEN '" & ConvertTSToSQLDateTime(date1.Ticks) & "' AND '" & ConvertTSToSQLDateTime(date2.Ticks, "23", "59") & "' "
            If viewtype = "dept" Then
                sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_ssip WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            '  sql &= " GROUP BY e.doctor_name_en , e.specialty , e.emp_no"
            sql &= " ORDER BY a.report_dept_name ASC"
            ' Response.Write(sql)
            ' Return
            ds = conn.getDataSet(sql, "t1")

            Gridview4.DataSource = ds
            Gridview4.DataBind()

            lblNumLog.Text = ds.Tables("t1").Rows.Count
            '  Response.Write(sql)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub Gridview1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview1.PageIndexChanging
        Gridview1.PageIndex = e.NewPageIndex
        bindGridSubmitDepartment()
    End Sub

    Protected Sub Gridview1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblDept As Label = CType(e.Row.FindControl("lblDept"), Label)
            Dim lblDeptId As Label = CType(e.Row.FindControl("lblDeptId"), Label)
            Dim lblHTML0 As Label = CType(e.Row.FindControl("lblHTML0"), Label)
            ' Dim lblHTML1 As Label = CType(e.Row.FindControl("lblHTML1"), Label)
            Dim lblHTML2 As Label = CType(e.Row.FindControl("lblHTML2"), Label)
            Dim lblHTML3_1 As Label = CType(e.Row.FindControl("lblHTML3_1"), Label)
            Dim lblHTML3 As Label = CType(e.Row.FindControl("lblHTML3"), Label)
            Dim lblHTML4 As Label = CType(e.Row.FindControl("lblHTML4"), Label)
            Dim lblHTML5 As Label = CType(e.Row.FindControl("lblHTML5"), Label)

            If irow = 0 Then
                lblHTML0.Text = "<table width='100%' CellPadding=0><tr><td height='25' bgcolor='#eef1f3'><strong>" & lblDept.Text & " (" & lblDeptId.Text & ")" & "<strong></td></tr></table>"
                ' lblHTML1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML2.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML3_1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML3.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML4.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML5.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
            Else
                If lblDept.Text = global_dept_name Then

                Else
                    lblHTML0.Text = "<table width='100%' CellPadding=0><tr><td height='25' bgcolor='#eef1f3'><strong>" & lblDept.Text & " (" & lblDeptId.Text & ")" & "<strong></td></tr></table>"
                    ' lblHTML1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML2.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML3_1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML3.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML4.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML5.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                End If
            End If

            global_dept_name = lblDept.Text
            irow += 1

        End If
    End Sub

    Protected Sub Gridview1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview1.SelectedIndexChanged

    End Sub

    Protected Sub Gridview2_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview2.PageIndexChanging
        Gridview2.PageIndex = e.NewPageIndex
        bindGridAwardDepartment()
    End Sub

    Protected Sub Gridview2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblDept As Label = CType(e.Row.FindControl("lblDept"), Label)
            Dim lblDeptId As Label = CType(e.Row.FindControl("lblDeptId"), Label)
            Dim lblHTML0 As Label = CType(e.Row.FindControl("lblHTML0"), Label)
            Dim lblHTML1 As Label = CType(e.Row.FindControl("lblHTML1"), Label)
            Dim lblHTML2 As Label = CType(e.Row.FindControl("lblHTML2"), Label)
            Dim lblHTML3_1 As Label = CType(e.Row.FindControl("lblHTML3_1"), Label)
            Dim lblHTML3 As Label = CType(e.Row.FindControl("lblHTML3"), Label)
            Dim lblHTML4 As Label = CType(e.Row.FindControl("lblHTML4"), Label)
            Dim lblHTML5 As Label = CType(e.Row.FindControl("lblHTML5"), Label)
            Dim lblHTML6 As Label = CType(e.Row.FindControl("lblHTML6"), Label)

            If irow = 0 Then
                lblHTML0.Text = "<table width='100%' CellPadding=0><tr><td height='25' bgcolor='#eef1f3'><strong>" & lblDept.Text & " (" & lblDeptId.Text & ")" & "<strong></td></tr></table>"
                lblHTML1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML2.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML3_1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML3.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML4.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML5.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                lblHTML6.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
            Else
                If lblDept.Text = global_dept_name Then

                Else
                    lblHTML0.Text = "<table width='100%' CellPadding=0><tr><td height='25' bgcolor='#eef1f3'><strong>" & lblDept.Text & " (" & lblDeptId.Text & ")" & "<strong></td></tr></table>"
                    lblHTML1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML2.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML3_1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML3.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML4.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML5.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                    lblHTML6.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                End If
            End If

            global_dept_name = lblDept.Text
            irow += 1

        End If
    End Sub

    Protected Sub Gridview2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview2.SelectedIndexChanged
 
    End Sub

    Protected Sub Gridview3_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview3.PageIndexChanging
        Gridview3.PageIndex = e.NewPageIndex
        bindGridImplementDepartment()
    End Sub

    Protected Sub Gridview3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblDept As Label = CType(e.Row.FindControl("lblDept"), Label)
            Dim lblDeptId As Label = CType(e.Row.FindControl("lblDeptId"), Label)
            Dim lblHTML0 As Label = CType(e.Row.FindControl("lblHTML0"), Label)
            Dim lblHTML1 As Label = CType(e.Row.FindControl("lblHTML1"), Label)
         

            If irow = 0 Then
                lblHTML0.Text = "<table width='100%' CellPadding=0><tr><td height='25' bgcolor='#eef1f3'><strong>" & lblDept.Text & " (" & lblDeptId.Text & ")" & "<strong></td></tr></table>"
                lblHTML1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
              
            Else
                If lblDept.Text = global_dept_name Then

                Else
                    lblHTML0.Text = "<table width='100%' CellPadding=0><tr><td height='25' bgcolor='#eef1f3'><strong>" & lblDept.Text & " (" & lblDeptId.Text & ")" & "<strong></td></tr></table>"
                    lblHTML1.Text = "<table width='100%' CellPadding=0><tr><td height='25' ></td></tr></table>"
                   
                End If
            End If

            global_dept_name = lblDept.Text
            irow += 1


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
            Export("submit_dept.xls", Gridview1)
        ElseIf txtselect_report.SelectedValue = "2" Then
            Export("award_dept.xls", Gridview2)
        ElseIf txtselect_report.SelectedValue = "3" Then
            Export("impl_dept.xls", Gridview3)
        ElseIf txtselect_report.SelectedValue = "4" Then
            Export("summary.xls", Gridview4)
        End If
    End Sub

    Protected Sub Gridview4_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Gridview4.PageIndexChanging
        Gridview4.PageIndex = e.NewPageIndex
        bindGridLogbook()
    End Sub

    Protected Sub Gridview4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles Gridview4.SelectedIndexChanged

    End Sub
End Class
