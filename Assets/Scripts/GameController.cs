using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static int currentScene = 0;

    public static GameController gameController;

    private void Awake()
    {
        GameController[] objs = GameObject.FindObjectsOfType<GameController>();
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        gameController = this;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentScene);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadNextScene()
    {
        if (currentScene < SceneManager.sceneCountInBuildSettings - 1)
            currentScene++;
        else
            currentScene = 0;
        
        SceneManager.LoadScene(currentScene);
    }
}
