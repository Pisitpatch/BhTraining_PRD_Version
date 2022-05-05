Imports ShareFunction

Partial Class ssip_SSIP_MasterPage
    Inherits System.Web.UI.MasterPage
    Protected priv_list() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        priv_list = Session("priv_list")
        ' Response.Write(priv_list(0))
        'priv_list = Session("priv_list")

        If findArrayValue(priv_list, "201") = True Then ' User
            panel_user.Visible = True
        Else
            panel_user.Visible = False
        End If

        If findArrayValue(priv_list, "202") = True Then ' Manager
            panel_manager.Visible = True
        Else
            panel_manager.Visible = False
        End If

        If findArrayValue(priv_list, "203") = True Then ' Commitee
            panel_commitee.Visible = True
        Else
            panel_commitee.Visible = False
        End If

        If findArrayValue(priv_list, "204") = True Then ' HR
            panel_hr.Visible = True
        Else
            panel_hr.Visible = False
        End If

        priv_list = Session("priv_list")
        ' Response.Write(priv_list(0))
        priv_list = Session("priv_list")

        lblInfo.Text = Session("user_fullname").ToString
        lblDeptName.Text = Session("dept_name").ToString
        lblCostcenter.Text = Session("costcenter_id").ToString
    End Sub

    Protected Sub linkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkLogout.Click
        Session.Abandon()
        Response.Redirect("../login.aspx")
    End Sub
End Class

