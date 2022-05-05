<%@ WebHandler Language="VB" Class="ajax_employee" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Configuration

Public Class ajax_employee : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim info As String = ""
        Dim name As String = context.Request.QueryString("q")
        Dim sql As String = "select user_fullname , emp_code , job_type , job_title , ISNULL(dept_name,'-') AS dept_name from user_profile WHERE emp_code LIKE '%" & name & "%' OR user_fullname LIKE '%" & name & "%' OR user_fullname_local LIKE '%" & name & "%'"
        sql &= " ORDER BY user_fullname ASC"
        ' context.Response.Write(sql)
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()
                
                Try
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            info = reader.GetInt32(1) & "#" & reader.GetString(2) & "#" & reader.GetString(3) & "#" & reader.GetString(4).Replace("/", "//")
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