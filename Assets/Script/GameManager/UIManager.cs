using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager ins;

    public const int SHOOT = 0;
    public const int ATTACK = 1;
    public const int RETURN = 2;
    public const int GROUP = 3;

    public Text m1Title, missionInnerTxt;
    public Text m2Title, m3Title;

    private List<string> missionTitle = new List<string>();
    private List<string> missionInner = new List<string>();

    public Image hpFG, weaponIcon;

    private Image[] hpIcons_Player;
    private Image[] hpIcons_Enemy;
    // 用字典存放每个游戏对象的血条
    private Dictionary<GameObject, GameObject> hpBarDict;

    public Text bulletCount;

    public Sprite[] groupButtonSprites;
    public Sprite[] attackButtonSprites;
    public Sprite[] shootButtonSprites;

    public GameObject[] gameObjectPanel = new GameObject[2];
    public GameObject[] gameObjectButtons;

    public GameObject gameOverPanel, winPanel;

    public GameObject deface;

    public GameObject inputIcon;

    public GameObject pausePanel;

    public GameObject missionPanel;

    public GameObject inputPanel;
    public GameObject SearchPanel;

    public GameObject frontPanel;

    public GameObject backpackPanel;

    public GameObject Canvas;

    public Text[] scoreText;

    public bool isInputing;

    private int score;

    private Transform hpFollowTarget;

    private void OnEnable()
    {
        EventManager.RegisterEvent<GameObject,int,int>("ShowHp", OnShowHp);
    }

    private void OnDisable()
    {
        EventManager.UnregisterEvent<GameObject,int,int>("ShowHp", OnShowHp);
    }

    private void Awake()
    {
        ins = GetComponent<UIManager>();
        //gameObjectPanel = GameObject.Find("GameUI/GameObject");

        //scoreText = transform.Find("PausePanel/Score").GetComponent<Text>();
        //scoreText = transform.Find("Score").GetComponent<Text>();

        hpIcons_Player = transform.Find("HpBar_Player").GetComponentsInChildren<Image>();
        hpIcons_Enemy = transform.Find("HpBar_Enemy").GetComponentsInChildren<Image>();

        //SetMaxHp(hpIcons_Player, 4);
        //SetMaxHp(hpIcons_Enemy, 6);

        //SetCurrentHp(hpIcons_Player, 2);
        //SetCurrentHp(hpIcons_Enemy, 3);

        OnScoreChanged(0);

    }

    private void OnShowHp(GameObject go, int currentHp, int maxHp)
    {
        Debug.Log("OnShowHp" + go.name);

        if (go.GetComponent<IDamagable>().hpBar == null)
        {
            EventManager.ExecuteEvent<GameObject>("AssignHpBar", go);
        }

        EventManager.ExecuteEvent<int, int>(go, "ChangeHp", currentHp, maxHp);
    }

    public void ShowUI(Vector3 worldPos)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = pos;

        gameObject.SetActive(true);
    }

    public void UpdateWeaponType(Sprite weaponSprite, int current, int max)
    {
        //更换武器图片
        weaponIcon.sprite = weaponSprite;
        //改变武器子弹数据
        UpdateBulletCount(current, max);
    }
    public void UpdateBulletCount(int current, int max)
    {
        bulletCount.text = current + "/" + max;
    }

    public void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void OpenGameObjectPanel(int state, int groupButtonType = 0, int attackButtonType = 0, int shootButtonType = 0)
    {
        ChangeGameObjectButtonImage(GROUP, groupButtonSprites[groupButtonType]);
        ChangeGameObjectButtonImage(ATTACK, attackButtonSprites[attackButtonType]);
        ChangeGameObjectButtonImage(SHOOT, shootButtonSprites[shootButtonType]);

        gameObjectPanel[state].SetActive(true);
    }

    public void OnRestartBtnClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitBtnClicked()
    {
        Debug.Log("Do OnQuitBtnClicked");
        Backpack.SaveFoundItemInfos();
        Application.Quit();
    }


    public void OnQuitClicked()
    {
        Debug.Log("Do OnQuitClicked");
        Backpack.SaveFoundItemInfos();
        SceneManager.LoadScene(0);
    }

    public void OnResetClicked()
    {
        GameController.ResetGame();
    }

    public void OnPauseClicked()
    {
        GameController.Pause(GameController.STATE_PAUSE);
        pausePanel.SetActive(true);
        UIManager.ins.ToggleFrontPanel(false);
    }

    public void ClossPauseClicked()
    {
        GameController.Resume();

        CloseAllPanels();
        frontPanel.SetActive(true);
    }

    private void CloseAllPanels()
    {
        pausePanel.SetActive(false);
        gameObjectPanel[0].SetActive(false);
        gameObjectPanel[1].SetActive(false);
        missionPanel.SetActive(false);
        inputPanel.SetActive(false);

    }

    private void ToggleGameObjectButtons(int buttonID, bool active)
    {
        gameObjectButtons[buttonID].SetActive(active);
    }

    private void ChangeGameObjectButtonImage(int buttonID, Sprite sprite)
    {
        gameObjectButtons[buttonID].GetComponent<Image>().sprite = sprite;
    }

    public void OnInputClicked()
    {
        GameController.Pause(GameController.STATE_INPUT);
        inputPanel.SetActive(true);
        //Canvas.SetActive(!Canvas.activeSelf);
        //isInputing = Canvas.activeSelf;
    }

    //时间暂停，状态切换
    public void OnSearchClicked()
    {
        if (GameController.state == GameController.STATE_PLAYING)
        {
            GameController.Pause(GameController.STATE_SEARCH);
            //SearchPanel.SetActive(true);
        }
        else if (GameController.state == GameController.STATE_SEARCH)
        {
            GameController.Resume();
            gameObjectPanel[GameController.STATE_SEARCH].SetActive(false);
        }
    }

    public void OnMissionClicked()
    {
        GameController.Pause(GameController.STATE_PAUSE);

        OpenMissionPanel();
    }


    public void AddMisionUI(string title, string inner)
    {
        ins.missionTitle.Add(title);
        ins.missionInner.Add(inner);
    }

    public void OpenMissionPanel()
    {
        //Mssion1
        ins.m1Title.text = ins.missionTitle[0];
        if (ins.missionTitle.Count >= 2)
            ins.m2Title.text = ins.missionTitle[1];
        if (ins.missionTitle.Count >= 3)
            ins.m3Title.text = ins.missionTitle[2];

        ins.missionInnerTxt.text = ins.missionInner[0];

        missionPanel.SetActive(true);
        frontPanel.SetActive(false);

    }

    public void ToggleFrontPanel(bool isOn)
    {
        frontPanel.SetActive(isOn);
    }

    public void OnToggleValueChange(Toggle toggle)
    {
        if (toggle.isOn)
        {
            int index = int.Parse(toggle.gameObject.name);
            if (ins.missionInner[index] != null)
                ins.missionInnerTxt.text = ins.missionInner[index];
        }
    }

    public void ShowDeleteIcon()
    {
        //1 . 拿到需要进行操作的对象 DeleteIcon - transform.find（XXX） 或者  public 拖
        // 2. 拿着这个对象设置其是否激活 XXX.setActive（）

        deface.SetActive(true);
        inputIcon.SetActive(false);
    }

    public void HideDeleteIcon()
    {
        deface.SetActive(false);
        inputIcon.SetActive(true);
    }

    public void OnScoreChanged(int score)
    {
        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = score.ToString("D7");
        }
    }

    public void AddScore()
    {
        score += 100;
        for (int i = 0; i < scoreText.Length; i++)
        {
            //transform.Find("Score").GetComponent<Text>().text = score.ToString();
            scoreText[i].text = score.ToString();
        }
    }

    public void ShowItemPanel(Vector3 worldPos, int groupButtonType)
    {
        SetPanelPos(gameObjectPanel[1].transform.parent.GetComponent<RectTransform>(), worldPos);
        OpenGameObjectPanel(GameController.STATE_PLAYING, groupButtonType, 1 ,1);
    }

    public void ShowItemText(string text, Vector3 worldPos)
    {
        Debug.Log("text = " + text);
        SetPanelPos(gameObjectPanel[1].transform.parent.GetComponent<RectTransform>(), worldPos);

        gameObjectPanel[1].transform.Find("Text").GetComponent<Text>().text = text;

        OpenGameObjectPanel(GameController.STATE_SEARCH);
    }

    // 修改worldPos或screenPos使文本往上偏移
    private void SetPanelPos(RectTransform panelRect, Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos) / GetScreenSizeRatio();

        panelRect.anchoredPosition = screenPos;
    }

    public void ChangePlayerHp(int currentHp, int maxHp, Transform targetTrans)
    {
        Debug.Log("ChangePlayerHp:" + currentHp + "/" + maxHp);

        hpFollowTarget = targetTrans;
        SetMaxHp(hpIcons_Player, maxHp);
        SetCurrentHp(hpIcons_Player, currentHp);

        StartCoroutine(HpFollow(hpIcons_Player));
    }

    public void ChangeEnemyHp(int currentHp, int maxHp, Transform targetTrans)
    {
        Debug.Log("ChangeEnemyHp:" + currentHp + "/" +  maxHp);

        hpFollowTarget = targetTrans;
        SetMaxHp(hpIcons_Enemy, maxHp);
        SetCurrentHp(hpIcons_Enemy, currentHp);

        StartCoroutine(HpFollow(hpIcons_Enemy));
    }

    private void SetMaxHp(Image[] hpIcons, int maxHp)
    {
        for(int i = 0; i < hpIcons.Length; i++)
        {
            if(i < maxHp)
            {
                hpIcons[i].gameObject.SetActive(true);
            }
            else
            {
                hpIcons[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetCurrentHp(Image[] hpIcons, int currentHp)
    {
        for (int i = 0; i < hpIcons.Length; i++)
        {
            if (i < currentHp)
            {
                hpIcons[i].enabled = true;
            }
            else
            {
                hpIcons[i].enabled = false;
            }
        }
    }

    private float GetScreenSizeRatio()
    {
        return (float)Screen.width / 1280;
    }

    // 添加血条跟踪的游戏对象参数
    IEnumerator HpFollow(Image[] hpIcons)
    {
        float timer = 0;

        while (timer < 1f)
        {
            timer += Time.deltaTime;

            if (hpFollowTarget)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(hpFollowTarget.position) / GetScreenSizeRatio();

                // hpIcons_Enemy改为根据参数从字典中获取
                RectTransform rectParent = hpIcons[0].transform.parent as RectTransform;
                rectParent.anchoredPosition = screenPos;
            }

            yield return null;
        }

        // 显示结束，隐藏血条
        SetCurrentHp(hpIcons, 0);
    }

    public void OpenBackpack()
    {
        backpackPanel.SetActive(true);
    }

    public void CloseBackpack()
    {
        backpackPanel.SetActive(false);
    }
}
