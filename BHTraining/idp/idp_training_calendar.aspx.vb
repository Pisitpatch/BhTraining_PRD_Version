Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction
Imports System.IO

Partial Class idp_idp_training_calendar
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected flag As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Request.QueryString("viewtype")
        Session("viewtype") = viewtype & ""
        flag = Request.QueryString("flag")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Page.IsPostBack = True Then
        Else ' load first time
            bindCourseCombo()

            If viewtype = "educator" Then
                lblHeader.Text = "Attendance Record"
            ElseIf flag = "evaluation" Then
                lblHeader.Text = "Course Evaluation"
            ElseIf flag = "register" Then
                lblHeader.Text = "Course Registration"
                '  Response.End()
            End If
            bindGrid()
        End If

        If viewtype = "educator" Then
            GridView1.Columns(7).Visible = False
            GridView1.Columns(9).Visible = False ' แบบประเมิน
        Else
            GridView1.Columns(8).Visible = False
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

    Sub updateCourseStatus()
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE idp_training_schedule SET is_open = 0 WHERE schedule_start_ts < " & Date.Now.Ticks
            sql &= " AND max_attendee >= (SELECT FROM idp_training_registered GROUP BY schedule_id )"
        Catch ex As Exception

        End Try
    End Sub

    Sub bindCourseCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.internal_title FROM idp_external_req a INNER JOIN idp_trans_list b ON a.idp_id = b.idp_id "
            sql &= " WHERE ISNULL(b.is_delete , 0) = 0  AND b.status_id > 1 AND ISNULL( a.internal_title,'') <> ''  "
            If viewtype <> "educator" Then
                sql &= " AND ( a.idp_id IN (SELECT idp_id FROM idp_training_employee WHERE emp_code = " & Session("emp_code").ToString & " )"

                sql &= " OR a.idp_id IN (SELECT idp_id FROM idp_training_costcenter WHERE costcenter_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") )"

                sql &= " OR a.idp_id IN (SELECT idp_id FROM idp_training_jobtitle WHERE job_title_en LIKE '%" & Session("job_title").ToString & "%' )"

                sql &= " OR a.idp_id IN (SELECT idp_id FROM idp_training_jobtype WHERE job_type_name_en  LIKE '%" & Session("user_position").ToString & "%' )"
                sql &= " )"
            End If
            sql &= " GROUP BY a.internal_title"


            ds = conn.getDataSet(sql, "t1")

            txtcourse_name.DataSource = ds
            txtcourse_name.DataBind()

            txtcourse_name.Items.Insert(0, New ListItem("-- Please Select -- ", ""))
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , ISNULL(d.register_id,0) AS is_booking FROM idp_training_schedule a INNER JOIN idp_external_req b ON a.idp_id = b.idp_id "
            sql &= " INNER JOIN idp_trans_list c ON b.idp_id = c.idp_id "
            sql &= " LEFT OUTER JOIN idp_training_registered d ON a.schedule_id = d.schedule_id AND d.emp_code = " & Session("emp_code").ToString
            sql &= " WHERE ISNULL(a.is_delete , 0) = 0  AND c.status_id > 1 "
            ' If viewtype = "" Then
            sql &= " AND  ISNULL(c.is_cancel , 0) = 0 "
            'End If
            If viewtype <> "educator" Then

                If flag <> "evaluation" Then ' ถ้าเป็น Evaluate ไม่ต้องใช้เงื่อนไขนี้ คือ ไม่ต้องอยู๋ใน target group ก็สามารถประเมินได้
                    sql &= " AND ( a.idp_id IN (SELECT idp_id FROM idp_training_employee WHERE emp_code = " & Session("emp_code").ToString & " )"

                    sql &= " OR a.idp_id IN (SELECT idp_id FROM idp_training_costcenter WHERE costcenter_id = " & Session("dept_id").ToString & " )"

                    sql &= " OR a.idp_id IN (SELECT idp_id FROM idp_training_jobtitle WHERE job_title_en LIKE '%" & Session("job_title").ToString & "%' )"

                    sql &= " OR a.idp_id IN (SELECT idp_id FROM idp_training_jobtype WHERE job_type_name_en  LIKE '%" & Session("user_position").ToString & "%' )"
                    sql &= " )"
                End If ' End if evaluation

                'sql &= " AND b.internal_target_group_id IN ()"
            End If



            If txtcourse_name.Text <> "" Then
                sql &= " AND b.internal_title LIKE '%" & addslashes(txtcourse_name.Text) & "%' "
            End If

            If txttopic.Text <> "" Then
                sql &= " AND LOWER(b.internal_title) LIKE '%" & addslashes(txttopic.Text.ToLower) & "%' "
            End If

            If txtclass_status.SelectedValue <> "" Then
                sql &= " AND a.is_open = " & txtclass_status.SelectedValue

            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.schedule_start_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text, 23, 59)
            End If

            If flag = "register" Then
                sql &= " AND (a.is_open = 1 OR a.schedule_id IN (SELECT schedule_id FROM idp_training_registered WHERE emp_code = " & Session("emp_code").ToString & ") ) "
            End If

            If flag = "evaluation" Then
                sql &= " AND ISNULL(d.is_evaluate ,  0) = 0 AND a.schedule_id IN (SELECT schedule_id FROM idp_training_registered WHERE emp_code = " & Session("emp_code").ToString & ") "
            End If

            sql &= " ORDER BY a.schedule_start DESC , b.internal_title ASC "
            'Response.Write(sql)
            'Return
            ' Response.Write(viewtype)
            ds = conn.getDataSet(sql, "t1")

            GridView1.DataSource = ds
            GridView1.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblMax As Label = CType(e.Row.FindControl("lblMax"), Label)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            Dim lblRegis As Label = CType(e.Row.FindControl("lblRegis"), Label)

            Dim lblDateTS As Label = CType(e.Row.FindControl("lblDateTS"), Label)
            Dim lblEvaluation As Label = CType(e.Row.FindControl("lblEvaluation"), Label)
            Dim lblEmpRegister As Label = CType(e.Row.FindControl("lblEmpRegister"), Label)
            Dim lblDocument As Label = CType(e.Row.FindControl("lblDocument"), Label)
            Dim lblId As Label = CType(e.Row.FindControl("lblId"), Label)
            Dim lblBook As Label = CType(e.Row.FindControl("lblBook"), Label)
            Dim lblRegisText As Label = CType(e.Row.FindControl("lblRegisText"), Label)

            Dim ds1 As New DataSet
            Dim sql As String = ""
            Dim errorMsg As String = ""
            Dim regis_num As Integer = 0
            Dim max_num As Integer = 0
            Dim date_ts As Long = 0

            Try

                Long.TryParse(lblDateTS.Text, date_ts)

                If lblEmpRegister.Text <> "0" Then
                    lblBook.Text = "Booked"
                Else
                    lblBook.Text = "-"
                End If

                sql = "SELECT * FROM idp_trainging_file WHERE idp_id = " & lblId.Text
                ds1 = conn.getDataSetForTransaction(sql, "t1")

                For i As Integer = 0 To ds1.Tables("t1").Rows.Count - 1
                    lblDocument.Text &= " - <a href='../share/idp/hr/" & ds1.Tables("t1").Rows(i)("file_path").ToString & "' target='_blank'>" & ds1.Tables("t1").Rows(i)("file_name").ToString & "</a> : " & ds1.Tables("t1").Rows(i)("file_remark").ToString & "<br/>"
                Next i
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds1.Dispose()
            End Try

            If Date.Now.Ticks >= CDbl(lblDateTS.Text) Then

            Else
                lblEvaluation.Text = ""
            End If


            Dim ds As New DataSet
            Dim num As String = ""
            Try

                sql = "SELECT * FROM idp_training_registered WHERE schedule_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                lblRegis.Text = ds.Tables("t1").Rows.Count
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try

            Integer.TryParse(lblRegis.Text, regis_num)
            Integer.TryParse(lblMax.Text, max_num)

            If (regis_num >= max_num) Then
                lblRegisText.Text = "Full"
            Else
                lblRegisText.Text = ""
            End If

            If lblStatus.Text = 1 And (regis_num <= max_num) And (date_ts > Date.Now.Ticks) Then
                ' If lblStatus.Text = 1 Then
                If lblEmpRegister.Text = "1" Then
                    lblStatus.Text = "<span style='color:green'>Open</span>&nbsp; <a href='idp_training_register.aspx?schedule_id=" & lblPK.Text & "&flag=" & flag & "'><span >[View Detail]</span></a>"
                Else ' ไม่ได้ register 
                    lblStatus.Text = "<span style='color:green'>Open</span>&nbsp; <a href='idp_training_register.aspx?schedule_id=" & lblPK.Text & "&flag=" & flag & "'>[View Detail]</a>"
                    '  lblEvaluation.Text = ""
                End If

            Else

                If lblStatus.Text = 1 Then
                    conn.startTransactionSQLServer()
                    sql = "UPDATE idp_training_schedule SET is_open = 0 WHERE schedule_id = " & lblPK.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        conn.setDBRollback()
                    Else
                        conn.setDBCommit()
                    End If
                End If

                lblStatus.Text = "<span style='color:red'>Closed</span>"


            End If

        End If
    End Sub

    Protected Sub GridView1_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridView1.RowDeleted

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        txtdate1.Text = ""
        txtdate2.Text = ""
        txttopic.Text = ""
        txtclass_status.SelectedIndex = 0
    End Sub
End Class
