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
    /// Test class for RolesObjectModel class
    /// </summary>
    [TestClass]
    public class RolesObjectModelTests
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
            RolesObjectModel objModel = new RolesObjectModel();
            var result = objModel.CompareTo(new RolesObjectModel());

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion
    }
}
