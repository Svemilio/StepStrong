using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private const int extraHeight = 930;
    private List<string> currentWorkoutName = new List<string>();
    private bool addDayWorkout = false;

    private int workoutIndex = 0;

    private float widthContetWorkout;

    [SerializeField]
    public List<RectTransform> contentExercises;
    [SerializeField]
    RectTransform contentWorkouts;
    [SerializeField]
    TextMeshProUGUI workoutName;
    [SerializeField]
    GameObject buttonAddDayWorkout;
    [SerializeField]
    GameObject scrollViewWorkout;

    public Canvas canvasWorkout;
    public Canvas canvasAddDayWorkout;

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentSize = contentWorkouts.sizeDelta;
        widthContetWorkout = currentSize.x;
    }

    public void SetWorkoutNameList(List<string> workoutsName)
    {
        currentWorkoutName = workoutsName;
    }

    public void AddExerciseToScrollView(Transform inputField)
    {
        if (!addDayWorkout)
        {
            addDayWorkout = true;
            ActiveButtonAddWorkout();
        }
        Instantiate(inputField, contentExercises[workoutIndex].transform);
        Vector2 currentSize = contentExercises[workoutIndex].sizeDelta;
        currentSize.y += extraHeight;
        contentExercises[workoutIndex].sizeDelta = currentSize;
    }

    public void UpdateWorkoutName(TextMeshProUGUI workoutName)
    {
        this.currentWorkoutName.Add(workoutName.text);
        this.workoutName.text = currentWorkoutName[0];
    }

    private void ActiveButtonAddWorkout()
    {
        buttonAddDayWorkout.SetActive(true);
    }

    public void ResetWorkoutCanvas()
    {
        addDayWorkout = false;
        foreach (Transform child in contentExercises[workoutIndex].transform)
        {
            Destroy(child.gameObject);
        }
        workoutName.text = "";
        buttonAddDayWorkout.SetActive(false);
    }

    public void ActiveCanvasWorkout()
    {
        canvasAddDayWorkout.transform.gameObject.SetActive(false);
        canvasWorkout.transform.gameObject.SetActive(true);
    }

    //instance a new scroll view vertical for the workout data
    public void AddWorkout()
    {
        Vector2 currentSize = contentWorkouts.sizeDelta;
        currentSize.x = currentSize.x * 2;
        contentWorkouts.sizeDelta = currentSize;

        GameObject instance = Instantiate(scrollViewWorkout, contentWorkouts.transform);

        RectTransform newRectTransform = instance.transform.Find("Viewport/Content").GetComponent<RectTransform>();
        
        contentExercises.Add(newRectTransform);

        canvasAddDayWorkout.gameObject.SetActive(true);
        canvasWorkout.gameObject.SetActive(false);
    }

    public void ScrollNextWorkout()
    {
        int checkIndex = workoutIndex + 1;
        //if click the button but there isn't any workout
        Debug.Log("checkIndex " + checkIndex + " -  currentWorkoutNameCount" + currentWorkoutName.Count);

        if (currentWorkoutName.Count <= checkIndex)
        {
            return;
        }
        Vector2 currentContentPos = contentWorkouts.GetComponent<RectTransform>().anchoredPosition;
        currentContentPos.x -= widthContetWorkout;

        contentWorkouts.GetComponent<RectTransform>().anchoredPosition = currentContentPos;
        this.workoutName.text = currentWorkoutName[++workoutIndex];
    }

    public void ScrollPreviousWorkout()
    {
        int checkIndex = workoutIndex - 1;
        //if click the button but there isn't any workout
        if (checkIndex < 0)
        {
            return;
        }

        Vector2 currentContentPos = contentWorkouts.GetComponent<RectTransform>().anchoredPosition;
        currentContentPos.x += widthContetWorkout;

        contentWorkouts.GetComponent<RectTransform>().anchoredPosition = currentContentPos;
        this.workoutName.text = currentWorkoutName[--workoutIndex];
    }

    public void GoToCurrentWorkoutPage()
    {
        for (int i = 0; i < currentWorkoutName.Count; i++)
        {
            ScrollNextWorkout();
        }
    }
}
