Imports ShareFunction

Partial Class idp_IDP2_MasterPage
    Inherits System.Web.UI.MasterPage
    Protected priv_list() As String
    Protected module_list() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        Try
            AddHandler SiteMap.SiteMapResolve, AddressOf Me.ExpandForumPaths
        Catch ex As Exception

        End Try

        priv_list = Session("priv_list")
        module_list = Session("mobule_list")

        If findArrayValue(priv_list, "12") = True Then
            panel_user.Visible = True
        Else
            panel_user.Visible = True
        End If

        If findArrayValue(priv_list, "13") = True Or findArrayValue(priv_list, "14") = True _
          Or findArrayValue(priv_list, "15") = True Or findArrayValue(priv_list, "16") = True Or findArrayValue(priv_list, "17") = True Then
            panel_manager.Visible = True
        Else
            panel_manager.Visible = False
        End If

        If findArrayValue(priv_list, "22") = True Then ' Traing
            panel_hr.Visible = True
            ' panel_educator.Visible = True
        Else
            panel_hr.Visible = False
            ' panel_educator.Visible = False
        End If

        If findArrayValue(priv_list, "19") = True Then
            ' panel_hr.Visible = True
            panel_educator.Visible = True
        Else
            'panel_hr.Visible = False
            panel_educator.Visible = False
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

 
    Private Function ExpandForumPaths(ByVal sender As Object, ByVal e As SiteMapResolveEventArgs) As SiteMapNode
        ' The current node represents a Post page in a bulletin board forum.
        ' Clone the current node and all of its relevant parents. This
        ' returns a site map node that a developer can then
        ' walk, modifying each node.Url property in turn.
        ' Since the cloned nodes are separate from the underlying
        ' site navigation structure, the fixups that are made do not
        ' effect the overall site navigation structure.
        ' Dim currentNode As SiteMapNode 

        Try
            Dim currentNode As SiteMapNode = SiteMap.CurrentNode.Clone(True)
            Dim tempNode As SiteMapNode = currentNode
            ' Obtain the recent IDs.
            Dim forumGroupID As Integer = 11
            Dim forumID As Integer = 2
            Dim postID As Integer = 1

            ' The current node, and its parents, can be modified to include
            ' dynamic querystring information relevant to the currently
            ' executing request.
            If Not (0 = postID) Then
                tempNode.Url = tempNode.Url & "?flag=ladder"
            End If

            tempNode = tempNode.ParentNode
            If Not (0 = forumID) And Not (tempNode Is Nothing) Then
                tempNode.Url = tempNode.Url & "?flag=ladder"
            End If

            tempNode = tempNode.ParentNode
            If Not (0 = forumGroupID) And Not (tempNode Is Nothing) Then
                tempNode.Url = tempNode.Url & "?flag=ladder"
            End If

            Return currentNode
        Catch ex As Exception

        End Try
      



    End Function
End Class

