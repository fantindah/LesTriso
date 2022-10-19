using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MyLoadScene(int idScene)
    {
        SceneManager.LoadScene(idScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
