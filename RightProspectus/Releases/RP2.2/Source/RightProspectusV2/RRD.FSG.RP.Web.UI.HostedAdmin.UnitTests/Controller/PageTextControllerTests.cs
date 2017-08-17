using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
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
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for PageTextController class
    /// </summary>
    [TestClass]
    public class PageTextControllerTests : BaseTestController<PageTextViewModel>
    {
        Mock<IFactoryCache<PageTextFactory, PageTextObjectModel, PageTextKey>> mockPageTextFactoryCache;
        Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>> mockTemplatePageFactoryCache;
        Mock<IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey>> mockTemplatePageTextFactoryCache;
        Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>> mockSiteFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;

        [TestInitialize]
        public void TestInitialize()
        {
            mockPageTextFactoryCache = new Mock<IFactoryCache<PageTextFactory, PageTextObjectModel, PageTextKey>>();
            mockTemplatePageFactoryCache = new Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>();
            mockTemplatePageTextFactoryCache = new Mock<IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey>>();
            mockSiteFactoryCache = new Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
        }

        #region ReturnValues
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageList()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePage = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePage = new TemplatePageObjectModel();
            objTemplatePage.PageID = 1;
            objTemplatePage.PageDescription = "Test_Desc";
            lstTemplatePage.Add(objTemplatePage);
            IEnumTemplatePage = lstTemplatePage;
            return IEnumTemplatePage;
        }

        private IEnumerable<PageTextObjectModel> CreatePageTextList()
        {
            IEnumerable<PageTextObjectModel> IEnumPageText = Enumerable.Empty<PageTextObjectModel>();
            List<PageTextObjectModel> lstPageText = new List<PageTextObjectModel>();
            PageTextObjectModel objPageText = new PageTextObjectModel();
            objPageText.PageTextID = 1;
            objPageText.PageID = 1;
            objPageText.Version = 1;
            objPageText.IsProofing = false;
            objPageText.ResourceKey = "Test_key";
            objPageText.Text = "Test_Text";
            lstPageText.Add(objPageText);
            IEnumPageText = lstPageText;
            return IEnumPageText;
        }

        private IEnumerable<TemplatePageTextObjectModel> CreateTemplatePageTextList()
        {
            IEnumerable<TemplatePageTextObjectModel> IEnumTemplatePageText = Enumerable.Empty<TemplatePageTextObjectModel>();
            List<TemplatePageTextObjectModel> lstTemplatePageText = new List<TemplatePageTextObjectModel>();
            TemplatePageTextObjectModel objTemplatePageText = new TemplatePageTextObjectModel();
            objTemplatePageText.ResourceKey = "css";
            objTemplatePageText.DefaultText = "Test.Test#Test{Test;Test}Test:Test";
            lstTemplatePageText.Add(objTemplatePageText);
            IEnumTemplatePageText = lstTemplatePageText;
            return IEnumTemplatePageText;
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

        private IEnumerable<SiteObjectModel> CreateSiteList()
        {
            IEnumerable<SiteObjectModel> IEnumSite = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lstSiteObjectModel = new List<SiteObjectModel>();
            SiteObjectModel objSiteObjectModel = new SiteObjectModel();
            objSiteObjectModel.SiteID = 1;
            objSiteObjectModel.Name = "Test_Site";
            objSiteObjectModel.DefaultPageId = 1;
            objSiteObjectModel.TemplateId = 1;
            objSiteObjectModel.Description = "Test_Description";
            lstSiteObjectModel.Add(objSiteObjectModel);
            IEnumSite = lstSiteObjectModel;
            return IEnumSite;
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
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.List();

            var result1 = result as ViewResult;
            Assert.AreEqual("PageText", result1.ViewName);
            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetAllPageTextDetails_Returns_JsonResult_ProductionVersion
        /// <summary>
        /// GetAllPageTextDetails_Returns_JsonResult_ProductionVersion
        /// </summary>
        [TestMethod]
        public void GetAllPageTextDetails_Returns_JsonResult_ProductionVersion()
        {
            //Arrange
            
            mockPageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageTextSearchDetail>())).Returns(CreatePageTextList());
            mockPageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PageTextSearchDetail>(), It.IsAny<PageTextSortDetail>())).Returns(CreatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
               mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetAllPageTextDetails(null, "1", "Production");

            List<PageTextViewModel> lstExpected = new List<PageTextViewModel>();
            PageTextViewModel objModel = new PageTextViewModel();
            objModel.PageTextID = 1;
            objModel.PageName = null;
            objModel.PageDescription = null;
            objModel.IsProofingAvailableForPageTextId = false;
            objModel.ResourceKey = "Test_key";
            objModel.Text = "Test_Text";
            objModel.Version = "Production";
            objModel.VersionID = 1;
          
            
            lstExpected.Add(objModel);
            // Verify and Assert
            ValidateData(lstExpected, result);
            mockPageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllPageTextDetails_Returns_JsonResult_ProofingVersion
        /// <summary>
        /// GetAllPageTextDetails_Returns_JsonResult_ProofingVersion
        /// </summary>
        [TestMethod]
        public void GetAllPageTextDetails_Returns_JsonResult_ProofingVersion()
        {
            //Arrange
            //mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageList());
            mockPageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageTextSearchDetail>())).Returns(CreatePageTextList());
            mockPageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PageTextSearchDetail>(), It.IsAny<PageTextSortDetail>())).Returns(CreatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
               mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetAllPageTextDetails(null, "1", "Proofing");

            // Verify and Assert
            List<PageTextViewModel> lstExpected = new List<PageTextViewModel>();
            PageTextViewModel objModel = new PageTextViewModel();
            objModel.PageTextID = 1;
            objModel.PageName = null;
            objModel.PageDescription = null;
            objModel.IsProofingAvailableForPageTextId = false;
            objModel.ResourceKey = "Test_key";
            objModel.Text = "Test_Text";
            objModel.Version = "Production";
            objModel.VersionID = 1;


            lstExpected.Add(objModel);
            // Verify and Assert
            ValidateData(lstExpected, result);
            mockPageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetResourceKeys_Returns_JsonResult
        /// <summary>
        /// GetResourceKeys_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetResourceKeys_Returns_JsonResult()
        {
            //Arrange
            mockPageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageTextSearchDetail>())).Returns(CreatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
              mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetResourceKeys();

            // Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "Test_key", Value = "Test_key" });
            ValidateDisplayValuePair(lstExpected, result);
            mockPageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageNames_Returns_JsonResult
        /// <summary>
        /// GetPageNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetPageNames_Returns_JsonResult()
        {
            //Arrange
            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageList());
            mockPageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageTextSearchDetail>())).Returns(CreatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
              mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetPageNames();

            // Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = null, Value = "1" });
            ValidateDisplayValuePair(lstExpected, result);
            mockPageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetVersions_Returns_JsonResult
        /// <summary>
        /// GetVersions_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetVersions_Returns_JsonResult()
        {
            //Arrange
            mockPageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageTextSearchDetail>())).Returns(CreatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
              mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetVersions();

            // Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "Production", Value = "Production" });
            ValidateDisplayValuePair(lstExpected, result);
            mockPageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region DisablePageText_Returns_JsonResult
        /// <summary>
        /// DisablePageText_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DisablePageText_Returns_JsonResult()
        {
            //Arrange
            mockPageTextFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<PageTextObjectModel>(),It.IsAny<int>()));

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
             mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.DisablePageText(1, 1, false);

            // Verify and Assert
            Assert.AreEqual(string.Empty, result.Data);
            mockPageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region EditPageText_Returns_ActionResult_GetMethod
        /// <summary>
        /// EditPageText_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void EditPageText_Returns_ActionResult_GetMethod()
        {
            PageTextObjectModel objPageText = new PageTextObjectModel();
            objPageText.PageTextID = 1;
            objPageText.PageID = 0;
            objPageText.Version = 1;
            objPageText.IsProofing = false;
            objPageText.ResourceKey = "css";
            objPageText.Text = "Test_Text";
           
            //Arrange
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());
            mockTemplatePageTextFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageTextList());
            mockPageTextFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<PageTextKey>())).Returns(objPageText);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>())).Returns(CreateUserList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.EditPageText(1, 1);

            List<DisplayValuePair> lstResourceKey = new List<DisplayValuePair>();
            lstResourceKey.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKey.Add(new DisplayValuePair() { Display = "css", Value = "css" });

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Desc", Value = "1" });

            EditPageTextViewModel objExpected = new EditPageTextViewModel()
            {
                HtmlText = null,
                IsProofing = false,
                IsProofingAvailableForPageTextId = false,
                ModifiedBy = null,
                ModifiedByName = "Test_UserName",
                PageDescriptions = lstPageDesc,
                PageTextID = 1,
                PlainText = "Test_Text",
                ResourceKeys = lstResourceKey,
                SelectedPageID = 0,
                SelectedResourceKey = "css",
                UTCLastModifiedDate = null,
                VersionID = 1
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageTextViewModel;
            ValidateViewModelData<EditPageTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageTextViewModel));
            // Verify and Assert
            mockTemplatePageFactoryCache.VerifyAll();
            mockTemplatePageTextFactoryCache.VerifyAll();
            mockPageTextFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageText_Returns_ActionResult_GetMethod_ForResourceKeyCss
        /// <summary>
        /// EditPageText_Returns_ActionResult_GetMethod_ForResourceKeyCss
        /// </summary>
        [TestMethod]
        public void EditPageText_Returns_ActionResult_GetMethod_ForResourceKeyCss()
        {
            //Arrange
           
            PageTextObjectModel objPageText = new PageTextObjectModel();
            objPageText.PageTextID = 1;
            objPageText.PageID = 0;
            objPageText.Version = 1;
            objPageText.IsProofing = false;
            objPageText.ResourceKey = "css";
            objPageText.Text = "Test_Text";
           

            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());
            mockTemplatePageTextFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageTextList());
            mockPageTextFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<PageTextKey>())).Returns(objPageText);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>())).Returns(CreateUserList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.EditPageText(1, 1);

            List<DisplayValuePair> lstResourceKey = new List<DisplayValuePair>();
            lstResourceKey.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKey.Add(new DisplayValuePair() { Display = "css", Value = "css" });

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Desc", Value = "1" });

            EditPageTextViewModel objExpected = new EditPageTextViewModel()
            {
                HtmlText = null,
                IsProofing = false,
                IsProofingAvailableForPageTextId = false,
                ModifiedBy = null,
                ModifiedByName = "Test_UserName",
                PageDescriptions = lstPageDesc,
                PageTextID = 1,
                PlainText = "Test_Text",
                ResourceKeys = lstResourceKey,
                SelectedPageID = 0,
                SelectedResourceKey = "css",
                UTCLastModifiedDate = null,
                VersionID = 1
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageTextViewModel;
            ValidateViewModelData<EditPageTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageTextViewModel));
            // Verify and Assert
            mockTemplatePageFactoryCache.VerifyAll();
            mockTemplatePageTextFactoryCache.VerifyAll();
            mockPageTextFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageText_Returns_ActionResult_GetMethod_ForAdd
        /// <summary>
        /// EditPageText_Returns_ActionResult_GetMethod_ForAdd
        /// </summary>
        [TestMethod]
        public void EditPageText_Returns_ActionResult_GetMethod_ForAdd()
        {
            //Arrange
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.EditPageText(0, 1);


            List<DisplayValuePair> lstResourceKey = new List<DisplayValuePair>();
            lstResourceKey.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = "-1" });
            //lstResourceKey.Add(new DisplayValuePair() { Display = "css", Value = "css" });

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Desc", Value = "1" });

            EditPageTextViewModel objExpected = new EditPageTextViewModel()
            {
                HtmlText = null,
                IsProofing = false,
                IsProofingAvailableForPageTextId = false,
                ModifiedBy = null,
                ModifiedByName = null,
                PageDescriptions = lstPageDesc,
                PageTextID = 0,
                PlainText = null,
                ResourceKeys = lstResourceKey,
                SelectedPageID = 0,
                SelectedResourceKey = null,
                UTCLastModifiedDate = null,
                VersionID = 0
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageTextViewModel;
            ValidateViewModelData<EditPageTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageTextViewModel));
            // Verify and Assert
            mockTemplatePageFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageText_Retuns_ActionResult_PostMethod
        /// <summary>
        /// EditPageText_Retuns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditPageText_Retuns_ActionResult_PostMethod()
        {
            //Arrange
            EditPageTextViewModel viewModel = new EditPageTextViewModel();
            viewModel.PageTextID = 1;
            viewModel.VersionID = 1;
            viewModel.SelectedResourceKey = "Test";
            viewModel.HtmlText = "Test_text";
            viewModel.SelectedPageID = 1;
            viewModel.IsProofing = false;



            mockPageTextFactoryCache.Setup(x => x.SaveEntity(It.IsAny<PageTextObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());
            mockTemplatePageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageTextSearchDetail>())).Returns(CreateTemplatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);

            var result = objController.EditPageText(viewModel);

            List<DisplayValuePair> lstResourceKey = new List<DisplayValuePair>();
            lstResourceKey.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKey.Add(new DisplayValuePair() { Display = "css", Value = "css" });

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Desc", Value = "1" });

            EditPageTextViewModel objExpected = new EditPageTextViewModel()
            {

                HtmlText = "Test_text",
                IsProofing = false,
                IsProofingAvailableForPageTextId = false,
                ModifiedBy = null,
                ModifiedByName = null,
                PageDescriptions = lstPageDesc,
                PageTextID = 1,
                PlainText = null,
                ResourceKeys = lstResourceKey,
                SelectedPageID = 1,
                SelectedResourceKey = "Test",
                UTCLastModifiedDate = null,
                VersionID = 1
            

            };

            var result1 = result as ViewResult;
            var viewModel1 = result1.Model as EditPageTextViewModel;
            ValidateViewModelData<EditPageTextViewModel>(viewModel1, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageTextViewModel));
            // Verify and Assert
            mockPageTextFactoryCache.VerifyAll();
            mockTemplatePageFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockTemplatePageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageText_Retuns_ActionResult_PostMethod_Handles_Exception
        /// <summary>
        /// EditPageText_Retuns_ActionResult_PostMethod_Handles_Exception
        /// </summary>
        [TestMethod]
        public void EditPageText_Retuns_ActionResult_PostMethod_Handles_Exception()
        {
            //Arrange
            EditPageTextViewModel viewModel = new EditPageTextViewModel();
            viewModel.PageTextID = 1;
            viewModel.VersionID = 1;
            viewModel.SelectedResourceKey = "Test";
            viewModel.HtmlText = "Test_text";
            viewModel.SelectedPageID = 1;
            viewModel.IsProofing = false;
            viewModel.IsProofingAvailableForPageTextId = false;
            viewModel.ModifiedBy = 1;
            viewModel.UTCLastModifiedDate = null;
            viewModel.ModifiedByName = "Test_name";

            mockPageTextFactoryCache.Setup(x => x.SaveEntity(It.IsAny<PageTextObjectModel>(), It.IsAny<int>())).Throws(new Exception());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.EditPageText(viewModel);

            // Verify and Assert
           

            EditPageTextViewModel objExpected = new EditPageTextViewModel()
            {

                HtmlText = "Test_text",
                IsProofing = false,
                IsProofingAvailableForPageTextId = false,
                ModifiedBy = 1,
                ModifiedByName = "Test_name",
                PageDescriptions = null,
                PageTextID = 1,
                PlainText = null,
                ResourceKeys = null,
                SelectedPageID = 1,
                SelectedResourceKey = "Test",
                UTCLastModifiedDate = null,
                VersionID = 1,
                SuccessOrFailedMessage = "Exception of type 'System.Exception' was thrown."


            };

            var result1 = result as ViewResult;
            var viewModel1 = result1.Model as EditPageTextViewModel;
            ValidateViewModelData<EditPageTextViewModel>(viewModel1, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageTextViewModel));
            mockPageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageText_Retuns_ActionResult_PostMethod_SelectedResourceKeyCss
        /// <summary>
        /// EditPageText_Retuns_ActionResult_PostMethod_SelectedResourceKeyCss
        /// </summary>
        [TestMethod]
        public void EditPageText_Retuns_ActionResult_PostMethod_SelectedResourceKeyCss()
        {
            //Arrange
            EditPageTextViewModel viewModel = new EditPageTextViewModel();
            viewModel.PageTextID = 1;
            viewModel.VersionID = 1;
            viewModel.SelectedResourceKey = "css";
            viewModel.PlainText = "Test";
            viewModel.HtmlText = "Test_text";
            viewModel.SelectedPageID = 1;
            viewModel.IsProofing = false;



            //mockPageTextFactoryCache.Setup(x => x.SaveEntity(It.IsAny<PageTextObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());
            mockTemplatePageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageTextSearchDetail>())).Returns(CreateTemplatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);


            var result = objController.EditPageText(viewModel);

            List<DisplayValuePair> lstResourceKey = new List<DisplayValuePair>();
            lstResourceKey.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKey.Add(new DisplayValuePair() { Display = "css", Value = "css" });

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Desc", Value = "1" });

            EditPageTextViewModel objExpected = new EditPageTextViewModel()
            {

                HtmlText = "Test_text",
                IsProofing = false,
                IsProofingAvailableForPageTextId = false,
                ModifiedBy = null,
                ModifiedByName = null,
                PageDescriptions = lstPageDesc,
                PageTextID = 1,
                PlainText = "Test",
                ResourceKeys = lstResourceKey,
                SelectedPageID = 1,
                SelectedResourceKey = "css",
                UTCLastModifiedDate = null,
                VersionID = 1


            };

            var result1 = result as ViewResult;
            var viewModel1 = result1.Model as EditPageTextViewModel;
            ValidateViewModelData<EditPageTextViewModel>(viewModel1, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageTextViewModel));
            // Verify and Assert
            
            mockTemplatePageFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockTemplatePageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region LoadResourceKeys_Returns_JsonResult
        /// <summary>
        /// LoadResourceKeys_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void LoadResourceKeys_Returns_JsonResult()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());
            mockTemplatePageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageTextSearchDetail>())).Returns(CreateTemplatePageTextList());
            mockPageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageTextSearchDetail>())).Returns(CreatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
           mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.LoadResourceKeys(1);

            List<DisplayValuePair> lstResourceKey = new List<DisplayValuePair>();
            lstResourceKey.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKey.Add(new DisplayValuePair() { Display = "css", Value = "css" });
            ValidateDisplayValuePair(lstResourceKey, result);
            // Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mockTemplatePageTextFactoryCache.VerifyAll();
            mockPageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadDefaultTextForResourceKey_Returns_ActionResult
        /// <summary>
        /// LoadDefaultTextForResourceKey_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void LoadDefaultTextForResourceKey_Returns_ActionResult()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());
            mockTemplatePageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageTextSearchDetail>())).Returns(CreateTemplatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
          mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.LoadDefaultTextForResourceKey(1, "css");

            Assert.AreEqual("Test\n.Test#Test{\nTest;\nTest}\nTest: Test", result.Data);
          
            // Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mockTemplatePageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadDefaultTextForResourceKey_Returns_ActionResult_WithResourceKeyTest
        /// <summary>
        /// LoadDefaultTextForResourceKey_Returns_ActionResult_WithResourceKeyTest
        /// </summary>
        [TestMethod]
        public void LoadDefaultTextForResourceKey_Returns_ActionResult_WithResourceKeyTest()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());
            mockTemplatePageTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageTextSearchDetail>())).Returns(CreateTemplatePageTextList());

            //Act
            PageTextController objController = new PageTextController(mockPageTextFactoryCache.Object, mockTemplatePageFactoryCache.Object,
          mockTemplatePageTextFactoryCache.Object, mockSiteFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.LoadDefaultTextForResourceKey(1, "Test");

            Assert.AreEqual("Test.Test#Test{Test;Test}Test:Test", result.Data);
            // Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mockTemplatePageTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
    }
}
