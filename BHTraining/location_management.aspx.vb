Imports System.Data.SqlClient
Imports System.Data

Partial Class location_management
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            bindGrid()
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

    Private Sub bindGrid()
        Dim ds As New DataSet
        Dim sql As String
        Try
            sql = "SELECT * FROM  m_location_new WHERE 1 = 1 "


            If txtname.Text <> "" Then
                sql &= " AND location_name LIKE '%" & txtname.Text.Trim & "%' "
            End If

            ds = conn.getDataSet(sql, "table1")

            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        bindGrid()
    End Sub
    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Response.Redirect("location_detail.aspx?mode=add")
    End Sub
    Protected Sub cmdReset_Click(sender As Object, e As EventArgs) Handles cmdReset.Click
        txtname.Text = ""
    End Sub
End Class
