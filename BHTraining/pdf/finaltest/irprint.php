<?php
 	ob_start();
 	include('irform.html');
	$content = ob_get_clean();
	$content = '<page style="font-family: cordiaupc">'.$content.'</page>';
	// conversion HTML => PDF
	require_once('html2pdf.class.php');
//	$html2pdf = new HTML2PDF('P','A4','en','UTF-8');
	$html2pdf = new HTML2PDF('P','A4','en', true, 'UTF-8');
//	$html2pdf->setModeDebug();
//	$html2pdf->AddFont('tahoma','','tahoma.php');
//	$html2pdf->AddFont('tahoma','B','tahomab.php');
//	$html2pdf->setDefaultFont('tahoma');
//	$html2pdf->writeHTML($content, isset($_GET['vuehtml']));
	$html2pdf->pdf->SetDisplayMode('fullwidth','Continuous');
	$html2pdf->writeHTML($content);
	$html2pdf->Output('print.pdf','I');
?>
