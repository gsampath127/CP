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
    /// Test class for PageTextObjectModel class
    /// </summary>
    [TestClass]
    public class PageTextObjectModelTests
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
            PageTextObjectModel objModel = new PageTextObjectModel();
            objModel.PageID = 1;
            var result = objModel.CompareTo(new PageTextObjectModel() { PageID = 1 });

            var result2 = objModel.CompareTo(new PageTextObjectModel() { PageID = 2 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, result2);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_PageTextID
        /// <summary>
        /// Get_Set_PageTextObjectModel_PageTextID
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_PageTextID()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { PageTextID = 2 };
            PageTextObjectModel objModel2 = new PageTextObjectModel { PageTextID = 100 };
            PageTextObjectModel objModel3 = new PageTextObjectModel { PageTextID = 9 };
            Assert.AreEqual(2, objModel1.PageTextID);
            Assert.AreEqual(100, objModel2.PageTextID);
            Assert.AreEqual(9, objModel3.PageTextID);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_Version
        /// <summary>
        /// Get_Set_PageTextObjectModel_Version 
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_Version()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { Version = 2 };
            PageTextObjectModel objModel2 = new PageTextObjectModel { Version = 100 };
            PageTextObjectModel objModel3 = new PageTextObjectModel { Version = 9 };
            Assert.AreEqual(2, objModel1.Version);
            Assert.AreEqual(100, objModel2.Version);
            Assert.AreEqual(9, objModel3.Version);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_PageID
        /// <summary>
        /// Get_Set_PageTextObjectModel_PageID
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_PageID()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { PageID = 2 };
            PageTextObjectModel objModel2 = new PageTextObjectModel { PageID = 100 };
            PageTextObjectModel objModel3 = new PageTextObjectModel { PageID = 9 };
            Assert.AreEqual(2, objModel1.PageID);
            Assert.AreEqual(100, objModel2.PageID);
            Assert.AreEqual(9, objModel3.PageID);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_TemplateID
        /// <summary>
        /// Get_Set_PageTextObjectModel_TemplateID 
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_TemplateID()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { TemplateID = 2 };
            PageTextObjectModel objModel2 = new PageTextObjectModel { TemplateID = 100 };
            PageTextObjectModel objModel3 = new PageTextObjectModel { TemplateID = 9 };
            Assert.AreEqual(2, objModel1.TemplateID);
            Assert.AreEqual(100, objModel2.TemplateID);
            Assert.AreEqual(9, objModel3.TemplateID);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_SiteID
        /// <summary>
        /// Get_Set_PageTextObjectModel_SiteID
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_SiteID()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { SiteID = 2 };
            PageTextObjectModel objModel2 = new PageTextObjectModel { SiteID = 100 };
            PageTextObjectModel objModel3 = new PageTextObjectModel { SiteID = 9 };
            Assert.AreEqual(2, objModel1.SiteID);
            Assert.AreEqual(100, objModel2.SiteID);
            Assert.AreEqual(9, objModel3.SiteID);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_SiteName
        /// <summary>
        /// Get_Set_PageTextObjectModel_SiteName
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_SiteName()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { SiteName = "Name1" };
            PageTextObjectModel objModel2 = new PageTextObjectModel { SiteName = "Name2" };
            PageTextObjectModel objModel3 = new PageTextObjectModel { SiteName = "Name3" };
            Assert.AreEqual("Name1", objModel1.SiteName);
            Assert.AreEqual("Name2", objModel2.SiteName);
            Assert.AreEqual("Name3", objModel3.SiteName);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_Text
        /// <summary>
        /// Get_Set_PageTextObjectModel_Text
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_Text()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { Text = "Name1" };
            PageTextObjectModel objModel2 = new PageTextObjectModel { Text = "Name2" };
            PageTextObjectModel objModel3 = new PageTextObjectModel { Text = "Name3" };
            Assert.AreEqual("Name1", objModel1.Text);
            Assert.AreEqual("Name2", objModel2.Text);
            Assert.AreEqual("Name3", objModel3.Text);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_IsProofing
        /// <summary>
        /// Get_Set_PageTextObjectModel_IsProofing
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_IsProofing()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { IsProofing = true };
            PageTextObjectModel objModel2 = new PageTextObjectModel { IsProofing = false };

            Assert.AreEqual(true, objModel1.IsProofing);
            Assert.AreEqual(false, objModel2.IsProofing);

        }
        #endregion
        #region Get_Set_PageTextObjectModel_PageName
        /// <summary>
        /// Get_Set_PageTextObjectModel_PageName
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_PageName()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { PageName = "Name1" };
            PageTextObjectModel objModel2 = new PageTextObjectModel { PageName = "Name2" };
            PageTextObjectModel objModel3 = new PageTextObjectModel { PageName = "Name3" };
            Assert.AreEqual("Name1", objModel1.PageName);
            Assert.AreEqual("Name2", objModel2.PageName);
            Assert.AreEqual("Name3", objModel3.PageName);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_PageDescription
        /// <summary>
        /// Get_Set_PageTextObjectModel_PageDescription
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_PageDescription()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { PageDescription = "Name1" };
            PageTextObjectModel objModel2 = new PageTextObjectModel { PageDescription = "Name2" };
            PageTextObjectModel objModel3 = new PageTextObjectModel { PageDescription = "Name3" };
            Assert.AreEqual("Name1", objModel1.PageDescription);
            Assert.AreEqual("Name2", objModel2.PageDescription);
            Assert.AreEqual("Name3", objModel3.PageDescription);
        }
        #endregion
        #region Get_Set_PageTextObjectModel_IsProofingAvailableForPageTextId
        /// <summary>
        /// Get_Set_PageTextObjectModel_IsProofingAvailableForPageTextId
        /// </summary>
        [TestMethod]
        public void Get_Set_PageTextObjectModel_IsProofingAvailableForPageTextId()
        {
            PageTextObjectModel objModel1 = new PageTextObjectModel { IsProofingAvailableForPageTextId = true };
            PageTextObjectModel objModel2 = new PageTextObjectModel { IsProofingAvailableForPageTextId = false };

            Assert.AreEqual(true, objModel1.IsProofingAvailableForPageTextId);
            Assert.AreEqual(false, objModel2.IsProofingAvailableForPageTextId);

        }
        #endregion


    }
}
