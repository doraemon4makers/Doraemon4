using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour, IHasID, IDamagable
{
    public HpBar hpBar { get; set; }

    public bool isPlayerSide { get; set; }
    public Color oldColor;

    public string englishName { get { return _englishName; } }
    protected string _englishName;

    public string chineseName { get { return _chineseName; } }
    protected string _chineseName;

    public int score;
    public int maxHp;
    private int currentHp;
    protected Transform hpBarPoint;

    public GameObject debris;

    private static Dictionary<string, GameObject> itemDict;

    protected Rigidbody2D body;

    public static void InitItemDict()
    {
        itemDict = new Dictionary<string, GameObject>();

        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("/");
        Debug.Log("Prefab count:" + allPrefabs.Length);

        for(int i = 0; i < allPrefabs.Length; i++)
        {
            itemDict.Add(allPrefabs[i].name, allPrefabs[i]);
        }
    }

    public static GameObject GetPrefab(string name)
    {
        if(itemDict.ContainsKey(name))
        {
            return itemDict[name];
        }
        else
        {
            return null;
        }
    }

    public int CurrentHp
    {
        get
        {
            //?
            return currentHp;
        }

        set
        {
            //？？
            currentHp = Mathf.Clamp(value, 0, maxHp);
        }

    }

    private void Awake()
    {
        Debug.Log(GetType().Name + " instantiated");
        GameController.AddItem(this);
        Backpack.FoundItemInfo(this);
        Illustrative.Unlock(this);
    }

    protected virtual void Start()
    {
        //oldColor = GetComponent<SpriteRenderer>().color;
        hpBarPoint = transform.Find("HpBarPoint");
        CurrentHp = maxHp;
        debris = Resources.Load<GameObject>("Prefabs/Debris");
        ItemInfoHelper.SetNames(this);
    }

    public virtual void Use(Transform target)
    { 

    }

    public void ChangeColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void SetEnglishName(string name)
    {
        _englishName = name;
    }

    public void SetChineseName(string name)
    {
        _chineseName = name;
    }

    public virtual void BeDamage(int damageValue)
    {
        Debug.Log("物品受伤:" + damageValue);
        CurrentHp -= damageValue;

        GameController.instance.AddScore(score);

        EventManager.ExecuteEvent<GameObject, int, int>("ShowHp", gameObject, CurrentHp, maxHp);

        if (CurrentHp <= 0)
        {
            Destroy(gameObject);         
            Instantiate(debris, gameObject.transform.position, Quaternion.identity);

        }
    }

    private void OnDestroy()
    {
        GameController.RemoveItem(this);
    }
}
