Imports ShareFunction
Imports System.Data

Partial Class jci2013_form_authen
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected form_id As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        form_id = Request.QueryString("form_id")

        If Not Page.IsPostBack Then ' ถ้าเปิดมาครั้งแรก
            bindAllPerson()
            bindSelectPerson()
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindAllPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT emp_code , ISNULL(user_fullname , user_fullname_local) AS user_fullname  from user_profile WHERE 1 = 1 "
           

            If txtfind_name.Text <> "" Then
                sql &= " AND (user_fullname LIKE '%" & txtfind_name.Text & "%' OR emp_code LIKE '%" & txtfind_name.Text & "%' OR user_fullname_local LIKE '%" & txtfind_name.Text & "%' )"
            Else
                sql &= " AND 1 > 2 "
            End If

            sql &= " ORDER BY user_fullname"
            '  Response.Write("Xxx" & txtfind_name.Text)
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtperson_all.DataSource = ds
            txtperson_all.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSelectPerson()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT b.emp_code , ISNULL(b.user_fullname , b.user_fullname_local) as user_fullname from jci13_form_authen a INNER JOIN user_profile b ON a.jci_emp_code = b.emp_code WHERE form_id = " & form_id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtperson_select.DataSource = ds
            txtperson_select.DataBind()

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                ' lblNominee.Text &= ds.Tables("t1").Rows(i)("user_fullname").ToString & "<br/>"
            Next i

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdFindName_Click(sender As Object, e As EventArgs) Handles cmdFindName.Click
        bindAllPerson()
    End Sub

    Protected Sub cmdAddRelatePerson_Click(sender As Object, e As EventArgs) Handles cmdAddRelatePerson.Click

        While txtperson_all.Items.Count > 0 AndAlso txtperson_all.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtperson_all.SelectedItem
            selectedItem.Selected = False
            txtperson_select.Items.Add(selectedItem)
            txtperson_all.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdRemoveRelatePerson_Click(sender As Object, e As EventArgs) Handles cmdRemoveRelatePerson.Click
        While txtperson_select.Items.Count > 0 AndAlso txtperson_select.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = txtperson_select.SelectedItem
            selectedItem.Selected = False
            txtperson_all.Items.Add(selectedItem)
            txtperson_select.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""

        Try

            sql = "DELETE FROM jci13_form_authen WHERE form_id = " & form_id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If


            For i As Integer = 0 To txtperson_select.Items.Count - 1
                pk = getPK("form_authen_id", "jci13_form_authen", conn)
                sql = "INSERT INTO jci13_form_authen (form_authen_id , jci_emp_code , form_id ) VALUES("
                sql &= "'" & pk & "' ,"
                sql &= "'" & txtperson_select.Items(i).Value & "' ,"
                sql &= "'" & form_id & "' "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)

                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
            Next i

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)

            Return
        End Try

        Response.Redirect("form_list.aspx?menu=3")
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Response.Redirect("form_list.aspx?menu=3")
    End Sub
End Class
