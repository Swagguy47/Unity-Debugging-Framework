using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour
{
    public Canvas myCanvas;
    bool isActive = false;
    [SerializeField] DevMenuObj homePage;
    [SerializeField] GameObject pageBg, button, subpage, floatinput;
    public List<UnityEvent<float>> DebugEvents;
    [SerializeField] float uiScale = 1;

    [SerializeField] GameObject e_fullbright;

    [HideInInspector] public List<DebugPage> pageDepth = new List<DebugPage>();
    [HideInInspector]public int depthOffset = 0;
    DevMenuObj currentPage;

    public static DebugMenu find;

    List<DevMenuObj.EventButton> itemParseElements = new List<DevMenuObj.EventButton>(), enemyParseElements = new List<DevMenuObj.EventButton>();

    private void Awake()
    {
        find = this;
        if (Application.isEditor || Debug.isDebugBuild)
        { 
        }else{
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isActive = !isActive;
            ToggleMenu();
        }

        if (isActive && transform.childCount == 0)
        {
            isActive = false;
            ToggleMenu();
        }

        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Mathf.Abs(depthOffset) != pageDepth.Count - 1)
            {
                depthOffset--;
                FindActivePage();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && Mathf.Abs(depthOffset) != 0)
            {
                depthOffset++;
                FindActivePage();
            }
        }

        /*Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);

        Debug.Log(pos);*/
    }

    void ToggleMenu()
    {
        depthOffset = 0;
        if (isActive) {
            ShowDebugPage(homePage, false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            pageDepth.Clear();
        }
    }

    public void FindActivePage()
    {
        if (Mathf.Abs(depthOffset) > pageDepth.Count - 1)
        {
            depthOffset = 0;
        }

        for (int i = 0; i <= pageDepth.Count - 1; i++)
        {
            //Debug.Log(pageDepth.Count - 1 + depthOffset);
            bool active = i == pageDepth.Count - 1 + depthOffset;
            pageDepth[i].activePage = active;
        }
    }

    public void ShowDebugPage(DevMenuObj page, bool placeOnCursor)
    {
        currentPage = page; //for pre-opening event edits
        if (page.openEvent != -1) {
            DebugEvents[page.openEvent].Invoke(0);
        }

        depthOffset = 0;
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        //transform.position = myCanvas.transform.TransformPoint(pos);

        if (!placeOnCursor) { pos = new Vector2(-500 + (15 * pageDepth.Count -1), 300 - (15 * pageDepth.Count - 1)); } 

        Transform bg = Instantiate(pageBg, myCanvas.transform.TransformPoint(pos), Quaternion.identity, transform).transform;
        DebugPage bgpg = bg.gameObject.GetComponent<DebugPage>();
        bgpg.pageHeader.text = page.PageName;
        pageDepth.Add(bgpg);
        FindActivePage();

        bg.transform.localScale = Vector3.one * uiScale;
        
        bg = bg.GetChild(0);
        bg.localScale = new Vector3(1, page.elements.Length, 1);


        for (int i = 0; i < page.elements.Length; i++)
        {
            GameObject element = null;
            switch (page.elements[i].elementType)
            {
                case DevMenuObj.DevElement.Button: element = Instantiate(button, bg.parent); break;
                case DevMenuObj.DevElement.Subpage: element = Instantiate(subpage, bg.parent); break;
                case DevMenuObj.DevElement.FloatInput: element = Instantiate(floatinput, bg.parent); break;
            }

            element.transform.localPosition = new Vector3(0, -35 - (60 * (i + 0)), 0);
            DebugElement elementComponent = element.GetComponent<DebugElement>();

            elementComponent.Event = DebugEvents[page.elements[i].eventIndex];
            elementComponent.buttonText.text = page.elements[i].text;
            elementComponent.subPage = page.elements[i].subpage;
            elementComponent.elementType = page.elements[i].elementType;
            elementComponent.eventVariable = page.elements[i].eventVariable;
            bgpg.elements.Add(elementComponent);
        }
    }

    public void Event_Fullbright()
    {
        e_fullbright.SetActive(!e_fullbright.activeSelf);
    }

    public void Event_UiRescale(float f)
    {
        uiScale = Mathf.Clamp(f, 0.333f, 2f);
        foreach (Transform child in transform)
        {
            child.localScale = Vector3.one * uiScale;
        }
    }

    public void Event_Timescale(float f)
    {
        Time.timeScale = f;
    }

    public void Event_Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
