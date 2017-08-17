<%@ Page Title="" Language="C#" MasterPageFile="~/BCSDocUpdateApproval.Master" AutoEventWireup="true"
    CodeBehind="RPCUSIPMissingReport.aspx.cs" Inherits="BCSDocUpdateApproval.RPCUSIPMissingReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <script src="./Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <telerik:RadAjaxManager ID="AjaxManagerBCSRemoveDuplicateCUSIP" runat="server">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="BCSRReportDashBoardPanel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="BCSRReportDashBoardPanel" LoadingPanelID="BCSReportDashBoardAjaxPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="BCSReportDashBoardAjaxPanel" runat="server" Skin="Sunset">
    </telerik:RadAjaxLoadingPanel>




    <asp:Panel ID="BCSRReportDashBoardPanel" runat="server" BorderColor="#333333" BorderStyle="None" Width="100%" Height="100%" CssClass="ClientContentDiv" HorizontalAlign="Left">
        <div id="divExceptionLog" runat="server" visible="false">
            <br />
        </div>
        <table style="width: 100%; height: 100%;">
            <tr>
                <td>
                    <div class="RightPanelBand">
                        <b>&nbsp;
                           <asp:Label runat="server" ID="lblMissingCUSIPHeader">Missing CUSIP(s) in RP Reports</asp:Label>
                        </b>
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lnkHome" runat="server" Style="float: right;" ForeColor="Blue" OnClick="lnkHome_Click">Go back to customer selection</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="border: solid; border-width: 1px">
                    <div id="dvClientSelection" runat="server">
                        <table style="width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                            <tr>
                                <td width="50%">
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td style="margin-right: 5em; width: 50%;" colspan="3">
                                                <b>NOTE: This page gives you information on CUSIP(s) that are not available in RP system</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%; padding-top: 5px">
                                                <asp:Label ID="lblClientName" runat="server" Text="Select Customer: " CssClass="InformationLabel"></asp:Label>

                                            </td>
                                            <td style="width: 70%; padding-top: 2px">
                                                <telerik:RadComboBox ID="comboClient" runat="server" EmptyMessage="Select Customer" AllowCustomText="true" Width="250px" EnableViewState="true" >
                                                </telerik:RadComboBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="padding-left: 8px" colspan="1">
                                                <br />
                                                <asp:Button ID="btnGenerateReport" Text="Generate Report" runat="server" CssClass="Button" OnClick="btnGenerateReport_Click" ToolTip="Generate Report"></asp:Button>
                                            </td>
                                            <td style="padding-left: 8px" colspan="1">
                                                <asp:Button ID="btnClearCUSIPReport" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnClearCUSIPReport_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                 <td style="padding-bottom: 10px; padding-top: 10px !important">
                                   <telerik:RadGrid ID="GridMissingCUSIP" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="10"
                                    OnNeedDataSource="GridMissingCUSIP_NeedDataSource"   OnPageIndexChanged="GridMissingCUSIP_PageIndexChanged"  OnPageSizeChanged="GridMissingCUSIP_PageSizeChanged"    
                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" AllowSorting="true" PageSizes="10,20,50" >
                                    <PagerStyle Mode="NumericPages" Position="Bottom" AlwaysVisible="true"></PagerStyle>
                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                        DataKeyNames="CompanyName" AllowPaging="true" PageSize="10" HierarchyLoadMode="Client" Name="RPMissingCUSIPDataEntries" EnableNoRecordsTemplate="true">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="CompanyName" DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width="15%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="Fund Name" ItemStyle-Width="25%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="FLTCUSIP" DataField="FLTCUSIP" HeaderText="FLT CUSIP" ItemStyle-Width="15%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="LIPPERCUSIP" HeaderText="LIPPER CUSIP" InitializeTemplatesFirst="false" ItemStyle-Width="15%" ReadOnly="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFLTCUSIP" Text='<%# Eval("LIPPERCUSIP").ToString() != "" ? Eval("LIPPERCUSIP").ToString() : "Not Found" %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="EOnlineCUSIP" HeaderText="EOnline CUSIP" InitializeTemplatesFirst="false" ItemStyle-Width="15%" ReadOnly="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEOnlineCUSIP" Text='<%# Eval("EOnlineCUSIP").ToString() != "" ? Eval("EOnlineCUSIP").ToString() : "Not Found" %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>                                             
                                            <telerik:GridBoundColumn UniqueName="CalculatedCUSIP" DataField="CalculatedCUSIP" HeaderText="Calculated CUSIP" ItemStyle-Width="15%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="true" />
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <NoRecordsTemplate>
                                            <div>
                                                There are no RP Missing CUSIP entries.
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
                                </telerik:RadGrid>
                            </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>

    </asp:Panel>
</asp:Content>
