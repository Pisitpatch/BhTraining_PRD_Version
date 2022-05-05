Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_popup_commitee_score
    Inherits System.Web.UI.Page
    Protected session_id As String
    Protected mode As String = ""
    Protected id As String
    Protected cid As String
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        session_id = Session("session_myid").ToString
        mode = Request.QueryString("mode")
        id = Request.QueryString("id")
        cid = Request.QueryString("cid")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If IsPostBack Then

        Else ' First time
            txtjobtitle.Text = Session("job_title").ToString
            lblJobType.Text = Session("user_position").ToString
            txtname.Text = Session("user_fullname").ToString
            txtdeptname.Text = Session("dept_name").ToString
            txtdatetime.Text = Date.Now
            isHasRow("ssip_committee_tab")
            bindScore()
            bindForm()
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

        Catch ex As Exception
            '  Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub isHasRow(ByVal table As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""

        Try
            sql = "SELECT * FROM " & table & " WHERE comment_id = " & cid
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            If ds.Tables("t1").Rows.Count <= 0 Then
                sql = "INSERT INTO " & table & " (comment_id , ssip_id) VALUES( "
                sql &= "" & cid & " , "
                sql &= "" & id & ""
                sql &= ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & " : " & sql)
                End If
                conn.setDBCommit()
            End If

            ds.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try


    End Sub

    Sub bindForm()
        Dim ds As New DataSet
        Dim sql As String

        Try

            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM ssip_committee_tab a INNER JOIN ssip_manager_comment b ON a.comment_id = b.comment_id WHERE a.comment_id = " & cid

            'sql &= " ORDER BY dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(sql)
            txtanswer1.SelectedValue = ds.Tables("t1").Rows(0)("q1_type").ToString()
            txtmgr_reason1.Text = ds.Tables("t1").Rows(0)("q1_reason").ToString()

            txtanswer2.SelectedValue = ds.Tables("t1").Rows(0)("q2_type").ToString()
            txtmgr_reason2.Text = ds.Tables("t1").Rows(0)("q2_reason").ToString()

            txtscorename1.SelectedValue = ds.Tables("t1").Rows(0)("score1").ToString()
            txtscorename2.SelectedValue = ds.Tables("t1").Rows(0)("score2").ToString()
            txtscorename3.SelectedValue = ds.Tables("t1").Rows(0)("score3").ToString()
            txtscorename4.SelectedValue = ds.Tables("t1").Rows(0)("score4").ToString()
            txtscorename5.SelectedValue = ds.Tables("t1").Rows(0)("score5").ToString()

            txtscore1.Value = ds.Tables("t1").Rows(0)("score1").ToString()
            txtscore2.Value = ds.Tables("t1").Rows(0)("score2").ToString()
            txtscore3.Value = ds.Tables("t1").Rows(0)("score3").ToString()
            txtscore4.Value = ds.Tables("t1").Rows(0)("score4").ToString()
            txtscore5.Value = ds.Tables("t1").Rows(0)("score5").ToString()

            txtamount_old.Text = ds.Tables("t1").Rows(0)("amount_old").ToString()
            txtamount_new.Text = ds.Tables("t1").Rows(0)("amount_new").ToString()
            txtamount_cal.Text = ds.Tables("t1").Rows(0)("amount_cal").ToString()
            txtamount_save.Text = ds.Tables("t1").Rows(0)("amount_save").ToString()

            Try
                txtaward_scale.SelectedValue = ds.Tables("t1").Rows(0)("intang_award_scale_id").ToString()
            Catch ex As Exception

            End Try

          

            totalScore()

            lblResult.Text = getResultText()

            If ds.Tables("t1").Rows(0)("review_by_empcode").ToString() <> Session("emp_code").ToString Then
                'panel_score.Enabled = False
                readonlyControl(panel_score)
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindScore()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM ssip_m_score WHERE score_type = 1"
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtscorename1.DataSource = ds
            txtscorename1.DataBind()

            sql = "SELECT * FROM ssip_m_score WHERE score_type = 2"
            ds = conn.getDataSetForTransaction(sql, "t2")
            txtscorename2.DataSource = ds
            txtscorename2.DataBind()

            sql = "SELECT * FROM ssip_m_score WHERE score_type = 3"
            ds = conn.getDataSetForTransaction(sql, "t3")
            txtscorename3.DataSource = ds
            txtscorename3.DataBind()

            sql = "SELECT * FROM ssip_m_score WHERE score_type = 4"
            ds = conn.getDataSetForTransaction(sql, "t4")
            txtscorename4.DataSource = ds
            txtscorename4.DataBind()

            sql = "SELECT * FROM ssip_m_score WHERE score_type = 5"
            ds = conn.getDataSetForTransaction(sql, "t5")
            txtscorename5.DataSource = ds
            txtscorename5.DataBind()

            txtscorename1.Items.Insert(0, New ListItem("-- Please Select --", "0"))
            txtscorename2.Items.Insert(0, New ListItem("-- Please Select --", "0"))
            txtscorename3.Items.Insert(0, New ListItem("-- Please Select --", "0"))
            txtscorename4.Items.Insert(0, New ListItem("-- Please Select --", "0"))
            txtscorename5.Items.Insert(0, New ListItem("-- Please Select --", "0"))
        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub onSubmitScore(ByVal sc As DropDownList, ByVal txt As HtmlInputText)
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_m_score WHERE score_id = " & sc.SelectedValue
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                txt.Value = ds.Tables("t1").Rows(0)("score_value").ToString
            Else
                txt.Value = 0
            End If
            ' Response.Write(sql)
            totalScore()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub totalScore()
        Dim p1 As Integer = 0
        Dim p2 As Integer = 0
        Dim p3 As Integer = 0
        Dim p4 As Integer = 0
        Dim p5 As Integer = 0

        Try
            p1 = Convert.ToInt32(txtscore1.Value)
        Catch ex As Exception
            p1 = 0
        End Try

        Try
            p2 = Convert.ToInt32(txtscore2.Value)
        Catch ex As Exception
            p2 = 0
        End Try

        Try
            p3 = Convert.ToInt32(txtscore3.Value)
        Catch ex As Exception
            p3 = 0
        End Try

        Try
            p4 = Convert.ToInt32(txtscore4.Value)
        Catch ex As Exception
            p4 = 0
        End Try

        Try
            p5 = Convert.ToInt32(txtscore5.Value)
        Catch ex As Exception
            p5 = 0
        End Try

        txtsum.Text = p1 + p2 + p3 + p4 + p5

    End Sub

    Protected Sub txtscorename1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename1.SelectedIndexChanged
        onSubmitScore(txtscorename1, txtscore1)
    End Sub

    Protected Sub txtscorename2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename2.SelectedIndexChanged
        onSubmitScore(txtscorename2, txtscore2)
    End Sub

    Protected Sub txtscorename3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename3.SelectedIndexChanged
        onSubmitScore(txtscorename3, txtscore3)
    End Sub

    Protected Sub txtscorename4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename4.SelectedIndexChanged
        onSubmitScore(txtscorename4, txtscore4)
    End Sub

    Protected Sub txtscorename5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscorename5.SelectedIndexChanged
        onSubmitScore(txtscorename5, txtscore5)
    End Sub

    Function getResultText() As String
        Dim result As String
        Dim total As Integer = 0

        Try
            total = CInt(txtsum.Text)
        Catch ex As Exception
            total = 0
            Response.Write(ex.Message)
        End Try

        If total >= 34 And total < 45 Then
            result = "Moderate"
        ElseIf total >= 45 And total < 65 Then
            result = "Substantial"
        ElseIf total >= 65 And total < 80 Then
            result = "High"
        ElseIf total > 80 Then
            result = "Exceptional"
        Else
            result = "Trophy"
        End If

        Return result
    End Function

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE ssip_committee_tab SET q1_type = '" & txtanswer1.SelectedValue & "' "
            sql &= " , q1_reason = '" & addslashes(txtmgr_reason1.Text) & "' "
            sql &= " , q2_type = '" & txtanswer2.SelectedValue & "' "
            sql &= " , q2_reason = '" & addslashes(txtmgr_reason2.Text) & "' "
            If txtamount_old.Text = "" Then
                sql &= " , amount_old = null "
            Else
                sql &= " , amount_old = '" & (txtamount_old.Text) & "' "
            End If

            If txtamount_new.Text = "" Then
                sql &= " , amount_new = null "
            Else
                sql &= " , amount_new = '" & (txtamount_new.Text) & "' "
            End If

            If txtamount_cal.Text = "" Then
                sql &= " , amount_cal = null "
            Else
                sql &= " , amount_cal = '" & (txtamount_cal.Text) & "' "
            End If

            If txtamount_save.Text = "" Then
                sql &= " , amount_save = null "
            Else
                sql &= " , amount_save = '" & (txtamount_save.Text) & "' "
            End If


            sql &= " , benefit1_id = '" & txtscorename1.SelectedValue & "' "
            sql &= " , benefit1_name = '" & addslashes(txtscorename1.SelectedItem.Text) & "' "
            sql &= " , score1 = '" & txtscore1.Value & "' "
           
            sql &= " , benefit2_id = '" & txtscorename2.SelectedValue & "' "
            sql &= " , benefit2_name = '" & addslashes(txtscorename2.SelectedItem.Text) & "' "
            sql &= " , score2 = '" & txtscore2.Value & "' "

            sql &= " , benefit3_id = '" & txtscorename3.SelectedValue & "' "
            sql &= " , benefit3_name = '" & addslashes(txtscorename3.SelectedItem.Text) & "' "
            sql &= " , score3 = '" & txtscore3.Value & "' "

            sql &= " , benefit4_id = '" & txtscorename4.SelectedValue & "' "
            sql &= " , benefit4_name = '" & addslashes(txtscorename4.SelectedItem.Text) & "' "
            sql &= " , score4 = '" & txtscore4.Value & "' "

            sql &= " , benefit5_id = '" & txtscorename5.SelectedValue & "' "
            sql &= " , benefit5_name = '" & addslashes(txtscorename5.SelectedItem.Text) & "' "
            sql &= " , score5 = '" & txtscore5.Value & "' "

            sql &= " , total_score = '" & txtsum.Text & "' "
            sql &= " , result_text = '" & getResultText() & "' "
            sql &= " , last_update = GETDATE() "

            sql &= " , intang_award_scale_id = '" & txtaward_scale.SelectedValue & "' "
            If txtaward_scale.SelectedValue = "" Then
                sql &= " , intang_award_scale_name = '-' "
            Else
                sql &= " , intang_award_scale_name = '" & txtaward_scale.SelectedItem.Text & "' "
            End If



            sql &= " WHERE comment_id = " & cid

            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If

            conn.setDBCommit()

            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; window.close();"
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub calCommitteeTang()
        Try
            txtamount_save.Text = CDbl(txtamount_old.Text) - CDbl(txtamount_new.Text)
        Catch ex As Exception
            txtamount_save.Text = ""
        End Try

    End Sub

    Protected Sub cmdCal11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCal11.Click
        calCommitteeTang()
    End Sub
End Class
