Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class dept_detail
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    'Private conn1 As DBUtil = CType(Session("myConn"), DBUtil)
    Private id As String = "0"
    Private mode As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        '    txtempname.Attributes.Add("onfocus", "this.select()")
        '   txtempname.Attributes.Add("onblur", "onCheckBlank(this)")
        id = Request.QueryString("id")
        mode = Request.QueryString("mode")

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            bindComboDivision()
            ' bindMailGroup()
            bindForm()
            If mode = "edit" Then
                bindMailGroup()
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

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet


        Try
            sql = "SELECT * FROM user_dept WHERE dept_id = " & id
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            txtid.Text = ds.Tables(0).Rows(0)("costcenter_id").ToString
            txtname.Text = ds.Tables(0).Rows(0)("dept_name_en").ToString
            txtnamelocal.Text = ds.Tables(0).Rows(0)("dept_name_th").ToString
            txtdivision.SelectedValue = ds.Tables(0).Rows(0)("division_id").ToString
            '  txtempname.Text = ds.Tables(0).Rows(0)("dept_manager_name").ToString
            ' txtempcode.Text = ds.Tables(0).Rows(0)("emp_code").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Clear()
            ds = Nothing
        End Try
    End Sub

    Sub bindComboDivision()
        Dim sql As String
        Dim ds As New DataSet


        Try
            sql = "SELECT * FROM user_division ORDER BY division_name_en ASC "
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            txtdivision.DataSource = ds
            txtdivision.DataBind()

            txtdivision.Items.Insert(0, New ListItem("-- Please Select --", ""))
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Clear()
            ds = Nothing
        End Try
    End Sub

  

    Sub bindMailGroup()
        Dim sql As String
        Dim ds As New DataSet


        Try
            sql = "SELECT * FROM user_mail_group WHERE dept_id = " & id
            '  Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("dept_management.aspx")
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

          

        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim sql As String
        Dim result As String
        Try
          

            sql = "DELETE FROM user_mail_group WHERE mail_id = " & GridView1.DataKeys(e.RowIndex).Value & ""
            result = conn.executeSQLForTransaction(sql)

            If result <> "" Then
                Throw New Exception(result)
            End If

            conn.setDBCommit()
            bindMailGroup()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
      
    End Sub



    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

   
    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String

        Try
            pk = getPK("mail_id", "user_mail_group", conn)
            sql = "INSERT INTO user_mail_group (mail_id , dept_id , emp_code , full_name , email , job_type , job_title) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & id & "' ,"
            sql &= " '" & txtemp_code.Text & "' ,"
            sql &= " '" & txtemp_name.Text & "' ,"
            sql &= " '" & txtemail.Text & "' ,"
            sql &= " '" & txtjob_type.Text & "' ,"
            sql &= " '" & txtjob_title.Text & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            bindMailGroup()

            txtemp_name.Text = ""
            txtemp_code.Text = ""
            txtjob_title.Text = ""
            txtjob_type.Text = ""
            txtemail.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
        End Try
    End Sub

  
    Protected Sub cmdSaveDept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveDept.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE user_dept SET division_id = '" & txtdivision.SelectedValue & "'"
            sql &= " , dept_code = '" & txtid.Text & "'"
            sql &= " , dept_name_th = '" & txtnamelocal.Text & "'"
            sql &= " , dept_name_en = '" & txtname.Text & "' "
            '  sql &= " , dept_manager_name = '" & txtempname.Text & "' "
            ' sql &= " , emp_code = '" & txtempcode.Text & "' "

            sql &= " WHERE dept_id = " & id
            'Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()
            ' bindCostCenter()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

  
End Class
