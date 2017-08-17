using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Test.ObjectModels.Vertical_Market
{
    /// <summary>
    /// Test class for TaxonomyObjectModel class
    /// </summary>
    [TestClass]
    public class TaxonomyObjectModelTests
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
            TaxonomyObjectModel objModel = new TaxonomyObjectModel();
            var result = objModel.CompareTo(new TaxonomyObjectModel());

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion
    }
}
