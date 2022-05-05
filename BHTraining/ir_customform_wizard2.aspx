<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="ir_customform_wizard2.aspx.vb" Inherits="ir_customform_wizard2" %>

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
  <div id="data">
<table width="100%" bgcolor="#EEEEEE">
<tr>
<td width="500" style="vertical-align:top">
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4"  CssClass="tdata3"   Width="98%" 
        DataKeyNames="element_id">
                   <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                       <asp:TemplateField HeaderText="No.">
               <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                   <asp:Label ID="lblPk" runat="server" Text='<%# Bind("element_id") %>' Visible="false"></asp:Label>
                   <asp:TextBox ID="txtorder" runat="server" Width="25px" Text='<%# Bind("element_order_sort") %>'></asp:TextBox>
            </ItemTemplate>
           </asp:TemplateField>
                        <asp:TemplateField HeaderText="Element name (en)">
                         
                            <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="selectRow"  CommandArgument='<%#Eval("element_id") & "," & (Container.DataItemIndex + 1) %>'   Text='<%# Bind("element_name_en") %>'></asp:LinkButton>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Element name (th)">
                           
                             <ItemTemplate>
                             <asp:LinkButton ID="LinkButton11" runat="server" CommandName="selectRow"  CommandArgument='<%#Eval("element_id") & "," & (Container.DataItemIndex + 1) %>'   Text='<%# Bind("element_name_th") %>'></asp:LinkButton>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="type_name" HeaderText="Type" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" ForeColor="Red"
                                    CommandName="Delete" Text="Delete" CommandArgument='<%#Eval("element_id") %>'   ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
     <asp:Button ID="cmdOrder" runat="server" Text="Reorder" />
     </td>
<td width="80" style="vertical-align:middle; text-align:center">
    <div>
    </div>
    <div>
    </div>
    </td>
<td style="vertical-align:top">
 <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical">
   <table width="100%">
   <tr>
   <td width="100">Field Order</td>
   <td>
       <asp:TextBox ID="txtelementorder" runat="server"></asp:TextBox>
       &nbsp;<asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
       </td>
   </tr>
       <tr>
           <td width="100">
               Field type</td>
           <td>
               <asp:DropDownList ID="txtelementtype" runat="server" 
                   DataTextField="element_type_name" DataValueField="element_type_id">
               </asp:DropDownList>
           </td>
       </tr>
       <tr>
           <td width="100">
               Label (th)</td>
           <td>
               <asp:TextBox ID="txtnameth" runat="server" EnableViewState="False"></asp:TextBox>
           </td>
       </tr>
       <tr>
           <td width="100">
               Label (en)</td>
           <td>
               <asp:TextBox ID="txtnameen" runat="server" EnableViewState="False"></asp:TextBox>
           </td>
       </tr>
       
   </table> 
   <asp:Button ID="cmdAdd" runat="server" Text="Add new field" />
    <asp:Button ID="cmdSave" runat="server" Text="Save" />
    <asp:Button ID="cmdCancel" runat="server" Text="Cancel" />
    <asp:Button ID="cmdItem" runat="server" Text="Edit item"  />
     &nbsp;
    </asp:Panel>
    
   
</td>
   </tr>
</table>
<br />
 <div align="right" style="width:100%">
      <asp:Button ID="cmdPrevStep" runat="server" Text="&lt;&lt; Back" />&nbsp;
      <asp:Button ID="cmdNextStep" runat="server" 
          Text="Next >>"  /></div>
    
      
  </div>
   
</asp:Content>

