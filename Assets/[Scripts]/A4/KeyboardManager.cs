using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject gameManager;

    public void OnAButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(0);
    }

    public void OnBButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(1);
    }

    public void OnCButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(2);
    }

    public void OnDButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(3);
    }

    public void OnEButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(4);
    }

    public void OnFButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(5);
    }

    public void OnGButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(6);
    }

    public void OnHButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(7);
    }

    public void OnIButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(8);
    }

    public void OnJButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(9);
    }

    public void OnKButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(10);
    }

    public void OnLButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(11);
    }

    public void OnMButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(12);
    }

    public void OnNButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(13);
    }

    public void OnOButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(14);
    }

    public void OnPButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(15);
    }

    public void OnQButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(16);
    }

    public void OnRButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(17);
    }

    public void OnSButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(18);
    }

    public void OnTButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(19);
    }

    public void OnUButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(20);
    }

    public void OnVButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(21);
    }

    public void OnWButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(22);
    }

    public void OnXButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(23);
    }

    public void OnYButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(24);
    }

    public void OnZButtonPress()
    {
        gameManager.GetComponent<A4GameManager>().KeyboardInput(25);
    }

    public void OnResetPress()
    {
        gameManager.GetComponent<A4GameManager>().ResetBoard();
    }
}
