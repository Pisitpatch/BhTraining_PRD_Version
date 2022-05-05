Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class jci2013_report_compare2
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected global_stack_title As String = ""
    Protected global_stack_fully As String = ""
    Protected global_stack_partial As String = ""
    Protected global_stack_notmed As String = ""
    Protected global_submit_date As String = ""

    Protected assessment_id As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        assessment_id = Request.QueryString("id")

        If Not Page.IsPostBack Then
            bindReport2()
        End If


    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

        Catch ex As Exception
            '  Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindReport2() ' Stack Graph
        Dim sql As String = ""
        Dim ds As New DataSet
        Dim ds2 As New DataSet
        Dim limit As String = ""
        Dim totalnum As Integer = 0
        Dim n1 As Integer = 0
        Dim n2 As Integer = 0
        Dim n3 As Integer = 0
        Dim form_id As String = "0"
        Dim sql_sub As String = ""
        Try
            sql = "SELECT form_id , assessment_id FROM jci13_assessment_list WHERE assessment_id = " & assessment_id

            'Response.Write(sql & "<br/>")
            ds2 = conn.getDataSetForTransaction(sql, "t1")
            If ds2.Tables("t1").Rows.Count > 0 Then
                form_id = ds2.Tables("t1").Rows(0)(0).ToString

                sql = "SELECT TOP 3 * , CONVERT(VARCHAR(10),create_date_raw,103) AS create_date_raw1  FROM jci13_assessment_list WHERE form_id = " & form_id
                sql &= " AND ISNULL(is_assessment_delete,0) = 0 ORDER BY assessment_id DESC"

                ds2 = conn.getDataSetForTransaction(sql, "t1")

                For i As Integer = 0 To ds2.Tables("t1").Rows.Count - 1
                    If i = 0 Then
                        limit = ""
                        global_submit_date = ds2.Tables("t1").Rows(i)("create_date_raw1").ToString
                    Else
                        limit = ","
                    End If

                    sql_sub &= limit & ds2.Tables("t1").Rows(i)("assessment_id").ToString
                Next i

            Else
                Return
            End If

            sql = " select  a2.chapter , a.assessment_id , isnull(max(b.full_med),0) as full_med , isnull(max(c.partial),0) as partial "
            sql &= " , isnull(max(d.not_met),0) as not_met"
            sql &= " from jci13_assessment_list a"
            sql &= " INNER JOIN jci13_assessment_me_list a1 ON a.assessment_id = a1.assessment_id"
            sql &= "  INNER JOIN jci13_std_select a2 ON a1.me_id = a2.me_id"

            sql &= " LEFT OUTER JOIN (select t1.assessment_id , t2.chapter , COUNT(assessment_id) as full_med from jci13_assessment_me_list t1"
            sql &= " inner join jci13_std_select t2 on t1.me_id = t2.me_id"
            sql &= " WHERE me_score_level = 10"
            sql &= " group by t1.assessment_id , t2.chapter) b on a.assessment_id = b.assessment_id and a2.chapter = b.chapter "


            sql &= " LEFT OUTER JOIN (select t1.assessment_id , t2.chapter , COUNT(assessment_id) as partial from jci13_assessment_me_list t1"
            sql &= " inner join jci13_std_select t2 on t1.me_id = t2.me_id"
            sql &= "  WHERE me_score_level = 5"
            sql &= " group by t1.assessment_id , t2.chapter) c on a.assessment_id = c.assessment_id and a2.chapter = c.chapter"

            sql &= " LEFT OUTER JOIN (select t1.assessment_id , t2.chapter , COUNT(assessment_id) as not_met from jci13_assessment_me_list t1"
            sql &= " inner join jci13_std_select t2 on t1.me_id = t2.me_id"
            sql &= " WHERE me_score_level = 0"
            sql &= " group by t1.assessment_id , t2.chapter) d on a.assessment_id = d.assessment_id and a2.chapter = d.chapter"

            ' sql &= " where a.assessment_id in (47,48,49)"
            If CInt(form_id) > 0 Then
                sql &= " WHERE a.assessment_id IN (" & sql_sub & ")"
            End If

            sql &= " group by a.assessment_id , a2.chapter"
            sql &= " order by a2.chapter , a.assessment_id asc"

            '  Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            global_stack_fully = "["
            global_stack_partial = "["
            global_stack_notmed = "["
            global_stack_title = "["

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If



                totalnum = 0
                n1 = 0
                n2 = 0
                n3 = 0

                Integer.TryParse(ds.Tables("t1").Rows(i)("full_med").ToString, n1)
                Integer.TryParse(ds.Tables("t1").Rows(i)("partial").ToString, n2)
                Integer.TryParse(ds.Tables("t1").Rows(i)("not_met").ToString, n3)

                totalnum = n1 + n2 + n3
                '  Response.Write(Math.Round((n1 / totalnum) * 100) & "<br/>")
                global_stack_fully &= limit & "" & Math.Round(((n1 / totalnum) * 100), 2) & ""
                global_stack_partial &= limit & "" & Math.Round(((n2 / totalnum) * 100), 2) & ""
                global_stack_notmed &= limit & "" & Math.Round(((n3 / totalnum) * 100), 2) & ""

                global_stack_title &= limit & "'" & ds.Tables("t1").Rows(i)("chapter").ToString & "<br/>[" & totalnum & "]'"

            Next i

            global_stack_title &= "]"
            global_stack_fully &= "]"
            global_stack_partial &= "]"
            global_stack_notmed &= "]"

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
            ds2.Dispose()
        End Try
    End Sub
End Class
