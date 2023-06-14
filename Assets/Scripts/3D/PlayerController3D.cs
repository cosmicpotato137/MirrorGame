using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float jumpHight;
    public float groundDist;

    public float rotationOffset;

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
        Move();
    }

    void Move()
    {
        float x = 0; //= Input.GetKey(KeyCode.A) || Input.;
        float z = 0; //= -Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.A))
            x = -1.0f;
        if (Input.GetKey(KeyCode.D))
            x = 1.0f;
        if (Input.GetKey(KeyCode.S))
            z = -1.0f;
        if (Input.GetKey(KeyCode.W))
            z = 1.0f;

        float y = IsGrounded() && Input.GetKeyDown(KeyCode.Space) ? jumpHight : rb.velocity.y;

        //transform.rotation = Quaternion.Lerp(transform.rotation, 
        //    Quaternion.LookRotation(runDir, transform.up), Time.deltaTime * 5);
        var newVel = new Vector3(0, y, 0);

        if (x != 0 || z != 0)
        {
            Vector3 newDir = Quaternion.Euler(0, rotationOffset, 0) * Vector3.Normalize(new Vector3(x, 0, z));
            //Debug.DrawLine(transform.position, transform.position + newDir);

            Quaternion rot = Quaternion.LookRotation(newDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                rot, Time.deltaTime * 5.0f);

            runDir = Vector3.Lerp(runDir, newDir, Time.deltaTime * acceleration);
            newVel += runDir * speed;

            anim.SetBool("New Bool", false);
        }
        else
        {
            runDir = Vector3.Lerp(runDir, Vector3.zero, Time.deltaTime * acceleration);
            newVel += runDir;

            anim.SetBool("New Bool", true);
        }

        rb.velocity = newVel;
    }

    bool IsGrounded()
    {
        Vector3 origin = bc.bounds.center;
        Vector3 direction = -transform.up;
        Debug.DrawLine(origin, origin + direction * groundDist);
        return Physics.Raycast(new Ray(origin, direction), groundDist, LayerMask.GetMask(
            new string[]{ "Ground" }));
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
    }

    public void SetRotationOffset(float newOffset)
    {
        rotationOffset = newOffset;
    }
}
