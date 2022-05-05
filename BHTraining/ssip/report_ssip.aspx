<%@ Page Title="" Language="VB" MasterPageFile="~/ssip/SSIP_MasterPage.master" AutoEventWireup="false" CodeFile="report_ssip.aspx.vb" Inherits="ssip_report_ssip" %>
<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="data">
        <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
          <tr>
            <td width="550" colspan="4" class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
          </tr>
          <tr>
            <td colspan="4"><table width="100%" cellpadding="2">
              <tr>
                <td width="120">Select Report Type</td>
                <td width="385">
                    <asp:DropDownList ID="txtselect_report" runat="server" AutoPostBack="true">
                    <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                    <asp:ListItem Value="1">SSIP Report Summary by Submitted Department</asp:ListItem>
                     <asp:ListItem Value="2">SSIP Report Summary by Awarded Department</asp:ListItem>
                     <asp:ListItem Value="3">SSIP Report Summary by Implemented Department</asp:ListItem>
                      <asp:ListItem Value="4">SSIP Report Summary (All)</asp:ListItem>
                    </asp:DropDownList>
             </td>
                <td width="80">Date Range</td>
                <td><label for="select"></label>
                <asp:DropDownList ID="txtdate1" runat="server">
                    </asp:DropDownList>
                  &nbsp;to&nbsp;
                <asp:DropDownList ID="txtdate2" runat="server"> </asp:DropDownList>
                 </td>
              </tr>
              <tr>
                <td width="80">&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              </table></td>
          </tr>
          <tr>
            <td colspan="4" align="right">
                <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="button-green2" />
              <asp:Button ID="Button1" runat="server" Text="Clear" CssClass="button-green2" /></td>
          </tr>
        </table>
        <br />
        <div class="small" style="margin-bottom: 3px; width: 98%">
          <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td><div align="right">
                <label>
         
            <asp:Button ID="cmdExport" runat="server" Text="Export to excel" />
         
                  &nbsp;</label></div></td>
              </tr>
          </table>
        </div>
        <asp:panel ID="panel_submit" runat="server" Visible="false">
		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">SSIP Summary by Submitted Department </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">SSIP&nbsp; report information from&nbsp;
                      <asp:Label ID="lblDate1" runat="server" Text="Label"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblDate2" runat="server" Text="Label"></asp:Label>
                    </td>
                  <td width="300" valign="top">Total no. of Submitted Department 
                      <asp:Label ID="lblNum" runat="server" Text="0"></asp:Label></td>
                </tr>
              </table>
            </div>
            <br />
                     <asp:GridView ID="Gridview1" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50">
           <Columns>
               <asp:TemplateField HeaderText="Date">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                     <asp:Label ID="lblPK" runat="server" Text='<%# Bind("ssip_id") %>' Visible="false" ></asp:Label>
                       <asp:Label ID="lblHTML0" runat="server" Text=""></asp:Label>
                       <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("submit_date_ts")) %>' Font-Bold="true"></asp:Label><br />
                      <asp:Label ID="lblDept" runat="server" Text='<%# bind("report_dept_name") %>' Visible="false"></asp:Label>
                       <asp:Label ID="lblDeptId" runat="server" Text='<%# bind("report_dept_id") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
              
             
               <asp:TemplateField HeaderText="SSIP No.">
               
                   <ItemTemplate>
                      <asp:Label ID="lblHTML2" runat="server" Text=""></asp:Label>
                   <a href="form_ssip.aspx?mode=edit&id=<%# Eval("ssip_id") %>&viewtype=<%response.write(viewtype) %>">
                     <asp:Label ID="lblNo" runat="server" Text='<%# bind("ssip_no") %>'></asp:Label></a>
                   </ItemTemplate>
               </asp:TemplateField>
             
                 <asp:TemplateField HeaderText="Employee ID">
                      <ItemStyle VerticalAlign="Top" />
                   <ItemTemplate>
                      <asp:Label ID="lblHTML3_1" runat="server" Text=""></asp:Label>
                      <asp:Label ID="lblEmpID" runat="server" Text='<%# bind("report_emp_code") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Employee name">
                      <ItemStyle VerticalAlign="Top" />
                   <ItemTemplate>
                      <asp:Label ID="lblHTML3" runat="server" Text=""></asp:Label>
                      <asp:Label ID="lblEmpName" runat="server" Text='<%# bind("report_by") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Topic">
                 
                   <ItemTemplate>
                      <asp:Label ID="lblHTML4" runat="server" Text=""></asp:Label>
                      <asp:Label ID="lblTopic" runat="server" Text='<%# bind("topic") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Status">
                  
                   <ItemTemplate>
                      <asp:Label ID="lblHTML5" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# bind("status_name") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
          
          </asp:GridView>
          &nbsp;<br />
          
          
          </td>
		    </tr>
		  </table>
          </asp:panel>
    
           <asp:panel ID="panel_award" runat="server" Visible="false">
          	<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">SSIP Report Summary by Awarded Department </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">SSIP report information from&nbsp;
                      <asp:Label ID="lblDateAward1" runat="server" Text=""></asp:Label>
&nbsp;to
                      <asp:Label ID="lblDateAward2" runat="server" Text=""></asp:Label>
                    </td>
                  <td width="300" valign="top">Total no. of Awarded Department 
                      <asp:Label ID="lblNum4" runat="server" Text="0"></asp:Label></td>
                </tr>
              </table>
            </div>
            <br />

  <asp:GridView ID="Gridview2" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50">
           <Columns>
               <asp:TemplateField HeaderText="Date">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                     <asp:Label ID="lblPK" runat="server" Text='<%# Bind("ssip_id") %>' Visible="false" ></asp:Label>
                       <asp:Label ID="lblHTML0" runat="server" Text=""></asp:Label>
                       <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("submit_date_ts")) %>' Font-Bold="true"></asp:Label><br />
                       <asp:Label ID="lblDept" runat="server" Text='<%# bind("report_dept_name") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblDeptId" runat="server" Text='<%# bind("report_dept_id") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
              
              
               <asp:TemplateField HeaderText="SSIP No.">
               
                   <ItemTemplate>
                    <asp:Label ID="lblHTML1" runat="server" Text=""></asp:Label>
                   <a href="form_ssip.aspx?mode=edit&id=<%# Eval("ssip_id") %>&viewtype=<%response.write(viewtype) %>">
                     <asp:Label ID="lblNo" runat="server" Text='<%# bind("ssip_no") %>'></asp:Label></a>
                   </ItemTemplate>
               </asp:TemplateField>
              
               <asp:TemplateField HeaderText="Employee ID">
                      <ItemStyle VerticalAlign="Top" />
                   <ItemTemplate>
                      <asp:Label ID="lblHTML3_1" runat="server" Text=""></asp:Label>
                      <asp:Label ID="lblEmpID" runat="server" Text='<%# bind("report_emp_code") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Employee name">
                      <ItemStyle VerticalAlign="Top" />
                   <ItemTemplate>
                    <asp:Label ID="lblHTML2" runat="server" Text=""></asp:Label>
                      <asp:Label ID="lblEmpName" runat="server" Text='<%# bind("report_by") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Topic">
                 
                   <ItemTemplate>
                    <asp:Label ID="lblHTML3" runat="server" Text=""></asp:Label>
                      <asp:Label ID="lblTopic" runat="server" Text='<%# bind("topic") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Cash">
                  
                   <ItemTemplate>
                    <asp:Label ID="lblHTML4" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# bind("cash_num") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Point">
                   <ItemTemplate>
                    <asp:Label ID="lblHTML5" runat="server" Text=""></asp:Label>
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("point_total") %>'></asp:Label>
                   </ItemTemplate>
                 
               </asp:TemplateField>
               <asp:TemplateField HeaderText="On the spot">
                   <ItemTemplate>
                    <asp:Label ID="lblHTML6" runat="server" Text=""></asp:Label>
                       <asp:Label ID="Label2" runat="server" Text='<%# Bind("coupon_num") %>'></asp:Label>
                   </ItemTemplate>
                  
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           
          </asp:GridView>
            
              
        
          </td>
		    </tr>
		  </table>
            </asp:panel>  

     <asp:panel ID="panel_implement" runat="server" Visible="false">
          	<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">SSIP Report Summary by Implemented Department </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">SSIP report information from&nbsp;
                      <asp:Label ID="lblDateImpl1" runat="server" Text=""></asp:Label>
&nbsp;to
                      <asp:Label ID="lblDateImpl2" runat="server" Text=""></asp:Label>
                    </td>
                  <td width="300" valign="top">Total no. of Implemented Department 
                      <asp:Label ID="lblNum3" runat="server" Text="0"></asp:Label></td>
                </tr>
              </table>
            </div>
            <br />

  <asp:GridView ID="Gridview3" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50">
           <Columns>
  
              
              
               <asp:TemplateField HeaderText="SSIP No.">
               <ItemStyle Width="200px" />
                   <ItemTemplate>
                   
                    <asp:Label ID="lblHTML0" runat="server" Text=""></asp:Label>
                   <a href="form_ssip.aspx?mode=edit&id=<%# Eval("ssip_id") %>&viewtype=<%response.write(viewtype) %>">
                     <asp:Label ID="lblNo" runat="server" Text='<%# bind("ssip_no") %>'></asp:Label></a>
                       <asp:Label ID="lblDept" runat="server" Text='<%# bind("costcenter_name") %>' Visible="false"></asp:Label>
                         <asp:Label ID="lblDeptId" runat="server" Text='<%# bind("costcenter_id") %>' Visible="false"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Topic">
                 
                   <ItemTemplate>
                    <asp:Label ID="lblHTML1" runat="server" Text=""></asp:Label>
                      <asp:Label ID="lblTopic" runat="server" Text='<%# bind("topic") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
          
          </asp:GridView>
            
              
        
          </td>
		    </tr>
		  </table>
            </asp:panel>  

              <asp:panel ID="panel_logbook" runat="server" Visible="false">
          	<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">SSIP Report Summary </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">SSIP report information from&nbsp;
                      <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
&nbsp;to
                      <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                    </td>
                  <td width="300" valign="top">Total no. of SSIP 
                      <asp:Label ID="lblNumLog" runat="server" Text="0"></asp:Label></td>
                </tr>
              </table>
            </div>
            <br />

  <asp:GridView ID="Gridview4" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50">
           <Columns>
  
              
              
                <asp:TemplateField HeaderText="Date">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                   
                       <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("submit_date_ts")) %>' Font-Bold="true"></asp:Label><br />
                    
                   </ItemTemplate>
               </asp:TemplateField>
  
  
              
              
               <asp:TemplateField HeaderText="SSIP No.">
               <ItemStyle Width="200px" />
                   <ItemTemplate>
              
                   <a href="form_ssip.aspx?mode=edit&id=<%# Eval("ssip_id") %>&viewtype=<%response.write(viewtype) %>">
                     <asp:Label ID="lblNo" runat="server" Text='<%# bind("ssip_no") %>'></asp:Label></a>
                      
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="report_emp_code" HeaderText="Emp ID." />
               <asp:BoundField DataField="report_by" HeaderText="Employee Name" />
               <asp:BoundField DataField="report_costcenter_id" HeaderText="Costcenter" />
               <asp:BoundField DataField="report_dept_name" HeaderText="Department" />
               <asp:TemplateField HeaderText="Topic">
                 
                   <ItemTemplate>
                  
                      <asp:Label ID="lblTopic" runat="server" Text='<%# bind("topic") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:BoundField DataField="cash_num" HeaderText="Cash" />
               <asp:BoundField DataField="point_total" HeaderText="Point" />
               <asp:BoundField DataField="coupon_num" HeaderText="On the spot" />
               <asp:BoundField DataField="result_title" HeaderText="HRD Reply" />
               <asp:BoundField DataField="status_name" HeaderText="Status" />
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
          
          </asp:GridView>
            
              
        
          </td>
		    </tr>
		  </table>
            </asp:panel>  
      </div>
</asp:Content>

