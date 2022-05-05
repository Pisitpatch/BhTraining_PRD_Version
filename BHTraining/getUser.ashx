<%@ WebHandler Language="VB" Class="getUser" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class getUser : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("q")
        Dim sql As String = "select user_fullname , user_fullname_local , emp_code  from user_profile  WHERE LOWER(user_fullname) LIKE '%" & id.ToLower & "%' OR LOWER(user_fullname_local) LIKE '%" & id.ToLower & "%' OR emp_code LIKE '%" & id & "%'"
       ' sql &= " ORDER BY doctor_name_en , doctor_name_th "
        
        Dim lang As String = "en"
        
        Try
            lang = context.Session("lang").ToString()
        Catch ex As Exception
            lang = "en"
        End Try
        
       ' context.Response.Write(sql)
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()

                Try
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            'reader.Read()
                            ' If lang = "en" Then
                            context.Response.Write(reader.GetString(0).Replace("\", "") & "|" & reader.GetInt32(2)  & Environment.NewLine)
                            ' Else
                            ' context.Response.Write(reader.GetString(1).Replace("\", "") & "|" & reader.GetString(1).Replace("\", "") & Environment.NewLine)
                            ' End If
                        
                        End While
                    
                        reader.Close()
                        command.Dispose()
                        'context.Response.Write(sql)
                    End Using
                Catch ex As Exception
                       context.Response.Write(ex.Message)
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