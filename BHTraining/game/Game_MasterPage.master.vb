Imports System.Data
Imports ShareFunction
Partial Class game_Game_MasterPage
    Inherits System.Web.UI.MasterPage
    Protected lang As String = "th"
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

     

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If IsPostBack Then

        Else ' First time load
            If IsNothing(Session("jci_emp_code")) Then
                lblInfo.Text = ""
                lblDeptName.Text = ""
                lblTitle.Text = ""
            Else
                lblInfo.Text = Session("jci_user_fullname").ToString
                lblDeptName.Text = Session("jci_dept_name").ToString
                lblTitle.Text = Session("jci_job_title").ToString
            End If
           

        End If

        If Request.QueryString("lang") = "" Then
            lang = "th"
        Else
            lang = Request.QueryString("lang")
        End If



        'Response.Write("M " & Session("lang").ToString & "<hr/>")
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            conn.closeSql()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cmdLogout_Click(sender As Object, e As System.EventArgs) Handles cmdLogout.Click
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub
End Class

