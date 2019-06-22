using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearOnDestroy : LevelClearBase
{
    public LevelClearGameObjectInfo[] targetInfos;
    public string[] targetNames;
    //public string[] targetEnglishNames;
    //private GameObject target;

    protected override void Awake()
    {
        base.Awake();
        //StartCoroutine(AddComponentWhenFoundTarget());
        EventManager.RegisterEvent<GameObject>("Instantiated", OnInstantiated);
        //EventManager.RegisterEvent<IHasID>("Instantiated", OnInstantiated);
        EventManager.RegisterEvent("LevelClearTargetDestroyed", OnLevelClearTargetDestroyed);
    }

    private GameObject[] FindTargets()
    {
        List<GameObject> targets = new List<GameObject>();
        foreach (LevelClearGameObjectInfo info in targetInfos)
        {
            GameObject target = GameObject.Find(info.GetFullName());
            targets.Add(target);
        }

        //Item[] items = GameController.SceneItems.ToArray();
        //foreach(Item item in items)
        //{
        //    foreach(string englishName in targetEnglishNames)
        //    {
        //        if(englishName.Equals(item.englishName))
        //        {
        //            targets.Add(item.gameObject);
        //            break;
        //        }
        //    }
        //}
        return targets.ToArray();
    }

    private LevelClearDestroyTarget AddComponentInTarget(GameObject target)
    {
        if (target == null) return null;
        Debug.Log("Found Target");
        LevelClearDestroyTarget triggerTarget = target.GetComponent<LevelClearDestroyTarget>();
        if (triggerTarget == null)
        {
            triggerTarget = target.AddComponent<LevelClearDestroyTarget>();
        }
        Debug.Log("AddComponent");
        return triggerTarget;
    }

    //private IEnumerator AddComponentWhenFoundTarget()
    //{
    //    target = FindTarget();
    //    while (target == null)
    //    {
    //        target = FindTarget();
    //        yield return null;
    //    }
    //    AddComponentInTarget();
    //}

    private void FindAndAddComponent()
    {
        GameObject[] targets = FindTargets();
        foreach(GameObject target in targets)
        {
            AddComponentInTarget(target);
        }
    }

    private void OnInstantiated(GameObject ins)
    {
        IHasID hasID = ins.GetComponent<IHasID>();
        if(hasID != null)
        {
            string insEnglishName = hasID.englishName;
            foreach (LevelClearGameObjectInfo info in targetInfos)
            {
                if (info.isInsInRuntime && insEnglishName.Equals(info.GetFullName()))
                {
                    AddComponentInTarget(ins);
                }
            }
            //foreach (string englishName in targetEnglishNames)
            //{
            //    if (englishName.Equals(insEnglishName))
            //    {
            //        AddComponentInTarget(ins);
            //    }
            //}
        }
    }

    //private void OnInstantiated(IHasID ins)
    //{
    //    string insEnglishName = ins.englishName;
    //    foreach(string englishName in targetEnglishNames)
    //    {
    //        if (englishName.Equals(insEnglishName))
    //        {
    //            AddComponentInTarget(((Component)ins).gameObject);
    //        }
    //    }
    //}

    private void OnLevelClearTargetDestroyed()
    {
        isClear = true;
    }
}
