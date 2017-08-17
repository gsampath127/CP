using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using System.Threading;
using RRD.FSG.RP.Utilities;
using System.Configuration.Install;
using System.Collections;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Configuration;

namespace RRD.FSG.RP.Services.ValidationService
{
    public partial class ValidationService : ServiceBase
    {
        public ValidationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ThreadStart CheckCUSIPMergerLiqudationThreadStart = new ThreadStart(this.CheckCUSIPMergerLiqudation);
            Thread CheckCUSIPMergerLiqudationThread = new Thread(CheckCUSIPMergerLiqudationThreadStart);
            CheckCUSIPMergerLiqudationThread.Start();

            EmailHelper.SendEmail(ConfigValues.EmailFrom, ConfigValues.ValidationServiceExceptionEmailTo, Process.GetCurrentProcess().ProcessName,
                    "RRD.FSG.RP.Services.ValidationService  Started " + ConfigValues.AppEnvironment, null, null, null, "support", null);
        }

        protected override void OnStop()
        {
            EmailHelper.SendEmail(ConfigValues.EmailFrom, ConfigValues.ValidationServiceExceptionEmailTo, Process.GetCurrentProcess().ProcessName, 
                    "RRD.FSG.RP.Services.ValidationService  Stopped " + ConfigValues.AppEnvironment, null, null, null, "support", null);
        }
        /// <summary>
        /// CheckCUSIPMergerLiqudation.
        /// </summary>
        public void CheckCUSIPMergerLiqudation()
        {
            while (true)
            {
                TimeSpan TimeFromCUSIPMergerLiqudationReport = TimeSpan.Parse(ConfigurationManager.AppSettings["TimeFromCUSIPMergerLiqudationReport"]);
                TimeSpan TimeToCUSIPMergerLiqudationReport = TimeSpan.Parse(ConfigurationManager.AppSettings["TimeToCUSIPMergerLiqudationReport"]);
                DateTime currenttime = DateTime.Now;

                if (currenttime.TimeOfDay >= TimeFromCUSIPMergerLiqudationReport && currenttime.TimeOfDay <= TimeToCUSIPMergerLiqudationReport)
                {
                    try
                    {
                        DataAccess dataaccess = new DataAccess();
                        ClientFactory clientFactory = new ClientFactory(dataaccess);

                        List<CUSIPMergerLiqudationReportObjectModel> lstResult = new List<CUSIPMergerLiqudationReportObjectModel>();
                        IEnumerable<ClientObjectModel> clients = clientFactory.GetAllEntities();

                        DataTable dt = new DataTable();
                        dt.Columns.Add("ClientName", typeof(string));
                        dt.Columns.Add("MarketID", typeof(string));
                        dt.Columns.Add("TaxonomyID", typeof(int));
                        dt.Columns.Add("Level", typeof(int));

                        foreach (var client in clients)
                        {
                            TaxonomyAssociationFactory objFactory = new TaxonomyAssociationFactory(dataaccess);

                            objFactory.ClientName = client.ClientName;

                            var details = objFactory.GetAllEntities();
                            foreach (var detail in details)
                            {
                                dt.Rows.Add(client.ClientName, detail.MarketId, detail.TaxonomyId, 0);
                            }

                        }

                        HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(dataaccess);
                        lstResult = obj.GetCUSIPMergerLiqudationReportData(dt);

                        SendCUSIPMergerLiqudationReport(lstResult);

                        Thread.Sleep(ConfigValues.SleepTimeCUSIPMergerLiqudationReport);
                    }
                    catch (Exception exception)
                    {

                        EmailHelper.SendEmail(ConfigValues.EmailFrom, ConfigValues.ValidationServiceExceptionEmailTo,
                                                    "CheckCUSIPMergerLiqudation Exception From RRD.FSG.RP.Services.ValidationService",
                                                    exception.Message, null, null, null, "support", null);

                        Thread.Sleep(ConfigValues.SleepTimeWhenError);
                    }
                }

                Thread.Sleep(120000);
            }
        }

        private void SendCUSIPMergerLiqudationReport(List<CUSIPMergerLiqudationReportObjectModel> lstResult)
        {
            if (lstResult.Count > 0)
            {
                GridView emailDoc = new GridView();

                BoundField ClientName = new BoundField();
                ClientName.DataField = "ClientName";
                ClientName.HeaderText = "Client Name";
                emailDoc.Columns.Add(ClientName);

                BoundField Cusip = new BoundField();
                Cusip.DataField = "MarketID";
                Cusip.HeaderText = "Market ID";
                emailDoc.Columns.Add(Cusip);

                BoundField Status = new BoundField();
                Status.DataField = "Status";
                Status.HeaderText = "Status";
                emailDoc.Columns.Add(Status);

                emailDoc.AutoGenerateColumns = false;

                emailDoc.Width = Unit.Percentage(90);
                emailDoc.RowDataBound += emailDoc_RowDataBound;
                emailDoc.DataSource = from t in lstResult
                                      select new
                                      {
                                          t.ClientName,
                                          t.MarketID,
                                          t.Status

                                      };

                emailDoc.DataBind();

                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                emailDoc.RenderControl(hw);
                string gridHtml = sb.ToString();

                StringBuilder MailBody = new StringBuilder();

                MailBody.Append("<html><body><p>");
                MailBody.Append("CUSIP Merger Liquidation Report");
                MailBody.Append("<br/>");
                MailBody.Append(gridHtml);
                MailBody.Append("</p></body></html>");

                EmailHelper.SendEmail(ConfigValues.EmailFrom, ConfigValues.CUSIPMergerLiqudationEmailTo,
                                            ConfigValues.CUSIPMergerLiqudationEmailSub, MailBody.ToString(), null, null, null, "support", "");

            }
        }

        private static void emailDoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Attributes.Add("style", "font-family: Verdana, Arial, Tahoma, Sans-Serif;vertical-align:middle;padding-left: 10px;line-height: 2.1;color: #ffffff;font-size: 12px;font-weight: 600;background: #1a6ab1;padding-bottom: 5px;");

                    e.Row.Cells[0].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:30%");
                    e.Row.Cells[1].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:30%");
                    e.Row.Cells[2].Attributes.Add("style", "text-align: left;padding-left: 10px;border:1px;border-style:solid;border-color:#333333;width:30%");                  

                    break;
                case DataControlRowType.DataRow:
                    if (e.Row.RowState == DataControlRowState.Normal)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#ffffff;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding: 0 10px 0 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding: 0 10px 0 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding: 0 10px 0 10px;");                        
                    }
                    else if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("style", "font-family:verdana, helvetica, arial, sans-serif;text-align:left;color: #555555;line-height: 2.1;font-size: 12px;font-weight: 500;overflow:auto;resize: none;background:#A9A9A9;border:1px;border-style:solid;border-color:#333333");

                        e.Row.Cells[0].Attributes.Add("style", "padding: 0 10px 0 10px;");
                        e.Row.Cells[1].Attributes.Add("style", "padding: 0 10px 0 10px;");
                        e.Row.Cells[2].Attributes.Add("style", "padding: 0 10px 0 10px;");                        
                    }
                    break;
            }

        }
    }

    [RunInstallerAttribute(true)]
    public partial class Installer1 : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;

        public Installer1()
        {

            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            // Service will run under system account
            processInstaller.Account = ServiceAccount.LocalSystem;

            // Service will have Start Type of Manual
            serviceInstaller.StartType = ServiceStartMode.Manual;

            serviceInstaller.ServiceName = "RRD.FSG.RP.Services.ValidationService";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }

    }

}
