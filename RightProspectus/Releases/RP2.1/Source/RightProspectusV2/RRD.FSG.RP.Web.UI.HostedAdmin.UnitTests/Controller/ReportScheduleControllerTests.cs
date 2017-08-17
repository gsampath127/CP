using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using Moq;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System.Web.Mvc;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System.Web;
using System.Security.Principal;
using System.Web.Routing;
using RRD.FSG.RP.Model.Interfaces;
using System.Web.Script.Serialization;
namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for Report schedule controller
    /// </summary>
    [TestClass]
    public class ReportScheduleControllerTests : BaseTestController<ReportScheduleViewModel>
    {
        Mock<IFactory<ReportScheduleObjectModel, int>> mockreportScheduleCacheFactory;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockuserCacheFactory;

        [TestInitialize]
        public void TestInitialze()
        {
            mockuserCacheFactory = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mockreportScheduleCacheFactory = new Mock<IFactory<ReportScheduleObjectModel, int>>();
        }

        #region EntityMockData_Methods
        /// <summary>
        /// Mocks Value for ReportSchedule Object Model
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportScheduleObjectModel> GetReportData()
        {
            IEnumerable<ReportScheduleObjectModel> enumReportScheduleObjects = Enumerable.Empty<ReportScheduleObjectModel>();
            List<ReportScheduleObjectModel> lstReportScheduleObjects = new List<ReportScheduleObjectModel>();
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ClientId = 0;
            objReportScheduleObjectModel.Description = "";
            objReportScheduleObjectModel.Email = "";
            objReportScheduleObjectModel.FrequencyDescription = "";
            objReportScheduleObjectModel.FrequencyInterval = 0;
            objReportScheduleObjectModel.FrequencyType = 0;
            objReportScheduleObjectModel.FTPFilePath = "";
            objReportScheduleObjectModel.FTPPassword = "";
            objReportScheduleObjectModel.FTPServerIP = "";
            objReportScheduleObjectModel.FTPUsername = "";
            objReportScheduleObjectModel.IsEnabled = true;
            objReportScheduleObjectModel.Name = "";
            objReportScheduleObjectModel.ReportId = 1;
            objReportScheduleObjectModel.ReportScheduleId = 1;
            objReportScheduleObjectModel.ReportName = "";
            objReportScheduleObjectModel.UtcFirstScheduledRunDate = DateTime.Parse("02/02/2015");
            objReportScheduleObjectModel.UtcLastActualRunDate = DateTime.Parse("02/02/2015");
            objReportScheduleObjectModel.UtcLastScheduledRunDate = DateTime.Parse("02/02/2015");
            objReportScheduleObjectModel.UtcNextScheduledRunDate = DateTime.Parse("02/02/2015");
            lstReportScheduleObjects.Add(objReportScheduleObjectModel);
            enumReportScheduleObjects = lstReportScheduleObjects;
            return enumReportScheduleObjects;
        }

        /// <summary>
        /// Returns mock view model data
        /// </summary>
        /// <returns></returns>
        public EditReportScheduleViewModel GetViewModelData()
        {
            EditReportScheduleViewModel objEditViewModel = new EditReportScheduleViewModel();
            List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();
            DisplayValuePair objDisplayValuePair = new DisplayValuePair();
            objDisplayValuePair.Display = "";
            objDisplayValuePair.Value = "";
            objDisplayValuePair.Selected = false;
            lstDisplayValuePair.Add(objDisplayValuePair);
            objEditViewModel.Email = "";
            objEditViewModel.FrequencyDescription = "";
            objEditViewModel.FrequencyInterval = 0;
            objEditViewModel.FrequencyType = lstDisplayValuePair;
            objEditViewModel.FTPFilePath = "";
            objEditViewModel.FTPPassword = "";
            objEditViewModel.FTPServerIP = "";
            objEditViewModel.FTPUsername = "";
            objEditViewModel.IsEnabled = true;
            objEditViewModel.ReportName = lstDisplayValuePair;
            objEditViewModel.ReportScheduleId = 0;
            objEditViewModel.SelectedFrequencyType = 0;
            objEditViewModel.SelectedReportId = 0;
            objEditViewModel.SelectedTransferType = 0;
            objEditViewModel.SuccessOrFailedMessage = "";
            objEditViewModel.TransferType = lstDisplayValuePair;
            objEditViewModel.UtcFirstScheduledRunDate = DateTime.Now.ToString();
            objEditViewModel.UTCLastModifiedDate = DateTime.Now;
            objEditViewModel.UtcLastScheduledRunDate = DateTime.Now.ToString();
            objEditViewModel.UtcNextScheduledRunDate = DateTime.Now.ToString();
            objEditViewModel.ModifiedBy = 1;
            objEditViewModel.ModifiedByName = "Test_Name";
            return objEditViewModel;


        }

        public IEnumerable<UserObjectModel> GetUserObjectModelData()
        {
            IEnumerable<UserObjectModel> enumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            List<RolesObjectModel> lstRolesObjectModel = new List<RolesObjectModel>();
            RolesObjectModel objRolesObjectModel = new RolesObjectModel();
            objRolesObjectModel.Description = "";
            lstRolesObjectModel.Add(objRolesObjectModel);
            List<int> lstint = new List<int>();
            int a = 0;
            lstint.Add(a);
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.AccessFailedCount = 0;
            objUserObjectModel.Clients = lstint;
            objUserObjectModel.Description = "";
            objUserObjectModel.Email = "";
            objUserObjectModel.EmailConfirmed = true;
            objUserObjectModel.FirstName = "";
            objUserObjectModel.LastName = "";
            objUserObjectModel.LockoutEnabled = true;
            objUserObjectModel.LockoutEndDateUtc = DateTime.Now;
            objUserObjectModel.Name = "";
            objUserObjectModel.PasswordHash = "";
            objUserObjectModel.PhoneNumber = "";
            objUserObjectModel.PhoneNumberConfirmed = true;
            objUserObjectModel.Roles = lstRolesObjectModel;
            objUserObjectModel.SecurityStamp = "";
            objUserObjectModel.TwoFactorEnabled = true;
            objUserObjectModel.UserId = 0;
            objUserObjectModel.UserName = "";
            lstUserObjectModel.Add(objUserObjectModel);
            enumUserObjectModel = lstUserObjectModel;
            return enumUserObjectModel;

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
            ReportScheduleController obj = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = obj.List();
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditReportSchedule_Returns_ActionResult_Pass_0
        /// <summary>
        /// EditReportSchedule_Returns_ActionResult_Pass_0
        /// </summary>
        [TestMethod]
        public void EditReportSchedule_Returns_ActionResult_Pass_0()
        {
            //Arrange
            //mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ReportScheduleSearchDetail>(), It.IsAny<ReportScheduleSortDetail>()))
            //    .Returns(GetReportData());

            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), null))
                .Returns(GetReportData());
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.EditReportSchedule(0);
            //Verify and Assert

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "--Please select Frequency Type-", Value = "-1" });
            lstexpected.Add(new DisplayValuePair() { Display = "RunOnce", Value = "1" });
            lstexpected.Add(new DisplayValuePair() { Display = "EveryXDays", Value = "2" });
            lstexpected.Add(new DisplayValuePair() { Display = "Weekly", Value = "3" });
            lstexpected.Add(new DisplayValuePair() { Display = "Monthly", Value = "4" });
            lstexpected.Add(new DisplayValuePair() { Display = "Quarterly", Value = "5" });
            lstexpected.Add(new DisplayValuePair() { Display = "BiAnnually", Value = "6" });
            lstexpected.Add(new DisplayValuePair() { Display = "Annually", Value = "7" });

            List<DisplayValuePair> lstexpectedReport = new List<DisplayValuePair>();
            lstexpectedReport.Add(new DisplayValuePair() { Display = "--Please select Report--", Value = "-1" });
            lstexpectedReport.Add(new DisplayValuePair() { Display = "", Value = "1" });

            List<DisplayValuePair> lstexpectedTransfertype = new List<DisplayValuePair>();
            lstexpectedTransfertype.Add(new DisplayValuePair() { Display = "--Please select Transfer type--", Value = "-1" });
            lstexpectedTransfertype.Add(new DisplayValuePair() { Display = "Email", Value = "0" });
            lstexpectedTransfertype.Add(new DisplayValuePair() { Display = "SFTP", Value = "1" });


            EditReportScheduleViewModel obj = new EditReportScheduleViewModel();
            obj.Email = null;
            obj.FrequencyDescription = null;
            obj.FrequencyInterval = 0;
            obj.FrequencyType = lstexpected;
            obj.FTPFilePath = null;
            obj.FTPPassword = null;
            obj.FTPServerIP = null;
            obj.FTPUsername = null;
            obj.IsEnabled = false;
            obj.ModifiedBy = null;
            obj.ModifiedByName = null;
            obj.ReportName = lstexpectedReport;
            obj.SelectedFrequencyType = -1;
            obj.SelectedReportId = -1;
            obj.SelectedTransferType = -1;
            obj.SuccessOrFailedMessage = null;
            obj.TransferType = lstexpectedTransfertype;
            obj.UtcFirstScheduledRunDate = null;
            obj.UTCLastModifiedDate = null;
            obj.UtcLastScheduledRunDate = null;
            obj.UtcNextScheduledRunDate = null;
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditReportScheduleViewModel;
            ValidateViewModelData<EditReportScheduleViewModel>(viewModel, obj);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditReportScheduleViewModel));

            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditReportSchedule_Returns_ActionResult_Pass_Id_With_Email
        /// <summary>
        /// EditReportSchedule_Returns_ActionResult_Pass_Id_With_Email
        /// </summary>
        [TestMethod]
        public void EditReportSchedule_Returns_ActionResult_Pass_Id_With_Email()
        {
            //Arrange

            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ClientId = 1;
            objReportScheduleObjectModel.ReportName = "Test";
            objReportScheduleObjectModel.Email = "ahhaha@hha.com";
            objReportScheduleObjectModel.UtcFirstScheduledRunDate = DateTime.Now;

            mockreportScheduleCacheFactory.Setup(x => x.GetEntityByKey(1))
                .Returns(objReportScheduleObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>()))
                .Returns(GetUserObjectModelData());

            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.EditReportSchedule(1);

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "--Please select Frequency Type-", Value = "-1" });
            lstexpected.Add(new DisplayValuePair() { Display = "RunOnce", Value = "1" });
            lstexpected.Add(new DisplayValuePair() { Display = "EveryXDays", Value = "2" });
            lstexpected.Add(new DisplayValuePair() { Display = "Weekly", Value = "3" });
            lstexpected.Add(new DisplayValuePair() { Display = "Monthly", Value = "4" });
            lstexpected.Add(new DisplayValuePair() { Display = "Quarterly", Value = "5" });
            lstexpected.Add(new DisplayValuePair() { Display = "BiAnnually", Value = "6" });
            lstexpected.Add(new DisplayValuePair() { Display = "Annually", Value = "7" });

            List<DisplayValuePair> lstexpectedReport = new List<DisplayValuePair>();
            lstexpectedReport.Add(new DisplayValuePair() { Display = "--Please select Report--", Value = "-1" });

            List<DisplayValuePair> lstexpectedTransfertype = new List<DisplayValuePair>();
            lstexpectedTransfertype.Add(new DisplayValuePair() { Display = "--Please select Transfer type--", Value = "-1" });
            lstexpectedTransfertype.Add(new DisplayValuePair() { Display = "Email", Value = "0" });
            lstexpectedTransfertype.Add(new DisplayValuePair() { Display = "SFTP", Value = "1" });


            EditReportScheduleViewModel obj = new EditReportScheduleViewModel();
            obj.Email = "ahhaha@hha.com";
            obj.FrequencyDescription = null;
            obj.FrequencyInterval = 0;
            obj.FrequencyType = lstexpected;
            obj.FTPFilePath = null;
            obj.FTPPassword = null;
            obj.FTPServerIP = null;
            obj.FTPUsername = null;
            obj.IsEnabled = false;
            obj.ModifiedBy = null;
            obj.ModifiedByName = "";
            obj.ReportName = lstexpectedReport;
            obj.ReportScheduleId = 0;
            obj.SelectedFrequencyType = 0;
            obj.SelectedReportId = 0;
            obj.SelectedTransferType = 0;
            obj.SuccessOrFailedMessage = null;
            obj.TransferType = lstexpectedTransfertype;
            obj.UtcFirstScheduledRunDate = DateTime.Now.ToString();
            obj.UTCLastModifiedDate = null;
            obj.UtcLastScheduledRunDate = null;
            obj.UtcNextScheduledRunDate = null;
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditReportScheduleViewModel;
            ValidateViewModelData<EditReportScheduleViewModel>(viewModel, obj);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditReportScheduleViewModel));


            //Verify and Assert
            mockreportScheduleCacheFactory.VerifyAll();
            mockuserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditReportSchedule_Returns_ActionResult_Pass_Id_With_Email_NULL
        /// <summary>
        /// EditReportSchedule_Returns_ActionResult_Pass_Id_With_Email_NULL
        /// </summary>
        [TestMethod]
        public void EditReportSchedule_Returns_ActionResult_Pass_Id_With_Email_NULL()
        {
            //Arrange
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ClientId = 1;
            objReportScheduleObjectModel.ReportName = "Test";
            objReportScheduleObjectModel.Email = "";
            objReportScheduleObjectModel.UtcFirstScheduledRunDate = DateTime.Now;

            mockreportScheduleCacheFactory.Setup(x => x.GetEntityByKey(1))
                .Returns(objReportScheduleObjectModel);
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>()))
                .Returns(GetUserObjectModelData());

            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.EditReportSchedule(1);

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "--Please select Frequency Type-", Value = "-1" });
            lstexpected.Add(new DisplayValuePair() { Display = "RunOnce", Value = "1" });
            lstexpected.Add(new DisplayValuePair() { Display = "EveryXDays", Value = "2" });
            lstexpected.Add(new DisplayValuePair() { Display = "Weekly", Value = "3" });
            lstexpected.Add(new DisplayValuePair() { Display = "Monthly", Value = "4" });
            lstexpected.Add(new DisplayValuePair() { Display = "Quarterly", Value = "5" });
            lstexpected.Add(new DisplayValuePair() { Display = "BiAnnually", Value = "6" });
            lstexpected.Add(new DisplayValuePair() { Display = "Annually", Value = "7" });

            List<DisplayValuePair> lstexpectedReport = new List<DisplayValuePair>();
            lstexpectedReport.Add(new DisplayValuePair() { Display = "--Please select Report--", Value = "-1" });

            List<DisplayValuePair> lstexpectedTransfertype = new List<DisplayValuePair>();
            lstexpectedTransfertype.Add(new DisplayValuePair() { Display = "--Please select Transfer type--", Value = "-1" });
            lstexpectedTransfertype.Add(new DisplayValuePair() { Display = "Email", Value = "0" });
            lstexpectedTransfertype.Add(new DisplayValuePair() { Display = "SFTP", Value = "1" });


            EditReportScheduleViewModel obj = new EditReportScheduleViewModel();
            obj.Email = "";
            obj.FrequencyDescription = null;
            obj.FrequencyInterval = 0;
            obj.FrequencyType = lstexpected;
            obj.FTPFilePath = null;
            obj.FTPPassword = null;
            obj.FTPServerIP = null;
            obj.FTPUsername = null;
            obj.IsEnabled = false;
            obj.ModifiedBy = null;
            obj.ModifiedByName = "";
            obj.ReportName = lstexpectedReport;
            obj.ReportScheduleId = 0;
            obj.SelectedFrequencyType = 0;
            obj.SelectedReportId = 0;
            obj.SelectedTransferType = 1;
            obj.SuccessOrFailedMessage = null;
            obj.TransferType = lstexpectedTransfertype;
            obj.UtcFirstScheduledRunDate = DateTime.Now.ToString();
            obj.UTCLastModifiedDate = null;
            obj.UtcLastScheduledRunDate = null;
            obj.UtcNextScheduledRunDate = null;
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditReportScheduleViewModel;
            ValidateViewModelData<EditReportScheduleViewModel>(viewModel, obj);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditReportScheduleViewModel));

            //Verify and Assert
            mockreportScheduleCacheFactory.VerifyAll();
            mockuserCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditReportSchedule_Post_Returns_ActionResult
        /// <summary>
        /// EditReportSchedule_Post_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void EditReportSchedule_Post_Returns_ActionResult()
        {
            //Arrange
            EditReportScheduleViewModel objEditViewModel = new EditReportScheduleViewModel();
            objEditViewModel.Email = "";
            objEditViewModel.FrequencyDescription = "";
            objEditViewModel.FrequencyInterval = 0;
            objEditViewModel.FTPFilePath = "";
            objEditViewModel.FTPPassword = "";
            objEditViewModel.FTPServerIP = "";
            objEditViewModel.FTPUsername = "";
            objEditViewModel.IsEnabled = true;
            objEditViewModel.ReportScheduleId = 0;
            objEditViewModel.SelectedFrequencyType = 0;
            objEditViewModel.SelectedReportId = 0;
            objEditViewModel.SelectedTransferType = 0;
            objEditViewModel.SuccessOrFailedMessage = "";
            objEditViewModel.UtcFirstScheduledRunDate = DateTime.Now.ToString();
            objEditViewModel.UTCLastModifiedDate = DateTime.Now;
            objEditViewModel.UtcLastScheduledRunDate = DateTime.Now.ToString();
            objEditViewModel.UtcNextScheduledRunDate = DateTime.Now.ToString();
            objEditViewModel.ModifiedBy = 1;
            objEditViewModel.ModifiedByName = "Test_Name";


            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ReportId = 1;
            objReportScheduleObjectModel.ReportName = "Test1";
            objReportScheduleObjectModel.ClientId = 1;
            objReportScheduleObjectModel.FrequencyInterval = 0;
            objReportScheduleObjectModel.UtcLastScheduledRunDate = DateTime.Now;

            mockreportScheduleCacheFactory.Setup(x => x.SaveEntity(It.IsAny<ReportScheduleObjectModel>(), It.IsAny<int>()));
            // mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ReportScheduleSearchDetail>(), It.IsAny<ReportScheduleSortDetail>())).Returns(GetReportData());
            //  mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(0, -1, null));


            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.EditReportSchedule(new EditReportScheduleViewModel());

            //Verify and Assert
            mockreportScheduleCacheFactory.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditReportSchedule_Post_Returns_ActionResult_CatchException
        /// <summary>
        /// EditReportSchedule_Post_Returns_ActionResult_CatchException
        /// </summary>
        [TestMethod]
        public void EditReportSchedule_Post_Returns_ActionResult_CatchException()
        {
            //Arrange
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ReportId = 1;
            objReportScheduleObjectModel.ReportName = "Test1";

            mockreportScheduleCacheFactory.Setup(x => x.SaveEntity(It.IsAny<ReportScheduleObjectModel>(), It.IsAny<int>())).Throws(new Exception());
            // mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ReportScheduleSearchDetail>(), It.IsAny<ReportScheduleSortDetail>())).Returns(GetReportData());
            //mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(0, -1, null))
            //    .Returns(GetReportData());

            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.EditReportSchedule(GetViewModelData());

            //Verify and Assert
            mockreportScheduleCacheFactory.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditReportSchedule_HandlesException
        /// <summary>
        /// EditReportSchedule_HandlesException
        /// </summary>
        [TestMethod]
        public void EditReportSchedule_HandlesException()
        {
            //Arrange
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ReportScheduleSearchDetail>(), It.IsAny<ReportScheduleSortDetail>())).Throws(new Exception());

            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.EditReportSchedule(GetViewModelData());

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetAllReportScheduleDetails_Post_Returns_JsonResult
        /// <summary>
        /// GetAllReportScheduleDetails_Post_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetAllReportScheduleDetails_Post_Returns_JsonResult()
        {
            //Arrange

            IEnumerable<ReportScheduleObjectModel> enumReportScheduleObjects;
            List<ReportScheduleObjectModel> lstReportScheduleObjects = new List<ReportScheduleObjectModel>();
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ReportScheduleId = 1;
            objReportScheduleObjectModel.UtcFirstScheduledRunDate = DateTime.Parse("01/02/2015");
            objReportScheduleObjectModel.UtcLastScheduledRunDate = DateTime.Now;
            objReportScheduleObjectModel.UtcNextScheduledRunDate = DateTime.Parse("01/02/2015").AddDays(-5);
            lstReportScheduleObjects.Add(objReportScheduleObjectModel);
            enumReportScheduleObjects = lstReportScheduleObjects;

            int i = 1;
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ReportScheduleSearchDetail>(), It.IsAny<ReportScheduleSortDetail>(), out i))
                .Returns(enumReportScheduleObjects);
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetAllReportScheduleDetails("Test", "Test", "True", DateTime.Parse("01/02/2015").ToString(), DateTime.Parse("01/02/2015").AddDays(-5).ToString(), 0);

            //Verify and Assert
            List<ReportScheduleViewModel> lstExpected = new List<ReportScheduleViewModel>();
            ReportScheduleViewModel obj = new ReportScheduleViewModel();
            obj.ClientId = 0;
            obj.FrequencyDescription = null;
            obj.FrequencyInterval = 0;
            obj.FrequencyType = null;
            obj.IsEnabled = "False";
            obj.ReportId = 0;
            obj.ReportName = null;
            obj.ReportScheduleId = 1;
            obj.SuccessOrFailedMessage = null;
            obj.UtcFirstScheduledRunDate = DateTime.Parse("01/02/2015").ToString();
            obj.UtcLastScheduledRunDate = null;
            obj.UtcLastActualRunDate = null;
            obj.UtcNextScheduledRunDate = DateTime.Parse("01/02/2015").AddDays(-5).ToString();
            lstExpected.Add(obj);

            ValidateData(lstExpected, result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllReportScheduleDetails_Post_Returns_JsonResult_IsEnabledFalse
        /// <summary>
        /// GetAllReportScheduleDetails_Post_Returns_JsonResult_IsEnabledFalse
        /// </summary>
        [TestMethod]
        public void GetAllReportScheduleDetails_Post_Returns_JsonResult_IsEnabledFalse()
        {
            //Arrange

            IEnumerable<ReportScheduleObjectModel> enumReportScheduleObjects;
            List<ReportScheduleObjectModel> lstReportScheduleObjects = new List<ReportScheduleObjectModel>();
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ReportScheduleId = 1;
            objReportScheduleObjectModel.UtcFirstScheduledRunDate = DateTime.Parse("02/02/2015");
            objReportScheduleObjectModel.UtcLastScheduledRunDate = DateTime.Parse("02/02/2015");
            objReportScheduleObjectModel.UtcNextScheduledRunDate = DateTime.Parse("02/02/2015");
            lstReportScheduleObjects.Add(objReportScheduleObjectModel);
            enumReportScheduleObjects = lstReportScheduleObjects;
            int i = 1;
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ReportScheduleSearchDetail>(), It.IsAny<ReportScheduleSortDetail>(), out i))
                .Returns(GetReportData());
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetAllReportScheduleDetails(It.IsAny<string>(), It.IsAny<string>(), "False", DateTime.Parse("02/02/2015").ToString(), DateTime.Parse("02/02/2015").AddDays(-5).ToString(), 0);

            //Verify and Assert
            List<ReportScheduleViewModel> lstExpected = new List<ReportScheduleViewModel>();
            ReportScheduleViewModel obj = new ReportScheduleViewModel();
            obj.ClientId = 0;
            obj.FrequencyDescription = null;
            obj.FrequencyInterval = 0;
            obj.FrequencyType = null;
            obj.IsEnabled = "True";
            obj.ReportId = 1;
            obj.ReportName = "";
            obj.ReportScheduleId = 1;
            obj.SuccessOrFailedMessage = null;
            obj.UtcFirstScheduledRunDate = DateTime.Parse("02/02/2015").ToString();
            obj.UtcLastScheduledRunDate = null;
            obj.UtcLastActualRunDate = DateTime.Parse("02/02/2015").ToString();
            obj.UtcNextScheduledRunDate = DateTime.Parse("02/02/2015").ToString();
            lstExpected.Add(obj);

            ValidateData(lstExpected, result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllReportScheduleDetails_Post_Returns_JsonResult_IsEnabledTrue
        /// <summary>
        /// GetAllReportScheduleDetails_Post_Returns_JsonResult_IsEnabledTrue
        /// </summary>
        [TestMethod]
        public void GetAllReportScheduleDetails_Post_Returns_JsonResult_IsEnabledTrue()
        {
            //Arrange
            int i = 0;
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ReportScheduleSearchDetail>(), It.IsAny<ReportScheduleSortDetail>(), out i))
                .Returns(GetReportData());
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetAllReportScheduleDetails(It.IsAny<string>(), It.IsAny<string>(), "True", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());

            //Verify and Assert
            List<ReportScheduleViewModel> lstExpected = new List<ReportScheduleViewModel>();
            ReportScheduleViewModel obj = new ReportScheduleViewModel();
            obj.ClientId = 0;
            obj.FrequencyDescription = null;
            obj.FrequencyInterval = 0;
            obj.FrequencyType = null;
            obj.IsEnabled = "False";
            obj.ReportId = 0;
            obj.ReportName = null;
            obj.ReportScheduleId = 1;
            obj.SuccessOrFailedMessage = null;
            obj.UtcFirstScheduledRunDate = DateTime.Now.ToString();
            obj.UtcLastScheduledRunDate = null;
            obj.UtcLastActualRunDate = null;
            obj.UtcNextScheduledRunDate = DateTime.Now.AddDays(-5).ToString();
            lstExpected.Add(obj);

            ValidateEmptyData<ReportScheduleViewModel>(result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllReportScheduleDetails_Post_Returns_JsonResult_entities_IsEnabledEmpty
        /// <summary>
        /// GetAllReportScheduleDetails_Post_Returns_JsonResult_entities
        /// </summary>
        [TestMethod]
        public void GetAllReportScheduleDetails_Post_Returns_JsonResult_entities_IsEnabledEmpty()
        {
            //Arrange
            List<ReportScheduleObjectModel> lstReportScheduleObjectModel = new List<ReportScheduleObjectModel>();
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            lstReportScheduleObjectModel.Add(objReportScheduleObjectModel);
            IEnumerable<ReportScheduleObjectModel> ienum = lstReportScheduleObjectModel;

            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);

            var result = objReportSchedule.GetAllReportScheduleDetails("Test", "1", "Test", DateTime.Now.ToString(), DateTime.Now.ToString(), 1);
            //Verify and Assert
            ValidateEmptyData<ReportScheduleViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllReportScheduleDetails_Post_Returns_JsonResult_NextDateNull
        /// <summary>
        /// GetAllReportScheduleDetails_Post_Returns_JsonResult_NextDateNull
        /// </summary>
        [TestMethod]
        public void GetAllReportScheduleDetails_Post_Returns_JsonResult_NextDateNull()
        {
            //Arrange

            IEnumerable<ReportScheduleObjectModel> enumReportScheduleObjects;
            List<ReportScheduleObjectModel> lstReportScheduleObjects = new List<ReportScheduleObjectModel>();
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ReportScheduleId = 1;
            objReportScheduleObjectModel.UtcFirstScheduledRunDate = DateTime.Parse("02/02/2015");
            objReportScheduleObjectModel.UtcLastScheduledRunDate = DateTime.Now;
            objReportScheduleObjectModel.UtcNextScheduledRunDate = null;
            lstReportScheduleObjects.Add(objReportScheduleObjectModel);
            enumReportScheduleObjects = lstReportScheduleObjects;

            int i = 1;
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ReportScheduleSearchDetail>(), It.IsAny<ReportScheduleSortDetail>(), out i))
                .Returns(enumReportScheduleObjects);
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetAllReportScheduleDetails("Test", "Test", "True", DateTime.Parse("02/02/2015").ToString(), DateTime.Now.AddDays(-5).ToString(), 0);


            //Verify and Assert
            List<ReportScheduleViewModel> lstExpected = new List<ReportScheduleViewModel>();
            ReportScheduleViewModel obj = new ReportScheduleViewModel();
            obj.ClientId = 0;
            obj.FrequencyDescription = null;
            obj.FrequencyInterval = 0;
            obj.FrequencyType = null;
            obj.IsEnabled = "False";
            obj.ReportId = 0;
            obj.ReportName = null;
            obj.ReportScheduleId = 1;
            obj.SuccessOrFailedMessage = null;
            obj.UtcFirstScheduledRunDate = DateTime.Parse("02/02/2015").ToString();
            obj.UtcLastScheduledRunDate = null;
            obj.UtcLastActualRunDate = null;
            obj.UtcNextScheduledRunDate = null;
            lstExpected.Add(obj);

            ValidateData(lstExpected, result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFrequencyType_Returns_JsonResult
        /// <summary>
        /// GetFrequencyType_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetFrequencyType_Returns_JsonResult()
        {
            //Arrange

            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetFrequencyType();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "RunOnce", Value = "1" });
            lstexpected.Add(new DisplayValuePair() { Display = "EveryXDays", Value = "2" });
            lstexpected.Add(new DisplayValuePair() { Display = "Weekly", Value = "3" });
            lstexpected.Add(new DisplayValuePair() { Display = "Monthly", Value = "4" });
            lstexpected.Add(new DisplayValuePair() { Display = "Quarterly", Value = "5" });
            lstexpected.Add(new DisplayValuePair() { Display = "BiAnnually", Value = "6" });
            lstexpected.Add(new DisplayValuePair() { Display = "Annually", Value = "7" });
            ValidateDisplayValuePair(lstexpected, result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetReportName_Returns_JsonResult
        /// <summary>
        /// GetReportName_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetReportName_Returns_JsonResult()
        {
            //Arrange
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ReportScheduleSearchDetail>())).Returns(GetReportData());
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetReportName();

            // Verify and Assert
            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair obj = new DisplayValuePair();
            obj.Display = "";
            obj.Selected = false;
            obj.Value = "";
            lst.Add(obj);
            ValidateDisplayValuePair(lst, result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetReportName_Returns_JsonResult_Null
        /// <summary>
        /// GetReportName_Returns_JsonResult_Null
        /// </summary>
        [TestMethod]
        public void GetReportName_Returns_JsonResult_Null()
        {
            //Arrange

            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetReportName();

            //Verify and Assert
            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair obj = new DisplayValuePair();
            lst.Add(obj);
            ValidateEmptyData<DisplayValuePair>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFrequencyInterval_Returns_JsonResult
        /// <summary>
        /// GetFrequencyInterval_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetFrequencyInterval_Returns_JsonResult()
        {
            //Arrange
            mockreportScheduleCacheFactory.Setup(x => x.GetAllEntities()).Returns(GetReportData());
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetFrequencyInterval();

            //Verify and Assert
            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair objDisplayValuePair = new DisplayValuePair();
            lst.Add(objDisplayValuePair);
            ValidateEmptyData<DisplayValuePair>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFrequencyInterval_Returns_JsonResult_Null
        /// <summary>
        /// GetFrequencyInterval_Returns_JsonResult_Null
        /// </summary>
        [TestMethod]
        public void GetFrequencyInterval_Returns_JsonResult_Null()
        {
            //Arrange

            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetFrequencyInterval();

            //Verify and Assert
            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair obj = new DisplayValuePair();
            lst.Add(obj);
            ValidateEmptyData<DisplayValuePair>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetIsEnabled_Returns_JsonResult
        /// <summary>
        /// GetIsEnabled
        /// </summary>
        [TestMethod]
        public void GetIsEnabled_Returns_JsonResult()
        {
            //Arrange

            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ReportScheduleSearchDetail>()))
                .Returns(GetReportData());
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetIsEnabled();

            //Verify and Assert
            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair obj = new DisplayValuePair();
            obj.Display = "True";
            obj.Selected = false;
            obj.Value = "True";
            lst.Add(obj);
            ValidateDisplayValuePair(lst, result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetIsEnabled_Returns_JsonResult_Null
        /// <summary>
        /// GetIsEnabled_Returns_JsonResult_Null
        /// </summary>
        [TestMethod]
        public void GetIsEnabled_Returns_JsonResult_Null()
        {
            //Arrange
            List<ReportScheduleObjectModel> lstReportScheduleObjects = new List<ReportScheduleObjectModel>();
            IEnumerable<ReportScheduleObjectModel> IenumReportScheduleObjects = lstReportScheduleObjects;
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ReportScheduleSearchDetail>())).Returns(IenumReportScheduleObjects);
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetIsEnabled();

            //Verify and Assert
            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair obj = new DisplayValuePair();
            lst.Add(obj);
            ValidateEmptyData<DisplayValuePair>(result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFirstScheduleRunDate_Returns_JsonResult
        /// <summary>
        /// GetFirstScheduleRunDate_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetFirstScheduleRunDate_Returns_JsonResult()
        {
            IEnumerable<ReportScheduleObjectModel> enumReportScheduleObjects = Enumerable.Empty<ReportScheduleObjectModel>();
            List<ReportScheduleObjectModel> lstReportScheduleObjects = new List<ReportScheduleObjectModel>();
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.UtcNextScheduledRunDate = DateTime.Parse("02-Feb-2015");
            lstReportScheduleObjects.Add(objReportScheduleObjectModel);
            enumReportScheduleObjects = lstReportScheduleObjects;
            //Arrange
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ReportScheduleSearchDetail>()))
                .Returns(enumReportScheduleObjects);
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetFirstScheduleRunDate();

            //Verify and Assert
            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair obj = new DisplayValuePair();

            ValidateDisplayValuePair(lst, result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFirstScheduleRunDate_Returns_JsonResultUtcFirstScheduledRunDateNull
        /// <summary>
        /// GetFirstScheduleRunDate_Returns_JsonResultUtcFirstScheduledRunDateNull
        /// </summary>
        [TestMethod]
        public void GetFirstScheduleRunDate_Returns_JsonResultUtcFirstScheduledRunDateNull()
        {
            //Arrange
            IEnumerable<ReportScheduleObjectModel> enumReportScheduleObjects = Enumerable.Empty<ReportScheduleObjectModel>();
            List<ReportScheduleObjectModel> lstReportScheduleObjects = new List<ReportScheduleObjectModel>();
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ClientId = 0;
            objReportScheduleObjectModel.Description = "";
            objReportScheduleObjectModel.Email = "";
            objReportScheduleObjectModel.FrequencyDescription = "";
            objReportScheduleObjectModel.FrequencyInterval = 0;
            objReportScheduleObjectModel.FrequencyType = 0;
            objReportScheduleObjectModel.FTPFilePath = "";
            objReportScheduleObjectModel.FTPPassword = "";
            objReportScheduleObjectModel.FTPServerIP = "";
            objReportScheduleObjectModel.FTPUsername = "";
            objReportScheduleObjectModel.IsEnabled = true;
            objReportScheduleObjectModel.Name = "";
            objReportScheduleObjectModel.ReportId = 0;
            objReportScheduleObjectModel.ReportScheduleId = 1;
            objReportScheduleObjectModel.ReportName = "";
            objReportScheduleObjectModel.UtcFirstScheduledRunDate = null;
            lstReportScheduleObjects.Add(objReportScheduleObjectModel);
            enumReportScheduleObjects = lstReportScheduleObjects;

            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ReportScheduleSearchDetail>()))
                .Returns(enumReportScheduleObjects);
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetFirstScheduleRunDate();

            //Verify and Assert
            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair obj = new DisplayValuePair();
            ValidateDisplayValuePair(lst, result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetNextScheduleRunDate_Returns_JsonResult
        /// <summary>
        /// GetNextScheduleRunDate_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetNextScheduleRunDate_Returns_JsonResult()
        {
            IEnumerable<ReportScheduleObjectModel> enumReportScheduleObjects = Enumerable.Empty<ReportScheduleObjectModel>();
            List<ReportScheduleObjectModel> lstReportScheduleObjects = new List<ReportScheduleObjectModel>();
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.UtcNextScheduledRunDate = DateTime.Parse("02-Feb-2015");
            lstReportScheduleObjects.Add(objReportScheduleObjectModel);
            enumReportScheduleObjects = lstReportScheduleObjects;
            //Arrange
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ReportScheduleSearchDetail>()))
                .Returns(enumReportScheduleObjects);
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetNextScheduleRunDate();

            //Verify and Assert
            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair obj = new DisplayValuePair();
            obj.Display = DateTime.Parse("02/02/2015").ToString();
            obj.Selected = false;
            obj.Value = DateTime.Parse("02/02/2015").ToString();
            lst.Add(obj);
            ValidateDisplayValuePair(lst, result);
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetNextScheduleRunDate_Returns_JsonResult_UtcNextScheduledRunDateNull
        /// <summary>
        /// GetNextScheduleRunDate_Returns_JsonResult_UtcNextScheduledRunDateNull
        /// </summary>
        [TestMethod]
        public void GetNextScheduleRunDate_Returns_JsonResult_UtcNextScheduledRunDateNull()
        {
            //Arrange
            IEnumerable<ReportScheduleObjectModel> enumReportScheduleObjects = Enumerable.Empty<ReportScheduleObjectModel>();
            List<ReportScheduleObjectModel> lstReportScheduleObjects = new List<ReportScheduleObjectModel>();
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ClientId = 0;
            objReportScheduleObjectModel.Description = "";
            objReportScheduleObjectModel.Email = "";
            objReportScheduleObjectModel.FrequencyDescription = "";
            objReportScheduleObjectModel.FrequencyInterval = 0;
            objReportScheduleObjectModel.FrequencyType = 0;
            objReportScheduleObjectModel.FTPFilePath = "";
            objReportScheduleObjectModel.FTPPassword = "";
            objReportScheduleObjectModel.FTPServerIP = "";
            objReportScheduleObjectModel.FTPUsername = "";
            objReportScheduleObjectModel.IsEnabled = true;
            objReportScheduleObjectModel.Name = "";
            objReportScheduleObjectModel.ReportId = 0;
            objReportScheduleObjectModel.ReportScheduleId = 1;
            objReportScheduleObjectModel.ReportName = "";
            objReportScheduleObjectModel.UtcNextScheduledRunDate = null;
            lstReportScheduleObjects.Add(objReportScheduleObjectModel);
            enumReportScheduleObjects = lstReportScheduleObjects;
            mockreportScheduleCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<ReportScheduleSearchDetail>()))
                .Returns(enumReportScheduleObjects);
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetNextScheduleRunDate();

            //Verify and Assert

            List<DisplayValuePair> lst = new List<DisplayValuePair>();
            DisplayValuePair obj = new DisplayValuePair();
            ValidateDisplayValuePair(lst, result);

            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region  DeleteReportSchedule_Returns_JsonResult
        /// <summary>
        /// DeleteReportSchedule_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DeleteReportSchedule_Returns_JsonResult()
        {

            var principal = new Mock<IPrincipal>();
            var moqContext = new Mock<HttpContextBase>();
            moqContext.Setup(x => x.User.Identity.Name).Returns("MName");

            //Arrange
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>())).Returns(GetUserObjectModelData());
            mockreportScheduleCacheFactory.Setup(x => x.DeleteEntity(It.IsAny<int>(), It.IsAny<int>()));
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            objReportSchedule.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objReportSchedule);
            var result = objReportSchedule.DeleteReportSchedule(It.IsAny<int>());

            //Verify and Assert
            Assert.AreEqual(string.Empty, result.Data);
            mockuserCacheFactory.VerifyAll();
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region  DeleteReportSchedule_Returns_JsonResult_ReportScheduleIdZero
        /// <summary>
        /// DeleteReportSchedule_Returns_JsonResult_ReportScheduleIdNull
        /// </summary>
        [TestMethod]
        public void DeleteReportSchedule_Returns_JsonResult_ReportScheduleIdZero()
        {

            var principal = new Mock<IPrincipal>();
            var moqContext = new Mock<HttpContextBase>();
            moqContext.Setup(x => x.User.Identity.Name).Returns("MName");
            IEnumerable<UserObjectModel> Ienum;
            List<UserObjectModel> lst = new List<UserObjectModel>();
            UserObjectModel obj = new UserObjectModel();
            obj.UserId = 1;
            lst.Add(obj);
            Ienum = lst;

            //Arrange
            mockuserCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>()))
                .Returns(Ienum);
            mockreportScheduleCacheFactory.Setup(x => x.DeleteEntity(It.IsAny<int>(), It.IsAny<int>()));
            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            objReportSchedule.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objReportSchedule);
            var result = objReportSchedule.DeleteReportSchedule(0);

            //Verify and Assert
            Assert.AreEqual(string.Empty, result.Data);
            mockuserCacheFactory.VerifyAll();
            mockreportScheduleCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetLocaltime
        ///<summary>
        ///GetLocalTime
        ///</summary>
        [TestMethod]
        public void GetLocalTime()
        {
            //Arrange

            //Act
            ReportScheduleController objReportSchedule = new ReportScheduleController(mockreportScheduleCacheFactory.Object, mockuserCacheFactory.Object);
            var result = objReportSchedule.GetLocaltime(DateTime.Now, 2);

            //Value and Assert
            Assert.IsInstanceOfType(result, typeof(string));
        }
        #endregion

    }
}
