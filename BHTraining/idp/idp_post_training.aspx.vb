Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_idp_post_training
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub
End Class
