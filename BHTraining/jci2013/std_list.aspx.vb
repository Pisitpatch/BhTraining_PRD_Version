Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci2013_std_list
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
            bindEdition()
            bindChapter()
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
            sql = "SELECT * "

            sql &= " FROM jci13_std_list a "
            sql &= " WHERE 1 = 1 "

            If txtedtion.SelectedIndex > 0 Then
                sql &= " AND edition = '" & txtedtion.SelectedValue & "' "
            End If

            If txtchapter.SelectedIndex > 0 Then
                sql &= " AND chapter = '" & txtchapter.SelectedValue & "' "
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

    Sub bindChapter()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT chapter "

            sql &= " FROM jci13_std_list a "
            sql &= " WHERE 1 = 1 "
            sql &= " GROUP BY chapter ORDER BY chapter"

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtchapter.DataSource = ds
            txtchapter.DataBind()

            txtchapter.Items.Insert(0, New ListItem("All Chapter", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindEdition()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT edition "

            sql &= " FROM jci13_std_list a "
            sql &= " WHERE 1 = 1 "
            sql &= " GROUP BY edition ORDER BY edition"

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtedtion.DataSource = ds
            txtedtion.DataBind()

            txtedtion.Items.Insert(0, New ListItem("All Edition", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Response.Redirect("std_upload.aspx?menu=2")
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "onDeleteGroup") Then
            Dim sql As String
            Dim errorMsg As String
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Try
                sql = "DELETE FROM jci13_std_list WHERE std_id = " & index
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                bindGrid()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub txtchapter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtchapter.SelectedIndexChanged
        bindGrid()
    End Sub

    Protected Sub txtedtion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtedtion.SelectedIndexChanged
        bindGrid()
    End Sub
End Class
