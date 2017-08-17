using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class TemplatePageNavigationKeyTests
    {

        #region Equals_With_TemplatePageNavigationKey
        /// <summary>
        /// Equals_With_TemplatePageNavigationKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TemplatePageNavigationKey()
        {
            //Act
            TemplatePageNavigationKey objTemplatePageNavigationKey = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            bool result = objTemplatePageNavigationKey.Equals(objTemplatePageNavigationKey);
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
            TemplatePageNavigationKey objTemplatePageNavigationKey = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            object obj = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            bool result = objTemplatePageNavigationKey.Equals(obj);
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
            TemplatePageNavigationKey objTemplatePageNavigationKey = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            int result = objTemplatePageNavigationKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion

        #region CompareTo_With_TemplatePageNavigationKey
        /// <summary>
        /// CompareTo_With_TemplatePageNavigationKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_TemplatePageNavigationKey()
        {
            //Act
            TemplatePageNavigationKey objTemplatePageNavigationKey = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            int result = objTemplatePageNavigationKey.CompareTo(objTemplatePageNavigationKey);
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
            TemplatePageNavigationKey objTemplatePageNavigationKey = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            try
            {
                objTemplatePageNavigationKey.CompareTo(obj);
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

        #region Create_With_templateId_pageId_navigationKey
        /// <summary>
        /// Create_With_templateId_pageId_navigationKey
        /// </summary>
        [TestMethod]
        public void Create_With_templateId_pageId_navigationKey()
        {
            //Act
            TemplatePageNavigationKey result = TemplatePageNavigationKey.Create(1, 2, "NavigationKey");
            //Assert
            Assert.IsInstanceOfType(result, typeof(TemplatePageNavigationKey));
        }
        #endregion

        #region Greater_Than_With_TemplatePageNavigationKeys
        /// <summary>
        /// Greater_Than_With_TemplatePageNavigationKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TemplatePageNavigationKeys()
        {
            //Act
            TemplatePageNavigationKey key1 = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            TemplatePageNavigationKey key2 = new TemplatePageNavigationKey(1, 3, "NavigationKey");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_With_TemplatePageNavigationKeys
        /// <summary>
        /// Less_Than_With_TemplatePageNavigationKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TemplatePageNavigationKeys()
        {
            //Act
            TemplatePageNavigationKey key1 = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            TemplatePageNavigationKey key2 = new TemplatePageNavigationKey(1, 3, "NavigationKey");
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Greater_Than_Equal_To_With_TemplatePageNavigationKeys
        /// <summary>
        /// Greater_Than_Equal_To_With_TemplatePageNavigationKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_Equal_To_With_TemplatePageNavigationKeys()
        {
            //Act
            TemplatePageNavigationKey key1 = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            TemplatePageNavigationKey key2 = new TemplatePageNavigationKey(1, 3, "NavigationKey");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_Equal_To_With_TemplatePageNavigationKeys
        /// <summary>
        /// Less_Than_Equal_To_With_TemplatePageNavigationKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_Equal_To_With_TemplatePageNavigationKeys()
        {
            //Act
            TemplatePageNavigationKey key1 = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            TemplatePageNavigationKey key2 = new TemplatePageNavigationKey(1, 3, "NavigationKey");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Equals_To_With_TemplatePageNavigationKeys
        /// <summary>
        /// Equals_To_With_TemplatePageNavigationKeys
        /// </summary>
        [TestMethod]
        public void Equals_To_With_TemplatePageNavigationKeys()
        {
            //Act
            TemplatePageNavigationKey key1 = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            TemplatePageNavigationKey key2 = new TemplatePageNavigationKey(1, 3, "NavigationKey");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Not_Equals_To_With_TemplatePageNavigationKeys
        /// <summary>
        /// Not_Equals_To_With_TemplatePageNavigationKeys
        /// </summary>
        [TestMethod]
        public void Not_Equals_To_With_TemplatePageNavigationKeys()
        {
            //Act
            TemplatePageNavigationKey key1 = new TemplatePageNavigationKey(1, 2, "NavigationKey");
            TemplatePageNavigationKey key2 = new TemplatePageNavigationKey(1, 3, "NavigationKey");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

    }
}
