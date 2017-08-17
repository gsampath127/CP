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
    /// Test class for ClientDocumentGroupObjectModel class
    /// </summary>
    [TestClass]
    public class ClientDocumentGroupObjectModelTests
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
            ClientDocumentGroupObjectModel objModel = new ClientDocumentGroupObjectModel();
            objModel.ClientDocumentGroupId = 12;
            var result = objModel.CompareTo(new ClientDocumentGroupObjectModel() { ClientDocumentGroupId = 12 });
          
            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
        }
        #endregion
        #region Get_Set_ClientDocumentGroupObjectModel_ClientDocumentGroupId
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_ClientDocumentGroupId
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentGroupObjectModel_ClientDocumentGroupId()
        {
            ClientDocumentGroupObjectModel objModel1 = new ClientDocumentGroupObjectModel { ClientDocumentGroupId = 3 };
            ClientDocumentGroupObjectModel objModel2 = new ClientDocumentGroupObjectModel { ClientDocumentGroupId = 30 };
            ClientDocumentGroupObjectModel objModel3 = new ClientDocumentGroupObjectModel { ClientDocumentGroupId = 13 };
            Assert.AreEqual(3, objModel1.ClientDocumentGroupId);
            Assert.AreEqual(30, objModel2.ClientDocumentGroupId);
            Assert.AreEqual(13, objModel3.ClientDocumentGroupId);
        }
        #endregion
        #region Get_Set_ClientDocumentGroupObjectModel_ParentClientDocumentGroupId
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_ParentClientDocumentGroupId
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentGroupObjectModel_ParentClientDocumentGroupId()
        {
            ClientDocumentGroupObjectModel objModel1 = new ClientDocumentGroupObjectModel { ParentClientDocumentGroupId = 3 };
            ClientDocumentGroupObjectModel objModel2 = new ClientDocumentGroupObjectModel { ParentClientDocumentGroupId = 10 };
            ClientDocumentGroupObjectModel objModel3 = new ClientDocumentGroupObjectModel { ParentClientDocumentGroupId = 173 };

            Assert.AreEqual(3, objModel1.ParentClientDocumentGroupId);
            Assert.AreEqual(10, objModel2.ParentClientDocumentGroupId);
            Assert.AreEqual(173, objModel3.ParentClientDocumentGroupId);
        }
        #endregion
        #region Get_Set_ClientDocumentGroupObjectModel_CssClass
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_CssClass 
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentGroupObjectModel_CssClass()
        {
            ClientDocumentGroupObjectModel objModel1 = new ClientDocumentGroupObjectModel { CssClass = "Class1" };
            ClientDocumentGroupObjectModel objModel2 = new ClientDocumentGroupObjectModel { CssClass = "Class2" };
            ClientDocumentGroupObjectModel objModel3 = new ClientDocumentGroupObjectModel { CssClass = "Class3" };

            Assert.AreEqual("Class1", objModel1.CssClass);
            Assert.AreEqual("Class2", objModel2.CssClass);
            Assert.AreEqual("Class3", objModel3.CssClass);
        }
        #endregion
        #region Get_Set_ClientDocumentGroupObjectModelList_ClientDocumentObjectModel
        /// <summary>
        /// Get_Set_ClientDocumentGroupObjectModel_List_ClientDocumentObjectModel>
        /// </summary>
        [TestMethod]
        public void Get_Set_ClientDocumentGroupObjectModel_List_ClientDocumentObjectModel()
        {
            List<ClientDocumentObjectModel> lstClientDocumentObjectModel = new List<ClientDocumentObjectModel>();
            ClientDocumentObjectModel objClientDocumentObjectModel = new ClientDocumentObjectModel
            {
                ClientDocumentId = 1,
                ClientDocumentTypeId = 3,
                ClientDocumentTypeName = "Test_TypeName",
                FileName = "Test_FileName",
                MimeType = "Test_Mime",
                IsPrivate = false,
                ContentUri = "Test_Uri",
                Order = 5,
                FileData = new byte[] { 7, 9 }
            };
            lstClientDocumentObjectModel.Add(objClientDocumentObjectModel);
            ClientDocumentGroupObjectModel objModel = new ClientDocumentGroupObjectModel { ClientDocuments = lstClientDocumentObjectModel };
            Assert.AreEqual(1, lstClientDocumentObjectModel.Count); // checking the count.

            Assert.AreEqual(objModel.ClientDocuments[0].ClientDocumentId, lstClientDocumentObjectModel[0].ClientDocumentId);
            Assert.AreEqual(objModel.ClientDocuments[0].ClientDocumentTypeId, lstClientDocumentObjectModel[0].ClientDocumentTypeId);
            Assert.AreEqual(objModel.ClientDocuments[0].ClientDocumentTypeName, lstClientDocumentObjectModel[0].ClientDocumentTypeName);
            Assert.AreEqual(objModel.ClientDocuments[0].FileName, lstClientDocumentObjectModel[0].FileName);
            Assert.AreEqual(objModel.ClientDocuments[0].MimeType, lstClientDocumentObjectModel[0].MimeType);
            Assert.AreEqual(objModel.ClientDocuments[0].IsPrivate, lstClientDocumentObjectModel[0].IsPrivate);
            Assert.AreEqual(objModel.ClientDocuments[0].ContentUri, lstClientDocumentObjectModel[0].ContentUri);
            Assert.AreEqual(objModel.ClientDocuments[0].Order, lstClientDocumentObjectModel[0].Order);
            Assert.AreEqual(objModel.ClientDocuments[0].FileData, lstClientDocumentObjectModel[0].FileData);




            //CollectionAssert.AreEqual(objModel.ClientDocuments, lstClientDocumentObjectModel);

        }
        #endregion


    }
}
