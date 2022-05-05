Imports System.Data.SqlClient
Imports System.Data

Partial Class user_management
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    'Private conn1 As DBUtil = CType(Session("myConn"), DBUtil)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Response.Write("session = " & Session("mobule_list").ToString)

        If IsNothing(Session("mobule_list")) Then

            Return
            Response.Redirect("login.aspx")

        End If

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If
        'Dim sql As String
        'Dim errorMsg As String
        'sql = "DELETE FROM dbo.user_fte"
        'Try
        '    errorMsg = conn.executeSQL(sql)
        '    If errorMsg <> "" Then
        '        Throw New Exception(errorMsg)
        '    End If
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
      
        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            bindDept()
            bindCostCenter()
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
            sql = "SELECT * FROM  user_profile WHERE 1 = 1 "
            If txtid.Text <> "" Then
                sql &= " AND emp_code LIKE '%" & Trim(txtid.Text) & "%'"
            End If

            If txtcostcenter.SelectedValue <> "" Then
                sql &= " AND costcenter_id = " & txtcostcenter.SelectedValue
            End If

            If txtname.Text <> "" Then
                sql &= " AND (LOWER(user_fullname) LIKE '%" & Trim(txtname.Text).ToLower & "%' OR LOWER(user_fullname_local) LIKE '%" & Trim(txtname.Text).ToLower & "%')"
            End If

            If txtdept.SelectedValue <> "" Then
                sql &= " AND dept_id = " & txtdept.SelectedValue
            End If

            ds = conn.getDataSet(sql, "table1")

            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
       
    End Sub

    Sub bindDept()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM user_dept "
            sql &= " ORDER BY dept_name_en"
            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txtdept.DataSource = reader
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("--All Department--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindCostCenter()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT costcenter_id FROM user_profile WHERE 1 = 1 "
            If txtdept.SelectedValue <> "" Then
                sql &= " AND dept_name = '" & txtdept.SelectedValue & "'"
            End If
            sql &= " GROUP BY costcenter_id ORDER BY costcenter_id"
            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txtcostcenter.DataSource = reader
            txtcostcenter.DataBind()

            txtcostcenter.Items.Insert(0, New ListItem("--All Cost Center--", ""))
            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
          

            Dim txtstatusid As Label = CType(e.Row.FindControl("txtstatusid"), Label)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)


            If txtstatusid.Text = "1" Then
                lblStatus.Text = "Enable"
                lblStatus.ForeColor = Drawing.Color.Green
            Else
                lblStatus.Text = "Disable"
                lblStatus.ForeColor = Drawing.Color.Red
            End If
            ' e.Row.ForeColor = Drawing.Color.PowderBlue

        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting

    End Sub

    Protected Sub cmdFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        bindGrid()
    End Sub

    Protected Sub txtdept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdept.SelectedIndexChanged
        bindCostCenter()
    End Sub

    Protected Sub cmdUpdateFTE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateFTE.Click

    End Sub
End Class
