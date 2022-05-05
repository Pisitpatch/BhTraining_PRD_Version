Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class IDP_TopicService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
      Public Function getTopic(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Dim responses As New List(Of String)()
        Try
            Dim sql As String = "SELECT * FROM idp_m_topic WHERE category_id = " & contextKey & " AND topic_name_en LIKE '%" & prefixText & "%' "
            'sql &= " ORDER BY user_fullname ASC"
            Dim connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)

            connection.Open()
            Dim da As New SqlDataAdapter(sql, connection)
            Dim ds As New DataSet
            da.Fill(ds, "t1")



            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                'items.SetValue(dr("Country_Name").ToString(), i)
                responses.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(ds.Tables(0).Rows(i)("topic_name_en").ToString, ds.Tables(0).Rows(i)("topic_id").ToString))

            Next i

            connection.Close()
        Catch ex As Exception
            responses.Add(ex.Message)
        End Try

        Return responses.ToArray

    End Function


    <WebMethod()> _
    Public Function getExpect(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim responses As New List(Of String)()
        Try
            Dim sql As String = "SELECT * FROM idp_m_expect WHERE  expect_detail LIKE '%" & prefixText & "%' "
            'sql &= " ORDER BY user_fullname ASC"
            Dim connection As New SqlConnection(ConfigurationManager.ConnectionStrings("MySqlServer").ToString)

            connection.Open()
            Dim da As New SqlDataAdapter(sql, connection)
            Dim ds As New DataSet
            da.Fill(ds, "t1")



            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                'items.SetValue(dr("Country_Name").ToString(), i)
                responses.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(ds.Tables(0).Rows(i)("expect_detail").ToString, ds.Tables(0).Rows(i)("expect_detail").ToString))

            Next i

            connection.Close()
        Catch ex As Exception
            responses.Add(ex.Message)
        End Try

        Return responses.ToArray

    End Function
End Class
