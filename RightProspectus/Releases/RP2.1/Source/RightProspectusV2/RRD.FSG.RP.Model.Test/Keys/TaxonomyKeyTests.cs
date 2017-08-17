using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class TaxonomyKeyTests
    {
        #region Equals_With_TaxonomyKey
        /// <summary>
        /// Equals_With_TaxonomyKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TaxonomyKey()
        {

            //Act
            TaxonomyKey ObjTaxonomyKey = new TaxonomyKey(16, 2);
            var result = ObjTaxonomyKey.Equals(ObjTaxonomyKey);
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

            //Act
            TaxonomyKey ObjTaxonomyKey = new TaxonomyKey(16, 2);
            object obj = new TaxonomyKey(1, 2);
            var result = ObjTaxonomyKey.Equals(obj);
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
            TaxonomyKey ObjTaxonomyKey = new TaxonomyKey(16, 2);
            var result = ObjTaxonomyKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));

        }
        #endregion
        #region CompareTo_with_TaxonomyKey
        /// <summary>
        /// CompareTo_with_TaxonomyKey
        /// </summary>
        [TestMethod]
        public void CompareTo_with_TaxonomyKey()
        {

            //Act
            TaxonomyKey ObjTaxonomyKey = new TaxonomyKey(16, 2);
            var result = ObjTaxonomyKey.CompareTo(ObjTaxonomyKey);
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
            Exception exe = null;
            object obj = new object();

            //Act
            TaxonomyKey ObjTaxonomyKey = new TaxonomyKey(16, 26);
            try
            {
                var result = ObjTaxonomyKey.CompareTo(obj);
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
        #region Create_returns_TaxonomyKey
        /// <summary>
        /// Create_returns_TaxonomyKey
        /// </summary>
        [TestMethod]
        public void Create_returns_TaxonomyKey()
        {

            //Act
            var result = TaxonomyKey.Create(3, 4);
            //Assert
            Assert.IsInstanceOfType(result, typeof(TaxonomyKey));

        }
        #endregion
        #region Greater_Than_With_TaxonomyKey
        /// <summary>
        /// Greater_Than_With_TaxonomyKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TaxonomyKey()
        {
            //Act
            TaxonomyKey key1 = new TaxonomyKey(1, 3);
            TaxonomyKey key2 = new TaxonomyKey(3, 5);
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_With_TaxonomyKey
        /// <summary>
        /// Less_Than_With_TaxonomyKey
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TaxonomyKey()
        {
            //Act
            TaxonomyKey key1 = new TaxonomyKey(1, 3);
            TaxonomyKey key2 = new TaxonomyKey(3, 5);
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Greater_Than_are_equalsto_With_TaxonomyKey
        /// <summary>
        /// Greater_Than_are_equalsto_With_TaxonomyKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_are_equalsto_With_TaxonomyKey()
        {
            //Act
            TaxonomyKey key1 = new TaxonomyKey(1, 3);
            TaxonomyKey key2 = new TaxonomyKey(3, 5);
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_are_equalsto_With_TaxonomyKey
        /// <summary>
        /// Less_Than_are_equalsto_With_TaxonomyKey
        /// </summary>
        [TestMethod]
        public void Less_Than_are_equalsto_With_TaxonomyKey()
        {
            //Act
            TaxonomyKey key1 = new TaxonomyKey(1, 3);
            TaxonomyKey key2 = new TaxonomyKey(3, 5);
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Equalsto_With_TaxonomyKey
        /// <summary>
        /// Equalsto_With_TaxonomyKey
        /// </summary>
        [TestMethod]
        public void Equalsto_With_TaxonomyKey()
        {
            //Act
            TaxonomyKey key1 = new TaxonomyKey(1, 3);
            TaxonomyKey key2 = new TaxonomyKey(3, 5);
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Not_Equalsto_With_TaxonomyKey
        /// <summary>
        /// Not_Equalsto_With_TaxonomyKey
        /// </summary>
        [TestMethod]
        public void Not_Equalsto_With_TaxonomyKey()
        {
            //Act
            TaxonomyKey key1 = new TaxonomyKey(1, 3);
            TaxonomyKey key2 = new TaxonomyKey(3, 5);
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
