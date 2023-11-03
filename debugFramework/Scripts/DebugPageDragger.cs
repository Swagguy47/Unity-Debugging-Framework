using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DebugPageDragger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Transform pageRoot;
    bool dragging, alreadyDragging;
    Vector2 dragOffset;

    public void Update()
    {
        if (dragging)
        {
            alreadyDragging = true;
            pageRoot.position = new Vector2(Input.mousePosition.x + dragOffset.x, Input.mousePosition.y + dragOffset.y);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
        if (!alreadyDragging)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(DebugMenu.find.myCanvas.transform as RectTransform, Input.mousePosition, DebugMenu.find.myCanvas.worldCamera, out pos);
            dragOffset = new Vector2(pageRoot.localPosition.x, pageRoot.localPosition.y) - pos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
        alreadyDragging = false;
    }
}
