using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WorkoutLogger.Models;

namespace WorkoutLogger.Controllers
{
    public class ActiveUserController : Controller
    {
        #region Properties
        private ActiveUserDbContext _workoutDB;
        public ActiveUserDbContext WorkoutDB
        {
            get { return _workoutDB ?? HttpContext.GetOwinContext().Get<ActiveUserDbContext>(); }
            set { _workoutDB = value; }
        }
        #endregion

        #region Constructor

        public ActiveUserController()
        {
            
        }

        public ActiveUserController(ActiveUserDbContext workoutDb)
        {
            WorkoutDB = workoutDb;
        }
        #endregion

        #region Recent Workouts
        [Authorize]
        // GET: RecentWorkouts
        public ActionResult RecentWorkouts(int page = 0)
        {
            if (Request.IsAuthenticated)
            {
                var model = new RecentWorkoutsViewModel();
                if (ModelState.IsValid)
                {
                    var userId = User.Identity.Name;
                    model.TotalWorkoutsCount = WorkoutDB.Workouts.Count(x => x.UserEmail == userId);

                    if (model.TotalWorkoutsCount/5 < page)
                        page = model.TotalWorkoutsCount;
                    else if (page < 0)
                        page = 0;

                    var results =
                        WorkoutDB.Workouts.Where(x => x.UserEmail == userId).OrderBy(y => y.Id).Skip(page*5).Take(5);
                    if (results.Any())
                    {
                        foreach (var workout in results)
                        {
                            model.RecentWorkoutList.Add(workout);
                        }
                    }
                }
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        #endregion

        #region New Workout
        // GET: EnterNewWorkout
        [AllowAnonymous]
        public ActionResult EnterNewWorkout()
        {
            if(Request.IsAuthenticated)
                return View();
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnterNewWorkout(NewWorkoutViewModel model, string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                if (model != null)
                {
                    model.UserEmail = User.Identity.Name;
                    WorkoutDB.Workouts.Add(model);
                    if (ModelState.IsValid)
                    {
                        await WorkoutDB.SaveChangesAsync();
                        ModelState.AddModelError(string.Empty, "New workout created!");
                        return View();
                    }
                }
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && _workoutDB != null)
            {
                _workoutDB.Dispose();
                _workoutDB = null;
            }

            base.Dispose(disposing);
        }

    }
}