<?php
	ob_start();
	require_once("setPDFaddlife.php");
 	include('../share/cfb_invest.html');
	$content = ob_get_clean();
	$pdf->setPrintHeader(false);
	$pdf->setPrintFooter(false);
//	$resolution= array(100, 40);
	$pdf->AddPage('P', $resolution);
//	$pdf->AddPage();
	$content = preg_replace('/\s+/uis', ' ', $content);
	$content = str_replace('&nbsp;',' ', $content);
	$content = str_replace('﻿',' ', $content);
	$htmlcontent= $content ;
//	$htmlcontent=stripslashes($htmlcontent);
//	$htmlcontent=AdjustHTML($htmlcontent);
	// สร้างเนื้อหาจาก  HTML code
//	$pdf->SetDisplayMode('fullwidth','TwoColumnRight');
	ob_end_clean();
	$pdf->writeHTML($htmlcontent, true, 0, true, 0);
	// เลื่อน pointer ไปหน้าสุดท้าย
	$pdf->lastPage();
	// ปิดและสร้างเอกสาร PDF
	$pdf->Output('IR.pdf', 'I');
?>