using MvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authorize(MvcApp.Models.LoginTable loginTable)
        {
            using (LoginTableEntities db = new LoginTableEntities())
            {
                var userDetails = db.LoginTables.Where(x => x.UserName == loginTable.UserName && x.Password == loginTable.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    loginTable.LoginErrorMessage = "Invalid Username or Password";
                    return View("Index", loginTable);
                }
                else
                {
                    Session["userId"] = userDetails.UserId;
                    return RedirectToAction("Index", "Home");
                }
            }

        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}