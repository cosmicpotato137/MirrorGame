using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Objective objective;
    Vector3 up;
    Vector3 down;
    public float delay = 1;
    float startTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        up = transform.position + Vector3.up;
        down = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (objective.on == true)
        {
            transform.position = Vector3.Lerp(transform.position, up, 5.0f * Time.deltaTime);
            startTime = Time.time;
        }
        else if (Time.time - startTime > delay)
            transform.position = Vector3.Lerp(transform.position, down, 5.0f * Time.deltaTime);
    }
}
