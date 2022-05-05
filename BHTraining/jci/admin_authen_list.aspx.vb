Imports System.IO
Imports System.Data
Imports ShareFunction


Partial Class jci_admin_authen_list
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("jci_emp_code")) Then
            Response.Redirect("login.aspx")
            'Response.Write("Please re-login again")
            Response.End()
        End If


        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        tid = Request.QueryString("tid")

        If IsPostBack Then

        Else ' First time load

            bindGrid()
            bindPathWay()
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_user_authen "
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindPathWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_test WHERE test_id = " & tid
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblPathWay.Text = " > " & ds.Tables("t1").Rows(0)("test_name_th").ToString & " > " & " User Authentication"
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
