using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeBehaviour : MonoBehaviour
{
    enum State
    {
        HIDDEN,
        REVEALED,
        MINED
    };

    enum Value
    {
        MAX,
        MID,
        MIN,
        NONE
    }

    private int tileValue;
    private State currentState;
    [SerializeField]
    private Value currentValue;

    public int row = 0;
    public int column = 0;

    public Transform button;

    public GameObject controller;

    private Color resourceColor;

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
        button = transform.GetChild(0);
        //currentValue = Value.NONE;
        controller = transform.parent.parent.gameObject;
        if (currentValue == Value.MAX)
        {
            resourceColor = Color.red;
        }
        else if (currentValue == Value.MID)
        {
            resourceColor = Color.yellow;
        }
        else if (currentValue == Value.MIN)
        {
            resourceColor = Color.green;
        }
        else if (currentValue == Value.NONE)
        {
            resourceColor = Color.grey;
        }

        tileValue = 60;

    }

    //**************************
    //Sets Value Type of Node
    //**************************
    public void SetValue(int valueType)
    {
        if (valueType == 0) //MAX
        {
            currentValue = Value.MAX;
            resourceColor = Color.red;
        }
        else if (valueType == 1) //MED
        {
            currentValue = Value.MID;
            resourceColor = Color.yellow;
        }
        else if (valueType == 2) //MIN
        {
            currentValue = Value.MIN;
            resourceColor = Color.green;
        }
        else if (valueType == 3) //NONE
        {
            currentValue = Value.NONE;
            resourceColor = Color.grey;
        }

        if (currentState == State.REVEALED) //Revealed
        {
            button.gameObject.GetComponent<UnityEngine.UI.Image>().color = resourceColor;
        }
    }

    //**************************
    //Returns value proportional to tile value type
    //**************************
    public int GetValue()
    {
        //MAX = 100%
        //MID = 50%
        //MIN = 25%
        //NONE = 0%
        if (currentValue == Value.MAX)
        {
            return tileValue;
        }
        if (currentValue == Value.MID)
        {
            return tileValue / 2;
        }
        if (currentValue == Value.MIN)
        {
            return tileValue / 4;
        }
        if (currentValue == Value.NONE)
        {
            return 0;
        }

        return 420;
    }

    //**************************
    //Returns Value Type
    //**************************
    public int GetValueType()
    {
        if (currentValue == Value.MAX)
        {
            return 0;
        }
        else if (currentValue == Value.MID)
        {
            return 1;
        }
        else if (currentValue == Value.MIN)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    //**************************
    //Sets tile state
    //**************************
    public void setState(int newState)
    {
        if (newState == 0) //Hidden
        {
            currentState = State.HIDDEN;
        }
        else if (newState == 1) //Revealed
        {
            currentState = State.REVEALED;
        }
        else if (newState == 2) //Mined
        {
            currentState = State.MINED;
        }

        updateButtonColour();
    }

    //**************************
    //Updates button colour depending on current state
    //**************************
    private void updateButtonColour()
    {
        if (currentState == State.HIDDEN) //Hidden
        {
            button.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
        else if (currentState == State.REVEALED) //Revealed
        {
            button.gameObject.GetComponent<UnityEngine.UI.Image>().color = resourceColor;
        }
        else if (currentState == State.MINED) //Mined
        {
            button.gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        }
    }

    //**************************
    //Debug
    //**************************
    public void Info()
    {
        Debug.Log(row + " : " + column);
    }

    //**************************
    //OnClick for button
    //**************************
    public void OnButtonClicked()
    {
        controller.GetComponent<NodeController>().nodeClicked(row, column);
    }
}
