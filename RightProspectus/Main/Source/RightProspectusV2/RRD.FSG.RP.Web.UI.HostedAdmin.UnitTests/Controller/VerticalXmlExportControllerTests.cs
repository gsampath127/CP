using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for  VerticalXmlExportController class
    /// </summary>
    [TestClass]
    public class VerticalXmlExportControllerTests : BaseTestController<VerticalXmlExportViewModel>
    {
        private Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;

        private Mock<IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int>>
            mockVerticalXmlExportFactoryCache;

        private Mock<IFactory<VerticalXmlExportObjectModel, int>> mockVerticalXmlExportfactory;
        private Mock<KendoGridPost> mockKendoGridPost;

        [TestInitialize]
        public void TestInitialize()
        {
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mockVerticalXmlExportFactoryCache =
                new Mock<IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int>>();
            mockVerticalXmlExportfactory = new Mock<IFactory<VerticalXmlExportObjectModel, int>>();
            mockKendoGridPost = new Mock<KendoGridPost>();
        }

        #region ReturnValues

        private IEnumerable<VerticalXmlExportObjectModel> CreateVerticalXmlExportList()
        {
            IEnumerable<VerticalXmlExportObjectModel> IEnumVrtclXmlExport =
                Enumerable.Empty<VerticalXmlExportObjectModel>();
            List<VerticalXmlExportObjectModel> lstVrtclXmlExport = new List<VerticalXmlExportObjectModel>();
            VerticalXmlExportObjectModel objVrtclXmlExport = new VerticalXmlExportObjectModel();
            objVrtclXmlExport.ExportedBy = 1;
            objVrtclXmlExport.ExportDate = DateTime.Today;
            objVrtclXmlExport.ExportDescription = "Test_Desc";
            objVrtclXmlExport.ExportedByName = "Test_Name";
            objVrtclXmlExport.ExportTypes = 1;
            objVrtclXmlExport.ExportXml = null;
            objVrtclXmlExport.Status = 1;
            objVrtclXmlExport.VerticalXmlExportId = 1;
            lstVrtclXmlExport.Add(objVrtclXmlExport);
            IEnumVrtclXmlExport = lstVrtclXmlExport;
            return IEnumVrtclXmlExport;
        }

        private IEnumerable<VerticalXmlExportObjectModel> CreateVerticalXmlExportList_StatusWithZero()
        {
            IEnumerable<VerticalXmlExportObjectModel> IEnumVrtclXmlExport =
                Enumerable.Empty<VerticalXmlExportObjectModel>();
            List<VerticalXmlExportObjectModel> lstVrtclXmlExport = new List<VerticalXmlExportObjectModel>();
            VerticalXmlExportObjectModel objVrtclXmlExport = new VerticalXmlExportObjectModel();
            objVrtclXmlExport.ExportedBy = 1;
            objVrtclXmlExport.ExportDate = DateTime.Now;
            objVrtclXmlExport.ExportDescription = "Test_Desc";
            objVrtclXmlExport.ExportedByName = "Test_Name";
            objVrtclXmlExport.ExportTypes = 1;
            objVrtclXmlExport.ExportXml = null;
            objVrtclXmlExport.Status = 0;
            objVrtclXmlExport.VerticalXmlExportId = 1;
            lstVrtclXmlExport.Add(objVrtclXmlExport);
            IEnumVrtclXmlExport = lstVrtclXmlExport;
            return IEnumVrtclXmlExport;
        }

        private IEnumerable<VerticalXmlExportObjectModel> CreateVerticalXmlExportList_StatusWithOne()
        {
            IEnumerable<VerticalXmlExportObjectModel> IEnumVrtclXmlExport =
                Enumerable.Empty<VerticalXmlExportObjectModel>();
            List<VerticalXmlExportObjectModel> lstVrtclXmlExport = new List<VerticalXmlExportObjectModel>();
            VerticalXmlExportObjectModel objVrtclXmlExport = new VerticalXmlExportObjectModel();
            objVrtclXmlExport.ExportedBy = 1;
            objVrtclXmlExport.ExportDate = DateTime.Now;
            objVrtclXmlExport.ExportDescription = "Test_Desc";
            objVrtclXmlExport.ExportedByName = "Test_Name";
            objVrtclXmlExport.ExportTypes = 1;
            objVrtclXmlExport.ExportXml = null;
            objVrtclXmlExport.Status = 1;
            objVrtclXmlExport.VerticalXmlExportId = 1;
            lstVrtclXmlExport.Add(objVrtclXmlExport);
            IEnumVrtclXmlExport = lstVrtclXmlExport;
            return IEnumVrtclXmlExport;
        }

        private IEnumerable<VerticalXmlExportObjectModel> CreateVerticalXmlExportList_ExportBy()
        {
            IEnumerable<VerticalXmlExportObjectModel> IEnumVrtclXmlExport =
                Enumerable.Empty<VerticalXmlExportObjectModel>();
            List<VerticalXmlExportObjectModel> lstVrtclXmlExport = new List<VerticalXmlExportObjectModel>();
            VerticalXmlExportObjectModel objVrtclXmlExport = new VerticalXmlExportObjectModel();
            objVrtclXmlExport.ExportedBy = 1;
            objVrtclXmlExport.ExportDate = DateTime.Now;
            objVrtclXmlExport.ExportDescription = "Test_Desc";
            objVrtclXmlExport.ExportedByName = "Test_Name";
            objVrtclXmlExport.ExportTypes = 1;
            objVrtclXmlExport.ExportXml = null;
            objVrtclXmlExport.Status = 0;
            objVrtclXmlExport.VerticalXmlExportId = 1;
            lstVrtclXmlExport.Add(objVrtclXmlExport);
            IEnumVrtclXmlExport = lstVrtclXmlExport;
            return IEnumVrtclXmlExport;
        }


        private VerticalXmlExportObjectModel CreateVerticalXmlExportObjectModel()
        {
            VerticalXmlExportObjectModel objVrtclXmlExport = new VerticalXmlExportObjectModel();
            objVrtclXmlExport.ExportedBy = 1;
            objVrtclXmlExport.ExportDate = DateTime.Now;
            objVrtclXmlExport.ExportDescription = "Test_Desc";
            objVrtclXmlExport.ExportedByName = "Test_Name";
            objVrtclXmlExport.ExportTypes = 1;
            objVrtclXmlExport.ExportXml = "Test";
            objVrtclXmlExport.Status = 1;
            objVrtclXmlExport.VerticalXmlExportId = 1;
            return objVrtclXmlExport;
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
            lstClients.Add(2);
            objUserObjectModel.Clients = lstClients;
            return objUserObjectModel;
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
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.List();

            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region GetUsers_Returns_JsonResult

        /// <summary>
        /// GetUsers_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetUsers_Returns_JsonResult()
        {
            //Arrange
            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlExportList());
            mockUserFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<Int32>())).Returns(CreateUserObjectModel());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.GetUsers();

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_FirstName Test_LastName", Value = "1" });
            ValidateDisplayValuePair(lstexpected, result);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        #endregion

        #region GetUsers_Returns_JsonResult_EmptyResult

        /// <summary>
        /// GetUsers_Returns_JsonResult_EmptyResult
        /// </summary>
        [TestMethod]
        public void GetUsers_Returns_JsonResult_EmptyResult()
        {
            //Arrange
            IEnumerable<VerticalXmlExportObjectModel> IEnumVrtclXmlExport =
                Enumerable.Empty<VerticalXmlExportObjectModel>();

            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities()).Returns(IEnumVrtclXmlExport);


            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.GetUsers();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        #endregion

        #region Search_Returns_JsonResult

        /// <summary>
        /// Search_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult()
        {
            //Arrange

            mockVerticalXmlExportFactoryCache.Setup(x =>x.GetEntitiesBySearch(It.IsAny<Int32>(), It.IsAny<Int32>(),It.IsAny<VerticalXmlExportSearchDetail>()))
                .Returns(CreateVerticalXmlExportList());

            mockVerticalXmlExportFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<Int32>(), It.IsAny<Int32>(), It.IsAny<VerticalXmlExportSearchDetail>(), It.IsAny<VerticalXmlExportSortDetail>()))
                .Returns(CreateVerticalXmlExportList());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Search(null, null, null);

            List<VerticalXmlExportViewModel> expected = new List<VerticalXmlExportViewModel>();
            VerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new VerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDate = DateTime.Today.ToString("MM/dd/yyyy hh:mm:ss");
            objAddVerticalXmlExportViewModel.ExportDescription = "Test_Desc";
            objAddVerticalXmlExportViewModel.ExportedBy = 1;
            objAddVerticalXmlExportViewModel.ExportedByName = "Test_Name";
            objAddVerticalXmlExportViewModel.ExportTypes = 1;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.Status = "In Progress";
            objAddVerticalXmlExportViewModel.StatusID = 1;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 1;
            expected.Add(objAddVerticalXmlExportViewModel);
            ValidateData(expected, result);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        #endregion

        #region Search_Returns_JsonResult_CheckNullForSearchDetail

        /// <summary>
        /// Search_Returns_JsonResult_CheckNullForSearchDetail
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_CheckNullForSearchDetail()
        {
            //Arrange

            mockVerticalXmlExportFactoryCache.Setup(
                x =>
                    x.GetEntitiesBySearch(It.IsAny<Int32>(), It.IsAny<Int32>(),
                        new VerticalXmlExportSearchDetail
                        {
                            FromExportDate = DateTime.Today,
                            ToExportDate = DateTime.UtcNow
                        }))
                .Returns(CreateVerticalXmlExportList());
            mockVerticalXmlExportFactoryCache.Setup(
                x =>
                    x.GetEntitiesBySearch(It.IsAny<Int32>(), It.IsAny<Int32>(),
                        It.IsAny<VerticalXmlExportSearchDetail>(), It.IsAny<VerticalXmlExportSortDetail>()))
                .Returns(CreateVerticalXmlExportList());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Search(null, null, null);

            ValidateEmptyData<VerticalXmlExportViewModel>(result);
            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        #endregion

        #region Search_Returns_JsonResult_With_UserIdAsParameter

        /// <summary>
        /// Search_Returns_JsonResult_With_UserIdAsParameter
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_With_UserIdAsParameter()
        {
            //Arrange

            mockVerticalXmlExportFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<Int32>(), It.IsAny<Int32>(), It.IsAny<VerticalXmlExportSearchDetail>(), It.IsAny<VerticalXmlExportSortDetail>()))
                .Returns(CreateVerticalXmlExportList());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Search(null, null, "2");

            ValidateEmptyData<VerticalXmlExportViewModel>(result);
            mockVerticalXmlExportFactoryCache.VerifyAll();
            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        #endregion

        #region Search_Returns_JsonResult_ReturnEmpty

        /// <summary>
        /// Search_Returns_JsonResult_ReturnEmpty
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_ReturnEmpty()
        {
            //Arrange
            IEnumerable<VerticalXmlExportObjectModel> IEnumVrtclXmlExport =
                Enumerable.Empty<VerticalXmlExportObjectModel>();

            mockVerticalXmlExportFactoryCache.Setup(
                x =>
                    x.GetEntitiesBySearch(It.IsAny<Int32>(), It.IsAny<Int32>(),
                        It.IsAny<VerticalXmlExportSearchDetail>()))
                .Returns(IEnumVrtclXmlExport);
            mockVerticalXmlExportFactoryCache.Setup(
                x =>
                    x.GetEntitiesBySearch(It.IsAny<Int32>(), It.IsAny<Int32>(),
                        It.IsAny<VerticalXmlExportSearchDetail>(), It.IsAny<VerticalXmlExportSortDetail>()))
                .Returns(CreateVerticalXmlExportList());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Search(null, null, null);

            ValidateEmptyData<VerticalXmlExportViewModel>(result);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        #endregion

        #region Add_Returns_ActionResult_GetMethod

        /// <summary>
        /// Add_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_GetMethod()
        {
            //Arrange
            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlExportList());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add();

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Data Export on "+ DateTime.Today.ToString("MM/dd/yyyy");
            objAddVerticalXmlExportViewModel.ExportedBy = 0;
            objAddVerticalXmlExportViewModel.ExportTypes = 0;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.InProgressJobCount = 0;
            objAddVerticalXmlExportViewModel.Status = 0;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 0;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModel, objAddVerticalXmlExportViewModel);
            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_GetMethod_EmptyDetails

        /// <summary>
        /// Add_Returns_ActionResult_GetMethod_EmptyDetails
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_GetMethod_EmptyDetails()
        {
            //Arrange
            IEnumerable<VerticalXmlExportObjectModel> IEnumVrtclXmlExport =
                Enumerable.Empty<VerticalXmlExportObjectModel>();

            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(IEnumVrtclXmlExport);

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add();

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Data Export on "+ DateTime.Today.ToString("MM/dd/yyyy");
            objAddVerticalXmlExportViewModel.ExportedBy = 0;
            objAddVerticalXmlExportViewModel.ExportTypes = 0;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.InProgressJobCount = 0;
            objAddVerticalXmlExportViewModel.Status = 0;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 0;
            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModel, objAddVerticalXmlExportViewModel);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_GetMethod_StatusCheckWithZero

        /// <summary>
        /// Add_Returns_ActionResult_GetMethod_StatusCheckWithZero
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_GetMethod_StatusCheckWithZero()
        {
            //Arrange
            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalXmlExportList_StatusWithZero());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add();

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Data Export on "+ DateTime.Today.ToString("MM/dd/yyyy");
            objAddVerticalXmlExportViewModel.ExportedBy = 0;
            objAddVerticalXmlExportViewModel.ExportTypes = 0;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.InProgressJobCount = 1;
            objAddVerticalXmlExportViewModel.Status = 0;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 0;
            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModel, objAddVerticalXmlExportViewModel);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_GetMethod_StatusCheckWithOne

        /// <summary>
        /// Add_Returns_ActionResult_GetMethod_StatusCheckWithOne
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_GetMethod_StatusCheckWithOne()
        {
            //Arrange
            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalXmlExportList_StatusWithOne());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add();

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Data Export on " + DateTime.Today.ToString("MM/dd/yyyy");
            objAddVerticalXmlExportViewModel.ExportedBy = 0;
            objAddVerticalXmlExportViewModel.ExportTypes = 0;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.InProgressJobCount = 0;
            objAddVerticalXmlExportViewModel.Status = 0;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 0;
            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModel, objAddVerticalXmlExportViewModel);


            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_GetMethod_StatusCheckWithExportBy

        /// <summary>
        /// Add_Returns_ActionResult_GetMethod_StatusCheckWithExportBy
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_GetMethod_StatusCheckWithExportBy()
        {
            //Arrange
            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalXmlExportList_ExportBy());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add();

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Data Export on " + DateTime.Today.ToString("MM/dd/yyyy");
            objAddVerticalXmlExportViewModel.ExportedBy = 0;
            objAddVerticalXmlExportViewModel.ExportTypes = 0;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.InProgressJobCount = 1;
            objAddVerticalXmlExportViewModel.Status = 0;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 0;
            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModel, objAddVerticalXmlExportViewModel);


            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_PostMethod

        /// <summary>
        /// Add_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_PostMethod()
        {
            //Arrange
            AddVerticalXmlExportViewModel viewModel = new AddVerticalXmlExportViewModel();
            viewModel.ExportDescription = "Test_Desc";
            viewModel.VerticalXmlExportId = 1;
            viewModel.ExportTypes = 2;
            viewModel.ExportXml = "test.xml";
            viewModel.ExportedBy = 1;
            viewModel.Status = 1;
            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlExportList());
            mockVerticalXmlExportFactoryCache.Setup(x => x.SaveEntity(It.IsAny<VerticalXmlExportObjectModel>()));

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add(viewModel);

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Test_Desc";
            objAddVerticalXmlExportViewModel.ExportedBy = 1;
            objAddVerticalXmlExportViewModel.ExportTypes = 2;
            objAddVerticalXmlExportViewModel.ExportXml = "test.xml";
            objAddVerticalXmlExportViewModel.InProgressJobCount = 0;
            objAddVerticalXmlExportViewModel.Status = 1;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 1;
            var result1 = result as ViewResult;
            var viewModels = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModels, objAddVerticalXmlExportViewModel);


            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_PostMethod_PassViewModelAsNULL

        /// <summary>
        /// Add_Returns_ActionResult_PostMethod_PassViewModelAsNULL
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_PostMethod_PassViewModelAsNULL()
        {

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add(null);

            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_PostMethod_EmptyDetails

        /// <summary>
        /// Add_Returns_ActionResult_PostMethod_EmptyDetails
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_PostMethod_EmptyDetails()
        {
            //Arrange
            IEnumerable<VerticalXmlExportObjectModel> IEnumVrtclXmlExport =
                Enumerable.Empty<VerticalXmlExportObjectModel>();

            AddVerticalXmlExportViewModel viewModel = new AddVerticalXmlExportViewModel();
            viewModel.ExportDescription = "Test_Desc";
            viewModel.VerticalXmlExportId = 1;
            viewModel.ExportTypes = 2;
            viewModel.ExportXml = "test.xml";
            viewModel.ExportedBy = 1;
            viewModel.Status = 1;

            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(IEnumVrtclXmlExport);
            mockVerticalXmlExportFactoryCache.Setup(x => x.SaveEntity(It.IsAny<VerticalXmlExportObjectModel>()));

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add(viewModel);

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Test_Desc";
            objAddVerticalXmlExportViewModel.ExportedBy = 1;
            objAddVerticalXmlExportViewModel.ExportTypes = 2;
            objAddVerticalXmlExportViewModel.ExportXml = "test.xml";
            objAddVerticalXmlExportViewModel.InProgressJobCount = 0;
            objAddVerticalXmlExportViewModel.Status = 1;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 1;
            var result1 = result as ViewResult;
            var viewModels = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModels, objAddVerticalXmlExportViewModel);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_PostMethod_Handles_Exception

        /// <summary>
        /// Add_Returns_ActionResult_PostMethod_Handles_Exception
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_PostMethod_Handles_Exception()
        {
            //Arrange
            AddVerticalXmlExportViewModel viewModel = new AddVerticalXmlExportViewModel();
            viewModel.ExportDescription = "Test_Desc";
            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities()).Throws(new Exception());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add(viewModel);

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Test_Desc";
            objAddVerticalXmlExportViewModel.ExportedBy = 0;
            objAddVerticalXmlExportViewModel.ExportTypes = 0;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.InProgressJobCount = 0;
            objAddVerticalXmlExportViewModel.Status = 0;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 0;
            var result1 = result as ViewResult;
            var viewModels = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModels, objAddVerticalXmlExportViewModel);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_PostMethod_StatusCheckWithZero

        /// <summary>
        /// Add_Returns_ActionResult_PostMethod_StatusCheckWithZero
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_PostMethod_StatusCheckWithZero()
        {
            //Arrange
            AddVerticalXmlExportViewModel viewModel = new AddVerticalXmlExportViewModel();
            viewModel.ExportDescription = "Test_Desc";

            VerticalXmlExportObjectModel ObjVerticalXmlExportObjectModel = new VerticalXmlExportObjectModel();
            ObjVerticalXmlExportObjectModel.Status = -1;
            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalXmlExportList_StatusWithZero());
            mockVerticalXmlExportFactoryCache.Setup(x => x.SaveEntity(It.IsAny<VerticalXmlExportObjectModel>()));

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add(viewModel);

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Test_Desc";
            objAddVerticalXmlExportViewModel.ExportedBy = 0;
            objAddVerticalXmlExportViewModel.ExportTypes = 0;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.InProgressJobCount = 1;
            objAddVerticalXmlExportViewModel.Status = 0;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 0;
            var result1 = result as ViewResult;
            var viewModels = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModels, objAddVerticalXmlExportViewModel);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_PostMethod_StatusCheck_StatusCheckWithOne

        /// <summary>
        /// Add_Returns_ActionResult_PostMethod_StatusCheck_StatusCheckWithOne
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_PostMethod_StatusCheck_StatusCheckWithOne()
        {
            //Arrange
            AddVerticalXmlExportViewModel viewModel = new AddVerticalXmlExportViewModel();
            viewModel.ExportDescription = "Test_Desc";

            VerticalXmlExportObjectModel ObjVerticalXmlExportObjectModel = new VerticalXmlExportObjectModel();
            ObjVerticalXmlExportObjectModel.Status = -1;
            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalXmlExportList_StatusWithOne());
            mockVerticalXmlExportFactoryCache.Setup(x => x.SaveEntity(It.IsAny<VerticalXmlExportObjectModel>()));

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add(viewModel);


            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Test_Desc";
            objAddVerticalXmlExportViewModel.ExportedBy = 0;
            objAddVerticalXmlExportViewModel.ExportTypes = 0;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.InProgressJobCount = 0;
            objAddVerticalXmlExportViewModel.Status = 0;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 0;
            var result1 = result as ViewResult;
            var viewModels = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModels, objAddVerticalXmlExportViewModel);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region Add_Returns_ActionResult_PostMethod_StatusCheck_StatusCheckWithExportedBy

        /// <summary>
        /// Add_Returns_ActionResult_PostMethod_StatusCheck_StatusCheckWithExportedBy
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_PostMethod_StatusCheck_StatusCheckWithExportedBy()
        {
            //Arrange
            AddVerticalXmlExportViewModel viewModel = new AddVerticalXmlExportViewModel();
            viewModel.ExportDescription = "Test_Desc";

            VerticalXmlExportObjectModel ObjVerticalXmlExportObjectModel = new VerticalXmlExportObjectModel();
            ObjVerticalXmlExportObjectModel.Status = -1;

            mockVerticalXmlExportFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateVerticalXmlExportList_ExportBy());
            mockVerticalXmlExportFactoryCache.Setup(x => x.SaveEntity(It.IsAny<VerticalXmlExportObjectModel>()));

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.Add(viewModel);

            AddVerticalXmlExportViewModel objAddVerticalXmlExportViewModel = new AddVerticalXmlExportViewModel();
            objAddVerticalXmlExportViewModel.ExportDescription = "Test_Desc";
            objAddVerticalXmlExportViewModel.ExportedBy = 0;
            objAddVerticalXmlExportViewModel.ExportTypes = 0;
            objAddVerticalXmlExportViewModel.ExportXml = null;
            objAddVerticalXmlExportViewModel.InProgressJobCount = 1;
            objAddVerticalXmlExportViewModel.Status = 0;
            objAddVerticalXmlExportViewModel.VerticalXmlExportId = 0;
            var result1 = result as ViewResult;
            var viewModels = result1.Model as AddVerticalXmlExportViewModel;
            ValidateViewModelData<AddVerticalXmlExportViewModel>(viewModels, objAddVerticalXmlExportViewModel);

            // Verify and Assert
            mockVerticalXmlExportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        #endregion

        #region GetExportedXml_Returns_FileContentResult

        /// <summary>
        /// GetExportedXml_Returns_FileContentResult
        /// </summary>
        [TestMethod]
        public void GetExportedXml_Returns_FileContentResult()
        {
            //Arrange
            mockVerticalXmlExportfactory.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(CreateVerticalXmlExportObjectModel());

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.GetExportedXml(null);

            // Verify and Assert
            mockVerticalXmlExportfactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(FileContentResult));
        }

        #endregion

        #region GetExportedXml_Returns_FileContentResult_ForNullXml

        /// <summary>
        /// GetExportedXml_Returns_FileContentResult_ForNullXml
        /// </summary>
        [TestMethod]
        public void GetExportedXml_Returns_FileContentResult_ForNullXml()
        {
            //Arrange
            VerticalXmlExportObjectModel objVrtclXmlExport = new VerticalXmlExportObjectModel();
            objVrtclXmlExport.ExportXml = "";
            mockVerticalXmlExportfactory.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objVrtclXmlExport);

            //Act
            VerticalXmlExportController objController = new VerticalXmlExportController(mockUserFactoryCache.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportfactory.Object);
            var result = objController.GetExportedXml(null);

            // Verify and Assert
            mockVerticalXmlExportfactory.VerifyAll();
            Assert.AreEqual(result, null);
        }

        #endregion
    }
}
