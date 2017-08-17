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
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for SiteNavigationController class
    /// </summary>
    [TestClass]
    public class SiteNavigationControllerTests : BaseTestController<SiteNavigationViewModel>
    {
        Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>> mockSiteFactoryCache;
        Mock<IFactoryCache<SiteNavigationFactory, SiteNavigationObjectModel, SiteNavigationKey>> mockSiteNavigationFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;
        Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>> mocktemplatePageCacheFactory;
        Mock<IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey>> mocktemplateNavigationCacheFactory;

        [TestInitialize]
        public void TestInitialze()
        {
            mockSiteFactoryCache = new Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>>();
            mockSiteNavigationFactoryCache = new Mock<IFactoryCache<SiteNavigationFactory, SiteNavigationObjectModel, SiteNavigationKey>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mocktemplatePageCacheFactory = new Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>();
            mocktemplateNavigationCacheFactory = new Mock<IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey>>();
        }
        #region ReturnLists
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageList()
        {
            IEnumerable<TemplatePageObjectModel> IenumPageObjectModel = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTeplateObjectModel = new List<TemplatePageObjectModel>();

            TemplatePageObjectModel objPageObjectModel = new TemplatePageObjectModel();
            objPageObjectModel.PageID = 2;
            objPageObjectModel.PageDescription = "Test";


            lstTeplateObjectModel.Add(objPageObjectModel);
            IenumPageObjectModel = lstTeplateObjectModel;
            return IenumPageObjectModel;
        }
        private IEnumerable<TemplateNavigationObjectModel> CreateTemplateNavigationList()
        {
            IEnumerable<TemplateNavigationObjectModel> IenumPageObjectModel = Enumerable.Empty<TemplateNavigationObjectModel>();
            List<TemplateNavigationObjectModel> lstTeplateObjectModel = new List<TemplateNavigationObjectModel>();

            TemplateNavigationObjectModel TemplateNavigationObjectModel = new TemplateNavigationObjectModel();
            TemplateNavigationObjectModel.NavigationKey = "1test";
            TemplateNavigationObjectModel.Name = "Test_Doc";
            TemplateNavigationObjectModel.TemplateID = 56;
            TemplateNavigationObjectModel.PageID = 1;

            lstTeplateObjectModel.Add(TemplateNavigationObjectModel);
            IenumPageObjectModel = lstTeplateObjectModel;
            return IenumPageObjectModel;
        }
        private IEnumerable<SiteNavigationObjectModel> CreateListSiteNavigation()
        {
            IEnumerable<SiteNavigationObjectModel> IenumSitenavigationObjectModel = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();

            objSiteNavigationObjectModel.NavigationKey = "Test";

            objSiteNavigationObjectModel.SiteNavigationId = 1;
            objSiteNavigationObjectModel.PageId = 1;
            objSiteNavigationObjectModel.PageDescription = "Test";
            objSiteNavigationObjectModel.Name = "Test_Doc";
            objSiteNavigationObjectModel.Version = 1;
            objSiteNavigationObjectModel.SiteNavigationId = 1;
            objSiteNavigationObjectModel.SiteId = 1;
            objSiteNavigationObjectModel.PageName = "Test";
            objSiteNavigationObjectModel.IsProofing = false;
            objSiteNavigationObjectModel.IsProofingAvailableForSiteNavigationId = true;
            objSiteNavigationObjectModel.NavigationXML = "True";
            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel);
            IenumSitenavigationObjectModel = lstSiteNavigationObjectModel;
            return IenumSitenavigationObjectModel;
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
            SiteNavigationController objSiteNavigationController =
        new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.List();

            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSiteNavigation_Returns_ActionResult_PostMethod
        /// <summary>
        /// EditSiteNavigation_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditSiteNavigation_Returns_ActionResult_PostMethod()
        {
            //Mocking for View Model Unit Testing.

            EditSiteNavigationViewModel objEditSiteNavigationViewModel = new EditSiteNavigationViewModel();
            objEditSiteNavigationViewModel.Text = "Text_test";
            objEditSiteNavigationViewModel.ModifiedBy = 0;
            objEditSiteNavigationViewModel.SuccessOrFailedMessage = "success";
            objEditSiteNavigationViewModel.UserName = "TestUser";

            //Arrange

            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                  .Returns(CreateSiteList);
            mockSiteNavigationFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteNavigationObjectModel>(), It.IsAny<int>()));
            mocktemplatePageCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateTemplatePageList());
            mocktemplateNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateNavigationSearchDetail>()))
                .Returns(CreateTemplateNavigationList());


            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.Edit(new EditSiteNavigationViewModel());


            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = null });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test", Value = "2" });

            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            //lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select NavigationKey--", Value = "-1" });
            lstNavKeys.Add(new DisplayValuePair() { Display = "1test", Value = "1test" });

            EditSiteNavigationViewModel objExpected = new EditSiteNavigationViewModel()
            {
                IsProofing = false,
                IsProofingAvailableForSiteNavigationId = false,
                NavigationKeys = lstNavKeys,
                NavigationXML = null,
                PageDescriptions = lstPageDesc,
                SiteNavigationId = 0,
                SelectedNavigationKey = null,
                SelectedPageID = null,
                ModifiedByName = null,
                VersionID = 0,
                ModifiedBy = null,
                SuccessOrFailedMessage = null,
                Text = null,
                UserName = null,
                UTCLastModifiedDate = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteNavigationViewModel;
            ValidateViewModelData<EditSiteNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteNavigationViewModel));


            //Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mockSiteNavigationFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplateNavigationCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSiteNavigation_Returns_ActionResult_PostMethod_Empty
        /// <summary>
        /// EditSiteNavigation_Returns_ActionResult_PostMethod_Empty
        /// </summary>
        [TestMethod]
        public void EditSiteNavigation_Returns_ActionResult_PostMethod_Empty()
        {
            //Mocking for View Model Unit Testing.

            EditSiteNavigationViewModel objEditSiteNavigationViewModel = new EditSiteNavigationViewModel();
            objEditSiteNavigationViewModel.Text = "Text_test";
            objEditSiteNavigationViewModel.ModifiedBy = 0;
            objEditSiteNavigationViewModel.SuccessOrFailedMessage = "success";
            objEditSiteNavigationViewModel.UserName = "TestUser";

            //Arrange
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            IEnumerable<TemplatePageObjectModel> IenumTemplatePageObjectModel = lstTemplatePageObjectModel;

            List<TemplateNavigationObjectModel> lstTemplateNavigationObjectModel = new List<TemplateNavigationObjectModel>();
            IEnumerable<TemplateNavigationObjectModel> IenumTemplateNavigationObjectModel = lstTemplateNavigationObjectModel;

            mockSiteNavigationFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteNavigationObjectModel>(), It.IsAny<int>()));

            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                 .Returns(CreateSiteList);

            mocktemplatePageCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(IenumTemplatePageObjectModel);

            mocktemplateNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateNavigationSearchDetail>()))
                .Returns(IenumTemplateNavigationObjectModel);

            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.Edit(new EditSiteNavigationViewModel());

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = null });
            //lstPageDesc.Add(new DisplayValuePair() { Display = "Test", Value = "2" });

            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            //lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select NavigationKey--", Value = "-1" });
            //lstNavKeys.Add(new DisplayValuePair() { Display = "1test", Value = "1test" });


            EditSiteNavigationViewModel objExpected = new EditSiteNavigationViewModel()
            {
                IsProofing = false,
                IsProofingAvailableForSiteNavigationId = false,
                NavigationKeys = lstNavKeys,
                NavigationXML = null,
                PageDescriptions = lstPageDesc,
                SiteNavigationId = 0,
                SelectedNavigationKey = null,
                SelectedPageID = null,
                ModifiedByName = null,
                VersionID = 0,
                ModifiedBy = null,
                SuccessOrFailedMessage = null,
                Text = null,
                UserName = null,
                UTCLastModifiedDate = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteNavigationViewModel;
            ValidateViewModelData<EditSiteNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteNavigationViewModel));


            //Verify and Assert
            mockSiteNavigationFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplateNavigationCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSiteNavigation_Returns_ActionResult_PostMethod_Exception
        /// <summary>
        /// EditSiteNavigation_Returns_ActionResult_PostMethod_Exception
        /// </summary>
        [TestMethod]
        public void EditSiteNavigation_Returns_ActionResult_PostMethod_Exception()
        {
            //Mocking for View Model Unit Testing.

            EditSiteNavigationViewModel objEditSiteNavigationViewModel = new EditSiteNavigationViewModel();
            objEditSiteNavigationViewModel.Text = "Text_test";
            objEditSiteNavigationViewModel.ModifiedBy = 0;
            objEditSiteNavigationViewModel.SuccessOrFailedMessage = "success";
            objEditSiteNavigationViewModel.UserName = "TestUser";

            //Arrange
            mockSiteNavigationFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteNavigationObjectModel>(), It.IsAny<int>())).Throws(new Exception());
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.Edit(new EditSiteNavigationViewModel());

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = null });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test", Value = "2" });

            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            //lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select NavigationKey--", Value = "-1" });
            lstNavKeys.Add(new DisplayValuePair() { Display = "1test", Value = "1test" });


            EditSiteNavigationViewModel objExpected = new EditSiteNavigationViewModel()
            {
                IsProofing = false,
                IsProofingAvailableForSiteNavigationId = false,
                NavigationKeys = null,
                NavigationXML = null,
                PageDescriptions = null,
                SiteNavigationId = 0,
                SelectedNavigationKey = null,
                SelectedPageID = null,
                ModifiedByName = null,
                VersionID = 0,
                ModifiedBy = null,
                SuccessOrFailedMessage = "Exception of type 'System.Exception' was thrown.",
                Text = null,
                UserName = null,
                UTCLastModifiedDate = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteNavigationViewModel;
            ValidateViewModelData<EditSiteNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteNavigationViewModel));

            //Verify and Assert
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSiteNavigation_Returns_ActionResult_GetMethod
        /// <summary>
        /// EditSiteNavigation_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void EditSiteNavigation_Returns_ActionResult_GetMethod()
        {
            //Arrange
            EditSiteNavigationViewModel objEditSiteNavigationViewModel = new EditSiteNavigationViewModel();
            objEditSiteNavigationViewModel.SelectedNavigationKey = "Test";
            objEditSiteNavigationViewModel.SiteNavigationId = 1;
            objEditSiteNavigationViewModel.VersionID = 1;
            objEditSiteNavigationViewModel.IsProofing = true;
            objEditSiteNavigationViewModel.IsProofingAvailableForSiteNavigationId = true;
            objEditSiteNavigationViewModel.NavigationXML = "True";
            objEditSiteNavigationViewModel.Text = "Text_test";
            objEditSiteNavigationViewModel.ModifiedBy = 1;
            objEditSiteNavigationViewModel.SuccessOrFailedMessage = "success";
            objEditSiteNavigationViewModel.UserName = "TestUser";

            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();
            objSiteNavigationObjectModel.SiteNavigationId = 1;

            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList);
            mocktemplatePageCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>()))
                .Returns(CreateTemplatePageList());
            mocktemplateNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateNavigationSearchDetail>()))
                .Returns(CreateTemplateNavigationList());
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(CreateListSiteNavigation());
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
               .Returns(CreateListUser);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.Edit(1, 1);

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = null });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test", Value = "2" });

            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            //lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Navigation Key--", Value = "-1" });
            lstNavKeys.Add(new DisplayValuePair() { Display = "1test", Value = "1test" });


            EditSiteNavigationViewModel objExpected = new EditSiteNavigationViewModel()
            {
                IsProofing = false,
                IsProofingAvailableForSiteNavigationId = true,
                NavigationKeys = lstNavKeys,
                NavigationXML = "True",
                PageDescriptions = lstPageDesc,
                SiteNavigationId = 1,
                SelectedNavigationKey = "Test",
                SelectedPageID = 1,
                ModifiedByName = "Test_UserName",
                VersionID = 1,
                ModifiedBy = null,
                SuccessOrFailedMessage = null,
                Text = null,
                UserName = null,
                UTCLastModifiedDate = null,


            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteNavigationViewModel;
            ValidateViewModelData<EditSiteNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteNavigationViewModel));

            //Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplateNavigationCacheFactory.VerifyAll();
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSiteNavigation_Returns_ActionResult_Empty
        /// <summary>
        /// EditSiteNavigation_Returns_ActionResult_Empty
        /// </summary>
        [TestMethod]
        public void EditSiteNavigation_Returns_ActionResult_Empty()
        {
            //Arrange
            //   EditSiteNavigationViewModel objEditSiteNavigationViewModel = new EditSiteNavigationViewModel();

            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            IEnumerable<SiteNavigationObjectModel> enumSiteNavigation = lstSiteNavigationObjectModel;

            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            IEnumerable<TemplatePageObjectModel> enumTemplatePageObjectModel = lstTemplatePageObjectModel;

            List<TemplateNavigationObjectModel> lstTemplateNavigationObjectModel = new List<TemplateNavigationObjectModel>();
            IEnumerable<TemplateNavigationObjectModel> enumTemplateNavigationObjectModel = lstTemplateNavigationObjectModel;

            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            IEnumerable<UserObjectModel> IenumUserObjectModel = lstUserObjectModel;
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
            .Returns(CreateSiteList);
            mocktemplatePageCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>()))
                .Returns(CreateTemplatePageList());
            mocktemplateNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateNavigationSearchDetail>()))
                .Returns(CreateTemplateNavigationList());
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(enumSiteNavigation);

            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.Edit(0, 0);

            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = null });
            //lstPageDesc.Add(new DisplayValuePair() { Display = "Test", Value = "2" });

            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select NavigationKey--", Value = "-1" });
            lstNavKeys.Add(new DisplayValuePair() { Display = "1test", Value = "1test" });


            EditSiteNavigationViewModel objExpected = new EditSiteNavigationViewModel()
            {
                IsProofing = false,
                IsProofingAvailableForSiteNavigationId = false,
                NavigationKeys = lstNavKeys,
                NavigationXML = null,
                PageDescriptions = lstPageDesc,
                SiteNavigationId = 0,
                SelectedNavigationKey = null,
                SelectedPageID = null,
                ModifiedByName = null,
                VersionID = 0,
                ModifiedBy = null,
                SuccessOrFailedMessage = null,
                Text = null,
                UserName = null,
                UTCLastModifiedDate = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteNavigationViewModel;
            ValidateViewModelData<EditSiteNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteNavigationViewModel));

            //Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplateNavigationCacheFactory.VerifyAll();
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSiteNavigation_Returns_ActionResult_IsEqualsToZero_GetMethod
        /// <summary>
        /// EditSiteNavigation_Returns_ActionResult_IsEqualsToZero_GetMethod
        /// </summary>
        [TestMethod]
        public void EditSiteNavigation_Returns_ActionResult_IsEqualsToZero_GetMethod()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
               .Returns(CreateSiteList);
            mocktemplatePageCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>()))
                 .Returns(CreateTemplatePageList());
            mocktemplateNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateNavigationSearchDetail>()))
                .Returns(CreateTemplateNavigationList());
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(CreateListSiteNavigation());
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.Edit(0, 5);


            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = null });
            //lstPageDesc.Add(new DisplayValuePair() { Display = "Test", Value = "2" });

            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select NavigationKey--", Value = "-1" });
            lstNavKeys.Add(new DisplayValuePair() { Display = "1test", Value = "1test" });


            EditSiteNavigationViewModel objExpected = new EditSiteNavigationViewModel()
            {
                IsProofing = false,
                IsProofingAvailableForSiteNavigationId = false,
                NavigationKeys = lstNavKeys,
                NavigationXML = null,
                PageDescriptions = lstPageDesc,
                SiteNavigationId = 0,
                SelectedNavigationKey = null,
                SelectedPageID = null,
                ModifiedByName = null,
                VersionID = 0,
                ModifiedBy = null,
                SuccessOrFailedMessage = null,
                Text = null,
                UserName = null,
                UTCLastModifiedDate = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteNavigationViewModel;
            ValidateViewModelData<EditSiteNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteNavigationViewModel));


            //Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplateNavigationCacheFactory.VerifyAll();
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSiteNavigation_Returns_ActionResult_VersionIDToZero
        /// <summary>
        /// EditSiteNavigation_Returns_ActionResult_VersionIDToZero
        /// </summary>
        [TestMethod]
        public void EditSiteNavigation_Returns_ActionResult_VersionIDToZero()
        {
            //Arrange
            EditSiteNavigationViewModel objEditSiteNavigationViewModel = new EditSiteNavigationViewModel();
            objEditSiteNavigationViewModel.SelectedNavigationKey = "Test";
            objEditSiteNavigationViewModel.VersionID = 1;
            objEditSiteNavigationViewModel.IsProofing = true;
            objEditSiteNavigationViewModel.IsProofingAvailableForSiteNavigationId = true;
            objEditSiteNavigationViewModel.NavigationXML = "True";
            objEditSiteNavigationViewModel.Text = "Text_test";
            objEditSiteNavigationViewModel.ModifiedBy = 1;
            objEditSiteNavigationViewModel.SuccessOrFailedMessage = "success";
            objEditSiteNavigationViewModel.UserName = "TestUser";


            IEnumerable<SiteNavigationObjectModel> IenumSitenavigationObject = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();

            objSiteNavigationObjectModel.NavigationKey = "Test";

            objSiteNavigationObjectModel.SiteNavigationId = 1;
            objSiteNavigationObjectModel.PageId = 1;
            objSiteNavigationObjectModel.PageDescription = "Test";
            objSiteNavigationObjectModel.Name = "Test_Doc";
            objSiteNavigationObjectModel.Version = 0;
            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel);
            IenumSitenavigationObject = lstSiteNavigationObjectModel;

            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
              .Returns(CreateSiteList);
            mocktemplatePageCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>()))
                .Returns(CreateTemplatePageList());
            mocktemplateNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateNavigationSearchDetail>()))
                .Returns(CreateTemplateNavigationList());

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(IenumSitenavigationObject);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
              .Returns(CreateListUser);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.Edit(1, 0);


            List<DisplayValuePair> lstPageDesc = new List<DisplayValuePair>();
            lstPageDesc.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = null });
            lstPageDesc.Add(new DisplayValuePair() { Display = "Test", Value = "2" });

            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
           // lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select NavigationKey--", Value = "-1" });
            lstNavKeys.Add(new DisplayValuePair() { Display = "1test", Value = "1test" });


            EditSiteNavigationViewModel objExpected = new EditSiteNavigationViewModel()
            {
                IsProofing = false,
                IsProofingAvailableForSiteNavigationId = false,
                NavigationKeys = lstNavKeys,
                NavigationXML = null,
                PageDescriptions = lstPageDesc,
                SiteNavigationId = 1,
                SelectedNavigationKey = "Test",
                SelectedPageID = 1,
                ModifiedByName = "Test_UserName",
                VersionID = 0,
                ModifiedBy = null,
                SuccessOrFailedMessage = null,
                Text = null,
                UserName = null,
                UTCLastModifiedDate = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteNavigationViewModel;
            ValidateViewModelData<EditSiteNavigationViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteNavigationViewModel));


            //Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mocktemplateNavigationCacheFactory.VerifyAll();
            mockSiteNavigationFactoryCache.VerifyAll();
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
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                 .Returns(CreateListSiteNavigation());
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetNavigationKey();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test", Value = "Test" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetNavigationKeys_Returns_JsonResult_Empty
        /// <summary>
        /// GetNavigationKeys_Returns_JsonResult_Empty
        /// </summary>
        [TestMethod]
        public void GetNavigationKeys_Returns_JsonResult_Empty()
        {
            //Arrange
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            IEnumerable<SiteNavigationObjectModel> enumSiteNavigation = lstSiteNavigationObjectModel;
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                 .Returns(enumSiteNavigation);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetNavigationKey();

            //Verify and Assert
            mockSiteNavigationFactoryCache.VerifyAll();

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageNames_Returns_JsonResult
        /// <summary>
        /// GetGetPageNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetPageNames_Returns_JsonResult()
        {
            //Arrange
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                 .Returns(CreateListSiteNavigation());
            SiteNavigationObjectModel site = new SiteNavigationObjectModel();
            site.PageId = 1;
            site.SiteId = 1;
            site.PageName = "Test";
            site.PageDescription = "Test";
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetPageNames();

            //Verify and Assert
            mockSiteNavigationFactoryCache.VerifyAll();
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test", Value = "1"});
            ValidateDisplayValuePair(lstexpected, result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageNames_Returns_JsonResult_PageIDZero
        /// <summary>
        /// GetPageNames_Returns_JsonResult_PageIDZero
        /// </summary>
        [TestMethod]
        public void GetPageNames_Returns_JsonResult_PageIDZero()
        {
            //Arrange
            IEnumerable<SiteNavigationObjectModel> IenumSitenavigationObjectModel = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();
            objSiteNavigationObjectModel.NavigationKey = "Test";
            objSiteNavigationObjectModel.SiteNavigationId = 1;
            objSiteNavigationObjectModel.PageId = 0;
            objSiteNavigationObjectModel.PageDescription = "Test_001";
            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel);
            IenumSitenavigationObjectModel = lstSiteNavigationObjectModel;


            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                 .Returns(IenumSitenavigationObjectModel);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetPageNames();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            //lstexpected.Add(new DisplayValuePair() { Display = "Test", Value = "Test" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageNames_Returns_JsonResult_Null
        /// <summary>
        /// GetPageNames_Returns_JsonResult_Null
        /// </summary>
        [TestMethod]
        public void GetPageNames_Returns_JsonResult_Null()
        {
            //Arrange
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            IEnumerable<SiteNavigationObjectModel> IenumSitenavigationObjectModel = lstSiteNavigationObjectModel;
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                 .Returns(IenumSitenavigationObjectModel);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetPageNames();

            //Verify and Assert
            mockSiteNavigationFactoryCache.VerifyAll();
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetPageNames_Returns_JsonResult_PageIdNull
        /// <summary>
        /// GetPageNames_Returns_JsonResult_Empty
        /// </summary>
        [TestMethod]
        public void GetPageNames_Returns_JsonResult_PageIdNull()
        {
            //Arrange
            IEnumerable<SiteNavigationObjectModel> IenumSitenavigationObjectModel = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();
            objSiteNavigationObjectModel.NavigationKey = "Test";
            objSiteNavigationObjectModel.SiteNavigationId = 1;
            objSiteNavigationObjectModel.PageDescription = "Test_Doc";

            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel);
            IenumSitenavigationObjectModel = lstSiteNavigationObjectModel;


            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                 .Returns(IenumSitenavigationObjectModel);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetPageNames();

            //Verify and Assert
            mockSiteNavigationFactoryCache.VerifyAll();
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
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
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(CreateListSiteNavigation());
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetVersions();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Production", Value = "Production" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetVersions_Returns_JsonResult_Proofing
        /// <summary>
        /// GetVersions_Returns_JsonResult_Proofing
        /// </summary>
        [TestMethod]
        public void GetVersions_Returns_JsonResult_Proofing()
        {
            //Arrange

            IEnumerable<SiteNavigationObjectModel> Ienum = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();
            objSiteNavigationObjectModel.IsProofing = true;
            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel);
            SiteNavigationObjectModel objSiteNavigationObjectModel_Prod = new SiteNavigationObjectModel();
            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel_Prod);
            Ienum = lstSiteNavigationObjectModel;

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(Ienum);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetVersions();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Production", Value = "Production" });
            lstexpected.Add(new DisplayValuePair() { Display = "Proofing", Value = "Proofing" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetVersions_Returns_JsonResult_Emptyresult
        /// <summary>
        /// GetVersions_Returns_JsonResult_Emptyresult
        /// </summary>
        [TestMethod]
        public void GetVersions_Returns_JsonResult_Emptyresult()
        {
            //Arrange
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            IEnumerable<SiteNavigationObjectModel> enumSiteNavigation = lstSiteNavigationObjectModel;
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(enumSiteNavigation);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetVersions();

            //Verify and Assert

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            // lstexpected.Add(new DisplayValuePair() { Display = "Production", Value = "Production" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteNavigationFactoryCache.VerifyAll();
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

            IEnumerable<SiteNavigationObjectModel> IenumSiteNavigationObjectModel = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstPageNavigationObjectModel = new List<SiteNavigationObjectModel>();
            lstPageNavigationObjectModel.Add(new SiteNavigationObjectModel() { IsProofing = false });
            IenumSiteNavigationObjectModel = lstPageNavigationObjectModel;

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(IenumSiteNavigationObjectModel);

            //Act
            SiteNavigationController objSiteNavigationController =
          new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetVersions();

            //Verify and Assert

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Production", Value = "Production" });
            ValidateDisplayValuePair(lstexpected, result);

            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region DisableSiteNavigation_Returns_JsonResult
        /// <summary>
        /// DisableSiteNavigation_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DisableSiteNavigation_Returns_JsonResult()
        {
            //Arrange
            mockSiteNavigationFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<SiteNavigationObjectModel>(), It.IsAny<int>()));

            //Act
            SiteNavigationController objDocumentTypeExternalIdController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objDocumentTypeExternalIdController.DisableSiteNavigation(1, 5, true);

            //Verify and Assert
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.AreEqual(string.Empty, result.Data);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllSiteNavigation_Action_Calls_SiteNavigationController
        /// <summary>
        /// GetAllSiteNavigation_Action_Calls_SiteNavigationController
        /// </summary>
        [TestMethod]
        public void GetAllSiteNavigation_Details_Action_CheckNull_SiteNavigationController()
        {
            //Arrange

            IEnumerable<SiteNavigationObjectModel> IenumSiteNavigationObjectModel = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();

            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();
            objSiteNavigationObjectModel.SiteNavigationId = 1;
            objSiteNavigationObjectModel.PageId = 4;
            objSiteNavigationObjectModel.Name = "Test_Doc";
            objSiteNavigationObjectModel.IsProofing = true;
            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel);
            IenumSiteNavigationObjectModel = lstSiteNavigationObjectModel;

            SiteNavigationSearchDetail objSiteNavigationSearchDetail = new SiteNavigationSearchDetail();
            objSiteNavigationSearchDetail.SiteNavigationId = 1;
            objSiteNavigationSearchDetail.PageId = 7;
            objSiteNavigationSearchDetail.Name = "rrr7";

            SiteNavigationSortDetail objSiteNavigationSortDetail = new SiteNavigationSortDetail();
            List<SiteNavigationViewModel> lstSiteNavigationViewModel = new List<SiteNavigationViewModel>();

            SiteNavigationViewModel objSiteNavigationViewModel = new SiteNavigationViewModel();
            objSiteNavigationViewModel.SiteNavigationId = 1;
            objSiteNavigationViewModel.PageName = "Test";
            objSiteNavigationViewModel.NavigationKey = "Test";
            objSiteNavigationViewModel.VersionID = 1;
            objSiteNavigationViewModel.IsProofing = true;
            objSiteNavigationViewModel.ModifiedBy = "Test";
            objSiteNavigationViewModel.PageDescription = "Test";
            lstSiteNavigationViewModel.Add(objSiteNavigationViewModel);

            //Act
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteNavigationSearchDetail>(), It.IsAny<SiteNavigationSortDetail>()))
                .Returns(CreateListSiteNavigation);

            SiteNavigationController objSiteNavigationController =
              new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetAllSiteNavigationDetails("Test", "4", "Proofing");

            //Verify and Assert
            List<SiteNavigationViewModel> lstExpected = new List<SiteNavigationViewModel>();
            SiteNavigationViewModel objViewModel = new SiteNavigationViewModel();
            objViewModel.IsProofing = false;
            objViewModel.IsProofingAvailableForPageTextId = false;
            objViewModel.ModifiedBy = null;
            objViewModel.NavigationKey = "Test";
            objViewModel.PageDescription = "Test";
            objViewModel.PageName = "Test";
            objViewModel.SiteNavigationId = 1;
            objViewModel.UtcModifiedDate = null;
            objViewModel.Version = "Production";
            objViewModel.VersionID = 1;
            lstExpected.Add(objViewModel);

            ValidateEmptyData<SiteNavigationViewModel>(result);
            // ValidateData(lstExpected, result1);


            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            //  Assert.IsInstanceOfType(result1, typeof(JsonResult));

        }
        #endregion

        #region GetAllSiteNavigation_Action_Calls_SiteNavigationController_Production
        /// <summary>
        /// GetAllSiteNavigation_Action_Calls_SiteNavigationController
        /// </summary>
        [TestMethod]
        public void GetAllSiteNavigation_Details_Action_Calls_SiteNavigationController()
        {
            //Arrange

            IEnumerable<SiteNavigationObjectModel> IenumSiteNavigationObjectModel = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();

            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();
            objSiteNavigationObjectModel.SiteNavigationId = 1;
            objSiteNavigationObjectModel.PageId = 4;
            objSiteNavigationObjectModel.Name = "Test_Doc";
            objSiteNavigationObjectModel.IsProofing = false;
            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel);
            IenumSiteNavigationObjectModel = lstSiteNavigationObjectModel;

            SiteNavigationSearchDetail objSiteNavigationSearchDetail = new SiteNavigationSearchDetail();
            objSiteNavigationSearchDetail.SiteNavigationId = 1;
            objSiteNavigationSearchDetail.PageId = 7;
            objSiteNavigationSearchDetail.Name = "rrr7";
            objSiteNavigationSearchDetail.IsProofing = false;

            SiteNavigationSortDetail objSiteNavigationSortDetail = new SiteNavigationSortDetail();
            objSiteNavigationSortDetail.Column = SiteNavigationSortColumn.NavigationKey;

            //Act

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteNavigationSearchDetail>(), It.IsAny<SiteNavigationSortDetail>()))
                .Returns(CreateListSiteNavigation);

            SiteNavigationController objSiteNavigationController =
              new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetAllSiteNavigationDetails("Test", "4", "Production");

            //Verify and Assert

            ValidateEmptyData<SiteNavigationViewModel>(result);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllSiteNavigation_Action_Calls_SiteNavigationController_VersionInvalid
        /// <summary>
        /// GetAllSiteNavigation_Action_Calls_SiteNavigationController_VersionInvalid
        /// </summary>
        [TestMethod]
        public void GetAllSiteNavigation_Action_Calls_SiteNavigationController_VersionInvalid()
        {
            //Arrange

            //Act
            SiteNavigationController objSiteNavigationController =
              new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetAllSiteNavigationDetails("Test", "4", "Test");

            //Verify and Assert
            ValidateEmptyData<SiteNavigationViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllSiteNavigation_Action_Calls_SiteNavigationController_Proofing
        /// <summary>
        /// GetAllSiteNavigation_Action_Calls_SiteNavigationController
        /// </summary>
        [TestMethod]
        public void GetAllSiteNavigation_Action_Calls_SiteNavigationController_Proofing()
        {
            //Arrange

            IEnumerable<SiteNavigationObjectModel> IenumSiteNavigationObjectModel = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();

            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();
            objSiteNavigationObjectModel.SiteNavigationId = 1;
            objSiteNavigationObjectModel.PageId = 4;
            objSiteNavigationObjectModel.Name = "Test_Doc";
            objSiteNavigationObjectModel.IsProofing = false;
            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel);
            IenumSiteNavigationObjectModel = lstSiteNavigationObjectModel;

            SiteNavigationSearchDetail objSiteNavigationSearchDetail = new SiteNavigationSearchDetail();
            objSiteNavigationSearchDetail.SiteNavigationId = 1;
            objSiteNavigationSearchDetail.PageId = 7;
            objSiteNavigationSearchDetail.Name = "rrr7";
            objSiteNavigationSearchDetail.IsProofing = true;

            SiteNavigationSortDetail objSiteNavigationSortDetail = new SiteNavigationSortDetail();
            objSiteNavigationSortDetail.Column = SiteNavigationSortColumn.NavigationKey;

            //Act

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteNavigationSearchDetail>(), It.IsAny<SiteNavigationSortDetail>()))
                .Returns(CreateListSiteNavigation);

            SiteNavigationController objSiteNavigationController =
              new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetAllSiteNavigationDetails("Test", "4", "Proofing");

            //Verify and Assert
            //templatePageCacheFactory.VerifyAll();
            mockSiteNavigationFactoryCache.VerifyAll();
            ValidateEmptyData<SiteNavigationViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllSiteNavigation_Details_Action_Calls_SiteNavigationController_Empty
        /// <summary>
        /// GetAllSiteNavigation_Details_Action_Calls_SiteNavigationController_Empty
        /// </summary>
        [TestMethod]
        public void GetAllSiteNavigation_Details_Action_Calls_SiteNavigationController_Empty()
        {
            //Arrange

            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            IEnumerable<SiteNavigationObjectModel> IenumSiteNavigationObjectModel = lstSiteNavigationObjectModel;

            List<SiteNavigationViewModel> lstSiteNavigationViewModel = new List<SiteNavigationViewModel>();
            IEnumerable<SiteNavigationViewModel> IenumSiteNavigationViewModel = lstSiteNavigationViewModel;

            //Act

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<SiteNavigationSearchDetail>(), It.IsAny<SiteNavigationSortDetail>()))
                .Returns(IenumSiteNavigationObjectModel);

            SiteNavigationController objSiteNavigationController =
              new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.GetAllSiteNavigationDetails("", "", "");

            //Verify and Assert
            ValidateEmptyData<SiteNavigationViewModel>(result);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadDefaultNavigationXML_Returns_JsonResult
        /// <summary>
        /// LoadDefaultNavigationXML_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void LoadDefaultNavigationXMLForNavigationKey_Returns_JsonResult()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
            .Returns(CreateSiteList());
            mocktemplateNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateNavigationSearchDetail>()))
                .Returns(CreateTemplateNavigationList());
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.LoadDefaultNavigationXMLForNavigationKey("test");
            Assert.AreEqual(result.Data, null);
            //Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mocktemplateNavigationCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadDefaultNavigationXMLForNavigationKey_Returns_JsonResult_Empty
        /// <summary>
        /// LoadDefaultNavigationXML_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void LoadDefaultNavigationXMLForNavigationKey_Returns_JsonResult_Empty()
        {
            //Arrange
            List<TemplateNavigationObjectModel> lstTemplateNavigationObjectModel = new List<TemplateNavigationObjectModel>();
            IEnumerable<TemplateNavigationObjectModel> IenumTemplateNavigationObjectModel = lstTemplateNavigationObjectModel;

            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
            .Returns(CreateSiteList());

            mocktemplateNavigationCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateNavigationSearchDetail>()))
                .Returns(IenumTemplateNavigationObjectModel);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.LoadDefaultNavigationXMLForNavigationKey("Test");

            //Verify and Assert
            Assert.AreEqual(result.Data, null);
            mockSiteFactoryCache.VerifyAll();
            mocktemplateNavigationCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckSiteNavigationAlreadyExists_Returns_JsonResult
        [TestMethod]
        public void CheckSiteNavigationAlreadyExists_Returns_JsonResult()
        {
            //Arrange
            SiteNavigationObjectModel obj = new SiteNavigationObjectModel();
            obj.SiteId = 1;

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(CreateListSiteNavigation());

            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.CheckSiteNavigationAlreadyExists("Test", 1);

            //Verify and Assert
            Assert.AreEqual(result.Data, true);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckSiteNavigationAlreadyExists_Returns_JsonResult_Empty
        /// <summary>
        /// CheckSiteNavigationAlreadyExists_Returns_JsonResult_Empty
        /// </summary>
        [TestMethod]
        public void CheckSiteNavigationAlreadyExists_Returns_JsonResult_Empty()
        {
            //Arrange
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            IEnumerable<SiteNavigationObjectModel> enumSiteNavigationObjectModel = lstSiteNavigationObjectModel;
            SiteNavigationObjectModel obj = new SiteNavigationObjectModel();
            obj.SiteId = 0;

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(enumSiteNavigationObjectModel);

            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.CheckSiteNavigationAlreadyExists("Test", 1);

            //Verify and Assert
            Assert.AreEqual(result.Data, false);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckSiteNavigationAlreadyExists_Returns_JsonResult_Else
        [TestMethod]
        public void CheckSiteNavigationAlreadyExists_Returns_JsonResult_Else()
        {
            //Arrange
            SiteNavigationObjectModel obj = new SiteNavigationObjectModel();
            obj.SiteId = 1;

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(CreateListSiteNavigation());

            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.CheckSiteNavigationAlreadyExists("", 0);

            //Verify and Assert
            Assert.AreEqual(result.Data, false);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckSiteNavigationAlreadyExists_Returns_JsonResult_NavigationKey
        [TestMethod]
        public void CheckSiteNavigationAlreadyExists_Returns_JsonResult_NavigationKey()
        {
            //Arrange
            IEnumerable<SiteNavigationObjectModel> IenumSitenavigationObjectModel = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            SiteNavigationObjectModel obj = new SiteNavigationObjectModel();
            obj.SiteId = 1;
            obj.NavigationKey = "Test_001";
            lstSiteNavigationObjectModel.Add(obj);
            IenumSitenavigationObjectModel = lstSiteNavigationObjectModel;

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(IenumSitenavigationObjectModel);

            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.CheckSiteNavigationAlreadyExists("Test", 1);

            //Verify and Assert
            Assert.AreEqual(result.Data, false);
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadPagesForNavigationKey_Returns_JsonResults
        ///<summary>
        ///LoadPagesForNavigationKey_Returns_JsonResults
        ///</summary>
        [TestMethod]
        public void LoadPagesForNavigationKey_Returns_JsonResults()
        {

            //Arrange
            IEnumerable<TemplatePageObjectModel> IenumPageObjectModel = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTeplateObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageDescription = "Test";
            objTemplatePageObjectModel.PageID = 1;
            lstTeplateObjectModel.Add(objTemplatePageObjectModel);
            IenumPageObjectModel = lstTeplateObjectModel;

            IEnumerable<SiteNavigationObjectModel> IenumSitenavigationObjectModel = Enumerable.Empty<SiteNavigationObjectModel>();
            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            SiteNavigationObjectModel objSiteNavigationObjectModel = new SiteNavigationObjectModel();
            objSiteNavigationObjectModel.PageId = 1;
            objSiteNavigationObjectModel.PageDescription = "Test";
            objSiteNavigationObjectModel.PageName = "Test";
            objSiteNavigationObjectModel.NavigationKey = "Test";

            lstSiteNavigationObjectModel.Add(objSiteNavigationObjectModel);
            IenumSitenavigationObjectModel = lstSiteNavigationObjectModel;
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                  .Returns(CreateSiteList);
            mocktemplatePageCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());
            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>())).Returns(CreateListSiteNavigation());
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.LoadPagesForNavigationKey("Test");

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = null });
            lstexpected.Add(new DisplayValuePair() { Display = "Test", Value = "2" });
            ValidateDisplayValuePair(lstexpected, result);
            
            mocktemplatePageCacheFactory.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }
        #endregion

        #region LoadPagesForNavigationKey_Returns_JsonResults_Empty
        ///<summary>
        ///LoadPagesForNavigationKey_Returns_JsonResults_Empty
        ///</summary>
        [TestMethod]
        public void LoadPagesForNavigationKey_Returns_JsonResults_Empty()
        {

            //Arrange

            List<TemplatePageObjectModel> lstTeplateObjectModel = new List<TemplatePageObjectModel>();
            IEnumerable<TemplatePageObjectModel> IenumPageObjectModel = lstTeplateObjectModel;


            List<SiteNavigationObjectModel> lstSiteNavigationObjectModel = new List<SiteNavigationObjectModel>();
            IEnumerable<SiteNavigationObjectModel> IenumSitenavigationObjectModel = lstSiteNavigationObjectModel;

            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                  .Returns(CreateSiteList);

            mocktemplatePageCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>()))
                .Returns(IenumPageObjectModel);

            mockSiteNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteNavigationSearchDetail>()))
                .Returns(IenumSitenavigationObjectModel);
            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.LoadPagesForNavigationKey("");
            
            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "--Please select Page --", Value = null });
            ValidateDisplayValuePair(lstexpected, result);
         
            mockSiteFactoryCache.VerifyAll();
            mocktemplatePageCacheFactory.VerifyAll();
            mockSiteNavigationFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }
        #endregion

        #region ValidateXml_Returns_JsonResult
        /// <summary>
        /// ValidateXml_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void ValidateXml_Returns_JsonResult()
        {
            //Arrange

            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.ValidateXml(string.Empty);

            //Verify and Assert
            ValidateEmptyData<SiteNavigationViewModel>(result);
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

            //Act
            SiteNavigationController objSiteNavigationController =
         new SiteNavigationController(mockSiteNavigationFactoryCache.Object, mockUserFactoryCache.Object, mockSiteFactoryCache.Object, mocktemplatePageCacheFactory.Object, mocktemplateNavigationCacheFactory.Object);
            var result = objSiteNavigationController.ValidateXml("TEST");
            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion



    }
}
