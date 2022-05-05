Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_popup_report_internal_summary
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        Session("viewtype") = "ha"
        id = Request.QueryString("id")
    

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' First time load
            bindgrid()
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

    Sub bindgrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , case when isnull(c.idp_id,'') = '' then 'No' else 'Yes' end AS attend "
            sql &= " , CASE WHEN ISNULL(d.Employee_id,0) > 0 THEN 'Active' Else 'Inactive' END AS emp_status"
            sql &= " FROM idp_training_employee a INNER JOIN user_profile b ON a.emp_code=b.emp_code "
            sql &= " left outer join (select s2.idp_id , s1.emp_code from  idp_training_registered s1 inner join idp_training_schedule s2 on s1.schedule_id = s2.schedule_id "
            sql &= " where s1.is_register = 1 "
            sql &= " group by s2.idp_id , s1.emp_code "

            sql &= ") c on a.idp_id = c.idp_id and a.emp_code = c.emp_code "
            sql &= " LEFT OUTER JOIN temp_BHUser d ON a.emp_code = d.Employee_id "
            sql &= " WHERE a.idp_id = " & id
            'Response.Write(sql)
            'Return
            ds = conn.getDataSet(sql, "t1")
            Gridview1.DataSource = ds
            Gridview1.DataBind()

            lblNum.Text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub Gridview1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Gridview1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblAttend = CType(e.Row.FindControl("lblAttend"), Label)
            If lblAttend.Text = "No" Then
                e.Row.BackColor = Drawing.Color.Yellow
            End If
        End If
    End Sub

    Protected Sub Gridview1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles Gridview1.SelectedIndexChanged

    End Sub
End Class
