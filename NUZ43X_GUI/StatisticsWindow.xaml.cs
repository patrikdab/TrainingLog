using NUZ43X_GUI.Models;
using NUZ43X_GUI.Models.TrainingLog.Models;
using System.Windows;

namespace NUZ43X_GUI
{

    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(List<Workout> workouts, List<Exercise> exercises, List<BodyWeightEntry> bodyWeights)
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

            int bodyWeightCount = bodyWeights.Count;
            BodyWeightCountTextBlock.Text = bodyWeightCount.ToString();

            if (bodyWeights.Count > 0)
            {
                BodyWeightEntry latestBodyWeight = bodyWeights
                    .OrderByDescending(bw => bw.Date)
                    .First();

                double minBodyWeight = bodyWeights.Min(bw => bw.Weight);
                double maxBodyWeight = bodyWeights.Max(bw => bw.Weight);
                double averageBodyWeight = bodyWeights.Average(bw => bw.Weight);

                CurrentBodyWeightTextBlock.Text = $"{latestBodyWeight.Weight:F1} kg";
                MinBodyWeightTextBlock.Text = $"{minBodyWeight:F1} kg";
                MaxBodyWeightTextBlock.Text = $"{maxBodyWeight:F1} kg";
                AverageBodyWeightTextBlock.Text = $"{averageBodyWeight:F1} kg";
            }
            else
            {
                CurrentBodyWeightTextBlock.Text = "-";
                MinBodyWeightTextBlock.Text = "-";
                MaxBodyWeightTextBlock.Text = "-";
                AverageBodyWeightTextBlock.Text = "-";
            }

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

