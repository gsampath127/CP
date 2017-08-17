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
    /// Test class for ClientDocumentObjectModel class
    /// </summary>
    [TestClass]
    public class ClientDocumentObjectModelTests
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
            ClientDocumentObjectModel objModel = new ClientDocumentObjectModel();
            objModel.ClientDocumentId = 1;
            var result = objModel.CompareTo(new ClientDocumentObjectModel() { ClientDocumentId = 1 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
        }
        #endregion
        #region Get_Set_ClientDocumentObjectModel_ClientDocumentId
        /// <summary>
        /// Get_Set_ClientDocumentObjectModel_ClientDocumentId
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentObjectModel_ClientDocumentId()
        {
            ClientDocumentObjectModel objModel1 = new ClientDocumentObjectModel { ClientDocumentId = 3 };
            ClientDocumentObjectModel objModel2 = new ClientDocumentObjectModel { ClientDocumentId = 30 };
            ClientDocumentObjectModel objModel3 = new ClientDocumentObjectModel { ClientDocumentId = 13 };
            Assert.AreEqual(3, objModel1.ClientDocumentId);
            Assert.AreEqual(30, objModel2.ClientDocumentId);
            Assert.AreEqual(13, objModel3.ClientDocumentId);
        }
        #endregion
        #region Get_Set_ClientDocumentObjectModel_ClientDocumentTypeId
        /// <summary>
        /// Get_Set_ClientDocumentObjectModel_ClientDocumentTypeId
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentObjectModel_ClientDocumentTypeId()
        {
            ClientDocumentObjectModel objModel1 = new ClientDocumentObjectModel { ClientDocumentTypeId = 31 };
            ClientDocumentObjectModel objModel2 = new ClientDocumentObjectModel { ClientDocumentTypeId = 301};
            ClientDocumentObjectModel objModel3 = new ClientDocumentObjectModel { ClientDocumentTypeId = 13 };
            Assert.AreEqual(31, objModel1.ClientDocumentTypeId);
            Assert.AreEqual(301, objModel2.ClientDocumentTypeId);
            Assert.AreEqual(13, objModel3.ClientDocumentTypeId);
        }
        #endregion
        #region Get_Set_ClientDocumentObjectModel_ClientDocumentTypeName
        /// <summary>
        /// Get_Set_ClientDocumentObjectModel_ClientDocumentTypeName
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentObjectModel_ClientDocumentTypeName()
        {
            ClientDocumentObjectModel objModel1 = new ClientDocumentObjectModel { ClientDocumentTypeName = "Name1" };
            ClientDocumentObjectModel objModel2 = new ClientDocumentObjectModel { ClientDocumentTypeName = "Name2" };
            ClientDocumentObjectModel objModel3 = new ClientDocumentObjectModel { ClientDocumentTypeName = "Name3" };
            Assert.AreEqual("Name1", objModel1.ClientDocumentTypeName);
            Assert.AreEqual("Name2", objModel2.ClientDocumentTypeName);
            Assert.AreEqual("Name3", objModel3.ClientDocumentTypeName);
        }
        #endregion
        #region Get_Set_ClientDocumentObjectModel_FileName
        /// <summary>
        /// Get_Set_ClientDocumentObjectModel_FileName
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentObjectModel_FileName()
        {
            ClientDocumentObjectModel objModel1 = new ClientDocumentObjectModel { FileName = "Name1" };
            ClientDocumentObjectModel objModel2 = new ClientDocumentObjectModel { FileName = "Name2" };
            ClientDocumentObjectModel objModel3 = new ClientDocumentObjectModel { FileName = "Name3" };
            Assert.AreEqual("Name1", objModel1.FileName);
            Assert.AreEqual("Name2", objModel2.FileName);
            Assert.AreEqual("Name3", objModel3.FileName);
        }
        #endregion
        #region Get_Set_ClientDocumentObjectModel_MimeType
        /// <summary>
        /// Get_Set_ClientDocumentObjectModel_MimeType
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentObjectModel_MimeType()
        {
            ClientDocumentObjectModel objModel1 = new ClientDocumentObjectModel { MimeType = "Name1" };
            ClientDocumentObjectModel objModel2 = new ClientDocumentObjectModel { MimeType = "Name2" };
            ClientDocumentObjectModel objModel3 = new ClientDocumentObjectModel { MimeType = "Name3" };
            Assert.AreEqual("Name1", objModel1.MimeType);
            Assert.AreEqual("Name2", objModel2.MimeType);
            Assert.AreEqual("Name3", objModel3.MimeType);
        }
        #endregion
        #region Get_Set_ClientDocumentObjectModel_IsPrivate
        /// <summary>
        /// Get_Set_ClientDocumentObjectModel_IsPrivate
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentObjectModel_IsPrivate()
        {
            ClientDocumentObjectModel objModel1 = new ClientDocumentObjectModel { IsPrivate = true};
            ClientDocumentObjectModel objModel2 = new ClientDocumentObjectModel { IsPrivate = false };
           
            Assert.AreEqual(true, objModel1.IsPrivate);
            Assert.AreEqual(false, objModel2.IsPrivate);
           
        }
        #endregion
        #region Get_Set_ClientDocumentObjectModel_ContentUri
        /// <summary>
        /// Get_Set_ClientDocumentObjectModel_ContentUri
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentObjectModel_ContentUri()
        {
            ClientDocumentObjectModel objModel1 = new ClientDocumentObjectModel { ContentUri = "Name1" };
            ClientDocumentObjectModel objModel2 = new ClientDocumentObjectModel { ContentUri = "Name2" };
            ClientDocumentObjectModel objModel3 = new ClientDocumentObjectModel { ContentUri = "Name3" };
            Assert.AreEqual("Name1", objModel1.ContentUri);
            Assert.AreEqual("Name2", objModel2.ContentUri);
            Assert.AreEqual("Name3", objModel3.ContentUri);
        }
        #endregion
        #region Get_Set_ClientDocumentObjectModel_Order
        /// <summary>
        /// Get_Set_ClientDocumentObjectModel_Order
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentObjectModel_Order()
        {
            ClientDocumentObjectModel objModel1 = new ClientDocumentObjectModel { Order = 3 };
            ClientDocumentObjectModel objModel2 = new ClientDocumentObjectModel { Order = 30 };
            ClientDocumentObjectModel objModel3 = new ClientDocumentObjectModel { Order = 13 };
            Assert.AreEqual(3, objModel1.Order);
            Assert.AreEqual(30, objModel2.Order);
            Assert.AreEqual(13, objModel3.Order);
        }
        #endregion
        //#region Get_Set_ClientDocumentObjectModel_FileData
        ///// <summary>
        ///// Get_Set_ClientDocumentObjectModel_FileData
        ///// </summary>
        //[TestMethod]
        //public void Get_Set_ClientDocumentObjectModel_FileData()
        //{ 
        //    ClientDocumentObjectModel objModel1 = new ClientDocumentObjectModel {byte[] { 7, 9 } };
        //    ClientDocumentObjectModel objModel2 = new ClientDocumentObjectModel { byte[] { 8, 1 } };
        //    ClientDocumentObjectModel objModel3 = new ClientDocumentObjectModel { byte[] { 0, 10 } };
        //    Assert.AreEqual(byte[] { 7, 9 }, objModel1.FileData);
        //    Assert.AreEqual(byte[] { 8, 1 }, objModel2.FileData);
        //    Assert.AreEqual( byte[] { 0, 10 }, objModel3.FileData);
        //}
        //#endregion
        
        
        

      
        
        
        

        
        

        

        

    }
}
