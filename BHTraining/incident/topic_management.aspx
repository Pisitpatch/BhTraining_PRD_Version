<%@ Page Title="" Language="VB" MasterPageFile="~/incident/Incident_MasterPage.master" AutoEventWireup="false" CodeFile="topic_management.aspx.vb" Inherits="incident.incident_topic_management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div id="header"><img src="../images/menu_12.png" width="32" height="32"  />&nbsp;&nbsp;Topic Management</div>
   
<div id="data">
    <asp:Label ID="lblError" runat="server"></asp:Label>
<div>
<asp:Button ID="cmdGrand" runat="server" Text="Add Grand Topic" 
        CausesValidation="False" /> 
<asp:Button ID="cmdTopic" runat="server" Text="Add Topic" 
        CausesValidation="False" /> 
<asp:Button ID="cmdSubTopic" runat="server" Text="Add Subtopic 1" 
        CausesValidation="False" /> 
<asp:Button ID="cmdSubTopic2" runat="server" Text="Add Subtopic 2" 
        CausesValidation="False" /> 
<asp:Button ID="cmdSubTopic3" runat="server" Text="Add Subtopic 3" 
        CausesValidation="False" /> 
<br /><br />
    <asp:Panel ID="Panel1" runat="server" Visible="false" BorderStyle="Solid" BorderWidth="1px">
    <br />
    <table width="100%">
    <tr><td width="100">Grand topic</td><td>
        <asp:TextBox ID="txtaddgrand" Width="450px" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"  ControlToValidate="txtaddgrand" ErrorMessage="Please enter Grand topic !" ></asp:RequiredFieldValidator>
        
        </td></tr>
        <tr>
            <td width="100">
                &nbsp;</td>
            <td>
                <asp:Button ID="cmdSaveGrandTopic" runat="server" Text="Save" 
                    Font-Bold="True" />
                &nbsp;<asp:Button ID="cmdCancel1" runat="server" Text="Cancel" 
                    CausesValidation="False" ForeColor="#FF3300" />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Visible="false" BorderStyle="Solid" BorderWidth="1px">
    <br />
    <table width="100%">
    <tr><td width="100">Grand topic</td><td>
        <asp:DropDownList ID="txtComboGrandTopic" runat="server" 
            DataTextField="grand_topic_name" DataValueField="grand_topic_id">
        </asp:DropDownList> 
        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
            ErrorMessage="Please enter Grand topic !" ControlToValidate="txtComboGrandTopic"></asp:RequiredFieldValidator>
        </td></tr>
        <tr>
            <td width="100">
                Topic</td>
            <td>
                <asp:TextBox ID="txtAddTopic" Width="450px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
            ErrorMessage="Please enter Topic !" ControlToValidate="txtAddTopic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="100">
                &nbsp;</td>
            <td>
                <asp:Button ID="cmdAddTopic" runat="server" Text="Save Topic" 
                    CausesValidation="true" Font-Bold="True" />
                &nbsp;<asp:Button ID="cmdDelTopic" runat="server" Text="Delete Topic" /> &nbsp;<asp:Button 
                    ID="cmdCancelTopic" runat="server" Text="Cancel" CausesValidation="false" 
                    ForeColor="#FF3300" />
            </td>
        </tr>
    </table>
    </asp:Panel>
       <asp:Panel ID="Panel3" runat="server" Visible="false" BorderStyle="Solid" BorderWidth="1px">
    <br />
    <table width="100%">
    <tr><td width="100" class="style1">Grand topic</td><td class="style1">
        <asp:DropDownList ID="txtComboGrandForSubTopic" runat="server" 
            DataTextField="grand_topic_name" DataValueField="grand_topic_id" 
            AutoPostBack="True">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
            ErrorMessage="Please enter Grand topic !" ControlToValidate="txtComboGrandForSubTopic"></asp:RequiredFieldValidator>
        </td></tr>
        <tr>
            <td width="100">
                Topic</td>
            <td>
                 <asp:DropDownList ID="txtComboTopic" runat="server" 
                    DataTextField="topic_name" DataValueField="ir_topic_id">
                </asp:DropDownList>
                
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
            ErrorMessage="Please enter Topic !" ControlToValidate="txtComboTopic"></asp:RequiredFieldValidator>
                 </td>
        </tr>
        <tr>
            <td width="100">
                Sub-Topic 1</td>
            <td>
             
                <asp:TextBox ID="txtAddSubTopic" Width="450px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
            ErrorMessage="Please enter Sub Topic1 !" ControlToValidate="txtAddSubTopic"></asp:RequiredFieldValidator>
            </td>
        </tr>
       
        <tr>
            <td width="100">
                &nbsp;</td>
            <td>
                <asp:Button ID="cmdAddSubTopic" runat="server" Text="Save Sub-topic 1" 
                    Font-Bold="True" />
                    &nbsp;<asp:Button ID="cmdDelSubTopic1" runat="server" Text="Delete Sub-topic 1" />
                &nbsp;<asp:Button ID="cmdCancelSubTopic" runat="server" Text="Cancel" 
                    CausesValidation="False" ForeColor="#FF3300" />
                </td>
        </tr>
    </table>
    </asp:Panel>
     <asp:Panel ID="Panel4" runat="server" Visible="false" BorderStyle="Solid" BorderWidth="1px">
    <br />
    <table width="100%">
    <tr><td width="100">Grand topic</td><td>
        <asp:DropDownList ID="txtComboGrandForSubTopic2" runat="server" 
            DataTextField="grand_topic_name" DataValueField="grand_topic_id" 
            AutoPostBack="True">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ControlToValidate="txtComboGrandForSubTopic2" 
            ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </td></tr>
        <tr>
            <td width="100">
                Topic</td>
            <td>
                 <asp:DropDownList ID="txtComboTopicForTopic2" runat="server" 
                    DataTextField="topic_name" DataValueField="ir_topic_id" AutoPostBack="True">
                </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                     ControlToValidate="txtComboTopicForTopic2" 
                     ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="100">
                Sub-Topic 1</td>
            <td>
             
                <asp:DropDownList ID="txtComboSubTopic1ForSubTopic2" runat="server" DataTextField="subtopic_name" 
                    DataValueField="ir_subtopic_id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="txtComboSubTopic1ForSubTopic2" 
                    ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            </td>
        </tr>
       
        <tr>
            <td width="100">
                Sub-Topic 2</td>
            <td>
                <asp:TextBox ID="txtAddSubTopic2" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
            <td width="100">
                &nbsp;</td>
            <td>
                <asp:Button ID="cmdAddSubTopic2" runat="server" Text="Save Sub-topic 2" Font-Bold="True" />
                &nbsp;<asp:Button ID="cmdDelSubTopic2" runat="server" Text="Delete Sub-topic 2" />
                &nbsp;<asp:Button ID="cmdCancel4" runat="server" Text="Cancel" 
                    CausesValidation="False" ForeColor="#FF3300" />
                </td>
        </tr>
    </table>
    </asp:Panel>
     <asp:Panel ID="Panel5" runat="server" Visible="false" BorderStyle="Solid" BorderWidth="1px">
    <br />
    <table width="100%">
    <tr><td width="100">Grand topic</td><td>
        <asp:DropDownList ID="txtComboGrandForSubTopic3" runat="server" 
            DataTextField="grand_topic_name" DataValueField="grand_topic_id" 
            AutoPostBack="True">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtComboGrandForSubTopic3" 
            ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </td></tr>
        <tr>
            <td width="100">
                Topic</td>
            <td>
                 <asp:DropDownList ID="txtComboTopicForTopic3" runat="server" 
                    DataTextField="topic_name" DataValueField="ir_topic_id" AutoPostBack="True">
                </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                     ControlToValidate="txtComboTopicForTopic3" 
                     ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="100">
                Sub-Topic 1</td>
            <td>
             
                <asp:DropDownList ID="txtComboSubTopic1ForSubTopic3" runat="server" DataTextField="subtopic_name" 
                    DataValueField="ir_subtopic_id" AutoPostBack="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtComboSubTopic1ForSubTopic3" 
                    ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                </td>
        </tr>
       
        <tr>
            <td width="100">
                Sub-Topic 2</td>
            <td>
                <asp:DropDownList ID="txtComboSubTopic2ForSubTopic3" runat="server" DataTextField="subtopic2_name_en" 
                    DataValueField="ir_subtopic2_id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtComboSubTopic2ForSubTopic3" 
                    ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="100">
                Sub-Topic 3</td>
            <td>
                <asp:TextBox ID="txtAddSubTopic3" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
            <td width="100">
                &nbsp;</td>
            <td>
                <asp:Button ID="cmdAddSubTopic3" runat="server" Text="Save Sub-topic 3" 
                    Font-Bold="True" />
                &nbsp;<asp:Button ID="cmdDelSubTopic3" runat="server" Text="Delete Sub-topic 3" />
                &nbsp;<asp:Button ID="cmdCancel5" runat="server" Text="Cancel" 
                    CausesValidation="False" ForeColor="#FF3300" />
                </td>
        </tr>
    </table>
    </asp:Panel>
</div>
<br />
 <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="grand_topic_id" 
              AllowPaging="True" RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data.">
              <RowStyle BackColor="#eef1f3" />
              <Columns>
                 <asp:TemplateField HeaderText="No.">
                 <HeaderStyle ForeColor="White" />
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" > <%# Container.DataItemIndex + 1 %>. </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                  <asp:TemplateField HeaderText="Grand Topic">
                  <ItemStyle VerticalAlign="Top" />
                    
                      <ItemTemplate>
                          <asp:Label ID="lblPK" runat="server" Text='<%# Bind("grand_topic_id") %>' Visible="false"></asp:Label>
                          <a href="topic_management.aspx?topic_type=<%response.write(topic_type) %>&g=<%# Eval("grand_topic_id") %>&mode=g"><asp:Label ID="Label1" runat="server" Text='<%# Bind("grand_topic_name") %>'></asp:Label></a> 
                        
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Topic">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                            <asp:Label ID="lblTopic" runat="server" Text='' ></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField ShowHeader="False">
                      <ItemStyle ForeColor="Red" />
                      <ItemTemplate>
                       <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                              CommandName="Delete" Text="Delete all" ForeColor="Red" OnClientClick="return confirm('Are you sure you want to delete this grand topic?')"></asp:LinkButton>
                      </ItemTemplate>
                      <ItemStyle ForeColor="Red" Width="80px" />
                  </asp:TemplateField>
              </Columns>
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#ABBBB4" ForeColor="White" HorizontalAlign="Center" 
                  BorderStyle="None" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
              
          </asp:GridView>
  </div>
 
</asp:Content>

