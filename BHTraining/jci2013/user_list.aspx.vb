Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci2013_user_list
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If


        If IsPostBack Then

        Else ' First time load
           
            bindGrid()
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , ISNULL(b.is_admin,0) AS jci_admin , ISNULL(b.is_user,0) AS jci_user "

            sql &= " FROM user_profile a LEFT OUTER JOIN jci13_user_authen b ON a.emp_code = b.jci_emp_code "
            sql &= " WHERE 1 = 1 "

            If txtsearch.Text <> "" Then
                sql &= " AND (emp_code LIKE '%" & txtsearch.Text & "%' "
                sql &= " OR user_fullname LIKE '%" & txtsearch.Text & "%' "
                sql &= " OR user_fullname_local LIKE '%" & txtsearch.Text & "%' "
                sql &= ")"
            End If

           
            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim r As RadioButtonList
            Dim lblPK As Label
            Dim lblAdmin As Label
            Dim lblUser As Label
         
            Dim sql As String
            Dim ds As New DataSet

            lblPK = CType(e.Row.FindControl("lblEmpcode"), Label)
            lblAdmin = CType(e.Row.FindControl("lblAdmin"), Label)
            lblUser = CType(e.Row.FindControl("lblUser"), Label)
       
            r = CType(e.Row.FindControl("RadioButtonList1"), RadioButtonList)
         

            If lblAdmin.Text = "1" Then
                r.SelectedIndex = 0
            ElseIf lblUser.Text = "1" Then
                r.SelectedIndex = 1
            Else
                r.SelectedIndex = 2
            End If
            Try
             

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As EventArgs) Handles CmdSave.Click
        Dim r As RadioButtonList
        Dim lblPK As Label
        Dim sql As String = ""
        Dim errorMsg As String
        Dim is_admin As String = "0"
        Dim is_user As String = "0"

        Try
            For i As Integer = 0 To GridView1.Rows.Count - 1

                lblPK = CType(GridView1.Rows(i).FindControl("lblEmpcode"), Label)
                r = CType(GridView1.Rows(i).FindControl("RadioButtonList1"), RadioButtonList)

                is_admin = "0"
                is_user = "0"

                If (r.SelectedIndex = 0) Then
                    is_admin = "1"
                End If

                If r.SelectedIndex = 1 Then
                    is_user = "1"
                End If

                sql = "DELETE FROM jci13_user_authen WHERE jci_emp_code = " & lblPK.Text
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If

                sql = "INSERT INTO jci13_user_authen (jci_emp_code , is_admin , is_user) VALUES("
                sql &= "" & lblPK.Text & " , "
                sql &= "" & is_admin & " , "
                sql &= "" & is_user & "  "
                sql &= ")"

                ' Response.Write(sql & "<br/>")
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
            Next i

            conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
       
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub
End Class
