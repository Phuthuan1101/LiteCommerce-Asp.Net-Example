using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;

namespace LiteCommerce.BusinessLayers
{
    public static class HRService
    {
        private static IEmployeeDAL EmployeeDB;
        /// <summary>
        /// Khởi tạo tầng nghiệp vụ
        /// (Hàm này phải được gọi trước khi sử dụng các chức năng khác trong lớp)
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    EmployeeDB = new LiteCommerce.DataLayers.SQLServer.EmployeeDAL(connectionString);
                    break;
                case DatabaseTypes.FakeDB:

                    break;
                default:
                    throw new Exception("Database type is not supported");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Employee> Employees_List()
        {
            return EmployeeDB.List();
        }

    }
}
