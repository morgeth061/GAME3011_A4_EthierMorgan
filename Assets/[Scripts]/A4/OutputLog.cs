using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class OutputLog : MonoBehaviour
{
    [SerializeField]
    private GameObject output1;
    [SerializeField]
    private GameObject output2;
    [SerializeField]
    private GameObject output3;
    [SerializeField]
    private GameObject output4;
    [SerializeField]
    private GameObject output5;
    [SerializeField]
    private GameObject output6;
    [SerializeField]
    private GameObject output7;
    [SerializeField]
    private GameObject output8;
    [SerializeField]
    private GameObject output9;
    [SerializeField]
    private GameObject output10;

    public void AddOutput(string Output)
    {
        output10.GetComponent<TextMeshProUGUI>().text = output9.GetComponent<TextMeshProUGUI>().text;
        output9.GetComponent<TextMeshProUGUI>().text = output8.GetComponent<TextMeshProUGUI>().text;
        output8.GetComponent<TextMeshProUGUI>().text = output7.GetComponent<TextMeshProUGUI>().text;
        output7.GetComponent<TextMeshProUGUI>().text = output6.GetComponent<TextMeshProUGUI>().text;
        output6.GetComponent<TextMeshProUGUI>().text = output5.GetComponent<TextMeshProUGUI>().text;
        output5.GetComponent<TextMeshProUGUI>().text = output4.GetComponent<TextMeshProUGUI>().text;
        output4.GetComponent<TextMeshProUGUI>().text = output3.GetComponent<TextMeshProUGUI>().text;
        output3.GetComponent<TextMeshProUGUI>().text = output2.GetComponent<TextMeshProUGUI>().text;
        output2.GetComponent<TextMeshProUGUI>().text = output1.GetComponent<TextMeshProUGUI>().text;
        output1.GetComponent<TextMeshProUGUI>().text = Output;
    }

    public void ResetLog()
    {
        output10.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
        output9.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
        output8.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
        output7.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
        output6.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
        output5.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
        output4.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
        output3.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
        output2.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
        output1.GetComponent<TextMeshProUGUI>().text = "XXXXXX | XXXXXX";
    }
}
