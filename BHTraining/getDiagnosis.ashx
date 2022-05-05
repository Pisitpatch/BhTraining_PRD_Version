<%@ WebHandler Language="VB" Class="getDiagnosis" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class getDiagnosis : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("q")
        Dim sql As String = "select diagename , diagtname  from m_diagnosis  WHERE LOWER(diagename) LIKE '%" & id.ToLower & "%' OR LOWER(diagtname) LIKE '%" & id.ToLower & "%' "
        sql &= " ORDER BY diagename , diagtname "
        
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
                            ' reader.Read()
                            If lang = "en" Then
                                context.Response.Write(reader.GetString(0).Replace("\", "") & " " & reader.GetString(1).Replace("\", "") & "|" & reader.GetString(1).Replace("\", "") & Environment.NewLine)
                            Else
                                context.Response.Write(reader.GetString(1).Replace("\", "") & "|" & reader.GetString(1).Replace("\", "") & Environment.NewLine)
                            End If
                            'context.Response.Write(reader.GetString(0) & Environment.NewLine)
                            ' context.Response.Write(reader.GetString(0).Replace("\", "") & " " & reader.GetString(1).Replace("\", "") & "|" & reader.GetString(1).Replace("\", "") & Environment.NewLine)
                        End While
                        'context.Response.Write(sql)
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