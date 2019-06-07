using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Character1
{
    public string dropPrefabName;
    //公有的计分变量 
    public int score = 100;

    //公有的浮点型 视察范围为4.75 变量
    public float viewRange = 4.75f;
    //私有的 方位类 巡逻点1
    private Vector3 patrolPos1;
    //私有的 方位类 巡逻点2
    public Vector3 patrolPos2;
    //私有的布尔类  可见玩家 变量
    private bool canSeePlayer;
    //私有的 对象类 玩家变量
    private GameObject player;
    //私有的 布尔类   is开火点变量 为 true
    private bool isFirstPos = true;
    //公有的 颜色类 trackColor变量 为 白色
    public Color trackColor = Color.white;
    //？？
    private List<Vector3> tracks = new List<Vector3>();
    //私有的 位置类 设计位置变量
    private Transform shotPos;
    //公有的 布尔 掉武器变量 为 true
    public bool dropWeapon = true;
    //私有的对象类  掉落武器图片变量
    private GameObject dropWeaponPrefab;

    private Transform hpBarPoint;

    //初始时调用初始化方法 ——套一层是为了方便阅读
    protected new void Awake()
    {
        OnAwake();
    }


    protected override void OnAwake() // 游戏对象初始化——Awake 游戏流程 就是游戏开始的时候 先调用OnAwake
    {
        //为何一开始就区分阵营  ？一开始 给一个初始值 
        isPlayerSide = false;
        hpBarPoint = transform.Find("HpBarPoint");

        //指代父类里的同名方法，调用的是父类的方法 base 一般情况下是指代父类 只是上一级的父类
        base.OnAwake();
        myAni = GetComponentInChildren<Animator>();
        myRender = transform.Find("Body").GetComponent<SpriteRenderer>();
        patrolPos1 = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        canSeePlayer = false;
        // 构造函数也是做初始化工作的一个函数  
        myArmor = new Armor1(0);

        shotPos = transform;
        //掉落武器 为  在Resources文件夹中加载对象名为robot 的对象
        dropWeaponPrefab = Resources.Load<GameObject>(dropPrefabName);
        //SetEnglishName("Enemy");
        //ReadWordDictionary.SetChineseAndEnglishName(this);
    }


    protected override void DoInGame()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BeDamage(1);
        }

        //如果不可见玩家
        if (!CanSeePlayer())
        {
            //则执行巡逻方法
            Patrol();
        }
        else
        {
            //否则执行攻击方法
            Stop();
            Attack();
            // time = time + Time.deltaTime

            //        每一帧跟上一帧的间隔时间
            timer += Time.deltaTime;
        }

        //1. 通过触发器 Trigger 撞到玩家  2. 通过距离
    }

    void Stop()
    {
        myRig.velocity = Vector2.zero;
        myAni.SetBool("Patrol", false);
    }

    //攻击方法
    void Attack()
    {
        //面向玩家 如果 玩家的x轴位置 大于 自身的X轴位置
        if (player.transform.position.x > transform.position.x)
        {
            //则执行turn方法（参数为1）
            Turn(1);
        }
        // 并且如果 玩家的x轴位置 小于 自身的X轴位置
        else if (player.transform.position.x < transform.position.x)
        {
            //则执行turn方法（参数为-1）
            Turn(-1);
        }
        //如果timer 大于 现有武器的开火 频率
        if (timer > currentWeapon.FireSpeed)
        {
            //则 timer 为 0 
            timer = 0;
            //执行攻击方法
            Shoot();
            // 为何 用减法 就是直线——这个跟高中数学有关，就是向量 方向 加上  A-B 就是向量
            Vector3 temp = (player.transform.position - firePoint.position).normalized;
            //现有武器的瞄准方法（玩家的位置参数）
            currentWeapon.Aim(player.transform.position);
            //现有武器的开火方法（开火点参数）
            currentWeapon.Fire(firePoint);
        }
    }
    // timer 做记录——记录上一次的某一个时间
    float timer;
    //巡逻方法
    void Patrol()
    {
        //判断自己在哪个点
        //获得目标点
        // 方向类 目标变量为 ——target =  isFirstPos 
        Vector2 target = isFirstPos ? patrolPos2 : patrolPos1;
        //Vector2 target; ——为上一句话的翻译
        //if(isFirstPos)
        //{
        //    target = patrolPos2;

        //}
        //else
        //{
        //    target = patrolPos1;

        //}

        //判断自己与目标点的距离
        //如果 距离 （自身位置 x1 目标 x2） 之间小于等于 0.3
        if (Vector2.Distance(transform.position, target) <= 0.3f)//到达目标点
        {
            Debug.Log("到达");
            //转换目标点
            //则 第一个点 等以非第一个点——给这个值取反
            isFirstPos = !isFirstPos;

        }
        //hor 为 目标x点 大于等于自身x点 恰好定了一个一样的 hor 名字
        float hor = target.x >= transform.position.x ? 1 : -1;
        //之后进行 移动方法（参数为hor）
        Move(hor);
    }
    //私有的布尔 可见玩家方法
    private bool CanSeePlayer()
    {
        // 刚体数组 刚体变量 为   2D物理类下的OverlapCircleAll？ （参数为 自身位置， 可视范围）
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, viewRange);
        //？？
        foreach(Collider2D c in cols)
        {
            //玩家1类 的玩家变量 获得玩家1的子对象
            Play1 player = c.GetComponentInChildren<Play1>();
            //如果玩家
            if(player)
            {
                // 返回true
                return true;
            }
        }
        //否则关闭
        return false;
    }

    //死亡方法
    protected override void Dead()
    {
        //播放动画 
        //死亡后播放死亡动画
        myAni.SetTrigger("Dead");
        //进入死亡状态
        //关闭碰撞体 
        //游戏对象 隐藏 gameObject.SetActive(false); 显示 gameObject.SetActive(true); 前提场景中有这游戏对象
        //组件  enabled 属性
        //我的碰撞体为 可动开关 为 false
        myCol.enabled = false;
        //我的刚体 动力学刚体 为 true
        myRig.isKinematic = true;
        //我的刚体速度 为 0
        myRig.velocity = Vector2.zero;
        //销毁自身
        //Destroy(gameObject, 1);
        //销毁类 dot ——？？ 
        DestroyOnTime1 dot = gameObject.AddComponent<DestroyOnTime1>();
        //调用dot的time 参数 为1
        dot.time = 1;

        //如果掉落武器
        if (dropWeapon)
        {
            //则实例化（参数为：掉落武器图片，自身位置， ？？ 身份？？）
            Instantiate(Item.GetPrefab(dropPrefabName), transform.position, Quaternion.identity);

        }
        //Destroy(gameObject);

        //生成宝物，宝物带触发器，触发器识别玩家，并提交任务，销毁自身。
    }

    //获取瞄准方向方法（参数为目标位置）
    Vector3 GetAimDirection(Vector3 targetPosition)
    {
        // 方向 瞄准方向为 （目标位置- 设计位置） 归一化
        Vector3 aimDirection = (targetPosition - shotPos.position).normalized;
        //返回 目标方向值
        return aimDirection;
    }

    //被伤害的虚方法（参数为 伤害值）
    public override void BeDamage(int damageValue)
    {
        //？？
        base.BeDamage(damageValue);
        
        GameController.instance.AddScore(score);

        // 需要显示血条时执行
        EventManager.ExecuteEvent<GameObject, int, int>("ShowHp", gameObject, CurrentHp, maxHp);
    }
    // 私有的画出线的方法
    private void OnDrawGizmos()
    {
        //
        Gizmos.color = trackColor;
        //??
        for(int i = 0;i < tracks.Count;i++)
        {

            Gizmos.DrawSphere(tracks[i], 0.1f);

        }
        //如果 设计位置不为空， 玩家不为空
        if(shotPos !=null && player !=null)
        {
            //则知悉 颜色为白色
            Gizmos.color = Color.white;
            //画出白线（参数为射击点位置，射击点位置加上获取瞄准点位置（参数为玩家位置）*5）
            Gizmos.DrawLine(shotPos.position,shotPos.position+GetAimDirection(player.transform.position)*5);
        }
    }

    public void Die()
    {

    }
}

