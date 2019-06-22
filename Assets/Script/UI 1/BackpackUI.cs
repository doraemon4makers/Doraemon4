using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BackpackUI : MonoBehaviour
{
    //public RadioGroup dateTabGroup;
    public string dateTabGroupName;
    public Transform itemIconButtonGridTrans;
    public Transform dateTabGridTrans;
    public ToggleGroup dateToggleGroup;

    private GameObject[] currentItemIconButtonGOs;
    private GameObject[] currentDateTabGOs;
    private GameObject[] currentDateToggleGOs;
    private GameObject itemIconButtonPrefab;
    private GameObject dateTabPrefab;
    private GameObject dateTogglePrefab;
    private DateTime currentDisplayDateTime = DateTime.MinValue;

    private void Awake()
    {
        itemIconButtonPrefab = Resources.Load<GameObject>("Prefabs/BackpackItemIconButton");
        dateTabPrefab = Resources.Load<GameObject>("Prefabs/DateTab");
        dateTogglePrefab = Resources.Load<GameObject>("Prefabs/DateToggle");
    }

    private void OnEnable()
    {
        //LoadDateTab();
        LoadDateToggle();
        //LoadItemIconButton();
    }

    /// <summary>
    /// 加载日期Tab
    /// </summary>
    public void LoadDateTab()
    {
        DateTime[] dateTimes = Backpack.GetSortKeys();
        if (dateTimes == null || dateTimes.Length <= 0) return;
        LoadButtons(ref currentDateTabGOs, dateTimes, CreateDateTab, ResetDateTab);

        if(currentDisplayDateTime == DateTime.MinValue)
        {
            currentDateTabGOs[0].GetComponent<DateTab>().OnClick();
        }
        else
        {
            foreach (GameObject tempDateTabGO in currentDateTabGOs)
            {
                DateTab dateTab = tempDateTabGO.GetComponent<DateTab>();
                if (dateTab.memoDateTime == currentDisplayDateTime)
                {
                    dateTab.OnClick();
                    break;
                }
            }
        }
    }

    public void LoadDateToggle()
    {
        DateTime[] dateTimes = Backpack.GetSortKeys();
        if (dateTimes == null || dateTimes.Length <= 0) return;
        LoadButtons(ref currentDateToggleGOs, dateTimes, CreateDateToggle, ResetDateToggle);

        if (currentDisplayDateTime == DateTime.MinValue)
        {
            currentDateToggleGOs[0].GetComponent<DateToggle>().OnPointerClick(new PointerEventData(EventSystem.current));
        }
        else
        {
            foreach (GameObject tempDateToggleGO in currentDateToggleGOs)
            {
                DateToggle dateToggle = tempDateToggleGO.GetComponent<DateToggle>();
                if (dateToggle.memoDateTime == currentDisplayDateTime)
                {
                    dateToggle.OnPointerClick(new PointerEventData(EventSystem.current));
                    break;
                }
                else
                {
                    dateToggle.isOn = false;
                }
            }
        }
    }

    /// <summary>
    /// 加载按钮, 仅适合单一初始值的按钮
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="currentGOs">当前按钮数组</param>
    /// <param name="initValues">初始值数组</param>
    /// <param name="CreateButton">创建新按钮方法</param>
    /// <param name="ResetButton">重置旧按钮方法</param>
    private void LoadButtons<T>(ref GameObject[] currentGOs, T[] initValues, Func<T, GameObject> CreateButton, Action<GameObject, T> ResetButton)
    {
        if (initValues == null || initValues.Length <= 0) return;
        int currentGOsLength = currentGOs == null ? 0 : currentGOs.Length;
        int initValuesLength = initValues.Length;

        if (currentGOs == null || currentGOsLength <= 0)
        {
            currentGOs = new GameObject[initValuesLength];
            for (int i = 0; i < initValuesLength; i++)
            {
                currentGOs[i] = CreateButton(initValues[i]);
            }
        }
        else if (currentGOsLength < initValuesLength)
        {
            List<GameObject> tempGOList = new List<GameObject>(currentGOs);
            for (int i = 0; i < tempGOList.Count; i++)
            {
                ResetButton(tempGOList[i], initValues[i]);
            }

            for (int i = currentGOsLength; i < initValuesLength; i++)
            {
                tempGOList.Add(CreateButton(initValues[i]));
            }
            currentGOs = tempGOList.ToArray();
        }
        else
        {
            for (int i = 0; i < initValuesLength; i++)
            {
                ResetButton(currentGOs[i], initValues[i]);
            }
            for (int i = initValuesLength; i < currentGOsLength; i++)
            {
                GameObject tempGO = currentGOs[i];
                if (tempGO.activeSelf)
                {
                    tempGO.SetActive(false);
                }
            }
        }
    }

    private GameObject CreateDateTab(DateTime dateTime)
    {
        GameObject ins = Instantiate(dateTabPrefab, dateTabGridTrans);
        DateTab dateTab = ReturnComponent<DateTab>(ins);
        dateTab.groupName = dateTabGroupName;
        dateTab.memoDateTime = dateTime;
        dateTab.ClickEvent += LoadItemIconButton;
        dateTab.ClickEvent += MarkDisplayDateTime;
        dateTab.Reload();
        return ins;
    }

    private void ResetDateTab(GameObject dateTabGO, DateTime dateTime)
    {
        DateTab dateTab = ReturnComponent<DateTab>(dateTabGO);
        dateTab.memoDateTime = dateTime;
        dateTab.isCheck = false;
        if (!dateTabGO.activeSelf)
        {
            dateTabGO.SetActive(true);
        }
        dateTab.Reload();
    }

    private GameObject CreateDateToggle(DateTime dateTime)
    {
        GameObject ins = Instantiate(dateTogglePrefab, dateTabGridTrans);
        DateToggle dateToggle = ReturnComponent<DateToggle>(ins);
        dateToggle.group = dateToggleGroup;
        dateToggle.memoDateTime = dateTime;
        dateToggle.onValueChanged.AddListener((bool isOn) =>
        {
            //if (!isOn)
            //{
            //    MarkDisplayDateTime();
            //    LoadItemIconButton();
            //}
            MarkDisplayDateTime();
            LoadItemIconButton();
        });
        return ins;
    }

    private void ResetDateToggle(GameObject dateTabGO, DateTime dateTime)
    {
        DateToggle dateToggle = ReturnComponent<DateToggle>(dateTabGO);
        dateToggle.memoDateTime = dateTime;
        if (!dateTabGO.activeSelf)
        {
            dateTabGO.SetActive(true);
        }
    }

    public void LoadItemIconButton()
    {
        //Radio check = RadioGroup.GetCheck(dateTabGroupName);
        //if (check == null) return;

        //DateTime selectedDateTime = ((DateTab)check).memoDateTime;
        //ItemInfo[] itemInfos = Backpack.GetItemInfos(selectedDateTime);
        //if (itemInfos == null || itemInfos.Length <= 0) return;
        //LoadButtons(ref currentItemIconButtonGOs, itemInfos, CreateItemInfoButton, ResetItemInfoButton;

        IEnumerable<Toggle> dateToggles = dateToggleGroup.ActiveToggles();
        foreach(Toggle tempToggle in dateToggles)
        {
            DateTime selectedDateTime = (tempToggle as DateToggle).memoDateTime;
            ItemInfo[] itemInfos = Backpack.GetItemInfos(selectedDateTime);
            if (itemInfos == null || itemInfos.Length <= 0) return;
            LoadButtons(ref currentItemIconButtonGOs, itemInfos, CreateItemInfoButton, ResetItemInfoButton);
        }

    }

    private GameObject CreateItemInfoButton(ItemInfo itemInfo)
    {
        GameObject ins = Instantiate(itemIconButtonPrefab, itemIconButtonGridTrans);
        ItemButton itemButton = ReturnComponent<ItemButton>(ins);
        itemButton.itemInfo = itemInfo;
        //itemButton.Reload();
        return ins;
    }

    private void ResetItemInfoButton(GameObject itemInfoButton, ItemInfo itemInfo)
    {
        ItemButton itemButton = ReturnComponent<ItemButton>(itemInfoButton);
        itemButton.itemInfo = itemInfo;
        if (!itemInfoButton.activeSelf)
        {
            itemInfoButton.SetActive(true);
        }
        //itemButton.Reload();
    }

    private T ReturnComponent<T>(GameObject ins) where T : Component
    {
        T resultComponent = ins.GetComponent<T>();
        if (resultComponent == null)
        {
            resultComponent = ins.AddComponent<T>();
        }
        return resultComponent;
    }

    private void MarkDisplayDateTime()
    {
        //Radio check = RadioGroup.GetCheck(dateTabGroupName);
        //if(check != null)
        //{
        //    currentDisplayDateTime = (check as DateTab).memoDateTime;
        //}
        IEnumerable<Toggle> activeToggles = dateToggleGroup.ActiveToggles();
        foreach (Toggle tempToggle in activeToggles)
        {
            currentDisplayDateTime = (tempToggle as DateToggle).memoDateTime;
        }

    }
}
