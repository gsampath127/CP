// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using System;
using System.Collections.Generic;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.System
{
    /// <summary>
    /// Added to Handle User
    /// </summary>
    public class UserObjectModel : AuditedBaseModel<int>, IComparable<UserObjectModel>
    {
        /// <summary>
        /// Gets or sets the identifier of User.
        /// </summary>
        /// <value>The email.</value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [email confirmed].
        /// </summary>
        /// <value><c>true</c> if [email confirmed]; otherwise, <c>false</c>.</value>
        public bool? EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>The password hash.</value>
        public string PasswordHash { get; set; }
        
        /// <summary>
        /// Gets or sets the security stamp.
        /// </summary>
        /// <value>The security stamp.</value>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// Gets or sets the PhoneNumber.
        /// </summary>
        /// <value>The PhoneNumber confirmed.</value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone number confirmed.
        /// </summary>
        /// <value>The phone number confirmed.</value>
        public bool? PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [two factor enabled].
        /// </summary>
        /// <value><c>true</c> if [two factor enabled]; otherwise, <c>false</c>.</value>
        public bool? TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets the lockout end date UTC.
        /// </summary>
        /// <value>The lockout end date UTC.</value>
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [lockout enabled].
        /// </summary>
        /// <value><c>true</c> if [lockout enabled]; otherwise, <c>false</c>.</value>
        public bool? LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the access failed count.
        /// </summary>
        /// <value>The access failed count.</value>
        public int? AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the UserClients.
        /// </summary>
        /// <value>The UserClients.</value>
        public List<int> Clients { get; set; }

        /// <summary>
        /// Gets or sets the UserRoles.
        /// </summary>
        /// <value>The UserRoles.</value>
        public List<RolesObjectModel> Roles { get; set; }

        /// <summary>
        /// Compares the two Site entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(UserObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }

}
