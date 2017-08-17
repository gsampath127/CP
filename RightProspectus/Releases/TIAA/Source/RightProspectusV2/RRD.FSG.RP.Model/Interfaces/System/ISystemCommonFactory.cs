// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.System;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.Interfaces.System
{
    /// <summary>
    /// Interface ISystemCommonFactory
    /// </summary>
   public interface ISystemCommonFactory
    {

        /// <summary>
        /// Gets the clients data from cache.
        /// </summary>
        /// <returns>ClientDataFromSystem.</returns>
        ClientDataFromSystem GetClientsDataFromCache();
    }
}
