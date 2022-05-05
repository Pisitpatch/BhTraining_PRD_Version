Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class jci2013_report_compare
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected global_unit_num1 As String = ""
    Protected global_submit_date1 As String = ""

    Protected global_unit_num2 As String = ""
    Protected global_submit_date2 As String = ""

    Protected global_unit_num3 As String = ""
    Protected global_submit_date3 As String = ""

    Protected assessment_id As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        assessment_id = Request.QueryString("id")

        If Not Page.IsPostBack Then
            bindReport1()
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


    Sub bindReport1()
        Dim sql As String = ""
        Dim ds As New DataSet
        Dim dsMain As New DataSet
        Dim form_id As String = "0"
        Try
            sql = "SELECT form_id FROM jci13_assessment_list WHERE assessment_id = " & assessment_id
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                form_id = ds.Tables("t1").Rows(0)(0).ToString
            End If

            sql = "SELECT  assessment_id , CONVERT(VARCHAR(10),a.create_date_raw,103) AS create_date_raw FROM jci13_assessment_list a  "
            If CInt(form_id) > 0 Then
                sql &= " WHERE form_id = " & form_id
            End If
            sql &= " ORDER BY assessment_id DESC"

            dsMain = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To dsMain.Tables("t1").Rows.Count - 1

                If i > 2 Then
                    Exit For
                End If

                sql = "select case when me_score_level = 10 Then 'Fully met' when me_score_level = 5 then 'Partial met' else 'not met' end as score"
                sql &= " , count(me_score_level) as num"
                sql &= " from jci13_assessment_me_list a "
                sql &= " INNER JOIN jci13_assessment_list b ON a.assessment_id = b.assessment_id AND ISNULL(b.is_assessment_delete,0) = 0 "
                sql &= " INNER JOIN jci13_form_list c ON b.form_id = c.form_id AND ISNULL(c.is_form_delete,0) = 0 "
                sql &= " INNER JOIN jci13_std_select d ON a.me_id = d.me_id AND ISNULL(d.is_std_select_delete,0) = 0 "
                sql &= " where me_score_level >= 0 "
                ' If txtassessment.SelectedIndex > 0 Then
                sql &= " AND a.assessment_id = " & dsMain.Tables("t1").Rows(i)("assessment_id").ToString
                ' End If

                sql &= " group by me_score_level"
                sql &= " ORDER BY me_score_level DESC"

                ds = conn.getDataSetForTransaction(sql, "t1")

                'Response.Write(sql)
                If i = 0 Then
                    global_unit_num1 = ""
                ElseIf i = 1 Then
                    global_unit_num2 = ""
                ElseIf i = 2 Then
                    global_unit_num3 = ""
                End If

                'global_unit_name = ""
                Dim limit As String = ""
                For ii As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    If ii = 0 Then
                        limit = ""
                    Else
                        limit = ","
                    End If

                    If i = 0 Then
                        global_submit_date1 = dsMain.Tables("t1").Rows(ii)("create_date_raw").ToString
                        global_unit_num1 &= limit & "['" & ds.Tables("t1").Rows(ii)("score").ToString & " [" & ds.Tables("t1").Rows(ii)("num").ToString & "ME]<br/>' , " & ds.Tables("t1").Rows(ii)("num").ToString & " ]"
                    ElseIf i = 1 Then
                        global_submit_date2 = dsMain.Tables("t1").Rows(ii)("create_date_raw").ToString
                        global_unit_num2 &= limit & "['" & ds.Tables("t1").Rows(ii)("score").ToString & " [" & ds.Tables("t1").Rows(ii)("num").ToString & "ME]<br/>' , " & ds.Tables("t1").Rows(ii)("num").ToString & " ]"
                    ElseIf i = 2 Then
                        global_submit_date3 = dsMain.Tables("t1").Rows(ii)("create_date_raw").ToString
                        global_unit_num3 &= limit & "['" & ds.Tables("t1").Rows(ii)("score").ToString & " [" & ds.Tables("t1").Rows(ii)("num").ToString & "ME]<br/>' , " & ds.Tables("t1").Rows(ii)("num").ToString & " ]"
                    End If

                Next
            Next i

         

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
            dsMain.Dispose()
        End Try
    End Sub

End Class
