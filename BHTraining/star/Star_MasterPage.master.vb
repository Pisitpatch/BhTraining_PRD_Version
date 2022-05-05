Imports ShareFunction
Partial Class star_Star_MasterPage
    Inherits System.Web.UI.MasterPage
    Protected priv_list() As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        priv_list = Session("priv_list")

        priv_list = Session("priv_list")

        If findArrayValue(priv_list, "302") = True Then ' Coordinator
            div_hr.Visible = True
        Else
            div_hr.Visible = False
        End If

        If findArrayValue(priv_list, "303") = True Then ' Dept
            div_dept.Visible = True
        Else
            div_dept.Visible = False
        End If

        If findArrayValue(priv_list, "304") = True Then ' Committee
            div_committee.Visible = True
        Else
            div_committee.Visible = False
        End If

        If findArrayValue(priv_list, "305") = True Or findArrayValue(priv_list, "13") = True Or findArrayValue(priv_list, "14") = True Or findArrayValue(priv_list, "15") = True Or findArrayValue(priv_list, "16") = True Or findArrayValue(priv_list, "17") = True Then ' Committee
            div_manager.Visible = True
        Else
            div_manager.Visible = False
        End If

        lblInfo.Text = Session("user_fullname").ToString
        lblDeptName.Text = Session("dept_name").ToString
        lblCostcenter.Text = Session("costcenter_id").ToString
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

    End Sub

    Protected Sub linkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkLogout.Click
        Session.Abandon()
        Response.Redirect("../login.aspx")
    End Sub
End Class

