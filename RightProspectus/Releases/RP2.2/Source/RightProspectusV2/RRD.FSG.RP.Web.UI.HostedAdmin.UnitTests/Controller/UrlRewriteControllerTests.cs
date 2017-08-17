//using System;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Entities.System;
using Moq;
using RRD.DSA;
using RRD.DSA.Core;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;


namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    [TestClass]
    public class UrlRewriteControllerTests : BaseTestController<UrlRewriteViewModel>
    {
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> userCacheFactory;
        Mock<IFactoryCache<UrlRewriteFactory, UrlRewriteObjectModel, int>> urlRewriteCacheFactory;

        [TestInitialize]
        public void TestInitialze()
        {
            userCacheFactory = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            urlRewriteCacheFactory = new Mock<IFactoryCache<UrlRewriteFactory, UrlRewriteObjectModel, int>>();
        }

        #region ReturnLists
        private IEnumerable<UrlRewriteObjectModel> CreateUrlRewriteList()
        {
            IEnumerable<UrlRewriteObjectModel> IenumUrlRewriteObjectModel = Enumerable.Empty<UrlRewriteObjectModel>();
            List<UrlRewriteObjectModel> lstUrlRewriteObjectModel = new List<UrlRewriteObjectModel>();

            UrlRewriteObjectModel objUrlRewriteObjectModel = new UrlRewriteObjectModel();
            objUrlRewriteObjectModel.UrlRewriteId = 1;
            objUrlRewriteObjectModel.PatternName = "Test_Doc";
            objUrlRewriteObjectModel.MatchPattern = "Pattern_test";
            objUrlRewriteObjectModel.RewriteFormat = "Format_test";
            lstUrlRewriteObjectModel.Add(objUrlRewriteObjectModel);
            IenumUrlRewriteObjectModel = lstUrlRewriteObjectModel;
            return IenumUrlRewriteObjectModel;
        }
        private IEnumerable<UserObjectModel> CreateUserList()
        {
            IEnumerable<UserObjectModel> IenumUserObjectModel = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();

            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 103;
            objUserObjectModel.UserName = "Test_Doc";
            lstUserObjectModel.Add(objUserObjectModel);
            IenumUserObjectModel = lstUserObjectModel;
            return IenumUserObjectModel;

        }
        private IEnumerable<UrlRewriteObjectModel> CreateUrlRewriteEmptyList()
        {
            IEnumerable<UrlRewriteObjectModel> IenumUrlRewriteObjectModel = Enumerable.Empty<UrlRewriteObjectModel>();
            List<UrlRewriteObjectModel> lstObjectModel = new List<UrlRewriteObjectModel>();
            lstObjectModel.Add(null);
            IenumUrlRewriteObjectModel = lstObjectModel;
            return IenumUrlRewriteObjectModel;
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
            UrlRewriteController objURLRewriteController =
                new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objURLRewriteController.List();

            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region GetPatternName_Returns_JonResult
        /// <summary>
        ///List_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void GetPatternNames_Returns_JsonResult()
        {
            //Arrange
            urlRewriteCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateUrlRewriteList());
            //Act
            UrlRewriteController objUrlRewriteController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);

            var result = objUrlRewriteController.GetPatternName();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "Test_Doc" });
            ValidateDisplayValuePair(lstexpected, result);

            urlRewriteCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region GetPatternName_Returns_JonResult_EmptyPatterndetails
        /// <summary>
        ///GetPatternName_Returns_JonResult_EmptyPatterndetails
        /// </summary>
        [TestMethod]
        public void GetPatternName_Returns_JonResult_EmptyPatterndetails()
        {
            //Arrange
            IEnumerable<UrlRewriteObjectModel> IenumUrlRewriteObjectModel = Enumerable.Empty<UrlRewriteObjectModel>();

            urlRewriteCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(IenumUrlRewriteObjectModel);
            //Act
            UrlRewriteController objUrlRewriteController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);

            var result = objUrlRewriteController.GetPatternName();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
            urlRewriteCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region CheckPatternName_Returns_JonResult_isPatternNameExistsIsTrue
        /// <summary>
        ///CheckPatternName_Returns_JSonResult_isPatternNameExistsIsTrue
        /// </summary>
        [TestMethod]
        public void CheckPatternName_Returns_JsonResult_isPatternNameExistsIsTrue()
        {
            //Arrange
            urlRewriteCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateUrlRewriteList());
            //Act
            UrlRewriteController objUrlRewriteController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objUrlRewriteController.CheckPatternName("Test_Doc");
            //Verify and Assert
            Assert.AreEqual(result.Data, true);
            urlRewriteCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region CheckPatternName_Returns_JSonResult
        /// <summary>
        ///CheckPatternName_Returns_JSonResult
        /// </summary>
        [TestMethod]
        public void CheckPatternName_Returns_JsonResult()
        {
            //Arrange
            urlRewriteCacheFactory.Setup(x => x.GetAllEntities())
                .Returns(CreateUrlRewriteList());
            //Act
            UrlRewriteController objUrlRewriteController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objUrlRewriteController.CheckPatternName("XBRL");
            //Verify and Assert
            urlRewriteCacheFactory.VerifyAll();
            Assert.AreEqual(result.Data, false);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_EqualsToZero
        /// <summary>
        ///CheckPatternName_Returns_ActionResult_EqualsToZero
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_EqualsToZero()
        {
            //Act
            UrlRewriteController objUrlRewriteController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objUrlRewriteController.Edit(0);
            EditUrlRewriteViewModel ObjEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            ObjEditUrlRewriteViewModel.UrlRewriteId = 0;
            ObjEditUrlRewriteViewModel.PatternName = null;
            ObjEditUrlRewriteViewModel.MatchPattern = null;
            ObjEditUrlRewriteViewModel.ModifiedBy = null;
            ObjEditUrlRewriteViewModel.ModifiedByName = null;
            ObjEditUrlRewriteViewModel.RewriteFormat = null;
            ObjEditUrlRewriteViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditUrlRewriteViewModel;
            ValidateViewModelData<EditUrlRewriteViewModel>(viewModel, ObjEditUrlRewriteViewModel);

            //Verify and Assert   
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_IsGreaterthanZero_Returns_URLRewriteDetails
        /// <summary>
        ///Edit_Returns_ActionResult_GetMethod_IsGreaterthanZero_Returns_URLRewriteDetails
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_IsGreaterthanZero_Returns_URLRewriteDetails()
        {
            //Arrange
            UrlRewriteObjectModel objUrlRewriteObjectModel = new UrlRewriteObjectModel();
            objUrlRewriteObjectModel.PatternName = "test_p";
            objUrlRewriteObjectModel.MatchPattern = "test_m";
            objUrlRewriteObjectModel.RewriteFormat = "test_format";
            objUrlRewriteObjectModel.UrlRewriteId = 1;

            urlRewriteCacheFactory.Setup(x => x.GetEntityByKey(1))
               .Returns(objUrlRewriteObjectModel);
            userCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
               .Returns(CreateUserList());

            //Act
            UrlRewriteController objUrlRewriteController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objUrlRewriteController.Edit(1);

            EditUrlRewriteViewModel ObjEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            ObjEditUrlRewriteViewModel.UrlRewriteId = 1;
            ObjEditUrlRewriteViewModel.PatternName = "test_p";
            ObjEditUrlRewriteViewModel.MatchPattern = "test_m";
            ObjEditUrlRewriteViewModel.RewriteFormat = "test_format";
            ObjEditUrlRewriteViewModel.PatternNames = null;
            ObjEditUrlRewriteViewModel.ModifiedBy = null;
            ObjEditUrlRewriteViewModel.ModifiedByName = "Test_Doc";
            ObjEditUrlRewriteViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditUrlRewriteViewModel;
            ValidateViewModelData<EditUrlRewriteViewModel>(viewModel, ObjEditUrlRewriteViewModel);

            //Verify and Assert
            urlRewriteCacheFactory.VerifyAll();
            userCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_EmptyUrlRewriteDetails
        /// <summary>
        ///Edit_Returns_ActionResult_GetMethod_EmptyUrlRewriteDetails
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_EmptyUrlRewriteDetails()
        {
            //Arrange
            UrlRewriteObjectModel objUrlRewriteObjectModel = new UrlRewriteObjectModel();
            objUrlRewriteObjectModel.PatternName = "test_p";
            objUrlRewriteObjectModel.MatchPattern = "test_m";
            objUrlRewriteObjectModel.RewriteFormat = "test_format";
            objUrlRewriteObjectModel.UrlRewriteId = 1;

            IEnumerable<UserObjectModel> IenumUserObjectModel = Enumerable.Empty<UserObjectModel>();

            urlRewriteCacheFactory.Setup(x => x.GetEntityByKey(1))
                .Returns(objUrlRewriteObjectModel);

            userCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>()))
                .Returns(IenumUserObjectModel);
            //Act
            UrlRewriteController objUrlRewriteController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objUrlRewriteController.Edit(1);


            EditUrlRewriteViewModel ObjEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            ObjEditUrlRewriteViewModel.UrlRewriteId = 1;
            ObjEditUrlRewriteViewModel.PatternName = "test_p";
            ObjEditUrlRewriteViewModel.MatchPattern = "test_m";
            ObjEditUrlRewriteViewModel.RewriteFormat = "test_format";
            ObjEditUrlRewriteViewModel.PatternNames = null;
            ObjEditUrlRewriteViewModel.ModifiedBy = null;
            ObjEditUrlRewriteViewModel.ModifiedByName = "";
            ObjEditUrlRewriteViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditUrlRewriteViewModel;
            ValidateViewModelData<EditUrlRewriteViewModel>(viewModel, ObjEditUrlRewriteViewModel);


            //Verify and Assert
            urlRewriteCacheFactory.VerifyAll();
            userCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region Edit_Returns_ActionResult_GetMethod_IsGreaterthanZero_Returns_urlRewriteObjectModel_Null
        /// <summary>
        ///Edit_Returns_ActionResult_GetMethod_IsGreaterthanZero_Returns_urlRewriteObjectModel_Null
        /// </summary>
        [TestMethod]
        public void Edit_Returns_ActionResult_GetMethod_IsGreaterthanZero_Returns_urlRewriteObjectModel_Null()
        {
            //Arrange
            UrlRewriteObjectModel objUrlRewriteObjectModel = null;

            urlRewriteCacheFactory.Setup(x => x.GetEntityByKey(1))
               .Returns(objUrlRewriteObjectModel);
           

            //Act
            UrlRewriteController objUrlRewriteController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objUrlRewriteController.Edit(1);

            EditUrlRewriteViewModel ObjEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            ObjEditUrlRewriteViewModel.UrlRewriteId = 0;
            ObjEditUrlRewriteViewModel.PatternName = null;
            ObjEditUrlRewriteViewModel.MatchPattern = null;
            ObjEditUrlRewriteViewModel.RewriteFormat = null;
            ObjEditUrlRewriteViewModel.PatternNames = null;
            ObjEditUrlRewriteViewModel.ModifiedBy = null;
            ObjEditUrlRewriteViewModel.ModifiedByName = null;
            ObjEditUrlRewriteViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditUrlRewriteViewModel;
            ValidateViewModelData<EditUrlRewriteViewModel>(viewModel, ObjEditUrlRewriteViewModel);

            //Verify and Assert
            urlRewriteCacheFactory.VerifyAll();
       
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region EditUrlRewrite_Returns_ActionResult_PostMethod
        /// <summary>
        /// UrlRewriteEdit_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditUrlRewrite_Returns_ActionResult_PostMethod()
        {
            //Arrange
            EditUrlRewriteViewModel objEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            objEditUrlRewriteViewModel.UrlRewriteId = 1;
            objEditUrlRewriteViewModel.PatternName = "test_p";
            objEditUrlRewriteViewModel.MatchPattern = "test_m";
            objEditUrlRewriteViewModel.RewriteFormat = "test_format";

            UrlRewriteObjectModel objUrlRewriteObjectModel = new UrlRewriteObjectModel();
            objUrlRewriteObjectModel.PatternName = "test_p";
            objUrlRewriteObjectModel.MatchPattern = "test_m";
            objUrlRewriteObjectModel.RewriteFormat = "test_format";
            objUrlRewriteObjectModel.UrlRewriteId = 1;


            //Act
            UrlRewriteController objDocumentTypeExternalIdController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objDocumentTypeExternalIdController.Edit(objEditUrlRewriteViewModel);

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Value = "-1", Display = "--Please select Pattern Name--" });
            lstexpected.Add(new DisplayValuePair() { Value = "Test_Doc", Display = "Test_Doc" });


            EditUrlRewriteViewModel ObjEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            ObjEditUrlRewriteViewModel.UrlRewriteId = 1;
            ObjEditUrlRewriteViewModel.PatternName = "test_p";
            ObjEditUrlRewriteViewModel.MatchPattern = "test_m";
            ObjEditUrlRewriteViewModel.RewriteFormat = "test_format";
            ObjEditUrlRewriteViewModel.PatternNames = lstexpected;
            ObjEditUrlRewriteViewModel.ModifiedBy = null;
            ObjEditUrlRewriteViewModel.ModifiedByName = null;
            ObjEditUrlRewriteViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditUrlRewriteViewModel;
            ValidateViewModelData<EditUrlRewriteViewModel>(viewModel, ObjEditUrlRewriteViewModel);

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region EditUrlRewrite_Returns_ActionResult_PostMethod_Catchblock
        /// <summary>
        /// EditUrlRewrite_Returns_ActionResult_PostMethod_Catchblock
        /// </summary>
        [TestMethod]
        public void EditUrlRewrite_Returns_ActionResult_PostMethod_Catchblock()
        {
            //Arrange
            EditUrlRewriteViewModel objEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            objEditUrlRewriteViewModel.UrlRewriteId = 1;
            objEditUrlRewriteViewModel.PatternName = "Test";
            urlRewriteCacheFactory.Setup(x => x.SaveEntity(It.IsAny<UrlRewriteObjectModel>(), It.IsAny<int>())).Throws(new Exception());
            //Act
            UrlRewriteController objDocumentTypeExternalIdController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objDocumentTypeExternalIdController.Edit(objEditUrlRewriteViewModel);


            EditUrlRewriteViewModel ObjEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            ObjEditUrlRewriteViewModel.UrlRewriteId = 1;
            ObjEditUrlRewriteViewModel.PatternName = "Test";
            ObjEditUrlRewriteViewModel.MatchPattern = null;
            ObjEditUrlRewriteViewModel.RewriteFormat = null;
            ObjEditUrlRewriteViewModel.PatternNames = null;
            ObjEditUrlRewriteViewModel.ModifiedBy = null;
            ObjEditUrlRewriteViewModel.ModifiedByName = null;
            ObjEditUrlRewriteViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditUrlRewriteViewModel;
            ValidateViewModelData<EditUrlRewriteViewModel>(viewModel, ObjEditUrlRewriteViewModel);


            //Verify and Assert
            urlRewriteCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region EditUrlRewrite_Returns_ActionResult_PostMethod_EmptyUrlRewriteDetails
        /// <summary>
        /// EditUrlRewrite_Returns_ActionResult_PostMethod_EmptyUrlRewriteDetails
        /// </summary>
        [TestMethod]
        public void EditUrlRewrite_Returns_ActionResult_PostMethod_EmptyUrlRewriteDetails()
        {
            //Arrange
            IEnumerable<UrlRewriteObjectModel> IenumUrlRewriteObjectModel = Enumerable.Empty<UrlRewriteObjectModel>();

            EditUrlRewriteViewModel objEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            objEditUrlRewriteViewModel.UrlRewriteId = 1;
            objEditUrlRewriteViewModel.PatternName = "Test";

            urlRewriteCacheFactory.Setup(x => x.SaveEntity(It.IsAny<UrlRewriteObjectModel>(), It.IsAny<int>()));

            //Act
            UrlRewriteController objDocumentTypeExternalIdController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objDocumentTypeExternalIdController.Edit(objEditUrlRewriteViewModel);

            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Value = "-1", Display = "--Please select Pattern Name--" });

            EditUrlRewriteViewModel ObjEditUrlRewriteViewModel = new EditUrlRewriteViewModel();
            ObjEditUrlRewriteViewModel.UrlRewriteId = 1;
            ObjEditUrlRewriteViewModel.PatternName = "Test";
            ObjEditUrlRewriteViewModel.MatchPattern = null;
            ObjEditUrlRewriteViewModel.RewriteFormat = null;
            ObjEditUrlRewriteViewModel.PatternNames = lstexpected;
            ObjEditUrlRewriteViewModel.ModifiedBy = null;
            ObjEditUrlRewriteViewModel.ModifiedByName = null;
            ObjEditUrlRewriteViewModel.UTCLastModifiedDate = null;

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditUrlRewriteViewModel;
            ValidateViewModelData<EditUrlRewriteViewModel>(viewModel, ObjEditUrlRewriteViewModel);

            //Verify and Assert
            urlRewriteCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        #region SearchUrlRewrite_JsonResult_Calls_UrlRewriteController

        /// <summary>
        /// SearchGetAllUrlRewrite_JsonResult_Calls_UrlRewriteController
        /// </summary>
        [TestMethod]
        public void SearchUrlRewrite_Details_JsonResult_Calls_UrlRewriteController()
        {
            //arrange
            List<UrlRewriteViewModel> lstUrlRewriteExpectedViewModel = new List<UrlRewriteViewModel>();
            UrlRewriteViewModel ObjUrlRewriteViewModel = new UrlRewriteViewModel();
            ObjUrlRewriteViewModel.UrlRewriteId = 1;
            ObjUrlRewriteViewModel.PatternName = "Test_Doc";

            lstUrlRewriteExpectedViewModel.Add(ObjUrlRewriteViewModel);

            urlRewriteCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UrlRewriteSearchDetail>()))
                .Returns(CreateUrlRewriteList());
            urlRewriteCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UrlRewriteSearchDetail>(), It.IsAny<UrlRewriteSortDetail>()))
               .Returns(CreateUrlRewriteList());
            //Act
            UrlRewriteController objDocumentTypeExternalIdController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objDocumentTypeExternalIdController.Search("TAL");

            //Verify and Assert           

            List<UrlRewriteViewModel> expected = new List<UrlRewriteViewModel>();
            UrlRewriteViewModel ObjUrlRewriteViewModels = new UrlRewriteViewModel();
            ObjUrlRewriteViewModels.PatternName = "Test_Doc";
            ObjUrlRewriteViewModels.UrlRewriteId = 1;
            expected.Add(ObjUrlRewriteViewModels);
            ValidateData(expected, result);
            urlRewriteCacheFactory.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region SearchUrlRewrite_JsonResult_Calls_IsNull_UrlRewriteController

        /// <summary>
        /// SearchGetAllUrlRewrite_JsonResult_Calls_IsNull_UrlRewriteController
        /// </summary>
        [TestMethod]
        public void SearchUrlRewrite_Details_JsonResult_Calls_IsNull_UrlRewriteController()
        {
            //arrange
            urlRewriteCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<UrlRewriteSearchDetail>()))
                .Returns(CreateUrlRewriteList());
            urlRewriteCacheFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UrlRewriteSearchDetail>(), It.IsAny<UrlRewriteSortDetail>()))
               .Returns(CreateUrlRewriteList());
            //Act
            UrlRewriteController objDocumentTypeExternalIdController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objDocumentTypeExternalIdController.Search(" ");


            //Verify and Assert          
            urlRewriteCacheFactory.VerifyAll();

            List<UrlRewriteViewModel> expected = new List<UrlRewriteViewModel>();
            UrlRewriteViewModel ObjUrlRewriteViewModel = new UrlRewriteViewModel();
            ObjUrlRewriteViewModel.PatternName = "Test_Doc";
            ObjUrlRewriteViewModel.UrlRewriteId = 1;
            expected.Add(ObjUrlRewriteViewModel);
            ValidateData(expected, result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
        #region Disable_Returns_JonResult
        /// <summary>
        ///Disable_Returns_JonResult
        /// </summary>
        [TestMethod]
        public void Disable_Returns_JonResult()
        {
            //Arrange
            urlRewriteCacheFactory.Setup(x => x.DeleteEntity(It.IsAny<int>(), It.IsAny<int>()));
            //Act
            UrlRewriteController objUrlRewriteController =
         new UrlRewriteController(userCacheFactory.Object, urlRewriteCacheFactory.Object);
            var result = objUrlRewriteController.Disable(2);

            //Verify and Assert
            urlRewriteCacheFactory.VerifyAll();
            Assert.AreEqual(result.Data, string.Empty);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
    }
}
