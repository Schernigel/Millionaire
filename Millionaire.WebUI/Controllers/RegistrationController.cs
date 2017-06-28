using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Millionaire.Domain.Abstract;
using Millionaire.Domain.Concrete;
using Millionaire.Domain.Entities;

namespace Millionaire.WebUI.Controllers
{
    public class RegistrationController : Controller
    {
        //EfUserRepository repository = new EfUserRepository();
        private IUserRepository _userRepository;

        public RegistrationController(IUserRepository repo)
        {
            _userRepository = repo;
        }

        // GET: Registration
        [HttpGet]
        public ActionResult RegForm()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult RegForm(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.InsertUser(user);
                    _userRepository.Save();
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.Try again, and if the problem persists contact your system administrator.");
            }
            
            return Redirect("/Account/Login");
        }

    }
}