<%@ WebHandler Language="VB" Class="ajax_sh_location" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Configuration

Public Class ajax_sh_location : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim info As String = ""
        Dim name As String = context.Request.QueryString("q")
        Dim sql As String = "select room_name , room_name as room_name1 from idp_m_room WHERE room_name LIKE '%" & name & "%' "
        sql &= " ORDER BY room_name ASC"
        ' context.Response.Write(sql)
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()
                
                Try
                    Using reader As SqlDataReader = command.ExecuteReader()
                        
                        While reader.Read()
                            info = reader.GetString(1).Replace("/", "//")
                            ' context.Response.Write(info & "|" & reader.GetString(0)  & Environment.NewLine)
                            context.Response.Write(reader.GetString(0).Replace("\", "") & "|" & info & Environment.NewLine)
                        End While
                        
                        reader.Close()
                        command.Dispose()
                 
                    End Using
                Catch ex As Exception

                Finally
                    connection.Close()
                    connection.Dispose
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