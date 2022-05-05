<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="assessment_list.aspx.vb" Inherits="jci2013_accessment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table class="box-header" width="100%">
        <tbody>
          <tr>
            <td><img alt="web" src="../images/web.png"  width="24" height="24" />&nbsp;&nbsp;
                <a href="form_list.aspx?menu=3">Form Setting</a> > Assessment list</td>
            <td style="PADDING-RIGHT: 15px" align="right">
                  Assessors Leader 
                <asp:DropDownList ID="txtleader" runat="server" AutoPostBack="True" DataTextField="emp_name" DataValueField="emp_code">
                </asp:DropDownList>
                <asp:Button ID="cmdNew" runat="server" Text="New Asscessment" /></td>
          </tr>
        </tbody>
      </table>
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True" 
        EmptyDataText="There is no data." AllowPaging="True">
        <Columns>
         <asp:TemplateField HeaderText="No.">
                                  <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="Assessors Leader">
             
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("emp_name")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Department" DataField="dept_name" />
            <asp:BoundField HeaderText="Type" DataField="type_name" />
            <asp:BoundField DataField="form_name" HeaderText="Form Name" />
            <asp:TemplateField HeaderText="Date Time">
            
                <ItemTemplate>
                   
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("assessment_date_str")%>'></asp:Label>
                  &nbsp;
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("assessment_time_str")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location">
               
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("location_name")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Score">
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <ItemTemplate>
                    <asp:Label ID="lblIP" runat="server" Text='<%# Bind("score")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
          
            <asp:BoundField DataField="rank" HeaderText="Rank" />
          
            <asp:BoundField DataField="create_date_raw" HeaderText="Create Date" DataFormatString="{0:dd/MM/yyyy hh:mm}" />
          
            <asp:BoundField HeaderText="Email Date" DataField="email_date_raw" />
          
            <asp:TemplateField>
               <ItemStyle HorizontalAlign="Center" BackColor="LightBlue" />
                <ItemTemplate>
                     <a href="assessment_detail.aspx?mode=edit&id=<%# Eval("assessment_id")%>&form_id=<%# Eval("form_id")%>&empcode=<%# Eval("emp_code")%>&ts=<%# Eval("ipad_timestamp")%>&menu=3"><asp:Label ID="Label2" runat="server" Text="Edit Assessment result"></asp:Label>
                         (<asp:Label runat="server" ID="lblNum" Text='<%# Eval("num")%>' />)

                     </a>
                </ItemTemplate>
            </asp:TemplateField>
          
            <asp:TemplateField>
                  <ItemStyle HorizontalAlign="Center" BackColor="LightBlue" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("assessment_id")%>' CommandName="onDeleteGroup" ImageUrl="../images/page_delete.png" OnClientClick="return confirm('Are you sure you want to delete ?')" ToolTip="Delete" />
                    <asp:LinkButton ID="LinkButton1" runat="server"  CommandArgument='<%# Eval("assessment_id")%>' CommandName="onDeleteGroup" ImageUrl="../images/page_delete.png" OnClientClick="return confirm('Are you sure you want to delete ?')" ToolTip="Delete" >Delete</asp:LinkButton>
                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Width="80px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <p>&nbsp;</p>
      <p>&nbsp;</p>
</asp:Content>

