// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-29-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************
using System;

namespace RRD.FSG.RP.Model.Keys
{
    /// <summary>
    /// Class CUDHistoryKey.
    /// </summary>
   public class CUDHistoryKey:IEquatable<CUDHistoryKey>,IComparable<CUDHistoryKey>,IComparable
   {
       /// <summary>
       /// Initializes a new instance of the <see cref="T:CUDHistoryKey" /> class.
       /// </summary>
       /// <param name="CUDHistoryId">CUDHistory.</param>
       /// <param name="ColumnName">ColumnName.</param>
       public CUDHistoryKey(int CUDHistoryId, string ColumnName)
       {
           this.CUDHistoryId = CUDHistoryId;
           this.ColumnName = ColumnName;
       }
       #region Public Properties

       /// <summary>
       /// Gets the CUDHistoryId
       /// </summary>
       /// <value>The cud history identifier.</value>
       public int CUDHistoryId { get; internal set; }
       /// <summary>
       /// Gets the Column Name
       /// </summary>
       /// <value>The name of the column.</value>
       public string ColumnName { get; internal set; }
       
       #endregion

       /// <summary>
       /// Indicates whether the current object is equal to another object of the same type.
       /// </summary>
       /// <param name="other">An object to compare with this object.</param>
       /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
       public bool Equals(CUDHistoryKey other)
       {
           return this.CUDHistoryId == other.CUDHistoryId
               && this.ColumnName == other.ColumnName;
       }

       /// <summary>
       /// Compares the current object with another object of the same type.
       /// </summary>
       /// <param name="other">An object to compare with this object.</param>
       /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.</returns>
       /// <exception cref="System.NotImplementedException"></exception>
       public int CompareTo(CUDHistoryKey other)
       {
           throw new NotImplementedException();
       }

       /// <summary>
       /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
       /// </summary>
       /// <param name="obj">An object to compare with this instance.</param>
       /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.</returns>
       /// <exception cref="System.NotImplementedException"></exception>
       public int CompareTo(object obj)
       {
           throw new NotImplementedException();
       }
   }
}
