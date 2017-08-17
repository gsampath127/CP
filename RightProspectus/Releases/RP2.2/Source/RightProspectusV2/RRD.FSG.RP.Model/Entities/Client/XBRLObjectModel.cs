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
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class XBRLObjectModel.
    /// </summary>
    public class XBRLObjectModel : BaseModel<int>, IComparable<XBRLObjectModel>
    {
        /// <summary>
        /// TaxonomyId
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int TaxonomyId { get; set; }
        /// <summary>
        /// Accession Number
        /// </summary>
        /// <value>The accession number.</value>
        public string AccessionNumber { get; set; }
        /// <summary>
        /// ZipFileName
        /// </summary>
        /// <value>The name of the zip file.</value>
        public string ZipFileName { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }
        /// <summary>
        /// CompanyName
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }
        /// <summary>
        /// TaxonomyName
        /// </summary>
        /// <value>The name of the taxonomy.</value>
        public string TaxonomyName { get; set; }
        /// <summary>
        /// DocumentDate
        /// </summary>
        /// <value>The document date.</value>
        public DateTime? DocumentDate { get; set; }
        /// <summary>
        /// FilingDate
        /// </summary>
        /// <value>The filing date.</value>
        public DateTime FilingDate { get; set; }
        /// <summary>
        /// OrderDate
        /// </summary>
        /// <value>The order date.</value>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// FormType
        /// </summary>
        /// <value>The type of the form.</value>
        public string FormType { get; set; }
        /// <summary>
        /// CreatedDate
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// DocumentType
        /// </summary>
        /// <value>The type of the document.</value>
        public string DocumentType { get; set; }
        /// <summary>
        /// IsViewerEnabled
        /// </summary>
        /// <value><c>true</c> if this instance is viewer enabled; otherwise, <c>false</c>.</value>
        public bool IsViewerEnabled { get; set; }
        /// <summary>
        /// IsViewerReadyForXBRL
        /// </summary>
        /// <value><c>true</c> if this instance is viewer ready for XBRL; otherwise, <c>false</c>.</value>
        public bool IsViewerReadyForXBRL { get; set; }
        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(XBRLObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

    }
}
