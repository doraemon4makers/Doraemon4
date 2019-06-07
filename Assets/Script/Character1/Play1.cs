using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play1 : Player, IHasID, IDamagable, IGroupable
{
    public bool isPlayerSide { get; set; }

    private const float Distance = 1f;
    //受保护的 继承武器类的 现有武器变量
    protected Weapon1 currentWeapon;
    /// <summary>
    /// 护甲
    /// </summary>
    //受保护的 继承武器类的 我的护甲变量
    protected Armor1 myArmor;
    /// <summary>
    /// 最大生命值
    /// </summary>
    /// 公有的 整形最大HP
    public int maxHp;
    //私有的 整形现有血量
    private int currentHp;
    /// <summary>
    /// 现在的生命值
    /// </summary>
    //能被写入的变量-属性：既 保护的整形现有HP ——用于现有血量
    protected int CurrentHp
    {
        get
        {
            return currentHp;
        }

        set
        {
            //生命值 要小于等于最大生命值 大于等于0

            //if (value <= maxHp && value >= 0)
            //{
            //    currentHp = value;
            //}
            //else if (value > maxHp)
            //{
            //    currentHp = maxHp;
            //}
            //else if(value<0)
            //{
            //    currentHp = 0;
            //}

            // Mathf 数学方法 被写入了？ 
            currentHp = Mathf.Clamp(value, 0, maxHp);

            //if (currentHp == 0)
            //{
            //   // 死亡逻辑
            //}
        }
    }
    //这种做法是外部能读 ，内部能改
    public string englishName
    {
        get
        {
            return _englishName;
        }
    }

    public string chineseName
    {
        get
        {
            return _chineseName;
        }
    }

    public HpBar hpBar
    {
        get;

        set;
    }

    protected string _englishName;
    protected string _chineseName;

    //公有刚体变量，用于储存玩家移动和跳跃
    public Rigidbody2D body;
    //公有渲染精灵类 我的渲染变量
    public SpriteRenderer myRender;
    //用于存放跳跃时力度大小
    public float jump;
    //用于存放移动力的大小
    public float moveSpeed;
    //用于存放动画控制器组件变量
    protected Animator myAni;
    //私有的布尔型，判断是否在地面上，true-在地面，false-不在地面
    private bool isGround = true;
    //私有的布尔型 判断是否飞行中，true-在飞行，false-不在飞行
    private bool isFlying;
    //私有的整形 存放现在可以跳跃的次数，初始值为2
    private int currentJump = 2;

    //私有的 物体类型 枪变量 ——作用：
    private GameObject gun;
    //公有的 位置类型 开火点变量——作用：储存开火点位置
    public Transform firePoint;
    //私有的 物体类型 敌人变量——存储敌人数据
    private GameObject enemy;
    // 公有的 整形 玩家 大=1，中=2，小=0的常量 ——存储玩家三种状态
    public const int PLAYER_SMALL= 0;
    public const int PLAYER_BIG = 1;
    public const int PLAYER_NORMAL = 2;
    //私有 UI管理类的 ui变量_存储ui的数据
    private UIManager ui;
    //私有 碰撞体类 col 变量——存储玩家碰撞体数据
    private Collider2D col;

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
        ui.ChangePlayerHp(currentHp, maxHp,hpBarPoint);

        hpBarPoint = transform.Find("HpBarPoint");

        //SetEnglishName("Doraemon");
        //ReadWordDictionary.SetChineseAndEnglishName(this);

    }

    void Update()
    {
        Debug.Log("不能走了0");
        // 如果 玩家 进入驾驶状态  return 后就不作为了。
        if (isDriving)// if（）括号里的就是个布尔值，因此这句话的意思是 假如 isDriving  为true的时候 return
            return;
        Debug.Log("不能走了1");
        //if (isFly)
        //    return;

        //isGround存储检测地面的方法
        isGround = CheckGround();

        //horizontal 存储 输入Horizontal的X轴移动参数
        float horizontal = Input.GetAxis("Horizontal");
        // vertical存储 输入Vertical的Y轴移动参数
        float vertical = Input.GetAxis("Vertical");

        if(isFly && vertical > 0.5f)
        {
            isFlying = true;
        }

        Debug.Log("不能走了2");
        //如果是在地面则现有跳跃状态为2，我的动画为跳跃的状态=false
        if (isGround)
        {
            Debug.Log("不能走了3");
            currentJump = 2;
            myAni.SetBool("Jump", false);
        }
        Debug.Log("不能走了4");
        //Debug.Log(horizontal);


        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("不能走了5");

        }

        //如果当按下J键按钮时，并且 currentJump大于等于1时，则玩家body的速度为，2D向量向上*跳跃+body速度*x轴*向右的向量
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("不能走了6");
            if (currentJump >= 1)
            {
                Debug.Log("J");
                body.velocity = Vector2.up * jump + body.velocity.x * Vector2.right;
                //此时我的为Jump的动画为true
                myAni.SetBool("Jump", true);
                //currentJump变量减一次
                currentJump--;
                Debug.Log("不能走了7");
            }
        }
        //当按下Y键时，ui执行加分方法
        //if (Input.GetKeyDown(KeyCode.Y)) ui.AddScore();
        //如果x轴方向与y轴方向的移动量不为0
        if (horizontal != 0||vertical !=0)
        {
            //则让移动的动画设为true
            myAni.SetBool("Move", true);
            //如果枪的值为空，则枪的变量为 子对象的位置
            if (gun == null)
                gun = transform.GetChild(0).gameObject;

            if (gun.activeSelf)
                gun.SetActive(false);
            //如果(horizontal< 0则执行水平位置向左
            if (horizontal < 0)
            {
                transform.right = Vector3.left;
            }
            //否则(horizontal< 0则执行水平位置向右
            else
            {
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
        //当按下U键则执行  isFlyin 为 true，而且body的重力比例为0
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
        ////当按下B的时候则执行开始改变玩家状态为BeginChangeStae（PLAYER_BIG）的方法
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    BeginChangeStae(PLAYER_BIG);
        //}
        ////当按下M的时候则执行开始改变玩家状态为BeginChangeStae（PLAYER_SMALL）的方法
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    BeginChangeStae(PLAYER_SMALL);
        //}
        ////当按下N的时候则执行开始改变玩家状态为BeginChangeStae（PLAYER_NORMAL）的方法
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    BeginChangeStae(PLAYER_NORMAL);
        //}

        //假如玩家在地面并且按下鼠标右键则执行Shoot（)的方法
        if (isGround && Input.GetMouseButton(1)) //鼠标左键 为0 鼠标右键为1
        {
            Shoot();
        }

        //冷却计时！！！！
        timer += Time.deltaTime;// timer = timer + Time.deltaTime; ——？？
    }

    //?? 
    float timer;

    protected void Shoot()
    {
        //假如现有武器冷却完毕 
        if (currentWeapon.IsColdDown())
        {
            //重新开始冷却计时！！！
            //timer = 0;

            //开火点对象显示开火火花
            firePoint.gameObject.SetActive(true);
            //播放射击动画
            myAni.SetTrigger("Shoot");
            //产生子弹
            //Debug.Log("生成子弹");

            Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position,5f);
            for(int i = 0; i < enemys.Length; i++)
            {
                if( enemys[i].gameObject.tag == "enemy")
                {
                    enemy = enemys[i].gameObject;
                    break;
                }
            }


            if (enemy != null)
            {
                //现有武器获取瞄准方法（参数为敌人的自身位置）
                
                currentWeapon.Aim(enemy.transform.position);
                currentWeapon.Fire(firePoint);
            }
            else
            {
                Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                v = Vector3.Scale(v, Vector3.right + Vector3.up);
                currentWeapon.Aim(v);
                currentWeapon.Fire(firePoint);
                //currentWeapon.Aim(transform.position + transform.right);
            }
            //现有武器的开火方法（开火点）——如何被调用的，作用是什么？
            //currentWeapon.Fire(firePoint);
           
        }
    }


    //private void OnTriggerEnter2D(Collider2D collision)//进入碰撞时
    //{
    //    //判断碰到的物体
    //    //判断是否落在地上
    //    if (collision.CompareTag("Ground") && body.velocity.y <= 0)// collision.gameObject.tag == "Ground"
    //    {
    //        isGround = true;
    //        myAni.SetBool("Jump", false);
    //        currentJump = 0;
    //    }
    //}
    //被伤害方法
    public void BeDamage(int damageValue)
    {

       // Debug.Log("BeDamage");
        //播放动画

        //受到伤害
        //现有血量= 现有血量-我的护甲的计算伤害（参数为：伤害值）
        CurrentHp -= myArmor.CalculateDamage(damageValue);
        //UI控制器下的ins变量的更新HP伤害值（参数为现有HP除以/最大HP）——为何要这么除
        UIManager.ins.ChangePlayerHp(currentHp, maxHp,hpBarPoint);



        //判断是否死亡
        if (currentHp == 0)
        {
            //假如现有HP等于0的时候吗死亡
            Dead();

        }
    }


    //死亡方法
    protected void Dead()
    {
        //死亡的时候游戏暂停
        Time.timeScale = 0;// 0 暂停 1 正常速度
        //UI控制器的ins变量下的打开游戏结束面板方法
        UIManager.ins.OpenGameOverPanel();
        //UI控制器的ins变量下的最前的游戏面板开关为false
        UIManager.ins.ToggleFrontPanel(false);
    }

    //地面射线检测方法
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
    // 开始改变状态
    void BeginChangeStae(int newState)
    {
       //新状态的不同分支
        switch(newState)
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
        while(transform.localScale != Vector3.one * scale)
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

    //protected void Move(float hor)
    //{
    //    if (hor != 0) //hor = 0 没有按下按钮  按下左键 -1~0  按下右键0~1
    //    {


    //        myAni.SetBool("Move", true);

    //        //移动 需要一个向量  速度数值 速度方向
    //        // 保留竖直方向的速度
    //        myRig.velocity = moveSpeed * hor * Vector2.right + Vector2.up * myRig.velocity.y;
    //    }
    //    else
    //        myAni.SetBool("Move", false);
    //}

    private void OnDrawGizmos()
    {
        
    }
}
