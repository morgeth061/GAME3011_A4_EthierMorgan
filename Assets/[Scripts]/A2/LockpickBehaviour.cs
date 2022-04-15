using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockpickBehaviour : MonoBehaviour
{
    public float mouseSensitivity = 1.0f;
    public Transform lockPick;

    public float lockpickVal;

    private float YRotation = 0.0f;

    public GameObject Lock;
    
    void Update()
    {
        if (!Lock.GetComponent<LockBehaviour>().isHeld && !Lock.GetComponent<LockBehaviour>().lockPicked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            YRotation -= mouseX;
            YRotation = Mathf.Clamp(YRotation, -90.0f, 90.0f);

            lockpickVal = YRotation + 90.0f;

            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, YRotation);
            lockPick.Rotate(Vector3.right * mouseY);
        }
    }
}
