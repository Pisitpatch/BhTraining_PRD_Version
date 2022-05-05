<%@ WebHandler Language="VB" Class="getDrugListHandler" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class getDrugListHandler : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("q")
        Dim sql As String
        sql = "SELECT MAX(drug_code) , drug_brand_name , MAX(class) FROM m_druglist WHERE LOWER(drug_brand_name) LIKE '%" & id & "%' "
        sql &= " GROUP BY drug_brand_name   "
        sql &= " ORDER BY drug_brand_name ASC "
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(Sql, connection)
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        'reader.Read()
                       
                        context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf,"") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                       
                        
                    End While
                    
                    reader.Close()
                    command.Dispose
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