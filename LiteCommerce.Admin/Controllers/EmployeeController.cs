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
    public class EmployeeController : Controller
    {
        public ActionResult List(int page = 1, string searchValue = "")
        {
            try
            {
                int rowCount = 0;
                int pageSize = 10;

                var listOfEmployees = DataService.ListEmployees(page, pageSize, searchValue, out rowCount);

                Models.BasePaginationQueryResult model = new Models.EmployeePaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfEmployees
                };

                return View(model);
            }catch (Exception e)
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
            ViewBag.Title = "Thay đổi thông tin của nhân viên";

            var model = DataService.GetEmployee(id);
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
            ViewBag.Title = "Bổ sung nhân viên";

            Employee model = new Employee()
            {
                EmployeeID = 0
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
                DataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = DataService.GetEmployee(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Employee data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.LastName))
                    data.LastName = "";
                if (string.IsNullOrWhiteSpace(data.FirstName))
                    ModelState.AddModelError("FirstName", "Vui lòng nhập tên nhân viên");
                if (string.IsNullOrEmpty(data.Photo))
                    data.Photo = "";
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Vui lòng nhập email nhân viên");
                if (string.IsNullOrEmpty(data.Notes))
                    data.Notes = "";

                if (!ModelState.IsValid)
                {
                    if (data.EmployeeID == 0)
                        ViewBag.Title = "Bổ sung nhân viên mới";
                    else
                        ViewBag.Title = "Thay đổi thông tin nhân viên";
                    return View("Edit", data);
                }

                if (data.EmployeeID == 0)
                    DataService.AddEmployee(data);
                else
                    DataService.UpdateEmployee(data);
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