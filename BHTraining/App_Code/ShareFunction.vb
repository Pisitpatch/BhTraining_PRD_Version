Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Net.Mail
Imports System.Web
Imports System.Net
Imports System.IO
Imports DBUtil


Public Class ShareFunction

    Private Shared tatNum As Long = 0

    Public Shared Sub disableControl(ByVal parent As Panel, Optional ByVal flagDisable As Boolean = True)
        For Each c As Control In parent.Controls

            If TypeOf c Is HtmlTextArea Then
                DirectCast(c, HtmlTextArea).Disabled = True
            End If

            If TypeOf c Is HtmlInputButton Then
                DirectCast(c, HtmlInputButton).Disabled = True
            End If

            If TypeOf c Is HtmlInputText Then
                DirectCast(c, HtmlInputText).Disabled = True
            End If

            If TypeOf c Is HtmlInputSubmit Then
                DirectCast(c, HtmlInputSubmit).Disabled = True
            End If
        Next
    End Sub

    Public Shared Sub readonlyControl(ByVal parent As Panel, Optional ByVal flagDisable As Boolean = True)
        For Each c As Control In parent.Controls

            If TypeOf c Is HtmlTextArea Then
                DirectCast(c, HtmlTextArea).Attributes.Add("readonly", "readonly")
            End If

            If TypeOf c Is HtmlInputButton Then
                DirectCast(c, HtmlInputButton).Attributes.Add("readonly", "readonly")
            End If

            If TypeOf c Is HtmlInputText Then
                DirectCast(c, HtmlInputText).Attributes.Add("readonly", "readonly")
            End If

            If TypeOf c Is HtmlInputSubmit Then
                DirectCast(c, HtmlInputSubmit).Attributes.Add("readonly", "readonly")
            End If

            If TypeOf c Is Button Then
                DirectCast(c, Button).Attributes.Add("disabled", "disabled")
            End If

            If TypeOf c Is DropDownList Then
                DirectCast(c, DropDownList).Enabled = False

            End If

            If TypeOf c Is TextBox Then
                '   DirectCast(c, TextBox).Attributes.Add("readonly", "readonly")
                DirectCast(c, TextBox).ReadOnly = True

            End If

            If TypeOf c Is HtmlInputCheckBox Then
                DirectCast(c, HtmlInputCheckBox).Attributes.Add("disabled", "disabled")

            End If

            If TypeOf c Is CheckBox Then
                DirectCast(c, CheckBox).Enabled = False

            End If

            If TypeOf c Is HtmlInputRadioButton Then
                DirectCast(c, HtmlInputRadioButton).Attributes.Add("disabled", "disabled")

            End If

            If TypeOf c Is RadioButton Then
                DirectCast(c, RadioButton).Enabled = False

            End If

            If TypeOf c Is RadioButtonList Then
                DirectCast(c, RadioButtonList).Enabled = False

            End If

        Next
    End Sub


    Public Shared Sub readonlyGrid(ByVal parent As GridView, Optional ByVal flagDisable As Boolean = True)
        For Each c As Control In parent.Controls

            If TypeOf c Is HtmlTextArea Then
                DirectCast(c, HtmlTextArea).Attributes.Add("readonly", "readonly")
            End If

            If TypeOf c Is HtmlInputButton Then
                DirectCast(c, HtmlInputButton).Attributes.Add("readonly", "readonly")
            End If

            If TypeOf c Is HtmlInputText Then
                DirectCast(c, HtmlInputText).Attributes.Add("readonly", "readonly")
            End If

            If TypeOf c Is HtmlInputSubmit Then
                DirectCast(c, HtmlInputSubmit).Attributes.Add("readonly", "readonly")
            End If

            If TypeOf c Is Button Then
                DirectCast(c, Button).Attributes.Add("disabled", "disabled")
            End If

            If TypeOf c Is DropDownList Then
                DirectCast(c, DropDownList).Enabled = False

            End If

            If TypeOf c Is TextBox Then
                DirectCast(c, TextBox).Attributes.Add("readonly", "readonly")

            End If

        Next
    End Sub


    Public Shared Function findArrayValue(ByVal tArray As Array, ByVal tValue As String) As Boolean
        Dim n As Integer
        n = tArray.Length
        Try
            For i As Integer = 0 To n - 1
                If CStr(tArray(i)) = tValue Then
                    Return True
                End If
            Next
        Catch ex As Exception
            Return False
        End Try

        Return False
    End Function


    Public Shared Function convertToSQLDatetime(ByVal d As String, Optional ByVal myhour As String = "00", Optional ByVal mymin As String = "00") As String
        Dim result As String = ""
        Dim dArray(3) As String
        Dim yArray(2) As String
        Dim y As String
        Try
            dArray = d.Split("/")

            yArray = dArray(2).Split(" ")
            If CInt(yArray(0)) > 2400 Then
                y = CInt(yArray(0)) - 543
            Else
                y = CInt(yArray(0))
            End If
            result = y & "-" & dArray(1) & "-" & dArray(0) & " " & myhour & ":" & mymin & ":00"
        Catch ex As Exception
            result = ""
        End Try

        Return result
    End Function

    Public Shared Function ConvertDateStringToTimeStamp(ByVal dateStr As String, Optional ByVal myhour As Integer = 0, Optional ByVal mymin As Integer = 0) As Long
        Dim dateArray() As String
        Dim result As Long = 0
        'errMsg = ""
        Try
            If dateStr = "" Then
                result = 0
            Else
                dateArray = dateStr.Split("/")
                Dim date_format As New Date(CInt(dateArray(2)), CInt(dateArray(1)), CInt(dateArray(0)), myhour, mymin, 0)

                result = date_format.Ticks
            End If
        Catch ex As Exception

            Return "วันที่ " & dateStr & " ไม่ถูกต้อง :: " & ex.Message & vbCrLf
        End Try

        Return result
    End Function

    Public Shared Function ConvertTSToDateString(ByVal tsObj As Object) As String
        Dim newdate As String = ""
        Dim ts As String = ""
        Try
            If tsObj Is Nothing Then
                ts = ""
            Else
                ts = tsObj.ToString
            End If


            If ts = "" Then
                Return ""
            End If

            If (CDbl(ts)) <> 0 Then
                Dim date_format1 As New Date(ts)
                ' Dim dateArray() As String
                ' dateArray = date_format1.ToShortDateString.Split("/")

                If date_format1.Year > 2500 Then
                    newdate = date_format1.Day.ToString & "/" & date_format1.Month.ToString & "/" & (date_format1.Year - 543).ToString
                Else
                    newdate = date_format1.Day.ToString & "/" & date_format1.Month.ToString & "/" & date_format1.Year.ToString
                End If
                ' If CInt(dateArray(2)) > 2500 Then
                'newdate = dateArray(0) & "/" & dateArray(1) & "/" & CStr(CInt(dateArray(2)) - 543)
                'Else
                '   newdate = date_format1.Day & "/" & date_format1.Month & "/" & date_format1.Year
                'End If
            End If
        Catch ex As Exception

            newdate = ""
        End Try

        Return newdate
    End Function

    Public Shared Function ConvertTSToSQLDateTime(ByVal ts As String, Optional ByVal myhour As String = "00", Optional ByVal mymin As String = "00") As String
        Dim newdate As String = ""
        If ts = "" Or ts = "0" Then
            Return ""
        End If

        Try
            If (CDbl(ts)) <> 0 Then
                Dim date_format1 As New Date(ts)
                Dim dateArray() As String
                'dateArray = date_format1.ToShortDateString.Split("/")

                If date_format1.Year > 2500 Then
                    newdate = (date_format1.Year - 543).ToString & "-" & date_format1.Month.ToString & "-" & date_format1.Day.ToString & " " & myhour & ":" & mymin & ":00"
                Else
                    newdate = date_format1.Year.ToString & "-" & date_format1.Month.ToString & "-" & date_format1.Day.ToString & " " & myhour & ":" & mymin & ":00"
                End If

                'If CInt(dateArray(2)) > 2500 Then
                '    newdate = CStr(CInt(dateArray(2)) - 543) & "-" & dateArray(1) & "-" & dateArray(0) & " " & myhour & ":" & mymin & ":00"
                'Else
                '    newdate = date_format1.Year & "-" & date_format1.Month & "-" & date_format1.Day & " " & myhour & ":" & mymin & ":00"
                'End If
            End If
        Catch ex As Exception

            newdate = ex.Message
        End Try

        Return newdate
    End Function

    Public Shared Function ConvertTSTo(ByVal tsObj As Object, ByVal type As String) As String
        Dim newdate As String = "0"
        Dim ts As String = ""

        If tsObj Is Nothing Then
            ts = ""
        Else
            ts = tsObj.ToString
        End If


        If ts = "" Then
            Return ""
        End If

        Try
            If (CDbl(ts)) <> 0 Then
                Dim date_format1 As New Date(ts)
                ' Dim dateArray() As String
                '  dateArray = date_format1.ToShortDateString.Split("/")
                If type = "hour" Then
                    newdate = date_format1.Hour
                Else
                    newdate = date_format1.Minute
                End If
            End If
        Catch ex As Exception

            newdate = "0"
        End Try

        Return newdate
    End Function

    Public Shared Function MinuteDiff(ByVal tsStart As String, ByVal tsEnd As String) As String
        Dim newdate As String = ""
        Dim hour1 As String = ""
        Dim min1 As String = ""
        Dim day1 As String = ""
        Dim left_day1 As Double = 0
        Try
            Dim date_format1 As New Date(tsStart)
            Dim date_format2 As New Date(tsEnd)

            Dim diff_min As Long = DateDiff(DateInterval.Minute, date_format1, date_format2)
            tatNum = diff_min
            '  day1 = CInt((diff_min / 1440))
            day1 = Math.Floor(diff_min / 1440)
            left_day1 = diff_min Mod 1440
            hour1 = CInt(left_day1 / 60)
            min1 = left_day1 Mod 60
            'hour1 = CInt(diff_min / 60)
            'min1 = diff_min Mod 60
            newdate = day1 & " d " & hour1.PadLeft(2, "0") & ":" & min1.PadLeft(2, "0") & " hr"

            'newdate = day1 & " d "
        Catch ex As Exception

            newdate = "-"
        End Try

        Return newdate
    End Function

    Public Shared Function getTAT() As Long
        Return tatNum
    End Function


    Public Shared Function getPK(ByVal column As String, ByVal table As String, ByVal conn As DBUtil) As String
        Dim sql As String
        Dim result As String = ""
        Dim ds As New DataSet
        ' Dim conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Try

            sql = "SELECT ISNULL(MAX(" & column & "),0) + 1 AS pk FROM " & table

            'ds = conn.getDataSet(sql, "t1")
            ds = conn.GetDataSetForTransaction(sql, "t1")
            If conn.ErrorMessage <> "" Then
                Throw New Exception(conn.ErrorMessage)
            End If
            result = ds.Tables("t1").Rows(0)("pk").ToString
        Catch ex As Exception

            result = ex.Message & " (" & sql & ")"
        Finally
            ds.Clear()
            ds = Nothing
            ' conn.closeSql()
        End Try

        Return result
    End Function

    Public Shared Function getMonthName(ByVal i As Integer) As String
        Select Case i
            Case 1 : Return "Jan"
            Case 2 : Return "Feb"
            Case 3 : Return "Mar"
            Case 4 : Return "Apr"
            Case 5 : Return "May"
            Case 6 : Return "Jun"
            Case 7 : Return "Jul"
            Case 8 : Return "Aug"
            Case 9 : Return "Sep"
            Case 10 : Return "Oct"
            Case 11 : Return "Nov"
            Case 12 : Return "Dec"
        End Select
        Return ""
    End Function

    Public Shared Sub sendMail(ByVal email As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Try
            'ConfigurationManager.AppSettings("database").ToString()
            oMsg.From = New MailAddress("tqm@bumrungrad.com")
            oMsg.To.Add(New MailAddress(email))
            oMsg.CC.Add(New MailAddress(email))
            oMsg.Subject = subject
            oMsg.IsBodyHtml = True
            oMsg.Body = message
            Dim smtp As New SmtpClient("mail.bumrungrad.com")
            'Dim smtp As New SmtpClient("mail.powerpointproduct.com")
            ' SMTP Authenticate
            'smtp.Credentials = New System.Net.NetworkCredential("info@powerpointproduct.com", "natee")
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.Send(oMsg)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            oMsg = Nothing
        End Try


    End Sub

    ' For multi-thread
    Public Shared Sub sendMail2(ByVal parameters As Object)
        Dim oMsg As New MailMessage()
        Try
            Dim parameterArray As Object() = DirectCast(parameters, Object())
            Dim emailTo() As String = DirectCast(parameterArray(0), String())
            Dim emailCC() As String = DirectCast(parameterArray(1), String())
            Dim subject As String = DirectCast(parameterArray(2), String)
            Dim message As String = DirectCast(parameterArray(3), String)
            Dim from As String = DirectCast(parameterArray(4), String)

            'ConfigurationManager.AppSettings("database").ToString()
            oMsg.From = New MailAddress("TQMIncident@bumrungrad.com")

            For i As Integer = 0 To UBound(emailTo)
                If emailTo(i) <> "" And emailTo(i).Length > 5 Then
                    oMsg.To.Add(New MailAddress(emailTo(i)))
                End If
            Next

            For i As Integer = 0 To UBound(emailCC)
                If emailCC(i) Is Nothing Or emailCC(i) = "" Then
                Else
                    If emailCC(i) <> "" And emailCC(i).Length > 5 Then
                        oMsg.CC.Add(New MailAddress(emailCC(i)))
                    End If
                End If

            Next

            oMsg.Subject = subject
            oMsg.IsBodyHtml = True
            oMsg.Body = message


            Dim smtp As New SmtpClient("mail.bumrungrad.com")
            'Dim smtp As New SmtpClient("mail.powerpointproduct.com")
            ' SMTP Authenticate
            ' smtp.Credentials = New System.Net.NetworkCredential("info@powerpointproduct.com", "natee")

            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.Send(oMsg)

            smtp = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            oMsg = Nothing
        End Try


    End Sub

    ' Send mail with CC
    Public Shared Sub sendMailWithCC(ByVal toEmail() As String, ByVal ccEmail() As String, ByVal bccEmail() As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal mailType As String = "ir", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Dim smtp As New SmtpClient("mail.bumrungrad.com")

        Try
            If mailType = "ir" Then
                oMsg.From = New MailAddress("TQMIncident@bumrungrad.com")
                oMsg.Headers.Add("Disposition-Notification-To", "<TQMIncident@bumrungrad.com>")
            Else
                oMsg.From = New MailAddress("CFB@bumrungrad.com")
                oMsg.Headers.Add("Disposition-Notification-To", "<CFB@bumrungrad.com>")
            End If
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
            '     oMsg.IsBodyHtml = True
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


    Public Shared Sub sendMailWithCC_SSIP(ByVal toEmail() As String, ByVal ccEmail() As String, ByVal bccEmail() As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal mailType As String = "ir", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Dim smtp As New SmtpClient("mail.bumrungrad.com")

        Try
           
            oMsg.From = New MailAddress("innovation@bumrungrad.com")
            oMsg.Headers.Add("Disposition-Notification-To", "<innovation@bumrungrad.com>")

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

    Public Shared Function sendSMS(ByVal postData As String) As String

        '  ConfigurationManager.AppSettings.Get("smsserver")
        Dim host As String = ConfigurationManager.AppSettings("smsserver")
        Dim username As String = ConfigurationManager.AppSettings("smsUsername")
        Dim password As String = ConfigurationManager.AppSettings("smsPassword")
        Dim result As String = String.Empty

        '  Dim postData As String = String.Format("parameter1={0}&parameter2={1}", paramOneValue, paramTwoValue)

        Try
            Dim encoding As ASCIIEncoding = New ASCIIEncoding()
            Dim buffer() As Byte = encoding.GetBytes(postData)
            ' Prepare web request...
            Dim myRequest As HttpWebRequest = CType(WebRequest.Create(host), HttpWebRequest)
            ' We use GET but you can also use the POST (method)  
            'myRequest.Proxy = New WebProxy("addr", True)
            'myRequest()
            ' ==== PROXY ==================
            Dim proxy As IWebProxy = CType(myRequest.Proxy, IWebProxy)
            ' Print the Proxy Url to the console.
            If Not proxy Is Nothing Then
                ' Console.WriteLine("Proxy: {0}", proxy.GetProxy(myRequest.RequestUri))
            Else
                'Console.WriteLine("Proxy is null; no proxy will be used")
            End If

            Dim myProxy As New WebProxy()

            'Console.WriteLine(ControlChars.Cr + "Please enter the new Proxy Address that is to be set ")
            'Console.WriteLine("(Example:http://myproxy.example.com:port)")
            Dim proxyAddress As String = "http://bhproxy/array.dll?Get.Routing.Script"
            Dim newUri As New Uri(proxyAddress)
            myProxy.Address = newUri
            myRequest.Proxy = myProxy

            myRequest.Method = "POST"
            ' Set the content type to a FORM
            myRequest.ContentType = "application/x-www-form-urlencoded"
            ' Get length of content
            myRequest.ContentLength = postData.Length
            ' Get request stream
            Dim newStream As Stream = myRequest.GetRequestStream()
            ' Send the data.
            newStream.Write(buffer, 0, buffer.Length)

            ' Close stream
            newStream.Close()

            ' Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
            Dim myResponse As HttpWebResponse = CType(myRequest.GetResponse(), HttpWebResponse)

            ' Display the contents of the page to the console.
            Dim streamResponse As Stream = myResponse.GetResponseStream()
            ' Get stream object
            Dim streamRead As StreamReader = New StreamReader(streamResponse)

            Dim readBuffer(255) As Char

            ' Read from buffer
            Dim count As Integer = streamRead.Read(readBuffer, 0, 256)


            Do While (count > 0)
                ' get string
                Dim resultData As String = New String(readBuffer, 0, count)
                ' Read from buffer
                count = streamRead.Read(readBuffer, 0, 256)
                ' get the result
                result &= resultData
            Loop

            ' Release the response object resources.
            streamRead.Close()
            streamResponse.Close()

            ' Close response
            myResponse.Close()
        Catch ex As Exception
            result = ex.Message
            '  result = "-1"
        End Try


        Return result
    End Function

    Public Shared Function addslashes(ByVal str As String) As String
        Dim ret As String = ""
        For Each c As Char In str
            Select Case c
                Case "'"c
                    ret += "''"
                    Exit Select
                Case """"c
                    ret += """"
                    Exit Select
                Case ControlChars.NullChar
                    ret += "\0"
                    Exit Select
                Case "\"c
                    ret += "\\"
                    Exit Select
                Case Else
                    ret += c.ToString()
            End Select
        Next
        Return ret
    End Function

    Public Shared Function isAccessToLinkClick() As Boolean
        Dim costcenter_id = HttpContext.Current.Session("costcenter_id").ToString
        Dim appSetting = ConfigurationManager.AppSettings()
        Dim authorizedAccessByCostcenterId = "authorizedAccessByCostcenterId"
        Dim isAuthorizedAccess As Boolean

        If appSetting.AllKeys.Contains(authorizedAccessByCostcenterId) Then
            isAuthorizedAccess = appSetting(authorizedAccessByCostcenterId).ToString().Split(",").Contains(costcenter_id)
        End If

        Return isAuthorizedAccess

    End Function

End Class
