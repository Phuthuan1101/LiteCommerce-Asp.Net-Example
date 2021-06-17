using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public abstract class BasePaginationQueryResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SearchValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageCount
        {
            get
            {
                int page = RowCount / PageSize;
                if (RowCount % PageSize != 0)
                    page += 1;
                return page;
            }
        }
    }
}