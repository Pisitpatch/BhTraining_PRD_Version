<%@ WebHandler Language="VB" Class="ajax_institution" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Configuration

Public Class ajax_institution : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim info As String = ""
        Dim name As String = context.Request.QueryString("q")
        Dim sql As String = "select institution_name , institution_name as institution_name1 from idp_m_institution WHERE institution_name LIKE '%" & name & "%' "
        sql &= " ORDER BY institution_name ASC"
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