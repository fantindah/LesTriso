using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LightsManager : MonoBehaviour
{
    [SerializeField] private List<bool> lightsOnList = new List<bool>();
    [SerializeField] private Slider electricityGauge;

    [Range(1f, 5f)]
    [SerializeField] private float electricityIncreaseTime;

    [Range(1f, 5f)]
    [SerializeField] private float electricityDecreaseTime;

    [SerializeField] private float electricityPerRoomAmount = 20f;

    void Start()
    {
        
    }

    void Update()
    {
        //lightsOnList = GetAllLightsOn(lightsOnList);
    }
    public List<bool> GetAllLightsOn(List<bool> lightsList)
    { 
        lightsList.Clear();
        foreach (Transform child in transform){
            lightsList.Add(child.GetComponent<LightSwitch>().lightsOnBool);  
        }
        return lightsList;
    }
    public void UpdateGauge()
    {
        lightsOnList.Clear();

        lightsOnList = GetAllLightsOn(lightsOnList);

        foreach (bool isRoomLit in lightsOnList)
        {
            if (isRoomLit)
                StartCoroutine(ProgressiveLightUpdate(electricityPerRoomAmount, electricityIncreaseTime));
            else
                StartCoroutine(ProgressiveLightUpdate(-electricityPerRoomAmount, electricityDecreaseTime));
        }
    }
    public IEnumerator ProgressiveLightUpdate(float ammount, float time)
    {
        float ammountLeft = Mathf.Abs(ammount);
        while(ammountLeft > 0)
        {
            electricityGauge.value += (ammount/time)/10;
            ammountLeft -= (ammount / time) / 10;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
