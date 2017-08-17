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
    /// Test class for PageFeatureObjectModel class
    /// </summary>
    [TestClass]
   public class PageFeatureObjectModelTests
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
            PageFeatureObjectModel objModel = new PageFeatureObjectModel();
            try
            {
               objModel.CompareTo(new PageFeatureObjectModel());
            }
            catch(Exception e)
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
