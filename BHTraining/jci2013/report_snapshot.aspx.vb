Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class jci2013_report_snapshot
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected global_unit_num As String = ""
    Protected global_stack_title As String = ""
    Protected global_stack_fully As String = ""
    Protected global_stack_partial As String = ""
    Protected global_stack_notmed As String = ""
    Protected global_submit_date As String = ""

    Dim assessment_id As Integer = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If



        If Request.QueryString("assessment_id") Is Nothing Then
            assessment_id = 0
        Else
            ' assessment_id = Request.QueryString("assessment_id")

            Try
                Integer.TryParse(Request.QueryString("assessment_id"), assessment_id)
            Catch ex As Exception
                Response.Write(ex.Message)
                assessment_id = 0
            End Try

        End If

        If Not Page.IsPostBack Then
            If assessment_id > 0 Then
                bindAssessment()
                bindReport1()
                bindReport2()

            End If
          
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

    Sub bindHeader()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql = "select * FROM jci13_assessment_list "
            sql &= " where 1 = 1 "
            If assessment_id > 0 Then
                sql &= " AND assessment_id = " & assessment_id
            End If
       

            ds = conn.getDataSetForTransaction(sql, "t1")

            global_submit_date = ds.Tables("t1").Rows(0)("assessment_date_str")
         

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReport1()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql = "select case when me_score_level = 10 Then 'Fully Met' when me_score_level = 5 then 'Partial Met' else 'Not Met' end as score"
            sql &= " , count(me_score_level) as num"
            sql &= " from jci13_assessment_me_list a "
            sql &= " INNER JOIN jci13_assessment_list b ON a.assessment_id = b.assessment_id AND ISNULL(b.is_assessment_delete,0) = 0 "
            sql &= " INNER JOIN jci13_form_list c ON b.form_id = c.form_id AND ISNULL(c.is_form_delete,0) = 0 "
            sql &= " INNER JOIN jci13_std_select d ON a.me_id = d.me_id AND ISNULL(d.is_std_select_delete,0) = 0 "
            sql &= " where me_score_level >= 0 "
            If assessment_id > 0 Then
                sql &= " AND a.assessment_id = " & assessment_id
            End If

            sql &= " group by me_score_level"
            sql &= " ORDER BY me_score_level DESC"

            ds = conn.getDataSetForTransaction(sql, "t1")

            'Response.Write(sql)
            global_unit_num = ""
            'global_unit_name = ""
            Dim limit As String = ""
            For ii As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If ii = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If

                global_unit_num &= limit & "['" & ds.Tables("t1").Rows(ii)("score").ToString & " [" & ds.Tables("t1").Rows(ii)("num").ToString & "ME]<br/>' , " & ds.Tables("t1").Rows(ii)("num").ToString & " ]"
            Next

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindReport2() ' Stack Graph
        Dim sql As String = ""
        Dim ds As New DataSet
        Dim ds2 As New DataSet
        Try
            ' เรียงชื่อ chaper ที่ที fully med จากมากไปน้อย
            sql = "select b.chapter, case when me_score_level = 10 Then 'Fully met' when me_score_level = 5 then 'Partial met' else 'Not med' end as score"
            sql &= " , count(me_score_level) as num"
            sql &= " , (select count(me_score_level)  from jci13_assessment_me_list aa"
            sql &= " INNER JOIN jci13_std_select bb ON aa.me_id = bb.me_id AND ISNULL(bb.is_std_select_delete,0) = 0 "
            sql &= " INNER JOIN jci13_assessment_list  cc ON aa.assessment_id = cc.assessment_id AND ISNULL(cc.is_assessment_delete,0) = 0  "
            sql &= "  where  1 = 1 "
            If assessment_id > 0 Then
                sql &= " AND aa.assessment_id = " & assessment_id
            End If
            sql &= " AND me_score_level >= 0  and chapter = b.chapter ) AS totalnum "
            sql &= " from jci13_assessment_me_list a INNER JOIN jci13_std_select b ON a.me_id = b.me_id AND ISNULL(b.is_std_select_delete,0) = 0"
            sql &= " INNER JOIN jci13_assessment_list c ON a.assessment_id = c.assessment_id AND ISNULL(c.is_assessment_delete,0) = 0 "
            sql &= " INNER JOIN jci13_form_list d ON c.form_id = d.form_id AND ISNULL(d.is_form_delete,0) = 0 "
            sql &= " where me_score_level = 10 "
            If assessment_id > 0 Then
                sql &= " AND a.assessment_id = " & assessment_id
            End If
            sql &= " group by chapter , me_score_level"
            sql &= " ORDER BY  count(me_score_level)*100 / "
            sql &= " (select count(me_score_level)  from jci13_assessment_me_list aa"
            sql &= " INNER JOIN jci13_std_select bb ON aa.me_id = bb.me_id AND ISNULL(bb.is_std_select_delete,0) = 0 "
            sql &= " INNER JOIN jci13_assessment_list  cc ON aa.assessment_id = cc.assessment_id AND ISNULL(cc.is_assessment_delete,0) = 0  "
            sql &= "  where  1 = 1 "
            If assessment_id > 0 Then
                sql &= " AND aa.assessment_id = " & assessment_id
            End If
            sql &= " AND me_score_level >= 0  and chapter = b.chapter ) "
            sql &= " DESC "
            ds = conn.getDataSetForTransaction(sql, "t1")

            'Response.Write(sql)
            global_stack_fully = "["
            global_stack_partial = "["
            global_stack_notmed = "["
            Dim limit As String = ""
            global_stack_title = "["

            For ii As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                If ii = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If

                global_stack_title &= limit & "'" & ds.Tables("t1").Rows(ii)("chapter").ToString & "<br/>[" & ds.Tables("t1").Rows(ii)("totalnum").ToString & "]'"

                sql = "select b.chapter, case when me_score_level = 10 Then 'Fully met' when me_score_level = 5 then 'Partial met' else 'Not med' end as score"
                sql &= " , count(me_score_level) as num , me_score_level"

                sql &= " , (select count(me_score_level)  from jci13_assessment_me_list aa"
                sql &= " INNER JOIN jci13_std_select bb ON aa.me_id = bb.me_id"
                sql &= "  inner join jci13_assessment_list cc ON aa.assessment_id = cc.assessment_id AND ISNULL(cc.is_assessment_delete,0) = 0  "
                sql &= "  where  chapter = '" & ds.Tables("t1").Rows(ii)("chapter").ToString & "' "
                If assessment_id > 0 Then
                    sql &= " AND aa.assessment_id = " & assessment_id
                End If
                sql &= " AND me_score_level >= 0  ) AS totalnum "

                sql &= " from jci13_assessment_me_list a INNER JOIN jci13_std_select b ON a.me_id = b.me_id"
                sql &= " "

                sql &= "  inner join jci13_assessment_list c ON a.assessment_id = c.assessment_id AND ISNULL(c.is_assessment_delete,0) = 0  "

                sql &= " where   me_score_level >= 0 AND chapter = '" & ds.Tables("t1").Rows(ii)("chapter").ToString & "'"
                If assessment_id > 0 Then
                    sql &= " AND a.assessment_id = " & assessment_id
                End If
                sql &= " group by chapter , me_score_level"
                sql &= " order by chapter , me_score_level DESC"

                ds2 = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                For isub As Integer = 0 To 2

                    If isub = 0 Then

                        If global_stack_fully = "[" Then
                            limit = ""
                        Else
                            limit = ","
                        End If

                        If ds2.Tables("t1").Rows.Count < 1 Then
                            global_stack_fully &= limit & "0"
                        Else
                            If CInt(ds2.Tables("t1").Rows(isub)("me_score_level").ToString) = 10 Then
                                global_stack_fully &= limit & "" & Math.Round(CInt(ds2.Tables("t1").Rows(isub)("num").ToString) / CInt(ds2.Tables("t1").Rows(isub)("totalnum").ToString) * 100, 1)
                            Else
                                global_stack_fully &= limit & "0"
                            End If
                        End If

                    ElseIf isub = 1 Then ' Partial med

                        If ds2.Tables("t1").Rows.Count < 2 Then

                            Dim haveFullMedBefore As Boolean = False
                            For ss As Integer = 0 To ds2.Tables("t1").Rows.Count - 1
                                If CInt(ds2.Tables("t1").Rows(ss)("me_score_level").ToString) = 5 Then
                                    haveFullMedBefore = True
                                End If

                            Next ss

                            If Not haveFullMedBefore Then
                                global_stack_partial &= limit & "0"
                            End If

                            'global_stack_partial &= limit & "0"
                        Else
                            If CInt(ds2.Tables("t1").Rows(isub)("me_score_level").ToString) = 5 Then
                                global_stack_partial &= limit & "" & Math.Round(CInt(ds2.Tables("t1").Rows(isub)("num").ToString) / CInt(ds2.Tables("t1").Rows(isub)("totalnum").ToString) * 100, 1)
                            Else
                                global_stack_partial &= limit & "0"
                            End If

                            If CInt(ds2.Tables("t1").Rows(isub)("me_score_level").ToString) = 0 Then
                                global_stack_notmed &= limit & "" & Math.Round(CInt(ds2.Tables("t1").Rows(isub)("num").ToString) / CInt(ds2.Tables("t1").Rows(isub)("totalnum").ToString) * 100, 1)
                            End If
                        End If

                    ElseIf isub = 2 Then ' Not med

                        If ds2.Tables("t1").Rows.Count < 3 Then

                            Dim haveNotMedBefore As Boolean = False
                            For ss As Integer = 0 To ds2.Tables("t1").Rows.Count - 1
                                If CInt(ds2.Tables("t1").Rows(ss)("me_score_level").ToString) = 0 Then
                                    haveNotMedBefore = True
                                End If

                            Next ss

                            If Not haveNotMedBefore Then
                                global_stack_notmed &= limit & "0"
                            End If

                        Else
                            If CInt(ds2.Tables("t1").Rows(isub)("me_score_level").ToString) = 0 Then
                                global_stack_notmed &= limit & "" & Math.Round(CInt(ds2.Tables("t1").Rows(isub)("num").ToString) / CInt(ds2.Tables("t1").Rows(isub)("totalnum").ToString) * 100, 1)
                            Else
                                global_stack_notmed &= limit & "0"
                            End If
                        End If

                    End If

                Next isub

            Next ii

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


    Sub bindAssessment()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT assessment_id , (b.form_name + ', ' + assessment_date_str + ' ' + assessment_time_str + ' [' + emp_name + ', ' + location_name +']' ) AS name , assessment_date_str , CONVERT(VARCHAR(10),a.create_date_raw,103) AS create_date_raw FROM jci13_assessment_list a  inner join jci13_form_list b on a.form_id = b.form_id "
            sql &= " WHERE ISNULL(a.is_assessment_delete,0) = 0 AND ISNULL(b.is_form_delete,0) = 0 "
            sql &= " AND assessment_id= " & assessment_id
            sql &= " ORDER BY b.form_name , assessment_date_str , assessment_time_str "

            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            lblHeader.Text = ds.Tables("t1").Rows(0)("name").ToString
            global_submit_date = ds.Tables("t1").Rows(0)("create_date_raw")
        Catch ex As Exception
            lblHeader.Text = ""
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
