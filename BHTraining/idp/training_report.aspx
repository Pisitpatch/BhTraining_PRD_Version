<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="training_report.aspx.vb" Inherits="idp_training_report" MaintainScrollPositionOnPostback="true" %>
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

    try{
            s1 = [<%response.write(data1) %>];
     
/*
        plot1 = $.jqplot('chart1', [s1], {
            seriesDefaults: {
                renderer: $.jqplot.PieRenderer,
                 rendererOptions:{
                sliceMargin: 4,
                startAngle: -90}
            },
            legend: { show: true }
        });
        */
  plot1 = $.jqplot('chart1', [s1], {
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
            }
        },
        title : 'Actual Expense <%response.write(limit_record & " ( Total " & formatNumber(totalExpense,0) & " baht)") %>',
        legend: {
            show: true,
            rendererOptions: {
                numberRows: 1
               
                
            },
            location: 's'
        }
    }); 

    /*   
       // var line1 = [14, 32, 41, 44, 40];
        var line1 = [<%response.write(data2) %>];
  var plot3 = $.jqplot('chart2', [line1], {
    title: 'Total Request <%response.write(limit_record & " ( Total " & formatNumber(totalRequest,0) & " request)") %>', 
  
    seriesDefaults: {renderer: $.jqplot.BarRenderer},
    series:[
     {pointLabels:{
        show: true,
        labels:[<%response.write(data2_name) %>]
      }}],
    axes: {
        
      xaxis:{renderer:$.jqplot.CategoryAxisRenderer},
      yaxis:{padMax:2 , min :0 , max:120 , numberTicks:5}}
  });
  */

   s2 = [<%response.write(data2) %>];
      plot1 = $.jqplot('chart2', [s2], {
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
            }
        },
        title : 'Total Approve ',
        legend: {
            show: true,
            rendererOptions: {
                numberRows: 1
               
                
            },
            location: 's'
        }
    }); 
    }catch(e){
    // alert(e)
    }
    
     
    

  /*=====================Repot 2 pie graph===*/
  try{
    var data = [
   <%response.write(data3) %>
  ];
 /*
   var data = [
    ['Heavy Industry', 12],['Retail', 9], ['Light Industry', 14], 
    ['Out of home', 16],['Commuting', 7], ['Orientation', 9]
  ]; */
  var plot33 = jQuery.jqplot ('g3', [data], 
    {
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
            legend: { show: false }
    
    }
  );
  }catch(e){

  }

  try{
   line1 = [<%response.write(data4_1) %>];
     line2 = [<%response.write(data4_2) %>];
     plot3c = $.jqplot('chart3b', [line1, line2], {
         legend: {
             show: true,
             location: 'nw'
         },
         title: 'Action after training <%response.write(limit_record) %>',
         seriesDefaults: {
             renderer: $.jqplot.BarRenderer,
             rendererOptions: {
                 barPadding: 6,
                 barMargin: 20
             }
         },
         series: [{
             label: 'Finish training'
         },
         {
             label: 'Action after training'
         }],
         axes: {
             xaxis: {
                 renderer: $.jqplot.CategoryAxisRenderer,
                // ticks: ['Q1', 'Q2', 'Q3', 'Q4']
                 ticks: [<%response.write(data4_name) %>]
             },
             yaxis: {min: 0, max: 20, numberTicks:5}
         }
     });   
  }catch(e){
    //alert(e);
  }

  // External Budget Pie graph
      try{
            s1 = [<%response.write(data_external_budget) %>];
     
  plotExtBudget = $.jqplot('chartExternalBudget', [s1], {
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
            }
        },
        title : 'External Training Expect Outcome',
        legend: {
            show: true,
            rendererOptions: {
                numberRows: 1
               
                
            },
            location: 's'
        }
    }); 

      }catch(e){
  //   alert(e)
    }

 try{
      // Internal Budget Pie graph
  s2 = [<%response.write(data_internal_budget) %>];
     
  plotExtBudget = $.jqplot('chartInternalBudget', [s2], {
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
            }
        },
        title : 'Internal Training Expect Outcome',
        legend: {
            show: true,
            rendererOptions: {
                numberRows: 1
               
                
            },
            location: 's'
        }
    }); 
   }catch(e){
  //   alert(e)
    }
        $(document).unload(function () { $('*').unbind(); });
    });
</script>


    <style type="text/css">
@import "../js/datepicker2/redmond.calendars.picker.css";
 /*Or use these for a ThemeRoller theme
 
@import "themes16/southstreet/ui.all.css";
@import "js/datepicker/ui-southstreet.datepick.css";
*/

        .style2
        {
            height: 25px;
        }

    </style>

<script type="text/javascript" src="../js/datepicker2/jquery.calendars.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.plus.js"></script>


<script type="text/javascript" src="../js/datepicker2/jquery.calendars.picker.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.picker.ext.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.thai.js"></script>

<script type="text/javascript">
    $(function () {
        //	$.datepick.setDefaults({useThemeRoller: true,autoSize:true});
        var calendar = $.calendars.instance();

        $('#ctl00_ContentPlaceHolder1_txtdate1').calendarsPicker(
			{

			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}
			);

        $('#ctl00_ContentPlaceHolder1_txtdate2').calendarsPicker(
			{

			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}
			);

        //	$('#inlineDatepicker').datepick({onSelect: showDate});
    });



</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer">
</div>
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
              </asp:ScriptManager>
    <div id="data">
        <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
          <tr>
            <td width="550" colspan="4" class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
          </tr>
          <tr>
            
            <td colspan="4"><table width="100%" cellpadding="2">
              <tr>
                <td width="150"><strong>Select Report Type</strong></td>
                <td width="385">
                    <asp:DropDownList ID="txtselect_report" runat="server" AutoPostBack="True" >
                   
                    </asp:DropDownList>
             </td>
                <td width="80">&nbsp;</td>
                <td>
                    &nbsp;</td>
              </tr>
              <tr runat="server" id ="search_date">
                <td >Date Range</td>
                <td width="385">
                    <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
          
          &nbsp;to&nbsp;<asp:TextBox 
                ID="txtdate2" runat="server" Width="80px"></asp:TextBox> &nbsp;</td>
                <td width="80">&nbsp;</td>
                <td>
                   
             
                &nbsp;
                                         
              
                 </td>
              </tr>
              <tr>
                <td width="120">Department</td>
                <td width="385">
                    <asp:DropDownList ID="txtdept" runat="server" DataTextField="dept_name_en" 
                                DataValueField="dept_id">
                    </asp:DropDownList>
             </td>
                <td width="80">&nbsp;</td>
                <td>
                    &nbsp;</td>
              </tr>
            
              </table>
              <asp:Panel ID="panel_search_dept" runat="server" Visible="false">
              <table width="100%" cellpadding="2">
              <tr>
                <td width="120">Employee name</td>
                <td width="385">
                    <asp:TextBox ID="txtempname" runat="server"></asp:TextBox>
             </td>
                <td width="80">&nbsp;</td>
                <td>
                    &nbsp;</td>
              </tr>
              <tr>
                <td width="120">Job Type</td>
                <td width="385">
                    <asp:DropDownList ID="txtjobtype" runat="server" DataTextField="job_type" 
                                DataValueField="job_type">
                    </asp:DropDownList>
             </td>
                <td width="80">Job Title</td>
                <td>
                    <asp:DropDownList ID="txtjobtitle" runat="server"  DataTextField="job_title" 
                                DataValueField="job_title">
                    </asp:DropDownList>
                  </td>
              </tr>
             
              </table>
              </asp:Panel>
              </td>
          </tr>
          <tr>
            <td colspan="4" align="right">
                <asp:Button ID="cmdSearch" runat="server" Text="Query report" CssClass="button-green2" />
              <asp:Button ID="cmdClear" runat="server" Text="Clear" CssClass="button-green2" />&nbsp;</td>
          </tr>
        </table>
        <br />
        <div class="small" style="margin-bottom: 3px; width: 98%">
          <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td><div align="right">
                <label>
         
            <asp:Button ID="cmdExport" runat="server" Text="Export to excel" Visible="False" />
         
                  &nbsp;</label></div></td>
              </tr>
          </table>
        </div>
        <asp:panel ID="Panel_Report1" runat="server" Visible="false">
		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Training Summary </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <%If Gridview1.Rows.Count > 1 Then%>
            <div id="chart1" style="margin-top:20px; margin-left:20px; width:600px; height:400px; float:left"></div>
            <div id="chart2" style="margin-top:10px; margin-left:20px; width:360px;height:400px; float:left"></div>
            <%end if %>
            <br style="float:none" />
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Training report information from&nbsp;
                      <asp:Label ID="lblDate1" runat="server" Text="Label"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblDate2" runat="server" Text="Label"></asp:Label>
                    </td>
                  <td width="300" valign="top">Found 
                      <asp:Label ID="lblNum" runat="server" Text="0"></asp:Label> records</td>
                </tr>
              </table>
            </div>
                <asp:Button ID="cmdExternalExcel" runat="server" Text="Export Excel" />
            <br />
                     <asp:GridView ID="Gridview1" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="100" 
                    ShowFooter="True">
           <Columns>
               <asp:BoundField DataField="report_dept_id" HeaderText="Cost Center" />
               <asp:TemplateField HeaderText="Department name">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"  />
                   <ItemTemplate>
                    
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("report_dept_name") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
     <asp:TemplateField HeaderText="Total Request">
                 <ItemStyle VerticalAlign="Top" Width="100px"  />
                   <ItemTemplate>
                       <asp:Label ID="lblTotalRequest" runat="server" Text='<%# bind("total_request") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Total Approve">
                  <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>  
                   <asp:Label ID="lblReq" runat="server" Text='<%# Bind("num") %>'></asp:Label>&nbsp;
                       (<asp:Label ID="lblnum" runat="server" Text='<%# Bind("num") %>'></asp:Label>)
                         <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("total") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
                   <ItemStyle Width="110px" />
               </asp:TemplateField>
    
               <asp:TemplateField HeaderText="Budget Approved">
                 
                   
                 <ItemStyle Width="120px" HorizontalAlign="Right" VerticalAlign="Top" />
                   <ItemTemplate>
                       <asp:Label ID="lblBudget" runat="server" Text='<%# FormatNumber(Eval("budget")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Actual expense">
                   <ItemStyle Width="120px" HorizontalAlign="Right" />
                   <ItemTemplate>
                       <asp:Label ID="lblExpense" runat="server" Text='<%# FormatNumber(Eval("expense")) %>' ForeColor="Red"></asp:Label>
                        <asp:Label ID="txtexpense" runat="server" Text='<%# Eval("expense") %>'  Visible="false"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Training hour">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblHour" runat="server" Text='<%# FormatNumber(Eval("train_hour"),1) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <FooterStyle BackColor="#cccccc" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
          &nbsp;<br />
          
          
          </td>
		    </tr>
		  </table>
          <br />
          </asp:panel>


          <asp:panel ID="Panel_Motivation" runat="server" Visible="false">
          	<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">External Training Summary by Motivation</td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
          <div id="g3" style="margin-top:20px; margin-left:20px; width:650px; height:400px; "></div>
            <div class="statdesc">
                Training report information from&nbsp;
                <asp:Label ID="lblDate3" runat="server" Text="Label"></asp:Label>
                &nbsp;to
                <asp:Label ID="lblDate4" runat="server" Text="Label"></asp:Label>
            </div>
            <br />

            <table width="100%"  cellpadding="3" cellspacing="2" class="stattable">
            <tr><td width="450px" class="dataHeader center">Motivation</td>
            <td class="dataHeader center">No.</td>
                <td class="dataHeader center">
                    Percent</td>
            </tr>
            <tr>
            <td class="style2">M1 หัวข้อนี้เป็นหัวข้อที่ระบุอยู่ในแผนพัฒนาตนเองของฉัน</td>
            <td class="style2">
                <asp:Label ID="lbln1" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label ID="lblp1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
                <tr>
                    <td>
                        M2 เพื่อให้ตนเองสามารถปรับระดับ Career Ladder</td>
                    <td>
                        <asp:Label ID="lbln2" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblp2" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        M3 การพัฒนาในหัวข้อนี้ ฉันได้รับคำแนะนำจาก หัวหน้างานของฉัน</td>
                    <td class="style2">
                        <asp:Label ID="lbln3" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:Label ID="lblp3" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        M4 การพัฒนาในหัวข้อนี้ ฉันได้จาการประเมินความสามารถของตัวฉันเอง</td>
                    <td>
                        <asp:Label ID="lbln4" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblp4" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        M5 ฉันต้องการเตรียมพร้อมตัวเองเพื่ออนาคตในหน้าที่การงานของตัวเองด้วยหัวข้อนี้</td>
                    <td>
                        <asp:Label ID="lbln5" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblp5" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        M6 องค์กรของฉันเล็งเห็นว่าหัวข้อนี้เป็นเรื่องที่สำคัญ</td>
                    <td>
                        <asp:Label ID="lbln6" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblp6" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        M7 อื่นๆ</td>
                    <td>
                        <asp:Label ID="lbln7" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblp7" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
            
          </td>
		    </tr>
		  </table>
            </asp:panel>  
		

          <asp:panel ID="panel_request" runat="server" Visible="false">
          	<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Action After Training Summary Report </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <%If Gridview3.Rows.Count > 1 Then%>
            <div id="chart3b" style="margin-top:20px; margin-left:50px; width:760px; height:400px;"></div>
            <%Else%>
             <div id="<%response.write("chart3b") %>" style="margin-top:20px; margin-left:50px; width:360px; height:400px;"></div>
            <%End If%>
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Training information from&nbsp;
                      <asp:Label ID="lblDate5" runat="server" Text="Label"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblDate6" runat="server" Text="Label"></asp:Label>
                    </td>
                  <td width="300" valign="top">&nbsp;</td>
                </tr>
              </table>
            </div>
            <br />

 
            
            <asp:GridView ID="Gridview3" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50">
           <Columns>
               <asp:TemplateField HeaderText="Department name">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                    
                       <asp:Label ID="lblDept" runat="server" Text='<%# Bind("report_dept_name") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Total Request" Visible="false">
                 <ItemStyle VerticalAlign="Top" Width="100px"  />
                   <ItemTemplate>
                       <asp:Label ID="lblTotal" runat="server" Text='<%# bind("total") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Total Approved" Visible="false">
                 <ItemStyle VerticalAlign="Top" Width="100px"  />
                   <ItemTemplate>
                   
                      <asp:Label ID="lblIRNo" runat="server" Text='<%# bind("approve") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Total Finished">
                 <ItemStyle VerticalAlign="Top" Width="100px" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="lblFinish" runat="server" Text='<%# bind("complete") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Action After Training Recording">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblActionComplete" runat="server" Text='<%# bind("action1") %>'></asp:Label>
                       
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="%">
                 
                   <ItemTemplate>
                        <asp:Label ID="lblActionPercent" runat="server" Text=''></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
        <br />
          <asp:GridView ID="GridviewActionList" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50" 
                    
                    Caption="&lt;strong&gt;Employee List (waiting for actions after training)&lt;/strong&gt;" 
                    ForeColor="Red">
           <Columns>
               <asp:TemplateField HeaderText="Department name">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                    
                       <asp:Label ID="lblDept" runat="server" Text='<%# Bind("report_dept_name") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Employee name">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblTotal" runat="server" Text='<%# bind("report_by") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Training title">
                 <ItemStyle VerticalAlign="Top"  />
                   <ItemTemplate>
                   
                      <asp:Label ID="lblIRNo" runat="server" Text='<%# bind("ext_title") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Finish date">
                 <ItemStyle VerticalAlign="Top" Width="100px" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="lblFinish" runat="server" Text='<%# ConvertTSToDateString(Eval("date_end_ts")) %>'></asp:Label>
                          <asp:Label ID="lblDateTS" runat="server" Text='<%# bind("date_end_ts") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="TAT" >
                 <ItemStyle Width="100px" />
                   <ItemTemplate>
                       <asp:Label ID="lblTAT" runat="server" Text=''></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
          </td>
		    </tr>
		  </table>
            </asp:panel>  
       <br />
   
          <asp:panel ID="panel_individual" runat="server" Visible="false">
         		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">External Training Summary </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
          
            <br style="float:none" />
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Training report information from&nbsp;
                      <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
&nbsp;to
                      <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                    </td>
                  <td width="300" valign="top">Found 
                      <asp:Label ID="lblExternalNum" runat="server" Text="0"></asp:Label> records</td>
                </tr>
                  <tr>
                      <td valign="top">
                          <asp:Button ID="cmdExtPersonExcel" runat="server" Text="Export Excel" />
                      </td>
                      <td valign="top" width="300">
                          &nbsp;</td>
                  </tr>
              </table>
            </div>
            <br />
           <asp:GridView ID="GridIndividual" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" 
                  CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50" 
                 >
           <Columns>
               <asp:TemplateField HeaderText="Training Title">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                    
                       <asp:Label ID="lblDept" runat="server" Text='<%# Bind("ext_title") %>' Font-Bold="true"></asp:Label><br />
                      Training No. :  
                        <a href="ext_training_detail.aspx?mode=edit&id=<%# Eval("idp_id") %>&req=<%# Eval("request_type") %>">
                      <asp:Label ID="Label3" runat="server" Text='<%# Bind("idp_no") %>'></asp:Label></a><br />
                       Status :  <asp:Label ID="Label4" runat="server" Text='<%# Bind("status_name") %>'></asp:Label><br />
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Start date">
                 <ItemStyle VerticalAlign="Top" Width="100px"  />
                   <ItemTemplate>
                   
                      <asp:Label ID="lblIRNo" runat="server" Text='<%# ConvertTSToDateString(Eval("date_start_ts")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Finish date">
                 <ItemStyle VerticalAlign="Top" Width="100px" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="lblFinish" runat="server" Text='<%# ConvertTSToDateString(Eval("date_end_ts")) %>'></asp:Label>
                          <asp:Label ID="lblDateTS" runat="server" Text='<%# bind("date_end_ts") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Training Hours">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblHour" runat="server" Text='<%# Bind("train_hour") %>' Font-Bold="true"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Expense">
                 
                   <ItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# formatNumber(Eval("expense")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Action after training" >
                 <ItemStyle  />
                   <ItemTemplate>
                       <asp:Label ID="lblAction" runat="server" Text='<%# Bind("action_complete") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
          </td>
          </tr>
          </table>
          </asp:panel>


             <asp:panel ID="panel_dept_individual" runat="server" Visible="false">
                   <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Training report information from&nbsp;
                      <asp:Label ID="lblDateExtHistory1" runat="server" ForeColor="red" Text=""></asp:Label>
&nbsp;to
                      <asp:Label ID="lblDateExtHistory2" runat="server" ForeColor="red" Text=""></asp:Label>
                    </td>
                  <td width="300" valign="top">Found 
                      <asp:Label ID="Label14" runat="server" Text="0"></asp:Label> records</td>
                </tr>
                  <tr>
                      <td valign="top">
                          <asp:Button ID="cmdExtPersonExcel0" runat="server" Text="Export Excel" />
                      </td>
                      <td valign="top" width="300">
                          &nbsp;</td>
                  </tr>
              </table>
            </div>
           <asp:GridView ID="GridDeptIndividual" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50" 
                    Caption="External Training history">
           <Columns>
               <asp:TemplateField HeaderText="Training Title">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                    
                       <asp:Label ID="lblDept" runat="server" Text='<%# Bind("ext_title") %>' Font-Bold="true"></asp:Label><br />
                      Training No. :  
                        <a href="ext_training_detail.aspx?mode=edit&id=<%# Eval("idp_id") %>&req=<%# Eval("request_type") %>">
                      <asp:Label ID="Label3" runat="server" Text='<%# Bind("idp_no") %>'></asp:Label></a><br />
                       Status :  <asp:Label ID="Label4" runat="server" Text='<%# Bind("status_name") %>'></asp:Label><br />
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:BoundField DataField="report_emp_code" HeaderText="Employee Code" />
  
               <asp:BoundField HeaderText="Employee name" DataField="report_by" />
  
               <asp:TemplateField HeaderText="Start date">
                 <ItemStyle VerticalAlign="Top" Width="100px"  />
                   <ItemTemplate>
                   
                      <asp:Label ID="lblIRNo" runat="server" Text='<%# ConvertTSToDateString(Eval("date_start_ts")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Finish date">
                 <ItemStyle VerticalAlign="Top" Width="100px" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="lblFinish" runat="server" Text='<%# ConvertTSToDateString(Eval("date_end_ts")) %>'></asp:Label>
                          <asp:Label ID="lblDateTS" runat="server" Text='<%# bind("date_end_ts") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Training Hours">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblHour" runat="server" Text='<%# Bind("train_hour") %>' Font-Bold="true"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Actual Expense">
                 
                   <ItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# formatNumber(Eval("expense")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Action after training" >
                 <ItemStyle  />
                   <ItemTemplate>
                       <asp:Label ID="lblAction" runat="server" Text='<%# Bind("action_complete") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
          </asp:panel>


           <asp:panel ID="panel_internal_summary" runat="server" Visible="false">
           <br /><br />
		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Internal Training Summary by % Target Staff Attended1 </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
          
            <br style="float:none" />
            <div class="statdesc" id="internal_summary" runat="server">
                
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Training report information from&nbsp;
                      <asp:Label ID="lblIntDate1" runat="server" ForeColor="Red" Text="Label"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblIntDate2" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                    </td>
                  <td width="300" valign="top">Found 
                      <asp:Label ID="lblNumInternal1" runat="server" Text="0"></asp:Label> records</td>
                </tr>
                  <tr>
                      <td valign="top">
                          <asp:DropDownList ID="txtinternal_topic" runat="server" 
                              DataTextField="internal_title" DataValueField="internal_title">
                          </asp:DropDownList>
                          <br /><asp:Button ID="cmdInterSummarySearch" runat="server" Text="Change topic" />
                      </td>
                      <td valign="top" width="300">
                          &nbsp;</td>
                  </tr>
              </table>
            
            <br />
            
            </div>
           
          <strong>Internal Training Summary by % Target Staff Attended</strong>
                     <asp:GridView ID="Gridview4" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50" 
                    EmptyDataText="There is no data.">
           <Columns>
           
  
               <asp:TemplateField HeaderText="Course Topic">
                  
                   <ItemTemplate>
                     <asp:Label ID="lblID" runat="server" Text='<%# Bind("idp_id") %>' Visible="false"></asp:Label>
                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("internal_title") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Training Date">
                   <ItemTemplate>
                       <asp:Label ID="lblDateStart" runat="server"></asp:Label>
                       -
                       <asp:Label ID="lblDateEnd" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="No. of Targeted Staff ">
                   
                   <ItemTemplate>
                       <a href="javascript:;" onclick="window.open('popup_report_internal_summary.aspx?id=<%# Eval("idp_id") %>')">
                       <asp:Label ID="Label2" runat="server" Text='<%# FormatNumber(Eval("target_num"),0) %>'></asp:Label></a>
                         <asp:Label ID="lblTargetNum" runat="server" Text='<%# Eval("target_num") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
                  <ItemStyle Width="110px" />
               </asp:TemplateField>
    
               <asp:TemplateField HeaderText="Targeted Staff Attended">
                 
                   <ItemTemplate>
                        <asp:Label ID="lblTargetAttend" runat="server" Text='<%# Bind("total_require") %>' ></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="% Targeted Staff">
                
                   <ItemTemplate>
                       <asp:Label ID="lblPercent" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Total Staff Attended">
                   <ItemTemplate>
                       <asp:Label ID="lblReq" runat="server" Text='<%# Bind("num_employee") %>'></asp:Label>
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="110px" />
               </asp:TemplateField>

           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
          &nbsp;<br />
          
          
          </td>
		    </tr>
		  </table>
          </asp:panel>

            <asp:panel ID="panel_internal_history" runat="server" Visible="false">
		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Internal Training History / Attendance Report</td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
          
            <br style="float:none" />
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Internal Training Report information from&nbsp;
                      <asp:Label ID="lblDateInternal1" runat="server" Text="-" ForeColor="Red" ></asp:Label>
&nbsp;to 
                      <asp:Label ID="lblDateInternal2" runat="server" Text="-" ForeColor="Red" ></asp:Label>
                    </td>
                  <td width="300" valign="top">Found 
                      <asp:Label ID="lblInternalNum" runat="server" Text="0"></asp:Label> records</td>
                </tr>
                  <tr>
                      <td valign="top" colspan="2">
                         Course :  <asp:DropDownList ID="txtinternal_topic_person" runat="server" AutoPostBack="True" 
                              DataTextField="internal_title" DataValueField="internal_title">
                          </asp:DropDownList>
                      </td>
                  </tr>
                  <tr>
                      <td valign="top">
                          <asp:Button ID="cmdExcelInternal1" runat="server" Text="Export to Excel" />
                      </td>
                      <td valign="top" width="300">
                          &nbsp;</td>
                  </tr>
              </table>
            </div>
            <br />
                     <asp:GridView ID="Gridview5" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50" 
                    EmptyDataText="There is no data.">
           <Columns>
               <asp:TemplateField HeaderText="Schedule">
                  
                   <ItemTemplate>
                       <asp:Label ID="lblStartxx" runat="server" Text='<%# ConvertTSToDateString(Eval("schedule_start_ts")) %>' Visible="true"></asp:Label>
                        &nbsp;<asp:Label ID="Label7" runat="server" Text='<%# ConvertTSTo(Eval("schedule_start_ts") , "hour").PadLeft(2,"0") %>' Visible="true"></asp:Label>:
                         <asp:Label ID="Label8" runat="server" Text='<%# ConvertTSTo(Eval("schedule_start_ts") , "min").PadLeft(2,"0") %>' Visible="true"></asp:Label>
                         <br />to<br />
                         <asp:Label ID="lblEndxx" runat="server" Text='<%# ConvertTSToDateString(Eval("schedule_end_ts")) %>' Visible="true"></asp:Label>
                          &nbsp;<asp:Label ID="Label9" runat="server" Text='<%# ConvertTSTo(Eval("schedule_end_ts") , "hour").PadLeft(2,"0") %>' Visible="true"></asp:Label>:
                           <asp:Label ID="Label12" runat="server" Text='<%# ConvertTSTo(Eval("schedule_end_ts") , "min").PadLeft(2,"0") %>' Visible="true"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="schedule_type" HeaderText="Training Type" />
               <asp:BoundField DataField="emp_code" HeaderText="Employee code" />
               <asp:TemplateField HeaderText="Employee name">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"  />
                   <ItemTemplate>
                    
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("emp_name") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:BoundField DataField="dept_name" HeaderText="Department Name" />
  
               <asp:TemplateField HeaderText="Course Topic">
                  
                   <ItemTemplate>
                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("internal_title") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Training Date">
                   <ItemTemplate>
                       <asp:Label ID="Label2" runat="server" 
                           Text='<%# Bind("schedule_start", "{0:dd/MM/yyyy}") %>'></asp:Label>
                       -
                       <asp:Label ID="Label4" runat="server" 
                           Text='<%# Bind("schedule_end", "{0:dd/MM/yyyy}") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Date Attended ">
                  <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>  
                   <asp:Label ID="lblDateAttend" runat="server"></asp:Label>
                      <asp:Label ID="lblRegisterTime" runat="server" 
                           Text='<%# Bind("register_time") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblIsRegister" runat="server" 
                           Text='<%# Bind("is_register") %>' Visible="false"></asp:Label> 
                   </ItemTemplate>
                   <ItemStyle Width="110px" />
               </asp:TemplateField>
    
               <asp:BoundField DataField="attendance_type_name1" HeaderText="Attendance Type" />
    
               <asp:TemplateField HeaderText="Training Hour">
                 <ItemStyle HorizontalAlign="Right"  />
                   <ItemTemplate>
                       <asp:Label ID="lblHour" runat="server" Text='<%# Bind("work_hour") %>'></asp:Label>
                        <asp:Label ID="lblStart" runat="server" Text='<%# Bind("schedule_start_ts") %>' Visible="false"></asp:Label>
                         <asp:Label ID="lblEnd" runat="server" Text='<%# Bind("schedule_end_ts") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
    
               <asp:TemplateField HeaderText="Evaluation" Visible="False">
                 
                   <ItemTemplate>
                        <asp:Label ID="lblEvaluate" runat="server" Text='<%# Bind("evaluate") %>' ></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>

           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
          &nbsp;<br />
          
          
          </td>
		    </tr>
		  </table>
          </asp:panel>


          <asp:panel ID="panel_external_expect" runat="server" Visible="false">
		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">External Training Expect Outcome (Budget)</td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
          
            <br style="float:none" />
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Training report information from&nbsp;
                      <asp:Label ID="lblExpectDate1" runat="server"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblExpectDate2" runat="server"></asp:Label>
                    </td>
                  <td width="300" valign="top">&nbsp;</td>
                </tr>
                  <tr>
                      <td valign="top" colspan="2">
                          <div ID="chartExternalBudget" 
                              style="margin-top:20px; margin-left:20px; width:600px; height:400px; float:left ; display:none">
                          </div>
                      </td>
                  </tr>
                  <tr>
                      <td valign="top">
                          &nbsp;</td>
                      <td valign="top" width="300">
                          &nbsp;</td>
                  </tr>
              </table>
            </div>
             <strong>New Data (Valid from 1 July 2012)</strong><br />
             <asp:GridView ID="GridViewDynamicExternalExpect" runat="server"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" 
              EmptyDataText="There is no data." 
        EnableModelValidation="True">
          <Columns>
             
              <asp:BoundField />
          </Columns>
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
       <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="#eef1f3" />
         <AlternatingRowStyle BackColor="White" />
      
</asp:GridView>
            <br />
            <strong>Old Data (Valid until 30 Jun 2012)</strong><br />
                     <asp:GridView ID="GridExpectOutcome" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50" 
                    EmptyDataText="There is no data.">
           <Columns>
               <asp:TemplateField HeaderText="Expect Outcome">
                
                   <ItemTemplate>
                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("expect_detail") %>'></asp:Label>
                       <br />
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("expect_detail_en") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Budget (BAHT)">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"  />
                   <ItemTemplate>
                    
                       <asp:Label ID="Label1" runat="server" Text='<%# FormatNumber(Eval("budget"),0) %>'></asp:Label> 
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Expense (BAHT)">
                
                   <ItemTemplate>
                         <asp:Label ID="Label2" runat="server" Text='<%# FormatNumber(Eval("expense"),0) %>'></asp:Label> 
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:BoundField DataField="num" HeaderText="Total Request" />
               <asp:TemplateField HeaderText="%">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("total") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblPercent" runat="server" Text='<%# FormatNumber((Eval("num")/ Eval("total")) * 100,2) %>' Visible="true"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>


          &nbsp;<br />
          
          
          </td>
		    </tr>
		  </table>
          </asp:panel>

            <asp:panel ID="panel_internal_expect" runat="server" Visible="false">
		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Internal Training Summary by Expected Outcome</td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
          
            <br style="float:none" />
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Training report information from&nbsp;
                      <asp:Label ID="lblInternalDate1" runat="server"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblInternalDate2" runat="server"></asp:Label>
                    </td>
                  <td width="300" valign="top">&nbsp;</td>
                </tr>
                  <tr>
                      <td valign="top">
                          &nbsp;</td>
                      <td valign="top" width="300">
                          &nbsp;</td>
                  </tr>
                    <tr>
                      <td valign="top" colspan="2">
                          <div id="chartInternalBudget" 
                              style="margin-top:20px; margin-left:20px; width:600px; height:500px; float:left">
                          </div>
                      </td>
                  </tr>
              </table>
            </div>
            <br />
                     <asp:GridView ID="GridExpectOutcomeInternal" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50" 
                    EmptyDataText="There is no data.">
           <Columns>
               <asp:TemplateField HeaderText="Expect Outcome">
                 
                   <ItemTemplate>
                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("expect_detail") %>'></asp:Label>
                       
                   </ItemTemplate>
               </asp:TemplateField>
             <asp:TemplateField HeaderText="Budget (BAHT)">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"  />
                   <ItemTemplate>
                    
                       <asp:Label ID="Label1" runat="server" Text='<%# FormatNumber(Eval("budget"),0) %>'></asp:Label> 
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Expense (BAHT)">
                
                   <ItemTemplate>
                         <asp:Label ID="Label2" runat="server" Text='<%# FormatNumber(Eval("expense"),0) %>'></asp:Label> 
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Total">
                   <EditItemTemplate>
                       <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("num") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:Label ID="Label4" runat="server" Text='<%# Bind("num") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="%">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("total") %>' Visible="false"></asp:Label>
                       <asp:Label ID="lblNum" runat="server" Text='<%# Bind("num") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblPercent" runat="server" Text='<%# formatnumber( (Eval("num")/Eval("Total")) * 100,2) %>' Visible="true"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
          &nbsp;<br />
          
          
          </td>
		    </tr>
		  </table>
          </asp:panel>

           <asp:panel ID="panel_external_actiondetail" runat="server" Visible="false">
		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Action After Training Detail</td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
          
            <br style="float:none" />
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Training report information from&nbsp;
                      <asp:Label ID="lblActionDate1" runat="server"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblActionDate2" runat="server"></asp:Label>
                    </td>
                  <td width="300" valign="top">&nbsp;</td>
                </tr>
                  <tr>
                      <td valign="top">
                          &nbsp;</td>
                      <td valign="top" width="300">
                          &nbsp;</td>
                  </tr>
              </table>
            </div>
            <br />
                     <asp:GridView ID="GridActionDetail" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50" 
                    EmptyDataText="There is no data.">
           <Columns>
               <asp:BoundField DataField="action_detail" HeaderText="Action Detail" />
               <asp:TemplateField HeaderText="Amount">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"  />
                   <ItemTemplate>
                    
                       <asp:Label ID="lblNum" runat="server" Text='<%# Bind("num") %>'></asp:Label>
                        <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("total") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Percent">
                   
                   <ItemTemplate>
                       <asp:Label ID="lblPercent" runat="server"></asp:Label> %
                   </ItemTemplate>
               </asp:TemplateField>
  
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
          &nbsp;<br />
          
          
          </td>
		    </tr>
		  </table>
          </asp:panel>

         <asp:panel ID="panelDynamic" runat="server" Visible="false">
		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		 
		  <tr>
		    <td valign="top" style="padding: 10px;">
          
            <br style="float:none" />
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Training report information from&nbsp;
                      <asp:Label ID="lblDynamicDate1" runat="server"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblDynamicDate2" runat="server"></asp:Label>
                    </td>
                  <td width="300" valign="top">&nbsp;</td>
                </tr>
                  <tr>
                      <td valign="top">
                          &nbsp;</td>
                      <td valign="top" width="300">
                          &nbsp;</td>
                  </tr>
              </table>
            </div>
            <br />
                 <asp:GridView ID="GridViewDynamic" runat="server"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" 
              EmptyDataText="There is no data." 
        EnableModelValidation="True"  Visible="True">
          <Columns>
             
              <asp:BoundField />
          </Columns>
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
       <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="#eef1f3" />
         <AlternatingRowStyle BackColor="White" />
      
</asp:GridView>
          &nbsp;<br />
          
          
          </td>
		    </tr>
		  </table>
          </asp:panel>
      </div>
</asp:Content>

