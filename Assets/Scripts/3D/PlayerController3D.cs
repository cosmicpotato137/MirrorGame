using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float jumpHight;
    public float rot;

    Vector3 runDir;

    Rigidbody rb;
    BoxCollider bc;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        runDir = Vector3.zero;

        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Vertical");
        float z = -Input.GetAxis("Horizontal");
        float y = IsGrounded() && Input.GetButtonDown("Space") ? 1 : 0;

        //transform.rotation = Quaternion.Lerp(transform.rotation, 
        //    Quaternion.LookRotation(runDir, transform.up), Time.deltaTime * 5);
        rb.velocity += new Vector3(0, y * jumpHight, 0);

        if (x != 0 || z != 0)
        {
            Vector3 newDir = Quaternion.Euler(0, -45, 0) * Vector3.Normalize(new Vector3(x, 0, z));
            //Debug.DrawLine(transform.position, transform.position + newDir);

            Quaternion rot = Quaternion.LookRotation(newDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                rot, Time.deltaTime * 5.0f);

            runDir = Vector3.Lerp(runDir, newDir, Time.deltaTime * acceleration);
            rb.velocity = runDir * speed;

            anim.SetBool("New Bool", false);
        }
        else
        {
            runDir = Vector3.Lerp(runDir, Vector3.zero, Time.deltaTime * acceleration);
            rb.velocity = runDir;

            anim.SetBool("New Bool", true);
        }
    }

    bool IsGrounded()
    {
        Vector3 origin = bc.bounds.center - new Vector3(0, bc.bounds.size.y + .01f, 0);
        Vector3 direction = -transform.up;
        return Physics.Raycast(new Ray(origin, direction), .1f);
    }
}
