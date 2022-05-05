<%@ WebHandler Language="VB" Class="api_pdf_list" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class api_pdf_list : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("q")
        Dim sql As String
        Dim result As String = ""
        
        sql = "SELECT pdf_id , pdf_name , pdf_path FROM jci13_pdf_list WHERE ISNULL(is_delete,0) = 0 "
        ' sql &= " ORDER BY drug_brand_name ASC "
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()
               
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        'reader.Read()
                       
                        ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                        result &= reader.GetInt32(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                        result &= reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|"
                      result &= reader.GetString(2).Replace("\", "").Replace(vbCrLf, "") & "|"
                        result &= "#"
                        
                    End While
                    
                   
                    context.Response.Write(result)
                    
                    reader.Close()
                    command.Dispose()
                    'context.Response.Write(sql)
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