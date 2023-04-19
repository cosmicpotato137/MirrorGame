using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractiveMaterial : LightInteractable
{
    public float refractiveIndex = 1.0f;

    RefractiveMaterial lastMaterial;

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

        List<List<Vector3>> reflection = new List<List<Vector3>>();

        float ri2;
        float ri1;
        var testhit = Physics2D.Raycast(hit.point + hit.normal * .001f, hit.normal, Mathf.Infinity, layerMask);
        if (!testhit || testhit.collider != hit.collider)
        {
            reflection = CalculateReflection(inDirection, hit, depth, layerMask);
            ri2 = refractiveIndex;
            ri1 = 1;
        }
        else
        {
            ri2 = 1;
            ri1 = refractiveIndex;
        }
        float angle1 = Vector3.Angle(-inDirection, hit.normal);
        float angle2 = Mathf.Asin(Mathf.Sin(Mathf.Deg2Rad * ri1 * angle1 / ri2)) * Mathf.Rad2Deg;
        var newDir = Quaternion.AngleAxis(angle2, Vector3.Cross(inDirection, hit.normal)) * -hit.normal;
        var newHit = CastRay(newDir, hit, layerMask);

        base.CalculateLight(newDir, newHit, ref positions, depth + 1, layerMask);

        foreach (var ray in reflection)
            positions.Add(ray);
    }


    private List<List<Vector3>> CalculateReflection(Vector3 inDirection, RaycastHit2D hit, int depth, LayerMask layerMask)
    {
        List<List<Vector3>> refPositions = new List<List<Vector3>>();
        refPositions.Add(new List<Vector3> { hit.point });

        Vector3 refDir = Vector3.Reflect(inDirection, hit.normal);
        var refHit = CastRay(refDir, hit, layerMask);

        base.CalculateLight(refDir, refHit, ref refPositions, depth + 1, layerMask);

        return refPositions;
    }
}
