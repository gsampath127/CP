using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for BaseController class
    /// </summary>
    [TestClass]
    public class BaseControllerTests
    {
        //#region GetBaseUrl_Returns_String
        ///// <summary>
        ///// GetBaseUrl_Returns_String
        ///// </summary>
        //[TestMethod]
        //public void GetBaseUrl_Returns_String()
        //{
        //    //Arrange
        //    // Mocking Context - starts
        //    var moqContext = new Mock<HttpContextBase>();
        //    var moqRequest = new Mock<HttpRequestBase>();
        //    //var formValues = new NameValueCollection
        //    //{
        //    //   { "hdnClients", "1,2,3" }               
        //    //};
        //    moqRequest.Setup(r => r.Url.GetLeftPart(It.IsAny < UriPartial>())).Returns("TestUrl");
        //    moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
        //    // Mocking Context - ends

        //    //Act
        //    BaseController objController = new BaseController();
        //    //Mocking the context of controller
        //    objController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objController);

        //    var result = objController.GetBaseUrl();

        //    // Verify and Assert
        //    Assert.IsInstanceOfType(result, typeof(string));
        //}
        //#endregion

        #region SessionTimedOut_Returns_ActionResult
        /// <summary>
        /// SessionTimedOut_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void SessionTimedOut_Returns_ActionResult()
        {
            //Arrange

            //Act
            BaseController objController = new BaseController();
            var result = objController.SessionTimedOut();

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region RPFormatDate_Returns_String_Nullable
        /// <summary>
        /// RPFormatDate_Returns_String_Nullable
        /// </summary>
        [TestMethod]
        public void RPFormatDate_Returns_String_Nullable()
        {
            //Arrange

            //Act
            BaseController objController = new BaseController();
            var result = objController.RPFormatDate(null);

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(string));
        }
        #endregion

        #region RPFormatDate_Returns_String
        /// <summary>
        /// RPFormatDate_Returns_String
        /// </summary>
        [TestMethod]
        public void RPFormatDate_Returns_String()
        {
            //Arrange

            //Act
            BaseController objController = new BaseController();
            var result = objController.RPFormatDate(DateTime.Now);

            // Verify and Assert
            Assert.IsInstanceOfType(result, typeof(string));
        }
        #endregion

    }
}
