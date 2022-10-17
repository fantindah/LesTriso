using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private GameObject lightsOff;
    [SerializeField] private GameObject lightsOn;
    [SerializeField] private GameObject lights;
    [SerializeField] private Image cooldownSprite;


    [SerializeField] private float switchCooldown;
   

    public bool lightsOnBool;

    private bool canSwitch = true;
    private float counter=0;

  
    void Start()
    {
        
    }

   
    void Update()
    {
        if (!canSwitch)
        {
            counter+= Time.deltaTime;
            cooldownSprite.fillAmount = 1-(counter / switchCooldown);
        }
    }
    public void LightsOn()
    {
        if (canSwitch)
        {
            lightsOff.SetActive(false);
            lightsOn.SetActive(true);
            lights.SetActive(false);
            lightsOnBool = true;
            StartCoroutine(SwitchCooldown());
        }
    }
    public void LightsOff()
    {
        if (canSwitch)
        {
            lightsOff.SetActive(true);
            lightsOn.SetActive(false);
            lights.SetActive(true);
            lightsOnBool = false;
            StartCoroutine(SwitchCooldown());
        }
    }
    private IEnumerator SwitchCooldown()
    {
        canSwitch = false;
        yield return new WaitForSeconds(switchCooldown);
        canSwitch = true;
        counter = 0;
    }
}
