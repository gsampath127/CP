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
    /// Test class for TemplatePageTextObjectModel class
    /// </summary>
    [TestClass]
    public class TemplatePageTextObjectModelTests
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
            TemplatePageTextObjectModel objModel = new TemplatePageTextObjectModel();
            objModel.ResourceKey = "Test";
            var result = objModel.CompareTo(new TemplatePageTextObjectModel() { ResourceKey = "Test" });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion
    }
}
