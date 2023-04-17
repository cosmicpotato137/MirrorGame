using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractiveMaterial : LightInteractable
{
    public float refractiveIndex = 1.0f;

    static RefractiveMaterial lastMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Vector3 CalculateLight(Vector3 inDirection, RaycastHit2D hit, ref List<Vector3> positions)
    {
        positions.Add(hit.point);

        float ri2;
        float ri1;
        if (lastMaterial == null)
        {
            lastMaterial = this;
            ri2 = lastMaterial.refractiveIndex;
            ri1 = 1;
        }
        else if (lastMaterial != this)
        {
            ri1 = lastMaterial.refractiveIndex;
            ri2 = this.refractiveIndex;
            lastMaterial = this;
        }
        else
        {
            ri2 = 1;
            ri1 = lastMaterial.refractiveIndex;
            lastMaterial = null;
        }
        float angle1 = Vector3.Angle(-inDirection, hit.normal);
        float angle2 = Mathf.Asin(Mathf.Sin(Mathf.Deg2Rad * ri1 * angle1 / ri2)) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle2, Vector3.Cross(inDirection, hit.normal)) * -hit.normal;
    }
}
