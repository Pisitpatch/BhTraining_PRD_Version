Imports System.IO
Imports System.Data
Imports ShareFunction


Partial Class jci_admin_review_question
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected tid As String
    Protected empcode As String
    Protected pageID As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("jci_emp_code")) Then
            Response.Redirect("login.aspx")
            'Response.Write("Please re-login again")
            Response.End()
        End If

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        pageID = Request.QueryString("pageID")
        Session("page_id") = pageID
        empcode = Request.QueryString("empcode")
        tid = Request.QueryString("tid")

        If IsPostBack Then

        Else ' First time load

            bindGrid()
            bindPathWay()
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
            sql = "SELECT * FROM jci_trans_list a INNER JOIN jci_master_question b ON a.question_id = b.question_id "
            sql &= "  WHERE b.test_id = " & tid & " AND a.trans_create_by_emp_code = " & empcode
            sql &= " "
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindPathWay()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM jci_trans_list a WHERE a.trans_create_by_emp_code = " & empcode
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblPathWay.Text = " > " & " <a href='admin_review_user_list.aspx?tid=" & tid & "'>Review Video Answer</a> > " & ds.Tables("t1").Rows(0)("trans_create_by_name").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        ' Response.Write(e.Row.RowType)

        Try
            ' Dim imgSelect1 As Image = CType(e.Row.FindControl("imgSelect1"), Image)
            'imgSelect1.Visible = False

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim imgSelect1 As Image = CType(e.Row.FindControl("imgSelect1"), Image)
                Dim imgSelect2 As Image = CType(e.Row.FindControl("imgSelect2"), Image)
                Dim imgSelect3 As Image = CType(e.Row.FindControl("imgSelect3"), Image)
                Dim lblScore As Label = CType(e.Row.FindControl("lblScore"), Label)
                ' Response.Write(lblScore.Text)
                If lblScore.Text = "1" Then
                    imgSelect1.Visible = True
                    imgSelect2.Visible = False
                    imgSelect3.Visible = False
                ElseIf lblScore.Text = "2" Then
                    imgSelect1.Visible = False
                    imgSelect2.Visible = True
                    imgSelect3.Visible = False
                ElseIf lblScore.Text = "3" Then
                    imgSelect1.Visible = False
                    imgSelect2.Visible = False
                    imgSelect3.Visible = True
                Else
                    imgSelect1.Visible = False
                    imgSelect2.Visible = False
                    imgSelect3.Visible = False
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
