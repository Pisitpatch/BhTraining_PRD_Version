<%@ WebHandler Language="VB" Class="srp_show_image" %>

Imports System
Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class srp_show_image : Implements IHttpHandler
    Dim mypath As String = ""
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As Int32
        If Not context.Request.QueryString("id") Is Nothing Then
            id = Convert.ToInt32(context.Request.QueryString("id"))
        Else
        '    Throw New ArgumentException("No parameter specified")
        End If
        mypath = context.Request.PhysicalApplicationPath
        context.Response.ContentType = "image/jpeg"
        Dim strm As Stream = ShowEmpImage(id)
        Dim buffer As Byte() = New Byte(4095) {}
        Dim byteSeq As Integer = strm.Read(buffer, 0, 4096)
 
        Do While byteSeq > 0
            context.Response.OutputStream.Write(buffer, 0, byteSeq)
            byteSeq = strm.Read(buffer, 0, 4096)
        Loop
    End Sub
    
    Public Function ShowEmpImage(ByVal id As Integer) As Stream
        Dim conn As String = ConfigurationManager.ConnectionStrings("MySqlServer").ToString
        Dim connection As SqlConnection = New SqlConnection(conn)
        'Dim sql As String = "SELECT empimg FROM shop_master_item WHERE empid = @ID"
        Dim sql As String = "SELECT picture_binary FROM shop_master_item WHERE ISNULL(is_delete,0) = 0 "
        sql &= " AND inv_item_id = " & id
        Dim cmd As SqlCommand = New SqlCommand(sql, connection)
        cmd.CommandType = CommandType.Text
        'cmd.Parameters.AddWithValue("@ID", empno)
        connection.Open()
        Dim img As Object = cmd.ExecuteScalar()
        Try
            Return New MemoryStream(CType(img, Byte()))
        Catch
           
            Return New MemoryStream(StreamFile( mypath & "/images/thumb_user.jpg"))
            'Return Nothing
        Finally
            connection.Close()
        End Try
    End Function
 
    Private Function StreamFile(ByVal filename As String) As Byte()
        Dim fs As New FileStream(filename, FileMode.Open, FileAccess.Read)
    
        ' Create a byte array of file stream length
        Dim ImageData As Byte() = New Byte(fs.Length - 1) {}
    
        'Read block of bytes from stream into the byte array
        fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length))
    
        'Close the File Stream
        fs.Close()
        'return the byte data
        Return ImageData
    End Function
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class