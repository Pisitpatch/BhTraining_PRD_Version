Imports System.IO
Imports System.Data
Imports ShareFunction

Partial Class jci2013_std_detail
    Inherits System.Web.UI.Page
    Protected id As String = ""
    Protected mode As String = ""
    Protected subid As String = ""

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

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

            If mode = "edit" Then
                bindForm()
            End If

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

            sql &= " FROM jci13_std_list a "
            sql &= " WHERE std_id =  " & id

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
         


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Response.Redirect("std_list.aspx?menu=2")
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE jci13_std_list SET type = '" & lblType.Text & "' "
            sql &= " , edition = '" & lblEdition.Text & "' "
            sql &= " , section_no = '" & lblSectionNo.Text & "' "
            sql &= " , section_name = '" & lblSectionName.Text & "' "
            sql &= " , chapter = '" & lblChapter.Text & "' "
            sql &= " , chapter_name = '" & lblChapterName.Text & "' "
            sql &= " , goal = '" & lblGoal.Text & "' "
            sql &= " , std_no = '" & lblStdNo.Text & "' "
            sql &= " , std_detail = '" & lblStdName.Text & "' "
            sql &= " , measure_element_no = '" & lblMeNo.Text & "' "
            sql &= " , measure_element_detail = '" & lblMEDetail.Text & "' "

            sql &= " WHERE std_id = " & id

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

        Response.Redirect("std_list.aspx?menu=2")
    End Sub
End Class
