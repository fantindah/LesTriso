using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private GameObject lightsOffIcon;
    [SerializeField] private GameObject lightsOnIcon;

    [SerializeField] private GameObject lightsOff;
    [SerializeField] private GameObject lightsOn;

    [SerializeField] private Image cooldownSprite;


    [SerializeField] private float switchCooldown;
   

    public bool lightsOnBool=false;

    public bool canSwitch = true;

  
    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
        if (!canSwitch)
        {
            cooldownSprite.fillAmount -= 1 / switchCooldown * Time.deltaTime;
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

            transform.GetComponent<ElectricityGaugeUpdate>().IncreaseGauge();

            StartCoroutine(SwitchCooldown());
        }
    }

    public void LightsOnWithoutCooldown()
    {
        if (canSwitch)
        {
            lightsOffIcon.SetActive(false);
            lightsOnIcon.SetActive(true);
            lightsOff.SetActive(false);
            lightsOn.SetActive(true);

            lightsOnBool = true;

            transform.GetComponent<ElectricityGaugeUpdate>().IncreaseGauge();

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

            transform.GetComponent<ElectricityGaugeUpdate>().DecreaseGauge();

            StartCoroutine(SwitchCooldown());
        }
    }

    private IEnumerator SwitchCooldown()
    {
        cooldownSprite.fillAmount = 1;
        canSwitch = false;
        yield return new WaitForSeconds(switchCooldown);
        canSwitch = true;
    }
}
