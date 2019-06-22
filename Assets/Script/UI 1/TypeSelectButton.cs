using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeSelectButton : IconButton
{
    public string itemType;
    public Text text;

    protected override void Init()
    {
        base.Init();
        text.text = itemType;
    }
}
