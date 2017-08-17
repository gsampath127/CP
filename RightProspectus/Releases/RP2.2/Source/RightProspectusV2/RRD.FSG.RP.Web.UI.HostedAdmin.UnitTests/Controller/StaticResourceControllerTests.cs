using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using RRD.FSG.RP.Model.Cache.VerticalMarket;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Entities.System;
using Moq;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;


namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for StaticResourceController class
    /// </summary>
    [TestClass]
    public class StaticResourceControllerTests : BaseTestController<StaticResourceObjectModel>
    {
        Mock<IFactoryCache<StaticResourceFactory, StaticResourceObjectModel, int>> mockstaticResourceCacheFactory;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserCacheFactory;

        [TestInitialize]
        public void TestInitialze()
        {
            mockstaticResourceCacheFactory = new Mock<IFactoryCache<StaticResourceFactory, StaticResourceObjectModel, int>>();
            mockUserCacheFactory = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
        }

        #region List_Returns_ActionResult
        /// <summary>
        /// List_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void List_Returns_ActionResult()
        {
            //Act
            StaticResourceController objStaticResourceController =
       new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.List();

            //Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Returnvalues
        private IEnumerable<StaticResourceObjectModel> CreateStaticResourceList()
        {
            IEnumerable<StaticResourceObjectModel> IenumStaticResourceObjectModel = Enumerable.Empty<StaticResourceObjectModel>();
            List<StaticResourceObjectModel> lstStaticResourceObjectModel = new List<StaticResourceObjectModel>();

            StaticResourceObjectModel objStaticResourceObjectModel = new StaticResourceObjectModel();
            objStaticResourceObjectModel.StaticResourceId = 1;
            objStaticResourceObjectModel.Name = "Test1";
            objStaticResourceObjectModel.Description = "Test_Doc";
            objStaticResourceObjectModel.MimeType = "XB";
            objStaticResourceObjectModel.FileName = "SA";
            // objStaticResourceObjectModel.LastModified = Convert.ToDateTime(DateTime.Now.ToString());
            lstStaticResourceObjectModel.Add(objStaticResourceObjectModel);
            IenumStaticResourceObjectModel = lstStaticResourceObjectModel;
            return IenumStaticResourceObjectModel;

        }
        private IEnumerable<StaticResourceObjectModel> CreateStaticResourceList_null()
        {
            IEnumerable<StaticResourceObjectModel> IenumStaticResourceObjectModel = Enumerable.Empty<StaticResourceObjectModel>();
            List<StaticResourceObjectModel> lstStaticResourceObjectModel = new List<StaticResourceObjectModel>();

            StaticResourceObjectModel objStaticResourceObjectModel = new StaticResourceObjectModel();
            //  objStaticResourceObjectModel = null;
            // objStaticResourceObjectModel.LastModified = Convert.ToDateTime(DateTime.Now.ToString());
            lstStaticResourceObjectModel.Add(objStaticResourceObjectModel);
            IenumStaticResourceObjectModel = lstStaticResourceObjectModel;
            return IenumStaticResourceObjectModel;

        }
        private IEnumerable<UserObjectModel> CreateUserList()
        {
            IEnumerable<UserObjectModel> IenumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_username";
            objUserObjectModel.FirstName = "Test_FirstName";
            objUserObjectModel.LastName = "Test_LastName";
            objUserObjectModel.Email = "Test_email@gmail.com";

            lstUserObjectModel.Add(objUserObjectModel);
            IenumUserObjectModel = lstUserObjectModel;
            return IenumUserObjectModel;

        }
        #endregion

        #region EditStaticResources_Returns_ActionResult_GreaterThansZero_GetMethod
        ///<summary>
        //// EditStaticResources_Returns_ActionResult_GreaterThansZero_GetMethod
        /// </summary>
        [TestMethod]
        public void EditStaticResources_Returns_ActionResult_GreaterThansZero_GetMethod()
        {
            StaticResourceObjectModel obj = new StaticResourceObjectModel();
            obj.StaticResourceId = 1;
            obj.Name = "Doc_1";

            obj.FileName = "XY";
            
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(obj);
            //Act
            StaticResourceController objStaticResourceController = new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.EditStaticResources(1);

            //Verify
            mockstaticResourceCacheFactory.VerifyAll();
            //Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditStaticResources_Returns_ActionResult_GetMethod_idZero
        ///<summary>
        //// EditStaticResources_Returns_ActionResult_GetMethod_idZero
        /// </summary>
        [TestMethod]
        public void EditStaticResources_Returns_ActionResult_GetMethod_idZero()
        {
            //Arrange

            //Act
            StaticResourceController objStaticResourceController = new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.EditStaticResources(0);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
        }
        #endregion

        #region GetFileName
        [TestMethod]
        public void GetFileName_Returns_JSonResult()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateStaticResourceList());
            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetFileName();
            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair
                {
                    Display = "SA",
                    Value = "SA"
                }
            };
            ValidateDisplayValuePair(lstExpected, result);
            mockstaticResourceCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFileName_null
        [TestMethod]
        public void GetFileName_Returns_JSonResult_null()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateStaticResourceList_null());
            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetFileName();
            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair()
            };
            ValidateDisplayValuePair(lstExpected, result);
            mockstaticResourceCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetMimeType

        [TestMethod]
        public void GetMimeType_Returns_JsonResult()
        {
            //Arrange 
            mockstaticResourceCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateStaticResourceList());
            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetMimeType();

            //verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair
                {
                    Display = "XB",
                    Value = "XB"
                }
            };
            ValidateDisplayValuePair(lstExpected, result);
            mockstaticResourceCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        #endregion

        #region GetMimeType_null
        [TestMethod]
        public void GetMimeType_Returns_JsonResult_null()
        {
            //Arrange 
            mockstaticResourceCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateStaticResourceList_null());
            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetMimeType();

            //verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
        {
            new DisplayValuePair()
        };
            ValidateDisplayValuePair(lstExpected, result);
            mockstaticResourceCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetModifiedDate
        ///<summary>
        ///GetModifiedDate_Returns_JsonResult
        ///</summary>
        [TestMethod]
        public void GetModifiedDate_Returns_JsonResult()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateStaticResourceList());
            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetModifiedDate();
            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair
                {
                    Display = "",
                    Value = ""
                }
            };
            ValidateDisplayValuePair(lstExpected, result);
            mockstaticResourceCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetModifiedDate_null
        ///<summary>
        ///GetModifiedDate_Returns_JsonResult_null
        ///</summary>
        [TestMethod]
        public void GetModifiedDate_Returns_JsonResult_null()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateStaticResourceList_null());
            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetModifiedDate();
            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair
                {
                    Display = "",
                    Value = ""
                }
            };
            ValidateDisplayValuePair(lstExpected, result);
            mockstaticResourceCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region DeleteStaticResource
        ///<summary>
        //// DeleteStaticResource_JsonResult
        ///<summary>
        [TestMethod]
        public void DeleteStaticResource_JsonResult()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.DeleteEntity(It.IsAny<int>(), It.IsAny<int>()));

            //Act
            StaticResourceController objStaticResourceController =
                 new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.DeleteStaticResource(It.IsAny<int>());

            //Verify and Assert
            Assert.AreEqual("", result.Data);
            mockstaticResourceCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetStaticResource_Result_JsonResult
        ///<summary>
        ////GetStaticResource_Result_JsonResult
        ///</summary>
        [TestMethod]
        public void GetStaticResource_Result_JsonResult()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<StaticResourceSearchDetail>()))
                .Returns(CreateStaticResourceList());

            mockstaticResourceCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<StaticResourceSearchDetail>(), It.IsAny<StaticResourceSortDetail>()))
                .Returns(CreateStaticResourceList());

            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetStaticResource("", "");

            //Verify and Assert
            ValidateEmptyData<StaticResourceObjectModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            mockstaticResourceCacheFactory.VerifyAll();
        }
        #endregion

        #region GetStaticResource_Result_JsonResult_fileName
        ///<summary>
        ////GetStaticResource_Result_JsonResult_fileName
        ///</summary>
        [TestMethod]
        public void GetStaticResource_Result_JsonResult_fileName()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<StaticResourceSearchDetail>()))
                .Returns(CreateStaticResourceList());

            mockstaticResourceCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<StaticResourceSearchDetail>(), It.IsAny<StaticResourceSortDetail>()))
                .Returns(CreateStaticResourceList());

            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetStaticResource("TestFile", "");

            //Verify and Assert
            ValidateEmptyData<StaticResourceObjectModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            mockstaticResourceCacheFactory.VerifyAll();
        }
        #endregion

        #region GetStaticResource_Result_JsonResult_MimeType
        ///<summary>
        ////GetStaticResource_Result_JsonResult_MimeType
        ///</summary>
        [TestMethod]
        public void GetStaticResource_Result_JsonResult_MimeType()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<StaticResourceSearchDetail>()))
                .Returns(CreateStaticResourceList());

            mockstaticResourceCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<StaticResourceSearchDetail>(), It.IsAny<StaticResourceSortDetail>()))
                .Returns(CreateStaticResourceList());

            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetStaticResource("", "TestMime");

            //Verify and Assert
            ValidateEmptyData<StaticResourceObjectModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            mockstaticResourceCacheFactory.VerifyAll();
        }
        #endregion

        #region GetStaticResource_Result_JsonResult_valid_fileName_MimeType
        ///<summary>
        ////GetStaticResource_Result_JsonResult_EmptyList
        ///</summary>
        [TestMethod]
        public void GetStaticResource_Result_JsonResult_EmptyList()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<StaticResourceSearchDetail>(), It.IsAny<StaticResourceSortDetail>()))
                .Returns(CreateStaticResourceList);

            //Act
            StaticResourceController objStaticResourceController =
                new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.GetStaticResource("File", "mime");

            //Verify and Assert
            mockstaticResourceCacheFactory.VerifyAll();
            ValidateEmptyData<StaticResourceObjectModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckFileName_Result_Json
        ///<summary>
        ///CheckFileName_Result_Json
        ///</summary>
        [TestMethod]
        public void CheckFileName_Result_Json()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<StaticResourceSearchDetail>())).Returns(CreateStaticResourceList());

            //Act
            StaticResourceController objStaticResourceController =
                 new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.CheckFileName("SA");

            //Verify and Asset
            mockstaticResourceCacheFactory.VerifyAll();
            Assert.AreEqual(true, result.Data);
            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }
        #endregion

        #region CheckFileName_Result_Json_null
        ///<summary>
        ///CheckFileName_Result_Json_null
        ///</summary>
        [TestMethod]
        public void CheckFileName_Result_Json_null()
        {
            //Arrange
            mockstaticResourceCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<StaticResourceSearchDetail>())).Returns(CreateStaticResourceList_null());

            //Act
            StaticResourceController objStaticResourceController =
                 new StaticResourceController(mockstaticResourceCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objStaticResourceController.CheckFileName("SA");

            //Verify and Asset
            mockstaticResourceCacheFactory.VerifyAll();
            Assert.AreEqual(true, result.Data);
            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }
        #endregion

    }
}
