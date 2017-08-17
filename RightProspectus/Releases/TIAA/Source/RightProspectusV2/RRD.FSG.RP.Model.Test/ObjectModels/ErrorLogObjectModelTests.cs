using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Test.ObjectModels
{
    /// <summary>
    /// Test class for ErrorLogObjectModel class
    /// </summary>
    [TestClass]
    public class ErrorLogObjectModelTests
    {
        #region CompareTo_Returns_Int
        /// <summary>
        /// CompareTo_Returns_Int
        /// </summary>
        [TestMethod]
        public void CompareTo_Returns_Int()
        {
            //Arrange

            //Act
            ErrorLogObjectModel objModel = new ErrorLogObjectModel();
            objModel.ErrorLogId = 1;
            var result = objModel.CompareTo(new ErrorLogObjectModel() { ErrorLogId = 1 });

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(0, result);
        }
        #endregion
        #region Get_Set_ErrorLogObjectModel_ErrorLogId
        /// <summary>
        /// Get_Set_ErrorLogObjectModel_ErrorLogId
        /// </summary>
        [TestMethod]
        public void Get_Set_ErrorLogObjectModel_ErrorLogId()
        {
            ErrorLogObjectModel objModel1 = new ErrorLogObjectModel { ErrorLogId = 3 };
            ErrorLogObjectModel objModel2 = new ErrorLogObjectModel { ErrorLogId = 30 };
            ErrorLogObjectModel objModel3 = new ErrorLogObjectModel { ErrorLogId = 13 };
            Assert.AreEqual(3, objModel1.ErrorLogId);
            Assert.AreEqual(30, objModel2.ErrorLogId);
            Assert.AreEqual(13, objModel3.ErrorLogId);
        }
        #endregion
        #region Get_Set_ErrorLogObjectModel_ErrorCode
        /// <summary>
        /// Get_Set_ErrorLogObjectModel_ErrorCode
        /// </summary>
        [TestMethod]
        public void Get_Set_ErrorLogObjectModel_ErrorCode()
        {
            ErrorLogObjectModel objModel1 = new ErrorLogObjectModel { ErrorCode = 3 };
            ErrorLogObjectModel objModel2 = new ErrorLogObjectModel { ErrorCode = 30 };
            ErrorLogObjectModel objModel3 = new ErrorLogObjectModel { ErrorCode = 13 };
            Assert.AreEqual(3, objModel1.ErrorCode);
            Assert.AreEqual(30, objModel2.ErrorCode);
            Assert.AreEqual(13, objModel3.ErrorCode);
        }
        #endregion
        #region Get_Set_ErrorLogObjectModel_ErrorUtcDate
        /// <summary>
        /// Get_Set_ErrorLogObjectModel_ErrorUtcDate
        /// </summary>
        [TestMethod]
        public void Get_Set_ErrorLogObjectModel_ErrorUtcDate()
        {
            ErrorLogObjectModel objModel1 = new ErrorLogObjectModel { ErrorUtcDate = DateTime.Now };
            ErrorLogObjectModel objModel2 = new ErrorLogObjectModel { ErrorUtcDate = DateTime.MinValue };
            ErrorLogObjectModel objModel3 = new ErrorLogObjectModel { ErrorUtcDate = DateTime.MaxValue };
            Assert.AreEqual(DateTime.Now, objModel1.ErrorUtcDate);
            Assert.AreEqual(DateTime.MinValue, objModel2.ErrorUtcDate);
            Assert.AreEqual(DateTime.MaxValue, objModel3.ErrorUtcDate);
        }
        #endregion
    }
}
