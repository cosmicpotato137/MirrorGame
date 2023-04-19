using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : LightInteractable
{
    public bool on;
    bool stillOn;

    public UnityEvent onSwitchOn;
    public UnityEvent onSwitchOff;

    SpriteRenderer sr;
    Color oldColor;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        on = false;
        stillOn = false;
        oldColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {

        if (!on && stillOn)
        {
            onSwitchOff.Invoke();
            sr.color = oldColor;
        }

        stillOn = on;
        on = false;
    }

    private void FixedUpdate()
    {
    }

    public override void CalculateLight(Vector3 inDirection, RaycastHit2D hit, ref List<List<Vector3>> positions, int depth, LayerMask layerMask)
    {
        positions[0].Add(hit.point);

        if (!on)
            onSwitchOn.Invoke();

        sr.color = Color.yellow;
        on = true;
        stillOn = true;
    }
}
