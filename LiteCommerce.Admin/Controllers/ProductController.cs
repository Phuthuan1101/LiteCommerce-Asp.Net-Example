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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var listCategory = DataService.ListCategories();
            var listSupplier = DataService.ListSuppliers(); 
            var model = new Models.ListSupplierAndCategory {listSuppliers  = listSupplier,listCategoris = listCategory }; ;
            return View(model);
        }

        public ActionResult List(int category = 0, int supplier = 0, string searchValue = "", int page = 1)
        {
            try
            {
                int rowCount = 0;
                int pageSize = 10;
                var listOfProducts = ProductService.List(page, pageSize, category, supplier, searchValue, out rowCount);
                var model = new Models.ProductPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfProducts
                };
                return View(model);

            } catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var model = ProductService.GetEx(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Detailts(int id)
        {
            var model = ProductService.GetEx(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        /// <summary>
        /// Thêm mới một PRODUCT MỚI
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            

            Product model = new Product()
            {
                ProductID = 0
            };

            return View( model);
        }
        /// <summary>
        /// Xóa 1 Mặt hàng theo Id mặt hàng đó
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {

            if (Request.HttpMethod == "POST")
            {
                ProductService.Delete(id);
                return RedirectToAction("Index");
            }
            else
            {

                var model = ProductService.Get(id);
                if (model == null)
                    RedirectToAction("Index");

                // Trả thông tin về cho view để hiển thị
                return View(model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attributeIds"></param>
        /// <returns></returns>
        public ActionResult DeleteAttributes(int id, long[] attributeIds)
        {
            if (attributeIds == null)
                return RedirectToAction("Edit", new { id = id });

            ProductService.DeleteAttribute(attributeIds);
            return RedirectToAction("Edit", new { id = id });
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gallerieIds"></param>
        /// <returns></returns>
        public ActionResult DeleteGalleries(int id, long[] gallerieIds)
        {
            if (gallerieIds == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            ProductService.DeleteGalleries(gallerieIds);
            return RedirectToAction("Edit", new { id = id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Save(Product data)
        {
            if (string.IsNullOrWhiteSpace(data.ProductName))
                ModelState.AddModelError("ProductName", "Tên hàng không được để trống");

            if (!ModelState.IsValid)
            {
                if (data.ProductID == 0)
                {
                    ViewBag.Title = "Bổ sung nhà cung cấp";
                    return View("Add", data);
                }
                else
                {
                    ViewBag.Title = "Cập nhật thông tin của nhà cung cấp";
                    var productEx = ProductService.GetEx(data.ProductID);
                    return View("Edit", productEx);
                }
              
            }

            if (data.ProductID == 0)
            { 
                int id = ProductService.Add(data);
                
                return RedirectToAction("Edit", new { id = id });
            }
            else
            {
                ProductService.Update(data);
                return RedirectToAction("Edit", new { id = data.ProductID });
            }

            
        }
        /// <summary>
        /// thêm mới một  Gallery
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Photo"></param>
        /// <param name="description"></param>
        /// <param name="displayOrder"></param>
        /// <param name="isHidden"></param>
        /// <returns></returns>
        public ActionResult AddGalleries(int id, string Photo, string description, int displayOrder, int isHidden)
        {
            bool check = true;
            if (isHidden == 0) check = false;
            ProductGallery data = new ProductGallery()
            {
                ProductID = id,
                Photo = Photo,
                Description = description,
                DisplayOrder = displayOrder,
                IsHidden = check
            };
            ProductService.AddGallery(data);
            return RedirectToAction("Edit", new { id = id });
        }
        /// <summary>
        /// Chỉnh sửa 1 Gallery
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditGallery(int id)
        {
            ViewBag.Title = "Thay đổi thông tin của ảnh";

            var model = ProductService.GetGallery(id);
            if (model == null)
                RedirectToAction("Index");

            return PartialView(model);
        }
        /// <summary>
        /// lưu thông tin PrductGallery
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveGallery(ProductGallery data)
        {

            
            if (data.GalleryID == 0)
            {
                ProductService.AddGallery(data);
            }
            else
            {
                ProductService.UpdateGallery(data);
            }
            return RedirectToAction("Edit", new { id = data.ProductID });


        }
        /// <summary>
        /// update 1 Product Atriibute
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="displayOrder"></param>
        /// <returns></returns>
        public ActionResult AddAttributes(int id, string attributeName, string attributeValue, int displayOrder)
        {
            ProductAttribute data = new ProductAttribute()
            {
                ProductID = id,
                AttributeName = attributeName,
                AttributeValue = attributeValue,
                DisplayOrder = displayOrder
            };
            ProductService.AddAttribute(data);
            return RedirectToAction("Edit", new { id = id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditAttribute(int id)
        {
            ViewBag.Title = "Thay đổi thông tin của thuộc tính";

            var model = ProductService.GetAttribute(id);
            if (model == null)
                RedirectToAction("Index");

            return PartialView(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            try
            {

                if (data.AttributeID == 0)
                {
                    ProductService.AddAttribute(data);
                }
                else
                {
                    ProductService.UpdateAttribute(data);
                }
                return RedirectToAction("Edit", new { id = data.ProductID });
            }

            catch
            {
                return Content("Hehe hình như có lỗi rồi :D");
            }

        }

    }
}