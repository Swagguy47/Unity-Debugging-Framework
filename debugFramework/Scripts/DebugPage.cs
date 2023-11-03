using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DebugPage : MonoBehaviour
{
    public TextMeshProUGUI pageHeader;
    [HideInInspector] public bool activePage;
    [SerializeField] GameObject selectionIco;
    int selectedElement = 0;
    [HideInInspector] public List<DebugElement> elements = new List<DebugElement>();

    public void RemovePage()
    {
        DebugMenu menu = transform.parent.gameObject.GetComponent<DebugMenu>();
        menu.pageDepth.Remove(this); 
        menu.FindActivePage();
        Destroy(gameObject);
    }

    private void Update()
    {
        selectionIco.SetActive(activePage);
        if (activePage) {
            selectionIco.transform.localPosition = new Vector3(-400, -35 - (60 * selectedElement), 0);
            if (Input.GetKeyDown(KeyCode.Escape)) {
                RemovePage();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && selectedElement != elements.Count -1)
            {
                selectedElement++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && selectedElement != 0)
            {
                selectedElement--;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (elements[selectedElement].elementType)
                {
                    case DevMenuObj.DevElement.Button: elements[selectedElement].TriggerEvent(); break;
                    case DevMenuObj.DevElement.FloatInput: elements[selectedElement].InputEvent(elements[selectedElement].inputField.text); break;
                    case DevMenuObj.DevElement.Subpage: elements[selectedElement].EnterSubpage(false); break;
                }
            }

            if (elements[selectedElement].elementType == DevMenuObj.DevElement.FloatInput)
            {
                elements[selectedElement].inputField.Select();
            }
        }
    }
}
