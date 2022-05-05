Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci_admin_group_edit
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String
    Protected gid As String
    Protected mode As String


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
        gid = Request.QueryString("gid")
        mode = Request.QueryString("mode")

        If IsPostBack Then

        Else ' First time load
            bindPathWay()

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
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id WHERE a.group_id = " & gid
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtgroup_th.Value = ds.Tables("t1").Rows(0)("group_name_th").ToString
            txtgroup_en.Value = ds.Tables("t1").Rows(0)("group_name_en").ToString
            txtip.Text = ds.Tables("t1").Rows(0)("group_fix_ip").ToString
            txtdetail.Value = ds.Tables("t1").Rows(0)("group_remark").ToString
            lblGroupName.Text = ds.Tables("t1").Rows(0)("test_name_th").ToString & "<br/>" & ds.Tables("t1").Rows(0)("test_name_en").ToString
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindPathWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id WHERE a.test_id = " & tid
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblPathWay.Text = " > <a href='admin_group.aspx?tid=" & tid & "'>" & ds.Tables("t1").Rows(0)("test_name_th").ToString & "</a> > "
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("admin_group.aspx?tid=" & tid)
    End Sub

    Sub isHasRow(ByVal table As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Dim pk As String

        pk = getPK("group_id", "jci_master_group", conn)
        gid = pk

        sql = "SELECT * FROM " & table & " WHERE group_id = " & gid
        ' Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage)
        End If

        If ds.Tables("t1").Rows.Count <= 0 Then
            sql = "INSERT INTO " & table & " (group_id) VALUES( "
            sql &= "" & pk & ""
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If
        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Try

            If mode = "add" Then
                isHasRow("jci_master_group")
            End If

            sql = "UPDATE jci_master_group SET group_name_th = '" & addslashes(txtgroup_th.Value) & "' "
            sql &= " , group_name_en = '" & addslashes(txtgroup_en.Value) & "' "
            sql &= " , group_fix_ip = '" & addslashes(txtip.Text) & "' "
            sql &= " , group_remark = '" & addslashes(txtdetail.Value) & "' "
            sql &= " , test_id = '" & tid & "' "
            sql &= " , create_by_emp_code = '" & Session("jci_emp_code").ToString & "' "
            sql &= " , create_by_emp_name = '" & Session("jci_user_fullname").ToString & "' "
            sql &= " , create_date = GETDATE() "
            sql &= " , create_date_ts = '" & Date.Now.Ticks & "' "
            sql &= " WHERE group_id = " & gid
            '  sql &= " , test_name_en = '" & addslashes(txttest_en.Value) & "' "
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
            Return
        Finally

        End Try

        Response.Redirect("admin_group.aspx?tid=" & tid)
    End Sub
End Class
