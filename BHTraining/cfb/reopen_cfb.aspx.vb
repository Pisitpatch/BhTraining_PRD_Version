Imports System.Data

Partial Class cfb_reopen_cfb
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected session_id As String
    Protected irId As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        irId = Request.QueryString("irId")


        If irId = "" Then
            Response.End()
            Return
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        bindForm()


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
            sql = "SELECT * FROM cfb_detail_tab a INNER JOIN ir_trans_list b "
            sql &= " ON a.ir_id = b.ir_id WHERE a.ir_id = " & irId

            ds = conn.getDataSetForTransaction(sql, "t1")
            lblDate.Text = ds.Tables(0).Rows(0)("datetime_report").ToString
            lblNo.Text = ds.Tables(0).Rows(0)("cfb_no").ToString
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        updateReason()
    End Sub

    Sub updateReason()
        Dim sql As String
        Dim errorMsg As String
        Dim status_id As String = "91"
        Try

            If txtadd_reason.Text = "" Then
                'lblReasonError.Text = "Please enter reason detail"
                Return
            Else
                ' lblReasonError.Text = ""
            End If

            sql = "UPDATE ir_trans_list SET status_id = " & status_id
            sql &= " ,reopen_reason = '" & txtadd_reason.Text & "'"
            sql &= " WHERE ir_id = " & irId

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            sql = "INSERT INTO ir_status_log (status_id , status_name , ir_id , log_time , log_time_ts , log_create_by , position , dept_name , log_remark) VALUES("
            sql &= "'" & status_id & "' ,"
            sql &= "'" & "" & "' ,"
            sql &= "'" & irId & "' ,"
            sql &= "GETDATE() ,"
            sql &= "'" & Date.Now.Ticks & "' ,"
            sql &= "'" & Session("user_fullname").ToString & "' ,"
            sql &= "'" & Session("user_position").ToString & "' ,"
            sql &= "'" & Session("dept_name").ToString & "' ,"
            sql &= "'Re-open by " & Session("user_fullname").ToString & "' "
            sql &= ")"
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & ":" & sql)
            End If



            conn.setDBCommit()

            Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & irId)
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("form_cfb.aspx?mode=edit&cfbId=" & irId)
    End Sub

End Class
