using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearBase : MonoBehaviour
{
    public bool IsClear { get { return isClear; } }
    protected bool isClear = false;

    protected virtual void Awake()
    {
        AddLevelClearManager();
    }
    
    private void AddLevelClearManager()
    {
        LevelClearManager manager = GetComponent<LevelClearManager>();
        if(manager == null)
        {
            gameObject.AddComponent<LevelClearManager>();
        }
    }
}
