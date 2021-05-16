using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        // GET: Account
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string loginName = "", string password = "")
        {
            ViewBag.LoginName = loginName;

            if(Request.HttpMethod == "POST")
            {
                var account = AccountService.Authorize(loginName, CryptHelper.Md5(password));
                if(account == null)
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập bị sai");
                    return View();
                }
                FormsAuthentication.SetAuthCookie(CookieHelper.AccountToCookieString(account), false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Save(Employee data, string password = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.Password) || data.Password != CryptHelper.Md5(password))
                    ModelState.AddModelError("Password", "Mat khau sai !");
                return RedirectToAction("Profile");
            }
            catch
            {
                return Content("Oops! Trang nay khong ton tai :)");
            }

        }
    }
}