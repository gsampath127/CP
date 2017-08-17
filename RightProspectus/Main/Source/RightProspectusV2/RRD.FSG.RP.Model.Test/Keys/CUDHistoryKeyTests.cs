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
    public class CUDHistoryKeyTests
    {
        #region Equals_With_CUDHistoryKey
        /// <summary>
        /// Equals_With_CUDHistoryKey
        /// </summary>
        [TestMethod]
        public void Equals_With_CUDHistoryKey()
        {
            //Act
            CUDHistoryKey ObjCUDHistoryKey = new CUDHistoryKey(1, "CUDHistoryID");
            var result = ObjCUDHistoryKey.Equals(ObjCUDHistoryKey);
            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));

        }
        #endregion
        #region CompareTo_With_CUDHistoryKey
        /// <summary>
        /// CompareTo_With_CUDHistoryKey
        /// </summary>
        [TestMethod]
        public void CompareTo_With_CUDHistoryKey()
        {
            //Arrange
            Exception exe = null;

            //Act
            CUDHistoryKey ObjCUDHistoryKey = new CUDHistoryKey(1, "CUDHistoryID");
            try
            {
                ObjCUDHistoryKey.CompareTo(ObjCUDHistoryKey);
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
        #region CompareTo_With_Object
        /// <summary>
        /// CompareTo_With_Object
        /// </summary>
        [TestMethod]
        public void CompareTo_With_Object()
        {
            //Arrange
            Exception exe = null;
            object obj=new object();            
            //Act
            CUDHistoryKey ObjCUDHistoryKey = new CUDHistoryKey(1, "CUDHistoryID");
            try
            {
                ObjCUDHistoryKey.CompareTo(obj);
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
    }
}
