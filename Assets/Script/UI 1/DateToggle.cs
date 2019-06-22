using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DateToggle : Toggle
{
    public string displayFormat = "MM月dd日";
    public Text displayText;
    public DateTime memoDateTime;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Init();
    }

    protected override void Start()
    {
        base.Start();
        Init();
    }

    private void Init()
    {
        if (displayText == null)
        {
            displayText = GetComponent<Text>();
            if (displayText == null)
            {
                displayText = transform.GetComponentInChildren<Text>();
            }
        }
        string displayStr = string.Empty;
        if (string.IsNullOrEmpty(displayFormat))
        {
            displayStr = memoDateTime.ToString("MM.dd");
        }
        else
        {
            displayStr = memoDateTime.ToString(displayFormat);
        }
        displayText.text = displayStr;
    }

    //public override void OnSelect(BaseEventData eventData)
    //{
    //    base.OnSelect(eventData);
    //    group.
    //}
}
