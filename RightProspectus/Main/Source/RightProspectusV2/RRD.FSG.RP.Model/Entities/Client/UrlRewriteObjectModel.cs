// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class UrlRewriteObjectModel.
    /// </summary>
    public class UrlRewriteObjectModel : AuditedBaseModel<int>, IComparable<UrlRewriteObjectModel>
    {
        #region Entity Properties
        /// <summary>
        /// Gets or sets the UrlRewrite identifier.
        /// </summary>
        /// <value>The UrlRewriteId identifier.</value>
        public int UrlRewriteId { get; set; }
        /// <summary>
        /// Gets or sets the MatchPattern.
        /// </summary>
        /// <value>The MatchPattern.</value>
        public string MatchPattern { get; set; }
        /// <summary>
        /// Gets or sets the RewriteFormat.
        /// </summary>
        /// <value>The RewriteFormat.</value>
        public string RewriteFormat { get; set; }
        /// <summary>
        /// Gets or sets the PatternName.
        /// </summary>
        /// <value>The PatternName.</value>
        public string PatternName { get; set; }
        #endregion
        /// <summary>
        /// Compares the two Site entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(UrlRewriteObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

    }
}
