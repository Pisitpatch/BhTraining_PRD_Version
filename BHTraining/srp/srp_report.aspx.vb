Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Globalization
Partial Class srp_srp_report
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

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        If txtreport.SelectedValue = "1" Then
            bindReport1()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "2" Then
            bindReportPointType()
            GridView1.Visible = False
            GridView2.Visible = True
            '  GridView2.Columns(1).Visible = True
        ElseIf txtreport.SelectedValue = "3" Then
            bindReportCardIssue()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "4" Then
            bindDeptCoast()
            GridView1.Visible = False
            GridView2.Visible = True
        ElseIf txtreport.SelectedValue = "5" Then
            bindEmployeeScore()
            GridView1.Visible = False
            GridView2.Visible = True
        End If
    End Sub

    Sub bindReport1()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select "
            sql &= " c.emp_code AS 'Emp.code' ,  emp_name AS 'Employee' , c.dept_id AS 'Costcenter' , c.dept_name AS 'Department' , card_id AS 'Card No.' , b.emp_code AS 'Manager Emp.code' , ISNULL(b.user_fullname, b.user_fullname_local) AS 'Rewarded by (Manager)'"
            sql &= " , b.dept_id AS 'Cost Center' , b.dept_name AS 'Department (Manager)'"
            sql &= " , CONVERT(varchar, movement_create_date_raw, 103)  AS 'Date'"
            sql &= " , r_note_other AS 'Complimentary Notes :'  "
         
            'sql &= "  ,case when r_out_chk1 = 1 then 'Yes' Else 'No' end AS 'BI Old1'"
            'sql &= "  ,case when r_out_chk2 = 1 then 'Yes' Else 'No' end AS 'BI Old2'"
            'sql &= "  ,case when r_out_chk3 = 1 then 'Yes' Else 'No' end AS 'BI Old3'"
            'sql &= "  ,case when r_out_chk4 = 1 then 'Yes' Else 'No' end AS 'BI Old4'"
            'sql &= "  ,case when r_out_chk5 = 1 then 'Yes' Else 'No' end AS 'BI Old5'"
            'sql &= "  ,case when r_out_chk6 = 1 then 'Yes' Else 'No' end AS 'BI Old6'"


            'sql &= "  ,case when r_new_bi1 = 1 then 'Yes' Else 'No' end AS 'BI New1'"
            'sql &= "  ,case when r_new_bi2 = 1 then 'Yes' Else 'No' end AS 'BI New2'"
            'sql &= "  ,case when r_new_bi3 = 1 then 'Yes' Else 'No' end AS 'BI New3'"
            'sql &= "  ,case when r_new_bi4 = 1 then 'Yes' Else 'No' end AS 'BI New4'"
            'sql &= "  ,case when r_new_bi5 = 1 then 'Yes' Else 'No' end AS 'BI New5'"
            'sql &= "  ,case when r_new_bi6 = 1 then 'Yes' Else 'No' end AS 'BI New6'"
            'sql &= "  ,case when r_new_bi7 = 1 then 'Yes' Else 'No' end AS 'BI New7'"
            'sql &= "  ,case when r_new_bi8 = 1 then 'Yes' Else 'No' end AS 'BI New8'"
            'sql &= "  ,case when r_new_bi9 = 1 then 'Yes' Else 'No' end AS 'BI New9'"
            'sql &= "  ,case when r_new_bi10 = 1 then 'Yes' Else 'No' end AS 'BI New10'"

            sql &= "  ,case when r_2015_1 = 1 then 'Yes' Else 'No' end AS 'CO'"
            sql &= "  ,case when r_2015_2 = 1 then 'Yes' Else 'No' end AS 'A'"
            sql &= "  ,case when r_2015_3 = 1 then 'Yes' Else 'No' end AS 'S'"
            sql &= "  ,case when r_2015_4 = 1 then 'Yes' Else 'No' end AS 'T'"

            ' sql &= " , r_new_bi1 , r_new_bi2 , r_new_bi3 , r_new_bi4 , r_new_bi5"
            ' sql &= " , r_new_bi6 , r_new_bi7 , r_new_bi8 , r_new_bi9 , r_new_bi10 "
            sql &= " from"
            sql &= " srp_point_movement a inner join user_profile b on a.r_award_by_emp_code = b.emp_code"
            sql &= " inner join user_profile c on a.emp_code = c.emp_code"
            sql &= " where movememt_type_id = 1 "

            If txtcard.Text <> "" Then
                sql &= " AND a.card_id LIKE '%" & txtcard.Text & "%' "
            End If

            If txtcode.Text <> "" Then
                sql &= " AND a.emp_code LIKE '%" & txtcode.Text & "%' "
            End If

            If txtname.Text <> "" Then
                sql &= " AND a.emp_name LIKE '%" & txtname.Text & "%' "
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.movement_create_date_raw BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If txtdept.SelectedValue <> "" Then
                '  sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            ' sql &= " ORDER BY a.star_id DESC"
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

    Sub bindReportPointType()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select max(t3.reward_type_name) as 'Project' ,  REPLACE(CONVERT(varchar(20), (CAST(SUM(point_trans) AS money)), 1), '.00', '')   as 'Point'  ,  REPLACE(CONVERT(varchar(20), (CAST( (SUM(point_trans)*60)/100 AS money)), 1), '.00', '')   as 'Amount (Baht)' from srp_point_movement t1 "
            sql &= " inner join srp_m_movement_type t2 on t1.movememt_type_id = t2.movememt_type_id"
            sql &= " inner join srp_m_reward_type t3 on t1.reward_type_id = t3.reward_type_id"
            sql &= " where  1  = 1"
            sql &= " AND t1.movememt_type_id <> 3 "
            If txtcard.Text <> "" Then
                sql &= " AND t1.card_id LIKE '%" & txtcard.Text & "%' "
            End If

            If txtcode.Text <> "" Then
                sql &= " AND t1.emp_code LIKE '%" & txtcode.Text & "%' "
            End If

            If txtname.Text <> "" Then
                sql &= " AND t1.emp_name LIKE '%" & txtname.Text & "%' "
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND t1.movement_create_date_raw BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If txtdept.SelectedValue <> "" Then
                '  sql &= " AND t1.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " group by t1.reward_type_id"
            sql &= " order by max(t3.reward_type_name)"

            ' sql &= " ORDER BY a.star_id DESC"
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

    Sub bindReportCardIssue()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select  b.dept_id AS 'Cost Center', b.dept_name as 'Department', count(*) as 'Card Qty.' ,REPLACE(CONVERT(varchar(20), (CAST( (SUM(point_trans)*60)/100 AS money)), 1), '.00', '')   as 'Amount (Baht)'"
            sql &= " from srp_point_movement a inner join user_profile b on a.emp_code = b.emp_code"
            sql &= " where(movememt_type_id = 1 And reward_type_id = 1)"


            If txtcard.Text <> "" Then
                sql &= " AND a.card_id LIKE '%" & txtcard.Text & "%' "
            End If

            If txtcode.Text <> "" Then
                sql &= " AND a.emp_code LIKE '%" & txtcode.Text & "%' "
            End If

            If txtname.Text <> "" Then
                sql &= " AND a.emp_name LIKE '%" & txtname.Text & "%' "
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.movement_create_date_raw BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If txtdept.SelectedValue <> "" Then
                '  sql &= " AND t1.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " group by  b.dept_id , b.dept_name"
             sql &= " order by b.dept_name"

            ' sql &= " ORDER BY a.star_id DESC"
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

    Sub bindDeptCoast()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select b.dept_id  AS 'Cost Center',  b.dept_name AS Department "
            sql &= " , SUM(CASE WHEN r_2015_1 =1 THEN 1 ELSE 0 END * point_trans) as 'Co' , SUM(CASE WHEN r_2015_2 =1 THEN 1 ELSE 0 END * point_trans)  as 'A' "
            sql &= " , SUM(CASE WHEN r_2015_3 =1 THEN 1 ELSE 0 END  * point_trans)  as 'S' , SUM(CASE WHEN r_2015_4 =1 THEN 1 ELSE 0 END  * point_trans)  as 'T'"
            sql &= "  , ((SUM(CASE WHEN r_2015_1 =1 THEN 1 ELSE 0 END  * point_trans) ) + (SUM(CASE WHEN r_2015_2 =1 THEN 1 ELSE 0 END  * point_trans)) "
            sql &= " + (SUM(CASE WHEN r_2015_3 =1 THEN 1 ELSE 0 END  * point_trans) ) + (SUM(CASE WHEN r_2015_4 =1 THEN 1 ELSE 0 END  * point_trans))) as Total"
            sql &= " from srp_point_movement a inner join user_profile b on a.emp_code = b.emp_code"
            sql &= "             where (movememt_type_id = 1)"
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.movement_create_date_raw BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If
            sql &= " group by b.dept_id , b.dept_name"
            '' sql &= " having (SUM(CASE WHEN r_2015_1 =1 THEN 1 ELSE 0 END) + SUM(CASE WHEN r_2015_2 =1 THEN 1 ELSE 0 END) "
            '' sql &= " + SUM(CASE WHEN r_2015_3 =1 THEN 1 ELSE 0 END) + SUM(CASE WHEN r_2015_4 =1 THEN 1 ELSE 0 END)) > 0"
            sql &= " order by b.dept_name"

            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindEmployeeScore()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select  a.emp_code AS 'Emp.code' ,  MAX(emp_name) AS 'Employee'  ,   SUM(point_trans) AS Point from srp_point_movement a  "
            sql &= " inner join temp_BHUser b on a.emp_code = b.Employee_id "

            sql &= " where a.emp_code > 0  "

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                '   sql &= " AND a.movement_create_date_raw BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If txtcode.Text <> "" Then
                sql &= " AND a.emp_code = " & txtcode.Text
            End If

            If txtname.Text <> "" Then
                sql &= " AND a.emp_name LIKE '%" & txtname.Text & "%' "
            End If

            sql &= " GROUP BY a.emp_code  ORDER BY a.emp_code"

            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView2.DataSource = ds
            GridView2.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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
        Export("srp.xls", GridView2)
    End Sub

    Protected Sub txtreport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtreport.SelectedIndexChanged
        GridView1.Visible = False
        GridView2.Visible = False
    End Sub
End Class
