using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkoutLogger.Controllers
{
    public class ActiveUserController : Controller
    {
        #region Recent Workouts
        [AllowAnonymous]
        // GET: RecentWorkouts
        public ActionResult RecentWorkouts()
        {
            return View();
        }

        #endregion

        #region New Workout
        // GET: EnterNewWorkout
        [AllowAnonymous]
        public ActionResult EnterNewWorkout()
        {
            return View();
        }
        #endregion
    }
}