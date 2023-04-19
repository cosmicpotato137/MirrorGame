using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class HollowSphereCollider : MonoBehaviour
{
    EdgeCollider2D ec;

    public Vector2 center = Vector2.zero;
    public float radius = .5f;
    [Range(3, 100)]
    public int numPoints;

    // Start is called before the first frame update
    void Start()
    {
        ec = GetComponent<EdgeCollider2D>();
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i <= numPoints; i++)
        {
            float angle = Mathf.PI / (float)numPoints * i * 2;
            points.Add(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius + center);
        }

        ec.SetPoints(points);
    }
}
