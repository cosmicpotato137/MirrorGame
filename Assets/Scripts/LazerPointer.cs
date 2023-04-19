using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPointer : LightInteractable
{
    public float width = .1f;
    public float maxBounces = 1000;
    [Range(0, 10)]
    public int maxSplits = 5;
    public Material lazerMaterial;
    public LayerMask layerMask;

    List<LineRenderer> lineRenderers;

    public bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderers = new List<LineRenderer>();
        LineRenderer[] lrs = GetComponentsInChildren<LineRenderer>();
        int i = 0;
        for (; i < lrs.Length && i < maxSplits; i++)
        {
            lrs[i].gameObject.name = "beam_" + i.ToString();
            lrs[i].startWidth = .1f;
            lrs[i].positionCount = 0;
            lrs[i].sortingLayerID = SortingLayer.NameToID("Lazer");
            lrs[i].material = lazerMaterial;
            lineRenderers.Add(lrs[i]);
        }
        for (; i < maxSplits; i++)
        {
            GameObject g = new GameObject("beam_" + i.ToString());
            g.transform.parent = this.transform;
            g.transform.position = Vector3.zero;
            var lr = g.AddComponent<LineRenderer>();
            lr.startWidth = .1f;
            lr.positionCount = 0;
            lr.sortingLayerID = SortingLayer.NameToID("Lazer");
            lr.material = lazerMaterial;
            lineRenderers.Add(lr);
        }
        for (; i < lrs.Length; i++)
            Destroy(lrs[i].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        var positions = new List<List<Vector3>>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, layerMask);
        CalculateLight(transform.up, hit, ref positions, 0, layerMask);

        int i = 0;
        foreach (var lr in lineRenderers)
        {
            if (i < positions.Count)
            {
                var line = positions[i];
                lr.positionCount = line.Count;
                lr.SetPositions(line.ToArray());
                i++;
            }
            else
                lr.positionCount = 0;
        }
    }

    public override void CalculateLight(Vector3 inDirection, RaycastHit2D hit, ref List<List<Vector3>> positions, int depth, LayerMask layerMask)
    {
        positions.Add(new List<Vector3>{ transform.position });
        base.CalculateLight(inDirection, hit, ref positions, depth, layerMask);
        //for (int numHits = 0; hit && numHits < maxBounces; numHits++)
        //{
        //    LightInteractable li = null;
        //    if (hit.collider.gameObject.TryGetComponent<LightInteractable>(out li))
        //    {
        //        lightDirection = li.CalculateLight(lightDirection, hit, positions, numHits, layerMask);
        //        if (lightDirection == Vector3.zero)
        //            break;

        //        Vector2 offset = Vector2.zero;

        //    }
        //    else
        //    {
        //        positions.Add(hit.point);
        //        break;
        //    }
        //}

        //if (!hit)
        //    positions.Add(lightDirection * 1000);
    }
}
