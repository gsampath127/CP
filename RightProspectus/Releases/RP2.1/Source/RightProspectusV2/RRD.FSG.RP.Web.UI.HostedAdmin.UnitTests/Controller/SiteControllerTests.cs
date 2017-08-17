using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.Controllers;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    /// <summary>
    /// Test class for SiteController class
    /// </summary>
    [TestClass]
    public class SiteControllerTests : BaseTestController<SiteViewModel>
    {
        Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>> mockSiteFactoryCache;
        Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>> mockTemplatePageFactoryCache;
        Mock<IFactoryCache<UserFactory, UserObjectModel, int>> mockUserFactoryCache;
        Mock<IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey>> mockTemplateTextFactoryCache;
        Mock<IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey>> mockTemplatePageTextFactoryCache;
        Mock<IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey>> mockTemplateNavigationFactoryCache;
        Mock<IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey>> mockTemplatePageNavigationFactoryCache;

        [TestInitialize]
        public void TestInitialize()
        {
            mockSiteFactoryCache = new Mock<IFactoryCache<SiteFactory, SiteObjectModel, int>>();
            mockTemplatePageFactoryCache = new Mock<IFactoryCache<TemplatePageFactory, TemplatePageObjectModel, TemplatePageKey>>();
            mockUserFactoryCache = new Mock<IFactoryCache<UserFactory, UserObjectModel, int>>();
            mockTemplateTextFactoryCache = new Mock<IFactoryCache<TemplateTextFactory, TemplateTextObjectModel, TemplateTextKey>>();
            mockTemplatePageTextFactoryCache = new Mock<IFactoryCache<TemplatePageTextFactory, TemplatePageTextObjectModel, TemplatePageTextKey>>();
            mockTemplateNavigationFactoryCache = new Mock<IFactoryCache<TemplateNavigationFactory, TemplateNavigationObjectModel, TemplateNavigationKey>>();
            mockTemplatePageNavigationFactoryCache = new Mock<IFactoryCache<TemplatePageNavigationFactory, TemplatePageNavigationObjectModel, TemplatePageNavigationKey>>();
        }

        #region ReturnValues
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageList()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TAL";


            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TAHD";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TADF";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageList2()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 2;
            objTemplatePageObjectModel.PageName = "TAL";


            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 2;
            objTemplatePageObjectModel.PageName = "TAHD";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 2;
            objTemplatePageObjectModel.PageName = "TADF";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 2;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageListTAHD()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();


            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TAHD";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TADF";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);


            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageListTAHDnull()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();
            IEnumerable<SiteObjectModel> IEnum = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lst = new List<SiteObjectModel>();
            SiteObjectModel obj = new SiteObjectModel();
            obj = null;
            lst.Add(obj);

            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TAHD";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TADF";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);



            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;

        }
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageListDEFAULT()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();


            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "tSt";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);


            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageListTADF()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();


            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TADF";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageListTADFnull()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();
            IEnumerable<SiteObjectModel> IEnum = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lst = new List<SiteObjectModel>();
            SiteObjectModel obj = new SiteObjectModel();
            obj = null;
            lst.Add(obj);

            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TADF";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageListTAD()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();


            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TAD";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TADF";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageListTADnull()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();

            IEnumerable<SiteObjectModel> IEnum = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lst = new List<SiteObjectModel>();
            SiteObjectModel obj = new SiteObjectModel();
            obj = null;
            lst.Add(obj);
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TAD";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "TADF";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);
            objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }

        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageListXBRL()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }
        private IEnumerable<TemplatePageObjectModel> CreateTemplatePageListXBRLnull()
        {
            IEnumerable<TemplatePageObjectModel> IEnumTemplatePage = Enumerable.Empty<TemplatePageObjectModel>();
            List<TemplatePageObjectModel> lstTemplatePageObjectModel = new List<TemplatePageObjectModel>();
            IEnumerable<SiteObjectModel> IEnum = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lst = new List<SiteObjectModel>();
            SiteObjectModel obj = new SiteObjectModel();
            obj = null;
            lst.Add(obj);
            TemplatePageObjectModel objTemplatePageObjectModel = new TemplatePageObjectModel();
            objTemplatePageObjectModel.PageID = 1;
            objTemplatePageObjectModel.TemplateID = 1;
            objTemplatePageObjectModel.PageName = "XBRL";
            lstTemplatePageObjectModel.Add(objTemplatePageObjectModel);

            IEnumTemplatePage = lstTemplatePageObjectModel;
            return IEnumTemplatePage;
        }

        private IEnumerable<TemplatePageTextObjectModel> CreateTemplatePageTextList()
        {
            IEnumerable<TemplatePageTextObjectModel> IEnumTemplatePageText = Enumerable.Empty<TemplatePageTextObjectModel>();
            List<TemplatePageTextObjectModel> lstTemplatePageTextObjectModel = new List<TemplatePageTextObjectModel>();
            TemplatePageTextObjectModel objTemplatePageTextObjectModel = new TemplatePageTextObjectModel();
            objTemplatePageTextObjectModel.PageID = 1;
            objTemplatePageTextObjectModel.ResourceKey = "Test";
            objTemplatePageTextObjectModel.DefaultText = "Test_Default";
            lstTemplatePageTextObjectModel.Add(objTemplatePageTextObjectModel);
            IEnumTemplatePageText = lstTemplatePageTextObjectModel;
            return IEnumTemplatePageText;
        }

        private IEnumerable<TemplatePageNavigationObjectModel> CreateTemplatePageNavigationList()
        {
            IEnumerable<TemplatePageNavigationObjectModel> IEnumTemplatePageNav = Enumerable.Empty<TemplatePageNavigationObjectModel>();
            List<TemplatePageNavigationObjectModel> lstTemplatePageNavObjectModel = new List<TemplatePageNavigationObjectModel>();
            TemplatePageNavigationObjectModel objTemplatePageNavObjectModel = new TemplatePageNavigationObjectModel();
            objTemplatePageNavObjectModel.PageID = 1;
            objTemplatePageNavObjectModel.NavigationKey = "Test";
            objTemplatePageNavObjectModel.DefaultNavigationXml = "Test_nav_Xml";
            lstTemplatePageNavObjectModel.Add(objTemplatePageNavObjectModel);
            IEnumTemplatePageNav = lstTemplatePageNavObjectModel;
            return IEnumTemplatePageNav;
        }

        private IEnumerable<SiteObjectModel> CreateSiteList()
        {
            IEnumerable<SiteObjectModel> IEnumSite = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lstSiteObjectModel = new List<SiteObjectModel>();
            SiteObjectModel objSiteObjectModel = new SiteObjectModel();
            objSiteObjectModel.SiteID = 0;
            objSiteObjectModel.Name = "Test_Site";
            objSiteObjectModel.DefaultPageName = "Test_Default";
            objSiteObjectModel.TemplateName = "Test";
            objSiteObjectModel.DefaultPageId = 1;
            objSiteObjectModel.TemplateId = 1;
            objSiteObjectModel.Description = "Test_Description";



            lstSiteObjectModel.Add(objSiteObjectModel);
            IEnumSite = lstSiteObjectModel;
            return IEnumSite;
        }
        private IEnumerable<SiteObjectModel> CreateSiteList_null()
        {
            IEnumerable<SiteObjectModel> IEnumSite = Enumerable.Empty<SiteObjectModel>();
            List<SiteObjectModel> lstSiteObjectModel = new List<SiteObjectModel>();
            SiteObjectModel objSiteObjectModel = new SiteObjectModel();

            lstSiteObjectModel.Add(objSiteObjectModel);
            IEnumSite = lstSiteObjectModel;
            return IEnumSite;
        }

        private IEnumerable<TemplateTextObjectModel> CreateTemplateTextList()
        {
            IEnumerable<TemplateTextObjectModel> IEnumTemplateText = Enumerable.Empty<TemplateTextObjectModel>();
            List<TemplateTextObjectModel> lstTemplateTextObjectModel = new List<TemplateTextObjectModel>();
            TemplateTextObjectModel objTemplateTextObjectModel = new TemplateTextObjectModel();
            objTemplateTextObjectModel.ResourceKey = "Test";
            objTemplateTextObjectModel.DefaultText = "Test_Default";
            lstTemplateTextObjectModel.Add(objTemplateTextObjectModel);
            IEnumTemplateText = lstTemplateTextObjectModel;
            return IEnumTemplateText;
        }

        private IEnumerable<TemplateNavigationObjectModel> CreateTemplateNavigationList()
        {
            IEnumerable<TemplateNavigationObjectModel> IEnumTemplateNavigation = Enumerable.Empty<TemplateNavigationObjectModel>();
            List<TemplateNavigationObjectModel> lstTemplateNavigationObjectModel = new List<TemplateNavigationObjectModel>();
            TemplateNavigationObjectModel objTemplateNavigationObjectModel = new TemplateNavigationObjectModel();
            objTemplateNavigationObjectModel.NavigationKey = "Test";
            objTemplateNavigationObjectModel.DefaultNavigationXml = "Test_navXml";
            lstTemplateNavigationObjectModel.Add(objTemplateNavigationObjectModel);
            IEnumTemplateNavigation = lstTemplateNavigationObjectModel;
            return IEnumTemplateNavigation;
        }
        #endregion

        #region List_Returns_ActionResult
        /// <summary>
        /// List_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void List_Returns_ActionResult()
        {
            //Arrange

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            var result = objSiteController.List();

            //Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region SiteConfiguration_Returns_ActionResult
        /// <summary>
        /// SiteConfiguration_Returns_ActionResult
        /// </summary>
        [TestMethod]
        public void SiteConfiguration_Returns_ActionResult()
        {
            //Arrange

            //Mocking session value - starts
            long sessionValue = 1;
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupSet(x => x.Session["SITE_ID"] = It.IsAny<long>()).Callback((string name, object val) =>
             {
                 httpContext.SetupGet(x => x.Session["SITE_ID"]).Returns(sessionValue);
             });
            //Mocking session value - ends

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
             mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
            mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), objSiteController);

            var result = objSiteController.SiteConfiguration(1, "Test_Site");

            //Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region SiteConfiguration_Returns_ActionResult_null
        /// <summary>
        /// SiteConfiguration_Returns_ActionResult_null
        /// </summary>
        [TestMethod]
        public void SiteConfiguration_Returns_ActionResult_null()
        {
            //Arrange

            //Mocking session value - starts
            long sessionValue = 1;
            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupSet(x => x.Session["SITE_ID"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                httpContext.SetupGet(x => x.Session["SITE_ID"]).Returns(sessionValue);
            });
            //Mocking session value - ends

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
             mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
            mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), objSiteController);

            var result = objSiteController.SiteConfiguration(null, "Test_Site");

            //Assert
            var result1 = result as ViewResult;
            Assert.AreEqual("", result1.ViewName);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region GetAllSiteDetails_Returns_JsonResult
        /// <summary>
        /// GetAllSiteDetails_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetAllSiteDetails_Returns_JsonResult()
        {
            // Arrange
            List<SiteViewModel> lstSiteViewModel = new List<SiteViewModel>();
            SiteViewModel objSiteViewModel = new SiteViewModel();
            objSiteViewModel.DefaultPageID = 1;
            objSiteViewModel.DefaultPageName = "Test_Default";
            objSiteViewModel.Description = "Test_Description";
            objSiteViewModel.SiteID = 0;
            objSiteViewModel.SiteName = "Test_Site";
            objSiteViewModel.TemplateID = 1;
            objSiteViewModel.TemplateName = "Test";

            lstSiteViewModel.Add(objSiteViewModel);




            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteSearchDetail>(), It.IsAny<SiteSortDetail>()))
                .Returns(CreateSiteList());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
           mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
          mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.GetAllSiteDetails("1", "1", "1");

            //Verify and Assert
            ValidateData(lstSiteViewModel, result);
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllSiteDetails_Returns_JsonResult_Emptystring
        /// <summary>
        /// GetAllSiteDetails_Returns_JsonResult_Emptystring
        /// </summary>
        [TestMethod]
        public void GetAllSiteDetails_Returns_JsonResult_Emptystring()
        {
            //Arrange
            List<SiteViewModel> lstSiteViewModel = new List<SiteViewModel>();
            SiteViewModel objSiteViewModel = new SiteViewModel();
            objSiteViewModel.DefaultPageID = 1;
            objSiteViewModel.DefaultPageName = "Test_Default";
            objSiteViewModel.Description = "Test_Description";
            objSiteViewModel.SiteID = 0;
            objSiteViewModel.SiteName = "Test_Site";
            objSiteViewModel.TemplateID = 1;
            objSiteViewModel.TemplateName = "Test";

            lstSiteViewModel.Add(objSiteViewModel);
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteSearchDetail>(), It.IsAny<SiteSortDetail>()))
                .Returns(CreateSiteList());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
           mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
          mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.GetAllSiteDetails(string.Empty, string.Empty, string.Empty);

            //Verify and Assert
            ValidateData(lstSiteViewModel, result);

            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetAllSiteDetails_Returns_JsonResult_String
        /// <summary>
        /// GetAllSiteDetails_Returns_JsonResult_String
        /// </summary>
        [TestMethod]
        public void GetAllSiteDetails_Returns_JsonResult_String()
        {
            //Arrange
            List<SiteViewModel> lstSiteViewModel = new List<SiteViewModel>();
            SiteViewModel objSiteViewModel = new SiteViewModel();
            objSiteViewModel.DefaultPageID = 1;
            objSiteViewModel.DefaultPageName = "Test_Default";
            objSiteViewModel.Description = "Test_Description";
            objSiteViewModel.SiteID = 0;
            objSiteViewModel.SiteName = "Test_Site";
            objSiteViewModel.TemplateID = 1;
            objSiteViewModel.TemplateName = "Test";

            lstSiteViewModel.Add(objSiteViewModel);
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<SiteSearchDetail>(), It.IsAny<SiteSortDetail>()))
                .Returns(CreateSiteList());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
           mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
          mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.GetAllSiteDetails("A", "B", "C");

            //Verify and Assert
            ValidateData(lstSiteViewModel, result);
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetSiteNames_Returns_JsonResult
        /// <summary>
        /// GetSiteNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetSiteNames_Returns_JsonResult()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateSiteList());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
          mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
         mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.GetSiteNames();

            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "Test_Site", Value = "Test_Site" });
            ValidateDisplayValuePair(lstExpected, result);
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetSiteNames_Returns_JsonResult_null
        /// <summary>
        /// GetSiteNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetSiteNames_Returns_JsonResult_null()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateSiteList_null());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
          mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
         mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.GetSiteNames();

            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = null, Value = null });
            ValidateDisplayValuePair(lstExpected, result);
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTemplateNames_Returns_JsonResult
        /// <summary>
        /// GetTemplateNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetTemplateNames_Returns_JsonResult()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateSiteList());
            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                     mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                    mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.GetTemplateNames();

            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "Test", Value = "1" });
            ValidateDisplayValuePair(lstExpected, result);

            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetTemplateNames_Returns_JsonResult_null
        /// <summary>
        /// GetTemplateNames_Returns_JsonResult_null
        /// </summary>
        [TestMethod]
        public void GetTemplateNames_Returns_JsonResult_null()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateSiteList_null());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                     mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                    mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.GetTemplateNames();

            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = null, Value = "0" });
            ValidateDisplayValuePair(lstExpected, result);
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetDefaultPageNames_Returns_JsonResult
        /// <summary>
        /// GetDefaultPageNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetDefaultPageNames_Returns_JsonResult()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateSiteList());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                                mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                               mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.GetDefaultPageNames();

            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "Test_Default", Value = "1" });
            ValidateDisplayValuePair(lstExpected, result);

            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region GetDefaultPageNames_Returns_JsonResult_null
        /// <summary>
        /// GetDefaultPageNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void GetDefaultPageNames_Returns_JsonResult_null()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateSiteList_null());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                                mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                               mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.GetDefaultPageNames();

            //Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = null, Value = "0" });
            ValidateDisplayValuePair(lstExpected, result);

            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region DeleteSite_Returns_JsonResult
        /// <summary>
        /// DeleteSite_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void DeleteSite_Returns_JsonResult()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.DeleteEntity(It.IsAny<int>(), It.IsAny<int>()));

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                                mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                               mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.DeleteSite(1);

            // Verify and Assert
            Assert.AreEqual(result.Data, string.Empty);
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAL
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAL()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };

            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");

            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });


            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());


            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });

            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false
            };

            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);

            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_XBRL
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_XBRL()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListXBRL());


            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });

            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false,

            };

            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_XBRL_null
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_XBRL_null()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListXBRLnull());


            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });

            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false
            };

            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);

            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAHD
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAHD()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListTAHD());


            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });

            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false

            };
            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAHD_null
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAL_null()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);

            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });
            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListTAHDnull());


            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });

            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false
            };
            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_TADF
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_TADF()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });


            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListTADF());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();
            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });

            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false
            };
            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_TADF_null
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_TADF_null()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });
            // Mocking Context - ends

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListTADFnull());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });

            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false
            };
            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);

            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAD
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAD()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });
            // Mocking Context - ends

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListTAD());


            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false
            };
            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAD_null
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_TAD_null()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;


            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListTADnull());


            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false

            };
            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);

            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_default
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_default()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;


            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListDEFAULT());


            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false

            };
            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);

            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Post__With_SiteIdZero_true
        /// <summary>
        /// EditSite_Returns_ActionResult_Post__With_SiteIdZero_true
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Post__With_SiteIdZero_ture()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = true;

            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };
            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageList());


            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = true

            };
            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);

            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_With_Post_SiteIdOne
        /// <summary>
        /// EditSite_Returns_ActionResult_With_Post_SiteIdOne
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_With_Post_SiteIdOne()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 1;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;


            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();
            var formValues = new NameValueCollection
            {
               { "hdnClients", "1,2,3" }               
            };

            moqRequest.Setup(r => r.Form).Returns(formValues);
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");
            // Mocking Context - ends
            long sessionValue = 1;

            moqContext.SetupSet(x => x.Session["SITE_EXIST"] = It.IsAny<long>()).Callback((string name, object val) =>
            {
                moqContext.SetupGet(x => x.Session["SITE_EXIST"]).Returns(sessionValue);
            });

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>()));
            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities()).Returns(CreateTemplatePageList());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            //Mocking the context of controller
            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(viewModel);
            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            lstTemplateNames.Add(new DisplayValuePair() { Display = null, Value = "1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstNavKeys.Add(new DisplayValuePair() { Display = null, Value = "1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {
                BaseURL = "http://localhosttest",
                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false,
                SiteID = 1,

            };
            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);
            mockSiteFactoryCache.VerifyAll();
            mockTemplatePageFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_Handles_Exception
        /// <summary>
        /// EditSite_Returns_ActionResult_Handles_Exception
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_Handles_Exception()
        {
            //Arrange
            EditSiteViewModel viewModel = new EditSiteViewModel();
            viewModel.SiteID = 0;
            viewModel.SiteName = "Test_Site";
            viewModel.SelectedTemplateID = 1;
            viewModel.SelectedDefaultPageNameID = 1;
            viewModel.Description = "Test_desc";
            viewModel.DisableDefaultSiteCheckbox = false;
            viewModel.SuccessOrFailedMessage = "Exception of type 'System.Exception' was thrown.";

            mockTemplateTextFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplateTextSearchDetail>()))
                .Returns(CreateTemplateTextList());
            mockTemplateNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<TemplateNavigationSearchDetail>()))
                .Returns(CreateTemplateNavigationList());

            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<TemplatePageSearchDetail>()))
                .Returns(CreateTemplatePageList());

            mockTemplatePageTextFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<TemplatePageTextSearchDetail>()))
                .Returns(CreateTemplatePageTextList());
            mockTemplatePageNavigationFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<TemplatePageNavigationSearchDetail>()))
                .Returns(CreateTemplatePageNavigationList());

            mockSiteFactoryCache.Setup(x => x.SaveEntity(It.IsAny<SiteObjectModel>(), It.IsAny<int>())).Throws(new Exception());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.EditSite(viewModel);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {

                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_desc",
                DisableDefaultSiteCheckbox = false,
                SuccessOrFailedMessage = "Exception of type 'System.Exception' was thrown."

            };


            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);
            mockTemplateTextFactoryCache.VerifyAll();
            mockTemplateNavigationFactoryCache.VerifyAll();
            mockTemplatePageFactoryCache.VerifyAll();
            mockTemplatePageTextFactoryCache.VerifyAll();
            mockTemplatePageNavigationFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region CheckSiteNameAlreadyExists_Returns_JsonResult
        /// <summary>
        /// CheckSiteNameAlreadyExists_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckSiteNameAlreadyExists_Returns_JsonResult()
        {
            //Arrange
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(CreateSiteList());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                                mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                               mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.CheckSiteNameAlreadyExists("Test_Site");

            // Verify and Assert
            Assert.AreEqual(result.Data, true);
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region CheckSiteNameAlreadyExists_Returns_JsonResult_null
        /// <summary>
        /// CheckSiteNameAlreadyExists_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void CheckSiteNameAlreadyExists_Returns_JsonResult_null()
        {
            //Arrange 
            IEnumerable<SiteObjectModel> ienum = null;
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>())).Returns(ienum);

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                                mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                               mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.CheckSiteNameAlreadyExists(string.Empty);

            // Verify and Assert
            Assert.AreEqual(result.Data, false);
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion

        #region LoadDefaultPageNames_Returns_JsonResult
        /// <summary>
        /// LoadDefaultPageNames_Returns_JsonResult
        /// </summary>
        [TestMethod]
        public void LoadDefaultPageNames_Returns_JsonResult()
        {
            //Arrange
            mockTemplatePageFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<TemplatePageSearchDetail>())).Returns(CreateTemplatePageListXBRL());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                                            mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                                           mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);
            var result = objSiteController.LoadDefaultPageNames(1);

            // Verify and Assert
            List<DisplayValuePair> lstExpected = new List<DisplayValuePair>();
            lstExpected.Add(new DisplayValuePair() { Display = "--Please select Default Page--", Value = "-1" });
            lstExpected.Add(new DisplayValuePair() { Display = null, Value = "1" });
            ValidateDisplayValuePair(lstExpected, result);
            mockTemplatePageFactoryCache.Verify();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        #endregion
      

        #region EditSite_Returns_ActionResult_GreaterThanZero
        /// <summary>
        /// EditSite_Returns_ActionResult_GreaterThanZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_GreaterThanZero()
        {
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();

            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));

            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");

            TemplatePageObjectModel ObjTemplatePageObjectModel = new TemplatePageObjectModel();
            ObjTemplatePageObjectModel.PageID = 1;
            ObjTemplatePageObjectModel.PageName = "Test_1";
            ObjTemplatePageObjectModel.PageDescription = "SiteTest1";
            ObjTemplatePageObjectModel.TemplateID = 1;
            ObjTemplatePageObjectModel.TemplateName = "Temp1";

            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateTemplatePageList());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(1);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            lstTemplateNames.Add(new DisplayValuePair() { Display =null, Value = "1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            lstNavKeys.Add(new DisplayValuePair() { Display = null, Value = "1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {

                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_Description",
                DisableDefaultSiteCheckbox = false,
                BaseURL="http://localhosttest"

            };


            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);
            mockTemplatePageFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_GreaterThanZero_
        /// <summary>
        /// EditSite_Returns_ActionResult_GreaterThanZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_GreaterThanZero_()
        {
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();

            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));

            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");

            TemplatePageObjectModel ObjTemplatePageObjectModel = new TemplatePageObjectModel();
            ObjTemplatePageObjectModel.PageID = 1;
            ObjTemplatePageObjectModel.PageName = "Test_1";
            ObjTemplatePageObjectModel.PageDescription = "SiteTest1";
            ObjTemplatePageObjectModel.TemplateID = 2;
            ObjTemplatePageObjectModel.TemplateName = "Temp1";

            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateTemplatePageList2());
            mockSiteFactoryCache.Setup(x => x.GetEntitiesBySearch( It.IsAny<SiteSearchDetail>()))
                .Returns(CreateSiteList());

            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);

            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(1);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            lstTemplateNames.Add(new DisplayValuePair() { Display = null, Value = "2" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Page--", Value = "-1" });
            //lstNavKeys.Add(new DisplayValuePair() { Display = null, Value = "1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {

                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                SiteName = "Test_Site",
                SelectedTemplateID = 1,
                SelectedDefaultPageNameID = 1,
                Description = "Test_Description",
                DisableDefaultSiteCheckbox = false,
                BaseURL = "http://localhosttest"

            };


            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);

            mockTemplatePageFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_LessThanZero
        /// <summary>
        /// EditSite_Returns_ActionResult_LessThanZero
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_LessThanZero()
        {
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();


            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));

            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");

            TemplatePageObjectModel ObjTemplatePageObjectModel = new TemplatePageObjectModel();

            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateTemplatePageList());
            mockSiteFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateSiteList());
            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);


            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(0);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            lstTemplateNames.Add(new DisplayValuePair() { Display = null, Value = "1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Template First--", Value = "-1" });
            //lstNavKeys.Add(new DisplayValuePair() { Display = null, Value = "1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {

                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
               
                DisableDefaultSiteCheckbox = false,
                BaseURL = "http://localhosttest"

            };


            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);

            mockTemplatePageFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion

        #region EditSite_Returns_ActionResult_LessThanZeronull
        /// <summary>
        /// EditSite_Returns_ActionResult_LessThanZeronull
        /// </summary>
        [TestMethod]
        public void EditSite_Returns_ActionResult_LessThanZeronull()
        {
            // Mocking Context - starts
            var moqContext = new Mock<HttpContextBase>();
            var moqRequest = new Mock<HttpRequestBase>();

            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
            moqRequest.Setup(x => x.Url).Returns(new Uri("http://localhost"));

            var moqUrlHlpr = new Mock<UrlHelper>();
            moqUrlHlpr.Setup(x => x.Content(It.IsAny<string>())).Returns("test");

            TemplatePageObjectModel ObjTemplatePageObjectModel = new TemplatePageObjectModel();
            ObjTemplatePageObjectModel.PageID = 0;
            ObjTemplatePageObjectModel.PageName = "Test_1";
            ObjTemplatePageObjectModel.PageDescription = "SiteTest1";
            ObjTemplatePageObjectModel.TemplateID = 3;
            ObjTemplatePageObjectModel.TemplateName = "Temp1";

            mockTemplatePageFactoryCache.Setup(x => x.GetAllEntities())
                .Returns(CreateTemplatePageList());

            mockSiteFactoryCache.Setup(x => x.GetAllEntities())
               .Returns(Enumerable.Empty<SiteObjectModel>());
            //Act
            SiteController objSiteController = new SiteController(mockSiteFactoryCache.Object, mockTemplatePageFactoryCache.Object,
                               mockUserFactoryCache.Object, mockTemplateTextFactoryCache.Object, mockTemplatePageTextFactoryCache.Object,
                              mockTemplateNavigationFactoryCache.Object, mockTemplatePageNavigationFactoryCache.Object);


            objSiteController.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), objSiteController);
            objSiteController.Url = moqUrlHlpr.Object;
            var result = objSiteController.EditSite(0);

            // Verify and Assert
            List<DisplayValuePair> lstTemplateNames = new List<DisplayValuePair>();

            lstTemplateNames.Add(new DisplayValuePair() { Display = "--Please select Template Name--", Value = "-1" });
            lstTemplateNames.Add(new DisplayValuePair() { Display = null, Value = "1" });
            List<DisplayValuePair> lstNavKeys = new List<DisplayValuePair>();
            lstNavKeys.Add(new DisplayValuePair() { Display = "--Please select Template First--", Value = "-1" });
            //lstNavKeys.Add(new DisplayValuePair() { Display = null, Value = "1" });


            EditSiteViewModel viewModelOutput = new EditSiteViewModel()
            {

                PageDescriptions = lstNavKeys,
                TemplateNames = lstTemplateNames,
                IsDefaultSite=true,
                DisableDefaultSiteCheckbox = true,
                BaseURL = "http://localhosttest"

            };


            var result1 = result as ViewResult;
            var viewModelInput = result1.Model as EditSiteViewModel;
            ValidateViewModelData<EditSiteViewModel>(viewModelInput, viewModelOutput);
            Assert.AreEqual("", result1.ViewName);


            mockTemplatePageFactoryCache.VerifyAll();
            mockSiteFactoryCache.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        #endregion
    }
}
