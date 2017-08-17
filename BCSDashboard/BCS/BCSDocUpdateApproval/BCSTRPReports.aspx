<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BCSTRPReports.aspx.cs" Inherits="BCSDocUpdateApproval.BCSTRPReports"
    MasterPageFile="~/BCSDocUpdateApproval.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControls/UIErrorDisplay.ascx" TagName="ExceptionLog"
    TagPrefix="UCEx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <telerik:RadCodeBlock ID="ResendPopup" runat="server">
        <script type="text/javascript">

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("FLT_DownLoad") >= 0 || args.get_eventTarget().indexOf("Doc_DownLoad") >= 0 ) {
                    args.set_enableAjax(false);
                }
            }

        </script>
        <style type="text/css">
        </style>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <telerik:RadAjaxManager ID="AjaxManagerBCSTRPReport" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="BCSTRPReportPanel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="BCSTRPReportPanel" LoadingPanelID="BCSTRPReportAjaxPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="BCSTRPReportAjaxPanel" runat="server" Skin="Sunset">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="BCSTRPReportPanel" runat="server" BorderColor="#333333" BorderStyle="None" Width="100%" Height="100%" CssClass="ClientContentDiv" HorizontalAlign="Left">
        <div id="divExceptionLog" runat="server" visible="false">
            <UCEx:ExceptionLog ID="ExceptionLogger" runat="server" />
            <br />
        </div>
        <p style="width: 99%; height: 20px;" class="RightPanelBand">
            <b>BCS TRP Reports</b>
        </p>
        <br />
        <asp:LinkButton ID="lnkHome" runat="server" Style="float: right;" ForeColor="Blue" OnClick="lnkHome_Click">Go back to customer selection</asp:LinkButton>
        <telerik:RadTabStrip ID="BCSTRPReportTabStrip" runat="server" MultiPageID="RadMultiPageReports"
            SelectedIndex="0" Font-Bold="True" AutoPostBack="True" OnTabClick="BCSTRPReportTabStrip_TabClick">
            <Tabs>
                <telerik:RadTab Text="Documents Status" PageViewID="pvFLTFTPINFO">
                </telerik:RadTab>
                <telerik:RadTab Text="Missing - Doc Info" PageViewID="pvFLTMissing" Value="Print">
                </telerik:RadTab>
                <telerik:RadTab Text="Missing - CUSIP(s) in RP" PageViewID="pvCUSIPMissinginRP" Value="Print">
                </telerik:RadTab>
                <telerik:RadTab Text="Customer - FLT" PageViewID="pvCustomerFLT" Value="CustomerFLT">
                </telerik:RadTab>
                <telerik:RadTab Text="Customer - Docs" PageViewID="pvCustomerDocs" Value="CustomerDocs">
                </telerik:RadTab>
                  <telerik:RadTab Text="Blank FLT CUSIP Details" PageViewID="pvBlankFltCUSIP" Value="blankFltCUSIP">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPageReports" runat="server" Width="100%">
            <telerik:RadPageView ID="pvFLTFTPINFO" runat="server" Width="100%" Selected="true">
                <p style="width: 99%; height: 20px;" class="RightPanelBand">
                    <b>Documents Status</b>
                </p>
                <br />
                <div>
                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                        <tr>
                            <td>
                                <table style="width: 100%; height: 100%; vertical-align: central; padding-top: 0px">
                                    <tr align="left">
                                        <td>
                                            <p class="TinyLabel"><span id="spanHistoryReportInstructions" runat="server">Note: This page gives you information on document(s) status after comparing documents received on FTP with FLT.</span></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%">
                                            <br />
                                            <table width="60%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        <asp:Label ID="lblCUSIP" runat="server" Text="CUSIP: " CssClass="InformationLabel"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <telerik:RadTextBox ID="txtCUSIP" runat="server" Text="" Width="250px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 25%; padding-top: 2px">
                                                        <asp:Label ID="Label1" runat="server" Text="FTP PDF Status: " CssClass="InformationLabel"></asp:Label>
                                                    </td>
                                                    <td style="width: 75%; padding-top: 2px">
                                                        <telerik:RadComboBox ID="comboPDFStatus" runat="server" EmptyMessage="Select PDF Status" AllowCustomText="true" Width="250px">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Value="0" Text="" />
                                                                <telerik:RadComboBoxItem Value="1" Text="PDF received on FTP" />
                                                                <telerik:RadComboBoxItem Value="2" Text="PDF is not yet received on FTP" />
                                                            </Items>
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
                                </table>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="GridFLTFTPInfoDataEntries" CssClass="RadGrid RadGrid_Default" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="10"
                                    OnNeedDataSource="GridFLTFTPInfoDataEntries_NeedDataSource"
                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                        DataKeyNames="CompanyName" AllowPaging="true" PageSize="10" HierarchyLoadMode="Client" Name="FLTFTPInfoDataEntries" EnableNoRecordsTemplate="true">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="CompanyName" DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width="20%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="FundName" DataField="FundName" HeaderText="Fund Name" ItemStyle-Width="25%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="FLTCUSIP" DataField="FLTCUSIP" HeaderText="FLT CUSIP" ItemStyle-Width="15%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="RPCUSIP" DataField="RPCUSIP" HeaderText="RP CUSIP" ItemStyle-Width="15%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="PDF Received On FTP" InitializeTemplatesFirst="false" UniqueName="DatePDFReceivedOnFTP" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDatePDFReceivedOnFTP" Text='<%# Eval("DatePDFReceivedOnFTP").ToString() != "" ? "Received on " + Eval("DatePDFReceivedOnFTP").ToString() : "PDF is not yet received on FTP" %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="true" />
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <NoRecordsTemplate>
                                            <div>
                                                There are no FLT FTP Infomation entries.
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pvFLTMissing" runat="server" Width="100%">
                <p style="width: 99%; height: 20px;" class="RightPanelBand">
                    <b>Missing - Doc Info</b>
                </p>
                <br />
                <div>
                    <table style="width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                        <tr align="left">
                            <td>
                                <p class="TinyLabel"><span id="span2">Note: This page gives you information on missing FLT entries for documents that are available on FTP.</span></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="GridFLTMissingDataEntries" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="10"
                                    OnNeedDataSource="GridFLTMissingDataEntries_NeedDataSource"
                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                        DataKeyNames="CompanyName" AllowPaging="true" PageSize="10" HierarchyLoadMode="Client" Name="FLTMissingDataEntries" EnableNoRecordsTemplate="true">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="CompanyName" DataField="CompanyName" HeaderText="Company Name" ItemStyle-Width="35%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="File Name" InitializeTemplatesFirst="false" UniqueName="FileName" ItemStyle-Width="35%">
                                                <ItemTemplate>
                                                    <a target="_blank" href="<%# Eval("Path") %>" style="color: blue;"><%# Eval("FileName") %></a>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="DateReceivedOnFTP" DataField="DateReceivedOnFTP" HeaderText="Date Received On FTP" ItemStyle-Width="40%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="true" />
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <NoRecordsTemplate>
                                            <div>
                                                There are no FLT Missing entries.
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pvCUSIPMissinginRP" runat="server" Width="100%">
                <p style="width: 99%; height: 20px;" class="RightPanelBand">
                    <b>Missing - CUSIP(s) in RP</b>
                </p>
                <br />
                <div>
                    <table style="width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                        <tr align="left">
                            <td>
                                <p class="TinyLabel"><span id="span1">Note: This page gives you information on CUSIP(s) that are not avaialble in RP system.</span></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="GridRPMissingCUSIPDataEntries" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="10"
                                    OnNeedDataSource="GridRPMissingCUSIPDataEntries_NeedDataSource"
                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
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
            </telerik:RadPageView>

            <telerik:RadPageView ID="pvCustomerFLT" runat="server" Width="100%">
                <p style="width: 99%; height: 20px;" class="RightPanelBand">
                    <b>Customer  - FLT</b>
                </p>
                <br />
                <div>
                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                        <tr>
                            <td>
                                <table style="width: 100%; height: 100%; vertical-align: central; padding-top: 0px">
                                    <tr align="left">
                                        <td>
                                            <p class="TinyLabel"><span id="span3" runat="server">Note: This page gives you information on customer FLT.</span></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%">
                                            <br />
                                            <table width="60%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        <asp:Label ID="lblFltDate" runat="server" Text="FLT Date: " CssClass="InformationLabel"></asp:Label>
                                                        <b style="color: red">*</b>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <telerik:RadDatePicker ID="dateFLT" runat="server">
                                                        </telerik:RadDatePicker>

                                                        <asp:RequiredFieldValidator runat="server" ID="reqDateFLT" ControlToValidate="dateFLT" ErrorMessage="Select Date!" ForeColor="Red" ValidationGroup="grpValFLT">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>


                                                        <%-- <asp:RequiredFieldValidator runat="server" id="" ControlToValidate="" ErrorMessage="Select Date"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        <br />
                                                        <asp:Button ID="btnGenerateReportFLT" Text="Generate Report" ValidationGroup="grpValFLT" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateReportFLT_Click"></asp:Button>
                                                    </td>
                                                    <td style="width: 75%" colspan="2">
                                                        <br />
                                                        <asp:Button ID="btnClearFLT" Text="Clear" runat="server" CssClass="Button" Width="100px" ToolTip="Clear" OnClick="btnClearFLT_Click"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>
                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                <telerik:RadGrid ID="GridCustomerFLT" Width="100%" Height="100%" runat="server" AllowPaging="false" AllowCustomPaging="false"
                                    OnItemCommand="GridCustomerFLT_ItemCommand"
                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                        AllowPaging="false" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                        <Columns>

                                            <telerik:GridTemplateColumn HeaderText="FLT Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="FLT_DownLoad" runat="server" ForeColor="Blue" CommandName="download_file" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("FileName") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridBoundColumn UniqueName="DateReceived" DataField="ReceivedTime" HeaderText="Date Received/Time " ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="true" />
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <NoRecordsTemplate>
                                            <div>
                                                There are no FLT for selected date.
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
            </telerik:RadPageView>

            <telerik:RadPageView ID="pvCustomerDocs" runat="server" Width="100%">
                <p style="width: 99%; height: 20px;" class="RightPanelBand">
                    <b>Customer  - Docs</b>
                </p>
                <br />
                <div>
                    <table style="border: solid; border-width: 1px; width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                        <tr>
                            <td>
                                <table style="width: 100%; height: 100%; vertical-align: central; padding-top: 0px">
                                    <tr align="left">
                                        <td>
                                            <p class="TinyLabel"><span id="span4" runat="server">Note: This page gives you information on customer Docs.</span></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%">
                                            <br />
                                            <table width="60%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        <asp:Label ID="lblDocDate" runat="server" Text="Doc Date: " CssClass="InformationLabel"></asp:Label>
                                                        <b style="color: red">*</b>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <telerik:RadDatePicker ID="dateDoc" runat="server">
                                                        </telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator runat="server" ID="reqDateDoc" ControlToValidate="dateDoc" ValidationGroup="valCustomerDoc" ErrorMessage="Select Date!" ForeColor="Red"></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%">
                                                        <br />
                                                        <asp:Button ID="btnGenerateReportCustomerDoc" Text="Generate Report" ValidationGroup="valCustomerDoc" runat="server" CssClass="Button" ToolTip="Generate Report" OnClick="btnGenerateReportCustomerDoc_Click"></asp:Button>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <br />
                                                        <asp:Button ID="btnClearREportCustomerDocs" Text="Clear" runat="server" CssClass="Button" Width="100px" ToolTip="Clear" OnClick="btnClearREportCustomerDocs_Click"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>
                            <td style="padding-bottom: 10px; padding-top: 10px !important">
                                <telerik:RadGrid ID="GridCustomerDocs" Width="100%" Height="100%" runat="server" AllowPaging="false" AllowCustomPaging="false"
                                    OnItemCommand="GridCustomerDocs_ItemCommand"
                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                        AllowPaging="false" HierarchyLoadMode="Client" Name="UnviewedEmailHistory" EnableNoRecordsTemplate="true">
                                        <Columns>

                                            <telerik:GridTemplateColumn HeaderText="Document Name">
                                                <ItemTemplate>
                                                    <a target="_blank" href="<%# Eval("DirectoryName") %>" style="color: blue;"><%# Eval("FileName") %></a>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridBoundColumn UniqueName="DocDate" DataField="DateReceived" HeaderText="Date Received/Time " ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="true" />
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <NoRecordsTemplate>
                                            <div>
                                                There are no Docs for selected date.
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
            </telerik:RadPageView>

            <telerik:RadPageView ID="pvBlankFltCUSIP" runat="server" Width="100%">
                <p style="width: 99%; height: 20px;" class="RightPanelBand">
                    <b>Blank Flt CUSIP Details</b>
                </p>
                <br />
                <div>
                    <table style="width: 100%; height: 100%; vertical-align: central; padding-left: 20px; padding-right: 20px; padding-top: 0px">
                        <tr align="left">
                            <td>
                                <p class="TinyLabel"><span id="span5">Note: This page gives you information on Blank FLT CUSIP details.</span></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="GridBlankFltCusipDetails" Width="100%" Height="100%" runat="server" AllowPaging="true" AllowCustomPaging="true" PageSize="10"
                                    OnNeedDataSource="GridBlankFltCusipDetails_NeedDataSource"
                                    EnableViewState="true" AllowAutomaticUpdates="true" CellSpacing="0">
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                    <MasterTableView AutoGenerateColumns="False" ShowHeader="true" EditMode="InPlace"
                                         AllowPaging="true" PageSize="10" HierarchyLoadMode="Client" Name="BlankFLTCUSIPDetails" EnableNoRecordsTemplate="true">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="FUNDCODE" DataField="FUNDCODE" HeaderText="FUND CODE" ItemStyle-Width="15%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="FUNDNAME" DataField="FUNDNAME" HeaderText="FUND NAME" ItemStyle-Width="30%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="FUNDTYPE" DataField="FUNDTYPE" HeaderText="FUND TYPE" ItemStyle-Width="13%" ReadOnly="true">
                                            </telerik:GridBoundColumn> 
                                           <telerik:GridBoundColumn UniqueName="FUNDTELEACCESSCODE" DataField="FUNDTELEACCESSCODE" HeaderText="FUND TELE ACCESS CODE" ItemStyle-Width="20%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn UniqueName="FUNDCUSIPNUMBER" DataField="FUNDCUSIPNUMBER" HeaderText="FUND CUSIP NUMBER" ItemStyle-Width="30%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn UniqueName="FUNDCHKHEADINGCODE" DataField="FUNDCHKHEADINGCODE" HeaderText="FUND CHK HEADING CODE" ItemStyle-Width="30%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn UniqueName="FUNDGROUPNUMBER" DataField="FUNDGROUPNUMBER" HeaderText="FUND GROUP NUMBER" ItemStyle-Width="30%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn UniqueName="FUNDPROSPECTUSINSERT" DataField="FUNDPROSPECTUSINSERT" HeaderText="FUND PROSPECTUS INSERT" ItemStyle-Width="30%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                           <telerik:GridBoundColumn UniqueName="FUNDPROSPECTUSINSERT2" DataField="FUNDPROSPECTUSINSERT2" HeaderText="FUND PROSPECTUS INSERT2" ItemStyle-Width="30%" ReadOnly="true">
                                            </telerik:GridBoundColumn>   
                                            <telerik:GridBoundColumn UniqueName="FUNDTICKERSYMBOL" DataField="FUNDTICKERSYMBOL" HeaderText="FUND TICKER SYMBOL" ItemStyle-Width="30%" ReadOnly="true">
                                            </telerik:GridBoundColumn> 
                                             <telerik:GridBoundColumn UniqueName="FUNDDocName" DataField="FUNDDocName" HeaderText="FUND DocName" ItemStyle-Width="25%" ReadOnly="true">
                                            </telerik:GridBoundColumn> 
                                            <telerik:GridBoundColumn UniqueName="DateFLTRecordHasChanged" DataField="DateFLTRecordHasChanged" HeaderText="Date FLT Record HasChanged" ItemStyle-Width="40%" ReadOnly="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="true" />
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <NoRecordsTemplate>
                                            <div>
                                                There are no FLT Missing entries.
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
            </telerik:RadPageView>
        </telerik:RadMultiPage>

    </asp:Panel>
    <telerik:RadWindowManager Style="z-index: 7001" VisibleStatusbar="false" runat="server"
        RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        ShowOnTopWhenMaximized="false" Skin="Windows7">
        <Windows>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
