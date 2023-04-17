using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual Vector3 CalculateLight(Vector3 inDirection, RaycastHit2D hit, ref List<Vector3> positions)
    {
        positions.Add(hit.point);
        return Vector3.zero;
    }
}
