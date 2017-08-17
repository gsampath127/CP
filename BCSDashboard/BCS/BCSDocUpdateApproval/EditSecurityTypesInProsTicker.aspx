<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSecurityTypesInProsTicker.aspx.cs" Inherits="BCSDocUpdateApproval.EditSecurityTypesInProsTicker" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="BCSDocUpdateApprovalMasterHead">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server" ID="select" />
        <telerik:RadAjaxManager ID="AjaxManagerBCSRemoveDuplicateCUSIP" runat="server">
            <%--  <ClientEvents OnRequestStart="onRequestStart" />--%>
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


        <table style="width: 100%; height: 100%; padding-left: 20px; padding-right: 20px; padding-top: 20px">
            <tr>
                <td style="vertical-align:top;width:70px">
                    <asp:Label ID="lblSecurityType" runat="server" Text="Level3"></asp:Label> 
                    <b style="color: red">*</b>                   
                </td>                
                <td>
                    <telerik:RadComboBox ID="cmbSecurityType" runat="server" EmptyMessage="----Select----" AllowCustomText="false" Width="250px" EnableViewState="true">
                    </telerik:RadComboBox>
                    <span style="margin-left: 20px;">
                        <asp:RequiredFieldValidator ID="reqReportType" ControlToValidate="cmbSecurityType" runat="server" ErrorMessage="Please select a Report Type" ForeColor="Red" ValidationGroup="cmbSecurityTypeValidationGroup"></asp:RequiredFieldValidator>
                    </span>
                </td>
            </tr>
            <tr style="height: 10px">
                <td colspan="2"></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="Button" ValidationGroup="cmbSecurityTypeValidationGroup" ToolTip="Save" OnClick="btnSave_Click"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="Button" ToolTip="Close" OnClick="btnClose_Click"></asp:Button>
                </td>
            </tr>
            <tr style="height: 10px">
                <td colspan="3"></td>
            </tr>
            <tr>                
                <td colspan="3">
                    <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
