Imports System.IO
Imports System.Data
Imports ShareFunction
Partial Class game_admin_test_master
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)


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



        If IsPostBack Then

        Else ' First time load
            bindGridTest()
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

    Sub bindGridTest()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_test WHERE ISNULL(is_delete,0) = 0 AND is_game = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("admin_test_edit.aspx?mode=add")
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim imgStatusActive As Image = CType(e.Row.FindControl("imgStatusActive"), Image)
            Dim imgStatusInActive As Image = CType(e.Row.FindControl("imgStatusInActive"), Image)
            Dim lblStatusID As Label = CType(e.Row.FindControl("lblStatusID"), Label)

            If lblStatusID.Text = "1" Then
                imgStatusActive.Visible = True
                imgStatusInActive.Visible = False
            Else
                imgStatusActive.Visible = False
                imgStatusInActive.Visible = True
            End If
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
