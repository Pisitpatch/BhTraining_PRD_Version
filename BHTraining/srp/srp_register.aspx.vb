Imports System.Data
Imports ShareFunction

Partial Class srp_srp_register
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If


        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Page.IsPostBack Then
        Else ' First time load
            bindHour()
            bindMinute()
            bindGrid()
            bindNoteCommbo()
            bindDept()
            bindEmployee()
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
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , CASE WHEN movememt_type_id = 1 THEN '' + CAST(card_id AS varchar) WHEN movememt_type_id = 3 THEN 'แลกซื้อของ' ELSE '-' END AS move_name  FROM srp_point_movement a "
            sql &= " INNER JOIN user_profile b ON a.emp_code = b.emp_code "
            sql &= " WHERE movememt_type_id = 1 "
            If txtfind_mgr.Text <> "" Then
                sql &= " AND (r_award_by_emp_code LIKE '%" & txtfind_mgr.Text & "%' OR  r_award_by_emp_name LIKE '%" & txtfind_mgr.Text & "%') "
            End If
            If txtfind_card.Text <> "" Then
                sql &= " AND card_id =  " & txtfind_card.Text
            End If
         
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND movement_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text)
            End If
            If txtdept.SelectedValue <> "" Then
                sql &= " AND b.dept_id = " & txtdept.SelectedValue
            End If
            sql &= " ORDER BY a.movement_create_date_ts DESC"
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            ' Response.Write(sql)
            gridview1.DataSource = ds
            gridview1.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Private Sub bindHour()

        Dim i As Integer = 0
        Dim i_str As String = ""
        For i = 0 To 23
            i_str = i.ToString
            txthour.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
            '  txthour2.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
        Next

        'txthour.Items.Insert(0, New ListItem("hh", "0"))
        ' txthour2.Items.Insert(0, New ListItem("hh", "0"))
    End Sub

    Private Sub bindMinute()
        Dim i As Integer = 0
        Dim i_str As String = ""
        For i = 0 To 59
            i_str = i.ToString
            txtmin.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
            ' txtmin2.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
        Next

        ' txtmin.Items.Insert(0, New ListItem("mm", "0"))
        ' txtmin2.Items.Insert(0, New ListItem("mm", "0"))
        'txtmin2.Items.Insert(0, New ListItem("-", "0"))
        'txtmin2.SelectedIndex = 0


    End Sub

    Sub bindNoteCommbo()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM star_m_note ORDER BY note_th "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtadd_detail_combo.DataSource = ds
            txtadd_detail_combo.DataBind()

            txtadd_detail_combo.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))

         
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDept()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM user_dept ORDER BY dept_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdept.DataSource = ds
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindEmployee()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * , ISNULL(ISNULL(user_fullname,user_fullname_local),'') AS user_fullname1 FROM user_profile  WHERE 1 = 1 "
            sql &= " AND emp_code IN (SELECT Employee_id FROM temp_BHUser)"
            If txtadd_empcode.Text <> "" Then
                sql &= " AND (emp_code LIKE '%" & txtadd_empcode.Text & "%' OR LOWER(user_fullname) LIKE '%" & txtadd_empcode.Text.ToLower & "%') "
            End If

            sql &= " ORDER BY user_fullname"
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtemplist.DataSource = ds
            txtemplist.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Try
            pk = getPK("point_movement_id", "srp_point_movement", conn)
            sql = "INSERT INTO srp_point_movement (point_movement_id , emp_code , emp_name , card_id , transaction_name , reward_type_id "
            sql &= " , reward_type_name , movememt_type_id , movememt_type_name , point_trans , movement_remark , movement_create_by "
            sql &= " , movement_create_date_ts , movement_create_date_raw , r_award_by_emp_code , r_award_by_emp_name "
            sql &= " , r_note_id , r_note_name , r_note_other , r_out_chk1 , r_out_chk2 , r_out_chk3 , r_out_chk4 , r_out_chk5 , r_out_chk6 "
            sql &= ", r_is_care , r_is_clear , r_is_smart1 , r_is_smart2 , r_date_ts , r_date_raw "
            sql &= ") VALUES("
            sql &= " '" & pk & "' , "
            sql &= " '" & txtemplist.SelectedValue & "' , "
            sql &= " '" & addslashes(txtemplist.SelectedItem.Text) & "' , "
            sql &= " '" & txtadd_cardid.Text & "' , "
            sql &= " '' , "
            sql &= " 1 , "
            sql &= " 'On-the-Spot' , "
            sql &= " 1 , "
            sql &= " 'Register' , "

            sql &= " " & txtpoint.SelectedValue & " , "
            sql &= " '" & addslashes(txtadd_remark.Text) & "' , "
            sql &= " '" & Session("user_fullname").ToString & "' , "
            sql &= " '" & Date.Now.Ticks & "' , "
            sql &= " GETDATE() , "
            sql &= " '" & addslashes(lblAwardEmpCode.Text) & "' , "
            sql &= " '" & addslashes(txtadd_award.Text) & "' , "
            sql &= " '" & txtadd_detail_combo.SelectedValue & "' , "
            sql &= " '" & txtadd_detail_combo.SelectedItem.Text & "' , "
            sql &= " '" & addslashes(txtadd_note.Text) & "' , "
            If chk1.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If
           
            If chk2.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If
            If chk3.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If
            If chk4.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If
            If chk5.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk6.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_care.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_clear.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_smart1.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk_smart2.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            sql &= " '" & ConvertDateStringToTimeStamp(txtdate_report.Text, txthour.SelectedValue, txtmin.SelectedValue) & "' ,"
            sql &= " '" & convertToSQLDatetime(txtdate_report.Text, txthour.SelectedValue, txtmin.SelectedValue) & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_award.Text = ""
            txtadd_cardid.Text = ""
            txtadd_detail_combo.SelectedIndex = 0
            txtadd_empcode.Text = ""
            '   txth_empname.Value = ""
            lblAwardEmpCode.Text = ""
            txtadd_note.Text = ""
            txtadd_remark.Text = ""
            chk1.Checked = False
            chk2.Checked = False
            chk3.Checked = False
            chk4.Checked = False
            chk5.Checked = False
            chk6.Checked = False
            chk_care.Checked = False
            chk_clear.Checked = False
            chk_smart1.Checked = False
            chk_smart2.Checked = False
            '  Button1.Enabled = False
            bindGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub gridview1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview1.PageIndexChanging
        gridview1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub gridview1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview1.SelectedIndexChanged

    End Sub

    Protected Sub cmdSearchCard_Click(sender As Object, e As System.EventArgs) Handles cmdSearchCard.Click
        Dim sql As String
        Dim ds As New DataSet

        Try
            If txtadd_cardid.Text = "" Then
                Return
            End If

            sql = "SELECT mgr_emp_code , mgr_emp_name FROM srp_m_quarter_issue WHERE " & txtadd_cardid.Text
            sql &= " BETWEEN card_id_start AND card_id_end"
            sql &= " AND " & txtadd_cardid.Text & " NOT IN (SELECT card_id FROM srp_point_movement WHERE movememt_type_id = 1 )"
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            If ds.Tables("t1").Rows.Count > 0 Then
                txtadd_award.Text = ds.Tables("t1").Rows(0)("mgr_emp_name").ToString
                lblAwardEmpCode.Text = ds.Tables("t1").Rows(0)("mgr_emp_code").ToString
                '  Button1.Enabled = True
            Else

                ' Find in srp_card_issue_list
                sql = "SELECT mgr_name , mgr_emp_no  FROM srp_card_issue_list WHERE  " & txtadd_cardid.Text
                sql &= " BETWEEN card_id_start AND card_id_end "
                sql &= " AND issue_id IN (SELECT issue_id FROM srp_card_issue_manager_comment WHERE comment_status_id = 1)"
                sql &= " AND " & txtadd_cardid.Text & " NOT IN (SELECT card_id FROM srp_point_movement WHERE movememt_type_id = 1 )"
                '  sql &= " GROUP BY mgr_emp_no "

                ds = conn.getDataSetForTransaction(sql, "tIssue")
                If ds.Tables("tIssue").Rows.Count > 0 Then
                    txtadd_award.Text = ds.Tables("tIssue").Rows(0)("mgr_name").ToString
                    lblAwardEmpCode.Text = ds.Tables("tIssue").Rows(0)("mgr_emp_no").ToString
                Else
                    txtadd_award.Text = ""
                    lblAwardEmpCode.Text = "ไม่พบข้อมูลในฐานข้อมูล หรือ ลงทะเบียนหมายเลขนี้ในระบบแล้ว"
                End If
             
                '  Button1.Enabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub

    Protected Sub cmdFindEmp_Click(sender As Object, e As System.EventArgs) Handles cmdFindEmp.Click
        bindEmployee()
    End Sub
End Class
