using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFBI : MonoBehaviour
{
    public List<Door> doorsToBreak = new();
    public List<LightSwitch> isLightOn = new();
    public EnemyMovement movement;
    public float cooldownDoorHiting;
    public int damages;

    private float currentCooldownDoorHiting;
    private bool isTransitory = false;

    private void Start()
    {
        currentCooldownDoorHiting = cooldownDoorHiting;
        isLightOn[0].LightsOn();
    }

    private void Update()
    {
        if (movement.isBlocked)
        {
            if (!movement.isGoingBack)
            {

                if (isTransitory)
                {
                    isTransitory = false;
                    isLightOn[0].LightsOn();
                    movement.unblocked = true;
                }

                currentCooldownDoorHiting -= Time.deltaTime;
                if (currentCooldownDoorHiting <= 0)
                {
                    currentCooldownDoorHiting = cooldownDoorHiting;
                    if (doorsToBreak[0].Damage(damages))
                    {
                        Destroy(doorsToBreak[0].complex);
                        doorsToBreak.RemoveAt(0);
                        isLightOn.RemoveAt(0);

                        isTransitory = true;
                        movement.unblocked = true;
                    }
                }
            } else
            {
                isLightOn[0].LightsOn();
                movement.unblocked = true;
                movement.goBack = true;
            }
                
        }

        if(!movement.isGoingBack && !isLightOn[0].lightsOnBool && !isTransitory)
        {
            movement.goBack = true;
        }

        if (!doorsToBreak[0].jauge.activeInHierarchy) doorsToBreak[0].jauge.SetActive(true);
    }
}
