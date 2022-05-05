Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class incident_DeleteScanControl
    Inherits System.Web.UI.UserControl

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Private mode As String = ""
    Private irId As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        mode = Request.QueryString("mode")
        irId = Request.QueryString("irId")

        bindHour()
        bindMinute()
        bindDeleteScan()
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload

    End Sub

    Private Sub bindHour()

        Dim i As Integer = 0
        Dim i_str As String = ""
        For i = 0 To 23
            i_str = i.ToString
            txthour.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
            txthour2.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
        Next

        'txthour.Items.Insert(0, New ListItem("hh", "0"))
        ' txthour2.Items.Insert(0, New ListItem("hh", "0"))
    End Sub

    Private Sub bindMinute()
        Dim i As Integer = 0
        Dim i_str As String = ""
        For i = 0 To 59
            i_str = i.ToString
            txtmin.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
            txtmin2.Items.Add(New ListItem(i_str.PadLeft(2, "0"), i_str, True))
        Next

        ' txtmin.Items.Insert(0, New ListItem("mm", "0"))
        ' txtmin2.Items.Insert(0, New ListItem("mm", "0"))
        'txtmin2.Items.Insert(0, New ListItem("-", "0"))
        'txtmin2.SelectedIndex = 0


    End Sub

    Sub bindDeleteScan()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_delete_scan_detail WHERE 1 = 1 "
            If mode = "add" Then
                sql &= " AND session_id = '" & Session.SessionID & "'"
            Else
                sql &= " AND ir_id = " & irId
            End If
            ds = conn.getDataSetForTransaction(sql, "t1")

            gridDeleteScan.DataSource = ds
            gridDeleteScan.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim sql As String
        Dim errorMsg As String = ""
        Dim pk As String

        Try
            pk = getPK("delete_scan_id", "ir_delete_scan_detail", conn)
            sql = "INSERT INTO ir_delete_scan_detail (delete_scan_id , ir_id , wrong_hn , wrong_doc_name ,  wrong_doctor , wrong_doc_no , wrong_other , correct_hn , correct_doc_name , correct_doctor , correct_doc_no , correct_other , wrong_date_raw , wrong_date_ts , correct_date_raw , correct_date_ts , create_date_raw , create_date_ts , create_by , session_id ) VALUES("
            sql &= " '" & pk & "' ,"
            If mode = "add" Then
                sql &= " null ,"
            Else
                sql &= " '" & irId & "' ,"

            End If
            sql &= " '" & addslashes(txtwrong_hn.Text) & "' ,"
            sql &= " '" & addslashes(txtwrong_doc_name.Text) & "' ,"
            sql &= " '" & addslashes(txtwrong_doctor.Text) & "' ,"
            sql &= " '" & addslashes(txtwrong_doc_no.Text) & "' ,"
            sql &= " '" & addslashes(txtwrong_other.Text) & "' ,"

            sql &= " '" & addslashes(txtcorrect_hn.Text) & "' ,"
            sql &= " '" & addslashes(txtcorrect_doc_name.Text) & "' ,"
            sql &= " '" & addslashes(txtcorrect_doctor.Text) & "' ,"
            sql &= " '" & addslashes(txtcorrect_doc_no.Text) & "' ,"
            sql &= " '" & addslashes(txtcorrect_other.Text) & "' ,"

            sql &= " '" & convertToSQLDatetime(txtdate1.Text, txthour.SelectedValue.PadLeft(2, "0"), txtmin.SelectedValue.PadLeft(2, "0")) & "' ,"
            sql &= " '" & ConvertDateStringToTimeStamp(txtdate1.Text, CInt(txthour.SelectedValue), CInt(txtmin.SelectedValue)) & "' ,"
            sql &= " '" & convertToSQLDatetime(txtdate2.Text, txthour2.SelectedValue.PadLeft(2, "0"), txtmin2.SelectedValue.PadLeft(2, "0")) & "' ,"
            sql &= " '" & ConvertDateStringToTimeStamp(txtdate2.Text, CInt(txthour2.SelectedValue), CInt(txtmin2.SelectedValue)) & "' ,"

            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' ,"
            sql &= " '" & addslashes(Session("user_fullname")) & "' ,"

            If mode = "add" Then
                sql &= " '" & Session.SessionID & "' "
            Else
                sql &= " null "
            End If

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            txtwrong_hn.Text = ""
            txtwrong_doc_name.Text = ""
            txtwrong_doc_no.Text = ""
            txtwrong_doctor.Text = ""
            txtwrong_other.Text = ""
            txtdate1.Text = ""
            txthour.SelectedIndex = 0
            txtmin.SelectedIndex = 0

            txtcorrect_hn.Text = ""
            txtcorrect_doc_name.Text = ""
            txtcorrect_doc_no.Text = ""
            txtcorrect_doctor.Text = ""
            txtcorrect_other.Text = ""
            txtdate2.Text = ""
            txthour2.SelectedIndex = 0
            txtmin2.SelectedIndex = 0
            ' Response.Write(sql)
           
            bindDeleteScan()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub gridDeleteScan_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gridDeleteScan.RowDeleting
        Dim sql As String
        Dim result As String
        Try
            sql = "DELETE FROM ir_delete_scan_detail WHERE delete_scan_id = " & gridDeleteScan.DataKeys(e.RowIndex).Value & ""
            result = conn.executeSQLForTransaction(sql)

            If result <> "" Then
                Throw New Exception(result)
            End If

            conn.setDBCommit()
            bindDeleteScan()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
