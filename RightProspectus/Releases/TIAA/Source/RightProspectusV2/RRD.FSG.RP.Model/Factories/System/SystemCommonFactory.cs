// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015

using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces.System;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections.Generic;
using System.Data;

namespace RRD.FSG.RP.Model.Factories.System
{
    /// <summary>
    /// Class SystemCommonFactory.
    /// </summary>
    public class SystemCommonFactory : HostedPageBaseFactory, ISystemCommonFactory
    {
        /// <summary>
        /// The sp get clients data
        /// </summary>
        private const string SPGetClientsData = "RPV2HostedSites_GetClientsData";

        /// <summary>
        /// The sp get all template pages
        /// </summary>
        private const string SPGetAllTemplatePages = "RPV2HostedSites_GetAllTemplatePages";

        /// <summary>
        /// The static clients data_ cache dependency check
        /// </summary>
        private const string StaticClientsData_CacheDependencyCheck = "RPV2HostedSites_StaticClientsData_CacheDependencyCheck";

        /// <summary>
        /// The template pages_ cache dependency check
        /// </summary>
        private const string TemplatePages_CacheDependencyCheck = "RPV2HostedSites_TemplatePages_CacheDependencyCheck";


        /// <summary>
        /// Initializes a new instance of the <see cref="SystemCommonFactory"/> class.
        /// </summary>
        /// <param name="paramDataAccess">The parameter data access.</param>
        public SystemCommonFactory(IDataAccess paramDataAccess)
            : base(paramDataAccess)
        {
            }

        # region "private methods"

        /// <summary>
        /// Gets the clients data.
        /// </summary>
        /// <returns>ClientDataFromSystem.</returns>
        private ClientDataFromSystem GetClientsData()
        {
            ClientDataFromSystem clientDataFromSystem = new ClientDataFromSystem();

            List<ClientDbConnection> clientDbConnections = new List<ClientDbConnection>();

            List<HostedTemplatePage> templatePages = new List<HostedTemplatePage>();

            List<HostedTemplateNavigation> hostedTemplateNavigation = new List<HostedTemplateNavigation>();

            List<HostedTemplatePageNavigation> hostedTemplatePageNavigation = new List<HostedTemplatePageNavigation>();

            DataSet results = DataAccess.ExecuteDataSet(DBConnectionString.SystemDBConnectionString(),
                         SPGetClientsData
                         );


            foreach (DataRow datarow in results.Tables[0].Rows)
            {
                clientDbConnections.Add(new ClientDbConnection()
                {
                    ClientConnectionStringName = datarow["ClientConnectionStringName"].ToString(),
                    ClientDatabaseName = datarow["ClientDatabaseName"].ToString(),
                    ClientDNS = datarow["ClientDNS"].ToString(),
                    ClientID = Convert.ToInt32(datarow["ClientID"]),
                    ClientName = datarow["ClientName"].ToString(),
                    VerticalMarketConnectionStringName = datarow["VerticalMarketConnectionStringName"].ToString(),
                    VerticalMarketsDatabaseName = datarow["VerticalMarketsDatabaseName"].ToString()

                });
            }


            foreach (DataRow datarow in results.Tables[1].Rows)
            {
                templatePages.Add(new HostedTemplatePage()
                {
                    TemplateID = Convert.ToInt32(datarow["TemplateID"]),
                    TemplateName = datarow["TemplateName"].ToString(),
                    PageID = Convert.ToInt32(datarow["PageID"]),
                    PageName = datarow["PageName"].ToString()
                });
            }

            foreach (DataRow datarow in results.Tables[2].Rows)
            {
                hostedTemplateNavigation.Add(new HostedTemplateNavigation()
                {
                    TemplateID = Convert.ToInt32(datarow["TemplateId"]),
                    NavigationKey = datarow["NavigationKey"].ToString(),
                    XslTransform = datarow["XslTransform"].ToString(),
                    DefaultNavigationXml = datarow["DefaultNavigationXml"].ToString()
                });
            }

            foreach (DataRow datarow in results.Tables[3].Rows)
            {
                hostedTemplatePageNavigation.Add(new HostedTemplatePageNavigation()
                {
                    TemplateID = Convert.ToInt32(datarow["TemplateId"]),
                    PageID = Convert.ToInt32(datarow["PageId"]),
                    NavigationKey = datarow["NavigationKey"].ToString(),
                    XslTransform = datarow["XslTransform"].ToString(),
                    DefaultNavigationXml = datarow["DefaultNavigationXml"].ToString()
                });
            }

            clientDataFromSystem.ClientDbConnections = clientDbConnections;

            clientDataFromSystem.TemplatePages = templatePages;

            clientDataFromSystem.HostedTemplateNavigations = hostedTemplateNavigation;

            clientDataFromSystem.HostedTemplatePageNavigations = hostedTemplatePageNavigation;

            return clientDataFromSystem;
        }




        # endregion

        /// <summary>
        /// Gets the clients data from cache.
        /// </summary>
        /// <returns>ClientDataFromSystem.</returns>
        public ClientDataFromSystem GetClientsDataFromCache()
        {
            bool isReturnedFromCache = false;
            ClientDataFromSystem clientDataFromSystem = DependencyHelper.GetCachedDataWithSqlDependency(SystemCacheItems.ClientData
                            , DBConnectionString.SystemDBConnectionString(),
                            StaticClientsData_CacheDependencyCheck,
                            ConfigValues.SystemDBCacheTimeOut, () => GetClientsData(), out isReturnedFromCache);

            return clientDataFromSystem;

        }

        /// <summary>
        /// Gets the database connections.
        /// </summary>
        /// <returns>List&lt;ClientDbConnection&gt;.</returns>
        public List<ClientDbConnection> GetDBConnections()
        {
            return GetClientsDataFromCache().ClientDbConnections;
        }
    }
}
