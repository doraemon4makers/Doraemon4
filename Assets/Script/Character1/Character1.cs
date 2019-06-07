using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class Character1 : MonoBehaviour, IHasID, IDamagable
{
    public bool isPlayerSide { get; set; }

    //公开的布尔类型的无敌状态变量为false
    public bool invincible = false;
    //保护的 武器类的 现持武器变量
    protected Weapon1 currentWeapon;
    //保护的 护甲类的 我的护甲变量
    protected Armor1 myArmor;
    //公有的整形 最大HP变量
    public int maxHp;
    //私有的整形 现有血量
    private int currentHp;
    //公有的现有血量属性
    public int CurrentHp
    {
        get
        {
            //做一个私有的currentHp，是给自己内部调用，返回一个currentHp私有值给CurrentHp属性是为了让外部能看到血量
            return currentHp;
        }

        set
        {
            //设置时也给了个规则，这个规则是0到最大生命值的 Clamp 象限图范围， value是指代外部的人给我指定的那个值
            currentHp = Mathf.Clamp(value, 0, maxHp);
        }

    }
    //？？ id的属性
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
    //受保护的 动画管理类 我的动画变量
    protected Animator myAni;
    //受保护刚体类我的刚体
    protected Rigidbody2D myRig;
   //受保护 碰撞体类 我的碰撞体
    protected Collider2D myCol;
    //公有 位置类 开火点位置变量
    public Transform firePoint;
    // 公有 浮点型 移动速度变量
    public float moveSpeed;
    //保护 精灵渲染类 我的渲染
    protected SpriteRenderer myRender;

    protected void Awake() // 游戏对象初始化
    {
        OnAwake();
    }
    //受保护 初始化的 虚方法 
    protected virtual void OnAwake()
    {
        //我的动画获取动画管理器组件子对象
        myAni = GetComponentInChildren<Animator>();
        //我的刚体获取刚体组件
        myRig = GetComponent<Rigidbody2D>();
        //我的碰撞体获取碰撞体组件
        myCol = GetComponent<Collider2D>();
        //我的渲染器获取组件
        myRender = GetComponent<SpriteRenderer>();
        //现有HP为最大血量
        CurrentHp = maxHp;
        //现有武器 实例 新的枪的开火点—— 为何要这样实例化 用处是？
        currentWeapon = new Gun(firePoint, isPlayerSide);
    }

    private void Update()
    {
        DoInGame();
    }

    //游戏过程逻辑
    protected abstract void DoInGame();

    protected void Move(float hor)
    {
        if (hor != 0) //hor = 0 没有按下按钮  按下左键 -1~0  按下右键0~1
        {
            //则执行turn方法
            Turn(hor);
            //我的动画执行巡逻为true
            myAni.SetBool("Patrol", true);

            //移动 需要一个向量  速度数值 速度方向
            // 保留竖直方向的速度
            myRig.velocity = moveSpeed * hor * Vector2.right + Vector2.up * myRig.velocity.y;
        }
        else
            //否则则不执行巡逻动画
            myAni.SetBool("Patrol", false);
    }

    protected void Turn(float hor)
    {
        // myRender下的这个flipX（就是Render面板下 flip 勾选框）属性赋值，赋值内容为 hor 的比较结果。
        myRender.flipX = hor > 0;//2>1

        //if (!myRender.flipX)
        //{                             实例化一个二维向量（参数为：负数的 Mathf类里的 Abs（绝对值的求值）， 本地位置的y点）
        //    firePoint.localPosition = new Vector2(-Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y);
        //}
        //else
        //{
        //    firePoint.localPosition = new Vector2(Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y);
        //}
        //下面那段话就是上面的那个判断语句的简写版本            
        firePoint.localPosition = !myRender.flipX ? new Vector2(-Mathf.Abs(firePoint.localPosition.x),
        firePoint.localPosition.y) : new Vector2(Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y);
       // firePoint.GetComponentInChildren<SpriteRenderer>().flipX = myRender.flipX;
    }


    protected void Shoot()
    {

        //if (!currentWeapon.IsColdDown())
        //{
        //    firePoint.gameObject.SetActive(false);
        //    return;
        //}
        //currentWeapon.
        //找到开火点，在开火点处生成子弹实例

        //currentWeapon.StartColdDown();

        //显示开火火花
       // firePoint.gameObject.SetActive(true);
        //播放射击动画
        myAni.SetTrigger("attack");
        //产生子弹
       // Debug.Log("生成子弹");
        //currentWeapon.Fire(firePoint);
    }

    public virtual void BeDamage(int damageValue)
    {
        //播放动画

        //收到伤害

        if (!invincible)
        {
            //现有HP 为 现有HP - 我的护甲的血量计算方法（血量值）
            CurrentHp -= damageValue;
            //如果现有血量小于等于0，则执行死亡方法
            if (CurrentHp <= 0)
            {
                Dead();
            }
        }

    }
    //
    protected virtual void ShowHp()
    {

    }

    //抽象方法

    protected abstract void Dead(); // 因为非抽象类可以实例 当实例的时候 抽象方法无意义  所以抽象方法只能在抽象类中——？？为何用抽象方法

    //——用于显示ID名字 这个方法就是给变量赋值
    public void SetEnglishName(string name) // 方法内的一些变量，自身不能决定，或者是想留给外部决定的，就做成一个输入参数（入参）
    {
        //this 指代这个类本身 前面这个_id是这个类里的变量， 后面这个id 是我定义的这个方法的输入参数，外面的人要调用这个方法也要给一个参数
        _englishName = name;
    }

    public void SetChineseName(string name)
    {
        _chineseName = name;
    }
}




