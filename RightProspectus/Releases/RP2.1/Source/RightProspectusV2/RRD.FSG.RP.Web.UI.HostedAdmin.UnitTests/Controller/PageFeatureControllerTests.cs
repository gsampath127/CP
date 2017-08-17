using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Keys;
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
using System.Web.Script.Serialization;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for PageFeatureController class
    /// </summary>
    [TestClass]
    public class PageFeatureControllerTests : BaseTestController<EditPageFeatureViewModel>
    {
        Mock<IFactoryCache<PageFeatureFactory, PageFeatureObjectModel, PageFeatureKey>> mockPageFeatureFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;
        Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>> mockTemplatePageFactoryCache;
        Mock<IFactoryCache<TemplatePageFeatureFactory, TemplatePageFeatureObjectModel, TemplatePageFeatureKey>> mockTemplatePageFeatureFactoryCache;
        Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>> mockSiteFactoryCache;

        [TestInitialize]
        public void TestInitialize()
        {
            mockPageFeatureFactoryCache = new Mock<IFactoryCache<PageFeatureFactory, PageFeatureObjectModel, PageFeatureKey>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mockTemplatePageFactoryCache = new Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>();
            mockTemplatePageFeatureFactoryCache = new Mock<IFactoryCache<TemplatePageFeatureFactory, TemplatePageFeatureObjectModel, TemplatePageFeatureKey>>();
            mockSiteFactoryCache = new Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>>();
        }

        #region ReturnValues
        private IEnumerable<PageFeatureObjectModel> CreatePageFeatureList()
        {
            IEnumerable<PageFeatureObjectModel> IEnumPageFeature = Enumerable.Empty<PageFeatureObjectModel>();
            List<PageFeatureObjectModel> lstPageFeature = new List<PageFeatureObjectModel>();
            PageFeatureObjectModel objPageFeature = new PageFeatureObjectModel();
            objPageFeature.PageKey = "Test_Key";
            objPageFeature.PageDescription = "Test_Desc";
            objPageFeature.PageName = "Test";
            objPageFeature.PageId = 1;
            objPageFeature.FeatureMode = 1;
            lstPageFeature.Add(objPageFeature);
            IEnumPageFeature = lstPageFeature;
            return IEnumPageFeature;
        }

        private PageFeatureObjectModel CreatePageFeature()
        {
            //IEnumerable<PageFeatureObjectModel> IEnumPageFeature = Enumerable.Empty<PageFeatureObjectModel>();
            //List<PageFeatureObjectModel> lstPageFeature = new List<PageFeatureObjectModel>();
            PageFeatureObjectModel objPageFeature = new PageFeatureObjectModel();
            objPageFeature.PageKey = "Test_Key";
            objPageFeature.PageDescription = "Test_Desc";
            objPageFeature.PageName = "Test";
            objPageFeature.PageId = 1;
            objPageFeature.FeatureMode = 1;
            //lstPageFeature.Add(objPageFeature);
            //IEnumPageFeature = lstPageFeature;
            return objPageFeature;
        }

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

        private IEnumerable<TemplatePageFeatureObjectModel> CreateTemplatePageFeatureList()
        {
            IEnumerable<TemplatePageFeatureObjectModel> IEnumTemplatePageFeature = Enumerable.Empty<TemplatePageFeatureObjectModel>();
            List<TemplatePageFeatureObjectModel> lstTemplatePageFeature = new List<TemplatePageFeatureObjectModel>();
            TemplatePageFeatureObjectModel objTemplatePageFeature = new TemplatePageFeatureObjectModel();
            objTemplatePageFeature.FeatureKey = "Test_Key_temp";
            lstTemplatePageFeature.Add(objTemplatePageFeature);
            IEnumTemplatePageFeature = lstTemplatePageFeature;
            return IEnumTemplatePageFeature;
        }

        private IEnumerable<SiteObjectModel> CreateSiteList()
        {
            IEnumerable<SiteObjectModel> IEnumPageFeature = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lstPageFeature = new List<SiteObjectModel>();
            SiteObjectModel objSite = new SiteObjectModel();
            objSite.ClientId = 1;
            objSite.DefaultPageId = 1;
            objSite.DefaultPageName = "Test_default";
            objSite.Description = "Test_Desc";
            objSite.TemplateId = 1;
            objSite.TemplateName = "Test";
            objSite.Name = "Test_Key";
            objSite.PageDescription = "Test_Desc";
            lstPageFeature.Add(objSite);
            IEnumPageFeature = lstPageFeature;
            return IEnumPageFeature;
        }
        #endregion

        //#region PageFeature_Returns_ActionResult
        ///// <summary>
        ///// PageFeature_Returns_ActionResult
        ///// </summary>
        //[TestMethod]
        //public void PageFeature_Returns_ActionResult()
        //{
        //    //Arrange

        //    //Act
        //    PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
        //        mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
        //    var result = objPageFeatureController.PageFeature();

        //    var result1 = result as ViewResult;
        //    Assert.AreEqual("", result1.ViewName);

        //    // Verify and Assert
        //    Assert.IsInstanceOfType(result, typeof(ActionResult));
        //}
        //#endregion

        #region GetPageFeatureKey_Returns_JsonResult
        /// <summary>
        /// GetPageFeatureKey_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetPageFeatureKey_Returns_JsonResult()
        {
            //Arrange
            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>()))
                .Returns(CreatePageFeatureList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureKey();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Key" });
            ValidateDisplayValuePair(lstexpected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureKey_Returns_JsonResult_WithNoResult
        /// <summary>
        /// GetPageFeatureKey_Returns_JsonResult_WithNoResult
        /// </summary>
        [TestMethod]
        public void GetPageFeatureKey_Returns_JsonResult_WithNoResult()
        {
            //Arrange
            IEnumerable<PageFeatureObjectModel> IEnumPageFeatureObjModel = Enumerable.Empty<PageFeatureObjectModel>();
            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>()))
                .Returns(IEnumPageFeatureObjModel);

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureKey();

            // Verify and Assert

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
            mockPageFeatureFactoryCache.VerifyAll();
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
            mockPageFeatureFactoryCache.Setup(
                x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>()))
                .Returns(CreatePageFeatureList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                      mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageNames();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Desc", Value = "1" });
            ValidateDisplayValuePair(lstexpected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageNames_Returns_JsonResult_WithNoResult
        /// <summary>
        /// GetPageNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetPageNames_Returns_JsonResult_WithNoResult()
        {
            //Arrange
            IEnumerable<PageFeatureObjectModel> IEnumPageFeatureObjModel = Enumerable.Empty<PageFeatureObjectModel>();
            mockPageFeatureFactoryCache.Setup(
                x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>()))
                .Returns(IEnumPageFeatureObjModel);

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                      mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageNames();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadAllPageFeature_Returns_JsonResult
        /// <summary>
        /// LoadAllPageFeature_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void LoadAllPageFeature_Returns_JsonResult()
        {

            //Arrange

            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(CreatePageFeatureList());
            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PageFeatureSearchDetail>(), It.IsAny<PageFeatureSortDetail>())).Returns(CreatePageFeatureList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                  mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.LoadAllPageFeature("Test", "1");

            // Verify and Assert
            List<PageFeatureViewModel> lstViewModelExpected = new List<PageFeatureViewModel>();
            PageFeatureViewModel objviewModel = new PageFeatureViewModel();
            objviewModel.PageKey = "Test_Key";
            objviewModel.PageName = "Test";
            objviewModel.PageDescription = "Test_Desc";
            objviewModel.PageId = 1;
            lstViewModelExpected.Add(objviewModel);
            ValidateEmptyData<PageFeatureObjectModel>(result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadAllPageFeature_Returns_JsonResult_WithEmptyPageKey
        /// <summary>
        /// LoadAllPageFeature_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void LoadAllPageFeature_Returns_JsonResult_WithEmptyPageKey()
        {

            //Arrange
            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(CreatePageFeatureList());
            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PageFeatureSearchDetail>(), It.IsAny<PageFeatureSortDetail>())).Returns(CreatePageFeatureList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                  mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.LoadAllPageFeature(string.Empty, "1");

            // Verify and Assert
            List<PageFeatureViewModel> lstViewModelExpected = new List<PageFeatureViewModel>();
            PageFeatureViewModel objviewModel = new PageFeatureViewModel();
            objviewModel.PageKey = "Test_Key";
            objviewModel.PageName = "Test";
            objviewModel.PageDescription = "Test_Desc";
            objviewModel.PageId = 1;
            lstViewModelExpected.Add(objviewModel);
            ValidateEmptyData<PageFeatureObjectModel>(result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadAllPageFeature_Returns_JsonResult_WithEmptyPageId
        /// <summary>
        /// LoadAllPageFeature_Returns_JsonResult_WithEmptyPageId
        /// </summary>
        [TestMethod]
        public void LoadAllPageFeature_Returns_JsonResult_WithEmptyPageId()
        {
            //Arrange


            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(CreatePageFeatureList());
            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PageFeatureSearchDetail>(), It.IsAny<PageFeatureSortDetail>())).Returns(CreatePageFeatureList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                  mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.LoadAllPageFeature("test", string.Empty);

            // Verify and Assert
            List<PageFeatureViewModel> lstViewModelExpected = new List<PageFeatureViewModel>();
            PageFeatureViewModel objviewModel = new PageFeatureViewModel();
            objviewModel.PageKey = "Test_Key";
            objviewModel.PageName = "Test";
            objviewModel.PageDescription = "Test_Desc";
            objviewModel.PageId = 1;
            lstViewModelExpected.Add(objviewModel);
            ValidateEmptyData<PageFeatureObjectModel>(result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadAllPageFeature_Returns_JsonResult_WithStringPageId
        /// <summary>
        /// LoadAllPageFeature_Returns_JsonResult_WithStringPageId
        /// </summary>
        [TestMethod]
        public void LoadAllPageFeature_Returns_JsonResult_WithStringPageId()
        {
            //Arrange


            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(CreatePageFeatureList());
            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PageFeatureSearchDetail>(), It.IsAny<PageFeatureSortDetail>())).Returns(CreatePageFeatureList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                  mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.LoadAllPageFeature("test", "Test");

            // Verify and Assert
            List<PageFeatureViewModel> lstViewModelExpected = new List<PageFeatureViewModel>();
            PageFeatureViewModel objviewModel = new PageFeatureViewModel();
            objviewModel.PageKey = "Test_Key";
            objviewModel.PageName = "Test";
            objviewModel.PageDescription = "Test_Desc";
            objviewModel.PageId = 1;
            lstViewModelExpected.Add(objviewModel);
            ValidateEmptyData<PageFeatureObjectModel>(result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModes_Returns_JsonResult_XBRL
        /// <summary>
        /// GetPageFeatureModes_Returns_JsonResult_XBRL
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModes_Returns_JsonResult_XBRL()
        {
            //Arrange
            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.label = "Disabled";
            multiSelectDropDownViewModel.title = "Disabled";
            multiSelectDropDownViewModel.value = "1";
            expected.Add(multiSelectDropDownViewModel);

            MultiSelectDropDownViewModel multiSelectDropDownViewModel1 = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel1.label = "Enabled";
            multiSelectDropDownViewModel1.title = "Enabled";
            multiSelectDropDownViewModel1.value = "2";
            expected.Add(multiSelectDropDownViewModel1);

            MultiSelectDropDownViewModel multiSelectDropDownViewModel2 = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel2.label = "ShowXBRLInNewTab";
            multiSelectDropDownViewModel2.title = "ShowXBRLInNewTab";
            multiSelectDropDownViewModel2.value = "4";
            expected.Add(multiSelectDropDownViewModel2);

            MultiSelectDropDownViewModel multiSelectDropDownViewModel3 = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel3.label = "ShowXBRLInTabbedView";
            multiSelectDropDownViewModel3.title = "ShowXBRLInTabbedView";
            multiSelectDropDownViewModel3.value = "8";
            expected.Add(multiSelectDropDownViewModel3);

            MultiSelectDropDownViewModel multiSelectDropDownViewModel4 = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel4.label = "ShowXBRLInLandingPage";
            multiSelectDropDownViewModel4.title = "ShowXBRLInLandingPage";
            multiSelectDropDownViewModel4.value = "16";
            expected.Add(multiSelectDropDownViewModel4);


            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                            mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModes("XBRL"); ;
            // Verify and Assert
            ValidateData<MultiSelectDropDownViewModel>(expected, result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModes_Returns_JsonResult_RequestMaterial
        /// <summary>
        /// GetPageFeatureModes_Returns_JsonResult_RequestMaterial
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModes_Returns_JsonResult_RequestMaterial()
        {
            //Arrange
            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.label = "Disabled";
            multiSelectDropDownViewModel.title = "Disabled";
            multiSelectDropDownViewModel.value = "1";
            expected.Add(multiSelectDropDownViewModel);

            MultiSelectDropDownViewModel multiSelectDropDownViewModel1 = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel1.label = "Enabled";
            multiSelectDropDownViewModel1.title = "Enabled";
            multiSelectDropDownViewModel1.value = "2";
            expected.Add(multiSelectDropDownViewModel1);


            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                            mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModes("RequestMaterial");
            // Verify and Assert
            ValidateData<MultiSelectDropDownViewModel>(expected, result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModes_Returns_JsonResult_EmptyData
        /// <summary>
        /// GetPageFeatureModes_Returns_JsonResult_EmptyData
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModes_Returns_JsonResult_EmptyData()
        {
            //Arrange
            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                            mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModes(string.Empty); ;
            // Verify and Assert

            ValidateEmptyData<MultiSelectDropDownViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModesByKey_Returns_JsonResult_XBRL_Disabled
        /// <summary>
        /// GetPageFeatureModesByKey_Returns_JsonResult_XBRL_Disabled
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModesByKey_Returns_JsonResult_XBRL_Disabled()
        {
            //Arrange
            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "1";
            expected.Add(multiSelectDropDownViewModel);
            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(CreatePageFeatureList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModesByKey("XBRL", "1");

            // Verify and Assert
            ValidateData<MultiSelectDropDownViewModel>(expected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModesByKey_Returns_JsonResult_XBRL_Enabled
        /// <summary>
        /// GetPageFeatureModesByKey_Returns_JsonResult_XBRL_Enabled
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModesByKey_Returns_JsonResult_XBRL_Enabled()
        {
            //Arrange
            IEnumerable<PageFeatureObjectModel> IEnumPageFeature = Enumerable.Empty<PageFeatureObjectModel>();
            List<PageFeatureObjectModel> lstPageFeature = new List<PageFeatureObjectModel>();
            PageFeatureObjectModel objPageFeature = new PageFeatureObjectModel();
            objPageFeature.PageKey = "Test_Key";
            objPageFeature.PageId = 1;
            objPageFeature.FeatureMode = 2;
            lstPageFeature.Add(objPageFeature);
            IEnumPageFeature = lstPageFeature;

            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "2";
            expected.Add(multiSelectDropDownViewModel);

            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(IEnumPageFeature);

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModesByKey("XBRL", "2");

            // Verify and Assert
            ValidateData<MultiSelectDropDownViewModel>(expected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModesByKey_Returns_JsonResult_XBRL_ShowXBRLInNewTab
        /// <summary>
        /// GetPageFeatureModesByKey_Returns_JsonResult_XBRL_ShowXBRLInNewTab
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModesByKey_Returns_JsonResult_XBRL_ShowXBRLInNewTab()
        {
            //Arrange
            IEnumerable<PageFeatureObjectModel> IEnumPageFeature = Enumerable.Empty<PageFeatureObjectModel>();
            List<PageFeatureObjectModel> lstPageFeature = new List<PageFeatureObjectModel>();
            PageFeatureObjectModel objPageFeature = new PageFeatureObjectModel();
            objPageFeature.PageKey = "Test_Key";
            objPageFeature.PageId = 1;
            objPageFeature.FeatureMode = 4;
            lstPageFeature.Add(objPageFeature);
            IEnumPageFeature = lstPageFeature;

            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "4";
            expected.Add(multiSelectDropDownViewModel);

            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(IEnumPageFeature);

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModesByKey("XBRL", "4");

            // Verify and Assert
            ValidateData<MultiSelectDropDownViewModel>(expected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModesByKey_Returns_JsonResult_XBRL_ShowXBRLInTabbedView
        /// <summary>
        /// GetPageFeatureModesByKey_Returns_JsonResult_XBRL_ShowXBRLInTabbedView
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModesByKey_Returns_JsonResult_XBRL_ShowXBRLInTabbedView()
        {
            //Arrange
            IEnumerable<PageFeatureObjectModel> IEnumPageFeature = Enumerable.Empty<PageFeatureObjectModel>();
            List<PageFeatureObjectModel> lstPageFeature = new List<PageFeatureObjectModel>();
            PageFeatureObjectModel objPageFeature = new PageFeatureObjectModel();
            objPageFeature.PageKey = "Test_Key";
            objPageFeature.PageId = 1;
            objPageFeature.FeatureMode = 8;
            lstPageFeature.Add(objPageFeature);
            IEnumPageFeature = lstPageFeature;

            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "8";
            expected.Add(multiSelectDropDownViewModel);

            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(IEnumPageFeature);

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModesByKey("XBRL", "8");

            // Verify and Assert
            ValidateData<MultiSelectDropDownViewModel>(expected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModesByKey_Returns_JsonResult_XBRL_ShowXBRLInLandingPage
        /// <summary>
        /// GetPageFeatureModesByKey_Returns_JsonResult_XBRL_ShowXBRLInLandingPage
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModesByKey_Returns_JsonResult_XBRL_ShowXBRLInLandingPage()
        {
            //Arrange
            IEnumerable<PageFeatureObjectModel> IEnumPageFeature = Enumerable.Empty<PageFeatureObjectModel>();
            List<PageFeatureObjectModel> lstPageFeature = new List<PageFeatureObjectModel>();
            PageFeatureObjectModel objPageFeature = new PageFeatureObjectModel();
            objPageFeature.PageKey = "Test_Key";
            objPageFeature.PageId = 1;
            objPageFeature.FeatureMode = 16;
            lstPageFeature.Add(objPageFeature);
            IEnumPageFeature = lstPageFeature;

            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "16";
            expected.Add(multiSelectDropDownViewModel);

            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(IEnumPageFeature);

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModesByKey("XBRL", "16");

            // Verify and Assert
            ValidateData<MultiSelectDropDownViewModel>(expected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModesByKey_Returns_JsonResult_RequestMaterial_Disabled
        /// <summary>
        /// GetPageFeatureModesByKey_Returns_JsonResult_RequestMaterial_Disabled
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModesByKey_Returns_JsonResult_RequestMaterial_Disabled()
        {
            //Arrange
            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "1";
            expected.Add(multiSelectDropDownViewModel);

            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>()))
                .Returns(CreatePageFeatureList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModesByKey("RequestMaterial", "1");

            // Verify and Assert
            ValidateData<MultiSelectDropDownViewModel>(expected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModesByKey_Returns_JsonResult_RequestMaterial_Enabled
        /// <summary>
        /// GetPageFeatureModesByKey_Returns_JsonResult_RequestMaterial_Enabled
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModesByKey_Returns_JsonResult_RequestMaterial_Enabled()
        {
            //Arrange
            IEnumerable<PageFeatureObjectModel> IEnumPageFeature = Enumerable.Empty<PageFeatureObjectModel>();
            List<PageFeatureObjectModel> lstPageFeature = new List<PageFeatureObjectModel>();
            PageFeatureObjectModel objPageFeature = new PageFeatureObjectModel();
            objPageFeature.PageKey = "Test_Key";
            objPageFeature.PageId = 1;
            objPageFeature.FeatureMode = 2;
            lstPageFeature.Add(objPageFeature);
            IEnumPageFeature = lstPageFeature;

            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "2";
            expected.Add(multiSelectDropDownViewModel);


            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(IEnumPageFeature);

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModesByKey("RequestMaterial", "2");

            // Verify and Assert
            ValidateData<MultiSelectDropDownViewModel>(expected, result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageFeatureModesByKey_Returns_JsonResult_XBRL_Disabled_WithEmptyKey
        /// <summary>
        /// GetPageFeatureModesByKey_Returns_JsonResult_XBRL_Disabled
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModesByKey_Returns_JsonResult_XBRL_Disabled_WithEmptyKey()
        {
            //Arrange
            mockPageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageFeatureSearchDetail>())).Returns(CreatePageFeatureList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                           mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageFeatureModesByKey(string.Empty, "1");

            // Verify and Assert

            ValidateEmptyData<MultiSelectDropDownViewModel>(result);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region EditPageFeature_Returns_ActionResult_GetMethod
        /// <summary>
        /// EditPageFeature_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void EditPageFeature_Returns_ActionResult_GetMethod()
        {
            PageFeatureObjectModel objPageFeatureObjectModel = new PageFeatureObjectModel()
            {
                PageKey = "Test_Key",
                PageDescription = "Test_Desc",
                PageName = "Test",
                PageId = 1,
                FeatureMode = 1
            };

            //Arrange
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());
            mockPageFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<PageFeatureKey>()))
                .Returns(objPageFeatureObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>())).Returns(CreateUserList);

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                          mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.EditPageFeature(1, "Test_Key");

            // Verify and Assert
            List<DisplayValuePair> lstPageKeys= new List<DisplayValuePair>();
            lstPageKeys.Add(new DisplayValuePair() { Display = "--Please select Page Feature Key--", Value = "-1" });
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Desc", Value = "1" });
            EditPageFeatureViewModel objExpected = new EditPageFeatureViewModel()
            {
                ModifiedBy = null,
                ModifiedByName = "Test_UserName",
                PageId = 1,
                PageKeys=lstPageKeys,
                PageNames = lstexpected,
                PageFeature = null,
                SelectedPageFeature = 0,
                SelectedPageId = 1,
                SelectedPageKey = "Test_Key",
                UTCLastModifiedDate = null,


            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageFeatureViewModel;
            ValidateViewModelData<EditPageFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageFeatureViewModel));


            mockTemplatePageFactoryCache.VerifyAll();
            mockPageFeatureFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageFeature_Returns_ActionResult_GetMethod_AddFunction
        /// <summary>
        /// EditPageFeature_Returns_ActionResult_GetMethod_AddFunction
        /// </summary>
        [TestMethod]
        public void EditPageFeature_Returns_ActionResult_GetMethod_AddFunction()
        {
            //Arrange
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                          mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.EditPageFeature(0, "Test_Key");

            List<DisplayValuePair> lstPageKeys = new List<DisplayValuePair>();
            lstPageKeys.Add(new DisplayValuePair() { Display = "--Please select Page Feature Key--", Value = "-1" });


            List<DisplayValuePair> lstPageNames = new List<DisplayValuePair>();
            lstPageNames.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstPageNames.Add(new DisplayValuePair() { Display = "Test_Desc", Value = "1" });

            EditPageFeatureViewModel objExpected = new EditPageFeatureViewModel()
            {
                PageId = 0,
                PageKeys = lstPageKeys,
                PageNames = lstPageNames,
                ModifiedByName = null,
                SelectedPageFeature = 0,
                SelectedPageId = -1,
                SelectedPageKey = "-1",
                UTCLastModifiedDate = null,
                ModifiedBy = null,

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageFeatureViewModel;
            ValidateViewModelData<EditPageFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageFeatureViewModel));

            // Verify and Assert
            mockTemplatePageFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetPageKeyByPageId_Returns_JsonResult
        /// <summary>
        /// GetPageKeyByPageId_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetPageKeyByPageId_Returns_JsonResult()
        {
            //Arrange
            mockTemplatePageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageFeatureSearchDetail>())).Returns(CreateTemplatePageFeatureList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                       mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageKeyByPageId("1");

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Key_temp", Value = "Test_Key_temp" });
            ValidateDisplayValuePair(lstexpected, result);

            mockTemplatePageFeatureFactoryCache.VerifyAll();
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageKeyByPageId_Returns_JsonResult_WithNoResult
        /// <summary>
        /// GetPageKeyByPageId_Returns_JsonResult_WithNoResult
        /// </summary>
        [TestMethod]
        public void GetPageKeyByPageId_Returns_JsonResult_WithNoResult()
        {
            //Arrange
            IEnumerable<TemplatePageFeatureObjectModel> IEnumTemplatePageFeature = Enumerable.Empty<TemplatePageFeatureObjectModel>();
            IEnumerable<PageFeatureObjectModel> IEnumPageFeature = Enumerable.Empty<PageFeatureObjectModel>();
            mockTemplatePageFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageFeatureSearchDetail>())).Returns(IEnumTemplatePageFeature);
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                       mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.GetPageKeyByPageId("1");

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);

            mockTemplatePageFeatureFactoryCache.VerifyAll();
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region EditPageFeature_Returns_ActionResult_PostMethod
        /// <summary>
        /// EditPageFeature_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditPageFeature_Returns_ActionResult_PostMethod()
        {
            //Arrange
            EditPageFeatureViewModel viewModel = new EditPageFeatureViewModel();

            DisplayValuePair objDisplayValuePair = new DisplayValuePair();
            objDisplayValuePair.Display = "Test_Display";
            objDisplayValuePair.Selected = false;
            objDisplayValuePair.Value = "Test_value";
            List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();
            lstDisplayValuePair.Add(objDisplayValuePair);

            viewModel.PageFeature = lstDisplayValuePair;
            viewModel.SelectedPageId = 1;
            viewModel.SelectedPageKey = "Test_key";
            viewModel.SelectedPageFeature = 1;
            viewModel.ModifiedBy = 1;

            mockPageFeatureFactoryCache.Setup(x => x.SaveEntity(It.IsAny<PageFeatureObjectModel>(), It.IsAny<int>()));
            mockPageFeatureFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreatePageFeatureList());
            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageList());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                      mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.EditPageFeature(viewModel, "1,2");

            /////
            List<DisplayValuePair> lstPageKeys = new List<DisplayValuePair>();
            lstPageKeys.Add(new DisplayValuePair() { Display = "--Please select Page Feature Key--", Value = "-1" });
            lstPageKeys.Add(new DisplayValuePair() { Display = "Test_Key", Value = "Test_Key" });

            List<DisplayValuePair> lstPageNames = new List<DisplayValuePair>();
            lstPageNames.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstPageNames.Add(new DisplayValuePair() { Display = "Test_Desc", Value = "1" });

            List<DisplayValuePair> lstPageFeature = new List<DisplayValuePair>();
            lstPageFeature.Add(new DisplayValuePair() { Display = "Test_Display", Value = "Test_value" });

            EditPageFeatureViewModel objExpected = new EditPageFeatureViewModel()
            {
                PageId = 0,
                PageKeys = lstPageKeys,
                PageNames = lstPageNames,
                ModifiedByName = null,
                SelectedPageFeature = 1,
                SelectedPageId = 1,
                SelectedPageKey = "Test_key",
                UTCLastModifiedDate = null,
                ModifiedBy = 1,
                PageFeature = lstPageFeature,
            };

            var result1 = result as ViewResult;
            var viewModel1 = result1.Model as EditPageFeatureViewModel;
            ValidateViewModelData<EditPageFeatureViewModel>(viewModel1, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageFeatureViewModel));

            // Verify and Assert
            mockPageFeatureFactoryCache.VerifyAll();
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageFeature_Returns_ActionResult_PostMethod_WithNoResult
        /// <summary>
        /// EditPageFeature_Returns_ActionResult_PostMethod_WithNoResult
        /// </summary>
        [TestMethod]
        public void EditPageFeature_Returns_ActionResult_PostMethod_WithNoResult()
        {
            //Arrange
            EditPageFeatureViewModel viewModel = new EditPageFeatureViewModel();

            DisplayValuePair objDisplayValuePair = new DisplayValuePair();
            objDisplayValuePair.Display = "Test_Display";
            objDisplayValuePair.Selected = false;
            objDisplayValuePair.Value = "Test_value";
            List<DisplayValuePair> lstDisplayValuePair = new List<DisplayValuePair>();
            lstDisplayValuePair.Add(objDisplayValuePair);

            viewModel.PageFeature = lstDisplayValuePair;
            viewModel.SelectedPageId = 1;
            viewModel.SelectedPageKey = "Test_key";
            viewModel.SelectedPageFeature = 1;
            viewModel.ModifiedBy = 1;

            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            IEnumerable<PageFeatureObjectModel> IEnumPageFeature = Enumerable.Empty<PageFeatureObjectModel>();

            mockPageFeatureFactoryCache.Setup(x => x.SaveEntity(It.IsAny<PageFeatureObjectModel>(), It.IsAny<int>()));
            mockPageFeatureFactoryCache.Setup(x => x.GetAllEntities()).Returns(IEnumPageFeature);
            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities()).Returns(IEnumTemplatePage);

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                      mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.EditPageFeature(viewModel, "1");

            /////
            List<DisplayValuePair> lstPageKeys = new List<DisplayValuePair>();
            lstPageKeys.Add(new DisplayValuePair() { Display = "--Please select Page Feature Key--", Value = "-1" });


            List<DisplayValuePair> lstPageNames = new List<DisplayValuePair>();
            lstPageNames.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });


            List<DisplayValuePair> lstPageFeature = new List<DisplayValuePair>();
            lstPageFeature.Add(new DisplayValuePair() { Display = "Test_Display", Value = "Test_value" });

            EditPageFeatureViewModel objExpected = new EditPageFeatureViewModel()
            {
                PageId = 0,
                PageKeys = lstPageKeys,
                PageNames = lstPageNames,
                ModifiedByName = null,
                SelectedPageFeature = 1,
                SelectedPageId = 1,
                SelectedPageKey = "Test_key",
                UTCLastModifiedDate = null,
                ModifiedBy = 1,
                PageFeature = lstPageFeature,
            };

            var result1 = result as ViewResult;
            var viewModel1 = result1.Model as EditPageFeatureViewModel;
            ValidateViewModelData<EditPageFeatureViewModel>(viewModel1, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageFeatureViewModel));

            // Verify and Assert
            mockPageFeatureFactoryCache.VerifyAll();
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageFeature_Returns_ActionResult_PostMethod_Handles_Exception
        /// <summary>
        /// EditPageFeature_Returns_ActionResult_PostMethod_Handles_Exception
        /// </summary>
        [TestMethod]
        public void EditPageFeature_Returns_ActionResult_PostMethod_Handles_Exception()
        {
            //Arrange
            EditPageFeatureViewModel viewModel = new EditPageFeatureViewModel();
            viewModel.SelectedPageId = 1;
            viewModel.SelectedPageKey = "Test_key";

            mockPageFeatureFactoryCache.Setup(x => x.SaveEntity(It.IsAny<PageFeatureObjectModel>(), It.IsAny<int>())).Throws(new Exception());

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                      mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.EditPageFeature(viewModel, "1,2");

            /////
            List<DisplayValuePair> lstPageKeys = new List<DisplayValuePair>();

            List<DisplayValuePair> lstPageNames = new List<DisplayValuePair>();

            EditPageFeatureViewModel objExpected = new EditPageFeatureViewModel()
            {
                PageId = 0,
                PageKeys = lstPageKeys,
                PageNames = lstPageNames,
                ModifiedByName = null,
                SelectedPageFeature = 0,
                SelectedPageId = 1,
                SelectedPageKey = "Test_key",
                UTCLastModifiedDate = null,
                ModifiedBy = null,
            };

            var result1 = result as ViewResult;
            var viewModel1 = result1.Model as EditPageFeatureViewModel;
            ValidateViewModelData<EditPageFeatureViewModel>(viewModel1, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageFeatureViewModel));

            // Verify and Assert
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region DeletePageFeature_Returns_JsonResult
        /// <summary>
        /// DeletePageFeature_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DeletePageFeature_Returns_JsonResult()
        {
            //Arrange
            mockPageFeatureFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<PageFeatureObjectModel>(), It.IsAny<int>()));

            //Act
            PageFeatureController objPageFeatureController = new PageFeatureController(mockPageFeatureFactoryCache.Object, mockUserFactoryCache.Object,
                                                     mockTemplatePageFactoryCache.Object, mockTemplatePageFeatureFactoryCache.Object, mockSiteFactoryCache.Object);
            var result = objPageFeatureController.DeletePageFeature(1, null);

            // Verify and Assert
            Assert.AreEqual(string.Empty, result.Data);
            mockPageFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

    }
}
