<%@ WebHandler Language="VB" Class="getDocument" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class getDocument : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("q")
        Dim sql As String = "select document_name_en   from ir_m_document_delete_list  WHERE LOWER(document_name_en) LIKE '%" & id.ToLower & "%'  "
        sql &= " ORDER BY document_name_en  "
        
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
                       ' context.Response.Write(sql)

                        While reader.Read()
                            ' reader.Read()
                            context.Response.Write(reader.GetString(0).Replace("\", "").Replace("""", "") & Environment.NewLine)
                        End While
                        ' context.Response.Write(11)
                        reader.Close()
                        command.Dispose()
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