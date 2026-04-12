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
    
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(List<Workout> workouts, List<Exercise> exercises)
        {
            InitializeComponent();

            int workoutCount = workouts.Count;
            int totalSets = workouts
                .SelectMany(w => w.Entries)
                .Sum(e => e.Sets.Count);

            int totalRepetitions = workouts
                .SelectMany(w => w.Entries)
                .SelectMany(e => e.Sets)
                .Sum(s => s.Repetitions);

            double totalVolume = workouts
                .SelectMany(w => w.Entries)
                .SelectMany(e => e.Sets)
                .Sum(s => s.Weight * s.Repetitions);

            int exerciseCount = exercises.Count;

            WorkoutCountTextBlock.Text = workoutCount.ToString();
            TotalSetsTextBlock.Text = totalSets.ToString();
            TotalRepetitionsTextBlock.Text = totalRepetitions.ToString();
            TotalVolumeTextBlock.Text = $"{totalVolume:F2} kg";
            ExerciseCountTextBlock.Text = exerciseCount.ToString();

            List<ExerciseStatistics> statistics = workouts
                .SelectMany(w => w.Entries)
                .GroupBy(e => e.ExerciseName)
                .Select(g => new ExerciseStatistics
                {
                    ExerciseName = g.Key,
                    WorkoutOccurrences = g.Count(),
                    TotalSets = g.Sum(e => e.Sets.Count),
                    TotalRepetitions = g.SelectMany(e => e.Sets).Sum(s => s.Repetitions),
                    MaxWeight = g.SelectMany(e => e.Sets)
                                 .Select(s => s.Weight)
                                 .DefaultIfEmpty(0)
                                 .Max(),
                    TotalVolume = g.SelectMany(e => e.Sets)
                                   .Sum(s => s.Weight * s.Repetitions),
                    BestEstimatedOneRm = g.SelectMany(e => e.Sets)
                                          .Select(s => s.Weight * (1 + s.Repetitions / 30.0))
                                          .DefaultIfEmpty(0)
                                          .Max()
                })
                .OrderBy(s => s.ExerciseName)
                .ToList();

            StatisticsDataGrid.ItemsSource = statistics;
        }
    }
}
