using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WorkoutLogger.Utilities;

namespace WorkoutLogger.Models
{
    public class ActiveUserViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public WorkoutType WorkoutType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Length")]
        public DateTime Length { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Intensity")]
        public Intensity Intensity { get; set; }
    }
}