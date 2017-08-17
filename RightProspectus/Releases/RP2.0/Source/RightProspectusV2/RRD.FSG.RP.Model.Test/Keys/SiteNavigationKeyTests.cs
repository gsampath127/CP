using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Keys;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class SiteNavigationKeyTests
    {
        #region Equals_With_SiteNavigationKey
        /// <summary>
        /// Equals_With_SiteNavigationKey
        /// </summary>
        [TestMethod]
        public void Equals_With_SiteNavigationKey()
        {

            //Act
            SiteNavigationKey ObjSiteNavigationKey = new SiteNavigationKey(1, 2);
            var result = ObjSiteNavigationKey.Equals(ObjSiteNavigationKey);
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));

        }
        #endregion
        #region Equals_With_Object
        /// <summary>
        /// Equals_With_Object
        /// </summary>
        [TestMethod]
        public void Equals_With_Object()
        {
            //Arrange
            Object obj = new SiteNavigationKey(2, 4);
            //Act
            SiteNavigationKey ObjSiteNavigationKey = new SiteNavigationKey(1, 2);
            var result = ObjSiteNavigationKey.Equals(obj);
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
            SiteNavigationKey ObjSiteNavigationKey = new SiteNavigationKey(1, 2);
            var result = ObjSiteNavigationKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));

        }
        #endregion
        #region CompareTo_With_SiteNavigationKey
        /// <summary>
        /// CompareTo_With_SiteNavigationKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_SiteNavigationKey()
        {

            //Act
            SiteNavigationKey ObjSiteNavigationKey = new SiteNavigationKey(1, 2);
            var result = ObjSiteNavigationKey.CompareTo(ObjSiteNavigationKey);
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));

        }
        #endregion
        #region CompareTo_With_object
        /// <summary>
        /// CompareTo_With_object
        /// </summary>
        [TestMethod]
        public void CompareTo_With_object()
        {
            //Arrange
            object obj = new object();
            Exception exe = null;
            //Act
            SiteNavigationKey ObjSiteNavigationKey = new SiteNavigationKey(1, 2);
            try
            {
                var result = ObjSiteNavigationKey.CompareTo(obj);
            }
            catch (Exception e)
            {

                if (e is NotImplementedException)
                    exe = e;
            }

            //Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));

        }
        #endregion
        #region Create_Returns_SiteNavigationKey
        /// <summary>
        /// Create_Returns_SiteNavigationKey
        /// </summary>
        [TestMethod]
        public void Create_Returns_SiteNavigationKey()
        {

            //Act

            var result = SiteNavigationKey.Create(2, 1);
            //Assert
            Assert.IsInstanceOfType(result, typeof(SiteNavigationKey));

        }
        #endregion
        #region Greater_Than_With_SiteNavigationKey
        /// <summary>
        /// Greater_Than_With_SiteNavigationKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_SiteNavigationKey()
        {
            //Act
            SiteNavigationKey key1 = new SiteNavigationKey(1, 4);
            SiteNavigationKey key2 = new SiteNavigationKey(1, 3);
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_With_SiteNavigationKey
        /// <summary>
        /// Less_Than_With_SiteNavigationKey
        /// </summary>
        [TestMethod]
        public void Less_Than_With_SiteNavigationKey()
        {
            //Act
            SiteNavigationKey key1 = new SiteNavigationKey(1, 4);
            SiteNavigationKey key2 = new SiteNavigationKey(1, 3);
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Greater_Than_are_equalsto_With_SiteNavigationKey
        /// <summary>
        /// Greater_Than_are_equalsto_With_SiteNavigationKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_are_equalsto_With_SiteNavigationKey()
        {
            //Act
            SiteNavigationKey key1 = new SiteNavigationKey(1, 4);
            SiteNavigationKey key2 = new SiteNavigationKey(1, 3);
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_are_equalsto_With_SiteNavigationKey
        /// <summary>
        /// Less_Than_are_equalsto_With_SiteNavigationKey
        /// </summary>
        [TestMethod]
        public void Less_Than_are_equalsto_With_SiteNavigationKey()
        {
            //Act
            SiteNavigationKey key1 = new SiteNavigationKey(1, 4);
            SiteNavigationKey key2 = new SiteNavigationKey(1, 3);
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Equalsto_With_SiteNavigationKey
        /// <summary>
        /// Equalsto_With_SiteNavigationKey
        /// </summary>
        [TestMethod]
        public void Equalsto_With_SiteNavigationKey()
        {
            //Act
            SiteNavigationKey key1 = new SiteNavigationKey(1, 4);
            SiteNavigationKey key2 = new SiteNavigationKey(1, 3);
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Not_Equalsto_With_SiteNavigationKey
        /// <summary>
        /// Not_Equalsto_With_SiteNavigationKey
        /// </summary>
        [TestMethod]
        public void Not_Equalsto_With_SiteNavigationKey()
        {
            //Act
            SiteNavigationKey key1 = new SiteNavigationKey(1, 4);
            SiteNavigationKey key2 = new SiteNavigationKey(1, 3);
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
