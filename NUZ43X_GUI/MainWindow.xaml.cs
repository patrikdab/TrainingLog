using NUZ43X_GUI.Models.TrainingLog.Models;
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
        private ObservableCollection<Exercise> exercises;

        public MainWindow()
        {
            InitializeComponent();

            exercises = new ObservableCollection<Exercise>();
            DataContext = exercises;
        }

        private void AddExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            ExerciseEditorWindow editorWindow = new ExerciseEditorWindow();
            editorWindow.Owner = this;

            bool? result = editorWindow.ShowDialog();

            if (result == true)
            {
                exercises.Add(editorWindow.Exercise);
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
                exercises.Remove(selectedExercise);
            }
        }
    }
}