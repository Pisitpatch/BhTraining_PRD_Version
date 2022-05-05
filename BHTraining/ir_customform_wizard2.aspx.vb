Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class ir_customform_wizard2
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    'Private conn1 As DBUtil = CType(Session("myConn"), DBUtil)
    Protected id As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        id = Request.QueryString("id")

        If id = "" Then
            Response.Write("No id")
            Return
        End If

        If txtid.Text = "" Then
            cmdSave.Visible = False
            cmdCancel.Visible = False
        Else
            cmdSave.Visible = True
            cmdCancel.Visible = True
        End If

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            cmdItem.Visible = False
            bindGridElement()
            bindFieldType()
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

    Sub bindFieldType()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT * FROM ir_formcustom_element_type "
            reader = conn.getDataReaderForTransaction(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            txtelementtype.DataSource = reader
            txtelementtype.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Sub bindGridElement()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT * , b.element_type_name AS type_name FROM ir_formcustom_element_list a INNER JOIN ir_formcustom_element_type b ON a.element_type_id = b.element_type_id WHERE a.form_id = " & id
            sql &= " ORDER BY a.element_order_sort ASC"
            reader = conn.getDataReaderForTransaction(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            GridView1.DataSource = reader
            GridView1.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write("bindGridElement :: ")
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Protected Sub cmdOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOrder.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim order As TextBox


        i = GridView1.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridView1.Rows(s).FindControl("lblPK"), Label)
                order = CType(GridView1.Rows(s).FindControl("txtorder"), TextBox)
                '  response.write(lbl.Text)
                
                sql = "UPDATE ir_formcustom_element_list SET element_order_sort = " & order.Text & " WHERE element_id = " & lbl.Text


                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
            Next s

            conn.setDBCommit()

            bindGridElement()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("cmdOrder_Click :: ")
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            pk = getPK("element_id", "ir_formcustom_element_list", conn)

            sql = "INSERT INTO ir_formcustom_element_list (element_id,form_id,element_name_th,element_name_en,element_type_id,element_type_name,element_order_sort) VALUES("
            sql &= "" & pk & ","
            sql &= "" & id & ","
            sql &= "'" & txtnameth.Text & "',"
            sql &= "'" & txtnameen.Text & "',"
            sql &= "" & txtelementtype.SelectedValue & ","
            sql &= "'" & "" & "',"
            sql &= "" & txtelementorder.Text & ""
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            bindGridElement()
            clearForm()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(sql)
            Response.Write("cmdAdd_Click :: ")
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim sql As String
        Dim ds As New DataSet

        If (e.CommandName = "selectRow") Then
           
            Dim CommandArgument As String() = e.CommandArgument.Split(",")

            Dim element_id As String = CommandArgument(0)
            Dim index As String = CommandArgument(1)
            ' Response.Write(e.CommandArgument)
            Try
                Sql = "SELECT * FROM ir_formcustom_element_list WHERE element_id = " & element_id
                ds = conn.getDataSetForTransaction(Sql, "t1")
                txtelementtype.SelectedValue = ds.Tables(0).Rows(0)("element_type_id").ToString
                txtnameen.Text = ds.Tables(0).Rows(0)("element_name_en").ToString
                txtnameth.Text = ds.Tables(0).Rows(0)("element_name_th").ToString
                txtelementorder.Text = ds.Tables(0).Rows(0)("element_order_sort").ToString
                txtid.Text = ds.Tables(0).Rows(0)("element_id").ToString

                cmdSave.Visible = True
                cmdCancel.Visible = True
                cmdAdd.Visible = False
                checkSubItem()
            Catch ex As Exception
                cmdSave.Visible = False
                cmdCancel.Visible = False
                cmdAdd.Visible = True
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try

            bindGridElement()
            GridView1.Rows(index - 1).BackColor = Drawing.Color.Yellow
        End If

        If (e.CommandName = "Delete") Then
            'sql = "DELETE FROM ir_formcustom_element_list WHERE element_id = " & e.CommandArgument.ToString & ""
            'conn.executeSQLForTransaction(sql)



            'bindGridElement()

        End If
    End Sub

    Sub clearForm()
        txtid.Text = ""
        txtnameen.Text = ""
        txtnameth.Text = ""
        txtelementorder.Text = 1
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim sql As String
        Dim result As String
        sql = "DELETE FROM ir_formcustom_element_list WHERE element_id = " & GridView1.DataKeys(e.RowIndex).Value & ""
        result = conn.executeSQLForTransaction(sql)

        If result <> "" Then

            Response.Write(result)
            conn.setDBRollback()
        Else
            conn.setDBCommit()
            bindGridElement()
            clearForm()

            cmdAdd.Visible = True
            cmdItem.Visible = False
            cmdSave.Visible = False
            cmdCancel.Visible = False
        End If
    End Sub



    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Try
            sql = "UPDATE ir_formcustom_element_list SET "
            sql &= " element_name_th = '" & txtnameth.Text & "' ,"
            sql &= " element_name_en = '" & txtnameen.Text & "' ,"
            sql &= " element_order_sort = " & txtelementorder.Text & " "
            sql &= " WHERE element_id = " & txtid.Text

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            bindGridElement()
            clearForm()
            cmdSave.Visible = False
            cmdCancel.Visible = False
            cmdItem.Visible = False
            cmdAdd.Visible = True
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdPrevStep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrevStep.Click
        Response.Redirect("ir_customform_wizard1.aspx?id=" & id)
    End Sub

    Private Sub checkSubItem()
        Dim type As Integer = 0
        type = CInt(txtelementtype.SelectedValue)

        If (type = 5 Or type = 3 Or type = 4) And txtid.Text <> "" Then ' checkbox
            cmdItem.Visible = True
        Else
            cmdItem.Visible = False
        End If
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        cmdSave.Visible = False
        cmdCancel.Visible = False
        cmdAdd.Visible = True
        cmdItem.Visible = False
        clearForm()
        bindGridElement()
    End Sub

    Protected Sub cmdItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdItem.Click
        Response.Redirect("ir_customform_element_detail.aspx?id=" & id & "&subelement=" & txtid.Text)
    End Sub

    Protected Sub cmdNextStep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNextStep.Click
        Response.Redirect("ir_customform_wizard3.aspx?id=" & id)
    End Sub

    Protected Sub txtelementtype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtelementtype.SelectedIndexChanged

    End Sub
End Class
