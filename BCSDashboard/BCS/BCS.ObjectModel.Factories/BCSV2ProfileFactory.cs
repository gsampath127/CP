using BCS.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel.Factories
{
    public class BCSV2ProfileFactory
    {
        private readonly IDataAccess dataAccess;
        private readonly string SystemDBConnectionString;

        private const string SP_BCSV2_UpdateSecurityQuestion = "BCSV2_UpdateSecurityQuestion";
        private const string SP_BCSV2_UpdatePassword = "BCSV2_UpdateNewPassword";
        private const string DBCSecurityQuestionId = "SecurityQuestionId";
        private const string DBCSecurityAnswer = "SecurityAnswer";
        private const string DBCOldpassword = "OldPassword";
        private const string DBCNewPassword = "NewPassword";
        private const string DBCUserId = "UserId";
        public BCSV2ProfileFactory()
        {
            this.SystemDBConnectionString = DBConnectionString.RPV2SystemDBConnectionString();
            this.dataAccess = new DataAccess();
        }

        public string UpdateSecurityQuestion(BCSV2User objUser)
        {
            string result = string.Empty;
            string newSecurityAnswer = string.Empty;
            int newSecurityQuestionId = -1;
            int userId = -1;
            try
            {
                newSecurityAnswer = objUser.SecurityAnswer;
                newSecurityQuestionId = Convert.ToInt32(objUser.SecurityQuestionId);
                userId = objUser.UserId;
                this.dataAccess.ExecuteNonQuery
                (
                    this.SystemDBConnectionString,
                    SP_BCSV2_UpdateSecurityQuestion,
                     this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, userId),
                    this.dataAccess.CreateParameter(DBCSecurityQuestionId, SqlDbType.Int, newSecurityQuestionId),
                    this.dataAccess.CreateParameter(DBCSecurityAnswer, SqlDbType.VarChar, newSecurityAnswer)
                );

                result = "Ok";
                return result;
            }
            catch (Exception e)
            {
                return result;
            }

        }

        public bool UpdatePassword(BCSV2User objUser)
        {
            bool result = false;
            string oldPassword = string.Empty;
            string newPassword = string.Empty;
            int userId = -1;
            try
            {
                oldPassword = objUser.Password;
                newPassword = objUser.NewPassword;
                userId = objUser.UserId;
                IDataReader dataReader = this.dataAccess.ExecuteReader
                (
                    this.SystemDBConnectionString,
                    SP_BCSV2_UpdatePassword,
                     this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, userId),
                    this.dataAccess.CreateParameter(DBCOldpassword, SqlDbType.VarChar, oldPassword),
                    this.dataAccess.CreateParameter(DBCNewPassword, SqlDbType.VarChar, newPassword)
                );

                if (dataReader.Read())
                {
                    result = Convert.ToBoolean(dataReader["Valid"]);
                }

              
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }
    }
}
