Imports ShareFunction

Partial Class srp_SRP_MasterPage
    Inherits System.Web.UI.MasterPage

    Protected priv_list() As String


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        priv_list = Session("priv_list")

        priv_list = Session("priv_list")

        If findArrayValue(priv_list, "402") = True Then ' Coordinator
            panel_hr.Visible = True
        Else
            panel_hr.Visible = False
        End If

        If findArrayValue(priv_list, "403") = True Then ' Dept
            panel_dept.Visible = True
        Else
            panel_dept.Visible = False
        End If

        lblInfo.Text = Session("user_fullname").ToString
        lblDeptName.Text = Session("dept_name").ToString
        lblCostcenter.Text = Session("costcenter_id").ToString
    End Sub

    Protected Sub linkLogout_Click(sender As Object, e As System.EventArgs) Handles linkLogout.Click
        Session.Abandon()
        '  Session.Abandon()
        Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        Response.Redirect("../login.aspx")
    End Sub
End Class

