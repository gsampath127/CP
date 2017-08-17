using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using RRD.FSG.RP.Model;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for DocumentTypeExternalIdController class
    /// </summary>
    [TestClass]
    public class DocumentTypeExternalIdControllerTests : BaseTestController<DocumentTypeExternalIdViewModel>
    {
        Mock<IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int>> mockDocumentTypeFactoryCache;
        Mock<IFactoryCache<DocumentTypeExternalIdFactory, DocumentTypeExternalIdObjectModel, DocumentTypeExternalIdKey>> mockDocumentTypeExternalIdFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;

        [TestInitialize]
        public void TestInitialze()
        {
            //mockDocumentTypeExternalIdFactoryCache = Mock.Create<DocumentTypeExternalIdFactoryCache>();
            //mockDocumentTypeFactoryCache = Mock.Create<DocumentTypeFactoryCache>();

            //mockDocumentTypeExternalIdFactoryCache = new Mock<IFactoryCache<DocumentTypeExternalIdFactory, DocumentTypeExternalIdObjectModel, DocumentTypeExternalIdKey>>();
            //mockDocumentTypeFactoryCache = new Mock<IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int>>();
            //mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();

            mockDocumentTypeFactoryCache = new Mock<IFactoryCache<DocumentTypeFactory, DocumentTypeObjectModel, int>>();
            mockDocumentTypeExternalIdFactoryCache = new Mock<IFactoryCache<DocumentTypeExternalIdFactory, DocumentTypeExternalIdObjectModel, DocumentTypeExternalIdKey>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
        }

        #region ReturnLists
        private IEnumerable<DocumentTypeExternalIdObjectModel> CreateList()
        {
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test_Doc";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 1;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;
            return IenumDocumentTypeExternalIdObjectModel;
        }

        private IEnumerable<DocumentTypeObjectModel> CreateListDocumentType()
        {
            IEnumerable<DocumentTypeObjectModel> IenumDocumentTypeObjectModel = Enumerable.Empty<DocumentTypeObjectModel>();
            List<DocumentTypeObjectModel> lstDocumentTypeObjectModel = new List<DocumentTypeObjectModel>();
            DocumentTypeObjectModel objDocumentTypeObjectModel = new DocumentTypeObjectModel();
            objDocumentTypeObjectModel.DocumentTypeId = 1;
            objDocumentTypeObjectModel.Name = "Test_Doc";
            lstDocumentTypeObjectModel.Add(objDocumentTypeObjectModel);
            IenumDocumentTypeObjectModel = lstDocumentTypeObjectModel;
            return IenumDocumentTypeObjectModel;
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

        #region GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController
        /// <summary>
        /// GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController
        /// </summary>
        [TestMethod]
        public void GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController()
        {
            //Arrange

            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel1 = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test_Doc";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 1;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            DocumentTypeExternalIdSearchDetail objDocumentTypeExternalIdSearchDetail = new DocumentTypeExternalIdSearchDetail();
            objDocumentTypeExternalIdSearchDetail.DocumentTypeID = 1;
            objDocumentTypeExternalIdSearchDetail.ExternalId = "TEST";

            DocumentTypeExternalIdSortDetail objDocumentTypeExternalIdSortDetail = new DocumentTypeExternalIdSortDetail();
            objDocumentTypeExternalIdSortDetail.Column = DocumentTypeExternalIdSortColumn.DocumentTypeName;
            objDocumentTypeExternalIdSortDetail.Order = SortOrder.Descending;

            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>()))
                .Returns(IenumDocumentTypeExternalIdObjectModel);
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(0,0, It.IsAny<DocumentTypeExternalIdSearchDetail>(), It.IsAny<DocumentTypeExternalIdSortDetail>()))
                .Returns(CreateList());
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());

            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;

            List<DocumentTypeExternalIdViewModel> lstViewModelExpected = new List<DocumentTypeExternalIdViewModel>();
            DocumentTypeExternalIdViewModel objviewModel = new DocumentTypeExternalIdViewModel();
            objviewModel.DocumentTypeId = 1;
            objviewModel.DocumentTypeName = "Test_Doc";
            objviewModel.ExternalId = "TEST";
            objviewModel.IsPrimary = "True";
            objviewModel.ModifiedBy = 0;
            objviewModel.ModifiedByName = null;
            objviewModel.ModifiedDate = null;
            lstViewModelExpected.Add(objviewModel);

          
            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
              new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetAllDocumentTypeExtrenalIdDetails("1", "TEST");

            //Verify and Assert
            ValidateData(lstViewModelExpected, result);
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithElseForModifiedBy
        /// <summary>
        /// GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithElseForModifiedBy
        /// </summary>
        [TestMethod]
        public void GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithElseForModifiedBy()
        {
            //Arrange

            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test_Doc";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 1;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            DocumentTypeExternalIdSearchDetail objDocumentTypeExternalIdSearchDetail = new DocumentTypeExternalIdSearchDetail();
            objDocumentTypeExternalIdSearchDetail.DocumentTypeID = 1;
            objDocumentTypeExternalIdSearchDetail.ExternalId = "TEST";

            List<DocumentTypeExternalIdViewModel> lstViewModelExpected = new List<DocumentTypeExternalIdViewModel>();
            DocumentTypeExternalIdViewModel objviewModel = new DocumentTypeExternalIdViewModel();
            objviewModel.DocumentTypeId = 1;
            objviewModel.DocumentTypeName = "Test_Doc";
            objviewModel.ExternalId = "TEST";
            objviewModel.IsPrimary = "True";
            objviewModel.ModifiedBy = 0;
            objviewModel.ModifiedByName = null;
            objviewModel.ModifiedDate = null;
            lstViewModelExpected.Add(objviewModel);

            DocumentTypeExternalIdSortDetail objDocumentTypeExternalIdSortDetail = new DocumentTypeExternalIdSortDetail();
            objDocumentTypeExternalIdSortDetail.Column = DocumentTypeExternalIdSortColumn.DocumentTypeName;
            objDocumentTypeExternalIdSortDetail.Order = SortOrder.Descending;

            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>()))
                .Returns(IenumDocumentTypeExternalIdObjectModel);
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<DocumentTypeExternalIdSearchDetail>(), It.IsAny<DocumentTypeExternalIdSortDetail>()))
               .Returns(CreateList());
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());

            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();

            

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
              new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetAllDocumentTypeExtrenalIdDetails("1", "TEST");

            //Verify and Assert
            ValidateData(lstViewModelExpected, result);
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithEmptyDocumentId
        /// <summary>
        /// GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithEmptyDocumentId
        /// </summary>
        [TestMethod]
        public void GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithEmptyDocumentId()
        {
            //Arrange
            

            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test_Doc";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 1;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            DocumentTypeExternalIdSearchDetail objDocumentTypeExternalIdSearchDetail = new DocumentTypeExternalIdSearchDetail();
            objDocumentTypeExternalIdSearchDetail.DocumentTypeID = 1;
            objDocumentTypeExternalIdSearchDetail.ExternalId = "TEST";

            DocumentTypeExternalIdSortDetail objDocumentTypeExternalIdSortDetail = new DocumentTypeExternalIdSortDetail();
            objDocumentTypeExternalIdSortDetail.Column = DocumentTypeExternalIdSortColumn.DocumentTypeName;
            objDocumentTypeExternalIdSortDetail.Order = SortOrder.Descending;

            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>()))
                .Returns(IenumDocumentTypeExternalIdObjectModel);
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<DocumentTypeExternalIdSearchDetail>(), It.IsAny<DocumentTypeExternalIdSortDetail>()))
                .Returns(CreateList());
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());

            List<DocumentTypeExternalIdViewModel> lstViewModelExpected = new List<DocumentTypeExternalIdViewModel>();
            DocumentTypeExternalIdViewModel objviewModel = new DocumentTypeExternalIdViewModel();
            objviewModel.DocumentTypeId = 1;
            objviewModel.DocumentTypeName = "Test_Doc";
            objviewModel.ExternalId = "TEST";
            objviewModel.IsPrimary = "True";
            objviewModel.ModifiedBy = 0;
            objviewModel.ModifiedByName = null;
            objviewModel.ModifiedDate = null;
            lstViewModelExpected.Add(objviewModel);

            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;

            
            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
              new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetAllDocumentTypeExtrenalIdDetails(string.Empty, "TEST");

            //Verify and Assert
            ValidateData(lstViewModelExpected, result);
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithStringDocumentId
        /// <summary>
        /// GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithStringDocumentId
        /// </summary>
        [TestMethod]
        public void GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithStringDocumentId()
        {
            //Arrange

            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test_Doc";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 1;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            DocumentTypeExternalIdSearchDetail objDocumentTypeExternalIdSearchDetail = new DocumentTypeExternalIdSearchDetail();
            objDocumentTypeExternalIdSearchDetail.DocumentTypeID = 1;
            objDocumentTypeExternalIdSearchDetail.ExternalId = "TEST";

            DocumentTypeExternalIdSortDetail objDocumentTypeExternalIdSortDetail = new DocumentTypeExternalIdSortDetail();
            objDocumentTypeExternalIdSortDetail.Column = DocumentTypeExternalIdSortColumn.DocumentTypeName;
            objDocumentTypeExternalIdSortDetail.Order = SortOrder.Descending;

            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>()))
                .Returns(IenumDocumentTypeExternalIdObjectModel);
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<DocumentTypeExternalIdSearchDetail>(), It.IsAny<DocumentTypeExternalIdSortDetail>()))
                .Returns(CreateList());
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());

            List<DocumentTypeExternalIdViewModel> lstViewModelExpected = new List<DocumentTypeExternalIdViewModel>();
            DocumentTypeExternalIdViewModel objviewModel = new DocumentTypeExternalIdViewModel();
            objviewModel.DocumentTypeId = 1;
            objviewModel.DocumentTypeName = "Test_Doc";
            objviewModel.ExternalId = "TEST";
            objviewModel.IsPrimary = "True";
            objviewModel.ModifiedBy = 0;
            objviewModel.ModifiedByName = null;
            objviewModel.ModifiedDate = null;
            lstViewModelExpected.Add(objviewModel);

            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;

            

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
              new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetAllDocumentTypeExtrenalIdDetails("Test", "TEST");

            //Verify and Assert
            ValidateData(lstViewModelExpected, result);
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithEmptyExternalId
        /// <summary>
        /// GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithEmptyExternalId
        /// </summary>
        [TestMethod]
        public void GetAllDocumentTypeExtrenalIdDetails_Action_Calls_DocumentTypeExternalIdController_WithEmptyExternalId()
        {
            //Arrange
            

            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test_Doc";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 1;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            DocumentTypeExternalIdSearchDetail objDocumentTypeExternalIdSearchDetail = new DocumentTypeExternalIdSearchDetail();
            objDocumentTypeExternalIdSearchDetail.DocumentTypeID = 1;
            objDocumentTypeExternalIdSearchDetail.ExternalId = "TEST";

            DocumentTypeExternalIdSortDetail objDocumentTypeExternalIdSortDetail = new DocumentTypeExternalIdSortDetail();
            objDocumentTypeExternalIdSortDetail.Column = DocumentTypeExternalIdSortColumn.DocumentTypeName;
            objDocumentTypeExternalIdSortDetail.Order = SortOrder.Descending;

            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>()))
               .Returns(IenumDocumentTypeExternalIdObjectModel);
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<DocumentTypeExternalIdSearchDetail>(), It.IsAny<DocumentTypeExternalIdSortDetail>()))
                .Returns(CreateList());
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());

            List<DocumentTypeExternalIdViewModel> lstViewModelExpected = new List<DocumentTypeExternalIdViewModel>();
            DocumentTypeExternalIdViewModel objviewModel = new DocumentTypeExternalIdViewModel();
            objviewModel.DocumentTypeId = 1;
            objviewModel.DocumentTypeName = "Test_Doc";
            objviewModel.ExternalId = "TEST";
            objviewModel.IsPrimary = "True";
            objviewModel.ModifiedBy = 0;
            objviewModel.ModifiedByName = null;
            objviewModel.ModifiedDate = null;
            lstViewModelExpected.Add(objviewModel);

            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;

            
            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
              new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetAllDocumentTypeExtrenalIdDetails("1", string.Empty);

            //Verify and Assert
            ValidateData(lstViewModelExpected, result);
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllDocumentTypeExtrenalIdDetailsWithEmptyModifiedBy_Action_Calls_DocumentTypeExternalIdController
        /// <summary>
        /// GetAllDocumentTypeExtrenalIdDetailsWithEmptyModifiedBy_Action_Calls_DocumentTypeExternalIdController
        /// </summary>
        [TestMethod]
        public void GetAllDocumentTypeExtrenalIdDetailsWithEmptyModifiedBy_Action_Calls_DocumentTypeExternalIdController()
        {
            //Arrange
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities())
              .Returns(CreateListDocumentType());

            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test_Doc";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 0;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            DocumentTypeExternalIdSearchDetail objDocumentTypeExternalIdSearchDetail = new DocumentTypeExternalIdSearchDetail();
            objDocumentTypeExternalIdSearchDetail.DocumentTypeID = 1;
            objDocumentTypeExternalIdSearchDetail.ExternalId = "TEST";

            DocumentTypeExternalIdSortDetail objDocumentTypeExternalIdSortDetail = new DocumentTypeExternalIdSortDetail();
            objDocumentTypeExternalIdSortDetail.Column = DocumentTypeExternalIdSortColumn.DocumentTypeName;
            objDocumentTypeExternalIdSortDetail.Order = SortOrder.Descending;

            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<DocumentTypeExternalIdSearchDetail>()))
              .Returns(IenumDocumentTypeExternalIdObjectModel);
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch(0, 0, It.IsAny<DocumentTypeExternalIdSearchDetail>(), It.IsAny<DocumentTypeExternalIdSortDetail>()))
                .Returns(CreateList());
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());

            List<DocumentTypeExternalIdViewModel> lstViewModelExpected = new List<DocumentTypeExternalIdViewModel>();
            DocumentTypeExternalIdViewModel objviewModel = new DocumentTypeExternalIdViewModel();
            objviewModel.DocumentTypeId = 1;
            objviewModel.DocumentTypeName = "Test_Doc";
            objviewModel.ExternalId = "TEST";
            objviewModel.IsPrimary = "True";
            objviewModel.ModifiedBy = 0;
            objviewModel.ModifiedByName = null;
            objviewModel.ModifiedDate = null;
            lstViewModelExpected.Add(objviewModel);

            IEnumerable<UserObjectModel> IEnumUser = Enumerable.Empty<UserObjectModel>();
            List<UserObjectModel> lstUserObjectModel = new List<UserObjectModel>();
            UserObjectModel objUserObjectModel = new UserObjectModel();
            objUserObjectModel.UserId = 1;
            objUserObjectModel.UserName = "Test_UserName";
            lstUserObjectModel.Add(objUserObjectModel);
            IEnumUser = lstUserObjectModel;


            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
              new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetAllDocumentTypeExtrenalIdDetails("1", "TEST");

            //Verify and Assert
            ValidateData(lstViewModelExpected, result);
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region Index_Returns_ActionResult
        /// <summary>
        /// Index_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void Index_Returns_ActionResult()
        {
            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
        new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.Index();

            var result1 = result as ViewResult;
            Assert.AreEqual("DocumentTypeExternalId", result1.ViewName);

            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetDocumentTypes_Returns_JsonResult
        /// <summary>
        /// GetDocumentTypes_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetDocumentTypes_Returns_JsonResult()
        {
            //Arrange
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateList());
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetDocumentTypes();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "Test_Doc" , Value = "1" });
            ValidateDisplayValuePair(lstexpected, result);
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            mockDocumentTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetDocumentTypes_Returns_JsonResult_NoResult
        /// <summary>
        /// GetDocumentTypes_Returns_JsonResult_NoResult
        /// </summary>
        [TestMethod]
        public void GetDocumentTypes_Returns_JsonResult_NoResult()
        {
            //Arrange
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetAllEntities()).Returns(IenumDocumentTypeExternalIdObjModel);
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetDocumentTypes();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            mockDocumentTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetExternalIds_Returns_JsonResult
        /// <summary>
        /// GetExternalIds_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetExternalIds_Returns_JsonResult()
        {
            //Arrange
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateList());

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetExternalIds();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            lstexpected.Add(new DisplayValuePair() { Display = "TEST", Value = "TEST" });
            ValidateDisplayValuePair(lstexpected, result);
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetExternalIds_Returns_JsonResult_NoResult
        /// <summary>
        /// GetExternalIds_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetExternalIds_Returns_JsonResult_NoResult()
        {
            //Arrange
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetAllEntities()).Returns(IenumDocumentTypeExternalIdObjModel);

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.GetExternalIds();

            //Verify and Assert
            List<DisplayValuePair> lstexpected = new List<DisplayValuePair>();
            ValidateDisplayValuePair(lstexpected, result);
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region EditDocumentTypeExternalId_Returns_ActionResult_GetMethod
        /// <summary>
        /// EditDocumentTypeExternalId_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void EditDocumentTypeExternalId_Returns_ActionResult_GetMethod()
        {
            //Arrange
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 1;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>())).Returns(IenumDocumentTypeExternalIdObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<UserSearchDetail>())).Returns(CreateListUser());

            //Act
            

            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.EditDocumentTypeExternalId(1, "TEST");

            List<DisplayValuePair> lstDocumentType = new List<DisplayValuePair>();
            lstDocumentType.Add(new DisplayValuePair() { Display = "--Please select Document Type--", Value = "-1" });
            lstDocumentType.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });


            EditDocumentTypeExternalIdViewModel objExpected = new EditDocumentTypeExternalIdViewModel()
            {
                DocumentType = lstDocumentType,
                ExternalId = "TEST",
                IsPrimary = true,
                ModifiedBy = 1,
                ModifiedByName = "Test_UserName",
                ModifiedDate = "",
                SelectedDocumentTypeId = 1,
                SuccessOrFailedMessage = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditDocumentTypeExternalIdViewModel;
            ValidateViewModelData<EditDocumentTypeExternalIdViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditDocumentTypeExternalIdViewModel));
            //Verify and Assert
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditDocumentTypeExternalId_Returns_ActionResult_GetMethod_ElseForModifiedBy
        /// <summary>
        /// EditDocumentTypeExternalId_Returns_ActionResult_GetMethod_ElseForModifiedBy
        /// </summary>
        [TestMethod]
        public void EditDocumentTypeExternalId_Returns_ActionResult_GetMethod_ElseForModifiedBy()
        {
            //Arrange
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            IEnumerable<UserObjectModel> IEnumUserObjModel = Enumerable.Empty<UserObjectModel>();
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 1;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>())).Returns(IenumDocumentTypeExternalIdObjectModel);
            mockUserFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<UserSearchDetail>())).Returns(IEnumUserObjModel);

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.EditDocumentTypeExternalId(1, "TEST");


            List<DisplayValuePair> lstDocumentType = new List<DisplayValuePair>();
            lstDocumentType.Add(new DisplayValuePair() { Display = "--Please select Document Type--", Value = "-1" });
            lstDocumentType.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });


            EditDocumentTypeExternalIdViewModel objExpected = new EditDocumentTypeExternalIdViewModel()
            {
                DocumentType = lstDocumentType,
                ExternalId = "TEST",
                IsPrimary = true,
                ModifiedBy = 1,
                ModifiedByName = "1",
                ModifiedDate = "",
                SelectedDocumentTypeId = 1,
                SuccessOrFailedMessage = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditDocumentTypeExternalIdViewModel;
            ValidateViewModelData<EditDocumentTypeExternalIdViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditDocumentTypeExternalIdViewModel));
            //Verify and Assert
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            mockUserFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditDocumentTypeExternalId_Returns_ActionResult_GetMethod_WithEmptyModifiedBy
        /// <summary>
        /// EditDocumentTypeExternalId_Returns_ActionResult_GetMethod
        /// </summary>
        [TestMethod]
        public void EditDocumentTypeExternalId_Returns_ActionResult_GetMethod_WithEmptyModifiedBy()
        {
            //Arrange
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            List<DocumentTypeExternalIdObjectModel> lstDocumentTypeExternalIdObjectModel = new List<DocumentTypeExternalIdObjectModel>();

            DocumentTypeExternalIdObjectModel objDocumentTypeExternalIdObjectModel = new DocumentTypeExternalIdObjectModel();
            objDocumentTypeExternalIdObjectModel.DocumentTypeId = 1;
            objDocumentTypeExternalIdObjectModel.ExternalId = "TEST";
            objDocumentTypeExternalIdObjectModel.DocumentTypeName = "Test";
            objDocumentTypeExternalIdObjectModel.IsPrimary = true;
            objDocumentTypeExternalIdObjectModel.ModifiedBy = 0;
            lstDocumentTypeExternalIdObjectModel.Add(objDocumentTypeExternalIdObjectModel);
            IenumDocumentTypeExternalIdObjectModel = lstDocumentTypeExternalIdObjectModel;

            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>())).Returns(IenumDocumentTypeExternalIdObjectModel);
            

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.EditDocumentTypeExternalId(1, "TEST");

            List<DisplayValuePair> lstDocumentType = new List<DisplayValuePair>();
            lstDocumentType.Add(new DisplayValuePair() { Display = "--Please select Document Type--", Value = "-1" });
            lstDocumentType.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });


            EditDocumentTypeExternalIdViewModel objExpected = new EditDocumentTypeExternalIdViewModel()
            {
                DocumentType = lstDocumentType,
                ExternalId = "TEST",
                IsPrimary = true,
                ModifiedBy = 0,
                ModifiedByName = "",
                ModifiedDate = "",
                SelectedDocumentTypeId = 1,
                SuccessOrFailedMessage = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditDocumentTypeExternalIdViewModel;
            ValidateViewModelData<EditDocumentTypeExternalIdViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditDocumentTypeExternalIdViewModel));
            //Verify and Assert
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditDocumentTypeExternalId_Returns_ActionResult_GetMethod_WithNoResult
        /// <summary>
        /// EditDocumentTypeExternalId_Returns_ActionResult_GetMethod_WithNoResult
        /// </summary>
        [TestMethod]
        public void EditDocumentTypeExternalId_Returns_ActionResult_GetMethod_WithNoResult()
        {
            //Arrange
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjectModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();

            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType());
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>())).Returns(IenumDocumentTypeExternalIdObjectModel);

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.EditDocumentTypeExternalId(1, "TEST");

            List<DisplayValuePair> lstDocumentType = new List<DisplayValuePair>();
            lstDocumentType.Add(new DisplayValuePair() { Display = "--Please select Document Type--", Value = "-1" });
            lstDocumentType.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });


            EditDocumentTypeExternalIdViewModel objExpected = new EditDocumentTypeExternalIdViewModel()
            {
                DocumentType = lstDocumentType,
                ExternalId = "",
                IsPrimary = false,
                ModifiedBy = null,
                ModifiedByName = null,
                ModifiedDate =null,
                SelectedDocumentTypeId = -1,
                SuccessOrFailedMessage = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditDocumentTypeExternalIdViewModel;
            ValidateViewModelData<EditDocumentTypeExternalIdViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditDocumentTypeExternalIdViewModel));
            //Verify and Assert
            mockDocumentTypeFactoryCache.VerifyAll();
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditDocumentTypeExternalId_Returns_ActionResult_PostMethod
        /// <summary>
        /// EditDocumentTypeExternalId_Returns_ActionResult_PostMethod
        /// </summary>
        [TestMethod]
        public void EditDocumentTypeExternalId_Returns_ActionResult_PostMethod()
        {
            UserObjectModel userObjectModel = new UserObjectModel();
            
            //Arrange
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.SaveEntity(It.IsAny<DocumentTypeExternalIdObjectModel>(), It.IsAny<int>()));
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateListDocumentType);
            EditDocumentTypeExternalIdViewModel objEditDocumentTypeExternalIdViewModel = new EditDocumentTypeExternalIdViewModel();
            objEditDocumentTypeExternalIdViewModel.ModifiedBy = 1;
            objEditDocumentTypeExternalIdViewModel.ModifiedByName = "TestName";
            objEditDocumentTypeExternalIdViewModel.ModifiedDate = "27/11/2015";

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.EditDocumentTypeExternalId(new EditDocumentTypeExternalIdViewModel());

            List<DisplayValuePair> lstDocumentType = new List<DisplayValuePair>();
            lstDocumentType.Add(new DisplayValuePair() { Display = "--Please select Document Type--", Value = "-1" });
            lstDocumentType.Add(new DisplayValuePair() { Display = "Test_Doc", Value = "1" });


            EditDocumentTypeExternalIdViewModel objExpected = new EditDocumentTypeExternalIdViewModel()
            {
                DocumentType = lstDocumentType,
                ExternalId = null,
                IsPrimary = false,
                ModifiedBy = null,
                ModifiedByName = null,
                ModifiedDate = null,
                SelectedDocumentTypeId = 0,
                SuccessOrFailedMessage = null

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditDocumentTypeExternalIdViewModel;
            ValidateViewModelData<EditDocumentTypeExternalIdViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditDocumentTypeExternalIdViewModel));
            //Verify and Assert
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            mockDocumentTypeFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditDocumentTypeExternalId_Returns_ActionResult_PostMethod_Handles_Exception
        /// <summary>
        /// EditDocumentTypeExternalId_Returns_ActionResult_PostMethod_Handles_Exception
        /// </summary>
        [TestMethod]
        public void EditDocumentTypeExternalId_Returns_ActionResult_PostMethod_Handles_Exception()
        {
            //Arrange
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.SaveEntity(It.IsAny<DocumentTypeExternalIdObjectModel>(), It.IsAny<int>()));
            mockDocumentTypeFactoryCache.Setup(x => x.GetAllEntities()).Throws(new Exception());

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.EditDocumentTypeExternalId(new EditDocumentTypeExternalIdViewModel());

            List<DisplayValuePair> lstDocumentType = new List<DisplayValuePair>();
            lstDocumentType.Add(new DisplayValuePair() { Display = "--Please select Document Type--", Value = "-1" });

            EditDocumentTypeExternalIdViewModel objExpected = new EditDocumentTypeExternalIdViewModel()
            {
                DocumentType = lstDocumentType,
                ExternalId = null,
                IsPrimary = false,
                ModifiedBy = null,
                ModifiedByName = null,
                ModifiedDate = null,
                SelectedDocumentTypeId = 0,
                SuccessOrFailedMessage = "Exception of type 'System.Exception' was thrown."

            };

            var result1 = result as ViewResult;
            var viewModel = result1.Model as EditDocumentTypeExternalIdViewModel;
            ValidateViewModelData<EditDocumentTypeExternalIdViewModel>(viewModel, objExpected);
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result1.Model, typeof(EditDocumentTypeExternalIdViewModel));
            //Verify and Assert
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region DisableDocumentTypeExtrenalId_Returns_JsonResult
        /// <summary>
        /// DisableDocumentTypeExtrenalId_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DisableDocumentTypeExtrenalId_Returns_JsonResult()
        {
            //Arrange
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<DocumentTypeExternalIdObjectModel>()));

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.DisableDocumentTypeExtrenalId(1, "AR");

            //Verify and Assert
            Assert.AreEqual(string.Empty, result.Data);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckCombinationDataAlreadyExists_Returns_JsonResult
        /// <summary>
        /// CheckCombinationDataAlreadyExists_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckCombinationDataAlreadyExists_Returns_JsonResult()
        {
            //Arrange
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>()))
            .Returns(CreateList);

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.CheckCombinationDataAlreadyExists(1, "AR");

            //Verify and Assert
            Assert.AreEqual(true, result.Data);
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckCombinationDataAlreadyExists_Returns_JsonResult_False
        /// <summary>
        /// CheckCombinationDataAlreadyExists_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckCombinationDataAlreadyExists_Returns_JsonResult_False()
        {
            //Arrange
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>()))
            .Returns(IenumDocumentTypeExternalIdObjModel);

            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.CheckCombinationDataAlreadyExists(1, string.Empty);

            //Verify and Assert
            Assert.AreEqual(false, result.Data);
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
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
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>()))
                          .Returns(CreateList());
            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.CheckExternalIdAlreadyExists("AR");

            //Verify and Assert
            Assert.AreEqual(true, result.Data);
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckExternalIdAlreadyExists_Returns_JsonResult_EmptyExternalId
        /// <summary>
        /// CheckExternalIdAlreadyExists_Returns_JsonResult_InvalidExternalId
        /// </summary>
        [TestMethod]
        public void CheckExternalIdAlreadyExists_Returns_JsonResult_EmptyExternalId()
        {
            //Arrange
            IEnumerable<DocumentTypeExternalIdObjectModel> IenumDocumentTypeExternalIdObjModel = Enumerable.Empty<DocumentTypeExternalIdObjectModel>();
            mockDocumentTypeExternalIdFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<DocumentTypeExternalIdSearchDetail>()))
                          .Returns(IenumDocumentTypeExternalIdObjModel);
            //Act
            DocumentTypeExternalIdController objDocumentTypeExternalIdController =
         new DocumentTypeExternalIdController(mockDocumentTypeExternalIdFactoryCache.Object, mockUserFactoryCache.Object, mockDocumentTypeFactoryCache.Object);
            var result = objDocumentTypeExternalIdController.CheckExternalIdAlreadyExists(string.Empty);

            //Verify and Assert
            Assert.AreEqual(false, result.Data);
            mockDocumentTypeExternalIdFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
    }
}
