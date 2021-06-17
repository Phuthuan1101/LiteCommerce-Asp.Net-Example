using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai báo các phép xử lý liên quan đến tài khoản của user
    /// </summary>
    public interface IAccountDAL
    {
        /// <summary>
        /// Kiểm tra thông tin đăng nhập của user (Hàm trả về null nếu thông tin đăng nhập không hợp lệ)
        /// </summary>
        /// <param name="loginName">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns></returns>
        Account Authorize(string loginName, string password);

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string accountId, string oldPassword, string newPassword);

        /// <summary>
        /// Lấy thông tin của 1 tài khoản theo Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Account Get(string accountId);
    }
}
