// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-13-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using System.Collections.Generic;
using System.Configuration;

namespace RRD.FSG.RP.Model.Factories
{
    /// <summary>
    /// Class DBConnectionString.
    /// </summary>
    public static class DBConnectionString
    {

        /// <summary>
        /// The system database connection string
        /// </summary>
        private static string systemDBConnectionString = string.Empty;

        /// <summary>
        /// Systems the database connection string.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string SystemDBConnectionString()
        {

            if (string.IsNullOrWhiteSpace(systemDBConnectionString))
            {
                systemDBConnectionString = ConfigurationManager.ConnectionStrings["SystemDB"].ConnectionString;

            }
            return systemDBConnectionString;
        }

        /// <summary>
        /// Hosted the connection string.
        /// </summary>
        /// <param name="clientID">The client identifier.</param>
        /// <param name="dataAccess">The data access.</param>
        /// <returns>System.String.</returns>
        public static string HostedConnectionString(int clientID, IDataAccess dataAccess)
        {
            List<ClientDbConnection> clientDataSystemModels = new SystemCommonFactory(dataAccess).GetDBConnections();

            //code required to get string from Cache or from the System
            ClientDbConnection clientDataSystemModel = clientDataSystemModels.Find(c => c.ClientID == clientID);

            return ConfigurationManager.ConnectionStrings[clientDataSystemModel.ClientConnectionStringName].ConnectionString.Replace("#DBName#", clientDataSystemModel.ClientDatabaseName);

        }

        /// <summary>
        /// Hosted the connection string.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="dataAccess">The data access.</param>
        /// <returns>System.String.</returns>
        public static string HostedConnectionString(string clientName, IDataAccess dataAccess)
        {
            List<ClientDbConnection> clientDataSystemModels = new SystemCommonFactory(dataAccess).GetDBConnections();

            //code required to get string from Cache or from the System
            ClientDbConnection clientDataSystemModel = clientDataSystemModels.Find(c => c.ClientName.ToLower() == clientName.ToLower());

            return ConfigurationManager.ConnectionStrings[clientDataSystemModel.ClientConnectionStringName].ConnectionString.Replace("#DBName#", clientDataSystemModel.ClientDatabaseName);
        }

        /// <summary>
        /// Verticals the database connection string.
        /// </summary>
        /// <param name="clientID">The client identifier.</param>
        /// <param name="dataAccess">The data access.</param>
        /// <returns>System.String.</returns>
        public static string VerticalDBConnectionString(int clientID, IDataAccess dataAccess)
        {
            List<ClientDbConnection> clientDataSystemModels = new SystemCommonFactory(dataAccess).GetDBConnections();

            //code required to get string from Cache or from the System
            ClientDbConnection clientDataSystemModel = clientDataSystemModels.Find(c => c.ClientID == clientID);

            return ConfigurationManager.ConnectionStrings[clientDataSystemModel.VerticalMarketConnectionStringName].ConnectionString.Replace("#DBName#", clientDataSystemModel.VerticalMarketsDatabaseName);
        }

        /// <summary>
        /// Verticals the database connection string.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="dataAccess">The data access.</param>
        /// <returns>System.String.</returns>
        public static string VerticalDBConnectionString(string clientName, IDataAccess dataAccess)
        {
            List<ClientDbConnection> clientDataSystemModels = new SystemCommonFactory(dataAccess).GetDBConnections();

            //code required to get string from Cache or from the System
            ClientDbConnection clientDataSystemModel = clientDataSystemModels.Find(c => c.ClientName.ToLower() == clientName.ToLower());

            return ConfigurationManager.ConnectionStrings[clientDataSystemModel.VerticalMarketConnectionStringName].ConnectionString.Replace("#DBName#", clientDataSystemModel.VerticalMarketsDatabaseName);
        }


    }
}
