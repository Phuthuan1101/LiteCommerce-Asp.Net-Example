using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class ListSupplierAndCategory: BasePaginationQueryResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Category> listCategoris;
        /// <summary>
        /// 
        /// </summary>
        public List<Supplier> listSuppliers;
    }
}