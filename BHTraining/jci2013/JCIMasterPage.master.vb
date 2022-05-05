
Partial Class jci2013_JCIMasterPage
    Inherits System.Web.UI.MasterPage
    Dim menu As String = ""
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("login.aspx")
            Response.End()
        End If

        menu = Request.QueryString("menu")

        'Session("menu") = menu
        If menu = "1" Then
            HyperLink1.BackColor = Drawing.Color.DarkGreen
        ElseIf menu = "2" Then
            HyperLink2.BackColor = Drawing.Color.DarkGreen
        ElseIf menu = "3" Then
            HyperLink3.BackColor = Drawing.Color.DarkGreen
        ElseIf menu = "4" Then
            HyperLink4.BackColor = Drawing.Color.DarkGreen
        ElseIf menu = "5" Then
            HyperLink5.BackColor = Drawing.Color.DarkGreen
        End If

        If Not IsNothing(Session("user_fullname")) Then
            lblInfo.Text = Session("user_fullname").ToString
            lblDeptName.Text = Session("dept_name").ToString
            LinkButton1.Visible = True
            div_mainmenu.Visible = True
        Else
            div_mainmenu.Visible = False
            LinkButton1.Visible = False
        End If
       

    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub
End Class

