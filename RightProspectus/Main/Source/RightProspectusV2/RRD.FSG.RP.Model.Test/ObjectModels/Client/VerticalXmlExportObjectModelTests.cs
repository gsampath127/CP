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
    /// Test class for VerticalXmlExportObjectModel class
    /// </summary>
    [TestClass]
    public class VerticalXmlExportObjectModelTests
    {
        #region Get_Set_VerticalXmlExportObjectModel_VerticalXmlExportId
        /// <summary>
        /// Get_Set_VerticalXmlExportObjectModel_VerticalXmlExportId
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlExportObjectModel_VerticalXmlExportId()
        {
            VerticalXmlExportObjectModel objModel1 = new VerticalXmlExportObjectModel { VerticalXmlExportId = 2 };
            VerticalXmlExportObjectModel objModel2 = new VerticalXmlExportObjectModel { VerticalXmlExportId = 100 };
            VerticalXmlExportObjectModel objModel3 = new VerticalXmlExportObjectModel { VerticalXmlExportId = 9 };
            Assert.AreEqual(2, objModel1.VerticalXmlExportId);
            Assert.AreEqual(100, objModel2.VerticalXmlExportId);
            Assert.AreEqual(9, objModel3.VerticalXmlExportId);
        }
        #endregion
        #region Get_Set_VerticalXmlExportObjectModel_ExportTypes
        /// <summary>
        /// Get_Set_VerticalXmlExportObjectModel_ExportTypes
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlExportObjectModel_ExportTypes()
        {
            VerticalXmlExportObjectModel objModel1 = new VerticalXmlExportObjectModel { ExportTypes = 2 };
            VerticalXmlExportObjectModel objModel2 = new VerticalXmlExportObjectModel { ExportTypes = 100 };
            VerticalXmlExportObjectModel objModel3 = new VerticalXmlExportObjectModel { ExportTypes = 9 };
            Assert.AreEqual(2, objModel1.ExportTypes);
            Assert.AreEqual(100, objModel2.ExportTypes);
            Assert.AreEqual(9, objModel3.ExportTypes);
        }
        #endregion

        #region Get_Set_VerticalXmlExportObjectModel_ExportXml
        /// <summary>
        /// Get_Set_VerticalXmlExportObjectModel_ExportXml
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlExportObjectModel_ExportXml()
        {
            VerticalXmlExportObjectModel objModel1 = new VerticalXmlExportObjectModel { ExportXml = "Tst1" };
            VerticalXmlExportObjectModel objModel2 = new VerticalXmlExportObjectModel { ExportXml = "Tst2" };
            VerticalXmlExportObjectModel objModel3 = new VerticalXmlExportObjectModel { ExportXml = "Tst3" };
            Assert.AreEqual("Tst1", objModel1.ExportXml);
            Assert.AreEqual("Tst2", objModel2.ExportXml);
            Assert.AreEqual("Tst3", objModel3.ExportXml);
        }
        #endregion
        #region Get_Set_VerticalXmlExportObjectModel_ExportDate
        /// <summary>
        /// Get_Set_VerticalXmlExportObjectModel_ExportDate
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlExportObjectModel_ExportDate()
        {
            VerticalXmlExportObjectModel objModel1 = new VerticalXmlExportObjectModel { ExportDate = DateTime.Now };
            VerticalXmlExportObjectModel objModel2 = new VerticalXmlExportObjectModel { ExportDate = DateTime.MinValue };
            VerticalXmlExportObjectModel objModel3 = new VerticalXmlExportObjectModel { ExportDate = DateTime.MaxValue };
            Assert.AreEqual(DateTime.Now, objModel1.ExportDate);
            Assert.AreEqual(DateTime.MinValue, objModel2.ExportDate);
            Assert.AreEqual(DateTime.MaxValue, objModel3.ExportDate);
        }
        #endregion
        #region Get_Set_VerticalXmlExportObjectModel_ExportedBy
        /// <summary>
        /// Get_Set_VerticalXmlExportObjectModel_ExportedBy
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlExportObjectModel_ExportedBy()
        {
            VerticalXmlExportObjectModel objModel1 = new VerticalXmlExportObjectModel { ExportedBy = 2 };
            VerticalXmlExportObjectModel objModel2 = new VerticalXmlExportObjectModel { ExportedBy = 100 };
            VerticalXmlExportObjectModel objModel3 = new VerticalXmlExportObjectModel { ExportedBy = 9 };
            Assert.AreEqual(2, objModel1.ExportedBy);
            Assert.AreEqual(100, objModel2.ExportedBy);
            Assert.AreEqual(9, objModel3.ExportedBy);
        }
        #endregion
        #region Get_Set_VerticalXmlExportObjectModel_ExportDescription
        /// <summary>
        /// Get_Set_VerticalXmlExportObjectModel_ExportDescription
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlExportObjectModel_ExportDescription()
        {
            VerticalXmlExportObjectModel objModel1 = new VerticalXmlExportObjectModel { ExportDescription = "Test1" };
            VerticalXmlExportObjectModel objModel2 = new VerticalXmlExportObjectModel { ExportDescription = "Test2" };
            VerticalXmlExportObjectModel objModel3 = new VerticalXmlExportObjectModel { ExportDescription = "Test3" };
            Assert.AreEqual("Test1", objModel1.ExportDescription);
            Assert.AreEqual("Test2", objModel2.ExportDescription);
            Assert.AreEqual("Test3", objModel3.ExportDescription);
        }
        #endregion
        #region Get_Set_VerticalXmlExportObjectModel_ExportedByName
        /// <summary>
        /// Get_Set_VerticalXmlExportObjectModel_ExportedByName
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlExportObjectModel_ExportedByName()
        {
            VerticalXmlExportObjectModel objModel1 = new VerticalXmlExportObjectModel { ExportedByName = "Test1" };
            VerticalXmlExportObjectModel objModel2 = new VerticalXmlExportObjectModel { ExportedByName = "Test2" };
            VerticalXmlExportObjectModel objModel3 = new VerticalXmlExportObjectModel { ExportedByName = "Test3" };
            Assert.AreEqual("Test1", objModel1.ExportedByName);
            Assert.AreEqual("Test2", objModel2.ExportedByName);
            Assert.AreEqual("Test3", objModel3.ExportedByName);
        }
        #endregion

        #region Get_Set_VerticalXmlExportObjectModel_Status
        /// <summary>
        /// Get_Set_VerticalXmlExportObjectModel_Status
        /// </summary>
        [TestMethod]
        public void Get_Set_VerticalXmlExportObjectModel_Status()
        {
            VerticalXmlExportObjectModel objModel1 = new VerticalXmlExportObjectModel { Status = 2 };
            VerticalXmlExportObjectModel objModel2 = new VerticalXmlExportObjectModel { Status = 100 };
            VerticalXmlExportObjectModel objModel3 = new VerticalXmlExportObjectModel { Status = 9 };
            Assert.AreEqual(2, objModel1.Status);
            Assert.AreEqual(100, objModel2.Status);
            Assert.AreEqual(9, objModel3.Status);
        }
        #endregion
        #region CompareTo_Returns_Int
        /// <summary>
        /// CompareTo_Returns_Int
        /// </summary>
        [TestMethod]
        public void CompareTo_Returns_Int()
        {
            //Arrange

            //Act
            VerticalXmlExportObjectModel objModel = new VerticalXmlExportObjectModel();
            objModel.VerticalXmlExportId = 1;
            var result = objModel.CompareTo(new VerticalXmlExportObjectModel() { VerticalXmlExportId = 1 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
        }
        #endregion
    }
}
