using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowFiend : MonoBehaviour
{
    [Header("QTE Values")]
    public int dangerStartDelay;
    public int QTETime;

    [HideInInspector] public bool inDanger;
    [HideInInspector] public bool isOutOfTime;
    private bool outOfTimeIsActive;
    [HideInInspector] public int activeWindow;
    public GameObject[] windowList;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //DEBUG COMMAND TO START A WINDOW QTE, DELETE LATER AND CHANGE TO RANDOM EVENT.

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ActivateDangerWindow(dangerStartDelay)); // KEEP AND USE TO START THE QTE
        }

        //DEBUG COMMAND TO START A WINDOW QTE, DELETE LATER AND CHANGE TO RANDOM EVENT.

        if (isOutOfTime && !outOfTimeIsActive)
        {
            OutOfTime();
        }
    }

    /// <summary>
    /// After a changeable delay, selects a random window for the window QTE.
    /// </summary>
    public IEnumerator ActivateDangerWindow(int time)
    {
        Debug.Log("Activating a random window in " + time + " seconds");
        yield return new WaitForSeconds(time);
        DangerOn(Random.Range(0, windowList.Length));
    }

    /// <summary>
    /// Starts the window QTE. Activates the selected window's timer, can't be called if any window QTE is already activated.
    /// </summary>
    public void DangerOn( int windowNumber)
    {
        if(!inDanger)
        {
        Debug.Log("Activating Window QTE on window number " + windowNumber);
        inDanger = true;
        activeWindow = windowNumber;
        windowList[windowNumber].gameObject.GetComponent<Renderer>().material.color = new Color(0.462f, 0, 0, 1);
        windowList[windowNumber].gameObject.GetComponent<Timer>().StartTimer(QTETime);
        }
        else
        {
            Debug.LogError("Window number " + activeWindow + " is already active");
        }
    }

    /// <summary>
    /// Deactivate the Window QTE and reset the windows. Gets called when the correct window is clicked on during the QTE.
    /// </summary>
    public void DangerOff(int activeWindowNumber)
    {
        Debug.Log("Deactivating Window QTE on window number " + activeWindowNumber);
        inDanger = false;
        windowList[activeWindowNumber].gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
    }

    /// <summary>
    /// Gets called when the timer of one of the window reaches zero. can be used to end the game.
    /// </summary>
    public void OutOfTime()
    {
        outOfTimeIsActive = true;
        Debug.Log("YOU LOST");
        // FUNCTION THAT ENDS THE WINDOW EVENT. CALL ANY FUNCTION YOU WANT HERE
    }
}
