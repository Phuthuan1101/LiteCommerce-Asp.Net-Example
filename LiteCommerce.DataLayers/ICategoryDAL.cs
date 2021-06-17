using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICategoryDAL
    {
        /// <summary>
        /// Bổ sung một danh mục. Hàm trả về mã của danh mục nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của danh mục cần bổ sung</param>
        /// <returns></returns>
        int Add(Category data);

        /// <summary>
        /// Lấy danh sách các danh mục (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">Số dòng hiện thị trên 1 trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CategoryName, CategoryID(chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        List<Category> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Đếm số lượng danh mục thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CategoryName, CategoryID(chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);


        /// <summary>
        /// Lấy thông tin của một danh mục theo ID
        /// Trong trường hợp danh mục không tồn tại, hàm trả về giá trị null
        /// </summary>
        /// <param name="categoryID">ID của nhà cung cấp cần lấy thông tin</param>
        /// <returns></returns>
        Category Get(int categoryID);

        /// <summary>
        /// Cập nhật thông tin của một danh mục
        /// Hàm trả về giá trị bool cho biết việc cập nhật có thành công hay không?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);

        /// <summary>
        /// Xóa một danh mục. Hàm trả về giá trị bool cho biết việc xóa có thành công hay không?
        /// (Lưu ý: Không được xóa danh mục nếu đang có đơn hàng tham chiếu đến danh mục)
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        bool Delete(int categoryID);
        /// <summary>
        /// lấy toàn bộ ds
        /// </summary>
        /// <returns></returns>
        List<Category> List();
    }
}
