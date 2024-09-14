using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PasswordHashingThree.Data;
using PasswordHashingThree.Models;

namespace PasswordHashingThree.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Account account, string plainPassword)
        {

            account.Password = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(account);
                    transaction.Commit();
                    return RedirectToAction("Index");
                }

            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string plainPassword)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var user = session.Query<Account>().FirstOrDefault(u => u.Username == username);
                if (user != null && BCrypt.Net.BCrypt.Verify(plainPassword, user.Password))
                {
                    return RedirectToAction("HomePage");
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
        public ActionResult HomePage()
        {
            return Content("Login Successfull!!!!!!");
        }
    }
}