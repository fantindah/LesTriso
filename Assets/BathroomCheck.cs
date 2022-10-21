using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomCheck : MonoBehaviour
{

    [HideInInspector] public GP burglarScript;
    private void Awake()
    {
        burglarScript = GetComponent<GP>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if(collision.CompareTag("EnemyBurglar"))
        {
            burglarScript.isInBathroom = true;
            Debug.Log("Burglar entered the room");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("EnemyBurglar"))
        {
            burglarScript.isInBathroom = false;
            Debug.Log("Burglar exited the room");
        }
    }
}
