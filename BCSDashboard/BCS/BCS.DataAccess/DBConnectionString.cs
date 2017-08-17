using System.Configuration;

namespace BCS.Core.DAL
{
    public static class DBConnectionString
    {
        #region Variable(s)

        const string DB1029ConnectionStringKey = "DB1029Connection";

        const string ReadOnlyDB1029ConnectionStringKey = "ReadOnlyDB1029Connection";

        const string hostedAdminConnectionStringKey = "HostedAdminConnection";

        const string WorkflowDBConnectionStringKey = "WorkflowDBConnection";

        const string SystemDB = "SystemDB";
        
        #endregion

        #region Method(s)

        public static string DB1029ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[DB1029ConnectionStringKey].ConnectionString;
        }

        public static string ReadOnlyDB1029Connection()
        {
            return ConfigurationManager.ConnectionStrings[ReadOnlyDB1029ConnectionStringKey].ConnectionString;
        }

        public static string HostedAdminConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[hostedAdminConnectionStringKey].ConnectionString;
        }

        public static string WorkflowDBConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[WorkflowDBConnectionStringKey].ConnectionString;
        } 

        public static string RPV2SystemDBConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[SystemDB].ConnectionString;
        } 
        

        #endregion
    }
}
