using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool changeRotation;
    public float rotation = 45;
    public float rotSpd = 2.5f;
    public float resetSpd = 5;
    Quaternion originalRot;


    public void ToggleChangeRot(bool rot)
    {
        changeRotation = rot;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(originalRot.eulerAngles + new Vector3(0, rotation, 0)), Time.deltaTime * rotSpd);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, Time.deltaTime * rotSpd);
        }
    }
}
