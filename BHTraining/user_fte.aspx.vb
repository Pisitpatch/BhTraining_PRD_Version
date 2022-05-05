Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Data.OleDb

Imports ShareFunction

Partial Class user_fte
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Private id As String = "0"
    Private mode As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If


        'Dim sql As String
        'Dim errorMsg As String
        'sql = "DELETE FROM dbo.user_fte"
        'Try
        '    errorMsg = conn.executeSQL(sql)
        '    If errorMsg <> "" Then
        '        Throw New Exception(errorMsg)
        '    End If
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try

        id = Request.QueryString("id")
        mode = Request.QueryString("mode")

        If Not Page.IsPostBack Then
            cmdUpdate.Visible = False
            If mode = "edit" Then

            End If

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

    Protected Sub cmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        If Not IsNothing(myFile1.PostedFile) Then

            Dim tempDs As New DataSet
            Dim filePath As String = "\share\fte\FTE.xls"
            Dim i As Integer

            If myFile1.FileName = "" Then
                lblMsg.Text = "Please select fte file to upload."
                Return
            Else
                lblMsg.Text = ""
            End If

            filePath = "share/fte/" & myFile1.FileName
            '*** Save Images ***'
            myFile1.PostedFile.SaveAs(Server.MapPath(filePath))

            ' Dim connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & "C:\SoftwareDevelop\vbproject\BHOnline\share\fte\FTE.xls" & ";Extended Properties=Excel 8.0"
            Dim connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Server.MapPath(filePath) & ";Extended Properties=Excel 8.0"


            ' Create the connection object
            Dim oledbConn As OleDbConnection = New OleDbConnection(connString)
            Try
                ' Open connection
                oledbConn.Open()

                ' Create OleDbCommand object and select data from worksheet Sheet1
                Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn)

                ' Create new OleDbDataAdapter
                Dim oleda As OleDbDataAdapter = New OleDbDataAdapter()

                oleda.SelectCommand = cmd

                ' Create a DataSet which will hold the data extracted from the worksheet.
                Dim ds As DataSet = New DataSet()

                ' Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(ds, "Employees")

                'Response.Write(ds.Tables(0).Rows.Count)
                ' Bind the data to the GridView
                updateToDatabase(ds)

                GridView1.DataSource = ds
                GridView1.DataBind()


                cmdUpdate.Visible = True
                '  ds.Clear()
                ' ds = Nothing
            Catch ex As Exception
                ' Response.Write(ex.Message)
                lblMsg.Text = ex.Message
            Finally

                ' Close connection
                oledbConn.Close()
            End Try

        End If
    End Sub


    Sub updateToDatabase()
        Dim ds1 As New DataSet
        Dim tempDs As New DataSet
        Dim sql As String
        Dim i As Int32
        Dim errorMsg As String

        Dim filePath As String
        filePath = "share/fte/" & "FTE.xls"

        Dim connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Server.MapPath(filePath) & ";Extended Properties=Excel 8.0"
        ' Create the connection object
        Dim oledbConn As OleDbConnection = New OleDbConnection(connString)

        Try

            oledbConn.Open()
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn)
            Dim oleda As OleDbDataAdapter = New OleDbDataAdapter()
            oleda.SelectCommand = cmd
            oleda.Fill(tempDs, "Employees")



            sql = "DELETE FROM user_fte"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If


            i = tempDs.Tables(0).Rows.Count

            For iRow As Int32 = 0 To i - 1
                ' Response.Write(tempDs.Tables(0).Rows(iRow)(7).ToString)
                sql = "INSERT INTO user_fte (cost_center,dept_name,emp_name,emp_name_local,emp_no,age,company_name,dob,emp_type,emp_status,hire_date,job_type,job_title,sex,year_service,hn,termination_date,national_id"

                sql &= ") VALUES("
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(0).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(1).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(2).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(3).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(4).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(5).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(6).ToString & "' , "
                sql &= " '" & convertToSQLDatetime(tempDs.Tables(0).Rows(iRow)(7).ToString) & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(8).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(9).ToString & "' , "
                sql &= " '" & convertToSQLDatetime(tempDs.Tables(0).Rows(iRow)(10).ToString) & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(11).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(12).ToString.Replace("'", "''") & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(13).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(14).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(15).ToString & "' , "
                sql &= " '" & convertToSQLDatetime(tempDs.Tables(0).Rows(iRow)(16).ToString) & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(17).ToString & "'  "

                sql &= ")"

                'sql = sql.Replace("'", "\'")
                Try
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                    Response.Write("<hr/>")
                End Try


            Next iRow

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(sql & ex.Message & "<hr/>")
        Finally
            oledbConn.Close()
        End Try

    End Sub

    Sub updateToDatabase(ByVal tempDs As DataSet)
        Dim ds1 As New DataSet

        Dim sql As String
        Dim i As Int32
        Dim errorMsg As String



        Try



            sql = "DELETE FROM user_fte"
            errorMsg = conn.executeSQLForTransaction(sql)
            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If


            i = tempDs.Tables(0).Rows.Count

            For iRow As Int32 = 0 To i - 1
                ' Response.Write(tempDs.Tables(0).Rows(iRow)(7).ToString)
                sql = "INSERT INTO user_fte (cost_center,dept_name,emp_name,emp_name_local,emp_no,age,company_name,dob,emp_type,emp_status,hire_date,job_type,job_title,sex,year_service,hn,termination_date,national_id"

                sql &= ") VALUES("
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(0).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(1).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(2).ToString & "' , " ' emp_name
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(3).ToString & "' , " ' emp_name_local
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(4).ToString & "' , " ' emp_no
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(5).ToString & "' , " ' age
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(6).ToString & "' , " ' company_name
                sql &= " '" & convertToSQLDatetime(tempDs.Tables(0).Rows(iRow)(7).ToString) & "' , " ' dob
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(8).ToString & "' , " ' emp_type
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(9).ToString & "' , "
                sql &= " '" & convertToSQLDatetime(tempDs.Tables(0).Rows(iRow)(10).ToString) & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(11).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(12).ToString.Replace("'", "''") & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(13).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(14).ToString & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(15).ToString & "' , "
                sql &= " '" & convertToSQLDatetime(tempDs.Tables(0).Rows(iRow)(16).ToString) & "' , "
                sql &= " '" & tempDs.Tables(0).Rows(iRow)(17).ToString & "'  "

                sql &= ")"

                'sql = sql.Replace("'", "\'")
                Try
                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                Catch ex As Exception
                    ' Response.Write(ex.Message)
                    '  Response.Write("<hr/>")
                    lblMsg.Text = tempDs.Tables(0).Rows(iRow)(2).ToString & " can't import becauase :: " & vbCrLf & ex.Message & vbCrLf
                End Try


            Next iRow

            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback()
            Response.Write(sql & ex.Message & "<hr/>")
        Finally

        End Try

    End Sub

    Protected Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        'updateToDatabase()
        Dim sql As String
        Dim ds As New DataSet
        Dim errorMsg As String = ""
        Dim connLocal As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

        Try

            If connLocal.setConnection Then
            Else
                Throw New Exception("Connecton ERROR")

            End If
            'sql = "MERGE INTO user_profile AS d"
            'sql &= " USING user_fte AS s ON d.emp_code = s.emp_no"
            'sql &= " WHEN MATCHED THEN "
            'sql &= " UPDATE SET d.user_fullname = s.emp_name , d.hire_date = s.hire_date"
            'sql &= " WHEN NOT MATCHED THEN"
            'sql &= " INSERT (emp_code,user_fullname,dept_name,costcenter_id,emp_type,job_type,job_title,sex,year_service,hn,termination_date,hire_date)"
            'sql &= " VALUES( s.emp_no , s.emp_name , s.dept_name,s.cost_center,s.emp_type,s.job_type,s.job_title,s.sex,s.year_service,s.hn,s.termination_date,s.hire_date"
            'sql &= ")"

            sql = "mergeUser"

            errorMsg = connLocal.executeSP(sql)

            If errorMsg <> "" Then
                Throw New Exception(errorMsg)
            End If

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            lblMsg.Text = "Synchonize data is completed"
            ' conn.setDBCommit()
        Catch ex As Exception
            ' conn.setDBRollback()
            ' Response.Write(sql)
            ' Response.Write(ex.Message)
            lblMsg.Text = ex.Message
        Finally
            connLocal.closeSql()
            connLocal = Nothing
        End Try
    End Sub
End Class
