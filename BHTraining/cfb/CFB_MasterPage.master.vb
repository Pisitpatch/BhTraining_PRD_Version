Imports ShareFunction
Namespace cfb
    Partial Class cfb_CFB_MasterPage
        Inherits System.Web.UI.MasterPage
        Protected priv_list() As String

        Protected Sub linkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkLogout.Click
            Session.Abandon()
            Response.Redirect("../login.aspx")
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If IsNothing(Session("session_myid")) Then
                Response.Redirect("../login.aspx")
            Else
                priv_list = Session("priv_list")
                ' Response.Write(priv_list(0))
                priv_list = Session("priv_list")
                ' Response.Write(priv_list(0))
                If findArrayValue(priv_list, "7") = True Then
                    panel_user.Visible = True
                Else
                    panel_user.Visible = False
                End If

                If findArrayValue(priv_list, "8") = True Then
                    panel_dept.Visible = True
                Else
                    panel_dept.Visible = False
                End If

                If findArrayValue(priv_list, "9") = True Then
                    panel_tqm.Visible = True
                    panel_admin.Visible = True
                Else
                    panel_tqm.Visible = False
                    panel_admin.Visible = False
                End If

                If findArrayValue(priv_list, "10") = True Then
                    panel_psm.Visible = True
                Else
                    panel_psm.Visible = False
                End If

                If findArrayValue(priv_list, "11") = True Then
                    panel_ha.Visible = True
                Else
                    panel_ha.Visible = False
                End If

                If findArrayValue(priv_list, "1002") = True Then
                    panel_logbook.Visible = True
                Else
                    panel_logbook.Visible = False
                End If

                lblInfo.Text = Session("user_fullname").ToString
                lblDeptName.Text = Session("dept_name").ToString
                lblCostcenter.Text = Session("costcenter_id").ToString
            End If
        End Sub
    End Class

End Namespace

