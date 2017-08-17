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
    public class DocumentTypeExternalIdKeyTests
    {
        #region Equals_With_DocumentTypeExternalIdKey
        /// <summary>
        /// Equals_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void Equals_With_DocumentTypeExternalIdKey()
        {

            //Act
            DocumentTypeExternalIdKey ObjDocumentTypeExternalIdKey = new DocumentTypeExternalIdKey(1, "DTEId");
            var result = ObjDocumentTypeExternalIdKey.Equals(ObjDocumentTypeExternalIdKey);
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));

        }
        #endregion
        #region Equals_With_object
        /// <summary>
        /// Equals_With_object
        /// </summary>
        [TestMethod]
        public void Equals_With_object()
        {
            //Arrange
            object Obj = new DocumentTypeExternalIdKey(1,"CUDHId");

            //Act
            DocumentTypeExternalIdKey ObjDocumentTypeExternalIdKey = new DocumentTypeExternalIdKey(1, "DTEId");
            var result = ObjDocumentTypeExternalIdKey.Equals(Obj);
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
            //Arrange
            object Obj = new object();

            //Act
            DocumentTypeExternalIdKey ObjDocumentTypeExternalIdKey = new DocumentTypeExternalIdKey(1, "DTEId");
            var result = ObjDocumentTypeExternalIdKey.GetHashCode();
            //Assert
            Assert.IsInstanceOfType(result, typeof(int));

        }
        #endregion
        #region CompareTo_With_DocumentTypeExternalIdKey
        /// <summary>
        /// CompareTo_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_DocumentTypeExternalIdKey()
        {

            //Act
            DocumentTypeExternalIdKey ObjDocumentTypeExternalIdKey = new DocumentTypeExternalIdKey(1, "DTEId");
            var result = ObjDocumentTypeExternalIdKey.CompareTo(ObjDocumentTypeExternalIdKey);
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
            object Obj = new object();
            Exception exe = null;
            //Act
            DocumentTypeExternalIdKey ObjDocumentTypeExternalIdKey = new DocumentTypeExternalIdKey(1, "DTEId");
            try
            {
                ObjDocumentTypeExternalIdKey.CompareTo(Obj);
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
        #region Create
        /// <summary>
        /// Create
        /// </summary>
        [TestMethod]
        public void Create()
        {
            //Arrange
            object Obj = new object();
            Exception exe = null;
            //Act
           

            var result = DocumentTypeExternalIdKey.Create(13, "41");

            //Assert
            Assert.IsInstanceOfType(result, typeof(DocumentTypeExternalIdKey));

        }
        #endregion
        #region Greater_Than_With_DocumentTypeExternalIdKey
        /// <summary>
        /// Greater_Than_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_With_DocumentTypeExternalIdKey()
        {
            //Act
            DocumentTypeExternalIdKey key1 = new DocumentTypeExternalIdKey(1, "Test1");
            DocumentTypeExternalIdKey key2 = new DocumentTypeExternalIdKey(1, "Test2");
            bool result = key2 > key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_With_DocumentTypeExternalIdKey
        /// <summary>
        /// Less_Than_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void Less_Than_With_DocumentTypeExternalIdKey()
        {
            //Act
            DocumentTypeExternalIdKey key1 = new DocumentTypeExternalIdKey(1, "Test1");
            DocumentTypeExternalIdKey key2 = new DocumentTypeExternalIdKey(1, "Test2");
            bool result = key2 < key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Greater_Than_are_equalsto_With_DocumentTypeExternalIdKey
        /// <summary>
        /// Greater_Than_are_equalsto_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void Greater_Than_are_equalsto_With_DocumentTypeExternalIdKey()
        {
            //Act
            DocumentTypeExternalIdKey key1 = new DocumentTypeExternalIdKey(1, "Test1");
            DocumentTypeExternalIdKey key2 = new DocumentTypeExternalIdKey(1, "Test2");
            bool result = key2 >= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Less_Than_are_equalsto_With_DocumentTypeExternalIdKey
        /// <summary>
        /// Less_Than_are_equalsto_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void Less_Than_are_equalsto_With_DocumentTypeExternalIdKey()
        {
            //Act
            DocumentTypeExternalIdKey key1 = new DocumentTypeExternalIdKey(1, "Test1");
            DocumentTypeExternalIdKey key2 = new DocumentTypeExternalIdKey(1, "Test2");
            bool result = key2 <= key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Equalsto_With_DocumentTypeExternalIdKey
        /// <summary>
        /// Equalsto_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void Equalsto_With_DocumentTypeExternalIdKey()
        {
            //Act
            DocumentTypeExternalIdKey key1 = new DocumentTypeExternalIdKey(1, "Test1");
            DocumentTypeExternalIdKey key2 = new DocumentTypeExternalIdKey(1, "Test2");
            bool result = key2 == key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
        #region Not_Equalsto_With_DocumentTypeExternalIdKey
        /// <summary>
        /// Not_Equalsto_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void Not_Equalsto_With_DocumentTypeExternalIdKey()
        {
            //Act
            DocumentTypeExternalIdKey key1 = new DocumentTypeExternalIdKey(1, "Test1");
            DocumentTypeExternalIdKey key2 = new DocumentTypeExternalIdKey(1, "Test2");
            bool result = key2 != key1;
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        #endregion
    }
}
