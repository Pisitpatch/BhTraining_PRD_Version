<%@ Page Title="" Language="VB" MasterPageFile="~/incident/Incident_MasterPage.master" AutoEventWireup="false" CodeFile="incident_physician_defendant.aspx.vb" Inherits="incident_incident_physician_defendant" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
              </asp:ScriptManager>
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
                    <asp:ListItem Value="1">Accident & Incident Reports Summary by Involving Physician</asp:ListItem>
                     <asp:ListItem Value="2">Accident & Incident Reports Summary by Unit Defendant</asp:ListItem>
                     <asp:ListItem Value="4">Accident & Incident Reports Summary by Relevant Unit</asp:ListItem>
                      <asp:ListItem Value="3">Accident & Incident Reports Summary by Involving Physicain Specialty</asp:ListItem>
                    </asp:DropDownList>
             </td>
                <td width="80">Date Range</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                            </ContentTemplate>
                    </asp:UpdatePanel>       
                <asp:DropDownList ID="txtdate1" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                  &nbsp;to&nbsp;
                <asp:DropDownList ID="txtdate2" runat="server" AutoPostBack="True"> </asp:DropDownList>
                &nbsp;
                   <asp:DropDownList ID="txtweek" runat="server" Width="60px" AutoPostBack="True">
                    </asp:DropDownList>
                       <asp:DropDownList ID="txtday" runat="server" Width="100px">
                    </asp:DropDownList>
                      
              
                 </td>
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
        <asp:panel ID="Panel_Doctor_Defendant" runat="server">
		<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Accident & Incident Reports Summary by Involving Physician</td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Accident & Incident Reports information from&nbsp;
                      <asp:Label ID="lblDate1" runat="server" Text="Label"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblDate2" runat="server" Text="Label"></asp:Label>
                    </td>
                  <td width="300" valign="top">Total no. of Involving Physician  
                      <asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label></td>
                </tr>
              </table>
            </div>
            <br />
                     <asp:GridView ID="Gridview1" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50">
           <Columns>
               <asp:TemplateField HeaderText="Physician Name">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                     <asp:Label ID="lblMDCode" runat="server" Text='<%# Bind("emp_no") %>' Visible="false" ></asp:Label>
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("doctor_name_en") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="Specialty">
                  <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="Label2" runat="server" Text='<%# Bind("specialty") %>'></asp:Label>
                   </ItemTemplate>
                   <ItemStyle Width="350px" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="No. of IR">
                 <ItemStyle VerticalAlign="Top" />
                   <ItemTemplate>
                       <asp:Label ID="lblTotal" runat="server" Text='<%# bind("num") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Incident No.">
               
                   <ItemTemplate>
                   
                      <asp:Label ID="lblIRNo" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Topic">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblTopic" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Severity Level">
                  
                   <ItemTemplate>
                       <asp:Label ID="lblLevel" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
          &nbsp;<br />
          
          
          </td>
		    </tr>
		  </table>
          </asp:panel>


          <asp:panel ID="Panel_Unit" runat="server">
          	<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Accident & Incident Reports Summary by Unit Defendant </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Accident & Incident Reports information from&nbsp;
                      <asp:Label ID="lblUnitDate1" runat="server" Text="Label"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblUnitDate2" runat="server" Text="Label"></asp:Label>
                    </td>
                  <td width="300" valign="top">Total no. of Unit defendent 
                      <asp:Label ID="lblNUm2" runat="server" Text="0"></asp:Label></td>
                </tr>
              </table>
            </div>
            <br />

 
            
            <asp:GridView ID="Gridview2" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50">
           <Columns>
               <asp:TemplateField HeaderText="Unit Defendant">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                     <asp:Label ID="lblDeptUnitID" runat="server" Text='<%# Bind("dept_unit_id") %>' Visible="false" ></asp:Label>
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("dept_unit_name") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="No. of IR">
                 <ItemStyle VerticalAlign="Top" />
                   <ItemTemplate>
                       <asp:Label ID="lblTotal" runat="server" Text='<%# bind("num") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Incident No.">
               
                   <ItemTemplate>
                   
                      <asp:Label ID="lblIRNo" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Topic">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblTopic" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Severity Level">
                  
                   <ItemTemplate>
                       <asp:Label ID="lblLevel" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Status">
                  
                   <ItemTemplate>
                       <asp:Label ID="lblStatus" runat="server" ></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
        
          </td>
		    </tr>
		  </table>
            </asp:panel>  
		

          <asp:panel ID="panel_specialty" runat="server">
          	<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Accident & Incident Reports Summary by Specialty </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Accident & Incident Reports information from&nbsp;
                      <asp:Label ID="lblSpecialDate1" runat="server" Text="Label"></asp:Label>
&nbsp;to
                      <asp:Label ID="lblSpecialDate2" runat="server" Text="Label"></asp:Label>
                    </td>
                  <td width="300" valign="top">Total no. of Specialty 
                      <asp:Label ID="lblNum3" runat="server" Text="0"></asp:Label></td>
                </tr>
              </table>
            </div>
            <br />

 
            
            <asp:GridView ID="Gridview3" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50">
           <Columns>
               <asp:TemplateField HeaderText="Specialty">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                    
                       <asp:Label ID="lblSpecialty" runat="server" Text='<%# Bind("specialty") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="No. of IR">
                 <ItemStyle VerticalAlign="Top" />
                   <ItemTemplate>
                       <asp:Label ID="lblTotal" runat="server" Text='<%# bind("num") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Incident No.">
               
                   <ItemTemplate>
                   
                      <asp:Label ID="lblIRNo" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Topic">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblTopic" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Severity Level">
                  
                   <ItemTemplate>
                       <asp:Label ID="lblLevel" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Status">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblStatus" runat="server" ></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
        
          </td>
		    </tr>
		  </table>
            </asp:panel>  


               <asp:panel ID="panel_relevant" runat="server">
          	<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
		  <tr>
		    <td valign="top" class="statheader">Accident & Incident Reports Summary by Relevant Unit </td>
		    </tr>
		  <tr>
		    <td valign="top" style="padding: 10px;">
            <div class="statdesc">
              <table width="100%" cellpadding="0">
                <tr>
                  <td valign="top">Accident & Incident Reports information from&nbsp;
                      <asp:Label ID="lblRelevantDate1" runat="server" Text=""></asp:Label>
&nbsp;to
                      <asp:Label ID="lblRelevantDate2" runat="server" Text=""></asp:Label>
                    </td>
                  <td width="300" valign="top">Total no. of Relevant 
                      <asp:Label ID="lblnum4" runat="server" Text="0"></asp:Label></td>
                </tr>
              </table>
            </div>
            <br />

 
            
            <asp:GridView ID="Gridview4" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" AllowPaging="True" PageSize="50">
           <Columns>
               <asp:TemplateField HeaderText="Relevant Unit">
                 <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                     <asp:Label ID="lblDeptUnitID" runat="server" Text='<%# Bind("dept_id") %>' Visible="false" ></asp:Label>
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("dept_name") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
  
               <asp:TemplateField HeaderText="No. of IR">
                 <ItemStyle VerticalAlign="Top" />
                   <ItemTemplate>
                       <asp:Label ID="lblTotal" runat="server" Text='<%# bind("num") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Incident No.">
               
                   <ItemTemplate>
                   
                      <asp:Label ID="lblIRNo" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Topic">
                 
                   <ItemTemplate>
                       <asp:Label ID="lblTopic" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Severity Level">
                  
                   <ItemTemplate>
                       <asp:Label ID="lblLevel" runat="server"></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
               <asp:TemplateField HeaderText="Status">
                  
                   <ItemTemplate>
                       <asp:Label ID="lblStatus" runat="server" ></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
        
          </td>
		    </tr>
		  </table>
            </asp:panel>
        
      </div>
</asp:Content>

