using BCS.Core.DAL;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;

namespace BCS.ObjectModel.Factories
{
    public class Logging
    {
        private const string DBCExceptionMessage = "ExceptionMessage";
        private const string DBCApplicationName = "ApplicationName";
        private const string DBCExceptionID = "ExceptionID";
        private readonly string DB1029ConnectionString;
        private readonly IDataAccess dataAccess;
        private const string SPSaveExceptionLog = "BCS_SaveExceptionLog";

        public Logging()
        {
            this.DB1029ConnectionString = DBConnectionString.DB1029ConnectionString();
            this.dataAccess = new DataAccess();
        }

        public static void LogToFile(string logMessage)
        {
            using (StreamWriter tws = new StreamWriter(ConfigValues.LogDirectory + ConfigValues.LogFileName, true))
            {
                tws.WriteLine(DateTime.Now.ToLongDateString() + " " + logMessage);
                tws.Close();
            }
        }

        public int SaveExceptionLog(string exceptionMessage, BCSApplicationName applicationName, bool sendExceptionEmail)
        {
            int ExceptionID = -1;

            DbParameterCollection parametercollection = this.dataAccess.ExecuteNonQueryReturnOutputParams
                 (
                     this.DB1029ConnectionString,
                     SPSaveExceptionLog,

                      this.dataAccess.CreateParameter(DBCExceptionMessage, SqlDbType.VarChar, exceptionMessage),
                      this.dataAccess.CreateParameter(DBCApplicationName, SqlDbType.VarChar, applicationName),
                      this.dataAccess.CreateParameter(DBCExceptionID, DbType.Int32, ExceptionID, ParameterDirection.Output)
                 );


            if (parametercollection != null) ExceptionID = Convert.ToInt32(parametercollection["ExceptionID"].Value);


            //Send email if sendExceptionEmail = true
            if (sendExceptionEmail)
            {
                StringBuilder MailBody = new StringBuilder();
                MailBody.Append("<html><body><p>");
                MailBody.Append("An exception has occurred in " + applicationName.ToString() + ", ExceptionID = " + ExceptionID + ". Please contact your Tech Team with ExceptionID.");
                MailBody.Append("</p></body></html>");

                foreach (string emailTo in ConfigValues.BCSExceptionEmailListTo.Split(';'))
                {
                    UtilityFactory.SendEmail(ConfigValues.EmailFrom, emailTo, null, null, ConfigValues.BCSExceptionEmailSub, MailBody.ToString(), "support", null);
                }
            }
            return ExceptionID;
        }
    }
}
