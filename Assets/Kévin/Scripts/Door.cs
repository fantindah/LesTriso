using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public int doorHealth = 100;
    public Image healthIndicator;
    public GameObject quest, blocker;

    private bool isQuestActive = false;

    private void Update()
    {
        healthIndicator.fillAmount = doorHealth / 100f;

        if(doorHealth <= 25)
        {
            blocker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }
        else if (doorHealth <= 50)
        {
            blocker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        } else if (doorHealth <= 75)
        {
            blocker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        } else if(doorHealth <= 100)
        {
            blocker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }

    private void OnMouseDown()
    {
        if (isQuestActive)
        {
            isQuestActive = false;
            quest.SetActive(false);
        } else
        {
            isQuestActive = true;
            quest.SetActive(true);
        }
    }
}
