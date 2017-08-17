using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using BCS.Core.DAL;
using BCS.ObjectModel;
using System.Configuration;

namespace BCS.ObjectModel.Factories
{
    public class BCSV2LoginFactory
    {

        #region Variable(s)

        private const string DBCEmail = "Email";

        private const string DBCPasswordHash = "PasswordHash";

        private const string DBCUserId = "UserId";

        private const string DBCToken = "Token";

        private const string DBCUtcIssuedOn = "UtcIssuedOn";

        private const string DBCUtcExpiresOn = "UtcExpiresOn";

        private const string DBCApplication = "ApplicationName";

        private const string DBCMaxFailedAttempts = "MaxFailedAttempts";

        private const string DBCAccountLockOutTime = "AccountLockOutTime";

        private const string SPGetUser = "BCSV2_GetUser";

        private const string SPAuthenticateUser = "BCSV2_AuthenticateUser";

        private const string SPInsertToken = "BCSV2_InsertToken";

        private const string SPUpdateToken = "BCSV2_UpdateToken";

        private const string SPGetToken = "BCSV2_GetToken";

        private const string SPDeleteToken = "BCSV2_DeleteToken";

        private readonly string RPV2SystemDBConnectionString;

        private readonly IDataAccess dataAccess;

        #endregion

        public BCSV2LoginFactory()
        {
            this.RPV2SystemDBConnectionString = DBConnectionString.RPV2SystemDBConnectionString();
            this.dataAccess = new DataAccess();
        }

        #region method(s)

        public Tuple<bool, string> AuthenticateUser(string Email, string Password)
        {
            bool valid = false;
            string message = string.Empty;
            IDataReader dataReader = this.dataAccess.ExecuteReader
                 (
                     this.RPV2SystemDBConnectionString,
                     SPAuthenticateUser,
                     this.dataAccess.CreateParameter(DBCEmail, SqlDbType.VarChar, Email),
                     this.dataAccess.CreateParameter(DBCPasswordHash, SqlDbType.VarChar, Password),
                     this.dataAccess.CreateParameter(DBCApplication, SqlDbType.VarChar, ConfigurationManager.AppSettings["Application"].ToString()),
                     this.dataAccess.CreateParameter(DBCMaxFailedAttempts, SqlDbType.Int, Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedAttempts"])),
                     this.dataAccess.CreateParameter(DBCAccountLockOutTime, SqlDbType.Int, Convert.ToInt32(ConfigurationManager.AppSettings["AccountLockOutTime"]))

                 );

            while (dataReader.Read())
            {
                valid = Convert.ToBoolean(dataReader["Valid"]);
                message = dataReader["Message"].ToString();
            }


            return new Tuple<bool, string>(valid, message);
        }

        public UserDetails GetUserDetails(string Email)
        {
            UserDetails userDetails = new UserDetails();
            IDataReader dataReader = this.dataAccess.ExecuteReader
                 (
                     this.RPV2SystemDBConnectionString,
                     SPGetUser,
                     this.dataAccess.CreateParameter(DBCEmail, SqlDbType.VarChar, Email),
                     this.dataAccess.CreateParameter(DBCApplication, SqlDbType.VarChar, ConfigurationManager.AppSettings["Application"].ToString())
                 );
            if (dataReader.Read())
            {
                userDetails.UserId = Convert.ToInt32(dataReader["UserId"]);
                userDetails.Email = Email;
                userDetails.UserName = dataReader["UserName"].ToString();
                userDetails.FullName = dataReader["FullName"].ToString();
                userDetails.ApplicationId = Convert.ToInt32(dataReader["ApplicationId"]);
                userDetails.Application = dataReader["Application"].ToString();
            }

            if (dataReader.NextResult())
            {
                userDetails.Roles = new List<Role>();
                while (dataReader.Read())
                {
                    userDetails.Roles.Add(new Role { Id = Convert.ToInt32(dataReader["RoleId"]), Name = dataReader["Name"].ToString() });
                }

            }
            return userDetails;
        }

        public void AddToken(TokenDetails token)
        {
            this.dataAccess.ExecuteNonQuery
                 (
                     this.RPV2SystemDBConnectionString,
                     SPInsertToken,
                     this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, token.UserId),
                     this.dataAccess.CreateParameter(DBCToken, SqlDbType.VarChar, token.Token),
                     this.dataAccess.CreateParameter(DBCUtcIssuedOn, SqlDbType.DateTime, token.UtcIssuedOn),
                     this.dataAccess.CreateParameter(DBCUtcExpiresOn, SqlDbType.DateTime, token.UtcExpiresOn)
                 );
        }

        public void UpdateToken(TokenDetails token)
        {
            this.dataAccess.ExecuteNonQuery
                 (
                     this.RPV2SystemDBConnectionString,
                     SPUpdateToken,
                     this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, token.UserId),
                     this.dataAccess.CreateParameter(DBCToken, SqlDbType.VarChar, token.Token),
                     this.dataAccess.CreateParameter(DBCUtcExpiresOn, SqlDbType.DateTime, token.UtcExpiresOn)
                 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenName"></param>
        /// <returns>
        /// 0 : when token does not exists in DB
        /// 1 : when Session Expires
        /// 2 : when Token is Valid
        /// </returns>
        public int ValidateToken(string tokenName, string application)
        {
            IDataReader dataReader = this.dataAccess.ExecuteReader
                 (
                     this.RPV2SystemDBConnectionString,
                     SPGetToken,
                     this.dataAccess.CreateParameter(DBCToken, SqlDbType.VarChar, tokenName)
                 );
            if (dataReader.Read() && dataReader["Token"].ToString() == tokenName && dataReader["ApplicationName"].ToString() == application)
            {
                DateTime utcExpiresOn = Convert.ToDateTime(dataReader["UtcExpiresOn"]);
                TokenDetails token = new TokenDetails()
                {
                    UserId = Convert.ToInt32(dataReader["UserId"]),
                    Token = tokenName
                };
                int timeDiff = utcExpiresOn.Subtract(DateTime.UtcNow).Minutes;
                if (timeDiff < 0)
                {
                    DeleteToken(token);
                    return 1;
                }
                else if (timeDiff > 0 && timeDiff <= 10)
                {
                    utcExpiresOn = utcExpiresOn.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["TokenExpiryTime"]));
                    token.UtcExpiresOn = utcExpiresOn;
                    UpdateToken(token);
                }
                return 2;
            }
            else
                return 0;
        }

        public void DeleteToken(TokenDetails token)
        {
            this.dataAccess.ExecuteNonQuery
                 (
                     this.RPV2SystemDBConnectionString,
                     SPDeleteToken,
                     this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, token.UserId),
                     this.dataAccess.CreateParameter(DBCToken, SqlDbType.VarChar, token.Token)
                 );
        }

        #endregion

    }
}
