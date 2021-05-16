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
        public ActionResult List(int CategoryID = 0, int SupplierID = 0, string searchValue = "", int page = 1)
        {
            try
            {
                int rowCount = 0;
                int pageSize = 6;
                var listOfProduct = ProductService.List(page, pageSize, CategoryID, SupplierID, searchValue, out rowCount);

                var model = new Models.ProductPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfProduct
                };
                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ActionResult Edit(int id)
        //{
        //    ViewBag.Title = "Thay đổi thông tin hang hoa";

        //    var model = ProductService.Get(id);

        //    if (model == null)
        //        return RedirectToAction("Index");
        //    return View(model);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Thêm thông tin hang hoa";

            Product model = new Product()
            {
                ProductID = 0
            };

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Xóa hang hoa";

            if (Request.HttpMethod == "GET")
            {
                var model = ProductService.Get(id);
                if (model == null)
                    return RedirectToAction("Index");
                return View(model);
            }
            else
            {
                ProductService.Delete(id);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Product data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ProductName))
                    ModelState.AddModelError("ProductName", "Vui long nhap ten hàng hóa !");
                if (string.IsNullOrWhiteSpace(data.Photo))
                    ModelState.AddModelError("Photo", "Ban chua thêm ảnh !");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.Price)))
                    ModelState.AddModelError("Price", "Ban chua nhập giá bán !");
                if (string.IsNullOrWhiteSpace(data.Unit))
                    ModelState.AddModelError("Unit", "Ban chua nhập đơn vị tính !");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.CategoryID)))
                    ModelState.AddModelError("CategoryID", "Ban chua nhập loại hàng !");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.SupplierID)))
                    ModelState.AddModelError("SupplierID", "Ban chua nhập nhà cung cấp !");

                if (!ModelState.IsValid)
                {
                    if (data.ProductID == 0)
                        ViewBag.Title = "Bo sung hàng hóa";
                    else
                        ViewBag.Title = "Thay doi thong tin hàng hóa";
                    return View("Edit", data);
                }

                if (data.ProductID == 0)
                {
                    var id = ProductService.Add(data);
                    var model = ProductService.GetEx(id);
                    return View("Edit", model);
                }
                else
                    ProductService.Update(data);

                return RedirectToAction("Edit", new { id = data.ProductID });
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var model = ProductService.GetEx(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
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
            ProductService.DeleteAttributes(attributeIds);
            return RedirectToAction("Edit", new { id = id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="galleryIds"></param>
        /// <returns></returns>
        public ActionResult DeleteGalleries(int id, long[] galleryIds)
        {
            if (galleryIds == null)
                return RedirectToAction("Edit", new { id = id });
            ProductService.DeleteGalleries(galleryIds);
            return RedirectToAction("Edit", new { id = id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditAttribute(int id)
        {
            ViewBag.Title = "Thay đổi thông tin thuộc tính";

            var model = ProductService.GetAttribute(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditGallery(int id)
        {
            ViewBag.Title = "Thay đổi thông tin anh";

            var model = ProductService.GetGallery(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAttribute()
        {
            ViewBag.Title = "Thêm thông tin thuoc tinh";

            ProductAttribute model = new ProductAttribute()
            {
                AttributeID = 0
            };

            return View("EditAttribute", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddGallery()
        {
            ViewBag.Title = "Thêm thông tin anh";

            ProductGallery model = new ProductGallery()
            {
                GalleryID = 0
            };

            return View("EditGallery", model);
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
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.ProductID)))
                    ModelState.AddModelError("ProductID", "Vui long nhap ten hàng hóa !");
                if (string.IsNullOrWhiteSpace(data.AttributeName))
                    ModelState.AddModelError("AttributeName", "Vui long nhap ten thuộc tính!");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.DisplayOrder)))
                    ModelState.AddModelError("DisplayOrder", "Ban chua nhập giá bán !");
                if (string.IsNullOrWhiteSpace(data.AttributeValue))
                    ModelState.AddModelError("AttributeValue", "Ban chua nhập đơn vị tính !");

                if (!ModelState.IsValid)
                {
                    if (data.AttributeID == 0)
                        ViewBag.Title = "Bo sung thuoc tinh";
                    else
                        ViewBag.Title = "Thay doi thong tin thuoc tinh";
                    return View("EditAttribute", data);
                }

                if (data.AttributeID == 0)
                    ProductService.AddAttribute(data);
                else
                    ProductService.UpdateAttribute(data);

                return RedirectToAction("Edit", new { id = data.ProductID });

            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
        public ActionResult SaveGallery(ProductGallery data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.ProductID)))
                    ModelState.AddModelError("ProductID", "Vui long nhap ten hàng hóa !");
                if (string.IsNullOrWhiteSpace(data.Photo))
                    ModelState.AddModelError("Photo", "Vui long nhap anh");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.DisplayOrder)))
                    ModelState.AddModelError("DisplayOrder", "Ban chua nhập ma");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.IsHidden)))
                    ModelState.AddModelError("IsHidden", "Ban chua nhập trang thai");
                if (string.IsNullOrEmpty(data.Description))
                    data.Description = "";

                if (!ModelState.IsValid)
                {
                    if (data.GalleryID == 0)
                        ViewBag.Title = "Bo sung thong tin anh";
                    else
                        ViewBag.Title = "Thay doi thong tin anh";
                    return View("EditAttribute", data);
                }

                if (data.GalleryID == 0)
                    ProductService.AddGallery(data);
                else
                    ProductService.UpdateGallery(data);

                return RedirectToAction("Edit", new { id = data.ProductID });

            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
    }
}