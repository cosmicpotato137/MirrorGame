using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public float width = .1f;
    public float maxBounces = 1000;


    LineRenderer lr;

    public bool on = false;

    string[] layermask =
    {
        "LightInteractable", 
        "Lazer"
    };

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CalcPoints();
    }

    public void CalcPoints()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(transform.position);

        Vector3 lightDirection = transform.up;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lightDirection, Mathf.Infinity, 
            LayerMask.GetMask(new string[] { "LightInteractable" }));
        for (int numHits = 0; hit && numHits < maxBounces; numHits++)
        {
            LightInteractable li = null;
            if (hit.collider.gameObject.TryGetComponent<LightInteractable>(out li))
            {
                lightDirection = li.CalculateLight(lightDirection, hit, ref positions);
                if (lightDirection == Vector3.zero)
                    break;

                Vector2 offset = Vector2.zero;
                if (Vector3.Dot(lightDirection, hit.normal) < 0)
                    offset = hit.normal * -.001f;
                else
                    offset = hit.normal * .001f;
                hit = Physics2D.Raycast(hit.point + offset, lightDirection, Mathf.Infinity, 
                    LayerMask.GetMask(layermask));
            }
            else
            {
                positions.Add(hit.point);
                break;
            }
        }

        if (!hit)
            positions.Add(lightDirection * 1000);

        lr.positionCount = positions.Count;
        lr.SetPositions(positions.ToArray());
    }
}
