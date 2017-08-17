// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************


/// <summary>
/// The Model namespace.
/// </summary>
using System.ComponentModel;
namespace RRD.FSG.RP.Model
{
    /// <summary>
    /// Enum FrequencyType
    /// </summary>
    public enum FrequencyType
    {
        /// <summary>
        /// Run the job once
        /// </summary>
        RunOnce = 1,

        /// <summary>
        /// Run the job every x days
        /// </summary>
        Daily = 2,

        /// <summary>
        /// Run the job weekly
        /// </summary>
        Weekly = 3,

        /// <summary>
        /// Run the job monthly
        /// </summary>
        Monthly = 4,

        /// <summary>
        /// Run the job quarterly
        /// </summary>
        Quarterly = 5,

        /// <summary>
        /// Run the job twise in a year
        /// </summary>
        [Description("Semi-Annually")]
        BiAnnually = 6,

        /// <summary>
        /// Run the job every year
        /// </summary>
        Annually = 7,

        /// <summary>
        /// Run the job every hpur
        /// </summary>
        Hourly = 8
    }
}
