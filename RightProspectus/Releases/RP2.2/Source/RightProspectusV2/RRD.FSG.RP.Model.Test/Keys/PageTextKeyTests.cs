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
    public class PageTextKeyTests
    {
        #region Equals_With_PageTextKey
        /// <summary>
        /// Equals_With_PageTextKey
        /// </summary>
        [TestMethod]
        public void Equals_With_PageTextKey()
        {

            //Act
            PageTextKey ObjPageTextKey = new PageTextKey(1, 2);
            var result = ObjPageTextKey.Equals(ObjPageTextKey);
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
            object obj = new PageTextKey(1, 2);
            //Act
            PageTextKey ObjPageTextKey = new PageTextKey(1, 2);
            var result = ObjPageTextKey.Equals(obj);
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
            //Arrange
            object obj = new object();
            //Act
            PageTextKey ObjPageTextKey = new PageTextKey(1, 2);
            var result = ObjPageTextKey.GetHashCode();
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
            //Arrange
            object obj = new object();
            //Act
            PageTextKey ObjPageTextKey = new PageTextKey(1, 2);
            var result = ObjPageTextKey.CompareTo(ObjPageTextKey);
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
            PageTextKey ObjPageTextKey = new PageTextKey(1, 2);
            try
            {
                var result = ObjPageTextKey.CompareTo(obj);
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
        #region Create
        /// <summary>
        /// Create
        /// </summary>
        [TestMethod]
        public void Create()
        {
            //Arrange
            object obj = new object();
            //Act
            PageTextKey ObjPageTextKey = new PageTextKey(1, 2);
            var result = PageTextKey.Create(32, 1);
            //Assert
            Assert.IsInstanceOfType(result, typeof(PageTextKey));

        }
        #endregion
        #region Greater_Than_With_PageTextKey
        /// <summary>
        /// Greater_Than_With_PageTextKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_PageTextKey()
        {
            //Act
            PageTextKey key1 = new PageTextKey(1, 2);
            PageTextKey key2 = new PageTextKey(1, 2);
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_With_PageTextKey
        /// <summary>
        /// Less_Than_With_PageTextKey
        /// </summary>
        [TestMethod]
        public void Less_Than_With_PageTextKey()
        {
            //Act
            PageTextKey key1 = new PageTextKey(1, 2);
            PageTextKey key2 = new PageTextKey(1, 2);
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Greater_Than_are_equalsto_With_PageTextKey
        /// <summary>
        /// Greater_Than_are_equalsto_With_PageTextKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_are_equalsto_With_PageTextKey()
        {
            //Act
            PageTextKey key1 = new PageTextKey(1, 2);
            PageTextKey key2 = new PageTextKey(1, 2);
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_are_equalsto_With_PageTextKey
        /// <summary>
        /// Less_Than_are_equalsto_With_PageTextKey
        /// </summary>
        [TestMethod]
        public void Less_Than_are_equalsto_With_PageTextKey()
        {
            //Act
            PageTextKey key1 = new PageTextKey(1, 2);
            PageTextKey key2 = new PageTextKey(1, 2);
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Equalsto_With_PageTextKey
        /// <summary>
        /// Equalsto_With_PageTextKey
        /// </summary>
        [TestMethod]
        public void Equalsto_With_PageTextKey()
        {
            //Act
            PageTextKey key1 = new PageTextKey(1, 2);
            PageTextKey key2 = new PageTextKey(1, 2);
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Not_Equalsto_With_PageTextKey
        /// <summary>
        /// Not_Equalsto_With_PageFeatureKey
        /// </summary>
        [TestMethod]
        public void Not_Equalsto_With_PageTextKey()
        {
            //Act
            PageTextKey key1 = new PageTextKey(1, 2);
            PageTextKey key2 = new PageTextKey(1, 2);
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
