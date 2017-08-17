<%@ Page Title="" Language="C#" MasterPageFile="~/BCSDocUpdateApproval.Master" AutoEventWireup="true"
    CodeBehind="BCSRemoveDuplicateCUSIP.aspx.cs" Inherits="BCSDocUpdateApproval.BCSRemoveDuplicateCUSIP" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControls/UIErrorDisplay.ascx" TagName="ExceptionLog"
    TagPrefix="UCEx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <telerik:RadCodeBlock ID="ResendPopup" runat="server">
        <script type="text/javascript">            
        </script>
        <style type="text/css">
        </style>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <telerik:RadAjaxManager ID="AjaxManagerBCSRemoveDuplicateCUSIP" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="BCSRemoveDuplicateCUSIPPanel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="BCSRemoveDuplicateCUSIPPanel" LoadingPanelID="BCSRemoveDuplicateCUSIPAjaxPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="BCSRemoveDuplicateCUSIPAjaxPanel" runat="server" Skin="Sunset">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="BCSRemoveDuplicateCUSIPPanel" runat="server" BorderColor="#333333" BorderStyle="None" Width="100%" Height="100%" CssClass="ClientContentDiv" HorizontalAlign="Left">
        <div id="divExceptionLog" runat="server" visible="false">
            <UCEx:ExceptionLog ID="ExceptionLogger" runat="server" />
            <br />
        </div>
        <table style="width: 100%; height: 100%;">
            <tr>
                <td>
                    <div class="RightPanelBand"><b>&nbsp;BCS DOCUMENT UPDATE APPROVAL CUSIP REPORT </b></div>
                    <br />
                </td>
            </tr>
            <tr align="left">
                <td>
                    <p class="TinyLabel"><span id="spanHistoryReportInstructions" runat="server">Please enter or select any combination of search criteria below, such as CUSIP, Acc#.</span></p>
                </td>
            </tr>
            <tr>
                <td style="border: solid; border-width: 1px">
                    <table style="width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                        <tr>
                            <td width="100%">
                                <br />
                                <table width="60%">
                                    <tr>
                                        <td style="width: 25%">
                                            <asp:Label ID="lblCUSIP" runat="server" Text="CUSIP: " CssClass="InformationLabel"></asp:Label>
                                        </td>
                                        <td style="width: 75%">
                                            <telerik:RadTextBox ID="txtCUSIP" runat="server" Text="" Width="175px" TextMode="MultiLine" Height="50px" Resize="Both">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                            <asp:Label ID="lblAccNumber" runat="server" Text="Acc#: " CssClass="InformationLabel"></asp:Label>
                                        </td>
                                        <td style="width: 75%">
                                            <telerik:RadTextBox ID="txtAccNumber" runat="server" Text="" Width="175px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 25%">
                                            <asp:Label ID="lblStatus" runat="server" Text="Status: " CssClass="InformationLabel"></asp:Label>
                                        </td>
                                        <td style="width: 75%">
                                            <telerik:RadComboBox ID="comboStatus" runat="server" EmptyMessage="Select Status" AllowCustomText="true" Width="190px" EnableViewState="true">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 25%">
                                            <br />
                                            <asp:Button ID="btnGenerateReport" Text="Generate Report" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateReport_Click"></asp:Button>
                                        </td>
                                        <td style="width: 75%">
                                            <br />
                                            <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="Button" Width="100px" ToolTip="Clear" OnClick="btnClear_Click"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr id="trTabbedReports" runat="server" style="width: 100%;">
                            <td style="width: 100%;">
                                <telerik:RadTabStrip ID="BCSRemoveDuplicateCUSIPTabStrip" runat="server" MultiPageID="RadMultiPageReports"
                                    SelectedIndex="0" Font-Bold="True" AutoPostBack="True" OnTabClick="BCSRemoveDuplicateCUSIPTabStrip_TabClick">
                                    <Tabs>
                                        <telerik:RadTab Text="Duplicate Entries - Gateway Doc Update" PageViewID="pvDuplicateEntries">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="All Entries - Gateway- Doc Update" PageViewID="pvAllEntries" Value="Print">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="Duplicate Entries - Customer - Doc Update" PageViewID="pvDuplicateEntriesCustomerDocUpdate" Value="Print">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="All Entries - Customer - Doc Update" PageViewID="pvAllEntriesCustomerDocUpdate" Value="Print">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage ID="RadMultiPageReports" runat="server" Width="100%">
                                    <telerik:RadPageView ID="pvDuplicateEntries" runat="server" Width="100%" Selected="true">
                                        <div>
                                            <p style="width: 99%; height: 20px;" class="RightPanelBand">
                                                <b>Duplicate Entries - Gateway Doc Update</b>
                                            </p>
                                            <br />
                                            <telerik:RadGrid ID="GridDuplicateCUSIPEntries" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                                OnItemCommand="GridDuplicateCUSIPEntries_Remove" OnNeedDataSource="GridDuplicateCUSIPEntries_NeedDataSource"
                                                EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                                <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                    DataKeyNames="CUSIP" AllowPaging="true" PageSize="25" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn UniqueName="BCSDocUpdateId" DataField="BCSDocUpdateId" HeaderText="BCSDocUpdateId" ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="CUSIP" DataField="CUSIP" HeaderText="CUSIP" ItemStyle-Width="10%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="EdgarID" DataField="EdgarID" HeaderText="Edgar ID" ItemStyle-Width="10%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Accnumber" DataField="Accnumber" HeaderText="Acc#" ItemStyle-Width="15%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="Fund Name" ItemStyle-Width="25%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Document Type" InitializeTemplatesFirst="false" UniqueName="DocumentType" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <a target="_blank" href="<%# Eval("RRDPDFURL") %>" style="color: blue;"><%# Eval("DocumentType") %></a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="DocumentDate" DataField="DocumentDate" HeaderText="Document Date" ItemStyle-Width="15%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="Status" ItemStyle-Width="5%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this entry?" ConfirmDialogType="RadWindow" ButtonType="LinkButton"
                                                            CommandName="Remove" Text="Remove" UniqueName="RemoveColumn" HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right"
                                                            ItemStyle-Font-Bold="true">
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <HeaderStyle Font-Bold="true" />
                                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                                    <NoRecordsTemplate>
                                                        <div>
                                                            There are no Duplicate CUSIP entries.
                                                        </div>
                                                    </NoRecordsTemplate>
                                                </MasterTableView>
                                                <FilterMenu EnableImageSprites="False">
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="pvAllEntries" runat="server" Width="100%">
                                        <div>
                                            <p style="width: 99%; height: 20px;" class="RightPanelBand">
                                                <b>All Entries - Gateway Doc Update</b>
                                            </p>
                                            <br />
                                            <telerik:RadGrid ID="GridAllCUSIPEntries" Width="100%" Height="100%" runat="server"
                                                EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                                OnNeedDataSource="GridAllCUSIPEntries_NeedDataSource">
                                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                                <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                    DataKeyNames="CUSIP" AllowPaging="true" PageSize="25" HierarchyLoadMode="Client" EnableNoRecordsTemplate="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn UniqueName="BCSDocUpdateId" DataField="BCSDocUpdateId" HeaderText="BCSDocUpdateId" ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="CUSIP" DataField="CUSIP" HeaderText="CUSIP" ItemStyle-Width="8%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="EdgarID" DataField="EdgarID" HeaderText="Edgar ID" ItemStyle-Width="7%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Accnumber" DataField="Accnumber" HeaderText="Acc#" ItemStyle-Width="15%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="Fund Name" ItemStyle-Width="22%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Document Type" InitializeTemplatesFirst="false" UniqueName="DocumentType" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <a target="_blank" href="<%# Eval("Status").ToString() == "Filed" ? "javascript:void(0)" : Eval("RRDPDFURL") %>" class="<%# Eval("Status").ToString() == "Filed" ? "cssLiveUpdateRemoveDocumentTypeLink" : "cssLiveUpdateDocumentTypeLink" %>"><%# Eval("DocumentType") %></a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="DocumentDate" DataField="DocumentDate" HeaderText="Document Date" ItemStyle-Width="14%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="Status" ItemStyle-Width="5%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="StatusDate" DataField="StatusDate" HeaderText="Status Date" ItemStyle-Width="14%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <HeaderStyle Font-Bold="true" />
                                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                                    <NoRecordsTemplate>
                                                        <div>
                                                            There are no CUSIPs to display.
                                                        </div>
                                                    </NoRecordsTemplate>
                                                </MasterTableView>
                                                <FilterMenu EnableImageSprites="False">
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="pvDuplicateEntriesCustomerDocUpdate" runat="server" Width="100%">
                                        <div>
                                            <p style="width: 99%; height: 20px;" class="RightPanelBand">
                                                <b>Duplicate Entries - Customer - Doc Update</b>
                                            </p>
                                            <br />
                                            <telerik:RadGrid ID="GridDuplicateEntriesForCUstomerDocUpdate" Width="100%" Height="100%" runat="server" OnItemCommand="GridDuplicateEntriesForCUstomerDocUpdate_ItemCommand"
                                                EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                                OnNeedDataSource="GridDuplicateEntriesForCUstomerDocUpdate_NeedDataSource">
                                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                                <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                    DataKeyNames="CUSIP" AllowPaging="true" PageSize="25" HierarchyLoadMode="Client" EnableNoRecordsTemplate="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn UniqueName="BCSDocUpdateId" DataField="BCSDocUpdateId" HeaderText="BCSDocUpdateId" ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="CUSIP" DataField="CUSIP" HeaderText="CUSIP" ItemStyle-Width="10%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="EdgarID" DataField="EdgarID" HeaderText="Edgar ID" ItemStyle-Width="10%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Accnumber" DataField="Accnumber" HeaderText="Acc#" ItemStyle-Width="15%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="Fund Name" ItemStyle-Width="25%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Document Type" InitializeTemplatesFirst="false" UniqueName="DocumentType" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <a target="_blank" href="<%# Eval("RRDPDFURL") %>" style="color: blue;"><%# Eval("DocumentType") %></a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="DocumentDate" DataField="DocumentDate" HeaderText="Document Date" ItemStyle-Width="15%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="Status" ItemStyle-Width="10%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn ConfirmText="Are you sure you want to remove this entry?" ConfirmDialogType="RadWindow" ButtonType="LinkButton"
                                                            CommandName="RemoveDuplicates" Text="Remove" UniqueName="RemoveColumn" HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right"
                                                            ItemStyle-Font-Bold="true">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridButtonColumn ConfirmText="Are you sure you want to make duplicate this entry?" ConfirmDialogType="RadWindow" ButtonType="LinkButton"
                                                            CommandName="MakeDuplicate" Text="IsNotDuplicate" UniqueName="IsNotDuplicate" HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right"
                                                            ItemStyle-Font-Bold="true">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="ReportType" DataField="ReportType" Display="false" ItemStyle-Width="10%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>


                                                    </Columns>
                                                    <HeaderStyle Font-Bold="true" />
                                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                                    <NoRecordsTemplate>
                                                        <div>
                                                            There are no CUSIPs to display.
                                                        </div>
                                                    </NoRecordsTemplate>
                                                </MasterTableView>
                                                <FilterMenu EnableImageSprites="False">
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="pvAllEntriesCustomerDocUpdate" runat="server" Width="100%">
                                        <div>
                                            <p style="width: 99%; height: 20px;" class="RightPanelBand">
                                                <b>All Entries - Customer - Doc Update</b>
                                            </p>
                                            <br />
                                            <telerik:RadGrid ID="GridAllEntriesCustomerDocUpdate" Width="100%" Height="100%" runat="server"
                                                EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0" AllowPaging="true" AllowCustomPaging="true" PageSize="25"
                                                OnNeedDataSource="GridAllEntriesCustomerDocUpdate_NeedDataSource">
                                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                                <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                                    DataKeyNames="CUSIP" AllowPaging="true" PageSize="25" HierarchyLoadMode="Client" EnableNoRecordsTemplate="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn UniqueName="BCSDocUpdateId" DataField="BCSDocUpdateId" HeaderText="BCSDocUpdateId" ReadOnly="true" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="CUSIP" DataField="CUSIP" HeaderText="CUSIP" ItemStyle-Width="8%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="EdgarID" DataField="EdgarID" HeaderText="Edgar ID" ItemStyle-Width="7%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Accnumber" DataField="Accnumber" HeaderText="Acc#" ItemStyle-Width="15%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="Fund Name" ItemStyle-Width="22%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Document Type" InitializeTemplatesFirst="false" UniqueName="DocumentType" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <a target="_blank" href="<%# Eval("Status").ToString() == "Filed" ? "javascript:void(0)" : Eval("RRDPDFURL") %>" class="<%# Eval("Status").ToString() == "Filed" ? "cssLiveUpdateRemoveDocumentTypeLink" : "cssLiveUpdateDocumentTypeLink" %>"><%# Eval("DocumentType") %></a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="DocumentDate" DataField="DocumentDate" HeaderText="Document Date" ItemStyle-Width="14%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="Status" ItemStyle-Width="5%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="StatusDate" DataField="StatusDate" HeaderText="Status Date" ItemStyle-Width="14%" ReadOnly="true">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <HeaderStyle Font-Bold="true" />
                                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                                    <NoRecordsTemplate>
                                                        <div>
                                                            There are no CUSIPs to display.
                                                        </div>
                                                    </NoRecordsTemplate>
                                                </MasterTableView>
                                                <FilterMenu EnableImageSprites="False">
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadWindowManager Style="z-index: 7001" VisibleStatusbar="false" runat="server"
        RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        ShowOnTopWhenMaximized="false" Skin="Windows7">
        <Windows>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
