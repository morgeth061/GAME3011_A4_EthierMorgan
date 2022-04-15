using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Difficulty 0 = Easy
//Difficulty 1 = Medium
//Difficulty 2 = Hard

public class LockBehaviour : MonoBehaviour
{
    public GameObject lockPick;

    public float lockNum; //Random number used as "Sweet Spot"

    public float lockDiff; //Difference between current lock pick position and "Sweet Spot"

    public float lockRot; //Rotation of lock

    public int currentDifficulty; //Int representation of current difficulty

    public bool isHeld = false;

    public bool lockPicked = false;

    private bool unlockAttempted = false;

    private float timer;

    public GameObject skillContainer; //Object that holds player skill

    public GameObject TimerText;

    public GameObject DifficultyText;

    public AudioSource KeyUnlock;
    
    void Start()
    {
        Setup(0);
    }

    public void Setup(int difficulty)
    {
        transform.Find("LockTop").transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        lockNum = Random.Range(0, 36) * 2.5f;
        lockRot = 0.0f;
        lockDiff = 0.0f;
        currentDifficulty = difficulty;
        lockPicked = false;
        isHeld = false;

        //Timer setup
        if (difficulty == 0)
        {
            timer = 60;
            DifficultyText.GetComponent<TextMeshProUGUI>().text = "Easy";
        }
        else if (difficulty == 1)
        {
            timer = 45;
            DifficultyText.GetComponent<TextMeshProUGUI>().text = "Medium";
        }
        else if (difficulty == 2)
        {
            timer = 30;
            DifficultyText.GetComponent<TextMeshProUGUI>().text = "Hard";
        }

    }
    
    void Update()
    {
        float lockpickNum = lockPick.GetComponent<LockpickBehaviour>().lockpickVal / 2;
        lockDiff = Mathf.Abs(lockpickNum - lockNum);
        TimerText.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(timer)).ToString();

        if (Input.GetKeyDown("w"))
        {
            isHeld = true;

        }
        if (Input.GetKeyUp("w"))
        {
            isHeld = false;
            unlockAttempted = false;
        }

        if (!lockPicked && timer > 0)
        {
            timer -= Time.deltaTime;

            if (isHeld && (lockRot <= 90.0f - lockDiff))
            {
                lockRot = lockRot + 1.0f;
                GetComponent<AudioSource>().Play();
                unlockAttempted = true;
            }
            else if (isHeld && lockRot > 90.0f && !unlockAttempted) //Return lock to base position
            {
                lockRot = 90.0f;
            }
            else if (!isHeld && lockRot > 0.0f) //Return lock to base position
            {
                lockRot = lockRot - 1.0f;
                GetComponent<AudioSource>().Stop();
            }
            else if (!isHeld && lockRot <= 0.0f)
            {
                lockRot = 0.0f;
            }

            if (lockRot >= (90.0f - (2.0f - currentDifficulty) * 7.5f))
            {
                GetComponent<AudioSource>().Stop();
                KeyUnlock.PlayOneShot(KeyUnlock.clip, 0.75f);
                print("Lock Picked");
                lockRot = 90.0f;
                lockPicked = true;
                transform.Find("LockTop").transform.localPosition = new Vector3(0.0f, 50.0f, 0.0f);
                if (skillContainer.GetComponent<A2GameButtonBehaviour>().lockDifficulty < 2)
                {
                    skillContainer.GetComponent<A2GameButtonBehaviour>().lockDifficulty = currentDifficulty + 1;
                }
            }
        }
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, lockRot);
    }
}
