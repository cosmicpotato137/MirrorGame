using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatMirror : MonoBehaviour
{
    Camera cam;
    Camera mainCamera;
    RenderTexture rt;
    Renderer ren;

    private void OnValidate()
    {
        Init();
    }
     
    void Init()
    {
        cam = GetComponentInChildren<Camera>();
        ren = GetComponent<Renderer>();
        Vector2 dim = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        rt = new RenderTexture((int)dim.x, (int)dim.y, 0);
        cam.targetTexture = rt;
        Material mirror = new Material(Shader.Find("Shader Graphs/Mirror"));
        mirror.name = "FlatMirror";
        mirror.SetTexture("_BaseMap", rt);
        ren.sharedMaterial = mirror;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 norm = transform.forward;
        Vector3 camDist = Camera.main.transform.position - transform.position;
        if (Vector3.Dot(norm, camDist) < 0)
        {
            cam.enabled = true;

            Vector3 dir = Vector3.Reflect(camDist, norm);
            Debug.DrawLine(transform.position, -dir.normalized + transform.position, Color.red);
            Debug.DrawLine(Camera.main.transform.position, transform.position, Color.blue);
            Debug.DrawLine(transform.position, norm + transform.position);

            Vector3 f = Vector3.Reflect(Camera.main.transform.forward, norm);
            Vector3 up = Vector3.Reflect(Camera.main.transform.up, norm);

            cam.transform.rotation = Quaternion.LookRotation(f, up);
            cam.transform.position = dir + transform.position;
            cam.nearClipPlane = Vector3.Magnitude(dir);
        }
        else
            cam.enabled = false;
        
    }
}
