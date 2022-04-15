using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A4GameButtonBehaviour : MonoBehaviour
{
    private bool gameVisible = false;

    private Transform parentObj;

    private GameObject gameWindow;


    //**************************
    //Toggle Game Window On And Off
    //**************************
    void Awake()
    {
        parentObj = transform.parent; //GameCanvas
        gameWindow = parentObj.transform.Find("HackingGame").gameObject; //HackingGame
    }

    public void OnMatchButtonClick()
    {
        gameVisible = !gameVisible;

        if (gameVisible)
        {
            gameWindow.SetActive(gameVisible);
            gameWindow.GetComponent<A4GameManager>().Initialize();
        }
        else
        {
            gameWindow.SetActive(gameVisible);
        }
    }
}
