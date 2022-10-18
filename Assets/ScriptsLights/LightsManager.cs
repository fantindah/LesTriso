using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    private List<bool> lightsOnList;

    void Start()
    {
        
    }

    void Update()
    {
        lightsOnList = GetAllLightsOn(lightsOnList);
    }
    private List<bool> GetAllLightsOn(List<bool> lightsList)
    {
        lightsList.Clear();
        foreach (Transform child in transform){
            lightsList.Add(child.GetComponent<LightSwitch>().lightsOnBool);  
        }
        return lightsList;
    }
}
