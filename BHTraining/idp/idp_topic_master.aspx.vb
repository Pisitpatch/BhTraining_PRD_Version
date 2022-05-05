Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_idp_topic_master
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

        If IsPostBack Then

        Else ' First time



            bindGridTopic()
            bindDept()
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

    Sub bindGridTopic()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_topic a INNER JOIN idp_m_category b ON a.category_id = b.category_id "
            sql &= " WHERE a.owner_dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            sql &= " AND ISNULL(a.is_delete,0) = 0 "
            sql &= " ORDER BY topic_order_sort"

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridTopic.DataSource = ds
            GridTopic.DataBind()
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
            sql = "SELECT * FROM user_dept WHERE dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            sql &= " ORDER BY dept_name_en"

            ds = conn.getDataSetForTransaction(sql, "t1")

            txtdept.DataSource = ds
            txtdept.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub onSelectAll()
        Dim i As Integer


        Dim chk As CheckBox
        Dim h_chk As CheckBox
        h_chk = CType(GridTopic.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)
        i = GridTopic.Rows.Count

        Try

            For s As Integer = 0 To i - 1


                chk = CType(GridTopic.Rows(s).FindControl("chk"), CheckBox)


                If h_chk.Checked Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If


            Next s


        Catch ex As Exception

            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim new_order_sort As String = ""
        Dim pk As String = ""
        Dim pk2 As String
        Dim ds As New DataSet
        Try
            sql = "SELECT ISNULL(MAX(topic_order_sort),0) + 1 FROM idp_m_topic "
            ds = conn.getDataSetForTransaction(sql, "t1")
            new_order_sort = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("topic_id", "idp_m_topic", conn)
            sql = "INSERT INTO idp_m_topic (topic_id , category_id , topic_name_th , topic_name_en , topic_order_sort , create_date , create_by_name , create_by_empno , owner_dept_id , owner_dept_name ) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & txtadd_category.SelectedValue & "' ,"
            sql &= " '" & addslashes(txtadd_topic.Text) & "' ,"
            sql &= " '" & addslashes(txtadd_topic_en.Text) & "' ,"
            sql &= " '" & new_order_sort & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Session("user_fullname").ToString & "' ,"
            sql &= " '" & Session("emp_code").ToString & "' , "
            sql &= " '" & Session("dept_id").ToString & "' , "
            sql &= " '" & Session("dept_name").ToString & "'  "
            ' sql &= " '" & txtdept.Items(i).Value & "' , "
            ' sql &= " '" & txtdept.Items(i).Text & "'  "
            sql &= ")"
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            For i As Integer = 0 To txtdept.Items.Count - 1
                If txtdept.Items(i).Selected = True Then

                    pk2 = getPK("topic_dept_pk_id", "idp_m_topic_dept", conn)
                    sql = "INSERT INTO idp_m_topic_dept (topic_dept_pk_id , topic_id , topic_dept_id , topic_dept_name ) VALUES("
                    sql &= " '" & pk2 & "' ,"
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & txtdept.Items(i).Value & "' , "
                    sql &= " '" & txtdept.Items(i).Text & "'  "
                    sql &= ")"
                    '  Response.Write(sql)
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                End If
            Next i



            conn.setDBCommit()

            txtadd_topic.Text = ""
            txtadd_topic_en.Text = ""
            txtadd_category.SelectedIndex = 0
            txtdept.SelectedIndex = -1
            bindGridTopic()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        Finally

        End Try
    End Sub

    Protected Sub cmdDelTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridTopic.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridTopic.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridTopic.Rows(s).FindControl("chk"), CheckBox)

                If chk.Checked = True Then
                    ' sql = "DELETE FROM idp_m_topic WHERE topic_id = " & lbl.Text
                    sql = "UPDATE idp_m_topic SET is_delete = 1  WHERE topic_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindGridTopic()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridTopic_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridTopic.PageIndexChanging
        GridTopic.PageIndex = e.NewPageIndex
        bindGridTopic()
    End Sub

    Protected Sub GridTopic_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridTopic.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblDeptList As Label = CType(e.Row.FindControl("lblDeptList"), Label)
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            '  Dim chk As CheckBox = CType(e.Row.FindControl("chk"), CheckBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM idp_m_topic_dept WHERE topic_id = " & lblPK.Text
                ds = conn.getDataSetForTransaction(sql, "t1")
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblDeptList.Text &= " - " & ds.Tables("t1").Rows(i)("topic_dept_name").ToString & "<br/>"
                Next

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()

            End Try

        End If
    End Sub

    Protected Sub GridTopic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridTopic.SelectedIndexChanged

    End Sub
End Class
