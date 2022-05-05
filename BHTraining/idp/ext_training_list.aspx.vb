Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports ShareFunction

Partial Class idp_ext_training_list
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected req As String = ""
    Protected flag As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Request.QueryString("viewtype")
        req = Request.QueryString("req")
        If req = "" Then
            req = "ext"
        End If
        flag = Request.QueryString("flag")
        Session("viewtype") = viewtype & ""
        Session("req") = req & ""

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

     

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            ' Response.Write(viewtype)

            txtdatetype.SelectedValue = "1"

            If req = "ext" Then
                lblHeader.text = "External"
            ElseIf req = "int" Then
                lblHeader.text = "Internal"
            End If

            If flag = "after" Then
                lblAction.Text = " [My Action After Training]"
                cmdNew.Visible = False
            End If

            lblApproveName.Text = Session("user_fullname").ToString

            If viewtype = "hr" Then
                div_hr.Visible = True
                div_dept.Visible = False
                cmdNew.Visible = False
            Else
                ' Response.Write("Xxxxxx")
                div_hr.Visible = False
                div_dept.Visible = False
            End If

            If viewtype = "dept" Then
                div_dept.Visible = True
                cmdNew.Visible = False
            End If

            If viewtype = "budget" Or viewtype = "expense" Then
                cmdNew.Visible = False
            End If

            If viewtype = "" Then
                GridView1.Columns(0).Visible = False
            End If

            bindDept()
            bindStatus()
            bindHRStatus()
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

    Sub bindDept()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM user_dept WHERE 1 = 1"
            If viewtype = "dept" Or viewtype = "" Then
                sql &= " AND dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"

            End If
            sql &= " ORDER BY dept_name_en"
            reader = conn.getDataReader(sql, "t1")
            ' Response.Write(sql)
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
            sql = "SELECT * FROM idp_status_list"
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
            sql = "SELECT * FROM idp_status_list"
            'sql &= " ORDER BY dept_name"
            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txthrstatus.DataSource = reader
            txthrstatus.DataBind()

            txthrstatus.Items.Insert(0, New ListItem("--All Status--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGrid()
        Dim ds As New DataSet
        Dim sql As String
        Try
            sql = "SELECT *  , case when ISNULL(goal_skill_level,'') <> '' then 'Complete' else 'Waiting' end AS action_complete "
            sql &= " , CASE WHEN request_type = 'ext' THEN ext_title ELSE internal_title END AS title "
            sql &= " FROM idp_external_req a INNER JOIN idp_trans_list b "
            sql &= " ON a.idp_id = b.idp_id "
            sql &= " INNER JOIN  idp_status_list c "
            sql &= " ON b.status_id = c.idp_status_id "

            sql &= " INNER JOIN user_profile d ON b.report_emp_code = d.emp_code "

            sql &= " WHERE ISNULL(b.is_delete,0) = 0 AND ISNULL(b.is_cancel,0) = 0 "

            If flag = "after" Then
                sql &= " AND  a.date_end_ts <= " & Date.Now.Ticks
            End If

            If viewtype <> "budget" And viewtype <> "expense" Then
                ' sql &= " AND a.request_type = '" & req & "' "
            End If

            If req <> "" Then
                If viewtype <> "budget" Then
                    sql &= " AND a.request_type = '" & req & "' "
                End If

            End If

            If txttitle.Text <> "" Then
                If req = "ext" Then
                    sql &= " AND a.ext_title LIKE '%" & txttitle.Text & "%' "
                Else
                    sql &= " AND a.internal_title LIKE '%" & txttitle.Text & "%' "

                End If

            End If

            If txtrequestno.Text <> "" Then
                sql &= " AND b.idp_no LIKE '%" & txtrequestno.Text & "%' "
            End If

            If txtemp_code.Value <> "" Then
                sql &= " AND b.report_emp_code = " & txtemp_code.Value
            End If

            If txtempname.Value <> "" Then
                sql &= " AND b.report_by LIKE '%" & txtempname.Value & "%' "
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND b.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtstatus.SelectedValue <> "" Then
                sql &= " AND b.status_id = " & txtstatus.SelectedValue
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                If txtdatetype.SelectedValue = "1" Then
                    sql &= " AND b.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                Else
                    sql &= " AND a.date_start BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
                End If

            End If

            If viewtype = "" Then
                sql &= " AND b.report_emp_code = " & Session("emp_code").ToString
            End If

            If viewtype = "dept" Then
                sql &= " AND  d.dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
                'sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
                sql &= " AND b.status_id IN (2 , 3,5)"
            End If

            If viewtype = "educator" Then
                sql &= " AND b.report_emp_code = " & Session("emp_code").ToString
                sql &= " AND b.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ") "
                ' sql &= " AND b.status_id IN (2 , 3,5)"
            End If

            If viewtype = "hr" Then
                sql &= " AND b.status_id >= 2"
            End If

            If viewtype = "post" Then
                sql &= " AND b.status_id IN (5 , 6)"
            End If

            If viewtype = "budget" Then
                sql &= " AND b.status_id IN (3, 5 , 6 , 7)" ' TRD Process
            End If

            If viewtype = "expense" Then
                sql &= " AND b.status_id IN (3 , 5 , 6 , 7) AND a.is_budget_approve = 1 " ' TRD Process
            End If

            sql &= " ORDER BY a.date_start_ts DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            'Return
            lblNum.Text = ds.Tables(0).Rows.Count
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try

    End Sub

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("ext_training_detail.aspx?mode=add")
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
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

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        'If e.Row.RowType = DataControlRowType.Header Then
        '    Dim HeaderGrid As GridView = DirectCast(sender, GridView)
        '    Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

        '    Dim HeaderCell As New TableCell()

        '    HeaderCell = New TableCell()
        '    HeaderCell.ForeColor = Drawing.Color.White
        '    HeaderCell.Text = ""
        '    HeaderGridRow.Cells.Add(HeaderCell)

        '    HeaderCell = New TableCell()
        '    HeaderGridRow.Cells.Add(HeaderCell)

        '    HeaderCell = New TableCell()
        '    HeaderCell.ForeColor = Drawing.Color.White

        '    HeaderCell.Text = "Training Request List"
        '    HeaderCell.ColumnSpan = 3
        '    HeaderCell.HorizontalAlign = HorizontalAlign.Center
        '    HeaderGridRow.Cells.Add(HeaderCell)

        '    HeaderCell = New TableCell()
        '    HeaderCell.ForeColor = Drawing.Color.White
        '    HeaderCell.Text = "Approve Status"
        '    HeaderCell.ColumnSpan = 5
        '    HeaderGridRow.Cells.Add(HeaderCell)

        '    HeaderCell = New TableCell()
        '    HeaderGridRow.Cells.Add(HeaderCell)

        '    HeaderCell = New TableCell()
        '    HeaderGridRow.Cells.Add(HeaderCell)

        '    GridView1.Controls(0).Controls.AddAt(0, HeaderGridRow)
        'End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblTitleExt As Label = CType(e.Row.FindControl("lblTitleExt"), Label)
            Dim lblTitleInt As Label = CType(e.Row.FindControl("lblTitleInt"), Label)
            Dim lblLink As Label = CType(e.Row.FindControl("lblLink"), Label)
            Dim lblPk As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim lblStatusId As Label = CType(e.Row.FindControl("lblStatusId"), Label)
            Dim lblAuthen As Label = CType(e.Row.FindControl("lblAuthen"), Label)
            Dim lblEmpCode As Label = CType(e.Row.FindControl("lblEmpCode"), Label)
            Dim lblImage1 As Label = CType(e.Row.FindControl("lblImage1"), Label)
            Dim lblImage2 As Label = CType(e.Row.FindControl("lblImage2"), Label)
            Dim lblImage3 As Label = CType(e.Row.FindControl("lblImage3"), Label)
            Dim lblImage4 As Label = CType(e.Row.FindControl("lblImage4"), Label)
            Dim lblImage5 As Label = CType(e.Row.FindControl("lblImage5"), Label)
            Dim lblImage6 As Label = CType(e.Row.FindControl("lblImage6"), Label)
            Dim imgReply As Image = CType(e.Row.FindControl("imgReply"), Image)
            'Dim lblDirectorComment As Label = CType(e.Row.FindControl("lblDirectorComment"), Label)

            Dim LinkDelete As ImageButton = CType(e.Row.FindControl("LinkDelete"), ImageButton)
            Dim LinkCancel As ImageButton = CType(e.Row.FindControl("LinkCancel"), ImageButton)
            Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
            Dim lblBudgetStatus As Label = CType(e.Row.FindControl("lblBudgetStatus"), Label)
            Dim lblReplyNote As Label = CType(e.Row.FindControl("lblReplyNote"), Label)
            Dim lblBudgetUpdateBy As Label = CType(e.Row.FindControl("lblBudgetUpdateBy"), Label)
            Dim lblBudgetRequest As Label = CType(e.Row.FindControl("lblBudgetRequest"), Label)

            Dim lblTrainDate As Label = CType(e.Row.FindControl("lblTrainDate"), Label)
            Dim lblApplyDate As Label = CType(e.Row.FindControl("lblApplyDate"), Label)

            Dim lblTrainDateTS As Label = CType(e.Row.FindControl("lblTrainDateTS"), Label)
            Dim lblApplyDateTS As Label = CType(e.Row.FindControl("lblApplyDateTS"), Label)
            Dim d1 As Date
            Dim d2 As Date
            Dim cur_date As New Date
            cur_date = Date.Now

            Dim user_max_authen As String = "0"
            Dim sql As String
            Dim ds As New DataSet
            Dim dsReply As New DataSet

            Try

                If req = "ext" Then
                    lblTitleExt.Visible = True
                    lblTitleInt.Visible = False
                Else
                    lblTitleExt.Visible = False
                    lblTitleInt.Visible = True
                End If
                If lblTrainDateTS.Text <> "" And lblTrainDateTS.Text <> "0" Then
                    d1 = New Date(CLng(lblTrainDateTS.Text))

                    If d1 >= cur_date Then
                        '  Response.Write(DateDiff(DateInterval.Day, d1, cur_date))
                        If DateDiff(DateInterval.Day, cur_date, d1) <= 10 Then
                            lblTrainDate.ForeColor = Drawing.Color.Red
                        End If
                    End If
                End If

                If lblApplyDateTS.Text <> "" And lblApplyDateTS.Text <> "0" Then
                    d2 = New Date(CLng(lblApplyDateTS.Text))

                    If d2 >= cur_date Then
                        ' Response.Write(DateDiff(DateInterval.Day, d2, cur_date))
                        If DateDiff(DateInterval.Day, cur_date, d2) <= 10 Then
                            lblApplyDate.ForeColor = Drawing.Color.Red
                        End If
                    End If
                End If

                If lblReplyNote.Text <> "" Then
                    imgReply.Visible = True
                End If

                If viewtype = "hr" And lblBudgetRequest.Text <> "2" Then ' ถ้ามีการเบิกค่าใช้จ่าย
                    e.Row.Cells(10).BackColor = Drawing.Color.Yellow
                End If

                sql = "SELECT * FROM idp_information_update WHERE idp_id = " & lblPk.Text & " ORDER BY inform_id DESC"
                dsReply = conn.getDataSet(sql, "t1")
                If dsReply.Tables("t1").Rows.Count > 0 Then
                    imgReply.Visible = True
                    imgReply.ToolTip = dsReply.Tables("t1").Rows(0)("inform_detail").ToString & vbCrLf & "By " & dsReply.Tables("t1").Rows(0)("inform_by").ToString
                End If


                If lblStatusId.Text = "1" And viewtype = "" Then
                    LinkDelete.Visible = True
                Else
                    LinkDelete.Visible = False
                End If

                If viewtype = "hr" Then
                    LinkCancel.Visible = True
                Else
                    LinkCancel.Visible = False
                End If


                If lblLink.Text = "" Then
                    lblLink.Text = "********"
                End If

                If lblStatusId.Text = "2" Then
                    e.Row.Font.Bold = True
                End If

                If lblBudgetStatus.Text = "" Then ' Wait
                    lblImage6.Text = "<img src='../images/history.png' id='img1' />"
                ElseIf lblBudgetStatus.Text = "0" Then ' Reject
                    lblImage6.Text = "<img src='../images/button_cancel.png' id='img1' alt='" & lblBudgetUpdateBy.Text & "' />"
                ElseIf lblBudgetStatus.Text = "1" Then ' Approve
                    lblImage6.Text = "<img src='../images/button_ok.png' id='img1' alt='" & lblBudgetUpdateBy.Text & "' />"
                End If

                lblAuthen.Text = "Employee"

                sql = "SELECT * FROM user_role a INNER JOIN m_role b ON a.role_id = b.role_id WHERE a.role_id IN (13,14,15,16,17) AND a.emp_code = " & lblEmpCode.Text & "  ORDER BY a.role_id DESC"
                ds = conn.getDataSet(sql, "t0")
                If ds.Tables("t0").Rows.Count > 0 Then
                    lblAuthen.Text = ds.Tables("t0").Rows(0)("role_name").ToString
                    user_max_authen = ds.Tables("t0").Rows(0)("role_id").ToString
                Else

                End If

                If viewtype = "dept" Then
                    If CInt(getMyIPDLevel()) > CInt(user_max_authen) Or CInt(getMyIPDLevel()) = 17 Then ' C-level
                        If lblEmpCode.Text <> Session("emp_code").ToString Then

                            If CInt(user_max_authen) = 17 And CInt(getMyIPDLevel()) = 23 Then  ' For IDP 4s
                                chkSelect.Visible = False
                            Else
                                chkSelect.Visible = True
                            End If

                        Else
                            chkSelect.Visible = False
                        End If

                      
                        ' Response.Write(getMyIPDLevel())
                        ' Response.Write(user_max_authen)
                    Else
                        chkSelect.Visible = False
                    End If
                End If
                ' Response.Write(user_max_authen)
                If 13 > CInt(user_max_authen) Then
                    ' ========== Sup
                    sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                    sql &= " AND review_by_role_id =  17 "
                    '  Response.Write(sql)
                    ds = conn.getDataSet(sql, "t1")
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
                    Else
                        lblImage1.Text &= "-"
                    End If
                Else
                    lblImage1.Text &= "-"
                End If

                If 14 > CInt(user_max_authen) Then
                    ' ========== Manager
                    sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                    sql &= " AND review_by_role_id =  14"
                    '  Response.Write(sql)
                    ds = conn.getDataSet(sql, "t2")
                    For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1
                        '  Response.Write(ds.Tables("t2").Rows(i)("comment_status_id").ToString)
                        If ds.Tables("t2").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                            lblImage2.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & ds.Tables("t2").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t2").Rows(i)("detail").ToString & "' />"
                        ElseIf ds.Tables("t2").Rows(i)("comment_status_id").ToString = "2" Then
                            lblImage2.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & ds.Tables("t2").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t2").Rows(i)("detail").ToString & "' />"
                        ElseIf ds.Tables("t2").Rows(i)("comment_status_id").ToString = "3" Then
                            lblImage2.Text &= "<img src='../images/information.gif' id='img1' alt='Wait for approve'  title='" & ds.Tables("t2").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t2").Rows(i)("detail").ToString & "' />"
                        Else
                            lblImage2.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                        End If

                    Next i

                    If ds.Tables("t2").Rows.Count = 0 Then
                        If lblStatusId.Text = "1" Then
                            lblImage2.Text &= "-"
                        Else
                            lblImage2.Text &= "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                        End If

                    Else
                        ' lblImage2.Text = "<img src='../images/history.png' id='img1' alt='Wait for approve' title='Wait for approve' />"
                    End If
                Else
                    lblImage2.Text &= "-"
                End If


                If 15 > CInt(user_max_authen) Then
                    ' ========== 3rd
                    sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                    sql &= " AND review_by_role_id =  15"
                    ' Response.Write(sql)
                    ds = conn.getDataSet(sql, "t3")
                    For i As Integer = 0 To ds.Tables("t3").Rows.Count - 1
                        If ds.Tables("t3").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                            lblImage3.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & ds.Tables("t3").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t3").Rows(i)("detail").ToString & "' />"
                        ElseIf ds.Tables("t3").Rows(i)("comment_status_id").ToString = "2" Then
                            lblImage3.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & ds.Tables("t3").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t3").Rows(i)("detail").ToString & "' />"
                        ElseIf ds.Tables("t3").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                            lblImage3.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='" & ds.Tables("t3").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t3").Rows(i)("detail").ToString & "' />"
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


                If 16 >= CInt(user_max_authen) Then
                    ' ========== 4th
                    sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                    sql &= " AND review_by_role_id IN ( 16,23)"
                    'Response.Write(sql)
                    ' Response.Write(1)
                    ds = conn.getDataSet(sql, "t4")
                    'Response.Write(2)
                    For i As Integer = 0 To ds.Tables("t4").Rows.Count - 1

                        If ds.Tables("t4").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                            lblImage4.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & ds.Tables("t4").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t4").Rows(i)("detail").ToString & "' />"
                        ElseIf ds.Tables("t4").Rows(i)("comment_status_id").ToString = "2" Then
                            lblImage4.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & ds.Tables("t4").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t4").Rows(i)("detail").ToString & "' />"
                        ElseIf ds.Tables("t4").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                            lblImage4.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='" & ds.Tables("t4").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t4").Rows(i)("detail").ToString & "' />"
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


             


                If 17 >= CInt(user_max_authen) Then
                    ' ========== 5th
                    sql = "SELECT * FROM idp_manager_comment WHERE idp_id = " & lblPk.Text
                    sql &= " AND review_by_role_id =  17"
                    ' Response.Write(sql & "<br/>")
                    ' Response.Write(1)
                    ds = conn.getDataSet(sql, "t5")
                    'Response.Write(2)
                    For i As Integer = 0 To ds.Tables("t5").Rows.Count - 1

                        If ds.Tables("t5").Rows(i)("comment_status_id").ToString = "1" Then ' approve
                            lblImage5.Text &= "<img src='../images/button_ok.png' id='img1' alt='approve' title='" & ds.Tables("t5").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t5").Rows(i)("detail").ToString & "' />"
                        ElseIf ds.Tables("t5").Rows(i)("comment_status_id").ToString = "2" Then
                            lblImage5.Text &= "<img src='../images/button_cancel.png' id='img1' alt='Reject' title='" & ds.Tables("t5").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t5").Rows(i)("detail").ToString & "' />"
                        ElseIf ds.Tables("t5").Rows(i)("comment_status_id").ToString = "3" Then ' N/A
                            lblImage5.Text &= "<img src='../images/information.gif' id='img1' alt='N/A' title='" & ds.Tables("t5").Rows(i)("review_by_name").ToString & vbCrLf & ds.Tables("t5").Rows(i)("detail").ToString & "' />"
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

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                dsReply.Dispose()
            End Try

        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

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


                    sql = "UPDATE idp_trans_list SET status_id = '" & txthrstatus.SelectedValue & "' "
                    sql &= "  WHERE idp_id = " & lbl.Text
                    'Response.Write(sql & "<br/>")

                    errorMsg = conn.executeSQL(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                        Exit For
                    End If

                    updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
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

    Protected Sub cmdDeptStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeptStatus.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

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

                    pk = getPK("comment_id", "idp_manager_comment", conn)
                    sql = "INSERT INTO idp_manager_comment (comment_id , idp_id , comment_status_id , comment_status_name , subject_id , subject , detail "
                    sql &= ",review_by_jobtitle , review_by_jobtype , review_by_name , review_by_empcode , review_by_dept_name , review_by_dept_id "
                    sql &= ",create_date , create_date_ts , review_by_role_id"
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

    Function getMyIPDLevel() As String ' Level ของผู้ login
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = "0"
        Try
            sql = "SELECT * FROM user_role a INNER JOIN m_role b ON a.role_id = b.role_id WHERE a.role_id IN (12,13,14,15,16,17,23) AND a.emp_code = " & Session("emp_code").ToString
            sql &= " ORDER BY a.role_id DESC"
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                result = ds.Tables("t1").Rows(0)("role_id").ToString
                lblYourLevel.Text = ds.Tables("t1").Rows(0)("role_name").ToString
            Else
                lblYourLevel.Text = "General Employee"
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
    
    Protected Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        txtdate1.Text = ""
        txtdate2.Text = ""
        txtdept.SelectedIndex = 0
        txtstatus.SelectedIndex = 0
        txtemp_code.Value = ""
        txtempname.Value = ""
        txthrstatus.SelectedIndex = 0
        txtdeptstatus.SelectedIndex = 0

        bindGrid()
    End Sub
End Class
