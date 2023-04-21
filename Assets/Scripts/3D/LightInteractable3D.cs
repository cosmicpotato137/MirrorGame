using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractable3D : MonoBehaviour
{
    public static int maxDepth = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void CalculateLight(Vector3 inDirection, RaycastHit hit, ref List<List<Vector3>> positions, int depth, LayerMask layerMask)
    {
        Debug.DrawLine(hit.point, hit.point + hit.normal);
        if (depth > maxDepth)
            return;

        LightInteractable3D li = null;
        if (hit.collider.gameObject.TryGetComponent<LightInteractable3D>(out li))
        {
            li.CalculateLight(inDirection, hit, ref positions, depth + 1, layerMask);
        }
        else
            positions[0].Add(hit.point);
    }
    
    protected bool CastRay(Vector3 direction, RaycastHit hit, out RaycastHit newhit, LayerMask layerMask)
    {
        Vector3 offset;
        if (Vector3.Dot(direction, hit.normal) < 0)
            offset = hit.normal * -.001f;
        else
            offset = hit.normal * .001f;

        var b = Physics.Raycast(new Ray(hit.point + offset, direction), out newhit, Mathf.Infinity, layerMask);

        return b;
    }
}
