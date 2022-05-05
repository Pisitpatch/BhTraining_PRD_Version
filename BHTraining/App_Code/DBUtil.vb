Option Strict On
Option Explicit On

Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports Npgsql


Public Class DBUtil

    'The developer was a VB6 dev and uses little bit the features of .net
    Private _dbconn As System.Data.Common.DbConnection
    Private _dbTransaction As System.Data.Common.DbTransaction
    Private _dbDataReader As System.Data.Common.DbDataReader

    Private errMsg As String = ""
    Private rowAffect As Integer = 0
    Private connStr As String
    Private _databaseTypeName As String = ""

    Sub New(ByVal dbtype As String)
        _databaseTypeName = dbtype
    End Sub

    Public ReadOnly Property RowAffectString() As Integer
        Get
            Return rowAffect
        End Get
    End Property

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return errMsg
        End Get
    End Property

    Public Property DatabaseTypeName() As String
        Get
            Return _databaseTypeName
        End Get
        Set(ByVal value As String)
            _databaseTypeName = value
        End Set
    End Property

    Public Function SetConnection(ByVal str As String) As Boolean
        Try
            If (_databaseTypeName = "postgres") Then
                connStr = ConfigurationManager.ConnectionStrings("MyPostGres").ConnectionString
                _dbconn = New NpgsqlConnection(connStr)
                _dbconn.Open()
            ElseIf (_databaseTypeName = "sqlserver") Then
                connStr = ConfigurationManager.ConnectionStrings("MySqlServer").ConnectionString
                _dbconn = New SqlConnection(connStr)
                _dbconn.Open()
            Else
                connStr = str
                _dbconn = New OleDbConnection(str)
                _dbconn.Open()
            End If
            Return True
        Catch ex As Exception
            errMsg = ex.Message
            Return False
        End Try
    End Function

    Public Function SetConnection() As Boolean
        Return SetConnection(_databaseTypeName)
    End Function

    Public Function GetSqlConnection() As SqlConnection
        Return CType(_dbconn, SqlConnection)
    End Function

    Public Function SetConnectionTransaction(ByVal str As String) As Boolean
        Try
            If (_databaseTypeName = "postgres") Then
                connStr = ConfigurationManager.ConnectionStrings("MyPostGres").ConnectionString
                _dbconn = New NpgsqlConnection(connStr)
                _dbconn.Open()
                _dbTransaction = _dbconn.BeginTransaction

            ElseIf (_databaseTypeName = "sqlserver") Then
                connStr = ConfigurationManager.ConnectionStrings("MySqlServer").ConnectionString
                _dbconn = New SqlConnection(connStr)
                _dbconn.Open()
                _dbTransaction = _dbconn.BeginTransaction()
            Else
                connStr = str
                _dbconn = New OleDbConnection(str)
                _dbconn.Open()
                _dbTransaction = _dbconn.BeginTransaction
            End If
            Return True
        Catch ex As Exception
            errMsg = ex.Message
            Return False
        End Try
    End Function

    Public Function SetConnectionForTransaction() As Boolean
        Return SetConnectionTransaction(_databaseTypeName)
    End Function

    Public Sub StartTransactionSQLServer()
        If (_dbTransaction IsNot Nothing) Then
            _dbTransaction.Commit()
        End If

        _dbTransaction = _dbconn.BeginTransaction
    End Sub

    Public Sub SetDBCommit()
        _dbTransaction.Commit()
    End Sub

    Public Sub SetDBRollback()
        _dbTransaction.Rollback()
    End Sub


    Public Function GetDataSet(ByVal sql As String, ByVal name1 As String) As DataSet
        Dim ds As New DataSet
        Try

            If (_databaseTypeName = "postgres") Then
                Dim da1 As New NpgsqlDataAdapter(sql, CType(_dbconn, NpgsqlConnection))
                da1.Fill(ds, name1)
                da1.Dispose()
            ElseIf (_databaseTypeName = "sqlserver") Then
                Dim da1 As New SqlDataAdapter(sql, CType(_dbconn, SqlConnection))

                da1.Fill(ds, name1)
                da1.Dispose()
            Else
                Dim da1 As New OleDbDataAdapter(sql, CType(_dbconn, OleDbConnection))
                da1.Fill(ds, name1)
                da1.Dispose()
            End If


        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Return ds
    End Function

    Public Function GetDataSetForTransaction(ByVal sql As String, ByVal name1 As String) As DataSet
        Dim ds As New DataSet
        Try

            If (_databaseTypeName = "postgres") Then
                Dim da1 As New NpgsqlDataAdapter(sql, CType(_dbconn, NpgsqlConnection))
                da1.Fill(ds, name1)
                da1.Dispose()
            ElseIf (_databaseTypeName = "sqlserver") Then
                Dim da1 As New SqlDataAdapter(sql, CType(_dbconn, SqlConnection))
                da1.SelectCommand.CommandTimeout = 600
                da1.SelectCommand.Transaction = CType(_dbTransaction, SqlTransaction)
                da1.Fill(ds, name1)
                da1.Dispose()
            Else
                Dim da1 As New OleDbDataAdapter(sql, CType(_dbconn, OleDbConnection))
                da1.Fill(ds, name1)
                da1.Dispose()
            End If


        Catch ex As Exception
            errMsg = ex.Message
        End Try
        Return ds
    End Function


    Public Function GetDataReader(ByVal sql As String, ByVal name1 As String) As Object
        Dim ds As Object
        Try

            If (_databaseTypeName = "postgres") Then
                Dim sa1 As New NpgsqlCommand(sql, CType(_dbconn, NpgsqlConnection))
                ds = sa1.ExecuteReader(CommandBehavior.CloseConnection)
            ElseIf (_databaseTypeName = "sqlserver") Then
                Dim sa1 As New SqlCommand(sql, GetSqlConnection)
                ds = sa1.ExecuteReader()
                sa1.Dispose()
                sa1 = Nothing

            Else
                Dim sa1 As New OleDbCommand(sql, CType(_dbconn, OleDbConnection))
                ds = sa1.ExecuteReader(CommandBehavior.CloseConnection)
            End If


        Catch ex As Exception
            errMsg = "getDataReader22 :: " & ex.Message
        End Try
        Return ds
    End Function

    Public Function GetDataReaderForTransaction(ByVal sql As String, ByVal name1 As String) As Object
        Dim ds As Object
        Try

            If (_databaseTypeName = "postgres") Then
                Dim sa1 As New NpgsqlCommand(sql, CType(_dbconn, NpgsqlConnection))
                ds = sa1.ExecuteReader(CommandBehavior.CloseConnection)
            ElseIf (_databaseTypeName = "sqlserver") Then
                Dim sa1 As New SqlCommand(sql, CType(_dbconn, SqlConnection), CType(_dbTransaction, SqlTransaction))
                ds = sa1.ExecuteReader()
                sa1.Dispose()
                sa1 = Nothing

            Else
                Dim sa1 As New OleDbCommand(sql, CType(_dbconn, OleDbConnection))
                ds = sa1.ExecuteReader(CommandBehavior.CloseConnection)
            End If


        Catch ex As Exception
            errMsg = "getDataReaderForTransaction :: " & ex.Message
        End Try
        Return ds
    End Function

    Public Function ExecuteSQL(ByVal sql As String) As String
        Dim result As String = ""
        Dim sqlComm As System.Data.Common.DbCommand
        errMsg = ""
        If _dbconn.State = ConnectionState.Closed Then
            _dbconn = New OleDbConnection(connStr)
            _dbconn.Open()
        End If

        'sqlComm = New OleDbCommand(sql, dbconn)
        If (_databaseTypeName = "postgres") Then
            sqlComm = New NpgsqlCommand(sql, CType(_dbconn, NpgsqlConnection))
        ElseIf (_databaseTypeName = "sqlserver") Then
            sqlComm = New SqlCommand(sql, CType(_dbconn, SqlConnection))
        Else
            sqlComm = New OleDbCommand(sql, CType(_dbconn, OleDbConnection))
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

    Public Function ExecuteSQLForTransaction(ByVal sql As String) As String
        Dim result As String = ""
        Dim sqlComm As System.Data.Common.DbCommand


        errMsg = ""
        If _dbconn.State = ConnectionState.Closed Then
            _dbconn = New OleDbConnection(connStr)
            _dbconn.Open()
        End If

        'sqlComm = New OleDbCommand(sql, dbconn)
        If (_databaseTypeName = "postgres") Then
            sqlComm = New NpgsqlCommand(sql, CType(_dbconn, NpgsqlConnection))
        ElseIf (_databaseTypeName = "sqlserver") Then
            sqlComm = New SqlCommand(sql, CType(_dbconn, SqlConnection), CType(_dbTransaction, SqlTransaction))

        Else
            sqlComm = New OleDbCommand(sql, CType(_dbconn, OleDbConnection))
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

    Public Function ExecuteScalarForTransaction(ByVal sql As String) As Int32
        Dim result As String = ""
        Dim sqlComm As System.Data.Common.DbCommand
        Dim newPk As Int32 = -100

        errMsg = ""
        If _dbconn.State = ConnectionState.Closed Then
            _dbconn = New OleDbConnection(connStr)
            _dbconn.Open()
        End If

        'sqlComm = New OleDbCommand(sql, dbconn)
        If (_databaseTypeName = "postgres") Then
            sqlComm = New NpgsqlCommand(sql, CType(_dbconn, NpgsqlConnection))
        ElseIf (_databaseTypeName = "sqlserver") Then
            sqlComm = New SqlCommand(sql, CType(_dbconn, SqlConnection), CType(_dbTransaction, SqlTransaction))

        Else
            sqlComm = New OleDbCommand(sql, CType(_dbconn, OleDbConnection))
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


    Public Function ExecuteSP(ByVal procedure_name As String) As String
        Dim result As String = ""
        Dim sqlComm As SqlCommand
        errMsg = ""
        If _dbconn.State = ConnectionState.Closed Then
            _dbconn = New OleDbConnection(connStr)
            _dbconn.Open()
        End If

        Try
            sqlComm = New SqlCommand(procedure_name, CType(_dbconn, SqlConnection))
            sqlComm.CommandType = CommandType.StoredProcedure


            rowAffect = sqlComm.ExecuteNonQuery()
        Catch ex As Exception
            errMsg = ex.Message
            result = ex.Message

        End Try

        Return result
    End Function


    Public Sub CloseSql()
        If (_dbconn IsNot Nothing) AndAlso (_dbconn.State <> ConnectionState.Closed) Then
            _dbconn.Close()
        End If

        If (_dbTransaction IsNot Nothing) Then
            _dbTransaction.Dispose()
        End If

        If (_dbDataReader IsNot Nothing) AndAlso (Not _dbDataReader.IsClosed) Then
            _dbDataReader.Close()
        End If


    End Sub


End Class
