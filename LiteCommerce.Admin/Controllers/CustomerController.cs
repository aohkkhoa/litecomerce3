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
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            var listCustomers = DataService.ListCustomers(page, pageSize, searchValue, out rowCount);

            var model = new Models.CustomerPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listCustomers
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
            ViewBag.Title = "Thay đổi thông tin khách hàng";
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
            ViewBag.Title = "Thêm thông tin khách hàng";
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
            ViewBag.Title = "Xóa khách hàng";
            if (Request.HttpMethod == "GET")
            {
                var model = DataService.GetCustomer(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
            }
            else
            {
                DataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Customer data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Vui long nhap ten khach hang !");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Vui long nhap ten giao dich !");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Vui long nhap dia chi !");

                if (string.IsNullOrEmpty(data.Country))
                    data.Country = "";
                if (string.IsNullOrEmpty(data.City))
                    data.City = "";
                if (string.IsNullOrEmpty(data.PostalCode))
                    data.PostalCode = "";
                if (string.IsNullOrEmpty(data.Email))
                    data.Email = "";
                if (string.IsNullOrEmpty(data.Password))
                    data.Password = "";

                if (!ModelState.IsValid)
                {
                    if (data.CustomerID == 0)
                        ViewBag.Title = "Bo sung khach hang";
                    else
                        ViewBag.Title = "Thay doi thong tin khach hàng";
                    return View("Edit", data);
                }

                if (data.CustomerID == 0)
                    DataService.AddCustomer(data);
                else
                    DataService.UpdateCustomer(data.CustomerID, data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
    }
}