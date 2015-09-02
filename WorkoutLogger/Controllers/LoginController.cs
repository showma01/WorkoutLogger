using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion

        #region Constructor

        public LoginController()
        {
            
        }

        public LoginController(UserManager userManager, UserSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        #endregion

        #region Login Methods
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("RecentWorkouts", "ActiveUser");
            }
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
                        return RedirectToAction("RecentWorkouts", "ActiveUser");
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
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("RecentWorkouts", "ActiveUser");
            }
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
                    return RedirectToAction("RecentWorkouts", "ActiveUser");
                }

                if (createResult.Errors.Any())
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(model);
        }
        #endregion

        #region Log off Methods
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Login");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}