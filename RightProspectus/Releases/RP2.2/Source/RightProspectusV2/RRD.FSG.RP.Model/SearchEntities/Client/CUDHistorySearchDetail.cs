// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System;

namespace RRD.FSG.RP.Model.SearchEntities.Client
{
    /// <summary>
    /// Class CUDHistorySearchDetail.
    /// </summary>
    public class CUDHistorySearchDetail
        :SearchDetail<CUDHistoryObjectModel>,ISearchDetailCopyAs<CUDHistorySearchDetail>
        {
        #region Public Properties
            /// <summary>
            /// TableName
            /// </summary>
            /// <value>The name of the table.</value>
        public string TableName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TableName property.
        /// </summary>
        /// <value>The table name compare.</value>
        public ValueCompare TableNameCompare { get; set; }
        /// <summary>
        /// CUDType
        /// </summary>
        /// <value>The type of the cud.</value>
        public string CUDType { get; set; }
        /// <summary>
        /// Determines the type of comparison for the CUDType property.
        /// </summary>
        /// <value>The cud type compare.</value>
        public ValueCompare CUDTypeCompare { get; set; }
        /// <summary>
        /// UtcCUDDateFrom
        /// </summary>
        /// <value>The UTC cud date from.</value>
        public DateTime? UtcCUDDateFrom { get; set; }
        /// <summary>
        /// Determines the type of comparison for the UtcCUDDateFrom property.
        /// </summary>
        /// <value>The UTC cud date from compare.</value>
        public ValueCompare UtcCUDDateFromCompare { get; set; }
        /// <summary>
        /// UtcCUDDateTo
        /// </summary>
        /// <value>The UTC cud date to.</value>
        public DateTime? UtcCUDDateTo { get; set; }
        /// <summary>
        /// Determines the type of comparison for the UtcCUDDateTo property.
        /// </summary>
        /// <value>The UTC cud date to compare.</value>
        public ValueCompare UtcCUDDateToCompare { get; set; }
        /// <summary>
        /// CUDHistoryId
        /// </summary>
        /// <value>The cud history identifier.</value>
        public int? CUDHistoryId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the UtcCUDDate property.
        /// </summary>
        /// <value>The cud history identifier compare.</value>
        public ValueCompare CUDHistoryIdCompare { get; set; }
        /// <summary>
        /// To Find the master table
        /// </summary>
        /// <value><c>true</c> if this instance is history data; otherwise, <c>false</c>.</value>
        public bool IsHistoryData { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        /// <value>The user identifier.</value>
        public int? UserId { get; set; }
        /// <summary>
        /// Is Admin
        /// </summary>
        /// <value><c>true</c> if this instance is admin; otherwise, <c>false</c>.</value>
        public bool IsAdmin { get; set; }



        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<CUDHistoryObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.TableName, entity.TableName, this.TableNameCompare)
                    && this.Match(this.CUDType, entity.CUDType, this.CUDTypeCompare)
                    && this.Match(this.CUDHistoryId, entity.CUDHistoryId, this.CUDHistoryIdCompare);
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Creates a new search detail and copies the parameters from this one to it.
        /// </summary>
        /// <typeparam name="TCopy">Type of search detail to create.</typeparam>
        /// <returns>A new search detail with the same search parameters as this one.</returns>
       TCopy ISearchDetailCopyAs<CUDHistorySearchDetail>.CopyAs<TCopy>()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.CUDType = this.CUDType;
            copy.TableName = this.TableName;
            copy.CUDHistoryId = this.CUDHistoryId;
            return copy;
        }
        #endregion
        }
}
