using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// 背包类,负责记录场景中出现的道具信息(包括原有的和玩家生成的)
/// </summary>
public class Backpack
{
    /// <summary>
    /// 最大记录时间(天)
    /// </summary>
    public const int MAX_RECORD_DAY = 30;
    //记录当天发现的道具文本路径
    private const string FOUND_ITEM_MEMO_DIRECTORY = "/FoundItemMemo";
    private const string DATE_FORMAT = "yyyyMMdd";
    /// <summary>
    /// 格式化日期-道具信息数组字典
    /// </summary>
    private static Dictionary<string, ItemInfo[]> dateInfosPairs = new Dictionary<string, ItemInfo[]>();
    /// <summary>
    /// 格式化日期-道具信息数组字典
    /// </summary>
    private static Dictionary<string, ItemInfo[]> DateInfosPairs
    {
        get
        {
            if(dateInfosPairs.Count <= 0)
            {
                //dateInfosPairs = ReadFoundItemInfos();
                ReadFoundItemInfos();
            }
            return dateInfosPairs;
        }
    }

    private Backpack() { }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <returns></returns>
    //private static Dictionary<string, ItemInfo[]> ReadFoundItemInfos()
    private static void ReadFoundItemInfos()
    {
        //Dictionary<string, ItemInfo[]> dateInfosPairs = new Dictionary<string, ItemInfo[]>(MAX_RECORD_DAY);
        DateTime today = DateTime.Today;

        string fullDirectory = Application.persistentDataPath + FOUND_ITEM_MEMO_DIRECTORY;
        // 判断是否存在数据文本的文件路径
        if (Directory.Exists(fullDirectory))
        {
            // 循环获取并封装数据
            for (int i = 0; i < MAX_RECORD_DAY; i++)
            {
                // 格式化日期
                string formatDate = today.AddDays(-i).ToString(DATE_FORMAT);
                // 拼接数据文本的完整路径
                string fullPath = string.Format("{0}/{1}.txt", fullDirectory, formatDate);
                // 判断是否存在该日期数据文本 存在则读取并封装数据, 否则进入下一次循环
                if (File.Exists(fullPath))
                {
                    // 读取文本内容
                    string[] allLines = File.ReadAllLines(fullPath);
                    // 道具信息列表
                    List<ItemInfo> foundItemInfos = new List<ItemInfo>(allLines.Length);
                    foreach (string oneLine in allLines)
                    {
                        // 解析json字符串,并把解析结果添加到列表
                        if (!string.IsNullOrEmpty(oneLine))
                        {
                            foundItemInfos.Add(JsonUtility.FromJson<ItemInfo>(oneLine));
                        }
                    }
                    // 把格式化日期字符串和道具信息列表的对应关系添加到字典
                    dateInfosPairs.Add(formatDate, foundItemInfos.ToArray());
                }
                else
                {
                    continue;
                }
            }
        }

        //return dateInfosPairs;
    }

    public static DateTime[] GetKeys()
    {
        if (DateInfosPairs.Count <= 0) return null;

        Dictionary<string, ItemInfo[]>.KeyCollection keys = DateInfosPairs.Keys;
        string[] formatDates = new string[keys.Count];
        keys.CopyTo(formatDates, 0);

        List<DateTime> resultList = new List<DateTime>(DateInfosPairs.Count);
        foreach(string tempFormatDate in formatDates)
        {
            DateTime tempDateTime = DateTime.Today;
            tempDateTime = DateTime.ParseExact(tempFormatDate, DATE_FORMAT, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            resultList.Add(tempDateTime);
        }
        return resultList.ToArray();
    }

    public static DateTime[] GetSortKeys()
    {
        List<DateTime> resultList = new List<DateTime>(GetKeys());
        resultList.Sort((a, b) => b.CompareTo(a));
        return resultList.ToArray();
    }

    /// <summary>
    /// 保存当天道具信息
    /// </summary>
    public static void SaveFoundItemInfos()
    {
        // 格式化当天日期
        string formatToday = FormatToday();
        // 按日期获取道具信息数组
        ItemInfo[] todayFoundItemInfos = GetItemInfos(formatToday);
        // 判断道具信息数组是否有效
        if (todayFoundItemInfos != null && todayFoundItemInfos.Length > 0)
        {
            // 定义文本内容字符串
            string allText = string.Empty;
            // 循环当天道具信息, 拼接文本内容字符串
            foreach (ItemInfo tempInfo in todayFoundItemInfos)
            {
                // 把道具信息实例转化为json字符串
                allText += JsonUtility.ToJson(tempInfo) + "\n";
            }
            // 拼接完整文件夹路径
            string fullDirectory = Application.persistentDataPath + FOUND_ITEM_MEMO_DIRECTORY;
            // 检测文件夹路径, 若路径不完整, 则创建完整路径
            if (!Directory.Exists(fullDirectory))
            {
                Directory.CreateDirectory(fullDirectory);
            }
            // 拼接数据文件路径
            string fullPath = string.Format("{0}/{1}.txt", fullDirectory, formatToday);
            // 写入文件
            File.WriteAllText(fullPath, allText, System.Text.Encoding.UTF8);
        }
    }

    public static ItemInfo[] GetItemInfos(DateTime date)
    {
        string formatDate = date.ToString(DATE_FORMAT);
        return GetItemInfos(formatDate);
    }

    public static ItemInfo[] GetItemInfos(string formatDate)
    {
        ItemInfo[] resultArray = null;
        if (DateInfosPairs.ContainsKey(formatDate))
        {
            resultArray = DateInfosPairs[formatDate];
        }
        return resultArray;
    }

    /// <summary>
    /// 添加道具信息
    /// </summary>
    /// <param name="newItem">需添加的道具实例</param>
    public static void FoundItemInfo(Item newItem)
    {
        if (newItem == null) return;
        ItemInfo newItemInfo = ItemInfoHelper.GetItemInfo(newItem);
        string formatToday = FormatToday();
        if (DateInfosPairs.ContainsKey(formatToday))
        {
            List<ItemInfo> newItemInfoList = new List<ItemInfo>(DateInfosPairs[formatToday]);
            if (!newItemInfoList.Contains(newItemInfo))
            {
                newItemInfoList.Add(newItemInfo);
                DateInfosPairs[formatToday] = newItemInfoList.ToArray();
            }
        }
        else
        {
            ItemInfo[] itemInfos = { newItemInfo };
            DateInfosPairs.Add(formatToday, itemInfos);
        }
    }

    /// <summary>
    /// 清空背包
    /// </summary>
    public static void Clean()
    {
        string formatToday = FormatToday();
        if (DateInfosPairs.ContainsKey(formatToday))
        {
            ItemInfo[] emptyArray = new ItemInfo[0];
            DateInfosPairs[formatToday] = emptyArray;
            SaveFoundItemInfos();
        }
    }

    /// <summary>
    /// 获得今天日期的格式化字符串
    /// </summary>
    /// <returns></returns>
    public static string FormatToday()
    {
        return DateTime.Today.ToString(DATE_FORMAT);
    }

}
