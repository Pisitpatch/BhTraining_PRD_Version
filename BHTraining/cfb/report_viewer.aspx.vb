Imports System.IO
Imports System.Data
Imports System.Threading
Imports ShareFunction

Partial Class incident_report_viewer
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected viewtype As String = ""
    Protected totalGrandTopic As String = ""
    Protected global_unit_num As String = ""
    Protected global_unit_name As String = ""

    Protected global_unit_num2 As String = ""
    Protected global_unit_name2 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        viewtype = Request.QueryString("viewtype")
        Session("viewtype") = viewtype & ""
        'Response.Write(Session("viewtype").ToString)
        ' priv_list = Session("priv_list")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If
        '  genNo()
        If Not Page.IsPostBack Then ' first time load
            bindDateRange()
            txtdate1.SelectedIndex = txtdate1.Items.Count - 1
            txtdate2.SelectedIndex = txtdate2.Items.Count - 1
        End If


        barGraph()
        pineGraph()
        lineChart()
        bindStackGraph()

        bindTotalGrandTopic()
        bindGridIR()

        bindDefendant()
        bindPraise()

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

    Sub genNo()
        Dim sql As String
        Dim a As Date
        Dim err As String
        Try
            For i = 1 To 12
                For s As Integer = 1 To 31
                    sql = "insert into temp_day (day_no , month_no) values("
                    sql &= s & ","
                    sql &= i
                    sql &= ")"
                    err = conn.executeSQL(sql)
                    If err <> "" Then
                        Throw New Exception(err)
                    End If
                Next
            Next
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        End Try
    End Sub

    Sub barGraph()
        Dim sql As String
        Dim ds As New DataSet

        Dim login_date As Date
        Dim query_date As Date
        login_date = Date.Now


        Dim StrWer As StreamWriter
        Try


            ' Dim file_name = Session.SessionID & "_VerticalBarsChart3D.xml"
            Dim file_name = "VerticalBarsChart3D.xml"
            StrWer = File.CreateText(Server.MapPath("xml/") & file_name)
            StrWer.Write("<?xml version=""1.0"" encoding=""utf-8""?>" & vbCrLf)
            StrWer.Write("<verticalBarsChart>" & vbCrLf)
            StrWer.WriteLine("<config>")
            StrWer.WriteLine("<chartarea x=""45"" y=""75"" width=""400"" height=""180"" backColor1=""0xFDFDFD"" backColor2=""0xECECEC"" bottomColor=""0xD9D9D9"" zeroColor=""0xD9D9D9"" zeroAlpha=""0.8"" xMargin=""5"" borderColor=""0xcccccc"" borderThick=""2"" borderAlpha=""1"" wDepth=""15"" hDepth=""10"" />")
            StrWer.WriteLine("<yaxis tagSize=""13"" tagColor=""0x5e7086"" tagMargin=""3"" min=""0"" max=""500"" intervals=""5"" decimals=""0"" />")
            StrWer.WriteLine("<xaxis tagSize=""13"" tagColor=""0x5e7086"" tagMargin=""1"" periodMargin=""0.2"" barMargin=""0.2"" />")
            StrWer.WriteLine("<gridlines thick=""1"" color=""0xcccccc"" alpha=""0.8"" />")
            StrWer.WriteLine("<legends visible=""true"" x=""480"" y=""65"" width=""100"" square=""10"" textSize=""13"" textColor=""0x000000"" backColor=""0xFFFFFF"" backAlpha=""1"" borderColor=""0xcccccc"" borderThick=""1"" borderAlpha=""1"" legendBackColor=""0xf2f2f2"" />")
            StrWer.WriteLine("<tooltip size=""13"" xmargin=""2"" ymargin=""0"" />")
            StrWer.WriteLine("<animation openTime=""1"" synchronized=""false"" />")
            StrWer.WriteLine("</config>")
            StrWer.WriteLine("<texts>")
            StrWer.WriteLine("<paragraph align=""center"" bold=""true"" color=""0x000066"" size=""16"" x=""45"" y=""10"" width=""500"" text=""Customer Feedback by Risk Level"" />")
            StrWer.WriteLine("<paragraph align=""center"" bold=""false"" color=""0x000066"" size=""14"" x=""45"" y=""30"" width=""500"" text=""Total Number of Incident Report by Risk Level"" />")
            StrWer.WriteLine("<paragraph align=""center"" bold=""true"" color=""0x5e7086"" size=""13"" x=""8"" y=""40"" width=""50"" text=""Values"" />")
            StrWer.WriteLine("<paragraph align=""center"" bold=""true"" color=""0x5e7086"" size=""13"" x=""45"" y=""280"" width=""400"" text=""Month"" />")
            StrWer.WriteLine("</texts>")
            StrWer.WriteLine("<series>")
            StrWer.WriteLine("<serie borderColor=""0xbd7400"" lightColor=""0xffde00"" darkColor=""0xbd7400"" tooltipBackColor=""0xe6b600"" tooltipTextColor=""0x000000"" name=""Not specific"" />")
            StrWer.WriteLine("<serie borderColor=""0x425802"" lightColor=""0xc1d681"" darkColor=""0x425802"" tooltipBackColor=""0x93a853"" tooltipTextColor=""0xFFFFFF"" name=""Near miss(0)"" />")
            StrWer.WriteLine("<serie borderColor=""0x076984"" lightColor=""0x40cff6"" darkColor=""0x06a2cc"" tooltipBackColor=""0xe0323c"" tooltipTextColor=""0xFFFFFF"" name=""No harm(1)"" />")
            StrWer.WriteLine("<serie borderColor=""0x5e2c8d"" lightColor=""0x9872bb"" darkColor=""0x562e7b""  tooltipBackColor=""0xe0323c"" tooltipTextColor=""0xFFFFFF"" name=""Mild AE(2)"" />")
            StrWer.WriteLine("<serie borderColor=""0xb3036b"" lightColor=""0xf758b6"" darkColor=""0xd60380"" tooltipBackColor=""0xe0323c"" tooltipTextColor=""0xFFFFFF"" name=""Moderate AE(3)"" />")
            StrWer.WriteLine("<serie borderColor=""0xa03f11"" lightColor=""0xe87037"" darkColor=""0xdc4e0a"" tooltipBackColor=""0xe0323c"" tooltipTextColor=""0xFFFFFF"" name=""Serious AE(4)"" />")
            StrWer.WriteLine("<serie borderColor=""0x700408"" lightColor=""0xf03a45"" darkColor=""0x700408"" tooltipBackColor=""0xe0323c"" tooltipTextColor=""0xFFFFFF"" name=""Sentinel Event(5)"" />")
            StrWer.WriteLine("</series>")
            StrWer.WriteLine("<periods>")
            For i As Integer = 1 To 6
                query_date = DateAdd(DateInterval.Month, -6 + (i), login_date)
                sql = "SELECT b.tqm_clinic , month(c.datetime_report) ,  year(c.datetime_report), COUNT(b.tqm_clinic) AS num_severe"
                sql &= " FROM  cfb_comment_list b "
                sql &= " INNER JOIN cfb_detail_tab c ON b.ir_id = c.ir_id "
                sql &= " WHERE month(c.datetime_report) = " & query_date.Month
                sql &= " AND year(c.datetime_report) = " & query_date.Year
                sql &= " GROUP BY b.tqm_clinic  ,month(c.datetime_report) , year(c.datetime_report)"
                sql &= " ORDER BY b.tqm_clinic "
                '   Response.Write(sql & "<Br/>")
                ds = conn.getDataSet(sql, "t1")
                StrWer.WriteLine("<period name=""" & getMonthName(query_date.Month) & """>")
                Dim iRow As Integer = 0
                For s As Integer = 0 To 6
                    If ds.Tables("t1").Rows.Count > 0 Then

                        If iRow < ds.Tables("t1").Rows.Count Then
                            '  Response.Write(Convert.ToInt32(ds.Tables("t1").Rows(s)("tqm_clinic")))
                            ' Response.Write(" irowx=" & ds.Tables("t1").Rows(iRow)("tqm_clinic").ToString & ": <br/>")
                            ' Response.Write(" s=" & s & ": <br/>")

                            If Convert.ToInt32(ds.Tables("t1").Rows(iRow)("tqm_clinic").ToString) = s Then
                                StrWer.Write("<v>" & ds.Tables("t1").Rows(iRow)("num_severe").ToString & "</v>")
                                iRow += 1
                                'Response.Write(" irow2=" & iRow & ": ")
                            Else
                                StrWer.Write("<v>0</v>")
                            End If

                            ' iRow += 1



                        Else
                            StrWer.Write("<v>0</v>")
                        End If
                    Else
                        StrWer.Write("<v>0</v>")
                    End If

                Next s
                StrWer.WriteLine("</period>")
            Next i

            StrWer.WriteLine("</periods>")
            StrWer.WriteLine("</verticalBarsChart>")

            Try
                StrWer.Close()
            Catch ex As Exception

            End Try

            ' Me.lblStatus.Text = "Files Writed."

            Thread.Sleep(1000)
        Catch ex As Exception
            '  Response.Write(ex.Message & sql)
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
        query_date = DateAdd(DateInterval.Day, -30, login_date)

        Dim StrWer As StreamWriter
        Try
            sql = "SELECT COUNT(c.comment_type_id) AS num , c.comment_type_name FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id"
            sql &= " WHERE date_submit BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & query_date.Day & "' AND "
            sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            sql &= " AND a.status_id > 1 AND ISNULL(a.is_delete,0) = 0 AND ISNULL(a.is_cancel,0) = 0 "
            sql &= " GROUP BY c.comment_type_name , c.comment_type_id ORDER BY c.comment_type_name "
            'Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")

            ' Dim file_name = Session.SessionID & "_VerticalBarsChart3D.xml"
            Dim file_name = "PieChart3D.xml"
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
            StrWer.WriteLine("<paragraph align=""center"" bold=""true"" color=""0x000066"" size=""16"" x=""45"" y=""10"" width=""500"" text=""Total Number of Customer Feedback by Type of Comment"" />")
            StrWer.WriteLine("<paragraph align=""center"" bold=""false"" color=""0x000066"" size=""14"" x=""45"" y=""30"" width=""500"" text=""Incident report information from " & query_date.Day & " " & MonthName(query_date.Month) & " " & query_date.Year & " to " & login_date.Day & " " & MonthName(login_date.Month) & " " & login_date.Year & """ />")
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
                StrWer.WriteLine("<serie " & fColor(i) & "  name=""" & ds.Tables("t1").Rows(i)("comment_type_name").ToString & """ value=""" & ds.Tables("t1").Rows(i)("num").ToString & """ />")

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

            Thread.Sleep(500)
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub lineChart()
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
        query_date = DateAdd(DateInterval.Day, -30, login_date)
        'Response.Write(Date.Now)
        Try

            ' Dim file_name = Session.SessionID & "_VerticalBarsChart3D.xml"
            Dim file_name = "line-chart.xml"
            StrWer = File.CreateText(Server.MapPath("xml/") & file_name)
            StrWer.Write("<?xml version=""1.0"" encoding=""utf-8""?>" & vbCrLf)
            StrWer.Write("<lineChart>" & vbCrLf)
            StrWer.WriteLine("<config>")
            StrWer.WriteLine("<chartarea x=""50"" y=""50"" width=""400"" height=""200"" backColor1=""0xfafafa"" backColor2=""0xf1f1f1"" xMargin=""20"" borderColor=""0xbdbdbd"" borderThick=""2"" borderAlpha=""1"" />")
            StrWer.WriteLine("<yaxis tagSize=""8"" tagColor=""0x587b8c"" tagMargin=""3"" min=""0"" max=""90"" intervals=""5"" decimals=""1"" />")
            StrWer.WriteLine("<xaxis tagSize=""8"" tagColor=""0x587b8c"" tagMargin=""1"" />")
            StrWer.WriteLine("<gridlines thick=""1"" color=""0xcdcdcd"" alpha=""0.8"" />")
            StrWer.WriteLine("<legends visible=""true"" x=""480"" y=""50"" width=""120"" square=""10"" textSize=""12"" textColor=""0x5e7086"" backColor=""0xFFFFFF"" backAlpha=""1"" borderColor=""0x587b8c"" borderThick=""1"" borderAlpha=""1"" legendBackColor=""0xf2f2f2"" />")
            StrWer.WriteLine("<points radius=""3"" />")
            StrWer.WriteLine("<tooltip size=""10"" xmargin=""2"" ymargin=""0"" />")
            StrWer.WriteLine("<animation openTime=""0.5"" moveTime=""0.5"" synchronized=""false"" />>")
            StrWer.WriteLine("</config>")

            StrWer.WriteLine("<texts>")
            StrWer.WriteLine("<paragraph align=""center"" bold=""true"" color=""0x000066"" size=""12"" x=""45"" y=""10"" width=""500"" text=""Total Number of Customer Feedback By Service Type"" />")
            StrWer.WriteLine("<paragraph align=""center"" bold=""false"" color=""0x000066"" size=""10"" x=""45"" y=""30"" width=""500"" text=""From " & query_date.Day & " " & MonthName(query_date.Month) & " " & query_date.Year & " to " & login_date.Day & " " & MonthName(login_date.Month) & " " & login_date.Year & """  />")
            StrWer.WriteLine("</texts>")

       

            StrWer.WriteLine("<series>")

            StrWer.WriteLine("<serie lineColor=""0x00AA00"" thick=""1"" pointInteriorColor=""0x55FF55"" pointBorderColor=""0x00AA00"" tooltipBackColor=""0x55FF55"" tooltipTextColor=""0x000000"" name=""OPD"" />")
            StrWer.WriteLine("<serie lineColor=""0xf99d39"" thick=""1"" pointInteriorColor=""0xfbc891"" pointBorderColor=""0xf99d39"" tooltipBackColor=""0xfbc891"" tooltipTextColor=""0x000000"" name=""IPD"" />")
            StrWer.WriteLine("<serie lineColor=""0x000066"" thick=""1"" pointInteriorColor=""0x000099"" pointBorderColor=""0x000066"" tooltipBackColor=""0x0000AA"" tooltipTextColor=""0xFFFFFF"" name=""Not specified"" />")

            StrWer.WriteLine("<serie lineColor=""0xDD0000"" thick=""2"" pointInteriorColor=""0xFF3333"" pointBorderColor=""0xDD0000"" tooltipBackColor=""0xFF3333"" tooltipTextColor=""0xFFFFFF"" name=""Total IR"" />")
            StrWer.WriteLine("</series>")

          

            sql = "SELECT day(date_submit) as day1 , month(date_submit) as month1  , service_type , COUNT(service_type) as num  from cfb_detail_tab a"
            sql &= " INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id"
            sql &= " WHERE date_submit BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & query_date.Day & "' AND "
            sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            sql &= " AND b.is_cancel = 0 AND b.is_delete = 0 AND b.status_id > 0 "
            sql &= " GROUP BY  service_type , day(date_submit), MONTH(date_submit)"
            sql &= " ORDER BY MONTH(date_submit) ASC , day(date_submit) ASC,  service_type DESC"
            '  Response.Write(sql)
            ' Response.End()
            ds = conn.getDataSet(sql, "t1")
            iCountRow = ds.Tables("t1").Rows.Count

            StrWer.WriteLine("<periods>")
            For iDay As Int32 = -30 To 0
                If iCountRow = 0 Then
                    Exit For
                End If
                '  irow = 0 - iDay
                target_date = DateAdd(DateInterval.Day, iDay, login_date)
                StrWer.WriteLine("<period name=""" & target_date.Day & """>")


                If target_date.Day.ToString = ds.Tables("t1").Rows(iStaticRow)("day1").ToString And target_date.Month.ToString = ds.Tables("t1").Rows(iStaticRow)("month1").ToString Then
                    total = 0
                    For s = 1 To 4

                        ' Response.Write(target_date.Day.ToString & " ||| ")
                        ' Response.Write(ds.Tables("t1").Rows(iStaticRow)("num").ToString & " || ")
                        ' Response.Write(iCountRow & " || ")
                        ' Response.Write(s & " || ")
                        'Response.Write(total & " || ")
                        'Response.Write(iStaticRow & "<br/>")

                        Try
                            If iStaticRow < (iCountRow - 1) Then
                                If s = 1 And ds.Tables("t1").Rows(iStaticRow)("service_type").ToString.Trim = "OPD" Then
                                    total += CInt(ds.Tables("t1").Rows(iStaticRow)("num").ToString)
                                    StrWer.WriteLine("<v>" & ds.Tables("t1").Rows(iStaticRow)("num").ToString & "</v>")
                                    iStaticRow += 1
                                ElseIf s = 2 And ds.Tables("t1").Rows(iStaticRow)("service_type").ToString = "IPD" Then
                                    total += CInt(ds.Tables("t1").Rows(iStaticRow)("num").ToString)
                                    StrWer.WriteLine("<v>" & ds.Tables("t1").Rows(iStaticRow)("num").ToString & "</v>")
                                    iStaticRow += 1

                                ElseIf s = 3 And ds.Tables("t1").Rows(iStaticRow)("service_type").ToString = "" Then
                                    total += CInt(ds.Tables("t1").Rows(iStaticRow)("num").ToString)
                                    StrWer.WriteLine("<v>" & ds.Tables("t1").Rows(iStaticRow)("num").ToString & "</v>")
                                    iStaticRow += 1

                                ElseIf s = 4 Then

                                    StrWer.WriteLine("<v>" & total & "</v>")
                                Else
                                    StrWer.WriteLine("<v>" & 0 & "</v>")
                                End If
                            Else
                                StrWer.WriteLine("<v>" & total & "</v>")
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


            StrWer.WriteLine("</lineChart>")
            Try
                StrWer.Close()
            Catch ex1 As Exception

            End Try
            ' Me.lblStatus.Text = "Files Writed."

            Thread.Sleep(500)
        Catch ex As Exception
            '  Response.Write(ex.Message & sql)
            Try
                StrWer.Close()
            Catch ex1 As Exception

            End Try
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
            StrWer.WriteLine("<paragraph align=""center"" bold=""true"" color=""0x000066"" size=""16"" x=""45"" y=""10"" width=""500"" text=""Total Number of Customer Feedback By Customer Segment"" />")
            StrWer.WriteLine("<paragraph align=""center"" bold=""false"" color=""0x000066"" size=""14"" x=""45"" y=""30"" width=""500"" text=""From " & 1 & " " & MonthName(query_date.Month) & " " & query_date.Year & " to " & login_date.Day & " " & MonthName(login_date.Month) & " " & login_date.Year & """  />")
            StrWer.WriteLine("</texts>")



            StrWer.WriteLine("<series>")
            StrWer.WriteLine("<serie borderColor=""0xd78d51"" lightColor=""0xefb585"" darkColor=""0xd78d51"" tooltipBackColor=""0xd78d51"" tooltipTextColor=""0xFFFFFF"" name=""Not specified"" />")
            StrWer.WriteLine("<serie borderColor=""0xda716e"" lightColor=""0xe6a19f"" darkColor=""0xda716e"" tooltipBackColor=""0xda716e"" tooltipTextColor=""0xFFFFFF"" name=""Thai"" />")
            StrWer.WriteLine("<serie borderColor=""0x9ab762"" lightColor=""0xc3db95"" darkColor=""0x9ab762"" tooltipBackColor=""0xc3db95"" tooltipTextColor=""0x000000"" name=""Expat"" />")
            StrWer.WriteLine("<serie borderColor=""0x90b1db"" lightColor=""0xc0d5f0"" darkColor=""0x90b1db"" tooltipBackColor=""0xc0d5f0"" tooltipTextColor=""0x000000"" name=""Inter"" />")


            StrWer.WriteLine("</series>")



            sql = "SELECT  month(date_submit) as month1  , customer_segment , COUNT(customer_segment) as num  from cfb_detail_tab a"
            sql &= " INNER JOIN ir_trans_list b ON a.ir_id = b.ir_id"
            sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id"
            sql &= " WHERE date_submit BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & 1 & "' AND "
            sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            sql &= " AND b.is_cancel = 0 AND b.is_delete = 0 AND b.status_id > 0 "
            sql &= " GROUP BY  customer_segment , MONTH(date_submit)"
            sql &= " ORDER BY MONTH(date_submit) ASC ,  customer_segment ASC"
            'Response.Write(sql)

            ' Response.End()
            ds = conn.getDataSet(sql, "t1")
            iCountRow = ds.Tables("t1").Rows.Count
            'Response.Write(ds.Tables("t1").Rows.Count & "<br/>")
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

            Thread.Sleep(500)
        Catch ex As Exception
            '   Response.Write(ex.Message & sql)
            Try
                StrWer.Close()
            Catch ex1 As Exception

            End Try

        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindTotalGrandTopic()
        Dim sql As String
        Dim ds As New DataSet

        Dim login_date As Date

        Dim query_date As Date

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)

        Try

            sql = "SELECT COUNT(d.grand_topic_id) AS num FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id"
            sql &= " INNER JOIN ir_topic_grand d ON c.grand_topic = d.grand_topic_id "
            sql &= " WHERE date_submit BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & 1 & "' AND "
            sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            '   sql &= " GROUP BY grand_topic_id , grand_topic_name"
            '  sql &= " ORDER BY COUNT(d.grand_topic_id) DESC"
            ' Response.Write(sql)
            ds = conn.getDataSet(sql, "t2")
            totalGrandTopic = ds.Tables("t2").Rows(0)(0).ToString
            txtTotalGrandTopic.text = totalGrandTopic
            ' Response.Write(totalGrandTopic)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try

    End Sub

    Sub bindGridIR()
        Dim sql As String
        Dim ds As New DataSet

        Dim login_date As Date

        Dim query_date As Date

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)

        Try
            sql = "SELECT d.grand_topic_id , d.grand_topic_name , COUNT(d.grand_topic_id) AS num FROM ir_trans_list a INNER JOIN cfb_detail_tab b ON a.ir_id = b.ir_id "
            sql &= " INNER JOIN cfb_comment_list c ON a.ir_id = c.ir_id"
            sql &= " INNER JOIN ir_topic_grand d ON c.grand_topic = d.grand_topic_id "
            sql &= " WHERE date_submit BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & 1 & "' AND "
            sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            sql &= " GROUP BY grand_topic_id , grand_topic_name"
            sql &= " ORDER BY COUNT(d.grand_topic_id) DESC"
            '  Response.Write(sql)
            '   Return
            ds = conn.getDataSet(sql, "t1")

            GridIR.DataSource = ds
            GridIR.DataBind()

         
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try

    End Sub

    Sub bindDefendant()
        Dim sql As String
        Dim ds As New DataSet
        Dim login_date As Date
        Dim query_date As Date
        Dim start_month As Array

        start_month = txtdate1.SelectedValue.Split("|")

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)
        Try
            sql = "SELECT a.dept_unit_name , ISNULL(COUNT(a.dept_unit_name),0) AS num FROM ir_cfb_unit_defendant a INNER JOIN cfb_comment_list b ON a.comment_id = b.comment_id  "
            sql &= " INNER JOIN ir_trans_list c ON b.ir_id = c.ir_id "
            sql &= " WHERE MONTH(date_submit) =" & start_month(0) & " AND YEAR(date_submit) = " & start_month(1) 
            sql &= " AND c.status_id > 1 AND ISNULL(c.is_delete,0) = 0 AND ISNULL(c.is_cancel,0) = 0 AND a.dept_unit_id NOT IN (136 ,143) "
            sql &= " AND b.comment_type_id = 3 "
            sql &= " GROUP BY a.dept_unit_id , a.dept_unit_name ORDER BY COUNT(a.dept_unit_name) DESC "
            '  Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")

            Dim limit = ""
            global_unit_num = ""
            global_unit_name = ""
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 10 Then
                    Exit For
                End If

                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                global_unit_num &= limit & "['" & ds.Tables("t1").Rows(i)("dept_unit_name").ToString.Trim & "' , " & CInt(ds.Tables("t1").Rows(i)("num").ToString) & " ]"

                'global_unit_num &= limit & CInt(ds.Tables("t1").Rows(i)("num").ToString)
                '   global_unit_name &= limit & "'" & ds.Tables("t1").Rows(i)("dept_unit_name").ToString & "'"
            Next i
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindPraise()
        Dim sql As String
        Dim ds As New DataSet
        Dim login_date As Date
        Dim query_date As Date
        Dim start_month As Array

        start_month = txtdate2.SelectedValue.Split("|")
        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)
        Try
            sql = "SELECT a.dept_unit_name , ISNULL(COUNT(a.dept_unit_name),0) AS num FROM ir_cfb_unit_defendant a INNER JOIN cfb_comment_list b ON a.comment_id = b.comment_id  "
            sql &= " INNER JOIN ir_trans_list c ON b.ir_id = c.ir_id "
            sql &= " WHERE MONTH(date_submit) =" & start_month(0) & " AND YEAR(date_submit) = " & start_month(1) 
            sql &= " AND c.status_id > 1 AND ISNULL(c.is_delete,0) = 0 AND ISNULL(c.is_cancel,0) = 0 AND a.dept_unit_id NOT IN (136 ,143) "
            sql &= " AND b.comment_type_id = 1 "
            sql &= " GROUP BY a.dept_unit_id , a.dept_unit_name ORDER BY COUNT(a.dept_unit_name) DESC "
            '  Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")

            Dim limit = ""
            global_unit_num2 = ""
            global_unit_name2 = ""
            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                If i = 10 Then
                    Exit For
                End If

                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                global_unit_num2 &= limit & "['" & ds.Tables("t1").Rows(i)("dept_unit_name").ToString.Trim & "' , " & CInt(ds.Tables("t1").Rows(i)("num").ToString) & " ]"

                'global_unit_num &= limit & CInt(ds.Tables("t1").Rows(i)("num").ToString)
                '   global_unit_name &= limit & "'" & ds.Tables("t1").Rows(i)("dept_unit_name").ToString & "'"
            Next i
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridIR_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridIR.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPercent As Label = CType(e.Row.FindControl("lblPercent"), Label)
            Dim lblNum As Label = CType(e.Row.FindControl("lblNum"), Label)

            Try

                lblPercent.Text = FormatNumber((Convert.ToDouble(lblNum.Text) / Convert.ToDouble(txtTotalGrandTopic.Text)) * 100, 2)

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally

            End Try

        End If
    End Sub

    Protected Sub GridIR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridIR.SelectedIndexChanged

    End Sub

    Sub bindDateRange()
        Dim sql As String
        Dim ds As New DataSet
        Dim myvalue As String
        Dim mytext As String
        Try
            sql = "SELECT  MONTH(date_submit) AS m , YEAR(date_submit) AS y FROM ir_trans_list WHERE ISNULL(is_delete,0) = 0 AND ISNULL(is_cancel,0) = 0 AND ISNULL(date_submit,0) > 0 "
            sql &= " GROUP BY MONTH(date_submit) , YEAR(date_submit)"
            sql &= " ORDER BY YEAR(date_submit) ASC , MONTH(date_submit) "
            ds = conn.getDataSet(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                myvalue = ds.Tables("t1").Rows(i)("m").ToString & "|" & ds.Tables("t1").Rows(i)("y").ToString
                mytext = getMonthName(CInt(ds.Tables("t1").Rows(i)("m").ToString)) & " " & ds.Tables("t1").Rows(i)("y").ToString
                txtdate1.Items.Add(New ListItem(mytext, myvalue))
                txtdate2.Items.Add(New ListItem(mytext, myvalue))
            Next i



        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
