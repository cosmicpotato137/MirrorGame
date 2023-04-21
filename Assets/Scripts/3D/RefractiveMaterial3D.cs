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

        List<List<Vector3>> reflection = new List<List<Vector3>>();

        float ri2;
        float ri1;

        RaycastHit testHit;
        var testb = Physics.Raycast(new Ray(hit.point + hit.normal * .001f, hit.normal), out testHit, Mathf.Infinity, layerMask);
        if (testb && testHit.collider.gameObject == hit.collider.gameObject)
        {
            ri1 = refractiveIndex;
            ri2 = 1;
        }
        else
        {
            reflection = CalculateReflection(inDirection, hit, depth, layerMask);
            ri1 = 1;
            ri2 = refractiveIndex;
        }
        float angle1 = Vector3.Angle(-inDirection, hit.normal);
        float angle2 = Mathf.Asin(Mathf.Sin(Mathf.Deg2Rad * ri1 * angle1 / ri2)) * Mathf.Rad2Deg;
        var newDir = Quaternion.AngleAxis(angle2, Vector3.Cross(inDirection, hit.normal)) * -hit.normal;
        RaycastHit newHit;
        var b = CastRay(newDir, hit, out newHit, layerMask);

        if (b)
            base.CalculateLight(newDir, newHit, ref positions, depth + 1, layerMask);
        else
            positions[0].Add(newDir * 1000);

        foreach (var ray in reflection)
            positions.Add(ray);
    }

    private List<List<Vector3>> CalculateReflection(Vector3 inDirection, RaycastHit hit, int depth, LayerMask layerMask)
    {
        List<List<Vector3>> refPositions = new List<List<Vector3>>();
        refPositions.Add(new List<Vector3> { hit.point });

        Vector3 refDir = Vector3.Reflect(inDirection, hit.normal);
        RaycastHit newHit;
        var b = CastRay(refDir, hit, out newHit, layerMask);

        if (b)
            base.CalculateLight(refDir, newHit, ref refPositions, depth + 1, layerMask);
        else
            refPositions[0].Add(refDir * 1000.0f);

        return refPositions;
    }
}
