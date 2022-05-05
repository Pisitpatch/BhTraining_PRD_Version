<?php
 	ob_start();
// 	include('irform.html');
//	$content = ob_get_clean();
              $content = "ภภภภภภภภ";
//	$content = str_replace("\xe0\xb8\x20", "\xe0\xb8\xa0", $content);
//	$content = '<page style="font-family: angsanaupc"><br />'.$content.'</page>';
	// conversion HTML => PDF
	require_once('html2pdf.class.php');
//	$html2pdf = new HTML2PDF('P','A4','en','UTF-8');
	$html2pdf = new HTML2PDF('P','A4','en', true, 'UTF-8');
//	$html2pdf->setModeDebug();
//	$html2pdf->AddFont('tahoma','','tahoma.php');
//	$html2pdf->AddFont('tahoma','B','tahomab.php');
	$html2pdf->setDefaultFont('tahoma');
//	$content = ob_end_clean();
//	$html2pdf->writeHTML($content, isset($_GET['vuehtml']));
	$html2pdf->writeHTML($content);
	$html2pdf->Output('print.pdf','I');
?>
