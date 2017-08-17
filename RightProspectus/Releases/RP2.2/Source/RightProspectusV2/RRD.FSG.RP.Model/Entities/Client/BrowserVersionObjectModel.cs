using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.Client
{
    public class BrowserVersionObjectModel : AuditedBaseModel<int>, IComparable<BrowserVersionObjectModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinimumVersion { get; set; }
        public string DownloadUrl { get; set; }
        public bool IsLatest { get; set; }
        public string LogoStaticResourceURL { get; set; }

        public int SelectedId { get; set; }

        public int CompareTo(BrowserVersionObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
