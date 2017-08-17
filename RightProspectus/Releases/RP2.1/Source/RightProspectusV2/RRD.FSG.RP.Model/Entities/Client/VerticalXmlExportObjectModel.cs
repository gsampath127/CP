// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
// ***********************************************************************
using System;

/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class VerticalXmlExportObjectModel.
    /// </summary>
    public class VerticalXmlExportObjectModel : BaseModel<int>, IComparable<VerticalXmlExportObjectModel>
    {
        /// <summary>
        /// VerticalXmlExportId
        /// </summary>
        /// <value>The vertical XML export identifier.</value>
        public int VerticalXmlExportId { get; set; }
        /// <summary>
        /// VerticalXMLImportExportTypes
        /// </summary>
        /// <value>The export types.</value>
        public int ExportTypes { get; set; }
        /// <summary>
        /// ExportXml
        /// </summary>
        /// <value>The export XML.</value>
        public string ExportXml { get; set; }
        /// <summary>
        /// ExportDate
        /// </summary>
        /// <value>The export date.</value>
        public DateTime ExportDate { get; set; }
        /// <summary>
        /// ExportedBy
        /// </summary>
        /// <value>The exported by.</value>
        public int ExportedBy { get; set; }
        /// <summary>
        /// ExportedByName
        /// </summary>
        /// <value>The name of the exported by.</value>
        public string ExportedByName { get; set; }
        /// <summary>
        /// ExportDescription
        /// </summary>
        /// <value>The export description.</value>
        public string ExportDescription { get; set; }
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
        public int CompareTo(VerticalXmlExportObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
