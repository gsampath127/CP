using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RRD.FSG.RP.Web.UI.Hosted.ViewModels
{
    /// <summary>
    /// View model for client
    /// </summary>
    public class ClientViewModel
    {
        [DisplayName("Client ID")]
        public Int64 ClientID { get; set; }
        [DisplayName("Client Name")]
        public string ClientName { get; set; }
        [DisplayName("Vertical Market")]
        public string VerticalMarket { get; set; }
        [DisplayName("Default Page Name")]
        public string DefaultPageName { get; set; }
    }
}