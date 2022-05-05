
Partial Class game_popup_right_choice
    Inherits System.Web.UI.Page

    Protected order As String = "0" ' answer order
    Protected q_order As String = "0" ' question order
    Protected gid As String = ""
    Protected lang As String = "th"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        order = CInt(Request.QueryString("order"))
        q_order = CInt(Request.QueryString("q_order"))
        gid = Request.QueryString("gid")
        lang = Request.QueryString("lang")

        If lang = "" Then
            lang = "th"
        End If

        If gid = "" Then
            Response.Write("Please select group")
            Return
        End If


    End Sub
End Class
