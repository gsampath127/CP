using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.SearchEntities.Client
{
    public class DocumentSubstitutionSearchDetail
    : AuditedSearchDetail<DocumentSubstitutionObjectModel>, ISearchDetailCopyAs<DocumentSubstitutionSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public ValueCompare IdCompare { get; set; }
        /// <summary>
        /// Name.
        /// </summary>
        /// <value>The document type identifier.</value>
        public string DocumentType { get; set; }
        /// <summary>
        /// Determines the type of comparison for the Name property.
        /// </summary>
        /// <value>The document type identifier compare.</value>
        public TextCompare DocumentTypeCompare { get; set; }
       
        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<DocumentSubstitutionObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.Id, entity.Id, this.IdCompare)
                    && this.Match(this.DocumentType, entity.DocumentType, this.DocumentTypeCompare);
                   
            }
        }

        #endregion

        TCopy ISearchDetailCopyAs<DocumentSubstitutionSearchDetail>.CopyAs<TCopy>()
        {
            throw new NotImplementedException();
        }
    }
}
