using BCS.Core.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel.Factories
{
    public class BCSV2ForgotPasswordFactory
    {
         private readonly IDataAccess dataAccess;
        private readonly string SystemDBConnectionString;

        private const string SP_BCSV2_CheckGetSecurityQuestion = "BCSV2_CheckGetSecurityQuestion";
        private const string DBCEmail = "Email";
        private const string DBCNewPassword = "NewPassword";
        private const string SP_BCSV2_UpdatePassword = "BCSV2_UpdatePassword";
        public BCSV2ForgotPasswordFactory()
        {
            this.SystemDBConnectionString = DBConnectionString.RPV2SystemDBConnectionString();
            this.dataAccess = new DataAccess();
        }

        public BCSV2User CheckEmailForSecurityQuestion(string email)
        {
            string result = string.Empty;
            string Status = string.Empty;
            //string existingEmailID = string.Empty;
            string securityQuestion = string.Empty;
            string securityAnswer = string.Empty;
            BCSV2User objUser = new BCSV2User();
            try
            {
                using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.SystemDBConnectionString,
                    SP_BCSV2_CheckGetSecurityQuestion,
                     this.dataAccess.CreateParameter(DBCEmail, SqlDbType.VarChar, email)
                    
               ))
                {
                    if (reader.Read())
                    {
                        objUser.SecurityQuestion = reader["Question"].ToString();
                        objUser.SecurityAnswer = reader["Answer"].ToString();
                     }
                }
            }
            catch
            {
                
            }
            return objUser;
        }




        public string CheckSecurityAnswer(BCSV2User objUser)
        {
            string result = "Wrong";

            if (objUser.UserSecurityAnswer== objUser.SecurityAnswer)
            {
                result = "Ok";
            }
            return result;
        }

        public string SetPassword(BCSV2User objUser)
        {
            string result = string.Empty;          
            string newPassword = string.Empty;
            string email = string.Empty;
            try
            {
                 newPassword = objUser.NewPassword;
                 email= objUser.Email;
                 this.dataAccess.ExecuteNonQuery
                 (
                     this.SystemDBConnectionString,
                     SP_BCSV2_UpdatePassword,
                      this.dataAccess.CreateParameter(DBCEmail, SqlDbType.VarChar, email),
                     this.dataAccess.CreateParameter(DBCNewPassword, SqlDbType.VarChar, newPassword)
                 );

                 result = "Ok";                 
            }
            catch
            {
                result= "False";
            }
            return result;
        }
    }
}
