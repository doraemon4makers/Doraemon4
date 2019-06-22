using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearDestroyTarget : MonoBehaviour
{
    private void OnDestroy()
    {
        EventManager.ExecuteEvent("LevelClearTargetDestroyed");
    }
}
