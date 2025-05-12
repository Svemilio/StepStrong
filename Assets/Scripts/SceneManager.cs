using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private string currentExerciseName;
    private string currentExerciseDescription;

    private string currentWorkoutName;
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
        currentWorkoutName = workoutName.text;
        SaveWorkouts();
    }

    public void AddExerciseToWorkout()
    {
        Debug.Log("workout = " + currentWorkoutName + "Exercise = " + currentExerciseName + "Desc = " + currentExerciseDescription);
        Exercise newExercise = new Exercise
        {
            ExecutionName = currentExerciseName,
            ExecutionDescription = currentExerciseDescription
        };

        foreach( var w in workouts)
        {
            if(w.WorkoutName.Equals(currentExerciseName))
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
        Debug.Log("Elementi salvati = " + json);
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

    public void SetCurrentExerciseName(TextMeshProUGUI exerciseName)
    {
        currentExerciseName = exerciseName.text;
        Debug.Log("Name = " + currentExerciseName);
    }

    public void SetCurrentExerciseDescription(TextMeshProUGUI exerciseDescription)
    {
        currentExerciseDescription = exerciseDescription.text;
        Debug.Log("Desc = " + currentExerciseDescription);
    }
    // Wrapper per serializzare liste
    [System.Serializable]
    private class WorkoutListWrapper
    {
        public List<Workout> Workouts;
    }
}