// ***********************************************************************
// Assembly         : RPV2.Utilities
// Author           : 
// Created          : 08-31-2015
//
// Last Modified By : 
// Last Modified On : 08-31-2015

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The Utilities namespace.
/// </summary>
namespace RRD.FSG.RP.Utilities
{
    #region RPV2Resolver
    /// <summary>
    /// The Resolver class will use a IoC container and resolve dependencies at run time
    /// </summary>
    public static class RPV2Resolver
    {
        
        #region Properties
        /// <summary>
        /// The IoC Container property
        /// </summary>
        private static IUnityContainer container;
        #endregion


        #region LoadConfiguration
        /// <summary>
        /// Loads the IoC configuration from the config file with default section and container name
        /// </summary>
        public static void LoadConfiguration()
        {
            container = new UnityContainer().LoadConfiguration();
        }

        /// <summary>
        /// Loads IoC configuration from config file with specified container name
        /// </summary>
        /// <param name="containerName">Specify the container name</param>
        public static void LoadConfiguration(string containerName)
        {
            container = new UnityContainer().LoadConfiguration(containerName);
        }


        /// <summary>
        /// Loads IoC configuration from config file with specified section name and container name
        /// </summary>
        /// <param name="sectionName">Specify the section name</param>
        /// <param name="containerName">Specify the container name</param>
        public static void LoadConfiguration(string sectionName, string containerName)
        {
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection(sectionName);
            container = new UnityContainer().LoadConfiguration(section, containerName);
        }
        #endregion


        #region Resolve
        /// <summary>
        /// Resolves and returns the concrete implementation of the type
        /// </summary>
        /// <typeparam name="T">Specify the abstract implementation</typeparam>
        /// <returns>T.</returns>
        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        /// <summary>
        /// Resolves by registration name and returns the concrete implementation of the type
        /// </summary>
        /// <typeparam name="T">A generic type</typeparam>
        /// <param name="registrationName">Name of the registration.</param>
        /// <returns>T.</returns>
        public static T Resolve<T>(string registrationName)
        {
            return container.Resolve<T>(registrationName);
        }
        #endregion

    }
    #endregion
}
