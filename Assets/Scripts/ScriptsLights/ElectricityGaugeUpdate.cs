using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityGaugeUpdate : MonoBehaviour
{
    private Slider electricityGauge;

    [Range(1f, 5f)]
    [SerializeField] private float electricityIncreaseTime;

    [Range(1f, 5f)]
    [SerializeField] private float electricityDecreaseTime;

    [SerializeField] private float electricityPerRoomAmount = 20f;

    void Start()
    {
        electricityGauge = GameObject.Find("ElectricityGauge").GetComponent<Slider>();
    }

    void Update()
    {
        
    }

    public void IncreaseGauge()
    {
        StartCoroutine(ProgressiveLightUpdate(electricityGauge.value, electricityPerRoomAmount, electricityIncreaseTime));
    }

    public void DecreaseGauge()
    {
        StartCoroutine(ProgressiveLightUpdate(electricityGauge.value , - electricityPerRoomAmount, electricityDecreaseTime));
    }

    private IEnumerator ProgressiveLightUpdate(float oldValue, float ammount, float time)
    {
        float ammountLeft = Mathf.Abs(ammount);
        if (ammount > 0)
        {
            while (ammountLeft > 0)
            {
                electricityGauge.value += ammount / (time / Time.deltaTime);
                ammountLeft -= ammount / (time / Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (ammountLeft > 0)
            {
                electricityGauge.value -= Mathf.Abs(ammount) / (time / Time.deltaTime);
                ammountLeft -= Mathf.Abs(ammount) / (time / Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
