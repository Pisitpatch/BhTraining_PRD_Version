Imports ShareFunction

Partial Class idp_IDP_MasterPage
    Inherits System.Web.UI.MasterPage

    Protected priv_list() As String
    Protected module_list() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        priv_list = Session("priv_list")
        module_list = Session("mobule_list")

        If findArrayValue(module_list, "2") = False Then
            Response.Redirect("../menu.aspx")
            Response.End()
        End If
        ' Response.Write(priv_list(0))
        'priv_list = Session("priv_list")

        If findArrayValue(priv_list, "12") = True Then
            panel_user.Visible = True
        Else
            panel_user.Visible = True
        End If

        If findArrayValue(priv_list, "13") = True Or findArrayValue(priv_list, "14") = True _
          Or findArrayValue(priv_list, "15") = True Or findArrayValue(priv_list, "16") = True Or findArrayValue(priv_list, "17") = True Or findArrayValue(priv_list, "23") = True Then
            panel_manager.Visible = True
        Else
            panel_manager.Visible = False
        End If

        If findArrayValue(priv_list, "18") = True Then ' Traing
            panel_hr.Visible = True
            ' panel_educator.Visible = True
        Else
            panel_hr.Visible = False
            ' panel_educator.Visible = False
        End If

        If findArrayValue(priv_list, "19") = True Or findArrayValue(priv_list, "14") = True Then
            ' panel_hr.Visible = True
            panel_educator.Visible = True
        Else
            'panel_hr.Visible = False
            panel_educator.Visible = False
        End If

        If findArrayValue(priv_list, "20") = True Or findArrayValue(priv_list, "21") = True Then
            ' panel_hr.Visible = True
            panel_account.Visible = True

            If findArrayValue(priv_list, "20") = True Then
                li_budget.Visible = True
            Else
                li_budget.Visible = False
            End If

            If findArrayValue(priv_list, "21") = True Then
                li_expense.Visible = True
            Else
                li_expense.Visible = False
            End If
        Else
            'panel_hr.Visible = False
            panel_account.Visible = False
        End If

        lblInfo.Text = Session("user_fullname").ToString
        lblDeptName.Text = Session("dept_name").ToString
        lblCostcenter.Text = Session("costcenter_id").ToString
    End Sub

    Protected Sub linkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkLogout.Click
        Session.Abandon()
        '  Session.Abandon()
        Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        Response.Redirect("../login.aspx")
    End Sub
End Class

