using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{

    /// <summary>
    /// mặt hàng với các thông tin bôe sung (mở rộng)
    /// </summary>
    public class ProductEx : Product
    {
        /// <summary>
        /// danh sách các thuộc tính của mặt hàng
        /// </summary>
        public List<ProductAttribute> Attributes { get; set; }


        /// <summary>
        /// danh sách các hình  ảnh của mặt hàng
        /// </summary>
        public List<ProductGallery> Galleries { get; set; }
    }
}
