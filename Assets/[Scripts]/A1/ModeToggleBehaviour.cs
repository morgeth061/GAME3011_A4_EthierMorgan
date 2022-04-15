using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeToggleBehaviour : MonoBehaviour
{
    private GameObject gameWindow;
    
    void Awake()
    {
        gameWindow = transform.parent.gameObject;
    }

    public void OnModeToggleClick()
    {
        gameWindow.transform.Find("Nodes").gameObject.GetComponent<NodeController>().ToggleScanMode();
    }
}
