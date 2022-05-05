Imports System.Data
Imports ShareFunction

Partial Class ssip_ssip_champion
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

        bindMessage("4", lblChampion)
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

    Sub bindMessage(ByVal pk As String, ByVal lb As Label)
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_message WHERE ssip_message_id = " & pk
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lb.Text = "<img src='../share/ssip/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' /><br/><br/>"
            End If
            lb.Text &= ds.Tables("t1").Rows(0)("message_detail").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
