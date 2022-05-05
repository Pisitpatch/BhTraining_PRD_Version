Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Namespace incident

    Partial Class incident_home
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

            If viewtype = "tqm" Then
                chkFlag.Visible = True
                chkChangeToDraft.Visible = True
                chkMoveToCFB.Visible = True
                If findArrayValue(priv_list, "4") = False Then
                    Response.Write("Invalid privilege!!")
                    Response.End()
                End If
            ElseIf viewtype = "" Then
                'If findArrayValue(priv_list, "2") = False Then
                '    Response.Write("Invalid privilege!!")
                '    Response.End()
                'End If
            ElseIf viewtype = "dept" Then
                If findArrayValue(priv_list, "3") = False Then
                    Response.Write("Invalid privilege !!")
                    Response.End()
                End If
            ElseIf viewtype = "psm" Then
                If findArrayValue(priv_list, "5") = False Then
                    Response.Write("Invalid privilege !!")
                    Response.End()
                End If
            ElseIf viewtype = "ha" Then
                If findArrayValue(priv_list, "6") = False Then
                    Response.Write("Invalid privilege !!")
                    Response.End()
                End If
            Else
                Response.Write("Invalid parameter")
                Response.End()
            End If

            If conn.setConnection Then

            Else
                Response.Write("Connection Error")
            End If

            If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
                bindDivision()
                'If Not IsNothing(Session("ir_citeria_dept")) Then
                '    txtdept.SelectedItem.Text = Session("ir_citeria_dept").ToString
                'End If
                bindStatus()
                bindSpecialty()
                bindDeptUnit()
                'If Not IsNothing(Session("ir_citeria_status")) Then
                '    txtstatus.SelectedValue = Session("ir_citeria_status").ToString
                'End If

                If findArrayValue(priv_list, "2") = False And viewtype = "" Then
                    '    Response.Write("Invalid privilege!!")
                    '    Response.End()
                Else
                    If viewtype = "dept" Then
                        chk_showall.Visible = True
                        chk_relevant.Visible = True
                        chk_reset.Visible = True
                    Else
                        chk_showall.Visible = False
                        chk_relevant.Visible = False
                        chk_reset.Visible = False
                    End If

                    bindGrid()
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

        Private Sub bindGrid()
            Dim ds As New DataSet
            Dim sql As String
            Try
                ' sql = "SELECT a.* , b.* , c.ir_status_name , d.* , g.form_name_en , a.date_report AS create_date FROM  ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id  "
                sql = "SELECT a.ir_id , a.date_report AS create_date,b.datetime_report_ts , a.date_submit ,a.date_submit_ts ,a.date_close_ts ,a.date_close ,a.form_id ,a.status_id , b.ir_no , b.division, b.hn , c.ir_status_name , d.severe_name , g.form_name_en , COUNT(h.file_id) AS file_num , b.flag_serious , a.is_cancel , i.psm_status_id , i.psm_status_name , topic.grand_topic_name , topic_level1.topic_name  "
                sql &= " , topic_level2.subtopic_name , a.is_change_to_draft , a.is_move_to_cfb "
                If viewtype = "dept" Then
                    If flag <> "update" Then
                        sql &= " ,MAX(rr.create_date_ts) "
                    End If
                End If
                sql &= " , tqm.chk_follow_id "
                sql &= " FROM  ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id "
                sql &= " INNER JOIN ir_status_list c ON a.status_id = c.ir_status_id "

                sql &= " LEFT OUTER JOIN ir_status_list e ON a.status_dept_id = e.ir_status_id "
                sql &= " LEFT OUTER JOIN ir_status_list f ON a.status_psm_id = f.ir_status_id "
                sql &= " LEFT OUTER JOIN ir_tqm_tab tqm ON a.ir_id = tqm.ir_id"
                sql &= " LEFT OUTER JOIN ir_topic_grand topic ON tqm.grand_topic = topic.grand_topic_id " 'Grand
                sql &= " LEFT OUTER JOIN ir_topic topic_level1 ON tqm.topic = topic_level1.ir_topic_id " ' Topic
                sql &= " LEFT OUTER JOIN ir_topic_sub topic_level2 ON tqm.subtopic1 = topic_level2.ir_subtopic_id " ' Sub1
                sql &= " LEFT OUTER JOIN  ir_m_severity d ON tqm.severe_level_id = d.severe_id "
                sql &= " INNER JOIN ir_form_master g ON a.form_id = g.form_id "
                sql &= " LEFT OUTER JOIN  ir_attachment h ON a.ir_id = h.ir_id "
                sql &= " LEFT OUTER JOIN  ir_psm_tab i ON a.ir_id = i.ir_id "
                If viewtype = "dept" Then

                    'sql &= " INNER JOIN (SELECT t2.ir_id as detail_id , t2.dept_id as report_dept_id ,t1.* FROM ir_relate_dept t1 RIGHT OUTER JOIN ir_detail_tab t2 ON t1.ir_id = t2.ir_id  ) rr ON a.ir_id = rr.detail_id and (rr.report_dept_id = " & Session("dept_id").ToString & " or rr.dept_id =  " & Session("dept_id").ToString & ")"
                    If flag <> "update" Then
                        Dim citeria As String = ""
                        If chk_showall.Checked = True Then
                            '  Response.Write("xxxxx<hr/>")
                            citeria = " AND rr.is_investigate = 0 AND  rr.dept_id = " & Session("dept_id").ToString
                            txtstatus.SelectedValue = "4"
                        ElseIf chk_relevant.Checked = True Then
                            '  txtstatus.SelectedValue = ""
                            '   Response.Write("yyyyy<hr/>")
                            citeria = ""
                        Else
                            citeria = ""
                        End If

                        If chk_showall.Checked = True Or chk_reset.Checked = True Then
                            '     Response.Write("111<hr/>")
                            sql &= " INNER JOIN (SELECT t2.ir_id as detail_id , t2.dept_id as report_dept_id ,t1.* FROM (SELECT * FROM ir_relate_dept WHERE ISNULL(is_dept_delete,0) = 0) t1 RIGHT OUTER JOIN ir_detail_tab t2 ON t1.ir_id = t2.ir_id  ) rr ON a.ir_id = rr.detail_id " & citeria & " and (rr.report_dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " )  or rr.dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & "  ))"
                        ElseIf chk_relevant.Checked = True Then
                            '   Response.Write("22<hr/>")
                            sql &= " INNER JOIN (SELECT t2.ir_id as detail_id , t2.dept_id as report_dept_id ,t1.* FROM (SELECT * FROM ir_relate_dept WHERE ISNULL(is_dept_delete,0) = 0) t1 RIGHT OUTER JOIN ir_detail_tab t2 ON t1.ir_id = t2.ir_id  ) rr ON a.ir_id = rr.detail_id " & citeria & "  AND rr.dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & "  )"

                        End If
                         
                    End If
                End If
                If viewtype = "ha" Then
                    If flag <> "update" Then
                        If Session("is_doctor").ToString = "0" Then
                            sql &= " INNER JOIN (SELECT t2.ir_id as detail_id , t2.dept_id as report_dept_id ,t1.* FROM ir_relate_dept t1 RIGHT OUTER JOIN ir_detail_tab t2 ON t1.ir_id = t2.ir_id  ) rr ON a.ir_id = rr.detail_id and (rr.report_dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " )  or rr.dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & "  ))"
                        Else
                            sql &= " INNER JOIN (SELECT t2.ir_id as detail_id , t2.dept_id as report_dept_id ,t1.* FROM ir_relate_dept t1 RIGHT OUTER JOIN ir_detail_tab t2 ON t1.ir_id = t2.ir_id  ) rr ON a.ir_id = rr.detail_id and (rr.report_dept_id IN (select y2.dept_id from doctor_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " )  or rr.dept_id IN (select y2.dept_id from doctor_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & "  ))"
                        End If
                    End If
                End If

                sql &= " WHERE (a.report_type = 'ir' OR is_move_to_cfb = 1) AND ISNULL(a.is_delete,0) = 0 "

                If txtunit_defendant.SelectedValue <> "" Then
                    sql &= " AND a.ir_id IN (SELECT ir_id FROM ir_cfb_unit_defendant WHERE dept_unit_id = " & txtunit_defendant.SelectedValue & ")"
                End If

                If txtfinddoctor.Text <> "" Then
                    sql &= " AND a.ir_id IN (SELECT ir_id FROM ir_doctor_defendant WHERE LOWER(doctor_name) LIKE '%" & txtfinddoctor.Text.ToLower & "%' OR md_code LIKE '%" & txtfinddoctor.Text.ToLower & "%' )"
                End If

                If txtstatus.SelectedValue <> "" Then
                    sql &= " AND a.status_id = " & txtstatus.SelectedValue
                    '    Session("ir_citeria_status") = txtstatus.SelectedValue
                ElseIf Not IsNothing(Session("ir_citeria_status")) Then
                    '   sql &= " AND a.status_id = '" & Session("ir_citeria_status").ToString & "'"
                End If

                If txtfindhn.Value <> "" Then
                    sql &= " AND b.hn = '" & Trim(txtfindhn.Value) & "'"
                End If

                If txtdept.SelectedValue <> "" Then
                    sql &= " AND b.division = '" & txtdept.SelectedItem.Text & "'"
                    '   Session("ir_citeria_dept") = txtdept.SelectedItem.Text
                ElseIf Not IsNothing(Session("ir_citeria_dept")) Then
                    '   sql &= " AND b.division = '" & Session("ir_citeria_dept").ToString & "'"
                End If

                If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                    sql &= " AND a.date_submit BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "'"
                End If

                If txtdept.SelectedValue <> "" Then
                    sql &= " AND b.dept_id = " & txtdept.SelectedValue
                End If

                If txtkeyword.Text <> "" Then
                    sql &= " AND ( (tqm.tqm_case_owner) LIKE '%" & txtkeyword.Text.ToLower & "%' OR LOWER(b.ir_no) LIKE '%" & txtkeyword.Text.ToLower & "%'  "
                    sql &= " OR LOWER(d.severe_name) LIKE '%" & txtkeyword.Text.ToLower & "%' "
                    sql &= " OR LOWER(topic.grand_topic_name) LIKE '%" & txtkeyword.Text.ToLower & "%' "
                    sql &= " OR LOWER(topic_level1.topic_name) LIKE '%" & txtkeyword.Text.ToLower & "%' "
                    sql &= " OR LOWER(topic_level2.subtopic_name) LIKE '%" & txtkeyword.Text.ToLower & "%' "
                    sql &= ")"

                    ' Session("ir_citeria_keyword") = txtkeyword.Text
                End If

                If txtspecialty.SelectedValue <> "" Then
                    sql &= " AND a.ir_id IN (SELECT ir_id FROM ir_doctor_defendant x INNER JOIN m_doctor y ON x.md_code = y.emp_no WHERE y.specialty = '" & txtspecialty.SelectedValue & "')"
                End If

                If chkFlag.Checked Then
                    sql &= " AND tqm.chk_follow_id = 1 "
                End If

                If chkChangeToDraft.Checked Then
                    sql &= " AND a.is_change_to_draft = 1 "
                End If

                If chkMoveToCFB.Checked Then
                    sql &= " AND a.is_move_to_cfb = 1 "
                End If

                If viewtype = "" Then
                    sql &= " AND report_emp_code = " & Session("emp_code").ToString
                ElseIf viewtype = "tqm" Then
                    sql &= " AND (a.status_id > 1 OR is_change_to_draft = 1 ) "
                ElseIf viewtype = "dept" Then
                    'sql &= " AND ( a.status_id IN (4,7) OR 4 IN (SELECT status_id FROM ir_status_log WHERE ir_id = a.ir_id) ) "
                    'sql &= " OR b.dept_id = " & Session("dept_id").ToString
                    If flag = "update" Then
                        ' sql &= " AND (EXISTS(SELECT * FROM ir_dept_inform_update WHERE ir_id = a.ir_id AND dept_id = " & Session("dept_id").ToString & ") )"
                        sql &= " AND (EXISTS(SELECT * FROM ir_dept_inform_update WHERE ir_id = a.ir_id AND dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " ) ) )"
                    End If

                ElseIf viewtype = "psm" Then
                    sql &= " AND (a.status_id IN (5,6,8) OR 5 IN (SELECT status_id FROM ir_status_log WHERE ir_id = a.ir_id) ) "
                ElseIf viewtype = "ha" Then
                    '  sql &= " AND (b.dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " ) "
                    '  sql &= " OR be"
                    '  sql &= " )"
                    ' sql &= " INNER JOIN (SELECT t2.ir_id as detail_id , t2.dept_id as report_dept_id ,t1.* FROM ir_relate_dept t1 RIGHT OUTER JOIN ir_detail_tab t2 ON t1.ir_id = t2.ir_id  ) rr ON a.ir_id = rr.detail_id and (rr.report_dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " )  or rr.dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & "  )"
                    If flag = "update" Then
                        sql &= " AND (EXISTS(SELECT * FROM ir_dept_inform_update WHERE ir_id = a.ir_id AND dept_id IN (select y2.dept_id from user_access_costcenter y1 inner join user_dept y2 on y1.costcenter_id = y2.costcenter_id where y1.emp_code = " & Session("emp_code").ToString & " )) )"
                    End If
                Else
                    Throw New Exception("Invalid parameter")
                End If

                sql &= " GROUP BY a.ir_id , a.date_report ,b.datetime_report_ts ,a.date_close_ts ,a.date_close , a.date_submit ,a.date_submit_ts ,a.form_id ,a.status_id , b.ir_no , b.division, b.hn , c.ir_status_name , d.severe_name , g.form_name_en , b.flag_serious , a.is_cancel ,  i.psm_status_id , i.psm_status_name  , topic.grand_topic_name , topic_level1.topic_name , topic_level2.subtopic_name , tqm.chk_follow_id ,  a.is_change_to_draft , a.is_move_to_cfb "
                ' sql &= " AND a.report_emp_code = " & Session("emp_code")
                If viewtype = "dept" Then
                    If flag <> "update" Then
                        ' sql &= "  ORDER BY ISNULL(MAX(rr.create_date_ts),-1) DESC , a.ir_id DESC"
                        sql &= "  ORDER BY  b.ir_no DESC"
                    End If
                Else

                    If viewtype = "" Or flag = "update" Then
                        sql &= " ORDER BY a.ir_id DESC"
                    Else
                        sql &= " ORDER BY  b.ir_no DESC , a.date_submit DESC , a.ir_id DESC"
                    End If


                End If


                ds = conn.getDataSet(sql, "table1")
                lblNum.Text = FormatNumber(ds.Tables(0).Rows.Count, 0)
                ' Response.Write(sql)
                'Return
                GridView1.DataSource = ds
                GridView1.DataBind()

                txtnum_page.Text = GridView1.PageIndex + 1

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            End Try

        End Sub

        Sub bindDept()
            Dim reader As SqlDataReader
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "SELECT dept_name FROM user_profile GROUP BY dept_name"
                sql &= " ORDER BY dept_name"
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

        Sub bindDivision()
            Dim reader As SqlDataReader
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "SELECT * FROM user_dept "
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
                Dim lblFileNum As Label = CType(e.Row.FindControl("lblFileNum"), Label)
                Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
                Dim lblStatusID As Label = CType(e.Row.FindControl("lblStatusID"), Label)
                Dim lblIsCancel As Label = CType(e.Row.FindControl("lblIsCancel"), Label)
                ' Dim lblStatusDept As Label = CType(e.Row.FindControl("lblStatusDept"), Label)
                'Dim lblStatusPSM As Label = CType(e.Row.FindControl("lblStatusPSM"), Label)
                Dim lblTAT As Label = CType(e.Row.FindControl("lblTAT"), Label)
                Dim lblDateTS As Label = CType(e.Row.FindControl("lblDateTS"), Label)
                Dim lblCloseDate As Label = CType(e.Row.FindControl("lblCloseDate"), Label)
                Dim lblCloseDateTS As Label = CType(e.Row.FindControl("lblCloseDateTS"), Label)
                Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
                Dim HyperLink1 As HyperLink = CType(e.Row.FindControl("HyperLink1"), HyperLink)
                Dim LinkHN As Label = CType(e.Row.FindControl("LinkHN"), Label)
                Dim Image1 As Image = CType(e.Row.FindControl("Image1"), Image)
                Dim Image2 As Image = CType(e.Row.FindControl("Image2"), Image)
                Dim Image3 As Image = CType(e.Row.FindControl("Image3"), Image)
                Dim Image4 As Image = CType(e.Row.FindControl("Image4"), Image)
                Dim imgReply As Image = CType(e.Row.FindControl("imgReply"), Image)
                Dim imgFlag As Image = CType(e.Row.FindControl("imgFlag"), Image)
                Dim lblTQMFlag As Label = CType(e.Row.FindControl("lblTQMFlag"), Label)
                Dim lblSentinel As Label = CType(e.Row.FindControl("lblSentinel"), Label)

                Dim LinkDelete As ImageButton = CType(e.Row.FindControl("LinkDelete"), ImageButton)
                Dim LinkCancel As ImageButton = CType(e.Row.FindControl("LinkCancel"), ImageButton)

                Dim lblStatusPSM As Label = CType(e.Row.FindControl("lblStatusPSM"), Label)
                Dim lblStatusPSM_Name As Label = CType(e.Row.FindControl("lblStatusPSM_Name"), Label)

                Dim lblChangeDraft As Label = CType(e.Row.FindControl("lblChangeDraft"), Label)
                Dim lblMoveCFB As Label = CType(e.Row.FindControl("lblMoveCFB"), Label)

                Dim sql As String
                Dim ds As New DataSet
                Dim dsReply As New DataSet
                Try
                    If HyperLink1.Text = "0" Then
                        HyperLink1.Text = "************"
                    Else
                        HyperLink1.Text = "IR" & HyperLink1.Text
                    End If

                    If lblStatusPSM.Text <> "0" Then
                        lblStatusPSM_Name.Text = "<br/>  " & lblStatusPSM_Name.Text & ""
                        lblStatusPSM_Name.Visible = True
                    Else
                        lblStatusPSM_Name.Text = ""
                        lblStatusPSM_Name.Visible = False
                    End If

                    If viewtype = "tqm" Then
                        If lblTQMFlag.Text = "1" Then
                            imgFlag.Visible = True
                        End If
                    End If

                    sql = "SELECT * FROM ir_information_update WHERE ir_id = " & lblPK.Text & " ORDER BY inform_id DESC"
                    dsReply = conn.getDataSet(sql, "t1")
                    If dsReply.Tables("t1").Rows.Count > 0 Then
                        imgReply.Visible = True
                        imgReply.ToolTip = dsReply.Tables("t1").Rows(0)("inform_detail").ToString & vbCrLf & "By " & dsReply.Tables("t1").Rows(0)("inform_by").ToString
                    End If

                    If viewtype = "dept" Then
                        sql = "SELECT * FROM ir_status_log a INNER JOIN ir_status_list b ON a.status_id = b.ir_status_id "
                        sql &= " INNER JOIN ir_relate_dept c ON a.ir_id = c.ir_id "
                        sql &= " WHERE a.status_id IN (4,7) AND a.ir_id = " & lblPK.Text
                        sql &= " AND c.dept_id = " & Session("dept_id").ToString
                        sql &= " ORDER BY a.log_time_ts DESC"
                        '  Response.Write(sql)
                        ds = conn.getDataSet(sql, "t1")
                        If ds.Tables("t1").Rows.Count > 0 Then

                            e.Row.BackColor = Drawing.ColorTranslator.FromHtml("#CDEBD9")

                            If ds.Tables("t1").Rows(0)("is_investigate").ToString = "1" Then
                                e.Row.Font.Bold = False
                            Else
                                e.Row.Font.Bold = True
                                lblStatus.Text = "Need Sup/Mgr/Dept Mgr Investigation"
                            End If

                            If lblStatusID.Text = "9" Then
                                lblStatus.Text = "Closed"
                            Else
                                '   lblStatus.Text = ds.Tables(0).Rows(0)("ir_status_name").ToString
                            End If
                        Else

                        End If
                    ElseIf viewtype = "psm" Then
                        sql = "SELECT * FROM ir_status_log a INNER JOIN ir_status_list b ON a.status_id = b.ir_status_id "
                        ' sql &= " INNER JOIN ir_relate_dept c ON a.ir_id = c.ir_id "
                        sql &= " WHERE a.status_id IN (5,8) AND a.ir_id = " & lblPK.Text
                        '  sql &= " AND c.dept_id = " & Session("dept_id").ToString
                        sql &= " ORDER BY a.log_time_ts DESC"
                        'Response.Write(sql)
                        ds = conn.getDataSet(sql, "t1")
                        If ds.Tables("t1").Rows.Count > 0 Then

                            'e.Row.BackColor = Drawing.ColorTranslator.FromHtml("#CDEBD9")
                            'lblStatus.Text


                            If lblStatusID.Text = "9" Then
                                lblStatus.Text = "Closed"
                            Else
                                lblStatus.Text = ds.Tables(0).Rows(0)("ir_status_name").ToString
                            End If
                        Else

                        End If
                    End If



                    lblTAT.Text = MinuteDiff(lblDateTS.Text, Date.Now.Ticks.ToString)

                    If lblStatusID.Text = "2" Then
                        If viewtype = "tqm" Then
                            If findArrayValue(priv_list, "4") = True Then
                                e.Row.Font.Bold = True
                            End If
                        End If

                    End If

                    If lblStatusID.Text = "9" Then
                        lblTAT.Text = MinuteDiff(lblDateTS.Text, lblCloseDateTS.Text)
                        lblCloseDate.Visible = True
                        lblStatus.ForeColor = Drawing.Color.Red

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
                    ElseIf getTAT() > 1440 And getTAT() <= (1440 * 14) Then
                        lblTAT.ForeColor = Drawing.Color.YellowGreen
                    ElseIf getTAT() > (1440 * 14) Then
                        lblTAT.ForeColor = Drawing.Color.Red
                    Else

                    End If

                    ' Repeat Incident
                    Dim sqlB As New StringBuilder
                    sqlB.Append("SELECT  b.hn ,b.datetime_report ,c.form_name_en  , b.datetime_report_ts FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id ")
                    sqlB.Append(" INNER JOIN ir_form_master c ON c.form_id = a.form_id WHERE 1 = 1 AND a.status_id <> 1 ")
                    ' sqlB.Append(" INNER JOIN ir_m_severity d ON d.severe_id = b.severe_id")
                    If LinkHN.Text = "" Then
                        sqlB.Append(" AND 1 > 2 ")
                    Else
                        sqlB.Append(" AND b.hn = '" & LinkHN.Text & "' ")
                    End If

                    sqlB.Append(" GROUP BY b.hn ,b.datetime_report ,c.form_name_en ,  b.datetime_report_ts")
                    sqlB.Append(" ORDER BY b.datetime_report_ts DESC")
                    sql = sqlB.ToString
                    ds = conn.getDataSet(sql, "t2")
                    If ds.Tables("t2").Rows.Count > 1 Then
                        '  Response.Write(sql & "<br/>")
                        Image1.Visible = True
                    Else
                        Image1.Visible = False
                    End If

                    ' Repeat CFB
                    sql = "SELECT a.hn ,a.datetime_report ,b.comment_type_name , c.dept_name , a.cfb_no FROM cfb_detail_tab a INNER JOIN cfb_comment_list b ON a.ir_id = b.ir_id "
                    sql &= " LEFT OUTER JOIN cbf_relate_dept c ON b.comment_id = c.comment_id"
                    sql &= " INNER JOIN ir_trans_list d ON a.ir_id = d.ir_id"
                    sql &= " WHERE 1 = 1 "
                    If LinkHN.Text = "" Then
                        sql &= "  AND 1 > 2"
                    Else
                        sql &= " AND a.hn = '" & LinkHN.Text & "' AND a.ir_id <> " & lblPK.Text
                    End If

                    sql &= " GROUP BY a.hn ,a.datetime_report ,b.comment_type_name,a.datetime_report_ts, c.dept_name, a.cfb_no"
                    sql &= " ORDER BY a.datetime_report_ts DESC"
                    '  Response.Write(sql & "<br/>")
                    ds = conn.getDataSet(sql, "t3")
                    If ds.Tables("t3").Rows.Count > 0 Then
                        ' Response.Write(sql & "<br/>")
                        Image2.Visible = True
                    Else
                        Image2.Visible = False
                    End If

                    If CInt(lblFileNum.Text) > 0 Then
                        Image3.Visible = True
                    Else
                        Image3.Visible = False
                    End If

                    If lblSentinel.Text = "1" Then
                        Image4.Visible = True
                    Else
                        Image4.Visible = False
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

                    If lblMoveCFB.Text = "1" Then
                        lblStatus.Text = "Move to CFB report"
                        e.Row.Font.Bold = False
                        lblStatus.Font.Bold = True
                        lblStatus.ForeColor = Drawing.Color.Red
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

        Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
            bindGrid()
        End Sub

        Protected Sub cmdReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReset.Click
            txtdate1.Text = ""
            txtdate2.Text = ""
            txtfindhn.Value = ""
            txtdept.SelectedIndex = 0
            txtstatus.SelectedIndex = 0
            txtunit_defendant.SelectedIndex = 0
            txtfinddoctor.Text = ""
            txtspecialty.SelectedIndex = 0
            bindGrid()
        End Sub

        Protected Sub cmdGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGo.Click
            Try
                If CInt(txtnum_page.Text) > GridView1.PageCount Then
                    txtnum_page.Text = GridView1.PageCount
                End If

                GridView1.PageIndex = txtnum_page.Text - 1
            Catch ex As Exception
                GridView1.PageIndex = 0
                txtnum_page.Text = 1
            End Try


            bindGrid()
        End Sub

        Protected Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
            Export("incident.xls", GridView1)
        End Sub
    End Class

End Namespace