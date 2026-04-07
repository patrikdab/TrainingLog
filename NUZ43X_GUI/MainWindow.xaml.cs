using NUZ43X_GUI.Models;
using NUZ43X_GUI.Models.TrainingLog.Models;
using NUZ43X_GUI.Services;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NUZ43X_GUI
{

    public partial class MainWindow : Window
    {
        private readonly JsonDataService dataService;

        public ObservableCollection<Exercise> Exercises { get; set; }
        public ObservableCollection<Workout> Workouts { get; set; }
        public ObservableCollection<BodyWeightEntry> BodyWeights { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            dataService = new JsonDataService();

            Exercises = dataService.LoadExercises();
            Workouts = dataService.LoadWorkouts();
            BodyWeights = dataService.LoadBodyWeights();

            DataContext = this;
        }

        private void AddExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            ExerciseEditorWindow editorWindow = new ExerciseEditorWindow();
            editorWindow.Owner = this;

            bool? result = editorWindow.ShowDialog();

            if (result == true)
            {
                Exercises.Add(editorWindow.Exercise);
                dataService.SaveExercises(Exercises);
            }
        }

        private void EditExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            Exercise? selectedExercise = ExercisesDataGrid.SelectedItem as Exercise;

            if (selectedExercise == null)
            {
                MessageBox.Show("Először válassz ki egy gyakorlatot.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ExerciseEditorWindow editorWindow = new ExerciseEditorWindow(selectedExercise);
            editorWindow.Owner = this;

            bool? result = editorWindow.ShowDialog();

            if (result == true)
            {
                selectedExercise.Name = editorWindow.Exercise.Name;
                selectedExercise.Category = editorWindow.Exercise.Category;
                selectedExercise.Description = editorWindow.Exercise.Description;

                ExercisesDataGrid.Items.Refresh();
                dataService.SaveExercises(Exercises);
            }
        }

        private void DeleteExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            Exercise? selectedExercise = ExercisesDataGrid.SelectedItem as Exercise;

            if (selectedExercise == null)
            {
                MessageBox.Show("Először válassz ki egy gyakorlatot.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Biztosan törölni szeretnéd ezt a gyakorlatot?\n\n{selectedExercise.Name}",
                "Megerősítés",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Exercises.Remove(selectedExercise);
                dataService.SaveExercises(Exercises);
            }
        }

        private void AddWorkout_Click(object sender, RoutedEventArgs e)
        {
            WorkoutWindow window = new WorkoutWindow(Exercises.ToList());
            window.Owner = this;

            bool? result = window.ShowDialog();

            if (result == true)
            {
                Workouts.Add(window.Workout);
                dataService.SaveWorkouts(Workouts);
            }
        }

        private void AddBodyWeightButton_Click(object sender, RoutedEventArgs e)
        {
            BodyWeightWindow window = new BodyWeightWindow();
            window.Owner = this;

            bool? result = window.ShowDialog();

            if (result == true)
            {
                BodyWeights.Add(window.BodyWeightEntry);
                dataService.SaveBodyWeights(BodyWeights);
            }
        }
    }
}