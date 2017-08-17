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
    public class SiteTextKeyTests
    {
        #region Equals_With_SiteTextKey
        /// <summary>
        /// Equals_With_SiteTextKey
        /// </summary>
        [TestMethod]
        public void Equals_With_SiteTextKey()
        {

            //Act
            SiteTextKey ObjSiteTextKey = new SiteTextKey(1, 2);
            var result = ObjSiteTextKey.Equals(ObjSiteTextKey);
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
            object obj = new SiteTextKey(2,3);
            //Act
            SiteTextKey ObjSiteTextKey = new SiteTextKey(1, 2);
            var result = ObjSiteTextKey.Equals(obj);
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
            SiteTextKey ObjSiteTextKey = new SiteTextKey(1, 2);
            var result = ObjSiteTextKey.GetHashCode();
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
            SiteTextKey ObjSiteTextKey = new SiteTextKey(1, 2);
            var result = ObjSiteTextKey.CompareTo(ObjSiteTextKey);
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
            SiteTextKey ObjSiteTextKey = new SiteTextKey(1, 2);
            try
            {
                var result = ObjSiteTextKey.CompareTo(obj);
            }
            catch (Exception e)
            {

                if (e is NotImplementedException)
                {
                    exe = e;
                }
            }

            //Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        }
        #endregion
        #region Create_return_SiteTextKey
        /// <summary>
        /// Create_return_SiteTextKey
        /// </summary>
        [TestMethod]
        public void Create_return_SiteTextKey()
        {
            //Act
            SiteTextKey ObjSiteTextKey = new SiteTextKey(1, 2);
            var result = SiteTextKey.Create(3,1);
            //Assert
            Assert.IsInstanceOfType(result, typeof(SiteTextKey));
        }
        #endregion
        #region Greater_Than_With_SiteTextKey
        /// <summary>
        /// Greater_Than_With_SiteTextKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_SiteTextKey()
        {
            //Act
            SiteTextKey key1 = new SiteTextKey(1, 4);
            SiteTextKey key2 = new SiteTextKey(1, 3);
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_With_SiteTextKey
        /// <summary>
        /// Less_Than_With_SiteTextKey
        /// </summary>
        [TestMethod]
        public void Less_Than_With_SiteTextKey()
        {
            //Act
            SiteTextKey key1 = new SiteTextKey(1, 4);
            SiteTextKey key2 = new SiteTextKey(1, 3);
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Greater_Than_are_equalsto_With_SiteTextKey
        /// <summary>
        /// Greater_Than_are_equalsto_With_SiteTextKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_are_equalsto_With_SiteTextKey()
        {
            //Act
            SiteTextKey key1 = new SiteTextKey(1, 4);
            SiteTextKey key2 = new SiteTextKey(1, 3);
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_are_equalsto_With_SiteTextKey
        /// <summary>
        /// Less_Than_are_equalsto_With_SiteTextKey
        /// </summary>
        [TestMethod]
        public void Less_Than_are_equalsto_With_SiteTextKey()
        {
            //Act
            SiteTextKey key1 = new SiteTextKey(1, 4);
            SiteTextKey key2 = new SiteTextKey(1, 3);
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Equalsto_With_SiteTextKey
        /// <summary>
        /// Equalsto_With_SiteTextKey
        /// </summary>
        [TestMethod]
        public void Equalsto_With_SiteTextKey()
        {
            //Act
            SiteTextKey key1 = new SiteTextKey(1, 4);
            SiteTextKey key2 = new SiteTextKey(1, 3);
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Not_Equalsto_With_SiteTextKey
        /// <summary>
        /// Not_Equalsto_With_SiteTextKey
        /// </summary>
        [TestMethod]
        public void Not_Equalsto_With_SiteTextKey()
        {
            //Act
            SiteTextKey key1 = new SiteTextKey(1, 4);
            SiteTextKey key2 = new SiteTextKey(1, 3);
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
