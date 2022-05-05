<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="ir_customform_list.aspx.vb" Inherits="ir_customform_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id = "data">
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4"  CssClass="tdata3"   Width="98%" 
        DataKeyNames="form_id">
                   <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                       <asp:TemplateField HeaderText="No.">
               <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                   <asp:Label ID="lblPk" runat="server" Text='<%# Bind("form_id") %>' Visible="false"></asp:Label>
                   <asp:TextBox ID="txtorder" runat="server" Width="25px" Text='<%# Container.DataItemIndex + 1 %>'></asp:TextBox>
            </ItemTemplate>
           </asp:TemplateField>
                        <asp:TemplateField HeaderText="Form name (th)">
                         
                            <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="selectRow"  CommandArgument='<%#Eval("form_id") %>'   Text='<%# Bind("form_name_th") %>'></asp:LinkButton>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="form_name_en" HeaderText="Form name (en)" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" ForeColor="Red"
                                    CommandName="Delete" Text="Delete" CommandArgument='<%#Eval("form_id") %>'   ></asp:LinkButton>
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
</div>
</asp:Content>

