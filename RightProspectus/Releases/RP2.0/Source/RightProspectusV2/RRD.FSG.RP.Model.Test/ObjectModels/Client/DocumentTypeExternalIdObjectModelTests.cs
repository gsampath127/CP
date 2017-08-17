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
    /// Test class for DocumentTypeExternalIdObjectModel class
    /// </summary>
    [TestClass]
    public class DocumentTypeExternalIdObjectModelTests
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
            DocumentTypeExternalIdObjectModel objModel = new DocumentTypeExternalIdObjectModel();
            objModel.ExternalId = "TXT";
            var result = objModel.CompareTo(new DocumentTypeExternalIdObjectModel() { ExternalId="TXT"});
            var result1 = objModel.CompareTo(new DocumentTypeExternalIdObjectModel { ExternalId = "Tst" });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
            Assert.AreEqual(1, result1);
        }
        #endregion
        #region Get_Set_DocumentTypeExternalIdObjectModel_DocumentTypeName
        /// <summary>
        /// Get_Set_DocumentTypeExternalIdObjectModel_DocumentTypeName
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeExternalIdObjectModel_DocumentTypeName()
        {
            DocumentTypeExternalIdObjectModel objModel1 = new DocumentTypeExternalIdObjectModel { DocumentTypeName = "Name1" };
            DocumentTypeExternalIdObjectModel objModel2 = new DocumentTypeExternalIdObjectModel { DocumentTypeName = "Name2" };
            DocumentTypeExternalIdObjectModel objModel3 = new DocumentTypeExternalIdObjectModel { DocumentTypeName = "Name3" };
            Assert.AreEqual("Name1", objModel1.DocumentTypeName);
            Assert.AreEqual("Name2", objModel2.DocumentTypeName);
            Assert.AreEqual("Name3", objModel3.DocumentTypeName);
        }
        #endregion
        #region Get_Set_DocumentTypeExternalIdObjectModel_DocumentTypeId
        /// <summary>
        /// Get_Set_DocumentTypeExternalIdObjectModel_DocumentTypeId
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeExternalIdObjectModel_DocumentTypeId()
        {
            DocumentTypeExternalIdObjectModel objModel1 = new DocumentTypeExternalIdObjectModel { DocumentTypeId = 2 };
            DocumentTypeExternalIdObjectModel objModel2 = new DocumentTypeExternalIdObjectModel { DocumentTypeId = 100 };
            DocumentTypeExternalIdObjectModel objModel3 = new DocumentTypeExternalIdObjectModel { DocumentTypeId = 9 };
            Assert.AreEqual(2, objModel1.DocumentTypeId);
            Assert.AreEqual(100, objModel2.DocumentTypeId);
            Assert.AreEqual(9, objModel3.DocumentTypeId);
        }
        #endregion
        #region Get_Set_DocumentTypeExternalIdObjectModel_ExternalId
        /// <summary>
        /// Get_Set_DocumentTypeExternalIdObjectModel_ExternalId
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeExternalIdObjectModel_ExternalId()
        {
            DocumentTypeExternalIdObjectModel objModel1 = new DocumentTypeExternalIdObjectModel { ExternalId = "Name1" };
            DocumentTypeExternalIdObjectModel objModel2 = new DocumentTypeExternalIdObjectModel { ExternalId = "Name2" };
            DocumentTypeExternalIdObjectModel objModel3 = new DocumentTypeExternalIdObjectModel { ExternalId = "Name3" };
            Assert.AreEqual("Name1", objModel1.ExternalId);
            Assert.AreEqual("Name2", objModel2.ExternalId);
            Assert.AreEqual("Name3", objModel3.ExternalId);
        }
        #endregion
        #region Get_Set_DocumentTypeExternalIdObjectModel_ModifiedDate
        /// <summary>
        /// Get_Set_DocumentTypeExternalIdObjectModel_ModifiedDate
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeExternalIdObjectModel_ModifiedDate()
        {
            DocumentTypeExternalIdObjectModel objModel1 = new DocumentTypeExternalIdObjectModel { ModifiedDate = DateTime.Now };
            DocumentTypeExternalIdObjectModel objModel2 = new DocumentTypeExternalIdObjectModel { ModifiedDate = DateTime.MaxValue };
            DocumentTypeExternalIdObjectModel objModel3 = new DocumentTypeExternalIdObjectModel { ModifiedDate = DateTime.MinValue };
            Assert.AreEqual(DateTime.Now, objModel1.ModifiedDate);
            Assert.AreEqual(DateTime.MaxValue, objModel2.ModifiedDate);
            Assert.AreEqual(DateTime.MinValue, objModel3.ModifiedDate);
        }
        #endregion
        #region Get_Set_DocumentTypeExternalIdObjectModel_ModifiedBy
        /// <summary>
        /// Get_Set_DocumentTypeExternalIdObjectModel_ModifiedBy
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeExternalIdObjectModel_ModifiedBy()
        {
            DocumentTypeExternalIdObjectModel objModel1 = new DocumentTypeExternalIdObjectModel { ModifiedBy = 2 };
            DocumentTypeExternalIdObjectModel objModel2 = new DocumentTypeExternalIdObjectModel { ModifiedBy = 100 };
            DocumentTypeExternalIdObjectModel objModel3 = new DocumentTypeExternalIdObjectModel { ModifiedBy = 9 };
            Assert.AreEqual(2, objModel1.ModifiedBy);
            Assert.AreEqual(100, objModel2.ModifiedBy);
            Assert.AreEqual(9, objModel3.ModifiedBy);
        }
        #endregion
        #region Get_Set_DocumentTypeExternalIdObjectModel_ModifiedByName
        /// <summary>
        /// Get_Set_DocumentTypeExternalIdObjectModel_ModifiedByName
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeExternalIdObjectModel_ModifiedByName()
        {
            DocumentTypeExternalIdObjectModel objModel1 = new DocumentTypeExternalIdObjectModel { ModifiedByName = "Name1" };
            DocumentTypeExternalIdObjectModel objModel2 = new DocumentTypeExternalIdObjectModel { ModifiedByName = "Name2" };
            DocumentTypeExternalIdObjectModel objModel3 = new DocumentTypeExternalIdObjectModel { ModifiedByName = "Name3" };
            Assert.AreEqual("Name1", objModel1.ModifiedByName);
            Assert.AreEqual("Name2", objModel2.ModifiedByName);
            Assert.AreEqual("Name3", objModel3.ModifiedByName);
        }
        #endregion
        #region Get_Set_DocumentTypeExternalIdObjectModel_IsPrimary
        /// <summary>
        /// Get_Set_DocumentTypeExternalIdObjectModel_IsPrimary
        /// </summary>
        [TestMethod]
        public void Get_Set_DocumentTypeExternalIdObjectModel_IsPrimary()
        {
            DocumentTypeExternalIdObjectModel objModel1 = new DocumentTypeExternalIdObjectModel { IsPrimary = true };
            DocumentTypeExternalIdObjectModel objModel2 = new DocumentTypeExternalIdObjectModel { IsPrimary = false};
            
            Assert.AreEqual(true, objModel1.IsPrimary);
            Assert.AreEqual(false, objModel2.IsPrimary);
           
        }
        #endregion
    }
}
