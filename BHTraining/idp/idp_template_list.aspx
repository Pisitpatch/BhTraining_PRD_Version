<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_template_list.aspx.vb" Inherits="idp_idp_template_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header"><img src="../images/doc01.gif" width="32" height="32"  />&nbsp;
    
    <asp:Label
            ID="lblHeader" runat="server" Text="IDP Program"></asp:Label>
          <div align="right">
              <asp:Button ID="cmdNew" runat="server" Text="Create New Program" />
        
          &nbsp;&nbsp;&nbsp;
  &nbsp;&nbsp;&nbsp;</div>
      </div>
       <div id="data">
<table width="100%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
    </tr>
   <tr>
    <td height="30">
    <table width="100%" cellpadding="3" cellspacing="0" >
    <tr>
    <td width="100"><strong>Year</strong></td>
    <td width="250">
        <asp:DropDownList ID="txtyear" runat="server" Width="80px" 
            DataTextField="template_yearno" DataValueField="template_yearno">
        </asp:DropDownList>
        </td>
    <td width="120">&nbsp;</td>
    <td>
        &nbsp;</td>
    </tr>
    <tr>
    <td width="100"><strong>Cost center</strong></td>
    <td width="250">
        <asp:DropDownList ID="txtdept" runat="server" DataTextField="dept_name_en" 
            DataValueField="dept_id" Width="200px">
        </asp:DropDownList>
        </td>
    <td width="120"><strong>Job title</strong></td>
    <td>
        <asp:DropDownList ID="txtjobtitle" runat="server" DataTextField="job_title" 
            DataValueField="job_title" Width="200px">
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td width="100"><strong>Job type</strong></td>
    <td width="250">
        <asp:DropDownList ID="txtjobtype" runat="server" DataTextField="job_type" 
            DataValueField="job_type" Width="200px">
        </asp:DropDownList>
        </td>
    <td width="120"><strong>Employee</strong></td>
    <td>
        <asp:TextBox ID="txtfindemp" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td width="100"><strong>Program No.</strong></td>
    <td width="250">
        <asp:TextBox ID="txtprogramno" runat="server"></asp:TextBox>
        </td>
    <td width="120"><strong>Program Name</strong></td>
    <td>
        <asp:TextBox ID="txtprogramname" runat="server"></asp:TextBox>
        </td>
    </tr>
    </table>
    </td>
    </tr>
 
  
  <tr>
    <td height="30">
        <asp:Button ID="cmdSearch" runat="server" Text="Search" Font-Bold="true" />  
      &nbsp;<asp:Button ID="cmdClear" runat="server" Text="Clear" />
      </td>
  </tr>
  
      </table>
        <br />
         <div id="div_hr" runat="server">
        <fieldset>
        <legend>Update status</legend>
         
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="140">
                    Change status to</td>
                <td>
            <asp:DropDownList ID="txtstatus" runat="server" >
            <asp:ListItem Value="0">Draft</asp:ListItem>
            <asp:ListItem Value="1">Active</asp:ListItem>
            </asp:DropDownList>
          &nbsp;
                    <asp:Button ID="cmdUpdateStatus" runat="server" Text="Update Status" OnClientClick="return confirm('Are you sure you want to change status ?')" />
                &nbsp;<asp:Button ID="cmdDelete" runat="server" Text="Delete Program" 
                        
                        OnClientClick="return confirm('Are you sure you want to delete template ?')" />
                </td>
            </tr>
        </table>
   
        </fieldset>
         </div>

           <div id="div_config" runat="server" visible="false">
        <fieldset>
        <legend>IDP Management</legend>
         
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="200">
                    Change IDP Submit status to</td>
                <td>
            <asp:DropDownList ID="txtconfig" runat="server" >
            <asp:ListItem Value="0">Pending all request</asp:ListItem>
            <asp:ListItem Value="1">Active</asp:ListItem>
            </asp:DropDownList>
          &nbsp;
                    <asp:Button ID="cmdConfig" runat="server" Text="Update Status" OnClientClick="return confirm('Are you sure you want to change status ?')" />
                &nbsp;</td>
            </tr>
        </table>
   
        </fieldset>
         </div>
    <br />
        <div class="small" style="margin-bottom: 3px; width: 98%">
          <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td valign="bottom"><span class="small">
                  <asp:Label ID="lblNum" runat="server" Text=""></asp:Label> Records Found</span></td>
              <td style="text-align: right;">&nbsp;</td>
            </tr>
          </table>
        </div>
    <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="template_id" 
              AllowPaging="True" PageSize="20" RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data." 
               EnableModelValidation="True">
        <RowStyle BackColor="#eef1f3" />
        <Columns>
            
         <asp:TemplateField>
                <HeaderTemplate>
                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" OnCheckedChanged="onCheckAll" AutoPostBack="True" />
            </HeaderTemplate>
                    <ItemTemplate>
                     
                      <asp:CheckBox ID="chkSelect" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
            <asp:TemplateField HeaderText="Program No">
            <ItemStyle Width="120px" />
                <ItemTemplate>
                <a href="idp_template_detail.aspx?mode=edit&id=<%# Eval("template_id")%>&flag=<% response.write(flag) %>">
                <asp:Label runat="server" Text='<%# Eval("template_yearno")%>'></asp:Label>-
                <asp:Label ID="Label2" runat="server" Text='<%# padNo(Eval("template_runno"))%>'></asp:Label>
                 </a>
                    <asp:Label ID="lblPk" runat="server" Text='<%#Bind ("template_id")%>' Visible="false"></asp:Label> 
                </ItemTemplate>
                <HeaderStyle ForeColor="White" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Program Name">
                <ItemTemplate>
                    <asp:Label ID="lblManager2" runat="server" Text='<%# Bind("template_title") %>'></asp:Label>
                   
                </ItemTemplate>
                <HeaderStyle ForeColor="White" />
            </asp:TemplateField>
         
            <asp:TemplateField HeaderText="Status" >
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# getActive(Eval("is_active"))%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle ForeColor="White" />
            </asp:TemplateField>
            <asp:BoundField DataField="create_by" HeaderText="Create By" />
            <asp:TemplateField HeaderText="Create date" >
                <ItemTemplate>
                <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("create_date") %>' />
                  
                </ItemTemplate>
                <HeaderStyle ForeColor="White" />
            </asp:TemplateField>
         
            <asp:BoundField DataField="last_update_by" HeaderText="Update By" />
            <asp:BoundField DataField="last_update_date" HeaderText="Update Date" />
         
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

