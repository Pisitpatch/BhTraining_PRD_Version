Imports System.Data
Imports System.IO
Imports ShareFunction


Partial Class star_star_master
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected clip_name As String = ""
    Protected dsRecognition As New DataSet

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

        bindRecognitionCombo()

        If Page.IsPostBack Then
        Else ' First time load
            bindNote()
            bindAdmire()
            bindGridRecognition()
            bindGrandTopic()
            bindGrandTopicCombo()

            bindForm()
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


    Sub bindNote()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_m_note "
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridRoom.DataSource = ds
            GridRoom.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridRecognition()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_m_recognition "
            ds = conn.getDataSetForTransaction(sql, "t1")
            gridRecognition.DataSource = ds
            gridRecognition.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindAdmire()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_m_admire "
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridAdmire.DataSource = ds
            GridAdmire.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrandTopic()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_m_topic_main "
            ds = conn.getDataSetForTransaction(sql, "t1")
            gridGrandTopic.DataSource = ds
            gridGrandTopic.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrandTopicCombo()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_m_topic_main "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtadd_subgrandtopic.DataSource = ds
            txtadd_subgrandtopic.DataBind()

            txtadd_subgrandtopic.Items.Insert(0, New ListItem("-- กรุณาระบุ --", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindRecognitionCombo()

        Dim sql As String

        Try
            sql = "SELECT * FROM star_m_recognition  "
            dsRecognition = conn.getDataSetForTransaction(sql, "t1")
           
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            '  ds.Dispose()
        End Try
    End Sub

    Protected Sub GridRoom_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridRoom.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txtaward As DropDownList = CType(e.Row.FindControl("txtaward"), DropDownList)
            Dim txtrecog As DropDownList = CType(e.Row.FindControl("txtrecog"), DropDownList)
            Dim txtendrose As DropDownList = CType(e.Row.FindControl("txtendrose"), DropDownList)
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            '   Dim txt As DropDownList = CType(GridRoom.FindControl("txtendrose"), DropDownList)
            Dim sql As String
            Dim ds As New DataSet

            Try
                txtrecog.DataSource = dsRecognition
                txtrecog.DataBind()

                sql = "SELECT * FROM star_m_note WHERE note_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                txtendrose.SelectedValue = ds.Tables("t1").Rows(0)("endrose_id").ToString
                txtrecog.SelectedValue = ds.Tables("t1").Rows(0)("recognition_id").ToString
                txtaward.SelectedValue = ds.Tables("t1").Rows(0)("recognition_award").ToString
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridRoom_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridRoom.SelectedIndexChanged

    End Sub

    Protected Sub GridAdmire_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridAdmire.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(2).Text &= "<br/>Compassionate Caring"
            e.Row.Cells(3).Text &= "<br/>Adaptability"
            e.Row.Cells(4).Text &= "<br/>Safety"
            e.Row.Cells(5).Text &= "<br/>Teamwork"
        End If
    End Sub

    Protected Sub GridAdmire_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridAdmire.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txtclear As CheckBox = CType(e.Row.FindControl("txtclear"), CheckBox)
            Dim txtcare As CheckBox = CType(e.Row.FindControl("txtcare"), CheckBox)
            Dim txtsmart As CheckBox = CType(e.Row.FindControl("txtsmart"), CheckBox)
            Dim txtquality As CheckBox = CType(e.Row.FindControl("txtquality"), CheckBox)
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)

            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM star_m_admire WHERE admire_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t1")

                If ds.Tables("t1").Rows(0)("is_clear").ToString = "1" Then
                    txtclear.Checked = True
                Else
                    txtclear.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("is_care").ToString = "1" Then
                    txtcare.Checked = True
                Else
                    txtcare.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("is_smart").ToString = "1" Then
                    txtsmart.Checked = True
                Else
                    txtsmart.Checked = False
                End If

                If ds.Tables("t1").Rows(0)("is_quality").ToString = "1" Then
                    txtquality.Checked = True
                Else
                    txtquality.Checked = False
                End If
             
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridAdmire_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridAdmire.SelectedIndexChanged

    End Sub

    Protected Sub cmdSaveAdmire_Click(sender As Object, e As System.EventArgs) Handles cmdSaveAdmire.Click
        Dim chk_clear As CheckBox
        Dim chk_care As CheckBox
        Dim chk_smart As CheckBox
        Dim chk_quality As CheckBox
        Dim txttopicname As TextBox
        Dim lbl As Label

        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String

        Try
            For i As Integer = 0 To GridAdmire.Rows.Count - 1
                chk_clear = CType(GridAdmire.Rows(i).FindControl("txtclear"), CheckBox)
                chk_care = CType(GridAdmire.Rows(i).FindControl("txtcare"), CheckBox)
                chk_smart = CType(GridAdmire.Rows(i).FindControl("txtsmart"), CheckBox)
                chk_quality = CType(GridAdmire.Rows(i).FindControl("txtquality"), CheckBox)
                txttopicname = CType(GridAdmire.Rows(i).FindControl("txttopicname"), TextBox)
                lbl = CType(GridAdmire.Rows(i).FindControl("lblPK"), Label)

                sql = "UPDATE star_m_admire SET "
                If chk_clear.Checked = True Then
                    sql &= " is_clear = 1 "
                Else
                    sql &= " is_clear = 0 "
                End If

                If chk_care.Checked = True Then
                    sql &= " ,is_care = 1 "
                Else
                    sql &= " ,is_care = 0 "
                End If

                If chk_smart.Checked = True Then
                    sql &= " ,is_smart = 1 "
                Else
                    sql &= " ,is_smart = 0 "
                End If

                If chk_quality.Checked = True Then
                    sql &= " ,is_quality = 1 "
                Else
                    sql &= " ,is_quality = 0 "
                End If

                sql &= " , admire_topic = '" & addslashes(txttopicname.Text) & "' "

                sql &= " WHERE admire_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            conn.setDBCommit()

            bindAdmire()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
      
    End Sub

    Protected Sub cmdSaveNote_Click(sender As Object, e As System.EventArgs) Handles cmdSaveNote.Click
        Dim txtendrose As DropDownList
        Dim txtrecog As DropDownList
        Dim txtaward As DropDownList
        Dim txtname As TextBox

        Dim lbl As Label

        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String

        Try
            For i As Integer = 0 To GridRoom.Rows.Count - 1
                txtendrose = CType(GridRoom.Rows(i).FindControl("txtendrose"), DropDownList)
                txtrecog = CType(GridRoom.Rows(i).FindControl("txtrecog"), DropDownList)
                txtaward = CType(GridRoom.Rows(i).FindControl("txtaward"), DropDownList)
                txtname = CType(GridRoom.Rows(i).FindControl("txtnotename"), TextBox)

                lbl = CType(GridAdmire.Rows(i).FindControl("lblPK"), Label)

                sql = "UPDATE star_m_note SET "
                sql &= " endrose_id = '" & txtendrose.SelectedValue & "' "
                sql &= " , endrose_name = '" & txtendrose.SelectedItem.Text & "' "
                sql &= " , recognition_id = '" & txtrecog.SelectedValue & "' "
                sql &= " , recognition_name = '" & txtrecog.SelectedItem.Text & "' "
                sql &= " , recognition_award = '" & txtaward.SelectedValue & "' "
                sql &= " , note_th = '" & addslashes(txtname.Text) & "' "
                sql &= " , note_en = '" & addslashes(txtname.Text) & "' "

                sql &= " WHERE note_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            conn.setDBCommit()

            bindNote()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub gridGrandTopic_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridGrandTopic.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblSubTopic As Label = CType(e.Row.FindControl("lblSubTopic"), Label)

            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM star_m_topic_detail WHERE main_topic_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblSubTopic.Text &= " - " & ds.Tables("t1").Rows(i)("subtopic_name_th").ToString & "<br/>"
                Next i
            Catch ex As Exception

            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub gridGrandTopic_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gridGrandTopic.SelectedIndexChanged

    End Sub

    Protected Sub cmdAddGrandTopic_Click(sender As Object, e As System.EventArgs) Handles cmdAddGrandTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            pk = getPK("main_topic_id", "star_m_topic_main", conn)
            sql = "INSERT INTO star_m_topic_main (main_topic_id , main_topic_name_th , main_topic_name_en) VALUES("
            sql &= "" & pk & " , "
            sql &= "'" & addslashes(txtadd_grandtopic.Text) & "' , "
            sql &= "'" & addslashes(txtadd_grandtopic.Text) & "'  "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_grandtopic.Text = ""
            bindGrandTopic()
            bindGrandTopicCombo()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddSubTopic_Click(sender As Object, e As System.EventArgs) Handles cmdAddSubTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            pk = getPK("subtopic_id", "star_m_topic_detail", conn)
            sql = "INSERT INTO star_m_topic_detail (subtopic_id , main_topic_id , subtopic_name_th , subtopic_name_en) VALUES("
            sql &= "" & pk & " , "
            sql &= "'" & txtadd_subgrandtopic.SelectedValue & "' , "
            sql &= "'" & addslashes(txtadd_subtopic.Text) & "' , "
            sql &= "'" & addslashes(txtadd_subtopic.Text) & "'  "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_subtopic.Text = ""
            bindGrandTopic()
            ' bindGrandTopicCombo()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddNote_Click(sender As Object, e As System.EventArgs) Handles cmdAddNote.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            pk = getPK("note_id", "star_m_note", conn)
            sql = "INSERT INTO star_m_note (note_id , note_th , note_en ) VALUES("
            sql &= "" & pk & " , "

            sql &= "'" & addslashes(txtadd_note.Text) & "' , "
            sql &= "'" & addslashes(txtadd_note.Text) & "'  "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_note.Text = ""
            bindNote()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddAdmire_Click(sender As Object, e As System.EventArgs) Handles cmdAddAdmire.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try
            pk = getPK("admire_id", "star_m_admire", conn)
            sql = "INSERT INTO star_m_admire (admire_id , admire_topic  ) VALUES("
            sql &= "" & pk & " , "


            sql &= "'" & addslashes(txtadd_admire.Text) & "'  "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_admire.Text = ""
            bindAdmire()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdDeleteNote_Click(sender As Object, e As System.EventArgs) Handles cmdDeleteNote.Click
        Dim chk As CheckBox
        Dim lbl As Label
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Try
            For i As Integer = 0 To GridRoom.Rows.Count - 1
                chk = CType(GridRoom.Rows(i).FindControl("chk"), CheckBox)
                lbl = CType(GridRoom.Rows(i).FindControl("lblPK"), Label)

                If chk.Checked = True Then
                    sql = "DELETE FROM star_m_note WHERE note_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If
               
            Next i

            conn.setDBCommit()
            bindNote()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
       
    End Sub

    Protected Sub cmdDeleteAdmire_Click(sender As Object, e As System.EventArgs) Handles cmdDeleteAdmire.Click
        Dim chk As CheckBox
        Dim lbl As Label
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Try
            For i As Integer = 0 To GridAdmire.Rows.Count - 1
                chk = CType(GridAdmire.Rows(i).FindControl("chk"), CheckBox)
                lbl = CType(GridAdmire.Rows(i).FindControl("lblPK"), Label)

                If chk.Checked = True Then
                    sql = "DELETE FROM star_m_admire WHERE admire_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If

            Next i

            conn.setDBCommit()
            bindAdmire()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
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


                sql = "UPDATE star_message SET picture_path = '" & strFileName & "' "
                sql &= " WHERE star_message_id = 1"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUpload0.PostedFile.SaveAs(Server.MapPath("../share/star/" & strFileName))

                conn.setDBCommit()

                ' bindForm()
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
            sql = "UPDATE star_message SET picture_path = '' WHERE star_message_id = 1 "
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

    Protected Sub cmdCommittee_Click(sender As Object, e As System.EventArgs) Handles cmdCommittee.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE star_message SET message_detail = '" & addslashes(txtnews.Content) & "' "
            sql &= " WHERE star_message_id = 1"
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


    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_message WHERE star_message_id = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtnews.Content = ds.Tables("t1").Rows(0)("message_detail").ToString
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPicture.Text = "<img src='../share/star/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' />"
            End If

            clip_name = ds.Tables("t1").Rows(0)("clip_path").ToString

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAddClip_Click(sender As Object, e As System.EventArgs) Handles cmdAddClip.Click
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet

        Try
            If Not IsNothing(FileUploadClip.PostedFile) Then
                Dim strFileName = FileUploadClip.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filename As String()

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)


                sql = "UPDATE star_message SET clip_path = '" & strFileName & "' "
                sql &= " WHERE star_message_id = 1"
                ' Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUploadClip.PostedFile.SaveAs(Server.MapPath("flash/" & strFileName))

                conn.setDBCommit()

                bindForm()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub cmdDeleteClip_Click(sender As Object, e As System.EventArgs) Handles cmdDeleteClip.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE star_message SET clip_path = '' WHERE star_message_id = 1 "
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            ' bindMessage()

            ' lblPicture.Text = ""
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
            pk = getPK("recognition_id", "star_m_recognition", conn)
            sql = "INSERT INTO star_m_recognition (recognition_id , recognition_name , is_delete  ) VALUES("
            sql &= "" & pk & " , "


            sql &= "'" & addslashes(txtadd_recog.Text) & "'  ,"
            sql &= " 0 "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_recog.Text = ""
            bindGridRecognition()
            bindRecognitionCombo()
            bindNote()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdSaveRecog_Click(sender As Object, e As System.EventArgs) Handles cmdSaveRecog.Click
       
        Dim txttopicname As TextBox

        Dim lbl As Label

        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String

        Try
            For i As Integer = 0 To gridRecognition.Rows.Count - 1
             
                txttopicname = CType(gridRecognition.Rows(i).FindControl("txttopicname"), TextBox)

                lbl = CType(gridRecognition.Rows(i).FindControl("lblPK"), Label)

                sql = "UPDATE star_m_recognition SET "
                sql &= " recognition_name = '" & addslashes(txttopicname.Text) & "' "
                sql &= " WHERE recognition_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            conn.setDBCommit()

            bindGridRecognition()
            bindRecognitionCombo()
            bindNote()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdDelRecog_Click(sender As Object, e As System.EventArgs) Handles cmdDelRecog.Click
        Dim chk As CheckBox
        Dim lbl As Label
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String
        Try
            For i As Integer = 0 To gridRecognition.Rows.Count - 1
                chk = CType(gridRecognition.Rows(i).FindControl("chk"), CheckBox)
                lbl = CType(gridRecognition.Rows(i).FindControl("lblPK"), Label)

                If chk.Checked = True Then
                    sql = "DELETE FROM star_m_recognition WHERE recognition_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If

            Next i

            conn.setDBCommit()
            bindGridRecognition()
            bindRecognitionCombo()
            bindNote()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
