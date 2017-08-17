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
using System.Web;
using System.Collections.Specialized;
using System.Web.Routing;
using RRD.FSG.RP.Model.Entities;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for StaticResourceController class
    /// </summary>
    [TestClass]
    public class ClientDocumentControllerTests : BaseTestController<ClientDocumentObjectModel>
    {
        Mock<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>> mockclientDocumentCacheFactory;
        Mock<IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int>> mockclientDocumentTypeCacheFactory;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserCacheFactory;

        [TestInitialize]
        public void TestInitialze()
        {
            mockclientDocumentCacheFactory = new Mock<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>>();
            mockclientDocumentTypeCacheFactory = new Mock<IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int>>();
            mockUserCacheFactory = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();

        }

        #region ReturnLists

        private IEnumerable<ClientDocumentObjectModel> CreateClientDocumentList()
        {
            IEnumerable<ClientDocumentObjectModel> IenumClientDocumentObjectModel = Enumerable.Empty<ClientDocumentObjectModel>();
            List<ClientDocumentObjectModel> lstClientDocumentObjectModel = new List<ClientDocumentObjectModel>();

            ClientDocumentObjectModel objClientDocumentObjectModel = new ClientDocumentObjectModel
            {
                ClientDocumentId = 40,
                ClientDocumentTypeId = 11,
                Description = "Abcg4444",
                MimeType = "image/jpeg",
                IsPrivate = true
            };
            lstClientDocumentObjectModel.Add(objClientDocumentObjectModel);
            IenumClientDocumentObjectModel = lstClientDocumentObjectModel;
            return IenumClientDocumentObjectModel;
        }

        private IEnumerable<ClientDocumentTypeObjectModel> CreateClientDocumentTypeList()
        {
            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = Enumerable.Empty<ClientDocumentTypeObjectModel>();
            List<ClientDocumentTypeObjectModel> lstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            ClientDocumentTypeObjectModel objClientDocumentTypeObjectModel = new ClientDocumentTypeObjectModel
            {
                ClientDocumentTypeId = 11,
                Description = "Test"
            };
            lstClientDocumentTypeObjectModel.Add(objClientDocumentTypeObjectModel);
            IenumClientDocumentTypeObjectModel = lstClientDocumentTypeObjectModel;
            return IenumClientDocumentTypeObjectModel;
        }

        private IEnumerable<UserObjectModel> CreateUserList()
        {
            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;
            return IEnumUser;
        }

        #endregion

        #region Index_Returns_ActionResult
        /// <summary>
        /// Index_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void Index_Returns_ActionResult()
        {
            //Act
            ClientDocumentController objClientDocumentController =
     new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.Index();
            var result1 = result as ViewResult;
            Assert.AreEqual("ClientDocument", result1.ViewName);

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
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
            ClientDocumentController objClientDocumentController =
      new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.List();
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetAllClientDocumentDetails_Action_Calls_ClientDocumentController
        /// <summary>
        /// GetAllClientDocumentDetails_Action_Calls_ClientDocumentController
        /// </summary>
        [TestMethod]
        public void GetAllClientDocumentDetails_Action_Calls_ClientDocumentController()
        {
            //Arrange
            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = Enumerable.Empty<ClientDocumentTypeObjectModel>();
            List<ClientDocumentTypeObjectModel> lstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();

            ClientDocumentTypeObjectModel objClientDocumentTypeObjectModel = new ClientDocumentTypeObjectModel();
            objClientDocumentTypeObjectModel.ClientDocumentTypeId = 1;
            objClientDocumentTypeObjectModel.Description = "TEST";
            //objClientDocumentTypeObjectModel.ModifiedBy = 1;
            lstClientDocumentTypeObjectModel.Add(objClientDocumentTypeObjectModel);
            IenumClientDocumentTypeObjectModel = lstClientDocumentTypeObjectModel;

            ClientDocumentTypeSearchDetail objClientDocumentTypeSearchDetail = new ClientDocumentTypeSearchDetail();
            objClientDocumentTypeSearchDetail.ClientDocumentTypeId = 1;
            //  objClientDocumentTypeSearchDetail.ClientDocumentTypeIdCompare= "TEST";

            ClientDocumentTypeSortDetail objClientDocumentTypeSortDetail = new ClientDocumentTypeSortDetail();
            // objClientDocumentTypeSortDetail.Column = ClientDocumentTypeSortDetail.DocumentTypeName;
            objClientDocumentTypeSortDetail.Order = SortOrder.Descending;

            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;



            //Act
            ClientDocumentController objClientDocumentController =
             new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetAllClientDocumentDetails(" ", "Test000", 1, " ", "true");
            //List<ClientDocumentObjectModel> lstExpected = new List<ClientDocumentObjectModel>
            //{
            //    new ClientDocumentObjectModel
            //    {
            //        ClientDocumentId = 40,
            //        ClientDocumentTypeId = 11,
            //        ClientDocumentTypeName = null,
            //        Name = null,
            //        FileName = null,
            //        MimeType = "image/jpeg",
            //        ContentUri = null
            //    }
            //};
            ValidateEmptyData<ClientDocumentObjectModel>(result);

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClientDocumentDetails_Returns_JsonResult
        /// <summary>
        /// GetAllClientDocumentDetails_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetAllClientDocumentDetails_Returns_JsonResult()
        {
            // Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentSearchDetail>()))
                .Returns(CreateClientDocumentList());
            mockclientDocumentCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentSearchDetail>(), It.IsAny<ClientDocumentSortDetail>()))
                .Returns(CreateClientDocumentList());

            //Act
            ClientDocumentController objClientDocumentController =
        new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetAllClientDocumentDetails("Hydrangeas.jpg", "Test000", 11, "image/jpeg", "true");
            List<ClientDocumentObjectModel> lstExpected = new List<ClientDocumentObjectModel>
            {
                new ClientDocumentObjectModel
                {
                    ClientDocumentId = 40,
                    ClientDocumentTypeId = 11,
                    ClientDocumentTypeName = null,
                    Name = null,
                    FileName = null,
                    MimeType = "image/jpeg",
                    ContentUri = null
                }
            };
            ValidateData(lstExpected, result);

            //Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClientDocumentDetails_Returns_JsonResult_With_Empty_Values
        /// <summary>
        /// GetAllClientDocumentDetails_Returns_JsonResult_With_Empty_Values
        /// </summary>
        [TestMethod]
        public void GetAllClientDocumentDetails_Returns_JsonResult_With_Empty_Values()
        {
            // Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentSearchDetail>()))
                .Returns(CreateClientDocumentList());
            mockclientDocumentCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentSearchDetail>(), It.IsAny<ClientDocumentSortDetail>()))
                .Returns(CreateClientDocumentList());

            //Act
            ClientDocumentController objClientDocumentController =
        new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetAllClientDocumentDetails(null, null, null, null, null);
            List<ClientDocumentObjectModel> lstExpected = new List<ClientDocumentObjectModel>
            {
                new ClientDocumentObjectModel
                {
                    ClientDocumentId = 40,
                    ClientDocumentTypeId = 11,
                    ClientDocumentTypeName = null,
                    Name = null,
                    FileName = null,
                    MimeType = "image/jpeg",
                    ContentUri = null
                }
            };
            ValidateData(lstExpected, result);

            //Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region EditClientDocument_Returns_ActionResult_GetMethod
        /// <summary>
        /// EditClientDocument_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void EditClientDocument_Returns_ActionResult_GetMethod()
        {
            //Arrange

            mockclientDocumentTypeCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentTypeList());
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentList());
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList());

            //Act
            ClientDocumentController objClientDocumentController =
            new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.EditClientDocument(40);
            ClientDocumentViewModel objClientDocumentViewModelExpected = new ClientDocumentViewModel
            {
                DocumentType = new List<DisplayValuePair>
                {
                    new DisplayValuePair {Display = "--Please select Document Type--", Value = "-1"},
                    new DisplayValuePair {Display = null, Value = "11"}
                },
                ClientDocumentId = 40,
                Name = null,
                FileName = null,
                MimeType = "image/jpeg",
                Description = "Abcg4444",
                IsPrivate = true,
                SelectedClientDocumentTypeId = 11,
                UTCLastModifiedDate = "",
                ModifiedByName = "Test_UserName"
            };
            var result1 = result as ViewResult;
            var objClientDocumentViewModelActual = result1.Model as ClientDocumentViewModel;
            ValidateViewModelData<ClientDocumentViewModel>(objClientDocumentViewModelActual,
                objClientDocumentViewModelExpected);

            //Verify and Assert
            mockclientDocumentTypeCacheFactory.VerifyAll();
            mockclientDocumentCacheFactory.VerifyAll();
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClientDocument_Returns_ActionResult_GetMethod_With_Empty_CreateClientDocumentTypeList
        /// <summary>
        /// EditClientDocument_Returns_ActionResult_GetMethod_With_Empty_CreateClientDocumentTypeList
        /// </summary>
        [TestMethod]
        public void EditClientDocument_Returns_ActionResult_GetMethod_With_Empty_CreateClientDocumentTypeList()
        {
            //Arrange
            IEnumerable<ClientDocumentTypeObjectModel> objClientDocumentTypeObjectModel = null;
            mockclientDocumentTypeCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(objClientDocumentTypeObjectModel);
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentList());
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList());

            //Act
            ClientDocumentController objClientDocumentController =
            new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.EditClientDocument(40);
            ClientDocumentViewModel objClientDocumentViewModelExpected = new ClientDocumentViewModel
            {
                DocumentType = new List<DisplayValuePair>
                {
                    new DisplayValuePair {Display = "--Please select Document Type--", Value = "-1"}
                },
                ClientDocumentId = 40,
                Name = null,
                FileName = null,
                MimeType = "image/jpeg",
                Description = "Abcg4444",
                IsPrivate = true,
                SelectedClientDocumentTypeId = 11,
                UTCLastModifiedDate = "",
                ModifiedByName = "Test_UserName"
            };
            var result1 = result as ViewResult;
            var objClientDocumentViewModelActual = result1.Model as ClientDocumentViewModel;
            ValidateViewModelData<ClientDocumentViewModel>(objClientDocumentViewModelActual,
                objClientDocumentViewModelExpected);

            //Verify and Assert
            mockclientDocumentTypeCacheFactory.VerifyAll();
            mockclientDocumentCacheFactory.VerifyAll();
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClientDocument_Returns_ActionResult_GetMethod_With_ID_Equals_To_Zero
        /// <summary>
        /// EditClientDocument_Returns_ActionResult_GetMethod_With_ID_Equals_To_Zero
        /// </summary>
        [TestMethod]
        public void EditClientDocument_Returns_ActionResult_GetMethod_With_ID_Equals_To_Zero()
        {
            //Arrange
            mockclientDocumentTypeCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentTypeList());

            //Act
            ClientDocumentController objClientDocumentController =
            new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.EditClientDocument(0);
            ClientDocumentViewModel objClientDocumentViewModelExpected = new ClientDocumentViewModel
            {
                DocumentType = new List<DisplayValuePair>
                {
                    new DisplayValuePair {Display = "--Please select Document Type--", Value = "-1"},
                    new DisplayValuePair {Display = null, Value = "11"}
                },
                SelectedClientDocumentTypeId = -1
            };
            var result1 = result as ViewResult;
            var objClientDocumentViewModelActual = result1.Model as ClientDocumentViewModel;
            ValidateViewModelData<ClientDocumentViewModel>(objClientDocumentViewModelActual,
                objClientDocumentViewModelExpected);

            //Verify and Assert
            mockclientDocumentTypeCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClientDocument_Returns_ActionResult_GetMethod_With_Empty_CreateUserList
        /// <summary>
        /// EditClientDocument_Returns_ActionResult_GetMethod_With_Empty_CreateUserList
        /// </summary>
        [TestMethod]
        public void EditClientDocument_Returns_ActionResult_GetMethod_With_Empty_CreateUserList()
        {
            //Arrange

            mockclientDocumentTypeCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentTypeList());
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentList());
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(Enumerable.Empty<UserObjectModel>());

            //Act
            ClientDocumentController objClientDocumentController =
            new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.EditClientDocument(40);
            ClientDocumentViewModel objClientDocumentViewModelExpected = new ClientDocumentViewModel
            {
                DocumentType = new List<DisplayValuePair>
                {
                    new DisplayValuePair {Display = "--Please select Document Type--", Value = "-1"},
                    new DisplayValuePair {Display = null, Value = "11"}
                },
                ClientDocumentId = 40,
                Name = null,
                FileName = null,
                MimeType = "image/jpeg",
                Description = "Abcg4444",
                IsPrivate = true,
                SelectedClientDocumentTypeId = 11,
                UTCLastModifiedDate = "",
                ModifiedByName = ""
            };
            var result1 = result as ViewResult;
            var objClientDocumentViewModelActual = result1.Model as ClientDocumentViewModel;
            ValidateViewModelData<ClientDocumentViewModel>(objClientDocumentViewModelActual,
                objClientDocumentViewModelExpected);

            //Verify and Assert
            mockclientDocumentTypeCacheFactory.VerifyAll();
            mockclientDocumentCacheFactory.VerifyAll();
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClientDocument_Returns_ActionResult_PostMethod
        /// <summary>
        /// EditClientDocument_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditClientDocument_Returns_ActionResult_PostMethod()
        {
            //Arrange
            mockclientDocumentTypeCacheFactory.Setup(x => x.GetAllEntities())
               .Returns(CreateClientDocumentTypeList());
            //mockclientDocumentCacheFactory.Setup(x => x.SaveEntity(It.IsAny<ClientDocumentObjectModel>(), It.IsAny<int>()));
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "fileName", "Hydrangeas.jpg" },
               { "mimeType", "image/jpeg" },
               { "ClientDocumentId", "40" }
            };

            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends

            //Act
            ClientDocumentController objClientDocumentController = new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            objClientDocumentController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientDocumentController);
            var result = objClientDocumentController.EditClientDocument(new ClientDocumentViewModel());
            ClientDocumentViewModel objClientDocumentViewModelExpected = new ClientDocumentViewModel
            {
                DocumentType = new List<DisplayValuePair>
                {
                    new DisplayValuePair {Display = "--Please select Document Type--", Value = "-1"},
                    new DisplayValuePair {Display = null, Value = "11"}
                },
                SuccessOrFailedMessage = "Object reference not set to an instance of an object."
            };
            var result1 = result as ViewResult;
            var objClientDocumentViewModelActual = result1.Model as ClientDocumentViewModel;
            ValidateViewModelData<ClientDocumentViewModel>(objClientDocumentViewModelActual,
                objClientDocumentViewModelExpected);

            //Verify and Assert
            mockclientDocumentTypeCacheFactory.VerifyAll();
            //mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClientDocument_Returns_ActionResult_PostMethod_With_Empty_CreateClientDocumentTypeList
        /// <summary>
        /// EditClientDocument_Returns_ActionResult_PostMethod_With_Empty_CreateClientDocumentTypeList
        /// </summary>
        [TestMethod]
        public void EditClientDocument_Returns_ActionResult_PostMethod_With_Empty_CreateClientDocumentTypeList()
        {
            //Arrange
            IEnumerable<ClientDocumentTypeObjectModel> objClientDocumentTypeObjectModel = null;
            mockclientDocumentTypeCacheFactory.Setup(x => x.GetAllEntities())
               .Returns(objClientDocumentTypeObjectModel);
            //mockclientDocumentCacheFactory.Setup(x => x.SaveEntity(It.IsAny<ClientDocumentObjectModel>(), It.IsAny<int>()));
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "fileName", "Hydrangeas.jpg" },
               { "mimeType", "image/jpeg" },
               { "ClientDocumentId", "40" }
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends

            //Act
            ClientDocumentController objClientDocumentController = new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            objClientDocumentController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientDocumentController);
            var result = objClientDocumentController.EditClientDocument(new ClientDocumentViewModel());
            ClientDocumentViewModel objClientDocumentViewModelExpected = new ClientDocumentViewModel
            {
                DocumentType = new List<DisplayValuePair>
                {
                    new DisplayValuePair {Display = "--Please select Document Type--", Value = "-1"}
                },
                SuccessOrFailedMessage = "Object reference not set to an instance of an object."
            };
            var result1 = result as ViewResult;
            var objClientDocumentViewModelActual = result1.Model as ClientDocumentViewModel;
            ValidateViewModelData<ClientDocumentViewModel>(objClientDocumentViewModelActual,
                objClientDocumentViewModelExpected);

            //Verify and Assert
            mockclientDocumentTypeCacheFactory.VerifyAll();
            //mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region DisableClientDocument_Returns_JsonResult
        /// <summary>
        /// DisableClientDocument_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DisableClientDocument_Returns_JsonResult()
        {
            //Arrange

            //mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>()))
            //    .Returns(CreateUserList());

            //Act
            ClientDocumentController objClientDocumentController =
         new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.DeleteClientDocument(40);
            Assert.AreEqual(result.Data, string.Empty);

            //Verify and Assert
            //mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult
        /// <summary>
        /// CheckDataAlreadyExists_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_Returns_JsonResult()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentSearchDetail>()))
            .Returns(CreateClientDocumentList());

            //Act
            ClientDocumentController objClientDocumentController =
          new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.CheckDataAlreadyExists("Test000", "Abcg4444", "43");
            Assert.AreEqual(result.Data, true);

            //Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult_With_Empty_Values
        /// <summary>
        /// CheckDataAlreadyExists_Returns_JsonResult_With_Empty_Values
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_Returns_JsonResult_With_Empty_Values()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentSearchDetail>()))
            .Returns(CreateClientDocumentList());

            //Act
            ClientDocumentController objClientDocumentController =
          new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.CheckDataAlreadyExists(null, null, null);
            Assert.AreEqual(result.Data, true);

            //Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult_With_Empty_CreateClientDocumentList
        /// <summary>
        /// CheckDataAlreadyExists_Returns_JsonResult_With_Empty_CreateClientDocumentList
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_Returns_JsonResult_With_Empty_CreateClientDocumentList()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentSearchDetail>()))
            .Returns(Enumerable.Empty<ClientDocumentObjectModel>());

            //Act
            ClientDocumentController objClientDocumentController =
          new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.CheckDataAlreadyExists("Test000", "Abcg4444", "43");
            Assert.AreEqual(result.Data, false);

            //Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetName_Returns_Jsonresult

        ///<summary>
        /// GetName_Returns_Jsonresult
        /// </summary>
        [TestMethod]
        public void GetName_Returns_Jsonresult()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateClientDocumentList());

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetName();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = null, Value = null}
            };
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetName_Returns_Jsonresult_With_Empty_CreateClientDocumentList

        ///<summary>
        /// GetName_Returns_Jsonresult_With_Empty_CreateClientDocumentList
        /// </summary>
        [TestMethod]
        public void GetName_Returns_Jsonresult_With_Empty_CreateClientDocumentList()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities()).Returns(Enumerable.Empty<ClientDocumentObjectModel>());

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetName();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFileName_Returns_Jsonresult

        ///<summary>
        /// GetFileName_Returns_Jsonresult
        /// </summary>
        [TestMethod]
        public void GetFileName_Returns_Jsonresult()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateClientDocumentList());

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetFileName();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = null, Value = null}
            };
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFileName_Returns_Jsonresult_With_Empty_CreateClientDocumentList

        ///<summary>
        /// GetFileName_Returns_Jsonresult_With_Empty_CreateClientDocumentList
        /// </summary>
        [TestMethod]
        public void GetFileName_Returns_Jsonresult_With_Empty_CreateClientDocumentList()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities()).Returns(Enumerable.Empty<ClientDocumentObjectModel>());

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetFileName();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetClientDocumentTypeName_Returns_Jsonresult

        ///<summary>
        /// GetClientDocumentTypeName_Returns_Jsonresult
        /// </summary>
        [TestMethod]
        public void GetClientDocumentTypeName_Returns_Jsonresult()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentList());
            mockclientDocumentTypeCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentTypeList);

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetClientDocumentTypeName();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = null, Value = "11"}
            };
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            mockclientDocumentTypeCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetClientDocumentTypeName_Returns_Jsonresult_With_Empty_CreateClientDocumentList

        ///<summary>
        /// GetClientDocumentTypeName_Returns_Jsonresult_With_Empty_CreateClientDocumentList
        /// </summary>
        [TestMethod]
        public void GetClientDocumentTypeName_Returns_Jsonresult_With_Empty_CreateClientDocumentList()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(Enumerable.Empty<ClientDocumentObjectModel>());

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetClientDocumentTypeName();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetMimeType_Returns_Jsonresult

        ///<summary>
        /// GetMimeType_Returns_Jsonresult
        /// </summary>
        [TestMethod]
        public void GetMimeType_Returns_Jsonresult()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateClientDocumentList());

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetMimeType();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = "image/jpeg", Value = "image/jpeg"}
            };
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetMimeType_Returns_Jsonresult_With_Empty_CreateClientDocumentList

        ///<summary>
        /// GetMimeType_Returns_Jsonresult_With_Empty_CreateClientDocumentList
        /// </summary>
        [TestMethod]
        public void GetMimeType_Returns_Jsonresult_With_Empty_CreateClientDocumentList()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities()).Returns(Enumerable.Empty<ClientDocumentObjectModel>());

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetMimeType();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetIsPrivate_Returns_Jsonresult

        ///<summary>
        /// GetIsPrivate_Returns_Jsonresult
        /// </summary>
        [TestMethod]
        public void GetIsPrivate_Returns_Jsonresult()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateClientDocumentList());

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetIsPrivate();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = "True", Value = "True"}
            };
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetIsPrivate_Returns_Jsonresult_With_Empty_CreateClientDocumentList

        ///<summary>
        /// GetIsPrivate_Returns_Jsonresult_With_Empty_CreateClientDocumentList
        /// </summary>
        [TestMethod]
        public void GetIsPrivate_Returns_Jsonresult_With_Empty_CreateClientDocumentList()
        {
            //Arrange
            mockclientDocumentCacheFactory.Setup(x => x.GetAllEntities()).Returns(Enumerable.Empty<ClientDocumentObjectModel>());

            //Act
            ClientDocumentController objClientDocumentController =
           new ClientDocumentController(mockclientDocumentCacheFactory.Object, mockclientDocumentTypeCacheFactory.Object, mockUserCacheFactory.Object);
            var result = objClientDocumentController.GetIsPrivate();
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockclientDocumentCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

    }

}
