Imports System.Data
Imports ShareFunction

Partial Class jci_JCI_MasterPage
    Inherits System.Web.UI.MasterPage
    Protected lang As String = "th"
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If IsPostBack Then

        Else ' First time load
            If Not IsNothing(Session("jci_emp_code_admin")) Then
                If Session("jci_emp_code_admin").ToString <> "0" Then
                    linkAdmin.Visible = True
                Else
                    linkAdmin.Visible = False
                End If
                lblInfo.Text = Session("jci_user_fullname").ToString
                lblDeptName.Text = Session("jci_dept_name").ToString
                lblTitle.Text = Session("jci_job_title").ToString
            Else
                div_tip.Visible = False
            End If
         

        End If

        If Request.QueryString("lang") = "" Then
            lang = "th"
        Else
            lang = Request.QueryString("lang")
        End If

        bindLang()

        'Response.Write("M " & Session("lang").ToString & "<hr/>")
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

    End Sub

    Sub bindLang()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_test WHERE is_active = 1"
            ds = conn.getDataSet(sql, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                If ds.Tables("t1").Rows(0)("lang_type").ToString = "1" Then
                    cmdThai.Visible = True
                    cmdEnglish.Visible = False
                ElseIf ds.Tables("t1").Rows(0)("lang_type").ToString = "2" Then
                    cmdThai.Visible = False
                    cmdEnglish.Visible = True
                ElseIf ds.Tables("t1").Rows(0)("lang_type").ToString = "3" Then
                    cmdThai.Visible = True
                    cmdEnglish.Visible = True
                End If
            End If
        Catch ex As Exception
            Response.Write("master : " & ex.Message)
        Finally
            ds.Dispose()
            Try
                conn.closeSql()
            Catch ex As Exception

            End Try
        End Try
    End Sub


    Protected Sub cmdLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogout.Click
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub


 

    Protected Sub cmdThai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdThai.Click
        Response.Redirect("jci_select_question.aspx?lang=th")
    End Sub

    Protected Sub cmdEnglish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnglish.Click
        Response.Redirect("jci_select_question.aspx?lang=en")
    End Sub
End Class

