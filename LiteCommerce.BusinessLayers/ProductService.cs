using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DataLayers;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng tác nghiệp liên quan đến quản lý hàng hóa
    /// </summary>
    public static class ProductService
    {

        private static IProductDAL ProductDB;
        /// <summary>
        /// Khởi tạo tính năng tác nghiệp (hàm này phải được gọi nếu muốn sử dụng các tính năng của lớp)
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    ProductDB = new DataLayers.SQLServer.ProductDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database type is not supported");
            }
        }
        public static List<Product> List(int page, int pageSize, int categoryId, int supplierId, string searchValue, out int rowCount)
        {
            rowCount = ProductDB.Count(categoryId, supplierId, searchValue);
            return ProductDB.List(page, pageSize, categoryId, supplierId, searchValue);
        }

        public static Product Get(int productId)
        {
            return ProductDB.Get(productId);
        }


        public static ProductEx GetEx(int productId)
        {
            return ProductDB.GetEx(productId);
        }


        public static int Add(Product data)
        {
            return ProductDB.Add(data);
        }

        public static bool Update(Product data)
        {
            return ProductDB.Update(data);
        }
        public static bool Delete(int productId)
        {
            return ProductDB.Delete(productId);
        }


        public static List<ProductAttribute> ListAttributes(int productId)
        {
            return ProductDB.ListAttributes(productId);
        }

        public static ProductAttribute GetAttribute(int productId)
        {
            return ProductDB.GetAttribute(productId);
        }
        
        public static long AddAttribute(ProductAttribute data)
        {
            return ProductDB.AddAttribute(data);
        }


        public static bool UpdateAttribute(ProductAttribute data)
        {
            return ProductDB.UpdateAttribute(data);
        }



        /// <summary>
        /// xóa (nhiều) attribute
        /// </summary>
        /// <param name="attributeIds"></param>
        public static void DeleteAttribute(long[] attributeIds)
        {
            foreach (var id in attributeIds)
            {
                ProductDB.DeleteAttribute(id);
            }
        }



        public static ProductGallery GetGallery(long galleryId)
        {
            return ProductDB.GetGallery(galleryId);
        }


        public static long AddGallery(ProductGallery data)
        {
            return ProductDB.AddGallery(data);
        }

        public static bool UpdateGallery(ProductGallery data)
        {
            return ProductDB.UpdateGallery(data);
        }
        public static void DeleteGalleries(long[] galleryIds)
        {
            foreach (var id in galleryIds)
            {
                ProductDB.DeleteGallery(id);
            }
        }

        public static long GetMaxProductId()
        {
            return ProductDB.GetMaxId();
        }
    }
}
