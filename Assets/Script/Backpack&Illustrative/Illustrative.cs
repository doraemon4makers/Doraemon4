using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Illustrative
{
    //记录已解锁的道具文本路径
    private const string UNLOCKED_ITEM_MEMO_DIRECTORY = "/UnlockedItemMemo";
    private const string UNLOCKED_ITEM_MEMO_FILENAME = "UnlockedItem.txt";

    private static string[] GetUnlockedItemNames()
    {
        string[] retStrArray = null;
        // 文本的完整路径
        string fullPath = Application.persistentDataPath + UNLOCKED_ITEM_MEMO_DIRECTORY + "/" + UNLOCKED_ITEM_MEMO_FILENAME;
        if (File.Exists(fullPath))
        {
            retStrArray = File.ReadAllLines(fullPath);
        }
        return retStrArray;
    }

    /// <summary>
    /// 返回所有已解锁的道具信息
    /// </summary>
    /// <returns>已解锁的道具信息数组</returns>
    public static ItemInfo[] GetUnlockedItemInfos()
    {
        string[] unlockedItemNames = GetUnlockedItemNames();
        if (unlockedItemNames == null) return null;

        List<ItemInfo> retList = new List<ItemInfo>(unlockedItemNames.Length);
        Dictionary<string, ItemInfo> typeNameItemInfoPairs = ItemInfoHelper.TypeNameItemInfoPairs;
        foreach (string tempName in unlockedItemNames)
        {
            if (typeNameItemInfoPairs.ContainsKey(tempName))
            {
                retList.Add(typeNameItemInfoPairs[tempName]);
            }
        }
        return retList.ToArray();
    }

    /// <summary>
    /// 返回所有与参数匹配的道具信息
    /// </summary>
    /// <param name="itemTypes">道具类型</param>
    /// <returns>所有与参数匹配的道具信息</returns>
    public static ItemInfo[] SelectUnlockedItemInfosByItemType(params string[] itemTypes)
    {
        ItemInfo[] allUnlockedItemInfos = GetUnlockedItemInfos();
        if (allUnlockedItemInfos == null || allUnlockedItemInfos.Length <= 0) return null;
        if (itemTypes.Length <= 0) return allUnlockedItemInfos;

        List<ItemInfo> resultList = new List<ItemInfo>(allUnlockedItemInfos.Length);
        foreach (ItemInfo tempItemInfo in allUnlockedItemInfos)
        {
            foreach (string itemType in itemTypes)
            {
                if (tempItemInfo.itemType.Equals(itemType))
                {
                    resultList.Add(tempItemInfo);
                    break;
                }
            }
        }
        return resultList.ToArray();
    }

    /// <summary>
    /// 返回所有与参数匹配的道具信息
    /// </summary>
    /// <param name="ABCs">道具英文名首字母</param>
    /// <returns>所有与参数匹配的道具信息</returns>
    public static ItemInfo[] SelectUnlockedItemInfosByABC(params string[] ABCs)
    {
        ItemInfo[] allUnlockedItemInfos = GetUnlockedItemInfos();
        if (allUnlockedItemInfos == null || allUnlockedItemInfos.Length <= 0) return null;
        if (ABCs.Length <= 0) return allUnlockedItemInfos;

        List<ItemInfo> resultList = new List<ItemInfo>(allUnlockedItemInfos.Length);
        foreach (ItemInfo tempItemInfo in allUnlockedItemInfos)
        {
            foreach (string ABC in ABCs)
            {
                string firstABC = tempItemInfo.englishName.Substring(0, 1).ToUpper();
                if (firstABC.Equals(ABC.ToUpper()))
                {
                    resultList.Add(tempItemInfo);
                    break;
                }
            }
        }
        return resultList.ToArray();
    }

    /// <summary>
    /// 返回所有与参数匹配的道具信息
    /// </summary>
    /// <param name="itemTypes">道具类型</param>
    /// <param name="ABCs">道具英文名首字母</param>
    /// <returns>所有与参数匹配的道具信息</returns>
    public static ItemInfo[] SelectUnlockedItemInfos(string[] itemTypes, string[] ABCs)
    {
        ItemInfo[] allUnlockedItemInfos = GetUnlockedItemInfos();
        
        if (allUnlockedItemInfos == null || allUnlockedItemInfos.Length <= 0) return null;
        if (itemTypes.Length <= 0 && ABCs.Length <= 0) return allUnlockedItemInfos;
        
        List<ItemInfo> matchTypeList;
        if (itemTypes.Length > 0)
        {
            matchTypeList = new List<ItemInfo>();
            foreach (ItemInfo tempItemInfo in allUnlockedItemInfos)
            {
                foreach (string itemType in itemTypes)
                {
                    if (tempItemInfo.itemType.Equals(itemType))
                    {
                        matchTypeList.Add(tempItemInfo);
                        break;
                    }
                }
            }
        }
        else
        {
            matchTypeList = new List<ItemInfo>(allUnlockedItemInfos);
        }

        List<ItemInfo> resultList;
        if (ABCs.Length > 0)
        {
            resultList = new List<ItemInfo>();
            foreach (ItemInfo tempItemInfo in matchTypeList)
            {
                foreach (string ABC in ABCs)
                {
                    string firstABC = tempItemInfo.englishName[0].ToString().ToUpper();
                    if (firstABC.Equals(ABC.ToUpper()))
                    {
                        resultList.Add(tempItemInfo);
                        break;
                    }
                }
            }
        }
        else
        {
            resultList = matchTypeList;
        }
        return resultList.ToArray();
    }

    /// <summary>
    /// 解锁道具
    /// </summary>
    /// <param name="unlockItem">待解锁道具</param>
    public static void Unlock(Item unlockItem)
    {
        if (IsUnlocked(unlockItem)) return;

        string fullPath = Application.persistentDataPath + UNLOCKED_ITEM_MEMO_DIRECTORY + "/" + UNLOCKED_ITEM_MEMO_FILENAME;
        string fullDirectory = Application.persistentDataPath + UNLOCKED_ITEM_MEMO_DIRECTORY;
        string content = string.Format("{0}\n", unlockItem.GetType().Name);

        if (!Directory.Exists(fullDirectory))
        {
            Directory.CreateDirectory(fullDirectory);
        }
        File.AppendAllText(fullPath, content, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// 检测是否入参道具类型是否已经解锁
    /// </summary>
    /// <param name="item">待检测道具</param>
    /// <returns>已解锁返回 true 否则返回 false</returns>
    public static bool IsUnlocked(Item item)
    {
        string[] unlockedItemNames = GetUnlockedItemNames();
        if (unlockedItemNames == null) return false;

        string typeName = item.GetType().Name;
        foreach (string tempItemName in unlockedItemNames)
        {
            if (tempItemName.Equals(typeName))
            {
                return true;
            }
        }
        return false;
    }

}
