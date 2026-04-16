using System.Collections.ObjectModel;

namespace NUZ43X_GUI.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
      
        public ObservableCollection<WorkoutEntry> Entries { get; set; }

        public Workout()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Title = string.Empty;
            Entries = new ObservableCollection<WorkoutEntry>();
        }
    }
}
