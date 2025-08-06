using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldCanvasHelper : MonoBehaviour
{
    [SerializeField]
    TMP_InputField[] inputFields;

    void OnEnable()
    {
        foreach (var inputField in inputFields)
        {
            inputField.text = "";
        }
    }
}
