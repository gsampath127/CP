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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Script.Serialization;
using System.Reflection;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    [TestClass]
    public class ClientDocumentGroupTests : BaseTestController<ClientDocumentGroupViewModel>
    {
        Mock<IFactoryCache<ClientDocumentGroupFactory, ClientDocumentGroupObjectModel, int>> clientDocumentGroupCacheFactory;
        Mock<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>> clientDocumentCacheFactory;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> userCacheFactory;

        [TestInitialize]
        public void TestInitialze()
        {
            clientDocumentGroupCacheFactory = new Mock<IFactoryCache<ClientDocumentGroupFactory, ClientDocumentGroupObjectModel, int>>();
            clientDocumentCacheFactory = new Mock<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>>();
            userCacheFactory = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();

        }

        #region ReturnLists
        private IEnumerable<ClientDocumentGroupObjectModel> CreateClientDocumentGroupList()
        {
            IEnumerable<ClientDocumentGroupObjectModel> IenumClientDocumentGroupObjectModel = Enumerable.Empty<ClientDocumentGroupObjectModel>();
            List<ClientDocumentGroupObjectModel> lstClientDocumentGroupModel = new List<ClientDocumentGroupObjectModel>();

            ClientDocumentGroupObjectModel objClientDocumentGroupObjectModel = new ClientDocumentGroupObjectModel();
            objClientDocumentGroupObjectModel.ClientDocumentGroupId = 1;
            objClientDocumentGroupObjectModel.Name = "test_name";
            objClientDocumentGroupObjectModel.CssClass = "test_Css";
            objClientDocumentGroupObjectModel.ParentClientDocumentGroupId = 1;
            objClientDocumentGroupObjectModel.ClientDocumentGroupId = 1;

            //   objClientDocumentGroupObjectModel.ClientDocuments = new List<ClientDocumentObjectModel>();
            List<ClientDocumentObjectModel> lstClientDocObjectModel = new List<ClientDocumentObjectModel>();
            ClientDocumentObjectModel cdom = new ClientDocumentObjectModel();
            cdom.ClientDocumentId = 1;
            cdom.ClientDocumentTypeName = "Doc_1";
            cdom.ClientDocumentTypeId = 3;
            cdom.ClientDocumentId = 5;
            lstClientDocObjectModel.Add(cdom);
            objClientDocumentGroupObjectModel.ClientDocuments = lstClientDocObjectModel;
            objClientDocumentGroupObjectModel.Description = "Test_Doc";
            objClientDocumentGroupObjectModel.ParentClientDocumentGroupId = 1;
            lstClientDocumentGroupModel.Add(objClientDocumentGroupObjectModel);
            IenumClientDocumentGroupObjectModel = lstClientDocumentGroupModel;
            return IenumClientDocumentGroupObjectModel;
        }

        private IEnumerable<ClientDocumentObjectModel> CreateClientDocumentList()
        {
            IEnumerable<ClientDocumentObjectModel> IenumClientDocumentObjectModel = Enumerable.Empty<ClientDocumentObjectModel>();
            List<ClientDocumentObjectModel> lstClientDocumentModel = new List<ClientDocumentObjectModel>();

            ClientDocumentObjectModel objClientDocumentObjectModel = new ClientDocumentObjectModel();
            objClientDocumentObjectModel.ClientDocumentId = 1;
            objClientDocumentObjectModel.ClientDocumentTypeId = 43;
            objClientDocumentObjectModel.FileName = "TAL";
            objClientDocumentObjectModel.ClientDocumentTypeName = "AZ";
            objClientDocumentObjectModel.ContentUri = "http//:WWW.RSVPT.com";
            objClientDocumentObjectModel.Name = "test_name";
            lstClientDocumentModel.Add(objClientDocumentObjectModel);

            IenumClientDocumentObjectModel = lstClientDocumentModel;
            return IenumClientDocumentObjectModel;
        }

        private IEnumerable<UserObjectModel> CreateUserList()
        {
            IEnumerable<UserObjectModel> IenumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserModel = new List<UserObjectModel>();

            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.Name = "WP";
            objUserObjectModel.Description = "TAL";
            objUserObjectModel.FirstName = "A";
            objUserObjectModel.LastName = "Z";
            objUserObjectModel.UserName = "rr27";
            lstUserModel.Add(objUserObjectModel);
            IenumUserObjectModel = lstUserModel;
            return IenumUserObjectModel;
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
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.List();

            //Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetName_Returns_ActionResult
        /// <summary>
        ///GetName_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void GetName_Returns_JsonResult()
        {
            //Arrange
            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentGroupList);
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.GetName();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "test_name", Value = "test_name" });
            ValidateDisplayValuePair(lstexpected, result);

            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetName_Returns_JsonResult_EmptyData
        /// <summary>
        ///GetName_Returns_JsonResult_EmptyData
        /// </summary>
        [TestMethod]
        public void GetName_Returns_JsonResult_EmptyData()
        {
            //Arrange
            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(Enumerable.Empty<ClientDocumentGroupObjectModel>());
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.GetName();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
           // lstexpected.Add(new DisplayValuePair() { Display = "test_name", Value = "test_name" });
            ValidateDisplayValuePair(lstexpected, result);

            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetParent_Returns_JsonResult
        /// <summary>
        ///GetParent_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetParent_Returns_JsonResult()
        {
            //Arrange
            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentGroupList);
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.GetParent();

            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "test_name", Value = "1" });
            ValidateDisplayValuePair(lstExpected, result);

            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetParent_Returns_JsonResult_EmptyparentDocGrpId
        /// <summary>
        ///GetParent_Returns_JsonResult_EmptyparentDocGrpId
        /// </summary>
        [TestMethod]
        public void GetParent_Returns_JsonResult_EmptyparentDocGrpId()
        {
            //Arrange
            IEnumerable<ClientDocumentGroupObjectModel> IenumClientDocumentGroupObjectModel = Enumerable.Empty<ClientDocumentGroupObjectModel>();
            List<ClientDocumentGroupObjectModel> lstClientDocumentGroupModel = new List<ClientDocumentGroupObjectModel>();

            ClientDocumentGroupObjectModel objClientDocumentGroupObjectModel = new ClientDocumentGroupObjectModel();
            objClientDocumentGroupObjectModel.ClientDocumentGroupId = 1;
            objClientDocumentGroupObjectModel.Name = "test_name";
            objClientDocumentGroupObjectModel.ClientDocumentGroupId = 1;
            lstClientDocumentGroupModel.Add(objClientDocumentGroupObjectModel);
            IenumClientDocumentGroupObjectModel = lstClientDocumentGroupModel;

            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(IenumClientDocumentGroupObjectModel);

            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.GetParent();

            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstExpected, result);

            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPreSelectClientDocuments_Returns_ActionResult
        /// <summary>
        ///GetPreSelectClientDocuments_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void GetPreSelectClientDocuments()
        {
            //Arrange
            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentGroupList());
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.GetPreSelectClientDocuments(1);

            //Verify and Assert

            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "5";
            expected.Add(multiSelectDropDownViewModel);

            ValidateData(expected, result);
            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_Returns_JsonResult
        /// <summary>
        ///CheckDataAlreadyExists_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists()
        {
            //Arrange
            clientDocumentGroupCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentGroupSearchDetail>()))
               .Returns(CreateClientDocumentGroupList());
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.CheckDataAlreadyExists("XBRL");

            //Verify and Assert
            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.AreEqual(result.Data, true);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckDataAlreadyExists_ForEmpty
        /// <summary>
        ///CheckDataAlreadyExists_ForEmpty
        /// </summary>
        [TestMethod]
        public void CheckDataAlreadyExists_ForEmpty()
        {
            //Arrange
            IEnumerable<ClientDocumentGroupObjectModel> IenumClientDocumentGroupObjectModel = Enumerable.Empty<ClientDocumentGroupObjectModel>();
            clientDocumentGroupCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ClientDocumentGroupSearchDetail>()))
               .Returns(IenumClientDocumentGroupObjectModel);
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.CheckDataAlreadyExists("XBRL");

            //Verify and Assert
            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.AreEqual(result.Data, false);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region DeleteClientDocumentGroup_Returns_JsonResult
        /// <summary>
        ///DeleteClientDocumentGroup_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DeleteClientDocumentGroup()
        {
            //Arrange
            clientDocumentGroupCacheFactory.Setup(x => x.DeleteEntity(It.IsAny<int>(), It.IsAny<int>()));
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.DeleteClientDocumentGroup(3);

            //Verify and Assert
            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.AreEqual(result.Data, string.Empty);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClientDocuments_Returns_JsonResult
        /// <summary>
        ///GetAllClientDocuments_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetAllClientDocuments()
        {
            //Arrange
            clientDocumentCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentList());
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.GetAllClientDocuments();

            //Verify and Assert

            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.label = "test_name";
            multiSelectDropDownViewModel.title = "test_name";
            multiSelectDropDownViewModel.value = "1";
            expected.Add(multiSelectDropDownViewModel);

            ValidateData<MultiSelectDropDownViewModel>(expected, result);

            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClientDocuments_Returns_JsonResult_EmptyData
        /// <summary>
        ///GetAllClientDocuments_Returns_JsonResult_EmptyData
        /// </summary>
        [TestMethod]
        public void GetAllClientDocuments_Returns_JsonResult_EmptyData()
        {
            //Arrange
            clientDocumentCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(Enumerable.Empty<ClientDocumentObjectModel>());
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.GetAllClientDocuments();

            //Verify and Assert

            ValidateEmptyData<MultiSelectDropDownViewModel>(result);
            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllClientDocumentGroupDetails_Returns_JsonResult
        /// <summary>
        ///GetAllClientDocumentGroupDetails_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetAllClientDocumentGroupDetails()
        {
            //Arrange
            List<ClientDocumentGroupViewModel> lstViewModelExpected = new List<ClientDocumentGroupViewModel>();
            ClientDocumentGroupViewModel objviewModel = new ClientDocumentGroupViewModel();
            objviewModel.Name = "test_name";
            objviewModel.ClientDocumentGroupId = 1;
            objviewModel.Description = "Test_Doc";
            objviewModel.CssClass = "test_Css";
            lstViewModelExpected.Add(objviewModel);

            clientDocumentGroupCacheFactory.Setup(x => x.GetEntitiesBySearch( It.IsAny<ClientDocumentGroupSearchDetail>()))
                .Returns(CreateClientDocumentGroupList);
            clientDocumentGroupCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ClientDocumentGroupSearchDetail>(), It.IsAny<ClientDocumentGroupSortDetail>()))
                .Returns(CreateClientDocumentGroupList);
            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.GetAllClientDocumentGroupDetails("TAHL", "1");
            var resultNullParams = objClientDocumentGroupController.GetAllClientDocumentGroupDetails("", "");

            //Verify and Assert
            ValidateData(lstViewModelExpected, result);
            ValidateData(lstViewModelExpected, resultNullParams);

            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_GetMethod_EqualsToZero
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_EqualsToZero
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_EqualsToZero_GetMethod()
        {
            //Arrange  
            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentGroupList);

            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.Edit(0);

            //Verify and Assert
            List<DisplayValuePair> lstParentDocGrp = new List<DisplayValuePair>();
            lstParentDocGrp.Add(new DisplayValuePair() { Display = "--Please select Document Group--", Value = null });
            lstParentDocGrp.Add(new DisplayValuePair() { Display = "test_name", Value = "1" });

            ClientDocumentGroupViewModel objExpected = new ClientDocumentGroupViewModel()
            {
                SelectedClientDocumentGroupId = -1,
                ParentClientDocumentGroup = lstParentDocGrp
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentGroupViewModel;

            ValidateViewModelData<ClientDocumentGroupViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentGroupViewModel));

            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_EqualsToZero_GetMethodEmptyData
        /// <summary>
        /// Edit_Returns_ActionResult_EqualsToZero_GetMethodEmptyData
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_EqualsToZero_GetMethodEmptyData()
        {
            //Arrange 
            IEnumerable<ClientDocumentGroupObjectModel> IenumEmpty = null;
            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(IenumEmpty);

            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.Edit(0);

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentGroupViewModel;

            //Verify and Assert
            List<DisplayValuePair> lstParentDocGrp = new List<DisplayValuePair>();
            lstParentDocGrp.Add(new DisplayValuePair() { Display = "--Please select Document Group--", Value = null });

            ClientDocumentGroupViewModel objExpected = new ClientDocumentGroupViewModel()
            {
                SelectedClientDocumentGroupId = -1,
                ParentClientDocumentGroup = lstParentDocGrp
            };

            ValidateViewModelData<ClientDocumentGroupViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentGroupViewModel));
            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_GetMethod_NotEqualsToZero
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_NotEqualsToZero
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_NotEqualsToZero_GetMethod()
        {
            //Arrange            
            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateClientDocumentGroupList);
            userCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList());

            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.Edit(1);

            //Verify and Assert
            List<DisplayValuePair> lstParentDocGrp = new List<DisplayValuePair>();
            lstParentDocGrp.Add(new DisplayValuePair() { Display = "--Please select Document Group--", Value = null });
            lstParentDocGrp.Add(new DisplayValuePair() { Display = "test_name", Value = "1" });

            ClientDocumentGroupViewModel objExpected = new ClientDocumentGroupViewModel()
            {
                ClientDocumentGroupId = 1,
                SelectedClientDocumentGroupId = 1,
                CssClass = "test_Css",
                Description = "Test_Doc",
                ModifiedByName = "rr27",
                Name = "test_name",
                UTCLastModifiedDate = string.Empty,
                ParentClientDocumentGroup = lstParentDocGrp
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentGroupViewModel;

            ValidateViewModelData<ClientDocumentGroupViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentGroupViewModel));

            clientDocumentGroupCacheFactory.VerifyAll();
            userCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_NotEqualsToZero_GetMethod_NullGrpId
        /// <summary>
        /// Edit_Returns_ActionResult_NotEqualsToZero_GetMethod_NullGrpId
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_NotEqualsToZero_GetMethod_NullGrpId()
        {
            //Arrange       
            IEnumerable<ClientDocumentGroupObjectModel> IenumClientDocumentGroupObjectModel = Enumerable.Empty<ClientDocumentGroupObjectModel>();
            List<ClientDocumentGroupObjectModel> lstClientDocumentGroupModel = new List<ClientDocumentGroupObjectModel>();

            ClientDocumentGroupObjectModel objClientDocumentGroupObjectModel = new ClientDocumentGroupObjectModel();
            objClientDocumentGroupObjectModel.ClientDocumentGroupId = 1;
            objClientDocumentGroupObjectModel.Name = "test_name";
            objClientDocumentGroupObjectModel.CssClass = "test_Css";
            objClientDocumentGroupObjectModel.ClientDocumentGroupId = 1;
            lstClientDocumentGroupModel.Add(objClientDocumentGroupObjectModel);
            IenumClientDocumentGroupObjectModel = lstClientDocumentGroupModel;

            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(IenumClientDocumentGroupObjectModel);
            userCacheFactory.Setup(x => x.GetEntitiesBySearch( It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList());

            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            var result = objClientDocumentGroupController.Edit(1);

            //Verify and Assert
            List<DisplayValuePair> lstParentDocGrp = new List<DisplayValuePair>();
            lstParentDocGrp.Add(new DisplayValuePair() { Display = "--Please select Document Group--", Value = null });
            lstParentDocGrp.Add(new DisplayValuePair() { Display = "test_name", Value = "1" });

            ClientDocumentGroupViewModel objExpected = new ClientDocumentGroupViewModel()
            {
                ClientDocumentGroupId = 1,
                SelectedClientDocumentGroupId = -1,
                CssClass = "test_Css",
                ModifiedByName = "rr27",
                Name = "test_name",
                UTCLastModifiedDate = string.Empty,
                ParentClientDocumentGroup = lstParentDocGrp
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentGroupViewModel;

            ValidateViewModelData<ClientDocumentGroupViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentGroupViewModel));

            clientDocumentGroupCacheFactory.VerifyAll();
            userCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_PostMethod
        /// <summary>
        /// Edit_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_PostMethod()
        {
            ClientDocumentGroupViewModel obj = new ClientDocumentGroupViewModel();
            obj.ClientDocumentGroupId = 1;
            obj.Name = "Test_2";
            obj.Description = "XBRL";
            obj.ParentClientDocumentGroupId = 1;
            obj.SuccessOrFailedMessage = "success";
            obj.ModifiedBy = 1;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "ClientDocumentGroupId", "1" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends

            //Arrange 
            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateClientDocumentGroupList());
            clientDocumentGroupCacheFactory.Setup(x => x.SaveEntity(It.IsAny <ClientDocumentGroupObjectModel>(),It.IsAny<int>()));

            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            objClientDocumentGroupController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientDocumentGroupController);
            var result = objClientDocumentGroupController.Edit(new ClientDocumentGroupViewModel() { ClientDocumentGroupId = 1, Name = "Test_2", Description = "XBRL" }, "2");

            //Verify and Assert   
            ClientDocumentGroupViewModel objExpected = new ClientDocumentGroupViewModel()
            {
                Description = "XBRL",
                Name = "Test_2",
                ClientDocumentGroupId = 1
            };
            clientDocumentGroupCacheFactory.VerifyAll();
            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentGroupViewModel;
            ValidateViewModelData<ClientDocumentGroupViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentGroupViewModel));
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_PostMethod_NullData
        /// <summary>
        /// Edit_Returns_ActionResult_PostMethod_NullData
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_PostMethod_NullData()
        {
            ClientDocumentGroupViewModel obj = new ClientDocumentGroupViewModel();
            obj.ClientDocumentGroupId = 1;
            obj.Name = "Test_2";
            obj.Description = "XBRL";
            obj.ParentClientDocumentGroupId = 1;
            obj.SuccessOrFailedMessage = "success";
            obj.ModifiedBy = 1;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "test", "1" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends

            //Arrange 

            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            objClientDocumentGroupController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientDocumentGroupController);
            var result = objClientDocumentGroupController.Edit(new ClientDocumentGroupViewModel() { ClientDocumentGroupId = 1, Name = "Test_2", Description = "XBRL" }, "2");

            //Verify and Assert   
            ClientDocumentGroupViewModel objExpected = new ClientDocumentGroupViewModel()
            {
                Description = "XBRL",
                Name = "Test_2",
                ClientDocumentGroupId = 1
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentGroupViewModel;
            ValidateViewModelData<ClientDocumentGroupViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentGroupViewModel));
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_PostMethod_Handles_Exception
        /// <summary>
        /// Edit_Returns_ActionResult_PostMethod_Handles_Exception
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_PostMethod_Handles_Exception()
        {
            ClientDocumentGroupViewModel obj = new ClientDocumentGroupViewModel();
            obj.ClientDocumentGroupId = 1;
            obj.Name = "Test_2";
            obj.Description = "XBRL";
            obj.ParentClientDocumentGroupId = 1;
            obj.SuccessOrFailedMessage = "success";
            obj.ModifiedBy = 1;
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "ClientDocumentGroupId", "1" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            // Mocking Context - ends

            //Arrange      
            IEnumerable<ClientDocumentGroupObjectModel> IenumEmpty = null;
            clientDocumentGroupCacheFactory.Setup(x => x.GetAllEntities()).Returns(IenumEmpty);
            clientDocumentGroupCacheFactory.Setup(x => x.SaveEntity(It.IsAny<ClientDocumentGroupObjectModel>(), It.IsAny<Int32>())).Throws(new Exception());

            //Act
            ClientDocumentGroupController objClientDocumentGroupController =
        new ClientDocumentGroupController(clientDocumentGroupCacheFactory.Object, clientDocumentCacheFactory.Object, userCacheFactory.Object);
            objClientDocumentGroupController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objClientDocumentGroupController);
            var result = objClientDocumentGroupController.Edit(new ClientDocumentGroupViewModel() { ClientDocumentGroupId = 1, Name = "Test_2", Description = "XBRL" }, string.Empty);

            //Verify and Assert    
            ClientDocumentGroupViewModel objExpected = new ClientDocumentGroupViewModel()
            {
                Description = "XBRL",
                Name = "Test_2",
                ClientDocumentGroupId = 1,
                SuccessOrFailedMessage = "Exception of type 'System.Exception' was thrown."
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as ClientDocumentGroupViewModel;
            ValidateViewModelData<ClientDocumentGroupViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(ClientDocumentGroupViewModel));


            clientDocumentGroupCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
    }
}