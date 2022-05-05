Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci_admin_test_edit
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String = ""
    Protected mode As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("jci_emp_code")) Then
            Response.Redirect("login.aspx")
            'Response.Write("Please re-login again")
            Response.End()
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        tid = Request.QueryString("tid")
        mode = Request.QueryString("mode")

        If IsPostBack Then

        Else ' First time load
            If mode = "edit" Then
                bindForm()
            End If
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

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_test WHERE test_id = " & tid
            ds = conn.getDataSetForTransaction(sql, "t1")

            txttest_th.Value = ds.Tables("t1").Rows(0)("test_name_th").ToString
            txttest_en.Value = ds.Tables("t1").Rows(0)("test_name_en").ToString
            txtorg.Value = ds.Tables("t1").Rows(0)("organizer").ToString

            If ds.Tables("t1").Rows(0)("is_active").ToString = "1" Then
                txtstatus1.Checked = True
            ElseIf ds.Tables("t1").Rows(0)("is_active").ToString = "0" Then
                txtstatus2.Checked = True
            End If
            txtdate1.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("test_start_date_ts").ToString)
            txtdate2.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("test_end_date_ts").ToString)

            If ds.Tables("t1").Rows(0)("lang_type").ToString = "1" Then
                txtlang1.Checked = True
            ElseIf ds.Tables("t1").Rows(0)("lang_type").ToString = "2" Then
                txtlang2.Checked = True
            ElseIf ds.Tables("t1").Rows(0)("lang_type").ToString = "3" Then
                txtlang3.Checked = True
            End If

          

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("admin_test.aspx")
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Try

            If mode = "add" Then
                isHasRow("jci_master_test")
            End If

            sql = "UPDATE jci_master_test SET test_name_th = '" & addslashes(txttest_th.Value) & "' "
            sql &= " , test_name_en = '" & addslashes(txttest_en.Value) & "' "
            sql &= " , organizer = '" & addslashes(txtorg.Value) & "' "

            If txtstatus1.Checked = True Then
                sql &= " , is_active = 1 "
            ElseIf txtstatus2.Checked = True Then
                sql &= " , is_active = 0 "
            End If

            If txtlang1.Checked = True Then
                sql &= " , lang_type = 1 "
            ElseIf txtlang2.Checked = True Then
                sql &= " , lang_type = 2 "
            ElseIf txtlang3.Checked = True Then
                sql &= " , lang_type = 3 "
            End If

            If txtdate1.Text <> "" Then
                sql &= " , test_start_date = '" & convertToSQLDatetime(txtdate1.Text) & "' "
                sql &= " , test_start_date_ts = '" & ConvertDateStringToTimeStamp(txtdate1.Text) & "' "
            Else
                sql &= " , test_start_date = null "
                sql &= " , test_start_date_ts = 0 "
            End If

            If txtdate2.Text <> "" Then
                sql &= " , test_end_date = '" & convertToSQLDatetime(txtdate2.Text) & "' "
                sql &= " , test_end_date_ts = '" & ConvertDateStringToTimeStamp(txtdate2.Text) & "' "
            Else
                sql &= " , test_end_date = null "
                sql &= " , test_end_date_ts = 0 "
            End If

            sql &= " WHERE test_id = " & tid
                '  sql &= " , test_name_en = '" & addslashes(txttest_en.Value) & "' "

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        Finally

        End Try

        Response.Redirect("admin_test.aspx")
    End Sub

    Sub isHasRow(ByVal table As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Dim pk As String

        pk = getPK("test_id", "jci_master_test", conn)
        tid = pk
        sql = "SELECT * FROM " & table & " WHERE test_id = " & tid
        ' Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage)
        End If

        If ds.Tables("t1").Rows.Count <= 0 Then
            sql = "INSERT INTO " & table & " (test_id) VALUES( "
            sql &= "" & pk & ""
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If
        End If

    End Sub
End Class
