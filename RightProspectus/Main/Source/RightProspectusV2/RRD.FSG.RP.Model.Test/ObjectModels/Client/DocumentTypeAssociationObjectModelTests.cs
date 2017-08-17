using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Entities.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Test.ObjectModels.Client
{
    /// <summary>
    /// Test class for DocumentTypeAssociationObjectModel class
    /// </summary>
    [TestClass]
    public class DocumentTypeAssociationObjectModelTests
    {
        #region CompareTo_Returns_Int
        /// <summary>
        /// CompareTo_Returns_Int
        /// </summary>
        [TestMethod]
        public void CompareTo_Returns_Int()
        {
            //Arrange

            //Act
            DocumentTypeAssociationObjectModel objModel = new DocumentTypeAssociationObjectModel();
            objModel.DocumentTypeId = 1;
            var result = objModel.CompareTo(new DocumentTypeAssociationObjectModel(){DocumentTypeId=1});

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0,result);
        }
        #endregion

        #region Get_Set_DocumentTypeAssociationObjectModelTests_DocumentTypeAssociationId
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_ClientDocumentGroupId
        /// </summary>
         [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_DocumentTypeAssociationId()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { DocumentTypeAssociationId = 3 };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { DocumentTypeAssociationId = 30 };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { DocumentTypeAssociationId = 13 };
            Assert.AreEqual(3, objModel1.DocumentTypeAssociationId);
            Assert.AreEqual(30, objModel2.DocumentTypeAssociationId);
            Assert.AreEqual(13, objModel3.DocumentTypeAssociationId);
        }
        #endregion
        #region Get_Set_DocumentTypeAssociationObjectModelTests_DocumentTypeId
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_DocumentTypeId
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_DocumentTypeId()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { DocumentTypeId = 3 };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { DocumentTypeId = 30 };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { DocumentTypeId = 13 };
            Assert.AreEqual(3, objModel1.DocumentTypeId);
            Assert.AreEqual(30, objModel2.DocumentTypeId);
            Assert.AreEqual(13, objModel3.DocumentTypeId);
        }
        #endregion
        #region Get_Set_DocumentTypeAssociationObjectModelTests_SiteId
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_SiteId
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_SiteId()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { SiteId = 3 };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { SiteId = 30 };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { SiteId = 13 };
            Assert.AreEqual(3, objModel1.SiteId);
            Assert.AreEqual(30, objModel2.SiteId);
            Assert.AreEqual(13, objModel3.SiteId);
        }
        #endregion
        #region Get_Set_DocumentTypeAssociationObjectModelTests_TaxonomyAssociationId
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_TaxonomyAssociationId
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_TaxonomyAssociationId()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { TaxonomyAssociationId = 3 };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { TaxonomyAssociationId = 30 };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { TaxonomyAssociationId = 13 };
            Assert.AreEqual(3, objModel1.TaxonomyAssociationId);
            Assert.AreEqual(30, objModel2.TaxonomyAssociationId);
            Assert.AreEqual(13, objModel3.TaxonomyAssociationId);
        }
        #endregion
        #region Get_Set_DocumentTypeAssociationObjectModelTests_Order
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_Order
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_Order()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { Order = 3 };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { Order = 30 };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { Order = 13 };
            Assert.AreEqual(3, objModel1.Order);
            Assert.AreEqual(30, objModel2.Order);
            Assert.AreEqual(13, objModel3.Order);
        }
        #endregion
        #region Get_Set_DocumentTypeAssociationObjectModelTests_HeaderText
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_HeaderText
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_HeaderText()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { HeaderText = "Text1" };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { HeaderText = "Text2" };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { HeaderText = "Text3" };
            Assert.AreEqual("Text1", objModel1.HeaderText);
            Assert.AreEqual("Text2", objModel2.HeaderText);
            Assert.AreEqual("Text3", objModel3.HeaderText);
        }
        #endregion
        #region Get_Set_DocumentTypeAssociationObjectModelTests_LinkText
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_LinkText
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_LinkText()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { LinkText = "Text1" };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { LinkText = "Text2" };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { LinkText = "Text3" };
            Assert.AreEqual("Text1", objModel1.LinkText);
            Assert.AreEqual("Text2", objModel2.LinkText);
            Assert.AreEqual("Text3", objModel3.LinkText);
        }
        #endregion
        #region Get_Set_DocumentTypeAssociationObjectModelTests_DescriptionOverride
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_DescriptionOverride
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_DescriptionOverride()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { DescriptionOverride = "Text1" };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { DescriptionOverride = "Text2" };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { DescriptionOverride = "Text3" };
            Assert.AreEqual("Text1", objModel1.DescriptionOverride);
            Assert.AreEqual("Text2", objModel2.DescriptionOverride);
            Assert.AreEqual("Text3", objModel3.DescriptionOverride);
        }
        #endregion
        #region Get_Set_DocumentTypeAssociationObjectModelTests_CssClass
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_CssClass
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_CssClass()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { CssClass = "Text1" };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { CssClass = "Text2" };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { CssClass = "Text3" };
            Assert.AreEqual("Text1", objModel1.CssClass);
            Assert.AreEqual("Text2", objModel2.CssClass);
            Assert.AreEqual("Text3", objModel3.CssClass);
        }
        #endregion
        #region Get_Set_DocumentTypeAssociationObjectModelTests_MarketId
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_MarketId
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeAssociationObjectModelTests_MarketId()
        {
            DocumentTypeAssociationObjectModel objModel1 = new DocumentTypeAssociationObjectModel { MarketId = "Text1" };
            DocumentTypeAssociationObjectModel objModel2 = new DocumentTypeAssociationObjectModel { MarketId = "Text2" };
            DocumentTypeAssociationObjectModel objModel3 = new DocumentTypeAssociationObjectModel { MarketId = "Text3" };
            Assert.AreEqual("Text1", objModel1.MarketId);
            Assert.AreEqual("Text2", objModel2.MarketId);
            Assert.AreEqual("Text3", objModel3.MarketId);
        }
        #endregion
        
        
        
    }
}
