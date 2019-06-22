using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZuHu : MonoBehaviour {

    public Dictionary<string, string> zuheDic = new Dictionary<string, string>();


    public virtual void zuhe(string id)
    {
        //Debug.Log("Do Zuhe");
        //Debug.Log("zuheDic.ContainsKey(id) = " + zuheDic.ContainsKey(id));
        if (zuheDic.ContainsKey(id))
        {
            //GameObject target = Resources.Load<GameObject>(zuheDic[id]);
            GameObject target = Item.GetPrefab(zuheDic[id]);

            target = Instantiate(target);

            target.transform.position = transform.position;

            Destroy(gameObject, 0.1f);

            gameObject.SetActive(false);

        }


    }
}
