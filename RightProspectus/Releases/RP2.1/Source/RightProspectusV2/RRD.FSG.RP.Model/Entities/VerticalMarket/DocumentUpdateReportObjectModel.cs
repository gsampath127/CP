using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.VerticalMarket
{
    public class DocumentUpdateReportObjectModel : AuditedBaseModel<int>, IComparable<DocumentUpdateReportObjectModel>
    {
        public DateTime DocumentDate { get; set; }
        public string CUSIP { get; set; }
        public string DocType { get; set; }
        public string IsDocUpdated { get; set; }

        int IComparable<DocumentUpdateReportObjectModel>.CompareTo(DocumentUpdateReportObjectModel other)
        {
            throw new NotImplementedException();
        }
    }
}
