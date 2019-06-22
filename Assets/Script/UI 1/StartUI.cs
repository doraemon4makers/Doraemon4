using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartUI : MonoBehaviour {

    //public GameObject illustrativePanel;

    public void OnStartClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");

    }

    public void RPG()
    {

        Debug.Log("冒险模式");

    }

    public void Play()
    {

    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    //public void OpenItemInfo(Button clickedButton)
    //{

    //}

    //public void Quit(GameObject ownerRootPanel)
    //{
    //    ownerRootPanel.SetActive(false);
    //}

}
