// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.HostedAdmin
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************


using System;
using System.Web;

/// <summary>
/// The Common namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.HostedAdmin.Common
{
    /// <summary>
    /// Class for handling Kendo Grid Post
    /// </summary>
      
    public class KendoGridPost
    {

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
            public int Page { get; set; }
            /// <summary>
            /// Gets or sets the size of the page.
            /// </summary>
            /// <value>The size of the page.</value>
            public int PageSize { get; set; }
            /// <summary>
            /// Gets or sets the skip.
            /// </summary>
            /// <value>The skip.</value>
            public int Skip { get; set; }
            /// <summary>
            /// Gets or sets the take.
            /// </summary>
            /// <value>The take.</value>
            public int Take { get; set; }
            /// <summary>
            /// Gets or sets the sort order.
            /// </summary>
            /// <value>The sort order.</value>
            public string SortOrder { get; set; }
            /// <summary>
            /// Gets or sets the sort column.
            /// </summary>
            /// <value>The sort column.</value>
            public string SortColumn { get; set; }
            /// <summary>
            /// Gets or sets the sort column mutiple.
            /// </summary>
            /// <value>The sort column mutiple.</value>
            public string SortColumnMutiple { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="KendoGridPost"/> class.
            /// </summary>
            public KendoGridPost()
            {
                if (HttpContext.Current != null)
                {
                    

                    this.Page = Convert.ToInt32(HttpContext.Current.Request["page"]);
                    this.PageSize = Convert.ToInt32(HttpContext.Current.Request["pageSize"]);
                    this.Skip = Convert.ToInt32(HttpContext.Current.Request["skip"]);
                    this.Take = Convert.ToInt32(HttpContext.Current.Request["take"]);
                    this.SortColumn = Convert.ToString(HttpContext.Current.Request["sort[0][field]"]);
                    this.SortOrder = Convert.ToString(HttpContext.Current.Request["sort[0][dir]"]);


                }
            }
        
    }
}