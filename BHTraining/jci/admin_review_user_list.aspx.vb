Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports ShareFunction

Partial Class jci_admin_review_user_list
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        tid = Request.QueryString("tid")

        'If Not IsNothing(Session("find_dept")) Then
        '    txtdept.SelectedValue = Session("find_dept").ToString
        'End If

        'If Not IsNothing(Session("find_date")) Then
        '    txtdate_report.Text = Session("find_date").ToString
        'End If

        If IsPostBack Then

        Else ' First time load
            bindDept()
            bindGrid()
            bindPathWay()
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

    Sub bindDept()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM user_dept "
            sql &= " ORDER BY dept_name_en"
            reader = conn.getDataReaderForTransaction(sql, "t1")
            'Response.Write(sql)
            txtdept.DataSource = reader
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("--All Department--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.trans_create_by_name , a.trans_create_by_emp_code , a.trans_dept_name , a.trans_job_title , COUNT(b.question_id) AS num FROM jci_trans_list a "
            sql &= " INNER JOIN jci_master_question b ON a.question_id = b.question_id WHERE b.test_id = " & tid

            If txtname.Text <> "" Then
                sql &= " AND (a.trans_create_by_name  LIKE'%" & txtname.Text & "%' OR a.trans_create_by_emp_code LIKE'%" & txtname.Text & "%') "
            End If
            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.trans_dept_id = " & txtdept.SelectedValue
            End If
            If txtdate_report.Text <> "" Then
                sql &= " AND a.trans_create_date BETWEEN '" & convertToSQLDatetime(txtdate_report.Text, "00", "00") & "' AND '" & convertToSQLDatetime(txtdate_report.Text, "23", "59") & "' "
            End If
            sql &= " GROUP BY a.trans_create_by_name , a.trans_create_by_emp_code , a.trans_dept_name , a.trans_job_title , a.question_id "
            sql &= " ORDER BY a.trans_create_by_name "
            ds = conn.getDataSetForTransaction(sql, "t1")


            ' Response.Write(sql)
            ' Return
            GridView1.DataSource = ds
            If Not IsNothing(Session("page_id")) Then
                GridView1.PageIndex = Session("page_id").ToString
                Session.Remove("page_id")
            End If

            Session("find_dept") = txtdept.SelectedValue
            Session("find_date") = txtdate_report.Text

            lblNum.Text = ds.Tables("t1").Rows.Count
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindPathWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            ' sql = "SELECT * FROM jci_master_test WHERE test_id = " & tid
            'ds = conn.getDataSetForTransaction(sql, "t1")

            lblPathWay.Text = " > " & " Review Video Answer"
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

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblEmpCode As Label = CType(e.Row.FindControl("lblEmpCode"), Label)
            Dim lblScore As Label = CType(e.Row.FindControl("lblScore"), Label)
            Dim lblReview As Label = CType(e.Row.FindControl("lblReview"), Label)
            Dim lblResult As Label = CType(e.Row.FindControl("lblResult"), Label)
            Dim lblTotal As Label = CType(e.Row.FindControl("lblTotal"), Label)

            Dim lblLow As Label = CType(e.Row.FindControl("lblLow"), Label)
            Dim lblMid As Label = CType(e.Row.FindControl("lblMid"), Label)
            Dim lblHigh As Label = CType(e.Row.FindControl("lblHigh"), Label)
            Dim sql As String
            Dim ds As New DataSet

            Try

                sql = "SELECT ISNULL(COUNT(a.review_score),0) FROM jci_trans_list a INNER JOIN jci_master_question b ON a.question_id = b.question_id "
                sql &= " WHERE a.trans_create_by_emp_code = " & lblEmpCode.Text & " AND b.test_id = " & tid
                sql &= " AND ISNULL(review_score,0) = 3 "
                'Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                lblReview.Text = ds.Tables("t1").Rows(0)(0).ToString
                lblHigh.Text = ds.Tables("t1").Rows(0)(0).ToString
                lblResult.Text = " (" & FormatNumber(100 * (CDbl(lblReview.Text) / CDbl(lblTotal.Text)), 0) & " % pass)"

                If CDbl(lblReview.Text) <> CDbl(lblTotal.Text) Then
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red
                    e.Row.Cells(5).Font.Bold = True
                Else
                    e.Row.Cells(5).ForeColor = Drawing.Color.Green
                End If


                sql = "SELECT ISNULL(COUNT(a.review_score),0) FROM jci_trans_list a INNER JOIN jci_master_question b ON a.question_id = b.question_id "
                sql &= " WHERE a.trans_create_by_emp_code = " & lblEmpCode.Text & " AND b.test_id = " & tid
                sql &= " AND ISNULL(review_score,0) = 2 "
                'Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t2")
                lblMid.Text = ds.Tables("t2").Rows(0)(0).ToString

                sql = "SELECT ISNULL(COUNT(a.review_score),0) FROM jci_trans_list a INNER JOIN jci_master_question b ON a.question_id = b.question_id "
                sql &= " WHERE a.trans_create_by_emp_code = " & lblEmpCode.Text & " AND b.test_id = " & tid
                sql &= " AND ISNULL(review_score,0) = 1 "
                'Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t3")
                lblLow.Text = ds.Tables("t3").Rows(0)(0).ToString
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
          
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub txtdept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdept.SelectedIndexChanged
        bindGrid()
    End Sub

    Protected Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        Export("FileName.xls", GridView1)
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

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub
End Class
