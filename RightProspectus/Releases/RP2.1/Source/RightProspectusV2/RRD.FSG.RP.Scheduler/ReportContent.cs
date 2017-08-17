// ***********************************************************************
// Assembly         : RRD.FSG.RP.Scheduler
// Author           : 
// Created          : 11-09-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Utilities;

namespace RRD.FSG.RP.Scheduler.Task.Reports
{
    /// <summary>
    /// Class ReportContent.
    /// </summary>
    public class ReportContent
    {

        /// <summary>
        /// The report content factory
        /// </summary>
        private IFactory<ReportContentObjectModel, int> reportContentFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportContent"/> class.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        public ReportContent(string clientName)
        {
            reportContentFactory = RPV2Resolver.Resolve<IFactory<ReportContentObjectModel, int>>("ReportContent");
            reportContentFactory.ClientName = clientName;
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportContent" /> class.
        /// </summary>
        /// <param name="ReportContentFactory">The report content factory.</param>
        /// <param name="clientName">Name of the client.</param>
        public ReportContent(IFactory<ReportContentObjectModel, int>ReportContentFactory, string clientName)
        {
            reportContentFactory = ReportContentFactory;
            reportContentFactory.ClientName = clientName;

        }

        /// <summary>
        /// Saves all Report Details
        /// </summary>
        /// <param name="objReportContentModel">The object report content model.</param>
         public void SaveReportContent(ReportContentObjectModel objReportContentModel)
         {
               
             
                objReportContentModel.ContentUri = "";
                objReportContentModel.ReportRunDate = objReportContentModel.ReportToDate;
                objReportContentModel.MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                objReportContentModel.IsPrivate = 0;
               reportContentFactory.SaveEntity(objReportContentModel);
              
       
        }

       

    }
}
