Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_idp_template_detail
    Inherits System.Web.UI.Page
    Protected mode As String = ""
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Private session_id As String = ""
    Private id As String = ""
    Protected viewtype As String = ""
    Protected flag As String = ""

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        flag = Request.QueryString("flag")
        If flag = "ladder" Then
            Me.MasterPageFile = "IDP2_MasterPage.master"
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        session_id = Session("session_myid").ToString
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        flag = Request.QueryString("flag")
        viewtype = Session("viewtype").ToString

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If IsPostBack = True Then

        Else ' First time
            If flag = "ladder" Then
                fieldset_tagetgroup.Visible = False
                tab_ladder_score.Visible = True
                lblHeader.Text = "Cnical Ladder Form"

                GridTemplate.Columns(5).Visible = False
                GridTemplate.Columns(6).Visible = False
                GridTemplate.Columns(9).Visible = False
                GridTemplate.Columns(10).Visible = False

                panel_add_ladder.Visible = True
            Else
                GridTemplate.Columns(7).Visible = False
                GridTemplate.Columns(8).Visible = False
                panel_add_idp.Visible = True
            End If

            If mode = "add" Then
                panelDetail.Visible = False
            Else


                bindForm()
                bindCategory()
                bindExpect()
                bindGridTemplate()
                bindEmployeeSelect()
                bindJobTypeAll()
                bindJobTypeSelect()
                bindJobTitleAll()
                bindJobTitleSelect()

                bindCCAll()
                bindCCSelect()

                bindMethodCombo()

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
            sql = "SELECT * FROM idp_template_master WHERE template_id = " & id
           

            ds = conn.getDataSetForTransaction(sql, "t1")

            txttitle.Text = ds.Tables("t1").Rows(0)("template_title").ToString
            txtstatus.SelectedValue = ds.Tables("t1").Rows(0)("is_active").ToString

            txtrequire_score.Text = ds.Tables("t1").Rows(0)("ladder_require_score").ToString
            txtelective_score.Text = ds.Tables("t1").Rows(0)("ladder_elective_score").ToString

            txtapply_date.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("ladder_apply_date_ts").ToString)
            txthour1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("ladder_apply_date_ts").ToString, "hour")
            txtmin1.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("ladder_apply_date_ts").ToString, "min")

            txtformtype.SelectedValue = ds.Tables("t1").Rows(0)("ladder_form_type").ToString
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindMethodCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_method"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtadd_method1.DataSource = ds
            txtadd_method1.DataBind()

            txtadd_method1.Items.Insert(0, New ListItem("-- Please Select --", "0"))

          

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            '  ds.Dispose()
        End Try
    End Sub

    Sub bindJobTypeAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_type FROM user_profile WHERE 1 = 1 AND job_type NOT IN (SELECT job_type_name_en FROM idp_template_jobtype WHERE template_id = " & id & ")"
            If txtfind_jobtype.Text <> "" Then
                sql &= " AND LOWER(job_type) LIKE '%" & txtfind_jobtype.Text.ToLower & "%' "
            End If
            sql &= " GROUP BY job_type ORDER BY job_type"

            ' Response.Write(sql)
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
            sql = "SELECT job_type_name_en AS job_type FROM idp_template_jobtype WHERE template_id = " & id & "  ORDER BY job_type_name_en"
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblJobTypeSelect.DataSource = ds
            lblJobTypeSelect.DataBind()

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
            sql = "SELECT job_title FROM user_profile WHERE 1 = 1 AND  job_title NOT IN (SELECT job_title_en FROM idp_template_jobtitle WHERE template_id = " & id & ")"
            If txtfind_jobtitle.Text <> "" Then
                sql &= " AND LOWER(job_title) LIKE '%" & txtfind_jobtitle.Text.ToLower & "%' "
            End If
            sql &= " GROUP BY job_title ORDER BY job_title"
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblJobTitleAll.DataSource = ds
            lblJobTitleAll.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobTitleSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_title_en AS job_title FROM idp_template_jobtitle  WHERE template_id = " & id & "  ORDER BY job_title_en"
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblJobTitleSelect.DataSource = ds
            lblJobTitleSelect.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCategory()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_category  WHERE  ISNULL(is_delete,0) = 0 "
            If flag = "ladder" Then
                sql &= " AND category_type = 'Nursing' "
            Else
                sql &= " AND category_type = 'General' "
            End If
            sql &= " ORDER BY category_name_en"
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtadd_category.DataSource = ds
            txtadd_category.DataBind()

            txtadd_category.Items.Insert(0, New ListItem("-- Please Select --", ""))

            '  txtadd_category.Items(4).Attributes.Add("style", "color:red;background-color:yellow")
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTopic()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_topic a INNER JOIN idp_m_master_topic b ON a.master_topic_id = b.master_topic_id "
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 "
            If txtadd_category.SelectedValue <> "" Then
                sql &= " AND a.category_id = " & txtadd_category.SelectedValue

                If viewtype = "educator" Then ' Unit mandatory for educator
                    sql &= " AND a.owner_dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code") & ")"
                    ' sql &= " AND topic_id IN (SELECT topic_id FROM idp_m_topic_dept WHERE topic_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code") & ") )"

                End If

            End If

            sql &= " ORDER BY a.topic_name_th"
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtadd_topic.DataSource = ds
            txtadd_topic.DataBind()

            txtadd_topic.Items.Insert(0, New ListItem("-- Please Select --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindExpect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_expect WHERE ISNULL(is_expect_delete,0) = 0 "
            sql &= " ORDER BY expect_order_sort"
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtadd_expect.DataSource = ds
            txtadd_expect.DataBind()

            txtadd_expect.Items.Insert(0, New ListItem("-- Please Select --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridTemplate()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_template_detail a  "
            sql &= " INNER JOIN idp_m_topic b ON a.template_topic_id = b.topic_id "
            sql &= " WHERE a.template_id = " & id

            sql &= " ORDER BY a.template_is_require DESC , a.template_order_sort ASC"

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridTemplate.DataSource = ds
            GridTemplate.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindEmployeeAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM user_profile WHERE 1 = 1 AND emp_code NOT IN (SELECT emp_code FROM idp_template_employee WHERE template_id = " & id & ") "
            If txtfindemployee.Text <> "" Then
                sql &= " AND (user_fullname LIKE '%" & txtfindemployee.Text & "%' "
                sql &= " OR emp_code LIKE '%" & txtfindemployee.Text & "%') "
            End If

            sql &= " ORDER BY user_fullname "
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblDivisionAll.DataSource = ds
            lblDivisionAll.DataBind()

            ' txtadd_topic.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindEmployeeSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_template_employee a INNER JOIN user_profile b ON a.emp_code = b.emp_code WHERE 1 = 1 "

            sql &= " AND a.template_id = " & id


            sql &= " ORDER BY a.emp_name_th "
            '   Response.Write(sql)
            '  Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblDivisionSelect.DataSource = ds
            lblDivisionSelect.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCCAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM user_dept WHERE 1 = 1 AND dept_id NOT IN (SELECT costcenter_id FROM idp_template_costcenter WHERE template_id = " & id & ") "
            If viewtype = "educator" Then
                'sql &= " AND dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            sql &= " ORDER BY dept_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblCCAll.DataSource = ds
            lblCCAll.DataBind()

            ' txtadd_topic.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCCSelect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_template_costcenter a INNER JOIN user_dept b ON a.costcenter_id = b.dept_id WHERE 1 = 1 "
            sql &= " AND a.template_id = " & id
            sql &= " ORDER BY b.dept_name_en "
            '   Response.Write(sql)
            '  Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblCCSelect.DataSource = ds
            lblCCSelect.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If mode = "add" Then
            onAddSave()
        ElseIf mode = "edit" Then
            onEditSave()

        End If
    End Sub

    Sub onEditSave()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try
            'updateOrder()
            saveOrderNoCommit()

            sql = "UPDATE idp_template_master SET template_title = '" & addslashes(txttitle.Text) & "' "
            sql &= " , is_active = '" & txtstatus.SelectedValue & "' "
            sql &= " , last_update_by = '" & Session("user_fullname").ToString & "' "
            sql &= " , last_update_date = GETDATE() "
            sql &= " , last_update_date_ts = '" & Date.Now.Ticks & "' "
            If flag = "ladder" Then
                sql &= " , ladder_require_score = '" & txtrequire_score.Text & "' "
                sql &= " , ladder_elective_score = '" & txtelective_score.Text & "' "
                sql &= " , ladder_apply_date_ts = '" & ConvertDateStringToTimeStamp(txtapply_date.Text, txthour1.SelectedValue, txtmin1.SelectedValue) & "' "
                sql &= " , ladder_apply_date_raw = '" & convertToSQLDatetime(txtapply_date.Text, txthour1.SelectedValue, txtmin1.SelectedValue) & "' "

                sql &= " , ladder_form_type = '" & txtformtype.SelectedValue & "' "
            End If
            sql &= " WHERE template_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            sql = "DELETE FROM idp_template_employee WHERE template_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            For i As Integer = 0 To lblDivisionSelect.Items.Count - 1
                If lblDivisionSelect.Items(i).Value <> "" Then
                    pk = getPK("template_emp_id", "idp_template_employee", conn)
                    sql = "INSERT INTO idp_template_employee (template_emp_id , template_id , emp_code , emp_name_th , emp_name_en) VALUES("
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & id & "' ,"
                    sql &= " '" & lblDivisionSelect.Items(i).Value & "' ,"
                    sql &= " '" & addslashes(lblDivisionSelect.Items(i).Text) & "' ,"
                    sql &= " '" & addslashes(lblDivisionSelect.Items(i).Text) & "' "

                    sql &= ")"

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If

            Next

            sql = "DELETE FROM idp_template_costcenter WHERE template_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            For i As Integer = 0 To lblCCSelect.Items.Count - 1
                pk = getPK("template_cost_id", "idp_template_costcenter", conn)
                sql = "INSERT INTO idp_template_costcenter (template_cost_id , template_id , costcenter_id) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & id & "' ,"
                sql &= " '" & lblCCSelect.Items(i).Value & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next

            sql = "DELETE FROM idp_template_jobtype WHERE template_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            'Response.Write(sql)

            For i As Integer = 0 To lblJobTypeSelect.Items.Count - 1
                pk = getPK("template_jobtype_id", "idp_template_jobtype", conn)
                sql = "INSERT INTO idp_template_jobtype (template_jobtype_id , template_id , job_type_name_th , job_type_name_en) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & id & "' ,"
                sql &= " '" & lblJobTypeSelect.Items(i).Value & "' ,"
                sql &= " '" & lblJobTypeSelect.Items(i).Value & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next

            sql = "DELETE FROM idp_template_jobtitle WHERE template_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            For i As Integer = 0 To lblJobTitleSelect.Items.Count - 1
                pk = getPK("template_jobtitle_id", "idp_template_jobtitle", conn)
                sql = "INSERT INTO idp_template_jobtitle (template_jobtitle_id , template_id , job_title_th , job_title_en) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & id & "' ,"
                sql &= " '" & lblJobTitleSelect.Items(i).Value & "' ,"
                sql &= " '" & lblJobTitleSelect.Items(i).Value & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next

            conn.setDBCommit()

            bindJobTitleSelect()
            bindJobTypeSelect()
            bindGridTemplate()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub onAddSave()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""

        Dim ds As New DataSet
        Dim new_no As String
        Dim new_year As String
        Try
            updateOrder()

            sql = "SELECT * FROM idp_template_master ORDER BY template_yearno DESC , template_runno DESC"
            ds = conn.getDataSetForTransaction(sql, "t1")
            If Date.Now.Year <> CInt(ds.Tables("t1").Rows(0)("template_yearno").ToString) Then
                new_year = Date.Now.Year.ToString
                new_no = "1"
            Else
                new_year = ds.Tables("t1").Rows(0)("template_yearno").ToString
                new_no = CStr(CInt(ds.Tables("t1").Rows(0)("template_runno").ToString) + 1)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            Return
        End Try

        Try
            pk = getPK("template_id", "idp_template_master", conn)
            sql = "INSERT INTO idp_template_master (template_id , template_runno , template_yearno , is_active , template_title , create_date , create_date_ts , create_by , create_by_dept_id , create_by_dept_name) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & new_no & "' ,"
            sql &= " '" & new_year & "' ,"
            sql &= " '" & txtstatus.SelectedValue & "' ,"
            sql &= " '" & addslashes(txttitle.Text) & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' ,"
            sql &= " '" & Session("user_fullname").ToString & "' ,"
            sql &= " '" & Session("dept_id").ToString & "' ,"
            sql &= " '" & Session("dept_name").ToString & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            If flag = "ladder" Then
                sql = "UPDATE idp_template_master SET is_ladder_template = 1 "
                sql &= " , ladder_require_score = '" & txtrequire_score.Text & "' "
                sql &= " , ladder_elective_score = '" & txtelective_score.Text & "' "
                sql &= " , ladder_apply_date_ts = '" & ConvertDateStringToTimeStamp(txtapply_date.Text, txthour1.SelectedValue, txtmin1.SelectedValue) & "' "
                sql &= " , ladder_apply_date_raw = '" & convertToSQLDatetime(txtapply_date.Text, txthour1.SelectedValue, txtmin1.SelectedValue) & "' "
                sql &= " WHERE  template_id = " & pk
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        Finally

        End Try

        Response.Redirect("idp_template_detail.aspx?mode=edit&id=" & pk & "&flag=" & flag)
    End Sub

    Protected Sub txtadd_category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtadd_category.SelectedIndexChanged
        bindTopic()
    End Sub

    Protected Sub cmdAddTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim new_order As String
        Try
            sql = "SELECT ISNULL(MAX(template_order_sort),0) + 1 FROM idp_template_detail WHERE"
            sql &= " template_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If
            new_order = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("template_detail_id", "idp_template_detail", conn)
            sql = "INSERT INTO idp_template_detail (template_detail_id , template_id , template_is_require , template_category_id , template_category_name , template_topic_id , template_topic_name , template_expect_id , template_expect_detail , template_methodogy_id , template_methodogy_name , template_start_date , template_start_date_ts , template_complete_date , template_complete_date_ts , template_order_sort) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & txtadd_require.SelectedValue & "' ,"
            sql &= " '" & txtadd_category.SelectedValue & "' ,"
            sql &= " '" & txtadd_category.SelectedItem.Text & "' ,"
            sql &= " '" & txtadd_topic.SelectedValue & "' ,"
            sql &= " '" & addslashes(txtadd_topic.SelectedItem.Text) & "' ,"
            sql &= " '" & addslashes(txtadd_expect.SelectedValue) & "' ,"
            sql &= " '" & addslashes(txtadd_expect.SelectedItem.Text) & "' ,"
            sql &= " '" & txtadd_method1.SelectedValue & "' ,"
            sql &= " '" & addslashes(txtadd_method1.SelectedItem.Text) & "' ,"

            If txtadd_start.Text = "" Then
                sql &= " null ,"
                sql &= " '" & 0 & "' ,"
            Else
                sql &= " '" & convertToSQLDatetime(txtadd_start.Text) & "' ,"
                sql &= " '" & ConvertDateStringToTimeStamp(txtadd_start.Text) & "' ,"
            End If

            If txtadd_complete.Text = "" Then
                sql &= " null ,"
                sql &= " '" & 0 & "' ,"
            Else
                sql &= " '" & convertToSQLDatetime(txtadd_complete.Text) & "' ,"
                sql &= " '" & ConvertDateStringToTimeStamp(txtadd_complete.Text) & "' ,"
            End If
           
            sql &= " '" & new_order & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If
            ' Response.Write(sql)
            If flag = "ladder" Then
                sql = "UPDATE idp_template_detail SET template_limit =  '" & txtadd_limit.Text & "' , is_ladder = 1 , template_score = '" & txtadd_score.Text & "' WHERE template_detail_id = " & pk
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
            End If

            conn.setDBCommit()

            bindGridTemplate()
            txtadd_require.SelectedIndex = 0
            txtadd_category.SelectedIndex = 0
            txtadd_topic.SelectedIndex = 0
            txtadd_expect.SelectedIndex = 0
            txtadd_method1.SelectedIndex = 0
            txtadd_start.Text = ""
            txtadd_complete.Text = ""
            txtadd_limit.Text = ""
            txtadd_score.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridTemplate_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridTemplate.PageIndexChanging
        GridTemplate.PageIndex = e.NewPageIndex
        bindGridTemplate()
    End Sub

    Protected Sub GridTemplate_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridTemplate.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
         
            Dim lblRequire As Label = CType(e.Row.FindControl("lblRequire"), Label)
            Dim txtedit_require As DropDownList = CType(e.Row.FindControl("txtedit_require"), DropDownList)
            '  Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                txtedit_require.SelectedValue = lblRequire.Text

                If lblRequire.Text = "0" Then
                    ' lblRequire.Text = "Elective"

                Else
                    'lblRequire.Text = "Require"
                    e.Row.BackColor = Drawing.Color.LightBlue
                    '   chk.Visible = False
                End If


            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridTemplate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridTemplate.SelectedIndexChanged

    End Sub

    Protected Sub cmdDelTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridTemplate.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridTemplate.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridTemplate.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM idp_template_detail WHERE template_detail_id = " & lbl.Text
                    'sql = "UPDATE idp_template_detail SET is_delete =  WHERE template_detail_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindGridTemplate()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        bindEmployeeAll()
    End Sub

    Protected Sub cmdSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        While lblDivisionAll.Items.Count > 0 AndAlso lblDivisionAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblDivisionAll.SelectedItem
            selectedItem.Selected = False
            lblDivisionSelect.Items.Add(selectedItem)
            lblDivisionAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        While lblDivisionSelect.Items.Count > 0 AndAlso lblDivisionSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblDivisionSelect.SelectedItem
            selectedItem.Selected = False
            lblDivisionAll.Items.Add(selectedItem)
            lblDivisionSelect.Items.Remove(selectedItem)
        End While
    End Sub

   

    Sub saveOrderNoCommit()
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim txtorder As TextBox
        Dim txtlimit As TextBox
        Dim txtedit_score As TextBox
        Dim txtedit_require As DropDownList

        i = GridTemplate.Rows.Count



        For s As Integer = 0 To i - 1

            lbl = CType(GridTemplate.Rows(s).FindControl("lblPK"), Label)
            txtorder = CType(GridTemplate.Rows(s).FindControl("txtorder"), TextBox)
            txtlimit = CType(GridTemplate.Rows(s).FindControl("txtlimit"), TextBox)
            txtedit_score = CType(GridTemplate.Rows(s).FindControl("txtedit_score"), TextBox)
            txtedit_require = CType(GridTemplate.Rows(s).FindControl("txtedit_require"), DropDownList)

            sql = "UPDATE idp_template_detail SET template_order_sort = '" & txtorder.Text & "' , template_limit = '" & txtlimit.Text & "'"
            sql &= " , template_score = '" & txtedit_score.Text & "' "
            sql &= " , template_is_require = " & txtedit_require.SelectedValue
            sql &= " WHERE template_detail_id = " & lbl.Text

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
                Exit For
            End If
        Next s


    End Sub

    Sub updateOrder()
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim txtorder As TextBox


        i = GridTemplate.Rows.Count



        For s As Integer = 0 To i - 1

            lbl = CType(GridTemplate.Rows(s).FindControl("lblPK"), Label)
            txtorder = CType(GridTemplate.Rows(s).FindControl("txtorder"), TextBox)

            sql = "UPDATE idp_template_detail SET template_order_sort = '" & txtorder.Text & "' WHERE template_detail_id = " & lbl.Text

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
                Exit For
            End If
        Next s


    End Sub

    Protected Sub cmdCCAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCCAdd.Click
        While lblCCAll.Items.Count > 0 AndAlso lblCCAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblCCAll.SelectedItem
            selectedItem.Selected = False
            lblCCSelect.Items.Add(selectedItem)
            lblCCAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub lblCCRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCCRemove.Click
        While lblCCSelect.Items.Count > 0 AndAlso lblCCSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblCCSelect.SelectedItem
            selectedItem.Selected = False
            lblCCAll.Items.Add(selectedItem)
            lblCCSelect.Items.Remove(selectedItem)
        End While
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

    Protected Sub cmdAddTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTitle.Click
        While lblJobTitleAll.Items.Count > 0 AndAlso lblJobTitleAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblJobTitleAll.SelectedItem
            selectedItem.Selected = False
            lblJobTitleSelect.Items.Add(selectedItem)
            lblJobTitleAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveTitle.Click
        While lblJobTitleSelect.Items.Count > 0 AndAlso lblJobTitleSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblJobTitleSelect.SelectedItem
            selectedItem.Selected = False
            lblJobTitleAll.Items.Add(selectedItem)
            lblJobTitleSelect.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdSearchJobType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchJobType.Click
        bindJobTypeAll()
    End Sub

    Protected Sub cmdSearchJobTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchJobTitle.Click
        bindJobTitleAll()
    End Sub

    Protected Sub cmdUploadCSV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUploadCSV.Click
        Dim strFileName As String = ""

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


        Dim Sr As New StreamReader(Server.MapPath(strPath) & strFilesName, Encoding.UTF8)
        Dim sb As New System.Text.StringBuilder()
        Dim s As String
        While Not Sr.EndOfStream
            s = Sr.ReadLine()
            'cmd.CommandText = (("INSERT INTO MyTable Field1, Field2, Field3 VALUES(" + s.Split(","c)(1) & ", ") + s.Split(","c)(2) & ", ") + s.Split(","c)(3) & ")"
            If s.Split(","c)(1) <> "" And s.Split(","c)(0) <> "" Then
                lblDivisionSelect.Items.Add(New ListItem(s.Split(","c)(1), s.Split(","c)(0)))
            End If


            'cmd.ExecuteNonQuery()
        End While


    End Sub

    Protected Sub cmdSave1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave1.Click
        If mode = "add" Then
            onAddSave()
        ElseIf mode = "edit" Then
            onEditSave()

        End If
    End Sub
End Class
