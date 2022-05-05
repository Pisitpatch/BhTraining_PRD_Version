Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class srp_srp_master
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If


        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If



        If Page.IsPostBack Then
        Else ' First time load
            bindGridBanner()
            bindForm()
            bindGridNews()
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

    Sub bindGridBanner()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM srp_banner WHERE is_active = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            gridBanner.DataSource = ds
            gridBanner.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGridNews()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , case when is_active = 1 then 'Active' else 'Inactive' end as status_name FROM srp_news WHERE ISNULL(is_delete,0) = 0  ORDER BY is_active DESC , new_date_ts DESC "
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridNews.DataSource = ds
            GridNews.DataBind()

          
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM srp_message WHERE srp_message_id = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtnews.Content = ds.Tables("t1").Rows(0)("message_detail").ToString
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPicture.Text = "<img src='../share/star/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' />"
            End If



        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCommittee_Click(sender As Object, e As System.EventArgs) Handles cmdCommittee.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE srp_message SET message_detail = '" & addslashes(txtnews.Content) & "' "
            sql &= " WHERE srp_message_id = 1"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If
            conn.setDBCommit()

            bindForm()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Button3_Click(sender As Object, e As System.EventArgs) Handles Button3.Click
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet

        Try
            If Not IsNothing(FileUpload0.PostedFile) Then
                Dim strFileName = FileUpload0.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filename As String()

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)


                sql = "UPDATE srp_message SET picture_path = '" & strFileName & "' "
                sql &= " WHERE srp_message_id = 1"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUpload0.PostedFile.SaveAs(Server.MapPath("../share/star/" & strFileName))

                conn.setDBCommit()

                bindForm()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub Button4_Click(sender As Object, e As System.EventArgs) Handles Button4.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE srp_message SET picture_path = '' WHERE srp_message_id = 1 "
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            ' bindMessage()
            bindForm()
            lblPicture.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddOTS_Click(sender As Object, e As EventArgs) Handles cmdAddOTS.Click
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet
        Dim pk As String

        Try
            If Not IsNothing(FileUploadOTS.PostedFile) Then
                Dim strFileName = FileUploadOTS.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filename As String()

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)


                pk = getPK("banner_id", "srp_banner", conn)
                sql = "INSERT INTO srp_banner (banner_id , banner_path , banner_type_id , banner_detail , is_active , create_by , create_date ) VALUES("
                sql &= "'" & pk & "' ,"
                sql &= "'" & addslashes(strFileName) & "' ,"
                sql &= " 1 ,"
                sql &= "'" & addslashes(txtots_detail.Text) & "' ,"
                sql &= "1 ,"
                sql &= "'" & Session("user_fullname").ToString & "' ,"
                sql &= " GETDATE() "
              
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                FileUploadOTS.PostedFile.SaveAs(Server.MapPath("../share/ots_card/" & strFileName))

                conn.setDBCommit()

                bindGridBanner()
                txtots_detail.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub gridBanner_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gridBanner.RowDeleting

    End Sub

    Protected Sub cmdDelBanner_Click(sender As Object, e As EventArgs) Handles cmdDelBanner.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim lblPath As Label
        Dim chk As CheckBox


        i = gridBanner.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(gridBanner.Rows(s).FindControl("lblPK1"), Label)
                lblPath = CType(gridBanner.Rows(s).FindControl("lblBannerPath"), Label)
                chk = CType(gridBanner.Rows(s).FindControl("chk1"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM srp_banner WHERE banner_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If

                    File.Delete(Server.MapPath("../share/ots_card/" & lblPath.Text))

                End If
            Next s

            conn.setDBCommit()

            txtots_detail.Text = ""
            bindGridBanner()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridNews_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridNews.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButton1 As LinkButton = CType(e.Row.FindControl("LinkButton1"), LinkButton)
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK1"), Label)
            ' Response.Write("x1 ")
            LinkButton1.Attributes.Add("onclick", "window.open('srp_news_edit.aspx?mode=edit&id=" & lblPK.Text & "', '', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600');return false;")

        End If
    End Sub

    Protected Sub cmdDelBanner0_Click(sender As Object, e As EventArgs) Handles cmdDelNews.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim lblPath As Label
        Dim chk As CheckBox


        i = GridNews.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridNews.Rows(s).FindControl("lblPK1"), Label)

                chk = CType(GridNews.Rows(s).FindControl("chk1"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM srp_news WHERE new_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If

                    'Response.Write(sql)

                End If
            Next s

            conn.setDBCommit()

            bindGridNews()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdNewsRefresh_Click(sender As Object, e As EventArgs) Handles cmdNewsRefresh.Click
        bindGridNews()
    End Sub
End Class
