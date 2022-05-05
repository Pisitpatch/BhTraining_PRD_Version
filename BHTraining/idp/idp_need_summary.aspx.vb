Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction
Imports System.IO

Partial Class idp_idp_need_summary
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Request.QueryString("viewtype")
        Session("viewtype") = viewtype & ""

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Page.IsPostBack = True Then
        Else ' load first time
            bindYear()
            bindCategory()
            bindMethod()
            bindDept()
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

    Sub bindCategory()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM idp_m_category WHERE ISNULL(is_delete,0) = 0 AND category_type = 'General' "
            'sql = "SELECT report_dept_id AS dept_id , cast(report_dept_id as varchar) + ' : ' + report_dept_name AS dept_name_en FROM idp_trans_list "
            ' sql &= " GROUP BY report_dept_id , report_dept_name ORDER BY report_dept_name"

            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txtcategory.DataSource = reader
            txtcategory.DataBind()

            txtcategory.Items.Insert(0, New ListItem("--All Category--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindMethod()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM idp_m_method"
            'sql = "SELECT report_dept_id AS dept_id , cast(report_dept_id as varchar) + ' : ' + report_dept_name AS dept_name_en FROM idp_trans_list "
            ' sql &= " GROUP BY report_dept_id , report_dept_name ORDER BY report_dept_name"

            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txtmethod.DataSource = reader
            txtmethod.DataBind()

            txtmethod.Items.Insert(0, New ListItem("--All Category--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select e.category_name_en as category_name , MAX(b.topic_id) AS topic_id , CASE WHEN ISNULL(b.topic_name,'') = '' THEN d.topic_name_th ELSE b.topic_name END AS topic_name "
            sql &= ", c.method_name AS methodology , COUNT(*) as num , a.report_jobtype ,  b.method_id , b.category_id , a.plan_year "
            sql &= "from idp_trans_list a inner join idp_function_tab b "
            sql &= "on a.idp_id = b.idp_id  "
            sql &= " inner join idp_m_method c ON b.method_id = c.method_id "
            sql &= " left outer join idp_m_topic d ON b.topic_id = d.topic_id "
            sql &= " inner join idp_m_category e ON b.category_id = e.category_id "
            sql &= "where b.topic_name is not null and b.topic_name <> 'NULL' and ISNULL(a.is_delete,0) = 0 and ISNULL(a.is_cancel,0) = 0  and ISNULL(is_ladder,0) = 0 and a.status_id > 1 "
            sql &= "  AND ISNULL(b.is_delete,0) = 0 "

            If viewtype = "dept" Then
                sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If

            If txtcategory.SelectedIndex <> 0 Then
                sql &= " AND b.category_id = " & txtcategory.SelectedValue
            End If
            If txtmethod.SelectedIndex <> 0 Then
                sql &= " AND b.method_id = " & txtmethod.SelectedValue
            End If
            If txtdept.SelectedIndex <> 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtyear.SelectedValue <> "" Then
                sql &= " AND a.plan_year = " & txtyear.SelectedValue
            End If

            If txtemployee.Text <> "" Then
                sql &= " AND (a.report_by LIKE '%" & txtemployee.Text & "%' OR  a.report_emp_code LIKE '%" & txtemployee.Text & "%') "
            End If

            If txttopic.Text <> "" Then
                sql &= " AND (b.topic_name LIKE '%" & txttopic.Text & "%' OR  b.topic_name_en LIKE '%" & txttopic.Text & "%') "
            End If

            sql &= " group by  e.category_name_en ,  CASE WHEN ISNULL(b.topic_name,'') = '' THEN d.topic_name_th ELSE b.topic_name END  , c.method_name , a.report_jobtype , b.method_id , b.category_id , a.plan_year "
            sql &= " order by 5 desc "
            'Response.Write(sql)
            ' Return
            ds = conn.getDataSet(sql, "t1")

            GridView1.DataSource = ds
            GridView1.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub

    Public Shared Sub Export(ByVal fileName As String, ByVal gv As GridView)


        HttpContext.Current.Response.Clear()

        'Export will take two parameter first one the name of Excel File, and second one for gridview to be exported

        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))

        ' HttpContext.Current.Response.ContentType = "application/octet-stream"
        HttpContext.Current.Response.Charset = "TIS-620"
        HttpContext.Current.Response.ContentType = " application/vnd.ms-excel"

        'HttpContext.Current.Response.Charset = "windows-874"
        ' 
        Dim ms As New System.IO.MemoryStream()
        Dim streamWrite As New System.IO.StreamWriter(ms, Encoding.UTF8)
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(streamWrite)
        '  divExport.RenderControl(htmlWrite)

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

                'table.RenderControl(htmlWriter)
                table.RenderControl(htmlWrite)
                ' render the htmlwriter into the response
                'HttpContext.Current.Response.Write(strWriter.ToString())

                Dim strEncodedHTML As String = Encoding.UTF8.GetString(ms.ToArray)

                HttpContext.Current.Response.Write(strEncodedHTML)
                HttpContext.Current.Response.End()
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
        'Export("idp_summary.xls", GridView1)
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select e.category_name_en as category_name , MAX(b.topic_id) AS topic_id , CASE WHEN ISNULL(b.topic_name,'') = '' THEN d.topic_name_th ELSE b.topic_name END AS topic_name "
            sql &= ", c.method_name AS methodology , COUNT(*) as num , a.report_jobtype ,  b.method_id , b.category_id , a.plan_year "
            sql &= "from idp_trans_list a inner join idp_function_tab b "
            sql &= "on a.idp_id = b.idp_id  "
            sql &= " inner join idp_m_method c ON b.method_id = c.method_id "
            sql &= " left outer join idp_m_topic d ON b.topic_id = d.topic_id "
            sql &= " inner join idp_m_category e ON b.category_id = e.category_id "
            sql &= "where b.topic_name is not null and b.topic_name <> 'NULL' and ISNULL(a.is_delete,0) = 0 and ISNULL(a.is_cancel,0) = 0  and ISNULL(is_ladder,0) = 0 and a.status_id > 1 "
            sql &= "  AND ISNULL(b.is_delete,0) = 0 "

            If viewtype = "dept" Then
                sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If

            If txtcategory.SelectedIndex <> 0 Then
                sql &= " AND b.category_id = " & txtcategory.SelectedValue
            End If
            If txtmethod.SelectedIndex <> 0 Then
                sql &= " AND b.method_id = " & txtmethod.SelectedValue
            End If
            If txtdept.SelectedIndex <> 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtyear.SelectedValue <> "" Then
                sql &= " AND a.plan_year = " & txtyear.SelectedValue
            End If

            If txtemployee.Text <> "" Then
                sql &= " AND (a.report_by LIKE '%" & txtemployee.Text & "%' OR  a.report_emp_code LIKE '%" & txtemployee.Text & "%') "
            End If

            If txttopic.Text <> "" Then
                sql &= " AND (b.topic_name LIKE '%" & txttopic.Text & "%' OR  b.topic_name_en LIKE '%" & txttopic.Text & "%') "
            End If

            sql &= " group by  e.category_name_en ,  CASE WHEN ISNULL(b.topic_name,'') = '' THEN d.topic_name_th ELSE b.topic_name END  , c.method_name , a.report_jobtype , b.method_id , b.category_id , a.plan_year "
            sql &= " order by 5 desc "
            'Response.Write(sql)
            ' Return
            ds = conn.getDataSet(sql, "t1")

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
           
            Me.EnableViewState = False
            Response.AddHeader("content-disposition", "attachment;filename=idp_traning_need.xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"

            '  Response.Charset = "Windows-874"
           ' Response.ContentEncoding = System.Text.Encoding.UTF8
            Response.ContentEncoding = System.Text.Encoding.Unicode
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())

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

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub cmdExport2_Click(sender As Object, e As EventArgs) Handles cmdExport2.Click
        'Export("idp_summary.xls", GridView1)
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select a.idp_no , a.plan_year , a.date_submit , a.report_by , a.report_emp_code , a.report_dept_id , a.report_dept_name , c.status_name"
            sql &= " , b.category_name  , case when isnull(b.is_required,0) = 0 then 'No' else 'Yes' end as 'Required' "
            sql &= " , replace(b.topic_name,  CHAR(13)+CHAR(10), '') as Topic	 , replace(b.expect_detail  ,  CHAR(13)+CHAR(10), '') As 'Expect Outcom'"
            sql &= " , replace(b.methodology ,  CHAR(13)+CHAR(10), '') as methodology"
            sql &= " , CONVERT(VARCHAR(10), b.date_start,101) as 'Date start' , CONVERT(VARCHAR(10), b.date_complete ,101) as 'Date complete'  , b.topic_status "
            sql &= " ,  replace(CONVERT(VARCHAR(1000), b.function_remark),  CHAR(13)+CHAR(10), '') +  '. ' as remark "
            sql &= " from idp_trans_list a inner join idp_function_tab b on a.idp_id = b.idp_id"
            sql &= " inner join idp_status_list c on a.status_id = c.idp_status_id"
            sql &= " where ISNULL(a.is_delete, 0) = 0 And ISNULL(is_cancel, 0) = 0 And ISNULL(is_ladder, 0) = 0 And a.status_id > 1"
            'sql &= " and a.date_submit > '2012-10-1 00:00:00'"

            If viewtype = "dept" Then
                sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If

            If txtcategory.SelectedIndex <> 0 Then
                sql &= " AND b.category_id = " & txtcategory.SelectedValue
            End If
            If txtmethod.SelectedIndex <> 0 Then
                sql &= " AND b.method_id = " & txtmethod.SelectedValue
            End If
            If txtdept.SelectedIndex <> 0 Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtyear.SelectedValue <> "" Then
                sql &= " AND a.plan_year = " & txtyear.SelectedValue
            End If

            If txtemployee.Text <> "" Then
                sql &= " AND (a.report_by LIKE '%" & txtemployee.Text & "%' OR  a.report_emp_code LIKE '%" & txtemployee.Text & "%') "
            End If

            If txttopic.Text <> "" Then
                sql &= " AND (b.topic_name LIKE '%" & txttopic.Text & "%' OR  b.topic_name_en LIKE '%" & txttopic.Text & "%') "
            End If

            ' sql &= " group by  e.category_name_en ,  CASE WHEN ISNULL(b.topic_name,'') = '' THEN d.topic_name_th ELSE b.topic_name END  , c.method_name , a.report_jobtype , b.method_id , b.category_id , a.plan_year "
            sql &= " order by a.report_emp_code "
            'Response.Write(sql)
            ' Return
            ds = conn.getDataSet(sql, "t1")

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()

            Me.EnableViewState = False
            Response.AddHeader("content-disposition", "attachment;filename=idp_traning_need2.xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"

            '  Response.Charset = "Windows-874"
            ' Response.ContentEncoding = System.Text.Encoding.UTF8
            Response.ContentEncoding = System.Text.Encoding.Unicode
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble())

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
End Class
