Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class DivisionService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function getDivision(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim responses As New List(Of String)()
        Try
            Dim sql As String = "SELECT * FROM user_division WHERE LOWER(division_name_th) LIKE '%" & prefixText.ToLower & "%' OR LOWER(division_name_en) LIKE '%" & prefixText.ToLower & "%'"
            sql &= " ORDER BY division_name_en ASC"
            Dim connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)

            connection.Open()
            Dim da As New SqlDataAdapter(sql, connection)
            Dim ds As New DataSet
            da.Fill(ds, "t1")



            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                'items.SetValue(dr("Country_Name").ToString(), i)
                responses.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(ds.Tables(0).Rows(i)("division_name_en").ToString, ds.Tables(0).Rows(i)("division_id").ToString))

            Next i

            connection.Close()
        Catch ex As Exception
            responses.Add(ex.Message)
        End Try

        Return responses.ToArray

    End Function

End Class
