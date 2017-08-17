using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    ///  Test class for HomeController class
    /// </summary>
    [TestClass]
    public class HomeControllerTests : BaseTestController<SelectCustomerViewModel>
    {
        Mock<IFactoryCache<ClientFactory, ClientObjectModel, int>> mockClientFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;

        [TestInitialize]
        public void TestInitialize()
        {
            mockClientFactoryCache = new Mock<IFactoryCache<ClientFactory, ClientObjectModel, int>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
        }

        #region ReturnValues
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

        private IEnumerable<ClientObjectModel> CreateClientList()
        {
            IEnumerable<ClientObjectModel> IEnumClient = Enumerable.Empty<ClientObjectModel>();
            List<ClientObjectModel> lstClient = new List<ClientObjectModel>();
            ClientObjectModel objClient = new ClientObjectModel();
            objClient.ClientID = 1;
            objClient.ClientName = "Test_Client";
            List<int> ClientUsers = new List<int>();
            ClientUsers.Add(0);
            objClient.Users = ClientUsers;
            lstClient.Add(objClient);
            IEnumClient = lstClient;
            return IEnumClient;
        }
        #endregion

        //#region SelectCustomer_Returns_ActionResult_GetMethod
        ///// <summary>
        ///// SelectCustomer_Returns_ActionResult_GetMethod
        ///// </summary>
        //[TestMethod]
        //public void SelectCustomer_Returns_ActionResult_GetMethod()
        //{
        //    //Arrange
        //    mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>())).Returns(CreateUserList());
        //    //Act
        //    HomeController objController = new HomeController(mockClientFactoryCache.Object, mockUserFactoryCache.Object);
        //    var result = objController.SelectCustomer();
        //    //Assert
        //}
        //#endregion

        #region SelectCustomer_Returns_ActionResult_PostMethod
        /// <summary>
        /// SelectCustomer_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void SelectCustomer_Returns_ActionResult_PostMethod()
        {
            //Arrange
            SelectCustomerViewModel selectCustomerViewModel = new SelectCustomerViewModel();
            selectCustomerViewModel.SelectedCustomerID = "1";

            //Mocking session value - starts
            long sessionValue = 1;
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupSet(x => x.Session["CLIENT_ID"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                httpContext.SetupGet(x => x.Session["CLIENT_ID"]).Returns(sessionValue);
            });
            //Mocking session value - ends

            mockClientFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateClientList());

            //Act
            HomeController objController = new HomeController(mockClientFactoryCache.Object, mockUserFactoryCache.Object);
            //Mocking the context of controller
            objController.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), objController);

            var result = objController.SelectCustomer(selectCustomerViewModel);

            var result1 = result as RedirectToRouteResult;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<string> resultActual = serializer.Deserialize<List<string>>(serializer.Serialize(result1.RouteValues.Values));
            
            // Verify and Assert
            Assert.AreEqual(resultActual[0], "WelcomeClient");
            Assert.AreEqual(resultActual[1], "Client");
            mockClientFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region SelectCustomer_Returns_ActionResult_PostMethod_AdminUser
        /// <summary>
        /// SelectCustomer_Returns_ActionResult_PostMethod_AdminUser
        /// </summary>
        [TestMethod]
        public void SelectCustomer_Returns_ActionResult_PostMethod_AdminUser()
        {
            //Arrange
            SelectCustomerViewModel selectCustomerViewModel = new SelectCustomerViewModel();
            selectCustomerViewModel.SelectedCustomerID = "ADMIN";

            //Act
            HomeController objController = new HomeController(mockClientFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.SelectCustomer(selectCustomerViewModel);

            var result1 = result as RedirectToRouteResult;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<string> resultActual = serializer.Deserialize<List<string>>(serializer.Serialize(result1.RouteValues.Values));

            // Verify and Assert
            Assert.AreEqual(resultActual[0], "List");
            Assert.AreEqual(resultActual[1], "Client");

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

        }
        #endregion

        #region SelectCustomer_Returns_ActionResult_PostMethod_UnknownUser_ThrowsExceptionInLoggedInUser
        /// <summary>
        /// SelectCustomer_Returns_ActionResult_PostMethod_UnknownUser
        /// </summary>
        [TestMethod]
        public void SelectCustomer_Returns_ActionResult_PostMethod_UnknownUser_ThrowsExceptionInLoggedInUser()
        {
            //Arrange
            SelectCustomerViewModel selectCustomerViewModel = new SelectCustomerViewModel();
            selectCustomerViewModel.SelectedCustomerID = "-1";

            //Mocking IPrincipal - starts
            var principal = new Moq.Mock<IPrincipal>();
            var httpContext = new Moq.Mock<HttpContextBase>();
            httpContext.Setup(x => x.User.Identity.Name).Returns(string.Empty);
            //Mocking IPrincipal - ends

            mockClientFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateClientList());
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>())).Returns(CreateUserList());

            //Act
            HomeController objController = new HomeController(mockClientFactoryCache.Object, mockUserFactoryCache.Object);
            //Mocking the context of controller
            objController.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), objController);

            var result = objController.SelectCustomer(selectCustomerViewModel);

            List<DisplayValuePair> lstCustomerNames = new List<DisplayValuePair>();
            lstCustomerNames.Add(new DisplayValuePair() { Display = "--Please select Client--", Value = "-1" });
            lstCustomerNames.Add(new DisplayValuePair() { Display = "ADMIN", Value = "ADMIN" });
            lstCustomerNames.Add(new DisplayValuePair() { Display = "Test_Client", Value = "1" });


            SelectCustomerViewModel objExpected = new SelectCustomerViewModel()
            {
                CustomerNames = lstCustomerNames,
                SelectedCustomerID = "-1"

            };

            // Verify and Assert
            var result1 = result as ViewResult;
            var viewModel = result1.Model as SelectCustomerViewModel;
            ValidateViewModelData<SelectCustomerViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            mockClientFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region SelectCustomer_Returns_ActionResult_PostMethod_UnknownUser
        /// <summary>
        /// SelectCustomer_Returns_ActionResult_PostMethod_UnknownUser
        /// </summary>
        [TestMethod]
        public void SelectCustomer_Returns_ActionResult_PostMethod_UnknownUser()
        {
            //Arrange
            SelectCustomerViewModel selectCustomerViewModel = new SelectCustomerViewModel();
            selectCustomerViewModel.SelectedCustomerID = "-1";

            //Mocking IPrincipal - starts
            var principal = new Moq.Mock<IPrincipal>();
            var httpContext = new Moq.Mock<HttpContextBase>();
            httpContext.Setup(x => x.User.Identity.Name).Returns("MName");
            //Mocking IPrincipal - ends


            mockClientFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateClientList());
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<UserSearchDetail>())).Returns(CreateUserList());

            //Act
            HomeController objController = new HomeController(mockClientFactoryCache.Object, mockUserFactoryCache.Object);
            //Mocking the context of controller
            objController.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), objController);

            var result = objController.SelectCustomer(selectCustomerViewModel);

            List<DisplayValuePair> lstCustomerNames = new List<DisplayValuePair>();
            lstCustomerNames.Add(new DisplayValuePair() { Display = "--Please select Client--", Value = "-1" });
            lstCustomerNames.Add(new DisplayValuePair() { Display = "ADMIN", Value = "ADMIN" });
            lstCustomerNames.Add(new DisplayValuePair() { Display = "Test_Client", Value = "1" });


            SelectCustomerViewModel objExpected = new SelectCustomerViewModel()
            {
                CustomerNames = lstCustomerNames,
                SelectedCustomerID = "-1"

            };

            // Verify and Assert
            var result1 = result as ViewResult;
            var viewModel = result1.Model as SelectCustomerViewModel;
            ValidateViewModelData<SelectCustomerViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            mockClientFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

    }
}
    