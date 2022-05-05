Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction
Imports System.IO
Partial Class idp_idp_training_register
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected schedule_id As String = ""
    Protected id As String = ""
    Protected flag As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Request.QueryString("viewtype")
        Session("viewtype") = viewtype & ""
        schedule_id = Request.QueryString("schedule_id")
        flag = Request.QueryString("flag")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        If Page.IsPostBack = True Then
        Else ' load first time
            lblempcode.Text = Session("emp_code").ToString
            lblDept.Text = Session("dept_name").ToString
            lblDivision.Text = Session("job_title").ToString
            lblrequest_NO.Text = ""
            lblname.Text = Session("user_fullname").ToString
            lbljobtitle.Text = Session("user_position").ToString
            lblCostcenter.Text = Session("costcenter_id").ToString

            bindForm()
            bindRegistration()
            '  bindGrid()
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
            sql = "SELECT * FROM idp_training_schedule a INNER JOIN idp_external_req b ON a.idp_id = b.idp_id "
            sql &= " INNER JOIN idp_trans_list c ON a.idp_id = c.idp_id "
            sql &= " WHERE ISNULL(a.is_delete , 0) = 0 AND a.schedule_id = " & schedule_id
            ds = conn.getDataSetForTransaction(sql, "t1")
            lblrequest_NO.Text = ds.Tables("t1").Rows(0)("idp_no").ToString
            lblTitle.Text = ds.Tables("t1").Rows(0)("internal_title").ToString
            lblLocation.Text = ds.Tables("t1").Rows(0)("location").ToString
            lblDate.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("schedule_start_ts").ToString)
            lblTime.Text = ConvertTSTo(ds.Tables("t1").Rows(0)("schedule_start_ts").ToString, "hour").PadLeft(2, "0") & ":" & ConvertTSTo(ds.Tables("t1").Rows(0)("schedule_start_ts").ToString, "min").PadLeft(2, "0")
            lblTime.Text &= " - "
            lblTime.Text &= ConvertTSTo(ds.Tables("t1").Rows(0)("schedule_end_ts").ToString, "hour").PadLeft(2, "0") & ":" & ConvertTSTo(ds.Tables("t1").Rows(0)("schedule_end_ts").ToString, "min").PadLeft(2, "0")
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindRegistration()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_registered WHERE emp_code = " & Session("emp_code").ToString
            sql &= " AND schedule_id =  " & schedule_id
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count > 0 Then
                If ds.Tables("t1").Rows(0)("is_workhour").ToString = "1" Then
                    txtwork1.Checked = True
                    txtwork2.Checked = False
                Else
                    txtwork1.Checked = False
                    txtwork2.Checked = True
                End If

                txtremark.Text = ds.Tables("t1").Rows(0)("register_remark").ToString
                txtmobile.Text = ds.Tables("t1").Rows(0)("contact_tel").ToString
                txtemail.Text = ds.Tables("t1").Rows(0)("contact_email").ToString
                cmdRegister.Visible = False
                cmdCancel.Visible = True
            Else
                cmdRegister.Visible = True
                cmdCancel.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub
   
    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Response.Redirect("idp_training_calendar.aspx?flag=" & flag)
    End Sub

    Protected Sub cmdRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try
            pk = getPK("register_id", "idp_training_registered", conn)
            sql = "INSERT INTO idp_training_registered (register_id , schedule_id , idp_id , emp_code , emp_name , dept_id , dept_name , job_title , job_type , create_by , create_date , create_date_ts , register_remark , contact_tel , contact_email  , is_register , is_workhour ) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & schedule_id & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & lblempcode.Text & "' ,"
            sql &= " '" & addslashes(Session("user_fullname").ToString) & "' ,"
            sql &= " '" & Session("dept_id").ToString & "' ,"
            sql &= " '" & addslashes(Session("dept_name").ToString) & "' ,"
            sql &= " '" & addslashes(Session("job_title").ToString) & "' ,"
            sql &= " '" & addslashes(Session("user_position").ToString) & "' ,"
            sql &= " '" & addslashes(Session("user_fullname").ToString) & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' ,"
            sql &= " '" & addslashes(txtremark.Text) & "' ,"
            sql &= " '" & addslashes(txtmobile.Text) & "' ,"
            sql &= " '" & addslashes(txtemail.Text) & "' ,"
            sql &= " 0 , "
            If txtwork1.Checked = True Then
                sql &= " 1 "
            Else
                sql &= " 0 "
            End If

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            lblMessage.Text = "การลงทะเบียนเสร็จสมบรูณ์แล้ว / Registration is completed"
            cmdRegister.Visible = False
            cmdCancel.Visible = True
        Catch ex As Exception
            cmdRegister.Visible = True
            cmdCancel.Visible = False
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "DELETE FROM idp_training_registered WHERE emp_code = " & Session("emp_code").ToString
            sql &= " AND schedule_id =  " & schedule_id
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            lblMessage.Text = "ยกเลิกการลงทะเบียนเรียบร้อยแล้ว / Registraion is cancelled"
            cmdRegister.Visible = True
            cmdCancel.Visible = False
        Catch ex As Exception
            cmdRegister.Visible = False
            cmdCancel.Visible = True
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub
End Class
