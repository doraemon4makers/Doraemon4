using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitUI : MonoBehaviour

{

    public void OnStartClicked()
    {
        SceneManager.LoadScene(0);

    }

    public void RPG()
    {

        Debug.Log("冒险模式");

    }



}
