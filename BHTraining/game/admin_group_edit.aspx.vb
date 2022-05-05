Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class Game_MasterPage
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String
    Protected gid As String
    Protected mode As String


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

        tid = Request.QueryString("tid")
        gid = Request.QueryString("gid")
        mode = Request.QueryString("mode")

        If IsPostBack Then

        Else ' First time load
            bindPathWay()
            ' bindJobTypeAll()
            bindTargetAll()
            If mode = "edit" Then
                bindIPAddressSelect()
                bindJobTypeSelect()
                bindTargetSelect()
                bindForm()
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

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id WHERE a.group_id = " & gid
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtgroup_th.Value = ds.Tables("t1").Rows(0)("group_name_th").ToString
            txtgroup_en.Value = ds.Tables("t1").Rows(0)("group_name_en").ToString
            txtip.Text = ds.Tables("t1").Rows(0)("group_fix_ip").ToString
            txtdetail.Value = ds.Tables("t1").Rows(0)("group_remark").ToString
            lblGroupName.Text = ds.Tables("t1").Rows(0)("test_name_th").ToString & " / " & ds.Tables("t1").Rows(0)("test_name_en").ToString
            txtqty.Text = ds.Tables("t1").Rows(0)("num_question").ToString

            Try
                txtzone.SelectedValue = ds.Tables("t1").Rows(0)("zone_no").ToString
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindPathWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id WHERE a.test_id = " & tid
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblPathWay.Text = " > <a href='admin_question_master.aspx?tid=" & tid & "'>" & ds.Tables("t1").Rows(0)("test_name_th").ToString & "</a> > "
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTypeAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_title FROM user_profile WHERE 1 = 1 AND job_title NOT IN (SELECT job_title_name_en FROM jci_jobtitle_target WHERE group_id = " & gid & ")"
            If txtfind_jobtype.Text <> "" Then
                sql &= " AND LOWER(job_title) LIKE '%" & txtfind_jobtype.Text.ToLower & "%' "
            End If
            sql &= " GROUP BY job_title ORDER BY job_title"

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblJobTypeAll.DataSource = ds
            lblJobTypeAll.DataBind()

           
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTypeSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_title_name_en AS job_title FROM jci_jobtitle_target WHERE group_id = " & gid & "  ORDER BY job_title_name_en"
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblJobTypeSelect.DataSource = ds
            lblJobTypeSelect.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTargetAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_target WHERE 1 =1 "
            If mode = "edit" Then
                sql &= " AND target_id NOT IN (SELECT target_id FROM jci_main_target WHERE group_id = " & gid & ") "
            End If


            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            txttarget_all.DataSource = ds
            txttarget_all.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTargetSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_main_target WHERE group_id =  " & gid

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            txttarget_select.DataSource = ds
            txttarget_select.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindIPAddressSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_ip_target WHERE group_id =  " & gid

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblIP.DataSource = ds
            lblIP.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("admin_question_master.aspx?tid=" & tid)
    End Sub

    Sub isHasRow(ByVal table As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Dim pk As String

        pk = getPK("group_id", "jci_master_group", conn)
        gid = pk

        sql = "SELECT * FROM " & table & " WHERE group_id = " & gid
        ' Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage)
        End If

        If ds.Tables("t1").Rows.Count <= 0 Then
            sql = "INSERT INTO " & table & " (group_id) VALUES( "
            sql &= "" & pk & ""
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If
        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Dim pk As String = "0"
        Try

            If mode = "add" Then
                isHasRow("jci_master_group")
            End If

            sql = "UPDATE jci_master_group SET group_name_th = '" & addslashes(txtgroup_th.Value) & "' "
            sql &= " , group_name_en = '" & addslashes(txtgroup_en.Value) & "' "
            sql &= " , group_fix_ip = '" & addslashes(txtip.Text) & "' "
            sql &= " , group_remark = '" & addslashes(txtdetail.Value) & "' "
            sql &= " , test_id = '" & tid & "' "
            sql &= " , create_by_emp_code = '" & Session("jci_emp_code").ToString & "' "
            sql &= " , create_by_emp_name = '" & Session("jci_user_fullname").ToString & "' "
            sql &= " , create_date = GETDATE() "
            sql &= " , create_date_ts = '" & Date.Now.Ticks & "' "
            sql &= " , num_question = '" & txtqty.Text & "' "
            sql &= " , zone_no = '" & txtzone.Text & "' "
            sql &= " WHERE group_id = " & gid
            '  sql &= " , test_name_en = '" & addslashes(txttest_en.Value) & "' "
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            If mode = "edit" Then
                sql = "DELETE FROM jci_jobtitle_target WHERE group_id = " & gid
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                sql = "DELETE FROM jci_ip_target WHERE group_id = " & gid
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                sql = "DELETE FROM jci_main_target WHERE group_id = " & gid
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If

            For i As Integer = 0 To lblJobTypeSelect.Items.Count - 1
                pk = getPK("target_jobtype_id", "jci_jobtitle_target", conn)
                sql = "INSERT INTO jci_jobtitle_target (target_jobtype_id , group_id , job_title_name_th , job_title_name_en) VALUES( "
                sql &= "'" & pk & "' ,"
                sql &= "'" & gid & "' ,"
                sql &= "'" & addslashes(lblJobTypeSelect.Items(i).Value) & "' ,"
                sql &= "'" & addslashes(lblJobTypeSelect.Items(i).Value) & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            For i As Integer = 0 To lblIP.Items.Count - 1

                sql = "INSERT INTO jci_ip_target (ip_address , group_id) VALUES( "
                sql &= "'" & lblIP.Items(i).Value & "' ,"
                sql &= "'" & gid & "' "
              
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            For i As Integer = 0 To txttarget_select.Items.Count - 1
                pk = getPK("jci_target_id", "jci_main_target", conn)
                sql = "INSERT INTO jci_main_target (jci_target_id , group_id , target_id , target_name) VALUES( "
                sql &= "'" & pk & "' ,"
                sql &= "'" & gid & "' ,"
                sql &= "'" & txttarget_select.Items(i).Value & "' ,"
                sql &= "'" & txttarget_select.Items(i).Text & "' "
                sql &= ")"

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
        Finally

        End Try

        Response.Redirect("admin_question_master.aspx?tid=" & tid)
    End Sub

    Protected Sub cmdSearchJobType_Click(sender As Object, e As System.EventArgs) Handles cmdSearchJobType.Click
        bindJobTypeAll()
    End Sub

    Protected Sub cmdAddType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddType.Click
        While lblJobTypeAll.Items.Count > 0 AndAlso lblJobTypeAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblJobTypeAll.SelectedItem
            selectedItem.Selected = False
            lblJobTypeSelect.Items.Add(selectedItem)
            lblJobTypeAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveType.Click
        While lblJobTypeSelect.Items.Count > 0 AndAlso lblJobTypeSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblJobTypeSelect.SelectedItem
            selectedItem.Selected = False
            lblJobTypeAll.Items.Add(selectedItem)
            lblJobTypeSelect.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdAddTarget_Click(sender As Object, e As System.EventArgs) Handles cmdAddTarget.Click
        While txttarget_all.Items.Count > 0 AndAlso txttarget_all.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txttarget_all.SelectedItem
            selectedItem.Selected = False
            txttarget_select.Items.Add(selectedItem)
            txttarget_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveTarget_Click(sender As Object, e As System.EventArgs) Handles cmdRemoveTarget.Click
        While txttarget_select.Items.Count > 0 AndAlso txttarget_select.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txttarget_select.SelectedItem
            selectedItem.Selected = False
            txttarget_all.Items.Add(selectedItem)
            txttarget_select.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdAddIP_Click(sender As Object, e As System.EventArgs) Handles cmdAddIP.Click
        lblIP.Items.Add(txtip.Text)
        txtip.Text = ""
    End Sub

    Protected Sub cmdIPDel_Click(sender As Object, e As System.EventArgs) Handles cmdIPDel.Click
        If lblIP.Items.Count <= 0 Then
            Return
        End If

        While lblIP.Items.Count > 0 AndAlso lblIP.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblIP.SelectedItem
            selectedItem.Selected = False
            lblIP.Items.Remove(selectedItem)
        End While
      
    End Sub

    Protected Sub cmdClearScore_Click(sender As Object, e As System.EventArgs) Handles cmdClearScore.Click
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Try
            sql = "DELETE FROM jci_trans_list WHERE trans_create_by_emp_code = " & txtempcode.Text & " AND game_group_id = " & gid
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()
            txtempcode.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
