using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorTool : MonoBehaviour
{
    [MenuItem("Tools/SpriteToPrefab", false,1)]
    public static void SpriteToPrefab()
    {
        string spritesPath = "Sprites";
        string savePath = Application.dataPath + "/Resources/Prefabs/AutoSpwan";
        //string savePath = Application.dataPath + "/Resources/Prefabs/AutoSpwan";
        //string savePath = "Resources/Prefabs/AutoSpwan";
        Sprite[] sprites = Resources.LoadAll<Sprite>(spritesPath);
        if(sprites == null)
        {
            return;
        }
        for(int i = 0; i < sprites.Length; i++)
        {
            GameObject tempGO = new GameObject(sprites[i].name);
            tempGO.AddComponent<SpriteRenderer>().sprite = sprites[i];
            PrefabUtility.SaveAsPrefabAsset(tempGO, savePath + "/" + tempGO.name + ".prefab");
            DestroyImmediate(tempGO);
        }
        AssetDatabase.Refresh();
    }
}
