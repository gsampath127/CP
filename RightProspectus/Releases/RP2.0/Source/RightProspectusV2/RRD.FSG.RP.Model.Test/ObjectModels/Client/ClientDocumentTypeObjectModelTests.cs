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
    /// Test class for ClientDocumentTypeObjectModel class
    /// </summary>
    [TestClass]
    public class ClientDocumentTypeObjectModelTests
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
            ClientDocumentTypeObjectModel objModel = new ClientDocumentTypeObjectModel();
            objModel.ClientDocumentTypeId = 12;
            var result = objModel.CompareTo(new ClientDocumentTypeObjectModel() { ClientDocumentTypeId = 12 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
        }
        #endregion

        #region Get_Set_ClientDocumentTypeObjectModel_ClientDocumentTypeId
        /// <summary>
        /// Get_Set_ClientDocumentTypeObjectModel_ClientDocumentTypeId
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentTypeObjectModel_ClientDocumentTypeId()
        {
            ClientDocumentTypeObjectModel objModel1 = new ClientDocumentTypeObjectModel { ClientDocumentTypeId = 3 };
            ClientDocumentTypeObjectModel objModel2 = new ClientDocumentTypeObjectModel { ClientDocumentTypeId = 30 };
            ClientDocumentTypeObjectModel objModel3 = new ClientDocumentTypeObjectModel { ClientDocumentTypeId = 13 };
            Assert.AreEqual(3, objModel1.ClientDocumentTypeId);
            Assert.AreEqual(30, objModel2.ClientDocumentTypeId);
            Assert.AreEqual(13, objModel3.ClientDocumentTypeId);
        }
        #endregion

        
    }
}
