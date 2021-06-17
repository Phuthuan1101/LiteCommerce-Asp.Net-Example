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
    public class SupplierController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult List(int page = 1, string searchValue = "")
        {
            try
            {
                int rowCount = 0;
                int pageSize = 10;

                var listOfSuppliers = DataService.ListSuppliers(page, pageSize, searchValue, out rowCount);

                Models.BasePaginationQueryResult model = new Models.SupplierPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfSuppliers
                };

                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin của nhà cung cấp";

            var model = DataService.GetSupplier(id);
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
            ViewBag.Title = "Bổ sung nhà cung cấp mới";

            Supplier model = new Supplier()
            {
                SupplierID = 0
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
                // Xóa Supplier có mã là id
                DataService.DeleteSupplier(id);
                // Quay về lại trang Index
                return RedirectToAction("Index");
            }
            else
            {
                // Lấy thông tin của Supplier cần xóa
                var model = DataService.GetSupplier(id);
                if (model == null)
                    RedirectToAction("Index");

                // Trả thông tin về cho view để hiển thị
                return View(model);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Supplier data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError("SupplierName", "Vui lòng nhập tên nhà cung cấp");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Bạn chưa nhập tên liên hệ của nhà cung cấp");
                if (string.IsNullOrEmpty(data.Address))
                    data.Address = "";
                if (string.IsNullOrEmpty(data.Country))
                    data.Country = "";
                if (string.IsNullOrEmpty(data.City))
                    data.City = "";
                if (string.IsNullOrEmpty(data.PostalCode))
                    data.PostalCode = "";
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Bạn chưa số điện thoại của nhà cung cấp");

                if (!ModelState.IsValid)
                {
                    if (data.SupplierID == 0)
                        ViewBag.Title = "Bổ sung nhà cung cấp mới";
                    else
                        ViewBag.Title = "Thay đổi thông tin nhà cung cấp";
                    return View("Edit", data);
                }

                if (data.SupplierID == 0)
                    DataService.AddSupplier(data);
                else
                    DataService.UpdateSupplier(data);
                //return Json(data);
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Hehe hình như có lỗi rồi :D");
            }

        }
    }
}