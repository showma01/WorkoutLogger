using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using WorkoutLogger.Models;

namespace WorkoutLogger.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {

        #region Managers

        private UserSignInManager _signInManager;
        public UserSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<UserSignInManager>(); }
            set { _signInManager = value; }
        }

        private UserManager _userManager;
        public UserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().Get<UserManager>(); }
            set { _userManager = value; }
        }
        #endregion

        #region Login Methods
        [AllowAnonymous]
        public ActionResult Login(string url)
        {
            ViewBag.ReturnUrl = url;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string url)
        {
            if (ModelState.IsValid)
            {
                var validLogin = await SignInManager.PasswordSignInAsync(model.UserEmail, model.UserPassword, false, false);
                Console.WriteLine(validLogin);
                switch (validLogin)
                {
                    case SignInStatus.Success:
                        if (Url.IsLocalUrl(url))
                            return Redirect(url);
                        return RedirectToAction("Active", "ActiveUser");
                        break;
                    default:
                        ModelState.AddModelError(string.Empty, "Invalid login.");
                        return View(model);
                        break;
                }
            }
            return View(model);
        }
        #endregion

        #region Register Methods

        [AllowAnonymous]
        public ActionResult RegisterNewUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterNewUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User {UserName = model.Email, Email = model.Email};
                var createResult = await UserManager.CreateAsync(newUser, model.Password);
                if (createResult.Succeeded)
                {
                    await SignInManager.SignInAsync(newUser, false, false);
                    return RedirectToAction("Active", "ActiveUser");
                }
            }
            return View(model);
        }
        #endregion
    }
}