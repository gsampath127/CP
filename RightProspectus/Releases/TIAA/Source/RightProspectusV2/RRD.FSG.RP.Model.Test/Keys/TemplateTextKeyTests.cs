using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRD.FSG.RP.Model.Test.Keys
{
    [TestClass]
    public class TemplateTextKeyTests
    {
        
        #region Equals_With_TemplateTextKey
        /// <summary>
        /// Equals_With_TemplateTextKey
        /// </summary>
        [TestMethod]
        public void Equals_With_TemplateTextKey()
        {
            //Act
            TemplateTextKey objTemplateTextKey = new TemplateTextKey(1, "ResourceKey");
            bool result = objTemplateTextKey.Equals(objTemplateTextKey);
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
            TemplateTextKey objTemplateTextKey = new TemplateTextKey(1, "ResourceKey");
            object obj = new TemplateTextKey(1, "ResourceKey");
            bool result = objTemplateTextKey.Equals(obj);
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
            TemplateTextKey objTemplateTextKey = new TemplateTextKey(1, "ResourceKey");
            int result = objTemplateTextKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
        #endregion

        #region CompareTo_With_TemplateTextKey
        /// <summary>
        /// CompareTo_With_TemplateTextKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_TemplateTextKey()
        {
            //Act
            TemplateTextKey objTemplateTextKey = new TemplateTextKey(1, "ResourceKey");
            int result = objTemplateTextKey.CompareTo(objTemplateTextKey);
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
            object obj = new TemplateTextKey(1, "ResourceKey");
            //Act
            TemplateTextKey objTemplateTextKey = new TemplateTextKey(1, "ResourceKey");
            try
            {
                objTemplateTextKey.CompareTo(obj);
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

        #region Create_With_templateId_resourceKey
        /// <summary>
        /// Create_With_templateId_resourceKey
        /// </summary>
        [TestMethod]
        public void Create_With_templateId_resourceKey()
        {
            //Act
            TemplateTextKey result = TemplateTextKey.Create(1, "ResourceKey");
            //Assert
            Assert.IsInstanceOfType(result, typeof(TemplateTextKey));
        }
        #endregion

        #region Greater_Than_With_TemplateTextKeys
        /// <summary>
        /// Greater_Than_With_TemplateTextKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_TemplateTextKeys()
        {
            //Act
            TemplateTextKey key1 = new TemplateTextKey(1, "ResourceKey");
            TemplateTextKey key2 = new TemplateTextKey(2, "ResourceKey");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_With_TemplateTextKeys
        /// <summary>
        /// Less_Than_With_TemplateTextKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_With_TemplateTextKeys()
        {
            //Act
            TemplateTextKey key1 = new TemplateTextKey(1, "ResourceKey");
            TemplateTextKey key2 = new TemplateTextKey(2, "ResourceKey");
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Greater_Than_Equal_To_With_TemplateTextKeys
        /// <summary>
        /// Greater_Than_Equal_To_With_TemplateTextKeys
        /// </summary>
        [TestMethod]
        public void Greater_Than_Equal_To_With_TemplateTextKeys()
        {
            //Act
            TemplateTextKey key1 = new TemplateTextKey(1, "ResourceKey");
            TemplateTextKey key2 = new TemplateTextKey(2, "ResourceKey");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Less_Than_Equal_To_With_TemplateTextKeys
        /// <summary>
        /// Less_Than_Equal_To_With_TemplateTextKeys
        /// </summary>
        [TestMethod]
        public void Less_Than_Equal_To_With_TemplateTextKeys()
        {
            //Act
            TemplateTextKey key1 = new TemplateTextKey(1, "ResourceKey");
            TemplateTextKey key2 = new TemplateTextKey(2, "ResourceKey");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Equals_To_With_TemplateTextKeys
        /// <summary>
        /// Equals_To_With_TemplateTextKeys
        /// </summary>
        [TestMethod]
        public void Equals_To_With_TemplateTextKeys()
        {
            //Act
            TemplateTextKey key1 = new TemplateTextKey(1, "ResourceKey");
            TemplateTextKey key2 = new TemplateTextKey(2, "ResourceKey");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

        #region Not_Equals_To_With_TemplateTextKeys
        /// <summary>
        /// Not_Equals_To_With_TemplateTextKeys
        /// </summary>
        [TestMethod]
        public void Not_Equals_To_With_TemplateTextKeys()
        {
            //Act
            TemplateTextKey key1 = new TemplateTextKey(1, "ResourceKey");
            TemplateTextKey key2 = new TemplateTextKey(2, "ResourceKey");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion

    }
}
