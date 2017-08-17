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
    /// Test class for PageNavigationObjectModel class
    /// </summary>
    [TestClass]
    public class PageNavigationObjectModelTests
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
            PageNavigationObjectModel objModel = new PageNavigationObjectModel();
            objModel.PageId = 2;
            var result = objModel.CompareTo(new PageNavigationObjectModel() { PageId = 1 });
            var result2 = objModel.CompareTo(new PageNavigationObjectModel() { PageId = 2 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, result2);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_PageNavigationId
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_PageNavigationId
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_PageNavigationId()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { PageNavigationId = 2 };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { PageNavigationId = 100 };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { PageNavigationId = 9 };
            Assert.AreEqual(2, objModel1.PageNavigationId);
            Assert.AreEqual(100, objModel2.PageNavigationId);
            Assert.AreEqual(9, objModel3.PageNavigationId);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_PageId
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_PageId
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_PageId()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { PageId = 2 };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { PageId = 100 };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { PageId = 9 };
            Assert.AreEqual(2, objModel1.PageId);
            Assert.AreEqual(100, objModel2.PageId);
            Assert.AreEqual(9, objModel3.PageId);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_SiteId
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_SiteId
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_SiteId()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { SiteId = 2 };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { SiteId = 100 };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { SiteId = 9 };
            Assert.AreEqual(2, objModel1.SiteId);
            Assert.AreEqual(100, objModel2.SiteId);
            Assert.AreEqual(9, objModel3.SiteId);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_ModifiedBy
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_ModifiedBy
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_ModifiedBy()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { ModifiedBy = 2 };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { ModifiedBy = 100 };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { ModifiedBy = 9 };
            Assert.AreEqual(2, objModel1.ModifiedBy);
            Assert.AreEqual(100, objModel2.ModifiedBy);
            Assert.AreEqual(9, objModel3.ModifiedBy);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_NavigationKey
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_NavigationKey
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_NavigationKey()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { NavigationKey = "Test1" };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { NavigationKey = "Test2" };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { NavigationKey = "Test3" };
            Assert.AreEqual("Test1", objModel1.NavigationKey);
            Assert.AreEqual("Test2", objModel2.NavigationKey);
            Assert.AreEqual("Test3", objModel3.NavigationKey);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_NavigationXML
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_NavigationXML
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_NavigationXML()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { NavigationXML = "Test1" };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { NavigationXML = "Test2" };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { NavigationXML = "Test3" };
            Assert.AreEqual("Test1", objModel1.NavigationXML);
            Assert.AreEqual("Test2", objModel2.NavigationXML);
            Assert.AreEqual("Test3", objModel3.NavigationXML);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_Text
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_Text
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_Text()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { Text = "Test1" };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { Text = "Test2" };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { Text = "Test3" };
            Assert.AreEqual("Test1", objModel1.Text);
            Assert.AreEqual("Test2", objModel2.Text);
            Assert.AreEqual("Test3", objModel3.Text);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_Version
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_Version
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_Version()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { Version = 2 };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { Version = 100 };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { Version = 9 };
            Assert.AreEqual(2, objModel1.Version);
            Assert.AreEqual(100, objModel2.Version);
            Assert.AreEqual(9, objModel3.Version);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_IsProofing
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_IsProofing
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_IsProofing()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { IsProofing = true };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { IsProofing = false };

            Assert.AreEqual(true, objModel1.IsProofing);
            Assert.AreEqual(false, objModel2.IsProofing);

        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_PageName
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_PageName
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_PageName()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { PageName = "Test1" };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { PageName = "Test2" };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { PageName = "Test3" };
            Assert.AreEqual("Test1", objModel1.PageName);
            Assert.AreEqual("Test2", objModel2.PageName);
            Assert.AreEqual("Test3", objModel3.PageName);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_PageDescription
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_PageDescription 
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_PageDescription()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { PageDescription = "Test1" };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { PageDescription = "Test2" };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { PageDescription = "Test3" };
            Assert.AreEqual("Test1", objModel1.PageDescription);
            Assert.AreEqual("Test2", objModel2.PageDescription);
            Assert.AreEqual("Test3", objModel3.PageDescription);
        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_IsProofingAvailableForPageNavigationIDAvailableForPageNavigationID
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_IsProofingAvailableForPageNavigationIDAvailableForPageNavigationID
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_IsProofingAvailableForPageNavigationIDAvailableForPageNavigationID()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { IsProofingAvailableForPageNavigationID = true };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { IsProofingAvailableForPageNavigationID = false };

            Assert.AreEqual(true, objModel1.IsProofingAvailableForPageNavigationID);
            Assert.AreEqual(false, objModel2.IsProofingAvailableForPageNavigationID);

        }
        #endregion
        #region Get_Set_PageNavigationObjectModel_UtcModifiedDate
        /// <summary>
        /// Get_Set_PageNavigationObjectModel_UtcModifiedDate
        /// </summary>
        [TestMethod]
        public void Get_Set_PageNavigationObjectModel_UtcModifiedDate()
        {
            PageNavigationObjectModel objModel1 = new PageNavigationObjectModel { UtcModifiedDate = DateTime.Parse("01/01/2015") };
            PageNavigationObjectModel objModel2 = new PageNavigationObjectModel { UtcModifiedDate = DateTime.MaxValue };
            PageNavigationObjectModel objModel3 = new PageNavigationObjectModel { UtcModifiedDate = DateTime.MinValue };
            Assert.AreEqual(DateTime.Parse("01/01/2015"), objModel1.UtcModifiedDate);
            Assert.AreEqual(DateTime.MaxValue, objModel2.UtcModifiedDate);
            Assert.AreEqual(DateTime.MinValue, objModel3.UtcModifiedDate);
        }
        #endregion
    }
}
