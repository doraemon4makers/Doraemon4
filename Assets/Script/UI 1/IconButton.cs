using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconButton : MonoBehaviour
{
    public string iconPath;
    public Sprite defaultSprite;
    public Image buttonImage;
    public bool isChecked;

    private static Dictionary<string, Sprite> pathSpritePairs = new Dictionary<string, Sprite>();

    protected virtual void OnEnable()
    {
        Init();
    }

    protected virtual void Start()
    {
        Init();
    }

    //public void Reload()
    //{
    //    Init();
    //}

    protected virtual void Init()
    {
        buttonImage.sprite = GetSprite(iconPath);
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
            if (retSprite == null)
            {
                retSprite = defaultSprite;
            }
            pathSpritePairs.Add(path, retSprite);
        }
        return retSprite;
    }

    public void ChangeChecked()
    {
        isChecked = !isChecked;
    }
}
