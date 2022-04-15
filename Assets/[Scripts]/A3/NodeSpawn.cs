using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawn : MonoBehaviour
{
    public GameObject nodeRef;

    public GameObject spawnPoint;

    public GameObject gameManager;

    public GameObject nodePool;

    private bool spawnReady;

    void Update()
    {
        if (spawnPoint.transform.childCount < 10 && spawnReady)
        {
            SpawnNode();
            spawnReady = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        spawnReady = true;
        //print(spawnReady);

    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (gameManager.GetComponent<A3GameManager>().spawnLock == false || gameManager.GetComponent<A3GameManager>().gameBegin == false)
    //    {
    //        Instantiate(nodeRef, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
    //    }
    //}

    public void SpawnNode()
    {
        if (gameManager.GetComponent<A3GameManager>().spawnLock == false || gameManager.GetComponent<A3GameManager>().gameBegin == false)
        {
            if (nodePool.transform.childCount > 0)
            {
                nodePool.transform.GetChild(0).transform.position = spawnPoint.transform.position;
                nodePool.transform.GetChild(0).GetComponent<A3NodeBehaviour>().Reset();
                nodePool.transform.GetChild(0).SetParent(spawnPoint.transform);
            }
            else
            {
                Instantiate(nodeRef, nodePool.transform.position, Quaternion.identity, nodePool.transform);
                nodePool.transform.GetChild(0).transform.position = spawnPoint.transform.position;
                nodePool.transform.GetChild(0).GetComponent<A3NodeBehaviour>().Reset();
                nodePool.transform.GetChild(0).SetParent(spawnPoint.transform);
            }
            
        }
    }
}
