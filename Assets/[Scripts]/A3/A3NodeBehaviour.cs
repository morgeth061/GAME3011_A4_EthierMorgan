using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class A3NodeBehaviour : MonoBehaviour
{
    public int row;
    public int col;

    public int matchNum;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite7;
    public Sprite sprite8;
    public Sprite sprite1_v;
    public Sprite sprite1_h;
    public Sprite sprite2_v;
    public Sprite sprite2_h;
    public Sprite sprite3_v;
    public Sprite sprite3_h;
    public Sprite sprite4_v;
    public Sprite sprite4_h;
    public Sprite sprite5_v;
    public Sprite sprite5_h;
    public Sprite sprite6_v;
    public Sprite sprite6_h;

    public int nodeColour;

    public bool isInMatch = false;

    public bool isActive = false;

    public bool horizBomb = false;

    public bool vertBomb = false;

    public bool immovable = false;

    public bool exemptFromDestroy = false;

    public GameObject nodePool;

    public GameObject gridRef;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gridRef = GameObject.FindWithTag("Grid");
        nodePool = GameObject.FindWithTag("NodePool");
        Reset();
    }

    public void Reset()
    {
        matchNum = 0;
        isActive = true;
        horizBomb = false;
        vertBomb = false;
        immovable = false;
        exemptFromDestroy = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        int rand = Random.Range(0, (10 + GameObject.FindWithTag("GameWindow").GetComponent<A3GameManager>().difficulty));

        if (rand == 0 || rand == 1)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite1;
            nodeColour = 0;
        }
        else if (rand == 2 || rand == 3)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite2;
            nodeColour = 1;
        }
        else if (rand == 4 || rand == 5)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite3;
            nodeColour = 2;
        }
        else if (rand == 6 || rand == 7)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite4;
            nodeColour = 3;
        }
        else if (rand == 8 || rand == 9)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite5;
            nodeColour = 4;
        }
        else if (rand == 10 || rand == 11)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite6;
            nodeColour = 5;
        }
        else if (rand == 12) //Immovable
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite7;
            immovable = true;
            nodeColour = -1;
        }
    }

    public void SetInactive()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.position = nodePool.transform.position;
        transform.SetParent(nodePool.transform);
        isActive = false;
        isInMatch = false;
    }

    public void OnNodeClick()
    {
        if (!GameObject.Find("MatchGame").GetComponent<A3GameManager>().swapLock || immovable)
        {
            gridRef.GetComponent<SwapManager>().SwapBehaviour(this.gameObject);
        }
        
    }

    public void UpdateColour()
    {
        if (nodeColour == 0)
        {
            if (horizBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite1_h;
            }
            else if (vertBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite1_v;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite1;
            }
        }
        else if (nodeColour == 1)
        {
            if (horizBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite2_h;
            }
            else if (vertBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite2_v;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite2;
            }
        }
        else if (nodeColour == 2)
        {
            if (horizBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite3_h;
            }
            else if (vertBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite3_v;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite3;
            }
        }
        else if (nodeColour == 3)
        {
            if (horizBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite4_h;
            }
            else if (vertBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite4_v;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite4;
            }
        }
        else if (nodeColour == 4)
        {
            if (horizBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite5_h;
            }
            else if (vertBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite5_v;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite5;
            }
        }
        else if (nodeColour == 5)
        {
            if (horizBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite6_h;
            }
            else if (vertBomb)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite6_v;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = sprite6;
            }
        }
    }
}
