Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_preview_ext_training
    Inherits System.Web.UI.Page

    Protected id As String
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected priceTotal As Decimal = 0
    Protected budgetTotal As Decimal = 0
    Protected priceTotal2 As Decimal = 0
    Protected budgetTotal2 As Decimal = 0
    Protected returnTotal As Decimal = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        id = Request.QueryString("id")
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        bindDetail()
        bindExpense()
        bindExpense2()
        getTotalApproveBudget()
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

    Sub bindDetail()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_external_req a INNER JOIN idp_trans_list b ON a.idp_id = b.idp_id WHERE a.idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblrequest_NO.Text = ds.Tables("t1").Rows(0)("idp_no").ToString
            'txtstatus.SelectedValue = ds.Tables("t1").Rows(0)("status_id").ToString
            ' txtrelate_idpno.SelectedValue = ds.Tables("t1").Rows(0)("relate_idp_no").ToString
            txttitle.Text = ds.Tables("t1").Rows(0)("ext_title").ToString
            txtcourse_detail.Text = ds.Tables("t1").Rows(0)("course_outline").ToString
            txtexect_detail.Text = ds.Tables("t1").Rows(0)("expect_detail").ToString
            txtfacility.Text = ds.Tables("t1").Rows(0)("facility").ToString
            txtinstitution.Text = ds.Tables("t1").Rows(0)("institution").ToString
            txtplace.Text = ds.Tables("t1").Rows(0)("place").ToString

            If ds.Tables("t1").Rows(0)("ext_type_id").ToString <> "0" Then
                txttype.Text = ds.Tables("t1").Rows(0)("ext_type_name").ToString
            Else
                txttype.Text = "-"
            End If

            If ds.Tables("t1").Rows(0)("training_type_id").ToString <> "0" Then
                txttraintype.Text = ds.Tables("t1").Rows(0)("training_type_name").ToString
            Else
                txttraintype.Text = "-"
            End If


            txtdate1.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_start_ts").ToString)
            txtdate2.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("date_end_ts").ToString)
            '  txthour1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_start_ts").ToString, "hour")
            '  txtmin1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_start_ts").ToString, "min")
            '  txthour2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_end_ts").ToString, "hour")
            ' txtmin2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("date_end_ts").ToString, "min")

            ' txtladder1.SelectedValue = ds.Tables("t1").Rows(0)("chk_attend2_ladder_before").ToString
            ' txtladder2.SelectedValue = ds.Tables("t1").Rows(0)("chk_attend2_ladder_after").ToString
            'txtattend_remark.Value = ds.Tables("t1").Rows(0)("chk_attend7_remark").ToString


            If ds.Tables("t1").Rows(0)("chk_attend1").ToString = "1" Then
                ' CType(panel_detail.FindControl("chk_attend" & i), HtmlInputCheckBox).Checked = True
                txtmotivation.Text &= "- หัวข้อนี้เป็นหัวข้อที่ระบุอยู่ในแผนพัฒนาตนเองของฉัน" & "<br/>"
            End If
            If ds.Tables("t1").Rows(0)("chk_attend" & 2).ToString = "1" Then
                txtmotivation.Text &= "- เพื่อให้ตนเองสามารถปรับระดับ Career Ladder" & "<br/>"
            End If
            If ds.Tables("t1").Rows(0)("chk_attend" & 3).ToString = "1" Then
                txtmotivation.Text &= "- การพัฒนาในหัวข้อนี้ ฉันได้รับคำแนะนำจาก หัวหน้างานของฉัน" & "<br/>"
            End If
            If ds.Tables("t1").Rows(0)("chk_attend" & 4).ToString = "1" Then
                txtmotivation.Text &= "- การพัฒนาในหัวข้อนี้ ฉันได้จาการประเมินความสามารถของตัวฉันเอง" & "<br/>"
            End If
            If ds.Tables("t1").Rows(0)("chk_attend" & 5).ToString = "1" Then
                txtmotivation.Text &= "- ฉันต้องการเตรียมพร้อมตัวเองเพื่ออนาคตในหน้าที่การงานของตัวเองด้วยหัวข้อนี้" & "<br/>"
            End If
            If ds.Tables("t1").Rows(0)("chk_attend" & 6).ToString = "1" Then
                txtmotivation.Text &= "- องค์กรของฉันเล็งเห็นว่าหัวข้อนี้เป็นเรื่องที่สำคัญ" & "<br/>"
            End If
            If ds.Tables("t1").Rows(0)("chk_attend" & 7).ToString = "1" Then
                txtmotivation.Text &= "- อื่นๆ" & "<br/>"
            End If

            If ds.Tables("t1").Rows(0)("is_budget_request").ToString = "1" Then
                txtrequestBudget.Text = "ต้องการเบิกค่าใช้จ่ายจากโรงพยาบาล"
            Else
                txtrequestBudget.Text = "ไม่ต้องการเบิกค่าใช้จ่ายจากโรงพยาบาล"
            End If

            If ds.Tables("t1").Rows(0)("register_type").ToString = "1" Then
                txtregisterType.Text = "ดำเนินการสมัครด้วยตนเองแล้ว"
            ElseIf ds.Tables("t1").Rows(0)("register_type").ToString = "2" Then
                txtregisterType.Text = "กรอกใบสมัครและส่งให้แผนกฝึกอบรมแล้ว ต้องการให้แผนกฝึกอบรมสมัครให้ภายในวันที่ " & ConvertTSToDateString(ds.Tables("t1").Rows(0)("register_type_reserve_date_ts").ToString)
            ElseIf ds.Tables("t1").Rows(0)("register_type").ToString = "3" Then
                txtregisterType.Text = "ให้แผนกฝึกอบรมสมัครทางออนไลน์ ภายในวันที่ " & ConvertTSToDateString(ds.Tables("t1").Rows(0)("register_type_reserve_date_ts2").ToString)
            End If
         

            '   txtdate_register.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("register_type_reserve_date_ts").ToString)
            '  txtdate_register2.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("register_type_reserve_date_ts2").ToString)



            lblempcode.Text = ds.Tables(0).Rows(0)("report_emp_code").ToString
            lblDept.Text = ds.Tables(0).Rows(0)("report_dept_name").ToString
            ' lblDivision.Text = ds.Tables(0).Rows(0)("report_jobtype").ToString ' replace by job_type

            lblname.Text = ds.Tables(0).Rows(0)("report_by").ToString
            lbljobtitle.Text = ds.Tables(0).Rows(0)("report_jobtitle").ToString
            lblCostcenter.Text = ds.Tables(0).Rows(0)("report_dept_id").ToString


            txthour.Text = ds.Tables(0).Rows(0)("train_hour").ToString
            txtinstitution.Text = ds.Tables(0).Rows(0)("institution").ToString

            If ds.Tables(0).Rows(0)("budget_update_by").ToString = "" Then
                lblApproveName.Text = "-"
            Else
                lblApproveName.Text = ds.Tables(0).Rows(0)("budget_update_by").ToString & " " & ds.Tables(0).Rows(0)("budget_last_update").ToString
            End If

            txtcategory.Text = ds.Tables(0).Rows(0)("ext_category_name").ToString

        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindExpense()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_expense a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE a.accouting_type = 1 "
            sql &= " AND a.idp_id = '" & id & "' "
            sql &= " ORDER BY a.order_sort "
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)


            GridExpense.DataSource = ds
            GridExpense.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridExpense_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridExpense.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblSponsor As Label = CType(e.Row.FindControl("lblSponsor"), Label)
            Dim lblExchange As Label = CType(e.Row.FindControl("lblExchange"), Label)
            Dim lblValue As Label = CType(e.Row.FindControl("lblValue"), Label)
            Dim lblcurtypeid As Label = CType(e.Row.FindControl("lblcurtypeid"), Label)
            Dim lblcurtype As Label = CType(e.Row.FindControl("lblcurtype"), Label)
            '  Dim chk_exchange As CheckBox = CType(e.Row.FindControl("chk_exchange"), CheckBox)
            Dim lblExpensetypeID As Label = CType(e.Row.FindControl("lblExpensetypeID"), Label)
            Dim lblReqBudget As Label = CType(e.Row.FindControl("lblReqBudget"), Label)
            Dim lblConvertToBaht As Label = CType(e.Row.FindControl("lblConvertToBaht"), Label)

            Dim sql As String
            Dim ds As New DataSet

            Try

                If lblReqBudget.Text = "1" Then ' is_request_budget = 1
                    ' e.Row.BackColor = Drawing.Color.LightYellow
                    If lblcurtypeid.Text = "1" Then ' Baht
                        'lblExchange.Text = "-"
                        budgetTotal += CDbl(lblValue.Text)
                    Else
                        If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                            lblExchange.Text = "1"
                        End If

                        budgetTotal += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))

                        ' lblExchange.Text = "1 Baht = " & lblExchange.Text & " " & lblcurtype.Text
                    End If
                End If

                If lblcurtypeid.Text = "1" Then ' Baht
                    lblExchange.Text = "-"
                    priceTotal += CDbl(lblValue.Text)
                Else
                    If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                        lblExchange.Text = "1"
                    End If

                    priceTotal += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))
                    lblConvertToBaht.Text = "<br/>" & FormatNumber((CDbl(lblValue.Text) * CDbl(lblExchange.Text)), 2) & " Baht"

                    lblExchange.Text = "1 Baht = " & lblExchange.Text & " " & lblcurtype.Text
                    ' lblExchange.Text &= "<br/>"

                End If

                txttotal.Text = FormatNumber(priceTotal)
                txtrequest_budget.Text = FormatNumber(budgetTotal)
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridExpense_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridExpense.SelectedIndexChanged

    End Sub

    Sub getTotalApproveBudget()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT SUM(expense_value) FROM idp_training_expense a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE a.idp_id = " & id
            sql &= " AND b.is_request_budget = 1 AND accouting_type = 1 "
            sql &= " GROUP BY a.idp_id "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                If lblApproveName.Text = "-" Then
                    lblApproveBudget.Text = "0"
                Else
                    lblApproveBudget.Text = ds.Tables(0).Rows(0)(0).ToString
                End If

            Else
                lblApproveBudget.Text = 0
            End If


        Catch ex As Exception

            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindExpense2()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_expense  a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE  a.accouting_type = 2 "
            sql &= " AND a.idp_id = '" & id & "' "

            sql &= " ORDER BY a.order_sort "
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count = 0 Then
                '  cmdSaveOrder2.Visible = False
                ' cmdDelExpense.Visible = False
            End If

            GridExpense2.DataSource = ds
            GridExpense2.DataBind()


            If CDbl(txtactual_expense.Text) > CDbl(lblApproveBudget.Text) Then
                txtactual_expense.ForeColor = Drawing.Color.Red
            Else
                txtactual_expense.ForeColor = Drawing.Color.Green
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridExpense2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridExpense2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblExchange As Label = CType(e.Row.FindControl("lblExchange"), Label)
            Dim lblValue As Label = CType(e.Row.FindControl("lblValue"), Label)
            Dim lblcurtypeid As Label = CType(e.Row.FindControl("lblcurtypeid"), Label)
            Dim lblcurtype As Label = CType(e.Row.FindControl("lblcurtype"), Label)
            Dim lblExpensetypeID As Label = CType(e.Row.FindControl("lblExpensetypeID"), Label)
            Dim lblReqBudget As Label = CType(e.Row.FindControl("lblReqBudget"), Label)
            Dim lblConvertToBaht As Label = CType(e.Row.FindControl("lblConvertToBaht"), Label)

            Dim txttype As Label = CType(e.Row.FindControl("txttype"), Label)
            Dim txtpayment As Label = CType(e.Row.FindControl("txtpayment"), Label)
            Dim lbltype_id As Label = CType(e.Row.FindControl("lbltype_id"), Label)
            Dim lblpayment_id As Label = CType(e.Row.FindControl("lblpayment_id"), Label)

            Dim lblTopicID As Label = CType(e.Row.FindControl("lblTopicID"), Label)
            Dim lblApprove As Label = CType(e.Row.FindControl("lblApprove"), Label)
            Dim lblDelete As Label = CType(e.Row.FindControl("lblDelete"), Label)
            '  Dim chk_exchange As CheckBox = CType(e.Row.FindControl("chk_exchange"), CheckBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                If lblDelete.Text = "1" Then
                    e.Row.Font.Strikeout = True
                    txttype.Enabled = False
                    txtpayment.Enabled = False
                End If


                sql = "SELECT SUM(expense_value) FROM idp_training_expense a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE a.idp_id = " & id
                sql &= " AND b.is_request_budget = 1 AND a.accouting_type = 1 AND a.expense_topic_id = " & lblExpensetypeID.Text
                sql &= " GROUP BY a.expense_topic_id "
                ' Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    lblApprove.Text = FormatNumber(CDbl(ds.Tables("t1").Rows(0)(0).ToString), 2)
                End If


                If lblReqBudget.Text = "1" And lblDelete.Text <> "1" Then ' ชำระเงิน , เบิกคืน
                    ' e.Row.BackColor = Drawing.Color.LightYellow
                    If lblcurtypeid.Text = "1" Then ' Baht
                        'lblExchange.Text = "-"
                        budgetTotal2 += CDbl(lblValue.Text)
                    Else
                        If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                            lblExchange.Text = "1"
                        End If

                        budgetTotal2 += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))

                        ' lblExchange.Text = "1 BAHT = " & lblExchange.Text & " " & lblcurtype.Text
                    End If
                ElseIf lblReqBudget.Text = "0" And lblDelete.Text <> "1" Then ' รับคืน

                    If lblcurtypeid.Text = "1" Then ' Baht
                        'lblExchange.Text = "-"
                        returnTotal += CDbl(lblValue.Text)
                    Else
                        If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                            lblExchange.Text = "1"
                        End If

                        returnTotal += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))

                        ' lblExchange.Text = "1 BAHT = " & lblExchange.Text & " " & lblcurtype.Text
                    End If

                End If

                If lblcurtypeid.Text = "1" Then ' Baht
                    lblExchange.Text = "-"
                    priceTotal2 += CDbl(lblValue.Text)
                Else
                    If lblExchange.Text = "" Or lblExchange.Text = "0" Then
                        lblExchange.Text = "1"
                    End If

                    lblConvertToBaht.Text = "<br/>" & FormatNumber((CDbl(lblValue.Text) * CDbl(lblExchange.Text)), 2) & " Baht"
                    priceTotal2 += (CDbl(lblValue.Text) * CDbl(lblExchange.Text))

                    lblExchange.Text = "1 " & lblcurtype.Text & " = " & lblExchange.Text & " BAHT"
                End If




                txtactual_expense.Text = FormatNumber(budgetTotal2)
                lblReturnBudget.Text = FormatNumber(returnTotal)
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Sub bindCommentList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")


            GridComment.DataSource = ds
            GridComment.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridExpense2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridExpense2.SelectedIndexChanged

    End Sub
End Class
