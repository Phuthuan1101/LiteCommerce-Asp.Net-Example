using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{

    /// <summary>
    /// định nghĩa các phép xử  lý dữ liệu liên quan đến hàng hóa
    /// </summary>
    public interface IProductDAL
    {
        /// <summary>
        /// Láy danh sách các mặt hàng ( phân trang, tìm kiếm , lọc dữ liệu)
        /// </summary>
        /// <param name="page">trang</param>
        /// <param name="pageSize">kich thước trang</param>
        /// <param name="categoryId">Mã loại hàng (0 nếu không  lọc theo loại hàng)</param>
        /// <param name="supplierId">mã nhà cung cấp (0 nếu như khôngg lọc mặt hàng theo nhà cung cấp)</param>
        /// <param name="searchValue">tên mặt hàng cần tìm kiếm ( chuỗi rỗng nếy không tìm kiếm</param>
        /// <returns></returns>
        List<Product> List(int page, int pageSize, int categoryId, int supplierId, string searchValue);



        /// <summary>
        /// đếm số lượng hàng (hỗ trợ cho phân trang)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(int categoryId, int supplierId, string searchValue);

        /// <summary>
        /// Lấy thông tin mặt hàng theo mã hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product Get(int productId);


        /// <summary>
        /// Lấy thông tin mặt hàng và các thông tin liên quan theo mã hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductEx GetEx(int productId);


        /// <summary>
        /// bổ sung 1 mặt hàng mới ( hàm trả về mã hàng được bổ sung nếu thanhf công, trả về 0 nếu khôngg bổ sung dc )
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);


        /// <summary>
        /// cật nhật thông tin mătj hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);

        /// <summary>
        /// xóa 1 mặtt hàng(khi xóa 1 mặt hàng thì đồng thời củng xóa các thuộc tính và thư viện ảnh của của mặt hàng đó)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool Delete(int productId);



        /// <summary>
        /// Lấy danh sách các thuộc tính của 1 product (sắp xêp theo DisplayOrder)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductAttribute> ListAttributes(int productId);


        /// <summary>
        /// Lấy thông tin chi tiết của 1 thuộc tính
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        ProductAttribute GetAttribute(long attributeId);



        /// <summary>
        /// Bổ sung thuộc tính cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddAttribute(ProductAttribute data);




        /// <summary>
        /// cập nhật thuộc tính
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAttribute(ProductAttribute data);



        /// <summary>
        /// xóa thuộc tính
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        bool DeleteAttribute(long attributeId);

        /// <summary>
        /// lấy danh sách hình ảnh của một mặt hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductGallery> ListGalleries(int productId);



        /// <summary>
        /// lấy thông tin của một ảnh
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        ProductGallery GetGallery(long galleryId);



        /// <summary>
        /// bổ sungg ảnh cho mặtt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddGallery(ProductGallery data);



        /// <summary>
        /// cập nhật thông tin của 1 ảnh
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateGallery(ProductGallery data);


        /// <summary>
        /// xóa 1 ảnh
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        bool DeleteGallery(long galleryId);
        /// <summary>
        /// Lấy giá trị của mặt hàng vừa mới tạo
        /// </summary>
        /// <returns></returns>
        long GetMaxId();
    }
}
