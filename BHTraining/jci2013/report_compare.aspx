<%@ Page Language="VB" AutoEventWireup="false" CodeFile="report_compare.aspx.vb" Inherits="jci2013_report_compare" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Compare JCI Standard Compliance by Percent</title>
      <link href="css/jcistyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/popup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>

      <style type="text/css">
#chart4 .jqplot-point-label {
  border: 1.5px solid #aaaaaa;
  padding: 1px 3px;
  background-color: #eeccdd;
}
</style>

       <!--[if IE]><script language="javascript" type="text/javascript" src="../js/jqplot/excanvas.js"></script><![endif]-->
  
  <link rel="stylesheet" type="text/css" href="../js/jqplot/jquery.jqplot.css" />
  
  <link type="text/css" href="../js/jqplot/examples/jquery-ui/css/ui-lightness/jquery-ui-1.8.1.custom.css" rel="Stylesheet" />	
  <script type="text/javascript" src="../js/jqplot/examples/jquery-ui/js/jquery-ui-1.8.1.custom.min.js"></script>

  
 
  <!-- BEGIN: load jqplot -->
  <script language="javascript" type="text/javascript" src="../js/jqplot/jquery.jqplot.js"></script>
  <script language="javascript" type="text/javascript" src="../js/jqplot/plugins/jqplot.pieRenderer.js"></script>
  <script language="javascript" type="text/javascript" src="../js/jqplot/plugins/jqplot.pointLabels.js"></script>

   <script language="javascript" type="text/javascript" src="../js/jqplot/plugins/jqplot.barRenderer.min.js"></script>
  <script language="javascript" type="text/javascript" src="../js/jqplot/plugins/jqplot.categoryAxisRenderer.min.js"></script>

  	<script language="javascript" type="text/javascript" src="../js/jqplot/plugins/jqplot.canvasTextRenderer.js"></script>
    <script language="javascript" type="text/javascript" src="../js/jqplot/plugins/jqplot.canvasAxisTickRenderer.js"></script>
  <script language="javascript" type="text/javascript" src="../js/jqplot/plugins/jqplot.pieRenderer.lineLabels.js"></script>

    <!-- END: load jqplot -->

    <script type="text/javascript">
        $(document).ready(function () {

            $.jqplot.config.enablePlugins = true;

            // s1 = [['B1', 215], ['B4', 8]];
            s1 = [<%Response.Write(global_unit_num1)%>];

          plot1 = $.jqplot('chart1', [s1], {
              grid: {
                  drawBorder: false,
                  drawGridlines: false,
                  background: '#ffffff',
               
                  shadow: false
              },
              axesDefaults: {

              },
              gridPadding: {top:0, bottom:38, left:0, right:0},
              seriesDefaults: {
                  renderer: $.jqplot.PieRenderer,
                
                  rendererOptions: {
                      showDataLabels: true,
                      shadowAlpha : 0.2,
                      padding: 30,
                      seriesColors: ["#0C0", "#FF0", "#F00"],
                      sliceMargin: 8,
                      startAngle: -90
                       , lineLabels: true, lineLabelsLineColor: '#777'
                  }
              },
              title: '<strong>% JCI Standard Compliance [<% Response.Write(global_submit_date1)%>]</strong>',
              legend: {
                  show: true,
                  rendererOptions: {
                      numberRows: 1


                  },
                  location: 'ne'
              }


          });


          
            s1 = [<%Response.Write(global_unit_num2)%>];

            plot1 = $.jqplot('chart2', [s1], {
                grid: {
                    drawBorder: false,
                    drawGridlines: false,
                    background: '#ffffff',

                    shadow: false
                },
                axesDefaults: {

                },
                gridPadding: { top: 0, bottom: 38, left: 0, right: 0 },
                seriesDefaults: {
                    renderer: $.jqplot.PieRenderer,

                    rendererOptions: {
                        showDataLabels: true,
                        shadowAlpha: 0.2,
                        padding: 30,
                        seriesColors: ["#0C0", "#FF0", "#F00"],
                        sliceMargin: 8,
                        startAngle: -90
                         , lineLabels: true, lineLabelsLineColor: '#777'
                    }
                },
                title: '<strong>% JCI Standard Compliance [<% Response.Write(global_submit_date1)%>]</strong>',
              legend: {
                  show: true,
                  rendererOptions: {
                      numberRows: 1


                  },
                  location: 'ne'
              }


          });
         

            s1 = [<%Response.Write(global_unit_num3)%>];

            plot1 = $.jqplot('chart3', [s1], {
                grid: {
                    drawBorder: false,
                    drawGridlines: false,
                    background: '#ffffff',

                    shadow: false
                },
                axesDefaults: {

                },
                gridPadding: { top: 0, bottom: 38, left: 0, right: 0 },
                seriesDefaults: {
                    renderer: $.jqplot.PieRenderer,

                    rendererOptions: {
                        showDataLabels: true,
                        shadowAlpha: 0.2,
                        padding: 30,
                        seriesColors: ["#0C0", "#FF0", "#F00"],
                        sliceMargin: 8,
                        startAngle: -90
                         , lineLabels: true, lineLabelsLineColor: '#777'
                    }
                },
                title: '<strong>% JCI Standard Compliance [<% Response.Write(global_submit_date1)%>]</strong>',
              legend: {
                  show: true,
                  rendererOptions: {
                      numberRows: 1


                  },
                  location: 'ne'
              }


          });

          $(document).unload(function () { $('*').unbind(); });
      });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="100%" cellpadding="1" cellspacing="1" border="0">
            <tr><td  style="text-align:center;" width="33%">
                 <div id="chart1" style="margin-top:10px;  width:400px;height:500px; "></div>
                </td>
                <td style="text-align:center;" width="33%"> 
                    <div id="chart2" style="margin-top:10px;  width:400px;height:500px; "></div></td>
                
             <td style="text-align:center;" width="33%"> 
                    <div id="chart3" style="margin-top:10px;  width:400px;height:500px; "></div></td>
                </tr>
                </table>
    </div>
    </form>
</body>
</html>
