Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Namespace incident
    Partial Class incident_popup_status
        Inherits System.Web.UI.Page
        Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Private irId As String = ""
        Private irNo As String = ""

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If IsNothing(Session("user_fullname")) Then
                ' Response.Redirect("../login.aspx")
                Response.End()
            End If

            irId = Request.QueryString("irId")
            irNo = Request.QueryString("irNo")

            If conn.setConnection Then

            Else
                Response.Write("Connection Error")
            End If

            If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
                lblHead.Text = "[" & irNo & "]"
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
                sql = "SELECT * FROM ir_status_log a INNER JOIN ir_status_list b ON a.status_id = b.ir_status_id WHERE  a.status_id <> 1 AND a.ir_id = " & irId
                sql &= " ORDER BY log_time ASC , log_status_id ASC"
               
                ds = conn.getDataSet(sql, "table1")

                ' Response.Write(sql)
                '  Return
                Gridview1.DataSource = ds
                GridView1.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End Sub

        Protected Sub Gridview1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview1.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lblDuration As Label = CType(e.Row.FindControl("lblDuration"), Label)
                Dim lblDateTS As Label = CType(e.Row.FindControl("lblDateTS"), Label)
                Dim startTS As String = ""
                Dim sql As String
                Dim ds As New DataSet
                Try
         

                    sql = "SELECT * FROM ir_status_log a INNER JOIN ir_status_list b ON a.status_id = b.ir_status_id WHERE a.status_id <> 1 AND a.ir_id = " & irId
                    sql &= " ORDER BY log_status_id ASC"
                    ds = conn.getDataSet(sql, "table1")
                    startTS = ds.Tables(0).Rows(0)("log_time_ts").ToString

                    If startTS <> lblDateTS.Text Then
                        lblDuration.Text = MinuteDiff(startTS, lblDateTS.Text)
                    Else
                        lblDuration.Text = 0
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message & sql)
                Finally
                    ds.Dispose()

                End Try

            End If
        End Sub

        Protected Sub Gridview1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gridview1.SelectedIndexChanged

        End Sub
    End Class
End Namespace
