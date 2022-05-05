Imports ShareFunction
Imports System.Data
Partial Class idp_idp_message2
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String
    Protected is_delay As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        id = Request.QueryString("id")

        If Not Page.IsPostBack Then ' first time
            bindForm()

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

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_message WHERE idp_message_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPicture.text = "<img src='../share/idp/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' /><br/><br/>"
            End If

            lblpicture.text &= "" & ds.Tables("t1").Rows(0)("message_detail").ToString
            is_delay = ds.Tables("t1").Rows(0)("is_delay").ToString

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdNext_Click(sender As Object, e As System.EventArgs) Handles cmdNext.Click
        If id = "2" Then
            Response.Redirect("home.aspx")
        ElseIf id = "3" Then
            Response.Redirect("ext_training_list.aspx?req=ext")
        End If
    End Sub

    Protected Sub cmdNext2_Click(sender As Object, e As EventArgs) Handles cmdNext2.Click
        If id = "2" Then
            Response.Redirect("home.aspx")
        ElseIf id = "3" Then
            Response.Redirect("ext_training_list.aspx?req=ext")
        End If
    End Sub
End Class
