Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_preview_budget
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
End Class
