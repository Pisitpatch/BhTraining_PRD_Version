Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_preview_actual_expense
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
        getTotalApproveBudget()
        bindExpense2()
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

    Sub getTotalApproveBudget()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT SUM(expense_value) FROM idp_training_expense a INNER JOIN idp_budget_request b ON a.expense_request_type_id = b.request_id WHERE a.idp_id = " & id
            sql &= " AND b.is_request_budget = 1 AND accouting_type = 1 "
            sql &= " GROUP BY a.idp_id "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                lblApproveBudget.Text = ds.Tables(0).Rows(0)(0).ToString
            Else
                lblApproveBudget.Text = 0
            End If


        Catch ex As Exception

            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDetail()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_external_req a INNER JOIN idp_trans_list b ON a.idp_id = b.idp_id WHERE a.idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")


            lblempcode.Text = ds.Tables(0).Rows(0)("report_emp_code").ToString
            lblDept.Text = ds.Tables(0).Rows(0)("report_dept_name").ToString
            ' lblDivision.Text = ds.Tables(0).Rows(0)("report_jobtype").ToString ' replace by job_type

            lblname.Text = ds.Tables(0).Rows(0)("report_by").ToString
            lbljobtitle.Text = ds.Tables(0).Rows(0)("report_jobtitle").ToString
            lblCostcenter.Text = ds.Tables(0).Rows(0)("report_dept_id").ToString
            lblrequest_NO.Text = ds.Tables("t1").Rows(0)("idp_no").ToString


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

    Protected Sub GridExpense2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridExpense2.SelectedIndexChanged

    End Sub
End Class
