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
    /// Class StaticResourceObjectModel.
    /// </summary>
    public class StaticResourceObjectModel : AuditedBaseModel<int>, IComparable<StaticResourceObjectModel>
    {
        #region Entity Properties
        /// <summary>
        /// Gets or sets the StaticResource identifier.
        /// </summary>
        /// <value>The StaticResourceId identifier.</value>
        public int  StaticResourceId { get; set; }

        /// <summary>
        /// Gets or sets the FileName identifier.
        /// </summary>
        /// <value>The FileName.</value>
         
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the Size identifier.
        /// </summary>
        /// <value>The Size.</value>
        public int Size { get; set; }
        /// <summary>
        /// Gets or sets the MimeType identifier.
        /// </summary>
        /// <value>The MimeType.</value>
        public string MimeType { get; set; }
        /// <summary>
        /// Gets or sets the Data identifier.
        /// </summary>
        /// <value>The Data.</value>
        public Byte[] Data { get; set; }
        #endregion

        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(StaticResourceObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }

}
