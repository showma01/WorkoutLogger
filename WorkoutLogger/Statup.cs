using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkoutLogger.Statup))]
namespace WorkoutLogger
{
    public partial class Statup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}