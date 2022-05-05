Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class cfb_preview_investigate
    Inherits System.Web.UI.Page
    Protected irId As String = ""
    Protected mode As String = ""
    Protected dept_id As String = ""
    Protected comment_id As String = ""
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        irId = Request.QueryString("irId")
        mode = Request.QueryString("mode")
        dept_id = Request.QueryString("dept_id")
        comment_id = Request.QueryString("comment_id")
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        bindForm()
        bindInvestigate()
        bindDeptPreventiveAction()
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
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

        Dim htmlSpecialChars As String = "&amp;|&quot;|&#039;|&lt;|&gt;"
        Dim regx As New Regex(htmlSpecialChars)

        Try
            sql = "SELECT * FROM cbf_relate_dept a INNER JOIN cfb_comment_list b ON a.comment_id = b.comment_id"
            sql &= " INNER JOIN ir_trans_list c ON b.ir_id = c.ir_id "
            sql &= " INNER JOIN cfb_detail_tab d ON c.ir_id = d.ir_id "
            sql &= " LEFT OUTER JOIN ir_topic_grand f ON b.grand_topic = f.grand_topic_id"
            sql &= " LEFT OUTER JOIN ir_topic g ON b.topic = g.ir_topic_id"
            sql &= " LEFT OUTER JOIN ir_topic_sub h ON b.subtopic1 = h.ir_subtopic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub2 i ON b.subtopic2 = i.ir_subtopic2_id "
            sql &= " WHERE a.cfb_relate_dept_id = " & comment_id
           
            ds = conn.getDataSet(sql, "t1")
            lblNo.Text = ds.Tables(0).Rows(0)("cfb_no").ToString
            lblservice_type.Text = ds.Tables(0).Rows(0)("service_type").ToString & " "
            lblName.Text = ds.Tables(0).Rows(0)("complain_detail").ToString
            lbltitle.Text = ds.Tables(0).Rows(0)("pt_title").ToString
            lblAge.Text = ds.Tables(0).Rows(0)("age").ToString
            lblSex.Text = ds.Tables(0).Rows(0)("sex").ToString
            lblHN.Text = ds.Tables(0).Rows(0)("hn").ToString
            lbldatetime_report.Text = ds.Tables(0).Rows(0)("datetime_report").ToString & " "
            ' lblcomplain_detail.Text = ds.Tables(0).Rows(0)("complain_detail").ToString
            lblcountry.Text = ds.Tables(0).Rows(0)("country").ToString
            lblcomplain_status_remark.Text = ds.Tables(0).Rows(0)("complain_status_remark").ToString

            If ds.Tables(0).Rows(0)("feedback_from").ToString = "1" Then
                lblfeedback_from_remark.Text = "โดยตรงจากลูกค้า"
            ElseIf ds.Tables(0).Rows(0)("feedback_from").ToString = "2" Then
                lblfeedback_from_remark.Text = "กล่องรับความคิดเห็น"
            ElseIf ds.Tables(0).Rows(0)("feedback_from").ToString = "3" Then
                lblfeedback_from_remark.Text = "แบบแสดงความคิดเห็น"
            ElseIf ds.Tables(0).Rows(0)("feedback_from").ToString = "4" Then
                lblfeedback_from_remark.Text = "โทรศัพท์"
            ElseIf ds.Tables(0).Rows(0)("feedback_from").ToString = "5" Then
                lblfeedback_from_remark.Text = "อีเมล์"
            ElseIf ds.Tables(0).Rows(0)("feedback_from").ToString = "6" Then
                lblfeedback_from_remark.Text = "จดหมาย"
            ElseIf ds.Tables(0).Rows(0)("feedback_from").ToString = "7" Then
                lblfeedback_from_remark.Text = "แฟกซ์"
            ElseIf ds.Tables(0).Rows(0)("feedback_from").ToString = "8" Then
                lblfeedback_from_remark.Text = "อื่นๆ ระบุ"

            End If

            '  lblresp_person.Text = ds.Tables(0).Rows(0)("resp_person").ToString

            lblDept.Text = ds.Tables(0).Rows(0)("division").ToString
            If ds.Tables(0).Rows(0)("complain_status").ToString = "1" Then
                txtcomplain_status.Text = "Patient"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "2" Then
                txtcomplain_status.Text = "Relatives"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "3" Then
                txtcomplain_status.Text = "Visitors"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "4" Then
                txtcomplain_status.Text = "Employee"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "5" Then
                txtcomplain_status.Text = "Physician"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "6" Then
                txtcomplain_status.Text = "Web Site"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "7" Then
                txtcomplain_status.Text = "Other"
            End If

            lblreport_date.Text = ds.Tables(0).Rows(0)("date_report").ToString & " "
            lblreport_by.Text = ds.Tables(0).Rows(0)("report_by").ToString & " "
            lblreport_tel.Text = ds.Tables(0).Rows(0)("report_tel").ToString & " "



            ' lblDeptCommentDetail.Text = ds.Tables(0).Rows(0)("comment_detail").ToString.Replace(vbCrLf, "<br/>")
            lblDeptCommentDetail.Text = regx.Replace(Server.HtmlEncode(ds.Tables(0).Rows(0)("comment_detail").ToString.Replace(vbCrLf, "<br/>").Trim()), String.Empty)
            lbltypename.Text = ds.Tables(0).Rows(0)("comment_type_name").ToString
            lblOrder.Text = ds.Tables(0).Rows(0)("order_sort").ToString
            lblGrand.Text = ds.Tables(0).Rows(0)("grand_topic_name").ToString
            lblTopic.Text = ds.Tables(0).Rows(0)("topic_name").ToString
            If ds.Tables(0).Rows(0)("subtopic_name").ToString <> "" Then
                lblTopic0.Text = "," & ds.Tables(0).Rows(0)("subtopic_name").ToString
            End If

            If ds.Tables(0).Rows(0)("subtopic2_name_th").ToString <> "" Then
                lblTopic1.Text = "," & ds.Tables(0).Rows(0)("subtopic2_name_th").ToString
            End If

            lblDeptName.text = ds.Tables(0).Rows(0)("dept_name").ToString
        Catch ex As Exception
            Response.Write(ex.Message & sql)
            Response.End()
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindInvestigate()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM cfb_comment_list a INNER JOIN cbf_relate_dept b ON a.comment_id = b.comment_id "
            sql &= " WHERE b.cfb_relate_dept_id = " & comment_id

            sql &= " ORDER BY a.order_sort ASC , b.dept_name"
            ds = conn.getDataSet(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                lblans1.text = ds.Tables("t1").Rows(0)("dept_investigation").ToString
                lblans2.text = ds.Tables("t1").Rows(0)("dept_cause").ToString
                lblans3.text = ds.Tables("t1").Rows(0)("dept_corrective").ToString
                lblans4.text = ds.Tables("t1").Rows(0)("dept_result_detail").ToString
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
            Response.End()
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDeptPreventiveAction()
        Dim sql As String
        Dim ds As New DataSet

        Try
            If comment_id <> "" Then
                sql = "SELECT * FROM cfb_dept_prevent_list WHERE cfb_relate_dept_id = " & comment_id
                sql &= " ORDER BY order_sort ASC"
                ds = conn.getDataSet(sql, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    GridViewPrevent.DataSource = ds
                    GridViewPrevent.DataBind()
                Else
                    GridViewPrevent.Columns.Clear()
                    GridViewPrevent.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

End Class
