<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="admin_review_user_list.aspx.vb" Inherits="jci_admin_review_user_list" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
    <table width="100%" class="box-header">
  <tr>
    <td><img src="../images/report.png" width="24" height="24"  />&nbsp;&nbsp
     <a href="admin_test.aspx">Main</a> 
                <asp:Label ID="lblPathWay" runat="server" Text="Label"></asp:Label>
    </td>
    <td align="right" style="padding-right: 15px;" >&nbsp;</td>
  </tr>
</table>
<fieldset style="border: solid 1px #CCC; margin: 5px 10px 8px 10px;"><legend style="background: url(images/boxheader.gif); border: solid 1px #CCC; padding: 3px 10px; margin-left: 10px; margin-bottom: 5px;">Search</legend>
<table width="100%" style="margin: 0px 30px 10px 30px;">
  <tr>
    <td width="150"><strong>Employee name</strong></td>
    <td>
          <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
               </td>
  </tr>
    
  <tr>
    <td width="100"><strong>Department :</strong></td>
    <td>
          <asp:DropDownList ID="txtdept" runat="server" DataTextField="dept_name_en" 
              DataValueField="dept_id" Width="200px" AutoPostBack="True">
            </asp:DropDownList>     
               </td>
  </tr>
    
  <tr>
    <td width="100"><strong>Date :</strong></td>
    <td>
                              <asp:TextBox ID="txtdate_report" runat="server" 
              Width="100px"></asp:TextBox>
                              
                              <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate_report" 
                                  PopupButtonID="Image1">
                              </asp:CalendarExtender>
                              <asp:CalendarExtender ID="txtdate_report_CalendarExtender0" 
              runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image1" 
              TargetControlID="txtdate_report">
          </asp:CalendarExtender>
          <asp:Image ID="Image1" runat="server" CssClass="mycursor" 
              ImageUrl="~/images/calendar.gif" />
         
               </td>
  </tr>
    
  <tr>
    <td width="100">&nbsp;</td>
    <td>
                              <asp:Button ID="cmdSearch" runat="server" Text="Search" />
         
            <asp:Button ID="cmdExport" runat="server" Text="Export to excel" />
         
               </td>
  </tr>
    
</table>

  </fieldset>
  <div>
      <asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label> Records found.
  
  </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True" AllowPaging="True" 
        PageSize="2000">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="30px" />
                <ItemTemplate>
                    <asp:Label ID="Labels" runat="server" >

                 <%# Container.DataItemIndex + 1 %>.

                </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Name">
               
                <ItemTemplate>
                    <a href='admin_review_question.aspx?tid=<%# tid %>&empcode=<%# Eval("trans_create_by_emp_code") %>&pageID=<%=gridview1.PageIndex %>'><asp:Label ID="Label1" runat="server" 
                        Text='<%# Bind("trans_create_by_name") %>'></asp:Label></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee ID">
              
                <ItemTemplate>
                    <asp:Label ID="lblEmpCode" runat="server" 
                        Text='<%# Bind("trans_create_by_emp_code") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Department" DataField="trans_dept_name" />
            <asp:BoundField HeaderText="Job Title" DataField="trans_job_title" />
            <asp:TemplateField HeaderText="Answer Reviewed">
             
                <ItemTemplate>
                   <asp:Label ID="lblScore" runat="server" Visible="false" ></asp:Label>
                  <asp:Label ID="lblReview" runat="server"></asp:Label>/
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("num") %>'></asp:Label>
                 <asp:Label ID="lblResult" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderImageUrl="~/images/151.png">
            <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lbllow" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderImageUrl="~/images/154.png">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblmid" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderImageUrl="~/images/152.png">
              <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblHigh" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Options">
                <ItemStyle HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                  
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

