Imports System.Data
Imports ShareFunction

Partial Class cfb_popup_star
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        id = Request.QueryString("id")
      

        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If


        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        If Page.IsPostBack Then

        Else ' load first time
            bindAllDept()
        
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

    Sub bindAllDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from user_dept ORDER BY dept_name_en"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdept_all.DataSource = ds.Tables(0)
            txtdept_all.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindAllPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT emp_code , (user_fullname + ', ' + dept_name) AS user_fullname  from user_profile WHERE emp_code IN (SELECT Employee_id FROM temp_BHUser) "
            If txtdept_all.SelectedValue <> "" Then
                ' sql &= " AND costcenter_id = " & txtdept_all.SelectedValue
            End If

            If txtfind_name.Text <> "" Then
                sql &= " AND (user_fullname LIKE '%" & txtfind_name.Text & "%' OR emp_code LIKE '%" & txtfind_name.Text & "%' OR user_fullname_local LIKE '%" & txtfind_name.Text & "%' )"
            Else
                sql &= " AND 1 > 2 "
            End If

            sql &= " ORDER BY user_fullname"
            '  Response.Write("Xxx" & txtfind_name.Text)
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtperson_all.DataSource = ds
            txtperson_all.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindAllDoctor()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from m_doctor WHERE ISNULL(doctor_name_en,'') <> ''"

            If txtfind_doctor.Text <> "" Then
                sql &= " AND (doctor_name_en LIKE '%" & txtfind_doctor.Text & "%' OR doctor_name_th LIKE '%" & txtfind_doctor.Text & "%' OR emp_no LIKE '%" & txtfind_doctor.Text & "%' ) "
            Else
                sql &= " AND 1 > 2 "
            End If

            sql &= "  ORDER BY doctor_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdoctor_all.DataSource = ds.Tables(0)
            txtdoctor_all.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim ds As New DataSet

        Try
            copyDraft()
            updateDetail("1")

            conn.setDBCommit()

            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; window.close();"
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddRelateDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddRelateDept.Click
        While txtdept_all.Items.Count > 0 AndAlso txtdept_all.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtdept_all.SelectedItem
            selectedItem.Selected = False
            txtdept_select.Items.Add(selectedItem)
            ' txtdept_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveRelateDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveRelateDept.Click
        While txtdept_select.Items.Count > 0 AndAlso txtdept_select.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtdept_select.SelectedItem
            selectedItem.Selected = False
            ' txtdept_all.Items.Add(selectedItem)
            txtdept_select.Items.Remove(selectedItem)
        End While
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

    Protected Sub cmdAddRelateDoctor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddRelateDoctor.Click
        While txtdoctor_all.Items.Count > 0 AndAlso txtdoctor_all.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtdoctor_all.SelectedItem
            selectedItem.Selected = False
            txtdoctor_select.Items.Add(selectedItem)
            ' txtdept_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveRelateDoctor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveRelateDoctor.Click
        While txtdoctor_select.Items.Count > 0 AndAlso txtdoctor_select.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtdoctor_select.SelectedItem
            selectedItem.Selected = False
            txtdoctor_all.Items.Add(selectedItem)
            txtdoctor_select.Items.Remove(selectedItem)
        End While
    End Sub

    Sub isHasRow(ByVal table As String, newpk As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""

        sql = "SELECT * FROM " & table & " WHERE star_id = " & newpk
        'Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage)
        End If

        If ds.Tables("t1").Rows.Count <= 0 Then
            sql = "INSERT INTO " & table & " (star_id) VALUES( "
            sql &= "" & newpk & ""
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If
            '   Response.Write(sql)
        End If

        ds.Dispose()

    End Sub

    Sub copyDraft()
        Dim sql As String
        Dim errorMsg As String = ""
        Dim ds As New DataSet
        Dim pk As String = ""

        Try
            sql = "SELECT ISNULL(MAX(star_id),0) + 1 AS pk FROM star_trans_list"
            ds = conn.getDataSetForTransaction(sql, "t1")
            pk = ds.Tables("t1").Rows(0)(0).ToString
            '  new_star_id = pk
        Catch ex As Exception
            Response.Write(ex.Message & sql)
            Response.Write(sql)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try


        sql = "INSERT INTO star_trans_list (star_id , submit_date , submit_date_ts , status_id ,  report_by , report_emp_code , report_dept_id , report_dept_name ,  report_costcenter_id)"
        sql &= " VALUES("
        sql &= "" & pk & " ,"


        sql &= " GETDATE() ,"
        sql &= "" & Date.Now.Ticks & " ,"
        sql &= "" & 1 & " ,"
        sql &= "'" & Session("user_fullname").ToString & "' ,"
        sql &= "'" & Session("emp_code").ToString & "' ,"
        sql &= "'" & Session("dept_id").ToString & "' ,"
        sql &= "'" & Trim(Session("dept_name").ToString) & "' ,"
        sql &= "'" & Session("costcenter_id").ToString & "' "
        sql &= ")"
        ' Response.Write(sql)
        '  Response.End()
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & sql)
        End If

        ' id = pk
        isHasRow("star_detail_tab", pk)
        updateDetail(pk)
        updateRelatePerson(pk)
        updateRelateDept(pk)
        updateRelateDoctor(pk)
        updateOnlyLog("1", pk, "Sent from CFB Online")
    End Sub

    Sub updateDetail(ByVal star_id As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim new_star_no As String = ""
        Dim ds As New DataSet

        sql = "SELECT * FROM cfb_comment_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id WHERE a.comment_id = " & id
        ds = conn.getDataSetForTransaction(sql, "t1")
        ' Response.Write(sql)
        sql = "UPDATE star_detail_tab SET hn = '" & ds.Tables("t1").Rows(0)("hn").ToString & "'"


        sql &= " , location = '" & addslashes(ds.Tables("t1").Rows(0)("location").ToString) & "'"
        sql &= " , room = '" & addslashes(ds.Tables("t1").Rows(0)("room").ToString) & "'"
        sql &= " , datetime_complaint = '" & ConvertTSToSQLDateTime(ds.Tables("t1").Rows(0)("datetime_complaint_ts").ToString) & "'"
        sql &= " , datetime_complaint_ts = '" & ds.Tables("t1").Rows(0)("datetime_complaint_ts").ToString & "' "


        '  sql &= " , hn = '" & txthn.Value & "'"
        sql &= " , datetime_report = '" & ConvertTSToSQLDateTime(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString) & "'"
        sql &= " , datetime_report_ts = " & ds.Tables("t1").Rows(0)("datetime_report_ts").ToString & ""
        'Response.Write("ttt")
        sql &= " , complain_detail = '" & addslashes(ds.Tables("t1").Rows(0)("complain_detail").ToString) & "'"
        sql &= " , service_type = '" & ds.Tables("t1").Rows(0)("service_type").ToString & "'"
        sql &= " , pt_title = '" & ds.Tables("t1").Rows(0)("pt_title").ToString & "'"

        sql &= " , age = '" & ds.Tables("t1").Rows(0)("age").ToString & "'"
        sql &= " , sex = '" & ds.Tables("t1").Rows(0)("sex").ToString & "'"
        sql &= " , customer_segment = '" & ds.Tables("t1").Rows(0)("customer_segment").ToString & "'"
        sql &= " , country = '" & ds.Tables("t1").Rows(0)("country").ToString & "'"
        'sql &= " , cfb_dept_id = '" & txtdept.SelectedValue & "'"
        ' sql &= " , cfb_dept_name = '" & txtdept.SelectedItem.Text & "'"
        sql &= " , complain_status = '" & ds.Tables("t1").Rows(0)("complain_status").ToString & "'"
        sql &= " , feedback_from = '" & addslashes(ds.Tables("t1").Rows(0)("feedback_from").ToString) & "'"
        sql &= " , complain_status_remark = '" & addslashes(ds.Tables("t1").Rows(0)("complain_status_remark").ToString) & "'"
        sql &= " , feedback_from_remark = '" & addslashes(ds.Tables("t1").Rows(0)("feedback_from_remark").ToString) & "'"


        sql &= " , cfb_customer_resp = '" & ds.Tables("t1").Rows(0)("cfb_customer_resp").ToString & "'"
        sql &= " , cfb_customer_resp_remark = '" & addslashes(ds.Tables("t1").Rows(0)("cfb_customer_resp_remark").ToString) & "'"

        sql &= " , cfb_chk_tel = " & addslashes(ds.Tables("t1").Rows(0)("cfb_chk_tel").ToString)
        sql &= " , cfb_chk_email = " & addslashes(ds.Tables("t1").Rows(0)("cfb_chk_email").ToString)
        sql &= " , cfb_chk_other = " & addslashes(ds.Tables("t1").Rows(0)("cfb_chk_other").ToString)

        sql &= " , cfb_tel_remark = '" & addslashes(ds.Tables("t1").Rows(0)("cfb_tel_remark").ToString) & "'"
        sql &= " , cfb_email_remark = '" & addslashes(ds.Tables("t1").Rows(0)("cfb_email_remark").ToString) & "'"
        sql &= " , cfb_other_remark = '" & addslashes(ds.Tables("t1").Rows(0)("cfb_other_remark").ToString) & "'"

        sql &= " , service_detail = '" & addslashes(ds.Tables("t1").Rows(0)("comment_detail").ToString) & "'"
        sql &= " , cfbno_relate = '" & addslashes(ds.Tables("t1").Rows(0)("cfb_no").ToString) & "'"
        sql &= " , nominee_type_id = '" & txtnominee_type.SelectedValue & "'"
        sql &= " , nominee_type_name = '" & txtnominee_type.SelectedItem.Text & "'"
        sql &= " , custom_nominee = '" & addslashes(txtcustom_name.Text) & "'"
        sql &= " WHERE star_id = " & star_id
        'Response.Write(status_id)
        ' Response.End()
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If

        Try
            ds.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Sub updateRelateDept(ByVal star_id As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""




        For i As Integer = 0 To txtdept_select.Items.Count - 1
            ' User Relate Dept ssip_relate_dept
            pk = getPK("star_relate_dept_id", "star_relate_dept", conn)
            sql = "INSERT INTO star_relate_dept (star_relate_dept_id , star_id , costcenter_id , costcenter_name ) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & star_id & "' ,"
            sql &= "'" & txtdept_select.Items(i).Value & "' ,"
            sql &= "'" & txtdept_select.Items(i).Text & "' "

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If


        Next i


    End Sub

    Sub updateRelatePerson(ByVal star_id As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""

        For i As Integer = 0 To txtperson_select.Items.Count - 1
            pk = getPK("star_relate_person_id", "star_relate_person", conn)
            sql = "INSERT INTO star_relate_person (star_relate_person_id , star_id , emp_code , user_fullname) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & star_id & "' ,"
            sql &= "'" & txtperson_select.Items(i).Value & "' ,"
            sql &= "'" & txtperson_select.Items(i).Text & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i


    End Sub

    Sub updateRelateDoctor(ByVal star_id As String)
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""


        For i As Integer = 0 To txtdoctor_select.Items.Count - 1
            pk = getPK("star_relate_doctor_id", "star_relate_doctor", conn)
            sql = "INSERT INTO star_relate_doctor (star_relate_doctor_id , star_id , emp_code , doctor_name) VALUES("
            sql &= "'" & pk & "' ,"
            sql &= "'" & star_id & "' ,"
            sql &= "'" & txtdoctor_select.Items(i).Value & "' ,"
            sql &= "'" & txtdoctor_select.Items(i).Text & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
        Next i


    End Sub

    Sub updateOnlyLog(ByVal status_id As String, star_id As String, Optional ByVal log_remark As String = "")
        Dim sql As String
        Dim errorMsg As String

        sql = "INSERT INTO star_status_log (status_id , status_name , star_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
        sql &= "'" & status_id & "' ,"
        sql &= "'" & "" & "' ,"
        sql &= "'" & star_id & "' ,"
        sql &= "GETDATE() ,"
        sql &= "'" & Date.Now.Ticks & "' ,"
        sql &= "'" & Session("user_fullname").ToString & "' ,"
        sql &= "'" & Session("user_position").ToString & "' ,"
        sql &= "'" & Session("dept_name").ToString & "' ,"
        sql &= "'" & log_remark & "' "
        sql &= ")"
        '  Response.Write(sql)
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If
    End Sub

    Protected Sub cmdFindName_Click(sender As Object, e As System.EventArgs) Handles cmdFindName.Click
        bindAllPerson()
    End Sub

    Protected Sub cmdFindDoctor_Click(sender As Object, e As System.EventArgs) Handles cmdFindDoctor.Click
        bindAllDoctor()
    End Sub
End Class
