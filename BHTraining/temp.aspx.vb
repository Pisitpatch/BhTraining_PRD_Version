Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction
Imports System.Net
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html

Partial Class temp
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateMyPDF()
    End Sub



    Private Sub CreateMyPDF()


        Dim doc As New Document(PageSize.A4)


       

        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Request.PhysicalApplicationPath + _
                                 "\share\2.pdf", FileMode.Create))


        Dim EnCodefont As BaseFont = BaseFont.CreateFont(Server.MapPath("font/tahoma.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
        Dim Nfont As New Font(EnCodefont, 9, Font.NORMAL)

        doc.Open()


        Dim strURL As String = "http://128.100.9.98/incident/preview_incident.aspx?irId=21"

        Dim uri As New Uri(strURL)



        'Create the request object



        Dim req As HttpWebRequest = DirectCast(WebRequest.Create(uri), HttpWebRequest)

        req.UserAgent = "Get Content"

        Dim resp As WebResponse = req.GetResponse()

        Dim stream As Stream = resp.GetResponseStream()

        Dim sr As New StreamReader(stream)

        Dim html As String = sr.ReadToEnd()
        ' Response.Write(html)

        Dim lt As List(Of IElement) = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(New StringReader(html), Nothing)


        Dim ct As New ColumnText(writer.DirectContent)


        ct.SetSimpleColumn(50, 50, PageSize.A4.Width - 50, PageSize.A4.Height - 50)


        For k As Integer = 0 To lt.Count - 1

            ct.AddElement(DirectCast(lt(k), IElement))

        Next

        ct.Go()

        doc.Close()

    End Sub






End Class
