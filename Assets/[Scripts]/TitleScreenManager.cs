using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void OnAssignment1ButtonClick()
    {
        SceneManager.LoadScene("Assignment1");
    }

    public void OnAssignment2ButtonClick()
    {
        SceneManager.LoadScene("Assignment2");
    }

    public void OnAssignment3ButtonClick()
    {
        SceneManager.LoadScene("Assignment3");
    }

    public void OnAssignment4ButtonClick()
    {
        SceneManager.LoadScene("Assignment4");
    }
}
