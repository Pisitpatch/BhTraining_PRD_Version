Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_idp_template_list
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected flag As String = ""

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("idp_template_detail.aspx?mode=add&flag=" & flag)
    End Sub

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

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        viewtype = Request.QueryString("viewtype")
        Session("viewtype") = viewtype & ""
        flag = Request.QueryString("flag")

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            bindConfig()
            bindYear()
            bindDept()
            bindJobTitleAll()
            bindJobTypeAll()
            bindGrid()

            If flag = "ladder" Then
                lblHeader.Text = "Nursing Clinical Ladder Program"
                div_config.Visible = False
            End If

            If viewtype = "hr" Or viewtype = "educator" Then
                div_hr.Visible = True

            Else
                div_hr.Visible = False
                div_config.Visible = False
            End If

            If viewtype = "hr" And flag <> "ladder" Then
                div_config.Visible = False
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

    Sub bindDept()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM user_dept WHERE 1 = 1 "
            If viewtype = "educator" Then
                sql &= " AND dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
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

    Sub bindConfig()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM idp_config WHERE idp_config_id = 1 "
           ds = conn.getDataSet(sql, "t1")

            txtconfig.SelectedValue = ds.Tables("t1").Rows(0)("idp_config_value").ToString

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid(Optional ByVal citeria As String = "")
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_template_master WHERE 1 = 1 AND ISNULL(is_delete,0) = 0 "

            If flag = "ladder" Then
                sql &= " AND is_ladder_template = 1 "
            Else
                sql &= " AND ISNULL(is_ladder_template,0) = 0 "
            End If

            If viewtype = "educator" Then
                sql &= " AND (template_id IN (SELECT template_id FROM idp_template_costcenter WHERE costcenter_id IN  (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") )"
                'sql &= " OR create_by_dept_id = " & Session("dept_id").ToString
                sql &= " OR create_by_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & " )"

                sql &= ")"
            End If

            If txtfindemp.Text <> "" Then
                sql &= " AND template_id IN (SELECT template_id FROM idp_template_employee WHERE emp_code LIKE '%" & txtfindemp.Text & "%' OR LOWER(emp_name_th) LIKE '%" & txtfindemp.Text.ToLower & "%' OR LOWER(emp_name_en) LIKE '%" & txtfindemp.Text.ToLower & "%'  )"
            End If

            If txtjobtitle.SelectedValue <> "" Then
                sql &= " AND template_id IN (SELECT template_id FROM idp_template_jobtitle WHERE LOWER(job_title_en) = '" & txtjobtitle.SelectedValue & "' OR LOWER(job_title_th) = '" & txtjobtitle.SelectedValue & "'  )"
            End If

            If txtjobtype.SelectedValue <> "" Then
                sql &= " AND template_id IN (SELECT template_id FROM idp_template_jobtype WHERE LOWER(job_type_name_en) = '" & txtjobtype.SelectedValue & "' OR LOWER(job_type_name_th) = '" & txtjobtype.SelectedValue & "'  )"
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND template_id IN (SELECT template_id FROM idp_template_costcenter WHERE costcenter_id = '" & txtdept.SelectedValue & "' ) "
            End If

            If txtprogramname.Text <> "" Then
                sql &= " AND LOWER(template_title) LIKE '%" & txtprogramname.Text.ToLower().Trim & "%' "
            End If

            If txtprogramno.Text <> "" Then
                sql &= " AND (template_runno LIKE '%" & txtprogramno.Text & "%' OR template_yearno LIKE '%" & txtprogramno.Text & "%') "
            End If

            If txtyear.SelectedIndex > 0 Then
                sql &= " AND template_yearno = " & txtyear.SelectedValue
            End If

            If citeria <> "" Then
                sql &= " AND 1 > 2 "
            End If

            sql &= " ORDER BY template_title ASC "
            'Response.Write(sql)

            ds = conn.getDataSet(sql, "t1")

            lblNum.Text = ds.Tables("t1").Rows.Count
            GridView1.DataSource = ds
            GridView1.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Function padNo(ByVal s As String) As String
        Return s.PadLeft(5, "0")
    End Function

    Protected Function getActive(ByVal s As String) As String
        If s = "0" Then
            Return "<strong style='color:red'>Drafted</strong?"
        Else
            Return "<strong style='color:green'>Active</strong>"
        End If

    End Function

    Sub bindJobTypeAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT job_type FROM user_profile WHERE 1 = 1 "
           
            sql &= " GROUP BY job_type ORDER BY job_type"

            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtjobtype.DataSource = ds
            txtjobtype.DataBind()

            txtjobtype.Items.Insert(0, New ListItem("-- Please Select --", ""))
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
          
            sql &= " GROUP BY job_title ORDER BY job_title"
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtjobtitle.DataSource = ds
            txtjobtitle.DataBind()

            txtjobtitle.Items.Insert(0, New ListItem("-- Please Select --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindYear()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT template_yearno FROM idp_template_master WHERE 1 = 1 "

            sql &= " GROUP BY template_yearno ORDER BY template_yearno"
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtyear.DataSource = ds
            txtyear.DataBind()

            txtyear.Items.Insert(0, New ListItem("-- Please Select --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub

    Protected Sub cmdUpdateStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateStatus.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chkSelect As CheckBox

        i = GridView1.Rows.Count

        Try

            For s As Integer = 0 To i - 1
                chkSelect = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)
                If chkSelect.Checked = True Then
                    lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)


                    sql = "UPDATE idp_template_master SET is_active = '" & txtstatus.SelectedValue & "' "
                    sql &= "  WHERE template_id = " & lbl.Text
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

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        txtyear.SelectedIndex = 0
        txtdept.SelectedIndex = 0
        txtjobtitle.SelectedIndex = 0
        txtjobtype.SelectedIndex = 0
        txtfindemp.Text = ""

        bindGrid("Clear all")
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chkSelect As CheckBox

        i = GridView1.Rows.Count

        Try

            For s As Integer = 0 To i - 1
                chkSelect = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)
                If chkSelect.Checked = True Then
                    lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)


                    sql = "UPDATE idp_template_master SET is_delete = 1 "
                    sql &= "  WHERE template_id = " & lbl.Text
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

    Protected Sub cmdConfig_Click(sender As Object, e As EventArgs) Handles cmdConfig.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Try

            sql = "UPDATE idp_config SET idp_config_value = '" & txtconfig.SelectedValue & "' "
            sql &= "  WHERE idp_config_id = 1 "
            'Response.Write(sql & "<br/>")

            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            bindConfig()
        Catch ex As Exception
            'conn.setDBRollback()
            Response.Write(ex.Message)
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


            Next s


        Catch ex As Exception

            Response.Write(ex.Message)
        End Try
    End Sub
End Class
