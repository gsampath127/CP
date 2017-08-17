// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
// ***********************************************************************

using System;

/// <summary>
/// The Enumerations namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Enumerations
{
    /// <summary>
    /// Class FeatureEnums.
    /// </summary>
    public class FeatureEnums
    {
        /// <summary>
        /// Enum XBRL
        /// </summary>
        [Flags]
        public enum XBRL
        {

            /// <summary>
            /// The disabled
            /// </summary>
            Disabled = 1,
            /// <summary>
            /// The enabled
            /// </summary>
            Enabled = 2,
            /// <summary>
            /// The show XBRL in new tab
            /// </summary>
            ShowXBRLInNewTab = 4,
            /// <summary>
            /// The show XBRL in tabbed view
            /// </summary>
            ShowXBRLInTabbedView = 8,
            /// <summary>
            /// The show XBRL in landing page
            /// </summary>
            ShowXBRLInLandingPage = 16

        }

        /// <summary>
        /// Enum RequestMaterial
        /// </summary>
        [Flags]
        public enum RequestMaterial
        {

            /// <summary>
            /// The disabled
            /// </summary>
            Disabled = 1,
            /// <summary>
            /// The enabled
            /// </summary>
            Enabled = 2
        }

        /// <summary>
        /// Enum RequestMaterial
        /// </summary>
        [Flags]
        public enum SinglePdfView
        {

            /// <summary>
            /// The disabled
            /// </summary>
            Disabled = 1,
            /// <summary>
            /// The enabled
            /// </summary>
            Enabled = 2,
            /// <summary>
            /// The ShowClientLogoFrame
            /// </summary>
            ShowClientLogoFrame=4
           

        }
    }
}
