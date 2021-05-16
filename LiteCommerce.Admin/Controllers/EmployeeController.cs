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
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 3;
            var listEmployees = DataService.ListEmployees(page, pageSize, searchValue, out rowCount);

            var model = new Models.EmployeePaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listEmployees
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
            ViewBag.Title = "Thay đổi thông tin nhân viên";
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
            ViewBag.Title = "Thêm thông tin nhân viên";
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
            ViewBag.Title = "Xóa nhân viên";
            if (Request.HttpMethod == "GET")
            {
                var model = DataService.GetEmployee(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
            }
            else
            {
                DataService.DeleteEmployee(id);
                return RedirectToAction("Index");
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
                if (string.IsNullOrWhiteSpace(data.FirstName))
                    ModelState.AddModelError("FirstName", "Vui long nhap họ nhan vien !");
                if (string.IsNullOrWhiteSpace(data.LastName))
                    ModelState.AddModelError("LastName", "Vui long nhap tên nhân viên !");
                if (string.IsNullOrWhiteSpace(Convert.ToString(data.BirthDate)))
                    ModelState.AddModelError("BirthDate", "Vui long nhap ngay sinh !");

                if (string.IsNullOrEmpty(data.Notes))
                    data.Notes = "";
                if (string.IsNullOrEmpty(data.Photo))
                    data.Photo = "";
                if (string.IsNullOrEmpty(data.Email))
                    data.Email = "";
                if (string.IsNullOrEmpty(data.Password))
                    data.Password = "";

                if (!ModelState.IsValid)
                {
                    if (data.EmployeeID == 0)
                        ViewBag.Title = "Bo sung nhan vien";
                    else
                        ViewBag.Title = "Thay doi thong tin nhan vien";
                    return View("Edit", data);
                }

                if (data.EmployeeID == 0)
                    DataService.AddEmployee(data);
                else
                    DataService.UpdateEmployee(data.EmployeeID, data);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }
        }
    }
}