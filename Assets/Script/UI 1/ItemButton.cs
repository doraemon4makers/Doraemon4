using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : IconButton
{
    public ItemInfo itemInfo;
    public Text text;

    protected override void Init()
    {
        base.Init();
        text.text = itemInfo.englishName;
    }
}
