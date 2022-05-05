Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class cfb_incident
    Inherits System.Web.UI.Page

    Protected irId As String = ""
    Protected mode As String = ""
    Protected dept_id As String = ""

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected diag As String = ""
    Protected viewtype As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        irId = Request.QueryString("irId")
        mode = Request.QueryString("mode")
        dept_id = Request.QueryString("dept_id")
        viewtype = Request.QueryString("viewtype")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        bindForm()
        bindDetailList()
        bindDepartment()
        ' bindInvestigate()
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
            sql = "SELECT a.report_by , b.* FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id"
            sql &= " WHERE a.ir_id = " & irId
            ds = conn.getDataSet(sql, "t1")
            lblNo.Text = ds.Tables(0).Rows(0)("cfb_no").ToString
            lblservice_type.Text = ds.Tables(0).Rows(0)("service_type").ToString & " "
            lblName.Text = ds.Tables(0).Rows(0)("complain_detail").ToString
            lbltitle.Text = ds.Tables(0).Rows(0)("pt_title").ToString
            lblAge.Text = ds.Tables(0).Rows(0)("age").ToString
            lblSex.Text = ds.Tables(0).Rows(0)("sex").ToString
            lblHN.Text = ds.Tables(0).Rows(0)("hn").ToString
            lbldatetime_report.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("datetime_report_ts").ToString) & " "
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
            ElseIf ds.Tables(0).Rows(0)("feedback_from").ToString = "9" Then
                lblfeedback_from_remark.Text = "เว็บไซต์"
            ElseIf ds.Tables(0).Rows(0)("feedback_from").ToString = "8" Then
                lblfeedback_from_remark.Text = "อื่นๆ ระบุ"
            End If

            '  lblresp_person.Text = ds.Tables(0).Rows(0)("resp_person").ToString

            lblDept.Text = ds.Tables(0).Rows(0)("division").ToString
            If ds.Tables(0).Rows(0)("complain_status").ToString = "1" Then
                txtcomplain_status.Text = "ผู้ป่วย"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "2" Then
                txtcomplain_status.Text = "ญาติผู้ป่วย"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "3" Then
                txtcomplain_status.Text = "ผู้มาเยี่ยมไข้"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "4" Then
                txtcomplain_status.Text = "พนักงาน"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "5" Then
                txtcomplain_status.Text = "แพทย์"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "6" Then
                txtcomplain_status.Text = "เว็บไซต์"
            ElseIf ds.Tables(0).Rows(0)("complain_status").ToString = "7" Then
                txtcomplain_status.Text = "อื่นๆ"
            End If

            lblreport_date.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("datetime_report_ts").ToString) & " "
            lblreport_by.Text = ds.Tables(0).Rows(0)("report_by").ToString & " "
            lblreport_tel.Text = ds.Tables(0).Rows(0)("report_tel").ToString & " "
            lblOwner.Text = ds.Tables(0).Rows(0)("report_tel").ToString & " "
            If ds.Tables(0).Rows(0)("cfb_is_complain").ToString = "1" Then
                lblcom.Text = "ได้"
            ElseIf ds.Tables(0).Rows(0)("cfb_is_complain").ToString = "0" Then
                lblcom.Text = "ไม่ได้"
            Else
                lblcom.Text = "-"
            End If

            If ds.Tables(0).Rows(0)("cfb_customer_resp").ToString = "1" Then
                lblcustomer.Text = "พึงพอใจ และไม่ต้องการให้ตอบกลับ"
            ElseIf ds.Tables(0).Rows(0)("cfb_customer_resp").ToString = "2" Then
                lblcustomer.Text = "พึงพอใจ และต้องการให้ตอบกลับ"
            ElseIf ds.Tables(0).Rows(0)("cfb_customer_resp").ToString = "3" Then
                lblcustomer.Text = "ไม่พึงพอใจ และไม่ต้องการให้ตอบกลับ "
            ElseIf ds.Tables(0).Rows(0)("cfb_customer_resp").ToString = "4" Then
                lblcustomer.Text = "ไม่พึงพอใจ และต้องการให้ตอบกลับ "
            End If

            If ds.Tables(0).Rows(0)("cfb_chk_tel").ToString = "1" Then
                lblcallback.Text &= "โทรศัพท์ " & ds.Tables(0).Rows(0)("cfb_tel_remark").ToString & "<br/>"
            End If
            If ds.Tables(0).Rows(0)("cfb_chk_email").ToString = "1" Then
                lblcallback.Text &= "อีเมล์ " & ds.Tables(0).Rows(0)("cfb_email_remark").ToString & "<br/>"
            End If
            If ds.Tables(0).Rows(0)("cfb_chk_other").ToString = "1" Then
                lblcallback.Text &= "อื่นๆ  " & ds.Tables(0).Rows(0)("cfb_other_remark").ToString & ""
            End If

            lbldatetime_complaint.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("datetime_complaint_ts").ToString)
            If ds.Tables(0).Rows(0)("location").ToString = "" Then
                lbllocation.Text = "-"
            Else
                lbllocation.Text = ds.Tables(0).Rows(0)("location").ToString
            End If
            If ds.Tables(0).Rows(0)("room").ToString = "" Then
                lblroom.Text = "-"
            Else
                lblroom.Text = ds.Tables(0).Rows(0)("room").ToString
            End If
            If ds.Tables(0).Rows(0)("diagnosis").ToString = "" Then
                lbldiagnosis.Text = "-"
            Else
                lbldiagnosis.Text = ds.Tables(0).Rows(0)("diagnosis").ToString
            End If
            If ds.Tables(0).Rows(0)("operation").ToString = "" Then
                lblprocedure.Text = "-"
            Else
                lblprocedure.Text = ds.Tables(0).Rows(0)("operation").ToString
            End If

            '  lblservice_type.Text = ds.Tables(0).Rows(0)("service_type").ToString
            If ds.Tables(0).Rows(0)("customer_segment").ToString = "1" Then
                lblcustomertype.Text = "ไทย"
            ElseIf ds.Tables(0).Rows(0)("customer_segment").ToString = "2" Then

                lblcustomertype.Text = "ต่างชาติ"
            ElseIf ds.Tables(0).Rows(0)("customer_segment").ToString = "3" Then
                lblcustomertype.Text = "ต่างชาติ (ย้ายภูมิลำเนา)"
            End If


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
            sql &= " WHERE a.ir_id = " & irId
            sql &= " ORDER BY a.order_sort ASC , b.dept_name"
            ds = conn.getDataSet(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                GridView2.DataSource = ds
                GridView2.DataBind()
                lblOwner.Text = ds.Tables(0).Rows(0)("tqm_owner").ToString
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
            Response.End()
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDetailList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM cfb_comment_list a "
            sql &= " LEFT OUTER JOIN ir_topic_grand d ON a.grand_topic = d.grand_topic_id "
            sql &= " LEFT OUTER jOIN ir_topic e ON d.grand_topic_id = e.grand_topic_id AND e.ir_topic_id = a.topic"
            sql &= " LEFT OUTER jOIN ir_topic_sub f ON e.ir_topic_id = f.ir_topic_id AND f.ir_subtopic_id = a.subtopic1"
            sql &= "  WHERE a.ir_id = " & irId
            sql &= " ORDER BY a.order_sort "
            '  Response.Write(sql)
            ' sql &= " WHERE a.ir_id = " & irId
            ds = conn.getDataSet(sql, "t1")
           
            GridView1.DataSource = ds
            GridView1.DataBind()


        Catch ex As Exception
            Response.Write("bindDetailList : " & ex.Message & sql)
            Response.End()
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblDeptList As Label = CType(e.Row.FindControl("lblDeptList"), Label)

            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblUnit As Label = CType(e.Row.FindControl("lblUnit"), Label)
            Dim lblDoctor As Label = CType(e.Row.FindControl("lblDoctor"), Label)

            Dim sql As String
            '  Dim ds As New DataSet
            Dim ds2 As New DataSet
            Try


                sql = "SELECT * FROM cbf_relate_dept WHERE comment_id = " & lblPK.Text

                ds2 = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds2.Tables(0).Rows.Count - 1

                    If i > 0 Then
                        ' lblDeptList.Text &= "<br/>"
                    End If
                    lblDeptList.Text &= "<br/> - " & ds2.Tables(0).Rows(i)("dept_name").ToString
                Next i

                sql = "SELECT * FROM ir_cfb_unit_defendant WHERE comment_id = " & lblPK.Text
                ds2 = conn.getDataSetForTransaction(sql, "t2")
                For i As Integer = 0 To ds2.Tables("t2").Rows.Count - 1
                    lblUnit.Text &= "- " & ds2.Tables("t2").Rows(i)("dept_unit_name").ToString & "<br/>"
                Next i

                sql = "SELECT * FROM ir_doctor_defendant WHERE comment_id = " & lblPK.Text
                ds2 = conn.getDataSetForTransaction(sql, "t3")
                For i As Integer = 0 To ds2.Tables("t3").Rows.Count - 1
                    lblDoctor.Text &= "- " & ds2.Tables("t3").Rows(i)("doctor_name").ToString & "<br/>"
                Next i
                'Response.Write(lblDeptList.Text)

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                '  ds.Dispose()
                ds2.Dispose()
            End Try

        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPKDept As Label = CType(e.Row.FindControl("lblPKDept"), Label)
            Dim lblDeptId As Label = CType(e.Row.FindControl("lblDeptId"), Label)
            Dim lblAns1 As Label = CType(e.Row.FindControl("lblAns1"), Label)
            Dim lblAns2 As Label = CType(e.Row.FindControl("lblAns2"), Label)
            Dim lblAns3 As Label = CType(e.Row.FindControl("lblAns3"), Label)
            Dim lblAns4 As Label = CType(e.Row.FindControl("lblAns4"), Label)
            Dim lblManagerName As Label = CType(e.Row.FindControl("lblManagerName"), Label)

            Dim sql As String
            '  Dim ds As New DataSet
            Dim ds2 As New DataSet
            Try


                sql = "SELECT * FROM cbf_relate_dept WHERE comment_id = " & lblPKDept.Text
                sql &= " AND dept_id = " & lblDeptId.Text

                ds2 = conn.getDataSetForTransaction(sql, "t1")
                If ds2.Tables(0).Rows(0)("dept_investigation").ToString <> "" Then
                    lblAns1.Text = ds2.Tables(0).Rows(0)("dept_investigation").ToString.Replace(vbCrLf, "<br/>")
                Else
                    lblAns1.Text = "-"
                End If

                If ds2.Tables(0).Rows(0)("dept_cause").ToString <> "" Then
                    lblAns2.Text = ds2.Tables(0).Rows(0)("dept_cause").ToString.Replace(vbCrLf, "<br/>")
                Else
                    lblAns2.Text = "-"
                End If

                If ds2.Tables(0).Rows(0)("dept_corrective").ToString <> "" Then
                    lblAns3.Text = ds2.Tables(0).Rows(0)("dept_corrective").ToString.Replace(vbCrLf, "<br/>")
                Else
                    lblAns3.Text = "-"
                End If

                If ds2.Tables(0).Rows(0)("dept_result_detail").ToString <> "" Then
                    lblAns4.Text = ds2.Tables(0).Rows(0)("dept_result_detail").ToString.Replace(vbCrLf, "<br/>")
                Else
                    lblAns4.Text = "-"
                End If

                If ds2.Tables(0).Rows(0)("investigate_by").ToString <> "" Then
                    lblManagerName.Text = ds2.Tables(0).Rows(0)("investigate_by").ToString & " " & ds2.Tables(0).Rows(0)("investigate_date").ToString
                Else
                    lblManagerName.Text = "-"
                End If
                'Response.Write(lblDeptList.Text)

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                '  ds.Dispose()
                ds2.Dispose()
            End Try

        End If
    End Sub

    Sub bindDepartment()
        Dim sql As String
        Dim ds As New DataSet
        Dim dsSub As New DataSet
        Dim ds2 As New DataSet
        Dim str As String = ""
        Try
            lblDepartmentList.Text = ""
            sql = "SELECT * FROM cfb_comment_list a "
            sql &= " LEFT OUTER JOIN ir_topic_grand f ON a.grand_topic = f.grand_topic_id"
            sql &= " LEFT OUTER JOIN ir_topic g ON a.topic = g.ir_topic_id"
            sql &= " LEFT OUTER JOIN ir_topic_sub h ON a.subtopic1 = h.ir_subtopic_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub2 i ON a.subtopic2 = i.ir_subtopic2_id "
            sql &= " WHERE ir_id = " & irId

            If viewtype = "" And dept_id <> "" Then
                ' sql &= " AND dept_id = " & dept_id
            ElseIf viewtype = "tqm" Then

            End If

            ds = conn.getDataSet(sql, "t1")

            For s = 0 To ds.Tables("t1").Rows.Count - 1 ' Loop ตาม Comment
                str &= "<strong>Comment #" & s + 1 & "</strong> " & ds.Tables("t1").Rows(s)("comment_type_name").ToString & "<br/>"
                str &= "<strong>Grand Topic" & "</strong> " & ds.Tables("t1").Rows(s)("grand_topic_name").ToString & "<br/>"
                str &= "<strong>Topic" & "</strong> " & ds.Tables("t1").Rows(s)("topic_name").ToString & "<br/>"
                str &= "<strong>Sub Topic" & "</strong> " & ds.Tables("t1").Rows(s)("subtopic_name").ToString & "<br/>"
                str &= "<strong>รายละเอียด</strong> " & ds.Tables("t1").Rows(s)("comment_detail").ToString & "<br/>"

                sql = "SELECT * FROM cbf_relate_dept WHERE comment_id = " & ds.Tables("t1").Rows(s)("comment_id").ToString
                dsSub = conn.getDataSetForTransaction(sql, "t1Sub")

                For ss = 0 To dsSub.Tables("t1Sub").Rows.Count - 1 ' Loop ตาม Department

                  
                    str &= "<strong>แผนก</strong> " & dsSub.Tables("t1Sub").Rows(ss)("dept_name").ToString & "<br/>"
                    str &= "<strong>สรุปเหตุการณ์</strong> " & dsSub.Tables("t1Sub").Rows(ss)("dept_investigation").ToString.Replace(vbCrLf, "<br/>") & "<br/>"
                    str &= "<strong>สาเหตุ</strong> " & dsSub.Tables("t1Sub").Rows(ss)("dept_cause").ToString.Replace(vbCrLf, "<br/>") & "<br/>"
                    str &= "<strong>Corrective & Prevention Action</strong> " & dsSub.Tables("t1Sub").Rows(ss)("dept_corrective").ToString.Replace(vbCrLf, "<br/>") & "<br/>"


                    sql = "SELECT * FROM cfb_dept_prevent_list WHERE cfb_relate_dept_id = " & dsSub.Tables("t1Sub").Rows(ss)("cfb_relate_dept_id").ToString
                    ' sql &= " ORDER BY order_sort ASC"

                    ds2 = conn.getDataSet(sql, "t" & (s + 2))
                    If ds2.Tables("t" & (s + 2)).Rows.Count > 0 Then
                        str &= "<strong>วิธีแก้ไขปัญหาและป้องกันการเกิดเหตุซ้ำ</strong> <br/>"

                        If ds2.Tables("t" & (s + 2)).Rows.Count > 0 Then

                            str &= "<table  width='100%' cellspacing='0' style='border-collapse:collapse' ><tr><td style='border:solid 1px #000;font-weight:bold'>Corrective & Preventive Actions</td><td style='border:solid 1px #000;font-weight:bold'>Start</td><td style='border:solid 1px #000;font-weight:bold'>Completed</td><td style='border:solid 1px #000;font-weight:bold'>Responsible Person</td></tr>"
                            For i As Integer = 0 To ds2.Tables("t" & (s + 2)).Rows.Count - 1 ' Loop ตาม Preventive Action
                                str &= "<tr>"
                                str &= "<td style='border:solid 1px #000'>" & ds2.Tables("t" & (s + 2)).Rows(i)("action_detail").ToString.Replace(vbCrLf, "<br/>") & "&nbsp;</td>"
                                str &= "<td style='border:solid 1px #000'>" & ds2.Tables("t" & (s + 2)).Rows(i)("date_start").ToString.Replace(vbCrLf, "<br/>") & "&nbsp;</td>"
                                str &= "<td style='border:solid 1px #000'>" & ds2.Tables("t" & (s + 2)).Rows(i)("date_end").ToString.Replace(vbCrLf, "<br/>") & "&nbsp;</td>"
                                str &= "<td style='border:solid 1px #000'>" & ds2.Tables("t" & (s + 2)).Rows(i)("resp_person").ToString.Replace(vbCrLf, "<br/>") & "&nbsp;</td>"
                                str &= "</tr>"
                            Next i

                            str &= "</table>"
                        End If
                    End If
                  
                    str &= "<br/>"
                Next ss
                '   Response.Write(s)
                '  Response.Write(ds.Tables("t1").Rows(s)("dept_name").ToString)
                'If ds.Tables("t1").Rows.Count > 0 Then
          
                ' Response.Write("xxx")
                'End If
            Next s

            lblDepartmentList.Text = str
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
            ds2.Dispose()
        End Try
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged

    End Sub
End Class
