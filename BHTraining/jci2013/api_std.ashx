<%@ WebHandler Language="VB" Class="api_std" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text


Public Class api_std : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
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
        
        sql = "SELECT edition , section_no , section_name , chapter , goal , std_no , std_detail , measure_element_no , measure_element_detail , std_id , std_no + '.' + measure_element_no AS special1 FROM jci13_std_list WHERE 1 = 1 "
        'sql &= " AND edition = '" & edition & "' "
        
        ' sql &= " ORDER BY drug_brand_name ASC "
        If (edition <> "") Then
            sql &= " AND edition = '" & edition & "' "
        End If
        
        If chapter <> "" Then
            sql &= " AND rtrim(ltrim(chapter)) = '" & chapter.Trim & "' "
        End If
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()

                'Dim fileName As String = "D:\test.xlsx"
                ' Dim txt = ReadExcelFileDOM(fileName)    ' DOM
                'context.Response.Write(txt)
                
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        'reader.Read()
                       
                        ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                        sb.Append(reader.GetString(0))
                        sb.Append("|")
                        sb.Append(reader.GetString(1))
                        sb.Append("|")
                        sb.Append(reader.GetString(2))
                        sb.Append("|")
                        sb.Append(reader.GetString(3))
                        sb.Append("|")
                        sb.Append(reader.GetString(4))
                        sb.Append("|")
                        sb.Append(reader.GetString(5))
                        sb.Append("|")
                        sb.Append(reader.GetString(6))
                        sb.Append("|")
                        sb.Append(reader.GetString(7))
                        sb.Append("|")
                        sb.Append(reader.GetString(8))
                        sb.Append("|")
                        sb.Append(reader.GetInt32(9))
                        sb.Append("|")
                        sb.Append(reader.GetString(10))
                        sb.Append("|")
                        sb.Append("#")
                        
                        'result &= reader.GetString(0).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(2).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(3).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(4).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(5).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(6).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(7).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(8).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetInt32(9).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(10).Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= "#"
                        
                    End While
                    
                   
                    context.Response.Write(sb.ToString)
                    
                    reader.Close()
                    command.Dispose()
                  '  context.Response.Write(sql)
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