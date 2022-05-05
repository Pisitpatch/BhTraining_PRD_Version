Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Globalization

Partial Class star_star_report2
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
            sql &= ", d.srp_point , a.submit_date "
            sql &= " , case when feedback_from = 1 then 'แบบฟอร์มดาวเด่น' when feedback_from = 2 then 'แบบฟอร์ม CFB' when feedback_from = 3 then 'Email / Web site ของ ร.พ.' "
            sql &= " when feedback_from = 4 then 'อีเมล์ / แฟกซ์' when feedback_from = 5 then 'โทรศัพท์' when feedback_from = 6 then 'อื่นๆ' "
            sql &= " end  AS feedback "

            sql &= "  FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id"
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " LEFT OUTER JOIN  star_hr_tab d ON a.star_id = d.star_id "
            ' sql &= " LEFT OUTER JOIN star_relate_person s1 ON a.star_id = s1.star_id "
            ' sql &= " LEFT OUTER JOIN star_relate_person s1 ON a.star_id = s1.star_id "
            '  sql &= " LEFT OUTER JOIN star_relate_person s1 ON a.star_id = s1.star_id "

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "
            sql &= " AND a.status_id > 1 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.submit_date BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY a.star_id DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
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
            sql = "select  CONVERT(VARCHAR(10),a.submit_date,101) AS 'Submit Date' , d.star_no AS 'Star No.' , e.status_name AS 'Status' , d.complain_status_remark AS 'ผู้เสนอชื่อ' "
            sql &= " , case when complain_status = 1 then 'ผู้ป่วย' when complain_status = 2 then 'ญาติผู้ป่วยหรือเพื่อน'  when complain_status = 3 then 'ผู้มาเยี่ยมไข้' "
            sql &= " when complain_status = 4 then 'พนักงาน'  when complain_status = 5 then 'แพทย์' else '-' end as 'ประเภทผู้เสนอ' "
            sql &= " , c.doctor_name as 'ชื่อแพทย์ที่ถูกเสนอ' , c.emp_code as 'Doctor Code' "
            sql &= "  , case when d.feedback_from = 1 then 'แบบฟอร์มดาวเด่น' when d.feedback_from = 2 then 'แบบฟอร์ม CFB' "
            sql &= " when d.feedback_from = 3 then 'Email / Web site ของ ร.พ.' when d.feedback_from = 4 then 'อีเมล์ / แฟกซ์' "
            sql &= " when d.feedback_from = 5 then 'โทรศัพท์' when d.feedback_from = 6 then 'อื่นๆ - ' + d.feedback_from_remark  "
            sql &= " end AS 'Nominate by' "
            sql &= " from star_trans_list a "
            sql &= " left outer join star_hr_tab b on a.star_id = b.star_id"
            sql &= " inner join star_relate_doctor c on a.star_id = c.star_id"
            sql &= " inner join star_detail_tab d on a.star_id = d.star_id"
            sql &= " inner join star_status_list e on a.status_id = e.status_id"
            sql &= " where(isnull(a.is_delete, 0) = 0 And isnull(a.is_delete, 0) = 0) and a.status_id = 7 "


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.submit_date BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
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
            sql = "select CONVERT(VARCHAR(10),a.submit_date,101) AS 'Submit Date' , d.star_no AS 'Star No.' , e.status_name AS 'Status' , d.complain_status_remark AS 'ผู้เสนอชื่อ' "
            sql &= " , case when complain_status = 1 then 'ผู้ป่วย' when complain_status = 2 then 'ญาติผู้ป่วยหรือเพื่อน'  when complain_status = 3 then 'ผู้มาเยี่ยมไข้' "
            sql &= " when complain_status = 4 then 'พนักงาน'  when complain_status = 5 then 'แพทย์' else '-' end as 'ประเภทผู้เสนอ' "
            sql &= " , c.user_fullname as 'ชื่อพนักงานที่ถูกเสนอ' , c.emp_code as 'Empployee Code' , f.job_type AS 'Job Type' , f.dept_name as 'Department' , ISNULL(b.srp_point,0) AS 'Staff Recognition Point' "
            sql &= " from star_trans_list a "
            sql &= " left outer join star_hr_tab b on a.star_id = b.star_id"
            sql &= " inner join star_relate_person c on a.star_id = c.star_id"
            sql &= " inner join star_detail_tab d on a.star_id = d.star_id"
            sql &= " inner join star_status_list e on a.status_id = e.status_id"
            sql &= " inner join user_profile f on c.emp_code = f.emp_code "
            sql &= " where(isnull(a.is_delete, 0) = 0 And isnull(a.is_delete, 0) = 0) and a.status_id = 7 "

            sql &= " AND a.star_id IN (SELECT star_id FROM star_relate_person a1 INNER JOIN user_profile a2 ON a1.emp_code = a2.emp_code WHERE a2.dept_id IN "
            sql &= " (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") )"

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.submit_date BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
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

    Sub bindReportDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select  CONVERT(VARCHAR(10),a.submit_date,101) AS 'Submit Date' , d.star_no AS 'Star No.' , e.status_name AS 'Status' , d.complain_status_remark AS 'ผู้เสนอชื่อ' "
            sql &= " , case when complain_status = 1 then 'ผู้ป่วย' when complain_status = 2 then 'ญาติผู้ป่วยหรือเพื่อน'  when complain_status = 3 then 'ผู้มาเยี่ยมไข้' "
            sql &= " when complain_status = 4 then 'พนักงาน'  when complain_status = 5 then 'แพทย์' else '-' end as 'ประเภทผู้เสนอ' "
            sql &= " , c.costcenter_name as 'ชื่อทีมที่ถูกเสนอ' , c.costcenter_id as 'Cost Center' , ISNULL(b.srp_point,0) AS 'Staff Recognition Point' "


            sql &= " from star_trans_list a "
            sql &= " left outer join star_hr_tab b on a.star_id = b.star_id"
            sql &= " inner join star_relate_dept c on a.star_id = c.star_id"
            sql &= " inner join star_detail_tab d on a.star_id = d.star_id"
            sql &= " inner join star_status_list e on a.status_id = e.status_id"
            sql &= " where(isnull(a.is_delete, 0) = 0 And isnull(a.is_delete, 0) = 0)"


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.submit_date BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
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
            sql &= " , case when CONVERT(varchar, datetime_complaint, 103) = '01/01/1900' then '-' else CONVERT(varchar, datetime_complaint, 103) end   as 'วันที่เขียนเสนอชื่อ' "
            sql &= " , case when CONVERT(varchar, datetime_report, 103) = '01/01/1900' then '-' else CONVERT(varchar, datetime_report, 103) end   as 'วันที่เกิดเหตุการณ์' "
            sql &= " , case when CONVERT(varchar, submit_date, 103) = '01/01/1900' then '-' else CONVERT(varchar, submit_date, 103) end as 'submit date'"
            '   sql &= " , case when CONVERT(varchar, award_date, 103) = '01/01/1900' then '-' else CONVERT(varchar, award_date, 103) end as 'award date'"

            sql &= " , a.report_by as 'ผู้ทำรายการ'"
            sql &= " from star_trans_list a inner join star_detail_tab b"
            sql &= " on a.star_id = b.star_id"
            sql &= " left outer join (SELECT star_id , MAX(log_time) AS award_date FROM star_status_log WHERE status_id = 7 GROUP BY star_id ) c ON a.star_id = c.star_id "
            sql &= " where a.status_id > 1 and isnull(a.is_delete,0) = 0 and isnull(a.is_cancel,0) = 0"


            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.submit_date BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
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
        If txtreport.SelectedValue = "3" Then
            bindReportEmployee()
        ElseIf txtreport.SelectedValue = "4" Then
            bindReportDoctor()
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
        End If

    End Sub


    Protected Sub txtreport_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtreport.SelectedIndexChanged
        'GridView1.Visible = False
        ' GridView2.Visible = False
    End Sub
End Class
