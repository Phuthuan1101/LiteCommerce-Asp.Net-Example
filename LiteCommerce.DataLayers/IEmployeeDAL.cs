using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến nhân viên
    /// </summary>
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns></returns>
        List<Employee> List();

        /// <summary>
        /// Bổ sung một nhân viên mới. Hàm trả về mã của nhân viên nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của nhân viên cần bổ sung</param>
        /// <returns></returns>
        int Add(Employee data);

        /// <summary>
        /// Lấy danh sách các nhân viên (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">Số dòng hiện thị trên 1 trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo EmployeeName, ContactName, Address, Phone(chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Đếm số lượng nhân viên thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo EmployeeName, ContactName, Address, Phone(chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lấy thông tin của một nhân viên theo ID
        /// Trong trường hợp nhân viên không tồn tại, hàm trả về giá trị null
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần lấy thông tin</param>
        /// <returns></returns>
        Employee Get(int employeeID);

        /// <summary>
        /// Cập nhật thông tin của một nhân viên
        /// Hàm trả về giá trị bool cho biết việc cập nhật có thành công hay không?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);

        /// <summary>
        /// Xóa một nhân viên. Hàm trả về giá trị bool cho biết việc xóa có thành công hay không?
        /// (Lưu ý: Không được xóa nhân viên nếu đang có mặt hàng tham chiếu đến nhân viên)
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        bool Delete(int employeeID);
    }
}
