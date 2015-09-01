using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.Owin;
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
            app.CreatePerOwinContext(DbContext.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<UserSignInManager>(UserSignInManager.Create);
        }
    }
}