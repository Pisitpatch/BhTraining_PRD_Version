Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class star_star_message
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected priv_list() As String
    Protected clip_name As String = ""
    Protected global_unit_num As String = ""
    Protected global_unit_stack1 As String = ""
    Protected global_unit_stack_xaxis As String = ""
    Protected global_dept_num As String = ""
    Protected global_dept_name As String = ""

    Protected global_clear As String = ""
    Protected global_care As String = ""
    Protected global_smart As String = ""
    Protected global_quality As String = ""

    Protected global_b1, global_b2, global_b3, global_b4, global_b5, global_b6, global_b7, global_b8, global_b9, global_b10 As String
    Protected data4_1 As String = ""
    Protected data4_name As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("session_myid")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        priv_list = Session("priv_list")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
        Response.Cache.SetNoStore()

        If Page.IsPostBack Then

        Else
            bindDateRange()
            txtbiMonth.SelectedIndex = txtbiMonth.Items.Count - 1
            txtbiMonthDept.SelectedIndex = txtbiMonthDept.Items.Count - 1

        End If

        bindGrid()
        bindMessage("1", lblInnovation)
        ' bindStackGraph()
        bindGridSubmitStar()
        '  bindGridAwardStar()
        pineGraph()

        bindBIWayPie1()
        'bindBIWayStack()
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing

        Catch ex As Exception
            '  Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub


    Sub bindMessage(ByVal pk As String, ByVal lb As Label)
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_message WHERE star_message_id = " & pk
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lb.Text = "<img src='../share/star/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' /><br/><br/>"
            End If
            lb.Text &= ds.Tables("t1").Rows(0)("message_detail").ToString

            clip_name = ds.Tables("t1").Rows(0)("clip_path").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM star_news WHERE ISNULL(is_delete,0) = 0 AND is_active = 1  ORDER BY new_date_ts DESC"
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridNews.DataSource = ds
            GridNews.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindDateRange()
        Dim sql As String
        Dim ds As New DataSet
        Dim myvalue As String
        Dim mytext As String
        Try
            sql = "SELECT  MONTH(submit_date) AS m , YEAR(submit_date) AS y FROM star_trans_list WHERE ISNULL(is_delete,0) = 0 AND ISNULL(is_cancel,0) = 0 AND ISNULL(submit_date_ts,0) > 0 "
            sql &= " GROUP BY MONTH(submit_date) , YEAR(submit_date)"
            sql &= " ORDER BY YEAR(submit_date) ASC , MONTH(submit_date) "
            ds = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                myvalue = ds.Tables("t1").Rows(i)("m").ToString & "|" & ds.Tables("t1").Rows(i)("y").ToString
                mytext = getMonthName(CInt(ds.Tables("t1").Rows(i)("m").ToString)) & " " & ds.Tables("t1").Rows(i)("y").ToString
                txtbiMonth.Items.Add(New ListItem(mytext, myvalue))
                txtbiMonthDept.Items.Add(New ListItem(mytext, myvalue))
            Next i

            txtbiMonth.SelectedIndex = ds.Tables("t1").Rows.Count - 1

        Catch ex As Exception
            ' Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindBIWayPie1()
        Dim sql As String
        Dim ds As New DataSet
        Dim start_month As Array

        start_month = txtbiMonth.SelectedValue.Split("|")


        Dim param(10) As Integer
        Dim login_date As Date
        Dim query_date As Date


        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)
        Try
            For ii As Integer = 0 To 9

                sql = "select ISNULL(SUM(chk_newbi" & (ii + 1) & "),0) from ("
                sql &= " select sum(case when chk_newbi" & (ii + 1) & " = 1 then 1 else 0 end) as chk_newbi" & (ii + 1)
                sql &= " from star_hr_tab a INNER JOIN star_trans_list b ON a.star_id = b.star_id "
                sql &= " where 1 = 1"
                sql &= " and MONTH(b.submit_date) =" & start_month(0) & " AND YEAR(b.submit_date) = " & start_month(1)
                sql &= " ) a1"

                ds = conn.getDataSetForTransaction(sql, "tCare")
                If ds.Tables("tCare").Rows.Count > 0 Then
                    'bi1 = CInt(ds.Tables("tCare").Rows(0)(0).ToString)
                    param(ii) = CInt(ds.Tables("tCare").Rows(0)(0).ToString)
                End If

            Next

            global_unit_num = ""
            'global_unit_name = ""
            Dim limit As String = ""
            For ii As Integer = 0 To 9
                If param(ii) > 0 Then
                    If global_unit_num = "" Then
                        limit = ""
                    Else
                        limit = ","
                    End If
                    global_unit_num &= limit & "['B" & (ii + 1) & "' , " & param(ii) & " ]"
                End If
            Next


            'sql = "  select SUM(case when chk_clear = 1 then 1 else 0 end) AS num_clear"
            'sql &= " ,SUM(case when chk_care = 1 then 1 else 0 end) AS num_care"
            'sql &= " ,SUM(case when chk_smart = 1 then 1 else 0 end) AS num_smart"
            'sql &= " ,SUM(case when chk_quality = 1 then 1 else 0 end) AS num_quality"
            'sql &= " from star_trans_list a "
            'sql &= " inner join star_detail_tab b on a.star_id = b.star_id"
            'sql &= " inner join star_hr_tab c on a.star_id = c.star_id"
            'sql &= " inner join star_status_list d on a.status_id = d.status_id"
            'sql &= " where(a.star_id > 1 And isnull(a.is_cancel, 0) = 0 And isnull(a.is_delete, 0) = 0)"

            'sql &= " AND MONTH(submit_date) =" & start_month(0) & " AND YEAR(submit_date) = " & start_month(1)

            'ds = conn.getDataSetForTransaction(sql, "t1")

            'Dim limit = ""
            'global_unit_num = ""
            ''global_unit_name = ""

            'global_unit_num &= "['Clear' , " & CInt(ds.Tables("t1").Rows(0)("num_clear").ToString) & " ]"
            'global_unit_num &= ",['Care' , " & CInt(ds.Tables("t1").Rows(0)("num_care").ToString) & " ]"
            'global_unit_num &= ",['Smart' , " & CInt(ds.Tables("t1").Rows(0)("num_smart").ToString) & " ]"
            'global_unit_num &= ",['Quality' , " & CInt(ds.Tables("t1").Rows(0)("num_quality").ToString) & " ]"


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub bindBIWayStack()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "  select TOP 5 DATENAME(MONTH , a.submit_date) AS monthname , YEAR(a.submit_date) AS yearname"
            sql &= " ,SUM(case when chk_clear = 1 then 1 else 0 end) AS num_clear"
            sql &= " ,SUM(case when chk_care = 1 then 1 else 0 end) AS num_care"
            sql &= " ,SUM(case when chk_smart = 1 then 1 else 0 end) AS num_smart"
            sql &= " ,SUM(case when chk_quality = 1 then 1 else 0 end) AS num_quality"
            sql &= " from star_trans_list a "
            sql &= " inner join star_detail_tab b on a.star_id = b.star_id"
            sql &= " inner join star_hr_tab c on a.star_id = c.star_id"
            sql &= " inner join star_status_list d on a.status_id = d.status_id"
            sql &= " where(a.star_id > 1 And isnull(a.is_cancel, 0) = 0 And isnull(a.is_delete, 0) = 0)"
            sql &= " group by  DATENAME(MONTH , a.submit_date) , YEAR(a.submit_date) , MONTH(a.submit_date) "
            sql &= " order by  YEAR(a.submit_date) DESC ,  MONTH(a.submit_date) DESC"

            'Response.Write(sql)
            ds = conn.getDataSetForTransaction(sql, "t1")

            Dim limit = ""

            For i As Integer = ds.Tables("t1").Rows.Count - 1 To 0 Step -1
                If i = (ds.Tables("t1").Rows.Count - 1) Then
                    limit = ""
                Else
                    limit = ","
                End If
                global_unit_stack_xaxis &= limit & "'" & ds.Tables("t1").Rows(i)("monthname").ToString & ds.Tables("t1").Rows(i)("yearname").ToString & "'"
            Next i

            global_unit_stack1 &= "["
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If

                global_unit_stack1 &= ds.Tables("t1").Rows(i)("num_clear").ToString & ","


            Next i
            global_unit_stack1 &= "]"

            global_unit_stack1 &= ",["
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If


                global_unit_stack1 &= limit & ds.Tables("t1").Rows(i)("num_care").ToString


            Next i
            global_unit_stack1 &= "]"

            global_unit_stack1 &= ", ["
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If


                global_unit_stack1 &= limit & ds.Tables("t1").Rows(i)("num_smart").ToString


            Next i
            global_unit_stack1 &= "]"

            global_unit_stack1 &= ", ["
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If


                global_unit_stack1 &= limit & ds.Tables("t1").Rows(i)("num_quality").ToString


            Next i
            global_unit_stack1 &= "]"

        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGridAwardStar()
        Dim sql As String
        Dim ds As New DataSet

        Dim login_date As Date

        Dim query_date As Date

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)

        Try
           
            sql = "SELECT TOP 20 COUNT(*) AS num "
            sql &= " , s2.user_fullname_local , s2.dept_name AS costcenter_name FROM star_relate_person s1 "
            sql &= " INNER JOIN user_profile s2 ON s1.emp_code = s2.emp_code "
            sql &= " INNER JOIN star_trans_list s3 ON s1.star_id = s3.star_id "
            sql &= " WHERE(ISNULL(s3.is_delete, 0) = 0 And ISNULL(s3.is_cancel, 0) = 0 And s3.status_id = 7)"
            sql &= " GROUP BY s2.user_fullname_local , s2.dept_name "
            sql &= " ORDER BY COUNT(*) DESC"
            Response.Write(sql)
            '   Return
            ds = conn.getDataSetForTransaction(sql, "t1")

        
            '  GripStarTopAward.DataSource = ds
            '  GripStarTopAward.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try

    End Sub

    Sub bindGridSubmitStar()
        Dim sql As String
        Dim ds As New DataSet

        Dim login_date As Date
        Dim query_date As Date
        Dim start_month As Array

        start_month = txtbiMonthDept.SelectedValue.Split("|")

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)

        Try
            'sql = "SELECT TOP 20 report_dept_name , COUNT(report_dept_id) AS num FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id "
            'sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id > 1 "
            ''sql &= " AND submit_date BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & 1 & "' AND "
            ''sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            'sql &= " GROUP BY report_dept_name , report_dept_id"
            'sql &= " ORDER BY COUNT(report_dept_id) DESC"
            '  Response.Write(sql)
            '   Return
            sql = "SELECT TOP 20  e.dept_name AS report_dept_name , COUNT(e.dept_name) AS num  "
            sql &= " , SUM(CASE WHEN chk_newbi1 = 1 THEN 1 ELSE 0 END) AS chk_newbi1 "
            sql &= " , SUM(CASE WHEN chk_newbi2 = 1 THEN 1 ELSE 0 END) AS chk_newbi2 "
            sql &= " , SUM(CASE WHEN chk_newbi3 = 1 THEN 1 ELSE 0 END) AS chk_newbi3 "
            sql &= " , SUM(CASE WHEN chk_newbi4 = 1 THEN 1 ELSE 0 END) AS chk_newbi4 "
            sql &= " , SUM(CASE WHEN chk_newbi5 = 1 THEN 1 ELSE 0 END) AS chk_newbi5 "
            sql &= " , SUM(CASE WHEN chk_newbi6 = 1 THEN 1 ELSE 0 END) AS chk_newbi6 "
            sql &= " , SUM(CASE WHEN chk_newbi7 = 1 THEN 1 ELSE 0 END) AS chk_newbi7 "
            sql &= " , SUM(CASE WHEN chk_newbi8 = 1 THEN 1 ELSE 0 END) AS chk_newbi8 "
            sql &= " , SUM(CASE WHEN chk_newbi9 = 1 THEN 1 ELSE 0 END) AS chk_newbi9 "
            sql &= " , SUM(CASE WHEN chk_newbi10 = 1 THEN 1 ELSE 0 END) AS chk_newbi10 "
            sql &= " FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id "
            sql &= " INNER JOIN star_hr_tab c ON a.star_id = c.star_id "
            sql &= " INNER JOIN star_relate_person d ON a.star_id = d.star_id "
            sql &= " INNER JOIN user_profile e ON d.emp_code = e.emp_code "
            sql &= " WHERE ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND a.status_id = 7 "
            sql &= " and MONTH(submit_date) =" & start_month(0) & " AND YEAR(submit_date) = " & start_month(1)
            sql &= " GROUP BY  e.dept_name , e.dept_id"
            sql &= " ORDER BY COUNT(e.dept_name) DESC"

            ds = conn.getDataSetForTransaction(sql, "t1")

            GripStarTopSubmit.DataSource = ds
            GripStarTopSubmit.DataBind()


            Dim limit As String = ""
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 5 Then
                    Exit For
                End If
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                data4_1 &= limit & ds.Tables("t1").Rows(i)("num").ToString
                ' data4_2 &= limit & ds.Tables("t1").Rows(i)("action1").ToString
                data4_name &= limit & "'" & ds.Tables("t1").Rows(i)("report_dept_name").ToString.Trim & "'"
            Next i

            'Dim limit As String = ""
            'For i As Integer = 0 To 5
            '    If i = 0 Then
            '        limit = ""
            '    Else
            '        limit = ","
            '    End If

            '    global_dept_num &= limit & ds.Tables("t1").Rows(i)("num").ToString
            '    global_dept_name &= limit & "'" & ds.Tables("t1").Rows(i)("report_dept_name").ToString & "' "

            '    global_b1 &= limit & ds.Tables("t1").Rows(i)("chk_newbi1").ToString
            '    global_b2 &= limit & ds.Tables("t1").Rows(i)("chk_newbi2").ToString
            '    global_b3 &= limit & ds.Tables("t1").Rows(i)("chk_newbi3").ToString
            '    global_b4 &= limit & ds.Tables("t1").Rows(i)("chk_newbi4").ToString
            '    global_b5 &= limit & ds.Tables("t1").Rows(i)("chk_newbi5").ToString
            '    global_b6 &= limit & ds.Tables("t1").Rows(i)("chk_newbi6").ToString
            '    global_b7 &= limit & ds.Tables("t1").Rows(i)("chk_newbi7").ToString
            '    global_b8 &= limit & ds.Tables("t1").Rows(i)("chk_newbi8").ToString
            '    global_b9 &= limit & ds.Tables("t1").Rows(i)("chk_newbi9").ToString
            '    global_b10 &= limit & ds.Tables("t1").Rows(i)("chk_newbi10").ToString

            'Next i

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try

    End Sub

    Sub bindStackGraph()
        Dim sql As String
        Dim ds As New DataSet

        Dim StrWer As StreamWriter
        Dim login_date As Date
        Dim target_date As Date
        Dim query_date As Date
        Dim irow As Int32 = 0
        Dim iCountRow As Integer = 0 ' จำนวน row จาก query
        Dim iStaticRow As Integer = 0
        Dim total As Integer = 0

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -180, login_date)

        Try

            ' Dim file_name = Session.SessionID & "_VerticalBarsChart3D.xml"
            Dim file_name = "VerticalStackedBarsChart.xml"
            StrWer = File.CreateText(Server.MapPath("xml/") & file_name)
            StrWer.Write("<?xml version=""1.0"" encoding=""utf-8""?>" & vbCrLf)
            StrWer.Write("<verticalStackedBarsChart>" & vbCrLf)
            StrWer.WriteLine("<config>")
            StrWer.WriteLine("<chartarea x=""45"" y=""75"" width=""400"" height=""180"" backColor1=""0xfafafa"" backColor2=""0xECECEC"" xMargin=""10"" borderColor=""0xbdbdbd"" borderThick=""2"" borderAlpha=""1"" />")
            StrWer.WriteLine("<yaxis tagSize=""13"" tagColor=""0x5e7086"" tagMargin=""3"" min=""0"" max=""1000"" intervals=""5"" decimals=""0"" />")
            StrWer.WriteLine("<xaxis tagSize=""13"" tagColor=""0x5e7086"" tagMargin=""1"" periodMargin=""0.5""/>")
            StrWer.WriteLine("<gridlines thick=""1"" color=""0xcccccc"" alpha=""0.8"" />")
            StrWer.WriteLine("<legends visible=""true"" x=""460"" y=""50"" width=""120"" square=""10"" textSize=""13"" textColor=""0x000000"" backColor=""0xFFFFFF"" backAlpha=""1"" borderColor=""0xcccccc"" borderThick=""1"" borderAlpha=""1"" legendBackColor=""0xf2f2f2"" />")
            StrWer.WriteLine("<tooltip size=""13"" xmargin=""2"" ymargin=""0"" />")
            StrWer.WriteLine("<animation openTime=""0.5"" synchronized=""false"" />")
            StrWer.WriteLine("</config>")

            StrWer.WriteLine("<texts>")
            StrWer.WriteLine("<paragraph align=""center"" bold=""true"" color=""0x000066"" size=""16"" x=""45"" y=""10"" width=""500"" text=""Total Number of Nominator By Customer Segment"" />")
            StrWer.WriteLine("<paragraph align=""center"" bold=""false"" color=""0x000066"" size=""14"" x=""45"" y=""30"" width=""500"" text=""From " & 1 & " " & MonthName(query_date.Month) & " " & query_date.Year & " to " & login_date.Day & " " & MonthName(login_date.Month) & " " & login_date.Year & """  />")
            StrWer.WriteLine("</texts>")



            StrWer.WriteLine("<series>")
            StrWer.WriteLine("<serie borderColor=""0xd78d51"" lightColor=""0xefb585"" darkColor=""0xd78d51"" tooltipBackColor=""0xd78d51"" tooltipTextColor=""0xFFFFFF"" name=""Not specified"" />")
            StrWer.WriteLine("<serie borderColor=""0xda716e"" lightColor=""0xe6a19f"" darkColor=""0xda716e"" tooltipBackColor=""0xda716e"" tooltipTextColor=""0xFFFFFF"" name=""Thai"" />")
            StrWer.WriteLine("<serie borderColor=""0x9ab762"" lightColor=""0xc3db95"" darkColor=""0x9ab762"" tooltipBackColor=""0xc3db95"" tooltipTextColor=""0x000000"" name=""Expat"" />")
            StrWer.WriteLine("<serie borderColor=""0x90b1db"" lightColor=""0xc0d5f0"" darkColor=""0x90b1db"" tooltipBackColor=""0xc0d5f0"" tooltipTextColor=""0x000000"" name=""Inter"" />")


            StrWer.WriteLine("</series>")



            sql = "SELECT  month(b.submit_date) as month1  , customer_segment , COUNT(customer_segment) as num  from star_detail_tab a"
            sql &= " INNER JOIN star_trans_list b ON a.star_id = b.star_id"
            ' sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id"
            sql &= " WHERE 1 = 1 "
            sql &= " AND b.submit_date BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & 1 & "' AND "
            sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            sql &= " AND isnull(b.is_cancel,0) = 0 AND isnull(b.is_delete,0) = 0 AND b.status_id > 0 "
            sql &= " GROUP BY  customer_segment , MONTH(b.submit_date)"
            sql &= " ORDER BY MONTH(b.submit_date) ASC ,  customer_segment ASC"
            ' Response.Write(sql)
            ' Response.Write(1)
            ' Response.End()
            ds = conn.getDataSetForTransaction(sql, "t1")
            'Response.Write(2)
            iCountRow = ds.Tables("t1").Rows.Count
            ' Return
            ' Response.Write(ds.Tables("t1").Rows.Count & "<br/>")
            StrWer.WriteLine("<periods>")
            For iDay As Int32 = -6 To 0
                If iCountRow = 0 Then
                    Exit For
                End If
                '  irow = 0 - iDay
                target_date = DateAdd(DateInterval.Month, iDay, login_date)
                StrWer.WriteLine("<period name=""" & MonthName(target_date.Month) & """>")


                If target_date.Month.ToString = ds.Tables("t1").Rows(iStaticRow)("month1").ToString Then
                    total = 0
                    For s = 1 To 4

                        ' Response.Write(target_date.Month.ToString & " || ")
                        ' Response.Write(ds.Tables("t1").Rows(iStaticRow)("num").ToString & " || ")
                        'Response.Write(ds.Tables("t1").Rows(iStaticRow)("customer_segment").ToString.Trim & " || ")
                        ' Response.Write(s & " || ")
                        'Response.Write(total & " || ")
                        '   Response.Write(iStaticRow & "<br/>")

                        Try
                            If iStaticRow <= (iCountRow - 1) Then
                                If s = 1 And ds.Tables("t1").Rows(iStaticRow)("customer_segment").ToString.Trim = "0" Then
                                    total += CInt(ds.Tables("t1").Rows(iStaticRow)("num").ToString)
                                    StrWer.WriteLine("<v>" & ds.Tables("t1").Rows(iStaticRow)("num").ToString & "</v>")
                                    iStaticRow += 1
                                ElseIf s = 2 And ds.Tables("t1").Rows(iStaticRow)("customer_segment").ToString = "1" Then
                                    total += CInt(ds.Tables("t1").Rows(iStaticRow)("num").ToString)
                                    StrWer.WriteLine("<v>" & ds.Tables("t1").Rows(iStaticRow)("num").ToString & "</v>")
                                    iStaticRow += 1

                                ElseIf s = 3 And ds.Tables("t1").Rows(iStaticRow)("customer_segment").ToString = "2" Then
                                    total += CInt(ds.Tables("t1").Rows(iStaticRow)("num").ToString)
                                    StrWer.WriteLine("<v>" & ds.Tables("t1").Rows(iStaticRow)("num").ToString & "</v>")
                                    iStaticRow += 1
                                ElseIf s = 4 And ds.Tables("t1").Rows(iStaticRow)("customer_segment").ToString = "3" Then
                                    total += CInt(ds.Tables("t1").Rows(iStaticRow)("num").ToString)
                                    StrWer.WriteLine("<v>" & ds.Tables("t1").Rows(iStaticRow)("num").ToString & "</v>")
                                    iStaticRow += 1

                                Else
                                    StrWer.WriteLine("<v>" & 0 & "</v>")
                                End If
                            Else
                                StrWer.WriteLine("<v>" & 0 & "</v>")
                            End If

                        Catch ex As Exception
                            StrWer.WriteLine("<v>" & 0 & "</v>")
                        End Try

                        If iStaticRow < iCountRow Then

                        Else
                            '  StrWer.WriteLine("<v>" & 0 & "</v>")
                        End If



                    Next s
                Else
                    For s = 1 To 4
                        total = 0
                        StrWer.WriteLine("<v>0</v>")
                    Next s
                End If

                StrWer.WriteLine("</period>")

                '  iStaticRow += 1
                If iCountRow < iStaticRow Then
                    Response.Write("End xxxxxxxxxxxxx")
                    Exit For
                End If
            Next iDay

            StrWer.WriteLine("</periods>")


            StrWer.WriteLine("</verticalStackedBarsChart>")
            Try
                StrWer.Close()
            Catch ex As Exception

            End Try
            ' Me.lblStatus.Text = "Files Writed."

            'Thread.Sleep(500)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
            Try
                StrWer.Close()
            Catch ex1 As Exception

            End Try

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub pineGraph()
        Dim sql As String
        Dim ds As New DataSet
        Dim login_date As Date
        Dim query_date As Date
        Dim irow As Int32 = 0
        Dim iCountRow As Integer = 0 ' จำนวน row จาก query
        Dim iStaticRow As Integer = 0
        Dim total As Integer = 0

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -180, login_date)
        Dim start_month As Array

        start_month = txtbiMonthDept.SelectedValue.Split("|")

        Dim StrWer As StreamWriter
        Try
            sql = "SELECT SUM(c.num) AS num , c.[Nominee type] AS nominee_type FROM star_trans_list a INNER JOIN star_detail_tab b ON a.star_id = b.star_id "


            sql &= " LEFT OUTER JOIN ("
            sql &= " SELECT aa.star_id , COUNT(*) AS num , 'Team' AS 'Nominee type'  FROM star_relate_dept aa INNER JOIN star_trans_list bb ON aa.star_id = bb.star_id "
            sql &= " WHERE MONTH(submit_date) =" & start_month(0) & " AND YEAR(submit_date) = " & start_month(1)
            sql &= " GROUP BY aa.star_id "
            sql &= " union "
            sql &= " SELECT aa.star_id , COUNT(*) AS num , 'Doctor' AS 'Nominee type' FROM star_relate_doctor aa INNER JOIN star_trans_list bb ON aa.star_id = bb.star_id "
            sql &= " WHERE MONTH(submit_date) =" & start_month(0) & " AND YEAR(submit_date) = " & start_month(1)
            sql &= " GROUP BY aa.star_id "
            sql &= " union"
            sql &= " SELECT aa.star_id , COUNT(*) AS num , 'Staff' AS 'Nominee type' FROM star_relate_person aa INNER JOIN star_trans_list bb ON aa.star_id = bb.star_id "
            sql &= " WHERE MONTH(submit_date) =" & start_month(0) & " AND YEAR(submit_date) = " & start_month(1)
            sql &= " GROUP BY aa.star_id "
            sql &= " ) c ON a.star_id = c.star_id"

            ' sql &= " WHERE submit_date BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & query_date.Day & "' AND "
            ' sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            sql &= " WHERE 1 = 1 "
            sql &= " and MONTH(submit_date) =" & start_month(0) & " AND YEAR(submit_date) = " & start_month(1)
            sql &= " AND a.status_id > 1 AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 AND ISNULL(c.[Nominee type],'') <> '' "
            sql &= " GROUP BY c.[Nominee type] ORDER BY c.[Nominee type] "
            ' Response.Write(sql)
            '  Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            ' Dim file_name = Session.SessionID & "_VerticalBarsChart3D.xml"

            Dim file_name = "PieChart3D.xml"

            File.Delete(Server.MapPath("xml/") & file_name)
            StrWer = File.CreateText(Server.MapPath("xml/") & file_name)
            StrWer.Write("<?xml version=""1.0"" encoding=""utf-8""?>" & vbCrLf)
            StrWer.Write("<pieChart>" & vbCrLf)
            StrWer.WriteLine("<config>")
            StrWer.WriteLine("<pie x=""120"" y=""80"" radiusX=""130"" radiusY=""60"" depth=""35"" />")
            StrWer.WriteLine("<legends visible=""true"" x=""435"" y=""80"" width=""140"" square=""10"" textSize=""13"" textColor=""0x000000"" backColor=""0xFFFFFF"" backAlpha=""1"" borderColor=""0x587b8c"" borderThick=""1"" borderAlpha=""1"" legendBackColor=""0xf2f2f2"" />")
            StrWer.WriteLine("<tooltip showName=""true"" showPercent=""true"" size=""13"" xmargin=""2"" ymargin=""0"" valueDecimals=""0"" percentDecimals=""1"" />")
            StrWer.WriteLine("<tag distance=""0.8"" size=""13"" decimals=""0"" />")
            StrWer.WriteLine("<animation openTime=""2"" moveTime=""0.5"" synchronized=""false"" />")
            StrWer.WriteLine("</config>")
            StrWer.WriteLine("<texts>")
            StrWer.WriteLine("<paragraph align=""center"" bold=""true"" color=""0x000066"" size=""16"" x=""45"" y=""10"" width=""500"" text=""Total Number of Nominee by Type of Nominee"" />")
            ' StrWer.WriteLine("<paragraph align=""center"" bold=""false"" color=""0x000066"" size=""14"" x=""45"" y=""30"" width=""500"" text=""Report from " & query_date.Day & " " & MonthName(query_date.Month) & " " & query_date.Year & " to " & login_date.Day & " " & MonthName(login_date.Month) & " " & login_date.Year & """ />")
            StrWer.WriteLine("<paragraph align=""center"" bold=""false"" color=""0x000066"" size=""14"" x=""45"" y=""30"" width=""500"" text=""Report from " & MonthName(start_month(0)) & " " & start_month(1) & """ />")
            StrWer.WriteLine("</texts>")
            StrWer.WriteLine("<series>")

            Dim fColor As String() = {"borderColor=""0xea6e29"" lightColor=""0xf8b088"" darkColor=""0xcd4a00"" tooltipBackColor=""0xea6e29"" tooltipTextColor=""0xFFFFFF"" tagColor=""0xFFFFFF""" _
                , "borderColor=""0x7fae3a"" lightColor=""0xacca80"" darkColor=""0x436413"" tooltipBackColor=""0x7fae3a"" tooltipTextColor=""0x000000"" tagColor=""0x000000""" _
                , "borderColor=""0x7e5433"" lightColor=""0xaa876b"" darkColor=""0x4d301a"" tooltipBackColor=""0x7e5433"" tooltipTextColor=""0xFFFFFF"" tagColor=""0xFFFFFF""" _
                , "borderColor=""0xc94c2f"" lightColor=""0xd7816d"" darkColor=""0x8c2208"" tooltipBackColor=""0xc94c2f"" tooltipTextColor=""0xFFFFFF"" tagColor=""0xFFFFFF""" _
                , "borderColor=""0xf6ad0f"" lightColor=""0xf6ce78"" darkColor=""0xce910b"" tooltipBackColor=""0xf6ad0f"" tooltipTextColor=""0x000000"" tagColor=""0x000000""" _
                , "borderColor=""0xc94c2f"" lightColor=""0xd7816d"" darkColor=""0x8c2208"" tooltipBackColor=""0xc94c2f"" tooltipTextColor=""0xFFFFFF"" tagColor=""0xFFFFFF""" _
                , "borderColor=""0x078fc8"" lightColor=""0x87d7f9"" darkColor=""0x0099da"" tooltipBackColor=""0x87d7f9"""}

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                StrWer.WriteLine("<serie " & fColor(i) & "  name=""" & ds.Tables("t1").Rows(i)("nominee_type").ToString & """ value=""" & ds.Tables("t1").Rows(i)("num").ToString & """ />")

            Next i
            ' StrWer.WriteLine("<serie borderColor=""0x7fae3a"" lightColor=""0xacca80"" darkColor=""0x436413"" tooltipBackColor=""0x7fae3a"" tooltipTextColor=""0x000000"" tagColor=""0x000000"" name=""Medication Error"" value=""180"" />")
            ' StrWer.WriteLine("<serie borderColor=""0x7e5433"" lightColor=""0xaa876b"" darkColor=""0x4d301a"" tooltipBackColor=""0x7e5433"" tooltipTextColor=""0xFFFFFF"" tagColor=""0xFFFFFF"" name=""Phlebitis/ Infiltration"" value=""320"" />")
            ' StrWer.WriteLine("<serie borderColor=""0xc94c2f"" lightColor=""0xd7816d"" darkColor=""0x8c2208"" tooltipBackColor=""0xc94c2f"" tooltipTextColor=""0xFFFFFF"" tagColor=""0xFFFFFF"" name=""Anesthesia Event"" value=""90"" />")
            ' StrWer.WriteLine("<serie borderColor=""0xf6ad0f"" lightColor=""0xf6ce78"" darkColor=""0xce910b"" tooltipBackColor=""0xf6ad0f"" tooltipTextColor=""0x000000"" tagColor=""0x000000"" name=""Pressure Ulcer"" value=""80"" />")
            ' StrWer.WriteLine("<serie borderColor=""0x078fc8"" lightColor=""0x87d7f9"" darkColor=""0x0099da"" tooltipBackColor=""0x87d7f9"" tooltipTextColor=""0x000000"" tagColor=""0x000000"" name=""Fall"" value=""131"" />")

            StrWer.WriteLine("</series>")

            StrWer.WriteLine("</pieChart>")
            Try
                StrWer.Close()
            Catch ex As Exception

            End Try
            ' Me.lblStatus.Text = "Files Writed."

            ' Thread.Sleep(500)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridNews_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridNews.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButton1 As LinkButton = CType(e.Row.FindControl("LinkButton1"), LinkButton)
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)

            If findArrayValue(priv_list, "204") = True Then ' HR
                LinkButton1.Attributes.Add("onclick", "window.open('star_news_edit.aspx?mode=edit&id=" & lblPK.Text & "', '', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600');return false;")
            Else
                LinkButton1.Visible = False
            End If

        End If
    End Sub

    Protected Sub GridNews_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridNews.SelectedIndexChanged

    End Sub

    Protected Sub txtbiMonth_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles txtbiMonth.SelectedIndexChanged
        bindBIWayPie1()
    End Sub
End Class
