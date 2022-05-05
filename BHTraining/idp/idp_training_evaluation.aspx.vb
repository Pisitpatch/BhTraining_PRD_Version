Imports ShareFunction
Imports System.Data

Partial Class idp_idp_training_evaluation
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected sh_id As String
    Protected id As String
    Dim irow As Integer = 0
    Dim cat_id As String = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       

        ' lang = Session("lang").ToString
        sh_id = Request.QueryString("sh_id")
        id = Request.QueryString("id")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Page.IsPostBack Then

        Else
            bindHeader()
            bindSpeaker()
            bindForm()
            bindGrid()
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

    Sub bindHeader()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_external_req a INNER JOIN idp_training_schedule b ON a.idp_id = b.idp_id "
            sql &= " WHERE b.schedule_id = " & sh_id
            ds = conn.getDataSetForTransaction(sql, "t1")
            lblCourseName.Text = ds.Tables("t1").Rows(0)("internal_title").ToString
            lblDate.Text = ConvertTSToDateString(ds.Tables("t1").Rows(0)("schedule_start_ts").ToString)
            lblLocation.text = ds.Tables("t1").Rows(0)("location").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindSpeaker()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_training_speaker a "
            sql &= " WHERE a.idp_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                lblSpeaker.Text &= ds.Tables("t1").Rows(0)("title").ToString & " " & ds.Tables("t1").Rows(0)("fname").ToString & " " & ds.Tables("t1").Rows(0)("lname").ToString & "<br/>"
            Next i
          
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_evaluation_head a  "
            sql &= " WHERE a.schedule_id = " & sh_id
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables("t1").Rows.Count > 0 Then
                txtdetail_course.Text = ds.Tables("t1").Rows(0)("suggest_speaker").ToString
                txtdetail_speaker.Text = ds.Tables("t1").Rows(0)("suggest_course").ToString
                If ds.Tables("t1").Rows(0)("method1").ToString = "1" Then
                    txtc1.Checked = True
                Else
                    txtc1.Checked = False
                End If
                If ds.Tables("t1").Rows(0)("method2").ToString = "1" Then
                    txtc2.Checked = True
                Else
                    txtc2.Checked = False
                End If
                If ds.Tables("t1").Rows(0)("method3").ToString = "1" Then
                    txtc3.Checked = True
                Else
                    txtc3.Checked = False
                End If
                If ds.Tables("t1").Rows(0)("method4").ToString = "1" Then
                    txtc4.Checked = True
                Else
                    txtc4.Checked = False
                End If
                If ds.Tables("t1").Rows(0)("method5").ToString = "1" Then
                    txtc5.Checked = True
                Else
                    txtc5.Checked = False
                End If
                If ds.Tables("t1").Rows(0)("method6").ToString = "1" Then
                    txtc6.Checked = True
                Else
                    txtc6.Checked = False
                End If
                txtkey1.Text = ds.Tables("t1").Rows(0)("key1").ToString
                txtkey2.Text = ds.Tables("t1").Rows(0)("key2").ToString
                txtkey3.Text = ds.Tables("t1").Rows(0)("key3").ToString

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , a.evaluation_form_id AS pk FROM idp_m_evaluation_topic a LEFT OUTER JOIN idp_evaluation_list b ON a.evaluation_form_id = b.evaluation_form_id "
            sql &= " AND b.emp_code = " & Session("emp_code").ToString
            sql &= " AND b.schedule_id = " & sh_id
            sql &= " INNER JOIN idp_m_evaluation_category c ON a.eval_category_id = c.eval_category_id "
            sql &= " WHERE 1 = 1 "
            sql &= " ORDER BY c.order_sort "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)

        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblHeaderHTML As Label = CType(e.Row.FindControl("lblHeaderHTML"), Label)
            Dim lblHeaderHTML0 As Label = CType(e.Row.FindControl("lblHeaderHTML0"), Label)
            Dim lblCategory As Label = CType(e.Row.FindControl("lblCategory"), Label)
            Dim lblCategory2 As Label = CType(e.Row.FindControl("lblCategory2"), Label)
            Dim RadioButtonList1f As RadioButtonList = CType(e.Row.FindControl("RadioButtonList1"), RadioButtonList)
            Dim sql As String
            Dim ds As New DataSet




            Try
                If irow = 0 Then ' first row
                    lblHeaderHTML0.Text = "<table width='100%' CellPadding=0><tr><td height='25' bgcolor='#eef1f3'><strong>" & lblCategory.Text & "/" & lblCategory2.Text & "<strong></td></tr></table>"
                    lblHeaderHTML.Text = "<table width='100%' CellPadding=0><tr><td height='25' >&nbsp;</td></tr></table>"

                Else ' next row
                    If cat_id = lblCategory.Text Then

                    Else
                        lblHeaderHTML0.Text = "<table width='100%' CellPadding=0><tr><td height='25' bgcolor='#eef1f3'><strong>" & lblCategory.Text & "/" & lblCategory2.Text & "<strong></td></tr></table>"
                        lblHeaderHTML.Text = "<table width='100%' CellPadding=0><tr><td height='25' >&nbsp;</td></tr></table>"
                    End If
                End If

                irow += 1
                cat_id = lblCategory.Text


                sql = "SELECT * FROM idp_evaluation_list WHERE evaluation_form_id = " & lblPK.Text
                sql &= " AND schedule_id = " & sh_id & " AND emp_code = " & Session("emp_code").ToString
                ds = conn.getDataSetForTransaction(sql, "t1")
                If ds.Tables("t1").Rows.Count > 0 Then
                    RadioButtonList1f.SelectedValue = ds.Tables("t1").Rows(0)("evaluation_score").ToString
                End If
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Sub onSave()
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lblPK As Label
        Dim r As RadioButtonList


        i = GridView1.Rows.Count
        Dim pk As String = ""
        Try
            sql = "DELETE FROM idp_evaluation_head WHERE emp_code = " & Session("emp_code").ToString
            sql &= " AND schedule_id = " & sh_id
            'Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            pk = getPK("[evaluate_head_id]", "idp_evaluation_head", conn)
            sql = "INSERT INTO idp_evaluation_head (evaluate_head_id , idp_id , schedule_id , emp_code , dept_id "
            sql &= " , method1 , method1_text , method2 , method2_text , method3 , method3_text , method4 , method4_text , method5, method5_text , method6 , method6_text"
            sql &= " , key1 , key2 , key3 , suggest_speaker , suggest_course "
            sql &= " , recommend1 , recommend1_text  , recommend2, recommend2_text , recommend3 , recommend3_text) VALUES( "

            sql &= "" & pk & " , "
            sql &= "" & id & " , "
            sql &= "" & sh_id & " , "
            sql &= "" & Session("emp_code").ToString & " , "
            sql &= "" & Session("dept_id").ToString & " , "
            If txtc1.Checked = True Then
                sql &= " 1 , '" & txtc1.Text & "' ,"
            Else
                sql &= " 0 , null , "
            End If

            If txtc2.Checked = True Then
                sql &= " 1 , '" & txtc2.Text & "' ,"
            Else
                sql &= " 0 , null , "
            End If

            If txtc3.Checked = True Then
                sql &= " 1 , '" & txtc3.Text & "' ,"
            Else
                sql &= " 0 , null , "
            End If

            If txtc4.Checked = True Then
                sql &= " 1 , '" & txtc4.Text & "' ,"
            Else
                sql &= " 0 , null , "
            End If

            If txtc5.Checked = True Then
                sql &= " 1 , '" & txtc5.Text & "' ,"
            Else
                sql &= " 0 , null , "
            End If

            If txtc6.Checked = True Then
                sql &= " 1 , '" & txtc6.Text & "' ,"
            Else
                sql &= " 0 , null , "
            End If

            sql &= " '" & addslashes(txtkey1.Text) & "' ,"
            sql &= " '" & addslashes(txtkey2.Text) & "' ,"
            sql &= " '" & addslashes(txtkey3.Text) & "' ,"
            sql &= " '" & addslashes(txtdetail_speaker.Text) & "' ,"
            sql &= " '" & addslashes(txtdetail_course.Text) & "' ,"
            If txtr1.Checked = True Then
                sql &= " 1 , '" & txtr1.Text & "' ,"
            Else
                sql &= " 0 , null , "
            End If

            If txtr2.Checked = True Then
                sql &= " 1 , '" & txtr2.Text & "' ,"
            Else
                sql &= " 0 , null , "
            End If
            If txtr3.Checked = True Then

                sql &= " 1 , '" & txtr3.Text & "' "
            Else
                sql &= " 0 , null  "
            End If

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If


            sql = "DELETE FROM idp_evaluation_list WHERE emp_code = " & Session("emp_code").ToString
            sql &= " AND schedule_id = " & sh_id
            'Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            For s As Integer = 0 To i - 1
                lblPK = CType(GridView1.Rows(s).FindControl("lblPK"), Label)
                r = CType(GridView1.Rows(s).FindControl("RadioButtonList1"), RadioButtonList)


                pk = getPK("evaluate_id", "idp_evaluation_list", conn)
                sql = "INSERT INTO idp_evaluation_list (evaluate_id , idp_id , schedule_id , emp_code , dept_id , evaluation_form_id , evaluation_score) VALUES("
                sql &= "" & pk & " , "
                sql &= "" & id & " , "
                sql &= "" & sh_id & " , "
                sql &= "" & Session("emp_code").ToString & " , "
                sql &= "" & Session("dept_id").ToString & " , "
                sql &= "" & lblPK.Text & " , "
                If r.SelectedValue = "" Then
                    sql &= " null  "
                Else
                    sql &= "" & r.SelectedValue & "  "
                End If

                sql &= ")"

                ' Response.Write(sql & "<Br/>")
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)

                End If



            Next s

            sql = "UPDATE idp_training_registered SET is_evaluate = 1 , evaluate_time = " & Date.Now.Ticks
            sql &= " WHERE schedule_id = " & sh_id & " AND emp_code = " & Session("emp_code").ToString
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If
            'Response.Write(sql & "<Br/>")
            conn.setDBCommit()
            bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write("row databound " & ex.Message)
            Return
        End Try

        Response.Redirect("idp_training_calendar.aspx?flag=evaluation")
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        onSave()
    End Sub

    Protected Sub cmdSave1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave1.Click
        onSave()
    End Sub
End Class
