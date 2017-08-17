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
    public class TemplatePageFeatureKeyTests
    {

        #region Equals_With_TemplatePageFeatureKey
        /// <summary>
        /// Equals_With_TemplatePageFeatureKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TemplatePageFeatureKey()
        {
            //Act
            TemplatePageFeatureKey objTemplatePageFeatureKey = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            bool result = objTemplatePageFeatureKey.Equals(objTemplatePageFeatureKey);
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
            TemplatePageFeatureKey objTemplatePageFeatureKey = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            object obj = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            bool result = objTemplatePageFeatureKey.Equals(obj);
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
            TemplatePageFeatureKey objTemplatePageFeatureKey = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            int result = objTemplatePageFeatureKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion

        #region CompareTo_With_TemplatePageFeatureKey
        /// <summary>
        /// CompareTo_With_TemplatePageFeatureKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_TemplatePageFeatureKey()
        {
            //Act
            TemplatePageFeatureKey objTemplatePageFeatureKey = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            int result = objTemplatePageFeatureKey.CompareTo(objTemplatePageFeatureKey);
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
            TemplatePageFeatureKey objTemplatePageFeatureKey = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            try
            {
                objTemplatePageFeatureKey.CompareTo(obj);
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

        #region Create_With_templateId_pageId_featureKey
        /// <summary>
        /// Create_With_templateId_pageId_featureKey
        /// </summary>
        [TestMethod]
        public void Create_With_templateId_pageId_featureKey()
        {
            //Act
            TemplatePageFeatureKey result = TemplatePageFeatureKey.Create(1, 1, "FeatureKey");
            //Assert
            Assert.IsInstanceOfType(result, typeof(TemplatePageFeatureKey));
        }
        #endregion

        #region Greater_Than_With_TemplatePageFeatureKeys
        /// <summary>
        /// Greater_Than_With_TemplatePageFeatureKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TemplatePageFeatureKeys()
        {
            //Act
            TemplatePageFeatureKey key1 = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            TemplatePageFeatureKey key2 = new TemplatePageFeatureKey(1, 2, "FeatureKey");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_With_TemplatePageFeatureKeys
        /// <summary>
        /// Less_Than_With_TemplatePageFeatureKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TemplatePageFeatureKeys()
        {
            //Act
            TemplatePageFeatureKey key1 = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            TemplatePageFeatureKey key2 = new TemplatePageFeatureKey(1, 2, "FeatureKey");
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Greater_Than_Equal_To_With_TemplatePageFeatureKeys
        /// <summary>
        /// Greater_Than_Equal_To_With_TemplatePageFeatureKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_Equal_To_With_TemplatePageFeatureKeys()
        {
            //Act
            TemplatePageFeatureKey key1 = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            TemplatePageFeatureKey key2 = new TemplatePageFeatureKey(1, 2, "FeatureKey");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_Equal_To_With_TemplatePageFeatureKeys
        /// <summary>
        /// Less_Than_Equal_To_With_TemplatePageFeatureKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_Equal_To_With_TemplatePageFeatureKeys()
        {
            //Act
            TemplatePageFeatureKey key1 = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            TemplatePageFeatureKey key2 = new TemplatePageFeatureKey(1, 2, "FeatureKey");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Equals_To_With_TemplatePageFeatureKeys
        /// <summary>
        /// Equals_To_With_TemplatePageFeatureKeys
        /// </summary>
        [TestMethod]
        public void Equals_To_With_TemplatePageFeatureKeys()
        {
            //Act
            TemplatePageFeatureKey key1 = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            TemplatePageFeatureKey key2 = new TemplatePageFeatureKey(1, 2, "FeatureKey");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Not_Equals_To_With_TemplatePageFeatureKeys
        /// <summary>
        /// Not_Equals_To_With_TemplatePageFeatureKeys
        /// </summary>
        [TestMethod]
        public void Not_Equals_To_With_TemplatePageFeatureKeys()
        {
            //Act
            TemplatePageFeatureKey key1 = new TemplatePageFeatureKey(1, 1, "FeatureKey");
            TemplatePageFeatureKey key2 = new TemplatePageFeatureKey(1, 2, "FeatureKey");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

    }
}
