Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_ssip_master
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
            bindRoom()
            bindForm()
            bindGridBenefit()
            bindGridCategory()
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

    Sub bindGridBenefit()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_m_benefit "
            ds = conn.getDataSetForTransaction(sql, "t1")
            gridBenefit.DataSource = ds
            gridBenefit.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridCategory()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_m_category WHERE ISNULL(is_delete,0) = 0 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            gridCategory.DataSource = ds
            gridCategory.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindRoom()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_m_room WHERE ISNULL(is_delete,0) = 0"
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridRoom.DataSource = ds
            GridRoom.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_message WHERE ssip_message_id = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtobject.Content = ds.Tables("t1").Rows(0)("message_detail").ToString
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPicture.Text = "<img src='../share/ssip/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' />"
            End If

            sql = "SELECT * FROM ssip_message WHERE ssip_message_id = 2 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtinnovation.Content = ds.Tables("t1").Rows(0)("message_detail").ToString
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPicture1.Text = "<img src='../share/ssip/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' />"
            End If

            sql = "SELECT * FROM ssip_message WHERE ssip_message_id = 3 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtcommittee.Content = ds.Tables("t1").Rows(0)("message_detail").ToString
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPicture2.Text = "<img src='../share/ssip/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' />"
            End If

            sql = "SELECT * FROM ssip_message WHERE ssip_message_id = 4 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtchampion.Content = ds.Tables("t1").Rows(0)("message_detail").ToString
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPicture3.Text = "<img src='../share/ssip/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' />"
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAddRoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddRoom.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Try
            pk = getPK("room_id", "ssip_m_room", conn)
            sql = "INSERT INTO ssip_m_room (room_id , room_name , is_delete , create_by_name , create_date) VALUES("
            sql &= " '" & pk & "' , "
            sql &= " '" & addslashes(txtadd_room.Text) & "' , "
            sql &= " '" & 0 & "' , "
            sql &= " '" & Session("user_fullname").ToString & "' , "
            sql &= " GETDATE() "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            txtadd_room.Text = ""
            bindRoom()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdDelCat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelCat.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridRoom.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridRoom.Rows(s).FindControl("lblPK0"), Label)
                chk = CType(GridRoom.Rows(s).FindControl("chk0"), CheckBox)

                If chk.Checked = True Then
                    '  sql = "DELETE FROM ssip_m_room WHERE room_id = " & lbl.Text
                    sql = "UPDATE ssip_m_room SET is_delete = 1 WHERE room_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindRoom()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUpdateObject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateObject.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE ssip_message SET message_detail = '" & addslashes(txtobject.Content) & "' "
            sql &= " WHERE ssip_message_id = 1"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If
            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUpdateInno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateInno.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE ssip_message SET message_detail = '" & addslashes(txtinnovation.Content) & "' "
            sql &= " WHERE ssip_message_id = 2"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If
            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCommittee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCommittee.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            'Response.Write(txtcommittee.Text)
            sql = "UPDATE ssip_message SET message_detail = '" & addslashes(txtcommittee.Content) & "' "
            sql &= " WHERE ssip_message_id = 3"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If
            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdChampion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdChampion.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE ssip_message SET message_detail = '" & addslashes(txtchampion.Content) & "' "
            sql &= " WHERE ssip_message_id = 4"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If
            ' Response.Write(sql)
            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUpload_Click(sender As Object, e As System.EventArgs) Handles cmdUpload.Click
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


                sql = "UPDATE ssip_message SET picture_path = '" & strFileName & "' "
                sql &= " WHERE ssip_message_id = 1"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUpload0.PostedFile.SaveAs(Server.MapPath("../share/ssip/" & strFileName))

                conn.setDBCommit()

                bindForm()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub cmdDelPicture_Click(sender As Object, e As System.EventArgs) Handles cmdDelPicture.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE ssip_message SET picture_path = '' WHERE ssip_message_id = 1 "
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            ' bindMessage()

            lblPicture.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet

        Try
            If Not IsNothing(FileUpload1.PostedFile) Then
                Dim strFileName = FileUpload1.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filename As String()

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)


                sql = "UPDATE ssip_message SET picture_path = '" & strFileName & "' "
                sql &= " WHERE ssip_message_id = 2"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUpload1.PostedFile.SaveAs(Server.MapPath("../share/ssip/" & strFileName))

                conn.setDBCommit()

                bindForm()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE ssip_message SET picture_path = '' WHERE ssip_message_id = 2 "
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            ' bindMessage()

            lblPicture1.Text = ""
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
            If Not IsNothing(FileUpload2.PostedFile) Then
                Dim strFileName = FileUpload2.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filename As String()

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)


                sql = "UPDATE ssip_message SET picture_path = '" & strFileName & "' "
                sql &= " WHERE ssip_message_id = 3"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUpload2.PostedFile.SaveAs(Server.MapPath("../share/ssip/" & strFileName))

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
            sql = "UPDATE ssip_message SET picture_path = '' WHERE ssip_message_id = 3 "
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            ' bindMessage()

            lblPicture2.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Button5_Click(sender As Object, e As System.EventArgs) Handles Button5.Click
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet

        Try
            If Not IsNothing(FileUpload3.PostedFile) Then
                Dim strFileName = FileUpload3.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filename As String()

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)


                sql = "UPDATE ssip_message SET picture_path = '" & strFileName & "' "
                sql &= " WHERE ssip_message_id = 4"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUpload3.PostedFile.SaveAs(Server.MapPath("../share/ssip/" & strFileName))

                conn.setDBCommit()

                bindForm()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub Button6_Click(sender As Object, e As System.EventArgs) Handles Button6.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE ssip_message SET picture_path = '' WHERE ssip_message_id = 4 "
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            ' bindMessage()

            lblPicture3.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddRecog_Click(sender As Object, e As System.EventArgs) Handles cmdAddRecog.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            pk = getPK("master_benefit_id", "ssip_m_benefit", conn)
            sql = "INSERT INTO ssip_m_benefit (master_benefit_id , master_benefit_name  ) VALUES("
            sql &= "" & pk & " , "


            sql &= "'" & addslashes(txtadd_recog.Text) & "'  "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_recog.Text = ""
            bindGridBenefit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdDelRecog_Click(sender As Object, e As System.EventArgs) Handles cmdDelRecog.Click
        Dim chk As CheckBox
        Dim lbl As Label
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Try
            For i As Integer = 0 To gridBenefit.Rows.Count - 1
                chk = CType(gridBenefit.Rows(i).FindControl("chk"), CheckBox)
                lbl = CType(gridBenefit.Rows(i).FindControl("lblPK"), Label)

                If chk.Checked = True Then
                    sql = "DELETE FROM ssip_m_benefit WHERE master_benefit_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If

            Next i

            conn.setDBCommit()
          bindGridBenefit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSaveRecog_Click(sender As Object, e As System.EventArgs) Handles cmdSaveRecog.Click
        Dim txttopicname As TextBox

        Dim lbl As Label

        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String

        Try
            For i As Integer = 0 To gridBenefit.Rows.Count - 1

                txttopicname = CType(gridBenefit.Rows(i).FindControl("txttopicname"), TextBox)

                lbl = CType(gridBenefit.Rows(i).FindControl("lblPK"), Label)

                sql = "UPDATE ssip_m_benefit SET "
                sql &= " master_benefit_name = '" & addslashes(txttopicname.Text) & "' "
                sql &= " WHERE master_benefit_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            conn.setDBCommit()

            bindGridBenefit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

 
    Protected Sub cmdAddCategory_Click(sender As Object, e As System.EventArgs) Handles cmdAddCategory.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            pk = getPK("ssip_cat_id", "ssip_m_category", conn)
            sql = "INSERT INTO ssip_m_category (ssip_cat_id , cat_name , is_delete  ) VALUES("
            sql &= "" & pk & " , "
            sql &= "'" & addslashes(txtadd_category.Text) & "' , "
            sql &= " 0 "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_category.Text = ""
            bindGridCategory()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdSaveCategory_Click(sender As Object, e As System.EventArgs) Handles cmdSaveCategory.Click
        Dim txttopicname As TextBox

        Dim lbl As Label

        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String

        Try
            For i As Integer = 0 To gridCategory.Rows.Count - 1

                txttopicname = CType(gridCategory.Rows(i).FindControl("txttopicname"), TextBox)

                lbl = CType(gridCategory.Rows(i).FindControl("lblPK"), Label)

                sql = "UPDATE ssip_m_category SET "
                sql &= " cat_name = '" & addslashes(txttopicname.Text) & "' "
                sql &= " WHERE ssip_cat_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            conn.setDBCommit()

            bindGridCategory()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdDelCategory_Click(sender As Object, e As System.EventArgs) Handles cmdDelCategory.Click
        Dim chk As CheckBox
        Dim lbl As Label
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Try
            For i As Integer = 0 To gridCategory.Rows.Count - 1
                chk = CType(gridCategory.Rows(i).FindControl("chk"), CheckBox)
                lbl = CType(gridCategory.Rows(i).FindControl("lblPK"), Label)

                If chk.Checked = True Then
                    sql = "UPDATE ssip_m_category SET is_delete = 1 WHERE ssip_cat_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If

            Next i

            conn.setDBCommit()
            bindGridCategory()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
