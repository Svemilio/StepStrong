using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
class Exercise
{
    public string ExecutionName;
    public string ExecutionDescription;
}

[Serializable]
struct Workout
{
    public string WorkoutName;
    public List<Exercise> Exercises;
}

//Una lista di workout mi permetterà di salvare tutti i dati che mi servono 
public class SceneManager : MonoBehaviour
{
    private List<Workout> workouts = new List<Workout>();
    private UIManager uIManager;
    public static SceneManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        uIManager = UIManager.instance;
        LoadWorkouts();
    }

    public void AddWorkout(TextMeshProUGUI workoutName)
    {
        foreach( var w in workouts)
        {
            if(w.WorkoutName == workoutName.text)
            {
                return;
            }
        }
        Workout newWorkout = new Workout { WorkoutName = workoutName.text };
        workouts.Add(newWorkout);
        SaveWorkouts();
    }

    public void AddExerciseToWorkout(string workoutName, string exerciseName, string description)
    {
        Exercise newExercise = new Exercise
        {
            ExecutionName = exerciseName,
            ExecutionDescription = description
        };

        foreach( var w in workouts)
        {
            if(w.WorkoutName.Equals(workoutName))
            {
                w.Exercises.Add(newExercise);
                break;
            }
        }
        SaveWorkouts();
    }

    public void GoToWorkoutPage(TextMeshProUGUI workoutName)
    {
        uIManager.ActiveCanvasWorkout();
        uIManager.UpdateWorkoutName(workoutName);
        AddWorkout(workoutName);
    }

    private void SaveWorkouts()
    {
        string json = JsonUtility.ToJson(new WorkoutListWrapper { Workouts = workouts });
        PlayerPrefs.SetString("WorkoutsData", json);
        PlayerPrefs.Save();
    }

    private void LoadWorkouts()
    {
        if (PlayerPrefs.HasKey("WorkoutsData"))
        {
            string json = PlayerPrefs.GetString("WorkoutsData");
            workouts = JsonUtility.FromJson<WorkoutListWrapper>(json).Workouts;
        }

        if(workouts != null)
        {
            
        }
    }

    // Wrapper per serializzare liste
    [System.Serializable]
    private class WorkoutListWrapper
    {
        public List<Workout> Workouts;
    }
}