using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesLoadManager
{
    private static Dictionary<Type, Dictionary<string, object>> typeDicPairs = new Dictionary<Type, Dictionary<string, object>>();
    //private static Dictionary<string, object> nameResourcesPairs = new Dictionary<string, object>();

    private ResourcesLoadManager() { }

    public static T LoadResource<T>(string path) where T : UnityEngine.Object
    {
        T resource = Resources.Load<T>(path);
        if (resource != null)
        {
            // 添加到字典
            SaveResource(resource.name, resource);
        }
        return resource;
    }

    public static T[] LoadResources<T>(string path) where T : UnityEngine.Object
    {
        T[] resources = Resources.LoadAll<T>(path);
        foreach(T temp in resources)
        {
            SaveResource(temp.name, temp);
        }
        return resources;
    }

    public static object GetResource(Type resourceType, string resourceName)
    {
        object result = null;
        if (typeDicPairs.ContainsKey(resourceType))
        {
            Dictionary<string, object> nameResourcesPairs = typeDicPairs[resourceType];
            if (nameResourcesPairs.ContainsKey(resourceName))
            {
                // 调出资源
                result = typeDicPairs[resourceType][resourceName];
            }
        }
        return result;
    }

    public static T GetResource<T>(string resourceName) where T:UnityEngine.Object
    {
        Type resourceType = typeof(T);
        return (T)GetResource(resourceType, resourceName);
    }

    public static void SaveResource<T>(string resourceName, T resource) where T : UnityEngine.Object
    {
        Type resourceType = resource.GetType();
        if (!typeDicPairs.ContainsKey(resourceType))
        {
            typeDicPairs.Add(resourceType, new Dictionary<string, object>());
        }
        Dictionary<string, object> nameResourcesPairs = typeDicPairs[resourceType];
        if (!nameResourcesPairs.ContainsKey(resourceName))
        {
            nameResourcesPairs.Add(resourceName, resource);
        }
    }
}
