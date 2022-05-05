
Partial Class srp_srp_shop_picture
    Inherits System.Web.UI.Page

    Protected inv_id As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        inv_id = Request.QueryString("inv_id")

    End Sub
End Class
