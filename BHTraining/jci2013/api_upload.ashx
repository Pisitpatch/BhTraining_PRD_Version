<%@ WebHandler Language="VB" Class="api_upload" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.IO

Public Class api_upload : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim filename As String
        
        filename = context.Request.QueryString("filename1")

        If context.Request.Files.Count > 0 Then
            Dim file1 As HttpPostedFile = context.Request.Files(0)
          '  file.SaveAs(Path.Combine(uploadDir, file.FileName))
            '  context.Response.Write(Path.Combine(uploadDir, file.FileName))
            ' context.Response.Write("filexx = " & file1.ContentType)
            
            context.Response.Write(context.Request.PhysicalApplicationPath & "\share\jci\pdf\" & filename)
            file1.SaveAs(context.Request.PhysicalApplicationPath & "\share\jci\pdf\" & filename)
            
            'context.Response.Write(context.Request.PhysicalApplicationPath & "\share\card\" & filename)
            'file1.SaveAs(context.Request.PhysicalApplicationPath & "\share\card\" & filename)
        Else
            context.Response.Write("fail")
        End If
        
    
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class