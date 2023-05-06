using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button3D : LightInteractable3D
{
    public Material offMat;
    public Material onMat;
    public bool on;
    bool stillOn;

    Renderer rend;

    public UnityEvent onSwitchOn;
    public UnityEvent onSwitchOff;

    Color oldColor;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        on = false;
        stillOn = false;
        if (!on)
            rend.material = offMat;
        else
            rend.material = onMat;
    }

    // Update is called once per frame
    void Update()
    {

        if (!on && stillOn)
        {
            onSwitchOff.Invoke();
            rend.material = offMat;
        }

        stillOn = on;
        on = false;
    }

    private void FixedUpdate()
    {
    }

    public override void CalculateLight(Vector3 inDirection, RaycastHit hit, ref List<List<Vector3>> positions, int depth, LayerMask layerMask)
    {
        positions[0].Add(hit.point);

        if (!on)
            onSwitchOn.Invoke();
        rend.material = onMat;
        on = true;
        stillOn = true;
    }
}
