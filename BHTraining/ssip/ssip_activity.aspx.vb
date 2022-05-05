Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_ssip_activity
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' First time
            '  bindDept()
            ' bindStatus()
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
            ' sql = "SELECT a.* , b.* , c.ir_status_name , d.* , g.form_name_en , a.date_report AS create_date FROM  ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id  "
            sql = "SELECT *  FROM ssip_activity_list a "
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND rec_emp_code = " & Session("emp_code").ToString
          

            'sql &= " ORDER BY a.ssip_id DESC"
            ds = conn.getDataSet(sql, "table1")

            'Response.Write(sql)
            'Return
            GridView1.DataSource = ds
            GridView1.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try

    End Sub

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("ssip_activity_detail.aspx?mode=add")
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
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
                sql = "UPDATE ssip_activity_list SET is_delete = 1 WHERE record_id = " & index
                conn.executeSQL(sql)
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If

                bindGrid()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        ElseIf (e.CommandName = "cancelCommandByTQM") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            ' Dim row As GridViewRow = GridView1.Rows(index)


            ' Add code here to add the item to the shopping cart.
            Try
                sql = "UPDATE ssip_activity_list SET is_cancel = 1 WHERE record_id = " & index
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

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
