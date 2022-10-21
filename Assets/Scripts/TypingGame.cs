using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class TypingGame : MonoBehaviour
{
    [SerializeField]
    private List<string> AllCodeLines = new List<string>();

    [SerializeField]
    private TMP_InputField InputField;

    [SerializeField]
    private TMP_Text TextToWrite;

    void Start()
    {
        TextToWrite.text = AllCodeLines[0];
    }

    void Update()
    {
        if(AllCodeLines.Count == 0)
        {
            SceneManager.LoadScene(2);
        }
        if (gameObject.activeSelf)
        {
            InputField.Select();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void CompareText(string textWritten)
    {
        if(textWritten.Replace("_", "") == AllCodeLines[0])
        {
            AllCodeLines.RemoveAt(0);

            if (AllCodeLines.Count != 0)
            {
                InputField.text = "_";           
                TextToWrite.text = AllCodeLines[0];
            }
            else
            {
                InputField.gameObject.SetActive(false);
                TextToWrite.text = "All Missions Completed!";
            }
                
            gameObject.SetActive(false);
        }
        else
        {
            if (textWritten.Length != 0 && textWritten[textWritten.Length - 1] != '_')
                InputField.text = textWritten + "_";
            else
                InputField.text = textWritten;
        }
    }
}
