Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_ssip_news_detail
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = "id"
    Protected lang As String = "th"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        id = Request.QueryString("id")
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Page.IsPostBack Then
        Else ' First time load
            bindDetail()
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)(
        End Try
    End Sub

    Sub bindDetail()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_news WHERE ISNULL(is_delete,0) = 0 AND new_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            lblDetail.Text = ds.Tables("t1").Rows(0)("detail_th").ToString
            lblFileName.Text = "<a href='../share/ssip/attach_file/" & ds.Tables("t1").Rows(0)("file_path").ToString & "' target='_blank'>" & ds.Tables("t1").Rows(0)("file_name").ToString & "</a>"
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
