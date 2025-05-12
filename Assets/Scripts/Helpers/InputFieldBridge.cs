using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputFieldBridge : MonoBehaviour, IDeselectHandler
{
    public TextMeshProUGUI inputField;

    void Awake()
    {
        //inputField = GetComponent<TextMeshProUGUI>(); // o InputField
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (SceneManager.instance != null)
        {
            Debug.Log("NOME OGGETTO = " + this.gameObject.name);
            if(this.gameObject.name == "InputFieldName")
            {
                SceneManager.instance.SetCurrentExerciseName(inputField);
            }
            else
            {
                SceneManager.instance.SetCurrentExerciseDescription(inputField);
            }
            
        }
    }
}
