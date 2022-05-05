Imports System.Data

Partial Class popup_login
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Private is_normal_user As Integer = 1
    Protected empCode As String = ""
    Protected viewtype As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        empCode = Request.QueryString("code")
        viewtype = Request.QueryString("viewtype")

        txtusername.Text = empCode
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If
        ' Response.Write(Session.Count)
        If Not Page.IsPostBack Then
            '   Session.Abandon()
            '  Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        If txtlopgintype.SelectedValue = "1" Then
            authenWithDatabase()
        ElseIf txtlopgintype.SelectedValue = "2" Then
            authenWithLdap()

        End If
    End Sub


    Sub authenWithDatabase()
        Dim sql As String
        Dim ds As New DataSet
        Dim password As String
        Session("session_myid") = Session.SessionID

        Try
            sql = "SELECT * FROM user_profile WHERE user_login = '" & txtusername.Text & "'"
            ds = conn.getDataSet(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                password = ds.Tables("t1").Rows(0)("user_password").ToString
                If password = txtpassword.Text Then
                    setSession(ds.Tables("t1").Rows(0)("emp_code").ToString, ds.Tables("t1").Rows(0)("user_fullname").ToString, ds.Tables("t1").Rows(0)("costcenter_id").ToString, ds.Tables("t1").Rows(0)("dept_name").ToString, ds.Tables("t1").Rows(0)("costcenter_id").ToString, ds.Tables("t1").Rows(0)("job_type").ToString, ds.Tables("t1").Rows(0)("job_title").ToString)
                    'Server.Transfer("menu.aspx")
                    lblError.Text = ""
                Else
                    Throw New Exception("Login failed")

                End If
            Else
                sql = "SELECT * FROM m_doctor WHERE emp_no = '" & txtusername.Text & "'"
                ds = conn.getDataSet(sql, "t2")
                If ds.Tables("t2").Rows.Count > 0 Then
                    ' password = ds.Tables("t2").Rows(0)("user_password").ToString
                    setSession(ds.Tables("t2").Rows(0)("emp_no").ToString, ds.Tables("t2").Rows(0)("doctor_name_en").ToString, "0", ds.Tables("t2").Rows(0)("specialty").ToString, ds.Tables("t2").Rows(0)("specialty").ToString, ds.Tables("t2").Rows(0)("specialty").ToString, ds.Tables("t2").Rows(0)("specialty").ToString)

                    lblError.Text = ""
                Else
                    Throw New Exception("Login failed (2)")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Session.Abandon()
            Session.RemoveAll()
            'Response.End()
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

        If lblError.Text = "" Then
            '  Response.Redirect("menu.aspx")
            cmdContinue.Visible = True
            cmdLogin.Visible = False
            panel_login.Visible = False
        End If
    End Sub

    Sub authenWithLdap()
        Dim email As String
        Dim displayName As String
        Dim department As String = ""

        'Dim adPath As String = "LDAP://directory.certum.pl:636"
        Dim adPath As String = "LDAP://bumrungrad.com:389"
        Dim adAuth As New LdapAuthentication(adPath)

        Dim domainName = "bmc-users"
        Try
            If True = adAuth.IsAuthenticated(domainName, txtusername.Text, txtpassword.Text) Then
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


            Else
                lblError.Text = "Authentication failed, check username and password."
            End If
        Catch ex As Exception
            lblError.Text = "Error authenticating. " + ex.Message
            Return
        End Try

        ' ถ้าเป็น user ที่ไม่เคยเข้าระบบ
        If True = isNewlyUser(txtusername.Text) Then
            Response.Redirect("confirmation.aspx?username=" & txtusername.Text)
        Else
            Dim sql As String
            Dim ds As New DataSet
            Dim emp_code As String
            Dim dept_id As String
            Dim costcenter As String
            Dim position As String
            Dim job_title As String
            Try
                If is_normal_user = 1 Then
                    sql = "SELECT * FROM user_profile a INNER JOIN user_dept b ON a.costcenter_id = b.dept_id WHERE LOWER(a.bh_username) = '" & txtusername.Text.ToLower & "'"
                    ds = conn.getDataSet(sql, "t1")
                    emp_code = ds.Tables(0).Rows(0)("emp_code").ToString
                    displayName = ds.Tables(0).Rows(0)("user_fullname").ToString
                    dept_id = ds.Tables(0).Rows(0)("costcenter_id").ToString
                    department = ds.Tables(0).Rows(0)("dept_name_en").ToString
                    costcenter = ds.Tables(0).Rows(0)("costcenter_id").ToString
                    position = ds.Tables(0).Rows(0)("job_type").ToString
                    job_title = ds.Tables(0).Rows(0)("job_title").ToString
                Else
                    sql = "SELECT * FROM m_doctor WHERE LOWER(bh_username) = '" & txtusername.Text.ToLower & "'"
                    ds = conn.getDataSet(sql, "t1")
                    emp_code = ds.Tables(0).Rows(0)("emp_no").ToString
                    displayName = ds.Tables(0).Rows(0)("doctor_name_en").ToString
                    'dept_id = ds.Tables(0).Rows(0)("dept_id").ToString
                    dept_id = "61"
                    department = ds.Tables(0).Rows(0)("specialty").ToString
                    costcenter = ds.Tables(0).Rows(0)("specialty").ToString
                    position = ds.Tables(0).Rows(0)("specialty").ToString
                    job_title = ds.Tables(0).Rows(0)("specialty").ToString
                End If


                setSession(emp_code, displayName, dept_id, department, costcenter, position, job_title)
            Catch ex As Exception
                Session.Abandon()
                lblError.Text = ex.Message
                Return
            Finally
                ds.Dispose()
            End Try

            '  Response.Redirect("menu.aspx")
            cmdContinue.Visible = True
            cmdLogin.Visible = False
            panel_login.Visible = False
        End If
    End Sub

    Sub setSession(ByVal emp_code As String, ByVal fullname As String, ByVal dept_id As String, ByVal dept_name As String, ByVal costcenter As String, ByVal position As String, ByVal job_title As String)
        Session("session_myid") = Session.SessionID
        Session("emp_code") = emp_code
        Session("user_fullname") = fullname
        Session("dept_id") = dept_id
        Session("dept_name") = dept_name
        Session("costcenter_id") = costcenter
        Session("user_position") = position ' job_type
        Session("is_doctor") = "0"
        Session("job_title") = job_title
        Session("bh_username") = txtusername.Text
        Session("viewtype") = viewtype

        Dim sql As String
        Dim ds As New DataSet

        sql = "SELECT * FROM user_access_costcenter WHERE emp_code = " & Session("emp_code").ToString
        ds = conn.getDataSet(sql, "tCos")
        If ds.Tables("tCos").Rows.Count > 0 Then
            Dim costcenter_list() As String
            ReDim costcenter_list(ds.Tables("tCos").Rows.Count)
            For i As Integer = 0 To ds.Tables("tCos").Rows.Count - 1
                costcenter_list(i) = ds.Tables("tCos").Rows(i)("costcenter_id").ToString
            Next i

            Session("costcenter_list") = costcenter_list
        Else
            sql = "SELECT * FROM doctor_access_costcenter WHERE emp_code = " & Session("emp_code").ToString
            ds = conn.getDataSet(sql, "tDocCenter")
            If ds.Tables("tDocCenter").Rows.Count > 0 Then
                Dim costcenter_list() As String
                ReDim costcenter_list(ds.Tables("tDocCenter").Rows.Count)
                For i As Integer = 0 To ds.Tables("tDocCenter").Rows.Count - 1
                    costcenter_list(i) = ds.Tables("tDocCenter").Rows(i)("costcenter_id").ToString
                Next i

                Session("costcenter_list") = costcenter_list
                Session("is_doctor") = "1"
            End If
        End If

        sql = "SELECT * FROM user_role WHERE emp_code = " & Session("emp_code").ToString
        ds = conn.getDataSet(sql, "t2")
        If ds.Tables("t2").Rows.Count > 0 Then
            Dim priv() As String
            ReDim priv(ds.Tables("t2").Rows.Count)
            For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1
                priv(i) = ds.Tables("t2").Rows(i)("role_id").ToString
            Next i

            Session("priv_list") = priv
        Else
            sql = "SELECT * FROM doctor_role WHERE emp_code = " & Session("emp_code").ToString
            ds = conn.getDataSet(sql, "t22")
            If ds.Tables("t22").Rows.Count > 0 Then
                Dim priv() As String
                ReDim priv(ds.Tables("t22").Rows.Count)
                For i As Integer = 0 To ds.Tables("t22").Rows.Count - 1
                    priv(i) = ds.Tables("t22").Rows(i)("role_id").ToString
                Next i

                Session("priv_list") = priv
                Session("is_doctor") = "1"
            End If

        End If

        sql = "SELECT * FROM user_module WHERE emp_code = " & Session("emp_code").ToString
        ds = conn.getDataSet(sql, "t3")
        If ds.Tables("t3").Rows.Count > 0 Then
            Dim module_list() As String
            ReDim module_list(ds.Tables("t3").Rows.Count)
            For i As Integer = 0 To ds.Tables("t3").Rows.Count - 1
                module_list(i) = ds.Tables("t3").Rows(i)("module_id").ToString
            Next i

            Session("mobule_list") = module_list
        Else
            sql = "SELECT * FROM doctor_module WHERE emp_code = " & Session("emp_code").ToString
            ds = conn.getDataSet(sql, "t4")
            If ds.Tables("t4").Rows.Count > 0 Then
                Dim module_list() As String
                ReDim module_list(ds.Tables("t4").Rows.Count)
                For i As Integer = 0 To ds.Tables("t4").Rows.Count - 1
                    module_list(i) = ds.Tables("t4").Rows(i)("module_id").ToString
                Next i

                Session("mobule_list") = module_list
            End If

        End If

        ds.Clear()
        ds.Dispose()
    End Sub

    Function isNewlyUser(ByVal username As String) As Boolean
        Dim sql As String
        Dim ds As New DataSet
        Dim newuser As Boolean = False
        Try
            sql = "SELECT * FROM user_profile WHERE LOWER(RTRIM(LTRIM(bh_username))) = '" & username.ToLower & "'"
            ds = conn.getDataSet(sql, "t1")
            If ds.Tables(0).Rows.Count > 0 Then
                newuser = False
                is_normal_user = 1
            Else
                sql = "SELECT * FROM m_doctor WHERE LOWER(bh_username) = '" & username.ToLower & "'"
                ds = conn.getDataSet(sql, "t2")
                If ds.Tables("t2").Rows.Count > 0 Then
                    newuser = False
                    is_normal_user = 0 ' ไม่ใช่ user ทั่วไป
                Else
                    newuser = True
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message & " isNewlyUser " & sql)
        Finally
            ds.Dispose()
        End Try

        Return newuser
    End Function
    
End Class
