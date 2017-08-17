using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class TemplateNavigationKeyTests
    {

        #region Equals_With_TemplateNavigationKey
        /// <summary>
        /// Equals_With_TemplateNavigationKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TemplateNavigationKey()
        {
            //Act
            TemplateNavigationKey objTemplateNavigationKey = new TemplateNavigationKey(1, "NavigationKey");
            bool result = objTemplateNavigationKey.Equals(objTemplateNavigationKey);
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
            TemplateNavigationKey objTemplateNavigationKey = new TemplateNavigationKey(1, "NavigationKey");
            object obj = new TemplateNavigationKey(1, "NavigationKey");
            bool result = objTemplateNavigationKey.Equals(obj);
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
            TemplateNavigationKey objTemplateNavigationKey = new TemplateNavigationKey(1, "NavigationKey");
            int result = objTemplateNavigationKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion

        #region CompareTo_With_TemplateNavigationKey
        /// <summary>
        /// CompareTo_With_TemplateNavigationKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_TemplateNavigationKey()
        {
            //Act
            TemplateNavigationKey objTemplateNavigationKey = new TemplateNavigationKey(1, "NavigationKey");
            int result = objTemplateNavigationKey.CompareTo(objTemplateNavigationKey);
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
            TemplateNavigationKey objTemplateNavigationKey = new TemplateNavigationKey(1, "NavigationKey");
            try
            {
                objTemplateNavigationKey.CompareTo(obj);
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

        #region Create_With_templateId_navigationKey
        /// <summary>
        /// Create_With_templateId_navigationKey
        /// </summary>
        [TestMethod]
        public void Create_With_templateId_navigationKey()
        {
            //Act
            TemplateNavigationKey result = TemplateNavigationKey.Create(1, "NavigationKey");
            //Assert
            Assert.IsInstanceOfType(result, typeof(TemplateNavigationKey));
        }
        #endregion

        #region Greater_Than_With_TemplateNavigationKeys
        /// <summary>
        /// Greater_Than_With_TemplateNavigationKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TemplateNavigationKeys()
        {
            //Act
            TemplateNavigationKey key1 = new TemplateNavigationKey(1, "NavigationKey");
            TemplateNavigationKey key2 = new TemplateNavigationKey(2, "NavigationKey");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_With_TemplateNavigationKeys
        /// <summary>
        /// Less_Than_With_TemplateNavigationKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TemplateNavigationKeys()
        {
            //Act
            TemplateNavigationKey key1 = new TemplateNavigationKey(1, "NavigationKey");
            TemplateNavigationKey key2 = new TemplateNavigationKey(2, "NavigationKey");
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Greater_Than_Equal_To_With_TemplateNavigationKeys
        /// <summary>
        /// Greater_Than_Equal_To_With_TemplateNavigationKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_Equal_To_With_TemplateNavigationKeys()
        {
            //Act
            TemplateNavigationKey key1 = new TemplateNavigationKey(1, "NavigationKey");
            TemplateNavigationKey key2 = new TemplateNavigationKey(2, "NavigationKey");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_Equal_To_With_TemplateNavigationKeys
        /// <summary>
        /// Less_Than_Equal_To_With_TemplateNavigationKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_Equal_To_With_TemplateNavigationKeys()
        {
            //Act
            TemplateNavigationKey key1 = new TemplateNavigationKey(1, "NavigationKey");
            TemplateNavigationKey key2 = new TemplateNavigationKey(2, "NavigationKey");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Equals_To_With_TemplateNavigationKeys
        /// <summary>
        /// Equals_To_With_TemplateNavigationKeys
        /// </summary>
        [TestMethod]
        public void Equals_To_With_TemplateNavigationKeys()
        {
            //Act
            TemplateNavigationKey key1 = new TemplateNavigationKey(1, "NavigationKey");
            TemplateNavigationKey key2 = new TemplateNavigationKey(2, "NavigationKey");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Not_Equals_To_With_TemplateNavigationKeys
        /// <summary>
        /// Not_Equals_To_With_TemplateNavigationKeys
        /// </summary>
        [TestMethod]
        public void Not_Equals_To_With_TemplateNavigationKeys()
        {
            //Act
            TemplateNavigationKey key1 = new TemplateNavigationKey(1, "NavigationKey");
            TemplateNavigationKey key2 = new TemplateNavigationKey(2, "NavigationKey");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

    }
}
