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
    /// Test class for UrlRewriteObjectModel class
    /// </summary>
    [TestClass]
    public class UrlRewriteObjectModelTests
    {
        #region CompareTo_Returns_Int
        /// <summary>
        /// CompareTo_Returns_Int
        /// </summary>
        [TestMethod]
        public void CompareTo_Return_Int()
        {
            //Arrange

            //Act
            UrlRewriteObjectModel objModel = new UrlRewriteObjectModel();
            objModel.UrlRewriteId = 1;
            var result = objModel.CompareTo(new UrlRewriteObjectModel() { UrlRewriteId = 1 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
        }
        #endregion
    }
}
