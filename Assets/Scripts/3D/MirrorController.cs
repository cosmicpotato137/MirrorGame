using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MirrorController : MonoBehaviour
{
    public UnityEvent onPush;
    public UnityEvent onStop;

    RigidbodyConstraints freeze = 
        RigidbodyConstraints.FreezePositionX |
        RigidbodyConstraints.FreezeRotationZ |
        RigidbodyConstraints.FreezeRotationY;

    Rigidbody rb;
    [HideInInspector]

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = freeze;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {

    }

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

            if (other.CompareTag("Player"))
            {
                SetLayer("Outline");
            }

            if (other.CompareTag("Player") && rb.velocity != Vector3.zero)
                onPush.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Mirror"))
        {
            rb.velocity = Vector3.zero;
            rb.constraints = freeze;

            if (other.CompareTag("Player"))
            {
                SetLayer("Default");
                onStop.Invoke();
            }
        }
    }

    void SetLayer(string layer)
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child != this.transform && child.name != "Colliders")
                child.gameObject.layer = LayerMask.NameToLayer(layer);
        }
    }
}
