using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    float speed;
    bool moving;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = Vector2.Lerp(transform.position, target, speed * Time.deltaTime);
            if (Mathf.Abs(Vector2.Distance(transform.position, target)) < .001)
            {
                transform.position = target;
                moving = false;
            }
        }

    }

    public void OnPush(Vector3 direction, float speed)
    {
        target = transform.position + direction;
        moving = true;
        this.speed = speed;
    }
}
