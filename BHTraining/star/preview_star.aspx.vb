Imports System.Data
Imports System.IO
Imports ShareFunction
Partial Class star_preview_star
    Inherits System.Web.UI.Page
    Protected id As String
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected num_endorse_com As Integer = 0
    Protected num_not_endorse_com As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        id = Request.QueryString("id")
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        bindDetail()
        bindSelectPerson()
        bindSelectDept()
        bindSelectDoctor()
        bindCommentList()

        bindCommitteeList()
        bindHrTab()
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

    Sub bindDetail()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT * "
            sql &= " , case when complain_status = 1 then 'ผู้ป่วย' when complain_status = 2 then 'ญาติผู้ป่วยหรือเพื่อน'  when complain_status = 3 then 'ผู้มาเยี่ยมไข้' "
            sql &= " when complain_status = 4 then 'พนักงาน'  when complain_status = 5 then 'แพทย์' else '-' end as nominee_type_name "
            sql &= " , case when feedback_from = 1 then 'แบบฟอร์มดาวเด่น' when feedback_from = 2 then 'แบบฟอร์ม CFB'  when feedback_from = 3 then 'Email / Web site ของ ร.พ.' "
            sql &= " when feedback_from = 4 then 'อีเมล์ / แฟกซ์'  when feedback_from = 5 then 'โทรศัพท์' else 'อื่นๆ' end as feedback "
            sql &= " , case when customer_segment = 1 then 'Thai' when customer_segment = 2 then 'Expatriate'  when customer_segment = 3 then 'International' "
            sql &= "  else '-' end as segment "

            sql &= " , case when cfb_customer_resp = 1 then 'พึงพอใจ และไม่ต้องการให้ตอบกลับ' when cfb_customer_resp = 2 then 'พึงพอใจ และต้องการให้ตอบกลับ'  when cfb_customer_resp = 3 then 'ไม่พึงพอใจ และไม่ต้องการให้ตอบกลับ' "
            sql &= " when cfb_customer_resp = 4 then 'ไม่พึงพอใจ และต้องการให้ตอบกลับ'   else '-' end as response "

            sql &= " FROM star_detail_tab a INNER JOIN star_trans_list b ON a.star_id = b.star_id WHERE a.star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

           

            lblStarNo.Text = ds.Tables(0).Rows(0)("star_no").ToString
            ' txtcfbid.Text = ds.Tables(0).Rows(0)("cfb_no").ToString
            txthn.Text = ds.Tables(0).Rows(0)("hn").ToString

            txtlocation.Text = ds.Tables(0).Rows(0)("location").ToString
            txtroom.Text = ds.Tables(0).Rows(0)("room").ToString
            txtdate_complaint.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("datetime_complaint_ts").ToString)

            txtptage.Text = ds.Tables(0).Rows(0)("age").ToString
            txtptsex.Text = ds.Tables(0).Rows(0)("sex").ToString
            lblTitle.Text = ds.Tables(0).Rows(0)("pt_title").ToString
            txtsegment.Text = ds.Tables(0).Rows(0)("segment").ToString
            txtservicetype.Text = ds.Tables(0).Rows(0)("service_type").ToString

            txtdate_report.Text = ConvertTSToDateString(ds.Tables(0).Rows(0)("datetime_report_ts").ToString)
            ' txthour.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString, "hour")
            ' txtmin.SelectedValue = ConvertTSTo(ds.Tables("t1").Rows(0)("datetime_report_ts").ToString, "min")
            txtptname.Value = ds.Tables(0).Rows(0)("complain_detail").ToString
            txtcountry.Text = ds.Tables(0).Rows(0)("country").ToString
            ' txtdept.SelectedValue = ds.Tables(0).Rows(0)("cfb_dept_id").ToString
            '  txtcomplain_status.SelectedValue = ds.Tables(0).Rows(0)("complain_status").ToString
            txtfeedback_from.Text = ds.Tables(0).Rows(0)("feedback").ToString
            txtname_type.Text = ds.Tables(0).Rows(0)("nominee_type_name").ToString
            txtcomplain_remark.Text = ds.Tables(0).Rows(0)("complain_status_remark").ToString
            txtfeedback_remark11.Text = ds.Tables(0).Rows(0)("feedback_from_remark").ToString


            txtcustomer.Text = ds.Tables(0).Rows(0)("response").ToString
            txtcus_detail.Text = ds.Tables(0).Rows(0)("cfb_customer_resp_remark").ToString

          

            txttel.Text = ds.Tables(0).Rows(0)("cfb_tel_remark").ToString
            txtemail.Text = ds.Tables(0).Rows(0)("cfb_email_remark").ToString
            txtother.Text = ds.Tables(0).Rows(0)("cfb_other_remark").ToString

            txtstar_detail.Text = ds.Tables(0).Rows(0)("service_detail").ToString
           

            txtCFBNo.Text = ds.Tables(0).Rows(0)("cfbno_relate").ToString

            txtnominee_type.SelectedValue = ds.Tables(0).Rows(0)("nominee_type_id").ToString
            txtcustom_name.Text = ds.Tables(0).Rows(0)("custom_nominee").ToString


          

         
        Catch ex As Exception
            Response.Write(ex.Message & ":" & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from star_relate_person WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            '  txtperson_select.DataSource = ds
            'txtperson_select.DataBind()

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblNominee.Text &= ds.Tables("t1").Rows(i)("user_fullname").ToString & "<br/>"
            Next i

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from star_relate_dept WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")



            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblDeptSelect.Text &= ds.Tables("t1").Rows(i)("costcenter_name").ToString & "<br/>"
            Next i

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectDoctor()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , doctor_name AS doctor_name_en , emp_code as emp_no from star_relate_doctor WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
         

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblDocSelect.Text &= ds.Tables("t1").Rows(i)("doctor_name").ToString & "<br/>"
            Next i
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCommentList() ' manager comment
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_manager_comment WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")



            GridManagerComment.DataSource = ds
            GridManagerComment.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCommitteeList() ' committee comment
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_committee_comment WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")



            GridComment.DataSource = ds
            GridComment.DataBind()

            If ds.Tables("t1").Rows.Count <= 0 Then
                lblSumEndorse1.Text = 0
                lblSumNotEndorse1.Text = 0

                '  lblSumEndorse2.Text = 0
                ' lblSumNotEndorse2.Text = 0
            End If

            bindCommitteeSummaryAward()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCommitteeSummaryAward() ' committee comment award
        Dim sql As String
        Dim ds As New DataSet

        Try
            lblEndorseDetail.Text = ""

            sql = "SELECT recognition_award , COUNT(recognition_award) AS num FROM star_committee_comment "

            sql &= " WHERE recognition_id = 1 AND star_id = " & id
            sql &= " GROUP BY recognition_award ORDER BY recognition_award"
            ds = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblEndorseDetail.Text &= " " & ds.Tables("t1").Rows(i)("num").ToString & " - " & ds.Tables("t1").Rows(i)("recognition_award").ToString & "<br/>"

                '  lblEndorseDetail2.Text = lblEndorseDetail.Text
            Next i



        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindHrTab()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_hr_tab WHERE star_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count > 0 Then
                txthr_recog_type.Text = ds.Tables("t1").Rows(0)("team_name").ToString
                txtevent_admire.Text = ds.Tables("t1").Rows(0)("event_name").ToString
                '  txtsentence_admire.SelectedValue = ds.Tables("t1").Rows(0)("sentence_name").ToString
                txthr_detail_combo.Text = ds.Tables("t1").Rows(0)("detail_name").ToString
                If ds.Tables("t1").Rows(0)("chk_clear").ToString = "1" Then
                    chk_communicate.Checked = True
                Else
                    chk_communicate.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_care").ToString = "1" Then
                    chk_relative.Checked = True
                Else
                    chk_relative.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_smart").ToString = "1" Then
                    chk_talent.Checked = True
                Else
                    chk_talent.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("chk_quality").ToString = "1" Then
                    chk_quality.Checked = True
                Else
                    chk_quality.Checked = False
                End If

                txthr_recog.Text = ds.Tables("t1").Rows(0)("recognition_name").ToString
                txthr_type.Text = ds.Tables("t1").Rows(0)("recognition_type_name").ToString
                txthr_commit.Text = ds.Tables("t1").Rows(0)("committee_name").ToString

                txtstar_comment.Text = ds.Tables("t1").Rows(0)("comment").ToString
                txtsrp.Text = ds.Tables("t1").Rows(0)("srp_point").ToString
                txtcash.Text = ds.Tables("t1").Rows(0)("cash_num").ToString
                txtdate_award.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("award_date").ToString)
                txtdate_receive.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("receive_date").ToString)
                txtstar_remark.Text = ds.Tables("t1").Rows(0)("award_remark").ToString

                Try
                    lblMainTopic.Text = ds.Tables("t1").Rows(0)("topic_name").ToString
                Catch ex As Exception

                End Try



                Try
                    lblSubTopic.Text = ds.Tables("t1").Rows(0)("subtopic_name").ToString
                Catch ex As Exception

                End Try


            End If


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridComment_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridComment.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblEmpcode As Label = CType(e.Row.FindControl("lblEmpcode"), Label)
            '  Dim cmdDelComment As ImageButton = CType(e.Row.FindControl("cmdDelComment"), ImageButton)

            Dim lblEndorseID As Label = CType(e.Row.FindControl("lblEndorseID"), Label)
            Dim lblAward As Label = CType(e.Row.FindControl("lblAward"), Label)

            Dim lblCare As Label = CType(e.Row.FindControl("lblCare"), Label)
            Dim lblChk1 As Label = CType(e.Row.FindControl("lblChk1"), Label)
            Dim lblChk2 As Label = CType(e.Row.FindControl("lblChk2"), Label)
            Dim lblChk3 As Label = CType(e.Row.FindControl("lblChk3"), Label)
            Dim lblChk4 As Label = CType(e.Row.FindControl("lblChk4"), Label)

            Dim sql As String
            Dim ds As New DataSet

            Try
                If lblChk1.Text = "1" Then
                    lblCare.Text &= "- ความสามารถในการสื่อสาร <br/>"
                End If

                If lblChk2.Text = "1" Then
                    lblCare.Text &= "- สัมพันธไมตรีแบบไทย <br/>"
                End If

                If lblChk3.Text = "1" Then
                    lblCare.Text &= "- ความเป็นเลิศทางวิชาการ <br/>"
                End If

                If lblChk4.Text = "1" Then
                    lblCare.Text &= "- คุณภาพงานบริการ <br/>"
                End If

                If lblEndorseID.Text = "1" Then
                    num_endorse_com += 1
                ElseIf lblEndorseID.Text = "2" Then
                    num_not_endorse_com += 1
                End If

                lblSumEndorse1.Text = num_endorse_com
                lblSumNotEndorse1.Text = num_not_endorse_com

                '  lblSumEndorse2.Text = num_endorse_com
                'lblSumNotEndorse2.Text = num_not_endorse_com

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try

          
        End If
    End Sub

    Protected Sub GridComment_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridComment.SelectedIndexChanged

    End Sub
End Class
