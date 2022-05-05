Imports ShareFunction

Partial Class menu
    Inherits System.Web.UI.Page
    Protected module_list() As String
    Protected priv_list() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        If IsNothing(Session("mobule_list")) Then
            Response.Redirect("login.aspx")
        End If
        ' Response.Write(Session.Count)

        module_list = Session("mobule_list")
        ' Response.Write(priv_list(0))
        If findArrayValue(module_list, "1") = False Then
            '  menuIR.HRef = "#"
            '  menuIR.InnerHtml = ""
        End If

        If findArrayValue(module_list, "2") = False Then
            '  menuIDP.HRef = "#"
            ' menuIDP.InnerHtml = ""
        End If

        If findArrayValue(module_list, "3") = False Then
            '  menuSSIP.HRef = "#"
            '  menuSSIP.InnerHtml = ""
        End If

        If findArrayValue(module_list, "4") = False Then
            '  menuCFB.HRef = "#"
            '   menuCFB.InnerHtml = ""
        End If

        priv_list = Session("priv_list")
        If findArrayValue(priv_list, "1") = True Then

        Else
            ' menuFTE.HRef = "#"
            'menuFTE.InnerHtml = ""

            menuUser.HRef = "#"
            menuUser.InnerHtml = ""

            menuDoctor.HRef = "#"
            menuDoctor.InnerHtml = ""

            menuDept.HRef = "#"
            menuDept.InnerHtml = ""

            menuDivision.HRef = "#"
            menuDivision.InnerHtml = ""

         
        End If
    End Sub
End Class
