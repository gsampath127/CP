using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel.Factories
{
    public enum BCSApplicationName
    {
        BCSInitialLoadBatch,
        BCSDocUpdateValidationService,
        BCSDocUpdateSlinkIntegrationService,
        BCSDocUpdateApproval,
        BCSDocumentSynchonizerService,
        BCSLinkWebService,
        BCSTRPReportService
    }

    public enum FromProcess
    {
        NewlyAddedOrModifiedCUSIPS = 1,
        ValidationProcesssToCheckSPInFLMode = 2
    }

    public enum BCSReportType
    {
        [Description("Difference Report")]
        DifferenceReport = 0,

        [Description("Added Report")]
        AddedReport = 1,

        [Description("Removed Report")]
        RemovedReport = 2,

        [Description("CUSIP Not Present in RP")]
        NotInRP = 3
    }

    public enum SlinkStatus
    {
        [Description("EX")]
        EX = 0,

        [Description("AP")]
        AP = 1,

        [Description("OP")]
        OP = 2,

        [Description("APC")]
        APC = 3,

        [Description("OPC")]
        OPC = 4
    }

    public enum LiveUpdateStatus
    {
        [Description("EX")]
        EX = 0,

        [Description("AP")]
        AP = 1,

        [Description("OP")]
        OP = 2,

        [Description("APC")]
        APC = 3,

        [Description("OPC")]
        OPC = 4,

        [Description("Filed")]
        Filed = 5,

        [Description("Processed")]
        Processed = 6
       
    }

    public enum DocUpdateDownstream
    {
        [Description("NU-File")]
         NUFile=0,

        [Description("IP-File")]
         IPFile=1,

        [Description("Gateway - Doc Update")]
        GatewayDocUpdate = 2
    }

    public enum CustomerUpdateDownstream
    {
        [Description("NU-File")]
        NUFile = 0,

        [Description("IP-File")]
        IPFile = 1,

        [Description("Customer - Doc Update")]
        CustomerDocUpdate = 2
    }
       
    public enum FailureNotificationEmailTemplate
    {
        WatchlistFailureNotification = 1,
        CustDocUPDTFailureNotification = 2
    }
    public enum BCSReportSelect
    {
        [Description("Duplicate CUSIP Report")]
        DuplicateCUSIPReport = 0,

        [Description("Security Type Report")]
        SecurityTypeReport = 1,

        [Description("Missing CUSIP(s) In RP")]
        MissingCUSIPReport = 2
    }  
}
