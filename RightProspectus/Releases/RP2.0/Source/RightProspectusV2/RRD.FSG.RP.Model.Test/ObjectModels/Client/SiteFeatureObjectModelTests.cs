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
    /// Test class for SiteFeatureObjectModel class
    /// </summary>
    [TestClass]
    public class SiteFeatureObjectModelTests
    {
        #region CompareTo_Returns_Int
        /// <summary>
        /// CompareTo_Returns_Int
        /// </summary>
        [TestMethod]
        public void CompareTo_Returns_Int()
        {
            //Arrange
            Exception exe = null;
            //Act
            SiteFeatureObjectModel objModel = new SiteFeatureObjectModel();
            try
            {
                objModel.CompareTo(new SiteFeatureObjectModel());
            }
            catch (Exception e)
            {
                if (e is NotImplementedException)
                    exe = e;
            }

            // Verify and Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        }
        #endregion
    }
}
