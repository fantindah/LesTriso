using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorQuest : MonoBehaviour
{

    public Door door;
    public GameObject doorObject;
    public GameObject cible;

    public Vector2 spawnArea;
    public GameObject textQuest;
    private GameObject currentTarget;

    private void Start()
    {
        StartCoroutine(DoorQuestCoroutine());
    }

    IEnumerator DoorQuestCoroutine()
    {
        while (true)
        {
            Vector3 _spawnArea = new Vector3(Random.Range(spawnArea.x / -2, spawnArea.x / 2), Random.Range(spawnArea.y / -2, spawnArea.y / 2), 0);
            currentTarget = Instantiate(cible, doorObject.transform.position + _spawnArea, Quaternion.identity);
            yield return new WaitUntil(() => currentTarget == null);
            
            if(door.doorHealth % 25 > 22)
            {
                door.doorHealth = 25 * ((door.doorHealth / 25) + 1);
            } else if(door.doorHealth % 25 != 0)
            {
                door.doorHealth += 3;
            }
        }
    }

    public void Unactive()
    {
        door.isQuestActive = false;
        door = null;
        textQuest.SetActive(true);
        Destroy(currentTarget);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(doorObject.transform.position, spawnArea);
    }

}
