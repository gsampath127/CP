// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015

using RRD.FSG.RP.Model.Keys;
using System;
using System.Collections.Generic;


/// <summary>
/// The Client namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Entities.Client
{
    /// <summary>
    /// Class CUDHistoryObjectModel.
    /// </summary>
    public class CUDHistoryObjectModel:BaseModel<CUDHistoryKey>,IComparable<CUDHistoryObjectModel>
    {

        /// <summary>
        /// Gets the primary key identifier of the entity.
        /// </summary>
        /// <value>The key.</value>
        public override CUDHistoryKey Key
        {
            get { return new CUDHistoryKey(this.CUDHistoryId, this.ColumnName); }
            internal set
            {

            }
        }
        /// <summary>
        /// CUDHistoryId
        /// </summary>
        /// <value>The cud history identifier.</value>
        public int CUDHistoryId { get; set; }
        /// <summary>
        /// TableName
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName { get; set; }
        /// <summary>
        /// Key
        /// </summary>
        /// <value>The cud key.</value>
        public int CUDKey { get; set; }
        /// <summary>
        /// SecondKey
        /// </summary>
        /// <value>The second key.</value>
        public string SecondKey { get; set; }
        /// <summary>
        /// ThirdKey
        /// </summary>
        /// <value>The third key.</value>
        public string ThirdKey { get; set; }
        /// <summary>
        /// CUDType
        /// </summary>
        /// <value>The type of the cud.</value>
        public string CUDType { get; set; }
        /// <summary>
        /// UtcCUDDate
        /// </summary>
        /// <value>The UTC cud date.</value>
        public DateTime UtcCUDDate { get; set; }
        /// <summary>
        /// BatchId
        /// </summary>
        /// <value>The batch identifier.</value>
        public Guid BatchId { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
        /// <summary>
        /// ColumnName
        /// </summary>
        /// <value>The name of the column.</value>
        public string ColumnName { get; set; }
        /// <summary>
        /// SqlDbType
        /// </summary>
        /// <value>The type of the SQL database.</value>
        public int SqlDbType { get; set; }
        /// <summary>
        /// OldValue
        /// </summary>
        /// <value>The old value.</value>
        public string OldValue { get; set; }
        /// <summary>
        /// NewValue
        /// </summary>
        /// <value>The new value.</value>
        public string NewValue { get; set; }
        /// <summary>
        /// NewImageDataURL
        /// </summary>
        /// <value>The new image data URL.</value>
        public string NewImageDataURL { get; set; }
        /// <summary>
        /// OldImageDataURL
        /// </summary>
        /// <value>The old image data URL.</value>
        public string OldImageDataURL { get; set; }
        /// <summary>
        /// IsBinaryIamge
        /// </summary>
        /// <value><c>true</c> if this instance is binary image; otherwise, <c>false</c>.</value>
        public bool IsBinaryImage { get; set; }
        /// <summary>
        /// IsAdmin
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// Display Value pair for CUDType
        /// </summary>
        /// <value>The type of the dictionary cud.</value>
        public Dictionary<string,string> dictCUDType { get; set; }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
        public int CompareTo(CUDHistoryObjectModel other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}
