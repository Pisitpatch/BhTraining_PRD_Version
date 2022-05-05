Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_popup_event
    Inherits System.Web.UI.Page

    Protected session_id As String
    Protected mode As String = ""
    Protected id As String
    Protected cid As String
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        session_id = Session("session_myid").ToString
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        cid = Request.QueryString("cid")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If IsPostBack Then

        Else ' First time
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
            sql = "SELECT * FROM ssip_reserve_room_list WHERE reserve_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            lblRoom.Text = ds.Tables("t1").Rows(0)("room_name").ToString
            txtreserve_date.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("reserve_date_start_ts").ToString)
            txthour1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("reserve_date_start_ts").ToString, "hour")
            txtmin1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("reserve_date_start_ts").ToString, "min")
            txthour2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("reserve_date_end_ts").ToString, "hour")
            txtmin2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("reserve_date_end_ts").ToString, "min")
            txtssip_no.Value = ds.Tables("t1").Rows(0)("ssip_no").ToString
            txtsubject.Value = ds.Tables("t1").Rows(0)("ssip_subject").ToString
            txtadd_activity.SelectedValue = ds.Tables("t1").Rows(0)("activity_id").ToString
            txtdetail.Value = ds.Tables("t1").Rows(0)("reserve_detail").ToString
            txttool.SelectedValue = ds.Tables("t1").Rows(0)("tool_name").ToString
            txtreference.SelectedValue = ds.Tables("t1").Rows(0)("reference_name").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
