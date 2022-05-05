Imports System.Data
Imports ShareFunction
Imports System.IO

Partial Class srp_srp_import
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
            bindDept()
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , CASE WHEN movememt_type_id = 1 THEN 'Register Card : ' + CAST(card_id AS varchar) WHEN movememt_type_id = 3 THEN 'แลกซื้อของ' ELSE '-' END AS move_name  "
            sql &= " FROM srp_point_movement a INNER JOIN user_profile b ON a.emp_code = b.emp_code "
            sql &= "  WHERE 1 = 1 "

            If txtfind_emp.Text <> "" Then
                sql &= " AND (b.user_fullname LIKE '%" & txtfind_emp.Text & "%'  OR b.user_fullname_local LIKE '%" & txtfind_emp.Text & "%' OR a.emp_code LIKE '%" & txtfind_emp.Text & "%') "
            End If
            If txtfind_mgr.Text <> "" Then
                sql &= " AND (r_award_by_emp_code LIKE '%" & txtfind_mgr.Text & "%' OR  r_award_by_emp_name LIKE '%" & txtfind_mgr.Text & "%') "
            End If
            If txtfind_card.Text <> "" Then
                sql &= " AND card_id =  " & txtfind_card.Text
            End If

            If txtdate1.Text <> "" And txtdate2.Text <> "" Then
                sql &= " AND movement_create_date_ts BETWEEN " & ConvertDateStringToTimeStamp(txtdate1.Text) & " AND " & ConvertDateStringToTimeStamp(txtdate2.Text)
            End If
            If txtdept.SelectedValue <> "" Then
                sql &= " AND b.dept_id = " & txtdept.SelectedValue
            End If

            sql &= " ORDER BY movement_create_date_ts DESC "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            gridview1.DataSource = ds
            gridview1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Public Shared Sub Export(ByVal fileName As String, ByVal gv As GridView)


        HttpContext.Current.Response.Clear()

        'Export will take two parameter first one the name of Excel File, and second one for gridview to be exported

        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))

        ' HttpContext.Current.Response.ContentType = "application/octet-stream"
        HttpContext.Current.Response.ContentType = " application/vnd.ms-excel"

        HttpContext.Current.Response.Charset = "TIS-620"
        ' HttpContext.Current.Response.Charset = "UTF-8"

        Using strWriter As New StringWriter()


            Using htmlWriter As New HtmlTextWriter(strWriter)


                ' Create a form to contain the grid

                Dim table As New Table()

                ' add the header row to the table

                If gv.HeaderRow IsNot Nothing Then


                    ExportControl(gv.HeaderRow)


                    table.Rows.Add(gv.HeaderRow)
                End If

                ' add each of the data rows to the table

                For Each row As GridViewRow In gv.Rows


                    ExportControl(row)


                    table.Rows.Add(row)
                Next

                ' add the footer row to the table

                If gv.FooterRow IsNot Nothing Then


                    ExportControl(gv.FooterRow)


                    table.Rows.Add(gv.FooterRow)
                End If

                ' render the table into the htmlwriter

                table.RenderControl(htmlWriter)

                ' render the htmlwriter into the response

                HttpContext.Current.Response.Write(strWriter.ToString())


                HttpContext.Current.Response.[End]()

            End Using
        End Using

    End Sub

    Private Shared Sub ExportControl(ByVal control As Control)


        For i As Integer = 0 To control.Controls.Count - 1


            Dim current As Control = control.Controls(i)

            If TypeOf current Is LinkButton Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, LinkButton).Text))

            ElseIf TypeOf current Is ImageButton Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, ImageButton).AlternateText))

            ElseIf TypeOf current Is HyperLink Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, HyperLink).Text))

            ElseIf TypeOf current Is DropDownList Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(TryCast(current, DropDownList).SelectedItem.Text))

            ElseIf TypeOf current Is CheckBox Then


                control.Controls.Remove(current)


                control.Controls.AddAt(i, New LiteralControl(If(TryCast(current, CheckBox).Checked, "True", "False")))
            End If

            'Like that you may convert any control to literals

            If current.HasControls() Then



                ExportControl(current)

            End If
        Next

    End Sub

    Sub csvUpload()
        Dim strFileName As String = ""
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String
        Dim emp_name As String = ""

        Try


            If Not IsNothing(myFile1.PostedFile) Then
                strFileName = myFile1.FileName

                If strFileName = "" Then
                    Return
                End If


                myFile1.PostedFile.SaveAs(Server.MapPath("../share/fte/" & myFile1.FileName))


            End If


        Catch ex As Exception

            Response.Write(ex.Message)
            Return
        Finally

        End Try


        Dim strFilesName As String = strFileName
        Dim strPath As String = "../share/fte/"


        Dim Sr As New StreamReader(Server.MapPath(strPath) & strFilesName, Encoding.Default, True)
        Dim sb As New System.Text.StringBuilder()
        Dim s As String
        Dim irow As Integer = 1
        Dim result As Int32 = 0
        Try

            While Not Sr.EndOfStream
                s = Sr.ReadLine()
                'cmd.CommandText = (("INSERT INTO MyTable Field1, Field2, Field3 VALUES(" + s.Split(","c)(1) & ", ") + s.Split(","c)(2) & ", ") + s.Split(","c)(3) & ")"
                ' lblDivisionSelect.Items.Add(New ListItem(s.Split(","c)(1), s.Split(","c)(0)))

                If irow > 1 And s.Split(","c)(1).ToString.Trim <> "" Then

                    If Int32.TryParse(s.Split(","c)(0), result) Then

                        pk = getPK("point_movement_id", "srp_point_movement", conn)
                        sql = "INSERT INTO srp_point_movement (point_movement_id , emp_code , emp_name , card_id , transaction_name , reward_type_id "
                        sql &= " , reward_type_name , movememt_type_id , movememt_type_name , point_trans , movement_remark , movement_create_by "
                        sql &= " , movement_create_date_ts , movement_create_date_raw "
                        ' sql &= ", r_award_by_emp_code , r_award_by_emp_name  , r_note_id , r_note_name , r_note_other , r_out_chk1 , r_out_chk2 , r_out_chk3 , r_out_chk4 , r_out_chk5 , r_out_chk6 "
                        'sql &= ", r_is_care , r_is_clear , r_is_smart1 , r_is_smart2 , r_date_ts , r_date_raw "
                        sql &= ") VALUES("
                        sql &= " '" & pk & "' , "
                        sql &= " '" & s.Split(","c)(0) & "' , "
                        sql &= " '" & addslashes(emp_name) & "' , "
                        sql &= " '" & 0 & "' , "
                        sql &= " 'Imported from Excel (CSV)' , "
                        sql &= " " & txtreward.SelectedValue & " , "
                        sql &= " '" & txtreward.SelectedItem.Text & "' , "
                        sql &= " 4 , "
                        sql &= " 'Adjust' , "

                        sql &= " " & s.Split(","c)(1) & " , "
                        sql &= " '" & addslashes(txtremark1.Text) & "' , "
                        sql &= " '" & Session("user_fullname").ToString & "' , "
                        sql &= " '" & Date.Now.Ticks & "' , "
                        sql &= " GETDATE()  "
                      
                        sql &= ")"

                        errorMsg = conn.executeSQLForTransaction(sql)
                        If errorMsg <> "" Then
                            Throw New Exception(errorMsg)
                        End If

                        'sql = "INSERT INTO srp_m_chain_command (emp_code,mgr_emp_code,mgr_name"
                        'sql &= ") VALUES("
                        'sql &= " '" & s.Split(","c)(0) & "' , "

                        'If Int32.TryParse(s.Split(","c)(1), result) Then
                        '    sql &= " '" & s.Split(","c)(1) & "' , "
                        'Else
                        '    sql &= " '" & s.Split(","c)(0) & "' , "
                        'End If

                        'sql &= " '" & addslashes(s.Split(","c)(1)) & "'  " ' emp_name
                        'sql &= ")"
                        ''  Response.Write(sql & "<br/>")
                        'errorMsg = conn.executeSQLForTransaction(sql)
                        'If errorMsg <> "" Then
                        '    Throw New Exception(errorMsg & sql)
                        'End If
                    End If

                End If

                'cmd.ExecuteNonQuery()

                irow += 1
            End While

            conn.setDBCommit()
            '  bindGridChainCommand()
            '  bindGrid()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub bindDept()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM user_dept ORDER BY dept_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtdept.DataSource = ds
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdUpload_Click(sender As Object, e As System.EventArgs) Handles cmdUpload.Click
        csvUpload()
        bindGrid()
    End Sub

    Protected Sub cmdAdjust_Click(sender As Object, e As System.EventArgs) Handles cmdAdjust.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim ds As New DataSet
        Try
            pk = getPK("point_movement_id", "srp_point_movement", conn)

            sql = "SELECT * FROM user_profile WHERE emp_code = " & txtemp_code.Text
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows.Count = 0 Then
                ds.Dispose()
                Exit Sub
            End If

            sql = "INSERT INTO srp_point_movement (point_movement_id , emp_code , emp_name , card_id , transaction_name , reward_type_id "
            sql &= " , reward_type_name , movememt_type_id , movememt_type_name , point_trans , movement_remark , movement_create_by "
            sql &= " , movement_create_date_ts , movement_create_date_raw "
            ' sql &= ", r_award_by_emp_code , r_award_by_emp_name  , r_note_id , r_note_name , r_note_other , r_out_chk1 , r_out_chk2 , r_out_chk3 , r_out_chk4 , r_out_chk5 , r_out_chk6 "
            'sql &= ", r_is_care , r_is_clear , r_is_smart1 , r_is_smart2 , r_date_ts , r_date_raw "
            sql &= ") VALUES("
            sql &= " '" & pk & "' , "
            sql &= " '" & addslashes(txtemp_code.Text) & "' , "
            sql &= " '" & addslashes(ds.Tables("t1").Rows(0)("user_fullname").ToString) & "' , "
            sql &= " '" & 0 & "' , "
            sql &= " 'Adjust Point' , "
            sql &= " 4 , "
            sql &= " 'Other' , "
            sql &= " 4 , "
            sql &= " 'Adjust' , "

            sql &= " " & txtpoint.Text & " , "
            sql &= " '" & addslashes(txtadjust_remark.Text) & "' , "
            sql &= " '" & Session("user_fullname").ToString & "' , "
            sql &= " '" & Date.Now.Ticks & "' , "
            sql &= " GETDATE()  "

            sql &= ")"
            ' Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtemp_code.Text = ""
            txtpoint.Text = 0
            txtadjust_remark.Text = ""
         
            '  Button1.Enabled = False
            bindGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As System.EventArgs) Handles cmdSearch.Click
        bindGrid()
    End Sub
    Protected Sub gridview1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gridview1.PageIndexChanging
        gridview1.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub
    Protected Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        Export("srp.xls", gridview1)
    End Sub
End Class
