using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPointer3D : LightInteractable3D
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
            lrs[i].startWidth = width;
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
            lr.startWidth = width;
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
        RaycastHit hit;
        bool h = Physics.Raycast(new Ray(transform.position, transform.up), out hit, Mathf.Infinity, layerMask);
        if (h)
            CalculateLight(transform.up, hit, ref positions, 0, layerMask);
        else
            positions.Add(new List<Vector3>{ transform.position, transform.up * 1000.0f });

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

    public override void CalculateLight(Vector3 inDirection, RaycastHit hit, ref List<List<Vector3>> positions, int depth, LayerMask layerMask)
    {
        positions.Add(new List<Vector3>{ transform.position });
        base.CalculateLight(inDirection, hit, ref positions, depth, layerMask);
    }
}
