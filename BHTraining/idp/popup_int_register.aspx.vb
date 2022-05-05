Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_popup_int_register
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""
    Protected sh_id As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        id = Request.QueryString("id")
        sh_id = Request.QueryString("sh_id")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        If Page.IsPostBack Then
        Else ' load first time
            bindRegister()
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

    Sub bindRegister()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_registered WHERE schedule_id = " & sh_id
            sql &= " order by create_date "
            ds = conn.getDataSetForTransaction(sql, "t1")

            GridRegister.DataSource = ds
            GridRegister.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try
            pk = getPK("register_id", "idp_training_registered", conn)
            sql = "INSERT INTO idp_training_registered (register_id , schedule_id , idp_id , emp_code , emp_name , dept_id , dept_name , job_title , job_type , create_by , create_date , create_date_ts , register_time) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & sh_id & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & txtempcode.Value & "' ,"
            sql &= " '" & txtname.Text & "' ,"
            sql &= " '" & 0 & "' ,"
            sql &= " '" & txtdept.Value & "' ,"
            sql &= " '" & txtjobtitle.Value & "' ,"
            sql &= " '" & txtjobtitle.Value & "' ,"
            sql &= " '" & Session("user_fullname").ToString & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' , "
            sql &= " 0 "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            bindRegister()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteFile.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridRegister.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridRegister.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridRegister.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM idp_training_registered WHERE register_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindRegister()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUploadCSV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUploadCSV.Click
        Dim strFileName As String = ""
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Try


            If Not IsNothing(FileUploadCSV.PostedFile) Then
                strFileName = FileUploadCSV.FileName

                If strFileName = "" Then
                    Return
                End If


                FileUploadCSV.PostedFile.SaveAs(Server.MapPath("../share/" & FileUploadCSV.FileName))


            End If


        Catch ex As Exception

            Response.Write(ex.Message)
            Return
        Finally

        End Try


        Dim strFilesName As String = strFileName
        Dim strPath As String = "../share/"


        Dim Sr As New StreamReader(Server.MapPath(strPath) & strFilesName)
        Dim sb As New System.Text.StringBuilder()
        Dim s As String

        Try
            While Not Sr.EndOfStream
                s = Sr.ReadLine()
                'cmd.CommandText = (("INSERT INTO MyTable Field1, Field2, Field3 VALUES(" + s.Split(","c)(1) & ", ") + s.Split(","c)(2) & ", ") + s.Split(","c)(3) & ")"
                ' lblDivisionSelect.Items.Add(New ListItem(s.Split(","c)(1), s.Split(","c)(0)))
                pk = getPK("register_id", "idp_training_registered", conn)
                sql = "INSERT INTO idp_training_registered (register_id , schedule_id , idp_id , emp_code , emp_name , dept_id , dept_name , job_title , job_type , create_by , create_date , create_date_ts , register_time ) VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & sh_id & "' ,"
                sql &= " '" & id & "' ,"
                sql &= " '" & s.Split(","c)(0) & "' ,"
                sql &= " '" & s.Split(","c)(1) & "' ,"
                sql &= " '" & 0 & "' ,"
                sql &= " '" & txtdept.Value & "' ,"
                sql &= " '" & txtjobtitle.Value & "' ,"
                sql &= " '" & txtjobtitle.Value & "' ,"
                sql &= " '" & Session("user_fullname").ToString & "' ,"
                sql &= " GETDATE() ,"
                sql &= " '" & Date.Now.Ticks & "' ,"
                sql &= " 0 "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
                'cmd.ExecuteNonQuery()
            End While

            conn.setDBCommit()
            bindRegister()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
     
    End Sub

    Protected Sub GridRegister_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridRegister.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblDate As Label = CType(e.Row.FindControl("lblDate"), Label)
            Dim lblDateTS As Label = CType(e.Row.FindControl("lblDateTS"), Label)

            Try
              

             

                If lblDateTS.Text <> "0" And lblDateTS.Text <> "" Then
                    'lblDate.Text = lblDateTS.Text
                    lblDate.Text = ConvertTSToDateString(lblDateTS.Text) & " " & ConvertTSTo(lblDateTS.Text, "hour") & ":" & ConvertTSTo(lblDateTS.Text, "min").PadLeft(2, "0")
                Else
                    lblDate.Text = "-"
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try




        End If
    End Sub

    Protected Sub GridRegister_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridRegister.SelectedIndexChanged

    End Sub
End Class
