Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction
Imports System.IO

Namespace cfb
    Partial Class cfb_home
        Inherits System.Web.UI.Page
        Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Protected viewtype As String = ""
        Protected flag As String = ""
        Dim priv_list() As String


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If IsNothing(Session("user_fullname")) Then
                Response.Redirect("../login.aspx")
                Response.End()
            End If

            viewtype = Request.QueryString("viewtype")
            flag = Request.QueryString("flag")

            Session("viewtype") = viewtype & ""
            Session("flag") = flag & ""
            'Response.Write(Session("viewtype").ToString)
            priv_list = Session("priv_list")

            If conn.setConnection Then

            Else
                Response.Write("Connection Error")
            End If

            If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
                If viewtype = "dept" Then
                    chk_showall.Visible = True
                    chk_showall.Checked = False
                    div_dept_priority.Visible = True
                Else
                    'chk_showall.Visible = False
                End If

                If viewtype = "tqm" Then
                    chkFlag.Visible = True
                    chkMoveToIR.Visible = True
                    chkChangeToDraft.Visible = True
                End If

                bindDeptUnit()
                bindSpecialty()
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

        Sub bindDeptUnit()
            Dim reader As SqlDataReader
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "SELECT * FROM m_dept_unit "
                sql &= " ORDER BY dept_unit_name"
                reader = conn.getDataReader(sql, "t1")
                'Response.Write(sql)
                txtunit_defendant.DataSource = reader
                txtunit_defendant.DataBind()

                txtunit_defendant.Items.Insert(0, New ListItem("--All Unit defendant --", ""))
                reader.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub bindSpecialty()
            Dim reader As SqlDataReader
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "SELECT specialty FROM m_doctor  WHERE specialty IS NOT NULL GROUP BY specialty"
                'sql &= " ORDER BY dept_name"
                reader = conn.getDataReader(sql, "t1")
                'Response.Write(sql)
                txtspecialty.DataSource = reader
                txtspecialty.DataBind()

                txtspecialty.Items.Insert(0, New ListItem("--All Specialty--", ""))
                reader.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub bindGrid()
            Dim ds As New DataSet
            Dim sql As String
            Try
                sql = "SELECT a.ir_id , b.hn , b.division , b.cfb_no , a.status_id , a.is_cancel , psm_status_id ,psm_status_name ,  c.ir_status_name "
                sql &= " , a.date_submit , a.date_close_ts , a.date_close , datetime_report_ts , a.date_report AS create_date "
                sql &= " ,a.date_submit_ts , b.datetime_complaint_ts , a.is_change_to_draft , a.is_move_to_ir FROM  ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id  "
                sql &= " INNER JOIN ir_status_list c ON a.status_id = c.ir_status_id "

                sql &= " LEFT OUTER JOIN ir_status_list e ON a.status_dept_id = e.ir_status_id "
                sql &= " LEFT OUTER JOIN ir_status_list f ON a.status_psm_id = f.ir_status_id "
                If viewtype = "dept" Then
                    '  sql &= " INNER JOIN (SELECT * FROM cbf_relate_dept xx INNER JOIN cfb_detail_tab yy ON xx.comment_id = yy.comment_id WHERE xx.dept_id = " & Session("dept_id").ToString & ") rr ON a.ir_id = rr.ir_id"

                End If
                sql &= " WHERE (a.report_type = 'cfb' OR is_move_to_ir = 1) AND ISNULL(is_delete,0) = 0 "


                If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                    sql &= " AND a.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "'"
                End If

                If txtstatus.SelectedValue <> "" Then
                    sql &= " AND a.status_id = " & txtstatus.SelectedValue
                End If

                If txtfindhn.Value <> "" Then
                    sql &= " AND b.hn = '" & Trim(txtfindhn.Value) & "'"
                End If

                If txtdept.SelectedValue <> "" Then
                    sql &= " AND b.division = '" & txtdept.SelectedItem.Text & "'"
                End If

                If txtcfbno.Text <> "" Then
                    sql &= " AND b.cfb_no LIKE '%" & txtcfbno.Text & "%' "
                End If

                If txtowner.Text <> "" Then
                    sql &= " AND a.ir_id IN (SELECT ir_id FROM cfb_comment_list WHERE LOWER(tqm_owner) LIKE '%" & txtowner.Text.ToLower & "%') "
                End If

                If txtunit_defendant.SelectedValue <> "" Then
                    sql &= " AND a.ir_id IN (SELECT ir_id FROM ir_cfb_unit_defendant WHERE dept_unit_id = " & txtunit_defendant.SelectedValue & ")"
                End If

                If txtspecialty.SelectedValue <> "" Then
                    sql &= " AND a.ir_id IN (SELECT ir_id FROM ir_doctor_defendant x INNER JOIN m_doctor y ON x.md_code = y.emp_no WHERE y.specialty = '" & txtspecialty.SelectedValue & "')"
                End If

                If txtfinddoctor.Text <> "" Then
                    sql &= " AND a.ir_id IN (SELECT ir_id FROM ir_doctor_defendant WHERE LOWER(doctor_name) LIKE '%" & txtfinddoctor.Text.ToLower & "%' OR md_code LIKE '%" & txtfinddoctor.Text.ToLower & "%' )"
                End If

                If chkFlag.Checked Then
                    sql &= " AND  EXISTS (SELECT * FROM cfb_comment_list WHERE is_followup_case = 1 AND ir_id = a.ir_id) "
                End If

                If chkChangeToDraft.Checked Then
                    sql &= " AND a.is_change_to_draft = 1 "
                End If

                If chkMoveToIR.Checked Then
                    sql &= " AND a.is_move_to_ir = 1 "
                End If

                If viewtype = "" Then
                    sql &= " AND report_emp_code = " & Session("emp_code").ToString
                ElseIf viewtype = "tqm" Then
                    sql &= " AND (a.status_id > 1 OR is_change_to_draft = 1 OR is_move_to_ir = 1) "
                ElseIf viewtype = "dept" Then
                   
                  
                    If flag = "update" Then
                        sql &= " AND (EXISTS(SELECT * FROM ir_dept_inform_update WHERE ir_id = a.ir_id AND dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " ) ) )"
                        'sql &= " AND (EXISTS(SELECT * FROM ir_dept_inform_update WHERE ir_id = a.ir_id AND dept_id = " & Session("dept_id").ToString & ") )"
                    Else
                        Dim citeria As String = ""
                        If chk_showall.Checked = True Then
                            citeria = " AND xx.is_investigate = 0 AND  xx.dept_id = " & Session("dept_id").ToString
                            txtstatus.SelectedValue = "4"
                        Else
                            citeria = ""
                        End If

                        sql &= " AND (EXISTS(SELECT * FROM cbf_relate_dept xx INNER JOIN cfb_comment_list yy ON xx.comment_id = yy.comment_id WHERE yy.ir_id = a.ir_id " & citeria & " AND xx.dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " ) "
                        If txtdept_priority.SelectedIndex > 0 Then
                            If txtdept_priority.SelectedValue = "normal" Then
                                sql &= " AND ISNULL(xx.dept_priority,'') IN ('','normal') "
                            Else
                                sql &= " AND xx.dept_priority = '" & txtdept_priority.SelectedValue & "' "
                            End If

                        End If
                        sql &= ")"
                        sql &= " AND ( a.status_id IN (4,7) OR 4 IN (SELECT status_id FROM ir_status_log WHERE ir_id = a.ir_id) ) "
                        '  sql &= " OR b.dept_id_report = " & Session("dept_id").ToString

                        sql &= " )"
                    End If


                ElseIf viewtype = "ha" Then


                 

                    If flag = "update" Then
                        sql &= " AND (EXISTS(SELECT * FROM ir_dept_inform_update WHERE ir_id = a.ir_id AND dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & ") ) )"
                    Else
                        If Session("is_doctor").ToString = "0" Then
                            sql &= " AND EXISTS(SELECT * FROM cbf_relate_dept xx INNER JOIN cfb_comment_list yy ON xx.comment_id = yy.comment_id WHERE yy.ir_id = a.ir_id AND xx.dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " ) )"
                        Else
                            sql &= " AND EXISTS(SELECT * FROM cbf_relate_dept xx INNER JOIN cfb_comment_list yy ON xx.comment_id = yy.comment_id WHERE yy.ir_id = a.ir_id AND xx.dept_id IN (select y2.dept_id from doctor_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " ) )"
                        End If
                        '  sql &= " AND ( a.status_id IN (4,7) OR 4 IN (SELECT status_id FROM ir_status_log WHERE ir_id = a.ir_id) ) "

                        'sql &= " OR b.dept_id_report = " & Session("dept_id").ToString
                        sql &= " OR b.dept_id_report IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " )"
                        sql &= " AND a.status_id > 2 "
                    End If

                ElseIf viewtype = "psm" Then
                        sql &= " AND (a.status_id IN (5,6,8) OR 5 IN (SELECT status_id FROM ir_status_log WHERE ir_id = a.ir_id) ) "
                ElseIf viewtype = "ha" Then
                        '  sql &= " AND (a.status_id IN (5,6,8) OR 5 IN (SELECT status_id FROM ir_status_log WHERE ir_id = a.ir_id) ) "
                Else
                        Throw New Exception("Invalid parameter")
                End If


                'sql &= " AND a.report_emp_code = " & Session("emp_code")
                If viewtype = "" Then
                    sql &= " ORDER BY  b.cfb_no DESC"
                Else
                    sql &= " ORDER BY b.cfb_no DESC , a.date_submit DESC"
                End If

                ' Response.Write(sql)
                'Return
                ds = conn.getDataSet(sql, "table1")
                If (conn.errMessage <> "") Then
                    Throw New Exception(conn.errMessage)
                End If
                lblNum.Text = ds.Tables(0).Rows.Count
              
                GridView1.DataSource = ds
                GridView1.DataBind()


            Catch ex As Exception
                Response.Write(ex.Message & sql)
            End Try

        End Sub

        Sub bindDept()
            Dim reader As SqlDataReader
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "SELECT * FROM user_dept WHERE 1 = 1 "
                If viewtype = "dept" Then
                    sql &= " AND dept_id IN (SELECT costcenter_id FROM user_access_costcenter WHERE emp_code = " & Session("emp_code").ToString & ")"
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
                sql = "SELECT * FROM ir_status_list"
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
                    sql = "UPDATE ir_trans_list SET is_delete = 1 WHERE ir_id = " & index
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
                    sql = "UPDATE ir_trans_list SET is_cancel = 1 WHERE ir_id = " & index
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
                Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
                Dim lblStatusID As Label = CType(e.Row.FindControl("lblStatusID"), Label)
                Dim lblIsCancel As Label = CType(e.Row.FindControl("lblIsCancel"), Label)
                ' Dim lblStatusDept As Label = CType(e.Row.FindControl("lblStatusDept"), Label)
                'Dim lblStatusPSM As Label = CType(e.Row.FindControl("lblStatusPSM"), Label)
                Dim lblTAT As Label = CType(e.Row.FindControl("lblTAT"), Label)
                Dim lblDateTS As Label = CType(e.Row.FindControl("lblDateTS"), Label)

                Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
                Dim lblComment As Label = CType(e.Row.FindControl("lblComment"), Label)
                Dim lblCloseDate As Label = CType(e.Row.FindControl("lblCloseDate"), Label)
                Dim lblCloseDateTS As Label = CType(e.Row.FindControl("lblCloseDateTS"), Label)
                Dim lblOwner As Label = CType(e.Row.FindControl("lblOwner"), Label)

                Dim LinkNo As HyperLink = CType(e.Row.FindControl("LinkNo"), HyperLink)
                Dim LinkDelete As ImageButton = CType(e.Row.FindControl("LinkDelete"), ImageButton)
                Dim LinkCancel As ImageButton = CType(e.Row.FindControl("LinkCancel"), ImageButton)
                Dim LinkHN As Label = CType(e.Row.FindControl("LinkHN"), Label)
                Dim Image1 As Image = CType(e.Row.FindControl("Image1"), Image)
                Dim Image2 As Image = CType(e.Row.FindControl("Image2"), Image)
                Dim Image3 As Image = CType(e.Row.FindControl("Image3"), Image)
                Dim imgReply As Image = CType(e.Row.FindControl("imgReply"), Image)
                Dim imgPriority As Image = CType(e.Row.FindControl("imgPriority"), Image)
                Dim imgFlag As Image = CType(e.Row.FindControl("imgFlag"), Image)

                Dim lblNoComment As Label = CType(e.Row.FindControl("lblNoComment"), Label)
                Dim lblStatusPSM As Label = CType(e.Row.FindControl("lblStatusPSM"), Label)
                Dim lblStatusPSM_Name As Label = CType(e.Row.FindControl("lblStatusPSM_Name"), Label)

                Dim lblChangeDraft As Label = CType(e.Row.FindControl("lblChangeDraft"), Label)
                Dim lblMoveIR As Label = CType(e.Row.FindControl("lblMoveIR"), Label)

                Dim sql As String
                Dim ds As New DataSet
                Dim ds2 As New DataSet
                Dim ds3 As New DataSet
                Dim dsReply As New DataSet
                Try

                    If LinkNo.Text = "0" Then
                        LinkNo.Text = "************"
                    Else
                        LinkNo.Text = "CFB" & LinkNo.Text
                    End If
                    'Response.Write("x" & lblStatusPSM.Text)
                    If lblStatusPSM.Text <> "0" Then
                        lblStatusPSM_Name.Text = "<br/>  " & lblStatusPSM_Name.Text & ""
                        lblStatusPSM_Name.Visible = True
                    Else
                        lblStatusPSM_Name.Visible = False
                    End If

                    sql = "SELECT * FROM ir_information_update WHERE ir_id = " & lblPK.Text & " ORDER BY inform_id DESC"
                    dsReply = conn.getDataSet(sql, "t1")
                    If dsReply.Tables("t1").Rows.Count > 0 Then
                        imgReply.Visible = True
                        imgReply.ToolTip = dsReply.Tables("t1").Rows(0)("inform_detail").ToString & vbCrLf & "By " & dsReply.Tables("t1").Rows(0)("inform_by").ToString
                    End If

                    If viewtype = "dept" Then

                        imgPriority.Visible = True

                        sql = "SELECT * FROM ir_status_log a INNER JOIN ir_status_list b ON a.status_id = b.ir_status_id "
                        sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id "
                        sql &= " INNER JOIN cbf_relate_dept d ON c.comment_id = d.comment_id "
                        sql &= " WHERE a.status_id IN (4,7) AND a.ir_id = " & lblPK.Text
                        sql &= " AND d.dept_id = " & Session("dept_id").ToString
                        'sql &= " AND d.dept_id IN (SELECT costcenter_id FROM user_access_costcenter WHERE emp_code = " & Session("emp_code").ToString & ")"
                        sql &= " ORDER BY a.log_time_ts DESC"
                        ' Response.Write(sql)
                        ds = conn.getDataSet(sql, "t1")

                        If ds.Tables("t1").Rows.Count > 0 Then

                            e.Row.BackColor = Drawing.ColorTranslator.FromHtml("#CDEBD9")
                            '  Response.Write(ds.Tables("t1").Rows(0)("dept_priority").ToString)
                            If ds.Tables("t1").Rows(0)("dept_priority").ToString = "normal" Or ds.Tables("t1").Rows(0)("dept_priority").ToString = "" Then
                                imgPriority.ToolTip = "Normal Priority"

                            ElseIf ds.Tables("t1").Rows(0)("dept_priority").ToString = "low" Then
                                imgPriority.ImageUrl = "../images/152.png"
                                imgPriority.ToolTip = "Low Priority"
                            ElseIf ds.Tables("t1").Rows(0)("dept_priority").ToString = "high" Then
                                imgPriority.ImageUrl = "../images/151.png"
                                imgPriority.ToolTip = "High Priority"
                            End If

                            If ds.Tables("t1").Rows(0)("is_investigate").ToString = "1" Then
                                e.Row.Font.Bold = False
                            Else
                                e.Row.Font.Bold = True
                                lblStatus.Text = "Need Dept Mgr/ Director Investigation"
                            End If

                            If lblStatusID.Text = "9" Then
                                lblStatus.Text = "Closed"
                            Else
                                '   lblStatus.Text = ds.Tables(0).Rows(0)("ir_status_name").ToString
                            End If
                        Else

                        End If


                    End If


                    sql = "SELECT * FROM cfb_comment_list WHERE ir_id = " & lblPK.Text
                    ds2 = conn.getDataSet(sql, "t1")
                    If ds2.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                            If i > 0 Then
                                lblComment.Text &= "<br/>"
                            End If
                            lblComment.Text &= " - " & ds2.Tables(0).Rows(i)("comment_type_name").ToString
                            lblOwner.Text = ds2.Tables(0).Rows(i)("tqm_owner").ToString

                            If (ds2.Tables(0).Rows(i)("is_followup_case").ToString = "1") Then
                                imgFlag.Visible = True
                            End If

                        Next i
                    End If

                    lblNoComment.Text = ds2.Tables(0).Rows.Count

                    If lblStatusID.Text = "9" Then
                        lblTAT.Text = MinuteDiff(lblDateTS.Text, lblCloseDateTS.Text)
                        lblStatus.ForeColor = Drawing.Color.Red
                    Else
                        lblTAT.Text = MinuteDiff(lblDateTS.Text, Date.Now.Ticks.ToString)
                    End If

                    If lblStatusID.Text = "2" Then
                        If viewtype = "tqm" Then
                            If findArrayValue(priv_list, "9") = True Then
                                e.Row.Font.Bold = True
                            End If
                        End If

                    End If

                 

                    If lblStatusID.Text = "9" Then
                        lblCloseDate.Visible = True
                    Else

                        If CLng(lblDateTS.Text) > 0 Then
                            lblTAT.Text = MinuteDiff(lblDateTS.Text, Date.Now.Ticks.ToString)
                            lblCloseDate.Visible = False
                            ' Response.Write(CLng(lblDateTS.Text) & "<br/>")
                        Else
                            lblTAT.Text = ""
                        End If
                    End If

                 

                    If getTAT() <= 1440 Then
                        lblTAT.ForeColor = Drawing.Color.Green
                    ElseIf getTAT() > 1440 And getTAT() <= (1440 * 3) Then
                        lblTAT.ForeColor = Drawing.Color.YellowGreen
                    ElseIf getTAT() > (1440 * 3) Then
                        lblTAT.ForeColor = Drawing.Color.Red
                    Else

                    End If

                    Dim sqlB As New StringBuilder

                    sqlB.Append("SELECT a.hn ,a.datetime_report ,b.comment_type_name , c.dept_name FROM cfb_detail_tab a LEFT OUTER JOIN cfb_comment_list b ON a.ir_id = b.ir_id ")
                    sqlB.Append(" INNER JOIN ir_trans_list d ON a.ir_id = d.ir_id ")
                    sqlB.Append(" LEFT OUTER JOIN cbf_relate_dept c ON b.comment_id = c.comment_id WHERE 1=1 AND ISNULL(d.is_delete,0) = 0 AND ISNULL(d.is_cancel,0) = 0 AND d.status_id > 1 ")
                    '  sqlB.Append(" WHERE a.hn = '" & txthn.Value & "' AND a.ir_id <> " & cfbId)
                    If LinkHN.Text = "" Then
                        sqlB.Append(" AND 1 > 2 ")
                    Else
                        sqlB.Append(" AND a.hn = '" & LinkHN.Text & "'  AND a.ir_id <> " & lblPK.Text)
                    End If
                    sqlB.Append(" GROUP BY a.hn ,a.datetime_report ,b.comment_type_name,a.datetime_report_ts, c.dept_name")
                    sqlB.Append(" ORDER BY a.datetime_report_ts DESC")
                    ' Response.Write(sqlB.ToString & "<br/>")
                    sql = sqlB.ToString
                    ds = conn.getDataSet(sql, "t2")
                    If ds.Tables("t2").Rows.Count > 0 Then
                        '  Response.Write(sql & "<br/>")
                        Image1.Visible = True
                    Else
                        Image1.Visible = False
                    End If


                    sql = "SELECT  b.hn ,b.datetime_report ,c.form_name_en  , b.datetime_report_ts , b.ir_no FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id "
                    sql &= " INNER JOIN ir_form_master c ON c.form_id = a.form_id"

                    If LinkHN.Text = "" Then
                        sql &= "  AND 1 > 2"
                    Else
                        sql &= " AND b.hn = '" & LinkHN.Text & "'  AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 1 AND  a.ir_id <> " & lblPK.Text
                    End If
                    sql &= " GROUP BY b.hn ,b.datetime_report ,c.form_name_en ,   b.datetime_report_ts , b.ir_no"
                    sql &= " ORDER BY b.datetime_report_ts DESC"


                    '  Response.Write(sql & "<br/>")
                    ds = conn.getDataSet(sql, "t3")
                    If ds.Tables("t3").Rows.Count > 0 Then
                        ' Response.Write(sql & "<br/>")
                        Image2.Visible = True
                    Else
                        Image2.Visible = False
                    End If

                    sql = "SELECT * FROM cfb_attachment WHERE  ir_id = " & lblPK.Text
                    ds3 = conn.getDataSet(sql, "t0")
                    If ds3.Tables("t0").Rows.Count > 0 Then
                        ' Response.Write(sql & "<br/>")
                        Image3.Visible = True
                    Else
                        Image3.Visible = False
                    End If


                    If lblStatusID.Text = "1" And viewtype = "" Then
                        LinkDelete.Visible = True
                    Else
                        LinkDelete.Visible = False
                    End If

                    If viewtype = "tqm" Then
                        LinkCancel.Visible = True

                    Else
                        LinkCancel.Visible = False
                    End If

                    If lblIsCancel.Text = "1" Then
                        lblStatus.Text = "Cancelled"
                        e.Row.Font.Bold = False
                        ' lblStatus.ForeColor = Drawing.Color.Red
                    End If

                    If lblChangeDraft.Text = "1" Then
                        lblStatus.Text = "Waiting for Submit"
                        e.Row.Font.Bold = False
                        lblStatus.Font.Bold = True
                        lblStatus.ForeColor = Drawing.Color.Red
                    End If

                    If lblMoveIR.Text = "1" Then
                        lblStatus.Text = "Move to IR report"
                        e.Row.Font.Bold = False
                        lblStatus.Font.Bold = True
                        lblStatus.ForeColor = Drawing.Color.Red
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    ds.Dispose()
                    ds2.Dispose()
                    ds3.Dispose()
                    dsReply.Dispose()
                End Try

            End If
        End Sub

        Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        End Sub

        Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
            bindGrid()
        End Sub

        Protected Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
            txtdate1.Text = ""
            txtdate2.Text = ""
            txtfindhn.Value = ""
            txtdept.SelectedIndex = 0
            txtstatus.SelectedIndex = 0

            bindGrid()
        End Sub

        Protected Sub chk_showall_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_showall.CheckedChanged
            If chk_showall.Checked = False Then
                txtstatus.SelectedIndex = 0
            End If
        End Sub

        Public Shared Sub Export(ByVal fileName As String, ByVal gv As GridView)


            HttpContext.Current.Response.Clear()

            'Export will take two parameter first one the name of Excel File, and second one for gridview to be exported

            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))

            HttpContext.Current.Response.ContentType = "application/octet-stream"

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

        Protected Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
            Export("cfb.xls", GridView1)
        End Sub

        Protected Sub txtdept_priority_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtdept_priority.SelectedIndexChanged
            bindGrid()
        End Sub
    End Class
End Namespace
