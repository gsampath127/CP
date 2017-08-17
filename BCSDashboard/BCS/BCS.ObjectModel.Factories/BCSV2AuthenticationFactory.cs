using BCS.Core.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BCS.ObjectModel.Factories
{
    public class BCSV2AuthenticationFactory
    {
        private readonly IDataAccess dataAccess;
        private readonly string SystemDBConnectionString;

        private const string SP_BCSV2_InsertUser = "BCSV2_InsertUser";
        private const string SP_BCSV2_GetSecurityQuestions = "BCSV2_GetSecurityQuestions";
        private const string SP_BCSV2_CompleteRegistration = "BCSV2_CompleteRegistration";
        private const string SP_BCSV2_GetChildRoles = "BCSV2_GetChildRoles";
        private const string SP_BCSV2_GetAllUsers = "BCSV2_GetAllUsers";
        private const string SP_BCSV2_GetUserDetails = "BCSV2_GetUserDetails";
        private const string SP_BCSV2_DeleteUser = "BCSV2_DeleteUser";
        private const string SP_BCSV2_AddEditUser = "BCSV2_AddEditUser";
        private const string SP_BCSV2_GetUsedDetailsWithUserId = "BCSV2_GetUsedDetailsWithUserId";
        private const string SP_BCSV2_UpdateUser = "BCSV2_UpdateUser";
        private const string SP_BCSV2_ValidateUserProfile = "BCSV2_ValidateUserProfile";


        private const string DBCEmail = "Email";
        private const string DBCPasswordHash = "PasswordHash";
        private const string DBCPhoneNumber = "PhoneNumber";
        private const string DBCUserName = "UserName";
        private const string DBCFirstName = "FirstName";
        private const string DBCLastName = "LastName";
        private const string DBCSecurityStamp = "SecurityStamp";
        private const string DBCApplicationName = "ApplicationName";
        private const string DBCModifiedBy = "ModifiedBy";
        private const string DBCTT_UserRoles = "TT_UserRoles";
        private const string DBCUserId = "UserId";
        private const string DBCSecurityQnId = "SecurityQnId";
        private const string DBCSecurityAnswer = "SecurityAnswer";
        private const string DBCUpdateStatus = "UpdateStatus";

        private const string DBCLockoutEnabled = "LockoutEnabled";
        private const string DBCEmailConfirmed = "EmailConfirmed";
        private const string DBCStartIndex = "startIndex";
        private const string DBCEndIndex = "endIndex";
        private const string DBCSortDirection = "sortDirection";
        private const string DBCSortColumn = "sortColumn";

        public BCSV2AuthenticationFactory()
        {
            this.SystemDBConnectionString = DBConnectionString.RPV2SystemDBConnectionString();
            this.dataAccess = new DataAccess();
        }

        public string RegisterUser(BCSV2User objUser, DataTable dtUserRoles)
        {
            string result = string.Empty;
            string Status = string.Empty;

            try
            {
                using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.SystemDBConnectionString,
                    SP_BCSV2_InsertUser,
                     this.dataAccess.CreateParameter(DBCEmail, SqlDbType.VarChar, objUser.Email),
                     this.dataAccess.CreateParameter(DBCFirstName, SqlDbType.VarChar, objUser.FirstName),
                     this.dataAccess.CreateParameter(DBCLastName, SqlDbType.VarChar, objUser.LastName),
                     this.dataAccess.CreateParameter(DBCSecurityStamp, SqlDbType.VarChar, objUser.SecurityStamp),
                     this.dataAccess.CreateParameter(DBCTT_UserRoles, SqlDbType.Structured, dtUserRoles),
                     this.dataAccess.CreateParameter(DBCApplicationName, SqlDbType.VarChar, ConfigurationManager.AppSettings["Application"].ToString()),
                     this.dataAccess.CreateParameter(DBCUserName, SqlDbType.VarChar, objUser.Email),
                     this.dataAccess.CreateParameter(DBCModifiedBy, SqlDbType.Int, objUser.ModifiedBy)
               ))
                {
                    if (reader.Read())
                    {
                        Status = reader["Status"].ToString();
                        result = Status;

                        if (Status == "Duplicate")
                        {
                            string Email = (reader["Email"]).ToString();
                            if (objUser.Email == Email)
                                result += " + Email";
                        }
                        else
                        {
                            //  mail
                            int InsertedUserId = int.Parse(reader["InsertedUserId"].ToString());
                            string name = objUser.FirstName + " " + objUser.LastName;
                            SendRegistrationEmail(objUser.Email, InsertedUserId, name, "Registration");
                        }
                    }
                }
            }
            catch
            {
                result = "Failed";
            }
            return result;
        }

        private string CreateMailBody(string Name, int InsertedUserId)
        {
            string body = string.Empty;
            StringBuilder strDtls = new StringBuilder();
            strDtls.Append("<span style='font-family:Calibri'>Dear " + Name + " ,<br><br>");
            strDtls.Append("Registration was successfull. Please follow below link to complete the registration process.<br><br></span> ");

            // string idHashed = UtilityFactory.EncryptString("126");  // InsertedUserId.ToString()
            string idHashed = InsertedUserId.ToString();
            string link = String.Format("<a href=\'" + ConfigurationManager.AppSettings["CompleteRegistration"].ToString() + "?userid={0}\'>Click here</a>", idHashed);
            strDtls.Append(link);

            strDtls.Append("<br><br> Thanks,<br> BCS Admin");

            body = strDtls.ToString();
            return body;
        }

        private void SendRegistrationEmail(string ReceipEmail, int InsertedUserId, string Name, string subject)
        {
            try
            {
                string EmailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
                string body = CreateMailBody(Name, InsertedUserId);
                string EmailCC = null;
                string EmailBCC = null;

                EmailHelper.SendEmail(EmailFrom, ReceipEmail, subject, body, EmailCC, EmailBCC);
            }
            catch (Exception ex)
            {

            }
        }

        public List<BCSV2SecurityQuestions> GetSecurityQns()
        {
            List<BCSV2SecurityQuestions> lstSecQns = new List<BCSV2SecurityQuestions>();
            using (IDataReader reader = this.dataAccess.ExecuteReader
              (
                   this.SystemDBConnectionString,
                   SP_BCSV2_GetSecurityQuestions
              ))
            {
                while (reader.Read())
                {
                    BCSV2SecurityQuestions objSecQn = new BCSV2SecurityQuestions
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString()
                    };
                    lstSecQns.Add(objSecQn);
                }
            }
            return lstSecQns;
        }

        public string CompleteRegistration(BCSV2User objUser)
        {
            string result = string.Empty;
            try
            {
                DbParameterCollection parametercollection = this.dataAccess.ExecuteNonQueryReturnOutputParams
               (
                    this.SystemDBConnectionString,
                    SP_BCSV2_CompleteRegistration,
                     this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, objUser.UserId),
                     this.dataAccess.CreateParameter(DBCPasswordHash, SqlDbType.VarChar, objUser.PasswordHash),
                     this.dataAccess.CreateParameter(DBCSecurityQnId, SqlDbType.Int, objUser.SecurityQuestionId),
                     this.dataAccess.CreateParameter(DBCSecurityAnswer, SqlDbType.VarChar, objUser.SecurityAnswer),
                     this.dataAccess.CreateParameter(DBCModifiedBy, SqlDbType.Int, objUser.ModifiedBy),
                     this.dataAccess.CreateParameter(DBCUpdateStatus, SqlDbType.VarChar, "", 10, ParameterDirection.Output)
               );

                if (parametercollection != null)
                    result = parametercollection[DBCUpdateStatus].Value.ToString();
                return result;
            }
            catch (Exception e)
            {
                result = "Failed";
                return result;
            }
        }

        public List<BCSV2Roles> GetChildRoles(int userId)
        {
            List<BCSV2Roles> lstChildRoles = new List<BCSV2Roles>();
            using (IDataReader reader = this.dataAccess.ExecuteReader
              (
                   this.SystemDBConnectionString,
                   SP_BCSV2_GetChildRoles,
                   this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, userId)
              ))
            {
                while (reader.Read())
                {
                    BCSV2Roles objRoles = new BCSV2Roles
                    {
                        RoleId = int.Parse(reader["RoleId"].ToString()),
                        RoleName = reader["Name"].ToString()
                    };
                    lstChildRoles.Add(objRoles);
                }
            }
            return lstChildRoles;
        }

        public List<BCSV2User> GetAllUsers(BCSV2User objUser, int startIndex, int endIndex, string sortField, string sortOrder, out int VirtualCount)
        {
            List<BCSV2User> lstUser = new List<BCSV2User>();
            try
            {              
                using (IDataReader reader = this.dataAccess.ExecuteReader
                   (
                        this.SystemDBConnectionString,
                        SP_BCSV2_GetAllUsers,
                        this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, objUser.UserId),
                         this.dataAccess.CreateParameter(DBCUserName, SqlDbType.VarChar, objUser.UserName),
                         this.dataAccess.CreateParameter(DBCFirstName, SqlDbType.VarChar, objUser.FirstName),
                         this.dataAccess.CreateParameter(DBCLastName, SqlDbType.VarChar, objUser.LastName),
                         this.dataAccess.CreateParameter(DBCEmail, SqlDbType.VarChar, objUser.Email),
                         this.dataAccess.CreateParameter(DBCLockoutEnabled, SqlDbType.Bit, objUser.LockoutEnabled),
                         this.dataAccess.CreateParameter(DBCEmailConfirmed, SqlDbType.Bit, objUser.EmailConfirmed),
                         this.dataAccess.CreateParameter(DBCStartIndex, SqlDbType.Int, startIndex),
                         this.dataAccess.CreateParameter(DBCEndIndex, SqlDbType.Int, endIndex),
                         this.dataAccess.CreateParameter(DBCSortDirection, SqlDbType.VarChar, sortOrder),
                         this.dataAccess.CreateParameter(DBCSortColumn, SqlDbType.VarChar, sortField),
                         this.dataAccess.CreateParameter(DBCApplicationName, SqlDbType.VarChar, ConfigurationManager.AppSettings["Application"].ToString())
                   ))
                {
                    while (reader.Read())
                    {
                        BCSV2User user = new BCSV2User();
                        user.UserId = int.Parse(reader["UserId"].ToString());
                        user.UserName = reader["UserName"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.LockoutEnabled = Convert.ToBoolean(reader["LockoutEnabled"].ToString());
                        user.EmailConfirmed = Convert.ToBoolean(reader["EmailConfirmed"].ToString());
                        lstUser.Add(user);
                    }
                    VirtualCount = 0;
                    if (reader.NextResult())
                    {
                        if (reader.Read())
                        {
                            VirtualCount = int.Parse(reader["VirtualCount"].ToString());
                        }
                    }
                }

                return lstUser;
            }
            catch
            {
                VirtualCount = 0;
                return lstUser;
            }

        }

        public List<BCSV2User> GetAllUserDetail(int UserId)
        {
            List<BCSV2User> lstUser = new List<BCSV2User>();
            try
            {
                using (IDataReader reader = this.dataAccess.ExecuteReader
                   (
                        this.SystemDBConnectionString,
                        SP_BCSV2_GetUserDetails,
                        this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, UserId),
                        this.dataAccess.CreateParameter(DBCApplicationName, SqlDbType.VarChar, ConfigurationManager.AppSettings["Application"].ToString())
                   ))
                {
                    while (reader.Read())
                    {
                        BCSV2User user = new BCSV2User();
                        user.UserId = int.Parse(reader["UserId"].ToString());
                        user.UserName = reader["UserName"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.LockoutEnabled = Convert.ToBoolean(reader["LockoutEnabled"].ToString());
                        user.EmailConfirmed = Convert.ToBoolean(reader["EmailConfirmed"].ToString());
                        lstUser.Add(user);
                    }
                }

                return lstUser;
            }
            catch
            {
                return lstUser;
            }

        }

        public string AddEditUser(BCSV2User objUser, DataTable dtUserRoles)
        {
            string result = string.Empty;
            string Status = string.Empty;

            try
            {
                using (IDataReader reader = this.dataAccess.ExecuteReader
               (
                    this.SystemDBConnectionString,
                    SP_BCSV2_AddEditUser,
                     this.dataAccess.CreateParameter(DBCEmail, SqlDbType.VarChar, objUser.Email),
                     this.dataAccess.CreateParameter(DBCFirstName, SqlDbType.VarChar, objUser.FirstName),
                     this.dataAccess.CreateParameter(DBCLastName, SqlDbType.VarChar, objUser.LastName),
                     this.dataAccess.CreateParameter(DBCSecurityStamp, SqlDbType.VarChar, objUser.SecurityStamp),
                     this.dataAccess.CreateParameter(DBCTT_UserRoles, SqlDbType.Structured, dtUserRoles),
                     this.dataAccess.CreateParameter(DBCApplicationName, SqlDbType.VarChar, ConfigurationManager.AppSettings["Application"].ToString()),
                     this.dataAccess.CreateParameter(DBCUserName, SqlDbType.VarChar, objUser.UserName),
                     this.dataAccess.CreateParameter(DBCModifiedBy, SqlDbType.Int, objUser.ModifiedBy)
               ))
                {
                    if (reader.Read())
                    {
                        Status = reader["Status"].ToString();
                        result = Status;

                        if (Status == "Duplicate")
                        {
                            string Email = (reader["Email"]).ToString();
                            if (objUser.Email == Email)
                                result += " + Email";
                            if (objUser.UserName == (reader["UserName"]).ToString())
                                result += " + UserName";
                        }
                        else
                        {
                            //  mail
                            int InsertedUserId = int.Parse(reader["InsertedUserId"].ToString());
                            string name = objUser.FirstName + " " + objUser.LastName;
                            SendRegistrationEmail(objUser.Email, InsertedUserId, name, "Registration");
                        }
                    }
                }
            }
            catch
            {
                result = "Failed";
            }
            return result;
        }

        public BCSV2UserData GetUserDetail(int UserId)
        {
            BCSV2UserData ObjUserData = new BCSV2UserData();
            BCSV2User ObjUser = new BCSV2User();
            List<BCSV2Roles> lstRoles = new List<BCSV2Roles>();
            try
            {
                using (IDataReader reader = this.dataAccess.ExecuteReader
                   (
                        this.SystemDBConnectionString,
                        SP_BCSV2_GetUsedDetailsWithUserId,
                        this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, UserId)
                   ))
                {
                    if (reader.Read())
                    {
                        ObjUser.UserId = int.Parse(reader["UserId"].ToString());
                        ObjUser.UserName = reader["UserName"].ToString();
                        ObjUser.FirstName = reader["FirstName"].ToString();
                        ObjUser.LastName = reader["LastName"].ToString();
                        ObjUser.Email = reader["Email"].ToString();
                        ObjUser.LockoutEnabled = Convert.ToBoolean(reader["LockoutEnabled"].ToString());
                        ObjUser.EmailConfirmed = Convert.ToBoolean(reader["EmailConfirmed"].ToString());
                        ObjUserData.UserData = ObjUser;
                    }
                    if(reader.NextResult())
                    {
                        while(reader.Read())
                        {
                            BCSV2Roles objRoles = new BCSV2Roles
                            {
                                RoleId = int.Parse(reader["RoleId"].ToString()),
                                RoleName = reader["RoleName"].ToString()
                            };
                            lstRoles.Add(objRoles);
                        }
                        ObjUserData.RolesData = lstRoles;
                    }
                }

                return ObjUserData;
            }
            catch
            {
                return ObjUserData;
            }
        }

        public string UpdateUser(BCSV2User objUser, DataTable dtUserRoles)
        {
            string result = string.Empty;

            try
            {
                DbParameterCollection parametercollection = this.dataAccess.ExecuteNonQueryReturnOutputParams
               (
                    this.SystemDBConnectionString,
                    SP_BCSV2_UpdateUser,
                     this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, objUser.UserId),
                     this.dataAccess.CreateParameter(DBCLockoutEnabled,SqlDbType.Bit,objUser.LockoutEnabled),
                     this.dataAccess.CreateParameter(DBCTT_UserRoles, SqlDbType.Structured, dtUserRoles),
                     this.dataAccess.CreateParameter(DBCModifiedBy, SqlDbType.Int, objUser.ModifiedBy),
                     this.dataAccess.CreateParameter(DBCUpdateStatus, SqlDbType.VarChar, "", 100, ParameterDirection.Output)
               );
                if (parametercollection != null)
                    result = parametercollection[DBCUpdateStatus].Value.ToString();
                return result;
            }
            catch
            {
                result = "Failed";
            }
            return result;
        }

        public string ValidateUserProfile(int UserId)
        {
            string result = string.Empty;
            try
            {
                DbParameterCollection parametercollection = this.dataAccess.ExecuteNonQueryReturnOutputParams
               (
                    this.SystemDBConnectionString,
                    SP_BCSV2_ValidateUserProfile,
                     this.dataAccess.CreateParameter(DBCUserId, SqlDbType.Int, UserId),
                     this.dataAccess.CreateParameter(DBCUpdateStatus, SqlDbType.VarChar, "", 100, ParameterDirection.Output)
               );

                if (parametercollection != null)
                    result = parametercollection[DBCUpdateStatus].Value.ToString();
                return result;
            }
            catch (Exception e)
            {
                result = "Failed";
                return result;
            }
        }
    }
}
