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
    /// Test class for SiteTextObjectModel class
    /// </summary>
    [TestClass]
    public class SiteTextObjectModelTests
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
            SiteTextObjectModel objModel = new SiteTextObjectModel();
            objModel.SiteID = 1;
            var result = objModel.CompareTo(new SiteTextObjectModel() { SiteID = 1 });
            var result2 = objModel.CompareTo(new SiteTextObjectModel() { SiteID = 10 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, result2);
        }
        #endregion
        #region Get_Set_SiteTextObjectModel_SiteTextID
        /// <summary>
        /// Get_Set_SiteTextObjectModel_SiteTextID
        /// </summary>
        [TestMethod]
        public void Get_Set_SiteTextObjectModel_SiteTextID()
        {
            SiteTextObjectModel objModel1 = new SiteTextObjectModel { SiteTextID = 3 };
            SiteTextObjectModel objModel2 = new SiteTextObjectModel { SiteTextID = 30 };
            SiteTextObjectModel objModel3 = new SiteTextObjectModel { SiteTextID = 13 };
            Assert.AreEqual(3, objModel1.SiteTextID);
            Assert.AreEqual(30, objModel2.SiteTextID);
            Assert.AreEqual(13, objModel3.SiteTextID);
        }
        #endregion
        #region Get_Set_SiteTextObjectModel_Version
        /// <summary>
        /// Get_Set_SiteTextObjectModel_Version
        /// </summary>
        [TestMethod]
        public void Get_Set_SiteTextObjectModel_Version()
        {
            SiteTextObjectModel objModel1 = new SiteTextObjectModel { Version = 3 };
            SiteTextObjectModel objModel2 = new SiteTextObjectModel { Version = 30 };
            SiteTextObjectModel objModel3 = new SiteTextObjectModel { Version = 13 };
            Assert.AreEqual(3, objModel1.Version);
            Assert.AreEqual(30, objModel2.Version);
            Assert.AreEqual(13, objModel3.Version);
        }
        #endregion
        #region Get_Set_SiteTextObjectModel_SiteID
        /// <summary>
        /// Get_Set_SiteTextObjectModel_SiteID
        /// </summary>
        [TestMethod]
        public void Get_Set_SiteTextObjectModel_SiteID()
        {
            SiteTextObjectModel objModel1 = new SiteTextObjectModel { SiteID = 3 };
            SiteTextObjectModel objModel2 = new SiteTextObjectModel { SiteID = 30 };
            SiteTextObjectModel objModel3 = new SiteTextObjectModel { SiteID = 13 };
            Assert.AreEqual(3, objModel1.SiteID);
            Assert.AreEqual(30, objModel2.SiteID);
            Assert.AreEqual(13, objModel3.SiteID);
        }
        #endregion
        #region Get_Set_SiteTextObjectModel_SiteName
        /// <summary>
        /// Get_Set_SiteTextObjectModel_SiteName
        /// </summary>
        [TestMethod]
        public void Get_Set_SiteTextObjectModel_SiteName()
        {
            SiteTextObjectModel objModel1 = new SiteTextObjectModel { SiteName = "Test1"};
            SiteTextObjectModel objModel2 = new SiteTextObjectModel { SiteName = "Test2" };
            SiteTextObjectModel objModel3 = new SiteTextObjectModel { SiteName = "Test3" };
            Assert.AreEqual("Test1", objModel1.SiteName);
            Assert.AreEqual("Test2", objModel2.SiteName);
            Assert.AreEqual("Test3", objModel3.SiteName);
        }
        #endregion
        #region Get_Set_SiteTextObjectModel_ResourceKey
        /// <summary>
        /// Get_Set_SiteTextObjectModel_ResourceKey
        /// </summary>
        [TestMethod]
        public void Get_Set_SiteTextObjectModel_ResourceKey()
        {
            SiteTextObjectModel objModel1 = new SiteTextObjectModel { ResourceKey = "Test1" };
            SiteTextObjectModel objModel2 = new SiteTextObjectModel { ResourceKey = "Test2" };
            SiteTextObjectModel objModel3 = new SiteTextObjectModel { ResourceKey = "Test3" };
            Assert.AreEqual("Test1", objModel1.ResourceKey);
            Assert.AreEqual("Test2", objModel2.ResourceKey);
            Assert.AreEqual("Test3", objModel3.ResourceKey);
        }
        #endregion
        #region Get_Set_SiteTextObjectModel_Text
        /// <summary>
        /// Get_Set_SiteTextObjectModel_Text
        /// </summary>
        [TestMethod]
        public void Get_Set_SiteTextObjectModel_Text()
        {
            SiteTextObjectModel objModel1 = new SiteTextObjectModel { Text = "Test1" };
            SiteTextObjectModel objModel2 = new SiteTextObjectModel { Text = "Test2" };
            SiteTextObjectModel objModel3 = new SiteTextObjectModel { Text = "Test3" };
            Assert.AreEqual("Test1", objModel1.Text);
            Assert.AreEqual("Test2", objModel2.Text);
            Assert.AreEqual("Test3", objModel3.Text);
        }
        #endregion
        #region Get_Set_SiteTextObjectModel_IsProofing
        /// <summary>
        /// Get_Set_SiteTextObjectModel_IsProofing
        /// </summary>
        [TestMethod]
        public void Get_Set_SiteTextObjectModel_IsProofing()
        {
            SiteTextObjectModel objModel1 = new SiteTextObjectModel { IsProofing = true };
            SiteTextObjectModel objModel2 = new SiteTextObjectModel { IsProofing = false };
          
            Assert.AreEqual(true, objModel1.IsProofing);
            Assert.AreEqual(false, objModel2.IsProofing);
            
        }
        #endregion
        #region Get_Set_SiteTextObjectModel_IsProofingAvailableForSiteTextId
        /// <summary>
        /// Get_Set_SiteTextObjectModel_IsProofingAvailableForSiteTextId
        /// </summary>
        [TestMethod]
        public void Get_Set_SiteTextObjectModel_IsProofingAvailableForSiteTextId()
        {
            SiteTextObjectModel objModel1 = new SiteTextObjectModel { IsProofingAvailableForSiteTextId = true };
            SiteTextObjectModel objModel2 = new SiteTextObjectModel { IsProofingAvailableForSiteTextId = false };

            Assert.AreEqual(true, objModel1.IsProofingAvailableForSiteTextId);
            Assert.AreEqual(false, objModel2.IsProofingAvailableForSiteTextId);

        }
        #endregion

    }
}
