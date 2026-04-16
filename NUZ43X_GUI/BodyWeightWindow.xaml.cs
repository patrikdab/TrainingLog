using NUZ43X_GUI.Models;
using System.Windows;
using System.Windows.Controls;

namespace NUZ43X_GUI
{
  
    public partial class BodyWeightWindow : Window
    {
        public BodyWeightEntry BodyWeightEntry { get; private set; }

        public BodyWeightWindow()
        {
            InitializeComponent();

            BodyWeightEntry = new BodyWeightEntry();
            DatePicker.SelectedDate = DateTime.Now;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("A dátum megadása kötelező.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(WeightTextBox.Text, out double weight) || weight <= 0)
            {
                MessageBox.Show("Adj meg egy érvényes testsúlyt.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            BodyWeightEntry.Date = DatePicker.SelectedDate.Value;
            BodyWeightEntry.Weight = weight;
            

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
