Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction
Imports System.IO

Partial Class idp_home
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected flag As String = ""
    Protected formtype As String = "" ' adjust or maintain

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        flag = Request.QueryString("flag")
        If flag = "ladder" Then
            Me.MasterPageFile = "IDP2_MasterPage.master"
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Request.QueryString("viewtype")
        formtype = Request.QueryString("formtype")
        flag = Request.QueryString("flag")
        Session("viewtype") = viewtype & ""
        Session("formtype") = formtype & ""

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If flag = "ladder" Then
            '  checkDupplicateLadder()
        End If

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            bindYear()

            If flag = "ladder" Then
                panel_ladder.Visible = True
                lblHeader.Text = "Nursing Clinical Ladder"
                txtformtype.SelectedValue = formtype
                bindLadderTemplateCombo()
                bindJobTitleAll()

            End If
            If viewtype = "hr" Then
                cmdExport.Visible = True
                div_hr.Visible = True
                div_dept.Visible = False
            Else
                div_hr.Visible = False
                div_dept.Visible = False
            End If

            If viewtype = "dept" Then
                div_dept.Visible = True
            End If

            If viewtype = "" Then
                GridView1.Columns(0).Visible = False
            Else
                cmdNew.Visible = False
            End If

            If viewtype = "dept" Then
                'cmdNew.Visible = False
            End If

            If viewtype = "educator" Then
                div_educator.Visible = True
            End If

            If flag <> "ladder" Then
                GridView1.Columns(5).Visible = False
                GridView1.Columns(11).Visible = False
            Else
                GridView1.Columns(2).Visible = False
            End If

            bindDept()
            bindStatus()
            bindHRStatus()
            bindGrid()

            If flag <> "ladder" Then ' ปิด IDP ชั่วคราว
                'cmdNew.Visible = False
                bindConfig()
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

    Sub bindYear()
        For i As Integer = Date.Now.Year - 2 To Date.Now.Year + 2
            txtyear.Items.Add(i)
            txtidp_year.Items.Add(i)
        Next i

        txtidp_year.Items.Insert(0, "All Year")
        txtyear.SelectedValue = Date.Now.Year
        ' txtidp_year.SelectedValue = Date.Now.Year
    End Sub

    Sub bindConfig()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM idp_config WHERE idp_config_id = 1 "
            ds = conn.getDataSet(sql, "t1")

            If ds.Tables("t1").Rows.Count > 0 Then
                If ds.Tables("t1").Rows(0)("idp_config_value").ToString = 1 Then
                    cmdNew.Visible = True
                Else
                    cmdNew.Visible = False
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub checkDupplicateIDP()
        Dim sql As String
        Dim ds As New DataSet
        Dim status_table As String = "idp_status_list"
        Try
            'sql = "SELECT * FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString
            'sql &= " AND plan_year = "

            If flag = "ladder" Then
                status_table = "idp_status_ladder"
            End If
            sql = "SELECT * FROM idp_trans_list a INNER JOIN " & status_table & " b"
            sql &= " ON a.status_id = b.idp_status_id "
            sql &= " INNER JOIN user_profile c ON a.report_emp_code = c.emp_code "

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "
            sql &= " AND a.idp_id NOT IN (SELECT idp_id FROM idp_external_req)"
            sql &= " AND ISNULL(a.is_ladder,0) = 0 AND a.status_id > 1 "
            sql &= " AND a.report_emp_code = " & Session("emp_code").ToString
            sql &= " AND a.plan_year = " & Date.Now.Year
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count >= 1 Then
                ' cmdNew.Enabled = False
            Else
                ' cmdNew.Enabled = True
            End If


        Catch ex As Exception

        End Try
    End Sub

    Sub checkDupplicateLadder()
        Dim sql As String
        Dim ds As New DataSet
        Dim status_table As String = "idp_status_list"
        Try
            'sql = "SELECT * FROM idp_trans_list WHERE report_emp_code = " & Session("emp_code").ToString
            'sql &= " AND plan_year = "

            If flag = "ladder" Then
                status_table = "idp_status_ladder"
            End If
            sql = "SELECT * FROM idp_trans_list a INNER JOIN " & status_table & " b"
            sql &= " ON a.status_id = b.idp_status_id "
            sql &= " INNER JOIN user_profile c ON a.report_emp_code = c.emp_code "

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "
            sql &= " AND a.idp_id NOT IN (SELECT idp_id FROM idp_external_req)"
            sql &= " AND a.is_ladder = 1 AND a.status_id = 1 "
            sql &= " AND a.report_emp_code = " & Session("emp_code").ToString

            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count >= 1 Then
                cmdNew.Enabled = False
            Else
                cmdNew.Enabled = True
            End If
         

        Catch ex As Exception

        End Try
    End Sub

    Sub bindLadderTemplateCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_template_master WHERE is_ladder_template = 1 AND is_active = 1 AND ISNULL(is_delete,0) = 0 "

            ds = conn.getDataSetForTransaction(sql, "t1")

            txtform.DataSource = ds
            txtform.DataBind()


            txtform.Items.Insert(0, New ListItem("--Please Select--", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTitleAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_title FROM user_profile WHERE 1 = 1 "
            ' If txtfind_jobtype.Text <> "" Then
            'sql &= " AND LOWER(job_type) LIKE '%" & txtfind_jobtype.Text.ToLower & "%' "
            'End If
            sql &= " GROUP BY job_title ORDER BY job_title"

            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtjobtitle.DataSource = ds
            txtjobtitle.DataBind()

            txtjobtitle.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Private Sub bindGrid()
        Dim ds As New DataSet
        Dim sql As String
        Dim status_table As String = "idp_status_list"
        Try
            If flag = "ladder" Then
                status_table = "idp_status_ladder"
            End If
            sql = "SELECT * FROM idp_trans_list a INNER JOIN " & status_table & " b"
            sql &= " ON a.status_id = b.idp_status_id "
            sql &= " INNER JOIN user_profile c ON a.report_emp_code = c.emp_code "

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "
            sql &= " AND a.idp_id NOT IN (SELECT idp_id FROM idp_external_req)"

            If flag = "ladder" Then
                If txtyear.SelectedValue <> "" Then
                    sql &= " AND a.plan_year = " & txtyear.SelectedValue
                End If

                If txtmonth.SelectedValue <> "" Then
                    sql &= " AND a.plan_month = " & txtmonth.SelectedValue
                End If

            Else
                If txtidp_year.SelectedIndex > 0 Then
                    sql &= " AND a.plan_year = " & txtidp_year.SelectedValue
                End If
            End If
        

            If txtdept.SelectedValue <> "" Then
                '  sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
                sql &= " AND c.dept_id = " & txtdept.SelectedValue
            End If

            If txtstatus.SelectedValue <> "" Then
                sql &= " AND a.status_id = " & txtstatus.SelectedValue
            End If

            If txtname.Value <> "" Then
                sql &= " AND LOWER(a.report_by) LIKE '%" & txtname.Value.ToLower & "%' "
            End If

            If txtemp_code.Value <> "" Then
                sql &= " AND a.report_emp_code = " & txtemp_code.Value & " "
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If flag = "ladder" Then
                sql &= " AND a.is_ladder = 1 "
            Else
                sql &= " AND ISNULL(a.is_ladder,0) = 0 "
            End If

            If viewtype = "" Then
                sql &= " AND a.report_emp_code = " & Session("emp_code").ToString
            End If

            If viewtype = "educator" Then
                'sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
                'sql &= " AND a.status_id >= 2"
                If flag = "ladder" Then
                    sql &= " AND ( a.status_id = 3  "
                    sql &= " OR  ( c.dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")  AND a.status_id >=1 )"
                    sql &= " )"
                Else
                    sql &= "  "
                    sql &= " AND  c.dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")  AND a.status_id >=2 )"
                    sql &= " "
                End If
              
            End If

            If viewtype = "dept" Then
                sql &= " AND  c.dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
                If flag <> "ladder" Then
                    sql &= " AND a.status_id >= 2"
                End If

            End If

            If viewtype = "hr" Then
                If flag = "ladder" Then
                    sql &= " AND a.status_id >= 1" ' เห็น draft ด้วย
                Else
                    sql &= " AND a.status_id >= 2" ' เห็น draft ด้วย
                End If

            End If

            If txtform.SelectedValue <> "" Then
                sql &= " AND a.ladder_template_id = " & txtform.SelectedValue
            End If

            If txtformtype.SelectedValue <> "" Then ' Adjust or maintain
                sql &= " AND a.ladder_template_id IN (SELECT template_id FROM idp_template_master WHERE ISNULL(is_delete,0) = 0 AND ladder_form_type = " & txtformtype.SelectedValue & ")"
            End If

            If txtjobtitle.SelectedValue <> "" Then
                sql &= " AND a.report_jobtitle LIKE '%" & txtjobtitle.SelectedValue & "%' "
            End If

            If flag = "ladder" Then
                sql &= " ORDER BY " & txtorder.SelectedValue & " " & txtsort.SelectedValue
            Else
                sql &= " ORDER BY a.idp_id DESC"
            End If

            If Session("idp_sql") Is Nothing Then
                '  Session("idp_sql") = sql
            Else
                If customsearch.Value <> 1 Then
                    '   sql = Session("idp_sql").ToString
                End If
            End If

            ds = conn.getDataSet(sql, "table1")

          

            ' Response.Write(sql)
            'Return
            lblNum.Text = ds.Tables(0).Rows.Count

            GridView1.DataSource = ds
            GridView1.DataBind()

            If flag = "ladder" Then
                checkDupplicateLadder()
            Else
                'checkDupplicateIDP()
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try

    End Sub

    Sub bindDept()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            'sql = "SELECT * FROM user_dept WHERE dept_id IN (SELECT report_dept_id FROM idp_trans_list WHERE ISNULL(is_delete,0) = 0  group by report_dept_id)"
            sql = "SELECT * FROM user_dept WHERE 1 = 1 "
            sql &= " ORDER BY dept_name_en"
            'sql = "SELECT report_dept_id AS dept_id , cast(report_dept_id as varchar) + ' : ' + report_dept_name AS dept_name_en FROM idp_trans_list "
            ' sql &= " GROUP BY report_dept_id , report_dept_name ORDER BY report_dept_name"

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
            If flag = "ladder" Then
                sql = "SELECT * FROM idp_status_ladder"
            Else
                sql = "SELECT * FROM idp_status_list"
            End If

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

    Sub bindHRStatus()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            If flag = "ladder" Then
                sql = "SELECT * FROM idp_status_ladder"
            Else
                sql = "SELECT * FROM idp_status_list"
            End If
            'sql &= " ORDER BY dept_name"
            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
             txthrstatus.DataSource = reader
            txthrstatus.DataBind()

            txtstatus.Items.Insert(0, New ListItem("--All Status--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("idp_detail.aspx?mode=add&flag=" & flag)
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
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
                sql = "UPDATE idp_trans_list SET is_delete = 1 WHERE idp_id = " & index
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
                sql = "UPDATE idp_trans_list SET is_cancel = 1 WHERE idp_id = " & index
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
            Dim lblFormName As Label = CType(e.Row.FindControl("lblFormName"), Label)
            Dim lblStatusId As Label = CType(e.Row.FindControl("lblStatusId"), Label)
            Dim lblAuthen As Label = CType(e.Row.FindControl("lblAuthen"), Label)
            Dim lblEmpCode As Label = CType(e.Row.FindControl("lblEmpCode"), Label)

            Dim lblImage1 As Label = CType(e.Row.FindControl("lblImage1"), Label)
            Dim lblImage2 As Label = CType(e.Row.FindControl("lblImage2"), Label)
            Dim lblImage3 As Label = CType(e.Row.FindControl("lblImage3"), Label)
            Dim lblImage4 As Label = CType(e.Row.FindControl("lblImage4"), Label)
            Dim lblImage5 As Label = CType(e.Row.FindControl("lblImage5"), Label)
            Dim lblImage6 As Label = CType(e.Row.FindControl("lblImage6"), Label)
            '  Dim lblDirectorComment As Label = CType(e.Row.FindControl("lblDirectorComment"), Label)

            Dim LinkDelete As ImageButton = CType(e.Row.FindControl("LinkDelete"), ImageButton)
            Dim LinkCancel As ImageButton = CType(e.Row.FindControl("LinkCancel"), ImageButton)
            Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
          
            Dim lblYear As Label = CType(e.Row.FindControl("lblYear"), Label)
            Dim lblMonth As Label = CType(e.Row.FindControl("lblMonth"), Label)
            Dim imgReply As Image = CType(e.Row.FindControl("imgReply"), Image)

            Dim lblDateSubmit As Label = CType(e.Row.FindControl("lblDateSubmit"), Label)

            Dim user_max_authen As String = "0"
            Dim login_max_authen As Integer = CInt(getMyIPDLevel())
            Dim sql As String
            Dim ds As New DataSet
            Dim dsReply As New DataSet
            Try

                If HyperLink1.Text = "" Then
                    HyperLink1.Text = "********"
                End If

                If viewtype = "hr" Then
                    HyperLink1.Target = "_blank"

                End If
                If flag = "ladder" Then
                    If viewtype = "dept" Or viewtype = "educator" Then
                        HyperLink1.Target = "_blank"
                    End If
                End If

                If viewtype = "hr" Then
                    LinkCancel.Visible = True
                Else
                    LinkCancel.Visible = False
                End If

                If lblStatusId.Text = "1" And viewtype = "" Then
                    LinkDelete.Visible = True
                Else
                    LinkDelete.Visible = False
                End If

                If lblStatusId.Text = "2" Then
                    e.Row.Font.Bold = True
                End If

                sql = "SELECT * FROM idp_information_update WHERE idp_id = " & lblPk.Text & " ORDER BY inform_id DESC"
                dsReply = conn.getDataSet(sql, "t1")
                If dsReply.Tables("t1").Rows.Count > 0 Then
                    imgReply.Visible = True
                    imgReply.ToolTip = dsReply.Tables("t1").Rows(0)("inform_detail").ToString & vbCrLf & "By " & dsReply.Tables("t1").Rows(0)("inform_by").ToString
                End If

                If flag = "ladder" Then

                    sql = "SELECT * FROM idp_status_log WHERE status_id = 2 AND idp_id = " & lblPk.Text
                    sql &= " ORDER BY log_time_ts DESC"
                    ds = conn.getDataSetForTransaction(sql, "tLog")
                    If ds.Tables("tLog").Rows.Count > 0 Then
                        lblDateSubmit.Text = ConvertTSToDateString(ds.Tables("tLog").Rows(0)("log_time_ts").ToString) & " " & ConvertTSTo(ds.Tables("tLog").Rows(0)("log_time_ts").ToString, "hour") & ":" & ConvertTSTo(ds.Tables("tLog").Rows(0)("log_time_ts").ToString, "min")
                    End If

                    lblFormName.Visible = True
                    lblFormName.Text = "<br/>" & lblFormName.Text

                    If lblMonth.Text = "4" Then
                        lblYear.Text = "Apr/" & lblYear.Text
                    Else
                        lblYear.Text = "Oct/" & lblYear.Text
                    End If

                    sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                    sql &= " AND is_educator = 1 "
                    ds = conn.getDataSet(sql, "tEdu")

                    For i As Integer = 0 To ds.Tables("tEdu").Rows.Count - 1

                        If ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                            lblImage6.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & ds.Tables("tEdu").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("tEdu").Rows(i)("detail").ToString.Replace("'", " ").Replace("""", " ") & "' />"
                        ElseIf ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "2" Then
                            lblImage6.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & ds.Tables("tEdu").Rows(i)("review_by_name").ToString & vbCrLf & addslashes(ds.Tables("tEdu").Rows(i)("detail").ToString.Replace("'", " ").Replace("""", " ")) & "' />"
                        ElseIf ds.Tables("tEdu").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                            lblImage6.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='" & ds.Tables("tEdu").Rows(i)("review_by_name").ToString & vbCrLf & addslashes(ds.Tables("tEdu").Rows(i)("detail").ToString.Replace("'", " ").Replace("""", " ")) & "' />"
                        Else
                            If lblStatusId.Text = "1" Then
                                lblImage6.Text &= "-"
                            Else
                                lblImage6.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                            End If

                        End If
                        '  Response.Write(12222)
                    Next i

                    If ds.Tables("tEdu").Rows.Count = 0 Then
                        If lblStatusId.Text = "1" Then
                            lblImage6.Text &= "-"
                        Else
                            lblImage6.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                        End If
                    Else
                        '   lblAuthen.Text = "4th Approval"
                    End If
                    'If lblEducatorStatus.Text = "3" Then ' Wait
                    '    lblImage6.Text = "<img src='../images/history.png' id='img1' />"
                    'ElseIf lblEducatorStatus.Text = "2" Then ' Reject
                    '    lblImage6.Text = "<img src='../images/button_cancel.png' id='img1' alt='" & lblEducatorStatus.Text & "' title='" & lblEducatorReviewBy.Text & "' />"
                    'ElseIf lblEducatorStatus.Text = "1" Then ' Approve
                    '    lblImage6.Text = "<img src='../images/button_ok.png' id='img1' alt='" & lblEducatorStatus.Text & "' title='" & lblEducatorReviewBy.Text & "' />"
                    'Else
                    '    lblImage6.Text = "-"
                    'End If
                End If

                lblAuthen.Text = "Employee"



                sql = "SELECT * FROM user_role a INNER JOIN m_role b ON a.role_id = b.role_id WHERE a.role_id IN (13,14,15,16,17) AND a.emp_code = " & lblEmpCode.Text & "  ORDER BY a.role_id DESC"
                'sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t0")
                If ds.Tables("t0").Rows.Count > 0 Then
                    lblAuthen.Text = ds.Tables("t0").Rows(0)("role_name").ToString
                    user_max_authen = ds.Tables("t0").Rows(0)("role_id").ToString
                Else

                End If

                'If viewtype = "dept" Then
                '    If CInt(getMyIPDLevel()) > CInt(user_max_authen) Then
                '        chkSelect.Visible = True
                '    Else
                '        chkSelect.Visible = False
                '    End If
                'End If

                If viewtype = "dept" Then
                    If CInt(getMyIPDLevel()) > CInt(user_max_authen) Or CInt(getMyIPDLevel()) = 17 Then
                        If lblEmpCode.Text <> Session("emp_code").ToString Then
                            chkSelect.Visible = True
                        Else
                            chkSelect.Visible = False
                        End If

                        ' Response.Write(getMyIPDLevel())
                        ' Response.Write(user_max_authen)
                    Else
                        chkSelect.Visible = False
                    End If
                End If

                If viewtype = "educator" Then
                    If lblStatusId.Text = "1" Then
                        chkSelect.Visible = False
                    End If
                End If
                '  Response.Write(CInt(user_max_authen) & "<br/>")
          
                Do While True
                    If 17 > CInt(user_max_authen) Then
                        ' ========== 5th
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  17 AND ISNULL(is_educator,0) = 0 "
                        ' Response.Write(sql & "<br/>")
                        ' Response.Write(1)
                        ds = conn.getDataSet(sql, "t5")
                        'Response.Write(2)
                        For i As Integer = 0 To ds.Tables("t5").Rows.Count - 1

                            If ds.Tables("t5").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage5.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & addslashes(ds.Tables("t5").Rows(i)("review_by_name").ToString & vbCrLf & "" & ds.Tables("t5").Rows(i)("detail").ToString) & "' />"
                            ElseIf ds.Tables("t5").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage5.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & addslashes(ds.Tables("t5").Rows(i)("review_by_name").ToString & vbCrLf & "" & ds.Tables("t5").Rows(i)("detail").ToString) & "' />"
                            ElseIf ds.Tables("t5").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage5.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='" & addslashes(ds.Tables("t5").Rows(i)("review_by_name").ToString & vbCrLf & "" & ds.Tables("t5").Rows(i)("detail").ToString) & "' />"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage5.Text &= "-"
                                Else
                                    lblImage5.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                                End If

                            End If
                            '  Response.Write(12222)
                        Next i

                        If ds.Tables("t5").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage5.Text &= "-"
                            Else
                                lblImage5.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                            End If
                        Else
                            '   lblAuthen.Text = "4th Approval"
                        End If


                    Else
                        lblImage5.Text &= "-"

                    End If


                    If 16 > CInt(user_max_authen) Then
                        ' ========== 4th
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  16  AND ISNULL(is_educator,0) = 0 "
                        'Response.Write(sql)
                        ' Response.Write(1)
                        ds = conn.getDataSet(sql, "t4")
                        'Response.Write(2)
                        For i As Integer = 0 To ds.Tables("t4").Rows.Count - 1

                            If ds.Tables("t4").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage4.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & addslashes(ds.Tables("t4").Rows(i)("review_by_name").ToString) & "' />"
                            ElseIf ds.Tables("t4").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage4.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & addslashes(ds.Tables("t4").Rows(i)("review_by_name").ToString) & "' />"
                            ElseIf ds.Tables("t4").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage4.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='N/A' />"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage4.Text &= "-"
                                Else
                                    lblImage4.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                                End If

                            End If
                            '  Response.Write(12222)
                        Next i

                        If ds.Tables("t4").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage4.Text &= "-"
                            Else
                                lblImage4.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                            End If
                        Else
                            '   lblAuthen.Text = "4th Approval"
                        End If

                    Else
                        lblImage4.Text &= "-"

                    End If


                    If 15 > CInt(user_max_authen) Then
                        ' ========== 3rd
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  15  AND ISNULL(is_educator,0) = 0 "
                        ' Response.Write(sql)
                        ds = conn.getDataSet(sql, "t3")
                        For i As Integer = 0 To ds.Tables("t3").Rows.Count - 1
                            If ds.Tables("t3").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage3.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & addslashes(ds.Tables("t3").Rows(i)("review_by_name").ToString) & "' />"
                            ElseIf ds.Tables("t3").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage3.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & addslashes(ds.Tables("t3").Rows(i)("review_by_name").ToString) & "' />"
                            ElseIf ds.Tables("t3").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage3.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='N/A' />"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage3.Text &= "-"
                                Else
                                    lblImage3.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                                End If

                            End If

                        Next i

                        If ds.Tables("t3").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage3.Text &= "-"
                            Else
                                lblImage3.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                            End If
                        Else
                            '   lblAuthen.Text = "3rd Approval"
                        End If

                    Else
                        lblImage3.Text &= "-"

                    End If


                    If 14 > CInt(user_max_authen) Then
                        ' ========== Manager
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  14  AND ISNULL(is_educator,0) = 0 "
                        '   Response.Write(sql)
                        ds = conn.getDataSet(sql, "t2")
                        For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1
                            If ds.Tables("t2").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage2.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & addslashes(ds.Tables("t2").Rows(i)("review_by_name").ToString) & "' />"
                            ElseIf ds.Tables("t2").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage2.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & addslashes(ds.Tables("t2").Rows(i)("review_by_name").ToString) & "' />"
                            ElseIf ds.Tables("t2").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                                lblImage2.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='N/A' />"
                            Else
                                If lblStatusId.Text = "1" Then
                                    lblImage2.Text &= "-"
                                Else
                                    lblImage2.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                                End If

                            End If

                        Next i

                        If ds.Tables("t2").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage2.Text &= "-"
                            Else
                                lblImage2.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                            End If
                        Else
                            '  lblAuthen.Text = "2nd Approval"
                        End If

                    Else
                        lblImage2.Text &= "-"

                    End If

                    ' ========== Sup
                    If 13 > CInt(user_max_authen) Then
                        sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                        sql &= " AND review_by_role_id =  13  AND ISNULL(is_educator,0) = 0 "
                        ' Response.Write(sql)
                        ds = conn.getDataSet(sql, "t1")


                        For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                            If ds.Tables("t1").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                                lblImage1.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & ds.Tables("t1").Rows(i)("review_by_name").ToString & "' />"
                            ElseIf ds.Tables("t1").Rows(i)("comment_status_id").ToString = "2" Then
                                lblImage1.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & ds.Tables("t1").Rows(i)("review_by_name").ToString & "' />"
                            ElseIf ds.Tables("t1").Rows(i)("comment_status_id").ToString = "3" Then
                                lblImage1.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='N/A' />"
                            Else
                                lblImage1.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                            End If

                        Next i

                        If ds.Tables("t1").Rows.Count = 0 Then
                            If lblStatusId.Text = "1" Then
                                lblImage1.Text &= "-"
                            Else
                                lblImage1.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                            End If
                        Else
                            '   lblAuthen.Text = "1st Approval"
                        End If

                    Else
                        lblImage1.Text &= "-"

                    End If

                    Exit Do
                Loop



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
        customsearch.Value = 1
        bindGrid()
    End Sub

    Protected Sub cmdUpdateStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateStatus.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer
        Dim ds As New DataSet
        Dim lbl As Label
        Dim chkSelect As CheckBox   

        i = GridView1.Rows.Count

        Try

            For s As Integer = 0 To i - 1
                chkSelect = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)
                If chkSelect.Checked = True Then
                    lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)


                    sql = "UPDATE idp_trans_list SET status_id = '" & txthrstatus.SelectedValue & "' "
                    sql &= "  WHERE idp_id = " & lbl.Text
                    'Response.Write(sql & "<br/>")

                    errorMsg = conn.executeSQL(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                        Exit For
                    End If
                End If
            Next s

            ' conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            'conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Function getMyIPDLevel() As String ' Level ของผู้ login
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = "0"
        Try
            sql = "SELECT * FROM user_role a INNER JOIN m_role b ON a.role_id = b.role_id WHERE a.role_id IN (12,13,14,15,16,17) AND a.emp_code = " & Session("emp_code").ToString
            sql &= " ORDER BY a.role_id DESC"
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                result = ds.Tables("t1").Rows(0)("role_id").ToString
                ' lblYourLevel.Text = ds.Tables("t1").Rows(0)("role_name").ToString
            Else
                '  lblYourLevel.Text = "General Employee"
                ' result = "12"
            End If
        Catch ex As Exception
            result = "0"
        End Try
        Return result
    End Function

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


            Next s


        Catch ex As Exception

            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdDeptStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeptStatus.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer
        Dim ds As New DataSet
        Dim lbl As Label
        Dim chkSelect As CheckBox
        Dim login_max_authen As Integer = 0

        i = GridView1.Rows.Count
        Dim pk As String = ""
        Try
            login_max_authen = getMyIPDLevel()

            For s As Integer = 0 To i - 1
                chkSelect = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)

                If chkSelect.Checked = True Then
                    lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)

                    sql = "SELECT * FROM idp_manager_comment WHERE review_by_empcode = " & Session("emp_code").ToString & " AND idp_id = " & lbl.Text
                    ds = conn.getDataSetForTransaction(sql, "t1")

                    If ds.Tables("t1").Rows.Count = 0 Then
                        pk = getPK("comment_id", "idp_manager_comment", conn)
                        sql = "INSERT INTO idp_manager_comment (comment_id , idp_id , comment_status_id , comment_status_name , subject_id , subject , detail "
                        sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
                        sql &= ",create_date , create_date_ts ,  review_by_role_id"
                        sql &= ") VALUES("
                        sql &= " '" & pk & "' ,"
                        sql &= " '" & lbl.Text & "' ,"
                        sql &= " '" & txtdeptstatus.SelectedValue & "' ,"
                        sql &= " '" & txtdeptstatus.SelectedItem.Text & "' ,"
                        sql &= " '" & "" & "' ,"
                        sql &= " '" & "" & "' ,"
                        sql &= " '" & "" & "' ,"
                        sql &= " '" & Session("job_title").ToString & "' ,"
                        sql &= " '" & Session("user_position").ToString & "' ,"
                        sql &= " '" & Session("user_fullname").ToString & "' ,"
                        sql &= " '" & Session("emp_code").ToString & "' ,"
                        sql &= " '" & Session("dept_name").ToString & "' ,"
                        sql &= " '" & Session("costcenter_id").ToString & "' ,"
                        sql &= " GETDATE() ,"
                        sql &= " '" & Date.Now.Ticks & "' ,"
                        sql &= " '" & login_max_authen & "' "
                        sql &= ")"
                    Else
                        sql = "UPDATE idp_manager_comment SET comment_status_id = " & txtdeptstatus.SelectedValue
                        sql &= " , comment_status_name = '" & txtdeptstatus.SelectedItem.Text & "' "
                        ' sql &= " , subject_id = '" & txtcomment.SelectedValue & "' "
                        ' sql &= " , subject = '" & txtcomment.SelectedItem.Text & "' "
                        ' sql &= " , detail = '" & addslashes(txtcomment_detail.Value) & "' "
                        sql &= " WHERE  review_by_empcode = " & Session("emp_code").ToString & "  AND idp_id = " & lbl.Text
                    End If
               

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)

                    End If

                    updateOnlyLog("0", lbl.Text, txtdeptstatus.SelectedItem.Text)

                    '  updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
                End If
            Next s


            ' conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            'conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub updateOnlyLog(ByVal status_id As String, ByVal idp_id As String, Optional ByVal log_remark As String = "")
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "INSERT INTO idp_status_log (status_id , status_name , idp_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
            sql &= "'" & status_id & "' ,"
            sql &= "'" & "" & "' ,"
            sql &= "'" & idp_id & "' ,"
            sql &= "GETDATE() ,"
            sql &= "'" & Date.Now.Ticks & "' ,"
            sql &= "'" & Session("user_fullname").ToString & "' ,"
            sql &= "'" & Session("user_position").ToString & "' ,"
            sql &= "'" & Session("dept_name").ToString & "' ,"
            sql &= "'" & log_remark & "' "
            sql &= ")"
            '  Response.Write(sql)
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sql)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub


    Protected Sub cmdUpdateStatus_Educator_Click(sender As Object, e As System.EventArgs) Handles cmdUpdateStatus_Educator.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer
        Dim ds As New DataSet

        Dim lbl As Label
        Dim chkSelect As CheckBox
        Dim login_max_authen As Integer = 0

        i = GridView1.Rows.Count
        Dim pk As String = ""
        Try
            login_max_authen = getMyIPDLevel()

            For s As Integer = 0 To i - 1
                chkSelect = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)

                If chkSelect.Checked = True Then
                    lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)

                    sql = "SELECT * FROM idp_manager_comment WHERE is_educator = 1 AND review_by_empcode = " & Session("emp_code").ToString & " AND idp_id = " & lbl.Text
                    ds = conn.getDataSetForTransaction(sql, "t1")

                    If ds.Tables("t1").Rows.Count = 0 Then
                        pk = getPK("comment_id", "idp_manager_comment", conn)
                        sql = "INSERT INTO idp_manager_comment (comment_id , idp_id , comment_status_id , comment_status_name , subject_id , subject , detail "
                        sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
                        sql &= ",create_date , create_date_ts ,  review_by_role_id , is_educator"
                        sql &= ") VALUES("
                        sql &= " '" & pk & "' ,"
                        sql &= " '" & lbl.Text & "' ,"
                        sql &= " '" & txteducator_status.SelectedValue & "' ,"
                        sql &= " '" & txteducator_status.SelectedItem.Text & "' ,"
                        sql &= " '" & "" & "' ,"
                        sql &= " '" & "" & "' ,"
                        sql &= " '" & "" & "' ,"
                        sql &= " '" & Session("job_title").ToString & "' ,"
                        sql &= " '" & Session("user_position").ToString & "' ,"
                        sql &= " '" & Session("user_fullname").ToString & "' ,"
                        sql &= " '" & Session("emp_code").ToString & "' ,"
                        sql &= " '" & Session("dept_name").ToString & "' ,"
                        sql &= " '" & Session("costcenter_id").ToString & "' ,"
                        sql &= " GETDATE() ,"
                        sql &= " '" & Date.Now.Ticks & "' ,"
                        sql &= " '" & login_max_authen & "' ,"
                        sql &= " 1  "
                        sql &= ")"
                    Else
                        sql = "UPDATE idp_manager_comment SET comment_status_id = " & txteducator_status.SelectedValue
                        sql &= " , comment_status_name = '" & txteducator_status.SelectedItem.Text & "' "
                     
                        sql &= " WHERE is_educator = 1 AND review_by_empcode = " & Session("emp_code").ToString & "  AND idp_id = " & lbl.Text
                    End If
                 

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)

                    End If

                    updateOnlyLog("0", lbl.Text, txteducator_status.SelectedItem.Text)

                    '  updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
                End If
            Next s


            ' conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            'conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Shared Sub Export(ByVal fileName As String, ByVal gv As GridView)


        HttpContext.Current.Response.Clear()

        'Export will take two parameter first one the name of Excel File, and second one for gridview to be exported

        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))

        ' HttpContext.Current.Response.ContentType = "application/octet-stream"
        HttpContext.Current.Response.ContentType = " application/vnd.ms-excel"

        HttpContext.Current.Response.Charset = "TIS-620"

        Using strWriter As New StringWriter()


            Using htmlWriter As New HtmlTextWriter(strWriter)


                ' Create a form to contain the grid

                Dim table As New Table()

                ' add the header row to the table

                If gv.HeaderRow IsNot Nothing Then


                    ExportControl(gv.HeaderRow)


                    table.Rows.Add(gv.HeaderRow)
                End If

                ' add each of the data rows to the table

                For Each row As GridViewRow In gv.Rows


                    ExportControl(row)


                    table.Rows.Add(row)
                Next

                ' add the footer row to the table

                If gv.FooterRow IsNot Nothing Then


                    ExportControl(gv.FooterRow)


                    table.Rows.Add(gv.FooterRow)
                End If

                ' render the table into the htmlwriter

                table.RenderControl(htmlWriter)

                ' render the htmlwriter into the response

                HttpContext.Current.Response.Write(strWriter.ToString())


                HttpContext.Current.Response.[End]()

            End Using
        End Using

    End Sub

    Private Shared Sub ExportControl(ByVal control As Control)


        For i As Integer = 0 To control.Controls.Count - 1


            Dim current As Control = control.Controls(i)

            If TypeOf current Is LinkButton Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, LinkButton).Text))

            ElseIf TypeOf current Is ImageButton Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, ImageButton).AlternateText))

            ElseIf TypeOf current Is HyperLink Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, HyperLink).Text))

            ElseIf TypeOf current Is DropDownList Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, DropDownList).SelectedItem.Text))

            ElseIf TypeOf current Is CheckBox Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(If(TryCast(current, CheckBox).Checked, "True", "False")))
            End If

            'Like that you may convert any control to literals

            If current.HasControls() Then



                ExportControl(current)

            End If
        Next

    End Sub

    Protected Sub cmdExport_Click(sender As Object, e As System.EventArgs) Handles cmdExport.Click
        'Export("idp.xls", GridView1)

        Dim ds As New DataSet
        Dim sql As String
        Dim status_table As String = "idp_status_list"
        Try
            If flag = "ladder" Then
                status_table = "idp_status_ladder"
            End If
            sql = "SELECT * FROM idp_trans_list a INNER JOIN " & status_table & " b"
            sql &= " ON a.status_id = b.idp_status_id "
            sql &= " INNER JOIN user_profile c ON a.report_emp_code = c.emp_code "

            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "
            sql &= " AND a.idp_id NOT IN (SELECT idp_id FROM idp_external_req)"

            If flag = "ladder" Then
                If txtyear.SelectedValue <> "" Then
                    sql &= " AND a.plan_year = " & txtyear.SelectedValue
                End If

                If txtmonth.SelectedValue <> "" Then
                    sql &= " AND a.plan_month = " & txtmonth.SelectedValue
                End If

            Else
                If txtidp_year.SelectedIndex > 0 Then
                    sql &= " AND a.plan_year = " & txtidp_year.SelectedValue
                End If
            End If


            If txtdept.SelectedValue <> "" Then
                '  sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
                sql &= " AND c.dept_id = " & txtdept.SelectedValue
            End If

            If txtstatus.SelectedValue <> "" Then
                sql &= " AND a.status_id = " & txtstatus.SelectedValue
            End If

            If txtname.Value <> "" Then
                sql &= " AND LOWER(a.report_by) LIKE '%" & txtname.Value.ToLower & "%' "
            End If

            If txtemp_code.Value <> "" Then
                sql &= " AND a.report_emp_code = " & txtemp_code.Value & " "
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If flag = "ladder" Then
                sql &= " AND a.is_ladder = 1 "
            Else
                sql &= " AND ISNULL(a.is_ladder,0) = 0 "
            End If

            If viewtype = "" Then
                sql &= " AND a.report_emp_code = " & Session("emp_code").ToString
            End If

            If viewtype = "educator" Then
                'sql &= " AND a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
                'sql &= " AND a.status_id >= 2"
                If flag = "ladder" Then
                    sql &= " AND ( a.status_id = 3  "
                    sql &= " OR  ( c.dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")  AND a.status_id >=1 )"
                    sql &= " )"
                Else
                    sql &= "  "
                    sql &= " AND  c.dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")  AND a.status_id >=2 )"
                    sql &= " "
                End If

            End If

            If viewtype = "dept" Then
                sql &= " AND  c.dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
                If flag <> "ladder" Then
                    sql &= " AND a.status_id >= 2"
                End If

            End If

            If viewtype = "hr" Then
                If flag = "ladder" Then
                    sql &= " AND a.status_id >= 1" ' เห็น draft ด้วย
                Else
                    sql &= " AND a.status_id >= 2" ' เห็น draft ด้วย
                End If

            End If

            If txtform.SelectedValue <> "" Then
                sql &= " AND a.ladder_template_id = " & txtform.SelectedValue
            End If

            If txtformtype.SelectedValue <> "" Then ' Adjust or maintain
                sql &= " AND a.ladder_template_id IN (SELECT template_id FROM idp_template_master WHERE ISNULL(is_delete,0) = 0 AND ladder_form_type = " & txtformtype.SelectedValue & ")"
            End If

            If txtjobtitle.SelectedValue <> "" Then
                sql &= " AND a.report_jobtitle LIKE '%" & txtjobtitle.SelectedValue & "%' "
            End If

            If flag = "ladder" Then
                sql &= " ORDER BY " & txtorder.SelectedValue & " " & txtsort.SelectedValue
            Else
                sql &= " ORDER BY a.idp_id DESC"
            End If

            If Session("idp_sql") Is Nothing Then
                '  Session("idp_sql") = sql
            Else
                If customsearch.Value <> 1 Then
                    '   sql = Session("idp_sql").ToString
                End If
            End If

            ds = conn.getDataSet(sql, "table1")

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.Charset = "Windows-874"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Me.EnableViewState = False
            Response.AddHeader("content-disposition", "attachment;filename=IDP.xls")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            Dim gv As New GridView()
            gv.DataSource = ds.Tables(0)
            gv.DataBind()
            gv.RenderControl(hw)
            Response.Output.Write(sw)
            Response.Flush()
            Response.[End]()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
