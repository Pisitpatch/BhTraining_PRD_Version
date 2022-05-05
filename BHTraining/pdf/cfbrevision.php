<?php
	$file = $_GET["f"];
	require_once("setPDF.php");
	$filepath = '../share/cfb/revision/'.$file.'.html';
              $filename =  '../share/cfb/revision/'.$file.'.pdf';
	ob_start();
	
 	include($filepath);
	$content = ob_get_clean();
//	$pdf->setPrintHeader(false);
//	$pdf->setPrintFooter(false);
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
//	$pdf->Output($filename, 'I');
	$pdf->Output($filename, 'F');
//              $redirectfile = 'Location: http://128.100.9.98/share/cfb/revision/'.$file.'.pdf';
//	header("$redirectfile");
?>