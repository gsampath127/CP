using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for UserController class
    /// </summary>
    [TestClass]
    public class UserControllerTests : BaseTestController<UserObjectModel>
    {
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserCacheFactory;
        Mock<IFactoryCache<ClientFactory, ClientObjectModel, int>> mockClientCacheFactory;
        Mock<IFactoryCache<RolesFactory, RolesObjectModel, int>> mockRoleCacheFactory;

        //  private Mock<IConfigurationManager> _configurationManager;

        [TestInitialize]
        public void TestInitialze()
        {
            mockUserCacheFactory = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mockClientCacheFactory = new Mock<IFactoryCache<ClientFactory, ClientObjectModel, int>>();
            mockRoleCacheFactory = new Mock<IFactoryCache<RolesFactory, RolesObjectModel, int>>();

            // _configurationManager = new Mock<IConfigurationManager>();
        }

        #region ReturnLists

        private IEnumerable<RolesObjectModel> CreateRolesList()
        {
            IEnumerable<RolesObjectModel> IEnumRoles = Enumerable.Empty<RolesObjectModel>();
            List<RolesObjectModel> lstRolesObjectModel = new List<RolesObjectModel>();
            RolesObjectModel objRolesObjectModel = new RolesObjectModel();
            objRolesObjectModel.RoleId = 1;
            lstRolesObjectModel.Add(objRolesObjectModel);
            IEnumRoles = lstRolesObjectModel;
            return IEnumRoles;
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
        private IEnumerable<UserObjectModel> CreateUserList_UserNameNull()
        {
            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = null;
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
        private IEnumerable<UserObjectModel> CreateUserList_FirstNameNull()
        {
            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_Fname";
            objUserObjectModel.FirstName = null;
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
        private IEnumerable<UserObjectModel> CreateUserList_LastNameNull()
        {
            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_Fname";
            objUserObjectModel.FirstName = "Test_FName";
            objUserObjectModel.LastName = null;
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
        private IEnumerable<UserObjectModel> CreateUserList_EmailNull()
        {
            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_Fname";
            objUserObjectModel.FirstName = "Test_FName";
            objUserObjectModel.LastName = null;
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
        private IEnumerable<UserObjectModel> CreateNonAdminUserList()
        {
            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserName = "Test_UserName";
            objUserObjectModel.FirstName = "Test_FirstName";
            objUserObjectModel.LastName = "Test_LastName";
            objUserObjectModel.Email = "Test_email@gmail.com";

            List<RolesObjectModel> lstRoles = new List<RolesObjectModel>();
            RolesObjectModel objRoles = new RolesObjectModel();
            objRoles.RoleName = "User";
            lstRoles.Add(objRoles);

            objUserObjectModel.Roles = lstRoles;
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;
            return IEnumUser;
        }
        private UserObjectModel CreateUserObjectModel()
        {
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            objUserObjectModel.FirstName = "Test_FirstName";
            objUserObjectModel.LastName = "Test_LastName";
            objUserObjectModel.Email = "Test_Email";
            List<int> lstClients = new List<int>();
            lstClients.Add(1);
            objUserObjectModel.Clients = lstClients;
            return objUserObjectModel;
        }
        private UserObjectModel CreateUserObjectModel_With_Clients()
        {
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            objUserObjectModel.FirstName = "Test_FirstName";
            objUserObjectModel.LastName = "Test_LastName";
            objUserObjectModel.Email = "Test_Email";
            List<int> lstClients = new List<int>();
            lstClients.Add(1);
            objUserObjectModel.Clients = lstClients;
            return objUserObjectModel;
        }
        private IEnumerable<ClientObjectModel> CreateClientList()
        {
            IEnumerable<ClientObjectModel> IenumClient = Enumerable.Empty<ClientObjectModel>();
            List<ClientObjectModel> lstClientObjectModel = new List<ClientObjectModel>();
            ClientObjectModel objClientObjectModel = new ClientObjectModel();
            objClientObjectModel.ClientName = "Test_Client";
            objClientObjectModel.ClientID = 1;
            lstClientObjectModel.Add(objClientObjectModel);
            IenumClient = lstClientObjectModel;
            return IenumClient;
        }

        #endregion
        #region List_Returns_ActionResult_IsNull
        /// <summary>
        /// List_Returns_ActionResult_IsNull
        /// </summary>
        [TestMethod]
        public void List_Returns_ActionResult_IsNull()
        {
            //Arrange
            IEnumerable<UserObjectModel> IenumUser = null;
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>()))
                .Returns(IenumUser);

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.List();

            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region List_Returns_ActionResult_NotNull
        /// <summary>
        /// List_Returns_ActionResult_NotNull
        /// </summary>
        [TestMethod]
        public void List_Returns_ActionResult_NotNull()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.List();

            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region EditUser_Returns_ActionResult_PostMethod
        /// <summary>
        /// EditUser_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditUser_Returns_ActionResult_PostMethod()
        {
            //Arrange
            UserViewModel objUserViewModel = new UserViewModel();
            objUserViewModel.UserID = 1;
            objUserViewModel.IsAdmin = true;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends

            mockRoleCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<RolesSearchDetail>())).Returns(CreateRolesList);
            mockUserCacheFactory.Setup(x => x.SaveEntity(It.IsAny<UserObjectModel>(), It.IsAny<int>()));

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);

            //Mocking the context of controller
            objUserController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objUserController);

            var result = objUserController.EditUser(objUserViewModel, 1);

            UserViewModel ObjUserViewModel = new UserViewModel();
            ObjUserViewModel.Email = null;
            ObjUserViewModel.FirstName = null;
            ObjUserViewModel.IsAdmin = true;
            ObjUserViewModel.LastName = null;
            ObjUserViewModel.ModifiedBy = null;
            ObjUserViewModel.SuccessOrFailedMessage = null;
            ObjUserViewModel.UserID = 1;
            ObjUserViewModel.UserName = null;
            ObjUserViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as UserViewModel;
            ValidateViewModelData<UserViewModel>(viewModel, ObjUserViewModel);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            mockRoleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        #endregion
        #region EditUser_Returns_ActionResult_PostMethod_IsAdmin_False
        /// <summary>
        /// EditUser_Returns_ActionResult_PostMethod_IsAdmin_False
        /// </summary>
        [TestMethod]
        public void EditUser_Returns_ActionResult_PostMethod_IsAdmin_False()
        {
            //Arrange
            UserViewModel objUserViewModel = new UserViewModel();
            objUserViewModel.UserID = 1;
            objUserViewModel.IsAdmin = false;
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
              { "hdnClients", "1,2,3" }      
               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends

            mockRoleCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<RolesSearchDetail>())).Returns(CreateRolesList);
            mockUserCacheFactory.Setup(x => x.SaveEntity(It.IsAny<UserObjectModel>(), It.IsAny<int>()));

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);

            //Mocking the context of controller
            objUserController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objUserController);

            var result = objUserController.EditUser(objUserViewModel, 1);

            UserViewModel ObjUserViewModel = new UserViewModel();
            ObjUserViewModel.Email = null;
            ObjUserViewModel.FirstName = null;
            ObjUserViewModel.IsAdmin = false;
            ObjUserViewModel.LastName = null;
            ObjUserViewModel.ModifiedBy = null;
            ObjUserViewModel.SuccessOrFailedMessage = null;
            ObjUserViewModel.UserID = 1;
            ObjUserViewModel.UserName = null;
            ObjUserViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as UserViewModel;
            ValidateViewModelData<UserViewModel>(viewModel, ObjUserViewModel);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        #endregion
        #region EditUser_Returns_ActionResult_PostMethod_hdnClients_Null
        /// <summary>
        /// EditUser_Returns_ActionResult_PostMethod_hdnClients_Null
        /// </summary>
        [TestMethod]
        public void EditUser_Returns_ActionResult_PostMethod_hdnClients_Null()
        {
            //Arrange
            UserViewModel objUserViewModel = new UserViewModel();
            objUserViewModel.UserID = 1;
            objUserViewModel.IsAdmin = false;
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
              { "hdnClients", null }      
             
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends


            mockUserCacheFactory.Setup(x => x.SaveEntity(It.IsAny<UserObjectModel>(), It.IsAny<int>()));

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);

            //Mocking the context of controller
            objUserController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objUserController);

            var result = objUserController.EditUser(objUserViewModel, 1);

            UserViewModel ObjUserViewModel = new UserViewModel();
            ObjUserViewModel.Email = null;
            ObjUserViewModel.FirstName = null;
            ObjUserViewModel.IsAdmin = false;
            ObjUserViewModel.LastName = null;
            ObjUserViewModel.ModifiedBy = null;
            ObjUserViewModel.SuccessOrFailedMessage = null;
            ObjUserViewModel.UserID = 1;
            ObjUserViewModel.UserName = null;
            ObjUserViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as UserViewModel;
            ValidateViewModelData<UserViewModel>(viewModel, ObjUserViewModel);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        #endregion
        #region EditUser_Returns_ActionResult_PostMethod_Clientid_Null
        /// <summary>
        /// EditUser_Returns_ActionResult_PostMethod_Clientid_Null
        /// </summary>
        [TestMethod]
        public void EditUser_Returns_ActionResult_PostMethod_Clientid_Null()
        {
            //Arrange
            UserViewModel objUserViewModel = new UserViewModel();
            objUserViewModel.UserID = 1;
            objUserViewModel.IsAdmin = false;
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               
               {"clientID",null}
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends

            mockUserCacheFactory.Setup(x => x.SaveEntity(It.IsAny<UserObjectModel>(), It.IsAny<int>()));

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);

            //Mocking the context of controller
            objUserController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objUserController);

            var result = objUserController.EditUser(objUserViewModel, 1);

            UserViewModel ObjUserViewModel = new UserViewModel();
            ObjUserViewModel.Email = null;
            ObjUserViewModel.FirstName = null;
            ObjUserViewModel.IsAdmin = false;
            ObjUserViewModel.LastName = null;
            ObjUserViewModel.ModifiedBy = null;
            ObjUserViewModel.SuccessOrFailedMessage = null;
            ObjUserViewModel.UserID = 1;
            ObjUserViewModel.UserName = null;
            ObjUserViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as UserViewModel;
            ValidateViewModelData<UserViewModel>(viewModel, ObjUserViewModel);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        #endregion
        #region GetUserName_Returns_JsonResult
        /// <summary>
        /// GetUserName_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetUserName_Returns_JsonResult()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateUserList());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetUserName();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_UserName", Value = null });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetUserName_Returns_JsonResult_Username_Null
        /// <summary>
        /// GetUserName_Returns_JsonResult_Username_Null
        /// </summary>
        [TestMethod]
        public void GetUserName_Returns_JsonResult_Username_Null()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateUserList_UserNameNull());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetUserName();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = null });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetFirstName_Returns_JsonResult
        /// <summary>
        /// GetFirstName_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetFirstName_Returns_JsonResult()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateUserList());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetFirstName();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_FirstName", Value = null });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetFirstName_Returns_JsonResult_LastName_Null
        /// <summary>
        /// GetFirstName_Returns_JsonResult_LastName_Null
        /// </summary>
        [TestMethod]
        public void GetFirstName_Returns_JsonResult_LastName_Null()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateUserList_FirstNameNull());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetFirstName();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = null });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetLastName_Returns_JsonResult
        /// <summary>
        /// GetLastName_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetLastName_Returns_JsonResult()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateUserList());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetLastName();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_LastName", Value = null });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetLastName_Returns_JsonResult_LastNameNull
        /// <summary>
        /// GetLastName_Returns_JsonResult_LastNameNull
        /// </summary>
        [TestMethod]
        public void GetLastName_Returns_JsonResult_LastNameNull()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateUserList_LastNameNull());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetLastName();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = null });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetEmail_Returns_JsonResult
        /// <summary>
        /// GetEmail_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetEmail_Returns_JsonResult()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateUserList());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetEmail();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_email@gmail.com", Value = null });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetEmail_Returns_JsonResult_EmailNull
        /// <summary>
        /// GetEmail_Returns_JsonResult_EmailNull
        /// </summary>
        [TestMethod]
        public void GetEmail_Returns_JsonResult_EmailNull()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateUserList());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetEmail();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_email@gmail.com", Value = null });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region CheckUserNameAlreadyExists_Returns_JsonResult
        /// <summary>
        /// CheckUserNameAlreadyExists_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckUserNameAlreadyExists_Returns_JsonResult()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>())).Returns(CreateUserList);

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.CheckUserNameAlreadyExists("Test_UserName");

            //Assert
            Assert.AreEqual(result.Data, true);

            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region CheckUserNameAlreadyExists_Returns_JsonResult_EmptyDetails
        /// <summary>
        /// CheckUserNameAlreadyExists_Returns_JsonResult_EmptyDetails
        /// </summary>
        [TestMethod]
        public void CheckUserNameAlreadyExists_Returns_JsonResult_EmptyDetails()
        {
            //Arrange
            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();

            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>())).Returns(IEnumUser);

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.CheckUserNameAlreadyExists("Test_Username");
            Assert.AreEqual(result.Data, false);

            //Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region EditUser_Returns_ActionResult_GetMethod
        /// <summary>
        /// EditUser_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void EditUser_Returns_ActionResult_GetMethod()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(CreateUserObjectModel);
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>())).Returns(CreateUserList);

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.EditUser(1);

            UserViewModel ObjUserViewModel = new UserViewModel();
            ObjUserViewModel.Email = "Test_Email";
            ObjUserViewModel.FirstName = "Test_FirstName";
            ObjUserViewModel.IsAdmin = true;
            ObjUserViewModel.LastName = "Test_LastName";
            ObjUserViewModel.ModifiedBy = null;
            ObjUserViewModel.SuccessOrFailedMessage = null;
            ObjUserViewModel.UserID = 1;
            ObjUserViewModel.UserName = "Test_UserName";
            ObjUserViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as UserViewModel;
            ValidateViewModelData<UserViewModel>(viewModel, ObjUserViewModel);

            //Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region EditUser_Returns_ActionResult_GetMethod_NonAdminUser
        /// <summary>
        /// EditUser_Returns_ActionResult_GetMethod_NonAdminUser
        /// </summary>
        [TestMethod]
        public void EditUser_Returns_ActionResult_GetMethod_NonAdminUser()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(CreateUserObjectModel);
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>())).Returns(CreateNonAdminUserList);

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.EditUser(1);

            UserViewModel ObjUserViewModel = new UserViewModel();
            ObjUserViewModel.Email = "Test_Email";
            ObjUserViewModel.FirstName = "Test_FirstName";
            ObjUserViewModel.IsAdmin = false;
            ObjUserViewModel.LastName = "Test_LastName";
            ObjUserViewModel.ModifiedBy = null;
            ObjUserViewModel.SuccessOrFailedMessage = null;
            ObjUserViewModel.UserID = 1;
            ObjUserViewModel.UserName = "Test_UserName";
            ObjUserViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as UserViewModel;
            ValidateViewModelData<UserViewModel>(viewModel, ObjUserViewModel);

            //Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region EditUser_Returns_ActionResult_GetMethod_InsertUser
        /// <summary>
        /// EditUser_Returns_ActionResult_GetMethod_InsertUser
        /// </summary>
        [TestMethod]
        public void EditUser_Returns_ActionResult_GetMethod_InsertUser()
        {
            //Arrange

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.EditUser(0);

            UserViewModel ObjUserViewModel = new UserViewModel();
            ObjUserViewModel.Email = null;
            ObjUserViewModel.FirstName = null;
            ObjUserViewModel.IsAdmin = false;
            ObjUserViewModel.LastName = null;
            ObjUserViewModel.ModifiedBy = null;
            ObjUserViewModel.SuccessOrFailedMessage = null;
            ObjUserViewModel.UserID = 0;
            ObjUserViewModel.UserName = null;
            ObjUserViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as UserViewModel;
            ValidateViewModelData<UserViewModel>(viewModel, ObjUserViewModel);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region DeleteUser_Returns_JsonResult
        /// <summary>
        /// DeleteUser_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DeleteUser_Returns_JsonResult()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.DeleteEntity(It.IsAny<int>(), It.IsAny<int>()));

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.DeleteUser(1);

            //Verify and Assert
            Assert.AreEqual(result.Data, string.Empty);
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetAllUsers_Returns_JsonResult
        /// <summary>
        /// GetAllUsers_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetAllUsers_Returns_JsonResult()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>()))
                          .Returns(CreateUserList());

            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>(), It.IsAny<UserSortDetail>()))
                .Returns(CreateUserList());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetAllUsers("Test_Username", "Test_Fname", "Test_Lname", "Test_email");

            List<UserObjectModel> lstExpected = new List<UserObjectModel>
            {
                new UserObjectModel
                {
                    UserId = 1,
                   UserName = "Test_UserName",
                   FirstName = "Test_FirstName",
                   LastName = "Test_LastName",
                   Email = "Test_email@gmail.com"
                }
            };
            ValidateData(lstExpected, result);
            //Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetAllUsers_Returns_JsonResult_Parameters_Null
        /// <summary>
        /// GetAllUsers_Returns_JsonResult_Parameters_Null
        /// </summary>
        [TestMethod]
        public void GetAllUsers_Returns_JsonResult_Parameters_Null()
        {
            //Arrange
            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>()))
                          .Returns(CreateUserList());

            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>(), It.IsAny<UserSortDetail>()))
                .Returns(CreateUserList());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetAllUsers(null, null, null, null);

            List<UserObjectModel> lstExpected = new List<UserObjectModel>
            {
                new UserObjectModel
                {
                    UserId = 1,
                   UserName = "Test_UserName",
                   FirstName = "Test_FirstName",
                   LastName = "Test_LastName",
                   Email = "Test_email@gmail.com"
                }
            };
            ValidateData(lstExpected, result);

            //Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetAllUsers_Returns_JsonResult_EmptyDetails
        /// <summary>
        /// GetAllUsers_Returns_JsonResult_EmptyDetails
        /// </summary>
        [TestMethod]
        public void GetAllUsers_Returns_JsonResult_EmptyDetails()
        {
            //Arrange
            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();

            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>()))
                          .Returns(IEnumUser);

            mockUserCacheFactory.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<UserSearchDetail>(), It.IsAny<UserSortDetail>()))
                .Returns(IEnumUser);

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetAllUsers("Test_Username", "Test_Fname", "Test_Lname", "Test_email");

            ValidateEmptyData<UserObjectModel>(result);
            //Assert
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetUserClients_Returns_JsonResult
        /// <summary>
        /// GetUserClients_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetUserClients_Returns_JsonResult()
        {
            //Arrange
            mockClientCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateClientList());
            mockUserCacheFactory.Setup(x => x.GetEntityByKey(1)).Returns(CreateUserObjectModel);

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetUserClients(1);

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Client", Value = "1" });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockClientCacheFactory.VerifyAll();
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetUserClients_Returns_JsonResult_With_UserIdLessThanZero
        /// <summary>
        /// GetUserClients_Returns_JsonResult_With_UserIdLessThanZero
        /// </summary>
        [TestMethod]
        public void GetUserClients_Returns_JsonResult_With_UserIdLessThanZero()
        {
            //Arrange

            mockClientCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateClientList());

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetUserClients(-1);

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Client", Value = "1" });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockClientCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetUserClients_Returns_JsonResult_With_ClientIdGreaterthanOne
        /// <summary>
        /// GetUserClients_Returns_JsonResult_With_ClientIdGreaterthanOne
        /// </summary>
        [TestMethod]
        public void GetUserClients_Returns_JsonResult_With_ClientIdGreaterthanOne()
        {
            //Arrange
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            objUserObjectModel.FirstName = "Test_FirstName";
            objUserObjectModel.LastName = "Test_LastName";
            objUserObjectModel.Email = "Test_Email";
            List<int> lstClients = new List<int>();
            lstClients.Add(4);
            objUserObjectModel.Clients = lstClients;

            mockClientCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateClientList());
            mockUserCacheFactory.Setup(x => x.GetEntityByKey(1)).Returns(objUserObjectModel);

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetUserClients(1);

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Client", Value = "1" });
            ValidateDisplayValuePair(lstexpected, result);

            //Verify and Assert
            mockClientCacheFactory.VerifyAll();
            mockUserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetUserDetails_Returns_JsonResult
        /// <summary>
        /// GetUserDetails_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetUserDetails_Returns_JsonResult()
        {

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetUserDetails("1");
            //Assert
            ValidateEmptyData<UserObjectModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetUserDetails_Returns_JsonResult_With_NullParameter
        /// <summary>
        /// GetUserDetails_Returns_JsonResult_With_NullParameter
        /// </summary>
        [TestMethod]
        public void GetUserDetails_Returns_JsonResult_With_NullParameter()
        {

            //Act
            UserController objUserController = new UserController(mockUserCacheFactory.Object, mockClientCacheFactory.Object, mockRoleCacheFactory.Object);
            var result = objUserController.GetUserDetails(null);
            //Assert

            ValidateEmptyData<UserObjectModel>(result);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

    }
}


