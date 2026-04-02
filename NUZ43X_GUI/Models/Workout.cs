using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUZ43X_GUI.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public ObservableCollection<WorkoutEntry> Entries { get; set; }

        public Workout()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Title = string.Empty;
            Notes = string.Empty;
            Entries = new ObservableCollection<WorkoutEntry>();
        }
    }
}
