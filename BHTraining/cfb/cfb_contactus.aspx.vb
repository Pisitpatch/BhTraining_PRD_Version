Imports System.Net.Mail
Partial Class cfb_cfb_contactus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtfrom.Text = Session("user_fullname").ToString
    End Sub

    Protected Sub cmdSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSendMail.Click
        sendMail("cfb@bumrungrad.com", txtsubject.Text, txtdetail.Text & vbCrLf & txtfrom.Text)

        Response.Redirect("report_viewer.aspx")
    End Sub

    Public Sub sendMail(ByVal email As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Try
            'ConfigurationManager.AppSettings("database").ToString()
            oMsg.From = New MailAddress("cfb_web@bumrungrad.com")
            oMsg.To.Add(New MailAddress(email))
            oMsg.CC.Add(New MailAddress(email))
            oMsg.Subject = subject
            oMsg.Body = message
            Dim smtp As New SmtpClient("mail.bumrungrad.com")
            'Dim smtp As New SmtpClient("mail.powerpointproduct.com")
            ' SMTP Authenticate
            'smtp.Credentials = New System.Net.NetworkCredential("info@powerpointproduct.com", "natee")
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.Send(oMsg)

        Catch ex As Exception
            ' Throw New Exception(ex.Message)
            Response.Write(ex.Message)
        Finally
            oMsg = Nothing
        End Try


    End Sub


    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("report_viewer.aspx")
    End Sub
End Class
