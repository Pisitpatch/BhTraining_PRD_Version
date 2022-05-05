Imports System.Data

Partial Class confirmation
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected username As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        username = Request.QueryString("username")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        lblUsername.Text = username
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

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String

        Try

            sql = "UPDATE user_profile SET bh_username = '" & username & "' "
            sql &= " , custom_mobile = '" & txtmobile.Text & "' "

            sql &= " WHERE emp_code = " & txtcode.Text
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            If conn.rowAffectString <= 0 Then

                sql = "UPDATE m_doctor SET bh_username = '" & username & "' "
                sql &= " , custom_mobile = '" & txtmobile.Text & "' "
                'sql &= " , custom_email = '" & txt.Text & "' "
                sql &= " WHERE emp_no = " & txtcode.Text
                errorMsg = conn.executeSQL(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If

                If conn.rowAffectString <= 0 Then
                    Throw New Exception("This user is not found in HR database.")
                End If

            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
            Return
        End Try

        Response.Redirect("login.aspx")
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("login.aspx")
    End Sub
End Class
