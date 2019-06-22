using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class Touch : MonoBehaviour,IPointerClickHandler,IDragHandler,IEndDragHandler,IBeginDragHandler

{
    public Item item;



    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("drag:" + gameObject.name);
        GameObject go = Resources.Load<GameObject>("");
        GameObject[] goArray = Resources.LoadAll<GameObject>("");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        gameObject.AddComponent<Rigidbody2D>();

        //
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = Color.white;
        }

        //显示删除图标

        //拿到 UIManager 调用显示图标的方法

        UIManager.ins.ShowDeleteIcon();
        Debug.Log("drag end:" + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();

        if(rigidbody !=null)
        {
            //rigidbody.gravityScale = 0;
            rigidbody.isKinematic = true;
        }

        transform.position = Camera.main.ScreenToWorldPoint( eventData.position) + new Vector3(0,0,10);
    }

    public void OnEndDrag(PointerEventData eventData)

    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

        // 结束拖拽 时 那个点上的游戏对象
        GameObject target = eventData.pointerCurrentRaycast.gameObject;

        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();

        if (rigidbody != null)
        {
            //rigidbody.gravityScale = 1;
            rigidbody.isKinematic = false;
            rigidbody.freezeRotation = true;
        }

        // Debug.Log(target.name);

        //
        if (target != null && target.tag == "Player")
        {
            //item.Use(target.transform.Find("gun"));
        }

        // 组 合 
        //temp 中间值
        ZuHu temp = null;

        if (target != null)
        temp = target.GetComponent<ZuHu>();

        if (temp != null) //结束点上的物品可以被组合
        {
            temp.zuhe(item.englishName);
            Destroy(gameObject);

        }

        //判断 target 是否为空
        //不为空 判断target 的标签是否 是 删除图标 的标签 “shanchu”
        //是 Destroy（gameObject）；

        //如果目标不为空
        if (target != null)
        {
            //且目标标签等于 损伤
            if (target.tag == "deface")
            {
               //则摧毁对象自身
                Destroy(gameObject);
            }
        }

    UIManager.ins.HideDeleteIcon();
    }

    //                         按键点击事件      事件数据
    public void OnPointerClick(PointerEventData eventData)
    {
        //对象获取刚体组件
        gameObject.AddComponent<Rigidbody2D>();
        //对象获取精灵渲染组件下的白颜色
        // gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = Color.white;
        }
    }
}
