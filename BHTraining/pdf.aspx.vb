Imports System
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Imports System.Threading

Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class pdf
    Inherits System.Web.UI.Page

    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected irId As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        irId = Request.QueryString("irId")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        ShowHello()
        'Else
        'ShowTable()
    

    End Sub

    Sub ShowHello()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM ir_trans_list a INNER JOIN ir_detail_tab b ON a.ir_id = b.ir_id WHERE 1 =1"
            sql &= " AND a.ir_id = " & irId
            Response.Write(sql)
            ds = conn.getDataSet(sql, "t1")


            Dim EnCodefont As BaseFont = BaseFont.CreateFont(Server.MapPath("font/tahoma.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim Nfont As New Font(EnCodefont, 9, Font.NORMAL)

            Dim EnCodefontBM As BaseFont = BaseFont.CreateFont(Server.MapPath("font/tahoma.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim NfontBM As New Font(EnCodefont, 9, Font.BOLD)

            Dim EnCodefontBL As BaseFont = BaseFont.CreateFont(Server.MapPath("font/tahoma.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
            Dim NfontBL As New Font(EnCodefont, 13, Font.BOLD)

            Dim doc As Document = New Document(PageSize.A4, 15, 15, 10, 10)
            PdfWriter.GetInstance(doc, New FileStream(Request.PhysicalApplicationPath + _
                                  "\share\1.pdf", FileMode.Create))
            doc.Open()
            'ir header
            Dim p1 As New Paragraph("รายงานอุบัติการณ์", NfontBL)
            p1.Alignment = Element.ALIGN_CENTER
            doc.Add(p1)

            Dim p2 As New Paragraph("Incident Report", NfontBL)
            p2.Alignment = Element.ALIGN_CENTER
            doc.Add(p2)

            doc.Add(New Paragraph(" "))

            'ir no.
            Dim p3 As New Paragraph("Department " & ds.Tables(0).Rows(0)("division").ToString.PadLeft(30, " ") & "  Incident No " & ds.Tables(0).Rows(0)("ir_no").ToString, Nfont)
            p3.Alignment = Element.ALIGN_RIGHT
            doc.Add(p3)

            doc.Add(New Paragraph(" "))

            'ir detail 
            Dim DT As New iTextSharp.text.pdf.PdfPTable(4)
            DT.DefaultCell.Border = Rectangle.NO_BORDER
            DT.WidthPercentage = 100
            DT.SetWidths(New Integer(3) {3, 1, 1, 1})
            'DT.DefaultCell.FixedHeight = 50
            DT.AddCell(New Phrase("ชื่อผู้ป่วย/ผู้ประสบปัญหา " & ds.Tables(0).Rows(0)("pt_name").ToString, Nfont))
            ' columns 1
            DT.AddCell(New Phrase("อายุ " & ds.Tables(0).Rows(0)("age").ToString, Nfont))
            ' columns 2
            DT.AddCell(New Phrase("เพศ " & ds.Tables(0).Rows(0)("sex").ToString, Nfont))
            ' columns 3
            DT.AddCell(New Phrase("HN" & ds.Tables(0).Rows(0)("hn").ToString, Nfont))
            ' columns 4
            doc.Add(DT)

            Dim DT1 As New iTextSharp.text.pdf.PdfPTable(1)
            DT1.DefaultCell.Border = Rectangle.NO_BORDER
            DT1.WidthPercentage = 100
            ' DT1.SetWidths(New Integer(1) {2, 3})
            DT1.AddCell(New Phrase("แผนก/ห้อง " & ds.Tables(0).Rows(0)("room").ToString, Nfont))
            ' columns 1
            DT1.AddCell(New Phrase("Diagnosis " & ds.Tables(0).Rows(0)("diagnosis").ToString, Nfont))
            ' columns 2
            doc.Add(DT1)

            Dim DT2 As New iTextSharp.text.pdf.PdfPTable(3)
            DT2.DefaultCell.Border = Rectangle.NO_BORDER
            DT2.WidthPercentage = 100
            DT2.SetWidths(New Integer(2) {2, 2, 3})
            DT2.AddCell(New Phrase("ผ่าตัด " & ds.Tables(0).Rows(0)("operation").ToString, Nfont))
            ' columns 1
            DT2.AddCell(New Phrase("วันที่ผ่าตัด/คลอด " & ds.Tables(0).Rows(0)("date_operation").ToString, Nfont))
            ' columns 2
            DT2.AddCell(New Phrase("แพทย์เจ้าของไข้ " & ds.Tables(0).Rows(0)("physician").ToString, Nfont))
            ' columns 3
            doc.Add(DT2)

            Dim DT3 As New iTextSharp.text.pdf.PdfPTable(3)
            DT3.DefaultCell.Border = Rectangle.NO_BORDER
            DT3.WidthPercentage = 100
            DT3.SetWidths(New Integer(2) {2, 2, 2})
            DT3.AddCell(New Phrase("สถานภาพ", Nfont))
            ' columns 1
            DT3.AddCell(New Phrase("วันที่เกิดเหตุ/รับรายงาน", Nfont))
            ' columns 2
            DT3.AddCell(New Phrase("เหตุเกิด (แผนก/ห้อง)", Nfont))
            ' columns 3
            doc.Add(DT3)

            Dim DT4 As New iTextSharp.text.pdf.PdfPTable(1)
            DT4.DefaultCell.Border = Rectangle.NO_BORDER
            DT4.WidthPercentage = 100
            DT4.AddCell(New Phrase("ต้นเหตุเกิดจากแผนก", Nfont))
            ' columns 1
            doc.Add(DT4)

            Dim p4 As New Paragraph("รายงานสรุปเหตุการณ์ที่เกิดขึ้นทั้งหมด", NfontBM)
            p4.Alignment = Element.ALIGN_LEFT
            doc.Add(p4)

            Dim p5 As New Paragraph(ds.Tables(0).Rows(0)("describe").ToString, Nfont)
            p5.Alignment = Element.ALIGN_LEFT
            doc.Add(p5)

            Dim p6 As New Paragraph("การดำเนินการหลังเกิดเหตุการณ์ขึ้น", NfontBM)
            p6.Alignment = Element.ALIGN_LEFT
            doc.Add(p6)

            'ir detail part 2 physician asessement
            Dim p7 As New Paragraph("ในกรณีที่จำเป็นต้องได้รับการตรวจประเมินอาการจากแพทย์ (โปรดระบุ)", NfontBM)
            p7.Alignment = Element.ALIGN_LEFT
            doc.Add(p7)

            Dim DT5 As New iTextSharp.text.pdf.PdfPTable(4)
            DT5.DefaultCell.Border = Rectangle.NO_BORDER
            DT5.WidthPercentage = 100
            DT5.AddCell(New Phrase("แพทย์ผู้ตรวจชื่อ", Nfont))
            ' columns 1
            DT5.AddCell(New Phrase("(แพทย์เจ้าของไข้)", Nfont))
            ' columns 2
            DT5.AddCell(New Phrase("วันที่ตรวจ", Nfont))
            ' columns 3
            DT5.AddCell(New Phrase("เวลา", Nfont))
            ' columns 4
            doc.Add(DT5)

            Dim p8 As New Paragraph("ความเห็นของแพทย์", NfontBM)
            p8.Alignment = Element.ALIGN_LEFT
            doc.Add(p8)

            Dim p9 As New Paragraph(ds.Tables(0).Rows(0)("describe_assessment").ToString, Nfont)
            p9.Alignment = Element.ALIGN_LEFT
            doc.Add(p9)

            Dim DT6 As New iTextSharp.text.pdf.PdfPTable(3)
            DT6.DefaultCell.Border = Rectangle.NO_BORDER
            DT6.WidthPercentage = 100
            DT6.SetWidths(New Integer(2) {1, 3, 3})
            DT6.AddCell(New Phrase("การรักษา", Nfont))
            ' columns 1
            DT6.AddCell(New Phrase("X-Ray", Nfont))
            ' columns 2
            DT6.AddCell(New Phrase("ผล", Nfont))
            ' columns 3
            DT6.AddCell(New Phrase("", Nfont))
            ' columns 4
            DT6.AddCell(New Phrase("การตรวจทางห้องปฏิบัติการ", Nfont))
            ' columns 5
            DT6.AddCell(New Phrase("ผล", Nfont))
            ' columns 6
            DT6.AddCell(New Phrase("", Nfont))
            ' columns 7
            DT6.AddCell(New Phrase("อื่นๆ", Nfont))
            ' columns 8
            DT6.AddCell(New Phrase("ผล", Nfont))
            ' columns 9
            doc.Add(DT6)

            Dim p10 As New Paragraph("ความเห็นของผู้บริหารที่เกี่ยวข้อง (ถ้ามีโปรดระบุ)", Nfont)
            p9.Alignment = Element.ALIGN_LEFT
            doc.Add(p10)

            Dim p11 As New Paragraph("วิธีการแก้ไขที่ได้ปฏิบัติไป", Nfont)
            p10.Alignment = Element.ALIGN_LEFT
            doc.Add(p11)

            Dim DT7 As New iTextSharp.text.pdf.PdfPTable(3)
            DT7.DefaultCell.Border = Rectangle.NO_BORDER
            DT7.WidthPercentage = 100
            DT7.SetWidths(New Integer(2) {1, 1, 4})
            DT7.AddCell(New Phrase("ผลการประเมิน", Nfont))
            ' columns 1
            DT7.AddCell(New Phrase("ผู้ป่วย", Nfont))
            ' columns 2
            DT7.AddCell(New Phrase("....................", Nfont))
            ' columns 3
            DT7.AddCell(New Phrase("", Nfont))
            ' columns 4
            DT7.AddCell(New Phrase("ด้านอื่น", Nfont))
            ' columns 5
            DT7.AddCell(New Phrase("....................", Nfont))
            ' columns 6
            doc.Add(DT7)

            'ir footer

            Dim DT8 As New iTextSharp.text.pdf.PdfPTable(3)
            DT8.DefaultCell.Border = Rectangle.NO_BORDER
            DT8.WidthPercentage = 100
            DT8.SetWidths(New Integer(2) {3, 2, 1})
            DT8.AddCell(New Phrase("ผู้รายงานเหตุการณ์", Nfont))
            ' columns 1
            DT8.AddCell(New Phrase("แผนก", Nfont))
            ' columns 2
            DT8.AddCell(New Phrase("วันที่", Nfont))
            ' columns 3
            doc.Add(DT8)



            doc.Close()
            'Response.Redirect("~/1.pdf")
            ds.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
            Return
        End Try

     

        Response.ContentType = "text/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=1.pdf")
        Thread.Sleep(5000)
        Response.TransmitFile("share/1.pdf")
        Response.End()
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
End Class
