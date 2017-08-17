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
    /// Test class for TemplateFeatureObjectModel class
    /// </summary>
    [TestClass]
    public class TemplateFeatureObjectModelTests
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
            TemplateFeatureObjectModel objModel = new TemplateFeatureObjectModel();
            objModel.FeatureKey = "Test";
            var result = objModel.CompareTo(new TemplateFeatureObjectModel() { FeatureKey="Test"});

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion
    }
}
