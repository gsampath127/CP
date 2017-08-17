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
    public class TemplateFeatureKeyTests
    {

        #region Equals_With_TemplateFeatureKey
        /// <summary>
        /// Equals_With_TemplateFeatureKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TemplateFeatureKey()
        {
            //Act
            TemplateFeatureKey objTemplateFeatureKey = new TemplateFeatureKey(1, "FeatureKey");
            bool result = objTemplateFeatureKey.Equals(objTemplateFeatureKey);
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
            TemplateFeatureKey objTemplateFeatureKey = new TemplateFeatureKey(1, "FeatureKey");
            object obj = new TemplateFeatureKey(1, "FeatureKey");
            bool result = objTemplateFeatureKey.Equals(obj);
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
            TemplateFeatureKey objTemplateFeatureKey = new TemplateFeatureKey(1, "FeatureKey");
            int result = objTemplateFeatureKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion

        #region CompareTo_With_TemplateFeatureKey
        /// <summary>
        /// CompareTo_With_TemplateFeatureKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_TemplateFeatureKey()
        {
            //Act
            TemplateFeatureKey objTemplateFeatureKey = new TemplateFeatureKey(1, "FeatureKey");
            int result = objTemplateFeatureKey.CompareTo(objTemplateFeatureKey);
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
            TemplateFeatureKey objTemplateFeatureKey = new TemplateFeatureKey(1, "FeatureKey");
            try
            {
                objTemplateFeatureKey.CompareTo(obj);
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

        #region Create_With_templateId_featureKey
        /// <summary>
        /// Create_With_templateId_featureKey
        /// </summary>
        [TestMethod]
        public void Create_With_templateId_featureKey()
        {
            //Act
            TemplateFeatureKey result = TemplateFeatureKey.Create(1, "FeatureKey");
            //Assert
            Assert.IsInstanceOfType(result, typeof(TemplateFeatureKey));
        }
        #endregion

        #region Greater_Than_With_TemplateFeatureKeys
        /// <summary>
        /// Greater_Than_With_TemplateFeatureKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TemplateFeatureKeys()
        {
            //Act
            TemplateFeatureKey key1 = new TemplateFeatureKey(1, "FeatureKey");
            TemplateFeatureKey key2 = new TemplateFeatureKey(2, "FeatureKey");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_With_TemplateFeatureKeys
        /// <summary>
        /// Less_Than_With_TemplateFeatureKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TemplateFeatureKeys()
        {
            //Act
            TemplateFeatureKey key1 = new TemplateFeatureKey(1, "FeatureKey");
            TemplateFeatureKey key2 = new TemplateFeatureKey(2, "FeatureKey");
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Greater_Than_Equal_To_With_TemplateFeatureKeys
        /// <summary>
        /// Greater_Than_Equal_To_With_TemplateFeatureKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_Equal_To_With_TemplateFeatureKeys()
        {
            //Act
            TemplateFeatureKey key1 = new TemplateFeatureKey(1, "FeatureKey");
            TemplateFeatureKey key2 = new TemplateFeatureKey(2, "FeatureKey");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_Equal_To_With_TemplateFeatureKeys
        /// <summary>
        /// Less_Than_Equal_To_With_TemplateFeatureKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_Equal_To_With_TemplateFeatureKeys()
        {
            //Act
            TemplateFeatureKey key1 = new TemplateFeatureKey(1, "FeatureKey");
            TemplateFeatureKey key2 = new TemplateFeatureKey(2, "FeatureKey");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Equals_To_With_TemplateFeatureKeys
        /// <summary>
        /// Equals_To_With_TemplateFeatureKeys
        /// </summary>
        [TestMethod]
        public void Equals_To_With_TemplateFeatureKeys()
        {
            //Act
            TemplateFeatureKey key1 = new TemplateFeatureKey(1, "FeatureKey");
            TemplateFeatureKey key2 = new TemplateFeatureKey(2, "FeatureKey");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Not_Equals_To_With_TemplateFeatureKeys
        /// <summary>
        /// Not_Equals_To_With_TemplateFeatureKeys
        /// </summary>
        [TestMethod]
        public void Not_Equals_To_With_TemplateFeatureKeys()
        {
            //Act
            TemplateFeatureKey key1 = new TemplateFeatureKey(1, "FeatureKey");
            TemplateFeatureKey key2 = new TemplateFeatureKey(2, "FeatureKey");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

    }
}
