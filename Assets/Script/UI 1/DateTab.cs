using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DateTab : Radio
{
    public string displayFormat;
    public Text displayText;
    public DateTime memoDateTime;
    
    private void OnEnable()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
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
}
