Imports System.Data
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO

Namespace QueryStringEncryption
    Public Class Cryptography
#Region "Fields"

        Private Shared key As Byte() = {}
        Private Shared IV As Byte() = {38, 55, 206, 48, 28, 64, _
         20, 16}
        Private Shared stringKey As String = "!5663a#KN"

#End Region

#Region "Public Methods"

        Public Shared Function Encrypt(ByVal text As String) As String
            Try
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8))

                Dim des As New DESCryptoServiceProvider()
                Dim byteArray As [Byte]() = Encoding.UTF8.GetBytes(text)

                Dim memoryStream As New MemoryStream()
                Dim cryptoStream As New CryptoStream(memoryStream, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)

                cryptoStream.Write(byteArray, 0, byteArray.Length)
                cryptoStream.FlushFinalBlock()

                Return Convert.ToBase64String(memoryStream.ToArray())
                ' Handle Exception Here
            Catch ex As Exception
            End Try

            Return String.Empty
        End Function

        Public Shared Function Decrypt(ByVal text As String) As String
            Try
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8))

                Dim des As New DESCryptoServiceProvider()
                Dim byteArray As [Byte]() = Convert.FromBase64String(text)

                Dim memoryStream As New MemoryStream()
                Dim cryptoStream As New CryptoStream(memoryStream, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)

                cryptoStream.Write(byteArray, 0, byteArray.Length)
                cryptoStream.FlushFinalBlock()

                Return Encoding.UTF8.GetString(memoryStream.ToArray())
                ' Handle Exception Here
            Catch ex As Exception
            End Try

            Return String.Empty
        End Function

        Public Shared Function MD5(ByVal strString As String) As String
            Dim ASCIIenc As New ASCIIEncoding
            Dim strReturn As String = ""
            Dim ByteSourceText() As Byte = ASCIIenc.GetBytes(strString)
            Dim Md5Hash As New MD5CryptoServiceProvider
            Dim ByteHash() As Byte = Md5Hash.ComputeHash(ByteSourceText)
            For Each b As Byte In ByteHash
                strReturn = strReturn & b.ToString("x2")
            Next
            Return strReturn
        End Function

#End Region
    End Class
End Namespace
