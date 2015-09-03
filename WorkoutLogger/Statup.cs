using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using WorkoutLogger.Models;

[assembly: OwinStartupAttribute(typeof(WorkoutLogger.Statup))]
namespace WorkoutLogger
{
    public partial class Statup
    {
        public void Configuration(IAppBuilder app)
        {
            ExecuteConfigure(app);
        }

        public void ExecuteConfigure(IAppBuilder app)
        {
            app.CreatePerOwinContext(UserDbContext.Create);
            app.CreatePerOwinContext(ActiveUserDbContext.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<UserSignInManager>(UserSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });  
        }
    }
}