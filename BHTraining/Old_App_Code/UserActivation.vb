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
Imports System.IO

Namespace QueryStringEncryption
    Public NotInheritable Class UserActivation
        Private Sub New()
        End Sub
#Region "Public Methods"

        Public Shared Sub ActivateUser(ByVal key As String)
            Dim username As String = Cryptography.Decrypt(key)

            ' TODO: Activation Login
        End Sub

        Public Shared Function GetActivationLink(ByVal username As String) As String
            Dim key As String = Cryptography.Encrypt(username)

            Dim writer As New StringWriter()

            HttpContext.Current.Server.UrlEncode(key, writer)

            ' Return String.Format("/default.aspx?key={0}", writer.ToString())
            Return writer.ToString
        End Function

#End Region
    End Class
End Namespace
