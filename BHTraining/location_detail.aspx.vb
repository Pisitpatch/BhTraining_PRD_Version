Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class location_detail
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Private id As String = ""
    Private mode As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            'bindComboDivision()
            ' bindMailGroup()
            'bindForm()
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

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Response.Redirect("location_management.aspx")
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet


        Try
            sql = "SELECT * FROM m_location_new WHERE  1  = 1"
            If id <> "" Then
                sql &= " AND location_id = " & id
            End If
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            txtnamelocal.Text = ds.Tables(0).Rows(0)("location_name").ToString

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Clear()
            ds = Nothing
        End Try
    End Sub

    Protected Sub cmdSaveDept_Click(sender As Object, e As EventArgs) Handles cmdSaveDept.Click
        Dim sql As String
        Dim errorMsg As String

        Try

            If mode = "edit" Then
                sql = "UPDATE m_location_new SET location_name = '" & addslashes(txtnamelocal.Text) & "'"
                sql &= " WHERE location_id = " & id
                'Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)

                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Else

                sql = "INSERT INTO m_location_new (location_name) VALUES("
                sql &= " '" & addslashes(txtnamelocal.Text) & "'"
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)

                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

            End If

            conn.setDBCommit()

            txtlabel.Text = "Data successfully saved."
            txtlabel.Visible = True
            ' bindCostCenter()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
            Return
        End Try

        If mode = "add" Then
            Response.Redirect("location_management.aspx")
        End If

    End Sub
End Class
