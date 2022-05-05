Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class star_home
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected session_id As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        session_id = Session("session_myid").ToString
        viewtype = Request.QueryString("viewtype")
        Session("viewtype") = viewtype & ""

        If viewtype <> "" Or viewtype = "public" Then
            cmdNew.Visible = False
        End If

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' First time
            If viewtype = "hr" Then
                lblHeader.Text = " (Coordinator)"
                div_hr.Visible = True

            ElseIf viewtype = "sup" Then
                lblHeader.Text = " (Chair of Star Committee)"
                GridView1.Columns(0).Visible = False
            ElseIf viewtype = "com" Then
                lblHeader.Text = " (Star Committee)"
                GridView1.Columns(0).Visible = False
            Else
                GridView1.Columns(0).Visible = False
            End If

            bindJobType()
            bindDept()
            bindStatus()
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

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("form_star.aspx?mode=add")
    End Sub

    Sub bindJobType()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_type FROM user_profile  GROUP BY job_type ORDER BY job_type"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtjobtype.DataSource = ds
            txtjobtype.DataBind()

            txtjobtype.Items.Insert(0, New ListItem("-- Please Select --", ""))
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Private Sub bindGrid()
        Dim ds As New DataSet
        Dim sql As String
        Try
            ' sql = "SELECT a.* , b.* , c.ir_status_name , d.* , g.form_name_en , a.date_report AS create_date FROM  ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id  "
            sql = "SELECT *  , ISNULL(b.star_no,'-') AS ssip_no1 FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id"
            sql &= " INNER JOIN star_status_list c ON a.status_id = c.status_id"
            sql &= " LEFT OUTER JOIN star_hr_tab d ON a.star_id = d.star_id "
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "

            If txtnominee.Text <> "" Then
                sql &= " AND ("
                sql &= " a.star_id IN (select star_id from star_relate_dept WHERE lower(costcenter_name) LIKE '%" & txtnominee.Text.ToLower & "%' or costcenter_id  LIKE '%" & txtnominee.Text.ToLower & "%' ) OR "
                sql &= " a.star_id IN (select star_id from star_relate_person WHERE lower(user_fullname) LIKE '%" & txtnominee.Text.ToLower & "%' or emp_code  LIKE '%" & txtnominee.Text.ToLower & "%'  ) OR "
                sql &= " a.star_id IN (select star_id from star_relate_doctor WHERE lower(doctor_name) LIKE '%" & txtnominee.Text.ToLower & "%' or emp_code  LIKE '%" & txtnominee.Text.ToLower & "%'  )  "
                sql &= ")"
            End If

            If txtdept.SelectedValue <> "" Then
                'sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
                sql &= " AND ("
                sql &= " a.star_id IN (select star_id from star_relate_dept WHERE costcenter_id = " & txtdept.SelectedValue & " ) OR "
                sql &= " a.star_id IN (select star_id from star_relate_person a1 inner join user_profile b1 ON a1.emp_code = b1.emp_code WHERE b1.dept_id = " & txtdept.SelectedValue & " )  "
                ' sql &= " a.star_id IN (select star_id from star_relate_doctor WHERE lower(doctor_name) LIKE '%" & txtnominee.Text.ToLower & "%' or emp_code  LIKE '%" & txtnominee.Text.ToLower & "%'  )  "
                sql &= ")"
            End If

            If txtpin.Checked = True Then
                sql &= " AND a.report_emp_code IN (SELECT bb.emp_code  FROM star_hr_tab aa INNER JOIN star_relate_person bb ON aa.star_id = bb.star_id "
                sql &= " INNER JOIN star_trans_list cc ON aa.star_id = cc.star_id "
                sql &= " WHERE ISNULL(cc.is_delete,0) = 0 AND ISNULL(cc.is_cancel,0) = 0 AND aa.recognition_type_id = 3 AND cc.status_id IN (5,7) "
                sql &= " GROUP BY bb.emp_code HAVING COUNT(bb.emp_code) >= 5 ) "
            End If

            If txtstarno.Text <> "" Then
                sql &= " AND b.star_no LIKE '%" & txtstarno.Text & "%' "
            End If

            If viewtype = "" Then
                sql &= " AND report_emp_code = " & Session("emp_code").ToString
            End If
          

            If viewtype = "hr" Then
                sql &= " AND a.status_id IN (2,3,4,5,6,7,8)"
            End If

            If viewtype = "nominee" Then
                sql &= " AND a.star_id IN (SELECT star_id FROM star_relate_person WHERE emp_code = " & Session("emp_code").ToString & " )"
                'sql &= " AND a.status_id IN (5,7) "
            End If

            If viewtype = "readonly" Then
                sql &= " AND a.star_id IN (SELECT star_id FROM star_relate_person a1 INNER JOIN user_profile a2 ON a1.emp_code = a2.emp_code "
                sql &= " WHERE a2.dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
                sql &= " )"
                '  sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
                ' sql &= " AND a.status_id = 7 "
            End If

            If viewtype = "sup" Or viewtype = "com" Then
                'sql &= " AND (a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_ssip WHERE emp_code = " & Session("emp_code").ToString & ") "
                'sql &= " )"
                sql &= " AND a.status_id IN (2,3,4,7)" ' During Consider
            End If

            If txtjobtype.SelectedValue <> "" Then
                sql &= " AND "

                sql &= " a.star_id IN (select star_id from star_relate_person a1 INNER JOIN user_profile a2 ON a1.emp_code = a2.emp_code WHERE lower(a2.job_type) LIKE '%" & txtjobtype.SelectedValue.ToLower & "%' ) "
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.submit_date BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

          

            If txtstatus.SelectedValue <> "" Then
                sql &= " AND a.status_id = " & txtstatus.SelectedValue
            End If

          

            sql &= " ORDER BY a.star_id DESC"
            ds = conn.getDataSet(sql, "table1")

            '  Response.Write(sql)
            ' Return
            lblNum.Text = FormatNumber(ds.Tables("table1").Rows.Count, 0)
            GridView1.DataSource = ds
            GridView1.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try

    End Sub

    Sub onCheckAll()

        Dim i As Integer


        Dim chk As CheckBox
        Dim h_chk As CheckBox
        h_chk = CType(GridView1.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)
        i = GridView1.Rows.Count

        Try

            For s As Integer = 0 To i - 1


                chk = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)


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

    Sub bindDept()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM user_dept WHERE 1 = 1 "
            If viewtype = "readonly" Then
                ' sql &= " AND dept_id IN (SELECT costcenter_id FROM user_access_costcenter_ssip WHERE emp_code = " & Session("emp_code").ToString & ") "
            End If
            sql &= " ORDER BY dept_name_en"
            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txtdept.DataSource = reader
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("--All Department--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindStatus()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM star_status_list"
            'sql &= " ORDER BY dept_name"
            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txtstatus.DataSource = reader
            txtstatus.DataBind()



            txtstatus.Items.Insert(0, New ListItem("--All Status--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim sql As String
        Dim errorMsg As String = ""

        If (e.CommandName = "cancelCommand") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            ' Dim row As GridViewRow = GridView1.Rows(index)


            ' Add code here to add the item to the shopping cart.
            Try
                sql = "UPDATE star_trans_list SET is_delete = 1 WHERE star_id = " & index
                conn.executeSQL(sql)
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                bindGrid()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        ElseIf (e.CommandName = "cancelCommandByTQM") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            ' Dim row As GridViewRow = GridView1.Rows(index)


            ' Add code here to add the item to the shopping cart.
            Try
                sql = "UPDATE star_trans_list SET is_cancel = 1 WHERE star_id = " & index
                conn.executeSQL(sql)
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                bindGrid()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim HyperLink1 As HyperLink = CType(e.Row.FindControl("HyperLink1"), HyperLink)
            Dim lblPk As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim lblStatusId As Label = CType(e.Row.FindControl("lblStatusId"), Label)
            Dim lblStatusName As Label = CType(e.Row.FindControl("lblStatusName"), Label)

            Dim lblDeptNum As Label = CType(e.Row.FindControl("lblDeptNum"), Label)
            Dim lblComNum As Label = CType(e.Row.FindControl("lblComNum"), Label)
            Dim lblCommentNum As Label = CType(e.Row.FindControl("lblCommentNum"), Label)
            Dim lblSubmitDateTS As Label = CType(e.Row.FindControl("lblSubmitDateTS"), Label)

            '  Dim lblDirectorComment As Label = CType(e.Row.FindControl("lblDirectorComment"), Label)

            Dim LinkDelete As ImageButton = CType(e.Row.FindControl("LinkDelete"), ImageButton)
            Dim LinkCancel As ImageButton = CType(e.Row.FindControl("LinkCancel"), ImageButton)

            Dim lblTAT As Label = CType(e.Row.FindControl("lblTAT"), Label)
            Dim lblName As Label = CType(e.Row.FindControl("lblName"), Label)

            Dim lblImage1 As Label = CType(e.Row.FindControl("lblImage1"), Label)
            Dim lblCommittee As Label = CType(e.Row.FindControl("lblCommittee"), Label)
            Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
            Dim lblSend As Label = CType(e.Row.FindControl("lblSend"), Label)
            Dim lblMsg As Label = CType(e.Row.FindControl("lblMsg"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Dim img_active As String = ""

            Dim can_send_score = True

            Try

                If HyperLink1.Text = "" Or HyperLink1.Text = "0" Then
                    HyperLink1.Text = "********"
                End If

                If viewtype = "hr" Then
                    HyperLink1.Target = "_blank"

                End If

                If lblStatusId.Text = "6" Then ' cancel
                    ' lblTAT.Text = MinuteDiff(lblSubmitDateTS.Text, lblCloseDateTS.Text)
                    lblStatusName.ForeColor = Drawing.Color.Red
                Else
                    lblTAT.Text = MinuteDiff(lblSubmitDateTS.Text, Date.Now.Ticks.ToString)
                End If

                If lblStatusId.Text = "1" And viewtype = "" Then
                    LinkDelete.Visible = True
                Else
                    LinkDelete.Visible = False
                End If

                If lblStatusId.Text = "2" And viewtype <> "" Then
                    e.Row.Font.Bold = True
                End If

              

                If viewtype = "hr" Then
                    LinkCancel.Visible = True
                Else
                    LinkCancel.Visible = False
                End If

                sql = "SELECT a.* , b.EmployeeType , ISNULL(b.Employee_id,0) AS is_working , ISNULL(b.Department,'-') AS Department FROM star_relate_person a LEFT OUTER JOIN temp_BHUser b ON a.emp_code = b.Employee_id "

                sql &= " WHERE star_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                    If ds.Tables("t1").Rows(i)("is_working").ToString <> "0" Then
                        img_active = "<img src='../images/staff_active.png' title='Active Employee' /> "
                    Else
                        img_active = "<img src='../images/staff_terminate.png' title='Inactive Employee' /> "
                    End If

                    If ds.Tables("t1").Rows(i)("EmployeeType").ToString <> "Full Time" And ds.Tables("t1").Rows(i)("EmployeeType").ToString <> "Shift" Then
                        can_send_score = False
                    End If

                    lblName.Text &= " <span style='color:blue'>" & img_active & ds.Tables("t1").Rows(i)("user_fullname").ToString & " (" & ds.Tables("t1").Rows(i)("EmployeeType").ToString & ")" & "</span><br/>"
                    ' lblName.Text &= " Department : " & ds.Tables("t1").Rows(i)("Department").ToString & "<br/>"
                    ' Response.Write(ds.Tables("1").Rows(i)("user_fullname").ToString & "<br/>")
                Next i

                sql = "SELECT a.*  FROM star_relate_doctor a  "

                sql &= " WHERE star_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    img_active = "<img src='../images/special-offer.png' title='Doctor' /> "
                    lblName.Text &= " <span style='color:blue'>" & img_active & ds.Tables("t1").Rows(i)("doctor_name").ToString & "</span><br/>"
                    can_send_score = False
                    ' Response.Write(ds.Tables("1").Rows(i)("user_fullname").ToString & "<br/>")
                Next i

                sql = "SELECT a.*  FROM star_relate_dept a  "

                sql &= " WHERE star_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    img_active = "<img src='../images/153.png' title='Cost Center' /> "
                    lblName.Text &= " <span style='color:blue'>" & img_active & ds.Tables("t1").Rows(i)("costcenter_name").ToString & "</span><br/>"
                    can_send_score = False
                    ' Response.Write(ds.Tables("1").Rows(i)("user_fullname").ToString & "<br/>")
                Next i



                sql = "SELECT * FROM star_manager_comment WHERE star_id = " & lblPk.Text
                'sql &= " AND review_by_role_id =  17"
                'Response.Write(sql)
                ds = conn.getDataSet(sql, "t2")
                For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1

                    If ds.Tables("t2").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                        lblImage1.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & ds.Tables("t2").Rows(i)("review_by_name").ToString & "' />"
                    ElseIf ds.Tables("t2").Rows(i)("comment_status_id").ToString = "2" Then
                        lblImage1.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & ds.Tables("t2").Rows(i)("review_by_name").ToString & "' />"
                    ElseIf ds.Tables("t2").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                        lblImage1.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='" & ds.Tables("t2").Rows(i)("review_by_name").ToString & "' />"
                    Else


                    End If

                Next i

                If ds.Tables("t2").Rows.Count = 0 Then
                    If lblStatusId.Text = "1" Then
                        lblImage1.Text &= "-"
                    Else
                        lblImage1.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                    End If
                End If

                sql = "SELECT * FROM star_committee_comment WHERE star_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t3")
                lblCommittee.Text = ds.Tables("t3").Rows.Count

                If lblStatusId.Text = "7" Then
                    chkSelect.Visible = True
                Else
                    chkSelect.Visible = False
                End If

                If lblSend.Text = "1" Then
                    chkSelect.Visible = False
                    lblMsg.Text = "คะแนนถูกส่งไป SRP แล้ว"
                    lblMsg.ForeColor = Drawing.Color.Green
                Else
                    If can_send_score = False Then
                        chkSelect.Visible = False
                        lblMsg.Text = "- "
                        lblMsg.ForeColor = Drawing.Color.Red
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub

    Protected Sub cmdAwardStatus_Click(sender As Object, e As System.EventArgs) Handles cmdAwardStatus.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer
        Dim ds As New DataSet
        Dim ds2 As New DataSet
        Dim lbl As Label
        Dim chkSelect As CheckBox
        Dim login_max_authen As Integer = 0

        i = GridView1.Rows.Count
        Dim pk As String = ""
        Try


            For s As Integer = 0 To i - 1
                '  Response.Write(1)
                chkSelect = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)
                '  Response.Write(2)
                If chkSelect.Checked = True Then
                    'Response.Write(4)
                    lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)

                    sql = "SELECT * FROM star_hr_tab a INNER JOIN star_trans_list b ON a.star_id = b.star_id "
                    sql &= " INNER JOIN star_detail_tab c ON a.star_id = c.star_id "
                    sql &= "  WHERE ISNULL(b.is_send_score,0) = 0 AND a.star_id = " & lbl.Text
                    ds = conn.getDataSet(sql, "t1")
                    ' Response.Write(sql & "<hr/>")
                    If ds.Tables("t1").Rows.Count > 0 Then
                        sql = "SELECT * FROM star_relate_person a INNER JOIN user_profile b ON a.emp_code = b.emp_code WHERE a.star_id = " & lbl.Text
                        ds2 = conn.getDataSet(sql, "t2")
                        'Response.Write(sql)
                        pk = getPK("point_movement_id", "srp_point_movement", conn)

                        sql = "INSERT INTO srp_point_movement (point_movement_id , emp_code , emp_name , card_id , transaction_name , reward_type_id "
                        sql &= " , reward_type_name , movememt_type_id , movememt_type_name , point_trans , movement_remark , movement_create_by "
                        sql &= " , movement_create_date_ts , movement_create_date_raw  "

                        sql &= ") VALUES("
                        sql &= " '" & pk & "' , "
                        'Response.Write(11)
                        sql &= " '" & ds2.Tables("t2").Rows(0)("emp_code").ToString & "' , "
                        sql &= " '" & ds2.Tables("t2").Rows(0)("user_fullname").ToString & "' , "
                        'Response.Write(22)
                        sql &= " '" & 0 & "' , "
                        sql &= " 'Star Point' , "
                        sql &= " 5 , "
                        sql &= " 'Star' , "
                        sql &= " 2 , "
                        sql &= " 'Awarded' , "
                        ' MsgBox(txtadd_point.SelectedItem.ToString())

                        sql &= " " & ds.Tables("t1").Rows(0)("srp_point").ToString & " , "
                        sql &= " 'Star No. : " & ds.Tables("t1").Rows(0)("star_no").ToString & " ' , "
                        sql &= " '" & Session("user_fullname").ToString & "' , "
                        sql &= " '" & Date.Now.Ticks & "' , "
                        sql &= " GETDATE() "

                        sql &= ")"

                        ' Response.Write(sql & "<hr/>")
                        '  Return

                        errorMsg = conn.executeSQL(sql)

                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg)
                        End If

                        sql = "UPDATE star_trans_list SET is_send_score = 1 , send_score_date_raw = GETDATE() , send_score_date_ts = " & Date.Now.Ticks & " WHERE  star_id = " & lbl.Text
                        errorMsg = conn.executeSQL(sql)
                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg)
                        End If

                    Else

                    End If

                    updateOnlyLog("0", lbl.Text, "Send Star Point to SRP")

                    '  updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
                End If
            Next s


            'conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            'conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub updateOnlyLog(ByVal status_id As String, id As String, Optional ByVal log_remark As String = "")
        Dim sql As String
        Dim errorMsg As String

        sql = "INSERT INTO star_status_log (status_id , status_name , star_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
        sql &= "'" & status_id & "' ,"
        sql &= "'" & "" & "' ,"
        sql &= "'" & id & "' ,"
        sql &= "GETDATE() ,"
        sql &= "'" & Date.Now.Ticks & "' ,"
        sql &= "'" & Session("user_fullname").ToString & "' ,"
        sql &= "'" & Session("user_position").ToString & "' ,"
        sql &= "'" & Session("dept_name").ToString & "' ,"
        sql &= "'" & log_remark & "' "
        sql &= ")"
        ' Response.Write(sql)
        errorMsg = conn.executeSQL(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg & ":" & sql)
        End If
    End Sub
End Class
