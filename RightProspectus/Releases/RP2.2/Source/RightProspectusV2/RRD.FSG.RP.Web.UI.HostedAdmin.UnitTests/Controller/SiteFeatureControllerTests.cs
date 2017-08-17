using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{

    /// <summary>
    /// Class SiteFeatureControllerTests.
    /// </summary>
    [TestClass]
    public class SiteFeatureControllerTests : BaseTestController<SiteFeatureViewModel>
    {
        /// <summary>
        /// The mock site feature factory cache
        /// </summary>
        Mock<IFactoryCache<SiteFeatureFactory, SiteFeatureObjectModel, SiteFeatureKey>> mockSiteFeatureFactoryCache;
        /// <summary>
        /// The mock template feature factory cache
        /// </summary>
        Mock<IFactoryCache<TemplateFeatureFactory, TemplateFeatureObjectModel, TemplateFeatureKey>> mockTemplateFeatureFactoryCache;
        /// <summary>
        /// The mock user factory cache
        /// </summary>
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;
        /// <summary>
        /// The mocksite cache factory
        /// </summary>
        Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>> mocksiteCacheFactory;
        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            mockSiteFeatureFactoryCache = new Mock<IFactoryCache<SiteFeatureFactory, SiteFeatureObjectModel, SiteFeatureKey>>();
            mockTemplateFeatureFactoryCache = new Mock<IFactoryCache<TemplateFeatureFactory, TemplateFeatureObjectModel, TemplateFeatureKey>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mocksiteCacheFactory = new Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>>();
        }

        #region ReturnValues
        /// <summary>
        /// Creates the template feature list.
        /// </summary>
        /// <returns>IEnumerable&lt;TemplateFeatureObjectModel&gt;.</returns>
        private IEnumerable<TemplateFeatureObjectModel> CreateTemplateFeatureList()
        {
            IEnumerable<TemplateFeatureObjectModel> IEnumTemplateFeature = Enumerable.Empty<TemplateFeatureObjectModel>();
            List<TemplateFeatureObjectModel> lstTemplateFeature = new List<TemplateFeatureObjectModel>();
            TemplateFeatureObjectModel objTemplateFeature = new TemplateFeatureObjectModel();
            objTemplateFeature.FeatureKey = "100";
            lstTemplateFeature.Add(objTemplateFeature);
            IEnumTemplateFeature = lstTemplateFeature;
            return IEnumTemplateFeature;
        }
        /// <summary>
        /// Creates the template feature list_null.
        /// </summary>
        /// <returns>IEnumerable&lt;TemplateFeatureObjectModel&gt;.</returns>
        private IEnumerable<TemplateFeatureObjectModel> CreateTemplateFeatureList_null()
        {
            IEnumerable<TemplateFeatureObjectModel> IEnumTemplateFeature = Enumerable.Empty<TemplateFeatureObjectModel>();
            List<TemplateFeatureObjectModel> lstTemplateFeature = new List<TemplateFeatureObjectModel>();
            TemplateFeatureObjectModel objTemplateFeature = new TemplateFeatureObjectModel();
            objTemplateFeature = null;
            lstTemplateFeature.Add(objTemplateFeature);
            IEnumTemplateFeature = lstTemplateFeature;
            return IEnumTemplateFeature;
        }


        /// <summary>
        /// Creates the site feature list.
        /// </summary>
        /// <returns>IEnumerable&lt;SiteFeatureObjectModel&gt;.</returns>
        private IEnumerable<SiteFeatureObjectModel> CreateSiteFeatureList()
        {
            IEnumerable<SiteFeatureObjectModel> IEnumSiteFeature = Enumerable.Empty<SiteFeatureObjectModel>();
            List<SiteFeatureObjectModel> lstSiteFeature = new List<SiteFeatureObjectModel>();
            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 1;
            objSiteFeature.SiteId = 1;
            lstSiteFeature.Add(objSiteFeature);
            IEnumSiteFeature = lstSiteFeature;
            return IEnumSiteFeature;
        }
        /// <summary>
        /// Creates the site feature list_null.
        /// </summary>
        /// <returns>IEnumerable&lt;SiteFeatureObjectModel&gt;.</returns>
        private IEnumerable<SiteFeatureObjectModel> CreateSiteFeatureList_null()
        {
            IEnumerable<SiteFeatureObjectModel> IEnumSiteFeature = Enumerable.Empty<SiteFeatureObjectModel>();
            List<SiteFeatureObjectModel> lstSiteFeature = new List<SiteFeatureObjectModel>();
            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();

            lstSiteFeature.Add(objSiteFeature);
            IEnumSiteFeature = lstSiteFeature;
            return IEnumSiteFeature;
        }

        /// <summary>
        /// Creates the user list.
        /// </summary>
        /// <returns>IEnumerable&lt;UserObjectModel&gt;.</returns>
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

        /// <summary>
        /// Creates the site list.
        /// </summary>
        /// <returns>IEnumerable&lt;SiteObjectModel&gt;.</returns>
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
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.List();
            var result1 = result as ViewResult;

            // Verify and Assert
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod()
        {
            //Arrange
            EditSiteFeatureViewModel objEditSiteFeatureViewModel = new EditSiteFeatureViewModel()
            {
                SiteId = 1,
                SelectedFeatureKey = "_New",
                FeatureMode = 1,
                ModifiedBy = 1
            };
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "Test",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };

            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 1;
            objSiteFeature.SiteId = 1;

            mockTemplateFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateFeatureSearchDetail>()))
                .Returns(CreateTemplateFeatureList);
            mocksiteCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList);
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
                .Returns(objSiteFeature);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);
            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Edit(1, "New");

            // Verify and Assert
            List<DisplayValuePair> lstFeature = new List<DisplayValuePair>();
            lstFeature.Add(new DisplayValuePair() { Display = "--Please select Site Feature Key--", Value = "-1" });
            lstFeature.Add(new DisplayValuePair() { Display = "100", Value = "100" });

            EditSiteFeatureViewModel objExpected = new EditSiteFeatureViewModel()
            {
                FeatureKeys = lstFeature,
                FeatureMode = 1,
                FeatureModes = null,
                ModifiedBy = null,
                ModifiedByName = "Test_UserName",
                SelectedFeatureKey = "_New",
                SelectedFeatureMode = null,
                SiteId = 1,
                UTCLastModifiedDate = null
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteFeatureViewModel;
            ValidateViewModelData<EditSiteFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteFeatureViewModel));

            mockTemplateFeatureFactoryCache.VerifyAll();
            mockSiteFeatureFactoryCache.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_WithNewKey
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_WithNewKey
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_WithNewKey()
        {
            //Arrange
            EditSiteFeatureViewModel objEditSiteFeatureViewModel = new EditSiteFeatureViewModel()
            {
                SiteId = 1,
                SelectedFeatureKey = "_New",
                FeatureMode = 1,
                ModifiedBy = 1
            };
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "Test",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };

            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 1;
            objSiteFeature.SiteId = 1;

            mockTemplateFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateFeatureSearchDetail>()))
                .Returns(CreateTemplateFeatureList);
            mocksiteCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList);
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
                .Returns(objSiteFeature);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);
            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Edit(1, "_New");

            // Verify and Assert
            List<DisplayValuePair> lstFeature = new List<DisplayValuePair>();
            lstFeature.Add(new DisplayValuePair() { Display = "--Please select Site Feature Key--", Value = "-1" });
            lstFeature.Add(new DisplayValuePair() { Display = "100", Value = "100" });

            EditSiteFeatureViewModel objExpected = new EditSiteFeatureViewModel()
            {
                FeatureKeys = lstFeature,
                FeatureMode = 1,
                FeatureModes = null,
                ModifiedBy = null,
                ModifiedByName = "Test_UserName",
                SelectedFeatureKey = "_New",
                SelectedFeatureMode = null,
                SiteId = 1,
                UTCLastModifiedDate = null
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteFeatureViewModel;
            ValidateViewModelData<EditSiteFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteFeatureViewModel));

            mockTemplateFeatureFactoryCache.VerifyAll();
            mockSiteFeatureFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_null
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_null
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_null()
        {
            //Arrange
            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            mocksiteCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList);
            mockTemplateFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateFeatureSearchDetail>()))
              .Returns(CreateTemplateFeatureList);
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
                .Returns(objSiteFeature);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);
            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Edit(1, "New");

            // Verify and Assert
            List<DisplayValuePair> lstFeature = new List<DisplayValuePair>();
            lstFeature.Add(new DisplayValuePair() { Display = "--Please select Site Feature Key--", Value = "-1" });
            lstFeature.Add(new DisplayValuePair() { Display = "100", Value = "100" });

            EditSiteFeatureViewModel objExpected = new EditSiteFeatureViewModel()
            {
                FeatureKeys = lstFeature,
                FeatureMode = 0,
                FeatureModes = null,
                ModifiedBy = null,
                ModifiedByName = "Test_UserName",
                SelectedFeatureKey = null,
                SelectedFeatureMode = null,
                SiteId = 0,
                UTCLastModifiedDate = null
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteFeatureViewModel;
            ValidateViewModelData<EditSiteFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteFeatureViewModel));

            mocksiteCacheFactory.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            mockTemplateFeatureFactoryCache.VerifyAll();
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_withkey_Null
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_withkey_Null
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_withkey_GetMethod_Null()
        {
            //Arrange
            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 1;
            objSiteFeature.SiteId = 1;

            mocksiteCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList);
            mockTemplateFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateFeatureSearchDetail>()))
              .Returns(CreateTemplateFeatureList);
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
              .Returns(objSiteFeature);

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Edit(1, "_New");

            // Verify and Assert
            List<DisplayValuePair> lstFeature = new List<DisplayValuePair>();
            lstFeature.Add(new DisplayValuePair() { Display = "--Please select Site Feature Key--", Value = "-1" });
            lstFeature.Add(new DisplayValuePair() { Display = "100", Value = "100" });

            EditSiteFeatureViewModel objExpected = new EditSiteFeatureViewModel()
            {
                FeatureKeys = lstFeature,
                FeatureMode = 1,
                FeatureModes = null,
                ModifiedBy = null,
                ModifiedByName = "",
                SelectedFeatureKey = "_New",
                SelectedFeatureMode = null,
                SiteId = 1,
                UTCLastModifiedDate = null
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteFeatureViewModel;
            ValidateViewModelData<EditSiteFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteFeatureViewModel));

            mockTemplateFeatureFactoryCache.VerifyAll();
            mockSiteFeatureFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
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
            //Arrange
            EditSiteFeatureViewModel objEditSiteFeatureViewModel = new EditSiteFeatureViewModel();
            objEditSiteFeatureViewModel.SelectedFeatureKey = "1";
            objEditSiteFeatureViewModel.FeatureModes = "1";
            objEditSiteFeatureViewModel.SelectedFeatureMode = "1";
            objEditSiteFeatureViewModel.SiteId = 1;

            mockSiteFeatureFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteFeatureObjectModel>(), It.IsAny<int>()));
            mockTemplateFeatureFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplateFeatureList());

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Edit(objEditSiteFeatureViewModel, "1,2");

            // Verify and Assert
            List<DisplayValuePair> lstFeature = new List<DisplayValuePair>();
            lstFeature.Add(new DisplayValuePair() { Display = "--Please select Site Feature Key--", Value = "-1" });
            lstFeature.Add(new DisplayValuePair() { Display = "100", Value = "100" });
            EditSiteFeatureViewModel objExpected = new EditSiteFeatureViewModel()
            {
                FeatureKeys = lstFeature,
                FeatureMode = null,
                FeatureModes = "1",
                ModifiedBy = null,
                ModifiedByName = null,
                SelectedFeatureKey = "1",
                SelectedFeatureMode = "1",
                SiteId = 1,
                UTCLastModifiedDate = null
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteFeatureViewModel;
            ValidateViewModelData<EditSiteFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteFeatureViewModel));
            mockSiteFeatureFactoryCache.VerifyAll();
            mockTemplateFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_PostMethod_calculate
        /// <summary>
        /// Edit_Returns_ActionResult_PostMethod_calculate
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_PostMethod_calculate()
        {
            //Arrange
            EditSiteFeatureViewModel objEditSiteFeatureViewModel = new EditSiteFeatureViewModel();
            objEditSiteFeatureViewModel.SelectedFeatureKey = "0";
            objEditSiteFeatureViewModel.FeatureModes = "0";
            objEditSiteFeatureViewModel.SelectedFeatureMode = "0";

            objEditSiteFeatureViewModel.SiteId = 1;

            mockSiteFeatureFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteFeatureObjectModel>(), It.IsAny<int>()));
            mockTemplateFeatureFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplateFeatureList());

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Edit(objEditSiteFeatureViewModel, "1");

            // Verify and Assert
            List<DisplayValuePair> lstFeature = new List<DisplayValuePair>();
            lstFeature.Add(new DisplayValuePair() { Display = "--Please select Site Feature Key--", Value = "-1" });
            lstFeature.Add(new DisplayValuePair() { Display = "100", Value = "100" });
            EditSiteFeatureViewModel objExpected = new EditSiteFeatureViewModel()
            {
                FeatureKeys = lstFeature,
                FeatureMode = null,
                FeatureModes = "0",
                ModifiedBy = null,
                ModifiedByName = null,
                SelectedFeatureKey = "0",
                SelectedFeatureMode = "0",
                SiteId = 1,
                UTCLastModifiedDate = null
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteFeatureViewModel;
            ValidateViewModelData<EditSiteFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteFeatureViewModel));

            mockSiteFeatureFactoryCache.VerifyAll();
            mockTemplateFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_PostMethod_null
        /// <summary>
        /// Edit_Returns_ActionResult_PostMethod_null
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_PostMethod_null()
        {
            //Arrange
            EditSiteFeatureViewModel objEditSiteFeatureViewModel = new EditSiteFeatureViewModel();
            objEditSiteFeatureViewModel.SelectedFeatureKey = "1";
            objEditSiteFeatureViewModel.FeatureModes = "1";
            objEditSiteFeatureViewModel.SelectedFeatureMode = "1";
            objEditSiteFeatureViewModel.SiteId = 1;

            mockSiteFeatureFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteFeatureObjectModel>(), It.IsAny<int>()));
            mockTemplateFeatureFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplateFeatureList_null());

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                  mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Edit(objEditSiteFeatureViewModel, "1,2");

            // Verify and Assert

            List<DisplayValuePair> lstFeature = new List<DisplayValuePair>();
            lstFeature.Add(new DisplayValuePair() { Display = "--Please select Site Feature Key--", Value = "-1" });
            // lstFeature.Add(new DisplayValuePair() { Display = "100", Value = "100" });
            EditSiteFeatureViewModel objExpected = new EditSiteFeatureViewModel()
            {
                FeatureKeys = lstFeature,
                FeatureMode = null,
                FeatureModes = "1",
                ModifiedBy = null,
                ModifiedByName = null,
                SelectedFeatureKey = "1",
                SelectedFeatureMode = "1",
                SiteId = 1,
                UTCLastModifiedDate = null
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteFeatureViewModel;
            ValidateViewModelData<EditSiteFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteFeatureViewModel));

            mockSiteFeatureFactoryCache.VerifyAll();
            mockTemplateFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Edit_Returns_ActionResult_PostMethod_CatchBlock
        /// <summary>
        /// Edit_Returns_ActionResult_PostMethod_CatchBlock
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_PostMethod_CatchBlock()
        {
            //Arrange
            EditSiteFeatureViewModel objEditSiteFeatureViewModel = new EditSiteFeatureViewModel();
            objEditSiteFeatureViewModel.SelectedFeatureKey = "1";

            mockSiteFeatureFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteFeatureObjectModel>(), It.IsAny<int>())).Throws(new Exception());
            //  mockTemplateFeatureFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplateFeatureList());

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                  mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Edit(objEditSiteFeatureViewModel, "1,2");

            // Verify and Assert
            List<DisplayValuePair> lstFeature = new List<DisplayValuePair>();
            //lstFeature.Add(new DisplayValuePair() { Display = "--Please select Site Feature Key--", Value = "-1" });
            //lstFeature.Add(new DisplayValuePair() { Display = "100", Value = "100" });
            EditSiteFeatureViewModel objExpected = new EditSiteFeatureViewModel()
            {
                FeatureKeys = lstFeature,
                FeatureMode = null,
                FeatureModes = null,
                ModifiedBy = null,
                ModifiedByName = null,
                SelectedFeatureKey = "1",
                SelectedFeatureMode = null,
                SiteId = 0,
                UTCLastModifiedDate = null
            };
            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteFeatureViewModel;
            ValidateViewModelData<EditSiteFeatureViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteFeatureViewModel));

            mockSiteFeatureFactoryCache.VerifyAll();
            mockTemplateFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
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

            mockSiteFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteFeatureSearchDetail>(), It.IsAny<SiteFeatureSortDetail>())).Returns(CreateSiteFeatureList());

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Search(null);

            // Verify and Assert
            mockSiteFeatureFactoryCache.VerifyAll();
            ValidateEmptyData<SiteFeatureViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region Search_Returns_JsonResult_notnull
        /// <summary>
        /// Search_Returns_JsonResult_notnull
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_notnull()
        {
            //Arrange
            // mockSiteFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteFeatureSearchDetail>())).Returns(CreateSiteFeatureList());
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteFeatureSearchDetail>(), It.IsAny<SiteFeatureSortDetail>())).Returns(CreateSiteFeatureList());

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.Search("Test");

            // Verify and Assert
            ValidateEmptyData<SiteFeatureViewModel>(result);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTemplateFeatureKeys_Returns_JsonResult
        /// <summary>
        /// GetTemplateFeatureKeys_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetTemplateFeatureKeys_Returns_JsonResult()
        {
            //Arrange
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteFeatureSearchDetail>())).Returns(CreateSiteFeatureList());

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetTemplateFeatureKeys();

            // Verify and Assert
            mockSiteFeatureFactoryCache.VerifyAll();
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "_New", Value = "_New" });
            ValidateDisplayValuePair(lstexpected, result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTemplateFeatureKeys_Returns_JsonResult_null
        /// <summary>
        /// GetTemplateFeatureKeys_Returns_JsonResult_null
        /// </summary>
        [TestMethod]
        public void GetTemplateFeatureKeys_Returns_JsonResult_null()
        {
            //Arrange
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteFeatureSearchDetail>())).Returns(CreateSiteFeatureList_null());

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetTemplateFeatureKeys();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = null });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeDisabled
        /// <summary>
        /// GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeDisabled
        /// </summary>
        [TestMethod]
        public void GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeDisabled()
        {
            //Arrange
            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 1;
            objSiteFeature.SiteId = 1;
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
                .Returns(objSiteFeature);

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModesByKey("XBRL");

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = "1" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeEnabled
        /// <summary>
        /// GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeEnabled
        /// </summary>
        [TestMethod]
        public void GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeEnabled()
        {
            //Arrange

            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 1;
            objSiteFeature.SiteId = 1;
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
                .Returns(objSiteFeature);


            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModesByKey("XBRL");

            // Verify and Assert
            mockSiteFeatureFactoryCache.VerifyAll();
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = "1" });
            ValidateDisplayValuePair(lstexpected, result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeShowXBRLInNewTab
        /// <summary>
        /// GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeShowXBRLInNewTab
        /// </summary>
        [TestMethod]
        public void GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeShowXBRLInNewTab()
        {
            //Arrange

            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 4;

            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
                .Returns(objSiteFeature);

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModesByKey("XBRL");

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = "4" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeShowXBRLInTabbedView
        /// <summary>
        /// GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeShowXBRLInTabbedView
        /// </summary>
        [TestMethod]
        public void GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeShowXBRLInTabbedView()
        {
            //Arrange
            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 8;


            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>())).Returns(objSiteFeature);

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModesByKey("XBRL");

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = "8" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeShowXBRLInLandingPage
        /// <summary>
        /// GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeShowXBRLInLandingPage
        /// </summary>
        [TestMethod]
        public void GetFeatureModesByKey_Returns_JsonResult_WithXBRL_ModeShowXBRLInLandingPage()
        {
            //Arrange

            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 16;

            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
              .Returns(objSiteFeature);

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModesByKey("XBRL");

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = "16" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModesByKey_Returns_JsonResult_WithRequestMaterial_ModeDisabled
        /// <summary>
        /// GetFeatureModesByKey_Returns_JsonResult_WithRequestMaterial_ModeDisabled
        /// </summary>
        [TestMethod]
        public void GetFeatureModesByKey_Returns_JsonResult_WithRequestMaterial_ModeDisabled()
        {
            //Arrange
            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();

            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
              .Returns(objSiteFeature);

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModesByKey("RequestMaterial");

            // Verify and Assert
            mockSiteFeatureFactoryCache.VerifyAll();
            ValidateEmptyData<SiteFeatureViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModesByKey_Returns_JsonResult_WithRequestMaterial_ModeEnabled
        /// <summary>
        /// GetFeatureModesByKey_Returns_JsonResult_WithRequestMaterial_ModeEnabled
        /// </summary>
        [TestMethod]
        public void GetFeatureModesByKey_Returns_JsonResult_WithRequestMaterial_ModeEnabled()
        {
            //Arrange

            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 2;

            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
            .Returns(objSiteFeature);

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                   mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModesByKey("RequestMaterial");

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = null, Value = "2" });
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModesByKey_Returns_JsonResult_Withothercase_ModeDisabled
        /// <summary>
        /// #region GetFeatureModesByKey_Returns_JsonResult_Withothercase_ModeDisabled
        /// </summary>
        [TestMethod]
        public void GetFeatureModesByKey_Returns_JsonResult_Withothercase_ModeDisabled()
        {
            //Arrange
            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            
            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
              .Returns(objSiteFeature);

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                  mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModesByKey("OtherCase");

            // Verify and Assert
            ValidateEmptyData<SiteFeatureViewModel>(result);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModesByKey_Returns_JsonResult_WithOtherCase_ModeEnabled
        /// <summary>
        /// GetFeatureModesByKey_Returns_JsonResult_WithOtherCase_ModeEnabled
        /// </summary>
        [TestMethod]
        public void GetFeatureModesByKey_Returns_JsonResult_WithOtherCase_ModeEnabled()
        {
            //Arrange
        
            SiteFeatureObjectModel objSiteFeature = new SiteFeatureObjectModel();
            objSiteFeature.SiteKey = "_New";
            objSiteFeature.FeatureMode = 2;


            mockSiteFeatureFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteFeatureKey>()))
               .Returns(objSiteFeature);

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                  mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModesByKey("OtherCase");

            // Verify and Assert
            ValidateEmptyData<SiteFeatureViewModel>(result);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region DisableSiteFeature_Returns_JsonResult
        /// <summary>
        /// DisableSiteFeature_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DisableSiteFeature_Returns_JsonResult()
        {
            //Arrange
            mockSiteFeatureFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<SiteFeatureObjectModel>(), It.IsAny<int>()));

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.DisableSiteFeature("Test");

            // Verify and Assert
            Assert.AreEqual(string.Empty, result.Data);
            mockSiteFeatureFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModes_Returns_JsonResult_XBRL
        /// <summary>
        /// GetFeatureModes_Returns_JsonResult_XBRL
        /// </summary>
        [TestMethod]
        public void GetFeatureModes_Returns_JsonResult_XBRL()
        {
            //Arrange

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                 mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModes("XBRL");

            // Verify and Assert
            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "5";
            expected.Add(multiSelectDropDownViewModel);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModes_Returns_JsonResult_RequestMaterial
        /// <summary>
        /// GetFeatureModes_Returns_JsonResult_RequestMaterial
        /// </summary>
        [TestMethod]
        public void GetFeatureModes_Returns_JsonResult_RequestMaterial()
        {
            //Arrange

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModes("RequestMaterial");

            // Verify and Assert
            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "2";
            expected.Add(multiSelectDropDownViewModel);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetFeatureModes_Returns_JsonResult_otherCase
        /// <summary>
        /// GetFeatureModes_Returns_JsonResult_otherCase
        /// </summary>
        [TestMethod]
        public void GetFeatureModes_Returns_JsonResult_otherCase()
        {
            //Arrange

            //Act
            SiteFeatureController objSiteFeatureController = new SiteFeatureController(mockSiteFeatureFactoryCache.Object,
                mockTemplateFeatureFactoryCache.Object, mockUserFactoryCache.Object, mocksiteCacheFactory.Object);
            var result = objSiteFeatureController.GetFeatureModes("OtherCases");

            // Verify and Assert
            List<MultiSelectDropDownViewModel> expected = new List<MultiSelectDropDownViewModel>();
            MultiSelectDropDownViewModel multiSelectDropDownViewModel = new MultiSelectDropDownViewModel();
            multiSelectDropDownViewModel.value = "0";
            expected.Add(multiSelectDropDownViewModel);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

    }
}
