using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using BCSDocUpdateApprovalV2.Filters;
using BCSDocUpdateApprovalV2.Formats;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCSDocUpdateApprovalV2.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorizationFilter(Roles = "*")]
    [CustomExceptionFilter]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        // GET: api/Users
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/5
        [Route("GetUserData")]
        [HttpGet]
        public BCSV2UserData GetUserData(int Id)
        {
            BCSV2AuthenticationFactory objFactory = new BCSV2AuthenticationFactory();
            return objFactory.GetUserDetail(Id);
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void DeleteUser(int id)
        {
           
        }

        [Route("GetAllUsers")]
        [HttpPost]
        public object GetAllUsers([FromBody]JObject data)
        {
            dynamic User = data;
            KendoGridPost postData = new KendoGridPost(User);
            int VirtualCount = 0;


            dynamic lockOut = null; dynamic emailcnfrm = null;
            if(!string.IsNullOrEmpty(User.ManageUsers.LockoutEnabled.ToString()))
                lockOut = Convert.ToBoolean(User.ManageUsers.LockoutEnabled);

            if (!string.IsNullOrEmpty(User.ManageUsers.EmailConfirmed.ToString()))
                emailcnfrm = Convert.ToBoolean(User.ManageUsers.EmailConfirmed);

            //int startIndex = (postData.Page - 1) * postData.PageSize + 1;
            //int endIndex = postData.Page * postData.PageSize;

            int startIndex = (postData.Page - 1) * postData.PageSize;
            int endIndex = startIndex + postData.PageSize;

             string sortField = string.IsNullOrEmpty(postData.SortColumn) ? "UserName" : postData.SortColumn,
                   sortOrder = string.IsNullOrEmpty(postData.SortOrder) ? "ASC" : postData.SortOrder.ToUpper();

            BCSV2AuthenticationFactory objAuthFactory = new BCSV2AuthenticationFactory();
            BCSV2User objUser = new BCSV2User
            {
                UserName = User.ManageUsers.UserName,
                FirstName = User.ManageUsers.FirstName,
                LastName = User.ManageUsers.LastName,
                Email = User.ManageUsers.Email,
                LockoutEnabled = lockOut,   
                EmailConfirmed = emailcnfrm,
                UserId = User.ManageUsers.UserId

            };
            List<BCSV2User> lstUserData = objAuthFactory.GetAllUsers(objUser, startIndex, endIndex, sortField, sortOrder,out VirtualCount);
            return Json(new { data = lstUserData, total = VirtualCount });
        }

        [Route("GetUserDetails")]
        [HttpGet]
        public object GetUserDetails(int UserId)
        {
            BCSV2AuthenticationFactory objAuthFactory = new BCSV2AuthenticationFactory();
            List<BCSV2User> lstUserData = objAuthFactory.GetAllUserDetail(UserId);

            List<string> lstUserName = lstUserData.Select(X => X.UserName).Distinct().ToList();
            List<string> lstFirstName = lstUserData.Select(X => X.FirstName).Distinct().ToList();
            List<string> lstLastName = lstUserData.Select(X => X.LastName).Distinct().ToList();
            List<string> lstEmail = lstUserData.Select(X => X.Email).Distinct().ToList();
            List<string> lstEmailConfirmed = lstUserData.Select(x => x.EmailConfirmed.ToString()).Distinct().ToList();
            List<string> lstLockoutEnabled = lstUserData.Select(x => x.LockoutEnabled.ToString()).Distinct().ToList();

            return Json(new
            {
                UserNames = lstUserName,
                FirstNames = lstFirstName,
                LastNames = lstLastName,
                Emails = lstEmail,
                EmailConfirmed=lstEmailConfirmed,
                LockoutEnabled = lstLockoutEnabled
            });
        }


        [Route("AddEditUser")]
        [HttpPost]
        public string Post([FromBody]JObject value)
        {
            dynamic json = value;

            BCSV2User objUser = new BCSV2User();
            objUser.Email = json.Email;
            objUser.FirstName = json.FirstName;
            objUser.LastName = json.LastName;
            objUser.UserName = json.Email;

            objUser.SecurityStamp = "test";
            objUser.ModifiedBy = 1;

            DataTable dtUserRoles = new DataTable();
            dtUserRoles.Columns.Add("UserId", typeof(int));
            dtUserRoles.Columns.Add("RoleId", typeof(int));

            List<int> lstRoles = json.selectedRoles.ToObject<List<int>>();
            lstRoles.ForEach(rl =>
            {
                dtUserRoles.Rows.Add(0, rl);
            });

            BCSV2AuthenticationFactory objAuthFactory = new BCSV2AuthenticationFactory();
            return objAuthFactory.AddEditUser(objUser, dtUserRoles);
        }

        [Route("UpdateUser")]
        [HttpPost]
        public string UpdateUser([FromBody]JObject value)
        {
            dynamic json = value;

            BCSV2User objUser = new BCSV2User();
            objUser.UserId = json.UserId;
            objUser.LockoutEnabled = json.LockOutEnabled;

            objUser.SecurityStamp = "test";
            objUser.ModifiedBy = 1;

            DataTable dtUserRoles = new DataTable();
            dtUserRoles.Columns.Add("UserId", typeof(int));
            dtUserRoles.Columns.Add("RoleId", typeof(int));

            List<int> lstRoles = json.selectedRoles.ToObject<List<int>>();
            lstRoles.ForEach(rl =>
            {
                dtUserRoles.Rows.Add(0, rl);
            });

            BCSV2AuthenticationFactory objAuthFactory = new BCSV2AuthenticationFactory();
            return objAuthFactory.UpdateUser(objUser, dtUserRoles);
        }
    }
}
