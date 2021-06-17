using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;

namespace LiteCommerce.Admin.Controllers
{
    public class TestController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;

            //ICountryDAL dal = new DataLayers.SQLServer.CountryDAL(connectionString);
            //var data = dal.List();

            //ICityDAL dal = new DataLayers.SQLServer.CityDAL(connectionString);
            //var data = dal.List("USA");

            Employee employee = new Employee()
            {
                EmployeeID = 11,
                LastName = "Nhật",
                FirstName = "Khanh",
                BirthDate = DateTime.Today,
                Notes = "Hello",
                Photo = "Hihi",
                Email = "khanh@gmail.com",
                
            };
            IEmployeeDAL dal = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            var data = dal.Add(employee);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Pagination(int page, int pageSize, string searchValue)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;

            IEmployeeDAL dal = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            var data = dal.List(page, pageSize, searchValue);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}