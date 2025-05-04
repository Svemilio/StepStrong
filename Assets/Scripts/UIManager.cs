using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private const int extraHeight = 930;
    private string currentWorkoutName = "";
    private bool addDayWorkout = false;

    [SerializeField]
    RectTransform contentExercises;
    [SerializeField]
    TextMeshProUGUI workoutName;
    [SerializeField]
    GameObject buttonAddDayWorkout;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
