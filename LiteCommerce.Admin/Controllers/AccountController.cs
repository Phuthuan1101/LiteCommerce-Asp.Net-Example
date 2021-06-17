using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string loginName = "", string password = "")
        {
            ViewBag.LoginName = loginName;

            if (Request.HttpMethod == "POST")
            {
                var account = AccountService.Authorize(loginName, CryptHelper.Md5(password));
                if(account == null)
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(string accountId = "", string oldPassword = "", string newPassword = "", string confirmPassword = "")
        {
            //ViewBag.UserName = accountId;
            //ViewBag.OldPassword = oldPassword;
            //ViewBag.NewPassword = newPassword;
            //ViewBag.ConfirmPassword = confirmPassword;
            if (accountId == null || accountId == "")
                return RedirectToAction("Index", "Home");
            else
            {
                if (newPassword == confirmPassword)
                {
                    bool check = AccountService.ChangePassword(accountId, CryptHelper.Md5(oldPassword), CryptHelper.Md5(newPassword));
                    if (check == false)
                    {
                        ModelState.AddModelError("", "Sai mật khẩu cũ");
                    }
                    else
                    {
                        AccountService.ChangePassword(accountId, CryptHelper.Md5(oldPassword), CryptHelper.Md5(newPassword));
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Đổi mật khẩu thất bại");
                    return View();
                }
            }

            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword(string accountId = "")
        {
            ViewBag.UserName = accountId;
            return View();
        }
    }
}