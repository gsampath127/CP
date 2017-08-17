using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Entities.Client
{
    public class DocumentSubstitutionObjectModel : AuditedBaseModel<int>, IComparable<DocumentSubstitutionObjectModel>
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public string SubstituteDocumentType { get; set; }
        public string NDocumentType { get; set; }
        public int SelectedId { get; set; }
        public int CompareTo(DocumentSubstitutionObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
