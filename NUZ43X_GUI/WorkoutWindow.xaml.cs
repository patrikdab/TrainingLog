using NUZ43X_GUI.Models;
using NUZ43X_GUI.Models.TrainingLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
            Exercise selectedExercise = ExerciseComboBox.SelectedItem as Exercise;

            if (selectedExercise == null)
            {
                MessageBox.Show("Válassz gyakorlatot.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            WorkoutEntry entry = new WorkoutEntry
            {
                ExerciseId = selectedExercise.Id,
                ExerciseName = selectedExercise.Name
            };

            Workout.Entries.Add(entry);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
