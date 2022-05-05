<%@ Page Language="VB" AutoEventWireup="false" CodeFile="report_compare2.aspx.vb" Inherits="jci2013_report_compare2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Compare JCI Standard Compliance by Chapter</title>
        <link href="css/jcistyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/popup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>

      <style type="text/css">
#chart1 .jqplot-point-label {
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
          
          var line1 = <% Response.Write(global_stack_fully)%>;
          var line2 = <% Response.Write(global_stack_partial)%>;
          var line3 = <% Response.Write(global_stack_notmed)%>;

          var tickers =  <% Response.Write(global_stack_title)%>;
     
          /* 
         var line1 = [6.75, 14, 10.75, 5.125, 10];
         var line2 = [4.88, 9.63, 7.25, 15.5, 11.75];
         var line3 = [4.88, 9.63, 7.25, 15.5, 11.75];
 
         var tickers = ['2008-03-01', '2008-04-01', '2008-05-01', '2008-06-01', '2008-07-01'];
         */

          /*
        plot1 = $.jqplot('chart4', [line1, line2, line3], {
            stackSeries: true,
            legend: {
                renderer: $.jqplot.EnhancedLegendRenderer,
                show: true,
                location: 'ne'
            },  seriesColors:  ["#0C0", "#FF0", "#F00"],

            title: '% JCI Complicance by Chapter',
            seriesDefaults: {
                renderer: $.jqplot.BarRenderer,
                rendererOptions: {
                    // barPadding: 6,
                    // barMargin: 15,
                    barWidth: 40
                },
                pointLabels:{show:false}
                // shadowAngle: 135
            },
            series: [{
                label: 'Fully med'
            },
			{
			    label: 'Partial med'
			}, {
			    label: 'Not med'
			}],
            axes: {
                xaxis: {
                    renderer: $.jqplot.CategoryAxisRenderer,
                    ticks: tickers
                },
                yaxis: {
                    min: 0,ticks:[0,25 , 50, 75 , 100, 125 ], tickOptions:{formatString:'%d\%'}
                },
                y2axis:{ticks:[0, 110], tickOptions:{formatString:'%d\%'}}

            }
        });
        */

  
          plot1 = $.jqplot('chart1', [line1, line2 , line3], {
              stackSeries: true,
              seriesColors: ["#00CC00","#FFFF00" ,"#FF0000"],
              legend: {
                  renderer: $.jqplot.EnhancedLegendRenderer,
                  show: false,
                  location: 'ne'
              },
              title: '<strong>% JCI Compliance by Chapter [<% Response.Write(global_submit_date)%>]</strong>',
              seriesDefaults: {
                  renderer: $.jqplot.BarRenderer,
                  rendererOptions: {
                      // barPadding: 6,
                      // barMargin: 15,
                      barWidth: 15
                       ,shadowAlpha : 0.2
                  },
                  pointLabels:{show:false}
                  // shadowAngle: 135
              },
              series: [{
                  label: 'Fully med',  pointLabels: {
                      show: true
                  }
              },
              {
                  label: 'Partial met'
              }, {
                  label: 'not met'
              }],
              axes: {
                  xaxis: {
                      renderer: $.jqplot.CategoryAxisRenderer,
                      ticks: tickers         
                  },
                  yaxis: {
                      min: 0,ticks:[0,20 ,40,60 ,80 , 100 ], tickOptions:{formatString:'%d\%'}
                  }
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
            <tr><td  style="text-align:center;" >
                 <div id="chart1" style="margin-top:10px;  width:1000px;height:400px; "></div>
                </td>
              
                </tr>
                </table>
    </div>
    </form>
</body>
</html>
