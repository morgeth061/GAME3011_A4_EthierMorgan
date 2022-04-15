using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    //Length/Width of Grid
    public int gridSize;

    //Amount of resources gathered
    public int resources = 0;

    //True: Scan Mode On
    //False: Scan Mode Off
    public bool scanMode;

    //Text on Scan/Extract button
    public GameObject buttonText;

    //Array of rows, used in initialization of nodes 2D array
    private GameObject[] rows;

    //2D Array for Nodes
    private GameObject[,] nodes;

    //Message Box
    public GameObject textBox;

    //ResourcesText
    public GameObject resourceText;

    //Scans and Extracts
    private int remainingScans;
    private int remainingExtracts;
    public GameObject scanText;
    public GameObject extractText;

    //**************************
    //Used for testing - when board is not hidden at start
    //**************************
    void Awake()
    {
        Setup();
    }

    //**************************
    //Sets up fresh instance of board
    //**************************
    public void Setup()
    {
        nodes = new GameObject[gridSize, gridSize];
        rows = GameObject.FindGameObjectsWithTag("Row");

        scanMode = false;

        int arrayIndex = 0;

        for (int i = 0; i < gridSize; i++)
        {
            int j = 0;
            foreach (Transform c in rows[i].transform)
            {
                nodes[i, j] = c.gameObject;
                nodes[i, j].GetComponent<NodeBehaviour>().column = j;
                nodes[i, j].GetComponent<NodeBehaviour>().row = i;
                nodes[i, j].GetComponent<NodeBehaviour>().Setup();
                nodes[i, j].GetComponent<NodeBehaviour>().setState(0);
                j++;
                arrayIndex++;
            }
        }

        textBox.gameObject.GetComponent<Text>().text = "";
        resourceText.gameObject.GetComponent<Text>().text = "0";
        

        buttonText = transform.parent.Find("ModeToggle").Find("Text").gameObject;
        buttonText.GetComponent<Text>().text = "Extract Mode";

        remainingScans = 6;
        remainingExtracts = 3;
        scanText.gameObject.GetComponent<Text>().text = remainingScans.ToString();
        extractText.gameObject.GetComponent<Text>().text = remainingExtracts.ToString();
    }

    //**************************
    //Update
    //**************************
    private void Update()
    {
        if (remainingExtracts == 0)
        {
            remainingExtracts = 0;
            remainingScans = 0;
            textBox.gameObject.GetComponent<Text>().text = "Final Count: " + resources + " Resources";
        }
    }

    //**************************
    //Toggles on/off scan mode
    //**************************
    public void ToggleScanMode()
    {
        scanMode = !scanMode;
        if (scanMode)
        {
            buttonText.GetComponent<Text>().text = "Scan Mode";
        }
        else
        {
            buttonText.GetComponent<Text>().text = "Extract Mode";
        }

    }

    //**************************
    //Called when node is clicked
    //**************************
    public void nodeClicked(int row, int col)
    {
        if (scanMode && remainingScans > 0) //Scan Mode
        {
            //Checks 3x3 grid around clicked button
            for (int i = row - 1; i <= row + 1; i++)
            {
                if (i >= 0 && i <= gridSize)
                {
                    for (int j = col - 1; j <= col + 1; j++)
                    {
                        if (j >= 0 && j <= gridSize)
                        {
                            nodes[i, j].GetComponent<NodeBehaviour>().setState(1);
                        }
                    }
                }
            }

            remainingScans--;
        }
        else if (!scanMode && remainingExtracts > 0) //Extract Mode
        {
            int newResources = 0;
            //Checks 3x3 grid around clicked button
            for (int i = row - 2; i <= row + 2; i++)
            {
                if (i >= 0 && i <= gridSize)
                {
                    for (int j = col - 2; j <= col + 2; j++)
                    {
                        if (j >= 0 && j <= gridSize)
                        {
                            Debug.Log(nodes[i, j].GetComponent<NodeBehaviour>().GetValueType());
                            newResources += nodes[i, j].GetComponent<NodeBehaviour>().GetValue();
                            nodes[i,j].GetComponent<NodeBehaviour>().SetValue(nodes[i,j].GetComponent<NodeBehaviour>().GetValueType() + 1);
                            if (nodes[i,j].GetComponent<NodeBehaviour>().GetValueType() > 2)
                            {
                                nodes[i, j].GetComponent<NodeBehaviour>().setState(2);
                            }
                            else
                            {
                                nodes[i, j].GetComponent<NodeBehaviour>().setState(1);
                            }
                        }
                    }
                }
            }

            //Update Resources
            textBox.gameObject.GetComponent<Text>().text = "Mined " + newResources + " resources!";
            resources += newResources;
            resourceText.gameObject.GetComponent<Text>().text = resources.ToString();
            remainingExtracts--;
        }
        scanText.gameObject.GetComponent<Text>().text = remainingScans.ToString();
        extractText.gameObject.GetComponent<Text>().text = remainingExtracts.ToString();
    }
}
