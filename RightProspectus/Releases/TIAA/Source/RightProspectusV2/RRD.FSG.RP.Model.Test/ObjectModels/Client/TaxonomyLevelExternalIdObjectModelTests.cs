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
    /// Test class for TaxonomyLevelExternalIdObjectModel class
    /// </summary>
    [TestClass]
    public class TaxonomyLevelExternalIdObjectModelTests
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
            TaxonomyLevelExternalIdObjectModel objModel = new TaxonomyLevelExternalIdObjectModel();
            objModel.ExternalId = "Test";
            var result = objModel.CompareTo(new TaxonomyLevelExternalIdObjectModel() { ExternalId="Test"});

            var result1 = objModel.CompareTo(new TaxonomyLevelExternalIdObjectModel() { ExternalId = "Tst" });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
            Assert.AreEqual(-1, result1);
        }
        #endregion
        #region Get_Set_TaxonomyLevelExternalIdObjectModel_TaxonomyId
        /// <summary>
        /// Get_Set_TaxonomyLevelExternalIdObjectModel_TaxonomyId
        /// </summary>
        [TestMethod]
        public void Get_Set_TaxonomyLevelExternalIdObjectModel_TaxonomyId()
        {
            TaxonomyLevelExternalIdObjectModel objModel1 = new TaxonomyLevelExternalIdObjectModel { TaxonomyId = 3 };
            TaxonomyLevelExternalIdObjectModel objModel2 = new TaxonomyLevelExternalIdObjectModel { TaxonomyId = 30 };
            TaxonomyLevelExternalIdObjectModel objModel3 = new TaxonomyLevelExternalIdObjectModel { TaxonomyId = 13 };
            Assert.AreEqual(3, objModel1.TaxonomyId);
            Assert.AreEqual(30, objModel2.TaxonomyId);
            Assert.AreEqual(13, objModel3.TaxonomyId);
        }
        #endregion
        #region Get_Set_TaxonomyLevelExternalIdObjectModel_Level
        /// <summary>
        /// Get_Set_TaxonomyLevelExternalIdObjectModel_Level
        /// </summary>
        [TestMethod]
        public void Get_Set_TaxonomyLevelExternalIdObjectModel_Level()
        {
            TaxonomyLevelExternalIdObjectModel objModel1 = new TaxonomyLevelExternalIdObjectModel { Level = 3 };
            TaxonomyLevelExternalIdObjectModel objModel2 = new TaxonomyLevelExternalIdObjectModel { Level = 30 };
            TaxonomyLevelExternalIdObjectModel objModel3 = new TaxonomyLevelExternalIdObjectModel { Level = 13 };
            Assert.AreEqual(3, objModel1.Level);
            Assert.AreEqual(30, objModel2.Level);
            Assert.AreEqual(13, objModel3.Level);
        }
        #endregion
        #region Get_Set_TaxonomyLevelExternalIdObjectModel_TaxonomyName
        /// <summary>
        /// Get_Set_TaxonomyLevelExternalIdObjectModel_TaxonomyName
        /// </summary>
        [TestMethod]
        public void Get_Set_TaxonomyLevelExternalIdObjectModel_TaxonomyName()
        {
            TaxonomyLevelExternalIdObjectModel objModel1 = new TaxonomyLevelExternalIdObjectModel { TaxonomyName = "Name1" };
            TaxonomyLevelExternalIdObjectModel objModel2 = new TaxonomyLevelExternalIdObjectModel { TaxonomyName = "Name2" };
            TaxonomyLevelExternalIdObjectModel objModel3 = new TaxonomyLevelExternalIdObjectModel { TaxonomyName = "Name3" };
            Assert.AreEqual("Name1", objModel1.TaxonomyName);
            Assert.AreEqual("Name2", objModel2.TaxonomyName);
            Assert.AreEqual("Name3", objModel3.TaxonomyName);
        }
        #endregion
        #region Get_Set_TaxonomyLevelExternalIdObjectModel_ExternalId
        /// <summary>
        /// Get_Set_TaxonomyLevelExternalIdObjectModel_ExternalId
        /// </summary>
        [TestMethod]
        public void Get_Set_TaxonomyLevelExternalIdObjectModel_ExternalId()
        {
            TaxonomyLevelExternalIdObjectModel objModel1 = new TaxonomyLevelExternalIdObjectModel { ExternalId = "Name1" };
            TaxonomyLevelExternalIdObjectModel objModel2 = new TaxonomyLevelExternalIdObjectModel { ExternalId = "Name2" };
            TaxonomyLevelExternalIdObjectModel objModel3 = new TaxonomyLevelExternalIdObjectModel { ExternalId = "Name3" };
            Assert.AreEqual("Name1", objModel1.ExternalId);
            Assert.AreEqual("Name2", objModel2.ExternalId);
            Assert.AreEqual("Name3", objModel3.ExternalId);
        }
        #endregion
        #region Get_Set_TaxonomyLevelExternalIdObjectModel_IsPrimary
        /// <summary>
        /// Get_Set_TaxonomyLevelExternalIdObjectModel_IsPrimary
        /// </summary>
        [TestMethod]
        public void Get_Set_TaxonomyLevelExternalIdObjectModel_IsPrimary()
        {
            TaxonomyLevelExternalIdObjectModel objModel1 = new TaxonomyLevelExternalIdObjectModel { IsPrimary = true};
            TaxonomyLevelExternalIdObjectModel objModel2 = new TaxonomyLevelExternalIdObjectModel { IsPrimary = false };
            
            Assert.AreEqual(true, objModel1.IsPrimary);
            Assert.AreEqual(false, objModel2.IsPrimary);
          
        }
        #endregion
        
    }
}
