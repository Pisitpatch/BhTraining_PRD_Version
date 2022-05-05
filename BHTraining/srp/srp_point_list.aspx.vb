Imports System.Data
Imports ShareFunction

Partial Class srp_srp_point_list
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If


        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Page.IsPostBack Then
        Else ' First time load
            lblName.Text = Session("user_fullname").ToString
            bindGrid()
            bindGridRedeem()

          
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , CASE WHEN movememt_type_id = 1 THEN 'Register Card : ' + CAST(card_id AS varchar) WHEN movememt_type_id = 3 THEN 'แลกซื้อของ' ELSE '-' END AS move_name  "
            sql &= " FROM srp_point_movement WHERE emp_code =  " & Session("emp_code").ToString
            sql &= " ORDER BY movement_create_date_ts DESC "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            gridview1.DataSource = ds
            gridview1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub bindGridRedeem()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = " SELECT b.customer_emp_code AS 'Employee ID' , b.customer_name AS 'Name' , b.customer_dept_name AS 'Department'"
            sql &= " , CASE WHEN b.customer_type = 1 THEN 'Employee' WHEN b.customer_type = 2 THEN 'Department' ELSE 'Annonymous' END as 'Type'"
            sql &= " , a.sale_item_name_th AS 'Item Name' , a.sale_item_code AS 'Item Code' , a.sale_qty AS 'Qty' "
            sql &= " , a.item_baht_used AS 'Cash' , a.item_point_used AS 'Point'"
            'sql &= " , b.pay_type_name AS 'Payment Type' "
            sql &= " , CASE WHEN pay_type_id = 2 THEN a.item_baht_used ELSE 0 END AS 'Credit' "
            sql &= " , CASE WHEN a.item_baht_used > 0 AND a.item_point_used > 0 AND pay_type_id = 1 THEN 'Point & Cash' WHEN a.item_baht_used > 0 AND a.item_point_used = 0 AND pay_type_id = 1 THEN 'Cash' "
            sql &= " WHEN a.item_baht_used = 0 AND a.item_point_used > 0 AND pay_type_id = 1 THEN 'Point'"
            sql &= " WHEN a.register_card_id > 0  AND pay_type_id = 1 THEN 'Card'"
            sql &= " WHEN pay_type_id = 2 THEN 'Credit'"
            sql &= " ELSE 'Unknown' END AS 'Payment Type' "

            sql &= " , CASE WHEN ISNULL(a.register_card_id,0) > 0 THEN 'Yes' Else 'No' END AS 'OTS Card' "
            sql &= " , CASE WHEN ISNULL(a.register_card_id,0) > 0 THEN a.register_card_id WHEN ISNULL(a.register_card_id,0) = 0 THEN '0'  ELSE '0' END AS 'Card No.'"
            sql &= " , b.sale_remark AS 'Sale Remark' "
            sql &= " , a.create_item_by AS 'Create by' , CONVERT(VARCHAR(10),a.create_date_raw,101)  AS 'Create Date' , CONVERT(VARCHAR(10),a.create_date_raw,108) AS 'Time' "
            sql &= ", reward_by AS 'Reward By' , reward_position AS 'Position' "
            sql &= " FROM shop_item_sale a INNER JOIN shop_sale_master b ON a.trans_id = b.trans_id"
            sql &= " LEFT OUTER JOIN (SELECT a1.card_id , MAX(a2.user_fullname) AS reward_by , MAX(a2.job_title) AS reward_position FROM srp_point_movement a1 INNER JOIN user_profile a2 ON a1.r_award_by_emp_code = a2.emp_code WHERE ISNULL(a1.card_id,0) > 0 GROUP BY a1.card_id , a2.user_fullname , a2.job_title ) c "
            sql &= " ON a.register_card_id = c.card_id "
            sql &= " WHERE ISNULL(is_refund,0) = 0  AND b.customer_emp_code = " & Session("emp_code").ToString
            sql &= " ORDER BY a.sale_id DESC"

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            gridview2.DataSource = ds
            gridview2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub gridview1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview1.PageIndexChanging
        gridview1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub gridview1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridview1.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lblTotalScore As Label = CType(e.Row.FindControl("lblTotalScore"), Label)
            Dim sql As String = ""
            Dim errorMsg As String = ""
            Dim ds As New DataSet
            sql = "SELECT SUM(point_trans) FROM srp_point_movement WHERE emp_code =  " & Session("emp_code").ToString
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                lblTotalScore.Text = ds.Tables("t1").Rows(0)(0).ToString
                lblSumScore.Text = ds.Tables("t1").Rows(0)(0).ToString
            Else
                lblTotalScore.Text = 0
                lblSumScore.Text = 0
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblMovementID As Label = CType(e.Row.FindControl("lblMovementID"), Label)
            Dim lblScore As Label = CType(e.Row.FindControl("lblScore"), Label)
            If lblMovementID.Text = "3" Then
                lblScore.ForeColor = Drawing.Color.Red

            End If
        End If
    End Sub

    Protected Sub gridview1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview1.SelectedIndexChanged

    End Sub
    Protected Sub gridview2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridview2.PageIndexChanging

        gridview2.PageIndex = e.NewPageIndex
        bindGridRedeem()
    End Sub
End Class
