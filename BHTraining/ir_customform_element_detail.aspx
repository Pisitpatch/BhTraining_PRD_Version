<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="ir_customform_element_detail.aspx.vb" Inherits="ir_customform_element_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%"  border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td width="10">&nbsp;</td>
      <td valign="top"><br />
        <table width="100%"  border="0" cellspacing="0" cellpadding="3">
          <tr>
            <td height="25" background="images/header_bkgd.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td class="green"><img src="images/head_lefto.gif" width="5" height="24" align="absmiddle" /> 
                    Step 2</td>
              </tr>
              <tr>
                <td height="6" background="images/shadowbg.gif"><img src="images/shadow.gif" width="150" height="6" /></td>
              </tr>
            </table></td>
          </tr>
        </table>
<table width="100%"  border="0" cellspacing="0" cellpadding="3"  >
          <tr>
            <td ></td>
            <td height="5" bgcolor="#F7941D"></td>
            <td ></td>
            <td ></td>
          </tr>
          <tr>
            <td width="25%" height="25" bgcolor='#EFEFEF' >1.Add/Edit form</td>
            <td width="25%" bgcolor='#FFFFFF' style='font-weight:bold'>2. Add/Edit element</td>
            <td width="25%" bgcolor='#EFEFEF'>3. Preview your form</td>
            <td width="25%" bgcolor='#EFEFEF'>4. Finish</td>
          </tr>
        </table>
        </td>
      <td width="10">&nbsp;</td>
    </tr>
  </table>
    
  <br/>
 <asp:Panel ID="panelItem"  runat="server">
 <div id="data">
    <div>
     Label (TH) <asp:TextBox ID="txtnewitemth" runat="server"></asp:TextBox> 
     Label (EN) <asp:TextBox ID="txtnewitemen" runat="server"></asp:TextBox> 
     <asp:Button ID="cmdAddItem" runat="server" Text="Add" />
        &nbsp;<asp:Button ID="cmdBack" runat="server" Text="Back" />
    </div>
       <br />
    <asp:GridView ID="GridViewItem" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4"  CssClass="tdata3"   Width="98%" 
        DataKeyNames="sub_element_id">
      
                   <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                     
                        <asp:TemplateField HeaderText="Order">
                        <ItemStyle Width="50px" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtorder" runat="server" Width="40px" Text='<%# Bind("item_order_sort") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelOrder" runat="server" Width="40px" Text='<%# Bind("item_order_sort") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Choice (TH)">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtlabelth" runat="server" Text='<%# Bind("item_label_th") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("item_label_th") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Choice (EN)">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtlabelen" runat="server" Text='<%# Bind("item_label_en") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("item_label_en") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                    CommandArgument='<%#Eval("sub_element_id") %>' CommandName="Delete" 
                                    Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle  BackColor="Yellow" />
              <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
    
    </div>
    </asp:Panel>
</asp:Content>

