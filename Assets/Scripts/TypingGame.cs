using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingGame : MonoBehaviour
{
    [SerializeField]
    private List<string> AllCodeLines = new List<string>();

    [SerializeField]
    private TMP_InputField InputField;

    [SerializeField]
    private TMP_Text TextToWrite;


    // Start is called before the first frame update
    void Start()
    {
        TextToWrite.text = AllCodeLines[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CompareText(string textWritten)
    {
        if(textWritten == AllCodeLines[0])
        {
            Debug.Log("Mission Success");
        }
        else
        {
            if (textWritten.Length != 0 && textWritten[textWritten.Length-1] != '_')
                InputField.text = textWritten + "_";
            if (textWritten.Length > AllCodeLines[0].Length /*&& textWritten[textWritten.Length - 1] == '_'*/)
            {
                InputField.text = textWritten.Replace("_", "");
            }
        }
    }
}
