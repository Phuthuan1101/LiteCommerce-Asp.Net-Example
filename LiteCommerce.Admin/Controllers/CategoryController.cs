using LiteCommerce.BusinessLayers;
using LiteCommerce.DataLayers.SQLServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
        [Authorize]
    public class CategoryController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult List(int page=1 , string searchValue = "")
        {
            try
            {
                int rowCount = 0;
                int pageSize = 10;
                var listOfCategorys = DataService.ListCategories(page, pageSize, searchValue, out rowCount);
                var model = new Models.CategoryPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfCategorys
                };
                return View(model);
            }catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        // GET: Category
        public ActionResult Index()
        {          
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Sữa loại hàng";
            var model = DataService.GetCategory(id);
            if (model == null)
            {
                RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Add(string id)
        {
            ViewBag.Title = "Thêm loại hàng";
            Category model = new Category()
            {
                CategoryID = 0
            };
            return View("Edit", model);
        }
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "GET")
            {
                //lấy thông tin của category cần xóa
                var model = DataService.GetCategory(id);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            else
            {
                // xóa thông tin category có mã là id
                DataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
         
        }
        public ActionResult Save(Category data)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                {
                    ModelState.AddModelError("CategoryName", "Vui lòng nhập tên loại hàng!");
                }
                if (string.IsNullOrWhiteSpace(data.Description))
                {
                    ModelState.AddModelError("Description", "Vui lòng nhập mô tả!");
                }

                if (!ModelState.IsValid)
                {
                    if (data.CategoryID == 0)
                        ViewBag.Title = "Thêm loại hàng";
                    else
                        ViewBag.Title = "Sữa đổi thông tin loại hàng";
                    return View("Edit", data);
                }
                if (data.CategoryID == 0)
                {
                    DataService.AddCategory(data);
                }
                else
                {
                    DataService.UpdateCategory(data);
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