using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractable : MonoBehaviour
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

    public virtual void CalculateLight(Vector3 inDirection, RaycastHit2D hit, ref List<List<Vector3>> positions, int depth, LayerMask layerMask)
    {
        Debug.DrawLine(hit.point, hit.point + hit.normal);
        if (depth > maxDepth)
            return;

        if (hit)
        {
            LightInteractable li = null;
            if (hit.collider.gameObject.TryGetComponent<LightInteractable>(out li))
            {
                li.CalculateLight(inDirection, hit, ref positions, depth + 1, layerMask);
            }
            else
                positions[0].Add(hit.point);
        }
        else
            positions[0].Add(inDirection * 1000);
    }
    
    protected RaycastHit2D CastRay(Vector3 direction, RaycastHit2D hit, LayerMask layerMask)
    {
        Vector3 offset;
        if (Vector3.Dot(direction, hit.normal) < 0)
            offset = hit.normal * -.001f;
        else
            offset = hit.normal * .001f;

        var newhit = Physics2D.Raycast(hit.point + (Vector2)offset, direction, Mathf.Infinity, layerMask);

        return newhit;
    }
}
