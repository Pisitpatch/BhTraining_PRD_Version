Imports System.Data
Imports System.IO
Imports ShareFunction
Partial Class idp_ext_register
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""
    Protected sh_id As String = ""
    Protected regis_num As Integer = 0
    Protected wait_num As Integer = 0
    Protected idp_relate As String = ""

    Protected priv_list() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        id = Request.QueryString("id")
        sh_id = Request.QueryString("sh_id")


        priv_list = Session("priv_list")

        If findArrayValue(priv_list, "19") = True Or findArrayValue(priv_list, "14") = True Then
           
        Else
            Response.End()
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        If Page.IsPostBack Then
        Else ' load first time
            bindRegister()
            bindForm()
            bindSpeaker()
            bindDocument()
        End If

        setIDPRelateTraining()
        ' ClientScript.RegisterStartupScript(Page.GetType(), "", "<script type='text/javascript'>document.getElementById('" & txtbarcode.ClientID & "').focus();</script>")
        ' txtbarcode.Attributes.Add("onfocus", "myFocus()")
      
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

    Sub setIDPRelateTraining()
        Dim sql As String
        Dim ds As New DataSet
        Dim limit As String = ""
        Try
            sql = "SELECT topic_id FROM idp_training_relate_idp WHERE ISNULL(topic_id , 0) > 0 AND idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If

                idp_relate &= limit & ds.Tables("t1").Rows(i)("topic_id").ToString
            Next i

            If idp_relate <> "" Then
                idp_relate = " topic_id IN (" & idp_relate & ")"
            Else
                idp_relate = "  1 > 2 "
            End If
            '  Response.Write(idp_relate)
        Catch ex As Exception
            Response.Write(sql)
        End Try
    End Sub

    Sub bindRegister()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.* , CASE WHEN ISNULL(b.idp_id,'') <> '' THEN 'Yes' ELSE 'No' END AS target  "
            sql &= ", CASE WHEN c.is_online = 1 THEN 'Yes' ELSE 'No' END AS online "
            sql &= " FROM idp_training_registered a "
            sql &= " LEFT OUTER JOIN (SELECT idp_id , emp_code FROM idp_training_employee GROUP BY idp_id , emp_code) b "
            sql &= " ON a.emp_code = b.emp_code AND b.idp_id = " & id
            sql &= " INNER JOIN idp_training_schedule c ON a.schedule_id = c.schedule_id "
            sql &= " WHERE a.schedule_id = " & sh_id
            sql &= " ORDER BY a.register_id DESC "
            ds = conn.getDataSetForTransaction(sql, "t1")

            GridRegister.DataSource = ds
            GridRegister.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_schedule a INNER JOIN idp_external_req b ON a.idp_id = b.idp_id "
            sql &= " "
            sql &= "  WHERE a.schedule_id = " & sh_id
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblTopic.Text = ds.Tables("t1").Rows(0)("internal_title").ToString
            lblSchedule.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("schedule_start_ts").ToString) & " " & ConvertTSTo(ds.Tables("t1").Rows(0)("schedule_start_ts").ToString, "hour") & ":" & ConvertTSTo(ds.Tables("t1").Rows(0)("schedule_start_ts").ToString, "min").PadLeft(2, "0")
            lblSchedule.Text &= " - " & ConvertTSTo(ds.Tables("t1").Rows(0)("schedule_end_ts").ToString, "hour") & ":" & ConvertTSTo(ds.Tables("t1").Rows(0)("schedule_end_ts").ToString, "min").PadLeft(2, "0")
            lblDetail.Text = ds.Tables("t1").Rows(0)("internal_outline").ToString.Replace(vbCrLf, "<br/>")
            lblLocation.Text = ds.Tables("t1").Rows(0)("location").ToString
            lblType.Text = ds.Tables("t1").Rows(0)("schedule_type").ToString

            Dim date_format1 As New Date(ds.Tables("t1").Rows(0)("schedule_start_ts").ToString)
            Dim date_format2 As New Date(ds.Tables("t1").Rows(0)("schedule_end_ts").ToString)

            Dim diff_min As Long = DateDiff(DateInterval.Hour, date_format1, date_format2)
            txttrain_hour.Text = diff_min

            If ds.Tables("t1").Rows(0)("is_online").ToString = "1" Then
                txttrain_hour.Text = 0
            End If


            If ds.Tables("t1").Rows(0)("is_open").ToString = "1" Then
                lblStatus.Text = "<span style='color:red'>Open</span>"
            Else
                lblStatus.Text = "<span style='color:red'>Close</span>"
            End If

            lblHeader.Text &= ds.Tables("t1").Rows(0)("internal_title").ToString & " (" & lblSchedule.Text & ")"
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSpeaker()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_speaker WHERE idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblSpeaker.Text &= " - " & ds.Tables("t1").Rows(i)("title").ToString & " " & ds.Tables("t1").Rows(i)("fname").ToString & " " & ds.Tables("t1").Rows(i)("lname").ToString & "<br/>"
            Next i
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDocument()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_trainging_file WHERE idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblDocument.Text &= " - <a href='../share/idp/hr/" & ds.Tables("t1").Rows(i)("file_path").ToString & "' target='_blank'>" & ds.Tables("t1").Rows(i)("file_name").ToString & "</a> : " & ds.Tables("t1").Rows(i)("file_remark").ToString & "<br/>"
            Next i
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridRegister_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridRegister.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblRegistered As Label = CType(e.Row.FindControl("lblRegistered"), Label)
            '  Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            Dim lblIsRegister As Label = CType(e.Row.FindControl("lblIsRegister"), Label)
            Dim lblDate As Label = CType(e.Row.FindControl("lblDate"), Label)
            Dim lblDateTS As Label = CType(e.Row.FindControl("lblDateTS"), Label)
            Dim lblTarget As Label = CType(e.Row.FindControl("lblTarget"), Label)

            Try
                If lblRegistered.Text = "1" Then
                    lblIsRegister.Text = "<span style='color:green'>Register</span>&nbsp; "
                    regis_num += 1
                Else
                    lblIsRegister.Text = "<span style='color:red'>Waiting</span>"
                    wait_num += 1
                End If

                If lblTarget.Text = "Yes" Then
                    lblTarget.ForeColor = Drawing.Color.Green
                Else
                    lblTarget.ForeColor = Drawing.Color.Red
                End If

                lblinfo.text = "<span style='color:red'>Wait : " & wait_num & " records</span> <br/>"
                lblInfo.Text &= "<span style='color:green'>Registered : " & regis_num & " records</span> <br/>"

                If lblDateTS.Text <> "0" And lblDateTS.Text <> "" Then
                    'lblDate.Text = lblDateTS.Text
                    lblDate.Text = ConvertTSToDateString(lblDateTS.Text) & " " & ConvertTSTo(lblDateTS.Text, "hour") & ":" & ConvertTSTo(lblDateTS.Text, "min").PadLeft(2, "0")
                Else
                    lblDate.Text = "-"
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
           

         

        End If
    End Sub

    Protected Sub GridRegister_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridRegister.SelectedIndexChanged

    End Sub

    Protected Sub cmdRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        Dim chk As CheckBox
        Dim lblPK As Label
        Dim i As Integer = GridRegister.Rows.Count
        Dim sql As String
        Dim errorMsg As String

        Try

            For s As Integer = 0 To i - 1

                lblPK = CType(GridRegister.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridRegister.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "UPDATE idp_training_registered SET is_register = 1 , register_time = " & Date.Now.Ticks & " , register_by = '" & Session("user_fullname").ToString & "' WHERE register_id = " & lblPK.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindRegister()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUnRegister.Click
        Dim chk As CheckBox
        Dim lblPK As Label
        Dim i As Integer = GridRegister.Rows.Count
        Dim sql As String
        Dim errorMsg As String

        Try

            For s As Integer = 0 To i - 1

                lblPK = CType(GridRegister.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridRegister.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "UPDATE idp_training_registered SET is_register = 0 , register_time = null , register_by = null WHERE register_id = " & lblPK.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindRegister()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

  
    Sub onSave()
        Dim sql As String
        Dim errorMsg As String = ""
        Dim ds As New DataSet
        Dim ds2 As New DataSet
        Dim pk As String
        If txtbarcode.Text = "" Then
            Return
        End If

        Try
            sql = "SELECT * FROM user_profile WHERE emp_code = " & txtbarcode.Text
            ds = conn.getDataSetForTransaction(sql, "t1")
            '  Response.Write(sql)
            If ds.Tables("t1").Rows.Count > 0 Then

                sql = "SELECT * FROM idp_training_registered WHERE emp_code = " & txtbarcode.Text
                sql &= " AND schedule_id = " & sh_id

                ds2 = conn.getDataSetForTransaction(sql, "t2")
                ' Response.Write(sql)
                If ds2.Tables("t2").Rows.Count > 0 Then ' ถ้า register ไว้
                    sql = "UPDATE idp_training_registered SET is_register = 1 , register_time = " & Date.Now.Ticks & " , register_by = '" & Session("user_fullname").ToString & "' "
                    sql &= " , attendance_type_id = '" & txttype.SelectedValue & "' , attendance_type_name = '" & txttype.SelectedItem.Text & "' "
                    sql &= " , training_hour = '" & txttrain_hour.Text & "' "
                    sql &= "  WHERE emp_code = " & txtbarcode.Text & " AND schedule_id = " & sh_id

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If

                Else ' ถ้ายังไม่ได้ register ไว้ก่อน
                    pk = getPK("register_id", "idp_training_registered", conn)
                    '  Response.Write(ds.Tables("t1").Rows(0)("user_fullname").ToString)
                    sql = "INSERT INTO idp_training_registered (register_id , schedule_id , idp_id , emp_code , emp_name , dept_id , dept_name , job_title , job_type , create_by , create_date , create_date_ts , register_time , is_register , register_by , attendance_type_id , attendance_type_name , training_hour ) VALUES("
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & sh_id & "' ,"
                    sql &= " '" & id & "' ,"
                    sql &= " '" & txtbarcode.Text & "' ,"
                    sql &= " '" & addslashes(ds.Tables("t1").Rows(0)("user_fullname").ToString) & "' ,"
                    sql &= " '" & ds.Tables("t1").Rows(0)("dept_id").ToString & "' ,"
                    sql &= " '" & addslashes(ds.Tables("t1").Rows(0)("dept_name").ToString) & "' ,"
                    sql &= " '" & addslashes(ds.Tables("t1").Rows(0)("job_title").ToString) & "' ,"
                    sql &= " '" & addslashes(ds.Tables("t1").Rows(0)("job_type").ToString) & "' ,"
                    sql &= " '" & addslashes(Session("user_fullname").ToString) & "' ,"
                    sql &= " GETDATE() ,"
                    sql &= " '" & Date.Now.Ticks & "' , "
                    sql &= " '" & Date.Now.Ticks & "' , "
                    sql &= " '" & 1 & "' , "
                    sql &= " '" & addslashes(Session("user_fullname").ToString) & "' , "
                    sql &= " '" & txttype.SelectedValue & "' , "
                    sql &= " '" & addslashes(txttype.SelectedItem.Text) & "' , "
                    sql &= " '" & txttrain_hour.Text & "' "
                    sql &= ")"

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                    End If
                End If

                ' Update IDP Topic to status Complete
                sql = "UPDATE idp_function_tab SET topic_status_id = 1 , topic_status = 'Completed' WHERE 1 =1 "
                sql &= " AND idp_id IN (SELECT idp_id FROM idp_trans_list WHERE report_emp_code = " & txtbarcode.Text & " AND " & idp_relate & ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If

            End If


            conn.setDBCommit()
            txtbarcode.Text = ""
            bindRegister()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds2.Dispose()
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, txtbarcode.TextChanged
        onSave()

    End Sub

  
    Protected Sub cmdUploadCSV_Click(sender As Object, e As System.EventArgs) Handles cmdUploadCSV.Click
        Dim strFileName As String = ""
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim ds As New DataSet

        Try


            If Not IsNothing(FileUploadCSV.PostedFile) Then
                strFileName = FileUploadCSV.FileName

                If strFileName = "" Then
                    Return
                End If


                FileUploadCSV.PostedFile.SaveAs(Server.MapPath("../share/" & FileUploadCSV.FileName))


            End If


        Catch ex As Exception

            Response.Write(ex.Message)
            Return
        Finally

        End Try


        Dim strFilesName As String = strFileName
        Dim strPath As String = "../share/"


        Dim Sr As New StreamReader(Server.MapPath(strPath) & strFilesName)
        Dim sb As New System.Text.StringBuilder()
        Dim s As String

        Try
            While Not Sr.EndOfStream
                s = Sr.ReadLine()
                'cmd.CommandText = (("INSERT INTO MyTable Field1, Field2, Field3 VALUES(" + s.Split(","c)(1) & ", ") + s.Split(","c)(2) & ", ") + s.Split(","c)(3) & ")"
                ' lblDivisionSelect.Items.Add(New ListItem(s.Split(","c)(1), s.Split(","c)(0)))
                sql = "SELECT * FROM user_profile WHERE emp_code = " & s.Split(","c)(0).Trim
                sql &= " AND emp_code NOT IN (SELECT emp_code FROM idp_training_registered WHERE schedule_id = " & sh_id & ")"
                ds = conn.getDataSetForTransaction(sql, "t1")

                If ds.Tables("t1").Rows.Count > 0 Then
                    pk = getPK("register_id", "idp_training_registered", conn)
                    '  Response.Write(ds.Tables("t1").Rows(0)("user_fullname").ToString)
                    sql = "INSERT INTO idp_training_registered (register_id , schedule_id , idp_id , emp_code , emp_name , dept_id , dept_name , job_title , job_type , create_by , create_date , create_date_ts , register_time , is_register , register_by , training_hour , attendance_type_id , attendance_type_name ) VALUES("
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & sh_id & "' ,"
                    sql &= " '" & id & "' ,"
                    sql &= " '" & s.Split(","c)(0) & "' ,"
                    sql &= " '" & addslashes(ds.Tables("t1").Rows(0)("user_fullname").ToString) & "' ,"
                    sql &= " '" & ds.Tables("t1").Rows(0)("dept_id").ToString & "' ,"
                    sql &= " '" & addslashes(ds.Tables("t1").Rows(0)("dept_name").ToString) & "' ,"
                    sql &= " '" & addslashes(ds.Tables("t1").Rows(0)("job_title").ToString) & "' ,"
                    sql &= " '" & addslashes(ds.Tables("t1").Rows(0)("job_type").ToString) & "' ,"
                    sql &= " '" & addslashes(Session("user_fullname").ToString) & "' ,"
                    sql &= " GETDATE() ,"
                    sql &= " '" & Date.Now.Ticks & "' , "
                    sql &= " '" & Date.Now.Ticks & "' , "
                    sql &= " '" & 1 & "' , "
                    sql &= " '" & addslashes(Session("user_fullname").ToString) & "' , "
                    sql &= " '" & txttrain_hour.Text & "' , "
                    sql &= " '" & txttype.SelectedValue & "' , "
                    sql &= " '" & txttype.SelectedItem.Text & "'  "
                    sql &= ")"

                    'pk = getPK("register_id", "idp_training_registered", conn)
                    'sql = "INSERT INTO idp_training_registered (register_id , schedule_id , idp_id , emp_code , emp_name , dept_id , dept_name , job_title , job_type , create_by , create_date , create_date_ts , register_time ) VALUES("
                    'sql &= " '" & pk & "' ,"
                    'sql &= " '" & sh_id & "' ,"
                    'sql &= " '" & id & "' ,"
                    'sql &= " '" & s.Split(","c)(0) & "' ,"
                    'sql &= " '" & s.Split(","c)(1) & "' ,"
                    'sql &= " '" & 0 & "' ,"
                    'sql &= " '" & txtdept.Value & "' ,"
                    'sql &= " '" & txtjobtitle.Value & "' ,"
                    'sql &= " '" & txtjobtitle.Value & "' ,"
                    'sql &= " '" & Session("user_fullname").ToString & "' ,"
                    'sql &= " GETDATE() ,"
                    'sql &= " '" & Date.Now.Ticks & "' ,"
                    'sql &= " 0 "
                    'sql &= ")"

                  

                Else
                    sql = "UPDATE idp_training_registered SET register_time = '" & Date.Now.Ticks & "'   "
                    sql &= " , is_register = 1 "
                    sql &= " , register_by = '" & addslashes(Session("user_fullname").ToString) & "' "
                    '  sql &= " , register_time = " & Date.Now.Ticks
                    sql &= " , training_hour = '" & txttrain_hour.Text & "' "
                    sql &= " , attendance_type_id = '" & txttype.SelectedValue & "' "
                    sql &= " , attendance_type_name = '" & txttype.SelectedItem.Text & "' "
                    sql &= " WHERE emp_code = " & s.Split(","c)(0) & " AND schedule_id = " & sh_id
                End If

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
                'cmd.ExecuteNonQuery()

                ' Update IDP Topic to status Complete
                sql = "UPDATE idp_function_tab SET topic_status_id = 1 , topic_status = 'Completed' WHERE 1 =1 "
                sql &= " AND idp_id IN (SELECT idp_id FROM idp_trans_list WHERE report_emp_code = " & s.Split(","c)(0) & " AND " & idp_relate & ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
            End While

            conn.setDBCommit()
            bindRegister()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As System.EventArgs) Handles cmdDelete.Click
        Dim chk As CheckBox
        Dim lblPK As Label
        Dim i As Integer = GridRegister.Rows.Count
        Dim sql As String
        Dim errorMsg As String

        Try

            For s As Integer = 0 To i - 1

                lblPK = CType(GridRegister.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridRegister.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM idp_training_registered  WHERE register_id = " & lblPK.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindRegister()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub onCheckAll()

        Dim i As Integer


        Dim chk As CheckBox
        Dim h_chk As CheckBox
        h_chk = CType(GridRegister.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)
        i = GridRegister.Rows.Count

        Try

            For s As Integer = 0 To i - 1


                chk = CType(GridRegister.Rows(s).FindControl("chk"), CheckBox)


                If h_chk.Checked Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If

                If chk.Visible = False Then
                    chk.Checked = False
                End If
            Next s


        Catch ex As Exception

            Response.Write(ex.Message)
        End Try
    End Sub
End Class
