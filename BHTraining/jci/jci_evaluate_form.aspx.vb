Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci_jci_evaluate_form
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected lang As String = "th"
    Protected q1_th As String = ""
    Protected q1_en As String = ""
    Protected q2_th As String = ""
    Protected q2_en As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("jci_emp_code")) Then
            Response.Redirect("login.aspx")
            Response.End()
        End If

        lang = Session("lang").ToString

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        q1_th = "ท่านสามารถเรียนรู้เนื้อหาและสามารถนำไปปฏิบัติใช้ได้จริงในลักษณะการอบรมรูปแบบนี้มากน้อยเพียงใด? (5 = มากที่สุด 1 = น้อยที่สุด)"
        q1_en = "Do you think that  the knowledge that you obtained from this training can be concrete  implemented ?"
        q2_th = "การเรียนรู้ด้วยตนเอง สามารถทำให้ท่านมีความรู้ความเข้าใจมากขึ้น"
        q2_en = "You can obtain more knowledge and better understanding by self-studying. "

        bindForm()
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
    End Sub

    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Response.Redirect("jci_select_question.aspx")
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
        End Try
    End Sub
End Class
