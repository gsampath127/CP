// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************

using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class ReportContentObjectModel.
    /// </summary>
    public class ReportContentObjectModel : AuditedBaseModel<int>, IComparable<ReportContentObjectModel>
    {
        /// <summary>
        /// ReportContentId
        /// </summary>
        /// <value>The report content identifier.</value>
        public int ReportContentId { get; set; }
        /// <summary>
        /// ReportScheduleId
        /// </summary>
        /// <value>The report schedule identifier.</value>
        public int ReportScheduleId { get; set; }
        /// <summary>
        /// FilePath
        /// </summary>
        /// <value>The Dir of the file.</value>
        public string FileDir { get; set; }
        /// <summary>
        /// FileName
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }
        /// <summary>
        /// MimeType
        /// </summary>
        /// <value>The type of the MIME.</value>
        public string MimeType { get; set; }
        /// <summary>
        /// IsPrivate
        /// </summary>
        /// <value>The is private.</value>
        public int IsPrivate { get; set; }
        /// <summary>
        /// ContentUri
        /// </summary>
        /// <value>The content URI.</value>
        public string ContentUri { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// ReportRunDate
        /// </summary>
        /// <value>The report run date.</value>
        public DateTime ReportRunDate { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        /// <value>The data.</value>
        public byte[] Data { get; set; }


        /// <summary>
        /// ReportFromDate
        /// </summary>
        /// <value>The report from date.</value>
        public DateTime ReportFromDate { get; set; }
        /// <summary>
        /// ReportToDate
        /// </summary>
        /// <value>The report to date.</value>
        public DateTime ReportToDate { get; set; }
        /// <summary>
        /// EmailBody
        /// </summary>
        /// <value>The email body.</value>
        public string EmailBody { get; set; }
        /// <summary>
        /// EmailSubject
        /// </summary>
        /// <value>The email subject.</value>
        public string EmailSubject { get; set; }
        /// <summary>
        /// isAttachedZipFile
        /// </summary>
        /// <value><c>true</c> if this instance is attached zip file; otherwise, <c>false</c>.</value>
        public bool IsAttachedZipFile { get; set; }
        /// <summary>
        /// FrequencyType
        /// </summary>
        /// <value>The FrequencyType description.</value>
        public string FrequencyType { get; set; }
        /// <summary>
        /// IsSFTP
        /// </summary>
        public bool IsSFTP { get; set; }
        /// <summary>
        /// IsSFTP
        /// </summary>
        public bool DoNotSendReport { get; set; }
        /// <summary>
        /// Compares the two Report Content entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(ReportContentObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
