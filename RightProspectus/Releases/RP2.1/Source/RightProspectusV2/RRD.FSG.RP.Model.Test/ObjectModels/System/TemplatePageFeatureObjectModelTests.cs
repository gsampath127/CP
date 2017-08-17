using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Test.ObjectModels.System
{
    /// <summary>
    /// Test class for TemplateNavigationObjectModel class
    /// </summary>
    [TestClass]
    public class TemplatePageFeatureObjectModelTests
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
            TemplatePageFeatureObjectModel objModel = new TemplatePageFeatureObjectModel();
            objModel.FeatureKey = "Test";
            var result = objModel.CompareTo(new TemplatePageFeatureObjectModel() { FeatureKey = "Test" });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion
    }
}
