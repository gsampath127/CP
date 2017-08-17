using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    ///  Test class for ErrorController class
    /// </summary>
    [TestClass]
    public class ErrorControllerTests
    {
        #region Index_Returns_ActionResult
        /// <summary>
        /// Index_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void Index_Returns_ActionResult()
        {
            //Arrange

            //Act
            ErrorController objController = new ErrorController();
            var result = objController.Index("Default");

            // Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result,typeof(ActionResult));
        }
        #endregion

        #region Index_Returns_ActionResult_Error404
        /// <summary>
        /// Index_Returns_ActionResult_Error404
        /// </summary>
        [TestMethod]
        public void Index_Returns_ActionResult_Error404()
        {
            //Arrange

            //Act
            ErrorController objController = new ErrorController();
            var result = objController.Index("404");

            // Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region Index_Returns_ActionResult_Error500
        /// <summary>
        /// Index_Returns_ActionResult_Error500
        /// </summary>
        [TestMethod]
        public void Index_Returns_ActionResult_Error500()
        {
            //Arrange

            //Act
            ErrorController objController = new ErrorController();
            var result = objController.Index("500");

            // Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
        
        #region Index_Returns_ActionResult_Error600
        /// <summary>
        /// Index_Returns_ActionResult_Error600
        /// </summary>
        [TestMethod]
        public void Index_Returns_ActionResult_Error600()
        {
            //Arrange

            //Act
            ErrorController objController = new ErrorController();
            var result = objController.Index("600");

            // Verify and Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
    }
}
