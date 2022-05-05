<%@ WebHandler Language="VB" Class="ajax_delete_event" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Configuration


Public Class ajax_delete_event : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim id As String = context.Request.QueryString("id")
        Dim sql As String = "UPDATE ssip_reserve_room_list SET is_delete = 1 WHERE reserve_id = " & id
       '  context.Response.Write(sql)
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()
                
                command.ExecuteNonQuery()
                connection.Close()
            End Using
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class