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
using System.Web.Mvc;
using RRD.DSA.Core;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for SiteTextController class
    /// </summary>
    [TestClass]
    public class SiteTextControllerTests : BaseTestController<SiteTextViewModel>
    {
        Mock<IFactoryCache<SiteTextFactory, SiteTextObjectModel, SiteTextKey>> mockSiteTextFactoryCache;
        Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>> mockSiteFactoryCache;
        Mock<IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey>> mockTemplateTextFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;

        [TestInitialize]
        public void TestInitialize()
        {
            mockSiteTextFactoryCache = new Mock<IFactoryCache<SiteTextFactory, SiteTextObjectModel, SiteTextKey>>();
            mockSiteFactoryCache = new Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>>();
            mockTemplateTextFactoryCache = new Mock<IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
        }

        #region ReturnValues
        private IEnumerable<SiteTextObjectModel> CreateSiteTextList()
        {
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();
            List<SiteTextObjectModel> lstSiteText = new List<SiteTextObjectModel>();
            SiteTextObjectModel objSiteText = new SiteTextObjectModel();
            objSiteText.ResourceKey = "1";
            objSiteText.IsProofing = false;
            objSiteText.SiteID = 1;
            objSiteText.SiteTextID = 1;
            objSiteText.Version = 1;
            objSiteText.Text = "Test";
            objSiteText.IsProofingAvailableForSiteTextId = false;
            lstSiteText.Add(objSiteText);
            IenumSiteText = lstSiteText;
            return IenumSiteText;
        }
        private IEnumerable<SiteTextObjectModel> CreateSiteTextList_With_Change_Text()
        {
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();
            List<SiteTextObjectModel> lstSiteText = new List<SiteTextObjectModel>();
            SiteTextObjectModel objSiteText = new SiteTextObjectModel();
            objSiteText.ResourceKey = "cssTest";
            objSiteText.IsProofing = false;
            objSiteText.SiteID = 1;
            objSiteText.SiteTextID = 1;
            objSiteText.Version = 1;
            objSiteText.Text = "Test1";
            objSiteText.IsProofingAvailableForSiteTextId = false;
            lstSiteText.Add(objSiteText);
            IenumSiteText = lstSiteText;
            return IenumSiteText;
        }

        private IEnumerable<SiteTextObjectModel> CreateSiteTextList_With_Change_Text_With_ResourceKey()
        {
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();
            List<SiteTextObjectModel> lstSiteText = new List<SiteTextObjectModel>();
            SiteTextObjectModel objSiteText = new SiteTextObjectModel();
            objSiteText.ResourceKey = "1";
            objSiteText.IsProofing = false;
            objSiteText.SiteID = 1;
            objSiteText.SiteTextID = 1;
            objSiteText.Version = 1;
            objSiteText.Text = "Test1";
            objSiteText.IsProofingAvailableForSiteTextId = false;
            lstSiteText.Add(objSiteText);
            IenumSiteText = lstSiteText;
            return IenumSiteText;
        }
        private IEnumerable<SiteTextObjectModel> CreateSiteTextList_With_Change_Text_With_ResourceKey_css()
        {
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();
            List<SiteTextObjectModel> lstSiteText = new List<SiteTextObjectModel>();
            SiteTextObjectModel objSiteText = new SiteTextObjectModel();
            objSiteText.ResourceKey = "css";
            objSiteText.IsProofing = false;
            objSiteText.SiteID = 1;
            objSiteText.SiteTextID = 1;
            objSiteText.Version = 1;
            objSiteText.Text = "Test1";
            objSiteText.IsProofingAvailableForSiteTextId = false;
            lstSiteText.Add(objSiteText);
            IenumSiteText = lstSiteText;
            return IenumSiteText;
        }



        private IEnumerable<SiteTextObjectModel> CreateSiteTextList_UniqueSiteTextId()
        {
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();
            List<SiteTextObjectModel> lstSiteText = new List<SiteTextObjectModel>();
            SiteTextObjectModel objSiteText = new SiteTextObjectModel();
            objSiteText.ResourceKey = "1";
            objSiteText.IsProofing = false;
            objSiteText.SiteID = 1;
            objSiteText.SiteTextID = 4;
            objSiteText.Version = 1;
            objSiteText.Text = "Test";
            objSiteText.IsProofingAvailableForSiteTextId = false;
            lstSiteText.Add(objSiteText);
            IenumSiteText = lstSiteText;
            return IenumSiteText;
        }

        private IEnumerable<SiteTextObjectModel> CreateSiteTextList_IsProofingTrue()
        {
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();
            List<SiteTextObjectModel> lstSiteText = new List<SiteTextObjectModel>();
            SiteTextObjectModel objSiteText = new SiteTextObjectModel();
            objSiteText.ResourceKey = "1";
            objSiteText.IsProofing = true;
            objSiteText.SiteID = 1;
            objSiteText.SiteTextID = 1;
            objSiteText.Version = 1;
            objSiteText.Text = "Test";
            objSiteText.IsProofingAvailableForSiteTextId = false;
            lstSiteText.Add(objSiteText);
            IenumSiteText = lstSiteText;
            return IenumSiteText;
        }

        private IEnumerable<SiteTextObjectModel> CreateSiteTextList_resourceKey_With_CSS()
        {
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();
            List<SiteTextObjectModel> lstSiteText = new List<SiteTextObjectModel>();
            SiteTextObjectModel objSiteText = new SiteTextObjectModel();
            objSiteText.ResourceKey = "css";
            objSiteText.IsProofing = false;
            objSiteText.SiteID = 1;
            objSiteText.SiteTextID = 1;
            objSiteText.Version = 1;
            objSiteText.Text = "Test";
            objSiteText.IsProofingAvailableForSiteTextId = false;
            lstSiteText.Add(objSiteText);
            IenumSiteText = lstSiteText;
            return IenumSiteText;
        }

        private IEnumerable<SiteTextObjectModel> CreateSiteTextList_Contains_P()
        {
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();
            List<SiteTextObjectModel> lstSiteText = new List<SiteTextObjectModel>();
            SiteTextObjectModel objSiteText = new SiteTextObjectModel();
            objSiteText.ResourceKey = "p";
            objSiteText.IsProofing = false;
            objSiteText.SiteID = 1;
            objSiteText.SiteTextID = 1;
            objSiteText.Version = 1;
            objSiteText.Text = "Test";
            objSiteText.IsProofingAvailableForSiteTextId = false;
            lstSiteText.Add(objSiteText);
            IenumSiteText = lstSiteText;
            return IenumSiteText;
        }

        private IEnumerable<SiteTextObjectModel> CreateSiteTextListAsProofingTrue()
        {
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();
            List<SiteTextObjectModel> lstSiteText = new List<SiteTextObjectModel>();
            SiteTextObjectModel objSiteText = new SiteTextObjectModel();
            objSiteText.ResourceKey = "p";
            objSiteText.IsProofing = true;
            objSiteText.SiteID = 1;
            objSiteText.SiteTextID = 1;
            objSiteText.Version = 1;
            objSiteText.Text = "Test";
            objSiteText.IsProofingAvailableForSiteTextId = false;
            lstSiteText.Add(objSiteText);
            IenumSiteText = lstSiteText;
            return IenumSiteText;
        }


        private IEnumerable<TemplateTextObjectModel> CreateTemplateTextList()
        {
            IEnumerable<TemplateTextObjectModel> IEnumTemplateText = Enumerable.Empty<TemplateTextObjectModel>();
            List<TemplateTextObjectModel> lstTemplateText = new List<TemplateTextObjectModel>();
            TemplateTextObjectModel objTemplateText = new TemplateTextObjectModel();
            objTemplateText.ResourceKey = "1";
            objTemplateText.IsHTML = true;
            objTemplateText.DefaultText = "Test.Test#Test{Test;Test}Test:Test";
            lstTemplateText.Add(objTemplateText);
            IEnumTemplateText = lstTemplateText;
            return IEnumTemplateText;
        }
        private IEnumerable<TemplateTextObjectModel> CreateTemplateTextList_IsHTML_False()
        {
            IEnumerable<TemplateTextObjectModel> IEnumTemplateText = Enumerable.Empty<TemplateTextObjectModel>();
            List<TemplateTextObjectModel> lstTemplateText = new List<TemplateTextObjectModel>();
            TemplateTextObjectModel objTemplateText = new TemplateTextObjectModel();
            objTemplateText.ResourceKey = "CssTest";
            objTemplateText.IsHTML = false;
            objTemplateText.DefaultText = "Test.Test#Test{Test;Test}Test:Test";
            lstTemplateText.Add(objTemplateText);
            IEnumTemplateText = lstTemplateText;
            return IEnumTemplateText;
        }

        private IEnumerable<TemplateTextObjectModel> CreateTemplateTextList_IsHTML_False_With_ResourceKey()
        {
            IEnumerable<TemplateTextObjectModel> IEnumTemplateText = Enumerable.Empty<TemplateTextObjectModel>();
            List<TemplateTextObjectModel> lstTemplateText = new List<TemplateTextObjectModel>();
            TemplateTextObjectModel objTemplateText = new TemplateTextObjectModel();
            objTemplateText.ResourceKey = "1";
            objTemplateText.IsHTML = false;
            objTemplateText.DefaultText = "Test.Test#Test{Test;Test}Test:Test";
            lstTemplateText.Add(objTemplateText);
            IEnumTemplateText = lstTemplateText;
            return IEnumTemplateText;
        }


        private IEnumerable<TemplateTextObjectModel> CreateTemplateTextList_Resourcekey_css()
        {
            IEnumerable<TemplateTextObjectModel> IEnumTemplateText = Enumerable.Empty<TemplateTextObjectModel>();
            List<TemplateTextObjectModel> lstTemplateText = new List<TemplateTextObjectModel>();
            TemplateTextObjectModel objTemplateText = new TemplateTextObjectModel();
            objTemplateText.ResourceKey = "Css";
            objTemplateText.IsHTML = false;
            objTemplateText.DefaultText = "Test.Test#Test{Test;Test}Test:Test";
            lstTemplateText.Add(objTemplateText);
            IEnumTemplateText = lstTemplateText;
            return IEnumTemplateText;
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
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.List();

            // Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            Assert.IsInstanceOfType(result, typeof(ActionResult));
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
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteTextSearchDetail>())).Returns(CreateSiteTextList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetResourceKeys();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "1", Value = "1" });
            ValidateDisplayValuePair(lstexpected, result);

            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetResourceKeys_Returns_JsonResult_ReturnEmptyDetails
        /// <summary>
        /// GetResourceKeys_Returns_JsonResult_ReturnEmptyDetails
        /// </summary>
        [TestMethod]
        public void GetResourceKeys_Returns_JsonResult_ReturnEmptyDetails()
        {
            //Arrange
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();

            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteTextSearchDetail>()))
                .Returns(IenumSiteText);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetResourceKeys();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetVersions_Returns_JsonResult_Returns_EmptyDetails
        /// <summary>
        /// GetVersions_Returns_JsonResult_Returns_EmptyDetails
        /// </summary>
        [TestMethod]
        public void GetVersions_Returns_JsonResult_Returns_EmptyDetails()
        {
            //Arrange
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();

            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteTextSearchDetail>()))
                .Returns(IenumSiteText);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetVersions();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
            mockSiteTextFactoryCache.VerifyAll();
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
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteTextSearchDetail>())).Returns(CreateSiteTextList());

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetVersions();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Production", Value = "Production" });
            ValidateDisplayValuePair(lstexpected, result);

            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetVersions_Returns_JsonResult_ProofingAsTrue
        /// <summary>
        /// GetVersions_Returns_JsonResult_ProofingAsTrue
        /// </summary>
        [TestMethod]
        public void GetVersions_Returns_JsonResult_ProofingAsTrue()
        {
            //Arrange


            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteTextSearchDetail>()))
                .Returns(CreateSiteTextListAsProofingTrue());

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.GetVersions();

            // Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Proofing", Value = "Proofing" });
            ValidateDisplayValuePair(lstexpected, result);

            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Search_Returns_JsonResult_ProductionVersion
        /// <summary>
        /// Search_Returns_JsonResult_ProductionVersion
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_ProductionVersion()
        {
            //Arrange
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteTextSearchDetail>())).Returns(CreateSiteTextList);
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>(), It.IsAny<SiteTextSortDetail>())).Returns(CreateSiteTextList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Search(null, "Production");

            // Verify and Assert
            List<SiteTextViewModel> expected = new List<SiteTextViewModel>();
            SiteTextViewModel ObjSiteTextViewModels = new SiteTextViewModel();
            ObjSiteTextViewModels.SiteTextID = 1;
            ObjSiteTextViewModels.SiteID = 1;
            ObjSiteTextViewModels.IsProofing = false;
            ObjSiteTextViewModels.IsProofingAvailableForSiteTextId = false;
            ObjSiteTextViewModels.ResourceKey = "1";
            ObjSiteTextViewModels.Text = "Test";
            ObjSiteTextViewModels.Version = "Production";
            ObjSiteTextViewModels.VersionID = 1;
            expected.Add(ObjSiteTextViewModels);
            ValidateEmptyData<SiteTextViewModel>(result);

            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Search_Returns_JsonResult_WithResourceKeyNOTNULL
        /// <summary>
        /// Search_Returns_JsonResult_WithResourceKeyNOTNULL
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_WithResourceKeyNOTNULL()
        {
            //Arrange
            //mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>())).Returns(CreateSiteTextList);
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>(), It.IsAny<SiteTextSortDetail>())).Returns(CreateSiteTextList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Search("1", "Production");

            // Verify and Assert
            List<SiteTextViewModel> expected = new List<SiteTextViewModel>();
            SiteTextViewModel ObjSiteTextViewModels = new SiteTextViewModel();
            ObjSiteTextViewModels.SiteTextID = 1;
            ObjSiteTextViewModels.SiteID = 1;
            ObjSiteTextViewModels.IsProofing = false;
            ObjSiteTextViewModels.IsProofingAvailableForSiteTextId = false;
            ObjSiteTextViewModels.ResourceKey = "1";
            ObjSiteTextViewModels.Text = "Test";
            ObjSiteTextViewModels.Version = "Production";
            ObjSiteTextViewModels.VersionID = 1;
            expected.Add(ObjSiteTextViewModels);
            ValidateEmptyData<SiteTextViewModel>(result);

            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Search_Returns_JsonResult_ForTotalRecords
        /// <summary>
        /// Search_Returns_JsonResult_ForTotalRecords
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_ForTotalRecords()
        {
            //Arrange
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();

            //mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>()))
            //    .Returns(IenumSiteText);
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>(), It.IsAny<SiteTextSortDetail>()))
                .Returns(CreateSiteTextList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Search("1", "Production");

            // Verify and Assert
            List<SiteTextViewModel> expected = new List<SiteTextViewModel>();
            SiteTextViewModel ObjSiteTextViewModels = new SiteTextViewModel();
            ObjSiteTextViewModels.SiteTextID = 1;
            ObjSiteTextViewModels.SiteID = 1;
            ObjSiteTextViewModels.IsProofing = false;
            ObjSiteTextViewModels.IsProofingAvailableForSiteTextId = false;
            ObjSiteTextViewModels.ResourceKey = "1";
            ObjSiteTextViewModels.Text = "Test";
            ObjSiteTextViewModels.Version = "Production";
            ObjSiteTextViewModels.VersionID = 1;
            expected.Add(ObjSiteTextViewModels);
            ValidateEmptyData<SiteTextViewModel>(result);

            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Search_Returns_JsonResult_ReturnsEmptyDetails_ForSiteTextDetails
        /// <summary>
        /// Search_Returns_JsonResult_ReturnsEmptyDetails_ForSiteTextDetails
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_ReturnsEmptyDetails_ForSiteTextDetails()
        {
            //Arrange
            IEnumerable<SiteTextObjectModel> IenumSiteText = Enumerable.Empty<SiteTextObjectModel>();

            //mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>()))
            //    .Returns(CreateSiteTextList());
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>(), It.IsAny<SiteTextSortDetail>()))
                .Returns(IenumSiteText);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Search("1", "Production");

            // Verify and Assert
            List<SiteTextViewModel> expected = new List<SiteTextViewModel>();
            SiteTextViewModel ObjSiteTextViewModels = new SiteTextViewModel();
            ObjSiteTextViewModels.SiteTextID = 1;
            ObjSiteTextViewModels.SiteID = 1;
            ObjSiteTextViewModels.IsProofing = false;
            ObjSiteTextViewModels.IsProofingAvailableForSiteTextId = false;
            ObjSiteTextViewModels.ResourceKey = "1";
            ObjSiteTextViewModels.Text = "Test";
            ObjSiteTextViewModels.Version = "Production";
            ObjSiteTextViewModels.VersionID = 1;
            expected.Add(ObjSiteTextViewModels);
            ValidateEmptyData<SiteTextViewModel>(result);
            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Search_Returns_JsonResult_ProofingVersion
        /// <summary>
        /// Search_Returns_JsonResult_ProofingVersion
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_ProofingVersion()
        {
            //Arrange
            //   mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>())).Returns(CreateSiteTextList);
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>(), It.IsAny<SiteTextSortDetail>())).Returns(CreateSiteTextList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Search(null, "Proofing");

            // Verify and Assert
            List<SiteTextViewModel> expected = new List<SiteTextViewModel>();
            SiteTextViewModel ObjSiteTextViewModels = new SiteTextViewModel();
            ObjSiteTextViewModels.SiteTextID = 1;
            ObjSiteTextViewModels.SiteID = 1;
            ObjSiteTextViewModels.IsProofing = false;
            ObjSiteTextViewModels.IsProofingAvailableForSiteTextId = false;
            ObjSiteTextViewModels.ResourceKey = "1";
            ObjSiteTextViewModels.Text = "Test";
            ObjSiteTextViewModels.Version = "Production";
            ObjSiteTextViewModels.VersionID = 1;
            expected.Add(ObjSiteTextViewModels);
            ValidateEmptyData<SiteTextViewModel>(result);


            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Search_Returns_JsonResult_Version_Null
        /// <summary>
        /// Search_Returns_JsonResult_Version_Null
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_Version_Null()
        {
            //Arrange        
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>(), It.IsAny<SiteTextSortDetail>())).Returns(CreateSiteTextList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Search(null, null);

            // Verify and Assert                     
            ValidateEmptyData<SiteTextViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            mockSiteTextFactoryCache.VerifyAll();
        }
        #endregion
        #region Search_Returns_JsonResult_IsVersionInvalid
        /// <summary>
        /// Search_Returns_JsonResult_IsVersionInvalid
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_IsVersionInvalid()
        {
            //Arrange
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteTextSearchDetail>())).Returns(CreateSiteTextList);
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>(), It.IsAny<SiteTextSortDetail>())).Returns(CreateSiteTextList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Search(null, "DefaultProofing");

            // Verify and Assert       
            ValidateEmptyData<SiteTextViewModel>(result);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Search_Returns_JsonResult_With_IsProofingTrueObjectModel
        /// <summary>
        /// Search_Returns_JsonResult_With_IsProofingTrueObjectModel
        /// </summary>
        [TestMethod]
        public void Search_Returns_JsonResult_With_IsProofingTrueObjectModel()
        {
            //Arrange
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteTextSearchDetail>()))
                .Returns(CreateSiteTextList());
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteTextSearchDetail>(), It.IsAny<SiteTextSortDetail>()))
                .Returns(CreateSiteTextList_IsProofingTrue());

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Search(null, "Proofing");

            // Verify and Assert
            List<SiteTextViewModel> expected = new List<SiteTextViewModel>();
            SiteTextViewModel ObjSiteTextViewModels = new SiteTextViewModel();
            ObjSiteTextViewModels.SiteTextID = 1;
            ObjSiteTextViewModels.SiteID = 1;
            ObjSiteTextViewModels.IsProofing = true;
            ObjSiteTextViewModels.IsProofingAvailableForSiteTextId = false;
            ObjSiteTextViewModels.ResourceKey = "1";
            ObjSiteTextViewModels.Text = "Test";
            ObjSiteTextViewModels.Version = "Proofing";
            ObjSiteTextViewModels.VersionID = 1;
            expected.Add(ObjSiteTextViewModels);
            ValidateData(expected, result);

            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Disable_Returns_JsonResult
        /// <summary>
        /// Disable_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void Disable_Returns_JsonResult()
        {
            //Arrange
            mockSiteTextFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<SiteTextObjectModel>(), It.IsAny<int>()));

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Disable(1, 1, false);

            // Verify and Assert      
            mockSiteTextFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, string.Empty);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region LoadDefaultTextForResourceKey_Returns_JsonResult_CssResourceKey
        /// <summary>
        /// LoadDefaultTextForResourceKey_Returns_JsonResult_CssResourceKey
        /// </summary>
        [TestMethod]
        public void LoadDefaultTextForResourceKey_Returns_JsonResult_CssResourceKey()
        {
            //Arrange
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                TemplateId = 1,
            };

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.LoadDefaultTextForResourceKey("test.css");

            // Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mockTemplateTextFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, "Test\n.Test#Test{\nTest;\nTest}\nTest: Test");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region LoadDefaultTextForResourceKey_Returns_JsonResult
        /// <summary>
        /// LoadDefaultTextForResourceKey_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void LoadDefaultTextForResourceKey_Returns_JsonResult()
        {
            //Arrange
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                TemplateId = 1,
            };
            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.LoadDefaultTextForResourceKey("test");

            // Verify and Assert
            mockSiteFactoryCache.VerifyAll();
            mockTemplateTextFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, "Test.Test#Test{Test;Test}Test:Test");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
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

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
                 {
                     TemplateId = 1,

                 };
            SiteTextObjectModel objSiteTextObjectModel = new SiteTextObjectModel()
            {
                SiteTextID = 1,
                ResourceKey = "1"
            };

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockSiteTextFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteTextKey>()))
                .Returns(objSiteTextObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);
            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(1, 1);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "1", Value = "1" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ModifiedByName = "Test_UserName";
            objExpected.ResourceKeys = lstResourceKeys;
            objExpected.PlainText = null;
            objExpected.SelectedResourceKey = "1";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 0;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));

            // Verify and Assert
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockSiteTextFactoryCache.VerifyAll();
            mockUserFactoryCache.Verify();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_ResourceKey_css
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_ResourceKey_css
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_ResourceKey_css()
        {
            //Arrange

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                TemplateId = 1,

            };
            SiteTextObjectModel objSiteTextObjectModel = new SiteTextObjectModel()
            {
                SiteTextID = 1,
                ResourceKey = "css",
                Text = "Test"
            };

            IEnumerable<TemplateTextObjectModel> IEnumTemplateText = Enumerable.Empty<TemplateTextObjectModel>();
            List<TemplateTextObjectModel> lstTemplateText = new List<TemplateTextObjectModel>();
            TemplateTextObjectModel objTemplateText = new TemplateTextObjectModel();
            objTemplateText.ResourceKey = "css";
            objTemplateText.IsHTML = false;
            objTemplateText.DefaultText = "Test.Test#Test{Test;Test}Test:Test";
            lstTemplateText.Add(objTemplateText);
            IEnumTemplateText = lstTemplateText;

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(IEnumTemplateText);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockSiteTextFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteTextKey>()))
                .Returns(objSiteTextObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);
            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(1, 1);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "css", Value = "css" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.PlainText = "Test";
            objExpected.ModifiedByName = "Test_UserName";
            objExpected.ResourceKeys = lstResourceKeys;
            objExpected.SelectedResourceKey = "css";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 0;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));

            // Verify and Assert
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockSiteTextFactoryCache.VerifyAll();
            mockUserFactoryCache.Verify();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_Userid_Null
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_Userid_Null
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_Userid_Null()
        {
            //Arrange
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
 {
     TemplateId = 1,

 };
            SiteTextObjectModel objSiteTextObjectModel = new SiteTextObjectModel()
            {
                SiteTextID = 1,
                ResourceKey = "1"
            };

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockSiteTextFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteTextKey>()))
                .Returns(objSiteTextObjectModel);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(1, 1);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "1", Value = "1" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ModifiedByName = "";
            objExpected.ResourceKeys = lstResourceKeys;
            objExpected.PlainText = null;
            objExpected.SelectedResourceKey = "1";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 0;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));

            // Verify and Assert
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockSiteTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_SiteTextIDZero
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_SiteTextIDZero
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_SiteTextIDZero()
        {
            //Arrange

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                TemplateId = 1,

            };
            SiteTextObjectModel objSiteTextObjectModel = new SiteTextObjectModel()
            {
                SiteTextID = 1,
                ResourceKey = "1"
            };

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockSiteTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteTextSearchDetail>()))
                .Returns(CreateSiteTextList);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);
            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(0, 1);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ResourceKeys = lstResourceKeys;
            objExpected.ModifiedByName = null;
            objExpected.PlainText = null;
            objExpected.SelectedResourceKey = null;
            objExpected.SiteTextID = 0;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 0;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));

            // Verify and Assert
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockSiteTextFactoryCache.VerifyAll();
            mockUserFactoryCache.Verify();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_SiteIDGreaterThanZero_Change_Text
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_SiteIDGreaterThanZero_Change_Text
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_SiteIDGreaterThanZero_Change_Text()
        {
            //Arrange

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                TemplateId = 1,

            };
            SiteTextObjectModel objSiteTextObjectModel = new SiteTextObjectModel()
            {
                SiteTextID = 1,
                ResourceKey = "1"
            };

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockSiteTextFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteTextKey>()))
                .Returns(objSiteTextObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);
            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(1, 1);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "1", Value = "1" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ResourceKeys = lstResourceKeys;
            objExpected.ModifiedByName = "Test_UserName";
            objExpected.PlainText = null;
            objExpected.SelectedResourceKey = "1";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 0;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));

            // Verify and Assert
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockSiteTextFactoryCache.VerifyAll();
            mockUserFactoryCache.Verify();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_SiteIDGreaterThanZero_ResourceKey_Css
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_SiteIDGreaterThanZero_ResourceKey_Css
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_SiteIDGreaterThanZero_ResourceKey_Css()
        {
            //Arrange

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                TemplateId = 1,

            };
            SiteTextObjectModel objSiteTextObjectModel = new SiteTextObjectModel()
            {
                SiteTextID = 1,
                ResourceKey = "Css"
            };

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList_Resourcekey_css);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockSiteTextFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteTextKey>()))
                .Returns(objSiteTextObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(1, 1);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "Css", Value = "Css" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ModifiedByName = "Test_UserName";
            objExpected.PlainText = null;
            objExpected.ResourceKeys = lstResourceKeys;
            objExpected.SelectedResourceKey = "Css";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 0;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));

            // Verify and Assert
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockSiteTextFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_AddFunction
        /// <summary>
        /// Edit_Returns_ActionResult_GetMethod_AddFunction
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_AddFunction()
        {
            //Arrange

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                TemplateId = 1,

            };
            SiteTextObjectModel objSiteTextObjectModel = new SiteTextObjectModel()
            {
                SiteTextID = 1,
                ResourceKey = "1"
            };

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(0, 1);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "1", Value = "1" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ModifiedByName = null;
            objExpected.PlainText = null;
            objExpected.ResourceKeys = lstResourceKeys;
            objExpected.SelectedResourceKey = null;
            objExpected.SiteTextID = 0;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 0;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));

            // Verify and Assert
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockUserFactoryCache.Verify();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_PopMethod
        /// <summary>
        /// Edit_Returns_ActionResult_PopMethod
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_PopMethod()
        {
            //Arrange
            EditSiteTextViewModel viewModelEditSiteTextViewModel = new EditSiteTextViewModel();
            viewModelEditSiteTextViewModel.SiteTextID = 1;
            viewModelEditSiteTextViewModel.VersionID = 1;
            viewModelEditSiteTextViewModel.SelectedResourceKey = "Test";
            viewModelEditSiteTextViewModel.PlainText = "Test.Test#Test{Test;Test}Test:Test";
            viewModelEditSiteTextViewModel.IsProofing = false;
            SiteObjectModel obj = new SiteObjectModel()
            {
                TemplateId = 1,
            };
            mockSiteTextFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteTextObjectModel>(), It.IsAny<int>()));
            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(obj);
            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(viewModelEditSiteTextViewModel);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "1", Value = "1" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ModifiedByName = null;
            objExpected.PlainText = "Test.Test#Test{Test;Test}Test:Test";
            objExpected.SelectedResourceKey = "Test";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 1;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));


            // Verify and Assert
            mockSiteTextFactoryCache.VerifyAll();
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_POST_Returns_ActionResult_Handles_Exception
        /// <summary>
        /// Edit_POST_Returns_ActionResult_Handles_Exception
        /// </summary>
        [TestMethod]
        public void Edit_POST_Returns_ActionResult_Handles_Exception()
        {
            //Arrange
            EditSiteTextViewModel viewModelEditSiteTextViewModel = new EditSiteTextViewModel();
            viewModelEditSiteTextViewModel.SiteTextID = 1;
            viewModelEditSiteTextViewModel.VersionID = 1;
            viewModelEditSiteTextViewModel.SelectedResourceKey = "Test";
            viewModelEditSiteTextViewModel.PlainText = "Test.Test#Test{Test;Test}Test:Test";
            viewModelEditSiteTextViewModel.IsProofing = false;
            viewModelEditSiteTextViewModel.ModifiedBy = 1;
            viewModelEditSiteTextViewModel.SuccessOrFailedMessage = "false";

            mockSiteTextFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteTextObjectModel>(), It.IsAny<int>())).Throws(new Exception());
            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);     
            var result = objController.Edit(viewModelEditSiteTextViewModel);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "1", Value = "1" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = 1;
            objExpected.ModifiedByName = null;
            objExpected.PlainText = "Test.Test#Test{Test;Test}Test:Test";
            objExpected.SelectedResourceKey = "Test";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = "Exception of type 'System.Exception' was thrown.";
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 1;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));

            // Verify and Assert
            mockTemplateTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_WithCss_PopMethod
        /// <summary>
        /// Edit_Returns_ActionResult_WithCss_PopMethod
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_WithCss_PopMethod()
        {
            //Arrange
            EditSiteTextViewModel viewModelEditSiteTextViewModel = new EditSiteTextViewModel();
            viewModelEditSiteTextViewModel.SiteTextID = 1;
            viewModelEditSiteTextViewModel.VersionID = 1;
            viewModelEditSiteTextViewModel.SelectedResourceKey = "Test.css";
            viewModelEditSiteTextViewModel.PlainText = "Test";
            viewModelEditSiteTextViewModel.HtmlText = "&lt;br /&gt;Test&amp;nbsp;TestA\nTest{Test}Test;}Test123";
            viewModelEditSiteTextViewModel.IsProofing = false;

            mockSiteTextFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteTextObjectModel>(), It.IsAny<int>()));
          //  mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());
          //  mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TemplateTextSearchDetail>())).Returns(CreateTemplateTextList());

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(viewModelEditSiteTextViewModel);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = "&lt;br /&gt;Test&amp;nbsp;TestA\nTest{Test}Test;}Test123";
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ModifiedByName = null;
            objExpected.PlainText = "Test";
            objExpected.ResourceKeys = lstResourceKeys;
            objExpected.SelectedResourceKey = "Test.css";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = "Object reference not set to an instance of an object.";
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 1;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));

            // Verify and Assert
            mockSiteTextFactoryCache.VerifyAll();
           // mockSiteFactoryCache.VerifyAll();
           // mockTemplateTextFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult
        /// <summary>
        /// Edit_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult()
        {
            //Arrange
            EditSiteTextViewModel viewModelEditSiteTextViewModel = new EditSiteTextViewModel();
            viewModelEditSiteTextViewModel.SiteTextID = 1;
            viewModelEditSiteTextViewModel.VersionID = 1;
            viewModelEditSiteTextViewModel.SelectedResourceKey = "Test";
            viewModelEditSiteTextViewModel.PlainText = "Test.Test#Test{Test;Test}Test:Test";
            viewModelEditSiteTextViewModel.IsProofing = false;
            viewModelEditSiteTextViewModel.ModifiedBy = 1;
            viewModelEditSiteTextViewModel.SuccessOrFailedMessage = "false";

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                TemplateId = 1,

            };
            SiteTextObjectModel objSiteTextObjectModel = new SiteTextObjectModel()
            {
                SiteTextID = 1,
                ResourceKey = "1"
            };

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockSiteTextFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<SiteTextKey>()))
                .Returns(objSiteTextObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(CreateUserList);

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(1, 1);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "1", Value = "1" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ModifiedByName = "Test_UserName";
            objExpected.PlainText = null;
            objExpected.ResourceKeys = lstResourceKeys;
            objExpected.SelectedResourceKey = "1";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 0;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));


            // Verify and Assert
            mockSiteTextFactoryCache.VerifyAll();
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Post_Returns_ActionResult_EmptyDetails
        /// <summary>
        /// Edit_Post_Returns_ActionResult_EmptyDetails
        /// </summary>
        [TestMethod]
        public void Edit_Post_Returns_ActionResult_EmptyDetails()
        {
            //Arrange
            EditSiteTextViewModel viewModelEditSiteTextViewModel = new EditSiteTextViewModel();
            viewModelEditSiteTextViewModel.SiteTextID = 1;
            viewModelEditSiteTextViewModel.VersionID = 1;
            viewModelEditSiteTextViewModel.SelectedResourceKey = "Test";
            viewModelEditSiteTextViewModel.PlainText = "Test.Test#Test{Test;Test}Test:Test";
            viewModelEditSiteTextViewModel.IsProofing = false;
            SiteObjectModel obj = new SiteObjectModel()
            {
                TemplateId = 1,
            };
            IEnumerable<TemplateTextObjectModel> IEnumTemplateText = Enumerable.Empty<TemplateTextObjectModel>();

            mockSiteTextFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteTextObjectModel>(), It.IsAny<int>()));
            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(IEnumTemplateText);
            mockSiteFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(obj);
            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.Edit(viewModelEditSiteTextViewModel);

            List<DisplayValuePair> lstResourceKeys = new List<DisplayValuePair>();
            lstResourceKeys.Add(new DisplayValuePair() { Display = "--Please select Resource Key--", Value = "-1" });
            lstResourceKeys.Add(new DisplayValuePair() { Display = "1", Value = "1" });

            EditSiteTextViewModel objExpected = new EditSiteTextViewModel();
            objExpected.HtmlText = null;
            objExpected.IsProofing = false;
            objExpected.IsProofingAvailableForSiteTextId = false;
            objExpected.ModifiedBy = null;
            objExpected.ModifiedByName = null;
            objExpected.PlainText = "Test.Test#Test{Test;Test}Test:Test";
            objExpected.SelectedResourceKey = "Test";
            objExpected.SiteTextID = 1;
            objExpected.SuccessOrFailedMessage = null;
            objExpected.UTCLastModifiedDate = null;
            objExpected.VersionID = 1;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditSiteTextViewModel;
            ValidateViewModelData<EditSiteTextViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditSiteTextViewModel));


            // Verify and Assert
            mockSiteTextFactoryCache.VerifyAll();
            mockTemplateTextFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region CheckIsHtmlTextForResourceKey_Returns_JsonResult
        ///<summary>
        ///CheckIsHtmlTextForResourceKey_Returns_JsonResult
        ///</summary>
        [TestMethod]
        public void CheckIsHtmlTextForResourceKey_Returns_JsonResult()
        {
            //Arrange
            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>())).Returns(CreateTemplateTextList());

            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.CheckIsHtmlTextForResourceKey("ABC");

            // Verify and Assert
            mockTemplateTextFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, true);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region CheckIsHtmlTextForResourceKey_Returns_JsonResult_EmptyDetails
        ///<summary>
        ///CheckIsHtmlTextForResourceKey_Returns_JsonResult_EmptyDetails
        ///</summary>
        [TestMethod]
        public void CheckIsHtmlTextForResourceKey_Returns_JsonResult_EmptyDetails()
        {
            //Arrange
            IEnumerable<TemplateTextObjectModel> IEnumTemplateText = Enumerable.Empty<TemplateTextObjectModel>();
            //Act
            SiteTextController objController = new SiteTextController(mockSiteTextFactoryCache.Object, mockSiteFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockUserFactoryCache.Object);
            var result = objController.CheckIsHtmlTextForResourceKey("-1");
            // Verify and Assert           
            Assert.AreEqual(result.Data, true);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
    }
}
