using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Model.SortDetail.System;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    [TestClass]
    public class UserFactoryTests : BaseTestFactory<UserObjectModel>
    {
        private Mock<IDataAccess> mockDataAccess;

        [TestInitialize]
        public void TestInitialze()
        {
            mockDataAccess = new Mock<IDataAccess>();
        }

        #region GetEntityByKey
        /// <summary>
        /// GetEntityByKey
        /// </summary>
        [TestMethod]
        public void GetEntityByKey()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            var result = objuserFactory.GetEntityByKey<UserObjectModel>(1);
            //Assert

            UserObjectModel objExpected = new UserObjectModel
            {
                AccessFailedCount = null,
                Clients = null,
                Email = "26682@gmail.com",
                EmailConfirmed = true,
                FirstName = null,
                LastName = null,
                LockoutEnabled = null,
                LockoutEndDateUtc = null,
                PasswordHash = "34628V193",
                PhoneNumber = null,
                PhoneNumberConfirmed = null,
                Roles = null,
                Description = null,
                Name = null,
                SecurityStamp = null,
                TwoFactorEnabled = null,
                UserName = null,
                UserId = 1
            };
            ValidateObjectModelData<UserObjectModel>(result, objExpected);
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetEntityByKey_Empty_Table
        /// <summary>
        /// GetEntityByKey_Empty_Table
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Empty_Table()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dt.Rows.Add(dtrow);

            DataTable dt1 = new DataTable();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt1);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            var result = objuserFactory.GetEntityByKey<UserObjectModel>(1);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetEntityByKey_returns_UserObjectModel
        /// <summary>
        /// GetEntityByKey_returns_UserObjectModel
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_returns_UserObjectModel()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            var result = objuserFactory.GetEntityByKey(1);
            //Assert           
            UserObjectModel objExpected = new UserObjectModel
            {
                AccessFailedCount = null,
                Clients = null,
                Email = "26682@gmail.com",
                EmailConfirmed = true,
                FirstName = null,
                LastName = null,
                LockoutEnabled = null,
                LockoutEndDateUtc = null,
                PasswordHash = "34628V193",
                PhoneNumber = null,
                PhoneNumberConfirmed = null,
                Roles = null,
                Description = null,
                Name = null,
                SecurityStamp = null,
                TwoFactorEnabled = null,
                UserName = null,
                UserId = 1
            };
            ValidateObjectModelData<UserObjectModel>(result, objExpected);
            mockDataAccess.VerifyAll();

        }
        #endregion
        #region GetEntityByKey__Returns_UserObjectModel_with_Empty_Table
        /// <summary>
        /// GetEntityByKey__Returns_UserObjectModel_with_Empty_Table
        /// </summary>
        [TestMethod]
        public void GetEntityByKey__Returns_UserObjectModel_with_Empty_Table()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dt.Rows.Add(dtrow);

            DataTable dt1 = new DataTable();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt1);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            var result = objuserFactory.GetEntityByKey(1);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region CreateEntity_With_NullParameter

        /// <summary>
        /// CreateEntity_With_NullParameter
        /// </summary>
        [TestMethod]
        public void CreateEntity_With_NullParameter()
        {
            //Arrange
            DataRow entity = null;
            //Act
            UserFactoryCache objTemplateTextFactoryCache = new UserFactoryCache(mockDataAccess.Object);
            var result = objTemplateTextFactoryCache.CreateEntity<UserObjectModel>(entity);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();

        }

        #endregion
        #region CreateEntity_IDataRecord_With_NullParameter

        /// <summary>
        /// CreateEntity_IDataRecord_With_NullParameter
        /// </summary>
        [TestMethod]
        public void CreateEntity_IDataRecord_With_NullParameter()
        {
            //Arrange
            IDataRecord entity = null;
            //Act
            UserFactoryCache objTemplateTextFactoryCache = new UserFactoryCache(mockDataAccess.Object);
            var result = objTemplateTextFactoryCache.CreateEntity<UserObjectModel>(entity);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();

        }

        #endregion
        #region GetAllEntities_Returns_IEnumerable
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("RoleId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientId"] = 3;
            dtrow["RoleId"] = 3;
            dtrow["Name"] = "Test_3";

            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            objuserFactory.Mode = FactoryCacheMode.All;
            var result = objuserFactory.GetAllEntities<UserObjectModel>();

            List<RolesObjectModel> listroles = new List<RolesObjectModel>();
            RolesObjectModel objRolesObjectModel = new RolesObjectModel();
            objRolesObjectModel.RoleId = 3;
            objRolesObjectModel.RoleName = "Test_3";
            listroles.Add(objRolesObjectModel);

            List<int> listclients = new List<int>();
            listclients.Add(3);

            UserObjectModel objExpected = new UserObjectModel
            {
                AccessFailedCount = null,
                Clients = listclients,
                Email = "26682@gmail.com",
                EmailConfirmed = true,
                FirstName = "U1",
                LastName = "L1",
                LockoutEnabled = null,
                LockoutEndDateUtc = null,
                PasswordHash = "34628V193",
                PhoneNumber = "9058585",
                PhoneNumberConfirmed = true,
                Roles = listroles,
                Description = null,
                Name = null,
                SecurityStamp = "DateTime.Now",
                TwoFactorEnabled = null,
                UserName = "user1",
                UserId = 1
            };
            ValidateObjectModelData<UserObjectModel>(result.ToList()[0], objExpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_IEnumerable_With_TwoRows
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_With_TwoRows
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_With_TwoRows()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("RoleId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientId"] = 3;
            dtrow["RoleId"] = 3;
            dtrow["Name"] = "Test_3";

            dt.Rows.Add(dtrow);

            DataRow dtrow_2 = dt.NewRow();
            dtrow_2["UserId"] = 2;
            dtrow_2["Email"] = "26682@gmail.com";
            dtrow_2["EmailConfirmed"] = true;
            dtrow_2["PasswordHash"] = "34628V193";
            dtrow_2["SecurityStamp"] = "DateTime.Now";
            dtrow_2["PhoneNumber"] = 9058585;
            dtrow_2["PhoneNumberConfirmed"] = true;
            dtrow_2["TwoFactorEnabled"] = false;
            dtrow_2["LockOutEndDateUtc"] = DateTime.Today;
            dtrow_2["LockoutEnabled"] = true;
            dtrow_2["AccessFailedCount"] = 1;
            dtrow_2["UserName"] = "user1";
            dtrow_2["FirstName"] = "U1";
            dtrow_2["LastName"] = "L1";
            dtrow_2["LastModified"] = DateTime.Today;
            dtrow_2["ModifiedBy"] = 2;
            dtrow_2["UtcLastModified"] = DateTime.Today;
            dtrow_2["ClientId"] = 6;
            dtrow_2["RoleId"] = 4;
            dtrow_2["Name"] = "Test_3";

            dt.Rows.Add(dtrow_2);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            objuserFactory.Mode = FactoryCacheMode.All;
            var result = objuserFactory.GetAllEntities<UserObjectModel>();

            List<RolesObjectModel> listroles = new List<RolesObjectModel>();
            RolesObjectModel objRolesObjectModel = new RolesObjectModel();
            objRolesObjectModel.RoleId = 3;
            objRolesObjectModel.RoleName = "Test_3";
            listroles.Add(objRolesObjectModel);

            List<int> listclients = new List<int>();
            listclients.Add(3);

            UserObjectModel objExpected = new UserObjectModel
            {
                AccessFailedCount = null,
                Clients = listclients,
                Email = "26682@gmail.com",
                EmailConfirmed = true,
                FirstName = "U1",
                LastName = "L1",
                LockoutEnabled = null,
                LockoutEndDateUtc = null,
                PasswordHash = "34628V193",
                PhoneNumber = "9058585",
                PhoneNumberConfirmed = true,
                Roles = listroles,
                Description = null,
                Name = null,
                SecurityStamp = "DateTime.Now",
                TwoFactorEnabled = null,
                UserName = "user1",
                UserId = 1
            };
            ValidateObjectModelData<UserObjectModel>(result.ToList()[0], objExpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_IEnumerable_ClientIdNull
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_ClientIdNull
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_ClientIdNull()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("RoleId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientId"] = DBNull.Value;
            dtrow["RoleId"] = 3;
            dtrow["Name"] = "Test_3";

            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            objuserFactory.Mode = FactoryCacheMode.All;
            var result = objuserFactory.GetAllEntities<UserObjectModel>();

            List<RolesObjectModel> listroles = new List<RolesObjectModel>();
            RolesObjectModel objRolesObjectModel = new RolesObjectModel();
            objRolesObjectModel.RoleId = 3;
            objRolesObjectModel.RoleName = "Test_3";
            listroles.Add(objRolesObjectModel);

            List<int> listclients = new List<int>();
            listclients.Add(3);

            UserObjectModel objExpected = new UserObjectModel
            {
                AccessFailedCount = null,
                Clients = listclients,
                Email = "26682@gmail.com",
                EmailConfirmed = true,
                FirstName = "U1",
                LastName = "L1",
                LockoutEnabled = null,
                LockoutEndDateUtc = null,
                PasswordHash = "34628V193",
                PhoneNumber = "9058585",
                PhoneNumberConfirmed = true,
                Roles = listroles,
                Description = null,
                Name = null,
                SecurityStamp = "DateTime.Now",
                TwoFactorEnabled = null,
                UserName = "user1",
                UserId = 1
            };
            ValidateObjectModelData<UserObjectModel>(result.ToList()[0], objExpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_IEnumerable_RoleIdNull
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_RoleIdNull
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_RoleIdNull()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("RoleId", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientId"] = DBNull.Value;
            dtrow["RoleId"] = DBNull.Value;
            dtrow["Name"] = "Test_3";

            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            objuserFactory.Mode = FactoryCacheMode.All;
            var result = objuserFactory.GetAllEntities<UserObjectModel>();

            List<RolesObjectModel> listroles = new List<RolesObjectModel>();
            RolesObjectModel objRolesObjectModel = new RolesObjectModel();
            objRolesObjectModel.RoleId = 3;
            objRolesObjectModel.RoleName = "Test_3";
            listroles.Add(objRolesObjectModel);

            List<int> listclients = new List<int>();
            listclients.Add(3);

            UserObjectModel objExpected = new UserObjectModel
            {
                AccessFailedCount = null,
                Clients = listclients,
                Email = "26682@gmail.com",
                EmailConfirmed = true,
                FirstName = "U1",
                LastName = "L1",
                LockoutEnabled = null,
                LockoutEndDateUtc = null,
                PasswordHash = "34628V193",
                PhoneNumber = "9058585",
                PhoneNumberConfirmed = true,
                Roles = listroles,
                Description = null,
                Name = null,
                SecurityStamp = "DateTime.Now",
                TwoFactorEnabled = null,
                UserName = "user1",
                UserId = 1
            };
            ValidateObjectModelData<UserObjectModel>(result.ToList()[0], objExpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_IEnumerable_with_Empty_Table
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_with_Empty_Table
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_with_Empty_Table()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("RoleId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientId"] = 3;
            dtrow["RoleId"] = 3;
            dtrow["Name"] = "Test_3";
            dt.Rows.Add(dtrow);

            DataTable dt1 = new DataTable();
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt1);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            objuserFactory.Mode = FactoryCacheMode.All;
            var result = objuserFactory.GetAllEntities<UserObjectModel>();
            ValidateEmptyData(result);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region SaveEntity_With_ModifyBy
        /// <summary>
        /// SaveEntity_With_ModifyBy
        /// </summary>
        [TestMethod]
        public void SaveEntity_With_ModifyBy()
        {
            //Arrange           
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "test";

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="UserId", Value=1 },
                new SqlParameter(){ ParameterName="Email", Value="pmk@wipro.com" },
                new SqlParameter(){ ParameterName="EmailConfirmed", Value=true },
                new SqlParameter(){ ParameterName="PasswordHash", Value="TEST_002" },
                new SqlParameter(){ ParameterName="PasswordHash", Value="TEST_002" },
                    new SqlParameter(){ ParameterName="SecurityStamp", Value="TEST_003" },
                        new SqlParameter(){ ParameterName="PhoneNumber", Value="TEST_004" },
                            new SqlParameter(){ ParameterName="PhoneNumberConfirmed", Value=true },
                                new SqlParameter(){ ParameterName="TwoFactorEnabled", Value=true },
                                    new SqlParameter(){ ParameterName="UserName", Value="TEST" },
                                        new SqlParameter(){ ParameterName="FirstName", Value="TEST_fname" },
                                            new SqlParameter(){ ParameterName="userRoles", Value="TEST_lna55" },
                                                new SqlParameter(){ ParameterName="clientUsers", Value="TEST_0077" },
                                                new SqlParameter(){ ParameterName="ModifiedBy", Value=32 },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
               .Returns(parameters[0])
               .Returns(parameters[1])
               .Returns(parameters[2])
               .Returns(parameters[3])
            .Returns(parameters[4])
            .Returns(parameters[5])
            .Returns(parameters[6])
            .Returns(parameters[7])
            .Returns(parameters[8])
            .Returns(parameters[9])
            .Returns(parameters[10])
            .Returns(parameters[11])
            .Returns(parameters[12])
            .Returns(parameters[13]);
            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            UserFactory objuserFactory = new UserFactory(mockDataAccess.Object);
            objuserFactory.SaveEntity(objUserObjectModel, 32);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region SaveEntity_With_UserObjectModel_Roles_And_Clients_NotEqualsToNull
        /// <summary>
        /// SaveEntity_With_UserObjectModel_Roles_NotEqualsToNull
        /// </summary>
        [TestMethod]
        public void SaveEntity_With_UserObjectModel_Roles_NotEqualsToNull()
        {
            //Arrange     

            List<RolesObjectModel> lstRolesObjectModel = new List<RolesObjectModel>();
            RolesObjectModel objroles = new RolesObjectModel();
            objroles.RoleId = 1;
            objroles.RoleName = "test_5";
            lstRolesObjectModel.Add(objroles);

            List<int> lstclientobjectmodel = new List<int>();
            lstclientobjectmodel.Add(4);

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="UserId", Value=1 },
                new SqlParameter(){ ParameterName="Email", Value="pmk@wipro.com" },
                new SqlParameter(){ ParameterName="EmailConfirmed", Value=true },
                new SqlParameter(){ ParameterName="PasswordHash", Value="TEST_002" },
                new SqlParameter(){ ParameterName="PasswordHash", Value="TEST_002" },
                    new SqlParameter(){ ParameterName="SecurityStamp", Value="TEST_003" },
                        new SqlParameter(){ ParameterName="PhoneNumber", Value="TEST_004" },
                            new SqlParameter(){ ParameterName="PhoneNumberConfirmed", Value=true },
                                new SqlParameter(){ ParameterName="TwoFactorEnabled", Value=true },
                                    new SqlParameter(){ ParameterName="UserName", Value="TEST" },
                                        new SqlParameter(){ ParameterName="FirstName", Value="TEST_fname" },
                                            new SqlParameter(){ ParameterName="userRoles", Value="TEST_lna55" },
                                                new SqlParameter(){ ParameterName="clientUsers", Value="TEST_0077" },
                                                new SqlParameter(){ ParameterName="ModifiedBy", Value=32 },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
               .Returns(parameters[0])
               .Returns(parameters[1])
               .Returns(parameters[2])
               .Returns(parameters[3])
            .Returns(parameters[4])
            .Returns(parameters[5])
            .Returns(parameters[6])
            .Returns(parameters[7])
            .Returns(parameters[8])
            .Returns(parameters[9])
            .Returns(parameters[10])
            .Returns(parameters[11])
            .Returns(parameters[12])
            .Returns(parameters[13]);
            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "test";
            objUserObjectModel.Roles = lstRolesObjectModel;
            objUserObjectModel.Clients = lstclientobjectmodel;
            //Act
            UserFactory objuserFactory = new UserFactory(mockDataAccess.Object);
            objuserFactory.SaveEntity(objUserObjectModel);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region DeleteEntity
        /// <summary>
        /// DeleteEntity
        /// </summary>
        [TestMethod]
        public void DeleteEntity()
        {
            //Arrange
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="DeletedBy", Value=32 },
               new SqlParameter(){ ParameterName="DBCUserId", Value=32 },

            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
            .Returns(parameters[0])
            .Returns(parameters[1]);
            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));


            //Act
            UserFactory objuserFactory = new UserFactory(mockDataAccess.Object);
            objuserFactory.DeleteEntity(3);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_IEnumerable_ClientId_NotContains
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_ClientId_NotContains
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_ClientId_NotContains()
        {
            //Arrange
            UserSortDetail objSortDtl = new UserSortDetail();
            objSortDtl.Column = UserSortColumn.UserName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("EmailConfirmed", typeof(bool));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("SecurityStamp", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dt.Columns.Add("TwoFactorEnabled", typeof(bool));
            dt.Columns.Add("LockOutEndDateUtc", typeof(DateTime));
            dt.Columns.Add("LockoutEnabled", typeof(bool));
            dt.Columns.Add("AccessFailedCount", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientId", typeof(int));
            dt.Columns.Add("RoleId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["UserId"] = 1;
            dtrow["Email"] = "26682@gmail.com";
            dtrow["EmailConfirmed"] = true;
            dtrow["PasswordHash"] = "34628V193";
            dtrow["SecurityStamp"] = "DateTime.Now";
            dtrow["PhoneNumber"] = 9058585;
            dtrow["PhoneNumberConfirmed"] = true;
            dtrow["TwoFactorEnabled"] = false;
            dtrow["LockOutEndDateUtc"] = DateTime.Today;
            dtrow["LockoutEnabled"] = true;
            dtrow["AccessFailedCount"] = 1;
            dtrow["UserName"] = "user1";
            dtrow["FirstName"] = "U1";
            dtrow["LastName"] = "L1";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["ModifiedBy"] = 2;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientId"] = 9;
            dtrow["RoleId"] = 3;
            dtrow["Name"] = "Test_3";

            dt.Rows.Add(dtrow);

            DataRow dtrow_2 = dt.NewRow();
            dtrow_2["UserId"] = 1;
            dtrow_2["Email"] = "26682@gmail.com";
            dtrow_2["EmailConfirmed"] = true;
            dtrow_2["PasswordHash"] = "34628V193";
            dtrow_2["SecurityStamp"] = "DateTime.Now";
            dtrow_2["PhoneNumber"] = 9058585;
            dtrow_2["PhoneNumberConfirmed"] = true;
            dtrow_2["TwoFactorEnabled"] = false;
            dtrow_2["LockOutEndDateUtc"] = DateTime.Today;
            dtrow_2["LockoutEnabled"] = true;
            dtrow_2["AccessFailedCount"] = 1;
            dtrow_2["UserName"] = "user1";
            dtrow_2["FirstName"] = "U1";
            dtrow_2["LastName"] = "L1";
            dtrow_2["LastModified"] = DateTime.Today;
            dtrow_2["ModifiedBy"] = 2;
            dtrow_2["UtcLastModified"] = DateTime.Today;
            dtrow_2["ClientId"] = DBNull.Value;
            dtrow_2["RoleId"] = 3;
            dtrow_2["Name"] = "Test_3";

            dt.Rows.Add(dtrow_2);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            UserFactoryCache objuserFactory = new UserFactoryCache(mockDataAccess.Object);
            objuserFactory.Mode = FactoryCacheMode.All;
            var result = objuserFactory.GetAllEntities<UserObjectModel>();

            List<RolesObjectModel> listroles = new List<RolesObjectModel>();
            RolesObjectModel objRolesObjectModel = new RolesObjectModel();
            objRolesObjectModel.RoleId = 3;
            objRolesObjectModel.RoleName = "Test_3";
            listroles.Add(objRolesObjectModel);

            List<int> listclients = new List<int>();
            listclients.Add(3);

            UserObjectModel objExpected = new UserObjectModel
            {
                AccessFailedCount = null,
                Clients = listclients,
                Email = "26682@gmail.com",
                EmailConfirmed = true,
                FirstName = "U1",
                LastName = "L1",
                LockoutEnabled = null,
                LockoutEndDateUtc = null,
                PasswordHash = "34628V193",
                PhoneNumber = "9058585",
                PhoneNumberConfirmed = true,
                Roles = listroles,
                Description = null,
                Name = null,
                SecurityStamp = "DateTime.Now",
                TwoFactorEnabled = null,
                UserName = "user1",
                UserId = 1
            };
            ValidateObjectModelData<UserObjectModel>(result.ToList()[0], objExpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion       
    }
}
