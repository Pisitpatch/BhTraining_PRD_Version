Imports System.Data
Imports ShareFunction

Partial Class srp_srp_shop
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT inv_item_id , ISNULL(total_point,0) AS total_point , ISNULL(inv_price,0) AS total_price , ISNULL(reserve_num,0) AS reserve_num , ISNULL(on_hand,0) AS on_hand , picture_binary , inv_item_name_en , inv_item_name_th , inv_remark , CASE WHEN is_voucher = 1 THEN '' ELSE CASE WHEN ISNULL(inv_price,0) = 0 THEN '' ELSE 'คำนวณคะแนน' END END AS is_voucher FROM shop_master_item WHERE ISNULL(is_delete,0) = 0 AND is_active = 1 "
            If txtname.Text <> "" Then
                sql &= " AND (inv_item_name_en LIKE '%" & txtname.Text & "%' OR inv_item_name_th LIKE '%" & txtname.Text & "%') "
            End If

            If txttype.SelectedValue <> "0" Then
                sql &= " AND inv_item_code LIKE '" & txttype.SelectedValue & "%' "
            End If

            sql &= " ORDER BY item_update_date_ts DESC"

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            PicturesListView.DataSource = ds
            PicturesListView.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub PicturesListView_PagePropertiesChanging(sender As Object, e As System.Web.UI.WebControls.PagePropertiesChangingEventArgs) Handles PicturesListView.PagePropertiesChanging
        DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
        bindGrid()
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub
End Class
