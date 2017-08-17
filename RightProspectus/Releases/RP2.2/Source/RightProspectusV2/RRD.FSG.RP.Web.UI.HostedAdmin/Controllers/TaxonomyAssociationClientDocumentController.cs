using RP.Extensions;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Interfaces.VerticalMarket;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Utilities;
using RRD.FSG.RP.Web.UI.HostedAdmin.Common;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.Controllers
{
    public class TaxonomyAssociationClientDocumentController : BaseController
    {
        #region Constants
        ///<summary>
        ///Constants
        ///</summary>
        private const string dropvalue = "-1";
        private const string dropTaxonomy = "--Please select Taxonomy--";
        private const string dropClientDocType = "--Please select Client Document Type--";
        private const string dropClientDocument = "--Please select Client Document--";

        #endregion

        #region Variables
        /// <summary>
        /// The taxonomy level external identifier cache factory
        /// </summary>
        private IFactoryCache<TaxonomyAssociationClientDocumentFactory, TaxonomyAssociationClientDocumentObjectModel, TaxonomyAssociationClientDocumentKey> taxonomyAssociationClientDocumentCacheFactory;
        /// <summary>
        /// The user cache factory
        /// </summary>
        private IFactoryCache<UserFactory, UserObjectModel, int> userCacheFactory;

        /// <summary>
        /// The document type cache factory
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public IFactory<TaxonomyAssociationObjectModel, int> taxonomyAssociationFactory;

        /// <summary>
        /// The taxonomy cache factory
        /// </summary>
        private IFactoryCache<TaxonomyFactory, TaxonomyObjectModel, TaxonomyKey> taxonomyCacheFactory;

        /// <summary>
        /// The client document type cache factory
        /// </summary>
        private IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int> clientDocumentTypeCacheFactory;

        /// <summary>
        /// The client document cache factory
        /// </summary>
        private IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int> clientDocumentCacheFactory;


        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyLevelExternalIdController"/> class.
        /// </summary>
        public TaxonomyAssociationClientDocumentController()
        {
            //Added this check for Session Timed Out.  RPActionFilterAttribute - OnActionExecuting(ActionExecutingContext filterContext) method will be called after constructor.            
            if (!string.IsNullOrWhiteSpace(SessionClientName))
            {

                taxonomyAssociationClientDocumentCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TaxonomyAssociationClientDocumentFactory, TaxonomyAssociationClientDocumentObjectModel, TaxonomyAssociationClientDocumentKey>>("TaxonomyAssociationClientDocument");
                taxonomyAssociationClientDocumentCacheFactory.ClientName = SessionClientName;
                taxonomyAssociationClientDocumentCacheFactory.Mode = FactoryCacheMode.All;


                userCacheFactory = RPV2Resolver.Resolve<IFactoryCache<UserFactory, UserObjectModel, int>>("Users");
                userCacheFactory.Mode = FactoryCacheMode.All;

                taxonomyAssociationFactory = RPV2Resolver.Resolve<IFactory<TaxonomyAssociationObjectModel, int>>("TaxonomyAssociationFactory");
                taxonomyAssociationFactory.ClientName = SessionClientName;                

                taxonomyCacheFactory = RPV2Resolver.Resolve<IFactoryCache<TaxonomyFactory, TaxonomyObjectModel, TaxonomyKey>>("Taxonomy");
                taxonomyCacheFactory.ClientName = SessionClientName;
                taxonomyCacheFactory.Mode = FactoryCacheMode.All;

                clientDocumentTypeCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int>>("ClientDocumentType");
                clientDocumentTypeCacheFactory.ClientName = SessionClientName;
                clientDocumentTypeCacheFactory.Mode = FactoryCacheMode.All;

                clientDocumentCacheFactory = RPV2Resolver.Resolve<IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int>>("ClientDocument");
                clientDocumentCacheFactory.ClientName = SessionClientName;
                clientDocumentCacheFactory.Mode = FactoryCacheMode.All;                
            }
        }
        #endregion

        #region Constructor_Test
        /// <summary>
        /// Constructor for the Test Methods
        /// </summary>
        /// <param name="TaxonomyLevelExternalIdFactoryCache">The taxonomy level external identifier factory cache.</param>
        /// <param name="UserFactoryCache">The user factory cache.</param>
        /// <param name="TaxonomyLevelFactoryCache">The taxonomy level factory cache.</param>
        public TaxonomyAssociationClientDocumentController(IFactoryCache<TaxonomyAssociationClientDocumentFactory, TaxonomyAssociationClientDocumentObjectModel, TaxonomyAssociationClientDocumentKey> TaxonomyAssociationClientDocumentFactoryCache,
            IFactoryCache<UserFactory, UserObjectModel, int> UserFactoryCache, IFactoryCache<TaxonomyFactory, TaxonomyObjectModel, TaxonomyKey> TaxonomyFactoryCache,
            IFactoryCache<ClientDocumentTypeFactory, ClientDocumentTypeObjectModel, int> ClientDocumentTypeFactoryCache ,
            IFactoryCache<ClientDocumentFactory, ClientDocumentObjectModel, int> ClientDocumentFactoryCache
            )
        {

            taxonomyAssociationClientDocumentCacheFactory = TaxonomyAssociationClientDocumentFactoryCache;
            taxonomyAssociationClientDocumentCacheFactory.ClientName = SessionClientName;
            taxonomyAssociationClientDocumentCacheFactory.Mode = FactoryCacheMode.All;

            userCacheFactory = UserFactoryCache;
            userCacheFactory.Mode = FactoryCacheMode.All;

            taxonomyCacheFactory = TaxonomyFactoryCache;
            taxonomyCacheFactory.ClientName = SessionClientName;
            taxonomyCacheFactory.Mode = FactoryCacheMode.All;

            clientDocumentTypeCacheFactory = ClientDocumentTypeFactoryCache;
            clientDocumentTypeCacheFactory.ClientName = SessionClientName;
            clientDocumentTypeCacheFactory.Mode = FactoryCacheMode.All;

            clientDocumentCacheFactory = ClientDocumentFactoryCache;
            clientDocumentCacheFactory.ClientName = SessionClientName;
            clientDocumentCacheFactory.Mode = FactoryCacheMode.All;


        }

        #endregion
        #region View
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        // GET: TaxonomyAssociationClientDocument
        public ActionResult List()
        {
            ViewData["SelectedCustomer"] = SessionClientName;
            return View();
        }
        #endregion



        #region Load Search Creteria Methods.


        /// <summary>
        /// Gets the taxonomy.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetTaxonomy()
        {
            return Json((from dbo in taxonomyAssociationClientDocumentCacheFactory.GetAllEntities()
                         select new { Display = dbo.TaxonomyAssociationName, Value = dbo.TaxonomyId.ToString() }).Distinct(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetClientDocumentTypeName()
        {
            return Json((from dbo in taxonomyAssociationClientDocumentCacheFactory.GetAllEntities()
                         select new { Display = dbo.ClientDocumentTypeName, Value = dbo.ClientDocumentTypeId.ToString() }).Distinct(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult GetClientDocumentName()
        {
            return Json((from dbo in taxonomyAssociationClientDocumentCacheFactory.GetAllEntities()
                         select new { Display = dbo.ClientDocumentName, Value = dbo.ClientDocumentId.ToString() }).Distinct(), JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region GetAllTaxonomyAssociationClientDocumentDetails
        /// <summary>
        /// Gets all taxonomy level external identifier details.
        /// </summary>
        /// <param name="taxonomyId">The level identifier.</param>
        /// <param name="taxonomyID">The taxonomy identifier.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult GetAllTaxonomyAssociationClientDocumentDetails(string taxonomyId, string clientDocumentTypeId, string clientDocumentId)
        {
            int id;
            TaxonomyAssociationClientDocumentSearchDetail objSearchDetail = new TaxonomyAssociationClientDocumentSearchDetail()
            {
                TaxonomyId = !string.IsNullOrEmpty(taxonomyId) ? (int.TryParse(taxonomyId, out id) ? id : 0) : (int?)null,
                ClientDocumentTypeId = !string.IsNullOrEmpty(clientDocumentTypeId) ? (int.TryParse(clientDocumentTypeId, out id) ? id : 0) : (int?)null,
                ClientDocumentId = !string.IsNullOrEmpty(clientDocumentId) ? (int.TryParse(clientDocumentId, out id) ? id : 0) : (int?)null,
                //TaxonomyAssociationName = string.IsNullOrEmpty(taxonomyAssociationName) ? null : taxonomyAssociationName,
                //ClientDocumentTypeName = string.IsNullOrEmpty(clientDocumentTypeName) ? null : clientDocumentTypeName,
                //ClientDocumentName = string.IsNullOrEmpty(clientDocumentName) ? null : clientDocumentName,
                //ClientDocumentFileName = string.IsNullOrEmpty(clientDocumentFileName) ? null : clientDocumentFileName
            };
            KendoGridPost kendoGridPost = new KendoGridPost();
            int startRowIndex = (kendoGridPost.Page - 1) * kendoGridPost.PageSize;

            TaxonomyAssociationClientDocumentSortColumn SortColumn = TaxonomyAssociationClientDocumentSortColumn.TaxonomyAssociationName;
            switch (kendoGridPost.SortColumn)
            {
                case "TaxonomyAssociationName":
                    SortColumn = TaxonomyAssociationClientDocumentSortColumn.TaxonomyAssociationName;
                    break;
                case "ClientDocumentTypeName":
                    SortColumn = TaxonomyAssociationClientDocumentSortColumn.ClientDocumentTypeName;
                    break;
                case "ClientDocumentName":
                    SortColumn = TaxonomyAssociationClientDocumentSortColumn.ClientDocumentName;
                    break;
                case "ClientDocumentFileName":
                    SortColumn = TaxonomyAssociationClientDocumentSortColumn.ClientDocumentFileName;
                    break;
            }

            SortOrder sortOrder = SortOrder.Ascending;
            if (!string.IsNullOrWhiteSpace(kendoGridPost.SortOrder) && kendoGridPost.SortOrder.Equals("desc"))
            {
                sortOrder = SortOrder.Descending;
            }

            return Json
                (new
                {
                    total = taxonomyAssociationClientDocumentCacheFactory.GetEntitiesBySearch(objSearchDetail).Select(p => p.TaxonomyId).Count(),
                    data = (from dbo in taxonomyAssociationClientDocumentCacheFactory.GetEntitiesBySearch(
                                startRowIndex,
                                kendoGridPost.PageSize,
                                objSearchDetail,
                                new TaxonomyAssociationClientDocumentSortDetail() { Column = SortColumn, Order = sortOrder })
                            select new
                            {
                                TaxonomyId = dbo.TaxonomyId,
                                TaxonomyAssociationName = dbo.TaxonomyAssociationName,
                                ClientDocumentId = dbo.ClientDocumentId,
                                ClientDocumentName = dbo.ClientDocumentName,
                                ClientDocumentFileName = dbo.ClientDocumentFileName,
                                ClientDocumentTypeId = dbo.ClientDocumentTypeId,
                                ClientDocumentTypeName = dbo.ClientDocumentTypeName

                            })
                });
        }
        #endregion

        #region Edit
        [HttpGet]
        [IsPopUp]
        public ActionResult EditTaxonomyAssociationClientDocument()
        {
            EditTaxonomyAssociationClientDocumentViewModel EditViewModel = new EditTaxonomyAssociationClientDocumentViewModel();

            EditViewModel.TaxonomyAssociation = new List<DisplayValuePair>();
            EditViewModel.TaxonomyAssociation.Add(new DisplayValuePair { Value = dropvalue, Display = dropTaxonomy });            

            var taxonomyDetails = taxonomyCacheFactory.GetAllEntities();
            if (taxonomyDetails != null)
            {
                foreach (var taxonomyObjectModel in taxonomyDetails.OrderBy(x => x.TaxonomyName))
                {
                    EditViewModel.TaxonomyAssociation.Add(new DisplayValuePair { Display = taxonomyObjectModel.TaxonomyName, Value = taxonomyObjectModel.TaxonomyId.ToString() });
                }
            }


            EditViewModel.ClientDocumentType = new List<DisplayValuePair>();
            EditViewModel.ClientDocumentType.Add(new DisplayValuePair { Value = dropvalue, Display = dropClientDocType });
            foreach (ClientDocumentTypeObjectModel clientDocTypeObjectModel in clientDocumentTypeCacheFactory.GetAllEntities())
            {
                EditViewModel.ClientDocumentType.Add(new DisplayValuePair { Display = clientDocTypeObjectModel.Name, Value = clientDocTypeObjectModel.ClientDocumentTypeId.ToString() });
            }

            EditViewModel.ClientDocument = new List<DisplayValuePair>();
            EditViewModel.ClientDocument.Add(new DisplayValuePair { Value = dropvalue, Display = dropClientDocument });            

            EditViewModel.SelectedTaxonomyId = -1;
            EditViewModel.SelectedClientDocumentTypeId = -1;
            EditViewModel.SelectedClientDocumentId = -1;

            ViewData["Success"] = "In Progress";
            return View(EditViewModel);

        }


        [HttpPost]
        [IsPopUp]
        public ActionResult EditTaxonomyAssociationClientDocument(EditTaxonomyAssociationClientDocumentViewModel EditViewModel)
        {
            try
            {
                TaxonomyAssociationClientDocumentObjectModel objTaxonomyAssociationClientDocumentObjectModel = new TaxonomyAssociationClientDocumentObjectModel()
                {
                    TaxonomyId = EditViewModel.SelectedTaxonomyId,
                    ClientDocumentTypeId = EditViewModel.SelectedClientDocumentTypeId,
                    ClientDocumentId = EditViewModel.SelectedClientDocumentId
                };
                taxonomyAssociationClientDocumentCacheFactory.SaveEntity(objTaxonomyAssociationClientDocumentObjectModel, SessionUserID);

                //
                EditViewModel.TaxonomyAssociation = new List<DisplayValuePair>();
                EditViewModel.TaxonomyAssociation.Add(new DisplayValuePair { Value = dropvalue, Display = dropTaxonomy });

                var taxonomyDetails = taxonomyCacheFactory.GetAllEntities();
                if (taxonomyDetails != null)
                {
                    foreach (var taxonomyObjectModel in taxonomyDetails.OrderBy(x => x.TaxonomyName))
                    {
                        EditViewModel.TaxonomyAssociation.Add(new DisplayValuePair { Display = taxonomyObjectModel.TaxonomyName, Value = taxonomyObjectModel.TaxonomyId.ToString() });
                    }
                }

                EditViewModel.ClientDocumentType = new List<DisplayValuePair>();
                EditViewModel.ClientDocumentType.Add(new DisplayValuePair { Value = dropvalue, Display = dropClientDocType });
                foreach (ClientDocumentTypeObjectModel clientDocTypeObjectModel in clientDocumentTypeCacheFactory.GetAllEntities())
                {
                    EditViewModel.ClientDocumentType.Add(new DisplayValuePair { Display = clientDocTypeObjectModel.Name, Value = clientDocTypeObjectModel.ClientDocumentTypeId.ToString() });
                }

                EditViewModel.ClientDocument = new List<DisplayValuePair>();
                EditViewModel.ClientDocument.Add(new DisplayValuePair { Value = dropvalue, Display = dropClientDocument });
                foreach (ClientDocumentObjectModel clientDocObjectModel in clientDocumentCacheFactory.GetAllEntities())
                {
                    EditViewModel.ClientDocument.Add(new DisplayValuePair { Display = clientDocObjectModel.Name, Value = clientDocObjectModel.ClientDocumentId.ToString() });
                }
                EditViewModel.SelectedTaxonomyId = -1;
                EditViewModel.SelectedClientDocumentTypeId = -1;
                EditViewModel.SelectedClientDocumentId = -1;

                ViewData["Success"] = "OK";
            }
            catch (Exception e)
            {
                EditViewModel.SuccessOrFailedMessage = e.Message;
            }
            return View(EditViewModel);
        }
        #endregion

        public JsonResult GetFileName(int ClientDocId)
        {
            string FileName = string.Empty;
            if (ClientDocId != -1)
            {
                FileName = clientDocumentCacheFactory.GetAllEntities().Where(x => x.ClientDocumentId == ClientDocId).FirstOrDefault().FileName;
            }
            return Json(FileName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClientDocument(int ClientDocTypeId, int TaxonomyId)
        {
            List<DisplayValuePair> lstClientDocument = new List<DisplayValuePair>();
            lstClientDocument.Add(new DisplayValuePair { Value = dropvalue, Display = dropClientDocument });
            if (ClientDocTypeId != -1)
            {
                List<int> lstClintDocIds = taxonomyAssociationClientDocumentCacheFactory.GetEntitiesBySearch(new TaxonomyAssociationClientDocumentSearchDetail { TaxonomyId = TaxonomyId }).Select(x => x.ClientDocumentId).ToList();

                List<ClientDocumentObjectModel> filteredClientDocument = clientDocumentCacheFactory.GetEntitiesBySearch(new ClientDocumentSearchDetail { ClientDocumentTypeId = ClientDocTypeId }).Where(i => !lstClintDocIds.Contains(i.ClientDocumentId)).ToList();

                foreach (ClientDocumentObjectModel clientDocObjectModel in filteredClientDocument)
                {
                    lstClientDocument.Add(new DisplayValuePair { Display = clientDocObjectModel.Name, Value = clientDocObjectModel.ClientDocumentId.ToString() });
                }
            }
            return Json(lstClientDocument, JsonRequestBehavior.AllowGet);
        }


        #region DisableTaxonomyAssociationClientDocument
        /// <summary>
        /// Disables the document type external identifier.
        /// </summary>
        /// <param name="TaxonomyAssociationId">Type of the document.</param>
        /// <param name="ClientDocumentId">The external identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpGet]
        public JsonResult DisableTaxonomyAssociationClientDocument(int TaxonomyId, int ClientDocumentId)
        {
            //TODO: Idealy should pass the DocumentTypeExternalIdKey object.
            taxonomyAssociationClientDocumentCacheFactory.DeleteEntity(
                new TaxonomyAssociationClientDocumentObjectModel
                {
                    TaxonomyId = TaxonomyId,
                    ClientDocumentId = ClientDocumentId
                },
                SessionUserID);

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
