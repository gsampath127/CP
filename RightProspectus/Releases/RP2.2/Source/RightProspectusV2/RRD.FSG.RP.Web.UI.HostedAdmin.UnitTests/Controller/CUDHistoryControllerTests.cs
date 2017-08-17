using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System.Web.Mvc;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Model.SortDetail.System;
using System.Web.Script.Serialization;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System.Collections;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for CUD History
    /// </summary>
    [TestClass]
    public class CUDHistoryControllerTests:BaseTestController<CUDHistoryObjectModel>
    {
        Mock<IFactory<CUDHistoryObjectModel, CUDHistoryKey>> mockCUDHistoryFactory;

        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockuserCacheFactory;
        [TestInitialize]
        public void TestInitialze()
        {
            mockCUDHistoryFactory = new Mock<IFactory<CUDHistoryObjectModel, CUDHistoryKey>>();
            mockuserCacheFactory = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
        }

        #region Internalmethodreturn
        public IEnumerable<CUDHistoryObjectModel> GetEntityData()
        {
            IEnumerable<CUDHistoryObjectModel> enumCUDHistoryObjectModel = Enumerable.Empty<CUDHistoryObjectModel>();
            List<CUDHistoryObjectModel> lstCUDHistoryObjectModel = new List<CUDHistoryObjectModel>();
            CUDHistoryObjectModel objCUDHistoryObjectModel = new CUDHistoryObjectModel();
            objCUDHistoryObjectModel.CUDHistoryId = 1;
            objCUDHistoryObjectModel.ColumnName = "A";
            objCUDHistoryObjectModel.TableName = "A";
            objCUDHistoryObjectModel.UserId = 0;
            objCUDHistoryObjectModel.CUDType = "U";
            objCUDHistoryObjectModel.NewValue = "A";
            objCUDHistoryObjectModel.OldValue = "A";
            objCUDHistoryObjectModel.SqlDbType = 0;
            objCUDHistoryObjectModel.UtcCUDDate = DateTime.Now;
            lstCUDHistoryObjectModel.Add(objCUDHistoryObjectModel);
            enumCUDHistoryObjectModel = lstCUDHistoryObjectModel;
            return enumCUDHistoryObjectModel;

        }

        public IEnumerable<UserObjectModel> Getuserlistadmin()
        {
            IEnumerable<UserObjectModel> IenumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            List<RolesObjectModel> lstroles=new List<RolesObjectModel>();
            lstroles.Add(new RolesObjectModel(){RoleId=0,RoleName="Admin"});
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 0;
            objUserObjectModel.UserName = "Test";
            objUserObjectModel.Name = "Test";
            objUserObjectModel.Roles = lstroles;
            lstUserObjectModel.Add(objUserObjectModel);
            IenumUserObjectModel = lstUserObjectModel;
            return IenumUserObjectModel;
        }

        public IEnumerable<UserObjectModel> Getuserlist()
        {
            IEnumerable<UserObjectModel> IenumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            List<RolesObjectModel> lstroles = new List<RolesObjectModel>();
            lstroles.Add(new RolesObjectModel() { RoleId = 0, RoleName = "User" });
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 0;
            objUserObjectModel.UserName = "Test";
            objUserObjectModel.Name = "Test";
            objUserObjectModel.Roles = lstroles;
            lstUserObjectModel.Add(objUserObjectModel);
            IenumUserObjectModel = lstUserObjectModel;
            return IenumUserObjectModel;
        }

        #endregion

        #region Expected 
        public List<DisplayValuePair> ExpectedUserName()
        {
            List<DisplayValuePair> lstUserNames = new List<DisplayValuePair>();
            lstUserNames.Add(new DisplayValuePair() { Display = "Test", Value = "0" });
            return lstUserNames;
        }

        public List<DisplayValuePair> ExpectedCUDType()
        {
            List<DisplayValuePair> lstCUDTypes = new List<DisplayValuePair>();
            lstCUDTypes.Add(new DisplayValuePair() { Display = "Update", Value = "U" });
            lstCUDTypes.Add(new DisplayValuePair() { Display = "Delete", Value = "D" });
            lstCUDTypes.Add(new DisplayValuePair() { Display = "Insert", Value = "I" });
            return lstCUDTypes;
        }
        #endregion

        #region Index_Returns_ActionResult
        /// <summary>
        /// Index_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void Index_Returns_ActionResult()
        {
            //Arrange
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>())).Returns(Getuserlist());
            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.Index();

            //Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("CUDHistoryView", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region CheckIsAdmin
        /// <summary>
        /// CheckIsAdmin
        /// </summary>
        [TestMethod]
        public void CheckIsAdmin()
        {
            //Arrange
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(Getuserlist());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.checkIsAdmin();
           
            //Verify and Assert
            mockuserCacheFactory.VerifyAll();
            Assert.AreEqual(result, false);
            Assert.IsInstanceOfType(result, typeof(bool));


        }
        #endregion

        #region GetUserName_Returns_JsonResults
        /// <summary>
        /// GetUserName_Returns_JsonResults
        /// </summary>
        [TestMethod]
        public void GetUserName_Returns_JsonResults()
        {

            //Arrange
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(Getuserlist());
            mockCUDHistoryFactory.Setup(x => x.GetAllEntities()).Returns(GetEntityData());
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
            .Returns(Getuserlistadmin());
           
            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetUserName();
            
            //Verify and Assert
            ValidateDisplayValuePair(ExpectedUserName(),result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetUserName_Returns_JsonResults_withoutmatch
        /// <summary>
        /// GetUserName_Returns_JsonResults_withoutmatch
        /// </summary>
        [TestMethod]
        public void GetUserName_Returns_JsonResults_withoutmatch()
        {
            IEnumerable<UserObjectModel> IenumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            List<RolesObjectModel> lstroles = new List<RolesObjectModel>();
            lstroles.Add(new RolesObjectModel() { RoleId = 0, RoleName = "User" });
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test";
            objUserObjectModel.Name = "Test";
            objUserObjectModel.Roles = lstroles;
            lstUserObjectModel.Add(objUserObjectModel);
            IenumUserObjectModel = lstUserObjectModel;

            List<DisplayValuePair> lstUserNames = new List<DisplayValuePair>();
            lstUserNames.Add(new DisplayValuePair() { Display = "2", Value = "2" });

            IEnumerable<CUDHistoryObjectModel> enumCUDHistoryObjectModel = Enumerable.Empty<CUDHistoryObjectModel>();
            List<CUDHistoryObjectModel> lstCUDHistoryObjectModel = new List<CUDHistoryObjectModel>();
            CUDHistoryObjectModel objCUDHistoryObjectModel = new CUDHistoryObjectModel();
            objCUDHistoryObjectModel.CUDHistoryId = 1;
            objCUDHistoryObjectModel.ColumnName = "A";
            objCUDHistoryObjectModel.TableName = "A";
            objCUDHistoryObjectModel.UserId = 2;
            objCUDHistoryObjectModel.CUDType = "U";
            objCUDHistoryObjectModel.NewValue = "A";
            objCUDHistoryObjectModel.OldValue = "A";
            objCUDHistoryObjectModel.SqlDbType = 0;
            objCUDHistoryObjectModel.UtcCUDDate = DateTime.Now;
            lstCUDHistoryObjectModel.Add(objCUDHistoryObjectModel);
            enumCUDHistoryObjectModel = lstCUDHistoryObjectModel;
 
            //Arrange
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(IenumUserObjectModel);
            mockCUDHistoryFactory.Setup(x => x.GetAllEntities()).Returns(enumCUDHistoryObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
            .Returns(Getuserlistadmin());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetUserName();

            //Verify and Assert
            ValidateDisplayValuePair(lstUserNames, result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetUserName_Returns_JsonResult_emptylist
        /// <summary>
        /// GetUserName_Returns_JsonResult_emptylist
        /// </summary>
        [TestMethod]
        public void GetUserName_Returns_JsonResult_emptylist()
        {
            DisplayValuePair empty = new DisplayValuePair();
            //Arrange
            IEnumerable<CUDHistoryObjectModel> enumCUDObjectModel = Enumerable.Empty<CUDHistoryObjectModel>();
            mockCUDHistoryFactory.Setup(x => x.GetAllEntities()).Returns(enumCUDObjectModel);
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(Getuserlist());
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
             .Returns(Getuserlistadmin());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetUserName();

            //Verify and Assert
            ValidateEmptyData<DisplayValuePair>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetUserName_Returns_JsonResults_withoutadmin
        /// <summary>
        /// GetUserName_Returns_JsonResults_withoutadmin
        /// </summary>
        [TestMethod]
        public void GetUserName_Returns_JsonResults_withoutadmin()
        {

            //Arrange
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(Getuserlist());
            mockCUDHistoryFactory.Setup(x => x.GetAllEntities()).Returns(GetEntityData());
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
              .Returns(Getuserlist());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetUserName();

            //Verify and Assert
            ValidateDisplayValuePair(ExpectedUserName(), result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetUserName_Returns_JsonResult_withoutadmin_emptylist
        /// <summary>
        /// GetUserName_Returns_JsonResult_withoutadmin_emptylist
        /// </summary>
        [TestMethod]
        public void GetUserName_Returns_JsonResult_withoutadmin_emptylist()
        {
            //Arrange
            IEnumerable<CUDHistoryObjectModel> enumCUDObjectModel = Enumerable.Empty<CUDHistoryObjectModel>();
           // IEnumerable<UserObjectModel> enumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(Getuserlist());
            mockCUDHistoryFactory.Setup(x => x.GetAllEntities()).Returns(enumCUDObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
             .Returns(Getuserlist());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetUserName();

            //Verify and Assert
            ValidateEmptyData<DisplayValuePair>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTableNames_Returns_JsonResult
        /// <summary>
        /// GetTableNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetTableNames_Returns_JsonResult()
        {
            //Arrange
            List<DisplayValuePair> lstTableName=new List<DisplayValuePair>();
            lstTableName.Add(new DisplayValuePair(){Display="A",Value="A"});
            mockCUDHistoryFactory.Setup(x => x.GetAllEntities()).Returns(GetEntityData());
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
             .Returns(Getuserlistadmin());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetTableNames();

            //Verify and Assert
            ValidateDisplayValuePair(lstTableName, result);
            mockCUDHistoryFactory.VerifyAll();
            mockuserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTableNames_Returns_JsonResult_emptylist
        /// <summary>
        /// GetTableNames_Returns_JsonResult_emptylist
        /// </summary>
        [TestMethod]
        public void GetTableNames_Returns_JsonResult_emptylist()
        {
            //Arrange
            IEnumerable<CUDHistoryObjectModel> enumCUDObjectModel = Enumerable.Empty<CUDHistoryObjectModel>();
            mockCUDHistoryFactory.Setup(x => x.GetAllEntities()).Returns(enumCUDObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
             .Returns(Getuserlistadmin());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetTableNames();

            //Verify and Assert
            ValidateEmptyData<DisplayValuePair>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTableNames_Returns_JsonResult_withoutadmin
        /// <summary>
        /// GetTableNames_Returns_JsonResult_withoutadmin
        /// </summary>
        [TestMethod]
        public void GetTableNames_Returns_JsonResult_withoutadmin()
        {
            //Arrange
             List<DisplayValuePair> lstTableName=new List<DisplayValuePair>();
            lstTableName.Add(new DisplayValuePair(){Display="A",Value="A"});

            mockCUDHistoryFactory.Setup(x => x.GetAllEntities()).Returns(GetEntityData());
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
             .Returns(Getuserlist());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetTableNames();

            //Verify and Assert
            ValidateDisplayValuePair(lstTableName, result);
            mockCUDHistoryFactory.VerifyAll();
            mockuserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTableNames_Returns_JsonResult_withoutadmin_emptylist
        /// <summary>
        /// GetTableNames_Returns_JsonResult_withoutadmin_emptylist
        /// </summary>
        [TestMethod]
        public void GetTableNames_Returns_JsonResult_withoutadmin_emptylist()
        {
            //Arrange
            IEnumerable<CUDHistoryObjectModel> enumCUDObjectModel = Enumerable.Empty<CUDHistoryObjectModel>();
            mockCUDHistoryFactory.Setup(x => x.GetAllEntities()).Returns(enumCUDObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
             .Returns(Getuserlist());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetTableNames();

            //Verify and Assert
            ValidateEmptyData<DisplayValuePair>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetCUDType_Returns_JsonResult
        [TestMethod]
        public void GetCUDType_Returns_JsonResult()
        {
            //Arrange
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
            .Returns(Getuserlist());
                
            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            JsonResult result = objCUDHistory.GetCUDType();
            string s = result.Data.ToString();
            
           //Verify and Assert
            ValidateDisplayValuePair(ExpectedCUDType(), result);
            mockuserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllCUDHistoryDetails_Post_Returns_JsonResult
        /// <summary>
        /// GetAllCUDHistoryDetails_Post_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetAllCUDHistoryDetails_Post_Returns_JsonResult()
        {
            //Arrange

            int r = 0;
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(Getuserlist());
            mockCUDHistoryFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CUDHistorySearchDetail>(), It.IsAny<CUDHistorySortDetail>(), out r)).Returns(GetEntityData());
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
            .Returns(Getuserlistadmin());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetAllCUDHistoryDetails("Table", "Test", DateTime.Now, DateTime.Now, "0");

            //Verify and Assert
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            ValidateEmptyData<CUDHistoryObjectModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllCUDHistoryDetails_Post_Returns_JsonResult_unmatchuserid
        /// <summary>
        /// GetAllCUDHistoryDetails_Post_Returns_JsonResult_unmatchuserid
        /// </summary>
        [TestMethod]
        public void GetAllCUDHistoryDetails_Post_Returns_JsonResult_unmatchuserid()
        {
            //Arrange
            IEnumerable<UserObjectModel> IenumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            List<RolesObjectModel> lstroles = new List<RolesObjectModel>();
            lstroles.Add(new RolesObjectModel() { RoleId = 0, RoleName = "User" });
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test";
            objUserObjectModel.Name = "Test";
            objUserObjectModel.Roles = lstroles;
            lstUserObjectModel.Add(objUserObjectModel);
            IenumUserObjectModel = lstUserObjectModel;

            IEnumerable<CUDHistoryObjectModel> enumCUDHistoryObjectModel = Enumerable.Empty<CUDHistoryObjectModel>();
            List<CUDHistoryObjectModel> lstCUDHistoryObjectModel = new List<CUDHistoryObjectModel>();
            CUDHistoryObjectModel objCUDHistoryObjectModel = new CUDHistoryObjectModel();
            objCUDHistoryObjectModel.CUDHistoryId = 1;
            objCUDHistoryObjectModel.ColumnName = "A";
            objCUDHistoryObjectModel.TableName = "A";
            objCUDHistoryObjectModel.UserId = 2;
            objCUDHistoryObjectModel.CUDType = "U";
            objCUDHistoryObjectModel.NewValue = "A";
            objCUDHistoryObjectModel.OldValue = "A";
            objCUDHistoryObjectModel.SqlDbType = 0;
            objCUDHistoryObjectModel.UtcCUDDate = DateTime.Now;
            lstCUDHistoryObjectModel.Add(objCUDHistoryObjectModel);
            enumCUDHistoryObjectModel = lstCUDHistoryObjectModel;

            int r = 0;
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(IenumUserObjectModel);
            mockCUDHistoryFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CUDHistorySearchDetail>(), It.IsAny<CUDHistorySortDetail>(), out r)).Returns(enumCUDHistoryObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
            .Returns(Getuserlistadmin());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetAllCUDHistoryDetails("Table", "Test", DateTime.Now, DateTime.Now, "3");

            //Verify and Assert
            ValidateEmptyData<CUDHistoryObjectModel>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllCUDHistoryDetails_Post_Returns_JsonResult_emptysearch
        /// <summary>
        /// GetAllCUDHistoryDetails_Post_Returns_JsonResult_emptysearch
        /// </summary>
        [TestMethod]
        public void GetAllCUDHistoryDetails_Post_Returns_JsonResult_emptysearch()
        {
            //Arrange
            IEnumerable<CUDHistoryObjectModel> enumCUDObjectModel = Enumerable.Empty<CUDHistoryObjectModel>();
            int r = 0;
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(Getuserlist());
            mockCUDHistoryFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CUDHistorySearchDetail>(), It.IsAny<CUDHistorySortDetail>(), out r)).Returns(enumCUDObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
            .Returns(Getuserlistadmin());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetAllCUDHistoryDetails(null,null, DateTime.Now, DateTime.Now, "0");

            //Verify and Assert
            ValidateEmptyData<CUDHistoryObjectModel>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllCUDHistoryDetails_Post_Returns_JsonResult_Useridnull
        /// <summary>
        /// GetAllCUDHistoryDetails_Post_Returns_JsonResult_Useridnull
        /// </summary>
        [TestMethod]
        public void GetAllCUDHistoryDetails_Post_Returns_JsonResult_Useridnull()
        {
            //Arrange
            int r = 0;
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(Getuserlist());
            mockCUDHistoryFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CUDHistorySearchDetail>(), It.IsAny<CUDHistorySortDetail>(), out r)).Returns(GetEntityData());
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
            .Returns(Getuserlistadmin());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetAllCUDHistoryDetails("Table", "Test", DateTime.Now, DateTime.Now.AddDays(-5), null);

            //Verify and Assert
            ValidateEmptyData<CUDHistoryObjectModel>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllCUDHistoryDetails_Post_Returns_JsonResult_NoDatatable
        /// <summary>
        /// GetAllCUDHistoryDetails_Post_Returns_JsonResult_NoDatatable
        /// </summary>
        [TestMethod]
        public void GetAllCUDHistoryDetails_Post_Returns_JsonResult_NoDatatable()
        {
            //Arrange
            int r = 0;
            IEnumerable<CUDHistoryObjectModel> enumCUDObjectModel = Enumerable.Empty<CUDHistoryObjectModel>(); 
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(Getuserlist());
            mockCUDHistoryFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CUDHistorySearchDetail>(), It.IsAny<CUDHistorySortDetail>(), out r)).Returns(enumCUDObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
            .Returns(Getuserlist());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetAllCUDHistoryDetails("", "Test", DateTime.Now, DateTime.Now, "Test");
            
            //Verify and Assert
            ValidateEmptyData<CUDHistoryObjectModel>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult)); 
        }
        #endregion

        #region GetAllCUDHistoryDetails_Post_Returns_JsonResult_NoCUDType
        /// <summary>
        /// GetAllCUDHistoryDetails_Post_Returns_JsonResult_NoCUDType
        /// </summary>
        [TestMethod]
        public void GetAllCUDHistoryDetails_Post_Returns_JsonResult_NoCUDType()
        {
            //Arrange
            int r = 0;
            mockuserCacheFactory.Setup(x => x.GetAllEntities()).Returns(Getuserlist());
            mockCUDHistoryFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CUDHistorySearchDetail>(), It.IsAny<CUDHistorySortDetail>(), out r)).Returns(GetEntityData());
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
            .Returns(Getuserlist());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetAllCUDHistoryDetails("Test", "", DateTime.Now, DateTime.Now, "");

            //Verify and Assert
            ValidateEmptyData<CUDHistoryObjectModel>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllCUDHistoryDataDetails_Post_Returns_JsonResult
        [TestMethod]
        public void GetAllCUDHistoryDataDetails_Post_Returns_JsonResult()
        {
            //Arrange
            int r = 0;
            mockCUDHistoryFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CUDHistorySearchDetail>(), It.IsAny<CUDHistorySortDetail>(), out r)).Returns(GetEntityData());
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
             .Returns(Getuserlist());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetAllCUDHistoryDataDetails(It.IsAny<int>());

            //Verify and Assert
            ValidateEmptyData<CUDHistoryObjectModel>(result);
            mockCUDHistoryFactory.VerifyAll();
            mockuserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllCUDHistoryDataDetails_Post_Returns_JsonResult_emptydata
        /// <summary>
        ///  GetAllCUDHistoryDataDetails_Post_Returns_JsonResult_emptydata
        /// </summary>
        [TestMethod]
        public void GetAllCUDHistoryDataDetails_Post_Returns_JsonResult_emptydata()
        {
            //Arrange
            int r = 0;
            IEnumerable<CUDHistoryObjectModel> enumCUDObjectModel = Enumerable.Empty<CUDHistoryObjectModel>(); 
            mockCUDHistoryFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CUDHistorySearchDetail>(), It.IsAny<CUDHistorySortDetail>(), out r)).Returns(enumCUDObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
             .Returns(Getuserlist());

            //Act
            CUDHistoryController objCUDHistory = new CUDHistoryController(mockCUDHistoryFactory.Object, mockuserCacheFactory.Object);
            var result = objCUDHistory.GetAllCUDHistoryDataDetails(It.IsAny<int>());

            //Verify and Assert
            ValidateEmptyData<CUDHistoryObjectModel>(result);
            mockuserCacheFactory.VerifyAll();
            mockCUDHistoryFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

    }
}
