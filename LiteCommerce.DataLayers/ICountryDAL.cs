using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai báo các phép xử lý dữ liệu liên quan dến quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy danh sách tất cả các quốc gia
        /// </summary>
        /// <returns></returns>
        List<Country> List();
    }
}
