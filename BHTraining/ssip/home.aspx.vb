Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Threading
Imports System.Net.Mail

Partial Class ssip_home
    Inherits System.Web.UI.Page

    Protected mode As String = ""
    Protected cfbId As String = ""
    Private new_ir_id As String = ""
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected session_id As String = ""
    Protected lang As String = "th"
    Protected flag As String = ""

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("form_ssip.aspx?mode=add")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mode = Request.QueryString("mode")
        cfbId = Request.QueryString("cfbId")
        flag = Request.QueryString("flag")

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
            bindDept()
            bindStatus()
            bindGrid()

            If viewtype = "" Or viewtype = "public" Then
                ' GridView1.Columns("3").Visible = False
                GridView1.Columns("4").Visible = False
            End If

            If viewtype = "hr" Then
                GridView1.Columns("0").Visible = True
                div_hr.Visible = True
            Else
                GridView1.Columns("0").Visible = False
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

    Private Sub bindGrid()
        Dim ds As New DataSet
        Dim sql As String
        Try
            ' sql = "SELECT a.* , b.* , c.ir_status_name , d.* , g.form_name_en , a.date_report AS create_date FROM  ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id  "
            sql = "SELECT *  , ISNULL(a.ssip_no,'-') AS ssip_no1 FROM ssip_trans_list a INNER JOIN ssip_detail_tab b ON a.ssip_id = b.ssip_id"
            sql &= " INNER JOIN ssip_status_list c ON a.status_id = c.status_id"
            ' sql &= " LEFT OUTER JOIN ssip_status_list d ON a.review_status_id = d.status_id AND d.status_type = 2 "
            sql &= " lEFT OUTER JOIN ssip_hr_tab d ON a.ssip_id = d.ssip_id"
            'sql &= " LEFT OUTER JOIN ssip_manager_tab e ON a.ssip_id = e.ssip_id "
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "
            If viewtype = "" Then
                sql &= " AND report_emp_code = " & Session("emp_code").ToString
            End If
            If flag = "activity" Then
                sql &= " AND a.ssip_id IN (SELECT ssip_id FROM ssip_relate_person WHERE emp_code = " & Session("emp_code").ToString & ") "
            End If

            If viewtype = "hr" Then
                sql &= " AND a.status_id IN (2,3,4,5,6,8)"
            End If
            If viewtype = "public" Then
                sql &= " AND a.is_public = 1 "
            End If

            If viewtype = "sup" Or viewtype = "com" Then
                sql &= " AND (a.report_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_ssip WHERE emp_code = " & Session("emp_code").ToString & ") "
                sql &= " OR EXISTS (SELECT costcenter_id FROM ssip_hr_relate_dept WHERE ssip_id = a.ssip_id AND costcenter_id = " & Session("dept_id").ToString & " ) )"
                sql &= " AND a.status_id IN (3,4,5)"
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND a.submit_date BETWEEN '" & convertToSQLDatetime(txtdate1.Text) & "' AND '" & convertToSQLDatetime(txtdate2.Text, "23", "59") & "' "
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND a.report_dept_id = " & txtdept.SelectedValue
            End If

            If txtstatus.SelectedValue <> "" Then
                sql &= " AND a.status_id = " & txtstatus.SelectedValue
            End If

            If txtname.Value <> "" Then
                sql &= " AND LOWER(a.report_by) LIKE '%" & txtname.Value.ToLower & "%' "
            End If

            If txttopic.Value <> "" Then
                sql &= " AND LOWER(b.topic) LIKE '%" & txttopic.Value.ToLower & "%' "
            End If

            sql &= " ORDER BY a.ssip_id DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            ' Return
            lblNum.text = ds.Tables("table1").Rows.Count
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
            sql = "SELECT * FROM ssip_status_list"
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
                sql = "UPDATE ssip_trans_list SET is_delete = 1 WHERE ssip_id = " & index
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
                sql = "UPDATE ssip_trans_list SET is_cancel = 1 WHERE ssip_id = " & index
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

            Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
            Dim lblSend As Label = CType(e.Row.FindControl("lblSend"), Label)
            Dim lblMsg As Label = CType(e.Row.FindControl("lblMsg"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Try

                If HyperLink1.Text = "" Or HyperLink1.Text = "0" Then
                    HyperLink1.Text = "********"
                End If

             

                If lblStatusId.Text = "8" Then
                    chkSelect.Visible = True
                Else
                    chkSelect.Visible = False
                End If

                If lblSend.Text = "1" Then
                    chkSelect.Visible = False
                    lblMsg.Text = "Score was sent"
                End If

                If lblStatusId.Text = "6" Then
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

                sql = "SELECT COUNT(a.ssip_id) FROM ssip_manager_tab a INNER JOIN ssip_manager_comment b ON a.comment_id = b.comment_id WHERE a.ssip_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    lblDeptNum.Text = ds.Tables("t1").Rows(0)(0).ToString
                Else
                    lblDeptNum.Text = 0
                End If

                sql = "SELECT COUNT(a.ssip_id) FROM ssip_committee_tab a INNER JOIN ssip_manager_comment b ON a.comment_id = b.comment_id WHERE a.ssip_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t2")
                If ds.Tables("t2").Rows.Count > 0 Then
                    lblComNum.Text = ds.Tables("t2").Rows(0)(0).ToString
                Else
                    lblComNum.Text = 0
                End If

                sql = "SELECT COUNT(*) FROM ssip_information_update WHERE ssip_id = " & lblPk.Text
                ds = conn.getDataSet(sql, "t3")
                If ds.Tables("t3").Rows.Count > 0 Then
                    lblCommentNum.Text = ds.Tables("t3").Rows(0)(0).ToString
                Else
                    lblCommentNum.Text = 0
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
        Dim avg_point As Integer = 0
        Dim total_point As Integer = 0

        Try


            For s As Integer = 0 To i - 1
                '  Response.Write(1)
                chkSelect = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)
                '  Response.Write(2)
                If chkSelect.Checked = True Then
                    'Response.Write(4)
                    lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)

                    sql = "SELECT * FROM ssip_hr_tab a INNER JOIN ssip_trans_list b ON a.ssip_id = b.ssip_id WHERE a.point_total > 0 AND ISNULL(b.is_send_score,0) = 0 AND a.ssip_id = " & lbl.Text
                    ds = conn.getDataSet(sql, "t1")
                    ' Response.Write(ds.Tables("t1").Rows.Count)
                    If ds.Tables("t1").Rows.Count > 0 Then

                        sql = "SELECT * FROM ssip_relate_person a INNER JOIN user_profile b ON a.emp_code = b.emp_code WHERE a.ssip_id = " & lbl.Text
                        ds2 = conn.getDataSet(sql, "t2")

                        Integer.TryParse(ds.Tables("t1").Rows(0)("point_total").ToString, total_point)

                        Try
                            avg_point = total_point / ds2.Tables("t2").Rows.Count
                        Catch ex As Exception
                            avg_point = 0
                        End Try


                        For iPerson As Integer = 0 To ds2.Tables("t2").Rows.Count - 1


                            pk = getPK("point_movement_id", "srp_point_movement", conn)

                            sql = "INSERT INTO srp_point_movement (point_movement_id , emp_code , emp_name , card_id , transaction_name , reward_type_id "
                            sql &= " , reward_type_name , movememt_type_id , movememt_type_name , point_trans , movement_remark , movement_create_by "
                            sql &= " , movement_create_date_ts , movement_create_date_raw  "

                            sql &= ") VALUES("
                            sql &= " '" & pk & "' , "
                            'Response.Write(11)
                            sql &= " '" & ds2.Tables("t2").Rows(iPerson)("emp_code").ToString & "' , "
                            sql &= " '" & ds2.Tables("t2").Rows(iPerson)("user_fullname").ToString & "' , "
                            'Response.Write(22)
                            sql &= " '" & 0 & "' , "
                            sql &= " 'SSIP Point' , "
                            sql &= " 2 , "
                            sql &= " 'SSIP' , "
                            sql &= " 2 , "
                            sql &= " 'SSIP No. " + ds.Tables("t1").Rows(0)("ssip_no").ToString + " ' , "
                            ' MsgBox(txtadd_point.SelectedItem.ToString())

                            sql &= " " & avg_point & " , "
                            sql &= " 'Point from SSIP, " + ds.Tables("t1").Rows(0)("innovation_subject").ToString + " ' , "
                            sql &= " '" & Session("user_fullname").ToString & "' , "
                            sql &= " '" & Date.Now.Ticks & "' , "
                            sql &= " GETDATE() "

                            sql &= ")"

                            '  Response.Write(sql)
                            '  Return
                            'MsgBox(sql)
                            errorMsg = conn.executeSQL(sql)

                            If errorMsg <> "" Then
                                Throw New Exception(errorMsg)
                            End If

                        Next iPerson

                        sql = "UPDATE ssip_trans_list SET is_send_score = 1 , send_score_date_raw = GETDATE() , send_score_date_ts = " & Date.Now.Ticks & " WHERE  ssip_id = " & lbl.Text
                        errorMsg = conn.executeSQL(sql)
                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg)
                        End If

                    Else

                    End If

                    'updateOnlyLog("0", lbl.Text, "Send Star Point to SRP")

                    '  updateOnlyLog(txthrstatus.SelectedValue, lbl.Text, "HR Process")
                End If
            Next s


            ' conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            'conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub
End Class
