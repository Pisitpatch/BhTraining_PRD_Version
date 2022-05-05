Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_idp_master
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected flag As String = ""
    Protected req As String = ""

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        flag = Request.QueryString("flag")
        If flag = "ladder" Then
            Me.MasterPageFile = "IDP2_MasterPage.master"
        End If

    End Sub

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

        viewtype = Request.QueryString("viewtype")
        flag = Request.QueryString("flag")



        If viewtype <> "" Then
            Session("viewtype") = viewtype
        Else
            viewtype = Session("viewtype")
        End If

        If viewtype <> "hr" And viewtype <> "educator" Then
            Response.Write("No permission")
            Response.End()
        End If

        If IsPostBack Then

        Else ' First time

            If viewtype = "educator" Then
                tab_category.Visible = False
                tab_expect.Visible = False
                tab_method.Visible = False
            End If

            If flag = "ladder" Then
                tab_category.Visible = True
                tab_expect.Visible = False
                tab_method.Visible = False
                panelTopicLadder.Visible = True
                chkAddUnique.Visible = False
                lblCategory.Text = "Nursing Clinical Ladder Category"
                lblTopic.Text = "Nursing Clinical Ladder Topic Setting"
                lblHeader.Text = "Nursing Clinical Ladder Master Data"
            Else
                ' Response.Write(1234)
                panelTopicLadder.Visible = False
                GridTopic.Columns(3).Visible = False
                GridTopic.Columns(4).Visible = False
                GridTopic.Columns(5).Visible = False
                cmdSaveMapTopic.Visible = False
                If viewtype = "hr" Then
                    tab_message.Visible = True
                End If
            End If


            bindMessage()
            bindCategory()
            bindGridMasterTopic()
            bindGridTopic()
            bindGridCategory()
            bindGridExpect()
            bindGridMethod()
            bindMasterTopic()

            bindCCAll()
            bindConfig()
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

    Sub bindMessage()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_message WHERE idp_message_id = 1  "
          
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtmessage.Content = ds.Tables("t1").Rows(0)("message_detail").ToString

            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPicture.Text = "<img src='../share/idp/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' />"
            End If


            sql = "SELECT * FROM idp_message WHERE idp_message_id = 2  "

            ds = conn.getDataSetForTransaction(sql, "t1")

            txtmessageIDP.Content = ds.Tables("t1").Rows(0)("message_detail").ToString
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPictureIDP.Text = "<img src='../share/idp/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' />"
            End If

            If ds.Tables("t1").Rows(0)("is_delay").ToString = "1" Then
                chkDelay.Checked = True
            Else
                chkDelay.Checked = False
            End If

            sql = "SELECT * FROM idp_message WHERE idp_message_id = 3  "

            ds = conn.getDataSetForTransaction(sql, "t1")

            txtmessageExternal.Content = ds.Tables("t1").Rows(0)("message_detail").ToString
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPictureExt.Text = "<img src='../share/idp/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' />"
            End If
            ' txtadd_topic.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCCAll()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM user_dept  "
            If viewtype = "educator" Then
                'sql &= " AND dept_id IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code").ToString & ")"
            End If
            sql &= " ORDER BY dept_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblCCAll.DataSource = ds
            lblCCAll.DataBind()

            ' txtadd_topic.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindCategory()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , category_name_th + ' / ' + category_name_en AS category_name FROM idp_m_category WHERE ISNULL(is_delete,0) = 0 "
            If flag = "ladder" Then
                sql &= " AND category_type = 'Nursing' "
            Else
                sql &= " AND category_type = 'General' "
            End If
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtadd_category.DataSource = ds
            txtadd_category.DataBind()

            txtadd_category.Items.Insert(0, New ListItem("-- Please Select", ""))


            txtfind_category.DataSource = ds
            txtfind_category.DataBind()

            txtfind_category.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridTopic()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_topic a INNER JOIN idp_m_category b ON a.category_id = b.category_id WHERE ISNULL(a.is_delete,0) = 0 "
            If viewtype = "educator" Then
                sql &= " AND a.owner_dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code") & ")"
                
            End If

            If flag = "ladder" Then
                sql &= " AND b.category_type = 'Nursing' "
            Else
                sql &= " AND b.category_type = 'General' "
            End If

            If txtfind_category.SelectedValue <> "" Then
                sql &= " AND b.category_id = " & txtfind_category.SelectedValue
            End If

          

            If txtfind_keyword.Text <> "" Then
                sql &= " AND a.topic_name_th LIKE '%" & txtfind_keyword.Text.ToLower & "%' OR LOWER(a.topic_name_en) LIKE '%" & txtfind_keyword.Text.ToLower & "%'  "
            End If

            sql &= " ORDER BY topic_id"
            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            GridTopic.DataSource = ds
            GridTopic.DataBind()

            lblNum2.text = ds.Tables("t1").Rows.Count
        Catch ex As Exception
            Response.Write("bindGridTopic :: " & ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridMasterTopic()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_master_topic a  WHERE ISNULL(a.is_delete,0) = 0  "
            If flag = "ladder" Then
                sql &= " AND a.is_ladder_topic = 1 "
            Else
                sql &= " AND ISNULL(a.is_ladder_topic,0) = 0 "
            End If

            If viewtype = "educator" Then
                sql &= " AND a.owner_dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code") & ")"
            End If
            sql &= " ORDER BY master_topic_name_th , master_topic_name_en"
            ' Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            GridMasterTopic.DataSource = ds
            GridMasterTopic.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridCategory()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * , CASE WHEN is_delete = 1 THEN '<span style=''color:red''>Inactive</span>' Else '<span style=''color:green''>Active<span>' END AS status FROM idp_m_category a WHERE 1 = 1 "
            If flag = "ladder" Then
                sql &= " AND category_type = 'Nursing' "
            Else
                sql &= " AND category_type = 'General' "
            End If
            sql &= " ORDER BY category_order_sort  "

            '   sql &= " ORDER BY template_is_require DESC , template_order_sort ASC"

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridCategory.DataSource = ds
            GridCategory.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridExpect()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_expect a WHERE ISNULL(is_expect_delete,0) = 0 ORDER BY expect_order_sort "

            '   sql &= " ORDER BY template_is_require DESC , template_order_sort ASC"

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridExpect.DataSource = ds
            GridExpect.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridMethod()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_m_method a ORDER BY method_order_sort "

            '   sql &= " ORDER BY template_is_require DESC , template_order_sort ASC"

            ds = conn.getDataSetForTransaction(sql, "t1")

            GridMethod.DataSource = ds
            GridMethod.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

  

    Sub bindMasterTopic()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT *  FROM idp_m_master_topic a WHERE ISNULL(is_delete,0) = 0 AND is_active = 1 "
            If flag = "ladder" Then
                sql &= " AND a.is_ladder_topic = 1 "
                '  sql &= " AND a.category_id"
            Else
                sql &= " AND ISNULL(a.is_ladder_topic ,0) = 0 "
            End If

            If viewtype = "educator" Then
                sql &= " AND owner_dept_id  IN (SELECT costcenter_id FROM user_access_costcenter_idp WHERE emp_code = " & Session("emp_code") & ")"
            End If
            If txtadd_category.SelectedValue <> "" Then
                'sql &= " AND master_topic_name_th NOT IN (SELECT topic_name_th FROM idp_m_topic WHERE ISNULL(is_delete,0) = 0 AND category_id = " & txtadd_category.SelectedValue & " )"
            Else
                sql &= " AND 1 > 2 "
            End If
            sql &= " ORDER BY master_topic_name_th , master_topic_name_en"
            'Response.Write(sql)
            '   sql &= " ORDER BY template_is_require DESC , template_order_sort ASC"

            ds = conn.getDataSetForTransaction(sql, "t1")

            txtadd_topic.DataSource = ds
            txtadd_topic.DataBind()

            txtadd_topic.Items.Insert(0, New ListItem("-- Please Select", ""))
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdAddTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim new_order_sort As String = ""
        Dim pk As String = ""
        Dim pk2 As String = ""
        Dim ds As New DataSet
        Dim name_th As String = ""
        Dim name_en As String = ""
        Try
            sql = "SELECT * FROM idp_m_master_topic WHERE master_topic_id = " & txtadd_topic.SelectedValue
            ds = conn.getDataSetForTransaction(sql, "t0")
            name_th = ds.Tables(0).Rows(0)("master_topic_name_th").ToString
            name_en = ds.Tables(0).Rows(0)("master_topic_name_en").ToString

            sql = "SELECT ISNULL(MAX(topic_order_sort),0) + 1 FROM idp_m_topic "
            ds = conn.getDataSetForTransaction(sql, "t1")
            new_order_sort = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("topic_id", "idp_m_topic", conn)
            sql = "INSERT INTO idp_m_topic (topic_id , master_topic_id , category_id , topic_name_th , topic_name_en , topic_order_sort , create_date , create_by_name , create_by_empno , owner_dept_id , owner_dept_name , is_unit_mandatory , is_nursing_unique , is_delete ) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & txtadd_topic.SelectedValue & "' ,"
            sql &= " '" & txtadd_category.SelectedValue & "' ,"
            sql &= " '" & addslashes(name_th) & "' ,"
            sql &= " '" & addslashes(name_en) & "' ,"
            sql &= " '" & new_order_sort & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Session("user_fullname").ToString & "' ,"
            sql &= " '" & Session("emp_code").ToString & "' , "
            sql &= " '" & Session("dept_id").ToString & "' , "
            sql &= " '" & Session("dept_name").ToString & "'  ,"
            sql &= " '" & txtscope.SelectedValue & "' ,"
            If chkAddUnique.Checked = True Then
                sql &= "  1 ,"
            Else
                sql &= "  0 ,"
            End If
            sql &= " 0 "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)
            End If

            'If flag = "ladder" Then
            '    sql = "UPDATE idp_m_topic SET nursing_score = '" & txtadd_score.Text & "' , nursing_limit = '" & txtadd_limit.Text & "' "
            '    sql &= " WHERE topic_id = " & pk
            '    'Response.Write(sql)
            '    errorMsg = conn.executeSQLForTransaction(sql)
            '    If errorMsg <> "" Then
            '        Throw New Exception(errorMsg)
            '    End If
            'End If

            If txtscope.SelectedValue = "1" Then ' เป็น Unit Mandatory , department level
                For i As Integer = 0 To lblCCSelect.Items.Count - 1
                    pk2 = getPK("topic_dept_pk_id", "idp_m_topic_dept", conn)
                    sql = "INSERT INTO idp_m_topic_dept (topic_dept_pk_id , topic_id , topic_dept_id , topic_dept_name  ) VALUES("
                    sql &= " '" & pk2 & "' ,"
                    sql &= " '" & pk & "' ,"
                    sql &= " '" & lblCCSelect.Items(i).Value & "' , "
                    sql &= " '" & lblCCSelect.Items(i).Text & "'  "

                    sql &= ")"
                    '  Response.Write(sql)
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                Next i
            End If

            conn.setDBCommit()

            bindGridTopic()
            txtadd_topic.SelectedIndex = 0
            panel_dept.Visible = False
            txtadd_category.SelectedIndex = 0
            chkAddUnique.Checked = False
            txtscope.SelectedValue = "0"
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        Finally

        End Try
    End Sub

    Sub addTopicWithNoCommit()
        Dim sql As String
        Dim errorMsg As String
        Dim new_order_sort As String = ""
        Dim pk As String = ""
        Dim ds As New DataSet
        Dim name_th As String = ""
        Dim name_en As String = ""

        sql = "SELECT * FROM idp_m_master_topic WHERE master_topic_id = " & txtadd_topic.SelectedValue
        ds = conn.getDataSetForTransaction(sql, "t0")
        name_th = ds.Tables(0).Rows(0)("master_topic_name_th").ToString
        name_en = ds.Tables(0).Rows(0)("master_topic_name_en").ToString

        sql = "SELECT ISNULL(MAX(topic_order_sort),0) + 1 FROM idp_m_topic "
        ds = conn.getDataSetForTransaction(sql, "t1")
        new_order_sort = ds.Tables(0).Rows(0)(0).ToString

        pk = getPK("topic_id", "idp_m_topic", conn)
        sql = "INSERT INTO idp_m_topic (topic_id , category_id , topic_name_th , topic_name_en , topic_order_sort , create_date , create_by_name , create_by_empno ) VALUES("
        sql &= " '" & pk & "' ,"
        sql &= " '" & txtadd_category.SelectedValue & "' ,"
        sql &= " '" & addslashes(name_th) & "' ,"
        sql &= " '" & addslashes(name_en) & "' ,"
        sql &= " '" & new_order_sort & "' ,"
        sql &= " GETDATE() ,"
        sql &= " '" & Session("user_fullname").ToString & "' ,"
        sql &= " '" & Session("emp_code").ToString & "' "
        sql &= ")"
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If

      
        txtadd_topic.SelectedIndex = 0
        txtadd_category.SelectedIndex = 0
     
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
                    sql = "DELETE FROM idp_m_topic WHERE topic_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If

                    sql = "DELETE FROM idp_m_topic_dept WHERE topic_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindGridTopic()
            txtadd_topic.SelectedIndex = 0
            panel_dept.Visible = False
            txtadd_category.SelectedIndex = 0
            txtscope.SelectedValue = "0"
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub



    Protected Sub cmdAddCat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddCat.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim new_order_sort As String = ""
        Dim ds As New DataSet
        Try
            sql = "SELECT ISNULL(MAX(category_order_sort),0) + 1 FROM idp_m_category "
            ds = conn.getDataSetForTransaction(sql, "t1")
            new_order_sort = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("category_id", "idp_m_category", conn)
            sql = "INSERT INTO idp_m_category (category_id , category_name_th , category_name_en , category_order_sort , create_date , create_by_name , create_by_empno , category_type ) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & addslashes(txtadd_mastercategory.Text) & "' ,"
            sql &= " '" & addslashes(txtadd_mastercategory.Text) & "' ,"
            sql &= " '" & new_order_sort & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Session("user_fullname").ToString & "' ,"
            sql &= " '" & Session("emp_code").ToString & "' , "
            If flag = "ladder" Then
                sql &= " 'Nursing'  "
            Else
                sql &= " 'General'  "
            End If


            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_mastercategory.Text = ""
            bindGridCategory()
            bindCategory()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        Finally

        End Try
    End Sub

    Protected Sub cmdDelCat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelCat.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridCategory.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridCategory.Rows(s).FindControl("lblPK0"), Label)
                chk = CType(GridCategory.Rows(s).FindControl("chk0"), CheckBox)

                If chk.Checked = True Then
                    'sql = "DELETE FROM idp_m_category WHERE category_id = " & lbl.Text
                    sql = "UPDATE idp_m_category SET is_delete = 1 WHERE category_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindGridCategory()
            bindCategory()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdOrderCat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOrderCat.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim txtorder As TextBox
        Dim txtnameth As TextBox
        Dim txtnameen As TextBox

        i = GridCategory.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridCategory.Rows(s).FindControl("lblPK0"), Label)
                txtorder = CType(GridCategory.Rows(s).FindControl("txtorder0"), TextBox)
                txtnameth = CType(GridCategory.Rows(s).FindControl("txtname_th"), TextBox)
                txtnameen = CType(GridCategory.Rows(s).FindControl("txtname_en"), TextBox)

                sql = "UPDATE idp_m_category SET category_order_sort = '" & txtorder.Text & "' "
                sql &= " , category_name_th = '" & addslashes(txtnameth.Text) & "' "
                sql &= " , category_name_en = '" & addslashes(txtnameen.Text) & "' "
                sql &= " , create_date = GETDATE() "
                sql &= " , create_by_name = '" & Session("user_fullname").ToString & "' "
                sql &= " , create_by_empno = '" & Session("emp_code").ToString & "' "

                sql &= " WHERE category_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
            Next s

            conn.setDBCommit()
            bindGridCategory()
            bindCategory()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddExpect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddExpect.Click
        Dim sql As String
        Dim errorMsg As String
        Dim new_order_sort As String = ""
        Dim pk As String = ""
        Dim ds As New DataSet
        Try
            sql = "SELECT ISNULL(MAX(expect_order_sort),0) + 1 FROM idp_m_expect "
            ds = conn.getDataSetForTransaction(sql, "t1")
            new_order_sort = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("expect_id", "idp_m_expect", conn)
            sql = "INSERT INTO idp_m_expect (expect_id , expect_detail , expect_detail_en , expect_order_sort , create_date , create_by_name , create_by_empno ) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & addslashes(txtadd_expect.Text) & "' ,"
            sql &= " '" & addslashes(txtadd_expect_en.Text) & "' ,"
            sql &= " '" & new_order_sort & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Session("user_fullname").ToString & "' ,"
            sql &= " '" & Session("emp_code").ToString & "' "
            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_expect.Text = ""
            txtadd_expect_en.Text = ""
            bindGridExpect()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        Finally

        End Try
    End Sub

    Protected Sub cmdDelExpect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelExpect.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridExpect.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridExpect.Rows(s).FindControl("lblPK1"), Label)
                chk = CType(GridExpect.Rows(s).FindControl("chk1"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM idp_m_expect WHERE expect_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If

                   
                End If
            Next s

            conn.setDBCommit()

            bindGridExpect()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdOrderExpect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOrderExpect.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim txtorder As TextBox
        Dim txtname_th As TextBox
        Dim txtname_en As TextBox

        i = GridExpect.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridExpect.Rows(s).FindControl("lblPK1"), Label)
                txtorder = CType(GridExpect.Rows(s).FindControl("txtorder1"), TextBox)
                txtname_th = CType(GridExpect.Rows(s).FindControl("txtname_th"), TextBox)
                txtname_en = CType(GridExpect.Rows(s).FindControl("txtname_en"), TextBox)

                sql = "UPDATE idp_m_expect SET expect_order_sort = '" & txtorder.Text & "' "
                sql &= " , expect_detail = '" & addslashes(txtname_th.Text) & "' "
                sql &= " , expect_detail_en = '" & addslashes(txtname_en.Text) & "' "
                sql &= " , create_date = GETDATE() "
                sql &= " , create_by_name = '" & Session("user_fullname").ToString & "' "
                sql &= " , create_by_empno = '" & Session("emp_code").ToString & "' "
                sql &= " WHERE expect_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
            Next s

            conn.setDBCommit()
            bindGridExpect()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdAddMethod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddMethod.Click
        Dim sql As String
        Dim errorMsg As String
        Dim new_order_sort As String = ""
        Dim pk As String = ""
        Dim ds As New DataSet
        Try
            sql = "SELECT ISNULL(MAX(method_order_sort),0) + 1 FROM idp_m_method "
            ds = conn.getDataSetForTransaction(sql, "t1")
            new_order_sort = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("method_id", "idp_m_method", conn)
            sql = "INSERT INTO idp_m_method (method_id , method_name , method_order_sort , create_date , create_by_name  ) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & addslashes(txtadd_method.Text) & "' ,"
            sql &= " '" & new_order_sort & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Session("user_fullname").ToString & "' "

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            conn.setDBCommit()

            txtadd_method.Text = ""
            bindGridMethod()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
            Return
        Finally

        End Try
    End Sub

    Protected Sub cmdDeleteMethod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteMethod.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridMethod.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridMethod.Rows(s).FindControl("lblPK1"), Label)
                chk = CType(GridMethod.Rows(s).FindControl("chk1"), CheckBox)

                If chk.Checked = True Then
                    sql = "DELETE FROM idp_m_method WHERE method_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindGridMethod()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdSaveMethod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveMethod.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim txtorder As TextBox
        Dim txtname_th As TextBox
        Dim txtname_en As TextBox

        i = GridMethod.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridMethod.Rows(s).FindControl("lblPK1"), Label)
                txtorder = CType(GridMethod.Rows(s).FindControl("txtorder1"), TextBox)
                txtname_th = CType(GridMethod.Rows(s).FindControl("txtname_th"), TextBox)
                txtname_en = CType(GridMethod.Rows(s).FindControl("txtname_en"), TextBox)

                sql = "UPDATE idp_m_method SET method_order_sort = '" & txtorder.Text & "' "
                sql &= " , method_name_th = '" & addslashes(txtname_th.Text) & "' "
                sql &= " , method_name = '" & addslashes(txtname_en.Text) & "' "
                sql &= " , create_date = GETDATE() "
                sql &= " , create_by_name = '" & Session("user_fullname").ToString & "' "
                sql &= " WHERE method_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
            Next s

            conn.setDBCommit()
            bindGridMethod()
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
            Dim lblRelateDept As Label = CType(e.Row.FindControl("lblRelateDept"), Label)
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
            Dim lblCategory As Label = CType(e.Row.FindControl("lblCategory"), Label)
            Dim lblunit As Label = CType(e.Row.FindControl("lblunit"), Label)
            Dim chkUnique As CheckBox = CType(e.Row.FindControl("chkUnique"), CheckBox)
            Dim lblUnique As Label = CType(e.Row.FindControl("lblUnique"), Label)

            Dim sql As String
            Dim ds As New DataSet

            Try
                If lblunit.Text = "1" Then ' ถ้าเป็น Unit Mandatory , Department Level
                    e.Row.Cells(1).ForeColor = Drawing.Color.HotPink
                    e.Row.Cells(2).ForeColor = Drawing.Color.HotPink
                    e.Row.Cells(3).ForeColor = Drawing.Color.HotPink
                    e.Row.Cells(4).ForeColor = Drawing.Color.HotPink
                    ' e.Row.ForeColor = Drawing.Color.Pink
                    e.Row.Font.Bold = True
                End If

                If lblUnique.Text = "1" Then
                    chkUnique.Checked = True
                    'Response.Write("x")
                    lblUnique.Text = "Yes"
                Else
                    chkUnique.Checked = False
                    lblUnique.Text = "No"
                End If

                'sql = "SELECT * FROM idp_m_topic_dept WHERE topic_id = " & lblPK.Text
                'ds = conn.getDataSetForTransaction(sql, "t1")
                'For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                '    lblRelateDept.Text &= " - " & ds.Tables("t1").Rows(i)("topic_dept_name").ToString & "<br/>"
                'Next

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()

            End Try
        End If
    End Sub

    Protected Sub GridTopic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridTopic.SelectedIndexChanged

    End Sub

    Protected Sub cmdFindTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFindTopic.Click
        bindGridTopic()
    End Sub

    Protected Sub cmdAddMasterTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddMasterTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim pk As String = ""
        Dim new_order_sort As String = ""
        Dim ds As New DataSet
        Try
            'sql = "SELECT ISNULL(MAX(category_order_sort),0) + 1 FROM idp_m_category "
            'ds = conn.getDataSetForTransaction(sql, "t1")
            'new_order_sort = ds.Tables(0).Rows(0)(0).ToString

            pk = getPK("master_topic_id", "idp_m_master_topic", conn)
            sql = "INSERT INTO idp_m_master_topic (master_topic_id , master_topic_name_th , master_topic_name_en , topic_create_by , topic_last_update , topic_last_update_ts , owner_dept_id , owner_dept_name , is_delete , is_active ) VALUES("
            sql &= " '" & pk & "' ,"
            sql &= " '" & addslashes(txtmaster_topic_th.Text) & "' ,"
            sql &= " '" & addslashes(txtmaster_topic_en.Text) & "' ,"
            sql &= " '" & Session("user_fullname").ToString & "' ,"
            sql &= " GETDATE() ,"
            sql &= " '" & Date.Now.Ticks & "' ,"
            sql &= " '" & Session("dept_id").ToString & "' ,"
            sql &= " '" & Session("dept_name").ToString & "' ,"
            sql &= " 0 ,"
            sql &= " 1 "

            sql &= ")"

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            If flag = "ladder" Then
                sql = "UPDATE idp_m_master_topic SET is_ladder_topic = 1 WHERE master_topic_id = " & pk
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If
            End If
         

            conn.setDBCommit()

            txtmaster_topic_th.Text = ""
            txtmaster_topic_en.Text = ""
            bindGridMasterTopic()
            bindMasterTopic()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message & sql)
            Return
        Finally

        End Try
    End Sub

    Protected Sub GridMasterTopic_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridMasterTopic.PageIndexChanging
        GridMasterTopic.PageIndex = e.NewPageIndex
        bindGridMasterTopic()
    End Sub

    Protected Sub GridMasterTopic_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridMasterTopic.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblStatusId As Label = CType(e.Row.FindControl("lblStatusId"), Label)
            Dim lblPk As Label = CType(e.Row.FindControl("lblPk"), Label)
            Dim txtstatus As DropDownList = CType(e.Row.FindControl("txtstatus"), DropDownList)
            Dim txtname_th As TextBox = CType(e.Row.FindControl("txtname_th"), TextBox)
            Dim txtname_en As TextBox = CType(e.Row.FindControl("txtname_en"), TextBox)
            Dim lblDeptId As Label = CType(e.Row.FindControl("lblDeptId"), Label)

            Try
                txtstatus.SelectedValue = lblStatusId.Text

              
            Catch ex As Exception
                txtstatus.SelectedValue = 0
            End Try

            If lblDeptId.Text = Session("dept_id").ToString Then
                txtstatus.ForeColor = Drawing.Color.BlueViolet
                txtname_th.ForeColor = Drawing.Color.BlueViolet
                txtname_en.ForeColor = Drawing.Color.BlueViolet
            End If

            If txtstatus.SelectedValue <> "1" Then
                txtstatus.ForeColor = Drawing.Color.Red
                txtname_th.ForeColor = Drawing.Color.Red
                txtname_en.ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

    Protected Sub GridMasterTopic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMasterTopic.SelectedIndexChanged

    End Sub

    Protected Sub cmdSaveTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label

        Dim txtnameth As TextBox
        Dim txtnameen As TextBox
        Dim status As DropDownList
     

        i = GridMasterTopic.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridMasterTopic.Rows(s).FindControl("lblPK"), Label)

                txtnameth = CType(GridMasterTopic.Rows(s).FindControl("txtname_th"), TextBox)
                txtnameen = CType(GridMasterTopic.Rows(s).FindControl("txtname_en"), TextBox)
                status = CType(GridMasterTopic.Rows(s).FindControl("txtstatus"), DropDownList)
              

                sql = "UPDATE idp_m_master_topic "
                sql &= " SET master_topic_name_th = '" & addslashes(txtnameth.Text) & "' "
                sql &= " , master_topic_name_en = '" & addslashes(txtnameen.Text) & "' "
               
                sql &= " , is_active = '" & status.SelectedValue & "' "
                ' sql &= " , topic_last_update = GETDATE() "
                ' sql &= " , topic_create_by = '" & Session("user_fullname").ToString & "' "
                '  sql &= " , topic_last_update_ts = '" & Date.Now.Ticks & "' "

                sql &= " WHERE master_topic_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If

                sql = "UPDATE idp_m_topic SET topic_name_th = '" & addslashes(txtnameth.Text) & "' "
                sql &= " , topic_name_en = '" & addslashes(txtnameen.Text) & "' "
                sql &= " WHERE master_topic_id = " & lbl.Text

                'Response.Write(sql)
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If

            Next s

            conn.setDBCommit()
            bindMasterTopic()
            bindGridMasterTopic()

        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub txtadd_category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtadd_category.SelectedIndexChanged
        bindMasterTopic()
    End Sub

    Protected Sub cmdCCAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCCAdd.Click
        While lblCCAll.Items.Count > 0 AndAlso lblCCAll.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblCCAll.SelectedItem
            selectedItem.Selected = False
            lblCCSelect.Items.Add(selectedItem)
            lblCCAll.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub lblCCRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCCRemove.Click
        While lblCCSelect.Items.Count > 0 AndAlso lblCCSelect.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = lblCCSelect.SelectedItem
            selectedItem.Selected = False
            lblCCAll.Items.Add(selectedItem)
            lblCCSelect.Items.Remove(selectedItem)
        End While
    End Sub

    Protected Sub txtscope_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtscope.SelectedIndexChanged
        If txtscope.SelectedValue = "1" Then
            panel_dept.Visible = True
        Else
            panel_dept.Visible = False
        End If
    End Sub

   

    Protected Sub GridExpect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridExpect.SelectedIndexChanged

    End Sub

    Protected Sub cmdDeleteTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label

        Dim txtnameth As TextBox
        Dim txtnameen As TextBox
        Dim status As DropDownList
        Dim chk As CheckBox
        i = GridMasterTopic.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridMasterTopic.Rows(s).FindControl("lblPK"), Label)
                chk = CType(GridMasterTopic.Rows(s).FindControl("chk"), CheckBox)
                txtnameth = CType(GridMasterTopic.Rows(s).FindControl("txtname_th"), TextBox)
                txtnameen = CType(GridMasterTopic.Rows(s).FindControl("txtname_en"), TextBox)
                status = CType(GridMasterTopic.Rows(s).FindControl("txtstatus"), DropDownList)

                If chk.Checked = True Then
                    sql = "UPDATE idp_m_master_topic "
                    sql &= " SET "
                    sql &= " is_delete = 1 "
                    ' sql &= " , topic_last_update = GETDATE() "
                    ' sql &= " , topic_create_by = '" & Session("user_fullname").ToString & "' "
                    '  sql &= " , topic_last_update_ts = '" & Date.Now.Ticks & "' "

                    sql &= " WHERE master_topic_id = " & lbl.Text

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
               
            Next s

            conn.setDBCommit()
            bindMasterTopic()
            bindGridMasterTopic()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUpdateObject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdateObject.Click
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet

        Try
            sql = "UPDATE idp_message SET message_detail = '" & addslashes(txtmessage.Content) & "' "
            sql &= " WHERE idp_message_id = 1"
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If

          
            sql = "UPDATE idp_message SET message_detail = '" & addslashes(txtmessageIDP.Content) & "' "
            If chkDelay.Checked Then
                sql &= " , is_delay = 1 "
            Else
                sql &= " , is_delay = 0 "
            End If
            sql &= " WHERE idp_message_id = 2"
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If

            sql = "UPDATE idp_message SET message_detail = '" & addslashes(txtmessageExternal.Content) & "' "
            sql &= " WHERE idp_message_id = 3"
            '  Response.Write(sql)
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)

            End If
            ' Response.Write(sql)
            conn.setDBCommit()

            bindMessage()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdDelPicture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelPicture.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE idp_message SET picture_path = '' WHERE idp_message_id = 1 "
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            bindMessage()

            lblPicture.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet

        Try
            If Not IsNothing(FileUpload0.PostedFile) Then
                Dim strFileName = FileUpload0.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filename As String()

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)


                sql = "UPDATE idp_message SET picture_path = '" & strFileName & "' "
                sql &= " WHERE idp_message_id = 1"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUpload0.PostedFile.SaveAs(Server.MapPath("../share/idp/" & strFileName))

                conn.setDBCommit()

                bindMessage()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try

    End Sub

    Protected Sub cmdSaveMapTopic_Click(sender As Object, e As System.EventArgs) Handles cmdSaveMapTopic.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label

        Dim txtcommentth As TextBox
        Dim txtcommenten As TextBox
        Dim chk As CheckBox

        i = GridTopic.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridTopic.Rows(s).FindControl("lblPK"), Label)

                txtcommentth = CType(GridTopic.Rows(s).FindControl("txtcomment_th"), TextBox)
                txtcommenten = CType(GridTopic.Rows(s).FindControl("txtcomment_en"), TextBox)
                chk = CType(GridTopic.Rows(s).FindControl("chkUnique"), CheckBox)

                sql = "UPDATE idp_m_topic "
                sql &= " SET comment_th = '" & addslashes(txtcommentth.Text) & "' "
                sql &= " , comment_en = '" & addslashes(txtcommenten.Text) & "' "
                If chk.Checked = True Then
                    sql &= " , is_nursing_unique = 1 "
                Else
                    sql &= " , is_nursing_unique = 0 "
                End If
                ' sql &= " , topic_last_update = GETDATE() "
                ' sql &= " , topic_create_by = '" & Session("user_fullname").ToString & "' "
                '  sql &= " , topic_last_update_ts = '" & Date.Now.Ticks & "' "

                sql &= " WHERE topic_id = " & lbl.Text

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                    Exit For
                End If
            Next s

            conn.setDBCommit()
            bindMasterTopic()
            bindGridMasterTopic()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUploadIDP_Click(sender As Object, e As System.EventArgs) Handles cmdUploadIDP.Click
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet

        Try
            If Not IsNothing(FileUploadIDP.PostedFile) Then
                Dim strFileName = FileUploadIDP.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filename As String()

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)


                sql = "UPDATE idp_message SET picture_path = '" & strFileName & "' "
                sql &= " WHERE idp_message_id = 2"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUploadIDP.PostedFile.SaveAs(Server.MapPath("../share/idp/" & strFileName))

                conn.setDBCommit()

                bindMessage()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub cmdUploadExt_Click(sender As Object, e As System.EventArgs) Handles cmdUploadExt.Click
        Dim sql As String
        Dim errorMsg As String
        Dim ds As New DataSet

        Try
            If Not IsNothing(FileUploadExt.PostedFile) Then
                Dim strFileName = FileUploadExt.FileName
                Dim extension As String
                Dim iCount As Integer = 0
                Dim filename As String()

                If strFileName = "" Then
                    Return
                End If

                filename = strFileName.Split(".")
                iCount = UBound(filename)
                extension = filename(iCount)


                sql = "UPDATE idp_message SET picture_path = '" & strFileName & "' "
                sql &= " WHERE idp_message_id = 3"
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)

                End If

                FileUploadExt.PostedFile.SaveAs(Server.MapPath("../share/idp/" & strFileName))

                conn.setDBCommit()

                bindMessage()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            conn.setDBRollback()
        End Try
    End Sub

    Protected Sub cmdDelPictureIDP_Click(sender As Object, e As System.EventArgs) Handles cmdDelPictureIDP.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE idp_message SET picture_path = '' WHERE idp_message_id = 2 "
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            bindMessage()

            lblPictureIDP.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdDelPictureExt_Click(sender As Object, e As System.EventArgs) Handles cmdDelPictureExt.Click
        Dim sql As String
        Dim errorMsg As String

        Try
            sql = "UPDATE idp_message SET picture_path = '' WHERE idp_message_id = 3 "
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If
            conn.setDBCommit()

            bindMessage()

            lblPictureExt.Text = ""
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridCategory_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridCategory.SelectedIndexChanged

    End Sub

    Protected Sub cmdConfig_Click(sender As Object, e As EventArgs) Handles cmdConfig.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Try

            sql = "UPDATE idp_config SET idp_config_value = '" & txtconfig.SelectedValue & "' "
            sql &= "  WHERE idp_config_id = 1 "
            'Response.Write(sql & "<br/>")

            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg & sql)

            End If

            conn.setDBCommit()
            bindConfig()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindConfig()
        Dim ds As New DataSet
        Dim sql As String

        Try
            'sql = "SELECT * FROM user_costcenter "
            sql = "SELECT * FROM idp_config WHERE idp_config_id = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")

            txtconfig.SelectedValue = ds.Tables("t1").Rows(0)("idp_config_value").ToString

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdActiveCat_Click(sender As Object, e As EventArgs) Handles cmdActiveCat.Click
        Dim sql As String
        Dim errorMsg As String
        Dim i As Integer

        Dim lbl As Label
        Dim chk As CheckBox


        i = GridCategory.Rows.Count

        Try

            For s As Integer = 0 To i - 1

                lbl = CType(GridCategory.Rows(s).FindControl("lblPK0"), Label)
                chk = CType(GridCategory.Rows(s).FindControl("chk0"), CheckBox)

                If chk.Checked = True Then
                    'sql = "DELETE FROM idp_m_category WHERE category_id = " & lbl.Text
                    sql = "UPDATE idp_m_category SET is_delete = 0 WHERE category_id = " & lbl.Text
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                        Exit For
                    End If
                End If
            Next s

            conn.setDBCommit()

            bindGridCategory()
            bindCategory()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
