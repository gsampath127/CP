
using System;
using System.Web;

namespace BCSDocUpdateApprovalV2.Formats
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
            public KendoGridPost(dynamic data)
            {
                if (HttpContext.Current != null)
                {


                    this.Page = Convert.ToInt32(data.options.page);
                    this.PageSize = Convert.ToInt32(data.options.pageSize);
                    this.Skip = Convert.ToInt32(data.options.skip);
                    this.Take = Convert.ToInt32(data.options.take);
                    try
                    {
                        this.SortColumn = Convert.ToString(data.options.sort[0].field);
                        this.SortOrder = Convert.ToString(data.options.sort[0].dir);
                    }
                    catch {}
                    


                }
            }
        
    }
}