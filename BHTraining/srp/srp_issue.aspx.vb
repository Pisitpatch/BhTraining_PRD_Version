Imports System.Data
Imports ShareFunction

Partial Class srp_srp_issue
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Request.QueryString("viewtype")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Page.IsPostBack Then
        Else ' First time load
            bindGrid()
            bindDept()
            txtadd_issueby.Text = Session("user_fullname").ToString

            lblApproveName.Text = Session("user_fullname").ToString
            lblYourLevel.Text = Session("job_title").ToString

            If viewtype = "dept" Then
                cmdDelete.Visible = False
                cmdUpdate.Visible = False
                div_dept.Visible = True
                mytabber1.Visible = False
            Else
                ' gridview1.Columns(0).Visible = False
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , ISNULL(c.num,0) AS register_num , ISNULL(d.quota_qty,0) AS quota_qty1 FROM srp_card_issue_list a INNER JOIN user_profile b ON a.mgr_emp_no = b.emp_code "

            sql &= " LEFT OUTER JOIN (SELECT ISNULL(COUNT(*),0) AS num , r_award_by_emp_code "
            sql &= " FROM srp_point_movement GROUP BY r_award_by_emp_code ) c ON a.mgr_emp_no = c.r_award_by_emp_code "

            sql &= " LEFT OUTER JOIN (SELECT quater_no , year_no , mgr_emp_code , quota_qty  FROM srp_m_quarter_issue "
            sql &= " GROUP BY  quater_no , year_no , mgr_emp_code , quota_qty ) d ON a.mgr_emp_no = d.mgr_emp_code AND a.quater_no = d.quater_no AND a.year_no = d.year_no "

            sql &= " WHERE 1 = 1 "
            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.issue_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text)
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND b.dept_id = " & txtdept.SelectedValue
            End If

            If txtfind_id.Text <> "" Then
                sql &= " AND " & txtfind_id.Text & " BETWEEN card_id_start AND card_id_end "
            End If

            If txtfind_project.Text <> "" Then
                sql &= " AND project_name LIKE '%" & txtfind_project.Text & "%' "
            End If

            If txtfind_empcode.Text <> "" Then
                sql &= " AND mgr_emp_no LIKE '%" & txtfind_empcode.Text & "%' "
            End If

            If txtfind_empname.Text <> "" Then
                sql &= " AND mgr_name LIKE '%" & txtfind_empname.Text & "%' "
            End If

            sql &= " ORDER BY issue_date_ts DESC"

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            gridview1.DataSource = ds
            gridview1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
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

 

    Protected Sub cmdSubmit_Click(sender As Object, e As System.EventArgs) Handles cmdSubmit.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim quarter As Integer = (Date.Now.Month - 1) \ 3 + 1
        Dim ds As New DataSet

        If txtadd_empcode.Text = "" Then
            Return
        End If

        Try

            sql = "SELECT * FROM srp_card_issue_list WHERE quater_no = " & quarter
            sql &= " AND year_no = " & Date.Now.Year
            sql &= " AND mgr_emp_no = " & txtadd_empcode.Text

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count = 0 Then
                pk = getPK("issue_id", "srp_card_issue_list", conn)
                sql = "INSERT INTO srp_card_issue_list (issue_id , quater_no , year_no , mgr_emp_no , mgr_name "
                sql &= " , project_name , issue_qty , card_id_start , card_id_end , issue_date_ts , issue_date_raw "
                sql &= " , issue_remark , issue_by"
                sql &= ") VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & quarter & "' ,"
                sql &= " '" & Date.Now.Year & "' ,"
                sql &= " '" & txtadd_empcode.Text & "' ,"
                sql &= " '" & lblname.Text & "' ,"
                sql &= " '" & addslashes(txtadd_project.Text) & "' ,"
                sql &= " '" & txtadd_qty.Text & "' ,"
                sql &= " '" & txtadd_start.Text & "' ,"
                sql &= " '" & txtadd_end.Text & "' ,"
                sql &= " '" & ConvertDateStringToTimeStamp(txtissue_date.Text) & "' ,"
                sql &= " '" & convertToSQLDatetime(txtissue_date.Text) & "' ,"
                sql &= " '" & addslashes(txtadd_note.Text) & "' ,"
                sql &= " '" & txtadd_issueby.Text & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Else

                sql = "UPDATE srp_card_issue_list SET project_name = '" & addslashes(txtadd_project.Text) & "' "
                sql &= " , issue_qty = '" & txtadd_qty.Text & "' "
                sql &= " , card_id_start = '" & txtadd_start.Text & "' "
                sql &= " , card_id_end = '" & txtadd_end.Text & "' "
                sql &= " , issue_date_ts = '" & ConvertDateStringToTimeStamp(txtissue_date.Text) & "' "
                sql &= " , issue_date_raw = '" & convertToSQLDatetime(txtissue_date.Text) & "' "
                sql &= " , issue_remark = '" & addslashes(txtadd_note.Text) & "' "
                sql &= " , issue_by = '" & txtadd_issueby.Text & "' "
                sql &= " WHERE quater_no = " & quarter
                sql &= " AND year_no = " & Date.Now.Year
                sql &= " AND mgr_emp_no = " & txtadd_empcode.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If
          

            conn.setDBCommit()
            history.Visible = False
            txtadd_empcode.Text = ""
            lblname.Text = ""
            txtissue_date.Text = ""
            txtadd_qty.Text = ""
            txtadd_start.Text = ""
            txtadd_end.Text = ""
            txtadd_project.Text = ""
            txtadd_note.Text = ""

            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gridview1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview1.PageIndexChanging
        gridview1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub gridview1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridview1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblImage1 As Label = CType(e.Row.FindControl("lblImage1"), Label)
            Dim lblPk As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM srp_card_issue_manager_comment WHERE issue_id = " & lblPk.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    If ds.Tables("t1").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                        lblImage1.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve'  title='" & ds.Tables("t1").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t1").Rows(i)("detail").ToString & "' />"
                    ElseIf ds.Tables("t1").Rows(i)("comment_status_id").ToString = "2" Then
                        lblImage1.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & ds.Tables("t1").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t1").Rows(i)("detail").ToString & "' />"
                    ElseIf ds.Tables("t1").Rows(i)("comment_status_id").ToString = "3" Then
                        lblImage1.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='" & ds.Tables("t1").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t1").Rows(i)("detail").ToString & "' />"
                    Else
                        lblImage1.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                    End If
                Next i

                If ds.Tables("t1").Rows.Count = 0 Then
                    lblImage1.Text = "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                    chkSelect.Visible = True
                Else
                    If viewtype <> "dept" Then
                        chkSelect.Visible = False
                    End If

                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub gridview1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview1.SelectedIndexChanged

    End Sub

    Protected Sub cmdFind_Click(sender As Object, e As System.EventArgs) Handles cmdFind.Click
        Dim sql As String
        Dim ds As New DataSet

        Try
            If txtadd_empcode.Text.Trim = "" Then
                Return
            End If

            sql = "SELECT * FROM user_profile WHERE (emp_code = '" & txtadd_empcode.Text & "' OR user_fullname LIKE '%" & txtadd_empcode.Text & "%') "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                lblname.Text = ds.Tables("t1").Rows(0)("user_fullname").ToString

                history.Visible = True
                ' Find in quota issue
                sql = "SELECT ISNULL(SUM(issue_qty),0) AS num , ISNULL(MAX(quota_qty),0) AS quota FROM srp_m_quarter_issue WHERE mgr_emp_code = " & txtadd_empcode.Text
                sql &= " GROUP BY mgr_emp_code "
                ds = conn.getDataSetForTransaction(sql, "t2")
                If ds.Tables("t2").Rows.Count > 0 Then
                    txtqty.Value = ds.Tables("t2").Rows(0)("num").ToString
                    txtquota.Value = ds.Tables("t2").Rows(0)("quota").ToString
                Else
                    txtqty.Value = 0
                    txtquota.Value = 0
                End If

                ' Find in srp_card_issue_list
                sql = "SELECT ISNULL(SUM(issue_qty),0) AS num FROM srp_card_issue_list WHERE mgr_emp_no =  " & txtadd_empcode.Text
                sql &= " AND issue_id IN (SELECT issue_id FROM srp_card_issue_manager_comment WHERE comment_status_id = 1)"
                sql &= " GROUP BY mgr_emp_no "
                ds = conn.getDataSetForTransaction(sql, "tIssue")
                If ds.Tables("tIssue").Rows.Count > 0 Then
                    '  txtqty.Value += CInt(ds.Tables("tIssue").Rows(0)("num").ToString)
                    txtspecial.Value = CInt(ds.Tables("tIssue").Rows(0)("num").ToString)
                Else
                  
                End If

                ' Find Registered card
                sql = "SELECT ISNULL(COUNT(*),0) AS num FROM srp_point_movement WHERE r_award_by_emp_code = " & txtadd_empcode.Text
                ds = conn.getDataSetForTransaction(sql, "t3")
                If ds.Tables("t3").Rows.Count > 0 Then
                    txtregister.Value = ds.Tables("t3").Rows(0)("num").ToString
                Else
                    txtregister.Value = 0
                End If

            Else
                history.Visible = False
                lblname.Text = "Employee is not found."
            End If


        Catch ex As Exception
            history.Visible = False
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try

    End Sub

    Protected Sub txtadd_start_TextChanged(sender As Object, e As System.EventArgs) Handles txtadd_start.TextChanged
        If txtadd_qty.Text = "" Then
            Return
        End If
        Try
            txtadd_end.Text = CInt(txtadd_qty.Text) + (CInt(txtadd_start.Text) - 1)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtadd_qty_TextChanged(sender As Object, e As System.EventArgs) Handles txtadd_qty.TextChanged
        If txtadd_start.Text = "" Then
            Return
        End If
        Try
            txtadd_end.Text = CInt(txtadd_qty.Text) + (CInt(txtadd_start.Text) - 1)
        Catch ex As Exception

        Finally
            txtadd_start.Focus()
        End Try
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub

    Protected Sub cmdDeptStatus_Click(sender As Object, e As System.EventArgs) Handles cmdDeptStatus.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chkSelect As CheckBox
        Dim login_max_authen As Integer = 0

        i = gridview1.Rows.Count
        Dim pk As String = ""
        Try
            ' login_max_authen = getMyIPDLevel()
            For s As Integer = 0 To i - 1
                chkSelect = CType(gridview1.Rows(s).FindControl("chkSelect"), CheckBox)

                If chkSelect.Checked = True Then
                    lbl = CType(gridview1.Rows(s).FindControl("lblPK"), Label)

                    pk = getPK("comment_id", "srp_card_issue_manager_comment", conn)
                    sql = "INSERT INTO srp_card_issue_manager_comment (comment_id , issue_id , comment_status_id , comment_status_name , subject_id , subject , detail "
                    sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
                    sql &= ",create_date , create_date_ts "
                    sql &= ") VALUES("
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & lbl.Text & "' ,"
                    sql &= " '" & txtdeptstatus.SelectedValue & "' ,"
                    sql &= " '" & txtdeptstatus.SelectedItem.Text & "' ,"
                    sql &= " '" & "" & "' ,"
                    sql &= " '" & "" & "' ,"
                    sql &= " '" & addslashes(txtremark.Text) & "' ,"
                    sql &= " '" & Session("job_title").ToString & "' ,"
                    sql &= " '" & Session("user_position").ToString & "' ,"
                    sql &= " '" & Session("user_fullname").ToString & "' ,"
                    sql &= " '" & Session("emp_code").ToString & "' ,"
                    sql &= " '" & Session("dept_name").ToString & "' ,"
                    sql &= " '" & Session("costcenter_id").ToString & "' ,"
                    sql &= " GETDATE() ,"
                    sql &= " '" & Date.Now.Ticks & "' "


                    sql &= ")"

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)

                    End If

                    ' updateOnlyLog("0", lbl.Text, txtdeptstatus.SelectedItem.Text)

                    '  updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
                End If
            Next s


            conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chkSelect As CheckBox
        Dim login_max_authen As Integer = 0

        i = gridview1.Rows.Count
        Dim pk As String = ""
        Try
            ' login_max_authen = getMyIPDLevel()
            For s As Integer = 0 To i - 1
                chkSelect = CType(gridview1.Rows(s).FindControl("chkSelect"), CheckBox)

                If chkSelect.Checked = True Then
                    lbl = CType(gridview1.Rows(s).FindControl("lblPK"), Label)

                    sql = "DELETE FROM srp_card_issue_list WHERE issue_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)

                    End If

                    ' updateOnlyLog("0", lbl.Text, txtdeptstatus.SelectedItem.Text)

                    '  updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
                End If
            Next s


            conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chkSelect As CheckBox
        Dim text As TextBox
        Dim login_max_authen As Integer = 0

        i = gridview1.Rows.Count
        Dim pk As String = ""
        Try
            ' login_max_authen = getMyIPDLevel()
            For s As Integer = 0 To i - 1
                chkSelect = CType(gridview1.Rows(s).FindControl("chkSelect"), CheckBox)
                text = CType(gridview1.Rows(s).FindControl("lblRemark"), TextBox)

                'If chkSelect.Checked = True Then
                lbl = CType(gridview1.Rows(s).FindControl("lblPK"), Label)

                sql = "UPDATE  srp_card_issue_list SET issue_remark = '" & addslashes(text.Text) & "' WHERE issue_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)

                End If

                ' updateOnlyLog("0", lbl.Text, txtdeptstatus.SelectedItem.Text)

                '  updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
                'End If
            Next s


            conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub
End Class
