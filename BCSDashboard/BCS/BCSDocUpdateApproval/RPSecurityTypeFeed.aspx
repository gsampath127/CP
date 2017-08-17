<%@ Page Title="" Language="C#" MasterPageFile="~/BCSDocUpdateApproval.Master" AutoEventWireup="true"
    CodeBehind="RPSecurityTypeFeed.aspx.cs" Inherits="BCSDocUpdateApproval.RPSecurityTypeFeed" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <script src="./Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function clientClose(sender, eventArgs) {
            __doPostBack('OnCloseWindow', '');
        }
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("edgarFileDownload") >= 0)
            {
                args.set_enableAjax(false);
            }
           
        }
    </script>
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

                    <div id="dvReports" runat="server">
                        <table width="100%" style="background-color: white">
                            <tr id="trTabbedReports" runat="server" style="width: 100%;">
                                <td style="width: 100%;">

                                    <telerik:RadTabStrip ID="RPSecurityTypeFeedTabStrip" runat="server" MultiPageID="RadMultiPageReports"
                                        Font-Bold="True" AutoPostBack="True" OnTabClick="RPSecurityTypeFeedTabStrip_TabClick">
                                        <Tabs>
                                            <telerik:RadTab Text="Missing Report" PageViewID="pvMissingReport" Value="MissingReport">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Daily Update Report" PageViewID="pvDailyUpdateReport" Value="DailyUpdateReport">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="EOnline Data" PageViewID="pvEdgarOnlineData" Value="EdgarOnlineData">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="EOnline Feed" PageViewID="pvEdgarOnline" Value="EdgarOnline">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Security Types - Analysis" PageViewID="pvSecurityTypes" Value="SecurityTypes">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>

                                    <telerik:RadMultiPage ID="RadMultiPageReports" runat="server" Width="100%">

                                        <telerik:RadPageView ID="pvDailyUpdateReport" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">

                                                    <b>&nbsp;
                                                        <asp:Label runat="server" ID="lblDailyUpdateReport">Daily Update Report</asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">

                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>NOTE: This page displays Security Types updated in RightProspectus after comparing data with Edgar Online.</b>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="lblDailyUpdate" runat="server" Text="Select Date: " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadDatePicker ID="dailyUpdateDate" runat="server">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvDailyUpdateDate" Display="None" ValidationGroup="valDailyUpdateDate"
                                                                                ControlToValidate="dailyUpdateDate"
                                                                                ErrorMessage="Select Date!">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="valDailyUpdateDateSummary" ValidationGroup="valDailyUpdateDate" runat="server" ForeColor="Red" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateDailyUpdate" Text="Generate Report" ValidationGroup="valDailyUpdateDate" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateDailyUpdate_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnDailyUpdateClear" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnDailyUpdateClear_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridDailyUpdate" OnNeedDataSource="GridDailyUpdate_NeedDataSource" Width="100%" Height="100%" runat="server"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" AllowPaging="true"
                                                                    AllowCustomPaging="true" PageSizes="10,20,50" AllowSorting="true" OnPageIndexChanged="GridDailyUpdate_PageIndexChanged" OnPageSizeChanged="GridDailyUpdate_PageSizeChanged" PageSize="20">
                                                                    <PagerStyle Mode="NumericPages" Position="Bottom" AlwaysVisible="true"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" AllowNaturalSort="false"
                                                                        AllowPaging="true" PageSize="20" HierarchyLoadMode="Client" EnableNoRecordsTemplate="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="CUSIP" DataField="CUSIP" HeaderText="rpCUSIP" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="CompanyName" DataField="CompanyName" HeaderText="rpCompanyName" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="rpFundName" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="CompanyCIK" DataField="CompanyCIK" HeaderText="rpCIK" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="SeriesId" DataField="SeriesID" HeaderText="rpSeriesId" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="Class" DataField="Class" HeaderText="rpClassId" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="Ticker" DataField="Ticker" HeaderText="rpTicker" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="OldSecurityType" DataField="OldSecurityType" HeaderText="OldSecurityType" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>

                                                                            <telerik:GridBoundColumn UniqueName="SecurityType" DataField="SecurityType" HeaderText="SecurityType" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>


                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Records for selected date.
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

                                        <telerik:RadPageView ID="pvMissingReport" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">

                                                    <b>&nbsp;
                                                        <asp:Label runat="server" ID="lblMissingReports">Missing CUSIP/Security Type Report</asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b runat="server" id="MissingCUSIPNote">Note: This page displays "Missing CUSIP(s) Report" and "Missing Security Type(s) Report"</b>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="lblReportType" Text="Selection : " CssClass="InformationLabel" runat="server"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadComboBox ID="cmbReportType" runat="server" AllowCustomText="true" EmptyMessage="----Missing----" Width="250px">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Value="CUSIP" Text="CUSIP" />
                                                                                    <telerik:RadComboBoxItem Value="Security Types" Text="Security Types" />
                                                                                </Items>
                                                                            </telerik:RadComboBox>
                                                                            <span style="margin-left: 20px;">
                                                                                <asp:RequiredFieldValidator ID="reqReportType" ControlToValidate="cmbReportType" runat="server" ErrorMessage="Please select a Report Type" ForeColor="Red" ValidationGroup="cmbReportTypeValidationGroup"></asp:RequiredFieldValidator>
                                                                            </span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateReport" Text="Generate Report" runat="server" CssClass="Button" ValidationGroup="cmbReportTypeValidationGroup" ToolTip="Generate Report" OnClick="btnGenerateReport_Click"></asp:Button>
                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="Button" Width="100px" ToolTip="Clear" OnClick="btnClear_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridMissingReports" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSizes="10,20,50"
                                                                    OnNeedDataSource="GridMissingReports_NeedDataSource" AllowSorting="true"
                                                                    OnPageIndexChanged="GridMissingReports_PageIndexChanged" OnPageSizeChanged="GridMissingReports_PageSizeChanged"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" CellPadding="0"
                                                                    OnItemDataBound="GridMissingReports_ItemDataBound" PageSize="20">
                                                                    <PagerStyle Mode="NumericPages" Position="Bottom" AlwaysVisible="true"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace" Name="TabViewCusip" AllowNaturalSort="false"
                                                                        AllowPaging="true" PageSize="20" EnableNoRecordsTemplate="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn UniqueName="CUSIP" DataField="CUSIP" HeaderText="eoCUSIP" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="CompanyName" DataField="CompanyName" HeaderText="eoCompanyName" ItemStyle-Width="20%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="eoFundName" ItemStyle-Width="35%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="CIK" DataField="CIK" HeaderText="eoCIK" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="SeriesID" DataField="SeriesID" HeaderText="eoSeries#" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="ClassContractID" DataField="ClassContractID" HeaderText="eoClass#" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Ticker" DataField="Ticker" HeaderText="eoTicker" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="Action" ItemStyle-Width="5%" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <a href="javascript:void(0);" style="color: blue;" onclick="<%# (cmbReportType.SelectedValue == "CUSIP") ? "window.open('" + BCS.ObjectModel.Factories.ConfigValues.RightprospectusURL + "','_blank')" : "radopen('EditSecurityTypesInProsTicker.aspx?CUSIP=" + Eval("CUSIP") + "', 'SecurityTypesWindow')" %>">Add</a>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There is no Data for Missing CUSIP Report.
                                                                            </div>
                                                                        </NoRecordsTemplate>
                                                                    </MasterTableView>
                                                                    <FilterMenu EnableImageSprites="False">
                                                                    </FilterMenu>
                                                                </telerik:RadGrid>
                                                                <telerik:RadWindowManager ID="SecurityTypesWindowManager" runat="server" Skin="Windows7" VisibleStatusbar="false">
                                                                    <Windows>
                                                                        <telerik:RadWindow Behaviors="Close" ID="SecurityTypesWindow" runat="server" Height="215px"
                                                                            OnClientClose="clientClose" AutoSize="false" Modal="true" Width="610px" Left="355px" Top="210px"
                                                                            ShowContentDuringLoad="false" VisibleOnPageLoad="false">
                                                                        </telerik:RadWindow>
                                                                    </Windows>
                                                                </telerik:RadWindowManager>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </telerik:RadPageView>

                                        <telerik:RadPageView ID="pvSecurityTypes" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">

                                                    <b>&nbsp;
                                                        <asp:Label runat="server" ID="lblSecurityTypes">Security Type Report</asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">

                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td colspan="2">
                                                                <b>Note: This page displays Security Type(s).</b><span style="float: right">*NA - Show the number of NA(ST) from the system</span>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%; vertical-align: top; padding-top: 26px">
                                                                <table style="display: inline-block">

                                                                    <tr>
                                                                        <td style="width: 15%; padding-top: 2px">
                                                                            <asp:Label ID="lblCusip" runat="server" Text="CUSIP: " CssClass="InformationLabel"></asp:Label>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadTextBox ID="txtCusip" runat="server" Width="250px" TextMode="MultiLine" Height="50px" Resize="Both">
                                                                            </telerik:RadTextBox>

                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="valSumSecurityType" ValidationGroup="valSecurityType" runat="server" ForeColor="Red" DisplayMode="SingleParagraph" Width="200px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 1px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblCompany" runat="server" Text="Comapny : " CssClass="InformationLabel"></asp:Label>
                                                                        </td>
                                                                        <td style="padding-left: 30px !important" colspan="2">

                                                                            <telerik:RadAjaxPanel ID="RadAjaxPanelCompany" runat="server">
                                                                                <telerik:RadComboBox ID="cmbCompany" runat="server" EmptyMessage="Select Company"
                                                                                    AllowCustomText="true" Width="250px" EnableViewState="true"
                                                                                    MarkFirstMatch="false" Filter="StartsWith"
                                                                                    ChangeTextOnKeyBoardNavigation="true">
                                                                                </telerik:RadComboBox>
                                                                            </telerik:RadAjaxPanel>
                                                                        </td>

                                                                    </tr>
                                                                    <tr style="height: 1px">
                                                                        <td colspan="3"></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="width: 15%; padding-top: 2px">
                                                                            <asp:Label ID="lblSecurityType" runat="server" Text="Security Type: " CssClass="InformationLabel"></asp:Label>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">


                                                                            <telerik:RadAjaxPanel ID="RadAjaxPanelSecurityType" runat="server">
                                                                                <telerik:RadComboBox RenderMode="Lightweight" ID="cmbSecurityType" runat="server" EmptyMessage="Select Security Type" AllowCustomText="true" Width="250px"
                                                                                    EnableViewState="true" MarkFirstMatch="false" Filter="StartsWith" ChangeTextOnKeyBoardNavigation="true">
                                                                                </telerik:RadComboBox>

                                                                            </telerik:RadAjaxPanel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 1px">
                                                                        <td colspan="3"></td>
                                                                    </tr>

                                                                    <tr style="height: 1px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateSecurityType" Text="Generate Report" ValidationGroup="valSecurityType" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateSecurityType_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnSecurityTypeClear" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnSecurityTypeClear_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%; vertical-align: top">

                                                                <div id="divCUSIP" style="margin-bottom: 30px; display: table; width: 100%;">
                                                                    <div runat="server" id="divCUSIPCount">
                                                                        <div style="margin-bottom: 10px; margin-top: 20px">
                                                                            <b>Summary: </b>
                                                                        </div>

                                                                        <telerik:RadGrid ID="GridSummarizedSecurityType" Width="50%" Height="100%" runat="server"
                                                                            EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" CssClass="summary_table">

                                                                            <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace" Name="TabViewCusip" AllowNaturalSort="false"
                                                                                EnableNoRecordsTemplate="true">
                                                                                <Columns>

                                                                                    <telerik:GridBoundColumn UniqueName="LoadType" DataField="LoadType" HeaderText="LoadType" ItemStyle-Width="20%" ReadOnly="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn UniqueName="ETF" DataField="ETF" HeaderText="ETF" ItemStyle-Width="10%" ReadOnly="true" AllowSorting="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn UniqueName="ETN" DataField="ETN" HeaderText="ETN" ItemStyle-Width="10%" ReadOnly="true" AllowSorting="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn UniqueName="MF" DataField="MF" HeaderText="MF" ItemStyle-Width="10%" ReadOnly="true" AllowSorting="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn UniqueName="NA" DataField="NA" HeaderText="NA" ItemStyle-Width="10%" ReadOnly="true" AllowSorting="true">
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn UniqueName="UIT" DataField="UIT" HeaderText="UIT" ItemStyle-Width="10%" ReadOnly="true" AllowSorting="true">
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

                                                                    </div>

                                                                </div>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important;" colspan="2">



                                                                <telerik:RadGrid ID="GridSecurityType" Width="100%" Height="100%" runat="server"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" AllowPaging="true" AllowCustomPaging="true" PageSizes="10,20,50" AllowSorting="true"
                                                                    OnNeedDataSource="GridSecurityType_NeedDataSource" OnPageIndexChanged="GridSecurityType_PageIndexChanged" OnPageSizeChanged="GridSecurityType_PageSizeChanged" PageSize="20">
                                                                    <PagerStyle Mode="NumericPages" Position="Bottom" AlwaysVisible="true"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace" Name="TabViewCusip" AllowNaturalSort="false"
                                                                        AllowPaging="true" PageSize="20" EnableNoRecordsTemplate="true">
                                                                        <Columns>

                                                                            <telerik:GridBoundColumn UniqueName="CUSIP" DataField="CUSIP" HeaderText="CUSIP" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="CompanyName" DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width="20%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="Fund Name" ItemStyle-Width="30%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="CompanyCIK" DataField="CompanyCIK" HeaderText="Company CIK" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="ShareClass" DataField="ShareClass" HeaderText="Share Class" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Ticker" DataField="Ticker" HeaderText="Ticker" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="SecurityType" DataField="SecurityType" HeaderText="Security Type" ItemStyle-Width="8%" ReadOnly="true" AllowSorting="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="Loadtype" DataField="Loadtype" HeaderText="Load Type" ItemStyle-Width="10%" ReadOnly="true" AllowSorting="true">
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

                                        <telerik:RadPageView ID="pvEdgarOnline" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">

                                                    <b>&nbsp;
                                                        <asp:Label runat="server" ID="Label1">Edgar Online Feed</asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">

                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b>Note: This page gives you access to Edgar Online daily feed file.</b>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="Label2" runat="server" Text="Select Date: " CssClass="InformationLabel"></asp:Label>
                                                                            <b style="color: red">*</b>
                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadDatePicker ID="EdgarOnlineDatePicker" runat="server">
                                                                            </telerik:RadDatePicker>

                                                                            <asp:RequiredFieldValidator runat="server" ID="rfvEdgarOnlineDatePicker" Display="None" ValidationGroup="valEdgarOnlineDatePicker"
                                                                                ControlToValidate="EdgarOnlineDatePicker"
                                                                                ErrorMessage="Select Date!">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ValidationSummary ID="valEdgarOnlineDatePickerSummary" ValidationGroup="valEdgarOnlineDatePicker" runat="server" ForeColor="Red" />
                                                                        </td>
                                                                    </tr>

                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateEdgarOnline" Text="Generate Report" ValidationGroup="valEdgarOnlineDatePicker" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateEdgarOnline_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <asp:Button ID="btnEdgarOnline" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnEdgarOnline_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                                                <telerik:RadGrid ID="GridEdgarOnline" Width="100%" Height="100%" runat="server"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0"
                                                                    AllowCustomPaging="true" OnItemCommand="GridEdgarOnline_ItemCommand">

                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" AllowNaturalSort="false"
                                                                        HierarchyLoadMode="Client" EnableNoRecordsTemplate="true">
                                                                        <Columns>

                                                                            <telerik:GridTemplateColumn HeaderText="Edgar Online File Name">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="edgarFileDownload" runat="server" ForeColor="Blue" CommandName="download_file" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("FileName") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn UniqueName="DateReceived" DataField="DateReceived" HeaderText="Received Date/Time" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />

                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Records for selected date.
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



                                        <telerik:RadPageView ID="pvEdgarOnlineData" runat="server" Width="100%">
                                            <div class="RadGrid RadGrid_Default" style="width: 100%">
                                                <div class="RightPanelBand">

                                                    <b>&nbsp;
                                                        <asp:Label runat="server" ID="Label3">Edgar Online Data</asp:Label>
                                                    </b>
                                                </div>
                                                <div style="height: 20px"></div>
                                                <div style="width: 100%">
                                                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                                                        <tr>
                                                            <td>
                                                                <b runat="server" id="B1">Note: This page allows you to search data on Edgar Online Feed</b>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="lblEdgarDataCUSIP" runat="server" Text="eoCUSIP:" CssClass="InformationLabel"></asp:Label>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <telerik:RadTextBox ID="txtEdgarCUSIP" runat="server" Width="250px" TextMode="MultiLine" Height="50px" Resize="Both">
                                                                            </telerik:RadTextBox>
                                                                        </td>
                                                                        <td colspan="2"></td>
                                                                        <td colspan="1" style="padding-left: 30px !important">
                                                                            <asp:Label ID="lblEdgarCIK" runat="server" Text="eoCIK:" CssClass="InformationLabel"></asp:Label>
                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadTextBox ID="txtEdgarCIK" runat="server" Width="250px" TextMode="MultiLine" Height="50px" Resize="Both">
                                                                            </telerik:RadTextBox>
                                                                        </td>

                                                                    </tr>

                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="lblEdgarSeriesID" runat="server" Text="eoSeries#:" CssClass="InformationLabel"></asp:Label>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <telerik:RadTextBox ID="txtEdgarSeriesID" runat="server" Width="250px" TextMode="MultiLine" Height="50px" Resize="Both">
                                                                            </telerik:RadTextBox>
                                                                        </td>
                                                                        <td colspan="2"></td>
                                                                        <td colspan="1" style="padding-left: 30px !important">
                                                                            <asp:Label ID="lblEdgarClass" runat="server" Text="eoClass#:" CssClass="InformationLabel"></asp:Label>
                                                                        </td>
                                                                        <td colspan="2" style="padding-left: 30px !important">
                                                                            <telerik:RadTextBox ID="txtEdgarClass" runat="server" Width="250px" TextMode="MultiLine" Height="50px" Resize="Both">
                                                                            </telerik:RadTextBox>
                                                                        </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1">
                                                                            <asp:Label ID="lblEdgarTicker" runat="server" Text="eoTicker:" CssClass="InformationLabel"></asp:Label>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <telerik:RadTextBox ID="txtEdgarTicker" runat="server" Width="250px" TextMode="MultiLine" Height="50px" Resize="Both">
                                                                            </telerik:RadTextBox>
                                                                        </td>



                                                                    </tr>
                                                                    <tr style="height: 10px">
                                                                        <td colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="1" align="right" style="padding-right: 50px !important; padding-left: 8px">
                                                                            <asp:Button ID="btnGenerateEdgarOnlineData" Text="Generate Report" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateEdgarOnlineData_Click"></asp:Button>

                                                                        </td>
                                                                        <td colspan="2">
                                                                            <asp:Button ID="btnClearEdgarOnlineData" Text="Clear" runat="server" CssClass="Button" ToolTip="Clear" OnClick="btnClearEdgarOnlineData_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 10px; padding-top: 10px !important">



                                                                <telerik:RadGrid ID="GridEdgarData" Width="100%" Height="100%" runat="server"
                                                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" AllowPaging="true" AllowCustomPaging="true" PageSizes="10,20,50"
                                                                    OnPageIndexChanged="GridEdgarData_PageIndexChanged" OnPageSizeChanged="GridEdgarData_PageSizeChanged" OnNeedDataSource="GridEdgarData_NeedDataSource"
                                                                    PageSize="20">
                                                                    <PagerStyle Mode="NumericPages" Position="Bottom" AlwaysVisible="true"></PagerStyle>
                                                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true"
                                                                        EnableNoRecordsTemplate="true" AllowPaging="true" PageSize="20">
                                                                        <Columns>


                                                                            <telerik:GridBoundColumn UniqueName="ECUSIP" DataField="ECUSIP" HeaderText="eoCUSIP" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="ECompanyName" DataField="ECompanyName" HeaderText="eoCompanyName" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="EFundName" DataField="EFundName" HeaderText="eoFundName" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="ECIK" DataField="ECIK" HeaderText="eoCIK" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="ESeriesID" DataField="ESeriesID" HeaderText="eoSeries#" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="EClassContractID" DataField="EClassContractID" HeaderText="eoClass#" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn UniqueName="ETicker" DataField="ETicker" HeaderText="eoTicker" ReadOnly="true">
                                                                            </telerik:GridBoundColumn>

                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <HeaderStyle Font-Bold="true" />
                                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                                        <NoRecordsTemplate>
                                                                            <div>
                                                                                There are no Records for selected date.
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
