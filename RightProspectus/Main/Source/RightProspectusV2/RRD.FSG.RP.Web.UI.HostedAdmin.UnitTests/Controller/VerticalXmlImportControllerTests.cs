using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for VerticalXmlImportController class
    /// </summary>
    [TestClass]
    public class VerticalXmlImportControllerTests : BaseTestController<VerticalXmlImportViewModel>
    {
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserfactorycache;
        Mock<IFactoryCache<VerticalXmlImportFactory, VerticalXmlImportObjectModel, int>> mockVerticalXmlImportFactoryCache;
        Mock<IFactory<VerticalXmlImportObjectModel, int>> mockVerticalXmlImportFactory;
        Mock<IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int>> mockVerticalXmlExportFactoryCache;
        Mock<IFactory<VerticalXmlExportObjectModel, int>> mockVerticalXmlExportFactory;
        Mock<IFactory<ErrorLogObjectModel, int>> mockErrorLogFactor;

        [TestInitialize]
        public void TestInitialize()
        {
            mockUserfactorycache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mockVerticalXmlImportFactoryCache = new Mock<IFactoryCache<VerticalXmlImportFactory, VerticalXmlImportObjectModel, int>>();
            mockVerticalXmlImportFactory = new Mock<IFactory<VerticalXmlImportObjectModel, int>>();
            mockVerticalXmlExportFactoryCache = new Mock<IFactoryCache<VerticalXmlExportFactory, VerticalXmlExportObjectModel, int>>();
            mockVerticalXmlExportFactory = new Mock<IFactory<VerticalXmlExportObjectModel, int>>();
            mockErrorLogFactor = new Mock<IFactory<ErrorLogObjectModel, int>>();
        }

        #region ReturnValues
        private IEnumerable<VerticalXmlImportObjectModel> CreateVerticalXmlImportList()
        {
            IEnumerable<VerticalXmlImportObjectModel> IEnumVrtclXmlImportport = Enumerable.Empty<VerticalXmlImportObjectModel>();
            List<VerticalXmlImportObjectModel> lstVrtclXmlImport = new List<VerticalXmlImportObjectModel>();
            for(int i=0 ; i<2 ; i++)
            {
                VerticalXmlImportObjectModel objVrtclXmlImport = new VerticalXmlImportObjectModel();
                objVrtclXmlImport.ImportedBy = i;
                objVrtclXmlImport.ImportDate = DateTime.Today.AddDays(i);
                objVrtclXmlImport.ImportDescription = "Test_Desc"+i;
                objVrtclXmlImport.ImportedByName = "Test_Name"+i;
                objVrtclXmlImport.ImportTypes = i;
                objVrtclXmlImport.ImportXml = null;
                objVrtclXmlImport.Status = i;
                objVrtclXmlImport.VerticalXmlImportId = i;
                lstVrtclXmlImport.Add(objVrtclXmlImport);
            }

            IEnumVrtclXmlImportport = lstVrtclXmlImport;
           
            return IEnumVrtclXmlImportport;
        }

        private VerticalXmlImportObjectModel CreateVerticalXmlImportObjectModel()
        {
            VerticalXmlImportObjectModel objVrtclXmlImport = new VerticalXmlImportObjectModel();
            objVrtclXmlImport.ImportedBy = 1;
            objVrtclXmlImport.ImportDate = DateTime.Now;
            objVrtclXmlImport.ImportDescription = "Test_Desc";
            objVrtclXmlImport.ImportedByName = "Test_Name";
            objVrtclXmlImport.ImportTypes = 1;
            objVrtclXmlImport.ImportXml = null;
            objVrtclXmlImport.Status = 3;
            objVrtclXmlImport.VerticalXmlImportId = 1;
            return objVrtclXmlImport;
        }

        private IEnumerable<ErrorLogObjectModel> CreateErrorLogList()
        {
            IEnumerable<ErrorLogObjectModel> IEnumErrorLog = Enumerable.Empty<ErrorLogObjectModel>();
            List<ErrorLogObjectModel> lstErrorLog = new List<ErrorLogObjectModel>();
            ErrorLogObjectModel objErrorLog = new ErrorLogObjectModel();
            objErrorLog.ErrorCode = 123;
            objErrorLog.ErrorUtcDate = DateTime.Now;
            objErrorLog.Title = "Error_Title";
            objErrorLog.MachineName = "Error_Machn";
            objErrorLog.ProcessName = "Error_ProcessName";
            objErrorLog.Message = "Error_Message";
            objErrorLog.FormattedMessage = "Error_FormattedMsg";

            objErrorLog.ErrorLogId = 1;
            objErrorLog.Priority = 1;
            objErrorLog.Severity = "test";
            objErrorLog.AppDomainName = "Test";
            objErrorLog.ProcessID = "1";
            objErrorLog.ThreadName = "Test";
            objErrorLog.Win32ThreadId = "1";
            objErrorLog.EventId = 1;
            objErrorLog.SiteActivityID = 1;
            objErrorLog.URL = null;
            objErrorLog.AbsoluteURL = null;

            lstErrorLog.Add(objErrorLog);
            IEnumErrorLog = lstErrorLog;
            return IEnumErrorLog;
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
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
                mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.List();

            // Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
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
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlImportList().Take(1));
            mockUserfactorycache.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(CreateUserObjectModel());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
                mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair
                {
                  Display="Test_FirstName"+ " " + "Test_LastName",
                  Value = "1"
                  
                }
            };
            var result = objController.GetUsers();
            ValidateDisplayValuePair(lstExpected, result);

            // Verify and Assert
            mockVerticalXmlImportFactoryCache.VerifyAll();
            mockUserfactorycache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetUsers_Returns_EmptyJsonResult
        /// <summary>
        /// GetUsers_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetUsers_Returns_EmptyJsonResult()
        {
            IEnumerable<VerticalXmlImportObjectModel> IEnumVerticalXmlImportObjectModel = Enumerable.Empty<VerticalXmlImportObjectModel>();
           
            //Arrange
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(IEnumVerticalXmlImportObjectModel);
            
            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
                mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
                mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.GetUsers();
            ValidateEmptyData<UserObjectModel>(result);

            // Verify and Assert
            mockVerticalXmlImportFactoryCache.VerifyAll();
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
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<VerticalXmlImportSearchDetail>())).Returns(CreateVerticalXmlImportList());
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<VerticalXmlImportSearchDetail>(), It.IsAny<VerticalXmlImportSortDetail>())).Returns(CreateVerticalXmlImportList());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
              mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
              mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.Search(null, null, null);
            
            List<VerticalXmlImportViewModel> lstExpected = new List<VerticalXmlImportViewModel>();
            
               VerticalXmlImportViewModel viewModelexpextd1 = new VerticalXmlImportViewModel();
               viewModelexpextd1.ImportDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy hh:mm:ss");
                 viewModelexpextd1.ImportDescription =  "Test_Desc0";
                 viewModelexpextd1.ImportedBy = 0;
                 viewModelexpextd1.ImportedByName =  "Test_Name0";
                 viewModelexpextd1.ImportTypes = 0;
                 viewModelexpextd1.ImportXml = null;
                 viewModelexpextd1.StatusID = 0;
                 viewModelexpextd1.Status = "Job Not Started";
                 
                 viewModelexpextd1.VerticalXmlImportId = 0;
             lstExpected.Add(viewModelexpextd1);

             VerticalXmlImportViewModel viewModelexpextd2 = new VerticalXmlImportViewModel();
             viewModelexpextd2.ImportDate = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy hh:mm:ss");
                 viewModelexpextd2.ImportDescription =  "Test_Desc1";
                 viewModelexpextd2.ImportedBy = 1;
                 viewModelexpextd2.ImportedByName =  "Test_Name1";
                 viewModelexpextd2.ImportTypes = 1;
                 viewModelexpextd2.ImportXml = null;
                 viewModelexpextd2.Status = "Job In Progress";
                 viewModelexpextd2.StatusID = 1;
                 viewModelexpextd2.VerticalXmlImportId = 1;
            lstExpected.Add(viewModelexpextd2);


            ValidateData(lstExpected, result);
           

            // Verify and Assert
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Search_Returns_JsonResult_WithUserIdString
        /// <summary>
        /// Search_Returns_JsonResult_WithUserIdString
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_WithUserIdString()
        {
            
            IEnumerable<VerticalXmlImportObjectModel> IEnumVerticalXmlImportObjectModel = Enumerable.Empty<VerticalXmlImportObjectModel>();
            //Arrange
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<VerticalXmlImportSearchDetail>())).Returns(IEnumVerticalXmlImportObjectModel);
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<VerticalXmlImportSearchDetail>(), It.IsAny<VerticalXmlImportSortDetail>())).Returns(IEnumVerticalXmlImportObjectModel);

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
              mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
              mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.Search(null, null, "Test");

            // Verify and Assert
            ValidateEmptyData<VerticalXmlImportViewModel>(result);
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region Search_Returns_JsonResult_WithUserId
        /// <summary>
        /// Search_Returns_JsonResult_WithUserId
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_WithUserId()
        {
            
            //Arrange
            List<VerticalXmlImportViewModel> lstViewModelExpected = new List<VerticalXmlImportViewModel>();
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<VerticalXmlImportSearchDetail>())).Returns(CreateVerticalXmlImportList());
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<VerticalXmlImportSearchDetail>(), It.IsAny<VerticalXmlImportSortDetail>())).Returns(CreateVerticalXmlImportList());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
              mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
              mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            for(int i = 0 ; i<2 ; i++)
            {
                VerticalXmlImportViewModel viewModel = new VerticalXmlImportViewModel();
                
                    viewModel.ImportDate = DateTime.Today.AddDays(i).ToString("MM/dd/yyyy hh:mm:ss");
                    viewModel.ImportDescription = "Test_Desc"+i;
                    viewModel.ImportedBy = i;
                    viewModel.ImportedByName = "Test_Name"+i;
                    viewModel.ImportTypes = i;
                    viewModel.ImportXml = null;
                    viewModel.Status = i.ToString();
                    switch (i)
                    {
                        case 0:
                            viewModel.Status  = "Job Not Started";
                            break;
                        case 1:
                            viewModel.Status = "Job In Progress";
                            break;
                        default:
                            viewModel.Status = "Error";
                            break;
                    }
                    viewModel.StatusID = i;
                    viewModel.VerticalXmlImportId = i;
                    viewModel.ExportBackupId = null;
                    lstViewModelExpected.Add(viewModel);
                
            }
            
            var result = objController.Search(null, null, "1");

            ValidateData(lstViewModelExpected, result);
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetLatestJobStatus_Returns_Int
        /// <summary>
        /// GetLatestJobStatus_Returns_Int
        /// </summary>
        [TestMethod]
        public void GetLatestJobStatus_Returns_Int()
        {

            //Arrange
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlImportList());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
             mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
             mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var   result = objController.GetLatestJobStatus();
            Assert.AreEqual((int)result,1);
            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(Int32));
        }
        #endregion

        #region GetLatestJobStatus_EmptyIenumerable_Returns_Int
        /// <summary>
        /// GetLatestJobStatus_EmptyIenumerable_Returns_Int
        /// </summary>
        [TestMethod]
        public void GetLatestJobStatus_EmptyIenumerable_Returns_Int()
        {
            IEnumerable<VerticalXmlImportObjectModel> IEnumVerticalXmlImportObjectModel = Enumerable.Empty<VerticalXmlImportObjectModel>();
            //Arrange
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(IEnumVerticalXmlImportObjectModel);

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
             mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
             mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.GetLatestJobStatus();
            Assert.AreEqual((int)result, 0);

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(Int32));
        }
        #endregion

        #region GetLatestJobStatus_SameImportId_Returns_Int
        /// <summary>
        /// GetLatestJobStatus_SameImportId_Returns_Int
        /// </summary>
        [TestMethod]
        public void GetLatestJobStatus_SameImportId_Returns_Int()
        {
            IEnumerable<VerticalXmlImportObjectModel> IEnumVrtclXmlImportport = Enumerable.Empty<VerticalXmlImportObjectModel>();
            List<VerticalXmlImportObjectModel> lstVrtclXmlImport = new List<VerticalXmlImportObjectModel>();
            for (int i = 1; i < 3; i++)
            {
                VerticalXmlImportObjectModel objVrtclXmlImport = new VerticalXmlImportObjectModel();
                objVrtclXmlImport.ImportedBy = i;
                objVrtclXmlImport.ImportDate = DateTime.Now.AddDays(i);
                objVrtclXmlImport.ImportDescription = "Test_Desc" + i;
                objVrtclXmlImport.ImportedByName = "Test_Name" + i;
                objVrtclXmlImport.ImportTypes = i;
                objVrtclXmlImport.ImportXml = null;
                objVrtclXmlImport.Status = i;
                objVrtclXmlImport.VerticalXmlImportId = 0;
                lstVrtclXmlImport.Add(objVrtclXmlImport);
            }

            IEnumVrtclXmlImportport = lstVrtclXmlImport;
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(IEnumVrtclXmlImportport);

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
             mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
             mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.GetLatestJobStatus();
            Assert.AreEqual((int)result , 1);
            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(Int32));
        }
        #endregion

        #region Rollback_Returns_String_JobStatus_ImportCompleted
        /// <summary>
        /// Rollback_Returns_String_JobStatus_ImportCompleted
        /// </summary>
        [TestMethod]
        public void Rollback_Returns_String_JobStatus_ImportCompleted()
        {
            //Arrange
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlImportList());
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<Int32>())).Returns(CreateVerticalXmlImportObjectModel());
            mockVerticalXmlImportFactoryCache.Setup(x => x.SaveEntity(It.IsAny<VerticalXmlImportObjectModel>()));

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
          mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
          mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            string[] expectedResult = new string[] { "Rollback Job added to the Queue.", "Error: Cannot add Rollback Job to the Queue" };
            var result = objController.Rollback();
            Assert.AreEqual(result, expectedResult[0]);
            // Verify and Assert
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(string));
        }
        #endregion

        #region Rollback_Returns_String_JobStatus_RollBackNotStarted
        /// <summary>
        /// Rollback_Returns_String_JobStatus_RollBackNotStarted
        /// </summary>
        [TestMethod]
        public void Rollback_Returns_String_JobStatus_RollBackNotStarted()
        {
            //Arrange
            VerticalXmlImportObjectModel objVrtclXmlImport = new VerticalXmlImportObjectModel();
            objVrtclXmlImport.Status = 1;

            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlImportList());
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<Int32>())).Returns(objVrtclXmlImport);

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
          mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
          mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            string[] expectedResult = new string[] { "Rollback Job added to the Queue.", "Error: Cannot add Rollback Job to the Queue" };
            var result = objController.Rollback();
            Assert.AreEqual(result, expectedResult[1]);
            // Verify and Assert
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(string));
        }
        #endregion

        #region Rollback_Returns_String_JobStatus_RollBackNotStarted_Handles_Exception
        /// <summary>
        /// Rollback_Returns_String_JobStatus_RollBackNotStarted_Handles_Exception
        /// </summary>
        [TestMethod]
        public void Rollback_Returns_String_JobStatus_RollBackNotStarted_Handles_Exception()
        {
            //Arrange
            VerticalXmlImportObjectModel objVrtclXmlImport = new VerticalXmlImportObjectModel();
            objVrtclXmlImport.Status = 1;

            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Throws(new Exception());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
          mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
          mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            string[] expectedResult = new string[] { "Rollback Job added to the Queue.", "Error: Cannot add Rollback Job to the Queue" };
            var result = objController.Rollback();
            Assert.AreNotEqual(result, expectedResult[0]);

            // Verify and Assert
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(string));
        }
        #endregion

        #region GetErrorLogs_Returns_FileContentResult
        /// <summary>
        /// GetErrorLogs_Returns_FileContentResult
        /// </summary>
        [TestMethod]
        public void GetErrorLogs_Returns_FileContentResult()
        {
            //Arrange
            mockErrorLogFactor.Setup(x => x.GetEntitiesBySearch(It.IsAny<ErrorLogSearchDetail>())).Returns(CreateErrorLogList());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
         mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
         mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);

            var result = objController.GetErrorLogs(null);
           
            // Verify and Assert
            mockErrorLogFactor.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(FileContentResult));
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
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlImportList());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
        mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
        mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.Add();
            AddVerticalXmlImportViewModel objExpected = new AddVerticalXmlImportViewModel
            {
                ImportTypes = 0,
                ImportXml = null,
                ImportedBy = 0,
                ImportDescription = "Data Import on " + DateTime.Now.ToShortDateString(),
                Status = 0,
                VerticalXmlImportId = 0,
                InProgressJobCount = 2
            };

            // Verify and Assert
            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlImportViewModel;
            ValidateViewModelData<AddVerticalXmlImportViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
           
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Add_Returns_ActionResult_GetMethod_Status_NotStarted
        /// <summary>
        /// Add_Returns_ActionResult_GetMethod_Status_NotStarted
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_GetMethod_Status_NotStarted()
        {
            
            //Arrange
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlImportList());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
        mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
        mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.Add();
            AddVerticalXmlImportViewModel objExpected = new AddVerticalXmlImportViewModel
            {
                ImportTypes = 0,
                ImportXml = null,
                ImportedBy = 0,
                ImportDescription = "Data Import on " + DateTime.Now.ToShortDateString(),
                Status = 0,
                VerticalXmlImportId = 0,
                InProgressJobCount = 2
            };

            // Verify and Assert
            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlImportViewModel;
            ValidateViewModelData<AddVerticalXmlImportViewModel>(viewModel, objExpected);
           
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Add_Returns_ActionResult_GetMethod_Status_InProgress
        /// <summary>
        /// Add_Returns_ActionResult_GetMethod_Status_InProgress
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_GetMethod_Status_InProgress()
        {

            //Arrange
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlImportList());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
        mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
        mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.Add();
            AddVerticalXmlImportViewModel objExpected = new AddVerticalXmlImportViewModel
            {
                ImportTypes = 0,
                ImportXml = null,
                ImportedBy = 0,
                ImportDescription = "Data Import on " + DateTime.Now.ToShortDateString(),
                Status = 0,
                VerticalXmlImportId = 0,
                InProgressJobCount = 2
            };

            // Verify and Assert
            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlImportViewModel;
            ValidateViewModelData<AddVerticalXmlImportViewModel>(viewModel, objExpected);
            
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Add_Returns_ActionResult_GetMethod_Status_BackupCompleted
        /// <summary>
        /// Add_Returns_ActionResult_GetMethod_Status_BackupCompleted
        /// </summary>
        [TestMethod]
        public void Add_Returns_ActionResult_GetMethod_Status_BackupCompleted()
        {

            //Arrange
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlImportList());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
        mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
        mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.Add();
            AddVerticalXmlImportViewModel objExpected = new AddVerticalXmlImportViewModel
            {
                ImportTypes = 0,
                ImportXml = null,
                ImportedBy = 0,
                ImportDescription = "Data Import on " + DateTime.Now.ToShortDateString(),
                Status = 0,
                VerticalXmlImportId = 0,
                InProgressJobCount = 2
            };

            // Verify and Assert
            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlImportViewModel;
            ValidateViewModelData<AddVerticalXmlImportViewModel>(viewModel, objExpected);
            mockVerticalXmlImportFactoryCache.VerifyAll();
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
           
            mockVerticalXmlImportFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateVerticalXmlImportList());

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
              mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
              mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.Add(null);
            AddVerticalXmlImportViewModel objExpected = new AddVerticalXmlImportViewModel
            {
                       ImportTypes = 0,
                       ImportXml = null,
                       ImportedBy = 0,
                       ImportDescription =null,
                       Status=0,
                       VerticalXmlImportId =0,
                       InProgressJobCount = 2
           };

            // Verify and Assert
            var result1 = result as ViewResult;
            var viewModel = result1.Model as AddVerticalXmlImportViewModel;
            ValidateViewModelData<AddVerticalXmlImportViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            mockVerticalXmlImportFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetImportedXml_Returns_FileContentResult
        /// <summary>
        /// GetImportedXml_Returns_FileContentResult
        /// </summary>
        [TestMethod]
        public void GetImportedXml_Returns_FileContentResult()
        {
            //Arrange
            VerticalXmlImportObjectModel objVrtclXmlImport = new VerticalXmlImportObjectModel();
            objVrtclXmlImport.ImportedBy = 1;
            objVrtclXmlImport.ImportDate = DateTime.Now;
            objVrtclXmlImport.ImportDescription = "Test_Desc";
            objVrtclXmlImport.ImportedByName = "Test_Name";
            objVrtclXmlImport.ImportTypes = 1;
            objVrtclXmlImport.ImportXml = "Test";
            objVrtclXmlImport.Status = 3;
            objVrtclXmlImport.VerticalXmlImportId = 1;
            mockVerticalXmlImportFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objVrtclXmlImport);

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
        mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
        mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.GetImportedXml(null);

            // Verify and Assert
            mockVerticalXmlImportFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(FileContentResult));
        }
        #endregion

        #region GetImportedXml_Returns_FileContentResult_ForNullXml
        /// <summary>
        /// GetImportedXml_Returns_FileContentResult_ForNullXml
        /// </summary>
        [TestMethod]
        public void GetImportedXml_Returns_FileContentResult_ForNullXml()
        {
            //Arrange
            VerticalXmlImportObjectModel objVrtclXmlImport = new VerticalXmlImportObjectModel();
            objVrtclXmlImport.ImportXml = "";
            mockVerticalXmlImportFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objVrtclXmlImport);

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
        mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
        mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.GetImportedXml(null);

            // Verify and Assert
            mockVerticalXmlImportFactory.VerifyAll();
            Assert.AreEqual(result, null);
        }
        #endregion

        #region GetBackupXml_Returns_FileContentResult
        /// <summary>
        /// GetBackupXml_Returns_FileContentResult
        /// </summary>
        [TestMethod]
        public void GetBackupXml_Returns_FileContentResult()
        {
            //Arrange
            VerticalXmlExportObjectModel objVrtclXmlExport = new VerticalXmlExportObjectModel();
            objVrtclXmlExport.ExportXml = "Test";
            mockVerticalXmlExportFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objVrtclXmlExport);

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
        mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
        mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.GetBackupXml(null);

            // Verify and Assert
            mockVerticalXmlImportFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(FileContentResult));
        }
        #endregion

        #region GetBackupXml_Returns_FileContentResult_ForNullXml
        /// <summary>
        /// GetBackupXml_Returns_FileContentResult_ForNullXml
        /// </summary>
        [TestMethod]
        public void GetBackupXml_Returns_FileContentResult_ForNullXml()
        {
            //Arrange
            VerticalXmlExportObjectModel objVrtclXmlExport = new VerticalXmlExportObjectModel();
            objVrtclXmlExport.ExportXml = "";
            mockVerticalXmlExportFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objVrtclXmlExport);

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
        mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
        mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
            var result = objController.GetBackupXml(null);

            // Verify and Assert
            mockVerticalXmlImportFactory.VerifyAll();
            Assert.AreEqual(result, null);
        }
        #endregion

        #region GetBackupXml_Returns_FileContentResult_ForNullObject
        /// <summary>
        /// GetBackupXml_Returns_FileContentResult_ForNullObject
        /// </summary>
        [TestMethod]
        public void GetBackupXml_Returns_FileContentResult_ForNullObject()
        {
            //Arrange
            VerticalXmlExportObjectModel objVrtclXmlExport = null;
            
            mockVerticalXmlExportFactory.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objVrtclXmlExport);

            //Act
            VerticalXmlImportController objController = new VerticalXmlImportController(mockUserfactorycache.Object,
        mockVerticalXmlImportFactoryCache.Object, mockVerticalXmlImportFactory.Object,
        mockVerticalXmlExportFactoryCache.Object, mockVerticalXmlExportFactory.Object, mockErrorLogFactor.Object);
          

            var result = objController.GetBackupXml(null);

            // Verify and Assert
            mockVerticalXmlImportFactory.VerifyAll();
            Assert.AreEqual(result, null);
        }
        #endregion
    }
}
