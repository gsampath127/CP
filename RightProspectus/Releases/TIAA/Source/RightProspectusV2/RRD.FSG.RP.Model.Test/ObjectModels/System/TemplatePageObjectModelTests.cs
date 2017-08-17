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
    /// Test class for TemplatePageObjectModel class
    /// </summary>
    [TestClass]
    public class TemplatePageObjectModelTests
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
            TemplatePageObjectModel objModel = new TemplatePageObjectModel();
            var result = objModel.CompareTo(new TemplatePageObjectModel());

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion
    }
}
