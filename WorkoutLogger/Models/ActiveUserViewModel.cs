using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WorkoutLogger.Utilities;

namespace WorkoutLogger.Models
{
    public class RecentWorkoutsViewModel
    {
        public List<NewWorkoutViewModel> RecentWorkoutList { get; set; }

        public int TotalWorkoutsCount { get; set; }

        public RecentWorkoutsViewModel()
        {
            RecentWorkoutList = new List<NewWorkoutViewModel>();
        }
    }

    public class NewWorkoutViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "UserName")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public WorkoutType WorkoutType { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Length")]
        public int Length { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Intensity")]
        public Intensity Intensity { get; set; }
    }


    public class ActiveUserDbContext : DbContext
    {
        public DbSet<NewWorkoutViewModel> Workouts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewWorkoutViewModel>().ToTable("UserWorkouts");

            base.OnModelCreating(modelBuilder);
        }

        public static ActiveUserDbContext Create()
        {
            return new ActiveUserDbContext();
        }
    }
}