using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
    }

    // Update is called once per frame
    void Update()
    {

    }


    //private void OnTriggerEnter(Collider other)
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Mirror"))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            var dir = Mathf.Abs(Vector3.Dot(
                Vector3.Normalize(Vector3.Scale(new Vector3(1, 0, 1), (other.transform.position - transform.position))),
                Vector3.forward)
            );
            if (dir > .5f)
                rb.constraints |= RigidbodyConstraints.FreezePositionX;
            else
                rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Mirror"))
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
