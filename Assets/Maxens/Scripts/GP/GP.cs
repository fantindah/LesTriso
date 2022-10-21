using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP : MonoBehaviour
{
    [HideInInspector] public EnemyMovement movescript;
    public GameObject[] lightRooms;

    public bool lightsOn;
    public bool isAlreadyRunning;
    public bool isInBathroom = true;

    [Header("Time Settings")]
    public float minWaitBeforeAttack = 0;
    public float maxWaitBeforeAttack = 13;
    public float attackDelay = 3;

    private void Awake()
    {
        movescript = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        StartCoroutine(RandomAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if(!movescript.isGoingBack && (isInBathroom && lightRooms[0].GetComponent<LightSwitch>().lightsOnBool) || (!isInBathroom && lightRooms[1].GetComponent<LightSwitch>().lightsOnBool))
        {
            movescript.goBack = true;
        }
        if(movescript.isGoingBack && (isInBathroom && !lightRooms[0].GetComponent<LightSwitch>().lightsOnBool) || (!isInBathroom && !lightRooms[1].GetComponent<LightSwitch>().lightsOnBool))
        {
            movescript.goBack = true;
        }
    }

    public IEnumerator RandomAttack()
    {        
        if(!isAlreadyRunning && movescript.isBlocked)
        {
        isAlreadyRunning = true;
        float randomTime = Random.Range(minWaitBeforeAttack, maxWaitBeforeAttack);
        Debug.Log("An attack will occur in " + randomTime + " seconds");
        yield return new WaitForSeconds(randomTime);
        movescript.unblocked = true;
        movescript.isGoingBack = false;
        Debug.Log("Attack launched");
        } else
        {
            Debug.LogError("The Enemy Is Currently Running");
        }
        yield return new WaitForSeconds(attackDelay);
        if(movescript.isBlocked)
        {
        isAlreadyRunning = false;
        }
        StartCoroutine(RandomAttack());
    }
}
