Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports ShareFunction

Partial Class idp_internal_training_list
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Request.QueryString("viewtype")
        Session("viewtype") = viewtype & ""

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If



        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            ' Response.Write(viewtype)
            lblApproveName.Text = Session("user_fullname").ToString

            If viewtype = "hr" Then
                div_hr.Visible = True
                div_dept.Visible = False
            Else
                ' Response.Write("Xxxxxx")
                div_hr.Visible = False
                div_dept.Visible = False
            End If

            If viewtype = "dept" Then
                div_dept.Visible = True
            End If

            If viewtype = "" Then
                GridView1.Columns(0).Visible = False
            End If

         
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

    Protected Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Response.Redirect("internal_training_detail.aspx?mode=add")
    End Sub

    Sub onCheckAll()

        Dim i As Integer


        Dim chk As CheckBox
        Dim h_chk As CheckBox
        h_chk = CType(GridView1.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)
        i = GridView1.Rows.Count

        Try

            For s As Integer = 0 To i - 1


                chk = CType(GridView1.Rows(s).FindControl("chkSelect"), CheckBox)


                If h_chk.Checked Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If


            Next s


        Catch ex As Exception

            Response.Write(ex.Message)
        End Try
    End Sub
End Class
