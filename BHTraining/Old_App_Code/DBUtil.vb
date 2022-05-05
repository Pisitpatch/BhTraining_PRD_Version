Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports Npgsql

Public Class DBUtil
    'Private connStr As String = ConfigurationManager.ConnectionStrings("IEATSqlServer").ConnectionString
    Private dbconn As Object
    Private dbconnPostgres As NpgsqlConnection
    Private dbconnMSAccess As OleDbConnection
    Private dbconnSQLServer As SqlConnection

    Private postgresTransaction As NpgsqlTransaction
    Private sqlserverTransaction As SqlTransaction
    Private msaccessTransaction As OleDbTransaction

    Private postgresReader As NpgsqlDataReader
    Private sqlserverReader As SqlDataReader
    Private accessReader As OleDbDataReader

    Private errMsg As String = ""
    Private rowAffect As Integer = 0
    Private connStr As String
    Private databaseType As String = ""

    Public Sub New(ByVal dbtype As String)
        databaseType = dbtype
    End Sub

    Public ReadOnly Property rowAffectString() As Integer
        Get
            Return rowAffect
        End Get
    End Property

    Public ReadOnly Property errMessage() As String
        Get
            Return errMsg
        End Get
    End Property

    Public Property database() As String
        Get
            Return databaseType
        End Get
        Set(ByVal value As String)
            databaseType = value
        End Set
    End Property

    Public Function setConnection(ByVal str As String) As Boolean
        Try
            If (databaseType = "postgres") Then
                connStr = ConfigurationManager.ConnectionStrings("MyPostGres").ConnectionString
                dbconnPostgres = New NpgsqlConnection(connStr)
                dbconnPostgres.Open()
                dbconn = dbconnPostgres
            ElseIf (databaseType = "sqlserver") Then
                connStr = ConfigurationManager.ConnectionStrings("MySqlServer").ConnectionString
                dbconnSQLServer = New SqlConnection(connStr)
                dbconnSQLServer.Open()
                dbconn = dbconnSQLServer
            Else
                connStr = str
                dbconnMSAccess = New OleDbConnection(str)
                dbconnMSAccess.Open()
                dbconn = dbconnMSAccess
            End If

            Return True

        Catch ex As Exception
            ' response.write(ex.Message)
            errMsg = ex.Message
            Return False


            ' End
        End Try
    End Function

    Public Function setConnection() As Boolean
        Dim result As Boolean = False
        result = setConnection(databaseType)

        Return result
    End Function

    Public Function getSqlConnection() As SqlConnection
        Return dbconnSQLServer
    End Function

    Public Function setConnectionTransaction(ByVal str As String) As Boolean
        Try
            If (databaseType = "postgres") Then
                connStr = ConfigurationManager.ConnectionStrings("MyPostGres").ConnectionString
                dbconnPostgres = New NpgsqlConnection(connStr)
                dbconnPostgres.Open()
                dbconn = dbconnPostgres
                postgresTransaction = dbconnPostgres.BeginTransaction

            ElseIf (databaseType = "sqlserver") Then
                connStr = ConfigurationManager.ConnectionStrings("MySqlServer").ConnectionString
                dbconnSQLServer = New SqlConnection(connStr)
                dbconnSQLServer.Open()
                dbconn = dbconnSQLServer
                sqlserverTransaction = dbconnSQLServer.BeginTransaction("BuilderTransaction")
            Else
                connStr = str
                dbconnMSAccess = New OleDbConnection(str)
                dbconnMSAccess.Open()
                dbconn = dbconnMSAccess
                msaccessTransaction = dbconnMSAccess.BeginTransaction
            End If

            Return True

        Catch ex As Exception
            '  response.write(ex.Message)
            Return False

            errMsg = ex.Message
            ' End
        End Try
    End Function

    Public Function setConnectionForTransaction() As Boolean
        Dim result As Boolean = False
        result = setConnectionTransaction(databaseType)

        Return result
    End Function

    Public Sub startTransactionSQLServer()
        Try
            sqlserverTransaction.Commit()
        Catch ex As Exception

        End Try

        Try
            sqlserverTransaction = dbconnSQLServer.BeginTransaction
        Catch ex As Exception

        End Try
    End Sub


    Public Sub setDBCommit()
        If (databaseType = "postgres") Then
            postgresTransaction.Commit()

        ElseIf (databaseType = "sqlserver") Then
            sqlserverTransaction.Commit()
        Else
            msaccessTransaction.Commit()
        End If
    End Sub

    Public Sub setDBRollback()
        If (databaseType = "postgres") Then
            Try
                postgresTransaction.Rollback()
            Catch ex As Exception

            End Try
        ElseIf (databaseType = "sqlserver") Then
            Try
                sqlserverTransaction.Rollback()
            Catch ex As Exception

            End Try

        Else
            Try
                msaccessTransaction.Rollback()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Public Sub setPostgresCommit()
        postgresTransaction.Commit()
    End Sub

    Public Sub setPostgresRollback()
        Try
            postgresTransaction.Rollback()
        Catch ex As Exception

        End Try

    End Sub

    Public Function getDataSet(ByVal sql As String, ByVal name1 As String) As DataSet
        Dim ds As New DataSet
        Try

            If (databaseType = "postgres") Then
                Dim da1 As New NpgsqlDataAdapter(sql, dbconnPostgres)
                da1.Fill(ds, name1)
                da1.Dispose()
            ElseIf (databaseType = "sqlserver") Then
                Dim da1 As New SqlDataAdapter(sql, dbconnSQLServer)

                da1.Fill(ds, name1)
                da1.Dispose()
            Else
                Dim da1 As New OleDbDataAdapter(sql, dbconnMSAccess)
                da1.Fill(ds, name1)
                da1.Dispose()
            End If


        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Return ds
    End Function

    Public Function getDataSetForTransaction(ByVal sql As String, ByVal name1 As String) As DataSet
        Dim ds As New DataSet
        Try

            If (databaseType = "postgres") Then
                Dim da1 As New NpgsqlDataAdapter(sql, dbconnPostgres)
                da1.Fill(ds, name1)
                da1.Dispose()
            ElseIf (databaseType = "sqlserver") Then
                Dim da1 As New SqlDataAdapter(sql, dbconnSQLServer)
                da1.SelectCommand.CommandTimeout = 600
                da1.SelectCommand.Transaction = sqlserverTransaction
                da1.Fill(ds, name1)
                da1.Dispose()
            Else
                Dim da1 As New OleDbDataAdapter(sql, dbconnMSAccess)
                da1.Fill(ds, name1)
                da1.Dispose()
            End If


        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Return ds
    End Function


    Public Function getDataReader(ByVal sql As String, ByVal name1 As String) As Object
        Dim ds As Object


        Try

            If (databaseType = "postgres") Then
                Dim sa1 As New NpgsqlCommand(sql, dbconnPostgres)
                ds = sa1.ExecuteReader(CommandBehavior.CloseConnection)
            ElseIf (databaseType = "sqlserver") Then
                Dim sa1 As New SqlCommand(sql, dbconnSQLServer)
                ds = sa1.ExecuteReader()
                sa1.Dispose()
                sa1 = Nothing

            Else
                Dim sa1 As New OleDbCommand(sql, dbconnMSAccess)
                ds = sa1.ExecuteReader(CommandBehavior.CloseConnection)
            End If


        Catch ex As Exception
            errMsg = "getDataReader22 :: " & ex.Message
        End Try
        Return ds
    End Function

    Public Function getDataReaderForTransaction(ByVal sql As String, ByVal name1 As String) As Object
        Dim ds As Object


        Try

            If (databaseType = "postgres") Then
                Dim sa1 As New NpgsqlCommand(sql, dbconnPostgres)
                ds = sa1.ExecuteReader(CommandBehavior.CloseConnection)
            ElseIf (databaseType = "sqlserver") Then
                Dim sa1 As New SqlCommand(sql, dbconnSQLServer, sqlserverTransaction)
                ds = sa1.ExecuteReader()
                sa1.Dispose()
                sa1 = Nothing

            Else
                Dim sa1 As New OleDbCommand(sql, dbconnMSAccess)
                ds = sa1.ExecuteReader(CommandBehavior.CloseConnection)
            End If


        Catch ex As Exception
            errMsg = "getDataReaderForTransaction :: " & ex.Message
        End Try
        Return ds
    End Function

    Public Function executeSQL(ByVal sql As String) As String
        Dim result As String = ""
        Dim sqlComm As Object
        errMsg = ""
        If dbconn.State = ConnectionState.Closed Then
            dbconn = New OleDbConnection(connStr)
            dbconn.Open()
        End If

        'sqlComm = New OleDbCommand(sql, dbconn)
        If (databaseType = "postgres") Then
            sqlComm = New NpgsqlCommand(sql, dbconn)
        ElseIf (databaseType = "sqlserver") Then
            sqlComm = New SqlCommand(sql, dbconnSQLServer)
        Else
            sqlComm = New OleDbCommand(sql, dbconn)
        End If

        '  sqlComm().ExecuteReader().GetSchemaTable()

        Try
            rowAffect = sqlComm.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
            result = ex.Message
            sqlComm.Dispose()
        End Try

        Return result
    End Function

    Public Function executeSQLForTransaction(ByVal sql As String) As String
        Dim result As String = ""
        Dim sqlComm As Object


        errMsg = ""
        If dbconn.State = ConnectionState.Closed Then
            dbconn = New OleDbConnection(connStr)
            dbconn.Open()
        End If

        'sqlComm = New OleDbCommand(sql, dbconn)
        If (databaseType = "postgres") Then
            sqlComm = New NpgsqlCommand(sql, dbconn)
        ElseIf (databaseType = "sqlserver") Then
            sqlComm = New SqlCommand(sql, dbconnSQLServer, sqlserverTransaction)

        Else
            sqlComm = New OleDbCommand(sql, dbconn)
        End If

        '  sqlComm().ExecuteReader().GetSchemaTable()

        Try
            rowAffect = sqlComm.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
            result = ex.Message
            sqlComm.Dispose()
        End Try

        Return result
    End Function

    Public Function executeScalarForTransaction(ByVal sql As String) As Int32
        Dim result As String = ""
        Dim sqlComm As Object
        Dim newPk As Int32 = -100

        errMsg = ""
        If dbconn.State = ConnectionState.Closed Then
            dbconn = New OleDbConnection(connStr)
            dbconn.Open()
        End If

        'sqlComm = New OleDbCommand(sql, dbconn)
        If (databaseType = "postgres") Then
            sqlComm = New NpgsqlCommand(sql, dbconn)
        ElseIf (databaseType = "sqlserver") Then
            sqlComm = New SqlCommand(sql, dbconnSQLServer, sqlserverTransaction)

        Else
            sqlComm = New OleDbCommand(sql, dbconn)
        End If

        '  sqlComm().ExecuteReader().GetSchemaTable()

        Try
            newPk = Convert.ToInt32(sqlComm.ExecuteScalar())
        Catch ex As Exception
            newPk = -99
            errMsg = ex.Message
            result = ex.Message
            sqlComm.Dispose()
        End Try

        Return newPk
    End Function


    Public Function executeSP(ByVal procedure_name As String) As String
        Dim result As String = ""
        Dim sqlComm As SqlCommand
        errMsg = ""
        If dbconn.State = ConnectionState.Closed Then
            dbconn = New OleDbConnection(connStr)
            dbconn.Open()
        End If

        Try
            sqlComm = New SqlCommand(procedure_name, dbconnSQLServer)
            sqlComm.CommandType = CommandType.StoredProcedure


            rowAffect = sqlComm.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
            result = ex.Message

        End Try

        Return result
    End Function


    Public Sub closeSql()
        If (dbconn.State <> ConnectionState.Closed) Then
            dbconn.Close()
        End If

        Try
            If (dbconnPostgres.State <> ConnectionState.Closed) Then
                dbconnPostgres.Close()
            End If
        Catch ex As NullReferenceException
        End Try

        Try
            If (dbconnSQLServer.State <> ConnectionState.Closed) Then
                dbconnSQLServer.Close()
            End If
        Catch ex As NullReferenceException
        End Try

        Try
            If (dbconnMSAccess.State <> ConnectionState.Closed) Then
                dbconnMSAccess.Close()
            End If
        Catch ex As NullReferenceException
        End Try

        Try
            sqlserverTransaction.Dispose()
        Catch ex As Exception

        End Try

        dbconn.Dispose()
        dbconnSQLServer.Dispose()

        dbconn = Nothing
        dbconnPostgres = Nothing
        dbconnMSAccess = Nothing
        dbconnSQLServer = Nothing



        ' sqlserverTransaction.Dispose()
        postgresTransaction = Nothing
        sqlserverTransaction = Nothing
        msaccessTransaction = Nothing



        Try
            postgresReader.Close()
        Catch ex As Exception

        End Try

        Try
            sqlserverReader.Close()
        Catch ex As Exception

        End Try

        Try
            accessReader.Close()
        Catch ex As Exception

        End Try

        postgresReader = Nothing
        sqlserverReader = Nothing
        accessReader = Nothing


    End Sub


    Protected Overrides Sub Finalize()

    End Sub
End Class
