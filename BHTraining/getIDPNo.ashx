<%@ WebHandler Language="VB" Class="getIDPNo" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class getIDPNo : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As String = context.Request.QueryString("q")
        Dim sql As String
        sql = "SELECT idp_no , idp_no  FROM idp_trans_list WHERE LOWER(idp_no) LIKE '%" & id & "%' "
        sql &= " ORDER BY idp_no ASC "
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        'reader.Read()
                       
                        context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                       
                        
                    End While
                    context.Response.Write(sql)
                End Using
            End Using
        End Using
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class