<%@ Page Title="" Language="VB" MasterPageFile="~/cfb/CFB_MasterPage.master" AutoEventWireup="false" CodeFile="report_viewer.aspx.vb" Inherits="incident_report_viewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

         s1 = [<%response.write(global_unit_num) %>];
         plot1 = $.jqplot('chart2', [s1], {
        grid: {
            drawBorder: false, 
            drawGridlines: false,
            background: '#ffffff',
            shadow:false
        },
        axesDefaults: {
            
        },
        seriesDefaults:{
            renderer:$.jqplot.PieRenderer,
            rendererOptions: {
                showDataLabels: true  ,
               //  dataLabels: 'value' , 
                sliceMargin: 4,
                startAngle: -90
                 ,lineLabels: true, lineLabelsLineColor: '#777'
            }
        },
        title : '',
        legend: {
            show: false,
            rendererOptions: {
                numberRows: 1
               
                
            },
            location: 'e'
        }


    }); 

  s2 = [<%response.write(global_unit_num2) %>];
         plot3 = $.jqplot('chart3', [s2], {
        grid: {
            drawBorder: false, 
            drawGridlines: false,
            background: '#ffffff',
            shadow:false
        },
        axesDefaults: {
            
        },
        seriesDefaults:{
            renderer:$.jqplot.PieRenderer,
            rendererOptions: {
                showDataLabels: true  ,
               //  dataLabels: 'value' , 
                sliceMargin: 4,
                startAngle: -90
                 ,lineLabels: true, lineLabelsLineColor: '#777'
            }
        },
        title : '',
        legend: {
            show: false,
            rendererOptions: {
                numberRows: 1
               
                
            },
            location: 'e'
        }


    }); 

     
$(document).unload(function () { $('*').unbind(); });
});
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="header"><img src="../images/doc01.gif" width="32" height="32" align="absmiddle" />&nbsp;&nbsp;Dashboard</div>
<div style="padding: 0px 20px;">
<!--
<fieldset style="background: #E8E8E8; width: 80%;">
  <legend>Date range</legend>
  <table>
    <tr>
      <td><input type="text" name="textfield" id="textfield" style="width: 100px;" />
        -
        <input type="text" name="textfield2" id="textfield2" style="width: 100px;" />
        <input type="submit" name="button" id="button" value="Submit" /></td>
      </tr>
  </table>
</fieldset>
-->
<!--
<table width="100%">
<tr>
  <td colspan="2">     <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="980" height="435" id="Object1" align="left" >
    <param name="allowScriptAccess" value="sameDomain" />
    <param name="allowFullScreen" value="false" />
    <param name="movie" value="Lines-Chart.swf" />
    <param name="quality" value="high" />
    <param name="wmode" value="opaque" /><table width="100%">


    <param name="bgcolor" value="#ffffff" />
    
  </object>    </td>
</tr></table>
-->
<table width="100%" cellpadding="3">
<tr>
<td width="50%">
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Top 10 Unit (Complaint)       
                <asp:DropDownList ID="txtdate1" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                  </td>
      </tr>
    <tr>
      <td style="vertical-align:top"><div id="chart2" style="margin-top:10px; margin-left:20px; width:370px;height:300px; float:left"></div></td>
    </tr>
  </table>
</td>
<td style="vertical-align:top">
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Top 10 Unit (Praise)       
                <asp:DropDownList ID="txtdate2" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                  </td>
      </tr>
    <tr>
      <td><div id="chart3" style="margin-top:10px; margin-left:20px; width:370px;height:300px; float:left"></div></td>
    </tr>
  </table>
</td>
</tr>
<tr>
  <td width="50%">

  <table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Customer Feedback Overview</td>
      </tr>
    <tr>
      <td><object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="500"   height="250" id="Object4" align="middle" >
        <param name="allowScriptAccess" value="sameDomain" />
        <param name="allowFullScreen" value="false" />
        <param name="movie" value="incident.swf" />
        <param name="quality" value="high" />
        <param name="wmode" value="opaque" />
        <param name="bgcolor" value="#ffffff" />
      </object></td>
    </tr>
  </table>

  </td>
  <td width="50%" valign="top"><table width="100%" cellpadding="0" cellspacing="0">
    <tr>
      <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Customer Feedback Overview</td>
    </tr>
    <tr>
      <td><object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="500"   height="250" id="Object2" align="middle" >
        <param name="allowScriptAccess" value="sameDomain" />
        <param name="allowFullScreen" value="false" />
        <param name="movie" value="Pie-Chart-3D.swf" />
        <param name="quality" value="high" />
        <param name="wmode" value="opaque" />
        <param name="bgcolor" value="#ffffff" />
      </object></td>
    </tr>
  </table></td>
</tr>
</table>
<br />

<table width="100%" cellpadding="3">
  <tr>
  <td width="50%" valign="top"><table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Customer Feedback Overview</td>
      </tr>
    <tr>
      <td><object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="500"   height="250" id="Object3" align="middle" >
        <param name="allowScriptAccess" value="sameDomain" />
        <param name="allowFullScreen" value="false" />
        <param name="movie" value="Vertical-Stacked-Bar-Chart.swf" />
        <param name="quality" value="high" />
        <param name="wmode" value="opaque" />
        <param name="bgcolor" value="#ffffff" />
      </object></td>
    </tr>
  </table></td>
  <td width="50%" valign="top">
  
  <table width="100%" cellpadding="0" cellspacing="0">
    <tr>
      <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Top 20 Customer Feedback</td>
    </tr>
    
    <tr>
      <td>
      <div style="height: 250px; overflow-x:hidden;overflow-y:auto;">
    <asp:GridView ID="GridIR" runat="server" Width="100%"  AutoGenerateColumns="False"
          EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="grand_topic_name" HeaderText="Topic name" />
            <asp:TemplateField HeaderText="%">
             
                <ItemTemplate>
                    <asp:Label ID="lblPercent" runat="server"></asp:Label>
                       <asp:Label ID="lblTotal" runat="server" Text=''></asp:Label>%
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Comment">
             
                <ItemTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Bind("num") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
      </asp:GridView>
        <asp:TextBox ID="txtTotalGrandTopic" runat="server" Visible="False" ></asp:TextBox>
      </div>
        
    </td>
    </tr>
  </table></td>
</tr>
</table>

</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
</asp:Content>

