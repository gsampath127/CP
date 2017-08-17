using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class TaxonomyLevelExternalIdKeyTests
    {

        #region Equals_With_TaxonomyLevelExternalIdKey
        /// <summary>
        /// Equals_With_TaxonomyLevelExternalIdKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TaxonomyLevelExternalIdKey()
        {
            //Act
            TaxonomyLevelExternalIdKey objTaxonomyLevelExternalIdKey = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            bool result = objTaxonomyLevelExternalIdKey.Equals(objTaxonomyLevelExternalIdKey);
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
            TaxonomyLevelExternalIdKey objTaxonomyLevelExternalIdKey = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            object obj = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            bool result = objTaxonomyLevelExternalIdKey.Equals(obj);
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
            TaxonomyLevelExternalIdKey objTaxonomyLevelExternalIdKey = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            int result = objTaxonomyLevelExternalIdKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion

        #region CompareTo_With_TaxonomyLevelExternalIdKey
        /// <summary>
        /// CompareTo_With_TaxonomyLevelExternalIdKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_TaxonomyLevelExternalIdKey()
        {
            //Act
            TaxonomyLevelExternalIdKey objTaxonomyLevelExternalIdKey = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            int result = objTaxonomyLevelExternalIdKey.CompareTo(objTaxonomyLevelExternalIdKey);
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion

        #region CompareTo_With_Object
        /// <summary>
        /// CompareTo_With_Object
        /// </summary>
        [TestMethod]
        public void CompareTo_With_Object()
        {
            //Arrange
            Exception exe = null;
            object obj = new object();
            //Act
            TaxonomyLevelExternalIdKey objTaxonomyLevelExternalIdKey = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            try
            {
                objTaxonomyLevelExternalIdKey.CompareTo(obj);
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

        #region Create_With_level_taxonomyId_externalId
        /// <summary>
        /// Create_With_level_taxonomyId_externalId
        /// </summary>
        [TestMethod]
        public void Create_With_level_taxonomyId_externalId()
        {
            //Act
            TaxonomyLevelExternalIdKey result = TaxonomyLevelExternalIdKey.Create(1, 5507, "ExternalId");
            //Assert
            Assert.IsInstanceOfType(result, typeof(TaxonomyLevelExternalIdKey));
        }
        #endregion

        #region Greater_Than_With_TaxonomyLevelExternalIdKeys
        /// <summary>
        /// Greater_Than_With_TaxonomyLevelExternalIdKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TaxonomyLevelExternalIdKeys()
        {
            //Act
            TaxonomyLevelExternalIdKey key1 = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            TaxonomyLevelExternalIdKey key2 = new TaxonomyLevelExternalIdKey(1, 19929, "ExternalId");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_With_TaxonomyLevelExternalIdKeys
        /// <summary>
        /// Less_Than_With_TaxonomyLevelExternalIdKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TaxonomyLevelExternalIdKeys()
        {
            //Act
            TaxonomyLevelExternalIdKey key1 = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            TaxonomyLevelExternalIdKey key2 = new TaxonomyLevelExternalIdKey(1, 19929, "ExternalId");
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Greater_Than_Equal_To_With_TaxonomyLevelExternalIdKeys
        /// <summary>
        /// Greater_Than_Equal_To_With_TaxonomyLevelExternalIdKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_Equal_To_With_TaxonomyLevelExternalIdKeys()
        {
            //Act
            TaxonomyLevelExternalIdKey key1 = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            TaxonomyLevelExternalIdKey key2 = new TaxonomyLevelExternalIdKey(1, 19929, "ExternalId");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_Equal_To_With_TaxonomyLevelExternalIdKeys
        /// <summary>
        /// Less_Than_Equal_To_With_TaxonomyLevelExternalIdKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_Equal_To_With_TaxonomyLevelExternalIdKeys()
        {
            //Act
            TaxonomyLevelExternalIdKey key1 = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            TaxonomyLevelExternalIdKey key2 = new TaxonomyLevelExternalIdKey(1, 19929, "ExternalId");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Equals_To_With_TaxonomyLevelExternalIdKeys
        /// <summary>
        /// Equals_To_With_TaxonomyLevelExternalIdKeys
        /// </summary>
        [TestMethod]
        public void Equals_To_With_TaxonomyLevelExternalIdKeys()
        {
            //Act
            TaxonomyLevelExternalIdKey key1 = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            TaxonomyLevelExternalIdKey key2 = new TaxonomyLevelExternalIdKey(1, 19929, "ExternalId");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Not_Equals_To_With_TaxonomyLevelExternalIdKeys
        /// <summary>
        /// Not_Equals_To_With_TaxonomyLevelExternalIdKeys
        /// </summary>
        [TestMethod]
        public void Not_Equals_To_With_TaxonomyLevelExternalIdKeys()
        {
            //Act
            TaxonomyLevelExternalIdKey key1 = new TaxonomyLevelExternalIdKey(1, 5507, "ExternalId");
            TaxonomyLevelExternalIdKey key2 = new TaxonomyLevelExternalIdKey(1, 19929, "ExternalId");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

    }
}
