using NUZ43X_GUI.Models;
using NUZ43X_GUI.Models.TrainingLog.Models;
using System.Windows;

namespace NUZ43X_GUI
{
    public partial class WorkoutWindow : Window
    {
        public Workout Workout { get; private set; }

        public WorkoutWindow(List<Exercise> exercises)
        {
            InitializeComponent();

            Workout = new Workout();
            DataContext = Workout;

            ExerciseComboBox.ItemsSource = exercises;
        }

        private void AddSet_Click(object sender, RoutedEventArgs e)
        {
            Exercise selectedExercise = ExerciseComboBox.SelectedItem as Exercise;

            if (selectedExercise == null)
            {
                MessageBox.Show("Válassz gyakorlatot.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!double.TryParse(WeightTextBox.Text, out double weight) || weight < 0)
            {
                MessageBox.Show("Adj meg egy érvényes súlyt.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!int.TryParse(RepetitionsTextBox.Text, out int repetitions) || repetitions <= 0)
            {
                MessageBox.Show("Adj meg egy érvényes ismétlésszámot.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            WorkoutEntry existingEntry = Workout.Entries.FirstOrDefault(ei => ei.ExerciseId == selectedExercise.Id);

            if (existingEntry == null)
            {
                existingEntry = new WorkoutEntry
                {
                    ExerciseId = selectedExercise.Id,
                    ExerciseName = selectedExercise.Name
                };

                Workout.Entries.Add(existingEntry);
            }

            SetEntry newSet = new SetEntry
            {
                SetNumber = existingEntry.Sets.Count + 1,
                Weight = weight,
                Repetitions = repetitions
            };

            existingEntry.Sets.Add(newSet);

            WorkoutEntriesDataGrid.Items.Refresh();

            WeightTextBox.Clear();
            RepetitionsTextBox.Clear();
            WeightTextBox.Focus();

            WorkoutEntriesDataGrid.Items.Refresh();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Workout.Entries.Count == 0)
            {
                MessageBox.Show("Legalább egy sorozatot adj hozzá az edzéshez.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
