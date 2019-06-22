using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelClearGameObjectInfo
{
    private const string RUNTIME_INS_SUFFIX = "(Clone)";
    public string name;
    public bool isInsInRuntime;

    public string GetFullName()
    {
        string fullName = string.Empty;
        fullName = name + (isInsInRuntime ? RUNTIME_INS_SUFFIX : "");
        return fullName;
    }
}
