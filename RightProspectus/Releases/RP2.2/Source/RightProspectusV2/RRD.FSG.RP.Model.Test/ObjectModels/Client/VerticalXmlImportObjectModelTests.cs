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
    /// Test class for VerticalXmlImportObjectModel class
    /// </summary>
    [TestClass]
    public class VerticalXmlImportObjectModelTests
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
            VerticalXmlImportObjectModel objModel = new VerticalXmlImportObjectModel();
            objModel.VerticalXmlImportId = 1;
            var result = objModel.CompareTo(new VerticalXmlImportObjectModel() { VerticalXmlImportId = 1 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
        }
        #endregion
        #region Get_Set_VerticalXmlImportObjectModel_VerticalXmlImportId
        /// <summary>
        /// Get_Set_VerticalXmlImportObjectModel_VerticalXmlImportId
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlImportObjectModel_VerticalXmlImportId()
        {
            VerticalXmlImportObjectModel objModel1 = new VerticalXmlImportObjectModel { VerticalXmlImportId = 2 };
            VerticalXmlImportObjectModel objModel2 = new VerticalXmlImportObjectModel { VerticalXmlImportId = 100 };
            VerticalXmlImportObjectModel objModel3 = new VerticalXmlImportObjectModel { VerticalXmlImportId = 9 };
            Assert.AreEqual(2, objModel1.VerticalXmlImportId);
            Assert.AreEqual(100, objModel2.VerticalXmlImportId);
            Assert.AreEqual(9, objModel3.VerticalXmlImportId);
        }
        #endregion
        #region Get_Set_VerticalXmlImportObjectModel_ImportTypes
        /// <summary>
        /// Get_Set_VerticalXmlImportObjectModel_ImportTypes
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlImportObjectModel_ImportTypes()
        {
            VerticalXmlImportObjectModel  objModel1 = new VerticalXmlImportObjectModel  { ImportTypes = 2 };
            VerticalXmlImportObjectModel  objModel2 = new VerticalXmlImportObjectModel  { ImportTypes = 100 };
            VerticalXmlImportObjectModel  objModel3 = new VerticalXmlImportObjectModel  { ImportTypes = 9 };
            Assert.AreEqual(2, objModel1.ImportTypes);
            Assert.AreEqual(100, objModel2.ImportTypes);
            Assert.AreEqual(9, objModel3.ImportTypes);
        }
        #endregion
        #region Get_Set_VerticalXmlImportObjectModel_ImportXml
        /// <summary>
        /// Get_Set_VerticalXmlImportObjectModel_ImportXml
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlImportObjectModel_ImportXml()
        {
            VerticalXmlImportObjectModel  objModel1 = new VerticalXmlImportObjectModel  { ImportXml = "Test1" };
            VerticalXmlImportObjectModel  objModel2 = new VerticalXmlImportObjectModel  { ImportXml = "Test2" };
            VerticalXmlImportObjectModel  objModel3 = new VerticalXmlImportObjectModel  { ImportXml = "Test3" };
            Assert.AreEqual("Test1", objModel1.ImportXml);
            Assert.AreEqual("Test2", objModel2.ImportXml);
            Assert.AreEqual("Test3", objModel3.ImportXml);
        }
        #endregion
        #region Get_Set_VerticalXmlImportObjectModel_ImportDate
        /// <summary>
        /// Get_Set_VerticalXmlImportObjectModel_ImportDate
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlImportObjectModel_ImportDate()
        {
            VerticalXmlImportObjectModel  objModel1 = new VerticalXmlImportObjectModel  { ImportDate = DateTime.Now };
            VerticalXmlImportObjectModel  objModel2 = new VerticalXmlImportObjectModel  { ImportDate = DateTime.MinValue};
            VerticalXmlImportObjectModel  objModel3 = new VerticalXmlImportObjectModel  { ImportDate = DateTime.MaxValue};
            Assert.AreEqual(DateTime.Now, objModel1.ImportDate);
            Assert.AreEqual(DateTime.MinValue, objModel2.ImportDate);
            Assert.AreEqual(DateTime.MaxValue, objModel3.ImportDate);
        }
        #endregion
        #region Get_Set_VerticalXmlImportObjectModel_ImportedBy
        /// <summary>
        /// Get_Set_VerticalXmlImportObjectModel_ImportedBy
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlImportObjectModel_ImportedBy()
        {
            VerticalXmlImportObjectModel objModel1 = new VerticalXmlImportObjectModel { ImportedBy = 2 };
            VerticalXmlImportObjectModel objModel2 = new VerticalXmlImportObjectModel { ImportedBy = 100 };
            VerticalXmlImportObjectModel objModel3 = new VerticalXmlImportObjectModel { ImportedBy = 9 };
            Assert.AreEqual(2, objModel1.ImportedBy);
            Assert.AreEqual(100, objModel2.ImportedBy);
            Assert.AreEqual(9, objModel3.ImportedBy);
        }
        #endregion
        #region Get_Set_VerticalXmlImportObjectModel_ImportDescription
        /// <summary>
        /// Get_Set_VerticalXmlImportObjectModel_ImportDescription
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlImportObjectModel_ImportDescription()
        {
            VerticalXmlImportObjectModel  objModel1 = new VerticalXmlImportObjectModel  { ImportDescription = "Test1" };
            VerticalXmlImportObjectModel  objModel2 = new VerticalXmlImportObjectModel  { ImportDescription = "Test2" };
            VerticalXmlImportObjectModel  objModel3 = new VerticalXmlImportObjectModel  { ImportDescription = "Test3" };
            Assert.AreEqual("Test1", objModel1.ImportDescription);
            Assert.AreEqual("Test2", objModel2.ImportDescription);
            Assert.AreEqual("Test3", objModel3.ImportDescription);
        }
        #endregion
        #region Get_Set_VerticalXmlImportObjectModel_ImportedByName
        /// <summary>
        /// Get_Set_VerticalXmlImportObjectModel_ImportedByName
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlImportObjectModel_ImportedByName()
        {
            VerticalXmlImportObjectModel  objModel1 = new VerticalXmlImportObjectModel  { ImportedByName = "Test1" };
            VerticalXmlImportObjectModel  objModel2 = new VerticalXmlImportObjectModel  { ImportedByName = "Test2" };
            VerticalXmlImportObjectModel  objModel3 = new VerticalXmlImportObjectModel  { ImportedByName = "Test3" };
            Assert.AreEqual("Test1", objModel1.ImportedByName);
            Assert.AreEqual("Test2", objModel2.ImportedByName);
            Assert.AreEqual("Test3", objModel3.ImportedByName);
        }
        #endregion
        #region Get_Set_VerticalXmlImportObjectModel_ExportBackupId
        /// <summary>
        /// Get_Set_VerticalXmlImportObjectModel_ExportBackupId
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlImportObjectModel_ExportBackupId()
        {
            VerticalXmlImportObjectModel objModel1 = new VerticalXmlImportObjectModel { ExportBackupId = 2 };
            VerticalXmlImportObjectModel objModel2 = new VerticalXmlImportObjectModel { ExportBackupId = 100 };
            VerticalXmlImportObjectModel objModel3 = new VerticalXmlImportObjectModel { ExportBackupId = 9 };
            Assert.AreEqual(2, objModel1.ExportBackupId);
            Assert.AreEqual(100, objModel2.ExportBackupId);
            Assert.AreEqual(9, objModel3.ExportBackupId);
        }
        #endregion
        #region Get_Set_VerticalXmlImportObjectModel_Status
        /// <summary>
        /// Get_Set_VerticalXmlImportObjectModel_Status
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlImportObjectModel_Status()
        {
            VerticalXmlImportObjectModel objModel1 = new VerticalXmlImportObjectModel { Status = 2 };
            VerticalXmlImportObjectModel objModel2 = new VerticalXmlImportObjectModel { Status = 100 };
            VerticalXmlImportObjectModel objModel3 = new VerticalXmlImportObjectModel { Status = 9 };
            Assert.AreEqual(2, objModel1.Status);
            Assert.AreEqual(100, objModel2.Status);
            Assert.AreEqual(9, objModel3.Status);
        }
        #endregion

    }
}
