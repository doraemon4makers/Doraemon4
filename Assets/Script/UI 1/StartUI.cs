using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartUI : MonoBehaviour {

    public void OnStartClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");

    }

    public void RPG()
    {

        Debug.Log("冒险模式");

    }

}
