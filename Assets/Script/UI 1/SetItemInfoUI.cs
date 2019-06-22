using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetItemInfoUI : MonoBehaviour
{
    private const string ITEM_IMAGES_PATH = "Sprites/ItemImages";

    public ItemInfo itemInfo;
    public Text chineseText;
    public Text englishText;
    public Text summary;
    public Image itemImage;
    public Sprite defaultSprite;

    private static Dictionary<string, Sprite> pathSpritePairs = new Dictionary<string, Sprite>();

    private void OnEnable()
    {
        SetItemInfo();
    }

    private void OnItemIconClick(ItemInfo itemInfo)
    {
        this.itemInfo = itemInfo;
        gameObject.SetActive(true);
    }

    private void SetItemInfo()
    {
        SetChinese();
        SetEnglish();
        SetSummary();
        SetImage();
    }

    private void SetChinese()
    {
        chineseText.text = itemInfo.chineseName;
    }

    private void SetEnglish()
    {
        englishText.text = itemInfo.englishName;
    }

    private void SetSummary()
    {
        summary.text = itemInfo.summary;
    }

    private void SetImage()
    {
        itemImage.sprite = GetSprite(itemInfo.spritePath);
    }

    private Sprite GetSprite(string path)
    {
        Sprite retSprite = null;
        if (pathSpritePairs.ContainsKey(path))
        {
            retSprite = pathSpritePairs[path];
        }
        else
        {
            retSprite = Resources.Load<Sprite>(path);
            if(retSprite == null)
            {
                retSprite = defaultSprite;
            }
            pathSpritePairs.Add(path, retSprite);
        }
        return retSprite;
    }

    public void OpenStore()
    {
        Application.OpenURL(itemInfo.storeUrl);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    //public void Open(ItemInfo itemInfo)
    //{
    //    this.itemInfo = itemInfo;
    //    gameObject.SetActive(true);
    //}
}
