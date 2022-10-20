using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP : MonoBehaviour
{
    [HideInInspector] public EnemyMovement movescript;

    public bool lightsOn;

    [Header("Time Settings")]
    public float minWaitBeforeAttack = 0;
    public float maxWaitBeforeAttack = 13;

    private void Awake()
    {
        movescript = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        StartCoroutine(StartAttacking());
    }

    // Update is called once per frame
    void Update()
    {
        if (lightsOn)
        {
            if(!movescript.goBack)
            {
                movescript.goBack = true;
            }
        }
    }

    public IEnumerator StartAttacking()
    {
        yield return new WaitForSeconds(Random.Range(minWaitBeforeAttack,maxWaitBeforeAttack));
        Debug.Log("Attack launched");
        StartCoroutine(movescript.Movement());
    }
}
