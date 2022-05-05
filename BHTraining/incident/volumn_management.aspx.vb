Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction
Partial Class incident_volumn_management
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        bindGrid()
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()

        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        Finally
            conn = Nothing
        End Try
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "DELETE FROM ir_volumn WHERE volumn_month = " & txtmonth.SelectedValue & " AND volumn_year = " & txtyear.SelectedValue
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            sql = "INSERT INTO ir_volumn (volumn_month , month_name , volumn_year , volumn_patient) VALUES("
            sql &= "" & txtmonth.SelectedValue & ","
            sql &= "'" & txtmonth.SelectedItem.Text & "',"
            sql &= "" & txtyear.SelectedValue & " ,"
            sql &= "" & txtvolumn.Text & " "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            bindGrid()
            txtvolumn.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_volumn ORDER BY  volumn_month , volumn_year"
            ds = conn.getDataSetForTransaction(sql, "t1")

            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim sql As String
        Dim result As String
        'Dim key As String()
        Try
            'key = GridView1.DataKeys(e.RowIndex).Value.ToString.Split(",")

            'Response.Write(GridView1.DataKeys(e.RowIndex)(1).ToString)
            sql = "DELETE FROM ir_volumn WHERE volumn_month = " & GridView1.DataKeys(e.RowIndex)(0).ToString & " AND volumn_year = " & GridView1.DataKeys(e.RowIndex)(1).ToString
            result = conn.executeSQLForTransaction(sql)

            If result <> "" Then
                Response.Write(result)
            End If

            conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
