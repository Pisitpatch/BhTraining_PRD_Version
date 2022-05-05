<%@ WebHandler Language="VB" Class="api_form_list" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class api_form_list : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim empcode As String = context.Request.QueryString("empcode")
        Dim sql As String
        Dim result As String = ""
        Dim sb As New StringBuilder
        
        sql = "SELECT form_id , form_name FROM jci13_form_list WHERE ISNULL(is_form_delete,0) = 0 AND is_form_active = 1 "
        sql &= " AND form_id IN (SELECT form_id FROM jci13_form_authen WHERE jci_emp_code = " & empcode &  " )"
        ' sql &= " ORDER BY drug_brand_name ASC "
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()
               
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        'reader.Read()
                       
                        ' context.Response.Write(reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" & reader.GetString(2).Replace("\", "") & Environment.NewLine)
                        sb.Append(reader.GetInt32(0).ToString.Replace("\", "").Replace(vbCrLf, ""))
                        sb.Append("|")
                        sb.Append(reader.GetString(1).Replace("\", "").Replace(vbCrLf, ""))
                        sb.Append("|")
                        
                        sb.Append("#")
                        ' result &= reader.GetInt32(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"
                        'result &= reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|"
                     
                        ' result &= "#"
                        
                    End While
                    
                   
                    context.Response.Write(sb.ToString)
                    
                    
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