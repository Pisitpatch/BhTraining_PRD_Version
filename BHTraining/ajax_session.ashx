<%@ WebHandler Language="VB" Class="ajax_session" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.SessionState

Public Class ajax_session : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim mode As String = context.Request.QueryString("mode")
      
        If IsNothing(context.Session("session_myid")) Then ' ถ้าไม่มี Session
            '  Response.Redirect("../login.aspx")
            context.Response.Write("99")
            'Response.End()
        Else
          '  context.Response.Write("15")
        End If
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class