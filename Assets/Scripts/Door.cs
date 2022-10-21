using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public int doorHealth = 100;
    public Image healthIndicator;
    public GameObject quest, blocker, complex, jauge;

    [HideInInspector] public bool isQuestActive = false;

    private void Update()
    {
        healthIndicator.fillAmount = doorHealth / 100f;

        if (doorHealth <= 25)
        {
            blocker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }
        else if (doorHealth <= 50)
        {
            blocker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        } else if (doorHealth <= 75)
        {
            blocker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        } else if (doorHealth <= 100)
        {
            blocker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isQuestActive)
        {
            quest.GetComponent<DoorQuest>().Unactive();
        }

    }

    private void OnMouseDown()
    {
        if (!isQuestActive && !quest.activeInHierarchy)
        {
            isQuestActive = true;
            quest.SetActive(true);
            DoorQuest _quest = quest.GetComponent<DoorQuest>();
            _quest.door = this;
            _quest.textQuest.SetActive(false);
            _quest.coroutine = StartCoroutine(_quest.DoorQuestCoroutine());
        }
    }

    public bool Damage(int damages)
    {
        doorHealth -= damages;
        return doorHealth <= 0;
    }
}
