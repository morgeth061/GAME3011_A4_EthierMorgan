using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2GameButtonBehaviour : MonoBehaviour
{
    private bool gameVisible = false;

    private Transform parentObj;

    private GameObject gameWindow;

    public int lockDifficulty = 0;

    //**************************
    //Toggle Game Window On And Off
    //**************************
    void Awake()
    {
        parentObj = transform.parent; //GameCanvas
        gameWindow = parentObj.transform.Find("LockpickGame").gameObject; //DiggingGame
    }

    public void OnLockpickGameButtonClick()
    {
        gameVisible = !gameVisible;
        gameWindow.SetActive(gameVisible);
        if (gameVisible)
        {
            gameWindow.transform.Find("Lock").gameObject.GetComponent<LockBehaviour>().Setup(lockDifficulty);
        }
    }
}
