using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustrativeUI : MonoBehaviour
{
    public BookPro bookPro;
    public Transform itemIconButtonGridTrans;
    // 一页图标按钮最大数量
    public int maxDisplayItemButtonNum = 20;
    public Transform typeSelectButtonGridTrans;
    public Transform ABCSelectButtonGridTrans;

    
    private GameObject itemPanelPrefab;
    private GameObject simpleInfoPanelPrefab;
    private GameObject emptyPage;
    private GameObject itemIconButtonPrefab;
    private List<ItemButton> allItemIconButtons = new List<ItemButton>();
    private GameObject itemTypeButtonPrefab;

    private void Awake()
    {
        itemPanelPrefab = Resources.Load<GameObject>("Prefabs/ItemPanel");
        simpleInfoPanelPrefab = Resources.Load<GameObject>("Prefabs/SimpleInfoPanel");
        emptyPage = Resources.Load<GameObject>("Prefabs/EmptyPage");
        itemIconButtonPrefab = Resources.Load<GameObject>("Prefabs/IllustrativeItemIconButton");
        //itemTypeButtonPrefab = Resources.Load<GameObject>("Prefabs/TypeButton");
        itemTypeButtonPrefab = Resources.Load<GameObject>("Prefabs/TypeToggle");
        InitItemTypeButton();
        InitItemIconButton();
        ShowItemIcon();
    }

    //private void OnEnable()
    //{
    //    InitItemTypeButton();
    //    InitItemIconButton();
    //    ShowItemIcon();
    //}

    private void InitItemIconButton()
    {
        ItemInfo[] unlockedItemInfos = Illustrative.GetUnlockedItemInfos();
        if(itemIconButtonPrefab != null)
        {
            foreach(ItemInfo tempItemInfo in unlockedItemInfos)
            {
                GameObject itemIconButtonIns = Instantiate(itemIconButtonPrefab, itemIconButtonGridTrans);
                //GameObject itemIconButtonIns = Instantiate(itemIconButtonPrefab);
                ItemButton itemButton = itemIconButtonIns.GetComponent<ItemButton>();
                if(itemButton == null)
                {
                    itemButton = itemIconButtonIns.AddComponent<ItemButton>();
                }
                itemButton.itemInfo = tempItemInfo;
                itemButton.iconPath = tempItemInfo.itemIconPath;
                allItemIconButtons.Add(itemButton);
            }
        }
    }

    private void InitItemTypeButton()
    {
        string[] itemTypes = ItemInfoHelper.GroupByItemType();
        if (itemTypeButtonPrefab != null)
        {
            foreach (string itemType in itemTypes)
            {
                GameObject itemTypeButtonIns = Instantiate(itemTypeButtonPrefab, typeSelectButtonGridTrans);
                TypeSelectButton itemTypeMark = itemTypeButtonIns.GetComponent<TypeSelectButton>();
                if (itemTypeMark == null)
                {
                    itemTypeMark = itemTypeButtonIns.AddComponent<TypeSelectButton>();
                }
                itemTypeMark.itemType = itemType;
                itemTypeMark.iconPath = ItemInfoHelper.GetItemTypeIconPath(itemType);
                itemTypeMark.isChecked = false;
            }
        }
    }

    public void CheckItemType(Button clickedButton)
    {
        TypeSelectButton mark = clickedButton.GetComponent<TypeSelectButton>();
        if (mark != null)
        {
            mark.isChecked = !mark.isChecked;
            ShowItemIcon();
        }
    }

    public void ShowItemIcon()
    {
        //ItemButton[] itemButtons = itemIconButtonGridTrans.GetComponentsInChildren<ItemButton>(true);
        ItemButton[] itemButtons = allItemIconButtons.ToArray();
        TypeSelectButton[] typeSelects = typeSelectButtonGridTrans.GetComponentsInChildren<TypeSelectButton>();
        ABCSelectButton[] ABCSelects = ABCSelectButtonGridTrans.GetComponentsInChildren<ABCSelectButton>();

        List<string> checkedItemTypeNames = new List<string>(typeSelects.Length);
        foreach (TypeSelectButton tempTypeSelect in typeSelects)
        {
            if (tempTypeSelect.isChecked)
            {
                checkedItemTypeNames.Add(tempTypeSelect.itemType);
            }
        }

        List<string> checkedABCs = new List<string>(ABCSelects.Length);
        foreach(ABCSelectButton tempABCSelect in ABCSelects)
        {
            if (tempABCSelect.isChecked)
            {
                checkedABCs.Add(tempABCSelect.ABC);
            }
        }

        if (checkedItemTypeNames.Count <= 0 && checkedABCs.Count <= 0)
        {
            SetPages(itemButtons, maxDisplayItemButtonNum);
            SetAllGameObjectActive(itemButtons, true);
        }
        else
        {
            SetAllGameObjectActive(itemButtons, false);
            ItemInfo[] unlockedAndInSelectItemInfos = Illustrative.SelectUnlockedItemInfos(checkedItemTypeNames.ToArray(), checkedABCs.ToArray());

            ItemButton[] matchItemButtons = GetItemButtonMatchItemInfo(itemButtons, unlockedAndInSelectItemInfos);
            SetPages(matchItemButtons, maxDisplayItemButtonNum);
            SetAllGameObjectActive(matchItemButtons, true);
        }
    }

    /// <summary>
    /// 筛选符合条件的ItemIcon控件
    /// </summary>
    /// <param name="itemIcons">参与筛选ItemIcon控件</param>
    /// <param name="selectItemInfos">符合条件ItemInfo</param>
    /// <returns>符合条件的ItemIcon控件</returns>
    private ItemButton[] GetItemButtonMatchItemInfo(ItemButton[] itemIcons, ItemInfo[] selectItemInfos)
    {
        List<ItemButton> retItemButtons = new List<ItemButton>(itemIcons.Length);
        List<ItemInfo> itemInfos = new List<ItemInfo>(selectItemInfos);
        foreach (ItemButton tempItemButton in itemIcons)
        {
            ItemButton matchItemButton = null;
            foreach (ItemInfo tempItemInfo in itemInfos)
            {
                if (tempItemButton.itemInfo.itemID == tempItemInfo.itemID)
                {
                    matchItemButton = tempItemButton;
                    break;
                }
            }
            if (matchItemButton != null)
            {
                retItemButtons.Add(matchItemButton);
                itemInfos.Remove(matchItemButton.itemInfo);
            }
        }
        return retItemButtons.ToArray();
    }

    private void SetPages(ItemButton[] displayItemButtons, int maxDisplayItemButtonNum)
    {
        int maxPageNum = displayItemButtons.Length / maxDisplayItemButtonNum + 1;
        bookPro.StartFlippingPaper = 1;
        bookPro.EndFlippingPaper = maxPageNum - 1;
        //bookPro.EndFlippingPaper = Mathf.Max(maxPageNum - 2, bookPro.StartFlippingPaper);
        List<Paper> paperList = new List<Paper>(bookPro.papers);
        int loopCount = Mathf.Max(maxPageNum, paperList.Count);
        for(int i = 0; i < loopCount; i++)
        {
            if (i >= paperList.Count)
            {
                Paper newPaper = new Paper();
                newPaper.Back = Instantiate(itemPanelPrefab, bookPro.transform);
                //newPaper.Front = Instantiate(emptyPage, bookPro.transform);
                newPaper.Front = Instantiate(simpleInfoPanelPrefab, bookPro.transform);
                SetItemButton(displayItemButtons, i + 1, newPaper.Back);
                paperList.Add(newPaper);
            }
            else if (i < maxPageNum && i < paperList.Count)
            {
                SetItemButton(displayItemButtons, i + 1, paperList[i].Back);
            }
        }
        bookPro.papers = paperList.ToArray();
        bookPro.CurrentPaper = 1;
    }

    private void SetItemButton(ItemButton[] itemButtons, int pageNum, GameObject pageGameObject)
    {
        int startIndex = (pageNum - 1) * maxDisplayItemButtonNum;
        int endIndex = Mathf.Min(itemButtons.Length, pageNum * maxDisplayItemButtonNum);
        Transform gridTrans = pageGameObject.transform.Find("Grid");
        
        for(int i = startIndex; i < endIndex; i++)
        {
            itemButtons[i].transform.SetParent(gridTrans);
        }
    }

    /// <summary>
    /// 批量设定控件所在游戏对象的Active
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="components"></param>
    /// <param name="active"></param>
    public static void SetAllGameObjectActive<T>(T[] components, bool active) where T : Component
    {
        foreach (T component in components)
        {
            GameObject go = component.gameObject;
            if (go.activeSelf != active)
            {
                go.SetActive(active);
            }
        }
    }
}
