Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class incident_run_no
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Dim irun As Integer = 103
        Try
            ' Run IR NO 4/8/2013
            sql = "select b.ir_no , * from ir_trans_list a inner join ir_detail_tab b on a.ir_id = b.ir_id "
            sql &= " where Day(date_submit) = 4 And Month(date_submit) = 8 And Year(date_submit) = 2013 And a.status_id >= 2"
            sql &= " and b.ir_no > 201308040102 "
            sql &= " order by a.ir_id asc"

            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(sql & "<br/>")
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                sql = "UPDATE ir_detail_tab SET ir_no =  '" & "20130804" & irun.ToString.PadLeft(4, "0") & "' "
                sql &= " WHERE ir_id = " & ds.Tables("t1").Rows(i)("ir_id").ToString
                Response.Write(sql & "<br/>")
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If

                irun = irun + 1
                ' Response.Write(getNewIRNoYesterday() & "<br/>")
            Next

            ' Run IR NO 5/8/2013
            sql = "select b.ir_no , * from ir_trans_list a inner join ir_detail_tab b on a.ir_id = b.ir_id "
            sql &= " where Day(date_submit) = 5 And Month(date_submit) = 8 And Year(date_submit) = 2013 And a.status_id >= 2"
            ' sql &= " and b.ir_no = 0 "
            sql &= " order by a.ir_id asc "

            ds = conn.getDataSetForTransaction(sql, "t1")
            ' Response.Write(sql & "<br/>")
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                sql = "UPDATE ir_detail_tab SET ir_no =  '" & "20130805" & irun.ToString.PadLeft(4, "0") & "' "
                sql &= " WHERE ir_id = " & ds.Tables("t1").Rows(i)("ir_id").ToString
                Response.Write(sql & "<br/>")
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
                ' Response.Write(getNewIRNoYesterday() & "<br/>")
                irun = irun + 1
            Next

            conn.setDBRollback()
            ' conn.setDBCommit()
        Catch ex As Exception

            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
        

    End Sub


    Function getNewIRNoYesterday() As String
        Dim sql As String
        Dim dsIR As New DataSet

        Dim new_ir_no As String = 0
        Dim run_no As Long
        Try
            Dim yyyymmdd As String = CStr(Date.Now.Year) & Date.Now.Month.ToString.PadLeft(2, "0") ' & Date.Now.Day.ToString.PadLeft(2, "0")


            sql = "SELECT ir_no , RIGHT(ir_no,4) AS running FROM ir_detail_tab WHERE ir_no LIKE '" & yyyymmdd & "%' ORDER BY ir_no DESC"
            dsIR = conn.getDataSetForTransaction(sql, "t1")
            If dsIR.Tables(0).Rows.Count > 0 Then
                run_no = CLng(Trim(dsIR.Tables(0).Rows(0)("running").ToString)) + 1
                new_ir_no = CLng(yyyymmdd & "04") & run_no.ToString.PadLeft(4, "0")
            Else
                new_ir_no = yyyymmdd & Date.Now.Day.ToString.PadLeft(2, "0") & "0001"
            End If

            'Dim d As New Date(new_ir_no
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            dsIR.Dispose()

        End Try

        Return new_ir_no
    End Function

    Function getNewIRNoToday() As String
        Dim sql As String
        Dim dsIR As New DataSet

        Dim new_ir_no As String = 0
        Dim run_no As Long
        Try
            Dim yyyymmdd As String = CStr(Date.Now.Year) & Date.Now.Month.ToString.PadLeft(2, "0") ' & Date.Now.Day.ToString.PadLeft(2, "0")


            sql = "SELECT ir_no , RIGHT(ir_no,4) AS running FROM ir_detail_tab WHERE ir_no LIKE '" & yyyymmdd & "%' ORDER BY ir_no DESC"
            dsIR = conn.getDataSetForTransaction(sql, "t1")
            If dsIR.Tables(0).Rows.Count > 0 Then
                run_no = CLng(Trim(dsIR.Tables(0).Rows(0)("running").ToString)) + 1
                new_ir_no = CLng(yyyymmdd & "05") & run_no.ToString.PadLeft(4, "0")
            Else
                new_ir_no = yyyymmdd & Date.Now.Day.ToString.PadLeft(2, "0") & "0001"
            End If

            'Dim d As New Date(new_ir_no
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            dsIR.Dispose()

        End Try

        Return new_ir_no
    End Function

    Protected Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

    End Sub
End Class
