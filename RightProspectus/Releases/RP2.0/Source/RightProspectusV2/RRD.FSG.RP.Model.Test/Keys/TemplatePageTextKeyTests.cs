using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class TemplatePageTextKeyTests
    {

        #region Equals_With_TemplatePageTextKey
        /// <summary>
        /// Equals_With_TemplatePageTextKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TemplatePageTextKey()
        {
            //Act
            TemplatePageTextKey objTemplatePageTextKey = new TemplatePageTextKey(1, 1, "ResourceKey");
            bool result = objTemplatePageTextKey.Equals(objTemplatePageTextKey);
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
            TemplatePageTextKey objTemplatePageTextKey = new TemplatePageTextKey(1, 1, "ResourceKey");
            object obj = new TemplatePageTextKey(1, 1, "ResourceKey");
            bool result = objTemplatePageTextKey.Equals(obj);
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
            TemplatePageTextKey objTemplatePageTextKey = new TemplatePageTextKey(1, 1, "ResourceKey");
            int result = objTemplatePageTextKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion

        #region CompareTo_With_TemplatePageTextKey
        /// <summary>
        /// CompareTo_With_TemplatePageTextKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_TemplatePageTextKey()
        {
            //Act
            TemplatePageTextKey objTemplatePageTextKey = new TemplatePageTextKey(1, 1, "ResourceKey");
            int result = objTemplatePageTextKey.CompareTo(objTemplatePageTextKey);
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
            TemplatePageTextKey objTemplatePageTextKey = new TemplatePageTextKey(1, 1, "ResourceKey");
            try
            {
                objTemplatePageTextKey.CompareTo(obj);
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

        #region Create_With_templateId_pageId_resourceKey
        /// <summary>
        /// Create_With_templateId_pageId_resourceKey
        /// </summary>
        [TestMethod]
        public void Create_With_templateId_pageId_resourceKey()
        {
            //Act
            TemplatePageTextKey result = TemplatePageTextKey.Create(1, 1, "ResourceKey");
            //Assert
            Assert.IsInstanceOfType(result, typeof(TemplatePageTextKey));
        }
        #endregion

        #region Greater_Than_With_TemplatePageTextKeys
        /// <summary>
        /// Greater_Than_With_TemplatePageTextKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TemplatePageTextKeys()
        {
            //Act
            TemplatePageTextKey key1 = new TemplatePageTextKey(1, 1, "ResourceKey");
            TemplatePageTextKey key2 = new TemplatePageTextKey(1, 2, "ResourceKey");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_With_TemplatePageTextKeys
        /// <summary>
        /// Less_Than_With_TemplatePageTextKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TemplatePageTextKeys()
        {
            //Act
            TemplatePageTextKey key1 = new TemplatePageTextKey(1, 1, "ResourceKey");
            TemplatePageTextKey key2 = new TemplatePageTextKey(1, 2, "ResourceKey");
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Greater_Than_Equal_To_With_TemplatePageTextKeys
        /// <summary>
        /// Greater_Than_Equal_To_With_TemplatePageTextKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_Equal_To_With_TemplatePageTextKeys()
        {
            //Act
            TemplatePageTextKey key1 = new TemplatePageTextKey(1, 1, "ResourceKey");
            TemplatePageTextKey key2 = new TemplatePageTextKey(1, 2, "ResourceKey");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_Equal_To_With_TemplatePageTextKeys
        /// <summary>
        /// Less_Than_Equal_To_With_TemplatePageTextKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_Equal_To_With_TemplatePageTextKeys()
        {
            //Act
            TemplatePageTextKey key1 = new TemplatePageTextKey(1, 1, "ResourceKey");
            TemplatePageTextKey key2 = new TemplatePageTextKey(1, 2, "ResourceKey");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Equals_To_With_TemplatePageTextKeys
        /// <summary>
        /// Equals_To_With_TemplatePageTextKeys
        /// </summary>
        [TestMethod]
        public void Equals_To_With_TemplatePageTextKeys()
        {
            //Act
            TemplatePageTextKey key1 = new TemplatePageTextKey(1, 1, "ResourceKey");
            TemplatePageTextKey key2 = new TemplatePageTextKey(1, 2, "ResourceKey");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Not_Equals_To_With_TemplatePageTextKeys
        /// <summary>
        /// Not_Equals_To_With_TemplatePageTextKeys
        /// </summary>
        [TestMethod]
        public void Not_Equals_To_With_TemplatePageTextKeys()
        {
            //Act
            TemplatePageTextKey key1 = new TemplatePageTextKey(1, 1, "ResourceKey");
            TemplatePageTextKey key2 = new TemplatePageTextKey(1, 2, "ResourceKey");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

    }
}
