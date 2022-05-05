Imports System.IO
Imports System.Data
Imports ShareFunction
Partial Class game_admin_answer_edit
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String
    Protected gid As String
    Protected qid As String

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
        qid = Request.QueryString("qid")

        If IsPostBack Then

        Else ' First time load
            bindGridAnswer()
            ' bindGrid()
            bindPathWay()

            bindTestInfo()
        End If
    End Sub

    Sub bindPathWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_group a INNER JOIN jci_master_test b ON a.test_id = b.test_id "
            sql &= " INNER JOIN jci_master_question c ON a.group_id = c.group_id "
            sql &= " WHERE c.question_id = " & qid
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblPathWay.Text = " > <a href='admin_question_master.aspx?tid=" & tid & "'>" & ds.Tables("t1").Rows(0)("test_name_th").ToString & "</a> > <a href='admin_question.aspx?tid=" & tid & "&gid=" & gid & "'>" & ds.Tables("t1").Rows(0)("group_name_th").ToString & "</a>"
            lblPathWay.Text &= " > " & ds.Tables("t1").Rows(0)("question_detail_th").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridAnswer()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_answer WHERE ISNULL(is_answer_delete,0) = 0 AND question_id = " & qid
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTestInfo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_master_test WHERE test_id = " & tid
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows(0)("category_id").ToString = "102" Then
                GridView1.Columns(5).Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("admin_question.aspx?tid=" & tid & "&gid=" & gid)
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim filename As String()
        Dim ds As New DataSet

        If Not IsNothing(FileUpload1.PostedFile) Then

            Dim strFileName = FileUpload1.FileName
            Dim extension As String
            Dim iCount As Integer = 0

            If strFileName = "" Then
                ' Return
            End If

            filename = strFileName.Split(".")
            iCount = UBound(filename)
            extension = filename(iCount)
            Try
                sql = "SELECT ISNULL(MAX(answer_id),0) + 1 AS pk FROM jci_master_answer"
                ds = conn.getDataSetForTransaction(sql, "t1")
                pk = ds.Tables("t1").Rows(0)(0).ToString
            Catch ex As Exception
                Response.Write(ex.Message)
                Response.Write(sql)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try


            sql = "INSERT INTO jci_master_answer (answer_id , question_id , answer_detail_th , answer_detail_en , file_name , file_path , file_size , session_id) VALUES("
            sql &= "" & pk & " , "
            sql &= "" & qid & " , "
            sql &= "'" & addslashes(txtanswer_th.Value) & "' , "
            sql &= "'" & addslashes(txtanswer_en.Value) & "' , "
            sql &= "'" & strFileName & "' , "
            sql &= "'" & pk & "." & extension & "' , "
            sql &= "'" & FileUpload1.PostedFile.ContentLength & "' , "
            sql &= "'" & Session.SessionID & "'  "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            'Response.Write("pk = " & pk)
            If errorMsg <> "" Then
                Throw New Exception("ไม่สามารถเพิ่มข้อมูลไฟล์ได้ " & conn.errMessage & " " & sql)
            End If

            If strFileName <> "" Then
                FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/game/answer/" & pk & "." & extension))
                Dim MyFile As New FileInfo(Server.MapPath("../share/game/answer/" & pk & "." & extension))
                If MyFile.Exists() Then

                Else
                    ' Throw New Exception("File Not Found")
                    ' MessageBox.Show("File not found.")
                    conn.setDBRollback()
                    Return
                End If
            End If
         
          

            conn.setDBCommit()
            bindGridAnswer()
            txtanswer_en.Value = ""
            txtanswer_th.Value = ""
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "onDeleteTest") Then
            Dim sql As String
            Dim errorMsg As String
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Try
                sql = "UPDATE jci_master_answer SET is_answer_delete = 1 WHERE answer_id = " & index
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                bindGridAnswer()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPK As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim lblCorrect As Label = CType(e.Row.FindControl("lblCorrect"), Label)
            Dim cmdCorrect As Button = CType(e.Row.FindControl("cmdCorrect"), Button)

            If lblCorrect.Text = "1" Then
                cmdCorrect.Enabled = False
            Else
                cmdCorrect.Enabled = True
            End If

        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Sub onChangeCorrect(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chkSelect As RadioButton

        i = GridView1.Rows.Count

        Try

            sql = "UPDATE jci_master_answer SET is_correct = 0 WHERE question_id = " & qid
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If

            For s As Integer = 0 To i - 1

                lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)
                chkSelect = CType(GridView1.Rows(s).FindControl("radioCorrect"), RadioButton)

                If lbl.Text = e.CommandArgument.ToString Then
                    sql = "UPDATE jci_master_answer SET is_correct = 1 WHERE answer_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If

            Next s

            conn.setDBCommit()

            bindGridAnswer()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdEdit_Click(sender As Object, e As System.EventArgs) Handles cmdEdit.Click
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Dim txta1 As TextBox
        Dim txta2 As TextBox
        Dim lblPK As Label
        Try
            For i As Integer = 0 To GridView1.Rows.Count - 1
                txta1 = CType(GridView1.Rows(i).FindControl("txtanswer_th"), TextBox)
                txta2 = CType(GridView1.Rows(i).FindControl("txtanswer_en"), TextBox)
                lblPK = CType(GridView1.Rows(i).FindControl("lblPK"), Label)

                sql = "UPDATE jci_master_answer SET answer_detail_th = '" & addslashes(txta1.Text) & "' "
                sql &= " , answer_detail_en = '" & addslashes(txta2.Text) & "' "
                sql &= " WHERE answer_id = " & lblPK.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            conn.setDBCommit()
            bindGridAnswer()
        Catch ex As Exception
            conn.setDBRollback()
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
