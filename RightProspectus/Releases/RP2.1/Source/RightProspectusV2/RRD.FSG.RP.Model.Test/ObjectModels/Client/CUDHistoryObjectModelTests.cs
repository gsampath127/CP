using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Entities.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Test.ObjectModels.Client
{
    /// <summary>
    /// Test class for 
    /// class
    /// </summary>
    [TestClass]
    public class CUDHistoryObjectModelTests
    {
        #region CompareTo_Returns_Int
        /// <summary>
        /// CompareTo_Returns_Int
        /// </summary>
        [TestMethod]
        public void CompareTo_Returns_Int()
        {
            //Arrange
            Exception exe = null;

            //Act
            CUDHistoryObjectModel objModel = new CUDHistoryObjectModel();
            try
            {
                objModel.CompareTo(new CUDHistoryObjectModel());
            }
            catch (Exception e)
            {
                if (e is NotImplementedException)
                    exe = e;
            }

            // Verify and Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        }
        #endregion

        #region CUDHistoryId
        /// <summary>
        /// CUDHistoryId
        /// </summary>
        [TestMethod]
        public void CUDHistoryId_StoresCorrectly()
        {
            var HistoryId = new CUDHistoryObjectModel();
            HistoryId.CUDHistoryId = 1;
            Assert.AreEqual(1, HistoryId.CUDHistoryId);
        }
        #endregion

        #region TableName
        /// <summary>
        /// TableName
        /// </summary>
        [TestMethod]
        public void TableName_StoresCorrectly()
        {
            var TbleName = new CUDHistoryObjectModel();
            TbleName.TableName = "Tst";
            Assert.AreEqual("Tst", TbleName.TableName);
        }
        #endregion

        #region CUDKey
        /// <summary>
        /// CUDKey
        /// </summary>
        [TestMethod]
        public void CUDKey_StoresCorrectly()
        {
            var Key = new CUDHistoryObjectModel();
            Key.CUDKey = 1;
            Assert.AreEqual(1, Key.CUDKey);
        }
        #endregion

        #region SecondKey
        /// <summary>
        /// SecondKey
        /// </summary>
        [TestMethod]
        public void SecondKey_StoresCorrectly()
        {
            var SecKey = new CUDHistoryObjectModel();
            SecKey.SecondKey = "Tst";
            Assert.AreEqual("Tst", SecKey.SecondKey);
        }
        #endregion

        #region ThirdKey
        /// <summary>
        /// ThirdKey
        /// </summary>
        [TestMethod]
        public void ThirdKey_StoresCorrectly()
        {
            var Thrdkey = new CUDHistoryObjectModel();
            Thrdkey.ThirdKey = "Tst";
            Assert.AreEqual("Tst", Thrdkey.ThirdKey);
        }
        #endregion

        #region CUDType
        /// <summary>
        /// CUDType
        /// </summary>
        [TestMethod]
        public void CUDType_StoresCorrectly()
        {
            var Type = new CUDHistoryObjectModel();
            Type.CUDType = "Tst";
            Assert.AreEqual("Tst", Type.CUDType);
        }
        #endregion

        #region UtcCUDDate
        /// <summary>
        /// UtcCUDDate
        /// </summary>
        [TestMethod]
        public void UtcCUDDate_StoresCorrectly()
        {
            var Date = new CUDHistoryObjectModel();
            Date.UtcCUDDate = DateTime.Now;
            Assert.AreEqual(DateTime.Now, Date.UtcCUDDate);
        }
        #endregion

        #region BatchId
        /// <summary>
        /// BatchId
        /// </summary>
        [TestMethod]
        public void BatchId_StoresCorrectly()
        {
            var BId = new CUDHistoryObjectModel();
            BId.BatchId = Guid.Empty;
            Assert.AreEqual(Guid.Empty, BId.BatchId);
        }
        #endregion

        
        

        
        

        
    }
}
