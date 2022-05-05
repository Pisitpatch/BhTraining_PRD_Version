Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_ssip_activity_detail
    Inherits System.Web.UI.Page
    Protected mode As String = ""
    Protected id As String = "0"
    Protected ssip_id As String = ""
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")
        ssip_id = Request.QueryString("ssip_id")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Not Page.IsPostBack Then ' First time
            '  bindDept()
            ' bindStatus()
            bindRoom()
            bindAllPerson()
            bindSelectPerson()
            If mode = "edit" Then
                bindForm()
            Else
                bindSSIPDetail()
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

    Sub bindSSIPDetail()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_detail_tab a INNER JOIN ssip_trans_list b ON a.ssip_id = b.ssip_id WHERE a.ssip_id = " & ssip_id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtssip_no.Text = ds.Tables("t1").Rows(0)("ssip_no").ToString
            txtsubject.Text = ds.Tables("t1").Rows(0)("topic").ToString
            txtdetail.Value = ds.Tables("t1").Rows(0)("topic_detail").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
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

        

            txtroom.Items.Insert(0, New ListItem("-- Please Select --", ""))

        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_activity_list WHERE record_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtroom.SelectedValue = ds.Tables("t1").Rows(0)("room_id").ToString
            txtaction_date.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("activity_date_start_ts").ToString)
            txthour1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("activity_date_start_ts").ToString, "hour")
            txthour2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("activity_date_end_ts").ToString, "hour")
            txtmin1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("activity_date_start_ts").ToString, "min")
            txtmin2.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("activity_date_end_ts").ToString, "min")

            txtssip_no.Text = ds.Tables("t1").Rows(0)("ssip_no").ToString
            txtadd_activity.SelectedValue = ds.Tables("t1").Rows(0)("activity_id").ToString
            txtsubject.Text = addslashes(ds.Tables("t1").Rows(0)("ssip_subject").ToString)
            txtdetail.Value = addslashes(ds.Tables("t1").Rows(0)("activity_detail").ToString)
            txttool.SelectedValue = ds.Tables("t1").Rows(0)("tool_id").ToString
            txtreference.SelectedValue = ds.Tables("t1").Rows(0)("reference_id").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String = ""
        Dim pk As String = id
        Dim pk2 As String
        Try
            If mode = "add" Then
                pk = getPK("record_id", "ssip_activity_list", conn)
                sql = "INSERT INTO ssip_activity_list (record_id , ssip_id , room_id , room_name , activity_date_start , activity_date_end , activity_date_start_ts ,activity_date_end_ts"
                sql &= " , ssip_no , ssip_subject , activity_id, activity_name , activity_detail , tool_name , reference_name"
                sql &= " , tool_id , reference_id , rec_emp_code , rec_emp_name , rec_dept_id , rec_dept_name , rec_job_type , rec_job_title"
                sql &= ") VALUES("
                sql &= "'" & pk & "' ,"
                sql &= "'" & ssip_id & "' ,"
                sql &= "'" & txtroom.SelectedValue & "' ,"
                sql &= "'" & addslashes(txtroom.SelectedItem.Text) & "' ,"
                sql &= "'" & convertToSQLDatetime(txtaction_date.Text, txthour1.SelectedValue.PadLeft(2, "0"), txtmin1.SelectedValue.PadLeft(2, "0")) & "' ,"
                sql &= "'" & convertToSQLDatetime(txtaction_date.Text, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "' ,"
                sql &= "'" & ConvertDateStringToTimeStamp(txtaction_date.Text, CInt(txthour1.SelectedValue), CInt(txtmin1.SelectedValue)) & "' ,"
                sql &= "'" & ConvertDateStringToTimeStamp(txtaction_date.Text, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & "' ,"
                sql &= "'" & txtssip_no.Text & "' ,"
                sql &= "'" & addslashes(txtsubject.Text) & "' ,"
                sql &= "'" & txtadd_activity.SelectedValue & "' ,"
                sql &= "'" & txtadd_activity.SelectedItem.Text & "' ,"
                sql &= "'" & addslashes(txtdetail.Value) & "' ,"
                sql &= "'" & txttool.SelectedItem.Text & "' ,"
                sql &= "'" & txtreference.SelectedItem.Text & "' ,"
                sql &= "'" & txttool.SelectedValue & "' ,"
                sql &= "'" & txtreference.SelectedValue & "' ,"
                sql &= "'" & Session("emp_code").ToString & "' ,"
                sql &= "'" & Session("user_fullname").ToString & "' ,"
                sql &= "'" & Session("dept_id").ToString & "' ,"
                sql &= "'" & Session("dept_name").ToString & "' , "
                sql &= "'" & Session("user_position").ToString & "' , "
                sql &= "'" & Session("job_title").ToString & "'  "
                sql &= ")"
            ElseIf mode = "edit" Then
                sql = "UPDATE ssip_activity_list SET room_id =  " & txtroom.SelectedValue
                sql &= " , room_name = '" & txtroom.SelectedItem.Text & "' "
                sql &= " , activity_date_start = '" & convertToSQLDatetime(txtaction_date.Text, txthour1.SelectedValue.PadLeft(2, "0"), txtmin1.SelectedValue.PadLeft(2, "0")) & "' "
                sql &= " , activity_date_end = '" & convertToSQLDatetime(txtaction_date.Text, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "' "
                sql &= " , activity_date_start_ts = '" & ConvertDateStringToTimeStamp(txtaction_date.Text, CInt(txthour1.SelectedValue), CInt(txtmin1.SelectedValue)) & "' "
                sql &= " , activity_date_end_ts = '" & ConvertDateStringToTimeStamp(txtaction_date.Text, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & "' "
                sql &= " , ssip_no = '" & txtssip_no.Text & "' "
                sql &= " , ssip_subject = '" & addslashes(txtsubject.Text) & "' "
                sql &= " , activity_id = '" & txtadd_activity.SelectedValue & "' "
                sql &= " , activity_name = '" & txtadd_activity.SelectedItem.Text & "' "
                sql &= " , activity_detail = '" & addslashes(txtdetail.Value) & "' "
                sql &= " , tool_id = '" & txttool.SelectedValue & "' "
                sql &= " , tool_name = '" & txttool.SelectedItem.Text & "' "
                sql &= " , reference_id = '" & txtreference.SelectedValue & "' "
                sql &= " , reference_name = '" & txtreference.SelectedItem.Text & "' "
                sql &= " WHERE record_id = " & id
            End If
   

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            If mode = "edit" Then
                sql = "DELETE FROM ssip_activity_person WHERE record_id = " & id
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If

            For i As Integer = 0 To txtperson_select.Items.Count - 1
                pk2 = getPK("ssip_activity_person_id", "ssip_activity_person", conn)
                sql = "INSERT INTO ssip_activity_person (ssip_activity_person_id , record_id , ssip_id , emp_code , user_fullname) VALUES("
                sql &= "" & pk2 & " , "
                sql &= "" & pk & " , "
                sql &= "" & ssip_id & " , "
                sql &= "" & txtperson_select.Items(i).Value & " , "
                sql &= " '" & txtperson_select.Items(i).Text & "'  "
                sql &= " )"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i



            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
            Return
        End Try

        Response.Redirect("form_ssip.aspx?mode=edit&id=" & ssip_id)
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("form_ssip.aspx?mode=edit&id=" & ssip_id)
    End Sub

    Protected Sub cmdAddRelatePerson_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddRelatePerson.Click
        While txtperson_all.Items.Count > 0 AndAlso txtperson_all.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtperson_all.SelectedItem
            selectedItem.Selected = False
            txtperson_select.Items.Add(selectedItem)
            txtperson_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveRelatePerson_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveRelatePerson.Click
        While txtperson_select.Items.Count > 0 AndAlso txtperson_select.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtperson_select.SelectedItem
            selectedItem.Selected = False
            txtperson_all.Items.Add(selectedItem)
            txtperson_select.Items.Remove(selectedItem)
        End While
    End Sub

    Sub bindAllPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from user_profile WHERE 1 = 1 "

            sql &= " ORDER BY user_fullname"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtperson_all.DataSource = ds
            txtperson_all.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            If mode = "add" Then
                sql = "SELECT * from ssip_activity_person WHERE 1 > 2 "
            Else
                sql = "SELECT * from ssip_activity_person WHERE record_id = " & id
            End If

            ds = conn.getDataSetForTransaction(sql, "t1")
            txtperson_select.DataSource = ds
            txtperson_select.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
