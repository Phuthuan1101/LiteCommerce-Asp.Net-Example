using LiteCommerce.BusinessLayers;
using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SQLServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class ShipperController : Controller
    {
       
        public ActionResult List(int page = 1, string searchValue = "")
        {
            try
            {
                int rowCount = 0;
                int pageSize = 10;
                var listOfShippers = DataService.ListShippers(page, pageSize, searchValue, out rowCount);
                var model = new Models.ShipperPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfShippers
                };
                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult Index()
        {
             return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Sữa đổi thông tin nhà vận chuyển";

            var model = DataService.GetShipper(id);
            if (model == null)
            {
                RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Add(string id)
        {
            ViewBag.Title = "Thêm nhà vận chuyển";

            Shipper model = new Shipper()
            {
                ShipperID = 0
            };
            return View("Edit", model);
        }
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "GET")
            {
                var model = DataService.GetShipper(id);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                DataService.DeleteShipper(id);
                return RedirectToAction("Index");
            }
           
        }
        public ActionResult Save(Shipper data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                {
                    ModelState.AddModelError("ShipperName", "Vui lòng nhập tên nhà cung cấp!");
                }

                if (string.IsNullOrWhiteSpace(data.Phone))
                {
                    ModelState.AddModelError("Phone", "Vui lòng nhập số điện thoại nhà cung cấp!");
                }

                if (!ModelState.IsValid)
                {
                    if (data.ShipperID == 0)
                    {
                        ViewBag.Title = "Thêm nhà vận chuyển";
                    }
                    else
                    {
                        ViewBag.Title = "Sữa đổi thông tin nhà vận chuyển";
                    }
                    return View("Edit", data);
                }

                if (data.ShipperID == 0)
                {
                    DataService.AddShipper(data);
                }
                else
                {
                    DataService.UpdateShipper(data);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! Trang này không tồn tại!");
            }

        }
    }
}