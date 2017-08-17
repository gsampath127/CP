using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
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
    ///  Test class for ApproveProofingController class
    /// </summary>
    [TestClass]
    public class ApproveProofingControllerTests
    {
        Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>> mockSitefactoryCache;
        Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>> mockTemplatePageFactoryCache;
        Mock<IFactory<ApproveProofingObjectModel, int>> mockApproveProofingFactory;
        Mock<IFactory<TaxonomyAssociationObjectModel, int>> mockTaxonomyAssociationFactory;
        Mock<IFactory<DocumentTypeAssociationObjectModel, int>> mockDocTypeAssociationFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            mockSitefactoryCache = new Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>>();
            mockTemplatePageFactoryCache = new Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>();
            mockApproveProofingFactory = new Mock<IFactory<ApproveProofingObjectModel, int>>();
            mockTaxonomyAssociationFactory = new Mock<IFactory<TaxonomyAssociationObjectModel, int>>();
            mockDocTypeAssociationFactory = new Mock<IFactory<DocumentTypeAssociationObjectModel, int>>();
        }

        #region ReturnValues
        private IEnumerable<SiteObjectModel> CreateSiteList()
        {
            IEnumerable<SiteObjectModel> IEnumSite = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lstSiteObjectModel = new List<SiteObjectModel>();
            SiteObjectModel objSiteObjectModel = new SiteObjectModel();
            objSiteObjectModel.SiteID = 1;
            objSiteObjectModel.DefaultPageName = "Test";
            objSiteObjectModel.Name = "Test_Site";
            objSiteObjectModel.DefaultPageId = 1;
            objSiteObjectModel.TemplateId = 1;
            objSiteObjectModel.Description = "Test_Description";
            lstSiteObjectModel.Add(objSiteObjectModel);
            IEnumSite = lstSiteObjectModel;
            return IEnumSite;
        }

        private IEnumerable<TaxonomyAssociationObjectModel> CreateTaxonomyAssociationList()
        {
            IEnumerable<TaxonomyAssociationObjectModel> IEnumTaxonomyAssociation = Enumerable.Empty<TaxonomyAssociationObjectModel>();
            List<TaxonomyAssociationObjectModel> lstTaxonomyAssociationObjectModel = new List<TaxonomyAssociationObjectModel>();
            TaxonomyAssociationObjectModel objTaxonomyAssociationObjectModel = new TaxonomyAssociationObjectModel();
            objTaxonomyAssociationObjectModel.TaxonomyAssociationId = 1;
            objTaxonomyAssociationObjectModel.Level = 1;
            objTaxonomyAssociationObjectModel.TaxonomyId = 1;
            objTaxonomyAssociationObjectModel.SiteId = 1;
            lstTaxonomyAssociationObjectModel.Add(objTaxonomyAssociationObjectModel);
            IEnumTaxonomyAssociation = lstTaxonomyAssociationObjectModel;
            return IEnumTaxonomyAssociation;
        }

        private IEnumerable<DocumentTypeAssociationObjectModel> CreateDocTypeAssociationList()
        {
            IEnumerable<DocumentTypeAssociationObjectModel> IEnumDocTypeAssociation = Enumerable.Empty<DocumentTypeAssociationObjectModel>();
            List<DocumentTypeAssociationObjectModel> lstDocTypeAssociationObjectModel = new List<DocumentTypeAssociationObjectModel>();
            DocumentTypeAssociationObjectModel objDocTypeAssociationObjectModel = new DocumentTypeAssociationObjectModel();
            objDocTypeAssociationObjectModel.SiteId = 1;
            objDocTypeAssociationObjectModel.DocumentTypeId = 1;
            objDocTypeAssociationObjectModel.DocumentTypeAssociationId = 1;
            lstDocTypeAssociationObjectModel.Add(objDocTypeAssociationObjectModel);
            IEnumDocTypeAssociation = lstDocTypeAssociationObjectModel;
            return IEnumDocTypeAssociation;
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
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
                 mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.List();

            // Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("ApproveProofing", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetHostedSiteProofingURL_Returns_JsonResult_TAL
        /// <summary>
        /// GetHostedSiteProofingURL_Returns_JsonResult_TAL
        /// </summary>
        [TestMethod]
        public void GetHostedSiteProofingURL_Returns_JsonResult_TAL()
        {
            //Arrange
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "TAL",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };

            mockSitefactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockTaxonomyAssociationFactory.Setup(x => x.GetAllEntities()).Returns(CreateTaxonomyAssociationList);
            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.GetHostedSiteProofingURL();

            // Verify and Assert
            mockSitefactoryCache.VerifyAll();
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, "http://localhost:54346/?site=");
            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }
        #endregion

        #region GetHostedSiteProofingURL_Returns_JsonResult_TAHD
        /// <summary>
        /// GetHostedSiteProofingURL_Returns_JsonResult_TAHD
        /// </summary>
        [TestMethod]
        public void GetHostedSiteProofingURL_Returns_JsonResult_TAHD()
        {
            //Arrange

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "TAHD",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };


            //IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            //List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            //TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();
            //objTemplatePageObjectModel.PageID = 1;
            //objTemplatePageObjectModel.TemplateID = 1;
            //objTemplatePageObjectModel.PageName = "TAHD";
            //lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            //IEnumTemplatePage = lstTemplatePageObjectModel;

            mockSitefactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objSiteObjectModel);
            //mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(IEnumTemplatePage);
            //mockTaxonomyAssociationFactory.Setup(x => x.GetAllEntities()).Returns(CreateTaxonomyAssociationList());

            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.GetHostedSiteProofingURL();

            // Verify and Assert
            mockSitefactoryCache.VerifyAll();
            //mockTemplatePageFactoryCache.VerifyAll();
            //mockTaxonomyAssociationFactory.VerifyAll();
            Assert.AreEqual(result.Data, "http://localhost:54346/?site=");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetHostedSiteProofingURL_Returns_JsonResult_TADF
        /// <summary>
        /// GetHostedSiteProofingURL_Returns_JsonResult_TADF
        /// </summary>
        [TestMethod]
        public void GetHostedSiteProofingURL_Returns_JsonResult_TADF()
        {
            //Arrange

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "TADF",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };
            mockSitefactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objSiteObjectModel);
            mockTaxonomyAssociationFactory.Setup(x => x.GetAllEntities()).Returns(CreateTaxonomyAssociationList);
            mockDocTypeAssociationFactory.Setup(x => x.GetAllEntities()).Returns(CreateDocTypeAssociationList());

            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.GetHostedSiteProofingURL();

            // Verify and Assert
            mockSitefactoryCache.VerifyAll();
            mockTaxonomyAssociationFactory.VerifyAll();
            mockDocTypeAssociationFactory.VerifyAll();
            Assert.AreEqual(result.Data, "http://localhost:54346//TADF/1/1?isInternalTAID=true&site=");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetHostedSiteProofingURL_Returns_JsonResult_TADF_TAxonomynull
        /// <summary>
        /// GetHostedSiteProofingURL_Returns_JsonResult_TADF_TAxonomynull
        /// </summary>
        [TestMethod]
        public void GetHostedSiteProofingURL_Returns_JsonResult_TADF_TAxonomynull()
        {
            //Arrange

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "TADF",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };

            mockSitefactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objSiteObjectModel);
            mockDocTypeAssociationFactory.Setup(x => x.GetAllEntities()).Returns(CreateDocTypeAssociationList());
            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.GetHostedSiteProofingURL();

            // Verify and Assert
            mockSitefactoryCache.VerifyAll();
            mockDocTypeAssociationFactory.VerifyAll();
            Assert.AreEqual(result.Data, "http://localhost:54346//TADF/0/1?isInternalTAID=true&site=");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetHostedSiteProofingURL_Returns_JsonResult_TADF_DocTypeNull
        /// <summary>
        /// GetHostedSiteProofingURL_Returns_JsonResult_TADF_DocTypeNull
        /// </summary>
        [TestMethod]
        public void GetHostedSiteProofingURL_Returns_JsonResult_TADF_DocTypeNull()
        {
            //Arrange

            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "TADF",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };
            mockSitefactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>())).Returns(objSiteObjectModel);
            mockTaxonomyAssociationFactory.Setup(x => x.GetAllEntities()).Returns(CreateTaxonomyAssociationList);

            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.GetHostedSiteProofingURL();

            // Verify and Assert
            mockSitefactoryCache.VerifyAll();
            mockTaxonomyAssociationFactory.VerifyAll();
            Assert.AreEqual(result.Data, "http://localhost:54346//TADF/1/0?isInternalTAID=true&site=");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetHostedSiteProofingURL_Returns_JsonResult_TAD
        /// <summary>
        /// GetHostedSiteProofingURL_Returns_JsonResult_TAD
        /// </summary>
        [TestMethod]
        public void GetHostedSiteProofingURL_Returns_JsonResult_TAD()
        {
            //Arrange
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "TAD",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };

            mockSitefactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);

            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.GetHostedSiteProofingURL();

            // Verify and Assert
            mockSitefactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, "http://localhost:54346/?site=");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetHostedSiteProofingURL_Returns_JsonResult_XBRL
        /// <summary>
        /// GetHostedSiteProofingURL_Returns_JsonResult_XBRL
        /// </summary>
        [TestMethod]
        public void GetHostedSiteProofingURL_Returns_JsonResult_XBRL()
        {
            //Arrange
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "XBRL",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };

            mockSitefactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            mockTaxonomyAssociationFactory.Setup(x => x.GetAllEntities()).Returns(CreateTaxonomyAssociationList);

            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.GetHostedSiteProofingURL();

            // Verify and Assert
            mockSitefactoryCache.VerifyAll();
            mockTemplatePageFactoryCache.VerifyAll();
            mockTaxonomyAssociationFactory.VerifyAll();
            Assert.AreEqual(result.Data, "http://localhost:54346//1?isInternalTAID=true&site=");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetHostedSiteProofingURL_Returns_JsonResult_XBRL_Taxnomy_Null
        /// <summary>
        /// GetHostedSiteProofingURL_Returns_JsonResult_XBRL_Taxnomy_Null
        /// </summary>
        [TestMethod]
        public void GetHostedSiteProofingURL_Returns_JsonResult_XBRL_Taxnomy_Null()
        {
            //Arrange
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "XBRL",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };

            mockSitefactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.GetHostedSiteProofingURL();

            // Verify and Assert
            mockSitefactoryCache.VerifyAll();
            mockTemplatePageFactoryCache.VerifyAll();
            mockTaxonomyAssociationFactory.VerifyAll();
            Assert.AreEqual(result.Data, "http://localhost:54346//0?isInternalTAID=true&site=");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetHostedSiteProofingURL_Returns_JsonResult_Default
        /// <summary>
        /// GetHostedSiteProofingURL_Returns_JsonResult_Default
        /// </summary>
        [TestMethod]
        public void GetHostedSiteProofingURL_Returns_JsonResult_Default()
        {
            //Arrange
            SiteObjectModel objSiteObjectModel = new SiteObjectModel()
            {
                SiteID = 1,
                DefaultPageName = "Test",
                DefaultPageId = 1,
                TemplateId = 1,
                Description = "Test_Description"

            };

            mockSitefactoryCache.Setup(x => x.GetEntityByKey(It.IsAny<int>()))
                .Returns(objSiteObjectModel);
            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
            mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.GetHostedSiteProofingURL();

            // Verify and Assert
            mockSitefactoryCache.VerifyAll();
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.AreEqual(result.Data, "http://localhost:54346/?site=");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region ApproveProofingChanges_Returns_Bool
        /// <summary>
        /// ApproveProofingChanges_Returns_Bool
        /// </summary>
        [TestMethod]
        public void ApproveProofingChanges_Returns_Bool()
        {
            //Arrange
            mockApproveProofingFactory.Setup(x => x.SaveEntity(It.IsAny<ApproveProofingObjectModel>(), It.IsAny<int>()));

            //Act
            ApproveProofingController objController = new ApproveProofingController(mockSitefactoryCache.Object, mockTemplatePageFactoryCache.Object,
           mockApproveProofingFactory.Object, mockTaxonomyAssociationFactory.Object, mockDocTypeAssociationFactory.Object);
            var result = objController.ApproveProofingChanges();

            // Verify and Assert
            Assert.AreEqual(true, result);
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
