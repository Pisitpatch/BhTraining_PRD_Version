Imports System.Data
Imports ShareFunction

Namespace cfb
    Partial Class cfb_cfb_service_recovery
        Inherits System.Web.UI.Page
        Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Protected id As String = ""
        Protected typeid As String = ""
        Protected mode As String = ""
        Protected session_id As String = ""
        Protected viewtype As String = ""
        Protected status As String = ""
        Protected lang As String = "th"


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            id = Request.QueryString("id")
            typeid = Request.QueryString("typeid")
            mode = Request.QueryString("mode")

            If IsNothing(Session("session_myid")) Then
                Response.Redirect("../login.aspx")
                Response.End()
            End If

            session_id = Session("session_myid").ToString
            viewtype = Session("viewtype").ToString
            status = Request.QueryString("status")
            lang = Session("lang").ToString

            If conn.setConnectionForTransaction Then

            Else
                Response.Write("Connection Error")
            End If

            If Page.IsPostBack Then

            Else ' load first time

                bindLanguage(lang)

                If viewtype = "" Or status = "1" Or status = "" Or viewtype = "tqm" Then
                    cmdSave.Enabled = True
                Else
                    cmdSave.Enabled = False
                    cmdAdd.Enabled = False
                    txtadd_dept.Enabled = False
                End If

                If typeid = "1" Or typeid = "2" Then
                    ' service.Visible = False
                    ' complain.Visible = False
                ElseIf typeid = "3" Then
                    '  service.Visible = False
                ElseIf typeid = "4" Then

                End If

                service.Visible = False
                complain.Visible = False


                bindDept()
                If mode = "edit" Then
                    bindForm()
                    bindGridDept()
                End If
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

        Sub bindLanguage(ByVal lang As String)
            Dim sql As String
            Dim ds As New DataSet
            Try
                sql = "SELECT * FROM m_language WHERE module_code = 'CFB_SRV' AND object_id <> 'N/A'"
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    Try
                        CType(panelDetail.FindControl(ds.Tables(0).Rows(i)("object_id").ToString), Label).Text = ds.Tables(0).Rows(i)("name_" & lang).ToString
                    Catch ex As Exception
                        Throw New Exception("ERROR : " & ds.Tables(0).Rows(i)("object_id").ToString)
                    End Try

                Next i

                sql = "SELECT * FROM m_language WHERE module_code = 'CFB_SRV_R' ORDER BY object_id , lang_id"
                ds = conn.getDataSetForTransaction(sql, "tCombo")
                Dim oName As String = ""
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    oName = ds.Tables(0).Rows(i)("object_id").ToString
                    Try

                        CType(panelDetail.FindControl(ds.Tables(0).Rows(i)("object_id").ToString), RadioButtonList).Items(ds.Tables(0).Rows(i)("order_sort")).Text = ds.Tables(0).Rows(i)("name_" & lang).ToString
                    Catch ex As Exception
                        ' Throw New Exception("ERROR Radio : " & ds.Tables(0).Rows(i)("object_id").ToString)
                        Try
                            CType(panelDetail.FindControl(ds.Tables(0).Rows(i)("object_id").ToString), DropDownList).Items(ds.Tables(0).Rows(i)("order_sort")).Text = ds.Tables(0).Rows(i)("name_" & lang).ToString

                        Catch ex1 As Exception
                            Throw New Exception("ERROR Dropdown : " & ds.Tables(0).Rows(i)("object_id").ToString & " " & ex1.Message)
                        End Try

                    End Try

                Next i


            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindDept() ' Combo box
            Dim ds As New DataSet
            Dim sql As String

            Try
                'sql = "SELECT * FROM user_costcenter "
                sql = "SELECT * FROM user_dept WHERE dept_id NOT IN (SELECT dept_id FROM cbf_relate_dept WHERE comment_id = " & id & ")"
                sql &= " ORDER BY dept_name_en"

                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                txtadd_dept.DataSource = ds
                txtadd_dept.DataBind()

                txtadd_dept.Items.Insert(0, New ListItem("--Plese select--", ""))

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindGridDept() ' Grid Unit
            Dim ds As New DataSet
            Dim sql As String

            Try
                sql = "SELECT * FROM cbf_relate_dept "
                If mode = "add" Then
                    sql &= " WHERE session_id = '" & session_id & "'"
                ElseIf mode = "edit" Then
                    sql &= " WHERE comment_id = '" & id & "'"
                End If
                ds = conn.getDataSetForTransaction(sql, "t1")
                'Response.Write(sql)
                GridDept.DataSource = ds
                GridDept.DataBind()


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
                sql = "SELECT * FROM cfb_comment_list  "
                If mode = "add" Then
                    sql &= " WHERE session_id = '" & session_id & "'"
                ElseIf mode = "edit" Then
                    sql &= " WHERE comment_id = '" & id & "'"
                End If

                ds = conn.getDataSetForTransaction(sql, "t1")
                txtfeedback.Value = ds.Tables(0).Rows(0)("comment_detail").ToString
                txtsolution.Value = ds.Tables(0).Rows(0)("comment_solution").ToString

                If ds.Tables(0).Rows(0)("is_service1").ToString = "1" Then
                    txts1.Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_service1").ToString = "0" Then
                    txts2.Checked = True
                Else
                End If

                If ds.Tables(0).Rows(0)("is_service2").ToString = "1" Then
                    txts11.Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_service2").ToString = "0" Then
                    txts22.Checked = True
                Else

                End If

                If ds.Tables(0).Rows(0)("is_complain").ToString = "1" Then
                    txtcom1.Checked = True
                ElseIf ds.Tables(0).Rows(0)("is_complain").ToString = "0" Then
                    txtcom2.Checked = True
                Else

                End If

                txtcustomer.SelectedValue = ds.Tables(0).Rows(0)("customer_resp").ToString
                txtcus_detail.Value = ds.Tables(0).Rows(0)("customer_resp_remark").ToString

                If ds.Tables(0).Rows(0)("chk_tel").ToString = "1" Then
                    chk_tel.Checked = True
                Else
                    chk_tel.Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_email").ToString = "1" Then
                    chk_email.Checked = True
                Else
                    chk_email.Checked = False
                End If

                If ds.Tables(0).Rows(0)("chk_other").ToString = "1" Then
                    chk_othter.Checked = True
                Else
                    chk_othter.Checked = False
                End If

                txttel.Value = ds.Tables(0).Rows(0)("tel_remark").ToString
                txtemail.Value = ds.Tables(0).Rows(0)("email_remark").ToString
                txtother.Value = ds.Tables(0).Rows(0)("other_remark").ToString

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            Dim sql As String
            Dim errorMsg As String
            Try

                sql = "UPDATE cfb_comment_list SET comment_detail = '" & addslashes(txtfeedback.Value) & "' "
                sql &= " , comment_solution = '" & addslashes(txtsolution.Value) & "' "
                If txts1.Checked = True Then
                    sql &= " , is_service1 = 1 "
                Else
                    sql &= " , is_service1 = 0 "
                End If

                If txts11.Checked = True Then
                    sql &= " , is_service2 = 1 "
                Else
                    sql &= " , is_service2 = 0 "
                End If

                If txtcom1.Checked = True Then
                    sql &= " , is_complain = 1 "
                Else
                    sql &= " , is_complain = 0 "
                End If

                sql &= " , customer_resp = '" & txtcustomer.SelectedValue & "'"
                sql &= " , customer_resp_remark = '" & addslashes(txtcus_detail.Value) & "'"

                If chk_tel.Checked = True Then
                    sql &= " , chk_tel = 1 "
                Else
                    sql &= " , chk_tel = 0 "
                End If

                If chk_email.Checked = True Then
                    sql &= " , chk_email = 1 "
                Else
                    sql &= " , chk_email = 0 "
                End If

                If chk_othter.Checked = True Then
                    sql &= " , chk_other = 1 "
                Else
                    sql &= " , chk_other = 0 "
                End If

                sql &= " , tel_remark = '" & addslashes(txttel.Value) & "'"
                sql &= " , email_remark = '" & addslashes(txtemail.Value) & "'"
                sql &= " , other_remark = '" & addslashes(txtother.Value) & "'"
                sql &= " , lastupdate_by = '" & Session("user_fullname").ToString & "'"
                sql &= " , lastupdate_time =  GETDATE() "

                If mode = "add" Then
                    sql &= " WHERE session_id = '" & session_id & "'"
                ElseIf mode = "edit" Then
                    sql &= " WHERE comment_id = '" & id & "'"
                End If

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()

                Dim myScript As String = "var url = window.opener.location.href;window.opener.location.href = url; window.close();"
                ClientScript.RegisterStartupScript(Me.GetType, "refresh", myScript, True)
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            End Try

        End Sub

        Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
            Dim sql As String
            Dim pk As String
            Dim errorMsg As String
            Try
                pk = getPK("cfb_relate_dept_id", "cbf_relate_dept", conn)
                If mode = "add" Then
                    sql = "INSERT INTO cbf_relate_dept (cfb_relate_dept_id , comment_id ,  dept_id , dept_name  , session_id) VALUES("
                    sql &= "'" & pk & "',"
                    sql &= "'" & id & "',"
                    sql &= "'" & txtadd_dept.SelectedValue & "',"
                    sql &= "'" & txtadd_dept.SelectedItem.Text & "',"

                 

                    sql &= "'" & session_id & "' "
                    sql &= ")"
                Else
                    sql = "INSERT INTO cbf_relate_dept (cfb_relate_dept_id , comment_id ,  dept_id , dept_name , session_id) VALUES("
                    sql &= "'" & pk & "',"
                    sql &= "'" & id & "',"
                    sql &= "'" & txtadd_dept.SelectedValue & "',"
                    sql &= "'" & txtadd_dept.SelectedItem.Text & "',"

                  

                    sql &= "'" & session_id & "' "
                    sql &= ")"
                End If
                '   Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                bindGridDept()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridDept_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridDept.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM cbf_relate_dept WHERE cfb_relate_dept_id = " & GridDept.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Throw New Exception(result)
                End If

                conn.setDBCommit()
                bindGridDept()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridDept.SelectedIndexChanged

        End Sub

        Protected Sub txtadd_dept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtadd_dept.SelectedIndexChanged
            'bindEmployee()
        End Sub
    End Class
End Namespace
