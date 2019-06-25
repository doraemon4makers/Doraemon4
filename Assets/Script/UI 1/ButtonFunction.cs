using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{
    //public string rootCanavsName;
    private static GameObject rootCanavs;
    private GameObject RootCanavs
    {
        get
        {
            if(rootCanavs == null)
            {
                Canvas canvas = GetComponentInParent<Canvas>();
                if(canvas != null)
                {
                    rootCanavs = canvas.rootCanvas.gameObject;
                }
            }
            return rootCanavs;
        }
    }

    private const string BACKPACK_PANEL_NAME = "BackpackPanel";
    private static GameObject backpackPanel;
    private GameObject BackpackPanel
    {
        get
        {
            if (backpackPanel == null)
            {
                Transform tempTrans = RootCanavs.transform.Find(BACKPACK_PANEL_NAME);
                if(tempTrans != null)
                {
                    backpackPanel = tempTrans.gameObject;
                }
            }
            return backpackPanel;
        }
    }

    private const string ILLUSTRATIVE_PANEL_NAME = "IllustrativePanel";
    private static GameObject illustrativePanel;
    private GameObject IllustrativePanel
    {
        get
        {
            if(illustrativePanel == null)
            {
                Transform tempTrans = RootCanavs.transform.Find(ILLUSTRATIVE_PANEL_NAME);
                if(tempTrans!= null)
                {
                    illustrativePanel = tempTrans.gameObject;
                }
            }
            return illustrativePanel;
        }
    }

    private static BookPro bookPro;
    private BookPro BookProBehaviour
    {
        get
        {
            if (bookPro == null)
            {
                bookPro = IllustrativePanel.GetComponent<IllustrativeUI>().bookPro;
            }
            return bookPro;
        }
    }

    private const string ITEM_INFO_PANEL_NAME = "ItemInfoPanel";
    private static GameObject itemInfoPanel;
    private GameObject ItemInfoPanel
    {
        get
        {
            //if (itemInfoPanel == null)
            //{
            //    Transform tempTrans = RootCanavs.transform.Find(ITEM_INFO_PANEL_NAME);
            //    if(tempTrans != null)
            //    {
            //        itemInfoPanel = tempTrans.gameObject;
            //    }
            //}
            itemInfoPanel = BookProBehaviour.papers[BookProBehaviour.CurrentPaper - 1].Back;
            return itemInfoPanel;
        }
    }

    private const string SIMPLE_INFO_PANEL_NAME = "IllustrativePanel/MiddlePanel/BookPanel/PagePanel/SimpleInfoPanel";
    private static GameObject simpleInfoPanel;
    private GameObject SimpleInfoPanel
    {
        get
        {
            //if(simpleInfoPanel == null)
            //{
            //    Transform tempTrans = RootCanavs.transform.Find(SIMPLE_INFO_PANEL_NAME);
            //    if (tempTrans != null)
            //    {
            //        simpleInfoPanel = tempTrans.gameObject;
            //    }
            //}
            simpleInfoPanel = BookProBehaviour.papers[BookProBehaviour.CurrentPaper].Front;
            return simpleInfoPanel;
        }
    }

    private const string FULL_INFO_PANEL_NAME = "FullInfoPanel";
    private static GameObject fullInfoPanel;
    private GameObject FullInfoPanel
    {
        get
        {
            if (fullInfoPanel == null)
            {
                Transform tempTrans = RootCanavs.transform.Find(FULL_INFO_PANEL_NAME);
                if(tempTrans != null)
                {
                    fullInfoPanel = tempTrans.gameObject;
                }
            }
            return fullInfoPanel;
        }
    }

    public void ClosePanel(GameObject panel)
    {
        if(panel!= null && panel.activeSelf)
        {
            panel.SetActive(false);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        if (panel != null && !panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }

    public void OpenBackpack()
    {
        if (BackpackPanel != null && !BackpackPanel.activeSelf)
        {
            BackpackPanel.SetActive(true);
        }
    }

    public void CloseBackpack()
    {
        if (BackpackPanel != null && BackpackPanel.activeSelf)
        {
            BackpackPanel.SetActive(false);
        }
    }

    public void OpenItemInfoPanel()
    {
        if(ItemInfoPanel != null && !ItemInfoPanel.activeSelf)
        {
            ItemInfoPanel.SetActive(true);
        }
    }

    public void CloseItemInfoPanel()
    {
        if (ItemInfoPanel != null && ItemInfoPanel.activeSelf)
        {
            ItemInfoPanel.SetActive(false);
        }
    }

    public void OpenFullInfoPanel()
    {
        if(FullInfoPanel != null && !FullInfoPanel.activeSelf)
        {
            FullInfoPanel.SetActive(true);
        }
    }

    public void SetFullInfoPanelItemInfo(ItemButton button)
    {
        FullInfoUI fullInfo = FullInfoPanel.GetComponent<FullInfoUI>();
        fullInfo.itemInfo = button.itemInfo;
    }

    public void SelectItem()
    {
        IllustrativeUI illustrativeUI = IllustrativePanel.GetComponent<IllustrativeUI>();
        if (illustrativeUI != null)
        {
            illustrativeUI.ShowItemIcon();
        }
    }

    public void DisplayImage(ItemButton button)
    {
        //Image image = SimpleInfoPanel.transform.GetComponentInChildren<Image>(true);
        //Button btn = SimpleInfoPanel.transform.GetComponentInChildren<Button>(true);
        //image.gameObject.SetActive(false);
        //btn.gameObject.SetActive(false);
        //if (button != null)
        //{
        //    Sprite sprite = Resources.Load<Sprite>(button.itemInfo.spritePath);
        //    if(sprite != null)
        //    {
        //        image.sprite = sprite;
        //        image.gameObject.SetActive(true);
        //        btn.gameObject.SetActive(true);
        //    }
        //}

        //if (SimpleInfoPanel != null && SimpleInfoPanel.activeSelf)
        //{
        //    SimpleInfoPanel.transform.Find("Content").gameObject.SetActive(false);
        //}
        foreach(Paper paper in BookProBehaviour.papers)
        {
            paper.Front.transform.Find("Content").gameObject.SetActive(false);
        }
        Image image = SimpleInfoPanel.transform.GetComponentInChildren<Image>(true);
        if (button != null && image != null)
        {
            Sprite sprite = Resources.Load<Sprite>(button.itemInfo.spritePath);
            if (sprite != null)
            {
                image.sprite = sprite;
                if (SimpleInfoPanel != null && !SimpleInfoPanel.activeSelf)
                {
                    //SimpleInfoPanel.SetActive(true);
                    SimpleInfoPanel.transform.Find("Content").gameObject.SetActive(true);
                }
            }
        }
    }
}
