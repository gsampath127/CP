using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Test.ObjectModels
{  /// <summary>
    /// Test class for ErrorLogObjectModel class
    /// </summary>
   
    [TestClass]

    public class SearchBaseModelTests
    {
        #region Get_Set_SearchBaseModel_PageSize
        /// <summary>
        /// Get_Set_SearchBaseModel_PageSize
        /// </summary>
        [TestMethod]
        public void Get_Set_SearchBaseModel_PageSize()
        {
            SearchBaseModel objModel1 = new SearchBaseModel { PageSize = 3 };
            SearchBaseModel objModel2 = new SearchBaseModel { PageSize = 30 };
            SearchBaseModel objModel3 = new SearchBaseModel { PageSize = 13 };
            Assert.AreEqual(3, objModel1.PageSize);
            Assert.AreEqual(30, objModel2.PageSize);
            Assert.AreEqual(13, objModel3.PageSize);
        }
        #endregion
        #region Get_Set_SearchBaseModel_PageIndex
        /// <summary>
        /// Get_Set_SearchBaseModel_PageIndex
        /// </summary>
        [TestMethod]
        public void Get_Set_SearchBaseModel_PageIndex()
        {
            SearchBaseModel objModel1 = new SearchBaseModel { PageIndex = 3 };
            SearchBaseModel objModel2 = new SearchBaseModel { PageIndex = 30 };
            SearchBaseModel objModel3 = new SearchBaseModel { PageIndex = 13 };
            Assert.AreEqual(3, objModel1.PageIndex);
            Assert.AreEqual(30, objModel2.PageIndex);
            Assert.AreEqual(13, objModel3.PageIndex);
        }
        #endregion
        #region Get_Set_SearchBaseModel_SortDirection
        /// <summary>
        /// Get_Set_SearchBaseModel_SortDirection
        /// </summary>
        [TestMethod]
        public void Get_Set_SearchBaseModel_SortDirection()
        {
            SearchBaseModel objModel1 = new SearchBaseModel { SortDirection = "Test1" };
            SearchBaseModel objModel2 = new SearchBaseModel { SortDirection = "Test2" };
            SearchBaseModel objModel3 = new SearchBaseModel { SortDirection = "Test3" };
            Assert.AreEqual("Test1", objModel1.SortDirection);
            Assert.AreEqual("Test2", objModel2.SortDirection);
            Assert.AreEqual("Test3", objModel3.SortDirection);
        }
        #endregion
        #region Get_Set_SearchBaseModel_SortColumn
        /// <summary>
        /// Get_Set_SearchBaseModel_SortColumn
        /// </summary>
        [TestMethod]
        public void Get_Set_SearchBaseModel_SortColumn()
        {
            SearchBaseModel objModel1 = new SearchBaseModel { SortColumn = "Test1" };
            SearchBaseModel objModel2 = new SearchBaseModel { SortColumn = "Test2" };
            SearchBaseModel objModel3 = new SearchBaseModel { SortColumn = "Test3" };
            Assert.AreEqual("Test1", objModel1.SortColumn);
            Assert.AreEqual("Test2", objModel2.SortColumn);
            Assert.AreEqual("Test3", objModel3.SortColumn);
        }
        #endregion
    }
}
