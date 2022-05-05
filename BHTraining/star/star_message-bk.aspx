<%@ Page Title="" Language="VB" MasterPageFile="~/star/Star_MasterPage.master" AutoEventWireup="false" CodeFile="star_message.aspx.vb" Inherits="star_star_message" %>

<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
         <script type="text/javascript" src="../js/media_play.js" charset="utf-8"></script>
 <script type="text/javascript" src="../js/media_chili.js" charset="utf-8"></script>
<script type="text/javascript">

    $(function () {

        //$.fn.media.mapFormat('avi','quicktime');

        // this one liner handles all the examples on this page

        $('a.media').media();

    });

</script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="data">
    <strong><img src="../images/star_32.png" alt="SSIP" width="32" height="32"   />News and Events</strong>
    <br />
    
    <br />
   
   
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
        <td style="vertical-align:top"><div style="height: 250px; overflow-x:hidden;overflow-y:auto;">
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
        <td style="background: pink; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">
            Star of Bumrungrad Channel</td>
      </tr>
      <tr>
        <td>
         <a class="media {width:450, height:370}" href="mediaplayer.swf?file=flash/<% response.write(clip_name) %>"></a> 
        </td>
      </tr>
    </table>
  </td>
  </tr>

  <tr>
    <td width="50%" valign="top"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: pink; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">
            Star of Bumrungrad Awards</td>
      </tr>
      <tr>
        <td><div style="height: 750px; overflow-x:hidden;overflow-y:auto;">
   
    <asp:GridView ID="GripSSIP1" runat="server" Width="100%"  AutoGenerateColumns="False"
          EnableModelValidation="True">
        <Columns>
            <asp:TemplateField HeaderText="Innovation Idea">
                <ItemTemplate>
                    <a href="form_ssip.aspx?mode=edit&id=<%# Eval("ssip_id") %>&viewtype=public"><asp:Label ID="Label1" runat="server" Text='<%# Bind("innovation_subject") %>'></asp:Label></a>
                </ItemTemplate>
               
            </asp:TemplateField>
          
            <asp:TemplateField HeaderText="Idea by">
             
                <ItemTemplate>
                    <asp:Label ID="lblNum0" runat="server" Text='<%# Bind("report_by") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="result_title" HeaderText="ผลการพิจารณา" />
            <asp:BoundField DataField="ssip_source_type_name" 
                HeaderText="ที่มาข้อเสนอแนะ" />
        </Columns>
      </asp:GridView>
   
      </div></td>
      </tr>
    </table></td>
    <td width="50%" valign="top"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: pink; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Top 20 : Star of Bumrungrad Submitted</td>
      </tr>
      <tr>
        <td>
    <asp:GridView ID="GripSSIP2" runat="server" Width="100%"  AutoGenerateColumns="False"
          EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="dept_name_en" HeaderText="Department name" />
          
            <asp:TemplateField HeaderText="No. of Star">
             
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


</table>
</div>
</asp:Content>

