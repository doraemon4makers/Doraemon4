using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearOnEnter : LevelClearBase
{
    public LevelClearGameObjectInfo[] targetInfos;
    public LevelClearGameObjectInfo[] triggerInfos;
    //public string[] targetEnglishNames;
    //public string[] triggerEnglishNames;

    private GameObject target;

    protected override void Awake()
    {
        base.Awake();
        //StartCoroutine(AddComponentWhenFoundTarget());
        EventManager.RegisterEvent<GameObject>("Instantiated", OnInstantiated);
        EventManager.RegisterEvent("LevelClearTargetEntered", OnLevelClearTargetEntered);
        EventManager.RegisterEvent("LevelClearTargetExited", OnLevelClearTargetExited);
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
        //foreach (Item item in items)
        //{
        //    foreach (string englishName in targetEnglishNames)
        //    {
        //        if (englishName.Equals(item.englishName))
        //        {
        //            targets.Add(item.gameObject);
        //            break;
        //        }
        //    }
        //}
        return targets.ToArray();
    }

    private LevelClearEnterTarget AddComponentInTarget(GameObject target)
    {
        if (target == null) return null;
        //Debug.Log("Found Target");
        LevelClearEnterTarget triggerTarget = target.GetComponent<LevelClearEnterTarget>();
        if (triggerTarget == null)
        {
            triggerTarget = target.AddComponent<LevelClearEnterTarget>();
        }
        string[] fullTriggerNames = new string[triggerInfos.Length];
        for (int i = 0; i < triggerInfos.Length; i++)
        {
            fullTriggerNames[i] = triggerInfos[i].GetFullName();
        }
        triggerTarget.triggerNames = fullTriggerNames;
        //triggerTarget.triggerEnglishNames = triggerEnglishNames;
        //Debug.Log("AddComponent");
        return triggerTarget;
    }

    private void FindAndAddComponent()
    {
        GameObject[] targets = FindTargets();
        foreach (GameObject target in targets)
        {
            AddComponentInTarget(target);
        }
    }

    private void OnInstantiated(GameObject ins)
    {
        IHasID hasID = ins.GetComponent<IHasID>();
        if (hasID != null)
        {
            string insName = ins.name;
            foreach (LevelClearGameObjectInfo info in targetInfos)
            {
                if (info.isInsInRuntime && insName.Equals(info.GetFullName()))
                {
                    AddComponentInTarget(ins);
                }
            }
            //string insEnglishName = hasID.englishName;
            //foreach (string englishName in targetEnglishNames)
            //{
            //    if (englishName.Equals(insEnglishName))
            //    {
            //        AddComponentInTarget(ins);
            //    }
            //}
        }
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

    private void OnLevelClearTargetEntered()
    {
        isClear = true;
    }

    private void OnLevelClearTargetExited()
    {
        isClear = false;
    }
}
