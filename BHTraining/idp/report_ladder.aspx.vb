Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Globalization

Partial Class idp_report_ladder
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected flag As String = "ladder"

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

            bindReport1()
            ' bindReport2()
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
            sql = "SELECT * "
            sql &= "  FROM idp_trans_list a "
            sql &= " INNER JOIN idp_status_ladder c ON a.status_id = c.idp_status_id"
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.is_ladder = 1 "
            'sql &= " AND a.status_id > 1 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY a.idp_id DESC"
            ds = conn.getDataSet(sql, "table1")

            ' Response.Write(sql)
            'Return

            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
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

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        bindReport1()
    End Sub

    Protected Sub cmdExport_Click(sender As Object, e As System.EventArgs) Handles cmdExport.Click
        Export("ladder.xls", GridView1)
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            '  Dim HyperLink1 As HyperLink = CType(e.Row.FindControl("HyperLink1"), HyperLink)
            Dim lblPk As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim lblStatusId As Label = CType(e.Row.FindControl("lblStatusId"), Label)

            Dim lblImage1 As Label = CType(e.Row.FindControl("lblImage1"), Label)
            Dim lblImage2 As Label = CType(e.Row.FindControl("lblImage2"), Label)
            Dim lblImage3 As Label = CType(e.Row.FindControl("lblImage3"), Label)
            Dim lblImage4 As Label = CType(e.Row.FindControl("lblImage4"), Label)
            Dim lblImage5 As Label = CType(e.Row.FindControl("lblImage5"), Label)
            Dim lblImage6 As Label = CType(e.Row.FindControl("lblImage6"), Label)
            Dim lblImage7 As Label = CType(e.Row.FindControl("lblImage7"), Label)
            Dim lblImage8 As Label = CType(e.Row.FindControl("lblImage8"), Label)
            Dim lblReview As Label = CType(e.Row.FindControl("lblReview"), Label)
            '  Dim lblDirectorComment As Label = CType(e.Row.FindControl("lblDirectorComment"), Label)


            Dim user_max_authen As String = "0"

            Dim sql As String
            Dim ds As New DataSet
            Dim dsReply As New DataSet
            Try


                If flag = "ladder" Then
                    lblReview.Text = ""

                    sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                    sql &= " AND is_educator = 1 "
                    ds = conn.getDataSet(sql, "tEdu")

                    For i As Integer = 0 To ds.Tables("tEdu").Rows.Count - 1

                        If i = 0 Then
                            If ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage6.Text = "Approve"
                            ElseIf ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage6.Text = "Reject"
                            ElseIf ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage6.Text = "N/A "
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage6.Text = "-"
                                Else
                                    lblImage6.Text = "Wait for approve"
                                End If

                            End If
                        End If


                        If i = 1 Then
                            If ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage7.Text = "Approve"
                            ElseIf ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage7.Text = "Reject"
                            ElseIf ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage7.Text = "N/A"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage7.Text = "-"
                                Else
                                    lblImage7.Text = "Wait for approve"
                                End If

                            End If
                        End If

                        If i = 2 Then
                            If ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage8.Text = "Approve"
                            ElseIf ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage8.Text = "Reject"
                            ElseIf ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage8.Text = "N/A"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage8.Text = "-"
                                Else
                                    lblImage8.Text = "Wait for approve"
                                End If

                            End If
                        End If

                        lblReview.Text &= ds.Tables("tEdu").Rows(i)("detail").ToString & vbCrLf
                        '  Response.Write(12222)
                    Next i

                    If ds.Tables("tEdu").Rows.Count = 0 Then
                        lblReview.Text = "-"

                        If lblStatusId.Text = "1" Then
                            lblImage6.Text = "-"
                        Else
                            lblImage6.Text = "Wait for approve"
                        End If

                        If lblStatusId.Text = "1" Then
                            lblImage7.Text = "-"
                        Else
                            lblImage7.Text = "Wait for approve"
                        End If

                        If lblStatusId.Text = "1" Then
                            lblImage8.Text = "-"
                        Else
                            lblImage8.Text = "Wait for approve"
                        End If
                    Else
                        '   lblAuthen.Text = "4th Approval"
                    End If
                    'If lblEducatorStatus.Text = "3" Then ' Wait
                    '    lblImage6.Text = "<img src='../images/history.png' id='img1' />"
                    'ElseIf lblEducatorStatus.Text = "2" Then ' Reject
                    '    lblImage6.Text = "<img src='../images/button_cancel.png' id='img1' alt='" & lblEducatorStatus.Text & "' title='" & lblEducatorReviewBy.Text & "' />"
                    'ElseIf lblEducatorStatus.Text = "1" Then ' Approve
                    '    lblImage6.Text = "<img src='../images/button_ok.png' id='img1' alt='" & lblEducatorStatus.Text & "' title='" & lblEducatorReviewBy.Text & "' />"
                    'Else
                    '    lblImage6.Text = "-"
                    'End If
                End If

             

                Do While True
                    If 17 > CInt(user_max_authen) Then
                        ' ========== 5th
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  17 AND ISNULL(is_educator,0) = 0 "
                        ' Response.Write(sql & "<br/>")
                        ' Response.Write(1)
                        ds = conn.getDataSet(sql, "t5")
                        'Response.Write(2)
                        For i As Integer = 0 To ds.Tables("t5").Rows.Count - 1

                            If ds.Tables("t5").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage5.Text &= "Approve"
                            ElseIf ds.Tables("t5").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage5.Text &= "Not approve"
                            ElseIf ds.Tables("t5").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage5.Text &= "N/A"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage5.Text &= "-"
                                Else
                                    lblImage5.Text &= "Wait for approve"
                                End If

                            End If
                            '  Response.Write(12222)
                        Next i

                        If ds.Tables("t5").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage5.Text &= "-"
                            Else
                                lblImage5.Text &= "Wait for approve"
                            End If
                        Else
                            '   lblAuthen.Text = "4th Approval"
                        End If


                    Else
                        lblImage5.Text &= "-"

                    End If


                    If 16 > CInt(user_max_authen) Then
                        ' ========== 4th
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  16  AND ISNULL(is_educator,0) = 0 "
                        'Response.Write(sql)
                        ' Response.Write(1)
                        ds = conn.getDataSet(sql, "t4")
                        'Response.Write(2)
                        For i As Integer = 0 To ds.Tables("t4").Rows.Count - 1

                            If ds.Tables("t4").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage4.Text &= "Approve"
                            ElseIf ds.Tables("t4").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage4.Text &= "Not approve"
                            ElseIf ds.Tables("t4").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage4.Text &= "N/A"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage4.Text &= "-"
                                Else
                                    lblImage4.Text &= "Wait for approve"
                                End If

                            End If
                            '  Response.Write(12222)
                        Next i

                        If ds.Tables("t4").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage4.Text &= "-"
                            Else
                                lblImage4.Text &= "Wait for approve"
                            End If
                        Else
                            '   lblAuthen.Text = "4th Approval"
                        End If

                    Else
                        lblImage4.Text &= "-"

                    End If


                    If 15 > CInt(user_max_authen) Then
                        ' ========== 3rd
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  15  AND ISNULL(is_educator,0) = 0 "
                        ' Response.Write(sql)
                        ds = conn.getDataSet(sql, "t3")
                        For i As Integer = 0 To ds.Tables("t3").Rows.Count - 1
                            If ds.Tables("t3").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage3.Text &= "Approve"
                            ElseIf ds.Tables("t3").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage3.Text &= "Not approve"
                            ElseIf ds.Tables("t3").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage3.Text &= "N/A"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage3.Text &= "-"
                                Else
                                    lblImage3.Text &= "Wait for approve"
                                End If

                            End If

                        Next i

                        If ds.Tables("t3").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage3.Text &= "-"
                            Else
                                lblImage3.Text &= "Wait for approve"
                            End If
                        Else
                            '   lblAuthen.Text = "3rd Approval"
                        End If

                    Else
                        lblImage3.Text &= "-"

                    End If


                    If 14 > CInt(user_max_authen) Then
                        ' ========== Manager
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  14  AND ISNULL(is_educator,0) = 0 "
                        '   Response.Write(sql)
                        ds = conn.getDataSet(sql, "t2")
                        For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1
                            If ds.Tables("t2").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage2.Text &= "Approve"
                            ElseIf ds.Tables("t2").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage2.Text &= "Not approve"
                            ElseIf ds.Tables("t2").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage2.Text &= "N/A"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage2.Text &= "-"
                                Else
                                    lblImage2.Text &= "Wait for approve"
                                End If

                            End If

                        Next i

                        If ds.Tables("t2").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage2.Text &= "-"
                            Else
                                lblImage2.Text &= "Wait for approve"
                            End If
                        Else
                            '  lblAuthen.Text = "2nd Approval"
                        End If

                    Else
                        lblImage2.Text &= "-"

                    End If

                    ' ========== Sup
                    If 13 > CInt(user_max_authen) Then
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  13  AND ISNULL(is_educator,0) = 0 "
                        ' Response.Write(sql)
                        ds = conn.getDataSet(sql, "t1")


                        For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                            If ds.Tables("t1").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage1.Text &= "Approve"
                            ElseIf ds.Tables("t1").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage1.Text &= "Not approve"
                            ElseIf ds.Tables("t1").Rows(i)("comment_status_id").ToString = "3" Then
                                lblImage1.Text &= "N/A"
                            Else
                                lblImage1.Text &= "Wait for approve"
                            End If

                        Next i

                        If ds.Tables("t1").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage1.Text &= "-"
                            Else
                                lblImage1.Text &= "Wait for approve"
                            End If
                        Else
                            '   lblAuthen.Text = "1st Approval"
                        End If

                    Else
                        lblImage1.Text &= "-"

                    End If

                    Exit Do
                Loop



            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
