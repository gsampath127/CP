using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.SearchEntities.Client
{
    public class BrowserVersionSearchDetail
         : AuditedSearchDetail<BrowserVersionObjectModel>, ISearchDetailCopyAs<BrowserVersionSearchDetail>
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
        public string Name { get; set; }
        /// <summary>
        /// Determines the type of comparison for the Name property.
        /// </summary>
        /// <value>The document type identifier compare.</value>
        public TextCompare NameCompare { get; set; }
        /// <summary>
        /// Version.
        /// </summary>
        /// <value>The name of the document type.</value>
        public int? Version { get; set; }
        /// <summary>
        /// Determines the type of comparison for the Version property..
        /// </summary>
        /// <value>The document type name compare.</value>
        public ValueCompare VersionCompare { get; set; }
        /// <summary>
        /// DownloadURL.
        /// </summary>
        /// <value>The external identifier.</value>
        public string DownloadURL { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DownloadURL property.
        /// </summary>
        /// <value>The external identifier compare.</value>
        public TextCompare DownloadURLCompare { get; set; }



        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<BrowserVersionObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.Id,entity.Id,this.IdCompare)
                    && this.Match(this.Name, entity.Name, this.NameCompare)
                    && this.Match(this.Version, entity.MinimumVersion, this.VersionCompare)
                    && this.Match(this.DownloadURL, entity.DownloadUrl, this.DownloadURLCompare);
            }
        }

        #endregion

        TCopy ISearchDetailCopyAs<BrowserVersionSearchDetail>.CopyAs<TCopy>()
        {
            throw new NotImplementedException();
        }
    }
}
