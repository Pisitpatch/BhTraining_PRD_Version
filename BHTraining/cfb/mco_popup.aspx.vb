Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Threading
Imports System.Net.Mail
Imports System.Net

Partial Class cfb_mco_popup
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected irId As String = ""
    Protected concern_id As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        concern_id = Request.QueryString("concern_id")
        irId = Request.QueryString("irId")

        If IsPostBack = True Then

        Else ' First time
            bindDeptComboBox()
            bindMCOCategory()
            bindForm()
            bindDept()
            bindDoctor()
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
            sql = "SELECT * FROM ir_psm_concern_list WHERE concern_id = " & concern_id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtpsm_concern.Value = ds.Tables("t1").Rows(0)("concern_detail").ToString
            txtpsm_category.SelectedValue = ds.Tables("t1").Rows(0)("topic_id").ToString
            bindMCOSubCategory()
            txtpsm_subcategory.SelectedValue = ds.Tables("t1").Rows(0)("subtopic_id").ToString

            If ds.Tables("t1").Rows(0)("std_care_type").ToString = "1" Then
                txtpsm_std1.Checked = True
            ElseIf ds.Tables("t1").Rows(0)("std_care_type").ToString = "2" Then
                txtpsm_std2.Checked = True
            ElseIf ds.Tables("t1").Rows(0)("std_care_type").ToString = "3" Then
                txtpsm_std3.Checked = True
            End If

        Catch ex As Exception

            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDoctor()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_psm_concern_doctor WHERE concern_id = " & concern_id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtpsm_list_doctor.DataSource = ds
            txtpsm_list_doctor.DataBind()

        Catch ex As Exception

            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDept()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_psm_concern_dept WHERE concern_id = " & concern_id
            ds = conn.getDataSetForTransaction(sql, "t1")
            txtpsm_list_dept.DataSource = ds
            txtpsm_list_dept.DataBind()

        Catch ex As Exception

            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDeptComboBox() ' Combo box
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM user_dept ORDER BY dept_name_en"
            'sql &= " ORDER BY dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(sql)
            txtpsm_add_dept.DataSource = ds
            txtpsm_add_dept.DataBind()

            txtpsm_add_dept.Items.Insert(0, New ListItem("--Please select--", ""))

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindMCOCategory()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM ir_psm_category a WHERE 1 = 1 ORDER BY psm_category_name "


            ds = conn.getDataSetForTransaction(sql, "t1")

            txtpsm_category.DataSource = ds.Tables(0)
            txtpsm_category.DataBind()

            txtpsm_category.Items.Insert(0, New ListItem("-- --", ""))
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Sub bindMCOSubCategory()
        Dim ds As New DataSet
        Dim sql As String

        Try
            sql = "SELECT * FROM ir_psm_subcategory a WHERE 1 = 1 "
            If txtpsm_category.SelectedValue <> "" Then
                sql &= " AND psm_category_id = " & txtpsm_category.SelectedValue
            End If


            ds = conn.getDataSetForTransaction(sql, "t1")

            txtpsm_subcategory.DataSource = ds.Tables(0)
            txtpsm_subcategory.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Protected Sub txtpsm_category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpsm_category.SelectedIndexChanged
        bindMCOSubCategory()
    End Sub

    Protected Sub cmdSaveConcenrn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveConcenrn.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk2 As String
        Dim pk3 As String

        Try
            sql = "UPDATE ir_psm_concern_list SET concern_detail = '" & addslashes(txtpsm_concern.Value) & "' "
            sql &= " ,topic_id = '" & txtpsm_category.SelectedValue & "' "
            sql &= " ,topic_name = '" & txtpsm_category.SelectedItem.Text & "' "
            sql &= " ,subtopic_id = '" & txtpsm_subcategory.SelectedValue & "' "
            sql &= " ,subtopic_name = '" & txtpsm_subcategory.SelectedItem.Text & "' "
            If txtpsm_std1.Checked = True Then
                sql &= " , std_care_type = 1 "
                sql &= " , std_care_type_name = 'Yes' "
            ElseIf txtpsm_std2.Checked = True Then
                sql &= " , std_care_type = 2 "
                sql &= " , std_care_type_name = 'No' "
            ElseIf txtpsm_std3.Checked = True Then
                sql &= " , std_care_type = 3 "
                sql &= " , std_care_type_name = 'Borderline' "
            End If

            sql &= " WHERE concern_id = " & concern_id

            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            sql = "DELETE FROM ir_psm_concern_doctor WHERE concern_id = " & concern_id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            For i As Integer = 0 To txtpsm_list_doctor.Items.Count - 1
                pk2 = getPK("concern_doctor_id", "ir_psm_concern_doctor", conn)
                sql = "INSERT INTO ir_psm_concern_doctor (concern_doctor_id , concern_id , ir_id , concern_doctor) VALUES("
                sql &= " '" & pk2 & "' ,"
                sql &= " '" & concern_id & "' ,"
                sql &= " '" & irId & "' ,"
                sql &= " '" & txtpsm_list_doctor.Items(i).Text & "' "
                sql &= ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            sql = "DELETE FROM ir_psm_concern_dept WHERE concern_id = " & concern_id
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            For i As Integer = 0 To txtpsm_list_dept.Items.Count - 1
                pk3 = getPK("concern_dept_id", "ir_psm_concern_dept", conn)
                sql = "INSERT INTO ir_psm_concern_dept (concern_dept_id , concern_id , ir_id , concern_dept_name , costcenter_id) VALUES("
                sql &= " '" & pk3 & "' ,"
                sql &= " '" & concern_id & "' ,"
                sql &= " '" & irId & "' ,"
                sql &= " '" & txtpsm_list_dept.Items(i).Text & "' ,"
                sql &= " '" & txtpsm_list_dept.Items(i).Value & "' "
                sql &= ")"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            Next i

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdPSMAddDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMAddDoc.Click
        txtpsm_list_doctor.Items.Add(New ListItem(txtpsm_add_doctor.Text & " " & txtpsm_add_special.Value, txtpsm_add_doctor.Text & " " & txtpsm_add_special.Value))

        txtpsm_add_doctor.Text = ""
        txtpsm_add_special.Value = ""
    End Sub

  
    Protected Sub cmdPSMRemoveDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMRemoveDept.Click
        If txtpsm_list_dept.SelectedIndex = -1 Then
            Return
        End If

        txtpsm_list_dept.Items.RemoveAt(txtpsm_list_dept.SelectedIndex)
    End Sub

    Protected Sub cmdPSMDelDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMDelDoc.Click
        If txtpsm_list_doctor.SelectedIndex = -1 Then
            Return
        End If

        txtpsm_list_doctor.Items.RemoveAt(txtpsm_list_doctor.SelectedIndex)
    End Sub

    Protected Sub cmdPSMAddDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPSMAddDept.Click
        txtpsm_list_dept.Items.Add(New ListItem(txtpsm_add_dept.SelectedItem.Text, txtpsm_add_dept.SelectedValue))
        txtpsm_add_dept.SelectedIndex = 0
    End Sub
End Class
