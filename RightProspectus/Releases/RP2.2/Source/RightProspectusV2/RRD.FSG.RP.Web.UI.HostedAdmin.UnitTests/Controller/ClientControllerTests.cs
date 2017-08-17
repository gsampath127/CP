using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RRD.FSG.RP.Model.Cache.VerticalMarket;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Entities.System;
using Moq;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using RRD.FSG.RP.Model.Keys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Utilities;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    [TestClass]
    public class ClientControllerTests : BaseTestController<ClientObjectModel>
    {
        Mock<IFactoryCache<ClientFactory, ClientObjectModel, int>> clientCacheFactory;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> userCacheFactory;
        Mock<IFactoryCache<VerticalMarketsFactory, VerticalMarketsObjectModel, int>> verticalMarketsCacheFactory;
        Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>> siteCacheFactory;

        [TestInitialize]
        public void TestInitialze()
        {
            clientCacheFactory = new Mock<IFactoryCache<ClientFactory, ClientObjectModel, int>>();
            userCacheFactory = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            verticalMarketsCacheFactory = new Mock<IFactoryCache<VerticalMarketsFactory, VerticalMarketsObjectModel, int>>();
            siteCacheFactory = new Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>>();

        }

        #region ReturnLists

        private IEnumerable<ClientObjectModel> CreateClientList()
        {
            IEnumerable<ClientObjectModel> IenumClientObjectModel = Enumerable.Empty<ClientObjectModel>();
            List<ClientObjectModel> lstClientObjectModel = new List<ClientObjectModel>();

            ClientObjectModel objClientObjectModel = new ClientObjectModel();
            objClientObjectModel.ClientID = 1;
            objClientObjectModel.ClientDescription = "Test_Doc";
            objClientObjectModel.ClientName = "RP";
            objClientObjectModel.ClientDatabaseName = "RPDB";
            objClientObjectModel.ClientConnectionStringName = "Test_conn";
            objClientObjectModel.VerticalMarketId = 3;
            lstClientObjectModel.Add(objClientObjectModel);
            IenumClientObjectModel = lstClientObjectModel;
            return IenumClientObjectModel;
        }

        private IEnumerable<VerticalMarketsObjectModel> CreateVerticalMarketList()
        {
            IEnumerable<VerticalMarketsObjectModel> IenumVerticalMarketObjectModel = Enumerable.Empty<VerticalMarketsObjectModel>();
            List<VerticalMarketsObjectModel> lstVerticalMarketObjectModel = new List<VerticalMarketsObjectModel>();

            VerticalMarketsObjectModel objVerticalMarketObjectModel = new VerticalMarketsObjectModel();
            objVerticalMarketObjectModel.VerticalMarketId = 1;
            objVerticalMarketObjectModel.MarketDescription = "Test_Doc";
            lstVerticalMarketObjectModel.Add(objVerticalMarketObjectModel);
            IenumVerticalMarketObjectModel = lstVerticalMarketObjectModel;
            return IenumVerticalMarketObjectModel;
        }

        private IEnumerable<UserObjectModel> CreateUserList()
        {
            IEnumerable<UserObjectModel> IenumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel
            {
                UserId = 2,
                Email = "abc",
                FirstName = "RP"
            };
            lstUserObjectModel.Add(objUserObjectModel);
            IenumUserObjectModel = lstUserObjectModel;
            return IenumUserObjectModel;
        }

        private IEnumerable<SiteObjectModel> CreateSiteList()
        {
            IEnumerable<SiteObjectModel> IenumSiteObjectModel = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lstSiteObjectModel = new List<SiteObjectModel>();
            SiteObjectModel objSiteObjectModel = new SiteObjectModel
            {
                SiteID = 1,
                Name = "Forethought",
                TemplateId = 1
            };
            lstSiteObjectModel.Add(objSiteObjectModel);
            IenumSiteObjectModel = lstSiteObjectModel;
            return IenumSiteObjectModel;
        }

        #endregion

        #region List_Returns_ActionResult
        /// <summary>
        ///List_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void List_Returns_ActionResult()
        {
            //Act
            ClientController objClientController =
        new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.List();
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClient_Returns_ActionResult_GetMethod_EqualsToZero
        /// <summary>
        /// EditClient_Returns_ActionResult_GetMethod_EqualsToZero
        /// </summary>
        [TestMethod]
        public void EditClient_Returns_ActionResult_EqualsToZero_GetMethod()
        {
            //Arrange            
            // clientCacheFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>()));


            verticalMarketsCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalMarketList());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.EditClient(0);
            ClientViewModel objClientViewModelExpected = new ClientViewModel
            {
                ClientID = 0,
                ClientConnectionStringNames = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Client Connection String Name--", Value = "-1" },
                    new DisplayValuePair {Display= "ClientDBInstance1", Value = "ClientDBInstance1"}
                },
                VerticalMarket = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Vertical Market--", Value = "-1" },
                    new DisplayValuePair {Display= null, Value = "1"}
                }
            };
            var result1 = result as ViewResult;
            var objClientViewModelActual = result1.Model as ClientViewModel;
            ValidateViewModelData<ClientViewModel>(objClientViewModelActual, objClientViewModelExpected);
            
            //Verify and Assert
            //  clientCacheFactory.VerifyAll();
            verticalMarketsCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClient_Returns_ActionResult_GetMethod_GreaterThanZero
        /// <summary>
        /// EditClient_Returns_ActionResult_GetMethod_GreaterThanZero
        /// </summary>
        [TestMethod]
        public void EditClient_Returns_ActionResult_GreaterThanZero_GetMethod()
        {
            ClientObjectModel objClientObjectModel = new ClientObjectModel();
            objClientObjectModel.ClientID = 1;
            objClientObjectModel.ClientDescription = "Test_Doc";
            objClientObjectModel.ClientName = "RP";
            objClientObjectModel.ClientDatabaseName = "RPDB";
            objClientObjectModel.ClientConnectionStringName = "Test_conn";
            objClientObjectModel.VerticalMarketId = 3;


            //Arrange            
            clientCacheFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objClientObjectModel);


            verticalMarketsCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalMarketList());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.EditClient(1);
            ClientViewModel objClientViewModelExpected = new ClientViewModel
            {
                ClientID = 1,
                ClientName = "RP",
                SelectedVerticalMarketId = 3,
                ClientDatabaseName = "RPDB",
                SelectedClientConnectionStringName = "Test_conn",
                ClientDescription = "Test_Doc",
                ClientConnectionStringNames = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Client Connection String Name--", Value = "-1" },
                    new DisplayValuePair {Display= "ClientDBInstance1", Value = "ClientDBInstance1"}
                },
                VerticalMarket = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Vertical Market--", Value = "-1" },
                    new DisplayValuePair {Display= null, Value = "1"}
                }
            };
            var result1 = result as ViewResult;
            var objClientViewModelActual = result1.Model as ClientViewModel;
            ValidateViewModelData<ClientViewModel>(objClientViewModelActual, objClientViewModelExpected);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            verticalMarketsCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClient_Returns_ActionResult_PostMethod
        /// <summary>
        /// EditClient_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditClient_Returns_ActionResult_PostMethod()
        {
            //Arrange
            verticalMarketsCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalMarketList());

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnUsers", "1,2,3"}               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends
            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            objClientController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientController);
            var result = objClientController.EditClient(new ClientViewModel() { ClientID = 1, ClientDescription = "Test_2", ModifiedBy = null }, 1);
            ClientViewModel objClientViewModelExpected = new ClientViewModel
            {
                ClientID = 1,
                ClientDescription = "Test_2",
                ModifiedBy = null,
                ClientConnectionStringNames = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Client Connection String Name--", Value = "-1" },
                    new DisplayValuePair {Display= "ClientDBInstance1", Value = "ClientDBInstance1"}
                },
                VerticalMarket = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Vertical Market--", Value = "-1" },
                    new DisplayValuePair {Display= null, Value = "1"}
                }
            };
            var result1 = result as ViewResult;
            var objClientViewModelActual = result1.Model as ClientViewModel;
            ValidateViewModelData<ClientViewModel>(objClientViewModelActual, objClientViewModelExpected);

            //Verify and Assert
            verticalMarketsCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClient_Returns_ActionResult_PostMethod_With_Empty_CreateVerticalMarketList
        /// <summary>
        /// EditClient_Returns_ActionResult_PostMethod_With_Empty_CreateVerticalMarketList
        /// </summary>
        [TestMethod]
        public void EditClient_Returns_ActionResult_PostMethod_With_Empty_CreateVerticalMarketList()
        {
            //Arrange
            verticalMarketsCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(Enumerable.Empty<VerticalMarketsObjectModel>());

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnUsers", "1,2,3"}               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends
            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            objClientController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientController);
            var result = objClientController.EditClient(new ClientViewModel() { ClientID = 1, ClientDescription = "Test_2", ModifiedBy = null }, 1);
            ClientViewModel objClientViewModelExpected = new ClientViewModel
            {
                ClientID = 1,
                ClientDescription = "Test_2",
                ModifiedBy = null,
                ClientConnectionStringNames = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Client Connection String Name--", Value = "-1" },
                    new DisplayValuePair {Display= "ClientDBInstance1", Value = "ClientDBInstance1"}
                },
                VerticalMarket = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Vertical Market--", Value = "-1" }
                }
            };
            var result1 = result as ViewResult;
            var objClientViewModelActual = result1.Model as ClientViewModel;
            ValidateViewModelData<ClientViewModel>(objClientViewModelActual, objClientViewModelExpected);

            //Verify and Assert
            verticalMarketsCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClient_Returns_ActionResult_PostMethod_With_Null_RequestForm_hdnUsers
        /// <summary>
        /// EditClient_Returns_ActionResult_PostMethod_With_Null_RequestForm_hdnUsers
        /// </summary>
        [TestMethod]
        public void EditClient_Returns_ActionResult_PostMethod_With_Null_RequestForm_hdnUsers()
        {
            //Arrange
            verticalMarketsCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalMarketList());

            ClientViewModel obj = new ClientViewModel();
            obj.ClientID = 1;
            obj.ClientDescription = "Test_2";
            obj.SelectedVerticalMarketId = 1;
            obj.UTCLastModifiedDate = DateTime.Now;
            obj.SuccessOrFailedMessage = string.Empty;
            obj.VerticalMarketName = "US";

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnUsers", null }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends
            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            objClientController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientController);
            var result = objClientController.EditClient(new ClientViewModel() { ClientID = 1, ClientDescription = "Test_2", ModifiedBy = null }, 1);
            ClientViewModel objClientViewModelExpected = new ClientViewModel
            {
                ClientID = 1,
                ClientDescription = "Test_2",
                ModifiedBy = null,
                ClientConnectionStringNames = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Client Connection String Name--", Value = "-1" },
                    new DisplayValuePair {Display= "ClientDBInstance1", Value = "ClientDBInstance1"}
                },
                VerticalMarket = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Vertical Market--", Value = "-1" },
                    new DisplayValuePair {Display= null, Value = "1"}
                }
            };
            var result1 = result as ViewResult;
            var objClientViewModelActual = result1.Model as ClientViewModel;
            ValidateViewModelData<ClientViewModel>(objClientViewModelActual, objClientViewModelExpected);

            //Verify and Assert
            verticalMarketsCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClient_Returns_ActionResult_PostMethod_With_Empty_RequestForm_hdnUsers
        /// <summary>
        /// EditClient_Returns_ActionResult_PostMethod_With_Empty_RequestForm_hdnUsers
        /// </summary>
        [TestMethod]
        public void EditClient_Returns_ActionResult_PostMethod_With_Empty_RequestForm_hdnUsers()
        {
            //Arrange
            verticalMarketsCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalMarketList());

            ClientViewModel obj = new ClientViewModel();
            obj.ClientID = 1;
            obj.ClientDescription = "Test_2";
            obj.SelectedVerticalMarketId = 1;
            obj.UTCLastModifiedDate = DateTime.Now;
            obj.SuccessOrFailedMessage = string.Empty;
            obj.VerticalMarketName = "US";

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnUsers", "" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends
            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            objClientController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientController);
            var result = objClientController.EditClient(new ClientViewModel() { ClientID = 1, ClientDescription = "Test_2", ModifiedBy = null }, 1);
            ClientViewModel objClientViewModelExpected = new ClientViewModel
            {
                ClientID = 1,
                ClientDescription = "Test_2",
                ModifiedBy = null,
                ClientConnectionStringNames = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Client Connection String Name--", Value = "-1" },
                    new DisplayValuePair {Display= "ClientDBInstance1", Value = "ClientDBInstance1"}
                },
                VerticalMarket = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Vertical Market--", Value = "-1" },
                    new DisplayValuePair {Display= null, Value = "1"}
                }
            };
            var result1 = result as ViewResult;
            var objClientViewModelActual = result1.Model as ClientViewModel;
            ValidateViewModelData<ClientViewModel>(objClientViewModelActual, objClientViewModelExpected);

            //Verify and Assert
            verticalMarketsCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClient_Returns_ActionResult_PostMethod_With_LargeValue_RequestForm_hdnUsers
        /// <summary>
        /// EditClient_Returns_ActionResult_PostMethod_With_LargeValue_RequestForm_hdnUsers
        /// </summary>
        [TestMethod]
        public void EditClient_Returns_ActionResult_PostMethod_With_LargeValue_RequestForm_hdnUsers()
        {
            //Arrange
            verticalMarketsCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalMarketList());

            ClientViewModel obj = new ClientViewModel();
            obj.ClientID = 1;
            obj.ClientDescription = "Test_2";
            obj.SelectedVerticalMarketId = 1;
            obj.UTCLastModifiedDate = DateTime.Now;
            obj.SuccessOrFailedMessage = string.Empty;
            obj.VerticalMarketName = "US";

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnUsers", "12345678901234567890" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends
            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            objClientController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientController);
            var result = objClientController.EditClient(new ClientViewModel() { ClientID = 1, ClientDescription = "Test_2", ModifiedBy = null }, 1);
            ClientViewModel objClientViewModelExpected = new ClientViewModel
            {
                ClientID = 1,
                ClientDescription = "Test_2",
                ModifiedBy = null,
                ClientConnectionStringNames = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Client Connection String Name--", Value = "-1" },
                    new DisplayValuePair {Display= "ClientDBInstance1", Value = "ClientDBInstance1"}
                },
                VerticalMarket = new List<DisplayValuePair>
                {
                    new DisplayValuePair { Display = "--Please select Vertical Market--", Value = "-1" },
                    new DisplayValuePair {Display= null, Value = "1"}
                }
            };
            var result1 = result as ViewResult;
            var objClientViewModelActual = result1.Model as ClientViewModel;
            ValidateViewModelData<ClientViewModel>(objClientViewModelActual, objClientViewModelExpected);

            //Verify and Assert
            verticalMarketsCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region WelcomeClient_With_clientId_clientName_Returns_ActionResult
        /// <summary>
        /// WelcomeClient_With_clientId_clientName_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void WelcomeClient_With_clientId_clientName_Returns_ActionResult()
        {
            //Arrange
            siteCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateSiteList());
            //Mocking session value - starts
            long sessionValue = 1;
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupSet(x => x.Session["SITE_ID"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                httpContext.SetupGet(x => x.Session["SITE_ID"]).Returns(sessionValue);
            });
            //Mocking session value - ends

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);

            //Mocking the context of controller
            objClientController.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), objClientController);
            var result = objClientController.WelcomeClient(2, "Forethought");
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            
            //Verify and Assert
            siteCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region WelcomeClient_With_clientId_clientName_Returns_ActionResult_With_Null_ClientId
        /// <summary>
        /// WelcomeClient_With_clientId_clientName_Returns_ActionResult_With_Null_ClientId
        /// </summary>
        [TestMethod]
        public void WelcomeClient_With_clientId_clientName_Returns_ActionResult_With_Null_ClientId()
        {
            //Arrange
            siteCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateSiteList());
            //Mocking session value - starts
            long sessionValue = 1;
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupSet(x => x.Session["SITE_ID"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                httpContext.SetupGet(x => x.Session["SITE_ID"]).Returns(sessionValue);
            });
            //Mocking session value - ends

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);

            //Mocking the context of controller
            objClientController.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), objClientController);
            var result = objClientController.WelcomeClient(null, "Forethought");
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            //Verify and Assert
            siteCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region WelcomeClient_With_clientId_clientName_Returns_ActionResult_With_Empty_CreateSiteList
        /// <summary>
        /// WelcomeClient_With_clientId_clientName_Returns_ActionResult_With_Empty_CreateSiteList
        /// </summary>
        [TestMethod]
        public void WelcomeClient_With_clientId_clientName_Returns_ActionResult_With_Empty_CreateSiteList()
        {
            //Arrange
            siteCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(Enumerable.Empty<SiteObjectModel>());
            //Mocking session value - starts
            long sessionValue = 1;
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupSet(x => x.Session["SITE_ID"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                httpContext.SetupGet(x => x.Session["SITE_ID"]).Returns(sessionValue);
            });
            //Mocking session value - ends

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);

            //Mocking the context of controller
            objClientController.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), objClientController);
            var result = objClientController.WelcomeClient(2, "Forethought");
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            //Verify and Assert
            siteCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetClientUsers_Returns_JsonResult_With_clientID_Greater_Than_Zero_And_Matching_UserId

        /// <summary>
        /// GetClientUsers_Returns_JsonResult_With_clientID_Greater_Than_Zero_And_Matching_UserId
        /// </summary>
        [TestMethod]
        public void GetClientUsers_Returns_JsonResult_With_clientID_Greater_Than_Zero_And_Matching_UserId()
        {
            ClientObjectModel objClientObjectModel = new ClientObjectModel
            {
                ClientID = 2,
                ClientName = "gkgkj",
                ClientDescription = "qwerty",
                Users = new List<int> { 2 }
            };
            userCacheFactory.Setup(x => x.GetAllEntities())
              .Returns(CreateUserList());

            clientCacheFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objClientObjectModel);

            //Act
            ClientController objClientController = new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetClientUsers(2);
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = "abc", Value = "2", Selected = true}
            };
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            userCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetClientUsers_Returns_JsonResult_With_clientID_Greater_Than_Zero_And_Not_Matching_UserId

        /// <summary>
        /// GetClientUsers_Returns_JsonResult_With_clientID_Greater_Than_Zero_And_Not_Matching_UserId
        /// </summary>
        [TestMethod]
        public void GetClientUsers_Returns_JsonResult_With_clientID_Greater_Than_Zero_And_Not_Matching_UserId()
        {
            ClientObjectModel objClientObjectModel = new ClientObjectModel
            {
                ClientID = 2,
                ClientName = "gkgkj",
                ClientDescription = "qwerty",
                Users = new List<int> { 3 }
            };
            userCacheFactory.Setup(x => x.GetAllEntities())
              .Returns(CreateUserList());

            clientCacheFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objClientObjectModel);

            //Act
            ClientController objClientController = new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetClientUsers(2);
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = "abc", Value = "2", Selected = false}
            };
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            userCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetClientUsers_Returns_JsonResult_With_clientID_Less_Than_Zero

        /// <summary>
        /// GetClientUsers_Returns_JsonResult_With_clientID_Less_Than_Zero
        /// </summary>
        [TestMethod]
        public void GetClientUsers_Returns_JsonResult_With_clientID_Less_Than_Zero()
        {
            ClientObjectModel objClientObjectModel = new ClientObjectModel
            {
                ClientID = -2,
                ClientName = "gkgkj",
                ClientDescription = "qwerty",
                Users = new List<int> { 3 }
            };
            userCacheFactory.Setup(x => x.GetAllEntities())
              .Returns(CreateUserList());

            clientCacheFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objClientObjectModel);

            //Act
            ClientController objClientController = new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetClientUsers(-2);
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = "abc", Value = "2", Selected = false}
            };
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            userCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        
        #region GetClientNames_Returns_JsonResult

        /// <summary>
        /// GetClientNames__Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetClientNames_Returns_JsonResult()
        {
            clientCacheFactory.Setup(x => x.GetAllEntities())
              .Returns(CreateClientList());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetClientNames();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = "RP", Value = "RP"}
            };
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetClientNames_Returns_JsonResult_With_Empty_CreateClientList

        /// <summary>
        /// GetClientNames_Returns_JsonResult_With_Empty_CreateClientList
        /// </summary>
        [TestMethod]
        public void GetClientNames_Returns_JsonResult_With_Empty_CreateClientList()
        {
            clientCacheFactory.Setup(x => x.GetAllEntities())
              .Returns(Enumerable.Empty<ClientObjectModel>());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetClientNames();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetVerticalMarkets_Returns_JsonResult
        /// <summary>
        /// GetVerticalMarkets__Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetVerticalMarkets_Returns_JsonResult()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetAllEntities())
              .Returns(CreateClientList());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetVerticalMarkets();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = null, Value = "3"}
            };
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetVerticalMarkets_Returns_JsonResult_With_Empty_CreateClientList
        /// <summary>
        /// GetVerticalMarkets_Returns_JsonResult_With_Empty_CreateClientList
        /// </summary>
        [TestMethod]
        public void GetVerticalMarkets_Returns_JsonResult_With_Empty_CreateClientList()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetAllEntities())
              .Returns(Enumerable.Empty<ClientObjectModel>());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetVerticalMarkets();

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetDatabaseNames__Returns_JsonResult
        /// <summary>
        /// GetDatabaseNames__Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetDatabaseNames_Returns_JsonResult()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetAllEntities())
              .Returns(CreateClientList());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetDatabaseNames();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = "RPDB"}
            };
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetDatabaseNames_Returns_JsonResult_With_Empty_CreateClientList
        /// <summary>
        /// GetDatabaseNames_Returns_JsonResult_With_Empty_CreateClientList
        /// </summary>
        [TestMethod]
        public void GetDatabaseNames_Returns_JsonResult_With_Empty_CreateClientList()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetAllEntities())
              .Returns(Enumerable.Empty<ClientObjectModel>());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetDatabaseNames();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region DeleteClientDetails_Returns_JsonResult
        /// <summary>
        /// DeleteClientDetails__Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DeleteClientDetails_Returns_JsonResult()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.DeleteEntity(It.IsAny<int>(), It.IsAny<int>()));

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.DeleteClientDetails(1);
            Assert.AreEqual(string.Empty, result.Data);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckClientNameAlreadyExists__Returns_JsonResult
        /// <summary>
        /// CheckClientNameAlreadyExists__Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckClientNameAlreadyExists_Returns_JsonResult()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientSearchDetail>()))
                .Returns(CreateClientList());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.CheckClientNameAlreadyExists("RS");
            Assert.AreEqual(result.Data, true);
            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckClientNameAlreadyExists_Returns_JsonResult_With_Empty_CreateClientList
        /// <summary>
        /// CheckClientNameAlreadyExists_Returns_JsonResult_With_Empty_CreateClientList
        /// </summary>
        [TestMethod]
        public void CheckClientNameAlreadyExists_Returns_JsonResult_With_Empty_CreateClientList()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch( It.IsAny<ClientSearchDetail>()))
                .Returns(Enumerable.Empty<ClientObjectModel>());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.CheckClientNameAlreadyExists("RS");
            Assert.AreEqual(result.Data, false);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClients_Returns_JsonResult
        /// <summary>
        /// GetAllClients__Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetAllClients_Returns_JsonResult()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientSearchDetail>()))
                .Returns(CreateClientList());
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientSearchDetail>(),It.IsAny<ClientSortDetail>()))
                .Returns(CreateClientList());
            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetAllClients("rs", "1", "test_db");
            List<ClientObjectModel> lstExpected = new List<ClientObjectModel>
            {
                new ClientObjectModel
                {
                    ClientID = 1,
                    ClientName = "RP",
                    VerticalMarketName = null,
                    ClientDatabaseName = "RPDB"
                }
            };
            ValidateData(lstExpected,result);
            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClients_Returns_JsonResult_With_Empty_CreateClientList
        /// <summary>
        /// GetAllClients_Returns_JsonResult_With_Empty_CreateClientList
        /// </summary>
        [TestMethod]
        public void GetAllClients_Returns_JsonResult_With_Empty_CreateClientList()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch( It.IsAny<ClientSearchDetail>()))
                .Returns(Enumerable.Empty<ClientObjectModel>());
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientSearchDetail>(), It.IsAny<ClientSortDetail>()))
                .Returns(Enumerable.Empty<ClientObjectModel>());
            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetAllClients("rs", "1", "test_db");
            List<ClientObjectModel> lstExpected = new List<ClientObjectModel>();
            ValidateEmptyData<ClientObjectModel>(result);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClients_Returns_JsonResult_With_Empty_Values
        /// <summary>
        /// GetAllClients_Returns_JsonResult_With_Empty_Values
        /// </summary>
        [TestMethod]
        public void GetAllClients_Returns_JsonResult_With_Empty_Values()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch( It.IsAny<ClientSearchDetail>()))
                .Returns(CreateClientList());
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientSearchDetail>(), It.IsAny<ClientSortDetail>()))
                .Returns(CreateClientList());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetAllClients(null, null, null);
            List<ClientObjectModel> lstExpected = new List<ClientObjectModel>
            {
                new ClientObjectModel
                {
                    ClientID = 1,
                    ClientName = "RP",
                    VerticalMarketName = null,
                    ClientDatabaseName = "RPDB"
                }
            };
            ValidateData(lstExpected, result);

            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClients_Returns_JsonResult_With_LargeValue_VerticalMarket
        /// <summary>
        /// GetAllClients_Returns_JsonResult_With_LargeValue_VerticalMarket
        /// </summary>
        [TestMethod]
        public void GetAllClients_Returns_JsonResult_With_LargeValue_VerticalMarket()
        {
            //Arrange
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch( It.IsAny<ClientSearchDetail>()))
                .Returns(CreateClientList());
            clientCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientSearchDetail>(), It.IsAny<ClientSortDetail>()))
                .Returns(CreateClientList());

            //Act
            ClientController objClientController =
         new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetAllClients("rs", "123456789901234567890", "test_db");
            List<ClientObjectModel> lstExpected = new List<ClientObjectModel>
            {
                new ClientObjectModel
                {
                    ClientID = 1,
                    ClientName = "RP",
                    VerticalMarketName = null,
                    ClientDatabaseName = "RPDB"
                }
            };
            ValidateData(lstExpected, result);
            //Verify and Assert
            clientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllConnectionStringsFromConfig_Returns_JsonResult
        /// <summary>
        /// GetAllConnectionStringsFromConfig_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetAllConnectionStringsFromConfig_Returns_JsonResult()
        {
            //Arrange

            //Act
            ClientController objClientController = new ClientController(clientCacheFactory.Object, userCacheFactory.Object, verticalMarketsCacheFactory.Object, siteCacheFactory.Object);
            var result = objClientController.GetAllConnectionStringsFromConfig();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display= "ClientDBInstance1"}
            };
            ValidateDisplayValuePair(lstExpected, result);
            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

    }
}
