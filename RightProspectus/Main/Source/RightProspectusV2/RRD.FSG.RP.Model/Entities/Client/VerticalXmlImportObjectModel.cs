// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************

using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class VerticalXmlImportObjectModel.
    /// </summary>
    public class VerticalXmlImportObjectModel : BaseModel<int>, IComparable<VerticalXmlImportObjectModel>
    {
        /// <summary>
        /// VerticalXmlImportId
        /// </summary>
        /// <value>The vertical XML import identifier.</value>
        public int VerticalXmlImportId { get; set; }
        /// <summary>
        /// VerticalXMLImportImportTypes
        /// </summary>
        /// <value>The import types.</value>
        public int ImportTypes { get; set; }
        /// <summary>
        /// ImportXml
        /// </summary>
        /// <value>The import XML.</value>
        public string ImportXml { get; set; }
        /// <summary>
        /// ImportDate
        /// </summary>
        /// <value>The import date.</value>
        public DateTime ImportDate { get; set; }
        /// <summary>
        /// ImportedBy
        /// </summary>
        /// <value>The imported by.</value>
        public int ImportedBy { get; set; }
        /// <summary>
        /// ImportDescription
        /// </summary>
        /// <value>The import description.</value>
        public string ImportDescription { get; set; }
        /// <summary>
        /// ImportedByName
        /// </summary>
        /// <value>The name of the imported by.</value>
        public string ImportedByName { get; set; }
        /// <summary>
        /// ExportBackupId
        /// </summary>
        /// <value>The export backup identifier.</value>
        public int? ExportBackupId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }
        /// <summary>
        /// Compares the two SiteText entities by their integer identifiers.
        /// </summary>
        /// <param name="other">The entity to compare this instance to.</param>
        /// <returns>-1 if the entity has a smaller id, 0 if they are the same, and 1 if the entity has a larger id.</returns>
        public int CompareTo(VerticalXmlImportObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
