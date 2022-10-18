using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private GameObject lightsOffIcon;
    [SerializeField] private GameObject lightsOnIcon;

    [SerializeField] private GameObject lightsOff;
    [SerializeField] private GameObject lightsOn;

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
            lightsOffIcon.SetActive(false);
            lightsOnIcon.SetActive(true);
            lightsOff.SetActive(false);
            lightsOn.SetActive(true);
            lightsOnBool = true;
            StartCoroutine(SwitchCooldown());
        }
    }
    public void LightsOff()
    {
        if (canSwitch)
        {
            lightsOffIcon.SetActive(true);
            lightsOnIcon.SetActive(false);
            lightsOff.SetActive(true);
            lightsOn.SetActive(false);
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
