Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_preview_ladder
    Inherits System.Web.UI.Page

    Protected id As String
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected lang As String = "th"
    Protected flag As String = "ladder"
    Protected total_require_score As Integer = 0
    Protected total_elective_score As Integer = 0

    Protected is_has_remark As Boolean = True
    Protected formtype As String = "" ' adjust or maintain

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        id = Request.QueryString("id")
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        bindIDPDetail()
        bindScaleForm()

        bindGridFunction()
        bindEducatorList()
        bindCommentList()
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


    Sub bindGridFunction()
        Dim sql As String
        Dim ds As New DataSet
        Dim flag As String
        Try
            If lang = "th" Then
                flag = "_th"
            Else
                flag = ""
            End If
            sql = "SELECT * , b.method_name" & flag & " AS methodLang FROM idp_function_tab a LEFT OUTER JOIN idp_m_method b ON a.method_id = b.method_id "
            sql &= " LEFT OUTER JOIN idp_m_topic c ON a.topic_id = c.topic_id "
            sql &= "  WHERE ipd_type_id = " & 1
           

            sql &= " AND a.idp_id = " & id

            sql &= " ORDER BY is_required DESC , order_sort ASC"
            '  Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

          
            ' Response.Write("xxx")
            GridFunction.DataSource = ds
            GridFunction.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindEducatorList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & id
            sql &= " AND ISNULL(is_educator,0) = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")

          

            GridEducator.DataSource = ds
            GridEducator.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCommentList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & id
            sql &= " AND ISNULL(is_educator,0) = 0 "
            ds = conn.getDataSetForTransaction(sql, "t1")


            GridComment.DataSource = ds
            GridComment.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindIDPDetail()
        Dim sql As String
        Dim ds As New DataSet
        Dim status_table As String = "idp_status_list"
        Dim h_date As String()
        Try
            If flag = "ladder" Then
                status_table = "idp_status_ladder"
            End If
            sql = "SELECT * , b.status_name AS idp_status_name FROM idp_trans_list a INNER JOIN " & status_table & " b ON a.status_id = b.idp_status_id "
            sql &= " INNER JOIN user_profile c ON a.report_emp_code = c.emp_code "
            sql &= " WHERE a.idp_id = " & id

            ds = conn.getDataSetForTransaction(sql, "t1")
            lblStatus.Text = ds.Tables(0).Rows(0)("idp_status_name").ToString

            lblrequest_NO.Text = ds.Tables(0).Rows(0)("idp_no").ToString

            If ds.Tables(0).Rows(0)("hire_date").ToString <> "" Then
                h_date = ds.Tables(0).Rows(0)("hire_date").ToString.Split(" ")
                lblHireDate.Text = h_date(0)
            End If

            lblempcode.Text = ds.Tables(0).Rows(0)("report_emp_code").ToString
            ' div_emp_code.InnerHtml = ds.Tables(0).Rows(0)("report_emp_code").ToString
            lblDept.Text = ds.Tables(0).Rows(0)("report_dept_name").ToString
            ' lblDivision.Text = ds.Tables(0).Rows(0)("report_jobtype").ToString ' replace by job_type

            lblname.Text = ds.Tables(0).Rows(0)("report_by").ToString
            lbljobtitle.Text = ds.Tables(0).Rows(0)("report_jobtitle").ToString
            lblCostcenter.Text = ds.Tables(0).Rows(0)("report_dept_id").ToString

            lblFormName.Text = ds.Tables(0).Rows(0)("ladder_template_name").ToString

            If flag = "ladder" Then
                lblScoreName.Text = ds.Tables(0).Rows(0)("ladder_template_name").ToString
                ' txtform.SelectedValue = ds.Tables(0).Rows(0)("ladder_template_id").ToString
                Try
                    '    txteducator_status.SelectedValue = ds.Tables(0).Rows(0)("educator_status_id").ToString
                Catch ex As Exception

                End Try

                ' txteducator_subject.SelectedValue = ds.Tables(0).Rows(0)("educator_subject_id").ToString
                ' txteducator_detail.Value = ds.Tables(0).Rows(0)("educator_comment_detail").ToString

                lblScoreRequire.Text = ds.Tables(0).Rows(0)("score_template_require").ToString
                lblScoreElective.Text = ds.Tables(0).Rows(0)("score_template_elective").ToString
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindScaleForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_trans_list WHERE idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtscale1.Text = ds.Tables("t1").Rows(0)("ladder_scale1").ToString
            txtscale2.Text = ds.Tables("t1").Rows(0)("ladder_scale2").ToString
            txtscale3.Text = ds.Tables("t1").Rows(0)("ladder_scale3").ToString
            txtscale4.Text = ds.Tables("t1").Rows(0)("ladder_scale4").ToString

            txttotal_scale.Text = ds.Tables("t1").Rows(0)("ladder_total_scale").ToString
            txtfullscore.Text = ds.Tables("t1").Rows(0)("ladder_full_score").ToString
            txtfullscore_percent.Text = ds.Tables("t1").Rows(0)("ladder_full_score_percent").ToString

            txtstatus_scale.SelectedValue = ds.Tables("t1").Rows(0)("ladder_scale_status_id").ToString
            lblstatus_scale.Text = txtstatus_scale.SelectedItem.Text
            txtscale_detail.Text = ds.Tables("t1").Rows(0)("ladder_scale_comment").ToString

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridFunction_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridFunction.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPk As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim lblFileList As Label = CType(e.Row.FindControl("lblFileList"), Label)
            Dim txtcat_id As Label = CType(e.Row.FindControl("txtcat_id"), Label)
            Dim lblRequire As Label = CType(e.Row.FindControl("lblRequire"), Label)
       
            Dim lblProgram As Label = CType(e.Row.FindControl("lblProgram"), Label)

            Dim lblDelete As Label = CType(e.Row.FindControl("lblDelete"), Label)
            Dim lblNewIDP As Label = CType(e.Row.FindControl("lblNewIDP"), Label)

            ' Dim lblH1 As Label = CType(e.Row.FindControl("lblH1"), Label)
            'Dim lblH2 As Label = CType(e.Row.FindControl("lblH2"), Label)
            Dim lblScore As Label = CType(e.Row.FindControl("lblScore"), Label)
            Dim lblArchieve As Label = CType(e.Row.FindControl("lblArchieve"), Label)
            Dim txtTime As Label = CType(e.Row.FindControl("txtTime"), Label)
            Dim lblLimit As Label = CType(e.Row.FindControl("lblLimit"), Label)


            Dim txtremark1 As Label = CType(e.Row.FindControl("txtremark1"), Label)
            Dim lblComment_th As Label = CType(e.Row.FindControl("lblComment_th"), Label)
            Dim lblComment_en As Label = CType(e.Row.FindControl("lblComment_en"), Label)

            Dim sql As String
            Dim ds As New DataSet

            Try
                lblArchieve.Text = "0"

                If lblDelete.Text = "1" Then
                    e.Row.Font.Strikeout = True
                    e.Row.ForeColor = Drawing.Color.Red
                End If

                If lblNewIDP.Text = "1" Then
                    e.Row.Font.Bold = True
                End If


                If flag <> "ladder" Then

                Else ' this is ladder ถ้าเป็น ladder
                    If lang = "th" Then
                        lblComment_th.Visible = True
                        lblComment_en.Visible = False
                    Else
                        lblComment_en.Visible = True
                        lblComment_th.Visible = False
                    End If
                    Try
                        If txtTime.Text = "" Then
                            txtTime.Text = "0"
                        End If
                        If txtTime.Text <> "" Then
                            If CInt(txtTime.Text) > CInt(lblLimit.Text) Then
                                lblArchieve.Text = CInt(lblScore.Text) * CInt(lblLimit.Text)
                            Else
                                lblArchieve.Text = CInt(lblScore.Text) * CInt(txtTime.Text)
                            End If

                        End If


                    Catch ex As Exception
                        lblArchieve.Text = "0"
                    End Try

                    If lblArchieve.Text = "" Then
                        lblArchieve.Text = "0"
                    End If

                    If txtremark1.Text = "" Then
                        is_has_remark = False
                    End If

                End If

             

                If lblRequire.Text = "0" Then
                    lblRequire.Text = "E"
                    total_elective_score += CInt(lblArchieve.Text)
                Else
                    lblRequire.Text = "R"
                    total_require_score += CInt(lblArchieve.Text)
                    e.Row.BackColor = Drawing.Color.LightBlue

                End If

                ' Response.Write(total_require_score & "<br/>")
                If flag = "ladder" Then
                    lblEmpScoreRequire.Text = total_require_score
                    lblEmpScoreElective.Text = total_elective_score
                End If

              

            Catch ex As Exception
                Response.Write("row bound : " & ex.Message & sql)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridFunction_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridFunction.SelectedIndexChanged

    End Sub

    Protected Sub GridComment_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridComment.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblStatusName As Label = CType(e.Row.FindControl("lblStatusName"), Label)
            Dim lblCommentStatusId As Label = CType(e.Row.FindControl("lblCommentStatusId"), Label)
            Dim lblEmpcode As Label = CType(e.Row.FindControl("lblEmpcode"), Label)
          

           

            If lblCommentStatusId.Text = "1" Then ' Approve
                lblStatusName.ForeColor = Drawing.Color.Green
                lblStatusName.Text = "<img src='../images/button_ok.png' id='img1' alt='approve' /> " & lblStatusName.Text
            ElseIf lblCommentStatusId.Text = "2" Then ' Reject
                lblStatusName.Text = "<img src='../images/button_cancel.png' id='img1' alt='approve' /> " & lblStatusName.Text
                lblStatusName.ForeColor = Drawing.Color.Red
            Else ' N/A
                lblStatusName.Text = "" & lblStatusName.Text
                lblStatusName.ForeColor = Drawing.Color.Red
            End If

        End If
    End Sub

    Protected Sub GridComment_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridComment.SelectedIndexChanged

    End Sub
End Class
