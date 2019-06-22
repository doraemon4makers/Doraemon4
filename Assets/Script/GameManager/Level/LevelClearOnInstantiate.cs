using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearOnInstantiate : LevelClearBase
{
    public enum TargetNameType
    {
        GameObjectName,
        ClassTypeName,
        InterfaceName
    }
    /// <summary>
    /// 指定目标名称的含义
    /// </summary>
    public TargetNameType type = TargetNameType.GameObjectName;
    /// <summary>
    /// 关注的名称
    /// </summary>
    public string[] targetNames;
    /// <summary>
    /// (使用Instantiate生成的)实例名字后缀()
    /// </summary>
    private const string INS_NAME_SUFFIX = "(Clone)";

    protected override void Awake()
    {
        base.Awake();
        EventManager.RegisterEvent<GameObject>("Instantiated", OnInstantiated);
    }

    private void OnInstantiated(GameObject ins)
    {
        switch (type)
        {
            case TargetNameType.GameObjectName:
                CaseGameObjectName(ins);
                break;
            case TargetNameType.ClassTypeName:
                CaseClassTypeName(ins);
                break;
            case TargetNameType.InterfaceName:
                CaseInterfaceName(ins);
                break;
        }
    }

    private void CaseGameObjectName(GameObject ins)
    {
        string insName = ins.name;
        insName = insName.Replace(INS_NAME_SUFFIX, "").Trim();
        foreach(string targetName in targetNames)
        {
            if (insName.Equals(targetName))
            {
                isClear = true;
                return;
            }
        }
    }

    private void CaseClassTypeName(GameObject ins)
    {
        MonoBehaviour[] monos = ins.GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour tempMono in monos)
        {
            foreach(string targetName in targetNames)
            {
                if(IsAssignableFrom(tempMono.GetType(), targetName))
                {
                    isClear = true;
                    return;
                }
            }
        }
    }

    private void CaseInterfaceName(GameObject ins)
    {
        MonoBehaviour[] monos = ins.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour tempMono in monos)
        {
            foreach(string targetName in targetNames)
            {
                if (tempMono.GetType().GetInterface(targetName) != null)
                {
                    isClear = true;
                    return;
                }
            }
        }
    }

    private static bool IsAssignableFrom(System.Type fromType, string toTypeName)
    {
        bool isAssignableFrom = false;
        if (fromType == null)
        {
            isAssignableFrom = false;
        }
        else if (fromType.Name.Equals(toTypeName))
        {
            isAssignableFrom = true;
        }
        else
        {
            isAssignableFrom = IsAssignableFrom(fromType.BaseType, toTypeName);
        }
        return isAssignableFrom;
    }
}
