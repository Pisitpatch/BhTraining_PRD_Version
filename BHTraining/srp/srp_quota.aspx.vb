Imports System.Data
Imports ShareFunction
Imports System.IO
Imports System.Data.OleDb

Partial Class srp_sqp_quota
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

            txtcal_y.Text = Date.Now.Year
            txtcal_q.SelectedValue = (Date.Now.Month - 1) \ 3 + 1
            bindDeptCommand()
            bindJobtypeCommand()
            bindManagerCommand()
            bindGridChainCommand()
            bindDept()
            bindJobtype()
            bindGrid() ' refresh ทุกครั้ง

            bindQuarter()
            txtfind_quarter.SelectedIndex = txtfind_quarter.Items.Count - 1 ' Calculate Quota
            txtfind_quarter2.SelectedIndex = txtfind_quarter2.Items.Count - 1 ' Create Quarter Issue
            cmdCreateIssueQuota0.Text = "Create Issue from quota in Quarter " & txtfind_quarter2.SelectedItem.Text
            bindGridCalQuota()
            bindGridIssueQuota()


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
            sql = "SELECT * FROM srp_m_quota_template WHERE 1 = 1 "
            sql &= " ORDER BY CAST(job_title_name AS INT)  "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            BuildNoRecords(gridview1, ds)
            ' gridview1.DataSource = ds
            'gridview1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridCalQuota()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM srp_m_calculate_quota a  "
            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT ISNULL(COUNT(*),0) AS issue_num , r_award_by_emp_code FROM srp_point_movement "
            '     sql &= " WHERE cast(quater_no as varchar) + '/' + cast(year_no as varchar)  "
            sql &= " GROUP BY r_award_by_emp_code "
            sql &= " ) b ON a.mgr_emp_code = b.r_award_by_emp_code "
            sql &= " WHERE 1 = 1 "
            If txtfind_dept.SelectedValue <> "" Then
                sql &= " AND mgr_dept_id = " & txtfind_dept.SelectedValue
            End If
            If txtfind_quarter.SelectedValue <> "" Then
                sql &= " AND cast(quater_no as varchar) + '/' + cast(year_no as varchar) = '" & txtfind_quarter.SelectedValue & "'"
            End If
            If txtfind_jobtype.SelectedValue <> "" Then
                sql &= " AND mgr_jobtitle LIKE '%" & addslashes(txtfind_jobtype.SelectedValue) & "%' "
            End If
            If txtfind_name.Text <> "" Then
                sql &= " AND (LOWER(mgr_name) LIKE '%" & txtfind_name.Text.ToLower & "%' OR  mgr_emp_code LIKE '%" & txtfind_name.Text & "%') "
            End If
            sql &= " ORDER BY  mgr_dept_name , mgr_name "
            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            ' gridview3.PageSize = 500
            gridview3.DataSource = ds
            gridview3.DataBind()

            lblQuotaNum.Text = FormatNumber(ds.Tables("t1").Rows.Count, 0)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridIssueQuota()
        Dim sql As String
        Dim ds As New DataSet
        Dim result As Integer = 0

        Try
            sql = "SELECT * , cast(quater_no as varchar) + '/' + cast(year_no as varchar)  AS quater FROM srp_m_quarter_issue a "
            sql &= " INNER JOIN user_profile b ON a.mgr_emp_code = b.emp_code "
            sql &= " WHERE 1 = 1 "
            If txtfind_dept2.SelectedValue <> "" Then
                ' sql &= " AND mgr_dept_id = " & txtfind_dept.SelectedValue
            End If
            If txtfind_quarter2.SelectedValue <> "" Then
                sql &= " AND cast(quater_no as varchar) + '/' + cast(year_no as varchar) = '" & txtfind_quarter2.SelectedValue & "'"
            End If
            If txtmgrname.Text <> "" Then
                sql &= " AND (mgr_emp_name LIKE '%" & txtmgrname.Text & "%' OR mgr_emp_code LIKE '%" & txtmgrname.Text & "%') "
            End If
            If txtfind_jobtype2.SelectedValue <> "" Then
                sql &= " AND mgr_job_type LIKE '%" & txtfind_jobtype2.SelectedValue & "%' "
            End If
            If txtfind_cardid.Text <> "" Then
                If Integer.TryParse(txtfind_cardid.Text, result) Then
                    sql &= " AND card_id_start > 0 AND  " & txtfind_cardid.Text & " BETWEEN card_id_start AND card_id_end "
                Else
                    sql &= " AND 1 > 2  "
                End If

            End If

            sql &= " ORDER BY quater_no, year_no, mgr_dept_name, mgr_emp_name "
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            gridview4.DataSource = ds
            gridview4.DataBind()

            lblIssueNum.Text = FormatNumber(ds.Tables("t1").Rows.Count, 0)
            If ds.Tables("t1").Rows.Count = 0 Then
                cmdGenerate0.Enabled = False
                cmdGenerate1.Enabled = False
                cmdSaveIssueQuota0.Enabled = False
                cmdSaveIssueQuota.Enabled = False
            Else
                cmdGenerate0.Enabled = True
                cmdGenerate1.Enabled = True
                cmdSaveIssueQuota0.Enabled = True
                cmdSaveIssueQuota.Enabled = True
            End If

            validateDupplicateCardAll()
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
            txtfind_dept.DataSource = ds
            txtfind_dept.DataBind()

            txtfind_dept.Items.Insert(0, New ListItem("-- All --", ""))

            txtfind_dept2.DataSource = ds
            txtfind_dept2.DataBind()

            txtfind_dept2.Items.Insert(0, New ListItem("-- All --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDeptCommand()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT b.dept_name AS dept_name_en , b.dept_id FROM srp_m_chain_command a INNER JOIN user_profile b ON a.emp_code = b.emp_code  "
            'sql &= " job_type IN (SELECT )"
            sql &= " GROUP BY b.dept_name , b.dept_id ORDER BY b.dept_name "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtcommand_dept.DataSource = ds
            txtcommand_dept.DataBind()

            txtcommand_dept.Items.Insert(0, New ListItem("-- All --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally

            ds.Dispose()
        End Try
    End Sub


    Sub bindJobtype()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT mgr_jobtitle AS job_type FROM srp_m_calculate_quota  "
            'sql &= " job_type IN (SELECT )"
            sql &= " GROUP BY mgr_jobtitle ORDER BY mgr_jobtitle "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtfind_jobtype.DataSource = ds
            txtfind_jobtype.DataBind()

            txtfind_jobtype.Items.Insert(0, New ListItem("-- All --", ""))

            txtfind_jobtype2.DataSource = ds
            txtfind_jobtype2.DataBind()

            txtfind_jobtype2.Items.Insert(0, New ListItem("-- All --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindJobtypeCommand()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT b.job_type AS job_type FROM srp_m_chain_command a INNER JOIN user_profile b ON a.emp_code = b.emp_code  "
            'sql &= " job_type IN (SELECT )"
            sql &= " GROUP BY b.job_type ORDER BY b.job_type "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtcommand_jobtype.DataSource = ds
            txtcommand_jobtype.DataBind()

            txtcommand_jobtype.Items.Insert(0, New ListItem("-- All --", ""))


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindManagerCommand()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT a.mgr_emp_code AS mgr_emp_code , ISNULL(b.user_fullname,b.user_fullname_local) AS user_fullname  FROM srp_m_chain_command a INNER JOIN user_profile b ON a.mgr_emp_code = b.emp_code  "

            sql &= " GROUP BY a.mgr_emp_code ,  ISNULL(b.user_fullname,b.user_fullname_local)  ORDER BY ISNULL(b.user_fullname,b.user_fullname_local) "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtcommand_manager.DataSource = ds
            txtcommand_manager.DataBind()

            txtcommand_manager.Items.Insert(0, New ListItem("-- All --", ""))


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindQuarter()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT cast(quater_no as varchar) + '/' + cast(year_no as varchar) AS id , cast(quater_no as varchar) + '/' + cast(year_no as varchar) AS name  , year_no , quater_no FROM srp_m_calculate_quota "

            sql &= " GROUP BY cast(quater_no as varchar) + '/' + cast(year_no as varchar) , year_no , quater_no "
            sql &= " ORDER BY year_no  ASC , quater_no ASC "
            'sql &= " ORDER BY quater_no , year_no "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtfind_quarter.DataSource = ds
            txtfind_quarter.DataBind()

            txtfind_quarter.Items.Insert(0, New ListItem("-- All --", ""))

            txtfind_quarter2.DataSource = ds
            txtfind_quarter2.DataBind()

            txtfind_quarter2.Items.Insert(0, New ListItem("-- All --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Public Sub BuildNoRecords(ByVal gridView As GridView, ByVal ds As DataSet)
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                'Add a blank row to the dataset
                ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
                'Bind the DataSet to the GridView
                gridView.DataSource = ds
                gridView.DataBind()
                'Get the number of columns to know what the Column Span should be
                Dim columnCount As Integer = gridView.Rows(0).Cells.Count
                'Call the clear method to clear out any controls that you use in the columns.  I use a dropdown list in one of the column so this was necessary.
                gridView.Rows(0).Cells.Clear()
                gridView.Rows(0).Cells.Add(New TableCell)
                gridView.Rows(0).Cells(0).ColumnSpan = columnCount
                gridView.Rows(0).Cells(0).Text = ""
            Else
                gridView.DataSource = ds
                gridView.DataBind()
            End If
        Catch ex As Exception
            'Do your exception handling here
        End Try
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click
        Dim txtjobtitle As Label
        Dim txtcal As DropDownList
        Dim txtmonth As TextBox
        Dim txtjobtype As TextBox

        Dim lblPK As Label
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try
            For i As Integer = 0 To gridview1.Rows.Count - 1

                txtjobtitle = CType(gridview1.Rows(i).FindControl("lblJobTitle"), Label)
                txtcal = CType(gridview1.Rows(i).FindControl("txtcal"), DropDownList)
                txtmonth = CType(gridview1.Rows(i).FindControl("txtx_month"), TextBox)
                txtjobtype = CType(gridview1.Rows(i).FindControl("txtx_jobtype"), TextBox)
                lblPK = CType(gridview1.Rows(i).FindControl("lblPK"), Label)

                sql = "UPDATE srp_m_quota_template SET job_title_name = '" & addslashes(txtjobtitle.Text) & "'"
                'Response.Write(22)
                sql &= " , calculate_percent = '" & txtcal.SelectedValue & "' "
                sql &= " , x_month = '" & txtmonth.Text & "' "
                sql &= " , x_job_type = '" & addslashes(txtjobtype.Text) & "' "

                sql &= " WHERE quota_id = " & lblPK.Text
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

            Next i

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            bindGrid()
        End Try
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim txtjobtitle As DropDownList
        Dim txtcal As DropDownList
        Dim txtmonth As TextBox
        Dim txtjobtype As TextBox

        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        'Response.Write(txtjobtitle.Text)

        Try
            txtjobtitle = CType(gridview1.FooterRow.FindControl("txtadd_jobtitle1"), DropDownList)
            txtcal = CType(gridview1.FooterRow.FindControl("txtadd_cal"), DropDownList)
            txtmonth = CType(gridview1.FooterRow.FindControl("txtadd_month"), TextBox)
            txtjobtype = CType(gridview1.FooterRow.FindControl("txtadd_jobtype"), TextBox)
            'Response.Write(111)
            pk = getPK("quota_id", "srp_m_quota_template", conn)
            sql = "INSERT INTO srp_m_quota_template (quota_id , job_title_name , calculate_percent , x_month , x_job_type , last_update_ts , last_update_raw , last_update_by) "
            sql &= " VALUES("
            sql &= " '" & pk & "' , "
            '    Response.Write(sql)
            sql &= " '" & addslashes(txtjobtitle.SelectedValue) & "' , "
            sql &= " '" & txtcal.SelectedValue & "' , "

            sql &= " '" & 3 & "' , "
            sql &= " '" & txtjobtype.Text & "' , "
            sql &= " '" & Date.Now.Ticks & "' , "
            sql &= " GETDATE() , "
            sql &= " '" & Session("user_fullname").ToString & "'  "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally

        End Try

    End Sub

    Protected Sub gridview1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview1.PageIndexChanging
        gridview1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub gridview1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridview1.RowCreated

    End Sub

    Protected Sub gridview1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridview1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblCal As Label
            Dim txtcal As DropDownList
            lblCal = CType(e.Row.FindControl("lblCal"), Label)
            txtcal = CType(e.Row.FindControl("txtcal"), DropDownList)

            Try
                txtcal.SelectedValue = lblCal.Text
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim sql As String
            Dim ds As New DataSet
            Try
                Dim txtjob_title_footer As DropDownList
                txtjob_title_footer = CType(e.Row.FindControl("txtadd_jobtitle1"), DropDownList)

                '  sql = "SELECT job_type FROM user_profile WHERE job_type NOT IN (SELECT job_title_name FROM srp_m_quota_template) GROUP BY job_type ORDER BY job_type"
                sql = "SELECT grade AS job_type FROM srp_m_grade "
                ds = conn.getDataSetForTransaction(sql, "t10")
                txtjob_title_footer.DataSource = ds
                txtjob_title_footer.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try

        End If
    End Sub

    Protected Sub gridview1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview1.SelectedIndexChanged

    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As System.EventArgs) Handles cmdDelete.Click
        Dim chkSelect As CheckBox
        Dim lblPK As Label
        Dim sql As String
        Dim errorMsg As String

        Try
            For i As Integer = 0 To gridview1.Rows.Count - 1
                chkSelect = CType(gridview1.Rows(i).FindControl("chkSelect"), CheckBox)
                lblPK = CType(gridview1.Rows(i).FindControl("lblPK"), Label)
                If chkSelect.Checked = True Then
                    sql = "DELETE FROM srp_m_quota_template WHERE quota_id = " & lblPK.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If
            Next i

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            bindGrid()
        End Try

    End Sub

    Protected Sub cmdUpload_Click(sender As Object, e As System.EventArgs) Handles cmdUpload.Click
        csvUpload()
        ' csvValidateManager()

        'excelUpload()
        bindJobtypeCommand()
        bindDeptCommand()
        bindManagerCommand()
        bindGridChainCommand()

        bindGrid()
        bindJobtype()

    End Sub

    Sub bindGridChainCommand()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT a.* , b.* , c.user_fullname AS mgr_name1  FROM srp_m_chain_command a INNER JOIN user_profile b ON a.emp_code = b.emp_code  "
            sql &= " INNER JOIN user_profile c ON a.mgr_emp_code = c.emp_code "
            sql &= " WHERE 1 = 1 "
            If txtcommand_name.Text <> "" Then
                sql &= " AND (b.user_fullname LIKE '%" & txtcommand_name.Text & "%' OR a.emp_code LIKE '%" & txtcommand_name.Text & "%') "
            End If
            If txtcommand_dept.SelectedValue <> "" Then
                sql &= " AND b.dept_id = " & txtcommand_dept.SelectedValue
            End If
            If txtcommand_jobtype.SelectedValue <> "" Then
                sql &= " AND b.job_type LIKE '%" & txtcommand_jobtype.SelectedValue & "%' "
            End If
            If txtcommand_manager.SelectedValue <> "" Then
                sql &= " AND a.mgr_emp_code = " & txtcommand_manager.SelectedValue
            End If

            sql &= " ORDER BY b.dept_name "
            '' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            gridview2.DataSource = ds
            gridview2.DataBind()

            lblNumCommand.Text = FormatNumber(ds.Tables("t1").Rows.Count, 0)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub csvUpload()
        Dim strFileName As String = ""
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try


            If Not IsNothing(myFile1.PostedFile) Then
                strFileName = myFile1.FileName

                If strFileName = "" Then
                    Return
                End If


                myFile1.PostedFile.SaveAs(Server.MapPath("../share/fte/" & myFile1.FileName))


            End If


        Catch ex As Exception

            Response.Write(ex.Message)
            Return
        Finally

        End Try


        Dim strFilesName As String = strFileName
        Dim strPath As String = "../share/fte/"


        Dim Sr As New StreamReader(Server.MapPath(strPath) & strFilesName, Encoding.Default, True)
        Dim sb As New System.Text.StringBuilder()
        Dim s As String
        Dim irow As Integer = 1
        Dim result As Int32 = 0
        Try
            sql = "DELETE FROM srp_m_chain_command"
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            While Not Sr.EndOfStream
                s = Sr.ReadLine()
                'cmd.CommandText = (("INSERT INTO MyTable Field1, Field2, Field3 VALUES(" + s.Split(","c)(1) & ", ") + s.Split(","c)(2) & ", ") + s.Split(","c)(3) & ")"
                ' lblDivisionSelect.Items.Add(New ListItem(s.Split(","c)(1), s.Split(","c)(0)))

                If irow > 1 And s.Split(","c)(1).ToString.Trim <> "" Then

                    If Int32.TryParse(s.Split(","c)(0), result) Then
                        If s.Split(","c)(1) = 215020 Then
                            ' Response.Write("yes<hr/>")
                        End If
                        sql = "INSERT INTO srp_m_chain_command (emp_code,mgr_emp_code,grade"
                        sql &= ") VALUES("
                        sql &= " '" & s.Split(","c)(0) & "' , "

                        If Int32.TryParse(s.Split(","c)(1), result) Then
                            sql &= " '" & s.Split(","c)(1) & "' , "
                        Else
                            sql &= " '" & s.Split(","c)(0) & "' , "
                        End If

                        sql &= " '" & addslashes(s.Split(","c)(2)) & "'  " ' emp_name
                        sql &= ")"
                        '  Response.Write(sql & "<br/>")
                        errorMsg = conn.executeSQLForTransaction(sql)
                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg & sql)
                        End If
                    End If

                End If

                'cmd.ExecuteNonQuery()

                irow += 1
            End While

            sql = "INSERT INTO srp_m_chain_command (emp_code,mgr_emp_code,grade)"
            sql &= " select emp_code,emp_code,grade from srp_m_chain_command"
            sql &= " where grade in (5,6,7,8,9,10,11,12,13 ) and emp_code not in (select mgr_emp_code from srp_m_chain_command)"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If



            conn.setDBCommit()
            '  bindGridChainCommand()
            '  bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub csvValidateManager()
        Dim strFileName As String = ""
        Dim sql As String
        Dim errorMsg As String


        Dim irow As Integer = 1
        Dim result As Int32 = 0
        Try


            sql = "INSERT INTO srp_m_chain_command (emp_code,mgr_emp_code,grade)"
            sql &= " select emp_code,emp_code,grade from srp_m_chain_command"
            sql &= " where grade in (5,6) and emp_code not in (select mgr_emp_code from srp_m_chain_command)"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            conn.setDBCommit()
            '  bindGridChainCommand()
            '  bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try

    End Sub


    Sub excelUpload()
        Dim sql As String = ""
        Dim errorMsg As String = ""

        If Not IsNothing(myFile1.PostedFile) Then

            Dim tempDs As New DataSet
            Dim filePath As String = "\share\fte\SRP_FTE.xls"
            Dim i As Integer

            If myFile1.FileName = "" Then
                lblMsg.Text = "Please select fte file to upload."
                Return
            Else
                lblMsg.Text = ""
            End If

            filePath = "../share/fte/" & myFile1.FileName
            '*** Save Images ***'
            myFile1.PostedFile.SaveAs(Server.MapPath(filePath))

            ' Dim connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & "C:\SoftwareDevelop\vbproject\BHOnline\share\fte\FTE.xls" & ";Extended Properties=Excel 8.0"
            Dim connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Server.MapPath(filePath) & ";Extended Properties=Excel 8.0"


            ' Create the connection object
            Dim oledbConn As OleDbConnection = New OleDbConnection(connString)
            Try
                ' Open connection
                oledbConn.Open()

                ' Create OleDbCommand object and select data from worksheet Sheet1
                Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn)

                ' Create new OleDbDataAdapter
                Dim oleda As OleDbDataAdapter = New OleDbDataAdapter()

                oleda.SelectCommand = cmd

                ' Create a DataSet which will hold the data extracted from the worksheet.
                Dim ds As DataSet = New DataSet()

                ' Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(ds, "Employees")

                'Response.Write(ds.Tables(0).Rows.Count)
                ' Bind the data to the GridView
                ' updateToDatabase(ds)
                sql = "DELETE FROM srp_m_chain_command"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If


                i = tempDs.Tables(0).Rows.Count

                For iRow As Int32 = 0 To i - 1
                    ' Response.Write(tempDs.Tables(0).Rows(iRow)(7).ToString)
                    sql = "INSERT INTO srp_m_chain_command (emp_code,mrg_name"

                    sql &= ") VALUES("
                    sql &= " '" & tempDs.Tables(0).Rows(iRow)(0).ToString & "' , "
                    sql &= " '" & tempDs.Tables(0).Rows(iRow)(1).ToString & "' , "
                    sql &= " '" & tempDs.Tables(0).Rows(iRow)(2).ToString & "'  " ' emp_name


                    sql &= ")"

                    'sql = sql.Replace("'", "\'")
                    Try
                        errorMsg = conn.executeSQLForTransaction(sql)
                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg)
                        End If
                    Catch ex As Exception
                        ' Response.Write(ex.Message)
                        '  Response.Write("<hr/>")
                        lblMsg.Text = tempDs.Tables(0).Rows(iRow)(2).ToString & " can't import becauase :: " & vbCrLf & ex.Message & vbCrLf
                    End Try


                Next iRow


                'gridview2.DataSource = ds
                'gridview2.DataBind()



                '  ds.Clear()
                ' ds = Nothing
            Catch ex As Exception
                ' Response.Write(ex.Message)
                lblMsg.Text = ex.Message
            Finally

                ' Close connection
                oledbConn.Close()
            End Try

        End If
    End Sub

    Protected Sub gridview2_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview2.PageIndexChanging
        gridview2.PageIndex = e.NewPageIndex
        bindGrid()
        bindGridChainCommand()
    End Sub

    Protected Sub gridview2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview2.SelectedIndexChanged

    End Sub

    Protected Sub cmdCal_Click(sender As Object, e As System.EventArgs) Handles cmdCal.Click
        Dim sql As String
        Dim ds As New DataSet
        Dim ds2 As New DataSet
        Dim errorMsg As String
        Dim pk As String
        Dim quarter As Integer = (Date.Now.Month - 1) \ 3 + 1
        Dim total As String = ""
        Dim calculate_percent As Integer = 1
        Dim x_month As Integer = 0
        Dim x_job As Integer = 0

        Dim cal_q As String = txtcal_q.SelectedValue
        Dim cal_y As String = txtcal_y.Text
        Dim num As String = ""

        Try
            quarter = cal_q

            sql = "DELETE FROM srp_m_calculate_quota WHERE quater_no = " & quarter & " AND year_no = " & cal_y
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            ' Find manager list
            sql = "SELECT a.mgr_emp_code , b.user_fullname , b.dept_id , b.dept_name , COUNT(a.emp_code) - 0 AS num , c.calculate_percent , c.x_month , c.x_job_type ,  b.job_type , d.grade"
            sql &= " FROM srp_m_chain_command a INNER JOIN user_profile b ON a.mgr_emp_code = b.emp_code "

            sql &= " inner join srp_m_chain_command d on a.mgr_emp_code = d.emp_code "
            sql &= " INNER JOIN srp_m_quota_template c ON  d.grade = c.job_title_name "
            sql &= " WHERE b.emp_code IN (SELECT Employee_id FROM temp_BHUser) "
            sql &= "  and a.emp_code <> d.mgr_emp_code "
            sql &= " GROUP BY a.mgr_emp_code , b.user_fullname , b.dept_id , b.dept_name , c.calculate_percent , c.x_month , c.x_job_type ,  b.job_type , d.grade "
            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(sql)
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                total = 0

                sql = "SELECT * FROM srp_m_chain_command WHERE emp_code = mgr_emp_code AND emp_code = " & ds.Tables("t1").Rows(i)("mgr_emp_code").ToString
                ds2 = conn.getDataSetForTransaction(sql, "t2")

                If (ds2.Tables("t2").Rows.Count > 0) Then
                    num = (Convert.ToInt32(ds.Tables("t1").Rows(i)("num").ToString) - 1).ToString
                Else
                    num = ds.Tables("t1").Rows(i)("num").ToString
                End If

                If Int32.TryParse(ds.Tables("t1").Rows(i)("calculate_percent").ToString, calculate_percent) And Int32.TryParse(ds.Tables("t1").Rows(i)("x_month").ToString, x_month) And Int32.TryParse(ds.Tables("t1").Rows(i)("x_job_type").ToString, x_job) Then
                    If calculate_percent = 20 Then

                        If num = 0 Then
                            total = CInt(ds.Tables("t1").Rows(i)("x_job_type").ToString)
                        Else
                            total = ((CInt(ds.Tables("t1").Rows(i)("num").ToString) * 0.2) * CInt(ds.Tables("t1").Rows(i)("x_month").ToString)) + CInt(ds.Tables("t1").Rows(i)("x_job_type").ToString)
                        End If


                    Else

                        total = CInt(ds.Tables("t1").Rows(i)("x_job_type").ToString)
                    End If

                    If total < 0 Then
                        total = 0
                    End If
                End If

                If CInt(ds.Tables("t1").Rows(i)("grade").ToString) > 6 And CInt(ds.Tables("t1").Rows(i)("calculate_percent").ToString) > 0 Then
                    ' Response.Write(sql)
                    Continue For
                End If

          

                pk = getPK("cal_id", "srp_m_calculate_quota", conn)
                sql = "INSERT INTO srp_m_calculate_quota (cal_id , quater_no , year_no , mgr_emp_code , mgr_dept_id , mgr_dept_name"
                sql &= " , mgr_name, mgr_jobtitle , staff_amount , calculate_percent , x_month , x_jobtype , quota_total "
                sql &= " , last_update_ts , last_update_raw , update_by"
                sql &= ") VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & quarter & "' ,"
                sql &= " '" & cal_y & "' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("mgr_emp_code").ToString & "' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("dept_id").ToString & "' ,"
                sql &= " '" & addslashes(ds.Tables("t1").Rows(i)("dept_name").ToString) & "' ,"
                sql &= " '" & addslashes(ds.Tables("t1").Rows(i)("user_fullname").ToString) & "' ,"
                sql &= " '" & addslashes(ds.Tables("t1").Rows(i)("job_type").ToString) & "' ,"
                sql &= " '" & num & "' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("calculate_percent").ToString & "' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("x_month").ToString & "' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("x_job_type").ToString & "' ,"
                sql &= " '" & total & "' ,"
                sql &= " '" & Date.Now.Ticks & "' ,"
                sql &= " GETDATE() ,"
                sql &= " '" & Session("user_fullname").ToString & "' "

                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
            Next i


            conn.setDBCommit()
            bindGridCalQuota()
            bindQuarter()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub gridview3_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview3.PageIndexChanging
        gridview3.PageIndex = e.NewPageIndex
        bindGridCalQuota()
    End Sub



    Protected Sub gridview3_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridview3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPercent As Label = CType(e.Row.FindControl("lblPercent"), Label)
            Dim txtpercent As DropDownList = CType(e.Row.FindControl("txtpercent"), DropDownList)

            Try
                txtpercent.SelectedValue = lblPercent.Text
            Catch ex As Exception
                txtpercent.SelectedValue = 0
            End Try
        End If
    End Sub

    Protected Sub gridview3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview3.SelectedIndexChanged

    End Sub

    Protected Sub cmdQuota_Click(sender As Object, e As System.EventArgs) Handles cmdQuota.Click
        bindGridCalQuota()
    End Sub

    Protected Sub cmdCreateIssueQuota_Click(sender As Object, e As System.EventArgs) Handles cmdCreateIssueQuota.Click
        createIssueQuota()
    End Sub

    Sub createIssueQuota()
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Dim pk As String = ""
        Dim qy As String()
        Try
            sql = "DELETE FROM srp_m_quarter_issue WHERE cast(quater_no as varchar) + '/' + cast(year_no as varchar) = '" & txtfind_quarter2.SelectedValue & "'"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            qy = txtfind_quarter2.SelectedValue.Split("/")

            sql = "SELECT * FROM srp_m_calculate_quota WHERE cast(quater_no as varchar) + '/' + cast(year_no as varchar) = '" & txtfind_quarter2.SelectedValue & "'"
            sql &= " ORDER BY year_no DESC , quater_no ASC "
            '   Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                pk = getPK("quater_issue_id", "srp_m_quarter_issue", conn)
                sql = "INSERT INTO srp_m_quarter_issue (quater_issue_id , quater_no , year_no , date_issue_raw , date_issue_ts , project_name "
                sql &= " , mgr_emp_code , mgr_emp_name , issue_qty , card_id_start , card_id_end , quota_qty , quota_issue_by "
                sql &= ", mgr_dept_id , mgr_dept_name , mgr_job_type "
                sql &= " ) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("quater_no").ToString & "' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("year_no").ToString & "' ,"
                sql &= " GETDATE() ,"
                sql &= " '" & Date.Now.Ticks & "' ,"
                sql &= " 'Quater Quota' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("mgr_emp_code").ToString & "' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("mgr_name").ToString & "' ,"
                sql &= " '" & Math.Round(CDbl(ds.Tables("t1").Rows(i)("quota_total").ToString)) & "' ,"
                sql &= " null ,"
                sql &= " null ,"
                sql &= " '" & Math.Round(CDbl(ds.Tables("t1").Rows(i)("quota_total").ToString)) & "' ,"
                sql &= " '" & Session("user_fullname").ToString & "' ,"
                sql &= " '" & ds.Tables("t1").Rows(i)("mgr_dept_id").ToString & "' ,"
                sql &= " '" & addslashes(ds.Tables("t1").Rows(i)("mgr_dept_name").ToString) & "' ,"
                sql &= " '" & addslashes(ds.Tables("t1").Rows(i)("mgr_jobtitle").ToString) & "' "
                sql &= ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i
            conn.setDBCommit()
            bindGridIssueQuota()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdSearchQuota_Click(sender As Object, e As System.EventArgs) Handles cmdSearchQuota.Click
        bindGridIssueQuota()
    End Sub

    Protected Sub gridview4_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview4.PageIndexChanging
        gridview4.PageIndex = e.NewPageIndex
        bindGridIssueQuota()
    End Sub

    Protected Sub gridview4_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridview4.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim chkSelectAll As CheckBox = CType(e.Row.FindControl("chkSelectAll"), CheckBox)


            chkSelectAll.Attributes.Add("onclick", "selectAll(this)")


        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cmdPrint As Button = CType(e.Row.FindControl("cmdPrint"), Button)
            Dim cmdValidate As Button = CType(e.Row.FindControl("cmdValidate"), Button)

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            cmdPrint.Attributes.Add("onclick", "window.open('preview_memo.aspx?id=" & lblPK.Text & "');return false;")
            ' cmdPrint.Enabled = False
        End If
    End Sub

    Protected Sub gridview4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview4.SelectedIndexChanged

    End Sub

    Protected Sub cmdSaveIssueQuota_Click(sender As Object, e As System.EventArgs) Handles cmdSaveIssueQuota.Click
        saveIssueQuota()
    End Sub

    Sub saveIssueQuota()
        Dim txtamount As TextBox
        Dim txtstart As TextBox
        Dim txtend As Label

        Dim lblPK As Label
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Dim id_start As Integer = 0
        Dim id_end As Integer = 0
        Dim qty As Integer = 0

        Try
            For i As Integer = 0 To gridview4.Rows.Count - 1


                txtamount = CType(gridview4.Rows(i).FindControl("txtamount"), TextBox)
                txtstart = CType(gridview4.Rows(i).FindControl("txtstart"), TextBox)
                txtend = CType(gridview4.Rows(i).FindControl("txtend"), Label)
                lblPK = CType(gridview4.Rows(i).FindControl("lblPK"), Label)

                Integer.TryParse(txtstart.Text, id_start)
                Integer.TryParse(txtamount.Text, qty)

                sql = "UPDATE srp_m_quarter_issue SET issue_qty = '" & txtamount.Text & "'"
                'Response.Write(22)
                If id_start > 0 Then
                    sql &= " , card_id_start = '" & txtstart.Text & "' "
                Else
                    sql &= " , card_id_start = null "
                End If

                If (id_start + qty) > 0 And id_start > 0 And qty > 0 Then
                    sql &= " , card_id_end = '" & ((id_start + qty) - 1) & "' "
                Else
                    sql &= " , card_id_end = null "
                End If



                sql &= " WHERE quater_issue_id = " & lblPK.Text
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
                '   Response.Write(sql)
            Next i

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            bindGridIssueQuota()
        End Try
    End Sub

    Protected Sub cmdSaveIssueQuota0_Click(sender As Object, e As System.EventArgs) Handles cmdSaveIssueQuota0.Click
        saveIssueQuota()
    End Sub

    Protected Sub cmdCreateIssueQuota0_Click(sender As Object, e As System.EventArgs) Handles cmdCreateIssueQuota0.Click
        createIssueQuota()
    End Sub

    Protected Sub cmdCommandSearch_Click(sender As Object, e As System.EventArgs) Handles cmdCommandSearch.Click
        bindGridChainCommand()
    End Sub

    Protected Sub cmdGenerate0_Click(sender As Object, e As System.EventArgs) Handles cmdGenerate0.Click
        generateCardID()
    End Sub

    Protected Sub cmdGenerate1_Click(sender As Object, e As System.EventArgs) Handles cmdGenerate1.Click
        generateCardID()
    End Sub

    Sub generateCardID()
        Dim chk As CheckBox
        Dim txtstart As TextBox
        '  Dim txtend As Label
        Dim txtamount As TextBox

        Dim id_start As Integer = 0
        Dim id_end As Integer = 0
        Dim qty As Integer = 0

        Dim lblPK As Label
        Dim sql As String
        Dim errorMsg As String
        ' Dim pk As String
        Dim ds As New DataSet
        Dim irow As Integer = 0
        Dim is_dupp As Boolean = False
        Try
            For i As Integer = 0 To gridview4.Rows.Count - 1
                chk = CType(gridview4.Rows(i).FindControl("chkSelect"), CheckBox)
                txtstart = CType(gridview4.Rows(i).FindControl("txtstart"), TextBox)
                txtamount = CType(gridview4.Rows(i).FindControl("txtamount"), TextBox)
                lblPK = CType(gridview4.Rows(i).FindControl("lblPK"), Label)

                If chk.Checked Then
                    If irow = 0 Then
                        Integer.TryParse(txtstart.Text, id_start)

                    Else
                    End If

                    Integer.TryParse(txtamount.Text, qty)

                    If (id_start + qty) > 0 And id_start > 0 And qty > 0 Then
                        is_dupp = isDupplicateCardID(id_start, ((id_start + qty) - 1), CInt(lblPK.Text))
                    End If

                    If (is_dupp = False) Then

                    End If

                    sql = "UPDATE srp_m_quarter_issue SET issue_qty = '" & txtamount.Text & "'"

                    If id_start > 0 Then
                        sql &= " , card_id_start = '" & id_start & "' "
                    Else
                        sql &= " , card_id_start = null "
                    End If
                    If (id_start + qty) > 0 And id_start > 0 And qty > 0 Then
                        sql &= " , card_id_end = '" & ((id_start + qty) - 1) & "' "
                    Else
                        sql &= " , card_id_end = null "
                    End If
                    sql &= " WHERE quater_issue_id = " & lblPK.Text
                    ' Response.Write(sql & "<br/>")
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If

                    id_start = id_start + qty
                    irow += 1
                End If


            Next i

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("generateCardID : " & ex.Message)
        Finally
            bindGridIssueQuota()
        End Try

    End Sub

    Sub onValidate(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim message As String
        Try

            message = e.CommandArgument.ToString
            Dim tempArray() As String
            tempArray = message.Split("#")

            If isDupplicateCardID(CInt(tempArray(0)), CInt(tempArray(1)), CInt(tempArray(2))) Then
                Response.Write("Dupplicated")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub validateDupplicateCard()
        Dim chk As CheckBox
        Dim txtstart As TextBox
        Dim txtend As Label
        Dim txtamount As TextBox

        Dim id_start As Integer = 0
        Dim id_end As Integer = 0
        Dim qty As Integer = 0

        Dim lblPK As Label
        Dim lblValidate As Label
        Dim sql As String
        Dim errorMsg As String
        ' Dim pk As String
        Dim ds As New DataSet
        Dim irow As Integer = 0
        Dim is_dupp As Boolean = False
        Try

            For i As Integer = 0 To gridview4.Rows.Count - 1
                chk = CType(gridview4.Rows(i).FindControl("chkSelect"), CheckBox)
                txtstart = CType(gridview4.Rows(i).FindControl("txtstart"), TextBox)
                txtend = CType(gridview4.Rows(i).FindControl("txtend"), Label)
                txtamount = CType(gridview4.Rows(i).FindControl("txtamount"), TextBox)
                lblPK = CType(gridview4.Rows(i).FindControl("lblPK"), Label)
                lblValidate = CType(gridview4.Rows(i).FindControl("lblValidate"), Label)

                ' lblValidate.Text = "777"
                If chk.Checked Then

                    Integer.TryParse(txtstart.Text, id_start)
                    Integer.TryParse(txtend.Text, id_end)
                    Integer.TryParse(txtamount.Text, qty)

                    If (id_start + qty) > 0 And id_start > 0 And qty > 0 Then
                        is_dupp = isDupplicateCardID(id_start, id_end, CInt(lblPK.Text))
                        If (is_dupp = True) Then
                            '  Response.Write("dup")
                            lblValidate.Text = "Dupplicate Card ID"
                        Else
                            'Response.Write("noo")
                            lblValidate.Text = ""
                        End If
                    Else
                        lblValidate.Text = ""
                    End If




                End If


            Next i

            ' conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("validateDupplicateCard : " & ex.Message)
        Finally
            'bindGridIssueQuota()
        End Try

    End Sub

    Sub validateDupplicateCardAll()
        Dim chk As CheckBox
        Dim txtstart As TextBox
        Dim txtend As Label
        Dim txtamount As TextBox

        Dim id_start As Integer = 0
        Dim id_end As Integer = 0
        Dim qty As Integer = 0

        Dim lblPK As Label
        Dim lblValidate As Label
        Dim sql As String
        Dim errorMsg As String
        ' Dim pk As String
        Dim ds As New DataSet
        Dim irow As Integer = 0
        Dim is_dupp As Boolean = False
        Try

            For i As Integer = 0 To gridview4.Rows.Count - 1
                chk = CType(gridview4.Rows(i).FindControl("chkSelect"), CheckBox)
                txtstart = CType(gridview4.Rows(i).FindControl("txtstart"), TextBox)
                txtend = CType(gridview4.Rows(i).FindControl("txtend"), Label)
                txtamount = CType(gridview4.Rows(i).FindControl("txtamount"), TextBox)
                lblPK = CType(gridview4.Rows(i).FindControl("lblPK"), Label)
                lblValidate = CType(gridview4.Rows(i).FindControl("lblValidate"), Label)


                Integer.TryParse(txtstart.Text, id_start)
                Integer.TryParse(txtend.Text, id_end)
                Integer.TryParse(txtamount.Text, qty)

                If (id_start + qty) > 0 And id_start > 0 And qty > 0 Then
                    is_dupp = isDupplicateCardID(id_start, id_end, CInt(lblPK.Text))
                    If (is_dupp = True) Then
                        '  Response.Write("dup")
                        lblValidate.Text = "Dupplicate Card ID"
                        txtstart.BackColor = Drawing.Color.Yellow
                    Else
                        'Response.Write("noo")
                        lblValidate.Text = ""
                        txtstart.BackColor = Drawing.Color.White
                    End If
                Else
                    lblValidate.Text = ""
                End If


            Next i


        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("validateDupplicateCard : " & ex.Message)
        Finally
            'bindGridIssueQuota()
        End Try

    End Sub
    Function isDupplicateCardID(id_start As Integer, id_end As Integer, issue_id As Integer) As Boolean
        Dim sql As String
        Dim ds As New DataSet
        Dim result As Boolean = False
        Try
            For i As Integer = id_start To id_end
                sql = "SELECT * FROM srp_m_quarter_issue WHERE " & i & " between card_id_start and card_id_end "
                sql &= " AND quater_issue_id <> " & issue_id
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                If ds.Tables("t1").Rows.Count > 0 Then
                    result = True
                    Exit For
                Else

                End If
            Next i

        Catch ex As Exception
            Response.Write(ex.Message)
            Return False
        End Try

        Return result
    End Function

    Protected Sub txtfind_quarter2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtfind_quarter2.SelectedIndexChanged
        cmdCreateIssueQuota0.Text = "Create Issue from quota in Quarter " & txtfind_quarter2.SelectedItem.Text
        bindGridIssueQuota()
    End Sub

    Protected Sub cmdExcel1_Click(sender As Object, e As System.EventArgs) Handles cmdExcel1.Click
        exportExcel1()
    End Sub

    Sub exportExcel1()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select b.quater_no AS 'Quarter' , b.year_no AS 'Year' , a.mgr_dept_name AS 'Cost Center' , a.mgr_name AS 'Mgr. Name' "
            sql &= " , a.staff_amount AS 'No.Staff' , a.calculate_percent , a.x_month , a.x_jobtype , a.quota_total "
            sql &= " , b.project_name , b.issue_qty AS 'Issue Qty.' , b.card_id_start , b.card_id_end "

            sql &= " from srp_m_calculate_quota a inner join srp_m_quarter_issue b on a.mgr_emp_code = b.mgr_emp_code "
            sql &= " and a.quater_no = b.quater_no and a.year_no = b.year_no "
            sql &= " where 1 = 1 "
            sql &= " and a.mgr_emp_code IN (SELECT Employee_id FROM temp_BHUser) "
            sql &= " AND cast(b.quater_no as varchar) + '/' + cast(b.year_no as varchar) = '" & txtfind_quarter2.SelectedValue & "'"
            sql &= " ORDER BY  b.card_id_start , b.card_id_end  "

            ds = conn.getDataSetForTransaction(sql, "t1")
            ' ExportToSpreadsheet(ds.Tables("t1"), "test")

            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.Charset = "Windows-874"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Me.EnableViewState = False
            Response.AddHeader("content-disposition", "attachment;filename=SRP.xls")
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
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdValidate1_Click(sender As Object, e As EventArgs) Handles cmdValidate1.Click
        validateDupplicateCard()
    End Sub
End Class
