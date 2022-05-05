<%@ Page Title="" Language="VB" MasterPageFile="~/ssip/SSIP_MasterPage.master" AutoEventWireup="false" CodeFile="ssip_message.aspx.vb" Inherits="ssip_ssip_message" %>
<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="data">
    <strong><img src="../images/menu_05.png" alt="SSIP" width="32" height="32"   />Definition of Innovation</strong>
    <br />
    
    <br />
   
   
    <br />

      <asp:Label ID="lblInnovation" runat="server" Text="-"></asp:Label>
</div>
  <div id="data1">
<table width="100%" align="center" cellpadding="3" cellspacing="0">
  <tr>
    <td width="50%"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Staff Suggestion &amp; Innovation News
        <%  If findArrayValue(priv_list, "204") = True Then ' HR %> 
        <a href="#" style="color:Yellow"  id="addNew"  onclick="window.open('ssip_news_edit.aspx?mode=add', '', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600')";>[Add new]</a>
        <%End If %>
         </td>
      </tr>
      <tr>
        <td><div style="height: 250px; overflow-x:hidden;overflow-y:auto;">
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
                <a href="#" onclick=" $.showAkModal('ssip_news_detail.aspx?id=<%# Eval("new_id") %>', '<%# Eval("title_th") %>', 600, 300);">
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
    <td width="50%" valign="top"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Staff Suggestion &amp; Innovation Overview</td>
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
    </table>
  </td>
  </tr>

  <tr>
    <td width="50%" valign="top"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Staff Suggestion &amp; Innovation Topics</td>
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
        <td style="background: #366; color: #FFF; font-weight: bold; padding: 5px 8px; font-size: 14px;">Top 20 : Staff Suggestion &amp; Innovation Submitted</td>
      </tr>
      <tr>
        <td>
    <asp:GridView ID="GripSSIP2" runat="server" Width="100%"  AutoGenerateColumns="False"
          EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="dept_name_en" HeaderText="Department name" />
          
            <asp:TemplateField HeaderText="No. of SSIP">
             
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

