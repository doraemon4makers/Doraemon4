using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Player, IHasID, IDamagable, IGroupable
{
    public bool isPlayerSide { get; set; }

    public float Distance = 1f;
    protected Weapon1 currentWeapon;
    public int maxHp;
    protected Armor1 myArmor;
    private int currentHp;
    protected int CurrentHp
    {
        get
        {
            return currentHp;
        }

        set
        {
            currentHp = Mathf.Clamp(value, 0, maxHp);
        }
    }
    public string englishName
    {
        get
        {
            return _englishName;
        }
    }
    protected string _englishName;

    public string chineseName
    {
        get
        {
            return _chineseName;
        }
    }
    protected string _chineseName;

    public HpBar hpBar
    {
        get;

        set;
    }

    public Rigidbody2D body;
    public SpriteRenderer myRender;
    public float jump;
    public float moveSpeed;
    public Transform leftFistTrans;
    public Transform rightFistTrans;
    protected Animator myAni;
    private bool isGround = true;
    private bool isFlying;
    private int currentJump = 2;
    private GameObject gun;
    public Transform firePoint;
    private GameObject enemy;
    public const int PLAYER_SMALL = 0;
    public const int PLAYER_BIG = 1;
    public const int PLAYER_NORMAL = 2;
    private UIManager ui;
    private Collider2D col;
    private Vector3 forwardZero = Vector3.right + Vector3.up;
    private bool isShootAnimPlaying = false;

    protected Transform hpBarPoint;

    void Awake()
    {
        IsOwner = true;
        IsGroupParent = false;
    }
    void Start()//游戏初始化上
    {
        isPlayerSide = true;

        //玩家身体 获取2D刚体组件
        body = GetComponent<Rigidbody2D>();
        //获取我的渲染变量在渲染精灵上的组件
        myRender = GetComponent<SpriteRenderer>();
        //我的动画变量获取动画控制器
        myAni = GetComponent<Animator>();
        //实例化护甲类——用于调用护甲类上的方法
        myArmor = new Armor1(1);
        //现在持有武器变量 获得 boomerang武器的实例
        currentWeapon = new Boomerang(firePoint, true);
        //现在血量 存储最大血量的值——？
        CurrentHp = maxHp;
        //碰撞体变量获得碰撞体组件
        col = GetComponent<Collider2D>();
        //敌人变量挂找到标签为Enemy2的物体
        enemy = GameObject.FindGameObjectWithTag("Enemy2");
        //ui挂找到GameUI身上的的物体与UIManager类上的组件
        ui = GameObject.Find("GameUI").GetComponent<UIManager>();

        ui.ChangePlayerHp(currentHp, maxHp, hpBarPoint);

        hpBarPoint = transform.Find("HpBarPoint");

        //SetEnglishName("Art I ");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

    }

    void Update()
    {
        isGround = CheckGround();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (isFly && vertical > 0.5f)
        {
            isFlying = true;
        }
        if (isGround)
        {
           
            currentJump = 2;
            Debug.Log("跳跃可以吗？0");
            myAni.SetBool("Jump", false);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            myAni.SetBool("Jump", true);
            Debug.Log("跳跃可以吗？1");
            if (currentJump >= 1)
            {
                body.velocity = Vector2.up * jump + body.velocity.x * Vector2.right;
                //此时我的为Jump的动画为true
               
                //currentJump变量减一次
                Debug.Log("跳跃可以吗？2");
                currentJump--;
               
            }
        }
        if (horizontal != 0 || vertical != 0)
        {
            //则让移动的动画设为true
            Debug.Log("跳跃可以吗？3");
            myAni.SetBool("Move", true);
            //如果枪的值为空，则枪的变量为 子对象的位置
            //if (gun == null)
            //    gun = transform.GetChild(0).gameObject;

            //if (gun.activeSelf)
            //    gun.SetActive(false);
            //如果(horizontal< 0则执行水平位置向左
            if (horizontal < 0)
            {
                Debug.Log("跳跃可以吗？4");
                transform.right = Vector3.left;
            }
            //否则(horizontal< 0则执行水平位置向右
            else
            {
                Debug.Log("跳跃可以吗？5");
                transform.right = Vector3.right;
            }
            //如果飞行 则 执行body力的速率是 水平向右*x轴方向操作*移动速度+水平向上*y轴方向操作*移动速度
            if (isFly && isFlying)
            {
                body.velocity = Vector2.right * horizontal * moveSpeed + Vector2.up * vertical * moveSpeed;
            }
            //否则body速率向为水平向上*y轴方向操作*移动速度的 力为0
            else
            {
                body.velocity = Vector2.right * horizontal * moveSpeed + body.velocity.y * Vector2.up;
            }
            //myRender.flipX = horizontal < 0;
        }
        //否则 执行 我的动画为MOVE的组件 为false
        else
        {
            myAni.SetBool("Move", false);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            isFlying = true;
            body.gravityScale = 0;
        }

        //当按下I键则执行 isFlyin 为 false，而且body的重力比例为5
        if (Input.GetKeyDown(KeyCode.I))
        {
            isFlying = false;
            body.gravityScale = 5;
        }

        //if (isGround && Input.GetMouseButton(1)) //鼠标左键 为0 鼠标右键为1
        if (isGround && Input.GetMouseButtonDown(1)) //鼠标左键 为0 鼠标右键为1
        {
            ColdWeapon coldWeapon = rightFistTrans.GetComponentInChildren<ColdWeapon>();
            if(coldWeapon != null)
            {
                Cut();
            }
        }

        if(isGround && Input.GetMouseButtonDown(0))
        {
            GunIns gun = leftFistTrans.GetComponentInChildren<GunIns>();
            if (gun != null&& !isShootAnimPlaying && gun.canShoot)
            {
                Shoot(gun);
            }
        }
        timer += Time.deltaTime;

    }
    float timer;
    protected void Shoot(GunIns gun)
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePositionInScene = Vector3.Scale(mousePositionInWorld, forwardZero);
        Vector3 toMousePosition = mousePositionInScene - transform.position;
        if (toMousePosition.x * transform.right.x < 0)
        {
            transform.right *= -1; 
        }
        
        gun.Use(transform);
        myAni.SetTrigger("Shoot");
        isShootAnimPlaying = true;
        ////假如现有武器冷却完毕 
        //if (currentWeapon.IsColdDown())
        //{
        //    //重新开始冷却计时！！！
        //    //timer = 0;

        //    //开火点对象显示开火火花
        //    firePoint.gameObject.SetActive(true);
        //    //播放射击动画
        //    myAni.SetTrigger("Shoot");
        //    //产生子弹

        //    Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, 5f);
        //    for (int i = 0; i < enemys.Length; i++)
        //    {
        //        if (enemys[i].gameObject.tag == "enemy")
        //        {
        //            enemy = enemys[i].gameObject;
        //            break;
        //        }
        //    }


        //    if (enemy != null)
        //    {
        //        currentWeapon.Aim(enemy.transform.position);
        //        currentWeapon.Fire(firePoint);
        //    }
        //    else
        //    {
        //        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        v = Vector3.Scale(v, Vector3.right + Vector3.up);
        //        currentWeapon.Aim(v);
        //        currentWeapon.Fire(firePoint);
        //    }
        //}
    }

    protected void Cut()
    {
        myAni.SetTrigger("Attack");
    }

    public void OnShootDone()
    {
        isShootAnimPlaying = false;
    }

    public void BeDamage(int damageValue)
    {

        // Debug.Log("BeDamage");
        //播放动画

        //受到伤害
        //现有血量= 现有血量-我的护甲的计算伤害（参数为：伤害值）
        CurrentHp -= myArmor.CalculateDamage(damageValue);
        //UI控制器下的ins变量的更新HP伤害值（参数为现有HP除以/最大HP）——为何要这么除
        UIManager.ins.ChangePlayerHp(currentHp, maxHp, hpBarPoint);



        //判断是否死亡
        if (currentHp == 0)
        {
            //假如现有HP等于0的时候吗死亡
            Dead();

        }
    }

    protected void Dead()
    {
        //死亡的时候游戏暂停
        Time.timeScale = 0;// 0 暂停 1 正常速度
        //UI控制器的ins变量下的打开游戏结束面板方法
        UIManager.ins.OpenGameOverPanel();
        //UI控制器的ins变量下的最前的游戏面板开关为false
        UIManager.ins.ToggleFrontPanel(false);
    }

    bool CheckGround()
    {
        //布尔类型 结果为false ——？
        bool result = false;
        //玩家身体的重力比例为5
        body.gravityScale = 5;
        //物理2D系统下的queriesStartInColliders 为 false
        Physics2D.queriesStartInColliders = false;

        // 在前方有没有地面——？
        bool frontHasGround = (Physics2D.Raycast(transform.position + Vector3.right * 0.53f, Vector2.down, Distance).transform != null);

        Debug.DrawLine(transform.position + Vector3.right * 0.53f, transform.position + Vector3.right * 0.53f + Vector3.down * Distance, Color.yellow);

        // 在后方有没有地面——？
        bool backHasGround = (Physics2D.Raycast(transform.position - Vector3.right * 0.53f, Vector2.down, Distance).transform != null);

        Debug.DrawLine(transform.position - Vector3.right * 0.53f, transform.position - Vector3.right * 0.53f + Vector3.down * Distance, Color.yellow);

        // 在正下方有没有地面——？
        bool midHasGround = (Physics2D.Raycast(transform.position, Vector2.down, Distance).transform != null);

        Debug.DrawLine(transform.position, transform.position + Vector3.down * Distance, Color.yellow);
        //假如 前方有地面 并且 后方有地面 并且 中间有地面
        if (frontHasGround || backHasGround || midHasGround)
        {
            //则 飞行方法为false
            isFlying = false;
            //输出结果为 true
            result = true;
            //玩家身体的重力比例为1
            body.gravityScale = 1;
        }
        //？
        Physics2D.queriesStartInColliders = true;

        Debug.Log("CheckGround:" + result);
        //返回值为 结果
        return result;
    }
    void BeginChangeStae(int newState)
    {
        //新状态的不同分支
        switch (newState)
        {
            //玩家变小状态
            case PLAYER_SMALL:
                //改变尺寸为2.5——方法名未声明如何得到的这个方法？
                StartCoroutine(ChangeSize(2.5f));
                break;
            //玩家变大状态
            case PLAYER_BIG:
                StartCoroutine(ChangeSize(10f));
                break;
            //玩家变正常状态
            case PLAYER_NORMAL:
                StartCoroutine(ChangeSize(5f));
                break;
            default:
                break;
        }
    }
    IEnumerator ChangeSize(float scale)
    {
        //while方法——？？
        while (transform.localScale != Vector3.one * scale)
        {
            Debug.Log("Big");
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * scale, Time.deltaTime * 5f);
            yield return null;
        }
    }
    //设置ID方法（参数为 字符串类型 id）
    public void SetEnglishName(string name)
    {
        // ??
        _englishName = name;
    }

    public void SetChineseName(string name)
    {
        _chineseName = name;
    }
}
