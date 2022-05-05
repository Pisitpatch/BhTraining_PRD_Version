<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_point_list.aspx.vb" Inherits="srp_srp_point_list" %>
<%@ Import Namespace="ShareFunction" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" cellpadding="2" cellspacing="1">
  <tr>
    <td><div id="header"><img src="../images/srp_logo_32.png" alt="1" width="32" height="32"  />&nbsp;&nbsp;<strong>Point Checking
    <br />สรุปคะแนนของคุณ <asp:Label ID="lblName" runat="server" /> ทั้งหมด คือ <asp:Label ID="lblSumScore" runat="server" ForeColor="Blue" /> คะแนน
    </strong></div></td>
  </tr>
</table>
<div id="data">
     <h3>Point Movement History</h3>
<asp:GridView ID="gridview1" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." AllowPaging="True" 
        ShowFooter="True" PageSize="25">
                  <HeaderStyle BackColor="#11720c" ForeColor="White" />
                  <FooterStyle  BackColor="#EEEEEE"  />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField HeaderText="Date">
                         <ItemStyle Width="100px" HorizontalAlign="Center" />
                          <ItemTemplate>
                              <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("movement_create_date_ts")) %>'></asp:Label>
                               <asp:Label ID="Label1" runat="server" Text='<%# ConvertTSTo(Eval("movement_create_date_ts"),"hour") %>'></asp:Label>:
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertTSTo(Eval("movement_create_date_ts"),"min") %>'></asp:Label>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("point_movement_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:Label ID="lblMovementID" runat="server" Text='<%# Bind("movememt_type_id") %>' 
                                  Visible="false"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="move_name" HeaderText="Reward ID." >
                      <ItemStyle Width="150px" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="Recognition">
                        
                          <ItemTemplate>
                          
                           
                              <asp:Label ID="lblTopicName" runat="server" Text='<%# Bind("reward_type_name") %>'></asp:Label>
                             
                       
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Transaction" DataField="movememt_type_name" 
                          ItemStyle-Width="100px"  >
                   
<ItemStyle Width="100px"></ItemStyle>
                   
                      </asp:BoundField>
                    
                    
                      <asp:TemplateField HeaderText="Point">
                      <ItemStyle HorizontalAlign="Right" />
                          <ItemTemplate>
                             <div style="text-align:right">
                        <asp:Label ID="lblScore" runat="server" Text='<%# Eval("point_trans") %>'></asp:Label>
                        </div>
                          </ItemTemplate>
                      <FooterTemplate>
                       <div style="text-align:right">
                       <asp:Label ID="lblTotalScore" runat="server" Font-Bold="true" ></asp:Label>
                       </div>
                      </FooterTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("movement_remark") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>

    <br />
    <h3>ประวัติการแลกซื้อสินค้า / Redeemed Items History</h3>
    <asp:GridView ID="gridview2" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." AllowPaging="True" 
        ShowFooter="True" PageSize="25">
                  <HeaderStyle BackColor="#11720c" ForeColor="White" />
                  <FooterStyle  BackColor="#EEEEEE"  />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField HeaderText="Date">
                        
                          <ItemTemplate>
                              <asp:Label ID="lblDate" runat="server" Text='<%# (Eval("Create Date"))%>'></asp:Label>
                              
                           
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:BoundField HeaderText="Time" DataField="Time" />
                      <asp:BoundField DataField="Payment Type" HeaderText="Payment Type" >
                    
                      </asp:BoundField>
                        <asp:BoundField HeaderText="OTS Card" DataField="OTS Card" />
                      <asp:TemplateField HeaderText="Item Name">
                        
                          <ItemTemplate>
                          
                           
                              <asp:Label ID="lblTopicName" runat="server" Text='<%# Eval("Item Name")%>'></asp:Label>
                             
                       
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Qty" DataField="Qty" 
                          ItemStyle-Width="100px"  >
                   
<ItemStyle Width="100px"></ItemStyle>
                   
                      </asp:BoundField>
                    
                       <asp:BoundField HeaderText="Cash" DataField="Cash" />
                        <asp:BoundField HeaderText="Point" DataField="Point" />
                   <asp:BoundField HeaderText="Credit" DataField="Credit" />
                      
                     
                        <asp:TemplateField HeaderText="Remark">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Sale Remark") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              </div>
</asp:Content>

