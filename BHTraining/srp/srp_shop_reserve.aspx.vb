Imports System.Data
Imports ShareFunction
Partial Class srp_srp_shop_reserve
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        id = Request.QueryString("id")
    

        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

      

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        If Page.IsPostBack Then

        Else ' load first time
            bindGrid()
            bindPrice()
          
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM shop_item_reserve WHERE inv_item_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            gridview1.DataSource = ds
            gridview1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindPrice()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM shop_master_item WHERE inv_item_id = " & id
            ds = conn.getDataSetForTransaction(Sql, "t1")

            lblTotalCash.Text = ds.Tables("t1").Rows(0)("inv_price").ToString
            lblTotalScore.Text = ds.Tables("t1").Rows(0)("total_point").ToString

            lblName.Text = ds.Tables("t1").Rows(0)("inv_item_name_th").ToString & " / " & ds.Tables("t1").Rows(0)("inv_item_name_en").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try

            pk = getPK("reserve_id", "shop_item_reserve", conn)
            sql = "INSERT INTO shop_item_reserve (reserve_id , inv_item_id , emp_code , emp_name , reserve_item_name_th , reserve_item_name_en "
            sql &= " , reserve_date_ts , reserve_date_raw , reserve_qty , contact_tel , reserve_remark "
            sql &= ") VALUES("
            sql &= " '" & pk & "' , "
            sql &= " '" & id & "' , "
            sql &= " '" & Session("emp_code").ToString & "' , "
            sql &= " '" & Session("user_fullname").ToString & "' , "
            sql &= " '" & "" & "' , "
            sql &= " '" & "" & "' , "
            sql &= " '" & Date.Now.Ticks & "' , "
            sql &= " GETDATE() , "
            sql &= " '" & txtqty.SelectedValue & "' , "
            sql &= " '" & addslashes(txttel.Text) & "' ,"
            sql &= " '" & addslashes(txtsolution.Value) & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            sql = "UPDATE shop_master_item SET reserve_num = ISNULL(reserve_num,0) + " & txtqty.SelectedValue
            sql &= " WHERE inv_item_id = " & id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            bindGrid()
            'Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; window.close();"
            Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; "
            ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub gridview1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview1.PageIndexChanging
        gridview1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub gridview1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridview1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkDelete As LinkButton = CType(e.Row.FindControl("LinkDelete"), LinkButton)
            Dim lblEmpNo As Label = CType(e.Row.FindControl("lblEmpNo"), Label)

            Dim sql As String
            Dim ds As New DataSet
            Try

                If lblEmpNo.Text = Session("emp_code").ToString Then
                    LinkDelete.Visible = True
                Else
                    LinkDelete.Visible = False
                End If


            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub gridview1_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridview1.RowDeleting
        Dim sql As String
        Dim errorMSg As String

        Try
            sql = "DELETE FROM shop_item_reserve WHERE reserve_id = '" & gridview1.DataKeys(e.RowIndex).Value & "'"
            errorMSg = conn.executeSQLForTransaction(sql)
            If errorMSg <> "" Then
                Throw New Exception(errorMSg)
            End If

            conn.setDBCommit()
            bindGrid()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gridview1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview1.SelectedIndexChanged

    End Sub

    Protected Sub cmdCalPoint_Click(sender As Object, e As EventArgs) Handles cmdCalPoint.Click

        Dim rate As String = "60"
        Dim point As Integer
        Dim baht As Integer = 0
        Dim baht2 As Double = 0

        Dim sql = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM shop_master_item WHERE inv_item_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
          
            Double.TryParse(ds.Tables("t1").Rows(0)("inv_price").ToString().Replace(",", ""), baht2)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        'MsgBox(gridPrice.CurrentRow.Cells("column3").Value.ToString)
        If Integer.TryParse(txtpoint.Text, point) Then

            txtbaht.Text = Math.Round((baht2 * CInt(txtqty.SelectedValue)) - Math.Round((point * 60) / 100, 0), 0)

            If CInt(txtbaht.Text) < 0 Then
                'MsgBox("จำนวนคะแนนที่ใส่ไม่ถูกต้องทำให้เงินติดลบ กรุณาใส่คะแนนใหม่")
                txtbaht.Text = 0
                txtpoint.ForeColor = Drawing.Color.Red
                txtpoint.Focus()
            Else
                txtpoint.ForeColor = Drawing.Color.Black
            End If
        Else
            txtpoint.Text = "0"
        End If
    End Sub

    Protected Sub cmdCalBaht_Click(sender As Object, e As EventArgs) Handles cmdCalBaht.Click
        txtpoint.Text = 0

        Dim sql = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM shop_master_item WHERE inv_item_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")

            ' Double.TryParse(ds.Tables("t1").Rows(0)("inv_price").ToString().Replace(",", ""), baht2)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Try
            ' txtbaht.Text = lblTotalCash.Text.Replace(",", "")

            txtbaht.Text = CInt(txtqty.SelectedValue) * Math.Round(CDbl(ds.Tables("t1").Rows(0)("inv_price").ToString()))
        Catch ex As Exception
            txtbaht.Text = "0"
        End Try
    End Sub

    Protected Sub txtqty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtqty.SelectedIndexChanged
        txtpoint.Text = "0"
        txtbaht.Text = "0"
    End Sub
End Class
