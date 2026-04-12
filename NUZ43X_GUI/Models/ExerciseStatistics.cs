using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUZ43X_GUI.Models
{
    public class ExerciseStatistics
    {
        public string ExerciseName { get; set; }
        public int WorkoutOccurrences { get; set; }
        public int TotalSets { get; set; }
        public int TotalRepetitions { get; set; }
        public double MaxWeight { get; set; }
        public double TotalVolume { get; set; }
        public double BestEstimatedOneRm { get; set; }

        public ExerciseStatistics()
        {
            ExerciseName = string.Empty;
        }
    }
}
