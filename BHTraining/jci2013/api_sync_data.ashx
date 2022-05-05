<%@ WebHandler Language="VB" Class="api_sync_data" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports ShareFunction

Public Class api_sync_data : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim master As String = context.Request.Form("master")
        Dim detail As String = context.Request.Form("detail")
        Dim picture As String = context.Request.Form("picture")
        Dim ts As String = context.Request.Form("ts")
        Dim empcode As String = context.Request.Form("empcode")
        Dim empname As String = context.Request.Form("empname")
        Dim assessment_id As String = context.Request.Form("assessment_id")
        Dim sql As String = ""
        Dim pk As String = ""
        Dim pk2 As String = ""
        Dim pk3 As String = ""
        Dim ds As New DataSet
        Dim msgbody As String = ""
     
        ' context.Response.Write("master = " & master)
        ' context.Response.Write("detail = " & detail)
        ' context.Response.Write("picture = " & picture)
        
        If master Is Nothing Then
            master = ""
        End If
        
        If detail Is Nothing Then
            detail = ""
        End If
        
        If picture Is Nothing Then
            picture = ""
        End If
        
       
        
        Dim masterArray() As String = master.Split("|")
        Dim detailArray() As String = detail.Split("#")
        Dim rowArray() As String
        Dim pictureArray() As String = picture.Split("#")
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            connection.Open()
                  
            pk = getPK("assessment_id", "jci13_assessment_list", connection)
                
            sql = "INSERT INTO jci13_assessment_list (assessment_id , form_id , emp_code , emp_name , assessment_date_str , assessment_time_str , dept_id , dept_name , location_id , location_name , type_id , type_name , member , score , rank , building_name , impression , create_date_raw , create_date_ts , ipad_assessment_id , ipad_timestamp "
            sql &= " )VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & masterArray(0) & "' ,"
            sql &= " '" & empcode & "' ," ' emp_code
            sql &= " '" & empname & "' ," ' emp_name
            sql &= " '" & masterArray(1) & "' ," ' assessment_date_str
            sql &= " '" & masterArray(2) & "' ," ' assessment_time_str
            sql &= " '" & masterArray(7) & "' ," ' dept_id
            sql &= " '" & masterArray(9) & "' ," ' dept_name
            sql &= " '" & 0 & "' ," ' location_id
            sql &= " '" & masterArray(3) & "' ," ' location_name
            sql &= " '" & 0 & "' ," ' type_id
            sql &= " '" & masterArray(8) & "' , " ' type_name
            sql &= " '" & masterArray(10) & "' , " ' member
            sql &= " '" & masterArray(11) & "' , " ' score
            sql &= " '" & masterArray(12) & "' , " ' rank
            sql &= " '" & masterArray(13) & "' , " ' building_name
            sql &= " '" & masterArray(14) & "' , " ' impression
            sql &= " GETDATE() , " ' create_date_raw
            sql &= " '" & Date.Now.Ticks & "' ,  " ' create_date_ts
            sql &= " '" & assessment_id & "'  , " ' assessment_id
            sql &= " '" & ts & "'   " ' ts
            sql &= " )"
            Using command As New SqlCommand(sql, connection)
                command.ExecuteNonQuery()
            End Using
                    
            For i As Integer = 0 To detailArray.Length - 1
                rowArray = detailArray(i).Split("|")
                                         
                If rowArray.Length > 1 Then
                    
                    Try
                        ' context.Response.Write(detailArray(i) & "\n")
                        'context.Response.Write("====================" & rowArray.Length)
                        
                        pk2 = getPK("assessment_me_id", "jci13_assessment_me_list", connection)
                        sql = "INSERT INTO jci13_assessment_me_list (assessment_me_id , ipad_assessment_me_id , assessment_id , me_id , me_score_level , me_comment_detail  "
                        sql &= " )VALUES("
                        sql &= " '" & pk2 & "' ," ' assessment_me_id
                        sql &= " '" & rowArray(15) & "' ," ' ipad_assessment_me_id
                        sql &= " '" & pk & "' ," ' assessment_id
                        sql &= " '" & rowArray(0) & "' ," ' me_id
                        sql &= " '" & rowArray(16) & "' ," ' me_score_level
                        sql &= " '" & rowArray(17) & "'  " ' me_comment_detail
                        sql &= " )"
                    
                       
                        Using command As New SqlCommand(sql, connection)
                            command.ExecuteNonQuery()
                        End Using
                        
                    
                        
                    Catch ex As Exception
                        context.Response.Write(ex.Message & detailArray(i))
                    End Try
                 
                End If
                      
            Next i
                
            '     context.Response.Write(pictureArray.Length)           
            For ii As Integer = 0 To pictureArray.Length - 1
                rowArray = pictureArray(ii).Split("|")
                                         
                If rowArray.Length > 1 Then
                    pk3 = getPK("picture_id", "jci13_assessment_picture_list", connection)
                    sql = "INSERT INTO jci13_assessment_picture_list (picture_id , assessment_me_id , ipad_assessment_me_id , picture_name , picture_path , ipad_timestamp  "
                    sql &= " )VALUES("
                    sql &= " '" & pk3 & "' ," ' picture_id
                    sql &= " '" & 0 & "' ," ' assessment_me_id
                    sql &= " '" & rowArray(1) & "' ," ' ipad_assessment_me_id
                    sql &= " '" & rowArray(2) & "' ," ' picture_name          
                    sql &= " '" & rowArray(2) & "' , " ' picture_path
                    sql &= " '" & ts & "'  " ' ts
                    sql &= " )"
                    Using command As New SqlCommand(sql, connection)
                        command.ExecuteNonQuery()
                    End Using
                    
                    context.Response.Write("xxx " & sql)
                End If
                      
            Next ii
            
        End Using
        
        ' Send email
        Dim email_list() As String = "".Split(",")
        Dim cc_list() As String = "".Split(",")
        Dim bcc_list() As String = "".Split(",")
        Dim subject As String = "Internal Quality Assessment Report"
        msgbody &= "<a href='http://bhtraining/jci2013/result_template.aspx?id=" & pk & "'>" & "Internal Quality Assessment Report" & "</a>"
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
           oMsg.From = New MailAddress("ipad-noreply@bumrungrad.com")
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
    
    Public Function getPK(ByVal column As String, ByVal table As String, ByVal conn As SqlConnection) As String
        Dim sql As String
        Dim result As String = ""
        Dim ds As New DataSet
        ' Dim conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Try

            sql = "SELECT ISNULL(MAX(" & column & "),0) + 1 AS pk FROM " & table
           
            Using da2 As New SqlDataAdapter(sql, conn)
                            
                da2.Fill(ds, "t1")
                result = ds.Tables("t1").Rows(0)("pk").ToString
            End Using
        Catch ex As Exception

            result = ex.Message & " (" & sql & ")"
        Finally
            ds.Clear()
            ds = Nothing
            ' conn.closeSql()
        End Try

        Return result
    End Function
End Class