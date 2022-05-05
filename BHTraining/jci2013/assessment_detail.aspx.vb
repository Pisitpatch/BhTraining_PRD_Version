Imports System.IO
Imports System.Data
Imports ShareFunction
Imports System.Net.Mail

Partial Class jci2013_assessment_detail
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""
    Protected form_id As String = ""
    Protected mode As String = ""
    Protected empcode As String = ""
    Protected ts As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")
        form_id = Request.QueryString("form_id")
        empcode = Request.QueryString("empcode")
        ts = Request.QueryString("ts")

        If IsPostBack Then

        Else ' First time load

            bindType()
            bindLocation()
            bindDepartment()

            If mode = "edit" Then
                '  updateMEList()
                bindForm()
                bindGridMEList()
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

            sql &= " FROM jci13_assessment_list a "
            sql &= " WHERE assessment_id =  " & id

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            txtcode.Text = ds.Tables("t1").Rows(0)("emp_code").ToString
            txtleader.Text = ds.Tables("t1").Rows(0)("emp_name").ToString
            txtdate.Text = ds.Tables("t1").Rows(0)("assessment_date_str").ToString
            txttime.Text = ds.Tables("t1").Rows(0)("assessment_time_str").ToString

            txtdept.SelectedValue = ds.Tables("t1").Rows(0)("dept_id").ToString
            txttype.SelectedValue = ds.Tables("t1").Rows(0)("type_id").ToString
            txtlocation.SelectedValue = ds.Tables("t1").Rows(0)("location_id").ToString

            txtcustom_dept.Text = ds.Tables("t1").Rows(0)("dept_name").ToString
            txtcustom_location.Text = ds.Tables("t1").Rows(0)("location_name").ToString
            txtcustom_type.Text = ds.Tables("t1").Rows(0)("type_name").ToString
            txtimpression.Text = ds.Tables("t1").Rows(0)("impression").ToString
            txtcustom_building.Text = ds.Tables("t1").Rows(0)("building_name").ToString
            txtmember.Text = ds.Tables("t1").Rows(0)("member").ToString
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub


    Sub updateMEList()
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim ds As New DataSet

        Try

            sql = "SELECT * FROM jci13_std_select WHERE form_id = " & form_id
            sql &= " AND me_id NOT IN (select me_id from jci13_assessment_me_list where assessment_id = " & id & ")"
            ds = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1

                pk = getPK("assessment_me_id", "jci13_assessment_me_list", conn)
                sql = "INSERT INTO jci13_assessment_me_list (assessment_me_id , assessment_id , me_id )"
                sql &= " VALUES( "
                sql &= " '" & pk & "' , "
                sql &= " '" & id & "' , "
                sql &= " '" & ds.Tables("t1").Rows(i)("me_id").ToString & "'  "
            
                sql &= " )"
                ' Response.Write(sql & "<br/>")
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

          

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
            Return
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridMEList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_assessment_me_list a INNER JOIN jci13_std_select b ON a.me_id = b.me_id "
            sql &= " WHERE b.form_id =  " & form_id
            'sql &= " AND a.assessment_id = " & id
            sql &= " AND a.assessment_id IN (SELECT assessment_id FROM jci13_assessment_list WHERE assessment_id = " & id & " AND emp_code = " & empcode & "  )"
            sql &= " ORDER BY order_sort , chapter"
            ' Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            GridView1.DataSource = ds
            GridView1.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindType()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_m_type a "

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            txttype.DataSource = ds
            txttype.DataBind()

            txttype.Items.Insert(0, New ListItem("--Please Select--", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindLocation()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_m_location a "

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtlocation.DataSource = ds
            txtlocation.DataBind()

            txtlocation.Items.Insert(0, New ListItem("--Please Select--", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDepartment()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM user_dept a "

            'Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtdept.DataSource = ds
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("--Please Select--", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Response.Redirect("assessment_list.aspx?form_id=" & form_id & "&menu=3")
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim ds As New DataSet
        Try
            If mode = "add" Then
                pk = getPK("assessment_id", "jci13_assessment_list", conn)
                sql = "INSERT INTO jci13_assessment_list (assessment_id , form_id , emp_code , emp_name , assessment_date_str , assessment_time_str , dept_id , dept_name , location_id , location_name , type_id , type_name , building_name , impression , member , create_date_raw , create_date_ts  "
                sql &= " )VALUES("
                sql &= " '" & pk & "' ,"
                sql &= " '" & form_id & "' ,"
                sql &= " '" & txtcode.Text & "' ,"
                sql &= " '" & txtleader.Text & "' ,"
                sql &= " '" & txtdate.Text & "' ,"
                sql &= " '" & txttime.Text & "' ,"
                sql &= " '" & txtdept.SelectedValue & "' ,"
                sql &= " '" & txtcustom_dept.Text & "' ,"
                sql &= " '" & txtlocation.SelectedValue & "' ,"
                sql &= " '" & txtcustom_location.Text & "' ,"
                sql &= " '" & txttype.SelectedValue & "' ,"
                sql &= " '" & txtcustom_type.Text & "' ,"
                sql &= " '" & txtcustom_building.Text & "' ,"
                sql &= " '" & txtimpression.Text & "' ,"
                sql &= " '" & txtmember.Text & "' ,"
                sql &= " GETDATE() , " ' create_date_raw
                sql &= " '" & Date.Now.Ticks & "'  " ' create_date_ts
                sql &= " )"

                id = pk
            Else
                sql = "UPDATE jci13_assessment_list SET "
                sql &= " emp_name = '" & txtleader.Text & "' "
                sql &= " , emp_code = '" & txtcode.Text & "' "
                sql &= " , assessment_date_str = '" & txtdate.Text & "' "
                sql &= " , assessment_time_str = '" & txttime.Text & "' "

                sql &= " , dept_id = '" & txtdept.SelectedValue & "' "
                sql &= " , dept_name = '" & txtcustom_dept.Text & "' "
                sql &= " , location_id = '" & txtlocation.SelectedValue & "' "
                sql &= " , location_name = '" & txtcustom_location.Text & "' "
                sql &= " , type_id = '" & txttype.SelectedValue & "' "
                sql &= " , type_name = '" & txtcustom_type.Text & "' "

                sql &= " , building_name = '" & txtcustom_building.Text & "' "
                sql &= " , impression = '" & addslashes(txtimpression.Text) & "' "
                sql &= " , member = '" & addslashes(txtmember.Text) & "' "
                sql &= " WHERE assessment_id = " & id
            End If

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            If mode = "edit" Then

                Dim txtcomment As TextBox
                Dim r As RadioButtonList
                Dim lblPK As Label

                For i As Integer = 0 To GridView1.Rows.Count - 1

                    txtcomment = CType(GridView1.Rows(i).FindControl("txtcomment"), TextBox)
                    lblPK = CType(GridView1.Rows(i).FindControl("lblPK"), Label)
                    r = CType(GridView1.Rows(i).FindControl("RadioButtonList1"), RadioButtonList)

                    sql = "UPDATE jci13_assessment_me_list SET me_comment_detail = '" & txtcomment.Text & "' "
                    sql &= " , me_score_level = '" & r.SelectedValue & "' "
                    sql &= " WHERE assessment_me_id = " & lblPK.Text
                    ' Response.Write(sql)
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg & sql)
                    End If
                Next i

                Dim num As Integer = 0
                Dim sum_score As Integer = 0

                sql = "SELECT * FROM jci13_assessment_me_list WHERE assessment_id = " & id
                sql &= " AND (me_score_level is null or me_score_level >=0 )"
                ds = conn.getDataSetForTransaction(sql, "t1")
                num = ds.Tables(0).Rows.Count
                ' context.Response.Write("num = " & num)
                For i As Integer = 0 To num - 1
                    If (ds.Tables(0).Rows(i)("me_score_level").ToString <> "") Then
                        sum_score += CInt(ds.Tables(0).Rows(i)("me_score_level").ToString)
                    End If
                Next i

                Dim percent As Double = 0
                Dim rank As String = ""
                percent = Math.Round((sum_score / (num * 10)) * 100, 1)
                '   context.Response.Write("score = " & sum_score )

                If (percent > 97.5) Then
                    rank = "FE"
                ElseIf (percent > 95) And (percent <= 97.5) Then
                    rank = "EE"
                ElseIf percent = 95 Then
                    rank = "ME"
                ElseIf (percent >= 90) And (percent < 95) Then
                    rank = "IN"
                ElseIf percent < 90 Then
                    rank = "UN"
                End If

                sql = "UPDATE jci13_assessment_list SET score = '" & percent & "' "
                sql &= " , rank = '" & rank & "' "
                sql &= "WHERE assessment_id = " & id

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
            End If


            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        End Try


        ' Response.Redirect("assessment_list.aspx?form_id=" & form_id & "&menu=3")
        Response.Redirect("assessment_detail.aspx?mode=edit&id=" & id & "&form_id=" & form_id & "&menu=3")
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        bindGridMEList()
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim r As RadioButtonList
            Dim lblScore As Label
            Dim lblPK As Label
            Dim lblPicture As Label
            Dim lblIpadPK As Label
            Dim sql As String
            Dim ds As New DataSet

            lblPK = CType(e.Row.FindControl("lblPK"), Label)
            lblIpadPK = CType(e.Row.FindControl("lblIpadPK"), Label)
            lblPicture = CType(e.Row.FindControl("lblPicture"), Label)
            r = CType(e.Row.FindControl("RadioButtonList1"), RadioButtonList)
            lblScore = CType(e.Row.FindControl("lblScore"), Label)

            If lblScore.Text <> "" Then
                r.SelectedValue = lblScore.Text
            End If

            lblPicture.Text = ""

            Try
                sql = "SELECT * FROM jci13_assessment_picture_list a INNER JOIN jci13_assessment_me_list b ON a.ipad_assessment_me_id = b.ipad_assessment_me_id AND a.ipad_timestamp =  " & ts
                sql &= " INNER JOIN jci13_assessment_list c ON b.assessment_id = c.assessment_id AND ISNULL(c.is_assessment_delete,0) = 0  AND c.emp_code = " & empcode
                sql &= " WHERE b.assessment_me_id = " & lblPK.Text
                sql &= " AND c.assessment_id = " & id
                If lblIpadPK.Text <> "" Then
                    sql &= " AND a.ipad_assessment_me_id = " & lblIpadPK.Text
                Else
                    sql &= " AND  1 > 2 "
                End If

                'Response.Write(sql)
                ds = conn.getDataSetForTransaction(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblPicture.Text &= "<a class='fancybox' data-fancybox-group='gallery' href='../share/jci/pdf/" & ds.Tables("t1").Rows(i)("picture_name").ToString & "'>" & ds.Tables("t1").Rows(i)("picture_name").ToString & "</a><br/>"
                Next i

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub txtdept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtdept.SelectedIndexChanged
        txtcustom_dept.Text = txtdept.SelectedItem.Text
    End Sub

    Protected Sub txttype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txttype.SelectedIndexChanged
        txtcustom_type.Text = txttype.SelectedItem.Text
    End Sub

    Protected Sub txtlocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtlocation.SelectedIndexChanged
        txtcustom_location.Text = txtlocation.SelectedItem.Text
    End Sub

    Protected Sub cmdSendMail_Click(sender As Object, e As EventArgs) Handles cmdSendMail.Click

        If txtto.Value = "" Then
            lblError.Text = "กรุณาระบุอีเมล์"
            Return
        Else
            lblError.Text = ""
        End If

        Dim msgbody As String = ""
        Dim email_list() As String
        Dim cc_list() As String
        Dim bcc_list() As String

        email_list = txtidselect.Value.Split(",") ' create emailTo array
        cc_list = txtidCCselect.Value.Split(",") ' create emailCC array
        bcc_list = txtidBCCSelect.Value.Split(",") ' create emailCC array

        Dim subject As String = "Internal Quality Assessment Report"
        msgbody &= "<a href='http://bhtraining/jci2013/result_template.aspx?id=" & id & "'>" & "Internal Quality Assessment Report" & "</a>"

        sendMailWithCC1(email_list, cc_list, bcc_list, subject, msgbody, "", "ir")
        updateSendMail()
        Response.Redirect("assessment_detail.aspx?mode=edit&id=" & id & "&form_id=" & form_id & "&empcode=" & empcode & "&ts=" & ts & "&menu=3")
    End Sub

    Sub sendMailWithCC1(ByVal toEmail() As String, ByVal ccEmail() As String, ByVal bccEmail() As String, ByVal subject As String, ByVal message As String, Optional ByVal from As String = "", Optional ByVal mailType As String = "ir", Optional ByVal username As String = "", Optional ByVal password As String = "")
        Dim oMsg As New MailMessage()
        Dim smtp As New SmtpClient("mail.bumrungrad.com")

        Try
            oMsg.From = New MailAddress("somsong@bumrungrad.com")
            oMsg.Headers.Add("Disposition-Notification-To", "<somsong@bumrungrad.com>")
            'ConfigurationManager.AppSettings("database").ToString()


            For i As Integer = 0 To UBound(toEmail)
                If toEmail(i) <> "" And toEmail(i).Length > 5 Then
                    oMsg.To.Add(New MailAddress(toEmail(i).ToLower))
                End If
            Next

            For i As Integer = 0 To UBound(ccEmail)
                If ccEmail(i) Is Nothing Or ccEmail(i) = "" Then
                Else
                    If ccEmail(i) <> "" And ccEmail(i).Length > 5 Then
                        oMsg.CC.Add(New MailAddress(ccEmail(i).ToLower))
                    End If
                End If

            Next

            Try
                For i As Integer = 0 To UBound(bccEmail)
                    If bccEmail(i) Is Nothing Or bccEmail(i) = "" Then
                    Else
                        If bccEmail(i) <> "" And bccEmail(i).Length > 5 Then
                            oMsg.Bcc.Add(New MailAddress(bccEmail(i).ToLower))
                        End If
                    End If

                Next
            Catch ex As Exception

            End Try



            oMsg.Subject = subject
            oMsg.IsBodyHtml = True
            oMsg.Body = message

            If chk_priority.Checked Then
                oMsg.Priority = MailPriority.High
            End If
            'Dim smtp As New SmtpClient("mail.powerpointproduct.com")
            ' SMTP Authenticate
            'smtp.Credentials = New System.Net.NetworkCredential("info@powerpointproduct.com", "natee")
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.Send(oMsg)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            oMsg = Nothing
            smtp = Nothing
        End Try


    End Sub

    Sub updateSendMail()
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE jci13_assessment_list SET email_date_raw = GETDATE() "
            sql &= " , email_date_ts = " & Date.Now.Ticks
            sql &= " WHERE assessment_id = " & id

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
