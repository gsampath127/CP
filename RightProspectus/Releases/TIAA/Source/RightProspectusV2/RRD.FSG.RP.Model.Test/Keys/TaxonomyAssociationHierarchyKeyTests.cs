using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class TaxonomyAssociationHierarchyKeyTests
    {
        #region Equals_With_TaxonomyAssociationHierarchyKey
        /// <summary>
        /// Equals_With_TaxonomyAssociationHierarchyKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TaxonomyAssociationHierarchyKey()
        {

            //Act
            TaxonomyAssociationHierarchyKey ObjTaxonomyAssociationHierarchyKey = new TaxonomyAssociationHierarchyKey(16, 26, 5);
            var result = ObjTaxonomyAssociationHierarchyKey.Equals(ObjTaxonomyAssociationHierarchyKey);
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
            object obj = new TaxonomyAssociationHierarchyKey(2, 3, 4);
            TaxonomyAssociationHierarchyKey ObjTaxonomyAssociationHierarchyKey = new TaxonomyAssociationHierarchyKey(16, 26, 5);
            var result = ObjTaxonomyAssociationHierarchyKey.Equals(obj);
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
            TaxonomyAssociationHierarchyKey ObjTaxonomyAssociationHierarchyKey = new TaxonomyAssociationHierarchyKey(16, 26, 5);
            var result = ObjTaxonomyAssociationHierarchyKey.GetHashCode();
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
            TaxonomyAssociationHierarchyKey ObjTaxonomyAssociationHierarchyKey = new TaxonomyAssociationHierarchyKey(16, 26, 5);
            var result = ObjTaxonomyAssociationHierarchyKey.CompareTo(ObjTaxonomyAssociationHierarchyKey);
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
            TaxonomyAssociationHierarchyKey ObjTaxonomyAssociationHierarchyKey = new TaxonomyAssociationHierarchyKey(16, 26, 5);
            try
            {
                var result = ObjTaxonomyAssociationHierarchyKey.CompareTo(obj);
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
        #region Create_returns_TaxonomyAssociationHierarchyKey
        /// <summary>
        /// Create_returns_TaxonomyAssociationHierarchyKey
        /// </summary>
        [TestMethod]
        public void Create_returns_TaxonomyAssociationHierarchyKey()
        {

            //Act

            var result = TaxonomyAssociationHierarchyKey.Create(2, 5, 7);
            //Assert
            Assert.IsInstanceOfType(result, typeof(TaxonomyAssociationHierarchyKey));
        }
        #endregion
        #region Greater_Than_With_TaxanomyAssociationHeirarichy
        /// <summary>
        /// Greater_Than_With_TaxanomyAssociationHeirarichy
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TaxanomyAssociationHeirarichy()
        {
            //Act
            TaxonomyAssociationHierarchyKey key1 = new TaxonomyAssociationHierarchyKey(1, 4, 1);
            TaxonomyAssociationHierarchyKey key2 = new TaxonomyAssociationHierarchyKey(1, 3, 4);
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_With_TaxanomyAssociationHeirarichy
        /// <summary>
        /// Less_Than_With_TaxanomyAssociationHeirarichy
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TaxanomyAssociationHeirarichy()
        {
            //Act
            TaxonomyAssociationHierarchyKey key1 = new TaxonomyAssociationHierarchyKey(1, 4, 1);
            TaxonomyAssociationHierarchyKey key2 = new TaxonomyAssociationHierarchyKey(1, 3, 4);
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Greater_Than_are_equalsto_With_TaxanomyAssociationHeirarichy
        /// <summary>
        /// Greater_Than_are_equalsto_With_TaxanomyAssociationHeirarichy
        /// </summary>
        [TestMethod]
        public void Greater_Than_are_equalsto_With_TaxanomyAssociationHeirarichy()
        {
            //Act
            TaxonomyAssociationHierarchyKey key1 = new TaxonomyAssociationHierarchyKey(1, 4, 1);
            TaxonomyAssociationHierarchyKey key2 = new TaxonomyAssociationHierarchyKey(1, 3, 4);
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_are_equalsto_With_TaxanomyAssociationHeirarichy
        /// <summary>
        /// Less_Than_are_equalsto_With_TaxanomyAssociationHeirarichy
        /// </summary>
        [TestMethod]
        public void Less_Than_are_equalsto_With_TaxanomyAssociationHeirarichy()
        {
            //Act
            TaxonomyAssociationHierarchyKey key1 = new TaxonomyAssociationHierarchyKey(1, 4, 1);
            TaxonomyAssociationHierarchyKey key2 = new TaxonomyAssociationHierarchyKey(1, 3, 4);
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Equalsto_With_TaxanomyAssociationHeirarichy
        /// <summary>
        /// Equalsto_With_TaxanomyAssociationHeirarichy
        /// </summary>
        [TestMethod]
        public void Equalsto_With_TaxanomyAssociationHeirarichy()
        {
            //Act
            TaxonomyAssociationHierarchyKey key1 = new TaxonomyAssociationHierarchyKey(1, 4, 1);
            TaxonomyAssociationHierarchyKey key2 = new TaxonomyAssociationHierarchyKey(1, 3, 4);
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Not_Equalsto_With_TaxanomyAssociationHeirarichy
        /// <summary>
        /// Not_Equalsto_With_TaxanomyAssociationHeirarichy
        /// </summary>
        [TestMethod]
        public void Not_Equalsto_With_TaxanomyAssociationHeirarichy()
        {
            //Act
            TaxonomyAssociationHierarchyKey key1 = new TaxonomyAssociationHierarchyKey(1, 4, 1);
            TaxonomyAssociationHierarchyKey key2 = new TaxonomyAssociationHierarchyKey(1, 3, 4);
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
