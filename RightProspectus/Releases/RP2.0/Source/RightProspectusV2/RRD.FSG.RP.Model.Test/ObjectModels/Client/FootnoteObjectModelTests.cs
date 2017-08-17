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
    /// Test class for FootnoteObjectModel class
    /// </summary>
    [TestClass]
    public class FootnoteObjectModelTests
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
            FootnoteObjectModel objModel = new FootnoteObjectModel();
            objModel.Order = 1;
            var result = objModel.CompareTo(new FootnoteObjectModel() { Order = 1 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
        }
        #endregion
        #region Get_Set_FootnoteObjectModel_FootnoteId
        /// <summary>
        /// Get_Set_FootnoteObjectModel_FootnoteId
        /// </summary>
        [TestMethod]
        public void Get_Set_FootnoteObjectModel_FootnoteId()
        {
            FootnoteObjectModel objModel1 = new FootnoteObjectModel { FootnoteId = 2 };
            FootnoteObjectModel objModel2 = new FootnoteObjectModel { FootnoteId = 100 };
            FootnoteObjectModel objModel3 = new FootnoteObjectModel { FootnoteId = 9 };
            Assert.AreEqual(2, objModel1.FootnoteId);
            Assert.AreEqual(100, objModel2.FootnoteId);
            Assert.AreEqual(9, objModel3.FootnoteId);
        }
        #endregion
        #region Get_Set_FootnoteObjectModel_TaxonomyAssociationId
        /// <summary>
        /// Get_Set_FootnoteObjectModel_TaxonomyAssociationId
        /// </summary>
        [TestMethod]
        public void Get_Set_FootnoteObjectModel_TaxonomyAssociationId()
        {
            FootnoteObjectModel objModel1 = new FootnoteObjectModel { TaxonomyAssociationId = 2 };
            FootnoteObjectModel objModel2 = new FootnoteObjectModel { TaxonomyAssociationId = 100 };
            FootnoteObjectModel objModel3 = new FootnoteObjectModel { TaxonomyAssociationId = 9 };
            Assert.AreEqual(2, objModel1.TaxonomyAssociationId);
            Assert.AreEqual(100, objModel2.TaxonomyAssociationId);
            Assert.AreEqual(9, objModel3.TaxonomyAssociationId);
        }
        #endregion
        #region Get_Set_FootnoteObjectModel_TaxonomyAssociationGroupId
        /// <summary>
        /// Get_Set_FootnoteObjectModel_TaxonomyAssociationGroupId
        /// </summary>
        [TestMethod]
        public void Get_Set_FootnoteObjectModel_TaxonomyAssociationGroupId()
        {
            FootnoteObjectModel objModel1 = new FootnoteObjectModel { TaxonomyAssociationGroupId = 2 };
            FootnoteObjectModel objModel2 = new FootnoteObjectModel { TaxonomyAssociationGroupId = 100 };
            FootnoteObjectModel objModel3 = new FootnoteObjectModel { TaxonomyAssociationGroupId = 9 };
            Assert.AreEqual(2, objModel1.TaxonomyAssociationGroupId);
            Assert.AreEqual(100, objModel2.TaxonomyAssociationGroupId);
            Assert.AreEqual(9, objModel3.TaxonomyAssociationGroupId);
        }
        #endregion
        #region Get_Set_FootnoteObjectModel_LanguageCulture
        /// <summary>
        /// Get_Set_FootnoteObjectModel_LanguageCulture
        /// </summary>
        [TestMethod]
        public void Get_Set_FootnoteObjectModel_LanguageCulture()
        {
            FootnoteObjectModel objModel1 = new FootnoteObjectModel { LanguageCulture = "Test1"};
            FootnoteObjectModel objModel2 = new FootnoteObjectModel { LanguageCulture = "Test2" };
            FootnoteObjectModel objModel3 = new FootnoteObjectModel { LanguageCulture = "Test3" };
            Assert.AreEqual("Test1", objModel1.LanguageCulture);
            Assert.AreEqual("Test2", objModel2.LanguageCulture);
            Assert.AreEqual("Test3", objModel3.LanguageCulture);
        }
        #endregion
        #region Get_Set_FootnoteObjectModel_Text
        /// <summary>
        /// Get_Set_FootnoteObjectModel_Text
        /// </summary>
        [TestMethod]
        public void Get_Set_FootnoteObjectModel_Text()
        {
            FootnoteObjectModel objModel1 = new FootnoteObjectModel { Text = "Test1" };
            FootnoteObjectModel objModel2 = new FootnoteObjectModel { Text = "Test2" };
            FootnoteObjectModel objModel3 = new FootnoteObjectModel { Text = "Test3" };
            Assert.AreEqual("Test1", objModel1.Text);
            Assert.AreEqual("Test2", objModel2.Text);
            Assert.AreEqual("Test3", objModel3.Text);
        }
        #endregion
        #region Get_Set_FootnoteObjectModel_Order
        /// <summary>
        /// Get_Set_FootnoteObjectModel_Order
        /// </summary>
        [TestMethod]
        public void Get_Set_FootnoteObjectModel_Order()
        {
            FootnoteObjectModel objModel1 = new FootnoteObjectModel { Order = 2 };
            FootnoteObjectModel objModel2 = new FootnoteObjectModel { Order = 100 };
            FootnoteObjectModel objModel3 = new FootnoteObjectModel { Order = 9 };
            Assert.AreEqual(2, objModel1.Order);
            Assert.AreEqual(100, objModel2.Order);
            Assert.AreEqual(9, objModel3.Order);
        }
        #endregion
    }
}
