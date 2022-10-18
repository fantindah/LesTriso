using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [Header("Componnents")]
    public WindowFiend windowFiend;

    [Header("Timer Values")]
    private float timeLeft;
    public bool timerOn;

    private void Awake()
    {
        windowFiend = FindObjectOfType<WindowFiend>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                timerOn = false;
                timeLeft = 0;
                windowFiend.isOutOfTime = true;
            }
        }
    }

    public void StartTimer(int selectedTimeLeft)
    {
        Debug.Log("You have " + selectedTimeLeft + " seconds left to click the window");
        timeLeft = selectedTimeLeft;
        timerOn = true;
    }

    private void OnMouseDown()
    {
        if(windowFiend.inDanger && timerOn)
        {
            timerOn = false;
            windowFiend.DangerOff(windowFiend.activeWindow);
        }
    }
}
