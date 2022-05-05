<%@ WebHandler Language="VB" Class="ajax_country" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Configuration

Public Class ajax_country : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Dim info As String = ""
        Dim name As String = context.Request.QueryString("q")
        Dim sql As String = "select ISNULL(printable_name,'') , ISNULL(printable_name,'') as printable_name1 from m_country WHERE printable_name LIKE '%" & name & "%' "
        'sql &= " ORDER BY printable_name ASC"
        sql &= " UNION"
        sql &= " SELECT ISNULL(province_name_Th,'')  as t1, ISNULL(province_name_Th,'') as t1 from m_province WHERE province_name_Th LIKE '%" & name & "%' OR province_name_en LIKE '%" & name & "%'"
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