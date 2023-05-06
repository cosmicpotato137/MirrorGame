using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractiveMaterial3D : LightInteractable3D
{
    public float refractiveIndex = 1.0f;

    RefractiveMaterial3D lastMaterial;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CalculateLight(Vector3 inDirection, RaycastHit hit, ref List<List<Vector3>> positions, int depth, LayerMask layerMask)
    {
        positions[0].Add(hit.point);

        List<List<Vector3>> reflection = CalculateReflection(inDirection, hit, depth, layerMask);

        // calculate entrance refraction
        Vector3 newDir = CalculateRefraction(hit, inDirection, 1, refractiveIndex);

        // calculate exit refraction
        RaycastHit testHit;
        float maxDist = hit.collider.bounds.size.sqrMagnitude;
        bool n = hit.collider.Raycast(new Ray(hit.point + newDir * maxDist, -newDir), out testHit, Mathf.Infinity);
        Debug.DrawLine(testHit.point, testHit.point + testHit.normal, Color.blue);

        if (n)
        {
            positions[0].Add(testHit.point);
            newDir = CalculateRefraction(testHit, newDir, refractiveIndex, 1, true);
        }

        // continue ray tracing
        RaycastHit newHit;
        var b = CastRay(newDir, hit, out newHit, layerMask);
        if (b)
            base.CalculateLight(newDir, newHit, ref positions, depth + 1, layerMask);
        else
            positions[0].Add(newDir * 1000);

        foreach (var ray in reflection)
            positions.Add(ray);
    }

    private Vector3 CalculateRefraction(RaycastHit hit, Vector3 inDirection, float ri1, float ri2, bool invertNormal = false)
    {
        Vector3 normal = invertNormal ? -hit.normal : hit.normal;
        float angle1 = Vector3.Angle(-inDirection, normal);
        float angle2 = Mathf.Asin(Mathf.Sin(Mathf.Deg2Rad * ri1 * angle1 / ri2)) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle2, Vector3.Cross(inDirection, normal)) * -normal;
    }

    private List<List<Vector3>> CalculateReflection(Vector3 inDirection, RaycastHit hit, int depth, LayerMask layerMask)
    {
        List<List<Vector3>> refPositions = new List<List<Vector3>>();
        refPositions.Add(new List<Vector3> { hit.point });

        Vector3 refDir = Vector3.Reflect(inDirection, hit.normal);
        RaycastHit newHit;
        bool b = CastRay(refDir, hit, out newHit, layerMask);

        if (b)
            base.CalculateLight(refDir, newHit, ref refPositions, depth + 1, layerMask);
        else
            refPositions[0].Add(refDir * 1000.0f);

        return refPositions;
    }
}
