using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Data.SqlClient;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.SearchEntities.VerticalMarkets;
using RRD.FSG.RP.Model.Enumerations;



namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for TaxonomyLevelExternalIdController class
    /// </summary>
    [TestClass]
    public class TaxonomyLevelExternalIdControllerTests : BaseTestController<TaxonomyLevelExternalIdViewModel>
    {
        Mock<IFactoryCache<TaxonomyFactory, TaxonomyObjectModel, TaxonomyKey>> mockTaxonomyFactoryCache;
        Mock<IFactoryCache<TaxonomyLevelExternalIdFactory, TaxonomyLevelExternalIdObjectModel, TaxonomyLevelExternalIdKey>> mockTaxonomyLevelExternalIdFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;

        [TestInitialize]
        public void TestInitialze()
        {
            mockTaxonomyFactoryCache = new Mock<IFactoryCache<TaxonomyFactory, TaxonomyObjectModel, TaxonomyKey>>();
            mockTaxonomyLevelExternalIdFactoryCache = new Mock<IFactoryCache<TaxonomyLevelExternalIdFactory, TaxonomyLevelExternalIdObjectModel, TaxonomyLevelExternalIdKey>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();


        }

        #region ReturnList
        private IEnumerable<TaxonomyLevelExternalIdObjectModel> CreateList()
        {
            IEnumerable<TaxonomyLevelExternalIdObjectModel> IenumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();
            List<TaxonomyLevelExternalIdObjectModel> lstTaxonomyLevelExternalIdObjectModel = new List<TaxonomyLevelExternalIdObjectModel>();

            TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();
            objTaxonomyLevelExternalIdObjectModel.TaxonomyId = 1;
            objTaxonomyLevelExternalIdObjectModel.Level = 1;
            objTaxonomyLevelExternalIdObjectModel.ExternalId = "Test";
            objTaxonomyLevelExternalIdObjectModel.Description = "Tese_Doc";
            objTaxonomyLevelExternalIdObjectModel.IsPrimary = true;
            objTaxonomyLevelExternalIdObjectModel.TaxonomyName = "TAXONOMY NAME";

            lstTaxonomyLevelExternalIdObjectModel.Add(objTaxonomyLevelExternalIdObjectModel);
            IenumTaxonomyLevelExternalIdObjectModel = lstTaxonomyLevelExternalIdObjectModel;
            return IenumTaxonomyLevelExternalIdObjectModel;
        }

        private IEnumerable<TaxonomyObjectModel> CreateListTaxonomyType()
        {

            IEnumerable<TaxonomyObjectModel> IenumTaxonomyObjectModel = Enumerable.Empty<TaxonomyObjectModel>();
            List<TaxonomyObjectModel> lstTaxonomyObjectModel = new List<TaxonomyObjectModel>();
            TaxonomyObjectModel objTaxonomyObjectModel = new TaxonomyObjectModel();
            objTaxonomyObjectModel.TaxonomyId = 1;
            objTaxonomyObjectModel.Name = "Test_Doc";
            objTaxonomyObjectModel.Level = 1;
            lstTaxonomyObjectModel.Add(objTaxonomyObjectModel);
            IenumTaxonomyObjectModel = lstTaxonomyObjectModel;
            return lstTaxonomyObjectModel;
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
        #endregion

        #region View_Returns_ActionResult
        /// <summary>
        /// View_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void View_Returns_ActionResult()
        {
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
       new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.List();

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetAllTaxonomyLevelExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController
        /// <summary>
        /// GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController
        /// </summary>
        [TestMethod]
        public void GetAllTaxonomyLevelExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController()
        {
            //Arrange
            mockTaxonomyFactoryCache.Setup(x => x.GetAllEntities())
              .Returns(CreateListTaxonomyType());

            IEnumerable<TaxonomyLevelExternalIdObjectModel> IenumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();
            List<TaxonomyLevelExternalIdObjectModel> lstTaxonomyLevelExternalIdObjectModel = new List<TaxonomyLevelExternalIdObjectModel>();

            TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();
            objTaxonomyLevelExternalIdObjectModel.TaxonomyId = 1;
            objTaxonomyLevelExternalIdObjectModel.ExternalId = "TEST";
            objTaxonomyLevelExternalIdObjectModel.TaxonomyName = "Test_Doc";
            objTaxonomyLevelExternalIdObjectModel.IsPrimary = true;
            //objTaxonomyLevelExternalIdObjectModel.ModifiedBy = 1;
            lstTaxonomyLevelExternalIdObjectModel.Add(objTaxonomyLevelExternalIdObjectModel);
            IenumTaxonomyLevelExternalIdObjectModel = lstTaxonomyLevelExternalIdObjectModel;

            TaxonomyLevelExternalIdSearchDetail objTaxonomyLevelExternalIdSearchDetail = new TaxonomyLevelExternalIdSearchDetail();
            objTaxonomyLevelExternalIdSearchDetail.TaxonomyId = 1;
            objTaxonomyLevelExternalIdSearchDetail.ExternalId = "TEST";

            TaxonomyLevelExternalIdSortDetail objTaxonomyLevelExternalIdSortDetail = new TaxonomyLevelExternalIdSortDetail();
            //    objTaxonomyLevelExternalIdSortDetail.Column = TaxonomyLevelExternalIdSortColumn.TaxonomyId;
            objTaxonomyLevelExternalIdSortDetail.Order = SortOrder.Descending;


            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<TaxonomyLevelExternalIdSearchDetail>(), It.IsAny<TaxonomyLevelExternalIdSortDetail>()))
                .Returns(IenumTaxonomyLevelExternalIdObjectModel);

            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
                new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);


            var result = objTaxonomyLevelExternalIdController.GetAllTaxonomyLevelExternalIdDetails("1", "1", "Test");


            //Verify and Assert

            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            ValidateEmptyData<TaxonomyLevelExternalIdViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_notnull
        /// <summary>
        /// GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_notnull
        /// </summary>
        [TestMethod]
        public void GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_notnull()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>())).Returns(CreateList());
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TaxonomyLevelExternalIdSearchDetail>(), It.IsAny<TaxonomyLevelExternalIdSortDetail>()))
                .Returns(CreateList());
            TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController = new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object,
                                                                                     mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);



            var result = objTaxonomyLevelExternalIdController.GetAllTaxonomyLevelExternalIdDetails("Test", "test1", "Test2");

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            ValidateEmptyData<TaxonomyLevelExternalIdViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_WithEmptyTaxonomyId
        /// <summary>
        /// GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_WithEmptyTaxonomyId
        /// </summary>
        [TestMethod]
        public void GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_WithEmptyTaxonomyId()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>())).Returns(CreateList());
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TaxonomyLevelExternalIdSearchDetail>(), It.IsAny<TaxonomyLevelExternalIdSortDetail>())).Returns(CreateList());

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController = new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.GetAllTaxonomyLevelExternalIdDetails("1", string.Empty, "1");

            // Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            ValidateEmptyData<TaxonomyLevelExternalIdViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_WithEmptyLevelId
        /// <summary>
        /// GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_WithEmptyLevelId
        /// </summary>
        [TestMethod]
        public void GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_WithEmptyLevelId()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>())).Returns(CreateList());
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TaxonomyLevelExternalIdSearchDetail>(), It.IsAny<TaxonomyLevelExternalIdSortDetail>())).Returns(CreateList());

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController = new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.GetAllTaxonomyLevelExternalIdDetails(string.Empty, "1", "1");

            // Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            ValidateEmptyData<TaxonomyLevelExternalIdViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_WithStringLevelId
        /// <summary>
        /// GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_WithStringLevelId
        /// </summary>
        [TestMethod]
        public void GetAllTaxonomyLevelExternalIdDetails_Returns_JsonResult_WithStringLevelId()
        {
            //Arrange
            //mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageList());
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>())).Returns(CreateList());
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TaxonomyLevelExternalIdSearchDetail>(), It.IsAny<TaxonomyLevelExternalIdSortDetail>())).Returns(CreateList());

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
          new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.GetAllTaxonomyLevelExternalIdDetails("1", "1", "1");

            // Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            ValidateEmptyData<TaxonomyLevelExternalIdViewModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetLevels_Returns_JsonResult_null
        /// <summary>
        /// GetLevels_Returns_JsonResult_null
        /// </summary>
        [TestMethod]
        public void GetLevels_Returns_JsonResult_null()
        {

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController = new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object,
                                                                                     mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.GetLevels();
            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>
            {
                new DisplayValuePair {Display = "Fund", Value = "1", Selected = false}
            };
            ValidateDisplayValuePair(lstExpected, result);

            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTaxonomy_Returns_JsonResult_null
        /// <summary>
        /// GetTaxonomy_Returns_JsonResult_null
        /// </summary>
        [TestMethod]
        public void GetTaxonomy_Returns_JsonResult_null()
        {
            //Arrange
            IEnumerable<TaxonomyLevelExternalIdObjectModel> IEnumTaxonomyLevelExternalId = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetAllEntities()).Returns(IEnumTaxonomyLevelExternalId);
            TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController = new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object,
                                                                                     mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);



            var result = objTaxonomyLevelExternalIdController.GetTaxonomy();

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            ValidateEmptyData<TaxonomyLevelExternalIdObjectModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetExternalId_Returns_JsonResult_null
        /// <summary>
        /// GetExternalId_Returns_JsonResult_null
        /// </summary>
        [TestMethod]
        public void GetExternalId_Returns_JsonResult_null()
        {
            //Arrange

            IEnumerable<TaxonomyLevelExternalIdObjectModel> IEnumTaxonomyLevelExternalId = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetAllEntities()).Returns(IEnumTaxonomyLevelExternalId);
            TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController = new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object,
                                                                                     mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);

            var result = objTaxonomyLevelExternalIdController.GetExternalId();

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            ValidateEmptyData<TaxonomyLevelExternalIdObjectModel>(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTaxonomyLevel_Returns_JsonResult
        /// <summary>
        /// GetTaxonomyLevel_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetTaxonomyLevel_Returns_JsonResult()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateList());

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
                new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.GetTaxonomy();

            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "TAXONOMY NAME", Value = "1" });
            ValidateDisplayValuePair(lstExpected, result);


            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetLevels_Returns_JsonResult
        /// <summary>
        /// GetLevels_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetLevels_Returns_JsonResult()
        {

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
                new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.GetLevels();


            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "Fund", Value = "1" });
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetExternalId_Returns_JsonResult
        /// <summary>
        /// GetExternalId_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetExternalId_Returns_JsonResult()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateList());

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
                new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.GetExternalId();

            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "Test", Value = "Test" });
            ValidateDisplayValuePair(lstExpected, result);

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod
        /// <summary>
        /// EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod()
        {
            //Arrange
            EditTaxonomyLevelExternalIdViewModel obj = new EditTaxonomyLevelExternalIdViewModel()
            {
                SelectedLevelId = 1,
                SelectedTaxonomyId = 1,
                ExternalId = "Test",
                IsPrimary = true,
                ModifiedBy = 32,
                UTCLastModifiedDate = DateTime.Parse("02/02/2015")

            };
            IEnumerable<TaxonomyLevelExternalIdObjectModel> IenumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();
            List<TaxonomyLevelExternalIdObjectModel> lstTaxonomyLevelExternalIdObjectModel = new List<TaxonomyLevelExternalIdObjectModel>();

            TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();
            objTaxonomyLevelExternalIdObjectModel.Level = 1;
            objTaxonomyLevelExternalIdObjectModel.TaxonomyId = 1;
            objTaxonomyLevelExternalIdObjectModel.ExternalId = "Test";
            objTaxonomyLevelExternalIdObjectModel.TaxonomyName = "Test";
            objTaxonomyLevelExternalIdObjectModel.IsPrimary = true;

            mockTaxonomyFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListTaxonomyType());
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<TaxonomyLevelExternalIdKey>()))
                .Returns(objTaxonomyLevelExternalIdObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>())).Returns(CreateListUser());

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.EditTaxonomyLevelExternalId(1, 1, "Test");

            //Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            mockTaxonomyFactoryCache.VerifyAll();
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod_TaxonomyIdentifier_Equals_Zero
        /// <summary>
        /// EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod_TaxonomyIdentifier_Equals_Zero
        /// </summary>
        [TestMethod]
        public void EditTaxonomyLevelExternalId_Returns_ActionResult_TaxonomyIdentifier_Equals_Zero()
        {
            //Arrange
            EditTaxonomyLevelExternalIdViewModel obj = new EditTaxonomyLevelExternalIdViewModel()
            {
                SelectedLevelId = 1,
                SelectedTaxonomyId = 1,
                ExternalId = "Test",
                IsPrimary = true,
                ModifiedBy = 32,
                UTCLastModifiedDate = DateTime.Parse("02/02/2015")

            };
            IEnumerable<TaxonomyLevelExternalIdObjectModel> IenumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();
            List<TaxonomyLevelExternalIdObjectModel> lstTaxonomyLevelExternalIdObjectModel = new List<TaxonomyLevelExternalIdObjectModel>();
            TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();
            objTaxonomyLevelExternalIdObjectModel.Level = 1;
            objTaxonomyLevelExternalIdObjectModel.TaxonomyId = 1;
            objTaxonomyLevelExternalIdObjectModel.ExternalId = "Test";
            objTaxonomyLevelExternalIdObjectModel.TaxonomyName = "Test";
            objTaxonomyLevelExternalIdObjectModel.IsPrimary = true;

            mockTaxonomyFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListTaxonomyType());

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.EditTaxonomyLevelExternalId(1, 0, "Test");

            //Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            mockTaxonomyFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod_TaxonomyIdentifier_Equals_Null
        /// <summary>
        /// EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod_TaxonomyIdentifier_Equals_Null
        /// </summary>
        [TestMethod]
        public void EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod_TaxonomyIdentifier_Equals_Null()
        {
            //Arrange

            IEnumerable<TaxonomyLevelExternalIdObjectModel> IenumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();

            IEnumerable<TaxonomyObjectModel> IenumTaxonomyObjectModel = Enumerable.Empty<TaxonomyObjectModel>();
            List<TaxonomyLevelExternalIdObjectModel> lstTaxonomyLevelExternalIdObjectModel = new List<TaxonomyLevelExternalIdObjectModel>();

            TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();
            objTaxonomyLevelExternalIdObjectModel.Level = 1;
            objTaxonomyLevelExternalIdObjectModel.TaxonomyId = 1;
            objTaxonomyLevelExternalIdObjectModel.ExternalId = "Test";
            objTaxonomyLevelExternalIdObjectModel.TaxonomyName = "Test";
            objTaxonomyLevelExternalIdObjectModel.IsPrimary = true;


            //objTaxonomyLevelExternalIdObjectModel.ModifiedBy = "";
            lstTaxonomyLevelExternalIdObjectModel.Add(objTaxonomyLevelExternalIdObjectModel);
            IenumTaxonomyLevelExternalIdObjectModel = lstTaxonomyLevelExternalIdObjectModel;

            mockTaxonomyFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListTaxonomyType());
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<TaxonomyLevelExternalIdKey>()))
               .Returns(objTaxonomyLevelExternalIdObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>())).Returns(CreateListUser());

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.EditTaxonomyLevelExternalId(1, null, "Test");

            //Verify and Assert

            mockTaxonomyFactoryCache.VerifyAll();
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditTaxonomyLevelExternalId_Returns_ActionResult_externalIdentifier_Equals_Null
        /// <summary>
        /// EditTaxonomyLevelExternalId_Returns_ActionResult_externalIdentifier_Equals_Null
        /// </summary>
        [TestMethod]
        public void EditTaxonomyLevelExternalId_Returns_ActionResult_externalIdentifier_Equals_Null()
        {
            //Arrange

            IEnumerable<TaxonomyLevelExternalIdObjectModel> IenumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();

            IEnumerable<TaxonomyObjectModel> IenumTaxonomyObjectModel = Enumerable.Empty<TaxonomyObjectModel>();
            List<TaxonomyLevelExternalIdObjectModel> lstTaxonomyLevelExternalIdObjectModel = new List<TaxonomyLevelExternalIdObjectModel>();

            TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();
            objTaxonomyLevelExternalIdObjectModel.Level = 1;
            objTaxonomyLevelExternalIdObjectModel.TaxonomyId = 1;
            objTaxonomyLevelExternalIdObjectModel.ExternalId = "Test";
            objTaxonomyLevelExternalIdObjectModel.TaxonomyName = "Test";
            objTaxonomyLevelExternalIdObjectModel.IsPrimary = true;


            //objTaxonomyLevelExternalIdObjectModel.ModifiedBy = "";
            lstTaxonomyLevelExternalIdObjectModel.Add(objTaxonomyLevelExternalIdObjectModel);
            IenumTaxonomyLevelExternalIdObjectModel = lstTaxonomyLevelExternalIdObjectModel;

            mockTaxonomyFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListTaxonomyType());
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<TaxonomyLevelExternalIdKey>()))
                .Returns(objTaxonomyLevelExternalIdObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<UserSearchDetail>())).Returns(CreateListUser());


            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.EditTaxonomyLevelExternalId(1, 1, string.Empty);

            //Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            mockTaxonomyFactoryCache.VerifyAll();
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        //#region EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod_TaxonomyDetails_equals_null
        ///// <summary>
        ///// EditTaxonomyLevelExternalId_Returns_ActionResult_GetMethod_TaxonomyDetails_equals_null
        ///// </summary>
        //[TestMethod]
        //public void EditTaxonomyLevelExternalId_Returns_ActionResult_TaxonomyDetails_equals_null()
        //{
        //    //Arrange

        //    IEnumerable<TaxonomyLevelExternalIdObjectModel> IenumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();

        //    IEnumerable<TaxonomyObjectModel> IenumTaxonomyObjectModel = Enumerable.Empty<TaxonomyObjectModel>();
        //    List<TaxonomyLevelExternalIdObjectModel> lstTaxonomyLevelExternalIdObjectModel = new List<TaxonomyLevelExternalIdObjectModel>();

        //    TaxonomyLevelExternalIdObjectModel objTaxonomyLevelExternalIdObjectModel = new TaxonomyLevelExternalIdObjectModel();
        //    objTaxonomyLevelExternalIdObjectModel.Level = 1;
        //    objTaxonomyLevelExternalIdObjectModel.TaxonomyId = 1;
        //    objTaxonomyLevelExternalIdObjectModel.ExternalId = "Test";
        //    objTaxonomyLevelExternalIdObjectModel.TaxonomyName = "Test";
        //    objTaxonomyLevelExternalIdObjectModel.IsPrimary = true;


        //    //objTaxonomyLevelExternalIdObjectModel.ModifiedBy = "";
        //    lstTaxonomyLevelExternalIdObjectModel.Add(objTaxonomyLevelExternalIdObjectModel);
        //    IenumTaxonomyLevelExternalIdObjectModel = lstTaxonomyLevelExternalIdObjectModel;

        //    TaxonomySearchDetail objTaxonomySearchDetail = null;
        //    TaxonomySortDetail objTaxonomySortDetail = null;

        //    mockTaxonomyFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListTaxonomyType());
        //    mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateList());
        //    mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UserSearchDetail>())).Returns(CreateListUser());
        //    mockTaxonomyFactoryCache.Setup(
        //        (x =>
        //            x.GetEntitiesBySearch(0, 0, objTaxonomySearchDetail,
        //                objTaxonomySortDetail))).Returns(IenumTaxonomyObjectModel);

        //    //Act
        //    TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
        // new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
        //    var result = objTaxonomyLevelExternalIdController.EditTaxonomyLevelExternalId(1, 0, "Test");

        //    //Verify and Assert

        //    mockTaxonomyFactoryCache.VerifyAll();
        //    mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
        //    mockUserFactoryCache.VerifyAll();
        //    Assert.IsInstanceOfType(result, typeof(ActionResult));
        //}
        //#endregion

        #region EditTaxonomyLevelExternalId_Returns_ActionResult_PostMethod
        /// <summary>
        /// EditTaxonomyLevelExternalId_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditTaxonomyLevelExternalId_Returns_ActionResult_PostMethod()
        {
            EditTaxonomyLevelExternalIdViewModel obj = new EditTaxonomyLevelExternalIdViewModel()
            {
                SelectedLevelId = 1,
                SelectedTaxonomyId = 1,
                ExternalId = "Test",
                IsPrimary = true,
                ModifiedBy = 32,
                UTCLastModifiedDate = DateTime.Parse("02/02/2015")
            };
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.SaveEntity(It.IsAny<TaxonomyLevelExternalIdObjectModel>(), It.IsAny<int>()));
            mockTaxonomyFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListTaxonomyType());
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.EditTaxonomyLevelExternalId(obj);

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            mockTaxonomyFactoryCache.VerifyAll();
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region DisableTaxonomyLevelExtrenalId_Returns_JsonResult
        /// <summary>
        /// DisableDocumentTypeExtrenalId_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DisableTaxonomyLevelExtrenalId_Returns_JsonResult()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<TaxonomyLevelExternalIdObjectModel>(), It.IsAny<int>()));

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.Disable(1, 2, "AR");


            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckTaxonomyLevelExternalAlreadyExists_Returns_JsonResult
        /// <summary>
        /// CheckCombinationDataAlreadyExists_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckTaxonomyLevelExternalAlreadyExists_Returns_JsonResult()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
            .Returns(CreateList);

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckDataAlreadyExists(1, 1, "AR");

            //Verify and Assert
            Assert.AreNotEqual(result.Data, false);
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withEmptyList
        /// <summary>
        /// CheckCombinationDataAlreadyExistsId_Returns_JsonResult_withEmptyList
        /// </summary>
        [TestMethod]
        public void CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withEmptyList()
        {
            //Arrange
            IEnumerable<TaxonomyLevelExternalIdObjectModel> iEnumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();

            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
            .Returns(iEnumTaxonomyLevelExternalIdObjectModel);

            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckDataAlreadyExists(1, 1, "AR");

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, false);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withLevelId_Equals_Null
        /// <summary>
        /// CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withLevelId_Equals_Null
        /// </summary>
        [TestMethod]
        public void CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withLevelId_Equals_Null()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
                          .Returns(CreateList());
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckDataAlreadyExists(null, 1, "AR");

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, true);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withTaxonomyID_Equals_Null
        /// <summary>
        /// CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withTaxonomyID_Equals_Null
        /// </summary>
        [TestMethod]
        public void CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withTaxonomyID_Equals_Null()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
                          .Returns(CreateList());
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckDataAlreadyExists(1, null, "AR");

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, true);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withExternalID_Equals_Null
        /// <summary>
        /// CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withExternalID_Equals_Null
        /// </summary>
        [TestMethod]
        public void CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_withExternalID_Equals_Null()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
                          .Returns(CreateList());
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckDataAlreadyExists(1, 1, string.Empty);

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, true);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_Empty_List
        /// <summary>
        /// CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_Empty_List
        /// </summary>
        [TestMethod]
        public void CheckTaxonomyLevelExternalIdAlreadyExists_Returns_JsonResult_Empty_List()
        {
            //Arrange

            IEnumerable<TaxonomyLevelExternalIdObjectModel> iEnumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
                          .Returns(iEnumTaxonomyLevelExternalIdObjectModel);
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckDataAlreadyExists(1, 1, "AR");

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, false);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckExternalIdAlreadyExists_Returns_JsonResult
        /// <summary>
        /// CheckExternalIdAlreadyExists_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckExternalIdAlreadyExists_Returns_JsonResult()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
                          .Returns(CreateList());
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckExternalIdAlreadyExists("AR");

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, true);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckExternalIdAlreadyExists_Returns_JsonResult_withExternalIdNull
        /// <summary>
        /// CheckExternalIdAlreadyExists_Returns_JsonResult_withExternalIdNull
        /// </summary>
        [TestMethod]
        public void CheckExternalIdAlreadyExists_Returns_JsonResult_withExternalIdNull()
        {
            //Arrange
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
                          .Returns(CreateList());
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckExternalIdAlreadyExists(string.Empty);

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, true);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckExternalIdAlreadyExists_Returns_JsonResult_withEmptyList
        /// <summary>
        /// CheckExternalIdAlreadyExists_Returns_JsonResult_withEmptyList
        /// </summary>
        [TestMethod]
        public void CheckExternalIdAlreadyExists_Returns_JsonResult_withEmptyList()
        {
            //Arrange
            IEnumerable<TaxonomyLevelExternalIdObjectModel> iEnumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();

            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
                          .Returns(iEnumTaxonomyLevelExternalIdObjectModel);
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckExternalIdAlreadyExists("AR");

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, false);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckExternalIdAlreadyExists_Returns_JsonResult_WithExternalId_null
        /// <summary>
        /// CheckExternalIdAlreadyExists_Returns_JsonResult_WithExternalId_null
        /// </summary>
        [TestMethod]
        public void CheckExternalIdAlreadyExists_Returns_JsonResult_withEmptyList_WithExternalId_null()
        {
            //Arrange
            IEnumerable<TaxonomyLevelExternalIdObjectModel> iEnumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();

            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
                          .Returns(CreateList);
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckExternalIdAlreadyExists(string.Empty);

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, true);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckExternalIdAlreadyExists_Returns_JsonResult_TaxonomyLevelexternalIdCacheFactory_GetEntitiesBySearch_null_Count_EqualsZero
        /// <summary>
        /// CheckExternalIdAlreadyExists_Returns_JsonResult_TaxonomyLevelexternalIdCacheFactory_GetEntitiesBySearch_null_Count_EqualsZero
        /// </summary>
        [TestMethod]
        public void CheckExternalIdAlreadyExists_Returns_JsonResult_TaxonomyLevelexternalIdCacheFactory_GetEntitiesBySearch_null_Count_EqualsZero()
        {
            //Arrange
            IEnumerable<TaxonomyLevelExternalIdObjectModel> iEnumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();

            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
                          .Returns(iEnumTaxonomyLevelExternalIdObjectModel);

            //mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TaxonomyLevelExternalIdSearchDetail>()))
            //             .Returns(iEnumTaxonomyLevelExternalIdObjectModel);
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.CheckExternalIdAlreadyExists(string.Empty);

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, false);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region Check_Disable_Returns_JsonResult_ExternalId_Equals_Null
        /// <summary>
        /// Check_Disable_Returns_JsonResult_ExternalId_Equals_Null
        /// </summary>
        [TestMethod]
        public void Check_Disable_Returns_JsonResult_ExternalId_Equals_Null()
        {
            //Arrange

            IEnumerable<TaxonomyLevelExternalIdObjectModel> iEnumTaxonomyLevelExternalIdObjectModel = Enumerable.Empty<TaxonomyLevelExternalIdObjectModel>();
            mockTaxonomyLevelExternalIdFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<TaxonomyLevelExternalIdObjectModel>(), It.IsAny<int>()));
            //Act
            TaxonomyLevelExternalIdController objTaxonomyLevelExternalIdController =
         new TaxonomyLevelExternalIdController(mockTaxonomyLevelExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockTaxonomyFactoryCache.Object);
            var result = objTaxonomyLevelExternalIdController.Disable(1, 1, string.Empty);

            //Verify and Assert
            mockTaxonomyLevelExternalIdFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, "");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion








    }
}
