<%@ Page Title="" Language="VB" MasterPageFile="~/star/Star_MasterPage.master" AutoEventWireup="false" CodeFile="star_message.aspx.vb" Inherits="star_star_message" MaintainScrollPositionOnPostback="true" %>

<%@ Import Namespace="ShareFunction" %>
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
            show: true,
            rendererOptions: {
                numberRows: 1
               
                
            },
            location: 'e'
        }


    }); 


  //var l1 = [2, 3, 1, 4, 3];
    //var l2 = [1, 4, 3, 2, 5];
        //plot1 = $.jqplot('chart3', [l1, l2], {


    //plot1 = $.jqplot('chart3', [<%response.write(global_unit_stack1) %>], {
   //   title:'Star of Bumrungrad by BI Way',
   //    legend: {
   //          show: true,
   //          location: 'nw'
    //     },

   //     stackSeries: true,
   //     grid:{background:'#fefbf3', borderWidth:2.5},
   //     seriesDefaults: {fill: true, showMarker: false, shadow: false},
  //      axes:{xaxis:{  renderer:$.jqplot.CategoryAxisRenderer,
  //              ticks:[<%response.write(global_unit_stack_xaxis) %>] }, yaxis:{min:0, max:500, numberTicks:20}},
  //      series:[{ label: 'Clear'}, { label: 'Care'} , {label:'Smart'} , {label:'Quality'}]
 //   });
  
   
   //  l1 = [<%response.write(global_dept_num) %>];
    
   //  lClear = [<%response.write(global_clear) %>];
    // lCare = [<%response.write(global_care) %>];
    // lSmart = [<%response.write(global_smart) %>];
        // lQuality = [<%response.write(global_quality) %>];

        try {
            line1 = [<%response.write(data4_1) %>];
          
            plot3c = $.jqplot('chart4', [line1], {
                  legend: {
                      show: true,
                      location: 'ne'
                  },
                  title: 'Top 5 : Star of Bumrungrad Awarded by Department  ',
         seriesDefaults: {
             renderer: $.jqplot.BarRenderer,
             rendererOptions: {
                 barPadding: 6,
                 barMargin: 20
             }
         },
         series: [{
             label: 'Star Awarded'
         },
         {
             label: 'Top 10 : Star of Bumrungrad Awarded by Department  '
         }],
         axes: {
             xaxis: {
                 renderer: $.jqplot.CategoryAxisRenderer,
                 // ticks: ['Q1', 'Q2', 'Q3', 'Q4']
                 ticks: [<%response.write(data4_name) %>]
             },
           //  yaxis: { min: 0, max: 20, numberTicks: 5 }
         }
     });
 } catch (e) {
     //alert(e);
 }


    
     
$(document).unload(function () { $('*').unbind(); });
});
</script>

<script type="text/javascript" src="jwplayer.js"></script>
         <script type="text/javascript" src="../js/media_play.js" charset="utf-8"></script>
 <script type="text/javascript" src="../js/media_chili.js" charset="utf-8"></script>
 <script type="text/javascript">

     $(function () {

         //$.fn.media.mapFormat('avi','quicktime');

         // this one liner handles all the examples on this page

         $('a.media').media();

     });

</script>
    <style type="text/css">
        .style1
        {
            height: 29px;
        }
        .style2
        {
            height: 29px;
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
   
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="data">
    <strong><img src="../images/star_32.png" alt="SSIP" width="32" height="32"   />News and Events</strong>
    <br />
    
    <br />
            <div style="width:100%; margin:auto 0" align="center" >
            <table width="600">
                <tr>
                    <td align="center" style="text-align:center">  <a href="form_star.aspx?mode=add"><img src="../images/BH-SOB-01.png" border="0" alt="check point" /></a>  </td>
                    <td align="center" style="text-align:center">  <a href="home.aspx?viewtype=nominee"><img src="../images/BH-SOB-02.png" border="0" alt="check point" /></a></td>
                   
                </tr>

            </table>
           
      

        </div>
   
    <br />

      <asp:Label ID="lblInnovation" runat="server" Text="-"></asp:Label>
</div>
  <div id="data1">
<table width="100%" align="center" cellpadding="3" cellspacing="0">
  <tr>
    <td width="50%" style="vertical-align:top"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: pink; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Star of Bumrungrad News
        <%  If findArrayValue(priv_list, "302") = True Then ' HR %> 
        <a href="#" style="color:Yellow"  id="addNew"  onclick="window.open('star_news_edit.aspx?mode=add', '', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600')";>[Add new]</a>
        <%End If %>
         </td>
      </tr>
      <tr>
        <td style="vertical-align:top"><div style="height: 250px; overflow:auto ">
   <asp:GridView ID="GridNews" runat="server" AutoGenerateColumns="False" 
            CellPadding="6" ForeColor="#333333" GridLines="None" 
            Width="100%" DataKeyNames="new_id" CellSpacing="0" 
                EnableModelValidation="True" ShowHeader="False">
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:TemplateField HeaderText="Date">
            <ItemStyle Width="100px" />
                  <ItemTemplate><img src="../images/lightbulb.png" width="16" height="16"  />
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Bind("new_date", "{0:dd MMM yyyy}") %>'></asp:Label>
                      <asp:Label ID="lblPK" runat="server" Text='<%# bind("new_id") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="News Title">
               
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red" >[Edit]</asp:LinkButton>&nbsp;
                <a href="#" onclick=" $.showAkModal('star_news_detail.aspx?id=<%# Eval("new_id") %>', '<%# Eval("title_th") %>', 600, 300);">
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("title_th") %>'></asp:Label>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="By">
             
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("create_by") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
      </div></td>
      </tr>
    </table></td>
    <td width="50%" style="vertical-align:top"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: pink; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;" 
             >
            Star of Bumrungrad Channel</td>
      </tr>
      <tr>
        <td align="center">
   <div style="padding: 10px 10px;">
        <embed src="flash/<%response.write (clip_name) %>"  />
         </div>
        </td>
      </tr>
    </table>
  </td>
  </tr>

  <tr>
    <td width="50%" valign="top"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: pink; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">
             Top 5 : Star of Bumrungrad 
            Awarded 
            by Department <asp:DropDownList ID="txtbiMonthDept" runat="server" 
                AutoPostBack="True">
            </asp:DropDownList></td>
      </tr>
      <tr>
        <td>
            <div id="chart4" style="margin-top:10px; margin-left:20px; width:480px;height:350px; float:left"></div>
            <!--
            <div >
   
            <object id="Object2" align="middle" 
                classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" 
                codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" 
                height="350" width="500">
                <param name="allowScriptAccess" value="sameDomain" />
                <param name="allowFullScreen" value="false" />
                <param name="movie" value="Pie-Chart-3D.swf?ts=<% Response.Write(Date.Now.Ticks) %>" />
                <param name="quality" value="high" />
                <param name="wmode" value="opaque" />
                <param name="bgcolor" value="#ffffff" />
            </object>
   
      </div>
       -->
        
      </td>
      </tr>
    </table></td>
    <td width="50%" valign="top"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: pink; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Top 20 : Star of Bumrungrad 
            Awarded 
            by Department</td>
      </tr>
      <tr>
        <td>
    <asp:GridView ID="GripStarTopSubmit" runat="server" Width="100%"  AutoGenerateColumns="False"
          EnableModelValidation="True">
          <AlternatingRowStyle BackColor="#ffccff" />
          <HeaderStyle BackColor="pink" />
        <Columns>
            <asp:BoundField DataField="report_dept_name" HeaderText="Department name" HeaderStyle-ForeColor="White" />
          
            <asp:TemplateField HeaderText="No. of Star">
             <HeaderStyle ForeColor="White" />
                <ItemTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Bind("num") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
            </asp:GridView>
          </td>
      </tr>
    </table>
  </td>
  </tr>

 
     <tr>
  <td colspan="2">
    
      </td>
         </tr>
  <tr>
  <td colspan="2"> 
      
      <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: pink; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;" 
              class="style2">
            Summary
            Star of Bumrungrad by BI Way
            &nbsp;
            <asp:DropDownList ID="txtbiMonth" runat="server" 
                AutoPostBack="True">
            </asp:DropDownList>
          </td>
      </tr>
      <tr><td><div id="chart2" style="margin-top:10px; margin-left:20px; width:800px;height:350px; float:left"></div>

            <div style="float:left;width:300px;overflow:auto">
        <br />
        <br />
        <table width="100%" cellpadding="0" cellspacing="3" border="0">
            <tr><td>B1 = Exceed our customer's expectations</td></tr>
             <tr><td>B2 = Committed to our staff's welfare and development</td></tr>
             <tr><td>B3 = Continually improve the quality and safety</td></tr>
             <tr><td>B4 = Professional excellence and innovation</td></tr>
             <tr><td>B5 = Embrace cultural diversity with Thai hospitality</td></tr>
             <tr><td>B6 = Make everything World Class</td></tr>
             <tr><td>B7 = Be trusted, honest, and ethical in all our dealing</td></tr>
             <tr><td>B8 = Work as a team</td></tr>
             <tr><td>B9 = Value corporate social responsibilities</td></tr>
             <tr><td>B10 = Operate in an environmentally responsible manner</td></tr>
        </table>
       
    </div>
          </td></tr>
      </table></td>
  </tr>
  <tr>
  <td>&nbsp;</td>
  <td>&nbsp;</td>
  </tr>
</table>
</div>

  
</asp:Content>

