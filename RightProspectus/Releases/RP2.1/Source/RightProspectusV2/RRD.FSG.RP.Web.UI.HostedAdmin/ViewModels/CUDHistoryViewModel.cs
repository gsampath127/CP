
/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels
{
    /// <summary>
    /// Class CUDHistoryViewModel.
    /// </summary>
    public class CUDHistoryViewModel
    {
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
        /// CUDType
        /// </summary>
        /// <value>The type of the cud.</value>
        public string CUDType { get; set; }
        /// <summary>
        /// UtcCUDDate
        /// </summary>
        /// <value>The UTC cud date.</value>
        public string UtcCUDDate { get; set; }
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
        /// NewValue
        /// </summary>
        /// <value>The new image data URL.</value>
        public string NewImageDataURL { get; set; }
        /// <summary>
        /// OldImageDataURL
        /// </summary>
        /// <value>The old image data URL.</value>
        public string OldImageDataURL { get; set; }
        /// <summary>
        /// IsBinaryImage
        /// </summary>
        /// <value><c>true</c> if this instance is binary image; otherwise, <c>false</c>.</value>
        public bool IsBinaryImage { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

   }
}