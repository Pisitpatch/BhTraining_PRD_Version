Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class incident_preivew_tqm_tab
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected irId As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        irId = Request.QueryString("irId")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then 'load first time
            bindTQMTab()
            bindTQMGridCause()

            bindInfoDepartment_Grant()

            bindGridRelateDocument("ir")
            bindGridRelateDocument("cfb")
            bindDefendantUnit_Grant()
            bindTQMDoctor()
        Else

        End If

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindDefendantUnit_Grant()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_cfb_unit_defendant a WHERE 1 = 1 AND a.ir_id = " & irId
            sql &= " ORDER BY a.dept_unit_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(sql)
            

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                txtunit_defandent_select.Text &= ds.Tables("t1").Rows(i)("dept_unit_name").ToString
            Next i

        Catch ex As Exception
            Response.Write(ex.Message & ":" & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindInfoDepartment_Grant()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT * , dept_name AS dept_name_en FROM ir_dept_inform_update WHERE ir_id = " & irId
            sql &= " ORDER BY dept_name ASC"
            ds = conn.getDataSetForTransaction(sql, "t1")

         

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                txtinfo_dept2.Text &= ds.Tables("t1").Rows(i)("dept_name").ToString
            Next i

        Catch ex As Exception
            Response.Write(ex.Message & ":" & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTQMDoctor()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_doctor_defendant a INNER JOIN m_doctor b ON a.md_code = b.emp_no WHERE a.ir_id = " & irId
            ds = conn.getDataSetForTransaction(sql, "t1")

            GridViewTQMDoctor.DataSource = ds
            GridViewTQMDoctor.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridRelateDocument(Optional ByVal docType As String = "ir")
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_relate_document WHERE reference_type = '" & docType & "' AND ir_id = " & irId
            ds = conn.getDataSetForTransaction(sql, "t1")

            If docType = "ir" Then
                GridRelateIR.DataSource = ds
                GridRelateIR.DataBind()
            ElseIf docType = "cfb" Then
                GridRelateCFB.DataSource = ds
                GridRelateCFB.DataBind()
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTQMGridCause()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_tqm_cause_list WHERE ir_id = " & irId
            sql &= " ORDER BY order_sort"
            ds = conn.getDataSetForTransaction(sql, "t1")

            GridTQMCause.DataSource = ds
            GridTQMCause.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTQMTab()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_tqm_tab a "
            sql &= "LEFT OUTER JOIN  ir_topic_grand t1 ON a.grand_topic = t1.grand_topic_id "
            sql &= "LEFT OUTER JOIN ir_topic t2 ON a.topic = t2.ir_topic_id "
            sql &= "LEFT OUTER JOIN ir_topic_sub t3 ON a.subtopic1 = t3.ir_subtopic_id "
            sql &= "LEFT OUTER JOIN ir_topic_sub2 t4 ON a.subtopic2 = t4.ir_subtopic2_id "
            sql &= " LEFT OUTER JOIN ir_topic_sub3 t5 ON a.subtopic3 = t5.ir_subtopic3_id "
            sql &= " WHERE a.ir_id = " & irId

            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                'txttqm_doctor.SelectedValue = ds.Tables("t1").Rows(0)("physician_id").ToString
                txtgrandtopic.Text = ds.Tables("t1").Rows(0)("grand_topic_name").ToString

                '  bindComboNormalTopic()
                txtnormaltopic.Text = ds.Tables("t1").Rows(0)("topic_name").ToString
                ' bindComboSubTopic("")
                ' bindComboSubTopic("2")
                txtsubtopic1.Text = ds.Tables("t1").Rows(0)("subtopic_name").ToString
                'bindComboSubTopic2("")
                txtsubtopic2.Text = ds.Tables("t1").Rows(0)("subtopic2_name_en").ToString
                'bindComboSubTopic3("")
                txtsubtopic3.Text = ds.Tables("t1").Rows(0)("subtopic3_name_en").ToString

                txttqm_detail.Text = ds.Tables("t1").Rows(0)("tqm_topic_detail").ToString
                txtcase_owner.Text = ds.Tables("t1").Rows(0)("tqm_case_owner").ToString
                ' txtcause.SelectedValue = ds.Tables("t1").Rows(0)("ir_type_no").ToString
                '  txtcause_detail.Value = ds.Tables("t1").Rows(0)("incident_detail").ToString
                txttqm_remark.Text = ds.Tables("t1").Rows(0)("tqm_remark").ToString
                txtaction_tqm_detail.Text = ds.Tables("t1").Rows(0)("action_detail").ToString

                If ds.Tables("t1").Rows(0)("severe_level_id").ToString = "1" Then
                    lblLevel.text = "Insignificant"

                ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "2" Then
                    lblLevel.text = "Near miss"
                    ' txtsevere_level2.Checked = True
                ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "3" Then
                    ' txtsevere_level3.Checked = True
                    lblLevel.text = "No harm"
                ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "4" Then
                    ' txtsevere_level4.Checked = True
                    lblLevel.text = "Mild Adverse Event"
                ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "5" Then
                    'txtsevere_level5.Checked = True
                    lblLevel.text = "Moderate Adverse Event"
                ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "6" Then
                    'txtsevere_level6.Checked = True
                    lblLevel.text = "Serious Adverse Event"
                ElseIf ds.Tables("t1").Rows(0)("severe_level_id").ToString = "7" Then
                    ' txtsevere_level7.Checked = True
                    lblLevel.text = "Sentinel event"
                Else
                    lblLevel.text = "None"
                End If


                If ds.Tables("t1").Rows(0)("is_concern").ToString = "1" Then
                    txttqmconcern1.Text = "Yes"
                Else
                    txttqmconcern1.Text = "No"
                End If

                If ds.Tables("t1").Rows(0)("is_refer").ToString = "1" Then
                    txttqmrefer1.Text = "Yes"
                Else
                    txttqmrefer1.Text = "No"
                End If

                ' txtir_cfb.Text = ds.Tables("t1").Rows(0)("relate_cfb_no").ToString
                '  txtrelate_ir.Text = ds.Tables("t1").Rows(0)("relate_ir_no").ToString

                txtlog_safety.Text = ds.Tables("t1").Rows(0)("log_safety_goal").ToString
                txtlog_safety2.Text = ds.Tables("t1").Rows(0)("log_safety_goal2").ToString
                txtlog_lab.Text = ds.Tables("t1").Rows(0)("log_lab_name").ToString
                txtlog_asa.Text = ds.Tables("t1").Rows(0)("log_asa_name").ToString
                txtrepeatIR.Text = ds.Tables("t1").Rows(0)("repeat_ir_no").ToString
                Try
                    txtreporttype.Text = ds.Tables("t1").Rows(0)("tqm_report_type_name").ToString
                Catch ex As Exception

                End Try


            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub
End Class
