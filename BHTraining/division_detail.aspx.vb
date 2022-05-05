Imports System.Data

Partial Class division_detail
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    'Private conn1 As DBUtil = CType(Session("myConn"), DBUtil)
    Private id As String = "0"
    Private mode As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

       
        id = Request.QueryString("id")
        mode = Request.QueryString("mode")

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
           
            If mode = "edit" Then
                bindForm()
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

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet


        Try
            sql = "SELECT * FROM user_division WHERE division_id = " & id
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            txtdivision_th.Text = ds.Tables(0).Rows(0)("division_name_th").ToString
            txtdivision_en.Text = ds.Tables(0).Rows(0)("division_name_en").ToString
            txtass_director.Text = ds.Tables(0).Rows(0)("ass_director_name").ToString
            txtdirector.Text = ds.Tables(0).Rows(0)("director_name").ToString
            txtass_chief.Text = ds.Tables(0).Rows(0)("ass_chief_name").ToString
            txtchief.Text = ds.Tables(0).Rows(0)("chief_name").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Clear()
            ds = Nothing
        End Try
    End Sub


    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE user_division SET division_name_th = '" & txtdivision_th.Text & "'"
            sql &= " , division_name_en = '" & txtdivision_en.Text & "'"
            sql &= " , ass_director_name = '" & txtass_director.Text & "'"
            sql &= " , director_name = '" & txtdirector.Text & "' "
            sql &= " , ass_chief_name = '" & txtass_chief.Text & "' "
            sql &= " , chief_name = '" & txtchief.Text & "' "

            sql &= " WHERE division_id = " & id
            'Response.Write(sql)
            errorMsg = conn.executeSQL(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("dept_management.aspx")
    End Sub
End Class
