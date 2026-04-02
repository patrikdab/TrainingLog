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

    public partial class ExerciseEditorWindow : Window
    {
        public Exercise Exercise { get; private set; }

        public ExerciseEditorWindow()
        {
            InitializeComponent();

            Exercise = new Exercise();
            DataContext = Exercise;
        }

        public ExerciseEditorWindow(Exercise exercise)
        {
            InitializeComponent();

            Exercise = new Exercise
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Category = exercise.Category,
                Description = exercise.Description
            };

            DataContext = Exercise;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Exercise.Name))
            {
                MessageBox.Show("A név megadása kötelező.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
