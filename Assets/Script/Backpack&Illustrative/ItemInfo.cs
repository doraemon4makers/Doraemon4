using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具信息类,用于背包/图鉴
/// </summary>
[System.Serializable]
public class ItemInfo
{
    /// <summary>
    /// 道具ID,用于图鉴排序
    /// </summary>
    public int itemID;
    /// <summary>
    /// 道具类型,用于分类筛选
    /// </summary>
    public string itemType;
    /// <summary>
    /// 道具图标路径,用于Resources.Load
    /// </summary>
    public string itemIconPath;
    /// <summary>
    /// 道具简介图片路径,用于Resources.Load
    /// </summary>
    public string spritePath;
    /// <summary>
    /// 道具中文名称
    /// </summary>
    public string chineseName;
    /// <summary>
    /// 道具英文名称
    /// </summary>
    public string englishName;
    /// <summary>
    /// 道具简介
    /// </summary>
    public string summary;
    /// <summary>
    /// 商店连接
    /// </summary>
    public string storeUrl;

    /// <summary>
    /// 重写比较方法
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        bool result = false;
        if (!GetType().IsAssignableFrom(obj.GetType()))
        {
            result = false;
        }
        else
        {
            ItemInfo tempInfo = (ItemInfo)obj;
            result = itemID == tempInfo.itemID
                && itemType.Equals(tempInfo.itemType)
                && itemIconPath.Equals(tempInfo.itemIconPath)
                && spritePath.Equals(spritePath)
                && chineseName.Equals(chineseName)
                && englishName.Equals(englishName)
                && summary.Equals(summary)
                && storeUrl.Equals(storeUrl);
        }
        return result;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
