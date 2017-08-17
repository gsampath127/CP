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
    public class PageFeatureKeyTests
    {
        #region Equals_With_PageFeatureKey
        /// <summary>
        /// Equals_With_PageFeatureKey
        /// </summary>
        [TestMethod]
        public void Equals_With_PageFeatureKey()
        {

            //Act
            PageFeatureKey ObjPageFeatureKey = new PageFeatureKey(1, 2, "PFId");
            var result = ObjPageFeatureKey.Equals(ObjPageFeatureKey);
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
            object Obj = new PageFeatureKey(1,2, "PFId");

            //Act
            PageFeatureKey ObjPageFeatureKey = new PageFeatureKey(1, 2, "PFId");
            var result = ObjPageFeatureKey.Equals(Obj);
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
            PageFeatureKey ObjPageFeatureKey = new PageFeatureKey(1, 2, "PFId");
            var result = ObjPageFeatureKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));

        }
        #endregion
        #region CompareTo_With_PageFeatureKey
        /// <summary>
        ///  CompareTo_With_PageFeatureKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_PageFeatureKey()
        {

            //Act
            PageFeatureKey ObjPageFeatureKey = new PageFeatureKey(1, 2, "PFId");
            var result = ObjPageFeatureKey.CompareTo(ObjPageFeatureKey);
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));

        }
        #endregion
        #region Create
        /// <summary>
        /// Create
        /// </summary>
        [TestMethod]
        public void Create()
        {
                     
            //Act
            var result = PageFeatureKey.Create(1, 2, "PFId");
            
            //Assert
            Assert.IsInstanceOfType(result, typeof(PageFeatureKey));

        }
        #endregion
        #region Greater_Than_With_PageFeatureKey
        /// <summary>
        /// Greater_Than_With_PageFeatureKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_PageFeatureKey()
        {
            //Act
            PageFeatureKey key1 = new PageFeatureKey(1,2, "Test1");
            PageFeatureKey key2 = new PageFeatureKey(1, 2, "Test1");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_With_PageFeatureKey
        /// <summary>
        /// Less_Than_With_PageFeatureKey
        /// </summary>
        [TestMethod]
        public void Less_Than_With_PageFeatureKey()
        {
            //Act
            PageFeatureKey key1 = new PageFeatureKey(1, 2, "Test1");
            PageFeatureKey key2 = new PageFeatureKey(1, 2, "Test1");
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Greater_Than_are_equalsto_With_PageFeatureKey
        /// <summary>
        /// Greater_Than_are_equalsto_With_PageFeatureKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_are_equalsto_With_PageFeatureKey()
        {
            //Act
            PageFeatureKey key1 = new PageFeatureKey(1, 2, "Test1");
            PageFeatureKey key2 = new PageFeatureKey(1, 2, "Test1");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_are_equalsto_With_PageFeatureKey
        /// <summary>
        /// Less_Than_are_equalsto_With_PageFeatureKey
        /// </summary>
        [TestMethod]
        public void Less_Than_are_equalsto_With_PageFeatureKey()
        {
            //Act
            PageFeatureKey key1 = new PageFeatureKey(1, 2, "Test1");
            PageFeatureKey key2 = new PageFeatureKey(1, 2, "Test1");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Equalsto_With_PageFeatureKey
        /// <summary>
        /// Equalsto_With_PageFeatureKey
        /// </summary>
        [TestMethod]
        public void Equalsto_With_PageFeatureKey()
        {
            //Act
            PageFeatureKey key1 = new PageFeatureKey(1, 2, "Test1");
            PageFeatureKey key2 = new PageFeatureKey(1, 2, "Test1");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Not_Equalsto_With_PageFeatureKey
        /// <summary>
        /// Not_Equalsto_With_PageFeatureKey
        /// </summary>
        [TestMethod]
        public void Not_Equalsto_With_PageFeatureKey()
        {
            //Act
            PageFeatureKey key1 = new PageFeatureKey(1, 2, "Test1");
            PageFeatureKey key2 = new PageFeatureKey(1, 2, "Test1");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
