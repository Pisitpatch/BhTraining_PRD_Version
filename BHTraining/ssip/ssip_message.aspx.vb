Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class ssip_ssip_message
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected priv_list() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        bindGrid()
        bindStackGraph()
        bindGridSSIP1()
        bindGridSSIP2()

        bindMessage("2", lblInnovation)
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

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_news WHERE ISNULL(is_delete,0) = 0 AND is_active = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridNews.DataSource = ds
            GridNews.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindMessage(ByVal pk As String, ByVal lb As Label)
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ssip_message WHERE ssip_message_id = " & pk
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lb.Text = "<img src='../share/ssip/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' /><br/><br/>"
            End If
            lb.Text &= ds.Tables("t1").Rows(0)("message_detail")

        Catch ex As Exception
            Response.Write(ex.Message)
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
            StrWer.WriteLine("<yaxis tagSize=""13"" tagColor=""0x5e7086"" tagMargin=""3"" min=""0"" max=""50"" intervals=""5"" decimals=""0"" />")
            StrWer.WriteLine("<xaxis tagSize=""13"" tagColor=""0x5e7086"" tagMargin=""1"" periodMargin=""0.5""/>")
            StrWer.WriteLine("<gridlines thick=""1"" color=""0xcccccc"" alpha=""0.8"" />")
            StrWer.WriteLine("<legends visible=""true"" x=""460"" y=""50"" width=""120"" square=""10"" textSize=""13"" textColor=""0x000000"" backColor=""0xFFFFFF"" backAlpha=""1"" borderColor=""0xcccccc"" borderThick=""1"" borderAlpha=""1"" legendBackColor=""0xf2f2f2"" />")
            StrWer.WriteLine("<tooltip size=""13"" xmargin=""2"" ymargin=""0"" />")
            StrWer.WriteLine("<animation openTime=""0.5"" synchronized=""false"" />")
            StrWer.WriteLine("</config>")

            StrWer.WriteLine("<texts>")
            StrWer.WriteLine("<paragraph align=""center"" bold=""true"" color=""0x000066"" size=""16"" x=""45"" y=""10"" width=""500"" text=""Staff Suggestion & Innovation Overview"" />")
            StrWer.WriteLine("<paragraph align=""center"" bold=""false"" color=""0x000066"" size=""14"" x=""45"" y=""30"" width=""500"" text=""From " & 1 & " " & MonthName(query_date.Month) & " " & query_date.Year & " to " & login_date.Day & " " & MonthName(login_date.Month) & " " & login_date.Year & """  />")
            StrWer.WriteLine("</texts>")



            StrWer.WriteLine("<series>")
            StrWer.WriteLine("<serie borderColor='0xda716e' lightColor='0xe6a19f' darkColor='0xda716e' tooltipBackColor='0xda716e' tooltipTextColor='0xFFFFFF' name='Not pass' />")
            StrWer.WriteLine("<serie borderColor='0xd78d51' lightColor='0xefb585' darkColor='0xd78d51' tooltipBackColor='0xd78d51' tooltipTextColor='0xFFFFFF' name='Duplicate subject' />")
            StrWer.WriteLine("<serie borderColor='0x9ab762' lightColor='0xc3db95' darkColor='0x9ab762' tooltipBackColor='0xc3db95' tooltipTextColor='0x000000' name='Pass consideration' />")
            StrWer.WriteLine("<serie borderColor='0x90b1db' lightColor='0xc0d5f0' darkColor='0x90b1db' tooltipBackColor='0xc0d5f0' tooltipTextColor='0x000000' name='During division/department consider' />")
            StrWer.WriteLine("<serie borderColor='0x90b1db' lightColor='0xc0d5f0' darkColor='0x90b1db' tooltipBackColor='0xc0d5f0' tooltipTextColor='0x000000' name='During management consider' />")
            StrWer.WriteLine("<serie borderColor='0x90b1db' lightColor='0xc0d5f0' darkColor='0x90b1db' tooltipBackColor='0xc0d5f0' tooltipTextColor='0x000000' name='Good ideas, but need more detail' />")
            StrWer.WriteLine("serie borderColor='0x90b1db' lightColor='0xc0d5f0' darkColor='0x90b1db' tooltipBackColor='0xc0d5f0' tooltipTextColor='0x000000' name='Good ideas, can be implement' />")

            StrWer.WriteLine("</series>")



        
            '  Response.Write("ttt")
            StrWer.WriteLine("<periods>")
            For iDay As Int32 = -6 To 0

                target_date = DateAdd(DateInterval.Month, iDay, login_date)
                StrWer.WriteLine("<period name=""" & MonthName(target_date.Month) & """>")

                For m As Integer = 0 To 5


                    sql = "select count(*) as num   from ssip_hr_tab a inner join ssip_trans_list b on a.ssip_id = b.ssip_id"
                    sql &= " WHERE YEAR(submit_date) =" & target_date.Year & " AND MONTH(submit_date) = " & target_date.Month
                    sql &= " AND ISNULL(b.is_cancel,0) = 0 AND ISNULL(b.is_delete,0) = 0 AND b.status_id > 1 AND ISNULL(a.result_id,0) =  " & m
                    sql &= " group by isnull(result_id,0)"
                    If target_date.Month = 7 Then
                        '   Response.Write(sql)
                    End If
                    ds = conn.getDataSetForTransaction(sql, "t" & m)

                    If ds.Tables("t" & m).Rows.Count > 0 Then
                        ' Response.Write("xx" & ds.Tables("t" & m).Rows(0)("num").ToString)
                        StrWer.WriteLine("<v>" & ds.Tables("t" & m).Rows(0)("num").ToString & "</v>")
                    Else
                        StrWer.WriteLine("<v>0</v>")
                    End If
                Next m


                StrWer.WriteLine("</period>")

                '  iStaticRow += 1

            Next iDay

            StrWer.WriteLine("</periods>")


            StrWer.WriteLine("</verticalStackedBarsChart>")
            Try
                StrWer.Close()
            Catch ex As Exception

            End Try
            ' Me.lblStatus.Text = "Files Writed."

            ' Thread.Sleep(500)
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

    Sub bindGridSSIP1()
        Dim sql As String
        Dim ds As New DataSet

        Dim login_date As Date

        Dim query_date As Date

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)

        Try
            sql = "select * from ssip_trans_list a  "
            sql &= "inner join ssip_hr_tab b on a.ssip_id = b.ssip_id"
            sql &= " INNER JOIN ssip_detail_tab c ON a.ssip_id = c.ssip_id "
            sql &= " WHERE a.is_public = 1 AND ISNULL(is_delete,0) = 0 AND ISNULL(is_cancel,0) = 0 "

            sql &= " ORDER BY a.submit_date DESC"
            '  Response.Write(sql)
            '   Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            GripSSIP1.DataSource = ds
            GripSSIP1.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try

    End Sub

    Sub bindGridSSIP2()
        Dim sql As String
        Dim ds As New DataSet

        Dim login_date As Date

        Dim query_date As Date

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)

        Try
            sql = "select COUNT(a.report_dept_id) AS num , a.report_dept_id  , b.dept_name_en from ssip_trans_list a  "
            sql &= "inner join user_dept b on a.report_dept_id = b.dept_id"

            sql &= " WHERE ISNULL(is_delete,0) = 0 AND ISNULL(is_cancel,0) = 0 AND status_id > 1  "
            'sql &= " AND submit_date BETWEEN '" & query_date.Year & "-" & query_date.Month & "-" & 1 & "' AND "
            'sql &= " '" & login_date.Year & "-" & login_date.Month & "-" & login_date.Day & " 23:59:59' "
            sql &= " group by a.report_dept_id ,  b.dept_name_en "
            sql &= " ORDER BY COUNT(a.report_dept_id) DESC"
            '  Response.Write(sql)
            '   Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            GripSSIP2.DataSource = ds
            GripSSIP2.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try

    End Sub

    Protected Sub GridNews_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridNews.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButton1 As LinkButton = CType(e.Row.FindControl("LinkButton1"), LinkButton)
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)

            If findArrayValue(priv_list, "204") = True Then ' HR
                LinkButton1.Attributes.Add("onclick", "window.open('ssip_news_edit.aspx?mode=edit&id=" & lblPK.Text & "', '', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600');return false;")
            Else
                LinkButton1.Visible = False
            End If

        End If
    End Sub

    Protected Sub GridNews_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridNews.SelectedIndexChanged

    End Sub
End Class
