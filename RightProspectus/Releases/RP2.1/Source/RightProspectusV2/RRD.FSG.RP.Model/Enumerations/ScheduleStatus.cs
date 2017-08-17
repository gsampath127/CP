// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 11-12-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************

/// <summary>
/// The Enumerations namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Enumerations
{
    /// <summary>
    /// Enum ScheduleStatus
    /// </summary>
    public enum ScheduleStatus
    {
        /// <summary>
        /// Entered status (or never ran)
        /// </summary>
        Entered = 0,

        /// <summary>
        /// Task Running status, or in progress
        /// </summary>
        Running = 1,

        /// <summary>
        /// Task success status
        /// </summary>
        Success = 2,

        /// <summary>
        /// Task failure status
        /// </summary>
        Failure = 3
    }
}
