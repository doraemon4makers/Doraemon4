using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public float displayTime = 1f;
    private GameObject followTarget;
    private Transform hpBarPoint;
    private Image[] hpIcons;

    // Start is called before the first frame update
    void Start()
    {
        hpIcons = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hpBarPoint)
        {
            transform.position = hpBarPoint.position;
        }
    }

    public void SetFollowTarget(GameObject newTarget)
    {
        if(hpIcons == null) hpIcons = GetComponentsInChildren<Image>();

        followTarget = newTarget;
        hpBarPoint = followTarget.transform.Find("HpBarPoint");
        
        transform.position = hpBarPoint.position;

        followTarget.GetComponent<IDamagable>().hpBar = this;
        EventManager.RegisterEvent<int, int>(followTarget, "ChangeHp", OnChangeHp);
        //Invoke("HideHpBar", displayTime);
    }

    private void OnEnable()
    {
        CancelInvoke();
    }

    private void OnDisable()
    {
        EventManager.UnregisterEvent<int, int>(followTarget, "ChangeHp", OnChangeHp);
    }

    private void HideHpBar()
    {
        if (followTarget)
        {
            followTarget.GetComponent<IDamagable>().hpBar = null;
        }

        gameObject.SetActive(false);
    }

    public void OnChangeHp(int currentHp, int maxHp)
    {
        Debug.Log("ChangeHp:" + currentHp + "/" + maxHp);

        SetMaxHp(hpIcons, maxHp);
        SetCurrentHp(hpIcons, currentHp);
        CancelInvoke();
        Invoke("HideHpBar", displayTime);
    }

    private void SetMaxHp(Image[] hpIcons, int maxHp)
    {
        for (int i = 0; i < hpIcons.Length; i++)
        {
            if (i < maxHp)
            {
                hpIcons[i].gameObject.SetActive(true);
            }
            else
            {
                hpIcons[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetCurrentHp(Image[] hpIcons, int currentHp)
    {
        for (int i = 0; i < hpIcons.Length; i++)
        {
            if (i < currentHp)
            {
                hpIcons[i].enabled = true;
            }
            else
            {
                hpIcons[i].enabled = false;
            }
        }
    }

}
