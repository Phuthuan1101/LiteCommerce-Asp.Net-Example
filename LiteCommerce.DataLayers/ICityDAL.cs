using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến thành phố
    /// </summary>
    public interface ICityDAL
    {
        /// <summary>
        /// Lấy danh sách tất cả thành phố
        /// </summary>
        /// <returns></returns>
        List<City> List();

        /// <summary>
        /// Lấy danh sách các thành phố thuộc một quốc gia
        /// </summary>
        /// <param name="countryName">Tên của quốc gia cần lấy thành phố</param>
        /// <returns></returns>
        List<City> List(string countryName);
    }
}
