Imports System.Data
Imports System.IO
Imports ShareFunction


Partial Class srp_srp_message
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected priv_list() As String
    Protected global_unit_num As String = ""

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

        If Not Page.IsPostBack Then
            bindDateRange()
            txtdate1.SelectedIndex = txtdate1.Items.Count - 1
            ' txtdate2.SelectedIndex = txtdate2.Items.Count - 1
        End If


        bindMessage(1, lblInnovation)
        bindGrid()
        bindGridSubmitStar()
        bindBIWayGraph()
        ' bindBannerOTS()
        bindBannerOTS2()
        bindDeptCoast()
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

    Sub bindBannerOTS()
        Dim sql As String
        Dim ds As New DataSet
        Dim html As String = ""
        Try
            sql = "SELECT * FROM srp_banner WHERE is_active = 1 "
            ds = conn.getDataSetForTransaction(sql, "t1")

            html &= "<div class='rslides_container'>"
            html &= "<ul class='rslides' id='slider1'>"

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                html &= "<li><img src='../share/ots_card/" & ds.Tables("t1").Rows(i)("banner_path").ToString() & "' alt='" & ds.Tables("t1").Rows(i)("banner_detail").ToString() & "'></li>"
            Next i

            html &= "</ul></div>"

            lblSlideShow.Text = html
        Catch ex As Exception

            lblSlideShow.Text = ""
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindBannerOTS2()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM srp_banner WHERE is_active = 1 "


            ds = conn.getDataSetForTransaction(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            PicturesListView.DataSource = ds
            PicturesListView.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindDateRange()
        Dim sql As String
        Dim ds As New DataSet
        Dim myvalue As String
        Dim mytext As String
        Try
            sql = "SELECT  MONTH(movement_create_date_raw) AS m , YEAR(movement_create_date_raw) AS y FROM srp_point_movement WHERE movememt_type_id = 1 AND ISNULL(movement_create_date_raw,0) > 0 "
            sql &= " GROUP BY MONTH(movement_create_date_raw) , YEAR(movement_create_date_raw)"
            sql &= " ORDER BY YEAR(movement_create_date_raw) ASC , MONTH(movement_create_date_raw) "
            ds = conn.getDataSetForTransaction(sql, "t1")

            For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                myvalue = ds.Tables("t1").Rows(i)("m").ToString & "|" & ds.Tables("t1").Rows(i)("y").ToString
                mytext = getMonthName(CInt(ds.Tables("t1").Rows(i)("m").ToString)) & " " & ds.Tables("t1").Rows(i)("y").ToString
                txtdate1.Items.Add(New ListItem(mytext, myvalue))
                ' txtdate2.Items.Add(New ListItem(mytext, myvalue))
            Next i

            ' Response.Write(sql)

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
            sql = "SELECT * , CONVERT(VARCHAR(10),new_date,103) AS new_date_format FROM srp_news WHERE ISNULL(is_delete,0) = 0 AND is_active = 1 ORDER BY new_date_ts DESC "
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridNews.DataSource = ds
            GridNews.DataBind()

            DataList1.DataSource = ds
            DataList1.DataBind()
        Catch ex As Exception
            Response.Write("xxxx" & ex.Message)
        End Try
    End Sub

    Sub bindMessage(ByVal pk As String, ByVal lb As Label)
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM srp_message WHERE srp_message_id = " & 1
            ds = conn.getDataSetForTransaction(sql, "t1")

            lb.Text = ds.Tables("t1").Rows(0)("message_detail").ToString

            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lb.Text &= "<p>&nbsp;</p><img src='../share/star/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' /><br/><br/>"
            End If



        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

   

    Sub bindDeptCoast()
        Dim sql As String
        Dim ds As New DataSet
        Dim start_month As Array

        start_month = txtdate1.SelectedValue.Split("|")
        Try
            sql = "select  b.dept_name "
            sql &= " , SUM(CASE WHEN r_2015_1 =1 THEN 1 ELSE 0 END) as c1 , SUM(CASE WHEN r_2015_2 =1 THEN 1 ELSE 0 END) as c2"
            sql &= " , SUM(CASE WHEN r_2015_3 =1 THEN 1 ELSE 0 END) as c3 , SUM(CASE WHEN r_2015_1 =4 THEN 1 ELSE 0 END) as c4"
            sql &= "  , (SUM(CASE WHEN r_2015_1 =1 THEN 1 ELSE 0 END) + SUM(CASE WHEN r_2015_2 =1 THEN 1 ELSE 0 END) "
            sql &= " + SUM(CASE WHEN r_2015_3 =1 THEN 1 ELSE 0 END) + SUM(CASE WHEN r_2015_4 =1 THEN 1 ELSE 0 END)) as total"
            sql &= " from srp_point_movement a inner join user_profile b on a.emp_code = b.emp_code"
            sql &= "             where (movememt_type_id = 1)"
            sql &= " and MONTH(movement_create_date_raw) =" & start_month(0) & " AND YEAR(movement_create_date_raw) = " & start_month(1)
            sql &= " group by b.dept_name"
            sql &= " having (SUM(CASE WHEN r_2015_1 =1 THEN 1 ELSE 0 END) + SUM(CASE WHEN r_2015_2 =1 THEN 1 ELSE 0 END) "
            sql &= " + SUM(CASE WHEN r_2015_3 =1 THEN 1 ELSE 0 END) + SUM(CASE WHEN r_2015_4 =1 THEN 1 ELSE 0 END)) > 0"
            sql &= " order by b.dept_name"
            ds = conn.getDataSetForTransaction(sql, "t1")
            GridCoastDept.DataSource = ds
            GridCoastDept.DataBind()

         
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGridSubmitStar()
        Dim sql As String
        Dim ds As New DataSet

        Dim login_date As Date

        Dim query_date As Date

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)

        Try
          
            sql = "select TOP 10 b.dept_name , COUNT(b.dept_name) AS num , SUM(a.point_trans) AS score "
            sql &= " from srp_point_movement a inner join user_profile b on a.emp_code = b.emp_code"
            sql &= " where(movememt_type_id = 1)"
            sql &= " group by b.dept_name"
            sql &= " order by COUNT(b.dept_name) DESC"

            ds = conn.getDataSetForTransaction(sql, "t1")

            GripStarTopSubmit.DataSource = ds
            GripStarTopSubmit.DataBind()

          

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try

    End Sub


    Sub bindBIWayGraph()
        Dim sql As String
        Dim ds As New DataSet
        Dim iCare As Integer = 0
        Dim iClear As Integer = 0
        Dim iSmart As Integer = 0
        Dim iQuality As Integer = 0

        Dim param(10) As Integer
        Dim login_date As Date
        Dim query_date As Date
        Dim start_month As Array

        start_month = txtdate1.SelectedValue.Split("|")

        login_date = Date.Now
        query_date = DateAdd(DateInterval.Day, -30, login_date)
        Try
            For ii As Integer = 0 To 3

                sql = "select SUM(r_2015_" & (ii + 1) & ") from ("
                sql &= " select sum(case when r_2015_" & (ii + 1) & " = 1 then 1 else 0 end) as r_2015_" & (ii + 1)
                sql &= " from srp_point_movement a "
                sql &= " where movememt_type_id = 1"
                sql &= " and MONTH(movement_create_date_raw) =" & start_month(0) & " AND YEAR(movement_create_date_raw) = " & start_month(1)
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
            For ii As Integer = 0 To 3
                If param(ii) > 0 Then
                    If global_unit_num = "" Then
                        limit = ""
                    Else
                        limit = ","
                    End If
                    global_unit_num &= limit & "['B" & (ii + 1) & "' , " & param(ii) & " ]"
                End If
            Next
           
          
        

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
      

    End Sub

    Protected Sub GridNews_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridNews.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LinkButton1 As LinkButton = CType(e.Row.FindControl("LinkButton1"), LinkButton)
            Dim lblPK As Label = CType(e.Row.FindControl("lblPK"), Label)

            If findArrayValue(priv_list, "402") = True Then ' HR
                LinkButton1.Attributes.Add("onclick", "window.open('srp_news_edit.aspx?mode=edit&id=" & lblPK.Text & "', '', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600');return false;")
            Else
                LinkButton1.Visible = False
            End If

        End If
    End Sub

    Protected Sub GridNews_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridNews.SelectedIndexChanged

    End Sub

    Protected Sub DataList1_ItemDataBound(sender As Object, e As DataListItemEventArgs) Handles DataList1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbl As Label
            lbl = CType(e.Item.FindControl("LabelIcon"), Label)

            If lbl.Text = "1" Then
                lbl.Text = "<img src='../images/new.gif' />"
            Else
                lbl.Text = ""
            End If
        End If
    End Sub
End Class
