using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến khách hàng
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Bổ sung một khách hàng mới. Hàm trả về mã của khách hàng nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của khách hàng cần bổ sung</param>
        /// <returns></returns>
        int Add(Customer data);

        /// <summary>
        /// Lấy danh sách các nhà cung cấp (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">Số dòng hiện thị trên 1 trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CustomerName, ContactName, Address(chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        List<Customer> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Đếm số lượng khách hàng thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CustomerName, ContactName, Address(chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);


        /// <summary>
        /// Lấy thông tin của một khách hàng theo ID
        /// Trong trường hợp khách hàng không tồn tại, hàm trả về giá trị null
        /// </summary>
        /// <param name="customerID">ID của nhà cung cấp cần lấy thông tin</param>
        /// <returns></returns>
        Customer Get(int customerID);

        /// <summary>
        /// Cập nhật thông tin của một khách hàng
        /// Hàm trả về giá trị bool cho biết việc cập nhật có thành công hay không?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);

        /// <summary>
        /// Xóa một khách hàng. Hàm trả về giá trị bool cho biết việc xóa có thành công hay không?
        /// (Lưu ý: Không được xóa khách hàng nếu đang có đơn hàng tham chiếu đến khách hàng)
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        bool Delete(int customerID);
    }
}
