using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class A4GameManager : MonoBehaviour
{
    enum Letters {A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, NUM_LETTERS}

    private Letters letterInput;

    private int currentLetter;

    public int difficulty = 1;

    private int boardSize;

    private Letters[] answerKey;

    private Letters[] guesses;

    //0 = no check, 1 = earlier, 2 = correct, 3 = later
    private int[] letterStatus;

    private bool inputLock;

    private bool winLock;

    [SerializeField] 
    private GameObject easyBoard;

    [SerializeField]
    private GameObject mediumBoard;

    [SerializeField]
    private GameObject hardBoard;

    [SerializeField]
    private GameObject OutputLog;

    [SerializeField]
    private Sprite blank;

    [SerializeField]
    private Sprite left;

    [SerializeField]
    private Sprite right;

    [SerializeField]
    private Sprite correct;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();   
    }

    public void Initialize()
    {
        OutputLog.GetComponent<OutputLog>().ResetLog();

        currentLetter = 0;
        boardSize = 3 + difficulty;
        inputLock = false;
        winLock = false;

        answerKey = new Letters[boardSize];
        guesses = new Letters[boardSize];
        letterStatus = new int[boardSize];

        for (int i = 0; i < answerKey.Length; i++)
        {
            answerKey[i] = (Letters)Random.Range(0, 26);
            letterStatus[i] = 0;
        }

        if (difficulty == 1)
        {
            foreach (Transform child in easyBoard.transform)
            {

                child.GetChild(0).gameObject.GetComponent<Image>().sprite = blank;
            }
        }
        else if (difficulty == 2)
        {
            foreach (Transform child in mediumBoard.transform)
            {

                child.GetChild(0).gameObject.GetComponent<Image>().sprite = blank;
            }
        }
        else if (difficulty == 3)
        {
            foreach (Transform child in hardBoard.transform)
            {

                child.GetChild(0).gameObject.GetComponent<Image>().sprite = blank;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeyboardInput(int key)
    {

        if (!inputLock && !winLock)
        {
            letterInput = (Letters)key;
            guesses[currentLetter] = letterInput;

            if (difficulty == 1)
            {
                easyBoard.transform.GetChild(currentLetter++).GetChild(1).GetComponent<TextMeshProUGUI>().text = letterInput.ToString();
            }
            else if (difficulty == 2)
            {
                mediumBoard.transform.GetChild(currentLetter++).GetChild(1).GetComponent<TextMeshProUGUI>().text = letterInput.ToString();
            }
            else if (difficulty >= 3)
            {
                hardBoard.transform.GetChild(currentLetter++).GetChild(1).GetComponent<TextMeshProUGUI>().text = letterInput.ToString();
            }
        }
        
        //Check for win
        CheckForWin();
    }

    private void CheckForWin()
    {
        if (currentLetter >= boardSize && inputLock == false && winLock == false) 
        {
            inputLock = true;
            for (int i = 0; i < answerKey.Length; i++)
            {
                if (answerKey[i] < guesses[i]) //Left
                {
                    if (difficulty == 1)
                    {
                        easyBoard.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = left;
                    }
                    else if (difficulty == 2)
                    {
                        mediumBoard.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = left;
                    }
                    else if (difficulty == 3)
                    {
                        hardBoard.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = left;
                    }

                    letterStatus[i] = 1;
                }
                else if (answerKey[i] == guesses[i]) //Correct
                {
                    if (difficulty == 1)
                    {
                        easyBoard.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = correct;
                    }
                    else if (difficulty == 2)
                    {
                        mediumBoard.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = correct;
                    }
                    else if (difficulty == 3)
                    {
                        hardBoard.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = correct;
                    }

                    letterStatus[i] = 2;
                }
                else if (answerKey[i] > guesses[i]) //Right
                {
                    if (difficulty == 1)
                    {
                        easyBoard.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = right;
                    }
                    else if (difficulty == 2)
                    {
                        mediumBoard.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = right;
                    }
                    else if (difficulty == 3)
                    {
                        hardBoard.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = right;
                    }

                    letterStatus[i] = 3;
                }
            }

            bool winCheck = true;

            foreach (int status in letterStatus)
            {
                if (status != 2)
                {
                    winCheck = false;
                    break;
                }
            }

            if (winCheck)
            {
                //Win
            }
            else
            {
                
                //Subtract Attempt
            }

            string guessesText = "";
            string statusText = "";

            for (int i = 0; i < guesses.Length; i++)
            {
                guessesText = guessesText + guesses[i].ToString();

                if (letterStatus[i] != 2)
                {
                    statusText = statusText + "X";
                }
                else
                {
                    statusText = statusText + "O";
                }
                Debug.Log("Test");
                
            }
            OutputLog.GetComponent<OutputLog>().AddOutput(guessesText + " | " + statusText);
        }
    }

    public void ResetBoard()
    {
        Debug.Log("Reset Board");
        for (int i = 0; i < guesses.Length; i++)
        {
            guesses[i] = Letters.NUM_LETTERS;
        }

        currentLetter = 0;
        inputLock = false;

        if (difficulty == 1)
        {
            foreach (Transform child in easyBoard.transform)
            {
                child.GetChild(1).GetComponent<TextMeshProUGUI>().text = "-";
                child.GetChild(0).gameObject.GetComponent<Image>().sprite = blank;
            }
        }
        else if (difficulty == 2)
        {
            foreach (Transform child in mediumBoard.transform)
            {
                child.GetChild(1).GetComponent<TextMeshProUGUI>().text = "-";
                child.GetChild(0).gameObject.GetComponent<Image>().sprite = blank;
            }
        }
        else if (difficulty == 3)
        {
            foreach (Transform child in hardBoard.transform)
            {
                child.GetChild(1).GetComponent<TextMeshProUGUI>().text = "-";
                child.GetChild(0).gameObject.GetComponent<Image>().sprite = blank;
            }
        }
    }
}
