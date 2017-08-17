// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015


/// <summary>
/// The Model namespace.
/// </summary>
namespace RRD.FSG.RP.Model
{
    // Note: The 3 enums are in the same file as they must contain the same values for the identically named members.
    // This allows easy conversion between the enums for like members which is used in the Match methods of SearchDetail.

    /// <summary>
    /// Specifies what kind of basic comparison equal or not) of two objects.
    /// </summary>
    public enum BasicCompare
    {
        /// <summary>
        /// Compares values by equality.
        /// </summary>
        Equal = 0,

        /// <summary>
        /// Compares values by inequality.
        /// </summary>
        NotEqual = 1,
    }

    /// <summary>
    /// Specifies what kind of value comparison of two IComparable objects.
    /// </summary>
    public enum ValueCompare
    {
        /// <summary>
        /// Compares values by equality.
        /// </summary>
        Equal = 0,

        /// <summary>
        /// Compares values by inequality.
        /// </summary>
        NotEqual = 1,

        /// <summary>
        /// Compares values using greater than comparison.
        /// </summary>
        GreaterThan = 2,

        /// <summary>
        /// Compares values using less than comparison.
        /// </summary>
        LessThan = 3,

        /// <summary>
        /// Compares values using greater than or equal to comparison.
        /// </summary>
        GreaterThanOrEqual = 4,

        /// <summary>
        /// Compares values using less than or equal to comparison.
        /// </summary>
        LessThanOrEqual = 5
    }

    /// <summary>
    /// Specifies what kind of advanced comparison of two strings.
    /// </summary>
    public enum TextCompare
    {
        /// <summary>
        /// Compares values by equality.
        /// </summary>
        Equal = 0,

        /// <summary>
        /// Compares values by inequality.
        /// </summary>
        NotEqual = 1,

        /// <summary>
        /// Compares values using greater than comparison.
        /// </summary>
        GreaterThan = 2,

        /// <summary>
        /// Compares values using less than comparison.
        /// </summary>
        LessThan = 3,

        /// <summary>
        /// Compares values using greater than or equal to comparison.
        /// </summary>
        GreaterThanOrEqual = 4,

        /// <summary>
        /// Compares values using less than or equal to comparison.
        /// </summary>
        LessThanOrEqual = 5,

        /// <summary>
        /// Compares strings using starts with comparison.
        /// </summary>
        StartsWith = 6,

        /// <summary>
        /// Compares strings using not starts with comparison.
        /// </summary>
        NotStartsWith = 7,

        /// <summary>
        /// Compares string using ends with comparison.
        /// </summary>
        EndsWith = 8,

        /// <summary>
        /// Compares strings using not ends with comparison.
        /// </summary>
        NotEndsWith = 9,

        /// <summary>
        /// Compares strings using contains comparison.
        /// </summary>
        Contains = 10,

        /// <summary>
        /// Compares strings using not contains comparison.
        /// </summary>
        NotContains = 11
    }
}
