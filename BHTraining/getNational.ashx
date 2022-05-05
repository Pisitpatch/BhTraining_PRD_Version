<%@ WebHandler Language="VB" Class="getNational" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class getNational : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("q")
        Dim sql As String = "select *  from m_national  WHERE LOWER(national_name) LIKE '%" & id.ToLower & "%' "
        sql &= " ORDER BY national_name  "
        
       
               
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        'reader.Read()
                       
                            context.Response.Write(reader.GetString(1).Replace("\", "") & "|" & reader.GetString(1).Replace("\", "") & Environment.NewLine)
                     
                        ' context.Response.Write(sql)
                    End While
                   
                    reader.Close()
                    command.Dispose()
                End Using
                
                connection.Close()
                connection.Dispose
            End Using
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class