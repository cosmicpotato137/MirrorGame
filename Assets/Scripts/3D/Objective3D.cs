using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objective3D : MonoBehaviour
{
    public UnityEvent TriggerEnter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        TriggerEnter.Invoke();
        GameController.gameController.LoadNextScene();
    }

}
