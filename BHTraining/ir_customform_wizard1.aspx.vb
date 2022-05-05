Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class ir_customform_wizard1
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

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            If id <> "" Then
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
            sql = "SELECT * FROM ir_form_master WHERE form_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtnameth.Text = ds.Tables(0).Rows(0)("form_name_th").ToString
            txtnameen.Text = ds.Tables(0).Rows(0)("form_name_en").ToString
        Catch ex As Exception
            Response.Write(sql)
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try

    End Sub

    Protected Sub cmdCreateForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCreateForm.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = "0"


        If id <> "" Then ' update
            Try
                sql = "UPDATE ir_form_master SET form_name_th = '" & txtnameth.Text & "'"
                sql &= ", form_name_en = '" & txtnameen.Text & "'"
                sql &= " WHERE form_id = " & id

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
                conn.setDBCommit()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
                Return
            End Try

        Else ' add 
            Try
                pk = getPK("form_id", "ir_form_master", conn)

                sql = "INSERT INTO ir_form_master (form_id , form_name_th , form_name_en , is_custom_form) VALUES("
                sql &= "" & pk & ","
                sql &= "'" & txtnameth.Text & "',"
                sql &= "'" & txtnameen.Text & "',"
                sql &= "" & 1 & ""
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()

                id = pk
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
                Return
            End Try

        End If

        Response.Redirect("ir_customform_wizard2.aspx?id=" & id)
    End Sub

    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Response.Redirect("ir_customform_list.aspx")
    End Sub
End Class
