Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci_admin_question_edit
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String
    Protected gid As String
    Protected qid As String
    Protected mode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        tid = Request.QueryString("tid")
        gid = Request.QueryString("gid")
        qid = Request.QueryString("qid")
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
            sql = "SELECT * FROM jci_master_question a INNER JOIN jci_master_group b ON a.group_id = b.group_id WHERE a.question_id = " & qid
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtq_th.Value = ds.Tables("t1").Rows(0)("question_detail_th").ToString
            txtq_en.Value = ds.Tables("t1").Rows(0)("question_detail_en").ToString
            txtorder.Text = ds.Tables("t1").Rows(0)("question_order_sort").ToString
            lblGroupName.Text = ds.Tables("t1").Rows(0)("group_name_th").ToString & "<br/>" & ds.Tables("t1").Rows(0)("group_name_en").ToString
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Try

            If mode = "add" Then
                isHasRow("jci_master_question")
            End If

            sql = "UPDATE jci_master_question SET question_detail_th = '" & addslashes(txtq_th.Value) & "' "
            sql &= " , question_detail_en = '" & addslashes(txtq_en.Value) & "' "
            sql &= " , question_order_sort = '" & addslashes(txtorder.Text) & "' "
            sql &= " , group_id = '" & gid & "' "
            sql &= " , test_id = '" & tid & "' "
            sql &= " , create_by_emp_code = '" & Session("jci_emp_code").ToString & "' "
            sql &= " , create_by_emp_name = '" & Session("jci_user_fullname").ToString & "' "
            sql &= " , create_date = GETDATE() "
            sql &= " , create_date_ts = '" & Date.Now.Ticks & "' "
            sql &= " WHERE question_id = " & qid
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

        Response.Redirect("admin_question.aspx?tid=" & tid & "&gid=" & gid)
    End Sub


    Sub isHasRow(ByVal table As String)
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Dim pk As String

        pk = getPK("question_id", "jci_master_question", conn)
        qid = pk
        sql = "SELECT * FROM " & table & " WHERE question_id = " & qid
        ' Response.Write(sql)
        ds = conn.getDataSetForTransaction(sql, "t1")
        If conn.errMessage <> "" Then
            Throw New Exception(conn.errMessage)
        End If

        If ds.Tables("t1").Rows.Count <= 0 Then
            sql = "INSERT INTO " & table & " (question_id) VALUES( "
            sql &= "" & pk & ""
            sql &= ")"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & " : " & sql)
            End If
        End If

    End Sub

    Sub bindPathWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id WHERE a.group_id = " & gid
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblPathWay.Text = " > <a href='admin_group.aspx?tid=" & tid & "'>" & ds.Tables("t1").Rows(0)("test_name_th").ToString & "</a> > <a href='admin_question.aspx?tid=" & tid & "&gid=" & gid & "'>" & ds.Tables("t1").Rows(0)("group_name_th").ToString & "</a>"
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("admin_question.aspx?tid=" & tid & "&gid=" & gid)
    End Sub
End Class
