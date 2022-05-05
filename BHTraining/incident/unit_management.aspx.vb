Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class incident_unit_management
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' First load
            bindGrid()
        End If
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM m_dept_unit  ORDER BY dept_unit_name "
            ds = conn.getDataSet(sql, "t1")

            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        GridView1.EditIndex = -1
        bindGrid()
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        GridView1.EditIndex = e.NewEditIndex
        bindGrid()

       
    End Sub

    Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView1.RowUpdating
        Dim sql As String
        Dim errorMsg As String
        Dim row As GridViewRow = CType(GridView1.Rows(e.RowIndex), GridViewRow)
        Dim txtdept_name As TextBox = CType(row.FindControl("txtdept_name"), TextBox)
        Try
            GridView1.EditIndex = -1
            sql = "UPDATE m_dept_unit SET dept_unit_name ='" & txtdept_name.Text & "' WHERE dept_unit_id = " & GridView1.DataKeys(e.RowIndex).Value
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            bindGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
            bindGrid()
        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub cmdSaveGrandTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveGrandTopic.Click
        Dim sql As String
        Dim errMsg As String
        Dim pk As String

        Try
            pk = getPK("dept_unit_id", "m_dept_unit", conn)
            sql = "INSERT INTO m_dept_unit (dept_unit_id , dept_unit_name) VALUES("
            sql &= "" & pk & ", "
            sql &= " '" & addslashes(txtadd_name.Text) & "' "
            sql &= " )"

            errMsg = conn.executeSQL(sql)
            If errMsg <> "" Then
                Throw New Exception(errMsg)
            End If

            bindGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
