using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using BCSDocUpdateApprovalV2.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "*")]
    [CustomExceptionFilter]
     [RoutePrefix("api/WATCHLIST")]
    public class WatchlistController : ApiController
    {
        private const string watchListTransamericaDocumentPath = "watchListTransamericaDocumentPath";
        private const string watchListAllianceBernsteinDocumentPath = "watchListAllianceBernsteinDocumentPath";
        
        [Route("Report")]
         [HttpPost]
         public List<SANFileDetails> Report([FromBody]JObject data)
         {
             dynamic gridData = data;
             List<SANFileDetails> watchListData = new List<SANFileDetails>();
             string doumentPath = string.Empty;
             string clientName = gridData.clientName.ToString();
             switch (clientName)
             {
                 case "Transamerica": doumentPath = ConfigurationManager.AppSettings.Get(watchListTransamericaDocumentPath); break;
                 case "AllianceBernstein": doumentPath = ConfigurationManager.AppSettings.Get(watchListAllianceBernsteinDocumentPath); break;
             }
             watchListData = new BCSDocUpdateApprovalFactory().GetSANFileDetails(doumentPath, Convert.ToDateTime(gridData.watchlistDate), "*.txt");
             return watchListData;
           
         }
    }
}
