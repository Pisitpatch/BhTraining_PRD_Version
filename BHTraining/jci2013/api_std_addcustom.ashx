<%@ WebHandler Language="VB" Class="api_std_addcustom" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Public Class api_std_addcustom : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("id")
        Dim edition As String = context.Request.QueryString("edition")
        Dim chapter As String = context.Request.QueryString("chapter")
        Dim sql As String
        Dim result As String = ""
        Dim sb As New StringBuilder
        
        If edition Is Nothing Then
            edition = ""
        End If
        
        If chapter Is Nothing Then
            chapter = ""
        End If
        
        sql = "SELECT ISNULL(me_id,0) AS std_select , b.form_id , a.std_id , a.edition, a.section_no , a.section_name , a.chapter , a.chapter_name , a.std_no , a.std_detail , a.goal, a.type , criteria , method , a.measure_element_no , a.measure_element_detail , a.std_no + '.' + a.measure_element_no AS special1 FROM jci13_std_list a  "
        sql &= " LEFT OUTER JOIN jci13_std_select b ON b.form_id = " & id
        sql &= " AND a.edition = b.edition "
        sql &= " AND a.section_no = b.section_no "
        sql &= " AND a.chapter = b.chapter "
        sql &= " AND a.std_no = b.std_no "
        sql &= " AND a.measure_element_no = b.measure_element_no "
        sql &= "  WHERE 1 = 1  "
        ' sql &= " ORDER BY drug_brand_name ASC "
        If (edition <> "") Then
            sql &= " AND a.edition = '" & edition & "' "
        End If
        
        If chapter <> "" Then
            sql &= " AND rtrim(ltrim(a.chapter)) = '" & chapter.Trim & "' "
        End If
        
        'context.Response.Write(sql)
        ' Return
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()
               
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        'reader.Read()
                       
                        ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                        
                        sb.Append(reader.GetSqlInt64(0).ToString) ' std_select
                        sb.Append("|")
                        If reader.IsDBNull(1) Then
                            sb.Append("|") ' form_id
                        Else
                            sb.Append(reader.GetInt32(1).ToString)
                            sb.Append("|")
                        End If
                        
                        If reader.IsDBNull(2) Then
                            sb.Append("|") ' std_id
                        Else
                            sb.Append(reader.GetInt32(2).ToString)
                            sb.Append("|")
                        End If
                        
                        sb.Append(reader.GetString(3).ToString) ' edition
                        sb.Append("|")
                        sb.Append(reader.GetString(4).ToString) ' section_no
                        sb.Append("|")
                        sb.Append(reader.GetString(5).ToString) ' section_name
                        sb.Append("|")
                        sb.Append(reader.GetString(6).ToString) ' chapter
                        sb.Append("|")
                        sb.Append(reader.GetString(7).ToString) ' chapter_name
                        sb.Append("|")
                        sb.Append(reader.GetString(8).ToString) ' std_no
                        sb.Append("|")
                        sb.Append(reader.GetString(9).ToString) ' std_detail
                        sb.Append("|")
                        sb.Append(reader.GetString(10).ToString) ' goal
                        sb.Append("|")
                        sb.Append(reader.GetString(11).ToString) ' type
                        sb.Append("|")
                        
                        If reader.IsDBNull(12) Then
                            sb.Append("|") ' criteria
                        Else
                            sb.Append(reader.GetString(12).ToString)
                            sb.Append("|")
                        End If
                        
                        If reader.IsDBNull(13) Then
                            sb.Append("|") ' method
                        Else
                            sb.Append(reader.GetString(13).ToString)
                            sb.Append("|")
                        End If
                        
                        sb.Append(reader.GetString(14).ToString) ' measure_element_no
                        sb.Append("|")
                        
                        sb.Append(reader.GetString(15).ToString) ' measure_element_detail
                        sb.Append("|")
                        
                        sb.Append(reader.GetString(16).ToString) ' special1
                        sb.Append("|")
                         sb.Append("#")
                        ' result &= reader.GetSqlInt64(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                        'If reader.IsDBNull(1) Then
                        '     result &= "" & "|" ' form_id
                        'Else
                        '    result &= reader.GetInt32(1).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                        'End If
                       
                        'If reader.IsDBNull(2) Then
                        '    result &= "" & "|" ' std_id
                        'Else
                        '    result &= reader.GetInt32(2).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                        'End If
                        
                        'result &= reader.GetString(3).Replace("\", "").Replace(vbCrLf, "") & "|"  ' edition
                        'result &= reader.GetString(4).Replace("\", "").Replace(vbCrLf, "") & "|" ' section_no
                        'result &= reader.GetString(5).Replace("\", "").Replace(vbCrLf, "") & "|" ' section_name
                        'result &= reader.GetString(6).Replace("\", "").Replace(vbCrLf, "") & "|" ' chapter
                        'result &= reader.GetString(7).Replace("\", "").Replace(vbCrLf, "") & "|" ' chapter_name
                        'result &= reader.GetString(8).Replace("\", "").Replace(vbCrLf, "") & "|" ' std_no
                        'result &= reader.GetString(9).Replace("\", "").Replace(vbCrLf, "") & "|" ' std_detail
                        'result &= reader.GetString(10).Replace("\", "").Replace(vbCrLf, "") & "|" ' goal
                        'result &= reader.GetString(11).Replace("\", "").Replace(vbCrLf, "") & "|" ' type
                      
                        
                        'If reader.IsDBNull(12) Then
                        '    result &= "" & "|" ' criteria
                        'Else
                        '    result &= reader.GetString(12).Replace("\", "").Replace(vbCrLf, "") & "|" ' criteria
                        'End If
                        
                        'If reader.IsDBNull(13) Then
                        '    result &= "" & "|" ' method
                        'Else
                        '    result &= reader.GetString(13).Replace("\", "").Replace(vbCrLf, "") & "|" ' method
                        'End If
                       
                        'result &= reader.GetString(14).Replace("\", "").Replace(vbCrLf, "") & "|" ' measure_element_no
                        'result &= reader.GetString(15).Replace("\", "").Replace(vbCrLf, "") & "|" ' measure_element_detail
                        'result &= reader.GetString(16).Replace("\", "").Replace(vbCrLf, "") & "|" ' measure_element_no
                        'result &= "#"
                        
                    End While
                    
                   
                    context.Response.Write(sb.ToString)
                    
                    reader.Close()
                    command.Dispose()
                    'context.Response.Write(sql)
                End Using
                
                connection.Close()
                connection.Dispose()
            End Using
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class