Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_internal_training_detail
    Inherits System.Web.UI.Page
    Protected session_id As String
    Protected mode As String = ""
    Protected id As String
    Protected idp_status As String = ""
    Protected new_idp_id As String
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Dim priv_list() As String
    Protected viewtype As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        session_id = Session("session_myid").ToString
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        priv_list = Session("priv_list")

        viewtype = Session("viewtype").ToString


        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If mode = "add" Then
            If viewtype = "" Then
                lblempcode.Text = Session("emp_code").ToString
                lblDept.Text = Session("dept_name").ToString
                lblDivision.Text = Session("job_title").ToString
                lblrequest_NO.Text = ""
                lblname.Text = Session("user_fullname").ToString
                lbljobtitle.Text = Session("user_position").ToString
                lblCostcenter.Text = Session("costcenter_id").ToString
            End If
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


    Sub onDeleteComment(ByVal sender As Object, ByVal e As CommandEventArgs)


    End Sub
End Class
