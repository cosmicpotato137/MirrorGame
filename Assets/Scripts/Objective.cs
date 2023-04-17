using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : LightInteractable
{
    public bool on = false;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        on = false;
    }

    private void FixedUpdate()
    {
        sr.color = Color.white;
    }

    public override Vector3 CalculateLight(Vector3 inDirection, RaycastHit2D hit, ref List<Vector3> positions)
    {
        sr.color = Color.yellow;
        on = true;
        return base.CalculateLight(inDirection, hit, ref positions);
    }
}
