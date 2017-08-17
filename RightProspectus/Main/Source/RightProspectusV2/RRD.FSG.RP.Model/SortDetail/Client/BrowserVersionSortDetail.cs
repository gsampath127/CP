using RRD.FSG.RP.Model.Entities.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.SortDetail.Client
{
    public class BrowserVersionSortDetail
        : AuditedSortDetail<BrowserVersionObjectModel>
    {

        /// <summary>
        /// Column to be sorted.
        /// </summary>
        public virtual new BrowserVersionSortColumn Column
        {
            get { return (BrowserVersionSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override IEnumerable<BrowserVersionObjectModel> Sort(IEnumerable<BrowserVersionObjectModel> source)
        {
            switch (this.Column)
            {
                case BrowserVersionSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case BrowserVersionSortColumn.Name:
                    return this.Sort(source, entity => entity.Name);
                case BrowserVersionSortColumn.Version:
                    return this.Sort(source, entity => entity.MinimumVersion);
                case BrowserVersionSortColumn.DownloadURL:
                    return this.Sort(source, entity => entity.DownloadUrl);
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
        public override int Compare(BrowserVersionObjectModel x, BrowserVersionObjectModel y)
        {
            switch (this.Column)
            {
                case BrowserVersionSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case BrowserVersionSortColumn.Name:
                    return this.Compare(x.Name, y.Name);
                case BrowserVersionSortColumn.Version:
                    return this.Compare(x.MinimumVersion, y.MinimumVersion);
                case BrowserVersionSortColumn.DownloadURL:
                    return this.Compare(x.DownloadUrl, y.DownloadUrl);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
