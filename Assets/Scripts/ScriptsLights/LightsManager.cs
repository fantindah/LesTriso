using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsManager : MonoBehaviour
{
    [SerializeField] private List<bool> lightsOnList = new List<bool>();
    [SerializeField] private List<LightSwitch> AllRoomsScripts = new List<LightSwitch>();
    private Slider electricityGauge;

    void Start()
    {
        electricityGauge = GameObject.Find("ElectricityGauge").GetComponent<Slider>();
        
        foreach (bool isRoomLit in GetAllLightsOn(lightsOnList))
        {
            if(isRoomLit)
                gameObject.GetComponent<ElectricityGaugeUpdate>().IncreaseGauge();
        }
    }

    void Update()
    {
        lightsOnList = GetAllLightsOn(lightsOnList);
        if (electricityGauge.value>=100)
        {
            BlowFuze(GetAllRooms(AllRoomsScripts));
        }
    }

    public List<bool> GetAllLightsOn(List<bool> lightsList)
    { 
        lightsList.Clear();
        foreach (Transform child in transform){
            lightsList.Add(child.GetComponent<LightSwitch>().lightsOnBool);  
        }
        return lightsList;
    }

    public List<LightSwitch> GetAllRooms(List<LightSwitch> AllRooms)
    {
        AllRooms.Clear();
        foreach (Transform child in transform)
        {
            AllRooms.Add(child.GetComponent<LightSwitch>());
        }
        return AllRooms;
    }

    private void BlowFuze(List<LightSwitch> AllRooms)
    {
        foreach (LightSwitch room in AllRooms)
        {
            room.StopAllCoroutines();
            room.canSwitch = true;
            room.LightsOff();
            electricityGauge.value = 0;
        }
    }

}
