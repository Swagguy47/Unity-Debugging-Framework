using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DebugElement : MonoBehaviour
{
    public UnityEvent<float> Event;
    public TextMeshProUGUI buttonText;
    public TMP_InputField inputField;
    public DevMenuObj subPage;
    [HideInInspector] public DevMenuObj.DevElement elementType;
    [HideInInspector] public float eventVariable;

    public void TriggerEvent()
    {
        Event.Invoke(eventVariable);
    }

    public void InputEvent(string input)
    {
        if (input != null && input != "")
        {
            Debug.Log("Inputted: " + input);
            Event.Invoke(float.Parse(input));
        }
    }

    public void EnterSubpage(bool pressed)
    {
        DebugMenu.find.ShowDebugPage(subPage, pressed);
    }
}
