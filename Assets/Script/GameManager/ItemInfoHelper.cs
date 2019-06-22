using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ChineseAndEnglish
{
    public string chineseName;
    public string englishName;
}
public class ItemInfoHelper
{
    // 中英文对照文本所在位置（不含Applicatioin.streamingAssetsPath)
    private const string ITEM_INFO_DICTIONARY_PATH = "/ItemInfoDictionary.txt";
    private const string ITEM_TYPE_ICON_PATH_PATH = "/ItemTypeIconPath.txt";
    private static Dictionary<string, ItemInfo> typeNameItemInfoPairs = new Dictionary<string, ItemInfo>();
    public static Dictionary<string, ItemInfo> TypeNameItemInfoPairs
    {
        get
        {
            if(typeNameItemInfoPairs.Count <= 0)
            {
                ReadItemInfoDictionaryText();
            }
            return typeNameItemInfoPairs;
        }
    }
    private static Dictionary<string, string> itemTypeIconPathPairs = new Dictionary<string, string>();
    public static Dictionary<string, string> ItemTypeIconPathPairs
    {
        get
        {
            if(itemTypeIconPathPairs.Count <= 0)
            {

            }
            return itemTypeIconPathPairs;
        }
    }

    private ItemInfoHelper() { }

    public static void ReadItemInfoDictionaryText()
    {
        string fullPath = Application.streamingAssetsPath + ITEM_INFO_DICTIONARY_PATH;
        if (File.Exists(fullPath))
        {
            ReadDictionaryTextAllLines(fullPath);
        }
        else
        {
            Debug.Log("丢失道具信息文件！");
        }
    }

    private static void ReadDictionaryTextAllLines(string fullPath)
    {
        string[] allLines = File.ReadAllLines(fullPath);
        foreach(string oneLine in allLines)
        {
            // 按‘：’切割类名 与 中英文信息
            char[] splitSeparator = { ':' };
            string[] splitRetule = oneLine.Split(splitSeparator, 2);
            // 去除前后空格
            string className = splitRetule[0].Trim();
            // 使用JsonUtility取中道具信息
            //Debug.Log(splitRetule[1]);
            ItemInfo itemInfo = GetItemInfoByJson(splitRetule[1]);
            //Debug.Log(itemInfo.englishName);
            //Debug.Log(itemInfo.chineseName);
            typeNameItemInfoPairs.Add(className, itemInfo);
        }
    }

    public static void ReadItemTypeIconPathText()
    {
        string fullPath = Application.streamingAssetsPath + ITEM_TYPE_ICON_PATH_PATH;
        if (File.Exists(fullPath))
        {
            string[] allLines = File.ReadAllLines(fullPath);
            foreach(string oneLine in allLines)
            {
                string[] splitRetule = oneLine.Split(':');
                itemTypeIconPathPairs.Add(splitRetule[0].Trim(), splitRetule[1].Trim());
            }
        }
        else
        {
            Debug.Log("丢失道具类型图标路径信息文件！");
        }
    }

    public static void SetNames(IHasID IHasIDIns)
    {
        string insTypeName = IHasIDIns.GetType().Name;
        if (TypeNameItemInfoPairs.ContainsKey(insTypeName))
        {
            ItemInfo itemInfo = TypeNameItemInfoPairs[insTypeName];
            IHasIDIns.SetChineseName(itemInfo.chineseName);
            IHasIDIns.SetEnglishName(itemInfo.englishName);
        }
    }

    public static ItemInfo GetItemInfo(string itemTypeName)
    {
        ItemInfo retItemInfo = null;
        if (TypeNameItemInfoPairs.ContainsKey(itemTypeName))
        {
            retItemInfo = TypeNameItemInfoPairs[itemTypeName];
        }
        return retItemInfo;
    }

    public static ItemInfo GetItemInfo(System.Type itemType)
    {
        string itemTypeName = itemType.Name;
        return GetItemInfo(itemTypeName);
    }

    public static ItemInfo GetItemInfo(Item item)
    {
        System.Type itemType = item.GetType();
        return GetItemInfo(itemType);
    }

    private static ItemInfo GetItemInfoByJson(string jsonStr)
    {
        return JsonUtility.FromJson<ItemInfo>(jsonStr);
    }

    public static string[] GroupByItemType()
    {
        Dictionary<string, ItemInfo>.ValueCollection values = TypeNameItemInfoPairs.Values;
        ItemInfo[] itemInfos = new ItemInfo[values.Count];
        values.CopyTo(itemInfos, 0);
        List<string> retItemTypes = new List<string>();
        foreach(ItemInfo tempItemInfo in itemInfos)
        {
            if (!retItemTypes.Contains(tempItemInfo.itemType))
            {
                retItemTypes.Add(tempItemInfo.itemType);
            }
        }
        return retItemTypes.ToArray();
    }

    public static string GetItemTypeIconPath(string itemType)
    {
        string ret = "";
        if (ItemTypeIconPathPairs.ContainsKey(itemType))
        {
            ret = ItemTypeIconPathPairs[itemType];
        }
        return ret;
    }
}
