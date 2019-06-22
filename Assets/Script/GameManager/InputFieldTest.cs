using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldTest : MonoBehaviour
{
    // public Dictionary<string,GameObject> objectPool=new Dictionary<string, GameObject>();

    // public string path = "weapons/";

    public GameObject InputPanel;

    public Vector3 insPos;

    string targetValue;

    public GameObject lastObj;


    public void OnvalueEditEnd(InputField input)
    {
        targetValue = input.text.ToLower();
    }

	//Ina 实例的 单词缩写
    public void InsGameObject()
    {
        GameController.Resume();

        GameObject targetPerfab = Item.GetPrefab(targetValue);

        if(targetPerfab == null)
        {
            Debug.Log("输入字符匹配");

            FindFunction(targetValue);
            return;
        }

        GameObject objIns = Instantiate(targetPerfab);
        objIns.transform.position = GameObject.FindWithTag("Player").transform.position + Vector3.up * 3+new Vector3(0,0,-1);

        lastObj = objIns;
        EventManager.ExecuteEvent("Instantiated", objIns);

        InputPanel.SetActive(false);
    }

    public void FindFunction(string functionName)
    {
        if (functionName.Contains("ChangeColor"))
        {
            string targetColor = functionName.Replace("ChangeColor", "");

            Color color = Color.white;

            switch (targetColor)
            {
                case "Red":
                    color = Color.red;
                        break;
                default:
                    Debug.LogError("没有该颜色");
                    break;
            }

            lastObj.GetComponent<Touch>().item.ChangeColor(color);
        }
    }

}
