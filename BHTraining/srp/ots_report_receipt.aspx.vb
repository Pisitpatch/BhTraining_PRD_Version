Imports System.Data
Imports ShareFunction

Partial Class srp_ots_report_receipt
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected trans_id As String = ""
    Protected dsInvoice As New DataSet
    Protected dsHeader As New DataSet
    Protected line As Integer = 20

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      

        trans_id = Request.QueryString("trans_id")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Page.IsPostBack Then
        Else ' First time load
          
        End If

        bindHeader()
        lblPrintDate.Text = FormatDateTime(Date.Now, DateFormat.GeneralDate)
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing
            dsHeader.Dispose()
            dsInvoice.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindHeader()
        Dim sql As String = ""

        Try
            sql = "SELECT * FROM shop_sale_master a  "
            sql &= " WHERE a.trans_id = " & trans_id

            dsHeader = conn.getDataSetForTransaction(sql, "t1")

            If dsHeader.Tables("t1").Rows.Count = 0 Then
                Response.End()
            Else
                lblDate.Text = ConvertTSToDateString(Date.Now.Ticks)
                lblName.Text = dsHeader.Tables("t1").Rows(0)("customer_name").ToString
                lblID.Text = dsHeader.Tables("t1").Rows(0)("customer_emp_code").ToString
                lblRef.Text = dsHeader.Tables("t1").Rows(0)("invoice_no").ToString
                lblDept.Text = dsHeader.Tables("t1").Rows(0)("customer_dept_name").ToString
                bindInvoice()
                getPointRemain()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindInvoice()
        Dim sql As String = ""
        Dim point_use As Integer = 0
        Dim baht_use As Double = 0
        Dim totalCard As Integer = 0
        Dim resultCard As Integer = 0

        Try
            sql = "SELECT * FROM shop_sale_master a LEFT OUTER JOIN shop_item_sale b ON a.trans_id = b.trans_id "
            sql &= " WHERE a.trans_id = " & trans_id

            dsInvoice = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To dsInvoice.Tables("t1").Rows.Count - 1
                If dsInvoice.Tables("t1").Rows(i)("item_point_used").ToString <> "" Then
                    point_use += CInt(dsInvoice.Tables("t1").Rows(i)("item_point_used").ToString)
                End If

                If dsInvoice.Tables("t1").Rows(i)("item_baht_used").ToString <> "" Then
                    baht_use += CDbl(dsInvoice.Tables("t1").Rows(i)("item_baht_used").ToString)
                End If

                If Integer.TryParse(dsInvoice.Tables("t1").Rows(i)("register_card_id").ToString, resultCard) Then
                    If resultCard > 0 Then
                        totalCard += 1
                    End If

                End If
            Next i

            lblpoint_use.Text = FormatNumber(point_use, 0)
            lblTotalPoint.Text = FormatNumber(point_use, 0)
            lblTotalAmount.Text = FormatNumber(baht_use, 2)
            lblCard.Text = totalCard
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub getPointRemain()
        Dim sql As String = ""
        Dim ds As New DataSet
        Dim result As Integer = 0
        Try
            If lblID.Text = "" Or lblID.Text = 0 Then

                Return
            End If

            sql = "SELECT SUM(point_trans) AS point_num FROM srp_point_movement WHERE emp_code = " & lblID.Text
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                If Int32.TryParse(ds.Tables("t1").Rows(0)("point_num").ToString, result) Then
                    lblpoint_remain.Text = result
                Else
                    Return
                End If

                If Int32.TryParse(lblpoint_use.Text, result) Then

                    lblpoint_available.Text = CInt(ds.Tables("t1").Rows(0)("point_num").ToString) - result
                Else
                    Return
                End If


            Else
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
