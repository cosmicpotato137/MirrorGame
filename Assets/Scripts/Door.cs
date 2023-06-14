using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 upDirection = Vector3.up;
    [Range(0, 1.0f)]
    public float openAmmount;
    Vector3 up;
    Vector3 down;
    public bool startOpen = false;
    public float delay = 1;
    float startTime = 0;

    bool open;

    // Start is called before the first frame update
    void Start()
    {
        up = transform.position + Quaternion.Inverse(transform.rotation) * Vector3.Scale(transform.lossyScale, transform.rotation * (Vector3.Normalize(upDirection) * openAmmount));
        down = transform.position;
        open = startOpen;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            transform.position = Vector3.Lerp(transform.position, up, 5.0f * Time.deltaTime);
            startTime = Time.time;
        }
        else
            transform.position = Vector3.Lerp(transform.position, down, 5.0f * Time.deltaTime);

    }

    public void Open()
    {
        open = true;
    }

    public void Close()
    {
        open = false;
    }
}
