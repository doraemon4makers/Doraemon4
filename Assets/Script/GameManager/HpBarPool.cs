using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarPool : MonoBehaviour
{
    public int startPoolSize = 10;
    private GameObject hpBarPrefab;
    private List<GameObject> hpBars = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        hpBarPrefab = Resources.Load<GameObject>("Prefabs/HpBar");

        for(int i = 0; i < startPoolSize; i++)
        {
            SpawnNewBar();
        }

        EventManager.RegisterEvent<GameObject>("AssignHpBar", OnAssignHpBar);
    }

    private GameObject SpawnNewBar()
    {
        GameObject bar = Instantiate(hpBarPrefab, transform.position, Quaternion.identity);
                       
        bar.transform.SetParent(transform);

        bar.SetActive(false);
        hpBars.Add(bar);

        return bar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAssignHpBar(GameObject followTarget)
    {
        GameObject bar = GetHpBar();
        bar.GetComponent<HpBar>().SetFollowTarget(followTarget);
        bar.SetActive(true);
    }

    GameObject GetHpBar()
    {
        GameObject ret = null;

        for(int i = 0; i < hpBars.Count; i++)
        {
            if(hpBars[i].activeSelf == false)
            {                
                ret = hpBars[i];
            }
        }

        if (ret == null) ret = SpawnNewBar();
        return ret;
    }
}
