<?php
require_once("setPDF.php");
// เพิ่มหน้าใน PDF 
$pdf->AddPage();

// กำหนด HTML code หรือรับค่าจากตัวแปรที่ส่งมา
//	กรณีกำหนดโดยตรง
//	ตัวอย่าง กรณีรับจากตัวแปร
// $htmlcontent =$_POST['HTMLcode'];
$htmlcontent='<p>ทดสอบ รายงานอุบัติการณ์ความคลาดเคลื่อนในระบบบริหารการใช้ยา รายงานอุบัติการณ์ความคลาดเคลื่อนในระบบบริหารการใช้ยา</p>';
$htmlcontent=stripslashes($htmlcontent);
$htmlcontent=AdjustHTML($htmlcontent);

// สร้างเนื้อหาจาก  HTML code
$pdf->setPrintHeader(false);
$pdf->setPrintFooter(false);
$pdf->writeHTML($htmlcontent, 0, 0, 0, 0);

// เลื่อน pointer ไปหน้าสุดท้าย
$pdf->lastPage();

// ปิดและสร้างเอกสาร PDF
ob_end_clean();
$pdf->Output('test.pdf', 'I');
?>