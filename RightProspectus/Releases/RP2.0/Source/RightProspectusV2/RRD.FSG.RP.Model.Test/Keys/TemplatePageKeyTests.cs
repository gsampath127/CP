using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class TemplatePageKeyTests
    {

        #region Equals_With_TemplatePageKey
        /// <summary>
        /// Equals_With_TemplatePageKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TemplatePageKey()
        {
            //Act
            TemplatePageKey objTemplatePageKey = new TemplatePageKey(1, 1);
            bool result = objTemplatePageKey.Equals(objTemplatePageKey);
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
            TemplatePageKey objTemplatePageKey = new TemplatePageKey(1, 1);
            object obj = new TemplatePageKey(1, 1);
            bool result = objTemplatePageKey.Equals(obj);
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
            TemplatePageKey objTemplatePageKey = new TemplatePageKey(1, 1);
            int result = objTemplatePageKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion

        #region CompareTo_With_TemplatePageKey
        /// <summary>
        /// CompareTo_With_TemplatePageKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_TemplatePageKey()
        {
            //Act
            TemplatePageKey objTemplatePageKey = new TemplatePageKey(1, 1);
            int result = objTemplatePageKey.CompareTo(objTemplatePageKey);
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
            TemplatePageKey objTemplatePageKey = new TemplatePageKey(1, 1);
            try
            {
                objTemplatePageKey.CompareTo(obj);
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

        #region Create_With_templateId_pageId
        /// <summary>
        /// Create_With_templateId_pageId
        /// </summary>
        [TestMethod]
        public void Create_With_templateId_pageId()
        {
            //Act
            TemplatePageKey result = TemplatePageKey.Create(1, 1);
            //Assert
            Assert.IsInstanceOfType(result, typeof(TemplatePageKey));
        }
        #endregion

        #region Greater_Than_With_TemplatePageKeys
        /// <summary>
        /// Greater_Than_With_TemplatePageKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TemplatePageKeys()
        {
            //Act
            TemplatePageKey key1 = new TemplatePageKey(1, 1);
            TemplatePageKey key2 = new TemplatePageKey(1, 2);
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_With_TemplatePageKeys
        /// <summary>
        /// Less_Than_With_TemplatePageKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TemplatePageKeys()
        {
            //Act
            TemplatePageKey key1 = new TemplatePageKey(1, 1);
            TemplatePageKey key2 = new TemplatePageKey(1, 2);
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Greater_Than_Equal_To_With_TemplatePageKeys
        /// <summary>
        /// Greater_Than_Equal_To_With_TemplatePageKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_Equal_To_With_TemplatePageKeys()
        {
            //Act
            TemplatePageKey key1 = new TemplatePageKey(1, 1);
            TemplatePageKey key2 = new TemplatePageKey(1, 2);
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_Equal_To_With_TemplatePageKeys
        /// <summary>
        /// Less_Than_Equal_To_With_TemplatePageKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_Equal_To_With_TemplatePageKeys()
        {
            //Act
            TemplatePageKey key1 = new TemplatePageKey(1, 1);
            TemplatePageKey key2 = new TemplatePageKey(1, 2);
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Equals_To_With_TemplatePageKeys
        /// <summary>
        /// Equals_To_With_TemplatePageKeys
        /// </summary>
        [TestMethod]
        public void Equals_To_With_TemplatePageKeys()
        {
            //Act
            TemplatePageKey key1 = new TemplatePageKey(1, 1);
            TemplatePageKey key2 = new TemplatePageKey(1, 2);
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Not_Equals_To_With_TemplatePageKeys
        /// <summary>
        /// Not_Equals_To_With_TemplatePageKeys
        /// </summary>
        [TestMethod]
        public void Not_Equals_To_With_TemplatePageKeys()
        {
            //Act
            TemplatePageKey key1 = new TemplatePageKey(1, 1);
            TemplatePageKey key2 = new TemplatePageKey(1, 2);
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

    }
}
