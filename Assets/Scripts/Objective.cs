using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Objective : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.gameController.LoadNextScene();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("test");  
    }
}
