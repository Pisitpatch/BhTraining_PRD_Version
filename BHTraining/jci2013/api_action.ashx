<%@ WebHandler Language="VB" Class="api_action" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class api_action : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("id")
        Dim form_id As String = context.Request.QueryString("form_id")
        Dim std_id As String = context.Request.QueryString("std_id")
        Dim me_id As String = context.Request.QueryString("me_id")
        Dim assessment_id As String = context.Request.QueryString("assessment_id")
        Dim type As String = context.Request.QueryString("type")
        Dim flag As String = context.Request.QueryString("flag")
        Dim field As String = context.Request.QueryString("field")
        Dim score As String = context.Request.QueryString("score")
        Dim value As String = context.Request.Form("value")
        
        Dim deptname As String = context.Request.Form("deptname")
        Dim location As String = context.Request.Form("location")
        Dim typename As String = context.Request.Form("typename")
        Dim empname As String = context.Request.Form("empname")
        Dim empcode As String = context.Request.Form("empcode")
        Dim empcode2 As String = context.Request.QueryString("empcode2")
        Dim datestr As String = context.Request.Form("datestr")
        Dim timestr As String = context.Request.Form("timestr")
        Dim member As String = context.Request.Form("member")

        Dim filename As String = context.Request.Form("filename")
        
        Dim sql As String = ""
        Dim result As String = ""
        Dim ds As New DataSet
        Dim pk As String = ""
        Dim sb As New StringBuilder
        
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
          
            connection.Open()
               
            If type = "checkboxMe" Then
                
                If flag = "add" Then
                        
                    sql = "SELECT ISNULL(MAX(me_id),0) + 1 AS pk FROM jci13_std_select"

                    Using da1 As New SqlDataAdapter(sql, connection)
                        da1.Fill(ds, "t1")
                        pk = ds.Tables("t1").Rows(0)("pk").ToString
                        
                        sql = "INSERT INTO jci13_std_select (me_id , form_id , edition , section_no , chapter , std_no , measure_element_no , section_name , goal , std_detail , measure_element_detail , std_id , type , chapter_name) "
                        sql &= "SELECT " & pk & " , " & form_id & " , edition , section_no , chapter , std_no , measure_element_no  , section_name , goal , std_detail , measure_element_detail , std_id , type , chapter_name"
                        sql &= " FROM jci13_std_list WHERE std_id = " & std_id
                        
                        Using command As New SqlCommand(sql, connection)
                            command.ExecuteNonQuery()
                        End Using
                            
                    End Using
                    
                ElseIf flag = "delete" Then
                    
                    sql = "DELETE FROM jci13_std_select WHERE me_id = " & me_id
                    Using command As New SqlCommand(sql, connection)
                        command.ExecuteNonQuery()
                    End Using
                      
                End If
                
            ElseIf type = "saveCriteria" Then
                
                sql = "UPDATE jci13_std_select SET " & field & " = '" & addslashes(value) & "' WHERE me_id = " & me_id
                Using command As New SqlCommand(sql, connection)
                    command.ExecuteNonQuery()
                End Using
                
                'context.Response.Write(value)
            ElseIf type = "saveForm" Then
                
                    sql = "UPDATE jci13_form_list SET form_name = '" & addslashes(value) & "' WHERE form_id = " & form_id
                ' context.Response.Write(sql)
                    Using command As New SqlCommand(sql, connection)
                        command.ExecuteNonQuery()
                    End Using
                
            ElseIf type = "addForm" Then
                
                    sql = "SELECT ISNULL(MAX(form_id),0) + 1 AS pk FROM jci13_form_list"

                    Using da1 As New SqlDataAdapter(sql, connection)
                        da1.Fill(ds, "t1")
                        pk = ds.Tables("t1").Rows(0)("pk").ToString
                        
                        sql = "INSERT INTO jci13_form_list (form_id , form_name , is_form_active , lastupdate_raw , lastupdate_ts) VALUES("
                        sql &= " '" & pk & "' ,"
                        sql &= " '" & addslashes(value) & "' ,"
                        sql &= " 1 ,"
                        sql &= " GETDATE() , "
                        sql &= " '" & Date.Now.Ticks & "' "
                        sql &= ")"
                        Using command As New SqlCommand(sql, connection)
                            command.ExecuteNonQuery()
                        End Using
               
                            
                End Using
                
            ElseIf type = "getDepartment" Then
                
                sql = "SELECT dept_id , dept_name_en FROM user_dept WHERE ISNULL(dept_name_en,'') <> ''  ORDER BY dept_name_en"
               
                
                Using command As New SqlCommand(sql, connection)
                    
               
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                       
                            ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                            sb.Append(reader.GetInt32(0).ToString)
                            sb.Append("|")
                            sb.Append(reader.GetString(1).ToString)
                            sb.Append("|")
                            sb.Append("#")
                            
                            ' result &= reader.GetInt32(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                            ' result &= reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|"
                          
                            'result &= "#"
                        
                        End While
                    
                   
                        context.Response.Write(sb.ToString)
                    
                        reader.Close()
                        command.Dispose()
                        'context.Response.Write(sql)
                    End Using
                
                  
                End Using
                
            ElseIf type = "getLocation" Then
                
                sql = "SELECT location_id , location_name FROM jci13_m_location  ORDER BY location_name"
               
                
                Using command As New SqlCommand(sql, connection)
                    
               
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                       
                            ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                            sb.Append(reader.GetInt32(0).ToString)
                            sb.Append("|")
                            sb.Append(reader.GetString(1).ToString)
                            sb.Append("|")
                            sb.Append("#")
                            '  result &= reader.GetInt32(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                            '   result &= reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|"
                          
                            ' result &= "#"
                        
                        End While
                    
                   
                        context.Response.Write(sb.ToString)
                    
                        reader.Close()
                        command.Dispose()
                        
                        'context.Response.Write(sql)
                    End Using
                
                  
                End Using
                
            ElseIf type = "getType" Then
                
                sql = "SELECT type_id , type_name FROM jci13_m_type  ORDER BY type_name"
               
                
                Using command As New SqlCommand(sql, connection)
                    
               
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                       
                            ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                            sb.Append(reader.GetInt32(0).ToString)
                            sb.Append("|")
                            sb.Append(reader.GetString(1).ToString)
                            sb.Append("|")
                            sb.Append("#")
                            
                            ' result &= reader.GetInt32(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                            ' result &= reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|"
                          
                            'result &= "#"
                        
                        End While
                    
                   
                        context.Response.Write(sb.ToString)
                    
                        reader.Close()
                        command.Dispose()
                        'context.Response.Write(sql)
                    End Using
                
                  
                End Using
                
            ElseIf type = "getAssessment" Then
                
                sql = "SELECT form_id , assessment_date_str , assessment_time_str , location_name , assessment_id , type_id , location_id , dept_id , type_name , dept_name , member , score , rank FROM jci13_assessment_list WHERE form_id = " & form_id
                sql &= " AND ISNULL(is_assessment_delete,0) = 0 "
                If id <> "" Then
                    sql &= " AND assessment_id = " & id
                End If
                If empcode2 <> "" Then
                    sql &= " AND emp_code = " & empcode2
                End If
                Using command As New SqlCommand(sql, connection)
                    
               
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                       
                            ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                            result &= reader.GetInt32(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|" ' form_id
                            result &= reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|"
                            result &= reader.GetString(2).Replace("\", "").Replace(vbCrLf, "") & "|"
                            result &= reader.GetString(3).Replace("\", "").Replace(vbCrLf, "") & "|" ' location_name
                            result &= reader.GetInt32(4).ToString.Replace("\", "").Replace(vbCrLf, "") & "|" ' assessment_id
                            result &= reader.GetInt32(5).ToString.Replace("\", "").Replace(vbCrLf, "") & "|" ' type_id
                            result &= reader.GetInt32(6).ToString.Replace("\", "").Replace(vbCrLf, "") & "|" ' location_id
                            result &= reader.GetInt32(7).ToString.Replace("\", "").Replace(vbCrLf, "") & "|" ' dept_id
                            result &= reader.GetString(8).Replace("\", "").Replace(vbCrLf, "") & "|" ' type_name
                            result &= reader.GetString(9).Replace("\", "").Replace(vbCrLf, "") & "|" ' dept_name
                            If reader.IsDBNull(10) Then
                                result &= "" & "|" ' member
                            Else
                                result &= reader.GetString(10).Replace("\", "").Replace(vbCrLf, "") & "|" ' member
                            End If
                           
                            If reader.IsDBNull(11) Then
                                result &= "" & "|" ' score
                            Else
                                result &= reader.GetString(11).Replace("\", "").Replace(vbCrLf, "") & "|" ' score
                            End If

                            If reader.IsDBNull(12) Then
                                result &= "" & "|" ' rank
                            Else
                                result &= reader.GetString(12).Replace("\", "").Replace(vbCrLf, "") & "|" ' rank
                            End If

                            result &= "#"
                        
                        End While
                    
                   
                        context.Response.Write(result)
                    
                        reader.Close()
                        command.Dispose()
                        'context.Response.Write(sql)
                    End Using
                
                  
                End Using
                
            ElseIf type = "getMeAssessment" Then
                
                Dim ds2 As New DataSet
                
                sql = "SELECT * FROM jci13_std_select WHERE form_id = " & form_id
                sql &= " AND me_id NOT IN (select me_id from jci13_assessment_me_list where assessment_id = " & id & ")"

                Using da1 As New SqlDataAdapter(sql, connection)
                    da1.Fill(ds, "t1")
    
                End Using
                
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                    pk = getPK("", "", connection)
                        
                    sql = "INSERT INTO jci13_assessment_me_list (assessment_me_id , assessment_id , me_score_level , me_id )"
                    sql &= " VALUES( "
                    sql &= " '" & pk & "' , "
                    sql &= " '" & id & "' , "
                    sql &= " null , "
                    sql &= " '" & ds.Tables("t1").Rows(i)("me_id").ToString & "'  "
            
                    sql &= " )"
                    ' Response.Write(sql & "<br/>")
                    ' context.Response.Write(sql)
                    'Return
                    Using command As New SqlCommand(sql, connection)
                         command.ExecuteNonQuery()
                    End Using
                      
                Next i
                    
                ds2.Dispose()
                    
              
                sql = "SELECT  a.me_id , c.form_id , std_id , edition, section_no , section_name , chapter , chapter_name , std_no , std_detail , goal, type , criteria , method , measure_element_no , measure_element_detail , a.assessment_me_id , a.me_score_level , a.me_comment_detail , d.score , d.rank "

                sql &= " FROM jci13_assessment_me_list a INNER JOIN jci13_std_select b ON a.me_id = b.me_id "
                sql &= " INNER JOIN jci13_form_list c ON b.form_id = c.form_id "
                sql &= " INNER JOIN jci13_assessment_list d ON a.assessment_id = d.assessment_id "
                sql &= " WHERE b.form_id =  " & form_id
                sql &= " AND a.assessment_id = " & id
                
                Using command As New SqlCommand(sql, connection)
                    
               
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                       
                            ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                            result &= reader.GetSqlInt64(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                            result &= reader.GetInt32(1).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                            If reader.IsDBNull(2) Then
                                result &= "" & "|" ' method
                            Else
                                result &= reader.GetInt32(2).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                            End If
                        
                            result &= reader.GetString(3).Replace("\", "").Replace(vbCrLf, "") & "|"  ' edition
                            result &= reader.GetString(4).Replace("\", "").Replace(vbCrLf, "") & "|" ' section_no
                            result &= reader.GetString(5).Replace("\", "").Replace(vbCrLf, "") & "|" ' section_name
                            result &= reader.GetString(6).Replace("\", "").Replace(vbCrLf, "") & "|" ' chapter
                            result &= reader.GetString(7).Replace("\", "").Replace(vbCrLf, "") & "|" ' chapter_name
                            result &= reader.GetString(8).Replace("\", "").Replace(vbCrLf, "") & "|" ' std_no
                            result &= reader.GetString(9).Replace("\", "").Replace(vbCrLf, "") & "|" ' std_detail
                            result &= reader.GetString(10).Replace("\", "").Replace(vbCrLf, "") & "|" ' goal
                            result &= reader.GetString(11).Replace("\", "").Replace(vbCrLf, "") & "|" ' type
                      
                        
                            If reader.IsDBNull(12) Then
                                result &= "" & "|" ' criteria
                            Else
                                result &= reader.GetString(12).Replace("\", "").Replace(vbCrLf, "") & "|" ' criteria
                            End If
                        
                            If reader.IsDBNull(13) Then
                                result &= "" & "|" ' method
                            Else
                                result &= reader.GetString(13).Replace("\", "").Replace(vbCrLf, "") & "|" ' method
                            End If
                       
                            result &= reader.GetString(14).Replace("\", "").Replace(vbCrLf, "") & "|" ' measure_element_no
                            result &= reader.GetString(15).Replace("\", "").Replace(vbCrLf, "") & "|" ' measure_element_detail
                            result &= reader.GetSqlInt64(16).ToString.Replace("\", "").Replace(vbCrLf, "") & "|" ' assessment_me_id
                            
                            If reader.IsDBNull(17) Then
                                result &= "" & "|" ' me_score_level
                            Else
                                result &= reader.GetInt16(17).ToString.Replace("\", "").Replace(vbCrLf, "") & "|" 'me_score_level
                            End If
                            
                            If reader.IsDBNull(18) Then
                                result &= "" & "|" ' me_comment_detail
                            Else
                                result &= reader.GetString(18).Replace("\", "").Replace(vbCrLf, "") & "|" ' me_comment_detail
                            End If
                            
                            If reader.IsDBNull(19) Then
                                result &= "" & "|" ' score
                            Else
                                result &= reader.GetString(19).Replace("\", "").Replace(vbCrLf, "") & "|" ' score
                            End If

                            If reader.IsDBNull(20) Then
                                result &= "" & "|" ' rank
                            Else
                                result &= reader.GetString(20).Replace("\", "").Replace(vbCrLf, "") & "|" ' rank
                            End If
                           
                            result &= "#"
                        
                        End While
                    
                   
                        context.Response.Write(result)
                    
                        reader.Close()
                        command.Dispose()
                        'context.Response.Write(sql)
                    End Using
                
                  
                End Using
                
            ElseIf type = "saveAssessmentHeader" Then
                
             
                
                sql = "UPDATE jci13_assessment_list SET "
                sql &= " emp_name = '" & empname & "' "
                sql &= " , emp_code = '" & empcode & "' "
                sql &= " , assessment_date_str = '" & datestr & "' "
                sql &= " , assessment_time_str = '" & timestr & "' "

                '  sql &= " , dept_id = '" & txtdept.SelectedValue & "' "
                sql &= " , dept_name = '" & deptname & "' "
                ' sql &= " , location_id = '" & txtlocation.SelectedValue & "' "
                sql &= " , location_name = '" & location & "' "
                ' sql &= " , type_id = '" & txttype.SelectedValue & "' "
                sql &= " , type_name = '" & typename & "' "
                sql &= " , member = '" & member & "' "
                sql &= " WHERE assessment_id = " & id
                ' context.Response.Write(sql)
                Using command As New SqlCommand(sql, connection)
                    command.ExecuteNonQuery()
                End Using

            ElseIf type = "deleteAssessmentHeader" Then
                
             
                
                sql = "UPDATE jci13_assessment_list SET is_assessment_delete = 1 "
               
                sql &= " WHERE assessment_id = " & id
                ' context.Response.Write(sql)
                Using command As New SqlCommand(sql, connection)
                    command.ExecuteNonQuery()
                End Using
                
            ElseIf type = "addAssessmentHeader" Then
                
             
                sql = "SELECT ISNULL(MAX(assessment_id),0) + 1 AS pk FROM jci13_assessment_list"

                Using da1 As New SqlDataAdapter(sql, connection)
                    da1.Fill(ds, "t1")
                    pk = ds.Tables("t1").Rows(0)("pk").ToString
                        
                    sql = "INSERT INTO jci13_assessment_list (assessment_id , form_id , emp_name , emp_code , assessment_date_str , assessment_time_str , dept_id , dept_name , location_id , location_name , type_id , type_name "
                    sql &= " )VALUES("
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & form_id & "' ,"
                    sql &= " '" & empname & "' ,"
                    sql &= " '" & empcode & "' ,"
                    sql &= " '" & datestr & "' ,"
                    sql &= " '" & timestr & "' ,"
                    sql &= " '" & 0 & "' ,"
                    sql &= " '" & deptname & "' ,"
                    sql &= " '" & 0 & "' ,"
                    sql &= " '" & location & "' ,"
                    sql &= " '" & 0 & "' ,"
                    sql &= " '" & typename & "' "
                    sql &= " )"
                    Using command As New SqlCommand(sql, connection)
                        command.ExecuteNonQuery()
                    End Using
                    
                End Using
              
             
             
                
            ElseIf type = "saveAssessment" Then
                
                Dim newscore As Integer = -1
                Dim num As Integer = 0
                Dim sum_score As Integer = 0

                    If score = "0" Then
                        newscore = 10
                    ElseIf score = "1" Then
                        newscore = 5
                    ElseIf score = "2" Then
                        newscore = 0
                    ElseIf score = "3" Then
                        newscore = -1
                    Else
                        newscore = -1
                    End If
                
                    sql = "UPDATE jci13_assessment_me_list SET me_comment_detail = '" & addslashes(value) & "' "
                    sql &= " , me_score_level = '" & newscore & "' "
                    sql &= "WHERE assessment_me_id = " & id
                    ' context.Response.Write(sql)
                    Using command As New SqlCommand(sql, connection)
                        command.ExecuteNonQuery()
                    End Using
                    
            
                sql = "SELECT * FROM jci13_assessment_me_list WHERE assessment_id = " & assessment_id
                sql &= " AND (me_score_level is null or me_score_level >=0 )"
             '   context.Response.Write(sql)
                Using da1 As New SqlDataAdapter(sql, connection)
                    da1.Fill(ds, "t1")
                    num = ds.Tables(0).Rows.Count
                   ' context.Response.Write("num = " & num)
                    For i As Integer = 0 To num - 1
                        If (ds.Tables(0).Rows(i)("me_score_level").ToString <> "") Then
                            sum_score += CInt(ds.Tables(0).Rows(i)("me_score_level").ToString)
                        End If
                    Next i
                
                    Dim percent As Double = 0
                    Dim rank As String = ""
                    percent = Math.Round((sum_score / (num * 10)) * 100, 1)
                    '   context.Response.Write("score = " & sum_score )

                    If (percent > 97.5) Then
                        rank = "FE"
                    ElseIf (percent > 95) And (percent <= 97.5) Then
                        rank = "EE"
                    ElseIf percent = 95 Then
                        rank = "ME"
                    ElseIf (percent >= 90) And (percent < 95) Then
                        rank = "IN"
                    ElseIf percent < 90 Then
                        rank = "UN"
                    End If
                    sql = "UPDATE jci13_assessment_list SET score = '" & percent & "' "
                    sql &= " , rank = '" & rank & "' "
                    sql &= "WHERE assessment_id = " & assessment_id

                    Using command As New SqlCommand(sql, connection)
                        command.ExecuteNonQuery()
                    End Using
                    
                End Using
            
            ElseIf type = "updateScore" Then
                
                Dim newscore As Integer = -1
                Dim num As Integer = 0
                Dim sum_score As Integer = 0

                If score = "0" Then
                    newscore = 5
                ElseIf score = "5" Then
                    newscore = 10
                ElseIf score = "10" Then
                    newscore = -1
                ElseIf score = "-1" Then
                    newscore = 0
                Else
                    newscore = -1
                End If
                
                sql = "UPDATE jci13_assessment_me_list SET "
                sql &= "  me_score_level = '" & newscore & "' "
                sql &= "WHERE assessment_me_id = " & id
                ' context.Response.Write(sql)
                Using command As New SqlCommand(sql, connection)
                    command.ExecuteNonQuery()
                End Using
                    
                sql = "SELECT * FROM jci13_assessment_me_list WHERE assessment_id = " & assessment_id
                '  sql &= " AND ISNULL(me_score_level,-1) >= 0 "
                sql &= " AND (me_score_level is null or me_score_level >=0 )"
                '   context.Response.Write(sql)
                Using da1 As New SqlDataAdapter(sql, connection)
                    da1.Fill(ds, "t1")
                    num = ds.Tables(0).Rows.Count
                    ' context.Response.Write("num = " & num)
                    For i As Integer = 0 To num - 1

                        If (ds.Tables(0).Rows(i)("me_score_level").ToString <> "")  Then
                            sum_score += CInt(ds.Tables(0).Rows(i)("me_score_level").ToString)
                        End If
                        
                        '   context.Response.Write("scorexxx = " & ds.Tables(0).Rows(0)("me_score_level").ToString)
                    Next i
                
                    Dim percent As Double = 0
                    Dim rank As String = ""
                    percent = Math.Round((sum_score / (num * 10)) * 100, 1)
                    '   context.Response.Write("score = " & sum_score )

                    If (percent > 97.5) Then
                        rank = "FE"
                    ElseIf (percent > 95) And (percent <= 97.5) Then
                        rank = "EE"
                    ElseIf percent = 95 Then
                        rank = "ME"
                    ElseIf (percent >= 90) And (percent < 95) Then
                        rank = "IN"
                    ElseIf percent < 90 Then
                        rank = "UN"
                    End If
                    sql = "UPDATE jci13_assessment_list SET score = '" & percent & "' "
                    sql &= " , rank = '" & rank & "' "
                    sql &= "WHERE assessment_id = " & assessment_id

                    Using command As New SqlCommand(sql, connection)
                        command.ExecuteNonQuery()
                    End Using
                    
                End Using

            ElseIf type = "addPicture" Then
                
             
                sql = "SELECT ISNULL(MAX(picture_id),0) + 1 AS pk FROM jci13_assessment_picture_list"

                Using da1 As New SqlDataAdapter(sql, connection)
                    da1.Fill(ds, "t1")
                    pk = ds.Tables("t1").Rows(0)("pk").ToString
                        
                    sql = "INSERT INTO jci13_assessment_picture_list (picture_id , assessment_me_id , picture_name , picture_path "
                    sql &= " )VALUES("
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & id & "' ,"
                    sql &= " '" & filename & "' ,"
                    sql &= " '" & filename & "' "
                   
                    sql &= " )"
                    Using command As New SqlCommand(sql, connection)
                        command.ExecuteNonQuery()
                    End Using
                    
                End Using

            ElseIf type = "getPictureList" Then
                
                sql = "SELECT picture_id , assessment_me_id , picture_name , picture_path FROM jci13_assessment_picture_list "
                sql &= " WHERE assessment_me_id = " & id
                
                Using command As New SqlCommand(sql, connection)
                    
               
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                       
                            ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                            result &= reader.GetInt32(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                            result &= reader.GetInt32(1).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                            result &= reader.GetString(2).Replace("\", "").Replace(vbCrLf, "") & "|"
                            result &= reader.GetString(3).Replace("\", "").Replace(vbCrLf, "") & "|"
                          
                            result &= "#"
                        
                        End While
                    
                   
                        context.Response.Write(result)
                    
                        reader.Close()
                        command.Dispose()
                        'context.Response.Write(sql)
                    End Using
                
                  
                End Using

            ElseIf type = "getEdition" Then
                
                sql = "SELECT edition  FROM jci13_std_list "
                sql &= " WHERE 1 = 1 GROUP BY edition "
                
                Using command As New SqlCommand(sql, connection)
                    
               
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                       
                            result &= reader.GetString(0).Replace("\", "").Replace(vbCrLf, "").Trim & "|"
                                                   
                            'result &= "#"
                        
                        End While
                    
                   
                        context.Response.Write(result)
                    
                        reader.Close()
                        command.Dispose()
                        'context.Response.Write(sql)
                    End Using
                
                  
                End Using
                
            ElseIf type = "getChapter" Then
                
                sql = "SELECT chapter  FROM jci13_std_list "
                sql &= " WHERE 1 = 1 GROUP BY chapter "
                
                Using command As New SqlCommand(sql, connection)
                    
               
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                       
                            result &= reader.GetString(0).Replace("\", "").Replace(vbCrLf, "").Trim & "|"
                                                   
                            'result &= "#"
                        
                        End While
                    
                   
                        context.Response.Write(result)
                    
                        reader.Close()
                        command.Dispose()
                        'context.Response.Write(sql)
                    End Using
                
                  
                End Using
                
            ElseIf type = "getUser" Then
                
                sql = "SELECT chapter  FROM user_profile "
                sql &= " WHERE 1 = 1 ORDER BY user_fullname "
                
                Using command As New SqlCommand(sql, connection)
                    
               
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                       
                            result &= reader.GetString(0).Replace("\", "").Replace(vbCrLf, "").Trim & "|"
                                                   
                            'result &= "#"
                        
                        End While
                    
                   
                        context.Response.Write(result)
                    
                        reader.Close()
                        command.Dispose()
                        'context.Response.Write(sql)
                    End Using
                
                  
                End Using
            End If
                
            ds.Dispose()
            connection.Close()
            connection.Dispose()
           ' context.Response.Write(sql)
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    
    Public Function getPK(ByVal column As String, ByVal table As String, ByVal conn As SqlConnection) As String
        Dim sql As String
        Dim result As String = ""
        Dim ds As New DataSet
        ' Dim conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Try

            sql = "SELECT ISNULL(MAX(assessment_me_id),0) + 1 AS pk FROM jci13_assessment_me_list"
           
            Using da2 As New SqlDataAdapter(sql, conn)
                            
                da2.Fill(ds, "t1")
                result = ds.Tables("t1").Rows(0)("pk").ToString
            End Using
        Catch ex As Exception

            result = ex.Message & " (" & sql & ")"
        Finally
            ds.Clear()
            ds = Nothing
            ' conn.closeSql()
        End Try

        Return result
    End Function
    
    Public Function addslashes(ByVal str As String) As String
        Dim ret As String = ""
        For Each c As Char In str
            Select Case c
                Case "'"c
                    ret += "''"
                    Exit Select
                Case """"c
                    ret += """"
                    Exit Select
                Case ControlChars.NullChar
                    ret += "\0"
                    Exit Select
                Case "\"c
                    ret += "\\"
                    Exit Select
                Case Else
                    ret += c.ToString()
            End Select
        Next
        Return ret
    End Function
End Class