<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_ladder.aspx.vb" Inherits="idp_preview_ladder" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Import Namespace="ShareFunction" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title> Preview 
      
     
   </title>
<link href="../css/main.css" rel="stylesheet" type="text/css" />
<style type="text/css">
thead { display: table-header-group; }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="data">
    <table>
    <thead><tr><td>
     <h3 style="text-decoration:underline"><asp:Label ID="lblFormName" runat="server" Text=""></asp:Label>
    <br />Nursing Clinical Ladder</h3>
  <table width="100%" cellspacing="1" cellpadding="2">
  <tr>
    <td valign="top"><span class="theader"><strong>No.</strong></span></td>
    <td><strong>
      <asp:Label ID="lblrequest_NO" runat="server" Text=""></asp:Label>
      </strong>
      <asp:Label ID="lblViewtype" runat="server" Text="Label" Visible="False"></asp:Label></td>
  </tr>
  <tr>
    <td width="150" valign="top"><strong>Staff ID.</strong></td>
    <td><asp:Label ID="lblempcode" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
    <td valign="top"><strong>Staff Name</strong></td>
    <td><asp:Label ID="lblname" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
    <td valign="top"><strong>Department</strong></td>
    <td><asp:Label ID="lblDept" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
    <td valign="top"><strong>Staff Cost Center</strong></td>
    <td><asp:Label ID="lblCostcenter" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
    <td valign="top"><strong>Job Title</strong></td>
    <td><asp:Label ID="lbljobtitle" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
    <td valign="top"><strong>Hire Date</strong></td>
    <td><asp:Label ID="lblHireDate" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
    <td valign="top"><strong>Status</strong></td>
    <td><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></td>
  </tr>
 
</table>

              <hr />
    </td></tr></thead>
    <tbody>
    <tr>
    <td>
    
                  <asp:GridView ID="GridFunction" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data.">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField HeaderText="R/E">
                         <ItemStyle Width="30px" HorizontalAlign="Center" />
                          <ItemTemplate>
                              <asp:Label ID="lblRequire" runat="server" Text='<%# Bind("is_required") %>'></asp:Label>
                                <asp:Label ID="lblDelete" runat="server" Text='<%# Bind("is_delete") %>' 
                                  Visible="false"></asp:Label>
                            <asp:Label ID="lblNewIDP" runat="server" Text='<%# Bind("is_new_idp") %>' 
                                  Visible="false"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="category_name" HeaderText="Categories" >
                      <ItemStyle Width="120px" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="Topics">
                        
                          <ItemTemplate>
                              <asp:Label ID="txtcat_id" runat="server" Text='<%# Bind("category_id") %>' 
                                  Visible="false"></asp:Label>
                           
                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("topic_name") %>'></asp:Label>
                              <br />
                                <asp:Label ID="lblProgram" runat="server" 
                                  Text='<%# Eval("template_title") %>'></asp:Label>
                              
                             
                       
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Limit">
                          <ItemTemplate>
                             <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("nursing_limit") %>'></asp:Label>
                          </ItemTemplate>
                         
                       
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Time">
                          <ItemTemplate>
                              <asp:Label ID="txtTime" runat="server" Text='<%# Bind("nursing_time") %>' 
                                  Width="25px"></asp:Label>
                            
                             
                          </ItemTemplate>
                          <ItemStyle VerticalAlign="Top" Width="60px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Score">
                          <ItemTemplate>
                             
                        <asp:Label ID="lblScore" runat="server" Text='<%# Eval("nursing_score") %>'></asp:Label>
                          </ItemTemplate>
                      
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Achieve">
                          <ItemTemplate>
                              <asp:Label ID="lblArchieve" runat="server" 
                                  Text='<%# Eval("nursing_achieve") %>'></asp:Label>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                           
                            <ItemTemplate>
                                <asp:Label ID="txtremark1" runat="server" 
                                    Text='<%# Bind("function_remark") %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblComment_th" runat="server" Text='<%# Bind("comment_th") %>' 
                                    Visible="false"></asp:Label>
                                 <asp:Label ID="lblComment_en" runat="server" Text='<%# Bind("comment_en") %>' 
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                      
                  </Columns>
              </asp:GridView>
                <asp:Panel ID="panel_ladder_score" runat="server">
              <table width="100%" style="background-color:#CCCCCC">
              <tr>
              <td>
                  <strong>
                  Total score for <asp:Label ID="lblScoreName" runat="server" Text=""></asp:Label>
                  </strong>
                  </td>
              <td><strong>Employee Score</strong></td>
              </tr>
                  <tr>
                      <td>
                          Require :
                          <asp:Label ID="lblScoreRequire" runat="server" Text=""></asp:Label>
                      </td>
                      <td>
                          Require :
                          <asp:Label ID="lblEmpScoreRequire" runat="server" Text=""></asp:Label>
                      </td>
                  </tr>
                  <tr>
                      <td>
                          Elective :
                          <asp:Label ID="lblScoreElective" runat="server" Text=""></asp:Label>
                      </td>
                      <td>
                          Elective :
                          <asp:Label ID="lblEmpScoreElective" runat="server" Text=""></asp:Label>
                      </td>
                  </tr>
              </table>
              </asp:Panel>
              <br />
               <h3 style="text-decoration:underline">Educator Review</h3>
                <asp:GridView ID="GridEducator" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderStyle="None" 
            EmptyDataText="There is no data.">
                          <Columns>
                              <asp:TemplateField>
                             
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                                 
                                     <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td bgcolor="#DBE1E6">
                      <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">
                          <tr>
                            <td width="156" valign="top" bgcolor="#DBE1E6"><strong>
                                <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label></strong></td>
                            <td width="180" valign="top"><strong><asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label></strong></td>
                            <td width="159" valign="top"><strong><asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label></strong></td>
                            <td width="189" valign="top"><strong><asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label></strong></td>
                            <td width="120"><strong>
                                <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label></strong></td>
                          </tr>
                      </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                      <table width="100%" border="0">
                          <tr>
                            <td bgcolor="#FFFFFF" width="250"><strong><asp:Label ID="lblCommentStatusId" runat="server" Text='<%# Bind("comment_status_id") %>' Visible="false" />
                            <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("comment_status_name") %>'></asp:Label> </strong></td>
                            <td bgcolor="#FFFFFF">
                             <strong>Comment Subject :</strong>   <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label></td>
                            <td align="right"><div align="right"> 
                            <!--<img src="../images/comment_add.png" width="16" height="16" /> 
                            <img src="../images/comment_edit.png" width="16" height="16" /> 
                            <img src="../images/comment_delete.png" width="16" height="16" />-->
                               <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>   
                                  <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>            
                              
                            </div></td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><p><asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label><br />
                              <br />
                      </p></td>
                    </tr>
                  </table>
                    <table width="100%">
                      <tr>
                        <td height="1" valign="top" bgcolor="#336666"></td>
                      </tr>
                    </table>
                     <br />
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>
    <br />
    <h3 style="text-decoration:underline">Approve and Comment</h3>
       <asp:GridView ID="GridComment" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderStyle="None" 
            EmptyDataText="There is no data.">
                          <Columns>
                              <asp:TemplateField>
                             
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                                 
                                     <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td bgcolor="#DBE1E6">
                      <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">
                          <tr>
                            <td width="156" valign="top" bgcolor="#DBE1E6"><strong>
                                <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label></strong></td>
                            <td width="180" valign="top"><strong><asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label></strong></td>
                            <td width="159" valign="top"><strong><asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label></strong></td>
                            <td width="189" valign="top"><strong><asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label></strong></td>
                            <td width="120"><strong>
                                <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label></strong></td>
                          </tr>
                      </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                      <table width="100%" border="0">
                          <tr>
                            <td bgcolor="#FFFFFF" width="250"><strong><asp:Label ID="lblCommentStatusId" runat="server" Text='<%# Bind("comment_status_id") %>' Visible="false" />
                            <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("comment_status_name") %>'></asp:Label></strong></td>
                            <td bgcolor="#FFFFFF">
                               <strong>Comment Subject :</strong>  <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label></td>
                            <td align="right"><div align="right"> 
                            <!--<img src="../images/comment_add.png" width="16" height="16" /> 
                            <img src="../images/comment_edit.png" width="16" height="16" /> 
                            <img src="../images/comment_delete.png" width="16" height="16" />-->
                               <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>   
                                  <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>            
                             
                            </div></td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><p><asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label><br />
                              <br />
                      </p></td>
                    </tr>
                  </table>
                    <table width="100%">
                      <tr>
                        <td height="1" valign="top" bgcolor="#336666"></td>
                      </tr>
                    </table>
                     <br />
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>
                      <br />
                      <h3 style="text-decoration:underline">Competency Proficiency Scale</h3>
                         <table width="900" cellspacing="1" cellpadding="2">
              <tr>
                <td colspan="5"><table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                  <tr>
                    <td><strong>1</strong></td>
                    <td><table width="100%" border="0">
                        <tr>
                          <td width="246"><label for="radio5"><strong>Professinal Competency</strong></label></td>
                          <td width="214">
                              <asp:Label ID="txtscale1" runat="server" Width="120px"></asp:Label>
                            </td>
                          <td width="382">&nbsp;</td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td><strong>2. </strong></td>
                    <td><table width="100%" border="0">
                        <tr>
                          <td width="246"><label for="radio5"><strong>General Nursing Skil</strong></label></td>
                          <td width="214">
                              <asp:Label ID="txtscale2" runat="server" Width="120px"></asp:Label>
                            </td>
                          <td width="382">&nbsp;</td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td><strong>3. </strong></td>
                    <td><table width="100%" border="0">
                        <tr>
                          <td width="246"><label for="radio5"><strong>General OPD Nursing Skill</strong></label></td>
                          <td width="214">
                              <asp:Label ID="txtscale3" runat="server" Width="120px"></asp:Label>
                            </td>
                          <td width="382">&nbsp;</td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td><strong>4</strong></td>
                    <td>
                    <table width="100%" border="0">
                        <tr>
                          <td width="246"><strong>Functional Competency</strong></td>
                          <td width="214">
                              <asp:Label ID="txtscale4" runat="server" Width="120px"></asp:Label>
                            </td>
                          <td width="382">&nbsp;</td>
                        </tr>
                    </table>
                    </td>
                  </tr>
                  <tr>
                    <td><strong>Total = 1+2+3+4</strong></td>
                    <td><table width="100%" border="0">
                        <tr>
                          <td width="246">&nbsp;</td>
                          <td width="214">
                              <asp:Label ID="txttotal_scale" runat="server" Width="120px"></asp:Label>
                            </td>
                          <td ><strong>Full Score</strong>
                              <asp:Label ID="txtfullscore" runat="server" ></asp:Label>
                              &nbsp;=
                            <asp:Label ID="txtfullscore_percent" runat="server" ></asp:Label>
                            &nbsp;%</td>
                        </tr>
                    </table></td>
                  </tr>
                    <tr>
                      <td width="150"><strong>Acknowledge</strong></td>
                      <td><asp:Label ID="lblstatus_scale" runat="server" ></asp:Label>
                          <asp:DropDownList ID="txtstatus_scale" runat="server" Visible="false">
                                  <asp:ListItem Value="">- Please Select -</asp:ListItem>
                                   <asp:ListItem Value="1">Approve</asp:ListItem>
                                    <asp:ListItem Value="2">Not Approve</asp:ListItem>
                                     <asp:ListItem Value="3">N/A</asp:ListItem>
                                  </asp:DropDownList>
                     &nbsp;
                     </td>
                    </tr>
                    <tr>
                      <td><strong>Detail</strong></td>
                      <td>
                          <asp:Label ID="txtscale_detail" runat="server" Rows="3" TextMode="MultiLine" 
                              ></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>

                  </table>
                  <br /></td>
              </tr>
            </table>
    
    </td>
    </tr>
    </tbody>
    </table>
   
    
    <br />

    </div>
    </form>
</body>
</html>
