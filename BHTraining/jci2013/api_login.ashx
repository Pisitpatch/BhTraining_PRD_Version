<%@ WebHandler Language="VB" Class="api_login" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Public Class api_login : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim username As String = context.Request.Form("username")
        Dim password As String = context.Request.Form("password")
        Dim type As String = context.Request.Form("type")
       
        Dim result As String
        
        If type = "" Then
            type = "1"
        End If
        
        If type = "1" Then
            result = authenWithDatabase(username, password)
        Else
            result = authenWithLdap(username, password.Replace("amp;", "&"))
        End If
        
        context.Response.Write(result)
      
        
   
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    
    Function authenWithDatabase(username As String, password As String) As String
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = "99"
       
        'sql = "SELECT emp_code , user_fullname FROM user_profile a WHERE user_login = '" & username & "'"
        'sql &= "AND user_password = '" & password & "'"
        
        sql = "SELECT a.emp_code , a.user_fullname , ISNULL(b.is_admin,0) AS jci_admin , ISNULL(b.is_user,0) AS jci_user "
        sql &= " FROM user_profile a LEFT OUTER JOIN jci13_user_authen b ON a.emp_code = b.jci_emp_code "
        sql &= " WHERE user_login = '" & username & "'"
        sql &= "AND user_password = '" & password & "'"
        
        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
            Using command As New SqlCommand(sql, connection)
                connection.Open()
               
                Using reader As SqlDataReader = command.ExecuteReader()
                    
                    While reader.Read()
                      
                        result = reader.GetInt32(0) & "|"  ' emp_code
                        result &= reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" ' user_fullname
                        result &= reader.GetInt16(2) & "|" ' jci_admin
                        
                        
                    End While
                        
                  
                    reader.Close()
                    command.Dispose()
                    'context.Response.Write(sql)
                   
                End Using
                
                connection.Close()
                connection.Dispose()
            End Using
        End Using

        Return result
    End Function

    Function authenWithLdap(username As String, password As String) As String
        Dim email As String
        Dim displayName As String
        Dim department As String = ""

        'Dim adPath As String = "LDAP://directory.certum.pl:636"
        Dim adPath As String = "LDAP://bumrungrad.com:389"
        Dim adAuth As New LdapAuthentication(adPath)

        Dim domainName = "bmc-users"
        
        Dim result As String = "99"
       ' Return username & " : " &  password
        Try
            If True = adAuth.IsAuthenticated(domainName, username, password) Then
                ' Create the authetication ticket
                ' version
                ' Dim authTicket As New FormsAuthenticationTicket(3, txtusername.Text, DateTime.Now, DateTime.Now.AddMinutes(60), False, "")
                ' Now encrypt the ticket.
                ' Dim encryptedTicket As String = FormsAuthentication.Encrypt(authTicket)
                ' Create a cookie and add the encrypted ticket to the
                ' cookie as data.
                ' Dim authCookie As New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                ' Add the cookie to the outgoing cookies collection.
                '  Response.Cookies.Add(authCookie)
                ' Redirect the user to the originally requested page
                ' Response.Redirect(FormsAuthentication.GetRedirectUrl(txtusername.Text, False))
                email = adAuth.email
                displayName = adAuth.displayName
                department = adAuth.department
                
                Dim sql As String
                Dim ds As New DataSet
                sql = "SELECT a.emp_code , a.user_fullname , ISNULL(c.is_admin,0) AS jci_admin , ISNULL(c.is_user,0) AS jci_user FROM user_profile a INNER JOIN user_dept b ON a.costcenter_id = b.dept_id "
                sql &= " LEFT OUTER JOIN jci13_user_authen c ON a.emp_code = c.jci_emp_code "
                sql &= " WHERE LOWER(a.bh_username) = '" & username & "'"
        
                Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)
                    Using command As New SqlCommand(sql, connection)
                        connection.Open()
               
                        Using reader As SqlDataReader = command.ExecuteReader()
                    
                            While reader.Read()
                      
                                result = reader.GetInt32(0).ToString.Replace("\", "").Replace(vbCrLf, "") & "|"  ' emp_code
                                result &= reader.GetString(1).Replace("\", "").Replace(vbCrLf, "") & "|" ' user_fullname
                                result &= reader.GetInt16(2) & "|" ' jci_admin
                            End While
                        
                  
                            reader.Close()
                            command.Dispose()
                            '  context.Response.Write(sql)
                            ' result = sql 
                        End Using
                
                        connection.Close()
                        connection.Dispose()
                    End Using
                End Using
            Else
                Return  result
            End If
        Catch ex As Exception
           
            Return result
        End Try

        Return result

    End Function
End Class