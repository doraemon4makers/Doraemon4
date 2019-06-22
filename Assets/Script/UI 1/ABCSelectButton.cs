using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABCSelectButton : IconButton
{
    public string ABC;
    public bool autoPad = true;
    public Text text;
    private const int START_INT = 'A';
    
    private void Awake()
    {
        PadABC();
        text.text = ABC;
    }

    private void PadABC()
    {
        if (!autoPad) return;

        ABCSelectButton[] ABCSelects = transform.parent.GetComponentsInChildren<ABCSelectButton>(true);
        for (int i = 0; i < ABCSelects.Length; i++)
        {
            if (ABCSelects[i].Equals(this))
            {
                ABC = ((char)(START_INT + i)).ToString();
            }
        }
    }
}
