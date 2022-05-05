Imports System.Data.SqlClient
Imports System.Data
Partial Class dept_management
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    'Private conn1 As DBUtil = CType(Session("myConn"), DBUtil)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            bindComboPosition()
            bindComboDivision()
            bindGrid()
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

    Private Sub bindGrid()
        Dim ds As New DataSet
        Dim sql As String
        Try
            sql = "SELECT a.* , b.division_name_en , b.division_name_th FROM  user_dept a LEFT OUTER JOIN user_division b ON a.division_id = b.division_id WHERE 1 = 1 "

            ds = conn.getDataSet(sql, "table1")

            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub

    Private Sub bindComboDivision()
        Dim ds As New DataSet
        Dim sql As String
        Try
            sql = "SELECT * FROM  user_division WHERE 1 = 1 ORDER BY division_name_en "

            ds = conn.getDataSet(sql, "table1")

            txtdivision.DataSource = ds
            txtdivision.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub

    Private Sub bindComboPosition()
        Dim ds As New DataSet
        Dim sql As String
        Try
            sql = "SELECT * FROM  user_position WHERE 1 = 1 ORDER BY priority "

            ds = conn.getDataSet(sql, "table1")

            txtposition.DataSource = ds
            txtposition.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim sql As String
            Dim ds As New DataSet
            Dim lblEmail As Label = CType(e.Row.FindControl("lblEmail"), Label)
            Dim lblPk As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim temp As String = ""
            Try
                sql = "SELECT * FROM user_mail_group WHERE dept_id = " & lblPk.Text
                ' Response.Write(sql)
                ds = conn.getDataSet(sql, "t1")
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If
                lblEmail.Text = ""
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                    temp = (ds.Tables("t1").Rows(i)("full_name").ToString & " &#60;" & ds.Tables("t1").Rows(i)("email").ToString & "&#62; <br/>")
                    '   Response.Write(i)
                    lblEmail.Text &= temp
                Next i

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub cmdEditDivision_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEditDivision.Click
        Response.Redirect("division_detail.aspx?mode=edit&id=" & txtdivision.SelectedValue)
    End Sub
End Class
