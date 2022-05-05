Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class srp_srp_reserve
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If


        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Page.IsPostBack Then
        Else ' First time load
            bindDept()
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT *  "
            sql &= " FROM shop_item_reserve a INNER JOIN user_profile b ON a.emp_code = b.emp_code  "
            sql &= " INNER JOIN shop_master_item c ON a.inv_item_id = c.inv_item_id "
            sql &= "  WHERE 1 = 1 "

            If txtfind_emp.Text <> "" Then
                sql &= " AND (b.user_fullname LIKE '%" & txtfind_emp.Text & "%'  OR b.user_fullname_local LIKE '%" & txtfind_emp.Text & "%' OR a.emp_code LIKE '%" & txtfind_emp.Text & "%') "
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND reserve_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text)
            End If
            If txtdept.SelectedValue <> "" Then
                sql &= " AND b.dept_id = " & txtdept.SelectedValue
            End If
          

            sql &= " ORDER BY reserve_id DESC "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            gridview1.DataSource = ds
            gridview1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDept()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM user_dept ORDER BY dept_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdept.DataSource = ds
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Public Shared Sub Export(ByVal fileName As String, ByVal gv As GridView)


        HttpContext.Current.Response.Clear()

        'Export will take two parameter first one the name of Excel File, and second one for gridview to be exported

        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))

        ' HttpContext.Current.Response.ContentType = "application/octet-stream"
        HttpContext.Current.Response.ContentType = " application/vnd.ms-excel"

        HttpContext.Current.Response.Charset = "TIS-620"

        Using strWriter As New StringWriter()


            Using htmlWriter As New HtmlTextWriter(strWriter)


                ' Create a form to contain the grid

                Dim table As New Table()

                ' add the header row to the table

                If gv.HeaderRow IsNot Nothing Then


                    ExportControl(gv.HeaderRow)


                    table.Rows.Add(gv.HeaderRow)
                End If

                ' add each of the data rows to the table

                For Each row As GridViewRow In gv.Rows


                    ExportControl(row)


                    table.Rows.Add(row)
                Next

                ' add the footer row to the table

                If gv.FooterRow IsNot Nothing Then


                    ExportControl(gv.FooterRow)


                    table.Rows.Add(gv.FooterRow)
                End If

                ' render the table into the htmlwriter

                table.RenderControl(htmlWriter)

                ' render the htmlwriter into the response

                HttpContext.Current.Response.Write(strWriter.ToString())


                HttpContext.Current.Response.[End]()

            End Using
        End Using

    End Sub

    Private Shared Sub ExportControl(ByVal control As Control)


        For i As Integer = 0 To control.Controls.Count - 1


            Dim current As Control = control.Controls(i)

            If TypeOf current Is LinkButton Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, LinkButton).Text))

            ElseIf TypeOf current Is ImageButton Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, ImageButton).AlternateText))

            ElseIf TypeOf current Is HyperLink Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, HyperLink).Text))

            ElseIf TypeOf current Is DropDownList Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, DropDownList).SelectedItem.Text))

            ElseIf TypeOf current Is CheckBox Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(If(TryCast(current, CheckBox).Checked, "True", "False")))
            End If

            'Like that you may convert any control to literals

            If current.HasControls() Then



                ExportControl(current)

            End If
        Next

    End Sub

    Protected Sub gridview1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridview1.PageIndexChanging
        gridview1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Protected Sub gridview1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridview1.RowCommand
        Dim sql As String
        Dim errorMsg As String = ""

        If (e.CommandName = "cancelCommand") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            ' Dim row As GridViewRow = GridView1.Rows(index)


            ' Add code here to add the item to the shopping cart.
            Try
                sql = "DELETE shop_item_reserve WHERE reserve_id = " & index
                conn.executeSQLForTransaction(sql)
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                conn.setDBCommit()
                bindGrid()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try

        ElseIf (e.CommandName = "cancelCommandByTQM") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            ' Dim row As GridViewRow = GridView1.Rows(index)


            ' Add code here to add the item to the shopping cart.
            Try
                sql = "UPDATE star_trans_list SET is_cancel = 1 WHERE star_id = " & index
                conn.executeSQL(sql)
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                bindGrid()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End If
    End Sub

    Protected Sub gridview1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridview1.SelectedIndexChanged

    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub

    Protected Sub cmdExport_Click(sender As Object, e As System.EventArgs) Handles cmdExport.Click
        Export("reserve_item.xls", gridview1)
    End Sub
 
End Class
