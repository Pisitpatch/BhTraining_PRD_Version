Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class ir_customform_element_detail
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    'Private conn1 As DBUtil = CType(Session("myConn"), DBUtil)
    Protected id As String
    Protected subelement As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        id = Request.QueryString("id")
        subelement = Request.QueryString("subelement")
      

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            bindGridElement()
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

    Sub bindGridElement()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT *  FROM ir_formcustom_element_item WHERE element_id = " & subelement
            sql &= " ORDER BY item_order_sort ASC"
            reader = conn.getDataReaderForTransaction(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            GridViewItem.DataSource = reader
            GridViewItem.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write("bindGridElement :: ")
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Protected Sub cmdAddItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddItem.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try
            pk = getPK("sub_element_id", "ir_formcustom_element_item", conn)

            sql = "INSERT INTO ir_formcustom_element_item (sub_element_id ,element_id,item_label_th,item_label_en,item_order_sort) VALUES("
            sql &= "" & pk & ","
            sql &= "" & subelement & ","
            sql &= "'" & txtnewitemth.Text & "',"
            sql &= "'" & txtnewitemen.Text & "',"
            sql &= "" & 1 & ""
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            bindGridElement()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridViewItem_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewItem.RowCancelingEdit
        GridViewItem.EditIndex = -1
        bindGridElement()
    End Sub

    Protected Sub GridViewItem_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewItem.RowDeleting
        Dim sql As String
        Dim result As String
        sql = "DELETE FROM ir_formcustom_element_item WHERE sub_element_id = " & GridViewItem.DataKeys(e.RowIndex).Value
        result = conn.executeSQLForTransaction(sql)

        If result <> "" Then
            conn.setDBRollback()
            Response.Write(result)
        Else
            conn.setDBCommit()
            bindGridElement()
        End If
    End Sub

    Protected Sub GridViewItem_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridViewItem.RowEditing
        GridViewItem.EditIndex = e.NewEditIndex
        bindGridElement()
    End Sub

    Protected Sub GridViewItem_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewItem.RowUpdating
        Dim sql As String
        Dim result As String
        Dim row As GridViewRow = CType(GridViewItem.Rows(e.RowIndex), GridViewRow)
        Dim txtlabelth As TextBox = CType(row.FindControl("txtlabelth"), TextBox)
        Dim txtlabelen As TextBox = CType(row.FindControl("txtlabelen"), TextBox)
        Dim txtorder As TextBox = CType(row.FindControl("txtorder"), TextBox)

        GridViewItem.EditIndex = -1

        Try

            sql = "UPDATE ir_formcustom_element_item SET "
            sql &= " item_label_th = '" & txtlabelth.Text & "' ,"
            sql &= " item_label_en = '" & txtlabelen.Text & "' ,"
            sql &= " item_order_sort = " & txtorder.Text & " "
          
            sql &= " WHERE sub_element_id = " & GridViewItem.DataKeys(e.RowIndex).Value
            'response.write(sql)
            result = conn.executeSQLForTransaction(sql)
            If result <> "" Then
                Response.Write(result)
            Else
                bindGridElement()
            End If

            conn.setDBCommit()
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub GridViewItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewItem.SelectedIndexChanged

    End Sub

    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Response.Redirect("ir_customform_wizard2.aspx?id=" & id)
    End Sub
End Class
