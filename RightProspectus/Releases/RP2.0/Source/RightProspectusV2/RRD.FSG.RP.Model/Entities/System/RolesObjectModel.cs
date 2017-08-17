// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using System;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Class RolesObjectModel.
    /// </summary>
    public class RolesObjectModel : AuditedBaseModel<int>, IComparable<RolesObjectModel>
    {
        /// <summary>
        /// RolesId
        /// </summary>
        /// <value>The role identifier.</value>
        public int RoleId { get; set; }

        /// <summary>
        /// RolesName
        /// </summary>
        /// <value>The name of the role.</value>
        public string RoleName { get; set; }

        /// <summary>
        /// Compares the two Roles entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(RolesObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

    }
}
