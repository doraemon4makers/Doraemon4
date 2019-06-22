using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullInfoUI : MonoBehaviour
{
    public ItemInfo itemInfo;
    public Image itemMainImage;
    public Text itemSummaryText;

    private void OnEnable()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        if(itemMainImage != null)
        {
            Sprite sprite = Resources.Load<Sprite>(itemInfo.spritePath);
            if (sprite != null)
            {
                itemMainImage.sprite = sprite;
            }
        }
        if(itemSummaryText != null)
        {
            itemSummaryText.text = itemInfo.summary;
        }
    }
}
