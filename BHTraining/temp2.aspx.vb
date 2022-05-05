Imports System.Data
Partial Class temp2
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""

        Try
            sql = " insert into user_profile (emp_code , emp_type , job_type , job_title , dept_id , dept_name , costcenter_id"
            sql &= ", costcentre_name_e , costcentre_name_l , user_fullname , user_fullname_local)"
            sql &= " select Employee_id , EmployeeType , JobType , Job_Title , Cost_Center , Department"
            sql &= ", Cost_Center ,  Department , Department , First_Name_EN + ' ' + Last_Name_EN"
            sql &= ", First_Name_TH + ' ' + Last_Name_TH"

            sql &= " from temp_BHUser "
            sql &= " where Employee_id not in (select emp_code from user_profile)"


            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If


            sql = "   update user_profile  set "
            sql &= " user_fullname = (select First_Name_EN + ' ' + Last_Name_EN from temp_BHUser where user_profile.emp_code = Employee_id)"

            sql &= ", user_fullname_local = (select First_Name_TH + ' ' + Last_Name_TH from temp_BHUser where user_profile.emp_code = Employee_id)"
            sql &= ", emp_type = (select EmployeeType from temp_BHUser where user_profile.emp_code = Employee_id)"
            sql &= ", job_type = (select JobType from temp_BHUser where user_profile.emp_code = Employee_id)"
            sql &= ", job_title = (select Job_Title from temp_BHUser where user_profile.emp_code = Employee_id)"
            '--, termination_date = (select termination_date from temp_BHUser where user_profile.emp_code = Employee_id)
            '--, dob = (select dob from temp_BHUser where user_profile.emp_code = Employee_id)
            '--, age = (select age from temp_BHUser where user_profile.emp_code = Employee_id)
            sql &= ", dept_id = (select Cost_Center from temp_BHUser where user_profile.emp_code = Employee_id)"
            '--, costcenter_id = (select Cost_Center from temp_BHUser where user_profile.emp_code = Employee_id)
            sql &= ", dept_name = (select Department from temp_BHUser where user_profile.emp_code = Employee_id)"
            sql &= ", costcenter_id = (select Cost_Center from temp_BHUser where user_profile.emp_code = Employee_id)"
            sql &= ", costcentre_name_e = (select Department from temp_BHUser where user_profile.emp_code = Employee_id)"
            sql &= ", costcentre_name_l = (select Department from temp_BHUser where user_profile.emp_code = Employee_id)"
            sql &= " where emp_code in (select Employee_id from temp_BHUser)"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "UPDATE user_profile SET dept_id = dept_id * 10  , costcenter_id = dept_id * 10 WHERE dept_id = 8610 AND dept_name LIKE 'VTL%'  "
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "UPDATE user_profile SET dept_id = dept_id * 10  , costcenter_id = dept_id * 10 WHERE dept_id = 7171 AND dept_name LIKE 'VTL%'  "
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "UPDATE user_profile SET dept_id = dept_id * 10  , costcenter_id = dept_id * 10 WHERE dept_id = 8645 AND dept_name LIKE 'VTL%'  "
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "UPDATE user_profile SET dept_id = dept_id * 10  , costcenter_id = dept_id * 10 WHERE dept_id = 9102 AND dept_name LIKE 'Vitech%'  "
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_access_costcenter (costcenter_id , emp_code)"
            sql &= " select dept_id , emp_code from user_profile"
            sql &= " where emp_code not in (select emp_code from user_access_costcenter )"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = " insert into user_access_costcenter_idp (costcenter_id , emp_code)"
            sql &= " select dept_id , emp_code from user_profile"
            sql &= " where emp_code not in (select emp_code from user_access_costcenter_idp )"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_access_costcenter_ssip (costcenter_id , emp_code)"
            sql &= " select dept_id , emp_code from user_profile"
            sql &= " where emp_code not in (select emp_code from user_access_costcenter_ssip )"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_role (role_id , emp_code)"
            sql &= " select 2 , emp_code from user_profile"
            sql &= " where emp_code not in (select emp_code from user_role where role_id = 2)"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_role (role_id , emp_code)"
            sql &= " select 7 , emp_code from user_profile"
            sql &= " where emp_code not in (select emp_code from user_role where role_id = 7)"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_role (role_id , emp_code)"
            sql &= " select 12 , emp_code from user_profile"
            sql &= " where emp_code not in (select emp_code from user_role where role_id = 12)"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_role (role_id , emp_code)"
            sql &= " select 201 , emp_code from user_profile"
            sql &= " where emp_code not in (select emp_code from user_role where role_id = 201)"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_role (role_id , emp_code)"
            sql &= " select 301 , emp_code from user_profile"
            sql &= " where emp_code not in (select emp_code from user_role where role_id = 301)"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_module (emp_code ,module_id)"
            sql &= " select emp_code , 2  from user_profile "
            sql &= " where emp_code not in (select emp_code from user_module where module_id = 2 )"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_module (emp_code ,module_id)"
            sql &= " select emp_code , 1  from user_profile "
            sql &= " where emp_code not in (select emp_code from user_module where module_id = 1 )"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_module (emp_code ,module_id)"
            sql &= " select emp_code , 3  from user_profile "
            sql &= " where emp_code not in (select emp_code from user_module where module_id = 3 )"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_module (emp_code ,module_id)"
            sql &= " select emp_code , 4  from user_profile "
            sql &= " where emp_code not in (select emp_code from user_module where module_id = 4 )"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "update user_profile set user_login = emp_code"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            sql = "insert into user_dept (dept_id , costcenter_id , dept_name_th , dept_name_en)"
            sql &= " select dept_id , dept_id , costcentre_name_e , costcentre_name_l from user_profile"
            sql &= " where costcentre_name_e is not null and dept_id not in (select dept_id from user_dept)"
            sql &= " group by dept_id , costcentre_name_e , costcentre_name_l"
            errorMsg = conn.executeSQL(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
            Return
        End Try

        Response.Redirect("user_management.aspx")
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
End Class
