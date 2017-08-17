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
    public class PageNavigationKeyTests
    {

        #region Equals_With_PageNavigationKey
        /// <summary>
        /// Equals_With_PageNavigationKey
        /// </summary>
        [TestMethod]
        public void Equals_With_PageNavigationKey()
        {

            //Act
            PageNavigationKey ObjPageNavigationKey = new PageNavigationKey(1, 2);
            var result = ObjPageNavigationKey.Equals(ObjPageNavigationKey);
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
            object obj = new PageNavigationKey(1,2);
            //Act
            PageNavigationKey ObjPageNavigationKey = new PageNavigationKey(1, 2);
            var result = ObjPageNavigationKey.Equals(obj);
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
            PageNavigationKey ObjPageNavigationKey = new PageNavigationKey(1, 2);
            var result = ObjPageNavigationKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion
        #region CompareTo_With_PageNavigationKey
        /// <summary>
        /// CompareTo_With_PageNavigationKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_PageNavigationKey()
        {

            //Act
            PageNavigationKey ObjPageNavigationKey = new PageNavigationKey(1, 2);
            var result = ObjPageNavigationKey.CompareTo(ObjPageNavigationKey);
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
            PageNavigationKey ObjPageNavigationKey = new PageNavigationKey(1, 2);
            try
            {
                var result = ObjPageNavigationKey.CompareTo(obj);
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
        #region Create_returns_PageNavigationKey
        /// <summary>
        /// Create_returns_PageNavigationKey
        /// </summary>
        [TestMethod]
        public void Create_returns_PageNavigationKey()
        {
            //Act
            var result = PageNavigationKey.Create(1, 2);
            //Assert
            Assert.IsInstanceOfType(result, typeof(PageNavigationKey));
        }
        #endregion
        #region Greater_Than_With_PageNavigationKey
        /// <summary>
        /// Greater_Than_With_PageNavigationKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_PageNavigationKey()
        {
            //Act
            PageNavigationKey key1 = new PageNavigationKey(1, 2);
            PageNavigationKey key2 = new PageNavigationKey(1, 2);
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_With_PageNavigationKey
        /// <summary>
        /// Less_Than_With_PageNavigationKey
        /// </summary>
        [TestMethod]
        public void Less_Than_With_PageNavigationKey()
        {
            //Act
            PageNavigationKey key1 = new PageNavigationKey(1, 2);
            PageNavigationKey key2 = new PageNavigationKey(1, 2);
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Greater_Than_are_equalsto_With_PageNavigationKey
        /// <summary>
        /// Greater_Than_are_equalsto_With_PageNavigationKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_are_equalsto_With_PageNavigationKey()
        {
            //Act
            PageNavigationKey key1 = new PageNavigationKey(1, 2);
            PageNavigationKey key2 = new PageNavigationKey(1, 2);
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_are_equalsto_With_PageNavigationKey
        /// <summary>
        /// Less_Than_are_equalsto_With_PageNavigationKey
        /// </summary>
        [TestMethod]
        public void Less_Than_are_equalsto_With_PageNavigationKey()
        {
            //Act
            PageNavigationKey key1 = new PageNavigationKey(1, 2);
            PageNavigationKey key2 = new PageNavigationKey(1, 2);
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Equalsto_With_PageNavigationKey
        /// <summary>
        /// Equalsto_With_PageNavigationKey
        /// </summary>
        [TestMethod]
        public void Equalsto_With_PageNavigationKey()
        {
            //Act
            PageNavigationKey key1 = new PageNavigationKey(1, 2);
            PageNavigationKey key2 = new PageNavigationKey(1, 2);
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Not_Equalsto_With_PageNavigationKey
        /// <summary>
        /// Not_Equalsto_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void Not_Equalsto_With_PageNavigationKey()
        {
            //Act
            PageNavigationKey key1 = new PageNavigationKey(1, 2);
            PageNavigationKey key2 = new PageNavigationKey(1, 2);
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
