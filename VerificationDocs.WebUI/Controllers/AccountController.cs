using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerificationDocs.Domain.Entities;
using VerificationDocs.Domain.Concrete;
using VerificationDocs.Domain.Abstract;
using VerificationDocs.WebUI.Infrastructure.Abstract;
using VerificationDocs.WebUI.Models;
using System.Web.Security;


namespace VerificationDocs.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        ISecurityRepository secRepository;
        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }
        public AccountController(ISecurityRepository repo)
        {
            secRepository = repo;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel/*Security*/ model/*, string returnUrl*/)
        {

            if (ModelState.IsValid)
            {
                Security user = null;
                bool check;
                using (SecurityContext db = new SecurityContext())
                {
                    //user = db.UsersSec.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                    user = db.UsersSec.Where(u => u.Login == model.Login).SingleOrDefault();/* &&u.Password == model.Password).FirstOrDefault();*/
                    check = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

                }
                if (user != null && check == true)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "CheckFiles");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    return View();
                }
                //if (authProvider.Authenticate(model.Login, model.Password))
                //{
                //    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Неправильный логин или пароль");
                //    return View();
                //}
            }
            else
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Security user = null;
                bool check;
                using (SecurityContext db = new SecurityContext())
                {
                    user = db.UsersSec.FirstOrDefault(u => u.Login == model.Login);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (SecurityContext db = new SecurityContext())
                    {
                        db.UsersSec.Add(new Security
                        {
                            Login = model.Login,
                            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                            //Password = model.Password,
                            Role = model.Role
                        });
                        db.SaveChanges();

                        user = db.UsersSec.Where(u => u.Login == model.Login).First();/* &&u.Password == model.Password).FirstOrDefault();*/
                        check = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null && check == true)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "CheckFiles");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "MainPage");
        }
    }
}