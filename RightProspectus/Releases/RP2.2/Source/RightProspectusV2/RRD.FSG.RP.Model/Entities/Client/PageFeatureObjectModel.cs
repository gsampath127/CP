// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Keys;
using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class PageFeatureObjectModel.
    /// </summary>
    public class PageFeatureObjectModel : AuditedBaseModel<PageFeatureKey>, IComparable<PageFeatureObjectModel>
    {
        #region Entity Properties



        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override PageFeatureKey Key
        {
            get { return new PageFeatureKey(this.SiteId,this.PageId, this.PageKey); }
            internal set
            {
                if (value.PageId != this.PageId)
                {
                    throw new ArgumentOutOfRangeException("Key",
                        string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}",
                        value.PageId, this.PageId));
                }


                this.SiteId = value.SiteId;
                this.PageId = value.PageId;
                this.PageKey = value.PageKey;
               
            }
        }
        /// <summary>
        /// Gets or sets the Site identifier.
        /// </summary>
        /// <value>The SiteId identifier.</value>
        public int SiteId { get; set; }
        /// <summary>
        /// Gets or sets the Page identifier.
        /// </summary>
        /// <value>The PageId identifier.</value>
        public int PageId { get; set; }
        /// <summary>
        /// Gets or sets the  Key.
        /// </summary>
        /// <value>The Key.</value>
        public string PageKey { get; set; }
        /// <summary>
        /// Page Name
        /// </summary>
        /// <value>The name of the page.</value>
        public string PageName { get; set; }
        /// <summary>
        /// PageDescription
        /// </summary>
        /// <value>The page description.</value>
        public string PageDescription { get; set; }
        /// <summary>
        /// Gets or sets the  Feature Mode.
        /// </summary>
        /// <value>The FeatureMode .</value>
        public int FeatureMode { get; set; }
       

        #endregion
        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int CompareTo(PageFeatureObjectModel other)
        {
            throw new NotImplementedException();
        }
    }
}
