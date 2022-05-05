Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports ShareFunction
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ws_course_list
     Inherits System.Web.Services.WebService


    <WebMethod()> _
    Public Function getCourseList() As DataSet
        Dim sql As String
        Dim ds As New DataSet

        sql = "SELECT a.schedule_id , a.idp_id , b.internal_title  , CONVERT(VARCHAR,a.schedule_start,103) + ' ' +  CONVERT(VARCHAR,a.schedule_start,108) AS schedule_start  , CONVERT(VARCHAR,a.schedule_end,103) + ' ' +  CONVERT(VARCHAR,a.schedule_start,108) AS schedule_end , max_attendee , a.location , a.create_by  FROM idp_training_schedule a INNER JOIN idp_external_req b ON a.idp_id = b.idp_id "
        sql &= " INNER JOIN idp_trans_list c ON b.idp_id = c.idp_id "
        sql &= " WHERE ISNULL(a.is_delete , 0) = 0  AND c.status_id > 1 "
        sql &= " AND  ISNULL(c.is_cancel , 0) = 0 "
        sql &= " ORDER BY a.schedule_start , b.internal_title DESC"

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)

            connection.Open()

            Using command As New SqlCommand(sql, connection)

                Using da1 As New SqlDataAdapter(sql, connection)
                    da1.Fill(ds, "t1")
                End Using
                'context.Response.Write(sql)
            End Using

            connection.Close()
            connection.Dispose()
        End Using

        Return ds

    End Function

    <WebMethod()> _
    Public Function getUserList() As DataSet
        Dim sql As String
        Dim ds As New DataSet

        sql = "SELECT emp_code , user_fullname , user_fullname_local , dept_id , dept_name , job_type , job_title FROM user_profile ORDER BY emp_code "

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)

            connection.Open()

            Using command As New SqlCommand(sql, connection)

                Using da1 As New SqlDataAdapter(sql, connection)
                    da1.Fill(ds, "t1")
                End Using
                'context.Response.Write(sql)
            End Using

            connection.Close()
            connection.Dispose()
        End Using

        Return ds

    End Function

    <WebMethod()> _
    Public Function addAttendance(dt As DataTable) As String
        Dim sql As String
        Dim pk As String

        Using connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)

            connection.Open()

            Using sqlserverTransaction As SqlTransaction = connection.BeginTransaction("BuilderTransaction")

                Try
                    For i As Integer = 0 To dt.Rows.Count - 1

                        pk = getPK("register_id", "idp_training_registered", connection, sqlserverTransaction)
                        '  Response.Write(ds.Tables("t1").Rows(0)("user_fullname").ToString)
                        sql = "INSERT INTO idp_training_registered (register_id , schedule_id , idp_id , emp_code , emp_name , dept_id , dept_name , job_title , job_type , create_by , create_date , create_date_ts , register_time , is_register , register_by , attendance_type_id , attendance_type_name , training_hour , is_workhour  ) VALUES("
                        sql &= " '" & pk & "' ,"
                        sql &= " '" & dt.Rows(i)(1).ToString & "' ,"
                        sql &= " '" & dt.Rows(i)(2).ToString & "' ,"
                        sql &= " '" & dt.Rows(i)(3).ToString & "' ," 'emp_code
                        sql &= " '" & dt.Rows(i)(4).ToString & "' ," 'emp_name
                        sql &= " '" & dt.Rows(i)(12).ToString & "' ," ' dept_id
                        sql &= " '" & dt.Rows(i)(13).ToString & "' ," 'dept_name
                        sql &= " '" & dt.Rows(i)(4).ToString & "' ," ' job_title
                        sql &= " '" & dt.Rows(i)(4).ToString & "' ," ' type
                        sql &= " 'Offline Program' ,"
                        sql &= " GETDATE() ,"
                        sql &= " '" & Date.Now.Ticks & "' , "
                        sql &= " '" & dt.Rows(i)(10).ToString & "' , " 'register_time
                        sql &= " '" & 1 & "' , "
                        sql &= " 'Offline Program' ,"
                        sql &= " '" & dt.Rows(i)(11).ToString & "' , "  ' attendance_type_id
                        sql &= " '" & dt.Rows(i)(5).ToString & "' , "  ' attendance_type_name
                        sql &= " '" & dt.Rows(i)(6).ToString & "' ," ' training_hour
                        sql &= " '" & dt.Rows(i)(7).ToString & "'  "  ' is_workhour
                       
                        sql &= ")"


                        Using command As New SqlCommand(sql, connection, sqlserverTransaction)
                            command.ExecuteNonQuery()
                        End Using
                    Next i
                Catch ex As Exception
                    sqlserverTransaction.Rollback()
                    Return ex.Message + sql
                End Try

                sqlserverTransaction.Commit()

            End Using


            connection.Close()
            connection.Dispose()
        End Using

        Return ""

    End Function


    Public Function getPK(ByVal column As String, ByVal table As String, ByVal conn As SqlConnection, ByVal sqlserverTransaction As SqlTransaction) As String
        Dim sql As String
        Dim result As String = ""
        Dim ds As New DataSet
        ' Dim conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Try

            sql = "SELECT ISNULL(MAX(register_id),0) + 1 AS pk FROM idp_training_registered"

            Using da2 As New SqlDataAdapter(sql, conn)
                da2.SelectCommand.CommandTimeout = 600
                da2.SelectCommand.Transaction = sqlserverTransaction
                da2.Fill(ds, "t1")
                result = ds.Tables("t1").Rows(0)("pk").ToString
            End Using
        Catch ex As Exception

            result = ex.Message & " (" & sql & ")"
        Finally
            ds.Clear()
            ds = Nothing
            ' conn.closeSql()
        End Try

        Return result
    End Function
End Class