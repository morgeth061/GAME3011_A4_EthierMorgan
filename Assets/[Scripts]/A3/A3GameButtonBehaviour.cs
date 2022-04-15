using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3GameButtonBehaviour : MonoBehaviour
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
        gameWindow = parentObj.transform.Find("MatchGame").gameObject; //DiggingGame
    }

    public void OnMatchButtonClick()
    {
        gameVisible = !gameVisible;
        
        if (gameVisible)
        {
            gameWindow.SetActive(gameVisible);
            gameWindow.GetComponent<A3GameManager>().BeginGame();
        }
        else
        {
            foreach (GameObject node in GameObject.FindGameObjectsWithTag("SwapNode"))
            {
                Destroy(node);
            }
            gameWindow.SetActive(gameVisible);
        }
    }
}
