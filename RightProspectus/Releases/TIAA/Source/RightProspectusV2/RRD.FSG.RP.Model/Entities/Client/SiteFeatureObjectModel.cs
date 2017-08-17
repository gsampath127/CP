// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Keys;
using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class SiteFeatureObjectModel.
    /// </summary>
    public class SiteFeatureObjectModel : AuditedBaseModel<SiteFeatureKey>, IComparable<SiteFeatureObjectModel>
    {
        #region Entity Properties



        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Key</exception>
        public override SiteFeatureKey Key
        {
            get { return new SiteFeatureKey(this.SiteId, this.SiteKey); }
            internal set
            {
                if (value.SiteId != this.SiteId)
                {
                    throw new ArgumentOutOfRangeException("Key",
                        string.Format("The first part of the composite key (hierarchical level) must be the correct value for this entity type. Value supplied was {0}. Value expected is {1}",
                        value.SiteId, this.SiteId));
                }

              
                this.SiteId = value.SiteId;
                this.SiteKey = value.SiteKey;
            }
        }
        /// <summary>
        /// Gets or sets the  identifier.
        /// </summary>
        /// <value>The SiteId identifier.</value>
        public int SiteId {get; set;}
        /// <summary>
        /// Gets or sets the  Key.
        /// </summary>
        /// <value>The Key.</value>
        public string SiteKey {get; set;}
        /// <summary>
        /// Gets or sets the  Feature Mode.
        /// </summary>
        /// <value>The FeatureMode .</value>
        public int FeatureMode {get; set;}
       

        #endregion
        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int CompareTo(SiteFeatureObjectModel other)
        {
            throw new NotImplementedException();
        }
    }
}
