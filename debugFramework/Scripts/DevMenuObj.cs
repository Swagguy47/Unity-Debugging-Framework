using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DevMenu", menuName = "JCCC/DevMenu")]
public class DevMenuObj : ScriptableObject
{
    public string PageName;
    public int openEvent = -1;
    public EventButton[] elements;

    [Serializable]
    public enum DevElement
    {
        Button, Subpage, FloatInput
    }

    [Serializable]
    public struct EventButton
    {
        public string text;
        public int eventIndex;
        public float eventVariable;
        public DevMenuObj subpage;
        public DevElement elementType;
    }
}
