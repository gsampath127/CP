<%@ Page Title="" Language="C#" MasterPageFile="~/BCSDocUpdateApproval.Master" AutoEventWireup="true"
    CodeBehind="BCSReportDashBoard.aspx.cs" Inherits="BCSDocUpdateApproval.BCSReportDashBoard" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Src="~/UserControls/UIErrorDisplay.ascx" TagName="ExceptionLog"
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <script src="./Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <telerik:RadCodeBlock ID="ResendPopup" runat="server">
        <script type="text/javascript">

            $(document).ready(function () {
                $("#ContentBody_hdnDateOffSet").val(new Date().getTimezoneOffset());
            });

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("watchListDownload") >= 0 || args.get_eventTarget().indexOf("filteredIPDownload") >= 0 || args.get_eventTarget().indexOf("docUpdateDownStreamDownload") >= 0) {
                    args.set_enableAjax(false);
                }
            }

        </script>
        <style type="text/css">
        </style>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <telerik:RadAjaxManager ID="AjaxManagerBCSRemoveDuplicateCUSIP" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
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
    <asp:HiddenField ID="hdnDateOffSet" Value="" runat="server" />
    <asp:Panel ID="BCSRReportDashBoardPanel" runat="server" BorderColor="#333333" BorderStyle="None" Width="100%" Height="100%" CssClass="ClientContentDiv" HorizontalAlign="Left">
        <div id="divExceptionLog" runat="server" visible="false">
            <br />
        </div>
        <table style="width: 100%; height: 100%;">
            <tr>
                <td>
                    <div class="RightPanelBand">
                        <b>&nbsp;
                           
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
                                            <td style="margin-right:5em;width:50%;" colspan="3">
                                                <asp:Label ID="lblInlineFD" runat="server" Text="InLine FD Process" CssClass="InformationLabelHeading"></asp:Label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; padding-top: 2px">
                                                <asp:Label ID="lblClientName" runat="server" Text="Select Customer: " CssClass="InformationLabel"></asp:Label>
                                                <b style="color: red">*</b>
                                            </td>
                                            <td style="width: 70%; padding-top: 2px">
                                                <telerik:RadComboBox ID="comboClient" runat="server" EmptyMessage="Select Customer" AllowCustomText="true" Width="250px" EnableViewState="true">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td style="padding-left: 5px !important">
                                                <asp:RequiredFieldValidator ID="reqClient" ControlToValidate="comboClient" runat="server" ErrorMessage="Please select a Customer" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%; padding-left: 8px" colspan="3">
                                                <br />
                                                <asp:Button ID="btnGenerateReport" Text="Generate Report" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateReport_Click"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                </td>

                                <td width="50%">
                                    <br />
                                    <table width="100%">
                                        <tr>                                            
                                            <td style="margin-right:5em;width:50%;" colspan="3">
                                                <asp:Label ID="lblInternalOps" runat="server" Text="Internal Ops Process" CssClass="InformationLabelHeading"></asp:Label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; padding-top: 2px">
                                                <asp:Label ID="Label2" runat="server" Text="Select: " CssClass="InformationLabel"></asp:Label>
                                                <b style="color: red">*</b>
                                            </td>
                                            <td style="width: 70%; padding-top: 2px">
                                                <telerik:RadComboBox ID="comboReport" runat="server" EmptyMessage="Select Report" AllowCustomText="true" Width="250px" EnableViewState="true" ValidationGroup="group2" />
                                            </td>
                                            <td style="padding-left: 5px !important">
                                                <asp:RequiredFieldValidator ID="reqReport" ControlToValidate="comboReport" runat="server" ErrorMessage="Please select a Report" ForeColor="Red" ValidationGroup="group2"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%; padding-left: 8px" colspan="3">
                                                <br />
                                                <asp:Button ID="btnGenerateReportSelection" Text="Generate Report" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateReportSelection_Click" ValidationGroup="group2"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="dvReports" runat="server">
                        <table width="100%">
                            <tr id="trTabbedReports" runat="server" style="width: 100%;">
                                <td style="width: 100%;">

                                    <telerik:RadTabStrip ID="BCSReportDashboardTabStrip" runat="server" MultiPageID="RadMultiPageReports"
                                        SelectedIndex="0" Font-Bold="True" AutoPostBack="True" OnTabClick="BCSReportDashboardTabStrip_TabClick">
                                        <Tabs>
                                            <telerik:RadTab Text="CUSIP - Report" PageViewID="pvCusipReports" Value="CUSIPReport">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="WatchList" PageViewID="pvWatchList" Value="WatchList">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Customer  - Doc Update" PageViewID="pvfilteredIP" Value="CustomerDocUpdate">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Gateway - Doc Update" PageViewID="pvdocUpdateDownStream" Value="GatewayDocUpdate">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Customer - Doc Update (Details)" PageViewID="pvdailyUpdate" Value="CustomerDocUpdateDetails">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="SLINK - Report" PageViewID="pvSlinkReport" Value="SLINKReport">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Live Update" PageViewID="pvLiveUpdate" Value="LiveUpdate">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Fullfillment Info" PageViewID="pvFullfillment" Value="Fullfillment">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>

                                    <telerik:RadMultiPage ID="RadMultiPageReports" runat="server" Width="100%">

                                        <telerik:RadPageView ID="pvCusipReports" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">

                                                    <b>&nbsp;
                                                        <asp:Label runat="server" ID="lblCusipClientName"></asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page gives you information on CUSIP(Added, Removed and Difference) based on the watchlist</b>
                                                                <b>
                                                                    <label id="lblClint"></label>
                                                                </b>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr id="trDateRange" runat="server">
                                                                        <td colspan="1">
                                                                            <asp:Label ID="lblFromDate" runat="server" Text="Date Range : " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important">
                                                                            <telerik:RadDatePicker ID="startDate" runat="server">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="validator1" Display="None"
                                                                                ControlToValidate="startDate" ValidationGroup="dateValidationGrp"
                                                                                ErrorMessage="Enter Start Date!">
                                                                            </asp:RequiredFieldValidator>

                                                                        </td>
                                                                        <td style="padding-left: 1px !important">
                                                                            <asp:Label ID="lblToDate" runat="server" Text="to" CssClass="InformationLabel"></asp:Label>

                                                                        </td>
                                                                        <td style="padding-left: 10px !important">
                                                                            <telerik:RadDatePicker ID="endDate" runat="server">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="validator2" Display="None"
                                                                                ControlToValidate="endDate" ValidationGroup="dateValidationGrp"
                                                                                ErrorMessage="Enter End Date!">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="valSummary" runat="server" ForeColor="Red" ValidationGroup="dateValidationGrp" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:CompareValidator ID="dateCompareValidator" runat="server" Display="None"
                                                                                ControlToValidate="endDate" ValidationGroup="dateValidationGrp"
                                                                                ControlToCompare="startDate"
                                                                                Operator="GreaterThan"
                                                                                Type="Date"
                                                                                ErrorMessage="The End Date cannot be before Start Date!">
                                                                            </asp:CompareValidator>

                                                                        </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblRepotyType" runat="server" Text="CUSIP Report : " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td colspan="5" style="padding-left: 30px !important">
                                                                            <telerik:RadComboBox ID="comboReportType" runat="server" EmptyMessage="Select Report Type" AllowCustomText="true" Width="350px" OnSelectedIndexChanged="comboReportType_SelectedIndexChanged" AutoPostBack="true">
                                                                            </telerik:RadComboBox>

                                                                            <asp:RequiredFieldValidator runat="server" ID="Validator3" Display="None"
                                                                                ControlToValidate="comboReportType" ValidationGroup="dateValidationGrp"
                                                                                ErrorMessage="Select Report Type!">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td colspan="4"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateCusipRpt" ValidationGroup="dateValidationGrp" Text="Generate Report" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateCusipRpt_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="5" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnClearCusipReport" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnClearCusipReport_Click"></asp:Button>
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridCUSIPReports" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSizes="10,20,50"
                                                                    OnNeedDataSource="GridCUSIPReports_NeedDataSource" AllowSorting="true"
                                                                    OnPageIndexChanged="GridCUSIPReports_PageIndexChanged" OnPageSizeChanged="GridCUSIPReports_PageSizeChanged"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" CellPadding="0"
                                                                    OnItemDataBound="GridCUSIPReports_ItemDataBound">
                                                                    <PagerStyle Mode="NumericPages" Position="Bottom" AlwaysVisible="true"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace" Name="TabViewCusip" AllowNaturalSort="false"
                                                                        AllowPaging="true" PageSize="10" EnableNoRecordsTemplate="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="CUSIP_WL" DataField="CUSIP_WL" HeaderText="CUSIP_WL" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="CUSIP_RP" DataField="CUSIP_RP" HeaderText="CUSIP_RP" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="FundName_WL" DataField="FundName_WL" HeaderText="FundName_WL" ItemStyle-Width="25%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="FundName_RP" DataField="FundName_RP" HeaderText="FundName_RP" ItemStyle-Width="25%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Class_WL" DataField="Class_WL" HeaderText="Class_WL" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Class_RP" DataField="Class_RP" HeaderText="Class_RP" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="DateModified" DataField="DateModified" HeaderText="DateModified" ItemStyle-Width="23%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="Status" ItemStyle-Width="5%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>

                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There is no Data for this CUSIP Report.
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
                                            </div>
                                        </telerik:RadPageView>

                                        <telerik:RadPageView ID="pvWatchList" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">
                                                    <b>&nbsp;
                                                 <asp:Label runat="server" ID="lblWatchListClientName"></asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page displays WatchLists.</b>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="WatchListHistory" runat="server" Text="Select Date: " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important">
                                                                            <telerik:RadDatePicker ID="watchListDate" runat="server">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvWatchListDate" Display="None" ValidationGroup="valWatchListReport"
                                                                                ControlToValidate="watchListDate"
                                                                                ErrorMessage="Select Date!">
                                                                            </asp:RequiredFieldValidator>

                                                                        </td>


                                                                        <td>
                                                                            <asp:ValidationSummary ID="valWatchListSummary" ValidationGroup="valWatchListReport" runat="server" ForeColor="Red" />
                                                                        </td>


                                                                    </tr>

                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="genearateWatchList" Text="Generate Report" ValidationGroup="valWatchListReport" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="genearateWatchList_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="clearWatchList" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="clearWatchList_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridWatchListEntries" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                                                    OnItemCommand="GridWatchListEntries_ItemCommand"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                        AllowPaging="true" PageSize="25" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderText="WatchList File Name">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="watchListDownload" runat="server" ForeColor="Blue" CommandName="download_file" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("DirectoryName") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn UniqueName="ReceivedTime" DataField="ReceivedTime" HeaderText="WatchList Received Date/Time" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no WatchLists files for selected date.
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
                                            </div>
                                        </telerik:RadPageView>

                                        <telerik:RadPageView ID="pvfilteredIP" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%" id="divCustomerDocUPDT_FilterIP" runat="server">
                                                <div class="RightPanelBand">
                                                    <b>&nbsp;
                                                 <asp:Label runat="server" ID="lblFilteredIPClientName"></asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page displays Customer - Doc Update files.</b>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="filteredIPHistory" runat="server" Text="Select Date: " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important">
                                                                            <telerik:RadDatePicker ID="filteredIPDate" runat="server">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvfilteredIPDate" Display="None" ValidationGroup="valfilteredIPReport"
                                                                                ControlToValidate="filteredIPDate"
                                                                                ErrorMessage="Select Date!">
                                                                            </asp:RequiredFieldValidator>

                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="valfilteredIPSummary" ValidationGroup="valfilteredIPReport" runat="server" ForeColor="Red" />
                                                                        </td>
                                                                    </tr>

                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="genearatefilteredIP" Text="Generate Report" ValidationGroup="valfilteredIPReport" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="genearatefilteredIP_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="clearfilteredIP" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="clearfilteredIP_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridfilteredIPEntries" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                                                    OnItemCommand="GridfilteredIPEntries_ItemCommand"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                        AllowPaging="true" PageSize="25" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderText="Customer - Doc Update File Name">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="filteredIPDownload" runat="server" ForeColor="Blue" CommandName="download_file" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("DirectoryName") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn UniqueName="ReceivedTime" DataField="ReceivedTime" HeaderText="File Sent To TA_ Time" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Customer - Doc Update Files for selected date.
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
                                            </div>



                                            <div class="RadGrid RadGrid_Default" style="width: 100%" id="divCustomerDocUPDT_IP_NU" runat="server">
                                                <div class="RightPanelBand">
                                                    <b>&nbsp;
                                                 <asp:Label runat="server" ID="lblCustomerDocUPDT_IPNUClientName"> </asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page displays Customer - Doc Update(NU/IP) files.</b>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>

                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label3" runat="server" Text="Customer - Doc Update(NU/IP): " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>

                                                                        </td>
                                                                        <td style="padding-left: 30px !important">
                                                                            <telerik:RadComboBox ID="comboCustomerDocUpdateFiles" runat="server" EmptyMessage="Select Customer - Doc Update(NU/IP)" AllowCustomText="true" Width="220px">
                                                                            </telerik:RadComboBox>

                                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="None" ValidationGroup="valCustDocUpdateDownStreamReport"
                                                                                ControlToValidate="comboCustomerDocUpdateFiles"
                                                                                ErrorMessage="Select Customer - Doc Update(NU/IP)!">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="ValidationSummary3" ValidationGroup="valCustDocUpdateDownStreamReport" runat="server" ForeColor="Red" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="Label5" runat="server" Text="Select Date: " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important" colspan="2">
                                                                            <telerik:RadDatePicker ID="CustomerDocUpdateDownStreamDate" runat="server" Width="220px">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Display="None" ValidationGroup="valCustDocUpdateDownStreamReport"
                                                                                ControlToValidate="CustomerDocUpdateDownStreamDate"
                                                                                ErrorMessage="Select Date!">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" style="padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateCustIPNU" Text="Generate Report" ValidationGroup="valCustDocUpdateDownStreamReport" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="genearatefilteredIP_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnClearCustIPNU" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="clearfilteredIP_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridCustomerDocUpdateHeader" Width="100%" Height="100%" runat="server"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                        AllowPaging="false" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="RecordType" DataField="HeaderRecordType" HeaderText="Record Type" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="DataType" DataField="HeaderDataType" HeaderText="Data Type" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="System" DataField="HeaderSystem" HeaderText="System" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="FileName" DataField="HeaderFileName" HeaderText="File Name" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="DateTime" DataField="HeaderDateTime" HeaderText="Date/Time" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="TotalRecordCount" DataField="HeaderTotalRecordCount" HeaderText="Total Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="FLRecordCount" DataField="HeaderFLRecordCount" HeaderText="FL Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="EXRecordCount" DataField="HeaderEXRecordCount" HeaderText="EX Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="ApRecordCount" DataField="HeaderAPRecordCount" HeaderText="AP RecordCount" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="OPRecordCount" DataField="HeaderOPRecordCount" HeaderText="OP Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="NURecordCount" DataField="HeaderAPCRecordCount" HeaderText="NU Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <%--<telerik:GridBoundColumn UniqueName="OPCRecordCount" DataField="HeaderOPCRecordCount" HeaderText="OPC Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="Reserved" DataField="HeaderField13Reserved" HeaderText="Reserved" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>--%>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Customer - Doc Update(NU/IP) files for selected date.
                                                                            </div>
                                                                        </NoRecordsTemplate>
                                                                    </MasterTableView>
                                                                    <FilterMenu EnableImageSprites="False">
                                                                    </FilterMenu>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridCustomerDocUpdateEntries" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                                                    OnItemCommand="GriddocUpdateDownStreamEntries_ItemCommand"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                        AllowPaging="true" PageSize="25" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderText="Customer - Doc Update(NU/IP) File">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="docUpdateDownStreamDownload" runat="server" ForeColor="Blue" CommandName="download_file" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("DirectoryName") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn UniqueName="ReceivedTime" DataField="ReceivedTime" HeaderText="File Sent Downstream" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Customer - Doc Update(NU/IP) files for selected date.
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
                                            </div>


                                        </telerik:RadPageView>

                                        <telerik:RadPageView ID="pvdocUpdateDownStream" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">
                                                    <b>&nbsp;
                                                 <asp:Label runat="server" ID="lblDocUpdateClientName"> </asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page displays Gateway - Doc Update(NU/IP) files.</b>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>

                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Text="Gateway - Doc Update(NU/IP): " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>

                                                                        </td>
                                                                        <td style="padding-left: 30px !important">
                                                                            <telerik:RadComboBox ID="comboDocUpdateFiles" runat="server" EmptyMessage="Select Gateway - Doc Update(NU/IP)" AllowCustomText="true" Width="220px">
                                                                            </telerik:RadComboBox>

                                                                            <asp:RequiredFieldValidator runat="server" ID="rvComboDocUpdatestream" Display="None" ValidationGroup="valdocUpdateDownStreamReport"
                                                                                ControlToValidate="comboDocUpdateFiles"
                                                                                ErrorMessage="Select Gateway - Doc Update(NU/IP)!">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="valdocUpdateDownStreamSummary" ValidationGroup="valdocUpdateDownStreamReport" runat="server" ForeColor="Red" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="docUpdateDownStreamHistory" runat="server" Text="Select Date: " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important" colspan="2">
                                                                            <telerik:RadDatePicker ID="docUpdateDownStreamDate" runat="server" Width="220px">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvdocUpdateDownStreamDate" Display="None" ValidationGroup="valdocUpdateDownStreamReport"
                                                                                ControlToValidate="docUpdateDownStreamDate"
                                                                                ErrorMessage="Select Date!">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" style="padding-left: 8px">
                                                                            <asp:Button ID="genearatedocUpdateDownStream" Text="Generate Report" ValidationGroup="valdocUpdateDownStreamReport" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="genearatedocUpdateDownStream_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="cleardocUpdateDownStream" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="cleardocUpdateDownStream_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GriddocUpdateDownStreamEntriesHeader" Width="100%" Height="100%" runat="server"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                        AllowPaging="false" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="RecordType" DataField="HeaderRecordType" HeaderText="Record Type" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="DataType" DataField="HeaderDataType" HeaderText="Data Type" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="System" DataField="HeaderSystem" HeaderText="System" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="FileName" DataField="HeaderFileName" HeaderText="File Name" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="DateTime" DataField="HeaderDateTime" HeaderText="Date/Time" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="TotalRecordCount" DataField="HeaderTotalRecordCount" HeaderText="Total Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="FLRecordCount" DataField="HeaderFLRecordCount" HeaderText="FL Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="EXRecordCount" DataField="HeaderEXRecordCount" HeaderText="EX Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="ApRecordCount" DataField="HeaderAPRecordCount" HeaderText="AP RecordCount" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="OPRecordCount" DataField="HeaderOPRecordCount" HeaderText="OP Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="NURecordCount" DataField="HeaderAPCRecordCount" HeaderText="NU Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <%--<telerik:GridBoundColumn UniqueName="OPCRecordCount" DataField="HeaderOPCRecordCount" HeaderText="OPC Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="Reserved" DataField="HeaderField13Reserved" HeaderText="Reserved" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>--%>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Gateway - Doc Update(NU/IP) files for selected date.
                                                                            </div>
                                                                        </NoRecordsTemplate>
                                                                    </MasterTableView>
                                                                    <FilterMenu EnableImageSprites="False">
                                                                    </FilterMenu>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GriddocUpdateDownStreamEntries" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                                                    OnItemCommand="GriddocUpdateDownStreamEntries_ItemCommand"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                        AllowPaging="true" PageSize="25" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderText="Gateway - Doc Update(NU/IP) File">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="docUpdateDownStreamDownload" runat="server" ForeColor="Blue" CommandName="download_file" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("DirectoryName") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>

                                                                            <telerik:GridBoundColumn UniqueName="ReceivedTime" DataField="ReceivedTime" HeaderText="File Sent Downstream" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Gateway - Doc Update(NU/IP) files for selected date.
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
                                            </div>
                                        </telerik:RadPageView>

                                        <telerik:RadPageView ID="pvdailyUpdate" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">
                                                    <b>&nbsp;<asp:Label runat="server" ID="lblDailyUpdateClientName"></asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page gives you details or parsed out information from Customer - Doc Update File</b>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblDailyUpdateHistory" runat="server" Text="Select Date: " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important">
                                                                            <telerik:RadDatePicker ID="datPickDailyUpdateHistory" runat="server">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="reqdatPickDailyUpdateHistory" Display="None" ValidationGroup="valdailyUpdateReport"
                                                                                ControlToValidate="datPickDailyUpdateHistory"
                                                                                ErrorMessage="Select Date!">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="valSumDailyUpdate" ValidationGroup="valdailyUpdateReport" runat="server" ForeColor="Red" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateDailyUpdate" Text="Generate Report" ValidationGroup="valdailyUpdateReport" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateDailyUpdate_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnClearDailyUpdate" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnClearDailyUpdate_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <div runat="server" id="dvHeaderInfo" class="dataCount" style="display: none">
                                                                    <br />
                                                                    Header Record                                                               
                                                                </div>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridDailyUpdateHeader" Width="1000px" Height="100%" runat="server"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                        AllowPaging="false" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="RecordType" DataField="HeaderRecordType" HeaderText="Record Type" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="DataType" DataField="HeaderDataType" HeaderText="Data Type" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="System" DataField="HeaderSystem" HeaderText="System" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="FileName" DataField="HeaderFileName" HeaderText="File Name" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="DateTime" DataField="HeaderDateTime" HeaderText="Date/Time" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="TotalRecordCount" DataField="HeaderTotalRecordCount" HeaderText="Total Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="FLRecordCount" DataField="HeaderFLRecordCount" HeaderText="FL Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="EXRecordCount" DataField="HeaderEXRecordCount" HeaderText="EX Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="ApRecordCount" DataField="HeaderAPRecordCount" HeaderText="AP RecordCount" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="OPRecordCount" DataField="HeaderOPRecordCount" HeaderText="OP Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="APCRecordCount" DataField="HeaderAPCRecordCount" HeaderText="APC Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="OPCRecordCount" DataField="HeaderOPCRecordCount" HeaderText="OPC Record Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="Reserved" DataField="HeaderField13Reserved" HeaderText="Reserved" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Daily Update entries.
                                                                            </div>
                                                                        </NoRecordsTemplate>
                                                                    </MasterTableView>
                                                                    <FilterMenu EnableImageSprites="False">
                                                                    </FilterMenu>
                                                                </telerik:RadGrid>
                                                                <br />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div runat="server" id="dvDetailInfo" class="dataCount" style="display: none">
                                                                    Detail Record
                                                                <br />
                                                                    <asp:Label runat="server" ID="lblDetailCount"></asp:Label>
                                                                    Items Found
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridDailyUpdateDetail" Width="1000px" runat="server" AllowSorting="true"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0"
                                                                    OnNeedDataSource="GridDailyUpdateDetail_NeedDataSource">
                                                                    <ClientSettings>
                                                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                                    </ClientSettings>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace" AllowNaturalSort="false"
                                                                        AllowPaging="false" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">

                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="RecordType" HeaderStyle-Width="100px" DataField="DetailRecordType" HeaderText="Record Type" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="System" HeaderStyle-Width="50px" DataField="DetailSystem" HeaderText="System" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="ClientId" HeaderStyle-Width="100px" DataField="DetailClientID" HeaderText="Client Id" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="RPProcessStep" HeaderStyle-Width="150px" DataField="DetailRPProcessStep" HeaderText="RP Process Step" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="CusipId" HeaderStyle-Width="100px" DataField="DetailCUSIPID" HeaderText="CUSIP Id" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="FundName" HeaderStyle-Width="300px" DataField="DetailFundName" HeaderText="Fund Name" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="PdfName" HeaderStyle-Width="300px" DataField="DetailPDFName" HeaderText="PDF Name" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="RrdExternalDocId" HeaderStyle-Width="300px" DataField="DetailRRDExternalDocID" HeaderText="RRD External DocId" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="DocumentType" HeaderStyle-Width="150px" DataField="DetailDocumentType" HeaderText="Document Type" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="EffectiveDate" HeaderStyle-Width="100px" DataField="DetailEffectiveDate" HeaderText="Effective Date" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="DocumentDate" HeaderStyle-Width="150px" DataField="DetailDocumentDate" HeaderText="Document Date" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="PageCount" HeaderStyle-Width="100px" DataField="DetailPageCount" HeaderText="Page Count" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="PageSizeHeight" HeaderStyle-Width="150px" DataField="DetailPageSizeheight" HeaderText="Page Size-Height" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="PageSizeWidth" HeaderStyle-Width="150px" DataField="DetailPageSizeWidth" HeaderText="Page Size-Width" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="Reserved" HeaderStyle-Width="100px" DataField="DetailField15Reserved" HeaderText="Reserved" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="RRDInternalDocID" HeaderStyle-Width="150px" DataField="DetailRRDInternalDocID" HeaderText="RRD Internal DocID" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="Reserved" HeaderStyle-Width="100px" DataField="DetailField17Reserved" HeaderText="Reserved" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="Accession" HeaderStyle-Width="250px" DataField="DetailAccessionNum" HeaderText="Accession #" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="EffectiveDate" HeaderStyle-Width="250px" DataField="DetailEffectiveDate" HeaderText="Effective Date" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="FilingDate" HeaderStyle-Width="250px" DataField="DetailFilingDate" HeaderText="Filing Date" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="SECFormType" HeaderStyle-Width="250px" DataField="DetailSECFormType" HeaderText="SEC Form Type" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Daily detail Update entries.
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
                                            </div>
                                        </telerik:RadPageView>

                                        <telerik:RadPageView ID="pvSlinkReport" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">

                                                    <b>&nbsp;
                                                        <asp:Label runat="server" ID="lblSlinkClentName"></asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page displays Slinks Reports.</b>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>

                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="lblSlinkReport" runat="server" Text="Select Date: " CssClass="InformationLabel"></asp:Label>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important; width: 250px">
                                                                            <telerik:RadDatePicker ID="slinkReportDate" runat="server">
                                                                            </telerik:RadDatePicker>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 15%; padding-top: 2px">
                                                                            <asp:Label ID="lblStatus" runat="server" Text="Status: " CssClass="InformationLabel"></asp:Label>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadComboBox ID="comboStatus" runat="server" EmptyMessage="Select Status" AllowCustomText="true" Width="250px" EnableViewState="true">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbltxtDocId" runat="server" Text="Doc Id: " CssClass="InformationLabel"></asp:Label>
                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadTextBox ID="txtDocId" runat="server" Width="350px" TextMode="MultiLine" Height="50px" Resize="Both">
                                                                            </telerik:RadTextBox>
                                                                        </td>

                                                                    </tr>

                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateSlinkReport" Text="Generate Report" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateSlinkReport_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnClearSlinkReport" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnClearSlinkReport_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 30px !important">
                                                                <telerik:RadGrid ID="GridSlinkReportsCount" Width="100%" Height="100%" runat="server"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" AllowNaturalSort="false">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="TotalCount" DataField="TotalCount" HeaderText="Total Count" ItemStyle-Width="20%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="EXCount" DataField="EXCount" HeaderText="EX Count" ItemStyle-Width="16%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="APCount" DataField="APCount" HeaderText="AP Count" ItemStyle-Width="16%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="OPCount" DataField="OPCount" HeaderText="OP Count" ItemStyle-Width="16%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="APCCount" DataField="APCCount" HeaderText="APC Count" ItemStyle-Width="16%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="OPCCount" DataField="OPCCount" HeaderText="OPC Count" ItemStyle-Width="16%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                    </MasterTableView>
                                                                    <FilterMenu EnableImageSprites="False">
                                                                    </FilterMenu>
                                                                </telerik:RadGrid>


                                                                <telerik:RadGrid ID="GridSlinkReports" Width="100%" Height="100%" runat="server"
                                                                    OnItemCommand="GridSlinkReports_ItemCommand" OnNeedDataSource="GridSlinkReports_NeedDataSource" AllowSorting="true"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" CssClass="GridSlinkReportsCssClass" AllowCustomPaging="true" AllowPaging="true" PageSize="25">
                                                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace" AllowNaturalSort="false"
                                                                        HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                                        <Columns>

                                                                            <telerik:GridBoundColumn UniqueName="SLINKFileName" DataField="SLINKFileName" HeaderText="SLINK File Name" ItemStyle-Width="45%" AllowSorting="true" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="ZipFileName" InitializeTemplatesFirst="false" UniqueName="ZipFileName" ItemStyle-Width="30%" ReadOnly="true">
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Eval("ZipFilePath") %>" style="color: blue;"><%# Eval("ZipFileName") %></a>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="Status" AllowSorting="true" ItemStyle-Width="10%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="ReceivedDate" DataField="ReceivedDate" HeaderText="Status Date" AllowSorting="true" ItemStyle-Width="15%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />

                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Slink Report for selected date.
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
                                            </div>
                                        </telerik:RadPageView>

                                        <telerik:RadPageView ID="pvLiveUpdate" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">

                                                    <b>&nbsp;
                                                        <asp:Label runat="server" ID="lblLiveUpdateClientName"></asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">

                                                    <table style="width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page displays Live Update Reports.</b>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 15%; padding-top: 2px">
                                                                            <asp:Label ID="lblCusip" runat="server" Text="CUSIP: " CssClass="InformationLabel"></asp:Label>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadTextBox ID="txtCusip" runat="server" Width="250px" TextMode="MultiLine" Height="50px" Resize="Both">
                                                                            </telerik:RadTextBox>

                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="valSumLiveUpdate" ValidationGroup="valLiveUpdate" runat="server" ForeColor="Red" DisplayMode="SingleParagraph" Width="200px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 1px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblAccount" runat="server" Text="Acc#: " CssClass="InformationLabel"></asp:Label>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important" colspan="2">
                                                                            <telerik:RadTextBox ID="txtAcc" runat="server" Width="250px">
                                                                            </telerik:RadTextBox>
                                                                        </td>

                                                                    </tr>
                                                                    <tr style="height: 1px">
                                                                        <td colspan="3"></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="width: 15%; padding-top: 2px">
                                                                            <asp:Label ID="lblLiveStatus" runat="server" Text="Status: " CssClass="InformationLabel"></asp:Label>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadComboBox ID="cmbLiveStatus" runat="server" EmptyMessage="Select Status" AllowCustomText="true" Width="250px" EnableViewState="true">
                                                                            </telerik:RadComboBox>

                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 1px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 15%; padding-top: 2px">
                                                                            <asp:Label ID="Label1" runat="server" Text="Document ID: " CssClass="InformationLabel"></asp:Label>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadTextBox ID="txtDocumentID" runat="server" Width="350px" TextMode="MultiLine" Height="50px" Resize="Both">
                                                                            </telerik:RadTextBox>

                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="valLiveUpdate" runat="server" ForeColor="Red" DisplayMode="SingleParagraph" Width="200px" />
                                                                        </td>
                                                                    </tr>

                                                                    <tr style="height: 1px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateLiveUpdate" Text="Generate Report" ValidationGroup="valLiveUpdate" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateLiveUpdate_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnLiveUpdateClear" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnLiveUpdateClear_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">

                                                                <div id="divCUSIP" style="margin-bottom: 30px; display: table; width: 100%">
                                                                    <div runat="server" id="divCUSIPCount" style="width: 40%; display: inline-block">
                                                                        <b>CUSIP Details</b>
                                                                        <br />
                                                                        <br />

                                                                        <telerik:RadGrid ID="GridLiveUpdateCUSIPDetails" Width="100%" Height="100%" runat="server"
                                                                            EnableViewState="false" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                            <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                                DataKeyNames="TotalCUSIPs" EnableNoRecordsTemplate="true">
                                                                                <Columns>
                                                                                    <telerik:GridBoundColumn UniqueName="TotalCUSIPs" DataField="TotalCUSIPs" HeaderText="Total CUSIPs" ItemStyle-Width="30%" ReadOnly="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn UniqueName="CUSIPsFound" DataField="CUSIPsFound" HeaderText="CUSIPs Found" ItemStyle-Width="30%" ReadOnly="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn UniqueName="MissingCUSIPs" DataField="MissingCUSIPs" HeaderText="Missing CUSIPs" ItemStyle-Width="30%" ReadOnly="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                </Columns>
                                                                                <HeaderStyle Font-Bold="true" />
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </div>
                                                                    <div runat="server" id="divGridLiveUpdateInvalidCUSIPs" visible="false" style="width: 40%; display: inline-block; float: right">
                                                                        <b>
                                                                            <asp:Label ID="lblGridLiveUpdateInvalidCUSIPs" runat="server"></asp:Label></b>
                                                                        <br />
                                                                        <br />

                                                                        <telerik:RadGrid ID="GridLiveUpdateInvalidCUSIPs" Width="100%" Height="100%" runat="server"
                                                                            EnableViewState="false" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                            <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                                DataKeyNames="CUSIP" EnableNoRecordsTemplate="true">
                                                                                <Columns>
                                                                                    <telerik:GridBoundColumn UniqueName="CUSIP" DataField="CUSIP" HeaderText="CUSIP" ItemStyle-Width="10%" ReadOnly="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                </Columns>
                                                                                <HeaderStyle Font-Bold="true" />
                                                                                <NoRecordsTemplate>
                                                                                    <div>
                                                                                        There are no data to display.
                                                                                    </div>
                                                                                </NoRecordsTemplate>
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </div>
                                                                </div>
                                                                <div id="divDocId" style="margin-bottom: 30px; display: table; width: 100%">
                                                                    <div runat="server" id="divDocIdsCount" style="width: 40%; display: inline-block">
                                                                        <b>Doc Id Details</b>
                                                                        <br />
                                                                        <br />

                                                                        <telerik:RadGrid ID="GridLiveUpdateDocIdDetails" Width="100%" Height="100%" runat="server"
                                                                            EnableViewState="false" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                            <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                                DataKeyNames="TotalDocIds" EnableNoRecordsTemplate="true">
                                                                                <Columns>
                                                                                    <telerik:GridBoundColumn UniqueName="TotalDocIds" DataField="TotalDocIds" HeaderText="Total DocIds" ItemStyle-Width="30%" ReadOnly="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn UniqueName="DocIdsFound" DataField="DocIdsFound" HeaderText="DocIds Found" ItemStyle-Width="30%" ReadOnly="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn UniqueName="MissingDocIds" DataField="MissingDocIds" HeaderText="Missing DocIds" ItemStyle-Width="30%" ReadOnly="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                </Columns>
                                                                                <HeaderStyle Font-Bold="true" />
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </div>
                                                                    <div runat="server" id="divGridLiveUpdateInvalidDocIds" visible="false" style="width: 40%; display: inline-block; float: right">
                                                                        <b>
                                                                            <asp:Label ID="lblGridLiveUpdateInvalidDocIds" runat="server"></asp:Label></b>
                                                                        <br />
                                                                        <br />

                                                                        <telerik:RadGrid ID="GridLiveUpdateInvalidDocIds" Width="100%" Height="100%" runat="server"
                                                                            EnableViewState="false" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                            <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                                DataKeyNames="DocId" EnableNoRecordsTemplate="true">
                                                                                <Columns>
                                                                                    <telerik:GridBoundColumn UniqueName="DocId" DataField="DocId" HeaderText="DocId" ItemStyle-Width="10%" ReadOnly="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                </Columns>
                                                                                <HeaderStyle Font-Bold="true" />
                                                                                <NoRecordsTemplate>
                                                                                    <div>
                                                                                        There are no data to display.
                                                                                    </div>
                                                                                </NoRecordsTemplate>
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </div>
                                                                </div>
                                                                <telerik:RadGrid ID="GridLiveUpdate" Width="100%" Height="100%" runat="server"
                                                                    EnableViewState="false" AllowAutomaticUpdates="true" CellSpacing="0" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                                                    OnNeedDataSource="GridLiveUpdate_NeedDataSource" OnItemDataBound="GridLiveUpdate_ItemDataBound">
                                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                        DataKeyNames="CUSIP" AllowPaging="true" PageSize="25" HierarchyLoadMode="Client" EnableNoRecordsTemplate="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="BCSDocUpdateId" DataField="BCSDocUpdateId" HeaderText="BCSDocUpdateId" ReadOnly="true" Display="false">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="CUSIP" DataField="CUSIP" HeaderText="CUSIP" ItemStyle-Width="7%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="EdgarID" DataField="EdgarID" HeaderText="Edgar ID" ItemStyle-Width="6%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Accnumber" DataField="Accnumber" HeaderText="Acc#" ItemStyle-Width="13%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="Fund Name" ItemStyle-Width="19%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Document Type" InitializeTemplatesFirst="false" UniqueName="DocumentType" ItemStyle-Width="13%">
                                                                                <ItemTemplate>
                                                                                    <a target="_blank" href="<%# Eval("Status").ToString() == "Filed" ? "javascript:void(0)" : Eval("RRDPDFURL") %>" class="<%# Eval("Status").ToString() == "Filed" ? "cssLiveUpdateRemoveDocumentTypeLink" : "cssLiveUpdateDocumentTypeLink" %>"><%# Eval("DocumentType") %></a>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn UniqueName="DocumentDate" DataField="DocumentDate" HeaderText="Document Date" ItemStyle-Width="13%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="DocumentID" DataField="DocumentID" HeaderText="Document ID" ItemStyle-Width="13%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="Status" ItemStyle-Width="3%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="StatusDate" DataField="StatusDate" HeaderText="Status Date" ItemStyle-Width="12%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no data to display.
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
                                            </div>
                                        </telerik:RadPageView>


                                        <telerik:RadPageView ID="pvFullfillment" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">
                                                    <b>&nbsp;
                                                 <asp:Label runat="server" ID="lblFullfillmentClientName"> </asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page displays Fullfillment Info.</b>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="lblFullfillDate" runat="server" Text="Select Date: " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important" colspan="2">
                                                                            <telerik:RadDatePicker ID="dateFullfillment" runat="server" Width="220px">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="reqDateFullfillment" Display="None" ValidationGroup="valGrpFullFillment"
                                                                                ControlToValidate="dateFullfillment" ErrorMessage="Select Date!">
                                                                            </asp:RequiredFieldValidator>

                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="valGrpFullFillment" runat="server" ForeColor="Red" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" style="padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateFullfillmentInfo" Text="Generate Report" ValidationGroup="valGrpFullFillment" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateFullfillmentInfo_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnClearFullfillment" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnClearFullfillment_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 30px !important">

                                                                <telerik:RadGrid ID="GridFullfillmentInfoCount" Width="100%" Height="100%" runat="server"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" AllowNaturalSort="false">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="TotalCount" DataField="TotalCount" HeaderText="Total Requests" ItemStyle-Width="30%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Completed" DataField="Completed" HeaderText="Completed" ItemStyle-Width="30%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="NotAvailable" DataField="NotAvailable" HeaderText="Not Available" ItemStyle-Width="40%" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                    </MasterTableView>
                                                                    <FilterMenu EnableImageSprites="False">
                                                                    </FilterMenu>
                                                                </telerik:RadGrid>

                                                                <telerik:RadGrid ID="GridFullfillmentInfo" Width="100%" Height="100%" runat="server" AllowPaging="false" AllowCustomPaging="false"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" CssClass="GridFullfillmentInfoCssClass">
                                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                                        AllowPaging="false" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="TransId" DataField="TransId" HeaderText="Trans Id" ReadOnly="true" ItemStyle-Width="30%">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Cusip" DataField="Cusip" HeaderText="CUSIP" ReadOnly="true" ItemStyle-Width="30%">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Message" DataField="Message" HeaderText="Status" ReadOnly="true" ItemStyle-Width="40%">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Fullfillment info for selected date.
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
                                            </div>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </td>
                            </tr>
                        </table>

                    </div>
                </td>
            </tr>
        </table>


    </asp:Panel>

</asp:Content>
