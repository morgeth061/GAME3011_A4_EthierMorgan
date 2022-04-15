using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class A3GameManager : MonoBehaviour
{
    //Column Spawn Objects
    public GameObject[] colSpawnObjects;

    private GameObject[] nodes;

    public GameObject nodeRef;

    public GameObject nodePool;

    public GameObject timerText;

    public GameObject difficultyText;

    public GameObject scoreSlider;

    public GameObject FillingText;

    public GameObject winScreen;

    public AudioSource lineClear;

    public bool gameBegin = false;

    public bool swapLock = true;

    public bool spawnLock = false;

    public bool recheck = false;

    public bool gameOver = false;

    private bool fullyFilled = false;

    public int difficulty = 1;

    public float timer;

    public float score;

    public float goalScore;

    public int forceReady;

    // Start is called before the first frame update
    void Start()
    {
        BeginGame();
    }

    public void BeginGame()
    {
        score = 0;
        gameOver = false;
        winScreen.SetActive(false);
        gameBegin = false;

        if (difficulty == 1)
        {
            timer = 240;
            goalScore = 750;
            difficultyText.GetComponent<TextMeshProUGUI>().text = "Easy";
        }
        else if (difficulty == 2)
        {
            timer = 180;
            goalScore = 1250;
            difficultyText.GetComponent<TextMeshProUGUI>().text = "Normal";
        }
        else if (difficulty == 3)
        {
            timer = 120;
            goalScore = 1250;

            difficultyText.GetComponent<TextMeshProUGUI>().text = "Hard";
        }

        gameOver = false;

        for (int i = 0; i < colSpawnObjects.Length; i++)
        {
            Instantiate(nodeRef, nodePool.transform.position, Quaternion.identity, nodePool.transform);
            nodePool.transform.GetChild(0).transform.position = colSpawnObjects[i].transform.position;
            nodePool.transform.GetChild(0).GetComponent<A3NodeBehaviour>().Reset();
            nodePool.transform.GetChild(0).SetParent(colSpawnObjects[i].transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= goalScore && !gameOver)//Check for Win
        {
            gameOver = true;
            winScreen.SetActive(true);
            swapLock = true;
            spawnLock = true;
            if (difficulty < 3)
            {
                difficulty++;
            }
        }

        if (gameBegin)//Timer
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                gameOver = true;
                swapLock = true;
                spawnLock = true;
            }

            FillingText.SetActive(false);
        }
        else
        {
            FillingText.SetActive(true);
        }

        //Update UI
        timerText.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(timer)).ToString();
        nodes = GameObject.FindGameObjectsWithTag("SwapNode");
        scoreSlider.GetComponent<Slider>().value = score / goalScore;

        //Column Check
        bool checkCols = true;

        for (int i = 0; i < 10; i++)
        {
            if (GameObject.Find("ColSpawn" + i).transform.childCount < 10)
            {
                checkCols = false;
            }
            else
            {
                if (GameObject.Find("ColSpawn" + i).transform.GetChild(9).GetComponent<Rigidbody2D>()
                    .velocity != Vector2.zero)
                {
                    if (forceReady < 3000)
                    {
                        forceReady++;
                        checkCols = false;
                    }
                    else
                    {
                        forceReady = 0;
                        print("Ready State Forced");
                    }
                }
            }
        }

        

        if (!gameBegin && checkCols)
        {
            fullyFilled = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (GameObject.FindGameObjectsWithTag("A3Row")[i].transform.GetChild(j).gameObject
                        .GetComponent<NodePositionDetection>().filled == false ||
                        nodes[(i * 10) + j].GetComponent<Rigidbody2D>().velocity.y != 0)
                    {
                        fullyFilled = false;
                        break;
                    }
                    if (!fullyFilled)
                    {
                        break;
                    }
                }
            }

            if (fullyFilled)
            {
                print("Grid Fully Filled");
                CheckForMatches();
                //print(recheck);
                if (!recheck)
                {
                    print("Game Begin");
                    gameBegin = true;
                    swapLock = false;
                    spawnLock = true;
                }
            }
        }
        else if (gameBegin && checkCols && swapLock)
        {
            fullyFilled = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (GameObject.FindGameObjectsWithTag("A3Row")[i].transform.GetChild(j).gameObject
                            .GetComponent<NodePositionDetection>().filled == false ||
                        nodes[(i * 10) + j].GetComponent<Rigidbody2D>().velocity.y != 0)
                    {
                        fullyFilled = false;
                        break;
                    }
                    if (!fullyFilled)
                    {
                        break;
                    }
                }
            }

            if (fullyFilled)
            {
                print("Grid Fully Filled");
                CheckForMatches();
                if (!recheck)
                {
                    swapLock = false;
                    spawnLock = true;
                }
            }
        }
        else
        {
            spawnLock = false;
        }
    }

    public void CheckForMatches()
    {
        recheck = false;
        swapLock = true;
        spawnLock = true;

        GameObject gridObj = GameObject.FindWithTag("Grid");
        print(("Checking for matches"));

        foreach (Transform rowTransform in gridObj.transform)
        {
            foreach (Transform nodeTransform in rowTransform)
            {

                GameObject currentNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().currentNode;

                if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode) //Check if right node exists
                {
                    GameObject rightNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode.GetComponent<NodePositionDetection>().currentNode;
                    if (!rightNode.GetComponent<A3NodeBehaviour>().isInMatch && currentNode.GetComponent<A3NodeBehaviour>().nodeColour == rightNode.GetComponent<A3NodeBehaviour>().nodeColour)
                    {
                        currentNode.GetComponent<A3NodeBehaviour>().matchNum = 1;
                        rightNode.GetComponent<A3NodeBehaviour>().matchNum = 2;
                        if (CheckForMatchingNeigbour(
                            nodeTransform.GetComponent<NodePositionDetection>().rightNode.transform, rightNode, true))
                        {
                            currentNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                            BombCheck(currentNode);
                            BombCheck(rightNode);
                        }
                    }
                }

                if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode)
                {
                    GameObject downNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode.GetComponent<NodePositionDetection>().currentNode;
                    if (!downNode.GetComponent<A3NodeBehaviour>().isInMatch && currentNode.GetComponent<A3NodeBehaviour>().nodeColour == downNode.GetComponent<A3NodeBehaviour>().nodeColour)
                    {
                        currentNode.GetComponent<A3NodeBehaviour>().matchNum = 1;
                        downNode.GetComponent<A3NodeBehaviour>().matchNum = 2;
                        if (CheckForMatchingNeigbour(
                            nodeTransform.GetComponent<NodePositionDetection>().downNode.transform, downNode, false))
                        {
                            currentNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                            BombCheck(currentNode);
                            BombCheck(downNode);
                        }
                    }
                }
            }
        }

        nodes = GameObject.FindGameObjectsWithTag("SwapNode");

        foreach (GameObject node in nodes) //Check for destroy
        {
            node.GetComponent<A3NodeBehaviour>().matchNum = 0;
            if (node.GetComponent<A3NodeBehaviour>().isInMatch && !node.GetComponent<A3NodeBehaviour>().exemptFromDestroy)
            {
                gridObj.transform.GetChild(node.GetComponent<A3NodeBehaviour>().row).
                    transform.GetChild(node.GetComponent<A3NodeBehaviour>().col).GetComponent<NodePositionDetection>()
                    .currentNode = null;
                gridObj.transform.GetChild(node.GetComponent<A3NodeBehaviour>().row).transform
                    .GetChild(node.GetComponent<A3NodeBehaviour>().col).GetComponent<NodePositionDetection>()
                    .filled = false;
                
                node.GetComponent<A3NodeBehaviour>().SetInactive();

                if (gameBegin)
                {
                    score += 25;
                }

                recheck = true;
            }
            else if (node.GetComponent<A3NodeBehaviour>().exemptFromDestroy)
            {
                node.GetComponent<A3NodeBehaviour>().exemptFromDestroy = false;
                node.GetComponent<A3NodeBehaviour>().isInMatch = false;
            }
        }

        if (recheck)
        {
            lineClear.PlayOneShot(lineClear.clip, 0.75f);
        }
        print("Test");
        fullyFilled = false;
        spawnLock = false;
        //swapLock = false;
    }

    private void BombCheck(GameObject node)
    {
        if (node.GetComponent<A3NodeBehaviour>().horizBomb)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject.Find("ColSpawn" + i).transform.GetChild(node.GetComponent<A3NodeBehaviour>().row)
                    .GetComponent<A3NodeBehaviour>().isInMatch = true;
            }
        }
        if (node.GetComponent<A3NodeBehaviour>().vertBomb)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject.Find("ColSpawn" + node.GetComponent<A3NodeBehaviour>().col).transform.GetChild(i)
                    .GetComponent<A3NodeBehaviour>().isInMatch = true;
            }
        }
    }

    private bool CheckForMatchingNeigbour(Transform nodeTransform, GameObject node, bool right)
    {
        if (right)
        {
            if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode)
            {
                GameObject rightNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().rightNode.GetComponent<NodePositionDetection>().currentNode;
                if (rightNode.GetComponent<A3NodeBehaviour>().nodeColour == node.GetComponent<A3NodeBehaviour>().nodeColour)
                {

                    BombCheck(node);
                    BombCheck(rightNode);

                    rightNode.GetComponent<A3NodeBehaviour>().matchNum = node.GetComponent<A3NodeBehaviour>().matchNum + 1;
                    if (rightNode.GetComponent<A3NodeBehaviour>().matchNum == 4 && difficulty >= 2)
                    {
                        node.GetComponent<A3NodeBehaviour>().horizBomb = true;
                        node.GetComponent<A3NodeBehaviour>().exemptFromDestroy = true;
                        node.GetComponent<A3NodeBehaviour>().UpdateColour();
                        node.GetComponent<A3NodeBehaviour>().isInMatch = false;
                    }
                    else
                    {
                        node.GetComponent<A3NodeBehaviour>().isInMatch = true;
                    }
                    
                    rightNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                    return CheckForMatchingNeigbour(nodeTransform.GetComponent<NodePositionDetection>().rightNode.transform, rightNode, true);
                }
            }
        }
        else
        {
            if (nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode)
            {
                GameObject downNode = nodeTransform.gameObject.GetComponent<NodePositionDetection>().downNode.GetComponent<NodePositionDetection>().currentNode;
                if (downNode.GetComponent<A3NodeBehaviour>().nodeColour == node.GetComponent<A3NodeBehaviour>().nodeColour)
                {
                    BombCheck(node);
                    BombCheck(downNode);

                    downNode.GetComponent<A3NodeBehaviour>().matchNum = node.GetComponent<A3NodeBehaviour>().matchNum + 1;
                    if (downNode.GetComponent<A3NodeBehaviour>().matchNum == 4 && difficulty >= 2)
                    {
                        node.GetComponent<A3NodeBehaviour>().vertBomb = true;
                        node.GetComponent<A3NodeBehaviour>().exemptFromDestroy = true;
                        node.GetComponent<A3NodeBehaviour>().UpdateColour();
                        node.GetComponent<A3NodeBehaviour>().isInMatch = false;
                    }
                    else
                    {
                        node.GetComponent<A3NodeBehaviour>().isInMatch = true;
                    }

                    downNode.GetComponent<A3NodeBehaviour>().isInMatch = true;
                    return CheckForMatchingNeigbour(nodeTransform.GetComponent<NodePositionDetection>().downNode.transform, downNode, false);

                }
            }
        }

        

        if (node.GetComponent<A3NodeBehaviour>().isInMatch)
        {
            return true;
        }
        return false;

    }
}
