using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;

namespace Assignment6_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult NewPassword(UserDTO dto)
        {
            string mail = Request.QueryString["login"];
            string code_for_reset = Request.QueryString["code_for_reset"];

            bool flag = BAL.UserBAL.Verify_Code(mail, code_for_reset);
            if (flag)
            {
                return View();
            }
            else
                return RedirectToAction("InvalidCode");
        }
        public ActionResult InvalidCode()
        {
            return View();
        }
    }
}
