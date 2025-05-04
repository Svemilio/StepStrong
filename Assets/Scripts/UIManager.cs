using UnityEngine;

public class UIManager : MonoBehaviour
{
    private const int extraHeight = 930;

    [SerializeField]
    RectTransform contentExercises;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AddExerciseToScrollView(Transform inputField)
    {
        Instantiate(inputField, contentExercises.transform);
        Vector2 currentSize = contentExercises.sizeDelta;
        currentSize.y += extraHeight;
        contentExercises.sizeDelta = currentSize;
    }
}
