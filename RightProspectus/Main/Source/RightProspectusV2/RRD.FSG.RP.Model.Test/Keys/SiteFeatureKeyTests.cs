using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Keys;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class SiteFeatureKeyTests
    {
        #region Equals_With_SiteFeatureKey
        /// <summary>
        /// Equals_With_SiteFeatureKey
        /// </summary>
        [TestMethod]
        public void Equals_With_SiteFeatureKey()
        {

            //Act
            SiteFeatureKey ObjSiteFeatureKey = new SiteFeatureKey(1, "Test_key");
            var result = ObjSiteFeatureKey.Equals(ObjSiteFeatureKey);
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));

        }
        #endregion
        #region Equals_With_object
        /// <summary>
        /// Equals_With_object
        /// </summary>
        [TestMethod]
        public void Equals_With_object()
        {
            //Arrange
            object obj = new SiteFeatureKey(1,"SFKey");
            //Act
            SiteFeatureKey ObjSiteFeatureKey = new SiteFeatureKey(1, "Test_key");
            var result = ObjSiteFeatureKey.Equals(obj);
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));

        }
        #endregion
        #region GetHashCode
        /// <summary>
        /// GetHashCode
        /// </summary>
        [TestMethod]
        public void GetHashCode()
        {

            //Act
            SiteFeatureKey ObjSiteFeatureKey = new SiteFeatureKey(1, "Test_key");
            var result = ObjSiteFeatureKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));

        }
        #endregion
        #region CompareTo
        /// <summary>
        /// CompareTo
        /// </summary>
        [TestMethod]
        public void CompareTo()
        {

            //Act
            SiteFeatureKey ObjSiteFeatureKey = new SiteFeatureKey(1, "Test_key");
            var result = ObjSiteFeatureKey.CompareTo(ObjSiteFeatureKey);
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));

        }
        #endregion
        #region Create_Returns_SiteFeatureKey
        /// <summary>
        /// Create_Returns_SiteFeatureKey
        /// </summary>
        [TestMethod]
        public void Create_Returns_SiteFeatureKey()
        {

            //Act
            SiteFeatureKey ObjSiteFeatureKey = new SiteFeatureKey(1, "SFKId");

            var result = SiteFeatureKey.Create(1, "test2");

            //Assert
            Assert.IsInstanceOfType(result, typeof(SiteFeatureKey));
        }
        #endregion
        #region Greater_Than_With_SiteFeatureKey
        /// <summary>
        /// Greater_Than_With_SiteFeatureKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_SiteFeatureKey()
        {
            //Act
            SiteFeatureKey key1 = new SiteFeatureKey(1, "Test1");
            SiteFeatureKey key2 = new SiteFeatureKey(1, "Test2");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_With_SiteFeatureKey
        /// <summary>
        /// Less_Than_With_SiteFeatureKey
        /// </summary>
        [TestMethod]
        public void Less_Than_With_SiteFeatureKey()
        {
            //Act
            SiteFeatureKey key1 = new SiteFeatureKey(1, "Test1");
            SiteFeatureKey key2 = new SiteFeatureKey(1, "Test2"); ;
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Greater_Than_are_equalsto_With_SiteFeatureKey
        /// <summary>
        /// Greater_Than_are_equalsto_With_SiteFeatureKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_are_equalsto_With_SiteFeatureKey()
        {
            //Act
            SiteFeatureKey key1 = new SiteFeatureKey(1, "Test1");
            SiteFeatureKey key2 = new SiteFeatureKey(1, "Test2");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_are_equalsto_With_SiteFeatureKey
        /// <summary>
        /// Less_Than_are_equalsto_With_SiteFeatureKey
        /// </summary>
        [TestMethod]
        public void Less_Than_are_equalsto_With_SiteFeatureKey()
        {
            //Act
            SiteFeatureKey key1 = new SiteFeatureKey(1, "Test1");
            SiteFeatureKey key2 = new SiteFeatureKey(1, "Test2");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Equalsto_With_SiteFeatureKey
        /// <summary>
        /// Equalsto_With_SiteFeatureKey
        /// </summary>
        [TestMethod]
        public void Equalsto_With_SiteFeatureKey()
        {
            //Act
            SiteFeatureKey key1 = new SiteFeatureKey(1, "Test1");
            SiteFeatureKey key2 = new SiteFeatureKey(1, "Test2");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Not_Equalsto_With_SiteFeatureKey
        /// <summary>
        /// Not_Equalsto_With_SiteFeatureKey
        /// </summary>
        [TestMethod]
        public void Not_Equalsto_With_SiteFeatureKey()
        {
            //Act
            SiteFeatureKey key1 = new SiteFeatureKey(1, "Test1");
            SiteFeatureKey key2 = new SiteFeatureKey(1, "Test2");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
