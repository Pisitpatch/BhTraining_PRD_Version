Imports System.Data
Imports ShareFunction

Partial Class srp_srp_self_register
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
            bindNoteCommbo()
            bindVoucher1()
            bindVoucher2()
            bindVoucherGrid()
            bindGrid()
            div_complete_register.Visible = False
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
            sql = "SELECT * , CASE WHEN movememt_type_id = 1 THEN '' + CAST(card_id AS varchar) WHEN movememt_type_id = 3 THEN 'แลกซื้อของ' ELSE '-' END AS move_name   "
            sql &= " , CASE WHEN ISNULL(is_register,0) = 0 THEN '<span style=''color:red''>Waiting for review</span>' WHEN ISNULL(is_register,0) = 1 THEN '<span style=''color:green''>Registered<span>' ELSE 'Error' END AS status  "
            sql &= " FROM srp_point_movement_temp a INNER JOIN user_profile b ON a.emp_code = b.emp_code "
            sql &= " LEFT OUTER JOIN shop_master_item c ON a.reserve_item_id1 = c.inv_item_id"
            sql &= " WHERE movememt_type_id IN (1,4) "
            sql &= " AND a.emp_code = " & Session("emp_code").ToString
          
            If txtfind_card.Text <> "" Then
                sql &= " AND card_id =  " & txtfind_card.Text
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND movement_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
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

    Sub bindNoteCommbo()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM star_m_note ORDER BY note_th "
            sql = "SELECT admire_id AS note_id , admire_topic AS note_th FROM star_m_admire ORDER BY admire_topic"
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

    Sub bindVoucher1()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM star_m_note ORDER BY note_th "
            sql = "SELECT inv_item_id , inv_item_name_en FROM shop_master_item WHERE is_voucher = 1 AND is_active = 1 ORDER BY inv_item_name_en"
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtvoucher1.DataSource = ds
            txtvoucher1.DataBind()
            txtvoucher1.Items.Insert(0, New ListItem("-- กรุณาเลือก --", "0"))
            'Response.End()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindVoucher2()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM star_m_note ORDER BY note_th "
            sql = "SELECT inv_item_id , inv_item_name_en FROM shop_master_item WHERE is_voucher = 1 AND is_active = 1 ORDER BY inv_item_name_en"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtvoucher2.DataSource = ds
            txtvoucher2.DataBind()
            txtvoucher2.Items.Insert(0, New ListItem("-- กรุณาเลือก --", "0"))

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindVoucherGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT inv_item_id , ISNULL(total_point,0) AS total_point , ISNULL(inv_price,0) AS total_price , ISNULL(reserve_num,0) AS reserve_num , ISNULL(on_hand,0) AS on_hand , picture_binary , inv_item_name_en , inv_item_name_th , inv_remark , CASE WHEN is_voucher = 1 THEN '' ELSE CASE WHEN ISNULL(inv_price,0) = 0 THEN '' ELSE 'คำนวณคะแนน' END END AS is_voucher FROM shop_master_item WHERE ISNULL(is_delete,0) = 0 AND is_active = 1 AND is_voucher = 1 "
          

            sql &= " ORDER BY item_update_date_ts DESC"

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            PicturesListView.DataSource = ds
            PicturesListView.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSearchCard_Click(sender As Object, e As System.EventArgs) Handles cmdSearchCard.Click
        Dim sql As String
        Dim ds As New DataSet
        Dim ds2 As New DataSet

        Try
            If txtadd_cardid.Text = "" Then
                cmdSave.Enabled = False
                Return
            End If

            sql = "SELECT mgr_emp_code , mgr_emp_name FROM srp_m_quarter_issue WHERE " & txtadd_cardid.Text
            sql &= " BETWEEN card_id_start AND card_id_end"
            sql &= " AND " & txtadd_cardid.Text & " NOT IN (SELECT card_id FROM srp_point_movement WHERE movememt_type_id = 1 )"
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            If ds.Tables("t1").Rows.Count > 0 Then
                txtadd_award.Text = ds.Tables("t1").Rows(0)("mgr_emp_name").ToString
                txtadd_award_empcode.Text = ds.Tables("t1").Rows(0)("mgr_emp_code").ToString
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
                    txtadd_award_empcode.Text = ds.Tables("tIssue").Rows(0)("mgr_emp_no").ToString
                    lblAwardEmpCode.Text = ds.Tables("tIssue").Rows(0)("mgr_emp_no").ToString
                Else
                    txtadd_award.Text = ""
                    txtadd_award_empcode.Text = ""
                    lblAwardEmpCode.Text = "ไม่พบหมายเลข Card นี้ในฐานข้อมูล <br/>กรุณาระบุชื่อและรหัสพนักผู้แจก Card ในช่องข้อความด้านล่าง"
                End If

                '  Button1.Enabled = False
            End If

            sql = "SELECT r_award_by_emp_code AS mgr_emp_code , r_award_by_emp_name AS mgr_emp_name FROM srp_point_movement WHERE movememt_type_id = 1 AND card_id = " & txtadd_cardid.Text
            ds2 = conn.getDataSetForTransaction(sql, "t2")

            If ds2.Tables("t2").Rows.Count > 0 Then
                ' MsgBox("Card ถูกลงทะเบียนแล้ว")
                'lblRegister.Text = "Card ถูกลงทะเบียนแล้ว "
                'txtregister.Focus()
                txtadd_award.Text = ""
                txtadd_award_empcode.Text = ""
                lblAwardEmpCode.Text = "Card ถูกลงทะเบียนแล้ว ไม่สามารถลงทะเบียนซ้ำได้อีก"
                cmdSave.Enabled = False
            Else
                ' MsgBox("ไม่พบรายการ")
                'lblRegister.Text = "ไม่พบรายการ "
                'txtregister.Focus()

                cmdSave.Enabled = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Try
            If chkbi1.Checked = True Or chkbi5.Checked = True Or chkbi9.Checked = True Or chkbi10.Checked = True Then
                chk2015_1.Checked = True
            End If

            If chkbi4.Checked = True Then
                chk2015_2.Checked = True
            End If

            If chkbi3.Checked = True Or chkbi6.Checked = True Then
                chk2015_3.Checked = True
            End If

            If chkbi2.Checked = True Or chkbi7.Checked = True Or chkbi8.Checked = True Then
                chk2015_4.Checked = True
            End If

            pk = getPK("point_movement_id", "srp_point_movement_temp", conn)
            sql = "INSERT INTO srp_point_movement_temp (point_movement_id , emp_code , emp_name , card_id , transaction_name , reward_type_id "
            sql &= " , reward_type_name , movememt_type_id , movememt_type_name , point_trans , movement_remark , movement_create_by "
            sql &= " , movement_create_date_ts , movement_create_date_raw , r_award_by_emp_code , r_award_by_emp_name , r_award_date_raw  , r_award_date_ts "
            sql &= " , r_note_id , r_note_name , r_note_other , r_out_chk1 , r_out_chk2 , r_out_chk3 , r_out_chk4 , r_out_chk5 , r_out_chk6 "
            sql &= ", r_is_care , r_is_clear , r_is_smart1 , r_is_smart2 , r_date_ts , r_date_raw "
            sql &= ", r_new_bi1 , r_new_bi2 , r_new_bi3 , r_new_bi4 , r_new_bi5 , r_new_bi6 , r_new_bi7 , r_new_bi8 , r_new_bi9 , r_new_bi10 , r_new_bi11 , r_new_bi11_remark "

            sql &= ", r_2015_1 , r_2015_2 , r_2015_3 , r_2015_4 "
            sql &= " , reserve_item_id1 , reserve_item_id2 , point_temp"
            sql &= ") VALUES("
            sql &= " '" & pk & "' , "
            sql &= " '" & Session("emp_code").ToString & "' , "
            sql &= " '" & addslashes(Session("user_fullname").ToString) & "' , "
            sql &= " '" & txtadd_cardid.Text & "' , "
            sql &= " '' , "
            sql &= " 1 , "
            sql &= " 'On-the-Spot' , "
            sql &= " 1 , "
            sql &= " 'Register' , "

            If chkbi3.Checked Then
                sql &= " " & 200 & " , "
            Else
                sql &= " " & 100 & " , "
            End If

            sql &= " '" & addslashes(txtadd_remark.Text) & "' , "
            sql &= " '" & Session("user_fullname").ToString & "' , "
            sql &= " '" & Date.Now.Ticks & "' , "
            sql &= " GETDATE() , "
            sql &= " '" & addslashes(txtadd_award_empcode.Text) & "' , "
            sql &= " '" & addslashes(txtadd_award.Text) & "' , "
            sql &= " '" & convertToSQLDatetime(txtdate.Text) & "' , "
            sql &= " '" & ConvertDateStringToTimeStamp(txtdate.Text) & "' , "
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

            sql &= " '" & Date.Now.Ticks & "' ,"
            sql &= " GETDATE() ,"

            If chkbi1.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkbi2.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkbi3.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkbi4.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkbi5.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkbi6.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkbi7.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkbi8.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkbi9.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkbi10.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chkSpecial.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            sql &= " '" & addslashes(txtspecial_remark.Text) & "' , "

            If chk2015_1.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk2015_2.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk2015_3.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0 , "
            End If

            If chk2015_4.Checked = True Then
                sql &= " 1 , "
            Else
                sql &= " 0  , "
            End If

            sql &= " " & txtvoucher1.SelectedValue & " ,"
            sql &= " " & txtvoucher2.SelectedValue & " , "
            sql &= " " & txtscore.SelectedValue & " "
            sql &= ")"
            'Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_award.Text = ""
            txtadd_cardid.Text = ""
            txtadd_detail_combo.SelectedIndex = 0
            '  txtadd_empcode.Text = ""
            '   txth_empname.Value = ""
            txtadd_award_empcode.Text = ""
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

            div_search.Visible = True
            mytabber1.Visible = False
            div_complete_register.Visible = True
        Catch ex As Exception
            div_complete_register.Visible = False
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub txtadd_detail_combo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtadd_detail_combo.SelectedIndexChanged
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            If txtadd_detail_combo.SelectedIndex <= 0 Then
                Return
            End If

            sql = "SELECT * FROM star_m_admire WHERE admire_id = " & txtadd_detail_combo.SelectedValue
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                If ds.Tables("t1").Rows(0)("is_clear").ToString = "1" Then
                    chk_clear.Checked = True
                Else
                    chk_clear.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("is_care").ToString = "1" Then
                    chk_care.Checked = True
                Else
                    chk_care.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("is_smart").ToString = "1" Then
                    chk_smart1.Checked = True
                Else
                    chk_smart1.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("is_quality").ToString = "1" Then
                    chk_smart2.Checked = True
                Else
                    chk_smart2.Checked = False
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()

        mytabber1.Visible = False
        div_complete_register.Visible = False
    End Sub

    Protected Sub gridview1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview1.PageIndexChanging
        gridview1.PageIndex = e.NewPageIndex
        bindGrid()

        mytabber1.Visible = True
        div_complete_register.Visible = False
    End Sub

    Protected Sub gridview1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview1.SelectedIndexChanged

    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As System.EventArgs) Handles cmdNew.Click
        mytabber1.Visible = True
        div_search.Visible = False
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As System.EventArgs) Handles cmdCancel.Click
        txtadd_award.Text = ""
        txtadd_cardid.Text = ""
        txtadd_detail_combo.SelectedIndex = 0
        '  txtadd_empcode.Text = ""
        '   txth_empname.Value = ""
        txtadd_award_empcode.Text = ""
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
        mytabber1.Visible = False
        div_search.Visible = True
        mytabber1.Visible = False
        div_complete_register.Visible = False
    End Sub
End Class
