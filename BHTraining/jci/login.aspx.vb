Imports System.Data

Partial Class jci_login
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If
        ' Response.Write(Session.Count)
        If Not Page.IsPostBack Then
            Session.Abandon()
            Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
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


        authenWithDatabase()


    End Sub

    Sub authenWithDatabase()
        Dim sql As String
        Dim ds As New DataSet

        Session("session_myid") = Session.SessionID

        Try
            sql = "SELECT a.* , ISNULL(b.emp_code,0) AS authen FROM user_profile a LEFT OUTER JOIN jci_user_authen b ON a.emp_code = b.emp_code "
            sql &= " WHERE a.emp_code = '" & txtusername.Text & "'"
            ds = conn.getDataSet(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
              
                Session("jci_emp_code") = txtusername.Text
                Session("jci_user_fullname") = ds.Tables("t1").Rows(0)("user_fullname").ToString
                Session("jci_dept_name") = ds.Tables("t1").Rows(0)("dept_name").ToString
                Session("jci_dept_id") = ds.Tables("t1").Rows(0)("dept_id").ToString
                Session("jci_job_title") = ds.Tables("t1").Rows(0)("job_title").ToString

                Session("jci_emp_code_admin") = ds.Tables("t1").Rows(0)("authen").ToString
            Else
                lblError.Text = "Not found Employee ID"
                Return
            End If

            lblError.Text = ""
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
            ' Response.Redirect("quiz.html")
            Response.Redirect("jci_select_question.aspx")
        End If
    End Sub
End Class
