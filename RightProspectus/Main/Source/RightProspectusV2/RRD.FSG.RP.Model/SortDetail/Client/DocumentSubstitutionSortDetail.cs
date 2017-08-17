using RRD.FSG.RP.Model.Entities.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.SortDetail.Client
{
   public class DocumentSubstitutionSortDetail
     : AuditedSortDetail<DocumentSubstitutionObjectModel>
    {

        /// <summary>
        /// Column to be sorted.
        /// </summary>
       public virtual new DocumentSubstitutionSortColumn Column
        {
            get { return (DocumentSubstitutionSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
       public override IEnumerable<DocumentSubstitutionObjectModel> Sort(IEnumerable<DocumentSubstitutionObjectModel> source)
        {
            switch (this.Column)
            {
                case DocumentSubstitutionSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case DocumentSubstitutionSortColumn.DocumentType:
                    return this.Sort(source, entity => entity.DocumentType);
                case DocumentSubstitutionSortColumn.SubstituteDocumentType:
                    return this.Sort(source, entity => entity.SubstituteDocumentType);
                case DocumentSubstitutionSortColumn.NDocumentType:
                    return this.Sort(source, entity => entity.NDocumentType);
                default:
                    return base.Sort(source);
            }
        }

        /// <summary>
        /// Compares two entities using teh sort properties of this instance.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
       public override int Compare(DocumentSubstitutionObjectModel x, DocumentSubstitutionObjectModel y)
        {
            switch (this.Column)
            {
                case DocumentSubstitutionSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case DocumentSubstitutionSortColumn.DocumentType:
                    return this.Compare(x.DocumentType, y.DocumentType);
                case DocumentSubstitutionSortColumn.SubstituteDocumentType:
                    return this.Compare(x.SubstituteDocumentType, y.SubstituteDocumentType);
                case DocumentSubstitutionSortColumn.NDocumentType:
                    return this.Compare(x.NDocumentType, y.NDocumentType);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
