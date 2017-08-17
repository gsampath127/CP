using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.System
{
    public class BrowserVersionObjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinimumVersion { get; set; }
        public string DownloadUrl { get; set; }
        public bool IsLatest { get; set; }
        public string LogoStaticResourceURL { get; set; }
    }
}
