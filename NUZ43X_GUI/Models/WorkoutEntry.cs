using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUZ43X_GUI.Models
{
    public class WorkoutEntry
    {
        public Guid Id { get; set; }
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public string Notes { get; set; }

        public List<SetEntry> Sets { get; set; }

        public WorkoutEntry()
        {
            Id = Guid.NewGuid();
            ExerciseId = Guid.Empty;
            ExerciseName = string.Empty;
            Notes = string.Empty;
            Sets = new List<SetEntry>();
        }
        public string SetSummary
        {
            get
            {
                return string.Join(", ", Sets.Select(s => $"{s.Weight} kg x {s.Repetitions}"));
            }
        }
        public int TotalRepetitions => Sets.Sum(s => s.Repetitions);
        public double TotalVolume => Sets.Sum(s => s.Weight * s.Repetitions);
        public double MaxWeight => Sets.Count == 0 ? 0 : Sets.Max(s => s.Weight);
    }
}
