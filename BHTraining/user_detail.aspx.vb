Imports System.Data.SqlClient
Imports System.Data

Partial Class user_detail
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Private id As String = "0"
    Private mode As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("mobule_list")) Then
            Response.Redirect("login.aspx")
        End If

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")

        If Not Page.IsPostBack Then

            bindDept()

            If mode = "edit" Then
                bindForm()
                bindModuleAvailable()
                bindModuleGrant()
                bindRoleAvailable()
                bindRoleGrant()

                bindCostCenter()
                bindCostcenterGrant()

                bindCostCenterIDP()
                bindCostcenterGrantIDP()

                bindCostCenterSSIP()
                bindCostcenterGrantSSIP()
                ' bindCostCenter()
                ' bindDepartment()
            End If

        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()

        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        Finally
            conn = Nothing
        End Try
    End Sub

    Sub bindDept() ' Combo box
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM user_dept"
            'sql &= " ORDER BY dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            txtdept.DataSource = ds
            txtdept.DataBind()

            txtdept.Items.Insert(0, New ListItem("--Please select--", ""))

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
  
    Sub bindForm()
        Dim sql As String
        '  Dim errorMsg As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM user_profile WHERE emp_code = " & id
            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception("Bindform :: " & conn.errMessage)
            End If

            txtdept.SelectedValue = ds.Tables(0).Rows(0)("dept_id").ToString
            txtusername.Text = ds.Tables(0).Rows(0)("user_login").ToString
            txtpassword.Text = ds.Tables(0).Rows(0)("user_password").ToString
            txtnameen.Text = ds.Tables(0).Rows(0)("user_fullname").ToString
            txtnameth.Text = ds.Tables(0).Rows(0)("user_fullname_local").ToString
            txtid.Text = ds.Tables(0).Rows(0)("emp_code").ToString

            txtjobtitle.Text = ds.Tables(0).Rows(0)("job_title").ToString
            txtjobtype.Text = ds.Tables(0).Rows(0)("job_type").ToString
            txtemptype.Text = ds.Tables(0).Rows(0)("emp_type").ToString
            txthiredate.Text = ds.Tables(0).Rows(0)("hire_date").ToString

            txtsex.Text = ds.Tables(0).Rows(0)("sex").ToString
            txtdob.Text = ds.Tables(0).Rows(0)("dob").ToString
            txtage.Text = ds.Tables(0).Rows(0)("age").ToString
            txthn.Text = ds.Tables(0).Rows(0)("hn").ToString

            txtcostcenter.Text = ds.Tables(0).Rows(0)("costcenter_id").ToString
            txtdeptname.Text = ds.Tables(0).Rows(0)("dept_name").ToString

            If ds.Tables(0).Rows(0)("is_active").ToString = "1" Then
                RadioButtonList1.Items(0).Selected = True
                RadioButtonList1.Items(1).Selected = False
            Else
                RadioButtonList1.Items(0).Selected = False
                RadioButtonList1.Items(1).Selected = True
            End If

            txtemail.Text = ds.Tables(0).Rows(0)("custom_user_email").ToString
            txtsms.Text = ds.Tables(0).Rows(0)("custom_mobile").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        ds.Clear()
        ds = Nothing
    End Sub

    Sub bindModuleAvailable()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT * FROM m_module WHERE module_id NOT IN  ("
            sql &= "SELECT module_id FROM user_module WHERE emp_code = " & txtid.Text
            sql &= ")"
            reader = conn.getDataReader(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            txtmodule1.DataSource = reader
            txtmodule1.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Sub bindModuleGrant()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT * FROM user_module a INNER JOIN m_module b ON a.module_id = b.module_id  WHERE a.emp_code =  " & txtid.Text
            reader = conn.getDataReader(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            txtmodule2.DataSource = reader
            txtmodule2.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Sub bindRoleAvailable()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT * FROM m_role WHERE role_id NOT IN  ("
            sql &= "SELECT role_id FROM user_role WHERE emp_code = " & txtid.Text
            sql &= ")"
            reader = conn.getDataReader(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            txtrole1.DataSource = reader
            txtrole1.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Sub bindRoleGrant()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT * FROM user_role a INNER JOIN m_role b ON a.role_id = b.role_id  WHERE a.emp_code =  " & txtid.Text

            reader = conn.getDataReader(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            txtrole2.DataSource = reader
            txtrole2.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Sub bindDepartment()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT * FROM user_dept ORDER BY dept_name_th"
            reader = conn.getDataReader(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            ' txtdept.DataSource = reader
            ' txtdept.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally

        End Try
    End Sub

    Sub bindCostCenter()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT dept_name_en + '(' + CAST(dept_id AS varchar) + ')'  AS costcenter_name , costcenter_id FROM user_dept "

            'sql = "SELECT name_en + '(' + CAST(costcenter_id AS varchar) + ')'  AS costcenter_name , costcenter_id FROM user_costcenter "
            sql &= " WHERE dept_id NOT IN (SELECT costcenter_id FROM user_access_costcenter WHERE emp_code = " & txtid.Text & ")"
            sql &= " ORDER BY dept_name_en"
            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txtcc.DataSource = reader
            txtcc.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindCostcenterGrant()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT c.dept_name_en + '(' + CAST(a.costcenter_id AS varchar) + ')'  AS costcenter_name , a.costcenter_id  FROM user_access_costcenter a INNER JOIN user_profile b ON a.emp_code = b.emp_code  "
            ' sql &= " INNER JOIN user_costcenter c ON a.costcenter_id = c.costcenter_id "
            sql &= " INNER JOIN user_dept c ON a.costcenter_id = c.costcenter_id "
            sql &= " WHERE a.emp_code =  " & txtid.Text
            reader = conn.getDataReader(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            txtcc2.DataSource = reader
            txtcc2.DataBind()
            ' Response.Write(sql)

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ' Try
            reader.Close()
            ' Catch ex As Exception

            ' End Try

        End Try
    End Sub

    Sub bindCostCenterIDP()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT dept_name_en + '(' + CAST(costcenter_id AS varchar) + ')'  AS costcenter_name , costcenter_id FROM user_dept "
            sql &= " WHERE dept_id NOT IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & txtid.Text & ")"
            sql &= " ORDER BY costcenter_name"
            ds = conn.getDataSet(sql, "t1")
            ' Response.Write(sql)
            txtidp_cc1.DataSource = ds
            txtidp_cc1.DataBind()


        Catch ex As Exception
            Response.Write("bindCostCenterIDP" & ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCostcenterGrantIDP()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT c.dept_name_en + '(' + CAST(a.costcenter_id AS varchar) + ')'  AS costcenter_name , a.costcenter_id  FROM user_access_costcenter_idp a INNER JOIN user_profile b ON a.emp_code = b.emp_code "
            sql &= " INNER JOIN user_dept c ON a.costcenter_id = c.costcenter_id "
            sql &= " WHERE a.emp_code =  " & txtid.Text
            reader = conn.getDataReader(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            txtidp_cc2.DataSource = reader
            txtidp_cc2.DataBind()
            ' Response.Write(sql)

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            Try
                reader.Close()
            Catch ex As Exception

            End Try

        End Try
    End Sub

    Sub bindCostCenterSSIP()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT dept_name_en + '(' + CAST(costcenter_id AS varchar) + ')'  AS costcenter_name , costcenter_id FROM user_dept "
            sql &= " WHERE dept_id NOT IN (SELECT costcenter_id FROM user_access_costcenter_ssip WHERE emp_code = " & txtid.Text & ")"
            sql &= " ORDER BY costcenter_name"
            reader = conn.getDataReader(sql, "t1")
            'Response.Write(sql)
            txtssip_cc1.DataSource = reader
            txtssip_cc1.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindCostcenterGrantSSIP()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT c.dept_name_en + '(' + CAST(a.costcenter_id AS varchar) + ')'  AS costcenter_name , a.costcenter_id  FROM user_access_costcenter_ssip a INNER JOIN user_profile b ON a.emp_code = b.emp_code "
            sql &= " INNER JOIN user_dept c ON a.costcenter_id = c.costcenter_id "
            sql &= " WHERE a.emp_code =  " & txtid.Text
            reader = conn.getDataReader(sql, "t0")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If
            txtssip_cc2.DataSource = reader
            txtssip_cc2.DataBind()
            ' Response.Write(sql)

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            Try
                reader.Close()
            Catch ex As Exception

            End Try

        End Try
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sql As String
        Dim errorMsg As String
        Try
            If mode = "edit" Then
                sql = "UPDATE user_profile SET custom_user_email = '" & txtemail.Text & "' "
                sql &= " , custom_mobile = '" & txtsms.Text & "'"
                sql &= " , dept_id = " & txtdept.SelectedValue
                sql &= " , costcenter_id = " & txtdept.SelectedValue
                sql &= " , dept_name = '" & txtdept.SelectedItem.Text & "' "
                sql &= " , is_active = " & RadioButtonList1.SelectedValue
                sql &= " WHERE emp_code = " & id
                errorMsg = conn.executeSQL(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Response.Redirect("user_management.aspx")
    End Sub

    Protected Sub cmdAddModule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddModule.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtmodule1.SelectedItem) Then
                Return
            End If

            sql = "INSERT INTO user_module (module_id , emp_code ) VALUES("
            sql &= "" & txtmodule1.SelectedValue & ","
            sql &= "" & txtid.Text & ""
            sql &= ")"

            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            bindModuleAvailable()
            bindModuleGrant()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdRemoveModule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveModule.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtmodule2.SelectedItem) Then
                Return
            End If

            sql = "DELETE FROM user_module WHERE module_id = " & txtmodule2.SelectedValue & " AND emp_code = " & txtid.Text


            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            bindModuleAvailable()
            bindModuleGrant()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddRole.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtrole1.SelectedItem) Then
                Return
            End If

            sql = "INSERT INTO user_role (role_id , emp_code ) VALUES("
            sql &= "" & txtrole1.SelectedValue & ","
            sql &= "" & txtid.Text & ""
            sql &= ")"

            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            bindRoleAvailable()
            bindRoleGrant()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdRemoveRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveRole.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtrole2.SelectedItem) Then
                Return
            End If

            sql = "DELETE FROM user_role WHERE role_id = " & txtrole2.SelectedValue & " AND emp_code = " & txtid.Text


            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            bindRoleAvailable()
            bindRoleGrant()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddCC.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtcc.SelectedItem) Then
                Return
            End If

            sql = "INSERT INTO user_access_costcenter (costcenter_id , emp_code ) VALUES("
            sql &= "" & txtcc.SelectedValue & ","
            sql &= "" & txtid.Text & ""
            sql &= ")"

            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            bindCostCenter()
            bindCostcenterGrant()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdRemoveCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveCC.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtcc2.SelectedItem) Then
                Return
            End If

            If txtcc2.SelectedValue = Session("dept_id").ToString Then
                Return
            End If

            sql = "DELETE FROM user_access_costcenter WHERE costcenter_id = " & txtcc2.SelectedValue & " AND emp_code = " & txtid.Text

            'Response.Write(sql)
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            bindCostCenter()
            bindCostcenterGrant()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddIDP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddIDP.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtidp_cc1.SelectedItem) Then
                Return
            End If

            sql = "INSERT INTO user_access_costcenter_idp (costcenter_id , emp_code ) VALUES("
            sql &= "" & txtidp_cc1.SelectedValue & ","
            sql &= "" & txtid.Text & ""
            sql &= ")"

            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            bindCostCenterIDP()
            bindCostcenterGrantIDP()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdRemoveIDP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveIDP.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtidp_cc2.SelectedItem) Then
                Return
            End If

            If txtidp_cc2.SelectedValue = Session("dept_id").ToString Then
                '    Return
            End If

            sql = "DELETE FROM user_access_costcenter_idp WHERE costcenter_id = " & txtidp_cc2.SelectedValue & " AND emp_code = " & txtid.Text
            ' Response.Write(sql)

            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            bindCostCenterIDP()
            bindCostcenterGrantIDP()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddSSIP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddSSIP.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtssip_cc1.SelectedItem) Then
                Return
            End If

            sql = "INSERT INTO user_access_costcenter_ssip (costcenter_id , emp_code ) VALUES("
            sql &= "" & txtssip_cc1.SelectedValue & ","
            sql &= "" & txtid.Text & ""
            sql &= ")"

            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            bindCostCenterSSIP()
            bindCostcenterGrantSSIP()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdRemoveSSIP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveSSIP.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtssip_cc2.SelectedItem) Then
                Return
            End If

            If txtssip_cc2.SelectedValue = Session("costcenter_id").ToString Then
                '  Return
            End If

            sql = "DELETE FROM user_access_costcenter_ssip WHERE costcenter_id = " & txtssip_cc2.SelectedValue & " AND emp_code = " & txtid.Text


            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            bindCostCenterSSIP()
            bindCostcenterGrantSSIP()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
