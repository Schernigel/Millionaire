using System.Web.Mvc;
using System.Web.Security;
using Millionaire.WebUI.Infrastructure.Abstuct;
using Millionaire.WebUI.Models;

namespace Millionaire.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;
        

        public AccountController(IAuthProvider auth)
        {
            _authProvider = auth;
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("StartGame", "Game");
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.Email, model.Password))
                {
                    //HttpCookie cookie = new HttpCookie("My cookie");
                    //cookie["Name"] = "Sergey";
                    //cookie["Id"] = "4";
                    //Response.Cookies.Add(cookie);
                    Session["Email"] = model.Email;
                    return Redirect(returnUrl ?? Url.Action("StartGame", "Game"));
                }
                else
                {
                    ModelState.AddModelError("","Неправильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}