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
public class ReadWordDictionary
{
    // 中英文对照文本所在位置（不含Applicatioin.streamingAssetsPath)
    private const string DICTIONARY_PATH = "/ChineseAndEnglishDictionary.txt";
    private static Dictionary<string, ChineseAndEnglish> classCnEnDic = new Dictionary<string, ChineseAndEnglish>();

    public static void ReadDictionaryText()
    {
        string fullPath = Application.streamingAssetsPath + DICTIONARY_PATH;
        if (File.Exists(fullPath))
        {
            ReadDictionaryTextAllLines(fullPath);
        }
        else
        {
            Debug.Log("丢失中英对照文件！");
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
            // 使用JsonUtility取中英文信息
            ChineseAndEnglish cnAndEn = GetChineseAndEnglishByJson(splitRetule[1]);
            classCnEnDic.Add(className, cnAndEn);
        }
    }

    private static ChineseAndEnglish GetChineseAndEnglishByJson(string jsonStr)
    {
        //Debug.Log("jsonStr.Trim() = "+jsonStr.Trim());
        ////JsonUtility.FromJson<ChineseAndEnglish>(jsonStr);
        //ChineseAndEnglish cnAndEnglish = new ChineseAndEnglish();
        //cnAndEnglish.chineseName = "机器人";
        //cnAndEnglish.englishName = "robot";
        //Debug.Log("JsonUtility.ToJson(cnAndEnglish) = " + JsonUtility.ToJson(cnAndEnglish));
        //ChineseAndEnglish ret = JsonUtility.FromJson<ChineseAndEnglish>(jsonStr.Trim());
        //Debug.Log("ret.chineseName = "+ret.chineseName);
        //Debug.Log("ret.englishName = "+ret.englishName);
        return JsonUtility.FromJson<ChineseAndEnglish>(jsonStr.Trim());
    }

    public static void SetChineseAndEnglishName(IHasID IHasIDIns)
    {
        string insTypeName = IHasIDIns.GetType().Name;
        if (classCnEnDic.ContainsKey(insTypeName))
        {
            ChineseAndEnglish chAndEn = classCnEnDic[insTypeName];
            IHasIDIns.SetChineseName(chAndEn.chineseName);
            IHasIDIns.SetEnglishName(chAndEn.englishName);
        }
    }
}
