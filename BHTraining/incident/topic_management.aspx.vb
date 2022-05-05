Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Namespace incident
    Partial Class incident_topic_management
        Inherits System.Web.UI.Page
        Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Protected topic_type As String = "ir"
        Protected g As String = ""
        Protected t As String = ""
        Protected t1 As String = ""
        Protected t2 As String = ""
        Protected t3 As String = ""

        Protected mode As String = ""

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If conn.setConnection Then

            Else
                Response.Write("Connection Error")
            End If

            g = Request.QueryString("g")
            t = Request.QueryString("t")
            t1 = Request.QueryString("t1")
            t2 = Request.QueryString("t2")
            t3 = Request.QueryString("t3")
            mode = Request.QueryString("mode")
            topic_type = Request.QueryString("topic_type")

            If topic_type = "" Then
                topic_type = "ir"
            End If

            If Not Page.IsPostBack Then
                If mode = "g" Then
                    Panel1.Visible = True
                    bindSelectGrandTopic(g, txtaddgrand)
                End If

                If mode = "t" Then
                    Panel2.Visible = True
                    bindGrandTopic()
                    txtComboGrandTopic.SelectedValue = g
                    bindSelectTopic(t, txtAddTopic)
                    cmdDelTopic.Visible = True
                Else
                    cmdDelTopic.Visible = False
                End If

                If mode = "t1" Then
                    Panel3.Visible = True
                    bindGrandTopic()
                    txtComboGrandForSubTopic.SelectedValue = g
                    bindTopic(g)
                    txtComboTopic.SelectedValue = t
                    bindSelectSubTopic(t1, txtAddSubTopic)
                    cmdDelSubTopic1.Visible = True
                Else
                    cmdDelSubTopic1.Visible = False
                End If

                If mode = "t2" Then
                    Panel4.Visible = True
                    bindGrandTopic()
                    txtComboGrandForSubTopic2.SelectedValue = g
                    bindTopic(g)
                    txtComboTopicForTopic2.SelectedValue = t
                    bindSubTopic1(t)
                    txtComboSubTopic1ForSubTopic2.SelectedValue = t1
                    bindSelectSubTopic2(t2, txtAddSubTopic2)
                    cmdDelSubTopic2.Visible = True
                Else
                    cmdDelSubTopic2.Visible = False
                End If

                If mode = "t3" Then
                    Panel5.Visible = True
                    bindGrandTopic()
                    txtComboGrandForSubTopic3.SelectedValue = g
                    bindTopic(g)
                    txtComboTopicForTopic3.SelectedValue = t
                    bindSubTopic1(t)
                    txtComboSubTopic1ForSubTopic3.SelectedValue = t1
                    bindSubTopic2(t1)
                    txtComboSubTopic2ForSubTopic3.SelectedValue = t2
                    bindSelectSubTopic3(t3, txtAddSubTopic3)
                    cmdDelSubTopic3.Visible = True
                Else
                    cmdDelSubTopic3.Visible = False
                End If

                bindGrid()
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


        Sub bindSelectGrandTopic(ByVal topic_id As String, ByVal txt As TextBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic_grand WHERE grand_topic_id = " & topic_id
                ds = conn.getDataSet(sql, "t1")
                txt.Text = ds.Tables(0).Rows(0)("grand_topic_name").ToString
            Catch ex As Exception
                Response.Write("bindSelectGrandTopic " & ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindSelectTopic(ByVal topic_id As String, ByVal txt As TextBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic WHERE ir_topic_id = " & topic_id
                ds = conn.getDataSet(sql, "t1")
                txt.Text = ds.Tables(0).Rows(0)("topic_name").ToString
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub


        Sub bindSelectSubTopic(ByVal topic_id As String, ByVal txt As TextBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic_sub WHERE ir_subtopic_id = " & topic_id
                ds = conn.getDataSet(sql, "t1")
                txt.Text = ds.Tables(0).Rows(0)("subtopic_name").ToString
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindSelectSubTopic2(ByVal topic_id As String, ByVal txt As TextBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic_sub2 WHERE ir_subtopic2_id = " & topic_id
                ds = conn.getDataSet(sql, "t1")
                txt.Text = ds.Tables(0).Rows(0)("subtopic2_name_en").ToString
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindSelectSubTopic3(ByVal topic_id As String, ByVal txt As TextBox)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic_sub3 WHERE ir_subtopic3_id = " & topic_id
                ds = conn.getDataSet(sql, "t1")
                txt.Text = ds.Tables(0).Rows(0)("subtopic3_name_en").ToString
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindGrid()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic_grand WHERE topic_type = '" & topic_type & "'"
                ds = conn.getDataSet(sql, "t1")

                GridView1.DataSource = ds
                GridView1.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Sub bindGrandTopic()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic_grand WHERE topic_type = '" & topic_type & "'"
                ds = conn.getDataSet(sql, "t1")

                txtComboGrandTopic.DataSource = ds
                txtComboGrandTopic.DataBind()

                txtComboGrandForSubTopic.DataSource = ds
                txtComboGrandForSubTopic.DataBind()

                txtComboGrandForSubTopic2.DataSource = ds
                txtComboGrandForSubTopic2.DataBind()

                txtComboGrandForSubTopic3.DataSource = ds
                txtComboGrandForSubTopic3.DataBind()

                If txtComboGrandTopic.Items.Count > 0 Then
                    ' bindTopic("")
                End If

                txtComboGrandTopic.Items.Insert(0, New ListItem("--Please Select --", ""))
                txtComboGrandForSubTopic.Items.Insert(0, New ListItem("--Please Select --", ""))
                txtComboGrandForSubTopic2.Items.Insert(0, New ListItem("--Please Select --", ""))
                txtComboGrandForSubTopic3.Items.Insert(0, New ListItem("--Please Select --", ""))

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                'ds = Nothing
            End Try
        End Sub

        Sub bindTopic(ByVal tid As String)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic WHERE 1 = 1 "
                If tid <> "" Then
                    sql &= " AND grand_topic_id = " & tid
                Else
                    sql &= " AND 1 > 2"
                End If


                ds = conn.getDataSet(sql, "t1")

                If ds.Tables(0).Rows.Count > 0 Then
                    ' Response.Write(sql)
                    txtComboTopic.DataSource = ds
                    txtComboTopic.DataBind()

                    txtComboTopicForTopic2.DataSource = ds
                    txtComboTopicForTopic2.DataBind()

                    txtComboTopicForTopic3.DataSource = ds
                    txtComboTopicForTopic3.DataBind()


                    ' Throw New Exception("Xxxxx")
                Else
                    ' Throw New Exception(sql)
                    '  Response.Write("No Data")
                    txtComboTopic.Items.Clear()
                    txtComboTopicForTopic2.Items.Clear()
                    txtComboTopicForTopic3.Items.Clear()
          
                End If
                ' Response.Write(12333333)
                txtComboTopic.Items.Insert(0, New ListItem("--Please Select --", ""))
                txtComboTopicForTopic2.Items.Insert(0, New ListItem("--Please Select --", ""))
                txtComboTopicForTopic3.Items.Insert(0, New ListItem("--Please Select --", ""))
                'Response.Write("jjj")
                ' bindSubTopic1("")
                '
            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
                'ds = Nothing
            End Try
        End Sub

        Sub bindSubTopic1(ByVal tid As String)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic_sub WHERE 1 = 1 "
                If tid <> "" Then
                    sql &= " AND ir_topic_id = " & tid
                Else
                    sql &= " AND 1 > 2 "
                End If
                'Response.Write(sql)
                ds = conn.getDataSet(sql, "t1")

                If ds.Tables(0).Rows.Count > 0 Then
                    txtComboSubTopic1ForSubTopic2.DataSource = ds
                    txtComboSubTopic1ForSubTopic2.DataBind()

                    txtComboSubTopic1ForSubTopic3.DataSource = ds
                    txtComboSubTopic1ForSubTopic3.DataBind()
                Else
                    txtComboSubTopic1ForSubTopic2.Items.Clear()
                    txtComboSubTopic1ForSubTopic3.Items.Clear()
                End If

                txtComboSubTopic1ForSubTopic2.Items.Insert(0, New ListItem("--Please Select --", ""))
                txtComboSubTopic1ForSubTopic3.Items.Insert(0, New ListItem("--Please Select --", ""))
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                'ds = Nothing
            End Try
        End Sub

        Sub bindSubTopic2(ByVal tid As String)
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_topic_sub2 WHERE 1 = 1 "
                If tid <> "" Then
                    sql &= " AND ir_subtopic_id = " & tid
                Else
                    sql &= " AND 1 > 2 "
                End If

                ds = conn.getDataSet(sql, "t1")

                If ds.Tables(0).Rows.Count > 0 Then
                    txtComboSubTopic2ForSubTopic3.DataSource = ds
                    txtComboSubTopic2ForSubTopic3.DataBind()

                   
                Else
                    txtComboSubTopic2ForSubTopic3.Items.Clear()

                End If

                txtComboSubTopic2ForSubTopic3.Items.Insert(0, New ListItem("--Please Select --", ""))

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                'ds = Nothing
            End Try
        End Sub

        Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
            GridView1.PageIndex = e.NewPageIndex
            bindGrid()
        End Sub


        Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)
                Dim lblTopic As Label = CType(e.Row.FindControl("lblTopic"), Label)
                Dim sql As String
                Dim ds As New DataSet
                Dim ds2 As New DataSet
                Dim ds3 As New DataSet
                Dim ds4 As New DataSet
                Dim strB As New StringBuilder

                Dim g As String = ""
                Dim t As String = ""
                Dim t1 As String = ""
                Dim t2 As String = ""
                Dim t3 As String = ""

                Try

                    sql = "SELECT * FROM ir_topic WHERE grand_topic_id = " & lblPK.Text
                    g = lblPK.Text
                    ds = conn.getDataSet(sql, "t1")
                    strB.Append("<table width='100%'>")
                    For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                        strB.Append("<tr><td>")
                        t = ds.Tables("t1").Rows(i)("ir_topic_id").ToString
                        strB.Append(" " & (e.Row.RowIndex + 1) & "." & (i + 1) & ". <a href='topic_management.aspx?topic_type=" & topic_type & "&g=" & g & "&t=" & t & "&mode=t'>" & ds.Tables("t1").Rows(i)("topic_name").ToString & "</a>")

                        sql = "SELECT * FROM ir_topic_sub WHERE ir_topic_id = " & ds.Tables("t1").Rows(i)("ir_topic_id").ToString
                        ds2 = conn.getDataSet(sql, "tSub")
                        If ds2.Tables("tSub").Rows.Count > 0 Then
                            strB.Append("<table width='100%'>")
                            For ii As Integer = 0 To ds2.Tables("tSub").Rows.Count - 1
                                strB.Append("<tr><td>")
                                strB.Append("&nbsp;&nbsp; " & (e.Row.RowIndex + 1) & "." & (i + 1) & "." & (ii + 1) & " Subtopic1 ")

                                t1 = ds2.Tables("tSub").Rows(ii)("ir_subtopic_id").ToString
                                strB.Append(" : <a href='topic_management.aspx?topic_type=" & topic_type & "&g=" & g & "&t=" & t & "&t1=" & t1 & "&mode=t1' >" & ds2.Tables("tSub").Rows(ii)("subtopic_name").ToString & "</a>")
                                ' Response.Write(12)

                                sql = "SELECT * FROM ir_topic_sub2 WHERE ir_subtopic_id = " & ds2.Tables("tSub").Rows(ii)("ir_subtopic_id").ToString
                                ds3 = conn.getDataSet(sql, "tSub2")
                                '  Response.Write(sql)
                                'Response.Write(ds3.Tables("tSub2").Rows.Count)
                                For iSub2 As Integer = 0 To ds3.Tables(0).Rows.Count - 1
                                    t2 = ds3.Tables("tSub2").Rows(iSub2)("ir_subtopic2_id").ToString

                                    strB.Append("<br/>&nbsp;&nbsp;&nbsp;&nbsp; " & (e.Row.RowIndex + 1) & "." & (i + 1) & "." & (ii + 1) & "." & (iSub2 + 1) & " Subtopic2 ")
                                    strB.Append(" : <a href='topic_management.aspx?topic_type=" & topic_type & "&g=" & g & "&t=" & t & "&t1=" & t1 & "&t2=" & t2 & "&mode=t2' >" & ds3.Tables("tSub2").Rows(iSub2)("subtopic2_name_en").ToString & "</a>")

                                    sql = "SELECT * FROM ir_topic_sub3 WHERE ir_subtopic2_id = " & ds3.Tables("tSub2").Rows(iSub2)("ir_subtopic2_id").ToString
                                    ds4 = conn.getDataSet(sql, "tSub3")
                                    'Response.Write(sql)
                                    For iSub3 As Integer = 0 To ds4.Tables(0).Rows.Count - 1
                                        t3 = ds4.Tables("tSub3").Rows(iSub3)("ir_subtopic3_id").ToString
                                        strB.Append("<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & (e.Row.RowIndex + 1) & "." & (i + 1) & "." & (ii + 1) & "." & (iSub2 + 1) & "." & (iSub3 + 1) & " Subtopic3 ")
                                        strB.Append(" : <a href='topic_management.aspx?topic_type=" & topic_type & "&g=" & g & "&t=" & t & "&t1=" & t1 & "&t2=" & t2 & "&t3=" & t3 & "&mode=t3' >" & ds4.Tables("tSub3").Rows(iSub3)("subtopic3_name_en").ToString & "</a>")
                                    Next iSub3

                                Next iSub2


                                strB.Append("</td></tr>")
                            Next ii
                            strB.Append("</table>")
                        End If

                        strB.Append("</td></tr>")
                    Next i
                    strB.Append("</table>")

                    lblTopic.Text = strB.ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    ds.Dispose()
                    ds = Nothing
                    ds2.Dispose()
                    ds2 = Nothing
                    ds3.Dispose()
                    ds4.Dispose()
                End Try

            End If
        End Sub

     
        Protected Sub cmdSaveGrandTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveGrandTopic.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String

            Try
                If mode = "g" Then
                    sql = "UPDATE ir_topic_grand SET grand_topic_name = '" & addslashes(txtaddgrand.Text) & "'"
                    sql &= " WHERE grand_topic_id = " & g
                Else
                    pk = getPK("grand_topic_id", "ir_topic_grand", conn)

                    sql = "INSERT INTO ir_topic_grand (grand_topic_id , grand_topic_name , topic_type) VALUES("
                    sql &= "" & pk & " ,"
                    sql &= "'" & addslashes(txtaddgrand.Text) & "' ,"
                    sql &= "'" & addslashes(topic_type) & "' "
                    sql &= ")"
                End If
               
                ' Response.Write(sql)
                errorMsg = conn.executeSQL(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                txtaddgrand.Text = ""
                Panel1.Visible = False
                bindGrid()

                bindGrandTopic()
                lblError.Text = ""
                txtaddgrand.Text = ""
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End Sub

        Protected Sub cmdGrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGrand.Click
            Panel1.Visible = True
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            g = ""
        End Sub

        Protected Sub cmdCancel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel1.Click
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

        Protected Sub cmdTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTopic.Click
            bindGrandTopic()
            bindTopic("")
            bindSubTopic1("")
            bindSubTopic2("")

            Panel1.Visible = False
            Panel2.Visible = True
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False

            cmdDelTopic.Visible = False
            t = ""
        End Sub


        Protected Sub cmdSubTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSubTopic.Click
            bindGrandTopic()
            bindTopic("")
            bindSubTopic1("")
            bindSubTopic2("")

            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = True
            Panel4.Visible = False
            Panel5.Visible = False
            cmdDelSubTopic1.Visible = False
            t1 = ""
        End Sub

        Protected Sub cmdSubTopic2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSubTopic2.Click
            bindGrandTopic()
            bindTopic("")
            bindSubTopic1("")
            bindSubTopic2("")

            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = True
            Panel5.Visible = False
            cmdDelSubTopic2.Visible = False
            t2 = ""
        End Sub

        Protected Sub cmdSubTopic3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSubTopic3.Click
            bindGrandTopic()
            bindTopic("")
            bindSubTopic1("")
            bindSubTopic2("")

            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = True
            cmdDelSubTopic3.Visible = False
            t3 = ""
        End Sub

        Protected Sub cmdCancelTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelTopic.Click
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

        Protected Sub cmdCancelSubTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelSubTopic.Click
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

  

        Protected Sub cmdAddTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddTopic.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String

            Try
                If mode = "t" Then
                    sql = "UPDATE ir_topic SET grand_topic_id = " & addslashes(txtComboGrandTopic.SelectedValue)
                    sql &= " , topic_name = '" & addslashes(txtAddTopic.Text) & "'"
                    sql &= " WHERE ir_topic_id = " & t

                Else
                    pk = getPK("ir_topic_id", "ir_topic", conn)

                    sql = "INSERT INTO ir_topic (ir_topic_id , topic_name , grand_topic_id) VALUES("
                    sql &= "" & pk & " ,"
                    sql &= "'" & addslashes(txtAddTopic.Text) & "' ,"
                    sql &= "'" & addslashes(txtComboGrandTopic.SelectedValue) & "' "
                    sql &= ")"


                End If
                'Response.Write(sql)
                errorMsg = conn.executeSQL(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                txtaddgrand.Text = ""
                Panel2.Visible = False
                bindGrid()
                bindTopic(txtComboGrandTopic.SelectedValue)
                lblError.Text = ""
                txtAddTopic.Text = ""
            Catch ex As Exception
                lblError.Text = ex.Message
                Return
            End Try

            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

        Protected Sub cmdAddSubTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddSubTopic.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String

            Try
                If mode = "t1" Then
                    sql = "UPDATE ir_topic_sub SET ir_topic_id = " & addslashes(txtComboTopic.SelectedValue)
                    sql &= " , subtopic_name = '" & addslashes(txtAddSubTopic.Text) & "'"
                    sql &= " WHERE ir_subtopic_id = " & t1
                Else
                    pk = getPK("ir_subtopic_id", "ir_topic_sub", conn)

                    sql = "INSERT INTO ir_topic_sub (ir_subtopic_id , subtopic_name , ir_topic_id ) VALUES("
                    sql &= "" & pk & " ,"
                    sql &= "'" & addslashes(txtAddSubTopic.Text) & "' ,"
                    sql &= "'" & addslashes(txtComboTopic.SelectedValue) & "' "

                    sql &= ")"
                End If

                

                errorMsg = conn.executeSQL(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                txtaddgrand.Text = ""
                Panel3.Visible = False
                bindGrid()
                ' bindTopic()
                lblError.Text = ""
                txtAddSubTopic.Text = ""
            Catch ex As Exception
                lblError.Text = ex.Message
                Return
            End Try

            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

        Protected Sub cmdAddSubTopic2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddSubTopic2.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String

            Try

                If mode = "t2" Then
                    sql = "UPDATE ir_topic_sub2 SET ir_subtopic_id = " & addslashes(txtComboSubTopic1ForSubTopic2.SelectedValue)
                    sql &= " , subtopic2_name_en = '" & addslashes(txtAddSubTopic2.Text) & "'"
                    sql &= " , subtopic2_name_th = '" & addslashes(txtAddSubTopic2.Text) & "'"
                    sql &= " WHERE ir_subtopic2_id = " & t2
                Else
                    pk = getPK("ir_subtopic2_id", "ir_topic_sub2", conn)

                    sql = "INSERT INTO ir_topic_sub2 (ir_subtopic2_id , subtopic2_name_en , subtopic2_name_th , ir_subtopic_id ) VALUES("
                    sql &= "" & pk & " ,"
                    sql &= "'" & addslashes(txtAddSubTopic2.Text) & "' ,"
                    sql &= "'" & addslashes(txtAddSubTopic2.Text) & "' ,"
                    sql &= "'" & addslashes(txtComboSubTopic1ForSubTopic2.SelectedValue) & "' "

                    sql &= ")"
                End If
              

                errorMsg = conn.executeSQL(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                txtaddgrand.Text = ""
                Panel4.Visible = False
                bindGrid()
                ' bindTopic()
                lblError.Text = ""
                txtAddSubTopic2.Text = ""
            Catch ex As Exception
                lblError.Text = ex.Message
                Return
            End Try

            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

        Protected Sub cmdAddSubTopic3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddSubTopic3.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String

            Try
                If mode = "t3" Then
                    sql = "UPDATE ir_topic_sub3 SET ir_subtopic2_id = " & addslashes(txtComboSubTopic2ForSubTopic3.SelectedValue)
                    sql &= " , subtopic3_name_en = '" & addslashes(txtAddSubTopic3.Text) & "'"
                    sql &= " , subtopic3_name_th = '" & addslashes(txtAddSubTopic3.Text) & "'"
                    sql &= " WHERE ir_subtopic3_id = " & t3
                Else
                    pk = getPK("ir_subtopic3_id", "ir_topic_sub3", conn)

                    sql = "INSERT INTO ir_topic_sub3 (ir_subtopic3_id , subtopic3_name_en , subtopic3_name_th , ir_subtopic2_id ) VALUES("
                    sql &= "" & pk & " ,"
                    sql &= "'" & addslashes(txtAddSubTopic3.Text) & "' ,"
                    sql &= "'" & addslashes(txtAddSubTopic3.Text) & "' ,"
                    sql &= "'" & addslashes(txtComboSubTopic2ForSubTopic3.SelectedValue) & "' "

                    sql &= ")"
                End If
              

                errorMsg = conn.executeSQL(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                txtaddgrand.Text = ""
                Panel5.Visible = False
                bindGrid()
                ' bindTopic()
                lblError.Text = ""
                txtAddSubTopic3.Text = ""
            Catch ex As Exception
                lblError.Text = ex.Message
                Return
            End Try

            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

        Protected Sub txtComboGrandForSubTopic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComboGrandForSubTopic.SelectedIndexChanged
            bindTopic(txtComboGrandForSubTopic.SelectedValue)
        End Sub

        Protected Sub txtComboGrandForSubTopic2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComboGrandForSubTopic2.SelectedIndexChanged
            bindTopic(txtComboGrandForSubTopic2.SelectedValue)
        End Sub

        Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM ir_topic_grand WHERE grand_topic_id = " & GridView1.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQL(sql)

                If result <> "" Then
                    'Response.Write(result)
                    Throw New Exception(result)
                End If

                bindGrid()
                bindGrandTopic()
                bindTopic("")
                bindSubTopic1("")
                bindSubTopic2("")
            Catch ex As Exception

                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        End Sub

       
        Protected Sub txtComboTopicForTopic2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComboTopicForTopic2.SelectedIndexChanged
            bindSubTopic1(txtComboTopicForTopic2.SelectedValue)
        End Sub

      
        Protected Sub txtComboGrandForSubTopic3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComboGrandForSubTopic3.SelectedIndexChanged
            bindTopic(txtComboGrandForSubTopic3.SelectedValue)
        End Sub

        Protected Sub txtComboTopicForTopic3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComboTopicForTopic3.SelectedIndexChanged
            bindSubTopic1(txtComboTopicForTopic3.SelectedValue)
        End Sub

        Protected Sub txtComboSubTopic1ForSubTopic3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComboSubTopic1ForSubTopic3.SelectedIndexChanged
            bindSubTopic2(txtComboSubTopic1ForSubTopic3.SelectedValue)
        End Sub

      
        Protected Sub cmdCancel5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel5.Click
            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
        End Sub

        Protected Sub cmdCancel4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel4.Click
            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
        End Sub


        Sub deleteTopic(ByVal table As String, ByVal field As String, ByVal tid As String, ByVal p As Panel)
            Dim sql As String
            Dim errorMsg As String

            Try
                sql = "DELETE FROM " & table & " WHERE " & field & " = " & tid
                errorMsg = conn.executeSQL(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                p.Visible = False
                bindGrid()
            Catch ex As Exception
                Response.Write(ex.Message)
                Response.End()
            End Try

        End Sub

        Protected Sub cmdDelSubTopic3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelSubTopic3.Click
            deleteTopic("ir_topic_sub3", "ir_subtopic3_id", t3, Panel5)
            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

        Protected Sub cmdDelSubTopic2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelSubTopic2.Click
            deleteTopic("ir_topic_sub2", "ir_subtopic2_id", t2, Panel4)
            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

        Protected Sub cmdDelSubTopic1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelSubTopic1.Click
            deleteTopic("ir_topic_sub", "ir_subtopic_id", t1, Panel3)
            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub

        Protected Sub cmdDelTopic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelTopic.Click
            deleteTopic("ir_topic", "ir_topic_id", t, Panel2)
            Response.Redirect("topic_management.aspx?topic_type=" & topic_type)
        End Sub
    End Class
End Namespace
