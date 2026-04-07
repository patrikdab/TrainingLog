using NUZ43X_GUI.Models;
using NUZ43X_GUI.Models.TrainingLog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NUZ43X_GUI.Services
{
    public class JsonDataService
    {
        private readonly string dataFolderPath;
        private readonly string exercisesFilePath;
        private readonly string workoutsFilePath;
        private readonly string bodyWeightsFilePath;
        private readonly JsonSerializerOptions jsonOptions;

        public JsonDataService()
        {
            dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            exercisesFilePath = Path.Combine(dataFolderPath, "exercises.json");
            workoutsFilePath = Path.Combine(dataFolderPath, "workouts.json");
            bodyWeightsFilePath = Path.Combine(dataFolderPath, "bodyweights.json");

            jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }
        }

        public ObservableCollection<Exercise> LoadExercises()
        {
            if (!File.Exists(exercisesFilePath))
            {
                return new ObservableCollection<Exercise>();
            }

            string json = File.ReadAllText(exercisesFilePath);
            ObservableCollection<Exercise>? exercises = JsonSerializer.Deserialize<ObservableCollection<Exercise>>(json, jsonOptions);

            return exercises ?? new ObservableCollection<Exercise>();
        }

        public ObservableCollection<Workout> LoadWorkouts()
        {
            if (!File.Exists(workoutsFilePath))
            {
                return new ObservableCollection<Workout>();
            }

            string json = File.ReadAllText(workoutsFilePath);
            ObservableCollection<Workout>? workouts = JsonSerializer.Deserialize<ObservableCollection<Workout>>(json, jsonOptions);

            return workouts ?? new ObservableCollection<Workout>();
        }

        public ObservableCollection<BodyWeightEntry> LoadBodyWeights()
        {
            if (!File.Exists(bodyWeightsFilePath))
            {
                return new ObservableCollection<BodyWeightEntry>();
            }

            string json = File.ReadAllText(bodyWeightsFilePath);
            ObservableCollection<BodyWeightEntry>? bodyWeights = JsonSerializer.Deserialize<ObservableCollection<BodyWeightEntry>>(json, jsonOptions);

            return bodyWeights ?? new ObservableCollection<BodyWeightEntry>();
        }

        public void SaveExercises(ObservableCollection<Exercise> exercises)
        {
            string json = JsonSerializer.Serialize(exercises, jsonOptions);
            File.WriteAllText(exercisesFilePath, json);
        }

        public void SaveWorkouts(ObservableCollection<Workout> workouts)
        {
            string json = JsonSerializer.Serialize(workouts, jsonOptions);
            File.WriteAllText(workoutsFilePath, json);
        }

        public void SaveBodyWeights(ObservableCollection<BodyWeightEntry> bodyWeights)
        {
            string json = JsonSerializer.Serialize(bodyWeights, jsonOptions);
            File.WriteAllText(bodyWeightsFilePath, json);
        }
    }

}
