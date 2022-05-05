Imports System.Net.Mail

Partial Class srp_srp_contact
    Inherits System.Web.UI.Page

    Protected Sub cmdSendMail_Click(sender As Object, e As System.EventArgs) Handles cmdSendMail.Click
        sendMail("Wonwisa@bumrungrad.com", txtsubject.Text, txtdetail.Text & vbCrLf & txtfrom.Text & vbCrLf & txtdeptname.Text & vbCrLf & txttel.Text)

        Response.Redirect("srp_message.aspx")
    End Sub

    Public Sub sendMail(ByVal email As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Try
            'ConfigurationManager.AppSettings("database").ToString()
            If txtemail.Text <> "" Then
                oMsg.From = New MailAddress(txtemail.Text)
            Else
                oMsg.From = New MailAddress("srp_web@bumrungrad.com")
            End If

            oMsg.To.Add(New MailAddress(email))
            oMsg.CC.Add(New MailAddress("EmployeeRelationshipManagement@bumrungrad.com"))
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

    Protected Sub cmdCancel_Click(sender As Object, e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("srp_message.aspx")
    End Sub
End Class
