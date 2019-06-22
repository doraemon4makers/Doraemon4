using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public delegate void OnClickHandler();
    public event OnClickHandler ClickEvent;

    public string groupName;
    public bool isCheck = false;

    private void Awake()
    {
        ClickEvent += Click;
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (!string.IsNullOrEmpty(groupName))
        {
            RadioGroup.Join(groupName, this);
            if (isCheck)
            {
                RadioGroup.SetCheck(groupName, this);
            }
        }
    }

    public void Reload()
    {
        Init();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>下层是否继续执行</returns>
    public void OnClick()
    {
        if (!isCheck && ClickEvent != null)
        {
            ClickEvent();
        }
    }

    private void Click()
    {
        isCheck = true;
        RadioGroup.SetCheck(groupName, this);
    }
}
