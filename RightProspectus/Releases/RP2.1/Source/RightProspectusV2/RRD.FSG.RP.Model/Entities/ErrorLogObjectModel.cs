// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-31-2015
// ***********************************************************************
using System;

/// <summary>
/// The Entities namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities
{
    /// <summary>
    /// Class ErrorLogObjectModel.
    /// </summary>
    public class ErrorLogObjectModel : BaseModel<int>, IComparable<ErrorLogObjectModel>
    {
        /// <summary>
        /// ErrorLogId
        /// </summary>
        /// <value>The error log identifier.</value>
        public int ErrorLogId { get; set; }

        /// <summary>
        /// ErrorCode
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; set; }

        /// <summary>
        /// ErrorUtcDate
        /// </summary>
        /// <value>The error UTC date.</value>
        public DateTime? ErrorUtcDate { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        /// <value>The priority.</value>
        public int  Priority { get; set; }

        /// <summary>
        /// Severity
        /// </summary>
        /// <value>The severity.</value>
        public string Severity { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// MachineName
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; set; }

        /// <summary>
        /// AppDomainName
        /// </summary>
        /// <value>The name of the application domain.</value>
        public string AppDomainName { get; set; }

        /// <summary>
        /// ProcessID
        /// </summary>
        /// <value>The process identifier.</value>
        public string ProcessID { get; set; }

        /// <summary>
        /// ProcessName
        /// </summary>
        /// <value>The name of the process.</value>
        public string ProcessName { get; set; }

        /// <summary>
        /// ThreadName
        /// </summary>
        /// <value>The name of the thread.</value>
        public string ThreadName { get; set; }

        /// <summary>
        /// Win32ThreadId
        /// </summary>
        /// <value>The win32 thread identifier.</value>
        public string Win32ThreadId { get; set; }

        /// <summary>
        /// EventId
        /// </summary>
        /// <value>The event identifier.</value>
        public int? EventId { get; set; }

        /// <summary>
        /// SiteActivityID
        /// </summary>
        /// <value>The site activity identifier.</value>
        public int? SiteActivityID { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// FormattedMessage
        /// </summary>
        /// <value>The formatted message.</value>
        public string FormattedMessage { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        /// <value>The URL.</value>
        public string URL { get; set; }

        /// <summary>
        /// Absolute URL
        /// </summary>
        /// <value>The absolute URL.</value>
        public string AbsoluteURL { get; set; }

        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(ErrorLogObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
