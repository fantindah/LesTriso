using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Scene currentScene;
    public GameObject credits;

    void Start()
    {
        UndisplayCredits();
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

    public void DisplayCredits()
    {
        credits.SetActive(true);
    }

    public void UndisplayCredits()
    {
        credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
