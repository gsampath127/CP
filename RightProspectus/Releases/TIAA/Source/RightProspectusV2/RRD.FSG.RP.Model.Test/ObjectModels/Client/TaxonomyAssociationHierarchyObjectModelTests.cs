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
    /// Test class for TaxonomyAssociationHierarchyObjectModel class
    /// </summary>
    [TestClass]
    public class TaxonomyAssociationHierarchyObjectModelTests
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
            TaxonomyAssociationHierarchyObjectModel objModel = new TaxonomyAssociationHierarchyObjectModel();
            objModel.ChildTaxonomyAssociationId = 1;
            var result = objModel.CompareTo(new TaxonomyAssociationHierarchyObjectModel() { ChildTaxonomyAssociationId = 1 });
            var result2 = objModel.CompareTo(new TaxonomyAssociationHierarchyObjectModel() { ChildTaxonomyAssociationId = 2 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
            Assert.AreEqual(-1, result2);
        }
        #endregion
        #region Get_Set_TaxonomyAssociationHierarchyObjectModel_ParentTaxonomyAssociationId
        /// <summary>
        /// Get_Set_TaxonomyAssociationHierarchyObjectModel_ParentTaxonomyAssociationId
        /// </summary>
        [TestMethod]
        public void Get_Set_TaxonomyAssociationHierarchyObjectModel_ParentTaxonomyAssociationId()
        {
            TaxonomyAssociationHierarchyObjectModel objModel1 = new TaxonomyAssociationHierarchyObjectModel { ParentTaxonomyAssociationId = 3 };
            TaxonomyAssociationHierarchyObjectModel objModel2 = new TaxonomyAssociationHierarchyObjectModel { ParentTaxonomyAssociationId = 30 };
            TaxonomyAssociationHierarchyObjectModel objModel3 = new TaxonomyAssociationHierarchyObjectModel { ParentTaxonomyAssociationId = 13 };
            Assert.AreEqual(3, objModel1.ParentTaxonomyAssociationId);
            Assert.AreEqual(30, objModel2.ParentTaxonomyAssociationId);
            Assert.AreEqual(13, objModel3.ParentTaxonomyAssociationId);
        }
        #endregion
        #region Get_Set_TaxonomyAssociationHierarchyObjectModel_ChildTaxonomyAssociationId
        /// <summary>
        /// Get_Set_TaxonomyAssociationHierarchyObjectModel_ChildTaxonomyAssociationId
        /// </summary>
        [TestMethod]
        public void Get_Set_TaxonomyAssociationHierarchyObjectModel_ChildTaxonomyAssociationId()
        {
            TaxonomyAssociationHierarchyObjectModel objModel1 = new TaxonomyAssociationHierarchyObjectModel { ChildTaxonomyAssociationId = 3 };
            TaxonomyAssociationHierarchyObjectModel objModel2 = new TaxonomyAssociationHierarchyObjectModel { ChildTaxonomyAssociationId = 30 };
            TaxonomyAssociationHierarchyObjectModel objModel3 = new TaxonomyAssociationHierarchyObjectModel { ChildTaxonomyAssociationId = 13 };
            Assert.AreEqual(3, objModel1.ChildTaxonomyAssociationId);
            Assert.AreEqual(30, objModel2.ChildTaxonomyAssociationId);
            Assert.AreEqual(13, objModel3.ChildTaxonomyAssociationId);
        }
        #endregion
        #region Get_Set_TaxonomyAssociationHierarchyObjectModel_RelationshipType
        /// <summary>
        /// Get_Set_TaxonomyAssociationHierarchyObjectModel_RelationshipType
        /// </summary>
        [TestMethod]
        public void Get_Set_TaxonomyAssociationHierarchyObjectModel_RelationshipType()
        {
            TaxonomyAssociationHierarchyObjectModel objModel1 = new TaxonomyAssociationHierarchyObjectModel { RelationshipType = 3 };
            TaxonomyAssociationHierarchyObjectModel objModel2 = new TaxonomyAssociationHierarchyObjectModel { RelationshipType = 30 };
            TaxonomyAssociationHierarchyObjectModel objModel3 = new TaxonomyAssociationHierarchyObjectModel { RelationshipType = 13 };
            Assert.AreEqual(3, objModel1.RelationshipType);
            Assert.AreEqual(30, objModel2.RelationshipType);
            Assert.AreEqual(13, objModel3.RelationshipType);
        }
        #endregion
        #region Get_Set_TaxonomyAssociationHierarchyObjectModel_Order
        /// <summary>
        /// Get_Set_TaxonomyAssociationHierarchyObjectModel_Order
        /// </summary>
        [TestMethod]
        public void Get_Set_TaxonomyAssociationHierarchyObjectModel_Order()
        {
            TaxonomyAssociationHierarchyObjectModel objModel1 = new TaxonomyAssociationHierarchyObjectModel { Order = 3 };
            TaxonomyAssociationHierarchyObjectModel objModel2 = new TaxonomyAssociationHierarchyObjectModel { Order = 30 };
            TaxonomyAssociationHierarchyObjectModel objModel3 = new TaxonomyAssociationHierarchyObjectModel { Order = 13 };
            Assert.AreEqual(3, objModel1.Order);
            Assert.AreEqual(30, objModel2.Order);
            Assert.AreEqual(13, objModel3.Order);
        }
        #endregion
    }
}
