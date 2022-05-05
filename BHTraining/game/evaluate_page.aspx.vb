Imports System.IO
Imports System.Data
Imports ShareFunction
Imports System.Random

Partial Class game_evaluate_page
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected dsQuestion As New DataSet
    Protected tid As String = ""
    Protected order As String = "0" ' answer order
    Protected q_order As String = "0" ' question order
    Protected gid As String = ""
    Protected qid As String = "0"
    Protected sound_correct(9) As String
    Protected sound_wrong(3) As String
    Protected question_text As String = ""
    Protected lang As String = "th"

    Protected num As String = "0" ' Number of questions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("jci_emp_code")) Then
            Response.Redirect("multiple-choice-login.aspx")
            'Response.Write("Please re-login again")
            Response.End()
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If
        ' Response.End()
        tid = Request.QueryString("tid")
        order = CInt(Request.QueryString("order")) + 1
        q_order = CInt(Request.QueryString("q_order")) + 1
        gid = Request.QueryString("gid")
        lang = Request.QueryString("lang")

        If lang = "" Then
            lang = "th"
        End If

        If gid = "" Then
            Response.Write("Please select group")
            Return
        End If

        If IsPostBack Then

        Else ' First time load
            '  bindGridTest()

        End If


        bindSound()
        bindQuestionAndGroup()
        bindAnswer()
        '  bindQuestion()
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

            '   dsQuestion.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Public Function RandomInteger(ByVal LowerBoundary As Integer, ByVal UpperBoundary As Integer) As Integer

        Randomize()    'Initialize the random-number generator
        RandomInteger = (Rnd() * (UpperBoundary - LowerBoundary)) + LowerBoundary

    End Function

    Sub bindSound()

        sound_correct(0) = "Chailaew.mp3"
        sound_correct(1) = "Deemark.mp3"
        sound_correct(2) = "Excellent.mp3"
        sound_correct(3) = "Good.mp3"
        sound_correct(4) = "Great.mp3"
        sound_correct(5) = "Kengmak.mp3"
        sound_correct(6) = "Tooktong.mp3"
        sound_correct(7) = "Welldone.mp3"
        sound_correct(8) = "Yiam.mp3"
        sound_correct(9) = "Chailaew.mp3"

        sound_wrong(0) = "Payayam.mp3"
        sound_wrong(1) = "PayayamIkkrung.mp3"
        sound_wrong(2) = "TryAgain.mp3"
        sound_wrong(3) = "Payayam.mp3"
    End Sub



    Sub bindQuestionAndGroup()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ("
            sql &= "SELECT ROW_NUMBER() OVER(ORDER BY a.question_id ASC) as rownum , a.* "
            sql &= " FROM jci_master_question a  "
            sql &= " INNER JOIN jci_master_test c ON a.test_id = c.test_id AND c.is_game = 1 AND ISNULL(a.is_delete,0) = 0 "
            sql &= " WHERE a.group_id = " & gid & "  "
            sql &= " AND a.question_id IN " & Session("question_index").ToString
            sql &= " ) aa WHERE rownum = " & q_order
            sql &= " ORDER BY aa.question_id ASC "
            ' Response.Write(sql)
            'Response.End()
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count > 0 Then
                qid = ds.Tables("t1").Rows(0)("question_id").ToString
                question_text = ds.Tables("t1").Rows(0)("question_detail_" & lang).ToString
            Else

            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindAnswer()
        Dim sql As String
        '  Dim ds As New DataSet

        Try


            sql = "SELECT * FROM ("
            sql &= "SELECT ROW_NUMBER() OVER(ORDER BY answer_id ASC) as rownum , b.* "
            sql &= " FROM jci_master_question a INNER JOIN jci_master_answer b ON a.question_id = b.question_id "
            sql &= " INNER JOIN jci_master_test c ON a.test_id = c.test_id AND c.is_game = 1 "
            sql &= " WHERE a.question_id = " & qid & " AND ISNULL(b.is_answer_delete,0) = 0 "
            sql &= " ) aa "
            ' Response.Write(sql)
            '  Response.End()
            dsQuestion = conn.getDataSetForTransaction(sql, "t1")

            If dsQuestion.Tables("t1").Rows.Count = 0 Then
                dsQuestion.Dispose()

                Response.Redirect("evaluate_over.aspx?gid=" & gid)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            '  ds.Dispose()
        End Try
    End Sub
End Class
