using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkoutLogger.Utilities
{
    public static class WorkoutHelper
    {
        public static IList<WorkoutType> WorkoutTypeCollection { get; set; }
        public static IList<Intensity> WorkoutIntensityCollection { get; set; }

        static WorkoutHelper()
        {
            WorkoutTypeCollection = new List<WorkoutType>()
            {
                WorkoutType.Bike,
                WorkoutType.Swim,
                WorkoutType.Run,
                WorkoutType.Other
            };
            WorkoutIntensityCollection = new List<Intensity>()
            {
                Intensity.Minimal,
                Intensity.Low,
                Intensity.Medium,
                Intensity.High
            };
        }
    }

    public enum WorkoutType
    {
        Run,
        Bike,
        Swim,
        Other
    }

    public enum Intensity
    {
        High,
        Medium,
        Low,
        Minimal
    }
}