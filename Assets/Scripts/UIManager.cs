using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private const int extraHeight = 930;
    private string currentWorkoutName = "";
    private bool addDayWorkout = false;

    private float widthContetWorkout;

    [SerializeField]
    public RectTransform contentExercises;
    [SerializeField]
    RectTransform contentWorkouts;
    [SerializeField]
    TextMeshProUGUI workoutName;
    [SerializeField]
    GameObject buttonAddDayWorkout;
    [SerializeField]
    GameObject scrollViewWorkout;

    public Canvas canvasWorkout;
    public Canvas canvasExercise;

    public static UIManager instance;

    void Awake()
    {
        if(instance == null)
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
    public void AddExerciseToScrollView(Transform inputField)
    {
        if(!addDayWorkout)
        {
            addDayWorkout = true;
            ActiveButtonAddWorkout();
        }
        Instantiate(inputField, contentExercises.transform);
        Vector2 currentSize = contentExercises.sizeDelta;
        currentSize.y += extraHeight;
        contentExercises.sizeDelta = currentSize;
    }

    public void UpdateWorkoutName(TextMeshProUGUI workoutName)
    {
        this.currentWorkoutName = workoutName.text;
        this.workoutName.text = currentWorkoutName;
    }

    private void ActiveButtonAddWorkout()
    {
        buttonAddDayWorkout.SetActive(true);
    }

    public void ResetWorkoutCanvas()
    {
        addDayWorkout = false;
        foreach (Transform child in contentExercises.transform)
        {
            Destroy(child.gameObject);
        }
        workoutName.text = "";
        buttonAddDayWorkout.SetActive(false);
    }

    public void ActiveCanvasWorkout()
    {
        canvasExercise.transform.gameObject.SetActive(false);
        canvasWorkout.transform.gameObject.SetActive(true);
    }

    public void AddWorkout()
    {
        Vector2 currentSize = contentWorkouts.sizeDelta;
        currentSize.x = currentSize.x * 2;
        contentWorkouts.sizeDelta = currentSize;

        Instantiate(scrollViewWorkout, contentWorkouts.transform);
    }

    public void ScrollNextWorkout()
    {
        Vector2 currentPos = contentWorkouts.GetComponent<RectTransform>().anchoredPosition;
        currentPos.x += widthContetWorkout;

        contentWorkouts.GetComponent<RectTransform>().anchoredPosition = currentPos;
    }
}
