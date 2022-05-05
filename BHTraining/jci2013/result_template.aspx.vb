Imports System.Data
Imports ShareFunction
Imports System.IO
Imports System.Drawing

Partial Class jci2013_result_template
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""
    Protected empcode As String = ""
    Protected type As String = ""
    Protected ts As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        id = Request.QueryString("id")
        empcode = Request.QueryString("empcode")
        type = Request.QueryString("type")
        ts = Request.QueryString("ts")

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If

        If Page.IsPostBack Then

        Else ' load first time
            bindForm()
            bindGridMEList()
            bindGridMEList2()
            bindGridMEList3()

            'Return
            Response.Clear()
            Response.Buffer = True
            Response.ClearContent()
            Response.Charset = "Windows-874"
            'Response.Charset = "utf-8"
            ' Response.ContentEncoding = System.Text.Encoding.UTF8
            ' Response.ContentEncoding = System.Text.Encoding.ASCII
            Me.EnableViewState = False
            Response.AddHeader("content-disposition", "attachment;filename=jci_result.doc")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/msword"
          

            Response.Flush()
            '  Response.[End]()
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub SetContentType(request As System.Web.HttpRequest, response As System.Web.HttpResponse, reportName As [String])
        If reportName.Length > 128 Then
            reportName = reportName.Substring(0, 128)
        End If
        Dim encodedFilename As String = reportName.Replace(";"c, " "c)

        If request.Browser.Browser.Contains("IE") Then
            ' Replace the %20 to obtain a clean name when saving the file from Word.
            encodedFilename = Uri.EscapeDataString(Path.GetFileNameWithoutExtension(encodedFilename)).Replace("%20", " ") + Path.GetExtension(encodedFilename)
        End If

        ' use the correct content-type (not 'application/msword') or FF will append .doc to the filename
        response.AppendHeader("Content-Disposition", "attachment;filename=""" & encodedFilename & """")
        If reportName.EndsWith(".docx") Then
            response.ContentType = "application/vnd.ms-word.document"
        Else
            response.ContentType = "application/vnd.ms-word.template"
        End If

        ' IE cannot download an MS Office document from a website using SSL if the response
        ' contains HTTP headers such as:   Pragram: no-cache   and/or    Cache-controo: no-cache,max-age=0,must-revalidate
        ' http://support.microsoft.com/kb/316431/
        If Not (request.IsSecureConnection AndAlso request.Browser.Browser.Contains("IE")) Then
            response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
        End If
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * from jci13_assessment_list WHERE 1 = 1 "
            If type = "ipad" Then
                ' sql &= "AND ipad_assessment_id = " & id
                sql &= " AND ipad_timestamp = " & ts
            Else
                sql &= "AND assessment_id = " & id
            End If

            'sql &= " AND emp_code = " & empcode
            '   sql &= "  ORDER BY doctor_name_en "
            ds = conn.getDataSetForTransaction(sql, "t1")

            lblDept.Text = ds.Tables("t1").Rows(0)("dept_name").ToString
            lblType.Text = ds.Tables("t1").Rows(0)("type_name").ToString

            lblDate.Text = ds.Tables("t1").Rows(0)("assessment_date_str").ToString
            lblTime.Text = ds.Tables("t1").Rows(0)("assessment_time_str").ToString
            lblLocation.Text = ds.Tables("t1").Rows(0)("location_name").ToString
            lblBuilding.Text = ds.Tables("t1").Rows(0)("building_name").ToString
            lblMember.Text = ds.Tables("t1").Rows(0)("member").ToString
            lblLeader.Text = ds.Tables("t1").Rows(0)("emp_name").ToString

            lblImpression.Text = ds.Tables("t1").Rows(0)("impression").ToString
        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridMEList()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_assessment_me_list a INNER JOIN jci13_std_select b ON a.me_id = b.me_id "
            sql &= " WHERE 1 = 1 "
            ' sql &= " AND a.assessment_id = " & id
            If type = "ipad" Then

                sql &= "AND a.assessment_id IN (SELECT assessment_id from jci13_assessment_list "
                sql &= " WHERE ipad_assessment_id = " & id & " AND ipad_timestamp = " & ts & ")"

            Else
                sql &= "AND a.assessment_id = " & id
            End If

            sql &= " AND a.me_score_level = 10 "
            '  Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables(0).Rows.Count = 0 Then

                ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
                GridView1.DataSource = ds
                GridView1.DataBind()

                Dim columnCount As Integer = GridView1.Rows(0).Cells.Count
                GridView1.Rows(0).Cells.Clear()
                GridView1.Rows(0).Cells.Add(New TableCell)
                GridView1.Rows(0).Cells(0).ColumnSpan = columnCount
                GridView1.Rows(0).Cells(0).Text = "No Records Found."
            Else
                GridView1.DataSource = ds
                GridView1.DataBind()
            End If

           

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridMEList2()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_assessment_me_list a INNER JOIN jci13_std_select b ON a.me_id = b.me_id "
            sql &= " WHERE 1 = 1 "
            ' sql &= " AND a.assessment_id = " & id
            If type = "ipad" Then
                sql &= "AND a.assessment_id IN (SELECT assessment_id from jci13_assessment_list "
                sql &= " WHERE ipad_assessment_id = " & id & " AND ipad_timestamp = " & ts & ")"
            Else
                sql &= "AND a.assessment_id = " & id
            End If
            ' sql &= " AND a.emp_code = " & empcode
            sql &= " AND a.me_score_level = 5 "
            '  Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables(0).Rows.Count = 0 Then

                ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
                GridView2.DataSource = ds
                GridView2.DataBind()

                Dim columnCount As Integer = GridView2.Rows(0).Cells.Count
                GridView2.Rows(0).Cells.Clear()
                GridView2.Rows(0).Cells.Add(New TableCell)
                GridView2.Rows(0).Cells(0).ColumnSpan = columnCount
                GridView2.Rows(0).Cells(0).Text = "No Records Found."
            Else
                GridView2.DataSource = ds
                GridView2.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridMEList3()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * "

            sql &= " FROM jci13_assessment_me_list a INNER JOIN jci13_std_select b ON a.me_id = b.me_id "
            sql &= " WHERE 1 = 1 "
            ' sql &= " AND a.assessment_id = " & id
            If type = "ipad" Then
                sql &= "AND a.assessment_id IN (SELECT assessment_id from jci13_assessment_list "
                sql &= " WHERE ipad_assessment_id = " & id & " AND ipad_timestamp = " & ts & ")"
            Else
                sql &= "AND a.assessment_id = " & id
            End If
            ' sql &= " AND a.emp_code = " & empcode
            sql &= " AND a.me_score_level = 0 "
            '  Response.Write(sql)
            'Return
            ds = conn.getDataSetForTransaction(sql, "t1")

            If ds.Tables(0).Rows.Count = 0 Then

                ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
                GridView3.DataSource = ds
                GridView3.DataBind()

                Dim columnCount As Integer = GridView3.Rows(0).Cells.Count
                GridView3.Rows(0).Cells.Clear()
                GridView3.Rows(0).Cells.Add(New TableCell)
                GridView3.Rows(0).Cells(0).ColumnSpan = columnCount
                GridView3.Rows(0).Cells(0).Text = "No Records Found."
            Else
                GridView3.DataSource = ds
                GridView3.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message & sql)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
         
            Dim lblPK As Label
            Dim lblPicture As Label
            Dim lblIpadPK As Label
            Dim sql As String
            Dim ds As New DataSet
            Dim strURL As String = "http://" & ConfigurationManager.AppSettings("frontHost").ToString & "/"


            lblPK = CType(e.Row.FindControl("lblPK"), Label)
            lblIpadPK = CType(e.Row.FindControl("lblIpadPK"), Label)
            lblPicture = CType(e.Row.FindControl("lblPicture"), Label)

            lblPicture.Text = ""

            If lblPK.Text = "" Then
                Return
            End If

            Try
                sql = "SELECT * FROM jci13_assessment_picture_list a INNER JOIN jci13_assessment_me_list b ON a.ipad_assessment_me_id = b.ipad_assessment_me_id WHERE b.assessment_me_id = " & lblPK.Text
                If lblIpadPK.Text <> "" Then
                    sql &= " AND a.ipad_assessment_me_id = " & lblIpadPK.Text
                Else
                    sql &= " AND  1 > 2 "
                End If

                If type = "ipad" Then
                    sql &= "  AND a.ipad_timestamp =  " & ts
                End If
                'Response.Write(sql)
                '  Return
                ds = conn.getDataSetForTransaction(sql, "t1")
                ' Return
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblPicture.Text &= "<img width='150' height='150' src='" & strURL & "share/jci/pdf/" & ds.Tables("t1").Rows(i)("picture_name").ToString & "' />" & " "
                Next i

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label
            Dim lblPicture As Label
            Dim lblIpadPK As Label
            Dim sql As String
            Dim ds As New DataSet
            Dim strURL As String = "http://" & ConfigurationManager.AppSettings("frontHost").ToString & "/"

            lblPK = CType(e.Row.FindControl("lblPK"), Label)
            lblIpadPK = CType(e.Row.FindControl("lblIpadPK"), Label)
            lblPicture = CType(e.Row.FindControl("lblPicture"), Label)

            lblPicture.Text = ""

            If lblPK.Text = "" Then
                Return
            End If

            Try
                sql = "SELECT * FROM jci13_assessment_picture_list a INNER JOIN jci13_assessment_me_list b ON a.ipad_assessment_me_id = b.ipad_assessment_me_id WHERE b.assessment_me_id = " & lblPK.Text
                If lblIpadPK.Text <> "" Then
                    sql &= " AND a.ipad_assessment_me_id = " & lblIpadPK.Text
                Else
                    sql &= " AND  1 > 2 "
                End If

                If type = "ipad" Then
                    sql &= "  AND a.ipad_timestamp =  " & ts
                End If

                'Response.Write(sql)
                '  Return
                ds = conn.getDataSetForTransaction(sql, "t1")
                ' Return
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblPicture.Text &= "<img width='150' height='150'  src='" & strURL & "share/jci/pdf/" & ds.Tables("t1").Rows(i)("picture_name").ToString & "' />" & " "
                Next i

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged

    End Sub

    Protected Sub GridView3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView3.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblPK As Label
            Dim lblPicture As Label
            Dim lblIpadPK As Label
            Dim sql As String
            Dim ds As New DataSet
            Dim strURL As String = "http://" & ConfigurationManager.AppSettings("frontHost").ToString & "/"

            lblPK = CType(e.Row.FindControl("lblPK"), Label)
            lblIpadPK = CType(e.Row.FindControl("lblIpadPK"), Label)
            lblPicture = CType(e.Row.FindControl("lblPicture"), Label)

            lblPicture.Text = ""

            If lblPK.Text = "" Then
                Return
            End If

            Try
                sql = "SELECT * FROM jci13_assessment_picture_list a INNER JOIN jci13_assessment_me_list b ON a.ipad_assessment_me_id = b.ipad_assessment_me_id WHERE b.assessment_me_id = " & lblPK.Text
                If lblIpadPK.Text <> "" Then
                    sql &= " AND a.ipad_assessment_me_id = " & lblIpadPK.Text
                Else
                    sql &= " AND  1 > 2 "
                End If

                If type = "ipad" Then
                    sql &= "  AND a.ipad_timestamp =  " & ts
                End If
                'Response.Write(sql)
                '  Return
                ds = conn.getDataSetForTransaction(sql, "t1")
                ' Return
                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    lblPicture.Text &= "<img  height='150' src='" & strURL & "share/jci/pdf/" & ds.Tables("t1").Rows(i)("picture_name").ToString & "' />" & " "
                Next i

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End If
    End Sub

    Protected Sub GridView3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView3.SelectedIndexChanged

    End Sub
End Class
