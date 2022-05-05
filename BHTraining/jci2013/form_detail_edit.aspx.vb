Imports System.IO
Imports System.Data
Imports ShareFunction


Partial Class jci2013_form_detail_edit
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""
    Protected mode As String = ""
    Protected subid As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")
        subid = Request.QueryString("subid")
        If IsPostBack Then

        Else ' First time load

            bindForm()

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

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_std_select a "
            sql &= " WHERE me_id =  " & subid

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblType.Text = ds.Tables("t1").Rows(0)("type").ToString
            lblEdition.Text = ds.Tables("t1").Rows(0)("edition").ToString
            lblSectionNo.Text = ds.Tables("t1").Rows(0)("section_no").ToString
            lblSectionName.Text = ds.Tables("t1").Rows(0)("section_name").ToString
            lblChapter.Text = ds.Tables("t1").Rows(0)("chapter").ToString
            lblChapterName.Text = ds.Tables("t1").Rows(0)("chapter_name").ToString
            lblGoal.Text = ds.Tables("t1").Rows(0)("goal").ToString
            lblStdNo.Text = ds.Tables("t1").Rows(0)("std_no").ToString
            lblStdName.Text = ds.Tables("t1").Rows(0)("std_detail").ToString
            lblMeNo.Text = ds.Tables("t1").Rows(0)("measure_element_no").ToString
            lblMEDetail.Text = ds.Tables("t1").Rows(0)("measure_element_detail").ToString
            txtcriteria.Text = ds.Tables("t1").Rows(0)("criteria").ToString
            txtmethod.Text = ds.Tables("t1").Rows(0)("method").ToString


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Response.Redirect("form_detail.aspx?id=" & id & "&menu=3&mode=edit")
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE jci13_std_select SET criteria = '" & txtcriteria.Text & "' "
            sql &= " , method = '" & txtmethod.Text & "' "
            sql &= " WHERE me_id = " & subid

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

        Response.Redirect("form_detail.aspx?id=" & id & "&menu=3&mode=edit")
    End Sub
End Class
