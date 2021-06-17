using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult List(int page = 1, string searchValue = "")
        {
            try {
                int rowCount = 0;
                int pageSize = 10;

                var listOfCustomers = DataService.ListCustomers(page, pageSize, searchValue, out rowCount);

                Models.BasePaginationQueryResult model = new Models.CustomerPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfCustomers
                };

                return View(model);
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin của khách hàng";

            var model = DataService.GetCustomer(id);
            if (model == null)
                RedirectToAction("Index");

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung khách hàng";

            Customer model = new Customer()
            {
                CustomerID = 0
            };

            return View("Edit", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                DataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = DataService.GetCustomer(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
            }
        }


        public ActionResult Save(Customer data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Vui lòng nhập tên khách hàng");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Bạn chưa nhập tên liên hệ của khách hàng");
                if (string.IsNullOrEmpty(data.Address))
                    data.Address = "";
                if (string.IsNullOrEmpty(data.Country))
                    data.Country = "";
                if (string.IsNullOrEmpty(data.City))
                    data.City = "";
                if (string.IsNullOrEmpty(data.PostalCode))
                    data.PostalCode = "";

                if (!ModelState.IsValid)
                {
                    if (data.CustomerID == 0)
                        ViewBag.Title = "Bổ sung khách hàng mới";
                    else
                        ViewBag.Title = "Thay đổi thông tin khách hàng";
                    return View("Edit", data);
                }

                if (data.CustomerID == 0)
                    DataService.AddCustomer(data);
                else
                    DataService.UpdateCustomer(data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Hehe hình như có lỗi rồi :D");
            }
        }
    }
}