using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFBI : MonoBehaviour
{
    public List<Door> doorsToBreak = new();
    public List<bool> isLightOn = new();
    public EnemyMovement movement;
    public float cooldownDoorHiting;
    public int damages;

    private float currentCooldownDoorHiting;

    private void Start()
    {
        currentCooldownDoorHiting = cooldownDoorHiting;
    }

    private void Update()
    {
        if (movement.isBlocked)
        {
            currentCooldownDoorHiting -= Time.deltaTime;
            if(currentCooldownDoorHiting <= 0)
            {
                currentCooldownDoorHiting = cooldownDoorHiting;
                if (doorsToBreak[0].Damage(damages))
                {
                    Destroy(doorsToBreak[0].complex);
                    doorsToBreak.RemoveAt(0);
                    isLightOn.RemoveAt(0);

                    movement.unblocked = true;
                }
            }
        }

        if (!isLightOn[0] && !movement.isGoingBack)
        {
            movement.goBack = true;
            StartCoroutine(WaitingForLight());
        }

        if (!doorsToBreak[0].jauge.activeInHierarchy) doorsToBreak[0].jauge.SetActive(true);
    }
    
    IEnumerator WaitingForLight()
    {
        yield return new WaitUntil(() => isLightOn[0] = true);
        movement.goBack = true;
    }
}
