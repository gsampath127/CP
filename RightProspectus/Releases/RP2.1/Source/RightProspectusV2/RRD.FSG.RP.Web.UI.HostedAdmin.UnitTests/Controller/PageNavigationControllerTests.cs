using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
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
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Model;
using System.Reflection;
using System.Collections;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for PageNavigationController class
    /// </summary>
    [TestClass]
    public class PageNavigationControllerTests : BaseTestController<PageNavigationViewModel>
    {
        Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>> mockSiteFactoryCache;
        Mock<IFactoryCache<PageNavigationFactory, PageNavigationObjectModel, PageNavigationKey>> mockPageNavigationFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;
        Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>> mocktemplatePageCacheFactory;
        Mock<IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey>> mocktemplatePageNavigationCacheFactory;
        [TestInitialize]
        public void TestInitialze()
        {
            mockSiteFactoryCache = new Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>>();
            mockPageNavigationFactoryCache = new Mock<IFactoryCache<PageNavigationFactory, PageNavigationObjectModel, PageNavigationKey>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mocktemplatePageCacheFactory = new Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>();
            mocktemplatePageNavigationCacheFactory = new Mock<IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey>>();
        }
        #region ReturnLists
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageList()
        {
            IEnumerable<TemplatePageObjectModel> IenumPageObjectModel = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTeplateObjectModel = new List<TemplatePageObjectModel>();

            TemplatePageObjectModel objPageObjectModel = new TemplatePageObjectModel();
            objPageObjectModel.PageID = 1;
            objPageObjectModel.PageDescription = "Test_Doc";
            lstTeplateObjectModel.Add(objPageObjectModel);
            IenumPageObjectModel = lstTeplateObjectModel;
            return IenumPageObjectModel;
        }
        private IEnumerable<TemplateNavigationObjectModel> CreateTemplateNavigationList()
        {
            IEnumerable<TemplateNavigationObjectModel> IenumPageObjectModel = Enumerable.Empty<TemplateNavigationObjectModel>();
            List<TemplateNavigationObjectModel> lstTeplateObjectModel = new List<TemplateNavigationObjectModel>();

            TemplateNavigationObjectModel objTemplateNavigationObjectModel = new TemplateNavigationObjectModel();
            objTemplateNavigationObjectModel.NavigationKey = "1test";
            objTemplateNavigationObjectModel.Name = "Test_Doc";
            objTemplateNavigationObjectModel.TemplateID = 56;
            objTemplateNavigationObjectModel.PageID = 8;


            lstTeplateObjectModel.Add(objTemplateNavigationObjectModel);
            IenumPageObjectModel = lstTeplateObjectModel;
            return IenumPageObjectModel;
        }
        private IEnumerable<TemplatePageNavigationObjectModel> CreateTemplatePageNavigationList()
        {
            IEnumerable<TemplatePageNavigationObjectModel> IenumtemplatePageObjectModel = Enumerable.Empty<TemplatePageNavigationObjectModel>();
            List<TemplatePageNavigationObjectModel> lstTemplateObjectModel = new List<TemplatePageNavigationObjectModel>();

            TemplatePageNavigationObjectModel objTemplatepageNavigationObjectModel = new TemplatePageNavigationObjectModel();
            objTemplatepageNavigationObjectModel.NavigationKey = "1test";
            objTemplatepageNavigationObjectModel.Name = "Test_Doc";
            objTemplatepageNavigationObjectModel.TemplateID = 56;
            objTemplatepageNavigationObjectModel.PageID = 8;


            lstTemplateObjectModel.Add(objTemplatepageNavigationObjectModel);
            IenumtemplatePageObjectModel = lstTemplateObjectModel;
            return IenumtemplatePageObjectModel;
        }
        private IEnumerable<PageNavigationObjectModel> CreateListPageNavigation()
        {
            IEnumerable<PageNavigationObjectModel> IenumPageNavigationObjectModel = Enumerable.Empty<PageNavigationObjectModel>();
            List<PageNavigationObjectModel> lstPageNavigationObjectModel = new List<PageNavigationObjectModel>();
            PageNavigationObjectModel objPageNavigationObjectModel = new PageNavigationObjectModel();
            objPageNavigationObjectModel.NavigationKey = "test";
            objPageNavigationObjectModel.PageNavigationId = 8;
            objPageNavigationObjectModel.PageDescription = "Test_Doc";
            objPageNavigationObjectModel.PageId = 1;
            objPageNavigationObjectModel.Name = "Test_Doc";
            objPageNavigationObjectModel.Version = 2;
            objPageNavigationObjectModel.ModifiedBy = 25;
            objPageNavigationObjectModel.UtcModifiedDate = DateTime.Today;
            objPageNavigationObjectModel.IsProofing = true;
            lstPageNavigationObjectModel.Add(objPageNavigationObjectModel);
            IenumPageNavigationObjectModel = lstPageNavigationObjectModel;
            return IenumPageNavigationObjectModel;
        }

        private PageNavigationObjectModel CreatePageNavigationObject()
        {
          
            PageNavigationObjectModel objPageNavigationObjectModel = new PageNavigationObjectModel();
            objPageNavigationObjectModel.NavigationKey = "test";
            objPageNavigationObjectModel.PageNavigationId = 8;
            objPageNavigationObjectModel.PageId = 1;
            objPageNavigationObjectModel.Name = "Test_Doc";
            objPageNavigationObjectModel.Version = 2;
            objPageNavigationObjectModel.ModifiedBy = 25;
            objPageNavigationObjectModel.UtcModifiedDate = DateTime.Today;
            objPageNavigationObjectModel.IsProofing = true;

            return objPageNavigationObjectModel;
        }
        private IEnumerable<UserObjectModel> CreateListUser()
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
        private IEnumerable<SiteObjectModel> CreateSiteList()
        {
            IEnumerable<SiteObjectModel> IEnumUser = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lstSiteObjectModel = new List<SiteObjectModel>();
            SiteObjectModel objSiteObjectModel = new SiteObjectModel();
            objSiteObjectModel.SiteID = 1;
            objSiteObjectModel.Name = "Test_UserName";
            objSiteObjectModel.TemplateId = 3;
            objSiteObjectModel.Description = "Test_UserName";
            lstSiteObjectModel.Add(objSiteObjectModel);
            IEnumUser = lstSiteObjectModel;
            return IEnumUser;
        }
        #endregion ReturnLists

        #region List_Returns_ActionResult
        /// <summary>
        ///List_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void List_Returns_ActionResult()
        {
            //Act
            PageNavigationController objPageNavigationController =
        new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.List();

            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageNavigation_Returns_ActionResult_GEtMethod
        /// <summary>
        /// EditPageNavigation_Returns_ActionResult_GEtMethod
        /// </summary>
        [TestMethod]
        public void EditPageNavigation_Returns_ActionResult_GEtMethod()
        {
            //Arrange
            mockPageNavigationFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<PageNavigationKey>())).Returns(CreatePageNavigationObject());
            mocktemplatePageCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());
            mocktemplatePageNavigationCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageNavigationList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.EditPageNavigation(8, 2);

            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Navigation Key--", Value = "-1" });
            lstNavKeys.Add(new DisplayValuePair() { Display = "1test", Value = "1test" });

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });

            EditPageNavigationViewModel objExpected = new EditPageNavigationViewModel()
            {
                IsProofing = true,
                IsProofingAvailableForPageNavigationId = false,
                NavigationKeys = lstNavKeys,
                NavigationXML = null,
                PageDescriptions = lstPageDesc,
                PageNavigationID = 8,
                SelectedNavigationKey = "test",
                SelectedPageID = 1,
                VersionID = 2,
                ModifiedBy = null,
                ModifiedByName = "25"
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageNavigationViewModel;
            ValidateViewModelData<EditPageNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditPageNavigationViewModel));

            //Verify and Assert
            mockPageNavigationFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplatePageNavigationCacheFactory.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageNavigation_Returns_ActionResult_GEtMethod_EmptyNavDtls
        /// <summary>
        /// EditPageNavigation_Returns_ActionResult_GEtMethod_EmptyNavDtls
        /// </summary>
        [TestMethod]
        public void EditPageNavigation_Returns_ActionResult_GEtMethod_EmptyNavDtls()
        {
            //Arrange
            IEnumerable<PageNavigationObjectModel> IenumPageNavigationObjectModel = Enumerable.Empty<PageNavigationObjectModel>();
            List<PageNavigationObjectModel> lstPageNavigationObjectModel = new List<PageNavigationObjectModel>();
            lstPageNavigationObjectModel.Add(new PageNavigationObjectModel()
            {
                NavigationKey = "test.css",
                PageNavigationId = 8,
                PageId = 1,
                Name = "Test_Doc",
                Version = 2,
                ModifiedBy = 25,
                UtcModifiedDate = DateTime.Today,
                IsProofing = true,
                Text = "Test.Test}#Test;Test{Test}Test:Test"
            });
            IenumPageNavigationObjectModel = lstPageNavigationObjectModel;


            mockPageNavigationFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<PageNavigationKey>())).Returns(CreatePageNavigationObject());
            mocktemplatePageCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());
            mocktemplatePageNavigationCacheFactory.Setup(x => x.GetAllEntities()).Returns(Enumerable.Empty<TemplatePageNavigationObjectModel>());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.EditPageNavigation(8, 2);

            //Verify and Assert
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Navigation Key--", Value = "-1" });

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });
            EditPageNavigationViewModel objExpected = new EditPageNavigationViewModel()
            {
                IsProofing = true,
                IsProofingAvailableForPageNavigationId = false,
                NavigationKeys = lstNavKeys,
                NavigationXML = null,
                PageDescriptions = lstPageDesc,
                PageNavigationID = 8,
                SelectedNavigationKey = "test",
                SelectedPageID = 1,
                VersionID = 2,
                Text = null,
                ModifiedByName = "25"
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageNavigationViewModel;
            ValidateViewModelData<EditPageNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);

            mockPageNavigationFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplatePageNavigationCacheFactory.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageNavigation_Returns_ActionResult_GEtMethod_ForAdd
        /// <summary>
        /// EditPageNavigation_Returns_ActionResult_GEtMethod_ForAdd
        /// </summary>
        [TestMethod]
        public void EditPageNavigation_Returns_ActionResult_GEtMethod_ForAdd()
        {
            //Arrange

            mocktemplatePageCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.EditPageNavigation(0, 2);


            //Verify and Assert
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Navigation Key--", Value = "-1" });

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });

            EditPageNavigationViewModel objExpected = new EditPageNavigationViewModel()
            {
                NavigationKeys = lstNavKeys,
                PageDescriptions = lstPageDesc,
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageNavigationViewModel;
            ValidateViewModelData<EditPageNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);

            mocktemplatePageCacheFactory.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageNavigation_Returns_ActionResult_PostMethod
        /// <summary>
        /// EditPageNavigation_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditPageNavigation_Returns_ActionResult_PostMethod()
        {
            //Arrange
            //Mocking for View Model Unit Testing
            EditPageNavigationViewModel objEditPageNavigationViewModel = new EditPageNavigationViewModel();
            objEditPageNavigationViewModel.Text = "Test_Text";
            objEditPageNavigationViewModel.IsProofingAvailableForPageNavigationId = false;
            objEditPageNavigationViewModel.ModifiedBy = 1;
            objEditPageNavigationViewModel.UTCLastModifiedDate = DateTime.Now;
            objEditPageNavigationViewModel.ModifiedByName = "Test_User";

            mockPageNavigationFactoryCache.Setup(x => x.SaveEntity(It.IsAny<PageNavigationObjectModel>(), It.IsAny<int>()));
            mocktemplatePageCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageList());
            mocktemplatePageNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageNavigationSearchDetail>())).Returns(Enumerable.Empty<TemplatePageNavigationObjectModel>());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.EditPageNavigation(new EditPageNavigationViewModel());


            //Verify and Assert
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Navigation Key--", Value = "-1" });
           
            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });

            EditPageNavigationViewModel objExpected = new EditPageNavigationViewModel()
            {
                NavigationKeys = lstNavKeys,
                PageDescriptions = lstPageDesc,
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageNavigationViewModel;
            ValidateViewModelData<EditPageNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);

            mockPageNavigationFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplatePageNavigationCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageNavigation_Returns_ActionResult_PostMethod_EmptyData
        /// <summary>
        /// EditPageNavigation_Returns_ActionResult_PostMethod_EmptyData
        /// </summary>
        [TestMethod]
        public void EditPageNavigation_Returns_ActionResult_PostMethod_EmptyData()
        {
            //Arrange
            //Mocking for View Model Unit Testing
            EditPageNavigationViewModel objEditPageNavigationViewModel = new EditPageNavigationViewModel();
            objEditPageNavigationViewModel.Text = "Test_Text";
            objEditPageNavigationViewModel.IsProofingAvailableForPageNavigationId = false;
            objEditPageNavigationViewModel.ModifiedBy = 1;
            objEditPageNavigationViewModel.UTCLastModifiedDate = DateTime.Now;
            objEditPageNavigationViewModel.ModifiedByName = "Test_User";


            mockPageNavigationFactoryCache.Setup(x => x.SaveEntity(It.IsAny<PageNavigationObjectModel>(), It.IsAny<int>()));
            mocktemplatePageCacheFactory.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageList());
            mocktemplatePageNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageNavigationSearchDetail>())).Returns(Enumerable.Empty<TemplatePageNavigationObjectModel>());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.EditPageNavigation(new EditPageNavigationViewModel());


            //Verify and Assert
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Navigation Key--", Value = "-1" });

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });

            EditPageNavigationViewModel objExpected = new EditPageNavigationViewModel()
            {
                IsProofing = false,
                IsProofingAvailableForPageNavigationId = false,
                NavigationKeys = lstNavKeys,
                NavigationXML = null,
                PageDescriptions = lstPageDesc,
                SelectedNavigationKey = null,
                VersionID = 0
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageNavigationViewModel;
            ValidateViewModelData<EditPageNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);

            mockPageNavigationFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplatePageNavigationCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditPageNavigation_Returns_ActionResult_PostMethod_ThrowsException
        /// <summary>
        /// EditPageNavigation_Returns_ActionResult_PostMethod_ThrowsException
        /// </summary>
        [TestMethod]
        public void EditPageNavigation_Returns_ActionResult_PostMethod_ThrowsException()
        {
            //Arrange

            mockPageNavigationFactoryCache.Setup(x => x.SaveEntity(It.IsAny<PageNavigationObjectModel>(), It.IsAny<int>()));
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Throws(new Exception());

            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.EditPageNavigation(new EditPageNavigationViewModel());


            //Verify and Assert
            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });

            EditPageNavigationViewModel objExpected = new EditPageNavigationViewModel()
            {
                PageDescriptions = lstPageDesc,
                SuccessOrFailedMessage="Exception of type 'System.Exception' was thrown."
            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditPageNavigationViewModel;
            ValidateViewModelData<EditPageNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);

            mockPageNavigationFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetNavigationKeys_Returns_JsonResult
        /// <summary>
        /// GetNavigationKeys_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetNavigationKeys_Returns_JsonResult()
        {
            //Arrange
            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>()))
                .Returns(CreateListPageNavigation());
            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetNavigationKey();

            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "test", Value = "test" });
            ValidateDisplayValuePair(lstExpected, result);

            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetNavigationKeys_Returns_JsonResult_WithEmptyData
        /// <summary>
        /// GetNavigationKeys_Returns_JsonResult_WithEmptyData
        /// </summary>
        [TestMethod]
        public void GetNavigationKeys_Returns_JsonResult_WithEmptyData()
        {
            //Arrange
            IEnumerable<PageNavigationObjectModel> IenumPageNavigationObjectModel = Enumerable.Empty<PageNavigationObjectModel>();
            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>()))
                .Returns(IenumPageNavigationObjectModel);
            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetNavigationKey();

            //Verify and Assert
            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region ValidateXml_Returns_JsonResult_Exception
        /// <summary>
        /// GetNavigationKeys_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void ValidateXml_Returns_JsonResult_Exception()
        {
            //Arrange
            //mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>()))
            //    .Returns(CreateListPageNavigation());
            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.ValidateXml("test");

            //Verify and Assert
            //mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region ValidateXml_Returns_JsonResult
        /// <summary>
        /// GetNavigationKeys_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void ValidateXml_Returns_JsonResult()
        {
            //Arrange
            //mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>()))
            //    .Returns(CreateListPageNavigation());
            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.ValidateXml(string.Empty);

            //Verify and Assert
            //mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region DisablePageNavigation_Returns_JsonResult
        /// <summary>
        /// DisablePageNavigation_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DisablePageNavigation_Returns_JsonResult()
        {
            //Arrange
            mockPageNavigationFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<PageNavigationObjectModel>(), It.IsAny<int>()));

            //Act
            PageNavigationController objPageNavigationController =
         new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.DisablePageNavigation(1, 5, true);

            //Verify and Assert
            Assert.AreEqual(string.Empty, result.Data);
            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllPageNavigation_Action_Calls_PageNavigationController
        /// <summary>
        /// GetAllPageNavigation_Action_Calls_PageNavigationController
        /// </summary>
        [TestMethod]
        public void GetAllPageNavigation_Details_Action_Calls_PageNavigationController()
        {
            //Arrange
            IEnumerable<PageNavigationObjectModel> IenumPageNavigationObjectModel = Enumerable.Empty<PageNavigationObjectModel>();
            List<PageNavigationObjectModel> lstPageNavigationObjectModel = new List<PageNavigationObjectModel>();

            PageNavigationObjectModel objPageNavigationObjectModel = new PageNavigationObjectModel();
            objPageNavigationObjectModel.PageNavigationId = 1;
            objPageNavigationObjectModel.PageId = 4;
            objPageNavigationObjectModel.Name = "Test_Doc";
            objPageNavigationObjectModel.IsProofing = true;
            objPageNavigationObjectModel.ModifiedBy = 1;
            lstPageNavigationObjectModel.Add(objPageNavigationObjectModel);
            IenumPageNavigationObjectModel = lstPageNavigationObjectModel;

            PageNavigationSearchDetail objPageNavigationSearchDetail = new PageNavigationSearchDetail();
            objPageNavigationSearchDetail.PageNavigationId = 1;
            objPageNavigationSearchDetail.PageID = 7;
            objPageNavigationSearchDetail.Name = "p7";

            PageNavigationSortDetail objPageNavigationSortDetail = new PageNavigationSortDetail();
            objPageNavigationSortDetail.Column = PageNavigationSortColumn.NavigationKey;

            //Act
            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>()))
            .Returns(CreateListPageNavigation());
            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PageNavigationSearchDetail>(), It.IsAny<PageNavigationSortDetail>()))
                .Returns(CreateListPageNavigation());

            PageNavigationController objPageNavigationController =
              new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetAllPageNavigationDetails("Test", "4", "Proofing");
            var result1 = objPageNavigationController.GetAllPageNavigationDetails("Test", "4", "Production");
            var result2 = objPageNavigationController.GetAllPageNavigationDetails(null, null, null);
            var result3 = objPageNavigationController.GetAllPageNavigationDetails("Test", "4abc", "Production");

            //Verify and Assert
            List<PageNavigationViewModel> lstExpected = new List<PageNavigationViewModel>();
            PageNavigationViewModel objViewModel = new PageNavigationViewModel();
            objViewModel.PageNavigationId = 8;
            objViewModel.NavigationKey = "test";
            objViewModel.Version = "Proofing";
            objViewModel.VersionID = 2;
            objViewModel.ModifiedBy = null;
            objViewModel.IsProofing = true;
            objViewModel.PageDescription = "Test_Doc";
            objViewModel.UtcModifiedDate = null;
            lstExpected.Add(objViewModel);

            ValidateData(lstExpected, result);
            ValidateData(lstExpected, result1);
            ValidateData(lstExpected, result2);
            ValidateData(lstExpected, result3);

            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.IsInstanceOfType(result1, typeof(JsonResult));
            Assert.IsInstanceOfType(result2, typeof(JsonResult));
            Assert.IsInstanceOfType(result3, typeof(JsonResult));
        }
        #endregion

        #region GetAllPageNavigation_Details_Action_Calls_PageNavigationController_WithFalseProofing
        /// <summary>
        /// GetAllPageNavigation_Details_Action_Calls_PageNavigationController_WithFalseProofing
        /// </summary>
        [TestMethod]
        public void GetAllPageNavigation_Details_Action_Calls_PageNavigationController_WithFalseProofing()
        {
            //Arrange
            IEnumerable<PageNavigationObjectModel> IenumPageNavigationObjectModel = Enumerable.Empty<PageNavigationObjectModel>();
            List<PageNavigationObjectModel> lstPageNavigationObjectModel = new List<PageNavigationObjectModel>();

            PageNavigationObjectModel objPageNavigationObjectModel = new PageNavigationObjectModel();
            objPageNavigationObjectModel.PageNavigationId = 1;
            objPageNavigationObjectModel.PageId = 4;
            objPageNavigationObjectModel.Name = "Test_Doc";
            objPageNavigationObjectModel.IsProofing = false;
            objPageNavigationObjectModel.ModifiedBy = 1;
            lstPageNavigationObjectModel.Add(objPageNavigationObjectModel);
            IenumPageNavigationObjectModel = lstPageNavigationObjectModel;

            PageNavigationSearchDetail objPageNavigationSearchDetail = new PageNavigationSearchDetail();
            objPageNavigationSearchDetail.PageNavigationId = 1;
            objPageNavigationSearchDetail.PageID = 7;
            objPageNavigationSearchDetail.Name = "p7";

            PageNavigationSortDetail objPageNavigationSortDetail = new PageNavigationSortDetail();
            objPageNavigationSortDetail.Column = PageNavigationSortColumn.NavigationKey;

            //Act
            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PageNavigationSearchDetail>(), It.IsAny<PageNavigationSortDetail>()))
                .Returns(IenumPageNavigationObjectModel);

            PageNavigationController objPageNavigationController =
              new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetAllPageNavigationDetails("Test", "4", "Proofing");

            //Verify and Assert

            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllPageNavigation_Action_Calls_PageNavigationController_WithInvalidVersion
        /// <summary>
        /// GetAllPageNavigation_Action_Calls_PageNavigationController
        /// </summary>
        [TestMethod]
        public void GetAllPageNavigation_Details_Action_Calls_PageNavigationController_WithInvalidVersion()
        {
            PageNavigationController objPageNavigationController =
               new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetAllPageNavigationDetails("Test", "4", "Version1");


            //Verify and Assert

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageNames_Returns_JsonResults
        ///<summary>
        ///GetPageNames
        ///</summary>
        [TestMethod]
        public void GetPageNames_Returns_JsonResults()
        {
            //Arrange

          
            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>())).Returns(CreateListPageNavigation());

            //Act
            PageNavigationController objPageNavigationController =
        new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetPageNames();


            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            mocktemplatePageNavigationCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageNames_Returns_JsonResults_EmptyData
        ///<summary>
        ///GetPageNames_Returns_JsonResults_EmptyData
        ///</summary>
        [TestMethod]
        public void GetPageNames_Returns_JsonResults_EmptyData()
        {
            //Arrange

            
            IEnumerable<PageNavigationObjectModel> IenumPageNavigationObjectModel = Enumerable.Empty<PageNavigationObjectModel>();
            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>())).Returns(IenumPageNavigationObjectModel);

            //Act
            PageNavigationController objPageNavigationController =
        new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetPageNames();

            //Verify and Assert
            mocktemplatePageNavigationCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetVersions_Returns_JsonResults
        ///<summary>
        ///GetVersions_Results_Json
        ///</summary>
        [TestMethod]
        public void GetVersions_Returns_JsonResults()
        {
            //Arrange
            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>()))
                .Returns(CreateListPageNavigation());

            //Act
            PageNavigationController objPageNavigationController =
       new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetVersions();

            //Verify and Assert

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Proofing", Value = "Proofing" });
            ValidateDisplayValuePair(lstexpected, result);

            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetVersions_Returns_JsonResults_WithProductionVersion
        ///<summary>
        ///GetVersions_Returns_JsonResults_WithProductionVersion
        ///</summary>
        [TestMethod]
        public void GetVersions_Returns_JsonResults_WithProductionVersion()
        {
            //Arrange

            IEnumerable<PageNavigationObjectModel> IenumPageNavigationObjectModel = Enumerable.Empty<PageNavigationObjectModel>();
            List<PageNavigationObjectModel> lstPageNavigationObjectModel = new List<PageNavigationObjectModel>();
            lstPageNavigationObjectModel.Add(new PageNavigationObjectModel() { IsProofing = false });
            IenumPageNavigationObjectModel = lstPageNavigationObjectModel;

            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>()))
                .Returns(IenumPageNavigationObjectModel);

            //Act
            PageNavigationController objPageNavigationController =
       new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetVersions();

            //Verify and Assert

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Production", Value = "Production" });
            ValidateDisplayValuePair(lstexpected, result);

            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetVersions_Returns_JsonResults_EmptyData
        ///<summary>
        ///GetVersions_Returns_JsonResults_EmptyData
        ///</summary>
        [TestMethod]
        public void GetVersions_Returns_JsonResults_EmptyData()
        {
            //Arrange
            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>()))
                .Returns(Enumerable.Empty<PageNavigationObjectModel>());

            //Act
            PageNavigationController objPageNavigationController =
       new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.GetVersions();

            //Verify and Assert

            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadNavigationKeys_Returns_JsonResults
        ///<summary>
        ///LoadNavigationKeys
        ///</summary>
        [TestMethod]
        public void LoadNavigationKeys_Returns_JsonResults()
        {
            //Arrange

            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>())).Returns(CreateListPageNavigation());
            mocktemplatePageNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageNavigationSearchDetail>())).Returns(CreateTemplatePageNavigationList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageNavigationController objPageNavigationController =
        new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.LoadNavigationKeys(1);

            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "--Please select Navigation Key--", Value = "-1" });
            lstExpected.Add(new DisplayValuePair() { Display = "1test", Value = "1test" });
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            mocktemplatePageNavigationCacheFactory.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadNavigationKeys_Returns_JsonResults_EmptyData
        ///<summary>
        ///LoadNavigationKeys_Returns_JsonResults_EmptyData
        ///</summary>
        [TestMethod]
        public void LoadNavigationKeys_Returns_JsonResults_EmptyData()
        {
            //Arrange

            mockPageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<PageNavigationSearchDetail>())).Returns(CreateListPageNavigation());
            mocktemplatePageNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageNavigationSearchDetail>())).Returns(Enumerable.Empty<TemplatePageNavigationObjectModel>());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageNavigationController objPageNavigationController =
        new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.LoadNavigationKeys(1);

            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "--Please select Navigation Key--", Value = "-1" });
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            mocktemplatePageNavigationCacheFactory.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadDefaultTextForNavigationKey_Returns_JsonResults
        ///<summary>
        ///LoadDefaultTextForNavigationKey_Returns_JsonResults
        ///</summary>
        [TestMethod]
        public void LoadDefaultTextForNavigationKey_Returns_JsonResults()
        {
            //Arrange
            mocktemplatePageNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageNavigationSearchDetail>())).Returns(CreateTemplatePageNavigationList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageNavigationController objPageNavigationController =
        new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.LoadDefaultTextForNavigationKey(1, "Test");

            //Verify and Assert
            mocktemplatePageNavigationCacheFactory.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadDefaultTextForNavigationKey_Returns_JsonResults_EmptyData
        ///<summary>
        ///LoadDefaultTextForNavigationKey_Returns_JsonResults_EmptyData
        ///</summary>
        [TestMethod]
        public void LoadDefaultTextForNavigationKey_Returns_JsonResults_EmptyData()
        {
            //Arrange

            mocktemplatePageNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageNavigationSearchDetail>())).Returns(Enumerable.Empty<TemplatePageNavigationObjectModel>());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            PageNavigationController objPageNavigationController =
        new PageNavigationController(mockPageNavigationFactoryCache.Object, mockUserFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplatePageNavigationCacheFactory.Object, mockSiteFactoryCache.Object);
            var result = objPageNavigationController.LoadDefaultTextForNavigationKey(1, "Test");

            //Verify and Assert

            mocktemplatePageNavigationCacheFactory.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            // mockPageNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

    }
}