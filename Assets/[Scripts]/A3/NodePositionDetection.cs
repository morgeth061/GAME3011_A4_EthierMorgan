using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePositionDetection : MonoBehaviour
{
    public int row;

    public int col;

    public GameObject currentNode;

    public GameObject rightNode;

    public GameObject downNode;

    public bool filled = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<A3NodeBehaviour>().row = row;
        other.gameObject.GetComponent<A3NodeBehaviour>().col = col;
        currentNode = other.gameObject;
        filled = true;
    }

    private void Update()
    {
        if (currentNode)
        {
            filled = true;
        }
        else
        {
            filled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //currentNode = null;
        //filled = false;
    }
}
