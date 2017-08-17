using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using System.Web.Script.Serialization;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for ClientDocumentTypeController class
    /// </summary>
    [TestClass]
    public class ClientDocumentTypeControllerTests : BaseTestController<ClientDocumentTypeViewModel>
    {
        Mock<IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int>> mockClientDocTypeFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;
        Mock<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>> mockClientDocumentFactoryCache;

        [TestInitialize]
        public void TestInitialize()
        {
            mockClientDocTypeFactoryCache = new Mock<IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mockClientDocumentFactoryCache = new Mock<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>>();
        }

        #region ReturnValues
        private IEnumerable<ClientDocumentTypeObjectModel> CreateClientDocTypeList()
        {
            IEnumerable<ClientDocumentTypeObjectModel> IEnumClientDocumentType = Enumerable.Empty<ClientDocumentTypeObjectModel>();
            List<ClientDocumentTypeObjectModel> lstClientDocType = new List<ClientDocumentTypeObjectModel>();
            ClientDocumentTypeObjectModel objClientDocType = new ClientDocumentTypeObjectModel();
            objClientDocType.Name = "Test_Doc";
            objClientDocType.Description = "Test_Doc";
            objClientDocType.ClientDocumentTypeId = 1;

            lstClientDocType.Add(objClientDocType);
            IEnumClientDocumentType = lstClientDocType;
            return IEnumClientDocumentType;
        }
        private IEnumerable<ClientDocumentObjectModel> CreateClientDocumentObjectModel()
        {
            IEnumerable<ClientDocumentObjectModel> IEnumClientDocumentObjectModel = Enumerable.Empty<ClientDocumentObjectModel>();
            List<ClientDocumentObjectModel> lstClientDocumentObjectModel = new List<ClientDocumentObjectModel>();
            ClientDocumentObjectModel objClientDocumentObjectModel = new ClientDocumentObjectModel();
            objClientDocumentObjectModel.Name = "Test_Doc";
            objClientDocumentObjectModel.Description = "Test_Dsc";
            objClientDocumentObjectModel.ClientDocumentTypeId = 1;

            lstClientDocumentObjectModel.Add(objClientDocumentObjectModel);
            IEnumClientDocumentObjectModel = lstClientDocumentObjectModel;
            return IEnumClientDocumentObjectModel;
        }
        private IEnumerable<UserObjectModel> CreateUserList()
        {
            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            objUserObjectModel.FirstName = "Test_FirstName";
            objUserObjectModel.LastName = "Test_LastName";
            objUserObjectModel.Email = "Test_email@gmail.com";

            List<RolesObjectModel> lstRoles = new List<RolesObjectModel>();
            RolesObjectModel objRoles = new RolesObjectModel();
            objRoles.RoleName = "Admin";
            lstRoles.Add(objRoles);

            objUserObjectModel.Roles = lstRoles;
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;
            return IEnumUser;
        }
        #endregion

        #region List_Returns_ActionResult
        /// <summary>
        /// List_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void List_Returns_ActionResult()
        {
            //Arrange

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.List();
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetAllClientDocumentTypeDetails_Returns_JsonResult_Empty
        /// <summary>
        /// GetAllClientDocumentTypeDetails_Returns_JsonResult_Empty
        /// </summary>
        [TestMethod]
        public void GetAllClientDocumentTypeDetails_Returns_JsonResult_Empty()
        {
            //Arrange
            List<ClientDocumentTypeObjectModel> LstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = LstClientDocumentTypeObjectModel;
            //mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>()))
               // .Returns(IenumClientDocumentTypeObjectModel);
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>(), It.IsAny<ClientDocumentTypeSortDetail>()))
                .Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.GetAllClientDocumentTypeDetails(string.Empty, string.Empty);
             ValidateEmptyData<ClientDocumentTypeObjectModel>(result);
            // Verify and Assert
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClientDocumentTypeDetails_Returns_JsonResult_EmptyName
        /// <summary>
        /// GetAllClientDocumentTypeDetails_Returns_JsonResult_EmptyName
        /// </summary>
        [TestMethod]
        public void GetAllClientDocumentTypeDetails_Returns_JsonResult_EmptyName()
        {
            //Arrange

            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = Enumerable.Empty<ClientDocumentTypeObjectModel>();
            List<ClientDocumentTypeObjectModel> LstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            ClientDocumentTypeObjectModel objClientDocumentTypeObjectModel = new ClientDocumentTypeObjectModel();
            objClientDocumentTypeObjectModel.Description = "Test";
            LstClientDocumentTypeObjectModel.Add(objClientDocumentTypeObjectModel);
            IenumClientDocumentTypeObjectModel = LstClientDocumentTypeObjectModel;

            //mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>()))
            //    .Returns(IenumClientDocumentTypeObjectModel);
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>(), It.IsAny<ClientDocumentTypeSortDetail>()))
                .Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.GetAllClientDocumentTypeDetails(string.Empty, "Test");

            // Verify and Assert
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClientDocumentTypeDetails_Returns_JsonResult_StringName
        /// <summary>
        /// GetAllClientDocumentTypeDetails_Returns_JsonResult_StringName
        /// </summary>
        [TestMethod]
        public void GetAllClientDocumentTypeDetails_Returns_JsonResult_StringName()
        {
            //Arrange

            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = Enumerable.Empty<ClientDocumentTypeObjectModel>();
            List<ClientDocumentTypeObjectModel> LstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            ClientDocumentTypeObjectModel objClientDocumentTypeObjectModel = new ClientDocumentTypeObjectModel();
            objClientDocumentTypeObjectModel.Description = "Test";
            LstClientDocumentTypeObjectModel.Add(objClientDocumentTypeObjectModel);
            IenumClientDocumentTypeObjectModel = LstClientDocumentTypeObjectModel;

            //mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>()))
            //    .Returns(IenumClientDocumentTypeObjectModel);
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>(), It.IsAny<ClientDocumentTypeSortDetail>()))
                .Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.GetAllClientDocumentTypeDetails("Test", "Test");

            // Verify and Assert
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClientDocumentTypeDetails_Returns_JsonResult_EmptyDescription
        /// <summary>
        /// GetAllClientDocumentTypeDetails_Returns_JsonResult_EmptyDescription
        /// </summary>
        [TestMethod]
        public void GetAllClientDocumentTypeDetails_Returns_JsonResult_EmptyDescription()
        {
            //Arrange

            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = Enumerable.Empty<ClientDocumentTypeObjectModel>();
            List<ClientDocumentTypeObjectModel> LstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            ClientDocumentTypeObjectModel objClientDocumentTypeObjectModel = new ClientDocumentTypeObjectModel();
            objClientDocumentTypeObjectModel.Description = "Test";
            LstClientDocumentTypeObjectModel.Add(objClientDocumentTypeObjectModel);
            IenumClientDocumentTypeObjectModel = LstClientDocumentTypeObjectModel;

            //mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>()))
            //    .Returns(IenumClientDocumentTypeObjectModel);
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>(), It.IsAny<ClientDocumentTypeSortDetail>()))
                .Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.GetAllClientDocumentTypeDetails("Test", string.Empty);

            // Verify and Assert
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClientDocumentTypeDetails_Returns_JsonResult_StringDescription
        /// <summary>
        /// GetAllClientDocumentTypeDetails_Returns_JsonResult_StringDescription
        /// </summary>
        [TestMethod]
        public void GetAllClientDocumentTypeDetails_Returns_JsonResult_StringDescription()
        {
            //Arrange

            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = Enumerable.Empty<ClientDocumentTypeObjectModel>();
            List<ClientDocumentTypeObjectModel> LstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            ClientDocumentTypeObjectModel objClientDocumentTypeObjectModel = new ClientDocumentTypeObjectModel();
            objClientDocumentTypeObjectModel.Description = "Test";
            LstClientDocumentTypeObjectModel.Add(objClientDocumentTypeObjectModel);
            IenumClientDocumentTypeObjectModel = LstClientDocumentTypeObjectModel;

            //mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>()))
            //    .Returns(IenumClientDocumentTypeObjectModel);
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>(), It.IsAny<ClientDocumentTypeSortDetail>()))
                .Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.GetAllClientDocumentTypeDetails("Test", "Test");

            // Verify and Assert
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region EditClientDocumentType_Returns_ActionResult_GetMethod
        /// <summary>
        /// EditClientDocumentType_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void EditClientDocumentType_Returns_ActionResult_GetMethod()
        {
            //Arrange
            mockClientDocTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateClientDocTypeList);
           // mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>())).Returns(CreateUserList());

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.EditClientDocumentType(1);

            ClientDocumentTypeViewModel objExpected = new ClientDocumentTypeViewModel()
            {
                ClientDocumentTypeId = 1,
                Description = "Test_Doc",
                ModifiedBy = null,
                ModifiedByName = "",
                Name = "Test_Doc",
                SuccessOrFailedMessage = null,
                UTCLastModifiedDate = ""
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentTypeViewModel;
            ValidateViewModelData<ClientDocumentTypeViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentTypeViewModel));


            // Verify and Assert
            mockClientDocTypeFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            mockClientDocTypeFactoryCache.VerifyAll();
        }
        #endregion

        #region EditClientDocumentType_Returns_ActionResult_GetMethod_Null
        /// <summary>
        /// EditClientDocumentType_Returns_ActionResult_GetMethod_Null
        /// </summary>
        [TestMethod]
        public void EditClientDocumentType_Returns_ActionResult_GetMethod_Null()
        {
            //Arrange
            UserObjectModel use = new UserObjectModel();
            use.UserId = 0;
            mockClientDocTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateClientDocTypeList);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>())).Returns(CreateUserList());
            ClientDocumentTypeObjectModel objClientDocumentTypeObjectModel = new ClientDocumentTypeObjectModel();
            objClientDocumentTypeObjectModel.ClientDocumentTypeId = 0;
            objClientDocumentTypeObjectModel.Name = "";
            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.EditClientDocumentType(1);
            ClientDocumentTypeViewModel objExpected = new ClientDocumentTypeViewModel()
            {
                ClientDocumentTypeId = 1,
                Description = "Test_Doc",
                ModifiedBy = null,
                ModifiedByName = "Test_UserName",
                Name = "Test_Doc",
                SuccessOrFailedMessage = null,
                UTCLastModifiedDate = ""
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentTypeViewModel;
            ValidateViewModelData<ClientDocumentTypeViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentTypeViewModel));

            // Verify and Assert
            mockClientDocTypeFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClientDocumentType_Returns_ActionResult_GetMethod_IsZero
        /// <summary>
        /// EditClientDocumentType_Returns_ActionResult_GetMethod_IsZero
        /// </summary>
        [TestMethod]
        public void EditClientDocumentType_Returns_ActionResult_GetMethod_IsZero()
        {
            //Arrange

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.EditClientDocumentType(0);

            ClientDocumentTypeViewModel objExpected = new ClientDocumentTypeViewModel()
            {
                ClientDocumentTypeId = 0,
                Description = null,
                ModifiedBy = null,
                ModifiedByName = null,
                Name = null,
                SuccessOrFailedMessage = null,
                UTCLastModifiedDate = null
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentTypeViewModel;
            ValidateViewModelData<ClientDocumentTypeViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentTypeViewModel));


            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClientDocumentType_Returns_ActionResult_PostMethod
        /// <summary>
        /// EditClientDocumentType_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditClientDocumentType_Returns_ActionResult_PostMethod()
        {
            //Arrange
            ClientDocumentTypeViewModel objViewModel = new ClientDocumentTypeViewModel();
            objViewModel.ClientDocumentTypeId = 1;
            objViewModel.Name = "Test_Doc";
            objViewModel.Description = "Test_Desc";
            mockClientDocTypeFactoryCache.Setup(x => x.SaveEntity(It.IsAny<ClientDocumentTypeObjectModel>(), It.IsAny<int>()));

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.EditClientDocumentType(objViewModel);

            ClientDocumentTypeViewModel objExpected = new ClientDocumentTypeViewModel()
            {
                ClientDocumentTypeId = 1,
                Description = "Test_Desc",
                ModifiedBy = null,
                ModifiedByName = null,
                Name = "Test_Doc",
                SuccessOrFailedMessage = null,
                UTCLastModifiedDate = null
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentTypeViewModel;
            ValidateViewModelData<ClientDocumentTypeViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentTypeViewModel));


            // Verify and Assert
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditClientDocumentType_Returns_ActionResult_PostMethod_Handles_Exception
        /// <summary>
        /// EditClientDocumentType_Returns_ActionResult_PostMethod_Handles_Exception
        /// </summary>
        [TestMethod]
        public void EditClientDocumentType_Returns_ActionResult_PostMethod_Handles_Exception()
        {
            //Arrange
            ClientDocumentTypeViewModel objViewModel = new ClientDocumentTypeViewModel();

            mockClientDocTypeFactoryCache.Setup(x => x.SaveEntity(It.IsAny<ClientDocumentTypeObjectModel>(), It.IsAny<int>())).Throws(new Exception());

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.EditClientDocumentType(objViewModel);

            ClientDocumentTypeViewModel objExpected = new ClientDocumentTypeViewModel()
            {
                ClientDocumentTypeId = 0,
                Description = null,
                ModifiedBy = null,
                ModifiedByName = null,
                Name = null,
                SuccessOrFailedMessage = "Exception of type 'System.Exception' was thrown.",
                UTCLastModifiedDate = null
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentTypeViewModel;
            ValidateViewModelData<ClientDocumentTypeViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentTypeViewModel));


            // Verify and Assert
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetName_Returns_JsonResult
        /// <summary>
        /// GetName_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetName_Returns_JsonResult()
        {
            //Arrange
            mockClientDocTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateClientDocTypeList);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.GetName();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Doc" });
            ValidateDisplayValuePair(lstexpected, result);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetName_Returns_JsonResult_Empty
        /// <summary>
        /// GetName_Returns_JsonResult_Empty
        /// </summary>
        [TestMethod]
        public void GetName_Returns_JsonResult_Empty()
        {
            //Arrange
            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = Enumerable.Empty<ClientDocumentTypeObjectModel>();
            mockClientDocTypeFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.GetName();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            // lstexpected.Add(new DisplayValuePair() { Display = "Test_Doc" });
            ValidateDisplayValuePair(lstexpected, result);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetDescription_Returns_JsonResult
        /// <summary>
        /// GetDescription_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetDescription_Returns_JsonResult()
        {
            //Arrange
            mockClientDocTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateClientDocTypeList);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.GetDescription();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Doc" });
            ValidateDisplayValuePair(lstexpected, result);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetDescription_Returns_JsonResult_Empty
        /// <summary>
        /// GetDescription_Returns_JsonResult_Empty
        /// </summary>
        [TestMethod]
        public void GetDescription_Returns_JsonResult_Empty()
        {
            //Arrange
            List<ClientDocumentTypeObjectModel> lstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = lstClientDocumentTypeObjectModel;
            mockClientDocTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.GetDescription();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            // lstexpected.Add(new DisplayValuePair() { Display = "Test_Doc" });
            ValidateDisplayValuePair(lstexpected, result);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region DeleteClientDocumentType_Returns_JsonResult
        /// <summary>
        /// DeleteClientDocumentType_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DeleteClientDocumentType_Returns_JsonResult()
        {
            //Arrange
            mockClientDocTypeFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<int>(), It.IsAny<int>()));

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.DeleteClientDocumentType(1);

            // Verify and Assert

            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, string.Empty);
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
         //   mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentTypeSearchDetail>())).Returns(CreateClientDocTypeList());
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentTypeSearchDetail>())).Returns(CreateClientDocTypeList());


            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.CheckDataAlreadyExists("Test", "Test", "10");

            // Verify and Assert
            Assert.AreEqual(result.Data, true);
            
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            mockClientDocTypeFactoryCache.VerifyAll();
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult_ClientDocument
        /// <summary>
        /// CheckDataAlreadyExists_Returns_JsonResult_ClientDocument
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_Returns_JsonResult_ClientDocument()
        {
            //Arrange
            ClientDocumentTypeObjectModel objClientDocumentTypeObjectModel = new ClientDocumentTypeObjectModel();
            objClientDocumentTypeObjectModel.ClientDocumentTypeId = 1;
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentTypeSearchDetail>())).Returns(CreateClientDocTypeList());

            //mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>())).Returns(CreateClientDocTypeList());

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.CheckDataAlreadyExists("Test", "Test", "10");

            // Verify and Assert
            Assert.AreEqual(result.Data, true);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult_ListNullValues
        /// <summary>
        /// CheckDataAlreadyExists_Returns_JsonResult_ListNullValues
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_Returns_JsonResult_ListNullValues()
        {
            //Arrange
            IEnumerable<ClientDocumentTypeObjectModel> Ienum = Enumerable.Empty<ClientDocumentTypeObjectModel>();
            List<ClientDocumentTypeObjectModel> lstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            ClientDocumentTypeObjectModel obj = new ClientDocumentTypeObjectModel();
            lstClientDocumentTypeObjectModel.Add(obj);
            Ienum = lstClientDocumentTypeObjectModel;
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<ClientDocumentTypeSearchDetail>()))
                .Returns(Ienum);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.CheckDataAlreadyExists("", "", "0");

            // Verify and Assert
            Assert.AreEqual(result.Data, false);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult_Empty
        /// <summary>
        /// CheckDataAlreadyExists_Returns_JsonResult_Empty
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_Returns_JsonResult_Empty()
        {
            //Arrange

            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = Enumerable.Empty<ClientDocumentTypeObjectModel>(); ;
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<ClientDocumentTypeSearchDetail>()))
                .Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.CheckDataAlreadyExists("name","Description","1");

            // Verify and Assert
            Assert.AreEqual(result.Data,false);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult_CurrentIDEmpty
        /// <summary>
        /// CheckDataAlreadyExists_Returns_JsonResult_CurrentIDEmpty
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_Returns_JsonResult_CurrentIDEmpty()
        {
            //Arrange
            List<ClientDocumentTypeObjectModel> lstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = lstClientDocumentTypeObjectModel;
            //mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>(),It.IsAny<ClientDocumentTypeSortDetail>()))
            //    .Returns(IenumClientDocumentTypeObjectModel);
            //mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentTypeSearchDetail>(), It.IsAny<ClientDocumentTypeSortDetail>())).Returns(IenumClientDocumentTypeObjectModel);
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentTypeSearchDetail>())).Returns(CreateClientDocTypeList());

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.CheckDataAlreadyExists("Test", "12", "12");

            // Verify and Assert
            Assert.AreEqual(result.Data, true);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult_DescriptionEmpty
        /// <summary>
        /// CheckDataAlreadyExists_Returns_JsonResult_DescriptionEmpty
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_Returns_JsonResult_DescriptionEmpty()
        {
            //Arrange
            List<ClientDocumentTypeObjectModel> lstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = lstClientDocumentTypeObjectModel;
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentTypeSearchDetail>()))
                .Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.CheckDataAlreadyExists("Test", string.Empty, "Test");

            // Verify and Assert
            Assert.AreEqual(result.Data, false);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult_NameEmpty
        /// <summary>
        /// CheckDataAlreadyExists_Returns_JsonResult_NameEmpty
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_Returns_JsonResult_NameEmpty()
        {
            //Arrange
            List<ClientDocumentTypeObjectModel> lstClientDocumentTypeObjectModel = new List<ClientDocumentTypeObjectModel>();
            IEnumerable<ClientDocumentTypeObjectModel> IenumClientDocumentTypeObjectModel = lstClientDocumentTypeObjectModel;
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<ClientDocumentTypeSearchDetail>()))
                .Returns(IenumClientDocumentTypeObjectModel);

            //Act
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.CheckDataAlreadyExists(string.Empty, "Test", "Test");

            // Verify and Assert
            Assert.AreEqual(result.Data, false);
            mockClientDocTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region ValidateDelete_Returns_JsonResult
        ///<summary>
        ///ValidateDelete_Returns_JsonResult
        ///</summary>
        [TestMethod]
        public void ValidateDelete_Returns_JsonResult()
        {
            //Act
            ClientDocumentObjectModel objClientDocumentObjectModel = new ClientDocumentObjectModel();
            objClientDocumentObjectModel.ClientDocumentTypeId = 1;
            mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentTypeSearchDetail>())).Returns(CreateClientDocTypeList());

            //mockClientDocumentFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentSearchDetail>())).Returns(CreateClientDocumentObjectModel());
        //   mockClientDocTypeFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentSearchDetail>(), It.IsAny<ClientDocumentSortDetail>())).Returns(CreateClientDocumentObjectModel());

            //Arrrange
            ClientDocumentTypeController objController = new ClientDocumentTypeController(mockClientDocTypeFactoryCache.Object, mockUserFactoryCache.Object, mockClientDocumentFactoryCache.Object);
            var result = objController.ValidateDelete(1);

            // Verify and Assert
            Assert.AreEqual(result.Data, false);
            mockClientDocumentFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }
        #endregion
    }
}
