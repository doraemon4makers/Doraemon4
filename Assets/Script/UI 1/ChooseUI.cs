using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ChooseUI : MonoBehaviour,IPointerClickHandler
{
    private delegate void GroupButtonDelegate();
    private GroupButtonDelegate[] groupButtonDelegates = new GroupButtonDelegate[2];

    IGroupable groupable;

    private const int GROUP = 0;
    private const int UNGROUP = 1;

    private int delegateIndex = GROUP;


    void Start()
    {
        groupButtonDelegates[0] = OnGroup;
        groupButtonDelegates[1] = OnUnGroup;

        groupable = GetComponent<IGroupable>();

        if(groupable != null && groupable.IsOwner)
        {
            delegateIndex = UNGROUP;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if(GameController.state == GameController.STATE_PLAYING)
        {
            if(groupable.ShouldShowUI)
            {
                Debug.Log("Playing Click");

                GameController.Pause(GameController.STATE_PAUSE);

                UIManager.ins.ShowItemPanel(transform.position, groupable.IsGroupParent ? 0 : 1);

                UIManager.ins.gameObjectPanel[GameController.STATE_PLAYING].transform.Find("Group").GetComponent<Button>().onClick.RemoveAllListeners();
                UIManager.ins.gameObjectPanel[GameController.STATE_PLAYING].transform.Find("Group").GetComponent<Button>().onClick.AddListener(OnGroupButtonClick);

//                UIManager.ins.gameObjectPanel[GameController.STATE_PLAYING].transform.Find("Group").GetComponent<Button>().onClick.AddListener(() => { groupButtonDelegates[delegateIndex](); });
            }
        }
        else if(GameController.state == GameController.STATE_SEARCH)
        {
            Debug.Log("Search Click");

            IHasID item = GetComponent<IHasID>();

            if(item != null)
            {
                    Debug.Log("eventData.button = " + eventData.button);
                // 左键显示英文 右键显示中文
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    //  按左键英文  按右键 中文    id 分成两组 一组英文，一组英文
                    UIManager.ins.ShowItemText(item.englishName, transform.position);
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    Debug.Log("监听到右键点击目标");
                    UIManager.ins.ShowItemText(item.chineseName, transform.position);
                }
            }
        }
    }

    void OnGroupButtonClick()
    {
        if(!groupable.HasGroup)
        {
            Debug.Log(gameObject.name + ": Group");
            OnGroup();
        }
        else
        {
            Debug.Log(gameObject.name + ": UnGroup");
            OnUnGroup();
        }
    }

    void OnGroup()
    {
        if(!groupable.IsOwner)
        {
            GameController.Group(groupable);
            delegateIndex = UNGROUP;
        }
    }

    void OnUnGroup()
    {
        groupable.UnGroup();

        if(!groupable.IsOwner)
        {
            delegateIndex = GROUP;
        }
    }
}
