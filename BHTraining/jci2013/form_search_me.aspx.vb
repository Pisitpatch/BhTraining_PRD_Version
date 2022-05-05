Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci2013_form_search_me
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""
    Protected mode As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")

        If IsPostBack Then

        Else ' First time load
            bindEdition()
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

    Sub bindEdition()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT edition "

            sql &= " FROM jci13_std_list s "
            sql &= " GROUP BY edition "

           
            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtedition.DataSource = ds
            txtedition.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_std_list a "
            sql &= " WHERE edition =  " & txtedition.SelectedValue

          

            If txtsearch.Text <> "" Then
                sql &= " AND (section_name LIKE '%" & txtsearch.Text & "%'"
                sql &= " OR chapter_name LIKE '%" & txtsearch.Text & "%'"
                sql &= " OR goal LIKE '%" & txtsearch.Text & "%'"
                sql &= " OR std_detail LIKE '%" & txtsearch.Text & "%'"
                sql &= ")"
            End If
            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView1.DataSource = ds
            GridView1.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSaveME_Click(sender As Object, e As EventArgs) Handles cmdSaveME.Click
        Dim sql As String = ""
        Dim errorMsg As String = ""
        Dim pk As String = ""
        Dim chk As CheckBox
        Dim lblPK As Label
        Try
            For i As Integer = 0 To GridView1.Rows.Count - 1

                chk = CType(GridView1.Rows(i).FindControl("chk"), CheckBox)
                lblPK = CType(GridView1.Rows(i).FindControl("lblPK"), Label)

                If chk.Checked Then
                    'Response.Write(i)
                    pk = getPK("me_id", "jci13_std_select", conn)

                    sql = "INSERT INTO jci13_std_select (me_id , form_id , edition , section_no , chapter , std_no , measure_element_no , section_name , goal , std_detail , measure_element_detail , std_id , type , chapter_name) "
                    sql &= "SELECT " & pk & " , " & id & " , edition , section_no , chapter , std_no , measure_element_no  , section_name , goal , std_detail , measure_element_detail , std_id , type , chapter_name"
                    sql &= " FROM jci13_std_list WHERE std_id = " & lblPK.Text

                    'Response.Write(sql)
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If
            Next i

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
            Return
        End Try

        Response.Redirect("form_detail.aspx?id=" & id & "&menu=3&mode=edit")
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub
End Class
