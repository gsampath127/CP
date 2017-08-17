// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 11-09-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************
using System;

/// <summary>
/// The Extensions namespace.
/// </summary>
namespace RP.Extensions
{
    #region SkipRPActionFilterAttribute
    /// <summary>
    /// This attribute should be decorated for those methods for which RPActionFilterAttribute should not be fired
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public sealed class SkipRPActionFilterAttribute : Attribute
    {
        //Intentionally left blank
    }
    #endregion


    #region IsPopUpAttribute
    /// <summary>
    /// The attribute to be decorated on Pop Up Window (new page) Get method to handle session timeout redirect
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public sealed class IsPopUpAttribute : Attribute
    {
        //Intentionally left blank
    }
    #endregion


}