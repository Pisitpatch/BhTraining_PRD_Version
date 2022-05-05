<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_message.aspx.vb" Inherits="srp_srp_message" MaintainScrollPositionOnPostback="true" %>
<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript" src="../js/akmodal/dimmer.js"></script>
<script type="text/javascript" src="../js/akmodal/dimensions.pack.js"></script>
<script type="text/javascript" src="../js/akmodal/akModal.js"></script>

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


           s1 = [<%Response.Write(global_unit_num)%>];
        plot1 = $.jqplot('chart2', [s1], {
            grid: {
                drawBorder: false,
                drawGridlines: false,
                background: '#ffffff',
                shadow: false
            },
            axesDefaults: {

            },
            seriesDefaults: {
                renderer: $.jqplot.PieRenderer,
                rendererOptions: {
                    showDataLabels: true,
                    //  dataLabels: 'value' , 
                    sliceMargin: 4,
                    startAngle: -90
                     , lineLabels: true, lineLabelsLineColor: '#777'
                }
            },
            seriesColors: ["#f608ea", "#68f259", "#597df2", "#ffcc33"],
            title: '',
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

     <link rel="stylesheet" href="../js/slideshow/responsiveslides.css" />
  <link rel="stylesheet" href="../js/slideshow/themes.css" />
 <script type="text/javascript" src="../js/slideshow/responsiveslides.min.js"></script>
 <script type="text/javascript">
     $(function () {
         $(".rslides").responsiveSlides({
             auto: true,
             pager: true,
             nav: true,
             pause: true,
             speed: 800,
             maxwidth: 200,
             namespace: "transparent-btns"
         });
     });
</script>

         <style type="text/css">
/* Picture Thumbnails */
#thumbnails ul
{
    width: 90%;
    list-style: none;
}
#thumbnails li
{
    text-align: center;
    display: inline;
    width: 200px;
    height: 200px;
    float: left;
    margin-bottom: 20px;
}
        </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="data">
<table width="100%" cellpadding="2" cellspacing="1">
  <tr>
    <td><div id="header"><img src="../images/srp_logo_32.png" alt="1" width="32" 
            height="32" align="middle"  />&nbsp;&nbsp;<strong>Staff Recognition Program</strong></div></td>
  </tr>
</table>
         <div style="width:800px; margin:auto 0" >
            <table width="100%">
                <tr>
                    <td align="center" style="text-align:center">  <a href="srp_point_list.aspx"><img src="../images/BH-OTS-01.png" border="0" alt="check point" /></a>  </td>
                    <td align="center" style="text-align:center">  <a href="srp_shop.aspx"><img src="../images/BH-OTS-02.png" border="0" alt="check point" /></a></td>
                    <td align="center" style="text-align:center">  <a href="srp_self_register.aspx"><img src="../images/BH-OTS-04.png" border="0" alt="check point" /></a></td>
                </tr>

            </table>
           
      

        </div>
        <br />
        <asp:Label ID="lblSlideShow" runat="server" Text="*" Width="80%" ></asp:Label>
        <!--
   <div class="rslides_container">
       <ul class="rslides" id="slider1">
      <li><img src="../share/ots_card/1.jpg" alt=""></li>
      <li><img src="../share/ots_card/noo_Bumrung2.gif" alt=""></li>
      <li><img src="../share/ots_card/3.jpg" alt=""></li>
    </ul>
       </div>
        -->
        <br />
       
       <br />&nbsp;
        <ul id="thumbnails">
          <asp:ListView ID="PicturesListView" runat="server" ItemPlaceholderID="PicturesListItemPlaceholder">
                                <LayoutTemplate>
                                    <li id="PicturesListItemPlaceholder" runat="server"></li>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li> <img src="../share/ots_card/<%# Eval("banner_path") %>" alt='<%# Eval("banner_detail") %>'  height="200"  border="0" />
                <br /> <span style="color:blue"><%# Eval("banner_detail") %></span>
                                        
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
            </ul>
        <br style="clear:left" />
        <br />

      <asp:Label ID="lblInnovation" runat="server" Text="-"></asp:Label>
      <!--<img src="../images/sample.png" alt="SRP Banner" />-->
</div>
  <div id="data">
         <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="background: #38c038; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Hot news
                    <%  If findArrayValue(priv_list, "402") = True Then ' HR %> 
               <!--     <a id="addNew" ;="" href="#" onclick="window.open('srp_news_edit.aspx?mode=add', '', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600')" style="color:Yellow">[Add new]</a>
                    -->
                    <%End If%>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top">
                     <asp:DataList ID="DataList1" runat="server" RepeatColumns="2"  Width="100%" CaptionAlign="Left" CellPadding="3" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" GridLines="Horizontal">
                         <AlternatingItemStyle BackColor="#F7F7F7" />
                         <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                         <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <ItemStyle VerticalAlign="Top" BackColor="#E7E7FF" ForeColor="#4A3C8C"  />

                    <ItemTemplate>
                        <div >
                            <table>
                                <tr>
                                    <td width="60"><img src="../images/noo_Bumrung2.png" width="50"  /></td>
                                    <td> <h4>
                              
           <a href="#" style="color:black" onclick=" window.open('srp_news_detail.aspx?id=<%# Eval("new_id") %>', '<%# Eval("title_th") %>', 'x', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600');"><%# Eval("title_th") %></a>
                                <asp:Label ID="LabelIcon" runat="server" Text='<%# Eval("is_hot_new")%>'></asp:Label>
                                <br />
                                        <%# Eval("new_date_format") %>
                            </h4></td>
                                </tr>
                               
                            </table>
                           

                            <div >
                               
                          &nbsp;&nbsp;<a href="../share/star/attach_file/<%# Eval("file_path")%>" target="_blank"><asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("file_name")%>'></asp:Label></a>      <br />   
                       <%# Eval("detail_th")%></div>
                        <div class="spacer"></div>
                     <br /><br />

                        </div>
              

                    </ItemTemplate>
                  
                         <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                  
                </asp:DataList>
                    <div >
                        <asp:GridView ID="GridNews" runat="server" AutoGenerateColumns="False" CellPadding="6" CellSpacing="0" DataKeyNames="new_id" EnableModelValidation="True" ForeColor="#333333" GridLines="None" ShowHeader="False" Width="100%" Visible="False">
                            <RowStyle BackColor="#E3EAEB" />
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemStyle Width="100px" />
                                    <ItemTemplate>
                                        <img src="../images/lightbulb.png" width="16" height="16"  />
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("new_date", "{0:dd MMM yyyy}") %>'></asp:Label>
                                        <asp:Label ID="lblPK" runat="server" Text='<%# bind("new_id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="News Title">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red">[Edit]</asp:LinkButton>
                                        &nbsp;   <a href="#" onclick=" window.open('srp_news_detail.aspx?id=<%# Eval("new_id") %>', '<%# Eval("title_th") %>', 'x', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600');">
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
                    </div>

                      
                </td>
            </tr>
        </table>
<table width="100%" align="center" cellpadding="3" cellspacing="0">


  


  <tr>
    <td width="50%" style="vertical-align:top; width: 100%;" colspan="2"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: #38c038; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Staff Recognition Program by CoAST <asp:DropDownList ID="txtdate1" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
         </td>
      </tr>
      <tr>
        <td style="vertical-align:top"><div style="height: 380px; overflow-x:hidden;overflow-y:auto;">
  <div id="chart2" style="margin-top:10px; margin-left:0px; width:600px;height:350px; float:left"></div>
    <div style="float:left;width:300px;overflow:auto">
        <br />
        <br />
        <table width="100%" cellpadding="0" cellspacing="3" border="0">
               <tr><td>B1 = Compassionate Caring  </td></tr>
             <tr><td>B2 = Adaptability, Learning, and Innovation </td></tr>
             <tr><td>B3 = Safety, Quality with Measurable Results</td></tr>
             <tr><td>B4 = Teamwork and Integrity</td></tr>
         
            <!--
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
            -->
        </table>
       
    </div>
      </div></td>
      </tr>
    </table></td>
  </tr>
     <tr>
    <td  style="vertical-align:top; width: 100%;" colspan="2"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: #5D7B9D; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">CoAST by Department</td>
      </tr>
      <tr>
        <td>   <asp:GridView ID="GridCoastDept" runat="server" Width="100%"  AutoGenerateColumns="False"
          EnableModelValidation="True" CellPadding="4" ForeColor="#333333" 
                GridLines="None">
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:BoundField DataField="dept_name" HeaderText="Department name" 
                HeaderStyle-ForeColor="White" >
          
<HeaderStyle ForeColor="White"></HeaderStyle>
            </asp:BoundField>
          
            <asp:TemplateField HeaderText="CO">
             <HeaderStyle ForeColor="White" />
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# FormatNumber(Eval("c1"),0) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="c2" HeaderText="A" />
            <asp:BoundField DataField="c3" HeaderText="S" />
            <asp:BoundField DataField="c4" HeaderText="T" />
            <asp:BoundField DataField="total" HeaderText="Total" />
        </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td  style="vertical-align:top; width: 100%;" colspan="2"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: #5D7B9D; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Top 10 Department with highest On-The-Spot Reward acquirers</td>
      </tr>
      <tr>
        <td>   <asp:GridView ID="GripStarTopSubmit" runat="server" Width="100%"  AutoGenerateColumns="False"
          EnableModelValidation="True" CellPadding="4" ForeColor="#333333" 
                GridLines="None">
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:BoundField DataField="dept_name" HeaderText="Department name" 
                HeaderStyle-ForeColor="White" >
          
<HeaderStyle ForeColor="White"></HeaderStyle>
            </asp:BoundField>
          
            <asp:TemplateField HeaderText="SRP Point">
             <HeaderStyle ForeColor="White" />
                <ItemTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# FormatNumber(Eval("score"),0) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="num" HeaderText="Card Qty." />
        </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td width="50%" style="vertical-align:top">&nbsp;</td>
    <td width="50%" valign="top"></td>
  </tr>

</table>
</div>
</asp:Content>
