using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustrativeUI : MonoBehaviour
{
    public Transform itemIconButtonGridTrans;
    public Transform typeSelectButtonGridTrans;
    public Transform ABCSelectButtonGridTrans;

    private GameObject itemIconButtonPrefab;
    private GameObject itemTypeButtonPrefab;

    private void Awake()
    {
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
                ItemButton itemButton = itemIconButtonIns.GetComponent<ItemButton>();
                if(itemButton == null)
                {
                    itemButton = itemIconButtonIns.AddComponent<ItemButton>();
                }
                itemButton.itemInfo = tempItemInfo;
                itemButton.iconPath = tempItemInfo.itemIconPath;
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
        ItemButton[] itemButtons = itemIconButtonGridTrans.GetComponentsInChildren<ItemButton>(true);
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
            SetAllGameObjectActive(itemButtons, true);
        }
        else
        {
            SetAllGameObjectActive(itemButtons, false);
            ItemInfo[] unlockedAndInSelectItemInfos = Illustrative.SelectUnlockedItemInfos(checkedItemTypeNames.ToArray(), checkedABCs.ToArray());
            SetAllGameObjectActive(GetItemIconsMatchItemInfo(itemButtons, unlockedAndInSelectItemInfos), true);
        }
    }

    /// <summary>
    /// 筛选符合条件的ItemIcon控件
    /// </summary>
    /// <param name="itemIcons">参与筛选ItemIcon控件</param>
    /// <param name="selectItemInfos">符合条件ItemInfo</param>
    /// <returns>符合条件的ItemIcon控件</returns>
    private ItemButton[] GetItemIconsMatchItemInfo(ItemButton[] itemIcons, ItemInfo[] selectItemInfos)
    {
        List<ItemButton> retItemIcons = new List<ItemButton>(itemIcons.Length);
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
                retItemIcons.Add(matchItemButton);
                itemInfos.Remove(matchItemButton.itemInfo);
            }
        }
        return retItemIcons.ToArray();
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
