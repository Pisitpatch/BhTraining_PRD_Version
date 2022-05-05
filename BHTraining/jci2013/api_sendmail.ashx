<%@ WebHandler Language="VB" Class="api_sendmail" %>

Imports System
Imports System.Web
Imports System.Net.Mail
Public Class api_sendmail : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim email As String = context.Request.QueryString("email")
        Dim id As String = context.Request.QueryString("id")
        Dim empcode As String = context.Request.QueryString("empcode")
        Dim ts As String = context.Request.QueryString("ts")
        
        Dim type As String = context.Request.QueryString("type")
        Dim email_list() As String = email.Split(",")
        Dim cc_list() As String = "".Split(",")
        Dim bcc_list() As String = "".Split(",")
        Dim subject As String = "Internal Quality Assessment Report"
        Dim msgbody As String = ""
        
        msgbody &= "<a href='http://bhtraining/jci2013/result_template.aspx?id=" & id & "&empcode=" & empcode & "&type=" & type & "&ts=" & ts & "'>" & "Internal Quality Assessment Report" & "</a>"
        sendMailWithCC1(email_list, cc_list, bcc_list, subject, msgbody, "", "ir")
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Sub sendMailWithCC1(ByVal toEmail() As String, ByVal ccEmail() As String, ByVal bccEmail() As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal mailType As String = "ir", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Dim smtp As New SmtpClient("mail.bumrungrad.com")

        Try
            oMsg.From = New MailAddress("somsong@bumrungrad.com")
            oMsg.Headers.Add("Disposition-Notification-To", "<somsong@bumrungrad.com>")
            'ConfigurationManager.AppSettings("database").ToString()


            For i As Integer = 0 To UBound(toEmail)
                If toEmail(i) <> "" And toEmail(i).Length > 5 Then
                    oMsg.To.Add(New MailAddress(toEmail(i).ToLower))
                End If
            Next

            For i As Integer = 0 To UBound(ccEmail)
                If ccEmail(i) Is Nothing Or ccEmail(i) = "" Then
                Else
                    If ccEmail(i) <> "" And ccEmail(i).Length > 5 Then
                        oMsg.CC.Add(New MailAddress(ccEmail(i).ToLower))
                    End If
                End If

            Next

            Try
                For i As Integer = 0 To UBound(bccEmail)
                    If bccEmail(i) Is Nothing Or bccEmail(i) = "" Then
                    Else
                        If bccEmail(i) <> "" And bccEmail(i).Length > 5 Then
                            oMsg.Bcc.Add(New MailAddress(bccEmail(i).ToLower))
                        End If
                    End If

                Next
            Catch ex As Exception

            End Try

          

            oMsg.Subject = subject
            oMsg.IsBodyHtml = True
            oMsg.Body = message


            'Dim smtp As New SmtpClient("mail.powerpointproduct.com")
            ' SMTP Authenticate
            'smtp.Credentials = New System.Net.NetworkCredential("info@powerpointproduct.com", "natee")
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.Send(oMsg)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            oMsg = Nothing
            smtp = Nothing
        End Try


    End Sub
End Class