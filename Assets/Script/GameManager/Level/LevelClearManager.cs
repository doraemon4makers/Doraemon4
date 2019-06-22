using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearManager : MonoBehaviour
{
    private LevelClearBase[] levelClears;

    private void Awake()
    {
        levelClears = GetComponents<LevelClearBase>();
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        while (!IsAllClear())
        {
            yield return null;
        }
        Debug.Log("ExecuteEvent LevelClear");
        //EventManager.ExecuteEvent("LevelClear");
    }

    private bool IsAllClear()
    {
        bool isAllClear = true;
        if (levelClears == null || levelClears.Length <= 0) return false;

        foreach(LevelClearBase tempLevelClear in levelClears)
        {
            isAllClear = isAllClear && tempLevelClear.IsClear;
            if (!isAllClear)
            {
                break;
            }
        }
        return isAllClear;
    }

}
