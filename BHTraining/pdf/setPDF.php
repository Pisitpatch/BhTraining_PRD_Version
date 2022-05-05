<?php
require_once('_tcpdf/config/lang/eng.php');
require_once('_tcpdf/tcpdf.php');
require_once('htmltoolkit.php');
class MYPDF extends TCPDF {
    //Page header
    public function Header() {
        // full background image
        // store current auto-page-break status
        $bMargin = $this->getBreakMargin();
        $auto_page_break = $this->AutoPageBreak;
        $this->SetAutoPageBreak(false, 0);
        $img_file = K_PATH_IMAGES.'bhbg.jpg';
        $this->Image($img_file, 0, 0, 210, 297, '', '', '', false, 300, '', false, false, 0);
        // restore auto-page-break status
        $this->SetAutoPageBreak($auto_page_break, $bMargin);
    }


    // Page footer
    public function Footer() {
        // full background image
        // store current auto-page-break status
        $bMargin = $this->getBreakMargin();
        $auto_page_break = $this->AutoPageBreak;
        $this->SetAutoPageBreak(false, 0);
        $image_file = K_PATH_IMAGES.'bhfooter.gif';
        $this->Image($image_file, '175', '282', 30, '', 'GIF', '', 'T', false, 300, '', false, false, 0, false, false, false);
        // Position at 15 mm from bottom
        $this->SetY(-15);
        // Set font
        $this->SetFont('tahoma', '', 8);
        // Page number
        $this->Cell(0, 10, '33 Sukhumvit 3, Bangkok 10110 Thailand Tel: +66 2 667 1000 Fax: +662 667-2525 www.bumrungrad.com Page '.$this->getAliasNumPage().'/'.$this->getAliasNbPages(), 0, false, 'C', 0, '', 0, false, 'T', 'M');
        $this->SetAutoPageBreak($auto_page_break, $bMargin);
    }
}

// ค่าเริ่มต้นต่างๆ สามารถเข้าไปกำหนดได้ที่ไฟล์ tcpdf_config.php ในโฟลเดอร์ config
// สร้าง PDF document ใหม่
$pdf = new MYPDF(PDF_PAGE_ORIENTATION, PDF_UNIT, PDF_PAGE_FORMAT, true, 'UTF-8', false); 

// กำหนดรายละเอียดของเอกสาร pdf แสดงเมื่อคลิกขวาที่ไฟล์ PDF แล้วเลือก Document Property
$pdf->SetCreator('TQM'); // เครื่องมือสร้าง PDF  ค่าเริ่ม PDF_CREATOR = TCPDF
$pdf->SetAuthor('IRCFB'); // ชื่อผู้สร้างไฟล์ PDF
$pdf->SetTitle('IRNO');//  กำหนด Title
$pdf->SetSubject('IRNO'); // กำหนด Subject
$pdf->SetKeywords('IRCFB No.'); // กำหนด Keyword

//   กำหนดค่าเริ่มต้นสำหรับ Header
//	PDF_HEADER_LOGO  โลโก้รูปภาพส่วน Header
//	PDF_HEADER_LOGO_WIDTH ความกว้างของโลโก้ เป็น มิลเมตร (mm)
//	PDF_HEADER_TITLE หัวเรื่องของ Header
//	PDF_HEADER_STRING ข้อความที่ต้องการแสดงในส่วน header ขึ้นบรรทัดใหม่ใช้ \n
//$pdf->SetHeaderData(PDF_HEADER_LOGO, PDF_HEADER_LOGO_WIDTH, PDF_HEADER_TITLE, PDF_HEADER_STRING);

// set header and footer fonts
$pdf->setHeaderFont(Array(PDF_FONT_NAME_MAIN, '', PDF_FONT_SIZE_MAIN));
$pdf->setFooterFont(Array(PDF_FONT_NAME_DATA, '', PDF_FONT_SIZE_DATA));

// กำหนดค่าเริ่มต้น Font สำหรับช่องว่าง
$pdf->SetDefaultMonospacedFont(PDF_FONT_MONOSPACED);

//ตั้งค่าหน้ากระดาษ
$pdf->SetMargins(2,10,2);
$pdf->SetHeaderMargin(10);
$pdf->SetFooterMargin(15);

//กำหนดการแบ่งหน้าอัตโนมัติ
$pdf->SetAutoPageBreak(TRUE,15);

// กำหนดอัดราส่วนของรูปภาพ
$pdf->setImageScale(PDF_IMAGE_SCALE_RATIO); 

// กำหนดกลุ่มภาษา
$pdf->setLanguageArray($l); 

// กำหนด Font กรณีใช้ภาษาไทย
//$pdf->SetFont('tahoma', '', 11);
$pdf->SetFont('thsarabun', '', 14);
?>