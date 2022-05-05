Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci_jci_select_question
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected lang As String = "th"
    Dim qid As String = ""
    Dim gid As String = ""
    Protected isEvaluate As Boolean = False
    Protected q1_th As String = ""
    Protected q1_en As String = ""
    Protected q2_th As String = ""
    Protected q2_en As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNothing(Session("jci_emp_code")) Then
            Response.Redirect("login.aspx")
            Response.End()
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        qid = Request.QueryString("qid")
        gid = Request.QueryString("gid")
        ' Response.Write(Session("lang").ToString)

      If Request.QueryString("lang") = "" Then
            lang = "th"
        Else
            lang = Request.QueryString("lang")
        End If


        ' Session("lang") = lang
        'lang = Session("lang").ToString

        q1_th = "ท่านสามารถเรียนรู้เนื้อหาและสามารถนำไปปฏิบัติใช้ได้จริงในลักษณะการอบรมรูปแบบนี้มากน้อยเพียงใด? (5 = มากที่สุด 1 = น้อยที่สุด)"
        q1_en = "Do you think that  the knowledge that you obtained from this training can be concrete  implemented ?"
        q2_th = "การเรียนรู้ด้วยตนเอง สามารถทำให้ท่านมีความรู้ความเข้าใจมากขึ้น"
        q2_en = "You can obtain more knowledge and better understanding by self-studying. "


        'Response.Write(Session("lang").ToString)
        isEvaluate = isSubmitEvaluateForm()
        bindForm()
        If isEvaluate = True Then
            panel_eval.enabled = False
        End If
        bindGroup()
        bindQuestion()

        If IsPostBack Then

        Else ' First time load
           

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

    Sub bindForm()
        If lang = "th" Then
            lblE1.Text = q1_th
            lblE2.Text = q2_th
        Else
            lblE1.Text = q1_en
            lblE2.Text = q2_en
        End If

        If isEvaluate = True Then
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM jci_evaluate_trans_list WHERE  eval_emp_code = " & Session("jci_emp_code").ToString
                ds = conn.getDataSetForTransaction(sql, "t1")
                txtanswer1.SelectedValue = ds.Tables("t1").Rows(0)("eval_ans1").ToString
                txtanswer2.SelectedValue = ds.Tables("t1").Rows(0)("eval_ans2").ToString
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Function isSubmitEvaluateForm() As Boolean
        Dim sql As String
        Dim ds As New DataSet
        Dim result As Boolean = False

        Try
            sql = "SELECT * FROM jci_evaluate_trans_list WHERE eval_emp_code = " & Session("jci_emp_code").ToString
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Response.Write("isSubmitEvaluateForm " & ex.Message)
        Finally
            ds.Dispose()
        End Try


        Return result
    End Function

    Sub bindGroup()
        Dim sql As String
        Dim ds As New dataset
        Dim thisClass As String = ""
        Dim aActive As String = ""
        Dim langType As String = "3"
        Dim strClientIP As String
        strClientIP = Request.UserHostAddress()

        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id  WHERE b.is_active = 1 AND ISNULL(b.is_game,0) = 0 "
            sql &= " AND (group_fix_ip = '" & strClientIP & "' OR group_fix_ip = null  OR group_fix_ip = '')"
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblGroupList.Text = ""

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                langType = ds.Tables("t1").Rows(i)("lang_type").ToString
                If i = 0 And gid = "" Then
                    gid = ds.Tables("t1").Rows(i)("group_id").ToString
                End If

                If gid = ds.Tables("t1").Rows(i)("group_id").ToString Then
                    thisClass = "questionBg"
                    aActive = "class = 'active'"
                Else
                    thisClass = "questionBg"
                    aActive = ""
                End If
                lblGroupList.Text &= "<div class='" & thisClass & "'><a " & aActive & " href='jci_select_question.aspx?gid=" & ds.Tables("t1").Rows(i)("group_id").ToString & "'>" & ds.Tables("t1").Rows(i)("group_name_" & lang) & "</a></div>" & vbCrLf
            Next i

           

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindQuestion()
        Dim sql As String
        Dim ds As New DataSet
        Dim header As String = ""
        Try
            sql = "SELECT a.* ,  ISNULL(b.question_id,0) AS q_num , b.trans_create_date , ISNULL(b.trans_id,0) AS trans_id FROM jci_master_question a "
            sql &= " LEFT OUTER JOIN jci_trans_list b ON a.question_id = b.question_id and b.trans_create_by_emp_code = " & Session("jci_emp_code").ToString & " WHERE a.is_active = 1 AND ISNULL(a.is_delete,0) = 0 "
            If gid <> "" Then
                sql &= " AND a.group_id = " & gid
            Else
                sql &= " AND 1 > 2 "
            End If

            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblQuestionList.Text = ""

            If lang = "th" Then
                header = "ซองคำถามที่"
            Else
                header = "Question"
            End If
            '  Response.Write("xxx " & Session("lang").ToString & "<br/>")
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
             

                If qid = ds.Tables("t1").Rows(i)("question_id").ToString Then ' Selected question id

                    lblQuestionList.Text &= " <div class='mediacontainer'><div class='questionBgGrey'><a href='jci_select_question.aspx?qid=" & _
                 ds.Tables("t1").Rows(i)("question_id").ToString & "&gid=" & ds.Tables("t1").Rows(i)("group_id").ToString & "&lang=" & lang & "#q" & ds.Tables("t1").Rows(i)("question_id").ToString & "' name='q" & ds.Tables("t1").Rows(i)("question_id").ToString & "'>" & _
                 "<img src='../images/email.png' alt='Question' /> " & header & " " & (i + 1) & " ? :: """ & ds.Tables("t1").Rows(i)("question_detail_" & lang).ToString & """</a></div>"

                    Session("question_id") = ds.Tables("t1").Rows(i)("question_id").ToString
                    Session("trans_id") = ds.Tables("t1").Rows(i)("trans_id").ToString

                    If ds.Tables("t1").Rows(i)("q_num").ToString <> "0" Then ' Already answered
                        ' lblQuestionList.Text &= "<span style='color:red;font-weight:bold'>You have already submitted answer at " & ds.Tables("t1").Rows(i)("trans_create_date").ToString & "</span>"
                        ' lblQuestionList.Text &= "<br/><a href='jci_evaluate_form.aspx' style='text-decoration:underline'>Please click this link to submit satisfaction evaluation form</a>"
                    Else
                        lblQuestionList.Text &= "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' width='800' height='400' id='webrecord_bh' align='middle'>"

                    End If

                    lblQuestionList.Text &= "<param name='allowScriptAccess' value='sameDomain' />"
                    lblQuestionList.Text &= "<param name='allowFullScreen' value='false' />"
                    lblQuestionList.Text &= "<param name='movie' value='webrecord_bh.swf' />"
                    lblQuestionList.Text &= "<param name='quality' value='high' /><param name='bgcolor' value='#333333' />"
                    lblQuestionList.Text &= "<embed src='webrecord_bh.swf' quality='high' bgcolor='#333333' width='800' height='400' name='webrecord_bh' align='middle' allowScriptAccess='sameDomain' allowFullScreen='false' type='application/x-shockwave-flash' pluginspage='http://www.adobe.com/go/getflashplayer' />"
                    lblQuestionList.Text &= "</object>"
                    '  lblQuestionList.Text &= "<div class='media'><img src='../images/bhtraining.gif' width='772' height='385' /></div>"
                    ' lblQuestionList.Text &= "<br/><input type='button' style='font-weight:bold' value='Confirm/Submit your Answer' onclick='return submitAnswer(" & qid & ")' /><br/>"
                Else
                    lblQuestionList.Text &= " <div class='mediacontainer'><div class='questionBgGrey'><a href='jci_select_question.aspx?qid=" & _
                 ds.Tables("t1").Rows(i)("question_id").ToString & "&gid=" & ds.Tables("t1").Rows(i)("group_id").ToString & "&lang=" & lang & "#q" & ds.Tables("t1").Rows(i)("question_id").ToString & "' name='q" & ds.Tables("t1").Rows(i)("question_id").ToString & "'>" & _
                 header & " " & (i + 1) & "</a></div>"
                End If

                lblQuestionList.Text &= "</div>"


            Next i

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

  

    Protected Sub cmdSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Try
            pk = getPK("evaluate_id", "jci_evaluate_trans_list", conn)
            sql = "INSERT INTO jci_evaluate_trans_list (evaluate_id , eval_emp_code , eval_qiuest1_th , eval_qiuest1_en "
            sql &= " , eval_ans1 , eval_qiuest2_th , eval_qiuest2_en , eval_ans2 , create_date , create_date_ts"
            sql &= ") VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & Session("jci_emp_code").ToString & "' ,"
            sql &= " '" & q1_th & "' ,"
            sql &= " '" & q1_en & "' ,"
            sql &= " '" & txtanswer1.SelectedValue & "' ,"
            sql &= " '" & q2_th & "' ,"
            sql &= " '" & q2_en & "' ,"
            sql &= " '" & txtanswer2.SelectedValue & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        End Try

        Response.Redirect("jci_select_question.aspx")
    End Sub
End Class
