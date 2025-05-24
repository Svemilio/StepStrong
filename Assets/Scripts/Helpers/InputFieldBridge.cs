using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputFieldBridge : MonoBehaviour, IDeselectHandler
{
    public TextMeshProUGUI inputField;

    public void OnDeselect(BaseEventData eventData)
    {
        if (SceneManager.instance != null)
        {
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
