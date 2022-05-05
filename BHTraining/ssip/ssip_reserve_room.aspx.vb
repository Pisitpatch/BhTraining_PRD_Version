Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_ssip_reserve_room
    Inherits System.Web.UI.Page
    Protected session_id As String
    Protected mode As String = ""
    Protected id As String
    Protected room As String
    Protected room_event As String = ""
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        session_id = Session("session_myid").ToString
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        room = Request.QueryString("room")


        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If IsPostBack Then

        Else ' First time
            lblempcode.Text = Session("emp_code").ToString
            lblDept.Text = Session("dept_name").ToString
            lblDivision.Text = Session("job_title").ToString

            lblname.Text = Session("user_fullname").ToString
            lbljobtitle.Text = Session("user_position").ToString
            lblCostcenter.Text = Session("costcenter_id").ToString
            bindRoom()
            bindSSIPNo()
            txtroom.SelectedValue = room
            If txtroom.SelectedValue <> "" Then
                ' lblRoom.Text = txtroom.SelectedItem.Text
            End If
        End If


        bindEvent()
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

    Sub bindRoom()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_m_room WHERE ISNULL(is_delete,0) = 0 ORDER BY room_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtroom.DataSource = ds
            txtroom.DataBind()

            lblroom.DataSource = ds
            lblroom.DataBind()

            txtroom.Items.Insert(0, New ListItem("-- Please Select --", ""))
            lblroom.Items.Insert(0, New ListItem("-- Please Select --", ""))
        Catch ex As Exception

        End Try
    End Sub

    Sub bindSSIPNo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_trans_list WHERE ISNULL(is_delete,0) = 0 AND status_id > 1 AND ssip_no > 0  ORDER BY ssip_no"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtssip_no.DataSource = ds
            txtssip_no.DataBind()

          

            txtssip_no.Items.Insert(0, New ListItem("-- Please Select --", ""))

        Catch ex As Exception

        End Try
    End Sub

    Sub bindEvent()
        Dim sql As String
        Dim ds As New DataSet
        Dim limit As String
        Dim dd As Date
        Dim ee As Date
        Try
            sql = "SELECT * FROM ssip_reserve_room_list WHERE 1 = 1 "
            If room <> "" Then
                sql &= " AND room_id = " & room
            End If
            sql &= " AND is_delete = 0 AND is_active = 1 ORDER BY reserve_date_start_ts"
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            'room_event = "{"
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                '   Response.Write(room_event)
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If

                dd = New Date(CLng(ds.Tables("t1").Rows(i)("reserve_date_start_ts").ToString))
                ee = New Date(CLng(ds.Tables("t1").Rows(i)("reserve_date_end_ts").ToString))

                room_event &= limit & "{"
                room_event &= "id: " & ds.Tables("t1").Rows(i)("reserve_id").ToString & "," & vbCrLf
                room_event &= " title: '" & ds.Tables("t1").Rows(i)("room_name").ToString & "\n" & ds.Tables("t1").Rows(i)("ssip_subject").ToString & "'," & vbCrLf
                room_event &= " start: new Date(" & dd.Year & ", " & dd.Month - 1 & ", " & dd.Day & " , " & dd.Hour & " , " & dd.Minute & ")," & vbCrLf
                room_event &= "  end: new Date(" & ee.Year & ", " & ee.Month - 1 & ", " & ee.Day & " , " & ee.Hour & " , " & ee.Minute & ") ," & vbCrLf
                room_event &= "allDay: false"
                room_event &= "}"
            Next i


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String = ""
        Dim pk As String

        Try
            pk = getPK("reserve_id", "ssip_reserve_room_list", conn)
            sql = "INSERT INTO ssip_reserve_room_list (reserve_id , room_id , room_name , reserve_date_start , reserve_date_end , reserve_date_start_ts , reserve_date_end_ts"
            sql &= " , ssip_no , ssip_subject , activity_id, activity_name , reserve_detail , tool_name , reference_name"
            sql &= ") VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & lblroom.SelectedValue & "' ,"
            sql &= "'" & addslashes(lblroom.SelectedItem.Text) & "' ,"
            sql &= "'" & convertToSQLDatetime(txtreserve_date.Text, txthour1.SelectedValue.PadLeft(2, "0"), txtmin1.SelectedValue.PadLeft(2, "0")) & "' ,"
            sql &= "'" & convertToSQLDatetime(txtreserve_date.Text, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "' ,"
            sql &= "'" & ConvertDateStringToTimeStamp(txtreserve_date.Text, CInt(txthour1.SelectedValue), CInt(txtmin1.SelectedValue)) & "' ,"
            sql &= "'" & ConvertDateStringToTimeStamp(txtreserve_date.Text, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & "' ,"
            sql &= "'" & txtssip_no.SelectedValue & "' ,"
            sql &= "'" & addslashes(txtsubject.Value) & "' ,"
            sql &= "'" & txtadd_activity.SelectedValue & "' ,"
            sql &= "'" & txtadd_activity.SelectedItem.Text & "' ,"
            sql &= "'" & addslashes(txtdetail.Value) & "' ,"
            sql &= "'" & "" & "' ,"
            sql &= "'" & "" & "' "

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        End Try

        Response.Redirect("ssip_reserve_room.aspx?room=" & txtroom.SelectedValue)
    End Sub

    Protected Sub txtroom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtroom.SelectedIndexChanged
        'bindEvent()
        Response.Redirect("ssip_reserve_room.aspx?room=" & txtroom.SelectedValue)
    End Sub
End Class
