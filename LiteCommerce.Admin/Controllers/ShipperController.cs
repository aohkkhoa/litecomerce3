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
    public class ShipperController : Controller
    {
        // GET: Shipper
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 3;
            var listShippers = DataService.ListShippers(page, pageSize, searchValue, out rowCount);

            var model = new Models.ShipperPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listShippers
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin nhà vận chuyển";
            var model = DataService.GetShipper(id);
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
            ViewBag.Title = "Thêm thông tin nhà vận chuyển";
            Shipper model = new Shipper()
            {
                ShipperID = 0
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
            ViewBag.Title = "Xóa nhà vận chuyển";
            if (Request.HttpMethod == "GET")
            {
                var model = DataService.GetShipper(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
            }
            else
            {
                DataService.DeleteShipper(id);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Shipper data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError("ShipperName", "Vui long nhap ten nha van chuyen !");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Vui long nhap so dien thoai nha van chuyen !");

                if (!ModelState.IsValid)
                {
                    if (data.ShipperID == 0)
                        ViewBag.Title = "Bo sung nha van chuyen";
                    else
                        ViewBag.Title = "Thay doi thong tin nha van chuyen";
                    return View("Edit", data);
                }

                if (data.ShipperID == 0)
                    DataService.AddShipper(data);
                else
                    DataService.UpdateShipper(data.ShipperID, data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
    }
}