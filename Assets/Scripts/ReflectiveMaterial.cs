using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectiveMaterial : LightInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CalculateLight(Vector3 inDirection, RaycastHit2D hit, ref List<List<Vector3>> positions, int depth, LayerMask layerMask)
    {
        positions[0].Add(hit.point);
        positions[0].Add(hit.point);
        positions[0].Add(hit.point);

        Vector3 newDir = Vector3.Reflect(inDirection, hit.normal);
        var newhit = CastRay(newDir, hit, layerMask);
        
        base.CalculateLight(newDir, newhit, ref positions, depth, layerMask);
    }
}
