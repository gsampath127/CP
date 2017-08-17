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
    /// Test class for TemplatePageNavigationObjectModel class
    /// </summary>
    [TestClass]
    public class TemplatePageNavigationObjectModelTests
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
            TemplatePageNavigationObjectModel objModel = new TemplatePageNavigationObjectModel();
            objModel.NavigationKey = "Test";
            var result = objModel.CompareTo(new TemplatePageNavigationObjectModel() { NavigationKey="Test"});

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion
    }
}
