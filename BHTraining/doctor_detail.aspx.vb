Imports System.Data.SqlClient
Imports System.Data

Partial Class doctor_detail
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Private id As String = "0"
    Private mode As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")

        If Not Page.IsPostBack Then



            If mode = "edit" Then

                bindModuleAvailable()
                bindModuleGrant()
                bindRoleAvailable()
                bindRoleGrant()

                bindCostCenter()
                bindCostcenterGrant()
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

    Sub bindModuleAvailable()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT * FROM m_module WHERE module_id NOT IN  ("
            sql &= "SELECT module_id FROM doctor_module WHERE emp_code = " & id
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
            sql = "SELECT * FROM doctor_module a INNER JOIN m_module b ON a.module_id = b.module_id  WHERE a.emp_code =  " & id
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
            sql &= "SELECT role_id FROM doctor_role WHERE emp_code = " & id
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
            sql = "SELECT * FROM doctor_role a INNER JOIN m_role b ON a.role_id = b.role_id  WHERE a.emp_code =  " & id
            ' Response.Write(sql)
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

    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Response.Redirect("doctor_management.aspx")
    End Sub

    Protected Sub cmdAddModule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddModule.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If txtmodule1.SelectedValue = "" Then
                Return
            End If
            sql = "INSERT INTO doctor_module (module_id , emp_code ) VALUES("
            sql &= "" & txtmodule1.SelectedValue & ","
            sql &= "" & id & ""
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
            If txtmodule2.SelectedValue = "" Then
                Return
            End If
            sql = "DELETE FROM doctor_module WHERE module_id = " & txtmodule2.SelectedValue & " AND emp_code = " & id


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
            If txtrole1.SelectedValue = "" Then
                Return
            End If

            sql = "INSERT INTO doctor_role (role_id , emp_code ) VALUES("
            sql &= "" & txtrole1.SelectedValue & ","
            sql &= "" & id & ""
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
            If txtrole2.SelectedValue = "" Then
                Return
            End If
            sql = "DELETE FROM doctor_role WHERE role_id = " & txtrole2.SelectedValue & " AND emp_code = " & id


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

    Sub bindCostCenter()
        Dim reader As SqlDataReader
        Dim sql As String

        Try
            sql = "SELECT name_en AS costcenter_name , costcenter_id FROM user_costcenter "
            sql &= " WHERE costcenter_id NOT IN (SELECT costcenter_id FROM user_access_costcenter WHERE emp_code = " & id & ")"
            sql &= " ORDER BY costcenter_name"
            reader = conn.getDataReader(sql, "t1")
            ' Response.Write(sql)
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
            sql = "SELECT c.name_en AS costcenter_name , a.costcenter_id  FROM doctor_access_costcenter a INNER JOIN m_doctor b ON a.emp_code = b.emp_no "
            sql &= " INNER JOIN user_costcenter c ON a.costcenter_id = c.costcenter_id "
            sql &= " WHERE a.emp_code =  " & id
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
            ' reader.Close()
        End Try
    End Sub

    Protected Sub cmdAddCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddCC.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            If IsNothing(txtcc.SelectedItem) Then
                Return
            End If

            sql = "INSERT INTO doctor_access_costcenter (costcenter_id , emp_code ) VALUES("
            sql &= "" & txtcc.SelectedValue & ","
            sql &= "" & id & ""
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

            ' If txtcc2.SelectedValue = Session("costcenter_id").ToString Then
            '      Return
            ' End If

            sql = "DELETE FROM doctor_access_costcenter WHERE costcenter_id = " & txtcc2.SelectedValue & " AND emp_code = " & id


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
End Class
