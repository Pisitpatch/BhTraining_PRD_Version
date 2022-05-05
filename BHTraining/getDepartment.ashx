<%@ WebHandler Language="VB" Class="getDepartment" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class getDepartment : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("q")
        Dim sql As String = "select dept_name_en , dept_id , dept_name_th from user_dept  WHERE LOWER(dept_name_en) LIKE '%" & id.ToLower & "%' OR LOWER(dept_name_th) LIKE '%" & id.ToLower & "%' OR dept_id LIKE '%" & id & "%'"
        sql &= " ORDER BY dept_name_en , dept_name_th "
        
        Dim lang As String = "en"
        
        Try
            lang = context.Session("lang").ToString()
        Catch ex As Exception
            lang = "en"
        End Try
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()

                Try
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                            ' If lang = "en" Then
                            context.Response.Write(reader.GetInt32(1) & " " & reader.GetString(0).Replace("\", "").Trim & "|" & reader.GetInt32(1) & Environment.NewLine)
                            'context.Response.Write(reader.GetString(0).Replace("\", "") & " " & reader.GetString(1).Replace("\", "") & "|" & reader.GetString(2).Replace("\", "").Replace(",", " ") & Environment.NewLine)
                            ' Else
                            ' context.Response.Write(reader.GetString(1).Replace("\", "") & "|" & reader.GetString(1).Replace("\", "") & Environment.NewLine)
                            ' End If
                        
                        End While
                  
                        reader.Close()
                        command.Dispose()
                    End Using
                Catch ex As Exception

                Finally
                    connection.Close()
                    connection.Dispose()
                End Try
             
                
             
            End Using
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class