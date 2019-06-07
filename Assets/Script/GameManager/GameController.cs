using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //存储静态 游戏控制器实例
    public static GameController instance;

    //0作为STATE_PLAYIN中的常量
    public const int STATE_PLAYING = 0;
    
    public const int STATE_SEARCH = 1;
    public const int STATE_INPUT = 2;
    public const int STATE_PAUSE = 3;

    //state属性 作为
    public static int state { get; private set; }

    //储存玩家位置静态变量
    public static Transform player;
    //存储父对象位置变量
    public Transform parent;
    //存储分数
    private int score;

    // Start is called before the first frame update
    private void Awake()
    {
        //？？
        instance = this;
        Item.InitItemDict();
        ReadWordDictionary.ReadDictionaryText();
        Debug.Log("Awake");
    }

    void Start()
    {
        //
        player = GameObject.FindWithTag("Player").transform;
        Debug.Log("Start");

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            AddScore(1000);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            //分离（玩家与父对象）
            Detach(player, parent);
        }

        // 开关文本输入框
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.ins.OnInputClicked();
        }
    }

    public static void Pause(int newState)
    {
        //??
        state = newState;
        Time.timeScale = 0;
    }

    public static void Resume()
    {
        //
        state = STATE_PLAYING;
        Time.timeScale = 1;
    }

    //重新开始
    public static void ResetGame()
    {
        Resume();
        SceneManager.LoadScene(1);
    }

    static Transform GetSlot(Transform root, SlotType slotType)
    {
        SlotMarker[] slotsOnParent = root.GetComponentsInChildren<SlotMarker>();

        foreach (SlotMarker s in slotsOnParent)
        {
            if (s.slotType == slotType && s.IsOccupied == false)
            {
                return s.transform;
            }
        }

        return null;
    }

    static void DisableColliders(Transform root)
    {
        Collider2D[] cols = root.GetComponentsInChildren<Collider2D>();

        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].enabled = false;
        }

        Rigidbody2D body = root.GetComponent<Rigidbody2D>();

        if (body)
        {
            body.velocity = Vector3.zero;
            body.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public static void TryAttach(Transform trans_1, Transform trans_2)
    {
        Transform attachParent = null;
        Transform attachChild = null;

        SlotType slotType1 = trans_1.GetComponent<IGroupable>().slotType;
        SlotType slotType2 = trans_2.GetComponent<IGroupable>().slotType;

        Debug.Log("Slot Type 1:" + slotType1);
        Debug.Log("Slot Type 2:" + slotType2);

        Transform slotOnTrans_1 = GetSlot(trans_1, slotType2);
        Transform slotOnTrans_2 = GetSlot(trans_2, slotType1);

        if(slotOnTrans_1)
        {
            Debug.Log(trans_2.name + " can attach to " + trans_1.name);
            attachParent = slotOnTrans_1;
            attachChild = trans_2;
        }
        else if(slotOnTrans_2)
        {
            Debug.Log(trans_1.name + " can attach to " + trans_2.name);
            attachParent = slotOnTrans_2;
            attachChild = trans_1;
        }

        if(attachParent != null)
        {
            DisableColliders(attachChild);

            attachChild.position = attachParent.position;
            //子对象旋转为合并点旋转
            attachChild.rotation = attachParent.rotation;
            //设置父子关系, setParent 是系统transform里有的方法
            attachChild.SetParent(attachParent);
        }
    }

    //分离方法（子对象位置，父对象位置）
    public static void Detach(Transform child, Transform parent)
    {
        Transform detachPoint = parent.Find("DetachPoint");

        if (detachPoint == null)
        {
            detachPoint = parent;
        }

        child.position = detachPoint.position;
        child.SetParent(null);

        Collider2D[] cols = child.GetComponentsInChildren<Collider2D>();

        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].enabled = true;
        }
        //子对象身体 为 获取子对象刚体
        Rigidbody2D childBody = child.GetComponent<Rigidbody2D>();

        //如果是子对象身体
        if (childBody)
        {
            //子对象获取刚体的动力学刚体为false
            childBody.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        RobotIns isPlayerSideRobot = parent.GetComponent<RobotIns>();
        if (isPlayerSideRobot != null)
        {
            isPlayerSideRobot.isPlayerSide = false;
        }
    }

    //组合功能
    public static void Group(IGroupable groupable)
    {
        groupable.Group(player.GetComponent<IGroupable>());
        player.GetComponent<IGroupable>().Group(groupable);
        ////驾驶获取父对象vehicle组件
        //Vehicle vehicle = parent.GetComponent<Vehicle>();

        //if(vehicle)
        //{
        //    vehicle.Group(player.GetComponent<Play1>());
        //}
        //Fly fly = parent.GetComponent<Fly>();
        //if (fly)
        //{
        //    fly.Group(player.GetComponent<Play1>());
        //}
    }

    public static void FlyGroup(Transform parent)
    {       

    }

    public static void UnGroup(Transform parent)
    {
        //？？ 这个是实例化吗 ——得到组件 不算实例化
        Vehicle vehicle = parent.GetComponent<Vehicle>();
        //是假如vehicle不为空这么翻译吗？？
        if (vehicle)
        {
            vehicle.UnGroup();
        }
    }

    //  增加分数的方法                     amount是在哪里声明的，不声明 可以直接在方法里声明吗？
    public void AddScore(int amount)
    {
        //分数为 分数加amount
        score = score + amount;
        //加完分数后执行 UIManager下的ins 的OnScoreChanged 分数变换方法
        UIManager.ins.OnScoreChanged(score);
    }
}
