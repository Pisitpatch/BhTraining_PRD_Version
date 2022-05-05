Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction


Partial Class ir_customform_list
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    'Private conn1 As DBUtil = CType(Session("myConn"), DBUtil)
    Protected id As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        id = Request.QueryString("id")



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

    Sub bindGrid()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT *  FROM ir_form_master "
            ' sql &= " ORDER BY item_order_sort ASC"
            reader = conn.getDataReaderForTransaction(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            GridView1.DataSource = reader
            GridView1.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write("bindGridElement :: ")
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim sql As String
        Dim ds As New DataSet

        If (e.CommandName = "selectRow") Then

            Dim CommandArgument As String = e.CommandArgument


            Dim index As String = CommandArgument
            ' Response.Write(e.CommandArgument)
            Response.Redirect("ir_customform_wizard1.aspx?id=" & index)
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
