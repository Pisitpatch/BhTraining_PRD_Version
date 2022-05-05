Imports System.Text
Imports System.Collections
Imports System.DirectoryServices

Public Class LdapAuthentication
    Dim _path As String
    Dim _filterAttribute As String
    Dim _email As String
    Dim _displayName As String
    Dim _department As String

    Public Sub New(ByVal path As String)
        _path = path
    End Sub

    Public ReadOnly Property email() As [String]
        Get
            email = _email
        End Get
    End Property

    Public ReadOnly Property displayName() As [String]
        Get
            displayName = _displayName
        End Get
    End Property

    Public ReadOnly Property department() As [String]
        Get
            department = _displayName
        End Get
    End Property

    Public Function IsAuthenticated(ByVal domain As String, ByVal username As String, ByVal pwd As String) As Boolean
        Dim domainAndUsername As String = domain & "\" & username
        Dim entry As New DirectoryEntry(_path, domainAndUsername, pwd)
        Try
            ' Bind to the native AdsObject to force authentication.
            Dim obj As [Object] = entry.NativeObject
            Dim search As New DirectorySearcher(entry)
            search.Filter = "(SAMAccountName=" & username & ")"
            search.PropertiesToLoad.Add("cn")
            '==> Added by TOP <==
            ' search.PropertiesToLoad.Add("mail")
            'search.PropertiesToLoad.Add("displayName")
            ' search.PropertiesToLoad.Add("department")

            '==> End Added <==
            Dim result As SearchResult = search.FindOne()
            If result Is Nothing Then
                Return False
            End If
            ' Update the new path to the user in the directory
            _path = result.Path
            _filterAttribute = DirectCast(result.Properties("cn")(0), [String])

            ' _email = DirectCast(result.Properties("mail")(0), [String])
            '_displayName = DirectCast(result.Properties("displayName")(0), [String])
            ' _department = DirectCast(result.Properties("department")(0), [String])

        Catch ex As Exception
            Throw New Exception("Error authenticating user. " + ex.Message)
        End Try
        Return True
    End Function



End Class
